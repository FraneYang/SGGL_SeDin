using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.HSSE.EduTrain
{
    public partial class Plan : PageBase
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
                this.GetButtonPower();
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
            string strSql = @"SELECT Plans.PlanId,Plans.PlanCode, Plans.PlanName, Plans.DesignerId, Plans.DesignerDate, Plans.WorkPostId,UnitIds,TrainStartDate,TeachHour,TeachAddress,
                                     (CASE Plans.States WHEN '0' THEN '计划中' WHEN '1' THEN '待考试' WHEN '2' THEN '考试中' WHEN '3' THEN '已结束' WHEN '-1' THEN '已作废' END ) AS States,
                                     Users.UserName AS DesignerName,WorkPostNames"
                             + @" FROM dbo.Training_Plan AS Plans "
                             + @" LEFT JOIN Sys_User AS Users ON Users.UserId = Plans.DesignerId "
                             + @" WHERE Plans.ProjectId='" + this.CurrUser.LoginProjectId + "'";
            List<SqlParameter> listStr = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                strSql += " AND (PlanCode LIKE @name OR PlanName LIKE @name)";
                listStr.Add(new SqlParameter("@name", "%" + this.txtName.Text.Trim() + "%"));
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
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

        #region 编辑
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
        protected void btnMenuView_Click(object sender, EventArgs e)
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
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PlanView.aspx?PlanId={0}", Grid1.SelectedRowID, "编辑 - ")));
        }
        #endregion

        #region 删除
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                string alterString = string.Empty;
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    string content = judgementDelete(rowID);
                    if (string.IsNullOrEmpty(content))
                    {
                        var getV = BLL.TrainingPlanService.GetPlanById(rowID);
                        if (getV != null)
                        {
                            LogService.AddSys_Log(this.CurrUser, getV.PlanCode, rowID, BLL.Const.ProjectTrainingPlanMenuId, BLL.Const.BtnDelete);
                            TrainingTaskService.DeleteTaskByPlanId(rowID);
                            TrainingPlanItemService.DeletePlanItemByPlanId(rowID);
                            TrainingPlanService.DeletePlanById(rowID);
                        }
                    }
                    else
                    {
                        alterString += "第" + (rowIndex + 1) + "行" + content;
                    }
                }
                BindGrid();
                if (string.IsNullOrEmpty(alterString))
                {
                    ShowNotify("删除数据成功!", MessageBoxIcon.Success);
                }
                else
                {
                    Alert.ShowInTop(alterString, MessageBoxIcon.Warning);
                }
            }
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 格式化受伤情况
        /// </summary>
        /// <param name="injury"></param>
        /// <returns></returns>
        protected string ConvertWorkPostName(object workPostIds)
        {
            string name = string.Empty;
            if (workPostIds != null)
            {
                name = BLL.WorkPostService.getWorkPostNamesWorkPostIds(workPostIds);
            }
            return name;
        }

        protected string ConvertUnitName(object unitIds)
        {
            string name = string.Empty;
            if (unitIds != null)
            {
                name = UnitService.getUnitNamesUnitIds(unitIds);
            }
            return name;
        }
        #endregion

        #region 输入框查询事件
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

        #region 获取权限按钮
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ProjectTrainingPlanMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
                }
            }
        }
        #endregion

        #region 判断是否可删除
        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private string judgementDelete(string id)
        {
            string content = string.Empty;
            if (Funs.DB.EduTrain_TrainRecord.FirstOrDefault(x => x.PlanId == id) != null)
            {
                content = "该计划已归档【培训记录】，不能删除！";
            }
            if (Funs.DB.Training_TestPlan.FirstOrDefault(x => x.PlanId == id) != null)
            {
                content = "该计划已做【考试计划】，不能删除！";
            }

            return content;
        }
        #endregion

        /// <summary>
        /// 查看二维码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQR_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请选择一条记录！", MessageBoxIcon.Warning);
                return;
            }

            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("~/Controls/SeeQRImage.aspx?TrainingPlanId={0}&strCode={1}", Grid1.SelectedRowID, "trainingPlan$" + Grid1.SelectedRowID), "二维码查看", 400, 400));
        }
    }
}