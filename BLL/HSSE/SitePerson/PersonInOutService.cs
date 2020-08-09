using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 人员信息
    /// </summary>
    public static class PersonInOutService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取人员出入场信息
        /// </summary>
        /// <param name="PersonInOutId"></param>
        /// <returns></returns>
        public static Model.SitePerson_PersonInOut GetPersonInOutById(string PersonInOutId)
        {
            return Funs.DB.SitePerson_PersonInOut.FirstOrDefault(e => e.PersonInOutId == PersonInOutId);
        }

        /// <summary>
        /// 根据人员id 出入时间取记录
        /// </summary>
        /// <param name="personId">人员id</param>
        /// <param name="time">出入场时间</param>
        /// <param name="isIn">出/入 true-入；false-出</param>
        /// <returns></returns>
        public static Model.SitePerson_PersonInOut GetPersonInOutByTimePersonId(string personId, DateTime ChangeTime, bool isIn)
        {
            return Funs.DB.SitePerson_PersonInOut.FirstOrDefault(x => x.PersonId == personId && x.ChangeTime == ChangeTime && x.IsIn == isIn);
        }

        /// <summary>
        /// 增加人员出入场信息
        /// </summary>
        /// <param name="PersonInOut">人员实体</param>
        public static void AddPersonInOut(Model.SitePerson_PersonInOut PersonInOut)
        {
            Model.SGGLDB db = Funs.DB;
            string postType = null;
            string workpostId = null;
            var getPerson = db.SitePerson_Person.FirstOrDefault(x => x.PersonId == PersonInOut.PersonId);
            if (getPerson != null)
            {
                workpostId = getPerson.WorkPostId;
                var getWokPost = db.Base_WorkPost.FirstOrDefault(x => x.PostType == getPerson.WorkPostId);
                if (getWokPost != null)
                {
                    postType = getWokPost.PostType;
                }
            }

            Model.SitePerson_PersonInOut newPersonInOut = new Model.SitePerson_PersonInOut
            {
                PersonInOutId = SQLHelper.GetNewID(typeof(Model.SitePerson_PersonInOut)),
                ProjectId = PersonInOut.ProjectId,
                UnitId = PersonInOut.UnitId,
                PersonId = PersonInOut.PersonId,
                IsIn = PersonInOut.IsIn,
                ChangeTime = PersonInOut.ChangeTime,
                WorkPostId = workpostId,
                PostType = postType,
            };

            db.SitePerson_PersonInOut.InsertOnSubmit(newPersonInOut);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据人员主键删除一个人员出入场记录
        /// </summary>
        /// <param name="personId"></param>
        public static void DeletePersonInOutByPersonId(string personId)
        {
            Model.SGGLDB db = Funs.DB;
            var personInOut = from x in db.SitePerson_PersonInOut where x.PersonId == personId select x;
            if (personInOut.Count() > 0)
            {
                db.SitePerson_PersonInOut.DeleteAllOnSubmit(personInOut);
                db.SubmitChanges();
            }
        }
        
        /// <summary>
        ///  获取出入记录人工时
        /// </summary>
        /// <returns></returns>
        public static List<Model.WorkPostStatisticItem> getWorkPostStatistic(List<Model.SitePerson_PersonInOut> getAllPersonInOutList)
        {
            Model.SGGLDB db = Funs.DB;
            List<Model.WorkPostStatisticItem> reports = new List<Model.WorkPostStatisticItem>();           
          
            var getUnitIdList = getAllPersonInOutList.Select(x => x.UnitId).Distinct();
            foreach (var uitem in getUnitIdList)
            {
                var getU = getAllPersonInOutList.Where(x => x.UnitId == uitem);
                var getWorkPostIdList = getU.Select(x => x.WorkPostId).Distinct();
                foreach (var witem in getWorkPostIdList)
                {
                    var getW = getU.Where(x => x.WorkPostId == witem);
                    Model.WorkPostStatisticItem newWItem = new Model.WorkPostStatisticItem
                    {
                        ID = SQLHelper.GetNewID(),
                        UnitId = uitem,
                        UnitName = UnitService.GetUnitNameByUnitId(uitem),
                        WorkPostId = witem,
                        WorkPostName = WorkPostService.getWorkPostNameById(witem),
                        PersonCount = getW.Select(x => x.PersonId).Distinct().Count(),
                        UnitWorkPostID = uitem + "|" + witem,
                    };

                    ////// 出场记录 集合
                    //var getUnitOutList = getW.Where(x => x.IsIn == false);
                    ////// 进场记录 集合
                    //var getUnitInList = getW.Where(x => x.IsIn == true);
                    //int personWorkTime = 0;
                    //List<string> personIdList = new List<string>();
                    //foreach (var itemOut in getUnitOutList)
                    //{
                    //    var getMaxInTime = getUnitInList.Where(x => x.ChangeTime < itemOut.ChangeTime
                    //                && x.PersonId == itemOut.PersonId && x.ChangeTime.Value.AddDays(1) > itemOut.ChangeTime).Max(x => x.ChangeTime);
                    //    if (getMaxInTime.HasValue)
                    //    {
                    //        personWorkTime += Convert.ToInt32((itemOut.ChangeTime - getMaxInTime).Value.TotalMinutes);
                    //    }
                    //    else
                    //    {
                    //        personIdList.Add(itemOut.PersonId);
                    //    }
                    //}
                    //if (personIdList.Count() > 0)
                    //{
                    //    personWorkTime += (personIdList.Distinct().Count() * 8 * 60);
                    //}

                    //newWItem.WorkHous = Convert.ToInt32(personWorkTime * 1.0 / 60);

                    reports.Add(newWItem);
                }
            }
            return reports;
        }
    }
}