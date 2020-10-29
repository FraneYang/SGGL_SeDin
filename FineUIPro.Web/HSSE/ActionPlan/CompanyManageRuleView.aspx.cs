using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace FineUIPro.Web.HSSE.ActionPlan
{
    public partial class CompanyManageRuleView :PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string CompanyManageRuleId
        {
            get
            {
                return (string)ViewState["CompanyManageRuleId"];
            }
            set
            {
                ViewState["CompanyManageRuleId"] = value;
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
        /// <summary>
        /// 附件路径
        /// </summary>
        public string FullAttachUrl
        {
            get
            {
                return (string)ViewState["FullAttachUrl"];
            }
            set
            {
                ViewState["FullAttachUrl"] = value;
            }
        }
        #endregion

        #region 加载页面
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.CompanyManageRuleId = Request.Params["ManagerRuleId"];
                if (!string.IsNullOrEmpty(this.CompanyManageRuleId))
                {
                    var managerRule = BLL.ActionPlan_CompanyManagerRuleService.GetManagerRuleById(this.CompanyManageRuleId);
                    if (managerRule != null)
                    {
                        this.ProjectId = managerRule.ProjectId;
                        this.txtManageRuleCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.CompanyManageRuleId);
                        this.txtManageRuleName.Text = managerRule.ManageRuleName;
                        this.txtSeeFile.Text = HttpUtility.HtmlDecode(managerRule.SeeFile);
                        var manag = BLL.ManageRuleTypeService.GetManageRuleTypeById(managerRule.ManageRuleTypeId);
                        if (manag != null)
                        {
                            this.ddlManageRuleTypeId.Text = manag.ManageRuleTypeName;
                        }
                    }
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ActionPlan_CompanyManagerRuleMenuId;
                this.ctlAuditFlow.DataId = this.CompanyManageRuleId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
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
            if (!string.IsNullOrEmpty(this.CompanyManageRuleId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ActionPlanManagerRule&menuId={1}&type=-1", CompanyManageRuleId, BLL.Const.ActionPlan_CompanyManagerRuleMenuId)));
            }

        }
        #endregion
    }
}