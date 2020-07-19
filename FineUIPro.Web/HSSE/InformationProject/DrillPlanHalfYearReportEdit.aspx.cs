using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.InformationProject
{
    public partial class DrillPlanHalfYearReportEdit : PageBase
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
                items.Clear();
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.CurrUser.LoginProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }

                BLL.ConstValue.InitConstValueDropDownList(this.ddlYearId, BLL.ConstValue.Group_0008, true);
                BLL.ConstValue.InitConstValueDropDownList(this.ddlHalfYearId, BLL.ConstValue.Group_0010, true);

                this.DrillPlanHalfYearReportId = Request.Params["DrillPlanHalfYearReportId"];
                if (!string.IsNullOrEmpty(this.DrillPlanHalfYearReportId))
                {
                    items = BLL.ProjectDrillPlanHalfYearReportItemService.GetDrillPlanHalfYearReportItemList(this.DrillPlanHalfYearReportId);
                    int i = items.Count * 10;
                    int count = items.Count;
                    if (count < 10)
                    {
                        for (int j = 0; j < (10 - count); j++)
                        {
                            i += 10;
                            Model.InformationProject_DrillPlanHalfYearReportItem newItem = new Model.InformationProject_DrillPlanHalfYearReportItem
                            {
                                DrillPlanHalfYearReportItemId = SQLHelper.GetNewID(typeof(Model.InformationProject_DrillPlanHalfYearReportItem)),
                                SortIndex = i
                            };
                            items.Add(newItem);
                        }
                    }
                    this.Grid1.DataSource = items;
                    this.Grid1.DataBind();

                    var drill = BLL.ProjectDrillPlanHalfYearReportService.GetDrillPlanHalfYearReportById(this.DrillPlanHalfYearReportId);
                    if (drill != null)
                    {
                        this.ProjectId = drill.ProjectId;
                        if (drill.YearId.HasValue)
                        {
                            this.ddlYearId.SelectedValue = drill.YearId.ToString();
                        }
                        if (drill.HalfYearId.HasValue)
                        {
                            this.ddlHalfYearId.SelectedValue = drill.HalfYearId.ToString();
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
                else
                {
                    this.ddlYearId.SelectedValue = DateTime.Now.Year.ToString();
                    this.ddlHalfYearId.SelectedValue = Funs.GetNowHalfYearByTime(DateTime.Now).ToString();
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    //增加明细集合                 
                    for (int i = 0; i < 100; i++)
                    {
                        i += 10;
                        Model.InformationProject_DrillPlanHalfYearReportItem newItem = new Model.InformationProject_DrillPlanHalfYearReportItem
                        {
                            DrillPlanHalfYearReportItemId = SQLHelper.GetNewID(typeof(Model.InformationProject_DrillPlanHalfYearReportItem)),
                            SortIndex = i
                        };
                        items.Add(newItem);
                    }
                    Grid1.DataSource = items;
                    Grid1.DataBind();
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectDrillPlanHalfYearReportMenuId;
                this.ctlAuditFlow.DataId = this.DrillPlanHalfYearReportId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.txtprojectName.Text = BLL.ProjectService.GetProjectNameByProjectId(this.ProjectId);
            }
        }
        #endregion

        #region Grid1行点击事件
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
                Model.InformationProject_DrillPlanHalfYearReportItem oldItem = items.FirstOrDefault(x => x.DrillPlanHalfYearReportItemId == rowID);
                Model.InformationProject_DrillPlanHalfYearReportItem newItem = new Model.InformationProject_DrillPlanHalfYearReportItem
                {
                    DrillPlanHalfYearReportItemId = SQLHelper.GetNewID(typeof(Model.InformationProject_DrillPlanHalfYearReportItem))
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
                foreach (var item in items)
                {
                    if (item.DrillPlanHalfYearReportItemId == rowID)
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

        #region 获取明细
        private void GetItems(string drillPlanHalfYearReportId)
        {
            items.Clear();
            int i = 10;
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                Model.InformationProject_DrillPlanHalfYearReportItem item = new Model.InformationProject_DrillPlanHalfYearReportItem();
                if (values["DrillPlanHalfYearReportItemId"].ToString() != "")
                {
                    item.DrillPlanHalfYearReportItemId = values.Value<string>("DrillPlanHalfYearReportItemId");
                }
                item.DrillPlanHalfYearReportId = drillPlanHalfYearReportId;
                item.SortIndex = i;
                if (values["DrillPlanName"].ToString() != "")
                {
                    item.DrillPlanName = values.Value<string>("DrillPlanName");
                }
                if (values["OrganizationUnit"].ToString() != "")
                {
                    item.OrganizationUnit = values.Value<string>("OrganizationUnit");
                }
                if (values["DrillPlanDate"].ToString() != "")
                {
                    item.DrillPlanDate = values.Value<string>("DrillPlanDate");
                }
                if (values["AccidentScene"].ToString() != "")
                {
                    item.AccidentScene = values.Value<string>("AccidentScene");
                }
                if (values["ExerciseWay"].ToString() != "")
                {
                    item.ExerciseWay = values.Value<string>("ExerciseWay");
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
            if (this.ddlHalfYearId.SelectedValue == BLL.Const._Null)
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
            if (this.ddlHalfYearId.SelectedValue == BLL.Const._Null)
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
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.InformationProject_DrillPlanHalfYearReport drillPlanHalfYearReport = new Model.InformationProject_DrillPlanHalfYearReport
            {
                ProjectId = this.ProjectId,
                UnitId = string.IsNullOrEmpty(this.CurrUser.UnitId) ? Const.UnitId_SEDIN : this.CurrUser.UnitId,
            };

            if (this.ddlYearId.SelectedValue != BLL.Const._Null)
            {
                drillPlanHalfYearReport.YearId = Funs.GetNewIntOrZero(this.ddlYearId.SelectedValue);
            }
            if (this.ddlHalfYearId.SelectedValue != BLL.Const._Null)
            {
                drillPlanHalfYearReport.HalfYearId = Funs.GetNewIntOrZero(this.ddlHalfYearId.SelectedValue);
            }
            drillPlanHalfYearReport.Telephone = this.txtTel.Text.Trim();
            drillPlanHalfYearReport.CompileMan = this.txtCompileMan.Text.Trim();
            drillPlanHalfYearReport.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
            drillPlanHalfYearReport.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                drillPlanHalfYearReport.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.DrillPlanHalfYearReportId))
            {
                drillPlanHalfYearReport.DrillPlanHalfYearReportId = this.DrillPlanHalfYearReportId;
                BLL.ProjectDrillPlanHalfYearReportService.UpdateDrillPlanHalfYearReport(drillPlanHalfYearReport);
                BLL.LogService.AddSys_Log(this.CurrUser, drillPlanHalfYearReport.YearId.ToString() + "-" + drillPlanHalfYearReport.HalfYearId.ToString(), drillPlanHalfYearReport.DrillPlanHalfYearReportId, BLL.Const.ProjectDrillPlanHalfYearReportMenuId, BLL.Const.BtnModify);
                BLL.ProjectDrillPlanHalfYearReportItemService.DeleteDrillPlanHalfYearReportItemList(this.DrillPlanHalfYearReportId);
            }
            else
            {
                Model.InformationProject_DrillPlanHalfYearReport oldDrillPlanHalfYearReport = (from x in Funs.DB.InformationProject_DrillPlanHalfYearReport
                                                                                               where x.ProjectId == drillPlanHalfYearReport.ProjectId && x.YearId == drillPlanHalfYearReport.YearId && x.HalfYearId == drillPlanHalfYearReport.HalfYearId
                                                                                               select x).FirstOrDefault();
                if (oldDrillPlanHalfYearReport == null)
                {
                    this.DrillPlanHalfYearReportId = SQLHelper.GetNewID(typeof(Model.InformationProject_DrillPlanHalfYearReport));
                    drillPlanHalfYearReport.DrillPlanHalfYearReportId = this.DrillPlanHalfYearReportId;
                    BLL.ProjectDrillPlanHalfYearReportService.AddDrillPlanHalfYearReport(drillPlanHalfYearReport);
                    BLL.LogService.AddSys_Log(this.CurrUser, drillPlanHalfYearReport.YearId.ToString() + "-" + drillPlanHalfYearReport.HalfYearId.ToString(), drillPlanHalfYearReport.DrillPlanHalfYearReportId, BLL.Const.ProjectDrillPlanHalfYearReportMenuId, BLL.Const.BtnAdd);
                    ////删除未上报月报信息
                    //Model.ManagementReport_ReportRemind reportRemind = (from x in Funs.DB.ManagementReport_ReportRemind
                    //                                                    where x.ProjectId == this.ProjectId && x.Year == drillPlanHalfYearReport.YearId && x.HalfYear == drillPlanHalfYearReport.HalfYearId && x.ReportName == "应急演练工作计划半年报"
                    //                                                    select x).FirstOrDefault();
                    //if (reportRemind != null)
                    //{
                    //    BLL.ReportRemindService.DeleteReportRemindByReportRemind(reportRemind);
                    //}
                }
                else
                {
                    Alert.ShowInTop("该半年记录已存在", MessageBoxIcon.Warning);
                    return;
                }
            }
            GetItems(drillPlanHalfYearReport.DrillPlanHalfYearReportId);
            foreach (var item in items)
            {
                if (!string.IsNullOrEmpty(item.DrillPlanName))
                {
                    BLL.ProjectDrillPlanHalfYearReportItemService.AddDrillPlanHalfYearReportItem(item);
                }
            }

            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectDrillPlanHalfYearReportMenuId, this.DrillPlanHalfYearReportId, (type == BLL.Const.BtnSubmit ? true : false), drillPlanHalfYearReport.YearId + "-" + drillPlanHalfYearReport.HalfYearId, "../InformationProject/DrillPlanHalfYearReportView.aspx?DrillPlanHalfYearReportId={0}");
        }
        #endregion
    }
}