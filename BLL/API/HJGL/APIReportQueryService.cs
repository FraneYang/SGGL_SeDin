using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class APIReportQueryService
    {
        #region 根据人员二维码获取焊工业绩
        /// <summary>
        /// 根据人员二维码获取焊工业绩
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public static Model.WelderPerformanceItem GetWelderPerformanceByQRC(string personId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                //  todo 二维码分解待做
                var getDataLists = from x in db.View_WelderPerformance
                                   where x.PersonId == personId
                                   select new Model.WelderPerformanceItem
                                   {
                                       WelderCode = x.WelderCode,
                                       PersonName = x.PersonName,
                                       UnitName = x.UnitName,
                                       CertificateLimitTime = x.CertificateLimitTime.ToString(),
                                       WelderLevel = x.WelderLevel,
                                       OnePassRate = x.PassRate,
                                       TotalJotDin = x.Nowtotal_jot.ToString() + "/" + x.Nowtotal_din.ToString(),
                                       WeldAvgNum = GetWeldAvgNum(personId, x.Nowtotal_din.HasValue ? x.Nowtotal_din.Value : 0),
                                       OneCheckJotNum = x.OneCheckJotNum.ToString(),
                                       OneCheckPassJotNum = x.OneCheckPassJotNum,
                                       RepairJotNum = x.OneCheckRepairJotNum.ToString(),
                                       ExpandJotNum = x.ExpandJotNum.ToString()
                                   };

                return getDataLists.FirstOrDefault();
            }
        }
        #endregion

        private static string GetWeldAvgNum(string personId,decimal totalDin)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                string vagDin = string.Empty;
                var date = from x in db.HJGL_WeldJoint
                           join y in db.HJGL_WeldingDaily on x.WeldingDailyId equals y.WeldingDailyId
                           where x.BackingWelderId == personId || x.CoverWelderId == personId
                           select y.WeldingDate;
                if (date.Count() > 0)
                {
                    DateTime startDate = Convert.ToDateTime(date.Min());
                    DateTime endDate = Convert.ToDateTime(date.Max());
                    TimeSpan t = endDate - startDate;
                    int dayNum = t.Days;
                    vagDin = (totalDin / dayNum).ToString("0.##");

                }
                return vagDin;
            }
        }

        #region 根据焊工号获取焊工业绩
        /// <summary>
        /// 根据焊工号获取焊工业绩
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="welderCode"></param>
        /// <returns></returns>
        public static Model.WelderPerformanceItem GetWelderPerformanceByWelderCode(string projectId,string welderCode)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                //  todo 二维码分解待做
                var getDataLists = from x in db.View_WelderPerformance
                                   where x.ProjectId == projectId && x.WelderCode == welderCode
                                   select new Model.WelderPerformanceItem
                                   {
                                       WelderCode = x.WelderCode,
                                       PersonName = x.PersonName,
                                       UnitName = x.UnitName,
                                       CertificateLimitTime = x.CertificateLimitTime.ToString(),
                                       WelderLevel = x.WelderLevel,
                                       OnePassRate = x.PassRate,
                                       TotalJotDin = x.Nowtotal_jot.ToString() + "/" + x.Nowtotal_din.ToString(),
                                       WeldAvgNum = GetWeldAvgNum(x.PersonId, x.Nowtotal_din.HasValue ? x.Nowtotal_din.Value : 0),
                                       OneCheckJotNum = x.OneCheckJotNum.ToString(),
                                       OneCheckPassJotNum = x.OneCheckPassJotNum,
                                       RepairJotNum = x.OneCheckRepairJotNum.ToString(),
                                       ExpandJotNum = x.ExpandJotNum.ToString()
                                   };

                return getDataLists.FirstOrDefault();
            }
        }
        #endregion

        #       region 根据人员ID获取焊工合格项目
        /// <summary>
        ///  根据人员ID获取焊工合格项目
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getWelderQualify(string personId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.Welder_WelderQualify
                                    where x.WelderId == personId
                                    orderby x.LimitDate
                                    select new Model.BaseInfoItem
                                    {
                                        BaseInfoId = x.QualificationItem,  // 合格项目
                                        BaseInfoCode = x.CheckDate.HasValue ? x.CheckDate.Value.ToString() : "",  // 批准日期
                                        BaseInfoName = x.LimitDate.HasValue ? x.LimitDate.Value.ToString() : ""   // 有效日期
                                    }
                                ).ToList();
                return getDataLists;
            }
        }
        #endregion

        #region  焊工资质预警
        /// <summary>
        /// 焊工资质预警
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> GetWelderQualifyWarning(string projectId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var welder = from x in db.SitePerson_Person
                             where x.ProjectId == projectId && x.WorkPostId == Const.WorkPost_Welder
                             && x.WelderCode != null
                             select x;
                List<Model.BaseInfoItem> warnWelder = new List<Model.BaseInfoItem>();

                foreach (var q in welder)
                {

                    DateTime? validity = q.CertificateLimitTime;
                    DateTime nowDate = DateTime.Now;
                    if (validity != null)
                    {
                        if (validity.Value.AddMonths(-1) < nowDate && validity >= nowDate)
                        {
                            Model.BaseInfoItem item = new Model.BaseInfoItem();
                            item.BaseInfoId = q.PersonId;
                            item.BaseInfoCode = q.WelderCode;
                            item.BaseInfoName = q.CertificateLimitTime.HasValue ? q.CertificateLimitTime.Value.ToString() : "" + "即将过期";
                            warnWelder.Add(item);
                        }
                        else if (validity < nowDate)
                        {
                            Model.BaseInfoItem item = new Model.BaseInfoItem();
                            item.BaseInfoId = q.PersonId;
                            item.BaseInfoCode =q.WelderCode;
                            item.BaseInfoName = q.CertificateLimitTime.HasValue ? q.CertificateLimitTime.Value.ToString() : "" + "已过期";
                            warnWelder.Add(item);
                        }
                    }
                }

                return warnWelder;
            }
        }
        #endregion


        #region  焊工一次合格率低于96%预警
        /// <summary>
        /// 焊工一次合格率低于96%预警
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> GetWelderOnePassRateWarning(string projectId)
        {

            string strSql = @"SELECT welder.ProjectId, welder.WelderCode, welder.PersonName,
	                                 CONVERT(NVARCHAR(10),(CAST((CASE ISNULL(oneCheck.OneCheckJotNum,0) WHEN 0 THEN 0 
		                             ELSE 100.0 * (ISNULL(oneCheck.OneCheckJotNum,0)-ISNULL(oneCheckRepair.oneCheckRepairJotNum,0))/(1.0 * oneCheck.OneCheckJotNum) END) AS DECIMAL(8,1))))+'%' AS passRate
                              FROM SitePerson_Person AS welder 
                              LEFT JOIN (SELECT jot.CoverWelderId,COUNT(ndeItem.NDEItemID) AS OneCheckJotNum
			                             FROM dbo.HJGL_Batch_NDEItem ndeItem
				                         LEFT JOIN dbo.HJGL_Batch_BatchTrustItem trustItem ON trustItem.TrustBatchItemId = ndeItem.TrustBatchItemId
				                         LEFT JOIN dbo.HJGL_WeldJoint jot ON jot.WeldJointId = trustItem.WeldJointId
					                     LEFT JOIN dbo.HJGL_WeldingDaily daily ON  daily.WeldingDailyId = jot.WeldingDailyId 	
					                     LEFT JOIN dbo.HJGL_Batch_PointBatchItem pointItem ON pointItem.PointBatchItemId = trustItem.PointBatchItemId
				                         LEFT JOIN dbo.HJGL_Batch_PointBatch point ON point.PointBatchId = pointItem.PointBatchId
			                             WHERE pointItem.PointDate IS NOT NULL AND pointItem.PointState=1 AND trustItem.RepairRecordId IS NULL		
			                            GROUP BY jot.CoverWelderId) AS oneCheck ON oneCheck.CoverWelderId = welder.PersonId

                              LEFT JOIN (SELECT jot.CoverWelderId,COUNT(ndeItem.NDEItemID) AS OneCheckRepairJotNum --一次检测返修焊口数
		                                 FROM dbo.HJGL_Batch_NDEItem ndeItem
				                         LEFT JOIN dbo.HJGL_Batch_BatchTrustItem trustItem ON trustItem.TrustBatchItemId = ndeItem.TrustBatchItemId
				                         LEFT JOIN dbo.HJGL_WeldJoint jot ON jot.WeldJointId = trustItem.WeldJointId
					                     LEFT JOIN dbo.HJGL_WeldingDaily daily ON  daily.WeldingDailyId = jot.WeldingDailyId 	
					                     LEFT JOIN dbo.HJGL_Batch_PointBatchItem pointItem ON pointItem.PointBatchItemId = trustItem.PointBatchItemId
				                         LEFT JOIN dbo.HJGL_Batch_PointBatch point ON point.PointBatchId = pointItem.PointBatchId  
			                             WHERE pointItem.PointDate IS NOT NULL AND pointItem.PointState=1
				                              AND trustItem.RepairRecordId IS NULL AND ndeItem.CheckResult='2' 			
			                             GROUP BY jot.CoverWelderId) AS oneCheckRepair ON oneCheckRepair.CoverWelderId = welder.PersonId

                             WHERE (welder.WelderCode IS NOT NULL AND welder.WelderCode!='') 
                                    AND (welder.WorkPostId='19B8F2A9-28D3-4F20-867A-1B2237C2E228')
	                                AND ISNULL(oneCheck.OneCheckJotNum,0)>0
	                                AND (CAST((CASE ISNULL(oneCheck.OneCheckJotNum,0) WHEN 0 THEN 0 
		                            ELSE 100.0 * (ISNULL(oneCheck.OneCheckJotNum,0)-ISNULL(oneCheckRepair.oneCheckRepairJotNum,0))/(1.0 * oneCheck.OneCheckJotNum) END) AS DECIMAL(8,1)))<=96";

            List<SqlParameter> listStr = new List<SqlParameter>();


            strSql += " AND welder.ProjectId=@ProjectId";
            listStr.Add(new SqlParameter("@ProjectId", projectId));

            SqlParameter[] parameter = listStr.ToArray();
            DataTable dt = SQLHelper.GetDataTableRunText(strSql, parameter);

            List<Model.BaseInfoItem> warnWelder = new List<Model.BaseInfoItem>();

            foreach (DataRow row in dt.Rows)
            {
                Model.BaseInfoItem item = new Model.BaseInfoItem();
                item.BaseInfoCode = row["WelderCode"].ToString();
                item.BaseInfoName = "一次合格率：" + row["passRate"].ToString();
                warnWelder.Add(item);
            }

            return warnWelder;

        }
        #endregion


        #region 根据焊口ID获取焊口信息和焊接信息
        /// <summary>
        ///  根据焊口ID获取焊口信息和焊接信息
        /// </summary>
        /// <param name="weldJointId"></param>
        /// <returns></returns>
        public static Model.JointCompreInfoItem GetJointCompreInfo(string weldJointId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDateInfo = from x in db.View_HJGL_WeldJoint
                                  where x.WeldJointId == weldJointId

                                  select new Model.JointCompreInfoItem
                                  {
                                      WeldJointCode = x.WeldJointCode,
                                      PipelineCode = x.PipelineCode,
                                      PipingClass = x.PipingClassCode,
                                      Medium = x.MediumCode,
                                      DetectionType = x.DetectionTypeCode,
                                      WeldType = x.WeldTypeCode,
                                      Material = x.MaterialCode,
                                      JointArea = x.JointArea,
                                      JointAttribute = x.JointAttribute,
                                      WeldingMode = x.WeldingMode,
                                      Size = x.Size,
                                      Dia = x.Dia,
                                      Thickness = x.Thickness,
                                      Specification = x.Specification,
                                      WeldingMethodCode = x.WeldingMethodCode,
                                      GrooveType = x.GrooveTypeCode,
                                      WeldingLocation = x.WeldingLocationCode,
                                      WeldingWire = x.WeldingWireCode,
                                      WeldingRod = x.WeldingRodCode,
                                      IsHotProess = x.IsHotProessStr,
                                      WelderCode = x.WelderCode,
                                      WeldingDate = x.WeldingDate,
                                      WeldingDailyCode = x.WeldingDailyCode,
                                      PointBatchCode = x.PointBatchCode,
                                      IsPoint = x.IsPoint

                                  };
                return getDateInfo.FirstOrDefault();
            }
        }
        #endregion

        #region 多维度查询报表
        /// <summary>
        /// 多维度查询报表
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="unitWorkId"></param>
        /// <param name="pipeLineId"></param>
        /// <param name="material"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static List<Model.ReportQueryItem> GetReportQueryByRequir(string projectId,string unitId,string unitWorkId, string pipeLineId, string material, string startTime,string endTime)
        {
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@projectId", projectId));

            if (!string.IsNullOrEmpty(pipeLineId))
            {
                listStr.Add(new SqlParameter("@pipeLineId", pipeLineId));
            }
            else if (!string.IsNullOrEmpty(unitWorkId) && string.IsNullOrEmpty(pipeLineId))
            {
                listStr.Add(new SqlParameter("@unitWorkId", unitWorkId));
            }

            else if (!string.IsNullOrEmpty(unitId) && string.IsNullOrEmpty(unitWorkId) && string.IsNullOrEmpty(pipeLineId))
            {
                listStr.Add(new SqlParameter("@unitId", unitId));
            }

            if (!string.IsNullOrEmpty(material))
            {
                listStr.Add(new SqlParameter("@material", material));
            }
            else
            {
                listStr.Add(new SqlParameter("@material", null));
            }

            if (!string.IsNullOrEmpty(startTime))
            {
                listStr.Add(new SqlParameter("@startTime", Convert.ToDateTime(startTime)));
            }
            else
            {
                listStr.Add(new SqlParameter("@startTime", null));
            }

            if (!string.IsNullOrEmpty(endTime))
            {
                listStr.Add(new SqlParameter("@endTime", Convert.ToDateTime(endTime)));
            }
            else
            {
                listStr.Add(new SqlParameter("@endTime", null));
            }
            SqlParameter[] parameter = listStr.ToArray();

            DataTable dt = null;
            if (string.IsNullOrEmpty(unitId) && string.IsNullOrEmpty(unitWorkId) && string.IsNullOrEmpty(pipeLineId))
            {
                dt = SQLHelper.GetDataTableRunProc("sp_ReportQueryByProject", parameter);
            }
            if (!string.IsNullOrEmpty(unitId) && string.IsNullOrEmpty(unitWorkId) && string.IsNullOrEmpty(pipeLineId))
            {
                dt = SQLHelper.GetDataTableRunProc("sp_ReportQueryByUnit", parameter);
            }
            if (!string.IsNullOrEmpty(unitWorkId) && string.IsNullOrEmpty(pipeLineId))
            {
                dt = SQLHelper.GetDataTableRunProc("sp_ReportQueryByUnitWork", parameter);
            }
            if (!string.IsNullOrEmpty(pipeLineId))
            {
                dt = SQLHelper.GetDataTableRunProc("sp_ReportQueryByPipeLine", parameter);
            }

            List<Model.ReportQueryItem> reportList = new List<Model.ReportQueryItem>();
            foreach (DataRow row in dt.Rows)
            {
                string totalJot = row["TotalJot"].ToString();
                string totalDin = row["TotalDin"].ToString();
                string weldedJot = row["weldedJot"].ToString();
                string weldedDin = row["weldedDin"].ToString();
                string oneCheckJotNum = row["OneCheckJotNum"].ToString();
                string oneCheckRepairJotNum = row["OneCheckRepairJotNum"].ToString();
                string expandJotNum = row["OneExpandJotNum"].ToString();

                Model.ReportQueryItem report = new Model.ReportQueryItem();

                string code = string.Empty;
                int mustCheckJotNum = 0;
                if (string.IsNullOrEmpty(unitId) && string.IsNullOrEmpty(unitWorkId) && string.IsNullOrEmpty(pipeLineId))
                {
                   mustCheckJotNum = GetMustCheckJotNum("1", row["ProjectId"].ToString());
                    code = row["ProjectCode"].ToString();
                    report.ProjectCode = row["ProjectCode"].ToString();
                }
                if (!string.IsNullOrEmpty(unitId) && string.IsNullOrEmpty(unitWorkId) && string.IsNullOrEmpty(pipeLineId))
                {
                    mustCheckJotNum = GetMustCheckJotNum("2", row["UnitId"].ToString());
                    report.CUnit = row["UnitCode"].ToString();

                }
                if (!string.IsNullOrEmpty(unitWorkId) && string.IsNullOrEmpty(pipeLineId))
                {
                    mustCheckJotNum = GetMustCheckJotNum("3", row["UnitWorkId"].ToString());
                    report.UnitWork = row["UnitWorkCode"].ToString();
                }
                if (!string.IsNullOrEmpty(pipeLineId))
                {
                    mustCheckJotNum = GetMustCheckJotNum("4", row["PipelineId"].ToString());
                    report.PipeLine = row["PipelineCode"].ToString();
                }
                

                string weldingRate = "0.0%";
                if (totalJot != "0")
                {
                    weldingRate = (Convert.ToInt32(weldedJot) * 100.0 / Convert.ToInt32(totalJot)).ToString() + "%";
                }

                string weldingOnePassRate = "0.0%";
                if (oneCheckJotNum != "0")
                {
                    weldingOnePassRate=((Convert.ToInt32(oneCheckJotNum)-Convert.ToInt32(oneCheckRepairJotNum))*100.0/ Convert.ToInt32(oneCheckJotNum)).ToString() + "%";
                }

                string checkCompRate = "0.0%";
                if (mustCheckJotNum != 0)
                {
                    checkCompRate = (Convert.ToInt32(oneCheckJotNum) * 100.0 / mustCheckJotNum).ToString() + "%";
                }
       
                report.TotalJotDin = totalJot + "/" + totalDin;
                report.WeldedJotDin = weldedJot + "/" + weldedDin;
                report.WeldingRate = weldingRate;
                report.WeldingOnePassRate = weldingOnePassRate;

                report.MustCheckJotNum = mustCheckJotNum.ToString();
                report.CheckedJotNum = oneCheckJotNum;
                report.CheckCompRate = checkCompRate;
                report.RepairJotNum = oneCheckRepairJotNum;
                report.ExpandJotNum = expandJotNum;

                reportList.Add(report);
            }

            return reportList;
        }
        #endregion

        /// <summary>
        /// 获取应检测焊口数
        /// </summary>
        /// <param name="flag">1-施工单位，2-单位工程，3-管线</param>
        /// <param name="id"></param>
        /// <returns></returns>
        private static int GetMustCheckJotNum(string flag, string id)
        {
            int num = 0;
            List<Model.HJGL_Pipeline> pipeList = null;
            if (flag == "1")
            {
                pipeList = (from x in Funs.DB.HJGL_Pipeline where x.ProjectId == id select x).ToList();
            }
            if (flag == "2")
            {
                pipeList = (from x in Funs.DB.HJGL_Pipeline where x.UnitId == id select x).ToList();
            }
            if (flag == "3")
            {
                pipeList = (from x in Funs.DB.HJGL_Pipeline where x.UnitWorkId == id select x).ToList();
            }
            if (flag == "4")
            {
                pipeList = (from x in Funs.DB.HJGL_Pipeline where x.PipelineId == id select x).ToList();
            }

            foreach (var pipe in pipeList)
            {
                var rate = BLL.Base_DetectionRateService.GetDetectionRateByDetectionRateId(pipe.DetectionRateId);
                int jointNum = (from x in Funs.DB.HJGL_WeldJoint where x.PipelineId == pipe.PipelineId select x).Count();
                decimal n = Convert.ToDecimal(jointNum * rate.DetectionRateValue * 1.0 / 100);
                num = num + Convert.ToInt32(Math.Ceiling(n));
            }
            return num;
        }
    }
}
