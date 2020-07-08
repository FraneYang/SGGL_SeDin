using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.PersonManage.Test
{
    public partial class TestRanking : PageBase
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
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                ServerTestPlanService.InitTestPlanDropDownList(this.drpPlan, false);
                //this.drpPlan.SelectedIndex = 0;
                UnitService.InitUnitDropDownList(this.drpUnit, string.Empty, true);
                DepartService.InitDepartDropDownList(this.drpDepart, true);
                ProjectService.InitProjectDropDownList(this.drpProject, true);
                // 绑定表格
                BindGrid();
                GetDataService.UpdateServerTestPlanStates();
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
            string strSql = @"SELECT T.TestRecordId,T.TestPlanId,P.PlanCode,P.PlanName,T.TestManId,T.ManType,T.TestManName,DATEDIFF(SS,T.TestStartTime,T.TestEndTime) AS UserTimesD,(CONVERT(VARCHAR(10),DATEDIFF(SS,T.TestStartTime,T.TestEndTime)/3600)+'时'+CONVERT(VARCHAR(10),DATEDIFF(SS,T.TestStartTime,T.TestEndTime)%3600/60)+'分'+CONVERT(VARCHAR(10),DATEDIFF(SS,T.TestStartTime,T.TestEndTime)%3600%60)+'秒')  AS UseTimes "
                                    + @"  ,(CASE WHEN T.ManType='1' THEN '管理人员' WHEN T.ManType='2' THEN '临时用户' ELSE '作业人员' END) AS ManTypeName"
                                    + @"  ,T.TestStartTime,T.TotalScore,T.QuestionCount,T.Duration,T.TestEndTime,T.TestScores,T.UnitId,Unit.UnitCode"
                                    + @", Unit.UnitName,T.DepartId,Depart.DepartCode,Depart.DepartName,T.ProjectId,Project.ProjectName,T.WorkPostId,T.IdentityCard,T.Telephone"
                                    + @",(CASE WHEN T.UnitId='" +Const.UnitId_SEDIN+ "' THEN Depart.DepartName ELSE Project.ProjectName END) AS DProjectName"
                                    + @" FROM Test_TestRecord AS T "
                                    + @" LEFT JOIN Test_TestPlan AS P ON T.TestPlanId=P.TestPlanId"
                                    + @" LEFT JOIN Base_Unit AS Unit ON T.UnitId=Unit.UnitId"
                                    + @" LEFT JOIN Base_Depart AS Depart ON T.DepartId=Depart.DepartId"
                                    + @" LEFT JOIN Base_Project AS Project ON T.ProjectId=Project.ProjectId"
                                    + @" WHERE 1=1";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                strSql += " AND (P.PlanName LIKE @name OR T.TestManName LIKE @name)";
                listStr.Add(new SqlParameter("@name", "%" + this.txtName.Text.Trim() + "%"));
            }
            if (this.drpPlan.SelectedValue != Const._Null && !string.IsNullOrEmpty(this.drpPlan.SelectedValue))
            {
                strSql += " AND (T.TestPlanId =@TestPlanId)";
                listStr.Add(new SqlParameter("@TestPlanId", this.drpPlan.SelectedValue));
            }

            if (this.cblUserType.SelectedValueArray.Length == 1)
            {
                strSql += " AND (T.ManType =@ManType )";
                listStr.Add(new SqlParameter("@ManType", this.cblUserType.SelectedValueArray.FirstOrDefault()));
            }
            else if (this.cblUserType.SelectedValueArray.Length == 2)
            {
                int i = 0;
                strSql += " AND (";
                foreach (var item in this.cblUserType.SelectedValueArray)
                {
                    if (i == 0)
                    {
                        strSql += " T.ManType =@ManType" + i;
                    }
                    else
                    {
                        strSql += " OR T.ManType =@ManType" + i;
                    }
                    listStr.Add(new SqlParameter("@ManType" + i, item));
                    i++;
                }
                strSql += " )";
            }
            if (this.drpUnit.SelectedValue != Const._Null && !string.IsNullOrEmpty(this.drpUnit.SelectedValue))
            {
                strSql += " AND (T.UnitId =@UnitId)";
                listStr.Add(new SqlParameter("@UnitId", this.drpUnit.SelectedValue));
            }
            if (this.drpDepart.SelectedValue != Const._Null && !string.IsNullOrEmpty(this.drpDepart.SelectedValue))
            {
                strSql += " AND (T.DepartId =@DepartId)";
                listStr.Add(new SqlParameter("@DepartId", this.drpDepart.SelectedValue));
            }
            if (this.drpProject.SelectedValue != Const._Null && !string.IsNullOrEmpty(this.drpProject.SelectedValue))
            {
                strSql += " AND (T.ProjectId =@ProjectId)";
                listStr.Add(new SqlParameter("@ProjectId", this.drpProject.SelectedValue));
            }
            if (this.isNoT.Checked)
            {
                strSql += " AND (T.UnitId !=@UnitIdT)";
                listStr.Add(new SqlParameter("@UnitIdT", Const.UnitId_SEDIN));
            }
            strSql += " ORDER BY T.TestScores DESC,DATEDIFF(SS,T.TestStartTime,T.TestEndTime) ASC";
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                string testRecordId = Grid1.Rows[i].DataKeys[0].ToString();
                var testRecord = BLL.TestRecordService.GetTestRecordById(testRecordId);
                if (testRecord != null)
                {
                    if (testRecord.TestScores < SysConstSetService.getPassScore())
                    {
                        Grid1.Rows[i].RowCssClass = "Red";
                    }
                }
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
            BindGrid();
        }

        /// <summary>
        /// 分页显示条数下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize =Convert.ToInt32( this.ddlPageSize.SelectedValue);
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
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("~/HSSE/EduTrain/TestRecordItem.aspx?TestRecordId={0}&type=1", Grid1.SelectedRowID, "编辑 - "), "考试试卷", 1200, 600));
        }
        #endregion
              
        #region 查询事件
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        protected void isNoT_CheckedChanged(object sender, CheckedEventArgs e)
        {
            this.BindGrid();
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("成绩排名" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = Grid1.RecordCount;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("~/HSSE/EduTrain/TestRecordPrint.aspx?TestRecordId={0}&type=1", Grid1.SelectedRowID, "编辑 - "), "考试试卷", 900, 600));
        }
    }
}