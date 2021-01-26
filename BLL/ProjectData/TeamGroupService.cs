using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 班组
    /// </summary>
    public static class TeamGroupService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取班组信息
        /// </summary>
        /// <param name="teamGroupId"></param>
        /// <returns></returns>
        public static Model.ProjectData_TeamGroup GetTeamGroupById(string teamGroupId)
        {
            return Funs.DB.ProjectData_TeamGroup.FirstOrDefault(e => e.TeamGroupId == teamGroupId);
        }

        /// <summary>
        /// 添加班组信息
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void AddTeamGroup(Model.ProjectData_TeamGroup teamGroup)
        {
            Model.SGGLDB db = Funs.DB;
            Model.ProjectData_TeamGroup newTeamGroup = new Model.ProjectData_TeamGroup
            {
                TeamGroupId = teamGroup.TeamGroupId,
                ProjectId = teamGroup.ProjectId,
                UnitId = teamGroup.UnitId,
                TeamGroupCode = teamGroup.TeamGroupCode,
                TeamGroupName = teamGroup.TeamGroupName,
                Remark = teamGroup.Remark,
                TeamTypeId = teamGroup.TeamTypeId,
                ThirdTeamCode = teamGroup.ThirdTeamCode,
                EntryTime = teamGroup.EntryTime,
                ExitTime = teamGroup.ExitTime,
                GroupLeaderId = teamGroup.GroupLeaderId,
                RealNamePushTime=teamGroup.RealNamePushTime,
            };
            db.ProjectData_TeamGroup.InsertOnSubmit(newTeamGroup);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改班组信息
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void UpdateTeamGroup(Model.ProjectData_TeamGroup teamGroup)
        {
            Model.SGGLDB db = Funs.DB;
            Model.ProjectData_TeamGroup newTeamGroup = db.ProjectData_TeamGroup.FirstOrDefault(e => e.TeamGroupId == teamGroup.TeamGroupId);
            if (newTeamGroup != null)
            {
                newTeamGroup.ProjectId = teamGroup.ProjectId;
                newTeamGroup.UnitId = teamGroup.UnitId;
                newTeamGroup.TeamGroupCode = teamGroup.TeamGroupCode;
                newTeamGroup.TeamGroupName = teamGroup.TeamGroupName;
                newTeamGroup.GroupLeaderId = teamGroup.GroupLeaderId;
                newTeamGroup.TeamTypeId = teamGroup.TeamTypeId;
                newTeamGroup.ThirdTeamCode = teamGroup.ThirdTeamCode;
                newTeamGroup.EntryTime = teamGroup.EntryTime;
                newTeamGroup.ExitTime = teamGroup.ExitTime;
                newTeamGroup.Remark = teamGroup.Remark;
                newTeamGroup.RealNamePushTime = null;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除班组信息
        /// </summary>
        /// <param name="teamGroupId"></param>
        public static void DeleteTeamGroupById(string teamGroupId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.ProjectData_TeamGroup teamGroup = db.ProjectData_TeamGroup.FirstOrDefault(e => e.TeamGroupId == teamGroupId);
            if (teamGroup != null)
            {
                db.ProjectData_TeamGroup.DeleteOnSubmit(teamGroup);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取班组人数
        /// </summary>
        /// <param name="teamGroupId"></param>
        /// <returns></returns>
        public static int getTeamGroupPersonNum(string teamGroupId)
        {
            Model.SGGLDB db = Funs.DB;
            return (from x in Funs.DB.SitePerson_Person
                    where x.TeamGroupId == teamGroupId && x.IsUsed == true && (!x.OutTime.HasValue || x.OutTime > DateTime.Now)
                    select x).Count();
        }

        /// <summary>
        /// 根据项目Id获取班组下拉选择项
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.ProjectData_TeamGroup> GetTeamGroupList(string projectId)
        {
            return (from x in Funs.DB.ProjectData_TeamGroup where x.ProjectId == projectId orderby x.TeamGroupCode select x).ToList();
        }

        /// <summary>
        /// 根据项目ID、单位ID获取班组下拉选择项
        /// </summary>
        /// <param name="p"></param>
        /// <param name="p_2"></param>
        /// <returns></returns>
        public static object GetTeamGroupListByUnitId(string projectId, string unitId)
        {
            return (from x in Funs.DB.ProjectData_TeamGroup where x.ProjectId == projectId && x.UnitId == unitId orderby x.TeamGroupCode select x).ToList();
        }

        #region 表下拉框
        /// <summary>
        ///  表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitTeamGroupProjectUnitDropDownList(FineUIPro.DropDownList dropName, string projectId, string unitId, bool isShowPlease)
        {
            dropName.DataValueField = "TeamGroupId";
            dropName.DataTextField = "TeamGroupName";
            dropName.DataSource = GetTeamGroupListByUnitId(projectId, unitId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion

        /// <summary>
        /// 获取班组名称
        /// </summary>
        /// <param name="UnitId"></param>
        /// <returns></returns>
        public static string GetTeamGroupNameByTeamGroupId(string TeamGroupId)
        {
            string name = string.Empty;
            var TeamGroup = Funs.DB.ProjectData_TeamGroup.FirstOrDefault(x => x.TeamGroupId == TeamGroupId);
            if (TeamGroup != null)
            {
                name = TeamGroup.TeamGroupName;
            }
            return name;
        }
    }
}
