using System;
using System.Web;

namespace FineUIPro.Web.PHTGL.ContractCompile
{
    public partial class AttachUrl18 : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                string attachUrlId = Request.Params["AttachUrlId"];
                if (!string.IsNullOrEmpty(attachUrlId))
                {
                    var att = BLL.AttachUrl18Service.GetPHTGL_AttachUrl18ById(attachUrlId);
                    if (att == null)
                    {
                        att = BLL.AttachUrl18Service.GetPHTGL_AttachUrl18ById(BLL.AttachUrlService.GetAttachUrlByAttachUrlCode("", 18).AttachUrlId);
                    }
                    if (att != null)
                    {
 
                        txtGeneralContractorName.Text = att.GeneralContractorName;
                        txtSubcontractorsName.Text = att.SubcontractorsName;
                        txtProjectName.Text = att.ProjectName;
                        txtContractId.Text = att.ContractId;
                        txtStartDate.Text = att.StartDate.ToString();
                        txtEndDate.Text = att.EndDate.ToString();
                        txtPersonSum.Text = att.PersonSum.ToString ();
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
                var attItem = BLL.AttachUrl18Service.GetPHTGL_AttachUrl18ById(attachUrlId);
                if (attItem != null)
                {
                    attItem.AttachUrlId = attachUrlId;
                    attItem.AttachUrlItemId= BLL.SQLHelper.GetNewID(typeof(Model.PHTGL_AttachUrl18));
                    attItem.GeneralContractorName=txtGeneralContractorName.Text;
                    attItem.SubcontractorsName=txtSubcontractorsName.Text;
                    attItem.ProjectName=txtProjectName.Text;
                    attItem.ContractId=txtContractId.Text;
                    attItem.StartDate= Convert.ToDateTime( txtStartDate.Text);
                    attItem.EndDate=Convert.ToDateTime( txtEndDate.Text);
                    attItem.PersonSum= Convert.ToInt32( txtPersonSum.Text);
                    

                    BLL.AttachUrl18Service.UpdatePHTGL_AttachUrl18(attItem);
                }
                else
                {
                    Model.PHTGL_AttachUrl18 newUrl18 = new Model.PHTGL_AttachUrl18();
                    newUrl18.AttachUrlId = attachUrlId;
                    newUrl18.AttachUrlItemId = BLL.SQLHelper.GetNewID(typeof(Model.PHTGL_AttachUrl18));
                    newUrl18.GeneralContractorName = txtGeneralContractorName.Text;
                    newUrl18.SubcontractorsName = txtSubcontractorsName.Text;
                    newUrl18.ProjectName = txtProjectName.Text;
                    newUrl18.ContractId = txtContractId.Text;
                    newUrl18.StartDate = Convert.ToDateTime(txtStartDate.Text);
                    newUrl18.EndDate = Convert.ToDateTime(txtEndDate.Text);
                    newUrl18.PersonSum = Convert.ToInt32(txtPersonSum.Text);
                    BLL.AttachUrl18Service.AddPHTGL_AttachUrl18(newUrl18);
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