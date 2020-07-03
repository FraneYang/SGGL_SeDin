using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HJGL.RepairAndExpand
{
    public partial class SeeFilm : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           string repairRecordId = Request.Params["repairRecordId"];
            var repairRecord = BLL.RepairRecordService.GetRepairRecordById(repairRecordId);
            if (!string.IsNullOrEmpty(repairRecord.PhotoUrl))
            {
                imgPhoto.ImageUrl = repairRecord.PhotoUrl;
            }
        }
    }
}