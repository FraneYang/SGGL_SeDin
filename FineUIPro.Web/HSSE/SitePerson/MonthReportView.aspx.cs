using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.IO;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.HSSE.SitePerson
{
    public partial class MonthReportView : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string MonthReportId
        {
            get
            {
                return (string)ViewState["MonthReportId"];
            }
            set
            {
                ViewState["MonthReportId"] = value;
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

                this.MonthReportId = Request.Params["MonthReportId"];
                var monthReport = BLL.SitePerson_MonthReportService.GetMonthReportByMonthReportId(this.MonthReportId);
                if (monthReport != null)
                {
                    this.ProjectId = monthReport.ProjectId;
                    this.txtMonthReportCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.MonthReportId);
                    var user = BLL.UserService.GetUserByUserId(monthReport.CompileMan);
                    if (user != null)
                    {
                        this.txtCompileMan.Text = user.UserName;
                    }
                    if (monthReport.CompileDate.HasValue)
                    {
                        this.txtCompileDate.Text = String.Format("{0:yyyy-MM}", monthReport.CompileDate);
                    }
                }
                BindGrid();

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectMonthReportMenuId;
                this.ctlAuditFlow.DataId = this.MonthReportId;
            }
        }
        #endregion

        #region 绑定明细数据
        /// <summary>
        /// 绑定明细数据
        /// </summary>
        private void BindGrid()
        {            
            var q = from x in Funs.DB.SitePerson_MonthReportDetail
                    join a in Funs.DB.SitePerson_MonthReport
                    on x.MonthReportId equals a.MonthReportId
                    join b in Funs.DB.Base_Unit
                    on x.UnitId equals b.UnitId
                    where x.MonthReportId == this.MonthReportId
                    orderby b.UnitCode
                    select new
                    {
                        x.MonthReportDetailId,
                        x.MonthReportId,
                        x.UnitId,
                        x.StaffData,
                        x.DayNum,
                        x.WorkTime,
                        x.CheckPersonNum,
                        x.RealPersonNum,
                        x.PersonWorkTime,
                        YearPersonWorkTime = (from y in Funs.DB.SitePerson_MonthReportDetail
                                              where (from z in Funs.DB.SitePerson_MonthReport
                                                     where z.CompileDate <= a.CompileDate && z.CompileDate.Value.Year == a.CompileDate.Value.Year
                                                     && x.UnitId == y.UnitId && z.ProjectId == this.ProjectId
                                                     select z.MonthReportId).Contains(y.MonthReportId)
                                              select y.PersonWorkTime ?? 0).Sum(),
                        TotalPersonWorkTime = (from y in Funs.DB.SitePerson_MonthReportDetail
                                               where (from z in Funs.DB.SitePerson_MonthReport
                                                      where z.CompileDate <= a.CompileDate && x.UnitId == y.UnitId && z.ProjectId == this.ProjectId
                                                      select z.MonthReportId).Contains(y.MonthReportId)
                                               select y.PersonWorkTime ?? 0).Sum(),
                        x.Remark,
                        b.UnitName,
                    };
            Grid1.DataSource = q;
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
            string monthReportDetailId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("MonthReportDetailEdit.aspx?MonthReportDetailId={0}&type=view", monthReportDetailId, "编辑 - ")));

        }
        #endregion
    }
}