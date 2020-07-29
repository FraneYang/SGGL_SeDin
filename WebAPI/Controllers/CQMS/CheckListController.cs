using System.Web.Http;
using Model;
using System.Collections.Generic;
using System;
using BLL;

namespace Mvc.Controllers
{
    public class CheckListController : ApiController
    {
        //
        // 质量巡检
        // GET: /Draw/
        [HttpGet]
        public ResponseData<List<Check_CheckControl>> Index(string projectId, int index, int page, string name = null )
        {
            ResponseData<List<Check_CheckControl>> res = new ResponseData<List<Check_CheckControl>>();

            res.successful = true;
            res.resultValue = BLL.CheckControlService.GetListDataForApi(name, projectId, index, page);
            return res;
        }
        [HttpGet]
        public ResponseData<List<Check_CheckControl>> Search(string projectId, int index, int page, string unitId = null, string unitWork = null, string problemType = null, string professional = null, string state = null, string dateA = null, string dateZ = null)
        {
            ResponseData<List<Check_CheckControl>> res = new ResponseData<List<Check_CheckControl>>();

            res.successful = true;
            res.resultValue = BLL.CheckControlService.GetListDataForApi(unitId, unitWork, problemType, professional, state, dateA, dateZ, projectId, index, page);
            return res;
        }

        /// <summary>
        /// 根据code获取详情
        /// </summary>
        /// <param name="CheckControlCode"></param>
        /// <returns></returns>
        public ResponseData<Check_CheckControl> GetCheckControl(string code)
        {
            ResponseData<Check_CheckControl> res = new ResponseData<Check_CheckControl>();
            Check_CheckControl checkControl = BLL.CheckControlService.GetCheckControlForApi(code);
            res.successful = true;
            res.resultValue = BeanUtil.CopyOjbect<Check_CheckControl>(checkControl, true);
            return res;
        }
        /// <summary>
        /// 根据code获取 审核记录
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ResponseData<List<Check_CheckControlApprove>> GetApproveByCode(string code)
        {
            ResponseData<List<Check_CheckControlApprove>> res = new ResponseData<List<Check_CheckControlApprove>>();

            res.successful = true;
            res.resultValue = BLL.CheckControlApproveService.GetListDataByCodeForApi(code);
            return res;
        }
        public ResponseData<Check_CheckControlApprove> GetCurrApproveByCode(string code)
        {
            ResponseData<Check_CheckControlApprove> res = new ResponseData<Check_CheckControlApprove>();

            res.successful = true;
            res.resultValue = BeanUtil.CopyOjbect<Check_CheckControlApprove>(BLL.CheckControlApproveService.getCurrApproveForApi(code), true);
            return res;
        }
        [HttpPost]
        public ResponseData<string> AddCheckControl([FromBody]Model.Check_CheckControl CheckControl)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {
                if (string.IsNullOrEmpty(CheckControl.CheckControlCode))
                {
                    if (string.IsNullOrEmpty(CheckControl.DocCode))
                    {
                        string prefix = BLL.ProjectService.GetProjectByProjectId(CheckControl.ProjectId).ProjectCode + "-06-CM03-XJ-";
                        CheckControl.DocCode = BLL.SQLHelper.RunProcNewId("SpGetNewCode5", "dbo.Check_CheckControl", "DocCode", prefix);
                    }
                    CheckControl.CheckControlCode = Guid.NewGuid().ToString();
                    BLL.CheckControlService.AddCheckControlForApi(CheckControl);
                    //BLL.AttachFileService.updateAttachFile(CheckControl.ReAttachUrl, CheckControl.CheckControlCode+"r", Const.CheckListMenuId);
                    //BLL.AttachFileService.updateAttachFile(CheckControl.AttachUrl, CheckControl.CheckControlCode, Const.CheckListMenuId);
                    SaveAttachFile(CheckControl.CheckControlCode + "r", BLL.Const.CheckListMenuId, CheckControl.ReAttachUrl);
                    SaveAttachFile(CheckControl.CheckControlCode, BLL.Const.CheckListMenuId, CheckControl.AttachUrl);
                    res.resultValue = CheckControl.CheckControlCode;
                }
                else
                {
                    BLL.CheckControlService.UpdateCheckControlForApi(CheckControl);
                    //BLL.AttachFileService.updateAttachFile(CheckControl.ReAttachUrl, CheckControl.CheckControlCode + "r", Const.CheckListMenuId);
                    //BLL.AttachFileService.updateAttachFile(CheckControl.AttachUrl, CheckControl.CheckControlCode, Const.CheckListMenuId);
                    SaveAttachFile(CheckControl.CheckControlCode + "r", BLL.Const.CheckListMenuId, CheckControl.ReAttachUrl);
                    SaveAttachFile(CheckControl.CheckControlCode, BLL.Const.CheckListMenuId, CheckControl.AttachUrl);
                    res.resultValue = CheckControl.CheckControlCode;
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
        public ResponseData<string> AddApprove([FromBody]Model.Check_CheckControlApprove approve)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {

                Model.Check_CheckControl CheckControl = new Model.Check_CheckControl();
                CheckControl.CheckControlCode = approve.CheckControlCode;
                CheckControl.State=approve.ApproveType;
                BLL.CheckControlService.UpdateCheckControlForApi(CheckControl);
                res.resultValue = BLL.CheckControlApproveService.AddCheckControlApproveForApi(approve);
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
        public ResponseData<string> UpdateApprove([FromBody]Model.Check_CheckControlApprove approve)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {
                // Model.Check_CheckControlApprove approve1 = BLL.CheckControlApproveService.GetCheckControlApproveByCheckControlId(approve.CheckControlCode);
                
                approve.ApproveDate = DateTime.Now;
                BLL.CheckControlApproveService.UpdateCheckControlApproveForApi(approve);
                res.successful = true;

            }
            catch (Exception e)
            {
                res.resultHint = e.StackTrace;
                res.successful = false;
            }
            return res;

        }
        // GET: /Draw/
        [HttpGet]
        public ResponseData<string> see(string dataId, string userId)
        {
            ResponseData<string> res = new ResponseData<string>();
            res.successful = true;
            BLL.CheckControlApproveService.See(dataId, userId);
            return res;
        }
    }
}
