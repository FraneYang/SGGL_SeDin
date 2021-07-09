using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 作业票控制器
    /// </summary>
    public class LicenseController : ApiController
    {
        #region 获取作业票列表
        /// <summary>
        /// 获取作业票列表
        /// </summary>
        /// <param name="strMenuId">菜单ID</param>
        /// <param name="projectId">项目ID</param>
        /// <param name="unitId">单位ID</param>
        /// <param name="states">状态</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public Model.ResponeData getLicenseDataList(string strMenuId, string projectId, string unitId, string states, int pageIndex)
        {
            var responeData = new Model.ResponeData();            
            try
            {
                var getDataList = APILicenseDataService.getLicenseDataList(strMenuId, projectId, unitId, states, pageIndex);
                int pageCount = APILicenseDataService.getLicenseDataCount;
                responeData.data = new { pageCount, getDataList };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 获取作业票列表-按状态
        /// <summary>
        /// 获取作业票列表-按状态
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="unitId">单位ID</param>
        /// <param name="states">状态(0-待提交；1-审核中；2-作业中；3-已关闭；-1已取消；-2作废)</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public Model.ResponeData getLicenseDataListByStates(string projectId, string unitId, string states, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APILicenseDataService.getLicenseDataListByStates( projectId, unitId, states, pageIndex);
                int pageCount = APILicenseDataService.getLicenseDataStatesCount;
                responeData.data = new { pageCount, getDataList };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 根据ID获取作业票详细
        /// <summary>
        ///  根据ID获取作业票详细
        /// </summary>
        /// <param name="strMenuId">菜单ID</param>
        /// <param name="id">作业票ID</param>
        /// <param name="userId">当前人ID</param>
        /// <returns></returns>
        public Model.ResponeData getLicenseDataById(string strMenuId, string id, string userId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                Model.LicenseDataItem getInfo = new Model.LicenseDataItem();
                if (!string.IsNullOrEmpty(id))
                {
                    getInfo = APILicenseDataService.getLicenseDataById(strMenuId, id);
                }

                var getLicenseFlowOperate = APILicenseDataService.getLicenseFlowOperate(id, userId);
                var getNextLicenseFlowOperate = APILicenseDataService.getNextLicenseFlowOperate(strMenuId, getInfo, getLicenseFlowOperate);
                responeData.data = new { getInfo, getLicenseFlowOperate, getNextLicenseFlowOperate };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 获取作业票安全措施列表信息
        /// <summary>
        ///  获取作业票安全措施列表信息
        /// </summary>
        /// <param name="dataId">作业票ID</param>
        /// <returns></returns>
        public Model.ResponeData getLicenseLicenseItemList(string dataId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APILicenseDataService.getLicenseLicenseItemList(dataId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 获取作业票审核列表信息
        /// <summary>
        ///  获取作业票审核列表信息
        /// </summary>
        /// <param name="dataId">作业票ID</param>
        /// <returns></returns>
        public Model.ResponeData getLicenseFlowOperateList(string dataId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APILicenseDataService.getLicenseFlowOperateList(dataId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 保存作业票信息
        /// <summary>
        /// 保存作业票信息
        /// </summary>
        /// <param name="licenseDataItem">作业票记录</param>
        [HttpPost]
        public Model.ResponeData SaveLicenseData([FromBody] Model.LicenseDataItem licenseDataItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APILicenseDataService.SaveLicenseData(licenseDataItem);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
                ErrLogInfo.WriteLog(string.Empty, ex);
            }

            return responeData;
        }

        /// <summary>
        /// 保存当前审核记录
        /// </summary>
        /// <param name="flowOperateItem">当前审核记录</param>
        [HttpPost]
        public Model.ResponeData SaveLicenseFlowOperate([FromBody] Model.FlowOperateItem flowOperateItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APILicenseDataService.SaveLicenseFlowOperate(flowOperateItem);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
                ErrLogInfo.WriteLog(string.Empty, ex);
            }
            return responeData;
        }

        /// <summary>
        /// 保存下一步审核流程
        /// </summary>
        /// <param name="flowOperateItem">下一步审核</param>
        [HttpPost]
        public Model.ResponeData SaveNextLicenseFlowOperate([FromBody] Model.NextFlowOperateItem flowOperateItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                if (flowOperateItem != null && flowOperateItem.FlowOperateList.Count() > 0)
                {
                    APILicenseDataService.SaveNextLicenseFlowOperate(flowOperateItem.FlowOperateList);
                }
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
                ErrLogInfo.WriteLog(string.Empty, ex);
            }
            return responeData;
        }
        #endregion

        #region 删除作业票步骤
        /// <summary>
        ///  删除作业票步骤
        /// </summary>
        /// <param name="flowOperateId">审核步骤ID</param>
        /// <returns></returns>
        public Model.ResponeData getDeleteLicenseFlowOperate(string flowOperateId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APILicenseDataService.getDeleteLicenseFlowOperate(flowOperateId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 获取作业票审核步骤下步分支
        /// <summary>
        ///  获取作业票审核步骤下步分支
        /// </summary>
        /// <param name="flowOperateId">审核步骤ID</param>
        /// <returns></returns>
        public Model.ResponeData getNextLicenseFlowOperateGroupList(string flowOperateId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APILicenseDataService.getNextLicenseFlowOperateGroupList(flowOperateId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 重申请保存作业票信息
        /// <summary>
        /// 重申请保存作业票信息
        /// </summary>
        /// <param name="licenseDataItem">作业票记录</param>
        [HttpPost]
        public Model.ResponeData SaveLicenseDataReApply([FromBody] Model.LicenseDataItem licenseDataItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APILicenseDataService.SaveLicenseDataReApply(licenseDataItem);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
                ErrLogInfo.WriteLog(string.Empty, ex);
            }

            return responeData;
        }
        #endregion
    }
}
