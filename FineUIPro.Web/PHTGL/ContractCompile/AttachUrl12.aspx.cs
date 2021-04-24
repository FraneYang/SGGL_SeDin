using System;
using System.Web;

namespace FineUIPro.Web.PHTGL.ContractCompile
{
    public partial class AttachUrl12 : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                string attachUrlId = Request.Params["AttachUrlId"];
                if (!string.IsNullOrEmpty(attachUrlId))
                {
                    var att = BLL.AttachUrl12Service.GetPHTGL_AttachUrl12ById(attachUrlId);
                    if (att == null)
                    {
                        att = BLL.AttachUrl12Service.GetPHTGL_AttachUrl12ById(BLL.AttachUrlService.GetAttachUrlByAttachUrlCode("", 12).AttachUrlId);
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
                var attItem = BLL.AttachUrl12Service.GetPHTGL_AttachUrl12ById(attachUrlId);
                if (attItem != null)
                {
                    attItem.AttachUrlContent = this.txtAttachUrlContent.Text.Trim();
                    attItem.AttachUrlId = attachUrlId;
                    BLL.AttachUrl12Service.UpdatePHTGL_AttachUrl12(attItem);
                }
                else
                {
                    Model.PHTGL_AttachUrl12 newUrl12 = new Model.PHTGL_AttachUrl12();
                    newUrl12.AttachUrlContent = this.txtAttachUrlContent.Text.Trim();
                    newUrl12.AttachUrlId = attachUrlId;
                    newUrl12.AttachUrlItemId = BLL.SQLHelper.GetNewID(typeof(Model.PHTGL_AttachUrl12));
                    BLL.AttachUrl12Service.AddPHTGL_AttachUrl12(newUrl12);
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