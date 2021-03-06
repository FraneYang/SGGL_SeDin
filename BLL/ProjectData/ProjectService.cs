﻿namespace BLL
{
    using System.Collections.Generic;
    using System.Linq;
    using Model;
    using System;

    public static class ProjectService
    {
        public static SGGLDB db = Funs.DB;

        /// <summary>
        ///获取项目信息
        /// </summary>
        /// <returns></returns>
        public static Model.Base_Project GetProjectByProjectId(string projectId)
        {
            return Funs.DB.Base_Project.FirstOrDefault(e => e.ProjectId == projectId);
        }

        /// <summary>
        ///根据ID获取项目名称
        /// </summary>
        /// <returns></returns>
        public static string GetProjectNameByProjectId(string projectId)
        {
            string name = string.Empty;
            var project = Funs.DB.Base_Project.FirstOrDefault(e => e.ProjectId == projectId);
            if (project != null)
            {
                name = project.ProjectName;
            }
            return name;
        }

        /// <summary>
        ///根据ID获取项目编号
        /// </summary>
        /// <returns></returns>
        public static string GetProjectCodeByProjectId(string projectId)
        {
            string name = string.Empty;
            var project = Funs.DB.Base_Project.FirstOrDefault(e => e.ProjectId == projectId);
            if (project != null)
            {
                name = project.ProjectCode;
            }
            return name;
        }
        public static Model.Base_Project GetProjectByProjectShortName(string name)
        {
            return Funs.DB.Base_Project.FirstOrDefault(e => e.ShortName == name);
        }

        public static Model.Base_Project GetProjectByProjectName(string name)
        {
            return Funs.DB.Base_Project.FirstOrDefault(e => e.ProjectName == name);
        }

        /// <summary>
        ///获取项目简称
        /// </summary>
        /// <returns></returns>
        public static string GetShortNameByProjectId(string projectId)
        {
            string name = string.Empty;
            var project = Funs.DB.Base_Project.FirstOrDefault(e => e.ProjectId == projectId);
            if (project != null)
            {
                name = project.ShortName;
            }
            return name;
        }
        /// <summary>
        /// 增加项目信息
        /// </summary>
        /// <returns></returns>
        public static void AddProject(Model.Base_Project project)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_Project newProject = new Base_Project
            {
                ProjectId = project.ProjectId,
                ProjectCode = project.ProjectCode,
                ProjectName = project.ProjectName,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                ProjectAddress = project.ProjectAddress,
                ContractNo = project.ContractNo,
                WorkRange = project.WorkRange,
                Duration = project.Duration,
                ShortName = project.ShortName,
                ProjectType = project.ProjectType,
                PostCode = project.PostCode,
                Remark = project.Remark,
                ProjectState = project.ProjectState,
                IsUpTotalMonth = project.IsUpTotalMonth,
                UnitId = project.UnitId,
                ProjectMainPerson = project.ProjectMainPerson,
                ProjectLiaisonPerson = project.ProjectLiaisonPerson,
                IsForeign = project.IsForeign,
                FromProjectId = project.FromProjectId,
                MapCoordinates = project.MapCoordinates,
                ProjectMoney = project.ProjectMoney,
                ConstructionMoney = project.ConstructionMoney,
                Telephone = project.Telephone,
                Country = project.Country,
                Province = project.Province,
                City = project.City,
                EnglishRemark=project.EnglishRemark,

            };
            db.Base_Project.InsertOnSubmit(newProject);
            db.SubmitChanges();
            HSEDataCollectService.ProjectHSEDataCollectSubmission(newProject);
        }

        /// <summary>
        ///修改项目信息 
        /// </summary>
        /// <param name="project"></param>
        public static void UpdateProject(Model.Base_Project project)
        {
            SGGLDB db = Funs.DB;
            Base_Project newProject = db.Base_Project.FirstOrDefault(e => e.ProjectId == project.ProjectId);
            if (newProject != null)
            {
                newProject.ProjectCode = project.ProjectCode;
                newProject.ProjectName = project.ProjectName;
                newProject.StartDate = project.StartDate;
                newProject.EndDate = project.EndDate;
                newProject.ProjectAddress = project.ProjectAddress;
                newProject.ShortName = project.ShortName;
                newProject.ContractNo = project.ContractNo;
                newProject.WorkRange = project.WorkRange;
                newProject.Duration = project.Duration;
                newProject.ProjectType = project.ProjectType;
                newProject.PostCode = project.PostCode;
                newProject.Remark = project.Remark;
                newProject.ProjectState = project.ProjectState;
                newProject.IsUpTotalMonth = project.IsUpTotalMonth;
                newProject.UnitId = project.UnitId;
                newProject.ProjectMainPerson = project.ProjectMainPerson;
                newProject.ProjectLiaisonPerson = project.ProjectLiaisonPerson;
                newProject.IsForeign = project.IsForeign;
                newProject.FromProjectId = project.FromProjectId;
                newProject.MapCoordinates = project.MapCoordinates;
                newProject.ProjectMoney = project.ProjectMoney;
                newProject.ConstructionMoney = project.ConstructionMoney;
                newProject.Telephone = project.Telephone;
                newProject.Country = project.Country;
                newProject.Province = project.Province;
                newProject.City = project.City;
                newProject.EnglishRemark = project.EnglishRemark;
                db.SubmitChanges();
                HSEDataCollectService.ProjectHSEDataCollectSubmission(newProject);
            }
        }

        /// <summary>
        /// 根据项目Id删除一个项目信息
        /// </summary>
        /// <param name="projectId"></param>
        public static void DeleteProject(string projectId)
        {
            SGGLDB db = Funs.DB;
            Base_Project project = db.Base_Project.FirstOrDefault(e => e.ProjectId == projectId);
            if (project != null)
            {
                db.Base_Project.DeleteOnSubmit(project);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取施工中项目集合
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_Project> GetProjectWorkList()
        {
            var list = (from x in Funs.DB.Base_Project
                        where x.ProjectState == null || x.ProjectState == BLL.Const.ProjectState_1
                        orderby x.ProjectCode descending
                        select x).ToList();
            return list;
        }

        /// <summary>
        /// 获取项目下拉选项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_Project> GetProjectDropDownListByState(string state)
        {
            if (state == BLL.Const.ProjectState_1)  //施工
            {
                var list = (from x in Funs.DB.Base_Project
                            where x.ProjectState == state || x.ProjectState == null
                            orderby x.ProjectCode descending
                            select x).ToList();
                return list;
            }
            else
            {
                var list = (from x in Funs.DB.Base_Project
                            where x.ProjectState == state
                            orderby x.ProjectCode descending
                            select x).ToList();
                return list;
            }
        }

        /// <summary>
        /// 获取项目下拉选项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_Project> GetAllProjectDropDownList()
        {
            var list = (from x in Funs.DB.Base_Project
                        orderby x.ProjectCode descending
                        select x).ToList();
            return list;
        }


        /// <summary>
        /// 获取某类型下项目下拉选项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_Project> GetProjectByProjectTypeDropDownList(string projectType)
        {
            var list = (from x in Funs.DB.Base_Project
                        where x.ProjectType == projectType
                        orderby x.ProjectCode descending
                        select x).ToList();
            return list;
        }

        /// <summary>
        /// 获取userId参与项目下拉框
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_Project> GetProjectByUserIdDropDownList(string userId)
        {
            if (userId == Const.sysglyId || userId == Const.hfnbdId || userId == Const.sedinId)
            {
                return (from x in Funs.DB.Base_Project
                        where x.ProjectState == null || x.ProjectState == BLL.Const.ProjectState_1
                        orderby x.ProjectCode descending
                        select x).ToList();
            }
            var getUser = UserService.GetUserByUserId(userId);
            if (getUser != null)
            {
                List<string> roleidList = Funs.GetStrListByStr(getUser.RoleId,',');
                /// 获取角色类型
                var getRoleP = Funs.DB.Sys_RolePower.FirstOrDefault(x => roleidList.Contains(x.RoleId) && x.IsOffice == false);
                if (getRoleP != null)
                {
                    return (from x in Funs.DB.Base_Project
                            where x.ProjectState == null || x.ProjectState == BLL.Const.ProjectState_1
                            orderby x.ProjectCode descending
                            select x).ToList();
                }
                else
                {
                    return (from x in Funs.DB.Base_Project
                            join y in Funs.DB.Project_ProjectUser on x.ProjectId equals y.ProjectId
                            where x.ProjectState == null || x.ProjectState == BLL.Const.ProjectState_1 && y.UserId == userId
                            orderby x.ProjectCode descending
                            select x).ToList();
                }
            }
            else
            {
                return null;
            }
        }

        #region 项目表下拉框
        /// <summary>
        ///  项目表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitProjectDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "ProjectId";
            dropName.DataTextField = "ProjectName";
            dropName.DataSource = BLL.ProjectService.GetProjectWorkList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        ///  项目表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitAllProjectDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "ProjectId";
            dropName.DataTextField = "ProjectName";
            var projectlist = BLL.ProjectService.GetAllProjectDropDownList();
            dropName.DataSource = projectlist;
            dropName.DataBind();
            if (projectlist.Count() == 0)
            {
                isShowPlease = true;
            }
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dropName"></param>
        /// <param name="userId"></param>
        /// <param name="isShowPlease"></param>
        public static void InitAllProjectShortNameDropDownList(FineUIPro.DropDownList dropName, string userId, bool isShowPlease)
        {
            dropName.DataValueField = "ProjectId";
            dropName.DataTextField = "ShortName";
            var projectlist = GetProjectByUserIdDropDownList(userId);
            dropName.DataSource = projectlist;
            dropName.DataBind();
            if (projectlist.Count() == 0)
            {
                isShowPlease = true;
            }
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        ///  某类型下项目表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitProjectByProjectTypeDropDownList(FineUIPro.DropDownList dropName, string projectType, bool isShowPlease)
        {
            dropName.DataValueField = "ProjectId";
            dropName.DataTextField = "ProjectName";
            var projectlist = BLL.ProjectService.GetProjectByProjectTypeDropDownList(projectType);
            dropName.DataSource = projectlist;
            dropName.DataBind();
            if (projectlist.Count() == 0)
            {
                isShowPlease = true;
            }
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion

        #region 获取项目经理、施工经理、安全经理
        /// <summary>
        /// 项目经理
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static string GetProjectManagerName(string projectId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                string name = string.Empty;
                if (projectId != null)
                {
                    name = (from x in db.Base_Project
                            join y in db.Project_ProjectUser on x.ProjectId equals y.ProjectId
                            join z in db.Sys_User on y.UserId equals z.UserId
                            where x.ProjectId == projectId && y.RoleId.Contains(BLL.Const.ProjectManager)
                            select z.UserName).FirstOrDefault();
                }
                return name;
            }
        }

        /// <summary>
        /// 施工经理
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static string GetConstructionManagerName(string projectId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                string name = string.Empty;
                if (projectId != null)
                {
                    name = (from x in db.Base_Project
                            join y in db.Project_ProjectUser on x.ProjectId equals y.ProjectId
                            join z in db.Sys_User on y.UserId equals z.UserId
                            where x.ProjectId == projectId && y.RoleId.Contains(BLL.Const.ConstructionManager)
                            select z.UserName).FirstOrDefault();
                }
                return name;
            }
        }

        /// <summary>
        /// 安全经理
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static string GetHSSEManagerName(string projectId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                string name = string.Empty;
                if (projectId != null)
                {
                    name = (from x in db.Base_Project
                            join y in db.Project_ProjectUser on x.ProjectId equals y.ProjectId
                            join z in db.Sys_User on y.UserId equals z.UserId
                            where x.ProjectId == projectId && y.RoleId.Contains(BLL.Const.HSSEManager)
                            select z.UserName).FirstOrDefault();
                }
                return name;
            }
        }
        /// <summary>
        /// 获取该项目下，角色名称
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public static string GetRoleName(string projectId, string RoleId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                string name = string.Empty;
                if (projectId != null)
                {
                    name = (from x in db.Base_Project
                            join y in db.Project_ProjectUser on x.ProjectId equals y.ProjectId
                            join z in db.Sys_User on y.UserId equals z.UserId
                            where x.ProjectId == projectId && y.RoleId.Contains(RoleId)
                            select z.UserName).FirstOrDefault();
                }
                return name;
            }
        }

        /// <summary>
        /// 获取该项目下，角色ID
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public static string GetRoleID(string projectId, string RoleId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                string UserId = string.Empty;
                if (projectId != null)
                {
                    UserId = (from x in db.Base_Project
                            join y in db.Project_ProjectUser on x.ProjectId equals y.ProjectId
                            join z in db.Sys_User on y.UserId equals z.UserId
                            where x.ProjectId == projectId && y.RoleId.Contains(RoleId)
                            select z.UserId).FirstOrDefault();
                }
                return UserId;
            }
        }

        #endregion

        /// <summary>
        ///  获取项目各单位类型单位名称
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitType"></param>
        /// <returns></returns>
        public static string getProjectUnitNameByUnitType(string projectId, string unitType)
        {
            string unitName = string.Empty;
            if (!string.IsNullOrEmpty(projectId))
            {
                var getUnitName = from x in Funs.DB.Project_ProjectUnit
                                  join y in Funs.DB.Base_Unit on x.UnitId equals y.UnitId
                                  where x.ProjectId == projectId.ToString() && x.UnitType == unitType
                                  select y.UnitName;
                if (getUnitName.Count() > 0)
                {
                    unitName = Funs.GetStringByArray(getUnitName.ToArray());
                }
            }
            return unitName;
        }

        /// <summary>
        /// 获取所有项目号
        /// </summary>
        /// <param name="dropName"></param>
        /// <param name="isShowPlease"></param>
        public static void InitAllProjectCodeDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "ProjectId";
            dropName.DataTextField = "ProjectCode";
            var projectlist = BLL.ProjectService.GetAllProjectDropDownList();
            dropName.DataSource = projectlist;
            dropName.DataBind();
            if (projectlist.Count() == 0)
            {
                isShowPlease = true;
            }
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        /// 获取该单位本部角色名称
        /// </summary>
        /// <param name="UnitId"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public static string GetOfficeRoleName(string UnitId, string RoleId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                string name = string.Empty;
                if (UnitId != null)
                {
                    name = (from x in db.Sys_User
                            where x.UnitId == UnitId && x.RoleId.Contains(RoleId)
                            select x.UserName).FirstOrDefault();
                }
                return name;
            }
        }
        /// <summary>
        /// 获取该单位本部角色ID
        /// </summary>
        /// <param name="UnitId"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public static string GetOfficeRoleID(string UnitId, string RoleId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                string UserId = string.Empty;
                if (UnitId != null)
                {
                    UserId = (from x in db.Sys_User
                              where x.UnitId == UnitId && x.RoleId.Contains(RoleId)
                              select x.UserId).FirstOrDefault();
                }
                return UserId;
            }
        }

        public static Model.Base_Project GetProjectByProjectCode(string projectCode)
        {
            return Funs.DB.Base_Project.FirstOrDefault(e => e.ProjectCode == projectCode);
        }
    }
}