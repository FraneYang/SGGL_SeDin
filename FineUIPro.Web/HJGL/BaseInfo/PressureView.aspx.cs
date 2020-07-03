using System;
using System.Linq;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class PressureView : PageBase
    {
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
                string PressureId = Request.Params["PressureId"];
                if (!string.IsNullOrEmpty(PressureId))
                {
                    Model.Base_Pressure getPressure = BLL.Base_PressureService.GetPressureByPressureId(PressureId);
                    if (getPressure != null)
                    {
                        this.txtPressureCode.Text = getPressure.PressureCode;
                        this.txtPressureName.Text = getPressure.PressureName;
                        this.txtRemark.Text = getPressure.Remark;
                    }
                }
            }
        }
        #endregion
    }
}