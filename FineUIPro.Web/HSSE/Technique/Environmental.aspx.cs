using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.Technique
{
    public partial class Environmental : PageBase
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
                //权限设置
                this.GetButtonPower();
                btnNew.OnClientClick = Window1.GetShowReference("EnvironmentalEdit.aspx?IsCompany=False") + "return false;";
                
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                //环境因素类型             
                BLL.ConstValue.InitConstValueDropDownList(this.drpEType, ConstValue.Group_EnvironmentalType, true);
                BLL.ConstValue.InitConstValueDropDownList(this.drpSmallType, ConstValue.Group_EnvironmentalSmallType, true);
                // 绑定表格
                this.BindGrid();
            }
            else
            {
                if (GetRequestEventArgument() == "reloadGrid")
                {
                    this.BindGrid();
                }
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT Environmental.EnvironmentalId,Environmental.EType,Environmental.ActivePoint,Environmental.EnvironmentalFactors,Environmental.AValue,Environmental.BValue,Environmental.CValue,Environmental.DValue,Environmental.EValue,Environmental.ZValue,Environmental.SmallType,Environmental.IsImportant,Environmental.Code,Environmental.ControlMeasures,Environmental.Remark"
                + @",(ISNULL(Environmental.AValue,0) + ISNULL(Environmental.BValue,0)+ ISNULL(Environmental.CValue,0)+ ISNULL(Environmental.DValue,0)+ ISNULL(Environmental.EValue,0)) AS ZValue1"
                + @",Environmental.FValue,Environmental.GValue,(ISNULL(Environmental.FValue,0) + ISNULL(Environmental.GValue,0)) AS ZValue2"
                + @" ,Sys_ConstEType.ConstText AS ETypeName,Sys_ConstESmallType.ConstText AS SmallTypeName "
                + @" FROM dbo.Technique_Environmental AS Environmental"
                + @" LEFT JOIN Sys_Const AS  Sys_ConstEType  ON Environmental.EType=Sys_ConstEType.ConstValue and Sys_ConstEType.GroupId='" + BLL.ConstValue.Group_EnvironmentalType + "'"
                + @" LEFT JOIN Sys_Const AS Sys_ConstESmallType ON Environmental.SmallType=Sys_ConstESmallType.ConstValue and Sys_ConstESmallType.GroupId='" + BLL.ConstValue.Group_EnvironmentalSmallType + "'"
                + @" WHERE (Environmental.IsCompany IS NULL OR Environmental.IsCompany='False') ";
            List<SqlParameter> listStr = new List<SqlParameter>();           
            if (this.drpEType.SelectedValue!=BLL.Const._Null)
            {
                strSql += " AND Environmental.EType= @EType";
                listStr.Add(new SqlParameter("@EType", this.drpEType.SelectedValue));
            }
            if (this.drpSmallType.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND Environmental.SmallType= @SmallType";
                listStr.Add(new SqlParameter("@SmallType", this.drpSmallType.SelectedValue));
            }
            if (!string.IsNullOrEmpty(this.txtActivePoint.Text.Trim()))
            {
                strSql += " AND Environmental.ActivePoint LIKE @ActivePoint";
                listStr.Add(new SqlParameter("@ActivePoint", "%" + this.txtActivePoint.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtEnvironmentalFactors.Text.Trim()))
            {
                strSql += " AND Environmental.EnvironmentalFactors LIKE @EnvironmentalFactors";
                listStr.Add(new SqlParameter("@EnvironmentalFactors", "%" + this.txtEnvironmentalFactors.Text.Trim() + "%"));
            }
            strSql += " order by Environmental.SmallType, Environmental.EType,Environmental.Code";

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(Grid1, tb1);
            Grid1.RecordCount = tb.Rows.Count;
           // tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
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
                bool isShow = false;
                if (Grid1.SelectedRowIndexArray.Length == 1)
                {
                    isShow = true;
                }
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    if (this.judgementDelete(rowID, isShow))
                    {
                        var getV = BLL.EnvironmentalService.GetEnvironmental(rowID);
                        if (getV != null)
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, getV.Code, getV.EnvironmentalId, BLL.Const.EnvironmentalMenuId, BLL.Const.BtnDelete);
                            BLL.EnvironmentalService.DeleteEnvironmental(rowID);
                        }
                    }
                }

                BindGrid();
                //ShowNotify("删除数据成功!");
            }
        }

        /// <summary>
        /// 判断是否可删除
        /// </summary>
        /// <param name="rowID"></param>
        /// <param name="isShow"></param>
        /// <returns></returns>
        private bool judgementDelete(string rowID, bool isShow)
        {
            string content = string.Empty;
            var environmentalRiskItem = Hazard_EnvironmentalRiskItemService.GetEnvironmentalRiskItemByEnvironmentalId(rowID);
            if (environmentalRiskItem.Count > 0)
            {
                content = "环境因素识别与评价中已使用了该环境因素，不能删除！";
            }
            if (string.IsNullOrEmpty(content))
            {
                return true;
            }
            else
            {
                if (isShow)
                {
                    Alert.ShowInTop(content, MessageBoxIcon.Warning);
                }
                return false;
            }
        }
        #endregion

        #region 数据编辑事件
        /// <summary>
        /// Grid行双击事件
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
                Alert.ShowInTop("请选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string EnvironmentalId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EnvironmentalEdit.aspx?EnvironmentalId={0}", EnvironmentalId, "编辑 - ")));
        }
        #endregion

        #region 根据表头信息过滤列表数据
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// 分页显示条数下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }
        #endregion

        #region 关闭窗口
        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 获取权限按钮
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.EnvironmentalMenuId);
            if (buttonList.Count > 0)
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
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnOut.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnIn))
                {
                    this.btnImport.Hidden = false;
                }
            }
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

        #region 导入
        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("EnvironmentalIn.aspx?IsCompany=False", "导入 - ")));
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("环境危险源清单" + filename, Encoding.UTF8) + ".xls");
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
                    sb.AppendFormat("<td>{0}</td>", html);
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }
        #endregion
    }
}