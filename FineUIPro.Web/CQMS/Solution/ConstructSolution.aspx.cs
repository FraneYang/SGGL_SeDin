using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.CQMS.Solution
{
    public partial class ConstructSolution : PageBase
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
            var buttonList = CommonService.GetAllButtonList(CurrUser.LoginProjectId, CurrUser.UserId, Const.CQMSConstructSolutionMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(Const.BtnAdd))
                {
                    btnNew.Hidden = false;

                }
                if (buttonList.Contains(Const.BtnAuditing))
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
                GetButtonPower();
                drpSolutionType.DataSource = CQMSConstructSolutionService.GetSolutionType();
                drpSolutionType.DataTextField = "Text";
                drpSolutionType.DataValueField = "Value";
                drpSolutionType.DataBind();
                Funs.FineUIPleaseSelect(drpSolutionType);
                Funs.FineUIPleaseSelect(drpState);
                UnitWorkService.InitUnitWorkDownList(drpUnitWork, this.CurrUser.LoginProjectId, true);
                CNProfessionalService.InitCNProfessionalDownList(drpCNProfessional, true);
                UnitService.InitUnitDropDownList(drpProposeUnit, CurrUser.LoginProjectId, true);
                btnNew.OnClientClick = window_tt.GetShowReference("EditConstructSolution.aspx") + "return false;";
                BindGrid();
            }
            else
            {
                var eventArgs = GetRequestEventArgument(); // 此函数所在文件：PageBase.cs
                if (eventArgs.StartsWith("ButtonClick"))
                {
                    string rootPath = Server.MapPath("~/");
                    string path = string.Empty;
                    if (drpModelType.SelectedValue == "1")
                    {
                        path = Const.CQMSConstructSolutionTemplateUrl1;
                    }
                    else
                    {
                        path = Const.CQMSConstructSolutionTemplateUrl3;
                    }
                    string uploadfilepath = rootPath + path;
                    string fileName = Path.GetFileName(uploadfilepath);
                    FileInfo fileInfo = new FileInfo(uploadfilepath);
                    FileInfo info = new FileInfo(uploadfilepath);
                    long fileSize = info.Length;
                    Response.Clear();
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                    Response.AddHeader("Content-Length", fileSize.ToString());
                    Response.TransmitFile(uploadfilepath, 0, fileSize);
                    Response.Flush();
                }
            }
        }
        protected DataTable ChecklistData()
        {

            string strSql = @"SELECT chec.ConstructSolutionId,chec.ProjectId,chec.UnitId,chec.UnitWorkIds,chec.CNProfessionalCodes,"
                              + @" chec.CompileMan,chec.CompileDate,chec.code,chec.state,chec.SolutionType,chec.SolutionName,"
                              + @" unit.UnitName,u.userName as CompileManName"
                              + @" FROM Solution_CQMSConstructSolution chec "
                              + @" left join Base_Unit unit on unit.unitId=chec.UnitId "
                              + @" left join sys_User u on u.userId = chec.CompileMan"
                              + @" where chec.ProjectId=@ProjectId";

            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", CurrUser.LoginProjectId));
            if (drpProposeUnit.SelectedValue != Const._Null)
            {
                strSql += " AND chec.UnitId=@unitId";
                listStr.Add(new SqlParameter("@unitId", drpProposeUnit.SelectedValue));
            }
            if (drpSolutionType.SelectedValue != Const._Null)
            {
                strSql += " AND chec.SolutionType=@SolutionType";
                listStr.Add(new SqlParameter("@SolutionType", drpSolutionType.SelectedValue));
            }
            if (drpUnitWork.SelectedValue != Const._Null)
            {
                strSql += " AND   CHARINDEX(@unitworkId,chec.unitworkIds) > 0 ";
                listStr.Add(new SqlParameter("@unitworkId", drpUnitWork.SelectedValue));
            }
            if (drpCNProfessional.SelectedValue != Const._Null)
            {
                strSql += " AND   CHARINDEX(@CNProfessionalCode,chec.CNProfessionalCodes) > 0 ";
                //strSql += " AND chec.CNProfessionalCode=@CNProfessionalCode";
                listStr.Add(new SqlParameter("@CNProfessionalCode", drpCNProfessional.SelectedValue));
            }
            if (drpState.SelectedValue != Const._Null)
            {
                if (drpState.SelectedValue == "1")   //已闭合
                {
                    strSql += " AND State=@State";
                    listStr.Add(new SqlParameter("@State", "3"));
                }
                else   //未闭合
                {
                    strSql += " AND State!=@State";
                    listStr.Add(new SqlParameter("@State", "3"));
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
            var CNProfessional = CNProfessionalService.GetCNProfessionalItem();
            var uniWork = UnitWorkService.GetUnitWorkLists(CurrUser.LoginProjectId);
            if (list.Rows.Count > 0)
            {
                for (int i = 0; i < list.Rows.Count; i++)
                {
                    if (list.Rows[i]["CNProfessionalCodes"] != null)
                    {
                        var code = list.Rows[i]["CNProfessionalCodes"].ToString().Split(',');
                        var listf = CNProfessional.Where(p => code.Contains(p.Value)).Select(p => p.Text).ToArray();
                        list.Rows[i]["CNProfessionalCodes"] = string.Join(",", listf);
                    }
                    if (list.Rows[i]["UnitWorkIds"] != null)
                    {
                        var code = list.Rows[i]["UnitWorkIds"].ToString().Split(',');
                        var workid = uniWork.Where(p => code.Contains(p.UnitWorkId)).Select(p => p.UnitWorkName).ToArray();
                        list.Rows[i]["UnitWorkIds"] = string.Join(",", workid);
                    }
                }
            }
            list = GetFilteredTable(Grid1.FilteredData, list);
            var table = GetPagedDataTable(Grid1, list);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnRset_Click(object sender, EventArgs e)
        {
            drpSolutionType.SelectedIndex = 0;
            drpProposeUnit.SelectedIndex = 0;
            drpUnitWork.SelectedIndex = 0;
            drpCNProfessional.SelectedIndex = 0;
            drpState.SelectedIndex = 0;
            BindGrid();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string id = Grid1.SelectedRowID.Split(',')[0];
            Model.Solution_CQMSConstructSolution constructSolution = CQMSConstructSolutionService.GetConstructSolutionByConstructSolutionId(id);
            if (constructSolution.State == Const.CQMSConstructSolution_Complete)
            {
                Alert.ShowInTop("该方案已经审批完成,无法操作,请右键查看!", MessageBoxIcon.Warning);
                return;
            }
            else if (constructSolution.State == Const.CQMSConstructSolution_Compile)
            {
                if (constructSolution.CompileMan == CurrUser.UserId || CurrUser.UserId == Const.sysglyId)
                {

                    PageContext.RegisterStartupScript(window_tt.GetShowReference(String.Format("EditConstructSolution.aspx?constructSolutionId={0}", id)));
                }
                else
                {
                    Alert.ShowInTop("您不是编制人，无法操作！请右键查看", MessageBoxIcon.Warning);
                    return;
                }
            }
            else if (constructSolution.State == Const.CQMSConstructSolution_Audit || constructSolution.State == Const.CQMSConstructSolution_ReCompile)
            {
                Model.Solution_CQMSConstructSolutionApprove approve = CQMSConstructSolutionApproveService.GetConstructSolutionApproveByApproveMan(id, CurrUser.UserId);
                if (approve != null || CurrUser.UserId == Const.sysglyId)
                {
                    PageContext.RegisterStartupScript(window_tt.GetShowReference(String.Format("EditConstructSolution.aspx?constructSolutionId={0}", id)));
                    return;
                    //Response.Redirect("CQMSConstructSolutionAudit.aspx?constructSolutionId=" + id);
                    //PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckListView.aspx?CheckControlCode={0}", id, "查看 - ")));
                }
                else
                {
                    if (constructSolution.CompileMan.Equals(CurrUser.UserId))
                    {
                        PageContext.RegisterStartupScript(window_tt.GetShowReference(String.Format("EditConstructSolution.aspx?constructSolutionId={0}", id)));
                    }
                    else
                    {
                        Alert.ShowInTop("您不是办理用户，无法操作！请右键查看", MessageBoxIcon.Warning);
                        return;
                    }

                }
            }


        }

        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string id = Grid1.SelectedRowID.Split(',')[0];
            PageContext.RegisterStartupScript(window_tt.GetShowReference(String.Format("ConstructSolutionView.aspx?constructSolutionId={0}", id)));

        }

        protected void btnMenuDel_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string id = Grid1.SelectedRowID.Split(',')[0];
            if (CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, CurrUser.UserId, Const.CQMSConstructSolutionMenuId, Const.BtnDelete))
            {
                var constructSolution = CQMSConstructSolutionService.GetConstructSolutionByConstructSolutionId(id);
                CQMSConstructSolutionApproveService.DeleteConstructSolutionApprovesByConstructSolutionId(id);
                CQMSConstructSolutionService.DeleteConstructSolution(id);
                LogService.AddSys_Log(CurrUser, constructSolution.Code, id, Const.CQMSConstructSolutionMenuId, "删除方案审查");
                BindGrid();
                Alert.ShowInTop("删除成功！", MessageBoxIcon.Success);

            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Success);
            }
        }

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }

        protected void window_tt_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }

        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            btnMenuModify_Click(sender, e);
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }
    }
}