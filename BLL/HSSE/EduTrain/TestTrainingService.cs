using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class TestTrainingService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Training_TestTraining GetTestTrainingById(string TrainingId)
        {
            return new Model.SGGLDB(Funs.ConnString).Training_TestTraining.FirstOrDefault(e => e.TrainingId == TrainingId);
        }

        /// <summary>
        /// 添加试题类型信息
        /// </summary>
        /// <param name="Training"></param>
        public static void AddTestTraining(Model.Training_TestTraining TestTraining)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Training_TestTraining newTestTraining = new Model.Training_TestTraining
            {
                TrainingId = TestTraining.TrainingId,
                TrainingCode = TestTraining.TrainingCode,
                TrainingName = TestTraining.TrainingName,
                SupTrainingId = TestTraining.SupTrainingId ?? "0",
                IsEndLever = TestTraining.IsEndLever
            };
            db.Training_TestTraining.InsertOnSubmit(newTestTraining);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改试题类型信息
        /// </summary>
        /// <param name="Training"></param>
        public static void UpdateTestTraining(Model.Training_TestTraining TestTraining)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Training_TestTraining newTestTraining = db.Training_TestTraining.FirstOrDefault(e => e.TrainingId == TestTraining.TrainingId);
            if (newTestTraining != null)
            {
                newTestTraining.TrainingCode = TestTraining.TrainingCode;
                newTestTraining.TrainingName = TestTraining.TrainingName;
                newTestTraining.SupTrainingId = TestTraining.SupTrainingId;
                newTestTraining.IsEndLever = TestTraining.IsEndLever;

                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="TrainingId"></param>
        public static void DeleteTestTrainingById(string TrainingId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Training_TestTraining TestTraining = db.Training_TestTraining.FirstOrDefault(e => e.TrainingId == TrainingId);
            if (TestTraining != null)
            {
                var TrainingItem = from x in db.Training_TestTrainingItem where x.TrainingId == TrainingId select x;
                if (TrainingItem.Count() > 0)
                {
                    db.Training_TestTrainingItem.DeleteAllOnSubmit(TrainingItem);
                }

                db.Training_TestTraining.DeleteOnSubmit(TestTraining);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取试题类型列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.Training_TestTraining> GetTestTrainingList()
        {
            return (from x in db.Training_TestTraining orderby x.TrainingCode select x).ToList();
        }

        /// <summary>
        /// 获取试题类型列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.Training_TestTraining> GetEndTestTrainingList()
        {
            return (from x in db.Training_TestTraining
                    where x.IsEndLever == true
                    orderby x.TrainingCode
                    select x).ToList();
        }

        #region 获取末级题目下拉框
        /// <summary>
        /// 用户下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="projectId">项目id</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitTestTrainingDropDownList(FineUIPro.DropDownList dropName,  bool isShowPlease)
        {
            dropName.DataValueField = "TrainingId";
            dropName.DataTextField = "TrainingName";
            dropName.DataSource = GetEndTestTrainingList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}
