using System;

namespace FineUIPro.Web.HSSE.QualityAudit
{
    public partial class EquipmentQualityAuditView : PageBase
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
                this.AuditDetailId = Request.Params["AuditDetailId"];
                if (!string.IsNullOrEmpty(this.AuditDetailId))
                {
                    var auditDetail = BLL.EquipmentQualityAuditDetailService.GetEquipmentQualityAuditDetailById(this.AuditDetailId);
                    if (auditDetail != null)
                    {
                        this.txtAuditContent.Text = auditDetail.AuditContent;
                        Model.Sys_User user = BLL.UserService.GetUserByUserId(auditDetail.AuditMan);
                        if (user != null)
                        {
                            this.txtAuditMan.Text = user.UserName;
                        }
                        if (auditDetail.AuditDate != null)
                        {
                            this.txtAuditDate.Text = string.Format("{0:yyyy-MM-dd}", auditDetail.AuditDate);
                        }
                        this.txtAuditResult.Text = auditDetail.AuditResult;
                    }
                }
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
            if (!string.IsNullOrEmpty(this.AuditDetailId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/EquipmentQualityAuditAttachUrl&menuId={1}&type=-1", this.AuditDetailId, BLL.Const.EquipmentQualityMenuId)));
            }

        }
        #endregion
    }
}