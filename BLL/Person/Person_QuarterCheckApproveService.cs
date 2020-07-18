using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class Person_QuarterCheckApproveService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <param name="QuarterCheckId">人员Id</param>
        /// <returns>人员信息</returns>
        public static List<Model.Person_QuarterCheckApprove> GetApproveByQuarterCheckId(string QuarterCheckId)
        {
            var getApprove=(from x in new Model.SGGLDB(Funs.ConnString).Person_QuarterCheckApprove where x.QuarterCheckId == QuarterCheckId && x.ApproveDate == null select x).ToList();
            return getApprove;
        }
        public static Model.Person_QuarterCheckApprove GetApproveByQuarterCheckIdUserId(string QuarterCheckId, string UserId)
        {
            return new Model.SGGLDB(Funs.ConnString).Person_QuarterCheckApprove.FirstOrDefault(e => e.QuarterCheckId == QuarterCheckId && e.UserId == UserId);
        }
        public static List<Model.Person_QuarterCheckApprove> GetCheckApproveListById(string QuarterCheckId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Person_QuarterCheckApprove where x.QuarterCheckId == QuarterCheckId select x).ToList();

        }
        /// <summary>
        /// 增加人员总结信息
        /// </summary>
        /// <param name="user">人员实体</param>
        public static void AddCheckApprove(Model.Person_QuarterCheckApprove contruct)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Person_QuarterCheckApprove newcontruct = new Model.Person_QuarterCheckApprove
            {
                ApproveId = contruct.ApproveId,
                QuarterCheckId = contruct.QuarterCheckId,
                UserId=contruct.UserId,
            };
            db.Person_QuarterCheckApprove.InsertOnSubmit(newcontruct);
            db.SubmitChanges();
        }
        /// <summary>
        /// 修改人
        /// </summary>
        /// <param name="user">实体</param>
        public static void UpdateCheckApprove(Model.Person_QuarterCheckApprove Approve)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Person_QuarterCheckApprove newApprove = db.Person_QuarterCheckApprove.FirstOrDefault(e => e.ApproveId == Approve.ApproveId);
            if (newApprove != null)
            {
                newApprove.ApproveDate = Approve.ApproveDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据人员Id删除
        /// </summary>
        /// <param name="PersonTotalId"></param>
        public static void DeleteCheckApprove(string ApproveId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Person_QuarterCheckApprove check = db.Person_QuarterCheckApprove.FirstOrDefault(e => e.ApproveId == ApproveId);
            if (check != null)
            {
                db.Person_QuarterCheckApprove.DeleteOnSubmit(check);
                db.SubmitChanges();
            }
        }
    }
}
