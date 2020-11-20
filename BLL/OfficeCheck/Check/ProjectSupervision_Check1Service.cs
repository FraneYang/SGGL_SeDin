using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class ProjectSupervision_Check1Service
    {

        public static Model.ProjectSupervision_Check1 GetCheck1ByCheckItem(string checkItem,string checkNoticeId)
        {
            return Funs.DB.ProjectSupervision_Check1.FirstOrDefault(e => e.CheckItem == checkItem && e.CheckNoticeId == checkNoticeId);
        }

        public static List<Model.ProjectSupervision_Check1> GetCheckLists(string checkItem, string checkNoticeId)
        {
            return (from x in Funs.DB.ProjectSupervision_Check1 where x.CheckItem == checkItem && x.CheckNoticeId == checkNoticeId select x).ToList();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="check"></param>
        public static void AddCheck1(Model.ProjectSupervision_Check1 check)
        {
            Model.ProjectSupervision_Check1 newCheck = new Model.ProjectSupervision_Check1();
            newCheck.ID = check.ID;
            newCheck.SortIndex = check.SortIndex;
            newCheck.CheckItem = check.CheckItem;
            newCheck.CheckStandard = check.CheckStandard;
            newCheck.CheckMethod = check.CheckMethod;
            newCheck.CheckResult = check.CheckResult;
            newCheck.BaseScore = check.BaseScore;
            newCheck.DeletScore = check.DeletScore;
            newCheck.GetScore = check.GetScore;
            newCheck.Type = check.Type;
            newCheck.CheckNoticeId = check.CheckNoticeId;
            Funs.DB.ProjectSupervision_Check1.InsertOnSubmit(newCheck);
            Funs.DB.SubmitChanges();
        }

        /// <summary>
        /// 根据检查主表Id删除相关的检查项
        /// </summary>
        /// <param name="noticeId"></param>
        public static void DeleteCheckByNoticeId(string noticeId)
        {
            var q = (from x in Funs.DB.ProjectSupervision_Check1 where x.CheckNoticeId == noticeId select x).ToList();
            if (q!=null)
            {
                Funs.DB.ProjectSupervision_Check1.DeleteAllOnSubmit(q);
                Funs.DB.SubmitChanges();
            }
        }
    }
}
