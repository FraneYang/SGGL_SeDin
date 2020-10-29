using System;
using System.Linq;
using System.Web.UI;

namespace FineUIPro.Web.HSSE.Accident
{
    public partial class AccidentReportView : PageBase
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
                var unit = BLL.UnitService.GetUnitByUnitId(BLL.Const.UnitId_SEDIN);
                if (unit != null && !string.IsNullOrEmpty(unit.UnitCode))
                {
                    string url = "../../Images/SUBimages/" + unit.UnitCode + ".jpg";
                    if (url.Contains('*'))
                    {
                        url = url.Replace('*', '-');
                    }
                    this.image.Src = url;
                }

                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
               
                this.AccidentReportId = Request.Params["AccidentReportId"];
                if (!string.IsNullOrEmpty(this.AccidentReportId))
                {
                    Model.Accident_AccidentReport accidentReport = BLL.AccidentReportService.GetAccidentReportById(this.AccidentReportId);
                    if (accidentReport != null)
                    {
                        this.txtAccidentReportCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.AccidentReportId);
                        this.txtUnitName.Text = BLL.UnitService.GetUnitNameByUnitId(accidentReport.UnitId);
                        this.txtCompileManName.Text = BLL.UserService.GetUserNameByUserId(accidentReport.CompileMan);
                        Model.Base_Project project = BLL.ProjectService.GetProjectByProjectId(accidentReport.ProjectId);
                        if (project != null)
                        {
                            this.lblProjectName.Text = project.ProjectName;
                            this.lblProjectCode.Text = project.ProjectCode;
                        }
                        if (accidentReport.CompileDate != null)
                        {
                            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", accidentReport.CompileDate);
                        }
                        if (!string.IsNullOrEmpty(accidentReport.AccidentTypeId))
                        {
                            Model.Sys_Const c = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_AccidentReportRegistration).FirstOrDefault(x => x.ConstValue == accidentReport.AccidentTypeId);
                            if (c != null)
                            {
                                this.txtAccidentTypeName.Text = c.ConstText;
                            }
                            if (accidentReport.IsNotConfirm != null)
                            {
                                this.txtIsNotConfirm.Visible = false;
                                this.txtIsNotConfirm.Text = accidentReport.IsNotConfirm == true ? "是" : "否";
                            }
                        }
                        this.txtAbstract.Text = accidentReport.Abstract;
                        if (accidentReport.AccidentDate != null)
                        {
                            this.txtAccidentDate.Text = string.Format("{0:yyyy-MM-dd}", accidentReport.AccidentDate);
                        }
                        this.txtWorkAreaName.Text = accidentReport.WorkArea;
                        if (accidentReport.PeopleNum != null)
                        {
                            this.txtPeopleNum.Text = Convert.ToString(accidentReport.PeopleNum);
                        }
                        if (accidentReport.IsNotConfirm == true)  //待定
                        {
                            this.txtWorkingHoursLoss.Text = accidentReport.NotConfirmWorkingHoursLoss;
                            this.txtEconomicLoss.Text = accidentReport.NotConfirmEconomicLoss;
                            this.txtEconomicOtherLoss.Text = accidentReport.NotConfirmEconomicOtherLoss;
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
            }
        }
        #endregion

        #region 导出
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            string filename = dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "GB2312";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write("<meta http-equiv=Content-Type content=text/html;charset=UTF-8>");

            Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("事故调查报告" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/ms-excel";
            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            this.Table1.RenderControl(oHtmlTextWriter);
            Response.Write(oStringWriter.ToString());
            Response.Flush();
            Response.End();
        }

        /// <summary>
        /// 重载VerifyRenderingInServerForm方法，否则运行的时候会出现如下错误提示：“类型“GridView”的控件“GridView1”必须放在具有 runat=server 的窗体标记内”
        /// </summary>
        /// <param name="control"></param>
        public override void VerifyRenderingInServerForm(Control control)
        {
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
            if (!string.IsNullOrEmpty(this.AccidentReportId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/AccidentReportAttachUrl&menuId={1}&type=-1", this.AccidentReportId, BLL.Const.ProjectAccidentReportMenuId)));
            }
        }
        #endregion
    }
}