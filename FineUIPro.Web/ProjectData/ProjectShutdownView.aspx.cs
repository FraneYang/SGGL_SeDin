using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.ProjectData
{
    public partial class ProjectShutdownView : PageBase
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
                this.ProjectStateId = Request.Params["ProjectStateId"];
                if (!string.IsNullOrEmpty(this.ProjectStateId))
                {
                    Model.Base_ProjectSate projectShutdown = BLL.ProjectSateService.GetProjectSateById(this.ProjectStateId);
                    if (projectShutdown != null)
                    {                        
                        this.txtCompileMan.Text = BLL.UserService.GetUserNameByUserId(projectShutdown.CompileMan);
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", projectShutdown.CompileDate);
                        this.txtProjectName.Text = BLL.ProjectService.GetProjectNameByProjectId(projectShutdown.ProjectId);
                        if (projectShutdown.OldProjectState == BLL.Const.ProjectState_2)
                        {
                            this.txtOldProjectState.Text = "暂停中";
                        }
                        else if (projectShutdown.OldProjectState == BLL.Const.ProjectState_3)
                        {
                            this.txtOldProjectState.Text = "已完工";
                        }
                        else
                        {
                            this.txtOldProjectState.Text = "施工中";
                        }

                        if (projectShutdown.ProjectState == BLL.Const.ProjectState_2)
                        {
                            this.txtProjectState.Text = "暂停中";
                        }
                        else if (projectShutdown.ProjectState == BLL.Const.ProjectState_3)
                        {
                            this.txtProjectState.Text = "已完工";
                        }
                        else
                        {
                            this.txtProjectState.Text = "施工中";
                        }
                    }
                }
                
             
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectShutdownMenuId;
                this.ctlAuditFlow.DataId = this.ProjectStateId;
            }
        }
        #endregion               
    }
}