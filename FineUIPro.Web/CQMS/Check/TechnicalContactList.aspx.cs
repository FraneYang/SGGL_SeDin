using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.CQMS.Check
{
    public partial class TechnicalContactList : PageBase
    {
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
            var buttonList = CommonService.GetAllButtonList(CurrUser.LoginProjectId, CurrUser.UserId, Const.TechnicalContactListMenuId);
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
        /// <summary>
        /// 工程联络单主键
        /// </summary>
        public string TechnicalContactListId
        {
            get
            {
                return (string)ViewState["TechnicalContactListId"];
            }
            set
            {
                ViewState["TechnicalContactListId"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UnitService.GetUnit(drpProposeUnit, CurrUser.LoginProjectId, true);
                Funs.FineUIPleaseSelect(this.drpContactListType);
                Funs.FineUIPleaseSelect(this.drpIsReply);
                Funs.FineUIPleaseSelect(this.drpState);
                UnitWorkService.InitUnitWorkDownList(drpUnitWork, this.CurrUser.LoginProjectId, true);
                CNProfessionalService.InitCNProfessionalDownList(drpCNProfessional, true);
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                btnNew.OnClientClick = Window1.GetShowReference("EditTechnicalContactList.aspx") + "return false;";
                GetButtonPower();
                BindData();

            }
        }


        protected DataTable ChecklistData()
        {
            string strSql = @"SELECT chec.TechnicalContactListId,chec.ProjectId,chec.ProposedUnitId,chec.UnitWorkId,"
                          + @" chec.CompileMan,chec.CompileDate,chec.code,chec.state,chec.CNProfessionalCode,chec.IsReply,chec.ContactListType,"
                          + @" unit.UnitName,u.userName "
                          + @" FROM Check_TechnicalContactList chec "
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
            if (drpContactListType.SelectedValue != Const._Null)
            {
                strSql += " AND chec.ContactListType=@ContactListType";
                listStr.Add(new SqlParameter("@ContactListType", drpContactListType.SelectedValue));
            }
            if (drpUnitWork.SelectedValue != Const._Null)
            {
                strSql += " AND   CHARINDEX(@unitworkId,chec.unitworkId) > 0 ";
                listStr.Add(new SqlParameter("@unitworkId", drpUnitWork.SelectedValue));
            }
            if (drpCNProfessional.SelectedValue != Const._Null)
            {
                strSql += " AND   CHARINDEX(@CNProfessionalCode,chec.CNProfessionalCode) > 0 ";
                //strSql += " AND chec.CNProfessionalCode=@CNProfessionalCode";
                listStr.Add(new SqlParameter("@CNProfessionalCode", drpCNProfessional.SelectedValue));
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
                    listStr.Add(new SqlParameter("@State", "8"));
                }
                else   //未闭合
                {
                    strSql += " AND chec.State!=@State";
                    listStr.Add(new SqlParameter("@State", "8"));
                }
            }
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


        private void BindData()
        {

            var list = ChecklistData();
            Grid1.RecordCount = list.Rows.Count;
            var CNProfessional = CNProfessionalService.GetCNProfessionalItem();
            var uniWork = UnitWorkService.GetUnitWorkLists(CurrUser.LoginProjectId);
            if (list.Rows.Count > 0)
            {
                for (int i = 0; i < list.Rows.Count; i++)
                {
                    if (list.Rows[i]["CNProfessionalCode"] != null)
                    {
                        var code = list.Rows[i]["CNProfessionalCode"].ToString().Split(',');
                        var listf = CNProfessional.Where(p => code.Contains(p.Value)).Select(p => p.Text).ToArray();
                        list.Rows[i]["CNProfessionalCode"] = string.Join(",", listf);
                    }
                    if (list.Rows[i]["UnitWorkId"] != null)
                    {
                        var code = list.Rows[i]["UnitWorkId"].ToString().Split(',');
                        var workid = uniWork.Where(p => code.Contains(p.UnitWorkId)).Select(p => p.UnitWorkName+BLL.UnitWorkService.GetProjectType(p.ProjectType)).ToArray();
                        list.Rows[i]["UnitWorkId"] = string.Join(",", workid);
                    }
                    if (list.Rows[i]["ContactListType"] != null)
                    {
                        list.Rows[i]["ContactListType"] = list.Rows[i]["ContactListType"].ToString() == "1" ? "图纸类" : "非图纸类";
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
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            //Grid1.PageIndex = e.NewPageIndex;
            BindData();
        }

        /// <summary>
        /// 分页显示条数下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindData();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindData();
        }

        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            TechnicalContactListId = Grid1.SelectedRowID.Split(',')[0];
            Model.Check_TechnicalContactList technicalContactList = TechnicalContactListService.GetTechnicalContactListByTechnicalContactListId(TechnicalContactListId);
            Model.Check_TechnicalContactListApprove approve = TechnicalContactListApproveService.GetTechnicalContactListApproveByTechnicalContactListId(TechnicalContactListId);

            if (technicalContactList.State == Const.TechnicalContactList_Complete)
            {
                Alert.ShowInTop("您不是当前办理人，无法编辑,请右键查看！", MessageBoxIcon.Warning);
                return;
            }
            if (approve != null)
            {
                if (!string.IsNullOrEmpty(approve.ApproveMan))
                {
                    if (CurrUser.UserId == approve.ApproveMan || CurrUser.UserId == Const.sysglyId)
                    {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EditTechnicalContactList.aspx?TechnicalContactListId={0}", TechnicalContactListId, "编辑 - ")));

                    }
                    else
                    {
                        Alert.ShowInTop("您不是当前办理人，无法操作！", MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            else
            {
                Alert.ShowInTop("您不是当前办理人，无法操作！", MessageBoxIcon.Warning);
                return;
            }


        }

        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            Grid1_RowDoubleClick(null, null);
        }

        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            TechnicalContactListId = Grid1.SelectedRowID.Split(',')[0];
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("TechnicalContactView.aspx?TechnicalContactListId={0}", TechnicalContactListId, "查看 - ")));
        }

        protected void btnMenuDel_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            TechnicalContactListId = Grid1.SelectedRowID.Split(',')[0];
            var code = TechnicalContactListService.GetTechnicalContactListByTechnicalContactListId(TechnicalContactListId).Code;
            TechnicalContactListApproveService.DeleteTechnicalContactListApprovesByTechnicalContactListId(TechnicalContactListId);
            TechnicalContactListService.DeleteTechnicalContactList(TechnicalContactListId);
            LogService.AddSys_Log(CurrUser, code, TechnicalContactListId, Const.TechnicalContactListMenuId, "删除工程联络单");
            BindData();
            Alert.ShowInTop("删除成功！", MessageBoxIcon.Success);
        }

        protected void btnRset_Click(object sender, EventArgs e)
        {
            txtStartTime.Text = "";
            txtEndTime.Text = "";
            drpProposeUnit.SelectedIndex = 0;
            drpContactListType.SelectedIndex = 0;
            drpIsReply.SelectedIndex = 0;
            drpState.SelectedIndex = 0;
            drpCNProfessional.SelectedIndex = 0;
            drpUnitWork.SelectedIndex = 0;
            BindData();
        }
    }
}