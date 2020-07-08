using System;

namespace FineUIPro.Web.HSSE.Accident
{
    public partial class AccidentHandleView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
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
                this.AccidentHandleId = Request.Params["AccidentHandleId"];
                if (!string.IsNullOrEmpty(this.AccidentHandleId))
                {
                    Model.Accident_AccidentHandle accidentHandle = BLL.AccidentHandleService.GetAccidentHandleById(this.AccidentHandleId);
                    if (accidentHandle != null)
                    {
                        this.txtAccidentHandleCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.AccidentHandleId);
                        this.txtAccidentHandleName.Text = accidentHandle.AccidentHandleName;
                        if (accidentHandle.AccidentDate != null)
                        {
                            this.txtAccidentDate.Text = string.Format("{0:yyyy-MM-dd}", accidentHandle.AccidentDate);
                        }
                        this.txtAccidentDef.Text = accidentHandle.AccidentDef;
                        if (accidentHandle.MoneyLoss != null)
                        {
                            this.txtMoneyLoss.Text = accidentHandle.MoneyLoss.ToString();
                        }
                        if (accidentHandle.WorkHoursLoss != null)
                        {
                            this.txtWorkHoursLoss.Text = accidentHandle.WorkHoursLoss.ToString();
                        }
                        if (accidentHandle.MinorInjuriesPersonNum != null)
                        {
                            this.txtMinorInjuriesPersonNum.Text = accidentHandle.MinorInjuriesPersonNum.ToString();
                        }
                        if (accidentHandle.InjuriesPersonNum != null)
                        {
                            this.txtInjuriesPersonNum.Text = accidentHandle.InjuriesPersonNum.ToString();
                        }
                        if (accidentHandle.DeathPersonNum != null)
                        {
                            this.txtDeathPersonNum.Text = accidentHandle.DeathPersonNum.ToString();
                        }
                        this.txtAccidentHandle.Text = accidentHandle.AccidentHandle;                        
                        this.txtUnitName.Text = BLL.UnitService.GetUnitNameByUnitId(accidentHandle.UnitId);
                    }
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectAccidentHandleMenuId;
                this.ctlAuditFlow.DataId = this.AccidentHandleId;
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
            if (!string.IsNullOrEmpty(this.AccidentHandleId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/AccidentHandleAttachUrl&menuId={1}", this.AccidentHandleId, BLL.Const.ProjectAccidentHandleMenuId)));
            }
        }
        #endregion
    }
}