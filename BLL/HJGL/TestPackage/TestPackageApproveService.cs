using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class TestPackageApproveService
    {
        public static Model.SGGLDB db = Funs.DB;
        public static Model.PTP_TestPackageApprove GetTestPackageApproveById(string PTP_ID)
        {
            return db.PTP_TestPackageApprove.FirstOrDefault(x => x.PTP_ID == PTP_ID && x.ApproveDate == null);
        }
        /// <summary>
        /// 修改尾项检查审批信息
        /// </summary>
        /// <param name="managerRuleApprove">尾项检查审批实体</param>
        public static void UpdateTestPackageApprove(Model.PTP_TestPackageApprove approve)
        {
            Model.SGGLDB db = Funs.DB;
            Model.PTP_TestPackageApprove newApprove = db.PTP_TestPackageApprove.First(e => e.ApproveId == approve.ApproveId && e.ApproveDate == null);
            newApprove.ApproveId = approve.ApproveId;
            newApprove.ApproveMan = approve.ApproveMan;
            newApprove.ApproveDate = approve.ApproveDate;
            newApprove.Opinion = approve.Opinion;
            newApprove.PTP_ID = approve.PTP_ID;
            newApprove.ApproveType = approve.ApproveType;
            db.SubmitChanges();
        }
        /// <summary>
        /// 增加尾项检查审批信息
        /// </summary>
        /// <param name="managerRuleApprove">尾项检查审批实体</param>
        public static void AddTestPackageApprove(Model.PTP_TestPackageApprove approve)
        {
            Model.SGGLDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.PTP_TestPackageApprove));
            Model.PTP_TestPackageApprove newApprove = new Model.PTP_TestPackageApprove();
            newApprove.ApproveId = approve.ApproveId;
            newApprove.ApproveMan = approve.ApproveMan;
            newApprove.ApproveDate = approve.ApproveDate;
            newApprove.Opinion = approve.Opinion;
            newApprove.PTP_ID = approve.PTP_ID;
            newApprove.ApproveType = approve.ApproveType;
            db.PTP_TestPackageApprove.InsertOnSubmit(newApprove);
            db.SubmitChanges();

        }
        /// <summary>
        /// 根据试压包主键删除所有办理记录
        /// </summary>
        /// <param name="PTP_Id"></param>
        public static void DeleteAllTestPackageApproveByID(string PTP_Id)
        {
            Model.SGGLDB db = Funs.DB;
            var Approve = from x in db.PTP_TestPackageApprove where x.PTP_ID == PTP_Id select x;
            if (Approve != null)
            {
                db.PTP_TestPackageApprove.DeleteAllOnSubmit(Approve);
                db.SubmitChanges();
            }
        }
    }
}
