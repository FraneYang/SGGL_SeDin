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
            return Funs.DB.SitePerson_MonthReportDetail.FirstOrDefault(x => x.MonthReportDetailId == monthReportDetailId);
        }

        /// <summary>
        /// 增加工作日报明细信息
        /// </summary>
        /// <param name="monthReportDetail">工作日报明细实体</param>
        public static void AddMonthReportDetail(Model.SitePerson_MonthReportDetail monthReportDetail)
        {
            Model.SGGLDB db = Funs.DB;
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
            Model.SGGLDB db = Funs.DB;
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
            Model.SGGLDB db = Funs.DB;
            var monthReportDetail = (from x in db.SitePerson_MonthReportDetail where x.MonthReportId == monthReportId select x).ToList();
            if (monthReportDetail.Count() > 0)
            {
                foreach (var item in monthReportDetail)
                {
                    var monthReportUnitDetail = from x in Funs.DB.SitePerson_MonthReportUnitDetail where x.MonthReportDetailId == item.MonthReportDetailId select x;
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
            return Funs.DB.SitePerson_MonthReportDetail.FirstOrDefault(e => e.UnitId == unitId && e.MonthReportId == montReportId);
        }


        /// <summary>
        /// 根据出入记录获取人工时明细
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="sDate"></param>
        /// <returns></returns>
        public static List<Model.SitePerson_MonthReportDetail> getMonthReportDetails(string projectId, DateTime sDate)
        {
            Model.SGGLDB db = Funs.DB;
            List<Model.SitePerson_MonthReportDetail> reportDetails = new List<Model.SitePerson_MonthReportDetail>();
            var getAllPersonInOutList = from x in db.SitePerson_PersonInOut
                                        where x.ProjectId == projectId && x.ChangeTime.Value.Year == sDate.Year
                                        && x.ChangeTime.Value.Month == sDate.Month 
                                        select x;
            if (getAllPersonInOutList.Count() > 0)
            {
                var getUnitIds = db.Project_ProjectUnit.Where(x => x.ProjectId == projectId).Select(x => x.UnitId);
                foreach (var unitItem in getUnitIds)
                {
                    Model.SitePerson_MonthReportDetail newDetail = new Model.SitePerson_MonthReportDetail
                    {
                        MonthReportDetailId = SQLHelper.GetNewID(),
                        UnitId = unitItem,
                        UnitName = UnitService.GetUnitNameByUnitId(unitItem),
                        PersonWorkTime = 0,
                    };

                    var getUnitAllList = getAllPersonInOutList.Where(x => x.UnitId == unitItem);
                    newDetail.RealPersonNum = getUnitAllList.Select(x => x.PersonId).Distinct().Count();
                    if (newDetail.RealPersonNum > 0)
                    {
                        //// 当日此单位出场记录 集合
                        var getUnitOutList = getUnitAllList.Where(x => x.IsIn == false);
                        //// 当日此单位进场记录 集合
                        var getUnitInList = getUnitAllList.Where(x => x.IsIn == true);
                        int personWorkTime = 0;
                        List<string> personIdList = new List<string>();
                        foreach (var itemOut in getUnitOutList)
                        {
                            var getMaxInTime = getUnitInList.Where(x => x.ChangeTime < itemOut.ChangeTime
                                        && x.PersonId == itemOut.PersonId && x.ChangeTime.Value.AddDays(1) > itemOut.ChangeTime).Max(x => x.ChangeTime);
                            if (getMaxInTime.HasValue)
                            {
                                personWorkTime += Convert.ToInt32((itemOut.ChangeTime - getMaxInTime).Value.TotalMinutes);
                            }
                            else
                            {
                                personIdList.Add(itemOut.PersonId);
                            }
                        }
                        if (personIdList.Count() > 0)
                        {
                            personWorkTime += (personIdList.Distinct().Count() * 8 * 60);
                        }

                        newDetail.PersonWorkTime = Convert.ToInt32(personWorkTime * 1.0 / 60);
                    }

                    reportDetails.Add(newDetail);
                }
            }
            return reportDetails;
        }
    }
}
