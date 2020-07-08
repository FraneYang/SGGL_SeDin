using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public static class ButtonToMenuService
    {      

       /// <summary>
       /// 根据menuId获取按钮权限信息
       /// </summary>
       /// <param name="menuId"></param>
       /// <returns></returns>
        public static List<Model.Sys_ButtonToMenu> GetButtonToMenuListByMenuId(string menuId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Sys_ButtonToMenu where x.MenuId == menuId orderby x.SortIndex select x).ToList();
        }

        /// <summary>
        /// 根据buttonName获取按钮权限信息
        /// </summary>
        /// <param name="buttonName"></param>
        /// <returns></returns>
        public static Model.Sys_ButtonToMenu GetButtonToMenuByButtonName(string menuId, string buttonName)
        {
            return new Model.SGGLDB(Funs.ConnString).Sys_ButtonToMenu.FirstOrDefault(e => e.ButtonName == buttonName && e.MenuId == menuId);
        }
    }
}
