using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.EduTrain
{
    public partial class CompanyTraining : PageBase
    {
        #region 加载
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
        #endregion

        #region 加载树
        /// <summary>
        /// 初始化树
        /// </summary>
        private void InitTreeMenu()
        {
            trCompanyTraining.Nodes.Clear();
            trCompanyTraining.ShowBorder = false;
            trCompanyTraining.ShowHeader = false;
            trCompanyTraining.EnableIcons = true;
            trCompanyTraining.AutoScroll = true;
            trCompanyTraining.EnableSingleClickExpand = true;
            TreeNode rootNode = new TreeNode
            {
                Text = "公司教材库",
                NodeID = "0",
                Expanded = true
            };
            this.trCompanyTraining.Nodes.Add(rootNode);
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
                        Text = dr.CompanyTrainingName,
                        NodeID = dr.CompanyTrainingId,
                        EnableClickEvent = true,
                        ToolTip = dr.CompanyTrainingName
                    };
                    nodes.Add(tn);
                    BoundTree(tn.Nodes, dr.CompanyTrainingId);
                }
            }
        }

        /// <summary>
        /// 得到菜单方法
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private List<Model.Training_CompanyTraining> GetNewTraining(string parentId)
        {
            return (from x in Funs.DB.Training_CompanyTraining where x.SupCompanyTrainingId == parentId orderby x.CompanyTrainingCode select x).ToList(); ;
        }
        #endregion

        #region 增加、修改、删除公司教材库类别
        /// <summary>
        /// 增加公司教材库类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (this.trCompanyTraining.SelectedNode != null)
            {
                Model.Training_CompanyTraining training = BLL.CompanyTrainingService.GetCompanyTrainingById(this.trCompanyTraining.SelectedNode.NodeID);
                if ((training != null && training.IsEndLever == false) || this.trCompanyTraining.SelectedNode.NodeID == "0")   //根节点或者非末级节点，可以增加
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CompanyTrainingSave.aspx?SupCompanyTrainingId={0}", this.trCompanyTraining.SelectedNode.NodeID, "编辑 - ")));
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
        /// 修改公司教材库类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.trCompanyTraining.SelectedNode != null)
            {
                if (this.trCompanyTraining.SelectedNode.NodeID != "0")   //非根节点可以编辑
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CompanyTrainingSave.aspx?CompanyTrainingId={0}", this.trCompanyTraining.SelectedNode.NodeID, "编辑 - ")));
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
        /// 删除公司教材库类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.trCompanyTraining.SelectedNode != null)
            {
                var q = BLL.CompanyTrainingService.GetCompanyTrainingById(this.trCompanyTraining.SelectedNode.NodeID);

                if (q != null && BLL.CompanyTrainingService.IsDeleteCompanyTraining(this.trCompanyTraining.SelectedNode.NodeID))
                {
                    BLL.CompanyTrainingService.DeleteCompanyTraining(this.trCompanyTraining.SelectedNode.NodeID);
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

        #region 树点击事件
        /// <summary>
        /// 树点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void trCompanyTraining_NodeCommand(object sender, FineUIPro.TreeCommandEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            if (this.trCompanyTraining.SelectedNode != null && !string.IsNullOrEmpty(this.trCompanyTraining.SelectedNode.NodeID))
            {
                string strSql = @"SELECT item.CompanyTrainingItemId,(item.CompanyTrainingItemId+'_'+CAST(row_number() over(order by  item.CompanyTrainingItemCode) AS nvarchar(10))) AS CompanyTrainingItemIdNum,"
                                + @"  item.CompanyTrainingItemId,item.CompanyTrainingId,item.CompanyTrainingItemCode,item.CompanyTrainingItemName,A.AttachUrl,"
                                + @" item.CompileMan, item.CompileDate,dbo.GetFileName(A.AttachUrl) AS AttachUrlName "
                                + @" FROM dbo.Training_CompanyTrainingItem AS item"
                                + @" LEFT JOIN (SELECT ToKeyId ,F1 as AttachUrl"
                                + @" FROM AttachFile CROSS APPLY (SELECT * FROM dbo.f_splitstr(AttachUrl,',')) t"
                                + @" WHERE LEN(F1) > 0) AS A ON A.ToKeyId=item.CompanyTrainingItemId "
                                + @" WHERE item.CompanyTrainingId=@CompanyTrainingId";

                List<SqlParameter> listStr = new List<SqlParameter>
                {
                    new SqlParameter("@CompanyTrainingId", this.trCompanyTraining.SelectedNode.NodeID)
                };
                if (!string.IsNullOrEmpty(this.txtCompanyTrainingItemCode.Text.Trim()))
                {
                    strSql += " AND CompanyTrainingItemCode LIKE @CompanyTrainingItemCode";
                    listStr.Add(new SqlParameter("@CompanyTrainingItemCode", "%" + this.txtCompanyTrainingItemCode.Text.Trim() + "%"));
                }
                if (!string.IsNullOrEmpty(this.txtCompanyTrainingItemName.Text.Trim()))
                {
                    strSql += " AND CompanyTrainingItemName LIKE @CompanyTrainingItemName";
                    listStr.Add(new SqlParameter("@CompanyTrainingItemName", "%" + this.txtCompanyTrainingItemName.Text.Trim() + "%"));
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

        #region 弹出窗关闭事件
        protected void Window1_Close(object sender, EventArgs e)
        {
            InitTreeMenu();
        }

        protected void Window2_Close(object sender, EventArgs e)
        {
            BindGrid();
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

        #region 查询
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

        #region 增加、修改、删除明细信息
        #region 新增明细
        /// <summary>
        /// 新增明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewDetail_Click(object sender, EventArgs e)
        {
            if (this.trCompanyTraining.SelectedNode != null)
            {
                if (this.trCompanyTraining.SelectedNode.Nodes.Count == 0)
                {
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("CompanyTrainingItemSave.aspx?CompanyTrainingId={0}", this.trCompanyTraining.SelectedNode.NodeID, "编辑 - ")));
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

            string companyTrainingItemId = Grid1.SelectedRowID;
            companyTrainingItemId = companyTrainingItemId.Substring(0, companyTrainingItemId.IndexOf("_"));
            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("CompanyTrainingItemSave.aspx?CompanyTrainingItemId={0}", companyTrainingItemId, "编辑 - ")));
        }
        #endregion

        #region 删除明细
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
                    rowID = rowID.Substring(0, rowID.IndexOf("_"));

                    var getD = BLL.CompanyTrainingItemService.GetCompanyTrainingItemById(rowID);
                    if (getD != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, getD.CompanyTrainingItemCode, getD.CompanyTrainingItemId, BLL.Const.CompanyTrainingMenuId, BLL.Const.BtnDelete);
                        BLL.CompanyTrainingItemService.DeleteCompanyTrainingItemById(rowID);
                        
                    }
                }

                BindGrid();
                ShowNotify("删除数据成功!");
            }
        }
        #endregion
        #endregion
                
        #region 按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.CompanyTrainingMenuId);
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
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("公司教材库" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = Grid1.RecordCount;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1, 5));
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
                        if (column.ColumnID == "tfCompanyTrainingItemCode" && (row.FindControl("lbCompanyTrainingItemCode") as AspNet.Label) != null)
                        {
                            html = (row.FindControl("lbCompanyTrainingItemCode") as AspNet.Label).Text;
                        }
                        if (column.ColumnID == "tfCompanyTrainingItemName" && (row.FindControl("lbCompanyTrainingItemName") as AspNet.Label) != null)
                        {
                            html = (row.FindControl("lbCompanyTrainingItemName") as AspNet.Label).Text;
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

        /// <summary>
        /// 行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {          
            if (e.CommandName == "Attach")
            {
                string attUrl = this.Grid1.Rows[e.RowIndex].Values[this.Grid1.Columns.Count - 1].ToString();
                try
                {
                    
                    string url = Funs.RootPath + attUrl;
                    FileInfo info = new FileInfo(url);
                    string savedName = Path.GetFileName(url);
                    if (!info.Exists || string.IsNullOrEmpty(savedName))
                    {
                        url = Funs.RootPath + "Images//Null.jpg";
                        info = new FileInfo(url);
                    }

                    if (Path.GetExtension(savedName) == ".mp4" || Path.GetExtension(savedName).ToLower() == ".mp4" || Path.GetExtension(savedName) == ".m4v")
                    {
                        string mpUrl = HttpUtility.UrlEncode(attUrl.Replace('\\', '/'));
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("~/AttachFile/player.aspx?url={0}", attUrl.Replace('\\', '/'), "播放 - "),"播放视频",700,560));
                    }
                    else
                    {
                        string fileName = Path.GetFileName(url);
                        long fileSize = info.Length;
                        System.Web.HttpContext.Current.Response.Clear();
                        //System.Web.HttpContext.Current.Response.ContentType = "application/x-zip-compressed";
                        System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                        System.Web.HttpContext.Current.Response.AddHeader("Content-Length", fileSize.ToString());
                        System.Web.HttpContext.Current.Response.TransmitFile(url, 0, fileSize);
                        System.Web.HttpContext.Current.Response.Flush();
                        System.Web.HttpContext.Current.Response.End();                       
                    }
                }
                catch (Exception)
                {

                }
            }
        }
    }
}