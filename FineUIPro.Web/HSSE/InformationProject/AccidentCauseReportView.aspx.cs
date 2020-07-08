using BLL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FineUIPro.Web.HSSE.InformationProject
{
    public partial class AccidentCauseReportView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string AccidentCauseReportId
        {
            get
            {
                return (string)ViewState["AccidentCauseReportId"];
            }
            set
            {
                ViewState["AccidentCauseReportId"] = value;
            }
        }

        /// <summary>
        /// 定义集合
        /// </summary>
        private static List<Model.InformationProject_AccidentCauseReportItem> items = new List<Model.InformationProject_AccidentCauseReportItem>();
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
                this.AccidentCauseReportId = Request.Params["AccidentCauseReportId"];
                if (!string.IsNullOrEmpty(this.AccidentCauseReportId))
                {
                    var q = BLL.ProjectAccidentCauseReportService.GetAccidentCauseReportById(AccidentCauseReportId);
                    if (q != null)
                    {
                        if (q.Month != null && q.Year != null)
                        {
                            string lastMonth = (from x in new Model.SGGLDB(Funs.ConnString).Sys_Const where x.GroupId == BLL.ConstValue.Group_0009 && Convert.ToInt32(x.ConstValue) == (q.Month - 1) select x.ConstText).FirstOrDefault();
                            lbLastMonth.Text = "(" + lastMonth + ")";
                            this.lblYearAndMonth.Text = "职工伤亡事故原因分析【" + q.Year.ToString() + "年" + q.Month.ToString() + "月】报表";
                        }
                        txtAccidentCauseReportCode.Text = CodeRecordsService.ReturnCodeByDataId(AccidentCauseReportId);
                        if (q.DeathAccident != null)
                        {
                            txtDeathAccident.Text = q.DeathAccident.ToString();
                        }
                        if (q.DeathToll != null)
                        {
                            txtDeathToll.Text = q.DeathToll.ToString();
                        }
                        if (q.InjuredAccident != null)
                        {
                            txtInjuredAccident.Text = q.InjuredAccident.ToString();
                        }
                        if (q.InjuredToll != null)
                        {
                            txtInjuredToll.Text = q.InjuredToll.ToString();
                        }
                        if (q.MinorWoundAccident != null)
                        {
                            txtMinorWoundAccident.Text = q.MinorWoundAccident.ToString();
                        }
                        if (q.MinorWoundToll != null)
                        {
                            txtMinorWoundToll.Text = q.MinorWoundToll.ToString();
                        }
                        if (q.AverageTotalHours != null)
                        {
                            txtAverageTotalHours.Text = q.AverageTotalHours.ToString();
                        }
                        if (q.AverageManHours != null)
                        {
                            txtAverageManHours.Text = q.AverageManHours.ToString();
                        }
                        if (q.TotalLossMan != null)
                        {
                            txtTotalLossMan.Text = q.TotalLossMan.ToString();
                        }
                        if (q.LastMonthLossHoursTotal != null)
                        {
                            txtLastMonthLossHoursTotal.Text = q.LastMonthLossHoursTotal.ToString();
                        }
                        if (q.KnockOffTotal != null)
                        {
                            txtKnockOffTotal.Text = q.KnockOffTotal.ToString();
                        }
                        if (q.DirectLoss != null)
                        {
                            txtDirectLoss.Text = q.DirectLoss.ToString();
                        }
                        if (q.IndirectLosses != null)
                        {
                            txtIndirectLosses.Text = q.IndirectLosses.ToString();
                        }
                        if (q.TotalLoss != null)
                        {
                            txtTotalLoss.Text = q.TotalLoss.ToString();
                        }
                        if (q.TotalLossTime != null)
                        {
                            txtTotalLossTime.Text = q.TotalLossTime.ToString();
                        }
                        this.txtCompileManName.Text = BLL.UserService.GetUserNameByUserId(q.CompileMan);
                        if (q.CompileDate != null)
                        {
                            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", q.CompileDate);
                        }

                        this.lblUnitName.Text = BLL.UnitService.GetUnitNameByUnitId(q.UnitId);
                        items = BLL.ProjectAccidentCauseReportItemService.GetItemsNoSum(AccidentCauseReportId);
                        this.Grid1.DataSource = items;
                        this.Grid1.DataBind();

                    }
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectAccidentCauseReportMenuId;
                this.ctlAuditFlow.DataId = this.AccidentCauseReportId;
                this.ctlAuditFlow.ProjectId = this.CurrUser.LoginProjectId;
            }
        }
        #endregion
    }
}