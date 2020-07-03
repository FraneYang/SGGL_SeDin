using System;
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
        /// 检测单主键
        /// </summary>
        public string NDEID
        {
            get
            {
                return (string)ViewState["NDEID"];
            }
            set
            {
                ViewState["NDEID"] = value;
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
                this.txtNDEDateMonth.Text = string.Format("{0:yyyy-MM}", DateTime.Now);
                this.ddlPageSize.SelectedValue = this.Grid1.PageSize.ToString();
                this.NDEID = string.Empty;
                this.InitTreeMenu();//加载树
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

            //var totalInstallation = from x in Funs.DB.Project_Installation select x;
            var totalUnitWork = from x in Funs.DB.WBS_UnitWork select x;
            var totalUnit = from x in Funs.DB.Project_ProjectUnit select x;

            ////装置
            //var pInstallation = (from x in totalInstallation where x.ProjectId == this.CurrUser.LoginProjectId select x).ToList();
            ////区域
            var pUnitWork = (from x in totalUnitWork where x.ProjectId == this.CurrUser.LoginProjectId select x).ToList();
            ////单位
            var pUnits = (from x in totalUnit where x.ProjectId == this.CurrUser.LoginProjectId select x).ToList();


            //pInstallation = (from x in pInstallation
            //                 join y in pWorkArea on x.InstallationId equals y.InstallationId
            //                 select x).Distinct().ToList();
            pUnits = (from x in pUnits
                      join y in pUnitWork on x.UnitId equals y.UnitId
                      select x).Distinct().ToList();
            this.BindNodes(null,null, pUnitWork, pUnits);
            //DateTime startTime = Convert.ToDateTime(this.txtNDEDateMonth.Text.Trim() + "-01");
            //DateTime endTime = startTime.AddMonths(1);

            //List<Model.Base_Unit> units = new List<Model.Base_Unit>();
            //Model.Project_Unit pUnit = BLL.Project_UnitService.GetProject_UnitByProjectIdUnitId(this.CurrUser.LoginProjectId, this.CurrUser.UnitId);
            //if (pUnit == null || pUnit.UnitType == BLL.Const.UnitType_1 || pUnit.UnitType == BLL.Const.UnitType_2
            //   || pUnit.UnitType == BLL.Const.UnitType_3 || pUnit.UnitType == BLL.Const.UnitType_4)
            //{
            //    units = (from x in Funs.DB.Base_Unit
            //             join y in Funs.DB.Project_Unit on x.UnitId equals y.UnitId
            //             where y.ProjectId == this.CurrUser.LoginProjectId && y.UnitType.Contains(BLL.Const.UnitType_5)
            //             select x).ToList();
            //}
            //else
            //{
            //    units.Add(BLL.Base_UnitService.GetUnit(this.CurrUser.UnitId));
            //}
            //if (units != null)
            //{
            //    foreach (var unit in units)
            //    {
            //        TreeNode newNode = new TreeNode();//定义根节点
            //        newNode.Text = unit.UnitCode;
            //        newNode.NodeID = unit.UnitId;
            //        newNode.ToolTip = unit.UnitName;
            //        newNode.Expanded = true;
            //        newNode.CommandName = "Unit";
            //        this.tvControlItem.Nodes.Add(newNode);
            //        this.BindNodes(newNode, startTime, endTime);
            //    }
            //}
            //else
            //{
            //    Alert.ShowInTop(Resources.Lan.PleaseAddUnitFirst, MessageBoxIcon.Warning);
            //}

        }

        /// <summary>
        ///  绑定树节点
        /// </summary>
        /// <param name="node"></param>
        private void BindNodes(TreeNode node1, TreeNode node2, List<Model.WBS_UnitWork> pUnitWork, List<Model.Project_ProjectUnit> pUnits)
        {
            var pUnitDepth = pUnits.FirstOrDefault(x => x.UnitId == this.CurrUser.UnitId);
            if (node1 == null && node2 == null)
            {
                TreeNode rootNode1 = new TreeNode();
                rootNode1.NodeID = "1";
                rootNode1.Text = "建筑工程";
                rootNode1.CommandName = "建筑工程";
                this.tvControlItem.Nodes.Add(rootNode1);

                TreeNode rootNode2 = new TreeNode();
                rootNode2.NodeID = "2";
                rootNode2.Text = "安装工程";
                rootNode2.CommandName = "安装工程";
                rootNode2.Expanded = true;
                this.tvControlItem.Nodes.Add(rootNode2);

                this.BindNodes(rootNode1, rootNode2, pUnitWork, pUnits);
            }
            else {
                if (node1.CommandName == "建筑工程")
                {
                    List<Model.WBS_UnitWork> workAreas = null;
                    if (pUnitDepth == null || pUnitDepth.UnitType.Contains(Const.ProjectUnitType_1) || pUnitDepth.UnitType.Contains(Const.ProjectUnitType_5))
                    {
                        workAreas = (from x in pUnitWork
                                     join y in pUnits on x.UnitId equals y.UnitId
                                     where x.ProjectType == node1.NodeID && y.UnitType.Contains(Const.ProjectUnitType_2)
                                     select x).ToList();
                    }
                    else
                    {
                        workAreas = (from x in pUnitWork
                                     join y in pUnits on x.UnitId equals y.UnitId
                                     where x.ProjectType == node1.NodeID && y.UnitType.Contains(Const.ProjectUnitType_2)
                                     && x.UnitId == this.CurrUser.UnitId
                                     select x).ToList();
                    }

                    workAreas = workAreas.OrderByDescending(x => x.UnitWorkCode).ToList();
                    foreach (var q in workAreas)
                    {
                        var u = BLL.UnitService.GetUnitByUnitId(q.UnitId);
                        TreeNode newNode = new TreeNode();
                        newNode.Text = q.UnitWorkName;
                        newNode.NodeID = q.UnitWorkId;
                        newNode.ToolTip = "施工单位：" + u.UnitName;
                        newNode.CommandName = "单位工程";
                        newNode.EnableExpandEvent = true;
                        node1.Nodes.Add(newNode);
                        BindChildNodes(newNode, pUnitWork, pUnits);
                    }
                }
                if (node2.CommandName == "安装工程")
                {
                    List<Model.WBS_UnitWork> workAreas = null;
                    if (pUnitDepth == null || pUnitDepth.UnitType.Contains(Const.ProjectUnitType_1) || pUnitDepth.UnitType.Contains(Const.ProjectUnitType_5))
                    {
                        workAreas = (from x in pUnitWork
                                     join y in pUnits on x.UnitId equals y.UnitId
                                     where x.ProjectType == node2.NodeID && y.UnitType.Contains(Const.ProjectUnitType_2)
                                     select x).ToList();
                    }
                    else
                    {
                        workAreas = (from x in pUnitWork
                                     join y in pUnits on x.UnitId equals y.UnitId
                                     where x.ProjectType == node2.NodeID && y.UnitType.Contains(Const.ProjectUnitType_2)
                                     && x.UnitId == this.CurrUser.UnitId
                                     select x).ToList();
                    }

                    workAreas = workAreas.OrderByDescending(x => x.UnitWorkCode).ToList();
                    foreach (var q in workAreas)
                    {
                        var u = BLL.UnitService.GetUnitByUnitId(q.UnitId);
                        TreeNode newNode = new TreeNode();
                        newNode.Text = q.UnitWorkName;
                        newNode.NodeID = q.UnitWorkId;
                        newNode.ToolTip = "施工单位：" + u.UnitName;
                        newNode.CommandName = "单位工程";
                        newNode.EnableExpandEvent = true;
                        node2.Nodes.Add(newNode);
                        BindChildNodes(newNode, pUnitWork, pUnits);
                    }
                }
            }
            
            //if (node.CommandName == "Unit")
            //{
            //    ///装置
            //    var install = (from x in Funs.DB.Project_Installation
            //                   join y in Funs.DB.HJGL_Batch_NDE on x.InstallationId equals y.InstallationId
            //                   where y.UnitId == node.NodeID && x.ProjectId==this.CurrUser.LoginProjectId
            //                   orderby x.InstallationCode
            //                   select x).Distinct();
            //    foreach (var q in install)
            //    {
            //        TreeNode newNode = new TreeNode();
            //        newNode.Text = q.InstallationName;
            //        newNode.NodeID = q.InstallationId.ToString();
            //        newNode.Expanded = true;
            //        newNode.CommandName = "Installation";
            //        node.Nodes.Add(newNode);
            //        BindNodes(newNode, startTime, endTime);
            //    }
            //}
            //else if (node.CommandName == "Installation")
            //{
            //    var types = (from x in Funs.DB.View_Batch_NDE
            //                 join y in Funs.DB.Base_DetectionType
            //                 on x.DetectionTypeId equals y.DetectionTypeId
            //                 where x.UnitId == node.ParentNode.NodeID && x.InstallationId == node.NodeID
            //                       && x.ProjectId==this.CurrUser.LoginProjectId
            //                 orderby x.DetectionTypeCode
            //                 select y).Distinct();
            //    //var u = BLL.Base_UnitService.GetUnit(node.ParentNode.NodeID);
            //    foreach (var q in types)
            //    {
            //        TreeNode newNode = new TreeNode();
            //        newNode.Text = q.DetectionTypeCode; // + "(" + u.UnitCode + ")";
            //        newNode.NodeID = q.DetectionTypeId + "|" + node.ParentNode.NodeID;
            //        newNode.ToolTip = "DetectionType";
            //        node.Nodes.Add(newNode);
            //        BindNodes(newNode, startTime, endTime);
            //    }
            //}
            //else if (node.ToolTip == "DetectionType")
            //{
            //    //单号
            //    string ndtTypeId = node.NodeID.Split('|')[0];
            //    var checks = from x in Funs.DB.View_Batch_NDE
            //                 where x.NDEDate < Convert.ToDateTime(this.txtNDEDateMonth.Text.Trim() + "-01").AddMonths(1)
            //                 && x.NDEDate >= Convert.ToDateTime(this.txtNDEDateMonth.Text.Trim() + "-01")
            //                 && x.ProjectId == this.CurrUser.LoginProjectId && x.NDECode.Contains(this.txtSearchCode.Text.Trim())
            //                 && x.InstallationId.ToString() == node.ParentNode.NodeID
            //                 && x.UnitId == node.ParentNode.ParentNode.NodeID
            //                 && x.DetectionTypeId == ndtTypeId
            //                 orderby x.NDECode descending
            //                 select x;
            //    foreach (var check in checks)
            //    {
            //        TreeNode newNode = new TreeNode();
            //        if (!check.AuditDate.HasValue)
            //        {
            //            newNode.Text = "<font color='#EE0000'>" + check.NDECode + "</font>";
            //        }
            //        else
            //        {
            //            newNode.Text = check.NDECode;
            //        }
            //        newNode.NodeID = check.NDEID;
            //        newNode.ToolTip = "check";
            //        newNode.EnableClickEvent = true;
            //        node.Nodes.Add(newNode);
            //    }
            //}
        }
        //绑定子节点
        private void BindChildNodes(TreeNode ChildNodes, List<Model.WBS_UnitWork> pUnitWork, List<Model.Project_ProjectUnit> pUnits)
        {
            if (ChildNodes.CommandName == "单位工程")
            {
                var p = from x in Funs.DB.HJGL_Batch_NDE
                        where x.UnitWorkId == ChildNodes.NodeID
&& x.NDEDate < Convert.ToDateTime(this.txtNDEDateMonth.Text.Trim() + "-01").AddMonths(1)
&& x.NDEDate >= Convert.ToDateTime(this.txtNDEDateMonth.Text.Trim() + "-01")
                        select x;
                if (p.Count() > 0)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = "探伤类型";
                    newNode.NodeID = "探伤类型";
                    ChildNodes.Nodes.Add(newNode);
                }
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
                    var types = (from x in Funs.DB.View_Batch_NDE
                                 join y in Funs.DB.Base_DetectionType
                                 on x.DetectionTypeId equals y.DetectionTypeId
                                 where  x.ProjectId == this.CurrUser.LoginProjectId
                                       && x.UnitWorkId == e.Node.NodeID
                                       && x.DetectionTypeId == item.DetectionTypeId
                                 orderby x.DetectionTypeCode
                                 select y).Distinct();
                   
                    TreeNode newNode = new TreeNode();
                    if (types.Count() > 0)
                    {
                        newNode.Text = item.DetectionTypeCode;
                        newNode.NodeID = item.DetectionTypeId + "|" + e.Node.NodeID;
                        newNode.EnableExpandEvent = true;
                        newNode.ToolTip = item.DetectionTypeName;
                        newNode.CommandName = "探伤类型";
                        e.Node.Nodes.Add(newNode);
                    }

                    TreeNode tn1 = new TreeNode
                    {
                        Text = "检测单号",
                        NodeID = "检测单号",
                    };
                    newNode.Nodes.Add(tn1);
                }
            }

            if (e.Node.CommandName == "探伤类型")
            {
                ///单号
                string ndtTypeId = e.Node.NodeID.Split('|')[0];
                var checks = from x in Funs.DB.View_Batch_NDE
                             where x.NDEDate < Convert.ToDateTime(this.txtNDEDateMonth.Text.Trim() + "-01").AddMonths(1)
                             && x.NDEDate >= Convert.ToDateTime(this.txtNDEDateMonth.Text.Trim() + "-01")
                             && x.ProjectId == this.CurrUser.LoginProjectId && x.NDECode.Contains(this.txtSearchCode.Text.Trim())
                             && x.UnitWorkId.ToString() == e.Node.ParentNode.NodeID
                             && x.DetectionTypeId == ndtTypeId
                             orderby x.NDECode descending
                             select x;
                foreach (var check in checks)
                {
                    TreeNode newNode = new TreeNode();
                    if (!check.AuditDate.HasValue)
                    {
                        newNode.Text = "<font color='#EE0000'>" + check.NDECode + "</font>";
                    }
                    else
                    {
                        newNode.Text = check.NDECode;
                    }
                    newNode.NodeID = check.NDEID;
                    newNode.ToolTip = "check";
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
            this.NDEID = tvControlItem.SelectedNodeID;
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
            if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.ToolTip == "check")
            {
                this.SetTextTemp();
                this.PageInfoLoad(); ///页面输入提交信息
                string strSql = @"SELECT * FROM dbo.View_Batch_NDEItem d WHERE NDEID=@NDEID ";
                List<SqlParameter> listStr = new List<SqlParameter>
                {

                };
                listStr.Add(new SqlParameter("@NDEID", this.tvControlItem.SelectedNodeID));
                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

                // 2.获取当前分页数据
                //var table = this.GetPagedDataTable(Grid1, tb1);
                Grid1.RecordCount = tb.Rows.Count;
                tb = GetFilteredTable(Grid1.FilteredData, tb);
                var table = this.GetPagedDataTable(Grid1, tb);
                Grid1.DataSource = table;
                Grid1.DataBind();
                for (int i = 0; i < this.Grid1.Rows.Count; i++)
                {
                    string id = Grid1.DataKeys[i][0].ToString();
                    Model.HJGL_Batch_NDEItem item = BLL.Batch_NDEItemService.GetNDEItemById(id);
                    if (item != null)
                    {
                        if (item.SubmitDate == null)    //未审核
                        {
                            this.Grid1.Rows[i].CellCssClasses[15] = "f-grid-cell-uneditable";
                        }
                    }
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
            var check = Funs.DB.View_Batch_NDE.FirstOrDefault(x => x.NDEID == this.NDEID);
            if (check != null)
            {
                this.txtUnitName.Text = check.UnitName;
                //this.txtInstallation.Text = check.InstallationName;
                this.txtCheckUnit.Text = check.NDEUnitName;
                this.txtDetectionTypeCode.Text = check.DetectionTypeCode;
                if (check.NDEDate != null)
                {
                    this.txtNDEDate.Text = string.Format("{0:yyyy-MM-dd}", check.NDEDate);
                }
                this.txtTrustBatchCode.Text = check.TrustBatchCode;
            }
        }
        #endregion

        /// <summary>
        /// 情况
        /// </summary>
        private void SetTextTemp()
        {
            this.txtUnitName.Text = string.Empty;
            this.txtInstallation.Text = string.Empty;
            this.txtCheckUnit.Text = string.Empty;
            this.txtDetectionTypeCode.Text = string.Empty;
            this.txtNDEDate.Text = string.Empty;
            this.txtTrustBatchCode.Text = string.Empty;
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
                if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.CommandName == "区域")
                {
                    this.SetTextTemp();
                    string window = String.Format("NDTBatchEdit.aspx?workAreaId={0}", tvControlItem.SelectedNodeID, "新增 - ");
                    PageContext.RegisterStartupScript(Window1.GetSaveStateReference(this.hdNDEID.ClientID)
                      + Window1.GetShowReference(window));
                }
                else
                {
                    ShowNotify("请选择区域！", MessageBoxIcon.Warning);
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
                if (this.tvControlItem.SelectedNode != null)
                {
                    Model.HJGL_Batch_NDE check = BLL.Batch_NDEService.GetNDEById(this.tvControlItem.SelectedNodeID);
                    if (check != null)
                    {
                        string window = String.Format("NDTBatchEdit.aspx?NDEID={0}", this.NDEID, "编辑 - ");
                        PageContext.RegisterStartupScript(Window1.GetSaveStateReference(this.hdNDEID.ClientID)
                          + Window1.GetShowReference(window));
                    }
                    else
                    {
                        ShowNotify("请选择要编辑的记录", MessageBoxIcon.Warning);
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
                    Model.HJGL_Batch_NDE check = BLL.Batch_NDEService.GetNDEById(this.tvControlItem.SelectedNodeID);
                    if (check != null)
                    {
                        string trustId = check.TrustBatchId;
                        if (judgementDelete(this.tvControlItem.SelectedNodeID))
                        {
                            BLL.Batch_NDEItemService.DeleteNDEItemById(this.NDEID);
                            BLL.Batch_NDEService.DeleteNDEById(this.NDEID);
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
                            ShowNotify("不能删除，明细检测已有审核！", MessageBoxIcon.Warning);
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
                string ndtItem = Grid1.SelectedRowID;
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
                Alert.ShowInTop(content, MessageBoxIcon.Error);
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
            this.NDEID = this.hdNDEID.Text;
            this.BindGrid();
            //this.InitTreeMenu();
            this.hdNDEID.Text = string.Empty;
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
                HttpCookie lanCookie = Request.Cookies["SelectLan"];
                if (lanCookie["lan"] == "zh-CN")   //中文
                {
                    str = BLL.Base_DefectService.GetDefectNameStrByDefectIdStr(CheckDefects.ToString());
                }
                else
                {
                    str = BLL.Base_DefectService.GetDefectEngNameStrByDefectIdStr(CheckDefects.ToString());
                }
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
                List<Model.HJGL_Batch_NDEItem> GetNDEItem = BLL.Batch_NDEItemService.GetNDEItemByNDEID(this.NDEID);
                Model.SGGLDB db = Funs.DB;
                //全部记录都已录入探伤报告编号
                var isNull = GetNDEItem.FirstOrDefault(x => x.NDEReportNo == null);
                if (isNull == null)
                {
                    foreach (var item in GetNDEItem)
                    {
                        if (!item.SubmitDate.HasValue)
                        {
                            if (!string.IsNullOrEmpty(item.CheckResult) && !String.IsNullOrEmpty(item.NDEReportNo))
                            {
                                var ndt = BLL.Base_DetectionTypeService.GetDetectionTypeByDetectionTypeId(item.DetectionTypeId);
                                if (ndt.DetectionTypeCode.Contains("RT") && (!item.PassFilm.HasValue || !item.TotalFilm.HasValue))
                                {
                                    ShowNotify("请填写拍片总数和拍片合格数！", MessageBoxIcon.Warning);
                                    return;
                                }

                                if (item.TotalFilm < item.PassFilm)
                                {
                                    ShowNotify("拍片合格数不能大于拍片总数！", MessageBoxIcon.Warning);
                                    return;
                                }
                            }

                            item.SubmitDate = DateTime.Now;
                            BLL.Batch_NDEItemService.UpdateNDEItem(item);
                        }
                    }
                }
                else
                {
                    ShowNotify("所有记录需填写探伤报告编号后才可审核！", MessageBoxIcon.Warning);
                    return;
                }

                Model.HJGL_Batch_NDE nde = BLL.Batch_NDEService.GetNDEById(this.NDEID);
                if (nde != null)
                {
                    int trustItemCount = BLL.Batch_BatchTrustItemService.GetBatchTrustItemByTrustBatchId(nde.TrustBatchId).Count;
                    int checkItemCount = BLL.Batch_NDEItemService.GetNDEItemByNDEID(this.NDEID).Count;
                    int noResultCheckItemCount = (from x in Funs.DB.HJGL_Batch_NDEItem where x.NDEID == this.NDEID && x.CheckResult == null select x).Count();
                    if (trustItemCount == checkItemCount && noResultCheckItemCount == 0)  //全部检测结果录入完毕，更新字段
                    {
                        BLL.Batch_BatchTrustService.UpdatTrustBatchtState(nde.TrustBatchId, true);
                    }
                }

                ShowNotify("审核成功！", MessageBoxIcon.Success);
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