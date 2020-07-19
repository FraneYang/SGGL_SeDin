using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 安全隐患明细
    /// </summary>
    public static class RectifyItemService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取安全隐患明细
        /// </summary>
        /// <param name="rectifyItemId"></param>
        /// <returns></returns>
        public static Model.Technique_RectifyItem GetRectifyItemById(string rectifyItemId)
        {
            return Funs.DB.Technique_RectifyItem.FirstOrDefault(e => e.RectifyItemId == rectifyItemId);
        }

        /// <summary>
        /// 根据整理人获取安全隐患明细
        /// </summary>
        /// <param name="compileMan"></param>
        /// <returns></returns>
        public static List<Model.Technique_RectifyItem> GetRectifyItemByCompileMan(string compileMan)
        {
            return (from x in Funs.DB.Technique_RectifyItem where x.CompileMan == compileMan select x).ToList();
        }

        /// <summary>
        /// 添加安全隐患明细
        /// </summary>
        /// <param name="rectifyItem"></param>
        public static void AddRectifyItem(Model.Technique_RectifyItem rectifyItem)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Technique_RectifyItem newRectifyItem = new Model.Technique_RectifyItem
            {
                RectifyItemId = rectifyItem.RectifyItemId,
                RectifyId = rectifyItem.RectifyId,
                HazardSourcePoint = rectifyItem.HazardSourcePoint,
                RiskAnalysis = rectifyItem.RiskAnalysis,
                RiskPrevention = rectifyItem.RiskPrevention,
                SimilarRisk = rectifyItem.SimilarRisk,
                CompileMan = rectifyItem.CompileMan,
                CompileDate = rectifyItem.CompileDate,
                IsPass = rectifyItem.IsPass,
                UnitId = rectifyItem.UnitId,
                UpState = rectifyItem.UpState
            };
            db.Technique_RectifyItem.InsertOnSubmit(newRectifyItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改安全隐患明细
        /// </summary>
        /// <param name="rectifyItem"></param>
        public static void UpdateRectifyItem(Model.Technique_RectifyItem rectifyItem)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Technique_RectifyItem newRectifyItem = db.Technique_RectifyItem.FirstOrDefault(e => e.RectifyItemId == rectifyItem.RectifyItemId);
            if (newRectifyItem != null)
            {
                newRectifyItem.HazardSourcePoint = rectifyItem.HazardSourcePoint;
                newRectifyItem.RiskAnalysis = rectifyItem.RiskAnalysis;
                newRectifyItem.RiskPrevention = rectifyItem.RiskPrevention;
                newRectifyItem.SimilarRisk = rectifyItem.SimilarRisk;
                newRectifyItem.UpState = rectifyItem.UpState;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 修改安全隐患明细 是否采用
        /// </summary>
        /// <param name="rectifyItem"></param>
        public static void UpdateRectifyItemIsPass(Model.Technique_RectifyItem rectifyItem)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Technique_RectifyItem newRectifyItem = db.Technique_RectifyItem.FirstOrDefault(e => e.RectifyItemId == rectifyItem.RectifyItemId);
            if (newRectifyItem != null)
            {
                newRectifyItem.AuditMan = rectifyItem.AuditMan;
                newRectifyItem.AuditDate = rectifyItem.AuditDate;
                newRectifyItem.IsPass = rectifyItem.IsPass;
                newRectifyItem.UpState = rectifyItem.UpState;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除安全隐患明细
        /// </summary>
        /// <param name="rectifyItemId"></param>
        public static void DeleteRectifyItem(string rectifyItemId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Technique_RectifyItem rectifyItem = db.Technique_RectifyItem.FirstOrDefault(e => e.RectifyItemId == rectifyItemId);
            if (rectifyItem != null)
            {
                db.Technique_RectifyItem.DeleteOnSubmit(rectifyItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据安全隐患主键删除所有相关明细信息
        /// </summary>
        /// <param name="rectifyId"></param>
        public static void DeleteRectifyItemByRectifyId(string rectifyId)
        {
            Model.SGGLDB db = Funs.DB;
            var q = (from x in db.Technique_RectifyItem where x.RectifyId == rectifyId select x).ToList();
            if (q != null)
            {
                db.Technique_RectifyItem.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}