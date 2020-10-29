using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FineUIPro.Web.HSSE.Accident
{
    public partial class AccidentReportOtherEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string AccidentReportOtherId
        {
            get
            {
                return (string)ViewState["AccidentReportOtherId"];
            }
            set
            {
                ViewState["AccidentReportOtherId"] = value;
            }
        }

        /// <summary>
        /// 定义调查组成员集合
        /// </summary>
        private static List<Model.Accident_AccidentReportOtherItem> accidentReportOtherItems = new List<Model.Accident_AccidentReportOtherItem>();
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
                Funs.DropDownPageSize(this.ddlPageSize);
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
                if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
                {
                    this.lblProjectName.Text = BLL.ProjectService.GetProjectNameByProjectId(this.CurrUser.LoginProjectId);
                }
                int l = 18 - this.lblProjectName.Text.Length;
                if (this.lblProjectName.Text.Contains("金晖煤焦"))
                {
                    l = -5;
                }
                this.lblProjectName.Margin = "5 0 0 " + (10 + 9 * l).ToString();
                this.txtAccidentReportOtherName.Text = "管理体系文件-施工管理";
                BLL.ConstValue.InitConstValueDropDownList(this.drpAccidentTypeId, BLL.ConstValue.Group_AccidentInvestigationProcessingReport, true);
                this.drpUnitId.DataValueField = "UnitId";
                this.drpUnitId.DataTextField = "UnitName";
                BLL.UnitService.InitUnitDropDownList(this.drpUnitId, this.CurrUser.LoginProjectId, true);
                BLL.UserService.InitUserDropDownList(this.drpCompileMan, this.CurrUser.LoginProjectId, true);
                this.AccidentReportOtherId = Request.Params["AccidentReportOtherId"];
                this.txtEconomicLoss.Text = "0";
                this.txtEconomicOtherLoss.Text = "0";
                if (!string.IsNullOrEmpty(this.AccidentReportOtherId))
                {
                    Model.Accident_AccidentReportOther accidentReportOther = BLL.AccidentReportOtherService.GetAccidentReportOtherById(this.AccidentReportOtherId);
                    if (accidentReportOther != null)
                    {
                        this.txtAccidentReportOtherCode.Text = accidentReportOther.AccidentReportOtherCode;
                        this.txtAccidentReportOtherName.Text = accidentReportOther.AccidentReportOtherName;
                        if (!string.IsNullOrEmpty(accidentReportOther.AccidentTypeId))
                        {
                            this.drpAccidentTypeId.SelectedValue = accidentReportOther.AccidentTypeId;
                        }
                        this.txtAbstract.Text = accidentReportOther.Abstract;
                        if (accidentReportOther.AccidentDate != null)
                        {
                            this.txtAccidentDate.Text = string.Format("{0:yyyy-MM-dd}", accidentReportOther.AccidentDate);
                        }
                        this.txtWorkArea.Text = accidentReportOther.WorkArea;
                        if (accidentReportOther.PeopleNum != null)
                        {
                            this.txtPeopleNum.Text = Convert.ToString(accidentReportOther.PeopleNum);
                        }
                        if (!string.IsNullOrEmpty(accidentReportOther.UnitId))
                        {
                            this.drpUnitId.SelectedValue = accidentReportOther.UnitId;
                        }

                        if (accidentReportOther.EconomicLoss != null)
                        {
                            this.txtEconomicLoss.Text = Convert.ToString(accidentReportOther.EconomicLoss);
                        }
                        if (accidentReportOther.EconomicOtherLoss != null)
                        {
                            this.txtEconomicOtherLoss.Text = Convert.ToString(accidentReportOther.EconomicOtherLoss);
                        }
                        this.txtReportMan.Text = accidentReportOther.ReportMan;
                        this.txtReporterUnit.Text = accidentReportOther.ReporterUnit;
                        if (accidentReportOther.ReportDate != null)
                        {
                            this.txtReportDate.Text = string.Format("{0:yyyy-MM-dd}", accidentReportOther.ReportDate);
                        }
                        this.txtProcessDescription.Text = accidentReportOther.ProcessDescription;
                        this.txtEmergencyMeasures.Text = accidentReportOther.EmergencyMeasures;
                        this.txtImmediateCause.Text = accidentReportOther.ImmediateCause;
                        this.txtIndirectReason.Text = accidentReportOther.IndirectReason;
                        this.txtCorrectivePreventive.Text = accidentReportOther.CorrectivePreventive;
                        if (!string.IsNullOrEmpty(accidentReportOther.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = accidentReportOther.CompileMan;
                        }
                        if (accidentReportOther.CompileDate != null)
                        {
                            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", accidentReportOther.CompileDate);
                        }
                        this.txtFileContents.Text = HttpUtility.HtmlDecode(accidentReportOther.FileContent);
                    }
                    BindGrid();
                }
                else
                {
                    this.txtAccidentReportOtherCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectAccidentReportOtherMenuId, this.CurrUser.LoginProjectId, this.CurrUser.UnitId);
                    //var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectAccidentReportOtherMenuId, this.CurrUser.LoginProjectId);
                    //if (codeTemplateRule != null)
                    //{
                    //    this.txtFileContents.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    //}
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectAccidentReportOtherMenuId;
                this.ctlAuditFlow.DataId = this.AccidentReportOtherId;
                this.ctlAuditFlow.ProjectId = this.CurrUser.LoginProjectId;
            }
        }
        #endregion

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
                Alert.ShowInTop("请选择事故类型！", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpUnitId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择事故责任单位！", MessageBoxIcon.Warning);
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
                Alert.ShowInTop("请选择事故类型！", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpUnitId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择事故责任单位！", MessageBoxIcon.Warning);
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
            Model.Accident_AccidentReportOther accidentReportOther = new Model.Accident_AccidentReportOther
            {
                ProjectId = this.CurrUser.LoginProjectId,
                AccidentReportOtherCode = this.txtAccidentReportOtherCode.Text.Trim(),
                AccidentReportOtherName = this.txtAccidentReportOtherName.Text.Trim()
            };
            if (this.drpAccidentTypeId.SelectedValue != BLL.Const._Null)
            {
                accidentReportOther.AccidentTypeId = this.drpAccidentTypeId.SelectedValue;
            }
            accidentReportOther.Abstract = this.txtAbstract.Text.Trim();
            accidentReportOther.AccidentDate = Funs.GetNewDateTime(this.txtAccidentDate.Text.Trim());
            accidentReportOther.WorkArea = this.txtWorkArea.Text;
            accidentReportOther.PeopleNum = Funs.GetNewIntOrZero(this.txtPeopleNum.Text.Trim());
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                accidentReportOther.UnitId = this.drpUnitId.SelectedValue;
            }
            accidentReportOther.WorkingHoursLoss = 0;
            accidentReportOther.EconomicLoss = Funs.GetNewDecimalOrZero(this.txtEconomicLoss.Text.Trim());
            accidentReportOther.EconomicOtherLoss = Funs.GetNewDecimalOrZero(this.txtEconomicOtherLoss.Text.Trim());
            accidentReportOther.ReportMan = this.txtReportMan.Text.Trim();
            accidentReportOther.ReporterUnit = this.txtReporterUnit.Text.Trim();
            accidentReportOther.ReportDate = Funs.GetNewDateTime(this.txtReportDate.Text.Trim());
            accidentReportOther.ProcessDescription = this.txtProcessDescription.Text.Trim();
            accidentReportOther.EmergencyMeasures = this.txtEmergencyMeasures.Text.Trim();
            accidentReportOther.ImmediateCause = this.txtImmediateCause.Text.Trim();
            accidentReportOther.IndirectReason = this.txtIndirectReason.Text.Trim();
            accidentReportOther.CorrectivePreventive = this.txtCorrectivePreventive.Text.Trim();
            accidentReportOther.FileContent = HttpUtility.HtmlEncode(this.txtFileContents.Text);
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                accidentReportOther.CompileMan = this.drpCompileMan.SelectedValue;
            }
            accidentReportOther.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
            accidentReportOther.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                accidentReportOther.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.AccidentReportOtherId))
            {
                accidentReportOther.AccidentReportOtherId = this.AccidentReportOtherId;
                BLL.AccidentReportOtherService.UpdateAccidentReportOther(accidentReportOther);
                BLL.LogService.AddSys_Log(this.CurrUser, accidentReportOther.AccidentReportOtherCode, accidentReportOther.AccidentReportOtherId, BLL.Const.ProjectAccidentReportOtherMenuId, Const.BtnModify);
            }
            else
            {
                this.AccidentReportOtherId = SQLHelper.GetNewID(typeof(Model.Accident_AccidentReportOther));
                accidentReportOther.AccidentReportOtherId = this.AccidentReportOtherId;
                BLL.AccidentReportOtherService.AddAccidentReportOther(accidentReportOther);
                BLL.LogService.AddSys_Log(this.CurrUser, accidentReportOther.AccidentReportOtherCode, accidentReportOther.AccidentReportOtherId, BLL.Const.ProjectAccidentReportOtherMenuId, Const.BtnAdd);
            }

            //保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.CurrUser.LoginProjectId, BLL.Const.ProjectAccidentReportOtherMenuId, this.AccidentReportOtherId, (type == BLL.Const.BtnSubmit ? true : false), accidentReportOther.AccidentReportOtherCode, "../Accident/AccidentReportOtherView.aspx?AccidentReportOtherId={0}");
        }
        #endregion

        #region 增加调查组成人员
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (this.drpAccidentTypeId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择事故类型！", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpUnitId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择事故责任单位！", MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(this.AccidentReportOtherId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("AccidentReportOtherItemEdit.aspx?AccidentReportOtherId={0}", this.AccidentReportOtherId, "编辑 - ")));
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 编辑数据方法
        /// </summary>
        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string id = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("AccidentReportOtherItemEdit.aspx?AccidentReportOtherItemId={0}", id, "编辑 - ")));
        }
        #endregion

        #region 删除
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    BLL.AccidentReportOtherItemService.DeleteAccidentReportOtherItemById(rowID);
                }
                BindGrid();
                ShowNotify("删除数据成功!（表格数据已重新绑定）", MessageBoxIcon.Success);
            }
        }
        #endregion

        /// <summary>
        /// 绑定Grid
        /// </summary>
        private void BindGrid()
        {
            accidentReportOtherItems = BLL.AccidentReportOtherItemService.GetAccidentReportOtherItemByAccidentReportOtherId(this.AccidentReportOtherId);
            this.Grid1.DataSource = accidentReportOtherItems;
            this.Grid1.PageIndex = 0;
            this.Grid1.DataBind();
        }

        /// <summary>
        /// 改变索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// 分页下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }

        #region 格式化字符串
        /// <summary>
        /// 获取单位名称
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        protected string ConvertUnit(object unitId)
        {
            string unitName = string.Empty;
            if (unitId != null)
            {
                var unit = BLL.UnitService.GetUnitByUnitId(unitId.ToString());
                if (unit != null)
                {
                    unitName = unit.UnitName;
                }
            }
            return unitName;
        }

        /// <summary>
        /// 获取人员姓名
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        protected string ConvertPerson(object personId)
        {
            string personName = string.Empty;
            if (personId != null)
            {
                var person = BLL.PersonService.GetPersonById(personId.ToString());
                if (person != null)
                {
                    personName = person.PersonName;
                }
            }
            return personName;
        }

        /// <summary>
        /// 获取职务名称
        /// </summary>
        /// <param name="convertPositionId"></param>
        /// <returns></returns>
        protected string ConvertPosition(object positionId)
        {
            string positionName = string.Empty;
            if (positionId != null)
            {
                var position = BLL.PositionService.GetPositionById(positionId.ToString());
                if (position != null)
                {
                    positionName = position.PositionName;
                }
            }
            return positionName;
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
            if (string.IsNullOrEmpty(this.AccidentReportOtherId))
            {
                if (this.drpAccidentTypeId.SelectedValue == BLL.Const._Null)
                {
                    Alert.ShowInTop("请选择事故类型！", MessageBoxIcon.Warning);
                    return;
                }
                if (this.drpUnitId.SelectedValue == BLL.Const._Null)
                {
                    Alert.ShowInTop("请选择事故责任单位！", MessageBoxIcon.Warning);
                    return;
                }
                this.SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/AccidentReportOtherAttachUrl&menuId={1}", this.AccidentReportOtherId, BLL.Const.ProjectAccidentReportOtherMenuId)));
        }
        #endregion
    }
}