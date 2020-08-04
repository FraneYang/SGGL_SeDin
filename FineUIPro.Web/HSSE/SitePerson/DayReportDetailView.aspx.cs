using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.SitePerson
{
    public partial class DayReportDetailView : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                string projectId = Request.Params["ProjectId"];
                string unitId = Request.Params["UnitId"];
                DateTime compileDate = Funs.GetNewDateTimeOrNow( Request.Params["CompileDate"]);
                this.Grid1.DataSource = BLL.SitePerson_DayReportUnitDetailService.getDayReportUnitDetails(projectId, unitId, compileDate);
                this.Grid1.DataBind();
            }
        }
    }
}