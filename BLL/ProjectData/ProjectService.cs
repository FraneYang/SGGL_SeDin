namespace BLL
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
        ///获取项目信息
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
            };
            db.Base_Project.InsertOnSubmit(newProject);
            db.SubmitChanges();
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

                db.SubmitChanges();
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
            /// 获取角色类型
            string roleType =RoleService.GetRoleTypeByUserId(userId);
            if (roleType == Const.RoleType_2 || roleType == Const.RoleType_3 || userId == Const.sysglyId || userId == Const.hfnbdId || userId == Const.sedinId)
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
                        where x.ProjectState == null || x.ProjectState == BLL.Const.ProjectState_1 && y.UserId ==userId
                        orderby x.ProjectCode descending
                        select x).ToList();
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
        public static void InitAllProjectShortNameDropDownList(FineUIPro.DropDownList dropName,string userId, bool isShowPlease)
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
        #endregion
    }
}