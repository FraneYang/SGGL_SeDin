
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace FineUIPro.Web.HJGL.PersonManage
{
    public partial class CheckerManage : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                BLL.UnitService.InitUnitByProjectIdUnitTypeDropDownList(drpUnit, this.CurrUser.LoginProjectId, Const.ProjectUnitType_2, true);
                BindGrid();
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid() {
            string sql = "select P.PersonId,P.WelderCode,P.PersonName,(case when P.Sex=1 then '男' else '女' end)As Sex,P.Birthday,P.IdentityCard,(case when P.IsUsed = 1 then '是' else '否' end)As IsUsed, B.UnitName from SitePerson_Person As P left join Base_Unit As B on P.UnitId = B.UnitId where 1=1";
            List<SqlParameter> parms = new List<SqlParameter>();
            sql += " and P.WorkPostId = @WorkPostId";
            parms.Add(new SqlParameter("@WorkPostId", Const.WorkPost_Checker));
            sql += " and P.ProjectId = @ProjectId";
            parms.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            if (drpUnit.SelectedValue != BLL.Const._Null)
            {
                sql += " and P.UnitId = @UnitId";
                parms.Add(new SqlParameter("@UnitId", drpUnit.SelectedValue));
            }
            if (!string.IsNullOrEmpty(this.txtCheckerCode.Text))
            {
                sql += " and P.WelderCode LIKE  @WelderCode";
                parms.Add(new SqlParameter("@WelderCode", "%" + this.txtCheckerCode.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtCHeckerName.Text))
            {
                sql += " and P.PersonName LIKE  @PersonName";
                parms.Add(new SqlParameter("@PersonName", "%" + this.txtCHeckerName.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = parms.ToArray();
            DataTable dt = SQLHelper.GetDataTableRunText(sql, parameter);
            Grid1.RecordCount = dt.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, dt);
            this.Grid1.DataSource = table;
            this.Grid1.DataBind();
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
        /// 右键编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            EditData();
        }
        /// <summary>
        /// 右键删除
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
                        var Checker = BLL.CheckerService.GetCheckerById(rowID);
                        if (Checker != null)
                        {
                            BLL.CheckerService.DeleteCheckerById(rowID);
                                //BLL.Sys_LogService.AddLog(Const.System_1, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.WelderManageMenuId, Const.BtnDelete, rowID);
                           
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
        
        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnView_Click(object sender, EventArgs e)
        {
            if (GetButtonPower(Const.BtnSee))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckerManageView.aspx?CheckerId={0}", Grid1.SelectedRowID, "查看 - ")));
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }
        /// <summary>
        /// 更改索引
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }
        /// <summary>
        /// 双击表单编辑行数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            EditData();
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
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckerManageEdit.aspx?CheckerId={0}", Grid1.SelectedRowID, "编辑 - ")));
            }
            else if (GetButtonPower(Const.BtnSee))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckerManageView.aspx?CheckerId={0}", Grid1.SelectedRowID, "查看 - ")));
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (GetButtonPower(Const.BtnAdd))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckerManageEdit.aspx", "新增 - ")));
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }
        /// <summary>
        /// 选择分页数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("无损检测工信息" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        /// <summary>
        /// 导出excel表格
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
        {
            StringBuilder sb = new StringBuilder();
            grid.PageSize = 10000;
            BindGrid();
            this.Grid1.Columns[5].Hidden = true;
            this.Grid1.Columns[6].Hidden = true;
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

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CheckerQualification")
            {
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("CheckerItem.aspx?PersonId={0}", Grid1.SelectedRowID, "资质 - ")));
            }
        }
    }
}