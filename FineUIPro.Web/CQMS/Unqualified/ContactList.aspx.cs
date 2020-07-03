using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.CQMS.Unqualified
{
    public partial class ContactList : PageBase
    {
        /// <summary>
        /// 工程联系单主键
        /// </summary>
        public string WorkContactId
        {
            get
            {
                return (string)ViewState["WorkContactId"];
            }
            set
            {
                ViewState["WorkContactId"] = value;
            }
        }
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
            var buttonList = CommonService.GetAllButtonList(CurrUser.LoginProjectId, CurrUser.UserId, Const.WorkContactMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(Const.BtnAdd))
                {
                    btnNew.Hidden = false;

                }
                if (buttonList.Contains(Const.BtnModify))
                {
                    btnMenuModify.Hidden = false;
                }
                if (buttonList.Contains(Const.BtnDelete))
                {
                    btnMenuDel.Hidden = false;
                }
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UnitService.GetUnit(drpProposeUnit, CurrUser.LoginProjectId, true);
                Funs.FineUIPleaseSelect(this.drpIsReply);
                Funs.FineUIPleaseSelect(this.drpState);
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                btnFinalFile.OnClientClick = Window1.GetShowReference("AddWorkContactFinalFile.aspx", "已定稿文件") + "return false;";
                btnNew.OnClientClick = window_tt.GetShowReference("EditWorkContact.aspx", "工作联系单") + "return false;";
                GetButtonPower();
                BindGrid();
            }
        }
        protected DataTable ChecklistData()
        {
            string strSql = @"SELECT chec.WorkContactId,chec.ProjectId,chec.ProposedUnitId,chec.MainSendUnitIds,chec.CCUnitIds,"
                          + @" chec.CompileMan,chec.CompileDate,chec.code,chec.state,chec.IsReply,chec.cause,"
                          + @" unit.UnitName,u.userName "
                          + @" FROM Unqualified_WorkContact chec "
                          + @" left join Base_Unit unit on unit.unitId=chec.ProposedUnitId "
                          + @" left join sys_User u on u.userId = chec.CompileMan"
                          + @" where chec.ProjectId=@ProjectId";

            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", CurrUser.LoginProjectId));
            strSql += " AND (chec.CompileDate>=@startTime or @startTime='') and (chec.CompileDate<=@endTime or @endTime='') ";
            listStr.Add(new SqlParameter("@startTime", !string.IsNullOrEmpty(txtStartTime.Text.Trim()) ? txtStartTime.Text.Trim() + " 00:00:00" : ""));
            listStr.Add(new SqlParameter("@endTime", !string.IsNullOrEmpty(txtEndTime.Text.Trim()) ? txtEndTime.Text.Trim() + " 23:59:59" : ""));
            if (drpProposeUnit.SelectedValue != Const._Null)
            {
                strSql += " AND chec.ProposedUnitId=@unitId";
                listStr.Add(new SqlParameter("@unitId", drpProposeUnit.SelectedValue));
            }
            if (drpIsReply.SelectedValue != Const._Null)
            {
                strSql += " AND chec.IsReply=@IsReply";
                listStr.Add(new SqlParameter("@IsReply", drpIsReply.SelectedValue));
            }
            if (drpState.SelectedValue != Const._Null)
            {
                if (drpState.SelectedValue == "1")   //已闭合
                {
                    strSql += " AND chec.State=@State";
                    listStr.Add(new SqlParameter("@State", "5"));
                }
                else   //未闭合
                {
                    strSql += " AND chec.State!=@State";
                    listStr.Add(new SqlParameter("@State", "5"));
                }
            }
            strSql += " order by chec.code desc ";
            //if (drpUnitWork.SelectedValue != Const._Null)
            //{
            //    strSql += " AND chec.unitworkId=@unitworkId";
            //    listStr.Add(new SqlParameter("@unitworkId", drpUnitWork.SelectedValue));
            //}
            //if (drpCNProfessional.SelectedValue != Const._Null)
            //{
            //    strSql += " AND chec.CNProfessionalCode=@CNProfessionalCode";
            //    listStr.Add(new SqlParameter("@CNProfessionalCode", drpCNProfessional.SelectedValue));
            //}
            //if (drpQuestionType.SelectedValue != Const._Null)
            //{
            //    strSql += " AND chec.QuestionType=@QuestionType";
            //    listStr.Add(new SqlParameter("@QuestionType", drpQuestionType.SelectedValue));
            //}
            //if (dpHandelStatus.SelectedValue != Const._Null)
            //{
            //    if (dpHandelStatus.SelectedValue.Equals("1"))
            //    {
            //        strSql += " AND (chec.state='5' or chec.state='6')";
            //    }
            //    else if (dpHandelStatus.SelectedValue.Equals("2"))
            //    {
            //        strSql += " AND chec.state='7'";
            //    }
            //    else if (dpHandelStatus.SelectedValue.Equals("3"))
            //    {
            //        strSql += " AND DATEADD(day,1,chec.LimitDate)< GETDATE() and chec.state<>5 and chec.state<>6 and chec.state<>7";
            //    }
            //    else if (dpHandelStatus.SelectedValue.Equals("4"))
            //    {
            //        strSql += " AND DATEADD(day,1,chec.LimitDate)> GETDATE() and chec.state<>5 and chec.state<>6 and chec.state<>7";
            //    }
            //}
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            return tb;
        }
        private void BindGrid()
        {
            var list = ChecklistData();
            Grid1.RecordCount = list.Rows.Count;
            var unit = UnitService.GetUnitByProjectIdList(CurrUser.LoginProjectId);
            if (list.Rows.Count > 0)
            {
                for (int i = 0; i < list.Rows.Count; i++)
                {
                    if (list.Rows[i]["MainSendUnitIds"] != null)
                    {
                        var unitIds = list.Rows[i]["MainSendUnitIds"].ToString().Split(',');
                        var listf = unit.Where(p => unitIds.Contains(p.UnitId)).Select(p => p.UnitName).ToArray();
                        list.Rows[i]["MainSendUnitIds"] = string.Join(",", listf);
                    }
                    if (list.Rows[i]["CCUnitIds"] != null)
                    {
                        var unitIds = list.Rows[i]["CCUnitIds"].ToString().Split(',');
                        var listf = unit.Where(p => unitIds.Contains(p.UnitId)).Select(p => p.UnitName).ToArray();
                        list.Rows[i]["CCUnitIds"] = string.Join(",", listf);
                    }
                    if (list.Rows[i]["IsReply"] != null)
                    {
                        list.Rows[i]["IsReply"] = list.Rows[i]["IsReply"].ToString() == "1" ? "需要回复" : "不需回复";
                    }
                }
            }
            list = GetFilteredTable(Grid1.FilteredData, list);
            var table = GetPagedDataTable(Grid1, list);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnRset_Click(object sender, EventArgs e)
        {
            drpProposeUnit.SelectedIndex = 0;
            txtStartTime.Text = "";
            txtEndTime.Text = "";
            drpIsReply.SelectedIndex = 0;
            drpState.SelectedIndex = 0;
            BindGrid();
        }

        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            WorkContactId = Grid1.SelectedRowID.Split(',')[0];
            Model.Unqualified_WorkContact WorkContact = BLL.WorkContactService.GetWorkContactByWorkContactId(WorkContactId);
            Model.Unqualified_WorkContactApprove approve = BLL.WorkContactApproveService.GetWorkContactApproveByWorkContactId(WorkContactId);
            if (WorkContact.State == BLL.Const.WorkContact_Complete)
            {
                Alert.ShowInTop("您不是当前办理人，无法操作！可以点击右键查看", MessageBoxIcon.Warning);
                return;
            }

            else if (approve != null)
            {
                if (!string.IsNullOrEmpty(approve.ApproveMan))
                {
                    if (this.CurrUser.UserId == approve.ApproveMan || this.CurrUser.UserId == BLL.Const.sysglyId)
                    {
                        PageContext.RegisterStartupScript(window_tt.GetShowReference(String.Format("EditWorkContact.aspx?WorkContactId={0}", WorkContactId, "编辑 - ")));

                    }
                    else
                    {
                        Alert.ShowInTop("您不是当前办理人，无法操作！可以点击右键查看", MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            else
            {
                Alert.ShowInTop("您不是当前办理人，无法操作！可以点击右键查看", MessageBoxIcon.Warning);
                return;
            }
            //else
            //{
            //    Alert.ShowInTop("您不是当前办理人，无法操作！", MessageBoxIcon.Warning);
            //    return;
            //}

        }

        protected void btnMenuDel_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            WorkContactId = Grid1.SelectedRowID.Split(',')[0];
            var workContact = WorkContactService.GetWorkContactByWorkContactId(WorkContactId);
            BLL.WorkContactApproveService.DeleteWorkContactApproveByWorkContactId(WorkContactId);
            BLL.WorkContactService.DeleteWorkContact(WorkContactId);
            BLL.LogService.AddSys_Log(this.CurrUser, workContact.Code, WorkContactId, Const.WorkContactMenuId, "删除工作联系单");
            BindGrid();
            Alert.ShowInTop("删除数据成功！", MessageBoxIcon.Success);
        }

        protected void window_tt_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }

        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            btnMenuModify_Click(sender, e);
        }

        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string workContactId = Grid1.SelectedRowID.Split(',')[0];
            Model.Unqualified_WorkContact workContact = WorkContactService.GetWorkContactByWorkContactId(workContactId);
            if (workContact.IsFinal == true)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("WorkContactFinalFileView.aspx?WorkContactId={0}", workContactId), "已定稿文件"));
                return;
            }
            PageContext.RegisterStartupScript(window_tt.GetShowReference(String.Format("WorkContactview.aspx?WorkContactId={0}", workContactId, "查看 - ")));
        }

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }
    }
}