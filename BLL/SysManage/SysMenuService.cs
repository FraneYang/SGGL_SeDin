using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace BLL
{
   public static class SysMenuService
    {
       public static Model.SGGLDB db = Funs.DB;

       /// <summary>
       /// 根据MenuId获取菜单名称项
       /// </summary>
       /// <param name="menuId"></param>
       /// <returns></returns>
       public static List<Model.Sys_Menu> GetSupMenuListBySuperMenu(string superMenu)
       {
           var list = (from x in Funs.DB.Sys_Menu where x.SuperMenu == superMenu orderby x.SortIndex select x).ToList();          
           return list;
       }

       /// <summary>
       /// 根据MenuId获取菜单名称项
       /// </summary>
       /// <param name="menuId"></param>
       /// <returns></returns>
       public static Model.Sys_Menu GetSupMenuBySuperMenu(string superMenu)
       {
           return Funs.DB.Sys_Menu.FirstOrDefault(x => x.SuperMenu == superMenu);    
       }

       /// <summary>
       /// 根据MenuId获取菜单名称项
       /// </summary>
       /// <param name="menuId"></param>
       /// <returns></returns>
       public static Model.Sys_Menu GetSysMenuByMenuId(string menuId)
       {
           return Funs.DB.Sys_Menu.FirstOrDefault(x => x.MenuId == menuId);
       }

       /// <summary>
       /// 根据MenuType获取菜单集合
       /// </summary>
       /// <param name="menuId"></param>
       /// <returns></returns>
       public static List<Model.Sys_Menu> GetMenuListByMenuType(string menuType)
       {
           var list = (from x in Funs.DB.Sys_Menu where x.MenuType == menuType orderby x.SortIndex select x).ToList();
           return list;
       }

        /// <summary>
        /// 根据MenuType获取菜单集合
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public static List<Model.Sys_Menu> GetIsUsedMenuListByMenuType(string menuType)
        {
            var list = (from x in Funs.DB.Sys_Menu
                        where (x.MenuType == menuType || menuType == null) && x.IsUsed == true
                        orderby x.SortIndex
                        select x).Distinct().ToList();
            return list;
        }

        /// <summary>
        /// 根据MenuType获取菜单集合
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public static List<Model.Sys_Menu> GetIsUsedMenuListBySupType(string menuType)
        {
            List<Model.Sys_Menu> lists = new List<Model.Sys_Menu>();

            if (menuType == "MenuType_S")
            {
                lists = (from x in Funs.DB.Sys_Menu
                         where x.IsOffice == true && x.IsUsed == true
                         orderby x.SortIndex
                         select x).Distinct().ToList();
            }
            else
            {
                lists = (from x in Funs.DB.Sys_Menu
                         where x.IsOffice == false && x.IsUsed == true
                         orderby x.SortIndex
                         select x).Distinct().ToList();
            }
            
            return lists;
        }
    }
}
