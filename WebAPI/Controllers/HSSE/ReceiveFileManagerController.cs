using BLL;
using System;
using System.Linq;
using System.Web.Http;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 来文信息管理
    /// </summary>
    public class ReceiveFileManagerController : ApiController
    {
        #region 根据主键ID获取来文信息管理
        /// <summary>
        ///  根据主键ID获取来文信息管理
        /// </summary>
        /// <param name="receiveFileManagerId">来文ID</param>
        /// <param name="fileType">来文类型(0-项目发文；1-公司来文)</param>
        /// <returns></returns>
        public Model.ResponeData getReceiveFileManagerById(string receiveFileManagerId, string fileType)
        {
            var responeData = new Model.ResponeData();
            try
            {
                 responeData.data = APIReceiveFileManagerService.getReceiveFileManagerById(receiveFileManagerId,fileType);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据projectId、unitid获取来文信息管理信息-查询
        /// <summary>
        /// 根据projectId、unitid获取来文信息管理信息
        /// </summary>       
        /// <param name="projectId"></param>
        /// <param name="fileType">来文类型(0-项目发文；1-公司来文)</param>
        /// <param name="unitId">单位ID</param>
        /// <param name="states">状态（0待提交；1-已提交；2：已回复）</param>
        /// <param name="pageIndex"></param>  
        /// <returns></returns>
        public Model.ResponeData getReceiveFileManagerList(string projectId, string fileType, string unitId, string states, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIReceiveFileManagerService.getReceiveFileManagerList(projectId, fileType, unitId, states);
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

        #region 保存来文信息管理ReceiveFileManager
        /// <summary>
        /// 保存来文信息管理ReceiveFileManager 
        /// </summary>
        /// <param name="newItem">来文信息管理</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveReceiveFileManager([FromBody] Model.ReceiveFileManagerItem newItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APIReceiveFileManagerService.SaveReceiveFileManager(newItem);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion        

        #region 来文信息回复单附件
        /// <summary>
        ///  上报处罚单-回执单
        /// </summary>
        /// <param name="receiveFileManagerId">主键</param>
        /// <param name="attachUrl">回复单路径</param>
        /// <returns></returns>
        public Model.ResponeData getSaveReplyFileAttachUrl(string receiveFileManagerId, string attachUrl)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APIReceiveFileManagerService.SaveReplyFileAttachUrl(receiveFileManagerId, attachUrl);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 公司来文下发
        /// <summary>
        ///  根据主键ID获取来文信息管理
        /// </summary>
        /// <param name="receiveFileManagerId">来文ID</param>
        /// <returns></returns>
        public Model.ResponeData getIssueReceiveFileManagerById(string receiveFileManagerId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getInfo = ReceiveFileManagerService.GetReceiveFileManagerById(receiveFileManagerId);
                if (getInfo != null && getInfo.FileType == "1")
                {
                    var getF = new Model.SGGLDB(Funs.ConnString).InformationProject_ReceiveFileManager.FirstOrDefault(x => x.FromId == getInfo.ReceiveFileManagerId);
                    if (getF == null)
                    {
                        ReceiveFileManagerService.IssueReceiveFile(getInfo.ReceiveFileManagerId);
                    }
                    else
                    {
                        responeData.code = 2;
                        responeData.message = "公司来文已下发到项目来文！";
                    }
                }
                else
                {
                    responeData.code = 2;
                    responeData.message = "非公司文件不能下发！";
                }

                responeData.data = new { receiveFileManagerId };
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
