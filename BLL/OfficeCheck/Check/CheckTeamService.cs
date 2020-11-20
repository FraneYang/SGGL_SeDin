using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// 检查组长
    /// </summary>
    public static class CheckTeamService
    {
        /// <summary>
        /// 根据主键获取检查组长
        /// </summary>
        /// <param name="CheckTeamId"></param>
        /// <returns></returns>
        public static Model.ProjectSupervision_CheckTeam GetCheckTeamByCheckTeamId(string CheckTeamId)
        {
            return Funs.DB.ProjectSupervision_CheckTeam.FirstOrDefault(e => e.CheckTeamId == CheckTeamId);
        }

        /// <summary>
        /// 根据检查通知ID获取所有检查组长
        /// </summary>
        /// <param name="checkNoticeId"></param>
        /// <returns></returns>
        public static List<Model.ProjectSupervision_CheckTeam> GetCheckTeamListByCheckNoticeId(string checkNoticeId)
        {
            return (from x in Funs.DB.ProjectSupervision_CheckTeam where x.CheckNoticeId == checkNoticeId select x).ToList();
        }


        /// <summary>
        /// 添加检查组长信息
        /// </summary>
        /// <param name="CheckTeam"></param>
        public static void AddCheckTeam(Model.ProjectSupervision_CheckTeam CheckTeam)
        {
            Model.ProjectSupervision_CheckTeam newCheckTeam = new Model.ProjectSupervision_CheckTeam
            {
                CheckTeamId = CheckTeam.CheckTeamId,
                CheckNoticeId = CheckTeam.CheckNoticeId,
                UserId = CheckTeam.UserId,
                SortIndex = CheckTeam.SortIndex,
                PostName = CheckTeam.PostName,
                WorkTitle = CheckTeam.WorkTitle,
                CheckPostName = CheckTeam.CheckPostName,
                CheckDate = CheckTeam.CheckDate,
                UserName = CheckTeam.UserName,
                UnitId = CheckTeam.UnitId,
                SexName = CheckTeam.SexName,
            };
            Funs.DB.ProjectSupervision_CheckTeam.InsertOnSubmit(newCheckTeam);
            Funs.DB.SubmitChanges();
        }

        /// <summary>
        /// 修改检查组长信息
        /// </summary>
        /// <param name="CheckTeam"></param>
        public static void UpdateCheckTeam(Model.ProjectSupervision_CheckTeam CheckTeam)
        {
            Model.ProjectSupervision_CheckTeam newCheckTeam = Funs.DB.ProjectSupervision_CheckTeam.FirstOrDefault(e => e.CheckTeamId == CheckTeam.CheckTeamId);
            if (newCheckTeam != null)
            {
                newCheckTeam.UserId = CheckTeam.UserId;
                newCheckTeam.SortIndex = CheckTeam.SortIndex;
                newCheckTeam.PostName = CheckTeam.PostName;
                newCheckTeam.WorkTitle = CheckTeam.WorkTitle;
                newCheckTeam.CheckPostName = CheckTeam.CheckPostName;
                newCheckTeam.CheckDate = CheckTeam.CheckDate;
                newCheckTeam.UserName = CheckTeam.UserName;
                newCheckTeam.UnitId = CheckTeam.UnitId;
                newCheckTeam.SexName = CheckTeam.SexName;
                Funs.DB.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除检查组长信息
        /// </summary>
        /// <param name="checkTeamId"></param>
        public static void DeleteCheckTeamByCheckTeamId(string checkTeamId)
        {
            Model.ProjectSupervision_CheckTeam CheckTeam = Funs.DB.ProjectSupervision_CheckTeam.FirstOrDefault(e => e.CheckTeamId == checkTeamId);
            if (CheckTeam != null)
            {
                Funs.DB.ProjectSupervision_CheckTeam.DeleteOnSubmit(CheckTeam);
                Funs.DB.SubmitChanges();
            }
        }

        /// <summary>
        /// 排序值加1
        /// </summary>
        /// <returns></returns>
        public static int ReturCheckTeamSortIndex(string checkNoticeId)
        {
            int SortIndex = 1;
            var maxSortIndex = Funs.DB.ProjectSupervision_CheckTeam.Where(x => x.CheckNoticeId == checkNoticeId).Max(x => x.SortIndex);
            if (maxSortIndex.HasValue)
            {
                SortIndex = maxSortIndex.Value + 1;
            }
            return SortIndex;
        }
    }
}
