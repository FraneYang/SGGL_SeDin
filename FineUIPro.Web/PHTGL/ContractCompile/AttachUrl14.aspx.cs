using System;
using System.Web;

namespace FineUIPro.Web.PHTGL.ContractCompile
{
    public partial class AttachUrl14 : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                string attachUrlId = Request.Params["AttachUrlId"];
                if (!string.IsNullOrEmpty(attachUrlId))
                {
                    var att = BLL.AttachUrl14Service.GetPHTGL_AttachUrl14ById(attachUrlId);
                    if (att == null)
                    {
                        att = BLL.AttachUrl14Service.GetPHTGL_AttachUrl14ById(BLL.AttachUrlService.GetAttachUrlByAttachUrlCode("", 14).AttachUrlId);
                    }
                    if (att != null)
                    {
                        txtProjectName.Text = att.ProjectName;   
                        txtPersonAmount.Text = att.PersonAmount.ToString();
                        txtSafetyManagerNumber.Text = att.SafetyManagerNumber.ToString();
                        txtSystemManagerNumber.Text = att.SystemManagerNumber.ToString();

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
                var attItem = BLL.AttachUrl14Service.GetPHTGL_AttachUrl14ById(attachUrlId);
                if (attItem != null)
                {
                    attItem.ProjectName= txtProjectName.Text  ;
                    attItem.PersonAmount=Convert.ToInt32( txtPersonAmount.Text ) ;
                    attItem.SafetyManagerNumber= Convert.ToInt32(txtSafetyManagerNumber.Text)  ;
                    attItem.SystemManagerNumber= Convert.ToInt32(txtSystemManagerNumber.Text ) ;
                    attItem.AttachUrlId = attachUrlId;
                    BLL.AttachUrl14Service.UpdatePHTGL_AttachUrl14(attItem);
                }
                else
                {
                    Model.PHTGL_AttachUrl14 newUrl14 = new Model.PHTGL_AttachUrl14();
                    newUrl14.ProjectName = txtProjectName.Text;
                    newUrl14.PersonAmount = Convert.ToInt32(txtPersonAmount.Text);
                    newUrl14.SafetyManagerNumber = Convert.ToInt32(txtSafetyManagerNumber.Text);
                    newUrl14.SystemManagerNumber = Convert.ToInt32(txtSystemManagerNumber.Text);

                    newUrl14.AttachUrlId = attachUrlId;
                    newUrl14.AttachUrlItemId = BLL.SQLHelper.GetNewID(typeof(Model.PHTGL_AttachUrl14));
                    BLL.AttachUrl14Service.AddPHTGL_AttachUrl14(newUrl14);
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