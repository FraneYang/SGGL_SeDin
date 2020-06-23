using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.ProjectData
{
    public partial class MainItemView : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                txtProjectName.Text = ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId).ProjectName;
                string MainItemId = Request.Params["MainItemId"];
                if (!string.IsNullOrEmpty(MainItemId))
                {
                    Model.ProjectData_MainItem MaineItem = BLL.MainItemService.GetMainItemByMainItemId(MainItemId);
                    this.txtMainItemCode.Text = MaineItem.MainItemCode;
                    this.txtMainItemName.Text = MaineItem.MainItemName;
                    this.txtRemark.Text = MaineItem.Remark;
                    this.txtUnitWork.Text = UnitWorkService.GetUnitWorkName(MaineItem.UnitWorkIds);
                }
            }
        }
    }
}