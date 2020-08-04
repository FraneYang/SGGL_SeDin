using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.HSSE.SitePerson
{
    public partial class MonthReportView : PageBase
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
            Grid1.DataSource = BLL.SitePerson_MonthReportDetailService.getMonthReportDetails(this.ProjectId, sdate);
            Grid1.DataBind();
        }
        #endregion
    }
}