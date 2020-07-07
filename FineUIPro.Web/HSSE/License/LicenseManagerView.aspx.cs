using System;
using System.Web;

namespace FineUIPro.Web.HSSE.License
{
    public partial class LicenseManagerView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string LicenseManagerId
        {
            get
            {
                return (string)ViewState["LicenseManagerId"];
            }
            set
            {
                ViewState["LicenseManagerId"] = value;
            }
        }
        #endregion

        #region 加载
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
                this.LicenseManagerId = Request.Params["LicenseManagerId"];
                if (!string.IsNullOrEmpty(this.LicenseManagerId))
                {
                    Model.License_LicenseManager licenseManager = BLL.LicenseManagerService.GetLicenseManagerById(this.LicenseManagerId);
                    if (licenseManager != null)
                    {
                        this.txtLicenseManagerCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.LicenseManagerId);
                        if (!string.IsNullOrEmpty(licenseManager.UnitId))
                        {
                            var unit = BLL.UnitService.GetUnitByUnitId(licenseManager.UnitId);
                            if (unit != null)
                            {
                                this.txtUnitName.Text = unit.UnitName;
                            }
                        }
                        if (!string.IsNullOrEmpty(licenseManager.LicenseTypeId))
                        {
                            var licenseType = BLL.LicenseTypeService.GetLicenseTypeById(licenseManager.LicenseTypeId);
                            if (licenseType != null)
                            {
                                this.txtLicenseTypeName.Text = licenseType.LicenseTypeName;
                            }
                        }
                        if (!string.IsNullOrEmpty(licenseManager.WorkAreaId))
                        {
                            this.txtWorkAreaName.Text = BLL.UnitWorkService.GetUnitWorkName(licenseManager.WorkAreaId);
                        }
                        this.txtApplicantMan.Text = licenseManager.ApplicantMan;
                        this.txtStartDate.Text = string.Format("{0:yyyy-MM-dd}", licenseManager.StartDate);
                        this.txtEndDate.Text = string.Format("{0:yyyy-MM-dd}", licenseManager.EndDate);                        
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", licenseManager.CompileDate);
                        this.txtLicenseManageContents.Text = HttpUtility.HtmlDecode(licenseManager.LicenseManageContents);
                        this.txtWorkStates.Text = "已关闭";
                        if (licenseManager.WorkStates == "1")
                        {
                            this.txtWorkStates.Text = "待开工";
                        }
                       else if (licenseManager.WorkStates == "2")
                        {
                            this.txtWorkStates.Text = "作业中";
                        }
                        else if (licenseManager.WorkStates == "-1")
                        {
                            this.txtWorkStates.Text = "已取消";
                        }
                    }
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectLicenseManagerMenuId;
                this.ctlAuditFlow.DataId = this.LicenseManagerId;
            }
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
            if (!string.IsNullOrEmpty(this.LicenseManagerId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/LicenseManagerAttachUrl&menuId={1}&type=-1", LicenseManagerId, BLL.Const.ProjectLicenseManagerMenuId)));
            }
        }
        #endregion
    }
}