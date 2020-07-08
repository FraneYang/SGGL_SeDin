using BLL;
using System;

namespace FineUIPro.Web.PZHGL.ProjectData
{
    public partial class ProjectMapView : PageBase
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
                btnClose.OnClientClick = ActiveWindow.GetHideReference();               
                this.ProjectMapId = Request.Params["ProjectMapId"];
                if (!string.IsNullOrEmpty(this.ProjectMapId))
                {
                   var ProjectMap = ProjectMapService.GetProjectMapById(this.ProjectMapId);
                    if (ProjectMap != null)
                    {                        
                        this.txtTitle.Text = ProjectMap.Title;
                        this.txtContentDef.Text = ProjectMap.ContentDef;
                        if (ProjectMap.MapType == "1")
                        {
                            this.txtType.Text = "总平面布置图";
                        }
                       else if (ProjectMap.MapType == "2")
                        {
                            this.txtType.Text = "区域平面图";
                        }
                        else if (ProjectMap.MapType == "3")
                        {
                            this.txtType.Text = "三维模型图";
                        }

                        this.txtUploadDate.Text = string.Format("{0:yyyy-MM-dd}", ProjectMap.UploadDate);
                        this.drpCompileMan.Text = BLL.UserService.GetUserNameByUserId(ProjectMap.CompileMan);
                    }
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
            if (!string.IsNullOrEmpty(this.ProjectMapId))
            {                
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ProjectMapAttachUrl&menuId={1}&type=-1", this.ProjectMapId, BLL.Const.ProjectProjectMapMenuId)));
            }            
        }
        #endregion
    }
}