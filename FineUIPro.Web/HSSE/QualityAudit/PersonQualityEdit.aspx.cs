using System;
using System.Linq;
using BLL;

namespace FineUIPro.Web.HSSE.QualityAudit
{
    public partial class PersonQualityEdit : PageBase
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
        private string PersonQualityId
        {
            get
            {
                return (string)ViewState["PersonQualityId"];
            }
            set
            {
                ViewState["PersonQualityId"] = value;
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
                CertificateService.InitCertificateDropDownList(this.drpCertificate, true);
                if (!string.IsNullOrEmpty(this.PersonId))
                {
                    var person = BLL.PersonService.GetPersonById(this.PersonId);
                    if (person != null)
                    {
                        this.ProjectId = person.ProjectId;                      
                        var unit = UnitService.GetUnitByUnitId(person.UnitId);
                        if (unit != null)
                        {
                            this.txtUnitCode.Text = unit.UnitCode;
                            this.txtUnitName.Text = unit.UnitName;
                        }
                        this.txtPersonName.Text = person.PersonName;
                        var workPost = WorkPostService.GetWorkPostById(person.WorkPostId);
                        if (workPost != null)
                        {
                            this.txtWorkPostName.Text = workPost.WorkPostName;
                        }
                    }

                    var personQuality = BLL.PersonQualityService.GetPersonQualityByPersonId(this.PersonId);
                    if (personQuality != null)
                    {
                        UserService.InitUserProjectIdUnitIdDropDownList(this.drpAuditor, this.ProjectId, Const.UnitId_SEDIN, true);
                        this.PersonQualityId = personQuality.PersonQualityId;
                        this.txtCertificateNo.Text = personQuality.CertificateNo;
                        this.drpCertificate.SelectedValue = personQuality.CertificateId;
                        this.txtGrade.Text = personQuality.Grade;
                        this.txtSendUnit.Text = personQuality.SendUnit;
                        this.txtSendDate.Text = string.Format("{0:yyyy-MM-dd}", personQuality.SendDate);
                        this.txtLimitDate.Text = string.Format("{0:yyyy-MM-dd}", personQuality.LimitDate);
                        this.txtLateCheckDate.Text = string.Format("{0:yyyy-MM-dd}", personQuality.LateCheckDate);
                        if (!string.IsNullOrEmpty(personQuality.AuditorId))
                        {
                            this.drpAuditor.SelectedValue = personQuality.AuditorId;
                        }
                        this.txtRemark.Text = personQuality.Remark;
                        this.txtAuditDate.Text = string.Format("{0:yyyy-MM-dd}", personQuality.AuditDate);
                    }
                }
                else
                {
                    this.txtSendDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtLimitDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    UserService.InitUserProjectIdUnitIdDropDownList(this.drpAuditor, this.ProjectId, Const.UnitId_SEDIN, true);
                }

                if (Request.Params["value"] == "0")
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
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PersonQualityAttachUrl&menuId={1}&type=-1", PersonQualityId, BLL.Const.PersonQualityMenuId)));
            }
            else
            {
                if (string.IsNullOrEmpty(this.PersonQualityId))
                {
                    SaveData(false);
                }

                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PersonQualityAttachUrl&menuId={1}", PersonQualityId, BLL.Const.PersonQualityMenuId)));
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
            if (this.drpCertificate.SelectedValue == BLL.Const._Null || string.IsNullOrEmpty(this.drpCertificate.SelectedValue))
            {
                Alert.ShowInTop("请选择特岗证书！", MessageBoxIcon.Warning);
                return;
            }

            if (!String.IsNullOrEmpty(this.PersonId))
            {
                Model.QualityAudit_PersonQuality personQuality = new Model.QualityAudit_PersonQuality
                {
                    PersonId = this.PersonId,
                    CertificateNo = this.txtCertificateNo.Text.Trim(),
                    CertificateId = this.drpCertificate.SelectedValue,
                    CertificateName = this.drpCertificate.SelectedItem.Text,
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
                    personQuality.AuditorId = this.drpAuditor.SelectedValue;
                }
                if (!string.IsNullOrEmpty(this.PersonQualityId))
                {
                    personQuality.PersonQualityId = this.PersonQualityId;
                    BLL.PersonQualityService.UpdatePersonQuality(personQuality);
                    BLL.LogService.AddSys_Log(this.CurrUser, personQuality.CertificateNo, personQuality.PersonQualityId,BLL.Const.PersonQualityMenuId,BLL.Const.BtnModify );
                }
                else
                {
                    this.PersonQualityId = SQLHelper.GetNewID(typeof(Model.QualityAudit_PersonQuality));
                    personQuality.PersonQualityId = this.PersonQualityId;
                    BLL.PersonQualityService.AddPersonQuality(personQuality);
                    BLL.LogService.AddSys_Log(this.CurrUser, personQuality.CertificateNo, personQuality.PersonQualityId, BLL.Const.PersonQualityMenuId, BLL.Const.BtnAdd);
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