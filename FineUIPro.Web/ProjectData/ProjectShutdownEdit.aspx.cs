using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.ProjectData
{
    public partial class ProjectShutdownEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string ProjectStateId
        {
            get
            {
                return (string)ViewState["ProjectStateId"];
            }
            set
            {
                ViewState["ProjectStateId"] = value;
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]))
                {
                    this.ProjectId = Request.Params["projectId"];
                }

                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ProjectStateId = Request.Params["ProjectStateId"];
                if (!string.IsNullOrEmpty(this.ProjectStateId))
                {
                    Model.Base_ProjectSate projectShutdown = BLL.ProjectSateService.GetProjectSateById(this.ProjectStateId);
                    if (projectShutdown != null)
                    {
                        this.ProjectId = projectShutdown.ProjectId;
                        this.hdOldProjectState.Text = projectShutdown.OldProjectState;
                        this.drpProjectState.SelectedValue = projectShutdown.ProjectState;
                        this.txtCompileMan.Text = BLL.UserService.GetUserNameByUserId(projectShutdown.CompileMan);
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", projectShutdown.CompileDate);
                    }
                }
                else
                {
                    var project = ProjectService.GetProjectByProjectId(this.ProjectId);
                    if (project != null)
                    {                       
                        this.hdOldProjectState.Text = project.ProjectState;
                        this.txtCompileMan.Text = this.CurrUser.UserName;
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
                    }
                }

                this.txtProjectName.Text = BLL.ProjectService.GetProjectNameByProjectId(this.ProjectId);
                if (string.IsNullOrEmpty(this.hdOldProjectState.Text))
                {
                    this.hdOldProjectState.Text = BLL.Const.ProjectState_1;
                }
                if (this.hdOldProjectState.Text == BLL.Const.ProjectState_2)
                {
                    this.txtOldProjectState.Text = "暂停中";
                }
                else if (this.hdOldProjectState.Text == BLL.Const.ProjectState_3)
                {
                    this.txtOldProjectState.Text = "已完工";
                }
                else
                {
                    this.txtOldProjectState.Text = "施工中";
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectShutdownMenuId;
                this.ctlAuditFlow.DataId = this.ProjectStateId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
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
            if (this.hdOldProjectState.Text == this.drpProjectState.SelectedValue)
            {
                ShowNotify("项目状态未改变！", MessageBoxIcon.Warning);
                return;
            }

            Model.Base_ProjectSate projectShutdown = new Model.Base_ProjectSate
            {
                ProjectId = this.ProjectId,
                ProjectState = this.drpProjectState.SelectedValue,
                ////单据状态
                States = BLL.Const.State_0
            };
            if (type == BLL.Const.BtnSubmit)
            {
                projectShutdown.States = this.ctlAuditFlow.NextStep;
                if (projectShutdown.States == Const.State_2) ///完成状态时
                {
                    projectShutdown.ShutdownDate = DateTime.Now;
                }
            }
            if (!string.IsNullOrEmpty(this.ProjectStateId))
            {
                projectShutdown.ProjectStateId = this.ProjectStateId;
                BLL.ProjectSateService.UpdateProjectSate(projectShutdown);
                BLL.LogService.AddSys_Log(this.CurrUser, null, projectShutdown.ProjectStateId,BLL.Const.ProjectShutdownMenuId,BLL.Const.BtnAdd);
            }
            else
            {
                projectShutdown.CompileDate = DateTime.Now;
                projectShutdown.CompileMan = this.CurrUser.UserId;
                projectShutdown.OldProjectState = this.hdOldProjectState.Text;
                this.ProjectStateId = SQLHelper.GetNewID(typeof(Model.Base_ProjectSate));
                projectShutdown.ProjectStateId = this.ProjectStateId;
                BLL.ProjectSateService.AddProjectSate(projectShutdown);
                BLL.LogService.AddSys_Log(this.CurrUser, null, projectShutdown.ProjectStateId, BLL.Const.ProjectShutdownMenuId, BLL.Const.BtnModify);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectShutdownMenuId, this.ProjectStateId, (type == BLL.Const.BtnSubmit ? true : false), this.txtProjectName.Text, "../ProjectData/ProjectShutdownView.aspx?ProjectStateId={0}");

            if (type == BLL.Const.BtnSubmit && projectShutdown.States == BLL.Const.State_2)
            {
                var project = BLL.ProjectService.GetProjectByProjectId(this.ProjectId);
                if (project != null)
                {
                    project.ProjectState = projectShutdown.ProjectState;
                    BLL.ProjectService.UpdateProject(project);
                }
            }
        }
        #endregion        
    }
}