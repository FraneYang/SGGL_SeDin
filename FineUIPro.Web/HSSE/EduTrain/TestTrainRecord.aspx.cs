using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FineUIPro.Web.HSSE.Train
{
    public partial class TestTrainRecord : PageBase
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
                this.BindGrid();
            }
            else
            {
                if (GetRequestEventArgument() == "reloadGrid")
                {
                    BindGrid();
                }
            }
        }
        #endregion

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT Task.TaskId,Task.TaskDate,person.PersonName,TrainingItem.TrainingId,TrainingItem.TrainingItemId,Training.TrainingCode,Training.TrainingName"
                        + @",TrainingItem.TrainingItemCode,TrainingItem.TrainingItemName "
                        + @" FROM Training_Task AS Task"
                        + @" LEFT JOIN Training_PlanItem AS PlanItem ON Task.PlanId =PlanItem.PlanId "
                        + @" LEFT JOIN SitePerson_Person AS person ON Task.UserId =person.PersonId "
                        + @" LEFT JOIN Training_TrainingItem AS TrainingItem ON PlanItem.TrainingEduItemId =TrainingItem.TrainingItemId"
                        + @" LEFT JOIN Training_Training AS Training ON TrainingItem.TrainingId=Training.TrainingId"
                        + @" WHERE Task.UserId=@UserId ";
            List<SqlParameter> listStr = new List<SqlParameter>
            {
                new SqlParameter("@UserId", this.CurrUser.UserId)
            };
            if (!string.IsNullOrEmpty(this.ckStates.SelectedValue) && this.ckStates.SelectedValue != "0")
            {
                if (this.ckStates.SelectedValue == "1")
                {
                    strSql += " AND (Task.States ='0') ";
                }
                else
                {
                    strSql += " AND Task.States ='1' ";
                }
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region 查看
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
        /// 右键编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("TaskRecordView.aspx?TrainingEduItemId={0}", Grid1.SelectedRowID, "编辑 - ")));
        }
        #endregion

        /// <summary>
        /// 状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        #region 分页排序
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
    }
}