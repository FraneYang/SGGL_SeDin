using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class PurgeMethodView : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtPurgeMethodCode.Focus();
                btnClose.OnClientClick = ActiveWindow.GetHideReference();

                string purgeMethodId = Request.Params["PurgeMethodId"];
                if (!string.IsNullOrEmpty(purgeMethodId))
                {
                    Model.Base_PurgeMethod purgeMethod = BLL.Base_PurgeMethodService.GetPurgeMethod(purgeMethodId);
                    if (purgeMethod != null)
                    {
                        this.txtPurgeMethodCode.Text = purgeMethod.PurgeMethodCode;
                        txtPurgeMethodName.Text = purgeMethod.PurgeMethodName;
                        this.txtRemark.Text = purgeMethod.Remark;
                    }
                }
            }
        }
    }
}