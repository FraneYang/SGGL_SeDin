using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 安全技术交底
    /// </summary>
    public static class HSETechnicalService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取安全技术交底
        /// </summary>
        /// <param name="hSETechnicalId"></param>
        /// <returns></returns>
        public static Model.License_HSETechnical GetHSETechnicalById(string hSETechnicalId)
        {
            return Funs.DB.License_HSETechnical.FirstOrDefault(e => e.HSETechnicalId == hSETechnicalId);
        }

        /// <summary>
        /// 添加安全技术交底
        /// </summary>
        /// <param name="hseTechnical"></param>
        public static void AddHSETechnical(Model.License_HSETechnical hseTechnical)
        {
            Model.SGGLDB db = Funs.DB;
            Model.License_HSETechnical newHSETechnical = new Model.License_HSETechnical
            {
                HSETechnicalId = hseTechnical.HSETechnicalId,
                ProjectId = hseTechnical.ProjectId,
                HSETechnicalCode = hseTechnical.HSETechnicalCode,
                HSETechnicalDate = hseTechnical.HSETechnicalDate,
                UnitId = hseTechnical.UnitId,
                TeamGroupId = hseTechnical.TeamGroupId,
                WorkContents = hseTechnical.WorkContents,
                Address = hseTechnical.Address,
                CompileMan = hseTechnical.CompileMan,
                CompileDate = hseTechnical.CompileDate,
                TechnicalManId=hseTechnical.TechnicalManId,
                PartTechnicalManIds=hseTechnical.PartTechnicalManIds,
                PartTechnicalManNames=hseTechnical.PartTechnicalManNames,
                States = hseTechnical.States
            };
            db.License_HSETechnical.InsertOnSubmit(newHSETechnical);
            db.SubmitChanges();

            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectHSETechnicalMenuId, hseTechnical.ProjectId, null, hseTechnical.HSETechnicalId, hseTechnical.CompileDate);
        }

        /// <summary>
        /// 修改安全技术交底
        /// </summary>
        /// <param name="hSETechnical"></param>
        public static void UpdateHSETechnical(Model.License_HSETechnical hseTechnical)
        {
            Model.SGGLDB db = Funs.DB;
            Model.License_HSETechnical newHSETechnical = db.License_HSETechnical.FirstOrDefault(e => e.HSETechnicalId == hseTechnical.HSETechnicalId);
            if (newHSETechnical != null)
            {
                //newHSETechnical.ProjectId = hseTechnical.ProjectId;
                newHSETechnical.HSETechnicalCode = hseTechnical.HSETechnicalCode;
                newHSETechnical.HSETechnicalDate = hseTechnical.HSETechnicalDate;
                newHSETechnical.UnitId = hseTechnical.UnitId;
                newHSETechnical.TeamGroupId = hseTechnical.TeamGroupId;
                newHSETechnical.WorkContents = hseTechnical.WorkContents;
                newHSETechnical.Address = hseTechnical.Address;
                newHSETechnical.CompileMan = hseTechnical.CompileMan;
                newHSETechnical.CompileDate = hseTechnical.CompileDate;
                newHSETechnical.TechnicalManId = hseTechnical.TechnicalManId;
                newHSETechnical.PartTechnicalManIds = hseTechnical.PartTechnicalManIds;
                newHSETechnical.PartTechnicalManNames = hseTechnical.PartTechnicalManNames;
                newHSETechnical.States = hseTechnical.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除安全技术交底
        /// </summary>
        /// <param name="hseTechnicalId"></param>
        public static void DeleteHSETechnicalById(string hseTechnicalId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.License_HSETechnical hseTechnical = db.License_HSETechnical.FirstOrDefault(e => e.HSETechnicalId == hseTechnicalId);
            if (hseTechnical != null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(hseTechnicalId);
                CommonService.DeleteAttachFileById(hseTechnicalId);
                BLL.CommonService.DeleteFlowOperateByID(hseTechnicalId);  ////删除审核流程表
                db.License_HSETechnical.DeleteOnSubmit(hseTechnical);
                db.SubmitChanges();
            }
        }
    }
}
