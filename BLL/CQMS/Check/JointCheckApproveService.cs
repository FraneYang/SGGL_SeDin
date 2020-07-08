using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class JointCheckApproveService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
        /// <summary>
        /// 根据质量共检编号删除对应的所有质量共检审批信息
        /// </summary>
        /// <param name="JointCheckCode">质量共检编号</param>
        public static void DeleteJointCheckApprovesByJointCheckId(string JointCheckId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var q = (from x in db.Check_JointCheckApprove where x.JointCheckId == JointCheckId select x).ToList();
            db.Check_JointCheckApprove.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }
        /// <summary>
        /// 获取质量共检模板列表
        /// </summary>
        /// <param name="satartRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListData(string JointCheckId)
        {
            var approves = (from x in new Model.SGGLDB(Funs.ConnString).Check_JointCheckApprove where x.JointCheckId == JointCheckId && x.ApproveDate != null && x.ApproveType != "S" orderby x.ApproveDate select x).ToList();
            var approves2 = (from x in new Model.SGGLDB(Funs.ConnString).Check_JointCheckApprove where x.JointCheckId == JointCheckId && x.ApproveDate != null && x.ApproveType != "S" orderby x.ApproveDate select x).ToList();
            string approveType = string.Empty;
            DateTime? date = null;
            foreach (var approve in approves2)
            {
                if (approve.ApproveType == approveType) //同一办理类型
                {
                    DateTime date1 = approve.ApproveDate.Value;
                    if (date != null)
                    {
                        TimeSpan t = date1 - Convert.ToDateTime(date);
                        if (t.Minutes < 1)
                        {
                            approves.Remove(approve);
                        }
                    }
                }
                approveType = approve.ApproveType;
                date = approve.ApproveDate;
            }
            var res = from x in approves
                      where x.JointCheckId == JointCheckId && x.ApproveDate != null && x.ApproveType != "S"
                      orderby x.ApproveDate
                      select new
                      {
                          x.JointCheckApproveId,
                          x.JointCheckId,
                          ApproveMan = x.ApproveMan == null ? "" : (from y in db.Sys_User where y.UserId == x.ApproveMan select y.UserName).First(),
                          x.ApproveDate,
                          x.IsAgree,
                          x.ApproveIdea,
                          x.ApproveType,
                      };
            return res;
        }
        /// <summary>
        /// 获取质量共检模板列表
        /// </summary>
        /// <param name="satartRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable listData(string JointCheckId)
        {
            var approves = (from x in new Model.SGGLDB(Funs.ConnString).Check_JointCheckApprove where x.JointCheckId == JointCheckId && x.ApproveDate != null && x.ApproveType != "S" orderby x.ApproveDate select x).ToList();
            var approves2 = (from x in new Model.SGGLDB(Funs.ConnString).Check_JointCheckApprove where x.JointCheckId == JointCheckId && x.ApproveDate != null && x.ApproveType != "S" orderby x.ApproveDate select x).ToList();
            string approveType = string.Empty;
            DateTime? date = null;
            foreach (var approve in approves2)
            {
                if (approve.ApproveType == approveType) //同一办理类型
                {
                    DateTime date1 = approve.ApproveDate.Value;
                    if (date != null)
                    {
                        TimeSpan t = date1 - Convert.ToDateTime(date);
                        if (t.Minutes < 1)
                        {
                            approves.Remove(approve);
                        }
                    }
                }
                approveType = approve.ApproveType;
                date = approve.ApproveDate;
            }
            var res = from x in approves
                      where x.JointCheckId == JointCheckId && x.ApproveDate != null && x.ApproveType != "S"
                      orderby x.ApproveDate
                      select new
                      {
                          x.JointCheckApproveId,
                          x.JointCheckId,
                          ApproveMan = (from y in db.Sys_User where y.UserId == x.ApproveMan select y.UserName).First(),
                          x.ApproveDate,
                          x.IsAgree,
                          x.ApproveIdea,
                          x.ApproveType,
                      };
            return res.AsEnumerable().ToList();
        }

        /// <summary>
        /// 根据质量共检编号获取质量共检审批信息集合
        /// </summary>
        /// <param name="JointCheckCode">质量共检编号</param>
        /// <returns>质量共检审批集合</returns>
        public static List<Model.Check_JointCheckApprove> GetJointCheckApprovesByJointCheckId(string JointCheckId, string ApproveMan)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Check_JointCheckApprove where x.JointCheckId == JointCheckId && x.ApproveType != "S" && x.ApproveDate == null && x.ApproveMan == ApproveMan select x).ToList();
        }
        /// <summary>
        /// 增加质量共检审批信息
        /// </summary>
        /// <param name="managerRuleApprove">质量共检审批实体</param>
        public static string AddJointCheckApproveForApi(Model.Check_JointCheckApprove approve)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                string newKeyID = SQLHelper.GetNewID(typeof(Model.Check_JointCheckApprove));
                Model.Check_JointCheckApprove newApprove = new Model.Check_JointCheckApprove();
                newApprove.JointCheckApproveId = newKeyID;
                newApprove.JointCheckId = approve.JointCheckId;
                newApprove.ApproveMan = approve.ApproveMan;
                newApprove.ApproveDate = approve.ApproveDate;
                newApprove.ApproveIdea = approve.ApproveIdea;
                newApprove.IsAgree = approve.IsAgree;
                newApprove.ApproveType = approve.ApproveType;
                newApprove.JointCheckDetailId = approve.JointCheckDetailId;
                db.Check_JointCheckApprove.InsertOnSubmit(newApprove);
                db.SubmitChanges();
                return newKeyID;
            }
        }
        public static void AddJointCheckApprove(Model.Check_JointCheckApprove approve)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Check_JointCheckApprove));
            Model.Check_JointCheckApprove newApprove = new Model.Check_JointCheckApprove();
            newApprove.JointCheckApproveId = newKeyID;
            newApprove.JointCheckId = approve.JointCheckId;
            newApprove.ApproveMan = approve.ApproveMan;
            newApprove.ApproveDate = approve.ApproveDate;
            newApprove.ApproveIdea = approve.ApproveIdea;
            newApprove.IsAgree = approve.IsAgree;
            newApprove.ApproveType = approve.ApproveType;
            newApprove.JointCheckDetailId = approve.JointCheckDetailId;

            db.Check_JointCheckApprove.InsertOnSubmit(newApprove);
            db.SubmitChanges();
        }
        /// <summary>
        /// 修改质量共检审批信息
        /// </summary>
        /// <param name="managerRuleApprove">质量共检审批实体</param>
        public static void UpdateJointCheckApprove(Model.Check_JointCheckApprove approve)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Check_JointCheckApprove newApprove = db.Check_JointCheckApprove.First(e => e.JointCheckApproveId == approve.JointCheckApproveId && e.ApproveDate == null);
            newApprove.JointCheckId = approve.JointCheckId;
            newApprove.ApproveMan = approve.ApproveMan;
            newApprove.ApproveDate = approve.ApproveDate;
            newApprove.ApproveIdea = approve.ApproveIdea;
            newApprove.IsAgree = approve.IsAgree;
            newApprove.ApproveType = approve.ApproveType;

            db.SubmitChanges();
        }
        public static Model.Check_JointCheckApprove GetSee(string JointCheckId, string userId)
        {
            return db.Check_JointCheckApprove.FirstOrDefault(x => x.JointCheckId == JointCheckId && x.ApproveType == "S" && x.ApproveMan == userId && x.ApproveDate == null);
        }
        public static void See(string JointCheckId, string userId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                var item = db.Check_JointCheckApprove.FirstOrDefault(x => x.JointCheckId == JointCheckId && x.ApproveType == "S" && x.ApproveMan == userId && x.ApproveDate == null);
                if (item != null)
                {
                    item.ApproveDate = DateTime.Now;
                    db.SubmitChanges();
                }
            }
        }
        /// <summary>
        /// 根据质量共检编号获取所有未办理质量共检审批信息
        /// </summary>
        /// <param name="JointCheckCode">质量共检编号</param>
        /// <returns>所有未办理质量共检审批实体</returns>
        public static List<Model.Check_JointCheckApprove> GetJointCheckApprovesByJointCheckId(string JointCheckId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Check_JointCheckApprove where x.JointCheckId == JointCheckId && x.ApproveType != "S" && x.ApproveDate == null select x).ToList();
        }

        /// <summary>
        /// 根据质量共检编号获取一个质量共检审批信息
        /// </summary>
        /// <param name="JointCheckCode">质量共检编号</param>
        /// <returns>一个质量共检审批实体</returns>
        public static Model.Check_JointCheckApprove GetJointCheckApproveByJointCheckId(string JointCheckId, string ApproveMan)
        {
            return new Model.SGGLDB(Funs.ConnString).Check_JointCheckApprove.FirstOrDefault(x => x.JointCheckId == JointCheckId && x.ApproveDate == null && x.ApproveMan == ApproveMan);
        }

        /// <summary>
        /// 根据质量共检明细Id获取一个质量共检审批信息
        /// </summary>
        /// <param name="JointCheckCode">质量共检编号</param>
        /// <returns>一个质量共检审批实体</returns>
        public static Model.Check_JointCheckApprove GetJointCheckApproveByJointCheckDetailId(string JointCheckDetailId, string ApproveMan)
        {
            return new Model.SGGLDB(Funs.ConnString).Check_JointCheckApprove.FirstOrDefault(x => x.JointCheckDetailId == JointCheckDetailId && x.ApproveDate == null && x.ApproveType != "S" && x.ApproveMan == ApproveMan);
        }

        /// <summary>
        /// 分包专工回复信息
        /// </summary>
        /// <param name="JointCheckId"></param>
        /// <returns></returns>
        public static Model.Check_JointCheckApprove GetAudit1(string JointCheckId, string jointCheckDetailId)
        {
            return db.Check_JointCheckApprove.OrderByDescending(x => x.ApproveDate).FirstOrDefault(x => x.JointCheckId == JointCheckId && x.ApproveType == BLL.Const.JointCheck_Audit1 && x.JointCheckDetailId == jointCheckDetailId);
        }

        /// <summary>
        /// 分包负责人审核信息
        /// </summary>
        /// <param name="JointCheckId"></param>
        /// <returns></returns>
        public static Model.Check_JointCheckApprove GetAudit2(string JointCheckId, string jointCheckDetailId)
        {
            return db.Check_JointCheckApprove.OrderByDescending(x => x.ApproveDate).FirstOrDefault(x => x.JointCheckId == JointCheckId && x.ApproveType == BLL.Const.JointCheck_Audit2 && x.JointCheckDetailId == jointCheckDetailId);
        }

        /// <summary>
        /// 总包专工确认信息
        /// </summary>
        /// <param name="JointCheckId"></param>
        /// <returns></returns>
        public static Model.Check_JointCheckApprove GetAudit3(string JointCheckId, string jointCheckDetailId)
        {
            return db.Check_JointCheckApprove.OrderByDescending(x => x.ApproveDate).FirstOrDefault(x => x.JointCheckId == JointCheckId && x.ApproveType == BLL.Const.JointCheck_Audit3 && x.JointCheckDetailId == jointCheckDetailId);
        }

        /// <summary>
        /// 总包负责人审核信息
        /// </summary>
        /// <param name="JointCheckId"></param>
        /// <returns></returns>
        public static Model.Check_JointCheckApprove GetAudit4(string JointCheckId, string jointCheckDetailId)
        {
            return db.Check_JointCheckApprove.OrderByDescending(x => x.ApproveDate).FirstOrDefault(x => x.JointCheckId == JointCheckId && x.ApproveType == BLL.Const.JointCheck_Audit4 && x.JointCheckDetailId == jointCheckDetailId);
        }
        public static List<Model.Check_JointCheckApprove> getListDataByJcidForApi(string JointCheckId, string JointCheckDetailId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                var q = from x in db.Check_JointCheckApprove
                        where x.JointCheckId == JointCheckId && x.JointCheckDetailId == JointCheckDetailId && x.ApproveDate != null && x.ApproveType != "S"
                        orderby x.ApproveDate
                        select new
                        {
                            x.JointCheckApproveId,
                            x.JointCheckId,
                            ApproveMan = (from y in db.Sys_User where y.UserId == x.ApproveMan select y.UserName).First(),
                            x.ApproveDate,
                            x.IsAgree,
                            x.ApproveIdea,
                            x.ApproveType,
                        };
                List<Model.Check_JointCheckApprove> res = new List<Model.Check_JointCheckApprove>();
                var list = q.ToList();
                foreach (var item in list)
                {
                    Model.Check_JointCheckApprove jc = new Model.Check_JointCheckApprove();
                    jc.JointCheckApproveId = item.JointCheckApproveId;
                    jc.JointCheckId = item.JointCheckApproveId;
                    jc.ApproveMan = item.ApproveMan;
                    jc.ApproveDate = item.ApproveDate;
                    jc.IsAgree = item.IsAgree;
                    jc.ApproveIdea = item.ApproveIdea;
                    jc.ApproveType = item.ApproveType;
                    res.Add(jc);
                }
                return res;
            }
        }
        public static Model.Check_JointCheckApprove getCurrApproveByDetailIdForApi(string id)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                List<Model.Check_JointCheckApprove> res = new List<Model.Check_JointCheckApprove>();

                var q = from e in db.Check_JointCheckApprove
                        where e.JointCheckDetailId == id && e.ApproveType != "S" && e.ApproveDate == null
                        select e;
                return q.FirstOrDefault();
            }
        }
        public static List<Model.Check_JointCheckApprove> getCurrApproveForApi(string id)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                List<Model.Check_JointCheckApprove> res = new List<Model.Check_JointCheckApprove>();

                var q = from e in db.Check_JointCheckApprove
                        where e.JointCheckId == id && e.ApproveType != "S" && e.ApproveDate == null
                        select e;
                //Model.Check_JointCheckApprove newApprove = db.Check_JointCheckApprove.FirstOrDefault(e => e.JointCheckId == id && e.ApproveType != "S" && e.ApproveDate == null);
                var list = q.ToList();
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        Model.Check_JointCheckApprove approve = new Model.Check_JointCheckApprove();
                        approve.ApproveDate = item.ApproveDate;
                        approve.ApproveIdea = item.ApproveIdea;
                        approve.ApproveMan = item.ApproveMan;
                        approve.ApproveType = item.ApproveType;
                        approve.JointCheckApproveId = item.JointCheckApproveId;
                        approve.JointCheckDetailId = item.JointCheckDetailId;
                        approve.JointCheckId = item.JointCheckId;
                        Model.Sys_User user = BLL.UserService.GetUserByUserId(item.ApproveMan);
                        if (user != null)
                        {
                            approve.ApproveIdea = user.UserName;
                        }

                        res.Add(approve);
                    }
                }

                return res;
            }
        }
        public static Model.Check_JointCheckApprove getCurrApproveByJoinCheckIdForApi(string id)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                List<Model.Check_JointCheckApprove> res = new List<Model.Check_JointCheckApprove>();
                Model.Check_JointCheckApprove item = db.Check_JointCheckApprove.FirstOrDefault(e => e.JointCheckId == id && e.ApproveType != "S" && e.ApproveDate == null);
                if (item != null)
                {
                    Model.Sys_User user = BLL.UserService.GetUserByUserId(item.ApproveMan);
                    if (user != null)
                    {
                        item.ApproveIdea = user.UserName;
                    }
                    Model.Check_JointCheckApprove approve = new Model.Check_JointCheckApprove();
                    approve.ApproveDate = item.ApproveDate;
                    approve.ApproveIdea = item.ApproveIdea;
                    approve.ApproveMan = item.ApproveMan;
                    approve.ApproveType = item.ApproveType;
                    approve.JointCheckApproveId = item.JointCheckApproveId;
                    approve.JointCheckDetailId = item.JointCheckDetailId;
                    approve.JointCheckId = item.JointCheckId;
                    return approve;
                }
                else
                {
                    return null;
                }
            }
        }
        public static Model.Check_JointCheckApprove UpdateJointCheckApproveForApi(Model.Check_JointCheckApprove approve)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Check_JointCheckApprove newApprove = db.Check_JointCheckApprove.FirstOrDefault(e => e.JointCheckApproveId == approve.JointCheckApproveId);
                if (newApprove != null)
                {
                    if (!string.IsNullOrEmpty(approve.JointCheckId))
                        newApprove.JointCheckId = approve.JointCheckId;
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
                    return newApprove;
                }
                return null;
            };

        }
    }
}
