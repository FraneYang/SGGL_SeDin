using System;
using System.Linq;
using BLL;

namespace FineUIPro.Web.HSSE.QualityAudit
{
    public partial class SafePersonQualityEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 人员主键
        /// </summary>
        private string PersonId
        {
            get
            {
                return (string)ViewState["PersonId"];
            }
            set
            {
                ViewState["PersonId"] = value;
            }
        }

        /// <summary>
        /// 资质主键
        /// </summary>
        private string SafePersonQualityId
        {
            get
            {
                return (string)ViewState["SafePersonQualityId"];
            }
            set
            {
                ViewState["SafePersonQualityId"] = value;
            }
        }
        /// <summary>
        /// 项目主键
        /// </summary>
        private string ProjectId
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
                this.PersonId = Request.Params["PersonId"];
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(this.PersonId))
                {
                    var person = BLL.PersonService.GetPersonById(this.PersonId);
                    if (person != null)
                    {
                        this.ProjectId = person.ProjectId;
                        var unit = BLL.UnitService.GetUnitByUnitId(person.UnitId);
                        if (unit != null)
                        {
                            this.txtUnitCode.Text = unit.UnitCode;
                            this.txtUnitName.Text = unit.UnitName;
                        }
                        this.txtPersonName.Text = person.PersonName;
                        var workPost = BLL.WorkPostService.GetWorkPostById(person.WorkPostId);
                        if (workPost != null)
                        {
                            this.txtWorkPostName.Text = workPost.WorkPostName;
                        }
                    }

                    var SafePersonQuality = BLL.SafePersonQualityService.GetSafePersonQualityByPersonId(this.PersonId);
                    if (SafePersonQuality != null)
                    {
                        UserService.InitUserProjectIdUnitIdDropDownList(this.drpAuditor, this.ProjectId, Const.UnitId_SEDIN, true);
                        this.SafePersonQualityId = SafePersonQuality.SafePersonQualityId;
                        this.txtCertificateNo.Text = SafePersonQuality.CertificateNo;
                        this.txtCertificateName.Text = SafePersonQuality.CertificateName;
                        this.txtGrade.Text = SafePersonQuality.Grade;
                        this.txtSendUnit.Text = SafePersonQuality.SendUnit;
                        this.txtSendDate.Text = string.Format("{0:yyyy-MM-dd}", SafePersonQuality.SendDate);
                        this.txtLimitDate.Text = string.Format("{0:yyyy-MM-dd}", SafePersonQuality.LimitDate);
                        this.txtLateCheckDate.Text = string.Format("{0:yyyy-MM-dd}", SafePersonQuality.LateCheckDate);
                        if (!string.IsNullOrEmpty(SafePersonQuality.AuditorId))
                        {
                            this.drpAuditor.SelectedValue = SafePersonQuality.AuditorId;
                        }
                        this.txtRemark.Text = SafePersonQuality.Remark;
                        this.txtAuditDate.Text = string.Format("{0:yyyy-MM-dd}", SafePersonQuality.AuditDate);
                    }
                }
                else
                {
                    UserService.InitUserProjectIdUnitIdDropDownList(this.drpAuditor, this.ProjectId, Const.UnitId_SEDIN, true);
                    this.txtSendDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtLimitDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);                   
                }

                if(Request.Params["value"] == "0")
                {
                    this.btnSave.Hidden = true;
                }
            }
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
            if (this.btnSave.Hidden)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SafePersonQualityAttachUrl&type=-1", SafePersonQualityId, BLL.Const.SafePersonQualityMenuId)));
            }
            else
            {
                if (string.IsNullOrEmpty(this.SafePersonQualityId))
                {
                    SaveData(false);
                }
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SafePersonQualityAttachUrl&menuId={1}", SafePersonQualityId, BLL.Const.SafePersonQualityMenuId)));
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData(true);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="isClose"></param>
        private void SaveData(bool isClose)
        {
            if (!String.IsNullOrEmpty(this.PersonId))
            {
                Model.QualityAudit_SafePersonQuality SafePersonQuality = new Model.QualityAudit_SafePersonQuality
                {
                    PersonId = this.PersonId,
                    CertificateNo = this.txtCertificateNo.Text.Trim(),
                    CertificateName = this.txtCertificateName.Text.Trim(),
                    Grade = this.txtGrade.Text.Trim(),
                    SendUnit = this.txtSendUnit.Text.Trim(),
                    SendDate = Funs.GetNewDateTime(this.txtSendDate.Text.Trim()),
                    LimitDate = Funs.GetNewDateTime(this.txtLimitDate.Text.Trim()),
                    LateCheckDate = Funs.GetNewDateTime(this.txtLateCheckDate.Text.Trim()),                   
                    Remark = this.txtRemark.Text.Trim(),
                    CompileMan = this.CurrUser.UserId,
                    CompileDate = DateTime.Now,
                    AuditDate = Funs.GetNewDateTime(this.txtAuditDate.Text.Trim())
                };
                if (this.drpAuditor.SelectedValue != Const._Null)
                {
                    SafePersonQuality.AuditorId = this.drpAuditor.SelectedValue;
                }
                if (!string.IsNullOrEmpty(this.SafePersonQualityId))
                {
                    SafePersonQuality.SafePersonQualityId = this.SafePersonQualityId;
                    BLL.SafePersonQualityService.UpdateSafePersonQuality(SafePersonQuality);
                    BLL.LogService.AddSys_Log(this.CurrUser, SafePersonQuality.CertificateNo, SafePersonQuality.SafePersonQualityId, BLL.Const.SafePersonQualityMenuId, BLL.Const.BtnModify);
                }
                else
                {
                    var updateSafe = BLL.SafePersonQualityService.GetSafePersonQualityByPersonId(this.PersonId);
                    if (updateSafe != null)
                    {
                        SafePersonQuality.SafePersonQualityId = updateSafe.SafePersonQualityId;
                        BLL.SafePersonQualityService.UpdateSafePersonQuality(SafePersonQuality);
                        BLL.LogService.AddSys_Log(this.CurrUser, SafePersonQuality.CertificateNo, SafePersonQuality.SafePersonQualityId, BLL.Const.SafePersonQualityMenuId, BLL.Const.BtnModify);
                    }
                    else
                    {
                        this.SafePersonQualityId = SQLHelper.GetNewID(typeof(Model.QualityAudit_SafePersonQuality));
                        SafePersonQuality.SafePersonQualityId = this.SafePersonQualityId;
                        BLL.SafePersonQualityService.AddSafePersonQuality(SafePersonQuality);
                        BLL.LogService.AddSys_Log(this.CurrUser, SafePersonQuality.CertificateNo, SafePersonQuality.SafePersonQualityId, BLL.Const.SafePersonQualityMenuId, BLL.Const.BtnAdd);
                    }
                }

                if (isClose)
                {
                    PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
                }
            }
        }
        #endregion
    }
}