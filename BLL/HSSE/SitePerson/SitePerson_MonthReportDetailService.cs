using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public class SitePerson_MonthReportDetailService
    {
        /// <summary>
        /// 获取工作日报明细信息
        /// </summary>
        /// <param name="MonthReportDetailId">工作日报明细Id</param>
        /// <returns></returns>
        public static Model.SitePerson_MonthReportDetail GetMonthReportDetailByMonthReportDetailId(string monthReportDetailId)
        {
            return new Model.SGGLDB(Funs.ConnString).SitePerson_MonthReportDetail.FirstOrDefault(x => x.MonthReportDetailId == monthReportDetailId);
        }

        /// <summary>
        /// 增加工作日报明细信息
        /// </summary>
        /// <param name="monthReportDetail">工作日报明细实体</param>
        public static void AddMonthReportDetail(Model.SitePerson_MonthReportDetail monthReportDetail)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.SitePerson_MonthReportDetail newMonthReportDetail = new Model.SitePerson_MonthReportDetail
            {
                MonthReportDetailId = monthReportDetail.MonthReportDetailId,
                MonthReportId = monthReportDetail.MonthReportId,
                UnitId = monthReportDetail.UnitId,
                WorkTime = monthReportDetail.WorkTime,
                CheckPersonNum = monthReportDetail.CheckPersonNum,
                RealPersonNum = monthReportDetail.RealPersonNum,
                PersonWorkTime = monthReportDetail.PersonWorkTime,
                TotalPersonWorkTime = monthReportDetail.TotalPersonWorkTime,
                Remark = monthReportDetail.Remark,
                StaffData = monthReportDetail.StaffData,
                DayNum = monthReportDetail.DayNum
            };

            db.SitePerson_MonthReportDetail.InsertOnSubmit(newMonthReportDetail);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改工作日报明细信息
        /// </summary>
        /// <param name="monthReportDetail">工作日报明细实体</param>
        public static void UpdateReportDetail(Model.SitePerson_MonthReportDetail monthReportDetail)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.SitePerson_MonthReportDetail newMonthReportDetail = db.SitePerson_MonthReportDetail.FirstOrDefault(e => e.MonthReportDetailId == monthReportDetail.MonthReportDetailId);
            if (newMonthReportDetail != null)
            {
                newMonthReportDetail.MonthReportId = monthReportDetail.MonthReportId;
                newMonthReportDetail.UnitId = monthReportDetail.UnitId;
                newMonthReportDetail.WorkTime = monthReportDetail.WorkTime;
                newMonthReportDetail.CheckPersonNum = monthReportDetail.CheckPersonNum;
                newMonthReportDetail.RealPersonNum = monthReportDetail.RealPersonNum;
                newMonthReportDetail.PersonWorkTime = monthReportDetail.PersonWorkTime;
                newMonthReportDetail.TotalPersonWorkTime = monthReportDetail.TotalPersonWorkTime;
                newMonthReportDetail.Remark = monthReportDetail.Remark;
                newMonthReportDetail.DayNum = monthReportDetail.DayNum;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据工作日报主键删除对应的所有工作日报明细信息
        /// </summary>
        /// <param name="monthReportId">工作日报主键</param>
        public static void DeleteMonthReportDetailsByMonthReportId(string monthReportId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var monthReportDetail = (from x in db.SitePerson_MonthReportDetail where x.MonthReportId == monthReportId select x).ToList();
            if (monthReportDetail.Count() > 0)
            {
                foreach (var item in monthReportDetail)
                {
                    var monthReportUnitDetail = from x in new Model.SGGLDB(Funs.ConnString).SitePerson_MonthReportUnitDetail where x.MonthReportDetailId == item.MonthReportDetailId select x;
                    if (monthReportUnitDetail.Count() > 0)
                    {
                        db.SitePerson_MonthReportUnitDetail.DeleteAllOnSubmit(monthReportUnitDetail);
                        db.SubmitChanges();
                    }
                }

                db.SitePerson_MonthReportDetail.DeleteAllOnSubmit(monthReportDetail);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据单位获取日报明细
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="dayReportId"></param>
        /// <returns></returns>
        public static Model.SitePerson_MonthReportDetail GetDayReportDetailByUnit(string unitId, string montReportId)
        {
            return new Model.SGGLDB(Funs.ConnString).SitePerson_MonthReportDetail.FirstOrDefault(e => e.UnitId == unitId && e.MonthReportId == montReportId);
        }
    }
}
