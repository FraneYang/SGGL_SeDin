using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.Personal
{
    public partial class PersonDuty : PageBase
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
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                }

                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                this.BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"select DutyId, DutyPersonId, DutyTime, CompilePersonId, CompileTime, D.WorkPostId, ApprovePersonId, ApproveTime,U.UserName,W.WorkPostName ,(CASE WHEN State = 0 THEN '待['+CompileMan.UserName+']提交' WHEN State = 1 THEN '待员工签字' WHEN State = 2 THEN '待['+Approve.UserName+']审核' WHEN State = 3 THEN '完成' END) AS State
              from Person_Duty D 
              left join Sys_User U on D.DutyPersonId=U.UserId 
              Left join Sys_User CompileMan on D.CompilePersonId=CompileMan.UserId
              Left join Sys_User Approve on D.ApprovePersonId=Approve.UserId
              Left Join Base_WorkPost W on  D.WorkPostId=W.WorkPostId Where D.DutyPersonId=@UserId";

            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@UserId", this.CurrUser.UserId));
            if (!string.IsNullOrEmpty(this.txtUserName.Text.Trim()))
            {
                strSql += " AND Users.UserName LIKE @UserName";
                listStr.Add(new SqlParameter("@UserName", "%" + this.txtUserName.Text.Trim() + "%"));
            }
            

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            //tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

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
        

        #region 分页
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
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
            BindGrid();
        }
        #endregion
        #endregion

        #region Grid操作
        
        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }
        protected void btnTemplate_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PersonDutyTemplate.aspx")));
        }

        protected void MenuView_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../Person/PersonDutyView.aspx?DutyId={0}", Grid1.SelectedRowID, "操作 - ")));
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
                Alert.ShowInParent("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string Id = Grid1.SelectedRowID;
            string url = "~/Person/PersonDutyView.aspx?DutyId={0}";
            var PersonDuty = BLL.Person_DutyService.GetPersonDutyById(Id);
            if (PersonDuty.State == "1" && PersonDuty.DutyPersonId == this.CurrUser.UserId)
            {
                url = "~/Person/PersonDutyEdit.aspx?DutyId={0}";
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format(url, Id, "操作 - ")));
        }
        #endregion
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
    }
}