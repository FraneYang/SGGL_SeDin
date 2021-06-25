using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Web;

namespace FineUIPro.Web.PHTGL.ContractCompile
{
    public partial class AttachUrl20 : PageBase
    {

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
         
 
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                string attachUrlId = Request.Params["AttachUrlId"];
                if (!string.IsNullOrEmpty(attachUrlId))
                {
                    var att = BLL.AttachUrl20Service.GetAttachUrl20ByAttachUrlId(attachUrlId);
                    if (att == null)
                    {
                        att = BLL.AttachUrl20Service.GetAttachUrl20ByAttachUrlId(BLL.AttachUrlService.GetAttachUrlByAttachUrlCode("", 10).AttachUrlId);
                    }
                    if (att != null)
                    {
                        this.txtAttachUrlContent.Text = HttpUtility.HtmlDecode(att.AttachUrlContent);
                    }
                }
            }
        }
        #endregion
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
                var attItem = BLL.AttachUrl20Service.GetAttachUrl20ByAttachUrlId(attachUrlId);
                if (attItem != null)
                {
                    attItem.AttachUrlContent = this.txtAttachUrlContent.Text.Trim();
                    attItem.AttachUrlId = attachUrlId;
                    BLL.AttachUrl20Service.UpdateAttachUrl20(attItem);
                }
                else
                {
                    Model.PHTGL_AttachUrl20 newUrl = new Model.PHTGL_AttachUrl20();
                    newUrl.AttachUrlContent = this.txtAttachUrlContent.Text.Trim();
                    newUrl.AttachUrlId = attachUrlId;
                    newUrl.AttachUrlItemId = BLL.SQLHelper.GetNewID(typeof(Model.PHTGL_AttachUrl20));
                    BLL.AttachUrl20Service.AddAttachUrl20(newUrl);
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