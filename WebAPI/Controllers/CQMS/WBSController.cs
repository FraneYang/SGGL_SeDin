using System;
using System.Collections.Generic;
using System.Web.Http;
using Model;
using BLL;

namespace Mvc.Controllers
{
    public class WBSController : ApiController
    {
        // GET: /WBSControlIer/
        [HttpGet]
        public ResponseData<List<View_WBS_ControlItemAndCycle>> Search(string projectId, int index, int page, string unitWorkId = "", string ControlItemContent = "", string ControlPoint = "", string ControlItemDef = "", string HGForms = "")
        {
            ResponseData<List<View_WBS_ControlItemAndCycle>> res = new ResponseData<List<View_WBS_ControlItemAndCycle>>();

            res.successful = true;
            res.resultValue = BLL.WBSsearchService.getWBSlistForApi(projectId, index, page,unitWorkId, ControlItemContent, ControlPoint, ControlItemDef, HGForms);
            return res;
        }
    }
}
