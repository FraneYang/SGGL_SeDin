using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public class SitePerson_CheckingService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据人员考勤主键获取人员考勤管理信息
        /// </summary>
        /// <param name="checkingId">人员考勤主键</param>
        /// <returns>人员考勤管理信息</returns>
        public static Model.SitePerson_Checking GetPersonInfoByCheckingId(string checkingId)
        {
            return Funs.DB.SitePerson_Checking.FirstOrDefault(x=> x.CheckingId == checkingId);
        }

        /// <summary>
        /// 增加人员考勤管理信息
        /// </summary>
        /// <param name="personInfo">人员考勤管理实体</param>
        public static void AddPersonInfo(Model.SitePerson_Checking personInfo)
        {
            Model.SGGLDB db = Funs.DB;
            Model.SitePerson_Checking newPersonInfo = new Model.SitePerson_Checking
            {
                CheckingId = personInfo.CheckingId,
                PersonId = personInfo.PersonId,
                CardNo = personInfo.CardNo,
                ProjectId = personInfo.ProjectId,
                WorkAreaId = personInfo.WorkAreaId,
                WorkAreaName = personInfo.WorkAreaName,
                IdentityCard = personInfo.IdentityCard,
                IntoOutTime = personInfo.IntoOutTime,
                IntoOut = personInfo.IntoOut,
                Address = personInfo.Address,
                States = BLL.Const.State_2
            };

            db.SitePerson_Checking.InsertOnSubmit(newPersonInfo);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改人员考勤管理信息
        /// </summary>
        /// <param name="personInfo">人员考勤管理实体</param>
        public static void UpdatePersonInfo(Model.SitePerson_Checking personInfo)
        {
            Model.SGGLDB db = Funs.DB;
            Model.SitePerson_Checking newPersonInfo = db.SitePerson_Checking.First(e => e.CheckingId == personInfo.CheckingId);
            newPersonInfo.CardNo = personInfo.CardNo;
            newPersonInfo.PersonId = personInfo.PersonId;
            newPersonInfo.ProjectId = personInfo.ProjectId;
            newPersonInfo.WorkAreaId = personInfo.WorkAreaId;
            newPersonInfo.WorkAreaName = personInfo.WorkAreaName;
            newPersonInfo.IdentityCard = personInfo.IdentityCard;
            newPersonInfo.IntoOutTime = personInfo.IntoOutTime;
            newPersonInfo.IntoOut = personInfo.IntoOut;
            newPersonInfo.Address = personInfo.Address;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据卡号和时间查询人员考勤信息
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <returns>人员实体</returns>
        public static Model.SitePerson_Checking GetPersonCheckByCardNo(string projectId, string cardNo, DateTime? intoOutTime)
        {
            return Funs.DB.SitePerson_Checking.FirstOrDefault(e => e.ProjectId == projectId && e.CardNo == cardNo && e.IntoOutTime == intoOutTime);
        }

        /// <summary>
        /// 根据人员考勤主键删除一个人员考勤管理信息
        /// </summary>
        /// <param name="checkingId">人员考勤主键</param>
        public static void DeletePersonInfoByCheckingId(string checkingId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.SitePerson_Checking personInfo = db.SitePerson_Checking.FirstOrDefault(e => e.CheckingId == checkingId);
            if (personInfo != null)
            {
                db.SitePerson_Checking.DeleteOnSubmit(personInfo);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据作业区域Id查询所有人员考勤信息数量
        /// </summary>
        /// <param name="workAreaId">作业区域Id</param>
        /// <returns>人员考勤信息数量</returns>
        public static int GetCheckingCountByWorkAreaId(string workAreaId)
        {
            return (from x in Funs.DB.SitePerson_Checking where x.WorkAreaId == workAreaId select x).ToList().Count();
        }


        /// <summary>
        /// 根据人员考勤主键删除一个人员考勤管理信息
        /// </summary>
        /// <param name="checkingId">人员考勤主键</param>
        public static void DeletePersonInOutByCheckingId(string PersonInOutId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.SitePerson_PersonInOut personInOut = db.SitePerson_PersonInOut.FirstOrDefault(e => e.PersonInOutId == PersonInOutId);
            if (personInOut != null)
            {
                db.SitePerson_PersonInOut.DeleteOnSubmit(personInOut);
                db.SubmitChanges();
            }
        }
    }
}
