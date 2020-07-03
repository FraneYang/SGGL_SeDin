using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HJGL.PersonManage
{
    public partial class CheckerManageView : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
            BLL.UnitService.InitUnitByProjectIdUnitTypeDropDownList(drpUnitId, this.CurrUser.LoginProjectId, Const.ProjectUnitType_2, true);
            string CheckerId = Request.Params["CheckerId"];
            if (!string.IsNullOrEmpty(CheckerId))
            {
                Model.SitePerson_Person Checker = BLL.CheckerService.GetCheckerById(CheckerId);
                if (Checker != null)
                {
                    this.txtCheckerCode.Text = Checker.WelderCode;
                    this.txtCheckerName.Text = Checker.PersonName;

                    if (!string.IsNullOrEmpty(Checker.UnitId))
                    {
                        this.drpUnitId.SelectedValue = Checker.UnitId;
                    }
                    this.rblSex.SelectedValue = Checker.Sex;

                    if (Checker.Birthday.HasValue)
                    {
                        this.txtBirthday.Text = string.Format("{0:yyyy-MM-dd}", Checker.Birthday);
                    }
                    this.txtIdentityCard.Text = Checker.IdentityCard;
                    if (Checker.IsUsed == true)
                    {
                        cbIsOnDuty.Checked = true;
                    }
                    else
                    {
                        cbIsOnDuty.Checked = false;
                    }
                }
            }
            else
            {
                this.cbIsOnDuty.Checked = true;
            }
        }
    }
    }