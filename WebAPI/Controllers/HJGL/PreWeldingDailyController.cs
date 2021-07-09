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
    ///  焊接日报
    /// </summary>
    public class PreWeldingDailyController : ApiController
    {
        #region 根据主键获取详细
        /// <summary>
        ///  根据主键获取详细
        /// </summary>
        /// <param name="preWeldingDailyId"></param>
        /// <returns></returns>
        public Model.ResponeData getPreWeldingDailyInfo(string preWeldingDailyId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIPreWeldingDailyService.getPreWeldingDailyInfo(preWeldingDailyId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 列表-查询
        /// <summary>
        /// 列表-查询
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="pageIndex"></param>  
        /// <returns></returns>
        public Model.ResponeData getPreWeldingDailyList(string projectId, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIPreWeldingDailyService.getPreWeldingDailyList(projectId);
                int pageCount = getDataList.Count();
                if (pageCount > 0 && pageIndex > 0)
                {
                    getDataList = getDataList.Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize).ToList();
                }
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

        #region 获取焊接任务单记录
        /// <summary>
        /// 获取日报明细记录
        /// </summary>
        /// <param name="itemEndCheckListId"></param>
        /// <returns></returns>
        [HttpGet]
        public ResponseData<List<View_HJGL_WeldingTask>> GetWeldingTasks(string weldingDailyId, string unitWorkId, string date, string projectId)
        {
            ResponseData<List<View_HJGL_WeldingTask>> res = new ResponseData<List<View_HJGL_WeldingTask>>();
            res.successful = true;
            res.resultValue = BLL.APIPreWeldingDailyService.GetWeldingTasks(weldingDailyId, unitWorkId, date, projectId);
            return res;

        }
        #endregion

        #region 保存焊接日报
        /// <summary>
        /// 保存PreWeldingDaily
        /// </summary>
        /// <param name="newItem">施工方案</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SavePreWeldingDaily([FromBody] Model.HJGL_PreWeldingDailyItem newItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APIPreWeldingDailyService.SavePreWeldingDaily(newItem);
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
