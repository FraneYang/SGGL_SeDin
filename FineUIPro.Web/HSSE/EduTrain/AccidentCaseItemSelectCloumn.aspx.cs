using System;

namespace FineUIPro.Web.HSSE.EduTrain
{
    public partial class AccidentCaseItemSelectCloumn : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            btnClose.OnClientClick = ActiveWindow.GetHideReference();
        }

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
    }
}