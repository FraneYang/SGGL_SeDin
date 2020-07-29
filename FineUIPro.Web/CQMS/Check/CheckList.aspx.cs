using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FineUIPro.Web.CQMS.Check
{
    public partial class CheckList : PageBase
    {
        /// <summary>
        /// 项目id
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
        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            // 表头过滤
            //FilterDataRowItem = FilterDataRowItemImplement;
            if (!IsPostBack)
            {
                this.ProjectId = this.CurrUser.LoginProjectId;
                GetButtonPower();
                //if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.ProjectId)
                //{
                //    this.ProjectId = Request.Params["projectId"];
                //}
                //权限按钮方法
                UnitService.InitUnitByProjectIdUnitTypeDropDownList(drpSponsorUnit, this.CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_2, true);
                UnitWorkService.InitUnitWorkDownList(drpUnitWork, this.CurrUser.LoginProjectId, true);
                CNProfessionalService.InitCNProfessionalDownList(drpCNProfessional, true);
                QualityQuestionTypeService.InitQualityQuestionTypeDownList(drpQuestionType,true);
                Funs.FineUIPleaseSelect(this.dpHandelStatus);
                btnNew.OnClientClick = Window1.GetShowReference("ChecklistEdit.aspx") + "return false;";
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();

                // 绑定表格
                BindGrid();



            }


        }
        #endregion
        //public System.Web.UI.WebControls.ListItem[] GetHandelStatus()
        //{
        //    var list = Handelstatus();
        //    System.Web.UI.WebControls.ListItem[]  litem = new System.Web.UI.WebControls.ListItem[list.Count];

        //    for (int i = 0; i < list.Count; i++)
        //    {

        //        litem[i]= new System.Web.UI.WebControls.ListItem(list.Keys.ToString(),list.Values.ToString());

        //     }      
        //    return litem;
        //}
        /// <summary>
        /// 整改状态
        /// </summary>
        /// <returns></returns>
        protected IDictionary<int, string> Handelstatus()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            dic.Add(1, "未确认");
            dic.Add(2, "已闭环");
            dic.Add(3, "超期未整改");
            dic.Add(4, "未整改");
            return dic;
        }
        public Task<DataTable> data()
        {
            Task<DataTable> task = new Task<DataTable>(() =>
            {
                return ChecklistData();
            });
            task.Start();
            return task;
        }
        protected DataTable ChecklistData()
        {
            string strSql = @"SELECT chec.CheckControlCode,chec.CheckSite,chec.ProjectId,chec.unitId,cNProfessional.ProfessionalName,"
                          + @" QualityQuestionType.QualityQuestionType as QuestionType,"
                          + @" chec.checkman,chec.CheckDate,chec.DocCode,chec.submitman,chec.state,chec.CNProfessionalCode,"
                          + @" unit.UnitName,unitWork.UnitWorkName,u.userName "
                          + @" FROM Check_CheckControl chec"
                          + @" left join Base_Unit unit on unit.unitId=chec.unitId"
                          + @" left join Base_CNProfessional cNProfessional on cNProfessional.CNProfessionalId=chec.CNProfessionalCode"
                          + @" left join WBS_UnitWork unitWork on unitWork.UnitWorkId = chec.UnitWorkId"
                          + @" left join Base_QualityQuestionType QualityQuestionType on QualityQuestionType.QualityQuestionTypeId = chec.QuestionType"
                          + @" left join sys_User u on u.userId = chec.CheckMan"
                          + @" where chec.ProjectId=@ProjectId";

            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            strSql += " AND (chec.CheckDate>=@startTime or @startTime='') and (chec.CheckDate<=@endTime or @endTime='') ";
            listStr.Add(new SqlParameter("@startTime", !string.IsNullOrEmpty(txtStartTime.Text.Trim()) ? txtStartTime.Text.Trim() + " 00:00:00" : ""));
            listStr.Add(new SqlParameter("@endTime", !string.IsNullOrEmpty(txtEndTime.Text.Trim()) ? txtEndTime.Text.Trim() + "  23:59:59" : ""));
            if (drpSponsorUnit.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND chec.unitId=@unitId";
                listStr.Add(new SqlParameter("@unitId", drpSponsorUnit.SelectedValue));
            }
            if (drpUnitWork.SelectedValue != Const._Null)
            {
                strSql += " AND chec.unitworkId=@unitworkId";
                listStr.Add(new SqlParameter("@unitworkId", drpUnitWork.SelectedValue));
            }
            if (drpCNProfessional.SelectedValue != Const._Null)
            {
                strSql += " AND chec.CNProfessionalCode=@CNProfessionalCode";
                listStr.Add(new SqlParameter("@CNProfessionalCode", drpCNProfessional.SelectedValue));
            }
            if (drpQuestionType.SelectedValue != Const._Null)
            {
                strSql += " AND chec.QuestionType=@QuestionType";
                listStr.Add(new SqlParameter("@QuestionType", drpQuestionType.SelectedValue));
            }
            if (dpHandelStatus.SelectedValue != Const._Null)
            {
                if (dpHandelStatus.SelectedValue.Equals("1"))
                {
                    strSql += " AND (chec.state='5' or chec.state='6')";
                }
                else if (dpHandelStatus.SelectedValue.Equals("2"))
                {
                    strSql += " AND chec.state='7'";
                }
                else if (dpHandelStatus.SelectedValue.Equals("3"))
                {
                    strSql += " AND DATEADD(day,1,chec.LimitDate)< GETDATE() and chec.state<>5 and chec.state<>6 and chec.state<>7";
                }
                else if (dpHandelStatus.SelectedValue.Equals("4"))
                {
                    strSql += " AND DATEADD(day,1,chec.LimitDate)> GETDATE() and chec.state<>5 and chec.state<>6 and chec.state<>7";
                }
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            return tb;
        }

        public string Convertstatus(Object code)
        {
            Model.Check_CheckControl checkControl = BLL.CheckControlService.GetCheckControl(code.ToString());
            if (checkControl.State.Equals("5") || checkControl.State.Equals("6"))
            {
                return "未确认";
            }
            else if (checkControl.State == Const.CheckControl_Complete)
            { //闭环
                return "已闭环";
            }
            //else if( checkControl.LimitDate> )
            else if (Convert.ToDateTime(checkControl.LimitDate).AddDays(1) < DateTime.Now)  //延期未整改
            {
                return "超期未整改";

            }
            else  //期内未整改
            {
                return "未整改";

            }
        }


        /// <summary>
        /// 绑定数据
        /// </summary>

        public void BindGrid()
        {
            DataTable tb = ChecklistData();

            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(Grid1, tb1);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();

            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                string rowID = Grid1.Rows[i].DataKeys[0].ToString();
                if (rowID.Count() > 0)
                {
                    Model.Check_CheckControl checkControl = BLL.CheckControlService.GetCheckControl(rowID);
                    if (checkControl.State.Equals("5") || checkControl.State.Equals("6"))
                    {
                        Grid1.Rows[i].RowCssClass = "LightGreen";//未确认      
                    }
                    else if (checkControl.State == Const.CheckControl_Complete)
                    { //闭环
                        Grid1.Rows[i].RowCssClass = "Green";

                    }
                    else if (Convert.ToDateTime(checkControl.LimitDate).AddDays(1) < DateTime.Now)  //延期未整改
                    {
                        Grid1.Rows[i].RowCssClass = "HotPink";
                    }
                    else  //期内未整改
                    {
                        Grid1.Rows[i].RowCssClass = " Yellow ";
                    }
                }
            }
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

        #region 过滤表头、排序、分页、关闭窗口
        /// <summary>
        /// 过滤表头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
        }

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
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            //Grid1.SortDirection = e.SortDirection;
            //Grid1.SortField = e.SortField;
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
        /// 关闭弹出窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region Grid双击事件
        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            btnMenuModify_Click(null, null);
        }
        #endregion
        #region 编辑
        /// <summary>
        /// 编辑按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string codes = Grid1.SelectedRowID.Split(',')[0];
            var checks = BLL.CheckControlService.GetCheckControl(codes);

            if (checks != null)
            {
                if (checks.State.Equals(Const.CheckControl_Complete))
                {
                    Alert.ShowInTop("您不是当前办理人，无法编辑,请右键查看！", MessageBoxIcon.Warning);
                    return;
                }
                Model.Check_CheckControlApprove approve = BLL.CheckControlApproveService.GetCheckControlApproveByCheckControlId(codes);
                if (approve != null)
                {
                    if (!string.IsNullOrEmpty(approve.ApproveMan))
                    {
                        if (this.CurrUser.UserId == approve.ApproveMan || CurrUser.UserId == Const.sysglyId)
                        {
                            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckListEdit.aspx?CheckControlCode={0}", codes, "编辑 - ")));
                            return;
                        }
                        else if (checks.State == BLL.Const.CheckControl_Complete)
                        {
                            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckListView.aspx?CheckControlCode={0}", codes, "查看 - ")));
                            return;
                        }
                        else
                        {
                            Alert.ShowInTop("您不是当前办理人，无法编辑,请右键查看！", MessageBoxIcon.Warning);
                            return;
                        }

                    }
                    if (this.btnMenuModify.Hidden || checks.State == BLL.Const.State_2)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
                    {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckListView.aspx?CheckControlCode={0}", codes, "查看 - ")));
                        return;
                    }
                    else
                    {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckListEdit.aspx?CheckControlCode={0}", codes, "编辑 - ")));
                        return;
                    }

                }
                else
                {
                    Alert.ShowInTop("您不是当前办理人，无法编辑,请右键查看！", MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }

        #endregion

        #region 删除
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDel_Click(object sender, EventArgs e)
        {

            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string codes = Grid1.SelectedRowID.Split(',')[0];
            var checks = BLL.CheckControlService.GetCheckControl(codes);

            BLL.CheckControlApproveService.DeleteCheckControlApprovesByCheckControlCode(codes);
            BLL.CheckControlService.DeleteCheckControl(codes);
            BLL.LogService.AddSys_Log(this.CurrUser, checks.DocCode, codes, BLL.Const.CheckListMenuId, "删除质量巡检记录");
            Grid1.DataBind();
            BindGrid();
            Alert.ShowInTop("删除数据成功！", MessageBoxIcon.Success);
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.ProjectId, this.CurrUser.UserId, BLL.Const.CheckListMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;

                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuModify.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDel.Hidden = false;
                }
            }
        }
        #endregion
        /// <summary>
        /// 把状态转换代号为文字形式
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertState(object state)
        {
            if (state != null)
            {
                if (state.ToString() == BLL.Const.CheckControl_ReCompile)
                {
                    return "重新编制";
                }
                else if (state.ToString() == BLL.Const.CheckControl_Compile)
                {
                    return "编制";
                }
                else if (state.ToString() == BLL.Const.CheckControl_Audit1)
                {
                    return "总包负责人审批";
                }
                else if (state.ToString() == BLL.Const.CheckControl_Audit2)
                {
                    return "分包专业工程师回复";
                }
                else if (state.ToString() == BLL.Const.CheckControl_Audit3)
                {
                    return "分包负责人审批";
                }
                else if (state.ToString() == BLL.Const.CheckControl_Audit4)
                {
                    return "总包专业工程师确认";
                }
                else if (state.ToString() == BLL.Const.CheckControl_Audit5)
                {
                    return "总包负责人确认";
                }
                else if (state.ToString() == BLL.Const.CheckControl_Complete)
                {
                    return "审批完成";
                }
                else if (state.ToString() == BLL.Const.CheckControl_ReCompile2)
                {
                    return "分包专业工程师重新回复";
                }
                else
                {
                    return "";
                }
            }
            return "";
        }
        //<summary>
        //获取办理人姓名
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        protected string ConvertMan(object CheckControlCode)
        {
            if (CheckControlCode != null)
            {
                Model.Check_CheckControlApprove a = BLL.CheckControlApproveService.GetCheckControlApproveByCheckControlId(CheckControlCode.ToString());
                if (a != null)
                {
                    if (a.ApproveMan != null)
                    {
                        return BLL.UserService.GetUserByUserId(a.ApproveMan).UserName;
                    }
                }
                else
                {
                    return "";
                }
            }
            return "";
        }

        protected void Grid1_RowDataBound(object sender, GridRowEventArgs e)
        {
            //if (checkControl.State.Equals("5") || checkControl.State.Equals("6"))
            //{
            //    Grid1.Rows[i].RowCssClass = "lightgreen";//未确认
            //}
            //else if (checkControl.State == Const.CheckControl_Complete)
            //{ //闭环
            //    Grid1.Rows[i].RowCssClass = "green";
            //}
            ////else if( checkControl.LimitDate> )
            //else if (Convert.ToDateTime(checkControl.LimitDate).AddDays(1).Date < DateTime.Now && checkControl.State != BLL.Const.CheckControl_Complete)  //延期未整改
            //{
            //    Grid1.Rows[i].RowCssClass = "orange";
            //}
            //else  //期内未整改
            //{
            //    Grid1.Rows[i].RowCssClass = "red";
            //}
        }

        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string codes = Grid1.SelectedRowID.Split(',')[0];
            var checks = BLL.CheckControlService.GetCheckControl(codes);

            if (checks != null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckListView.aspx?CheckControlCode={0}", codes, "查看 - ")));
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void btnRset_Click(object sender, EventArgs e)
        {
            drpSponsorUnit.SelectedIndex = 0;
            drpCNProfessional.SelectedIndex = 0;
            dpHandelStatus.SelectedIndex = 0;
            drpQuestionType.SelectedIndex = 0;
            drpUnitWork.SelectedIndex = 0;
            txtStartTime.Text = "";
            txtEndTime.Text = "";
            BindGrid();
        }
    }
}