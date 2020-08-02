using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BLL;
using Model;

namespace Mvc.Controllers
{
    public class TechnicalContactController : ApiController
    {
        [HttpGet]
        public ResponseData<List<Check_TechnicalContactList>> Index(string projectId, int index, int page, string name =""  )
        {
            ResponseData<List<Check_TechnicalContactList>> res = new ResponseData<List<Check_TechnicalContactList>>();

            res.successful = true;
            res.resultValue = BLL.TechnicalContactListService.getListDataForApi( name, projectId, index, page);
            return res;
        }


        [HttpGet]
        public ResponseData<List<Check_TechnicalContactList>> Search(string projectId, int index, int page,string proposedUnitId = "", string unitWorkId = "", string mainSendUnit = "",string cCUnitIds="", string professional="", string state="",string contactListType="", string isReply = "", string dateA = "", string dateZ = "")
        {
            ResponseData<List<Check_TechnicalContactList>> res = new ResponseData<List<Check_TechnicalContactList>>();

            res.successful = true;
            res.resultValue = BLL.TechnicalContactListService.getListDataForApi( state ,  contactListType ,  isReply ,  dateA ,  dateZ,proposedUnitId ,  unitWorkId ,  mainSendUnit , cCUnitIds,  professional , projectId, index, page);
            return res;
        }
        /// <summary>
        /// 根据id获取详情
        /// </summary>
        /// <param name="CheckControlCode"></param>
        /// <returns></returns>
        public ResponseData<Check_TechnicalContactList> GetContactById(string id)
        { 
            ResponseData<Check_TechnicalContactList> res = new ResponseData<Check_TechnicalContactList>();
            Check_TechnicalContactList technicalContactList = BLL.TechnicalContactListService.GetTechnicalContactListByTechnicalContactListIdForApi(id);

            res.successful = true;
            res.resultValue = BeanUtil.CopyOjbect<Check_TechnicalContactList>(technicalContactList, true);
            return res;
        }
        /// <summary>
        /// 根据code获取 审核记录
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ResponseData<List<Check_TechnicalContactListApprove>> GetApproveById(string id)
        {
            ResponseData<List<Check_TechnicalContactListApprove>> res = new ResponseData<List<Check_TechnicalContactListApprove>>();

            res.successful = true;
            res.resultValue = BLL.TechnicalContactListApproveService.GetListDataByIdForApi(id);
            return res;
        }


        public ResponseData<Check_TechnicalContactListApprove> GetCurrApproveById(string id)
        {
            ResponseData<Check_TechnicalContactListApprove> res = new ResponseData<Check_TechnicalContactListApprove>();

            res.successful = true;
            res.resultValue = BeanUtil.CopyOjbect<Check_TechnicalContactListApprove>(BLL.TechnicalContactListApproveService.getCurrApproveForApi(id), true);
            return res;
        }


        [HttpPost]
        public ResponseData<string> AddTechnicalContact([FromBody]Model.Check_TechnicalContactList CheckControl)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {
                if (string.IsNullOrEmpty(CheckControl.TechnicalContactListId))
                { 
                    CheckControl.TechnicalContactListId = Guid.NewGuid().ToString();
                    CheckControl.CompileDate = DateTime.Now;
                    BLL.TechnicalContactListService.AddTechnicalContactListForApi(CheckControl);
                    //BLL.AttachFileService.updateAttachFile(CheckControl.ReturnAttachUrl, CheckControl.TechnicalContactListId + "r",BLL. Const.TechnicalContactListMenuId);
                    //BLL.AttachFileService.updateAttachFile(CheckControl.AttachUrl, CheckControl.TechnicalContactListId, BLL.Const.TechnicalContactListMenuId);
                    SaveAttachFile(CheckControl.TechnicalContactListId + "r", BLL.Const.TechnicalContactListMenuId, CheckControl.ReturnAttachUrl);
                    SaveAttachFile(CheckControl.TechnicalContactListId, BLL.Const.TechnicalContactListMenuId, CheckControl.AttachUrl);
                    res.resultValue = CheckControl.TechnicalContactListId;
                }
                else
                {
                    BLL.TechnicalContactListService.UpdateTechnicalContactListForApi(CheckControl);
                    //BLL.AttachFileService.updateAttachFile(CheckControl.ReturnAttachUrl, CheckControl.TechnicalContactListId + "r", BLL.Const.TechnicalContactListMenuId);
                    //BLL.AttachFileService.updateAttachFile(CheckControl.AttachUrl, CheckControl.TechnicalContactListId, BLL.Const.TechnicalContactListMenuId);
                    SaveAttachFile(CheckControl.TechnicalContactListId + "r", BLL.Const.TechnicalContactListMenuId, CheckControl.ReturnAttachUrl);
                    SaveAttachFile(CheckControl.TechnicalContactListId, BLL.Const.TechnicalContactListMenuId, CheckControl.AttachUrl);
                    res.resultValue = CheckControl.TechnicalContactListId;
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
        public ResponseData<string> AddApprove([FromBody]Model.Check_TechnicalContactListApprove approve)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {
                Model.Check_TechnicalContactList CheckControl = new Model.Check_TechnicalContactList();
                CheckControl.TechnicalContactListId = approve.TechnicalContactListId;
                CheckControl.State = approve.ApproveType;
                BLL.TechnicalContactListService.UpdateTechnicalContactListForApi(CheckControl);
                Model.Check_TechnicalContactList technicalContactList = TechnicalContactListService.GetTechnicalContactListByTechnicalContactListId(approve.TechnicalContactListId);
                string unitType = string.Empty;
                Model.Project_ProjectUnit unit = ProjectUnitService.GetProjectUnitByUnitIdProjectId(technicalContactList.ProjectId, technicalContactList.ProposedUnitId);
                if (unit != null)
                {
                    unitType = unit.UnitType;
                }
                if (unitType == BLL.Const.ProjectUnitType_1 && technicalContactList.IsReply == "2" && approve.ApproveType == Const.TechnicalContactList_Complete)  //总包发起
                {
                    List<Model.Sys_User> seeUsers = new List<Model.Sys_User>();
                    seeUsers.AddRange(UserService.GetSeeUserList3(technicalContactList.ProjectId, technicalContactList.ProposedUnitId, technicalContactList.MainSendUnitId, technicalContactList.CCUnitIds, technicalContactList.CNProfessionalCode, technicalContactList.UnitWorkId.ToString()));
                    seeUsers = seeUsers.Distinct().ToList();
                    foreach (var seeUser in seeUsers)
                    {
                        Model.Check_TechnicalContactListApprove approveS = new Model.Check_TechnicalContactListApprove();
                        approveS.TechnicalContactListId = approve.TechnicalContactListId;
                        approveS.ApproveMan = seeUser.UserId;
                        approveS.ApproveType = "S";
                        TechnicalContactListApproveService.AddTechnicalContactListApprove(approveS);
                    }
                }
                if (unitType == BLL.Const.ProjectUnitType_2 && technicalContactList.IsReply == "2" && approve.ApproveType == Const.TechnicalContactList_Complete)  //分包发起
                {
                    List<Model.Sys_User> seeUsers = new List<Model.Sys_User>();
                    seeUsers.AddRange(UserService.GetSeeUserList3(technicalContactList.ProjectId, technicalContactList.ProposedUnitId, technicalContactList.MainSendUnitId, technicalContactList.CCUnitIds, technicalContactList.CNProfessionalCode, technicalContactList.UnitWorkId.ToString()));
                    seeUsers = seeUsers.Distinct().ToList();
                    foreach (var seeUser in seeUsers)
                    {
                        Model.Check_TechnicalContactListApprove approveS = new Model.Check_TechnicalContactListApprove();
                        approveS.TechnicalContactListId = approve.TechnicalContactListId;
                        approveS.ApproveMan = seeUser.UserId;
                        approveS.ApproveType = "S";
                        TechnicalContactListApproveService.AddTechnicalContactListApprove(approveS);
                    }
                }
                res.resultValue=  BLL.TechnicalContactListApproveService.AddTechnicalContactListApprove(approve);
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
        public ResponseData<string> UpdateApprove([FromBody]Model.Check_TechnicalContactListApprove approve)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {
                Model.Check_TechnicalContactListApprove approve1 = BLL.TechnicalContactListApproveService.GetTechnicalContactListApproveByApproveIdForApi(approve.TechnicalContactListApproveId);

                Check_TechnicalContactList technicalContactList = BLL.TechnicalContactListService.GetTechnicalContactListByTechnicalContactListId(approve1.TechnicalContactListId);

                if (technicalContactList.IsReply == "1")
                {
                    approve.ApproveDate = DateTime.Now;
                    //  approve1.ApproveIdea = approve.ApproveIdea;
                    // approve1.IsAgree = approve.IsAgree;
                    //approve1.AttachUrl = approve.AttachUrl;

                    switch (approve1.ApproveType)
                    {
                        case "5":
                        case "7":
                        case "F":
                        case "Z":
                        case "J":
                        case "H":
                            {
                                Model.Check_TechnicalContactList CheckControl = new Model.Check_TechnicalContactList();
                                CheckControl.TechnicalContactListId = approve1.TechnicalContactListId;
                                CheckControl.ReOpinion = approve.ApproveIdea;
                                BLL.TechnicalContactListService.UpdateTechnicalContactListForApi(CheckControl);
                                approve.ApproveIdea = null;

                            }
                            break;
                        case "2":
                            Project_ProjectUnit unit = BLL.ProjectUnitService.GetProjectUnitByUnitIdProjectId(technicalContactList.ProjectId, technicalContactList.ProposedUnitId);
                            //Base_Unit unit = BLL.UnitService.GetUnitByUnitId(technicalContactList.ProposedUnitId);
                            if (unit.UnitType == BLL.Const.ProjectUnitType_1)
                            {
                                Model.Check_TechnicalContactList CheckControl = new Model.Check_TechnicalContactList();
                                CheckControl.TechnicalContactListId = approve1.TechnicalContactListId;
                                CheckControl.ReOpinion = approve.ApproveIdea;
                                BLL.TechnicalContactListService.UpdateTechnicalContactListForApi(CheckControl);
                                approve.ApproveIdea = null;

                            }

                            break;
                        case "4":
                            Project_ProjectUnit unit1 = BLL.ProjectUnitService.GetProjectUnitByUnitIdProjectId(technicalContactList.ProjectId, technicalContactList.ProposedUnitId);

                            if (unit1.UnitType != BLL.Const.ProjectUnitType_1)
                            {
                                Model.Check_TechnicalContactList CheckControl = new Model.Check_TechnicalContactList();
                                CheckControl.TechnicalContactListId = approve1.TechnicalContactListId;
                                CheckControl.ReOpinion = approve.ApproveIdea;
                                BLL.TechnicalContactListService.UpdateTechnicalContactListForApi(CheckControl);
                                approve.ApproveIdea = null;

                            }

                            break;
                    }

                }
                BLL.TechnicalContactListApproveService.UpdateTechnicalContactListApproveForApi(approve);
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
            BLL.TechnicalContactListApproveService.See(dataId, userId);
            return res;
        }
    }
}
