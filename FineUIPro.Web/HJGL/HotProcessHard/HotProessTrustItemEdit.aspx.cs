using System;
using System.Collections.Generic;
using System.Linq;
using BLL;
using System.Data;

namespace FineUIPro.Web.HJGL.HotProcessHard
{
    public partial class HotProessTrustItemEdit : PageBase
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
        /// <summary>
        /// 选择内容字符串
        /// </summary>
        public string ItemsString
        {
            get
            {
                return (string)ViewState["ItemsString"];
            }
            set
            {
                ViewState["ItemsString"] = value;
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
                ItemsString = string.Empty;
                string strList = Request.Params["strList"];
                List<string> list = Funs.GetStrListByStr(strList, '|');
                if (list.Count() == 2)
                {
                    this.UnitId = list[0];
                    this.HotProessTrustId = list[1];
                    this.InitTreeMenu();//加载树
                }
            }
        }
        #endregion

        #region 加载管线信息
        /// <summary>
        /// 加载树
        /// </summary>
        private void InitTreeMenu()
        {
            this.tvControlItem.Nodes.Clear();
            TreeNode rootNode = new TreeNode();
            rootNode.Text = "管线号";
            rootNode.NodeID = "0";
            rootNode.ToolTip = "绿色表示管线下有已焊接的焊口未委托热处理";
            rootNode.Expanded = true;
            this.tvControlItem.Nodes.Add(rootNode);
           
            var hotProessItems = from x in Funs.DB.HJGL_HotProess_TrustItem select x;    //热处理委托明细集合
            var iso = from x in Funs.DB.HJGL_Pipeline where x.ProjectId == this.CurrUser.LoginProjectId && x.UnitId == this.UnitId select x;
            if (!string.IsNullOrEmpty(this.txtIsono.Text))
            {
                iso = iso.Where(e => e.PipelineCode.Contains(this.txtIsono.Text.Trim()));
            }

            iso = iso.OrderBy(x => x.PipelineCode);
            if (iso.Count() > 0)
            {
                foreach (var q in iso)
                {
                    var jots = from x in Funs.DB.HJGL_WeldJoint
                               where x.PipelineId == q.PipelineId && x.IsHotProess == true
                               select x;
                    var hotItem = from x in Funs.DB.HJGL_HotProess_TrustItem
                                  join y in Funs.DB.HJGL_WeldJoint on x.WeldJointId equals y.WeldJointId
                                  where y.PipelineId == q.PipelineId
                                  select x;
                    if (jots.Count() > hotItem.Count())
                    {
                        TreeNode newNode = new TreeNode();
                        newNode.NodeID = q.PipelineId;
                        newNode.Text = q.PipelineCode;
                        newNode.EnableClickEvent = true;
                        rootNode.Nodes.Add(newNode);
                    }
                }
            }
        }
        #endregion

        #region 管线查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Tree_TextChanged(object sender, EventArgs e)
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

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            //List<Model.View_HotProessTrustItemSearch> toDoMatterList = BLL.HotProess_TrustService.GetHotProessTrustFind(this.ProjectId, this.HotProessTrustId, this.tvControlItem.SelectedNodeID);

            List<Model.View_HJGL_HotProessTrustItemSearch> toDoMatterList = (from x in Funs.DB.View_HJGL_HotProessTrustItemSearch
                                                                             where x.ProjectId == this.CurrUser.LoginProjectId && x.HotProessTrustItemId == null
                                                                                && x.IsHotProess==true
                                                                        orderby x.WeldJointCode
                                                                        select x).ToList();
            if (rblIsWeld.SelectedValue == "0")
            {
                toDoMatterList = toDoMatterList.Where(x => x.WeldingDailyId == null).ToList();
            }
            if (rblIsWeld.SelectedValue == "1")
            {
                toDoMatterList = toDoMatterList.Where(x => x.WeldingDailyId != null).ToList();
            }
            if (this.tvControlItem.SelectedNodeID != null)
            {
                toDoMatterList = toDoMatterList.Where(x => x.PipelineId == this.tvControlItem.SelectedNodeID).ToList();
            }
            if (!string.IsNullOrEmpty(this.txtJointNo.Text.Trim()))
            {
                toDoMatterList = toDoMatterList.Where(x => x.WeldJointCode.Contains(this.txtJointNo.Text.Trim())).ToList();
            }
            //string weldJointIds = Request.Params["weldJointIds"];
            //if (!string.IsNullOrEmpty(weldJointIds))
            //{
            //    string[] jots = weldJointIds.Split('|');
            //    foreach (string jotId in jots)
            //    {
            //        Model.View_HotProessTrustItemSearch item = toDoMatterList.FirstOrDefault(e => e.WeldJointId == jotId);
            //        if (item != null)
            //        {
            //            toDoMatterList.Remove(item);
            //        }
            //    }
            //}
            //DataTable tb = this.LINQToDataTable(toDoMatterList);

            //Grid1.RecordCount = tb.Rows.Count;
            //tb = GetFilteredTable(Grid1.FilteredData, tb);

            
            //var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = toDoMatterList;
            Grid1.DataBind();
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

        #region 筛选焊口信息
        /// <summary>
        /// 筛选焊口信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblIsWeld_SelectedIndexChanged(object sender, EventArgs e)
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
            string itemsString = "";
            string[] selectRowId = Grid1.SelectedRowIDArray;
            int n = 0;
            int j = 0;
            int[] selections = new int[selectRowId.Count()];
            foreach (GridRow row in Grid1.Rows)
            {
                if (selectRowId.Contains(row.DataKeys[0]))
                {
                    selections[n] = j;
                    n++;
                }
                j++;
            }
            var select = selections.Distinct();
            string jotIds = Request.Params["weldJointIds"];
            if (!string.IsNullOrEmpty(jotIds))
            {
                string[] jots = jotIds.Split('|');
                foreach (string jotId in jots)
                {
                    itemsString += jotId + "|";
                }
            }
            foreach (int i in select)
            {
                string rowID = Grid1.DataKeys[i][0].ToString();
                if (!itemsString.Contains(rowID))
                {
                    itemsString += rowID + "|";
                }
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(itemsString)
                    + ActiveWindow.GetHidePostBackReference());
        }
        #endregion
    }
}