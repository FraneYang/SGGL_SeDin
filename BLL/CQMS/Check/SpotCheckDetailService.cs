using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SpotCheckDetailService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 获取实体验收记录明细
        /// </summary>
        /// <param name="satartRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListData(string SpotCheckCode)
        {
            return from x in db.Check_SpotCheckDetail
                   where x.SpotCheckCode == SpotCheckCode
                   select new
                   {
                       x.SpotCheckDetailId,
                       x.SpotCheckCode,
                       x.ControlItemAndCycleId,
                       x.IsOnesOK,
                       x.IsOK,
                       x.ConfirmDate,
                   };
        }

        /// <summary>
        /// 添加实体验收记录明细
        /// </summary>
        /// <param name="SpotCheckDetail"></param>
        public static void AddSpotCheckDetail(Model.Check_SpotCheckDetail SpotCheckDetail)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Check_SpotCheckDetail newSpotCheckDetail = new Model.Check_SpotCheckDetail();
            newSpotCheckDetail.SpotCheckDetailId = SpotCheckDetail.SpotCheckDetailId;
            newSpotCheckDetail.SpotCheckCode = SpotCheckDetail.SpotCheckCode;
            newSpotCheckDetail.ControlItemAndCycleId = SpotCheckDetail.ControlItemAndCycleId;
            newSpotCheckDetail.IsOnesOK = SpotCheckDetail.IsOnesOK;
            newSpotCheckDetail.IsOK = SpotCheckDetail.IsOK;
            newSpotCheckDetail.ConfirmDate = SpotCheckDetail.ConfirmDate;
            newSpotCheckDetail.RectifyDescription = SpotCheckDetail.RectifyDescription;
            newSpotCheckDetail.CreateDate = SpotCheckDetail.CreateDate;

            db.Check_SpotCheckDetail.InsertOnSubmit(newSpotCheckDetail);
            db.SubmitChanges();
        }
        public static void AddSpotCheckDetailForApi(Model.Check_SpotCheckDetail SpotCheckDetail)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Check_SpotCheckDetail newSpotCheckDetail = new Model.Check_SpotCheckDetail();
                newSpotCheckDetail.SpotCheckDetailId = SpotCheckDetail.SpotCheckDetailId;
                newSpotCheckDetail.SpotCheckCode = SpotCheckDetail.SpotCheckCode;
                newSpotCheckDetail.ControlItemAndCycleId = SpotCheckDetail.ControlItemAndCycleId;
                newSpotCheckDetail.IsOnesOK = SpotCheckDetail.IsOnesOK;
                newSpotCheckDetail.IsOK = SpotCheckDetail.IsOK;
                newSpotCheckDetail.ConfirmDate = SpotCheckDetail.ConfirmDate;
                newSpotCheckDetail.RectifyDescription = SpotCheckDetail.RectifyDescription;
                newSpotCheckDetail.CreateDate = SpotCheckDetail.CreateDate;

                db.Check_SpotCheckDetail.InsertOnSubmit(newSpotCheckDetail);
                db.SubmitChanges();
            }
        }
        /// <summary>
        /// 修改实体验收记录明细
        /// </summary>
        /// <param name="SpotCheckDetail"></param>
        public static void UpdateSpotCheckDetail(Model.Check_SpotCheckDetail SpotCheckDetail)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Check_SpotCheckDetail newSpotCheckDetail = db.Check_SpotCheckDetail.First(e => e.SpotCheckDetailId == SpotCheckDetail.SpotCheckDetailId);
            newSpotCheckDetail.SpotCheckCode = SpotCheckDetail.SpotCheckCode;
            newSpotCheckDetail.ControlItemAndCycleId = SpotCheckDetail.ControlItemAndCycleId;
            newSpotCheckDetail.IsOnesOK = SpotCheckDetail.IsOnesOK;
            newSpotCheckDetail.IsOK = SpotCheckDetail.IsOK;
            newSpotCheckDetail.ConfirmDate = SpotCheckDetail.ConfirmDate;
            newSpotCheckDetail.RectifyDescription = SpotCheckDetail.RectifyDescription;
            newSpotCheckDetail.IsDataOK = SpotCheckDetail.IsDataOK;
            newSpotCheckDetail.DataConfirmDate = SpotCheckDetail.DataConfirmDate;
            newSpotCheckDetail.State = SpotCheckDetail.State;
            newSpotCheckDetail.HandleMan = SpotCheckDetail.HandleMan;
            newSpotCheckDetail.IsShow = SpotCheckDetail.IsShow;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据实体验收记录明细Id删除一个实体验收记录信息明细
        /// </summary>
        /// <param name="SpotCheckDetailId"></param>
        public static void DeleteSpotCheckDetail(string SpotCheckDetailId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Check_SpotCheckDetail SpotCheckDetail = db.Check_SpotCheckDetail.First(e => e.SpotCheckDetailId == SpotCheckDetailId);
            db.Check_SpotCheckDetail.DeleteOnSubmit(SpotCheckDetail);
            db.SubmitChanges();
        }
        public static void DeleteSpotCheckDetailForApi(string SpotCheckDetailId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Check_SpotCheckDetail SpotCheckDetail = db.Check_SpotCheckDetail.First(e => e.SpotCheckDetailId == SpotCheckDetailId);
                db.Check_SpotCheckDetail.DeleteOnSubmit(SpotCheckDetail);
                db.SubmitChanges();
            }
        }
        /// <summary>
        /// 根据实体验收记录主表删除所有实体验收记录信息明细
        /// </summary>
        /// <param name="DrawingAuditId"></param>
        public static void DeleteAllSpotCheckDetail(string SpotCheckCode)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var q = (from x in db.Check_SpotCheckDetail where x.SpotCheckCode == SpotCheckCode select x).ToList();
            db.Check_SpotCheckDetail.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据实体验收记录明细Id获取一个实体验收记录信息明细
        /// </summary>
        /// <param name="SpotCheckDetailId"></param>
        public static Model.Check_SpotCheckDetail GetSpotCheckDetail(string SpotCheckDetailId)
        {
            return new Model.SGGLDB(Funs.ConnString).Check_SpotCheckDetail.FirstOrDefault(e => e.SpotCheckDetailId == SpotCheckDetailId);
        }

        /// <summary>
        /// 根据实体验收记录Id获取一个实体验收记录信息明细
        /// </summary>
        /// <param name="SpotCheckDetailId"></param>
        public static Model.Check_SpotCheckDetail GetSpotCheckDetailBySoptCheckCode(string SpotCheckCode)
        {
            return new Model.SGGLDB(Funs.ConnString).Check_SpotCheckDetail.FirstOrDefault(e => e.SpotCheckCode == SpotCheckCode);
        }

        /// <summary>
        /// 根据实体验收记录Id获取一个不合格实体验收记录信息明细
        /// </summary>
        /// <param name="SpotCheckDetailId"></param>
        public static Model.Check_SpotCheckDetail GetNotOKSpotCheckDetailBySoptCheckCode(string SpotCheckCode)
        {
            return new Model.SGGLDB(Funs.ConnString).Check_SpotCheckDetail.FirstOrDefault(e => e.SpotCheckCode == SpotCheckCode && (e.IsOK == null || e.IsOK == false));
        }

        /// <summary>
        /// 根据实体验收记录主键获取所有实体验收记录信息明细
        /// </summary>
        /// <param name="SpotCheckCode"></param>
        public static List<Model.Check_SpotCheckDetail> GetSpotCheckDetails(string SpotCheckCode)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Check_SpotCheckDetail
                    join y in new Model.SGGLDB(Funs.ConnString).WBS_ControlItemAndCycle
                    on x.ControlItemAndCycleId equals y.ControlItemAndCycleId
                    where x.SpotCheckCode == SpotCheckCode
                    orderby y.WorkPackageId
                    select x).ToList();
        }
        public static List<Model.Check_SpotCheckDetail> GetSpotCheckDetailsForApi(string SpotCheckCode)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                return (from x in db.Check_SpotCheckDetail
                        join y in db.WBS_ControlItemAndCycle
                        on x.ControlItemAndCycleId equals y.ControlItemAndCycleId
                        where x.SpotCheckCode == SpotCheckCode
                        orderby y.WorkPackageId
                        select x).ToList();
            }
        }

        /// <summary>
        /// 根据实体验收记录主键获取所有实体合格实体验收记录信息明细
        /// </summary>
        /// <param name="SpotCheckCode"></param>
        public static List<Model.Check_SpotCheckDetail> GetOKSpotCheckDetails(string SpotCheckCode)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Check_SpotCheckDetail
                    join y in new Model.SGGLDB(Funs.ConnString).WBS_ControlItemAndCycle
                    on x.ControlItemAndCycleId equals y.ControlItemAndCycleId
                    where x.SpotCheckCode == SpotCheckCode && x.IsOK == true
                    orderby y.WorkPackageId
                    select x).ToList();
        }
        public static List<Model.Check_SpotCheckDetail> GetOKSpotCheckDetailsForApi(string SpotCheckCode)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                return (from x in db.Check_SpotCheckDetail
                        join y in db.WBS_ControlItemAndCycle
                        on x.ControlItemAndCycleId equals y.ControlItemAndCycleId
                        where x.SpotCheckCode == SpotCheckCode && x.IsOK == true
                        orderby y.WorkPackageId
                        select x).ToList();
            }

        }
        /// <summary>
        /// 根据实体验收记录主键获取所有实体合格并且有资料表格需要上传的实体验收记录信息明细
        /// </summary>
        /// <param name="SpotCheckCode"></param>
        public static List<Model.Check_SpotCheckDetail> GetShowSpotCheckDetails(string SpotCheckCode)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Check_SpotCheckDetail
                    join y in new Model.SGGLDB(Funs.ConnString).WBS_ControlItemAndCycle
                    on x.ControlItemAndCycleId equals y.ControlItemAndCycleId
                    where x.SpotCheckCode == SpotCheckCode && x.IsOK == true && x.IsShow == true
                    orderby y.WorkPackageId
                    select x).ToList();
        }

        /// <summary>
        /// 根据单位工程Id获取所有工序验收记录信息明细
        /// </summary>
        /// <param name="SpotCheckCode"></param>
        public static List<Model.View_Check_SoptCheckDetail> GetViewSpotCheckDetailsByUnitWorkIdAndDate(string unitWorkId, DateTime endDate, string isDataOK)
        {
            if (string.IsNullOrEmpty(isDataOK))   //不按资料合格统计
            {
                return (from x in new Model.SGGLDB(Funs.ConnString).View_Check_SoptCheckDetail
                        where x.UnitWorkId == unitWorkId && x.IsOK == true && x.SpotCheckDate <= endDate
                        select x).ToList();
            }
            else    //按资料合格统计
            {
                return (from x in new Model.SGGLDB(Funs.ConnString).View_Check_SoptCheckDetail
                        where x.UnitWorkId == unitWorkId && x.IsOK == true && (x.IsDataOK == "1" || x.IsDataOK == "2") && x.SpotCheckDate <= endDate
                        select x).ToList();
            }
        }

        /// <summary>
        /// 根据项目Id获取所有工序验收记录信息明细
        /// </summary>
        /// <param name="SpotCheckCode"></param>
        public static List<Model.View_Check_SoptCheckDetail> GetViewSpotCheckDetailsByProjectIdAndDate(string projectId, DateTime endDate, string isDataOK)
        {
            if (string.IsNullOrEmpty(isDataOK))   //不按资料合格统计
            {
                return (from x in new Model.SGGLDB(Funs.ConnString).View_Check_SoptCheckDetail
                        where x.ProjectId == projectId && x.IsOK == true && x.SpotCheckDate <= endDate
                        select x).ToList();
            }
            else    //按资料合格统计
            {
                return (from x in new Model.SGGLDB(Funs.ConnString).View_Check_SoptCheckDetail
                        where x.ProjectId == projectId && x.IsOK == true && (x.IsDataOK == "1" || x.IsDataOK == "2") && x.SpotCheckDate <= endDate
                        select x).ToList();
            }
        }

        /// <summary>
        /// 根据工作包主键获取所有实体验收记录信息明细
        /// </summary>
        /// <param name="SpotCheckCode"></param>
        public static List<Model.Check_SpotCheckDetail> GetSpotCheckDetailsByControlItemAndCycleId(string controlItemAndCycleId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Check_SpotCheckDetail
                    where x.ControlItemAndCycleId == controlItemAndCycleId && x.IsOK == true
                    select x).ToList();
        }

        /// <summary>
        /// 根据工作包主键获取所有工序验收记录信息明细
        /// </summary>
        /// <param name="SpotCheckCode"></param>
        public static List<Model.Check_SpotCheckDetail> GetSpotCheckDetailsByControlItemAndCycleIds(List<string> controlItemAndCycleId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Check_SpotCheckDetail
                    where controlItemAndCycleId.Contains(x.ControlItemAndCycleId) && x.IsOK == true
                    select x).ToList();
        }


        /// 根据时间段获取实体验收记录明细集合
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="projectType">工程类型</param>
        public static List<Model.Check_SpotCheckDetail> GetOKSpotCheckDetailListByTime(string projectId, DateTime startTime, DateTime endTime)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Check_SpotCheckDetail
                    join y in new Model.SGGLDB(Funs.ConnString).Check_SpotCheck
                    on x.SpotCheckCode equals y.SpotCheckCode
                    where y.ProjectId == projectId && x.ConfirmDate >= startTime && x.ConfirmDate < endTime && x.IsOK == true
                    select x).ToList();
        }

        /// <summary>
        /// 根据时间段获取实体验收记录明细集合
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="projectType">工程类型</param>
        public static List<Model.Check_SpotCheckDetail> GetTotalOKSpotCheckDetailListByTime(string projectId, DateTime endTime)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Check_SpotCheckDetail
                    join y in new Model.SGGLDB(Funs.ConnString).Check_SpotCheck
                    on x.SpotCheckCode equals y.SpotCheckCode
                    where y.ProjectId == projectId && x.ConfirmDate < endTime && x.IsOK == true
                    select x).ToList();
        }
        public static void UpdateSpotCheckDetailForApi(Model.Check_SpotCheckDetail SpotCheckDetail)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Check_SpotCheckDetail newSpotCheckDetail = db.Check_SpotCheckDetail.First(e => e.SpotCheckDetailId == SpotCheckDetail.SpotCheckDetailId);
                if (!string.IsNullOrEmpty(SpotCheckDetail.SpotCheckCode))
                    newSpotCheckDetail.SpotCheckCode = SpotCheckDetail.SpotCheckCode;
                if (!string.IsNullOrEmpty(SpotCheckDetail.ControlItemAndCycleId))
                    newSpotCheckDetail.ControlItemAndCycleId = SpotCheckDetail.ControlItemAndCycleId;
                if (SpotCheckDetail.IsOnesOK.HasValue)
                    newSpotCheckDetail.IsOnesOK = SpotCheckDetail.IsOnesOK;
                if (SpotCheckDetail.IsOK.HasValue)
                    newSpotCheckDetail.IsOK = SpotCheckDetail.IsOK;
                if (SpotCheckDetail.ConfirmDate.HasValue)
                    newSpotCheckDetail.ConfirmDate = SpotCheckDetail.ConfirmDate;
                if (!string.IsNullOrEmpty(SpotCheckDetail.RectifyDescription))
                    newSpotCheckDetail.RectifyDescription = SpotCheckDetail.RectifyDescription;
                if (SpotCheckDetail.IsShow.HasValue)
                    newSpotCheckDetail.IsShow = SpotCheckDetail.IsShow;
                if (!string.IsNullOrEmpty(SpotCheckDetail.State))
                    newSpotCheckDetail.State = SpotCheckDetail.State;

                newSpotCheckDetail.HandleMan = SpotCheckDetail.HandleMan;

                db.SubmitChanges();
            }
        }

        /// 质量验收(本月合格)
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        public static List<Model.View_Check_SoptCheckDetail> GetOKSpotCheckDetailListByTime1(string projectId, DateTime startTime, DateTime endTime)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).View_Check_SoptCheckDetail
                    where x.ProjectId == projectId && !x.ControlPoint.Contains("C") && x.SpotCheckDate >= startTime && x.SpotCheckDate < endTime && x.IsOK == true
                    select x).ToList();
        }
        /// 质量验收(本月全部)
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        public static List<Model.View_Check_SoptCheckDetail> GetAllSpotCheckDetailListByTime(string projectId, DateTime startTime, DateTime endTime)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).View_Check_SoptCheckDetail
                    where x.ProjectId == projectId && !x.ControlPoint.Contains("C") && x.SpotCheckDate >= startTime && x.SpotCheckDate < endTime && x.IsOK != null
                    select x).ToList();
        }
        /// 质量验收累计合格（累计合格）
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        public static List<Model.View_Check_SoptCheckDetail> GetTotalOKSpotCheckDetailListByTime1(string projectId, DateTime endTime)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).View_Check_SoptCheckDetail
                    where x.ProjectId == projectId && !x.ControlPoint.Contains("C") && x.SpotCheckDate < endTime && x.IsOK == true
                    select x).ToList();
        }
        /// 质量验收累计合格（累计全部）
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        public static List<Model.View_Check_SoptCheckDetail> GetTotalAllSpotCheckDetailListByTime(string projectId, DateTime endTime)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).View_Check_SoptCheckDetail
                    where x.ProjectId == projectId && !x.ControlPoint.Contains("C") && x.SpotCheckDate < endTime && x.IsOK != null
                    select x).ToList();
        }
        /// <summary>
        /// 质量记录本月同步率（资料本月合格）
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        public static List<Model.View_Check_SoptCheckDetail> GetMonthDataOkSpotCheckDetailListByTime(string projectId, DateTime startTime, DateTime endTime)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).View_Check_SoptCheckDetail
                    where x.ProjectId == projectId && !x.ControlPoint.Contains("C") && x.SpotCheckDate >= startTime && x.SpotCheckDate < endTime && x.IsDataOK == "1"
                    select x).ToList();
        }
        /// <summary>
        /// 质量记录本月同步率（资料累计合格）
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="projectType">工程类型</param>
        public static List<Model.View_Check_SoptCheckDetail> GetAllDataOkSpotCheckDetailListByTime(string projectId, DateTime endTime)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).View_Check_SoptCheckDetail
                    where x.ProjectId == projectId && !x.ControlPoint.Contains("C") && x.SpotCheckDate < endTime && x.IsDataOK == "1"
                    select x).ToList();
        }

        /// <summary>
        /// 根据单位工程Id获取所有工序验收记录信息明细
        /// </summary>
        /// <param name="SpotCheckCode"></param>
        public static List<Model.View_Check_SoptCheckDetail> GetViewSpotCheckDetailsByUnitWorkIdsAndDate(string[] unitWorkIds, DateTime endDate, string isDataOK)
        {
            if (string.IsNullOrEmpty(isDataOK))   //不按资料合格统计
            {
                return (from x in new Model.SGGLDB(Funs.ConnString).View_Check_SoptCheckDetail
                        where unitWorkIds.Contains(x.UnitWorkId) && x.IsOK == true && x.SpotCheckDate <= endDate
                        select x).ToList();
            }
            else    //按资料合格统计
            {
                return (from x in new Model.SGGLDB(Funs.ConnString).View_Check_SoptCheckDetail
                        where unitWorkIds.Contains(x.UnitWorkId) && x.IsOK == true && (x.IsDataOK == "1" || x.IsDataOK == "2") && x.SpotCheckDate <= endDate
                        select x).ToList();
            }
        }
    }
}
