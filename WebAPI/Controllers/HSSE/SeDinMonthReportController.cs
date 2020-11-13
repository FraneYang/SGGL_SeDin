using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 赛鼎月报
    /// </summary>
    public class SeDinMonthReportController : ApiController
    {
        #region 获取赛鼎月报列表信息
        /// <summary>
        /// 获取赛鼎月报列表信息
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="month">月份</param>
        ///  <param name="states">状态0-待提交；1-已提交</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public Model.ResponeData getSeDinMonthReportList(string projectId,string month, string states, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APISeDinMonthReportService.getSeDinMonthReportList(projectId, month, states);
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
        
        #region 获取赛鼎月报详细信息
        /// <summary>
        /// 根据主键ID获取赛鼎月报
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="month">月份</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="pageNum">页数</param>
        /// <returns></returns>
        public Model.ResponeData getSeDinMonthReportInfo(string projectId, string month, string startDate, string endDate, string pageNum)
        {
            var responeData = new Model.ResponeData();
            try
            {
                if (pageNum == "0") ////封面
                {
                    var getInfo = APISeDinMonthReportService.getSeDinMonthReport0ById(projectId, month);
                    if (getInfo == null || string.IsNullOrEmpty(getInfo.MonthReportId))
                    {
                        getInfo = APISeDinMonthReportService.getSeDinMonthReportNullPage0(projectId);
                    }
                    responeData.data = getInfo;
                }
                else if (pageNum == "1") ////1、项目信息
                {
                    var getInfo = APISeDinMonthReportService.getSeDinMonthReport1ById(projectId, month);
                    if (getInfo == null || string.IsNullOrEmpty(getInfo.MonthReportId))
                    {
                        getInfo = APISeDinMonthReportService.getSeDinMonthReportNullPage1(projectId);
                    }
                    responeData.data = getInfo;
                }
                else if (pageNum == "2") ////2、项目安全工时统计
                {
                    var getInfo = APISeDinMonthReportService.getSeDinMonthReport2ById(projectId, month);
                    if (getInfo == null || string.IsNullOrEmpty(getInfo.MonthReportId))
                    {
                        getInfo = APISeDinMonthReportService.getSeDinMonthReportNullPage2(projectId, month, startDate, endDate);
                    }
                    responeData.data = getInfo;
                }
                else if (pageNum == "3") ////3、项目HSE事故、事件统计
                {
                    var getInfo = APISeDinMonthReportService.getSeDinMonthReport3ById(projectId, month);
                    if (getInfo == null || getInfo.SeDinMonthReport3Item == null || getInfo.SeDinMonthReport3Item.Count() == 0)
                    {
                        getInfo = APISeDinMonthReportService.getSeDinMonthReportNullPage3(projectId, month, startDate, endDate);
                    }

                    responeData.data = getInfo;
                }
                else if (pageNum == "4") ////4、人员
                {
                    var getLists = APISeDinMonthReportService.getSeDinMonthReport4ById(projectId, month);
                    if (getLists.Count() == 0)
                    {
                        getLists = APISeDinMonthReportService.getSeDinMonthReportNullPage4(projectId, month, startDate, endDate);
                    }
                    responeData.data = getLists;
                }
                else if (pageNum == "5") ////5、本月大型、特种设备投入情况
                {
                    var getLists = APISeDinMonthReportService.getSeDinMonthReport5ById(projectId, month);
                    if (getLists.Count == 0)
                    {
                        getLists = APISeDinMonthReportService.getSeDinMonthReportNullPage5(projectId, month, startDate, endDate);
                    }
                    responeData.data = getLists;
                }
                else if (pageNum == "6") ////6、安全生产费用投入情况
                {
                    var getInfo = APISeDinMonthReportService.getSeDinMonthReport6ById(projectId, month);
                    if (getInfo == null || string.IsNullOrEmpty(getInfo.MonthReportId))
                    {
                        getInfo = APISeDinMonthReportService.getSeDinMonthReportNullPage6(projectId, month, startDate, endDate);
                    }

                    responeData.data = getInfo;
                }
                else if (pageNum == "7") ////7、项目HSE培训统计
                {
                    var getInfo = APISeDinMonthReportService.getSeDinMonthReport7ById(projectId, month);
                    if (getInfo == null || string.IsNullOrEmpty(getInfo.MonthReportId))
                    {
                        getInfo = APISeDinMonthReportService.getSeDinMonthReportNullPage7(projectId, month, startDate, endDate);
                    }

                    responeData.data = getInfo;
                }
                else if (pageNum == "8") ////8、项目HSE会议统计
                {
                    var getInfo = APISeDinMonthReportService.getSeDinMonthReport8ById(projectId, month);
                    if (getInfo == null || string.IsNullOrEmpty(getInfo.MonthReportId))
                    {
                        getInfo = APISeDinMonthReportService.getSeDinMonthReportNullPage8(projectId, month, startDate, endDate);
                    }

                    responeData.data = getInfo;
                }
                else if (pageNum == "9") ////9、项目HSE检查统计
                {
                    var getInfo = APISeDinMonthReportService.getSeDinMonthReport9ById(projectId, month);
                    if (getInfo == null || string.IsNullOrEmpty(getInfo.MonthReportId))
                    {
                        getInfo = APISeDinMonthReportService.getSeDinMonthReportNullPage9(projectId, month, startDate, endDate);
                    }

                    responeData.data = getInfo;
                }
                else if (pageNum == "10") ////10、项目奖惩情况统计
                {
                    var getInfo = APISeDinMonthReportService.getSeDinMonthReport10ById(projectId, month);
                    if (getInfo == null || string.IsNullOrEmpty(getInfo.MonthReportId))
                    {
                        getInfo = APISeDinMonthReportService.getSeDinMonthReportNullPage10(projectId, month, startDate, endDate);
                    }

                    responeData.data = getInfo;
                }
                else if (pageNum == "11") ////11、项目危大工程施工情况
                {
                    var getInfo = APISeDinMonthReportService.getSeDinMonthReport11ById(projectId, month);
                    if (getInfo == null || string.IsNullOrEmpty(getInfo.MonthReportId))
                    {
                        getInfo = APISeDinMonthReportService.getSeDinMonthReportNullPage11(projectId, month, startDate, endDate);
                    }

                    responeData.data = getInfo;
                }
                else if (pageNum == "12") ////12、项目应急演练情况
                {
                    var getInfo = APISeDinMonthReportService.getSeDinMonthReport12ById(projectId, month);
                    if (getInfo == null || string.IsNullOrEmpty(getInfo.MonthReportId))
                    {
                        getInfo = APISeDinMonthReportService.getSeDinMonthReportNullPage12(projectId, month, startDate, endDate);
                    }

                    responeData.data = getInfo;
                }
                else ////13、14、本月HSE活动综述、下月HSE工作计划
                {
                    responeData.data = APISeDinMonthReportService.getSeDinMonthReport13ById(projectId, month); ;
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

        #region 保存赛鼎月报信息
        #region 保存 MonthReport0 封面
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveSeDinMonthReport0([FromBody] Model.SeDinMonthReportItem newItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APISeDinMonthReportService.SaveSeDinMonthReport0(newItem);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion
        #region 保存 MonthReport1、项目信息
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveSeDinMonthReport1([FromBody] Model.SeDinMonthReport1Item newItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                if (!string.IsNullOrEmpty(newItem.MonthReportId))
                {
                    responeData.data = APISeDinMonthReportService.SaveSeDinMonthReport1(newItem);
                }
                else
                {
                    responeData.code = 1;
                    responeData.message = "请先保存月报主表信息！";
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
        #region 保存 MonthReport2、项目安全工时统计
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveSeDinMonthReport2([FromBody] Model.SeDinMonthReport2Item newItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                if (!string.IsNullOrEmpty(newItem.MonthReportId))
                {
                    responeData.data = APISeDinMonthReportService.SaveSeDinMonthReport2(newItem);
                }
                else
                {
                    responeData.code = 1;
                    responeData.message = "请先保存月报主表信息！";
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
        #region 保存 MonthReport3、项目HSE事故、事件统计
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveSeDinMonthReport3([FromBody] Model.SeDinMonthReportItem newItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                if (!string.IsNullOrEmpty(newItem.MonthReportId))
                {
                    responeData.data = APISeDinMonthReportService.SaveSeDinMonthReport3(newItem);
                }
                else
                {
                    responeData.code = 1;
                    responeData.message = "请先保存月报主表信息！";
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
        #region 保存 MonthReport4、本月人员投入情况
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveSeDinMonthReport4([FromBody]  Model.SeDinMonthReportItem newItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                if (!string.IsNullOrEmpty(newItem.MonthReportId))
                {
                    responeData.data = APISeDinMonthReportService.SaveSeDinMonthReport4(newItem);
                }
                else
                {
                    responeData.code = 1;
                    responeData.message = "请先保存月报主表信息！";
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
        #region 保存 MonthReport5、本月大型、特种设备投入情况
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveSeDinMonthReport5([FromBody] Model.SeDinMonthReportItem newItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                if (!string.IsNullOrEmpty(newItem.MonthReportId))
                {
                    responeData.data = APISeDinMonthReportService.SaveSeDinMonthReport5(newItem);
                }
                else
                {
                    responeData.code = 1;
                    responeData.message = "请先保存月报主表信息！";
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
        #region 保存 MonthReport6、安全生产费用投入情况
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveSeDinMonthReport6([FromBody] Model.SeDinMonthReport6Item newItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                if (!string.IsNullOrEmpty(newItem.MonthReportId))
                {
                    responeData.data = APISeDinMonthReportService.SaveSeDinMonthReport6(newItem);
                }
                else
                {
                    responeData.code = 1;
                    responeData.message = "请先保存月报主表信息！";
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
        #region 保存 MonthReport7、项目HSE培训统计
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveSeDinMonthReport7([FromBody] Model.SeDinMonthReport7Item newItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                if (!string.IsNullOrEmpty(newItem.MonthReportId))
                {
                    responeData.data = APISeDinMonthReportService.SaveSeDinMonthReport7(newItem);
                }
                else
                {
                    responeData.code = 1;
                    responeData.message = "请先保存月报主表信息！";
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
        #region 保存 MonthReport8、项目HSE会议统计
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveSeDinMonthReport8([FromBody] Model.SeDinMonthReport8Item newItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                if (!string.IsNullOrEmpty(newItem.MonthReportId))
                {
                    responeData.data = APISeDinMonthReportService.SaveSeDinMonthReport8(newItem);
                }
                else
                {
                    responeData.code = 1;
                    responeData.message = "请先保存月报主表信息！";
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
        #region 保存 MonthReport9、项目HSE检查统计
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveSeDinMonthReport9([FromBody] Model.SeDinMonthReport9Item newItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                if (!string.IsNullOrEmpty(newItem.MonthReportId))
                {
                    responeData.data = APISeDinMonthReportService.SaveSeDinMonthReport9(newItem);
                }
                else
                {
                    responeData.code = 1;
                    responeData.message = "请先保存月报主表信息！";
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
        #region 保存 MonthReport10、项目奖惩情况统计
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveSeDinMonthReport10([FromBody] Model.SeDinMonthReport10Item newItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                if (!string.IsNullOrEmpty(newItem.MonthReportId))
                {
                    responeData.data = APISeDinMonthReportService.SaveSeDinMonthReport10(newItem);
                }
                else
                {
                    responeData.code = 1;
                    responeData.message = "请先保存月报主表信息！";
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
        #region 保存 MonthReport11、项目危大工程施工情况
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveSeDinMonthReport11([FromBody] Model.SeDinMonthReport11Item newItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                if (!string.IsNullOrEmpty(newItem.MonthReportId))
                {
                    responeData.data = APISeDinMonthReportService.SaveSeDinMonthReport11(newItem);
                }
                else
                {
                    responeData.code = 1;
                    responeData.message = "请先保存月报主表信息！";
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
        #region 保存 MonthReport12、项目应急演练情况
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveSeDinMonthReport12([FromBody] Model.SeDinMonthReport12Item newItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                if (!string.IsNullOrEmpty(newItem.MonthReportId))
                {
                    responeData.data = APISeDinMonthReportService.SaveSeDinMonthReport12(newItem);
                }
                else
                {
                    responeData.code = 1;
                    responeData.message = "请先保存月报主表信息！";
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
        #region 保存 MonthReport13、14、本月HSE活动综述、下月HSE工作计划
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveSeDinMonthReport13([FromBody] Model.SeDinMonthReportItem newItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APISeDinMonthReportService.SaveSeDinMonthReport13(newItem);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion        
        #endregion

        #region 汇总到HSE数据
        /// <summary>
        /// 汇总到HSE数据
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveSeDinMonthHSEDataCollect([FromBody] Model.SeDinMonthReportItem newItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                ////如果月报审核完成 月报信息写入到汇总   
                var getSeDin_MonthReport = ManagerMonth_SeDinService.GetMonthReportByMonthReportId(newItem.MonthReportId);
                if (getSeDin_MonthReport != null && getSeDin_MonthReport.States == Const.State_3 && getSeDin_MonthReport.ReporMonth.HasValue)
                {
                    HSEDataCollectService.SaveHSEDataCollectItem(getSeDin_MonthReport);
                }
                responeData.data = newItem.MonthReportId;
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
