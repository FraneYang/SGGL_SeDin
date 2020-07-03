using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.Technique
{
    public partial class HazardList : PageBase
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
                ////权限按钮方法
                this.GetButtonPower();             
                InitTreeMenu();
            }
        }
        #endregion

        #region 加载树
        /// <summary>
        /// 初始化树
        /// </summary>
        private void InitTreeMenu()
        {
            trHazardListType.Nodes.Clear();
            trHazardListType.ShowBorder = false;
            trHazardListType.ShowHeader = false;
            trHazardListType.EnableIcons = true;
            trHazardListType.AutoScroll = true;
            trHazardListType.EnableSingleClickExpand = true;
            TreeNode rootNode = new TreeNode
            {
                Text = "危险源辨识与评价清单",
                NodeID = "0",
                Expanded = true
            };
            this.trHazardListType.Nodes.Add(rootNode);
            BoundTree(rootNode.Nodes, "0");
        }

        private void BoundTree(TreeNodeCollection nodes, string menuId)
        {
            var dt = GetNewHazardListType(menuId);
            if (dt.Count() > 0)
            {
                TreeNode tn = null;
                foreach (var dr in dt)
                {
                    tn = new TreeNode
                    {
                        Text = dr.HazardListTypeName,
                        ToolTip = dr.HazardListTypeName,
                        NodeID = dr.HazardListTypeId,
                        EnableClickEvent = true
                    };
                    nodes.Add(tn);
                    BoundTree(tn.Nodes, dr.HazardListTypeId);
                }
            }
        }
        #endregion

        #region 得到菜单方法
        /// <summary>
        /// 得到菜单方法
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private List<Model.Technique_HazardListType> GetNewHazardListType(string parentId)
        {
            string hazardListTypeName = string.Empty;
            if (!string.IsNullOrEmpty(this.txtHazardListType.Text.Trim()))
            {
                hazardListTypeName = this.txtHazardListType.Text.Trim();
            }
            if (parentId == "0")
            {
                return (from x in Funs.DB.Technique_HazardListType where (x.IsCompany == false || x.IsCompany == null) && x.SupHazardListTypeId == "0" && x.HazardListTypeName.Contains(hazardListTypeName) orderby x.HazardListTypeCode select x).ToList();
            }
            else
            {
                return (from x in Funs.DB.Technique_HazardListType where (x.IsCompany == false || x.IsCompany == null) && x.SupHazardListTypeId == parentId orderby x.HazardListTypeCode select x).ToList();
            }
        }
        #endregion

        #region 增加数节点数据
        /// <summary>
        /// 增加树节点数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuNew2_Click(object sender, EventArgs e)
        {
            if (this.trHazardListType.SelectedNode != null)
            {
                Model.Technique_HazardListType hazardListType = BLL.HazardListTypeService.GetHazardListTypeById(this.trHazardListType.SelectedNode.NodeID);
                if ((hazardListType != null && hazardListType.IsEndLevel == false) || this.trHazardListType.SelectedNode.NodeID == "0")   //根节点或者非末级节点，可以增加
                {

                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HazardListTypeEdit.aspx?SupHazardListTypeId={0}&&IsCompany=False", this.trHazardListType.SelectedNode.NodeID, "编辑 - ")));

                }
                else
                {
                    ShowNotify("选择的项已是末级！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 编辑树
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit2_Click(object sender, EventArgs e)
        {
            if (this.trHazardListType.SelectedNode != null)
            {
                if (this.trHazardListType.SelectedNode.NodeID != "0")   //非根节点可以编辑
                {

                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HazardListTypeEdit.aspx?HazardListTypeId={0}", this.trHazardListType.SelectedNode.NodeID, "编辑 - ")));
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

        #region 删除树
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete2_Click(object sender, EventArgs e)
        {
            if (this.trHazardListType.SelectedNode != null)
            {
                var hazardList = BLL.HazardListTypeService.GetHazardListTypeById(this.trHazardListType.SelectedNode.NodeID);
                if (hazardList != null && BLL.HazardListTypeService.IsDeleteHazardListType(this.trHazardListType.SelectedNode.NodeID))
                {
                    BLL.LogService.AddSys_Log(this.CurrUser, hazardList.HazardListTypeCode, hazardList.HazardListTypeId, BLL.Const.HazardListMenuId, Const.BtnDelete);
                    BLL.HazardListTypeService.DeleteHazardListTypeById(this.trHazardListType.SelectedNode.NodeID);
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

        #region 点击树节点
        /// <summary>
        /// 点击树节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void trHazardListType_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 绑定Grid
        /// <summary>
        /// 绑定Grid
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT H.HazardId,T.HazardListTypeId,T.HazardListTypeCode,T.HazardListTypeName,H.HazardCode,H.HazardItems,H.DefectsType,"
                                     + @" H.MayLeadAccidents,H.HelperMethod,H.HazardJudge_L,H.HazardJudge_E,H.HazardJudge_C,H.HazardJudge_D,Const0007.ConstText AS HazardLevel,"
                                     + @"  H.ControlMeasures,H.CompileMan,H.CompileDate,H.AuditMan,H.AuditDate,H.IsPass,U.UserName AS CompileManName,UR.UserName AS AuditManName"
                                     + @" FROM Technique_HazardList AS H"
                                     + @" LEFT JOIN Technique_HazardListType AS T ON T.HazardListTypeId = H.HazardListTypeId"
                                     + @" LEFT JOIN Sys_User AS U ON U.UserId = H.CompileMan"
                                     + @" LEFT JOIN Sys_User AS UR ON UR.UserId = H.AuditMan"
                                     + @" LEFT JOIN Sys_Const AS Const0007 ON Const0007.ConstValue = H.HazardLevel and Const0007.GroupId = '" + ConstValue.Group_0007 + "'"
                                     + @"  WHERE T.HazardListTypeId = @HazardListTypeId ";
            List<SqlParameter> listStr = new List<SqlParameter>
            {
                new SqlParameter("@HazardListTypeId", this.trHazardListType.SelectedNode.NodeID)
            };
            if (!string.IsNullOrEmpty(this.HazardCode.Text.Trim()))
            {
                strSql += " AND HazardCode LIKE @HazardCode";
                listStr.Add(new SqlParameter("@HazardCode", "%" + this.HazardCode.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.HazardListTypeCode.Text.Trim()))
            {
                strSql += " AND HazardListTypeCode LIKE @HazardListTypeCode";
                listStr.Add(new SqlParameter("@HazardListTypeCode", "%" + this.HazardListTypeCode.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
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
            BLL.LogService.AddSys_Log(this.CurrUser, string.Empty, string.Empty, BLL.Const.HazardListMenuId, Const.BtnQuery);
        }
        #endregion

        #region 表分页、排序
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

        /// <summary>
        /// Grid排序
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
        #endregion

        #region Grid行双击事件
        /// <summary>
        /// 双击行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }
        #endregion

        #region 增加明细
        /// <summary>
        /// 增加危险源清单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewDetail_Click(object sender, EventArgs e)
        {
            if (this.trHazardListType.SelectedNode != null)
            {
                if (this.trHazardListType.SelectedNode.Nodes.Count == 0)
                {
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("HazardListEdit.aspx?HazardListTypeId={0}", this.trHazardListType.SelectedNode.NodeID, "编辑 - ")));
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
            string hazardId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("HazardListEdit.aspx?HazardId={0}", hazardId, "编辑 - ")));
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
                    var hazard = BLL.HazardListService.GetHazardListById(rowID);
                    if (hazard != null)
                    {
                        var selectItem = BLL.Hazard_HazardSelectedItemService.GetHazardSelectedItemByHazardId(rowID);
                        if (selectItem != null)
                        {
                            string value = "在项目级危险源评价清单中已使用该资源，无法删除！";
                            var hazardList = BLL.Hazard_HazardListService.GetHazardList(selectItem.HazardListId);
                            if (hazardList != null)
                            {
                                var project = BLL.ProjectService.GetProjectByProjectId(hazardList.ProjectId);
                                if (project != null)
                                {
                                    value = "在项目：" + project.ProjectName + "中；危险源辨识评价清单：" + hazardList.HazardListCode + "中已使用该资源，无法删除！";
                                }
                            }
                            Alert.ShowInTop(value, MessageBoxIcon.Warning);
                            return;
                        }

                        BLL.LogService.AddSys_Log(this.CurrUser, hazard.HazardCode, hazard.HazardId, BLL.Const.HazardListMenuId, Const.BtnDelete);
                        BLL.HazardListService.DeleteHazardListById(rowID);
                    }
                }

                BindGrid();
                ShowNotify("删除数据成功!");
            }
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
        /// <param name = "grid" ></ param >
        /// < param name="columns"></param>
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

        #region 关闭窗口
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            InitTreeMenu();
        }

        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HazardListMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnMenuNew2.Hidden = false;
                    this.btnNewDetail.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuEdit2.Hidden = false;
                    this.btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelete2.Hidden = false;
                    this.btnMenuDelete.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnOut))
                {
                    this.btnOut.Hidden = false;
                    this.btnAllOut.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnIn))
                {
                    this.btnImport.Hidden = false;
                }
            }
        }
        #endregion

        #region 导入
        /// <summary>
        /// 导入按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window6.GetShowReference(String.Format("HazardListIn.aspx?IsCompany=False", "导入 - ")));    
        }
        #endregion

        #region 查询危险源类别
        /// <summary>
        /// 查询危险源类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Text_TextChanged(object sender, EventArgs e)
        {
            InitTreeMenu();
            BLL.LogService.AddSys_Log(this.CurrUser, string.Empty, string.Empty, BLL.Const.HazardListMenuId, Const.BtnQuery);
        }
        #endregion

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("危险源清单" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = Grid1.RecordCount;
            BindGrid();
            Response.Write(GetGridTableHtmlPage(Grid1));
            Response.End();
        }

        #region 导出方法
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static string GetGridTableHtmlPage(Grid grid)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<meta http-equiv=\"content-type\" content=\"application/excel; charset=UTF-8\"/>");
            sb.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");
            sb.Append("<tr>");
            foreach (GridColumn column in grid.Columns)
            {
                sb.AppendFormat("<td>{0}</td>", column.HeaderText);
            }
            sb.Append("</tr>");
            foreach (GridRow row in grid.Rows)
            {
                sb.Append("<tr>");
                foreach (GridColumn column in grid.Columns)
                {
                    string html = row.Values[column.ColumnIndex].ToString();
                    if (column.ColumnID == "tfNumber" && (row.FindControl("lbNumber") as AspNet.Label) != null)
                    {
                        html = (row.FindControl("lbNumber") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfHazardCode" && (row.FindControl("lbHazardCode") as AspNet.Label) != null)
                    {
                        html = (row.FindControl("lbHazardCode") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfHazardItems" && (row.FindControl("lbHazardItems") as AspNet.Label) != null)
                    {
                        html = (row.FindControl("lbHazardItems") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfDefectsType" && (row.FindControl("lbDefectsType") as AspNet.Label) != null)
                    {
                        html = (row.FindControl("lbDefectsType") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfMayLeadAccidents" && (row.FindControl("lbMayLeadAccidents") as AspNet.Label) != null)
                    {
                        html = (row.FindControl("lbMayLeadAccidents") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfHelperMethod" && (row.FindControl("lbHelperMethod") as AspNet.Label) != null)
                    {
                        html = (row.FindControl("lbHelperMethod") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfControlMeasures" && (row.FindControl("lbControlMeasures") as AspNet.Label) != null)
                    {
                        html = (row.FindControl("lbControlMeasures") as AspNet.Label).Text;
                    }
                    sb.AppendFormat("<td>{0}</td>", html);
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }
        #endregion

        protected void btnAllOut_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window6.GetShowReference(String.Format("HazardListOut.aspx?IsCompany=False", "导出 - "),"导出",1200,650));
        }
    }
}