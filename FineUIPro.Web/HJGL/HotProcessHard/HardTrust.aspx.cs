using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;

namespace FineUIPro.Web.HJGL.HotProcessHard
{
    public partial class HardTrust : PageBase
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
                this.ddlPageSize.SelectedValue = this.Grid1.PageSize.ToString();
                this.HardTrustID = string.Empty;
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
            var totalUnitWork = from x in Funs.DB.WBS_UnitWork select x;
            var totalUnit = from x in Funs.DB.Project_ProjectUnit select x;

            ////单位工程
            var pUnitWork = (from x in totalUnitWork where x.ProjectId == this.CurrUser.LoginProjectId select x).ToList();
            ////单位
            var pUnits = (from x in totalUnit where x.ProjectId == this.CurrUser.LoginProjectId select x).ToList();

            pUnits = (from x in pUnits
                      join y in pUnitWork on x.UnitId equals y.UnitId
                      select x).Distinct().ToList();
            this.BindNodes(null, null, pUnitWork, pUnits);

            //// 装置
            //var pInstallation = (from x in Funs.DB.Project_Installation where x.ProjectId == this.CurrUser.LoginProjectId select x).ToList();
            //// 单位
            //var pUnits = (from x in Funs.DB.Project_Unit where x.ProjectId == this.CurrUser.LoginProjectId select x).ToList();

            //List<Model.HJGL_HotProess_Trust> trustLists = new List<Model.HJGL_HotProess_Trust>(); ///热处理委托单
            //if (!string.IsNullOrEmpty(this.txtSearchNo.Text.Trim()))
            //{
            //    trustLists = (from x in Funs.DB.HJGL_HotProess_Trust where x.HotProessTrustNo.Contains(this.txtSearchNo.Text.Trim()) orderby x.HotProessTrustNo select x).ToList();
            //}
            //else
            //{
            //    trustLists = (from x in Funs.DB.HJGL_HotProess_Trust orderby x.HotProessTrustNo select x).ToList();
            //}

            //BindNodes(null, pInstallation, pUnits);
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
            else
            {


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
        }

        //绑定子节点
        private void BindChildNodes(TreeNode ChildNodes, List<Model.WBS_UnitWork> pUnitWork, List<Model.Project_ProjectUnit> pUnits)
        {
            if (ChildNodes.CommandName == "单位工程")
            {
                List<Model.HJGL_Hard_Trust> trustLists = new List<Model.HJGL_Hard_Trust>();

                if (!string.IsNullOrEmpty(this.txtSearchNo.Text.Trim()))
                {
                    trustLists = (from x in Funs.DB.HJGL_Hard_Trust where x.HardTrustNo.Contains(this.txtSearchNo.Text.Trim()) orderby x.HardTrustNo select x).ToList();
                }
                else
                {
                    trustLists = (from x in Funs.DB.HJGL_Hard_Trust orderby x.HardTrustNo select x).ToList();
                }

                string[] units = ChildNodes.NodeID.Split('|');
                var trustList = from x in trustLists
                                where x.ProjectId == this.CurrUser.LoginProjectId
                                      && x.UnitWorkId == ChildNodes.NodeID
                                select x;
                foreach (var item in trustList)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = item.HardTrustNo;
                    newNode.NodeID = item.HardTrustID;
                    newNode.ToolTip = item.HardTrustNo;
                    newNode.CommandName = "委托单号";
                    newNode.EnableClickEvent = true;
                    ChildNodes.Nodes.Add(newNode);
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
            this.HardTrustID = tvControlItem.SelectedNodeID;
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
            this.SetTextTemp();
            this.PageInfoLoad(); ///页面输入提交信息
            string strSql = string.Empty;
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.CommandName == "委托单号")
            {
                strSql = @"SELECT * "
                     + @" FROM dbo.View_HJGL_Hard_TrustItem AS Trust"
                     + @" WHERE Trust.HardTrustID=@HardTrustID";
                listStr.Add(new SqlParameter("@HardTrustID", this.HardTrustID));
            }
            if (!string.IsNullOrEmpty(this.txtPipelineCode.Text.Trim()))
            {
                strSql += @" and Trust.PipelineCode like @PipelineCode ";
                listStr.Add(new SqlParameter("@PipelineCode", "%" + this.txtPipelineCode.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtWeldJointCode.Text.Trim()))
            {
                strSql += @" and Trust.WeldJointCode like @WeldJointCode ";
                listStr.Add(new SqlParameter("@WeldJointCode", "%" + this.txtWeldJointCode.Text.Trim() + "%"));
            }
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

        #region 加载页面输入提交信息
        /// <summary>
        /// 加载页面输入提交信息
        /// </summary>
        private void PageInfoLoad()
        {
            this.SimpleForm1.Reset(); ///重置所有字段
            var trust = Funs.DB.View_HJGL_Hard_Trust.FirstOrDefault(x => x.HardTrustID == this.HardTrustID);
            if (trust != null)
            {
                this.txtHardTrustNo.Text = trust.HardTrustNo;
                this.txtCheckUnit.Text = trust.CheckUnitName;
                this.txtHardTrustMan.Text = trust.HardTrustManName;
                if (trust.HardTrustDate != null)
                {
                    this.txtHardTrustDate.Text = string.Format("{0:yyyy-MM-dd}", trust.HardTrustDate);
                }
                this.txtHardnessMethod.Text = trust.HardnessMethod;
                this.txtHardnessRate.Text = trust.HardnessRate;
                this.txtStandards.Text = trust.Standards;
                this.txtInspectionNum.Text = trust.InspectionNum;
                this.txtCheckNum.Text = trust.CheckNum;
                this.txtTestWeldNum.Text = trust.TestWeldNum;
                this.txtSendee.Text = trust.Sendee;
                this.txtDetectionTime.Text = trust.DetectionTimeStr;
            }
        }
        #endregion

        /// <summary>
        /// 情况
        /// </summary>
        private void SetTextTemp()
        {
            this.txtHardTrustNo.Text = string.Empty;
            this.txtCheckUnit.Text = string.Empty;
            this.txtHardTrustMan.Text = string.Empty;
            this.txtHardTrustDate.Text = string.Empty;
            this.txtHardnessMethod.Text = string.Empty;
            this.txtHardnessRate.Text = string.Empty;
            this.txtStandards.Text = string.Empty;
            this.txtInspectionNum.Text = string.Empty;
            this.txtCheckNum.Text = string.Empty;
            this.txtTestWeldNum.Text = string.Empty;
            this.txtSendee.Text = string.Empty;
            this.txtDetectionTime.Text = string.Empty;
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

        #region 硬度委托 维护事件
        /// <summary>
        /// 增加硬度委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_HotHardManageEditMenuId, Const.BtnAdd))
            {
                if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.CommandName == "单位工程")
                {
                    this.SetTextTemp();
                    string window = String.Format("HardTrustEdit.aspx?workAreaId={0}", tvControlItem.SelectedNodeID, "新增 - ");
                    PageContext.RegisterStartupScript(Window1.GetSaveStateReference(this.hdHardTrustID.ClientID)
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

        #region 编辑硬度委托
        /// <summary>
        /// 编辑硬度委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_HotHardManageEditMenuId, Const.BtnSave))
            {
                if (this.tvControlItem.SelectedNode != null)
                {
                    Model.HJGL_Hard_Trust trust = BLL.Hard_TrustService.GetHardTrustById(this.tvControlItem.SelectedNodeID);
                    if (trust != null)
                    {
                        string window = String.Format("HardTrustEdit.aspx?HardTrustID={0}", this.HardTrustID, "编辑 - ");
                        PageContext.RegisterStartupScript(Window1.GetSaveStateReference(this.hdHardTrustID.ClientID)
                          + Window1.GetShowReference(window));
                    }
                    else
                    {
                        ShowNotify("请至少选择一条记录", MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    ShowNotify("请至少选择一条记录", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 删除硬度委托
        /// <summary>
        /// 删除硬度委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_HotHardManageEditMenuId, Const.BtnDelete))
            {
                if (this.tvControlItem.SelectedNode != null)
                {
                    Model.HJGL_Hard_Trust trust = BLL.Hard_TrustService.GetHardTrustById(this.tvControlItem.SelectedNodeID);
                    if (trust != null)
                    {
                        var hardTrustItems = BLL.Hard_TrustItemService.GetHardTrustItemByHardTrustId(this.HardTrustID);
                        foreach (var hardTrustItem in hardTrustItems)
                        {
                            //更新热处理委托明细的口已做硬度委托
                            Model.HJGL_HotProess_TrustItem hotProessTrustItem = BLL.HotProessTrustItemService.GetHotProessTrustItemById(hardTrustItem.HotProessTrustItemId);
                            if (hotProessTrustItem != null)
                            {
                                hotProessTrustItem.IsTrust = null;
                                BLL.HotProessTrustItemService.UpdateHotProessTrustItem(hotProessTrustItem);
                            }
                            //删除硬度报告记录
                            BLL.Hard_ReportService.DeleteHard_ReportsByHardTrustItemID(hardTrustItem.HardTrustItemID);
                        }
                        BLL.Hard_TrustItemService.DeleteHardTrustItemById(this.HardTrustID);
                        BLL.Hard_TrustService.DeleteHardTrustById(this.HardTrustID);
                        //BLL.Sys_LogService.AddLog(BLL.Const.System_3, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Resources.Lan.DeleteHardTrust);
                        Alert.ShowInTop("删除成功！", MessageBoxIcon.Success);
                        this.InitTreeMenu();
                        this.Grid1.DataSource = null;
                        this.Grid1.DataBind();
                        this.SetTextTemp();
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

        #region 关闭弹出窗口及刷新页面
        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            this.HardTrustID = this.hdHardTrustID.Text;
            this.BindGrid();
            //this.InitTreeMenu();
            this.hdHardTrustID.Text = string.Empty;
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
    }
}