using System;
using System.Linq;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class GrooveView : PageBase
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
                string GrooveTypeId = Request.Params["GrooveTypeId"];
                if (!string.IsNullOrEmpty(GrooveTypeId))
                {
                    Model.Base_GrooveType getGrooveType = BLL.Base_GrooveTypeService.GetGrooveTypeByGrooveTypeId(GrooveTypeId);
                    if (getGrooveType != null)
                    {
                        this.txtGrooveTypeCode.Text = getGrooveType.GrooveTypeCode;
                        this.txtGrooveTypeName.Text = getGrooveType.GrooveTypeName;                      
                        this.txtRemark.Text = getGrooveType.Remark;
                    }
                }
            }
        }
        #endregion
    }
}