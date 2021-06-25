using System;
using System.Web;

namespace FineUIPro.Web.PHTGL.ContractCompile
{
    public partial class AttachUrl8 : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region 旧版
                //this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                //string attachUrlId = Request.Params["AttachUrlId"];
                //if (!string.IsNullOrEmpty(attachUrlId))
                //{
                //    var att = BLL.AttachUrl8Service.GetAttachurl8ById(attachUrlId);
                //    if (att == null)
                //    {
                //        att = BLL.AttachUrl8Service.GetAttachurl8ById(BLL.AttachUrlService.GetAttachUrlByAttachUrlCode("", 8).AttachUrlId);
                //    }
                //    if (att != null)
                //    {
                //        txtProjectManager.Text = att.ProjectManager;
                //        txtProjectManager_deputy.Text = att.ProjectManager_deputy;
                //        txtSafetyDirector.Text = att.SafetyDirector;
                //        txtControlManager.Text = att.ControlManager;
                //        txtDesignManager.Text = att.DesignManager;
                //        txtPurchasingManager.Text = att.PurchasingManager;
                //        txtConstructionManager.Text = att.ConstructionManager;
                //        txtConstructionManager_deputy.Text = att.ConstructionManager_deputy;
                //        txtQualityManager.Text = att.QualityManager;
                //        txtHSEManager.Text = att.HSEManager;
                //        txtDrivingManager.Text = att.DrivingManager;
                //        txtFinancialManager.Text = att.FinancialManager;
                //        txtOfficeManager.Text = att.OfficeManager;


                //    }
                //}
                #endregion
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                string attachUrlId = Request.Params["AttachUrlId"];
                if (!string.IsNullOrEmpty(attachUrlId))
                {
                    var att = BLL.AttachUrl8Service.GetAttachurl8ById(attachUrlId);
                    if (att == null)
                    {
                        att = BLL.AttachUrl8Service.GetAttachurl8ById(BLL.AttachUrlService.GetAttachUrlByAttachUrlCode("", 8).AttachUrlId);
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
                var attItem = BLL.AttachUrl8Service.GetAttachurl8ById(attachUrlId);
                if (attItem != null)
                {
                    attItem.AttachUrlContent = this.txtAttachUrlContent.Text.Trim();
                    attItem.AttachUrlId = attachUrlId;
                    BLL.AttachUrl8Service.UpdateAttachurl8(attItem);
                }
                else
                {
                    Model.PHTGL_AttachUrl8 newUrl = new Model.PHTGL_AttachUrl8();
                    newUrl.AttachUrlContent = this.txtAttachUrlContent.Text.Trim();
                    newUrl.AttachUrlId = attachUrlId;
                    newUrl.AttachUrlItemId = BLL.SQLHelper.GetNewID(typeof(Model.PHTGL_AttachUrl8));
                    BLL.AttachUrl8Service.AddAttachurl8(newUrl);
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
        //#region 旧版
        ///// <summary>
        ///// 保存按钮
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    string attachUrlId = Request.Params["AttachUrlId"];
        //    if (!string.IsNullOrEmpty(attachUrlId))
        //    {
        //        var attItem = BLL.AttachUrl8Service.GetAttachurl8ById(attachUrlId);
        //        if (attItem != null)
        //        {
        //            attItem.AttachUrlId = attachUrlId;
        //            attItem.AttachUrlItemId = Guid.NewGuid().ToString();
        //            attItem.ProjectManager = txtProjectManager.Text;
        //            attItem.ProjectManager_deputy = txtProjectManager_deputy.Text;
        //            attItem.SafetyDirector = txtSafetyDirector.Text;
        //            attItem.ControlManager = txtControlManager.Text;
        //            attItem.DesignManager = txtDesignManager.Text;
        //            attItem.PurchasingManager = txtPurchasingManager.Text;
        //            attItem.ConstructionManager = txtConstructionManager.Text;
        //            attItem.ConstructionManager_deputy = txtConstructionManager_deputy.Text;
        //            attItem.QualityManager = txtQualityManager.Text;
        //            attItem.HSEManager = txtHSEManager.Text;
        //            attItem.DrivingManager = txtDrivingManager.Text;
        //            attItem.FinancialManager = txtFinancialManager.Text;
        //            attItem.OfficeManager = txtOfficeManager.Text;

        //            BLL.AttachUrl8Service.UpdateAttachurl8(attItem);
        //        }
        //        else
        //        {
        //            Model.PHTGL_AttachUrl8 newUrl8 = new Model.PHTGL_AttachUrl8();
        //            newUrl8.ProjectManager = txtProjectManager.Text;
        //            newUrl8.ProjectManager_deputy = txtProjectManager_deputy.Text;
        //            newUrl8.SafetyDirector = txtSafetyDirector.Text;
        //            newUrl8.ControlManager = txtControlManager.Text;
        //            newUrl8.DesignManager = txtDesignManager.Text;
        //            newUrl8.PurchasingManager = txtPurchasingManager.Text;
        //            newUrl8.ConstructionManager = txtConstructionManager.Text;
        //            newUrl8.ConstructionManager_deputy = txtConstructionManager_deputy.Text;
        //            newUrl8.QualityManager = txtQualityManager.Text;
        //            newUrl8.HSEManager = txtHSEManager.Text;
        //            newUrl8.DrivingManager = txtDrivingManager.Text;
        //            newUrl8.FinancialManager = txtFinancialManager.Text;
        //            newUrl8.OfficeManager = txtOfficeManager.Text;
        //            newUrl8.AttachUrlId = attachUrlId;
        //            newUrl8.AttachUrlItemId = BLL.SQLHelper.GetNewID(typeof(Model.PHTGL_AttachUrl8));
        //            BLL.AttachUrl8Service.AddAttachurl8(newUrl8);
        //        }
        //        var att = BLL.AttachUrlService.GetAttachUrlById(attachUrlId);
        //        if (att != null)
        //        {
        //            att.IsSelected = true;
        //            BLL.AttachUrlService.UpdateAttachUrl(att);
        //        }
        //    }
        //    ShowNotify("保存成功！", MessageBoxIcon.Success);
        //    PageContext.RegisterStartupScript(ActiveWindow.GetHideReference());
        //}

        //#endregion
    }
}