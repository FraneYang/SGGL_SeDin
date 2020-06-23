using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.BaseInfo
{
    public partial class DesignProfessionalView : PageBase
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
    }
}