using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace FineUIPro.Web.HSSE.SitePerson
{
    public partial class DayReportView : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 时间
        /// </summary>
        public string CompileDate
        {
            get
            {
                return (string)ViewState["CompileDate"];
            }
            set
            {
                ViewState["CompileDate"] = value;
            }
        }

        /// <summary>
        /// 主键
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
            }
        }
        #endregion

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
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.CompileDate = Request.Params["CompileDate"];
              
                BindGrid();
            }
        }
        #endregion

        #region 绑定明细数据
        /// <summary>
        /// 绑定明细数据
        /// </summary>
        private void BindGrid()
        {
            DateTime sdate = Funs.GetNewDateTimeOrNow(this.CompileDate);
            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", sdate);
            Grid1.DataSource = BLL.SitePerson_DayReportDetailService.getDayReportDetails(this.ProjectId, sdate);
            Grid1.DataBind();
        }
        #endregion

        #region 关闭弹出窗
        /// <summary>
        /// 关闭弹出窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region Grid双击事件
        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            btnMenuEdit_Click(null, null);
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 编辑按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }

            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("DayReportDetailView.aspx?ProjectId={0}&UnitId={1}&CompileDate={2}", this.ProjectId, Grid1.SelectedRowID, this.CompileDate, "查看 - ")));
        }
        #endregion
    }
}