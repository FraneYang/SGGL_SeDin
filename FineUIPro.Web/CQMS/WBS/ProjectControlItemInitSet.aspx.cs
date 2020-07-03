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
using Newtonsoft.Json.Linq;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.CQMS.WBS
{
    public partial class ProjectControlItemInitSet : PageBase
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
                if (!BLL.WorkPackageProjectService.IsExitWorkPackageProject(this.CurrUser.LoginProjectId))
                {
                    //拷贝项目WBS数据
                    var workPackageInits = from x in Funs.DB.WBS_WorkPackageInit select x;
                    foreach (var workPackageInit in workPackageInits)
                    {
                        Model.WBS_WorkPackageProject workPackageProject = new Model.WBS_WorkPackageProject();
                        workPackageProject.WorkPackageCode = workPackageInit.WorkPackageCode;
                        workPackageProject.ProjectId = this.CurrUser.LoginProjectId;
                        workPackageProject.ProjectType = workPackageInit.ProjectType;
                        workPackageProject.PackageContent = workPackageInit.PackageContent;
                        workPackageProject.SuperWorkPack = workPackageInit.SuperWorkPack;
                        workPackageProject.IsChild = workPackageInit.IsChild;
                        workPackageProject.PackageCode = workPackageInit.PackageCode;
                        workPackageProject.ProjectType = workPackageInit.ProjectType;
                        BLL.WorkPackageProjectService.AddWorkPackageProject(workPackageProject);
                    }
                    var controlItemInits = from x in Funs.DB.WBS_ControlItemInit select x;
                    foreach (var controlItemInit in controlItemInits)
                    {
                        Model.WBS_ControlItemProject controlItemProject = new Model.WBS_ControlItemProject();
                        controlItemProject.ControlItemCode = controlItemInit.ControlItemCode;
                        controlItemProject.ProjectId = this.CurrUser.LoginProjectId;
                        controlItemProject.WorkPackageCode = controlItemInit.WorkPackageCode;
                        controlItemProject.ControlItemContent = controlItemInit.ControlItemContent;
                        controlItemProject.ControlPoint = controlItemInit.ControlPoint;
                        controlItemProject.ControlItemDef = controlItemInit.ControlItemDef;
                        controlItemProject.Weights = controlItemInit.Weights;
                        controlItemProject.HGForms = controlItemInit.HGForms;
                        controlItemProject.SHForms = controlItemInit.SHForms;
                        controlItemProject.Standard = controlItemInit.Standard;
                        controlItemProject.ClauseNo = controlItemInit.ClauseNo;
                        controlItemProject.CheckNum = 1;
                        BLL.ControlItemProjectService.AddControlItemProject(controlItemProject);
                    }
                }
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
            //rootNode1.EnableExpandEvent = true;
            rootNode1.EnableClickEvent = true;
            this.trWBS.Nodes.Add(rootNode1);
            this.GetNodes(rootNode1.Nodes, rootNode1.NodeID);

            TreeNode rootNode2 = new TreeNode();
            rootNode2.Text = "安装工程";
            rootNode2.NodeID = "2";
            rootNode2.CommandName = "ProjectType";
            //rootNode2.EnableExpandEvent = true;
            rootNode2.EnableClickEvent = true;
            this.trWBS.Nodes.Add(rootNode2);
            this.GetNodes(rootNode2.Nodes, rootNode2.NodeID);
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
                workPackages = (from x in BLL.Funs.DB.WBS_WorkPackageProject
                                where x.SuperWorkPack == null && x.ProjectId == this.CurrUser.LoginProjectId && x.ProjectType == parentId
                                orderby x.PackageCode ascending
                                select x).ToList();
            }
            else
            {
                workPackages = (from x in BLL.Funs.DB.WBS_WorkPackageProject
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
                var workPackages = from x in Funs.DB.WBS_WorkPackageProject where x.ProjectType == e.NodeID && x.SuperWorkPack == null orderby x.WorkPackageCode select x;
                foreach (var workPackage in workPackages)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = workPackage.PackageContent;
                    newNode.NodeID = workPackage.WorkPackageCode;
                    newNode.CommandName = "WorkPackage";
                    newNode.EnableExpandEvent = true;
                    newNode.EnableClickEvent = true;
                    e.Node.Nodes.Add(newNode);
                    var childWorkPackages = from x in Funs.DB.WBS_WorkPackageProject where x.SuperWorkPack == workPackage.WorkPackageCode select x;
                    if (childWorkPackages.Count() > 0)
                    {
                        TreeNode emptyNode = new TreeNode();
                        emptyNode.Text = "";
                        emptyNode.NodeID = "";
                        newNode.Nodes.Add(emptyNode);
                    }
                }
            }
            else if (e.Node.CommandName == "WorkPackage")   //展开分部节点
            {
                var workPackages = from x in Funs.DB.WBS_WorkPackageProject where x.SuperWorkPack == e.Node.NodeID orderby x.WorkPackageCode select x;
                if (workPackages.Count() > 0)   //存在子分部工程
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
                        var childWorkPackages = from x in Funs.DB.WBS_WorkPackageProject where x.SuperWorkPack == workPackage.WorkPackageCode select x;
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
            this.cbAllHGForms.Checked = false;
            this.cbAllSHForms.Checked = false;
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
                if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ControlItemProjectSetMenuId, BLL.Const.BtnModify))
                {
                    if (this.trWBS.SelectedNode.CommandName != "ProjectType")   //非工程类型节点可以修改
                    {
                        this.hdSelectId.Text = this.trWBS.SelectedNode.NodeID;

                        string openUrl = String.Format("WorkPackageProjectEdit.aspx?type=modify&Id={0}", this.trWBS.SelectedNode.NodeID, "编辑 - ");
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
                if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ControlItemProjectSetMenuId, BLL.Const.BtnAdd))
                {
                    if (this.trWBS.SelectedNode.CommandName != "ProjectType")   //非工程类型节点可以增加
                    {
                        Model.WBS_WorkPackageProject workPackageProject = BLL.WorkPackageProjectService.GetWorkPackageProjectByWorkPackageCode(this.trWBS.SelectedNodeID, this.CurrUser.LoginProjectId);
                        if (workPackageProject.IsChild == true)
                        {
                            string openUrl = String.Format("WorkPackageProjectEdit.aspx?type=add&Id={0}", this.trWBS.SelectedNode.NodeID, "增加 - ");

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
                if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ControlItemProjectSetMenuId, BLL.Const.BtnDelete))
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
                Model.WBS_WorkPackageProject workPackageProject = BLL.WorkPackageProjectService.GetWorkPackageProjectByWorkPackageCode(id, this.CurrUser.LoginProjectId);
                if (workPackageProject != null)
                {
                    List<Model.WBS_WorkPackageProject> childWorkPackageProjects1 = BLL.WorkPackageProjectService.GetWorkPackageProjectsBySuperWorkPack(id, this.CurrUser.LoginProjectId);
                    if (childWorkPackageProjects1.Count > 0)   //存在子分部分项
                    {
                        this.hdSelectId.Text = workPackageProject.WorkPackageCode;
                        foreach (var childWorkPackageProject1 in childWorkPackageProjects1)
                        {
                            List<Model.WBS_WorkPackageProject> childWorkPackageProjects2 = BLL.WorkPackageProjectService.GetWorkPackageProjectsBySuperWorkPack(childWorkPackageProject1.WorkPackageCode, this.CurrUser.LoginProjectId);
                            if (childWorkPackageProjects2.Count > 0)
                            {
                                foreach (var childWorkPackageProject2 in childWorkPackageProjects2)
                                {
                                    BLL.ControlItemProjectService.DeleteAllControlItemProject(childWorkPackageProject2.WorkPackageCode, this.CurrUser.LoginProjectId);
                                    BLL.WorkPackageProjectService.DeleteWorkPackageProject(childWorkPackageProject2.WorkPackageCode, this.CurrUser.LoginProjectId);
                                }
                            }
                            BLL.ControlItemProjectService.DeleteAllControlItemProject(childWorkPackageProject1.WorkPackageCode, this.CurrUser.LoginProjectId);
                            BLL.WorkPackageProjectService.DeleteWorkPackageProject(childWorkPackageProject1.WorkPackageCode, this.CurrUser.LoginProjectId);
                        }
                    }
                    BLL.ControlItemProjectService.DeleteAllControlItemProject(id, this.CurrUser.LoginProjectId);
                    BLL.WorkPackageProjectService.DeleteWorkPackageProject(id, this.CurrUser.LoginProjectId);
                }
            }
            BLL.LogService.AddSys_Log(this.CurrUser, id, id, BLL.Const.ControlItemProjectSetMenuId, "删除分部分项信息！");
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
                    Model.WBS_WorkPackageProject workPackageProject = BLL.WorkPackageProjectService.GetWorkPackageProjectByWorkPackageCode(this.trWBS.SelectedNodeID, this.CurrUser.LoginProjectId);
                    if (workPackageProject.IsChild == false)
                    {
                        //string openUrl = String.Format("ControlItemProjectEdit.aspx?type=add&WorkPackageCode={0}", this.trWBS.SelectedNode.NodeID, "增加 - ");
                        //PageContext.RegisterStartupScript(Window3.GetSaveStateReference(hdSelectId.ClientID)
                        //        + Window2.GetShowReference(openUrl));
                        PageContext.RegisterStartupScript(Window3.GetShowReference(String.Format("ControlItemProjectEdit.aspx?type=add&WorkPackageCode={0}", this.trWBS.SelectedNode.NodeID, "新增 - ")));
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

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            decimal weights = 0;
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");

                try
                {
                    weights += Convert.ToDecimal(values.Value<string>("Weights"));
                }
                catch (Exception)
                {

                }
            }
            if (weights != 100)
            {
                Alert.ShowInTop("权重值不是100，请调整后再保存！", MessageBoxIcon.Warning);
                return;
            }
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                string controlItemCode = values.Value<string>("ControlItemCode");
                Model.WBS_ControlItemProject controlItemProject = BLL.ControlItemProjectService.GetControlItemProjectByCode(controlItemCode, this.CurrUser.LoginProjectId);
                controlItemProject.ControlPoint = values.Value<string>("ControlPoint");
                try
                {
                    controlItemProject.Weights = Convert.ToDecimal(values.Value<string>("Weights"));
                }
                catch (Exception)
                {
                    controlItemProject.Weights = null;
                }
                controlItemProject.ControlItemDef = values.Value<string>("ControlItemDef");
                controlItemProject.Standard = values.Value<string>("Standard");
                controlItemProject.ClauseNo = values.Value<string>("ClauseNo");
                try
                {
                    controlItemProject.CheckNum = Convert.ToInt32(values.Value<string>("CheckNum"));
                }
                catch (Exception)
                {
                    controlItemProject.CheckNum = null;
                }
                AspNet.CheckBoxList cblHGForms1 = (AspNet.CheckBoxList)(this.Grid1.Rows[i].FindControl("cblHGForms"));
                string hGForms = string.Empty;
                for (int j = 0; j < cblHGForms1.Items.Count; j++)
                {
                    if (cblHGForms1.Items[j].Selected)
                    {
                        hGForms += cblHGForms1.Items[j].Text + ",";
                    }
                }
                if (!string.IsNullOrEmpty(hGForms))
                {
                    hGForms = hGForms.Substring(0, hGForms.LastIndexOf(","));
                }
                controlItemProject.HGForms = hGForms;
                AspNet.CheckBoxList cblSHForms1 = (AspNet.CheckBoxList)(this.Grid1.Rows[i].FindControl("cblSHForms"));
                string sHForms = string.Empty;
                for (int j = 0; j < cblSHForms1.Items.Count; j++)
                {
                    if (cblSHForms1.Items[j].Selected)
                    {
                        sHForms += cblSHForms1.Items[j].Text + ",";
                    }
                }
                if (!string.IsNullOrEmpty(sHForms))
                {
                    sHForms = sHForms.Substring(0, sHForms.LastIndexOf(","));
                }
                controlItemProject.SHForms = sHForms;
                BLL.ControlItemProjectService.UpdateControlItemProject(controlItemProject);
            }
            ShowNotify("保存成功！", MessageBoxIcon.Success);
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
            PageContext.RegisterStartupScript(Window3.GetShowReference(String.Format("ControlItemProjectEdit.aspx?type=modify&ControlItemCode={0}&WorkPackageCode={1}", this.Grid1.SelectedRowID, this.trWBS.SelectedNodeID, "新增 - ")));
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
            BLL.ControlItemProjectService.DeleteControlItemProject(Grid1.SelectedRowID, this.CurrUser.LoginProjectId);
            BLL.LogService.AddSys_Log(this.CurrUser, Grid1.SelectedRowID, Grid1.SelectedRowID, BLL.Const.ControlItemProjectSetMenuId, "删除工作包");
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
            string strSql = @"SELECT ControlItemCode,WorkPackageCode,ControlItemContent,ControlPoint,ControlItemDef,Weights,HGForms,SHForms,Standard,ClauseNo,CheckNum"
                     + @" FROM WBS_ControlItemProject ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " where WorkPackageCode = @WorkPackageCode and ProjectId=@ProjectId";
            listStr.Add(new SqlParameter("@WorkPackageCode", this.trWBS.SelectedNodeID));
            listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
            Model.WBS_WorkPackageProject workPackageProject = BLL.WorkPackageProjectService.GetWorkPackageProjectByWorkPackageCode(this.trWBS.SelectedNodeID, this.CurrUser.LoginProjectId);
            if (workPackageProject != null)
            {
                if (workPackageProject.ProjectType == "1")  //建筑工程
                {
                    this.Grid1.Columns[4].HeaderText = "对应的建筑资料表格";
                    this.Grid1.Columns[5].Hidden = true;
                    this.lbHd.Width = System.Web.UI.WebControls.Unit.Pixel(480);
                    this.cbAllHGForms.Label = "全选对应的建筑资料表格";
                    this.cbAllHGForms.LabelWidth = System.Web.UI.WebControls.Unit.Pixel(175);
                    this.cbAllSHForms.Hidden = true;
                }
                else    //安装工程
                {
                    this.Grid1.Columns[4].HeaderText = "对应的化工资料表格";
                    this.Grid1.Columns[5].Hidden = false;
                    this.lbHd.Width = System.Web.UI.WebControls.Unit.Pixel(380);
                    this.cbAllHGForms.Label = "全选对应的化工资料表格";
                    this.cbAllHGForms.LabelWidth = System.Web.UI.WebControls.Unit.Pixel(175);
                    this.cbAllSHForms.Hidden = false;
                }
            }
            for (int i = 0; i < this.Grid1.Rows.Count; i++)
            {
                Model.WBS_ControlItemInit c = BLL.ControlItemInitService.GetControlItemInitByCode(this.Grid1.Rows[i].RowID.ToString());
                Model.WBS_ControlItemProject cp = BLL.ControlItemProjectService.GetControlItemProjectByCode(this.Grid1.Rows[i].RowID.ToString(), this.CurrUser.LoginProjectId);
                List<string> hGForms = new List<string>();
                if (c != null && !string.IsNullOrEmpty(c.HGForms))
                {
                    hGForms = c.HGForms.Split(',').ToList();
                }
                string[] phGForms = cp.HGForms.Split(',');
                foreach (var item in phGForms)   //项目工作包表格中增加的表格也要显示
                {
                    hGForms.Add(item);
                }
                hGForms = hGForms.Distinct().ToList();
                AspNet.CheckBoxList cblHGForms = (AspNet.CheckBoxList)(this.Grid1.Rows[i].FindControl("cblHGForms"));
                cblHGForms.Items.Clear();
                foreach (var hGForm in hGForms)   //加载基础库的表格数据
                {
                    if (!string.IsNullOrEmpty(hGForm))
                    {
                        AspNet.ListItem li = new AspNet.ListItem();
                        li.Text = hGForm;
                        li.Value = hGForm;
                        cblHGForms.Items.Add(li);
                    }
                }
                for (int j = 0; j < cblHGForms.Items.Count; j++)
                {
                    if (cp.HGForms.Contains(cblHGForms.Items[j].Value))   //项目库中包含的表格选中
                    {
                        cblHGForms.Items[j].Selected = true;
                    }
                }
                List<string> sHForms = new List<string>();
                if (c != null && !string.IsNullOrEmpty(c.SHForms))
                {
                    sHForms = c.SHForms.Split(',').ToList();
                }
                string[] psHForms = cp.SHForms.Split(',');
                foreach (var item in psHForms)    //项目工作包表格中增加的表格也要显示
                {
                    sHForms.Add(item);
                }
                sHForms = sHForms.Distinct().ToList();
                AspNet.CheckBoxList cblSHForms = (AspNet.CheckBoxList)(this.Grid1.Rows[i].FindControl("cblSHForms"));
                cblSHForms.Items.Clear();
                foreach (var sHForm in sHForms)   //加载基础库的表格数据
                {
                    if (!string.IsNullOrEmpty(sHForm))
                    {
                        AspNet.ListItem li = new AspNet.ListItem();
                        li.Text = sHForm;
                        li.Value = sHForm;
                        cblSHForms.Items.Add(li);
                    }
                }
                for (int j = 0; j < cblSHForms.Items.Count; j++)
                {
                    if (cp.SHForms.Contains(cblSHForms.Items[j].Value))   //项目库中包含的表格选中
                    {
                        cblSHForms.Items[j].Selected = true;
                    }
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
            Model.WBS_WorkPackageProject workPackageProject = BLL.WorkPackageProjectService.GetWorkPackageProjectByWorkPackageCode(this.hdSelectId.Text, this.CurrUser.LoginProjectId);
            if (workPackageProject.SuperWorkPack == null)   //选中第一级分部分项
            {
                projectType = workPackageProject.ProjectType;
            }
            else
            {
                Model.WBS_WorkPackageProject pWorkPackageProject = BLL.WorkPackageProjectService.GetWorkPackageProjectByWorkPackageCode(workPackageProject.SuperWorkPack, this.CurrUser.LoginProjectId);
                {
                    if (pWorkPackageProject.SuperWorkPack == null)    //选中第二级分部分项
                    {
                        projectType = pWorkPackageProject.ProjectType;
                        workPackageCode1 = pWorkPackageProject.WorkPackageCode;
                    }
                    else
                    {
                        Model.WBS_WorkPackageProject ppWorkPackageProject = BLL.WorkPackageProjectService.GetWorkPackageProjectByWorkPackageCode(pWorkPackageProject.SuperWorkPack, this.CurrUser.LoginProjectId);
                        projectType = ppWorkPackageProject.ProjectType;
                        workPackageCode1 = ppWorkPackageProject.WorkPackageCode;
                        workPackageCode2 = pWorkPackageProject.WorkPackageCode;
                    }
                }
            }
            InitTreeMenu();
            for (int i = 0; i < trWBS.Nodes.Count; i++)
            {
                if (trWBS.Nodes[i].NodeID == projectType)
                {
                    trWBS.Nodes[i].Expanded = true;
                    if (!string.IsNullOrEmpty(workPackageCode1))
                    {
                        for (int j = 0; j < trWBS.Nodes[i].Nodes.Count; j++)
                        {
                            if (trWBS.Nodes[i].Nodes[j].NodeID == workPackageCode1)
                            {
                                trWBS.Nodes[i].Nodes[j].Expanded = true;
                                if (!string.IsNullOrEmpty(workPackageCode2))
                                {
                                    for (int k = 0; k < trWBS.Nodes[i].Nodes[j].Nodes.Count; k++)
                                    {
                                        if (trWBS.Nodes[i].Nodes[j].Nodes[k].NodeID == workPackageCode2)
                                        {
                                            trWBS.Nodes[i].Nodes[j].Nodes[k].Expanded = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            this.trWBS.SelectedNodeID = this.hdSelectId.Text;
            //BindGrid();
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ControlItemProjectSetMenuId);
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
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                    this.btnRset.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
                    this.btnMenuDel.Hidden = false;
                }
            }
        }
        #endregion

        #region  表格选择事件
        protected void cblHGForms_SelectedIndexChanged(object sender, EventArgs e)
        {
            AspNet.CheckBoxList cblHGForms = sender as AspNet.CheckBoxList;
            for (int i = 0; i < this.Grid1.Rows.Count; i++)
            {
                AspNet.CheckBoxList cblHGForms1 = (AspNet.CheckBoxList)(this.Grid1.Rows[i].FindControl("cblHGForms"));
                if (cblHGForms1.ClientID == cblHGForms.ClientID)
                {
                    Model.WBS_ControlItemProject cp = BLL.ControlItemProjectService.GetControlItemProjectByCode(this.Grid1.SelectedRowID, this.CurrUser.LoginProjectId);
                    string hGForms = string.Empty;
                    for (int j = 0; j < cblHGForms.Items.Count; j++)
                    {
                        if (cblHGForms.Items[j].Selected)
                        {
                            hGForms += cblHGForms.Items[j].Text + ",";
                        }
                    }
                    if (!string.IsNullOrEmpty(hGForms))
                    {
                        hGForms = hGForms.Substring(0, hGForms.LastIndexOf(","));
                    }
                    cp.HGForms = hGForms;
                    BLL.ControlItemProjectService.UpdateControlItemProject(cp);
                }
            }
        }

        protected void cblSHForms_SelectedIndexChanged(object sender, EventArgs e)
        {
            AspNet.CheckBoxList cblSHForms = sender as AspNet.CheckBoxList;
            for (int i = 0; i < this.Grid1.Rows.Count; i++)
            {
                AspNet.CheckBoxList cblSHForms1 = (AspNet.CheckBoxList)(this.Grid1.Rows[i].FindControl("cblSHForms"));
                if (cblSHForms1.ClientID == cblSHForms.ClientID)
                {
                    Model.WBS_ControlItemProject cp = BLL.ControlItemProjectService.GetControlItemProjectByCode(this.Grid1.SelectedRowID, this.CurrUser.LoginProjectId);
                    if (cp != null)
                    {
                        string sHForms = string.Empty;
                        for (int j = 0; j < cblSHForms.Items.Count; j++)
                        {
                            if (cblSHForms.Items[j].Selected)
                            {
                                sHForms += cblSHForms.Items[j].Text + ",";
                            }
                        }
                        if (!string.IsNullOrEmpty(sHForms))
                        {
                            sHForms = sHForms.Substring(0, sHForms.LastIndexOf(","));
                        }
                        cp.SHForms = sHForms;
                        BLL.ControlItemProjectService.UpdateControlItemProject(cp);
                    }
                }
            }
        }
        #endregion

        #region  按级别显示事件
        /// <summary>
        /// 第一级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLevel1_Click(object sender, EventArgs e)
        {
            this.trWBS.CollapseAllNodes();
            for (int i = 0; i < trWBS.Nodes.Count; i++)
            {
                trWBS.Nodes[i].Expanded = true;
            }
        }

        /// <summary>
        /// 第二级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLevel2_Click(object sender, EventArgs e)
        {
            this.trWBS.CollapseAllNodes();
            for (int i = 0; i < trWBS.Nodes.Count; i++)
            {
                trWBS.Nodes[i].Expanded = true;
                for (int j = 0; j < trWBS.Nodes[i].Nodes.Count; j++)
                {
                    trWBS.Nodes[i].Nodes[j].Expanded = true;
                }
            }
        }

        /// <summary>
        /// 第三级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLevel3_Click(object sender, EventArgs e)
        {
            this.trWBS.CollapseAllNodes();
            for (int i = 0; i < trWBS.Nodes.Count; i++)
            {
                trWBS.Nodes[i].Expanded = true;
                for (int j = 0; j < trWBS.Nodes[i].Nodes.Count; j++)
                {
                    trWBS.Nodes[i].Nodes[j].Expanded = true;
                    for (int k = 0; k < trWBS.Nodes[i].Nodes[j].Nodes.Count; k++)
                    {
                        trWBS.Nodes[i].Nodes[j].Nodes[k].Expanded = true;
                    }
                }
            }
        }
        #endregion

        #region
        protected void btnRset_Click(object sender, EventArgs e)
        {
            if (this.trWBS.SelectedNode.Nodes.Count == 0)  //末级节点
            {
                //删除新增项
                var q = BLL.ControlItemProjectService.GetItemsByWorkPackageCode(this.trWBS.SelectedNodeID, this.CurrUser.LoginProjectId);
                foreach (var item in q)
                {
                    Model.WBS_ControlItemInit init = BLL.ControlItemInitService.GetControlItemInitByCode(item.ControlItemCode);
                    if (init == null)
                    {
                        BLL.ControlItemProjectService.DeleteControlItemProject(item.ControlItemCode, this.CurrUser.LoginProjectId);
                    }
                }
                //恢复默认
                var q2 = BLL.ControlItemInitService.GetItemsByWorkPackageCode(this.trWBS.SelectedNodeID);
                foreach (var item in q2)
                {
                    var cp = BLL.ControlItemProjectService.GetControlItemProjectByCode(item.ControlItemCode, this.CurrUser.LoginProjectId);
                    if (cp != null)
                    {
                        cp.ControlItemContent = item.ControlItemContent;
                        cp.ControlPoint = item.ControlPoint;
                        cp.ControlItemDef = item.ControlItemDef;
                        cp.Weights = item.Weights;
                        cp.HGForms = item.HGForms;
                        cp.SHForms = item.SHForms;
                        cp.Standard = item.Standard;
                        cp.ClauseNo = item.ClauseNo;
                        cp.CheckNum = 1;
                        BLL.ControlItemProjectService.UpdateControlItemProject(cp);
                    }
                    else
                    {
                        Model.WBS_ControlItemProject controlItemProject = new Model.WBS_ControlItemProject();
                        controlItemProject.ControlItemCode = item.ControlItemCode;
                        controlItemProject.ProjectId = this.CurrUser.LoginProjectId;
                        controlItemProject.WorkPackageCode = item.WorkPackageCode;
                        controlItemProject.ControlItemContent = item.ControlItemContent;
                        controlItemProject.ControlPoint = item.ControlPoint;
                        controlItemProject.ControlItemDef = item.ControlItemDef;
                        controlItemProject.Weights = item.Weights;
                        controlItemProject.HGForms = item.HGForms;
                        controlItemProject.SHForms = item.SHForms;
                        controlItemProject.Standard = item.Standard;
                        controlItemProject.ClauseNo = item.ClauseNo;
                        controlItemProject.CheckNum = 1;
                        BLL.ControlItemProjectService.AddControlItemProject(controlItemProject);
                    }
                }
                ShowNotify("恢复默认成功！", MessageBoxIcon.Success);
                BindGrid();
            }
            else
            {
                Alert.ShowInTop("请选择树节点的末级！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region  全选化工表格
        protected void cbAllHGForms_CheckedChanged(object sender, CheckedEventArgs e)
        {
            CheckBox cblAllHGForms = sender as CheckBox;
            BindGrid();
            for (int i = 0; i < this.Grid1.Rows.Count; i++)
            {
                AspNet.CheckBoxList cblHGForms1 = (AspNet.CheckBoxList)(this.Grid1.Rows[i].FindControl("cblHGForms"));
                Model.WBS_ControlItemProject cp = BLL.ControlItemProjectService.GetControlItemProjectByCode(this.Grid1.Rows[i].RowID, this.CurrUser.LoginProjectId);
                for (int j = 0; j < cblHGForms1.Items.Count; j++)
                {
                    if (cblAllHGForms.Checked)
                    {
                        cblHGForms1.Items[j].Selected = true;
                    }
                    else
                    {
                        cblHGForms1.Items[j].Selected = false;
                    }
                }
            }

            //CheckBox cblAllHGForms = sender as CheckBox;
            //for (int i = 0; i < this.Grid1.Rows.Count; i++)
            //{
            //    AspNet.CheckBoxList cblHGForms1 = (AspNet.CheckBoxList)(this.Grid1.Rows[i].FindControl("cblHGForms"));
            //    Model.WBS_ControlItemProject cp = BLL.ControlItemProjectService.GetControlItemProjectByCode(this.Grid1.Rows[i].RowID, this.CurrUser.LoginProjectId);
            //    string hGForms = string.Empty;
            //    for (int j = 0; j < cblHGForms1.Items.Count; j++)
            //    {
            //        if (cblAllHGForms.Checked)
            //        {
            //            hGForms += cblHGForms1.Items[j].Text + ",";
            //        }
            //    }
            //    if (!string.IsNullOrEmpty(hGForms))
            //    {
            //        hGForms = hGForms.Substring(0, hGForms.LastIndexOf(","));
            //    }
            //    cp.HGForms = hGForms;
            //    BLL.ControlItemProjectService.UpdateControlItemProject(cp);
            //}
            //BindGrid();
        }
        #endregion

        #region  全选化工表格
        protected void cbAllSHForms_CheckedChanged(object sender, CheckedEventArgs e)
        {
            CheckBox cblAllSHForms = sender as CheckBox;
            BindGrid();
            for (int i = 0; i < this.Grid1.Rows.Count; i++)
            {
                AspNet.CheckBoxList cblSHForms1 = (AspNet.CheckBoxList)(this.Grid1.Rows[i].FindControl("cblSHForms"));
                Model.WBS_ControlItemProject cp = BLL.ControlItemProjectService.GetControlItemProjectByCode(this.Grid1.Rows[i].RowID, this.CurrUser.LoginProjectId);
                for (int j = 0; j < cblSHForms1.Items.Count; j++)
                {
                    if (cblAllSHForms.Checked)
                    {
                        cblSHForms1.Items[j].Selected = true;
                    }
                    else
                    {
                        cblSHForms1.Items[j].Selected = false;
                    }
                }
            }

            //CheckBox cblAllSHForms = sender as CheckBox;
            //for (int i = 0; i < this.Grid1.Rows.Count; i++)
            //{
            //    AspNet.CheckBoxList cblSHForms1 = (AspNet.CheckBoxList)(this.Grid1.Rows[i].FindControl("cblSHForms"));
            //    Model.WBS_ControlItemProject cp = BLL.ControlItemProjectService.GetControlItemProjectByCode(this.Grid1.Rows[i].RowID, this.CurrUser.LoginProjectId);
            //    string hGForms = string.Empty;
            //    for (int j = 0; j < cblSHForms1.Items.Count; j++)
            //    {
            //        if (cblAllSHForms.Checked)
            //        {
            //            hGForms += cblSHForms1.Items[j].Text + ",";
            //        }
            //    }
            //    if (!string.IsNullOrEmpty(hGForms))
            //    {
            //        hGForms = hGForms.Substring(0, hGForms.LastIndexOf(","));
            //    }
            //    cp.SHForms = hGForms;
            //    BLL.ControlItemProjectService.UpdateControlItemProject(cp);
            //}
            //BindGrid();
        }
        #endregion
    }
}