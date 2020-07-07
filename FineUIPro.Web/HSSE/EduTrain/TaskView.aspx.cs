using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FineUIPro.Web.HSSE.EduTrain
{
    public partial class TaskView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string TaskId
        {
            get
            {
                return (string)ViewState["TaskId"];
            }
            set
            {
                ViewState["TaskId"] = value;
            }
        }
        #endregion

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
                this.TaskId = Request.Params["TaskId"];
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                this.BindGrid();

            }
        }
        #endregion

        #region 分页、关闭窗口
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

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            var task = BLL.TrainingTaskService.GetTaskById(this.TaskId);
            if (task != null)
            {
                string strSql = @"SELECT PlanItem.PlanItemId,PlanItem.PlanId,PlanItem.TrainingEduId,Training.TrainingCode,Training.TrainingId,Training.TrainingName"
                                + @",TrainingItem.TrainingItemId,TrainingItem.TrainingItemCode,TrainingItem.TrainingItemName"
                                + @" FROM Training_PlanItem AS PlanItem"
                                + @" LEFT JOIN Training_TrainingItem AS TrainingItem ON PlanItem.TrainingEduItemId =TrainingItem.TrainingItemId"
                                + @" LEFT JOIN Training_Training AS Training ON TrainingItem.TrainingId =Training.TrainingId"
                                + @" WHERE PlanItem.PlanId=@PlanId ";
                List<SqlParameter> listStr = new List<SqlParameter>();
                listStr.Add(new SqlParameter("@PlanId", task.PlanId));
                //var user = BLL.UserService.GetUserByUserId(task.UserId);
                //if (user != null && !string.IsNullOrEmpty(user.InstallationId))
                //{
                //    List<string> installs = Funs.GetStrListByStr(user.InstallationId, ',');
                //    if (installs.Count > 0)
                //    {
                //        int i = 0;
                //        foreach (var item in installs)
                //        {
                //            strSql += " AND (TrainingEduItem.InstallationIds LIKE @InstallationId" + i.ToString() + " OR TrainingEduItem.InstallationIds IS NULL)";
                //            listStr.Add(new SqlParameter("@InstallationId" + i.ToString(), item));
                //            i++;
                //        }
                //    }
                //}

                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
                Grid1.RecordCount = tb.Rows.Count;
                var table = this.GetPagedDataTable(Grid1, tb);
                Grid1.DataSource = table;
                Grid1.DataBind();
            }
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
    }
}