using BLL;
using System;
using System.Web;

namespace FineUIPro.Web.HSSE.License
{
    public partial class LicenseManagerEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string LicenseManagerId
        {
            get
            {
                return (string)ViewState["LicenseManagerId"];
            }
            set
            {
                ViewState["LicenseManagerId"] = value;
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
                this.LicenseManagerId = Request.Params["LicenseManagerId"];
                if (!string.IsNullOrEmpty(this.LicenseManagerId))
                {
                    Model.License_LicenseManager licenseManager = BLL.LicenseManagerService.GetLicenseManagerById(this.LicenseManagerId);
                    if (licenseManager != null)
                    {
                        this.ProjectId = licenseManager.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtLicenseManagerCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.LicenseManagerId);
                        //this.txtLicenseManageName.Text = licenseManager.LicenseManageName;
                        if (!string.IsNullOrEmpty(licenseManager.UnitId))
                        {
                            this.drpUnitId.SelectedValue = licenseManager.UnitId;
                        }
                        if (!string.IsNullOrEmpty(licenseManager.LicenseTypeId))
                        {
                            this.drpLicenseTypeId.SelectedValue = licenseManager.LicenseTypeId;
                        }
                        this.txtApplicantMan.Text = licenseManager.ApplicantMan;
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", licenseManager.CompileDate);
                        this.txtLicenseManageContents.Text = HttpUtility.HtmlDecode(licenseManager.LicenseManageContents);
                        if (!string.IsNullOrEmpty(licenseManager.WorkAreaId))
                        {
                            this.drpWorkAreaId.SelectedValueArray = licenseManager.WorkAreaId.Split(',');
                        }
                        this.txtStartDate.Text = string.Format("{0:yyyy-MM-dd}", licenseManager.StartDate);
                        this.txtEndDate.Text = string.Format("{0:yyyy-MM-dd}", licenseManager.EndDate);
                        this.drpStates.SelectedValue = licenseManager.WorkStates;
                    }
                }
                else
                {
                    this.txtApplicantMan.Text = this.CurrUser.UserName;
                    this.drpUnitId.SelectedValue = this.CurrUser.UnitId;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtStartDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtEndDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(1));
                    //var pcodeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectLicenseManagerMenuId, this.ProjectId);
                    //if (pcodeTemplateRule != null)
                    //{                  
                    //    this.txtLicenseManageContents.Text = HttpUtility.HtmlDecode(pcodeTemplateRule.Template);
                    //}  
                    ////自动生成编码
                    this.txtLicenseManagerCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectLicenseManagerMenuId, this.ProjectId, this.CurrUser.UnitId);
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectLicenseManagerMenuId;
                this.ctlAuditFlow.DataId = this.LicenseManagerId;
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
            UnitService.InitUnitDropDownList(this.drpUnitId, this.ProjectId, true);
            LicenseTypeService.InitLicenseTypeDropDownList(this.drpLicenseTypeId, true);
            UnitWorkService.InitUnitWorkDownList(this.drpWorkAreaId, this.ProjectId, false);
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
            if (this.drpLicenseTypeId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择许可证类型！", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpUnitId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择申请单位！", MessageBoxIcon.Warning);
                return;
            }

            Model.License_LicenseManager licenseManager = new Model.License_LicenseManager
            {
                ProjectId = this.ProjectId,
                LicenseManagerCode = this.txtLicenseManagerCode.Text.Trim()
            };
            //licenseManager.LicenseManageName = this.txtLicenseManageName.Text.Trim();
            if (this.drpLicenseTypeId.SelectedValue != BLL.Const._Null)
            {
                licenseManager.LicenseTypeId = this.drpLicenseTypeId.SelectedValue;
            }
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                licenseManager.UnitId = this.drpUnitId.SelectedValue;
            }
            
            licenseManager.ApplicantMan = this.txtApplicantMan.Text.Trim();
            if (!string.IsNullOrEmpty(this.drpWorkAreaId.SelectedValue))
            {
                string workAreaIds = string.Empty;
                var workAreas = this.drpWorkAreaId.SelectedValueArray;
                foreach (var item in workAreas)
                {
                    workAreaIds += item + ",";
                }
                licenseManager.WorkAreaId = workAreaIds;
            }
            licenseManager.StartDate = Funs.GetNewDateTime(this.txtStartDate.Text);
            licenseManager.EndDate = Funs.GetNewDateTime(this.txtEndDate.Text);
            licenseManager.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
            licenseManager.LicenseManageContents = HttpUtility.HtmlEncode(this.txtLicenseManageContents.Text);
            licenseManager.States = BLL.Const.State_0;
            if (!string.IsNullOrEmpty(this.drpStates.SelectedValue))
            {
                licenseManager.WorkStates = this.drpStates.SelectedValue;
            }
            else
            {
                licenseManager.WorkStates =null;
            }
            if (type == BLL.Const.BtnSubmit)
            {
                licenseManager.States = this.ctlAuditFlow.NextStep;
                if (licenseManager.States == Const.State_2 && licenseManager.WorkStates != Const.State_R)
                {
                    licenseManager.WorkStates = Const.State_3;
                }
            }
            if (!string.IsNullOrEmpty(this.LicenseManagerId))
            {
                licenseManager.LicenseManagerId = this.LicenseManagerId;
                BLL.LicenseManagerService.UpdateLicenseManager(licenseManager);
                BLL.LogService.AddSys_Log(this.CurrUser, licenseManager.LicenseManagerCode, licenseManager.LicenseManagerId, BLL.Const.ProjectLicenseManagerMenuId, BLL.Const.BtnModify);
            }
            else
            {
                licenseManager.CompileMan = this.CurrUser.UserId;
                this.LicenseManagerId = SQLHelper.GetNewID(typeof(Model.License_LicenseManager));
                licenseManager.LicenseManagerId = this.LicenseManagerId;
                BLL.LicenseManagerService.AddLicenseManager(licenseManager);
                BLL.LogService.AddSys_Log(this.CurrUser, licenseManager.LicenseManagerCode, licenseManager.LicenseManagerId, BLL.Const.ProjectLicenseManagerMenuId, BLL.Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectLicenseManagerMenuId, this.LicenseManagerId, (type == BLL.Const.BtnSubmit ? true : false), licenseManager.LicenseManageName, "../License/LicenseManagerView.aspx?LicenseManagerId={0}");
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
            if (string.IsNullOrEmpty(this.LicenseManagerId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/LicenseManagerAttachUrl&menuId={1}", LicenseManagerId, BLL.Const.ProjectLicenseManagerMenuId)));
        }
        #endregion

        #region 单位选择事件
        /// <summary>
        /// 单位选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpLicenseTypeId_SelectedIndexChanged(object sender, EventArgs e)
        {
            var type = BLL.LicenseTypeService.GetLicenseTypeById(this.drpLicenseTypeId.SelectedValue);
            if (type != null)
            {
                this.txtLicenseManageContents.Text = HttpUtility.HtmlDecode(type.LicenseContents);
            }
        }
        #endregion
    }
}