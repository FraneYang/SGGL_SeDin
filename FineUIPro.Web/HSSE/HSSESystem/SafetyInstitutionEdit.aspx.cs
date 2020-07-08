using BLL;
using System;
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
                this.SafetyInstitutionId = Request.Params["SafetyInstitutionId"];
                if (!string.IsNullOrEmpty(this.SafetyInstitutionId))
                {
                    Model.HSSESystem_SafetyInstitution safetyInstitution = BLL.ServerSafetyInstitutionService.GetSafetyInstitutionById(this.SafetyInstitutionId);
                    if (safetyInstitution!=null)
                    {
                        this.txtTitle.Text = safetyInstitution.SafetyInstitutionName;
                        if (safetyInstitution.EffectiveDate!=null)
                        {
                            this.txtEffectiveDate.Text = string.Format("{0:yyyy-MM-dd}", safetyInstitution.EffectiveDate);
                        }
                        this.txtScope.Text = safetyInstitution.Scope;
                        this.txtRemark.Text = safetyInstitution.Remark;
                        this.txtSeeFile.Text = HttpUtility.HtmlDecode(safetyInstitution.FileContents);      
                    }
                }
                else
                {
                    this.txtEffectiveDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    //var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ServerSafetyInstitutionMenuId, this.CurrUser.LoginProjectId);
                    //if (codeTemplateRule != null)
                    //{
                    //    this.txtSeeFile.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    //}
                    ////自动生成编码
                    this.txtTitle.Text = this.SimpleForm1.Title;
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
                SafetyInstitutionName = this.txtTitle.Text.Trim(),
                EffectiveDate = Funs.GetNewDateTime(this.txtEffectiveDate.Text.Trim()),
                Scope = this.txtScope.Text.Trim(),
                Remark = this.txtRemark.Text.Trim(),
                FileContents = HttpUtility.HtmlEncode(this.txtSeeFile.Text)
            };
            if (!string.IsNullOrEmpty(this.SafetyInstitutionId))
            {
                newSafetyInstitution.SafetyInstitutionId = this.SafetyInstitutionId;
                BLL.ServerSafetyInstitutionService.UpdateSafetyInstitution(newSafetyInstitution);
                BLL.LogService.AddSys_Log(this.CurrUser, newSafetyInstitution.SafetyInstitutionName, newSafetyInstitution.SafetyInstitutionId,BLL.Const.ServerSafetyInstitutionMenuId,BLL.Const.BtnModify);
            }
            else
            {
                this.SafetyInstitutionId = SQLHelper.GetNewID(typeof(Model.HSSESystem_SafetyInstitution));
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
    }
}