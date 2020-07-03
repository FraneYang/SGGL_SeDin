using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace FineUIPro.Web.HSSE.EduTrain
{
    public partial class AccidentCase : PageBase
    {
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnSelectColumns.OnClientClick = Window5.GetShowReference("AccidentCaseItemSelectCloumn.aspx");
                Funs.DropDownPageSize(this.ddlPageSize);
                this.GetButtonPower();
                InitTreeMenu();
            }
        }

        #region 加载树
        /// <summary>
        /// 初始化树
        /// </summary>
        private void InitTreeMenu()
        {
            trAccidentCase.Nodes.Clear();
            trAccidentCase.ShowBorder = false;
            trAccidentCase.ShowHeader = false;
            trAccidentCase.EnableIcons = true;
            trAccidentCase.AutoScroll = true;
            trAccidentCase.EnableSingleClickExpand = true;
            TreeNode rootNode = new TreeNode
            {
                Text = "事故案例库",
                NodeID = "0",
                Expanded = true
            };
            this.trAccidentCase.Nodes.Add(rootNode);
            var aCaseList = (from x in Funs.DB.EduTrain_AccidentCase
                       select x).ToList();
            if (aCaseList.Count() > 0)
            {
                BoundTree(rootNode.Nodes, "0", aCaseList);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="menuId"></param>
        /// <param name="aCaseList"></param>
        private void BoundTree(TreeNodeCollection nodes, string menuId, List<Model.EduTrain_AccidentCase> aCaseList)
        {
            var dt = aCaseList.Where(x => x.SupAccidentCaseId == menuId).ToList();
            if (dt.Count() > 0)
            {
                TreeNode tn = null;
                foreach (var dr in dt)
                {
                    tn = new TreeNode
                    {
                        Text = dr.AccidentCaseName,
                        ToolTip = dr.AccidentCaseName,
                        NodeID = dr.AccidentCaseId,
                        EnableClickEvent = true
                    };
                    nodes.Add(tn);
                    BoundTree(tn.Nodes, dr.AccidentCaseId, aCaseList);
                }
            }
        }
        #endregion

        /// <summary>
        /// Tree行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void trAccidentCase_NodeCommand(object sender, FineUIPro.TreeCommandEventArgs e)
        {
            BindGrid();
        }

        #region Tree增加、修改、删除 按钮事件
        /// <summary>
        /// 增加Tree节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (this.trAccidentCase.SelectedNode != null)
            {
                Model.EduTrain_AccidentCase accidentCase = BLL.AccidentCaseService.GetAccidentCaseById(this.trAccidentCase.SelectedNode.NodeID);
                if ((accidentCase != null && accidentCase.IsEndLever == false) || this.trAccidentCase.SelectedNode.NodeID == "0")   //根节点或者非末级节点，可以增加
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("AccidentCaseSave.aspx?SupAccidentCaseId={0}", this.trAccidentCase.SelectedNode.NodeID, "编辑 - ")));       
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

        /// <summary>
        /// 编辑Tree按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.trAccidentCase.SelectedNode != null)
            {
                if (this.trAccidentCase.SelectedNode.NodeID != "0")   //非根节点可以编辑
                {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("AccidentCaseSave.aspx?AccidentCaseId={0}", this.trAccidentCase.SelectedNode.NodeID, "编辑 - ")));
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

        /// <summary>
        /// 删除Tree节点数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.trAccidentCase.SelectedNode != null)
            {
                    var accidentCase = BLL.AccidentCaseService.GetAccidentCaseById(this.trAccidentCase.SelectedNode.NodeID);
                    if (accidentCase != null && BLL.AccidentCaseService.IsDeleteAccidentCase(this.trAccidentCase.SelectedNode.NodeID))
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, accidentCase.AccidentCaseCode, accidentCase.AccidentCaseId, BLL.Const.AccidentCaseMenuId, BLL.Const.BtnDelete);
                        BLL.AccidentCaseService.DeleteAccidentCaseByAccidentCaseId(this.trAccidentCase.SelectedNode.NodeID);
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

        #region BindGrid

        private void BindGrid()
        {
            if (!string.IsNullOrEmpty(this.trAccidentCase.SelectedNode.NodeID))
            {
                string strSql = @"SELECT I.AccidentCaseItemId,I.AccidentCaseId,I.Activities,I.AccidentName,I.AccidentProfiles,I.AccidentReview,
                                            A.AccidentCaseName,I.CompileMan,I.CompileDate,I.AuditMan,I.AuditDate,I.IsPass,U.UserName AS CompileManName,UR.UserName AS AuditManName
                                            FROM dbo.EduTrain_AccidentCaseItem AS I
                                            LEFT JOIN dbo.EduTrain_AccidentCase AS A ON A.AccidentCaseId = I.AccidentCaseId
                                            LEFT JOIN dbo.Sys_User AS U ON U.UserId = I.CompileMan
                                            LEFT JOIN dbo.Sys_User AS UR ON UR.UserId = I.AuditMan
                                            WHERE  I.AccidentCaseId=@AccidentCaseId";
                List<SqlParameter> listStr = new List<SqlParameter>
                {
                    new SqlParameter("@AccidentCaseId", this.trAccidentCase.SelectedNode.NodeID)
                };

                if (!string.IsNullOrEmpty(this.Activities.Text.Trim()))
                {
                    strSql += " AND Activities LIKE @Activities";
                    listStr.Add(new SqlParameter("@Activities", "%" + this.Activities.Text.Trim() + "%"));
                }
                if (!string.IsNullOrEmpty(this.AccidentCaseName.Text.Trim()))
                {
                    strSql += " AND AccidentCaseName LIKE @AccidentCaseName";
                    listStr.Add(new SqlParameter("@AccidentCaseName", "%" + this.AccidentCaseName.Text.Trim() + "%"));
                }
                if (!string.IsNullOrEmpty(this.AccidentName.Text.Trim()))
                {
                    strSql += " AND AccidentName LIKE @AccidentName";
                    listStr.Add(new SqlParameter("@AccidentName", "%" + this.AccidentName.Text.Trim() + "%"));
                }
                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
                Grid1.RecordCount = tb.Rows.Count;
                var table = this.GetPagedDataTable(Grid1, tb);

                Grid1.DataSource = table;
                Grid1.DataBind();
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        /// <summary>
        /// 过滤表头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
        }

        #region 关闭弹出框

        protected void Window1_Close(object sender, EventArgs e)
        {
            InitTreeMenu();
        }

        protected void Window2_Close(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 删除数据
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
                    var getV = BLL.AccidentCaseItemService.GetAccidentCaseItemById(rowID);
                    if (getV != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, getV.AccidentName, getV.AccidentCaseItemId, BLL.Const.AccidentCaseMenuId, BLL.Const.BtnDelete);
                        BLL.AccidentCaseItemService.DeleteAccidentCaseItemId(rowID);
                    }
                }

                BindGrid();
                ShowNotify("删除数据成功!");
            }
        }
        #endregion

        #region 排序、分页
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

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

            string accidentCaseItemId = Grid1.SelectedRowID;

            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("AccidentCaseItemSave.aspx?AccidentCaseItemId={0}", accidentCaseItemId, "编辑 - ")));
        }
        #endregion

        protected void btnNewDetail_Click(object sender, EventArgs e)
        {
            if (this.trAccidentCase.SelectedNode != null)
            {
                if (this.trAccidentCase.SelectedNode.Nodes.Count == 0)
                {
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("AccidentCaseItemSave.aspx?AccidentCaseId={0}", this.trAccidentCase.SelectedNode.NodeID, "编辑 - ")));
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

        #region 按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.AccidentCaseMenuId);
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