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

namespace FineUIPro.Web.HSSE.EduTrain
{
    public partial class TrainDB : PageBase
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
                Funs.DropDownPageSize(this.ddlPageSize);
                this.GetButtonPower();
                InitTreeMenu();
            }
        }

        /// <summary>
        /// 初始化树
        /// </summary>
        private void InitTreeMenu()
        {
            trTraining.Nodes.Clear();
            trTraining.ShowBorder = false;
            trTraining.ShowHeader = false;
            trTraining.EnableIcons = true;
            trTraining.AutoScroll = true;
            trTraining.EnableSingleClickExpand = true;
            TreeNode rootNode = new TreeNode
            {
                Text = "培训教材库",
                NodeID = "0",
                Expanded = true
            };
            this.trTraining.Nodes.Add(rootNode);
            BoundTree(rootNode.Nodes, "0");
        }

        private void BoundTree(TreeNodeCollection nodes, string menuId)
        {
            var dt = GetNewTraining(menuId);
            if (dt.Count() > 0)
            {
                TreeNode tn = null;
                foreach (var dr in dt)
                {
                    tn = new TreeNode
                    {
                        Text = dr.TrainingName,
                        NodeID = dr.TrainingId,
                        EnableClickEvent = true,
                        ToolTip = dr.TrainingName
                    };
                    //var dt2 = GetNewTraining(dr.TrainingId);
                    //if (dt2.Count == 0)
                    //{
                    //    tn.Icon = "TagBlue";
                    //}
                    nodes.Add(tn);
                    BoundTree(tn.Nodes, dr.TrainingId);
                }
            }
        }

        /// <summary>
        /// 得到菜单方法
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private List<Model.Training_Training> GetNewTraining(string parentId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Training_Training where x.SupTrainingId == parentId
                    orderby x.TrainingCode select x).ToList();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (this.trTraining.SelectedNode != null)
            {
                Model.Training_Training training = BLL.TrainingService.GetTrainingByTrainingId(this.trTraining.SelectedNode.NodeID);
                if ((training != null && training.IsEndLever == false) || this.trTraining.SelectedNode.NodeID == "0")   //根节点或者非末级节点，可以增加
                {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("TrainingSave.aspx?SupTrainingId={0}", this.trTraining.SelectedNode.NodeID, "编辑 - ")));                  
                }
                else
                {
                    ShowNotify("选择的项已是末级！",MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.trTraining.SelectedNode != null)
            {
                if (this.trTraining.SelectedNode.NodeID != "0")   //非根节点可以编辑
                {                   
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("TrainingSave.aspx?TrainingId={0}", this.trTraining.SelectedNode.NodeID, "编辑 - ")));                   
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.trTraining.SelectedNode != null)
            {
                var q = BLL.TrainingService.GetTrainingByTrainingId(this.trTraining.SelectedNode.NodeID);

                if (q != null && BLL.TrainingService.IsDeleteTraining(this.trTraining.SelectedNode.NodeID))
                {
                    BLL.TrainingService.DeleteTrainingByTrainingId(this.trTraining.SelectedNode.NodeID);
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

        protected void trTraining_NodeCommand(object sender, FineUIPro.TreeCommandEventArgs e)
        {
            BindGrid();
        }

        #region BindGrid

        private void BindGrid()
        {
            if (this.trTraining.SelectedNode !=null && !string.IsNullOrEmpty(this.trTraining.SelectedNode.NodeID))
            {
                string strSql = @"SELECT TrainingItemId,TrainingId,TrainingItemCode,TrainingItemName,AttachUrl,VersionNum,ApproveState,ResourcesFrom "
                                + @" ,CompileMan,CompileDate,ResourcesFromType,AuditMan,AuditDate,IsPass,UnitId,UnitCode,UnitName,TrainingCode,TrainingName,AttachUrlName "
                                + @" FROM dbo.View_Training_TrainingItem"
                                + @" WHERE TrainingId=@TrainingId ";

                List<SqlParameter> listStr = new List<SqlParameter>
                {
                    new SqlParameter("@TrainingId", this.trTraining.SelectedNode.NodeID),
                };
                if (!string.IsNullOrEmpty(this.TrainingItemCode.Text.Trim()))
                {
                    strSql += " AND TrainingItemCode LIKE @TrainingItemCode";
                    listStr.Add(new SqlParameter("@TrainingItemCode", "%" + this.TrainingItemCode.Text.Trim() + "%"));
                }
                if (!string.IsNullOrEmpty(this.TrainingItemName.Text.Trim()))
                {
                    strSql += " AND TrainingItemName LIKE @TrainingItemName";
                    listStr.Add(new SqlParameter("@TrainingItemName", "%" + this.TrainingItemName.Text.Trim() + "%"));
                }
                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

                Grid1.RecordCount = tb.Rows.Count;
                var table = this.GetPagedDataTable(Grid1, tb);

                Grid1.DataSource = table;
                Grid1.DataBind();
            }
        }
        #endregion

        #region Events
        protected void Window1_Close(object sender, EventArgs e)
        {
            InitTreeMenu();
        }

        protected void Window2_Close(object sender, EventArgs e)
        {
            BindGrid();
        }

        // 删除数据
        protected void btnDeleteDetail_Click(object sender, EventArgs e)
        {
            this.DeleteData();
        }

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
                    var getV = BLL.TrainingItemService.GetTrainingItemByTrainingItemId(rowID);
                    if(getV !=null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, getV.TrainingItemCode, getV.TrainingItemId, BLL.Const.TrainDBMenuId, BLL.Const.BtnDelete);
                        BLL.TrainingItemService.DeleteTrainingItemsByTrainingItemId(rowID);
                    }
                    
                }

                BindGrid();
                ShowNotify("删除数据成功!");
            }
        }
        #endregion
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
            BindGrid();
        }

        protected void btnEditDetail_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

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

            string trainingItemId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("TrainingItemSave.aspx?TrainingItemId={0}", trainingItemId, "编辑 - ")));
        }

        protected void btnNewDetail_Click(object sender, EventArgs e)
        {
            if (this.trTraining.SelectedNode != null)
            {
                if (this.trTraining.SelectedNode.Nodes.Count == 0)
                {
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("TrainingItemSave.aspx?TrainingId={0}", this.trTraining.SelectedNode.NodeID, "编辑 - ")));
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.TrainDBMenuId);
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
            }
        }
        #endregion

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Action1" && !string.IsNullOrEmpty(Grid1.SelectedRow.Values[5].ToString()))
            {
                string url = "~/common/ShowUpFile.aspx?fileUrl=" + Server.UrlEncode(Grid1.SelectedRow.Values[5].ToString());
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format(url), "从集团服务下载大附件"));
            }
            else
            {
                Alert.ShowInTop("附件不存在或数据不同步！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("培训教材库" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = Grid1.RecordCount;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1, 3));
            Response.End();
        }

        #region 导出方法
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static string GetGridTableHtml(Grid grid, int count)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<meta http-equiv=\"content-type\" content=\"application/excel; charset=UTF-8\"/>");
            sb.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");
            sb.Append("<tr>");
            foreach (GridColumn column in grid.Columns)
            {
                if (column.ColumnIndex < count)
                {
                    sb.AppendFormat("<td>{0}</td>", column.HeaderText);
                }
            }
            sb.Append("</tr>");
            foreach (GridRow row in grid.Rows)
            {
                sb.Append("<tr>");
                foreach (GridColumn column in grid.Columns)
                {
                    if (column.ColumnIndex < count)
                    {
                        string html = row.Values[column.ColumnIndex].ToString();
                        if (column.ColumnID == "tfNumber" && (row.FindControl("lbNumber") as AspNet.Label) != null)
                        {
                            html = (row.FindControl("lbNumber") as AspNet.Label).Text;
                        }
                        if (column.ColumnID == "tfTrainingItemCode" && (row.FindControl("lbTrainingItemCode") as AspNet.Label) != null)
                        {
                            html = (row.FindControl("lbTrainingItemCode") as AspNet.Label).Text;
                        }
                        if (column.ColumnID == "tfTrainingItemName" && (row.FindControl("lbTrainingItemName") as AspNet.Label) != null)
                        {
                            html = (row.FindControl("lbTrainingItemName") as AspNet.Label).Text;
                        }
                        if (column.ColumnID == "tfCompileMan" && (row.FindControl("lbCompileMan") as AspNet.Label) != null)
                        {
                            html = (row.FindControl("lbCompileMan") as AspNet.Label).Text;
                        }
                        if (column.ColumnID == "tfCompileDate" && (row.FindControl("lbCompileDate") as AspNet.Label) != null)
                        {
                            html = (row.FindControl("lbCompileDate") as AspNet.Label).Text;
                        }
                        sb.AppendFormat("<td>{0}</td>", html);
                    }
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }
        #endregion
    }
}