using System;
using System.Linq;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class DetectionView : PageBase
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
                string DetectionRateId = Request.Params["DetectionRateId"];
                if (!string.IsNullOrEmpty(DetectionRateId))
                {
                    Model.Base_DetectionRate getDetectionRate = BLL.Base_DetectionRateService.GetDetectionRateByDetectionRateId(DetectionRateId);
                    if (getDetectionRate != null)
                    {
                        this.txtDetectionRateCode.Text = getDetectionRate.DetectionRateCode;
                        this.txtDetectionRateValue.Text = getDetectionRate.DetectionRateValue.ToString();                      
                        this.txtRemark.Text = getDetectionRate.Remark;
                    }
                }
            }
        }
        #endregion
    }
}