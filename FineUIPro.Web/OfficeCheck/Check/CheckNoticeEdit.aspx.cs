using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.OfficeCheck.Check
{
    public partial class CheckNoticeEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 检查通知主键
        /// </summary>
        public string CheckNoticeId
        {
            get
            {
                return (string)ViewState["CheckNoticeId"];
            }
            set
            {
                ViewState["CheckNoticeId"] = value;
            }
        }
        #endregion

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetButtonPower();               

                BLL.ProjectService.InitAllProjectDropDownList(this.drpSubjectProject, true);
                BLL.UnitService.InitUnitDropDownList(this.drpUnit, this.CurrUser.LoginProjectId, true);

                this.CheckNoticeId = Request.Params["CheckNoticeId"];
                if (!string.IsNullOrEmpty(this.CheckNoticeId))
                {
                    var checkNotice = BLL.CheckNoticeService.GetCheckNoticeById(this.CheckNoticeId);
                    if (checkNotice != null)
                    {
                        this.txtCheckStartTime.Text = string.Format("{0:yyyy-MM-dd}", checkNotice.CheckStartTime);
                        this.txtCheckEndTime.Text = string.Format("{0:yyyy-MM-dd}", checkNotice.CheckEndTime);
                        if (!string.IsNullOrEmpty(checkNotice.SubjectProjectId))
                        {
                            this.drpSubjectProject.SelectedValue = checkNotice.SubjectProjectId;
                        }
                        this.txtSubjectUnitMan.Text = checkNotice.SubjectUnitMan;
                        this.txtSubjectUnitAdd.Text = checkNotice.SubjectUnitAdd;
                        this.txtSubjectUnitTel.Text = checkNotice.SubjectUnitTel;
                        //this.txtSubjectObject.Text = checkNotice.SubjectObject;
                        this.txtCheckTeamLeaderName.Text = checkNotice.CheckTeamLeaderName;
                        if (!string.IsNullOrEmpty(checkNotice.UnitId))
                        {
                            this.drpUnit.SelectedValue = checkNotice.UnitId;
                            this.drpUnit.Enabled = false;
                        }
                        this.drpSex.SelectedValue = checkNotice.SexName;
                        this.hdUserId.Text = checkNotice.CheckTeamLeader;
                    }
                }
                else
                {
                    this.txtCheckStartTime.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
                    this.txtCheckEndTime.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now.AddDays(3));
                }
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtCheckTeamLeaderName.Text))
            {
                ShowNotify("请先填写检查组长!", MessageBoxIcon.Warning);
                return;
            }
            Model.ProjectSupervision_CheckNotice newCheckNotice = new Model.ProjectSupervision_CheckNotice();            
            if (this.drpSubjectProject.SelectedValue != BLL.Const._Null && !string.IsNullOrEmpty(this.drpSubjectProject.SelectedValue))
            {
                newCheckNotice.SubjectProjectId = this.drpSubjectProject.SelectedValue;
            }
            newCheckNotice.SubjectUnitAdd = this.txtSubjectUnitAdd.Text.Trim();
            newCheckNotice.SubjectUnitMan = this.txtSubjectUnitMan.Text.Trim();
            newCheckNotice.SubjectUnitTel = this.txtSubjectUnitTel.Text.Trim();
            newCheckNotice.CheckStartTime = Funs.GetNewDateTime(this.txtCheckStartTime.Text).Value;
            newCheckNotice.CheckEndTime = Funs.GetNewDateTime(this.txtCheckEndTime.Text).Value;
            newCheckNotice.CompileMan = this.CurrUser.UserId;
            newCheckNotice.CompileDate = DateTime.Now;
            newCheckNotice.CheckTeamLeaderName = this.txtCheckTeamLeaderName.Text.Trim();
            if (this.drpSex.SelectedValue != BLL.Const._Null && !string.IsNullOrEmpty(this.drpSex.SelectedValue))
            {
                newCheckNotice.SexName = this.drpSex.SelectedValue;
            }
            if (this.drpUnit.SelectedValue != BLL.Const._Null && !string.IsNullOrEmpty(this.drpUnit.SelectedValue))
            {
                newCheckNotice.UnitId = this.drpUnit.SelectedValue;
            }
            if (!string.IsNullOrEmpty(this.hdUserId.Text))
            {
                newCheckNotice.CheckTeamLeader = this.hdUserId.Text;
            }

            if (string.IsNullOrEmpty(this.CheckNoticeId))
            {
                newCheckNotice.CheckNoticeId = SQLHelper.GetNewID(typeof(Model.ProjectSupervision_CheckNotice));
                BLL.CheckNoticeService.AddCheckNotice(newCheckNotice);
                BLL.LogService.AddSys_Log(this.CurrUser, newCheckNotice.CheckTeamLeaderName, newCheckNotice.CheckNoticeId, BLL.Const.CheckNoticeMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                newCheckNotice.CheckNoticeId = this.CheckNoticeId;
                BLL.CheckNoticeService.UpdateCheckNotice(newCheckNotice);
                BLL.LogService.AddSys_Log(this.CurrUser, newCheckNotice.CheckTeamLeaderName, newCheckNotice.CheckNoticeId, BLL.Const.CheckNoticeMenuId, BLL.Const.BtnAdd);
            }

            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        #region 受检单位下拉框事件
        /// <summary>
        ///  受检单位下拉框事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpSubjectProject_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var units = BLL.ProjectService.GetProjectByProjectId(this.drpSubjectProject.SelectedValue);
            if (units != null)
            {
                this.txtSubjectUnitAdd.Text = units.ProjectAddress;
            }
        }
        #endregion

        #region 按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId,this.CurrUser.UserId, BLL.Const.CheckNoticeMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        #region Text改变事件
        /// <summary>
        /// 检查组长事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtCheckTeamLeaderName_TextChanged(object sender, EventArgs e)
        {
            this.drpUnit.Enabled = true;
            var sysUser = BLL.UserService.GetUserByUserName(this.txtCheckTeamLeaderName.Text.Trim());
            if (sysUser != null)
            {
                if (!string.IsNullOrEmpty(sysUser.UnitId))
                {
                    this.drpUnit.SelectedValue = sysUser.UnitId;
                    this.drpUnit.Enabled = false;
                }
                this.hdUserId.Text = sysUser.UserId;
                this.drpSex.SelectedValue = sysUser.Sex;
            }
        }
        #endregion
    }
}