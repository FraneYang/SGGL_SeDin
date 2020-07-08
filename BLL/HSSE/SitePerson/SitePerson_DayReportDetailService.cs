using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public class SitePerson_DayReportDetailService
    {
        /// <summary>
        /// 获取工作日报明细信息
        /// </summary>
        /// <param name="DayReportDetailId">工作日报明细Id</param>
        /// <returns></returns>
        public static Model.SitePerson_DayReportDetail GetDayReportDetailByDayReportDetailId(string dayReportDetailId)
        {
            return new Model.SGGLDB(Funs.ConnString).SitePerson_DayReportDetail.FirstOrDefault(x => x.DayReportDetailId == dayReportDetailId);
        }

        /// <summary>
        /// 增加工作日报明细信息
        /// </summary>
        /// <param name="dayReportDetail">工作日报明细实体</param>
        public static void AddDayReportDetail(Model.SitePerson_DayReportDetail dayReportDetail)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.SitePerson_DayReportDetail newDayReportDetail = new Model.SitePerson_DayReportDetail
            {
                DayReportDetailId = dayReportDetail.DayReportDetailId,
                DayReportId = dayReportDetail.DayReportId,
                UnitId = dayReportDetail.UnitId,
                WorkTime = dayReportDetail.WorkTime,
                CheckPersonNum = dayReportDetail.CheckPersonNum,
                RealPersonNum = dayReportDetail.RealPersonNum,
                PersonWorkTime = dayReportDetail.PersonWorkTime,
                TotalPersonWorkTime = dayReportDetail.TotalPersonWorkTime,
                Remark = dayReportDetail.Remark,
                StaffData = dayReportDetail.StaffData,
                DayNum = dayReportDetail.DayNum
            };

            db.SitePerson_DayReportDetail.InsertOnSubmit(newDayReportDetail);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改工作日报明细信息
        /// </summary>
        /// <param name="dayReportDetail">工作日报明细实体</param>
        public static void UpdateReportDetail(Model.SitePerson_DayReportDetail dayReportDetail)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.SitePerson_DayReportDetail newDayReportDetail = db.SitePerson_DayReportDetail.FirstOrDefault(e => e.DayReportDetailId == dayReportDetail.DayReportDetailId);
            if (newDayReportDetail != null)
            {
                newDayReportDetail.DayReportId = dayReportDetail.DayReportId;
                newDayReportDetail.UnitId = dayReportDetail.UnitId;
                newDayReportDetail.WorkTime = dayReportDetail.WorkTime;
                newDayReportDetail.CheckPersonNum = dayReportDetail.CheckPersonNum;
                newDayReportDetail.RealPersonNum = dayReportDetail.RealPersonNum;
                newDayReportDetail.PersonWorkTime = dayReportDetail.PersonWorkTime;
                newDayReportDetail.TotalPersonWorkTime = dayReportDetail.TotalPersonWorkTime;
                newDayReportDetail.Remark = dayReportDetail.Remark;
                newDayReportDetail.DayNum = dayReportDetail.DayNum;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据工作日报主键删除对应的所有工作日报明细信息
        /// </summary>
        /// <param name="dayReportId">工作日报主键</param>
        public static void DeleteDayReportDetailsByDayReportId(string dayReportId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var dayReportDetail = (from x in db.SitePerson_DayReportDetail where x.DayReportId == dayReportId select x).ToList();
            if (dayReportDetail.Count() > 0)
            {
                foreach (var item in dayReportDetail)
                {
                    var dayReportUnitDetail = from x in new Model.SGGLDB(Funs.ConnString).SitePerson_DayReportUnitDetail where x.DayReportDetailId == item.DayReportDetailId select x;
                    if (dayReportUnitDetail.Count() > 0)
                    {
                        db.SitePerson_DayReportUnitDetail.DeleteAllOnSubmit(dayReportUnitDetail);
                        db.SubmitChanges();
                    }
                }

                db.SitePerson_DayReportDetail.DeleteAllOnSubmit(dayReportDetail);
                db.SubmitChanges();
            }
        }


        /// <summary>
        /// 根据单位获取日报明细
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="dayReportId"></param>
        /// <returns></returns>
        public static Model.SitePerson_DayReportDetail GetDayReportDetailByUnit(string unitId, string dayReportId)
        {
            return new Model.SGGLDB(Funs.ConnString).SitePerson_DayReportDetail.FirstOrDefault(e => e.UnitId == unitId && e.DayReportId == dayReportId);
        }
    }
}
