using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace FineUIPro.Web.PZHGL.InformationProject
{
    public partial class ConstructionLogEdit : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string ConstructionLogId
        {
            get
            {
                return (string)ViewState["ConstructionLogId"];
            }
            set
            {
                ViewState["ConstructionLogId"] = value;
            }
        }
        public int ContactImg
        {
            get
            {
                return Convert.ToInt32(ViewState["ContactImg"]);
            }
            set
            {
                ViewState["ContactImg"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ConstructionLogId = Request.Params["ConstructionLogId"];
                this.drpWeather.DataValueField = "Value";
                this.drpWeather.DataTextField = "Text";
                this.drpWeather.DataSource = ConstructionLogService.GetWeatherList();
                this.drpWeather.DataBind();
                Funs.FineUIPleaseSelect(this.drpWeather);
                ContactImg = 0;
                if (!string.IsNullOrEmpty(ConstructionLogId))
                {
                    HFConstructionLogId.Text = ConstructionLogId;
                    Model.ZHGL_ConstructionLog constructionLog = ConstructionLogService.GetConstructionLogById(ConstructionLogId);
                    string unitType = string.Empty;
                    if (!string.IsNullOrEmpty(constructionLog.Weather))
                    {
                        this.drpWeather.SelectedValue = constructionLog.Weather;
                    }
                    if (constructionLog.TemperatureMin != null)
                    {
                        this.txtTemperatureMin.Text = constructionLog.TemperatureMin.ToString();
                    }
                    if (constructionLog.TemperatureMax != null)
                    {
                        this.txtTemperatureMax.Text = constructionLog.TemperatureMax.ToString();
                    }
                    if (constructionLog.CompileDate != null)
                    {
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", constructionLog.CompileDate);
                    }
                    this.txtMainWork.Text = constructionLog.MainWork;
                    this.txtMainProblems.Text = constructionLog.MainProblems;
                    this.txtRemark.Text = constructionLog.Remark;

                }
                else
                {
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }
                GetMainWork();
            }
        }

        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            GetMainWork();
        }

        private void GetMainWork()
        {
            if (!string.IsNullOrEmpty(this.txtCompileDate.Text.Trim()))
            {
                DateTime startTime = Convert.ToDateTime(this.txtCompileDate.Text.Trim());
                DateTime endTime = startTime.AddDays(1);
                //质量控制点
                var spotCheckDetails = (from x in Funs.DB.View_Check_SoptCheckDetail
                                        where x.ProjectId == this.CurrUser.LoginProjectId && !x.ControlPoint.Contains("C") && x.SpotCheckDate >= startTime
                                        && x.SpotCheckDate < endTime && x.IsOK == true && x.JointCheckMans.Contains(this.CurrUser.UserId)
                                        select x).ToList();
                //质量巡检
                var checkControls = (from x in Funs.DB.Check_CheckControl
                                     where x.ProjectId == this.CurrUser.LoginProjectId && x.CheckDate >= startTime
                                     && x.CheckDate < endTime && x.IsOK == true && x.CheckMan == this.CurrUser.UserId
                                     select x).ToList();
                //工程联络单
                var technicalContactLists = (from x in Funs.DB.Check_TechnicalContactList
                                             where x.ProjectId == this.CurrUser.LoginProjectId && x.CompileDate >= startTime
                                             && x.CompileDate < endTime && x.CompileMan == this.CurrUser.UserId
                                             select x).ToList();
                //工作联系单
                var workContacts = (from x in Funs.DB.Unqualified_WorkContact
                                    where x.ProjectId == this.CurrUser.LoginProjectId && x.CompileDate >= startTime
                                    && x.CompileDate < endTime && x.CompileMan == this.CurrUser.UserId
                                    select x).ToList();
                //安全巡检
                var hazardRegisters = (from x in Funs.DB.HSSE_Hazard_HazardRegister
                                       where x.ProjectId == this.CurrUser.LoginProjectId && x.CheckTime >= startTime
                                       && x.CheckTime < endTime && x.CheckManId == this.CurrUser.UserId
                                       select x).ToList();
                //隐患整改单
                var rectifyNotices = (from x in Funs.DB.Check_RectifyNotices
                                      where x.ProjectId == this.CurrUser.LoginProjectId && x.CheckedDate >= startTime
                                      && x.CheckedDate < endTime && x.CheckManIds.Contains(this.CurrUser.UserId)
                                      select x).ToList();
                this.txtMainWork.Text = "质量控制点验收数：" + spotCheckDetails.Count + "，质量巡检数：" + checkControls.Count + "，发起的工程联络单数：" + technicalContactLists.Count + "，发起的工作联系单数：" + workContacts.Count + "。安全巡检数：" + hazardRegisters.Count + "，隐患整改单数：" + rectifyNotices.Count + "。";
            }
            else
            {
                this.txtCompileDate.Text = string.Empty;
            }
        }

        /// <summary>
        /// 附件内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgBtnFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HFConstructionLogId.Text))   //新增记录
            {
                HFConstructionLogId.Text = SQLHelper.GetNewID(typeof(Model.ZHGL_ConstructionLog));
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(
            String.Format("../../AttachFile/webuploader.aspx?type={0}&toKeyId={1}&path=FileUpload/ConstructionLog&menuId={2}",
            ContactImg, HFConstructionLogId.Text, Const.ConstructionLogMenuId)));
        }

        #region  保存
        /// <summary>
        /// 保存开工报告
        /// </summary>
        private void SavePauseNotice(string saveType)
        {
            Model.ZHGL_ConstructionLog constructionLog = new Model.ZHGL_ConstructionLog();
            constructionLog.Weather = drpWeather.SelectedValue;
            constructionLog.ProjectId = CurrUser.LoginProjectId;
            constructionLog.TemperatureMin = Funs.GetNewInt(this.txtTemperatureMin.Text.Trim());
            constructionLog.TemperatureMax = Funs.GetNewInt(this.txtTemperatureMax.Text.Trim());
            constructionLog.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
            constructionLog.MainWork = this.txtMainWork.Text;
            constructionLog.MainProblems = this.txtMainProblems.Text;
            constructionLog.Remark = this.txtRemark.Text;
            if (!string.IsNullOrEmpty(ConstructionLogId) && ConstructionLogService.GetConstructionLogById(Request.Params["ConstructionLogId"]) != null)
            {
                constructionLog.ConstructionLogId = ConstructionLogId;
                ConstructionLogService.UpdateConstructionLog(constructionLog);
            }
            else
            {
                if (!string.IsNullOrEmpty(HFConstructionLogId.Text))
                {
                    constructionLog.ConstructionLogId = HFConstructionLogId.Text;
                }
                else
                {
                    constructionLog.ConstructionLogId = SQLHelper.GetNewID(typeof(Model.ZHGL_ConstructionLog));
                }
                constructionLog.CompileMan = CurrUser.UserId;
                ConstructionLogService.AddConstructionLog(constructionLog);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            LogService.AddSys_Log(CurrUser, string.Format("{0:yyyy-MM-dd}", constructionLog.CompileDate), ConstructionLogId, Const.ConstructionLogMenuId, "项目级施工日志");
        }
        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, CurrUser.UserId, Const.ConstructionLogMenuId, Const.BtnSave))
            {
                if (this.drpWeather.SelectedValue == BLL.Const._Null)
                {
                    Alert.ShowInTop("请选择天气状况！", MessageBoxIcon.Warning);
                    return;
                }
                Model.ZHGL_ConstructionLog log = BLL.ConstructionLogService.GetConstructionLogByProjectIdAndUserIDAndDate(this.ConstructionLogId ?? "", this.CurrUser.LoginProjectId, this.CurrUser.UserId, Convert.ToDateTime(this.txtCompileDate.Text.Trim()));
                if (log != null)
                {
                    Alert.ShowInTop("当前日期施工日志已存在！", MessageBoxIcon.Warning);
                    return;
                }
                SavePauseNotice("save");
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }
    }
}