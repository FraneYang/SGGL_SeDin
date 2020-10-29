using BLL;
using System;
using System.Linq;
using System.Web;

namespace FineUIPro.Web.HSSE.Accident
{
    public partial class AccidentReportEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string AccidentReportId
        {
            get
            {
                return (string)ViewState["AccidentReportId"];
            }
            set
            {
                ViewState["AccidentReportId"] = value;
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
                var unit = UnitService.GetUnitByUnitId(Const.UnitId_SEDIN);
                if (unit != null && !string.IsNullOrEmpty(unit.UnitCode))
                {
                    string url = "../../Images/SUBimages/" + unit.UnitCode + ".jpg";
                    if (url.Contains('*'))
                    {
                        url = url.Replace('*', '-');
                    }
                    this.Image1.ImageUrl = url;
                }

                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(this.ProjectId))
                {
                    this.lblProjectName.Text = BLL.ProjectService.GetProjectByProjectId(this.ProjectId).ProjectName;
                }
                int l = 18 - this.lblProjectName.Text.Length;
                this.lblProjectName.Margin = "5 0 0 " + (10 + 7 * l).ToString();
                this.txtAccidentReportName.Text = "管理体系文件-施工管理";
                this.InitDropDownList();
                this.AccidentReportId = Request.Params["AccidentReportId"];
                this.txtWorkingHoursLoss.Text = "0";
                this.txtEconomicLoss.Text = "0";
                this.txtEconomicOtherLoss.Text = "0";
                if (!string.IsNullOrEmpty(this.AccidentReportId))
                {
                    var accidentReport = BLL.AccidentReportService.GetAccidentReportById(this.AccidentReportId);
                    if (accidentReport != null)
                    {
                        this.ProjectId = accidentReport.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtAccidentReportCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.AccidentReportId);
                        //this.drpAccidentReportType.SelectedValue = accidentReport.AccidentReportType;
                        if (!string.IsNullOrEmpty(accidentReport.UnitId))
                        {
                            this.drpUnitId.SelectedValue = accidentReport.UnitId;
                        }
                        if (!string.IsNullOrEmpty(accidentReport.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = accidentReport.CompileMan;
                        }
                        if (accidentReport.CompileDate != null)
                        {
                            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", accidentReport.CompileDate);
                        }
                        this.txtFileContents.Text = HttpUtility.HtmlDecode(accidentReport.FileContent);
                        this.txtAccidentReportName.Text = accidentReport.AccidentReportName;
                        if (!string.IsNullOrEmpty(accidentReport.AccidentTypeId))
                        {
                            this.drpAccidentTypeId.SelectedValue = accidentReport.AccidentTypeId;
                        }
                        if (accidentReport.AccidentTypeId == "1" || accidentReport.AccidentTypeId == "2" || accidentReport.AccidentTypeId == "3")
                        {
                            if (accidentReport.IsNotConfirm == true)  //待定事故
                            {
                                this.trConfirm.Hidden = true;
                                this.trNotConfirm.Hidden = false;
                                this.drpUnitId2.SelectedValue = accidentReport.UnitId;
                                this.txtNotConfirmWorkingHoursLoss.Text = accidentReport.NotConfirmWorkingHoursLoss;
                                this.txtNotConfirmEconomicLoss.Text = accidentReport.NotConfirmEconomicLoss;
                                this.txtNotConfirmEconomicOtherLoss.Text = accidentReport.NotConfirmEconomicOtherLoss;
                            }
                            else  //确定事故
                            {
                                this.trConfirm.Hidden = false;
                                this.trNotConfirm.Hidden = true;
                                this.drpUnitId.SelectedValue = accidentReport.UnitId;
                                if (accidentReport.WorkingHoursLoss != null)
                                {
                                    this.txtWorkingHoursLoss.Text = Convert.ToString(accidentReport.WorkingHoursLoss);
                                }
                                if (accidentReport.EconomicLoss != null)
                                {
                                    this.txtEconomicLoss.Text = Convert.ToString(accidentReport.EconomicLoss);
                                }
                                if (accidentReport.EconomicOtherLoss != null)
                                {
                                    this.txtEconomicOtherLoss.Text = Convert.ToString(accidentReport.EconomicOtherLoss);
                                }
                            }
                        }
                        else
                        {
                            if (accidentReport.WorkingHoursLoss != null)
                            {
                                this.txtWorkingHoursLoss.Text = Convert.ToString(accidentReport.WorkingHoursLoss);
                            }
                            if (accidentReport.EconomicLoss != null)
                            {
                                this.txtEconomicLoss.Text = Convert.ToString(accidentReport.EconomicLoss);
                            }
                            if (accidentReport.EconomicOtherLoss != null)
                            {
                                this.txtEconomicOtherLoss.Text = Convert.ToString(accidentReport.EconomicOtherLoss);
                            }
                        }
                        if (accidentReport.IsNotConfirm != null)
                        {
                            this.rbIsNotConfirm.Hidden = false;
                            if (accidentReport.IsNotConfirm == true)   //待定
                            {
                                this.rbIsNotConfirm.SelectedValue = "True";
                            }
                            else
                            {
                                this.rbIsNotConfirm.SelectedValue = "False";
                            }
                        }
                        this.txtAbstract.Text = accidentReport.Abstract;
                        if (accidentReport.AccidentDate != null)
                        {
                            this.txtAccidentDate.Text = string.Format("{0:yyyy-MM-dd}", accidentReport.AccidentDate);
                        }
                        this.txtWorkArea.Text = accidentReport.WorkArea;
                        if (accidentReport.PeopleNum != null)
                        {
                            this.txtPeopleNum.Text = Convert.ToString(accidentReport.PeopleNum);
                        }
                        this.txtReportMan.Text = accidentReport.ReportMan;
                        this.txtReporterUnit.Text = accidentReport.ReporterUnit;
                        if (accidentReport.ReportDate != null)
                        {
                            this.txtReportDate.Text = string.Format("{0:yyyy-MM-dd}", accidentReport.ReportDate);
                        }
                        this.txtProcessDescription.Text = accidentReport.ProcessDescription;
                        this.txtEmergencyMeasures.Text = accidentReport.EmergencyMeasures;
                    }
                }
                else
                {
                    this.txtAccidentReportCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectAccidentReportMenuId, this.ProjectId, this.CurrUser.UnitId);
                    //var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectAccidentReportMenuId, this.ProjectId);
                    //if (codeTemplateRule != null)
                    //{
                    //    this.txtFileContents.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    //}
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectAccidentReportMenuId;
                this.ctlAuditFlow.DataId = this.AccidentReportId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }
        #endregion

        /// <summary>
        /// 事故类型选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpAccidentTypeId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpAccidentTypeId.SelectedValue == "1" || this.drpAccidentTypeId.SelectedValue == "2" || this.drpAccidentTypeId.SelectedValue == "3")
            {
                this.frIsNotConfirm.Hidden = false;
                if (this.drpAccidentTypeId.SelectedValue == "1")
                {
                    this.txtWorkingHoursLoss.Readonly = true;
                }
                else
                {
                    this.txtWorkingHoursLoss.Readonly = false;
                }
            }
            else
            {
                this.txtWorkingHoursLoss.Readonly = false;
                this.frIsNotConfirm.Hidden = true;
            }
        }

        /// <summary>
        /// 是否待定选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rbIsNotConfirm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rbIsNotConfirm.SelectedValue == "True")
            {
                this.trConfirm.Hidden = true;
                this.trNotConfirm.Hidden = false;
            }
            else
            {
                this.trConfirm.Hidden = false;
                this.trNotConfirm.Hidden = true;
                if (this.drpAccidentTypeId.SelectedValue == "1")
                {
                    this.txtWorkingHoursLoss.Readonly = true;
                }
            }
        }

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {            
            BLL.UnitService.InitUnitDropDownList(this.drpUnitId, this.ProjectId, true);
            BLL.UnitService.InitUnitDropDownList(this.drpUnitId2, this.ProjectId, true);
            BLL.UserService.InitUserDropDownList(this.drpCompileMan, this.ProjectId, true);
            BLL.ConstValue.InitConstValueDropDownList(this.drpAccidentTypeId, BLL.ConstValue.Group_AccidentReportRegistration, true);            
        }

        #region 保存、提交
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {          
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
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 数据保存
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            if (this.drpAccidentTypeId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择事故类型！", MessageBoxIcon.Warning);
                return;
            }
            if (this.rbIsNotConfirm.SelectedValue == "True")
            {
                if (this.drpUnitId2.SelectedValue == BLL.Const._Null)
                {
                    Alert.ShowInTop("请选择事故责任单位！", MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                if (this.drpUnitId.SelectedValue == BLL.Const._Null)
                {
                    Alert.ShowInTop("请选择事故责任单位！", MessageBoxIcon.Warning);
                    return;
                }
            }

            Model.Accident_AccidentReport accidentReport = new Model.Accident_AccidentReport
            {
                ProjectId = this.ProjectId,
                AccidentReportCode = this.txtAccidentReportCode.Text.Trim(),
                //accidentReport.AccidentReportType = this.drpAccidentReportType.SelectedValue;
                FileContent = HttpUtility.HtmlEncode(this.txtFileContents.Text)
            };
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                accidentReport.CompileMan = this.drpCompileMan.SelectedValue;
            }
            accidentReport.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
            accidentReport.States = BLL.Const.State_0;
            accidentReport.AccidentReportName = this.txtAccidentReportName.Text.Trim();
            if (this.drpAccidentTypeId.SelectedValue != BLL.Const._Null)
            {
                accidentReport.AccidentTypeId = this.drpAccidentTypeId.SelectedValue;
                if (this.rbIsNotConfirm.Hidden == false)
                {
                    accidentReport.IsNotConfirm = Convert.ToBoolean(this.rbIsNotConfirm.SelectedValue);
                }
            }
            accidentReport.Abstract = this.txtAbstract.Text.Trim();
            accidentReport.AccidentDate = Funs.GetNewDateTime(this.txtAccidentDate.Text.Trim());
            accidentReport.WorkArea = this.txtWorkArea.Text;
            accidentReport.PeopleNum = Funs.GetNewIntOrZero(this.txtPeopleNum.Text.Trim());
            if (accidentReport.IsNotConfirm == true)  //待定事故
            {
                accidentReport.UnitId = this.drpUnitId2.SelectedValue;
                accidentReport.NotConfirmWorkingHoursLoss = this.txtNotConfirmWorkingHoursLoss.Text.Trim();
                accidentReport.NotConfirmEconomicLoss = this.txtNotConfirmEconomicLoss.Text.Trim();
                accidentReport.NotConfirmEconomicOtherLoss = this.txtNotConfirmEconomicOtherLoss.Text.Trim();
            }
            else
            {
                accidentReport.UnitId = this.drpUnitId.SelectedValue;
                accidentReport.WorkingHoursLoss = Funs.GetNewDecimalOrZero(this.txtWorkingHoursLoss.Text.Trim());
                accidentReport.EconomicLoss = Funs.GetNewDecimalOrZero(this.txtEconomicLoss.Text.Trim());
                accidentReport.EconomicOtherLoss = Funs.GetNewDecimalOrZero(this.txtEconomicOtherLoss.Text.Trim());
            }
            accidentReport.ReportMan = this.txtReportMan.Text.Trim();
            accidentReport.ReporterUnit = this.txtReporterUnit.Text.Trim();
            accidentReport.ReportDate = Funs.GetNewDateTime(this.txtReportDate.Text.Trim());
            accidentReport.ProcessDescription = this.txtProcessDescription.Text.Trim();
            accidentReport.EmergencyMeasures = this.txtEmergencyMeasures.Text.Trim();
            if (type == BLL.Const.BtnSubmit)
            {
                accidentReport.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.AccidentReportId))
            {
                accidentReport.AccidentReportId = this.AccidentReportId;
                BLL.AccidentReportService.UpdateAccidentReport(accidentReport);
                BLL.LogService.AddSys_Log(this.CurrUser, this.txtAccidentReportCode.Text, this.AccidentReportId, BLL.Const.ProjectAccidentReportMenuId, Const.BtnModify);
                Model.Accident_AccidentReport a = BLL.AccidentReportService.GetAccidentReportById(this.AccidentReportId);                
            }
            else
            {
                this.AccidentReportId = SQLHelper.GetNewID(typeof(Model.Accident_AccidentReport));
                accidentReport.AccidentReportId = this.AccidentReportId;
                if (accidentReport.IsNotConfirm == true)  //待定事故
                {
                    accidentReport.NotConfirmed = true;
                }
                BLL.AccidentReportService.AddAccidentReport(accidentReport);
                BLL.LogService.AddSys_Log(this.CurrUser, this.txtAccidentReportCode.Text, this.AccidentReportId, BLL.Const.ProjectAccidentReportMenuId, Const.BtnAdd);                
            }
            //保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectAccidentReportMenuId, this.AccidentReportId, (type == BLL.Const.BtnSubmit ? true : false), accidentReport.AccidentReportCode, "../Accident/AccidentReportView.aspx?AccidentReportId={0}");
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
            if (string.IsNullOrEmpty(this.AccidentReportId))
            {
                if (this.drpAccidentTypeId.SelectedValue == BLL.Const._Null)
                {
                    Alert.ShowInTop("请选择事故类型！", MessageBoxIcon.Warning);
                    return;
                }
                if (this.rbIsNotConfirm.SelectedValue == "True")
                {
                    if (this.drpUnitId2.SelectedValue == BLL.Const._Null)
                    {
                        Alert.ShowInTop("请选择事故责任单位！", MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    if (this.drpUnitId.SelectedValue == BLL.Const._Null)
                    {
                        Alert.ShowInTop("请选择事故责任单位！", MessageBoxIcon.Warning);
                        return;
                    }
                }
                this.SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/AccidentReportAttachUrl&menuId={1}", this.AccidentReportId, BLL.Const.ProjectAccidentReportMenuId)));
        }
        #endregion
    }
}
