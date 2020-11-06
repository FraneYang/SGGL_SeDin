using System;
using System.Web;

namespace FineUIPro.Web.Notice
{
    public partial class NoticeView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string NoticeId
        {
            get
            {
                return (string)ViewState["NoticeId"];
            }
            set
            {
                ViewState["NoticeId"] = value;
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
                this.NoticeId = Request.Params["NoticeId"];
                if (!string.IsNullOrEmpty(this.NoticeId))
                {
                    Model.InformationProject_Notice notice = BLL.NoticeService.GetNoticeById(this.NoticeId);
                    if (notice != null)
                    {                        
                        ///读取编号
                        if (!string.IsNullOrEmpty(notice.NoticeCode))
                        {
                            this.txtNoticeCode.Text = notice.NoticeCode;
                        }
                        else
                        {
                            this.txtNoticeCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.NoticeId);
                        }
                        this.txtNoticeTitle.Text = notice.NoticeTitle;
                        this.txtMainContent.Text = HttpUtility.HtmlDecode(notice.MainContent);
                        var userCompileMan = BLL.UserService.GetUserByUserId(notice.CompileMan);                      
                        this.drpProjects.Text = notice.AccessProjectText;
                        BLL.APIUserService.getSaveUserRead(BLL.Const.ServerNoticeMenuId, notice.ProjectId, this.CurrUser.UserId, this.NoticeId);
                    }
                }
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
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/NoticeAttachUrl&type=-1", this.NoticeId)));
        }
        #endregion
    }
}