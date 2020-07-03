using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BLL;
using Model;

namespace Mvc.Controllers
{
    public class JointCheckController : ApiController
    {


        //
        // 质量共检
        // GET: /Draw/
        [HttpGet]
        public ResponseData<List<Check_JointCheck>> Index(string projectId, int index, int page, string name = null )
        {
            ResponseData<List<Check_JointCheck>> res = new ResponseData<List<Check_JointCheck>>();

            res.successful = true;
            res.resultValue = BLL.JointCheckService.GetListDataForApi(name,  projectId, index, page);
            return res;
        }

        [HttpGet]
        public ResponseData<List<Check_JointCheck>> Search(string projectId, int index, int page, string name = null, string code = null, string unitId = null, string proposeUnitId = null, string type = null, string dateA = null, string dateZ = null, string state = null)
        {
            ResponseData<List<Check_JointCheck>> res = new ResponseData<List<Check_JointCheck>>();

            res.successful = true;
            res.resultValue = BLL.JointCheckService.GetListDataForApi(name, code, unitId, proposeUnitId, type, dateA, dateZ, projectId, state, index, page);
            return res;
        }

        //
        // 质量共检
        // GET: /Draw/
        [HttpGet]
        public ResponseData<Check_JointCheck> GetJointCheck(string id)
        {
            ResponseData<Check_JointCheck> res = new ResponseData<Check_JointCheck>();
            Check_JointCheck jc = BLL.JointCheckService.GetJointCheckForApi(id);
            res.successful = true;
            res.resultValue = BeanUtil.CopyOjbect<Check_JointCheck>(jc, true);
            return res;
        }

        [HttpGet]
        public ResponseData<List<Check_JointCheckApprove>> GetApproveByJcid(string id,string detailId)
        {
            ResponseData<List<Check_JointCheckApprove>> res = new ResponseData<List<Check_JointCheckApprove>>();
            res.successful = true;
            res.resultValue = BLL.JointCheckApproveService.getListDataByJcidForApi(id, detailId);
            return res;

        }
        [HttpGet]
        public ResponseData<Check_JointCheckApprove> getCurrApproveByDetailId(string id)
        {
            ResponseData<Check_JointCheckApprove> res = new ResponseData<Check_JointCheckApprove>();

            res.successful = true;
            res.resultValue = BeanUtil.CopyOjbect<Check_JointCheckApprove>(BLL.JointCheckApproveService.getCurrApproveByDetailIdForApi(id), true);
            return res;
        }
        [HttpGet]
        public ResponseData<List<Check_JointCheckApprove>> GetCurrApproveById(string id)
        {
            ResponseData<List<Check_JointCheckApprove>> res = new ResponseData<List<Check_JointCheckApprove>>();

            res.successful = true;
            res.resultValue =  BLL.JointCheckApproveService.getCurrApproveForApi(id) ;
            return res;
        }
        [HttpGet]
        public ResponseData<Check_JointCheckApprove> GetCurrApproveByJoinCheckId(string id)
        {
            ResponseData<Check_JointCheckApprove> res = new ResponseData<Check_JointCheckApprove>();

            res.successful = true;
            res.resultValue = BLL.JointCheckApproveService.getCurrApproveByJoinCheckIdForApi(id);
            return res;
        }

        [HttpGet]
        public ResponseData<List<View_Check_JointCheckDetail>> GetJointCheckDetail(string id)
        {
            ResponseData<List<View_Check_JointCheckDetail>> res = new ResponseData<List<View_Check_JointCheckDetail>>();
            res.successful = true;
            res.resultValue = BLL.JointCheckDetailService.getListDataForApi(id);
            return res;

        }
        [HttpGet]
        public ResponseData<string> delJointCheckDetail(string id)
        {
            ResponseData<string> res = new ResponseData<string>();
            res.successful = true;
            BLL.JointCheckDetailService.DeleteJointCheckDetailById(id);
            res.resultValue = id;
            return res;

        }

        [HttpPost]
        public ResponseData<string> AddJointCheckDetail([FromBody]Model.Check_JointCheckDetail CheckControl)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {
                if (string.IsNullOrEmpty(CheckControl.JointCheckDetailId))
                {
                  
                    CheckControl.JointCheckDetailId = Guid.NewGuid().ToString();
                    CheckControl.CreateDate = DateTime.Now;
                   
                    BLL.JointCheckDetailService.AddJointCheckDetailForApi(CheckControl);

                    BLL.AttachFileService.updateAttachFile(CheckControl.ReAttachUrl, CheckControl.JointCheckDetailId + "r", Const.JointCheckMenuId);
                    BLL.AttachFileService.updateAttachFile(CheckControl.AttachUrl, CheckControl.JointCheckDetailId, Const.JointCheckMenuId);
                    res.resultValue = CheckControl.JointCheckDetailId;

                    res.successful = true;
                }
                else
                {
                    BLL.JointCheckDetailService.UpdateJointCheckDetailForApi(CheckControl);
                    res.resultValue = CheckControl.JointCheckDetailId;
                    BLL.AttachFileService.updateAttachFile(CheckControl.ReAttachUrl, CheckControl.JointCheckDetailId + "r", Const.JointCheckMenuId);
                    BLL.AttachFileService.updateAttachFile(CheckControl.AttachUrl, CheckControl.JointCheckDetailId, Const.JointCheckMenuId);
                    res.successful = true;
                }
            }
            catch (Exception e)
            {
                res.resultHint = e.StackTrace;
                res.successful = false;
            }
            return res;

        }


        [HttpPost]
        public ResponseData<string> AddJointCheck([FromBody]Model.Check_JointCheck CheckControl)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {
                if (string.IsNullOrEmpty(CheckControl.JointCheckId))
                {
                    string prefix = "T18006-JC-";
                    CheckControl.JointCheckCode =  BLL.SQLHelper.RunProcNewId("SpGetNewCode3", "dbo.Check_JointCheck", "JointCheckCode", prefix);
                    CheckControl.JointCheckId = Guid.NewGuid().ToString();
                    BLL.JointCheckService.AddJointCheckForApi(CheckControl);
                    res.resultValue = CheckControl.JointCheckId;
                }
                else
                {
                    BLL.JointCheckService.UpdateJointCheckForApi(CheckControl);
                    res.resultValue = CheckControl.JointCheckId;
                }
            }
            catch (Exception e)
            {
                res.successful = false;
            }
            res.successful = true;
            return res;

        }

        [HttpPost]
        public ResponseData<string> AddApprove([FromBody]Model.Check_JointCheckApprove approve)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {
                //Model.Check_JointCheck CheckControl = new Model.Check_JointCheck();
                //CheckControl.JointCheckId = approve.JointCheckId;
                //CheckControl.State = approve.ApproveType;
                //BLL.JointCheckService.UpdateJointCheckForApi(CheckControl);
                Check_JointCheckDetail joinCheckDetail = new Check_JointCheckDetail();// BLL.JointCheckDetailService.GetJointCheckDetailByJointCheckDetailIdForApi(approve.JointCheckDetailId);
                joinCheckDetail.State = approve.ApproveType;
                joinCheckDetail.HandleMan = approve.ApproveMan;
                joinCheckDetail.SaveHandleMan = "";
                joinCheckDetail.JointCheckDetailId = approve.JointCheckDetailId;
                BLL.JointCheckDetailService.UpdateJointCheckDetailForApi(joinCheckDetail);
                if (approve.ApproveType != "1")
                {
                    var joinCheckDetailList = BLL.JointCheckDetailService.GetListsForApi(approve.JointCheckId);
                    bool isFinished = true;
                    bool isEditing = true;
                    foreach (var item in joinCheckDetailList)
                    {
                        if ("6" != item.State) isFinished = false;
                        if ("1" != item.State) isEditing = false;
                    }

                    var joinCheck = BLL.JointCheckService.GetJointCheckForApi(approve.JointCheckId);
                    Check_JointCheck cj = new Check_JointCheck();
                    cj.JointCheckId = joinCheck.JointCheckId;
                    if (isFinished)
                    {
                        cj.State = "6";
                    }
                    else if (isEditing)
                    {
                        cj.State = "1";
                    }
                    else
                    {
                        cj.State = "Z";

                    }
                    BLL.JointCheckService.UpdateJointCheckForApi(cj);
                }
                   res.resultValue = BLL.JointCheckApproveService.AddJointCheckApproveForApi(approve);
                
            }
            catch (Exception e)
            {
                res.successful = false;
                res.resultValue = e.Message;
            }
            res.successful = true;
            return res;

        }
        [HttpPost]
        public ResponseData<string> UpdateApprove([FromBody]Model.Check_JointCheckApprove approve)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {
                approve.ApproveDate = DateTime.Now;
             var resApprove =   BLL.JointCheckApproveService.UpdateJointCheckApproveForApi(approve);

                if (approve.ApproveType == "1")
                {
                    var jointCheck = BLL.JointCheckService.GetJointCheckForApi(resApprove.JointCheckId);
                    var jointCheckDetails = BLL.JointCheckDetailService.getListDataForApi(resApprove.JointCheckId);
                    List<Model.Sys_User> seeUsers = new List<Model.Sys_User>();
                    foreach (var a in jointCheckDetails)
                    {
                        a.JointCheckId = approve.JointCheckId;
                        if (string.IsNullOrEmpty(approve.JointCheckId))
                        {
                            seeUsers.AddRange(BLL.UserService.GetSeeUserList2(jointCheck.ProjectId, jointCheck.UnitId, a.CNProfessionalCode, a.UnitWorkId.ToString(), approve.ApproveMan));
                        }
                    }
                    if (string.IsNullOrEmpty(approve.JointCheckId))
                    {
                        seeUsers = seeUsers.Distinct().ToList();
                        foreach (var seeUser in seeUsers)
                        {
                            Model.Check_JointCheckApprove approves = new Model.Check_JointCheckApprove();
                            approves.JointCheckId = approve.JointCheckId;
                            approves.ApproveMan = seeUser.UserId;
                            approves.ApproveType = "S";
                            BLL.JointCheckApproveService.AddJointCheckApproveForApi(approve);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                res.successful = false;
            }
            res.successful = true;
            return res;
        }
        [HttpGet]
        public ResponseData<string> see(string dataId, string userId)
        {
            ResponseData<string> res = new ResponseData<string>();
            res.successful = true;
            BLL.JointCheckApproveService.See(dataId, userId);
            return res;
        }
    }
}
