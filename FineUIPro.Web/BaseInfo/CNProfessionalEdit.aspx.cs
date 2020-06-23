using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.BaseInfo
{
    public partial class CNProfessionalEdit : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                string CNProfessionalId = Request.Params["CNProfessionalId"];
                if (!string.IsNullOrEmpty(CNProfessionalId))
                {

                    Model.Base_CNProfessional CNProfessional = BLL.CNProfessionalService.GetCNProfessional(CNProfessionalId);
                    if (CNProfessional != null)
                    {
                        this.txtCNProfessionalCode.Text = CNProfessional.CNProfessionalCode;
                        this.txtProfessionalName.Text = CNProfessional.ProfessionalName;
                        if (CNProfessional.SortIndex != null)
                        {
                            this.txtSortIndex.Text = CNProfessional.SortIndex.ToString();
                        }
                    }
                }
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData(true);
        }

        private void SaveData(bool b)
        {
            string CNProfessionalId = Request.Params["CNProfessionalId"];
            Model.Base_CNProfessional CNProfessional = new Model.Base_CNProfessional();
            CNProfessional.CNProfessionalCode = this.txtCNProfessionalCode.Text.Trim();
            CNProfessional.ProfessionalName = this.txtProfessionalName.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtSortIndex.Text.Trim()))
            {
                CNProfessional.SortIndex = Convert.ToInt32(this.txtSortIndex.Text.Trim());
            }
            if (!string.IsNullOrEmpty(CNProfessionalId))
            {
                CNProfessional.CNProfessionalId = CNProfessionalId;
                BLL.CNProfessionalService.UpdateCNProfessional(CNProfessional);
            }
            else
            {
                CNProfessional.CNProfessionalId = SQLHelper.GetNewID(typeof(Model.Base_CNProfessional));
                BLL.CNProfessionalService.AddCNProfessional(CNProfessional);

            }
            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
    }
}