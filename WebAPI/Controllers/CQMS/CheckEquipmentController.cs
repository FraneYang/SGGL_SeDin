using System;
using System.Collections.Generic;
using System.Web.Http;
using Model;

namespace Mvc.Controllers
{
    public class CheckEquipmentController : ApiController
    {
        //
        // GET: /CheckEquipment/
        [HttpGet]
        public ResponseData<List<Check_CheckEquipment>> Index(string projectId, int index, int page, string name = null)
        {
            ResponseData<List<Check_CheckEquipment>> res = new ResponseData<List<Check_CheckEquipment>>();
            if (name == null)
                name = "";
            res.successful = true;
            try
            {
                res.resultValue = BLL.CheckEquipmentService.getListByProject(name, projectId, index, page);
            }
            catch (Exception e)
            {
                res.resultHint = e.StackTrace;
                res.successful = false;
            }
            return res; ;
        }
        //
        // GET: /Draw/
        [HttpGet]
        public ResponseData<Check_CheckEquipment> GetCheckEquipmentById(string id)
        {
            ResponseData<Check_CheckEquipment> res = new ResponseData<Check_CheckEquipment>();
            Check_CheckEquipment ce = BLL.CheckEquipmentService.GetCheckEquipmentByCheckEquipmentIdForApi(id);

            res.successful = true;
            res.resultValue = BeanUtil.CopyOjbect<Check_CheckEquipment>(ce, true);
            return res;
        }
        [HttpGet]
        public ResponseData<List<Check_CheckEquipmentApprove>> GetApproveByEquipmentId(string id)
        {
            ResponseData<List<Check_CheckEquipmentApprove>> res = new ResponseData<List<Check_CheckEquipmentApprove>>();
            res.successful = true;
            res.resultValue = BLL.CheckEquipmentApproveService.getListDataByEid(id);
            return res;
        }
    }
}
