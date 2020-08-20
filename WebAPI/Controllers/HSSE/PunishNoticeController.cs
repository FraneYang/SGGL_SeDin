using BLL;
using System;
using System.Linq;
using System.Web.Http;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 处罚通知单
    /// </summary>
    public class PunishNoticeController : ApiController
    {
        #region 根据主键ID获取处罚通知单
        /// <summary>
        ///  根据主键ID获取处罚通知单
        /// </summary>
        /// <param name="punishNoticeId"></param>
        /// <returns></returns>
        public Model.ResponeData getPunishNoticeById(string punishNoticeId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIPunishNoticeService.getPunishNoticeById(punishNoticeId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据projectId、unitid获取处罚通知单信息-查询
        /// <summary>
        /// 根据projectId、unitid获取处罚通知单信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        ///  <param name="strParam">查询条件</param>
        ///  <param name="states">处罚单状态（0待提交；1待签发；2待批准；3待回执；4已闭环）</param>
        /// <param name="pageIndex"></param>  
        /// <returns></returns>
        public Model.ResponeData getPunishNoticeList(string projectId, string unitId, string strParam, string states, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIPunishNoticeService.getPunishNoticeList(projectId, unitId, strParam, states);
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

        #region 根据projectId获取各状处罚通知单数量
        /// <summary>
        /// 根据projectId获取各状处罚通知单数量
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public Model.ResponeData getPunishNoticeCount(string projectId, string unitId, string strParam)
        {
            var responeData = new Model.ResponeData();
            try
            {
                //总数  0待提交；1待签发；2待批准；3待回执；4已闭环
                var getDataList = Funs.DB.Check_PunishNotice.Where(x => x.ProjectId == projectId && (x.UnitId == unitId || unitId == null));
                if (!string.IsNullOrEmpty(strParam))
                {
                    getDataList = getDataList.Where(x => x.PunishNoticeCode.Contains(strParam) || x.IncentiveReason.Contains(strParam));
                }
                int tatalCount = getDataList.Count();
                //待提交 0
                int count0 = getDataList.Where(x => x.States == "0").Count();
                //待签发 1
                int count1 = getDataList.Where(x => x.States == "1").Count();
                //待批准 2
                int count2 = getDataList.Where(x => x.States == "2").Count();
                //待回执 3
                int count3 = getDataList.Where(x => x.States == "3").Count();
                //已闭环 4
                int count4 = getDataList.Where(x => x.States == "4").Count();    
                responeData.data = new { tatalCount, count0, count1, count2, count3, count4 };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 保存处罚通知单 Check_PunishNotice
        /// <summary>
        /// 保存处罚通知单 Check_PunishNotice
        /// </summary>
        /// <param name="newItem">处罚通知单</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SavePunishNotice([FromBody] Model.PunishNoticeItem newItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APIPunishNoticeService.SavePunishNotice(newItem);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 处罚单-附件通知单 回执单
        /// <summary>
        ///  上报处罚单-回执单
        /// </summary>
        /// <param name="punishNoticeId">主键</param>
        /// <param name="attachUrl">回执单路径</param>
        /// /// <param name="type">类型：0-通知单；1-回执单</param>
        /// <returns></returns>
        public Model.ResponeData getSavePunishNoticeReceiptUrl(string punishNoticeId, string attachUrl, string type = null)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APIPunishNoticeService.SavePunishNoticeReceiptUrl(punishNoticeId, attachUrl, type);
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
