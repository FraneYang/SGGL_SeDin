using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class GasProtectionModeView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string GasProtectionModeId
        {
            get
            {
                return (string)ViewState["GasProtectionModeId"];
            }
            set
            {
                ViewState["GasProtectionModeId"] = value;
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

                this.GasProtectionModeId = Request.Params["GasProtectionModeId"];
                if (!string.IsNullOrEmpty(this.GasProtectionModeId))
                {
                    Model.Base_GasProtectionMode GasProtectionMode = BLL.Base_GasProtectionModeService.GetGasProtectionModeByGasProtectionModeId(this.GasProtectionModeId);
                    if (GasProtectionMode != null)
                    {
                        this.txtGasProtectionModeName.Text = GasProtectionMode.GasProtectionModeName;
                        this.txtRemark.Text = GasProtectionMode.Remark;
                    }
                }
            }
        }
        #endregion
    }
}