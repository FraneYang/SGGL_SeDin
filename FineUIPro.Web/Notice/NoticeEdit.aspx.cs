using BLL;
using System;
using System.Linq;
using System.Web;

namespace FineUIPro.Web.Notice
{
    public partial class NoticeEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string NoticeId
        {
            get
            {
                return (string)ViewState["NoticeId"];
            }
            set
            {
                ViewState["NoticeId"] = value;
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
                this.InitDropDownList(); ///初始化下拉框
                this.NoticeId = Request.Params["NoticeId"];
                if (!string.IsNullOrEmpty(this.NoticeId))
                {
                    Model.InformationProject_Notice notice = BLL.NoticeService.GetNoticeById(this.NoticeId);
                    if (notice != null)
                    {
                        this.ProjectId = notice.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        } 
                        ///读取编号
                        if (!string.IsNullOrEmpty(notice.NoticeCode))
                        {
                            this.txtNoticeCode.Text = notice.NoticeCode;
                        }
                        else
                        {
                            this.txtNoticeCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.NoticeId);
                        }
                        this.txtNoticeTitle.Text = notice.NoticeTitle;
                        this.txtMainContent.Text = HttpUtility.HtmlDecode(notice.MainContent);
                        if (!string.IsNullOrEmpty(notice.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = notice.CompileMan;
                        }
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", notice.CompileDate);
                        if (!string.IsNullOrEmpty(notice.AccessProjectId))
                        {
                            this.drpProjects.SelectedValueArray = notice.AccessProjectId.Split(',');
                        }
                    }
                }
                else
                {
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
                    {
                        //var pcodeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectNoticeMenuId, this.CurrUser.LoginProjectId);
                        //if (pcodeTemplateRule != null)
                        //{
                        //    this.txtMainContent.Text = HttpUtility.HtmlDecode(pcodeTemplateRule.Template);
                        //}
                        ////自动生成编码
                        this.txtNoticeCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectNoticeMenuId, this.ProjectId, this.CurrUser.UnitId);
                    }
                    else
                    {
                        var codeTemplateRule = BLL.SysConstSetService.GetCodeTemplateRuleByMenuId(BLL.Const.ServerNoticeMenuId);
                        if (codeTemplateRule != null)
                        {
                            this.txtMainContent.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                        }

                        ////自动生成编码
                        this.txtNoticeCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ServerNoticeMenuId, this.ProjectId, this.CurrUser.UnitId);
                    }

                    this.txtNoticeTitle.Text = this.SimpleForm1.Title;
                }    
                ///初始化审核菜单
                if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))  ///项目上发通知
                {
                    this.ctlAuditFlow.MenuId = BLL.Const.ProjectNoticeMenuId;
                    this.drpProjects.SelectedValue = this.CurrUser.LoginProjectId;
                    this.drpProjects.Enabled = false;
                    this.trProjects1.Hidden = true;
                }
                else
                {
                    this.ctlAuditFlow.MenuId = BLL.Const.ServerNoticeMenuId;
                    this.trProjects1.Hidden = false;
                }
                this.ctlAuditFlow.DataId = this.NoticeId;
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
            BLL.UserService.InitUserProjectIdUnitIdDropDownList(this.drpCompileMan, this.ProjectId,
                    (string.IsNullOrEmpty(this.CurrUser.UnitId) ? Const.UnitId_SEDIN : this.CurrUser.UnitId), true);

            this.drpProjects.DataTextField = "ProjectName";
            this.drpProjects.DataValueField = "ProjectId";
            var projectsNo = BLL.ProjectService.GetProjectWorkList();
            if (string.IsNullOrEmpty(this.ProjectId))
            {
                Model.Base_Project newProject = new Model.Base_Project
                {
                    ProjectId = "#",
                    ProjectName = "公司本部",
                    ProjectCode = "0"
                };
                projectsNo.Add(newProject);
            }           
            this.drpProjects.DataSource = projectsNo;
            this.drpProjects.DataBind();
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
               ShowNotify("请选择下一步办理人！", MessageBoxIcon.Warning);
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
            if (this.drpProjects.SelectedItemArray.Count() == 0)
            {
                ShowNotify("请选择接收对象！", MessageBoxIcon.Warning);
                return;
            }

            Model.InformationProject_Notice newNotice = new Model.InformationProject_Notice
            {
                ProjectId =string.IsNullOrEmpty(this.ProjectId) ? null : this.ProjectId,
                NoticeCode = this.txtNoticeCode.Text.Trim(),
                NoticeTitle = this.txtNoticeTitle.Text.Trim(),
                CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim())
            };
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                newNotice.CompileMan = this.drpCompileMan.SelectedValue;
            }
            newNotice.MainContent = HttpUtility.HtmlEncode(this.txtMainContent.Text);
            if (this.drpProjects.SelectedItemArray.Count() > 0)
            {
                string projectIds = string.Empty;
                string projectTexts = string.Empty;
                foreach (ListItem item in this.drpProjects.SelectedItemArray)
                {
                    if (item.Value == "#")
                    {
                        projectIds += item.Value + ",";
                        projectTexts += item.Text + ",";
                    }
                    else
                    {
                        var projects = BLL.ProjectService.GetProjectByProjectId(item.Value);
                        if (projects != null)
                        {
                            projectIds += item.Value + ",";
                            projectTexts += item.Text + ",";
                        }
                    }
                }

                if (!string.IsNullOrEmpty(projectIds))
                {
                    projectIds = projectIds.Substring(0, projectIds.LastIndexOf(","));
                }
                if (!string.IsNullOrEmpty(projectTexts))
                {
                    projectTexts = projectTexts.Substring(0, projectTexts.LastIndexOf(","));
                }
                newNotice.AccessProjectId = projectIds;
                newNotice.AccessProjectText = projectTexts;
            }
            else
            {  
                newNotice.AccessProjectId = this.ProjectId;
            }  
            ////单据状态
            newNotice.States = BLL.Const.State_0;
            newNotice.IsRelease =false;
            if (type == BLL.Const.BtnSubmit)
            {
                newNotice.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.NoticeId))
            {
                newNotice.NoticeId = this.NoticeId;
                BLL.NoticeService.UpdateNotice(newNotice);
                BLL.LogService.AddSys_Log(this.CurrUser, newNotice.NoticeCode, newNotice.NoticeId, BLL.Const.ServerNoticeMenuId, Const.BtnModify);
            }
            else
            {
                this.NoticeId = SQLHelper.GetNewID(typeof(Model.InformationProject_Notice));
                newNotice.NoticeId = this.NoticeId;
                BLL.NoticeService.AddNotice(newNotice);
                BLL.LogService.AddSys_Log(this.CurrUser, newNotice.NoticeCode, newNotice.NoticeId, BLL.Const.ServerNoticeMenuId, Const.BtnAdd);
            }
            ///初始化审核菜单
            string menuId = BLL.Const.ServerNoticeMenuId;
            if (!string.IsNullOrEmpty(this.ProjectId))
            {
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectNoticeMenuId;
            }

            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, menuId, this.NoticeId, (type == BLL.Const.BtnSubmit ? true : false), newNotice.NoticeTitle, "../InformationProject/NoticeView.aspx?NoticeId={0}");
           
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
            if (string.IsNullOrEmpty(this.NoticeId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/NoticeAttachUrl&menuId={1}", this.NoticeId, BLL.Const.ProjectNoticeMenuId)));
            }
            else
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/NoticeAttachUrl&menuId={1}", this.NoticeId, BLL.Const.ServerNoticeMenuId)));
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SelectALL_Click(object sender, EventArgs e)
        {           
            string value = string.Empty;
            foreach (var item in this.drpProjects.Items)
            {
                if (string.IsNullOrEmpty(value))
                {
                    value = item.Value;
                }
                else
                {
                    value += "," + item.Value; ;
                }
            }
            if (!string.IsNullOrEmpty(value))
            {
                this.drpProjects.SelectedValueArray = value.Split(',');
            }
        }

        protected void SelectNoALL_Click(object sender, EventArgs e)
        {
            this.drpProjects.SelectedValueArray = null;
        }
    }
}