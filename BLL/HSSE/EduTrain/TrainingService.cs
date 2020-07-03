using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class TrainingService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据上级Id查询所有教育培训主键列的值
        /// </summary>
        /// <param name="supItem">上级Id</param>
        /// <returns>教育培训主键列值的集合</returns>
        public static List<string> GetTrainingIdsBySupItem(string supItem)
        {
            return (from x in Funs.DB.Training_Training where x.SupTrainingId == supItem select x.TrainingId).ToList();
        }

        /// <summary>
        /// 根据上级Id查询所有教育培训
        /// </summary>
        /// <param name="supItem">上级Id</param>
        /// <returns>教育培训的集合</returns>
        public static List<Model.Training_Training> GetTrainingBySupItem(string supItem)
        {
            return (from x in Funs.DB.Training_Training where x.SupTrainingId == supItem select x).ToList();
        }

        /// <summary>
        /// 根据教育培训Id查询教育培训实体
        /// </summary>
        /// <param name="TrainingId">教育培训主键</param>
        /// <returns>教育培训实体</returns>
        public static Model.Training_Training GetTrainingByTrainingId(string TrainingId)
        {
            Model.Training_Training Training = Funs.DB.Training_Training.FirstOrDefault(e => e.TrainingId == TrainingId);
            return Training;
        }

        /// <summary>
        /// 添加教育培训
        /// </summary>
        /// <param name="Training">教育培训实体</param>
        public static void AddTraining(Model.Training_Training Training)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Training_Training newTraining = new Model.Training_Training
            {
                TrainingId = Training.TrainingId,
                TrainingCode = Training.TrainingCode,
                TrainingName = Training.TrainingName,
                SupTrainingId = Training.SupTrainingId,
                IsEndLever = Training.IsEndLever,
                IsBuild = false
            };
            db.Training_Training.InsertOnSubmit(newTraining);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改教育培训
        /// </summary>
        /// <param name="Training">教育培训实体</param>
        public static void UpdateTraining(Model.Training_Training Training)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Training_Training newTraining = db.Training_Training.First(e => e.TrainingId == Training.TrainingId);
            newTraining.TrainingCode = Training.TrainingCode;
            newTraining.TrainingName = Training.TrainingName;
            newTraining.SupTrainingId = Training.SupTrainingId;
            newTraining.IsEndLever = Training.IsEndLever;
            newTraining.IsBuild = Training.IsBuild;
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据教育培训Id删除一个教育培训
        /// </summary>
        /// <param name="TrainingId">教育培训ID</param>
        public static void DeleteTrainingByTrainingId(string TrainingId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Training_Training Training = db.Training_Training.FirstOrDefault(e => e.TrainingId == TrainingId);
            if (Training != null)
            {
                BLL.TrainingItemService.DeleteTrainingItemsByTrainingId(Training.TrainingId);
                db.Training_Training.DeleteOnSubmit(Training);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据上级Id删除所有对应的教育培训
        /// </summary>
        /// <param name="supItem">上级Id</param>
        public static void DeleteTrainingBySupItem(string supItem)
        {
            Model.SGGLDB db = Funs.DB;
            var q = (from x in db.Training_Training where x.SupTrainingId == supItem select x).ToList();
            if (q.Count() > 0)
            {
                foreach (var item in q)
                {
                    DeleteTrainingByTrainingId(item.TrainingId);
                }
            }
        }


        /// <summary>
        /// 是否存在检查项名称
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-存在，false-不存在</returns>
        public static bool IsExistCheckItemName(string TrainingId, string SupTrainingId, string trainingName)
        {
            var q = Funs.DB.Training_Training.FirstOrDefault(x => x.SupTrainingId == SupTrainingId && x.TrainingName == trainingName
                    && x.TrainingId != TrainingId);
            if (q != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 是否可删除资源节点
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-可以，false-不可以</returns>
        public static bool IsDeleteTraining(string TrainingId)
        {
            bool isDelete = true;
            var Training = BLL.TrainingService.GetTrainingByTrainingId(TrainingId);
            if (Training != null)
            {
                //if (Training.IsBuild == true)
                //{
                //    isDelete = false;
                //}
                if (Training.IsEndLever == true)
                {
                    var detailCout = Funs.DB.Training_TrainingItem.FirstOrDefault(x => x.TrainingId == TrainingId);
                    if (detailCout != null)
                    {
                        isDelete = false;
                    }
                }
                else
                {
                    var supItemSetCount = BLL.TrainingService.GetTrainingBySupItem(TrainingId);
                    if (supItemSetCount.Count() > 0)
                    {
                        isDelete = false;
                    }
                }
            }

            return isDelete;
        }

        /// <summary>
        /// 获取是否末级项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_HSSEStandardListType> GetIsEndLeverList()
        {
            Model.Base_HSSEStandardListType t1 = new Model.Base_HSSEStandardListType
            {
                TypeId = "true",
                TypeName = "是"
            };
            Model.Base_HSSEStandardListType t2 = new Model.Base_HSSEStandardListType
            {
                TypeId = "false",
                TypeName = "否"
            };
            List<Model.Base_HSSEStandardListType> list = new List<Model.Base_HSSEStandardListType>
            {
                t1,
                t2
            };
            return list;
        }
    }
}
