using System;
using BLL;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace FineUIPro.Web.HSSE.Accident
{
    public partial class AccidentReportOtherView : PageBase
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
                var thisunit = BLL.UnitService.GetUnitByUnitId(Const.UnitId_SEDIN);
                if (thisunit != null && !string.IsNullOrEmpty(thisunit.UnitCode))
                {
                    string url = "../../Images/SUBimages/" + thisunit.UnitCode + ".jpg";
                    if (url.Contains('*'))
                    {
                        url = url.Replace('*', '-');
                    }
                    this.image.Src = url;
                }

                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.AccidentReportOtherId = Request.Params["AccidentReportOtherId"];
                if (!string.IsNullOrEmpty(this.AccidentReportOtherId))
                {
                    Model.Accident_AccidentReportOther accidentReportOther = BLL.AccidentReportOtherService.GetAccidentReportOtherById(this.AccidentReportOtherId);
                    if (accidentReportOther != null)
                    {
                        Model.Base_Project project=BLL.ProjectService.GetProjectByProjectId(accidentReportOther.ProjectId);
                        if(project!=null)
                        {
                            this.lblProjectName.Text = project.ProjectName;
                            this.lblProjectCode.Text = project.ProjectCode;
                        }
                        this.txtAccidentReportOtherCode.Text = accidentReportOther.AccidentReportOtherCode;
                        if (!string.IsNullOrEmpty(accidentReportOther.AccidentTypeId))
                        {
                            Model.Sys_Const c = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_AccidentInvestigationProcessingReport).FirstOrDefault(x => x.ConstValue == accidentReportOther.AccidentTypeId);
                            if (c != null)
                            {
                                this.txtAccidentTypeName.Text = c.ConstText;
                            }
                        }
                        this.txtAbstract.Text = accidentReportOther.Abstract;
                        if (accidentReportOther.AccidentDate != null)
                        {
                            this.txtAccidentDate.Text = string.Format("{0:yyyy-MM-dd}", accidentReportOther.AccidentDate);
                        }
                        this.txtWorkAreaName.Text = accidentReportOther.WorkArea;
                    }
                    if (accidentReportOther.PeopleNum != null)
                    {
                        this.txtPeopleNum.Text = Convert.ToString(accidentReportOther.PeopleNum);
                    }
                    if (!string.IsNullOrEmpty(accidentReportOther.UnitId))
                    {
                        var unit = BLL.UnitService.GetUnitByUnitId(accidentReportOther.UnitId);
                        if (unit != null)
                        {
                            this.txtUnitName.Text = unit.UnitName;
                        }
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
                        var user = BLL.UserService.GetUserByUserId(accidentReportOther.CompileMan);
                        if (user != null)
                        {
                            this.txtCompileManName.Text = user.UserName;
                        }
                    }
                    if (accidentReportOther.CompileDate != null)
                    {
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", accidentReportOther.CompileDate);
                    }
                }
                BindGrid();
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        private void BindGrid()
        {
            accidentReportOtherItems = BLL.AccidentReportOtherItemService.GetAccidentReportOtherItemByAccidentReportOtherId(this.AccidentReportOtherId);
            this.Grid1.DataSource = accidentReportOtherItems;
            this.Grid1.PageIndex = 0;
            this.Grid1.DataBind();
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
            if (!string.IsNullOrEmpty(this.AccidentReportOtherId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/AccidentReportOtherAttachUrl&menuId={1}&type=-1", this.AccidentReportOtherId, BLL.Const.ProjectAccidentReportOtherMenuId)));
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

            Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("事故调查处理报告" + filename, System.Text.Encoding.UTF8) + ".xls");
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
    }
}