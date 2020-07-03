using Apache.NMS.ActiveMQ.Threads;
using BLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.CQMS.Check
{
    public partial class ChecklistEdit : PageBase
    {
        #region 公共字段
        /// <summary>
        /// 主键
        /// </summary>
        public string CheckControlCode
        {
            get
            {
                return (string)ViewState["CheckControlCode"];
            }
            set
            {
                ViewState["CheckControlCode"] = value;
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
        #endregion
        /// <summary>
        /// 图片是否可以编辑 -1查看 0编辑
        /// </summary>
        public int QuestionImg
        {
            get
            {
                return Convert.ToInt32(ViewState["QuestionImg"]);
            }
            set
            {
                ViewState["QuestionImg"] = value;
            }
        }
        /// <summary>
        /// 整改图片
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UnitService.InitUnitDropDownList(drpUnit, this.CurrUser.LoginProjectId, false);
                UnitService.InitUnitNotsub(drpProposeUnit, this.CurrUser.LoginProjectId, false);
                UnitWorkService.InitUnitWorkDownList(drpUnitWork, this.CurrUser.LoginProjectId, false);
                CNProfessionalService.InitCNProfessionalDownList(drpCNProfessional, false);
                QualityQuestionTypeService.InitQualityQuestionTypeDownList(drpQuestionType, false);
                CheckControlCode = Request.Params["CheckControlCode"];
                plApprove1.Hidden = true;
                plApprove2.Hidden = true;
                rblIsAgree.Hidden = true;
                rblIsAgree.SelectedValue = "true";
                if (!string.IsNullOrEmpty(CheckControlCode))
                {
                    this.hdCheckControlCode.Text = CheckControlCode;
                    plApprove1.Hidden = false;
                    plApprove2.Hidden = false;
                    var dt = CheckControlApproveService.getListData(CheckControlCode);
                    gvApprove.DataSource = dt;
                    gvApprove.DataBind();
                    Model.Check_CheckControl checkControl = CheckControlService.GetCheckControl(CheckControlCode);
                    txtDocCode.Text = checkControl.DocCode;
                    if (!string.IsNullOrEmpty(checkControl.UnitId))
                    {
                        this.drpUnit.SelectedValue = checkControl.UnitId;
                    }
                    if (!string.IsNullOrEmpty(checkControl.ProposeUnitId))
                    {
                        this.drpProposeUnit.SelectedValue = checkControl.ProposeUnitId;
                    }
                    if (checkControl.UnitWorkId != null)
                    {
                        this.drpUnitWork.SelectedValue = checkControl.UnitWorkId.ToString();
                    }
                    if (!string.IsNullOrEmpty(checkControl.CNProfessionalCode))
                    {
                        this.drpCNProfessional.SelectedValue = checkControl.CNProfessionalCode;
                    }
                    if (!string.IsNullOrEmpty(checkControl.QuestionType))
                    {
                        this.drpQuestionType.SelectedValue = checkControl.QuestionType;
                    }
                    this.txtCheckSite.Text = checkControl.CheckSite;
                    if (checkControl.CheckDate != null)
                    {
                        this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", checkControl.CheckDate);
                    }
                    this.txtQuestionDef.Text = checkControl.QuestionDef;
                    this.txtRectifyOpinion.Text = checkControl.RectifyOpinion;
                    if (checkControl.LimitDate != null)
                    {
                        this.txtLimitDate.Text = string.Format("{0:yyyy-MM-dd}", checkControl.LimitDate);
                    }
                    this.txtHandleWay.Text = checkControl.HandleWay;
                    if (checkControl.RectifyDate != null)
                    {
                        this.txtRectifyDate.Text = string.Format("{0:yyyy-MM-dd}", checkControl.RectifyDate);
                    }
                    if (!string.IsNullOrEmpty(checkControl.State))
                    {
                        State = checkControl.State;
                    }
                    else
                    {
                        State = BLL.Const.CheckControl_Compile;
                        this.rblIsAgree.Visible = false;
                    }
                    if (State != BLL.Const.CheckControl_Complete)
                    {
                        //Funs.Bind(drpHandleType, CheckControlService.GetDHandleTypeByState(State));
                        CheckControlService.Init(drpHandleType, State, false);
                    }
                    if (State == BLL.Const.CheckControl_Compile)
                    {
                        this.rblIsAgree.Visible = false;
                        //Funs.Bind(drpHandleMan, UserService.GetMainUserList(this.CurrUser.LoginProjectId))
                        UserService.Init(drpHandleMan, CurrUser.LoginProjectId, false);
                        this.drpHandleMan.SelectedIndex = 1;
                    }
                    else
                    {
                        UserService.Init(drpHandleMan, CurrUser.LoginProjectId, false);
                        //Funs.Bind(drpHandleMan, UserService.GetMainUserList(this.CurrUser.LoginProjectId));
                        this.rblIsAgree.Visible = true;

                    }

                    if (State == BLL.Const.CheckControl_Audit2 || State == BLL.Const.CheckControl_Audit3 || State == BLL.Const.CheckControl_ReCompile2 || State == BLL.Const.CheckControl_Audit4 || State == BLL.Const.CheckControl_Audit5)
                    {
                        this.drpUnit.Enabled = false;
                        this.drpProposeUnit.Enabled = false;
                        this.drpUnitWork.Enabled = false;
                        this.drpCNProfessional.Enabled = false;
                        this.drpQuestionType.Enabled = false;
                        this.txtCheckSite.Enabled = false;
                        this.txtCheckDate.Enabled = false;
                        this.txtQuestionDef.Enabled = false;
                        this.txtRectifyOpinion.Enabled = false;
                        this.txtLimitDate.Enabled = false;
                        txtDocCode.Enabled = false;
                        txtProjectName.Enabled = false;
                        //imgBtnFile.Enabled = false;

                    }
                    if (State == BLL.Const.CheckControl_Compile || State == BLL.Const.CheckControl_ReCompile || State == BLL.Const.CheckControl_Audit1 || State == BLL.Const.CheckControl_Audit4 || State == BLL.Const.CheckControl_Audit5)
                    {
                        this.txtHandleWay.Enabled = false;
                        this.txtRectifyDate.Enabled = false;
                        if (State == BLL.Const.CheckControl_ReCompile)
                        {
                            this.btnAttach.Enabled = false;
                        }
                        //btnAttach.Enabled = false;
                        //lblAttach.Enabled = false;
                    }
                    if (State == BLL.Const.CheckControl_Audit4 || State == BLL.Const.CheckControl_Audit5)
                    {
                        this.txtHandleWay.Enabled = false;
                        this.txtRectifyDate.Enabled = false;
                        //btnAttach.Enabled = false;
                        //lblAttach.Enabled = false;


                    }
                    if (State == BLL.Const.CheckControl_Audit2 || State == BLL.Const.CheckControl_ReCompile2)
                    {
                        this.rblIsAgree.Visible = false;
                        txtDocCode.Enabled = false;
                    }

                    //设置流程上是否有同意不同意
                    if (State == Const.CheckControl_Audit1 || State == Const.CheckControl_Audit3 ||
                        State == Const.CheckControl_Audit4 || State == Const.CheckControl_Audit5)
                    {
                        rblIsAgree.Hidden = false;
                    }
                    else
                    {
                        rblIsAgree.Hidden = true;
                    }
                    //设置页面图片附件是否可以编辑
                    if (!State.Equals(Const.CheckControl_Complete))
                    {
                        if (State.Equals(Const.CheckControl_ReCompile) || State.Equals(Const.CheckControl_Compile) ||
                            State.Equals(Const.CheckControl_Audit1) || State.Equals(Const.CheckControl_Audit5) || State.Equals(Const.CheckControl_Audit4))
                        {
                            QuestionImg = 0;
                            HandleImg = -1;
                        }
                        else
                        {
                            QuestionImg = -1;
                            HandleImg = 0;
                        }
                    }
                }
                else
                {
                    txtDocCode.Text = "";
                    State = Const.CheckControl_Compile;
                    UserService.Init(drpHandleMan, CurrUser.LoginProjectId, false);
                    txtHandleWay.Enabled = false;
                    txtRectifyDate.Enabled = false;
                    txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now); ;
                    btnAttach.Enabled = false;
                    lblAttach.Enabled = false;
                    QuestionImg = 0;
                    CheckControlService.Init(drpHandleType, State, false);
                    string code = ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId).ProjectCode + "-06-CM03-XJ-";
                    txtDocCode.Text = BLL.SQLHelper.RunProcNewId("SpGetNewCode5", "dbo.Check_CheckControl", "DocCode", code);
                    var mainUnit = BLL.UnitService.GetUnitByProjectIdUnitTypeList(this.CurrUser.LoginProjectId,Const.ProjectUnitType_1)[0];
                    if (mainUnit != null)
                    {
                        this.drpProposeUnit.SelectedValue = mainUnit.UnitId;
                    }
                }
                HandleMan();
                txtProjectName.Text = ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId).ProjectName;
                //是否同意触发
                if (!rblIsAgree.Hidden)
                {
                    HandleType();

                }
            }
        }


        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttach_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?type={0}&toKeyId={1}&path=FileUpload/CheckControl&menuId={2}", HandleImg, CheckControlCode + "r", BLL.Const.CheckListMenuId)));
        }
        #endregion



        protected void imgBtnFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.hdCheckControlCode.Text))   //新增记录
            {
                this.hdCheckControlCode.Text = SQLHelper.GetNewID(typeof(Model.Check_CheckControl));
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?type={0}&toKeyId={1}&path=FileUpload/CheckControl&menuId={2}", QuestionImg, this.hdCheckControlCode.Text, BLL.Const.CheckListMenuId)));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //string projectId, string userId, string menuId, string buttonName)
            if (BLL.CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.CheckListMenuId, BLL.Const.BtnSave))
            {
                SavePauseNotice("save");
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                //Response.Redirect("/check/CheckList.aspx");

            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.CheckListMenuId, BLL.Const.BtnSubmit))
            {
                SavePauseNotice("submit");
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        #region 保存处理
        /// <summary>
        /// 保存方法
        /// </summary>
        private void SavePauseNotice(string saveType)
        {
            Model.Check_CheckControl checkControl = new Model.Check_CheckControl();
            checkControl.DocCode = this.txtDocCode.Text.Trim();
            checkControl.ProjectId = this.CurrUser.LoginProjectId;
            if (this.drpUnit.SelectedValue != Const._Null)
            {
                checkControl.UnitId = this.drpUnit.SelectedValue;
            }
            if (this.drpProposeUnit.SelectedValue != Const._Null)
            {
                checkControl.ProposeUnitId = this.drpProposeUnit.SelectedValue;
            }
            if (this.drpUnitWork.SelectedValue != Const._Null)
            {
                checkControl.UnitWorkId = drpUnitWork.SelectedValue;
            }
            if (this.drpCNProfessional.SelectedValue != Const._Null)
            {
                checkControl.CNProfessionalCode = this.drpCNProfessional.SelectedValue;
            }
            if (this.drpQuestionType.SelectedValue != Const._Null)
            {
                checkControl.QuestionType = this.drpQuestionType.SelectedValue;
            }
            checkControl.CheckSite = this.txtCheckSite.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtCheckDate.Text.Trim()))
            {
                checkControl.CheckDate = Convert.ToDateTime(this.txtCheckDate.Text.Trim());
            }
            checkControl.QuestionDef = this.txtQuestionDef.Text.Trim();
            checkControl.RectifyOpinion = this.txtRectifyOpinion.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtLimitDate.Text.Trim()))
            {
                checkControl.LimitDate = Convert.ToDateTime(this.txtLimitDate.Text.Trim());
            }
            //checkControl.AttachUrl = this.hdFilePath.Value;
            checkControl.HandleWay = this.txtHandleWay.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtRectifyDate.Text.Trim()))
            {
                checkControl.RectifyDate = Convert.ToDateTime(this.txtRectifyDate.Text.Trim());
            }
            //checkControl.ReAttachUrl = this.hdReFilePath.Value;
            if (saveType == "submit")
            {
                checkControl.State = drpHandleType.SelectedValue.Trim();
            }
            else
            {
                Model.Check_CheckControl checkControl1 = BLL.CheckControlService.GetCheckControl(CheckControlCode);

                if (checkControl1 != null)
                {
                    if (string.IsNullOrEmpty(checkControl1.State))
                    {
                        checkControl.State = BLL.Const.CheckControl_Compile;
                    }
                    else
                    {
                        checkControl.State = checkControl1.State;
                    }
                }
                else
                {
                    checkControl.State = BLL.Const.CheckControl_Compile;
                }
            }
            string initPath = "FileUpload\\" + BLL.ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId).ProjectCode + "\\Check\\CheckControl\\";
            if (!string.IsNullOrEmpty(CheckControlCode) && BLL.CheckControlService.GetCheckControl(Request.Params["CheckControlCode"]) != null)
            {
                Model.Check_CheckControlApprove approve1 = BLL.CheckControlApproveService.GetCheckControlApproveByCheckControlId(CheckControlCode);
                if (approve1 != null && saveType == "submit")
                {
                    approve1.ApproveDate = DateTime.Now;
                    approve1.ApproveIdea = txtOpinions.Text.Trim();
                    approve1.IsAgree = Convert.ToBoolean(this.rblIsAgree.SelectedValue);
                    BLL.CheckControlApproveService.UpdateCheckControlApprove(approve1);
                }
                if (saveType == "submit")
                {
                    Model.Check_CheckControlApprove approve = new Model.Check_CheckControlApprove();
                    approve.CheckControlCode = CheckControlCode;
                    if (this.drpHandleMan.SelectedValue != "0")
                    {
                        approve.ApproveMan = this.drpHandleMan.SelectedValue;
                    }
                    approve.ApproveType = this.drpHandleType.SelectedValue;
                    BLL.CheckControlApproveService.AddCheckControlApprove(approve);
                }
                checkControl.CheckControlCode = CheckControlCode;
                BLL.CheckControlService.UpdateCheckControl(checkControl);
            }
            else
            {
                checkControl.CheckMan = this.CurrUser.UserId;
                if (!string.IsNullOrEmpty(this.hdCheckControlCode.Text))
                {
                    checkControl.CheckControlCode = this.hdCheckControlCode.Text;
                }
                else
                {
                    checkControl.CheckControlCode = SQLHelper.GetNewID(typeof(Model.Check_CheckControl));
                }
                BLL.CheckControlService.AddCheckControl(checkControl);
                if (saveType == "submit")
                {
                    Model.Check_CheckControlApprove approve1 = new Model.Check_CheckControlApprove();
                    approve1.CheckControlCode = checkControl.CheckControlCode;
                    approve1.ApproveDate = DateTime.Now;
                    approve1.ApproveMan = this.CurrUser.UserId;
                    approve1.ApproveType = BLL.Const.CheckControl_Compile;
                    BLL.CheckControlApproveService.AddCheckControlApprove(approve1);

                    Model.Check_CheckControlApprove approve = new Model.Check_CheckControlApprove();
                    approve.CheckControlCode = checkControl.CheckControlCode;
                    if (this.drpHandleMan.SelectedValue != "0")
                    {
                        approve.ApproveMan = this.drpHandleMan.SelectedValue;
                    }
                    approve.ApproveType = this.drpHandleType.SelectedValue;

                    BLL.CheckControlApproveService.AddCheckControlApprove(approve);
                }
                else
                {
                    Model.Check_CheckControlApprove approve1 = new Model.Check_CheckControlApprove();
                    approve1.CheckControlCode = checkControl.CheckControlCode;
                    approve1.ApproveMan = this.CurrUser.UserId;
                    approve1.ApproveType = BLL.Const.CheckControl_Compile;
                    BLL.CheckControlApproveService.AddCheckControlApprove(approve1);
                }
                List<Model.Sys_User> seeUsers = BLL.UserService.GetSeeUserList(this.CurrUser.LoginProjectId, this.drpUnit.SelectedValue, this.drpCNProfessional.SelectedValue, this.drpUnitWork.SelectedValue, this.drpHandleMan.SelectedValue, this.CurrUser.UserId);
                foreach (var seeUser in seeUsers)
                {
                    Model.Check_CheckControlApprove approve = new Model.Check_CheckControlApprove();
                    approve.CheckControlCode = checkControl.CheckControlCode;
                    approve.ApproveMan = seeUser.UserId;
                    approve.ApproveType = "S";
                    BLL.CheckControlApproveService.AddCheckControlApprove(approve);
                }
            }
            BLL.LogService.AddSys_Log(this.CurrUser, checkControl.DocCode, CheckControlCode, BLL.Const.CheckListMenuId, "编辑质量巡检记录");
            //BLL.LogService.AddLog(this.CurrUser.UserId, "编辑质量巡检记录", this.CurrUser.LoginProjectId);
        }
        #endregion

        protected void drpUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            HandleMan();
        }
        /// <summary>
        /// 办理人员的自动筛选
        /// </summary>
        protected void HandleMan()
        {
            drpHandleMan.Items.Clear();
            //Funs.Bind(drpHandleMan, UserService.GetMainUserList(this.CurrUser.LoginProjectId));
            if (!string.IsNullOrEmpty(drpHandleType.SelectedText))
            {
                if (drpHandleType.SelectedText.Contains("分包"))
                {
                    UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, drpUnit.SelectedValue);

                }
                else
                {
                    UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, string.Empty);
                }
                if (drpHandleMan.Items.Count > 0)
                {
                    drpHandleMan.SelectedIndex = 0;
                }
                if (drpHandleType.SelectedValue == BLL.Const.CheckControl_Complete)
                {
                    drpHandleMan.Items.Clear();
                    drpHandleMan.Enabled = false;
                    drpHandleMan.Required = false;
                }
                else
                {
                    drpHandleMan.Enabled = true;
                    drpHandleMan.Required = true;
                }
            }


        }

        protected void drpHandleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            HandleMan();
            //if (drpHandleType.SelectedValue == BLL.Const.CheckControl_Audit2 || drpHandleType.SelectedValue == BLL.Const.CheckControl_ReCompile2)
            //{
            //    drpHandleMan.Items.Clear();
            //Funs.Bind(drpHandleMan, UserService.GetUserByUnitId(this.CurrUser.LoginProjectId, drpUnit.SelectedValue));
            //}
            //else
            //{
            //    drpHandleMan.Items.Clear();
            //    Funs.Bind(drpHandleMan, UserService.GetMainUserList(this.CurrUser.LoginProjectId));
            //}
        }

        protected void rblIsAgree_SelectedIndexChanged(object sender, EventArgs e)
        {
            HandleType();
        }

        protected void WindowAtt_Close(object sender, WindowCloseEventArgs e)
        {

        }
        //if (state == Const.CheckControl_Compile || state == Const.CheckControl_ReCompile)  //无是否同意
        //{
        //    ListItem[] lis = new ListItem[2];
        //    lis[0] = new ListItem("总包负责人审核", Const.CheckControl_Audit1);
        //    lis[1] = new ListItem("分包专业工程师回复", Const.CheckControl_Audit2);
        //    return lis;
        //}
        //else if (state == Const.CheckControl_Audit1)//有是否同意
        //{
        //    ListItem[] lis = new ListItem[2];
        //    lis[0] = new ListItem("分包专业工程师回复", Const.CheckControl_Audit2);//是 加载
        //    lis[1] = new ListItem("重新整理", Const.CheckControl_ReCompile);//否加载
        //    return lis;
        //}
        //else if (state == Const.CheckControl_Audit2 || state == Const.CheckControl_ReCompile2)//无是否同意
        //{
        //    ListItem[] lis = new ListItem[2];
        //    lis[0] = new ListItem("分包负责人审批", Const.CheckControl_Audit3);
        //    lis[1] = new ListItem("总包专业工程师确认", Const.CheckControl_Audit4);
        //    return lis;
        //}
        //else if (state == Const.CheckControl_Audit3)//有是否同意
        //{
        //    ListItem[] lis = new ListItem[2];
        //    lis[0] = new ListItem("总包专业工程师确认", Const.CheckControl_Audit4);//是 加载
        //    lis[1] = new ListItem("分包专业工程师重新回复", Const.CheckControl_ReCompile2);//否加载
        //    return lis;
        //}
        //else if (state == Const.CheckControl_Audit4)//有是否同意
        //{
        //    ListItem[] lis = new ListItem[3];
        //    lis[0] = new ListItem("总包负责人确认", Const.CheckControl_Audit5);//是 加载
        //    lis[1] = new ListItem("审批完成", Const.CheckControl_Complete);//是 加载
        //    lis[2] = new ListItem("分包专业工程师重新回复", Const.CheckControl_ReCompile2);//否加载
        //    return lis;
        //}
        //else if (state == Const.CheckControl_Audit5)//有是否同意
        //{
        //    ListItem[] lis = new ListItem[2];
        //    lis[0] = new ListItem("审批完成", Const.CheckControl_Complete);//是 加载
        //    lis[1] = new ListItem("分包专业工程师重新回复", Const.CheckControl_ReCompile2);//否加载
        //    return lis;
        //}

        /// <summary>
        /// 待办事项的下拉框的处理
        /// </summary>
        public void HandleType()
        {

            drpHandleType.Items.Clear();
            //Funs.Bind(drpHandleType, CheckControlService.GetDHandleTypeByState(State));
            CheckControlService.Init(drpHandleType, State, false);
            string res = null;
            List<string> list = new List<string>();
            list.Add(Const.CheckControl_ReCompile);
            list.Add(Const.CheckControl_ReCompile2);
            var count = drpHandleType.Items.Count;
            List<ListItem> listitem = new List<ListItem>();
            if (rblIsAgree.SelectedValue.Equals("true"))
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
                    }
                }


            }
            if (count > 0)
            {
                drpHandleType.SelectedIndex = 0;
                HandleMan();
            }
        }
    }
}