using System;
using System.Linq;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class ComponentsView : PageBase
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
                string ComponentsId = Request.Params["ComponentsId"];
                if (!string.IsNullOrEmpty(ComponentsId))
                {
                    Model.Base_Components getComponents = BLL.Base_ComponentsService.GetComponentsByComponentsId(ComponentsId);
                    if (getComponents != null)
                    {
                        this.txtComponentsCode.Text = getComponents.ComponentsCode;
                        this.txtComponentsName.Text = getComponents.ComponentsName;                      
                        this.txtRemark.Text = getComponents.Remark;
                    }
                }
            }
        }
        #endregion
    }
}