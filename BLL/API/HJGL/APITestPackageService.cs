using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class APITestPackageService
    {
        #region 
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
    }
}
