using System;
using System.Web;

namespace FineUIPro.Web.PHTGL.ContractCompile
{
    public partial class AttachUrl7 : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                string attachUrlId = Request.Params["AttachUrlId"];
                if (!string.IsNullOrEmpty(attachUrlId))
                {
                    var att = BLL.AttachUrl7Service.GetAttachUrl7ById(attachUrlId);
                    if (att == null)
                    {
                        att = BLL.AttachUrl7Service.GetAttachUrl7ById(BLL.AttachUrlService.GetAttachUrlByAttachUrlCode("", 7).AttachUrlId);
                    }
                    if (att != null)
                    {
                        this.txtAttachUrlContent.Text = att.AttachUrlContent;
                    }
                }
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string attachUrlId = Request.Params["AttachUrlId"];
            if (!string.IsNullOrEmpty(attachUrlId))
            {
                var attItem = BLL.AttachUrl7Service.GetAttachUrl7ById(attachUrlId);
                if (attItem != null)
                {
                    attItem.AttachUrlContent = this.txtAttachUrlContent.Text.Trim();
                    attItem.AttachUrlId = attachUrlId;
                    BLL.AttachUrl7Service.UpdateAttachUrl7(attItem);
                }
                else
                {
                    Model.PHTGL_AttachUrl7 newUrl7 = new Model.PHTGL_AttachUrl7();
                    newUrl7.AttachUrlContent = this.txtAttachUrlContent.Text.Trim();
                    newUrl7.AttachUrlId = attachUrlId;
                    newUrl7.AttachUrlItemId = BLL.SQLHelper.GetNewID(typeof(Model.PHTGL_AttachUrl7));
                    BLL.AttachUrl7Service.AddAttachUrl7(newUrl7);
                }
                var att = BLL.AttachUrlService.GetAttachUrlById(attachUrlId);
                if (att != null)
                {
                    att.IsSelected = true;
                    BLL.AttachUrlService.UpdateAttachUrl(att);
                }
            }
            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
    }
}