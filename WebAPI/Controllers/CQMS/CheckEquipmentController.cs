using System;
using System.Collections.Generic;
using System.Web.Http;
using Model;
using BLL;

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
        
        // GET: /详情
        [HttpGet]
        public ResponseData<Check_CheckEquipment> GetCheckEquipmentById(string id)
        {
            ResponseData<Check_CheckEquipment> res = new ResponseData<Check_CheckEquipment>();
            //Check_CheckEquipment ce = BLL.CheckEquipmentService.GetCheckEquipmentByCheckEquipmentIdForApi(id);

            res.successful = true;
            res.resultValue = BLL.CheckEquipmentService.GetCheckEquipmentByCheckEquipmentIdForApi(id);
            return res;
        }

        [HttpGet]
        // 获取当前办理状态
        public ResponseData<Check_CheckEquipmentApprove> GetCurrApproveByCheckEquipmentId(string code)
        {
            ResponseData<Check_CheckEquipmentApprove> res = new ResponseData<Check_CheckEquipmentApprove>();

            res.successful = true;
            res.resultValue = BeanUtil.CopyOjbect<Check_CheckEquipmentApprove>(BLL.CheckEquipmentApproveService.CurrentApproveType(code), true);
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

        // 保存提交的检实验信息
        [HttpPost]
        public ResponseData<string> AddEquipment([FromBody]Model.Check_CheckEquipment CheckEquipment)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {
                if (string.IsNullOrEmpty(CheckEquipment.CheckEquipmentId))
                {
                    CheckEquipment.CheckEquipmentId = Guid.NewGuid().ToString();
                    BLL.CheckEquipmentService.AddCheckEquipment(CheckEquipment);
                    SaveAttachFile(CheckEquipment.CheckEquipmentId, BLL.Const.CheckEquipmentMenuId, CheckEquipment.AttachUrl);
                    res.resultValue = CheckEquipment.CheckEquipmentId;
                }else
                {
                    BLL.CheckEquipmentService.UpdateCheckEquipment(CheckEquipment);
                    SaveAttachFile(CheckEquipment.CheckEquipmentId, BLL.Const.CheckEquipmentMenuId, CheckEquipment.AttachUrl);
                    res.resultValue = CheckEquipment.CheckEquipmentId;
                }
                res.successful = true;
            }
            catch (Exception e)
            {
                res.resultHint = e.StackTrace;
                res.successful = false;
            }
            return res;
        }

        public static void SaveAttachFile(string dataId, string menuId, string url)
        {
            Model.ToDoItem toDoItem = new Model.ToDoItem
            {
                MenuId = menuId,
                DataId = dataId,
                UrlStr = url,
            };
            APIUpLoadFileService.SaveAttachUrl(toDoItem);
        }

        // 更新
        [HttpPost]
        public ResponseData<string> UpdateApprove([FromBody]Model.Check_CheckEquipmentApprove approve)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {
                approve.ApproveDate = DateTime.Now;
                BLL.CheckEquipmentApproveService.UpdateCheckEquipmentApproveApi(approve);
                res.successful = true;

            }
            catch (Exception e)
            {
                res.resultHint = e.StackTrace;
                res.successful = false;
            }
            return res;

        }
        // 添加办理记录(提交)
        [HttpPost]
        public ResponseData<string> AddApprove([FromBody]Model.Check_CheckEquipmentApprove approve)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {

                Model.Check_CheckEquipment CheckControl = new Model.Check_CheckEquipment();
                CheckControl.CheckEquipmentId = approve.CheckEquipmentId;
                CheckControl.State = approve.ApproveType;
                BLL.CheckEquipmentService.UpdateCheckEquipment(CheckControl);
                res.resultValue = BLL.CheckEquipmentApproveService.AddCheckEquipmentApproveApi(approve);
                res.successful = true;

            }
            catch (Exception e)
            {
                res.resultHint = e.StackTrace;
                res.successful = false;
            }
            return res;

        }
    }
}
