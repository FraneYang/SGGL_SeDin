using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.Notice
{
    public partial class NoticeView2 : PageBase
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.NoticeId = Request.Params["NoticeId"];
                if (!string.IsNullOrEmpty(this.NoticeId))
                {
                    Model.InformationProject_Notice notice = BLL.NoticeService.GetNoticeById(this.NoticeId);
                    if (notice != null)
                    {
                        this.lbTitle.Text = notice.NoticeTitle;
                        this.lbMainContent.Text = HttpUtility.HtmlDecode(notice.MainContent);
                        BLL.APIUserService.getSaveUserRead(BLL.Const.ServerNoticeMenuId, notice.ProjectId, this.CurrUser.UserId, this.NoticeId);
                    }
                }
            }
        }
    }
}