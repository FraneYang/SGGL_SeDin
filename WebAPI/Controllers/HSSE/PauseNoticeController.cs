using BLL;
using System;
using System.Linq;
using System.Web.Http;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 工程暂停令
    /// </summary>
    public class PauseNoticeController : ApiController
    {
        #region 根据主键ID获取工程暂停令
        /// <summary>
        ///  根据主键ID获取工程暂停令
        /// </summary>
        /// <param name="PauseNoticeId"></param>
        /// <returns></returns>
        public Model.ResponeData getPauseNoticeById(string PauseNoticeId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                 responeData.data = APIPauseNoticeService.getPauseNoticeById(PauseNoticeId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据projectId、unitid获取工程暂停令信息-查询
        /// <summary>
        /// 根据projectId、unitid获取工程暂停令信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId">单位ID</param>
        ///  <param name="strParam">查询条件</param>
        ///  <param name="states">状态（0：全部；1：待确认；2：已确认）</param>
        /// <param name="pageIndex"></param>  
        /// <returns></returns>
        public Model.ResponeData getPauseNoticeList(string projectId, string unitId, string strParam, string states, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIPauseNoticeService.getPauseNoticeList(projectId, unitId, strParam, states);
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

        #region 根据projectId获取各状态风险数
        /// <summary>
        /// 根据projectId获取各状态风险数
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public Model.ResponeData getPauseNoticeCount(string projectId, string unitId, string strParam)
        {
            var responeData = new Model.ResponeData();
            try
            {
                //总数  0待提交；1待签发；2待批准；3待接收；4已闭环
                var getDataList = new Model.SGGLDB(Funs.ConnString).Check_PauseNotice.Where(x => x.ProjectId == projectId && (x.UnitId == unitId || unitId == null));
                if (!string.IsNullOrEmpty(strParam))
                {
                    getDataList = getDataList.Where(x => x.PauseNoticeCode.Contains(strParam) || x.WrongContent.Contains(strParam) || x.PauseContent.Contains(strParam));
                }
                int tatalCount = getDataList.Count();
                //待提交 0
                int count0 = getDataList.Where(x => x.States == "0").Count();
                //待签发 1
                int count1 = getDataList.Where(x => x.States == "1").Count();
                //待批准 2
                int count2 = getDataList.Where(x => x.States == "2").Count();
                //待接收 3
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

        #region 保存工程暂停令 Check_PauseNotice
        /// <summary>
        /// 保存工程暂停令 Check_PauseNotice
        /// </summary>
        /// <param name="newItem">工程暂停令</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SavePauseNotice([FromBody] Model.PauseNoticeItem newItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APIPauseNoticeService.SavePauseNotice(newItem);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion        

        #region 暂停令通知单附件
        /// <summary>
        ///  上报处罚单-回执单
        /// </summary>
        /// <param name="pauseNoticeId">主键</param>
        /// <param name="attachUrl">通知单路径</param>
        /// <returns></returns>
        public Model.ResponeData getSavePauseNoticeReceiptUrl(string pauseNoticeId, string attachUrl)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APIPauseNoticeService.SavePauseNoticeUrl(pauseNoticeId, attachUrl);
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
