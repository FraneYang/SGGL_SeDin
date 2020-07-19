using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.InformationProject
{
    public partial class MillionsMonthlyReportEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string MillionsMonthlyReportId
        {
            get
            {
                return (string)ViewState["MillionsMonthlyReportId"];
            }
            set
            {
                ViewState["MillionsMonthlyReportId"] = value;
            }
        }
        #endregion

        #region 项目主键
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
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.CurrUser.LoginProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                BLL.ConstValue.InitConstValueDropDownList(this.drpYear, BLL.ConstValue.Group_0008, true);
                BLL.ConstValue.InitConstValueDropDownList(this.drpMonth, BLL.ConstValue.Group_0009, true);

                BLL.UserService.InitUserDropDownList(this.drpCompileMan, this.ProjectId, true);

                this.MillionsMonthlyReportId = Request.Params["MillionsMonthlyReportId"];
                if (!string.IsNullOrEmpty(this.MillionsMonthlyReportId))
                {
                    Model.InformationProject_MillionsMonthlyReport millionsMonthlyReport = BLL.ProjectMillionsMonthlyReportService.GetMillionsMonthlyReportById(this.MillionsMonthlyReportId);
                    if (millionsMonthlyReport != null)
                    {
                        this.ProjectId = millionsMonthlyReport.ProjectId;
                        BLL.UserService.InitUserDropDownList(this.drpCompileMan, this.ProjectId, true);
                        if (millionsMonthlyReport.Year != null)
                        {
                            this.drpYear.SelectedValue = Convert.ToString(millionsMonthlyReport.Year);
                        }
                        if (millionsMonthlyReport.Month != null)
                        {
                            this.drpMonth.SelectedValue = Convert.ToString(millionsMonthlyReport.Month);
                        }
                        //this.txtAffiliation.Text = millionsMonthlyReport.Affiliation;
                        //this.txtName.Text = millionsMonthlyReport.Name;
                        if (millionsMonthlyReport.TotalWorkNum != null)
                        {
                            this.txtTotalWorkNum.Text = Convert.ToString(millionsMonthlyReport.TotalWorkNum);
                        }
                        if (millionsMonthlyReport.PostPersonNum != null)
                        {
                            this.txtPostPersonNum.Text = Convert.ToString(millionsMonthlyReport.PostPersonNum);
                        }
                        if (millionsMonthlyReport.SnapPersonNum != null)
                        {
                            this.txtSnapPersonNum.Text = Convert.ToString(millionsMonthlyReport.SnapPersonNum);
                        }
                        if (millionsMonthlyReport.ContractorNum != null)
                        {
                            this.txtContractorNum.Text = Convert.ToString(millionsMonthlyReport.ContractorNum);
                        }
                        if (millionsMonthlyReport.SeriousInjuriesNum != null)
                        {
                            this.txtSeriousInjuriesNum.Text = Convert.ToString(millionsMonthlyReport.SeriousInjuriesNum);
                        }
                        if (millionsMonthlyReport.SeriousInjuriesPersonNum != null)
                        {
                            this.txtSeriousInjuriesPersonNum.Text = Convert.ToString(millionsMonthlyReport.SeriousInjuriesPersonNum);
                        }
                        if (millionsMonthlyReport.SeriousInjuriesLossHour != null)
                        {
                            this.txtSeriousInjuriesLossHour.Text = Convert.ToString(millionsMonthlyReport.SeriousInjuriesLossHour);
                        }
                        if (millionsMonthlyReport.MinorAccidentNum != null)
                        {
                            this.txtMinorAccidentNum.Text = Convert.ToString(millionsMonthlyReport.MinorAccidentNum);
                        }
                        if (millionsMonthlyReport.MinorAccidentPersonNum != null)
                        {
                            this.txtMinorAccidentPersonNum.Text = Convert.ToString(millionsMonthlyReport.MinorAccidentPersonNum);
                        }
                        if (millionsMonthlyReport.MinorAccidentLossHour != null)
                        {
                            this.txtMinorAccidentLossHour.Text = Convert.ToString(millionsMonthlyReport.MinorAccidentLossHour);
                        }
                        if (millionsMonthlyReport.OtherAccidentNum != null)
                        {
                            this.txtOtherAccidentNum.Text = Convert.ToString(millionsMonthlyReport.OtherAccidentNum);
                        }
                        if (millionsMonthlyReport.OtherAccidentPersonNum != null)
                        {
                            this.txtOtherAccidentPersonNum.Text = Convert.ToString(millionsMonthlyReport.OtherAccidentPersonNum);
                        }
                        if (millionsMonthlyReport.OtherAccidentLossHour != null)
                        {
                            this.txtOtherAccidentLossHour.Text = Convert.ToString(millionsMonthlyReport.OtherAccidentLossHour);
                        }
                        if (millionsMonthlyReport.RestrictedWorkPersonNum != null)
                        {
                            this.txtRestrictedWorkPersonNum.Text = Convert.ToString(millionsMonthlyReport.RestrictedWorkPersonNum);
                        }
                        if (millionsMonthlyReport.RestrictedWorkLossHour != null)
                        {
                            this.txtRestrictedWorkLossHour.Text = Convert.ToString(millionsMonthlyReport.RestrictedWorkLossHour);
                        }
                        if (millionsMonthlyReport.MedicalTreatmentPersonNum != null)
                        {
                            this.txtMedicalTreatmentPersonNum.Text = Convert.ToString(millionsMonthlyReport.MedicalTreatmentPersonNum);
                        }
                        if (millionsMonthlyReport.MedicalTreatmentLossHour != null)
                        {
                            this.txtMedicalTreatmentLossHour.Text = Convert.ToString(millionsMonthlyReport.MedicalTreatmentLossHour);
                        }
                        if (millionsMonthlyReport.FireNum != null)
                        {
                            this.txtFireNum.Text = Convert.ToString(millionsMonthlyReport.FireNum);
                        }
                        if (millionsMonthlyReport.ExplosionNum != null)
                        {
                            this.txtExplosionNum.Text = Convert.ToString(millionsMonthlyReport.ExplosionNum);
                        }
                        if (millionsMonthlyReport.TrafficNum != null)
                        {
                            this.txtTrafficNum.Text = Convert.ToString(millionsMonthlyReport.TrafficNum);
                        }
                        if (millionsMonthlyReport.EquipmentNum != null)
                        {
                            this.txtEquipmentNum.Text = Convert.ToString(millionsMonthlyReport.EquipmentNum);
                        }
                        if (millionsMonthlyReport.QualityNum != null)
                        {
                            this.txtQualityNum.Text = Convert.ToString(millionsMonthlyReport.QualityNum);
                        }
                        if (millionsMonthlyReport.OtherNum != null)
                        {
                            this.txtOtherNum.Text = Convert.ToString(millionsMonthlyReport.OtherNum);
                        }
                        if (millionsMonthlyReport.FirstAidDressingsNum != null)
                        {
                            this.txtFirstAidDressingsNum.Text = Convert.ToString(millionsMonthlyReport.FirstAidDressingsNum);
                        }
                        if (millionsMonthlyReport.AttemptedEventNum != null)
                        {
                            this.txtAttemptedEventNum.Text = Convert.ToString(millionsMonthlyReport.AttemptedEventNum);
                        }
                        if (millionsMonthlyReport.LossDayNum != null)
                        {
                            this.txtLossDayNum.Text = Convert.ToString(millionsMonthlyReport.LossDayNum);
                        }
                        if (!string.IsNullOrEmpty(millionsMonthlyReport.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = millionsMonthlyReport.CompileMan;
                        }
                        if (millionsMonthlyReport.CompileDate != null)
                        {
                            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", millionsMonthlyReport.CompileDate);
                        }
                    }
                }
                else
                {
                    this.drpYear.SelectedValue = DateTime.Now.Year.ToString();
                    this.drpMonth.SelectedValue = DateTime.Now.Month.ToString();
                    DateTime startTime = Convert.ToDateTime(this.drpYear.SelectedValue + "-" + this.drpMonth.SelectedValue + "-01");
                    DateTime endTime = startTime.AddMonths(1);
                    GetData(startTime, endTime);
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtSnapPersonNum.Text = "0";
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectMillionsMonthlyReportMenuId;
                this.ctlAuditFlow.DataId = this.MillionsMonthlyReportId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.txtProjectName.Text = BLL.ProjectService.GetProjectNameByProjectId(this.ProjectId);
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
            if (this.drpYear.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择年度", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpMonth.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择月份", MessageBoxIcon.Warning);
                return;
            }
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
            if (this.drpYear.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择年度", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpMonth.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择月份", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="p"></param>
        private void SaveData(string type)
        {
            Model.InformationProject_MillionsMonthlyReport millionsMonthlyReport = new Model.InformationProject_MillionsMonthlyReport
            {
                ProjectId = this.ProjectId
            };
            if (this.drpYear.SelectedValue != BLL.Const._Null)
            {
                millionsMonthlyReport.Year = Funs.GetNewInt(this.drpYear.SelectedValue);
            }
            if (this.drpMonth.SelectedValue != BLL.Const._Null)
            {
                millionsMonthlyReport.Month = Funs.GetNewInt(this.drpMonth.SelectedValue);
            }
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                millionsMonthlyReport.CompileMan = this.drpCompileMan.SelectedValue;
            }
            millionsMonthlyReport.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text);
            millionsMonthlyReport.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                var flowOperate = Funs.DB.Sys_FlowOperate.FirstOrDefault(x => x.DataId == this.MillionsMonthlyReportId && x.State == BLL.Const.State_2 && x.IsClosed == true);
                if (flowOperate != null)
                {
                    millionsMonthlyReport.States = BLL.Const.State_2;
                }               
                else
                {
                    millionsMonthlyReport.States = this.ctlAuditFlow.NextStep;
                }
            }
            //millionsMonthlyReport.Affiliation = this.txtAffiliation.Text.Trim();
            //millionsMonthlyReport.Name = this.txtName.Text.Trim();
            millionsMonthlyReport.PostPersonNum = Funs.GetNewIntOrZero(this.txtPostPersonNum.Text.Trim());
            millionsMonthlyReport.SnapPersonNum = Funs.GetNewIntOrZero(this.txtSnapPersonNum.Text.Trim());
            millionsMonthlyReport.ContractorNum = Funs.GetNewIntOrZero(this.txtContractorNum.Text.Trim());
            millionsMonthlyReport.SumPersonNum = millionsMonthlyReport.PostPersonNum + millionsMonthlyReport.SnapPersonNum + millionsMonthlyReport.ContractorNum;
            millionsMonthlyReport.TotalWorkNum = Funs.GetNewDecimalOrZero(this.txtTotalWorkNum.Text.Trim());
            millionsMonthlyReport.SeriousInjuriesNum = Funs.GetNewInt(this.txtSeriousInjuriesNum.Text.Trim());
            millionsMonthlyReport.SeriousInjuriesPersonNum = Funs.GetNewInt(this.txtSeriousInjuriesPersonNum.Text.Trim());
            millionsMonthlyReport.SeriousInjuriesLossHour = Funs.GetNewInt(this.txtSeriousInjuriesLossHour.Text.Trim());
            millionsMonthlyReport.MinorAccidentNum = Funs.GetNewInt(this.txtMinorAccidentNum.Text.Trim());
            millionsMonthlyReport.MinorAccidentPersonNum = Funs.GetNewInt(this.txtMinorAccidentPersonNum.Text.Trim());
            millionsMonthlyReport.MinorAccidentLossHour = Funs.GetNewInt(this.txtMinorAccidentLossHour.Text.Trim());
            millionsMonthlyReport.OtherAccidentNum = Funs.GetNewInt(this.txtOtherAccidentNum.Text.Trim());
            millionsMonthlyReport.OtherAccidentPersonNum = Funs.GetNewInt(this.txtOtherAccidentPersonNum.Text.Trim());
            millionsMonthlyReport.OtherAccidentLossHour = Funs.GetNewInt(this.txtOtherAccidentLossHour.Text.Trim());
            millionsMonthlyReport.RestrictedWorkPersonNum = Funs.GetNewInt(this.txtRestrictedWorkPersonNum.Text.Trim());
            millionsMonthlyReport.RestrictedWorkLossHour = Funs.GetNewInt(this.txtRestrictedWorkLossHour.Text.Trim());
            millionsMonthlyReport.MedicalTreatmentPersonNum = Funs.GetNewInt(this.txtMedicalTreatmentPersonNum.Text.Trim());
            millionsMonthlyReport.MedicalTreatmentLossHour = Funs.GetNewInt(this.txtMedicalTreatmentLossHour.Text.Trim());
            millionsMonthlyReport.FireNum = Funs.GetNewInt(this.txtFireNum.Text.Trim());
            millionsMonthlyReport.ExplosionNum = Funs.GetNewInt(this.txtExplosionNum.Text.Trim());
            millionsMonthlyReport.TrafficNum = Funs.GetNewInt(this.txtTrafficNum.Text.Trim());
            millionsMonthlyReport.EquipmentNum = Funs.GetNewInt(this.txtEquipmentNum.Text.Trim());
            millionsMonthlyReport.QualityNum = Funs.GetNewInt(this.txtQualityNum.Text.Trim());
            millionsMonthlyReport.OtherNum = Funs.GetNewInt(this.txtOtherNum.Text.Trim());
            millionsMonthlyReport.FirstAidDressingsNum = Funs.GetNewInt(this.txtFirstAidDressingsNum.Text.Trim());
            millionsMonthlyReport.AttemptedEventNum = Funs.GetNewInt(this.txtAttemptedEventNum.Text.Trim());
            millionsMonthlyReport.LossDayNum = Funs.GetNewInt(this.txtLossDayNum.Text.Trim());
            if (!string.IsNullOrEmpty(this.MillionsMonthlyReportId))
            {
                millionsMonthlyReport.MillionsMonthlyReportId = this.MillionsMonthlyReportId;
                BLL.ProjectMillionsMonthlyReportService.UpdateMillionsMonthlyReport(millionsMonthlyReport);
                BLL.LogService.AddSys_Log(this.CurrUser, millionsMonthlyReport.Year.ToString() + "-" + millionsMonthlyReport.Month.ToString(), millionsMonthlyReport.MillionsMonthlyReportId, BLL.Const.ProjectMillionsMonthlyReportMenuId, BLL.Const.BtnModify);
                
            }
            else
            {
                Model.InformationProject_MillionsMonthlyReport oldMillionsMonthlyReport = (from x in Funs.DB.InformationProject_MillionsMonthlyReport
                                                                                           where x.ProjectId == millionsMonthlyReport.ProjectId && x.Year == millionsMonthlyReport.Year && x.Month == millionsMonthlyReport.Month
                                                                                           select x).FirstOrDefault();
                if (oldMillionsMonthlyReport == null)
                {
                    this.MillionsMonthlyReportId = SQLHelper.GetNewID(typeof(Model.InformationProject_MillionsMonthlyReport));
                    millionsMonthlyReport.MillionsMonthlyReportId = this.MillionsMonthlyReportId;
                    BLL.ProjectMillionsMonthlyReportService.AddMillionsMonthlyReport(millionsMonthlyReport);
                    BLL.LogService.AddSys_Log(this.CurrUser, millionsMonthlyReport.Year.ToString() + "-" + millionsMonthlyReport.Month.ToString(), millionsMonthlyReport.MillionsMonthlyReportId, BLL.Const.ProjectMillionsMonthlyReportMenuId, BLL.Const.BtnAdd);
                    ////删除未上报月报信息
                    //Model.ManagementReport_ReportRemind reportRemind = (from x in Funs.DB.ManagementReport_ReportRemind
                    //                                                    where x.ProjectId == this.ProjectId && x.Year == millionsMonthlyReport.Year && x.Month == millionsMonthlyReport.Month && x.ReportName == "百万工时安全统计月报"
                    //                                                    select x).FirstOrDefault();
                    //if (reportRemind != null)
                    //{
                    //    BLL.ReportRemindService.DeleteReportRemindByReportRemind(reportRemind);
                    //}
                }
                else
                {
                    Alert.ShowInTop("该月份记录已存在", MessageBoxIcon.Warning);
                    return;
                }
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectMillionsMonthlyReportMenuId, this.MillionsMonthlyReportId, (type == BLL.Const.BtnSubmit ? true : false), millionsMonthlyReport.Year + "-" + millionsMonthlyReport.Month, "../InformationProject/MillionsMonthlyReportView.aspx?MillionsMonthlyReportId={0}");
        }
        #endregion

        #region 年月变化事件
        /// <summary>
        /// 年月变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpYear.SelectedValue != BLL.Const._Null && this.drpMonth.SelectedValue != BLL.Const._Null)
            {
                DateTime startTime = Convert.ToDateTime(this.drpYear.SelectedValue + "-" + this.drpMonth.SelectedValue + "-01");
                DateTime endTime = startTime.AddMonths(1);
                GetData(startTime, endTime);
            }
            else
            {

            }
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        private void GetData(DateTime startTime, DateTime endTime)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                int? sumTotalPanhours = 0;

                //获取当期人工时日报
                List<Model.SitePerson_DayReport> dayReports = BLL.SitePerson_DayReportService.GetDayReportsByCompileDate(startTime, endTime, this.ProjectId);
                if (dayReports.Count > 0)
                {
                    foreach (var dayReport in dayReports)
                    {
                        sumTotalPanhours += Convert.ToInt32((from y in db.SitePerson_DayReportDetail where y.DayReportId == dayReport.DayReportId select y.PersonWorkTime ?? 0).Sum());
                    }

                    //总工时数（万）
                    this.txtTotalWorkNum.Text = decimal.Round(decimal.Round(Convert.ToDecimal(sumTotalPanhours), 4) / 10000, 4).ToString();
                    //在岗员工
                    //获取单位集合
                    var unitIds = (from x in dayReports
                                   join y in db.SitePerson_DayReportDetail
                                   on x.DayReportId equals y.DayReportId
                                   select y.UnitId).Distinct();
                    int subUnitsPersonNum = 0;
                    foreach (var unitId in unitIds)
                    {
                        Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(unitId);
                        if (unit != null)
                        {
                            int count = BLL.SitePerson_DayReportService.GetDayReportsByCompileDateAndUnitId(startTime, endTime, this.CurrUser.LoginProjectId, unitId).Count();
                            if (unit.UnitId == Const.UnitId_SEDIN)    //本单位
                            {
                                //本单位在岗员工
                                decimal personNum = (from x in dayReports
                                                     join y in db.SitePerson_DayReportDetail
                                                  on x.DayReportId equals y.DayReportId
                                                     where y.UnitId == unitId
                                                     select y.RealPersonNum ?? 0).Sum();
                                if (count > 0)
                                {
                                    decimal persontotal = Convert.ToDecimal(Math.Round(personNum / count, 2));
                                    if (persontotal.ToString().IndexOf(".") > 0 && persontotal.ToString().Substring(persontotal.ToString().IndexOf("."), persontotal.ToString().Length - persontotal.ToString().IndexOf(".")) != ".00")
                                    {
                                        string personint = persontotal.ToString().Substring(0, persontotal.ToString().IndexOf("."));
                                        this.txtPostPersonNum.Text = (Convert.ToInt32(personint) + 1).ToString();
                                    }
                                    else
                                    {
                                        this.txtPostPersonNum.Text = Convert.ToInt32(persontotal).ToString();
                                    }
                                }
                            }
                            else if (unit.UnitName.Contains("临时员工"))  //临时员工
                            {
                                //本单位临时员工
                                decimal personNum = (from x in dayReports
                                                     join y in db.SitePerson_DayReportDetail
                                                  on x.DayReportId equals y.DayReportId
                                                     where y.UnitId == unitId
                                                     select y.RealPersonNum ?? 0).Sum();
                                if (count > 0)
                                {
                                    decimal persontotal = Convert.ToDecimal(Math.Round(personNum / count, 2));
                                    if (persontotal.ToString().IndexOf(".") > 0 && persontotal.ToString().Substring(persontotal.ToString().IndexOf("."), persontotal.ToString().Length - persontotal.ToString().IndexOf(".")) != ".00")
                                    {
                                        string personint = persontotal.ToString().Substring(0, persontotal.ToString().IndexOf("."));
                                        this.txtSnapPersonNum.Text = (Convert.ToInt32(personint) + 1).ToString();
                                    }
                                    else
                                    {
                                        this.txtSnapPersonNum.Text = Convert.ToInt32(persontotal).ToString();
                                    }
                                }
                            }
                            else    //承包商
                            {
                                decimal personNum = (from x in dayReports
                                                     join y in db.SitePerson_DayReportDetail
                                                  on x.DayReportId equals y.DayReportId
                                                     where y.UnitId == unitId
                                                     select y.RealPersonNum ?? 0).Sum();
                                if (count > 0)
                                {
                                    decimal persontotal = Convert.ToDecimal(Math.Round(personNum / count, 2));
                                    if (persontotal.ToString().IndexOf(".") > 0 && persontotal.ToString().Substring(persontotal.ToString().IndexOf("."), persontotal.ToString().Length - persontotal.ToString().IndexOf(".")) != ".00")
                                    {
                                        string personint = persontotal.ToString().Substring(0, persontotal.ToString().IndexOf("."));
                                        subUnitsPersonNum += Convert.ToInt32(personint) + 1;
                                    }
                                    else
                                    {
                                        subUnitsPersonNum += Convert.ToInt32(persontotal);
                                    }
                                }
                            }
                        }
                    }

                    //承包商员工
                    this.txtContractorNum.Text = subUnitsPersonNum.ToString();
                }
                else
                {
                    //获取当期人工时日报
                    var monthReport = BLL.SitePerson_MonthReportService.GetMonthReportsByCompileDate(startTime, this.ProjectId);
                    if (monthReport != null)
                    {
                        decimal? sumCount = db.SitePerson_MonthReportDetail.Where(x => x.MonthReportId == monthReport.MonthReportId).Sum(x => x.PersonWorkTime);
                        if (sumCount.HasValue)
                        {
                            sumTotalPanhours += Convert.ToInt32(sumCount.Value);
                        }
                        //总工时数（万）
                        this.txtTotalWorkNum.Text = decimal.Round(decimal.Round(Convert.ToDecimal(sumTotalPanhours), 4) / 10000, 4).ToString();
                        //在岗员工
                        //获取单位集合
                        var unitIds = (from x in db.SitePerson_MonthReportDetail
                                       where x.MonthReportId == monthReport.MonthReportId
                                       select x.UnitId).Distinct();
                        int subUnitsPersonNum = 0;
                        foreach (var unitId in unitIds)
                        {
                            Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(unitId);
                            if (unit != null)
                            {
                                if (unit.UnitId == Const.UnitId_SEDIN)    //本单位
                                {
                                    //本单位在岗员工
                                    decimal? personNum = db.SitePerson_MonthReportDetail.Where(x => x.MonthReportId == monthReport.MonthReportId && x.UnitId == unitId).Sum(x => x.RealPersonNum);
                                    if (personNum.HasValue)
                                    {
                                        decimal persontotal = Convert.ToDecimal(Math.Round(personNum.Value, 2));
                                        if (persontotal.ToString().IndexOf(".") > 0 && persontotal.ToString().Substring(persontotal.ToString().IndexOf("."), persontotal.ToString().Length - persontotal.ToString().IndexOf(".")) != ".00")
                                        {
                                            string personint = persontotal.ToString().Substring(0, persontotal.ToString().IndexOf("."));
                                            this.txtPostPersonNum.Text = (Convert.ToInt32(personint) + 1).ToString();
                                        }
                                        else
                                        {
                                            this.txtPostPersonNum.Text = Convert.ToInt32(persontotal).ToString();
                                        }
                                    }
                                }
                                else if (unit.UnitName.Contains("临时员工"))  //临时员工
                                {
                                    //本单位在岗员工
                                    decimal? personNum = db.SitePerson_MonthReportDetail.Where(x => x.MonthReportId == monthReport.MonthReportId && x.UnitId == unitId).Sum(x => x.RealPersonNum);
                                    if (personNum.HasValue)
                                    {
                                        decimal persontotal = Convert.ToDecimal(Math.Round(personNum.Value, 2));
                                        if (persontotal.ToString().IndexOf(".") > 0 && persontotal.ToString().Substring(persontotal.ToString().IndexOf("."), persontotal.ToString().Length - persontotal.ToString().IndexOf(".")) != ".00")
                                        {
                                            string personint = persontotal.ToString().Substring(0, persontotal.ToString().IndexOf("."));
                                            this.txtSnapPersonNum.Text = (Convert.ToInt32(personint) + 1).ToString();
                                        }
                                        else
                                        {
                                            this.txtSnapPersonNum.Text = Convert.ToInt32(persontotal).ToString();
                                        }
                                    }
                                }
                                else    //承包商
                                {
                                    decimal? personNum = db.SitePerson_MonthReportDetail.Where(x => x.MonthReportId == monthReport.MonthReportId && x.UnitId == unitId).Sum(x => x.RealPersonNum);
                                    if (personNum.HasValue)
                                    {
                                        decimal persontotal = Convert.ToDecimal(Math.Round(personNum.Value, 2));
                                        if (persontotal.ToString().IndexOf(".") > 0 && persontotal.ToString().Substring(persontotal.ToString().IndexOf("."), persontotal.ToString().Length - persontotal.ToString().IndexOf(".")) != ".00")
                                        {
                                            string personint = persontotal.ToString().Substring(0, persontotal.ToString().IndexOf("."));
                                            subUnitsPersonNum += Convert.ToInt32(personint) + 1;
                                        }
                                        else
                                        {
                                            subUnitsPersonNum += Convert.ToInt32(persontotal);
                                        }
                                    }
                                }
                            }
                        }
                        //承包商员工
                        this.txtContractorNum.Text = subUnitsPersonNum.ToString();
                    }
                }

                List<Model.Accident_AccidentReport> accidentReports1 = BLL.AccidentReportService.GetAccidentReportsByTimeAndAccidentTypeId(startTime, endTime, this.ProjectId, "2");
                this.txtSeriousInjuriesNum.Text = accidentReports1.Count().ToString();
                this.txtSeriousInjuriesPersonNum.Text = accidentReports1.Sum(x => x.PeopleNum ?? 0).ToString();
                this.txtSeriousInjuriesLossHour.Text = accidentReports1.Sum(x => x.WorkingHoursLoss ?? 0).ToString();
                List<Model.Accident_AccidentReport> accidentReports2 = BLL.AccidentReportService.GetAccidentReportsByTimeAndAccidentTypeId(startTime, endTime, this.ProjectId, "3");
                this.txtMinorAccidentNum.Text = accidentReports2.Count().ToString();
                this.txtMinorAccidentPersonNum.Text = accidentReports2.Sum(x => x.PeopleNum ?? 0).ToString();
                this.txtMinorAccidentLossHour.Text = accidentReports2.Sum(x => x.WorkingHoursLoss ?? 0).ToString();
                List<Model.Accident_AccidentReport> accidentReports3 = BLL.AccidentReportService.GetAccidentReportsByTimeAndAccidentTypeId(startTime, endTime, this.ProjectId, "1");
                this.txtOtherAccidentNum.Text = accidentReports3.Count().ToString();
                this.txtOtherAccidentPersonNum.Text = accidentReports3.Sum(x => x.PeopleNum ?? 0).ToString();
                this.txtOtherAccidentLossHour.Text = accidentReports3.Sum(x => x.WorkingHoursLoss ?? 0).ToString();
                List<Model.Accident_AccidentReportOther> accidentReports4 = BLL.AccidentReportOtherService.GetAccidentReportOthersByTimeAndAccidentTypeId(startTime, endTime, this.ProjectId, "1");
                this.txtRestrictedWorkPersonNum.Text = accidentReports4.Sum(x => x.PeopleNum ?? 0).ToString();
                this.txtRestrictedWorkLossHour.Text = accidentReports4.Sum(x => x.WorkingHoursLoss ?? 0).ToString();
                List<Model.Accident_AccidentReportOther> accidentReports5 = BLL.AccidentReportOtherService.GetAccidentReportOthersByTimeAndAccidentTypeId(startTime, endTime, this.ProjectId, "2");
                this.txtMedicalTreatmentPersonNum.Text = accidentReports5.Sum(x => x.PeopleNum ?? 0).ToString();
                this.txtMedicalTreatmentLossHour.Text = accidentReports5.Sum(x => x.WorkingHoursLoss ?? 0).ToString();
                List<Model.Accident_AccidentReport> accidentReports6 = BLL.AccidentReportService.GetAccidentReportsByTimeAndAccidentTypeId(startTime, endTime, this.ProjectId, "4");
                this.txtFireNum.Text = accidentReports6.Count().ToString();
                List<Model.Accident_AccidentReport> accidentReports7 = BLL.AccidentReportService.GetAccidentReportsByTimeAndAccidentTypeId(startTime, endTime, this.ProjectId, "5");
                this.txtExplosionNum.Text = accidentReports7.Count().ToString();
                List<Model.Accident_AccidentReport> accidentReports8 = BLL.AccidentReportService.GetAccidentReportsByTimeAndAccidentTypeId(startTime, endTime, this.ProjectId, "6");
                this.txtTrafficNum.Text = accidentReports8.Count().ToString();
                List<Model.Accident_AccidentReport> accidentReports9 = BLL.AccidentReportService.GetAccidentReportsByTimeAndAccidentTypeId(startTime, endTime, this.ProjectId, "7");
                this.txtEquipmentNum.Text = accidentReports9.Count().ToString();
                List<Model.Accident_AccidentReport> accidentReports10 = BLL.AccidentReportService.GetAccidentReportsByTimeAndAccidentTypeId(startTime, endTime, this.ProjectId, "10");
                this.txtQualityNum.Text = accidentReports10.Count().ToString();
                List<Model.Accident_AccidentReport> accidentReports11 = BLL.AccidentReportService.GetAccidentReportsByTimeAndAccidentTypeId(startTime, endTime, this.ProjectId, "11");
                this.txtOtherNum.Text = accidentReports11.Count().ToString();
                List<Model.Accident_AccidentReportOther> accidentReports12 = BLL.AccidentReportOtherService.GetAccidentReportOthersByTimeAndAccidentTypeId(startTime, endTime, this.ProjectId, "3");
                this.txtFirstAidDressingsNum.Text = accidentReports12.Count().ToString();
                List<Model.Accident_AccidentReportOther> accidentReports13 = BLL.AccidentReportOtherService.GetAccidentReportOthersByTimeAndAccidentTypeId(startTime, endTime, this.ProjectId, "4");
                this.txtAttemptedEventNum.Text = accidentReports13.Count().ToString();
                decimal totalWorkingHoursLoss = 0;
                totalWorkingHoursLoss = accidentReports1.Sum(x => x.WorkingHoursLoss ?? 0) + accidentReports2.Sum(x => x.WorkingHoursLoss ?? 0) + accidentReports3.Sum(x => x.WorkingHoursLoss ?? 0);
                this.txtLossDayNum.Text = decimal.Round(totalWorkingHoursLoss / 8, 2).ToString().Split('.')[0];
            }
        }
        #endregion
    }
}