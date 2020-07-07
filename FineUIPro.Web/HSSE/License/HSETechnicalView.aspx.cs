using System;

namespace FineUIPro.Web.HSSE.License
{
    public partial class HSETechnicalView : PageBase
    {
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
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnClose.OnClientClick = ActiveWindow.GetHideReference();               
                this.HSETechnicalId = Request.Params["HSETechnicalId"];
                if (!string.IsNullOrEmpty(this.HSETechnicalId))
                {
                    Model.License_HSETechnical hseTechnical = BLL.HSETechnicalService.GetHSETechnicalById(this.HSETechnicalId);
                    if (hseTechnical != null)
                    {
                        this.txtHSETechnicalCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.HSETechnicalId);
                        if (hseTechnical.HSETechnicalDate != null)
                        {
                            this.txtHSETechnicalDate.Text = string.Format("{0:yyyy-MM-dd}", hseTechnical.HSETechnicalDate);
                        }
                        if (!string.IsNullOrEmpty(hseTechnical.UnitId))
                        {
                            var unit = BLL.UnitService.GetUnitByUnitId(hseTechnical.UnitId);
                            if (unit!=null)
                            {
                                this.txtUnitName.Text = unit.UnitName;
                            }
                        }
                        if (!string.IsNullOrEmpty(hseTechnical.TeamGroupId))
                        {
                            var teamGroup = BLL.TeamGroupService.GetTeamGroupById(hseTechnical.TeamGroupId);
                            if (teamGroup!=null)
                            {
                                this.txtTeamGroupName.Text = teamGroup.TeamGroupName;
                            }
                        }
                        this.txtWorkContents.Text = hseTechnical.WorkContents;
                        this.txtAddress.Text = hseTechnical.Address;
                    }
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectHSETechnicalMenuId;
                this.ctlAuditFlow.DataId = this.HSETechnicalId;
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
            if (!string.IsNullOrEmpty(this.HSETechnicalId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/HSETechnicalAttachUrl&menuId={1}", this.HSETechnicalId, BLL.Const.ProjectHSETechnicalMenuId)));
            }
        }
        #endregion

    }
}