using BLL;
using System;
using System.Web;

namespace FineUIPro.Web.HSSE.ActionPlan
{
    public partial class ActionPlanListEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string ActionPlanListId
        {
            get
            {
                return (string)ViewState["ActionPlanListId"];
            }
            set
            {
                ViewState["ActionPlanListId"] = value;
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

                this.InitDropDownList();
                this.ActionPlanListId = Request.Params["ActionPlanListId"];
                if (!string.IsNullOrEmpty(this.ActionPlanListId))
                {
                    Model.ActionPlan_ActionPlanList actionPlanList = BLL.ActionPlanListService.GetActionPlanListById(this.ActionPlanListId);
                    if (actionPlanList!=null)
                    {
                        this.ProjectId = actionPlanList.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }

                        ///读取编号
                        this.txtActionPlanListCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.ActionPlanListId);                        
                        this.txtActionPlanListName.Text = actionPlanList.ActionPlanListName;
                        this.txtVersionNo.Text = actionPlanList.VersionNo;
                        this.drpProjectType.SelectedValue = actionPlanList.VersionNo;
                        if (!string.IsNullOrEmpty(actionPlanList.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = actionPlanList.CompileMan;
                        }
                        if (actionPlanList.CompileDate!=null)
                        {
                            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", actionPlanList.CompileDate);
                        }
                        this.txtActionPlanListContents.Text = HttpUtility.HtmlDecode(actionPlanList.ActionPlanListContents);
                    }
                }
                else
                {
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    //var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectActionPlanListMenuId, this.ProjectId);
                    //if (codeTemplateRule != null)
                    //{
                    //    this.txtActionPlanListContents.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    //}

                    ////自动生成编码
                    this.txtActionPlanListCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectActionPlanListMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtActionPlanListName.Text = this.SimpleForm1.Title;
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectActionPlanListMenuId;
                this.ctlAuditFlow.DataId = this.ActionPlanListId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }
        #endregion

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            BLL.UserService.InitUserDropDownList(this.drpCompileMan, this.ProjectId, true);
        }

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
            Model.ActionPlan_ActionPlanList actionPlanList = new Model.ActionPlan_ActionPlanList
            {
                ProjectId = this.ProjectId,
                ActionPlanListCode = this.txtActionPlanListCode.Text.Trim(),
                ActionPlanListName = this.txtActionPlanListName.Text.Trim(),
                VersionNo = this.txtVersionNo.Text.Trim(),
                ProjectType = this.drpProjectType.SelectedValue,
                ActionPlanListContents = HttpUtility.HtmlEncode(this.txtActionPlanListContents.Text)
            };
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                actionPlanList.CompileMan = this.drpCompileMan.SelectedValue;
            }
            if (!string.IsNullOrEmpty(this.txtCompileDate.Text.Trim()))
            {
                actionPlanList.CompileDate = Convert.ToDateTime(this.txtCompileDate.Text.Trim());
            }
            actionPlanList.States = BLL.Const.State_0;
            if (type==BLL.Const.BtnSubmit)
            {
                actionPlanList.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.ActionPlanListId))
            {
                actionPlanList.ActionPlanListId = this.ActionPlanListId;
                BLL.ActionPlanListService.UpdateActionPlanList(actionPlanList);
                BLL.LogService.AddSys_Log(this.CurrUser, this.txtActionPlanListCode.Text, this.ActionPlanListId, BLL.Const.ProjectActionPlanListMenuId, Const.BtnModify);
            }
            else
            {
                this.ActionPlanListId = SQLHelper.GetNewID(typeof(Model.ActionPlan_ActionPlanList));
                actionPlanList.ActionPlanListId = this.ActionPlanListId;
                BLL.ActionPlanListService.AddActionPlanList(actionPlanList);
                BLL.LogService.AddSys_Log(this.CurrUser, this.txtActionPlanListCode.Text, this.ActionPlanListId, BLL.Const.ProjectActionPlanListMenuId, Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectActionPlanListMenuId, this.ActionPlanListId, (type == BLL.Const.BtnSubmit ? true : false), actionPlanList.ActionPlanListName, "../ActionPlan/ActionPlanListView.aspx?ActionPlanListId={0}");
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
            if (string.IsNullOrEmpty(this.ActionPlanListId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ActionPlanListAttachUrl&menuId={1}", this.ActionPlanListId, BLL.Const.ProjectActionPlanListMenuId)));
        }
        #endregion
    }
}