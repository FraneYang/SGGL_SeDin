using BLL;
using System;

namespace FineUIPro.Web.HSSE.QualityAudit
{
    public partial class SubUnitQualityAuditEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 审查明细id
        /// </summary>
        public string AuditDetailId
        {
            get
            {
                return (string)ViewState["AuditDetailId"];
            }
            set
            {
                ViewState["AuditDetailId"] = value;
            }
        }

        /// <summary>
        /// 项目id
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
        ///  加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.InitDropDownList();
                this.AuditDetailId = Request.Params["AuditDetailId"];
                if (!string.IsNullOrEmpty(this.AuditDetailId))
                {
                    var auditDetail = BLL.SubUnitQualityAuditDetailService.GetSubUnitQualityAuditDetailById(this.AuditDetailId);
                    if (auditDetail != null)
                    {
                        this.txtAuditContent.Text = auditDetail.AuditContent;
                        if (!string.IsNullOrEmpty(auditDetail.AuditMan))
                        {
                            this.drpAuditMan.SelectedValue = auditDetail.AuditMan;
                        }
                        if (auditDetail.AuditDate != null)
                        {
                            this.txtAuditDate.Text = string.Format("{0:yyyy-MM-dd}", auditDetail.AuditDate);
                        }
                        this.txtAuditResult.Text = auditDetail.AuditResult;
                    }
                }
                else
                {
                    this.drpAuditMan.SelectedValue = this.CurrUser.UserId;
                    this.txtAuditDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }
            }
        }

        /// <summary>
        /// 初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            ///审查人下拉框
            BLL.UserService.InitUserDropDownList(this.drpAuditMan, this.ProjectId, true);
        }

        /// <summary>
        /// 保存按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.drpAuditMan.SelectedValue == BLL.Const._Null)
            {
                ShowNotify("请选择审查人！", MessageBoxIcon.Warning);
                return;
            }
            SaveData();
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        private void SaveData()
        {
            Model.QualityAudit_SubUnitQualityAuditDetail detail = new Model.QualityAudit_SubUnitQualityAuditDetail
            {
                ProjectId = this.ProjectId,
                UnitId = Request.Params["UnitId"],
                AuditContent = this.txtAuditContent.Text.Trim(),
                AuditMan = this.drpAuditMan.SelectedValue,
                AuditDate = Funs.GetNewDateTimeOrNow(this.txtAuditDate.Text.Trim()),
                AuditResult = this.txtAuditResult.Text.Trim()
            };
            if (!string.IsNullOrEmpty(AuditDetailId))
            {
                detail.AuditDetailId = this.AuditDetailId;
                BLL.SubUnitQualityAuditDetailService.UpdateSubUnitQualityAuditDetail(detail);
                BLL.LogService.AddSys_Log(this.CurrUser, null, AuditDetailId,BLL.Const.SubUnitQualityMenuId,BLL.Const.BtnModify );
            }
            else
            {
                detail.AuditDetailId = SQLHelper.GetNewID(typeof(Model.QualityAudit_SubUnitQualityAuditDetail));
                this.AuditDetailId = detail.AuditDetailId;
                BLL.SubUnitQualityAuditDetailService.AddSubUnitQualityAuditDetail(detail);
                BLL.LogService.AddSys_Log(this.CurrUser, null, AuditDetailId, BLL.Const.SubUnitQualityMenuId, BLL.Const.BtnAdd);
            }
        }

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.AuditDetailId))
            {
                if (this.drpAuditMan.SelectedValue == BLL.Const._Null)
                {
                    ShowNotify("请选择审查人！", MessageBoxIcon.Warning);
                    return;
                }
                SaveData();
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SubUnitQualityAuditAttachUrl&menuId={1}", this.AuditDetailId, BLL.Const.SubUnitQualityMenuId)));
        }
        #endregion
    }
}