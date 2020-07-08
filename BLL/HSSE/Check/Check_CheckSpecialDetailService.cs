using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 专项检查明细表
    /// </summary>
    public class Check_CheckSpecialDetailService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据专项检查id获取所有相关明细信息
        /// </summary>
        /// <param name="CheckRectifyId"></param>
        /// <returns></returns>
        public static List<Model.Check_CheckSpecialDetail> GetCheckSpecialDetailByCheckSpecialId(string checkSpecialId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Check_CheckSpecialDetail where x.CheckSpecialId == checkSpecialId select x).ToList();
        }

        /// <summary>
        /// 根据主键获取专项检查明细信息
        /// </summary>
        /// <param name="checkSpecialDetailId"></param>
        /// <returns></returns>
        public static Model.Check_CheckSpecialDetail GetCheckSpecialDetailByCheckSpecialDetailId(string checkSpecialDetailId)
        {
            return new Model.SGGLDB(Funs.ConnString).Check_CheckSpecialDetail.FirstOrDefault(e => e.CheckSpecialDetailId == checkSpecialDetailId);
        }

        /// <summary>
        /// 增加专项检查明细信息
        /// </summary>
        /// <param name="CheckSpecialDetail"></param>
        public static void AddCheckSpecialDetail(Model.Check_CheckSpecialDetail CheckSpecialDetail)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Check_CheckSpecialDetail newCheckSpecialDetail = new Model.Check_CheckSpecialDetail
            {
                CheckSpecialDetailId = CheckSpecialDetail.CheckSpecialDetailId,
                CheckSpecialId = CheckSpecialDetail.CheckSpecialId,
                SortIndex = CheckSpecialDetail.SortIndex,
                CheckItem = CheckSpecialDetail.CheckItem,
                CheckItemType = CheckSpecialDetail.CheckItemType,
                Unqualified = CheckSpecialDetail.Unqualified,
                CheckArea = CheckSpecialDetail.CheckArea,
                UnitId = CheckSpecialDetail.UnitId,
                HandleStep = CheckSpecialDetail.HandleStep,
                CompleteStatus = CheckSpecialDetail.CompleteStatus,
                RectifyNoticeId = CheckSpecialDetail.RectifyNoticeId,
                LimitedDate = CheckSpecialDetail.LimitedDate,
                CompletedDate = CheckSpecialDetail.CompletedDate,
                Suggestions = CheckSpecialDetail.Suggestions,
                WorkArea = CheckSpecialDetail.WorkArea,
                CheckContent = CheckSpecialDetail.CheckContent
            };
            db.Check_CheckSpecialDetail.InsertOnSubmit(newCheckSpecialDetail);
            db.SubmitChanges();

        }

        /// <summary>
        /// 修改专项检查明细信息
        /// </summary>
        /// <param name="CheckSpecialDetail"></param>
        public static void UpdateCheckSpecialDetail(Model.Check_CheckSpecialDetail CheckSpecialDetail)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var newCheckSpecialDetail = db.Check_CheckSpecialDetail.FirstOrDefault(x => x.CheckSpecialDetailId == CheckSpecialDetail.CheckSpecialDetailId);
            if (newCheckSpecialDetail != null)
            {
                newCheckSpecialDetail.Unqualified = CheckSpecialDetail.Unqualified;
                newCheckSpecialDetail.CheckArea = CheckSpecialDetail.CheckArea;
                newCheckSpecialDetail.UnitId = CheckSpecialDetail.UnitId;
                newCheckSpecialDetail.HandleStep = CheckSpecialDetail.HandleStep;
                newCheckSpecialDetail.CompleteStatus = CheckSpecialDetail.CompleteStatus;
                newCheckSpecialDetail.RectifyNoticeId = CheckSpecialDetail.RectifyNoticeId;
                newCheckSpecialDetail.LimitedDate = CheckSpecialDetail.LimitedDate;
                newCheckSpecialDetail.CompletedDate = CheckSpecialDetail.CompletedDate;
                newCheckSpecialDetail.Suggestions = CheckSpecialDetail.Suggestions;
                newCheckSpecialDetail.WorkArea = CheckSpecialDetail.WorkArea;
                newCheckSpecialDetail.CheckContent = CheckSpecialDetail.CheckContent;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据专项检查ID删除所有专项检查明细信息
        /// </summary>
        /// <param name="checkSpecialId"></param>
        public static void DeleteCheckSpecialDetails(string checkSpecialId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var q = (from x in db.Check_CheckSpecialDetail where x.CheckSpecialId == checkSpecialId select x).ToList();
            if (q != null)
            {
                foreach (var item in q)
                {
                    ////删除附件表
                    BLL.CommonService.DeleteAttachFileById(item.CheckSpecialDetailId);
                }
                db.Check_CheckSpecialDetail.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据专项检查ID删除一条专项检查明细信息
        /// </summary>
        /// <param name="checkSpecialDetailId"></param>
        public static void DeleteCheckSpecialDetailById(string checkSpecialDetailId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var q = (from x in db.Check_CheckSpecialDetail where x.CheckSpecialDetailId == checkSpecialDetailId select x).FirstOrDefault();
            if (q != null)
            {
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(q.CheckSpecialDetailId);
                db.Check_CheckSpecialDetail.DeleteOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
