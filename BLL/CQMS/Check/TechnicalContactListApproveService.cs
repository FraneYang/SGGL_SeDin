using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class TechnicalContactListApproveService
    {
        public static Model.SGGLDB db = Funs.DB;
        public static Model.Check_TechnicalContactListApprove GetSee(string TechnicalContactListId, string userId)
        {
            return db.Check_TechnicalContactListApprove.FirstOrDefault(x => x.TechnicalContactListId == TechnicalContactListId && x.ApproveType == "S" && x.ApproveMan == userId && x.ApproveDate == null);
        }
        public static void See(string TechnicalContactListId, string userId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                var item = db.Check_TechnicalContactListApprove.FirstOrDefault(x => x.TechnicalContactListId == TechnicalContactListId && x.ApproveType == "S" && x.ApproveMan == userId && x.ApproveDate == null);
                if (item != null)
                {
                    item.ApproveDate = DateTime.Now;
                    db.SubmitChanges();
                }
            }
        }

        /// <summary>
        /// 获取工程联络单模板列表
        /// </summary>
        /// <param name="satartRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListData(string TechnicalContactListId)
        {
            var db = Funs.DB;
            return from x in db.Check_TechnicalContactListApprove
                   where x.TechnicalContactListId == TechnicalContactListId && x.ApproveDate != null && x.ApproveType != "S"
                   orderby x.ApproveDate
                   select new
                   {
                       x.TechnicalContactListApproveId,
                       x.TechnicalContactListId,
                       ApproveMan = (from y in db.Sys_User where y.UserId == x.ApproveMan select y.UserName).First(),
                       x.ApproveDate,
                       x.IsAgree,
                       x.ApproveIdea,
                       x.ApproveType,
                   };
        }

        /// <summary>
        /// 分包负责人审批信息
        /// </summary>
        /// <param name="TechnicalContactListId"></param>
        /// <returns></returns>
        public static Model.Check_TechnicalContactListApprove GetApprove(string TechnicalContactListId)
        {
            return db.Check_TechnicalContactListApprove.OrderByDescending(x => x.ApproveDate).FirstOrDefault(x => x.TechnicalContactListId == TechnicalContactListId && x.ApproveType == BLL.Const.TechnicalContactList_Audit1);
        }

        /// <summary>
        /// 根据工程联络单编号删除对应的所有工程联络单审批信息
        /// </summary>
        /// <param name="TechnicalContactListCode">工程联络单编号</param>
        public static void DeleteTechnicalContactListApprovesByTechnicalContactListId(string TechnicalContactListId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                var q = (from x in db.Check_TechnicalContactListApprove where x.TechnicalContactListId == TechnicalContactListId select x).ToList();
                db.Check_TechnicalContactListApprove.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
        /// <summary>
        /// 增加工程联络单审批信息
        /// </summary>
        /// <param name="managerRuleApprove">工程联络单审批实体</param>
        public static string AddTechnicalContactListApprove(Model.Check_TechnicalContactListApprove approve)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                string newKeyID = SQLHelper.GetNewID(typeof(Model.Check_TechnicalContactListApprove));
                Model.Check_TechnicalContactListApprove newApprove = new Model.Check_TechnicalContactListApprove();
                newApprove.TechnicalContactListApproveId = newKeyID;
                newApprove.TechnicalContactListId = approve.TechnicalContactListId;
                newApprove.ApproveMan = approve.ApproveMan;
                newApprove.ApproveDate = approve.ApproveDate;
                newApprove.ApproveIdea = approve.ApproveIdea;
                newApprove.IsAgree = approve.IsAgree;
                newApprove.ApproveType = approve.ApproveType;

                db.Check_TechnicalContactListApprove.InsertOnSubmit(newApprove);
                db.SubmitChanges();
                return newKeyID;
            }


        }
        /// <summary>
        /// 修改工程联络单审批信息
        /// </summary>
        /// <param name="managerRuleApprove">工程联络单审批实体</param>
        public static void UpdateTechnicalContactListApprove(Model.Check_TechnicalContactListApprove approve)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Check_TechnicalContactListApprove newApprove = db.Check_TechnicalContactListApprove.First(e => e.TechnicalContactListApproveId == approve.TechnicalContactListApproveId && e.ApproveDate == null);
                newApprove.TechnicalContactListId = approve.TechnicalContactListId;
                newApprove.ApproveMan = approve.ApproveMan;
                newApprove.ApproveDate = approve.ApproveDate;
                newApprove.ApproveIdea = approve.ApproveIdea;
                newApprove.IsAgree = approve.IsAgree;
                newApprove.ApproveType = approve.ApproveType;

                db.SubmitChanges();
            }
        }


        /// <summary>
        /// 根据工程联络单编号获取一个工程联络单审批信息
        /// </summary>
        /// <param name="TechnicalContactListCode">工程联络单编号</param>
        /// <returns>一个工程联络单审批实体</returns>
        public static Model.Check_TechnicalContactListApprove GetTechnicalContactListApproveByTechnicalContactListId(string TechnicalContactListId)
        {
            return db.Check_TechnicalContactListApprove.FirstOrDefault(x => x.TechnicalContactListId == TechnicalContactListId && x.ApproveDate == null && x.ApproveType != "S");
        }
        /// <summary>
        /// 根据工程联络单审批编号获取一个工程联络单审批信息
        /// </summary>
        /// <param name="TechnicalContactListCode">工程联络单编号</param>
        /// <returns>一个工程联络单审批实体</returns>
        public static Model.Check_TechnicalContactListApprove GetTechnicalContactListApproveByApproveId(string approveId)
        {
            return db.Check_TechnicalContactListApprove.FirstOrDefault(x => x.TechnicalContactListApproveId == approveId);
        }
        public static Model.Check_TechnicalContactListApprove GetTechnicalContactListApproveByApproveIdForApi(string approveId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                return db.Check_TechnicalContactListApprove.FirstOrDefault(x => x.TechnicalContactListApproveId == approveId);
            }
        }
        public static Model.Check_TechnicalContactListApprove GetComplie(string TechnicalContactListId)
        {
            return db.Check_TechnicalContactListApprove.FirstOrDefault(x => x.TechnicalContactListId == TechnicalContactListId && x.ApproveType == BLL.Const.TechnicalContactList_Compile);
        }

        /// <summary>
        /// 总包负责人审批信息
        /// </summary>
        /// <param name="TechnicalContactListId"></param>
        /// <returns></returns>
        public static Model.Check_TechnicalContactListApprove GetApprove2(string TechnicalContactListId)
        {
            return db.Check_TechnicalContactListApprove.OrderByDescending(x => x.ApproveDate).FirstOrDefault(x => x.TechnicalContactListId == TechnicalContactListId && x.ApproveType == BLL.Const.TechnicalContactList_Audit3);
        }

        /// <summary>
        /// 总包专工确认信息
        /// </summary>
        /// <param name="TechnicalContactListId"></param>
        /// <returns></returns>
        public static Model.Check_TechnicalContactListApprove GetApprove3(string TechnicalContactListId)
        {
            return db.Check_TechnicalContactListApprove.OrderByDescending(x => x.ApproveDate).FirstOrDefault(x => x.TechnicalContactListId == TechnicalContactListId && (x.ApproveType == BLL.Const.TechnicalContactList_Audit2 || x.ApproveType == BLL.Const.TechnicalContactList_Audit2H));
        }

        /// <summary>
        /// 分包专工回复信息
        /// </summary>
        /// <param name="TechnicalContactListId"></param>
        /// <returns></returns>
        public static Model.Check_TechnicalContactListApprove GetApprove4(string TechnicalContactListId)
        {
            return db.Check_TechnicalContactListApprove.OrderByDescending(x => x.ApproveDate).FirstOrDefault(x => x.TechnicalContactListId == TechnicalContactListId && x.ApproveType == BLL.Const.TechnicalContactList_Audit6);
        }
        public static List<Model.Check_TechnicalContactListApprove> GetListDataByIdForApi(string TechnicalContactListId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {

                var q = from x in db.Check_TechnicalContactListApprove
                        where x.TechnicalContactListId == TechnicalContactListId && x.ApproveDate != null && x.ApproveType != "S"
                        orderby x.ApproveDate
                        select new
                        {
                            x.TechnicalContactListApproveId,
                            x.TechnicalContactListId,
                            ApproveMan = (from y in db.Sys_User where y.UserId == x.ApproveMan select y.UserName).First(),
                            x.ApproveDate,
                            x.IsAgree,
                            x.ApproveIdea,
                            x.ApproveType,
                        };
                List<Model.Check_TechnicalContactListApprove> res = new List<Model.Check_TechnicalContactListApprove>();
                var list = q.ToList();
                foreach (var item in list)
                {
                    Model.Check_TechnicalContactListApprove a = new Model.Check_TechnicalContactListApprove();
                    a.TechnicalContactListApproveId = item.TechnicalContactListApproveId;
                    a.TechnicalContactListId = item.TechnicalContactListId;
                    a.ApproveMan = item.ApproveMan;
                    a.ApproveDate = item.ApproveDate;
                    a.IsAgree = item.IsAgree;
                    a.ApproveIdea = item.ApproveIdea;
                    a.ApproveType = item.ApproveType;
                    res.Add(a);
                }
                return res;
            }
        }
        public static Model.Check_TechnicalContactListApprove getCurrApproveForApi(string id)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {

                Model.Check_TechnicalContactListApprove newApprove = db.Check_TechnicalContactListApprove.FirstOrDefault(e => e.TechnicalContactListId == id && e.ApproveType != "S" && e.ApproveDate == null);
                if (newApprove != null)
                {
                    Model.Sys_User user = BLL.UserService.GetUserByUserId(newApprove.ApproveMan);
                    if (user != null)
                    {
                        newApprove.ApproveIdea = user.UserName;
                    }
                }
                return newApprove;
            }
        }
        public static void UpdateTechnicalContactListApproveForApi(Model.Check_TechnicalContactListApprove approve)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {

                Model.Check_TechnicalContactListApprove newApprove = db.Check_TechnicalContactListApprove.FirstOrDefault(e => e.TechnicalContactListApproveId == approve.TechnicalContactListApproveId);
                if (newApprove != null)
                {
                    if (!string.IsNullOrEmpty(approve.TechnicalContactListId))
                        newApprove.TechnicalContactListId = approve.TechnicalContactListId;
                    if (!string.IsNullOrEmpty(approve.ApproveMan))
                        newApprove.ApproveMan = approve.ApproveMan;
                    if (approve.ApproveDate.HasValue)
                        newApprove.ApproveDate = approve.ApproveDate;
                    if (!string.IsNullOrEmpty(approve.ApproveIdea))
                        newApprove.ApproveIdea = approve.ApproveIdea;
                    if (approve.IsAgree.HasValue)
                        newApprove.IsAgree = approve.IsAgree;
                    if (!string.IsNullOrEmpty(approve.ApproveType))
                        newApprove.ApproveType = approve.ApproveType;

                    db.SubmitChanges();
                }
            }
        }
    }
}
