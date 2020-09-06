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
    public partial class CheckEquipment : PageBase
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ProjectId = this.CurrUser.LoginProjectId;
                GetButtonPower();
                BLL.UnitService.InitUnitByProjectIdUnitTypeDropDownList1(drpUserUnit, this.CurrUser.LoginProjectId, true);
                BindGrid();
            }
        }

        private void BindGrid()
        {
            string strSql = "select C.CheckEquipmentId,C.ProjectId,B.UnitName AS UserUnit,Isdamage,EquipmentName,Format,SetAccuracyGrade,CompileMan, RealAccuracyGrade,CheckCycle, CheckDay,(case IsIdentification when 'true' then '是' when 'false' then '否' else '' end) IsIdentification,(case IsCheckCertificate when 'true' then '是' when 'false' then '否' else '' end) IsCheckCertificate,U.UserName,C.State from[dbo].[Check_CheckEquipment] C left join Sys_User U on C.CompileMan=U.UserId left join Base_Unit B on C.UserUnitId=B.UnitId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " where C.ProjectId = @ProjectId";
            listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));
            if (drpUserUnit.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND C.UserUnitId=@UserUnitId";
                listStr.Add(new SqlParameter("@UserUnitId", drpUserUnit.SelectedValue));
            }
            if (!string.IsNullOrEmpty(this.txtEquipmentName.Text.Trim()))
            {
                strSql += @" and C.EquipmentName like @EquipmentName ";
                listStr.Add(new SqlParameter("@EquipmentName", "%" + this.txtEquipmentName.Text.Trim() + "%"));
            }
            if (this.rblIsBeOverdue.SelectedValue == "true")
            {
                strSql += " and dateadd(day,(CheckCycle*365),CheckDay)<getdate()";
            }
            else if (this.rblIsBeOverdue.SelectedValue == "false")
            {
                strSql += " and dateadd(day,(CheckCycle*365),CheckDay)>=getdate()";
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }



        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
        #region 操作数据
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.CheckEquipmentMenuId, BLL.Const.BtnAdd))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EditCheckEquipment.aspx", "新增 - ")));
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }

        }
        //右键编辑
        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.CheckEquipmentMenuId, BLL.Const.BtnModify))
            {
                if (Grid1.SelectedRowIndexArray.Length == 0)
                {
                    Alert.ShowInTop("请至少选择一条记录", MessageBoxIcon.Warning);
                    return;
                }
                Model.Check_CheckEquipment checkEquipment = BLL.CheckEquipmentService.GetCheckEquipmentByCheckEquipmentId(Grid1.SelectedRowID);
                Model.Check_CheckEquipmentApprove approve = BLL.CheckEquipmentApproveService.GetCheckEquipmentApproveByCheckEquipmentId(Grid1.SelectedRowID);

                if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.CheckEquipmentMenuId, BLL.Const.BtnModify)
                    || BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.CheckEquipmentMenuId, BLL.Const.BtnAdd)
                    || BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.CheckEquipmentMenuId, BLL.Const.BtnAuditing))
                {
                    if (checkEquipment.State == BLL.Const.CheckEquipment_Complete)
                    {
                        if (checkEquipment.CompileMan == this.CurrUser.UserId || this.CurrUser.UserId == BLL.Const.sysglyId)
                        {
                            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EditCheckEquipmentTwo.aspx?CheckEquipmentId=" + Grid1.SelectedRowID, "编辑 - ")));
                        }
                        else
                        {
                            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EditCheckEquipment.aspx?see=see&CheckEquipmentId=" + Grid1.SelectedRowID, "查看 - ")));
                        }

                    }

                    if (approve != null)
                    {
                        if (!string.IsNullOrEmpty(approve.ApproveMan))
                        {
                            if (this.CurrUser.UserId == approve.ApproveMan || this.CurrUser.UserId == BLL.Const.sysglyId)
                            {
                                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EditCheckEquipment.aspx?CheckEquipmentId=" + Grid1.SelectedRowID, "编辑 - ")));
                            }
                            else
                            {
                                Alert.ShowInTop("您不是当前办理人，无法操作！", MessageBoxIcon.Warning);
                            }
                        }
                    }
                    else
                    {
                        Alert.ShowInTop("您不是当前办理人，无法编辑,请右键查看！", MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }
        //右键查看
        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EditCheckEquipment.aspx?see=see&CheckEquipmentId=" + Grid1.SelectedRowID, "查看 - ")));
        }
        //右键删除
        protected void btnMenuDel_Click(object sender, EventArgs e)
        {

            if (BLL.CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.CheckEquipmentMenuId, BLL.Const.BtnDelete))
            {
                BLL.CheckEquipmentApproveService.DeleteCheckEquipmentApprovesByCheckEquipmentId(Grid1.SelectedRowID);
                BLL.CheckEquipmentService.DeleteCheckEquipment(Grid1.SelectedRowID);
                //BLL.LogService.AddLog(this.CurrUser.UserId, "删除检试验设备及测量器具", this.CurrUser.LoginProjectId);
                BindGrid();
                Alert.ShowInTop("删除数据成功！", MessageBoxIcon.Success);
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            btnMenuModify_Click(null, null);
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.ProjectId, this.CurrUser.UserId, BLL.Const.CheckEquipmentMenuId);
            if (buttonList.Count() > 0)
            {
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

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
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
                if (state.ToString() == BLL.Const.CheckEquipment_ReCompile)
                {
                    return "重新编制";
                }
                else if (state.ToString() == BLL.Const.CheckEquipment_Compile)
                {
                    return "编制";
                }
                else if (state.ToString() == BLL.Const.CheckEquipment_Approve)
                {
                    return "审核";
                }
                else if (state.ToString() == BLL.Const.CheckEquipment_Complete)
                {
                    return "审批完成";
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        //<summary>
        //获取办理人姓名
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        protected string ConvertMan(object designId)
        {
            if (designId != null)
            {
                Model.Check_CheckEquipmentApprove a = BLL.CheckEquipmentApproveService.GetCheckEquipmentApproveByCheckEquipmentId(designId.ToString());
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

        //<summary>
        //获取办理人姓名
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        protected string ConvertIsBeOverdue(object CheckCycle, object CheckDay)
        {
            if (CheckCycle != null && CheckDay != null)
            {
                if (!string.IsNullOrEmpty(CheckCycle.ToString()) && !string.IsNullOrEmpty(CheckDay.ToString()))
                {
                    var ResultData = Convert.ToDateTime(CheckDay).AddDays(Convert.ToDouble(CheckCycle) * 365);
                    if (ResultData >= DateTime.Now)
                    {
                        return "未过期";
                    }
                    else
                    {
                        return "过期";
                    }
                }
            }
            return "";

        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnRset_Click(object sender, EventArgs e)
        {
            drpUserUnit.SelectedIndex = 0;
            txtEquipmentName.Text = "";
            rblIsBeOverdue.SelectedValue = "0";
            BindGrid();
        }
    }
}