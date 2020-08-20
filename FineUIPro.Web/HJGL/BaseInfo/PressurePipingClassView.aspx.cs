using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class PressurePipingClassView : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnClose.OnClientClick = ActiveWindow.GetHideReference();

                string pressurePipingClassId = Request.Params["PressurePipingClassId"];
                if (!string.IsNullOrEmpty(pressurePipingClassId))
                {
                    Model.Base_PressurePipingClass pipingClass = BLL.Base_PressurePipingClassService.GetPressurePipingClass(pressurePipingClassId);
                    if (pipingClass != null)
                    {
                        this.txtPressurePipingClassCode.Text = pipingClass.PressurePipingClassCode;
                        txtPressurePipingType.Text = pipingClass.PressurePipingType;
                        this.txtRemark.Text = pipingClass.Remark;
                    }
                }
            }
        }
    }
}