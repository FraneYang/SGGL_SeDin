using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace FineUIPro.Web.HSSE.ActionPlan
{
    public partial class ProjectManageRuleEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string ProjectManageRuleId
        {
            get
            {
                return (string)ViewState["ProjectManageRuleId"];
            }
            set
            {
                ViewState["ProjectManageRuleId"] = value;
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
                if (Request.Params["type"] == "see")   //查看
                {
                    this.btnSave.Hidden = true;
                }
                //加载管理规定类别下拉选项
                this.ddlManageRuleTypeId.DataTextField = "ManageRuleTypeName";
                this.ddlManageRuleTypeId.DataValueField = "ManageRuleTypeId";
                this.ddlManageRuleTypeId.DataSource = BLL.ManageRuleTypeService.GetManageRuleTypeList();
                ddlManageRuleTypeId.DataBind();
                Funs.FineUIPleaseSelect(this.ddlManageRuleTypeId);
                this.ProjectManageRuleId = Request.Params["ManagerRuleId"];
                if (!string.IsNullOrEmpty(this.ProjectManageRuleId))
                {
                    var managerRule = BLL.ActionPlan_ProjectManagerRuleService.GetManagerRuleById(this.ProjectManageRuleId);
                    if (managerRule != null)
                    {
                        this.ProjectId = managerRule.ProjectId;
                        this.txtManageRuleCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.ProjectManageRuleId);
                        this.txtManageRuleName.Text = managerRule.ManageRuleName;
                        this.txtSeeFile.Text = HttpUtility.HtmlDecode(managerRule.SeeFile);
                        if (!string.IsNullOrEmpty(managerRule.ManageRuleTypeId))
                        {
                            this.ddlManageRuleTypeId.SelectedValue = managerRule.ManageRuleTypeId;
                        }
                    }
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ActionPlan_ProjectManagerRuleMenuId;
                this.ctlAuditFlow.DataId = this.ProjectManageRuleId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData(string type)
        {
            Model.ActionPlan_ProjectManagerRule newManagerRule = new Model.ActionPlan_ProjectManagerRule();
            newManagerRule.ManageRuleCode = this.txtManageRuleCode.Text.Trim();
            newManagerRule.ManageRuleName = this.txtManageRuleName.Text.Trim();
            newManagerRule.SeeFile = HttpUtility.HtmlEncode(this.txtSeeFile.Text);
            if (this.ddlManageRuleTypeId.SelectedValue != BLL.Const._Null)
            {
                newManagerRule.ManageRuleTypeId = this.ddlManageRuleTypeId.SelectedValue;
            }
            newManagerRule.AttachUrl = this.FullAttachUrl;
            newManagerRule.State = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                newManagerRule.State = this.ctlAuditFlow.NextStep;
            }
            var manageRule = BLL.ActionPlan_ProjectManagerRuleService.GetManagerRuleById(this.ProjectManageRuleId);
            if (manageRule == null)
            {
                newManagerRule.ManagerRuleId = SQLHelper.GetNewID(typeof(Model.ActionPlan_ProjectManagerRule));
                newManagerRule.ProjectId = this.CurrUser.LoginProjectId;
                newManagerRule.CompileDate = DateTime.Now;
                newManagerRule.CompileMan = this.CurrUser.UserId;
                newManagerRule.IsIssue = false;
                newManagerRule.Flag = true;
                BLL.ActionPlan_ProjectManagerRuleService.AddManageRule(newManagerRule);
                ////保存流程审核数据         
                this.ctlAuditFlow.btnSaveData(this.CurrUser.LoginProjectId, BLL.Const.ActionPlan_ManagerRuleMenuId, newManagerRule.ManagerRuleId, true, newManagerRule.ManageRuleName, "../ActionPlan/ManagerRuleView.aspx?ManagerRuleId={0}");
            }
            else
            {
                newManagerRule.ManagerRuleId = manageRule.ManagerRuleId;
                newManagerRule.ProjectId = manageRule.ProjectId;
                newManagerRule.CompileDate = manageRule.CompileDate;
                newManagerRule.CompileMan = manageRule.CompileMan;
                newManagerRule.IsIssue = manageRule.IsIssue;
                newManagerRule.Flag = manageRule.Flag;
                BLL.ActionPlan_ProjectManagerRuleService.UpdateManageRule(newManagerRule);
            }

            BLL.LogService.AddSys_Log(this.CurrUser, newManagerRule.ManageRuleCode, newManagerRule.ManagerRuleId, BLL.Const.ActionPlan_ProjectManagerRuleMenuId, Const.BtnModify);
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ActionPlan_ProjectManagerRuleMenuId, this.ProjectManageRuleId, (type == BLL.Const.BtnSubmit ? true : false), newManagerRule.ManageRuleName, "../ActionPlan/ProjectManagerRuleView.aspx?ManagerRuleId={0}");
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
            if (string.IsNullOrEmpty(this.ProjectManageRuleId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ActionPlanManagerRule&menuId={1}", ProjectManageRuleId, BLL.Const.ActionPlan_ProjectManagerRuleMenuId)));
        }
        #endregion

        #region 验证管理规定名称是否存在
        /// <summary>
        /// 验证管理规定名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var standard = Funs.DB.ActionPlan_ProjectManagerRule.FirstOrDefault(x => x.ProjectId == this.CurrUser.LoginProjectId && x.ManageRuleName == this.txtManageRuleName.Text.Trim() );
            if (standard != null)
            {
                ShowNotify("输入的文件名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}