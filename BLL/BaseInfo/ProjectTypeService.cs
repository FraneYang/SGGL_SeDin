﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class ProjectTypeService
    {

        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Base_ProjectType GetProjectTypeById(string ProjectTypeId)
        {
            return Funs.DB.Base_ProjectType.FirstOrDefault(e => e.ProjectTypeId == ProjectTypeId);
        }

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Base_ProjectType GetProjectTypeByName(string ProjectTypeName)
        {
            return Funs.DB.Base_ProjectType.FirstOrDefault(e => e.ProjectTypeName == ProjectTypeName);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="?"></param>
        public static void AddProjectType(Model.Base_ProjectType ProjectType)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_ProjectType newProjectType = new Model.Base_ProjectType
            {
                ProjectTypeId = ProjectType.ProjectTypeId,
                ProjectTypeCode = ProjectType.ProjectTypeCode,
                ProjectTypeName = ProjectType.ProjectTypeName,
                Remark = ProjectType.Remark
            };

            db.Base_ProjectType.InsertOnSubmit(newProjectType);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void UpdateProjectType(Model.Base_ProjectType ProjectType)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_ProjectType newProjectType = db.Base_ProjectType.FirstOrDefault(e => e.ProjectTypeId == ProjectType.ProjectTypeId);
            if (newProjectType != null)
            {
                newProjectType.ProjectTypeCode = ProjectType.ProjectTypeCode;
                newProjectType.ProjectTypeName = ProjectType.ProjectTypeName;
                newProjectType.Remark = ProjectType.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="ProjectTypeId"></param>
        public static void DeleteProjectTypeById(string ProjectTypeId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_ProjectType ProjectType = db.Base_ProjectType.FirstOrDefault(e => e.ProjectTypeId == ProjectTypeId);
            {
                db.Base_ProjectType.DeleteOnSubmit(ProjectType);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取类别下拉项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_ProjectType> GetProjectTypeList()
        {
            var list = (from x in Funs.DB.Base_ProjectType orderby x.ProjectTypeCode select x).ToList();
            return list;
        }
        /// <summary>
        /// 获取单位类型下拉选项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_ProjectType> GetProjectTypeDropDownList()
        {
            var list = (from x in Funs.DB.Base_ProjectType orderby x.ProjectTypeCode select x).ToList();
            return list;
        }

        #region 单位类型表下拉框
        /// <summary>
        ///  单位类型表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitProjectTypeDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "ProjectTypeId";
            dropName.DataTextField = "ProjectTypeName";
            dropName.DataSource = BLL.ProjectTypeService.GetProjectTypeDropDownList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}
