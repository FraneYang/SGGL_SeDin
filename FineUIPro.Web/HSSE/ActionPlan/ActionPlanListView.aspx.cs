using System;
using System.Web;

namespace FineUIPro.Web.HSSE.ActionPlan
{
    public partial class ActionPlanListView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string ActionPlanListId
        {
            get
            {
                return (string)ViewState["ActionPlanListId"];
            }
            set
            {
                ViewState["ActionPlanListId"] = value;
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
              
                this.ActionPlanListId = Request.Params["ActionPlanListId"];
                if (!string.IsNullOrEmpty(this.ActionPlanListId))
                {
                    Model.ActionPlan_ActionPlanList actionPlanList = BLL.ActionPlanListService.GetActionPlanListById(this.ActionPlanListId);
                    if (actionPlanList!=null)
                    {
                        ///读取编号
                        this.txtActionPlanListCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.ActionPlanListId);                        
                        this.txtActionPlanListName.Text = actionPlanList.ActionPlanListName;
                        this.txtVersionNo.Text = actionPlanList.VersionNo;
                        this.drpProjectType.SelectedValue = actionPlanList.VersionNo;
                        this.drpCompileMan.Text = BLL.UserService.GetUserNameByUserId(actionPlanList.CompileMan);
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", actionPlanList.CompileDate);
                        this.txtActionPlanListContents.Text = HttpUtility.HtmlDecode(actionPlanList.ActionPlanListContents);
                    }
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectActionPlanListMenuId;
                this.ctlAuditFlow.DataId = this.ActionPlanListId;
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
            if (!string.IsNullOrEmpty(this.ActionPlanListId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ActionPlanListAttachUrl&menuId={1}&type=-1", this.ActionPlanListId, BLL.Const.ProjectActionPlanListMenuId)));
            }
        }
        #endregion
    }
}