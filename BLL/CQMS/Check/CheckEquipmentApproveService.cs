using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CheckEquipmentApproveService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 获取设计变更模板列表
        /// </summary>
        /// <param name="satartRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListData(string CheckEquipmentId)
        {
            return from x in db.Check_CheckEquipmentApprove
                   where x.CheckEquipmentId == CheckEquipmentId && x.ApproveDate != null
                   orderby x.ApproveDate
                   select new
                   {
                       x.CheckEquipmentApproveId,
                       x.CheckEquipmentId,
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
        /// <param name="CheckEquipmentCode">设计变更编号</param>
        /// <returns>一个设计变更审批实体</returns>
        public static Model.Check_CheckEquipmentApprove GetCheckEquipmentApproveByCheckEquipmentId(string CheckEquipmentId)
        {
            return db.Check_CheckEquipmentApprove.FirstOrDefault(x => x.CheckEquipmentId == CheckEquipmentId && x.ApproveDate == null);
        }

        /// <summary>
        /// 根据设计变更审批编号获取一个设计变更审批信息
        /// </summary>
        /// <param name="CheckEquipmentCode">设计变更编号</param>
        /// <returns>一个设计变更审批实体</returns>
        public static Model.Check_CheckEquipmentApprove GetCheckEquipmentApproveByApproveId(string approveId)
        {
            return db.Check_CheckEquipmentApprove.FirstOrDefault(x => x.CheckEquipmentApproveId == approveId);
        }

        public static Model.Check_CheckEquipmentApprove GetComplie(string CheckEquipmentId)
        {
            return db.Check_CheckEquipmentApprove.FirstOrDefault(x => x.CheckEquipmentId == CheckEquipmentId && x.ApproveType == BLL.Const.CheckEquipment_Compile);
        }

        public static Model.Check_CheckEquipmentApprove GetApprove(string CheckEquipmentId)
        {
            return db.Check_CheckEquipmentApprove.FirstOrDefault(x => x.CheckEquipmentId == CheckEquipmentId && x.ApproveType == BLL.Const.CheckEquipment_Approve);
        }

        /// <summary>
        /// 根据设计变更发布Id获取所以对应设计变更审批信息
        /// </summary>
        /// <param name="CheckEquipmentCode">设计变更发布Id</param>
        /// <returns>设计变更审批集合</returns>
        public static List<Model.Check_CheckEquipmentApprove> GetCheckEquipmentApprovesByCheckEquipmentId(string CheckEquipmentId)
        {
            return (from x in db.Check_CheckEquipmentApprove where x.CheckEquipmentId == CheckEquipmentId select x).ToList();
        }

        /// <summary>
        /// 增加设计变更审批信息
        /// </summary>
        /// <param name="managerRuleApprove">设计变更审批实体</param>
        public static void AddCheckEquipmentApprove(Model.Check_CheckEquipmentApprove approve)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Check_CheckEquipmentApprove));
            Model.Check_CheckEquipmentApprove newApprove = new Model.Check_CheckEquipmentApprove();
            newApprove.CheckEquipmentApproveId = newKeyID;
            newApprove.CheckEquipmentId = approve.CheckEquipmentId;
            newApprove.ApproveMan = approve.ApproveMan;
            newApprove.ApproveDate = approve.ApproveDate;
            newApprove.ApproveIdea = approve.ApproveIdea;
            newApprove.IsAgree = approve.IsAgree;
            newApprove.ApproveType = approve.ApproveType;

            db.Check_CheckEquipmentApprove.InsertOnSubmit(newApprove);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改设计变更审批信息
        /// </summary>
        /// <param name="managerRuleApprove">设计变更审批实体</param>
        public static void UpdateCheckEquipmentApprove(Model.Check_CheckEquipmentApprove approve)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Check_CheckEquipmentApprove newApprove = db.Check_CheckEquipmentApprove.First(e => e.CheckEquipmentApproveId == approve.CheckEquipmentApproveId && e.ApproveDate == null);
            newApprove.CheckEquipmentId = approve.CheckEquipmentId;
            newApprove.ApproveMan = approve.ApproveMan;
            newApprove.ApproveDate = approve.ApproveDate;
            newApprove.ApproveIdea = approve.ApproveIdea;
            newApprove.IsAgree = approve.IsAgree;
            newApprove.ApproveType = approve.ApproveType;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据设计变更编号删除对应的所有设计变更审批信息
        /// </summary>
        /// <param name="CheckEquipmentCode">设计变更编号</param>
        public static void DeleteCheckEquipmentApprovesByCheckEquipmentId(string CheckEquipmentId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var q = (from x in db.Check_CheckEquipmentApprove where x.CheckEquipmentId == CheckEquipmentId select x).ToList();
            db.Check_CheckEquipmentApprove.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据用户主键获得设计变更审批的数量
        /// </summary>
        /// <param name="userId">角色</param>
        /// <returns></returns>
        public static int GetManagerRuleApproveCountByUserId(string userId)
        {
            var q = (from x in new Model.SGGLDB(Funs.ConnString).Check_CheckEquipmentApprove where x.ApproveMan == userId select x).ToList();
            return q.Count();
        }
        public static List<Model.Check_CheckEquipmentApprove> getListDataByEid(string CheckEquipmentId)
        {
            var q = from x in db.Check_CheckEquipmentApprove
                    where x.CheckEquipmentId == CheckEquipmentId && x.ApproveDate != null && x.ApproveType != "S"
                    orderby x.ApproveDate
                    select new
                    {
                        x.CheckEquipmentApproveId,
                        x.CheckEquipmentId,
                        ApproveMan = (from y in db.Sys_User where y.UserId == x.ApproveMan select y.UserName).First(),
                        x.ApproveDate,
                        x.IsAgree,
                        x.ApproveIdea,
                        x.ApproveType,
                    };
            var list = q.ToList();
            List<Model.Check_CheckEquipmentApprove> res = new List<Model.Check_CheckEquipmentApprove>();
            foreach (var item in list)
            {
                Model.Check_CheckEquipmentApprove x = new Model.Check_CheckEquipmentApprove();
                x.CheckEquipmentApproveId = item.CheckEquipmentApproveId;
                x.CheckEquipmentId = item.CheckEquipmentId;
                x.ApproveMan = item.ApproveMan;
                x.ApproveDate = item.ApproveDate;
                x.IsAgree = item.IsAgree;
                x.ApproveIdea = item.ApproveIdea;
                x.ApproveType = item.ApproveType;
                res.Add(x);
            }
            return res;
        }
    }
}
