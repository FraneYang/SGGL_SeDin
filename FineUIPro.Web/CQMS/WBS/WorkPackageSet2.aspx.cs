using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Newtonsoft.Json.Linq;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.CQMS.WBS
{
    public partial class WorkPackageSet2 : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string WorkPackageId
        {
            get
            {
                return (string)ViewState["WorkPackageId"];
            }
            set
            {
                ViewState["WorkPackageId"] = value;
            }
        }

        /// <summary>
        /// 主键
        /// </summary>
        public string UnitWorkId
        {
            get
            {
                return (string)ViewState["UnitWorkId"];
            }
            set
            {
                ViewState["UnitWorkId"] = value;
            }
        }

        /// <summary>
        /// 工程类型
        /// </summary>
        public string ProjectType
        {
            get
            {
                return (string)ViewState["ProjectType"];
            }
            set
            {
                ViewState["ProjectType"] = value;
            }
        }

        /// <summary>
        /// 明细集合
        /// </summary>
        private List<Model.WBS_WorkPackage> workPackages = new List<Model.WBS_WorkPackage>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                workPackages.Clear();
                WorkPackageId = Request.Params["WorkPackageId"];
                UnitWorkId = Request.Params["UnitWorkId"];
                Model.WBS_WorkPackage workPackage = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(WorkPackageId);
                if (workPackage.Costs != null)
                {
                    this.hdTotalValue.Text = workPackage.Costs.ToString();
                }
                else
                {
                    this.hdTotalValue.Text = "0";
                }
                ProjectType = workPackage.ProjectType;
                if (workPackage.SuperWorkPackageId == null)
                {
                    this.Grid1.Columns[1].HeaderText = "第2级";
                }
                else
                {
                    this.Grid1.Columns[1].HeaderText = "第3级";
                }
                UnitWorkId = workPackage.UnitWorkId;
                InitTreeMenu();
                var workPackageProjects = BLL.WorkPackageProjectService.GetWorkPackageProjects2ByWorkPackageCode(workPackage.InitWorkPackageCode, this.CurrUser.LoginProjectId);
                var addWorkPackages = BLL.WorkPackageService.GetAllWorkPackagesBySuperWorkPackageId(WorkPackageId);
                foreach (var workPackageProject in workPackageProjects)
                {
                    Model.WBS_WorkPackage newWorkPackageProject = new Model.WBS_WorkPackage();
                    newWorkPackageProject.WorkPackageCode = workPackageProject.WorkPackageCode;
                    newWorkPackageProject.WorkPackageId = SQLHelper.GetNewID(typeof(Model.WBS_WorkPackage));
                    newWorkPackageProject.PackageContent = workPackageProject.PackageContent;
                    newWorkPackageProject.Weights = null;
                    workPackages.Add(newWorkPackageProject);
                }
                foreach (var addWorkPackage in addWorkPackages)
                {
                    Model.WBS_WorkPackage newAddWorkPackage = new Model.WBS_WorkPackage();
                    newAddWorkPackage.WorkPackageCode = addWorkPackage.InitWorkPackageCode;
                    newAddWorkPackage.WorkPackageId = addWorkPackage.WorkPackageId;
                    Model.WBS_WorkPackageProject workPackageProject = BLL.WorkPackageProjectService.GetWorkPackageProjectByWorkPackageCode(addWorkPackage.InitWorkPackageCode, this.CurrUser.LoginProjectId);
                    if (workPackageProject != null)
                    {
                        newAddWorkPackage.PackageContent = workPackageProject.PackageContent;
                        if (addWorkPackage.PackageContent.Contains("-"))
                        {
                            newAddWorkPackage.SuperWorkPack = addWorkPackage.PackageContent.Substring(addWorkPackage.PackageContent.IndexOf("-") + 1);
                        }
                    }
                    newAddWorkPackage.Weights = addWorkPackage.Weights;
                    newAddWorkPackage.Costs = addWorkPackage.Costs;
                    workPackages.Add(newAddWorkPackage);
                }
                workPackages = workPackages.OrderBy(x => x.WorkPackageCode).ToList();
                this.Grid1.DataSource = workPackages;
                this.Grid1.DataBind();
                for (int i = 0; i < this.Grid1.Rows.Count; i++)
                {
                    Model.WBS_WorkPackage w = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(this.Grid1.Rows[i].RowID.ToString());
                    AspNet.CheckBox cb = (AspNet.CheckBox)(this.Grid1.Rows[i].FindControl("cbSelect"));
                    if (w != null && w.IsApprove == true)   //已生成的分部分项内容
                    {
                        cb.Checked = true;
                    }
                    else
                    {
                        var w2 = workPackages.FirstOrDefault(x => x.WorkPackageId == this.Grid1.Rows[i].RowID.ToString());
                        if (w2 != null && w2.IsCopy == true)
                        {
                            cb.Checked = true;
                        }
                    }
                }
                GetTotalWeights();
            }
            else
            {
                if (GetRequestEventArgument() == "UPDATE_SUMMARY")
                {
                    GetTotalWeights();
                }
            }
        }

        #region 获取总权重
        private void GetTotalWeights()
        {
            decimal totalWeights = 0;
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                AspNet.CheckBox cb = (AspNet.CheckBox)(this.Grid1.Rows[i].FindControl("cbSelect"));
                if (cb.Checked)   //总权重只累计选择项
                {
                    string weights = values.Value<string>("Weights");
                    try
                    {
                        totalWeights += Convert.ToDecimal(weights);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            this.lbWeights.Text = totalWeights.ToString();
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
            if (ProjectType == "1")
            {
                TreeNode rootNode1 = new TreeNode();
                rootNode1.Text = "建筑工程";
                rootNode1.NodeID = "1";
                rootNode1.CommandName = "ProjectType";
                //rootNode1.EnableExpandEvent = true;
                rootNode1.EnableClickEvent = true;
                this.trWBS.Nodes.Add(rootNode1);
                this.GetNodes(rootNode1.Nodes, rootNode1.NodeID);
            }
            else
            {
                TreeNode rootNode2 = new TreeNode();
                rootNode2.Text = "安装工程";
                rootNode2.NodeID = "2";
                rootNode2.CommandName = "ProjectType";
                //rootNode2.EnableExpandEvent = true;
                rootNode2.EnableClickEvent = true;
                this.trWBS.Nodes.Add(rootNode2);
                this.GetNodes(rootNode2.Nodes, rootNode2.NodeID);
            }
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

        #region Grid行点击事件
        /// <summary>
        /// Grid行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string id = this.Grid1.SelectedRow.RowID;
            string code = this.Grid1.SelectedRow.DataKeys[1].ToString();
            //保存页面数据
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                string name = values.Value<string>("SuperWorkPack");
                string weights = values.Value<string>("Weights");
                AspNet.CheckBox cb = (AspNet.CheckBox)(this.Grid1.Rows[i].FindControl("cbSelect"));
                var w = workPackages.FirstOrDefault(x => x.WorkPackageId == this.Grid1.Rows[i].RowID);
                if (w != null)
                {
                    w.SuperWorkPack = name;
                    try
                    {
                        w.Weights = Convert.ToDecimal(weights);
                    }
                    catch (Exception)
                    {
                        w.Weights = null;
                    }
                    if (cb.Checked)
                    {
                        w.IsCopy = true;
                    }
                }
            }
            workPackages = GetDetails();
            if (e.CommandName == "add")//增加
            {
                Model.WBS_WorkPackageProject workPackageProject = BLL.WorkPackageProjectService.GetWorkPackageProjectByWorkPackageCode(code, this.CurrUser.LoginProjectId);
                Model.WBS_WorkPackage newWorkPackage = new Model.WBS_WorkPackage();
                newWorkPackage.WorkPackageCode = workPackageProject.WorkPackageCode;
                newWorkPackage.WorkPackageId = SQLHelper.GetNewID(typeof(Model.WBS_WorkPackage));
                newWorkPackage.PackageContent = workPackageProject.PackageContent;
                workPackages.Add(newWorkPackage);
                workPackages = workPackages.OrderBy(x => x.WorkPackageCode).ToList();
                this.Grid1.DataSource = workPackages;
                this.Grid1.DataBind();
            }
            if (e.CommandName == "del")//删除
            {
                var w = workPackages.FirstOrDefault(x => x.WorkPackageId == id);
                if (w != null)
                {
                    workPackages.Remove(w);
                    Model.WBS_WorkPackage oldw = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(id);
                    if (oldw != null)
                    {
                        var child1s = BLL.WorkPackageService.GetAllWorkPackagesBySuperWorkPackageId(id);
                        BLL.ControlItemAndCycleService.DeleteAllControlItemAndCycle(id);
                        BLL.WorkPackageService.DeleteWorkPackage(id);
                        if (child1s.Count > 0)
                        {
                            foreach (var child1 in child1s)
                            {
                                BLL.ControlItemAndCycleService.DeleteAllControlItemAndCycle(child1.WorkPackageId);
                                BLL.WorkPackageService.DeleteWorkPackage(child1.WorkPackageId);
                            }
                        }
                    }
                }
                workPackages = workPackages.OrderBy(x => x.WorkPackageCode).ToList();
                this.Grid1.DataSource = workPackages;
                this.Grid1.DataBind();
            }
            for (int i = 0; i < this.Grid1.Rows.Count; i++)
            {
                Model.WBS_WorkPackage w = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(this.Grid1.Rows[i].RowID.ToString());
                AspNet.CheckBox cb = (AspNet.CheckBox)(this.Grid1.Rows[i].FindControl("cbSelect"));
                if (w != null && w.IsApprove == true)   //已生成的分部分项内容
                {
                    cb.Checked = true;
                }
                else
                {
                    var w2 = workPackages.FirstOrDefault(x => x.WorkPackageId == this.Grid1.Rows[i].RowID.ToString());
                    if (w2 != null && w2.IsCopy == true)
                    {
                        cb.Checked = true;
                    }
                }
            }
            GetTotalWeights();
        }
        #endregion

        private List<Model.WBS_WorkPackage> GetDetails()
        {
            workPackages.Clear();
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                string txtPackageContent = values.Value<string>("PackageContent");
                string txtName = values.Value<string>("SuperWorkPack");
                string txtWeights = values.Value<string>("Weights");
                string txtCosts = values.Value<string>("Costs");
                AspNet.CheckBox ckbWorkPackageCode = (AspNet.CheckBox)(this.Grid1.Rows[i].FindControl("cbSelect"));
                Model.WBS_WorkPackage newAddWorkPackage = new Model.WBS_WorkPackage();
                newAddWorkPackage.WorkPackageCode = Grid1.Rows[i].DataKeys[1].ToString();
                newAddWorkPackage.WorkPackageId = Grid1.Rows[i].DataKeys[0].ToString();
                newAddWorkPackage.SuperWorkPack = txtName;
                newAddWorkPackage.PackageContent = txtPackageContent;
                if (!string.IsNullOrEmpty(txtWeights))
                {
                    newAddWorkPackage.Weights = Convert.ToDecimal(txtWeights);
                }
                if (!string.IsNullOrEmpty(txtCosts))
                {
                    newAddWorkPackage.Costs = Convert.ToDecimal(txtCosts);
                }
                if (ckbWorkPackageCode.Checked)
                {
                    newAddWorkPackage.IsCopy = true;
                }
                workPackages.Add(newAddWorkPackage);
            }
            return workPackages;
        }

        #region 选择事件
        /// <summary>
        /// 选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbSelect_CheckedChanged(object sender, EventArgs e)
        {
            AspNet.CheckBox cb = sender as AspNet.CheckBox;
            if (cb.Checked)
            {

            }
            else
            {
                this.hdId.Text = this.Grid1.SelectedRowID;
                //PageContext.RegisterStartupScript(String.Format("ShowDel();"));
            }
            GetTotalWeights();
        }
        #endregion

        #region 删除事件
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDel_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.hdId.Text))
            {
                string id = this.hdId.Text;
                var w = workPackages.FirstOrDefault(x => x.WorkPackageId == id);
                if (w != null)
                {
                    workPackages.Remove(w);
                    Model.WBS_WorkPackage oldw = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(id);
                    if (oldw != null)
                    {
                        var child1s = BLL.WorkPackageService.GetAllWorkPackagesBySuperWorkPackageId(id);
                        BLL.ControlItemAndCycleService.DeleteAllControlItemAndCycle(id);
                        BLL.WorkPackageService.DeleteWorkPackage(id);
                        if (child1s.Count > 0)
                        {
                            foreach (var child1 in child1s)
                            {
                                var child2s = BLL.WorkPackageService.GetAllWorkPackagesBySuperWorkPackageId(child1.WorkPackageId);
                                BLL.ControlItemAndCycleService.DeleteAllControlItemAndCycle(child1.WorkPackageId);
                                BLL.WorkPackageService.DeleteWorkPackage(child1.WorkPackageId);
                                if (child2s.Count > 0)
                                {
                                    foreach (var child2 in child2s)
                                    {
                                        BLL.ControlItemAndCycleService.DeleteAllControlItemAndCycle(child2.WorkPackageId);
                                        BLL.WorkPackageService.DeleteWorkPackage(child2.WorkPackageId);
                                    }
                                }
                            }
                        }
                    }
                }
                workPackages = workPackages.OrderBy(x => x.WorkPackageCode).ToList();
                this.Grid1.DataSource = workPackages;
                this.Grid1.DataBind();
                for (int i = 0; i < this.Grid1.Rows.Count; i++)
                {
                    Model.WBS_WorkPackage w1 = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(this.Grid1.Rows[i].RowID.ToString());
                    AspNet.CheckBox cb = (AspNet.CheckBox)(this.Grid1.Rows[i].FindControl("cbSelect"));
                    if (w1 != null && w.IsApprove == true)   //已生成的分部分项内容
                    {
                        cb.Checked = true;
                    }
                    else
                    {
                        var w2 = workPackages.FirstOrDefault(x => x.WorkPackageId == this.Grid1.Rows[i].RowID.ToString());
                        if (w2 != null && w2.IsCopy == true)
                        {
                            cb.Checked = true;
                        }
                    }
                }
                GetTotalWeights();
            }
        }
        #endregion

        #region 重新选择未删除项事件
        /// <summary>
        /// 重新选择未删除项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReCheck_Click(object sender, EventArgs e)
        {
            this.Grid1.DataSource = workPackages;
            this.Grid1.DataBind();
            for (int i = 0; i < this.Grid1.Rows.Count; i++)
            {
                Model.WBS_WorkPackage w = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(this.Grid1.Rows[i].RowID.ToString());
                AspNet.CheckBox cb = (AspNet.CheckBox)(this.Grid1.Rows[i].FindControl("cbSelect"));
                if (w != null && w.IsApprove == true)   //已生成的分部分项内容
                {
                    cb.Checked = true;
                }
                else
                {
                    var w2 = workPackages.FirstOrDefault(x => x.WorkPackageId == this.Grid1.Rows[i].RowID.ToString());
                    if (w2 != null && w2.IsCopy == true)
                    {
                        cb.Checked = true;
                    }
                }
            }
            GetTotalWeights();
        }
        #endregion

        #region 保存事件
        /// <summary>
        /// 临时保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSet_Click(object sender, EventArgs e)
        {
            string workPackageCode = string.Empty;
            int num = 1;
            string code = string.Empty;
            string name = string.Empty;
            Model.WBS_WorkPackage parentWorkPackage = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(WorkPackageId);
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                string workPackageId = this.Grid1.Rows[i].DataKeys[0].ToString();
                Model.WBS_WorkPackage oldWorkPackage = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(workPackageId);
                AspNet.CheckBox ckbWorkPackageCode = (AspNet.CheckBox)(this.Grid1.Rows[i].FindControl("cbSelect"));
                string workPackageCode2 = this.Grid1.Rows[i].DataKeys[1].ToString();
                string txtName = values.Value<string>("SuperWorkPack");
                string txtWeights = values.Value<string>("Weights");
                string txtCosts = values.Value<string>("Costs");
                name = string.Empty;
                if (!string.IsNullOrEmpty(txtName.Trim()))
                {
                    name = "-" + txtName.Trim();
                }
                if (!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(txtWeights))
                {
                    Model.WBS_WorkPackageProject workPackageProject = BLL.WorkPackageProjectService.GetWorkPackageProjectByWorkPackageCode(workPackageCode2, this.CurrUser.LoginProjectId);
                    if (oldWorkPackage == null)   //新增内容
                    {
                        Model.WBS_WorkPackage newWorkPackage = new Model.WBS_WorkPackage();
                        if (workPackageCode != workPackageProject.WorkPackageCode)  //循环至新的分部
                        {
                            workPackageCode = workPackageProject.WorkPackageCode;
                            var oldWorkPackages = BLL.WorkPackageService.GetWorkPackagesByInitWorkPackageCodeAndUnitWorkId(workPackageCode, UnitWorkId);
                            if (oldWorkPackages.Count > 0)  //该工作包已存在内容
                            {
                                var old = oldWorkPackages.First();
                                string oldStr = old.WorkPackageCode.Substring(old.WorkPackageCode.Length - 2);
                                num = Convert.ToInt32(oldStr) + 1;
                                if (num < 10)
                                {
                                    code = "0" + num.ToString();
                                }
                                else
                                {
                                    code = num.ToString();
                                }
                            }
                            else
                            {
                                num = 1;
                                code = "01";
                            }
                        }
                        else
                        {
                            if (num < 10)
                            {
                                code = "0" + num.ToString();
                            }
                            else
                            {
                                code = num.ToString();
                            }
                        }
                        newWorkPackage.WorkPackageId = SQLHelper.GetNewID(typeof(Model.WBS_WorkPackage));
                        newWorkPackage.WorkPackageCode = parentWorkPackage.WorkPackageCode + workPackageCode.Substring(workPackageCode.IndexOf(parentWorkPackage.InitWorkPackageCode) + parentWorkPackage.InitWorkPackageCode.Length).Replace("00", "0000") + code;
                        newWorkPackage.ProjectId = this.CurrUser.LoginProjectId;
                        newWorkPackage.UnitWorkId = UnitWorkId;
                        newWorkPackage.PackageContent = workPackageProject.PackageContent + name;
                        newWorkPackage.SuperWorkPack = workPackageProject.SuperWorkPack;
                        newWorkPackage.SuperWorkPackageId = WorkPackageId;
                        newWorkPackage.IsChild = workPackageProject.IsChild;
                        newWorkPackage.PackageCode = code;
                        newWorkPackage.ProjectType = workPackageProject.ProjectType;
                        newWorkPackage.InitWorkPackageCode = workPackageProject.WorkPackageCode;
                        try
                        {
                            newWorkPackage.Weights = Convert.ToDecimal(txtWeights.Trim());
                        }
                        catch (Exception)
                        {
                            newWorkPackage.Weights = null;
                        }
                        try
                        {
                            newWorkPackage.Costs = Convert.ToDecimal(txtCosts.Trim());
                        }
                        catch (Exception)
                        {
                            newWorkPackage.Costs = null;
                        }
                        BLL.WorkPackageService.AddWorkPackage(newWorkPackage);
                        num++;
                    }
                    else
                    {
                        oldWorkPackage.PackageContent = workPackageProject.PackageContent + name;
                        try
                        {
                            oldWorkPackage.Weights = Convert.ToDecimal(txtWeights.Trim());
                        }
                        catch (Exception)
                        {
                            oldWorkPackage.Weights = null;
                        }
                        try
                        {
                            oldWorkPackage.Costs = Convert.ToDecimal(txtCosts.Trim());
                        }
                        catch (Exception)
                        {
                            oldWorkPackage.Costs = null;
                        }
                        BLL.WorkPackageService.UpdateWorkPackage(oldWorkPackage);
                    }
                }
                //else   //未选中项
                //{
                //    if (oldWorkPackage != null)   //已存在内容
                //    {
                //        oldWorkPackage.IsApprove = null;
                //        BLL.WorkPackageService.UpdateWorkPackage(oldWorkPackage);
                //    }
                //}
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(WorkPackageId) + ActiveWindow.GetHidePostBackReference());
            //ShowNotify("保存成功！", MessageBoxIcon.Success);
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
                int i = mergedRow.Value<int>("index");
                AspNet.CheckBox cb = (AspNet.CheckBox)(this.Grid1.Rows[i].FindControl("cbSelect"));
                if (cb.Checked)   //总权重只累计选择项
                {
                    string weights1 = values.Value<string>("Weights");
                    try
                    {
                        weights += Convert.ToDecimal(weights1);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            if (weights != 100)
            {
                Alert.ShowInTop("权重值不是100，请调整后再保存！", MessageBoxIcon.Warning);
                return;
            }
            string workPackageCode = string.Empty;
            int num = 1;
            string code = string.Empty;
            string name = string.Empty;
            Model.WBS_WorkPackage parentWorkPackage = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(WorkPackageId);
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                string workPackageId = this.Grid1.Rows[i].DataKeys[0].ToString();
                Model.WBS_WorkPackage oldWorkPackage = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(workPackageId);
                AspNet.CheckBox ckbWorkPackageCode = (AspNet.CheckBox)(this.Grid1.Rows[i].FindControl("cbSelect"));
                if (ckbWorkPackageCode.Checked)
                {
                    string workPackageCode2 = this.Grid1.Rows[i].DataKeys[1].ToString();
                    string txtName = values.Value<string>("SuperWorkPack");
                    string txtWeights = values.Value<string>("Weights");
                    string txtCosts = values.Value<string>("Costs");
                    name = string.Empty;
                    if (!string.IsNullOrEmpty(txtName.Trim()))
                    {
                        name = "-" + txtName.Trim();
                    }
                    Model.WBS_WorkPackageProject workPackageProject = BLL.WorkPackageProjectService.GetWorkPackageProjectByWorkPackageCode(workPackageCode2, this.CurrUser.LoginProjectId);
                    if (oldWorkPackage == null)   //新增内容
                    {
                        Model.WBS_WorkPackage newWorkPackage = new Model.WBS_WorkPackage();
                        if (workPackageCode != workPackageProject.WorkPackageCode)  //循环至新的分部
                        {
                            workPackageCode = workPackageProject.WorkPackageCode;
                            var oldWorkPackages = BLL.WorkPackageService.GetWorkPackagesByInitWorkPackageCodeAndUnitWorkId(workPackageCode, UnitWorkId);
                            if (oldWorkPackages.Count > 0)  //该工作包已存在内容
                            {
                                var old = oldWorkPackages.First();
                                string oldStr = old.WorkPackageCode.Substring(old.WorkPackageCode.Length - 2);
                                num = Convert.ToInt32(oldStr) + 1;
                                if (num < 10)
                                {
                                    code = "0" + num.ToString();
                                }
                                else
                                {
                                    code = num.ToString();
                                }
                            }
                            else
                            {
                                num = 1;
                                code = "01";
                            }
                        }
                        else
                        {
                            if (num < 10)
                            {
                                code = "0" + num.ToString();
                            }
                            else
                            {
                                code = num.ToString();
                            }
                        }
                        newWorkPackage.WorkPackageId = SQLHelper.GetNewID(typeof(Model.WBS_WorkPackage));
                        newWorkPackage.WorkPackageCode = parentWorkPackage.WorkPackageCode + workPackageCode.Substring(workPackageCode.IndexOf(parentWorkPackage.InitWorkPackageCode) + parentWorkPackage.InitWorkPackageCode.Length).Replace("00", "0000") + code;
                        newWorkPackage.ProjectId = this.CurrUser.LoginProjectId;
                        newWorkPackage.UnitWorkId = UnitWorkId;
                        newWorkPackage.PackageContent = workPackageProject.PackageContent + name;
                        newWorkPackage.SuperWorkPack = workPackageProject.SuperWorkPack;
                        newWorkPackage.SuperWorkPackageId = WorkPackageId;
                        newWorkPackage.IsChild = workPackageProject.IsChild;
                        newWorkPackage.PackageCode = code;
                        newWorkPackage.IsApprove = true;
                        newWorkPackage.ProjectType = workPackageProject.ProjectType;
                        newWorkPackage.InitWorkPackageCode = workPackageProject.WorkPackageCode;
                        try
                        {
                            newWorkPackage.Weights = Convert.ToDecimal(txtWeights.Trim());
                        }
                        catch (Exception)
                        {
                            newWorkPackage.Weights = null;
                        }
                        try
                        {
                            newWorkPackage.Costs = Convert.ToDecimal(txtCosts.Trim());
                        }
                        catch (Exception)
                        {
                            newWorkPackage.Costs = null;
                        }
                        BLL.WorkPackageService.AddWorkPackage(newWorkPackage);
                        num++;
                    }
                    else
                    {
                        oldWorkPackage.PackageContent = workPackageProject.PackageContent + name;
                        try
                        {
                            oldWorkPackage.Weights = Convert.ToDecimal(txtWeights.Trim());
                        }
                        catch (Exception)
                        {
                            oldWorkPackage.Weights = null;
                        }
                        try
                        {
                            oldWorkPackage.Costs = Convert.ToDecimal(txtCosts.Trim());
                        }
                        catch (Exception)
                        {
                            oldWorkPackage.Costs = null;
                        }
                        oldWorkPackage.IsApprove = true;
                        BLL.WorkPackageService.UpdateWorkPackage(oldWorkPackage);
                    }
                }
                else   //未选中项
                {
                    if (oldWorkPackage != null)   //已存在内容
                    {
                        oldWorkPackage.IsApprove = null;
                        BLL.WorkPackageService.UpdateWorkPackage(oldWorkPackage);
                    }
                }
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(WorkPackageId) + ActiveWindow.GetHidePostBackReference());
            //ShowNotify("保存成功！", MessageBoxIcon.Success);
        }
        #endregion

        protected void cbAll_CheckedChanged(object sender, CheckedEventArgs e)
        {
            //保存页面数据
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                string name = values.Value<string>("SuperWorkPack");
                string weights = values.Value<string>("Weights");
                AspNet.CheckBox cb = (AspNet.CheckBox)(this.Grid1.Rows[i].FindControl("cbSelect"));
                var w = workPackages.FirstOrDefault(x => x.WorkPackageId == this.Grid1.Rows[i].RowID);
                if (w != null)
                {
                    w.SuperWorkPack = name;
                    try
                    {
                        w.Weights = Convert.ToDecimal(weights);
                    }
                    catch (Exception)
                    {
                        w.Weights = null;
                    }
                    if (cb.Checked)
                    {
                        w.IsCopy = true;
                    }
                }
            }
            workPackages = workPackages.OrderBy(x => x.WorkPackageCode).ToList();
            this.Grid1.DataSource = workPackages;
            this.Grid1.DataBind();
            for (int i = 0; i < this.Grid1.Rows.Count; i++)
            {
                AspNet.CheckBox ckbWorkPackageCode = (AspNet.CheckBox)(this.Grid1.Rows[i].FindControl("cbSelect"));
                ckbWorkPackageCode.Checked = e.Checked;
            }
            GetTotalWeights();
        }
    }
}