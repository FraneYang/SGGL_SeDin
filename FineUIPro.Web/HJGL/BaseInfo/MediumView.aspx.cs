using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class MediumView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string MediumId
        {
            get
            {
                return (string)ViewState["MediumId"];
            }
            set
            {
                ViewState["MediumId"] = value;
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
                this.txtMediumCode.Focus();
                btnClose.OnClientClick = ActiveWindow.GetHideReference();

                this.MediumId = Request.Params["MediumId"];
                if (!string.IsNullOrEmpty(this.MediumId))
                {
                    Model.Base_Medium Medium = BLL.Base_MediumService.GetMediumByMediumId(this.MediumId);
                    if (Medium != null)
                    {
                        this.txtMediumCode.Text = Medium.MediumCode;
                        this.txtMediumName.Text = Medium.MediumName;
                        this.txtMediumAbbreviation.Text = Medium.MediumAbbreviation;
                        this.txtRemark.Text = Medium.Remark;
                    }
                }
            }
        }
        #endregion
    }
}