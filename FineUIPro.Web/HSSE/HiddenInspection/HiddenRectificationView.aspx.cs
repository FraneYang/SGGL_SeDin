using System;
using System.Linq;
using System.Web.UI.WebControls;

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
                    Model.View_Hazard_HazardRegister registration = (from x in BLL.Funs.DB.View_Hazard_HazardRegister where x.HazardRegisterId == HazardRegisterId select x).FirstOrDefault();
                    if (registration != null)
                    {
                        this.txtWorkAreaName.Text = registration.WorkAreaName;
                        this.txtResponsibilityUnitName.Text = registration.ResponsibilityUnitName;
                        this.txtRegisterTypesName.Text = registration.RegisterTypesName;
                        this.txtProblemDescription.Text = registration.RegisterDef;
                        this.txtTakeSteps.Text = registration.Rectification;
                        this.txtResponsibilityManName.Text = registration.ResponsibilityManName;
                        this.txtRectificationPeriod.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", registration.RectificationPeriod);
                        this.txtCheckManName.Text = registration.CheckManName;
                        this.txtCheckTime.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", registration.CheckTime);
                        this.txtRectificationTime.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", registration.RectificationTime);
                        this.txtStates.Text = registration.StatesStr;
                        this.ImageUrl = registration.ImageUrl;
                        this.RectificationImageUrl = registration.RectificationImageUrl;
                        this.divImageUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("~/", this.ImageUrl);
                        this.divRectificationImageUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("~/", this.RectificationImageUrl);
                        //var punishRecords = (from x in BLL.Funs.DB.View_Common_PunishRecord
                        //                     where x.HazardRegisterId == this.HazardRegisterId
                        //                     orderby x.PunishDate descending
                        //                     select x).ToList();
                        //Grid1.DataSource = punishRecords;
                        //Grid1.DataBind();
                    }
                }
            }
        }
        #endregion
    }
}