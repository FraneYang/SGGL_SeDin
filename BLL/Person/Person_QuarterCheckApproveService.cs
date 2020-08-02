using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class Person_QuarterCheckApproveService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <param name="QuarterCheckId">人员Id</param>
        /// <returns>人员信息</returns>
        public static List<Model.Person_QuarterCheckApprove> GetApproveByQuarterCheckId(string QuarterCheckId)
        {
            var getApprove = (from x in Funs.DB.Person_QuarterCheckApprove where x.QuarterCheckId == QuarterCheckId && x.ApproveDate == null select x).ToList();
            return getApprove;
        }
        public static Model.Person_QuarterCheckApprove GetApproveByQuarterCheckIdUserId(string QuarterCheckId, string UserId)
        {
            return Funs.DB.Person_QuarterCheckApprove.FirstOrDefault(e => e.QuarterCheckId == QuarterCheckId && e.UserId == UserId);
        }
        public static List<Model.Person_QuarterCheckApprove> GetCheckApproveListById(string QuarterCheckId)
        {
            return (from x in Funs.DB.Person_QuarterCheckApprove where x.QuarterCheckId == QuarterCheckId select x).ToList();

        }
        /// <summary>
        /// 增加人员总结信息
        /// </summary>
        /// <param name="user">人员实体</param>
        public static void AddCheckApprove(Model.Person_QuarterCheckApprove contruct)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Person_QuarterCheckApprove newcontruct = new Model.Person_QuarterCheckApprove
            {
                ApproveId = contruct.ApproveId,
                QuarterCheckId = contruct.QuarterCheckId,
                UserId = contruct.UserId,
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
            Model.SGGLDB db = Funs.DB;
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
            Model.SGGLDB db = Funs.DB;
            Model.Person_QuarterCheckApprove check = db.Person_QuarterCheckApprove.FirstOrDefault(e => e.ApproveId == ApproveId);
            if (check != null)
            {
                db.Person_QuarterCheckApprove.DeleteOnSubmit(check);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主表id和用户id获取审批记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static Model.Person_QuarterCheckApprove getCurrApproveForApi(string id, string userId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Person_QuarterCheckApprove newApprove = db.Person_QuarterCheckApprove.FirstOrDefault(e => e.QuarterCheckId == id && e.UserId == userId && e.ApproveDate == null);
                //if (newApprove != null)
                //{
                //    Model.Sys_User user = BLL.UserService.GetUserByUserId(newApprove.ApproveMan);
                //    if (user != null)
                //    {
                //        newApprove.ApproveIdea = user.UserName;
                //    }
                //}
                return newApprove;
            }
        }

        /// <summary>
        /// 根据审批信息id更新审批记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static void UpdateApproveForApi(string approveId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Person_QuarterCheckApprove newApprove = db.Person_QuarterCheckApprove.FirstOrDefault(e => e.ApproveId == approveId);
                if (newApprove != null)
                {
                    newApprove.ApproveDate = DateTime.Now;
                    db.SubmitChanges();
                }
                var getApprove = (from x in db.Person_QuarterCheckApprove where x.QuarterCheckId == newApprove.QuarterCheckId && x.ApproveDate == null select x).ToList();
                if (getApprove.Count == 0)
                {
                    Model.Person_QuarterCheck Construct = BLL.Person_QuarterCheckService.GetPerson_QuarterCheckById(newApprove.QuarterCheckId);
                    if (Construct != null)
                    {
                        Construct.State = "1";
                        BLL.Person_QuarterCheckService.UpdatePerson_QuarterCheck(Construct);
                    }
                }
            }
        }
    }
}
