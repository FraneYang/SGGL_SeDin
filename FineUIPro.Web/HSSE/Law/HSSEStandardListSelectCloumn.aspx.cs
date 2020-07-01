using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.Law
{
    public partial class HSSEStandardListSelectCloumn : PageBase
    {
        #region 加载页面
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
            }
        }
        #endregion

        #region 导出
        /// <summary>
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {
            // 1. 这里放置保存窗体中数据的逻辑                        
            // 2. 关闭本窗体，然后回发父窗体           
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference(String.Join("#", cblColumns.SelectedValueArray)));
        }
        #endregion
    }
}