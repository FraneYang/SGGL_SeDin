using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace FineUIPro.Web.HSSE.CostGoods
{
    public partial class CostManageEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string CostManageId
        {
            get
            {
                return (string)ViewState["CostManageId"];
            }
            set
            {
                ViewState["CostManageId"] = value;
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
        private static List<Model.CostGoods_CostManageItem> costManageItems = new List<Model.CostGoods_CostManageItem>();
        #endregion

        #region 加载
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Funs.DropDownPageSize(this.ddlPageSize);
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.InitDropDownList();
                this.CostManageId = Request.Params["CostManageId"];
                this.drpInvestCostProject.DataTextField = "Text";
                this.drpInvestCostProject.DataValueField = "Value";
                this.drpInvestCostProject.DataSource = BLL.CostManageItemService.GetInvestCostProjectList();
                this.drpInvestCostProject.DataBind();
                if (!BLL.CommonService.IsMainUnitOrAdmin(this.CurrUser.UserId))   //分包单位用户，隐藏审核列
                {
                    this.Grid1.Columns[7].Hidden = true;
                    this.Grid1.Columns[8].Hidden = true;
                    this.Grid1.Columns[9].Hidden = true;
                }
                this.txtCostManageDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
                if (!string.IsNullOrEmpty(this.CostManageId))
                {
                    Model.CostGoods_CostManage costManage = BLL.CostManageService.GetCostManageById(this.CostManageId);
                    if (costManage != null)
                    {
                        this.ProjectId = costManage.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtCostManageCode.Text = CodeRecordsService.ReturnCodeByDataId(this.CostManageId);
                        this.txtCostManageName.Text = costManage.CostManageName;
                        if (!string.IsNullOrEmpty(costManage.UnitId))
                        {
                            this.drpUnitId.SelectedValue = costManage.UnitId;
                        }
                        this.txtContractNum.Text = costManage.ContractNum;
                        this.txtCostManageDate.Text = string.Format("{0:yyyy-MM-dd}", costManage.CostManageDate);
                        this.txtOpinion.Text = costManage.Opinion;
                        this.txtSubHSE.Text = costManage.SubHSE;
                        this.txtSubCN.Text = costManage.SubCN;
                        this.txtSubProject.Text = costManage.SubProject;
                    }
                    BindGrid();

                }
                else
                {
                    this.txtCostManageCode.Text = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectCostManageMenuId, this.ProjectId, this.CurrUser.UserId);
                    this.txtCostManageName.Text = "分包商HSE措施费申请表";
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectCostManageMenuId;
                this.ctlAuditFlow.DataId = this.CostManageId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
            else
            {
                if (GetRequestEventArgument() == "UPDATE_SUMMARY")
                {
                    jerqueSaveMonthPlanList();
                    this.Grid1.DataSource = costManageItems;
                    this.Grid1.DataBind();
                    // 页面要求重新计算合计行的值
                    OutputSummaryData();
                }
            }
        }

        #region 计算合计及各行总价
        /// <summary>
        /// 计算合计
        /// </summary>
        private void OutputSummaryData()
        {
            Grid1.CommitChanges();
            decimal sumTotalMoney = 0, sumAuditTotalMoney = 0, totalMoney = 0, auditTotalMoney = 0, priceMoney = 0, auditPriceMoney = 0;
            int counts = 0, auditCounts = 0;
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                counts = Funs.GetNewIntOrZero(this.Grid1.Rows[i].Values[4].ToString());
                priceMoney = Funs.GetNewDecimalOrZero(this.Grid1.Rows[i].Values[5].ToString());
                totalMoney = counts * priceMoney;
                sumTotalMoney += totalMoney;
                this.Grid1.Rows[i].Values[6] = totalMoney.ToString();
                auditCounts = Funs.GetNewIntOrZero(this.Grid1.Rows[i].Values[7].ToString());
                auditPriceMoney = Funs.GetNewDecimalOrZero(this.Grid1.Rows[i].Values[8].ToString());
                auditTotalMoney = auditCounts * auditPriceMoney;
                sumAuditTotalMoney += auditTotalMoney;
                this.Grid1.Rows[i].Values[9] = auditTotalMoney.ToString();
            }
            if (this.Grid1.Rows.Count > 0)
            {
                JObject summary = new JObject
                {
                    { "PriceMoney", "总计" },
                    { "TotalMoney", sumTotalMoney },
                    { "AuditTotalMoney", sumAuditTotalMoney }
                };
                Grid1.SummaryData = summary;
            }
            else
            {
                Grid1.SummaryData = null;
            }
        }
        #endregion

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            BLL.UnitService.InitUnitDropDownList(this.drpUnitId, this.ProjectId, true);
        }

        /// <summary>
        /// 绑定Grid
        /// </summary>
        private void BindGrid()
        {
            costManageItems = BLL.CostManageItemService.GetCostManageItemByCostManageId(this.CostManageId);
            this.Grid1.DataSource = costManageItems;
            this.Grid1.PageIndex = 0;
            this.Grid1.DataBind();
            OutputSummaryData();
        }

        /// <summary>
        /// 改变索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// 分页下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.drpUnitId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择分包商！", MessageBoxIcon.Warning);
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
            if (this.drpUnitId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择分包商！", MessageBoxIcon.Warning);
                return;
            }
            if (this.ctlAuditFlow.NextStep == BLL.Const.State_1 && this.ctlAuditFlow.NextPerson == BLL.Const._Null)
            {
                ShowNotify("请选择下一步办理人！", MessageBoxIcon.Warning);
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
            if (!BLL.CommonService.IsMainUnitOrAdmin(this.CurrUser.UserId) && type == BLL.Const.BtnSubmit && this.ctlAuditFlow.NextStep == BLL.Const.State_2)
            {
                Alert.ShowInParent("分包商不能关闭审核流程", MessageBoxIcon.Warning);
                return;
            }

            Model.CostGoods_CostManage costManage = new Model.CostGoods_CostManage
            {
                ProjectId = this.ProjectId,
                CostManageCode = this.txtCostManageCode.Text.Trim(),
                CostManageName = this.txtCostManageName.Text.Trim()
            };
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                costManage.UnitId = this.drpUnitId.SelectedValue;
            }
            costManage.ContractNum = this.txtContractNum.Text.Trim();
            costManage.CostManageDate = Funs.GetNewDateTime(this.txtCostManageDate.Text.Trim());
            costManage.Opinion = this.txtOpinion.Text.Trim();
            costManage.SubHSE = this.txtSubHSE.Text.Trim();
            costManage.SubCN = this.txtSubCN.Text.Trim();
            costManage.SubProject = this.txtSubProject.Text.Trim();
            costManage.CompileMan = this.CurrUser.UserId;
            costManage.CompileDate = DateTime.Now;
            costManage.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                costManage.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.CostManageId))
            {
                costManage.CostManageId = this.CostManageId;
                BLL.CostManageService.UpdateCostManage(costManage);
                BLL.LogService.AddSys_Log(this.CurrUser, costManage.CostManageCode, costManage.CostManageId, BLL.Const.ProjectCostManageMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.CostManageId = SQLHelper.GetNewID(typeof(Model.CostGoods_CostManage));
                costManage.CostManageId = this.CostManageId;
                BLL.CostManageService.AddCostManage(costManage);
                BLL.LogService.AddSys_Log(this.CurrUser, costManage.CostManageCode, costManage.CostManageId, BLL.Const.ProjectCostManageMenuId, BLL.Const.BtnAdd);
            }
            jerqueSaveMonthPlanList();
            BLL.CostManageItemService.DeleteCostManageItemByCostManageId(this.CostManageId);
            foreach (var costManageItem in costManageItems)
            {
                costManageItem.CostManageId = this.CostManageId;
                BLL.CostManageItemService.AddCostManageItem(costManageItem);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectCostManageMenuId, this.CostManageId, (type == BLL.Const.BtnSubmit ? true : false), costManage.CostManageName, "../CostGoods/CostManageView.aspx?CostManageId={0}");
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.CostManageId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CostManageAttachUrl&menuId={1}", this.CostManageId, BLL.Const.ProjectCostManageMenuId)));
        }
        #endregion

        #region 新增费用申请情况
        /// <summary>
        /// 新增费用申请情况
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            //if (this.drpUnitId.SelectedValue == BLL.Const._Null)
            //{
            //    Alert.ShowInTop("请选择分包商！", MessageBoxIcon.Warning);
            //    return;
            //}
            jerqueSaveMonthPlanList();
            Model.CostGoods_CostManageItem costManageItem = new Model.CostGoods_CostManageItem
            {
                CostManageItemId = SQLHelper.GetNewID(typeof(Model.CostGoods_CostManageItem))
            };
            costManageItems.Add(costManageItem);
            this.Grid1.DataSource = costManageItems;
            this.Grid1.DataBind();
            OutputSummaryData();
        }

        /// <summary>
        /// 检查并保存其他HSE管理活动集合
        /// </summary>
        private void jerqueSaveMonthPlanList()
        {
            costManageItems.Clear();
            JArray mergedData = Grid1.GetMergedData();
            foreach (JObject mergedRow in mergedData)
            {
                string status = mergedRow.Value<string>("status");
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                Model.CostGoods_CostManageItem costManageItem = new Model.CostGoods_CostManageItem
                {
                    CostManageItemId = this.Grid1.Rows[i].DataKeys[0].ToString(),
                    InvestCostProject = values.Value<string>("InvestCostProject").ToString(),
                    UseReason = values.Value<string>("UseReason").ToString(),
                    Counts = Funs.GetNewIntOrZero(values.Value<string>("Counts").ToString()),
                    PriceMoney = Funs.GetNewDecimalOrZero(values.Value<string>("PriceMoney").ToString()),
                    AuditCounts = !string.IsNullOrEmpty(values.Value<string>("AuditCounts").ToString()) ? Funs.GetNewInt(values.Value<string>("AuditCounts").ToString()) : null,
                    AuditPriceMoney = !string.IsNullOrEmpty(values.Value<string>("AuditPriceMoney").ToString()) ? Funs.GetNewDecimal(values.Value<string>("AuditPriceMoney").ToString()) : null
                };
                costManageItems.Add(costManageItem);
            }
        }

        protected void gvMonthPlan_RowCommand(object sender, GridCommandEventArgs e)
        {
            jerqueSaveMonthPlanList();
            string rowID = this.Grid1.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "Delete")
            {
                foreach (var item in costManageItems)
                {
                    if (item.CostManageItemId == rowID)
                    {
                        costManageItems.Remove(item);
                        break;
                    }
                }
                Grid1.DataSource = costManageItems;
                Grid1.DataBind();
                OutputSummaryData();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 获取总价
        /// </summary>
        /// <param name="costManageId"></param>
        /// <returns></returns>
        protected string GetTotalMoney(object costManageItemId)
        {
            string total = string.Empty;
            if (costManageItemId != null)
            {
                var costManageItem = BLL.CostManageItemService.GetCostManageItemById(costManageItemId.ToString());
                if (costManageItem != null)
                {
                    decimal? price = costManageItem.PriceMoney;
                    int? count = costManageItem.Counts;
                    total = Convert.ToString(price * count);
                }
            }
            return total;
        }
        /// <summary>
        /// 获取审核总价
        /// </summary>
        /// <param name="costManageId"></param>
        /// <returns></returns>
        protected string GetAuditTotalMoney(object costManageItemId)
        {
            string total = string.Empty;
            if (costManageItemId != null)
            {
                var costManageItem = BLL.CostManageItemService.GetCostManageItemById(costManageItemId.ToString());
                if (costManageItem != null)
                {
                    decimal? price = costManageItem.AuditPriceMoney;
                    int? count = costManageItem.AuditCounts;
                    total = Convert.ToString(price * count);
                }
            }
            return total;
        }
        #endregion
    }
}