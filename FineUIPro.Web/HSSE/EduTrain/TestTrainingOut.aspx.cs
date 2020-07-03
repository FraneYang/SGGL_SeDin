using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FineUIPro.Web.HSSE.EduTrain
{
    public partial class TestTrainingOut : PageBase
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
            string strSql = @"SELECT TrainingItemId,Training.TrainingCode,Training.TrainingName,TrainingItemCode,TrainingItemName,Abstracts,AttachUrl,VersionNum,TestType,WorkPostIds "
                        + @" ,(CASE WHEN TestType = '1' THEN '单选题' WHEN TestType = '2' THEN '多选题' ELSE '判断题' END) AS TestTypeName,WorkPostNames,AItem,BItem,CItem,DItem,EItem "
                        + @" ,Score,Replace(Replace(Replace(Replace(Replace(AnswerItems,'1','A'),'2', 'B'),'3', 'C'),'4', 'D'),'5', 'E') AS AnswerItems "
                        + @" FROM dbo.Training_TestTrainingItem AS Item"
                        + @" LEFT JOIN Training_TestTraining AS Training ON Item.TrainingId=Training.TrainingId"
                        + @" WHERE 1 = 1";
            List<SqlParameter> listStr = new List<SqlParameter>();

            //if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
            //{
            //    strSql += " AND (JobActivityName LIKE @Name OR JobActivityCode LIKE @Name OR JobActivitys.Remark LIKE @Name OR WorkAreas.WorkAreaName LIKE @Name OR Installation.InstallationName LIKE @Name)";
            //    listStr.Add(new SqlParameter("@Name", "%" + this.txtName.Text.Trim() + "%"));
            //}      

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
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

        #region 导出按钮
        /// <summary>
        /// 导出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("考试试题" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = Grid1.RecordCount;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion
    }
}