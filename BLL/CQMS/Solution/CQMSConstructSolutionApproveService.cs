using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CQMSConstructSolutionApproveService
    {
        public static Model.Solution_CQMSConstructSolutionApprove GetSee(string ConstructSolutionId, string userId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            return db.Solution_CQMSConstructSolutionApprove.FirstOrDefault(x => x.ConstructSolutionId == ConstructSolutionId && x.ApproveType == "S" && x.ApproveMan == userId && x.ApproveDate == null);
        }

        public static void See(string ConstructSolutionId, string userId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                var res = db.Solution_CQMSConstructSolutionApprove.FirstOrDefault(x => x.ConstructSolutionId == ConstructSolutionId && x.ApproveType == "S" && x.ApproveMan == userId && x.ApproveDate == null);
                if (res != null)
                {
                    res.ApproveDate = DateTime.Now;
                    db.SubmitChanges();
                }
            }
        }
        /// <summary>
        /// 根据施工方案发布Id获取对应组会签人id集合信息
        /// </summary>
        /// <param name="ConstructSolutionCode">施工方案发布Id</param>
        /// <returns>施工方案审批集合</returns>
        public static List<string> GetUserIdsApprovesBySignType(string ConstructSolutionId, string signType)
        {
            var edtion = GetUserIdsApprovesBySignTypeEditon(ConstructSolutionId);
            return (from x in new Model.SGGLDB(Funs.ConnString).Solution_CQMSConstructSolutionApprove where x.ConstructSolutionId == ConstructSolutionId && x.ApproveType != "S" && x.SignType == signType && x.Edition == edtion select x.ApproveMan).ToList();
        }

        public static int? GetUserIdsApprovesBySignTypeEditon(string ConstructSolutionId)
        {
            int edition = 0;
            var solution = new Model.SGGLDB(Funs.ConnString).Solution_CQMSConstructSolution.FirstOrDefault(p => p.ConstructSolutionId == ConstructSolutionId);
            if (solution != null)
            {
                edition = Convert.ToInt32(solution.Edition);
            }
            return edition;
        }

        /// <summary>
        /// 修改施工方案审批信息
        /// </summary>
        /// <param name="managerRuleApprove">施工方案审批实体</param>
        public static void UpdateConstructSolutionApprove(Model.Solution_CQMSConstructSolutionApprove approve)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Solution_CQMSConstructSolutionApprove newApprove = db.Solution_CQMSConstructSolutionApprove.First(e => e.ConstructSolutionApproveId == approve.ConstructSolutionApproveId && e.ApproveDate == null);
            newApprove.ConstructSolutionId = approve.ConstructSolutionId;
            newApprove.ApproveMan = approve.ApproveMan;
            newApprove.ApproveDate = approve.ApproveDate;
            newApprove.ApproveIdea = approve.ApproveIdea;
            newApprove.IsAgree = approve.IsAgree;
            newApprove.ApproveType = approve.ApproveType;
            newApprove.Edition = approve.Edition;
            db.SubmitChanges();
        }
        /// <summary>
        /// 根据施工方案发布Id获取所以对应施工方案审批信息(查询全部会签状态)
        /// </summary>
        /// <param name="ConstructSolutionCode">施工方案发布Id</param>
        /// <returns>施工方案审批集合</returns>
        public static List<Model.Solution_CQMSConstructSolutionApprove> GetHandleConstructSolutionApprovesByConstructSolutionId(string ConstructSolutionId, int edition)
        {
            var list = new Model.SGGLDB(Funs.ConnString).Solution_CQMSConstructSolutionApprove.Where(p => p.ConstructSolutionId == ConstructSolutionId && p.ApproveType == Const.CQMSConstructSolution_Audit && p.Edition == edition).ToList();
            return list;

        }

        public static List<Model.Solution_CQMSConstructSolutionApprove> GetHandleConstruct(string ConstructSolutionId, int edition)
        {
            var list = new Model.SGGLDB(Funs.ConnString).Solution_CQMSConstructSolutionApprove.Where(p => p.ConstructSolutionId == ConstructSolutionId && p.ApproveType != "S" && p.ApproveDate != null && p.Edition == edition).ToList();
            return list;

        }
        /// <summary>
        /// 根据施工方案发布Id获取所以对应施工方案审批信息
        /// </summary>
        /// <param name="ConstructSolutionCode">施工方案发布Id</param>
        /// <returns>施工方案审批集合</returns>
        public static List<Model.Solution_CQMSConstructSolutionApprove> GetConstructSolutionApprovesByConstructSolutionId(string ConstructSolutionId, string state)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var reDate = (from x in db.Solution_CQMSConstructSolutionApprove where x.ConstructSolutionId == ConstructSolutionId && x.ApproveType == BLL.Const.CQMSConstructSolution_ReCompile orderby x.ApproveDate descending select x.ApproveDate).FirstOrDefault();
            if (reDate == null)
            {
                return (from x in db.Solution_CQMSConstructSolutionApprove where x.ConstructSolutionId == ConstructSolutionId && x.ApproveType == state select x).ToList();
            }
            else
            {
                return (from x in db.Solution_CQMSConstructSolutionApprove where x.ConstructSolutionId == ConstructSolutionId && x.ApproveType == state && (x.ApproveDate == null || x.ApproveDate > reDate) select x).ToList();
            }
        }

        /// <summary>
        /// 获取施工方案模板列表
        /// </summary>
        /// <param name="satartRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IList<Model.Solution_CQMSConstructSolutionApprove> getListData(string ConstructSolutionId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var res = from x in db.Solution_CQMSConstructSolutionApprove
                      where x.ConstructSolutionId == ConstructSolutionId && x.ApproveDate != null && x.ApproveType != "S"
                      orderby x.ApproveDate
                      select x;

            //select new
            //{
            //    x.ConstructSolutionApproveId,
            //    x.ConstructSolutionId,
            //    x.ApproveMan,
            //    x.ApproveDate,
            //    x.IsAgree,
            //    x.ApproveIdea,
            //    x.ApproveType,
            //    x.SignType,
            //    //= x.IsAgree == true ? "是" : "否",
            //};
            return res.ToList();
        }

        /// <summary>
        /// 获取未代办的记录数量
        /// </summary>
        /// <param name="satartRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static int getListSolutionApproveCount(string constructSolutionId, string man)
        {

            var res = (from x in new Model.SGGLDB(Funs.ConnString).Solution_CQMSConstructSolutionApprove
                       where x.ConstructSolutionId == constructSolutionId && x.ApproveDate == null && x.ApproveType != "S" && x.ApproveMan.Equals(man)
                       orderby x.ApproveDate
                       select x).Count();
            return res;
        }
        public static IList<Model.Solution_CQMSConstructSolutionApprove> getListSolutionApprove(string constructSolutionId, string man)
        {

            var res = (from x in new Model.SGGLDB(Funs.ConnString).Solution_CQMSConstructSolutionApprove
                       where x.ConstructSolutionId == constructSolutionId && x.ApproveDate == null && x.ApproveType != "S" && x.ApproveMan.Equals(man)
                       orderby x.ApproveDate
                       select x).ToList();
            return res;
        }
        /// <summary>
        /// 删除未代办的记录
        /// </summary>
        /// <param name="ConstructSolutionId"></param>
        public static void delSolutionApprove(string constructSolutionId, string man)
        {
            var q = (from x in new Model.SGGLDB(Funs.ConnString).Solution_CQMSConstructSolutionApprove where x.ConstructSolutionId == constructSolutionId && x.ApproveType != "S" && x.ApproveMan.Equals(man) && x.ApproveDate == null select x).ToList();
            new Model.SGGLDB(Funs.ConnString).Solution_CQMSConstructSolutionApprove.DeleteAllOnSubmit(q);
            new Model.SGGLDB(Funs.ConnString).SubmitChanges();

        }


        /// <summary>
        /// 根据施工方案编号删除对应的所有施工方案审批信息
        /// </summary>
        /// <param name="ConstructSolutionCode">施工方案编号</param>
        public static void DeleteConstructSolutionApprovesByConstructSolutionId(string ConstructSolutionId)
        {
            var q = (from x in new Model.SGGLDB(Funs.ConnString).Solution_CQMSConstructSolutionApprove where x.ConstructSolutionId == ConstructSolutionId select x).ToList();
            new Model.SGGLDB(Funs.ConnString).Solution_CQMSConstructSolutionApprove.DeleteAllOnSubmit(q);
            new Model.SGGLDB(Funs.ConnString).SubmitChanges();
        }
        /// <summary>
        /// 根据施工方案编号获取一个施工方案审批信息
        /// </summary>
        /// <param name="ConstructSolutionCode">施工方案编号</param>
        /// <returns>一个施工方案审批实体</returns>
        public static Model.Solution_CQMSConstructSolutionApprove GetConstructSolutionApproveByApproveMan(string ConstructSolutionId, string approveMan)
        {

            return new Model.SGGLDB(Funs.ConnString).Solution_CQMSConstructSolutionApprove.FirstOrDefault(x => x.ConstructSolutionId == ConstructSolutionId && x.ApproveMan == approveMan && x.ApproveType != "S" && x.ApproveDate == null);
        }
        public static Model.Solution_CQMSConstructSolutionApprove GetConstructSoluAppByApproveMan(string ConstructSolutionId, string approveMan, int edtion)
        {

            return new Model.SGGLDB(Funs.ConnString).Solution_CQMSConstructSolutionApprove.FirstOrDefault(x => x.ConstructSolutionId == ConstructSolutionId && x.Edition == edtion && x.ApproveMan == approveMan && x.ApproveType != "S" && x.ApproveDate == null);
        }

        public static Model.Solution_CQMSConstructSolutionApprove GetConstructSolApproveByApproveMan(string ConstructSolutionId, string approveMan)
        {
            return new Model.SGGLDB(Funs.ConnString).Solution_CQMSConstructSolutionApprove.FirstOrDefault(x => x.ConstructSolutionId == ConstructSolutionId && x.ApproveMan == approveMan && x.ApproveType != "S" && x.ApproveDate == null);
        }


        /// <summary>
        /// 增加施工方案审批信息
        /// </summary>
        /// <param name="managerRuleApprove">施工方案审批实体</param>
        public static void AddConstructSolutionApprove(Model.Solution_CQMSConstructSolutionApprove approve)
        {
            Model.Solution_CQMSConstructSolutionApprove newApprove = new Model.Solution_CQMSConstructSolutionApprove();
            if (string.IsNullOrWhiteSpace(approve.ConstructSolutionApproveId))
            {
                newApprove.ConstructSolutionApproveId = SQLHelper.GetNewID(typeof(Model.Solution_CQMSConstructSolutionApprove));
            }
            else
            {
                newApprove.ConstructSolutionApproveId = approve.ConstructSolutionApproveId;
            }
            newApprove.ConstructSolutionId = approve.ConstructSolutionId;
            newApprove.ApproveMan = approve.ApproveMan;
            newApprove.ApproveDate = approve.ApproveDate;
            newApprove.ApproveIdea = approve.ApproveIdea;
            newApprove.IsAgree = approve.IsAgree;
            newApprove.ApproveType = approve.ApproveType;
            newApprove.SignType = approve.SignType;
            newApprove.Edition = approve.Edition;
            new Model.SGGLDB(Funs.ConnString).Solution_CQMSConstructSolutionApprove.InsertOnSubmit(newApprove);
            new Model.SGGLDB(Funs.ConnString).SubmitChanges();
        }
        public static void AddConstructSolutionApproveForApi(Model.Solution_CQMSConstructSolutionApprove approve)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Solution_CQMSConstructSolutionApprove newApprove = new Model.Solution_CQMSConstructSolutionApprove();
                if (string.IsNullOrWhiteSpace(approve.ConstructSolutionApproveId))
                {
                    newApprove.ConstructSolutionApproveId = SQLHelper.GetNewID(typeof(Model.Solution_CQMSConstructSolutionApprove));
                }
                else
                {
                    newApprove.ConstructSolutionApproveId = approve.ConstructSolutionApproveId;
                }
                newApprove.ConstructSolutionId = approve.ConstructSolutionId;
                newApprove.ApproveMan = approve.ApproveMan;
                newApprove.ApproveDate = approve.ApproveDate;
                newApprove.ApproveIdea = approve.ApproveIdea;
                newApprove.IsAgree = approve.IsAgree;
                newApprove.ApproveType = approve.ApproveType;
                newApprove.SignType = approve.SignType;
                newApprove.Edition = approve.Edition;
                db.Solution_CQMSConstructSolutionApprove.InsertOnSubmit(newApprove);
                db.SubmitChanges();
            }
        }
        public static List<Model.Solution_CQMSConstructSolutionApprove> getListDataForApi(string ConstructSolutionId, int edition)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                var q = from x in db.Solution_CQMSConstructSolutionApprove
                        where x.ConstructSolutionId == ConstructSolutionId && x.Edition == edition && x.ApproveDate != null && x.ApproveType != "S"
                        orderby x.ApproveDate
                        select new
                        {
                            x.ConstructSolutionApproveId,
                            x.ConstructSolutionId,
                            x.ApproveMan,
                            ApproveManName = (from y in db.Sys_User where y.UserId == x.ApproveMan select y.UserName).First(),
                            x.ApproveDate,
                            x.IsAgree,
                            x.ApproveIdea,
                            x.ApproveType,
                            x.SignType,
                        };
                var list = q.ToList();
                List<Model.Solution_CQMSConstructSolutionApprove> res = new List<Model.Solution_CQMSConstructSolutionApprove>();
                foreach (var item in list)
                {
                    Model.Solution_CQMSConstructSolutionApprove ap = new Model.Solution_CQMSConstructSolutionApprove();
                    ap.ConstructSolutionApproveId = item.ConstructSolutionApproveId;
                    ap.ConstructSolutionId = item.ConstructSolutionId;
                    ap.ApproveMan = item.ApproveMan + "$" + item.ApproveManName;
                    ap.ApproveDate = item.ApproveDate;
                    ap.IsAgree = item.IsAgree;
                    ap.ApproveIdea = item.ApproveIdea;
                    ap.ApproveType = item.ApproveType;
                    ap.SignType = item.SignType;
                    ap.AttachUrl = AttachFileService.getFileUrl(ap.ConstructSolutionApproveId);
                    res.Add(ap);
                }
                return res;
            }
        }
        public static List<Model.Solution_CQMSConstructSolutionApprove> getListDataForApi(string ConstructSolutionId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                var q = from x in db.Solution_CQMSConstructSolutionApprove
                        where x.ConstructSolutionId == ConstructSolutionId && x.ApproveDate != null && x.ApproveType != "S"
                        orderby x.ApproveDate
                        select new
                        {
                            x.ConstructSolutionApproveId,
                            x.ConstructSolutionId,
                            x.ApproveMan,
                            ApproveManName = (from y in db.Sys_User where y.UserId == x.ApproveMan select y.UserName).First(),
                            x.ApproveDate,
                            x.IsAgree,
                            x.ApproveIdea,
                            x.ApproveType,
                            x.SignType,
                        };
                var list = q.ToList();
                List<Model.Solution_CQMSConstructSolutionApprove> res = new List<Model.Solution_CQMSConstructSolutionApprove>();
                foreach (var item in list)
                {
                    Model.Solution_CQMSConstructSolutionApprove ap = new Model.Solution_CQMSConstructSolutionApprove();
                    ap.ConstructSolutionApproveId = item.ConstructSolutionApproveId;
                    ap.ConstructSolutionId = item.ConstructSolutionId;
                    ap.ApproveMan = item.ApproveMan + "$" + item.ApproveManName;
                    ap.ApproveDate = item.ApproveDate;
                    ap.IsAgree = item.IsAgree;
                    ap.ApproveIdea = item.ApproveIdea;
                    ap.ApproveType = item.ApproveType;
                    ap.SignType = item.SignType;
                    ap.AttachUrl = AttachFileService.getFileUrl(ap.ConstructSolutionApproveId);
                    res.Add(ap);
                }
                return res;
            }
        }
        public static Model.Solution_CQMSConstructSolutionApprove getCurrApproveForApi(string ConstructSolutionId, string approveMan, int edition)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Solution_CQMSConstructSolutionApprove newApprove = db.Solution_CQMSConstructSolutionApprove.FirstOrDefault(e => e.ConstructSolutionId == ConstructSolutionId && e.ApproveMan == approveMan && e.Edition == edition && e.ApproveType != "S" && e.ApproveDate == null);
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
        public static List<Model.Solution_CQMSConstructSolutionApprove> getConApproveForApi(string ConstructSolutionId, int edition)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                List<Model.Solution_CQMSConstructSolutionApprove> res = new List<Model.Solution_CQMSConstructSolutionApprove>();
                var newApproves = db.Solution_CQMSConstructSolutionApprove.Where(e => e.ConstructSolutionId == ConstructSolutionId && e.Edition == edition && e.ApproveType == "2").ToList();
                if (newApproves != null)
                {
                    foreach (Model.Solution_CQMSConstructSolutionApprove newApprove in newApproves)
                    {
                        Model.Solution_CQMSConstructSolutionApprove a = new Model.Solution_CQMSConstructSolutionApprove();
                        a.ConstructSolutionApproveId = newApprove.ConstructSolutionApproveId;
                        a.ConstructSolutionId = newApprove.ConstructSolutionId;
                        a.ApproveDate = newApprove.ApproveDate;
                        a.ApproveMan = newApprove.ApproveMan;
                        a.ApproveType = newApprove.ApproveType;
                        a.Edition = newApprove.Edition;
                        a.IsAgree = newApprove.IsAgree;
                        Model.Sys_User user = BLL.UserService.GetUserByUserId(newApprove.ApproveMan);
                        if (user != null)
                        {
                            a.ApproveMan = a.ApproveMan + "$" + user.UserName;
                        }
                        a.AttachUrl = AttachFileService.getFileUrl(a.ConstructSolutionApproveId);
                        res.Add(a);
                    }
                }
                return res;
            }
        }
        public static List<Model.Solution_CQMSConstructSolutionApprove> getConApprovesForApi(string ConstructSolutionId, int edition)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                List<Model.Solution_CQMSConstructSolutionApprove> res = new List<Model.Solution_CQMSConstructSolutionApprove>();
                var newApproves = db.Solution_CQMSConstructSolutionApprove.Where(e => e.ConstructSolutionId == ConstructSolutionId && e.Edition == edition).ToList();
                if (newApproves != null)
                {
                    foreach (Model.Solution_CQMSConstructSolutionApprove newApprove in newApproves)
                    {
                        Model.Solution_CQMSConstructSolutionApprove a = new Model.Solution_CQMSConstructSolutionApprove();
                        a.ConstructSolutionApproveId = newApprove.ConstructSolutionApproveId;
                        a.ConstructSolutionId = newApprove.ConstructSolutionId;
                        a.ApproveDate = newApprove.ApproveDate;
                        a.ApproveMan = newApprove.ApproveMan;
                        a.ApproveType = newApprove.ApproveType;
                        a.Edition = newApprove.Edition;
                        a.IsAgree = newApprove.IsAgree;
                        Model.Sys_User user = BLL.UserService.GetUserByUserId(newApprove.ApproveMan);
                        if (user != null)
                        {
                            a.ApproveMan = a.ApproveMan + "$" + user.UserName;
                        }
                        a.AttachUrl = AttachFileService.getFileUrl(a.ConstructSolutionApproveId);
                        res.Add(a);
                    }
                }
                return res;
            }
        }
        public static Model.Solution_CQMSConstructSolutionApprove UpdateConstructSolutionApproveForApi(Model.Solution_CQMSConstructSolutionApprove approve)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Solution_CQMSConstructSolutionApprove newApprove = db.Solution_CQMSConstructSolutionApprove.First(e => e.ConstructSolutionApproveId == approve.ConstructSolutionApproveId);
                if (!string.IsNullOrEmpty(approve.ConstructSolutionId))
                    newApprove.ConstructSolutionId = approve.ConstructSolutionId;
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
                AttachFileService.updateAttachFile(approve.AttachUrl, newApprove.ConstructSolutionApproveId, Const.CQMSConstructSolutionMenuId);
                return newApprove;
            }
        }
    }
}
