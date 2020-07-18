using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using AspNet = System.Web.UI.WebControls;
using System.Text;
using BLL;

namespace FineUIPro.Web.HJGL.PersonManage
{
    public partial class WelderManage : PageBase
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
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                BLL.UnitService.InitUnitByProjectIdUnitTypeDropDownList(drpUnit, this.CurrUser.LoginProjectId, Const.ProjectUnitType_2, true);
                // 绑定表格
                this.BindGrid();
            }
        }

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT Welder.PersonId, Welder.WelderCode, Welder.PersonName, Welder.UnitId,Welder.Birthday, 
                                      (CASE WHEN Welder.Sex=1 THEN '男' ELSE '女' END) AS Sex,
                                      Welder.IdentityCard, Welder.CertificateCode, Welder.CertificateLimitTime, 
                                      Welder.WelderLevel, Welder.Remark,Unit.UnitName,
                                      (CASE WHEN Welder.IsUsed=1 THEN '是' ELSE '否' END) AS IsUsed
                               FROM SitePerson_Person AS Welder
                               LEFT JOIN Base_Unit AS Unit ON Unit.UnitId = Welder.UnitId WHERE 1=1";

            List<SqlParameter> parms = new List<SqlParameter>();
            strSql += " and Welder.WorkPostId = @WorkPostId";
            parms.Add(new SqlParameter("@WorkPostId", Const.WorkPost_Welder));
            strSql += " and Welder.ProjectId = @ProjectId";
            parms.Add(new SqlParameter("@ProjectId",this.CurrUser.LoginProjectId));
            if (drpUnit.SelectedValue != BLL.Const._Null)
            {
                strSql += " and Welder.UnitId = @UnitId";
                parms.Add(new SqlParameter("@UnitId", drpUnit.SelectedValue));
            }
            if (!string.IsNullOrEmpty(this.txtWelderCode.Text))
            {
                strSql += " and Welder.WelderCode LIKE  @WelderCode";
                parms.Add(new SqlParameter("@WelderCode", "%" + this.txtWelderCode.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtWelderName.Text))
            {
                strSql += " and Welder.PersonName LIKE  @PersonName";
                parms.Add(new SqlParameter("@PersonName", "%" + this.txtWelderName.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = parms.ToArray();
            DataTable dt = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = dt.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, dt);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        protected void Grid1_RowDataBound(object sender, GridRowEventArgs e)
        {
            DataRowView row = e.DataItem as DataRowView;
            if (row["CertificateLimitTime"].ToString() != string.Empty)
            {
                DateTime validity = Convert.ToDateTime(row["CertificateLimitTime"]);
                DateTime nowDate = DateTime.Now;

                if (validity.AddMonths(-1)<nowDate && validity>=nowDate)
                {
                    e.RowCssClass = "color1";
                }
                else if (validity < nowDate)
                {
                    e.RowCssClass = "color3";
                }
            }
        }

        /// <summary>
        /// 改变索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
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
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion
        #endregion

        #region 增加按钮事件
        /// <summary>
        /// 增加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (GetButtonPower(Const.BtnAdd))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("WelderManageEdit.aspx", "新增 - ")));
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
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
                Alert.ShowInTop("请至少选择一条记录", MessageBoxIcon.Warning);
                return;
            }

            ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
            if (GetButtonPower(Const.BtnModify))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("WelderManageEdit.aspx?PersonId={0}", Grid1.SelectedRowID, "编辑 - ")));
            }
            else if (GetButtonPower(Const.BtnSee))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("WelderManageView.aspx?PersonId={0}", Grid1.SelectedRowID, "查看 - ")));
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
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
            if (GetButtonPower(Const.BtnDelete))
            {
                if (Grid1.SelectedRowIndexArray.Length > 0)
                {
                    string strShowNotify = string.Empty;
                    foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                    {
                        string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                        var welder = BLL.WelderService.GetWelderById(rowID);
                        if (welder != null)
                        {
                            string cont = judgementDelete(rowID);
                            if (string.IsNullOrEmpty(cont))
                            {
                                BLL.WelderService.DeleteWelderById(rowID);
                                //BLL.Sys_LogService.AddLog(Const.System_1, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.WelderManageMenuId, Const.BtnDelete, rowID);
                            }
                            else
                            {
                                strShowNotify += "焊工管理" + "：" + welder.WelderCode + cont;
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(strShowNotify))
                    {
                        Alert.ShowInTop(strShowNotify, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        BindGrid();
                        ShowNotify("删除成功！", MessageBoxIcon.Success);
                    }
                }
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }

        #region 判断是否可删除
        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private string judgementDelete(string id)
        {
            string content = string.Empty;
            if (Funs.DB.Project_ProjectUser.FirstOrDefault(x => x.ProjectUserId == id) != null)
            {
                content += "已在【项目焊工】中使用，不能删除！";
            }

            //if (Funs.DB.Pipeline_WeldJoint.FirstOrDefault(x => x.BackingWelderId == id) != null)
            //{
            //    content += "已在【焊接信息】中使用，不能删除！";
            //}

            return content;
        }
        #endregion

        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region 查看按钮
        /// <summary>
        /// 查看按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnView_Click(object sender, EventArgs e)
        {
            if (GetButtonPower(Const.BtnSee))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("WelderManageView.aspx?PersonId={0}", Grid1.SelectedRowID, "查看 - ")));
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 获取性别
        /// </summary>
        /// <param name="sex"></param>
        /// <returns></returns>
        protected string ConvertSex(object sex)
        {
            if (sex != null)
            {
                if (sex.ToString() == "1")
                {
                    return "男";
                }
                else if (sex.ToString() == "2")
                {
                    return "女";
                }
            }
            return null;
        }

        /// <summary>
        /// 获取是否在岗
        /// </summary>
        /// <param name="isOnduty"></param>
        /// <returns></returns>
        protected string ConvertIsOnDuty(object isOnduty)
        {
            if (isOnduty!=null)
            {
                if (Convert.ToBoolean(isOnduty) == true)
                {
                    return "是";
                }
                else if (Convert.ToBoolean(isOnduty) == false)
                {
                    return "否";
                }
            }
            return null;
        }
        #endregion

        #region 打印按钮
        /// <summary>
        /// 打印按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (GetButtonPower(Const.BtnPrint))
            {
                PageContext.RegisterStartupScript(Window3.GetShowReference(String.Format("WelderPrint.aspx?PersonId={0}", Grid1.SelectedRowID, "打印 - ")));
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }
        #endregion

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("焊工信息" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
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
            grid.PageSize = 10000;
            BindGrid();
            this.Grid1.Columns[9].Hidden = true;
            this.Grid1.Columns[10].Hidden = true;
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
                    
                    sb.AppendFormat("<td>{0}</td>", html);
                    
                }
                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }
        #endregion

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private bool GetButtonPower(string button)
        {
            return BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.WelderManageMenuId, button);
        }
        #endregion
    }
}