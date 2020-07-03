using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.CQMS.Check
{
    public partial class DrawView : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BLL.DrawService.InitMainItemDropDownList(drpMainItem, this.CurrUser.LoginProjectId);
                BLL.DrawService.InitDesignCNNameDropDownList(drpDesignCN);
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                string DrawId = Request.Params["DrawId"];
                if (!string.IsNullOrEmpty(DrawId))
                {

                    Model.Check_Draw Draw = BLL.DrawService.GetDrawByDrawId(DrawId);
                    if (Draw != null)
                    {
                        this.txtDrawCode.Text = Draw.DrawCode;
                        this.txtDrawName.Text = Draw.DrawName;
                        if (!string.IsNullOrEmpty(Draw.MainItem))
                        {
                            this.drpMainItem.SelectedValue = Draw.MainItem;
                        }
                        if (!string.IsNullOrEmpty(Draw.DesignCN))
                        {
                            this.drpDesignCN.SelectedValue = Draw.DesignCN;
                        }
                        this.txtEdition.Text = Draw.Edition;
                        this.txtAcceptDate.Text = string.Format("{0:yyyy-MM-dd}", Draw.AcceptDate);

                        if (Draw.IsInvalid == true && Draw.IsInvalid != null)
                        {
                            this.rblIsInvalid.SelectedValue = "true";
                            drprecover.Enabled = true;
                            if (!string.IsNullOrEmpty(Draw.Recover))
                            {
                                this.drprecover.SelectedValue = Draw.Recover;
                            }
                        }
                        else
                        {
                            this.drprecover.SelectedIndex = -1;
                            this.drprecover.Enabled = false;
                        }
                    }
                }
            }
        }
    }
}