using System;
using System.Web;

namespace FineUIPro.Web.HSSE.Meeting
{
    public partial class MonthMeetingView : PageBase
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
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.MonthMeetingId = Request.Params["MonthMeetingId"];
                if (!string.IsNullOrEmpty(this.MonthMeetingId))
                {
                    Model.Meeting_MonthMeeting monthMeeting = BLL.MonthMeetingService.GetMonthMeetingById(this.MonthMeetingId);
                    if (monthMeeting != null)
                    {
                        this.txtMonthMeetingCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.MonthMeetingId);
                        this.txtMonthMeetingName.Text = monthMeeting.MonthMeetingName;
                        if (monthMeeting.MonthMeetingDate != null)
                        {
                            this.txtMonthMeetingDate.Text = string.Format("{0:yyyy-MM-dd}", monthMeeting.MonthMeetingDate);
                        }
                        if (!string.IsNullOrEmpty(monthMeeting.CompileMan))
                        {
                            var user = BLL.UserService.GetUserByUserId(monthMeeting.CompileMan);
                            if (user!=null)
                            {
                                this.txtCompileMan.Text = user.UserName;
                            }
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
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectMonthMeetingMenuId;
                this.ctlAuditFlow.DataId = this.MonthMeetingId;
            }
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
            if (!string.IsNullOrEmpty(this.MonthMeetingId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/MonthMeetingAttachUrl&menuId={1}&type=-1", this.MonthMeetingId, BLL.Const.ProjectMonthMeetingMenuId)));
            }
        }
        #endregion

        protected void btnAttachUrl1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.MonthMeetingId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ClassMeetingAttachUrl&menuId={1}&strParam=1&type=-1", this.MonthMeetingId, BLL.Const.ProjectMonthMeetingMenuId)));
            }
        }

        protected void btnAttachUrl2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.MonthMeetingId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ClassMeetingAttachUrl&menuId={1}&strParam=2&type=-1", this.MonthMeetingId, BLL.Const.ProjectMonthMeetingMenuId)));
            }
        }
    }
}