using BLL;
using System;
using System.Web;

namespace FineUIPro.Web.HSSE.Meeting
{
    public partial class AttendMeetingEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string AttendMeetingId
        {
            get
            {
                return (string)ViewState["AttendMeetingId"];
            }
            set
            {
                ViewState["AttendMeetingId"] = value;
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.InitDropDownList();
                this.AttendMeetingId = Request.Params["AttendMeetingId"];
                if (!string.IsNullOrEmpty(this.AttendMeetingId))
                {
                    Model.Meeting_AttendMeeting attendMeeting = BLL.AttendMeetingService.GetAttendMeetingById(this.AttendMeetingId);
                    if (attendMeeting != null)
                    {
                        this.ProjectId = attendMeeting.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtAttendMeetingCode.Text = CodeRecordsService.ReturnCodeByDataId(this.AttendMeetingId);
                        this.txtAttendMeetingName.Text = attendMeeting.AttendMeetingName;
                        if (attendMeeting.AttendMeetingDate != null)
                        {
                            this.txtAttendMeetingDate.Text = string.Format("{0:yyyy-MM-dd}", attendMeeting.AttendMeetingDate);
                        }
                        if (!string.IsNullOrEmpty(attendMeeting.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = attendMeeting.CompileMan;
                        }
                        this.txtAttendMeetingContents.Text = HttpUtility.HtmlDecode(attendMeeting.AttendMeetingContents);
                    }
                }
                else
                {
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtAttendMeetingDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    var codeTemplateRule = BLL.SysConstSetService.GetCodeTemplateRuleByMenuId(BLL.Const.ProjectWeekMeetingMenuId);
                    if (codeTemplateRule != null)
                    {
                        this.txtAttendMeetingContents.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    }

                    ////自动生成编码
                    this.txtAttendMeetingCode.Text = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectAttendMeetingMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtAttendMeetingName.Text = this.SimpleForm1.Title;
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectAttendMeetingMenuId;
                this.ctlAuditFlow.DataId = this.AttendMeetingId;
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
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.Meeting_AttendMeeting attendMeeting = new Model.Meeting_AttendMeeting
            {
                ProjectId = this.ProjectId,
                AttendMeetingCode = this.txtAttendMeetingCode.Text.Trim(),
                AttendMeetingName = this.txtAttendMeetingName.Text.Trim(),
                AttendMeetingDate = Funs.GetNewDateTime(this.txtAttendMeetingDate.Text.Trim())
            };
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                attendMeeting.CompileMan = this.drpCompileMan.SelectedValue;
            }
            attendMeeting.AttendMeetingContents = HttpUtility.HtmlEncode(this.txtAttendMeetingContents.Text);
            attendMeeting.CompileDate = DateTime.Now;
            attendMeeting.States = BLL.Const.State_0; 
            if (type == BLL.Const.BtnSubmit)
            {
                attendMeeting.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.AttendMeetingId))
            {
                attendMeeting.AttendMeetingId = this.AttendMeetingId;
                BLL.AttendMeetingService.UpdateAttendMeeting(attendMeeting);
                BLL.LogService.AddSys_Log(this.CurrUser, attendMeeting.AttendMeetingCode, attendMeeting.AttendMeetingId, BLL.Const.ProjectAttendMeetingMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.AttendMeetingId = SQLHelper.GetNewID(typeof(Model.Meeting_AttendMeeting));
                attendMeeting.AttendMeetingId = this.AttendMeetingId;
                BLL.AttendMeetingService.AddAttendMeeting(attendMeeting);
                BLL.LogService.AddSys_Log(this.CurrUser, attendMeeting.AttendMeetingCode, attendMeeting.AttendMeetingId, BLL.Const.ProjectAttendMeetingMenuId, BLL.Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectAttendMeetingMenuId, this.AttendMeetingId, (type == BLL.Const.BtnSubmit ? true : false), attendMeeting.AttendMeetingName, "../Meeting/AttendMeetingView.aspx?AttendMeetingId={0}");
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
            if (string.IsNullOrEmpty(this.AttendMeetingId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/AttendMeetingAttachUrl&menuId={1}", this.AttendMeetingId, BLL.Const.ProjectAttendMeetingMenuId)));
        }
        #endregion

        protected void btnAttachUrl1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.AttendMeetingId))
            {
                SaveData(Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ClassMeetingAttachUrl&menuId={1}&strParam=1", this.AttendMeetingId, Const.ProjectAttendMeetingMenuId)));
        }

        protected void btnAttachUrl2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.AttendMeetingId))
            {
                SaveData(Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ClassMeetingAttachUrl&menuId={1}&strParam=2", this.AttendMeetingId, Const.ProjectAttendMeetingMenuId)));
        }
    }
}