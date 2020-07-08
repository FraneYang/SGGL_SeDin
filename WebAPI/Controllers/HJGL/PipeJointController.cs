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
    /// 
    /// </summary>
    public class PipeJointController : ApiController
    {
        /// <summary>
        /// 根据单位工程获取管线列表
        /// </summary>
        /// <param name="unitWrokId"></param>
        /// <returns></returns>
        public Model.ResponeData getPipelineListByUnitWrokId(string unitWrokId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIPipeJointService.getPipelineList(unitWrokId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        /// <summary>
        /// 根据管线信息获取焊口列表
        /// </summary>
        /// <param name="pipeLineId"></param>
        /// <returns></returns>
        public Model.ResponeData getWeldJointListByPipeLineId(string pipeLineId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIPipeJointService.getWeldJointList(pipeLineId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        /// <summary>
        /// 获取焊工列表
        /// </summary>
        /// <param name="unitWorkId"></param>
        /// <returns></returns>
        public Model.ResponeData getWelderListByUnitWorkId(string unitWorkId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIPipeJointService.getWelderList(unitWorkId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        /// <summary>
        /// 根据焊口ID获取焊口信息
        /// </summary>
        /// <param name="weldJointId"></param>
        /// <returns></returns>
        public Model.ResponeData getWeldJointInfoById(string weldJointId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIPipeJointService.getWeldJointInfo(weldJointId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }


        #region 根据焊口标识获取焊口详细信息
        /// <summary>
        /// 根据焊口标识获取焊口详细信息
        /// </summary>
        /// <param name="weldJointIdentify"></param>
        /// <returns></returns>
        public Model.ResponeData getWeldJointByIdentify(string weldJointIdentify)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIPipeJointService.getWeldJointByIdentify(weldJointIdentify);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 保存管线焊口信息
        /// <summary>
        /// 保存管线焊口信息
        /// </summary>
        /// <param name="addItem">管线焊口项</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SavePipeWeldJoint([FromBody] Model.WeldJointItem addItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APIPipeJointService.SavePipeWeldJoint(addItem);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 批量保存管线焊口信息
        /// <summary>
        /// 批量保存管线焊口信息
        /// </summary>
        /// <param name="addItems">管线焊口项</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SavePipeWeldJointList([FromBody] List<Model.WeldJointItem> addItems)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APIPipeJointService.SavePipeWeldJointList(addItems);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 保存预焊接日报
        /// <summary>
        /// 保存预焊接日报
        /// </summary>
        /// <param name="addItem"></param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData PostSavePreWeldingDaily([FromBody] Model.WeldJointItem addItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APIPipeJointService.SavePreWeldingDaily(addItem);
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