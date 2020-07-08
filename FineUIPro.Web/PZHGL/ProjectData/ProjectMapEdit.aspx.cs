using BLL;
using System;

namespace FineUIPro.Web.PZHGL.ProjectData
{
    public partial class ProjectMapEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string ProjectMapId
        {
            get
            {
                return (string)ViewState["ProjectMapId"];
            }
            set
            {
                ViewState["ProjectMapId"] = value;
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
                this.ProjectMapId = Request.Params["ProjectMapId"];
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.InitDropDownList();
                if (!string.IsNullOrEmpty(this.ProjectMapId))
                {
                    var ProjectMap = BLL.ProjectMapService.GetProjectMapById(this.ProjectMapId);
                    if (ProjectMap != null)
                    {
                        this.ProjectId = ProjectMap.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }

                        this.txtTitle.Text = ProjectMap.Title;
                        this.txtContentDef.Text = ProjectMap.ContentDef;
                        this.drpMapType.SelectedValue = ProjectMap.MapType;
                        this.txtUploadDate.Text = string.Format("{0:yyyy-MM-dd}", ProjectMap.UploadDate);
                        if (!string.IsNullOrEmpty(ProjectMap.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = ProjectMap.CompileMan;
                        }
                    }                  
                }
                else
                {                
                    this.txtTitle.Text = this.drpMapType.SelectedText;
                    this.txtUploadDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                }
            }
        }
        #endregion

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            UserService.InitUserDropDownList(this.drpCompileMan, this.ProjectId, true);
        }

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData();
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 
        /// </summary>
        private void SaveData()
        {
            Model.InformationProject_ProjectMap ProjectMap = new Model.InformationProject_ProjectMap
            {
                ProjectId = this.ProjectId,
                Title = this.txtTitle.Text.Trim(),
                ContentDef = this.txtContentDef.Text.Trim()
            };
            if (this.drpMapType.SelectedValue != Const._Null)
            {
                ProjectMap.MapType = this.drpMapType.SelectedValue;
            }
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                ProjectMap.CompileMan = this.drpCompileMan.SelectedValue;
            }
            ProjectMap.UploadDate = Funs.GetNewDateTime(this.txtUploadDate.Text.Trim());

            if (!string.IsNullOrEmpty(this.ProjectMapId))
            {
                ProjectMap.ProjectMapId = this.ProjectMapId;
                BLL.ProjectMapService.UpdateProjectMap(ProjectMap);
                BLL.LogService.AddSys_Log(this.CurrUser, ProjectMap.Title, ProjectMap.ProjectMapId, BLL.Const.ProjectProjectMapMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.ProjectMapId = SQLHelper.GetNewID();
                ProjectMap.ProjectMapId = this.ProjectMapId;
                BLL.ProjectMapService.AddProjectMap(ProjectMap);
                BLL.LogService.AddSys_Log(this.CurrUser, ProjectMap.Title, ProjectMap.ProjectMapId, BLL.Const.ProjectProjectMapMenuId, BLL.Const.BtnAdd);
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
            if (string.IsNullOrEmpty(this.ProjectMapId))
            {
                this.SaveData();
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ProjectMapAttachUrl&menuId={1}", this.ProjectMapId, BLL.Const.ProjectProjectMapMenuId)));
        }
        #endregion

        protected void drpMapType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtTitle.Text = this.drpMapType.SelectedText;
        }
    }
}