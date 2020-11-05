using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ConstructionReportApproveService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <returns></returns>
        public static IEnumerable getListData(string ConstructionReportId)
        {
            return from x in db.ZHGL_ConstructionReportApprove
                   where x.ConstructionReportId == ConstructionReportId && x.ApproveDate != null
                   orderby x.ApproveDate
                   select new
                   {
                       x.ConstructionReportApproveId,
                       x.ConstructionReportId,
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
        /// 根据总承包商施工报告主键删除对应的所有总承包商施工报告审批信息
        /// </summary>
        /// <param name="noticeId">总承包商施工报告主键</param>
        public static void DeleteConstructionReportApproveByConstructionReportId(string noticeId)
        {
            Model.SGGLDB db = Funs.DB;
            var data = (from x in db.ZHGL_ConstructionReportApprove where x.ConstructionReportId == noticeId select x).ToList();
            if (data != null)
            {
                db.ZHGL_ConstructionReportApprove.DeleteAllOnSubmit(data);
            }

            db.SubmitChanges();
        }
        /// <summary>
        /// 增加总承包商施工报告审批信息
        /// </summary>
        /// <param name="ConstructionReportApprove">总承包商施工报告审批实体</param>
        public static void AddConstructionReportApprove(Model.ZHGL_ConstructionReportApprove ConstructionReportApprove)
        {
            Model.SGGLDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.ZHGL_ConstructionReportApprove));
            Model.ZHGL_ConstructionReportApprove newConstructionReportApprove = new Model.ZHGL_ConstructionReportApprove();
            newConstructionReportApprove.ConstructionReportApproveId = newKeyID;
            newConstructionReportApprove.ConstructionReportId = ConstructionReportApprove.ConstructionReportId;
            newConstructionReportApprove.ApproveMan = ConstructionReportApprove.ApproveMan;
            newConstructionReportApprove.ApproveDate = ConstructionReportApprove.ApproveDate;
            newConstructionReportApprove.ApproveIdea = ConstructionReportApprove.ApproveIdea;
            newConstructionReportApprove.IsAgree = ConstructionReportApprove.IsAgree;
            newConstructionReportApprove.ApproveType = ConstructionReportApprove.ApproveType;

            db.ZHGL_ConstructionReportApprove.InsertOnSubmit(newConstructionReportApprove);
            db.SubmitChanges();

        }
        public static string AddConstructionReportApproveForApi(Model.ZHGL_ConstructionReportApprove ConstructionReportApprove)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                string newKeyID = SQLHelper.GetNewID(typeof(Model.ZHGL_ConstructionReportApprove));
                Model.ZHGL_ConstructionReportApprove newConstructionReportApprove = new Model.ZHGL_ConstructionReportApprove();
                newConstructionReportApprove.ConstructionReportApproveId = newKeyID;
                newConstructionReportApprove.ConstructionReportId = ConstructionReportApprove.ConstructionReportId;
                newConstructionReportApprove.ApproveMan = ConstructionReportApprove.ApproveMan;
                newConstructionReportApprove.ApproveDate = ConstructionReportApprove.ApproveDate;
                newConstructionReportApprove.ApproveIdea = ConstructionReportApprove.ApproveIdea;
                newConstructionReportApprove.IsAgree = ConstructionReportApprove.IsAgree;
                newConstructionReportApprove.ApproveType = ConstructionReportApprove.ApproveType;

                db.ZHGL_ConstructionReportApprove.InsertOnSubmit(newConstructionReportApprove);
                db.SubmitChanges();
                return newKeyID;
            }
        }

        /// <summary>
        /// 根据总承包商施工报告Id获取一个总承包商施工报告审批信息
        /// </summary>
        /// <param name="noticeId">总承包商施工报告Id</param>
        /// <returns>一个总承包商施工报告审批实体</returns>
        public static Model.ZHGL_ConstructionReportApprove GetConstructionReportApproveByConstructionReportId(string constructionReportId)
        {
            return db.ZHGL_ConstructionReportApprove.FirstOrDefault(x => x.ConstructionReportId == constructionReportId && x.ApproveDate == null);
        }
        /// <summary>
        /// 修改总承包商施工报告审批信息
        /// </summary>
        /// <param name="ConstructionReportApprove">总承包商施工报告审批实体</param>
        public static void UpdateConstructionReportApprove(Model.ZHGL_ConstructionReportApprove ConstructionReportApprove)
        {
            Model.SGGLDB db = Funs.DB;
            Model.ZHGL_ConstructionReportApprove newConstructionReportApprove = db.ZHGL_ConstructionReportApprove.First(e => e.ConstructionReportApproveId == ConstructionReportApprove.ConstructionReportApproveId);
            newConstructionReportApprove.ConstructionReportId = ConstructionReportApprove.ConstructionReportId;
            newConstructionReportApprove.ApproveMan = ConstructionReportApprove.ApproveMan;
            newConstructionReportApprove.ApproveDate = ConstructionReportApprove.ApproveDate;
            newConstructionReportApprove.ApproveIdea = ConstructionReportApprove.ApproveIdea;
            newConstructionReportApprove.IsAgree = ConstructionReportApprove.IsAgree;
            newConstructionReportApprove.ApproveType = ConstructionReportApprove.ApproveType;

            db.SubmitChanges();
        }

        public static Model.ZHGL_ConstructionReportApprove GetComplie(string ConstructionReportId)
        {
            return db.ZHGL_ConstructionReportApprove.FirstOrDefault(x => x.ConstructionReportId == ConstructionReportId && x.ApproveType == BLL.Const.ConstructionReport_Compile);
        }
        public static Model.ZHGL_ConstructionReportApprove GetConstructionReportApproveById(string ConstructionReportApproveId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                return db.ZHGL_ConstructionReportApprove.FirstOrDefault(x => x.ConstructionReportApproveId == ConstructionReportApproveId);
            }
        }
        public static List<Model.ZHGL_ConstructionReportApprove> getListDataByIdForApi(string ConstructionReportId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                var q = from x in db.ZHGL_ConstructionReportApprove
                        where x.ConstructionReportId == ConstructionReportId && x.ApproveDate != null && x.ApproveType != "S"
                        orderby x.ApproveDate
                        select new
                        {
                            x.ConstructionReportApproveId,
                            x.ConstructionReportId,
                            ApproveMan = (from y in db.Sys_User where y.UserId == x.ApproveMan select y.UserName).First(),
                            x.ApproveDate,
                            x.ApproveIdea,
                            x.IsAgree,
                            x.ApproveType,
                            UserName = (from y in db.Sys_User where y.UserId == x.ApproveMan select y.UserName).First(),

                        };
                List<Model.ZHGL_ConstructionReportApprove> res = new List<Model.ZHGL_ConstructionReportApprove>();
                var list = q.ToList();
                foreach (var item in list)
                {
                    Model.ZHGL_ConstructionReportApprove wc = new Model.ZHGL_ConstructionReportApprove();
                    wc.ConstructionReportApproveId = item.ConstructionReportApproveId;
                    wc.ConstructionReportId = item.ConstructionReportId;
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
        public static Model.ZHGL_ConstructionReportApprove getCurrApproveForApi(string id)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.ZHGL_ConstructionReportApprove newConstructionReportApprove = db.ZHGL_ConstructionReportApprove.FirstOrDefault(e => e.ConstructionReportId == id && e.ApproveDate == null);
                if (newConstructionReportApprove != null)
                {
                    Model.Sys_User user = BLL.UserService.GetUserByUserId(newConstructionReportApprove.ApproveMan);
                    if (user != null)
                    {
                        newConstructionReportApprove.ApproveIdea = user.UserName;
                    }
                }
                return newConstructionReportApprove;
            }
        } /// <summary>
          /// 修改总承包商施工报告审批信息
          /// </summary>
          /// <param name="ConstructionReportApprove">总承包商施工报告审批实体</param>
        public static void UpdateConstructionReportApproveForApi(Model.ZHGL_ConstructionReportApprove ConstructionReportApprove)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {

                Model.ZHGL_ConstructionReportApprove newConstructionReportApprove = db.ZHGL_ConstructionReportApprove.FirstOrDefault(e => e.ConstructionReportApproveId == ConstructionReportApprove.ConstructionReportApproveId);
                if (newConstructionReportApprove != null)
                {
                    if (!string.IsNullOrEmpty(ConstructionReportApprove.ConstructionReportId))
                        newConstructionReportApprove.ConstructionReportId = ConstructionReportApprove.ConstructionReportId;
                    if (!string.IsNullOrEmpty(ConstructionReportApprove.ApproveMan))
                        newConstructionReportApprove.ApproveMan = ConstructionReportApprove.ApproveMan;
                    if (ConstructionReportApprove.ApproveDate.HasValue)
                        newConstructionReportApprove.ApproveDate = ConstructionReportApprove.ApproveDate;
                    if (!string.IsNullOrEmpty(ConstructionReportApprove.ApproveIdea))
                        newConstructionReportApprove.ApproveIdea = ConstructionReportApprove.ApproveIdea;
                    if (ConstructionReportApprove.IsAgree.HasValue)
                        newConstructionReportApprove.IsAgree = ConstructionReportApprove.IsAgree;
                    if (!string.IsNullOrEmpty(ConstructionReportApprove.ApproveType))
                        newConstructionReportApprove.ApproveType = ConstructionReportApprove.ApproveType;

                    db.SubmitChanges();
                }
            }
        }
    }
}
