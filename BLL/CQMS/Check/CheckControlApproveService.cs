using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CheckControlApproveService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
        /// <summary>
        /// 获取质量巡检模板列表
        /// </summary>
        /// <param name="satartRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static DataTable getListData(string CheckControlCode)
        {
            var res = from x in db.Check_CheckControlApprove
                      where x.CheckControlCode == CheckControlCode && x.ApproveDate != null && x.ApproveType != "S"
                      orderby x.ApproveDate
                      select new
                      {
                          x.CheckControlApproveId,
                          x.CheckControlCode,
                          ApproveMan = (from y in db.Sys_User where y.UserId == x.ApproveMan select y.UserName).First(),
                          x.ApproveDate,
                          x.IsAgree,
                          x.ApproveIdea,
                          x.ApproveType,
                      };
            return Funs.LINQToDataTable(res);
        }
        /// <summary>
        /// 根据质量巡检编号删除对应的所有质量巡检审批信息
        /// </summary>
        /// <param name="CheckControlCode">质量巡检编号</param>
        public static void DeleteCheckControlApprovesByCheckControlCode(string CheckControlCode)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var q = (from x in db.Check_CheckControlApprove where x.CheckControlCode == CheckControlCode select x).ToList();
            db.Check_CheckControlApprove.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }
        /// <summary>
        /// 获取登录人的通知信息
        /// </summary>
        /// <param name="CheckControlCode"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static IQueryable getList(string userId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))            {
                var res = from x in db.Check_CheckControlApprove
                          join ca in db.Check_CheckControl on x.CheckControlCode equals ca.CheckControlCode
                          where x.ApproveDate == null && x.ApproveType == "S" && x.ApproveMan == userId
                          orderby x.ApproveDate
                          select new
                          {
                              //x.CheckControlApproveId,
                              x.CheckControlCode,
                              //x.ApproveDate,
                              //x.IsAgree,
                              //x.ApproveIdea,
                              //x.ApproveType,
                              ca.DocCode
                          };
                return res.AsQueryable().Distinct();
            }
        }
        /// <summary>
        /// 更新通知信息提醒
        /// </summary>
        /// <param name="CheckControlCode"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static Model.Check_CheckControlApprove GetSee(string CheckControlCode, string userId)
        {
            return db.Check_CheckControlApprove.FirstOrDefault(x => x.CheckControlCode == CheckControlCode && x.ApproveType == "S" && x.ApproveMan == userId && x.ApproveDate == null);
        }
        public static void See(string CheckControlCode, string userId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                var res = db.Check_CheckControlApprove.FirstOrDefault(x => x.CheckControlCode == CheckControlCode && x.ApproveType == "S" && x.ApproveMan == userId && x.ApproveDate == null);
                if (res != null)
                {
                    res.ApproveDate = DateTime.Now;
                    db.SubmitChanges();
                }
            }
        }
        /// <summary>
        /// 根据质量巡检编号获取一个质量巡检审批信息
        /// </summary>
        /// <param name="CheckControlCode">质量巡检编号</param>
        /// <returns>一个质量巡检审批实体</returns>
        public static Model.Check_CheckControlApprove GetCheckControlApproveByCheckControlId(string CheckControlCode)
        {
            return db.Check_CheckControlApprove.FirstOrDefault(x => x.CheckControlCode == CheckControlCode && x.ApproveType != "S" && x.ApproveDate == null);
        }
        /// <summary>
        /// 修改质量巡检审批信息
        /// </summary>
        /// <param name="managerRuleApprove">质量巡检审批实体</param>
        public static void UpdateCheckControlApprove(Model.Check_CheckControlApprove approve)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Check_CheckControlApprove newApprove = db.Check_CheckControlApprove.First(e => e.CheckControlApproveId == approve.CheckControlApproveId && e.ApproveDate == null);
            newApprove.CheckControlCode = approve.CheckControlCode;
            newApprove.ApproveMan = approve.ApproveMan;
            newApprove.ApproveDate = approve.ApproveDate;
            newApprove.ApproveIdea = approve.ApproveIdea;
            newApprove.IsAgree = approve.IsAgree;
            newApprove.ApproveType = approve.ApproveType;

            db.SubmitChanges();
        }
        /// <summary>
        /// 增加质量巡检审批信息
        /// </summary>
        /// <param name="managerRuleApprove">质量巡检审批实体</param>
        public static void AddCheckControlApprove(Model.Check_CheckControlApprove approve)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Check_CheckControlApprove));
            Model.Check_CheckControlApprove newApprove = new Model.Check_CheckControlApprove();
            newApprove.CheckControlApproveId = newKeyID;
            newApprove.CheckControlCode = approve.CheckControlCode;
            newApprove.ApproveMan = approve.ApproveMan;
            newApprove.ApproveDate = approve.ApproveDate;
            newApprove.ApproveIdea = approve.ApproveIdea;
            newApprove.IsAgree = approve.IsAgree;
            newApprove.ApproveType = approve.ApproveType;
            db.Check_CheckControlApprove.InsertOnSubmit(newApprove);
            db.SubmitChanges();

        }
        public static string AddCheckControlApproveForApi(Model.Check_CheckControlApprove approve)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                string newKeyID = SQLHelper.GetNewID(typeof(Model.Check_CheckControlApprove));
                Model.Check_CheckControlApprove newApprove = new Model.Check_CheckControlApprove();
                newApprove.CheckControlApproveId = newKeyID;
                newApprove.CheckControlCode = approve.CheckControlCode;
                newApprove.ApproveMan = approve.ApproveMan;
                newApprove.ApproveDate = approve.ApproveDate;
                newApprove.ApproveIdea = approve.ApproveIdea;
                newApprove.IsAgree = approve.IsAgree;
                newApprove.ApproveType = approve.ApproveType;

                db.Check_CheckControlApprove.InsertOnSubmit(newApprove);
                db.SubmitChanges();
                return newKeyID;
            }
        }
        /// <summary>
        /// 总包负责人审批信息
        /// </summary>
        /// <param name="CheckControlCode"></param>
        /// <returns></returns>
        public static Model.Check_CheckControlApprove GetAudit1(string CheckControlCode)
        {
            return db.Check_CheckControlApprove.OrderByDescending(x => x.ApproveDate).FirstOrDefault(x => x.CheckControlCode == CheckControlCode && x.ApproveType == BLL.Const.CheckControl_Audit1);
        }

        /// <summary>
        /// 分包专工回复信息
        /// </summary>
        /// <param name="CheckControlCode"></param>
        /// <returns></returns>
        public static Model.Check_CheckControlApprove GetAudit2(string CheckControlCode)
        {
            return db.Check_CheckControlApprove.OrderByDescending(x => x.ApproveDate).FirstOrDefault(x => x.CheckControlCode == CheckControlCode && x.ApproveType == BLL.Const.CheckControl_Audit2);
        }

        /// <summary>
        /// 分包负责人审批信息
        /// </summary>
        /// <param name="CheckControlCode"></param>
        /// <returns></returns>
        public static Model.Check_CheckControlApprove GetAudit3(string CheckControlCode)
        {
            return db.Check_CheckControlApprove.OrderByDescending(x => x.ApproveDate).FirstOrDefault(x => x.CheckControlCode == CheckControlCode && x.ApproveType == BLL.Const.CheckControl_Audit3);
        }

        /// <summary>
        /// 总包专工确认信息
        /// </summary>
        /// <param name="CheckControlCode"></param>
        /// <returns></returns>
        public static Model.Check_CheckControlApprove GetAudit4(string CheckControlCode)
        {
            return db.Check_CheckControlApprove.OrderByDescending(x => x.ApproveDate).FirstOrDefault(x => x.CheckControlCode == CheckControlCode && x.ApproveType == BLL.Const.CheckControl_Audit4);
        }

        /// <summary>
        /// 总包负责人审批信息
        /// </summary>
        /// <param name="CheckControlCode"></param>
        /// <returns></returns>
        public static Model.Check_CheckControlApprove GetAudit5(string CheckControlCode)
        {
            return db.Check_CheckControlApprove.OrderByDescending(x => x.ApproveDate).FirstOrDefault(x => x.CheckControlCode == CheckControlCode && x.ApproveType == BLL.Const.CheckControl_Audit5);
        }

        public static Model.Check_CheckControlApprove GetComplie(string CheckControlCode)
        {
            return db.Check_CheckControlApprove.FirstOrDefault(x => x.CheckControlCode == CheckControlCode && x.ApproveType == BLL.Const.CheckControl_Compile);
        }
        public static List<Model.Check_CheckControlApprove> GetListDataByCodeForApi(string code)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                var q = from x in db.Check_CheckControlApprove
                        where x.CheckControlCode == code && x.ApproveDate != null && x.ApproveType != "S"
                        orderby x.ApproveDate
                        select new
                        {
                            x.CheckControlApproveId,
                            x.CheckControlCode,
                            ApproveMan = (from y in db.Sys_User where y.UserId == x.ApproveMan select y.UserName).First(),
                            x.ApproveDate,
                            x.IsAgree,
                            x.ApproveIdea,
                            x.ApproveType,
                        };
                List<Model.Check_CheckControlApprove> res = new List<Model.Check_CheckControlApprove>();
                var list = q.ToList();
                foreach (var item in list)
                {
                    Model.Check_CheckControlApprove approve = new Model.Check_CheckControlApprove();
                    approve.CheckControlApproveId = item.CheckControlApproveId;
                    approve.CheckControlCode = item.CheckControlCode;
                    approve.ApproveMan = item.ApproveMan;
                    approve.ApproveDate = item.ApproveDate;
                    approve.IsAgree = item.IsAgree;
                    approve.ApproveIdea = item.ApproveIdea;
                    approve.ApproveType = item.ApproveType;
                    res.Add(approve);
                }
                return res;
            }
        }
        public static Model.Check_CheckControlApprove getCurrApproveForApi(string checkControlCode)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Check_CheckControlApprove newApprove = db.Check_CheckControlApprove.FirstOrDefault(e => e.CheckControlCode == checkControlCode && e.ApproveType != "S" && e.ApproveDate == null);
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
        public static void UpdateCheckControlApproveForApi(Model.Check_CheckControlApprove approve)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Check_CheckControlApprove newApprove = db.Check_CheckControlApprove.FirstOrDefault(e => e.CheckControlApproveId == approve.CheckControlApproveId && e.ApproveDate == null);
                if (newApprove != null)
                {
                    if (!string.IsNullOrEmpty(approve.CheckControlCode))
                    {
                        newApprove.CheckControlCode = approve.CheckControlCode;
                    }
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
