using System;
using System.Web;

namespace FineUIPro.Web.PHTGL.ContractCompile
{
    public partial class AttachUrl11 : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                string attachUrlId = Request.Params["AttachUrlId"];
                if (!string.IsNullOrEmpty(attachUrlId))
                {
                    var att = BLL.AttachUrl11Service.GetPHTGL_AttachUrl11ById(attachUrlId);
                    if (att == null)
                    {
                        att = BLL.AttachUrl11Service.GetPHTGL_AttachUrl11ById(BLL.AttachUrlService.GetAttachUrlByAttachUrlCode("", 11).AttachUrlId);
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
                var attItem = BLL.AttachUrl11Service.GetPHTGL_AttachUrl11ById(attachUrlId);
                if (attItem != null)
                {
                    attItem.AttachUrlContent = this.txtAttachUrlContent.Text.Trim();
                    attItem.AttachUrlId = attachUrlId;
                    BLL.AttachUrl11Service.UpdatePHTGL_AttachUrl11(attItem);
                }
                else
                {
                    Model.PHTGL_AttachUrl11 newUrl11 = new Model.PHTGL_AttachUrl11();
                    newUrl11.AttachUrlContent = this.txtAttachUrlContent.Text.Trim();
                    newUrl11.AttachUrlId = attachUrlId;
                    newUrl11.AttachUrlItemId = BLL.SQLHelper.GetNewID(typeof(Model.PHTGL_AttachUrl11));
                    BLL.AttachUrl11Service.AddPHTGL_AttachUrl11(newUrl11);
                }
                var att = BLL.AttachUrlService.GetAttachUrlById(attachUrlId);
                if (att != null)
                {
                    att.IsSelected = true;
                    BLL.AttachUrlService.UpdateAttachUrl(att);
                }
            }
            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideReference());
        }
    }
}