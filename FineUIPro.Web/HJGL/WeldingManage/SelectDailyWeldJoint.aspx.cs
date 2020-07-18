using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BLL;

namespace FineUIPro.Web.HJGL.WeldingManage
{
    public partial class SelectDailyWeldJoint : PageBase
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
        /// <summary>
        /// 焊口主键
        /// </summary>
        public string WeldJointId
        {
            get
            {
                return (string)ViewState["WeldJointId"];
            }
            set
            {
                ViewState["WeldJointId"] = value;
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
                BLL.Base_WeldingLocationServie.InitWeldingLocationDropDownList(drpWeldingLocation, true);
                ///焊接属性
                this.drpJointAttribute.DataTextField = "Text";
                this.drpJointAttribute.DataValueField = "Value";
                this.drpJointAttribute.DataSource = BLL.DropListService.HJGL_JointAttribute();
                this.drpJointAttribute.DataBind();

                this.SelectedList = new List<string>();
                //this.NoSelectedList = new List<string>();

                string strList = Request.Params["strList"];
                List<string> list = Funs.GetStrListByStr(strList, '|');
                if (list.Count() == 3)
                {
                    this.UnitWorkId = list[0];
                    this.UnitId = list[1];
                    this.WeldingDailyId = list[2];
                    string projectId = string.Empty;
                    Model.WBS_UnitWork UnitWork = BLL.UnitWorkService.getUnitWorkByUnitWorkId(this.UnitWorkId);
                    if (UnitWorkId != null)
                    {
                        projectId = UnitWork.ProjectId;
                    }

                    ///盖面焊工
                    BLL.WelderService.InitProjectWelderDropDownList(this.drpCoverWelderId, true, projectId, this.UnitId, "请选择");
                    ///打底焊工
                    BLL.WelderService.InitProjectWelderDropDownList(this.drpBackingWelderId, true, projectId, this.UnitId, "请选择");
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

            var iso = (from x in Funs.DB.HJGL_Pipeline where  x.UnitId == this.UnitId orderby x.PipelineCode select x).ToList();
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

            List<Model.SpWeldingDailyItem> toDoMatterList = BLL.WeldingDailyService.GetWeldReportItemFind(this.UnitWorkId, this.WeldingDailyId, pipelineId);
            string weldJointIds = Request.Params["weldJointIds"];
            if (!string.IsNullOrEmpty(weldJointIds))
            {
                string[] weldJoints = weldJointIds.Split('|');
                foreach (string weldJointId in weldJoints)
                {
                    Model.SpWeldingDailyItem item = toDoMatterList.FirstOrDefault(e => e.WeldJointId == weldJointId);
                    if (item != null)
                    {
                        toDoMatterList.Remove(item);
                    }
                }
            }

            DataTable tb = this.LINQToDataTable(toDoMatterList);
            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(GridNewDynamic, tb1);
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
            string[] selectRowId = Grid1.SelectedRowIDArray;
            for (int i = 0; i < this.Grid1.Rows.Count; i++)
            {
                string rowId = this.Grid1.Rows[i].DataKeys[0].ToString();
                if (selectRowId.Contains(rowId))
                {
                    SelectedList.Add(rowId);
                }
                //else
                //{
                //    NoSelectedList.Add(rowId);
                //}
            }
            this.BindGrid();
        }
        #endregion

        #region DropDownList下拉事件
        /// <summary>
        /// 盖面、打底焊工一致
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpCoverWelderId_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpBackingWelderId.SelectedValue = this.drpCoverWelderId.SelectedValue;
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
            if (this.drpCoverWelderId.SelectedValue != Const._Null 
                && this.drpBackingWelderId.SelectedValue != Const._Null
                && drpJointAttribute.SelectedValue != Const._Null)
            {
                string itemsString = string.Empty;
                string[] selectRowId = Grid1.SelectedRowIDArray;
                for (int i = 0; i < this.Grid1.Rows.Count; i++)
                {
                    string rowId = this.Grid1.Rows[i].DataKeys[0].ToString();
                    if (selectRowId.Contains(rowId))
                    {
                        SelectedList.Add(rowId);
                    }

                }
                string weldJointIds = Request.Params["weldJointIds"];
                if (!string.IsNullOrEmpty(weldJointIds))
                {
                    string[] jots = weldJointIds.Split('|');
                    foreach (string jotId in jots)
                    {
                        SelectedList.Add(jotId);
                    }
                }

                itemsString = this.drpCoverWelderId.SelectedValue + "|" + this.drpBackingWelderId.SelectedValue + "#";
                itemsString += drpWeldingLocation.SelectedValue + "#";
                itemsString += drpJointAttribute.SelectedValue + "#";
                
                foreach (var item in SelectedList)
                {
                    if (!itemsString.Contains(item))
                    {
                        itemsString += item + "|";
                    }
                }
                PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(itemsString)
                        + ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                ShowNotify("请选择焊工", MessageBoxIcon.Warning);
                return;
            }
        }
        #endregion
    }
}