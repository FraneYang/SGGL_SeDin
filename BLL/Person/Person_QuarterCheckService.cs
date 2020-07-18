using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class Person_QuarterCheckService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <param name="QuarterCheckId">人员Id</param>
        /// <returns>人员信息</returns>
        public static Model.Person_QuarterCheck GetPerson_QuarterCheckById(string QuarterCheckId)
        {
            return new Model.SGGLDB(Funs.ConnString).Person_QuarterCheck.FirstOrDefault(e => e.QuarterCheckId == QuarterCheckId);
        }
        public static Model.Person_QuarterCheck GetQuarterCheckByDateTime(DateTime startTime, DateTime endTime)
        {
            return new Model.SGGLDB(Funs.ConnString).Person_QuarterCheck.FirstOrDefault(e => e.StartTime  == startTime && e.EndTime == endTime);
        }

        /// <summary>
        /// 增加人员总结信息
        /// </summary>
        /// <param name="user">人员实体</param>
        public static void AddPerson_QuarterCheck(Model.Person_QuarterCheck check)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Person_QuarterCheck newcheck = new Model.Person_QuarterCheck
            {
                QuarterCheckId = check.QuarterCheckId,
                QuarterCheckName=check.QuarterCheckName,
                UserId = check.UserId,
                ProjectId = check.ProjectId,
                StartTime = check.StartTime,
                EndTime = check.EndTime,
                State = check.State,
                CheckType=check.CheckType
            };
            db.Person_QuarterCheck.InsertOnSubmit(newcheck);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改人员总结信息
        /// </summary>
        /// <param name="user">实体</param>
        public static void UpdatePerson_QuarterCheck(Model.Person_QuarterCheck total)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Person_QuarterCheck newTotal = db.Person_QuarterCheck.FirstOrDefault(e => e.QuarterCheckId == total.QuarterCheckId);
            if (newTotal != null)
            {
                newTotal.State = total.State;
                db.SubmitChanges();
            }
        }
        /// <summary>
        /// 根据人员Id删除
        /// </summary>
        /// <param name="PersonTotalId"></param>
        public static void DeleteQuarterCheck(string QuarterCheckId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Person_QuarterCheck check = db.Person_QuarterCheck.FirstOrDefault(e => e.QuarterCheckId == QuarterCheckId);
            if (check != null)
            {
                db.Person_QuarterCheck.DeleteOnSubmit(check);
                db.SubmitChanges();
            }
        }
    }
}
