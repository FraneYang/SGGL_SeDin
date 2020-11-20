using BLL;
using System;

namespace FineUIPro.Web.OfficeCheck.Check
{
    public partial class CheckTeamEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 监督检查主键
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
        /// <summary>
        /// 监督检查工作组主键
        /// </summary>
        public string CheckTeamId
        {
            get
            {
                return (string)ViewState["CheckTeamId"];
            }
            set
            {
                ViewState["CheckTeamId"] = value;
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.CheckNoticeId = Request.Params["CheckNoticeId"];
                this.CheckTeamId = Request.Params["CheckTeamId"];

                BLL.UnitService.InitUnitDropDownList(this.drpUnit, this.CurrUser.LoginProjectId, true);

                if (!string.IsNullOrEmpty(this.CheckTeamId))
                {
                    var checkTeam = BLL.CheckTeamService.GetCheckTeamByCheckTeamId(this.CheckTeamId);
                    if (checkTeam != null)
                    {
                        this.CheckNoticeId = checkTeam.CheckNoticeId;
                        if (checkTeam.SortIndex.HasValue)
                        {
                            this.txtSortIndex.Text = checkTeam.SortIndex.ToString();
                        }
                        this.txtPostName.Text = checkTeam.PostName;
                        this.txtWorkTitle.Text = checkTeam.WorkTitle;
                        this.txtCheckPostName.Text = checkTeam.CheckPostName;
                        this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", checkTeam.CheckDate);
                        this.txtUserName.Text = checkTeam.UserName;
                        if (!string.IsNullOrEmpty(checkTeam.UserId))
                        {
                            this.hdUserId.Text = checkTeam.UserId;
                        }
                        if (!string.IsNullOrEmpty(checkTeam.UnitId))
                        {
                            this.drpUnit.SelectedValue = checkTeam.UnitId;
                            this.drpUnit.Enabled = false;
                        }
                        this.drpSex.SelectedValue = checkTeam.SexName;
                    }
                }
                else
                {
                    this.txtCheckPostName.Text = "组员";
                    this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
                    this.txtSortIndex.Text = BLL.CheckTeamService.ReturCheckTeamSortIndex(this.CheckNoticeId).ToString();
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
            if (string.IsNullOrEmpty(this.CheckNoticeId))
            {
                Alert.ShowInTop("检查异常，重新点击维护。", MessageBoxIcon.Warning);
            }
            if (string.IsNullOrEmpty(this.txtUserName.Text))
            {
                Alert.ShowInTop("请先填写检查组成员姓名!", MessageBoxIcon.Warning);
                return;
            }
            Model.ProjectSupervision_CheckTeam newCheckTeam = new Model.ProjectSupervision_CheckTeam
            {
                CheckNoticeId = this.CheckNoticeId,
                SortIndex = Funs.GetNewInt(this.txtSortIndex.Text)
            };
            newCheckTeam.PostName = this.txtPostName.Text.Trim();
            newCheckTeam.WorkTitle = this.txtWorkTitle.Text.Trim();
            newCheckTeam.CheckPostName = this.txtCheckPostName.Text.Trim();
            newCheckTeam.CheckDate = Funs.GetNewDateTime(this.txtCheckDate.Text);

            if (this.drpSex.SelectedValue != BLL.Const._Null && !string.IsNullOrEmpty(this.drpSex.SelectedValue))
            {
                newCheckTeam.SexName = this.drpSex.SelectedValue;
            }
            if (this.drpUnit.SelectedValue != BLL.Const._Null && !string.IsNullOrEmpty(this.drpUnit.SelectedValue))
            {
                newCheckTeam.UnitId = this.drpUnit.SelectedValue;
            }
            if (!string.IsNullOrEmpty(this.hdUserId.Text))
            {
                newCheckTeam.UserId = this.hdUserId.Text;
            }
            newCheckTeam.UserName = this.txtUserName.Text.Trim();
            if (string.IsNullOrEmpty(this.CheckTeamId))
            {
                newCheckTeam.CheckTeamId = SQLHelper.GetNewID(typeof(Model.ProjectSupervision_CheckTeam));
                BLL.CheckTeamService.AddCheckTeam(newCheckTeam);
            }
            else
            {
                newCheckTeam.CheckTeamId = this.CheckTeamId;
                BLL.CheckTeamService.UpdateCheckTeam(newCheckTeam);
            }
            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        #region Text改变事件
        /// <summary>
        /// 根据组成员获取信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtUserName_TextChanged(object sender, EventArgs e)
        {
            this.drpUnit.Enabled = true;
            var sysUser = BLL.UserService.GetUserByUserName(this.txtUserName.Text.Trim());
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