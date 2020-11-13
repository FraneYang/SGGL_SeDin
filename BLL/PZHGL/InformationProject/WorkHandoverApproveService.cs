using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class WorkHandoverApproveService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <returns></returns>
        public static IEnumerable getListData(string WorkHandoverId)
        {
            return from x in db.ZHGL_WorkHandoverApprove
                   where x.WorkHandoverId == WorkHandoverId && x.ApproveDate != null
                   orderby x.ApproveDate
                   select new
                   {
                       x.WorkHandoverApproveId,
                       x.WorkHandoverId,
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
        /// 根据工作交接主键删除对应的所有工作交接审批信息
        /// </summary>
        /// <param name="noticeId">工作交接主键</param>
        public static void DeleteWorkHandoverApproveByWorkHandoverId(string noticeId)
        {
            Model.SGGLDB db = Funs.DB;
            var data = (from x in db.ZHGL_WorkHandoverApprove where x.WorkHandoverId == noticeId select x).ToList();
            if (data != null)
            {
                db.ZHGL_WorkHandoverApprove.DeleteAllOnSubmit(data);
            }

            db.SubmitChanges();
        }
        /// <summary>
        /// 增加工作交接审批信息
        /// </summary>
        /// <param name="WorkHandoverApprove">工作交接审批实体</param>
        public static void AddWorkHandoverApprove(Model.ZHGL_WorkHandoverApprove WorkHandoverApprove)
        {
            Model.SGGLDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.ZHGL_WorkHandoverApprove));
            Model.ZHGL_WorkHandoverApprove newWorkHandoverApprove = new Model.ZHGL_WorkHandoverApprove();
            newWorkHandoverApprove.WorkHandoverApproveId = newKeyID;
            newWorkHandoverApprove.WorkHandoverId = WorkHandoverApprove.WorkHandoverId;
            newWorkHandoverApprove.ApproveMan = WorkHandoverApprove.ApproveMan;
            newWorkHandoverApprove.ApproveDate = WorkHandoverApprove.ApproveDate;
            newWorkHandoverApprove.ApproveIdea = WorkHandoverApprove.ApproveIdea;
            newWorkHandoverApprove.IsAgree = WorkHandoverApprove.IsAgree;
            newWorkHandoverApprove.ApproveType = WorkHandoverApprove.ApproveType;

            db.ZHGL_WorkHandoverApprove.InsertOnSubmit(newWorkHandoverApprove);
            db.SubmitChanges();

        }
        public static string AddWorkHandoverApproveForApi(Model.ZHGL_WorkHandoverApprove WorkHandoverApprove)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                string newKeyID = SQLHelper.GetNewID(typeof(Model.ZHGL_WorkHandoverApprove));
                Model.ZHGL_WorkHandoverApprove newWorkHandoverApprove = new Model.ZHGL_WorkHandoverApprove();
                newWorkHandoverApprove.WorkHandoverApproveId = newKeyID;
                newWorkHandoverApprove.WorkHandoverId = WorkHandoverApprove.WorkHandoverId;
                newWorkHandoverApprove.ApproveMan = WorkHandoverApprove.ApproveMan;
                newWorkHandoverApprove.ApproveDate = WorkHandoverApprove.ApproveDate;
                newWorkHandoverApprove.ApproveIdea = WorkHandoverApprove.ApproveIdea;
                newWorkHandoverApprove.IsAgree = WorkHandoverApprove.IsAgree;
                newWorkHandoverApprove.ApproveType = WorkHandoverApprove.ApproveType;

                db.ZHGL_WorkHandoverApprove.InsertOnSubmit(newWorkHandoverApprove);
                db.SubmitChanges();
                return newKeyID;
            }
        }

        /// <summary>
        /// 根据工作交接Id获取一个工作交接审批信息
        /// </summary>
        /// <param name="noticeId">工作交接Id</param>
        /// <returns>一个工作交接审批实体</returns>
        public static Model.ZHGL_WorkHandoverApprove GetWorkHandoverApproveByWorkHandoverId(string constructionPlanId)
        {
            return db.ZHGL_WorkHandoverApprove.FirstOrDefault(x => x.WorkHandoverId == constructionPlanId && x.ApproveDate == null);
        }
        /// <summary>
        /// 修改工作交接审批信息
        /// </summary>
        /// <param name="WorkHandoverApprove">工作交接审批实体</param>
        public static void UpdateWorkHandoverApprove(Model.ZHGL_WorkHandoverApprove WorkHandoverApprove)
        {
            Model.SGGLDB db = Funs.DB;
            Model.ZHGL_WorkHandoverApprove newWorkHandoverApprove = db.ZHGL_WorkHandoverApprove.First(e => e.WorkHandoverApproveId == WorkHandoverApprove.WorkHandoverApproveId);
            newWorkHandoverApprove.WorkHandoverId = WorkHandoverApprove.WorkHandoverId;
            newWorkHandoverApprove.ApproveMan = WorkHandoverApprove.ApproveMan;
            newWorkHandoverApprove.ApproveDate = WorkHandoverApprove.ApproveDate;
            newWorkHandoverApprove.ApproveIdea = WorkHandoverApprove.ApproveIdea;
            newWorkHandoverApprove.IsAgree = WorkHandoverApprove.IsAgree;
            newWorkHandoverApprove.ApproveType = WorkHandoverApprove.ApproveType;

            db.SubmitChanges();
        }

        public static Model.ZHGL_WorkHandoverApprove GetComplie(string WorkHandoverId)
        {
            return db.ZHGL_WorkHandoverApprove.FirstOrDefault(x => x.WorkHandoverId == WorkHandoverId && x.ApproveType == BLL.Const.WorkHandover_Compile);
        }
        public static Model.ZHGL_WorkHandoverApprove GetWorkHandoverApproveById(string WorkHandoverApproveId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                return db.ZHGL_WorkHandoverApprove.FirstOrDefault(x => x.WorkHandoverApproveId == WorkHandoverApproveId);
            }
        }
        public static List<Model.ZHGL_WorkHandoverApprove> getListDataByIdForApi(string WorkHandoverId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                var q = from x in db.ZHGL_WorkHandoverApprove
                        where x.WorkHandoverId == WorkHandoverId && x.ApproveDate != null && x.ApproveType != "S"
                        orderby x.ApproveDate
                        select new
                        {
                            x.WorkHandoverApproveId,
                            x.WorkHandoverId,
                            ApproveMan = (from y in db.Sys_User where y.UserId == x.ApproveMan select y.UserName).First(),
                            x.ApproveDate,
                            x.ApproveIdea,
                            x.IsAgree,
                            x.ApproveType,
                            UserName = (from y in db.Sys_User where y.UserId == x.ApproveMan select y.UserName).First(),

                        };
                List<Model.ZHGL_WorkHandoverApprove> res = new List<Model.ZHGL_WorkHandoverApprove>();
                var list = q.ToList();
                foreach (var item in list)
                {
                    Model.ZHGL_WorkHandoverApprove wc = new Model.ZHGL_WorkHandoverApprove();
                    wc.WorkHandoverApproveId = item.WorkHandoverApproveId;
                    wc.WorkHandoverId = item.WorkHandoverId;
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
        public static Model.ZHGL_WorkHandoverApprove getCurrApproveForApi(string id)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.ZHGL_WorkHandoverApprove newWorkHandoverApprove = db.ZHGL_WorkHandoverApprove.FirstOrDefault(e => e.WorkHandoverId == id && e.ApproveDate == null);
                if (newWorkHandoverApprove != null)
                {
                    Model.Sys_User user = BLL.UserService.GetUserByUserId(newWorkHandoverApprove.ApproveMan);
                    if (user != null)
                    {
                        newWorkHandoverApprove.ApproveIdea = user.UserName;
                    }
                }
                return newWorkHandoverApprove;
            }
        } /// <summary>
          /// 修改工作交接审批信息
          /// </summary>
          /// <param name="WorkHandoverApprove">工作交接审批实体</param>
        public static void UpdateWorkHandoverApproveForApi(Model.ZHGL_WorkHandoverApprove WorkHandoverApprove)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {

                Model.ZHGL_WorkHandoverApprove newWorkHandoverApprove = db.ZHGL_WorkHandoverApprove.FirstOrDefault(e => e.WorkHandoverApproveId == WorkHandoverApprove.WorkHandoverApproveId);
                if (newWorkHandoverApprove != null)
                {
                    if (!string.IsNullOrEmpty(WorkHandoverApprove.WorkHandoverId))
                        newWorkHandoverApprove.WorkHandoverId = WorkHandoverApprove.WorkHandoverId;
                    if (!string.IsNullOrEmpty(WorkHandoverApprove.ApproveMan))
                        newWorkHandoverApprove.ApproveMan = WorkHandoverApprove.ApproveMan;
                    if (WorkHandoverApprove.ApproveDate.HasValue)
                        newWorkHandoverApprove.ApproveDate = WorkHandoverApprove.ApproveDate;
                    if (!string.IsNullOrEmpty(WorkHandoverApprove.ApproveIdea))
                        newWorkHandoverApprove.ApproveIdea = WorkHandoverApprove.ApproveIdea;
                    if (WorkHandoverApprove.IsAgree.HasValue)
                        newWorkHandoverApprove.IsAgree = WorkHandoverApprove.IsAgree;
                    if (!string.IsNullOrEmpty(WorkHandoverApprove.ApproveType))
                        newWorkHandoverApprove.ApproveType = WorkHandoverApprove.ApproveType;

                    db.SubmitChanges();
                }
            }
        }
    }
}
