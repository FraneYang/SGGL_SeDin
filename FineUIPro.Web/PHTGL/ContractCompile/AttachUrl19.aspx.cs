using System;
using System.Web;

namespace FineUIPro.Web.PHTGL.ContractCompile
{
    public partial class AttachUrl19 : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                string attachUrlId = Request.Params["AttachUrlId"];
                if (!string.IsNullOrEmpty(attachUrlId))
                {
                    var att = BLL.AttachUrl19Service.GetPHTGL_AttachUrl19ById(attachUrlId);
                    if (att == null)
                    {
                        att = BLL.AttachUrl19Service.GetPHTGL_AttachUrl19ById(BLL.AttachUrlService.GetAttachUrlByAttachUrlCode("", 19).AttachUrlId);
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
                var attItem = BLL.AttachUrl19Service.GetPHTGL_AttachUrl19ById(attachUrlId);
                if (attItem != null)
                {
                    attItem.AttachUrlContent = this.txtAttachUrlContent.Text.Trim();
                    attItem.AttachUrlId = attachUrlId;
                    BLL.AttachUrl19Service.UpdatePHTGL_AttachUrl19(attItem);
                }
                else
                {
                    Model.PHTGL_AttachUrl19 newUrl19 = new Model.PHTGL_AttachUrl19();
                    newUrl19.AttachUrlContent = this.txtAttachUrlContent.Text.Trim();
                    newUrl19.AttachUrlId = attachUrlId;
                    newUrl19.AttachUrlItemId = BLL.SQLHelper.GetNewID(typeof(Model.PHTGL_AttachUrl19));
                    BLL.AttachUrl19Service.AddPHTGL_AttachUrl19(newUrl19);
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