using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class ProtectionGasView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string ProtectionGasId
        {
            get
            {
                return (string)ViewState["ProtectionGasId"];
            }
            set
            {
                ViewState["ProtectionGasId"] = value;
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

                this.ProtectionGasId = Request.Params["ProtectionGasId"];
                if (!string.IsNullOrEmpty(this.ProtectionGasId))
                {
                    Model.Base_ProtectionGas ProtectionGas = BLL.Base_ProtectionGasService.GetProtectionGasByProtectionGasId(this.ProtectionGasId);
                    if (ProtectionGas != null)
                    {
                        this.txtProtectionGasCode.Text = ProtectionGas.ProtectionGasCode;
                        this.txtProtectionGasName.Text = ProtectionGas.ProtectionGasName;
                        this.txtRemark.Text = ProtectionGas.Remark;
                    }
                }
            }
        }
        #endregion
    }
}