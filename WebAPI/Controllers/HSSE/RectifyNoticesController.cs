using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 隐患整改单
    /// </summary>
    public class RectifyNoticesController : ApiController
    {
        #region 根据Id获取隐患整改单
        /// <summary>
        /// 根据Id获取隐患整改单
        /// </summary>
        /// <param name="rectifyNoticesId"></param>
        /// <returns></returns>
        public Model.ResponeData getRectifyNoticesById(string rectifyNoticesId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                 responeData.data = BLL.APIRectifyNoticesService.getRectifyNoticesById(rectifyNoticesId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据projectId、states获取隐患整改单
        /// <summary>
        /// 根据projectId、states获取隐患整改单
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="states">状态 0待提交；1待签发；2待整改；3待审核；4待复查；5已完成</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public Model.ResponeData getRectifyNoticesByProjectIdStates(string projectId, string states, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                List<Model.RectifyNoticesItem> getDataList = new List<Model.RectifyNoticesItem>();                   
                int pageCount = Funs.DB.Check_RectifyNotices.Count(x => x.ProjectId == projectId && (x.States == states || states == null));
                if (pageCount > 0 && pageIndex > 0)
                {
                    getDataList = BLL.APIRectifyNoticesService.getRectifyNoticesByProjectIdStates(projectId, states, pageIndex);
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

        #region 根据projectId获取各状态风险数
        /// <summary>
        /// 根据projectId获取各状态风险数
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Model.ResponeData getRectifyNoticesCount(string projectId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                //总数  0待提交；1待签发；2待整改；3待审核；4待复查；5已完成
                var getDataList = Funs.DB.Check_RectifyNotices.Where(x => x.ProjectId == projectId);
                int tatalCount = getDataList.Count();
                //待提交 0
                int count0 = getDataList.Where(x => x.States == "0").Count();
                //待签发 1
                int count1 = getDataList.Where(x => x.States == "1").Count();
                //待整改 2
                int count2 = getDataList.Where(x => x.States == "2").Count();
                //待审核 3
                int count3 = getDataList.Where(x => x.States == "3").Count();
                //待复查 4
                int count4 = getDataList.Where(x => x.States == "4").Count();
                //已完成 5
                int count5 = getDataList.Where(x => x.States == "5").Count();
                responeData.data = new { tatalCount, count0, count1, count2, count3, count4, count5 };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 保存RectifyNotices
        /// <summary>
        /// 保存RectifyNotices
        /// </summary>
        /// <param name="rectifyNotices"></param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveRectifyNotices([FromBody] Model.RectifyNoticesItem rectifyNotices)
        {
            var responeData = new Model.ResponeData();
            try
            {
                BLL.APIRectifyNoticesService.SaveRectifyNotices(rectifyNotices);
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
