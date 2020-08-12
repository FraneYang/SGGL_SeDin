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
    }
}
