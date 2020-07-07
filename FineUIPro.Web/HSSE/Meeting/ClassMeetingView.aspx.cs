using BLL;
using System;
using System.Web;

namespace FineUIPro.Web.HSSE.Meeting
{
    public partial class ClassMeetingView : PageBase
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

                this.ClassMeetingId = Request.Params["ClassMeetingId"];
                if (!string.IsNullOrEmpty(this.ClassMeetingId))
                {
                    Model.Meeting_ClassMeeting classMeeting = BLL.ClassMeetingService.GetClassMeetingById(this.ClassMeetingId);
                    if (classMeeting != null)
                    {
                        this.txtClassMeetingCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.ClassMeetingId);
                        this.txtClassMeetingName.Text = classMeeting.ClassMeetingName;
                        if (classMeeting.ClassMeetingDate != null)
                        {
                            this.txtClassMeetingDate.Text = string.Format("{0:yyyy-MM-dd}", classMeeting.ClassMeetingDate);
                        }
                        if (!string.IsNullOrEmpty(classMeeting.CompileMan))
                        {
                            var user = BLL.UserService.GetUserByUserId(classMeeting.CompileMan);
                            if (user != null)
                            {
                                this.txtCompileManName.Text = user.UserName;
                            }
                        }
                        this.txtClassMeetingContents.Text = HttpUtility.HtmlDecode(classMeeting.ClassMeetingContents);
                        this.drpUnit.Text = UnitService.GetUnitNameByUnitId(classMeeting.UnitId);
                        this.drpTeamGroup.Text = TeamGroupService.GetTeamGroupNameByTeamGroupId(classMeeting.TeamGroupId);
                        this.txtAttentPersonNum.Text = classMeeting.AttentPersonNum.ToString();
                    }
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectClassMeetingMenuId;
                this.ctlAuditFlow.DataId = this.ClassMeetingId;
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
            if (!string.IsNullOrEmpty(this.ClassMeetingId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ClassMeetingAttachUrl&menuId={1}&type=-1", this.ClassMeetingId, Const.ProjectClassMeetingMenuId)));
            }
        }
        #endregion

        protected void btnAttachUrl1_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ClassMeetingAttachUrl&menuId={1}&strParam=1&type=-1", this.ClassMeetingId, Const.ProjectClassMeetingMenuId)));
        }

        protected void btnAttachUrl2_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ClassMeetingAttachUrl&menuId={1}&strParam=2&type=-1", this.ClassMeetingId, Const.ProjectClassMeetingMenuId)));
        }
    }
}