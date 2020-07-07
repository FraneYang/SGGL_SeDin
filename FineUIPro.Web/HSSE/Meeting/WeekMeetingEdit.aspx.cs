using System;
using System.Web;
using BLL;

namespace FineUIPro.Web.HSSE.Meeting
{
    public partial class WeekMeetingEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string WeekMeetingId
        {
            get
            {
                return (string)ViewState["WeekMeetingId"];
            }
            set
            {
                ViewState["WeekMeetingId"] = value;
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.InitDropDownList();                
                this.WeekMeetingId = Request.Params["WeekMeetingId"];
                if (!string.IsNullOrEmpty(this.WeekMeetingId))
                {
                    Model.Meeting_WeekMeeting weekMeeting = BLL.WeekMeetingService.GetWeekMeetingById(this.WeekMeetingId);
                    if (weekMeeting != null)
                    {
                        this.ProjectId = weekMeeting.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        ///读取编号
                        this.txtWeekMeetingCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.WeekMeetingId);
                        this.txtWeekMeetingName.Text = weekMeeting.WeekMeetingName;
                        this.txtWeekMeetingDate.Text = string.Format("{0:yyyy-MM-dd}", weekMeeting.WeekMeetingDate);
                        if (weekMeeting.AttentPersonNum != null)
                        {
                            this.txtAttentPersonNum.Text = weekMeeting.AttentPersonNum.ToString();
                        }
                        if (!string.IsNullOrEmpty(weekMeeting.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = weekMeeting.CompileMan;
                        }
                        this.txtWeekMeetingContents.Text = HttpUtility.HtmlDecode(weekMeeting.WeekMeetingContents);
                        this.txtMeetingHours.Text = Convert.ToString(weekMeeting.MeetingHours);
                        this.txtMeetingHostMan.Text = weekMeeting.MeetingHostMan;
                        this.txtAttentPerson.Text = weekMeeting.AttentPerson;
                    }
                }
                else
                {
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtWeekMeetingDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    //var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectWeekMeetingMenuId, this.ProjectId);
                    //if (codeTemplateRule != null)
                    //{
                    //    this.txtWeekMeetingContents.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    //}
                    ////自动生成编码
                    this.txtWeekMeetingCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectWeekMeetingMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtWeekMeetingName.Text = this.SimpleForm1.Title;
                    this.txtMeetingHours.Text = "1";
                    this.txtMeetingHostMan.Text = this.CurrUser.UserName;
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectWeekMeetingMenuId;
                this.ctlAuditFlow.DataId = this.WeekMeetingId;
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
            Model.Meeting_WeekMeeting weekMeeting = new Model.Meeting_WeekMeeting
            {
                ProjectId = this.ProjectId,
                WeekMeetingCode = this.txtWeekMeetingCode.Text.Trim(),
                WeekMeetingName = this.txtWeekMeetingName.Text.Trim(),
                WeekMeetingDate = Funs.GetNewDateTime(this.txtWeekMeetingDate.Text.Trim())
            };
            if (this.drpCompileMan.SelectedValue!=BLL.Const._Null)
            {
                weekMeeting.CompileMan = this.drpCompileMan.SelectedValue;
            }
            weekMeeting.AttentPersonNum = Funs.GetNewIntOrZero(this.txtAttentPersonNum.Text.Trim());
            weekMeeting.WeekMeetingContents = HttpUtility.HtmlEncode(this.txtWeekMeetingContents.Text);
            weekMeeting.CompileDate = DateTime.Now;
            weekMeeting.MeetingHours = Funs.GetNewIntOrZero(this.txtMeetingHours.Text.Trim());
            weekMeeting.MeetingHostMan = this.txtMeetingHostMan.Text.Trim();
            weekMeeting.AttentPerson = this.txtAttentPerson.Text.Trim();
            ////单据状态
            weekMeeting.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                weekMeeting.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.WeekMeetingId))
            {
                weekMeeting.WeekMeetingId = this.WeekMeetingId;
                BLL.WeekMeetingService.UpdateWeekMeeting(weekMeeting);
                BLL.LogService.AddSys_Log(this.CurrUser, weekMeeting.WeekMeetingCode, weekMeeting.WeekMeetingId, BLL.Const.ProjectWeekMeetingMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.WeekMeetingId = SQLHelper.GetNewID(typeof(Model.Meeting_WeekMeeting));
                weekMeeting.WeekMeetingId = this.WeekMeetingId;
                BLL.WeekMeetingService.AddWeekMeeting(weekMeeting);
                BLL.LogService.AddSys_Log(this.CurrUser, weekMeeting.WeekMeetingCode, weekMeeting.WeekMeetingId, BLL.Const.ProjectWeekMeetingMenuId, BLL.Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectWeekMeetingMenuId, this.WeekMeetingId, (type == BLL.Const.BtnSubmit ? true : false), weekMeeting.WeekMeetingName, "../Meeting/WeekMeetingView.aspx?WeekMeetingId={0}");
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
            if (string.IsNullOrEmpty(this.WeekMeetingId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/WeekMeetingAttachUrl&menuId={1}", WeekMeetingId,BLL.Const.ProjectWeekMeetingMenuId)));
        }
        #endregion

        protected void btnAttachUrl1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.WeekMeetingId))
            {
                SaveData(Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ClassMeetingAttachUrl&menuId={1}&strParam=1", this.WeekMeetingId, Const.ProjectWeekMeetingMenuId)));
        }

        protected void btnAttachUrl2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.WeekMeetingId))
            {
                SaveData(Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ClassMeetingAttachUrl&menuId={1}&strParam=2", this.WeekMeetingId, Const.ProjectWeekMeetingMenuId)));
        }

        /// <summary>
        ///  计算参会人数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtAttentPerson_Blur(object sender, EventArgs e)
        {
            string str = this.txtAttentPerson.Text.Trim();
            if (!string.IsNullOrEmpty(str))
            {
                if (str.Contains(","))
                {
                    this.txtAttentPersonNum.Text = str.Split(',').Length.ToString();
                }
                else if (str.Contains("，"))
                {
                    this.txtAttentPersonNum.Text = str.Split('，').Length.ToString();
                }
                else if (str.Contains(";"))
                {
                    this.txtAttentPersonNum.Text = str.Split(';').Length.ToString();
                }
                else if (str.Contains("；"))
                {
                    this.txtAttentPersonNum.Text = str.Split('；').Length.ToString();
                }
            }
        }
    }
}