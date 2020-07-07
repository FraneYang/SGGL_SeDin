using System;
using System.Web;

namespace FineUIPro.Web.HSSE.Meeting
{
    public partial class WeekMeetingView : PageBase
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
                this.WeekMeetingId = Request.Params["WeekMeetingId"];
                if (!string.IsNullOrEmpty(this.WeekMeetingId))
                {
                    Model.Meeting_WeekMeeting weekMeeting = BLL.WeekMeetingService.GetWeekMeetingById(this.WeekMeetingId);
                    if (weekMeeting != null)
                    {
                        ///读取编号
                        this.txtWeekMeetingCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.WeekMeetingId);
                        this.txtWeekMeetingName.Text = weekMeeting.WeekMeetingName;
                        if (weekMeeting.WeekMeetingDate != null)
                        {
                            this.txtWeekMeetingDate.Text = string.Format("{0:yyyy-MM-dd}", weekMeeting.WeekMeetingDate);
                        }
                        var users = BLL.UserService.GetUserByUserId(weekMeeting.CompileMan);
                        if (users != null)
                        {
                            this.drpCompileMan.Text = users.UserName;
                        }
                        if (weekMeeting.AttentPersonNum != null)
                        {
                            this.txtAttentPersonNum.Text = weekMeeting.AttentPersonNum.ToString();
                        }
                        this.txtWeekMeetingContents.Text = HttpUtility.HtmlDecode(weekMeeting.WeekMeetingContents);
                        this.txtMeetingHours.Text = Convert.ToString(weekMeeting.MeetingHours);
                        this.txtMeetingHostMan.Text = weekMeeting.MeetingHostMan;
                        this.txtAttentPerson.Text = weekMeeting.AttentPerson;
                    }
                }
                
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectWeekMeetingMenuId;
                this.ctlAuditFlow.DataId = this.WeekMeetingId;
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
            if (!string.IsNullOrEmpty(this.WeekMeetingId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/WeekMeetingAttachUrl&menuId={1}&type=-1", WeekMeetingId, BLL.Const.ProjectWeekMeetingMenuId)));
            }
        }
        #endregion
        protected void btnAttachUrl1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.WeekMeetingId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ClassMeetingAttachUrl&menuId={1}&strParam=1&type=-1", this.WeekMeetingId, BLL.Const.ProjectWeekMeetingMenuId)));
            }
        }

        protected void btnAttachUrl2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.WeekMeetingId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ClassMeetingAttachUrl&menuId={1}&strParam=2&type=-1", this.WeekMeetingId, BLL.Const.ProjectWeekMeetingMenuId)));
            }
        }
    }
}