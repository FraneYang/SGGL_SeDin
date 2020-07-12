namespace BLL
{
    using System.Collections.Generic;
    using System.Linq;
    using Model;

    public static class ProjectUnitService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        ///获取项目单位信息
        /// </summary>
        /// <returns></returns>
        public static Model.Project_ProjectUnit GetProjectUnitById(string projectUnitId)
        {
            return new Model.SGGLDB(Funs.ConnString).Project_ProjectUnit.FirstOrDefault(e => e.ProjectUnitId == projectUnitId);
        }

        /// <summary>
        ///获取项目单位信息
        /// </summary>
        /// <returns></returns>
        public static Model.Project_ProjectUnit GetProjectUnitByUnitIdProjectId(string projectId, string unitId)
        {
            return new Model.SGGLDB(Funs.ConnString).Project_ProjectUnit.FirstOrDefault(e => e.ProjectId == projectId && e.UnitId == unitId);
        }

        /// <summary>
        ///获取项目单位信息
        /// </summary>
        /// <returns></returns>
        public static List<Model.Project_ProjectUnit> GetProjectUnitListByProjectId(string projectId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))            {
                return (from x in db.Project_ProjectUnit
                        join y in db.Base_Unit on x.UnitId equals y.UnitId
                        where x.ProjectId == projectId
                        orderby x.UnitType, y.UnitCode
                        descending
                        select x).ToList();
            }
        }

        /// <summary>
        /// 根据项目及单位类型获取单位信息
        /// </summary>
        /// <returns></returns>
        public static List<Model.Project_ProjectUnit> GetProjectUnitListByProjectIdUnitType(string projectId, string unitType)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))            {
                return (from x in db.Project_ProjectUnit
                        join y in db.Base_Unit on x.UnitId equals y.UnitId
                        where x.ProjectId == projectId && x.UnitType == unitType
                        orderby x.UnitType, y.UnitCode descending
                        select x).ToList();
            }
        }

        /// <summary>
        /// 增加项目单位信息
        /// </summary>
        /// <returns></returns>
        public static void AddProjectUnit(Project_ProjectUnit projectUnit)
        {
            SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Project_ProjectUnit newProjectUnit = new Project_ProjectUnit
            {
                ProjectUnitId = SQLHelper.GetNewID(typeof(Model.Project_ProjectUnit)),
                ProjectId = projectUnit.ProjectId,
                UnitId = projectUnit.UnitId,
                UnitType = projectUnit.UnitType,
                InTime = projectUnit.InTime,
                OutTime = projectUnit.OutTime,
                PlanCostA = projectUnit.PlanCostA,
                PlanCostB = projectUnit.PlanCostB,
                ContractRange = projectUnit.ContractRange
            };
            db.Project_ProjectUnit.InsertOnSubmit(newProjectUnit);
            db.SubmitChanges();
        }

        /// <summary>
        ///修改项目单位信息 
        /// </summary>
        /// <param name="projectUnit"></param>
        public static void UpdateProjectUnit(Model.Project_ProjectUnit projectUnit)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Project_ProjectUnit newProjectUnit = db.Project_ProjectUnit.FirstOrDefault(e => e.ProjectUnitId == projectUnit.ProjectUnitId);
            if (newProjectUnit != null)
            {
                newProjectUnit.UnitType = projectUnit.UnitType;
                newProjectUnit.InTime = projectUnit.InTime;
                newProjectUnit.OutTime = projectUnit.OutTime;
                newProjectUnit.PlanCostA = projectUnit.PlanCostA;
                newProjectUnit.PlanCostB = projectUnit.PlanCostB;
                newProjectUnit.ContractRange = projectUnit.ContractRange;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据项目单位Id删除一个项目单位信息
        /// </summary>
        /// <param name="projectUnitId"></param>
        public static void DeleteProjectProjectUnitById(string projectUnitId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Project_ProjectUnit delProjectUnit = db.Project_ProjectUnit.FirstOrDefault(e => e.ProjectUnitId == projectUnitId);
            if (delProjectUnit != null)
            {
                db.Project_ProjectUnit.DeleteOnSubmit(delProjectUnit);
                db.SubmitChanges();
            }
        }

        /// <summary>
        ///获取当前人单位是否 施工工分包单位且非本单位
        /// </summary>
        /// <returns></returns>
        public static bool GetProjectUnitTypeByProjectIdUnitId(string projectId, string unitId)
        {
            bool isShow = false;
            if (unitId != Const.UnitId_SEDIN)
            {
                var pUnit = new Model.SGGLDB(Funs.ConnString).Project_ProjectUnit.FirstOrDefault(e => e.ProjectId == projectId && e.UnitId == unitId);
                if (pUnit != null)
                {
                    if (pUnit.UnitType == Const.ProjectUnitType_2 || pUnit.UnitType == Const.ProjectUnitType_0)
                    {
                        isShow = true;
                    }
                }
            }

            return isShow;
        }

        #region 项目类型单位表下拉框
        /// <summary>
        ///  项目类型单位表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitUnitDropDownList(FineUIPro.DropDownList dropName, string projectId, string unitType, bool isShowPlease)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))            {
                var pUnit = (from x in db.Project_ProjectUnit
                             join y in db.Base_Unit on x.UnitId equals y.UnitId
                             where x.ProjectId == projectId && x.UnitType == unitType
                             orderby y.UnitCode
                             select y).ToList();

                dropName.DataValueField = "UnitId";
                dropName.DataTextField = "UnitName";
                dropName.DataSource = pUnit;
                dropName.DataBind();
                if (isShowPlease)
                {
                    Funs.FineUIPleaseSelect(dropName);
                }
            }
        }
        #endregion

        /// <summary>
        /// 根据项目Id删除项目单位
        /// </summary>
        /// <param name="projectId"></param>
        public static void DeleteProjectUnitByProjectId(string projectId)
        {
            var q = (from x in db.Project_ProjectUnit where x.ProjectId == projectId select x).ToList();
            if (q != null)
            {
                db.Project_ProjectUnit.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }

        public static List<Model.Project_ProjectUnit> GetProjectUnitListByProjectIdForApi(string projectId, string unitType, string name)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))            {
                string[] types = unitType.Split(',');
                return (from x in db.Project_ProjectUnit
                        join y in db.Base_Unit on x.UnitId equals y.UnitId
                        where x.ProjectId == projectId
                        where unitType == "" || types.Contains(x.UnitType)
                        where name == "" || y.UnitName.Contains(name)
                        orderby x.UnitType, y.UnitCode
                        select x).ToList();
            }
        }
    }
}
