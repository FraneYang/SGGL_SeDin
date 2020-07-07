using System;
using System.Linq;
using BLL;

namespace FineUIPro.Web.HSSE.EduTrain
{
    public partial class TaskRecordView : PageBase
    {
        #region 加载页面
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                var q = BLL.TrainingItemService.GetTrainingItemByTrainingItemId(Request.QueryString["TrainingEduItemId"]);
                if (q != null)
                {
                    this.txtTrainingEduItemCode.Text = q.TrainingItemCode;
                    this.txtTrainingEduItemName.Text = q.TrainingItemName;
                    //this.txtSummary.Text = q.Summary;
                    //this.txtInstallationNames.Text = q.InstallationNames;
                    //if (!string.IsNullOrEmpty(q.PictureUrl))
                    //{
                    //    this.trImageUrl.Visible = true;
                    //    this.divPictureUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", q.PictureUrl);
                    //    this.divBeImageUrl.InnerHtml = BLL.UploadAttachmentService.ShowImage("../", q.PictureUrl);
                    //}
                    if (!string.IsNullOrEmpty(q.AttachUrl))
                    {
                        this.divAttachUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("~/", q.AttachUrl);
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
        protected void btnAttachUrlC_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Training&menuId={1}&type=-1", Request.QueryString["TrainingEduItemId"], BLL.Const.TrainDBMenuId)));
        }
        #endregion
    }
}