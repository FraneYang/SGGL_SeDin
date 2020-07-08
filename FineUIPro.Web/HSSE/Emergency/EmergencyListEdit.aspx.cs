using BLL;
using System;
using System.Web;

namespace FineUIPro.Web.HSSE.Emergency
{
    public partial class EmergencyListEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string EmergencyListId
        {
            get
            {
                return (string)ViewState["EmergencyListId"];
            }
            set
            {
                ViewState["EmergencyListId"] = value;
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

                this.drpAuditMan.DataValueField = "UserId";
                this.drpAuditMan.DataTextField = "UserName";
                this.drpAuditMan.DataSource = BLL.UserService.GetProjectUserListByProjectId(this.CurrUser.LoginProjectId);
                this.drpAuditMan.DataBind();
                Funs.FineUIPleaseSelect(this.drpAuditMan);

                this.drpApproveMan.DataValueField = "UserId";
                this.drpApproveMan.DataTextField = "UserName";
                this.drpApproveMan.DataSource = BLL.UserService.GetProjectUserListByProjectId(this.CurrUser.LoginProjectId);
                this.drpApproveMan.DataBind();
                Funs.FineUIPleaseSelect(this.drpApproveMan);

                this.EmergencyListId = Request.Params["EmergencyListId"];
                if (!string.IsNullOrEmpty(this.EmergencyListId))
                {
                    Model.Emergency_EmergencyList EmergencyList = BLL.EmergencyListService.GetEmergencyListById(this.EmergencyListId);
                    if (EmergencyList != null)
                    {
                        this.ProjectId = EmergencyList.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        ///读取编号
                        this.txtEmergencyCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.EmergencyListId);
                        this.txtEmergencyName.Text = EmergencyList.EmergencyName;
                        if (!string.IsNullOrEmpty(EmergencyList.UnitId))
                        {
                            this.drpUnit.SelectedValue = EmergencyList.UnitId;
                        }
                        if (!string.IsNullOrEmpty(EmergencyList.EmergencyTypeId))
                        {
                            this.drpEmergencyType.SelectedValue = EmergencyList.EmergencyTypeId;
                        }
                        this.txtVersionCode.Text = EmergencyList.VersionCode;
                        if (!string.IsNullOrEmpty(EmergencyList.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = EmergencyList.CompileMan;
                        }
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", EmergencyList.CompileDate);
                        if (!string.IsNullOrEmpty(EmergencyList.AuditMan))
                        {
                            this.drpAuditMan.SelectedValue = EmergencyList.AuditMan;
                        }
                        if (!string.IsNullOrEmpty(EmergencyList.ApproveMan))
                        {
                            this.drpApproveMan.SelectedValue = EmergencyList.ApproveMan;
                        }
                        this.txtEmergencyContents.Text = HttpUtility.HtmlDecode(EmergencyList.EmergencyContents);
                    }
                }
                else
                {   
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.drpUnit.SelectedValue = this.CurrUser.UnitId;
                    this.txtVersionCode.Text = "V1.0";
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    //var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectEmergencyListMenuId, this.ProjectId);
                    //if (codeTemplateRule != null)
                    //{
                    //    this.txtEmergencyContents.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    //}
                    ////自动生成编码
                    this.txtEmergencyCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectEmergencyListMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtEmergencyName.Text = this.SimpleForm1.Title;
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectEmergencyListMenuId;
                this.ctlAuditFlow.DataId = this.EmergencyListId;
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
            BLL.UnitService.InitUnitDropDownList(this.drpUnit, this.ProjectId, true);
            BLL.UserService.InitUserDropDownList(this.drpCompileMan, this.ProjectId, true);
            BLL.EmergencyTypeService.InitEmergencyTypeDropDownList(this.drpEmergencyType, true);
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
            Model.Emergency_EmergencyList EmergencyList = new Model.Emergency_EmergencyList
            {
                ProjectId = this.ProjectId,
                EmergencyCode = this.txtEmergencyCode.Text.Trim(),
                EmergencyName = this.txtEmergencyName.Text.Trim()
            };
            if (this.drpUnit.SelectedValue != BLL.Const._Null)
            {
                EmergencyList.UnitId = this.drpUnit.SelectedValue;
            }
            if (this.drpEmergencyType.SelectedValue != BLL.Const._Null)
            {
                EmergencyList.EmergencyTypeId = this.drpEmergencyType.SelectedValue;
            }
            EmergencyList.VersionCode = this.txtVersionCode.Text.Trim();
            EmergencyList.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
            if (this.drpCompileMan.SelectedValue!=BLL.Const._Null)
            {
                EmergencyList.CompileMan = this.drpCompileMan.SelectedValue;
            }
            EmergencyList.EmergencyContents = HttpUtility.HtmlEncode(this.txtEmergencyContents.Text);
            if (this.drpAuditMan.SelectedValue != BLL.Const._Null)
            {
                EmergencyList.AuditMan = this.drpAuditMan.SelectedValue;
            }
            if (this.drpAuditMan.SelectedValue != BLL.Const._Null)
            {
                EmergencyList.ApproveMan = this.drpApproveMan.SelectedValue;
            }
            ////单据状态
            EmergencyList.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                EmergencyList.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.EmergencyListId))
            {
                EmergencyList.EmergencyListId = this.EmergencyListId;
                BLL.EmergencyListService.UpdateEmergencyList(EmergencyList);
                BLL.LogService.AddSys_Log(this.CurrUser, EmergencyList.EmergencyCode, EmergencyList.EmergencyListId, BLL.Const.ProjectEmergencyListMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.EmergencyListId = SQLHelper.GetNewID(typeof(Model.Emergency_EmergencyList));
                EmergencyList.EmergencyListId = this.EmergencyListId;
                BLL.EmergencyListService.AddEmergencyList(EmergencyList);
                BLL.LogService.AddSys_Log(this.CurrUser, EmergencyList.EmergencyCode, EmergencyList.EmergencyListId, BLL.Const.ProjectEmergencyListMenuId, BLL.Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectEmergencyListMenuId, this.EmergencyListId, (type == BLL.Const.BtnSubmit ? true : false), EmergencyList.EmergencyName, "../Emergency/EmergencyListView.aspx?EmergencyListId={0}");
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
            if (string.IsNullOrEmpty(this.EmergencyListId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/EmergencyListAttachUrl&menuId={1}", EmergencyListId,BLL.Const.ProjectEmergencyListMenuId)));
        }
        #endregion
    }
}