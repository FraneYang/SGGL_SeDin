using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 安全管理机构
    /// </summary>
    public static class SafetyOrganizationService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取安全管理机构信息
        /// </summary>
        /// <param name="SafetyOrganizationId"></param>
        /// <returns></returns>
        public static Model.SecuritySystem_SafetyOrganization GetSafetyOrganizationById(string SafetyOrganizationId)
        {
            return new Model.SGGLDB(Funs.ConnString).SecuritySystem_SafetyOrganization.FirstOrDefault(e => e.SafetyOrganizationId == SafetyOrganizationId);
        }

        /// <summary>
        /// 添加安全管理机构
        /// </summary>
        /// <param name="item"></param>
        public static void AddSafetyOrganization(Model.SecuritySystem_SafetyOrganization item)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.SecuritySystem_SafetyOrganization newItem = new Model.SecuritySystem_SafetyOrganization
            {
                SafetyOrganizationId = item.SafetyOrganizationId,
                ProjectId = item.ProjectId,
                UnitId = item.UnitId,
                Post = item.Post,
                Names = item.Names,
                Telephone = item.Telephone,
                MobilePhone = item.MobilePhone,
                EMail = item.EMail,
                Duty = item.Duty,
                SortIndex = item.SortIndex
            };
            db.SecuritySystem_SafetyOrganization.InsertOnSubmit(newItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="item"></param>
        public static void UpdateSafetyOrganization(Model.SecuritySystem_SafetyOrganization item)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.SecuritySystem_SafetyOrganization newItem = db.SecuritySystem_SafetyOrganization.FirstOrDefault(e => e.SafetyOrganizationId == item.SafetyOrganizationId);
            if (newItem != null)
            {
                newItem.SafetyOrganizationId = item.SafetyOrganizationId;
                newItem.Post = item.Post;
                newItem.Names = item.Names;
                newItem.Telephone = item.Telephone;
                newItem.MobilePhone = item.MobilePhone;
                newItem.EMail = item.EMail;
                newItem.Duty = item.Duty;
                newItem.SortIndex = item.SortIndex;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="SafetyOrganizationId"></param>
        public static void DeleteSafetyOrganization(string SafetyOrganizationId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.SecuritySystem_SafetyOrganization item = db.SecuritySystem_SafetyOrganization.FirstOrDefault(e => e.SafetyOrganizationId == SafetyOrganizationId);
            if (item != null)
            {
                db.SecuritySystem_SafetyOrganization.DeleteOnSubmit(item);
                db.SubmitChanges();
            }
        }
    }
}