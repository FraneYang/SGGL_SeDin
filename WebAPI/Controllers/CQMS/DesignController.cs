using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BLL;
using Model;
using System.Collections;

namespace Mvc.Controllers
{
    public class DesignController : ApiController
    {
        //
        // GET: /Draw/
        [HttpGet]
        public ResponseData<List<Check_Design>> Index(string projectId, int index, int page, string name = null)
        {
            ResponseData<List<Check_Design>> res = new ResponseData<List<Check_Design>>();
            if (name == null)
                name = "";
            res.successful = true;
            res.resultValue = BLL.DesignService.getListDataForApi(  projectId, name,   index,   page); 
            return res;
        } //
        // GET: /Draw/
        [HttpGet]
        public ResponseData<List<Check_Design>> Search(string projectId, int index, int page,string carryUnitIds,string state, string mainItemId = null, string cNProfessionalCode = null, string designType = null, string designDateA = null, string designDateZ = null)
        {
            ResponseData<List<Check_Design>> res = new ResponseData<List<Check_Design>>();
           
            res.successful = true;
            res.resultValue = BLL.DesignService.getListDataForApi(carryUnitIds,  state,mainItemId, cNProfessionalCode ,  designType ,  designDateA,  designDateZ ,projectId,  index, page);
            return res;
        }
        //
        // GET: /Draw/
        [HttpGet]
        public ResponseData<Check_Design> GetDesignById(string id)
        {
            ResponseData<Check_Design> res = new ResponseData<Check_Design>();
            Check_Design cd = BLL.DesignService.GetDesignByDesignIdForApi(id);

            res.successful = true;
            res.resultValue = BeanUtil.CopyOjbect<Check_Design>(cd, true);
            res.resultValue.PlanDay = cd.PlanDay;
            return res;
        }
        [HttpGet]
        public ResponseData<List<Check_DesignApprove>>GetApproveById(string id)
        {
            ResponseData<List<Check_DesignApprove>> res = new ResponseData<List<Check_DesignApprove>>();
            res.successful = true;
            res.resultValue = BLL.DesignApproveService.getListDataByIdForApi(id);
            return res;

        }

        public ResponseData<Check_DesignApprove> GetCurrApproveById(string id)
        {
            ResponseData<Check_DesignApprove> res = new ResponseData<Check_DesignApprove>();

            res.successful = true;
            res.resultValue = BeanUtil.CopyOjbect<Check_DesignApprove>(BLL.DesignApproveService.getCurrApproveForApi(id), true);
            return res;
        }

        [HttpPost]
        public ResponseData<string> AddDesign([FromBody]Model.Check_Design CheckControl)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {
                if (string.IsNullOrEmpty(CheckControl.DesignId))
                {
                    CheckControl.DesignId = Guid.NewGuid().ToString();
                    BLL.DesignService.AddDesignForApi(CheckControl);
                    //BLL.AttachFileService.updateAttachFile(CheckControl.AttachUrl, CheckControl.DesignId, Const.DesignMenuId);
                    SaveAttachFile(CheckControl.DesignId, BLL.Const.DesignMenuId, CheckControl.AttachUrl);
                    res.resultValue = CheckControl.DesignId;
                    res.successful = true;
                }
                else
                {
                    BLL.DesignService.UpdateDesignForApi(CheckControl);
                    //BLL.AttachFileService.updateAttachFile(CheckControl.AttachUrl, CheckControl.DesignId, Const.DesignMenuId);
                    SaveAttachFile(CheckControl.DesignId, BLL.Const.DesignMenuId, CheckControl.AttachUrl);
                    res.resultValue = CheckControl.DesignId;
                    res.successful = true;
                }
            }
            catch (Exception e)
            {
                res.successful = false;
                res.resultHint = e.StackTrace;
            }
           
            return res;

        }

        /// <summary>
        /// 
        /// </summary>
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

        [HttpPost]
        public ResponseData<string> AddApprove([FromBody]Model.Check_DesignApprove approve)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {
                Model.Check_Design CheckControl = new Model.Check_Design();
                CheckControl.DesignId = approve.DesignId;
                CheckControl.State = approve.ApproveType;
                BLL.DesignService.UpdateDesignForApi(CheckControl);

              res.resultValue =  BLL.DesignApproveService.AddDesignApproveForApi(approve);
                res.successful = true;

            }
            catch (Exception e)
            {
                res.resultHint = e.StackTrace;
                res.successful = false;
            }
            return res;

        }
        [HttpPost]
        public ResponseData<string> UpdateApprove([FromBody]Model.Check_DesignApprove approve)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {
               // Model.Check_DesignApprove approve1 = BLL.DesignApproveService.GetDesignApproveByDesignId(approve.DesignId);
                approve.ApproveDate = DateTime.Now;
                //approve1.ApproveIdea = approve.ApproveIdea;
                //approve1.IsAgree = approve.IsAgree;
                //approve1.AttachUrl = approve.AttachUrl;
                BLL.DesignApproveService.UpdateDesignApproveForApi(approve);
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
