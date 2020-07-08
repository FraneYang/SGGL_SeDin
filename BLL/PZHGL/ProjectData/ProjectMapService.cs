using System.Linq;

namespace BLL
{
    /// <summary>
    /// 项目地图
    /// </summary>
    public static class ProjectMapService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取项目地图
        /// </summary>
        /// <param name="ProjectMapId"></param>
        /// <returns></returns>
        public static Model.InformationProject_ProjectMap GetProjectMapById(string ProjectMapId)
        {
            return new Model.SGGLDB(Funs.ConnString).InformationProject_ProjectMap.FirstOrDefault(e => e.ProjectMapId == ProjectMapId);
        }

        /// <summary>
        /// 增加地图信息
        /// </summary>
        /// <param name="personQuality">地图实体</param>
        public static void AddProjectMap(Model.InformationProject_ProjectMap ProjectMap)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InformationProject_ProjectMap newProjectMap = new Model.InformationProject_ProjectMap
            {
                ProjectMapId = ProjectMap.ProjectMapId,
                ProjectId = ProjectMap.ProjectId,
                Title = ProjectMap.Title,
                ContentDef = ProjectMap.ContentDef,
                UploadDate = ProjectMap.UploadDate,
                MapType = ProjectMap.MapType,
                AttachUrl = ProjectMap.AttachUrl,
                CompileMan = ProjectMap.CompileMan
            };
            db.InformationProject_ProjectMap.InsertOnSubmit(newProjectMap);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改项目地图
        /// </summary>
        /// <param name="ProjectMap"></param>
        public static void UpdateProjectMap(Model.InformationProject_ProjectMap ProjectMap)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InformationProject_ProjectMap newProjectMap = db.InformationProject_ProjectMap.FirstOrDefault(e => e.ProjectMapId == ProjectMap.ProjectMapId);
            if (newProjectMap != null)
            {
                //newProjectMap.ProjectId = ProjectMap.ProjectId;
                newProjectMap.Title = ProjectMap.Title;
                newProjectMap.ContentDef = ProjectMap.ContentDef;
                newProjectMap.UploadDate = ProjectMap.UploadDate;
                newProjectMap.MapType = ProjectMap.MapType;
                newProjectMap.AttachUrl = ProjectMap.AttachUrl;
                newProjectMap.CompileMan = ProjectMap.CompileMan;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除项目地图
        /// </summary>
        /// <param name="ProjectMapId"></param>
        public static void deleteProjectMapById(string ProjectMapId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InformationProject_ProjectMap ProjectMap = db.InformationProject_ProjectMap.FirstOrDefault(e => e.ProjectMapId == ProjectMapId);
            if (ProjectMap != null)
            {
                BLL.CommonService.DeleteAttachFileById(ProjectMap.ProjectMapId);  ///删除附件
                BLL.UploadFileService.DeleteFile(Funs.RootPath, ProjectMap.AttachUrl);  ///删除附件

                db.InformationProject_ProjectMap.DeleteOnSubmit(ProjectMap);
                db.SubmitChanges();
            }
        }
    }
}