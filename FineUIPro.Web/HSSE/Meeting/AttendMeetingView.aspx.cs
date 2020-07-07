using System;
using System.Web;

namespace FineUIPro.Web.HSSE.Meeting
{
    public partial class AttendMeetingView : PageBase
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
                this.AttendMeetingId = Request.Params["AttendMeetingId"];
                if (!string.IsNullOrEmpty(this.AttendMeetingId))
                {
                    Model.Meeting_AttendMeeting attendMeeting = BLL.AttendMeetingService.GetAttendMeetingById(this.AttendMeetingId);
                    if (attendMeeting != null)
                    {
                        this.txtAttendMeetingCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.AttendMeetingId);
                        this.txtAttendMeetingName.Text = attendMeeting.AttendMeetingName;
                        if (attendMeeting.AttendMeetingDate != null)
                        {
                            this.txtAttendMeetingDate.Text = string.Format("{0:yyyy-MM-dd}", attendMeeting.AttendMeetingDate);
                        }
                        if (!string.IsNullOrEmpty(attendMeeting.CompileMan))
                        {
                            var user = BLL.UserService.GetUserByUserId(attendMeeting.CompileMan);
                            if (user != null)
                            {
                                this.txtCompileMan.Text = user.UserName;
                            }
                        }
                        this.txtAttendMeetingContents.Text = HttpUtility.HtmlDecode(attendMeeting.AttendMeetingContents);
                    }
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectAttendMeetingMenuId;
                this.ctlAuditFlow.DataId = this.AttendMeetingId;
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
            if (!string.IsNullOrEmpty(this.AttendMeetingId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/AttendMeetingAttachUrl&menuId={1}&type=-1", this.AttendMeetingId, BLL.Const.ProjectAttendMeetingMenuId)));
            }
        }
        #endregion

        protected void btnAttachUrl1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.AttendMeetingId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ClassMeetingAttachUrl&menuId={1}&strParam=1&type=-1", this.AttendMeetingId, BLL.Const.ProjectAttendMeetingMenuId)));
            }
        }

        protected void btnAttachUrl2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.AttendMeetingId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ClassMeetingAttachUrl&menuId={1}&strParam=2&type=-1", this.AttendMeetingId, BLL.Const.ProjectAttendMeetingMenuId)));
            }
        }
    }
}