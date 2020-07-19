using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public class SpotCheckApproveService
    {
        public static Model.SGGLDB db = Funs.DB;

        public static Model.Check_SpotCheckApprove GetSpotApproveBySpotCheckDetailId(string spotCheckDetailId)
        {
            return (from x in Funs.DB.Check_SpotCheckApprove where x.SpotCheckDetailId == spotCheckDetailId && x.ApproveType != "S" && x.Sign == "2" && x.ApproveDate == null select x).FirstOrDefault();
        }

        public static List<Model.Check_SpotCheckApprove> GetSpotApprovesBySpotCheckCode(string spotCheckCode)
        {
            return (from x in Funs.DB.Check_SpotCheckApprove where x.SpotCheckCode == spotCheckCode && x.ApproveType != "S" && x.Sign == "2" && x.ApproveDate == null select x).ToList();
        }

        public static List<Model.Check_SpotCheckApprove> GetSpotCheck2ApproveBySpotCheckCode(string SpotCheckCode)
        {
            return (from x in Funs.DB.Check_SpotCheckApprove
                    where x.SpotCheckCode == SpotCheckCode && x.ApproveDate == null && x.Sign == "2" && x.ApproveType != "S"
                    select x).ToList();
        }

        /// <summary>
        /// 获取实体验收模板列表
        /// </summary>
        /// <param name="satartRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListData(string SpotCheckCode)
        {
            return from x in db.Check_SpotCheckApprove
                   where x.SpotCheckCode == SpotCheckCode && x.ApproveDate != null && x.ApproveType != "S" && x.Sign == "1"
                   orderby x.ApproveDate
                   select new
                   {
                       x.SpotCheckApproveId,
                       x.SpotCheckCode,
                       ApproveMan = (from y in db.Sys_User where y.UserId == x.ApproveMan select y.UserName).First(),
                       x.ApproveDate,
                       x.IsAgree,
                       x.ApproveIdea,
                       x.ApproveType,
                   };
        }

        /// <summary>
        /// 获取实体验收模板列表
        /// </summary>
        /// <param name="satartRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getList(string SpotCheckCode)
        {
            return from x in db.Check_SpotCheckApprove
                   where x.SpotCheckCode == SpotCheckCode && x.ApproveDate != null && x.ApproveType != "S" && x.Sign == "2"
                   orderby x.ApproveDate
                   select new
                   {
                       x.SpotCheckApproveId,
                       x.SpotCheckCode,
                       ApproveMan = (from y in db.Sys_User where y.UserId == x.ApproveMan select y.UserName).First(),
                       x.ApproveDate,
                       x.IsAgree,
                       x.ApproveIdea,
                       x.ApproveType,
                   };
        }

        /// <summary>
        /// 根据实体验收编号获取一个实体验收审批信息
        /// </summary>
        /// <param name="SpotCheckCode">实体验收编号</param>
        /// <returns>一个实体验收审批实体</returns>
        public static Model.Check_SpotCheckApprove GetSpotCheckApproveBySpotCheckCode(string SpotCheckCode)
        {
            return db.Check_SpotCheckApprove.FirstOrDefault(x => x.SpotCheckCode == SpotCheckCode && x.ApproveDate == null && x.Sign == "1" && x.ApproveType != "S");
        }

        /// <summary>
        /// 根据实体验收审批编号获取一个实体验收审批信息
        /// </summary>
        /// <param name="SpotCheckCode">实体验收编号</param>
        /// <returns>一个实体验收审批实体</returns>
        public static Model.Check_SpotCheckApprove GetSpotCheckApproveByApproveId(string approveId)
        {
            return db.Check_SpotCheckApprove.FirstOrDefault(x => x.SpotCheckApproveId == approveId);
        }

        public static Model.Check_SpotCheckApprove GetComplie(string SpotCheckCode)
        {
            return db.Check_SpotCheckApprove.FirstOrDefault(x => x.SpotCheckCode == SpotCheckCode && x.ApproveType == BLL.Const.SpotCheck_Compile);
        }
        public static Model.Check_SpotCheckApprove GetComplieForApi(string SpotCheckCode)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                return db.Check_SpotCheckApprove.FirstOrDefault(x => x.SpotCheckCode == SpotCheckCode && x.ApproveType == BLL.Const.SpotCheck_Compile);
            }
        }

        public static Model.Check_SpotCheckApprove GetSee(string SpotCheckCode, string userId)
        {
            return db.Check_SpotCheckApprove.FirstOrDefault(x => x.SpotCheckCode == SpotCheckCode && x.ApproveType == "S" && x.ApproveMan == userId && x.ApproveDate == null);
        }
        public static void See(string SpotCheckCode, string userId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                var res = db.Check_SpotCheckApprove.FirstOrDefault(x => x.SpotCheckCode == SpotCheckCode && x.ApproveType == "S" && x.ApproveMan == userId && x.ApproveDate == null);
                if (res != null)
                {
                    res.ApproveDate = DateTime.Now;
                    db.SubmitChanges();
                }
            }
        }
        public static Model.Check_SpotCheckApprove GetAudit2(string SpotCheckCode)
        {
            return db.Check_SpotCheckApprove.FirstOrDefault(x => x.SpotCheckCode == SpotCheckCode && x.ApproveType == BLL.Const.SpotCheck_Audit2);
        }

        public static Model.Check_SpotCheckApprove GetReCompile(string SpotCheckCode)
        {
            return db.Check_SpotCheckApprove.FirstOrDefault(x => x.SpotCheckCode == SpotCheckCode && x.ApproveType == BLL.Const.SpotCheck_ReCompile);
        }

        /// <summary>
        /// 根据实体验收发布Id获取所以对应实体验收审批信息
        /// </summary>
        /// <param name="SpotCheckCode">实体验收发布Id</param>
        /// <returns>实体验收审批集合</returns>
        public static List<Model.Check_SpotCheckApprove> GetSpotCheckApprovesBySpotCheckCode(string SpotCheckCode)
        {
            return (from x in db.Check_SpotCheckApprove where x.SpotCheckCode == SpotCheckCode select x).ToList();
        }

        /// <summary>
        /// 增加实体验收审批信息
        /// </summary>
        /// <param name="managerRuleApprove">实体验收审批实体</param>
        public static void AddSpotCheckApprove(Model.Check_SpotCheckApprove approve)
        {
            Model.SGGLDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Check_SpotCheckApprove));
            Model.Check_SpotCheckApprove newApprove = new Model.Check_SpotCheckApprove();
            newApprove.SpotCheckApproveId = newKeyID;
            newApprove.SpotCheckCode = approve.SpotCheckCode;
            newApprove.ApproveMan = approve.ApproveMan;
            newApprove.ApproveDate = approve.ApproveDate;
            newApprove.ApproveIdea = approve.ApproveIdea;
            newApprove.IsAgree = approve.IsAgree;
            newApprove.ApproveType = approve.ApproveType;
            newApprove.SpotCheckDetailId = approve.SpotCheckDetailId;
            newApprove.Sign = approve.Sign;

            db.Check_SpotCheckApprove.InsertOnSubmit(newApprove);
            db.SubmitChanges();
        }
        public static string AddSpotCheckApproveForApi(Model.Check_SpotCheckApprove approve)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                string newKeyID = SQLHelper.GetNewID(typeof(Model.Check_SpotCheckApprove));
                Model.Check_SpotCheckApprove newApprove = new Model.Check_SpotCheckApprove();
                newApprove.SpotCheckApproveId = newKeyID;
                newApprove.SpotCheckCode = approve.SpotCheckCode;
                newApprove.ApproveMan = approve.ApproveMan;
                newApprove.ApproveDate = approve.ApproveDate;
                newApprove.ApproveIdea = approve.ApproveIdea;
                newApprove.IsAgree = approve.IsAgree;
                newApprove.ApproveType = approve.ApproveType;
                newApprove.Sign = approve.Sign;
                newApprove.SpotCheckDetailId = approve.SpotCheckDetailId;

                db.Check_SpotCheckApprove.InsertOnSubmit(newApprove);
                db.SubmitChanges();
                return newKeyID;
            }
        }
        /// <summary>
        /// 修改实体验收审批信息
        /// </summary>
        /// <param name="managerRuleApprove">实体验收审批实体</param>
        public static void UpdateSpotCheckApprove(Model.Check_SpotCheckApprove approve)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_SpotCheckApprove newApprove = db.Check_SpotCheckApprove.First(e => e.SpotCheckApproveId == approve.SpotCheckApproveId && e.ApproveDate == null);
            newApprove.SpotCheckCode = approve.SpotCheckCode;
            newApprove.ApproveMan = approve.ApproveMan;
            newApprove.ApproveDate = approve.ApproveDate;
            newApprove.ApproveIdea = approve.ApproveIdea;
            newApprove.IsAgree = approve.IsAgree;
            newApprove.ApproveType = approve.ApproveType;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据实体验收编号删除对应的所有实体验收审批信息
        /// </summary>
        /// <param name="SpotCheckCode">实体验收编号</param>
        public static void DeleteSpotCheckApprovesBySpotCheckCode(string SpotCheckCode)
        {
            Model.SGGLDB db = Funs.DB;
            var q = (from x in db.Check_SpotCheckApprove where x.SpotCheckCode == SpotCheckCode select x).ToList();
            db.Check_SpotCheckApprove.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据用户主键获得实体验收审批的数量
        /// </summary>
        /// <param name="userId">角色</param>
        /// <returns></returns>
        public static int GetManagerRuleApproveCountByUserId(string userId)
        {
            var q = (from x in Funs.DB.Check_SpotCheckApprove where x.ApproveMan == userId select x).ToList();
            return q.Count();
        }
        public static List<Model.Check_SpotCheckApprove> getListDataBySpotCheckCodeForApi(string SpotCheckCode)
        {
            var q = from x in db.Check_SpotCheckApprove
                    where x.SpotCheckCode == SpotCheckCode && x.ApproveDate != null && x.ApproveType != "S" && x.Sign == "1"
                    orderby x.ApproveDate
                    select new
                    {
                        x.SpotCheckApproveId,
                        x.SpotCheckCode,
                        ApproveMan = (from y in db.Sys_User where y.UserId == x.ApproveMan select y.UserName).First(),
                        x.ApproveDate,
                        x.IsAgree,
                        x.ApproveIdea,
                        x.ApproveType,
                    };
            List<Model.Check_SpotCheckApprove> res = new List<Model.Check_SpotCheckApprove>();
            var list = q.ToList();
            foreach (var item in list)
            {
                Model.Check_SpotCheckApprove jc = new Model.Check_SpotCheckApprove();
                jc.SpotCheckApproveId = item.SpotCheckApproveId;
                jc.SpotCheckCode = item.SpotCheckCode;
                jc.ApproveMan = item.ApproveMan;
                jc.ApproveDate = item.ApproveDate;
                jc.IsAgree = item.IsAgree;
                jc.ApproveIdea = item.ApproveIdea;
                jc.ApproveType = item.ApproveType;
                res.Add(jc);
            }
            return res;
        }
        public static Model.Check_SpotCheckApprove getCurrApproveForApi(string SpotCheckCode)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_SpotCheckApprove newApprove = db.Check_SpotCheckApprove.FirstOrDefault(e => e.SpotCheckCode == SpotCheckCode && e.ApproveType != "S" && e.ApproveDate == null);
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
        public static void UpdateSpotCheckApproveForApi(Model.Check_SpotCheckApprove approve)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Check_SpotCheckApprove newApprove = db.Check_SpotCheckApprove.First(e => e.SpotCheckApproveId == approve.SpotCheckApproveId);
                if (!string.IsNullOrEmpty(approve.SpotCheckCode))
                    newApprove.SpotCheckCode = approve.SpotCheckCode;
                if (!string.IsNullOrEmpty(approve.ApproveMan))
                    newApprove.ApproveMan = approve.ApproveMan;
                newApprove.ApproveDate = approve.ApproveDate;
                if (!string.IsNullOrEmpty(approve.ApproveIdea))
                    newApprove.ApproveIdea = approve.ApproveIdea;
                if (approve.IsAgree.HasValue)
                    newApprove.IsAgree = approve.IsAgree;
                if (!string.IsNullOrEmpty(approve.ApproveType))
                    newApprove.ApproveType = approve.ApproveType;
                if (!string.IsNullOrEmpty(approve.Sign))
                    newApprove.Sign = approve.Sign;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据实体验收编号删除对应的所有实体验收审批信息
        /// </summary>
        /// <param name="SpotCheckCode">实体验收编号</param>
        public static void DeleteSpotCheckApprovesBySpotCheckDetailId(string spotCheckDetailId)
        {
            Model.SGGLDB db = Funs.DB;
            var q = (from x in db.Check_SpotCheckApprove where x.SpotCheckDetailId == spotCheckDetailId select x).ToList();
            db.Check_SpotCheckApprove.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }
    }
}
