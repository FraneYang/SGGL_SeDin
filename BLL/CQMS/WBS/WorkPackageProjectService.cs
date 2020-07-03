using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class WorkPackageProjectService
    {
        /// <summary>
        ///// 添加分部工程
        /// </summary>
        /// <param name="UnitWork"></param>
        public static void AddWorkPackageProject(Model.WBS_WorkPackageProject workPack)
        {
            Model.SGGLDB db = Funs.DB;
            Model.WBS_WorkPackageProject newPack = new Model.WBS_WorkPackageProject();
            newPack.WorkPackageCode = workPack.WorkPackageCode;
            newPack.ProjectId = workPack.ProjectId;
            newPack.SuperWorkPack = workPack.SuperWorkPack;
            newPack.PackageCode = workPack.PackageCode;
            newPack.PackageContent = workPack.PackageContent;
            newPack.IsChild = workPack.IsChild;
            newPack.ProjectType = workPack.ProjectType;
            db.WBS_WorkPackageProject.InsertOnSubmit(newPack);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改子分部工程
        /// </summary>
        /// <param name="UnitWork"></param>
        public static void UpdateWorkPackageProject(Model.WBS_WorkPackageProject workPack)
        {
            Model.SGGLDB db = Funs.DB;
            Model.WBS_WorkPackageProject newPack = db.WBS_WorkPackageProject.First(e => e.WorkPackageCode == workPack.WorkPackageCode);
            newPack.WorkPackageCode = workPack.WorkPackageCode;
            newPack.IsChild = workPack.IsChild;
            newPack.PackageContent = workPack.PackageContent;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据分部分项编号和项目id获取项目分部分项内容
        /// </summary>
        /// <param name="workPackageCode"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static Model.WBS_WorkPackageProject GetWorkPackageProjectByWorkPackageCode(string workPackageCode, string projectId)
        {
            return BLL.Funs.DB.WBS_WorkPackageProject.FirstOrDefault(x => x.WorkPackageCode == workPackageCode && x.ProjectId == projectId);
        }

        /// <summary>
        /// 根据项目id判断是否存在项目分部分项内容
        /// </summary>
        /// <param name="workPackageCode"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static bool IsExitWorkPackageProject(string projectId)
        {
            return BLL.Funs.DB.WBS_WorkPackageProject.FirstOrDefault(x => x.ProjectId == projectId) != null;
        }

        /// <summary>
        /// 根据分部分项编号获取子级项目分部分项信息集合
        /// </summary>
        /// <param name="workPackageCode"></param>
        /// <returns></returns>
        public static List<Model.WBS_WorkPackageProject> GetWorkPackageProjectsBySuperWorkPack(string workPackageCode, string projectId)
        {
            return (from x in BLL.Funs.DB.WBS_WorkPackageProject where x.SuperWorkPack == workPackageCode && x.ProjectId == projectId select x).ToList();
        }

        /// <summary>
        /// 根据分部工程编号删除一个项目分部工程信息
        /// </summary>
        /// <param name="UnitWorkId"></param>
        public static void DeleteWorkPackageProject(string workPackageCode, string projectId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.WBS_WorkPackageProject delWorkPack = db.WBS_WorkPackageProject.First(e => e.WorkPackageCode == workPackageCode && e.ProjectId == projectId);
            db.WBS_WorkPackageProject.DeleteOnSubmit(delWorkPack);
            db.SubmitChanges();
        }

        /// <summary>
        /// 是否存在分部分项工程
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-存在，false-不存在</returns>
        public static bool IsExistWorkPackageProjectName(string supWorkPack, string packageContent, string workPackageCode, string projectId)
        {
            var q = from x in Funs.DB.WBS_WorkPackageProject where x.SuperWorkPack == supWorkPack && x.PackageContent == packageContent && x.WorkPackageCode != workPackageCode && x.ProjectId == projectId select x;
            if (q.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据项目编号和工程类型获取第一级分部
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.WBS_WorkPackageProject> GetWorkPackageProjects1ByProjectIdAndProjectType(string projectId, string projectType)
        {
            return (from x in BLL.Funs.DB.WBS_WorkPackageProject where x.ProjectId == projectId && x.ProjectType == projectType && x.SuperWorkPack == null select x).ToList();
        }

        /// <summary>
        /// 根据项目编号和工程类型获取第二、三级分部
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.WBS_WorkPackageProject> GetWorkPackageProjects2ByWorkPackageCode(string workPackageCode, string projectId)
        {
            return (from x in BLL.Funs.DB.WBS_WorkPackageProject where x.ProjectId == projectId && x.SuperWorkPack == workPackageCode select x).ToList();
        }
    }
}
