using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.ProjectData
{
    public partial class UnitWorkView : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                string UnitWorkId = Request.Params["UnitWorkId"];
                if (!string.IsNullOrEmpty(UnitWorkId))
                {
                    Model.WBS_UnitWork UnitWork = BLL.UnitWorkService.GetUnitWorkByUnitWorkId(UnitWorkId);
                    if (UnitWork != null)
                    {
                        this.txtUnitWorkCode.Text = UnitWork.UnitWorkCode;
                        this.txtUnitWorkName.Text = UnitWork.UnitWorkName;
                        if (UnitWork.ProjectType == "1")
                        {
                            this.txtProjectType.Text = "建筑工程";
                        }
                        else
                        {
                            this.txtProjectType.Text = "安装工程";
                        }
                        if (UnitWork.Weights != null)
                        {
                            this.txtWeights.Text = UnitWork.Weights.ToString();
                        }
                    }
                }
            }
        }
    }
}