using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.BaseInfo
{
    public partial class CNProfessionalView : PageBase
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
    }
}