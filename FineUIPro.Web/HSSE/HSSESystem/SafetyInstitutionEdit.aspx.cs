using BLL;
using System;
using System.Linq;
using System.Web;

namespace FineUIPro.Web.HSSE.HSSESystem
{
    public partial class SafetyInstitutionEdit : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string SafetyInstitutionId
        {
            get
            {
                return (string)ViewState["SafetyInstitutionId"];
            }
            set
            {
                ViewState["SafetyInstitutionId"] = value;
            }
        }

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
                //加载类型下拉选项
                BLL.ManageRuleTypeService.InitManageRuleTypeDropDownList(this.ddlManageRuleTypeId, true);
                ConstValue.InitConstValueDropDownList(this.drpIndexesIds, ConstValue.Group_HSSE_Indexes, false);
                ConstValue.InitConstValueDropDownList(this.drpReleaseStates, ConstValue.Group_HSSE_ReleaseStates, false);
                this.SafetyInstitutionId = Request.Params["SafetyInstitutionId"];
                if (!string.IsNullOrEmpty(this.SafetyInstitutionId))
                {
                    Model.HSSESystem_SafetyInstitution safetyInstitution = BLL.ServerSafetyInstitutionService.GetSafetyInstitutionById(this.SafetyInstitutionId);
                    if (safetyInstitution!=null)
                    {
                        this.txtCode.Text = safetyInstitution.Code;
                        this.txtName.Text = safetyInstitution.SafetyInstitutionName;
                        this.ddlManageRuleTypeId.SelectedValue = safetyInstitution.TypeId;
                        this.drpReleaseStates.SelectedValue = safetyInstitution.ReleaseStates;
                        this.txtReleaseUnit.Text = safetyInstitution.ReleaseUnit;
                        this.dpkApprovalDate.Text = string.Format("{0:yyyy-MM-dd}", safetyInstitution.ApprovalDate);
                        this.dpkEffectiveDate.Text = string.Format("{0:yyyy-MM-dd}", safetyInstitution.EffectiveDate);
                        this.txtAbolitionDate.Text = string.Format("{0:yyyy-MM-dd}", safetyInstitution.AbolitionDate);
                        this.txtReplaceInfo.Text = safetyInstitution.ReplaceInfo;
                        this.txtDescription.Text = safetyInstitution.Description;
                        if (!string.IsNullOrEmpty(safetyInstitution.IndexesIds))
                        {
                            this.drpIndexesIds.SelectedValueArray = safetyInstitution.IndexesIds.Split(',');
                        }
                        this.txtCompileMan.Text = safetyInstitution.CompileMan;
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", safetyInstitution.CompileDate);
                    }
                }
                else
                {
                    txtCompileMan.Text = this.CurrUser.UserName;
                    txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.HSSESystem_SafetyInstitution newSafetyInstitution = new Model.HSSESystem_SafetyInstitution
            {
                Code = this.txtCode.Text.Trim(),
                SafetyInstitutionName = this.txtName.Text.Trim(),
                ApprovalDate = Funs.GetNewDateTime(this.dpkApprovalDate.Text.Trim()),
                EffectiveDate = Funs.GetNewDateTime(this.dpkEffectiveDate.Text.Trim()),
                ReleaseUnit = this.txtReleaseUnit.Text.Trim(),
                AbolitionDate = Funs.GetNewDateTime(this.txtAbolitionDate.Text.Trim()),
                ReplaceInfo = this.txtReplaceInfo.Text.Trim(),
                Description = this.txtDescription.Text.Trim(),
                CompileMan = this.CurrUser.UserName,
                CompileDate = System.DateTime.Now,
            };

            if (this.ddlManageRuleTypeId.SelectedValue != BLL.Const._Null)
            {
                newSafetyInstitution.TypeId = this.ddlManageRuleTypeId.SelectedValue;
            }
            if (!string.IsNullOrEmpty(this.drpReleaseStates.SelectedValue))
            {
                newSafetyInstitution.ReleaseStates = this.drpReleaseStates.SelectedValue;
            }
            newSafetyInstitution.IndexesIds = Funs.GetStringByArray(this.drpIndexesIds.SelectedValueArray);
            newSafetyInstitution.UnitId = string.IsNullOrEmpty(this.CurrUser.UnitId) ? Const.UnitId_SEDIN : this.CurrUser.UnitId;
            if (!string.IsNullOrEmpty(this.SafetyInstitutionId))
            {
              
                newSafetyInstitution.SafetyInstitutionId = this.SafetyInstitutionId;
                BLL.ServerSafetyInstitutionService.UpdateSafetyInstitution(newSafetyInstitution);
                BLL.LogService.AddSys_Log(this.CurrUser, newSafetyInstitution.SafetyInstitutionName, newSafetyInstitution.SafetyInstitutionId,BLL.Const.ServerSafetyInstitutionMenuId,BLL.Const.BtnModify);
            }
            else
            {               
                newSafetyInstitution.UnitId = this.CurrUser.UnitId;
                this.SafetyInstitutionId = SQLHelper.GetNewID();
                newSafetyInstitution.SafetyInstitutionId = this.SafetyInstitutionId;
                BLL.ServerSafetyInstitutionService.AddSafetyInstitution(newSafetyInstitution);
                BLL.LogService.AddSys_Log(this.CurrUser, newSafetyInstitution.SafetyInstitutionName, newSafetyInstitution.SafetyInstitutionId, BLL.Const.ServerSafetyInstitutionMenuId, BLL.Const.BtnAdd);
            }
        }

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.SafetyInstitutionId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SafetyInstitutionAttachUrl&menuId={1}", SafetyInstitutionId, BLL.Const.ServerSafetyInstitutionMenuId)));
        }
        #endregion

        #region 验证名称是否存在
        /// <summary>
        /// 验证管理规定名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var standard = Funs.DB.HSSESystem_SafetyInstitution.FirstOrDefault(x => x.SafetyInstitutionName == this.txtName.Text.Trim() && (x.SafetyInstitutionId != this.SafetyInstitutionId || (this.SafetyInstitutionId == null && x.SafetyInstitutionId != null)));
            if (standard != null)
            {
                ShowNotify("输入的文件名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}