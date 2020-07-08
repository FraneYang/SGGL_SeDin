using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.InformationProject
{
    public partial class MillionsMonthlyReportView : PageBase
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
                this.MillionsMonthlyReportId = Request.Params["MillionsMonthlyReportId"];
                if (!string.IsNullOrEmpty(this.MillionsMonthlyReportId))
                {
                    Model.InformationProject_MillionsMonthlyReport millionsMonthlyReport = BLL.ProjectMillionsMonthlyReportService.GetMillionsMonthlyReportById(this.MillionsMonthlyReportId);
                    if (millionsMonthlyReport != null)
                    {
                        this.ProjectId = millionsMonthlyReport.ProjectId;
                        this.txtProjectName.Text = BLL.ProjectService.GetProjectNameByProjectId(this.ProjectId);
                        if (millionsMonthlyReport.Year != null)
                        {
                            var constValue = BLL.ConstValue.GetConstByConstValueAndGroupId(millionsMonthlyReport.Year.ToString(), BLL.ConstValue.Group_0008);
                            if (constValue!=null)
                            {
                                this.txtYear.Text = constValue.ConstText;
                            }
                        }
                        if (millionsMonthlyReport.Month != null)
                        {
                            var constValue = BLL.ConstValue.GetConstByConstValueAndGroupId(millionsMonthlyReport.Month.ToString(), BLL.ConstValue.Group_0009);
                            if (constValue != null)
                            {
                                this.txtMonth.Text = constValue.ConstText;
                            }
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
                            var user = BLL.UserService.GetUserByUserId(millionsMonthlyReport.CompileMan);
                            if (user!=null)
                            {
                                this.txtCompileManName.Text = user.UserName;
                            }
                        }
                        if (millionsMonthlyReport.CompileDate != null)
                        {
                            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", millionsMonthlyReport.CompileDate);
                        }
                    }
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectMillionsMonthlyReportMenuId;
                this.ctlAuditFlow.DataId = this.MillionsMonthlyReportId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
            }
        }
        #endregion
    }
}