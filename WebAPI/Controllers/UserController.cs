using BLL;
using System;
using System.Linq;
using System.Web.Http;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserController : ApiController
    {
        #region 根据账号或手机号码登录方法
        /// <summary>
        /// 登录方法
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData postLoginOn([FromBody] Model.UserItem userInfo)
        {
            //登录方法 Model.UserItem
            var responeData = new Model.ResponeData
            {
                message = "账号密码不匹配！"
            };
            try
            {
                var user = APIUserService.UserLogOn(userInfo);
                if (user != null)
                {
                    responeData.message = "登录成功！";
                    responeData.data = user;
                }
                else
                {
                    var user1 = APIPersonService.PersonLogOn(userInfo);
                    if (user1 != null)
                    {
                        responeData.message = "登录成功！";
                        responeData.code = 2;
                        responeData.data = user1;
                    }
                }                
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据userid获取用户信息
        /// <summary>
        /// 根据userid获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Model.ResponeData getUserByUserId(string userId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIUserService.getUserByUserId(userId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据unitId获取用户信息
        /// <summary>
        /// 根据unitId获取用户信息
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public Model.ResponeData getUserByUnitid(string unitId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIUserService.getUserByUnitId(unitId, null);
                responeData.data = new { getDataList.Count, getDataList };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据unitId获取用户信息
        /// <summary>
        /// 根据unitId获取用户信息
        /// </summary>
        /// <param name="unitId">单位ID</param>
        /// <param name="strParam">查询</param>
        /// <returns></returns>
        public Model.ResponeData getUserByUnitidQuery(string unitId, string strParam)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIUserService.getUserByUnitId(unitId, strParam);
                responeData.data = new { getDataList.Count, getDataList };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据projectId、unitid获取用户信息
        /// <summary>
        /// 根据projectId、unitid获取用户信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public Model.ResponeData getUserByProjectIdUnitId(string projectId, string unitId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIUserService.getUserByProjectIdUnitIdQuery(projectId, unitId, null, null);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 根据projectId、unitid获取用户信息
        /// <summary>
        /// 根据projectId、unitid获取用户信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="roleIds"></param>
        /// <param name="strParam"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public Model.ResponeData getUserByProjectIdUnitIdQuery(string projectId, string unitId, string roleIds, string strParam, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIUserService.getUserByProjectIdUnitIdQuery(projectId, unitId, roleIds, strParam);
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

        #region 根据UnitType单位类型获取用户信息
        /// <summary>
        /// 根据UnitType单位类型获取用户信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitType">（总包1;施工分包2;监理3;业主4;其他5）</param>
        /// <param name="roleIds"></param>
        /// <param name="strParam"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public Model.ResponeData getUserByProjectIdUnitTypeQuery(string projectId, string unitType, string roleIds, string strParam, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIUserService.getUserByProjectIdUnitTypeQuery(projectId, unitType, roleIds, strParam);
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

        #region 保存人员电话号码
        /// <summary>
        /// 保存人员电话号码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tel">电话</param>
        /// <returns></returns>
        public Model.ResponeData getSaveUserTel(string userId, string tel)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APIUserService.getSaveUserTel(userId, tel);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 保存人员签名
        /// <summary>
        /// 保存人员电话号码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="signatureUrl">签名</param>
        /// <returns></returns>
        public Model.ResponeData getSaveUserSignatureUrl(string userId, string signatureUrl)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APIUserService.getSaveUserSignatureUrl(userId, signatureUrl);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据用户UnitId判断是否为本单位用户或管理员
        /// <summary>
        /// 根据用户UnitId判断是否为本单位用户或管理员
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Model.ResponeData getIsMainUnitOrAdmin(string userId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data= CommonService.IsMainUnitOrAdmin(userId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 获取当前用户是否有菜单权限
        /// <summary>
        /// 获取当前用户是否有菜单权限
        /// </summary>
        /// <param name="userId">当前人ID</param>
        /// <param name="menuId">菜单ID</param>
        /// <param name="projectId">项目ID</param>
        /// <returns></returns>
        public Model.ResponeData getIsHaveMenuPower(string userId, string menuId, string projectId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = CommonService.ReturnMenuByUserIdMenuId( userId,  menuId,  projectId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 获取当前用户系统权限集合
        /// <summary>
        /// 获取当前用户是否有菜单权限
        /// </summary>
        /// <param name="userId">当前人ID</param>
        /// <returns></returns>
        public Model.ResponeData getSystemPowerList(string userId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = CommonService.GetSystemPowerList(userId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 获取当前用户菜单权限集合
        /// <summary>
        /// 获取当前用户是否有菜单权限
        /// </summary>
        /// <param name="userId">当前人ID</param>
        /// <param name="projectId">项目ID</param>
        /// <returns></returns>
        public Model.ResponeData getMenuPowerList(string userId,  string projectId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = CommonService.GetAllMenuList(projectId, userId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 获取当前用户是否有按钮权限
        /// <summary>
        /// 获取当前用户是否有菜单权限
        /// </summary>
        /// <param name="userId">当前人ID</param>
        /// <param name="menuId">菜单ID</param>
        /// <param name="projectId">项目ID</param>
        /// <param name="buttonName">按钮名称</param>
        /// <returns></returns>
        public Model.ResponeData getIsHaveButtonPower(string userId, string menuId, string projectId,string buttonName)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = CommonService.GetAllButtonPowerList(projectId, userId, menuId, buttonName);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 获取当前人未读数量
        /// <summary>
        /// 获取当前人未读数量
        /// </summary>
        /// <param name="menuId">菜单ID</param>
        /// <param name="projectId">菜单ID</param>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public Model.ResponeData getMenuUnreadCount(string menuId, string projectId, string userId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIUserService.getMenuUnreadCount(menuId, projectId, userId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 保存浏览记录
        /// <summary>
        /// 保存浏览记录
        /// </summary>
        /// <param name="menuId">菜单ID</param>
        /// <param name="projectId">菜单ID</param>
        /// <param name="userId">用户id</param>
        /// <param name="dataId">主键ID</param>
        /// <returns></returns>
        public Model.ResponeData getSaveUserRead(string menuId, string projectId, string userId, string dataId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APIUserService.getSaveUserRead(menuId, projectId, userId, dataId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据人员ID获取下一步办理人
        /// <summary>
        /// 根据人员ID获取下一步办理人
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="userIds"></param>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public Model.ResponeData getUserByProjectIdUserIdsQuery(string projectId, string userIds, string strParam)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIUserService.getUserByProjectIdUserIdsQuery(projectId, userIds, strParam);
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
