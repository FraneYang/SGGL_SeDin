using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BLL;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.HJGL.HotProcessHard
{
    public partial class HardTrustEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 硬度委托主键
        /// </summary>
        public string HardTrustID
        {
            get
            {
                return (string)ViewState["HardTrustID"];
            }
            set
            {
                ViewState["HardTrustID"] = value;
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.HardTrustID = Request.Params["HardTrustID"];
                ///委托人
                this.drpHardTrustMan.DataValueField = "UserId";
                this.drpHardTrustMan.DataTextField = "UserName";
                this.drpHardTrustMan.DataSource = from x in Funs.DB.Sys_User
                                                  join y in Funs.DB.Project_ProjectUser
                                                  on x.UserId equals y.UserId
                                                  where y.ProjectId == this.ProjectId
                                                  select x;
                this.drpHardTrustMan.DataBind();
                Funs.FineUIPleaseSelect(this.drpHardTrustMan, "请选择");
                List<Model.View_HJGL_Hard_TrustItem> GetHardTrustItem = BLL.Hard_TrustService.GetHardTrustItem(this.HardTrustID);
                this.BindGrid(GetHardTrustItem);  // 初始化页面 
                this.PageInfoLoad(); // 加载页面 
            }
        }
        #endregion

        #region 加载页面输入提交信息
        /// <summary>
        /// 加载页面输入提交信息
        /// </summary>
        private void PageInfoLoad()
        {
            var trust = BLL.Hard_TrustService.GetHardTrustById(this.HardTrustID);
            BLL.UnitService.InitUnitByProjectIdUnitTypeDropDownList(this.drpHardTrustUnit, this.ProjectId, BLL.Const.ProjectUnitType_2, true);
            BLL.UnitService.InitUnitByProjectIdUnitTypeDropDownList(this.drpCheckUnit, this.ProjectId, BLL.Const.ProjectUnitType_5, true);
            BLL.UnitWorkService.InitUnitWorkDownList(this.drpUnitWork, this.ProjectId,true);
            BLL.UserService.InitUserProjectIdUnitTypeDropDownList(drpSendee, this.CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_5, true);
            if (trust != null)
            {
                this.txtHardTrustNo.Text = trust.HardTrustNo;
                if (!string.IsNullOrEmpty(trust.HardTrustUnit))
                {
                    this.drpHardTrustUnit.SelectedValue = trust.HardTrustUnit;
                }
                if (!string.IsNullOrEmpty(trust.UnitWorkId))
                {
                    this.drpUnitWork.SelectedValue = trust.UnitWorkId;
                }
                if (!string.IsNullOrEmpty(trust.CheckUnit))
                {
                    this.drpCheckUnit.SelectedValue = trust.CheckUnit;
                }
                if (!string.IsNullOrEmpty(trust.HardTrustMan))
                {
                    this.drpHardTrustMan.SelectedValue = trust.HardTrustMan;
                }
                if (trust.HardTrustDate != null)
                {
                    this.txtHardTrustDate.Text = string.Format("{0:yyyy-MM-dd}", trust.HardTrustDate);
                }
                this.txtHardnessRate.Text = trust.HardnessRate;
                this.txtStandards.Text = trust.Standards;
                this.txtCheckNum.Text = trust.CheckNum;
                this.txtTestWeldNum.Text = trust.TestWeldNum;
                this.rblDetectionTime.SelectedValue = trust.DetectionTime;
                if (!string.IsNullOrEmpty(trust.Sendee)) {
                    drpSendee.SelectedValue = trust.Sendee;
                }
                this.txtCheckName.Text = trust.CheckName;
                this.txtAcceptStandard.Text = trust.AcceptStandard;
            }
            else
            {
                string unitWorkId = Request.Params["unitWorkId"];

                if (!string.IsNullOrEmpty(unitWorkId))
                {
                    var w = BLL.UnitWorkService.getUnitWorkByUnitWorkId(unitWorkId);
                    if (w.UnitId != null)
                    {
                        drpHardTrustUnit.SelectedValue = w.UnitId;
                    }
                    if (w.NDEUnit != null)
                    {
                        drpCheckUnit.SelectedValue = w.NDEUnit;
                    }
                    this.drpUnitWork.SelectedValue = w.UnitWorkId;
                }

                this.SimpleForm1.Reset(); ///重置所有字段
                this.txtHardTrustDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
                this.drpHardTrustMan.SelectedValue = this.CurrUser.UserId;
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid(List<Model.View_HJGL_Hard_TrustItem> GetHardTrustItem)
        {
            DataTable tb = this.LINQToDataTable(GetHardTrustItem);
            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(GridNewDynamic, tb1);
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region 排序
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            List<Model.View_HJGL_Hard_TrustItem> GetHardTrustItem = this.CollectGridJointInfo();
            this.BindGrid(GetHardTrustItem);
        }
        #endregion

        #region 硬度委托 提交事件
        /// <summary>
        /// 编辑硬度委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.ProjectId, this.CurrUser.UserId, Const.HJGL_HotHardManageEditMenuId, Const.BtnSave))
            {
                if (BLL.Hard_TrustService.IsExistTrustCode(this.txtHardTrustNo.Text, !string.IsNullOrEmpty(this.HardTrustID) ? this.HardTrustID : "", this.ProjectId))
                {
                    ShowNotify("委托单号已存在，请重新录入!", MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(this.txtHardTrustDate.Text) || string.IsNullOrEmpty(this.txtHardTrustNo.Text.Trim()))
                {
                    ShowNotify("委托单号、委托日期不能为空", MessageBoxIcon.Warning);
                    return;
                }

                string unitWorkId =drpUnitWork.SelectedValue;
                Model.HJGL_Hard_Trust newHardTrust = new Model.HJGL_Hard_Trust();
                newHardTrust.HardTrustNo = this.txtHardTrustNo.Text.Trim();
                newHardTrust.ProjectId = this.ProjectId;
                if (this.drpHardTrustUnit.SelectedValue != BLL.Const._Null)
                {
                    newHardTrust.HardTrustUnit = this.drpHardTrustUnit.SelectedValue;
                }

                newHardTrust.UnitWorkId = unitWorkId;

                if (this.drpCheckUnit.SelectedValue != BLL.Const._Null)
                {
                    newHardTrust.CheckUnit = this.drpCheckUnit.SelectedValue;
                }
                if (this.drpHardTrustMan.SelectedValue != BLL.Const._Null)
                {
                    newHardTrust.HardTrustMan = this.drpHardTrustMan.SelectedValue;
                }
                newHardTrust.HardTrustDate = Funs.GetNewDateTime(this.txtHardTrustDate.Text);
                newHardTrust.HardnessRate = this.txtHardnessRate.Text.Trim();
                newHardTrust.Standards = this.txtStandards.Text.Trim();
                newHardTrust.CheckNum = this.txtCheckNum.Text.Trim();
                newHardTrust.TestWeldNum = this.txtTestWeldNum.Text.Trim();
                newHardTrust.DetectionTime = this.rblDetectionTime.SelectedValue;
                if (drpSendee.SelectedValue != BLL.Const._Null)
                {
                    newHardTrust.Sendee = drpSendee.SelectedValue;
                }
                newHardTrust.CheckName = this.txtCheckName.Text.Trim();
                newHardTrust.AcceptStandard = this.txtAcceptStandard.Text.Trim();
                if (!string.IsNullOrEmpty(this.HardTrustID))
                {
                    newHardTrust.HardTrustID = this.HardTrustID;
                    BLL.Hard_TrustService.UpdateHardTrust(newHardTrust);
                    //BLL.Sys_LogService.AddLog(BLL.Const.System_3, this.ProjectId, this.CurrUser.UserId, Resources.Lan.ModifyHardTrust);
                }
                else
                {
                    this.HardTrustID = SQLHelper.GetNewID(typeof(Model.HJGL_Hard_Trust));
                    newHardTrust.HardTrustID = this.HardTrustID;
                    BLL.Hard_TrustService.AddHardTrust(newHardTrust);
                    //BLL.Sys_LogService.AddLog(BLL.Const.System_3, this.ProjectId, this.CurrUser.UserId, Resources.Lan.AddHardTrust);
                }

                List<Model.HJGL_Hard_TrustItem> GetHardTrustItem = this.SaveGrid();
                string errlog = string.Empty;
                //BLL.Hard_TrustItemService.DeleteHardTrustItemById(this.HardTrustID);
                var hardTrustItems = from x in Funs.DB.HJGL_Hard_TrustItem select x;
                foreach (var item in GetHardTrustItem)
                {
                    var oldHardTrustItem = hardTrustItems.FirstOrDefault(x=>x.WeldJointId == item.WeldJointId);
                    if (oldHardTrustItem == null)
                    {
                        Model.HJGL_Hard_TrustItem trustItem = new Model.HJGL_Hard_TrustItem();
                        trustItem.HardTrustItemID = SQLHelper.GetNewID(typeof(Model.HJGL_Hard_TrustItem));
                        trustItem.HardTrustID = this.HardTrustID;
                        trustItem.HotProessTrustItemId = item.HotProessTrustItemId;
                        trustItem.WeldJointId = item.WeldJointId;
                        BLL.Hard_TrustItemService.AddHardTrustItem(trustItem);
                    }
                    //更新热处理委托明细的口已做硬度委托
                    Model.HJGL_HotProess_TrustItem hotProessTrustItem = BLL.HotProessTrustItemService.GetHotProessTrustItemById(item.HotProessTrustItemId);
                    if (hotProessTrustItem != null)
                    {
                        hotProessTrustItem.IsTrust = true;
                        BLL.HotProessTrustItemService.UpdateHotProessTrustItem(hotProessTrustItem);
                    }
                }
                if (string.IsNullOrEmpty(errlog))
                {
                    ShowNotify("保存成功！", MessageBoxIcon.Success);
                    PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
                }
                else
                {
                    // string okj = ActiveWindow.GetWriteBackValueReference(newWeldReportMain.HardTrustID) + ActiveWindow.GetHidePostBackReference();
                    Alert.ShowInTop("保存成功！" + "焊接明细中" + errlog, "提交结果", MessageBoxIcon.Warning);
                    // ShowAlert("焊接明细中" + errlog, MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }
        #endregion

        #region 收集Grid页面信息
        private List<Model.HJGL_Hard_TrustItem> SaveGrid()
        {
            JArray mergedData = Grid1.GetMergedData();
            List<Model.HJGL_Hard_TrustItem> GetHardTrustItem = new List<Model.HJGL_Hard_TrustItem>();
            var items = from x in Funs.DB.HJGL_Hard_TrustItem select x;
            foreach (JObject mergedRow in mergedData)
            {
                JObject values = mergedRow.Value<JObject>("values");

                Model.HJGL_Hard_TrustItem newTrustItem = new Model.HJGL_Hard_TrustItem();
                newTrustItem.HardTrustItemID = values.Value<string>("HardTrustItemID").ToString();
                newTrustItem.HotProessTrustItemId = values.Value<string>("HotProessTrustItemId").ToString();
                newTrustItem.WeldJointId = values.Value<string>("WeldJointId").ToString();
                var item = items.FirstOrDefault(x => x.HardTrustItemID == newTrustItem.HardTrustItemID);
                if (item == null)
                {
                    GetHardTrustItem.Add(newTrustItem);
                }
            }
            return GetHardTrustItem;
        }

        /// <summary>
        /// 收集Grid页面信息
        /// </summary>
        /// <returns></returns>
        private List<Model.View_HJGL_Hard_TrustItem> CollectGridJointInfo()
        {
            List<Model.View_HJGL_Hard_TrustItem> GetHardTrustItem = null;
            if (!string.IsNullOrEmpty(this.hdItemsString.Text))
            {
                GetHardTrustItem = BLL.Hard_TrustService.GetHardTrustAddItem(this.hdItemsString.Text);
            }
            else if (string.IsNullOrEmpty(this.hdItemsString.Text) && this.HardTrustID != null)
            {
                GetHardTrustItem = BLL.Hard_TrustService.GetHardTrustItem(this.HardTrustID);
            }
            return GetHardTrustItem;
        }
        #endregion

        #region Grid 关闭弹出窗口事件
        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            List<Model.View_HJGL_Hard_TrustItem> GetHardTrustItem = BLL.Hard_TrustService.GetHardTrustAddItem(this.hdItemsString.Text);
            this.BindGrid(GetHardTrustItem);
            //SetDrpByDrpUnitChange();             
            //this.hdItemsString.Text = string.Empty;
        }
        #endregion

        #region 右键删除事件
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                List<Model.View_HJGL_Hard_TrustItem> GetHardTrustItem = this.CollectGridJointInfo();
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var item = GetHardTrustItem.FirstOrDefault(x => x.WeldJointId == rowID);
                    if (item != null)
                    {
                        if (item.IsPass != null)
                        {
                            ShowNotify("已生成硬度报告，不能删除！", MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            GetHardTrustItem.Remove(item);
                            //更新热处理委托明细的口已做硬度委托
                            Model.HJGL_HotProess_TrustItem hotProessTrustItem = BLL.HotProessTrustItemService.GetHotProessTrustItemById(item.HotProessTrustItemId);
                            if (hotProessTrustItem != null)
                            {
                                hotProessTrustItem.IsTrust = null;
                                BLL.HotProessTrustItemService.UpdateHotProessTrustItem(hotProessTrustItem);
                            }
                            BLL.Hard_TrustItemService.DeleteHardTrustItemByHardTrustItemID(item.HardTrustItemID);
                        }
                    }
                }

                BindGrid(GetHardTrustItem);
                ShowNotify("删除成功！", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region 查找
        /// <summary>
        /// 查找未焊接焊口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckSelect_Click(object sender, EventArgs e)
        {
            string weldJointIds = string.Empty;

            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                string Id = Grid1.DataKeys[i][0].ToString() + "," + Grid1.DataKeys[i][1].ToString();
                weldJointIds = weldJointIds + Id + "|";
            }

            if (weldJointIds != string.Empty)
            {
                weldJointIds = weldJointIds.Substring(0, weldJointIds.Length - 1);
            }

            if (!string.IsNullOrEmpty(this.drpHardTrustUnit.SelectedValue) && this.drpHardTrustUnit.SelectedValue != BLL.Const._Null && !string.IsNullOrEmpty(this.drpUnitWork.SelectedValue) && this.drpUnitWork.SelectedValue != BLL.Const._Null)
            {
                string strList = this.drpUnitWork.SelectedValue + "|" + this.drpHardTrustUnit.SelectedValue + "|" + this.HardTrustID;
                string window = String.Format("HardTrustItemEdit.aspx?strList={0}&weldJointIds={1}", strList, weldJointIds, "编辑 - ");
                PageContext.RegisterStartupScript(Window1.GetSaveStateReference(hdItemsString.ClientID) + Window1.GetShowReference(window));
            }
            else
            {
                Alert.ShowInTop("请选择单位和单位工程", MessageBoxIcon.Warning);
            }
        }
        #endregion

        protected void drpCheckUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpCheckUnit.SelectedValue != BLL.Const._Null) {
                this.drpSendee.Items.Clear();
                BLL.UserService.InitUserProjectIdUnitIdDropDownList(drpSendee, this.CurrUser.LoginProjectId, drpCheckUnit.SelectedValue, true);
            }
        }
    }
}