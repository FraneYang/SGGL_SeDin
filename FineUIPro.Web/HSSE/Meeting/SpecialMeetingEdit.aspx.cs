using BLL;
using System;
using System.Web;

namespace FineUIPro.Web.HSSE.Meeting
{
    public partial class SpecialMeetingEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string SpecialMeetingId
        {
            get
            {
                return (string)ViewState["SpecialMeetingId"];
            }
            set
            {
                ViewState["SpecialMeetingId"] = value;
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
                this.SpecialMeetingId = Request.Params["SpecialMeetingId"];
                if (!string.IsNullOrEmpty(this.SpecialMeetingId))
                {
                    Model.Meeting_SpecialMeeting specialMeeting = BLL.SpecialMeetingService.GetSpecialMeetingById(this.SpecialMeetingId);
                    if (specialMeeting != null)
                    {
                        this.ProjectId = specialMeeting.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtSpecialMeetingCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.SpecialMeetingId);
                        this.txtSpecialMeetingName.Text = specialMeeting.SpecialMeetingName;
                        this.txtSpecialMeetingDate.Text = string.Format("{0:yyyy-MM-dd}", specialMeeting.SpecialMeetingDate);
                        if (specialMeeting.AttentPersonNum != null)
                        {
                            this.txtAttentPersonNum.Text = specialMeeting.AttentPersonNum.ToString();
                        }
                        if (!string.IsNullOrEmpty(specialMeeting.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = specialMeeting.CompileMan;
                        }
                        this.txtSpecialMeetingContents.Text = HttpUtility.HtmlDecode(specialMeeting.SpecialMeetingContents);
                        this.txtMeetingHours.Text = Convert.ToString(specialMeeting.MeetingHours);
                        this.txtMeetingHostMan.Text = specialMeeting.MeetingHostMan;
                        this.txtAttentPerson.Text = specialMeeting.AttentPerson;
                    }
                }
                else
                {
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtSpecialMeetingDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                     var codeTemplateRule = BLL.SysConstSetService.GetCodeTemplateRuleByMenuId(BLL.Const.ProjectWeekMeetingMenuId);
                    if (codeTemplateRule != null)
                    {
                        this.txtSpecialMeetingContents.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    }
                    ////自动生成编码
                    this.txtSpecialMeetingCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectSpecialMeetingMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtSpecialMeetingName.Text = this.SimpleForm1.Title;
                    this.txtMeetingHours.Text = "1";
                    this.txtMeetingHostMan.Text = this.CurrUser.UserName;
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectSpecialMeetingMenuId;
                this.ctlAuditFlow.DataId = this.SpecialMeetingId;
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
            Model.Meeting_SpecialMeeting specialMeeting = new Model.Meeting_SpecialMeeting
            {
                ProjectId = this.ProjectId,
                SpecialMeetingCode = this.txtSpecialMeetingCode.Text.Trim(),
                SpecialMeetingName = this.txtSpecialMeetingName.Text.Trim(),
                SpecialMeetingDate = Funs.GetNewDateTime(this.txtSpecialMeetingDate.Text.Trim())
            };
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                specialMeeting.CompileMan = this.drpCompileMan.SelectedValue;
            }
            specialMeeting.AttentPersonNum = Funs.GetNewIntOrZero(this.txtAttentPersonNum.Text.Trim());
            specialMeeting.SpecialMeetingContents = HttpUtility.HtmlEncode(this.txtSpecialMeetingContents.Text);
            specialMeeting.CompileDate = DateTime.Now;
            specialMeeting.States = BLL.Const.State_0;
            specialMeeting.MeetingHours = Funs.GetNewInt(this.txtMeetingHours.Text.Trim());
            specialMeeting.MeetingHostMan = this.txtMeetingHostMan.Text.Trim();
            specialMeeting.AttentPerson = this.txtAttentPerson.Text.Trim();
            if (type == BLL.Const.BtnSubmit)
            {
                specialMeeting.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.SpecialMeetingId))
            {
                specialMeeting.SpecialMeetingId = this.SpecialMeetingId;
                BLL.SpecialMeetingService.UpdateSpecialMeeting(specialMeeting);
                BLL.LogService.AddSys_Log(this.CurrUser, specialMeeting.SpecialMeetingCode, specialMeeting.SpecialMeetingId, BLL.Const.ProjectWeekMeetingMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.SpecialMeetingId = SQLHelper.GetNewID(typeof(Model.Meeting_SpecialMeeting));
                specialMeeting.SpecialMeetingId = this.SpecialMeetingId;
                BLL.SpecialMeetingService.AddSpecialMeeting(specialMeeting);
                BLL.LogService.AddSys_Log(this.CurrUser, specialMeeting.SpecialMeetingCode, specialMeeting.SpecialMeetingId, BLL.Const.ProjectWeekMeetingMenuId, BLL.Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectSpecialMeetingMenuId, this.SpecialMeetingId, (type == BLL.Const.BtnSubmit ? true : false), specialMeeting.SpecialMeetingName, "../Meeting/SpecialMeetingView.aspx?SpecialMeetingId={0}");
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
            if (string.IsNullOrEmpty(this.SpecialMeetingId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SpecialMeetingAttachUrl&menuId={1}", this.SpecialMeetingId, BLL.Const.ProjectSpecialMeetingMenuId)));
        }
        #endregion


        protected void btnAttachUrl1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.SpecialMeetingId))
            {
                SaveData(Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ClassMeetingAttachUrl&menuId={1}&strParam=1", this.SpecialMeetingId, Const.ProjectSpecialMeetingMenuId)));
        }

        protected void btnAttachUrl2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.SpecialMeetingId))
            {
                SaveData(Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ClassMeetingAttachUrl&menuId={1}&strParam=2", this.SpecialMeetingId, Const.ProjectSpecialMeetingMenuId)));
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