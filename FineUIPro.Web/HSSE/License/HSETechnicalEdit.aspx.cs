using BLL;
using System;

namespace FineUIPro.Web.HSSE.License
{
    public partial class HSETechnicalEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string HSETechnicalId
        {
            get
            {
                return (string)ViewState["HSETechnicalId"];
            }
            set
            {
                ViewState["HSETechnicalId"] = value;
            }
        }
        /// <summary>
        /// 项目主键
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.InitDropDownList();
                Funs.FineUIPleaseSelect(this.drpTeamGroupId);
                this.HSETechnicalId = Request.Params["HSETechnicalId"];
                if (!string.IsNullOrEmpty(this.HSETechnicalId))
                {
                    Model.License_HSETechnical hseTechnical = BLL.HSETechnicalService.GetHSETechnicalById(this.HSETechnicalId);
                    if (hseTechnical != null)
                    {
                        this.ProjectId = hseTechnical.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtHSETechnicalCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.HSETechnicalId);
                        this.txtHSETechnicalDate.Text = string.Format("{0:yyyy-MM-dd}", hseTechnical.HSETechnicalDate);
                        if (!string.IsNullOrEmpty(hseTechnical.UnitId))
                        {
                            this.drpUnitId.SelectedValue = hseTechnical.UnitId;
                            this.drpTeamGroupId.DataValueField = "TeamGroupId";
                            this.drpTeamGroupId.DataTextField = "TeamGroupName";
                            this.drpTeamGroupId.DataSource = BLL.TeamGroupService.GetTeamGroupListByUnitId(this.ProjectId, hseTechnical.UnitId);
                            this.drpTeamGroupId.DataBind();
                            Funs.FineUIPleaseSelect(this.drpTeamGroupId);
                        }
                        if (!string.IsNullOrEmpty(hseTechnical.TeamGroupId))
                        {
                            this.drpTeamGroupId.SelectedValue = hseTechnical.TeamGroupId;
                        }
                        this.txtWorkContents.Text = hseTechnical.WorkContents;
                        this.txtAddress.Text = hseTechnical.Address;
                    }
                }
                else
                {
                    this.txtHSETechnicalDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    ////自动生成编码
                    this.txtHSETechnicalCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectHSETechnicalMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.drpUnitId.SelectedValue=this.CurrUser.UnitId;
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectHSETechnicalMenuId;
                this.ctlAuditFlow.DataId = this.HSETechnicalId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            BLL.UnitService.InitUnitDropDownList(this.drpUnitId, this.ProjectId, true);
        }

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.ctlAuditFlow.NextStep == BLL.Const.State_1 && this.ctlAuditFlow.NextPerson == BLL.Const._Null)
            {
                ShowNotify("请选择下一步办理人！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }


        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.License_HSETechnical hSETechnical = new Model.License_HSETechnical
            {
                ProjectId = this.ProjectId,
                HSETechnicalCode = this.txtHSETechnicalCode.Text.Trim(),
                HSETechnicalDate = Funs.GetNewDateTime(this.txtHSETechnicalDate.Text.Trim())
            };
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                hSETechnical.UnitId = this.drpUnitId.SelectedValue;
            }
            if (this.drpTeamGroupId.SelectedValue != BLL.Const._Null)
            {
                hSETechnical.TeamGroupId = this.drpTeamGroupId.SelectedValue;
            }
            hSETechnical.WorkContents = this.txtWorkContents.Text.Trim();
            hSETechnical.Address = this.txtAddress.Text.Trim();
            hSETechnical.CompileMan = this.CurrUser.UserId;
            hSETechnical.CompileDate = DateTime.Now;
            hSETechnical.States = BLL.Const.State_0;
            if (type==BLL.Const.BtnSubmit)
            {
                hSETechnical.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.HSETechnicalId))
            {
                hSETechnical.HSETechnicalId = this.HSETechnicalId;
                BLL.HSETechnicalService.UpdateHSETechnical(hSETechnical);
                BLL.LogService.AddSys_Log(this.CurrUser, hSETechnical.HSETechnicalCode, hSETechnical.HSETechnicalId, BLL.Const.ProjectHSETechnicalMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.HSETechnicalId = SQLHelper.GetNewID(typeof(Model.License_HSETechnical));
                hSETechnical.HSETechnicalId = this.HSETechnicalId;
                BLL.HSETechnicalService.AddHSETechnical(hSETechnical);
                BLL.LogService.AddSys_Log(this.CurrUser, hSETechnical.HSETechnicalCode, hSETechnical.HSETechnicalId, BLL.Const.ProjectHSETechnicalMenuId, BLL.Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectHSETechnicalMenuId, this.HSETechnicalId, (type == BLL.Const.BtnSubmit ? true : false), hSETechnical.HSETechnicalCode, "../License/HSETechnicalView.aspx?HSETechnicalId={0}");
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.HSETechnicalId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/HSETechnicalAttachUrl&menuId={1}", this.HSETechnicalId, BLL.Const.ProjectHSETechnicalMenuId)));
        }
        #endregion

        /// <summary>
        /// 单位下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpUnitId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                this.drpTeamGroupId.Items.Clear();
                TeamGroupService.InitTeamGroupProjectUnitDropDownList(this.drpTeamGroupId, this.ProjectId, this.drpUnitId.SelectedValue, true);
                this.drpTeamGroupId.SelectedIndex = 0;
            }
            else
            {
                this.drpTeamGroupId.Items.Clear();
                Funs.FineUIPleaseSelect(this.drpTeamGroupId);
                this.drpTeamGroupId.SelectedIndex = 0;
            }
        }
    }
}