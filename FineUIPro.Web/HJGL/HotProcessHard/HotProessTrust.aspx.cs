using System;
using System.Collections.Generic;
using System.Linq;
using BLL;
using System.Data.SqlClient;
using System.Data;

namespace FineUIPro.Web.HJGL.HotProcessHard
{
    public partial class HotProessTrust : PageBase
    {
        #region 定义项
        /// <summary>
        /// 热处理委托主键
        /// </summary>
        public string HotProessTrustId
        {
            get
            {
                return (string)ViewState["HotProessTrustId"];
            }
            set
            {
                ViewState["HotProessTrustId"] = value;
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

                this.HotProessTrustId = string.Empty;
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
                List<Model.HJGL_HotProess_Trust> trustLists = new List<Model.HJGL_HotProess_Trust>();

                if (!string.IsNullOrEmpty(this.txtSearchNo.Text.Trim()))
                {
                    trustLists = (from x in Funs.DB.HJGL_HotProess_Trust where x.HotProessTrustNo.Contains(this.txtSearchNo.Text.Trim()) orderby x.HotProessTrustNo select x).ToList();
                }
                else
                {
                    trustLists = (from x in Funs.DB.HJGL_HotProess_Trust orderby x.HotProessTrustNo select x).ToList();
                }
                string[] units = ChildNodes.NodeID.Split('|');
                var trustList = from x in trustLists
                                where x.ProjectId == this.CurrUser.LoginProjectId
                                      && x.UnitWorkId == ChildNodes.NodeID
                                select x;
                foreach (var item in trustList)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = item.HotProessTrustNo;
                    newNode.NodeID = item.HotProessTrustId;
                    newNode.ToolTip = item.HotProessTrustNo;
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
            this.BindGrid();
        }
        #endregion

        #region DropDownList下拉选择事件
        /// <summary>
        /// 项目下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpProjectId_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.InitTreeMenu();
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            string strSql = string.Empty;
            List<SqlParameter> listStr = new List<SqlParameter>();
            this.SetTextTemp();
            this.PageInfoLoad(); ///页面输入提交信息

            if (this.tvControlItem.SelectedNode!=null && this.tvControlItem.SelectedNode.CommandName== "委托单号")
            {
                var hotProessTrust = BLL.HotProess_TrustService.GetHotProessTrustById(this.tvControlItem.SelectedNodeID);
                if (hotProessTrust != null)
                {
                    this.HotProessTrustId = hotProessTrust.HotProessTrustId;
                    strSql = @"SELECT * "
                    + @" FROM dbo.View_HJGL_HotProess_TrustItem AS Trust"
                    + @" WHERE Trust.ProjectId= @ProjectId AND Trust.HotProessTrustId=@HotProessTrustId ";

                    listStr.Add(new SqlParameter("@ProjectId", hotProessTrust != null ? hotProessTrust.ProjectId : this.CurrUser.LoginProjectId));
                    listStr.Add(new SqlParameter("@HotProessTrustId", this.HotProessTrustId));

                    if (!string.IsNullOrEmpty(this.txtIsoNo.Text.Trim()))
                    {
                        strSql += @" and Trust.PipelineCode like '%'+@PipelineCode+'%' ";
                        listStr.Add(new SqlParameter("@PipelineCode", this.txtIsoNo.Text.Trim()));
                    }

                    SqlParameter[] parameter = listStr.ToArray();
                    DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
                    // 2.获取当前分页数据
                    //var table = this.GetPagedDataTable(Grid1, tb1);
                    Grid1.RecordCount = tb.Rows.Count;
                    //tb = GetFilteredTable(Grid1.FilteredData, tb);
                    var table = this.GetPagedDataTable(Grid1, tb);
                    Grid1.DataSource = table;
                    Grid1.DataBind();
                }
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        #region 加载页面输入提交信息
        /// <summary>
        /// 加载页面输入提交信息
        /// </summary>
        private void PageInfoLoad()
        {
            var trust = BLL.HotProess_TrustService.GetHotProessTrustById(this.HotProessTrustId);
            if (trust != null)
            {
                this.txtHotProessTrustNo.Text = trust.HotProessTrustNo;
                if (trust.ProessDate.HasValue)
                {
                    this.txtProessDate.Text = string.Format("{0:yyyy-MM-dd}", trust.ProessDate);
                }
                this.txtProessMethod.Text = trust.ProessMethod;
                this.txtProessEquipment.Text = trust.ProessEquipment;
                if (!string.IsNullOrEmpty(trust.Tabler))
                {
                    this.txtTabler.Text = BLL.UserService.GetUserNameByUserId(trust.Tabler);
                }
                this.txtRemark.Text = trust.Remark;
            }
        }
        #endregion

        #region 清空文本
        /// <summary>
        /// 清空文本
        /// </summary>
        private void SetTextTemp()
        {
            this.txtHotProessTrustNo.Text = string.Empty;
            this.txtProessDate.Text = string.Empty;
            this.txtProessMethod.Text = string.Empty;
            this.txtProessEquipment.Text = string.Empty;
            this.txtTabler.Text = string.Empty;
            this.txtRemark.Text = string.Empty;
        }
        #endregion
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

        #region 热处理委托 维护事件
        /// <summary>
        /// 增加热处理委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_HotProessTrustMenuId, Const.BtnAdd))
            {
                if (this.tvControlItem.SelectedNode!=null && this.tvControlItem.SelectedNode.CommandName == "单位工程")
                {
                    this.SetTextTemp();
                    string window = String.Format("HotProessTrustEdit.aspx?workAreaId={0}", tvControlItem.SelectedNodeID, "新增 - ");
                    PageContext.RegisterStartupScript(Window1.GetSaveStateReference(this.hdHotProessTrustId.ClientID)
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

        #region 编辑热处理委托
        /// <summary>
        /// 编辑热处理委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_HotProessTrustMenuId, Const.BtnSave))
            {
                var trustManage = BLL.HotProess_TrustService.GetHotProessTrustById(this.HotProessTrustId);
                if (trustManage != null)
                {
                    string window = String.Format("HotProessTrustEdit.aspx?HotProessTrustId={0}", this.HotProessTrustId, "编辑 - ");
                    PageContext.RegisterStartupScript(Window1.GetSaveStateReference(this.hdHotProessTrustId.ClientID)
                      + Window1.GetShowReference(window));
                }
                else
                {
                    ShowNotify("请选择要修改的热处理委托记录！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 删除热处理委托
        /// <summary>
        /// 删除热处理委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_HotProessTrustMenuId, Const.BtnDelete))
            {
                var trustManage = BLL.HotProess_TrustService.GetHotProessTrustById(this.HotProessTrustId);
                if (trustManage != null)
                {
                    var hotProessItems = from x in Funs.DB.HJGL_HotProess_TrustItem where x.HotProessTrustId == this.HotProessTrustId select x;
                    foreach (var item in hotProessItems)
                    {
                        if (!string.IsNullOrEmpty(item.HotProessTrustItemId))
                        {
                            var hotReort = (from x in Funs.DB.HJGL_HotProess_Report where x.HotProessTrustItemId == item.HotProessTrustItemId select x).ToList();
                            if (hotReort.Count() > 0)
                            {
                                ShowNotify("已生成热处理报告，不能删除！", MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                    BLL.HotProessTrustItemService.DeleteHotProessTrustItemById(this.HotProessTrustId);
                    BLL.HotProess_TrustService.DeleteHotProessTrustById(this.HotProessTrustId);
                    //BLL.Sys_LogService.AddLog(BLL.Const.System_3, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Resources.Lan.DeleteHotProess);
                    Alert.ShowInTop("删除成功！", MessageBoxIcon.Success);
                    this.InitTreeMenu();
                    SetTextTemp();
                    this.Grid1.DataSource = null;
                    this.Grid1.DataBind();
                }
                else
                {
                    ShowNotify("请选择要删除的热处理委托记录！", MessageBoxIcon.Warning);
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
            this.InitTreeMenu();
            this.HotProessTrustId = this.hdHotProessTrustId.Text;
            this.tvControlItem.SelectedNodeID = this.HotProessTrustId;
            this.BindGrid();
            this.hdHotProessTrustId.Text = string.Empty;
        }

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Tree_TextChanged(object sender, EventArgs e)
        {
            this.InitTreeMenu();
        }
        #endregion
        #endregion

        #region 右键编辑热处理报告
        /// <summary>
        /// 热处理报告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuHotProessReport_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_HotProessTrustMenuId, Const.BtnSave))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HotProessReport.aspx?HotProessTrustItemId={0}", this.Grid1.SelectedRowID, "编辑热处理报告 - ")));
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}