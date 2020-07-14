using System.Web.Http;
using Model;
using System.Collections.Generic;
using System;

namespace Mvc.Controllers
{
    public class CQMSConstructSolutionController : ApiController
    {
        //
        // 质量巡检
        // GET: /Draw/
        [HttpGet]
        public ResponseData<List<Solution_CQMSConstructSolution>> Index(string projectId, int index, int page, string name = null)
        {
            ResponseData<List<Solution_CQMSConstructSolution>> res = new ResponseData<List<Solution_CQMSConstructSolution>>();

            res.successful = true;
            res.resultValue = BLL.CQMSConstructSolutionService.getListDataForApi(name, projectId, index, page);
            return res;
        }
        [HttpGet]
        public ResponseData<List<Solution_CQMSConstructSolution>> Search(string projectId, int index, int page, string unitId = "",string unitWork= "",string cNProfessionalCode="", string solutionType = "", string state = "")
        {
            ResponseData<List<Solution_CQMSConstructSolution>> res = new ResponseData<List<Solution_CQMSConstructSolution>>();

            res.successful = true;
            res.resultValue = BLL.CQMSConstructSolutionService.getListDataForApi("", unitId ,  unitWork , cNProfessionalCode,  solutionType ,  state, projectId, index, page);
            return res;
        }
        /// <summary>
        /// 根据code获取详情
        /// </summary>
        /// <param name="CheckControlCode"></param>
        /// <returns></returns>
        public ResponseData<Solution_CQMSConstructSolution> GetSolution(string id)
        {
            ResponseData<Solution_CQMSConstructSolution> res = new ResponseData<Solution_CQMSConstructSolution>();
            Solution_CQMSConstructSolution checkControl = BLL.CQMSConstructSolutionService.GetConstructSolutionByConstructSolutionIdForApi(id);
            res.successful = true;
            res.resultValue = BeanUtil.CopyOjbect<Solution_CQMSConstructSolution>(checkControl, true);
            res.resultValue.Edition = checkControl.Edition;
            return res;
        }
        /// <summary>
        /// 根据code获取 审核记录
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ResponseData<List<Solution_CQMSConstructSolutionApprove>> GetApproveById(string id,int edition)
        {
            ResponseData<List<Solution_CQMSConstructSolutionApprove>> res = new ResponseData<List<Solution_CQMSConstructSolutionApprove>>();

            res.successful = true;
            res.resultValue = BLL.CQMSConstructSolutionApproveService.getListDataForApi(id);
            return res;
        }
        public ResponseData<Solution_CQMSConstructSolutionApprove> GetCurrApproveById(string id, string userId, int edition)
        {
            ResponseData<Solution_CQMSConstructSolutionApprove> res = new ResponseData<Solution_CQMSConstructSolutionApprove>();
            var approve = BLL.CQMSConstructSolutionApproveService.getCurrApproveForApi(id, userId, edition);
            res.successful = true;
            res.resultValue = BeanUtil.CopyOjbect<Solution_CQMSConstructSolutionApprove>(approve, true);
            if (approve != null)
            {
                res.resultValue.Edition = approve.Edition;
            }
            return res;
        }
        /// <summary>
        /// 当前版次 会签人
        /// </summary>
        /// <param name="id"></param>
        /// <param name="edition"></param>
        /// <returns></returns>
        public ResponseData<List<Solution_CQMSConstructSolutionApprove>> GetConApproveById(string id, int edition)
        {
            ResponseData<List<Solution_CQMSConstructSolutionApprove>> res = new ResponseData<List<Solution_CQMSConstructSolutionApprove>>();
            res.successful = true;
            res.resultValue = BLL.CQMSConstructSolutionApproveService.getConApproveForApi(id, edition);
            return res;
        }
        [HttpPost]
        public ResponseData<string> AddApprove([FromBody]Model.Solution_CQMSConstructSolutionApprove approve)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {
                Model.Solution_CQMSConstructSolution CheckControl = new Model.Solution_CQMSConstructSolution();
                CheckControl.ConstructSolutionId = approve.ConstructSolutionId;
                CheckControl.State = approve.ApproveType;
                BLL.CQMSConstructSolutionService.UpdateConstructSolutionForApi(CheckControl);
                BLL.CQMSConstructSolutionApproveService.AddConstructSolutionApprove(approve);
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
        public ResponseData<string> UpdateApprove([FromBody]Solution_CQMSConstructSolutionApprove approve)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {
                // Model.Check_CheckControlApprove approve1 = BLL.CheckControlApproveService.GetCheckControlApproveByCheckControlId(approve.CheckControlCode);
                approve.ApproveDate = DateTime.Now;
                approve = BLL.CQMSConstructSolutionApproveService.UpdateConstructSolutionApproveForApi(approve);
                var approves = BLL.CQMSConstructSolutionApproveService.getConApprovesForApi(approve.ConstructSolutionId, approve.Edition.Value);
                Solution_CQMSConstructSolutionApprove first = null;
                bool allAgree = true;
                bool hasFinish = true;
                foreach (Solution_CQMSConstructSolutionApprove a in approves)
                {
                    if (a.ApproveType == "1")
                    {
                        first = a;
                    }
                    if (a.ApproveType == "2")
                    {
                        approve.ConstructSolutionId = a.ConstructSolutionId;
                        if (a.IsAgree.HasValue && !a.IsAgree.Value)
                        {
                            allAgree = false;
                        }
                        if (!a.IsAgree.HasValue)
                        {
                            hasFinish = false;
                        }
                    }
                }

                if (!allAgree)
                {
                    if (first != null && hasFinish)
                    {
                        Solution_CQMSConstructSolutionApprove approveReEdit = new Solution_CQMSConstructSolutionApprove();
                        approveReEdit.ApproveType = "0";
                        approveReEdit.ApproveMan = first.ApproveMan.Split('$')[0];
                        approveReEdit.ConstructSolutionId = first.ConstructSolutionId;
                        approveReEdit.Edition = first.Edition;
                        BLL.CQMSConstructSolutionApproveService.AddConstructSolutionApproveForApi(approveReEdit);
                        Model.Solution_CQMSConstructSolution CheckControl = new Model.Solution_CQMSConstructSolution();
                        CheckControl.ConstructSolutionId = approveReEdit.ConstructSolutionId;
                        CheckControl.State = approveReEdit.ApproveType;
                        BLL.CQMSConstructSolutionService.UpdateConstructSolutionForApi(CheckControl);
                    }
                }
                else
                {
                    if (hasFinish)
                    {
                        Solution_CQMSConstructSolutionApprove approveReEdit = new Solution_CQMSConstructSolutionApprove();
                        approveReEdit.ApproveType = "3";
                        approveReEdit.ConstructSolutionId = approve.ConstructSolutionId;
                        approveReEdit.Edition = approve.Edition;
                        BLL.CQMSConstructSolutionApproveService.AddConstructSolutionApproveForApi(approveReEdit);
                        Model.Solution_CQMSConstructSolution CheckControl = new Model.Solution_CQMSConstructSolution();
                        CheckControl.ConstructSolutionId = approveReEdit.ConstructSolutionId;
                        CheckControl.State = approveReEdit.ApproveType;
                        BLL.CQMSConstructSolutionService.UpdateConstructSolutionForApi(CheckControl);
                    }
                }
                if (first != null)
                {
                    Solution_CQMSConstructSolutionApprove approveS = new Solution_CQMSConstructSolutionApprove();
                    approveS.ApproveType = "S";
                    approveS.ApproveMan = first.ApproveMan.Split('$')[0];
                    approveS.ConstructSolutionId = first.ConstructSolutionId;
                    approveS.Edition = first.Edition;
                    BLL.CQMSConstructSolutionApproveService.AddConstructSolutionApproveForApi(approveS);
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
        [HttpGet]
        public ResponseData<string> see(string dataId, string userId)
        {
            ResponseData<string> res = new ResponseData<string>();
            res.successful = true;
            BLL.CQMSConstructSolutionApproveService.See(dataId, userId);
            return res;
        }
    }
}
