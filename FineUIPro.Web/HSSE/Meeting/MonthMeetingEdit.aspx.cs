using BLL;
using System;
using System.Web;

namespace FineUIPro.Web.HSSE.Meeting
{
    public partial class MonthMeetingEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string MonthMeetingId
        {
            get
            {
                return (string)ViewState["MonthMeetingId"];
            }
            set
            {
                ViewState["MonthMeetingId"] = value;
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
                this.MonthMeetingId = Request.Params["MonthMeetingId"];
                if (!string.IsNullOrEmpty(this.MonthMeetingId))
                {
                    Model.Meeting_MonthMeeting monthMeeting = BLL.MonthMeetingService.GetMonthMeetingById(this.MonthMeetingId);
                    if (monthMeeting!=null)
                    {
                        this.ProjectId = monthMeeting.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtMonthMeetingCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.MonthMeetingId);
                        this.txtMonthMeetingName.Text = monthMeeting.MonthMeetingName;
                        this.txtMonthMeetingDate.Text = string.Format("{0:yyyy-MM-dd}", monthMeeting.MonthMeetingDate);
                        if (!string.IsNullOrEmpty(monthMeeting.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = monthMeeting.CompileMan;
                        }
                        if (monthMeeting.AttentPersonNum != null)
                        {
                            this.txtAttentPersonNum.Text = monthMeeting.AttentPersonNum.ToString();
                        }
                        this.txtMonthMeetingContents.Text = HttpUtility.HtmlDecode(monthMeeting.MonthMeetingContents);
                        this.txtMeetingHours.Text = Convert.ToString(monthMeeting.MeetingHours);
                        this.txtMeetingHostMan.Text = monthMeeting.MeetingHostMan;
                        this.txtAttentPerson.Text = monthMeeting.AttentPerson;
                    }
                }
                else
                {
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtMonthMeetingDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    var codeTemplateRule = BLL.SysConstSetService.GetCodeTemplateRuleByMenuId(BLL.Const.ProjectWeekMeetingMenuId);
                    if (codeTemplateRule != null)
                    {
                        this.txtMonthMeetingContents.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    }
                    ////自动生成编码
                    this.txtMonthMeetingCode.Text = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectMonthMeetingMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtMonthMeetingName.Text = this.SimpleForm1.Title;
                    this.txtMeetingHours.Text = "1";
                    this.txtMeetingHostMan.Text = this.CurrUser.UserName;
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectMonthMeetingMenuId;
                this.ctlAuditFlow.DataId = this.MonthMeetingId;
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
            UserService.InitUserDropDownList(this.drpCompileMan, this.ProjectId, true);
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
        /// 提交
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
            Model.Meeting_MonthMeeting monthMeeting = new Model.Meeting_MonthMeeting
            {
                ProjectId = this.ProjectId,
                MonthMeetingCode = this.txtMonthMeetingCode.Text.Trim(),
                MonthMeetingName = this.txtMonthMeetingName.Text.Trim(),
                MonthMeetingDate = Funs.GetNewDateTime(this.txtMonthMeetingDate.Text.Trim())
            };
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                monthMeeting.CompileMan = this.drpCompileMan.SelectedValue;
            }
            monthMeeting.AttentPersonNum = Funs.GetNewIntOrZero(this.txtAttentPersonNum.Text.Trim());
            monthMeeting.MonthMeetingContents = HttpUtility.HtmlEncode(this.txtMonthMeetingContents.Text);
            monthMeeting.CompileDate = DateTime.Now;
            monthMeeting.States = BLL.Const.State_0;
            monthMeeting.MeetingHours = Funs.GetNewInt(this.txtMeetingHours.Text.Trim());
            monthMeeting.MeetingHostMan = this.txtMeetingHostMan.Text.Trim();
            monthMeeting.AttentPerson = this.txtAttentPerson.Text.Trim();
            if (type == BLL.Const.BtnSubmit)
            {
                monthMeeting.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.MonthMeetingId))
            {
                monthMeeting.MonthMeetingId = this.MonthMeetingId;
                BLL.MonthMeetingService.UpdateMonthMeeting(monthMeeting);
                BLL.LogService.AddSys_Log(this.CurrUser, monthMeeting.MonthMeetingCode, monthMeeting.MonthMeetingId, BLL.Const.ProjectMonthMeetingMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.MonthMeetingId = SQLHelper.GetNewID(typeof(Model.Meeting_MonthMeeting));
                monthMeeting.MonthMeetingId = this.MonthMeetingId;
                BLL.MonthMeetingService.AddMonthMeeting(monthMeeting);
                BLL.LogService.AddSys_Log(this.CurrUser, monthMeeting.MonthMeetingCode, monthMeeting.MonthMeetingId, BLL.Const.ProjectMonthMeetingMenuId, BLL.Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectMonthMeetingMenuId, this.MonthMeetingId, (type == BLL.Const.BtnSubmit ? true : false), monthMeeting.MonthMeetingName, "../Meeting/MonthMeetingView.aspx?MonthMeetingId={0}");
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
            if (string.IsNullOrEmpty(this.MonthMeetingId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/MonthMeetingAttachUrl&menuId={1}", this.MonthMeetingId, BLL.Const.ProjectMonthMeetingMenuId)));
        }
        #endregion

        protected void btnAttachUrl1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.MonthMeetingId))
            {
                SaveData(Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ClassMeetingAttachUrl&menuId={1}&strParam=1", this.MonthMeetingId, Const.ProjectMonthMeetingMenuId)));
        }

        protected void btnAttachUrl2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.MonthMeetingId))
            {
                SaveData(Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ClassMeetingAttachUrl&menuId={1}&strParam=2", this.MonthMeetingId, Const.ProjectMonthMeetingMenuId)));
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