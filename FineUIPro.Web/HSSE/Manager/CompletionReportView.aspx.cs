using System;
using System.Web;

namespace FineUIPro.Web.HSSE.Manager
{
    public partial class CompletionReportView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string CompletionReportId
        {
            get
            {
                return (string)ViewState["CompletionReportId"];
            }
            set
            {
                ViewState["CompletionReportId"] = value;
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
                this.CompletionReportId = Request.Params["CompletionReportId"];
                if (!string.IsNullOrEmpty(this.CompletionReportId))
                {
                    Model.Manager_CompletionReport completionReport = BLL.CompletionReportService.GetCompletionReportById(this.CompletionReportId);
                    if (completionReport != null)
                    {
                        this.txtCompletionReportCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.CompletionReportId);
                        this.txtCompletionReportName.Text = completionReport.CompletionReportName;
                        var users = BLL.UserService.GetUserByUserId(completionReport.CompileMan);
                        if (users != null)
                        {
                            this.drpCompileMan.Text = users.UserName;
                        }
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", completionReport.CompileDate);
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(completionReport.FileContent);
                    }
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectCompletionReportMenuId;
                this.ctlAuditFlow.DataId = this.CompletionReportId;
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
            if (!string.IsNullOrEmpty(this.CompletionReportId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CompletionReportAttachUrl&menuId={1}&type=-1", CompletionReportId, BLL.Const.ProjectCompletionReportMenuId)));
            }

        }
        #endregion
    }
}