using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.HSSE.Accident
{
    public partial class AccidentHandleEdit : PageBase
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
                BLL.UnitService.InitUnitDropDownList(this.drpUnitId, this.ProjectId, true);
                this.drpUnitId.SelectedValue = this.CurrUser.UnitId;
                if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
                {
                    this.drpUnitId.Enabled = false;
                }

                this.ProjectId = this.CurrUser.LoginProjectId;
                this.AccidentHandleId = Request.Params["AccidentHandleId"];
                if (!string.IsNullOrEmpty(this.AccidentHandleId))
                {
                    Model.Accident_AccidentHandle accidentHandle = BLL.AccidentHandleService.GetAccidentHandleById(this.AccidentHandleId);
                    if (accidentHandle != null)
                    {
                        this.ProjectId = accidentHandle.ProjectId;
                        this.drpUnitId.SelectedValue = accidentHandle.UnitId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            BLL.UnitService.InitUnitDropDownList(this.drpUnitId, this.ProjectId, true);
                            this.drpUnitId.SelectedValue = this.CurrUser.UnitId;                            
                        }

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
                        //this.txtRemark.Text = accidentHandle.Remark;
                    }
                }
                else
                {
                    this.txtAccidentHandleCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectAccidentHandleMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtAccidentDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectAccidentHandleMenuId;
                this.ctlAuditFlow.DataId = this.AccidentHandleId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }
        #endregion

        #region 保存、提交
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.ctlAuditFlow.NextStep == BLL.Const.State_1 && this.ctlAuditFlow.NextPerson == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择下一步办理人！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.Accident_AccidentHandle accidentHandle = new Model.Accident_AccidentHandle
            {
                ProjectId = this.ProjectId,
                AccidentHandleCode = this.txtAccidentHandleCode.Text.Trim(),
                AccidentHandleName = this.txtAccidentHandleName.Text.Trim(),
                AccidentDate = Convert.ToDateTime(this.txtAccidentDate.Text.Trim()),
                AccidentDef = this.txtAccidentDef.Text.Trim(),
                MoneyLoss = Funs.GetNewDecimalOrZero(this.txtMoneyLoss.Text.Trim()),
                WorkHoursLoss = Funs.GetNewDecimalOrZero(this.txtWorkHoursLoss.Text.Trim()),
                MinorInjuriesPersonNum = Funs.GetNewIntOrZero(this.txtMinorInjuriesPersonNum.Text.Trim()),
                InjuriesPersonNum = Funs.GetNewIntOrZero(this.txtInjuriesPersonNum.Text.Trim()),
                DeathPersonNum = Funs.GetNewIntOrZero(this.txtDeathPersonNum.Text.Trim()),
                AccidentHandle = this.txtAccidentHandle.Text.Trim(),
                //Remark = this.txtRemark.Text.Trim(),
                CompileMan = this.CurrUser.UserId,
                CompileDate = DateTime.Now,
                States = BLL.Const.State_0
            };
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                accidentHandle.UnitId = this.drpUnitId.SelectedValue;
            }

            if (type == BLL.Const.BtnSubmit)
            {
                var flowOperate = new Model.SGGLDB(Funs.ConnString).Sys_FlowOperate.FirstOrDefault(x => x.DataId == this.AccidentHandleId && x.State == BLL.Const.State_2 && x.IsClosed == true);
                if (flowOperate != null)
                {
                    accidentHandle.States = BLL.Const.State_2;
                }
                else
                {
                    accidentHandle.States = this.ctlAuditFlow.NextStep;
                }
            }
            if (!string.IsNullOrEmpty(this.AccidentHandleId))
            {
                accidentHandle.AccidentHandleId = this.AccidentHandleId;
                BLL.AccidentHandleService.UpdateAccidentHandle(accidentHandle);
                BLL.LogService.AddSys_Log(this.CurrUser, this.txtAccidentHandleCode.Text, this.AccidentHandleId, BLL.Const.ProjectAccidentHandleMenuId, Const.BtnModify);
            }
            else
            {
                this.AccidentHandleId = SQLHelper.GetNewID(typeof(Model.Accident_AccidentHandle));
                accidentHandle.AccidentHandleId = this.AccidentHandleId;
                BLL.AccidentHandleService.AddAccidentHandle(accidentHandle);
                BLL.LogService.AddSys_Log(this.CurrUser, this.txtAccidentHandleCode.Text, this.AccidentHandleId, BLL.Const.ProjectAccidentHandleMenuId, Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectAccidentHandleMenuId, this.AccidentHandleId, (type == BLL.Const.BtnSubmit ? true : false), accidentHandle.AccidentHandleName, "../Accident/AccidentHandleView.aspx?AccidentHandleId={0}");
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
            if (string.IsNullOrEmpty(this.AccidentHandleId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/AccidentHandleAttachUrl&menuId={1}", this.AccidentHandleId, BLL.Const.ProjectAccidentHandleMenuId)));
        }
        #endregion
    }
}