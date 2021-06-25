using System;
using System.Web;

namespace FineUIPro.Web.PHTGL.ContractCompile
{
    public partial class AttachUrl1 : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                string attachUrlId = Request.Params["AttachUrlId"];
                if (!string.IsNullOrEmpty(attachUrlId))
                {
                    var att = BLL.AttachUrl1Service.GetAttachUrl1ById(attachUrlId);
                    if (att==null)
                    {
                        att= BLL.AttachUrl1Service.GetAttachUrl1ById(BLL.AttachUrlService.GetAttachUrlByAttachUrlCode("",1).AttachUrlId);
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
                var attItem = BLL.AttachUrl1Service.GetAttachUrl1ById(attachUrlId);
                if (attItem != null)
                {
                    attItem.AttachUrlContent = this.txtAttachUrlContent.Text.Trim();
                    attItem.AttachUrlId = attachUrlId;
                    BLL.AttachUrl1Service.UpdateAttachUrl1(attItem);
                }
                else
                {
                    Model.PHTGL_AttachUrl1 newUrl1 = new Model.PHTGL_AttachUrl1();
                    newUrl1.AttachUrlContent = this.txtAttachUrlContent.Text.Trim();
                    newUrl1.AttachUrlId = attachUrlId;
                    newUrl1.AttachUrlItemId = BLL.SQLHelper.GetNewID(typeof(Model.PHTGL_AttachUrl1));
                    BLL.AttachUrl1Service.AddAttachUrl1(newUrl1);
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