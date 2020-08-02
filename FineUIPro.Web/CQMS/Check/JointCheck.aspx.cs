using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace FineUIPro.Web.CQMS.Check
{
    public partial class JointCheck : PageBase
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.ProjectId, this.CurrUser.UserId, BLL.Const.JointCheckMenuId);
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ProjectId = CurrUser.LoginProjectId;
                GetButtonPower();

                btnNew.OnClientClick = Window1.GetShowReference("EditJointCheck.aspx") + "return false;";
                //if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.ProjectId)
                //{
                //    this.ProjectId = Request.Params["projectId"];
                //}
                //权限按钮方法
                UnitService.InitUnitByProjectIdUnitTypeDropDownList(drpSponsorUnit, this.CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_2, true);
                JointCheckService.Init(drpCheckType, true);
                JointCheckService.InitState(drpState, true);
                bindata();
            }
        }
        //<summary>
        //获取办理人姓名
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        protected string ConvertMan(object JointCheckId)
        {
            string userNames = string.Empty;
            if (JointCheckId != null)
            {
                List<Model.Check_JointCheckApprove> list = BLL.JointCheckApproveService.GetJointCheckApprovesByJointCheckId(JointCheckId.ToString());
                foreach (var a in list)
                {
                    if (a != null)
                    {
                        if (a.ApproveMan != null)
                        {
                            if (!userNames.Contains(BLL.UserService.GetUserByUserId(a.ApproveMan).UserName))
                            {
                                userNames += UserService.GetUserByUserId(a.ApproveMan).UserName + ",";
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(userNames))
                {
                    userNames = userNames.Substring(0, userNames.LastIndexOf(","));
                }
            }
            return userNames;
        }
        /// <summary>
        /// 把状态转换代号为文字形式
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertState(object state)
        {
            if (state != null)
            {
                if (state.ToString() == BLL.Const.JointCheck_ReCompile)
                {
                    return "重新编制";
                }
                else if (state.ToString() == BLL.Const.JointCheck_Compile)
                {
                    return "编制";
                }
                else if (state.ToString() == BLL.Const.JointCheck_Audit1)
                {
                    return "分包专工回复";
                }
                else if (state.ToString() == BLL.Const.JointCheck_Audit2)
                {
                    return "分包负责人审批";
                }
                else if (state.ToString() == BLL.Const.JointCheck_Audit3)
                {
                    return "总包专工回复";
                }
                else if (state.ToString() == BLL.Const.JointCheck_Audit4)
                {
                    return "总包负责人审批";
                }
                else if (state.ToString() == BLL.Const.JointCheck_Complete)
                {
                    return "审批完成";
                }
                else if (state.ToString() == BLL.Const.JointCheck_Z)
                {
                    return "整改中";
                }
                else if (state.ToString() == BLL.Const.JointCheck_Audit1R)
                {
                    return "分包专工重新回复";
                }
                else
                {
                    return "";
                }
            }
            return "";
        }
        //<summary>
        //获取检查类别
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        protected string ConvertCheckType(object CheckType)
        {
            if (CheckType != null)
            {
                string checkType = CheckType.ToString();
                if (checkType == "1")
                {
                    return "周检查";
                }
                else if (checkType == "2")
                {
                    return "月检查";
                }
                else if (checkType == "3")
                {
                    return "不定期检查";
                }
                else if (checkType == "4")
                {
                    return "专业检查";
                }
            }
            return "";
        }
        /// <summary>
        /// 列表数据
        /// </summary>
        private void bindata()
        {

            string strSql = @"SELECT chec.JointCheckId,chec.JointCheckCode,chec.State,chec.CheckDate,chec.CheckName,chec.unitId,"
                          + @" unit.UnitName,u.userName as CheckMan,chec.CheckType "
                          + @" FROM Check_JointCheck chec"
                          + @" left join Base_Unit unit on unit.unitId=chec.unitId"
                          + @" left join sys_User u on u.userId = chec.CheckMan"
                          + @" where chec.ProjectId=@ProjectId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            strSql += " AND (chec.CheckDate>=@startTime or @startTime='') and (chec.CheckDate<=@endTime or @endTime='') ";
            listStr.Add(new SqlParameter("@startTime", !string.IsNullOrEmpty(txtStartTime.Text.Trim()) ? txtStartTime.Text.Trim() + " 00:00:00" : ""));
            listStr.Add(new SqlParameter("@endTime", !string.IsNullOrEmpty(txtEndTime.Text.Trim()) ? txtEndTime.Text.Trim() + " 23:59:59" : ""));
            if (drpSponsorUnit.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND chec.unitId=@unitId";
                listStr.Add(new SqlParameter("@unitId", drpSponsorUnit.SelectedValue));
            }
            if (drpCheckType.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND chec.CheckType=@CheckType";
                listStr.Add(new SqlParameter("@CheckType", drpCheckType.SelectedValue));
            }
            if (drpState.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND chec.State=@State";
                listStr.Add(new SqlParameter("@State", drpState.SelectedValue));
            }
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
            GvJoinCheck.RecordCount = tb.Rows.Count;
            ddlPageSize.SelectedValue = GvJoinCheck.PageSize.ToString();
            tb = GetFilteredTable(GvJoinCheck.FilteredData, tb);
            var table = GetPagedDataTable(GvJoinCheck, tb);
            GvJoinCheck.DataSource = table;
            GvJoinCheck.DataBind();

        }


        protected void btnQuery_Click(object sender, EventArgs e)
        {
            bindata();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            GvJoinCheck.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            bindata();
        }

        protected void GvJoinCheck_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            edit(sender, e);
        }



        protected void GvJoinCheck_PageIndexChange(object sender, GridPageEventArgs e)
        {
            bindata();
        }

        protected void GvJoinCheck_FilterChange(object sender, EventArgs e)
        {

        }

        protected void GvJoinCheck_Sort(object sender, GridSortEventArgs e)
        {

        }

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            bindata();
        }

        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            edit(sender, e);
        }

        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            if (GvJoinCheck.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string jointCheckId = GvJoinCheck.SelectedRowID.Split(',')[0];
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("JointCheckView.aspx?JointCheckId={0}", jointCheckId, "查看 - ")));
        }

        protected void btnMenuDel_Click(object sender, EventArgs e)
        {
            if (GvJoinCheck.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string jointCheckId = GvJoinCheck.SelectedRowID.Split(',')[0];
            JointCheckDetailService.DeleteJointCheckDetailByJointCheckId(jointCheckId);
            JointCheckApproveService.DeleteJointCheckApprovesByJointCheckId(jointCheckId);
            JointCheckService.DeleteJointCheck(jointCheckId);
            LogService.AddSys_Log(CurrUser, "001", jointCheckId, Const.JointCheckMenuId, "删除质量共检记录");
            Alert.ShowInTop("删除成功！", MessageBoxIcon.Warning);
            bindata();

        }

        protected void edit(object sender, EventArgs e)
        {

            if (GvJoinCheck.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string JointCheckId = GvJoinCheck.SelectedRowID.Split(',')[0];
            Model.Check_JointCheck jointCheck = BLL.JointCheckService.GetJointCheck(JointCheckId);
            Model.Check_JointCheckApprove approve = BLL.JointCheckApproveService.GetJointCheckApproveByJointCheckId(JointCheckId, this.CurrUser.UserId);
            if (jointCheck.State == BLL.Const.JointCheck_Complete)
            {
                Alert.ShowInTop("该记录已审批完成，请右键查看!", MessageBoxIcon.Warning);
                return;
            }
            if (approve != null)
            {
                if (!string.IsNullOrEmpty(approve.ApproveMan))
                {
                    if (this.CurrUser.UserId == approve.ApproveMan || CurrUser.UserId == Const.sysglyId)
                    {
                        if (jointCheck.State == BLL.Const.JointCheck_Compile)
                        {
                            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EditJointCheck.aspx?JointCheckId={0}", JointCheckId, "查看 - ")));
                        }
                        else
                        {
                            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EditJointCheckTwo.aspx?JointCheckId={0}", JointCheckId, "查看 - ")));
                        }
                        return;
                    }
                    else
                    {
                        Alert.ShowInTop("您不是当前办理人，无法操作，请右键查看!", MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            else
            {
                Alert.ShowInTop("您不是当前办理人，无法操作，请右键查看!", MessageBoxIcon.Warning);
                return;
            }
        }

        protected void btnRset_Click(object sender, EventArgs e)
        {
            drpSponsorUnit.SelectedIndex = 0;
            drpCheckType.SelectedIndex = 0;
            drpState.SelectedIndex = 0;
            txtStartTime.Text = "";
            txtEndTime.Text = "";
            bindata();
        }
    }
}