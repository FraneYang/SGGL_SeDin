using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class WeldingLocationView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtWeldingLocationCode.Focus();
                btnClose.OnClientClick = ActiveWindow.GetHideReference();

                string WeldingLocationId = Request.Params["WeldingLocationId"];
                if (!string.IsNullOrEmpty(WeldingLocationId))
                {
                    Model.Base_WeldingLocation WeldingLocation = BLL.Base_WeldingLocationServie.GetWeldingLocationById(WeldingLocationId);
                    if (WeldingLocation != null)
                    {
                        this.txtWeldingLocationCode.Text = WeldingLocation.WeldingLocationCode;
                        this.txtWeldingLocationName.Text = WeldingLocation.WeldingLocationName;
                        this.txtRemark.Text = WeldingLocation.Remark;
                    }
                }
            }
        }
    }
}