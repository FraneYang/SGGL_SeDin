using BLL;
using System;
using System.Web;

namespace FineUIPro.Web.HSSE.Meeting
{
    public partial class ClassMeetingEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string ClassMeetingId
        {
            get
            {
                return (string)ViewState["ClassMeetingId"];
            }
            set
            {
                ViewState["ClassMeetingId"] = value;
            }
        }
        /// <summary>
        /// 项目主键
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
        #endregion

        #region 加载
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.ProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.InitDropDownList();
                this.ClassMeetingId = Request.Params["ClassMeetingId"];
                if (!string.IsNullOrEmpty(this.ClassMeetingId))
                {
                    Model.Meeting_ClassMeeting classMeeting = BLL.ClassMeetingService.GetClassMeetingById(this.ClassMeetingId);
                    if (classMeeting != null)
                    {
                        this.ProjectId = classMeeting.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtClassMeetingCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.ClassMeetingId);
                        this.txtClassMeetingName.Text = classMeeting.ClassMeetingName;
                        this.txtClassMeetingDate.Text = string.Format("{0:yyyy-MM-dd}", classMeeting.ClassMeetingDate);
                        if (!string.IsNullOrEmpty(classMeeting.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = classMeeting.CompileMan;
                        }
                        this.txtClassMeetingContents.Text = HttpUtility.HtmlDecode(classMeeting.ClassMeetingContents);
                        this.txtAttentPersonNum.Text = classMeeting.AttentPersonNum.ToString();
                        if (!string.IsNullOrEmpty(classMeeting.UnitId))
                        {
                            this.drpUnit.SelectedValue = classMeeting.UnitId;
                            this.drpTeamGroup.Items.Clear();
                            TeamGroupService.InitTeamGroupProjectUnitDropDownList(this.drpTeamGroup, this.ProjectId, this.drpUnit.SelectedValue, true);
                            this.drpTeamGroup.SelectedValue = classMeeting.TeamGroupId;
                        }
                    }
                }
                else
                {
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtClassMeetingDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtAttentPersonNum.Text = "1";
                    var codeTemplateRule = BLL.SysConstSetService.GetCodeTemplateRuleByMenuId(BLL.Const.ProjectClassMeetingMenuId);
                    if (codeTemplateRule != null)
                    {
                        this.txtClassMeetingContents.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    }

                    ////自动生成编码
                    this.txtClassMeetingCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectClassMeetingMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtClassMeetingName.Text = this.SimpleForm1.Title;
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectClassMeetingMenuId;
                this.ctlAuditFlow.DataId = this.ClassMeetingId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }
        #endregion

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            BLL.UserService.InitUserDropDownList(this.drpCompileMan, this.ProjectId, true);
            UnitService.InitUnitByProjectIdUnitTypeDropDownList(this.drpUnit, this.ProjectId, "2", true);
            this.drpUnit.SelectedValue = this.CurrUser.UnitId;
            TeamGroupService.InitTeamGroupProjectUnitDropDownList(this.drpTeamGroup, this.ProjectId, this.drpUnit.SelectedValue, true);            
        }

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.ctlAuditFlow.NextStep == BLL.Const.State_1 && this.ctlAuditFlow.NextPerson == BLL.Const._Null)
            {
                ShowNotify("请选择下一步办理人！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.Meeting_ClassMeeting classMeeting = new Model.Meeting_ClassMeeting
            {
                ProjectId = this.ProjectId,
                ClassMeetingCode = this.txtClassMeetingCode.Text.Trim(),
                ClassMeetingName = this.txtClassMeetingName.Text.Trim(),
                ClassMeetingDate = Funs.GetNewDateTime(this.txtClassMeetingDate.Text.Trim()),
                AttentPersonNum = Funs.GetNewIntOrZero(this.txtAttentPersonNum.Text)
            };
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                classMeeting.CompileMan = this.drpCompileMan.SelectedValue;
            }
            if (this.drpUnit.SelectedValue != BLL.Const._Null)
            {
                classMeeting.UnitId = this.drpUnit.SelectedValue;
            }
            if (this.drpTeamGroup.SelectedValue != BLL.Const._Null)
            {
                classMeeting.TeamGroupId = this.drpTeamGroup.SelectedValue;
            }
            classMeeting.ClassMeetingContents = HttpUtility.HtmlEncode(this.txtClassMeetingContents.Text);
            classMeeting.CompileDate = DateTime.Now;
            classMeeting.States = BLL.Const.State_0;
            if (type==BLL.Const.BtnSubmit)
            {
                classMeeting.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.ClassMeetingId))
            {
                classMeeting.ClassMeetingId = this.ClassMeetingId;
                BLL.ClassMeetingService.UpdateClassMeeting(classMeeting);
                BLL.LogService.AddSys_Log(this.CurrUser, classMeeting.ClassMeetingCode, classMeeting.ClassMeetingId, BLL.Const.ProjectClassMeetingMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.ClassMeetingId = SQLHelper.GetNewID(typeof(Model.Meeting_ClassMeeting));
                classMeeting.ClassMeetingId = this.ClassMeetingId;
                BLL.ClassMeetingService.AddClassMeeting(classMeeting);
                BLL.LogService.AddSys_Log(this.CurrUser, classMeeting.ClassMeetingCode, classMeeting.ClassMeetingId, BLL.Const.ProjectClassMeetingMenuId, BLL.Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectClassMeetingMenuId, this.ClassMeetingId, (type == BLL.Const.BtnSubmit ? true : false), classMeeting.ClassMeetingName, "../Meeting/ClassMeetingView.aspx?ClassMeetingId={0}");
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.ClassMeetingId))
            {
                SaveData(Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ClassMeetingAttachUrl&menuId={1}", this.ClassMeetingId, Const.ProjectClassMeetingMenuId)));
        }
        #endregion

        protected void btnAttachUrl1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.ClassMeetingId))
            {
                SaveData(Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ClassMeetingAttachUrl&menuId={1}&strParam=1", this.ClassMeetingId, Const.ProjectClassMeetingMenuId)));
        }

        protected void btnAttachUrl2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.ClassMeetingId))
            {
                SaveData(Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ClassMeetingAttachUrl&menuId={1}&strParam=2", this.ClassMeetingId, Const.ProjectClassMeetingMenuId)));
        }

        protected void drpUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpTeamGroup.Items.Clear();
            TeamGroupService.InitTeamGroupProjectUnitDropDownList(this.drpTeamGroup, this.ProjectId, this.drpUnit.SelectedValue, true);
        }
    }
}