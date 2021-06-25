using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.PHTGL.ContractCompile
{
    public partial class AttachUrl3 : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                string attachUrlId = Request.Params["AttachUrlId"];
                if (!string.IsNullOrEmpty(attachUrlId))
                {
                    var att = BLL.AttachUrl3Service.GetAttachUrl3ByAttachUrlId(attachUrlId);
                    if (att == null)
                    {
                        att = BLL.AttachUrl3Service.GetAttachUrl3ByAttachUrlId(BLL.AttachUrlService.GetAttachUrlByAttachUrlCode("", 3).AttachUrlId);
                    }
                    if (att != null)
                    {
                        this.txtAttachUrlContent.Text = HttpUtility.HtmlDecode(att.AttachUrlContent);  
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
                var attItem = BLL.AttachUrl3Service.GetAttachUrl3ByAttachUrlId(attachUrlId);
                if (attItem != null)
                {
                    attItem.AttachUrlContent = this.txtAttachUrlContent.Text.Trim();
                    attItem.AttachUrlId = attachUrlId;
                    BLL.AttachUrl3Service.UpdateAttachUrl3(attItem);
                }
                else
                {
                    Model.PHTGL_AttachUrl3 newUrl = new Model.PHTGL_AttachUrl3();
                    newUrl.AttachUrlContent = this.txtAttachUrlContent.Text.Trim();
                    newUrl.AttachUrlId = attachUrlId;
                    newUrl.AttachUrlItemId = BLL.SQLHelper.GetNewID(typeof(Model.PHTGL_AttachUrl3));
                    BLL.AttachUrl3Service.AddAttachUrl3(newUrl);
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