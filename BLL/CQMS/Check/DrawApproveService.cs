using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DrawApproveService
    {
        /// <summary>
        /// 增加图纸审批信息
        /// </summary>
        /// <param name="approve">图纸审批实体</param>
        public static void AddDrawApprove(Model.Check_DrawApprove approve)
        {
            Model.SGGLDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Check_DrawApprove));
            Model.Check_DrawApprove newApprove = new Model.Check_DrawApprove();
            newApprove.DrawApproveId = newKeyID;
            newApprove.DrawId = approve.DrawId;
            newApprove.ApproveMan = approve.ApproveMan;
            newApprove.ApproveDate = approve.ApproveDate;
            newApprove.ApproveIdea = approve.ApproveIdea;
            newApprove.IsAgree = approve.IsAgree;
            newApprove.ApproveType = approve.ApproveType;

            db.Check_DrawApprove.InsertOnSubmit(newApprove);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改图纸审批信息
        /// </summary>
        /// <param name="approve">图纸审批实体</param>
        public static void UpdateDrawApprove(Model.Check_DrawApprove approve)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_DrawApprove newApprove = db.Check_DrawApprove.First(e => e.DrawApproveId == approve.DrawApproveId && e.ApproveDate == null);
            newApprove.DrawId = approve.DrawId;
            newApprove.ApproveMan = approve.ApproveMan;
            newApprove.ApproveDate = approve.ApproveDate;
            newApprove.ApproveIdea = approve.ApproveIdea;
            newApprove.IsAgree = approve.IsAgree;
            newApprove.ApproveType = approve.ApproveType;

            db.SubmitChanges();
        }

        /// <summary>
        /// 修改图纸审批信息
        /// </summary>
        /// <param name="approve">图纸审批实体</param>
        public static void UpdateDrawApproveForApi(Model.Check_DrawApprove approve)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Check_DrawApprove newApprove = db.Check_DrawApprove.FirstOrDefault(e => e.DrawApproveId == approve.DrawApproveId);
                if (newApprove != null)
                {
                    if (!string.IsNullOrEmpty(approve.DrawId))
                        newApprove.DrawId = approve.DrawId;
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
        /// 根据图纸编号删除对应的所有图纸审批信息
        /// </summary>
        /// <param name="DrawCode">图纸编号</param>
        public static void DeleteDrawApprovesByDrawId(string DrawId)
        {
            Model.SGGLDB db = Funs.DB;
            var q = (from x in db.Check_DrawApprove where x.DrawId == DrawId select x).ToList();
            db.Check_DrawApprove.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }

        public static void See(string DrawId, string userId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                var item = db.Check_DrawApprove.FirstOrDefault(x => x.DrawId == DrawId && x.ApproveType == "S" && x.ApproveMan == userId && x.ApproveDate == null);
                if (item != null)
                {
                    item.ApproveDate = DateTime.Now;
                    db.SubmitChanges();
                }
            }
        }
    }
}
