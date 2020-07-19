using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 安全试题库
    /// </summary>
    public static class TrainTestDBService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取安全试题库信息
        /// </summary>
        /// <param name="trainTestDBId"></param>
        /// <returns></returns>
        public static Model.Training_TrainTestDB GetTrainTestDBById(string trainTestDBId)
        {
            return Funs.DB.Training_TrainTestDB.FirstOrDefault(e => e.TrainTestId == trainTestDBId);
        }

        /// <summary>
        /// 根据上一节点的id获取安全试题库信息
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public static List<Model.Training_TrainTestDB> GetTrainTestDBBySupTrainTestId(string parentId)
        {
            return (from x in Funs.DB.Training_TrainTestDB where x.SupTrainTestId == parentId orderby x.TrainTestCode select x).ToList(); ;
        }

        /// <summary>
        /// 添加安全试题库
        /// </summary>
        /// <param name="trainTestDB"></param>
        public static void AddTrainTestDB(Model.Training_TrainTestDB trainTestDB)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Training_TrainTestDB newTrainTestDB = new Model.Training_TrainTestDB
            {
                TrainTestId = trainTestDB.TrainTestId,
                TrainTestCode = trainTestDB.TrainTestCode,
                TrainTestName = trainTestDB.TrainTestName,
                SupTrainTestId = trainTestDB.SupTrainTestId,
                IsEndLever = trainTestDB.IsEndLever
            };
            db.Training_TrainTestDB.InsertOnSubmit(newTrainTestDB);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改安全试题库
        /// </summary>
        /// <param name="trainTestDB"></param>
        public static void UpdateTrainTestDB(Model.Training_TrainTestDB trainTestDB)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Training_TrainTestDB newTrainTestDB = db.Training_TrainTestDB.FirstOrDefault(e => e.TrainTestId == trainTestDB.TrainTestId);
            if (newTrainTestDB != null)
            {
                newTrainTestDB.TrainTestCode = trainTestDB.TrainTestCode;
                newTrainTestDB.TrainTestName = trainTestDB.TrainTestName;
                newTrainTestDB.SupTrainTestId = trainTestDB.SupTrainTestId;
                newTrainTestDB.IsEndLever = trainTestDB.IsEndLever;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除安全试题库
        /// </summary>
        /// <param name="trainTestDBId"></param>
        public static void DeleteTrainTestDB(string trainTestId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Training_TrainTestDB trainTestDB = db.Training_TrainTestDB.FirstOrDefault(e => e.TrainTestId == trainTestId);
            if (trainTestDB != null)
            {
                db.Training_TrainTestDB.DeleteOnSubmit(trainTestDB);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 是否可删除资源节点
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-可以，false-不可以</returns>
        public static bool IsDeleteTrainTestDB(string trainTestId)
        {
            bool isDelete = true;
            var trainTestDB = BLL.TrainTestDBService.GetTrainTestDBById(trainTestId);
            if (trainTestDB != null)
            {
                if (trainTestDB.IsBuild == true)
                {
                    isDelete = false;
                }
                if (trainTestDB.IsEndLever == true)
                {
                    var detailCout = Funs.DB.Training_TrainTestDBItem.FirstOrDefault(x => x.TrainTestId == trainTestId);
                    if (detailCout != null)
                    {
                        isDelete = false;
                    }
                }
                else
                {
                    var supItemSetCount = BLL.TrainTestDBService.GetTrainTestDBBySupTrainTestId(trainTestId);
                    if (supItemSetCount.Count() > 0)
                    {
                        isDelete = false;
                    }
                }
            }

            return isDelete;
        }

    }
}
