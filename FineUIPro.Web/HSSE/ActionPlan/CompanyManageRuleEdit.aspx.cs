using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace FineUIPro.Web.HSSE.ActionPlan
{
    public partial class CompanyManageRuleEdit :PageBase
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
                        if (!string.IsNullOrEmpty(managerRule.ManageRuleTypeId))
                        {
                            this.ddlManageRuleTypeId.SelectedValue = managerRule.ManageRuleTypeId;
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
            var manageRule = BLL.ActionPlan_CompanyManagerRuleService.GetManagerRuleById(this.CompanyManageRuleId);
            manageRule.ManageRuleCode = this.txtManageRuleCode.Text.Trim();
            manageRule.ManageRuleName = this.txtManageRuleName.Text.Trim();
            if (this.ddlManageRuleTypeId.SelectedValue != BLL.Const._Null)
            {
                manageRule.ManageRuleTypeId = this.ddlManageRuleTypeId.SelectedValue;
            }
            manageRule.SeeFile = HttpUtility.HtmlEncode(this.txtSeeFile.Text);
            manageRule.AttachUrl = this.FullAttachUrl;
            manageRule.State = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                manageRule.State = this.ctlAuditFlow.NextStep;
            }
            BLL.ActionPlan_CompanyManagerRuleService.UpdateManageRule(manageRule);

            BLL.LogService.AddSys_Log(this.CurrUser, manageRule.ManageRuleCode, manageRule.ManagerRuleId, BLL.Const.ActionPlan_CompanyManagerRuleMenuId, Const.BtnModify);
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ActionPlan_CompanyManagerRuleMenuId, this.CompanyManageRuleId, (type == BLL.Const.BtnSubmit ? true : false), manageRule.ManageRuleName, "../ActionPlan/ManagerRuleView.aspx?ManagerRuleId={0}");
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
            if (string.IsNullOrEmpty(this.CompanyManageRuleId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ActionPlanManagerRule&menuId={1}", CompanyManageRuleId, BLL.Const.ActionPlan_CompanyManagerRuleMenuId)));
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
            var standard = Funs.DB.Law_ManageRule.FirstOrDefault(x => x.IsPass == true && x.ManageRuleName == this.txtManageRuleName.Text.Trim() && (x.ManageRuleId != this.CompanyManageRuleId || (this.CompanyManageRuleId == null && x.ManageRuleId != null)));
            if (standard != null)
            {
                ShowNotify("输入的文件名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}