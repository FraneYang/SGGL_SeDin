using System;
using System.Linq;
using BLL;

namespace FineUIPro.Web.HSSE.SitePerson
{
    public partial class MonthReportEdit : PageBase
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();                
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.MonthReportId = Request.Params["MonthReportId"];
                var monthReport = BLL.SitePerson_MonthReportService.GetMonthReportByMonthReportId(this.MonthReportId);
                if (monthReport != null)
                {
                    this.ProjectId = monthReport.ProjectId;
                    this.txtMonthReportCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.MonthReportId);
                    this.txtCompileMan.Text = BLL.UserService.GetUserNameByUserId(monthReport.CompileMan);
                    this.txtCompileDate.Text = String.Format("{0:yyyy-MM}", monthReport.CompileDate);
                }
                BindGrid();

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectMonthReportMenuId;
                this.ctlAuditFlow.DataId = this.MonthReportId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
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

        #region 提交按钮
        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.ctlAuditFlow.NextStep == BLL.Const.State_1 && this.ctlAuditFlow.NextPerson == BLL.Const._Null)
            {
                ShowNotify("请选择下一步办理人！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        #endregion

        #region 保存按钮
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.SitePerson_MonthReport monthReport = BLL.SitePerson_MonthReportService.GetMonthReportByMonthReportId(this.MonthReportId);
            if (monthReport != null)
            {
                if (type == BLL.Const.BtnSubmit)
                {
                    monthReport.States = this.ctlAuditFlow.NextStep;
                }

                BLL.LogService.AddSys_Log(this.CurrUser, null, monthReport.MonthReportId, BLL.Const.ProjectMonthReportMenuId, BLL.Const.BtnModify);
                BLL.SitePerson_MonthReportService.UpdateMonthReport(monthReport);               
                ////保存流程审核数据         
                this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectMonthReportMenuId, this.MonthReportId, (type == BLL.Const.BtnSubmit ? true : false), this.txtCompileDate.Text.Trim(), "../SitePerson/MonthReportView.aspx?MonthReportId={0}");
            }
        }

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
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("MonthReportDetailEdit.aspx?MonthReportDetailId={0}", monthReportDetailId, "编辑 - ")));

        }
        #endregion
    }
}