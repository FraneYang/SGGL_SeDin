using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public class AccidentCaseItemService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据常见事故案例明细Id获取事故案例明细
        /// </summary>
        /// <param name="accidentCaseItemId"></param>
        /// <returns></returns>
        public static Model.EduTrain_AccidentCaseItem GetAccidentCaseItemById(string accidentCaseItemId)
        {
            return Funs.DB.EduTrain_AccidentCaseItem.FirstOrDefault(e => e.AccidentCaseItemId == accidentCaseItemId);
        }

        /// <summary>
        /// 根据整理人获取事故案例明细
        /// </summary>
        /// <param name="compileMan"></param>
        /// <returns></returns>
        public static List<Model.EduTrain_AccidentCaseItem> GetAccidentCaseItemByCompileMan(string compileMan)
        {
            return (from x in Funs.DB.EduTrain_AccidentCaseItem where x.CompileMan == compileMan select x).ToList();
        }

        /// <summary>
        /// 增加常见事故案例明细信息
        /// </summary>
        /// <param name="item"></param>
        public static void AddAccidentCaseItem(Model.EduTrain_AccidentCaseItem item)
        {
            Model.SGGLDB db = Funs.DB;
            Model.EduTrain_AccidentCaseItem newItem = new Model.EduTrain_AccidentCaseItem
            {
                AccidentCaseItemId = item.AccidentCaseItemId,
                AccidentCaseId = item.AccidentCaseId,
                Activities = item.Activities,
                AccidentName = item.AccidentName,
                AccidentProfiles = item.AccidentProfiles,
                AccidentReview = item.AccidentReview,
                CompileMan = item.CompileMan,
                CompileDate = item.CompileDate,
                IsPass = item.IsPass,
                UnitId = item.UnitId,
                UpState = item.UpState
            };
            db.EduTrain_AccidentCaseItem.InsertOnSubmit(newItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改常见事故案例明细信息
        /// </summary>
        /// <param name="item"></param>
        public static void UpdateAccidentCaseItem(Model.EduTrain_AccidentCaseItem item)
        {
            Model.SGGLDB db = Funs.DB;
            Model.EduTrain_AccidentCaseItem newItem = db.EduTrain_AccidentCaseItem.FirstOrDefault(e => e.AccidentCaseItemId == item.AccidentCaseItemId);
            if (newItem != null)
            {
                newItem.Activities = item.Activities;
                newItem.AccidentName = item.AccidentName;
                newItem.AccidentProfiles = item.AccidentProfiles;
                newItem.AccidentReview = item.AccidentReview;
                newItem.UpState = item.UpState;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 修改常见事故案例明细
        /// </summary>
        /// <param name="item"></param>
        public static void UpdateAccidentCaseItemIsPass(Model.EduTrain_AccidentCaseItem item)
        {
            Model.SGGLDB db = Funs.DB;
            Model.EduTrain_AccidentCaseItem newItem = db.EduTrain_AccidentCaseItem.FirstOrDefault(e => e.AccidentCaseItemId == item.AccidentCaseItemId);
            if (newItem != null)
            {
                newItem.AuditMan = item.AuditMan;
                newItem.AuditDate = item.AuditDate;
                newItem.IsPass = item.IsPass;
                newItem.UpState = item.UpState;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据事故案例ID删除所有对应的事故案例明细实体
        /// </summary>
        /// <param name="trainingId">教育培训项ID</param>
        public static void DeleteAccidentCaseItemsByAccidentCaseId(string accidentCaseId)
        {
            Model.SGGLDB db = Funs.DB;
            var accidentCaseItems = (from x in db.EduTrain_AccidentCaseItem where x.AccidentCaseId == accidentCaseId select x).ToList();
            if (accidentCaseItems != null)
            {
                db.EduTrain_AccidentCaseItem.DeleteAllOnSubmit(accidentCaseItems);
            }
        }

        /// <summary>
        /// 根据主键删除常见事故案例明细信息
        /// </summary>
        /// <param name="accidentCaseItemId">常见事故案例明细主键</param>
        public static void DeleteAccidentCaseItemId(string accidentCaseItemId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.EduTrain_AccidentCaseItem item = db.EduTrain_AccidentCaseItem.FirstOrDefault(e => e.AccidentCaseItemId == accidentCaseItemId);
            if (item != null)
            {
                db.EduTrain_AccidentCaseItem.DeleteOnSubmit(item);
                db.SubmitChanges();
            }
        }
    }
}