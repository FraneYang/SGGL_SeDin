using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public class SitePerson_DayReportUnitDetailService
    {
        public static Model.SGGLDB db = Funs.DB;


        /// <summary>
        /// 增加工作日报明细信息
        /// </summary>
        /// <param name="dayReportUnitDetail">工作日报明细实体</param>
        public static void AddDayReportUnitDetail(Model.SitePerson_DayReportUnitDetail dayReportUnitDetail)
        {
            Model.SGGLDB db = Funs.DB;
            Model.SitePerson_DayReportUnitDetail newDayReportUnitDetail = new Model.SitePerson_DayReportUnitDetail();
            string newKeyID = SQLHelper.GetNewID(typeof(Model.SitePerson_DayReportUnitDetail));
            newDayReportUnitDetail.DayReportUnitDetailId = newKeyID;
            newDayReportUnitDetail.DayReportDetailId = dayReportUnitDetail.DayReportDetailId;
            newDayReportUnitDetail.PostId = dayReportUnitDetail.PostId;
            newDayReportUnitDetail.CheckPersonNum = dayReportUnitDetail.CheckPersonNum;
            newDayReportUnitDetail.RealPersonNum = dayReportUnitDetail.RealPersonNum;
            newDayReportUnitDetail.PersonWorkTime = dayReportUnitDetail.PersonWorkTime;
            newDayReportUnitDetail.Remark = dayReportUnitDetail.Remark;
            db.SitePerson_DayReportUnitDetail.InsertOnSubmit(newDayReportUnitDetail);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改工作日报明细信息
        /// </summary>
        /// <param name="dayReportDetail">工作日报明细实体</param>
        public static void UpdateDayReportUnitDetail(Model.SitePerson_DayReportUnitDetail dayReportUnitDetail)
        {
            Model.SGGLDB db = Funs.DB;
            Model.SitePerson_DayReportUnitDetail newDayReportUnitDetail = db.SitePerson_DayReportUnitDetail.FirstOrDefault(e => e.DayReportUnitDetailId == dayReportUnitDetail.DayReportUnitDetailId);
            if (newDayReportUnitDetail != null)
            {
                newDayReportUnitDetail.PostId = dayReportUnitDetail.PostId;
                newDayReportUnitDetail.CheckPersonNum = dayReportUnitDetail.CheckPersonNum;
                newDayReportUnitDetail.RealPersonNum = dayReportUnitDetail.RealPersonNum;
                newDayReportUnitDetail.PersonWorkTime = dayReportUnitDetail.PersonWorkTime;
                newDayReportUnitDetail.Remark = dayReportUnitDetail.Remark;
                db.SubmitChanges();
            }
        }


        /// <summary>
        /// 根据出入记录获取人工时明细
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="sDate"></param>
        /// <returns></returns>
        public static List<Model.SitePerson_DayReportUnitDetail> getDayReportUnitDetails(string projectId, string unitId, DateTime sDate)
        {
            Model.SGGLDB db = Funs.DB;
            List<Model.SitePerson_DayReportUnitDetail> reportDetails = new List<Model.SitePerson_DayReportUnitDetail>();
            var getAllPersonInOutList = from x in db.SitePerson_PersonInOut
                                        where x.ProjectId == projectId && x.ChangeTime.Value.Year == sDate.Year
                                        && x.ChangeTime.Value.Month == sDate.Month && x.ChangeTime.Value.Day == sDate.Day
                                        && x.UnitId == unitId
                                        select x;
            if (getAllPersonInOutList.Count() > 0)
            {
                var getWorkPostIds = (from x in getAllPersonInOutList
                                      join y in db.SitePerson_Person on x.PersonId equals y.PersonId
                                      select y.WorkPostId).Distinct();
                foreach (var workItem in getWorkPostIds)
                {
                    Model.SitePerson_DayReportUnitDetail newDetail = new Model.SitePerson_DayReportUnitDetail
                    {
                        DayReportUnitDetailId = SQLHelper.GetNewID(),
                        PostId = workItem,
                        PostName = WorkPostService.getWorkPostNameById(workItem),
                        PersonWorkTime = 0,
                    };

                    var getUnitAllList = (from x in getAllPersonInOutList
                                          join y in db.SitePerson_Person on x.PersonId equals y.PersonId
                                          where y.WorkPostId == workItem
                                          select x);
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
                    if (newDetail.RealPersonNum > 0)
                    {
                        reportDetails.Add(newDetail);

                    }
                }
            }
            return reportDetails;
        }
    }
}
