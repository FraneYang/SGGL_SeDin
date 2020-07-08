using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public class TrainingItemService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trainingItemId"></param>
        /// <returns></returns>
        public static Model.Training_TrainingItem GetTrainingItemByTrainingItemId(string trainingItemId)
        {
            return new Model.SGGLDB(Funs.ConnString).Training_TrainingItem.FirstOrDefault(x => x.TrainingItemId == trainingItemId);
        }

        /// <summary>
        /// 根据整理人获取培训教材
        /// </summary>
        /// <param name="compileMan"></param>
        /// <returns></returns>
        public static List<Model.Training_TrainingItem> GetTrainingItemByCompileMan(string compileMan)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Training_TrainingItem where x.CompileMan == compileMan select x).ToList();
        }

        /// <summary>
        /// 增加教育培训内容信息
        /// </summary>
        /// <param name="trainingItem">教育培训内容实体</param>
        public static void AddTrainingItem(Model.Training_TrainingItem trainingItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Training_TrainingItem newTrainingItem = new Model.Training_TrainingItem
            {
                TrainingItemId = trainingItem.TrainingItemId,
                TrainingId = trainingItem.TrainingId,
                TrainingItemCode = trainingItem.TrainingItemCode,
                TrainingItemName = trainingItem.TrainingItemName,
                AttachUrl = trainingItem.AttachUrl,
                CompileMan = trainingItem.CompileMan,
                CompileDate = trainingItem.CompileDate,
                ApproveState = trainingItem.ApproveState,
                VersionNum = trainingItem.VersionNum,
                ResourcesFrom = trainingItem.ResourcesFrom,
                ResourcesFromType = trainingItem.ResourcesFromType,
                IsPass = trainingItem.IsPass,
                UnitId = trainingItem.UnitId,
                UpState = trainingItem.UpState
            };

            db.Training_TrainingItem.InsertOnSubmit(newTrainingItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改教育培训内容
        /// </summary>
        /// <param name="trainingItem">教育培训内容实体</param>
        public static void UpdateTrainingItem(Model.Training_TrainingItem trainingItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Training_TrainingItem newTrainingItem = db.Training_TrainingItem.FirstOrDefault(e => e.TrainingItemId == trainingItem.TrainingItemId);
            if (newTrainingItem != null)
            {
                newTrainingItem.TrainingId = trainingItem.TrainingId;
                newTrainingItem.TrainingItemCode = trainingItem.TrainingItemCode;
                newTrainingItem.TrainingItemName = trainingItem.TrainingItemName;
                newTrainingItem.AttachUrl = trainingItem.AttachUrl;
                //newTrainingItem.CompileMan = trainingItem.CompileMan;
                //newTrainingItem.CompileDate = trainingItem.CompileDate;
                newTrainingItem.ApproveState = trainingItem.ApproveState;
                newTrainingItem.VersionNum = trainingItem.VersionNum;
                newTrainingItem.ResourcesFrom = trainingItem.ResourcesFrom;
                newTrainingItem.ResourcesFromType = trainingItem.ResourcesFromType;
                newTrainingItem.UpState = trainingItem.UpState;

                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 修改培训教材  是否采用
        /// </summary>
        /// <param name="trainingItem"></param>
        public static void UpdateTrainingItemIsPass(Model.Training_TrainingItem trainingItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Training_TrainingItem newTrainingItem = db.Training_TrainingItem.FirstOrDefault(e => e.TrainingItemId == trainingItem.TrainingItemId);
            if (newTrainingItem != null)
            {
                newTrainingItem.AuditMan = trainingItem.AuditMan;
                newTrainingItem.AuditDate = trainingItem.AuditDate;
                newTrainingItem.IsPass = trainingItem.IsPass;
                newTrainingItem.UpState = trainingItem.UpState;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据教育培训项ID删除所有对应的教育培训内容实体
        /// </summary>
        /// <param name="trainingId">教育培训项ID</param>
        public static void DeleteTrainingItemsByTrainingId(string trainingId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var trainingItems = (from x in db.Training_TrainingItem where x.TrainingId == trainingId select x).ToList();
            if (trainingItems.Count() > 0)
            {
                foreach (var item in trainingItems)
                {
                    DeleteTrainingItemsByTrainingItemId(item.TrainingItemId);
                }
            }
        }

        /// <summary>
        /// 根据教育培训内容ID删除所有对应的教育培训内容实体
        /// </summary>
        /// <param name="trainingItemId">教育培训内容ID</param>
        public static void DeleteTrainingItemsByTrainingItemId(string trainingItemId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var trainingItem = db.Training_TrainingItem.FirstOrDefault(x => x.TrainingItemId == trainingItemId);
            if (trainingItem != null)
            {
                if (!string.IsNullOrEmpty(trainingItem.AttachUrl))
                {
                    BLL.UploadFileService.DeleteFile(Funs.RootPath, trainingItem.AttachUrl);
                }
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(trainingItem.TrainingItemId);

                db.Training_TrainingItem.DeleteOnSubmit(trainingItem);
                db.SubmitChanges();
            }
        }
    }
}
