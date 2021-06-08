using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;
using Model;

namespace WebAPI.Controllers
{
    /// <summary>
    ///试压包
    /// </summary>
    public class TestPackageController : ApiController
    {
        #region 获取试压包号
        /// <summary>
        /// 获取试压包号
        /// </summary>
        /// <param name="unitWorkId">单位工程</param>
        /// <param name="isFinish">是否完成</param>
        /// <param name="testPackageNo">试压包号</param>
        /// <returns></returns>
        public Model.ResponeData getTestPackageNo(string unitWorkId, bool isFinish, string testPackageNo)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APITestPackageService.getTestPackageNo(unitWorkId, isFinish, testPackageNo);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据试压包ID获取明细
        /// <summary>
        /// 根据试压包ID获取明细
        /// </summary>
        /// <param name="ptp_Id"></param>
        /// <returns></returns>
        public Model.ResponeData GetTestPackageDetail(string ptp_Id)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APITestPackageService.GetTestPackageDetail(ptp_Id);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 获取具备试压条件的试压包提醒
        /// <summary>
        /// 获取具备试压条件的试压包提醒
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Model.ResponeData GetCanTestPackageWarning(string projectId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APITestPackageService.GetCanTestPackageWarning(projectId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        #endregion

        #region 获取尾项检查试压包号
        /// <summary>
        /// 获取尾项检查试压包号
        /// </summary>
        /// <param name="unitWorkId">单位工程</param>
        /// <param name="isFinish">是否完成</param>
        /// <param name="testPackageNo">试压包号</param>
        /// <returns></returns>
        public Model.ResponeData getItemEndCheckTestPackageNo(string unitWorkId, string testPackageNo)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APITestPackageService.getItemEndCheckTestPackageNo(unitWorkId, testPackageNo);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region
        /// <summary>
        /// 获取尾项检查列表记录
        /// </summary>
        /// <param name="pTP_ID"></param>
        /// <param name="index"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public ResponseData<List<View_PTP_ItemEndCheckList>> getItemEndCheckList(string pTP_ID, int index, int page)
        {
            ResponseData<List<View_PTP_ItemEndCheckList>> res = new ResponseData<List<View_PTP_ItemEndCheckList>>();

            res.successful = true;
            res.resultValue = BLL.APITestPackageService.getItemEndCheckList(pTP_ID, index, page);
            return res;
        }
        #endregion

        #region 获取尾项检查管线号
        /// <summary>
        /// 获取尾项检查管线号
        /// </summary>
        /// <param name="pTP_ID">试压包Id</param>
        /// <returns></returns>
        public Model.ResponeData getItemEndCheckPipeline(string pTP_ID)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APITestPackageService.getItemEndCheckPipeline(pTP_ID);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据状态获取尾项检查办理步骤
        /// <summary>
        /// 根据状态获取尾项检查办理步骤
        /// </summary>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public Model.ResponeData getHandleType(string state)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APITestPackageService.getHandleType(state);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据状态获取尾项检查办理人员
        /// <summary>
        /// 根据状态获取尾项检查办理人员
        /// </summary>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public Model.ResponeData getHandleMan(string state, string projectId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APITestPackageService.getHandleMan(state, projectId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据选择的状态获取尾项检查办理人员
        /// <summary>
        /// 根据状态获取尾项检查办理人员
        /// </summary>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public Model.ResponeData getChangeStateHandleMan(string state, string projectId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APITestPackageService.getChangeStateHandleMan(state, projectId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 获取一个尾项检查记录
        /// <summary>
        /// 获取一个尾项检查记录
        /// </summary>
        /// <param name="itemEndCheckListId"></param>
        /// <returns></returns>
        [HttpGet]
        public ResponseData<View_PTP_ItemEndCheckList> GetItemEndCheckList(string itemEndCheckListId)
        {
            ResponseData<View_PTP_ItemEndCheckList> res = new ResponseData<View_PTP_ItemEndCheckList>();
            View_PTP_ItemEndCheckList jc = BLL.APITestPackageService.GetViewItemEndCheckList(itemEndCheckListId);
            res.successful = true;
            res.resultValue = BeanUtil.CopyOjbect<View_PTP_ItemEndCheckList>(jc, true);
            return res;
        }
        #endregion

        #region 获取尾项检查明细记录
        /// <summary>
        /// 获取尾项检查明细记录
        /// </summary>
        /// <param name="itemEndCheckListId"></param>
        /// <returns></returns>
        [HttpGet]
        public ResponseData<List<View_PTP_ItemEndCheck>> GetItemEndCheckDetail(string itemEndCheckListId)
        {
            ResponseData<List<View_PTP_ItemEndCheck>> res = new ResponseData<List<View_PTP_ItemEndCheck>>();
            res.successful = true;
            res.resultValue = BLL.APITestPackageService.getItemEndCheckDetail(itemEndCheckListId);
            return res;

        }
        #endregion

        #region 获取尾项检查审批记录
        /// <summary>
        /// 获取尾项检查审批记录
        /// </summary>
        /// <param name="itemEndCheckListId"></param>
        /// <returns></returns>
        [HttpGet]
        public ResponseData<List<View_PTP_TestPackageApprove>> GetTestPackageApprove(string itemEndCheckListId)
        {
            ResponseData<List<View_PTP_TestPackageApprove>> res = new ResponseData<List<View_PTP_TestPackageApprove>>();
            res.successful = true;
            res.resultValue = BLL.APITestPackageService.getTestPackageApprove(itemEndCheckListId);
            return res;

        }
        #endregion

        #region 获取尾项检查当前办理人Id
        /// <summary>
        /// 获取尾项检查当前办理人Id
        /// </summary>
        /// <param name="itemEndCheckListId"></param>
        /// <returns></returns>
        [HttpGet]
        public ResponseData<string> GetCurrAuditMan(string itemEndCheckListId)
        {
            ResponseData<string> res = new ResponseData<string>();
            res.successful = true;
            Model.PTP_TestPackageApprove approve1 = BLL.TestPackageApproveService.GetTestPackageApproveById(itemEndCheckListId);
            if (approve1 != null)
            {
                res.resultValue = approve1.ApproveMan;
            }
            return res;

        }
        #endregion

        #region 保存尾项检查记录
        /// <summary>
        /// 保存尾项检查记录
        /// </summary>
        /// <param name="ItemEndCheckList"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseData<string> AddItemEndCheckList([FromBody]Model.PTP_ItemEndCheckList ItemEndCheckList)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {
                if (string.IsNullOrEmpty(ItemEndCheckList.ItemEndCheckListId))
                {
                    ItemEndCheckList.ItemEndCheckListId = Guid.NewGuid().ToString();
                    BLL.ItemEndCheckListService.AddItemEndCheckList(ItemEndCheckList);
                    res.resultValue = ItemEndCheckList.ItemEndCheckListId;
                }
                else
                {
                    BLL.ItemEndCheckListService.UpdateItemEndCheckListForApi(ItemEndCheckList);
                    res.resultValue = ItemEndCheckList.ItemEndCheckListId;
                }
            }
            catch (Exception e)
            {
                res.successful = false;
            }
            res.successful = true;
            return res;
        }
        #endregion

        #region 保存尾项检查明细记录
        /// <summary>
        /// 保存尾项检查明细记录
        /// </summary>
        /// <param name="ItemEndCheck"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseData<string> AddItemEndCheckDetail([FromBody]Model.PTP_ItemEndCheck ItemEndCheck)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {
                if (string.IsNullOrEmpty(ItemEndCheck.ItemCheckId))
                {
                    ItemEndCheck.ItemCheckId = Guid.NewGuid().ToString();
                    BLL.AItemEndCheckService.AddAItemEndCheckForApi(ItemEndCheck);
                    res.resultValue = ItemEndCheck.ItemCheckId;
                    res.successful = true;
                }
                else
                {
                    BLL.AItemEndCheckService.UpdateAItemEndCheckForApi(ItemEndCheck);
                    res.resultValue = ItemEndCheck.ItemCheckId;
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
        #endregion

        #region 保存尾项检查审批记录
        /// <summary>
        /// 保存尾项检查审批记录
        /// </summary>
        /// <param name="approve"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseData<string> AddApprove([FromBody]Model.PTP_TestPackageApprove approve)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {
                var itemEndCheckList = BLL.APITestPackageService.GetItemEndCheckList(approve.ItemEndCheckListId);
                PTP_ItemEndCheckList iec = new PTP_ItemEndCheckList();
                iec.ItemEndCheckListId = itemEndCheckList.ItemEndCheckListId;
                iec.State = approve.ApproveType;
                BLL.ItemEndCheckListService.UpdateItemEndCheckListForApi(iec);
                res.resultValue = BLL.TestPackageApproveService.AddTestPackageApproveForApi(approve);

            }
            catch (Exception e)
            {
                res.successful = false;
                res.resultValue = e.Message;
            }
            res.successful = true;
            return res;

        }
        #endregion

        #region 更新尾项检查审批记录
        /// <summary>
        /// 更新尾项检查审批记录
        /// </summary>
        /// <param name="approve"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseData<string> UpdateApprove([FromBody]Model.PTP_TestPackageApprove approve)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {
                approve.ApproveDate = DateTime.Now;
                var resApprove = BLL.TestPackageApproveService.UpdateTestPackageApproveForApi(approve);
            }
            catch (Exception e)
            {
                res.successful = false;
            }
            res.successful = true;
            return res;
        }
        #endregion

        #region 保存尾项记录 ItemEndCheckList
        /// <summary>
        /// 保存尾项记录 ItemEndCheckList
        /// </summary>
        /// <param name="newItem">专项检查</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveItemEndCheckList([FromBody] Model.ItemEndCheckList newItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.message = APITestPackageService.SaveItemEndCheckList(newItem);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion
    }
}