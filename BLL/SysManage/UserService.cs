namespace BLL
{
    using System.Collections.Generic;
    using System.Linq;

    public static class UserService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns>用户信息</returns>
        public static Model.Sys_User GetUserByUserId(string userId)
        {
            return Funs.DB.Sys_User.FirstOrDefault(e => e.UserId == userId);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns>用户信息</returns>
        public static void UpdateLastUserInfo(string userId, string LastMenuType,bool LastIsOffice,string LastProjectId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var uU = db.Sys_User.FirstOrDefault(e => e.UserId == userId);
                if (uU != null)
                {
                    uU.LastMenuType = LastMenuType;
                    uU.LastIsOffice = LastIsOffice;
                    uU.LastMenuType = LastMenuType;
                    db.SubmitChanges();
                }
            }
        }

        /// <summary>
        /// 获取用户账号是否存在
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="account">账号</param>
        /// <returns>是否存在</returns>
        public static bool IsExistUserAccount(string userId, string account)
        {
            bool isExist = false;
            var role = Funs.DB.Sys_User.FirstOrDefault(x => x.Account == account && (x.UserId != userId || (userId == null && x.UserId != null)));
            if (role != null)
            {
                isExist = true;
            }
            return isExist;
        }

        /// <summary>
        /// 获取用户账号是否存在
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="identityCard">身份证号码</param>
        /// <returns>是否存在</returns>
        public static bool IsExistUserIdentityCard(string userId, string identityCard)
        {
            bool isExist = false;
            var role = Funs.DB.Sys_User.FirstOrDefault(x => x.IdentityCard == identityCard && (x.UserId != userId || (userId == null && x.UserId != null)));
            if (role != null)
            {
                isExist = true;
            }
            return isExist;
        }

        /// <summary>
        /// 根据用户获取密码
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string GetPasswordByUserId(string userId)
        {
            Model.Sys_User m = Funs.DB.Sys_User.FirstOrDefault(e => e.UserId == userId);
            return m.Password;
        }

        /// <summary>
        /// 根据用户获取用户名称
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string GetUserNameByUserId(string userId)
        {
            string userName = string.Empty;
            Model.Sys_User user = Funs.DB.Sys_User.FirstOrDefault(e => e.UserId == userId);
            if (user != null)
            {
                userName = user.UserName;
            }

            return userName;
        }

        /// <summary>
        /// 根据用户获取用户名称
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string GetUserNameAndTelByUserId(string userId)
        {
            string userName = string.Empty;
            Model.Sys_User user = Funs.DB.Sys_User.FirstOrDefault(e => e.UserId == userId);
            if (user != null)
            {
                userName = user.UserName ;
                if (!string.IsNullOrEmpty(user.Telephone))
                {
                    userName += "；" + user.Telephone;
                }
            }

            return userName;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        public static void UpdatePassword(string userId, string password)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Sys_User m = db.Sys_User.FirstOrDefault(e => e.UserId == userId);
            if (m != null)
            {
                m.Password = Funs.EncryptionPassword(password);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 增加人员信息
        /// </summary>
        /// <param name="user">人员实体</param>
        public static void AddUser(Model.Sys_User user)
        {
            Model.SGGLDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Sys_User));
            Model.Sys_User newUser = new Model.Sys_User
            {
                UserId = newKeyID,
                Account = user.Account,
                UserName = user.UserName,
                UserCode = user.UserCode,
                Password = user.Password,
                UnitId = user.UnitId,
                RoleId = user.RoleId,
                IsPost = user.IsPost,
                IdentityCard = user.IdentityCard,
                PageSize = 10,
                IsOffice = user.IsOffice,
                Telephone=user.Telephone,
                DataSources = user.DataSources,
                SignatureUrl = user.SignatureUrl,
                DepartId = user.DepartId,
            };
            db.Sys_User.InsertOnSubmit(newUser);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改人员信息
        /// </summary>
        /// <param name="user">人员实体</param>
        public static void UpdateUser(Model.Sys_User user)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Sys_User newUser = db.Sys_User.FirstOrDefault(e => e.UserId == user.UserId);
            if (newUser != null)
            {
                newUser.Account = user.Account;
                newUser.UserName = user.UserName;
                newUser.UserCode = user.UserCode;
                if (!string.IsNullOrEmpty(user.Password))
                {
                    newUser.Password = user.Password;
                }
                newUser.IdentityCard = user.IdentityCard;
                newUser.UnitId = user.UnitId;
                newUser.RoleId = user.RoleId;
                newUser.IsPost = user.IsPost;
                newUser.IsOffice = user.IsOffice;
                newUser.Telephone = user.Telephone;
                newUser.SignatureUrl = user.SignatureUrl;
                newUser.DepartId = user.DepartId;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据人员Id删除一个人员信息
        /// </summary>
        /// <param name="userId"></param>
        public static void DeleteUser(string userId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Sys_User user = db.Sys_User.FirstOrDefault(e => e.UserId == userId);
            if (user != null)
            {
                var logs = from x in db.Sys_Log where x.UserId == userId select x;
                if (logs.Count() > 0)
                {
                    db.Sys_Log.DeleteAllOnSubmit(logs);
                }
                db.Sys_User.DeleteOnSubmit(user);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取用户下拉选项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Sys_User> GetUserList()
        {
            var list = (from x in Funs.DB.Sys_User orderby x.UserName select x).ToList();
            return list;
        }

        /// <summary>
        /// 获取用户下拉选项  项目 角色 且可审批
        /// </summary>
        /// <returns></returns>
        public static List<Model.SpSysUserItem> GetProjectRoleUserListByProjectId(string projectId, string unitId)
        {
            IQueryable<Model.SpSysUserItem> users = null;
            if (!string.IsNullOrEmpty(projectId))
            {
                List<Model.SpSysUserItem> returUsers = new List<Model.SpSysUserItem>();
                List<Model.Project_ProjectUser> getPUser = new List<Model.Project_ProjectUser>();
                if (!string.IsNullOrEmpty(unitId))
                {
                    getPUser = (from x in Funs.DB.Project_ProjectUser
                                join u in Funs.DB.Project_ProjectUnit on new { x.ProjectId, x.UnitId } equals new { u.ProjectId, u.UnitId }
                                where x.ProjectId == projectId && (u.UnitId == unitId || u.UnitType == BLL.Const.ProjectUnitType_1 || u.UnitType == BLL.Const.ProjectUnitType_3 || u.UnitType == BLL.Const.ProjectUnitType_4)
                                select x).ToList();
                }
                else
                {
                    getPUser = (from x in Funs.DB.Project_ProjectUser
                                where x.ProjectId == projectId
                                select x).ToList();
                }

                if (getPUser.Count() > 0)
                {
                    foreach (var item in getPUser)
                    {
                        List<string> roleIdList = Funs.GetStrListByStr(item.RoleId,',');
                        var getRoles = Funs.DB.Sys_Role.FirstOrDefault(x => x.IsAuditFlow == true && roleIdList.Contains(x.RoleId));
                        if (getRoles != null)
                        {
                            string userName = RoleService.getRoleNamesRoleIds(item.RoleId) + "-" + UserService.GetUserNameByUserId(item.UserId);
                            Model.SpSysUserItem newsysUser = new Model.SpSysUserItem
                            {
                                UserId = item.UserId,
                                UserName = userName,
                            };
                            returUsers.Add(newsysUser);
                        }
                    }
                }
                return returUsers;
            }
            else
            {
                if (!string.IsNullOrEmpty(unitId))
                {
                    users = (from x in Funs.DB.Sys_User
                             join z in Funs.DB.Sys_Role on x.RoleId equals z.RoleId
                             where x.IsPost == true && z.IsAuditFlow == true && x.UnitId == unitId
                             orderby x.UserCode
                             select new Model.SpSysUserItem
                             {
                                 UserName = z.RoleName + "- " + x.UserName,
                                 UserId = x.UserId,
                             });
                }
                else
                {
                    users = (from x in Funs.DB.Sys_User
                             join z in Funs.DB.Sys_Role on x.RoleId equals z.RoleId
                             where x.IsPost == true && z.IsAuditFlow == true
                             orderby x.UserCode
                             select new Model.SpSysUserItem
                             {
                                 UserName = z.RoleName + "- " + x.UserName,
                                 UserId = x.UserId,
                             });
                }
            }
            return users.ToList();
        }

        /// <summary>
        /// 根据项目号和单位Id获取用户下拉选项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Sys_User> GetUserListByProjectIdAndUnitId(string projectId, string unitId)
        {
            List<Model.Sys_User> list = new List<Model.Sys_User>();
            if (!string.IsNullOrEmpty(projectId))
            {
                if (!string.IsNullOrEmpty(unitId))
                {
                    list = (from x in Funs.DB.Sys_User
                            join y in Funs.DB.Project_ProjectUser
                            on x.UserId equals y.UserId
                            where y.ProjectId == projectId && x.UnitId == unitId
                            orderby x.UserName
                            select x).ToList();
                }
                else
                {
                    list = (from x in Funs.DB.Sys_User
                            join y in Funs.DB.Project_ProjectUser
                            on x.UserId equals y.UserId
                            where y.ProjectId == projectId 
                            orderby x.UserName
                            select x).ToList();
                }
               
            }
            else
            {
                list = (from x in Funs.DB.Sys_User
                        where x.UnitId == unitId
                        orderby x.UserName
                        select x).ToList();
            }
            return list;
        }

        /// <summary>
        /// 获取在岗用户下拉选项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Sys_User> GetProjectUserListByProjectId(string projectId)
        {
            var users = (from x in Funs.DB.Sys_User
                         where x.IsPost == true && x.UserId != Const.hfnbdId && x.UserId != Const.sedinId
                         orderby x.UserName
                         select x).ToList();
            if (!string.IsNullOrEmpty(projectId))
            {
                users = (from x in users
                         join y in Funs.DB.Project_ProjectUser on x.UserId equals y.UserId
                         where y.ProjectId == projectId
                         orderby x.UserName
                         select x).ToList();
            }

            return users;
        }
        
        /// <summary>
        /// 根据项目号和角色Id获取用户下拉选项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Sys_User> GetUserListByProjectIdAndRoleId(string projectId, string roleIds)
        {
            List<string> listRoles = Funs.GetStrListByStr(roleIds, ',');
            List<Model.Sys_User> list = new List<Model.Sys_User>();
            if (!string.IsNullOrEmpty(projectId))
            {
                if (listRoles.Count() > 0)
                {
                    list = (from x in Funs.DB.Sys_User
                            join y in Funs.DB.Project_ProjectUser
                            on x.UserId equals y.UserId
                            where y.ProjectId == projectId && listRoles.Contains(y.RoleId)
                            orderby x.UserName
                            select x).ToList();
                }
                else
                {
                    list = (from x in Funs.DB.Sys_User
                            join y in Funs.DB.Project_ProjectUser
                            on x.UserId equals y.UserId
                            where y.ProjectId == projectId
                            orderby x.UserName
                            select x).ToList();
                }
            }
            else
            {
                list = (from x in Funs.DB.Sys_User
                        where x.UserId != BLL.Const.hfnbdId && x.UserId != Const.sedinId
                        orderby x.UserName
                        select x).ToList();

                if (listRoles.Count() > 0)
                {
                    list = list.Where(x => listRoles.Contains(x.RoleId)).ToList();
                }
            }

            return list;
        }

        #region 用户下拉框
        /// <summary>
        /// 用户下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="projectId">项目id</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitUserDropDownList(FineUIPro.DropDownList dropName, string projectId, bool isShowPlease)
        {
            dropName.DataValueField = "UserId";
            dropName.DataTextField = "UserName";
            dropName.DataSource = BLL.UserService.GetProjectUserListByProjectId(projectId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        /// 带角色用户下拉框 
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="projectId">项目id</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitFlowOperateControlUserDropDownList(FineUIPro.DropDownList dropName, string projectId, string unitId, bool isShowPlease)
        {
            dropName.DataValueField = "UserId";
            dropName.DataTextField = "UserName";
            dropName.DataSource = BLL.UserService.GetProjectRoleUserListByProjectId(projectId, unitId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        /// 用户下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="projectId">项目id</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitUserProjectIdUnitIdDropDownList(FineUIPro.DropDownList dropName, string projectId, string unitId, bool isShowPlease)
        {
            dropName.DataValueField = "UserId";
            dropName.DataTextField = "UserName";
            dropName.DataSource = BLL.UserService.GetUserListByProjectIdAndUnitId(projectId, unitId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        /// 用户下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="projectId">项目id</param>
        /// <param name="roleIds">角色id</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitUserProjectIdRoleIdDropDownList(FineUIPro.DropDownList dropName, string projectId, string roleIds, bool isShowPlease)
        {
            dropName.DataValueField = "UserId";
            dropName.DataTextField = "UserName";
            dropName.DataSource = BLL.UserService.GetUserListByProjectIdAndRoleId(projectId, roleIds);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataId"></param>
        public static void DeleteUserRead(string dataId)
        {
            Model.SGGLDB db = Funs.DB;
            var userRs = from x in Funs.DB.Sys_UserRead where x.DataId == dataId select x;
            if (userRs.Count()>0)
            {
                Funs.DB.Sys_UserRead.DeleteAllOnSubmit(userRs);
                Funs.DB.SubmitChanges();
            }
        }

        #region 根据多用户ID得到用户名称字符串
        /// <summary>
        /// 根据多用户ID得到用户名称字符串
        /// </summary>
        /// <param name="bigType"></param>
        /// <returns></returns>
        public static string getUserNamesUserIds(object userIds)
        {
            string userName = string.Empty;
            if (userIds != null)
            {
                string[] ids = userIds.ToString().Split(',');
                foreach (string id in ids)
                {
                    var q = GetUserNameByUserId(id);
                    if (q != null)
                    {
                        userName += q + ",";
                    }
                }
                if (userName != string.Empty)
                {
                    userName = userName.Substring(0, userName.Length - 1); ;
                }
            }

            return userName;
        }
        #endregion
    }
}
