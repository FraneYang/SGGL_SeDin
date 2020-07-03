using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class WorkContactApproveService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <returns></returns>
        public static IEnumerable getListData(string workContactId)
        {
            return from x in db.Unqualified_WorkContactApprove
                   where x.WorkContactId == workContactId && x.ApproveDate != null && x.ApproveType != "S"
                   orderby x.ApproveDate
                   select new
                   {
                       x.WorkContactApproveId,
                       x.WorkContactId,
                       ApproveMan = (from y in db.Sys_User where y.UserId == x.ApproveMan select y.UserName).First(),
                       x.ApproveDate,
                       x.ApproveIdea,
                       x.IsAgree,
                       x.ApproveType,
                       UserName = (from y in db.Sys_User where y.UserId == x.ApproveMan select y.UserName).First(),
                       IsAgreeName = x.IsAgree == true ? "同意" : "不同意"
                   };
        }


        /// <summary>
        /// 根据工作联系单主键删除对应的所有工作联系单审批信息
        /// </summary>
        /// <param name="noticeId">工作联系单主键</param>
        public static void DeleteWorkContactApproveByWorkContactId(string noticeId)
        {
            Model.SGGLDB db = Funs.DB;
            var data = (from x in db.Unqualified_WorkContactApprove where x.WorkContactId == noticeId select x).ToList();
            if (data != null)
            {
                db.Unqualified_WorkContactApprove.DeleteAllOnSubmit(data);
            }

            db.SubmitChanges();
        }
        /// <summary>
        /// 增加工作联系单审批信息
        /// </summary>
        /// <param name="workContactApprove">工作联系单审批实体</param>
        public static void AddWorkContactApprove(Model.Unqualified_WorkContactApprove workContactApprove)
        {
            Model.SGGLDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Unqualified_WorkContactApprove));
            Model.Unqualified_WorkContactApprove newWorkContactApprove = new Model.Unqualified_WorkContactApprove();
            newWorkContactApprove.WorkContactApproveId = newKeyID;
            newWorkContactApprove.WorkContactId = workContactApprove.WorkContactId;
            newWorkContactApprove.ApproveMan = workContactApprove.ApproveMan;
            newWorkContactApprove.ApproveDate = workContactApprove.ApproveDate;
            newWorkContactApprove.ApproveIdea = workContactApprove.ApproveIdea;
            newWorkContactApprove.IsAgree = workContactApprove.IsAgree;
            newWorkContactApprove.ApproveType = workContactApprove.ApproveType;

            db.Unqualified_WorkContactApprove.InsertOnSubmit(newWorkContactApprove);
            db.SubmitChanges();

        }
        public static string AddWorkContactApproveForApi(Model.Unqualified_WorkContactApprove workContactApprove)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                string newKeyID = SQLHelper.GetNewID(typeof(Model.Unqualified_WorkContactApprove));
                Model.Unqualified_WorkContactApprove newWorkContactApprove = new Model.Unqualified_WorkContactApprove();
                newWorkContactApprove.WorkContactApproveId = newKeyID;
                newWorkContactApprove.WorkContactId = workContactApprove.WorkContactId;
                newWorkContactApprove.ApproveMan = workContactApprove.ApproveMan;
                newWorkContactApprove.ApproveDate = workContactApprove.ApproveDate;
                newWorkContactApprove.ApproveIdea = workContactApprove.ApproveIdea;
                newWorkContactApprove.IsAgree = workContactApprove.IsAgree;
                newWorkContactApprove.ApproveType = workContactApprove.ApproveType;

                db.Unqualified_WorkContactApprove.InsertOnSubmit(newWorkContactApprove);
                db.SubmitChanges();
                return newKeyID;
            }
        }
        public static Model.Unqualified_WorkContactApprove GetSee(string WorkContactId, string userId)
        {
            return db.Unqualified_WorkContactApprove.FirstOrDefault(x => x.WorkContactId == WorkContactId && x.ApproveType == "S" && x.ApproveMan == userId && x.ApproveDate == null);
        }
        public static void See(string WorkContactId, string userId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                var res = db.Unqualified_WorkContactApprove.FirstOrDefault(x => x.WorkContactId == WorkContactId && x.ApproveType == "S" && x.ApproveMan == userId && x.ApproveDate == null);
                if (res != null)
                {
                    res.ApproveDate = DateTime.Now;
                    db.SubmitChanges();
                }
            }
        }
        /// <summary>
        /// 根据工作联系单Id获取一个工作联系单审批信息
        /// </summary>
        /// <param name="noticeId">工作联系单Id</param>
        /// <returns>一个工作联系单审批实体</returns>
        public static Model.Unqualified_WorkContactApprove GetWorkContactApproveByWorkContactId(string noticeId)
        {
            return db.Unqualified_WorkContactApprove.FirstOrDefault(x => x.WorkContactId == noticeId && x.ApproveDate == null && x.ApproveType != "S");
        }
        /// <summary>
        /// 修改工作联系单审批信息
        /// </summary>
        /// <param name="workContactApprove">工作联系单审批实体</param>
        public static void UpdateWorkContactApprove(Model.Unqualified_WorkContactApprove workContactApprove)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Unqualified_WorkContactApprove newWorkContactApprove = db.Unqualified_WorkContactApprove.First(e => e.WorkContactApproveId == workContactApprove.WorkContactApproveId);
            newWorkContactApprove.WorkContactId = workContactApprove.WorkContactId;
            newWorkContactApprove.ApproveMan = workContactApprove.ApproveMan;
            newWorkContactApprove.ApproveDate = workContactApprove.ApproveDate;
            newWorkContactApprove.ApproveIdea = workContactApprove.ApproveIdea;
            newWorkContactApprove.IsAgree = workContactApprove.IsAgree;
            newWorkContactApprove.ApproveType = workContactApprove.ApproveType;

            db.SubmitChanges();
        }

        /// <summary>
        /// 分包负责人审核
        /// </summary>
        /// <param name="workContactId"></param>
        /// <returns></returns>
        public static Model.Unqualified_WorkContactApprove GetAudit1(string workContactId)
        {
            return db.Unqualified_WorkContactApprove.OrderByDescending(x => x.ApproveDate).FirstOrDefault(x => x.WorkContactId == workContactId && x.ApproveType == BLL.Const.WorkContact_Audit1);
        }

        /// <summary>
        /// 总包专工回复
        /// </summary>
        /// <param name="workContactId"></param>
        /// <returns></returns>
        public static Model.Unqualified_WorkContactApprove GetAudit2(string workContactId)
        {
            return db.Unqualified_WorkContactApprove.OrderByDescending(x => x.ApproveDate).FirstOrDefault(x => x.WorkContactId == workContactId && (x.ApproveType == BLL.Const.WorkContact_Audit2 || x.ApproveType == BLL.Const.WorkContact_Audit2R));
        }

        /// <summary>
        /// 总包负责人审核
        /// </summary>
        /// <param name="workContactId"></param>
        /// <returns></returns>
        public static Model.Unqualified_WorkContactApprove GetAudit3(string workContactId)
        {
            return db.Unqualified_WorkContactApprove.OrderByDescending(x => x.ApproveDate).FirstOrDefault(x => x.WorkContactId == workContactId && x.ApproveType == BLL.Const.WorkContact_Audit3);
        }

        /// <summary>
        /// 分包专工回复
        /// </summary>
        /// <param name="workContactId"></param>
        /// <returns></returns>
        public static Model.Unqualified_WorkContactApprove GetAudit4(string workContactId)
        {
            return db.Unqualified_WorkContactApprove.OrderByDescending(x => x.ApproveDate).FirstOrDefault(x => x.WorkContactId == workContactId && (x.ApproveType == BLL.Const.WorkContact_Audit4 || x.ApproveType == BLL.Const.WorkContact_Audit1R));
        }

        public static Model.Unqualified_WorkContactApprove GetComplie(string workContactId)
        {
            return db.Unqualified_WorkContactApprove.FirstOrDefault(x => x.WorkContactId == workContactId && x.ApproveType == BLL.Const.WorkContact_Compile);
        }
        public static Model.Unqualified_WorkContactApprove GetWorkContactApproveById(string WorkContactApproveId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                return db.Unqualified_WorkContactApprove.FirstOrDefault(x => x.WorkContactApproveId == WorkContactApproveId);
            }
        }
        public static List<Model.Unqualified_WorkContactApprove> getListDataByIdForApi(string workContactId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                var q = from x in db.Unqualified_WorkContactApprove
                        where x.WorkContactId == workContactId && x.ApproveDate != null && x.ApproveType != "S"
                        orderby x.ApproveDate
                        select new
                        {
                            x.WorkContactApproveId,
                            x.WorkContactId,
                            ApproveMan = (from y in db.Sys_User where y.UserId == x.ApproveMan select y.UserName).First(),
                            x.ApproveDate,
                            x.ApproveIdea,
                            x.IsAgree,
                            x.ApproveType,
                            UserName = (from y in db.Sys_User where y.UserId == x.ApproveMan select y.UserName).First(),

                        };
                List<Model.Unqualified_WorkContactApprove> res = new List<Model.Unqualified_WorkContactApprove>();
                var list = q.ToList();
                foreach (var item in list)
                {
                    Model.Unqualified_WorkContactApprove wc = new Model.Unqualified_WorkContactApprove();
                    wc.WorkContactApproveId = item.WorkContactApproveId;
                    wc.WorkContactId = item.WorkContactId;
                    wc.ApproveMan = item.ApproveMan;
                    wc.ApproveDate = item.ApproveDate;
                    wc.ApproveIdea = item.ApproveIdea;
                    wc.IsAgree = item.IsAgree;
                    wc.ApproveType = item.ApproveType;
                    wc.ApproveMan = item.UserName;

                    res.Add(wc);
                }
                return res;
            }
        }
        public static Model.Unqualified_WorkContactApprove getCurrApproveForApi(string id)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Unqualified_WorkContactApprove newWorkContactApprove = db.Unqualified_WorkContactApprove.FirstOrDefault(e => e.WorkContactId == id && e.ApproveType != "S" && e.ApproveDate == null);
                if (newWorkContactApprove != null)
                {
                    Model.Sys_User user = BLL.UserService.GetUserByUserId(newWorkContactApprove.ApproveMan);
                    if (user != null)
                    {
                        newWorkContactApprove.ApproveIdea = user.UserName;
                    }
                }
                return newWorkContactApprove;
            }
        } /// <summary>
          /// 修改工作联系单审批信息
          /// </summary>
          /// <param name="workContactApprove">工作联系单审批实体</param>
        public static void UpdateWorkContactApproveForApi(Model.Unqualified_WorkContactApprove workContactApprove)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {

                Model.Unqualified_WorkContactApprove newWorkContactApprove = db.Unqualified_WorkContactApprove.FirstOrDefault(e => e.WorkContactApproveId == workContactApprove.WorkContactApproveId);
                if (newWorkContactApprove != null)
                {
                    if (!string.IsNullOrEmpty(workContactApprove.WorkContactId))
                        newWorkContactApprove.WorkContactId = workContactApprove.WorkContactId;
                    if (!string.IsNullOrEmpty(workContactApprove.ApproveMan))
                        newWorkContactApprove.ApproveMan = workContactApprove.ApproveMan;
                    if (workContactApprove.ApproveDate.HasValue)
                        newWorkContactApprove.ApproveDate = workContactApprove.ApproveDate;
                    if (!string.IsNullOrEmpty(workContactApprove.ApproveIdea))
                        newWorkContactApprove.ApproveIdea = workContactApprove.ApproveIdea;
                    if (workContactApprove.IsAgree.HasValue)
                        newWorkContactApprove.IsAgree = workContactApprove.IsAgree;
                    if (!string.IsNullOrEmpty(workContactApprove.ApproveType))
                        newWorkContactApprove.ApproveType = workContactApprove.ApproveType;

                    db.SubmitChanges();
                }
            }
        }
    }
}
