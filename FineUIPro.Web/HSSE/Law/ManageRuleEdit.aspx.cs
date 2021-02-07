using System;
using System.Linq;
using System.Web;
using BLL;

namespace FineUIPro.Web.Law
{
    public partial class ManageRuleEdit : PageBase
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
                this.GetButtonPower();//设置权限
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                //加载类型下拉选项
                BLL.ManageRuleTypeService.InitManageRuleTypeDropDownList(this.ddlManageRuleTypeId, true);
                ConstValue.InitConstValueDropDownList(this.drpIndexesIds, ConstValue.Group_HSSE_Indexes, false);
                ConstValue.InitConstValueDropDownList(this.drpReleaseStates, ConstValue.Group_HSSE_ReleaseStates, false);                
                this.ManageRuleId = Request.Params["ManageRuleId"];
                if (!string.IsNullOrEmpty(this.ManageRuleId))
                {
                    var manageRule = BLL.ManageRuleService.GetManageRuleById(this.ManageRuleId);
                    if (manageRule != null)
                    {
                        this.txtManageRuleCode.Text = manageRule.ManageRuleCode;
                        this.txtManageRuleName.Text = manageRule.ManageRuleName;
                        this.ddlManageRuleTypeId.SelectedValue = manageRule.ManageRuleTypeId;
                        this.drpReleaseStates.SelectedValue =manageRule.ReleaseStates;
                        this.txtReleaseUnit.Text =manageRule.ReleaseUnit;
                        this.dpkApprovalDate.Text = string.Format("{0:yyyy-MM-dd}",manageRule.ApprovalDate);
                        this.dpkEffectiveDate.Text = string.Format("{0:yyyy-MM-dd}",manageRule.EffectiveDate);
                        this.txtAbolitionDate.Text = string.Format("{0:yyyy-MM-dd}",manageRule.AbolitionDate);
                        this.txtReplaceInfo.Text =manageRule.ReplaceInfo;
                        this.txtDescription.Text =manageRule.Description;
                        if (!string.IsNullOrEmpty(manageRule.IndexesIds))
                        {
                            this.drpIndexesIds.SelectedValueArray =manageRule.IndexesIds.Split(',');
                        }
                        this.txtCompileMan.Text = manageRule.CompileMan;
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", manageRule.CompileDate);
                    }
                }
                else
                {
                    txtCompileMan.Text = this.CurrUser.UserName;
                    txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }
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
            SaveData(true);
        }
        
        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData(bool isColose)
        {
            Model.Law_ManageRule manageRule = new Model.Law_ManageRule
            {
                ManageRuleCode = this.txtManageRuleCode.Text.Trim(),
                ManageRuleName = this.txtManageRuleName.Text.Trim(),
                ApprovalDate = Funs.GetNewDateTime(this.dpkApprovalDate.Text.Trim()),
                EffectiveDate = Funs.GetNewDateTime(this.dpkEffectiveDate.Text.Trim()),
                ReleaseUnit = this.txtReleaseUnit.Text.Trim(),
                AbolitionDate = Funs.GetNewDateTime(this.txtAbolitionDate.Text.Trim()),
                ReplaceInfo = this.txtReplaceInfo.Text.Trim(),
                Description = this.txtDescription.Text.Trim(),
            };
            if (this.ddlManageRuleTypeId.SelectedValue != BLL.Const._Null)
            {
                manageRule.ManageRuleTypeId = this.ddlManageRuleTypeId.SelectedValue;
            }
            if (!string.IsNullOrEmpty(this.drpReleaseStates.SelectedValue))
            {
                manageRule.ReleaseStates = this.drpReleaseStates.SelectedValue;
            }
            manageRule.IndexesIds = Funs.GetStringByArray(this.drpIndexesIds.SelectedValueArray);
            manageRule.UnitId = string.IsNullOrEmpty(this.CurrUser.UnitId) ? Const.UnitId_SEDIN : this.CurrUser.UnitId;
            if (string.IsNullOrEmpty(this.ManageRuleId))
            {
                manageRule.IsPass = true;
                manageRule.CompileMan = this.CurrUser.UserName;
                manageRule.CompileDate = System.DateTime.Now;
                manageRule.UnitId = this.CurrUser.UnitId;
                this.ManageRuleId = SQLHelper.GetNewID(typeof(Model.Law_ManageRule));
                manageRule.ManageRuleId = this.ManageRuleId;
                BLL.ManageRuleService.AddManageRule(manageRule);
                BLL.LogService.AddSys_Log(this.CurrUser, manageRule.ManageRuleCode, manageRule.ManageRuleId, BLL.Const.ManageRuleMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                manageRule.ManageRuleId = this.ManageRuleId;
                BLL.ManageRuleService.UpdateManageRule(manageRule);
                BLL.LogService.AddSys_Log(this.CurrUser, manageRule.ManageRuleCode, manageRule.ManageRuleId, BLL.Const.ManageRuleMenuId, BLL.Const.BtnModify);
            }
            if (isColose)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
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
            if (string.IsNullOrEmpty(this.ManageRuleId))
            {
                SaveData( false);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ManageRule&menuId={1}", ManageRuleId, BLL.Const.ManageRuleMenuId)));
        }
        #endregion

        #region 设置权限
        /// <summary>
        /// 设置权限
        /// </summary>
        private void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ManageRuleMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
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

        #region 附件上传
        /// <summary>
        /// 上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploadResources_Click(object sender, EventArgs e)
        {
            if (this.btnSave.Hidden)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ManageRule&type=-1", ManageRuleId, BLL.Const.ManageRuleMenuId)));
            }
            else
            {
                if (string.IsNullOrEmpty(this.ManageRuleId))
                {
                    SaveData(false);
                }
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ManageRule&menuId={1}", ManageRuleId, BLL.Const.ManageRuleMenuId)));
            }
        }
        #endregion
    }
}