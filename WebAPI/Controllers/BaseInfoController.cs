using BLL;
using System;
using System.Linq;
using System.Web.Http;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 基础数据信息
    /// </summary>
    public class BaseInfoController : ApiController
    {
        //#region 根据groupType获取检查类型
        ///// <summary>
        /////  根据groupType获取检查类型
        ///// </summary>
        ///// <param name="type"></param>
        ///// <returns></returns>
        //public Model.ResponeData getHazardRegisterTypes(string type)
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = BLL.APIBaseInfoService.getHazardRegisterTypes(type);
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }

        //    return responeData;
        //}
        //#endregion

        #region 根据projectId获取区域表
         /// <summary>
        ///  根据projectId获取区域表
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Model.ResponeData getProjectWorkArea(string projectId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = BLL.APIBaseInfoService.getProjectWorkArea(projectId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        //#region  获取专项检查类型及检查项【原】
        ///// <summary>
        /////   获取专项检查类型及检查项
        ///// </summary>
        ///// <param name="type">父级类型</param>
        ///// <returns></returns>
        //public Model.ResponeData getSpeciaCheckTypes(string type)
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getSpeciaCheckTypes(type);
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }

        //    return responeData;
        //}
        ///// <summary>
        /////   获取专项检查检查项内容明细
        ///// </summary>
        ///// <param name="type">检查项ID</param>
        ///// <returns></returns>
        //public Model.ResponeData getSpeciaCheckItemDetails(string type)
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getSpeciaCheckItemDetails(type);
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }

        //    return responeData;
        //}
        //#endregion

        //#region  获取项目专项检查类型及检查项
        ///// <summary>
        ///// 获取专项检查类型及检查项
        ///// </summary>
        ///// <param name="projectId">项目ID</param>
        ///// <param name="checkType">1-日常检查;2-专项检查;3-综合检查;4-开工前检查;5-季节性/节假日检查</param>
        ///// <param name="supCheckItem">上级菜单</param>
        ///// <returns></returns>
        //public Model.ResponeData getProjectCheckItemSet(string projectId, string checkType, string supCheckItem)
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getProjectCheckItemSet(projectId, checkType, supCheckItem);
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }

        //    return responeData;
        //}
        ///// <summary>
        /////   获取项目专项检查检查项内容明细
        ///// </summary>
        ///// <param name="type">检查项ID</param>
        ///// <returns></returns>
        //public Model.ResponeData getProjectCheckItemDetails(string type)
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getProjectCheckItemDetails(type);
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }

        //    return responeData;
        //}
        //#endregion

        //#region  获取专项检查处理措施
        ///// <summary>
        /////   获取专项检查处理措施
        ///// </summary>
        ///// <returns></returns>
        //public Model.ResponeData getHandleStep()
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getSysConst(ConstValue.Group_HandleStep);
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }
        //    return responeData;
        //}
        //#endregion

        //#region  获项目图片
        ///// <summary>
        /////  获项目图片
        ///// </summary>
        ///// <param name="projectId"></param>
        ///// <param name="pageIndex"></param>
        ///// <returns></returns>
        //public Model.ResponeData getProjectPictureByProjectId(string projectId, int pageIndex)
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        var getDataList = APIBaseInfoService.getProjectPictureByProjectId(projectId, null, null);
        //        int pageCount = getDataList.Count();
        //        if (pageCount > 0 && pageIndex > 0)
        //        {
        //            getDataList = getDataList.OrderByDescending(u => u.BaseInfoCode).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize).ToList();

        //        }
        //        responeData.data = new { pageCount, getDataList };
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }

        //    return responeData;
        //}

        ///// <summary>
        /////  根据类型获项目图片
        ///// </summary>
        ///// <param name="projectId"></param>
        ///// <param name="type"></param>
        ///// <returns></returns>
        //public Model.ResponeData getProjectPictureByProjectIdType(string projectId, string type)
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getProjectPictureByProjectId(projectId, type, null); ;
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }
        //    return responeData;
        //}
        ///// <summary>
        /////  根据类型获项目图片
        ///// </summary>
        ///// <param name="projectId"></param>
        ///// <param name="type"></param>
        ///// <param name="strParam"></param>
        ///// <param name="pageIndex"></param>
        ///// <returns></returns>
        //public Model.ResponeData getProjectPictureByProjectIdType(string projectId, string type, string strParam, int pageIndex)
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        var getDataList = APIBaseInfoService.getProjectPictureByProjectId(projectId, type, strParam);
        //        int pageCount = getDataList.Count();
        //        if (pageCount > 0 && pageIndex > 0)
        //        {
        //            getDataList = getDataList.OrderByDescending(u => u.BaseInfoCode).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize).ToList();

        //        }
        //        responeData.data = new { pageCount, getDataList };
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }

        //    return responeData;
        //}
        ///// <summary>
        /////   获项目图片
        ///// </summary>
        ///// <returns></returns>
        //public Model.ResponeData getProjectPicture()
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        var getLists = (from x in Funs.DB.InformationProject_Picture
        //                        join y in Funs.DB.AttachFile on x.PictureId equals y.ToKeyId
        //                        where x.States == Const.State_2 && y.AttachUrl != null
        //                        orderby x.UploadDate descending
        //                        select new Model.BaseInfoItem { BaseInfoId = x.PictureId, BaseInfoName = x.Title, ImageUrl = y.AttachUrl.Replace('\\', '/') }).Take(5).ToList();
        //        responeData.data = getLists;
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }

        //    return responeData;
        //}

        //#region 保存项目图片信息
        ///// <summary>
        ///// 保存项目图片信息
        ///// </summary>
        ///// <param name="picture">图片信息</param>
        ///// <returns></returns>
        //[HttpPost]
        //public Model.ResponeData SaveProjectPicture([FromBody] Model.PictureItem picture)
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        APIBaseInfoService.SaveProjectPicture(picture);
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }

        //    return responeData;
        //}
        //#endregion
        //#endregion

        //#region 获取项目地图信息
        ///// <summary>
        /////  获取项目地图信息
        ///// </summary>
        ///// <param name="projectId"></param>
        ///// <param name="type">1总平面布置图，2区域平面图，3三维模型图</param>
        ///// <returns></returns>
        //public Model.ResponeData getProjectMapByProjectId(string projectId, string type)
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getProjectMapByProjectId(projectId, type); ;
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }

        //    return responeData;
        //}

        //#region 保存项目地图信息
        ///// <summary>
        ///// 保存项目地图信息
        ///// </summary>
        ///// <param name="projectMap">地图信息</param>
        ///// <returns></returns>
        //[HttpPost]
        //public Model.ResponeData SaveProjectMap([FromBody] Model.PictureItem projectMap)
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        APIBaseInfoService.SaveProjectMap(projectMap);
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }

        //    return responeData;
        //}
        //#endregion
        //#endregion

        //#region 获取通知通告
        ///// <summary>
        ///// 获取头条通知
        ///// </summary>
        ///// <returns></returns>
        //public Model.ResponeData getTopNotices()
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        string returnValue = string.Empty;
        //        var notice = (from x in Funs.DB.InformationProject_Notice
        //                      where x.IsRelease == true
        //                      orderby x.ReleaseDate descending
        //                      select x).FirstOrDefault();

        //        if (notice != null)
        //        {
        //            returnValue = notice.NoticeTitle;
        //        }

        //        responeData.data = new { returnValue };
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }

        //    return responeData;
        //}

        ///// <summary>
        ///// 根据项目ID通知通告
        ///// </summary>
        ///// <param name="projectId"></param>
        ///// <param name="pageIndex"></param>
        ///// <returns></returns>
        //public Model.ResponeData getNotices(string projectId, int pageIndex)
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        var noticeList = APIBaseInfoService.getNoticesList(projectId, null);
        //        int pageCount = noticeList.Count();
        //        if (pageCount > 0 && pageIndex > 0)
        //        {
        //            noticeList = noticeList.OrderByDescending(u => u.ReleaseDate).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize).ToList();
        //        }
        //        responeData.data = new { pageCount, noticeList };
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }

        //    return responeData;
        //}

        ///// <summary>
        ///// 根据项目ID通知通告
        ///// </summary>
        ///// <param name="projectId">项目ID</param>
        ///// <param name="strParam">查询条件</param>
        ///// <param name="pageIndex"></param>
        ///// <returns></returns>
        //public Model.ResponeData getNoticesQuery(string projectId, string strParam, int pageIndex)
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        var noticeList = APIBaseInfoService.getNoticesList(projectId, strParam);
        //        int pageCount = noticeList.Count();
        //        if (pageCount > 0 && pageIndex > 0)
        //        {
        //            noticeList = noticeList.OrderByDescending(u => u.ReleaseDate).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize).ToList();
        //        }
        //        responeData.data = new { pageCount, noticeList };
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }

        //    return responeData;
        //}

        ///// <summary>
        ///// 根据项目ID通知通告
        ///// </summary>
        ///// <param name="projectId">项目ID</param>
        ///// <param name="userId">用户ID</param>
        ///// <param name="strParam">查询条件</param>
        ///// <param name="pageIndex"></param>
        ///// <returns></returns>
        //public Model.ResponeData getNoticesQuery(string projectId, string userId, string strParam, int pageIndex)
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        var noticeList = APIBaseInfoService.getNoticesList(projectId, userId, strParam);
        //        int pageCount = noticeList.Count();
        //        if (pageCount > 0 && pageIndex > 0)
        //        {
        //            noticeList = noticeList.Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize).ToList();
        //        }
        //        responeData.data = new { pageCount, noticeList };
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }

        //    return responeData;
        //}

        ///// <summary>
        ///// 根据noticeId获取通知通告详细
        ///// </summary>
        ///// <param name="noticeId"></param>
        ///// <returns></returns>
        //public Model.ResponeData getNoticesByNoticeId(string noticeId)
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getNoticesByNoticeId(noticeId);
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }

        //    return responeData;
        //}
        //#endregion

        //#region  获取岗位信息
        ///// <summary>
        /////   获取岗位信息
        ///// </summary>
        ///// <returns></returns>
        //public Model.ResponeData getWorkPost()
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getWorkPost(null);
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }

        //    return responeData;
        //}
        ///// <summary>
        /////   获取岗位信息
        ///// </summary>
        ///// <returns></returns>
        //public Model.ResponeData getWorkPostQuery(string strParam)
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getWorkPost(strParam);
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }

        //    return responeData;
        //}
        //#endregion

        //#region  获取法律法规类型
        ///// <summary>
        /////   获取法律法规类型
        ///// </summary>
        ///// <returns></returns>
        //public Model.ResponeData getLawsRegulationsType()
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getLawsRegulationsType();
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }

        //    return responeData;
        //}
        //#endregion

        //#region  获取标准规范类型
        ///// <summary>
        /////   获取标准规范类型
        ///// </summary>
        ///// <returns></returns>
        //public Model.ResponeData getHSSEStandardListType()
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getHSSEStandardListType();
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }

        //    return responeData;
        //}
        //#endregion

        //#region  获取规章制度类型
        ///// <summary>
        /////   获取规章制度类型
        ///// </summary>
        ///// <returns></returns>
        //public Model.ResponeData getRulesRegulationsType()
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getRulesRegulationsType();
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }

        //    return responeData;
        //}
        //#endregion

        //#region  获取管理规定类型
        ///// <summary>
        /////   获取管理规定类型
        ///// </summary>
        ///// <returns></returns>
        //public Model.ResponeData getManageRuleType()
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getManageRuleType();
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }

        //    return responeData;
        //}
        //#endregion

        //#region  获取培训类别
        ///// <summary>
        /////   获取培训类别
        ///// </summary>
        ///// <returns></returns>
        //public Model.ResponeData getTrainType()
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getTrainType();
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }
        //    return responeData;
        //}
        //#endregion

        //#region  获取培训级别
        ///// <summary>
        /////   获取培训级别
        ///// </summary>
        ///// <returns></returns>
        //public Model.ResponeData getTrainLevel()
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getTrainLevel();
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }
        //    return responeData;
        //}
        //#endregion

        //#region  获取考试规则信息
        ///// <summary>
        /////   获取考试规则信息
        ///// </summary>
        ///// <returns></returns>
        //public Model.ResponeData getSysTestRule()
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        var getTestRule = Funs.DB.Sys_TestRule.FirstOrDefault();
        //        responeData.data = new { getTestRule };
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }
        //    return responeData;
        //}
        //#endregion

        //#region  获取机具类型
        ///// <summary>
        ///// 获取机具类型
        ///// </summary>
        ///// <param name="isSpecial">是否特殊</param>
        ///// <returns></returns>
        //public Model.ResponeData getSpecialEquipment(bool isSpecial)
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getSpecialEquipment(isSpecial);
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }
        //    return responeData;
        //}
        //#endregion

        //#region  获取施工方案审核类型
        ///// <summary>
        /////   获取施工方案审核类型
        ///// </summary>
        ///// <returns></returns>
        //public Model.ResponeData getInvestigateType()
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getSysConst(ConstValue.Group_InvestigateType);
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }
        //    return responeData;
        //}
        //#endregion

        //#region  获取施工方案类别
        ///// <summary>
        /////   获取施工方案类别
        ///// </summary>
        ///// <returns></returns>
        //public Model.ResponeData getSolutinType()
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getSysConst(ConstValue.Group_CNProfessional);
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }
        //    return responeData;
        //}
        //#endregion

        //#region  获取工作阶段
        ///// <summary>
        /////   获取施工方案类别
        ///// </summary>
        ///// <returns></returns>
        //public Model.ResponeData getWorkStage()
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getWorkStage();
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }
        //    return responeData;
        //}
        //#endregion

        //#region  获取奖励类型
        ///// <summary>
        /////   获取奖励类型
        ///// </summary>
        ///// <returns></returns>
        //public Model.ResponeData getRewardType()
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getSysConst(ConstValue.Group_RewardType);
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }
        //    return responeData;
        //}
        //#endregion

        //#region  获取项目班组
        ///// <summary>
        ///// 获取项目班组
        ///// </summary>
        ///// <param name="projectId"></param>
        ///// <param name="unitId"></param>
        ///// <returns></returns>
        //public Model.ResponeData getTeamGroup(string projectId, string unitId)
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getTeamGroup(projectId, unitId);
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }
        //    return responeData;
        //}

        ///// <summary>
        ///// 获取项目班组人数
        ///// </summary>
        ///// <param name="teamGroupId"></param>
        ///// <returns></returns>
        //public Model.ResponeData getTeamGroupPersonNum(string teamGroupId)
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = TeamGroupService.getTeamGroupPersonNum(teamGroupId);
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }
        //    return responeData;
        //}
        ///// <summary>
        ///// 获取项目班组人数
        ///// </summary>
        ///// <param name="teamGroupId"></param>
        ///// <returns></returns>
        //public Model.ResponeData getTeamGroupLeader(string teamGroupId)
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getTeamGroupLeader(teamGroupId);
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }
        //    return responeData;
        //}
        //#endregion

        //#region  获取项目区域
        ///// <summary>
        ///// 获取项目区域
        ///// </summary>
        ///// <param name="projectId"></param>
        ///// <param name="unitId"></param>
        ///// <returns></returns>
        //public Model.ResponeData getWorkArea(string projectId, string unitId)
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getWorkArea(projectId, unitId);
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }
        //    return responeData;
        //}
        //#endregion

        //#region  获取作业票类型
        ///// <summary>
        /////   获取作业票类型
        ///// </summary>
        ///// <returns></returns>
        //public Model.ResponeData getLicenseType()
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getSysConst(ConstValue.Group_LicenseType);
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }
        //    return responeData;
        //}
        //#endregion

        //#region  获取作业票类型（定稿）
        ///// <summary>
        /////   获取作业票类型
        ///// </summary>
        ///// <returns></returns>
        //public Model.ResponeData getLicenseType2()
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getBase_LicenseType();
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }
        //    return responeData;
        //}
        //#endregion

        //#region 获取作业票-安全措施
        ///// <summary>
        ///// 获取作业票-安全措施
        ///// </summary>
        ///// <param name="licenseType">作业票类型</param>
        ///// <returns></returns>
        //public Model.ResponeData getSafetyMeasures(string licenseType)
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getSafetyMeasures(licenseType);
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }
        //    return responeData;
        //}
        //#endregion

        //#region 获取项目图片分类
        ///// <summary>
        /////   获取项目图片分类
        ///// </summary>
        ///// <returns></returns>
        //public Model.ResponeData getPictureType()
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getPictureType();
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }
        //    return responeData;
        //}
        //#endregion

        //#region 获取标牌类型
        ///// <summary>
        /////   获取标牌类型
        ///// </summary>
        ///// <returns></returns>
        //public Model.ResponeData getSignType()
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getSysConst(ConstValue.Group_SignType);
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }
        //    return responeData;
        //}
        //#endregion

        //#region 获取事故类型 -事故登记
        ///// <summary>
        /////   获取事故类型-事故登记
        ///// </summary>
        ///// <returns></returns>
        //public Model.ResponeData getAccidentReportRegistration()
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getSysConst(ConstValue.Group_AccidentReportRegistration);
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }
        //    return responeData;
        //}
        //#endregion

        //#region 获取岗位类型
        ///// <summary>
        /////   获取岗位类型
        ///// </summary>
        ///// <returns></returns>
        //public Model.ResponeData getPostType()
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getSysConst(ConstValue.Group_PostType);
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }
        //    return responeData;
        //}
        //#endregion

        //#region 获取特岗证书
        ///// <summary>
        /////   获取特岗证书
        ///// </summary>
        ///// <returns></returns>
        //public Model.ResponeData getCertificate()
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getCertificate();
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }
        //    return responeData;
        //}
        //#endregion

        //#region 获取部门信息
        ///// <summary>
        /////   获取部门信息
        ///// </summary>
        ///// <returns></returns>
        //public Model.ResponeData getDepart()
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getDepart();
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }
        //    return responeData;
        //}
        //#endregion

        //#region 获取应急预案类型
        ///// <summary>
        /////   获取应急预案类型
        ///// </summary>
        ///// <returns></returns>
        //public Model.ResponeData getEmergencyType()
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        responeData.data = APIBaseInfoService.getEmergencyType();
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }
        //    return responeData;
        //}
        //#endregion
    }
}
