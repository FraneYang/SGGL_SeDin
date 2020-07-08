using BLL;
using System;
using System.Web;

namespace FineUIPro.Web.HSSE.Emergency
{
    public partial class EmergencySupplyEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string FileId
        {
            get
            {
                return (string)ViewState["FileId"];
            }
            set
            {
                ViewState["FileId"] = value;
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
                this.FileId = Request.Params["FileId"];
                if (!string.IsNullOrEmpty(this.FileId))
                {
                    Model.Emergency_EmergencySupply EmergencySupply = BLL.EmergencySupplyService.GetEmergencySupplyById(this.FileId);
                    if (EmergencySupply != null)
                    {
                        this.ProjectId = EmergencySupply.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        ///读取编号
                        this.txtFileCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.FileId);
                        this.txtFileName.Text = EmergencySupply.FileName;

                        if (!string.IsNullOrEmpty(EmergencySupply.UnitId))
                        {
                            this.drpUnit.SelectedValue = EmergencySupply.UnitId;
                        }
                        if (!string.IsNullOrEmpty(EmergencySupply.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = EmergencySupply.CompileMan;
                        }
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", EmergencySupply.CompileDate);
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(EmergencySupply.FileContent);
                    }
                }
                else
                {
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.drpUnit.SelectedValue = this.CurrUser.UnitId;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    //var FileCodeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectEmergencySupplyMenuId, this.ProjectId);
                    //if (FileCodeTemplateRule != null)
                    //{
                    //    this.txtFileContent.Text = HttpUtility.HtmlDecode(FileCodeTemplateRule.Template);
                    //}
                    ////自动生成编码
                    this.txtFileCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectEmergencySupplyMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtFileName.Text = this.SimpleForm1.Title;
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectEmergencySupplyMenuId;
                this.ctlAuditFlow.DataId = this.FileId;
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
            BLL.UnitService.InitUnitDropDownList(this.drpUnit, this.ProjectId, false);
            BLL.UserService.InitUserDropDownList(this.drpCompileMan, this.ProjectId, true);
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
            if (this.ctlAuditFlow.NextStep == BLL.Const.State_1 && this.ctlAuditFlow.NextPerson == BLL.Const._Null)
            {
                ShowNotify("请选择下一步办理人！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.Emergency_EmergencySupply EmergencySupply = new Model.Emergency_EmergencySupply
            {
                ProjectId = this.ProjectId,
                FileCode = this.txtFileCode.Text.Trim(),
                FileName = this.txtFileName.Text.Trim()
            };
            if (this.drpUnit.SelectedValue != BLL.Const._Null)
            {
                EmergencySupply.UnitId = this.drpUnit.SelectedValue;
            }
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                EmergencySupply.CompileMan = this.drpCompileMan.SelectedValue;
            }
            EmergencySupply.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
            EmergencySupply.FileContent = HttpUtility.HtmlEncode(this.txtFileContent.Text);
            ////单据状态
            EmergencySupply.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                EmergencySupply.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.FileId))
            {
                EmergencySupply.FileId = this.FileId;
                BLL.EmergencySupplyService.UpdateEmergencySupply(EmergencySupply);
                BLL.LogService.AddSys_Log(this.CurrUser, EmergencySupply.FileCode, EmergencySupply.FileId, BLL.Const.ProjectEmergencySupplyMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.FileId = SQLHelper.GetNewID(typeof(Model.Emergency_EmergencySupply));
                EmergencySupply.FileId = this.FileId;
                BLL.EmergencySupplyService.AddEmergencySupply(EmergencySupply);
                BLL.LogService.AddSys_Log(this.CurrUser, EmergencySupply.FileCode, EmergencySupply.FileId, BLL.Const.ProjectEmergencySupplyMenuId,BLL.Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectEmergencySupplyMenuId, this.FileId, (type == BLL.Const.BtnSubmit ? true : false), EmergencySupply.FileName, "../Emergency/EmergencySupplyView.aspx?FileId={0}");
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
            if (string.IsNullOrEmpty(this.FileId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/EmergencySupplyAttachUrl&menuId={1}", FileId, BLL.Const.ProjectEmergencySupplyMenuId)));
        }
        #endregion
    }
}