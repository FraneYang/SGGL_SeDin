using System;
using System.Linq;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class PipingClassView : PageBase
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
                string PipingClassId = Request.Params["PipingClassId"];
                if (!string.IsNullOrEmpty(PipingClassId))
                {
                    Model.Base_PipingClass getPipingClass = BLL.Base_PipingClassService.GetPipingClassByPipingClassId(PipingClassId);
                    if (getPipingClass != null)
                    {
                        this.txtPipingClassCode.Text = getPipingClass.PipingClassCode;
                        this.txtPipingClassName.Text = getPipingClass.PipingClassName;                      
                        this.txtRemark.Text = getPipingClass.Remark;
                    }
                }
            }
        }
        #endregion
    }
}