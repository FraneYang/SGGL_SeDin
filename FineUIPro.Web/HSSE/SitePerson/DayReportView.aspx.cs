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
        /// 主键
        /// </summary>
        public string DayReportId
        {
            get
            {
                return (string)ViewState["DayReportId"];
            }
            set
            {
                ViewState["DayReportId"] = value;
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
                this.DayReportId = Request.Params["DayReportId"];
                var dayReport = BLL.SitePerson_DayReportService.GetDayReportByDayReportId(this.DayReportId);
                if (dayReport != null)
                {
                    this.ProjectId = dayReport.ProjectId;
                    this.txtDayReportCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.DayReportId);
                    var user = BLL.UserService.GetUserByUserId(dayReport.CompileMan);
                    if (user != null)
                    {
                        this.txtCompileMan.Text = user.UserName;
                    }
                    if (dayReport.CompileDate.HasValue)
                    {
                        this.txtCompileDate.Text = String.Format("{0:yyyy-MM-dd}", dayReport.CompileDate);
                    }
                }
                BindGrid();

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.DayReportMenuId;
                this.ctlAuditFlow.DataId = this.DayReportId;
            }
        }
        #endregion

        #region 绑定明细数据
        /// <summary>
        /// 绑定明细数据
        /// </summary>
        private void BindGrid()
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var q = from x in db.SitePerson_DayReportDetail
                        join a in db.SitePerson_DayReport
                        on x.DayReportId equals a.DayReportId
                        join b in db.Base_Unit
                        on x.UnitId equals b.UnitId
                        where x.DayReportId == this.DayReportId
                        orderby b.UnitCode
                        select new
                        {
                            x.DayReportDetailId,
                            x.DayReportId,
                            x.UnitId,
                            x.StaffData,
                            x.DayNum,
                            x.WorkTime,
                            x.CheckPersonNum,
                            x.RealPersonNum,
                            x.PersonWorkTime,
                            YearPersonWorkTime = (from y in db.SitePerson_DayReportDetail
                                                  where (from z in db.SitePerson_DayReport
                                                         where z.CompileDate <= a.CompileDate && z.CompileDate.Value.Year == a.CompileDate.Value.Year
                                                         && x.UnitId == y.UnitId && z.ProjectId == this.ProjectId
                                                         select z.DayReportId).Contains(y.DayReportId)
                                                  select y.PersonWorkTime ?? 0).Sum(),
                            TotalPersonWorkTime = (from y in db.SitePerson_DayReportDetail
                                                   where (from z in db.SitePerson_DayReport
                                                          where z.CompileDate <= a.CompileDate && x.UnitId == y.UnitId && z.ProjectId == this.ProjectId
                                                          select z.DayReportId).Contains(y.DayReportId)
                                                   select y.PersonWorkTime ?? 0).Sum(),
                            x.Remark,
                            b.UnitName,
                        };
                Grid1.DataSource = q;
                Grid1.DataBind();
            }
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
            string dayReportDetailId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("DayReportDetailEdit.aspx?DayReportDetailId={0}&type=view", dayReportDetailId, "查看 - ")));

        }
        #endregion
    }
}