using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace FineUIPro.Web.Person
{
    public partial class PersonDutyView : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                BLL.UserService.InitUserUnitIdDropDownList(drpSEDINUser, Const.UnitId_SEDIN, true);
                BLL.WorkPostService.InitWorkPostDropDownList(drpWorkPost, true);
                string DutyId = Request.Params["DutyId"];
                if (!string.IsNullOrEmpty(DutyId))
                {
                    var PersonDuty = BLL.Person_DutyService.GetPersonDutyById(DutyId);
                    if (PersonDuty != null)
                    {
                        if (!string.IsNullOrEmpty(PersonDuty.DutyPersonId))
                        {
                            this.drpSEDINUser.SelectedValue = PersonDuty.DutyPersonId;
                        }
                        if (!string.IsNullOrEmpty(PersonDuty.WorkPostId))
                        {
                            this.drpWorkPost.SelectedValue = PersonDuty.WorkPostId;
                        }
                        if (!string.IsNullOrEmpty(PersonDuty.Template))
                        {
                            this.txtTemplate.Text = HttpUtility.HtmlDecode(PersonDuty.Template);
                        }
                    }
                }

            }
        }
    }
}