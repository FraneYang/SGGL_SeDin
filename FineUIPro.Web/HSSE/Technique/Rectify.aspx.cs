using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace FineUIPro.Web.HSSE.Technique
{
    public partial class Rectify : PageBase
    {
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
                Funs.DropDownPageSize(this.ddlPageSize);
                btnSelectColumns.OnClientClick = Window5.GetShowReference("RectifySelectCloumn.aspx");
                ////权限按钮方法
                this.GetButtonPower();
                InitTreeMenu();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT RectifyItem.RectifyItemId,RectifyItem.RectifyId,RectifyItem.HazardSourcePoint,(CASE WHEN LEN(RectifyItem.HazardSourcePoint) > 30 THEN LEFT(RectifyItem.HazardSourcePoint,30) + '...' ELSE RectifyItem.HazardSourcePoint END) AS ShortHazardSourcePoint, "
                    + @" RectifyItem.RiskAnalysis,(CASE WHEN LEN(RectifyItem.RiskPrevention) > 30 THEN LEFT(RectifyItem.RiskPrevention,30) + '...' ELSE RectifyItem.RiskPrevention END) AS ShortRiskPrevention,RectifyItem.RiskPrevention, "
                    + @" RectifyItem.SimilarRisk,(CASE WHEN LEN(RectifyItem.SimilarRisk) > 30 THEN LEFT(RectifyItem.SimilarRisk,30) + '...' ELSE RectifyItem.SimilarRisk END) AS ShortSimilarRisk,R.RectifyName,R.RectifyCode,RectifyItem.CompileMan,RectifyItem.CompileDate,"
                    + @" RectifyItem.AuditMan,RectifyItem.AuditDate,RectifyItem.IsPass,U.UserName AS CompileManName,UR.UserName AS AuditManName"
                    + @" FROM dbo.Technique_RectifyItem AS RectifyItem"
                    + @" LEFT JOIN dbo.Technique_Rectify AS R ON R.RectifyId=RectifyItem.RectifyId"
                    + @" LEFT JOIN dbo.Sys_User AS U ON U.UserId=RectifyItem.CompileMan"
                    + @" LEFT JOIN dbo.Sys_User AS UR ON U.UserId=RectifyItem.AuditMan"
                    + @" WHERE RectifyItem.RectifyId=@RectifyId";
            List<SqlParameter> listStr = new List<SqlParameter>
            {
                new SqlParameter("@RectifyId", this.trRectify.SelectedNode.NodeID)
            };
            if (!string.IsNullOrEmpty(this.RectifyName.Text.Trim()))
            {
                strSql += " AND RectifyName LIKE @RectifyName";
                listStr.Add(new SqlParameter("@RectifyName", "%" + this.RectifyName.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.HazardSourcePoint.Text.Trim()))
            {
                strSql += " AND HazardSourcePoint LIKE @HazardSourcePoint";
                listStr.Add(new SqlParameter("@HazardSourcePoint", "%" + this.HazardSourcePoint.Text.Trim() + "%"));
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region 加载树
        /// <summary>
        /// 加载树
        /// </summary>
        private void InitTreeMenu()
        {
            this.trRectify.Nodes.Clear();
            this.trRectify.ShowBorder = false;
            this.trRectify.ShowHeader = false;
            this.trRectify.EnableIcons = true;
            this.trRectify.AutoScroll = true;
            this.trRectify.EnableSingleClickExpand = true;
            TreeNode rootNode = new TreeNode
            {
                Text = "隐患类型",
                NodeID = "0",
                Expanded = true
            };
            this.trRectify.Nodes.Add(rootNode);
            BoundTree(rootNode.Nodes, "0");
        }

        /// <summary>
        /// 加载树
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="menuId"></param>
        private void BoundTree(TreeNodeCollection nodes, string menuId)
        {
            var dt = BLL.RectifyService.GetRectifyBySupRectifyId(menuId);
            if (dt.Count() > 0)
            {
                TreeNode tn = null;
                foreach (var dr in dt)
                {
                    tn = new TreeNode
                    {
                        Text = dr.RectifyName,
                        ToolTip = dr.RectifyName,
                        NodeID = dr.RectifyId,
                        EnableClickEvent = true
                    };
                    nodes.Add(tn);
                    BoundTree(tn.Nodes, dr.RectifyId);
                }
            }
        }
        #endregion

        #region 添加按钮
        /// <summary>
        /// 添加安全隐患类型按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {

            if (this.trRectify.SelectedNode != null)
            {
                Model.Technique_Rectify rectify = BLL.RectifyService.GetRectifyById(this.trRectify.SelectedNode.NodeID);
                if ((rectify != null && rectify.IsEndLever == false) || this.trRectify.SelectedNode.NodeID == "0")   //根节点或者非末级节点，可以增加
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("RectifyEdit.aspx?SupRectifyId={0}", this.trRectify.SelectedNode.NodeID, "编辑 - ")));
                }
                else
                {
                    ShowNotify("选择的项已是末级！", MessageBoxIcon.Success);
                }
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改安全隐患类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.trRectify.SelectedNode != null)
            {
                if (this.trRectify.SelectedNode.NodeID != "0")   //非根节点可以编辑
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("RectifyEdit.aspx?RectifyId={0}", this.trRectify.SelectedNode.NodeID, "编辑 - ")));
                }
                else
                {
                    ShowNotify("根节点无法编辑！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除安全试题类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.trRectify.SelectedNode != null)
            {
                var q = BLL.RectifyService.GetRectifyById(this.trRectify.SelectedNode.NodeID);

                if (q != null && BLL.RectifyService.IsDeleteRectify(this.trRectify.SelectedNode.NodeID))
                {
                    BLL.LogService.AddSys_Log(this.CurrUser, q.RectifyName, q.RectifyId, BLL.Const.RectifyMenuId, Const.BtnDelete);
                    BLL.RectifyItemService.DeleteRectifyItemByRectifyId(this.trRectify.SelectedNode.NodeID);
                    BLL.RectifyService.DeleteRectify(this.trRectify.SelectedNode.NodeID);
                    InitTreeMenu();
                }
                else
                {
                    ShowNotify("存在下级菜单或已增加资源数据或者为内置项，不允许删除！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("请选择删除项！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Tree点击事件
        /// <summary>
        /// tree点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void trRectify_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 文本框查询事件
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
            BLL.LogService.AddSys_Log(this.CurrUser, string.Empty, string.Empty, BLL.Const.RectifyMenuId, Const.BtnQuery);
        }
        #endregion

        #region 增加明细
        /// <summary>
        /// 增加安全隐患明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewDetail_Click(object sender, EventArgs e)
        {
            if (this.trRectify.SelectedNode != null)
            {
                if (this.trRectify.SelectedNode.Nodes.Count == 0)
                {
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("RectifyItemEdit.aspx?RectifyId={0}", this.trRectify.SelectedNode.NodeID, "编辑 - ")));
                }
                else
                {
                    ShowNotify("请选择末级节点！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 编辑明细
        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 编辑数据方法
        /// </summary>
        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }

            string rectifyItemId = Grid1.SelectedRowID;

            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("RectifyItemEdit.aspx?RectifyItemId={0}", rectifyItemId, "编辑 - ")));
        }
        #endregion

        #region 删除明细
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            this.DeleteData();
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        private void DeleteData()
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    if (judgementDelete(rowID, true))
                    {
                        var rectifyItem = BLL.RectifyItemService.GetRectifyItemById(rowID);
                        if (rectifyItem != null)
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, rectifyItem.HazardSourcePoint, rectifyItem.RectifyItemId, BLL.Const.RectifyMenuId, Const.BtnDelete);
                            BLL.RectifyItemService.DeleteRectifyItem(rowID);
                            BindGrid();
                            ShowNotify("删除数据成功!");
                        }
                    }
                }
            }
        }
        #endregion

        #region 表头分页、排序
        /// <summary>
        /// Grid分页索引改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
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
        #endregion

        #region Grid行双击事件
        /// <summary>
        /// Grid1行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }
        #endregion

        #region 关闭弹出框
        /// <summary>
        /// 关闭弹出窗口1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            InitTreeMenu();
        }

        /// <summary>
        /// 关闭弹出窗口2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion
        
        #region 导出
        /// <summary>
        /// 关闭导出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window5_Close(object sender, WindowCloseEventArgs e)
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=MyExcelFile.xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(GetGridTableHtml(Grid1, e.CloseArgument.Split('#')));
            Response.End();
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid, string[] columns)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<meta http-equiv=\"content-type\" content=\"application/excel; charset=UTF-8\"/>");
            List<string> columnHeaderTexts = new List<string>(columns);
            List<int> columnIndexs = new List<int>();
            sb.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");
            sb.Append("<tr>");
            foreach (GridColumn column in grid.Columns)
            {
                if (columnHeaderTexts.Contains(column.HeaderText))
                {
                    sb.AppendFormat("<td>{0}</td>", column.HeaderText);
                    columnIndexs.Add(column.ColumnIndex);
                }
            }
            sb.Append("</tr>");
            foreach (GridRow row in grid.Rows)
            {
                sb.Append("<tr>");
                int columnIndex = 0;
                foreach (object value in row.Values)
                {
                    if (columnIndexs.Contains(columnIndex))
                    {
                        string html = value.ToString();
                        if (html.StartsWith(Grid.TEMPLATE_PLACEHOLDER_PREFIX))
                        {
                            // 模板列                            
                            string templateID = html.Substring(Grid.TEMPLATE_PLACEHOLDER_PREFIX.Length);
                            Control templateCtrl = row.FindControl(templateID);
                            html = GetRenderedHtmlSource(templateCtrl);
                        }
                        //else
                        //{
                        //    // 处理CheckBox             
                        //    if (html.Contains("f-grid-static-checkbox"))
                        //    {
                        //        if (!html.Contains("f-checked"))
                        //        {
                        //            html = "×";
                        //        }
                        //        else
                        //        {
                        //            html = "√";
                        //        }
                        //    }
                        //    // 处理图片                           
                        //    if (html.Contains("<img"))
                        //    {
                        //        string prefix = Request.Url.AbsoluteUri.Replace(Request.Url.AbsolutePath, ""); 
                        //        html = html.Replace("src=\"", "src=\"" + prefix);
                        //    }
                        //}
                        sb.AppendFormat("<td>{0}</td>", html);
                    }
                    columnIndex++;
                }
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            return sb.ToString();
        }

        /// <summary>        
        /// 获取控件渲染后的HTML源代码        
        /// </summary>        
        /// <param name="ctrl"></param>        
        /// <returns></returns>        
        private string GetRenderedHtmlSource(Control ctrl)
        {
            if (ctrl != null)
            {
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                    {
                        ctrl.RenderControl(htw);
                        return sw.ToString();
                    }
                }
            }
            return String.Empty;
        }
        #endregion

        #region 判断是否可删除
        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private bool judgementDelete(string id, bool isShow)
        {
            string content = string.Empty;
            //if (Funs.DB.Supervise_SuperviseCheckRectifyItem.FirstOrDefault(x => x.RectifyItemId == id) != null)
            //{
            //    content = "该安全隐患已在【检查整改】中使用，不能删除！";
            //}
            if (string.IsNullOrEmpty(content))
            {
                return true;
            }
            else
            {
                if (isShow)
                {
                    Alert.ShowInTop(content);
                }
                return false;
            }
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.RectifyMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                    this.btnNewDetail.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnEdit.Hidden = false;
                    this.btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnDelete.Hidden = false;
                    this.btnMenuDelete.Hidden = false;
                }                
                if (buttonList.Contains(BLL.Const.BtnOut))
                {
                    this.btnSelectColumns.Hidden = false;
                }
            }
        }
        #endregion
    }
}