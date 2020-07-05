using System;
using System.Linq;
using BLL;

namespace FineUIPro.Web.HSSE.QualityAudit
{
    public partial class EquipmentPersonQualityEdit : PageBase
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
        private string EquipmentPersonQualityId
        {
            get
            {
                return (string)ViewState["EquipmentPersonQualityId"];
            }
            set
            {
                ViewState["EquipmentPersonQualityId"] = value;
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

                    var EquipmentPersonQuality = BLL.EquipmentPersonQualityService.GetEquipmentPersonQualityByPersonId(this.PersonId);
                    if (EquipmentPersonQuality != null)
                    {
                        UserService.InitUserProjectIdUnitIdDropDownList(this.drpAuditor, this.ProjectId, Const.UnitId_SEDIN, true);
                        this.EquipmentPersonQualityId = EquipmentPersonQuality.EquipmentPersonQualityId;
                        this.txtCertificateNo.Text = EquipmentPersonQuality.CertificateNo;
                        this.drpCertificate.SelectedValue = EquipmentPersonQuality.CertificateId;
                        this.txtGrade.Text = EquipmentPersonQuality.Grade;
                        this.txtSendUnit.Text = EquipmentPersonQuality.SendUnit;
                        this.txtSendDate.Text = string.Format("{0:yyyy-MM-dd}", EquipmentPersonQuality.SendDate);
                        this.txtLimitDate.Text = string.Format("{0:yyyy-MM-dd}", EquipmentPersonQuality.LimitDate);
                        this.txtLateCheckDate.Text = string.Format("{0:yyyy-MM-dd}", EquipmentPersonQuality.LateCheckDate);
                        if (!string.IsNullOrEmpty(EquipmentPersonQuality.AuditorId))
                        {
                            this.drpAuditor.SelectedValue = EquipmentPersonQuality.AuditorId;
                        }
                        this.txtRemark.Text = EquipmentPersonQuality.Remark;
                        this.txtAuditDate.Text = string.Format("{0:yyyy-MM-dd}", EquipmentPersonQuality.AuditDate);
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
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/EquipmentPersonQualityAttachUrl&menuId={1}&type=-1", EquipmentPersonQualityId, Const.EquipmentPersonQualityMenuId)));
            }
            else
            {
                if (string.IsNullOrEmpty(this.EquipmentPersonQualityId))
                {
                    SaveData(false);
                }

                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/EquipmentPersonQualityAttachUrl&menuId={1}", EquipmentPersonQualityId, Const.EquipmentPersonQualityMenuId)));
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
                Model.QualityAudit_EquipmentPersonQuality EquipmentPersonQuality = new Model.QualityAudit_EquipmentPersonQuality
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
                    EquipmentPersonQuality.AuditorId = this.drpAuditor.SelectedValue;
                }
                if (!string.IsNullOrEmpty(this.EquipmentPersonQualityId))
                {
                    EquipmentPersonQuality.EquipmentPersonQualityId = this.EquipmentPersonQualityId;
                    BLL.EquipmentPersonQualityService.UpdateEquipmentPersonQuality(EquipmentPersonQuality);
                    BLL.LogService.AddSys_Log(this.CurrUser, EquipmentPersonQuality.CertificateNo, EquipmentPersonQuality.EquipmentPersonQualityId,BLL.Const.EquipmentPersonQualityMenuId,BLL.Const.BtnModify );
                }
                else
                {
                    this.EquipmentPersonQualityId = SQLHelper.GetNewID(typeof(Model.QualityAudit_EquipmentPersonQuality));
                    EquipmentPersonQuality.EquipmentPersonQualityId = this.EquipmentPersonQualityId;
                    BLL.EquipmentPersonQualityService.AddEquipmentPersonQuality(EquipmentPersonQuality);
                    BLL.LogService.AddSys_Log(this.CurrUser, EquipmentPersonQuality.CertificateNo, EquipmentPersonQuality.EquipmentPersonQualityId, BLL.Const.EquipmentPersonQualityMenuId, BLL.Const.BtnAdd);
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