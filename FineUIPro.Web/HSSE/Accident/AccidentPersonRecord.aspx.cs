using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.HSSE.Accident
{
    public partial class AccidentPersonRecord : PageBase
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
                ////权限按钮方法
                this.GetButtonPower();
                this.btnNew.OnClientClick = Window1.GetShowReference("AccidentPersonRecordEdit.aspx") + "return false;";
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
            string strSql = @"SELECT AccidentPersonRecord.AccidentPersonRecordId,"
                          + @"AccidentPersonRecord.ProjectId,"
                          + @"AccidentPersonRecord.AccidentTypeId,"
                          + @"AccidentPersonRecord.WorkAreaId,"
                          + @"AccidentPersonRecord.AccidentDate,"
                          + @"AccidentPersonRecord.PersonId,"
                          + @"AccidentPersonRecord.Injury,"
                          + @"AccidentPersonRecord.InjuryPart,"
                          + @"AccidentPersonRecord.HssePersons,"
                          + @"AccidentPersonRecord.InjuryResult,"
                          + @"AccidentPersonRecord.PreventiveAction,"
                          + @"AccidentPersonRecord.HandleOpinion,"
                          + @"AccidentPersonRecord.FileContent,"
                          + @"AccidentPersonRecord.CompileMan,"
                          + @"AccidentPersonRecord.CompileDate,"
                          + @"AccidentPersonRecord.States,"
                          + @"AccidentType.AccidentTypeName,"
                          + @"WorkArea. UnitWorkName AS WorkAreaName,"
                          + @"Person.PersonName,"
                          + @"Person.CardNo,"
                          + @"Person.IdentityCard,"
                          + @"WorkPost.WorkPostName,"
                          + @"(CASE WHEN AccidentPersonRecord.States = " + BLL.Const.State_0 + " OR AccidentPersonRecord.States IS NULL THEN '待['+OperateUser.UserName+']提交' WHEN AccidentPersonRecord.States =  " + BLL.Const.State_2 + " THEN '审核/审批完成' ELSE '待['+OperateUser.UserName+']办理' END) AS  FlowOperateName"
                          + @" FROM Accident_AccidentPersonRecord AS AccidentPersonRecord "
                          + @" LEFT JOIN Base_AccidentType AS AccidentType ON AccidentType.AccidentTypeId = AccidentPersonRecord.AccidentTypeId "
                          + @" LEFT JOIN WBS_UnitWork AS WorkArea ON WorkArea.UnitWorkId = AccidentPersonRecord.WorkAreaId "
                          + @" LEFT JOIN SitePerson_Person AS Person ON Person.PersonId = AccidentPersonRecord.PersonId "
                          + @" LEFT JOIN Base_WorkPost AS WorkPost ON WorkPost.WorkPostId = Person.WorkPostId "
                          + @" LEFT JOIN Sys_FlowOperate AS FlowOperate ON AccidentPersonRecord.AccidentPersonRecordId = FlowOperate.DataId AND FlowOperate.IsClosed <> 1"
                          + @" LEFT JOIN Sys_User AS OperateUser ON FlowOperate.OperaterId = OperateUser.UserId"
                          + @" LEFT JOIN Sys_User AS Users ON AccidentPersonRecord.CompileMan = Users.UserId WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND AccidentPersonRecord.ProjectId = @ProjectId";
            if (!string.IsNullOrEmpty(Request.Params["projectId"]))  ///是否文件柜查看页面传项目值
            {
                listStr.Add(new SqlParameter("@ProjectId", Request.Params["projectId"]));
                strSql += " AND AccidentPersonRecord.States = @States";  ///状态为已完成
                listStr.Add(new SqlParameter("@States", BLL.Const.State_2));
            }
            else
            {
                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }

            if (!string.IsNullOrEmpty(this.txtAccidentTypeName.Text.Trim()))
            {
                strSql += " AND AccidentTypeName LIKE @AccidentTypeName";
                listStr.Add(new SqlParameter("@AccidentTypeName", "%" + this.txtAccidentTypeName.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtStartDate.Text.Trim()))
            {
                strSql += " AND AccidentPersonRecord.AccidentDate >= @StartDate";
                listStr.Add(new SqlParameter("@StartDate", this.txtStartDate.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
            {
                strSql += " AND AccidentPersonRecord.AccidentDate <= @EndDate";
                listStr.Add(new SqlParameter("@EndDate", this.txtEndDate.Text.Trim()));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
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
            this.Grid1.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtStartDate.Text.Trim()) && !string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
            {
                if (Convert.ToDateTime(this.txtStartDate.Text.Trim()) > Convert.ToDateTime(this.txtEndDate.Text.Trim()))
                {
                    Alert.ShowInTop("开始时间不能大于结束时间", MessageBoxIcon.Warning);
                    return;
                }
            }
            this.BindGrid();
            BLL.LogService.AddSys_Log(this.CurrUser, string.Empty, string.Empty, BLL.Const.ProjectAccidentPersonRecordMenuId, BLL.Const.BtnQuery);
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
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string id = Grid1.SelectedRowID;
            var accidentPersonRecord = BLL.AccidentPersonRecordService.GetAccidentPersonRecordById(id);
            if (accidentPersonRecord != null)
            {
                if (this.btnMenuEdit.Hidden || accidentPersonRecord.States == BLL.Const.State_2)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("AccidentPersonRecordView.aspx?AccidentPersonRecordId={0}", id, "查看 - ")));
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("AccidentPersonRecordEdit.aspx?AccidentPersonRecordId={0}", id, "编辑 - ")));
                }
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
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var getV = BLL.AccidentPersonRecordService.GetAccidentPersonRecordById(rowID);
                    if (getV != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, string.Empty, getV.AccidentPersonRecordId, BLL.Const.ProjectAccidentPersonRecordMenuId, BLL.Const.BtnDelete);
                        BLL.AccidentPersonRecordService.DeleteAccidentPersonRecordById(rowID);
                    }
                }

                this.BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 格式化受伤情况
        /// </summary>
        /// <param name="injury"></param>
        /// <returns></returns>
        protected string ConvertInjury(object injury)
        {
            if (injury != null)
            {
                if (injury.ToString() == "1")
                {
                    return "死亡";
                }
                else if (injury.ToString() == "2")
                {
                    return "重伤";
                }
                else if (injury.ToString() == "3")
                {
                    return "轻伤";
                }
            }
            return "";
        }
        #endregion

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ProjectAccidentPersonRecordMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
                }
            }
        }
        #endregion
    }
}