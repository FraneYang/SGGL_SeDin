using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class ButtonPowerService
    {
        /// <summary>
        /// 获取按钮权限集合
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="menuId">菜单ID</param>
        /// <returns>按钮集合</returns>
        public static string[] GetButtonPowerList(string roleId, string menuId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var q = from x in db.Sys_ButtonPower where x.RoleId == roleId && x.MenuId == menuId select x;
            string[] button = new string[q.Count()];
            if (q.Count() > 0)
            {                
                int i = 0;
                foreach (var b in q)
                {
                    Model.Sys_ButtonToMenu btn = db.Sys_ButtonToMenu.FirstOrDefault(e => e.ButtonToMenuId == b.ButtonToMenuId);
                    if (btn != null)
                    {
                        button[i] = btn.ButtonName;
                    }
                    i++;
                }               
            }

            return button;
        }

        /// <summary>
        /// 判断按键对应菜单是否存在
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public static bool isExistButtonToMenu(string buttonToMenuId)
        {
            Model.Sys_ButtonToMenu b = new Model.SGGLDB(Funs.ConnString).Sys_ButtonToMenu.FirstOrDefault(e => e.ButtonToMenuId == buttonToMenuId);
            if (b != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除按钮权限
        /// </summary>
        /// <param name="roleId"></param>
        public static void DeleteButtonPower(string roleId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var bt = from x in db.Sys_ButtonPower where x.RoleId == roleId select x;
            if (bt.Count() > 0)
            {
                db.Sys_ButtonPower.DeleteAllOnSubmit(bt);
                db.SubmitChanges();
            }
        }
        
        /// <summary>
        /// 删除按钮权限
        /// </summary>
        /// <param name="roleId"></param>
        public static void DeleteButtonPowerByRoleIdMenuType(string roleId, string menuType)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var bt = from x in db.Sys_ButtonPower
                     join y in db.Sys_Menu on x.MenuId equals y.MenuId
                     where x.RoleId == roleId && y.MenuType == menuType
                     select x;
            if (bt.Count() > 0)
            {
                db.Sys_ButtonPower.DeleteAllOnSubmit(bt);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="roleId"></param>
        public static void DeleteButtonPowerByMenuId(string menuId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var bt = from x in db.Sys_ButtonPower where x.MenuId == menuId select x;
            if (bt.Count() > 0)
            {
                db.Sys_ButtonPower.DeleteAllOnSubmit(bt);
                db.SubmitChanges();
            }
        }
        
        /// <summary>
        /// 增加按钮权限
        /// </summary>
        /// <param name="power"></param>
        public static void SaveButtonPower(Model.Sys_ButtonPower btn)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Sys_ButtonPower));
            Model.Sys_ButtonPower button = new Model.Sys_ButtonPower
            {
                ButtonPowerID = newKeyID,
                RoleId = btn.RoleId,
                MenuId = btn.MenuId,
                ButtonToMenuId = btn.ButtonToMenuId
            };

            db.Sys_ButtonPower.InsertOnSubmit(button);
            db.SubmitChanges();
        }
    }
}
