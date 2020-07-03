using BLL;
using System;
using System.Linq;
using System.Web;

namespace FineUIPro.Web.HSSE.ActionPlan
{
    public partial class ManagerRuleEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string ManageRuleId
        {
            get
            {
                return (string)ViewState["ManageRuleId"];
            }
            set
            {
                ViewState["ManageRuleId"] = value;
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
                //加载管理规定类别下拉选项
                this.ddlManageRuleTypeId.DataTextField = "ManageRuleTypeName";
                this.ddlManageRuleTypeId.DataValueField = "ManageRuleTypeId";
                this.ddlManageRuleTypeId.DataSource = BLL.ManageRuleTypeService.GetManageRuleTypeList();
                ddlManageRuleTypeId.DataBind();
                Funs.FineUIPleaseSelect(this.ddlManageRuleTypeId);
                if (Request.Params["type"] == "see")   //查看
                {
                    this.btnSave.Hidden = true;
                }
                this.ManageRuleId = Request.Params["ManagerRuleId"];
                if (!string.IsNullOrEmpty(this.ManageRuleId))
                {
                    var managerRule = BLL.ActionPlan_ManagerRuleService.GetManagerRuleById(this.ManageRuleId);
                    if (managerRule != null)
                    {
                        this.ProjectId = managerRule.ProjectId;
                        this.txtManageRuleCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.ManageRuleId);
                        this.txtManageRuleName.Text = managerRule.ManageRuleName;
                        if (!string.IsNullOrEmpty(managerRule.ManageRuleTypeId))
                        {
                            this.ddlManageRuleTypeId.SelectedValue = managerRule.ManageRuleTypeId;
                        }
                        //this.txtVersionNo.Text = managerRule.VersionNo;
                        this.txtRemark.Text = managerRule.Remark;
                        this.txtSeeFile.Text = HttpUtility.HtmlDecode(managerRule.SeeFile);
                    }
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ActionPlan_ManagerRuleMenuId;
                this.ctlAuditFlow.DataId = this.ManageRuleId;
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
            var manageRule = BLL.ActionPlan_ManagerRuleService.GetManagerRuleById(this.ManageRuleId);
            manageRule.ManageRuleCode = this.txtManageRuleCode.Text.Trim();
            manageRule.ManageRuleName = this.txtManageRuleName.Text.Trim();
            manageRule.SeeFile = HttpUtility.HtmlEncode(this.txtSeeFile.Text);
            if (this.ddlManageRuleTypeId.SelectedValue != BLL.Const._Null)
            {
                manageRule.ManageRuleTypeId = this.ddlManageRuleTypeId.SelectedValue;
            }
            //manageRule.VersionNo = this.txtVersionNo.Text.Trim();
            manageRule.AttachUrl = this.FullAttachUrl;
            manageRule.Remark = this.txtRemark.Text.Trim();
            manageRule.State = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                manageRule.State = this.ctlAuditFlow.NextStep;
            }
            BLL.ActionPlan_ManagerRuleService.UpdateManageRule(manageRule);

            BLL.LogService.AddSys_Log(this.CurrUser, manageRule.ManageRuleCode, manageRule.ManagerRuleId, BLL.Const.ActionPlan_ManagerRuleMenuId, Const.BtnModify);
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ActionPlan_ManagerRuleMenuId, this.ManageRuleId, (type == BLL.Const.BtnSubmit ? true : false), manageRule.ManageRuleName, "../ActionPlan/ManagerRuleView.aspx?ManagerRuleId={0}");
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
            if (string.IsNullOrEmpty(this.ManageRuleId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ActionPlanManagerRule&menuId={1}", ManageRuleId, BLL.Const.ActionPlan_ManagerRuleMenuId)));
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
            var standard = Funs.DB.Law_ManageRule.FirstOrDefault(x => x.IsPass == true && x.ManageRuleName == this.txtManageRuleName.Text.Trim() && (x.ManageRuleId != this.ManageRuleId || (this.ManageRuleId == null && x.ManageRuleId != null)));
            if (standard != null)
            {
                ShowNotify("输入的文件名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}