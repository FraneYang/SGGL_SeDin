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
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取人员出入场信息
        /// </summary>
        /// <param name="PersonInOutId"></param>
        /// <returns></returns>
        public static Model.SitePerson_PersonInOut GetPersonInOutById(string PersonInOutId)
        {
            return new Model.SGGLDB(Funs.ConnString).SitePerson_PersonInOut.FirstOrDefault(e => e.PersonInOutId == PersonInOutId);
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
            return new Model.SGGLDB(Funs.ConnString).SitePerson_PersonInOut.FirstOrDefault(x => x.PersonId == personId && x.ChangeTime == ChangeTime && x.IsIn == isIn);
        }

        /// <summary>
        /// 增加人员出入场信息
        /// </summary>
        /// <param name="PersonInOut">人员实体</param>
        public static void AddPersonInOut(Model.SitePerson_PersonInOut PersonInOut)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.SitePerson_PersonInOut newPersonInOut = new Model.SitePerson_PersonInOut
            {
                PersonInOutId = SQLHelper.GetNewID(typeof(Model.SitePerson_PersonInOut)),
                ProjectId = PersonInOut.ProjectId,
                UnitId = PersonInOut.UnitId,
                PersonId = PersonInOut.PersonId,
                IsIn = PersonInOut.IsIn,
                ChangeTime = PersonInOut.ChangeTime
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
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var personInOut =from x in db.SitePerson_PersonInOut where x.PersonId == personId select x;
            if (personInOut.Count()> 0)
            {
                db.SitePerson_PersonInOut.DeleteAllOnSubmit(personInOut);
                db.SubmitChanges();
            }
        }
    }
}