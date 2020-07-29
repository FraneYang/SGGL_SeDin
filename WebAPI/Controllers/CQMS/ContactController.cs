using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BLL;
using Model;
using System.Collections;

namespace Web.obj
{
    public class ContactController : ApiController
    {
        [HttpGet]
        public ResponseData<List<Unqualified_WorkContact>> Index(string projectId, int index, int page, string name = "")
        {
            ResponseData<List<Unqualified_WorkContact>> res = new ResponseData<List<Unqualified_WorkContact>>();

            res.successful = true;
            res.resultValue = BLL.WorkContactService.getListDataForApi(projectId, name, index, page);
            return res;
        }
        [HttpGet]
        public ResponseData<List<Unqualified_WorkContact>> Search(string projectId, int index, int page,string code="", string proposedUnitId = "", string mainSendUnitId = "", string cCUnitId = "", string cause = "", string contents = "", string dateA = "", string dateZ = "")
        {
            ResponseData<List<Unqualified_WorkContact>> res = new ResponseData<List<Unqualified_WorkContact>>();

            res.successful = true;
            res.resultValue = BLL.WorkContactService.getListDataForApi( code ,  proposedUnitId ,  mainSendUnitId ,  cCUnitId ,  cause ,  contents ,  dateA ,  dateZ ,projectId,  index, page);
            return res;
        }

        /// <summary>
        /// 根据id获取详情
        /// </summary>
        /// <param name="CheckControlCode"></param>
        /// <returns></returns>
        [HttpGet]
        public ResponseData<Unqualified_WorkContact> GetContactById(string id)
        {
            ResponseData<Unqualified_WorkContact> res = new ResponseData<Unqualified_WorkContact>();
            Unqualified_WorkContact technicalContactList = BLL.WorkContactService.GetWorkContactByWorkContactIdForApi(id);
            res.successful = true;
            res.resultValue = BeanUtil.CopyOjbect<Unqualified_WorkContact>(technicalContactList, true);
            return res;
        }
        /// <summary>
        /// 根据code获取 审核记录
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        public ResponseData<List<Unqualified_WorkContactApprove>> GetApproveById(string id)
        {
            ResponseData<List<Unqualified_WorkContactApprove>> res = new ResponseData<List<Unqualified_WorkContactApprove>>();

            res.successful = true;
            res.resultValue = BLL.WorkContactApproveService.getListDataByIdForApi(id);
            return res;
        }
        public ResponseData<Unqualified_WorkContactApprove> GetCurrApproveById(string id)
        {
            ResponseData<Unqualified_WorkContactApprove> res = new ResponseData<Unqualified_WorkContactApprove>();

            res.successful = true;
            res.resultValue = BeanUtil.CopyOjbect<Unqualified_WorkContactApprove>(BLL.WorkContactApproveService.getCurrApproveForApi(id), true);
            return res;
        }


        [HttpPost]
        public ResponseData<string> AddContact([FromBody]Model.Unqualified_WorkContact CheckControl)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {
            
                if (string.IsNullOrEmpty(CheckControl.WorkContactId))
                {
                    CheckControl.WorkContactId = Guid.NewGuid().ToString();
                    CheckControl.CompileDate = DateTime.Now;
                    BLL.WorkContactService.AddWorkContactForApi(CheckControl);
                    res.resultValue = CheckControl.WorkContactId;

                    //BLL.AttachFileService.updateAttachFile(CheckControl.ReturnAttachUrl, CheckControl.WorkContactId + "r", BLL.Const.WorkContactMenuId);
                    //BLL.AttachFileService.updateAttachFile(CheckControl.AttachUrl, CheckControl.WorkContactId, BLL.Const.WorkContactMenuId);
                    SaveAttachFile(CheckControl.WorkContactId + "r", BLL.Const.WorkContactMenuId, CheckControl.ReturnAttachUrl);
                    SaveAttachFile(CheckControl.WorkContactId, BLL.Const.WorkContactMenuId, CheckControl.AttachUrl);

                }
                else
                {
                    BLL.WorkContactService.UpdateWorkContactForApi(CheckControl);
                    res.resultValue = CheckControl.WorkContactId;
                    //BLL.AttachFileService.updateAttachFile(CheckControl.ReturnAttachUrl, CheckControl.WorkContactId + "r", BLL.Const.WorkContactMenuId);
                    //BLL.AttachFileService.updateAttachFile(CheckControl.AttachUrl, CheckControl.WorkContactId, BLL.Const.WorkContactMenuId);
                    SaveAttachFile(CheckControl.WorkContactId + "r", BLL.Const.WorkContactMenuId, CheckControl.ReturnAttachUrl);
                    SaveAttachFile(CheckControl.WorkContactId, BLL.Const.WorkContactMenuId, CheckControl.AttachUrl);
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
        public ResponseData<string> AddApprove([FromBody]Model.Unqualified_WorkContactApprove approve)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {
                Model.Unqualified_WorkContact CheckControl = new Model.Unqualified_WorkContact();
                CheckControl.WorkContactId = approve.WorkContactId;
                CheckControl.State = approve.ApproveType;
                BLL.WorkContactService.UpdateWorkContactForApi(CheckControl);

                res.resultValue = BLL.WorkContactApproveService.AddWorkContactApproveForApi(approve);
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
        public ResponseData<string> UpdateApprove([FromBody]Model.Unqualified_WorkContactApprove approve)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {

                approve.ApproveDate = DateTime.Now;
                switch (approve.ApproveType)
                {
                    case "3":
                    case "6":
                    case "7":
                    case "8":
                        {
                            Model.Unqualified_WorkContact contact = new Model.Unqualified_WorkContact();
                            Model.Unqualified_WorkContactApprove approveTemp = WorkContactApproveService.GetWorkContactApproveById(approve.WorkContactApproveId);
                            if (approveTemp != null)
                            {
                                contact.WorkContactId = approveTemp.WorkContactId;
                                contact.ReOpinion = approve.ApproveIdea;
                                BLL.WorkContactService.UpdateWorkContactForApi(contact);
                                approve.ApproveIdea = null;
                            }
                        }
                        break;
                    case "2":
                        {
                            var temp = WorkContactApproveService.GetWorkContactApproveById(approve.WorkContactApproveId);
                            if (temp != null)
                            {
                                Model.Unqualified_WorkContact workContact = WorkContactService.GetWorkContactByWorkContactIdForApi(temp.WorkContactId);

                                // Base_Unit unit = BLL.UnitService.GetUnitByUnitId(workContact.ProposedUnitId.Split('$')[0]);
                                if (!string.IsNullOrEmpty(workContact.ProposedUnitId) && workContact.ProposedUnitId != "$$")
                                {
                                    
                                    if (workContact.ProposedUnitId.Split('$')[2] == BLL.Const.ProjectUnitType_1)
                                    {
                                        Model.Unqualified_WorkContactApprove approveTemp = WorkContactApproveService.GetWorkContactApproveById(approve.WorkContactApproveId);

                                        Model.Unqualified_WorkContact contact = new Model.Unqualified_WorkContact();
                                        contact.WorkContactId = approveTemp.WorkContactId;
                                        contact.ReOpinion = approve.ApproveIdea;
                                        BLL.WorkContactService.UpdateWorkContactForApi(contact);
                                        approve.ApproveIdea = null;

                                    }
                                }
                            }
                        }
                        break;
                    case "4":
                        {


                            Model.Unqualified_WorkContact workContact = WorkContactService.GetWorkContactByWorkContactIdForApi(WorkContactApproveService.GetWorkContactApproveById(approve.WorkContactApproveId).WorkContactId);
                            if (!string.IsNullOrEmpty(workContact.ProposedUnitId) && workContact.ProposedUnitId != "$")
                            {
                               // Project_ProjectUnit unit1 = BLL.ProjectUnitService.GetProjectUnitByUnitIdProjectId(workContact.ProjectId, workContact.ProposedUnitId.Split('$')[0]);

                              //  Base_Unit unit1 = BLL.UnitService.GetUnitByUnitId(workContact.ProposedUnitId.Split('$')[0]);
                                if (workContact.ProposedUnitId.Split('$')[2] != BLL.Const.ProjectUnitType_1)
                                {
                                    Model.Unqualified_WorkContactApprove approveTemp = WorkContactApproveService.GetWorkContactApproveById(approve.WorkContactApproveId);

                                    Model.Unqualified_WorkContact contact = new Model.Unqualified_WorkContact();
                                    contact.WorkContactId = approveTemp.WorkContactId;
                                    contact.ReOpinion = approve.ApproveIdea;
                                    BLL.WorkContactService.UpdateWorkContactForApi(contact);
                                    approve.ApproveIdea = null;

                                }
                            }
                        }
                        break;
                }

                BLL.WorkContactApproveService.UpdateWorkContactApproveForApi(approve);
                res.successful = true;

            }
            catch (Exception e)
            {
                res.resultHint = e.StackTrace;

                res.successful = false;
            }
            return res;

        }
        [HttpGet]
        public ResponseData<string> see(string dataId, string userId)
        {
            ResponseData<string> res = new ResponseData<string>();
            res.successful = true;
            BLL.WorkContactApproveService.See(dataId, userId);
            return res;
        }

    }
}
