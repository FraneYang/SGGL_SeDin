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
    public partial class EditCheckEquipment : PageBase
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


        /// <summary>
        /// 办理类型
        /// </summary>
        public string State
        {
            get
            {
                return (string)ViewState["State"];
            }
            set
            {
                ViewState["State"] = value;
            }
        }
        /// <summary>
        /// 附件
        /// </summary>
        public int HandleImg
        {
            get
            {
                return Convert.ToInt32(ViewState["HandleImg"]);
            }
            set
            {
                ViewState["HandleImg"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckEquipmentId = Request.Params["CheckEquipmentId"];
                if (!string.IsNullOrEmpty(CheckEquipmentId))
                {
                    BindGrid();
                }
                txtProjectName.Text = ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId).ProjectName;
                //this.hdCheckEquipmentCode.Text = string.Empty;
                BLL.UnitService.InitUnitByProjectIdUnitTypeDropDownList1(drpUserUnitId, this.CurrUser.LoginProjectId, false);
                Funs.FineUIPleaseSelect(drpHandleType);
                Funs.FineUIPleaseSelect(drpHandleMan);
                HandleImg = 0;
                drpHandleType.Readonly = true;
                if (!string.IsNullOrEmpty(CheckEquipmentId))
                {
                    this.hdCheckEquipmentCode.Text = CheckEquipmentId;
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
                        this.cbIsIdentification.Checked = true;
                    }
                    else
                    {
                        this.cbIsIdentification.Checked = false;
                    }
                    if (checkEquipment.IsCheckCertificate == true)
                    {
                        this.cbIsCheckCertificate.Checked = true;
                    }
                    else
                    {
                        this.cbIsCheckCertificate.Checked = false;
                    }
                    if (!string.IsNullOrEmpty(checkEquipment.Isdamage))
                    {
                        drpIsdamage.SelectedValue = checkEquipment.Isdamage;
                    }
                    if (!string.IsNullOrEmpty(checkEquipment.State))
                    {
                        State = checkEquipment.State;
                    }
                    else
                    {
                        State = BLL.Const.CheckEquipment_Compile;
                        this.HideOptions.Hidden = true;
                        this.rblIsAgree.Hidden = true;
                    }
                    if (State != BLL.Const.CheckEquipment_Complete)
                    {
                        this.drpHandleType.DataTextField = "Text";
                        this.drpHandleType.DataValueField = "Value";
                        this.drpHandleType.DataSource = BLL.CheckEquipmentService.GetDHandleTypeByState(State);
                        this.drpHandleType.DataBind();
                    }
                    if (State == BLL.Const.CheckEquipment_Compile.ToString() || State == BLL.Const.CheckEquipment_ReCompile.ToString())
                    {
                        this.HideOptions.Hidden = true;
                        this.rblIsAgree.Hidden = true;
                        this.drpHandleMan.Enabled = true;
                        this.plApprove2.Hidden = false;
                        this.drpHandleMan.DataTextField = "Text";
                        this.drpHandleMan.DataValueField = "Value";
                        this.drpHandleMan.DataSource = BLL.UserService.GetProjectUserListByProjectId(this.CurrUser.LoginProjectId);
                        this.drpHandleMan.DataBind();
                        this.drpHandleMan.SelectedIndex = 1;
                    }
                    else
                    {
                        this.drpHandleMan.DataTextField = "Text";
                        this.drpHandleMan.DataValueField = "Value";
                        this.drpHandleMan.DataSource = BLL.UserService.GetProjectUserListByProjectId(this.CurrUser.LoginProjectId);
                        this.HideOptions.Hidden = false;
                        this.rblIsAgree.Hidden = false;
                        this.plApprove2.Hidden = false;
                    }
                    if (State == Const.CheckEquipment_Approve.ToString())
                    {
                        this.drpHandleMan.Enabled = false;
                    }
                    if (State == BLL.Const.CheckEquipment_Complete.ToString() || !string.IsNullOrEmpty(Request.Params["see"]))
                    {
                        this.btnSave.Hidden = true;
                        this.btnSubmit.Hidden = true;
                        this.next.Hidden = true;
                        this.HideOptions.Hidden = true;
                        this.rblIsAgree.Hidden = true;
                        this.plApprove2.Hidden = false;
                        this.txtEquipmentName.Readonly = true;
                        this.txtFormat.Readonly = true;
                        this.txtOpinions.Readonly = true;
                        this.txtCheckDay.Readonly = true;
                        this.txtCheckCycle.Readonly = true;
                        this.cbIsCheckCertificate.Readonly = true;
                        this.cbIsIdentification.Readonly = true;
                        this.txtSetAccuracyGrade.Readonly = true;
                        this.txtRealAccuracyGrade.Readonly = true;
                        HandleImg = -1;
                        this.drpIsdamage.Readonly = true;
                    }
                }
                else
                {

                    State = Const.CheckEquipment_Compile;
                    this.HideOptions.Hidden = true;
                    this.rblIsAgree.Hidden = true;
                    this.plApprove2.Hidden = true;
                    this.drpHandleType.DataTextField = "Text";
                    this.drpHandleType.DataValueField = "Value";
                    this.drpHandleType.DataSource = BLL.CheckEquipmentService.GetDHandleTypeByState(State);
                    this.drpHandleType.DataBind();
                    this.drpHandleMan.DataTextField = "Text";
                    this.drpHandleMan.DataValueField = "Value";
                    this.drpHandleMan.DataSource = BLL.UserService.GetProjectUserListByProjectId(this.CurrUser.LoginProjectId);
                    this.drpHandleMan.DataBind();
                    this.drpHandleMan.SelectedIndex = 1;
                    this.cbIsCheckCertificate.Checked = true;
                    this.cbIsIdentification.Checked = true;
                }
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
                    return "审批";
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
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            if (BLL.CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.CheckEquipmentMenuId, BLL.Const.BtnSubmit))
            {
                SavePauseNotice("submit");
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.CheckEquipmentMenuId, BLL.Const.BtnSave))
            {
                SavePauseNotice("save");
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpHandleType.Items.Clear();
            this.drpHandleType.DataTextField = "Text";
            this.drpHandleType.DataValueField = "Value";
            this.drpHandleType.DataSource = BLL.CheckEquipmentService.GetDHandleTypeByState(State);
            this.drpHandleType.DataBind();
            string res = null;
            List<string> list = new List<string>();
            list.Add(Const.CheckControl_ReCompile);
            list.Add(Const.CheckControl_ReCompile2);
            var count = drpHandleType.Items.Count;
            List<ListItem> listitem = new List<ListItem>();
            if (RadioButtonList1.SelectedValue.Equals("true"))
            {
                for (int i = 0; i < count; i++)
                {
                    res = drpHandleType.Items[i].Value;
                    if (list.Contains(res))
                    {
                        var item = (drpHandleType.Items[i]);
                        listitem.Add(item);
                    }
                }
                if (listitem.Count > 0)
                {
                    for (int i = 0; i < listitem.Count; i++)
                    {
                        drpHandleType.Items.Remove(listitem[i]);
                        this.drpHandleMan.DataTextField = "Text";
                        this.drpHandleMan.DataValueField = "Value";
                        this.drpHandleMan.DataSource = BLL.UserService.GetProjectUserListByProjectId(this.CurrUser.LoginProjectId);
                        this.drpHandleMan.DataBind();
                        Funs.FineUIPleaseSelect(drpHandleMan);
                        this.drpHandleMan.SelectedIndex = 0;
                        drpHandleMan.Enabled = false;
                    }
                }
            }
            else
            {


                for (int i = 0; i < count; i++)
                {

                    res = drpHandleType.Items[i].Value;
                    if (!list.Contains(res))
                    {
                        var item = drpHandleType.Items[i];
                        listitem.Add(item);
                    }
                }
                if (listitem.Count > 0)
                {
                    for (int i = 0; i < listitem.Count; i++)
                    {
                        drpHandleType.Items.Remove(listitem[i]);
                        this.drpHandleMan.DataTextField = "Text";
                        this.drpHandleMan.DataValueField = "Value";
                        this.drpHandleMan.DataSource = BLL.UserService.GetProjectUserListByProjectId(this.CurrUser.LoginProjectId);
                        this.drpHandleMan.DataBind();
                        this.drpHandleMan.SelectedIndex = 1;
                        drpHandleMan.Enabled = true;
                    }
                }


            }
            if (count > 0)
            {
                drpHandleType.SelectedIndex = 0;
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
            if (this.drpUserUnitId.SelectedValue != BLL.Const._Null)
            {
                checkEquipment.UserUnitId = this.drpUserUnitId.SelectedValue;
            }
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
            if (this.cbIsIdentification.Checked == true)
            {
                checkEquipment.IsIdentification = true;
            }
            else
            {
                checkEquipment.IsIdentification = false;
            }
            if (this.cbIsCheckCertificate.Checked == true)
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
            string initPath = "FileUpload\\" + BLL.ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId).ProjectCode + "\\Check\\CheckEquipment\\";
            //checkEquipment.AttachUrl = this.hdFilePath.Value;
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
                    checkEquipment.State = BLL.Const.CheckEquipment_Compile;
                }
            }
            if (!string.IsNullOrEmpty(CheckEquipmentId))
            {
                Model.Check_CheckEquipment checkEquipment1 = BLL.CheckEquipmentService.GetCheckEquipmentByCheckEquipmentId(CheckEquipmentId);
                Model.Check_CheckEquipmentApprove approve1 = BLL.CheckEquipmentApproveService.GetCheckEquipmentApproveByCheckEquipmentId(CheckEquipmentId);
                if (approve1 != null && saveType == "submit")
                {
                    approve1.ApproveDate = DateTime.Now;
                    approve1.ApproveIdea = this.txtOpinions.Text.Trim();
                    if (this.drpHandleMan.SelectedValue != BLL.Const._Null)
                    {
                        approve1.ApproveMan = this.drpHandleMan.SelectedValue;
                    }
                    approve1.ApproveType = this.drpHandleType.SelectedValue;
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
            else
            {
                if (!string.IsNullOrEmpty(this.hdCheckEquipmentCode.Text))
                {
                    checkEquipment.CheckEquipmentId = this.hdCheckEquipmentCode.Text;
                }
                else
                {
                    checkEquipment.CheckEquipmentId = SQLHelper.GetNewID(typeof(Model.Check_CheckEquipment));
                }
                checkEquipment.CompileMan = this.CurrUser.UserId;
                checkEquipment.CompileDate = DateTime.Now;
                BLL.CheckEquipmentService.AddCheckEquipment(checkEquipment);
                CheckEquipmentId = checkEquipment.CheckEquipmentId;
                if (saveType == "submit")
                {
                    Model.Check_CheckEquipmentApprove approve1 = new Model.Check_CheckEquipmentApprove();
                    approve1.CheckEquipmentId = checkEquipment.CheckEquipmentId;
                    approve1.ApproveDate = DateTime.Now;
                    approve1.ApproveMan = this.CurrUser.UserId;
                    approve1.ApproveType = BLL.Const.CheckEquipment_Compile;
                    BLL.CheckEquipmentApproveService.AddCheckEquipmentApprove(approve1);

                    Model.Check_CheckEquipmentApprove approve = new Model.Check_CheckEquipmentApprove();
                    approve.CheckEquipmentId = checkEquipment.CheckEquipmentId;
                    if (this.drpHandleMan.SelectedValue != BLL.Const._Null)
                    {
                        approve.ApproveMan = this.drpHandleMan.SelectedValue;
                    }
                    approve.ApproveType = this.drpHandleType.SelectedValue;

                    BLL.CheckEquipmentApproveService.AddCheckEquipmentApprove(approve);
                }
                else
                {
                    Model.Check_CheckEquipmentApprove approve1 = new Model.Check_CheckEquipmentApprove();
                    approve1.CheckEquipmentId = checkEquipment.CheckEquipmentId;
                    approve1.ApproveMan = this.CurrUser.UserId;
                    approve1.ApproveType = BLL.Const.CheckEquipment_Compile;
                    BLL.CheckEquipmentApproveService.AddCheckEquipmentApprove(approve1);
                }
            }
            ShowNotify("提交成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        protected void btnAttach_Click1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.hdCheckEquipmentCode.Text))   //新增记录
            {
                this.hdCheckEquipmentCode.Text = SQLHelper.GetNewID(typeof(Model.Check_CheckEquipment));
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?type={0}&toKeyId={1}&path=FileUpload/CheckEquipment&menuId={2}", HandleImg, this.hdCheckEquipmentCode.Text, BLL.Const.CheckEquipmentMenuId)));
        }
    }
}