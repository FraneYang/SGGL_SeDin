using System;
using System.Web;

namespace FineUIPro.Web.PHTGL.ContractCompile
{
    public partial class AttachUrl13 : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                string attachUrlId = Request.Params["AttachUrlId"];
                if (!string.IsNullOrEmpty(attachUrlId))
                {
                    var att = BLL.AttachUrl13Service.GetPHTGL_AttachUrl13ById(attachUrlId);
                    if (att == null)
                    {
                        att = BLL.AttachUrl13Service.GetPHTGL_AttachUrl13ById(BLL.AttachUrlService.GetAttachUrlByAttachUrlCode("", 13).AttachUrlId);
                    }
                    if (att != null)
                    {

                        txtGeneralContractorName.Text = att.GeneralContractorName;
                        txtSubcontractorsName.Text = att.SubcontractorsName;
                        txtProjectName.Text = att.ProjectName;
                         txtOtherWarrantyPeriod.Text = att.OtherWarrantyPeriod;
                        txtWarrantyPeriodDate.Text = att.WarrantyPeriodDate;
                        txtDefectLiabilityDate.Text = att.DefectLiabilityDate;
                        txtDefectLiabilityPeriod.Text = att.DefectLiabilityPeriod;
                    //    txtOtherqualityWarranty.Text = att.OtherqualityWarranty;

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
                var attItem = BLL.AttachUrl13Service.GetPHTGL_AttachUrl13ById(attachUrlId);
                if (attItem != null)
                {
 

                    attItem.AttachUrlId = attachUrlId;
                    attItem.GeneralContractorName = txtGeneralContractorName.Text.Trim();
                    attItem.SubcontractorsName = txtSubcontractorsName.Text.Trim();
                    attItem.ProjectName = txtProjectName.Text.Trim();
                    attItem.WarrantyContent = "";
                    attItem.OtherWarrantyPeriod = txtOtherWarrantyPeriod.Text.Trim();
                    attItem.WarrantyPeriodDate = txtWarrantyPeriodDate.Text;
                    attItem.DefectLiabilityDate = txtDefectLiabilityDate.Text;
                    attItem.DefectLiabilityPeriod = txtDefectLiabilityPeriod.Text.Trim();
                    attItem.OtherqualityWarranty = "";
                    BLL.AttachUrl13Service.UpdatePHTGL_AttachUrl13(attItem);
                }
                else
                {
                    Model.PHTGL_AttachUrl13 newUrl13 = new Model.PHTGL_AttachUrl13();
                    newUrl13.AttachUrlItemId = BLL.SQLHelper.GetNewID(typeof(Model.PHTGL_AttachUrl13));
                    newUrl13.AttachUrlId = attachUrlId;
                    newUrl13.GeneralContractorName = txtGeneralContractorName.Text.Trim();
                    newUrl13.SubcontractorsName = txtSubcontractorsName.Text.Trim();
                    newUrl13.ProjectName = txtProjectName.Text.Trim();
                    newUrl13.WarrantyContent ="";
                    newUrl13.OtherWarrantyPeriod = txtOtherWarrantyPeriod.Text.Trim();
                    newUrl13.WarrantyPeriodDate = txtWarrantyPeriodDate.Text;
                    newUrl13.DefectLiabilityDate = txtDefectLiabilityDate.Text;
                    newUrl13.DefectLiabilityPeriod = txtDefectLiabilityPeriod.Text.Trim();
                    newUrl13.OtherqualityWarranty = "";

                    BLL.AttachUrl13Service.AddPHTGL_AttachUrl13(newUrl13);
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