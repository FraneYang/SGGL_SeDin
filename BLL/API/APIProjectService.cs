using EmitMapper;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public static class APIProjectService
    {
        /// <summary>
        /// 根据userId获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<Model.ProjectItem> geProjectsByUserId(string userId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                List<Model.Base_Project> projects = new List<Model.Base_Project>();
                if (CommonService.IsThisUnitLeaderOfficeOrManage(userId))
                {
                    projects = (from x in db.Base_Project
                                where x.ProjectState == null || x.ProjectState == BLL.Const.ProjectState_1
                                select x).ToList();
                }
                else
                {
                    projects = (from x in db.Project_ProjectUser
                                join y in db.Base_Project on x.ProjectId equals y.ProjectId
                                where x.UserId == userId && (y.ProjectState == null || y.ProjectState == BLL.Const.ProjectState_1)
                                select y).ToList();
                }

                return ObjectMapperManager.DefaultInstance.GetMapper<List<Model.Base_Project>, List<Model.ProjectItem>>().Map(projects);
            }
        }

        /// <summary>
        /// 根据userId获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<Model.ProjectItem> getALLProjectsByUserId(string userId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var projects = (from x in db.Project_ProjectUser
                                join y in db.Base_Project on x.ProjectId equals y.ProjectId
                                where x.UserId == userId
                                orderby y.ProjectCode
                                select y).ToList();

                return ObjectMapperManager.DefaultInstance.GetMapper<List<Model.Base_Project>, List<Model.ProjectItem>>().Map(projects);
            }
        }

        /// <summary>
        /// 根据userId获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static Model.ProjectItem getProjectByProjectId(string projectId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getproject = db.Base_Project.FirstOrDefault(x => x.ProjectId == projectId);
                return ObjectMapperManager.DefaultInstance.GetMapper<Model.Base_Project, Model.ProjectItem>().Map(getproject);
            }
        }
    }
}
