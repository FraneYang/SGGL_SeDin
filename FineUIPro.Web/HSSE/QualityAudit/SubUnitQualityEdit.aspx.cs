using System;
using System.Linq;
using BLL;

namespace FineUIPro.Web.HSSE.QualityAudit
{
    public partial class SubUnitQualityEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 分包商资质ID
        /// </summary>
        private string SubUnitQualityId
        {
            get
            {
                return (string)ViewState["SubUnitQualityId"];
            }
            set
            {
                ViewState["SubUnitQualityId"] = value;
            }
        }

        /// <summary>
        /// 单位ID
        /// </summary>
        private string UnitId
        {
            get
            {
                return (string)ViewState["UnitId"];
            }
            set
            {
                ViewState["UnitId"] = value;
            }
        }

        /// <summary>
        /// 营业执照扫描件
        /// </summary>
        private string BL_ScanUrl
        {
            get
            {
                return (string)ViewState["BL_ScanUrl"];
            }
            set
            {
                ViewState["BL_ScanUrl"] = value;
            }
        }

        /// <summary>
        /// 机构代码扫描件
        /// </summary>
        private string O_ScanUrl
        {
            get
            {
                return (string)ViewState["O_ScanUrl"];
            }
            set
            {
                ViewState["O_ScanUrl"] = value;
            }
        }

        /// <summary>
        /// 资质证书扫描件
        /// </summary>
        private string C_ScanUrl
        {
            get
            {
                return (string)ViewState["C_ScanUrl"];
            }
            set
            {
                ViewState["C_ScanUrl"] = value;
            }
        }

        /// <summary>
        /// 质量--扫描件
        /// </summary>
        private string QL_ScanUrl
        {
            get
            {
                return (string)ViewState["QL_ScanUrl"];
            }
            set
            {
                ViewState["QL_ScanUrl"] = value;
            }
        }

        /// <summary>
        /// HSE--扫描件
        /// </summary>
        private string H_ScanUrl
        {
            get
            {
                return (string)ViewState["H_ScanUrl"];
            }
            set
            {
                ViewState["H_ScanUrl"] = value;
            }
        }
        /// <summary>
        /// HSE--扫描件
        /// </summary>
        private string H_ScanUrl2
        {
            get
            {
                return (string)ViewState["H_ScanUrl2"];
            }
            set
            {
                ViewState["H_ScanUrl2"] = value;
            }
        }
        /// <summary>
        /// 安全生产许可证扫描件
        /// </summary>
        private string SL_ScanUrl
        {
            get
            {
                return (string)ViewState["SL_ScanUrl"];
            }
            set
            {
                ViewState["SL_ScanUrl"] = value;
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.UnitId = Request.Params["UnitId"];
               
                if (!string.IsNullOrEmpty(this.UnitId))
                {
                    var unit = BLL.UnitService.GetUnitByUnitId(this.UnitId);
                    if (unit != null)
                    {
                        this.txtUnitName.Text = unit.UnitName;
                        this.txtTelephone.Text = unit.Telephone;
                        this.txtEmail.Text = unit.EMail;
                    }
                    var subUnitQuality = BLL.SubUnitQualityService.GetSubUnitQualityByUnitId(this.UnitId);
                    if (subUnitQuality != null)
                    {
                        this.SubUnitQualityId = subUnitQuality.SubUnitQualityId;                        
                        this.txtSubUnitQualityName.Text = subUnitQuality.SubUnitQualityName;
                        this.txtBusinessLicense.Text = subUnitQuality.SubUnitQualityName;
                        this.txtBL_EnableDate.Text = string.Format("{0:yyyy-MM-dd}", subUnitQuality.BL_EnableDate);
                        this.BL_ScanUrl = subUnitQuality.BL_ScanUrl;
                        this.divBL_ScanUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../../", this.BL_ScanUrl);
                        this.txtOrganCode.Text = subUnitQuality.OrganCode;
                        this.txtO_EnableDate.Text = string.Format("{0:yyyy-MM-dd}", subUnitQuality.O_EnableDate);
                        this.O_ScanUrl = subUnitQuality.O_ScanUrl;
                        this.divO_ScanUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../../", this.O_ScanUrl);
                        this.txtCertificate.Text = subUnitQuality.Certificate;
                        this.txtC_EnableDate.Text = string.Format("{0:yyyy-MM-dd}", subUnitQuality.C_EnableDate);
                        this.C_ScanUrl = subUnitQuality.C_ScanUrl;
                        this.divC_ScanUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../../", this.C_ScanUrl);
                        this.txtQualityLicense.Text = subUnitQuality.QualityLicense;
                        this.txtQL_EnableDate.Text = string.Format("{0:yyyy-MM-dd}", subUnitQuality.QL_EnableDate);
                        this.QL_ScanUrl = subUnitQuality.QL_ScanUrl;
                        this.divQL_ScanUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../../", this.QL_ScanUrl);
                        
                        this.txtHSELicense.Text = subUnitQuality.HSELicense;
                        this.txtH_EnableDate.Text = string.Format("{0:yyyy-MM-dd}", subUnitQuality.H_EnableDate);
                        this.H_ScanUrl = subUnitQuality.H_ScanUrl;
                        this.divH_ScanUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../../", this.H_ScanUrl);

                        this.txtHSELicense2.Text = subUnitQuality.HSELicense2;
                        this.txtH_EnableDate2.Text = string.Format("{0:yyyy-MM-dd}", subUnitQuality.H_EnableDate2);
                        this.H_ScanUrl2 = subUnitQuality.H_ScanUrl2;
                        this.divH_ScanUrl2.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../../", this.H_ScanUrl2);
                        
                        this.txtSecurityLicense.Text = subUnitQuality.SecurityLicense;
                        this.txtSL_EnableDate.Text = string.Format("{0:yyyy-MM-dd}", subUnitQuality.SL_EnableDate);
                        this.SL_ScanUrl = subUnitQuality.SL_ScanUrl;
                        this.divSL_ScanUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../../", this.SL_ScanUrl);
                    }
                    
                    this.GetButtonPower();
                }
            }
        }
        #endregion

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }
            string menuId = BLL.Const.SubUnitQualityMenuId;
            if (string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
            {
                menuId = BLL.Const.UnitMenuId;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, menuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 营业执照扫描件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBL_ScanUrl_Click(object sender, EventArgs e)
        {
            if (this.btnBL_ScanUrl.HasFile)
            {
                this.BL_ScanUrl = BLL.UploadFileService.UploadAttachment(BLL.Funs.RootPath, this.btnBL_ScanUrl, this.BL_ScanUrl, UploadFileService.BL_ScanUrlFilePath);
                this.divBL_ScanUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../../", this.BL_ScanUrl);
            }
        }

        /// <summary>
        /// 机构代码描件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnO_ScanUrl_Click(object sender, EventArgs e)
        {
            if (this.btnO_ScanUrl.HasFile)
            {
                this.O_ScanUrl = BLL.UploadFileService.UploadAttachment(BLL.Funs.RootPath, this.btnO_ScanUrl, this.O_ScanUrl, UploadFileService.O_ScanUrlFilePath);
                this.divO_ScanUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../../", this.O_ScanUrl);
            }
        }

        /// <summary>
        /// 资质证书描件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnC_ScanUrl_Click(object sender, EventArgs e)
        {
            if (this.btnC_ScanUrl.HasFile)
            {
                this.C_ScanUrl = BLL.UploadFileService.UploadAttachment(BLL.Funs.RootPath, this.btnC_ScanUrl, this.C_ScanUrl, UploadFileService.C_ScanUrlFilePath);
                this.divC_ScanUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../../", this.C_ScanUrl);
            }
        }

        /// <summary>
        /// 质量--扫描件附件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQL_ScanUrl_Click(object sender, EventArgs e)
        {
            if (this.btnQL_ScanUrl.HasFile)
            {
                this.QL_ScanUrl = BLL.UploadFileService.UploadAttachment(BLL.Funs.RootPath, this.btnQL_ScanUrl, this.QL_ScanUrl, UploadFileService.QL_ScanUrlFilePath);
                this.divQL_ScanUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../../", this.QL_ScanUrl);
            }
        }

        /// <summary>
        /// HSE体系认证证书扫描件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnH_ScanUrl_Click(object sender, EventArgs e)
        {
            if (this.btnH_ScanUrl.HasFile)
            {
                this.H_ScanUrl = BLL.UploadFileService.UploadAttachment(BLL.Funs.RootPath, this.btnH_ScanUrl, this.H_ScanUrl, UploadFileService.H_ScanUrlFilePath);
                this.divH_ScanUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../../", this.H_ScanUrl);
            }
        }
        /// <summary>
        /// HSE体系认证证书扫描件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnH_ScanUrl2_Click(object sender, EventArgs e)
        {
            if (this.btnH_ScanUrl2.HasFile)
            {
                this.H_ScanUrl2 = BLL.UploadFileService.UploadAttachment(BLL.Funs.RootPath, this.btnH_ScanUrl2, this.H_ScanUrl2, UploadFileService.H_ScanUrl2FilePath);
                this.divH_ScanUrl2.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../../", this.H_ScanUrl2);
            }
        }
        /// <summary>
        /// 安全生产许可证扫描件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSL_ScanUrl_Click(object sender, EventArgs e)
        {
            if (this.btnSL_ScanUrl.HasFile)
            {
                this.SL_ScanUrl = BLL.UploadFileService.UploadAttachment(BLL.Funs.RootPath, this.btnSL_ScanUrl, this.SL_ScanUrl, UploadFileService.SL_ScanUrlFilePath);
                this.divSL_ScanUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../../", this.SL_ScanUrl);
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
            Model.QualityAudit_SubUnitQuality subUnitQuality = new Model.QualityAudit_SubUnitQuality
            {
                UnitId = this.UnitId,
                SubUnitQualityName = this.txtSubUnitQualityName.Text.Trim(),
                BusinessLicense = this.txtBusinessLicense.Text.Trim(),
                BL_EnableDate = Funs.GetNewDateTime(this.txtBL_EnableDate.Text.Trim()),
                BL_ScanUrl = this.BL_ScanUrl,
                OrganCode = this.txtOrganCode.Text.Trim(),
                O_EnableDate = Funs.GetNewDateTime(this.txtO_EnableDate.Text.Trim()),
                O_ScanUrl = this.O_ScanUrl,
                Certificate = this.txtCertificate.Text.Trim(),
                C_EnableDate = Funs.GetNewDateTime(this.txtC_EnableDate.Text.Trim()),
                C_ScanUrl = this.C_ScanUrl,
                QualityLicense = this.txtQualityLicense.Text.Trim(),
                QL_EnableDate = Funs.GetNewDateTime(this.txtQL_EnableDate.Text.Trim()),
                QL_ScanUrl = this.QL_ScanUrl,
                HSELicense = this.txtHSELicense.Text.Trim(),
                H_EnableDate = Funs.GetNewDateTime(this.txtH_EnableDate.Text.Trim()),
                H_ScanUrl = this.H_ScanUrl,
                HSELicense2 = this.txtHSELicense2.Text.Trim(),
                H_EnableDate2 = Funs.GetNewDateTime(this.txtH_EnableDate2.Text.Trim()),
                H_ScanUrl2 = this.H_ScanUrl2,
                SecurityLicense = this.txtSecurityLicense.Text.Trim(),
                SL_EnableDate = Funs.GetNewDateTime(this.txtSL_EnableDate.Text.Trim()),
                SL_ScanUrl = this.SL_ScanUrl,
                CompileMan = this.CurrUser.UserId,
                CompileDate = DateTime.Now
            };
            if (!string.IsNullOrEmpty(this.SubUnitQualityId))
            {
                subUnitQuality.SubUnitQualityId = this.SubUnitQualityId;
                BLL.SubUnitQualityService.UpdateSubUnitQuality(subUnitQuality);
                BLL.LogService.AddSys_Log(this.CurrUser, subUnitQuality.SubUnitQualityCode, subUnitQuality.SubUnitQualityId, BLL.Const.SubUnitQualityMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.SubUnitQualityId = SQLHelper.GetNewID(typeof(Model.QualityAudit_SubUnitQuality));
                subUnitQuality.SubUnitQualityId = this.SubUnitQualityId;
                BLL.SubUnitQualityService.AddSubUnitQuality(subUnitQuality);
                BLL.LogService.AddSys_Log(this.CurrUser, subUnitQuality.SubUnitQualityCode, subUnitQuality.SubUnitQualityId, BLL.Const.SubUnitQualityMenuId, BLL.Const.BtnAdd);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        #endregion
    }
}