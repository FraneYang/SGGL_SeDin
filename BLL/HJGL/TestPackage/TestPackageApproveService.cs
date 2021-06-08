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
        public static Model.PTP_TestPackageApprove GetTestPackageApproveById(string ItemEndCheckListId)
        {
            return db.PTP_TestPackageApprove.FirstOrDefault(x => x.ItemEndCheckListId == ItemEndCheckListId && x.ApproveDate == null);
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
            newApprove.ItemEndCheckListId = approve.ItemEndCheckListId;
            newApprove.ApproveType = approve.ApproveType;
            db.SubmitChanges();
        }

        public static Model.PTP_TestPackageApprove UpdateTestPackageApproveForApi(Model.PTP_TestPackageApprove approve)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.PTP_TestPackageApprove newApprove = db.PTP_TestPackageApprove.FirstOrDefault(e => e.ApproveId == approve.ApproveId);
                if (newApprove != null)
                {
                    if (!string.IsNullOrEmpty(approve.ApproveMan))
                        newApprove.ApproveMan = approve.ApproveMan;
                    if (approve.ApproveDate.HasValue)
                        newApprove.ApproveDate = approve.ApproveDate;
                    if (!string.IsNullOrEmpty(approve.Opinion))
                        newApprove.Opinion = approve.Opinion;
                    if (!string.IsNullOrEmpty(approve.ItemEndCheckListId))
                        newApprove.ItemEndCheckListId = approve.ItemEndCheckListId;
                    if (!string.IsNullOrEmpty(approve.ApproveType))
                        newApprove.ApproveType = approve.ApproveType;

                    db.SubmitChanges();
                    return newApprove;
                }
                return null;
            };

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
            newApprove.ItemEndCheckListId = approve.ItemEndCheckListId;
            newApprove.ApproveType = approve.ApproveType;
            db.PTP_TestPackageApprove.InsertOnSubmit(newApprove);
            db.SubmitChanges();

        }

        /// <summary>
        /// 增加尾项检查审批信息
        /// </summary>
        /// <param name="managerRuleApprove">尾项检查审批实体</param>
        public static string AddTestPackageApproveForApi(Model.PTP_TestPackageApprove approve)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                string newKeyID = SQLHelper.GetNewID(typeof(Model.PTP_TestPackageApprove));
                Model.PTP_TestPackageApprove newApprove = new Model.PTP_TestPackageApprove();
                newApprove.ApproveId = newKeyID;
                newApprove.ApproveMan = approve.ApproveMan;
                newApprove.ApproveDate = approve.ApproveDate;
                newApprove.Opinion = approve.Opinion;
                newApprove.ItemEndCheckListId = approve.ItemEndCheckListId;
                newApprove.ApproveType = approve.ApproveType;
                db.PTP_TestPackageApprove.InsertOnSubmit(newApprove);
                db.SubmitChanges();
                return newKeyID;
            }
        }

        /// <summary>
        /// 根据试压包主键删除所有办理记录
        /// </summary>
        /// <param name="PTP_Id"></param>
        public static void DeleteAllTestPackageApproveByID(string ItemEndCheckListId)
        {
            Model.SGGLDB db = Funs.DB;
            var Approve = from x in db.PTP_TestPackageApprove where x.ItemEndCheckListId == ItemEndCheckListId select x;
            if (Approve != null)
            {
                db.PTP_TestPackageApprove.DeleteAllOnSubmit(Approve);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除办理记录
        /// </summary>
        /// <param name="PTP_Id"></param>
        public static void DeleteAllTestPackageApproveByApproveID(string ApproveId)
        {
            Model.SGGLDB db = Funs.DB;
            var Approve = (from x in db.PTP_TestPackageApprove where x.ApproveId == ApproveId select x).FirstOrDefault();
            if (Approve != null)
            {
                db.PTP_TestPackageApprove.DeleteOnSubmit(Approve);
                db.SubmitChanges();
            }
        }
    }
}
