using BLL;
using System;
using System.Web;

namespace FineUIPro.Web.HSSE.Accident
{
    public partial class NoFourLetoffEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// HSE事故(含未遂)处理Id
        /// </summary>
        private string AccidentHandleId
        {
            get
            {
                return (string)ViewState["AccidentHandleId"];
            }
            set
            {
                ViewState["AccidentHandleId"] = value;
            }
        }

        /// <summary>
        /// 四不放过主键
        /// </summary>
        private string NoFourLetoffId
        {
            get
            {
                return (string)ViewState["NoFourLetoffId"];
            }
            set
            {
                ViewState["NoFourLetoffId"] = value;
            }
        }
        #endregion

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
                BLL.UnitService.InitUnitDropDownList(this.drpRegistUnitId, this.CurrUser.LoginProjectId, true);
                BLL.UserService.InitUserDropDownList(this.drpHeadManId, this.CurrUser.LoginProjectId, true);
                this.AccidentHandleId = Request.Params["AccidentHandleId"];
                if (!string.IsNullOrEmpty(this.AccidentHandleId))
                {
                    var accidentHandle = BLL.AccidentHandleService.GetAccidentHandleById(AccidentHandleId);
                    if (accidentHandle != null)
                    {
                        this.txtAccidentDate.Text = string.Format("{0:yyyy-MM-dd}", accidentHandle.AccidentDate);
                        this.txtAccidentHandleCode.Text = accidentHandle.AccidentHandleCode;
                        this.txtUnitName.Text = BLL.UnitService.GetUnitNameByUnitId(accidentHandle.UnitId);

                        Model.Accident_NoFourLetoff noFourLetoff = BLL.NoFourLetoffService.GetNoFourLetoffByAccidentHandleId(this.AccidentHandleId);
                        if (noFourLetoff != null)
                        {
                            this.NoFourLetoffId = noFourLetoff.NoFourLetoffId;
                           
                            if (noFourLetoff.AccidentDate != null)
                            {
                                this.txtAccidentDate.Text = string.Format("{0:yyyy-MM-dd}", noFourLetoff.AccidentDate);
                            }
                            this.txtFileContents.Text = HttpUtility.HtmlDecode(noFourLetoff.FileContents);
                            if (!string.IsNullOrEmpty(noFourLetoff.RegistUnitId))
                            {
                                this.drpRegistUnitId.SelectedValue = noFourLetoff.RegistUnitId;
                            }
                            if (!string.IsNullOrEmpty(noFourLetoff.HeadMan))
                            {
                                this.drpHeadManId.SelectedValue = noFourLetoff.HeadMan;
                            }
                            if (noFourLetoff.RegistDate != null)
                            {
                                this.txtRegistDate.Text = string.Format("{0:yyyy-MM-dd}", noFourLetoff.RegistDate);
                            }
                        }

                        else
                        {                          
                            this.txtRegistDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                            this.drpHeadManId.SelectedValue = this.CurrUser.UserId;
                            this.drpRegistUnitId.SelectedValue = this.CurrUser.UnitId;
                            this.txtAccidentHandleCode.Text = accidentHandle.AccidentHandleCode;
                            //var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectAccidentHandleMenuId, this.CurrUser.LoginProjectId);
                            //if (codeTemplateRule != null)
                            //{
                            //    this.txtFileContents.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                            //}
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData(true);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="p"></param>
        private void SaveData(bool isClose)
        {
            Model.Accident_NoFourLetoff noFourLetOff = new Model.Accident_NoFourLetoff
            {
                ProjectId = this.CurrUser.LoginProjectId,
                AccidentHandleId = this.AccidentHandleId,
                AccidentDate = Funs.GetNewDateTime(this.txtAccidentDate.Text.Trim()),
                FileContents = HttpUtility.HtmlEncode(this.txtFileContents.Text),
                NoFourLetoffCode=this.txtAccidentHandleCode.Text ?? "", 
            };
            if (this.drpRegistUnitId.SelectedValue != BLL.Const._Null)
            {
                noFourLetOff.RegistUnitId = this.drpRegistUnitId.SelectedValue;
            }
            if (this.drpHeadManId.SelectedValue != BLL.Const._Null)
            {
                noFourLetOff.HeadMan = this.drpHeadManId.SelectedValue;
            }
            noFourLetOff.RegistDate = Funs.GetNewDateTime(this.txtRegistDate.Text.Trim());
            if (!string.IsNullOrEmpty(this.NoFourLetoffId))
            {
                noFourLetOff.NoFourLetoffId = this.NoFourLetoffId;
                BLL.NoFourLetoffService.UpdateNoFourLetoff(noFourLetOff);                
                BLL.LogService.AddSys_Log(this.CurrUser, this.txtAccidentHandleCode.Text, this.AccidentHandleId, BLL.Const.ProjectAccidentHandleMenuId, Const.BtnModify);
            }
            else
            {
                this.NoFourLetoffId = SQLHelper.GetNewID(typeof(Model.Accident_NoFourLetoff));
                noFourLetOff.NoFourLetoffId = this.NoFourLetoffId;
                BLL.NoFourLetoffService.AddNoFourLetoff(noFourLetOff);                
                BLL.LogService.AddSys_Log(this.CurrUser, this.txtAccidentHandleCode.Text, this.AccidentHandleId, BLL.Const.ProjectAccidentHandleMenuId, Const.BtnAdd);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.NoFourLetoffId))
            {
                SaveData(false);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/NoFourLetoffAttachUrl&menuId={1}", NoFourLetoffId, BLL.Const.ProjectAccidentHandleMenuId)));
        }

        #endregion
    }
}