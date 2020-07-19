using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FineUIPro.Web.Notice
{
    public partial class IssuedNotice : PageBase
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
                Funs.DropDownPageSize(this.SddlPageSize);
                this.SddlPageSize.SelectedValue = SGrid.PageSize.ToString();
                // 绑定表格
                this.SBindGrid();
            }
        }
        #endregion

        #region 绑定数据SBindGrid
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void SBindGrid()
        {
            string strSql = @"SELECT Notice.NoticeId AS SNoticeId,(CASE WHEN Notice.NoticeCode IS NULL THEN  CodeRecords.Code ELSE Notice.NoticeCode END) AS NoticeCode,Notice.NoticeTitle,Notice.MainContent,Notice.CompileDate,Notice.CompileMan,Users.UserName AS CompileManName,Notice.CompileDate,Notice.States "
                          + @" ,Notice.IsRelease,(CASE WHEN IsRelease=1 THEN '已发布' ELSE '未发布' END) AS IsReleaseName,Notice.ReleaseDate,AccessProjectText"
                          + @" ,Notice.ProjectId,(CASE WHEN Notice.ProjectId IS NULL THEN '公司本部' ELSE Project.ProjectName END ) AS ProjectName"
                          + @" ,(CASE WHEN LEN(Notice.AccessProjectText) > 40 THEN SUBSTRING(Notice.AccessProjectText,0,40)+'...' ELSE  AccessProjectText END)  AS SortAccessProjectText"
                          + @" ,(CASE WHEN Notice.States = " + BLL.Const.State_0 + " OR Notice.States IS NULL THEN '待['+OperateUser.UserName+']提交' WHEN Notice.States =  " + BLL.Const.State_2 + " THEN '审核/审批完成' ELSE '待['+OperateUser.UserName+']办理' END) AS  StateName"
                          + @" FROM InformationProject_Notice AS Notice "
                          + @" LEFT JOIN Sys_CodeRecords AS CodeRecords ON Notice.NoticeId=CodeRecords.DataId "
                          + @" LEFT JOIN Base_Project AS Project ON Notice.ProjectId=Project.ProjectId"
                          + @" LEFT JOIN Sys_FlowOperate AS FlowOperate ON Notice.NoticeId=FlowOperate.DataId AND FlowOperate.IsClosed <> 1"
                          + @" LEFT JOIN Sys_User AS OperateUser ON FlowOperate.OperaterId=OperateUser.UserId"
                          + @" LEFT JOIN Sys_User AS Users ON Notice.CompileMan=Users.UserId WHERE IsRelease =1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(Request.Params["projectId"]))  ///是否文件柜查看页面传项目值
            {
                strSql += " AND Notice.ProjectId = @ProjectId";
                listStr.Add(new SqlParameter("@ProjectId", Request.Params["projectId"]));
                strSql += " AND Notice.States = @States";  ///状态为已完成
                listStr.Add(new SqlParameter("@States", BLL.Const.State_2)); 
            }
            else if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
            {
                strSql += " AND Notice.ProjectId = @ProjectId";
                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }
            else
            {                
                strSql += " AND Notice.ProjectId IS NULL";
            }
           
            if (!string.IsNullOrEmpty(this.txtNoticeCode.Text.Trim()))
            {
                strSql += " AND NoticeCode LIKE @NoticeCode";
                listStr.Add(new SqlParameter("@NoticeCode", "%" + this.txtNoticeCode.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtNoticeTitle.Text.Trim()))
            {
                strSql += " AND Notice.NoticeTitle LIKE @NoticeTitle";
                listStr.Add(new SqlParameter("@NoticeTitle", "%" + this.txtNoticeTitle.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            SGrid.RecordCount = tb.Rows.Count;         
            var table = this.GetPagedDataTable(SGrid, tb);
            SGrid.DataSource = table;
            SGrid.DataBind();
        }

        #region 分页 排序
        /// <summary>
        /// 改变索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SGrid_PageIndexChange(object sender, GridPageEventArgs e)
        {
            SBindGrid();
        }

        /// <summary>
        /// 分页下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SGrid.PageSize = Convert.ToInt32(this.SddlPageSize.SelectedValue);
            SBindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SGrid_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            this.SBindGrid();
        }
        #endregion
        #endregion        

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void STextBox_TextChanged(object sender, EventArgs e)
        {
            this.SBindGrid();
            BLL.LogService.AddSys_Log(this.CurrUser, string.Empty, string.Empty, BLL.Const.ServerNoticeMenuId, Const.BtnQuery);
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SGrid_RowDoubleClick(object sender, GridRowClickEventArgs e)
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
            if (SGrid.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string id = SGrid.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("NoticeView.aspx?NoticeId={0}", id, "查看 - ")));
        }
        #endregion
        
        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOutS_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("管理通知(发出)" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.SGrid.PageSize = this.SGrid.RecordCount;
            this.SBindGrid();
            Response.Write(GetGridTableHtml(SGrid));
            Response.End();
            BLL.LogService.AddSys_Log(this.CurrUser, string.Empty, string.Empty, BLL.Const.ServerNoticeMenuId, Const.BtnQuery);
        }
        #endregion
    }
}