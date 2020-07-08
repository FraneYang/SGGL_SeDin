using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DesignApproveService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 获取设计变更模板列表
        /// </summary>
        /// <param name="satartRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListData(string DesignId)
        {
            return from x in db.Check_DesignApprove
                   where x.DesignId == DesignId && x.ApproveDate != null
                   orderby x.ApproveDate
                   select new
                   {
                       x.DesignApproveId,
                       x.DesignId,
                       ApproveMan = (from y in db.Sys_User where y.UserId == x.ApproveMan select y.UserName).First(),
                       x.ApproveDate,
                       x.IsAgree,
                       x.ApproveIdea,
                       x.ApproveType,
                   };
        }

        /// <summary>
        /// 根据设计变更编号获取一个设计变更审批信息
        /// </summary>
        /// <param name="DesignCode">设计变更编号</param>
        /// <returns>一个设计变更审批实体</returns>
        public static Model.Check_DesignApprove GetDesignApproveByDesignId(string DesignId)
        {
            return db.Check_DesignApprove.FirstOrDefault(x => x.DesignId == DesignId && x.ApproveDate == null);
        }

        /// <summary>
        /// 根据设计变更审批编号获取一个设计变更审批信息
        /// </summary>
        /// <param name="DesignCode">设计变更编号</param>
        /// <returns>一个设计变更审批实体</returns>
        public static Model.Check_DesignApprove GetDesignApproveByApproveId(string approveId)
        {
            return db.Check_DesignApprove.FirstOrDefault(x => x.DesignApproveId == approveId);
        }

        public static Model.Check_DesignApprove GetComplie(string DesignId)
        {
            return db.Check_DesignApprove.FirstOrDefault(x => x.DesignId == DesignId && x.ApproveType == BLL.Const.Design_Compile);
        }

        public static Model.Check_DesignApprove GetAuditMan(string DesignId, string state)
        {
            return db.Check_DesignApprove.OrderByDescending(x => x.ApproveDate).FirstOrDefault(x => x.DesignId == DesignId && x.ApproveType == state);
        }

        public static Model.Check_DesignApprove GetAudit2(string DesignId)
        {
            return db.Check_DesignApprove.FirstOrDefault(x => x.DesignId == DesignId && x.ApproveType == BLL.Const.Design_Audit2);
        }

        /// <summary>
        /// 根据设计变更发布Id获取所以对应设计变更审批信息
        /// </summary>
        /// <param name="DesignCode">设计变更发布Id</param>
        /// <returns>设计变更审批集合</returns>
        public static List<Model.Check_DesignApprove> GetDesignApprovesByDesignId(string DesignId)
        {
            return (from x in db.Check_DesignApprove where x.DesignId == DesignId select x).ToList();
        }

        /// <summary>
        /// 增加设计变更审批信息
        /// </summary>
        /// <param name="managerRuleApprove">设计变更审批实体</param>
        public static void AddDesignApprove(Model.Check_DesignApprove approve)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Check_DesignApprove));
            Model.Check_DesignApprove newApprove = new Model.Check_DesignApprove();
            newApprove.DesignApproveId = newKeyID;
            newApprove.DesignId = approve.DesignId;
            newApprove.ApproveMan = approve.ApproveMan;
            newApprove.ApproveDate = approve.ApproveDate;
            newApprove.ApproveIdea = approve.ApproveIdea;
            newApprove.IsAgree = approve.IsAgree;
            newApprove.ApproveType = approve.ApproveType;

            db.Check_DesignApprove.InsertOnSubmit(newApprove);
            db.SubmitChanges();
        }
        public static string AddDesignApproveForApi(Model.Check_DesignApprove approve)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                string newKeyID = SQLHelper.GetNewID(typeof(Model.Check_DesignApprove));
                Model.Check_DesignApprove newApprove = new Model.Check_DesignApprove();
                newApprove.DesignApproveId = newKeyID;
                newApprove.DesignId = approve.DesignId;
                newApprove.ApproveMan = approve.ApproveMan;
                newApprove.ApproveDate = approve.ApproveDate;
                newApprove.ApproveIdea = approve.ApproveIdea;
                newApprove.IsAgree = approve.IsAgree;
                newApprove.ApproveType = approve.ApproveType;

                db.Check_DesignApprove.InsertOnSubmit(newApprove);
                db.SubmitChanges();
                return newKeyID;
            }
        }
        /// <summary>
        /// 修改设计变更审批信息
        /// </summary>
        /// <param name="managerRuleApprove">设计变更审批实体</param>
        public static void UpdateDesignApprove(Model.Check_DesignApprove approve)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Check_DesignApprove newApprove = db.Check_DesignApprove.First(e => e.DesignApproveId == approve.DesignApproveId && e.ApproveDate == null);
            newApprove.DesignId = approve.DesignId;
            newApprove.ApproveMan = approve.ApproveMan;
            newApprove.ApproveDate = approve.ApproveDate;
            newApprove.ApproveIdea = approve.ApproveIdea;
            newApprove.IsAgree = approve.IsAgree;
            newApprove.ApproveType = approve.ApproveType;

            db.SubmitChanges();
        }
        /// <summary>
        /// 修改设计变更审批信息
        /// </summary>
        /// <param name="managerRuleApprove">设计变更审批实体</param>
        public static void UpdateDesignApproveForApi(Model.Check_DesignApprove approve)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Check_DesignApprove newApprove = db.Check_DesignApprove.FirstOrDefault(e => e.DesignApproveId == approve.DesignApproveId);
                if (newApprove != null)
                {
                    if (!string.IsNullOrEmpty(approve.DesignId))
                        newApprove.DesignId = approve.DesignId;
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
        /// <summary>
        /// 根据设计变更编号删除对应的所有设计变更审批信息
        /// </summary>
        /// <param name="DesignCode">设计变更编号</param>
        public static void DeleteDesignApprovesByDesignId(string DesignId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var q = (from x in db.Check_DesignApprove where x.DesignId == DesignId select x).ToList();
            db.Check_DesignApprove.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据用户主键获得设计变更审批的数量
        /// </summary>
        /// <param name="userId">角色</param>
        /// <returns></returns>
        public static int GetManagerRuleApproveCountByUserId(string userId)
        {
            var q = (from x in new Model.SGGLDB(Funs.ConnString).Check_DesignApprove where x.ApproveMan == userId select x).ToList();
            return q.Count();
        }
        public static List<Model.Check_DesignApprove> getListDataByIdForApi(string DesignId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                var q = from x in db.Check_DesignApprove
                        where x.DesignId == DesignId && x.ApproveDate != null && x.ApproveType != "S"
                        orderby x.ApproveDate
                        select new
                        {
                            x.DesignApproveId,
                            x.DesignId,
                            ApproveMan = (from y in db.Sys_User where y.UserId == x.ApproveMan select y.UserName).First(),
                            x.ApproveDate,
                            x.IsAgree,
                            x.ApproveIdea,
                            x.ApproveType,
                        };
                List<Model.Check_DesignApprove> res = new List<Model.Check_DesignApprove>();
                var list = q.ToList();
                foreach (var item in list)
                {
                    Model.Check_DesignApprove cd = new Model.Check_DesignApprove();
                    cd.DesignApproveId = item.DesignApproveId;
                    cd.DesignId = item.DesignId;
                    cd.ApproveMan = item.ApproveMan;
                    cd.ApproveDate = item.ApproveDate;
                    cd.IsAgree = item.IsAgree;
                    cd.ApproveIdea = item.ApproveIdea;
                    cd.ApproveType = item.ApproveType;
                    res.Add(cd);
                }
                return res;
            }
        }
        public static Model.Check_DesignApprove getCurrApproveForApi(string id)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Check_DesignApprove newApprove = db.Check_DesignApprove.FirstOrDefault(e => e.DesignId == id && e.ApproveType != "S" && e.ApproveDate == null);
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
    }
}
