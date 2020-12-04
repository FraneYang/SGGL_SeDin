using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace BLL
{
    public static class CommonService
    {
        #region 获取当前人系统集合
        /// <summary>
        ///  获取当前人系统集合
        /// </summary> 
        /// <param name="userId">用户id</param>
        /// <returns>是否具有权限</returns>
        public static List<string> GetSystemPowerList(string userId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getUser = db.Sys_User.FirstOrDefault(x => x.UserId == userId);
                if (getUser != null)
                {
                    if (userId == Const.sysglyId || userId == Const.hfnbdId)  ////|| getUser.DepartId == Const.Depart_constructionId
                    {
                        return new List<string>() { Const.Menu_Server, Const.Menu_HSSE, Const.Menu_CQMS, Const.Menu_HJGL };
                    }
                    else if (userId == Const.sedinId)
                    {
                        return new List<string>() { Const.Menu_CQMS };
                    }
                    else
                    {
                        List<string> returnList = new List<string>();
                        string rolesStr = string.Empty;
                        if (!string.IsNullOrEmpty(getUser.RoleId))
                        {
                            rolesStr = getUser.RoleId;
                            var getOffice = db.Sys_RolePower.FirstOrDefault(x => x.RoleId == getUser.RoleId && x.IsOffice == true);
                            if (getOffice != null)
                            {
                                returnList.Add(Const.Menu_Server);
                            }
                        }
                        ////获取项目角色的集合
                        var getPRoles = (from x in db.Project_ProjectUser
                                         join y in db.Base_Project on x.ProjectId equals y.ProjectId
                                         where (y.ProjectState == Const.ProjectState_1 || y.ProjectState == null) && x.UserId == userId && x.RoleId != null
                                         select x.RoleId).ToList();
                        foreach (var item in getPRoles)
                        {
                            if (string.IsNullOrEmpty(rolesStr))
                            {
                                rolesStr = item;
                            }
                            else
                            {
                                if (!rolesStr.Contains(item))
                                {
                                    rolesStr += "," + item;
                                }
                            }
                        }
                        ////项目角色集合list
                        List<string> roleIdList = Funs.GetStrListByStr(rolesStr, ',').Distinct().ToList();
                        var getProjectRolePowers = (from x in db.Sys_RolePower
                                                    where roleIdList.Contains(x.RoleId)
                                                    select x).ToList();
                        if (getProjectRolePowers.FirstOrDefault(x => x.MenuType == Const.Menu_HSSE) != null)
                        {
                            returnList.Add(Const.Menu_HSSE);
                        }
                        if (getProjectRolePowers.FirstOrDefault(x => x.MenuType == Const.Menu_CQMS) != null)
                        {
                            returnList.Add(Const.Menu_CQMS);
                        }
                        if (getProjectRolePowers.FirstOrDefault(x => x.MenuType == Const.Menu_HJGL) != null)
                        {
                            returnList.Add(Const.Menu_HJGL);
                        }

                        return returnList.Distinct().ToList();
                    }
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        #region 获取当前人菜单集合
        /// <summary>
        ///  获取当前人菜单集合
        /// </summary> 
        /// <param name="projectId">项目ID</param>    
        /// <param name="userId">用户id</param>
        /// <returns>是否具有权限</returns>
        public static List<string> GetAllMenuList(string projectId, string userId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                /// 启用且末级菜单
                var getMenus = from x in db.Sys_Menu
                               where x.IsUsed == true 
                               && (x.MenuType == Const.Menu_Server || x.MenuType == Const.Menu_Person ||
                               x.MenuType == Const.Menu_HSSE || x.MenuType==Const.Menu_CQMS || x.MenuType == Const.Menu_HJGL 
                               || x.MenuType == Const.Menu_PZHGL)
                               select x;            
                List<Model.Sys_Menu> menus = new List<Model.Sys_Menu>();
                if (userId == Const.sysglyId || userId == Const.hfnbdId || userId == Const.sedinId)
                {
                    menus = getMenus.ToList();
                }              
                else
                {
                    var getUser = UserService.GetUserByUserId(userId); ////用户            
                    if (getUser != null)
                    {
                        List<string> roleIdList = UserService.GetRoleListByProjectIdUserId(projectId, userId);
                        menus = (from x in db.Sys_RolePower
                                 join y in getMenus on x.MenuId equals y.MenuId
                                 where roleIdList.Contains(x.RoleId)
                                 select y).ToList();
                    }
                }

                return menus.Select(x => x.MenuId).Distinct().ToList();
            }
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
            var getUser = Funs.DB.Sys_User.FirstOrDefault(x => x.UserId == userId); ////用户
            if (menu != null && getUser != null)
            {
                ///1、当前用户是管理员 
                ///2、当前菜单是个人设置 
                if (userId == Const.sysglyId || userId == Const.hfnbdId || userId == Const.sedinId)
                {
                    returnValue = true;
                }
                else if (userId == Const.sedinId)
                {
                    returnValue = true;
                }
                else if (menu.MenuType == Const.Menu_Personal)
                {
                    returnValue = true;
                }
                else if (string.IsNullOrEmpty(projectId)) ///本部、系统设置
                {
                    if (!string.IsNullOrEmpty(getUser.RoleId))
                    {
                        string[] roleList = getUser.RoleId.Split(',');
                        var power = Funs.DB.Sys_RolePower.FirstOrDefault(x => x.MenuId == menuId && roleList.Contains(x.RoleId));
                        if (power != null)
                        {
                            returnValue = true;
                        }
                    }
                }
                else
                {
                    List<string> roleIdList = UserService.GetRoleListByProjectIdUserId(projectId, userId);
                    var power = Funs.DB.Sys_RolePower.FirstOrDefault(x => x.MenuId == menuId && roleIdList.Contains(x.RoleId));
                    if (power != null)
                    {
                        returnValue = true;
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

            var getMenu = Funs.DB.Sys_Menu.FirstOrDefault(x => x.MenuId == menuId);
            var user = BLL.UserService.GetUserByUserId(userId); ////用户            
            if (getMenu != null && user != null)
            {
                if (userId == Const.sysglyId || userId == Const.hfnbdId || getMenu.MenuType == Const.Menu_Personal)
                {
                    buttons = (from x in db.Sys_ButtonToMenu
                               where x.MenuId == menuId
                               select x).ToList();
                }
                else
                {
                    if (string.IsNullOrEmpty(projectId))
                    {
                        if (!string.IsNullOrEmpty(user.RoleId))
                        {
                            string[] roleList = user.RoleId.Split(',');
                            buttons = (from x in db.Sys_ButtonToMenu
                                       join y in db.Sys_ButtonPower on x.ButtonToMenuId equals y.ButtonToMenuId
                                       where roleList.Contains(y.RoleId) && y.MenuId == menuId && x.MenuId == menuId
                                       select x).ToList();
                        }
                    }
                    else
                    {
                        List<string> roleIdList = UserService.GetRoleListByProjectIdUserId(projectId, userId);
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
                var user = UserService.GetUserByUserId(userId); ////用户            
                if (user != null)
                {
                    if (string.IsNullOrEmpty(projectId))
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
                    else
                    {
                        List<string> roleIdList = UserService.GetRoleListByProjectIdUserId(projectId, userId);
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

        #region 根据当前人ID、系统类型判断是否有进入权限
        /// <summary>
        /// 根据当前人ID、系统类型判断是否有进入权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="menuType"></param>
        /// <returns></returns>
        public static bool IsHaveSystemPower(string userId,string menuType, string projectId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                bool isHave = false;
                var getUser = db.Sys_User.FirstOrDefault(x => x.UserId == userId);
                if (getUser != null)
                {
                    if (userId == Const.sysglyId || userId == Const.hfnbdId || userId == Const.sedinId) 
                    {
                        isHave = true;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(projectId))
                        {
                            var getOffice = db.Sys_RolePower.FirstOrDefault(x => x.MenuType == menuType);
                            if (getOffice != null)
                            {
                                isHave = true;
                            }
                        }
                        else
                        {
                            List<string> roleIdList = UserService.GetRoleListByProjectIdUserId(projectId, userId);
                            var pPower = db.Sys_RolePower.FirstOrDefault(x => roleIdList.Contains(x.RoleId) && x.MenuType == menuType);
                            if (pPower != null)
                            {
                                isHave = true;
                            }
                        }
                    }
                }

                return isHave;
            }
        }
        #endregion

        /// <summary>
        ///根据主键删除附件
        /// </summary>
        /// <param name="lawRegulationId"></param>
        public static void DeleteAttachFileById(string id)
        {
            Model.AttachFile attachFile = Funs.DB.AttachFile.FirstOrDefault(e => e.ToKeyId == id);
            if (attachFile != null)
            {
                if (!string.IsNullOrEmpty(attachFile.AttachUrl))
                {
                    UploadFileService.DeleteFile(Funs.RootPath, attachFile.AttachUrl);
                }

                Funs.DB.AttachFile.DeleteOnSubmit(attachFile);
                Funs.DB.SubmitChanges();
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
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var flowOperateList = from x in db.Sys_FlowOperate where x.DataId == id select x;
                if (flowOperateList.Count() > 0)
                {
                    db.Sys_FlowOperate.DeleteAllOnSubmit(flowOperateList);
                    db.SubmitChanges();
                }
            }
        }

        #region 保存数据
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="menuId">菜单id</param>
        /// <param name="dataId">主键id</param>
        /// <param name="isClosed">是否关闭这步流程</param>
        /// <param name="content">单据内容</param>
        /// <param name="url">路径</param>
        public static void btnSaveData(string projectId, string menuId, string dataId, string userId, bool isClosed, string content, string url)
        {
            Model.Sys_FlowOperate newFlowOperate = new Model.Sys_FlowOperate
            {
                MenuId = menuId,
                DataId = dataId,
                OperaterId = userId,
                State = Const.State_2,
                IsClosed = isClosed,
                Opinion = "系统自动关闭流程",
                ProjectId = projectId,
                Url = url
            };
            var user = UserService.GetUserByUserId(newFlowOperate.OperaterId);
            if (user != null)
            {
                var roles = RoleService.GetRoleByRoleId(user.RoleId);
                if (roles != null && !string.IsNullOrEmpty(roles.RoleName))
                {
                    newFlowOperate.AuditFlowName = "[" + roles.RoleName + "]";
                }
                else
                {
                    newFlowOperate.AuditFlowName = "[" + user.UserName + "]";
                }

                newFlowOperate.AuditFlowName += "系统审核完成";
            }

            var updateFlowOperate = from x in Funs.DB.Sys_FlowOperate
                                    where x.DataId == newFlowOperate.DataId && (x.IsClosed == false || !x.IsClosed.HasValue)
                                    select x;
            if (updateFlowOperate.Count() > 0)
            {
                foreach (var item in updateFlowOperate)
                {
                    item.OperaterId = newFlowOperate.OperaterId;
                    item.OperaterTime = System.DateTime.Now;
                    item.State = newFlowOperate.State;
                    item.Opinion = newFlowOperate.Opinion;
                    item.AuditFlowName = "系统审核完成";
                    item.IsClosed = newFlowOperate.IsClosed;
                    Funs.DB.SubmitChanges();
                }
            }
            else
            {
                int maxSortIndex = 1;
                var flowSet = Funs.DB.Sys_FlowOperate.Where(x => x.DataId == newFlowOperate.DataId);
                var sortIndex = flowSet.Select(x => x.SortIndex).Max();
                if (sortIndex.HasValue)
                {
                    maxSortIndex = sortIndex.Value + 1;
                }
                newFlowOperate.FlowOperateId = SQLHelper.GetNewID(typeof(Model.Sys_FlowOperate));
                newFlowOperate.SortIndex = maxSortIndex;
                newFlowOperate.OperaterTime = System.DateTime.Now;
                newFlowOperate.AuditFlowName = "系统审核完成";
                Funs.DB.Sys_FlowOperate.InsertOnSubmit(newFlowOperate);
                Funs.DB.SubmitChanges();
            }

            if (newFlowOperate.IsClosed == true)
            {
                var updateNoClosedFlowOperate = from x in Funs.DB.Sys_FlowOperate
                                                where x.DataId == newFlowOperate.DataId && (x.IsClosed == false || !x.IsClosed.HasValue)
                                                select x;
                if (updateNoClosedFlowOperate.Count() > 0)
                {
                    foreach (var itemClosed in updateNoClosedFlowOperate)
                    {
                        itemClosed.IsClosed = true;
                        Funs.DB.SubmitChanges();
                    }
                }
            }
        }
        #endregion

        /// 接收http post请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="parameters">查询参数集合</param>
        /// <returns></returns>
        public static string CreateGetHttpResponse(string url)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;//创建请求对象
            request.Method = "Get";//请求方式
            request.ContentType = "application/x-www-form-urlencoded";//链接类型
            try
            {
                HttpWebResponse webresponse = request.GetResponse() as HttpWebResponse;
                using (Stream s = webresponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(s, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
