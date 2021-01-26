using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class WorkPackageService
    {
        /// <summary>
        /// 添加分部分项工程
        /// </summary>
        /// <param name="WorkPackage"></param>
        public static void AddWorkPackage(Model.WBS_WorkPackage WorkPackage)
        {
            Model.SGGLDB db = Funs.DB;
            Model.WBS_WorkPackage newWorkPackage = new Model.WBS_WorkPackage();

            newWorkPackage.WorkPackageId = WorkPackage.WorkPackageId;
            newWorkPackage.WorkPackageCode = WorkPackage.WorkPackageCode;
            newWorkPackage.UnitWorkId = WorkPackage.UnitWorkId;
            newWorkPackage.SuperWorkPack = WorkPackage.SuperWorkPack;
            newWorkPackage.PackageCode = WorkPackage.PackageCode;
            newWorkPackage.SuperWorkPackageId = WorkPackage.SuperWorkPackageId;
            newWorkPackage.PackageContent = WorkPackage.PackageContent;
            newWorkPackage.ProjectId = WorkPackage.ProjectId;
            newWorkPackage.IsChild = WorkPackage.IsChild;
            newWorkPackage.SortIndex = WorkPackage.SortIndex;
            newWorkPackage.InitWorkPackageCode = WorkPackage.InitWorkPackageCode;
            newWorkPackage.Weights = WorkPackage.Weights;
            newWorkPackage.ProjectType = WorkPackage.ProjectType;
            newWorkPackage.IsApprove = WorkPackage.IsApprove;
            newWorkPackage.Costs = WorkPackage.Costs;

            db.WBS_WorkPackage.InsertOnSubmit(newWorkPackage);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改分部分项工程
        /// </summary>
        /// <param name="WorkPackage"></param>
        public static void UpdateWorkPackage(Model.WBS_WorkPackage WorkPackage)
        {
            Model.SGGLDB db = Funs.DB;
            Model.WBS_WorkPackage newWorkPackage = db.WBS_WorkPackage.First(e => e.WorkPackageId == WorkPackage.WorkPackageId);
            newWorkPackage.WorkPackageCode = WorkPackage.WorkPackageCode;
            newWorkPackage.UnitWorkId = WorkPackage.UnitWorkId;
            newWorkPackage.PackageCode = WorkPackage.PackageCode;
            newWorkPackage.PackageContent = WorkPackage.PackageContent;
            newWorkPackage.ProjectId = WorkPackage.ProjectId;
            newWorkPackage.IsChild = WorkPackage.IsChild;
            newWorkPackage.SortIndex = WorkPackage.SortIndex;
            newWorkPackage.Weights = WorkPackage.Weights;
            newWorkPackage.IsApprove = WorkPackage.IsApprove;
            newWorkPackage.Costs = WorkPackage.Costs;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据单位工程id获取一个第一级分部信息
        /// </summary>
        /// <param name="unitWorkId"></param>
        /// <returns></returns>
        public static Model.WBS_WorkPackage GetWorkPackages1ByUnitWorkId(string unitWorkId)
        {
            return Funs.DB.WBS_WorkPackage.FirstOrDefault(x => x.SuperWorkPackageId == null && x.IsApprove == true && x.UnitWorkId == unitWorkId);
        }

        /// <summary>
        /// 根据单位工程Id获取第一级所有分部信息
        /// </summary>
        /// <param name="unitWorkId"></param>
        /// <returns></returns>
        public static List<Model.WBS_WorkPackage> GetWorkPackages1sByUnitWorkId(string unitWorkId)
        {
            return (from x in Funs.DB.WBS_WorkPackage where x.SuperWorkPackageId == null && x.UnitWorkId.ToString() == unitWorkId orderby x.WorkPackageCode select x).ToList();
        }
        /// <summary>
        /// 根据单位工程Id获取第一级审批所有分部信息
        /// </summary>
        /// <param name="unitWorkId"></param>
        /// <returns></returns>
        public static List<Model.WBS_WorkPackage> GetApproveWorkPackages1sByUnitWorkId(string unitWorkId)
        {
            return (from x in Funs.DB.WBS_WorkPackage where x.SuperWorkPackageId == null && x.UnitWorkId.ToString() == unitWorkId && x.IsApprove == true orderby x.WorkPackageCode select x).ToList();
        }
        /// <summary>
        /// 根据单位工程Id获取所有分部信息
        /// </summary>
        /// <param name="unitWorkId"></param>
        /// <returns></returns>
        public static List<Model.WBS_WorkPackage> GetAllWorkPackagesByUnitWorkId(string unitWorkId)
        {
            return (from x in Funs.DB.WBS_WorkPackage where x.UnitWorkId.ToString() == unitWorkId orderby x.WorkPackageCode select x).ToList();
        }
        /// <summary>
        /// 根据项目Id获取所有分部信息
        /// </summary>
        /// <param name="unitWorkId"></param>
        /// <returns></returns>
        public static List<Model.WBS_WorkPackage> GetAllWorkPackagesByProjectId(string projectId)
        {
            return (from x in Funs.DB.WBS_WorkPackage where x.ProjectId == projectId orderby x.WorkPackageCode select x).ToList();
        }
        /// <summary>
        /// 根据分部分项Id获取分部分项信息
        /// </summary>
        /// <param name="workPackageId"></param>
        /// <returns></returns>
        public static Model.WBS_WorkPackage GetWorkPackageByWorkPackageId(string workPackageId)
        {
            return Funs.DB.WBS_WorkPackage.FirstOrDefault(x => x.WorkPackageId == workPackageId);
        }

        /// <summary>
        /// 根据单位工程Id和初始化编号获取分部分项信息
        /// </summary>
        /// <param name="unitWorkId"></param>
        /// <param name="initWorkPackageCode"></param>
        /// <returns></returns>
        public static Model.WBS_WorkPackage GetWorkPackageByUnitWorkIdAndInitWorkPackageCode(string unitWorkId, string initWorkPackageCode)
        {
            return Funs.DB.WBS_WorkPackage.FirstOrDefault(x => x.UnitWorkId == unitWorkId && x.InitWorkPackageCode == initWorkPackageCode);
        }

        /// <summary>
        /// 根据父级Id获取所有分部分项信息
        /// </summary>
        /// <param name="workPackageId"></param>
        /// <returns></returns>
        public static List<Model.WBS_WorkPackage> GetAllWorkPackagesBySuperWorkPackageId(string workPackageId)
        {
            return (from x in Funs.DB.WBS_WorkPackage where x.SuperWorkPackageId == workPackageId orderby x.WorkPackageCode select x).ToList();
        }

        /// <summary>
        /// 根据父级Id获取所有审批分部分项信息
        /// </summary>
        /// <param name="workPackageId"></param>
        /// <returns></returns>
        public static List<Model.WBS_WorkPackage> GetAllApproveWorkPackagesBySuperWorkPackageId(string workPackageId)
        {
            return (from x in Funs.DB.WBS_WorkPackage where x.SuperWorkPackageId == workPackageId && x.IsApprove == true orderby x.WorkPackageCode select x).ToList();
        }

        /// <summary>
        /// 根据子分部工程Id删除一个子分部工程信息
        /// </summary>
        /// <param name="WorkPackageId"></param>
        public static void DeleteWorkPackage(string WorkPackageId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.WBS_WorkPackage WorkPackage = db.WBS_WorkPackage.First(e => e.WorkPackageId == WorkPackageId);
            db.WBS_WorkPackage.DeleteOnSubmit(WorkPackage);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据分部分项Id删除所有明细信息
        /// </summary>
        /// <param name="ControlItemAndCycleId"></param>
        public static void DeleteAllWorkPackageByUnitWorkId(string unitWorkId)
        {
            Model.SGGLDB db = Funs.DB;
            List<Model.WBS_WorkPackage> q = (from x in db.WBS_WorkPackage where x.UnitWorkId == unitWorkId orderby x.WorkPackageCode select x).ToList();
            db.WBS_WorkPackage.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据初始工作包编码和单位工程获取工作包集合
        /// </summary>
        /// <param name="initWorkPackageCode"></param>
        /// <param name="unitWorkId"></param>
        /// <returns></returns>
        public static List<Model.WBS_WorkPackage> GetWorkPackagesByInitWorkPackageCodeAndUnitWorkId(string initWorkPackageCode, string unitWorkId)
        {
            return (from x in Funs.DB.WBS_WorkPackage where x.InitWorkPackageCode == initWorkPackageCode && x.UnitWorkId.ToString() == unitWorkId orderby x.WorkPackageCode descending select x).ToList();
        }
        /// <summary>
        /// 根据单位工程Id获取所有分部信息
        /// </summary>
        /// <param name="unitWorkId"></param>
        /// <returns></returns>
        public static List<Model.WBS_WorkPackage> GetAllWorkPackagesByUnitWorkIds(string[] unitWorkIds)
        {
            return (from x in Funs.DB.WBS_WorkPackage where unitWorkIds.Contains(x.UnitWorkId) orderby x.WorkPackageCode select x).ToList();
        }

        /// <summary>
        ///  获取单位工程分部分项下拉列表
        /// </summary>
        /// <param name="dropName"></param>
        /// <param name="projectId"></param>
        /// <param name="isShowPlease"></param>
        public static void InitWorkPackagesDropDownListByUnitWorkId(FineUIPro.DropDownList dropName, string unitWorkId, bool isShowPlease)
        {
            dropName.DataValueField = "WorkPackageId";
            dropName.DataTextField = "PackageContent";
            dropName.DataSource = GetAllWorkPackagesByUnitWorkId(unitWorkId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
    }
}
