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
    public partial class ControlItemAndCycle : PageBase
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
                var trUnitWork = from x in BLL.Funs.DB.WBS_UnitWork
                                 where x.ProjectId == this.CurrUser.LoginProjectId && x.SuperUnitWork == null && x.ProjectType == e.Node.NodeID
                                 select x;
                trUnitWork = trUnitWork.OrderBy(x => x.UnitWorkCode);
                if (trUnitWork.Count() > 0)
                {
                    foreach (var trUnitWorkItem in trUnitWork)
                    {
                        TreeNode newNode = new TreeNode();
                        string weights = string.Empty;
                        if (trUnitWorkItem.Weights != null)
                        {
                            weights = "(" + Convert.ToDouble(trUnitWorkItem.Weights).ToString() + "%)";
                        }
                        newNode.Text = trUnitWorkItem.UnitWorkCode + "-" + trUnitWorkItem.UnitWorkName + weights;
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
            ShowNotify("定制成功！", MessageBoxIcon.Success);

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
            ShowNotify("定制成功！", MessageBoxIcon.Success);

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
        //protected void btnMenuEdit_Click(object sender, EventArgs e)
        //{
        //    if (this.trWBS.SelectedNode != null)
        //    {
        //        if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ControlItemAndCycleMenuId, BLL.Const.BtnModify))
        //        {
        //            if (this.trWBS.SelectedNode.CommandName != "ProjectType")   //非工程类型节点可以定制
        //            {
        //                this.hdSelectId.Text = this.trWBS.SelectedNode.NodeID;

        //                string openUrl = String.Format("WorkPackageProjectEdit.aspx?type=modify&Id={0}", this.trWBS.SelectedNode.NodeID, "编辑 - ");
        //                PageContext.RegisterStartupScript(Window1.GetSaveStateReference(hdSelectId.ClientID)
        //                            + Window1.GetShowReference(openUrl));
        //            }
        //            else
        //            {
        //                ShowNotify("工程类型节点无法修改！", MessageBoxIcon.Warning);
        //            }
        //        }
        //        else
        //        {
        //            ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
        //        }
        //    }
        //    else
        //    {
        //        ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
        //    }
        //}

        /// <summary>
        /// 定制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuAdd_Click(object sender, EventArgs e)
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
                            string openUrl = String.Format("WorkPackageSet1.aspx?UnitWorkId={0}", this.trWBS.SelectedNode.NodeID, "定制 - ");
                            PageContext.RegisterStartupScript(Window1.GetSaveStateReference(hdSelectId.ClientID)
                                    + Window1.GetShowReference(openUrl));
                        }
                        else
                        {
                            Model.WBS_WorkPackage workPackage = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(this.trWBS.SelectedNode.NodeID);
                            if (workPackage.IsChild == true)   //非末级节点
                            {
                                string openUrl = String.Format("WorkPackageSet2.aspx?WorkPackageId={0}", this.trWBS.SelectedNode.NodeID, "定制 - ");
                                PageContext.RegisterStartupScript(Window1.GetSaveStateReference(hdSelectId.ClientID)
                                        + Window1.GetShowReference(openUrl));
                            }
                            else    //末级节点
                            {
                                string openUrl = String.Format("ControlItemAndCycleSet.aspx?WorkPackageId={0}", this.trWBS.SelectedNode.NodeID, "定制 - ");
                                PageContext.RegisterStartupScript(Window1.GetSaveStateReference(hdSelectId.ClientID)
                                        + Window1.GetShowReference(openUrl));
                            }
                        }
                    }
                    else
                    {
                        ShowNotify("工程类型节点无法定制！", MessageBoxIcon.Warning);
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

        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (this.trWBS.SelectedNode != null)
            {
                if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ControlItemAndCycleMenuId, BLL.Const.BtnDelete))
                {
                    if (this.trWBS.SelectedNode.CommandName != "ProjectType" && this.trWBS.SelectedNode.CommandName != "UnitWork")   //非工程类型和单位工程节点可以删除
                    {
                        DeleteData();
                    }
                    else
                    {
                        ShowNotify("工程类型、单位工程无法删除！", MessageBoxIcon.Warning);
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
                Model.WBS_WorkPackage workPackage = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(id);
                if (workPackage != null)
                {
                    List<Model.WBS_WorkPackage> childWorkPackage1 = BLL.WorkPackageService.GetAllWorkPackagesBySuperWorkPackageId(id);
                    if (childWorkPackage1.Count > 0)   //存在子分部分项
                    {
                        this.hdSelectId.Text = workPackage.WorkPackageCode;
                        foreach (var cWorkPackage1 in childWorkPackage1)
                        {
                            List<Model.WBS_WorkPackage> childWorkPackages2 = BLL.WorkPackageService.GetAllWorkPackagesBySuperWorkPackageId(cWorkPackage1.WorkPackageId);
                            if (childWorkPackages2.Count > 0)
                            {
                                foreach (var cWorkPackage2 in childWorkPackages2)
                                {
                                    BLL.ControlItemAndCycleService.DeleteAllControlItemAndCycle(cWorkPackage2.WorkPackageId);
                                    BLL.WorkPackageService.DeleteWorkPackage(cWorkPackage2.WorkPackageId);
                                }
                            }
                            BLL.ControlItemAndCycleService.DeleteAllControlItemAndCycle(cWorkPackage1.WorkPackageId);
                            BLL.WorkPackageService.DeleteWorkPackage(cWorkPackage1.WorkPackageId);
                        }
                    }
                    BLL.ControlItemAndCycleService.DeleteAllControlItemAndCycle(id);
                    BLL.WorkPackageService.DeleteWorkPackage(id);
                }
            }
            BLL.LogService.AddSys_Log(this.CurrUser, id, id, BLL.Const.ControlItemAndCycleMenuId, "删除分部分项信息！");
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
                    Model.WBS_WorkPackage workPackage = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(this.trWBS.SelectedNodeID);
                    if (workPackage.IsChild == false)
                    {
                        PageContext.RegisterStartupScript(Window3.GetShowReference(String.Format("ControlItemAndCycleEdit.aspx?type=add&WorkPackageId={0}", this.trWBS.SelectedNode.NodeID, "新增 - ")));
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
        /// 时间转换
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string ConvertDate(object date)
        {
            if (date != null)
            {
                return string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(date));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 批量导入计划完成时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnIn_Click(object sender, EventArgs e)
        {
            Alert.ShowInTop("该功能尚未开发！", MessageBoxIcon.Warning);
            return;
        }

        /// <summary>
        /// 增加
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
                string controlItemAndCycleId = values.Value<string>("ControlItemAndCycleId");
                Model.WBS_ControlItemAndCycle controlItemAndCycle = BLL.ControlItemAndCycleService.GetControlItemAndCycleById(controlItemAndCycleId);
                controlItemAndCycle.ControlPoint = values.Value<string>("ControlPoint");
                try
                {
                    controlItemAndCycle.Weights = Convert.ToDecimal(values.Value<string>("Weights"));
                }
                catch (Exception)
                {
                    controlItemAndCycle.Weights = null;
                }
                controlItemAndCycle.ControlItemDef = values.Value<string>("ControlItemDef");
                controlItemAndCycle.Standard = values.Value<string>("Standard");
                controlItemAndCycle.ClauseNo = values.Value<string>("ClauseNo");
                try
                {
                    controlItemAndCycle.CheckNum = Convert.ToInt32(values.Value<string>("CheckNum"));
                }
                catch (Exception)
                {
                    controlItemAndCycle.CheckNum = null;
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
                //判断是否需要修改资料情况
                bool needChange = false;
                string changeDataState = string.Empty;
                if (string.IsNullOrEmpty(controlItemAndCycle.HGForms) && string.IsNullOrEmpty(controlItemAndCycle.SHForms))   //之前记录资料为空
                {
                    if (!string.IsNullOrEmpty(hGForms) || !string.IsNullOrEmpty(sHForms))   //修改资料不为空后需要处理
                    {
                        needChange = true;
                        changeDataState = "1";    //需要上传资料
                    }
                }
                else   //之前记录资料不为空
                {
                    if (string.IsNullOrEmpty(hGForms) && string.IsNullOrEmpty(sHForms))   //修改资料为空后需要处理
                    {
                        needChange = true;
                        changeDataState = "2";    //不需要上传资料
                    }
                }
                if (needChange)   //需要修改资料情况
                {
                    string state2 = string.Empty;
                    var spotCheckDetails = BLL.SpotCheckDetailService.GetSpotCheckDetailsByControlItemAndCycleId(controlItemAndCycleId);
                    foreach (var item in spotCheckDetails)
                    {
                        Model.Check_SpotCheck spotCheck = BLL.SpotCheckService.GetSpotCheck(item.SpotCheckCode);
                        var details = BLL.SpotCheckDetailService.GetOKSpotCheckDetails(item.SpotCheckCode);
                        if (changeDataState == "1")
                        {
                            state2 = BLL.Const.SpotCheck_Audit5;
                            item.IsShow = true;
                            item.IsDataOK = null;
                            item.State = BLL.Const.SpotCheck_Audit5;
                            item.HandleMan = spotCheck.CreateMan;
                            BLL.SpotCheckDetailService.UpdateSpotCheckDetail(item);
                            //新增待办记录
                            Model.Check_SpotCheckApprove approve = new Model.Check_SpotCheckApprove();
                            approve.SpotCheckCode = item.SpotCheckCode;
                            approve.ApproveMan = spotCheck.CreateMan;
                            approve.ApproveType = BLL.Const.SpotCheck_Audit5;
                            approve.Sign = "2";
                            approve.SpotCheckDetailId = item.SpotCheckDetailId;
                            BLL.SpotCheckApproveService.AddSpotCheckApprove(approve);
                            foreach (var d in details)
                            {
                                if (d.SpotCheckDetailId != item.SpotCheckDetailId)
                                {
                                    if (d.State != BLL.Const.SpotCheck_Audit5)
                                    {
                                        state2 = BLL.Const.SpotCheck_Z;
                                        break;
                                    }
                                }
                            }
                            spotCheck.IsShow = true;
                            spotCheck.State2 = state2;
                            BLL.SpotCheckService.UpdateSpotCheck(spotCheck);
                        }
                        else
                        {
                            state2 = BLL.Const.SpotCheck_Complete;
                            item.IsShow = false;
                            item.IsDataOK = "2";
                            item.State = BLL.Const.SpotCheck_Complete;
                            item.HandleMan = null;
                            BLL.SpotCheckDetailService.UpdateSpotCheckDetail(item);
                            //删除该明细办理步骤
                            BLL.SpotCheckApproveService.DeleteSpotCheckApprovesBySpotCheckDetailId(item.SpotCheckDetailId);
                            foreach (var d in details)
                            {
                                if (d.SpotCheckDetailId != item.SpotCheckDetailId)
                                {
                                    if (d.State != BLL.Const.SpotCheck_Complete)
                                    {
                                        state2 = BLL.Const.SpotCheck_Z;
                                        break;
                                    }
                                }
                            }
                            spotCheck.State2 = state2;
                            BLL.SpotCheckService.UpdateSpotCheck(spotCheck);
                        }
                    }
                }
                controlItemAndCycle.HGForms = hGForms;
                controlItemAndCycle.SHForms = sHForms;
                if (!string.IsNullOrEmpty(values.Value<string>("PlanCompleteDate")))
                {
                    controlItemAndCycle.PlanCompleteDate = Convert.ToDateTime(values.Value<string>("PlanCompleteDate"));
                }
                else
                {
                    controlItemAndCycle.PlanCompleteDate = null;
                }
                BLL.ControlItemAndCycleService.UpdateControlItemAndCycle(controlItemAndCycle);
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
            PageContext.RegisterStartupScript(Window3.GetShowReference(String.Format("ControlItemAndCycleEdit.aspx?type=modify&ControlItemAndCycleId={0}&WorkPackageId={1}", this.Grid1.SelectedRowID, this.trWBS.SelectedNodeID, "新增 - ")));
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
            BLL.ControlItemAndCycleService.DeleteControlItemAndCycle(Grid1.SelectedRowID);
            BLL.LogService.AddSys_Log(this.CurrUser, string.Empty, Grid1.SelectedRowID, BLL.Const.ControlItemAndCycleMenuId, "删除工作包");
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
            string strSql = @"SELECT ControlItemAndCycleId,ControlItemAndCycleCode,InitControlItemCode,ControlItemContent,PlanCompleteDate,ControlPoint,ControlItemDef,Weights,HGForms,SHForms,Standard,ClauseNo,CheckNum"
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
                Model.WBS_ControlItemAndCycle c = BLL.ControlItemAndCycleService.GetControlItemAndCycleById(this.Grid1.Rows[i].RowID.ToString());
                Model.WBS_ControlItemProject cp = BLL.ControlItemProjectService.GetControlItemProjectByCode(this.Grid1.Rows[i].DataKeys[1].ToString(), this.CurrUser.LoginProjectId);
                if (c != null)
                {
                    List<string> hGForms = new List<string>();
                    if (cp != null && !string.IsNullOrEmpty(cp.HGForms))
                    {
                        hGForms = cp.HGForms.Split(',').ToList();
                    }
                    string[] phGForms = c.HGForms.Split(',');
                    foreach (var item in phGForms)   //项目工作包表格中增加的表格也要显示
                    {
                        hGForms.Add(item);
                    }
                    hGForms = hGForms.Distinct().ToList();
                    AspNet.CheckBoxList cblHGForms = (AspNet.CheckBoxList)(this.Grid1.Rows[i].FindControl("cblHGForms"));
                    cblHGForms.Items.Clear();
                    foreach (var hGForm in hGForms)   //加载项目基础库的表格数据
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
                        if (c.HGForms.Contains(cblHGForms.Items[j].Value))   //项目中包含的表格选中
                        {
                            cblHGForms.Items[j].Selected = true;
                        }
                    }
                    List<string> sHForms = new List<string>();
                    if (cp != null && !string.IsNullOrEmpty(cp.SHForms))
                    {
                        sHForms = cp.SHForms.Split(',').ToList();
                    }
                    string[] psHForms = c.SHForms.Split(',');
                    foreach (var item in psHForms)    //项目工作包表格中增加的表格也要显示
                    {
                        sHForms.Add(item);
                    }
                    sHForms = sHForms.Distinct().ToList();
                    AspNet.CheckBoxList cblSHForms = (AspNet.CheckBoxList)(this.Grid1.Rows[i].FindControl("cblSHForms"));
                    cblSHForms.Items.Clear();
                    foreach (var sHForm in sHForms)   //加载项目基础库的表格数据
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
                        if (c.SHForms.Contains(cblSHForms.Items[j].Value))   //项目中包含的表格选中
                        {
                            cblSHForms.Items[j].Selected = true;
                        }
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
            string unitWorkId = string.Empty;
            string workPackageId1 = string.Empty;
            string workPackageId2 = string.Empty;
            string workPackageId3 = string.Empty;
            Model.WBS_UnitWork unitWork = BLL.UnitWorkService.GetUnitWorkByUnitWorkId(this.hdSelectId.Text);
            if (unitWork != null)
            {
                projectType = unitWork.ProjectType;
                unitWorkId = this.hdSelectId.Text;
            }
            Model.WBS_WorkPackage workPackage = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(this.hdSelectId.Text);
            if (workPackage != null)
            {
                if (workPackage.SuperWorkPackageId == null)   //选中第一级分部分项
                {
                    projectType = workPackage.ProjectType;
                    unitWorkId = workPackage.UnitWorkId;
                    workPackageId1 = this.hdSelectId.Text;
                }
                else
                {
                    Model.WBS_WorkPackage pWorkPackage = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(workPackage.SuperWorkPackageId);
                    {
                        if (pWorkPackage.SuperWorkPack == null)    //选中第二级分部分项
                        {
                            projectType = pWorkPackage.ProjectType;
                            unitWorkId = pWorkPackage.UnitWorkId;
                            workPackageId1 = pWorkPackage.WorkPackageId;
                            workPackageId2 = this.hdSelectId.Text;
                        }
                        else
                        {
                            Model.WBS_WorkPackage ppWorkPackage = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(pWorkPackage.SuperWorkPackageId);
                            projectType = ppWorkPackage.ProjectType;
                            unitWorkId = ppWorkPackage.UnitWorkId;
                            workPackageId1 = ppWorkPackage.WorkPackageId;
                            workPackageId2 = pWorkPackage.WorkPackageId;
                            workPackageId3 = this.hdSelectId.Text;
                        }
                    }
                }
            }
            ListItem[] list = new ListItem[2];
            list[0] = new ListItem("建筑工程", "1");
            list[1] = new ListItem("安装工程", "2");
            this.trWBS.Nodes.Clear();
            this.trWBS.ShowBorder = false;
            this.trWBS.ShowHeader = false;
            this.trWBS.EnableIcons = true;
            this.trWBS.AutoScroll = true;
            this.trWBS.EnableSingleClickExpand = true;
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
                    var trUnitWork = from x in BLL.Funs.DB.WBS_UnitWork
                                     where x.ProjectId == this.CurrUser.LoginProjectId && x.SuperUnitWork == null && x.ProjectType == projectType
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
                            rootNode.Nodes.Add(newNode);
                            if (BLL.WorkPackageService.GetWorkPackages1ByUnitWorkId(trUnitWorkItem.UnitWorkId.ToString()) != null)
                            {
                                if (newNode.NodeID == unitWorkId)
                                {
                                    newNode.Expanded = true;
                                    var workPackage1s = from x in Funs.DB.WBS_WorkPackage where x.UnitWorkId == newNode.NodeID && x.SuperWorkPack == null && x.IsApprove == true orderby x.WorkPackageCode select x;
                                    foreach (var workPackage1 in workPackage1s)
                                    {
                                        TreeNode newNode1 = new TreeNode();
                                        string weights = string.Empty;
                                        if (workPackage1.Weights != null)
                                        {
                                            weights = "(" + Convert.ToDouble(workPackage1.Weights).ToString() + "%)";
                                        }
                                        newNode1.Text = workPackage1.PackageContent + weights;
                                        newNode1.NodeID = workPackage1.WorkPackageId;
                                        newNode1.CommandName = "WorkPackage";
                                        newNode1.EnableExpandEvent = true;
                                        newNode1.EnableClickEvent = true;
                                        newNode.Nodes.Add(newNode1);
                                        if (newNode1.NodeID == workPackageId1)
                                        {
                                            newNode1.Expanded = true;
                                            var workPackage2s = from x in Funs.DB.WBS_WorkPackage where x.SuperWorkPackageId == workPackage1.WorkPackageId && x.IsApprove == true orderby x.WorkPackageCode select x;
                                            if (workPackage2s.Count() > 0)
                                            {
                                                foreach (var workPackage2 in workPackage2s)
                                                {
                                                    TreeNode newNode2 = new TreeNode();
                                                    string weights2 = string.Empty;
                                                    if (workPackage2.Weights != null)
                                                    {
                                                        weights2 = "(" + Convert.ToDouble(workPackage2.Weights).ToString() + "%)";
                                                    }
                                                    newNode2.Text = workPackage2.PackageContent + weights2;
                                                    newNode2.NodeID = workPackage2.WorkPackageId;
                                                    newNode2.CommandName = "WorkPackage";
                                                    newNode2.EnableExpandEvent = true;
                                                    newNode2.EnableClickEvent = true;
                                                    newNode1.Nodes.Add(newNode2);
                                                    if (newNode2.NodeID == workPackageId2)
                                                    {
                                                        newNode2.Expanded = true;
                                                        var workPackage3s = from x in Funs.DB.WBS_WorkPackage where x.SuperWorkPackageId == workPackage2.WorkPackageId && x.IsApprove == true orderby x.WorkPackageCode select x;
                                                        if (workPackage3s.Count() > 0)
                                                        {
                                                            foreach (var workPackage3 in workPackage3s)
                                                            {
                                                                TreeNode newNode3 = new TreeNode();
                                                                string weights3 = string.Empty;
                                                                if (workPackage3.Weights != null)
                                                                {
                                                                    weights3 = "(" + Convert.ToDouble(workPackage3.Weights).ToString() + "%)";
                                                                }
                                                                newNode3.Text = workPackage3.PackageContent + weights3;
                                                                newNode3.NodeID = workPackage3.WorkPackageId;
                                                                newNode3.CommandName = "WorkPackage";
                                                                newNode3.EnableExpandEvent = true;
                                                                newNode3.EnableClickEvent = true;
                                                                newNode2.Nodes.Add(newNode3);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        var workPackage3s = from x in Funs.DB.WBS_WorkPackage where x.SuperWorkPackageId == workPackage2.WorkPackageId && x.IsApprove == true select x;
                                                        if (workPackage3s.Count() > 0)
                                                        {
                                                            TreeNode emptyNode = new TreeNode();
                                                            emptyNode.Text = "";
                                                            emptyNode.NodeID = "";
                                                            newNode2.Nodes.Add(emptyNode);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            var workPackage2s = from x in Funs.DB.WBS_WorkPackage where x.SuperWorkPackageId == workPackage1.WorkPackageId && x.IsApprove == true select x;
                                            if (workPackage2s.Count() > 0)
                                            {
                                                TreeNode emptyNode = new TreeNode();
                                                emptyNode.Text = "";
                                                emptyNode.NodeID = "";
                                                newNode1.Nodes.Add(emptyNode);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    TreeNode temp = new TreeNode();
                                    temp.Text = "temp";
                                    temp.NodeID = "temp";
                                    newNode.Nodes.Add(temp);
                                }
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
            }
            this.trWBS.SelectedNodeID = this.hdSelectId.Text;
            BindGrid();
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ControlItemAndCycleMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    //this.btnNew.Hidden = false;
                    this.btnMenuAdd.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuModify.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnIn.Hidden = false;
                    this.btnSave.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    //this.btnMenuDelete.Hidden = false;
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
                    Model.WBS_ControlItemAndCycle c = BLL.ControlItemAndCycleService.GetControlItemAndCycleById(this.Grid1.SelectedRowID);
                    if (c != null)
                    {
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
                        c.HGForms = hGForms;
                        BLL.ControlItemAndCycleService.UpdateControlItemAndCycle(c);
                    }
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
                    Model.WBS_ControlItemAndCycle c = BLL.ControlItemAndCycleService.GetControlItemAndCycleById(this.Grid1.SelectedRowID);
                    if (c != null)
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
                        c.SHForms = sHForms;
                        BLL.ControlItemAndCycleService.UpdateControlItemAndCycle(c);
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
                var q = BLL.ControlItemProjectService.GetItemsByWorkPackageCode(this.trWBS.SelectedNodeID, this.CurrUser.LoginProjectId);
                foreach (var item in q)
                {
                    Model.WBS_ControlItemInit init = BLL.ControlItemInitService.GetControlItemInitByCode(item.ControlItemCode);
                    if (init != null)
                    {
                        item.ControlPoint = init.ControlPoint;
                        item.Weights = init.Weights;
                        item.ControlItemDef = init.ControlItemDef;
                        item.HGForms = init.HGForms;
                        item.SHForms = init.SHForms;
                        item.Standard = init.Standard;
                        item.ClauseNo = init.ClauseNo;
                        item.CheckNum = 1;
                        BLL.ControlItemProjectService.UpdateControlItemProject(item);
                    }
                    else
                    {
                        BLL.ControlItemProjectService.DeleteControlItemProject(item.ControlItemCode, this.CurrUser.LoginProjectId);
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
            //    Model.WBS_ControlItemAndCycle cp = BLL.ControlItemAndCycleService.GetControlItemAndCycleById(this.Grid1.Rows[i].RowID);
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
            //    BLL.ControlItemAndCycleService.UpdateControlItemAndCycle(cp);
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
            //    Model.WBS_ControlItemAndCycle cp = BLL.ControlItemAndCycleService.GetControlItemAndCycleById(this.Grid1.Rows[i].RowID);
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
            //    BLL.ControlItemAndCycleService.UpdateControlItemAndCycle(cp);
            //}
            //BindGrid();
        }
        #endregion
    }
}