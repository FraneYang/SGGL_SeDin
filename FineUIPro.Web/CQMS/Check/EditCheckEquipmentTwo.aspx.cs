using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.CQMS.Check
{
    public partial class EditCheckEquipmentTwo : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string CheckEquipmentId
        {
            get
            {
                return (string)ViewState["CheckEquipmentId"];
            }
            set
            {
                ViewState["CheckEquipmentId"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckEquipmentId = Request.Params["CheckEquipmentId"];
                txtProjectName.Text = ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId).ProjectName;
                BLL.UnitService.InitUnitByProjectIdUnitTypeDropDownList1(drpUserUnitId, this.CurrUser.LoginProjectId, false);
                BindGrid();
                if (!string.IsNullOrEmpty(CheckEquipmentId))
                {
                    Model.Check_CheckEquipment checkEquipment = BLL.CheckEquipmentService.GetCheckEquipmentByCheckEquipmentId(CheckEquipmentId);
                    if (!string.IsNullOrEmpty(checkEquipment.UserUnitId))
                    {
                        this.drpUserUnitId.SelectedValue = checkEquipment.UserUnitId;
                    }
                    this.txtEquipmentName.Text = checkEquipment.EquipmentName;
                    this.txtFormat.Text = checkEquipment.Format;
                    this.txtSetAccuracyGrade.Text = checkEquipment.SetAccuracyGrade;
                    this.txtRealAccuracyGrade.Text = checkEquipment.RealAccuracyGrade;
                    if (checkEquipment.CheckCycle != null)
                    {
                        this.txtCheckCycle.Text = checkEquipment.CheckCycle.ToString();
                    }
                    if (checkEquipment.CheckDay != null)
                    {
                        this.txtCheckDay.Text = string.Format("{0:yyyy-MM-dd}", checkEquipment.CheckDay);
                    }
                    if (checkEquipment.IsIdentification == true)
                    {
                        this.cbIsIdentification.Text = "是";
                    }
                    else
                    {
                        this.cbIsIdentification.Text = "否";
                    }
                    if (checkEquipment.IsCheckCertificate == true)
                    {
                        this.cbIsCheckCertificate.Text = "是";
                    }
                    else
                    {
                        this.cbIsCheckCertificate.Text = "否";
                    }
                    if (!string.IsNullOrEmpty(checkEquipment.Isdamage))
                    {
                        drpIsdamage.SelectedValue = checkEquipment.Isdamage;
                    }
                    this.drpHandleType.DataTextField = "Text";
                    this.drpHandleType.DataValueField = "Value";
                    this.drpHandleType.DataSource = BLL.CheckEquipmentService.GetDHandleTypeByState(BLL.Const.CheckEquipment_Compile);
                    this.drpHandleType.DataBind();
                    this.drpHandleType.SelectedIndex = 0;
                    this.drpHandleMan.DataTextField = "UserName";
                    this.drpHandleMan.DataValueField = "UserId";
                    this.drpHandleMan.DataSource = BLL.UserService.GetProjectUserListByProjectId(this.CurrUser.LoginProjectId);
                    this.drpHandleMan.DataBind();
                    this.drpHandleMan.SelectedIndex = 0;
                }
            }
        }

        protected void btnAttach_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?type=0&toKeyId={0}&path=FileUpload/CheckEquipment&menuId={1}", CheckEquipmentId, BLL.Const.CheckEquipmentMenuId)));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.CheckEquipmentMenuId, BLL.Const.BtnSave))
            {
                SavePauseNotice("save");
                ShowNotify("提交成功！", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());

            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.CheckEquipmentMenuId, BLL.Const.BtnSubmit))
            {
                SavePauseNotice("submit");
                ShowNotify("提交成功！", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        private void SavePauseNotice(string saveType)
        {
            Model.Check_CheckEquipment checkEquipment = new Model.Check_CheckEquipment();
            checkEquipment.EquipmentName = this.txtEquipmentName.Text.Trim();
            checkEquipment.ProjectId = this.CurrUser.LoginProjectId;
            checkEquipment.UserUnitId = this.drpUserUnitId.SelectedValue;
            checkEquipment.Format = this.txtFormat.Text.Trim();
            checkEquipment.SetAccuracyGrade = this.txtSetAccuracyGrade.Text.Trim();
            checkEquipment.RealAccuracyGrade = this.txtRealAccuracyGrade.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtCheckCycle.Text.Trim()))
            {
                checkEquipment.CheckCycle = Convert.ToDecimal(this.txtCheckCycle.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtCheckDay.Text.Trim()))
            {
                checkEquipment.CheckDay = Convert.ToDateTime(this.txtCheckDay.Text.Trim());
            }
            if (this.cbIsIdentification.Text == "是")
            {
                checkEquipment.IsIdentification = true;
            }
            else
            {
                checkEquipment.IsIdentification = false;
            }
            if (this.cbIsCheckCertificate.Text == "是")
            {
                checkEquipment.IsCheckCertificate = true;
            }
            else
            {
                checkEquipment.IsCheckCertificate = false;
            }
            if (drpIsdamage.SelectedValue != BLL.Const._Null)
            {
                checkEquipment.Isdamage = drpIsdamage.SelectedValue;
            }
            if (saveType == "submit")
            {
                checkEquipment.State = this.drpHandleType.SelectedValue;
            }
            else
            {
                Model.Check_CheckEquipment checkEquipment1 = BLL.CheckEquipmentService.GetCheckEquipmentByCheckEquipmentId(CheckEquipmentId);
                if (checkEquipment1 != null)
                {
                    if (string.IsNullOrEmpty(checkEquipment1.State))
                    {
                        checkEquipment.State = BLL.Const.CheckEquipment_Compile;
                    }
                    else
                    {
                        checkEquipment.State = checkEquipment1.State;
                    }
                }
                else
                {
                    checkEquipment.State = BLL.Const.CheckEquipment_ReCompile;
                }
            }
            if (!string.IsNullOrEmpty(CheckEquipmentId))
            {
                Model.Check_CheckEquipment checkEquipment1 = BLL.CheckEquipmentService.GetCheckEquipmentByCheckEquipmentId(CheckEquipmentId);
                Model.Check_CheckEquipmentApprove approve1 = BLL.CheckEquipmentApproveService.GetCheckEquipmentApproveByCheckEquipmentId(CheckEquipmentId);
                if (approve1 != null && saveType == "submit")
                {
                    approve1.ApproveDate = DateTime.Now;
                    if (this.drpHandleMan.SelectedValue != BLL.Const._Null)
                    {
                        approve1.ApproveMan = this.CurrUser.UserId;
                    }
                    approve1.ApproveType = BLL.Const.CheckEquipment_Compile;
                    BLL.CheckEquipmentApproveService.UpdateCheckEquipmentApprove(approve1);
                }
                if (saveType == "submit")
                {
                    Model.Check_CheckEquipmentApprove approve = new Model.Check_CheckEquipmentApprove();
                    approve.CheckEquipmentId = checkEquipment1.CheckEquipmentId;
                    if (this.drpHandleMan.SelectedValue != BLL.Const._Null)
                    {
                        approve.ApproveMan = this.drpHandleMan.SelectedValue;
                    }
                    approve.ApproveType = this.drpHandleType.SelectedValue;
                    BLL.CheckEquipmentApproveService.AddCheckEquipmentApprove(approve);
                }
                checkEquipment.CheckEquipmentId = CheckEquipmentId;
                BLL.CheckEquipmentService.UpdateCheckEquipment(checkEquipment);
            }
        }

        private void BindGrid()
        {
            string strSql = "select C.ApproveDate,C.ApproveIdea,C.ApproveMan,U.UserName ,C.CheckEquipmentApproveId,C.ApproveType from Check_CheckEquipmentApprove C left join Sys_User U on C.ApproveMan = U.UserId   where CheckEquipmentId=@CheckEquipmentId and  C.ApproveDate is not null";

            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@CheckEquipmentId", CheckEquipmentId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            gvApprove.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(gvApprove.FilteredData, tb);
            var table = this.GetPagedDataTable(gvApprove, tb);

            gvApprove.DataSource = table;
            gvApprove.DataBind();
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
    }
}