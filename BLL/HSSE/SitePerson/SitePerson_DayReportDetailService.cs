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
            return Funs.DB.SitePerson_DayReportDetail.FirstOrDefault(x => x.DayReportDetailId == dayReportDetailId);
        }

        /// <summary>
        /// 增加工作日报明细信息
        /// </summary>
        /// <param name="dayReportDetail">工作日报明细实体</param>
        public static void AddDayReportDetail(Model.SitePerson_DayReportDetail dayReportDetail)
        {
            Model.SGGLDB db = Funs.DB;
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
            Model.SGGLDB db = Funs.DB;
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
            Model.SGGLDB db = Funs.DB;
            var dayReportDetail = (from x in db.SitePerson_DayReportDetail where x.DayReportId == dayReportId select x).ToList();
            if (dayReportDetail.Count() > 0)
            {
                foreach (var item in dayReportDetail)
                {
                    var dayReportUnitDetail = from x in Funs.DB.SitePerson_DayReportUnitDetail where x.DayReportDetailId == item.DayReportDetailId select x;
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
            return Funs.DB.SitePerson_DayReportDetail.FirstOrDefault(e => e.UnitId == unitId && e.DayReportId == dayReportId);
        }

        /// <summary>
        /// 根据出入记录获取人工时明细
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="sDate"></param>
        /// <returns></returns>
        public static List<Model.SitePerson_DayReportDetail> getDayReportDetails(string projectId, DateTime sDate)
        {
            Model.SGGLDB db = Funs.DB;
            List<Model.SitePerson_DayReportDetail> reportDetails = new List<Model.SitePerson_DayReportDetail>();
            var getAllPersonInOutList = from x in db.SitePerson_PersonInOut
                                        where x.ProjectId == projectId && x.ChangeTime.Value.Year == sDate.Year
                                        && x.ChangeTime.Value.Month == sDate.Month && x.ChangeTime.Value.Day == sDate.Day
                                        select x;
            if (getAllPersonInOutList.Count() > 0)
            {
                var getUnitIds = db.Project_ProjectUnit.Where(x=>x.ProjectId ==projectId).Select(x => x.UnitId);                
                foreach (var unitItem in getUnitIds)
                {
                    Model.SitePerson_DayReportDetail newDetail = new Model.SitePerson_DayReportDetail
                    {
                        DayReportDetailId = SQLHelper.GetNewID(),
                        UnitId = unitItem,
                        UnitName = UnitService.GetUnitNameByUnitId(unitItem),
                        PersonWorkTime=0,
                    };

                    var getUnitAllList = getAllPersonInOutList.Where(x => x.UnitId == unitItem);
                    newDetail.RealPersonNum = getUnitAllList.Select(x => x.PersonId).Distinct().Count();
                    if (newDetail.RealPersonNum > 0)
                    {
                        //// 当日此单位出场记录 集合
                        var getUnitOutList = getUnitAllList.Where(x =>x.IsIn==false);
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
