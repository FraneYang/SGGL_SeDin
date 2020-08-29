﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;
using System.IO;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.CQMS.Check
{
    public partial class ShowWBS : PageBase
    {
        /// <summary>
        /// 被选择项列表
        /// </summary>
        public List<string> SelectedList
        {
            get
            {
                return (List<string>)ViewState["SelectedList"];
            }
            set
            {
                ViewState["SelectedList"] = value;
            }
        }

        /// <summary>
        /// 未被选择项列表
        /// </summary>
        public List<string> NoSelectedList
        {
            get
            {
                return (List<string>)ViewState["NoSelectedList"];
            }
            set
            {
                ViewState["NoSelectedList"] = value;
            }
        }

        #region  页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.SelectedList = new List<string>();
                this.NoSelectedList = new List<string>();
                InitTreeMenu();
            }
        }
        #endregion

        #region  加载树
        /// <summary>
        /// 加载树
        /// </summary>
        private void InitTreeMenu()
        {
            this.trWBS.Nodes.Clear();
            this.trWBS.ShowBorder = false;
            this.trWBS.ShowHeader = false;
            this.trWBS.EnableIcons = true;
            this.trWBS.AutoScroll = true;
            this.trWBS.EnableSingleClickExpand = true;

            TreeNode rootNode1 = new TreeNode();
            rootNode1.Text = "建筑工程";
            rootNode1.NodeID = "1";
            rootNode1.CommandName = "ProjectType";
            rootNode1.EnableExpandEvent = true;
            rootNode1.EnableClickEvent = true;
            this.trWBS.Nodes.Add(rootNode1);
            TreeNode emptyNode = new TreeNode();
            emptyNode.Text = "";
            emptyNode.NodeID = "";
            rootNode1.Nodes.Add(emptyNode);
            //this.GetNodes(rootNode1.Nodes, rootNode1.NodeID);

            TreeNode rootNode2 = new TreeNode();
            rootNode2.Text = "安装工程";
            rootNode2.NodeID = "2";
            rootNode2.CommandName = "ProjectType";
            rootNode2.EnableExpandEvent = true;
            rootNode2.EnableClickEvent = true;
            this.trWBS.Nodes.Add(rootNode2);
            rootNode2.Nodes.Add(emptyNode);
            //this.GetNodes(rootNode2.Nodes, rootNode2.NodeID);
        }

        #region  遍历节点方法
        /// <summary>
        /// 遍历节点方法
        /// </summary>
        /// <param name="nodes">节点集合</param>
        /// <param name="parentId">父节点</param>
        private void GetNodes(TreeNodeCollection nodes, string parentId)
        {
            List<Model.WBS_WorkPackageProject> workPackages = new List<Model.WBS_WorkPackageProject>();
            if (parentId.Length == 1) //工程类型节点
            {
                workPackages = (from x in Funs.DB.WBS_WorkPackageProject
                                where x.SuperWorkPack == null && x.ProjectId == this.CurrUser.LoginProjectId && x.ProjectType == parentId
                                orderby x.PackageCode ascending
                                select x).ToList();
            }
            else
            {
                workPackages = (from x in Funs.DB.WBS_WorkPackageProject
                                where x.SuperWorkPack == parentId && x.ProjectId == this.CurrUser.LoginProjectId
                                orderby x.PackageCode ascending
                                select x).ToList();
            }
            foreach (var q in workPackages)
            {
                TreeNode newNode = new TreeNode();
                newNode.Text = q.PackageContent;
                newNode.NodeID = q.WorkPackageCode;
                newNode.CommandName = "WorkPackage";
                newNode.EnableClickEvent = true;
                nodes.Add(newNode);
            }

            for (int i = 0; i < nodes.Count; i++)
            {
                GetNodes(nodes[i].Nodes, nodes[i].NodeID);
            }
        }
        #endregion
        #endregion

        #region  展开树
        /// <summary>
        /// 展开树
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void trWBS_NodeExpand(object sender, TreeNodeEventArgs e)
        {
            e.Node.Nodes.Clear();
            if (e.Node.CommandName == "ProjectType")   //展开工程类型
            {
                var trUnitWork = from x in Funs.DB.WBS_UnitWork
                                 where x.ProjectId == this.CurrUser.LoginProjectId && x.SuperUnitWork == null && x.ProjectType == e.Node.NodeID
                                 select x;
                trUnitWork = trUnitWork.OrderBy(x => x.UnitWorkCode);
                if (trUnitWork.Count() > 0)
                {
                    foreach (var trUnitWorkItem in trUnitWork)
                    {
                        TreeNode newNode = new TreeNode();
                        string uweights = string.Empty;
                        if (trUnitWorkItem.Weights != null)
                        {
                            uweights = "(" + Convert.ToDouble(trUnitWorkItem.Weights).ToString() + "%)";
                        }
                        newNode.Text = trUnitWorkItem.UnitWorkCode + "-" + trUnitWorkItem.UnitWorkName + uweights;
                        newNode.NodeID = trUnitWorkItem.UnitWorkId;
                        newNode.CommandName = "UnitWork";
                        newNode.EnableExpandEvent = true;
                        newNode.EnableClickEvent = true;
                        e.Node.Nodes.Add(newNode);
                        if (BLL.WorkPackageService.GetWorkPackages1ByUnitWorkId(trUnitWorkItem.UnitWorkId.ToString()) != null)
                        {
                            TreeNode temp = new TreeNode();
                            temp.Text = "temp";
                            temp.NodeID = "temp";
                            newNode.Nodes.Add(temp);
                        }
                    }
                }
            }
            else if (e.Node.CommandName == "UnitWork")   //展开单位工程节点
            {
                var workPackages = from x in Funs.DB.WBS_WorkPackage where x.UnitWorkId == e.NodeID && x.SuperWorkPack == null && x.IsApprove == true orderby x.WorkPackageCode select x;
                foreach (var workPackage in workPackages)
                {
                    TreeNode newNode = new TreeNode();
                    string weights = string.Empty;
                    if (workPackage.Weights != null)
                    {
                        weights = "(" + Convert.ToDouble(workPackage.Weights).ToString() + "%)";
                    }
                    newNode.Text = workPackage.PackageContent + weights;
                    newNode.NodeID = workPackage.WorkPackageId;
                    newNode.CommandName = "WorkPackage";
                    newNode.EnableExpandEvent = true;
                    newNode.EnableClickEvent = true;
                    e.Node.Nodes.Add(newNode);
                    var childWorkPackages = from x in Funs.DB.WBS_WorkPackage where x.SuperWorkPackageId == workPackage.WorkPackageId && x.IsApprove == true select x;
                    if (childWorkPackages.Count() > 0)
                    {
                        TreeNode emptyNode = new TreeNode();
                        emptyNode.Text = "";
                        emptyNode.NodeID = "";
                        newNode.Nodes.Add(emptyNode);
                    }
                }
            }
            else if (e.Node.CommandName == "WorkPackage")   //展开工作包节点
            {
                var workPackages = from x in Funs.DB.WBS_WorkPackage where x.SuperWorkPackageId == e.Node.NodeID && x.IsApprove == true orderby x.WorkPackageCode select x;
                if (workPackages.Count() > 0)   //存在子单位工程
                {
                    foreach (var workPackage in workPackages)
                    {
                        TreeNode newNode = new TreeNode();
                        string weights = string.Empty;
                        if (workPackage.Weights != null)
                        {
                            weights = "(" + Convert.ToDouble(workPackage.Weights).ToString() + "%)";
                        }
                        newNode.Text = workPackage.PackageContent + weights;
                        newNode.NodeID = workPackage.WorkPackageId;
                        newNode.CommandName = "WorkPackage";
                        newNode.EnableExpandEvent = true;
                        newNode.EnableClickEvent = true;
                        e.Node.Nodes.Add(newNode);
                        var childWorkPackages = from x in Funs.DB.WBS_WorkPackage where x.SuperWorkPackageId == workPackage.WorkPackageId && x.IsApprove == true select x;
                        if (childWorkPackages.Count() > 0)
                        {
                            TreeNode emptyNode = new TreeNode();
                            emptyNode.Text = "";
                            emptyNode.NodeID = "";
                            newNode.Nodes.Add(emptyNode);
                        }
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 展开全部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuMore_Click(object sender, EventArgs e)
        {
            if (this.trWBS.SelectedNode != null)
            {
                if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ControlItemAndCycleMenuId, BLL.Const.BtnAdd))
                {
                    if (this.trWBS.SelectedNode.CommandName != "ProjectType")   //非工程类型节点可以增加
                    {
                        Model.WBS_UnitWork unitWork = BLL.UnitWorkService.GetUnitWorkByUnitWorkId(this.trWBS.SelectedNodeID);
                        if (unitWork != null)   //单位工程节点
                        {
                            this.trWBS.SelectedNode.Expanded = true;
                            this.trWBS.SelectedNode.Nodes.Clear();
                            var workPackages = from x in Funs.DB.WBS_WorkPackage where x.UnitWorkId == this.trWBS.SelectedNodeID && x.SuperWorkPack == null && x.IsApprove == true orderby x.WorkPackageCode select x;
                            foreach (var workPackage in workPackages)
                            {
                                TreeNode newNode = new TreeNode();
                                string weights = string.Empty;
                                if (workPackage.Weights != null)
                                {
                                    weights = "(" + Convert.ToDouble(workPackage.Weights).ToString() + "%)";
                                }
                                newNode.Text = workPackage.PackageContent + weights;
                                newNode.NodeID = workPackage.WorkPackageId;
                                newNode.CommandName = "WorkPackage";
                                newNode.EnableExpandEvent = true;
                                newNode.EnableClickEvent = true;
                                this.trWBS.SelectedNode.Nodes.Add(newNode);
                                var childWorkPackages = from x in Funs.DB.WBS_WorkPackage where x.SuperWorkPackageId == workPackage.WorkPackageId && x.IsApprove == true select x;
                                if (childWorkPackages.Count() > 0)
                                {
                                    newNode.Expanded = true;
                                    ExpandWorkPackage(newNode.Nodes, newNode.NodeID);
                                }
                            }
                        }
                        else
                        {
                            this.trWBS.SelectedNode.Expanded = true;
                            this.trWBS.SelectedNode.Nodes.Clear();
                            ExpandWorkPackage(this.trWBS.SelectedNode.Nodes, this.trWBS.SelectedNodeID);
                        }
                    }
                    else
                    {
                        ShowNotify("请选择单位工程节点展开！", MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 展开子级分部分项节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="parentId"></param>
        private void ExpandWorkPackage(TreeNodeCollection nodes, string parentId)
        {
            var workPackages = from x in Funs.DB.WBS_WorkPackage where x.SuperWorkPackageId == parentId && x.IsApprove == true orderby x.WorkPackageCode select x;
            if (workPackages.Count() > 0)   //存在子单位工程
            {
                foreach (var workPackage in workPackages)
                {
                    TreeNode newNode = new TreeNode();
                    string weights = string.Empty;
                    if (workPackage.Weights != null)
                    {
                        weights = "(" + Convert.ToDouble(workPackage.Weights).ToString() + "%)";
                    }
                    newNode.Text = workPackage.PackageContent + weights;
                    newNode.NodeID = workPackage.WorkPackageId;
                    newNode.CommandName = "WorkPackage";
                    newNode.EnableExpandEvent = true;
                    newNode.EnableClickEvent = true;
                    nodes.Add(newNode);
                    var childWorkPackages = from x in Funs.DB.WBS_WorkPackage where x.SuperWorkPackageId == workPackage.WorkPackageId && x.IsApprove == true select x;
                    if (childWorkPackages.Count() > 0)
                    {
                        newNode.Expanded = true;
                        ExpandWorkPackage(newNode.Nodes, newNode.NodeID);
                    }
                }
            }
        }

        #region  Tree点击事件
        /// <summary>
        /// Tree点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void trWBS_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            int rowsCount = this.Grid1.Rows.Count;
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                AspNet.CheckBox ckb = (AspNet.CheckBox)(this.Grid1.Rows[i].FindControl("cbSelect"));
                if (ckb.Checked)
                {
                    SelectedList.Add(this.Grid1.Rows[i].RowID);
                }
                else
                {
                    NoSelectedList.Add(this.Grid1.Rows[i].RowID);
                }
            }
            BindGrid();
        }
        #endregion

        #region  绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// Grid1排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
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
        /// 加载Grid
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT ControlItemAndCycleId,ControlItemAndCycleCode,InitControlItemCode,PlanCompleteDate,ControlItemContent,ControlPoint,ControlItemDef,Weights,HGForms,SHForms,Standard,ClauseNo,CheckNum"
                     + @" FROM WBS_ControlItemAndCycle ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " where WorkPackageId = @WorkPackageId and IsApprove=1 ";
            listStr.Add(new SqlParameter("@WorkPackageId", this.trWBS.SelectedNodeID));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();

            Model.WBS_WorkPackage workPackage = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(this.trWBS.SelectedNodeID);
            if (workPackage != null)
            {
                if (workPackage.ProjectType == "1")  //建筑工程
                {
                    this.Grid1.Columns[5].HeaderText = "对应的建筑资料表格";
                    this.Grid1.Columns[6].Hidden = true;
                }
                else    //安装工程
                {
                    this.Grid1.Columns[5].HeaderText = "对应的化工资料表格";
                    this.Grid1.Columns[6].Hidden = false;
                }
            }
            for (int i = 0; i < this.Grid1.Rows.Count; i++)
            {
                string controlPointType = Request.Params["ControlPointType"];
                Model.WBS_ControlItemAndCycle c = BLL.ControlItemAndCycleService.GetControlItemAndCycleById(this.Grid1.Rows[i].RowID.ToString());
                if (c.CheckNum != 0)  //检查次数为0表示一直检查
                {
                    List<Model.Check_SpotCheckDetail> details = BLL.SpotCheckDetailService.GetSpotCheckDetailsByControlItemAndCycleId(c.ControlItemAndCycleId);
                    if (details.Count == c.CheckNum)  //检查次数已达到最大值
                    {
                        AspNet.CheckBox ckbWorkPackageCode = (AspNet.CheckBox)(this.Grid1.Rows[i].FindControl("cbSelect"));
                        ckbWorkPackageCode.Enabled = false;
                        foreach (GridColumn column in Grid1.AllColumns)
                        {
                            Grid1.Rows[i].CellCssClasses[column.ColumnIndex] = "f-grid-cell-uneditable";
                        }
                    }
                }
                if (controlPointType != "C")   //非C级
                {
                    if (c.ControlPoint.Contains("C"))
                    {
                        AspNet.CheckBox ckbWorkPackageCode = (AspNet.CheckBox)(this.Grid1.Rows[i].FindControl("cbSelect"));
                        ckbWorkPackageCode.Enabled = false;
                        foreach (GridColumn column in Grid1.AllColumns)
                        {
                            Grid1.Rows[i].CellCssClasses[column.ColumnIndex] = "f-grid-cell-uneditable";
                        }
                        Grid1.Rows[i].RowCssClass = " Yellow ";
                    }
                }
                else    //C级
                {
                    if (!c.ControlPoint.Contains("C"))
                    {
                        AspNet.CheckBox ckbWorkPackageCode = (AspNet.CheckBox)(this.Grid1.Rows[i].FindControl("cbSelect"));
                        ckbWorkPackageCode.Enabled = false;
                        foreach (GridColumn column in Grid1.AllColumns)
                        {
                            Grid1.Rows[i].CellCssClasses[column.ColumnIndex] = "f-grid-cell-uneditable";
                        }
                        Grid1.Rows[i].RowCssClass = " Yellow ";
                    }
                }
            }
        }
        #endregion

        #region 全选事件
        protected void cbAll_CheckedChanged(object sender, CheckedEventArgs e)
        {
            //保存页面数据
            BindGrid();
            for (int i = 0; i < this.Grid1.Rows.Count; i++)
            {
                AspNet.CheckBox ckbWorkPackageCode = (AspNet.CheckBox)(this.Grid1.Rows[i].FindControl("cbSelect"));
                if (ckbWorkPackageCode.Enabled == true)
                {
                    ckbWorkPackageCode.Checked = e.Checked;
                }
            }
        }
        #endregion

        #region 保存事件
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string ids = string.Empty;
            bool res = false;
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                AspNet.CheckBox ckb = (AspNet.CheckBox)(this.Grid1.Rows[i].FindControl("cbSelect"));
                if (ckb.Checked)
                {
                    res = true;
                    break;
                }
            }
            if (SelectedList.Count == 0 && !res)
            {
                Alert.ShowInTop("没有选中任何项！", MessageBoxIcon.Warning);
                return;
            }
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                AspNet.CheckBox ckb = (AspNet.CheckBox)(this.Grid1.Rows[i].FindControl("cbSelect"));
                if (ckb.Checked)
                {
                    SelectedList.Add(this.Grid1.Rows[i].RowID);
                }
                else
                {
                    if (SelectedList.Contains(this.Grid1.Rows[i].RowID))
                    {
                        SelectedList.Remove(this.Grid1.Rows[i].RowID);
                    }
                    NoSelectedList.Add(this.Grid1.Rows[i].RowID);
                }
            }
            foreach (var item in SelectedList.Distinct())
            {
                ids += item + ",";
            }
            if (!string.IsNullOrEmpty(ids))
            {
                ids = ids.Substring(0, ids.LastIndexOf(","));
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(ids) + ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        //<summary>
        //获取文本
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        public static string ConvertText(object Str)
        {
            string s = string.Empty;
            if (Str != null)
            {
                string[] strs = Str.ToString().Split(',');
                foreach (var item in strs)
                {
                    s += item + "<br/>";
                }
                if (!string.IsNullOrEmpty(s))
                {
                    s = s.Substring(0, s.Length - 5);
                }
            }
            return s;
        }
    }
}