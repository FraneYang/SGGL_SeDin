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
    /// 奖励通知单
    /// </summary>
    public class IncentiveNoticeController : ApiController
    {
        #region 根据主键ID获取奖励通知单
        /// <summary>
        ///  根据主键ID获取奖励通知单
        /// </summary>
        /// <param name="incentiveNoticeId"></param>
        /// <returns></returns>
        public Model.ResponeData getIncentiveNoticeById(string incentiveNoticeId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                 responeData.data = APIIncentiveNoticeService.getIncentiveNoticeById(incentiveNoticeId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据projectId、unitid获取奖励通知单信息-查询
        /// <summary>
        /// 根据projectId、unitid获取奖励通知单信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        ///  <param name="strParam">查询条件</param>
        ///  <param name="states">状态0 待提交1 待签发2 待批准3 已完成</param>
        /// <param name="pageIndex"></param>  
        /// <returns></returns>
        public Model.ResponeData getIncentiveNoticeList(string projectId, string unitId, string strParam, string states, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIIncentiveNoticeService.getIncentiveNoticeList(projectId, unitId, strParam, states);
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

        #region 根据projectId获取各状奖励通知单数量
        /// <summary>
        /// 根据projectId获取各状奖励通知单数量
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public Model.ResponeData getIncentiveNoticeCount(string projectId, string unitId, string strParam)
        {
            var responeData = new Model.ResponeData();
            try
            {
                //总数  0待提交；1待签发；2待批准；3已完成
                var getDataList = Funs.DB.Check_IncentiveNotice.Where(x => x.ProjectId == projectId && (x.UnitId == unitId || unitId == null));
                if (!string.IsNullOrEmpty(strParam))
                {
                    getDataList = getDataList.Where(x => x.IncentiveNoticeCode.Contains(strParam) || x.BasicItem.Contains(strParam));
                }
                int tatalCount = getDataList.Count();
                //待提交 0
                int count0 = getDataList.Where(x => x.States == "0").Count();
                //待签发 1
                int count1 = getDataList.Where(x => x.States == "1").Count();
                //待批准 2
                int count2 = getDataList.Where(x => x.States == "2").Count();
                //已完成 3
                int count3 = getDataList.Where(x => x.States == "3").Count();
                responeData.data = new { tatalCount, count0, count1, count2, count3 };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 保存奖励通知单 Check_IncentiveNotice
        /// <summary>
        /// 保存奖励通知单 Check_IncentiveNotice
        /// </summary>
        /// <param name="newItem">奖励通知单</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveIncentiveNotice([FromBody] Model.IncentiveNoticeItem newItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APIIncentiveNoticeService.SaveIncentiveNotice(newItem);
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
