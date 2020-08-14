﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BLL;
using System.Data;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.HJGL.WPQ
{
    public partial class WPQList : PageBase
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
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT wpq.WPQId,u.UnitName,wpq.WPQCode,wpq.CompileDate,wpq.Specifications,
                                      c.ConsumablesName WeldingRod,c1.ConsumablesName WeldingWire, g.GrooveTypeName,
                                     wpq.WeldingPosition,wpq.WeldingMethodId,
									 CAST(wpq.MinImpactDia AS VARCHAR(10))+'~'+CAST(wpq.MaxImpactDia AS VARCHAR(10)) AS DiaRange,
									 CAST(wpq.MinCImpactDia AS VARCHAR(10))+'~'+CAST(wpq.MaxCImpactDia AS VARCHAR(10)) AS CDiaRange,
                                     CAST(wpq.MinImpactThickness AS VARCHAR(10))+'~'+ CAST(wpq.MaxImpactThickness AS VARCHAR(10)) AS ThicknessRange,
									 CAST(wpq.NoMinImpactThickness AS VARCHAR(10))+'~'+ CAST(wpq.NoMaxImpactThickness AS VARCHAR(10)) AS CThicknessRange,
									 (CASE wpq.IsHotProess WHEN 1 THEN '是' ELSE '否' END) AS IsHotProess, 
                                     wpq.WPQStandard,wpq.PreTemperature,wpq.Remark,wpq.JointType,wpq.Motorization,wpq.ProtectiveGas,
                                     wpq.Stretching,wpq.Bend,wpq.ToAttack,wpq.Others,wmt.WeldingMethodCode,
                                     mat1.MaterialCode as MaterialCode1,mat1.MaterialClass as MaterialClass2,mat1.MaterialGroup as MaterialGroup1,
                                     mat2.MaterialCode as MaterialCode2,mat2.MaterialClass as MaterialClass2,mat2.MaterialGroup as MaterialGroup2
                              FROM WPQ_WPQList AS wpq
                                   LEFT JOIN Base_Material AS mat1 ON mat1.MaterialId = wpq.MaterialId1
                                   LEFT JOIN Base_Material AS mat2 ON mat2.MaterialId = wpq.MaterialId2
                                   LEFT JOIN Base_WeldingMethod AS wmt ON wmt.WeldingMethodId = wpq.WeldingMethodId
								   LEFT JOIN dbo.Base_Unit u ON u.UnitId=wpq.UnitId
                                   LEFT JOIN dbo.Base_Consumables c ON c.ConsumablesId=wpq.WeldingRod 
								   LEFT JOIN dbo.Base_Consumables c1 ON c1.ConsumablesId=wpq.WeldingWire 
                                   LEFT JOIN dbo.Base_GrooveType g ON g.GrooveTypeId=wpq.GrooveType
                             WHERE 1 = 1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.txtWeldingProcedureCode.Text.Trim()))
            {
                strSql += " AND wpq.WPQCode LIKE @WPQCode";
                listStr.Add(new SqlParameter("@WPQCode", "%" + this.txtWeldingProcedureCode.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region 表头过滤
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
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

        #region 弹出编辑窗口关闭事件
        /// <summary>
        /// 弹出编辑窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
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
            BindGrid();
        }
        #endregion

        #region 增加按钮
        /// <summary>
        /// 增加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.WPQListMenuId, BLL.Const.BtnAdd))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("WPQEdit.aspx?WPQId={0}", string.Empty, "编辑 - ")));
            }
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 双击Grid事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 编辑按钮
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
            string id = Grid1.SelectedRowID;
            if (!string.IsNullOrEmpty(id))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("WPQEdit.aspx?WPQId={0}", id, "编辑 - ")));
            }
        }
        #endregion

        #region 删除
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
            if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.WPQListMenuId, BLL.Const.BtnDelete))
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
                            BLL.WPQListServiceService.DeleteWPQById(rowID);
                            //BLL.Sys_LogService.AddLog(BLL.Const.System_2, this.CurrUser.LoginProjectId, this.CurrUser.UserId, "删除焊接工艺评定台账");
                        }
                    }
                    this.BindGrid();
                    ShowNotify("删除数据成功!", MessageBoxIcon.Success);
                }
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
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

        #region 导入
        /// <summary>
        /// 导入按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.WPQListMenuId, BLL.Const.BtnIn))
            {
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("WPQIn.aspx", "导入 - ")));
            }
        }
        #endregion

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("焊接工艺评定台账" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
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
                    if (column.ColumnID == "tfNumber")
                    {
                        html = (row.FindControl("lblNumber") as AspNet.Label).Text;
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