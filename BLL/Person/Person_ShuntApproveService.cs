using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Person_ShuntApproveService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 获取分流管理模板列表
        /// </summary>
        /// <param name="satartRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListData(string ShuntId)
        {
            return from x in db.Person_ShuntApprove
                   where x.ShuntId == ShuntId && x.ApproveDate != null
                   orderby x.ApproveDate
                   select new
                   {
                       x.ShuntApproveId,
                       x.ShuntId,
                       ApproveMan = (from y in db.Sys_User where y.UserId == x.ApproveMan select y.UserName).First(),
                       x.ApproveDate,
                       x.IsAgree,
                       x.ApproveIdea,
                       x.ApproveType,
                   };
        }

        /// <summary>
        /// 根据分流管理编号获取一个分流管理审批信息
        /// </summary>
        /// <param name="ShuntCode">分流管理编号</param>
        /// <returns>一个分流管理审批实体</returns>
        public static Model.Person_ShuntApprove GetShuntApproveByShuntApproveId(string ShuntApproveId)
        {
            return db.Person_ShuntApprove.FirstOrDefault(x => x.ShuntApproveId == ShuntApproveId);
        }

        /// <summary>
        /// 根据分流管理编号获取一个分流管理审批信息
        /// </summary>
        /// <param name="ShuntCode">分流管理编号</param>
        /// <returns>一个分流管理审批实体</returns>
        public static Model.Person_ShuntApprove GetShuntApproveByShuntId(string ShuntId)
        {
            return db.Person_ShuntApprove.FirstOrDefault(x => x.ShuntId == ShuntId && x.ApproveDate == null);
        }

        /// <summary>
        /// 根据分流管理审批编号获取一个分流管理审批信息
        /// </summary>
        /// <param name="ShuntCode">分流管理编号</param>
        /// <returns>一个分流管理审批实体</returns>
        public static Model.Person_ShuntApprove GetShuntApproveByApproveId(string approveId)
        {
            return db.Person_ShuntApprove.FirstOrDefault(x => x.ShuntApproveId == approveId);
        }

        public static Model.Person_ShuntApprove GetComplie(string ShuntId)
        {
            return db.Person_ShuntApprove.FirstOrDefault(x => x.ShuntId == ShuntId && x.ApproveType == BLL.Const.Shunt_Compile);
        }

        public static Model.Person_ShuntApprove GetAudit(string ShuntId)
        {
            return db.Person_ShuntApprove.FirstOrDefault(x => x.ShuntId == ShuntId && x.ApproveType == BLL.Const.Shunt_Audit);
        }

        /// <summary>
        /// 根据分流管理发布Id获取所以对应分流管理审批信息
        /// </summary>
        /// <param name="ShuntCode">分流管理发布Id</param>
        /// <returns>分流管理审批集合</returns>
        public static List<Model.Person_ShuntApprove> GetShuntApprovesByShuntApproveId(string ShuntApproveId)
        {
            return (from x in db.Person_ShuntApprove where x.ShuntApproveId == ShuntApproveId select x).ToList();
        }

        /// <summary>
        /// 增加分流管理审批信息
        /// </summary>
        /// <param name="managerRuleApprove">分流管理审批实体</param>
        public static void AddShuntApprove(Model.Person_ShuntApprove approve)
        {
            Model.SGGLDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Person_ShuntApprove));
            Model.Person_ShuntApprove newApprove = new Model.Person_ShuntApprove();
            newApprove.ShuntApproveId = newKeyID;
            newApprove.ShuntId = approve.ShuntId;
            newApprove.ApproveMan = approve.ApproveMan;
            newApprove.ApproveDate = approve.ApproveDate;
            newApprove.ApproveIdea = approve.ApproveIdea;
            newApprove.IsAgree = approve.IsAgree;
            newApprove.ApproveType = approve.ApproveType;

            db.Person_ShuntApprove.InsertOnSubmit(newApprove);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改分流管理审批信息
        /// </summary>
        /// <param name="managerRuleApprove">分流管理审批实体</param>
        public static void UpdateShuntApprove(Model.Person_ShuntApprove approve)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Person_ShuntApprove newApprove = db.Person_ShuntApprove.First(e => e.ShuntApproveId == approve.ShuntApproveId && e.ApproveDate == null);
            newApprove.ShuntId = approve.ShuntId;
            newApprove.ApproveMan = approve.ApproveMan;
            newApprove.ApproveDate = approve.ApproveDate;
            newApprove.ApproveIdea = approve.ApproveIdea;
            newApprove.IsAgree = approve.IsAgree;
            newApprove.ApproveType = approve.ApproveType;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据分流管理编号删除对应的所有分流管理审批信息
        /// </summary>
        /// <param name="ShuntCode">分流管理编号</param>
        public static void DeleteShuntApprovesByShuntId(string ShuntId)
        {
            Model.SGGLDB db = Funs.DB;
            var q = (from x in db.Person_ShuntApprove where x.ShuntId == ShuntId select x).ToList();
            db.Person_ShuntApprove.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据用户主键获得分流管理审批的数量
        /// </summary>
        /// <param name="userId">角色</param>
        /// <returns></returns>
        public static int GetManagerRuleApproveCountByUserId(string userId)
        {
            var q = (from x in Funs.DB.Person_ShuntApprove where x.ApproveMan == userId select x).ToList();
            return q.Count();
        }
    }
}
