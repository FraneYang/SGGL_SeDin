using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BLL;

namespace FineUIPro.Web.HJGL.HotProcessHard
{
    public partial class HardTrustItemEdit : PageBase
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
        /// 装置主键
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
                this.SelectedList = new List<string>();
                //this.NoSelectedList = new List<string>();

                string strList = Request.Params["strList"];
                List<string> list = Funs.GetStrListByStr(strList, '|');
                if (list.Count() == 3)
                {
                    this.UnitWorkId = list[0];
                    this.UnitId = list[1];
                    this.HardTrustID = list[2];
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
            ///根据已经热处理且需要硬度检测且未进行硬度检测的焊口获取管线id集合
            var pipelineIds = (from x in new Model.SGGLDB(Funs.ConnString).HJGL_HotProess_TrustItem
                               join y in new Model.SGGLDB(Funs.ConnString).HJGL_WeldJoint
                               on x.WeldJointId equals y.WeldJointId
                               join z in new Model.SGGLDB(Funs.ConnString).HJGL_Pipeline
                               on y.PipelineId equals z.PipelineId
                               where z.UnitWorkId == this.UnitWorkId &&
                               z.UnitId == this.UnitId && x.IsHardness == true && x.IsTrust == null
                               select y.PipelineId).Distinct().ToList();

            var pipelines = (from x in new Model.SGGLDB(Funs.ConnString).HJGL_Pipeline
                             where x.UnitWorkId == this.UnitWorkId && x.UnitId == this.UnitId
                             orderby x.PipelineCode
                             select x).ToList();
            pipelines = pipelines.Where(e => pipelineIds.Contains(e.PipelineId)).ToList();
            if (!string.IsNullOrEmpty(this.txtPipelineCode.Text))
            {
                pipelines = (from x in pipelines where x.PipelineCode.Contains(this.txtPipelineCode.Text.Trim()) orderby x.PipelineCode select x).ToList();
            }

            foreach (var item in pipelines)
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

            List<Model.View_HJGL_Hard_TrustItem> toDoMatterList = BLL.Hard_TrustService.GetHardTrustFind(this.UnitWorkId, this.HardTrustID, pipelineId);
            string weldJointIds = Request.Params["weldJointIds"];
            if (!string.IsNullOrEmpty(weldJointIds))
            {
                string[] weldJoints = weldJointIds.Split('|');
                foreach (string weldJointId in weldJoints)
                {
                    Model.View_HJGL_Hard_TrustItem item = toDoMatterList.FirstOrDefault(e => e.WeldJointId == weldJointId);
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
                string id = this.Grid1.Rows[i].DataKeys[0].ToString() + "," + this.Grid1.Rows[i].DataKeys[1].ToString();
                if (selectRowId.Contains(rowId))
                {
                    SelectedList.Add(id);
                }
            }
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
            string itemsString = string.Empty;
            string[] selectRowId = Grid1.SelectedRowIDArray;
            for (int i = 0; i < this.Grid1.Rows.Count; i++)
            {
                string rowId = this.Grid1.Rows[i].DataKeys[0].ToString();
                string id = this.Grid1.Rows[i].DataKeys[0].ToString() + "," + this.Grid1.Rows[i].DataKeys[1].ToString();
                if (selectRowId.Contains(rowId))
                {
                    SelectedList.Add(id);
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
            foreach (var item in SelectedList)
            {
                if (!itemsString.Contains(item))
                {
                    itemsString += item + "|";
                }
            }
            if (!string.IsNullOrEmpty(itemsString))
            {
                itemsString = itemsString.Substring(0, itemsString.LastIndexOf("|"));
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(itemsString)
                    + ActiveWindow.GetHidePostBackReference());
        }
        #endregion
    }
}