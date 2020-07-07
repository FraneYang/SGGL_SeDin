using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.HSSE.EduTrain
{
    public partial class TestPlan : PageBase
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
            string strSql = @"SELECT TestPlan.TestPlanId,TestPlan.PlanName,TestPlan.PlanCode,TestPlan.PlanManId,PlanMan.UserName AS PlanManName,TestPlan.PlanDate,TestPlan.TestStartTime,TestPlan.TestEndTime,TestPlan.Duration,TestPlan.TotalScore,TestPlan.QuestionCount,TestPlan.TestPalce,TestPlan.UnitIds,TestPlan.UnitNames,TestPlan.DepartIds,TestPlan.DepartNames,TestPlan.WorkPostIds,TestPlan.WorkPostNames,TestPlan.QRCodeUrl,TestPlan.States"
                          + @" ,(CASE WHEN TestPlan.States='1' THEN '已发布未考试'  WHEN TestPlan.States='2' THEN '考试中' WHEN TestPlan.States='3' THEN '考试结束' WHEN TestPlan.States='-1' THEN '已作废' ELSE '待提交' END) AS StatesName"
                          + @" FROM dbo.Training_TestPlan AS TestPlan"
                          + @" LEFT JOIN Sys_User AS PlanMan ON TestPlan.PlanManId= PlanMan.UserId "
                          + @" WHERE TestPlan.ProjectId='" + this.CurrUser.LoginProjectId+"'";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                strSql += " AND (TestPlan.PlanName LIKE @name OR TestPlan.PlanCode LIKE @name OR PlanMan.UserName LIKE @name OR TestPlan.TestPalce LIKE @name OR TestPlan.WorkPostNames LIKE @name)";
                listStr.Add(new SqlParameter("@name", "%" + this.txtName.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.ckStates.SelectedValue) && this.ckStates.SelectedValue != "-2")
            {
                if (this.ckStates.SelectedValue == "1")
                {
                    strSql += " AND TestPlan.States !='0' AND TestPlan.States !='-1' AND TestPlan.States IS NOT NULL";
                }
                else
                {
                    strSql += " AND TestPlan.States = @States";
                    listStr.Add(new SqlParameter("@States", this.ckStates.SelectedValue));
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
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("TestPlanView.aspx?TestPlanId={0}", Grid1.SelectedRowID, "编辑 - ")));
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
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                string alterString = string.Empty;
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    string content = judgementDelete(rowID);
                    if (string.IsNullOrEmpty(content))
                    {
                        var getV = TestPlanService.GetTestPlanById(rowID);
                        if (getV != null)
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, getV.PlanCode, rowID, BLL.Const.ProjectTestPlanMenuId, BLL.Const.BtnDelete);
                            BLL.TestPlanService.DeleteTestPlanById(rowID);                            
                        }
                        else
                        {
                            alterString += "第" + (rowIndex + 1) + "行" + content;
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ProjectTestPlanMenuId);
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
            if (Funs.DB.Training_TestRecord.FirstOrDefault(x => x.TestPlanId == id) != null)
            {
                content = "该计划已存在【考试记录】，不能删除！";
            }

            return content;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strValue"></param>
        private void ShowQurl(string strValue, string title)
        {
            string urlName = strValue.Replace("$", "");
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("~/Controls/ShowQRImage.aspx?strValue={0}&urlName={1}&title={2}", strValue, urlName, System.Web.HttpUtility.UrlEncode(title), "查看 - "), "二维码查看", 340, 400));
        }

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

            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("~/Controls/SeeQRImage.aspx?TestPlanId={0}&strCode={1}", Grid1.SelectedRowID, "testPlan$" + Grid1.SelectedRowID), "二维码查看", 400, 400));
        }
    }
}