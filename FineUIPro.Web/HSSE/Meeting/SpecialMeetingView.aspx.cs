using System;
using System.Web;

namespace FineUIPro.Web.HSSE.Meeting
{
    public partial class SpecialMeetingView : PageBase
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
                this.SpecialMeetingId = Request.Params["SpecialMeetingId"];
                if (!string.IsNullOrEmpty(this.SpecialMeetingId))
                {
                    Model.Meeting_SpecialMeeting specialMeeting = BLL.SpecialMeetingService.GetSpecialMeetingById(this.SpecialMeetingId);
                    if (specialMeeting != null)
                    {
                        this.txtSpecialMeetingCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.SpecialMeetingId);
                        this.txtSpecialMeetingName.Text = specialMeeting.SpecialMeetingName;
                        if (specialMeeting.SpecialMeetingDate != null)
                        {
                            this.txtSpecialMeetingDate.Text = string.Format("{0:yyyy-MM-dd}", specialMeeting.SpecialMeetingDate);
                        }
                        if (!string.IsNullOrEmpty(specialMeeting.CompileMan))
                        {
                            var user = BLL.UserService.GetUserByUserId(specialMeeting.CompileMan);
                            if (user != null)
                            {
                                this.txtCompileMan.Text = user.UserName;
                            }
                        }
                        if (specialMeeting.AttentPersonNum != null)
                        {
                            this.txtAttentPersonNum.Text = specialMeeting.AttentPersonNum.ToString();
                        }
                        this.txtSpecialMeetingContents.Text = HttpUtility.HtmlDecode(specialMeeting.SpecialMeetingContents);
                        this.txtMeetingHours.Text = Convert.ToString(specialMeeting.MeetingHours);
                        this.txtMeetingHostMan.Text = specialMeeting.MeetingHostMan;
                        this.txtAttentPerson.Text = specialMeeting.AttentPerson;
                    }
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectSpecialMeetingMenuId;
                this.ctlAuditFlow.DataId = this.SpecialMeetingId;
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
            if (!string.IsNullOrEmpty(this.SpecialMeetingId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SpecialMeetingAttachUrl&menuId={1}&type=-1", this.SpecialMeetingId, BLL.Const.ProjectSpecialMeetingMenuId)));
            }
        }
        #endregion

        protected void btnAttachUrl1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.SpecialMeetingId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ClassMeetingAttachUrl&menuId={1}&strParam=1&type=-1", this.SpecialMeetingId, BLL.Const.ProjectSpecialMeetingMenuId)));
            }
        }

        protected void btnAttachUrl2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.SpecialMeetingId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ClassMeetingAttachUrl&menuId={1}&strParam=2&type=-1", this.SpecialMeetingId, BLL.Const.ProjectSpecialMeetingMenuId)));
            }
        }
    }
}