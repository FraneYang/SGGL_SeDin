﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;
using System.Web;
using System.Collections;

namespace FineUIPro.Web.HJGL.NDT
{
    public partial class NDTBatch : PageBase
    {
        #region 定义项
        /// <summary>
        /// 委托单主键
        /// </summary>
        public string TrustBatchId
        {
            get
            {
                return (string)ViewState["TrustBatchId"];
            }
            set
            {
                ViewState["TrustBatchId"] = value;
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
                GetButtonPower();
                this.txtNDEDateMonth.Text = string.Format("{0:yyyy-MM}", DateTime.Now);
                this.ddlPageSize.SelectedValue = this.Grid1.PageSize.ToString();
                this.TrustBatchId = string.Empty;
                this.InitTreeMenu();//加载树
            }
        }
        #endregion

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HJGL_NDTBatchMenuId);
            if (buttonList.Count() > 0)
            {
                //if (buttonList.Contains(BLL.Const.BtnAdd))
                //{
                //    this.btnNew.Hidden = false;
                //}
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnAuditing))
                {
                    this.btnAudit.Hidden = false;
                }
                //if (buttonList.Contains(BLL.Const.BtnRepairNotice))
                //{
                //    this.BtnRepairRecord.Hidden = false;
                //}
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnDelete.Hidden = false;
                }
            }
        }
        #endregion

        #region 加载树
        /// <summary>
        /// 加载树
        /// </summary>
        private void InitTreeMenu()
        {
            this.tvControlItem.Nodes.Clear();

            TreeNode rootNode1 = new TreeNode();
            rootNode1.NodeID = "1";
            rootNode1.Text = "建筑工程";
            rootNode1.CommandName = "建筑工程";
            rootNode1.EnableClickEvent = true;
            this.tvControlItem.Nodes.Add(rootNode1);

            TreeNode rootNode2 = new TreeNode();
            rootNode2.NodeID = "2";
            rootNode2.Text = "安装工程";
            rootNode2.CommandName = "安装工程";
            rootNode2.EnableClickEvent = true;
            rootNode2.Expanded = true;
            this.tvControlItem.Nodes.Add(rootNode2);

            var pUnits = (from x in Funs.DB.Project_ProjectUnit where x.ProjectId == this.CurrUser.LoginProjectId select x).ToList();
            // 获取当前用户所在单位
            var currUnit = pUnits.FirstOrDefault(x => x.UnitId == this.CurrUser.UnitId);

            var unitWorkList = (from x in Funs.DB.WBS_UnitWork
                                where x.ProjectId == this.CurrUser.LoginProjectId
                                      && x.SuperUnitWork == null && x.UnitId != null && x.ProjectType != null
                                select x).ToList();

            List<Model.WBS_UnitWork> unitWork1 = null;
            List<Model.WBS_UnitWork> unitWork2 = null;

            // 当前为施工单位，只能操作本单位的数据
            if (currUnit != null && currUnit.UnitType == Const.ProjectUnitType_2)
            {
                unitWork1 = (from x in unitWorkList
                             where x.UnitId == this.CurrUser.UnitId && x.ProjectType == "1"
                             select x).ToList();
                unitWork2 = (from x in unitWorkList
                             where x.UnitId == this.CurrUser.UnitId && x.ProjectType == "2"
                             select x).ToList();
            }
            else
            {
                unitWork1 = (from x in unitWorkList where x.ProjectType == "1" select x).ToList();
                unitWork2 = (from x in unitWorkList where x.ProjectType == "2" select x).ToList();
            }

            if (unitWork1.Count() > 0)
            {
                foreach (var q in unitWork1)
                {
                    int a = (from x in Funs.DB.HJGL_Pipeline where x.ProjectId == this.CurrUser.LoginProjectId && x.UnitWorkId == q.UnitWorkId select x).Count();
                    var u = BLL.UnitService.GetUnitByUnitId(q.UnitId);
                    TreeNode tn1 = new TreeNode();
                    tn1.NodeID = q.UnitWorkId;
                    tn1.Text = q.UnitWorkName;
                    tn1.ToolTip = "施工单位：" + u.UnitName;
                    tn1.CommandName = "单位工程";
                    tn1.EnableClickEvent = true;
                    tn1.EnableExpandEvent = true;
                    rootNode1.Nodes.Add(tn1);
                    BindNodes(tn1);
                }
            }
            if (unitWork2.Count() > 0)
            {
                foreach (var q in unitWork2)
                {
                    int a = (from x in Funs.DB.HJGL_Pipeline where x.ProjectId == this.CurrUser.LoginProjectId && x.UnitWorkId == q.UnitWorkId select x).Count();
                    var u = BLL.UnitService.GetUnitByUnitId(q.UnitId);
                    TreeNode tn2 = new TreeNode();
                    tn2.NodeID = q.UnitWorkId;
                    tn2.Text = q.UnitWorkName;
                    tn2.ToolTip = "施工单位：" + u.UnitName;
                    tn2.CommandName = "单位工程";
                    tn2.EnableClickEvent = true;
                    tn2.EnableExpandEvent = true;
                    rootNode2.Nodes.Add(tn2);
                    BindNodes(tn2);
                }
            }
        }

        /// <summary>
        ///  绑定树节点
        /// </summary>
        /// <param name="node"></param>
        private void BindNodes(TreeNode node)
        {
            var p = from x in Funs.DB.HJGL_Batch_BatchTrust
                    where x.UnitWorkId == node.NodeID
                    && x.TrustDate < Convert.ToDateTime(this.txtNDEDateMonth.Text.Trim() + "-01").AddMonths(1)
                    && x.TrustDate >= Convert.ToDateTime(this.txtNDEDateMonth.Text.Trim() + "-01")
                    select x;
            if (p.Count() > 0)
            {
                TreeNode newNode = new TreeNode();
                newNode.Text = "探伤类型";
                newNode.NodeID = "探伤类型";
                node.Nodes.Add(newNode);
            }
        }
        protected void tvControlItem_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
        {
            e.Node.Nodes.Clear();
            e.Node.Expanded = true;
            if (e.Node.CommandName == "单位工程")
            {
                var detectionTypes = from x in Funs.DB.Base_DetectionType
                                     orderby x.DetectionTypeCode
                                     select new { x.DetectionTypeId, x.DetectionTypeCode, x.DetectionTypeName };
                foreach (var item in detectionTypes)
                {
                    var types = (from x in Funs.DB.HJGL_Batch_BatchTrust
                                 join y in Funs.DB.Base_DetectionType
                                 on x.DetectionTypeId equals y.DetectionTypeId
                                 where x.ProjectId == this.CurrUser.LoginProjectId
                                       && x.UnitWorkId == e.Node.NodeID
                                       && x.DetectionTypeId == item.DetectionTypeId
                                 orderby y.DetectionTypeCode
                                 select y).Distinct();

                    TreeNode newNode = new TreeNode();
                    if (types.Count() > 0)
                    {
                        newNode.Text = item.DetectionTypeCode;
                        newNode.NodeID = item.DetectionTypeId + "|" + e.Node.NodeID;
                        newNode.EnableExpandEvent = true;
                        newNode.ToolTip = item.DetectionTypeName;
                        newNode.CommandName = "探伤类型";
                        newNode.EnableClickEvent = true;
                        e.Node.Nodes.Add(newNode);
                    }

                    TreeNode tn1 = new TreeNode
                    {
                        Text = "委托单号",
                        NodeID = "委托单号",
                    };
                    newNode.Nodes.Add(tn1);
                }
            }

            if (e.Node.CommandName == "探伤类型")
            {
                string ndtTypeId = e.Node.NodeID.Split('|')[0];
                var detectionRates = from x in Funs.DB.Base_DetectionRate
                                     orderby x.DetectionRateCode
                                     select new { x.DetectionRateId, x.DetectionRateCode, x.DetectionRateValue };
                foreach (var item in detectionRates)
                {
                    var trusts = from x in Funs.DB.HJGL_Batch_BatchTrust
                                 where x.TrustDate < Convert.ToDateTime(this.txtNDEDateMonth.Text.Trim() + "-01").AddMonths(1)
                                 && x.TrustDate >= Convert.ToDateTime(this.txtNDEDateMonth.Text.Trim() + "-01")
                                 && x.ProjectId == this.CurrUser.LoginProjectId && x.TrustBatchCode.Contains(this.txtSearchCode.Text.Trim())
                                 && x.UnitWorkId.ToString() == e.Node.ParentNode.NodeID
                                 && x.DetectionTypeId == ndtTypeId && x.DetectionRateId == item.DetectionRateId
                                 orderby x.TrustBatchCode descending
                                 select x;
                    if (item.DetectionRateValue > 0)   //探伤比例为0的批不显示
                    {
                        TreeNode newNode = new TreeNode();
                        if (trusts.Count() > 0)
                        {
                            newNode.Text = item.DetectionRateValue.ToString() + "%";
                            newNode.NodeID = item.DetectionRateId + "|" + e.Node.NodeID;
                            newNode.EnableExpandEvent = true;
                            newNode.ToolTip = item.DetectionRateCode;
                            newNode.CommandName = "检测比例";

                            e.Node.Nodes.Add(newNode);
                        }

                        TreeNode tn1 = new TreeNode
                        {
                            Text = "检测批",
                            NodeID = "检测批",
                        };
                        newNode.Nodes.Add(tn1);
                    }
                }
            }
            if (e.Node.CommandName == "检测比例")
            {
                ///单号
                string ndtTypeId = e.Node.ParentNode.NodeID.Split('|')[0];
                string ndtRateId = e.NodeID.Split('|')[0];
                var trusts = from x in Funs.DB.HJGL_Batch_BatchTrust
                             where x.TrustDate < Convert.ToDateTime(this.txtNDEDateMonth.Text.Trim() + "-01").AddMonths(1)
                             && x.TrustDate >= Convert.ToDateTime(this.txtNDEDateMonth.Text.Trim() + "-01")
                             && x.ProjectId == this.CurrUser.LoginProjectId && x.TrustBatchCode.Contains(this.txtSearchCode.Text.Trim())
                             && x.UnitWorkId.ToString() == e.Node.ParentNode.ParentNode.NodeID
                             && x.DetectionTypeId == ndtTypeId && x.DetectionRateId == ndtRateId
                             orderby x.TrustBatchCode descending
                             select x;
                foreach (var trust in trusts)
                {
                    TreeNode newNode = new TreeNode();
                    string code = string.Empty;
                    if (trust.TrustType == "R")
                    {
                        code = "FXWT-" + trust.TrustBatchCode.Substring(trust.TrustBatchCode.Length - 4);
                    }
                    else
                    {
                        code = "WT-" + trust.TrustBatchCode.Substring(trust.TrustBatchCode.Length - 4);
                    }
                    // 未检测委托红色显示
                    if (BLL.Batch_NDEService.GetNDEViewByTrustBatchId(trust.TrustBatchId) == null)
                    {
                        Model.HJGL_Batch_PointBatch batch = BLL.PointBatchService.GetPointBatchById(trust.PointBatchId);
                        if (batch != null && batch.IsClosed == true)
                        {
                            newNode.Text = code;
                        }
                        else
                        {
                            newNode.Text = "<font color='#EE0000'>" + code + "</font>";
                        }
                    }
                    else
                    {
                        newNode.Text = code;
                    }
                    newNode.NodeID = trust.TrustBatchId;
                    newNode.ToolTip = "委托单号";
                    newNode.CommandName = "委托单号";
                    newNode.EnableClickEvent = true;
                    e.Node.Nodes.Add(newNode);
                }
            }
        }
        #endregion

        #region 点击TreeView
        /// <summary>
        /// 点击TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvControlItem_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HJGL_NDTBatchMenuId);
            if (this.tvControlItem.SelectedNode.CommandName == "建筑工程" || this.tvControlItem.SelectedNode.CommandName == "安装工程" || this.tvControlItem.SelectedNode.CommandName == "探伤类型")
            {
                //this.btnNew.Hidden = true;
                this.btnEdit.Hidden = true;
                this.btnAudit.Hidden = true;
                this.btnView.Hidden = true;
                this.btnDelete.Hidden = true;
            }
            else if (this.tvControlItem.SelectedNode.CommandName == "单位工程")
            {
                //if (buttonList.Contains(BLL.Const.BtnAdd))
                //{
                //    this.btnNew.Hidden = false;
                //}
                this.btnEdit.Hidden = true;
                this.btnAudit.Hidden = true;
                this.btnView.Hidden = true;
                this.btnDelete.Hidden = true;
            }
            else if (this.tvControlItem.SelectedNode.CommandName == "探伤类型")
            {
                //if (buttonList.Contains(BLL.Const.BtnAdd))
                //{
                //    this.btnNew.Hidden = false;
                //}
                this.btnEdit.Hidden = true;
                this.btnAudit.Hidden = true;
                this.btnView.Hidden = true;
                this.btnDelete.Hidden = true;
            }
            else if (this.tvControlItem.SelectedNode.CommandName == "委托单号")
            {
                //this.btnNew.Hidden = true;
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnAuditing))
                {
                    this.btnAudit.Hidden = false;
                }
                this.btnView.Hidden = false;
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnDelete.Hidden = false;
                }
            }
            this.TrustBatchId = tvControlItem.SelectedNodeID;
            this.BindGrid();
        }
        #endregion

        #region 数据绑定
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.ToolTip == "委托单号")
            {
                this.SetTextTemp();
                this.PageInfoLoad(); ///页面输入提交信息
                var check = Funs.DB.View_Batch_NDE.FirstOrDefault(x => x.TrustBatchId == this.TrustBatchId);
                if (check != null)
                {
                    string strSql = @"SELECT * FROM dbo.View_Batch_NDEItem d WHERE NDEID=@NDEID ";
                    List<SqlParameter> listStr = new List<SqlParameter>
                    {

                    };
                    listStr.Add(new SqlParameter("@NDEID", check.NDEID));
                    SqlParameter[] parameter = listStr.ToArray();
                    DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

                    // 2.获取当前分页数据
                    //var table = this.GetPagedDataTable(Grid1, tb1);
                    Grid1.RecordCount = tb.Rows.Count;
                    tb = GetFilteredTable(Grid1.FilteredData, tb);
                    var table = this.GetPagedDataTable(Grid1, tb);
                    Grid1.DataSource = table;
                    Grid1.DataBind();
                }
                else
                {
                    string strSql = @"SELECT * FROM dbo.View_Batch_BatchTrustItem d WHERE TrustBatchId=@TrustBatchId ";
                    List<SqlParameter> listStr = new List<SqlParameter>
                    {

                    };
                    listStr.Add(new SqlParameter("@TrustBatchId", this.TrustBatchId));
                    SqlParameter[] parameter = listStr.ToArray();
                    DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

                    // 2.获取当前分页数据
                    //var table = this.GetPagedDataTable(Grid1, tb1);
                    Grid1.RecordCount = tb.Rows.Count;
                    tb = GetFilteredTable(Grid1.FilteredData, tb);
                    var table = this.GetPagedDataTable(Grid1, tb);
                    Grid1.DataSource = table;
                    Grid1.DataBind();
                }
            }
        }

        #region 加载页面输入提交信息
        /// <summary>
        /// 加载页面输入提交信息
        /// </summary>
        private void PageInfoLoad()
        {
            this.SimpleForm1.Reset(); ///重置所有字段
            var check = Funs.DB.View_Batch_NDE.FirstOrDefault(x => x.TrustBatchId == this.TrustBatchId);
            if (check != null)
            {
                this.txtUnitName.Text = check.UnitName;
                this.txtIsCheck.Text = "已检测";
                this.txtCheckUnit.Text = check.NDEUnitName;
                this.txtDetectionTypeCode.Text = check.DetectionTypeCode;
                if (check.NDEDate != null)
                {
                    this.txtNDEDate.Text = string.Format("{0:yyyy-MM-dd}", check.NDEDate);
                }
                this.txtNDECode.Text = check.NDECode;
            }
            else
            {
                Model.View_Batch_BatchTrust trust = BLL.Batch_BatchTrustService.GetBatchTrustViewById(this.TrustBatchId);
                if (trust != null)
                {
                    Model.HJGL_Batch_PointBatch batch = BLL.PointBatchService.GetPointBatchById(trust.PointBatchId);
                    if (batch != null && batch.IsClosed == true)
                    {
                        this.txtIsCheck.Text = "无需检测";
                    }
                    else
                    {
                        this.txtUnitName.Text = trust.UnitName;
                        this.txtIsCheck.Text = "未检测";
                        Model.Base_Unit ndeUnit = BLL.UnitService.GetUnitByUnitId(trust.NDEUnit);
                        if (ndeUnit != null)
                        {
                            this.txtCheckUnit.Text = ndeUnit.UnitName;
                        }
                        this.txtDetectionTypeCode.Text = trust.DetectionTypeCode;
                        this.txtNDECode.Text = string.Empty;
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 情况
        /// </summary>
        private void SetTextTemp()
        {
            this.txtUnitName.Text = string.Empty;
            this.txtIsCheck.Text = string.Empty;
            this.txtCheckUnit.Text = string.Empty;
            this.txtDetectionTypeCode.Text = string.Empty;
            this.txtNDEDate.Text = string.Empty;
            this.txtNDECode.Text = string.Empty;
        }
        #endregion

        #region 分页排序
        #region 页索引改变事件
        /// <summary>
        /// 页索引改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
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
            BindGrid();
        }
        #endregion

        #region 分页选择下拉改变事件
        /// <summary>
        /// 分页选择下拉改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }
        #endregion
        #endregion

        #region 检测单 维护事件
        /// <summary>
        /// 增加检测单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_NDTBatchMenuId, Const.BtnAdd))
            {
                if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.CommandName == "单位工程")
                {
                    this.SetTextTemp();
                    string window = String.Format("NDTBatchEdit.aspx?unitWorkId={0}", tvControlItem.SelectedNodeID, "新增 - ");
                    PageContext.RegisterStartupScript(Window1.GetSaveStateReference(this.hdTrustBatchId.ClientID)
                      + Window1.GetShowReference(window));
                }
                else
                {
                    ShowNotify("请选择单位工程！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        #region 编辑检测单
        /// <summary>
        /// 编辑检测单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_NDTBatchMenuId, Const.BtnSave))
            {
                if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.CommandName == "委托单号")
                {
                    Model.View_Batch_NDE check = BLL.Batch_NDEService.GetNDEViewByTrustBatchId(this.tvControlItem.SelectedNodeID);
                    if (check != null)
                    {
                        if (check.AuditDate == null)
                        {
                            string window = String.Format("NDTBatchEdit.aspx?TrustBatchId={0}", this.TrustBatchId, "编辑 - ");
                            PageContext.RegisterStartupScript(Window1.GetSaveStateReference(this.hdTrustBatchId.ClientID)
                              + Window1.GetShowReference(window));
                        }
                        else
                        {
                            ShowNotify("该单据已审核，无法编辑", MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        var batch = (from x in Funs.DB.HJGL_Batch_PointBatch
                                     join y in Funs.DB.HJGL_Batch_BatchTrust on x.PointBatchId equals y.PointBatchId
                                     where y.TrustBatchId == this.TrustBatchId
                                     select x).FirstOrDefault();
                        if (batch != null && batch.IsClosed == true)
                        {
                            ShowNotify("该委托无需检测", MessageBoxIcon.Warning);
                            return;
                        }
                        string window = String.Format("NDTBatchEdit.aspx?TrustBatchId={0}", this.TrustBatchId, "编辑 - ");
                        PageContext.RegisterStartupScript(Window1.GetSaveStateReference(this.hdTrustBatchId.ClientID)
                          + Window1.GetShowReference(window));
                    }
                }
                else
                {
                    ShowNotify("请选择要编辑的记录", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 删除检测单
        /// <summary>
        /// 删除检测单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_NDTBatchMenuId, Const.BtnDelete))
            {
                if (this.tvControlItem.SelectedNode != null)
                {
                    Model.HJGL_Batch_NDE check = BLL.Batch_NDEService.GetNDEByTrustBatchId(this.tvControlItem.SelectedNodeID);
                    if (check != null)
                    {
                        string trustId = check.TrustBatchId;
                        if (judgementDelete(check.NDEID))
                        {
                            BLL.Batch_NDEItemService.DeleteNDEItemById(check.NDEID);
                            BLL.Batch_NDEService.DeleteNDEById(check.NDEID);
                            BLL.Batch_BatchTrustService.UpdatTrustBatchtState(trustId, null);
                            //BLL.Sys_LogService.AddLog(BLL.Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_NDTBatchMenuId, Const.BtnDelete, this.NDEID);
                            ShowNotify("删除成功！", MessageBoxIcon.Success);
                            this.InitTreeMenu();
                            this.Grid1.DataSource = null;
                            this.Grid1.DataBind();
                            this.SetTextTemp();
                        }
                        else
                        {
                            ShowNotify("检测单明细已审核，不能删除！", MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        ShowNotify("请选择要删除的记录", MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    ShowNotify("请选择要删除的记录", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }
        #endregion
        #endregion

        #region 生成返修通知单
        protected void BtnRepairRecord_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_NDTBatchMenuId, Const.BtnRepairNotice))
            {
                //Model.HJGL_Batch_NDE nde = BLL.Batch_NDEService.GetNDEById(this.NDEID);
                //if (nde != null)
                //{
                //    var trust = Batch_BatchTrustService.GetBatchTrustById(nde.TrustBatchId);
                //    if (trust != null)
                //    {
                //        if (trust.IsCheck != true)
                //        {
                //            ShowNotify("请先审核该检测单！", MessageBoxIcon.Warning);
                //            return;
                //        }
                //    }
                //}
                //var notOKCheckItem = from x in Funs.DB.HJGL_Batch_NDEItem where x.NDEID == this.NDEID  select x;
                if (Grid1.SelectedRow != null)
                {
                    if (Grid1.SelectedRow.DataKeys[1] != null)
                    {
                        string ndtItem = Grid1.SelectedRow.DataKeys[1].ToString();
                        if (ndtItem != string.Empty)
                        {
                            var q = BLL.Batch_NDEItemService.GetNDEItemById(ndtItem);
                            if (q.PassFilm != q.TotalFilm && q.SubmitDate.HasValue)
                            {
                                string window = String.Format("RepairNotice.aspx?NDEItemID={0}", ndtItem, "返修通知单");
                                PageContext.RegisterStartupScript(WindowRepair.GetShowReference(window));
                            }
                            else
                            {
                                ShowNotify("请选择不合格并且已审核的检测项！", MessageBoxIcon.Warning);
                            }

                        }
                        else
                        {
                            ShowNotify("请选择不合格检测项！", MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        ShowNotify("请选择不合格检测项！", MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    ShowNotify("请选择不合格检测项！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 判断是否可删除
        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private bool judgementDelete(string id)
        {
            string content = string.Empty;
            var ndeItems = from x in Funs.DB.HJGL_Batch_NDEItem where x.NDEID == id && x.SubmitDate.HasValue select x;

            if (ndeItems.Count() > 0)
            {
                content = "检测单明细已审核，不能删除！";

            }

            if (string.IsNullOrEmpty(content))
            {
                return true;
            }
            else
            {
                //Alert.ShowInTop(content, MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion

        #region 关闭弹出窗口及刷新页面
        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            this.TrustBatchId = this.hdTrustBatchId.Text;
            this.BindGrid();
            //this.InitTreeMenu();
            this.hdTrustBatchId.Text = string.Empty;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Tree_TextChanged(object sender, EventArgs e)
        {
            this.InitTreeMenu();
            //this.BindGrid();
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 获取缺陷
        /// </summary>
        /// <param name="bigType"></param>
        /// <returns></returns>
        protected string ConvertCheckDefects(object CheckDefects)
        {
            string str = string.Empty;
            if (CheckDefects != null)
            {
                str = BLL.Base_DefectService.GetDefectNameStrByDefectIdStr(CheckDefects.ToString());
            }
            return str;
        }
        #endregion

        #region 检测单 审核事件
        /// <summary>
        /// 编辑检测单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAudit_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_NDTBatchMenuId, Const.BtnAuditing))
            {
                if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.CommandName == "委托单号")
                {
                    Model.View_Batch_NDE check = BLL.Batch_NDEService.GetNDEViewByTrustBatchId(this.tvControlItem.SelectedNodeID);
                    if (check != null)
                    {
                        if (check.AuditDate == null)
                        {
                            string window = String.Format("NDTBatchAudit.aspx?TrustBatchId={0}", this.TrustBatchId, "编辑 - ");
                            PageContext.RegisterStartupScript(Window1.GetSaveStateReference(this.hdTrustBatchId.ClientID)
                              + Window1.GetShowReference(window));
                        }
                        else
                        {
                            ShowNotify("该单据已审核！", MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        var batch = (from x in Funs.DB.HJGL_Batch_PointBatch
                                     join y in Funs.DB.HJGL_Batch_BatchTrust on x.PointBatchId equals y.PointBatchId
                                     where y.TrustBatchId == this.TrustBatchId
                                     select x).FirstOrDefault();
                        if (batch != null && batch.IsClosed == true)
                        {
                            ShowNotify("该委托无需检测", MessageBoxIcon.Warning);
                            return;
                        }
                        ShowNotify("请先编辑检测单记录！", MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    ShowNotify("请选择要审核的记录", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 更新自动扩透焊口
        /// </summary>
        /// <param name="pointBatchItemId">批明细Id</param>
        private void AutoExpandUpdate(string pointBatchItemId, string toPointBatchItemId)
        {
            //Model.HJGLDB db = Funs.DB;
            //var pointBatchItem = db.Batch_PointBatchItem.FirstOrDefault(e => e.PointBatchItemId == pointBatchItemId);
            //pointBatchItem.PointDate = Convert.ToDateTime(DateTime.Now.Date);
            //pointBatchItem.PointState = "2";
            //pointBatchItem.IsBuildTrust = false;
            //pointBatchItem.IsCheckRepair = false;
            //pointBatchItem.ToPointBatchItemId = toPointBatchItemId; 
            //db.SubmitChanges(); // 扩透口

            //var jointInfo = db.Pipeline_WeldJoint.FirstOrDefault(x => x.WeldJointId == pointBatchItem.WeldJointId);

            //int k_num = 0;
            //string jot = "K1";
            //int indexK = jointInfo.WeldJointCode.LastIndexOf("K");

            //if (indexK > 0)
            //{
            //    try
            //    {
            //        k_num = Convert.ToInt32(jointInfo.WeldJointCode.Substring(indexK + 1, 1)) + 1;
            //        jot = "K" + k_num.ToString();
            //    }
            //    catch { }
            //}
            //BLL.Batch_PointBatchService.UpdateNewKuoOrCutJointNo(pointBatchItem.PointBatchItemId, jot);
        }
        #endregion

        #region 检测单 查看事件
        /// <summary>
        /// 查看检测单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnView_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_NDTBatchMenuId, Const.BtnAuditing))
            {
                if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.CommandName == "委托单号")
                {
                    Model.View_Batch_NDE check = BLL.Batch_NDEService.GetNDEViewByTrustBatchId(this.tvControlItem.SelectedNodeID);
                    if (check != null)
                    {
                        string window = String.Format("NDTBatchAudit.aspx?TrustBatchId={0}&View=View", this.TrustBatchId, "编辑 - ");
                        PageContext.RegisterStartupScript(Window1.GetSaveStateReference(this.hdTrustBatchId.ClientID)
                          + Window1.GetShowReference(window));
                    }
                    else
                    {
                        var batch = (from x in Funs.DB.HJGL_Batch_PointBatch
                                     join y in Funs.DB.HJGL_Batch_BatchTrust on x.PointBatchId equals y.PointBatchId
                                     where y.TrustBatchId == this.TrustBatchId
                                     select x).FirstOrDefault();
                        if (batch != null && batch.IsClosed == true)
                        {
                            ShowNotify("该委托无需检测", MessageBoxIcon.Warning);
                            return;
                        }
                        ShowNotify("请先编辑检测单记录！", MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    ShowNotify("请选择要审核的记录", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }
        #endregion

        #region Grid行点击事件
        /// <summary>
        /// Grid行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string id = Grid1.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "CancelAudit")
            {
                if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_NDTBatchMenuId, Const.BtnCancelAuditing))
                {
                    Model.HJGL_Batch_NDEItem item = BLL.Batch_NDEItemService.GetNDEItemById(id);
                    if (item.SubmitDate == null)
                    {

                    }
                    else
                    {
                        var trustBatchItem = Funs.DB.HJGL_Batch_BatchTrustItem.FirstOrDefault(x => x.TrustBatchItemId == item.TrustBatchItemId);
                        if (trustBatchItem != null)
                        {
                            item.SubmitDate = null;
                            BLL.Batch_NDEItemService.UpdateNDEItem(item);
                            Model.HJGL_Batch_NDE nde = BLL.Batch_NDEService.GetNDEById(item.NDEID);
                            BLL.Batch_BatchTrustService.UpdatTrustBatchtState(nde.TrustBatchId, null);
                            ShowNotify("提交成功！", MessageBoxIcon.Success);
                        }
                    }
                }
                else
                {
                    Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                    return;
                }
            }
        }
        #endregion

        #region 导入
        /// <summary>
        /// 导入按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_NDTBatchMenuId, Const.BtnAdd))
            {
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("CheckManageIn.aspx", "导入 - ")));
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 关闭导入弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            InitTreeMenu();
        }
        #endregion
    }
}