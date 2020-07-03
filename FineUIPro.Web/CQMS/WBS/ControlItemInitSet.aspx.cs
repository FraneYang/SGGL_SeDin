using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;
using System.IO;
using System.Data.SqlClient;

namespace FineUIPro.Web.CQMS.WBS
{
    public partial class ControlItemInitSet : PageBase
    {
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
                GetButtonPower();
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

            TreeNode rootNode2 = new TreeNode();
            rootNode2.Text = "安装工程";
            rootNode2.NodeID = "2";
            rootNode2.CommandName = "ProjectType";
            rootNode2.EnableExpandEvent = true;
            rootNode2.EnableClickEvent = true;
            this.trWBS.Nodes.Add(rootNode2);
            rootNode2.Nodes.Add(emptyNode);
        }
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
                var workPackages = from x in Funs.DB.WBS_WorkPackageInit where x.ProjectType == e.NodeID && x.SuperWorkPack == null orderby x.WorkPackageCode select x;
                foreach (var workPackage in workPackages)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = workPackage.PackageContent;
                    newNode.NodeID = workPackage.WorkPackageCode;
                    newNode.CommandName = "WorkPackage";
                    newNode.EnableExpandEvent = true;
                    newNode.EnableClickEvent = true;
                    e.Node.Nodes.Add(newNode);
                    var childWorkPackages = from x in Funs.DB.WBS_WorkPackageInit where x.SuperWorkPack == workPackage.WorkPackageCode select x;
                    if (childWorkPackages.Count() > 0)
                    {
                        TreeNode emptyNode = new TreeNode();
                        emptyNode.Text = "";
                        emptyNode.NodeID = "";
                        newNode.Nodes.Add(emptyNode);
                    }
                }
            }
            else if (e.Node.CommandName == "WorkPackage")   //展开单位工程节点
            {
                var workPackages = from x in Funs.DB.WBS_WorkPackageInit where x.SuperWorkPack == e.Node.NodeID orderby x.WorkPackageCode select x;
                if (workPackages.Count() > 0)   //存在子单位工程
                {
                    foreach (var workPackage in workPackages)
                    {
                        TreeNode newNode = new TreeNode();
                        newNode.Text = workPackage.PackageContent;
                        newNode.NodeID = workPackage.WorkPackageCode;
                        newNode.CommandName = "WorkPackage";
                        newNode.EnableExpandEvent = true;
                        newNode.EnableClickEvent = true;
                        e.Node.Nodes.Add(newNode);
                        var childWorkPackages = from x in Funs.DB.WBS_WorkPackageInit where x.SuperWorkPack == workPackage.WorkPackageCode select x;
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

        #region  Tree点击事件
        /// <summary>
        /// Tree点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void trWBS_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region  修改关闭窗口
        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            ShowNotify("修改成功！", MessageBoxIcon.Success);

            GetSelectTreeNode();
        }
        #endregion

        #region  增加关闭窗口
        /// <summary>
        /// 增加关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            ShowNotify("增加成功！", MessageBoxIcon.Success);

            GetSelectTreeNode();
        }
        #endregion

        #region  关闭窗口
        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window3_Close(object sender, WindowCloseEventArgs e)
        {
            ShowNotify("保存成功！", MessageBoxIcon.Success);
            BindGrid();
        }
        #endregion

        #region 右键增加、修改、删除方法
        /// <summary>
        /// 右键修改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            if (this.trWBS.SelectedNode != null)
            {
                if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ControlItemInitSetMenuId, BLL.Const.BtnModify))
                {
                    if (this.trWBS.SelectedNode.CommandName != "ProjectType")   //非工程类型节点可以修改
                    {
                        this.hdSelectId.Text = this.trWBS.SelectedNode.NodeID;

                        string openUrl = String.Format("WorkPackageInitEdit.aspx?type=modify&Id={0}", this.trWBS.SelectedNode.NodeID, "编辑 - ");
                        PageContext.RegisterStartupScript(Window1.GetSaveStateReference(hdSelectId.ClientID)
                                    + Window1.GetShowReference(openUrl));
                    }
                    else
                    {
                        ShowNotify("工程类型节点无法修改！", MessageBoxIcon.Warning);
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
        /// 增加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuAdd_Click(object sender, EventArgs e)
        {
            if (this.trWBS.SelectedNode != null)
            {
                if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ControlItemInitSetMenuId, BLL.Const.BtnAdd))
                {
                    if (this.trWBS.SelectedNode.CommandName != "ProjectType")   //非工程类型节点可以增加
                    {
                        Model.WBS_WorkPackageInit workPackageInit = BLL.WorkPackageInitService.GetWorkPackageInitByWorkPackageCode(this.trWBS.SelectedNodeID);
                        if (workPackageInit.IsChild == true)
                        {
                            string openUrl = String.Format("WorkPackageInitEdit.aspx?type=add&Id={0}", this.trWBS.SelectedNode.NodeID, "增加 - ");

                            PageContext.RegisterStartupScript(Window2.GetSaveStateReference(hdSelectId.ClientID)
                                    + Window2.GetShowReference(openUrl));
                        }
                        //PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("WBSSetCopy.aspx?Id={0}&Type={1}", this.trWBS.SelectedNode.NodeID, this.trWBS.SelectedNode.CommandName, "拷贝 - ")));
                        else
                        {
                            ShowNotify("已是末级，无法添加子级节点！", MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        ShowNotify("工程类型节点无法增加！", MessageBoxIcon.Warning);
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
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (this.trWBS.SelectedNode != null)
            {
                if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ControlItemInitSetMenuId, BLL.Const.BtnDelete))
                {
                    if (this.trWBS.SelectedNode.CommandName != "ProjectType")   //非工程类型节点可以删除
                    {
                        DeleteData();
                    }
                    else
                    {
                        ShowNotify("工程类型节点无法删除！", MessageBoxIcon.Warning);
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
        /// 删除方法
        /// </summary>
        private void DeleteData()
        {
            string id = this.trWBS.SelectedNodeID;
            this.hdSelectId.Text = this.trWBS.SelectedNode.ParentNode.NodeID;
            if (this.trWBS.SelectedNode.CommandName == "WorkPackage")
            {
                Model.WBS_WorkPackageInit workPackageInit = BLL.WorkPackageInitService.GetWorkPackageInitByWorkPackageCode(id);
                if (workPackageInit != null)
                {
                    List<Model.WBS_WorkPackageInit> childWorkPackageInits1 = BLL.WorkPackageInitService.GetWorkPackageInitsBySuperWorkPack(id);
                    if (childWorkPackageInits1.Count > 0)   //存在子分部分项
                    {
                        this.hdSelectId.Text = workPackageInit.WorkPackageCode;
                        foreach (var childWorkPackageInit1 in childWorkPackageInits1)
                        {
                            List<Model.WBS_WorkPackageInit> childWorkPackageInits2 = BLL.WorkPackageInitService.GetWorkPackageInitsBySuperWorkPack(childWorkPackageInit1.WorkPackageCode);
                            if (childWorkPackageInits2.Count > 0)
                            {
                                foreach (var childWorkPackageInit2 in childWorkPackageInits2)
                                {
                                    BLL.ControlItemInitService.DeleteAllControlItemInit(childWorkPackageInit2.WorkPackageCode);
                                    BLL.WorkPackageInitService.DeleteWorkPackageInit(childWorkPackageInit2.WorkPackageCode);
                                }
                            }
                            BLL.ControlItemInitService.DeleteAllControlItemInit(childWorkPackageInit1.WorkPackageCode);
                            BLL.WorkPackageInitService.DeleteWorkPackageInit(childWorkPackageInit1.WorkPackageCode);
                        }
                    }
                    BLL.ControlItemInitService.DeleteAllControlItemInit(id);
                    BLL.WorkPackageInitService.DeleteWorkPackageInit(id);
                }
            }
            BLL.LogService.AddSys_Log(this.CurrUser, id, id, BLL.Const.ControlItemInitSetMenuId, "删除分部分项信息！");
            ShowNotify("删除成功！", MessageBoxIcon.Success);

            GetSelectTreeNode();
        }
        #endregion

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (this.trWBS.SelectedNode != null)
            {
                if (this.trWBS.SelectedNode.CommandName != "ProjectType")   //非工程类型节点可以增加
                {
                    Model.WBS_WorkPackageInit workPackageInit = BLL.WorkPackageInitService.GetWorkPackageInitByWorkPackageCode(this.trWBS.SelectedNodeID);
                    if (workPackageInit.IsChild == false)
                    {
                        //string openUrl = String.Format("ControlItemInitEdit.aspx?type=add&WorkPackageCode={0}", this.trWBS.SelectedNode.NodeID, "增加 - ");

                        //PageContext.RegisterStartupScript(Window3.GetSaveStateReference(hdSelectId.ClientID)
                        //        + Window2.GetShowReference(openUrl));
                        PageContext.RegisterStartupScript(Window3.GetShowReference(String.Format("ControlItemInitEdit.aspx?type=add&WorkPackageCode={0}", this.trWBS.SelectedNode.NodeID, "新增 - ")));
                    }
                    else
                    {
                        ShowNotify("不是末级，无法添加工作包！", MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    ShowNotify("工程类型节点无法增加工作包！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
            }
        }

        #region Grid双击事件
        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            btnMenuModify_Click(null, null);
        }
        #endregion
        #region 编辑
        /// <summary>
        /// 编辑按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            PageContext.RegisterStartupScript(Window3.GetShowReference(String.Format("ControlItemInitEdit.aspx?type=modify&ControlItemCode={0}&WorkPackageCode={1}", this.Grid1.SelectedRowID, this.trWBS.SelectedNodeID, "新增 - ")));
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDel_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            BLL.ControlItemInitService.DeleteControlItemInit(Grid1.SelectedRowID);
            BLL.LogService.AddSys_Log(this.CurrUser, Grid1.SelectedRowID, Grid1.SelectedRowID, BLL.Const.ControlItemInitSetMenuId, "删除工作包");
            Grid1.DataBind();
            BindGrid();
            Alert.ShowInTop("删除数据成功！", MessageBoxIcon.Success);
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
            string strSql = @"SELECT ControlItemCode,WorkPackageCode,ControlItemContent,ControlPoint,ControlItemDef,Weights,HGForms,SHForms,Standard,ClauseNo"
                     + @" FROM WBS_ControlItemInit ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " where WorkPackageCode = @WorkPackageCode";
            listStr.Add(new SqlParameter("@WorkPackageCode", this.trWBS.SelectedNodeID));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();

            Model.WBS_WorkPackageInit workPackageInit = BLL.WorkPackageInitService.GetWorkPackageInitByWorkPackageCode(this.trWBS.SelectedNodeID);
            if (workPackageInit != null)
            {
                if (workPackageInit.ProjectType == "1")  //建筑工程
                {
                    this.Grid1.Columns[4].HeaderText = "对应的建筑资料表格";
                    this.Grid1.Columns[5].Hidden = true;
                }
                else    //安装工程
                {
                    this.Grid1.Columns[4].HeaderText = "对应的化工资料表格";
                    this.Grid1.Columns[5].Hidden = false;
                }
            }
        }
        #endregion

        #region 根据所给Id定位到对应分部分项
        /// <summary>
        /// 根据所给Id定位到对应具体的工程类型、单位工程、子单位工程、分部、子分部、分项、子分项
        /// </summary>
        private void GetSelectTreeNode()
        {
            string projectType = string.Empty;
            string workPackageCode1 = string.Empty;
            string workPackageCode2 = string.Empty;
            Model.WBS_WorkPackageInit workPackageInit = BLL.WorkPackageInitService.GetWorkPackageInitByWorkPackageCode(this.hdSelectId.Text);
            if (workPackageInit.SuperWorkPack == null)   //选中第一级分部分项
            {
                projectType = workPackageInit.ProjectType;
            }
            else
            {
                Model.WBS_WorkPackageInit pWorkPackageInit = BLL.WorkPackageInitService.GetWorkPackageInitByWorkPackageCode(workPackageInit.SuperWorkPack);
                {
                    if (pWorkPackageInit.SuperWorkPack == null)    //选中第二级分部分项
                    {
                        projectType = pWorkPackageInit.ProjectType;
                        workPackageCode1 = pWorkPackageInit.WorkPackageCode;
                    }
                    else
                    {
                        Model.WBS_WorkPackageInit ppWorkPackageInit = BLL.WorkPackageInitService.GetWorkPackageInitByWorkPackageCode(pWorkPackageInit.SuperWorkPack);
                        projectType = ppWorkPackageInit.ProjectType;
                        workPackageCode1 = ppWorkPackageInit.WorkPackageCode;
                        workPackageCode2 = pWorkPackageInit.WorkPackageCode;
                    }
                }
            }
            //重新加载树
            this.trWBS.Nodes.Clear();
            this.trWBS.ShowBorder = false;
            this.trWBS.ShowHeader = false;
            this.trWBS.EnableIcons = true;
            this.trWBS.AutoScroll = true;
            this.trWBS.EnableSingleClickExpand = true;

            List<ListItem> list = new List<ListItem>();
            ListItem item1 = new ListItem();
            item1.Text = "建筑工程";
            item1.Value = "1";
            list.Add(item1);
            ListItem item2 = new ListItem();
            item2.Text = "安装工程";
            item2.Value = "2";
            list.Add(item2);
            foreach (var item in list)
            {
                TreeNode rootNode = new TreeNode();
                rootNode.Text = item.Text;
                rootNode.NodeID = item.Value;
                rootNode.CommandName = "ProjectType";
                rootNode.EnableExpandEvent = true;
                rootNode.EnableClickEvent = true;
                this.trWBS.Nodes.Add(rootNode);
                if (rootNode.NodeID == projectType)
                {
                    rootNode.Expanded = true;
                    var workPackages1 = from x in Funs.DB.WBS_WorkPackageInit where x.ProjectType == projectType && x.SuperWorkPack == null orderby x.WorkPackageCode select x;
                    foreach (var workPackage1 in workPackages1)
                    {
                        TreeNode newNode = new TreeNode();
                        newNode.Text = workPackage1.PackageContent;
                        newNode.NodeID = workPackage1.WorkPackageCode;
                        newNode.CommandName = "WorkPackage";
                        newNode.EnableExpandEvent = true;
                        newNode.EnableClickEvent = true;
                        rootNode.Nodes.Add(newNode);
                        if (workPackageCode1 == workPackage1.WorkPackageCode)
                        {
                            newNode.Expanded = true;
                            var workPackages2 = from x in Funs.DB.WBS_WorkPackageInit where x.ProjectType == projectType && x.SuperWorkPack == workPackage1.WorkPackageCode orderby x.WorkPackageCode select x;
                            foreach (var workPackage2 in workPackages2)
                            {
                                TreeNode newNode2 = new TreeNode();
                                newNode2.Text = workPackage2.PackageContent;
                                newNode2.NodeID = workPackage2.WorkPackageCode;
                                newNode2.CommandName = "WorkPackage";
                                newNode2.EnableExpandEvent = true;
                                newNode2.EnableClickEvent = true;
                                newNode.Nodes.Add(newNode2);
                                if (workPackageCode2 == workPackage2.WorkPackageCode)
                                {
                                    newNode2.Expanded = true;
                                    var workPackages3 = from x in Funs.DB.WBS_WorkPackageInit where x.ProjectType == projectType && x.SuperWorkPack == workPackage2.WorkPackageCode orderby x.WorkPackageCode select x;
                                    foreach (var workPackage3 in workPackages3)
                                    {
                                        TreeNode newNode3 = new TreeNode();
                                        newNode3.Text = workPackage3.PackageContent;
                                        newNode3.NodeID = workPackage3.WorkPackageCode;
                                        newNode3.CommandName = "WorkPackage";
                                        newNode3.EnableExpandEvent = true;
                                        newNode3.EnableClickEvent = true;
                                        newNode2.Nodes.Add(newNode3);
                                    }
                                }
                                else
                                {
                                    var childWorkPackages2 = from x in Funs.DB.WBS_WorkPackageInit where x.SuperWorkPack == workPackage2.WorkPackageCode select x;
                                    if (childWorkPackages2.Count() > 0)
                                    {
                                        TreeNode emptyNode = new TreeNode();
                                        emptyNode.Text = "";
                                        emptyNode.NodeID = "";
                                        newNode.Nodes.Add(emptyNode);
                                    }
                                }
                            }
                        }
                        else
                        {
                            var childWorkPackages1 = from x in Funs.DB.WBS_WorkPackageInit where x.SuperWorkPack == workPackage1.WorkPackageCode select x;
                            if (childWorkPackages1.Count() > 0)
                            {
                                TreeNode emptyNode = new TreeNode();
                                emptyNode.Text = "";
                                emptyNode.NodeID = "";
                                newNode.Nodes.Add(emptyNode);
                            }
                        }
                    }
                }
                else
                {
                    TreeNode emptyNode = new TreeNode();
                    emptyNode.Text = "";
                    emptyNode.NodeID = "";
                    rootNode.Nodes.Add(emptyNode);
                }
                this.trWBS.SelectedNodeID = this.hdSelectId.Text;
                //BindGrid();
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ControlItemInitSetMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                    this.btnMenuAdd.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuEdit.Hidden = false;
                    this.btnMenuModify.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
                    this.btnMenuDel.Hidden = false;
                }
            }
        }
        #endregion
    }
}