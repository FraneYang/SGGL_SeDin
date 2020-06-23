using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.BaseInfo
{
    public partial class DesignProfessionalEdit : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                string DesignProfessionalId = Request.Params["DesignProfessionalId"];
                if (!string.IsNullOrEmpty(DesignProfessionalId))
                {

                    Model.Base_DesignProfessional DesignProfessional = BLL.DesignProfessionalService.GetDesignProfessional(DesignProfessionalId);
                    if (DesignProfessional != null)
                    {
                        this.txtDesignProfessionalCode.Text = DesignProfessional.DesignProfessionalCode;
                        this.txtProfessionalName.Text = DesignProfessional.ProfessionalName;
                        if (DesignProfessional.SortIndex != null)
                        {
                            this.txtSortIndex.Text = DesignProfessional.SortIndex.ToString();
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
            string DesignProfessionalId = Request.Params["DesignProfessionalId"];
            Model.Base_DesignProfessional DesignProfessional = new Model.Base_DesignProfessional();
            DesignProfessional.DesignProfessionalCode = this.txtDesignProfessionalCode.Text.Trim();
            DesignProfessional.ProfessionalName = this.txtProfessionalName.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtSortIndex.Text.Trim()))
            {
                DesignProfessional.SortIndex = Convert.ToInt32(this.txtSortIndex.Text.Trim());
            }
            if (!string.IsNullOrEmpty(DesignProfessionalId))
            {
                DesignProfessional.DesignProfessionalId = DesignProfessionalId;
                BLL.DesignProfessionalService.UpdateDesignProfessional(DesignProfessional);
            }
            else
            {
                DesignProfessional.DesignProfessionalId = SQLHelper.GetNewID(typeof(Model.Base_DesignProfessional));
                BLL.DesignProfessionalService.AddDesignProfessional(DesignProfessional);

            }
            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
    }
}