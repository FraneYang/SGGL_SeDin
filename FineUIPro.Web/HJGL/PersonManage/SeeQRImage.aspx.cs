using System;
using System.Web;
using System.Web.UI;

namespace FineUIPro.Web.HJGL.PersonManage
{
    public partial class SeeQRImage : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string welderId = Request.Params["PersonId"];
                if (!string.IsNullOrEmpty(welderId))
                {
                    Model.SitePerson_Person welder = BLL.WelderService.GetWelderById(welderId);
                    if (welder != null && !string.IsNullOrEmpty(welder.QRCodeAttachUrl))
                    {
                        this.Image1.ImageUrl = "~/" + welder.QRCodeAttachUrl;
                    }
                }
            }
        }


    }
}
