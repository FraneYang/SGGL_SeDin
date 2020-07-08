using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 培训级别
    /// </summary>
    public static class TrainLevelService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取主键
        /// </summary>
        /// <param name="trainLevelId"></param>
        /// <returns></returns>
        public static Model.Base_TrainLevel GetTrainLevelById(string trainLevelId)
        {
            return new Model.SGGLDB(Funs.ConnString).Base_TrainLevel.FirstOrDefault(e => e.TrainLevelId == trainLevelId);
        }

        /// <summary>
        /// 添加培训级别
        /// </summary>
        /// <param name="trainLevel"></param>
        public static void AddTrainLevel(Model.Base_TrainLevel trainLevel)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_TrainLevel newTrainLevel = new Model.Base_TrainLevel
            {
                TrainLevelId = trainLevel.TrainLevelId,
                TrainLevelCode = trainLevel.TrainLevelCode,
                TrainLevelName = trainLevel.TrainLevelName,
                Remark = trainLevel.Remark
            };
            db.Base_TrainLevel.InsertOnSubmit(newTrainLevel);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改培训级别
        /// </summary>
        /// <param name="trainLevel"></param>
        public static void UpdateTrainLevel(Model.Base_TrainLevel trainLevel)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_TrainLevel newTrainLevel = db.Base_TrainLevel.FirstOrDefault(e => e.TrainLevelId == trainLevel.TrainLevelId);
            if (newTrainLevel != null)
            {
                newTrainLevel.TrainLevelCode = trainLevel.TrainLevelCode;
                newTrainLevel.TrainLevelName = trainLevel.TrainLevelName;
                newTrainLevel.Remark = trainLevel.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除培训级别
        /// </summary>
        /// <param name="trainLevelId"></param>
        public static void DeleteTrainLevelById(string trainLevelId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_TrainLevel trainLevel = db.Base_TrainLevel.FirstOrDefault(e => e.TrainLevelId == trainLevelId);
            if (trainLevel != null)
            {
                db.Base_TrainLevel.DeleteOnSubmit(trainLevel);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取培训级别列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_TrainLevel> GetTrainLevelList()
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Base_TrainLevel orderby x.TrainLevelCode select x).ToList();
        }

        #region 培训级别下拉框
        /// <summary>
        /// 培训级别下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitTrainLevelDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "TrainLevelId";
            dropName.DataTextField = "TrainLevelName";
            dropName.DataSource = GetTrainLevelList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}
