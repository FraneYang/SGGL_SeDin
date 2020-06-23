using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ControlItemAndCycleService
    {
        /// <summary>
        /// 根据编号获取明细
        /// </summary>
        /// <param name="WorkPackageId"></param>
        public static Model.WBS_ControlItemAndCycle GetControlItemAndCycleById(string ControlItemAndCycleId)
        {
            return Funs.DB.WBS_ControlItemAndCycle.FirstOrDefault(e => e.ControlItemAndCycleId == ControlItemAndCycleId);
        }
        public static Model.WBS_ControlItemAndCycle GetControlItemAndCycleByIdForApi(string ControlItemAndCycleId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                return db.WBS_ControlItemAndCycle.FirstOrDefault(e => e.ControlItemAndCycleId == ControlItemAndCycleId);
            }
        }
        /// <summary>
        /// 根据控制点等级获取对应已选择分项的数量
        /// </summary>
        /// <param name="WorkPackageId">工作包Id</param>
        /// <returns></returns>
        public static int GetControlItemAndCyclesByControlPoint(string controlPoint)
        {
            return (from x in Funs.DB.WBS_ControlItemAndCycle where x.ControlPoint == controlPoint && x.IsApprove == true select x).Count();
        }

        public static List<Model.WBS_ControlItemAndCycle> GetTotalControlItemAndCycles(string projectId)
        {
            return (from x in Funs.DB.WBS_ControlItemAndCycle where x.ProjectId == projectId && x.IsApprove == true && (x.ControlPoint.Contains("A") || x.ControlPoint.Contains("B")) orderby x.ControlItemAndCycleCode select x).ToList();
        }

        /// <summary>
        /// 添加工作包
        /// </summary>
        /// <param name="ControlItemAndCycle"></param>
        public static void AddControlItemAndCycle(Model.WBS_ControlItemAndCycle ControlItemAndCycle)
        {
            Model.SGGLDB db = Funs.DB;
            Model.WBS_ControlItemAndCycle newControlItemAndCycle = new Model.WBS_ControlItemAndCycle();
            //string newKeyID = SQLHelper.GetNewID(typeof(Model.WBS_ControlItemAndCycle));
            newControlItemAndCycle.ControlItemAndCycleId = ControlItemAndCycle.ControlItemAndCycleId;
            newControlItemAndCycle.ControlItemAndCycleCode = ControlItemAndCycle.ControlItemAndCycleCode;
            newControlItemAndCycle.WorkPackageId = ControlItemAndCycle.WorkPackageId;
            newControlItemAndCycle.ProjectId = ControlItemAndCycle.ProjectId;
            newControlItemAndCycle.ControlItemContent = ControlItemAndCycle.ControlItemContent;
            newControlItemAndCycle.ControlPoint = ControlItemAndCycle.ControlPoint;
            newControlItemAndCycle.ControlItemDef = ControlItemAndCycle.ControlItemDef;
            newControlItemAndCycle.IsSelected = ControlItemAndCycle.IsSelected;
            newControlItemAndCycle.IsApprove = ControlItemAndCycle.IsApprove;
            newControlItemAndCycle.AttachUrl = ControlItemAndCycle.AttachUrl;
            newControlItemAndCycle.Weights = ControlItemAndCycle.Weights;
            newControlItemAndCycle.HGForms = ControlItemAndCycle.HGForms;
            newControlItemAndCycle.SHForms = ControlItemAndCycle.SHForms;
            newControlItemAndCycle.Standard = ControlItemAndCycle.Standard;
            newControlItemAndCycle.ClauseNo = ControlItemAndCycle.ClauseNo;
            newControlItemAndCycle.CheckNum = ControlItemAndCycle.CheckNum;
            newControlItemAndCycle.InitControlItemCode = ControlItemAndCycle.InitControlItemCode;
            newControlItemAndCycle.PlanCompleteDate = ControlItemAndCycle.PlanCompleteDate;

            db.WBS_ControlItemAndCycle.InsertOnSubmit(newControlItemAndCycle);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改工作包
        /// </summary>
        /// <param name="ControlItemAndCycle"></param>
        public static void UpdateControlItemAndCycle(Model.WBS_ControlItemAndCycle ControlItemAndCycle)
        {
            Model.SGGLDB db = Funs.DB;
            Model.WBS_ControlItemAndCycle newControlItemAndCycle = db.WBS_ControlItemAndCycle.First(e => e.ControlItemAndCycleId == ControlItemAndCycle.ControlItemAndCycleId);
            newControlItemAndCycle.ControlItemAndCycleCode = ControlItemAndCycle.ControlItemAndCycleCode;
            newControlItemAndCycle.WorkPackageId = ControlItemAndCycle.WorkPackageId;
            newControlItemAndCycle.ControlItemContent = ControlItemAndCycle.ControlItemContent;
            newControlItemAndCycle.ControlPoint = ControlItemAndCycle.ControlPoint;
            newControlItemAndCycle.ControlItemDef = ControlItemAndCycle.ControlItemDef;
            newControlItemAndCycle.ControlItemContent = ControlItemAndCycle.ControlItemContent;
            newControlItemAndCycle.ControlPoint = ControlItemAndCycle.ControlPoint;
            newControlItemAndCycle.ControlItemDef = ControlItemAndCycle.ControlItemDef;
            newControlItemAndCycle.IsSelected = ControlItemAndCycle.IsSelected;
            newControlItemAndCycle.IsApprove = ControlItemAndCycle.IsApprove;
            newControlItemAndCycle.AttachUrl = ControlItemAndCycle.AttachUrl;
            newControlItemAndCycle.Weights = ControlItemAndCycle.Weights;
            newControlItemAndCycle.HGForms = ControlItemAndCycle.HGForms;
            newControlItemAndCycle.SHForms = ControlItemAndCycle.SHForms;
            newControlItemAndCycle.Standard = ControlItemAndCycle.Standard;
            newControlItemAndCycle.ClauseNo = ControlItemAndCycle.ClauseNo;
            newControlItemAndCycle.CheckNum = ControlItemAndCycle.CheckNum;
            newControlItemAndCycle.PlanCompleteDate = ControlItemAndCycle.PlanCompleteDate;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据工作包Id删除一个工作包信息
        /// </summary>
        /// <param name="ControlItemAndCycleId"></param>
        public static void DeleteControlItemAndCycle(string ControlItemAndCycleId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.WBS_ControlItemAndCycle ControlItemAndCycle = db.WBS_ControlItemAndCycle.First(e => e.ControlItemAndCycleId == ControlItemAndCycleId);
            db.WBS_ControlItemAndCycle.DeleteOnSubmit(ControlItemAndCycle);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据初始工作包编码和分部分项获取工作包集合
        /// </summary>
        /// <param name="initWorkPackageCode"></param>
        /// <param name="unitWorkId"></param>
        /// <returns></returns>
        public static List<Model.WBS_ControlItemAndCycle> GetControlItemAndCyclesByInitControlItemCodeAndWorkPackageId(string initControlItemCode, string workPackageId)
        {
            return (from x in Funs.DB.WBS_ControlItemAndCycle where x.InitControlItemCode == initControlItemCode && x.WorkPackageId == workPackageId orderby x.ControlItemAndCycleCode descending select x).ToList();
        }

        /// <summary>
        /// 根据分部分项Id删除所有明细信息
        /// </summary>
        /// <param name="ControlItemAndCycleId"></param>
        public static void DeleteAllControlItemAndCycle(string WorkPackageId)
        {
            Model.SGGLDB db = Funs.DB;
            List<Model.WBS_ControlItemAndCycle> q = (from x in db.WBS_ControlItemAndCycle where x.WorkPackageId == WorkPackageId orderby x.ControlItemAndCycleCode select x).ToList();
            db.WBS_ControlItemAndCycle.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据单位工程Id删除所有明细信息
        /// </summary>
        /// <param name="ControlItemAndCycleId"></param>
        public static void DeleteAllControlItemAndCycleByUnitWorkId(string unitWorkId)
        {
            Model.SGGLDB db = Funs.DB;
            List<Model.WBS_ControlItemAndCycle> q = (from x in db.WBS_ControlItemAndCycle
                                                     join y in db.WBS_WorkPackage
                                                     on x.WorkPackageId equals y.WorkPackageId
                                                     where y.UnitWorkId == unitWorkId
                                                     orderby x.ControlItemAndCycleCode select x).ToList();
            db.WBS_ControlItemAndCycle.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据分部分项Id获取所有工作包内容
        /// </summary>
        /// <param name="WorkPackageId"></param>
        /// <returns></returns>
        public static List<Model.WBS_ControlItemAndCycle> GetListByWorkPackageId(string WorkPackageId)
        {
            return (from x in Funs.DB.WBS_ControlItemAndCycle where x.WorkPackageId == WorkPackageId orderby x.ControlItemAndCycleCode select x).ToList();
        }
        //public static List<Model.WBS_ControlItemAndCycle> GetListByWorkPackageIdForApi(string WorkPackageId)
        //{
        //    using (var db = new Model.SGGLDB(Funs.ConnString))
        //    {
        //        var res = (from x in db.WBS_ControlItemAndCycle where x.WorkPackageId == WorkPackageId  orderby x.ControlItemAndCycleCode select x).ToList();
        //        foreach(var item in res)
        //        {
        //            var details = BLL.SpotCheckDetailService.GetSpotCheckDetailsByControlItemAndCycleId(item.ControlItemAndCycleId);
        //            if (details != null)
        //            {
        //                item.ControlPoint = item.ControlPoint + "$" + details.Count;
        //            }
        //            else
        //            {
        //                item.ControlPoint = item.ControlPoint + "$0";
        //            }
        //            item.AttachUrl = ConvertDetailName(item.ControlItemAndCycleId);

        //        }
        //        return res;
        //    }
        //}
        public static string ConvertDetailName(object ControlItemAndCycleId)
        {
            string name = string.Empty;
            if (ControlItemAndCycleId != null)
            {
                Model.WBS_ControlItemAndCycle c = BLL.ControlItemAndCycleService.GetControlItemAndCycleById(ControlItemAndCycleId.ToString());
                if (c != null)
                {
                    name = c.ControlItemContent;
                    Model.WBS_WorkPackage w = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(c.WorkPackageId);
                    if (w != null)
                    {
                        name = w.PackageContent + "/" + name;
                        Model.WBS_WorkPackage pw = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(w.SuperWorkPackageId);
                        if (pw != null)
                        {
                            name = pw.PackageContent + "/" + name;
                            Model.WBS_WorkPackage ppw = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(pw.SuperWorkPackageId);
                            if (ppw != null)
                            {
                                name = ppw.PackageContent + "/" + name;
                                Model.WBS_UnitWork u = BLL.UnitWorkService.GetUnitWorkByUnitWorkId(ppw.UnitWorkId);
                                if (u != null)
                                {
                                    name = u.UnitWorkName + "/" + name;
                                }
                            }
                            else
                            {
                                Model.WBS_UnitWork u = BLL.UnitWorkService.GetUnitWorkByUnitWorkId(pw.UnitWorkId);
                                if (u != null)
                                {
                                    name = u.UnitWorkName + "/" + name;
                                }
                            }
                        }
                        else
                        {
                            Model.WBS_UnitWork u = BLL.UnitWorkService.GetUnitWorkByUnitWorkId(w.UnitWorkId);
                            if (u != null)
                            {
                                name = u.UnitWorkName + "/" + name;
                            }
                        }
                    }
                }
            }
            return name;
        }

        /// <summary>
        /// 是否存在工作包
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-存在，false-不存在</returns>
        public static bool IsExistControlItemAndCycleName(string workPackageId, string controlItemContent, string controlItemCode, string projectId)
        {
            var q = from x in Funs.DB.WBS_ControlItemAndCycle where x.WorkPackageId == workPackageId && x.ControlItemContent == controlItemContent && x.ControlItemAndCycleCode != controlItemCode && x.ProjectId == projectId select x;
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
        /// 获取表格名称
        /// </summary>
        /// <param name="ControlItemAndCycleId"></param>
        /// <returns></returns>
        public static string ConvertContronInfo(object ControlItemAndCycleId)
        {
            StringBuilder sbform = new StringBuilder();
            var str = Funs.DB.WBS_ControlItemAndCycle.FirstOrDefault(e => e.ControlItemAndCycleId == ControlItemAndCycleId.ToString());
            if (str != null)
            {
                if (!string.IsNullOrWhiteSpace(str.HGForms))
                {
                    sbform.Append(str.HGForms);
                }
                sbform.Append(' ', 8);
                if (!string.IsNullOrWhiteSpace(str.SHForms))
                {
                    sbform.Append(str.SHForms);
                }
            }
            if (sbform.Length > 0)
            {
                return sbform.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 根据单位工程Id获取需要计算权重的工作包
        /// </summary>
        /// <param name="WorkPackageId"></param>
        /// <returns></returns>
        public static List<Model.WBS_ControlItemAndCycle> GetControlItemAndCyclesByUnitWorkIdAndDate(string UnitWorkId, DateTime endDate)
        {
            return (from x in Funs.DB.WBS_ControlItemAndCycle
                    join y in Funs.DB.WBS_WorkPackage
                    on x.WorkPackageId equals y.WorkPackageId
                    where y.UnitWorkId == UnitWorkId
                    where x.IsApprove == true && x.Weights != null
                    && x.PlanCompleteDate <= endDate
                    orderby x.ControlItemAndCycleCode
                    select x).ToList();
        }

        /// <summary>
        /// 根据项目Id获取需要计算权重的工作包
        /// </summary>
        /// <param name="WorkPackageId"></param>
        /// <returns></returns>
        public static List<Model.WBS_ControlItemAndCycle> GetControlItemAndCyclesByProjectIdAndDate(string projectId, DateTime endDate)
        {
            return (from x in Funs.DB.WBS_ControlItemAndCycle
                    join y in Funs.DB.WBS_WorkPackage
                    on x.WorkPackageId equals y.WorkPackageId
                    where y.ProjectId == projectId
                    where x.IsApprove == true && x.Weights != null
                    && x.PlanCompleteDate <= endDate
                    orderby x.ControlItemAndCycleCode
                    select x).ToList();
        }

        /// <summary>
        /// 根据单位工程Id获取需要计算权重的工作包
        /// </summary>
        /// <param name="WorkPackageId"></param>
        /// <returns></returns>
        public static List<Model.WBS_ControlItemAndCycle> GetControlItemAndCyclesByUnitWorkIdsAndDate(string[] UnitWorkIds, DateTime endDate)
        {
            return (from x in Funs.DB.WBS_ControlItemAndCycle
                    join y in Funs.DB.WBS_WorkPackage
                    on x.WorkPackageId equals y.WorkPackageId
                    where UnitWorkIds.Contains(y.UnitWorkId)
                    where x.IsApprove == true && x.Weights != null
                    && x.PlanCompleteDate <= endDate
                    orderby x.ControlItemAndCycleCode
                    select x).ToList();
        }
    }
}
