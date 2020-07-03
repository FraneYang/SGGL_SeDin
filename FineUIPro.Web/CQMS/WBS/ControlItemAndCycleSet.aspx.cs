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
    public partial class ControlItemAndCycleSet : PageBase
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
        private static List<Model.WBS_ControlItemAndCycle> controlItemAndCycles = new List<Model.WBS_ControlItemAndCycle>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                controlItemAndCycles.Clear();
                WorkPackageId = Request.Params["WorkPackageId"];
                Model.WBS_WorkPackage workPackage = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(WorkPackageId);
                ProjectType = workPackage.ProjectType;
                InitTreeMenu();
                var controlItemProjects = BLL.ControlItemProjectService.GetItemsByWorkPackageCode(workPackage.InitWorkPackageCode, this.CurrUser.LoginProjectId);
                var addControlItemAndCycles = BLL.ControlItemAndCycleService.GetListByWorkPackageId(WorkPackageId);
                foreach (var controlItemProject in controlItemProjects)
                {
                    Model.WBS_ControlItemAndCycle newControlItemAndCycle = new Model.WBS_ControlItemAndCycle();
                    newControlItemAndCycle.ControlItemAndCycleCode = controlItemProject.ControlItemCode;
                    newControlItemAndCycle.ControlItemAndCycleId = SQLHelper.GetNewID(typeof(Model.WBS_WorkPackage));
                    newControlItemAndCycle.ControlItemContent = controlItemProject.ControlItemContent;
                    newControlItemAndCycle.Weights = controlItemProject.Weights;
                    controlItemAndCycles.Add(newControlItemAndCycle);
                }
                foreach (var addControlItemAndCycle in addControlItemAndCycles)
                {
                    Model.WBS_ControlItemAndCycle newAddControlItemAndCycle = new Model.WBS_ControlItemAndCycle();
                    newAddControlItemAndCycle.ControlItemAndCycleCode = addControlItemAndCycle.InitControlItemCode;
                    newAddControlItemAndCycle.ControlItemAndCycleId = addControlItemAndCycle.ControlItemAndCycleId;
                    Model.WBS_ControlItemProject controlItemProject = BLL.ControlItemProjectService.GetControlItemProjectByCode(addControlItemAndCycle.InitControlItemCode, this.CurrUser.LoginProjectId);
                    if (controlItemProject != null)
                    {
                        newAddControlItemAndCycle.ControlItemContent = controlItemProject.ControlItemContent;
                        if (addControlItemAndCycle.ControlItemContent.Contains("-"))
                        {
                            newAddControlItemAndCycle.AttachUrl = addControlItemAndCycle.ControlItemContent.Substring(addControlItemAndCycle.ControlItemContent.IndexOf("-") + 1);
                        }
                    }
                    newAddControlItemAndCycle.PlanCompleteDate = addControlItemAndCycle.PlanCompleteDate;
                    newAddControlItemAndCycle.Weights = addControlItemAndCycle.Weights;
                    controlItemAndCycles.Add(newAddControlItemAndCycle);
                }
                controlItemAndCycles = controlItemAndCycles.OrderBy(x => x.ControlItemAndCycleCode).ToList();
                this.Grid1.DataSource = controlItemAndCycles;
                this.Grid1.DataBind();
                for (int i = 0; i < this.Grid1.Rows.Count; i++)
                {
                    Model.WBS_ControlItemAndCycle c = BLL.ControlItemAndCycleService.GetControlItemAndCycleById(this.Grid1.Rows[i].RowID.ToString());
                    AspNet.CheckBox cb = (AspNet.CheckBox)(this.Grid1.Rows[i].FindControl("cbSelect"));
                    if (c != null && c.IsApprove == true)   //已生成的分部分项内容
                    {
                        cb.Checked = true;
                    }
                    else
                    {
                        var c2 = controlItemAndCycles.FirstOrDefault(x => x.ControlItemAndCycleId == this.Grid1.Rows[i].RowID.ToString());
                        if (c2 != null && c2.IsApprove == true)
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
                string name = values.Value<string>("AttachUrl");
                string weights = values.Value<string>("Weights");
                AspNet.CheckBox cb = (AspNet.CheckBox)(this.Grid1.Rows[i].FindControl("cbSelect"));
                string planCompleteDate = values.Value<string>("PlanCompleteDate");
                var c = controlItemAndCycles.FirstOrDefault(x => x.ControlItemAndCycleId == this.Grid1.Rows[i].RowID);
                if (c != null)
                {
                    c.AttachUrl = name;
                    try
                    {
                        c.Weights = Convert.ToDecimal(weights);
                    }
                    catch (Exception)
                    {
                        c.Weights = null;
                    }
                    if (cb.Checked)
                    {
                        c.IsApprove = true;
                    }
                    if (!string.IsNullOrEmpty(planCompleteDate))
                    {
                        c.PlanCompleteDate = Convert.ToDateTime(planCompleteDate);
                    }
                }
            }
            if (e.CommandName == "add")//增加
            {
                Model.WBS_ControlItemProject controlItemProject = BLL.ControlItemProjectService.GetControlItemProjectByCode(code, this.CurrUser.LoginProjectId);
                Model.WBS_ControlItemAndCycle newControlItemAndCycle = new Model.WBS_ControlItemAndCycle();
                newControlItemAndCycle.ControlItemAndCycleCode = controlItemProject.ControlItemCode;
                newControlItemAndCycle.ControlItemAndCycleId = SQLHelper.GetNewID(typeof(Model.WBS_WorkPackage));
                newControlItemAndCycle.ControlItemContent = controlItemProject.ControlItemContent;
                controlItemAndCycles.Add(newControlItemAndCycle);
                controlItemAndCycles = controlItemAndCycles.OrderBy(x => x.ControlItemAndCycleCode).ToList();
                this.Grid1.DataSource = controlItemAndCycles;
                this.Grid1.DataBind();
            }
            if (e.CommandName == "del")//删除
            {
                var w = controlItemAndCycles.FirstOrDefault(x => x.ControlItemAndCycleId == id);
                if (w != null)
                {
                    controlItemAndCycles.Remove(w);
                    Model.WBS_ControlItemAndCycle oldc = BLL.ControlItemAndCycleService.GetControlItemAndCycleById(id);
                    if (oldc != null)
                    {
                        BLL.ControlItemAndCycleService.DeleteControlItemAndCycle(id);
                    }
                }
                controlItemAndCycles = controlItemAndCycles.OrderBy(x => x.ControlItemAndCycleCode).ToList();
                this.Grid1.DataSource = controlItemAndCycles;
                this.Grid1.DataBind();
            }
            for (int i = 0; i < this.Grid1.Rows.Count; i++)
            {
                Model.WBS_ControlItemAndCycle c = BLL.ControlItemAndCycleService.GetControlItemAndCycleById(this.Grid1.Rows[i].RowID.ToString());
                AspNet.CheckBox cb = (AspNet.CheckBox)(this.Grid1.Rows[i].FindControl("cbSelect"));
                if (c != null && c.IsApprove == true)   //已生成的分部分项内容
                {
                    cb.Checked = true;
                }
                else
                {
                    var c2 = controlItemAndCycles.FirstOrDefault(x => x.ControlItemAndCycleId == this.Grid1.Rows[i].RowID.ToString());
                    if (c2 != null && c2.IsApprove == true)
                    {
                        cb.Checked = true;
                    }
                }
            }
            GetTotalWeights();
        }
        #endregion

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
                var w = controlItemAndCycles.FirstOrDefault(x => x.ControlItemAndCycleId == id);
                if (w != null)
                {
                    controlItemAndCycles.Remove(w);
                    Model.WBS_ControlItemAndCycle oldc = BLL.ControlItemAndCycleService.GetControlItemAndCycleById(id);
                    if (oldc != null)
                    {
                        BLL.ControlItemAndCycleService.DeleteControlItemAndCycle(id);
                    }
                }
                controlItemAndCycles = controlItemAndCycles.OrderBy(x => x.ControlItemAndCycleCode).ToList();
                this.Grid1.DataSource = controlItemAndCycles;
                this.Grid1.DataBind();
                for (int i = 0; i < this.Grid1.Rows.Count; i++)
                {
                    Model.WBS_ControlItemAndCycle c = BLL.ControlItemAndCycleService.GetControlItemAndCycleById(this.Grid1.Rows[i].RowID.ToString());
                    AspNet.CheckBox cb = (AspNet.CheckBox)(this.Grid1.Rows[i].FindControl("cbSelect"));
                    if (c != null && c.IsApprove == true)   //已生成的分部分项内容
                    {
                        cb.Checked = true;
                    }
                    else
                    {
                        var c2 = controlItemAndCycles.FirstOrDefault(x => x.ControlItemAndCycleId == this.Grid1.Rows[i].RowID.ToString());
                        if (c2 != null && c2.IsApprove == true)
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
            this.Grid1.DataSource = controlItemAndCycles;
            this.Grid1.DataBind();
            for (int i = 0; i < this.Grid1.Rows.Count; i++)
            {
                Model.WBS_ControlItemAndCycle c = BLL.ControlItemAndCycleService.GetControlItemAndCycleById(this.Grid1.Rows[i].RowID.ToString());
                AspNet.CheckBox cb = (AspNet.CheckBox)(this.Grid1.Rows[i].FindControl("cbSelect"));
                if (c != null && c.IsApprove == true)   //已生成的分部分项内容
                {
                    cb.Checked = true;
                }
                else
                {
                    var c2 = controlItemAndCycles.FirstOrDefault(x => x.ControlItemAndCycleId == this.Grid1.Rows[i].RowID.ToString());
                    if (c2 != null && c2.IsApprove == true)
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
                if (cb.Checked)   //判断是否选择了
                {
                    string weights1 = values.Value<string>("Weights");
                    if (!string.IsNullOrEmpty(weights1))
                    {
                        string planCompleteDate = values.Value<string>("PlanCompleteDate");
                        if (string.IsNullOrEmpty(planCompleteDate))   //选择有权重的项，计划完成时间不能为空
                        {
                            Alert.ShowInTop("有权重值的工作包，计划完成时间不能为空！", MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
            }
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
            string controlItemCode = string.Empty;
            int num = 1;
            string code = string.Empty;
            string name = string.Empty;
            Model.WBS_WorkPackage parentWorkPackage = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(WorkPackageId);
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                string controlItemAndCycleId = this.Grid1.Rows[i].DataKeys[0].ToString();
                Model.WBS_ControlItemAndCycle oldControlItemAndCycle = BLL.ControlItemAndCycleService.GetControlItemAndCycleById(controlItemAndCycleId);
                AspNet.CheckBox ckbWorkPackageCode = (AspNet.CheckBox)(this.Grid1.Rows[i].FindControl("cbSelect"));
                string planCompleteDate = values.Value<string>("PlanCompleteDate");
                if (ckbWorkPackageCode.Checked)
                {
                    string controlItemAndCycleCode = this.Grid1.Rows[i].DataKeys[1].ToString();
                    string txtName = values.Value<string>("AttachUrl");
                    string txtWeights = values.Value<string>("Weights");
                    name = string.Empty;
                    if (!string.IsNullOrEmpty(txtName.Trim()))
                    {
                        name = "-" + txtName.Trim();
                    }
                    Model.WBS_ControlItemProject controlItemProject = BLL.ControlItemProjectService.GetControlItemProjectByCode(controlItemAndCycleCode, this.CurrUser.LoginProjectId);

                    if (oldControlItemAndCycle == null)   //新增内容
                    {
                        Model.WBS_ControlItemAndCycle newControlItemAndCycle = new Model.WBS_ControlItemAndCycle();
                        if (controlItemCode != controlItemProject.ControlItemCode)  //循环至新的工作包
                        {
                            controlItemCode = controlItemProject.ControlItemCode;
                            var oldControlItemAndCycles = BLL.ControlItemAndCycleService.GetControlItemAndCyclesByInitControlItemCodeAndWorkPackageId(controlItemCode, WorkPackageId);
                            if (oldControlItemAndCycles.Count > 0)  //该工作包已存在内容
                            {
                                var old = oldControlItemAndCycles.First();
                                string oldStr = old.ControlItemAndCycleCode.Substring(old.ControlItemAndCycleCode.Length - 2);
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
                        newControlItemAndCycle.ControlItemAndCycleId = SQLHelper.GetNewID(typeof(Model.WBS_WorkPackage));
                        newControlItemAndCycle.ControlItemAndCycleCode = parentWorkPackage.WorkPackageCode + controlItemCode.Substring(controlItemCode.IndexOf(parentWorkPackage.InitWorkPackageCode) + parentWorkPackage.InitWorkPackageCode.Length).Replace("00", "0000") + code;
                        newControlItemAndCycle.ProjectId = this.CurrUser.LoginProjectId;
                        newControlItemAndCycle.WorkPackageId = WorkPackageId;
                        newControlItemAndCycle.ControlItemContent = controlItemProject.ControlItemContent + name;
                        newControlItemAndCycle.ControlPoint = controlItemProject.ControlPoint;
                        newControlItemAndCycle.ControlItemDef = controlItemProject.ControlItemDef;
                        newControlItemAndCycle.HGForms = controlItemProject.HGForms;
                        newControlItemAndCycle.SHForms = controlItemProject.SHForms;
                        newControlItemAndCycle.Standard = controlItemProject.Standard;
                        newControlItemAndCycle.ClauseNo = controlItemProject.ClauseNo;
                        newControlItemAndCycle.IsApprove = true;
                        newControlItemAndCycle.CheckNum = controlItemProject.CheckNum;
                        newControlItemAndCycle.InitControlItemCode = controlItemProject.ControlItemCode;
                        if (!string.IsNullOrEmpty(planCompleteDate))
                        {
                            newControlItemAndCycle.PlanCompleteDate = Convert.ToDateTime(planCompleteDate);
                        }
                        else
                        {
                            newControlItemAndCycle.PlanCompleteDate = null;
                        }
                        try
                        {
                            newControlItemAndCycle.Weights = Convert.ToDecimal(txtWeights.Trim());
                        }
                        catch (Exception)
                        {
                            newControlItemAndCycle.Weights = null;
                        }
                        BLL.ControlItemAndCycleService.AddControlItemAndCycle(newControlItemAndCycle);
                        num++;
                    }
                    else
                    {
                        oldControlItemAndCycle.ControlItemContent = controlItemProject.ControlItemContent + name;
                        try
                        {
                            oldControlItemAndCycle.Weights = Convert.ToDecimal(txtWeights.Trim());
                        }
                        catch (Exception)
                        {
                            oldControlItemAndCycle.Weights = null;
                        }
                        if (!string.IsNullOrEmpty(planCompleteDate))
                        {
                            oldControlItemAndCycle.PlanCompleteDate = Convert.ToDateTime(planCompleteDate);
                        }
                        else
                        {
                            oldControlItemAndCycle.PlanCompleteDate = null;
                        }
                        oldControlItemAndCycle.IsApprove = true;
                        BLL.ControlItemAndCycleService.UpdateControlItemAndCycle(oldControlItemAndCycle);
                    }
                }
                else   //未选中项
                {
                    if (oldControlItemAndCycle != null)   //已存在内容
                    {
                        oldControlItemAndCycle.IsApprove = null;
                        BLL.ControlItemAndCycleService.UpdateControlItemAndCycle(oldControlItemAndCycle);
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
                string name = values.Value<string>("AttachUrl");
                string weights = values.Value<string>("Weights");
                AspNet.CheckBox cb = (AspNet.CheckBox)(this.Grid1.Rows[i].FindControl("cbSelect"));
                var c = controlItemAndCycles.FirstOrDefault(x => x.ControlItemAndCycleId == this.Grid1.Rows[i].RowID);
                if (c != null)
                {
                    c.AttachUrl = name;
                    try
                    {
                        c.Weights = Convert.ToDecimal(weights);
                    }
                    catch (Exception)
                    {
                        c.Weights = null;
                    }
                    if (cb.Checked)
                    {
                        c.IsApprove = true;
                    }
                }
            }
            controlItemAndCycles = controlItemAndCycles.OrderBy(x => x.ControlItemAndCycleCode).ToList();
            this.Grid1.DataSource = controlItemAndCycles;
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