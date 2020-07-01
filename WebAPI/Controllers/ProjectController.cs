using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 项目
    /// </summary>
    public class ProjectController : ApiController
    {
        #region 根据userid获取用户参与项目
        /// <summary>
        /// 根据userid获取用户参与项目
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Model.ResponeData getProjectsByUserId(string userId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = BLL.APIProjectService.geProjectsByUserId(userId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据userid获取用户参与项目
        /// <summary>
        /// 根据userid获取用户参与项目
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Model.ResponeData getALLProjectsByUserId(string userId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = BLL.APIProjectService.getALLProjectsByUserId(userId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据projectId获取项目信息
        /// <summary>
        /// 根据projectId获取项目信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Model.ResponeData getProjectByProjectId(string projectId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = BLL.APIProjectService.getProjectByProjectId(projectId);
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
