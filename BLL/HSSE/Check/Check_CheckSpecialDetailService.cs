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
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据专项检查id获取所有相关明细信息
        /// </summary>
        /// <param name="CheckRectifyId"></param>
        /// <returns></returns>
        public static List<Model.Check_CheckSpecialDetail> GetCheckSpecialDetailByCheckSpecialId(string checkSpecialId)
        {
            return (from x in Funs.DB.Check_CheckSpecialDetail where x.CheckSpecialId == checkSpecialId select x).ToList();
        }

        /// <summary>
        /// 根据主键获取专项检查明细信息
        /// </summary>
        /// <param name="checkSpecialDetailId"></param>
        /// <returns></returns>
        public static Model.Check_CheckSpecialDetail GetCheckSpecialDetailByCheckSpecialDetailId(string checkSpecialDetailId)
        {
            return Funs.DB.Check_CheckSpecialDetail.FirstOrDefault(e => e.CheckSpecialDetailId == checkSpecialDetailId);
        }

        /// <summary>
        /// 增加专项检查明细信息
        /// </summary>
        /// <param name="checkSpecialDetail"></param>
        public static void AddCheckSpecialDetail(Model.Check_CheckSpecialDetail checkSpecialDetail)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_CheckSpecialDetail newCheckSpecialDetail = new Model.Check_CheckSpecialDetail
            {
                CheckSpecialDetailId = checkSpecialDetail.CheckSpecialDetailId,
                CheckSpecialId = checkSpecialDetail.CheckSpecialId,
                SortIndex = checkSpecialDetail.SortIndex,
                CheckItem = checkSpecialDetail.CheckItem,
                CheckItemType = checkSpecialDetail.CheckItemType,
                Unqualified = checkSpecialDetail.Unqualified,
                CheckArea = checkSpecialDetail.CheckArea,
                UnitId = checkSpecialDetail.UnitId,
                HandleStep = checkSpecialDetail.HandleStep,
                CompleteStatus = checkSpecialDetail.CompleteStatus,
                RectifyNoticeId = checkSpecialDetail.RectifyNoticeId,
                LimitedDate = checkSpecialDetail.LimitedDate,
                CompletedDate = checkSpecialDetail.CompletedDate,
                Suggestions = checkSpecialDetail.Suggestions,
                WorkArea = checkSpecialDetail.WorkArea,
                CheckContent = checkSpecialDetail.CheckContent,
                HiddenHazardType=checkSpecialDetail.HiddenHazardType,
            };
            db.Check_CheckSpecialDetail.InsertOnSubmit(newCheckSpecialDetail);
            db.SubmitChanges();

            //// 根据明细ID判断是否全部整改完成 并更新专项检查状态
            Check_CheckSpecialService.UpdateCheckSpecialStates(checkSpecialDetail.CheckSpecialId);
        }

        /// <summary>
        /// 修改专项检查明细信息
        /// </summary>
        /// <param name="CheckSpecialDetail"></param>
        public static void UpdateCheckSpecialDetail(Model.Check_CheckSpecialDetail CheckSpecialDetail)
        {
            Model.SGGLDB db = Funs.DB;
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
            Model.SGGLDB db = Funs.DB;
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
            Model.SGGLDB db = Funs.DB;
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
