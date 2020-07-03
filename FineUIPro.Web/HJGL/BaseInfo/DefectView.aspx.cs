using System;
using System.Linq;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class DefectView : PageBase
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
                string DefectId = Request.Params["DefectId"];
                if (!string.IsNullOrEmpty(DefectId))
                {
                    Model.Base_Defect getDefect = BLL.Base_DefectService.GetDefectByDefectId(DefectId);
                    if (getDefect != null)
                    {
                        this.txtDefectName.Text = getDefect.DefectName;
                        this.txtDefectEngName.Text = getDefect.DefectEngName;
                    }
                }
            }
        }
        #endregion
    }
}