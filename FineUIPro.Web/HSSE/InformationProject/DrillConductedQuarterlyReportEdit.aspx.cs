using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FineUIPro.Web.HSSE.InformationProject
{
    public partial class DrillConductedQuarterlyReportEdit : PageBase
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
        #region 项目主键
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
        #endregion
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
                items.Clear();
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.CurrUser.LoginProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                BLL.ConstValue.InitConstValueDropDownList(this.ddlYearId, BLL.ConstValue.Group_0008, true);
                BLL.ConstValue.InitConstValueDropDownList(this.ddlQuarter, BLL.ConstValue.Group_0011, true);
             
                this.DrillConductedQuarterlyReportId = Request.Params["DrillConductedQuarterlyReportId"];
                if (!string.IsNullOrEmpty(this.DrillConductedQuarterlyReportId))
                {
                    items = BLL.ProjectDrillConductedQuarterlyReportItemService.GetDrillConductedQuarterlyReportItemList(this.DrillConductedQuarterlyReportId);                    
                    this.Grid1.DataSource = items;
                    this.Grid1.DataBind();

                    var drillConductedQuarterlyReport = BLL.ProjectDrillConductedQuarterlyReportService.GetDrillConductedQuarterlyReportById(this.DrillConductedQuarterlyReportId);
                    if (drillConductedQuarterlyReport != null)
                    {
                        this.ProjectId = drillConductedQuarterlyReport.ProjectId;
                        if (drillConductedQuarterlyReport.YearId.HasValue)
                        {
                            this.ddlYearId.SelectedValue = drillConductedQuarterlyReport.YearId.ToString();
                        }
                        if (drillConductedQuarterlyReport.Quarter.HasValue)
                        {
                            this.ddlQuarter.SelectedValue = drillConductedQuarterlyReport.Quarter.ToString();
                        }
                        if (drillConductedQuarterlyReport.CompileDate != null)
                        {
                            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", drillConductedQuarterlyReport.CompileDate);
                        }
                    }
                }
                else
                {
                    DateTime showDate = DateTime.Now.AddMonths(-3);
                    this.ddlQuarter.SelectedValue = Funs.GetNowQuarterlyByTime(showDate).ToString();
                    this.ddlYearId.SelectedValue = showDate.Year.ToString();
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    DateTime startTime = Funs.GetQuarterlyMonths(this.ddlYearId.SelectedValue, this.ddlQuarter.SelectedValue);
                    DateTime endTime = startTime.AddMonths(3);
                    GetData(startTime, endTime);
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectDrillConductedQuarterlyReportMenuId;
                this.ctlAuditFlow.DataId = this.DrillConductedQuarterlyReportId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
            }
        }
        #endregion

        #region 年季度变化事件
        /// <summary>
        /// 年季度变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlYearId.SelectedValue != BLL.Const._Null && this.ddlQuarter.SelectedValue != BLL.Const._Null)
            {
                DateTime startTime = Funs.GetQuarterlyMonths(this.ddlYearId.SelectedValue, this.ddlQuarter.SelectedValue);
                DateTime endTime = startTime.AddMonths(3);
                GetData(startTime, endTime);
            }
        }
        #endregion

        #region Grid行事件
        /// <summary>
        /// Grid行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string rowID = Grid1.DataKeys[e.RowIndex][0].ToString();
            GetItems(string.Empty);
            if (e.CommandName == "Add")
            {
                Model.InformationProject_DrillConductedQuarterlyReportItem oldItem = items.FirstOrDefault(x => x.DrillConductedQuarterlyReportItemId == rowID);
                Model.InformationProject_DrillConductedQuarterlyReportItem newItem = new Model.InformationProject_DrillConductedQuarterlyReportItem
                {
                    DrillConductedQuarterlyReportItemId = SQLHelper.GetNewID(typeof(Model.InformationProject_DrillConductedQuarterlyReportItem))
                };
                if (oldItem != null)
                {
                    newItem.SortIndex = oldItem.SortIndex + 1;
                }
                else
                {
                    newItem.SortIndex = 0;
                }
                items.Add(newItem);
                items = items.OrderBy(x => x.SortIndex).ToList();
                Grid1.DataSource = items;
                Grid1.DataBind();
            }
            if (e.CommandName == "Delete")
            {
                if (Grid1.Rows.Count == 1)
                {
                    ShowNotify("只有一条数据，无法删除", MessageBoxIcon.Warning);
                    return;
                }
                foreach (var item in items)
                {
                    if (item.DrillConductedQuarterlyReportItemId == rowID)
                    {
                        items.Remove(item);
                        break;
                    }
                }
                Grid1.DataSource = items;
                Grid1.DataBind();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region 获取明细值
        /// <summary>
        /// 获取明细值
        /// </summary>
        /// <param name="drillConductedQuarterlyReportId"></param>
        private void GetItems(string drillConductedQuarterlyReportId)
        {
            items.Clear();
            int i = 10;
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                Model.InformationProject_DrillConductedQuarterlyReportItem item = new Model.InformationProject_DrillConductedQuarterlyReportItem();
                if (values["DrillConductedQuarterlyReportItemId"].ToString() != "")
                {
                    item.DrillConductedQuarterlyReportItemId = values.Value<string>("DrillConductedQuarterlyReportItemId");
                }
                item.DrillConductedQuarterlyReportId = drillConductedQuarterlyReportId;
                item.SortIndex = i;
                if (values["IndustryType"].ToString() != "")
                {
                    item.IndustryType = values.Value<string>("IndustryType");
                }
                if (values["TotalConductCount"].ToString() != "")
                {
                    item.TotalConductCount = values.Value<int>("TotalConductCount");
                }
                if (values["TotalPeopleCount"].ToString() != "")
                {
                    item.TotalPeopleCount = values.Value<int>("TotalPeopleCount");
                }
                if (values["TotalInvestment"].ToString() != "")
                {
                    item.TotalInvestment = values.Value<int>("TotalInvestment");
                }
                if (values["HQConductCount"].ToString() != "")
                {
                    item.HQConductCount = values.Value<int>("HQConductCount");
                }
                if (values["HQPeopleCount"].ToString() != "")
                {
                    item.HQPeopleCount = values.Value<int>("HQPeopleCount");
                }
                if (values["HQInvestment"].ToString() != "")
                {
                    item.HQInvestment = values.Value<int>("HQInvestment");
                }
                if (values["BasicConductCount"].ToString() != "")
                {
                    item.BasicConductCount = values.Value<int>("BasicConductCount");
                }
                if (values["BasicPeopleCount"].ToString() != "")
                {
                    item.BasicPeopleCount = values.Value<int>("BasicPeopleCount");
                }
                if (values["BasicInvestment"].ToString() != "")
                {
                    item.BasicInvestment = values.Value<int>("BasicInvestment");
                }
                if (values["ComprehensivePractice"].ToString() != "")
                {
                    item.ComprehensivePractice = values.Value<int>("ComprehensivePractice");
                }
                if (values["CPScene"].ToString() != "")
                {
                    item.CPScene = values.Value<int>("CPScene");
                }
                if (values["CPDesktop"].ToString() != "")
                {
                    item.CPDesktop = values.Value<int>("CPDesktop");
                }
                if (values["SpecialDrill"].ToString() != "")
                {
                    item.SpecialDrill = values.Value<int>("SpecialDrill");
                }
                if (values["SDScene"].ToString() != "")
                {
                    item.SDScene = values.Value<int>("SDScene");
                }
                if (values["SDDesktop"].ToString() != "")
                {
                    item.SDDesktop = values.Value<int>("SDDesktop");
                }
                items.Add(item);
                i += 10;
            }
        }
        #endregion

        #region 保存、提交
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.ddlYearId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择年度", MessageBoxIcon.Warning);
                return;
            }
            if (this.ddlQuarter.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择季度", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.ddlYearId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择年度", MessageBoxIcon.Warning);
                return;
            }
            if (this.ddlQuarter.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择季度", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="p"></param>
        private void SaveData(string type)
        {
            Model.InformationProject_DrillConductedQuarterlyReport drillConductedQuarterlyReport = new Model.InformationProject_DrillConductedQuarterlyReport
            {
                ProjectId = this.ProjectId,
                UnitId = string.IsNullOrEmpty(this.CurrUser.UnitId) ? Const.UnitId_SEDIN : this.CurrUser.UnitId,
            };

            if (this.ddlYearId.SelectedValue != BLL.Const._Null)
            {
                drillConductedQuarterlyReport.YearId = Funs.GetNewIntOrZero(this.ddlYearId.SelectedValue);
            }
            if (this.ddlQuarter.SelectedValue != BLL.Const._Null)
            {
                drillConductedQuarterlyReport.Quarter = Funs.GetNewIntOrZero(this.ddlQuarter.SelectedValue);
            }
            drillConductedQuarterlyReport.CompileMan = this.CurrUser.UserId;
            drillConductedQuarterlyReport.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
            drillConductedQuarterlyReport.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                drillConductedQuarterlyReport.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.DrillConductedQuarterlyReportId))
            {
                drillConductedQuarterlyReport.DrillConductedQuarterlyReportId = this.DrillConductedQuarterlyReportId;
                BLL.ProjectDrillConductedQuarterlyReportService.UpdateDrillConductedQuarterlyReport(drillConductedQuarterlyReport);
                BLL.LogService.AddSys_Log(this.CurrUser, drillConductedQuarterlyReport.YearId.ToString() + "-" + drillConductedQuarterlyReport.Quarter.ToString(), drillConductedQuarterlyReport.DrillConductedQuarterlyReportId, BLL.Const.ProjectDrillConductedQuarterlyReportMenuId, BLL.Const.BtnModify);
                BLL.ProjectDrillConductedQuarterlyReportItemService.DeleteDrillConductedQuarterlyReportItemList(this.DrillConductedQuarterlyReportId);
            }
            else
            {
                Model.InformationProject_DrillConductedQuarterlyReport oldDrillConductedQuarterlyReport = (from x in new Model.SGGLDB(Funs.ConnString).InformationProject_DrillConductedQuarterlyReport
                                                                                                           where x.ProjectId == drillConductedQuarterlyReport.ProjectId && x.YearId == drillConductedQuarterlyReport.YearId && x.Quarter == drillConductedQuarterlyReport.Quarter
                                                                                                           select x).FirstOrDefault();
                if (oldDrillConductedQuarterlyReport == null)
                {
                    this.DrillConductedQuarterlyReportId = SQLHelper.GetNewID(typeof(Model.InformationProject_DrillConductedQuarterlyReport));
                    drillConductedQuarterlyReport.DrillConductedQuarterlyReportId = this.DrillConductedQuarterlyReportId;
                    BLL.ProjectDrillConductedQuarterlyReportService.AddDrillConductedQuarterlyReport(drillConductedQuarterlyReport);
                    BLL.LogService.AddSys_Log(this.CurrUser, drillConductedQuarterlyReport.YearId.ToString() + "-" + drillConductedQuarterlyReport.Quarter.ToString(), drillConductedQuarterlyReport.DrillConductedQuarterlyReportId, BLL.Const.ProjectDrillConductedQuarterlyReportMenuId, BLL.Const.BtnAdd);
                    ////删除未上报月报信息
                    //Model.ManagementReport_ReportRemind reportRemind = (from x in new Model.SGGLDB(Funs.ConnString).ManagementReport_ReportRemind
                    //                                                    where x.ProjectId == this.ProjectId && x.Year == drillConductedQuarterlyReport.YearId && x.Quarterly == drillConductedQuarterlyReport.Quarter && x.ReportName == "应急演练开展情况季报"
                    //                                                    select x).FirstOrDefault();
                    //if (reportRemind != null)
                    //{
                    //    BLL.ReportRemindService.DeleteReportRemindByReportRemind(reportRemind);
                    //}
                }
                else
                {
                    Alert.ShowInTop("该季度记录已存在", MessageBoxIcon.Warning);
                    return;
                }
            }
            GetItems(this.DrillConductedQuarterlyReportId);
            foreach (var item in items)
            {
                BLL.ProjectDrillConductedQuarterlyReportItemService.AddDrillConductedQuarterlyReportItem(item);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectDrillConductedQuarterlyReportMenuId, this.DrillConductedQuarterlyReportId, (type == BLL.Const.BtnSubmit ? true : false), drillConductedQuarterlyReport.YearId + "-" + drillConductedQuarterlyReport.Quarter, "../InformationProject/DrillConductedQuarterlyReportView.aspx?DrillConductedQuarterlyReportId={0}");
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        private void GetData(DateTime startTime, DateTime endTime)
        {
            items.Clear();
            List<Model.Emergency_DrillRecordList> list = BLL.DrillRecordListService.GetDrillRecordListsByDrillRecordDate(startTime,endTime,this.ProjectId);
            Model.InformationProject_DrillConductedQuarterlyReportItem newItem = new Model.InformationProject_DrillConductedQuarterlyReportItem
            {
                DrillConductedQuarterlyReportItemId = SQLHelper.GetNewID(typeof(Model.InformationProject_DrillConductedQuarterlyReportItem)),
                SortIndex = 10
            };
            if (list.Count > 0)
            {
                newItem.TotalConductCount = list.Count();
                newItem.TotalPeopleCount = list.Sum(x => x.JointPersonNum ?? 0);
                newItem.TotalInvestment = list.Sum(x => x.DrillCost ?? 0);
                newItem.BasicConductCount = list.Count();
                newItem.BasicPeopleCount = list.Sum(x => x.JointPersonNum ?? 0);
                newItem.BasicInvestment = list.Sum(x => x.DrillCost ?? 0);
                newItem.ComprehensivePractice = list.Count(x => x.DrillRecordType == "1" || x.DrillRecordType == "2");
                newItem.CPScene = list.Count(x => x.DrillRecordType == "1");
                newItem.CPDesktop = list.Count(x => x.DrillRecordType == "2");
                newItem.SpecialDrill = list.Count(x => x.DrillRecordType == "3" || x.DrillRecordType == "4");
                newItem.SDScene = list.Count(x => x.DrillRecordType == "3");
                newItem.SDDesktop = list.Count(x => x.DrillRecordType == "4");
            }
            items.Add(newItem);
            Grid1.DataSource = items;
            Grid1.DataBind();
        }
        #endregion
    }
}