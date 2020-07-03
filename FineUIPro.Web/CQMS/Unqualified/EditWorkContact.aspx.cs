using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace FineUIPro.Web.CQMS.Unqualified
{
    public partial class EditWorkContact : PageBase
    {
        /// <summary>
        /// 主键
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
        public int ContactImg
        {
            get
            {
                return Convert.ToInt32(ViewState["ContactImg"]);
            }
            set
            {
                ViewState["ContactImg"] = value;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UnitService.GetUnit(drpUnit, CurrUser.LoginProjectId, false);
                //主送单位
                gvMainSendUnit.DataSource = UnitService.GetUnitByProjectIdList(CurrUser.LoginProjectId);
                gvMainSendUnit.DataBind();
                //抄送单位
                gvCCUnit.DataSource = UnitService.GetUnitByProjectIdList(CurrUser.LoginProjectId);
                gvCCUnit.DataBind();
                WorkContactId = Request.Params["WorkContactId"];
                HideOptions.Hidden = true;
                //plfile.Hidden = true;
                rblIsAgree.Hidden = true;
                ReOpinion.Hidden = true;
                HideReplyFile.Hidden = true;
                BindData();
                if (!string.IsNullOrEmpty(WorkContactId))
                {
                    HFWorkContactId.Text = WorkContactId;
                    Model.Unqualified_WorkContact workContact = WorkContactService.GetWorkContactByWorkContactId(WorkContactId);
                    string unitType = string.Empty;
                    txtCode.Text = workContact.Code;
                    if (!string.IsNullOrEmpty(workContact.ProposedUnitId))
                    {
                        drpUnit.SelectedValue = workContact.ProposedUnitId;
                        Model.Project_ProjectUnit unit = ProjectUnitService.GetProjectUnitByUnitIdProjectId(this.CurrUser.LoginProjectId, workContact.ProposedUnitId);
                        if (unit != null)
                        {
                            unitType = unit.UnitType;
                        }
                    }
                    if (!string.IsNullOrEmpty(workContact.MainSendUnitIds))
                    {
                        txtMainSendUnit.Values = workContact.MainSendUnitIds.Split(',');
                    }
                    if (!string.IsNullOrEmpty(workContact.CCUnitIds))
                    {
                        txtCCUnit.Values = workContact.CCUnitIds.Split(',');
                    }
                    //string isReply = workContact.IsReply;
                    if (!string.IsNullOrEmpty(workContact.IsReply))
                    {
                        rblIsReply.SelectedValue = workContact.IsReply;
                    }
                    txtCause.Text = workContact.Cause;
                    txtContents.Text = workContact.Contents;
                    txtReOpinion.Text = workContact.ReOpinion;
                    if (!string.IsNullOrEmpty(workContact.State))
                    {
                        State = workContact.State;
                    }
                    else
                    {
                        State = Const.WorkContact_Compile;
                        HideOptions.Hidden = true;
                        //Url.Visible = false;//附件查看权限-1
                        ContactImg = -1;
                        rblIsAgree.Hidden = true;
                    }
                    if (State != Const.WorkContact_Complete)
                    {
                        WorkContactService.InitHandleType(drpHandleType, false, State, unitType, workContact.IsReply);
                    }
                    if (State == Const.WorkContact_Compile || State == Const.WorkContact_ReCompile)
                    {
                        HideOptions.Hidden = true;
                        ContactImg = 0;
                        rblIsAgree.Hidden = true;
                        drpHandleMan.Enabled = true;
                        drpHandleMan.Required = true;
                        UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, string.Empty);
                        //drpHandleMan.Items.AddRange(UserService.GetAllUserList(CurrUser.LoginProjectId));
                        drpHandleMan.SelectedIndex = 0;
                    }
                    else
                    {
                        //------------
                        UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, string.Empty);
                        //drpHandleMan.Items.AddRange(UserService.GetAllUserList(CurrUser.LoginProjectId));
                        HideOptions.Hidden = false;
                        //Url.Visible = true; 附件查看权限 - 1
                        ContactImg = -1;
                        rblIsAgree.Hidden = false;
                    }
                    if (drpHandleType.SelectedValue == Const.WorkContact_Complete)
                    {
                        rblIsAgree.Hidden = false;
                        drpHandleMan.Enabled = false;
                        drpHandleMan.Required = false;

                    }
                    else
                    {
                        drpHandleMan.Items.Clear();
                        UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, string.Empty);
                        drpHandleMan.Enabled = true;
                        drpHandleMan.Required = true;
                    }
                    if (State == Const.WorkContact_Complete || !string.IsNullOrEmpty(Request.Params["see"]))
                    {
                        btnSave.Hidden = true;
                        btnSubmit.Hidden = true;
                        next.Hidden = true;
                        Model.Unqualified_WorkContactApprove approve = WorkContactApproveService.GetSee(WorkContactId, CurrUser.UserId);
                        if (approve != null)
                        {
                            approve.ApproveDate = DateTime.Now;
                            WorkContactApproveService.UpdateWorkContactApprove(approve);
                        }
                    }
                    if (unitType == "3")   //施工分包商发起
                    {
                        if (rblIsReply.SelectedValue == "1")    //需要回复
                        {
                            ContactImg = 0;
                            if (State == Const.WorkContact_Audit2 || State == Const.WorkContact_Audit2R || State == Const.WorkContact_Audit3)
                            {
                                //txtCode.Enabled = false;
                                //drpUnit.Enabled = false;
                                //txtMainSendUnit.Enabled = false;
                                //txtCCUnit.Enabled = false;
                                //rblIsReply.Enabled = false;
                                //txtCause.Enabled = false;
                                //txtContents.Enabled = false;
                                //ContactImg = -1;
                                //txtProjectName.Enabled = true;
                                DoEabled();
                                //imgfile.Visible = false;附件查看权限 - 1
                            }
                            if (State == Const.WorkContact_Audit2 || State == Const.WorkContact_Audit2R)
                            {
                                //lbStar.Visible = true;
                                //rfvStar.Enabled = true;
                                txtOpinions.Enabled = true;
                            }
                            if (State == Const.WorkContact_Audit1)
                            {
                                DoEdit();
                            }
                        }
                        if (State == Const.WorkContact_Audit3)
                        {
                            drpHandleType.Enabled = true;
                        }

                    }
                    else //总包发起
                    {
                        if (rblIsReply.SelectedValue == "1")    //需要回复
                        {
                            if (State == Const.WorkContact_Audit4 || State == Const.WorkContact_Audit1R || State == Const.WorkContact_Audit1)
                            {
                                //txtCode.Enabled = false;
                                //drpUnit.Enabled = false;
                                //txtMainSendUnit.Enabled = false;
                                //txtCCUnit.Enabled = false;
                                //txtProjectName.Enabled = false;
                                //rblIsReply.Enabled = false;
                                //txtCause.Enabled = false;
                                //txtContents.Enabled = false;
                                //ContactImg = -1;
                                DoEabled();
                                //imgfile.Visible = false;//权限等于-1
                            }

                            if (State == Const.WorkContact_Audit4 || State == Const.WorkContact_Audit1R)
                            {
                                rblIsAgree.Hidden = true;
                                txtOpinions.Enabled = true;
                            }

                            //if (State == Const.WorkContact_Audit1)
                            //{
                            //    drpHandleType.Enabled = true;
                            //}
                        }

                    }

                    if (!State.Equals(Const.TechnicalContactList_Complete))
                    {
                        if (State.Equals(Const.WorkContact_ReCompile) || State.Equals(Const.WorkContact_Compile) ||
                          State.Equals(Const.WorkContact_Audit1) || State.Equals(Const.WorkContact_Audit4))
                        {
                            DoEabled();
                        }
                        if (State.Equals(Const.WorkContact_Compile) || State.Equals(Const.WorkContact_ReCompile))
                        {
                            DoEdit();
                        }
                    }
                    drpUnit_SelectedIndexChanged(null, null);
                    if (rblIsAgree.Hidden == false)
                    {
                        Agree();
                    }
                    Reply(unitType);
                    if (State == Const.WorkContact_Compile || State == Const.WorkContact_ReCompile)
                    {
                        HideOptions.Hidden = true;
                    }
                    //设置回复审批场景下的操作
                }
                else
                {
                    //string perfix = EditNoService.GetEditNoById(CurrUser.LoginProjectId, CurrUser.LoginProjectId).NoContent + "-" + EditNoService.GetEditNoById("05", CurrUser.LoginProjectId).NoContent + "-";
                    //txtCode.Text = SQLHelper.RunProcNewId("SpGetNewHazardCode", "dbo.Unqualified_WorkContact", "Code", perfix);
                    State = Const.WorkContact_Compile;
                    //UserService.Init(drpHandleMan, CurrUser.LoginProjectId, false);
                    UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, string.Empty);
                    drpHandleMan.SelectedIndex = 0;
                    plApprove2.Hidden = true;
                    txtCode.Text = SQLHelper.RunProcNewId2("SpGetNewCode3ByProjectId", "dbo.Unqualified_WorkContact", "Code", CurrUser.LoginProjectId);
                    var mainUnit = UnitService.GetUnitByProjectIdUnitTypeList(CurrUser.LoginProjectId, Const.ProjectUnitType_1)[0];
                    if (mainUnit != null)
                    {
                        drpUnit.SelectedValue = mainUnit.UnitId;
                    }
                    drpUnit_SelectedIndexChanged(null, null);
                }

                txtProjectName.Text = ProjectService.GetProjectByProjectId(CurrUser.LoginProjectId).ProjectName;
            }
        }

        private void BindData()
        {
            var table = WorkContactApproveService.getListData(WorkContactId);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        public void DoEabled()
        {
            txtCode.Enabled = false;
            drpUnit.Enabled = false;
            txtMainSendUnit.Enabled = false;
            txtCCUnit.Enabled = false;
            txtProjectName.Enabled = false;
            rblIsReply.Enabled = false;
            txtCause.Enabled = false;
            txtContents.Enabled = false;
            ContactImg = -1;
            txtProjectName.Enabled = false;
        }

        public void DoEdit()
        {
            txtCode.Enabled = true;
            drpUnit.Enabled = true;
            txtMainSendUnit.Enabled = true;
            txtCCUnit.Enabled = true;
            txtProjectName.Enabled = true;
            rblIsReply.Enabled = true;
            txtCause.Enabled = true;
            txtContents.Enabled = true;
            ContactImg = 0;
            txtProjectName.Enabled = true;
        }
        /// <summary>
        /// 附件内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgBtnFile_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(HFWorkContactId.Text))   //新增记录
            {
                HFWorkContactId.Text = SQLHelper.GetNewID(typeof(Model.Unqualified_WorkContact));
            }

            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(
            String.Format("../../AttachFile/webuploader.aspx?type={0}&toKeyId={1}&path=FileUpload/WorkContact&menuId={2}",
            ContactImg, HFWorkContactId.Text, Const.WorkContactMenuId)));
        }

        #region 单位选择操作
        protected void drpUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpHandleType.Items.Clear();
            string unitType = string.Empty;
            var unit = ProjectUnitService.GetProjectUnitByUnitIdProjectId(this.CurrUser.LoginProjectId, drpUnit.SelectedValue);
            if (unit != null)
            {
                unitType = unit.UnitType;
            }
            WorkContactService.InitHandleType(drpHandleType, false, State, unitType, rblIsReply.SelectedValue);
            drpHandleType.SelectedIndex = 0;
            drpHandleType_SelectedIndexChanged(null, null);
        }
        #endregion
        /// <summary>      
        /// 答复变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblIsReply_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpHandleType.Items.Clear();
            string unitType = string.Empty;
            var unit = ProjectUnitService.GetProjectUnitByUnitIdProjectId(this.CurrUser.LoginProjectId, drpUnit.SelectedValue);
            if (unit != null)
            {
                unitType = unit.UnitType;
            }
            WorkContactService.InitHandleType(drpHandleType, false, State, unitType, rblIsReply.SelectedValue);
            //drpHandleType.Items.AddRange(WorkContactService.GetDHandleTypeByState(State, unitType, rblIsReply.SelectedValue));
            //txtMainSendUnit.Text = UnitService.GetUnitName(hdMainSendUnitId.Value);
            //txtCCUnit.Text = UnitService.GetUnitName(hdCCUnitId.Value);
            if (rblIsReply.SelectedValue == "2" && State == Const.WorkContact_Audit1)
            {
                rblIsAgree.Hidden = false;
                drpHandleMan.Enabled = false;
                drpHandleMan.SelectedIndex = 0;
                drpHandleMan.Required = true;
            }
            else
            {

                drpHandleMan.Enabled = true;
                drpHandleMan.Required = true;
            }

        }
        #region 设置回复审批场景下的操作
        /// <summary>
        /// 设置回复审批场景下的操作
        /// </summary>

        public void Reply(string type)
        {
            if (rblIsReply.SelectedValue.Equals("1"))
            {
                if (type.Equals(Const.ProjectUnitType_1))
                {
                    if (State.Equals(Const.WorkContact_Audit1) || State.Equals(Const.WorkContact_Audit1R)
                        || State.Equals(Const.WorkContact_Audit4))
                    {
                        HideReplyFile.Hidden = false;
                        ReOpinion.Hidden = false;
                        HideOptions.Hidden = true;
                        txtReOpinion.Required = true;
                        txtReOpinion.ShowRedStar = true;
                    }
                    else
                    {
                        HideReplyFile.Hidden = true;
                        ReOpinion.Hidden = true;
                        HideOptions.Hidden = false;
                    }

                    if (drpHandleType.SelectedValue.Equals(Const.WorkContact_Audit1) || drpHandleType.SelectedValue.Equals(Const.WorkContact_Audit4)
                        || drpHandleType.SelectedValue.Equals(Const.WorkContact_Audit1R))
                    {
                        //HideReplyFile.Visible = true;
                        //txtMainSendUnit.Values.Join(",")
                        var str = txtMainSendUnit.Values.ToList();
                        drpHandleMan.Items.Clear();
                        UserService.InitUsersDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, string.Join(",", str));
                    }

                }
                if (type.Equals(Const.ProjectUnitType_2))
                {
                    if (State.Equals(Const.WorkContact_Audit2) || State.Equals(Const.WorkContact_Audit3)
                            || State.Equals(Const.WorkContact_Audit2R))
                    {
                        HideReplyFile.Hidden = false;
                        ReOpinion.Hidden = false;
                        HideOptions.Hidden = true;
                        txtReOpinion.Required = true;
                        txtReOpinion.ShowRedStar = true;
                    }
                    else
                    {
                        HideReplyFile.Hidden = true;
                        ReOpinion.Hidden = true;
                        HideOptions.Hidden = false;
                    }

                    if (drpHandleType.SelectedValue.Equals(Const.WorkContact_Audit2) || drpHandleType.SelectedValue.Equals(Const.WorkContact_Audit3)
                    || drpHandleType.SelectedValue.Equals(Const.WorkContact_Audit2R))
                    {
                        var str = txtMainSendUnit.Values.ToList();
                        drpHandleMan.Items.Clear();
                        UserService.InitUsersDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, string.Join(",", str));
                    }

                }
            }
        }
        #endregion

        #region  保存
        /// <summary>
        /// 保存开工报告
        /// </summary>
        private void SavePauseNotice(string saveType)
        {
            Model.Unqualified_WorkContact workContact = new Model.Unqualified_WorkContact();
            workContact.Code = txtCode.Text.Trim();
            workContact.ProjectId = CurrUser.LoginProjectId;
            if (drpUnit.SelectedValue != "0")
            {
                workContact.ProposedUnitId = drpUnit.SelectedValue;
            }
            if (txtMainSendUnit.Values != null)
            {
                workContact.MainSendUnitIds = string.Join(",", txtMainSendUnit.Values);
            }
            if (txtCCUnit.Values != null)
            {
                workContact.CCUnitIds = string.Join(",", txtCCUnit.Values);
            }
            if (!string.IsNullOrEmpty(rblIsReply.SelectedValue))
            {
                workContact.IsReply = rblIsReply.SelectedValue;
            }
            else
            {
                workContact.IsReply = null;
            }
            workContact.Cause = txtCause.Text.Trim();
            workContact.Contents = txtContents.Text.Trim();
            workContact.ReOpinion = txtReOpinion.Text.Trim();
            if (saveType == "submit")
            {
                workContact.State = drpHandleType.SelectedValue.Trim();
            }
            else
            {
                Model.Unqualified_WorkContact workContact1 = WorkContactService.GetWorkContactByWorkContactId(WorkContactId);
                if (workContact1 != null)
                {
                    if (string.IsNullOrEmpty(workContact1.State))
                    {
                        workContact.State = Const.WorkContact_Compile;
                    }
                    else
                    {
                        workContact.State = workContact1.State;
                    }
                }
                else
                {
                    workContact.State = Const.WorkContact_Compile;
                }
            }

            if (!string.IsNullOrEmpty(WorkContactId) && WorkContactService.GetWorkContactByWorkContactId(Request.Params["WorkContactId"]) != null)
            {
                Model.Unqualified_WorkContact workContact1 = WorkContactService.GetWorkContactByWorkContactId(WorkContactId);
                Model.Unqualified_WorkContactApprove approve1 = WorkContactApproveService.GetWorkContactApproveByWorkContactId(WorkContactId);
                if (approve1 != null && saveType == "submit")
                {
                    approve1.IsAgree = Convert.ToBoolean(rblIsAgree.SelectedValue);
                    approve1.ApproveDate = DateTime.Now;
                    approve1.ApproveIdea = txtOpinions.Text.Trim();
                    WorkContactApproveService.UpdateWorkContactApprove(approve1);
                }
                if (saveType == "submit")
                {
                    Model.Unqualified_WorkContactApprove approve = new Model.Unqualified_WorkContactApprove();
                    approve.WorkContactId = workContact1.WorkContactId;
                    if (drpHandleMan.SelectedValue != "0")
                    {
                        approve.ApproveMan = drpHandleMan.SelectedValue;
                    }
                    approve.ApproveType = drpHandleType.SelectedValue;
                    WorkContactApproveService.AddWorkContactApprove(approve);
                    if (workContact.IsReply == "2" && drpHandleType.SelectedValue == Const.WorkContact_Complete)
                    {
                        List<Model.Sys_User> seeUsers = new List<Model.Sys_User>();
                        seeUsers.AddRange(UserService.GetSeeUserList4(CurrUser.LoginProjectId, workContact.ProposedUnitId, workContact.MainSendUnitIds, workContact.CCUnitIds));
                        seeUsers = seeUsers.Distinct().ToList();
                        foreach (var seeUser in seeUsers)
                        {
                            Model.Unqualified_WorkContactApprove approveS = new Model.Unqualified_WorkContactApprove();
                            approveS.WorkContactId = WorkContactId;
                            approveS.ApproveMan = seeUser.UserId;
                            approveS.ApproveType = "S";
                            WorkContactApproveService.AddWorkContactApprove(approveS);
                        }
                    }
                }
                workContact.WorkContactId = WorkContactId;
                workContact.ReOpinion = txtReOpinion.Text.Trim();
                WorkContactService.UpdateWorkContact(workContact);
            }
            else
            {
                if (!string.IsNullOrEmpty(HFWorkContactId.Text))
                {
                    workContact.WorkContactId = HFWorkContactId.Text;
                }
                else
                {
                    workContact.WorkContactId = SQLHelper.GetNewID(typeof(Model.Unqualified_WorkContact));
                }
                workContact.ReOpinion = txtReOpinion.Text.Trim();
                workContact.CompileMan = CurrUser.UserId;
                workContact.CompileDate = DateTime.Now;
                WorkContactService.AddWorkContact(workContact);
                if (saveType == "submit")
                {
                    Model.Unqualified_WorkContactApprove approve1 = new Model.Unqualified_WorkContactApprove();
                    approve1.WorkContactId = workContact.WorkContactId;
                    approve1.ApproveDate = DateTime.Now;
                    approve1.ApproveMan = CurrUser.UserId;
                    approve1.ApproveType = Const.WorkContact_Compile;
                    WorkContactApproveService.AddWorkContactApprove(approve1);

                    Model.Unqualified_WorkContactApprove approve = new Model.Unqualified_WorkContactApprove();
                    approve.WorkContactId = workContact.WorkContactId;
                    if (drpHandleMan.SelectedValue != "0")
                    {
                        approve.ApproveMan = drpHandleMan.SelectedValue;
                    }
                    approve.ApproveType = drpHandleType.SelectedValue;

                    WorkContactApproveService.AddWorkContactApprove(approve);
                }
                else
                {
                    Model.Unqualified_WorkContactApprove approve1 = new Model.Unqualified_WorkContactApprove();
                    approve1.WorkContactId = workContact.WorkContactId;
                    approve1.ApproveMan = CurrUser.UserId;
                    approve1.ApproveType = Const.WorkContact_Compile;
                    WorkContactApproveService.AddWorkContactApprove(approve1);
                }
                List<string> list = new List<string>();
                if (txtMainSendUnit.Values != null)
                {
                    string[] strs1 = txtMainSendUnit.Values;
                    foreach (var strs in strs1)
                    {
                        list.Add(strs);
                    }
                }

                if (txtCCUnit.Values != null)
                {
                    string[] strs2 = txtCCUnit.Values;
                    foreach (var strs in strs2)
                    {
                        list.Add(strs);
                    }
                }
                List<Model.Sys_User> seeUsers = new List<Model.Sys_User>();
                foreach (var item in list)
                {
                    var u = UserService.GetSeeUserListByRole(CurrUser.LoginProjectId, item,
                        Const.ProjectManager, Const.ZXPrincipalRole, Const.TechnicalPrincipalRole, Const.ConstructionManager);
                    if (u.Count > 0)
                    {
                        seeUsers.AddRange(u);
                    }
                }
                seeUsers = seeUsers.Distinct().ToList();
                foreach (var seeUser in seeUsers)
                {
                    Model.Unqualified_WorkContactApprove approve = new Model.Unqualified_WorkContactApprove();
                    approve.WorkContactId = workContact.WorkContactId;
                    approve.ApproveMan = seeUser.UserId;
                    approve.ApproveType = "S";
                    WorkContactApproveService.AddWorkContactApprove(approve);
                }
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            LogService.AddSys_Log(CurrUser, workContact.Code, WorkContactId, Const.WorkContactMenuId, "工作联系单");
        }
        #endregion

        protected void drpHandleType_SelectedIndexChanged(object sender, EventArgs e)
        {

            drpHandleMan.Items.Clear();
            if (drpHandleType.SelectedText.Contains("分包") || drpHandleType.SelectedText.Contains("编制"))
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

            if (drpHandleType.SelectedValue == Const.WorkContact_Complete)
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {


            if (CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, CurrUser.UserId, Const.WorkContactMenuId, Const.BtnSubmit))
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
            if (CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, CurrUser.UserId, Const.WorkContactMenuId, Const.BtnSave))
            {
                SavePauseNotice("save");

                //Response.Redirect("/check/CheckList.aspx");

            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        protected void rblIsAgree_SelectedIndexChanged(object sender, EventArgs e)
        {
            Agree();
            //string unitType = string.Empty;
            //string isReply = this.rblIsReply.SelectedValue;
            //this.drpHandleMan.Enabled = true;
            //drpHandleMan.Required = true;
            //string State = WorkContactService.GetWorkContactByWorkContactId(WorkContactId).State;
        }
        /// <summary>
        /// 是否同意的逻辑处理
        /// </summary>
        public void Agree()
        {
            string unitType = string.Empty;
            Model.Project_ProjectUnit unit = ProjectUnitService.GetProjectUnitByUnitIdProjectId(this.CurrUser.LoginProjectId, drpUnit.SelectedValue);
            if (unit != null)
            {
                unitType = unit.UnitType;
            }
            string isReply = rblIsReply.SelectedValue;
            drpHandleType.Items.Clear();
            string State = WorkContactService.GetWorkContactByWorkContactId(WorkContactId).State;
            WorkContactService.InitHandleType(drpHandleType, false, State, unitType, rblIsReply.SelectedValue);
            if (rblIsAgree.SelectedValue.Equals("true"))
            {
                if (unitType == "3")  //分包发起
                {
                    if (State == Const.WorkContact_Audit1)
                    {
                        DoEdit();
                    }
                }
                else   //总包发起
                {
                    if (isReply == "1")  //需要回复
                    {
                        if (State == Const.WorkContact_Audit1)
                        {
                            drpHandleMan.Enabled = false;
                            drpHandleMan.Required = false;
                        }
                    }
                    else  //不需回复
                    {
                        if (State == Const.WorkContact_Audit3)
                        {
                            drpHandleMan.Enabled = false;
                            drpHandleMan.Required = false;
                        }
                    }

                }
                if (drpHandleType.Items.Count == 2)
                {
                    drpHandleType.Readonly = true;
                }
                drpHandleType.SelectedIndex = 0;
                if (drpHandleType.SelectedValue == Const.WorkContact_Complete)
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
                //List<>
                if (drpHandleType.Items.Count > 0)
                {
                    List<ListItem> lst = new List<ListItem>();
                    foreach (var item in drpHandleType.Items)
                    {
                        int index = drpHandleType.Items.IndexOf(item);
                        if (index != 0)
                        {
                            lst.Add(item);
                            //drpHandleType.Items.Remove(item);
                        }

                    }
                    if (lst.Count > 0)
                    {
                        foreach (var item in lst)
                        {
                            drpHandleType.Items.Remove(item);
                        }
                    }

                }
            }
            else
            {
                drpHandleMan.Items.Clear();
                //Funs.FineUIPleaseSelect(drpHandleMan);
                if (drpHandleType.Items.Count == 2)
                {
                    drpHandleType.Readonly = true;
                }
                drpHandleType.SelectedIndex = 1;
                if (drpHandleType.Items.Count > 0)
                {
                    drpHandleType.Items.RemoveAt(0);
                }
                if (drpHandleType.SelectedValue == Const.WorkContact_ReCompile)
                {
                    drpHandleMan.Enabled = true;
                    UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, drpUnit.SelectedValue);
                    drpHandleMan.Required = true;
                }
                else
                {
                    drpHandleMan.Enabled = true;
                    drpHandleMan.Required = true;

                }

            }

            Reply(unitType);

            if (drpHandleMan.Items.Count > 0)
            {

                drpHandleMan.SelectedIndex = 0;
            }
        }

        protected void ReplyFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HFWorkContactId.Text))   //新增记录
            {
                HFWorkContactId.Text = SQLHelper.GetNewID(typeof(Model.Unqualified_WorkContact));
            }

            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(
            String.Format("../../AttachFile/webuploader.aspx?type={0}&toKeyId={1}&path=FileUpload/WorkContact&menuId={2}",
            0, HFWorkContactId.Text + "r", Const.WorkContactMenuId)));
        }
    }
}