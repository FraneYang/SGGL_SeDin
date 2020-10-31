using System.Collections.Generic;
using System.Linq;
using System;

namespace BLL
{
    public static class APIPageDataService
    {
        #region 获取当前人数
        /// <summary>
        /// 获取当前人数
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <returns></returns>
        public static List<Model.PageDataPersonInOutItem> getPersonNum(string projectId, DateTime dateValue)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                List<Model.PageDataPersonInOutItem> getSiteInOutList = new List<Model.PageDataPersonInOutItem>();
                var getDayAll = from x in db.SitePerson_PersonInOutNow
                                where x.ProjectId == projectId && x.ChangeTime.Value.Year == dateValue.Year && x.ChangeTime.Value.Month == dateValue.Month
                                && x.ChangeTime.Value.Day == dateValue.Day
                                select x;
                if (getDayAll.Count() > 0)
                {
                    var getInMaxs = from x in getDayAll
                                    group x by x.PersonId into g
                                    select new Model.PageDataPersonInOutItem
                                    {
                                        PersonId = g.First().PersonId,
                                        ChangeTime = g.Max(x => x.ChangeTime),
                                        IsIn = g.First().IsIn,
                                        PostType = g.First().PostType
                                    };
                    if (getInMaxs.Count() > 0)
                    {
                        getSiteInOutList = getInMaxs.Where(x => x.IsIn == true).ToList();
                    }
                }
                return getSiteInOutList;
            }
        }
        #endregion

        #region 获取当前人工时
        /// <summary>
        /// 获取当前人工时
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <returns></returns>
        public static int getSafeHours(string projectId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                int safeHours = 0;
                var getPersonInOutNumber = db.SitePerson_PersonInOutNumber.FirstOrDefault(x => x.ProjectId == projectId && x.InOutDate.Year == DateTime.Now.Year
                     && x.InOutDate.Month == DateTime.Now.Month && x.InOutDate.Day == DateTime.Now.Day);
                if (getPersonInOutNumber != null)
                {                    //// 获取工时                        
                    safeHours = getPersonInOutNumber.WorkHours ?? 0;
                }
                else
                {
                    safeHours = getSafeWorkTime(projectId);
                }

                return safeHours;
            }
        }
        #endregion

        /// <summary>
        ///  获取当前人工时
        /// </summary>
        /// <returns></returns>
        public static int getSafeWorkTime(string projectId)
        {
            Model.SGGLDB db = Funs.DB;
           int SafeHours = 0;
            //// 查找当前项目 最新的人工时数量记录
            int getMaxWorkHours = db.SitePerson_PersonInOutNumber.Where(x => x.ProjectId == projectId).Max(x => x.WorkHours) ?? 0;
            var getAllPersonInOutList = from x in db.SitePerson_PersonInOutNow
                                        where x.ProjectId == projectId && x.ChangeTime.Value.Year == DateTime.Now.Year && x.ChangeTime.Value.Month == DateTime.Now.Month
                                        && x.ChangeTime.Value.Day == DateTime.Now.Day
                                        select x;
            var getPersonOutTimes = getAllPersonInOutList.Where(x => x.IsIn == false);
            var getInLists = getAllPersonInOutList.Where(x => x.IsIn == true);
            if (getPersonOutTimes.Count() > 0)
            {
                List<string> personIdList = new List<string>();
                foreach (var item in getPersonOutTimes)
                {
                    var getMaxInTime = getInLists.Where(x => x.ChangeTime < item.ChangeTime
                                && x.PersonId == item.PersonId && x.ChangeTime.Value.AddDays(1) >= item.ChangeTime).Max(x => x.ChangeTime);
                    if (getMaxInTime.HasValue)
                    {
                        SafeHours += Convert.ToInt32((item.ChangeTime - getMaxInTime).Value.TotalMinutes);
                    }
                    else
                    {
                        personIdList.Add(item.PersonId);
                    }
                }
                if (personIdList.Count() > 0)
                {
                    SafeHours += (personIdList.Distinct().Count() * 8 * 60);
                }
            }

            SafeHours = Convert.ToInt32((SafeHours) * 1.0 / 60) + getMaxWorkHours;
            return SafeHours;
        }

        #region 获取日进场人数
        /// <summary>
        /// 获取当前人数
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <returns></returns>
        public static int getPersonInNowNum(string projectId, DateTime dateValue)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDayAll = from x in db.SitePerson_PersonInOutNow
                                where x.ProjectId == projectId && x.ChangeTime.Value.Year == dateValue.Year && x.ChangeTime.Value.Month == dateValue.Month
                                && x.ChangeTime.Value.Day == dateValue.Day && x.IsIn == true
                                select x;
                return getDayAll.Select(x=>x.PersonId).Distinct().Count(); ;
            }
        }
        #endregion
    }
}
