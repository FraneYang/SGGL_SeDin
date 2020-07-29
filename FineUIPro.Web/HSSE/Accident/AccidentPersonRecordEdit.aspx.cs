using BLL;
using System;
using System.Linq;
using System.Web;

namespace FineUIPro.Web.HSSE.Accident
{
    public partial class AccidentPersonRecordEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string AccidentPersonRecordId
        {
            get
            {
                return (string)ViewState["AccidentPersonRecordId"];
            }
            set
            {
                ViewState["AccidentPersonRecordId"] = value;
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.InitDropDownList();

                this.AccidentPersonRecordId = Request.Params["AccidentPersonRecordId"];
                if (!string.IsNullOrEmpty(this.AccidentPersonRecordId))
                {
                    Model.Accident_AccidentPersonRecord accidentPersonRecord = BLL.AccidentPersonRecordService.GetAccidentPersonRecordById(this.AccidentPersonRecordId);
                    if (accidentPersonRecord != null)
                    {
                        this.ProjectId = accidentPersonRecord.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }

                        if (!string.IsNullOrEmpty(accidentPersonRecord.ProjectId))
                        {
                            var project = BLL.ProjectService.GetProjectByProjectId(accidentPersonRecord.ProjectId);
                            if (project != null)
                            {
                                this.txtProjectName.Text = project.ProjectName;
                            }
                        }
                        if (!string.IsNullOrEmpty(accidentPersonRecord.AccidentTypeId))
                        {
                            this.drpAccidentTypeId.SelectedValue = accidentPersonRecord.AccidentTypeId;
                        }
                        if (!string.IsNullOrEmpty(accidentPersonRecord.WorkAreaId))
                        {
                            this.drpWorkAreaId.SelectedValue = accidentPersonRecord.WorkAreaId;
                        }
                        if (accidentPersonRecord.AccidentDate != null)
                        {
                            this.txtAccidentDate.Text = string.Format("{0:yyyy-MM-dd}", accidentPersonRecord.AccidentDate);
                        }
                        if (!string.IsNullOrEmpty(accidentPersonRecord.PersonId))
                        {
                            this.drpPersonId.SelectedValue = accidentPersonRecord.PersonId;
                        }
                        if (!string.IsNullOrEmpty(accidentPersonRecord.Injury))
                        {
                            this.drpInjury.SelectedValue = accidentPersonRecord.Injury;
                        }
                        this.txtInjuryPart.Text = accidentPersonRecord.InjuryPart;
                        this.txtHssePersons.Text = accidentPersonRecord.HssePersons;
                        this.txtInjuryResult.Text = accidentPersonRecord.InjuryResult;
                        this.txtPreventiveAction.Text = accidentPersonRecord.PreventiveAction;
                        this.txtHandleOpinion.Text = accidentPersonRecord.HandleOpinion;
                        this.txtFileContents.Text = HttpUtility.HtmlDecode(accidentPersonRecord.FileContent);
                    }
                }
                else
                {
                    this.txtProjectName.Text = BLL.ProjectService.GetProjectNameByProjectId(this.ProjectId);
                    this.txtAccidentDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    //var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectAccidentPersonRecordMenuId, this.ProjectId);
                    //if (codeTemplateRule != null)
                    //{
                    //    this.txtFileContents.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    //}
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectAccidentPersonRecordMenuId;
                this.ctlAuditFlow.DataId = this.AccidentPersonRecordId;
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
            AccidentTypeService.InitAccidentTypeDropDownList(this.drpAccidentTypeId,true);
            UnitWorkService.InitUnitWorkDownList(this.drpWorkAreaId, this.ProjectId, true);
            PersonService.InitPersonByProjectUnitDropDownList(this.drpPersonId, this.ProjectId, string.Empty, true);
            BLL.ConstValue.InitConstValueDropDownList(this.drpInjury, ConstValue.Group_Accident, true);
        }

        #region 保存、提交
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.drpAccidentTypeId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择事故类别！", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpWorkAreaId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择单位工程！", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpPersonId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择人员姓名！", MessageBoxIcon.Warning);
                return;
            }
            SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.drpAccidentTypeId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择事故类别！", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpWorkAreaId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择单位工程！", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpPersonId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择人员姓名！", MessageBoxIcon.Warning);
                return;
            }
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
            Model.Accident_AccidentPersonRecord accidentPersonRecord = new Model.Accident_AccidentPersonRecord
            {
                ProjectId = this.ProjectId
            };
            if (this.drpAccidentTypeId.SelectedValue != BLL.Const._Null)
            {
                accidentPersonRecord.AccidentTypeId = this.drpAccidentTypeId.SelectedValue;
            }
            if (this.drpWorkAreaId.SelectedValue != BLL.Const._Null)
            {
                accidentPersonRecord.WorkAreaId = this.drpWorkAreaId.SelectedValue;
            }
            if (!string.IsNullOrEmpty(this.txtAccidentDate.Text.Trim()))
            {
                accidentPersonRecord.AccidentDate = Convert.ToDateTime(this.txtAccidentDate.Text.Trim());
            }
            if (this.drpPersonId.SelectedValue != BLL.Const._Null)
            {
                accidentPersonRecord.PersonId = this.drpPersonId.SelectedValue;
            }
            if (this.drpInjury.SelectedValue != BLL.Const._Null)
            {
                accidentPersonRecord.Injury = this.drpInjury.SelectedValue;
            }

            accidentPersonRecord.InjuryPart = this.txtInjuryPart.Text.Trim();
            accidentPersonRecord.HssePersons = this.txtHssePersons.Text.Trim();
            accidentPersonRecord.InjuryResult = this.txtInjuryResult.Text.Trim();
            accidentPersonRecord.PreventiveAction = this.txtPreventiveAction.Text.Trim();
            accidentPersonRecord.HandleOpinion = this.txtHandleOpinion.Text.Trim();
            accidentPersonRecord.FileContent = HttpUtility.HtmlEncode(this.txtFileContents.Text);
            accidentPersonRecord.CompileMan = this.CurrUser.UserId;
            accidentPersonRecord.CompileDate = DateTime.Now;
            accidentPersonRecord.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                var flowOperate = Funs.DB.Sys_FlowOperate.FirstOrDefault(x => x.DataId == this.AccidentPersonRecordId && x.State == BLL.Const.State_2 && x.IsClosed == true);
                if (flowOperate != null)
                {
                    accidentPersonRecord.States = BLL.Const.State_2;
                }
                else
                {
                    accidentPersonRecord.States = this.ctlAuditFlow.NextStep;
                }
            }
            if (!string.IsNullOrEmpty(this.AccidentPersonRecordId))
            {
                accidentPersonRecord.AccidentPersonRecordId = this.AccidentPersonRecordId;
                BLL.AccidentPersonRecordService.UpdateAccidentPersonRecord(accidentPersonRecord);
                BLL.LogService.AddSys_Log(this.CurrUser, string.Empty, this.AccidentPersonRecordId, BLL.Const.ProjectAccidentPersonRecordMenuId, Const.BtnModify);
            }
            else
            {
                this.AccidentPersonRecordId = SQLHelper.GetNewID(typeof(Model.Accident_AccidentPersonRecord));
                accidentPersonRecord.AccidentPersonRecordId = this.AccidentPersonRecordId;
                BLL.AccidentPersonRecordService.AddAccidentPersonRecord(accidentPersonRecord);
                BLL.LogService.AddSys_Log(this.CurrUser, string.Empty, this.AccidentPersonRecordId, BLL.Const.ProjectAccidentPersonRecordMenuId, Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectAccidentPersonRecordMenuId, this.AccidentPersonRecordId, (type == BLL.Const.BtnSubmit ? true : false), this.drpPersonId.SelectedText, "../Accident/AccidentPersonRecordView.aspx?AccidentPersonRecordId={0}");
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
            if (string.IsNullOrEmpty(this.AccidentPersonRecordId))
            {
                if (this.drpAccidentTypeId.SelectedValue == BLL.Const._Null)
                {
                    Alert.ShowInTop("请选择事故类别！", MessageBoxIcon.Warning);
                    return;
                }
                if (this.drpWorkAreaId.SelectedValue == BLL.Const._Null)
                {
                    Alert.ShowInTop("请选择单位工程！", MessageBoxIcon.Warning);
                    return;
                }
                if (this.drpPersonId.SelectedValue == BLL.Const._Null)
                {
                    Alert.ShowInTop("请选择人员姓名！", MessageBoxIcon.Warning);
                    return;
                }
                this.SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/AccidentPersonRecordAttachUrl&menuId={1}", this.AccidentPersonRecordId, BLL.Const.ProjectAccidentPersonRecordMenuId)));
        }
        #endregion
    }
}