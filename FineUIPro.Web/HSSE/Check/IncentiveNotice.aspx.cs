using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.HSSE.Check
{
    public partial class IncentiveNotice :PageBase
    {
        #region 项目主键
        /// <summary>
        /// 项目主键
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.CurrUser.LoginProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                BLL.UnitService.InitUnitDropDownList(this.drpUnitId, this.ProjectId, true);
                if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
                {
                    this.drpUnitId.SelectedValue = this.CurrUser.UnitId;
                    this.drpUnitId.Enabled = false;
                }
                ////权限按钮方法
                this.GetButtonPower();
                this.btnNew.OnClientClick = Window1.GetShowReference("IncentiveNoticeAdd.aspx") + "return false;";
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
            string strSql = @"SELECT IncentiveNotice.IncentiveNoticeId,"
                          + @"IncentiveNotice.ProjectId,"
                          + @"CodeRecords.Code AS IncentiveNoticeCode,"
                          + @"IncentiveNotice.UnitId,"
                          + @"IncentiveNotice.IncentiveDate,"
                          + @"IncentiveNotice.PersonId,"
                          + @"IncentiveNotice.BasicItem,"
                          + @"IncentiveNotice.IncentiveMoney,ISNULL(IncentiveNotice.Currency,'人民币') AS Currency,"
                          + @"IncentiveNotice.TitleReward,"
                          + @"IncentiveNotice.MattleReward,"
                          + @"IncentiveNotice.FileContents,"
                          + @"IncentiveNotice.AttachUrl,"
                          + @"IncentiveNotice.CompileMan,"
                          + @"IncentiveNotice.CompileDate,"
                          + @"Sign.UserName AS SignMan,"
                          + @"ApproveMan.UserName AS ApproveMan,"
                          + @"RewardType.ConstText AS RewardTypeName,"
                          + @"IncentiveNotice.States,"
                          + @"Unit.UnitName,"
                          +@"Person.PersonName,"
                          + @"(CASE WHEN IncentiveNotice.States = '0' THEN '待[' + Users.UserName + ']提交' WHEN IncentiveNotice.States = '1' THEN '待[' + Sign.UserName + ']签发'  WHEN IncentiveNotice.States = '2' THEN '待[' + Approve.UserName + ']批准'  WHEN IncentiveNotice.States = '3' THEN '已完成' END) AS  FlowOperateName"
                          + @" FROM Check_IncentiveNotice AS IncentiveNotice "
                          + @" LEFT JOIN Sys_CodeRecords AS CodeRecords ON IncentiveNotice.IncentiveNoticeId = CodeRecords.DataId "
                          + @" LEFT JOIN Sys_FlowOperate AS FlowOperate ON IncentiveNotice.IncentiveNoticeId = FlowOperate.DataId AND FlowOperate.IsClosed <> 1"
                          + @" LEFT JOIN Sys_User AS OperateUser ON FlowOperate.OperaterId = OperateUser.UserId"
                          + @" LEFT JOIN Base_Unit AS Unit ON Unit.UnitId = IncentiveNotice.UnitId "
                          + @" LEFT JOIN SitePerson_Person AS Person ON Person.PersonId = IncentiveNotice.PersonId "
                          + @" LEFT JOIN Sys_Const AS RewardType ON RewardType.ConstValue = IncentiveNotice.RewardType and RewardType.GroupId='RewardType'"
                          + @" LEFT JOIN Sys_User AS Sign ON Sign.UserId = IncentiveNotice.SignMan "
                          + @" LEFT JOIN Sys_User AS Approve ON Approve.UserId = IncentiveNotice.ApproveMan"
                          + @" LEFT JOIN Sys_User AS Duty ON Duty.UserId = IncentiveNotice.DutyPersonId"
                          + @" LEFT JOIN Sys_User AS ApproveMan ON ApproveMan.UserId = IncentiveNotice.ApproveMan "
                          + @" LEFT JOIN Sys_User AS Users ON IncentiveNotice.CompileMan = Users.UserId WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND IncentiveNotice.ProjectId = @ProjectId";
            if (!string.IsNullOrEmpty(Request.Params["projectId"]))  ///是否文件柜查看页面传项目值
            {
                listStr.Add(new SqlParameter("@ProjectId", Request.Params["projectId"]));
                strSql += " AND IncentiveNotice.States = @States";  ///状态为已完成
                listStr.Add(new SqlParameter("@States", BLL.Const.State_3));
            }
            else
            {
                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }
            if (this.rbStates.SelectedValue != "-1")
            {
                strSql += " AND IncentiveNotice.States =@States";
                listStr.Add(new SqlParameter("@States", this.rbStates.SelectedValue));
            }
            /// 施工分包 只看到自己已完成的奖励单
            //if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
            //{
            //    strSql += " AND IncentiveNotice.UnitId = @UnitId";  ///状态为已完成
            //    listStr.Add(new SqlParameter("@UnitId", this.CurrUser.UnitId));

            //    strSql += " AND IncentiveNotice.States = @States";  ///状态为已完成
            //    listStr.Add(new SqlParameter("@States", BLL.Const.State_2));
            //}
            if (!string.IsNullOrEmpty(this.txtIncentiveNoticeCode.Text.Trim()))
            {
                strSql += " AND IncentiveNoticeCode LIKE @IncentiveNoticeCode";
                listStr.Add(new SqlParameter("@IncentiveNoticeCode", "%" + this.txtIncentiveNoticeCode.Text.Trim() + "%"));
            }
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND IncentiveNotice.UnitId = @UnitId2";
                listStr.Add(new SqlParameter("@UnitId2", this.drpUnitId.SelectedValue.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtStartDate.Text.Trim()))
            {
                strSql += " AND IncentiveNotice.IncentiveDate >= @StartDate";
                listStr.Add(new SqlParameter("@StartDate", this.txtStartDate.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
            {
                strSql += " AND IncentiveNotice.IncentiveDate <= @EndDate";
                listStr.Add(new SqlParameter("@EndDate", this.txtEndDate.Text.Trim()));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        #region 分页 排序
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
        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string id = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("IncentiveNoticeView.aspx?IncentiveNoticeId={0}", id, "操作 - ")));
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
            var incentiveNotice = BLL.IncentiveNoticeService.GetIncentiveNoticeById(id);
            if (incentiveNotice != null)
            {
                string url = "IncentiveNoticeView.aspx?IncentiveNoticeId={0}";
                if (incentiveNotice.States == BLL.Const.State_0 && incentiveNotice.CompileMan == this.CurrUser.UserId)
                {
                    url = "IncentiveNoticeAdd.aspx?incentiveNoticeId={0}";
                }
                else if (incentiveNotice.States == BLL.Const.State_1 && incentiveNotice.SignMan == this.CurrUser.UserId)
                {
                    url = "IncentiveNoticeEdit.aspx?IncentiveNoticeId={0}";
                }
                else if (incentiveNotice.States == BLL.Const.State_2 && incentiveNotice.ApproveMan == this.CurrUser.UserId)
                {
                    url = "IncentiveNoticeEdit.aspx?IncentiveNoticeId={0}";
                }
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format(url, id, "操作 - ")));
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
                    var incentiveNotice = BLL.IncentiveNoticeService.GetIncentiveNoticeById(rowID);
                    if (incentiveNotice != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, incentiveNotice.IncentiveNoticeCode, incentiveNotice.IncentiveNoticeId, BLL.Const.ProjectIncentiveNoticeMenuId, BLL.Const.BtnDelete);
                        BLL.IncentiveNoticeService.DeleteIncentiveNoticeById(rowID);
                    }
                }

                this.BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ProjectIncentiveNoticeMenuId);
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

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("奖励通知单" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = this.Grid1.RecordCount;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

        #endregion
    }
}