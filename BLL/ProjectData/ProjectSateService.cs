using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 项目状态及软件关闭
    /// </summary>
    public static class ProjectSateService
    {
        public static Model.SGGLDB db = Funs.DB; 

        /// <summary>
        /// 根据主键获取项目状态及软件关闭信息
        /// </summary>
        /// <param name="projectStateId"></param>
        /// <returns></returns>
        public static Model.Base_ProjectSate GetProjectSateById(string projectStateId)
        {
            return Funs.DB.Base_ProjectSate.FirstOrDefault(e => e.ProjectStateId == projectStateId);
        }

        /// <summary>
        /// 添加项目状态及软件关闭信息
        /// </summary>
        /// <param name="ProjectSate"></param>
        public static void AddProjectSate(Model.Base_ProjectSate ProjectSate)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_ProjectSate newProjectSate = new Model.Base_ProjectSate
            {
                ProjectStateId = ProjectSate.ProjectStateId,
                ProjectId = ProjectSate.ProjectId,
                ProjectState = ProjectSate.ProjectState,
                OldProjectState = ProjectSate.OldProjectState,
                CompileMan = ProjectSate.CompileMan,
                CompileDate = ProjectSate.CompileDate,
                ShutdownDate = ProjectSate.ShutdownDate,
                States = ProjectSate.States
            };
            db.Base_ProjectSate.InsertOnSubmit(newProjectSate);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改项目状态及软件关闭信息
        /// </summary>
        /// <param name="ProjectSate"></param>
        public static void UpdateProjectSate(Model.Base_ProjectSate ProjectSate)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_ProjectSate newProjectSate = db.Base_ProjectSate.FirstOrDefault(e => e.ProjectStateId == ProjectSate.ProjectStateId);
            if (newProjectSate != null)
            {
                newProjectSate.ProjectState = ProjectSate.ProjectState;
                newProjectSate.ShutdownDate = ProjectSate.ShutdownDate;
                newProjectSate.States = ProjectSate.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除项目状态及软件关闭信息
        /// </summary>
        /// <param name="projectStateId"></param>
        public static void DeleteProjectSateById(string projectStateId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_ProjectSate ProjectSate = db.Base_ProjectSate.FirstOrDefault(e => e.ProjectStateId == projectStateId);
            if (ProjectSate != null)
            {
                ////删除审核流程表
                BLL.CommonService.DeleteFlowOperateByID(ProjectSate.ProjectStateId);   
                db.Base_ProjectSate.DeleteOnSubmit(ProjectSate);
                db.SubmitChanges();
            }
        }
    }
}
