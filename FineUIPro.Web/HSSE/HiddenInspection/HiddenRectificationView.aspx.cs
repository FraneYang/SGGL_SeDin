using BLL;
using System;
using System.Data;
using System.Linq;

namespace FineUIPro.Web.HSSE.HiddenInspection
{
    public partial class HiddenRectificationView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string HazardRegisterId
        {
            get
            {
                return (string)ViewState["HazardRegisterId"];
            }
            set
            {
                ViewState["HazardRegisterId"] = value;
            }
        }

        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImageUrl
        {
            get
            {
                return (string)ViewState["ImageUrl"];
            }
            set
            {
                ViewState["ImageUrl"] = value;
            }
        }

        /// <summary>
        /// 整改后附件路径
        /// </summary>
        public string RectificationImageUrl
        {
            get
            {
                return (string)ViewState["RectificationImageUrl"];
            }
            set
            {
                ViewState["RectificationImageUrl"] = value;
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
                this.HazardRegisterId = Request.Params["HazardRegisterId"];
                if (!string.IsNullOrEmpty(this.HazardRegisterId))
                {
                    var registration = Funs.DB.View_Hazard_HazardRegister.FirstOrDefault(x => x.HazardRegisterId == HazardRegisterId);
                    if (registration != null)
                    {
                        this.txtRegisterDef.Text = registration.RegisterDef;
                        this.txtCheckManName.Text = registration.CheckManName;
                        this.txtCheckTime.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", registration.CheckTime);
                        this.drpRegisterTypes.Text = registration.RegisterTypesName;
                        this.drpRegisterTypes2.Text = registration.RegisterTypes2Name;
                        this.drpRegisterTypes3.Text = registration.RegisterTypes3Name;
                        this.drpRegisterTypes4.Text = registration.RegisterTypes4Name;
                        this.drpHazardValue.Text = registration.HazardValue;
                        this.txtRequirements.Text = registration.Requirements;
                        this.drpUnit.Text = registration.ResponsibilityUnitName;
                        this.drpResponsibleMan.Text = registration.ResponsibilityManName;
                        this.drpWorkArea.Text = registration.WorkAreaName;
                        this.txtRectificationPeriod.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", registration.RectificationPeriod);
                        this.drpCCManIds.Text = registration.CCManNames;
                        this.txtRectification.Text = registration.Rectification;

                        this.txtRectificationTime.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", registration.RectificationTime);
                        this.txtStates.Text = registration.StatesStr;
                        this.ImageUrl = registration.ImageUrl;
                        this.RectificationImageUrl = registration.RectificationImageUrl;
                        this.divImageUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../../", this.ImageUrl);
                        this.divRectificationImageUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../../", this.RectificationImageUrl);                      
                    }
                }
            }
        }
        #endregion
    }
}