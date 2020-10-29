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
                    TaskDate = list[2];
                    if (!string.IsNullOrEmpty(TaskDate))
                    {
                        txtTaskDate.Text = TaskDate;
                        txtTaskDate.Enabled = false;
                    }
                    string projectId = string.Empty;
                    Model.WBS_UnitWork UnitWork = BLL.UnitWorkService.getUnitWorkByUnitWorkId(this.UnitWorkId);
                    if (UnitWorkId != null)
                    {
                        projectId = UnitWork.ProjectId;
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

            var iso = (from x in Funs.DB.HJGL_Pipeline where x.UnitId == this.UnitId orderby x.PipelineCode select x).ToList();
            if (!string.IsNullOrEmpty(this.txtPipelineCode.Text))
            {
                iso = (from x in iso where x.PipelineCode.Contains(this.txtPipelineCode.Text.Trim()) orderby x.PipelineCode select x).ToList();
            }

            foreach (var item in iso)
            {
                TreeNode newNode = new TreeNode();
                newNode.Text = item.PipelineCode;
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
            string[] selectRowId = Grid1.SelectedRowIDArray;
            for (int i = 0; i < selectRowId.Count(); i++)
            {
                Model.HJGL_WeldTask NewTask = new Model.HJGL_WeldTask();
                NewTask.ProjectId = this.CurrUser.LoginProjectId;
                NewTask.UnitWorkId = this.UnitWorkId;
                NewTask.UnitId = this.UnitId;


                NewTask.WeldTaskId = SQLHelper.GetNewID();
                NewTask.WeldJointId = selectRowId[i];

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
        #endregion
    }
}