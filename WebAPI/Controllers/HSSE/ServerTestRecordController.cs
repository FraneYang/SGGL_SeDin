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
    /// 知识竞赛-考生记录信息
    /// </summary>
    public class ServerTestRecordController : ApiController
    {
        #region 获取考生信息
        /// <summary>
        /// 获取考生信息
        /// </summary>
        /// <param name="testPlanId"></param>
        /// <param name="testManId"></param>
        /// <param name="userType"></param>
        /// <param name="identityCard"></param>
        /// <returns></returns>
        public Model.ResponeData getTestRecordInfo(string testPlanId, string testManId, string userType,  string identityCard)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIServerTestRecordService.getTestRecordInfo(testPlanId, testManId, userType, identityCard);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 保存知识竞赛考生信息
        /// <summary>
        /// 保存知识竞赛考生信息
        /// </summary>
        /// <param name="testRecord">考生答题信息</param>
        [HttpPost]
        public Model.ResponeData SaveTestRecord([FromBody] Model.TestRecordItem testRecord)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIServerTestRecordService.SaveTestRecord(testRecord);               
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 生成试卷开始考试
        /// <summary>
        /// 生成试卷开始考试
        /// </summary>
        /// <param name="testPlanId">培训考试计划ID</param>
        /// <param name="testRecordId">考生信息ID</param>
        /// <returns></returns>
        public Model.ResponeData getCreateTestRecordItem(string testPlanId, string testRecordId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getTestPlan = new Model.SGGLDB(Funs.ConnString).Test_TestPlan.FirstOrDefault(x => x.TestPlanId == testPlanId && x.States == Const.State_2
                        && x.TestStartTime <= DateTime.Now && x.TestEndTime > DateTime.Now);
                if (getTestPlan != null)
                {
                    var getTestRecord = new Model.SGGLDB(Funs.ConnString).Test_TestRecord.FirstOrDefault(x => x.TestRecordId == testRecordId && !x.TestEndTime.HasValue);
                    if (getTestRecord != null)
                    {
                        responeData.data = APIServerTestRecordService.CreateTestRecordItem(testPlanId, testRecordId);
                    }
                    else
                    {
                        responeData.code = 2;
                        responeData.message = "您未加入到此次考试中！";
                    }
                }
                else
                {
                    responeData.code = 2;
                    responeData.message = "不在考试时间内！";
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

        #region 根据TestPlanId获取考生及试卷列表
        /// <summary>
        /// 根据TestPlanId获取考生及试卷列表
        /// </summary>
        /// <param name="testPlanId"></param>
        /// <returns>考试人员</returns>
        public Model.ResponeData getTestRecordListByTestPlanId(string testPlanId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIServerTestRecordService.getTestRecordListByTestPlanId(testPlanId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 根据试卷ID获取试卷记录详细
        /// <summary>
        /// 根据试卷ID获取试卷记录详细
        /// </summary>
        /// <param name="testRecordId"></param>
        /// <returns>试卷详细</returns>
        public Model.ResponeData getTestRecordByTestRecordId(string testRecordId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIServerTestRecordService.getTestRecordByTestRecordId(testRecordId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 根据TestRecordId获取试卷题目列表
        /// <summary>
        /// 根据TestRecordId获取试卷题目列表
        /// </summary>
        /// <param name="testRecordId"></param>
        /// <param name="pageIndex">页码</param>
        /// <returns>试卷题目列表</returns>
        public Model.ResponeData getTestRecordItemListByTestRecordId(string testRecordId, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataLists = APIServerTestRecordService.geTestRecordItemListByTestRecordId(testRecordId);
                int pageCount = getDataLists.Count;
                if (pageCount > 0 && pageIndex > 0)
                {
                    getDataLists = getDataLists.OrderBy(x => x.TestType).ThenBy(x => x.TrainingItemCode).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize).ToList();
                }
                responeData.data = new { pageCount, getDataLists };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 获取当前试卷的答题倒计时
        /// <summary>
        /// 获取当前试卷的答题倒计时
        /// </summary>
        /// <param name="testRecordId"></param>
        /// <returns></returns>
        public Model.ResponeData getTestTimesByTestRecordId(string testRecordId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                int mTime = 0;
                using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
                {
                    var getTestRecord = db.Test_TestRecord.FirstOrDefault(x => x.TestRecordId == testRecordId);
                    if (getTestRecord != null)
                    {
                        DateTime startTime = DateTime.Now;
                        if (getTestRecord.TestStartTime.HasValue)
                        {
                            startTime = getTestRecord.TestStartTime.Value;
                        }
                        else
                        {
                            getTestRecord.TestStartTime = startTime;
                            db.SubmitChanges();
                        }
                    }
                    mTime = Convert.ToInt32((getTestRecord.TestStartTime.Value.AddMinutes(getTestRecord.Duration.Value) - DateTime.Now).TotalSeconds);
                }
                responeData.data = new { mTime };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 根据TestRecordItemId、selectedItem 考生答题
        /// <summary>
        /// 根据TestRecordItemId、selectedItem 考生答题
        /// </summary>
        /// <param name="testRecordItemId">题目ID</param>
        /// <param name="selectedItem">选项</param>
        public Model.ResponeData getTestRecordItemAnswerBySelectedItem(string testRecordItemId, string selectedItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getItem = new Model.SGGLDB(Funs.ConnString).Test_TestRecordItem.FirstOrDefault(e => e.TestRecordItemId == testRecordItemId);
                if (getItem != null)
                {
                    //更新没有结束时间且超时的考试记录
                    int closeCount = ServerTestRecordService.UpdateTestEndTimeNull(getItem.TestRecordId);
                    if (closeCount > 0)
                    {
                        responeData.code = 2;
                        responeData.message = "本次考试已结束，系统自动交卷！";
                    }
                    else
                    {
                        APIServerTestRecordService.getTestRecordItemAnswerBySelectedItem(getItem, selectedItem);
                    }
                }
                else
                {
                    responeData.code = 0;
                    responeData.message = "试题有问题！";
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

        #region 交卷 
        /// <summary>
        ///  交卷
        /// </summary>
        /// <param name="testRecordId">试卷ID</param>
        public Model.ResponeData getSubmitTestRecordByTestRecordId(string testRecordId)
        {
            var responeData = new Model.ResponeData();
            try
            {                                
                ////考试分数
                var score= APIServerTestRecordService.getSubmitTestRecord(testRecordId);
                responeData.data = score;
                responeData.message = "您的成绩为：【" + score.ToString() + "】";
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
