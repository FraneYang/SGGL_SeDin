using System;
using System.Web;

namespace FineUIPro.Web.PHTGL.ContractCompile
{
    public partial class AttachUrl6: PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                string attachUrlId = Request.Params["AttachUrlId"];
                if (!string.IsNullOrEmpty(attachUrlId))
                {
                    var att = BLL.AttachUrl6Service.GetAttachUrl6ById(attachUrlId);
                    if (att == null)
                    {
                        att = BLL.AttachUrl6Service.GetAttachUrl6ById(BLL.AttachUrlService.GetAttachUrlByAttachUrlCode("", 6).AttachUrlId);
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
                var attItem = BLL.AttachUrl6Service.GetAttachUrl6ById(attachUrlId);
                if (attItem != null)
                {
                    attItem.AttachUrlContent = this.txtAttachUrlContent.Text.Trim();
                    attItem.AttachUrlId = attachUrlId;
                    BLL.AttachUrl6Service.UpdateAttachUrl6(attItem);
                }
                else
                {
                    Model.PHTGL_AttachUrl6 newUrl6 = new Model.PHTGL_AttachUrl6();
                    newUrl6.AttachUrlContent = this.txtAttachUrlContent.Text.Trim();
                    newUrl6.AttachUrlId = attachUrlId;
                    newUrl6.AttachUrlItemId = BLL.SQLHelper.GetNewID(typeof(Model.PHTGL_AttachUrl6));
                    BLL.AttachUrl6Service.AddAttachUrl6(newUrl6);
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