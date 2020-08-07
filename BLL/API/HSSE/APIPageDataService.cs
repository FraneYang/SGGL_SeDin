using System.Collections.Generic;
using System.Linq;
using System;

namespace BLL
{
    public static class APIPageDataService
    {
        #region 根据类型获取图型数据
        /// <summary>
        /// 根据类型获取图型数据
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int getPersonNum(string projectId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                int SitePersonNum = 0;
                var getDayAll = from x in db.SitePerson_PersonInOut
                                where x.ProjectId == projectId && x.ChangeTime.Value.Year == DateTime.Now.Year && x.ChangeTime.Value.Month == DateTime.Now.Month
                                && x.ChangeTime.Value.Day == DateTime.Now.Day
                                select x;
                if (getDayAll.Count() > 0)
                {
                    var getInMaxs = from x in getDayAll
                                    group x by x.PersonId into g
                                    select new { g.First().PersonId, ChangeTime = g.Max(x => x.ChangeTime), g.First().IsIn };
                    if (getInMaxs.Count() > 0)
                    {
                        SitePersonNum = getInMaxs.Where(x => x.IsIn == true).Count();
                    }
                }
                return SitePersonNum;
            }
        }
        #endregion
    }
}
