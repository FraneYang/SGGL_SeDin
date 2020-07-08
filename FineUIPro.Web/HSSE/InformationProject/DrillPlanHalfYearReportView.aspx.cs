using System;
using System.Collections.Generic;

namespace FineUIPro.Web.HSSE.InformationProject
{
    public partial class DrillPlanHalfYearReportView : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string DrillPlanHalfYearReportId
        {
            get
            {
                return (string)ViewState["DrillPlanHalfYearReportId"];
            }
            set
            {
                ViewState["DrillPlanHalfYearReportId"] = value;
            }
        }
        /// <summary>
        /// 项目主键
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

        /// <summary>
        /// 定义集合
        /// </summary>
        private static List<Model.InformationProject_DrillPlanHalfYearReportItem> items = new List<Model.InformationProject_DrillPlanHalfYearReportItem>();
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
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.CurrUser.LoginProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                
                this.DrillPlanHalfYearReportId = Request.Params["DrillPlanHalfYearReportId"];
                if (!string.IsNullOrEmpty(this.DrillPlanHalfYearReportId))
                {
                    items = BLL.ProjectDrillPlanHalfYearReportItemService.GetDrillPlanHalfYearReportItemList(this.DrillPlanHalfYearReportId);
                    this.Grid1.DataSource = items;
                    this.Grid1.DataBind();

                    var drill = BLL.ProjectDrillPlanHalfYearReportService.GetDrillPlanHalfYearReportById(this.DrillPlanHalfYearReportId);
                    if (drill != null)
                    {
                        this.ProjectId = drill.ProjectId;
                        this.txtprojectName.Text = BLL.ProjectService.GetProjectNameByProjectId(this.ProjectId);
                        if (drill.YearId.HasValue)
                        {
                            var constValue = BLL.ConstValue.GetConstByConstValueAndGroupId(drill.YearId.ToString(), BLL.ConstValue.Group_0008);
                            if (constValue!=null)
                            {
                                this.txtYearId.Text = constValue.ConstText;
                            }
                        }
                        if (drill.HalfYearId.HasValue)
                        {
                            var constValue = BLL.ConstValue.GetConstByConstValueAndGroupId(drill.HalfYearId.ToString(), BLL.ConstValue.Group_0010);
                            if (constValue != null)
                            {
                                this.txtHalfYearId.Text = constValue.ConstText;
                            }
                        }
                        if (!string.IsNullOrEmpty(drill.CompileMan))
                        {
                            this.txtCompileMan.Text = drill.CompileMan;
                        }
                        this.txtTel.Text = drill.Telephone;
                        if (drill.CompileDate != null)
                        {
                            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", drill.CompileDate);
                        }
                    }
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectDrillPlanHalfYearReportMenuId;
                this.ctlAuditFlow.DataId = this.DrillPlanHalfYearReportId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
            }
        }
        #endregion
    }
}