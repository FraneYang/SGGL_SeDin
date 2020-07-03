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
    /// 点口，委托，无损检测
    /// </summary>
    public class NDETrustController : ApiController
    {
        /// <summary>
        /// 根据单位工程、点口批号、批开始时间获取还未进行委托的批列表
        /// </summary>
        /// <param name="unitWorkId"></param>
        /// <param name="pointBatchCode"></param>
        /// <param name="startDate"></param>
        /// <returns></returns>
        public Model.ResponeData getPointBatchCode(string unitWorkId, string pointBatchCode, DateTime? startDate)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APINDETrustService.getPointBatchCode(unitWorkId, pointBatchCode, startDate);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        /// <summary>
        /// 根据点口批ID获取已点口还未审批的焊口
        /// </summary>
        /// <param name="pointBatchId"></param>
        /// <returns></returns>
        public Model.ResponeData getPointWeldJoint(string pointBatchId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APINDETrustService.getPointWeldJoint(pointBatchId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        /// <summary>
        /// 根据点口批ID和焊口ID获取待调整焊口
        /// </summary>
        /// <param name="pointBatchId"></param>
        /// <param name="weldJointId"></param>
        /// <returns></returns>
        public Model.ResponeData getPointWeldJoint(string pointBatchId, string weldJointId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APINDETrustService.getPointWeldJoint(pointBatchId, weldJointId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        /// <summary>
        /// 根据单位工程获取所有已点的焊口列表
        /// </summary>
        /// <param name="unitWorkId"></param>
        /// <returns></returns>
        public Model.ResponeData GetPointWeldJointList(string unitWorkId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APINDETrustService.GetPointWeldJointList(unitWorkId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        /// <summary>
        /// 点口调整 
        /// </summary>
        /// <param name="oldJointId"></param>
        /// <param name="newJointId"></param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData RePointSave([FromBody] string oldJointId, [FromBody] string newJointId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APINDETrustService.RePointSave(oldJointId, newJointId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
    }
}