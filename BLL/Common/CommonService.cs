using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class CommonService
    {
        #region 获取当前人菜单集合
        /// <summary>
        ///  获取当前人菜单集合
        /// </summary> 
        /// <param name="projectId">项目ID</param>    
        /// <param name="userId">用户id</param>
        /// <returns>是否具有权限</returns>
        public static List<string> GetAllMenuList(string projectId, string userId)
        {
            Model.SGGLDB db = Funs.DB;
            List<Model.Sys_Menu> menus = new List<Model.Sys_Menu>();
            /// 启用且末级菜单
            var getMenus = from x in db.Sys_Menu
                           where x.IsUsed == true && x.IsEnd == true
                           select x;
            if (!string.IsNullOrEmpty(projectId))
            {
                getMenus = getMenus.Where(x => x.IsOffice == false);
            }

            if (userId == Const.sysglyId || userId == Const.hfnbdId || userId == Const.sedinId)
            {
                menus = getMenus.ToList();
            }
            else
            {
                if (string.IsNullOrEmpty(projectId))
                {
                    var user = UserService.GetUserByUserId(userId); ////用户            
                    if (user != null)
                    {
                        menus = (from x in getMenus
                                 join y in db.Sys_RolePower on x.MenuId equals y.MenuId
                                 where y.RoleId == user.RoleId
                                 select x).ToList();
                    }
                }
                else
                {
                    var pUser = ProjectUserService.GetProjectUserByUserIdProjectId(projectId, userId); ///项目用户
                    if (pUser != null)
                    {
                        List<string> roleIdList = Funs.GetStrListByStr(pUser.RoleId, ',');
                        menus = (from x in db.Sys_RolePower
                                 join y in getMenus on x.MenuId equals y.MenuId
                                 where roleIdList.Contains(x.RoleId)
                                 select y).ToList();
                    }
                }
            }

            return menus.Select(x => x.MenuId).ToList();
        }
        #endregion

        #region 根据登陆id菜单id判断是否有权限
        /// <summary>
        /// 根据登陆id菜单id判断是否有权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool ReturnMenuByUserIdMenuId(string userId, string menuId, string projectId)
        {
            bool returnValue = false;
            var menu = Funs.DB.Sys_Menu.FirstOrDefault(x => x.MenuId == menuId);
            if (menu != null)
            {
                ///1、当前用户是管理员 
                ///2、当前菜单是个人设置 资源库|| menu.MenuType == BLL.Const.Menu_Resource
                if (userId == Const.sysglyId || userId == Const.hfnbdId || userId == Const.sedinId || menu.MenuType == Const.Menu_Personal)
                {
                    returnValue = true;
                }
                else if (string.IsNullOrEmpty(projectId)) ///本部、系统设置
                {
                    var user =Funs.DB.Sys_User.FirstOrDefault(x=>x.UserId ==userId); ////用户
                    if (user != null && !string.IsNullOrEmpty(user.RoleId))
                    {
                        var power = Funs.DB.Sys_RolePower.FirstOrDefault(x => x.MenuId == menuId && x.RoleId == user.RoleId);
                        if (power != null)
                        {
                            returnValue = true;
                        }

                        // var getRoles=Funs.DB.Sys_Role.FirstOrDefault(x=> user.RoleId)
                    }
                }
                else
                {
                    ///3、管理角色、领导角色能访问项目菜单
                    var puser = ProjectUserService.GetProjectUserByUserIdProjectId(projectId, userId); ////用户
                    if (puser != null && !string.IsNullOrEmpty(puser.RoleId))
                    {
                        List<string> roleIdList = Funs.GetStrListByStr(puser.RoleId, ',');
                        var power = Funs.DB.Sys_RolePower.FirstOrDefault(x => x.MenuId == menuId && roleIdList.Contains(x.RoleId));
                        if (power != null)
                        {
                            returnValue = true;
                        }
                    }
                }
            }
            return returnValue;
        }
        #endregion

        #region 获取当前人按钮集合
        /// <summary>
        ///  获取当前人按钮集合
        /// </summary>        
        /// <param name="userId">用户id</param>
        /// <param name="menuId">按钮id</param>    
        /// <returns>是否具有权限</returns>
        public static List<string> GetAllButtonList(string projectId, string userId, string menuId)
        {
            Model.SGGLDB db = Funs.DB;
            List<string> buttonList = new List<string>();
            List<Model.Sys_ButtonToMenu> buttons = new List<Model.Sys_ButtonToMenu>();
            if (userId == Const.sedinId)
            {
                return buttonList;
            }
            if (userId == Const.sysglyId || userId == Const.hfnbdId)
            {
                buttons = (from x in db.Sys_ButtonToMenu
                           where x.MenuId == menuId
                           select x).ToList();
            }
            else
            {
                if (string.IsNullOrEmpty(projectId))
                {
                    var user = BLL.UserService.GetUserByUserId(userId); ////用户            
                    if (user != null)
                    {
                        buttons = (from x in db.Sys_ButtonToMenu
                                   join y in db.Sys_ButtonPower on x.ButtonToMenuId equals y.ButtonToMenuId
                                   where y.RoleId == user.RoleId && y.MenuId == menuId && x.MenuId == menuId
                                   select x).ToList();
                    }
                }
                else
                {
                    var pUser = BLL.ProjectUserService.GetProjectUserByUserIdProjectId(projectId, userId); ///项目用户
                    if (pUser != null)
                    {
                        List<string> roleIdList = Funs.GetStrListByStr(pUser.RoleId, ',');
                        buttons = (from x in db.Sys_ButtonToMenu
                                   join y in db.Sys_ButtonPower on x.ButtonToMenuId equals y.ButtonToMenuId
                                   where roleIdList.Contains(y.RoleId) && y.MenuId == menuId && x.MenuId == menuId
                                   select x).ToList();
                    }
                }
            }

            if (buttons.Count() > 0)
            {
                buttonList = buttons.Select(x => x.ButtonName).ToList();
            }

            if (!String.IsNullOrEmpty(projectId) && menuId != BLL.Const.ProjectShutdownMenuId)
            {
                var porject = BLL.ProjectService.GetProjectByProjectId(projectId);
                if (porject != null && (porject.ProjectState == BLL.Const.ProjectState_2 || porject.ProjectState == BLL.Const.ProjectState_3))
                {
                    buttonList.Clear();
                }
            }
            return buttonList;
        }
        #endregion

        #region 获取当前人是否具有按钮操作权限
        /// <summary>
        /// 获取当前人是否具有按钮操作权限
        /// </summary>        
        /// <param name="userId">用户id</param>
        /// <param name="menuId">按钮id</param>
        /// <param name="buttonName">按钮名称</param>
        /// <returns>是否具有权限</returns>
        public static bool GetAllButtonPowerList(string projectId, string userId, string menuId, string buttonName)
        {
            Model.SGGLDB db = Funs.DB;
            bool isPower = false;    ////定义是否具备按钮权限    
            if (userId == Const.sedinId)
            {
                return isPower;
            }
            if (!isPower && (userId == Const.sysglyId || userId == Const.hfnbdId))
            {
                isPower = true;
            }
            // 根据角色判断是否有按钮权限
            if (!isPower)
            {
                if (string.IsNullOrEmpty(projectId))
                {
                    var user = UserService.GetUserByUserId(userId); ////用户            
                    if (user != null)
                    {
                        if (!string.IsNullOrEmpty(user.RoleId))
                        {
                            var buttonToMenu = from x in db.Sys_ButtonToMenu
                                               join y in db.Sys_ButtonPower on x.ButtonToMenuId equals y.ButtonToMenuId
                                               join z in db.Sys_Menu on x.MenuId equals z.MenuId
                                               where y.RoleId == user.RoleId && y.MenuId == menuId
                                               && x.ButtonName == buttonName && x.MenuId == menuId
                                               select x;
                            if (buttonToMenu.Count() > 0)
                            {
                                isPower = true;
                            }
                        }
                    }
                }
                else
                {
                    var pUser = BLL.ProjectUserService.GetProjectUserByUserIdProjectId(projectId, userId); ///项目用户
                    if (pUser != null)
                    {
                        if (!string.IsNullOrEmpty(pUser.RoleId))
                        {
                            List<string> roleIdList = Funs.GetStrListByStr(pUser.RoleId, ',');
                            var buttonToMenu = from x in db.Sys_ButtonToMenu
                                               join y in db.Sys_ButtonPower on x.ButtonToMenuId equals y.ButtonToMenuId
                                               join z in db.Sys_Menu on x.MenuId equals z.MenuId
                                               where roleIdList.Contains(y.RoleId) && y.MenuId == menuId
                                               && x.ButtonName == buttonName && x.MenuId == menuId
                                               select x;
                            if (buttonToMenu.Count() > 0)
                            {
                                isPower = true;
                            }
                        }
                    }
                }
            }

            if (isPower && !String.IsNullOrEmpty(projectId) && menuId != BLL.Const.ProjectShutdownMenuId)
            {
                var porject = BLL.ProjectService.GetProjectByProjectId(projectId);
                if (porject != null && (porject.ProjectState == BLL.Const.ProjectState_2 || porject.ProjectState == BLL.Const.ProjectState_3))
                {
                    isPower = false;
                }
            }
            
            return isPower;
        }
        #endregion

        #region 根据用户Id判断是否为本单位用户或管理员
        /// <summary>
        /// 根据用户UnitId判断是否为本单位用户或管理员
        /// </summary>
        /// <returns></returns>
        public static bool IsMainUnitOrAdmin(string userId)
        {
            bool result = false;
            if (userId == Const.sysglyId || userId == Const.hfnbdId)
            {
                result = true;
            }
            else
            {
                var user = UserService.GetUserByUserId(userId);
                if (user != null && user.UnitId == Const.UnitId_SEDIN)
                {
                    result = true;
                }
            }
            return result;
        }
        #endregion

        #region 根据用户ID判断是否 本单位本部用户或管理员
        /// <summary>
        /// 根据用户UnitId判断是否为本单位用户或管理员
        /// </summary>
        /// <returns></returns>
        public static bool IsThisUnitLeaderOfficeOrManage(string userId)
        {
            bool result = false;
            if (userId == Const.sysglyId || userId == Const.hfnbdId || userId == Const.sedinId)
            {
                result = true;
            }
            else
            {             
                var user = BLL.UserService.GetUserByUserId(userId);
                if (user != null && user.IsOffice == true)
                {
                    result = true;
                }               
            }

            return result;
        }
        #endregion

        /// <summary>
        ///根据主键删除附件
        /// </summary>
        /// <param name="lawRegulationId"></param>
        public static void DeleteAttachFileById(string id)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.AttachFile attachFile = db.AttachFile.FirstOrDefault(e => e.ToKeyId == id);
                if (attachFile != null)
                {
                    if (!string.IsNullOrEmpty(attachFile.AttachUrl))
                    {
                        UploadFileService.DeleteFile(Funs.RootPath, attachFile.AttachUrl);
                    }

                    db.AttachFile.DeleteOnSubmit(attachFile);
                    db.SubmitChanges();
                }
            }
        }

        /// <summary>
        ///根据主键删除附件
        /// </summary>
        /// <param name="lawRegulationId"></param>
        public static void DeleteAttachFileById(string menuId, string id)
        {
            Model.SGGLDB db = Funs.DB;
            Model.AttachFile attachFile = db.AttachFile.FirstOrDefault(e => e.MenuId == menuId && e.ToKeyId == id);
            if (attachFile != null)
            {
                if (!string.IsNullOrEmpty(attachFile.AttachUrl))
                {
                    BLL.UploadFileService.DeleteFile(Funs.RootPath, attachFile.AttachUrl);
                }

                db.AttachFile.DeleteOnSubmit(attachFile);
                db.SubmitChanges();
            }
        }

        /// <summary>
        ///根据主键删除流程
        /// </summary>
        /// <param name="lawRegulationId"></param>
        public static void DeleteFlowOperateByID(string id)
        {
            var flowOperateList = from x in Funs.DB.Sys_FlowOperate where x.DataId == id select x;
            if (flowOperateList.Count() > 0)
            {
                Funs.DB.Sys_FlowOperate.DeleteAllOnSubmit(flowOperateList);
                Funs.DB.SubmitChanges();
            }
        }
    }
}
