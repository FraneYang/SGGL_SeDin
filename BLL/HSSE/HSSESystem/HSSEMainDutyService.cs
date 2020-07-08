using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// 安全主体责任
    /// </summary>
    public static class HSSEMainDutyService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取安全主体责任
        /// </summary>
        /// <param name="hsseMainDutyId"></param>
        /// <returns></returns>
        public static Model.HSSESystem_HSSEMainDuty GetHSSEMainDutyById(string hsseMainDutyId)
        {
            return new Model.SGGLDB(Funs.ConnString).HSSESystem_HSSEMainDuty.FirstOrDefault(e => e.HSSEMainDutyId == hsseMainDutyId);
        }

        /// <summary>
        /// 根据岗位Id获取安全主体责任集合
        /// </summary>
        /// <param name="workPostId"></param>
        /// <returns></returns>
        public static List<Model.HSSESystem_HSSEMainDuty> GetHSSEMainDutyByWorkPostId(string workPostId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).HSSESystem_HSSEMainDuty where x.WorkPostId == workPostId select x).ToList();
        }

        /// <summary>
        /// 添加安全主体责任
        /// </summary>
        /// <param name="hsseMainDuty"></param>
        public static void AddHSSEMainDuty(Model.HSSESystem_HSSEMainDuty hsseMainDuty)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.HSSESystem_HSSEMainDuty newHSSEMainDuty = new Model.HSSESystem_HSSEMainDuty
            {
                HSSEMainDutyId = hsseMainDuty.HSSEMainDutyId,
                WorkPostId = hsseMainDuty.WorkPostId,
                Duties = hsseMainDuty.Duties,
                Remark = hsseMainDuty.Remark,
                SortIndex = hsseMainDuty.SortIndex
            };
            db.HSSESystem_HSSEMainDuty.InsertOnSubmit(newHSSEMainDuty);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改安全主体责任
        /// </summary>
        /// <param name="hsseMainDuty"></param>
        public static void UpdateHSSEMainDuty(Model.HSSESystem_HSSEMainDuty hsseMainDuty)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.HSSESystem_HSSEMainDuty newHSSEMainDuty = db.HSSESystem_HSSEMainDuty.FirstOrDefault(e => e.HSSEMainDutyId == hsseMainDuty.HSSEMainDutyId);
            if (newHSSEMainDuty != null)
            {
                newHSSEMainDuty.Duties = hsseMainDuty.Duties;
                newHSSEMainDuty.Remark = hsseMainDuty.Remark;
                newHSSEMainDuty.SortIndex = hsseMainDuty.SortIndex;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除安全主体责任
        /// </summary>
        /// <param name="hsseMainDutyId"></param>
        public static void DeleteHSSEMainDuty(string hsseMainDutyId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.HSSESystem_HSSEMainDuty hsseMainDuty = db.HSSESystem_HSSEMainDuty.FirstOrDefault(e => e.HSSEMainDutyId == hsseMainDutyId);
            if (hsseMainDuty != null)
            {
                db.HSSESystem_HSSEMainDuty.DeleteOnSubmit(hsseMainDuty);
                db.SubmitChanges();
            }
        }
    }
}