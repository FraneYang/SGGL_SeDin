using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ConstructionPlanApproveService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <returns></returns>
        public static IEnumerable getListData(string ConstructionPlanId)
        {
            return from x in db.ZHGL_ConstructionPlanApprove
                   where x.ConstructionPlanId == ConstructionPlanId && x.ApproveDate != null 
                   orderby x.ApproveDate
                   select new
                   {
                       x.ConstructionPlanApproveId,
                       x.ConstructionPlanId,
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
        /// 根据总承包商施工计划主键删除对应的所有总承包商施工计划审批信息
        /// </summary>
        /// <param name="noticeId">总承包商施工计划主键</param>
        public static void DeleteConstructionPlanApproveByConstructionPlanId(string noticeId)
        {
            Model.SGGLDB db = Funs.DB;
            var data = (from x in db.ZHGL_ConstructionPlanApprove where x.ConstructionPlanId == noticeId select x).ToList();
            if (data != null)
            {
                db.ZHGL_ConstructionPlanApprove.DeleteAllOnSubmit(data);
            }

            db.SubmitChanges();
        }
        /// <summary>
        /// 增加总承包商施工计划审批信息
        /// </summary>
        /// <param name="ConstructionPlanApprove">总承包商施工计划审批实体</param>
        public static void AddConstructionPlanApprove(Model.ZHGL_ConstructionPlanApprove ConstructionPlanApprove)
        {
            Model.SGGLDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.ZHGL_ConstructionPlanApprove));
            Model.ZHGL_ConstructionPlanApprove newConstructionPlanApprove = new Model.ZHGL_ConstructionPlanApprove();
            newConstructionPlanApprove.ConstructionPlanApproveId = newKeyID;
            newConstructionPlanApprove.ConstructionPlanId = ConstructionPlanApprove.ConstructionPlanId;
            newConstructionPlanApprove.ApproveMan = ConstructionPlanApprove.ApproveMan;
            newConstructionPlanApprove.ApproveDate = ConstructionPlanApprove.ApproveDate;
            newConstructionPlanApprove.ApproveIdea = ConstructionPlanApprove.ApproveIdea;
            newConstructionPlanApprove.IsAgree = ConstructionPlanApprove.IsAgree;
            newConstructionPlanApprove.ApproveType = ConstructionPlanApprove.ApproveType;

            db.ZHGL_ConstructionPlanApprove.InsertOnSubmit(newConstructionPlanApprove);
            db.SubmitChanges();

        }
        public static string AddConstructionPlanApproveForApi(Model.ZHGL_ConstructionPlanApprove ConstructionPlanApprove)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                string newKeyID = SQLHelper.GetNewID(typeof(Model.ZHGL_ConstructionPlanApprove));
                Model.ZHGL_ConstructionPlanApprove newConstructionPlanApprove = new Model.ZHGL_ConstructionPlanApprove();
                newConstructionPlanApprove.ConstructionPlanApproveId = newKeyID;
                newConstructionPlanApprove.ConstructionPlanId = ConstructionPlanApprove.ConstructionPlanId;
                newConstructionPlanApprove.ApproveMan = ConstructionPlanApprove.ApproveMan;
                newConstructionPlanApprove.ApproveDate = ConstructionPlanApprove.ApproveDate;
                newConstructionPlanApprove.ApproveIdea = ConstructionPlanApprove.ApproveIdea;
                newConstructionPlanApprove.IsAgree = ConstructionPlanApprove.IsAgree;
                newConstructionPlanApprove.ApproveType = ConstructionPlanApprove.ApproveType;

                db.ZHGL_ConstructionPlanApprove.InsertOnSubmit(newConstructionPlanApprove);
                db.SubmitChanges();
                return newKeyID;
            }
        }

        /// <summary>
        /// 根据总承包商施工计划Id获取一个总承包商施工计划审批信息
        /// </summary>
        /// <param name="noticeId">总承包商施工计划Id</param>
        /// <returns>一个总承包商施工计划审批实体</returns>
        public static Model.ZHGL_ConstructionPlanApprove GetConstructionPlanApproveByConstructionPlanId(string constructionPlanId)
        {
            return db.ZHGL_ConstructionPlanApprove.FirstOrDefault(x => x.ConstructionPlanId == constructionPlanId && x.ApproveDate == null);
        }
        /// <summary>
        /// 修改总承包商施工计划审批信息
        /// </summary>
        /// <param name="ConstructionPlanApprove">总承包商施工计划审批实体</param>
        public static void UpdateConstructionPlanApprove(Model.ZHGL_ConstructionPlanApprove ConstructionPlanApprove)
        {
            Model.SGGLDB db = Funs.DB;
            Model.ZHGL_ConstructionPlanApprove newConstructionPlanApprove = db.ZHGL_ConstructionPlanApprove.First(e => e.ConstructionPlanApproveId == ConstructionPlanApprove.ConstructionPlanApproveId);
            newConstructionPlanApprove.ConstructionPlanId = ConstructionPlanApprove.ConstructionPlanId;
            newConstructionPlanApprove.ApproveMan = ConstructionPlanApprove.ApproveMan;
            newConstructionPlanApprove.ApproveDate = ConstructionPlanApprove.ApproveDate;
            newConstructionPlanApprove.ApproveIdea = ConstructionPlanApprove.ApproveIdea;
            newConstructionPlanApprove.IsAgree = ConstructionPlanApprove.IsAgree;
            newConstructionPlanApprove.ApproveType = ConstructionPlanApprove.ApproveType;

            db.SubmitChanges();
        }

        public static Model.ZHGL_ConstructionPlanApprove GetComplie(string ConstructionPlanId)
        {
            return db.ZHGL_ConstructionPlanApprove.FirstOrDefault(x => x.ConstructionPlanId == ConstructionPlanId && x.ApproveType == BLL.Const.ConstructionPlan_Compile);
        }
        public static Model.ZHGL_ConstructionPlanApprove GetConstructionPlanApproveById(string ConstructionPlanApproveId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                return db.ZHGL_ConstructionPlanApprove.FirstOrDefault(x => x.ConstructionPlanApproveId == ConstructionPlanApproveId);
            }
        }
        public static List<Model.ZHGL_ConstructionPlanApprove> getListDataByIdForApi(string ConstructionPlanId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                var q = from x in db.ZHGL_ConstructionPlanApprove
                        where x.ConstructionPlanId == ConstructionPlanId && x.ApproveDate != null && x.ApproveType != "S"
                        orderby x.ApproveDate
                        select new
                        {
                            x.ConstructionPlanApproveId,
                            x.ConstructionPlanId,
                            ApproveMan = (from y in db.Sys_User where y.UserId == x.ApproveMan select y.UserName).First(),
                            x.ApproveDate,
                            x.ApproveIdea,
                            x.IsAgree,
                            x.ApproveType,
                            UserName = (from y in db.Sys_User where y.UserId == x.ApproveMan select y.UserName).First(),

                        };
                List<Model.ZHGL_ConstructionPlanApprove> res = new List<Model.ZHGL_ConstructionPlanApprove>();
                var list = q.ToList();
                foreach (var item in list)
                {
                    Model.ZHGL_ConstructionPlanApprove wc = new Model.ZHGL_ConstructionPlanApprove();
                    wc.ConstructionPlanApproveId = item.ConstructionPlanApproveId;
                    wc.ConstructionPlanId = item.ConstructionPlanId;
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
        public static Model.ZHGL_ConstructionPlanApprove getCurrApproveForApi(string id)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.ZHGL_ConstructionPlanApprove newConstructionPlanApprove = db.ZHGL_ConstructionPlanApprove.FirstOrDefault(e => e.ConstructionPlanId == id && e.ApproveDate == null);
                if (newConstructionPlanApprove != null)
                {
                    Model.Sys_User user = BLL.UserService.GetUserByUserId(newConstructionPlanApprove.ApproveMan);
                    if (user != null)
                    {
                        newConstructionPlanApprove.ApproveIdea = user.UserName;
                    }
                }
                return newConstructionPlanApprove;
            }
        } /// <summary>
          /// 修改总承包商施工计划审批信息
          /// </summary>
          /// <param name="ConstructionPlanApprove">总承包商施工计划审批实体</param>
        public static void UpdateConstructionPlanApproveForApi(Model.ZHGL_ConstructionPlanApprove ConstructionPlanApprove)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {

                Model.ZHGL_ConstructionPlanApprove newConstructionPlanApprove = db.ZHGL_ConstructionPlanApprove.FirstOrDefault(e => e.ConstructionPlanApproveId == ConstructionPlanApprove.ConstructionPlanApproveId);
                if (newConstructionPlanApprove != null)
                {
                    if (!string.IsNullOrEmpty(ConstructionPlanApprove.ConstructionPlanId))
                        newConstructionPlanApprove.ConstructionPlanId = ConstructionPlanApprove.ConstructionPlanId;
                    if (!string.IsNullOrEmpty(ConstructionPlanApprove.ApproveMan))
                        newConstructionPlanApprove.ApproveMan = ConstructionPlanApprove.ApproveMan;
                    if (ConstructionPlanApprove.ApproveDate.HasValue)
                        newConstructionPlanApprove.ApproveDate = ConstructionPlanApprove.ApproveDate;
                    if (!string.IsNullOrEmpty(ConstructionPlanApprove.ApproveIdea))
                        newConstructionPlanApprove.ApproveIdea = ConstructionPlanApprove.ApproveIdea;
                    if (ConstructionPlanApprove.IsAgree.HasValue)
                        newConstructionPlanApprove.IsAgree = ConstructionPlanApprove.IsAgree;
                    if (!string.IsNullOrEmpty(ConstructionPlanApprove.ApproveType))
                        newConstructionPlanApprove.ApproveType = ConstructionPlanApprove.ApproveType;

                    db.SubmitChanges();
                }
            }
        }
    }
}
