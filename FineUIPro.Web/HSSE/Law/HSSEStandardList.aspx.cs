using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace FineUIPro.Web.HSSE.Law
{
    public partial class HSSEStandardList : PageBase
    {
        #region 加载页面
        protected void Page_Load(object sender, EventArgs e)
        {          
            if (!IsPostBack)
            {
                Funs.DropDownPageSize(this.ddlPageSize);
                this.GetButtonPower();//设置权限
                btnNew.OnClientClick = Window1.GetShowReference("HSSEStandardListSave.aspx") + "return false;";             
                btnSelectColumns.OnClientClick = Window5.GetShowReference("HSSEStandardListSelectCloumn.aspx");
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                BindGrid();
            }
            else
            {
                if (GetRequestEventArgument() == "reloadGrid")
                {
                    BindGrid();
                }
            }
        } 

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT hsl.StandardId,hsl.StandardGrade,hsl.StandardNo,hsl.StandardName,hsl.TypeId,hslt.TypeCode,hslt.TypeName,hsl.AttachUrl,hsl.IsSelected1,hsl.IsSelected2,hsl.IsSelected3
                                            ,hsl.IsSelected4,hsl.IsSelected5,hsl.IsSelected6,hsl.IsSelected7,hsl.IsSelected8,hsl.IsSelected9,hsl.IsSelected10,hsl.IsSelected11,hsl.IsSelected12,hsl.IsSelected13
                                            ,hsl.IsSelected14,hsl.IsSelected15,hsl.IsSelected16,hsl.IsSelected17,hsl.IsSelected18,hsl.IsSelected19,hsl.IsSelected20,hsl.IsSelected21,hsl.IsSelected22,hsl.IsSelected23
                                            ,hsl.IsSelected24,hsl.IsSelected25,hsl.IsSelected90,hsl.CompileMan,hsl.CompileDate,hsl.IsPass,(CASE WHEN IsBuild = 1 THEN '集团' ELSE '' END ) AS IsBuildName
                                            ,hsl.UnitId,hsl.UpState,hsl.IsBuild,Substring(hsl.AttachUrl,charindex('~',hsl.AttachUrl)+1,LEN(hsl.AttachUrl)) as  AttachUrlName
                                            FROM Law_HSSEStandardsList as hsl
                                            LEFT JOIN Base_HSSEStandardListType AS hslt ON hslt.TypeId = hsl.TypeId WHERE 1=1";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.txtStandardNo.Text.Trim()))
            {
                strSql += " AND StandardNo LIKE @StandardNo";
                listStr.Add(new SqlParameter("@StandardNo", "%" + this.txtStandardNo.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtStandardName.Text.Trim()))
            {
                strSql += " AND StandardName LIKE @StandardName";
                listStr.Add(new SqlParameter("@StandardName", "%" + this.txtStandardName.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtTypeName.Text.Trim()))
            {
                strSql += " AND TypeName LIKE @TypeName";
                listStr.Add(new SqlParameter("@TypeName", "%" + this.txtTypeName.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion
        
        #region 删除
        // 删除数据
        protected void btnDelete_Click(object sender, EventArgs e)
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
                    var getV = BLL.HSSEStandardsListService.GetHSSEStandardsListByHSSEStandardsListId(rowID);
                    if (getV != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser,getV.StandardNo,getV.StandardId,BLL.Const.HSSEStandardListMenuId,BLL.Const.BtnDelete);
                        BLL.HSSEStandardsListService.DeleteHSSEStandardsList(rowID);
                    }
                }
                
                BindGrid();
                ShowNotify("删除数据成功!");
            }
        }
        #endregion

        #region 分页
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

        #region 关闭弹出框
        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 编辑
        protected void btnEdit_Click(object sender, EventArgs e)
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
                Alert.ShowInTop("请选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string standardId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HSSEStandardListSave.aspx?StandardId={0}", standardId, "编辑 - ")));
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

        #region 文本框查询事件
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

        #region 按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSEStandardListMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
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