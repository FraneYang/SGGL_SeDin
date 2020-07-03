using System;
using System.Linq;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class WeldView : PageBase
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
                string WeldTypeId = Request.Params["WeldTypeId"];
                if (!string.IsNullOrEmpty(WeldTypeId))
                {
                    Model.Base_WeldType getWeldType = BLL.Base_WeldTypeService.GetWeldTypeByWeldTypeId(WeldTypeId);
                    if (getWeldType != null)
                    {
                        this.txtWeldTypeCode.Text = getWeldType.WeldTypeCode;
                        this.txtWeldTypeName.Text = getWeldType.WeldTypeName;                      
                        this.txtRemark.Text = getWeldType.Remark;
                    }
                }
            }
        }
        #endregion
    }
}