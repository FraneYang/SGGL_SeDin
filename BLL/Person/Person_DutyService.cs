using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class Person_DutyService
    {
        public static Model.SGGLDB db = Funs.DB;
        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <param name="DutyId">人员Id</param>
        /// <returns>人员信息</returns>
        public static Model.Person_Duty GetPersonDutyById(string DutyId)
        {
            return Funs.DB.Person_Duty.FirstOrDefault(e => e.DutyId == DutyId);
        }

        /// <summary>
        /// 增加人员总结信息
        /// </summary>
        /// <param name="user">人员实体</param>
        public static void AddPersonDuty(Model.Person_Duty duty)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Person_Duty newDuty = new Model.Person_Duty
            {
                DutyId = duty.DutyId,
                DutyPersonId = duty.DutyPersonId,
                CompilePersonId = duty.CompilePersonId,
                CompileTime = duty.CompileTime,
                WorkPostId = duty.WorkPostId,
                State=duty.State,
                Template=duty.Template
            };
            db.Person_Duty.InsertOnSubmit(newDuty);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改人员总结信息
        /// </summary>
        /// <param name="user">实体</param>
        public static void UpdatePersonDuty(Model.Person_Duty duty)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Person_Duty newduty = db.Person_Duty.FirstOrDefault(e => e.DutyId == duty.DutyId);
            if (newduty != null)
            {
                newduty.DutyId = duty.DutyId;
                newduty.DutyPersonId = duty.DutyPersonId;
                newduty.WorkPostId = duty.WorkPostId;
                newduty.State = duty.State;
                newduty.Template = duty.Template;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据人员Id删除一个人员信息
        /// </summary>
        /// <param name="Person_DutyId"></param>
        public static void DeletePersonDuty(string DutyId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Person_Duty user = db.Person_Duty.FirstOrDefault(e => e.DutyId == DutyId);
            if (user != null)
            {
                db.Person_Duty.DeleteOnSubmit(user);
                db.SubmitChanges();
            }
        }
    }
}
