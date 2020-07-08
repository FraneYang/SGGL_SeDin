using BLL;
using System;
using System.Web;

namespace FineUIPro.Web.HSSE.Manager
{
    public partial class CompletionReportEdit : PageBase
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.InitDropDownList();
                this.CompletionReportId = Request.Params["CompletionReportId"];
                if (!string.IsNullOrEmpty(this.CompletionReportId))
                {
                    Model.Manager_CompletionReport completionReport = BLL.CompletionReportService.GetCompletionReportById(this.CompletionReportId);
                    if (completionReport != null)
                    {
                        this.ProjectId = completionReport.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtCompletionReportCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.CompletionReportId);
                        this.txtCompletionReportName.Text = completionReport.CompletionReportName;
                        if (!string.IsNullOrEmpty(completionReport.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = completionReport.CompileMan;
                        }
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", completionReport.CompileDate);
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(completionReport.FileContent);
                    }
                }
                else
                {
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    //var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectCompletionReportMenuId, this.ProjectId);
                    //if (codeTemplateRule != null)
                    //{
                    //    this.txtFileContent.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    //}
                    ////自动生成编码
                    this.txtCompletionReportCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectCompletionReportMenuId, this.ProjectId, this.CurrUser.UnitId);
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectCompletionReportMenuId;
                this.ctlAuditFlow.DataId = this.CompletionReportId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }
        #endregion

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            UserService.InitUserDropDownList(this.drpCompileMan, this.ProjectId, true);
        }

        #region 保存
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
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.Manager_CompletionReport completionReport = new Model.Manager_CompletionReport
            {
                ProjectId = this.ProjectId,
                CompletionReportCode = this.txtCompletionReportCode.Text.Trim(),
                CompletionReportName = this.txtCompletionReportName.Text.Trim()
            };
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                completionReport.CompileMan = this.drpCompileMan.SelectedValue;
            }
            completionReport.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
            completionReport.FileContent = HttpUtility.HtmlEncode(this.txtFileContent.Text);
            completionReport.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                completionReport.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.CompletionReportId))
            {
                completionReport.CompletionReportId = this.CompletionReportId;
                BLL.CompletionReportService.UpdateCompletionReport(completionReport);
                BLL.LogService.AddSys_Log(this.CurrUser, completionReport.CompletionReportCode, completionReport.CompletionReportId, BLL.Const.ProjectCompletionReportMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.CompletionReportId = SQLHelper.GetNewID(typeof(Model.Manager_CompletionReport));
                completionReport.CompletionReportId = this.CompletionReportId;
                BLL.CompletionReportService.AddCompletionReport(completionReport);
                BLL.LogService.AddSys_Log(this.CurrUser, completionReport.CompletionReportCode, completionReport.CompletionReportId, BLL.Const.ProjectCompletionReportMenuId, BLL.Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectCompletionReportMenuId, this.CompletionReportId, (type == BLL.Const.BtnSubmit ? true : false), completionReport.CompletionReportName, "../Manager/CompletionReportView.aspx?CompletionReportId={0}");
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
            if (string.IsNullOrEmpty(this.CompletionReportId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CompletionReportAttachUrl&menuId={1}", CompletionReportId, BLL.Const.ProjectCompletionReportMenuId)));
        }
        #endregion
    }
}