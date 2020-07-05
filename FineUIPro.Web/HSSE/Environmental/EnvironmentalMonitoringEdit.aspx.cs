using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.HSSE.Environmental
{
    public partial class EnvironmentalMonitoringEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string FileId
        {
            get
            {
                return (string)ViewState["FileId"];
            }
            set
            {
                ViewState["FileId"] = value;
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
                BLL.UserService.InitUserDropDownList(this.drpCompileMan, this.CurrUser.LoginProjectId, true);
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.FileId = Request.Params["FileId"];
                if (!string.IsNullOrEmpty(this.FileId))
                {
                    Model.Environmental_EnvironmentalMonitoring EnvironmentalMonitoring = BLL.EnvironmentalMonitoringService.GetEnvironmentalMonitoringById(this.FileId);
                    if (EnvironmentalMonitoring != null)
                    {
                        this.ProjectId = EnvironmentalMonitoring.ProjectId;
                        this.txtFileCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.FileId);
                        this.txtFileName.Text = EnvironmentalMonitoring.FileName;                        
                        if (!string.IsNullOrEmpty(EnvironmentalMonitoring.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = EnvironmentalMonitoring.CompileMan;
                        }
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", EnvironmentalMonitoring.CompileDate);
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(EnvironmentalMonitoring.FileContent);
                    }
                }
                else
                {
                    ////自动生成编码
                    this.txtFileCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.EnvironmentalMonitoringMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;                    
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);                  
                    this.txtFileName.Text = this.SimpleForm1.Title;
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.EnvironmentalMonitoringMenuId;
                this.ctlAuditFlow.DataId = this.FileId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }
        #endregion

        #region 提交按钮
        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.ctlAuditFlow.NextStep == BLL.Const.State_1 && this.ctlAuditFlow.NextPerson == BLL.Const._Null)
            {
                ShowNotify("请选择下一步办理人！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
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
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }


        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.Environmental_EnvironmentalMonitoring EnvironmentalMonitoring = new Model.Environmental_EnvironmentalMonitoring
            {
                FileCode = this.txtFileCode.Text.Trim(),
                FileName = this.txtFileName.Text.Trim()
            };
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                EnvironmentalMonitoring.CompileMan = this.drpCompileMan.SelectedValue;
            }
            EnvironmentalMonitoring.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
            EnvironmentalMonitoring.FileContent = HttpUtility.HtmlEncode(this.txtFileContent.Text);
            ////单据状态
            EnvironmentalMonitoring.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                EnvironmentalMonitoring.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.FileId))
            {
                EnvironmentalMonitoring.FileId = this.FileId;
                BLL.EnvironmentalMonitoringService.UpdateEnvironmentalMonitoring(EnvironmentalMonitoring);
                BLL.LogService.AddSys_Log(this.CurrUser, EnvironmentalMonitoring.FileCode, EnvironmentalMonitoring.FileId, BLL.Const.EnvironmentalMonitoringMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.FileId = SQLHelper.GetNewID(typeof(Model.Environmental_EnvironmentalMonitoring));
                EnvironmentalMonitoring.FileId = this.FileId;
                EnvironmentalMonitoring.ProjectId = this.ProjectId;
                BLL.EnvironmentalMonitoringService.AddEnvironmentalMonitoring(EnvironmentalMonitoring);
                BLL.LogService.AddSys_Log(this.CurrUser, EnvironmentalMonitoring.FileCode, EnvironmentalMonitoring.FileId, BLL.Const.EnvironmentalMonitoringMenuId, BLL.Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.EnvironmentalMonitoringMenuId, this.FileId, (type == BLL.Const.BtnSubmit ? true : false), this.txtFileName.Text.Trim(), "../Environmental/EnvironmentalMonitoringView.aspx?FileId={0}");                         
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
            if (string.IsNullOrEmpty(this.FileId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/EnvironmentalMonitoringAttachUrl&menuId={1}", FileId, BLL.Const.EnvironmentalMonitoringMenuId)));
        }
        #endregion
    }
}