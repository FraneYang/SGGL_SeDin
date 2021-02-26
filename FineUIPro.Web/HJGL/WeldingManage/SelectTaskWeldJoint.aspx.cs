using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace FineUIPro.Web.HJGL.WeldingManage
{
    public partial class SelectTaskWeldJoint : PageBase
    {
        #region 定义项
        /// <summary>
        /// 单位主键
        /// </summary>
        public string UnitId
        {
            get
            {
                return (string)ViewState["UnitId"];
            }
            set
            {
                ViewState["UnitId"] = value;
            }
        }
        /// <summary>
        /// 单位工程主键
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

        // 任务日期
        public string TaskDate
        {
            get
            {
                return (string)ViewState["TaskDate"];
            }
            set
            {
                ViewState["TaskDate"] = value;
            }
        }

        /// <summary>
        /// 日报主键
        /// </summary>
        public string WeldingDailyId
        {
            get
            {
                return (string)ViewState["WeldingDailyId"];
            }
            set
            {
                ViewState["WeldingDailyId"] = value;
            }
        }

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
                this.txtTaskDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(1));

                BLL.Base_WeldingLocationServie.InitWeldingLocationDropDownList(drpWeldingLocation, true);
                ///焊接属性
                this.drpJointAttribute.DataTextField = "Text";
                this.drpJointAttribute.DataValueField = "Value";
                this.drpJointAttribute.DataSource = BLL.DropListService.HJGL_JointAttribute();
                this.drpJointAttribute.DataBind();
                ///机动化程度
                this.drpWeldingMode.DataTextField = "Text";
                this.drpWeldingMode.DataValueField = "Value";
                this.drpWeldingMode.DataSource = BLL.DropListService.HJGL_WeldingMode();
                this.drpWeldingMode.DataBind();
                //this.SelectedList = new List<string>();
                //this.NoSelectedList = new List<string>();

                string strList = Request.Params["strList"];
                List<string> list = Funs.GetStrListByStr(strList, '|');
                if (list.Count() == 3)
                {
                    this.UnitWorkId = list[0];
                    this.UnitId = list[1];
                    Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(this.UnitId);
                    if (unit != null)
                    {
                        this.txtUnitName.Text = unit.UnitName;
                    }
                    TaskDate = list[2];
                    if (!string.IsNullOrEmpty(TaskDate))
                    {
                        txtTaskDate.Text = TaskDate;
                        txtTaskDate.Enabled = false;
                        var task = (from x in Funs.DB.HJGL_WeldTask
                                    where x.UnitWorkId == UnitWorkId
                                    && x.TaskDate.Value.Date.ToString() == Convert.ToDateTime(txtTaskDate.Text.Trim()).ToString("yyyy-MM-dd")
                                    select x).FirstOrDefault();
                        if (task != null)
                        {
                            txtTaskCode.Text = task.TaskCode;
                        }
                        txtTaskCode.Enabled = false;
                    }
                    string projectId = string.Empty;
                    Model.WBS_UnitWork UnitWork = BLL.UnitWorkService.getUnitWorkByUnitWorkId(this.UnitWorkId);
                    if (UnitWorkId != null)
                    {
                        projectId = UnitWork.ProjectId;
                        this.txtUnitWorkName.Text = UnitWork.UnitWorkName;
                    }
                }
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
            TreeNode rootNode = new TreeNode();
            rootNode.Text = "管线号";
            rootNode.NodeID = "0";
            rootNode.Expanded = true;
            this.tvControlItem.Nodes.Add(rootNode);

            var iso = (from x in Funs.DB.HJGL_Pipeline where x.UnitWorkId == this.UnitWorkId && x.UnitId == this.UnitId orderby x.PipelineCode select x).ToList();
            if (!string.IsNullOrEmpty(this.txtPipelineCode.Text))
            {
                iso = (from x in iso where x.PipelineCode.Contains(this.txtPipelineCode.Text.Trim()) orderby x.PipelineCode select x).ToList();
            }
            var joints = from x in Funs.DB.HJGL_WeldJoint where x.ProjectId == this.CurrUser.LoginProjectId select x;
            foreach (var item in iso)
            {
                TreeNode newNode = new TreeNode();
                int totalJointNum = joints.Count(x => x.PipelineId == item.PipelineId);
                int weldJointNum = joints.Count(x => x.PipelineId == item.PipelineId && x.WeldingDailyId != null);
                newNode.Text = item.PipelineCode + "(" + (totalJointNum - weldJointNum).ToString() + ")";
                newNode.NodeID = item.PipelineId;
                newNode.ToolTip = item.PipelineCode;
                newNode.EnableClickEvent = true;
                rootNode.Nodes.Add(newNode);
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            string pipelineId = this.tvControlItem.SelectedNodeID;

            var toDoMatterList = (from x in Funs.DB.View_HJGL_NoWeldJointFind
                                  where x.PipelineId == pipelineId && x.WeldingDailyId == null
                                  select x).ToList();

            //去除任务表已存在的焊口 
            if (!string.IsNullOrEmpty(TaskDate))
            {
                var task = from x in Funs.DB.HJGL_WeldTask where x.UnitWorkId == UnitWorkId && x.TaskDate.Value.Date.ToString() == Convert.ToDateTime(txtTaskDate.Text.Trim()).ToString("yyyy-MM-dd") select x;
                if (task.Count() > 0)
                {
                    foreach (var item in task)
                    {
                        Model.View_HJGL_NoWeldJointFind jot = toDoMatterList.FirstOrDefault(e => e.WeldJointId == item.WeldJointId);
                        if (jot != null)
                        {
                            toDoMatterList.Remove(jot);
                        }
                    }
                }
            }

            DataTable tb = this.LINQToDataTable(toDoMatterList);
            // 2.获取当前分页数据
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();

            //string[] arr = new string[this.Grid1.Rows.Count];
            //int a = 0;
            //for (int i = 0; i < this.Grid1.Rows.Count; i++)
            //{
            //    string rowId = this.Grid1.Rows[i].DataKeys[0].ToString();
            //    if (weldJointIds.Contains(rowId))
            //    {
            //        arr[a] = rowId;
            //    }
            //    a++;
            //}
            //Grid1.SelectedRowIDArray = arr;
        }
        #endregion

        #region 管线查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            this.InitTreeMenu();
            this.BindGrid();
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
        #region 排序
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region 提交按钮
        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAccept_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtTaskCode.Text.Trim()))
            {
                ShowNotify("请输入焊接任务单编号", MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrEmpty(txtTaskDate.Text))
            {
                if (!string.IsNullOrEmpty(TaskDate))
                {
                    SaveTask();
                }
                else
                {
                    var task = from x in Funs.DB.HJGL_WeldTask
                               where x.UnitWorkId == UnitWorkId
                               && x.TaskDate.Value.Date.ToString() == Convert.ToDateTime(txtTaskDate.Text.Trim()).ToString("yyyy-MM-dd")
                               select x;
                    if (task.Count() > 0)
                    {
                        ShowNotify("所选预计焊接日期已存在,请重新选择！", MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        SaveTask();
                    }
                }
            }

            else
            {
                ShowNotify("请选择预计焊接日期", MessageBoxIcon.Warning);
                return;
            }
        }


        private void SaveTask()
        {
            var weldingRods = from x in Funs.DB.Base_Consumables where x.ConsumablesType == "2" select x;
            var weldingWires = from x in Funs.DB.Base_Consumables where x.ConsumablesType == "1" select x;
            string[] selectRowId = Grid1.SelectedRowIDArray;
            for (int i = 0; i < selectRowId.Count(); i++)
            {
                string canWeldingRodName = string.Empty;
                string canWeldingWireName = string.Empty;
                Model.HJGL_WeldTask NewTask = new Model.HJGL_WeldTask();
                NewTask.ProjectId = this.CurrUser.LoginProjectId;
                NewTask.UnitWorkId = this.UnitWorkId;
                NewTask.UnitId = this.UnitId;

                NewTask.TaskCode = this.txtTaskCode.Text.Trim();
                NewTask.WeldTaskId = SQLHelper.GetNewID();
                NewTask.WeldJointId = selectRowId[i];
                Model.HJGL_WeldJoint weldJoint = BLL.WeldJointService.GetWeldJointByWeldJointId(NewTask.WeldJointId);
                if (weldJoint != null)
                {
                    NewTask.WeldingRod = weldJoint.WeldingRod;
                    NewTask.WeldingWire = weldJoint.WeldingWire;
                    //获取可替代焊丝焊条
                    var mat = BLL.Base_MaterialService.GetMaterialByMaterialId(weldJoint.Material1Id);
                    string matClass = mat.MaterialClass;
                    var matRod = weldingRods.FirstOrDefault(x => x.ConsumablesId == weldJoint.WeldingRod);
                    if (matRod != null)
                    {
                        foreach (var item in weldingRods)
                        {
                            if (matClass == "Fe-1" || matClass == "Fe-3")
                            {
                                if (IsCoverClass(matRod.SteelType, item.SteelType))
                                {
                                    canWeldingRodName = canWeldingRodName + item.ConsumablesName + ",";
                                }
                            }
                            else
                            {
                                canWeldingRodName = canWeldingRodName + item.ConsumablesName + ",";
                            }
                        }
                        if (!string.IsNullOrEmpty(canWeldingRodName))
                        {
                            NewTask.CanWeldingRodName = canWeldingRodName.Substring(0, canWeldingRodName.Length - 1);
                        }
                    }
                    var matWire = weldingWires.FirstOrDefault(x => x.ConsumablesId == weldJoint.WeldingWire);
                    if (matWire != null)
                    {
                        foreach (var item in weldingWires)
                        {
                            if (matClass == "Fe-1" || matClass == "Fe-3")
                            {
                                if (IsCoverClass(matWire.SteelType, item.SteelType))
                                {
                                    canWeldingWireName = canWeldingWireName + item.ConsumablesName + ",";
                                }
                            }
                            else
                            {
                                canWeldingWireName = canWeldingWireName + item.ConsumablesName + ",";
                            }
                        }
                        if (!string.IsNullOrEmpty(canWeldingWireName))
                        {
                            NewTask.CanWeldingWireName = canWeldingWireName.Substring(0, canWeldingWireName.Length - 1);
                        }
                    }
                }
                NewTask.JointAttribute = drpJointAttribute.SelectedValue;
                NewTask.WeldingMode = drpWeldingMode.SelectedValue;

                NewTask.TaskDate = Convert.ToDateTime(txtTaskDate.Text);
                NewTask.Tabler = this.CurrUser.UserId;
                NewTask.TableDate = DateTime.Now;
                BLL.WeldTaskService.AddWeldTask(NewTask);
            }
            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(txtTaskDate.Text) + ActiveWindow.GetHidePostBackReference());
            //PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        /// <summary>
        /// 判断耗材强度是否大于WPS耗材强度，如是为true,否则为false
        /// </summary>
        /// <param name="wpsClass"></param>
        /// <param name="matClass"></param>
        /// <returns></returns>
        private bool IsCoverClass(string wpsClass, string matClass)
        {
            bool isCover = false;
            int wpsSn = 0;
            int matSn = 0;
            if (wpsClass.Length > 2 && matClass.Length > 2)
            {
                string wpsPre = wpsClass.Substring(0, wpsClass.Length - 2);
                string matPre = matClass.Substring(0, matClass.Length - 2);

                string wps = wpsClass.Substring(wpsClass.Length - 1, 1);
                wpsSn = Funs.GetNewInt(wps).HasValue ? Funs.GetNewInt(wps).Value : 0;

                string mat = matClass.Substring(matClass.Length - 1, 1);
                matSn = Funs.GetNewInt(mat).HasValue ? Funs.GetNewInt(mat).Value : 0;

                if (wpsPre == matPre && matSn >= wpsSn)
                {
                    return true;
                }
            }
            return isCover;
        }
        #endregion
    }
}