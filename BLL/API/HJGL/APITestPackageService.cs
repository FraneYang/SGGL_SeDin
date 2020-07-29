using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class APITestPackageService
    {
        #region 获取试压包号
        /// <summary>
        /// 获取试压包号
        /// </summary>
        /// <param name="unitWorkId">单位工程</param>
        /// <param name="isFinish">是否完成</param>
        /// <param name="testPackageNo">试压包号</param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getTestPackageNo(string unitWorkId, bool isFinish, string testPackageNo)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var dataList = from x in db.PTP_TestPackage
                               where x.UnitWorkId == unitWorkId
                               select x;

                if (!string.IsNullOrEmpty(testPackageNo))
                {
                    dataList = dataList.Where(e => e.TestPackageNo.Contains(testPackageNo));
                }

                if (isFinish == true)
                {
                    dataList = dataList.Where(e => e.FinishDate.HasValue == true);
                }
                else
                {
                    dataList = dataList.Where(e => e.FinishDate.HasValue == false);
                }

                var getDataLists = (from x in dataList
                                    orderby x.TestPackageNo
                                    select new Model.BaseInfoItem
                                    {
                                        BaseInfoId = x.PTP_ID,
                                        BaseInfoCode = x.TestPackageNo,
                                    }).ToList();
                return getDataLists;
            }
        }
        #endregion

        #region 根据试压包ID获取明细
        /// <summary>
        /// 根据试压包ID获取明细
        /// </summary>
        /// <param name="ptp_Id"></param>
        /// <returns></returns>
        public static List<Model.TestPackageItem> GetTestPackageDetail(string ptp_Id)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.View_PTP_TestPackageAudit
                                    where x.PTP_ID == ptp_Id
                                    orderby x.PipelineCode
                                    select new Model.TestPackageItem
                                    {
                                        PipelineCode = x.PipelineCode,
                                        WeldJointCount = x.WeldJointCount,
                                        WeldJointCountT = x.WeldJointCountT,
                                        CountS = x.CountS,
                                        CountU = x.CountU,
                                        NDTR_Name = x.NDTR_Name,
                                        Ratio = x.Ratio

                                    }).ToList();

                return getDataLists;
            }
        }
        #endregion

        #region  获取具备试压条件的试压包提醒
        /// <summary>
        /// 获取具备试压条件的试压包提醒
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> GetCanTestPackageWarning(string projectId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                List<Model.BaseInfoItem> canTest = new List<Model.BaseInfoItem>();
                // 获取项目中未完成的试压包
                var testPackage = from x in db.PTP_TestPackage
                                  where x.ProjectId == projectId && !x.FinishDate.HasValue
                                  select x;

                foreach (var t in testPackage)
                {
                    string strSql = @"SELECT ProjectId,PTP_ID,WeldJointCount,WeldJointCountT,CountU 
                              FROM dbo.View_PTP_TestPackageAudit
                             WHERE PTP_ID=@PTP_ID";

                    List<SqlParameter> listStr = new List<SqlParameter>();
                    listStr.Add(new SqlParameter("@PTP_ID", t.PTP_ID));
                    SqlParameter[] parameter = listStr.ToArray();
                    DataTable dt = SQLHelper.GetDataTableRunText(strSql, parameter);

                    if(IsCanTest(dt))
                    {

                        Model.BaseInfoItem item = new Model.BaseInfoItem();
                        item.BaseInfoId = t.PTP_ID;
                        item.BaseInfoCode ="具备试压条件："+ t.TestPackageNo;
                        canTest.Add(item);
                    }
                }
                return canTest;
            }
        }


        private static bool IsCanTest(DataTable dt)
        {
            bool isPass = true;
            foreach (DataRow row in dt.Rows)
            {
                int totalJoint = Convert.ToInt32(row["WeldJointCount"]);
                int compJoint = Convert.ToInt32(row["WeldJointCountT"]);
                int noPassJoint = Convert.ToInt32(row["CountU"]);

                if (totalJoint != compJoint || noPassJoint != 0)
                {
                    isPass = false;
                    break;
                }
            }

            return isPass;
        }
        #endregion
    }
}
