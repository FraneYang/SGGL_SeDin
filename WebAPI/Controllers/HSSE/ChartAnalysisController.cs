using BLL;
using System;
using System.Linq;
using System.Web.Http;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 图型分信息
    /// </summary>
    public class ChartAnalysisController : ApiController
    {
        #region 根据类型获取图型数据
        /// <summary>
        /// 根据类型获取图型数据
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="type">1:问题类型;2:危害因素;3:作业内容;4:导致伤害/事故;5:单位工程;6:责任单位</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns></returns>
        public Model.ResponeData getChartAnalysisByType(string projectId, string type,string startDate,string endDate)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = BLL.APIChartAnalysisService.getChartAnalysisByType(projectId, type, startDate, endDate);
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
