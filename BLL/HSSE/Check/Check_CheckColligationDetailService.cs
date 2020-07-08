using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 综合检查明细表
    /// </summary>
    public class Check_CheckColligationDetailService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据综合检查id获取所有相关明细信息
        /// </summary>
        /// <param name="CheckRectifyId"></param>
        /// <returns></returns>
        public static List<Model.Check_CheckColligationDetail> GetCheckColligationDetailByCheckColligationId(string checkColligationId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Check_CheckColligationDetail where x.CheckColligationId == checkColligationId select x).ToList();
        }

        /// <summary>
        /// 根据主键获取综合检查明细信息
        /// </summary>
        /// <param name="checkColligationDetailId"></param>
        /// <returns></returns>
        public static Model.Check_CheckColligationDetail GetCheckColligationDetailByCheckColligationDetailId(string checkColligationDetailId)
        {
            return new Model.SGGLDB(Funs.ConnString).Check_CheckColligationDetail.FirstOrDefault(e => e.CheckColligationDetailId == checkColligationDetailId);
        }

        /// <summary>
        /// 增加综合检查明细信息
        /// </summary>
        /// <param name="CheckColligationDetail"></param>
        public static void AddCheckColligationDetail(Model.Check_CheckColligationDetail CheckColligationDetail)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Check_CheckColligationDetail newCheckColligationDetail = new Model.Check_CheckColligationDetail
            {
                CheckColligationDetailId = CheckColligationDetail.CheckColligationDetailId,
                CheckColligationId = CheckColligationDetail.CheckColligationId,
                CheckItem = CheckColligationDetail.CheckItem,
                CheckItemType = CheckColligationDetail.CheckItemType,
                Unqualified = CheckColligationDetail.Unqualified,
                CheckArea = CheckColligationDetail.CheckArea,
                UnitId = CheckColligationDetail.UnitId,
                HandleStep = CheckColligationDetail.HandleStep,
                CompleteStatus = CheckColligationDetail.CompleteStatus,
                RectifyNoticeId = CheckColligationDetail.RectifyNoticeId,
                LimitedDate = CheckColligationDetail.LimitedDate,
                CompletedDate = CheckColligationDetail.CompletedDate,
                Suggestions = CheckColligationDetail.Suggestions,
                WorkArea = CheckColligationDetail.WorkArea,
                CheckContent = CheckColligationDetail.CheckContent,
                HiddenDangerType = CheckColligationDetail.HiddenDangerType,
                HiddenDangerLevel = CheckColligationDetail.HiddenDangerLevel,
                PersonId = CheckColligationDetail.PersonId
            };
            db.Check_CheckColligationDetail.InsertOnSubmit(newCheckColligationDetail);
            db.SubmitChanges();

        }

        /// <summary>
        /// 修改综合检查明细信息
        /// </summary>
        /// <param name="CheckColligationDetail"></param>
        public static void UpdateCheckColligationDetail(Model.Check_CheckColligationDetail CheckColligationDetail)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var newCheckColligationDetail = db.Check_CheckColligationDetail.FirstOrDefault(x => x.CheckColligationDetailId == CheckColligationDetail.CheckColligationDetailId);
            if (newCheckColligationDetail != null)
            {
                newCheckColligationDetail.Unqualified = CheckColligationDetail.Unqualified;
                newCheckColligationDetail.CheckArea = CheckColligationDetail.CheckArea;
                newCheckColligationDetail.UnitId = CheckColligationDetail.UnitId;
                newCheckColligationDetail.HandleStep = CheckColligationDetail.HandleStep;
                newCheckColligationDetail.CompleteStatus = CheckColligationDetail.CompleteStatus;
                newCheckColligationDetail.RectifyNoticeId = CheckColligationDetail.RectifyNoticeId;
                newCheckColligationDetail.LimitedDate = CheckColligationDetail.LimitedDate;
                newCheckColligationDetail.CompletedDate = CheckColligationDetail.CompletedDate;
                newCheckColligationDetail.Suggestions = CheckColligationDetail.Suggestions;
                newCheckColligationDetail.WorkArea = CheckColligationDetail.WorkArea;
                newCheckColligationDetail.CheckContent = CheckColligationDetail.CheckContent;
                newCheckColligationDetail.HiddenDangerType = CheckColligationDetail.HiddenDangerType;
                newCheckColligationDetail.HiddenDangerLevel = CheckColligationDetail.HiddenDangerLevel;
                newCheckColligationDetail.PersonId = CheckColligationDetail.PersonId;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据综合检查ID删除所有综合检查明细信息
        /// </summary>
        /// <param name="checkColligationId"></param>
        public static void DeleteCheckColligationDetails(string checkColligationId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var q = (from x in db.Check_CheckColligationDetail where x.CheckColligationId == checkColligationId select x).ToList();
            if (q != null)
            {
                foreach (var item in q)
                {
                    ////删除附件表
                    BLL.CommonService.DeleteAttachFileById(item.CheckColligationDetailId);
                }
                db.Check_CheckColligationDetail.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据综合检查ID删除一条综合检查明细信息
        /// </summary>
        /// <param name="checkColligationDetailId"></param>
        public static void DeleteCheckColligationDetailById(string checkColligationDetailId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var q = db.Check_CheckColligationDetail.FirstOrDefault(x => x.CheckColligationDetailId == checkColligationDetailId);
            if (q != null)
            {
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(q.CheckColligationDetailId);               
                db.Check_CheckColligationDetail.DeleteOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
