namespace BLL
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.UI.WebControls;

    public static class UserService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// ��ȡ�û���Ϣ
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <returns>�û���Ϣ</returns>
        public static Model.Sys_User GetUserByUserId(string userId)
        {
            return Funs.DB.Sys_User.FirstOrDefault(e => e.UserId == userId);
        }

        /// <summary>
        /// ��ȡ�û���Ŀ�Ͻ�ɫList
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <returns>�û���Ϣ</returns>
        public static List<string> GetRoleListByProjectIdUserId(string projectId, string userId)
        {
            List<string> roleList = new List<string>();
            var getUser = GetUserByUserId(userId);
            if (getUser != null)
            {
                string rolesStr = string.Empty;
                if (!string.IsNullOrEmpty(getUser.RoleId))
                {
                    rolesStr = getUser.RoleId;
                }
                var pUser = ProjectUserService.GetProjectUserByUserIdProjectId(projectId, userId); ////�û�
                if (pUser != null && !string.IsNullOrEmpty(pUser.RoleId))
                {
                    if (string.IsNullOrEmpty(rolesStr))
                    {
                        rolesStr = pUser.RoleId;
                    }
                    else
                    {
                        if (!rolesStr.Contains(pUser.RoleId))
                        {
                            rolesStr += "," + pUser.RoleId;
                        }
                    }
                }

                roleList = Funs.GetStrListByStr(rolesStr, ',');
            }
            return roleList;
        }

        /// <summary>
        /// ��ȡ�û���Ϣ
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <returns>�û���Ϣ</returns>
        public static void UpdateLastUserInfo(string userId, string LastMenuType, bool LastIsOffice, string LastProjectId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var uU = db.Sys_User.FirstOrDefault(e => e.UserId == userId);
                if (uU != null)
                {
                    uU.LastMenuType = LastMenuType;
                    uU.LastIsOffice = LastIsOffice;
                    uU.LastMenuType = LastMenuType;
                    uU.LastProjectId = LastProjectId;
                    db.SubmitChanges();
                }
            }
        }

        /// <summary>
        /// ��ȡ�û��˺��Ƿ����
        /// </summary>
        /// <param name="userId">�û�id</param>
        /// <param name="account">�˺�</param>
        /// <returns>�Ƿ����</returns>
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
        /// ��ȡ�û��˺��Ƿ����
        /// </summary>
        /// <param name="userId">�û�id</param>
        /// <param name="identityCard">���֤����</param>
        /// <returns>�Ƿ����</returns>
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
        /// �����û���ȡ����
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string GetPasswordByUserId(string userId)
        {
            Model.Sys_User m = Funs.DB.Sys_User.FirstOrDefault(e => e.UserId == userId);
            return m.Password;
        }

        /// <summary>
        /// �����û���ȡ�û�����
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
        /// �����û���ȡ�û�����
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string GetUserNameAndTelByUserId(string userId)
        {
            string userName = string.Empty;
            Model.Sys_User user = Funs.DB.Sys_User.FirstOrDefault(e => e.UserId == userId);
            if (user != null)
            {
                userName = user.UserName;
                if (!string.IsNullOrEmpty(user.Telephone))
                {
                    userName += "��" + user.Telephone;
                }
            }

            return userName;
        }
        /// <summary>
        /// �޸�����
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
        /// ������Ա��Ϣ
        /// </summary>
        /// <param name="user">��Աʵ��</param>
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
                Telephone = user.Telephone,
                DataSources = user.DataSources,
                SignatureUrl = user.SignatureUrl,
                DepartId = user.DepartId,
                Politicalstatus = user.Politicalstatus,
                Hometown = user.Hometown,
                Education = user.Education,
                Graduate = user.Graduate,
                Major = user.Major,
                CertificateId = user.CertificateId,
                IntoDate = user.IntoDate,
                ValidityDate = user.ValidityDate,
                Sex = user.Sex,
                BirthDay = user.BirthDay,
                PositionId = user.PositionId,
                PostTitleId = user.PostTitleId,
                WorkPostId=user.WorkPostId,
                DataFrom = user.DataFrom,
            };
            db.Sys_User.InsertOnSubmit(newUser);
            db.SubmitChanges();
        }

        #region �û���Ϣά�� --����
        /// <summary>
        /// �޸��û���Ϣ
        /// </summary>
        /// <param name="user">��Աʵ��</param>
        public static void UpdateSysUser(Model.Sys_User user)
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
                newUser.DataFrom = user.DataFrom;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// �޸�Ա����Ϣ
        /// </summary>
        /// <param name="user">��Աʵ��</param>
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
                newUser.Politicalstatus = user.Politicalstatus;
                newUser.Hometown = user.Hometown;
                newUser.Education = user.Education;
                newUser.Graduate = user.Graduate;
                newUser.Major = user.Major;
                newUser.CertificateId = user.CertificateId;
                newUser.IntoDate = user.IntoDate;
                newUser.ValidityDate = user.ValidityDate;
                newUser.Sex = user.Sex;
                newUser.BirthDay = user.BirthDay;
                newUser.PositionId = user.PositionId;
                newUser.PostTitleId = user.PostTitleId;
                newUser.WorkPostId = user.WorkPostId;
                newUser.ProjectId = user.ProjectId;
                newUser.ProjectRoleId = user.ProjectRoleId;
                newUser.ProjectWorkPostId = user.ProjectWorkPostId;
                db.SubmitChanges();
            }
        }
        #endregion

        /// <summary>
        /// ������ԱIdɾ��һ����Ա��Ϣ
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
        /// ��ȡ�û�����ѡ��
        /// </summary>
        /// <returns></returns>
        public static List<Model.Sys_User> GetUserList()
        {
            var list = (from x in Funs.DB.Sys_User orderby x.UserName select x).ToList();
            return list;
        }

        /// <summary>
        /// ��ȡ�û�����ѡ��  ��Ŀ ��ɫ �ҿ�����
        /// </summary>
        /// <returns></returns>
        public static List<Model.SpSysUserItem> GetProjectRoleUserListByProjectId(string projectId, string unitId)
        {
            Model.SGGLDB db = Funs.DB;
            IQueryable<Model.SpSysUserItem> users = null;
            if (!string.IsNullOrEmpty(projectId))
            {
                List<Model.SpSysUserItem> returUsers = new List<Model.SpSysUserItem>();
                List<Model.Project_ProjectUser> getPUser = new List<Model.Project_ProjectUser>();
                if (!string.IsNullOrEmpty(unitId))
                {
                    getPUser = (from x in db.Project_ProjectUser
                                join u in db.Project_ProjectUnit on new { x.ProjectId, x.UnitId } equals new { u.ProjectId, u.UnitId }
                                where x.ProjectId == projectId && (u.UnitId == unitId || u.UnitType == BLL.Const.ProjectUnitType_1 || u.UnitType == BLL.Const.ProjectUnitType_3 || u.UnitType == BLL.Const.ProjectUnitType_4)
                                select x).ToList();
                }
                else
                {
                    getPUser = (from x in db.Project_ProjectUser
                                where x.ProjectId == projectId
                                select x).ToList();
                }

                if (getPUser.Count() > 0)
                {
                    foreach (var item in getPUser)
                    {
                        List<string> roleIdList = Funs.GetStrListByStr(item.RoleId, ',');
                        var getRoles = db.Sys_Role.FirstOrDefault(x =>roleIdList.Contains(x.RoleId));
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
                    users = (from x in db.Sys_User
                             join z in db.Sys_Role on x.RoleId equals z.RoleId
                             where x.IsPost == true && x.UnitId == unitId
                             orderby x.UserCode
                             select new Model.SpSysUserItem
                             {
                                 UserName = z.RoleName + "- " + x.UserName,
                                 UserId = x.UserId,
                             });
                }
                else
                {
                    users = (from x in db.Sys_User
                             join z in db.Sys_Role on x.RoleId equals z.RoleId
                             where x.IsPost == true 
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
        /// ������Ŀ�ź͵�λId��ȡ�û�����ѡ��
        /// </summary>
        /// <returns></returns>
        public static List<Model.Sys_User> GetUserListByProjectIdAndUnitId(string projectId, string unitId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                List<Model.Sys_User> list = new List<Model.Sys_User>();
                if (!string.IsNullOrEmpty(projectId))
                {
                    if (!string.IsNullOrEmpty(unitId))
                    {
                        list = (from x in db.Sys_User
                                join y in db.Project_ProjectUser
                                on x.UserId equals y.UserId
                                where y.ProjectId == projectId && x.UnitId == unitId
                                orderby x.UserName
                                select x).ToList();
                    }
                    else
                    {
                        list = (from x in db.Sys_User
                                join y in db.Project_ProjectUser
                                on x.UserId equals y.UserId
                                where y.ProjectId == projectId
                                orderby x.UserName
                                select x).ToList();
                    }

                }
                else
                {
                    list = (from x in db.Sys_User
                            where x.UnitId == unitId
                            orderby x.UserName
                            select x).ToList();
                }
                return list;
            }
        }

        /// <summary>
        /// ��ȡ�ڸ��û�����ѡ��
        /// </summary>
        /// <returns></returns>
        public static List<Model.Sys_User> GetProjectUserListByProjectId(string projectId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var users = (from x in db.Sys_User
                             where x.IsPost == true && x.UserId != Const.hfnbdId && x.UserId != Const.sedinId
                             orderby x.UserName
                             select x).ToList();
                if (!string.IsNullOrEmpty(projectId))
                {
                    users = (from x in users
                             join y in db.Project_ProjectUser on x.UserId equals y.UserId
                             where y.ProjectId == projectId
                             orderby x.UserName
                             select x).ToList();
                }

                return users;
            }
        }

        /// <summary>
        /// ������Ŀ�źͽ�ɫId��ȡ�û�����ѡ��
        /// </summary>
        /// <returns></returns>
        public static List<Model.Sys_User> GetUserListByProjectIdAndRoleId(string projectId, string roleIds)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                List<string> listRoles = Funs.GetStrListByStr(roleIds, ',');
                List<Model.Sys_User> list = new List<Model.Sys_User>();
                if (!string.IsNullOrEmpty(projectId))
                {
                    if (listRoles.Count() > 0)
                    {
                        list = (from x in db.Sys_User
                                join y in db.Project_ProjectUser
                                on x.UserId equals y.UserId
                                where y.ProjectId == projectId && listRoles.Contains(y.RoleId)
                                orderby x.UserName
                                select x).ToList();
                    }
                    else
                    {
                        list = (from x in db.Sys_User
                                join y in db.Project_ProjectUser
                                on x.UserId equals y.UserId
                                where y.ProjectId == projectId
                                orderby x.UserName
                                select x).ToList();
                    }
                }
                else
                {
                    list = (from x in db.Sys_User
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
        }
        
        /// <summary>
        /// ������Ŀ�źͽ�ɫId��ȡ�û�����ѡ��
        /// </summary>
        /// <returns></returns>
        public static List<Model.Sys_User> GetUserListByProjectIdUnitIdRoleId(string projectId,string unitId, string roleIds)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                List<string> listRoles = Funs.GetStrListByStr(roleIds, ',');
                List<Model.Sys_User> list = new List<Model.Sys_User>();
                if (!string.IsNullOrEmpty(projectId))
                {
                    if (listRoles.Count() > 0)
                    {
                        list = (from x in db.Sys_User
                                join y in db.Project_ProjectUser
                                on x.UserId equals y.UserId
                                where y.ProjectId == projectId && x.UnitId == unitId && listRoles.Contains(y.RoleId) 
                                orderby x.UserName
                                select x).ToList();
                    }
                    else
                    {
                        list = (from x in db.Sys_User
                                join y in db.Project_ProjectUser
                                on x.UserId equals y.UserId
                                where y.ProjectId == projectId && x.UnitId == unitId
                                orderby x.UserName
                                select x).ToList();
                    }
                }
                else
                {
                    list = (from x in db.Sys_User
                            where   x.UnitId == unitId && x.UserId != BLL.Const.hfnbdId && x.UserId != Const.sedinId
                            orderby x.UserName
                            select x).ToList();

                    if (listRoles.Count() > 0)
                    {
                        list = list.Where(x => listRoles.Contains(x.RoleId)).ToList();
                    }
                }

                return list;
            }
        }

        #region �û�������
        /// <summary>
        /// �û�������
        /// </summary>
        /// <param name="dropName">����������</param>
        /// <param name="projectId">��Ŀid</param>
        /// <param name="isShowPlease">�Ƿ���ʾ��ѡ��</param>
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
        /// ����ɫ�û������� 
        /// </summary>
        /// <param name="dropName">����������</param>
        /// <param name="projectId">��Ŀid</param>
        /// <param name="isShowPlease">�Ƿ���ʾ��ѡ��</param>
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
        /// �û�������
        /// </summary>
        /// <param name="dropName">����������</param>
        /// <param name="projectId">��Ŀid</param>
        /// <param name="isShowPlease">�Ƿ���ʾ��ѡ��</param>
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
        /// �û�������
        /// </summary>
        /// <param name="dropName">����������</param>
        /// <param name="projectId">��Ŀid</param>
        /// <param name="roleIds">��ɫid</param>
        /// <param name="isShowPlease">�Ƿ���ʾ��ѡ��</param>
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

        /// <summary>
        /// �û�������
        /// </summary>
        /// <param name="dropName">����������</param>
        /// <param name="projectId">��Ŀid</param>
        /// <param name="roleIds">��ɫid</param>
        /// <param name="isShowPlease">�Ƿ���ʾ��ѡ��</param>
        public static void InitUserProjectIdUnitIdRoleIdDropDownList(FineUIPro.DropDownList dropName, string projectId, string unitId, string roleIds, bool isShowPlease)
        {
            dropName.DataValueField = "UserId";
            dropName.DataTextField = "UserName";
            dropName.DataSource = BLL.UserService.GetUserListByProjectIdUnitIdRoleId(projectId,unitId, roleIds);
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
            if (userRs.Count() > 0)
            {
                Funs.DB.Sys_UserRead.DeleteAllOnSubmit(userRs);
                Funs.DB.SubmitChanges();
            }
        }

        /// <summary>
        /// ����ʩ����λ����λ���̡�רҵ��ȡ�鿴��Ϣ�û� 
        /// </summary>
        /// <returns></returns>
        public static List<Model.Sys_User> GetSeeUserList(string projectId, string subUnitId, string cNProfessionalCode, string unitWorkId, string subUserId, string mainUserId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                List<Model.Sys_User> users = new List<Model.Sys_User>();
                //�ְ��û�
                var q1 = (from x in db.Project_ProjectUser
                          join y in db.Sys_User
                          on x.UserId equals y.UserId
                          join z in db.SitePerson_Person
                          on y.IdentityCard equals z.IdentityCard
                          join a in db.Base_WorkPost
                          on z.WorkPostId equals a.WorkPostId
                          where x.IsPost == true && x.UnitId == subUnitId
                          && a.CNCodes.Contains(cNProfessionalCode)
                              && x.UserId != subUserId
                              && x.ProjectId == projectId && z.ProjectId == projectId
                          orderby x.UserId
                          select x).ToList();
                foreach (var item in q1)
                {
                    if (!string.IsNullOrEmpty(item.WorkAreaId))
                    {
                        string[] workAreas = item.WorkAreaId.Split(',');
                        foreach (var workArea in workAreas)
                        {
                            if (workArea == unitWorkId)
                            {
                                users.Add(GetUserByUserId(item.UserId));
                            }
                        }
                    }
                }
                //�ܰ��û�
                Model.Base_Unit mainUnit = BLL.UnitService.GetUnitByProjectIdUnitTypeList(projectId, Const.ProjectUnitType_1)[0];
                string mainUnitId = string.Empty;
                if (mainUnit != null)
                {
                    mainUnitId = mainUnit.UnitId;
                }
                var q2 = (from x in db.Project_ProjectUser
                          join y in db.Sys_User
                          on x.UserId equals y.UserId
                          join z in db.SitePerson_Person
                          on y.IdentityCard equals z.IdentityCard
                          join a in db.Base_WorkPost
                          on z.WorkPostId equals a.WorkPostId
                          where x.IsPost == true && x.UnitId == mainUnitId && a.CNCodes.Contains(cNProfessionalCode)
                              && x.UserId != mainUserId
                              && x.ProjectId == projectId && z.ProjectId == projectId
                          orderby x.UserId
                          select x).ToList();
                foreach (var item in q2)
                {
                    if (!string.IsNullOrEmpty(item.WorkAreaId))
                    {
                        string[] workAreas = item.WorkAreaId.Split(',');
                        foreach (var workArea in workAreas)
                        {
                            if (workArea == unitWorkId)
                            {
                                users.Add(GetUserByUserId(item.UserId));
                            }
                        }
                    }
                }
                return users;
            }
        }

        /// <summary>
        /// �û�������
        /// </summary>
        /// <param name="dropName">����������</param>
        /// <param name="projectId">��Ŀid</param>
        /// <param name="isShowPlease">�Ƿ���ʾ��ѡ��</param>
        public static void InitUserDropDownList(FineUIPro.DropDownList dropName, string projectId, bool isShowPlease, string UnitId)
        {
            dropName.DataValueField = "Value";
            dropName.DataTextField = "Text";
            if (!string.IsNullOrWhiteSpace(UnitId))
            {
                dropName.DataSource = GetUserByUnitId(projectId, UnitId);
            }
            else
            {
                dropName.DataSource = GetMainUserList(projectId);
            }

            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        /// ���յ�λ��ѯ�û�
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static ListItem[] GetUserByUnitId(string projectId, string unitId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var user = (from x in db.Sys_User
                            where x.IsPost == true
                            orderby x.UserId
                            select x).ToList();
                if (!string.IsNullOrEmpty(projectId) && !string.IsNullOrEmpty(unitId))
                {
                    user = (from x in user
                            join y in db.Project_ProjectUser on x.UserId equals y.UserId
                            where (y.ProjectId == projectId && y.UnitId == unitId)
                            select x).ToList();
                }
                else
                {
                    user = (from x in user
                            join y in db.Project_ProjectUser on x.UserId equals y.UserId
                            where (y.ProjectId == projectId)
                            select x).ToList();
                }

                ListItem[] lis = new ListItem[user.Count()];
                for (int i = 0; i < user.Count(); i++)
                {
                    lis[i] = new ListItem(user[i].UserName ?? "", user[i].UserId.ToString());
                }
                return lis;
            }
        }

        /// <summary>
        /// ��ȡ��Ŀ�ܰ��û�
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static ListItem[] GetMainUserList(string projectId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var user = (from x in db.Sys_User
                            join y in db.Project_ProjectUser on x.UserId equals y.UserId
                            join z in db.Project_ProjectUnit on x.UnitId equals z.UnitId
                            where x.IsPost == true && y.ProjectId == projectId && z.ProjectId == projectId && z.UnitType == Const.ProjectUnitType_1
                            orderby x.UserId
                            select x).ToList();
                ListItem[] lis = new ListItem[user.Count()];
                for (int i = 0; i < user.Count(); i++)
                {
                    lis[i] = new ListItem(user[i].UserName ?? "", user[i].UserId.ToString());
                }
                return lis;
            }
        }

        public static void Init(FineUIPro.DropDownList dropName, string projectId, bool isShowPlease)
        {
            dropName.DataValueField = "Text";
            dropName.DataTextField = "Text";
            dropName.DataSource = GetMainUserList(projectId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        /// ����ʩ����λ����λ���̡�רҵ��ȡ�鿴��Ϣ�û� 
        /// </summary>
        /// <returns></returns>
        public static List<Model.Sys_User> GetSeeUserList2(string projectId, string subUnitId, string cNProfessionalCode, string unitWorkId, string mainUserId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                List<Model.Sys_User> users = new List<Model.Sys_User>();
                //�ְ��û�
                var q1 = (from x in db.Project_ProjectUser
                          join y in db.Sys_User
                          on x.UserId equals y.UserId
                          join z in db.SitePerson_Person
                          on y.IdentityCard equals z.IdentityCard
                          join a in db.Base_WorkPost
                          on z.WorkPostId equals a.WorkPostId
                          where x.IsPost == true && x.UnitId == subUnitId && a.CNCodes.Contains(cNProfessionalCode)
                              && x.ProjectId == projectId && z.ProjectId == projectId
                          orderby x.UserId
                          select x).ToList();
                foreach (var item in q1)
                {
                    if (!string.IsNullOrEmpty(item.WorkAreaId))
                    {
                        string[] workAreas = item.WorkAreaId.Split(',');
                        foreach (var workArea in workAreas)
                        {
                            if (workArea == unitWorkId)
                            {
                                users.Add(GetUserByUserId(item.UserId));
                            }
                        }
                    }
                }
                //�ܰ��û�
                Model.Base_Unit mainUnit = BLL.UnitService.GetUnitByProjectIdUnitTypeList(projectId, Const.ProjectUnitType_1)[0];
                string mainUnitId = string.Empty;
                if (mainUnit != null)
                {
                    mainUnitId = mainUnit.UnitId;
                }
                var q2 = (from x in db.Project_ProjectUser
                          join y in db.Sys_User
                          on x.UserId equals y.UserId
                          join z in db.SitePerson_Person
                          on y.IdentityCard equals z.IdentityCard
                          join a in db.Base_WorkPost
                          on z.WorkPostId equals a.WorkPostId
                          where x.IsPost == true && x.UnitId == mainUnitId && a.CNCodes.Contains(cNProfessionalCode)
                              && x.UserId != mainUserId
                              && x.ProjectId == projectId && z.ProjectId == projectId
                          orderby x.UserId
                          select x).ToList();
                foreach (var item in q2)
                {
                    if (!string.IsNullOrEmpty(item.WorkAreaId))
                    {
                        string[] workAreas = item.WorkAreaId.Split(',');
                        foreach (var workArea in workAreas)
                        {
                            if (workArea == unitWorkId)
                            {
                                users.Add(GetUserByUserId(item.UserId));
                            }
                        }
                    }
                }
                return users;
            }
        }

        /// <summary>
        /// ����ʩ����λ����λ���̡�רҵ��ȡ�鿴��Ϣ�û� 
        /// </summary>
        /// <returns></returns>
        public static List<Model.Sys_User> GetSeeUserList3(string projectId, string unitId1, string unitId2, string unitId3, string cNProfessionalCode, string unitWorkId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                List<Model.Sys_User> users = new List<Model.Sys_User>();
                var q1 = (from x in db.Project_ProjectUser
                          join y in db.Sys_User
                          on x.UserId equals y.UserId
                          join z in db.SitePerson_Person
                          on y.IdentityCard equals z.IdentityCard
                          join a in db.Base_WorkPost
                          on z.WorkPostId equals a.WorkPostId
                          where x.IsPost == true && (x.UnitId == unitId1 || x.UnitId == unitId2) && a.CNCodes.Contains(cNProfessionalCode)
                              && x.ProjectId == projectId && z.ProjectId == projectId
                          orderby x.UserId
                          select x).ToList();
                foreach (var item in q1)
                {
                    if (!string.IsNullOrEmpty(item.WorkAreaId))
                    {
                        string[] workAreas = item.WorkAreaId.Split(',');
                        foreach (var workArea in workAreas)
                        {
                            if (workArea == unitWorkId)
                            {
                                users.Add(GetUserByUserId(item.UserId));
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(unitId3))
                {
                    var q2 = (from x in db.Project_ProjectUser
                              join y in db.Sys_User
                              on x.UserId equals y.UserId
                              join z in db.SitePerson_Person
                              on y.IdentityCard equals z.IdentityCard
                              join a in db.Base_WorkPost
                              on z.WorkPostId equals a.WorkPostId
                              where x.IsPost == true && a.CNCodes.Contains(cNProfessionalCode)
                                  && x.ProjectId == projectId && z.ProjectId == projectId
                              orderby x.UserId
                              select x).ToList();
                    var q3 = (from x in q2
                              where unitId3.Split(',').Contains(x.UnitId)
                              select x).ToList();
                    foreach (var item in q3)
                    {
                        if (!string.IsNullOrEmpty(item.WorkAreaId))
                        {
                            string[] workAreas = item.WorkAreaId.Split(',');
                            foreach (var workArea in workAreas)
                            {
                                if (workArea == unitWorkId)
                                {
                                    users.Add(GetUserByUserId(item.UserId));
                                }
                            }
                        }
                    }
                }
                return users;
            }
        }

        /// <summary>
        /// ���ض൥λ�µ��û�
        /// </summary>
        /// <param name="dropName"></param>
        /// <param name="projectId"></param>
        /// <param name="isShowPlease"></param>
        /// <param name="UnitId"></param>
        public static void InitUsersDropDownList(FineUIPro.DropDownList dropName, string projectId, bool isShowPlease, string UnitId)
        {
            dropName.DataValueField = "Value";
            dropName.DataTextField = "Text";
            if (!string.IsNullOrWhiteSpace(UnitId))
            {
                dropName.DataSource = GetUserByUnitIds(projectId, UnitId);
            }
            else
            {
                dropName.DataSource = GetMainUserList(projectId);
            }

            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        /// ��ȡ�൥λ���û�
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitIds"></param>
        /// <returns></returns>
        public static ListItem[] GetUserByUnitIds(string projectId, string unitIds)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var user = (from x in db.Sys_User
                            where x.IsPost == true
                            orderby x.UserId
                            select x).ToList();
                if (!string.IsNullOrEmpty(projectId) && !string.IsNullOrEmpty(unitIds))
                {
                    user = (from x in user
                            join y in db.Project_ProjectUser on x.UserId equals y.UserId
                            where (y.ProjectId == projectId && unitIds.Split(',').Contains(y.UnitId))
                            select x).ToList();
                }
                else
                {
                    user = (from x in user
                            join y in db.Project_ProjectUser on x.UserId equals y.UserId
                            where (y.ProjectId == projectId)
                            select x).ToList();
                }

                ListItem[] lis = new ListItem[user.Count()];
                for (int i = 0; i < user.Count(); i++)
                {
                    lis[i] = new ListItem(user[i].UserName ?? "", user[i].UserId.ToString());
                }
                return lis;
            }
        }

        /// <summary>
        /// ���ݵ�λId����ɫ��ȡ�鿴��Ϣ�û� 
        /// </summary>
        /// <returns></returns>
        public static List<Model.Sys_User> GetSeeUserListByRole(string projectId, string unitId, string role1, string role2, string role3, string role4)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                return (from x in db.Sys_User
                        join z in db.Project_ProjectUser
                        on x.UserId equals z.UserId
                        join y in db.Sys_Role
                        on z.RoleId equals y.RoleId
                        where x.IsPost == true && x.UnitId == unitId && (y.RoleId == role1 || y.RoleId == role2 || y.RoleId == role3 || y.RoleId == role4)
                            && z.ProjectId == projectId
                        orderby x.UserId
                        select x).Distinct().ToList();
            }
        }

        /// <summary>
        /// ����ʩ����λ����λ���̡�רҵ��ȡ�鿴��Ϣ�û� 
        /// </summary>
        /// <returns></returns>
        public static List<Model.Sys_User> GetSeeUserList4(string projectId, string unitId1, string unitId2, string unitId3)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                List<Model.Sys_User> users = new List<Model.Sys_User>();
                var q1 = (from x in db.Sys_User
                          join y in db.Project_ProjectUser
                          on x.UserId equals y.UserId
                          where x.IsPost == true && x.UnitId == unitId1 && y.ProjectId == projectId
                          orderby x.UserId
                          select x).ToList();
                users.AddRange(q1);
                if (!string.IsNullOrEmpty(unitId2))
                {
                    var q2 = from x in db.Sys_User
                             join y in db.Project_ProjectUser
                             on x.UserId equals y.UserId
                             where y.ProjectId == projectId && x.IsPost == true
                             select x;
                    var q3 = (from x in q2 where unitId2.Split(',').Contains(x.UnitId) select x).ToList();
                    users.AddRange(q3);
                }
                if (!string.IsNullOrEmpty(unitId3))
                {
                    var q4 = from x in db.Sys_User
                             join y in db.Project_ProjectUser
                             on x.UserId equals y.UserId
                             where y.ProjectId == projectId && x.IsPost == true
                             select x;
                    var q5 = (from x in q4 where unitId3.Split(',').Contains(x.UnitId) select x).ToList();
                    users.AddRange(q5);
                }
                return users;
            }
        }

        /// <summary>
        /// �����û�������
        /// </summary>
        /// <param name="dropName">����������</param>
        /// <param name="projectId">��Ŀid</param>
        /// <param name="isShowPlease">�Ƿ���ʾ��ѡ��</param>
        public static void InitJLUserDropDownList(FineUIPro.DropDownList dropName, string projectId, bool isShowPlease)
        {
            dropName.DataValueField = "Value";
            dropName.DataTextField = "Text";
            dropName.DataSource = GetJLUserList(projectId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        /// ��ȡ��Ŀ�����û�
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static ListItem[] GetJLUserList(string projectId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var user = (from x in db.Sys_User
                            join y in db.Project_ProjectUser
                            on x.UserId equals y.UserId
                            join z in db.Project_ProjectUnit
                            on y.UnitId equals z.UnitId
                            where x.IsPost == true && y.ProjectId == projectId && z.UnitType == BLL.Const.ProjectUnitType_3
                            orderby x.UserId
                            select x).ToList();
                ListItem[] lis = new ListItem[user.Count()];
                for (int i = 0; i < user.Count(); i++)
                {
                    lis[i] = new ListItem(user[i].UserName ?? "", user[i].UserId.ToString());
                }
                return lis;
            }
        }

        /// <summary>
        /// ҵ���û�������
        /// </summary>
        /// <param name="dropName">����������</param>
        /// <param name="projectId">��Ŀid</param>
        /// <param name="isShowPlease">�Ƿ���ʾ��ѡ��</param>
        public static void InitYZUserDropDownList(FineUIPro.DropDownList dropName, string projectId, bool isShowPlease)
        {
            dropName.DataValueField = "Value";
            dropName.DataTextField = "Text";
            dropName.DataSource = GetYZUserList(projectId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        /// ��ȡ��Ŀҵ���û�
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static ListItem[] GetYZUserList(string projectId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var user = (from x in db.Sys_User
                            join y in db.Project_ProjectUser
                            on x.UserId equals y.UserId
                            join z in db.Project_ProjectUnit
                            on y.UnitId equals z.UnitId
                            where x.IsPost == true && y.ProjectId == projectId && z.UnitType == BLL.Const.ProjectUnitType_4
                            orderby x.UserId
                            select x).ToList();
                ListItem[] lis = new ListItem[user.Count()];
                for (int i = 0; i < user.Count(); i++)
                {
                    lis[i] = new ListItem(user[i].UserName ?? "", user[i].UserId.ToString());
                }
                return lis;
            }
        }

        /// <summary>
        /// ���ݵ�λ���ͼ����û�������
        /// </summary>
        /// <param name="dropName">����������</param>
        /// <param name="projectId">��Ŀid</param>
        /// <param name="isShowPlease">�Ƿ���ʾ��ѡ��</param>
        public static void InitUserProjectIdUnitTypeDropDownList(FineUIPro.DropDownList dropName, string projectId, string unitType, bool isShowPlease)
        {
            dropName.DataValueField = "UserId";
            dropName.DataTextField = "UserName";
            dropName.DataSource = BLL.UserService.GetUserListByProjectIdAndUnitType(projectId, unitType);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        /// <summary>
        /// ������Ŀ�ź͵�λ���ͻ�ȡ�û�����ѡ��
        /// </summary>
        /// <returns></returns>
        public static List<Model.Sys_User> GetUserListByProjectIdAndUnitType(string projectId, string unitType)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                List<Model.Sys_User> list = new List<Model.Sys_User>();
                if (!string.IsNullOrEmpty(projectId))
                {
                    list = (from x in db.Sys_User
                            join y in db.Project_ProjectUser
                            on x.UserId equals y.UserId
                            join z in db.Project_ProjectUnit
                            on x.UnitId equals z.UnitId
                            where y.ProjectId == projectId && z.ProjectId == projectId && z.UnitType == unitType
                            orderby x.UserName
                            select x).ToList();
                }
                return list;
            }
        }

        #region ���ݶ��û�ID�õ��û������ַ���
        /// <summary>
        /// ���ݶ��û�ID�õ��û������ַ���
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<Model.Sys_User> GetProjectUserListByProjectIdForApi(string projectId, string name)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var users = (from x in db.Sys_User
                             where x.IsPost == true && x.UserId != BLL.Const.hfnbdId
                             where name == "" || x.UserName.Contains(name)
                             orderby x.UserName
                             select x).ToList();
                if (!string.IsNullOrEmpty(projectId))
                {
                    users = (from x in users
                             join y in db.Project_ProjectUser on x.UserId equals y.UserId
                             where y.ProjectId == projectId
                             orderby x.UserName
                             select x).ToList();
                }

                return users;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="unitType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<Model.Sys_User> GetProjectUserListByProjectIdForApi(string projectId, string unitId, string unitType, string name)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                List<Model.Sys_User> list = new List<Model.Sys_User>();
                if (!string.IsNullOrEmpty(projectId))
                {
                    string[] unitTypes = { };
                    string[] unitIds = { };

                    if (!string.IsNullOrEmpty(unitType))
                    {
                        unitTypes = unitType.Split(',');
                    }
                    if (!string.IsNullOrEmpty(unitId))
                    {
                        unitIds = unitId.Split(',');
                    }
                    list = (from x in db.Sys_User
                            join y in db.Project_ProjectUser on x.UserId equals y.UserId
                            join z in db.Project_ProjectUnit on x.UnitId equals z.UnitId
                            where name == "" || x.UserName.Contains(name)
                            where y.ProjectId == projectId && z.ProjectId == projectId
                            where unitType == "" || unitTypes.Contains(z.UnitType)
                            where unitId == "" || unitIds.Contains(z.UnitId)
                            orderby z.UnitType descending, x.UserName
                            select x).ToList();
                }
                return list;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static Model.Sys_User FindUserByAccount(string account)
        {
            var q = from y in Funs.DB.Sys_User
                    where y.Account == account && y.IsPost == true
                    select y;
            return q.FirstOrDefault();
        }

        /// <summary>
        /// ���ݵ�λId��ȡ�û�����ѡ��
        /// </summary>
        /// <returns></returns>
        public static List<Model.Sys_User> GetUserListByUnitId(string unitId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                List<Model.Sys_User> list = new List<Model.Sys_User>();
                list = (from x in db.Sys_User
                        where x.UnitId == unitId
                        orderby x.UserName
                        select x).ToList();
                return list;
            }
        }

        /// <summary>
        /// �û�������
        /// </summary>
        /// <param name="dropName">����������</param>
        /// <param name="isShowPlease">�Ƿ���ʾ��ѡ��</param>
        public static void InitUserUnitIdDropDownList(FineUIPro.DropDownList dropName, string unitId, bool isShowPlease)
        {
            dropName.DataValueField = "UserId";
            dropName.DataTextField = "UserName";
            dropName.DataSource = BLL.UserService.GetUserListByUnitId(unitId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        /// ���ݽ�ɫ��ȡ�鿴�û� 
        /// </summary>
        /// <returns></returns>
        public static List<Model.Sys_User> GetUserListByRole(string role)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                return (from x in db.Sys_User
                        where x.RoleId.Contains(role)
                        orderby x.UserId
                        select x).Distinct().ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string getSignatureName(string userId)
        {
            string userName = string.Empty;
            var getUser = UserService.GetUserByUserId(userId);
            if (getUser != null)
            {
                userName = getUser.UserName;
                if (!string.IsNullOrEmpty(getUser.SignatureUrl))
                {
                    string url = Funs.RootPath + getUser.SignatureUrl;
                    FileInfo info = new FileInfo(url);
                    if (info.Exists)
                    {
                        userName = "<img width='90' height='35' src='" + (Funs.SGGLUrl + getUser.SignatureUrl).Replace('\\', '/') + "'></img>";
                    }
                }
            }
            return userName;
        }



        /// <summary>
        /// ���ݵ�λId���Ż�ȡ����ʩ�����û�����ѡ��
        /// </summary>
        /// <returns></returns>
        public static List<Model.Sys_User> GetUserListByUnitIdDepartId(string unitId, string DepartId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                List<Model.Sys_User> list = new List<Model.Sys_User>();
                list = (from x in db.Sys_User
                        where x.UnitId == unitId && x.DepartId == DepartId
                        orderby x.UserName
                        select x).ToList();
                return list;
            }
        }

        /// <summary>
        /// �û�������
        /// </summary>
        /// <param name="dropName">����������</param>
        /// <param name="isShowPlease">�Ƿ���ʾ��ѡ��</param>
        public static void InitUserUnitIdDepartIdDropDownList(FineUIPro.DropDownList dropName, string unitId, string DepartId, bool isShowPlease)
        {
            dropName.DataValueField = "UserId";
            dropName.DataTextField = "UserName";
            dropName.DataSource = BLL.UserService.GetUserListByUnitIdDepartId(unitId, DepartId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        /// ����ʩ�������û�������
        /// </summary>
        /// <param name="dropName"></param>
        /// <param name="isShowPlease"></param>
        public static void InitSGBUser(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "Value";
            dropName.DataTextField = "Text";
            dropName.DataSource = GetSGBUserList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        /// ��ȡʩ�����û�
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static ListItem[] GetSGBUserList()
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var user = (from x in db.Sys_User
                            where x.IsPost == true && x.UnitId == Const.UnitId_SEDIN && x.DepartId == Const.Depart_constructionId
                            orderby x.UserId
                            select x).ToList();
                ListItem[] lis = new ListItem[user.Count()];
                for (int i = 0; i < user.Count(); i++)
                {
                    lis[i] = new ListItem(user[i].UserName ?? "", user[i].UserId.ToString());
                }
                return lis;
            }
        }

        /// <summary>
        ///�����û�����ȡ�û���Ϣ
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static Model.Sys_User GetUserByUserName(string userName)
        {
            return Funs.DB.Sys_User.FirstOrDefault(e => e.UserName == userName);
        }

        /// <summary>
        /// ���ݽ�ɫid�͵�λId��ȡ�����û�����ѡ�� 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static List<Model.Sys_User> GetUserListByRoleIDAndUnitId(string unitId, string RoleId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                List<Model.Sys_User> list = new List<Model.Sys_User>();
                if (!string.IsNullOrEmpty(RoleId))
                {
                    if (!string.IsNullOrEmpty(unitId))
                    {
                        list = (from x in db.Sys_User
                                where x.RoleId == RoleId && x.UnitId == unitId
                                orderby x.UserName
                                select x).ToList();
                    }
                    else
                    {
                        list = (from x in db.Sys_User
                                where x.RoleId == RoleId
                                orderby x.UserName
                                select x).ToList();
                    }

                }
                else
                {
                    list = (from x in db.Sys_User
                            where x.UnitId == unitId
                            orderby x.UserName
                            select x).ToList();
                }
                return list;
            }
        }

        /// <summary>
        /// �û�������
        /// </summary>
        /// <param name="dropName">����������</param>
        /// <param name="RoleId">��ɫid</param>
        /// <param name="unitId">��λid</param>
        /// <param name="isShowPlease">�Ƿ���ʾ��ѡ��</param>
        public static void InitUserRoleIdUnitIdDropDownList(FineUIPro.DropDownList dropName, string unitId, string RoleId, bool isShowPlease)
        {
            dropName.DataValueField = "UserId";
            dropName.DataTextField = "UserName";
            dropName.DataSource = BLL.UserService.GetUserListByRoleIDAndUnitId(unitId, RoleId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        /// <summary>
        /// �û�������
        /// </summary>
        /// <param name="dropName">����������</param>
        /// <param name="RoleId">��ɫid</param>
        /// <param name="unitId">��λid</param>
        /// <param name="isShowPlease">�Ƿ���ʾ��ѡ��</param>
        public static void InitUserRoleIdUnitIdDropDownList(FineUIPro.DropDownList dropName, string unitId, string RoleId1, string RoleId2, bool isShowPlease)
        {
            dropName.DataValueField = "UserId";
            dropName.DataTextField = "UserName";
            var model1 = BLL.UserService.GetUserListByRoleIDAndUnitId(unitId, RoleId1);
            var model2 = BLL.UserService.GetUserListByRoleIDAndUnitId(unitId, RoleId2);
            var model3 = model1.Concat(model2).ToList();
            dropName.DataSource = model3;
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        /// �������֤�Ż�ȡ�û���Ϣ
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static Model.Sys_User GetUserByIdentityCard(string IdentityCard)
        {
            return Funs.DB.Sys_User.FirstOrDefault(e => e.IdentityCard == IdentityCard);
        }
    }
}
