using BLL;
using System;
using System.Collections.Generic;

namespace FineUIPro.Web.HSSE.InformationProject
{
    public partial class DrillConductedQuarterlyReportView : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string DrillConductedQuarterlyReportId
        {
            get
            {
                return (string)ViewState["DrillConductedQuarterlyReportId"];
            }
            set
            {
                ViewState["DrillConductedQuarterlyReportId"] = value;
            }
        }

        /// <summary>
        /// 定义集合
        /// </summary>
        private static List<Model.InformationProject_DrillConductedQuarterlyReportItem> items = new List<Model.InformationProject_DrillConductedQuarterlyReportItem>();
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
                
                this.DrillConductedQuarterlyReportId = Request.Params["DrillConductedQuarterlyReportId"];
                if (!string.IsNullOrEmpty(this.DrillConductedQuarterlyReportId))
                {
                    items = BLL.ProjectDrillConductedQuarterlyReportItemService.GetDrillConductedQuarterlyReportItemList(this.DrillConductedQuarterlyReportId);
                    int i = items.Count * 10;
                    int count = items.Count;
                    if (count < 10)
                    {
                        for (int j = 0; j < (10 - count); j++)
                        {
                            i += 10;
                            Model.InformationProject_DrillConductedQuarterlyReportItem newItem = new Model.InformationProject_DrillConductedQuarterlyReportItem
                            {
                                DrillConductedQuarterlyReportItemId = SQLHelper.GetNewID(typeof(Model.InformationProject_DrillConductedQuarterlyReportItem)),
                                SortIndex = i
                            };
                            items.Add(newItem);
                        }
                    }
                    this.Grid1.DataSource = items;
                    this.Grid1.DataBind();

                    var drillConductedQuarterlyReport = BLL.ProjectDrillConductedQuarterlyReportService.GetDrillConductedQuarterlyReportById(this.DrillConductedQuarterlyReportId);
                    if (drillConductedQuarterlyReport != null)
                    {
                        if (drillConductedQuarterlyReport.YearId.HasValue)
                        {
                            var constValue = BLL.ConstValue.GetConstByConstValueAndGroupId(drillConductedQuarterlyReport.YearId.ToString(), BLL.ConstValue.Group_0008);
                            if (constValue!=null)
                            {
                                this.txtYear.Text = constValue.ConstText;   
                            }
                        }
                        if (drillConductedQuarterlyReport.Quarter.HasValue)
                        {
                            var constValue = BLL.ConstValue.GetConstByConstValueAndGroupId(drillConductedQuarterlyReport.Quarter.ToString(), BLL.ConstValue.Group_0011);
                            if (constValue != null)
                            {
                                this.txtQuarter.Text = constValue.ConstText;
                            }
                        }
                        if (drillConductedQuarterlyReport.CompileDate != null)
                        {
                            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", drillConductedQuarterlyReport.CompileDate);
                        }
                    }
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectDrillConductedQuarterlyReportMenuId;
                this.ctlAuditFlow.DataId = this.DrillConductedQuarterlyReportId;
                this.ctlAuditFlow.ProjectId = this.CurrUser.LoginProjectId;
            }
        }
        #endregion
    }
}