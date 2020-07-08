using BLL;
using System;

namespace FineUIPro.Web.PZHGL.InformationProject
{
    public partial class PictureView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string PictureId
        {
            get
            {
                return (string)ViewState["PictureId"];
            }
            set
            {
                ViewState["PictureId"] = value;
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
                PictureTypeService.InitPictureTypeDropDownList(this.drpPictureType, true);
                this.PictureId = Request.Params["PictureId"];
                if (!string.IsNullOrEmpty(this.PictureId))
                {
                    Model.InformationProject_Picture picture = PictureService.GetPictureById(this.PictureId);
                    if (picture != null)
                    {
                        this.txtCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.PictureId);
                        this.txtTitle.Text = picture.Title;
                        this.txtContentDef.Text = picture.ContentDef;
                        this.drpPictureType.SelectedValue = picture.PictureType;
                        this.txtUploadDate.Text = string.Format("{0:yyyy-MM-dd}", picture.UploadDate);
                        this.drpCompileMan.Text = BLL.UserService.GetUserNameByUserId(picture.CompileMan);
                    }
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = Const.ProjectPictureMenuId;
                this.ctlAuditFlow.DataId = this.PictureId;
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
            if (!string.IsNullOrEmpty(this.PictureId))
            {                
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&menuId={1}&path=FileUpload/PictureAttachUrl&type=-1", this.PictureId, BLL.Const.ProjectPictureMenuId)));
            }            
        }
        #endregion
    }
}