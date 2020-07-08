using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.Check
{
    public partial class ShowCheckItem : PageBase
    {
        #region 定义集合
        /// <summary>
        /// 定义集合
        /// </summary>
        private static List<string> list = new List<string>();
        private static List<string> parentIds = new List<string>();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                list = new List<string>();
                parentIds = new List<string>();
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                CheckItemSetDataBind();//加载树
            }
        }

        private void BindGrid()
        {
            string ids = string.Empty;

            this.Grid1.DataSource = null;
            this.Grid1.DataBind();
            string strSql = @"SELECT CheckItemDetailId,CheckItemSetId,CheckContent,SortIndex,IsBuiltIn "
                        + @" FROM Technique_CheckItemDetail "
                        + @" WHERE CheckItemSetId=@CheckItemSetId";
            List<SqlParameter> listStr = new List<SqlParameter>
            {
                new SqlParameter("@CheckItemSetId", this.tvCheckItemSet.SelectedNodeID)
            };

            if (!string.IsNullOrEmpty(Request.Params["CheckColligationId"]))
            {
                List<Model.View_Check_CheckColligationDetail> details = (from x in new Model.SGGLDB(Funs.ConnString).View_Check_CheckColligationDetail where x.CheckColligationId == Request.Params["CheckColligationId"] select x).ToList();
                if (details.Count() > 0)
                {
                    for (int i = 0; i < details.Count(); i++)
                    {
                        if (i == 0)
                        {
                            strSql += " AND CheckItemDetailId not in (@Ids" + i;
                        }
                        else
                        {
                            strSql += ",@Ids" + i;
                        }
                        listStr.Add(new SqlParameter("@Ids" + i, details[i].CheckItem));
                    }
                    strSql += ")";
                }
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
            if (list.Count() > 0)
            {
                for (int i = 0; i < Grid1.Rows.Count; i++)
                {
                    string id = Grid1.DataKeys[i][0].ToString();
                    if (list.Contains(id))
                    {
                        Grid1.Rows[i].Values[0] = true;
                    }
                }
            }
        }

        #region 绑定树节点
        /// <summary>
        /// 绑定树节点
        /// </summary>
        private void CheckItemSetDataBind()
        {
            this.tvCheckItemSet.Nodes.Clear();
            this.tvCheckItemSet.SelectedNodeID = string.Empty;
            var checks = (from x in new Model.SGGLDB(Funs.ConnString).Technique_CheckItemSet
                         where x.CheckType == Request.Params["checkType"]                  
                         select x).ToList();
            if (checks.Count() > 0)
            {
                var supChecks = checks.Where(x => x.SupCheckItem == "0").OrderBy(x=>x.SortIndex).ToList();
                if (supChecks.Count() > 0)
                {
                    foreach (var item in supChecks)
                    {
                        TreeNode rootNode = new TreeNode
                        {
                            Text = item.CheckItemName,
                            NodeID = item.CheckItemSetId,
                            EnableClickEvent = true,
                        };//定义根节点

                        this.tvCheckItemSet.Nodes.Add(rootNode);
                        this.GetNodes(rootNode.Nodes, checks.Where(x=>x.SupCheckItem== item.CheckItemSetId).ToList());
                    }
                }
            }
        }

        #region  遍历节点方法
        /// <summary>
        /// 遍历节点方法
        /// </summary>
        /// <param name="nodes">节点集合</param>
        /// <param name="parentId">父节点</param>
        private void GetNodes(TreeNodeCollection nodes, List<Model.Technique_CheckItemSet> checkItemSetLists)
        {            
            foreach (var q in checkItemSetLists)
            {
                var isEnd = BLL.Technique_CheckItemSetService.IsEndLevel(q.CheckItemSetId);
                TreeNode newNode = new TreeNode
                {
                    Text = q.CheckItemName,
                    NodeID = q.CheckItemSetId,
                    EnableClickEvent = true,
                };
                nodes.Add(newNode);
                if (!isEnd)
                {
                    GetNodes(newNode.Nodes, checkItemSetLists.Where(x => x.SupCheckItem == q.CheckItemSetId).ToList());
                }
            }
        }
        #endregion
        #endregion

        #region 点击TreeView
        /// <summary>
        /// 点击TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvCheckItemSet_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            if (this.tvCheckItemSet.SelectedNodeID != "0" && this.tvCheckItemSet.SelectedNode != null)
            {
                this.Grid1.DataSource = null;
                this.Grid1.DataBind();
                BindGrid();
            }
        }
        #endregion

        #region 页索引改变事件
        /// <summary>
        /// 页索引改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
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
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }
        #endregion

        #region 分页选择下拉改变事件
        /// <summary>
        /// 分页选择下拉改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }
        #endregion

        #region 确认按钮
        /// <summary>
        /// 确认按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<string> newParentIds = new List<string>();
            TreeNode[] nodes = this.tvCheckItemSet.GetCheckedNodes();
            foreach (var item in nodes)
            {
                if (item.Nodes.Count == 0)
                {
                    parentIds.Add(item.NodeID);   //集合中增加末级节点
                    if (item.ParentNode != null)
                    {
                        newParentIds.Add(item.ParentNode.NodeID);    //记录已增加的末级节点的父级节点集合
                    }
                }
            }
            foreach (var item in nodes)
            {
                if (item.Nodes.Count > 0)
                {
                    if (!newParentIds.Contains(item.NodeID))
                    {
                        parentIds.Add(item.NodeID);
                    }
                }
            }
            if (parentIds.Count == 0 && list.Count == 0)
            {
                ShowNotify("请至少选择一项！", MessageBoxIcon.Warning);
                return;
            }
            //if (!string.IsNullOrEmpty(Request.Params["CheckDayId"]))
            //{
            //    foreach (var item in parentIds)
            //    {
            //        Model.Technique_CheckItemDetail detail = new Model.Technique_CheckItemDetail
            //        {
            //            CheckDayDetailId = SQLHelper.GetNewID(typeof(Model.Technique_CheckItemDetail)),
            //            CheckDayId = Request.Params["CheckDayId"],
            //            CheckItem = item
            //        };
            //        var checkItemDetail = BLL.Technique_CheckItemDetailService.GetCheckItemDetailById(item);
            //        if (checkItemDetail != null)
            //        {
            //            detail.CheckContent = checkItemDetail.CheckContent;
            //        }
            //        else
            //        {
            //            var projectCheckItemDetail = BLL.Technique_CheckItemDetailService.GetCheckItemDetailById(item);
            //            if (projectCheckItemDetail != null)
            //            {
            //                detail.CheckContent = projectCheckItemDetail.CheckContent;
            //            }
            //        }
            //        detail.Unqualified = "隐患";
            //        detail.Suggestions = "整改";
            //        detail.CompleteStatus = true;
            //        BLL.Check_CheckDayDetailService.AddCheckDayDetail(detail);
            //    }
            //}
            //else if (!string.IsNullOrEmpty(Request.Params["CheckSpecialId"]))
            //{
            //    foreach (var item in parentIds)
            //    {
            //        Model.Check_CheckSpecialDetail detail = new Model.Check_CheckSpecialDetail
            //        {
            //            CheckSpecialDetailId = SQLHelper.GetNewID(typeof(Model.Check_CheckSpecialDetail)),
            //            CheckSpecialId = Request.Params["CheckSpecialId"],
            //            CheckItem = item
            //        };
            //        var checkItemDetail = BLL.Technique_CheckItemDetailService.GetCheckItemDetailById(item);
            //        if (checkItemDetail != null)
            //        {
            //            detail.CheckContent = checkItemDetail.CheckContent;
            //        }
            //        else
            //        {
            //            var projectCheckItemDetail = BLL.Technique_CheckItemDetailService.GetCheckItemDetailById(item);
            //            if (projectCheckItemDetail != null)
            //            {
            //                detail.CheckContent = projectCheckItemDetail.CheckContent;
            //            }
            //        }
            //        detail.Unqualified = "隐患";
            //        detail.Suggestions = "整改";
            //        detail.CompleteStatus = true;
            //        BLL.Check_CheckSpecialDetailService.AddCheckSpecialDetail(detail);
            //    }
            //}
            if (!string.IsNullOrEmpty(Request.Params["CheckColligationId"]))
            {
                foreach (var item in parentIds)
                {
                    Model.Check_CheckColligationDetail detail = new Model.Check_CheckColligationDetail
                    {
                        CheckColligationDetailId = SQLHelper.GetNewID(typeof(Model.Check_CheckColligationDetail)),
                        CheckColligationId = Request.Params["CheckColligationId"],
                        CheckItem = item
                    };
                    var checkItemDetail = BLL.Technique_CheckItemDetailService.GetCheckItemDetailById(item);
                    if (checkItemDetail != null)
                    {
                        detail.CheckContent = checkItemDetail.CheckContent;
                    }
                    else
                    {
                        var projectCheckItemDetail = BLL.Technique_CheckItemDetailService.GetCheckItemDetailById(item);
                        if (projectCheckItemDetail != null)
                        {
                            detail.CheckContent = projectCheckItemDetail.CheckContent;
                        }
                    }
                    detail.Unqualified = "隐患";
                    detail.Suggestions = "整改";
                    detail.CompleteStatus = true;
                    BLL.Check_CheckColligationDetailService.AddCheckColligationDetail(detail);
                }
            }
            if (!string.IsNullOrEmpty(Request.Params["CheckColligationId"]))
            {
                foreach (var item in list)
                {
                    Model.Check_CheckColligationDetail detail = new Model.Check_CheckColligationDetail
                    {
                        CheckColligationDetailId = SQLHelper.GetNewID(typeof(Model.Check_CheckColligationDetail)),
                        CheckColligationId = Request.Params["CheckColligationId"],
                        CheckItem = item
                    };
                    var checkItemSet = BLL.Technique_CheckItemSetService .GetCheckItemSetById(item);
                    if (checkItemSet != null)
                    {
                        detail.CheckContent = checkItemSet.CheckItemName;
                    }
                    else
                    {
                        var projectCheckItemDetail = BLL.Technique_CheckItemDetailService.GetCheckItemDetailById(item);
                        if (projectCheckItemDetail != null)
                        {
                            detail.CheckContent = projectCheckItemDetail.CheckContent;
                        }
                    }
                    detail.Unqualified = "隐患";
                    detail.Suggestions = "整改";
                    detail.CompleteStatus = true;
                    BLL.Check_CheckColligationDetailService.AddCheckColligationDetail(detail);
                }
            }

            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        #region Grid行点击事件
        /// <summary>
        /// Grid1行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string rowID = Grid1.DataKeys[e.RowIndex][0].ToString();
            //string parentId = Grid1.DataKeys[e.RowIndex][1].ToString();
            if (e.CommandName == "IsSelected")
            {
                CheckBoxField checkField = (CheckBoxField)Grid1.FindColumn("ckbIsSelected");
                if (checkField.GetCheckedState(e.RowIndex))
                {
                    if (!list.Contains(rowID))
                    {
                        list.Add(rowID);
                    }
                    //if (!parentIds.Contains(parentId + "," + rowID))
                    //{
                    //    parentIds.Add(parentId + "," + rowID);
                    //}
                }
                else
                {
                    if (list.Contains(rowID))
                    {
                        list.Remove(rowID);
                    }
                    //if (parentIds.Contains(parentId + "," + rowID))
                    //{
                    //    parentIds.Remove(parentId + "," + rowID);
                    //}
                }
            }
        }
        #endregion
    }
}