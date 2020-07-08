using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 安全试题库明细表
    /// </summary>
    public static class TrainTestDBItemService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键删除安全试题库明细信息
        /// </summary>
        /// <param name="trainTestDBItemId"></param>
        /// <returns></returns>
        public static Model.Training_TrainTestDBItem GetTrainTestDBItemById(string trainTestItemId)
        {
            return new Model.SGGLDB(Funs.ConnString).Training_TrainTestDBItem.FirstOrDefault(e => e.TrainTestItemId == trainTestItemId);
        }

        /// <summary>
        /// 根据整理人获取安全试题库明细
        /// </summary>
        /// <param name="compile"></param>
        /// <returns></returns>
        public static List<Model.Training_TrainTestDBItem> GetTrainTestDBItemByCompile(string compile)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Training_TrainTestDBItem where x.CompileMan == compile select x).ToList();
        }

        /// <summary>
        /// 添加安全试题库明细信息
        /// </summary>
        /// <param name="trainTestDBItem"></param>
        public static void AddTrainTestDBItem(Model.Training_TrainTestDBItem trainTestDBItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Training_TrainTestDBItem newTrainTestDBItem = new Model.Training_TrainTestDBItem
            {
                TrainTestItemId = trainTestDBItem.TrainTestItemId,
                TrainTestId = trainTestDBItem.TrainTestId,
                TrainTestItemCode = trainTestDBItem.TrainTestItemCode,
                TraiinTestItemName = trainTestDBItem.TraiinTestItemName,
                AttachUrl = trainTestDBItem.AttachUrl,
                CompileMan = trainTestDBItem.CompileMan,
                CompileDate = trainTestDBItem.CompileDate,
                IsPass = trainTestDBItem.IsPass,
                UnitId = trainTestDBItem.UnitId,
                UpState = trainTestDBItem.UpState
            };
            db.Training_TrainTestDBItem.InsertOnSubmit(newTrainTestDBItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改安全试题库明细信息
        /// </summary>
        /// <param name="trainTestDBItem"></param>
        public static void UpdateTrainTestDBItem(Model.Training_TrainTestDBItem trainTestDBItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Training_TrainTestDBItem newTrainTestDBItem = db.Training_TrainTestDBItem.FirstOrDefault(e => e.TrainTestItemId == trainTestDBItem.TrainTestItemId);
            if (newTrainTestDBItem != null)
            {
                newTrainTestDBItem.TrainTestId = trainTestDBItem.TrainTestId;
                newTrainTestDBItem.TrainTestItemCode = trainTestDBItem.TrainTestItemCode;
                newTrainTestDBItem.TraiinTestItemName = trainTestDBItem.TraiinTestItemName;
                newTrainTestDBItem.AttachUrl = trainTestDBItem.AttachUrl;
                newTrainTestDBItem.UpState = trainTestDBItem.UpState;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 修改安全试题库  是否采用
        /// </summary>
        /// <param name="trainTestDBItem"></param>
        public static void UpdateTrainTestDBItemIsPass(Model.Training_TrainTestDBItem trainTestDBItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Training_TrainTestDBItem newTrainTestDBItem = db.Training_TrainTestDBItem.FirstOrDefault(e => e.TrainTestItemId == trainTestDBItem.TrainTestItemId);
            if (newTrainTestDBItem != null)
            {
                newTrainTestDBItem.AuditMan = trainTestDBItem.AuditMan;
                newTrainTestDBItem.AuditDate = trainTestDBItem.AuditDate;
                newTrainTestDBItem.IsPass = trainTestDBItem.IsPass;
                newTrainTestDBItem.UpState = trainTestDBItem.UpState;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除安全试题库明细信息
        /// </summary>
        /// <param name="trainTestItemId"></param>
        public static void DeleteTrainTestDBItemById(string trainTestItemId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Training_TrainTestDBItem trainTestDBItem = db.Training_TrainTestDBItem.FirstOrDefault(e => e.TrainTestItemId == trainTestItemId);
            if (trainTestDBItem != null)
            {
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(trainTestDBItem.TrainTestItemId);
                db.Training_TrainTestDBItem.DeleteOnSubmit(trainTestDBItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据安全试题库主键删除所有相关明细信息
        /// </summary>
        /// <param name="trainTestId"></param>
        public static void DeleteTrainTestDBItemList(string trainTestId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var q = (from x in db.Training_TrainTestDBItem where x.TrainTestId == trainTestId select x).ToList();
            if (q != null)
            {
                db.Training_TrainTestDBItem.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
