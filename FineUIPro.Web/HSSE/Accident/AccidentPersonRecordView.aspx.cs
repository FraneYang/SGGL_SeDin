using System;
using System.Web;

namespace FineUIPro.Web.HSSE.Accident
{
    public partial class AccidentPersonRecordView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string AccidentPersonRecordId
        {
            get
            {
                return (string)ViewState["AccidentPersonRecordId"];
            }
            set
            {
                ViewState["AccidentPersonRecordId"] = value;
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

                this.AccidentPersonRecordId = Request.Params["AccidentPersonRecordId"];
                if (!string.IsNullOrEmpty(this.AccidentPersonRecordId))
                {
                    Model.Accident_AccidentPersonRecord accidentPersonRecord = BLL.AccidentPersonRecordService.GetAccidentPersonRecordById(this.AccidentPersonRecordId);
                    if (accidentPersonRecord != null)
                    {
                        if (!string.IsNullOrEmpty(accidentPersonRecord.ProjectId))
                        {
                            var project = BLL.ProjectService.GetProjectByProjectId(accidentPersonRecord.ProjectId);
                            if (project != null)
                            {
                                this.txtProjectName.Text = project.ProjectName;
                            }
                        }
                        if (!string.IsNullOrEmpty(accidentPersonRecord.AccidentTypeId))
                        {
                            var accidentType = BLL.AccidentTypeService.GetAccidentTypeById(accidentPersonRecord.AccidentTypeId);
                            if (accidentType != null)
                            {
                                this.txtAccidentTypeName.Text = accidentType.AccidentTypeName;
                            }
                        }
                        if (!string.IsNullOrEmpty(accidentPersonRecord.WorkAreaId))
                        {
                            this.txtWorkAreaName.Text = BLL.UnitWorkService.GetNameById(accidentPersonRecord.WorkAreaId);
                        }
                        if (accidentPersonRecord.AccidentDate != null)
                        {
                            this.txtAccidentDate.Text = string.Format("{0:yyyy-MM-dd}", accidentPersonRecord.AccidentDate);
                        }
                        if (!string.IsNullOrEmpty(accidentPersonRecord.PersonId))
                        {
                            var person = BLL.PersonService.GetPersonById(accidentPersonRecord.PersonId);
                            if (person != null)
                            {
                                this.txtPersonName.Text = person.PersonName;
                            }
                        }
                        if (!string.IsNullOrEmpty(accidentPersonRecord.Injury))
                        {
                            if (accidentPersonRecord.Injury == "1")
                            {
                                this.txtInjury.Text = "死亡";
                            }
                            else if (accidentPersonRecord.Injury == "2")
                            {
                                this.txtInjury.Text = "重伤";
                            }
                            else if (accidentPersonRecord.Injury == "3")
                            {
                                this.txtInjury.Text = "轻伤";
                            }
                        }
                        this.txtInjuryPart.Text = accidentPersonRecord.InjuryPart;
                        this.txtHssePersons.Text = accidentPersonRecord.HssePersons;
                        this.txtInjuryResult.Text = accidentPersonRecord.InjuryResult;
                        this.txtPreventiveAction.Text = accidentPersonRecord.PreventiveAction;
                        this.txtHandleOpinion.Text = accidentPersonRecord.HandleOpinion;
                        this.txtFileContents.Text = HttpUtility.HtmlDecode(accidentPersonRecord.FileContent);
                    }
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectAccidentPersonRecordMenuId;
                this.ctlAuditFlow.DataId = this.AccidentPersonRecordId;
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
            if (!string.IsNullOrEmpty(this.AccidentPersonRecordId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/AccidentPersonRecordAttachUrl&menuId={1}", this.AccidentPersonRecordId, BLL.Const.ProjectAccidentPersonRecordMenuId)));
            }
        }
        #endregion
    }
}