using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class RoleItemService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 获取角色明细信息
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns>用户信息</returns>
        public static Model.Sys_RoleItem GeRoleItemByUserId(string userId)
        {
            return Funs.DB.Sys_RoleItem.FirstOrDefault(e => e.UserId == userId);
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="newitem">实体</param>
        public static void AddRoleItem(Model.Sys_RoleItem newitem)
        {
            Model.SGGLDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Sys_RoleItem));
            Model.Sys_RoleItem newUser = new Model.Sys_RoleItem
            {
                RoleItemId = newKeyID,
                RoleId = newitem.RoleId,
                ProjectId = newitem.ProjectId,
                UserId = newitem.UserId,
                IntoDate = newitem.IntoDate
            };
            db.Sys_RoleItem.InsertOnSubmit(newUser);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="user">实体</param>
        public static void UpdateRoleItem(Model.Sys_RoleItem NewRole)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Sys_RoleItem item = db.Sys_RoleItem.FirstOrDefault(e => e.UserId == NewRole.UserId);
            if (item != null)
            {
                item.RoleId = NewRole.RoleId;
                item.ProjectId = NewRole.ProjectId;
                item.OutDate = NewRole.OutDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据Id删除一个明细信息
        /// </summary>
        /// <param name="userId"></param>
        public static void DeleteRoleItem(string userId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Sys_RoleItem user = db.Sys_RoleItem.FirstOrDefault(e => e.UserId == userId);
            if (user != null)
            {
                db.Sys_RoleItem.DeleteOnSubmit(user);
                db.SubmitChanges();
            }
        }
    }
}
