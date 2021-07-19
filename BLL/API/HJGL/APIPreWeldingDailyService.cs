using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;

namespace BLL
{
    /// <summary>
    /// 焊接日报
    /// </summary>
    public static class APIPreWeldingDailyService
    {
        #region 根据获取详细信息
        /// <summary>
        ///  根据获取详细信息
        /// </summary>
        /// <param name="preWeldingDailyId"></param>
        /// <returns></returns>
        public static Model.HJGL_WeldingDaily getWeldingDailyInfo(string weldingDailyId, string unitWorkId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.HJGL_WeldingDaily weldingDaily = new Model.HJGL_WeldingDaily();
                string unitWorkName = string.Empty, unitName = string.Empty, projectId=string.Empty;
                var unitWork = db.WBS_UnitWork.FirstOrDefault(x => x.UnitWorkId == unitWorkId);
                if (unitWork != null)
                {
                    projectId = unitWork.ProjectId;
                    unitWorkName = unitWork.UnitWorkName;
                    var unit = db.Base_Unit.FirstOrDefault(x => x.UnitId == unitWork.UnitId);
                    if (unit != null)
                    {
                        unitName = unit.UnitId + "," + unit.UnitName;
                    }
                }
                if (string.IsNullOrEmpty(weldingDailyId))
                {
                    string perfix = string.Format("{0:yyyyMMdd}", System.DateTime.Now) + "-";
                    weldingDaily.WeldingDailyCode = BLL.SQLHelper.RunProcNewId("SpGetThreeNumber", "dbo.HJGL_WeldingDaily", "WeldingDailyCode", projectId, perfix);
                    weldingDaily.UnitId = unitName;
                    weldingDaily.UnitWorkId = unitWorkName;
                }
                else
                {
                    weldingDaily = db.HJGL_WeldingDaily.FirstOrDefault(x => x.WeldingDailyId == weldingDailyId);
                    weldingDaily.UnitId = unitName;
                    weldingDaily.UnitWorkId = unitWorkName;
                }
                Model.HJGL_WeldingDaily res = new Model.HJGL_WeldingDaily();
                res.WeldingDailyId = weldingDaily.WeldingDailyId;
                res.WeldingDailyCode = weldingDaily.WeldingDailyCode;
                res.ProjectId = weldingDaily.ProjectId;
                res.UnitWorkId = weldingDaily.UnitWorkId;
                res.UnitId = weldingDaily.UnitId;
                res.WeldingDate = weldingDaily.WeldingDate;
                res.Tabler = weldingDaily.Tabler;
                res.TableDate = weldingDaily.TableDate;
                res.Remark = weldingDaily.Remark;
                return res;
            }
        }
        #endregion

        #region 根据日期、单位工程Id获取日报记录
        /// <summary>
        /// 根据日期、单位工程Id获取日报记录
        /// </summary>
        /// <param name="unitWorkId">单位工程Id</param>
        /// <param name="projectId">项目Id</param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> GetWeldingDailyList(string date, string unitWorkId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var dataList = from x in Funs.DB.HJGL_WeldingDaily
                               where x.UnitWorkId == unitWorkId
                                    && x.WeldingDate < Convert.ToDateTime(date + "-01").AddMonths(1)
                                    && x.WeldingDate >= Convert.ToDateTime(date + "-01")
                               orderby x.WeldingDailyCode descending
                               select x;
                var getDataLists = (from x in dataList
                                    orderby x.WeldingDailyCode
                                    select new Model.BaseInfoItem
                                    {
                                        BaseInfoId = x.WeldingDailyId,
                                        BaseInfoCode = x.WeldingDailyCode,
                                    }).ToList();
                return getDataLists;
            }
        }
        #endregion

        #region 获取焊接任务单信息
        public static List<Model.View_HJGL_WeldingTask> GetWeldingTasks(string weldingDailyId, string unitWorkId, string date, string projectId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                var list = from x in db.View_HJGL_WeldingTask where x.ProjectId == projectId select x;
                List<string> ids = new List<string>();
                //IQueryable<Model.View_HJGL_WeldingTask> q = db.View_HJGL_WeldingTask;
                var task = new List<Model.View_HJGL_WeldingTask>();
                if (!string.IsNullOrEmpty(weldingDailyId))
                {
                    var weldJointIds = (from x in list
                                        where (x.UnitWorkId == unitWorkId && x.TaskDate.Value.Date <= Convert.ToDateTime(date)
                               && x.WeldingDailyId == null && x.CoverWelderId != null && x.BackingWelderId != null) || x.WeldingDailyId == weldingDailyId
                                        select x.WeldJointId).Distinct().ToList();
                    foreach (var weldJointId in weldJointIds)
                    {
                        task.Add(list.FirstOrDefault(x => x.WeldJointId == weldJointId));
                    }
                    ids = (from x in Funs.DB.View_HJGL_WeldingTask
                               where x.WeldingDailyId == weldingDailyId
                               select x.WeldTaskId).ToList();
                }
                else
                {
                    var weldJointIds = (from x in list
                                        where x.UnitWorkId == unitWorkId && x.TaskDate.Value.Date <= Convert.ToDateTime(date)
                               && x.WeldingDailyId == null && x.CoverWelderId != null && x.BackingWelderId != null
                                        select x.WeldJointId).Distinct().ToList();
                    foreach (var weldJointId in weldJointIds)
                    {
                        task.Add(list.FirstOrDefault(x => x.WeldJointId == weldJointId && x.CoverWelderId != null));
                    }
                }
                //var qres = from x in q
                //           where x.ApproveDate != null
                //           orderby x.ADate ascending
                //           select new
                //           {
                //               x.ApproveId,
                //               x.ApproveDate,
                //               x.Opinion,
                //               x.ApproveMan,
                //               x.ApproveType,
                //               x.ItemEndCheckListId,
                //               x.ApproveManName,
                //               x.StateStr,
                //           };
                //var list = qres.ToList();
                List<Model.View_HJGL_WeldingTask> res = new List<Model.View_HJGL_WeldingTask>();
                task = task.OrderBy(x => x.PipelineCode).OrderBy(x => x.WeldJointCode).ToList();
                foreach (var item in task)
                {
                    Model.View_HJGL_WeldingTask x = new Model.View_HJGL_WeldingTask();
                    x.WeldTaskId = item.WeldTaskId;
                    x.WeldJointId = item.WeldJointId;
                    x.CoverWelderId = item.CoverWelderId;
                    x.BackingWelderId = item.BackingWelderId;
                    x.CoverWelderCode = item.CoverWelderCode;
                    x.BackingWelderCode = item.BackingWelderCode;
                    x.JointAttribute = item.JointAttribute;
                    x.WeldingMode = item.WeldingMode;
                    x.ProjectId = item.ProjectId;
                    x.UnitWorkId = item.UnitWorkId;
                    x.UnitId = item.UnitId;
                    x.TaskDate = item.TaskDate;
                    x.Tabler = item.Tabler;
                    x.TableDate = item.TableDate;
                    x.WeldJointCode = item.WeldJointCode;
                    x.Dia = item.Dia;
                    x.Thickness = item.Thickness;
                    x.Size = item.Size;
                    x.WeldingLocationId = item.WeldingLocationId;
                    if (ids.Contains(item.WeldTaskId))
                    {
                        x.CanWelderId = "1";
                    }
                    else
                    {
                        x.CanWelderId = "0";
                    }
                    x.IsWelding = item.IsWelding;
                    x.PipelineCode = item.PipelineCode;
                    x.WeldTypeCode = item.WeldTypeCode;
                    x.WeldingMethodCode = item.WeldingMethodCode;
                    x.WeldingLocationCode = item.WeldingLocationCode;
                    x.CanWelderCode = item.CanWelderCode;
                    x.WeldingRodCode = item.WeldingRodCode;
                    x.CanWeldingRodName = item.CanWeldingRodName;
                    x.CanWeldingWireName = item.CanWeldingWireName;
                    x.WeldingWireCode = item.WeldingWireCode;
                    x.WeldingDailyId = item.WeldingDailyId;
                    res.Add(x);
                }
                return res;
            }
        }
        #endregion

        #region 获取焊接日报列表信息
        /// <summary>
        /// 获取施工方案列表信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.HJGL_PreWeldingDailyItem> getPreWeldingDailyList(string projectId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getList = from x in db.HJGL_PreWeldingDaily
                              where x.ProjectId == projectId
                              orderby x.WeldingDate descending
                              select new Model.HJGL_PreWeldingDailyItem
                              {
                                  PreWeldingDailyId = x.PreWeldingDailyId,
                                  ProjectId = x.ProjectId,
                                  UnitWorkId = x.UnitWorkId,
                                  UnitWorkName = db.WBS_UnitWork.First(y => y.UnitWorkId == x.UnitWorkId).UnitWorkName,
                                  UnitId = x.UnitId,
                                  UnitName = db.Base_Unit.First(u => u.UnitId == x.UnitId).UnitName,
                              };
                return getList.ToList();
            }
        }
        #endregion

        #region 保存焊接日报
        /// <summary>
        /// 保存焊接日报
        /// </summary>
        /// <param name="newItem">焊接日报</param>
        /// <returns></returns>
        public static void SaveWeldingDaily(Model.HJGL_WeldingDaily newItem)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.HJGL_WeldingDaily newP = new Model.HJGL_WeldingDaily
                {
                    WeldingDailyId = newItem.WeldingDailyId,
                    WeldingDailyCode = newItem.WeldingDailyCode,
                    ProjectId = newItem.ProjectId,
                    UnitWorkId = newItem.UnitWorkId,
                    UnitId = newItem.UnitId,
                    WeldingDate = newItem.WeldingDate,
                    Tabler = newItem.Tabler,
                    TableDate = newItem.TableDate,
                    Remark = newItem.Remark,
                };

                var updateItem = db.HJGL_WeldingDaily.FirstOrDefault(x => x.WeldingDailyId == newItem.WeldingDailyId);
                if (updateItem == null)
                {
                    db.HJGL_WeldingDaily.InsertOnSubmit(newP);
                    db.SubmitChanges();
                }
                else
                {
                    db.SubmitChanges();
                }
            }
        }
        #endregion

        #region 保存焊接日报明细
        /// <summary>
        /// 保存焊接日报明细
        /// </summary>
        /// <param name="newItem">焊接日报</param>
        /// <returns></returns>
        public static void SaveWeldingDailyDetail(string weldingDailyId, string selectIds, string notSelectIds, string projectId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                string[] ids = selectIds.Split(',');
                string[] notIds = notSelectIds.Split(',');
                var weldJointView = (from x in db.View_HJGL_WeldJoint where x.WeldingDailyId == weldingDailyId orderby x.PipelineCode, x.WeldJointCode select x).ToList();
                var daily = db.HJGL_WeldingDaily.FirstOrDefault(x => x.WeldingDailyId == weldingDailyId);
                // 获取组批条件
                var batchC = BLL.Project_SysSetService.GetSysSetBySetId("5", projectId);
                if (batchC != null)
                {
                    string batchCondition = batchC.SetValue;
                    // 新建日报
                    if (weldJointView.Count() == 0)
                    {
                        foreach (string id in ids)
                        {

                            var t = BLL.WeldTaskService.GetWeldTaskById(id);
                            if (t != null)
                            {
                                InsertWeldingDailyItem(t.WeldJointId, t.CoverWelderId, t.BackingWelderId, t.JointAttribute, daily.WeldingDate, batchCondition, true, weldingDailyId, projectId);
                            }
                        }
                    }
                    else
                    {
                        foreach (string id in ids)
                        {
                            var t = BLL.WeldTaskService.GetWeldTaskById(id);
                            if (t != null)
                            {
                                InsertWeldingDailyItem(t.WeldJointId, t.CoverWelderId, t.BackingWelderId, t.JointAttribute, daily.WeldingDate, batchCondition, true, weldingDailyId, projectId);
                            }
                        }
                        foreach (string notId in notIds)
                        {
                            var t = BLL.WeldTaskService.GetWeldTaskById(notId);
                            if (t != null)
                            {
                                DeleteWeldingDailyItem(t.WeldJointId, t.CoverWelderId, t.BackingWelderId, t.JointAttribute, daily.WeldingDate, batchCondition, true);
                            }
                        }
                    }
                    // 日报已存在的情况  暂时
                    //else
                    //{
                    //    var weldJoints = from x in weldJointView select x.WeldJointId;
                    //    foreach (var item in GetWeldingDailyItem)
                    //    {
                    //        // 如日报明细存在则只更新焊口信息，如进批条件改变，则只有删除后再重新增加
                    //        if (weldJoints.Contains(item.WeldJointId))
                    //        {
                    //            var newWeldJoint = BLL.WeldJointService.GetWeldJointByWeldJointId(item.WeldJointId);
                    //            newWeldJoint.WeldingDailyId = this.WeldingDailyId;
                    //            newWeldJoint.WeldingDailyCode = this.txtWeldingDailyCode.Text.Trim();
                    //            newWeldJoint.CoverWelderId = item.CoverWelderId;
                    //            newWeldJoint.BackingWelderId = item.BackingWelderId;

                    //            if (!string.IsNullOrEmpty(item.JointAttribute))
                    //            {
                    //                newWeldJoint.JointAttribute = item.JointAttribute;

                    //            }
                    //            BLL.WeldJointService.UpdateWeldJoint(newWeldJoint);

                    //            //更新焊口号 修改固定焊口号后 +G
                    //            BLL.WeldJointService.UpdateWeldJointAddG(newWeldJoint.WeldJointId, newWeldJoint.JointAttribute, Const.BtnAdd);

                    //        }
                    //        else
                    //        {
                    //            errlog += InsertWeldingDailyItem(item, newWeldingDaily.WeldingDate, batchCondition, true);
                    //        }
                    //    }
                    //}
                }
                //更新焊口属性
                foreach (var id in ids)
                {
                    var t = BLL.WeldTaskService.GetWeldTaskById(id);
                    if (t != null)
                    {
                        var newWeldJoint = BLL.WeldJointService.GetWeldJointByWeldJointId(t.WeldJointId);
                        if (newWeldJoint != null)
                        {
                            BLL.WeldJointService.UpdateWeldJointAddG(newWeldJoint.WeldJointId, newWeldJoint.JointAttribute, Const.BtnAdd);
                        }
                    }
                }
            }
        }
        #endregion

        #region 保存Solution_ConstructSolution
        /// <summary>
        /// 保存Solution_ConstructSolution
        /// </summary>
        /// <param name="newItem">施工方案</param>
        /// <returns></returns>
        public static void SavePreWeldingDaily(Model.HJGL_PreWeldingDailyItem newItem)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_PreWeldingDaily newP = new Model.HJGL_PreWeldingDaily
            {
                PreWeldingDailyId = newItem.PreWeldingDailyId,
                ProjectId = newItem.ProjectId,
                UnitWorkId = newItem.UnitWorkId,
                UnitId = newItem.UnitId,
            };

            var updateItem = db.HJGL_PreWeldingDaily.FirstOrDefault(x => x.PreWeldingDailyId == newItem.PreWeldingDailyId);
            if (updateItem == null)
            {

                newP.PreWeldingDailyId = SQLHelper.GetNewID();
                db.HJGL_PreWeldingDaily.InsertOnSubmit(newP);
                db.SubmitChanges();
            }
            else
            {
                ///   更新
                ///   //
            }

            //if (newItem.BaseInfoItem != null && newItem.BaseInfoItem.Count() > 0)
            //{
            //    foreach (var item in newItem.BaseInfoItem)
            //    {
            //       // var a =item.BaseInfoId,

            //    }
            //}
        }
        #endregion

        #region 日报明细插入（更新焊口信息），组批等
        /// <summary>
        /// 日报明细插入（更新焊口信息），组批等
        /// </summary>
        /// <param name="item"></param>
        /// <param name="weldingDailyId"></param>
        /// <returns></returns>
        private static string InsertWeldingDailyItem(string weldJointId, string coverWelderId, string backingWelderId, string jointAttribute, DateTime? weldingDate, string batchCondition, bool isSave, string weldingDailyId, string projectId)
        {
            string errlog = string.Empty;
            string[] condition = batchCondition.Split('|');
            var project = BLL.ProjectService.GetProjectByProjectId(projectId);
            var newWeldJoint = BLL.WeldJointService.GetWeldJointByWeldJointId(weldJointId);
            var pipeline = BLL.PipelineService.GetPipelineByPipelineId(newWeldJoint.PipelineId);
            var unitWork = BLL.UnitWorkService.GetUnitWorkByUnitWorkId(pipeline.UnitWorkId);
            var unit = BLL.UnitService.GetUnitByUnitId(pipeline.UnitId);
            var ndt = BLL.Base_DetectionTypeService.GetDetectionTypeByDetectionTypeId(newWeldJoint.DetectionTypeId);
            var ndtr = BLL.Base_DetectionRateService.GetDetectionRateByDetectionRateId(pipeline.DetectionRateId);
            var weldingDaily = BLL.WeldingDailyService.GetPipeline_WeldingDailyByWeldingDailyId(weldingDailyId);
            if (newWeldJoint != null && string.IsNullOrEmpty(newWeldJoint.WeldingDailyId))
            {
                if (!string.IsNullOrEmpty(coverWelderId) && !string.IsNullOrEmpty(backingWelderId))
                {
                    if (isSave)
                    {
                        newWeldJoint.WeldingDailyId = weldingDailyId;
                        newWeldJoint.WeldingDailyCode = weldingDaily.WeldingDailyCode;
                        newWeldJoint.CoverWelderId = coverWelderId;
                        newWeldJoint.BackingWelderId = backingWelderId;
                        //if (item.WeldingLocationId != Const._Null)
                        //{
                        //    newWeldJoint.WeldingLocationId = item.WeldingLocationId;
                        //}
                        newWeldJoint.JointAttribute = jointAttribute;
                        BLL.WeldJointService.UpdateWeldJoint(newWeldJoint);

                        // 更新焊口号 修改固定焊口号后 +G
                        BLL.WeldJointService.UpdateWeldJointAddG(newWeldJoint.WeldJointId, newWeldJoint.JointAttribute, Const.BtnAdd);

                        // 进批
                        //BLL.Batch_PointBatchItemService.InsertPointBatch(this.ProjectId, this.drpUnit.SelectedValue, this.drpUnitWork.SelectedValue, item.CoverWelderId, item.WeldJointId, weldingDate);
                        bool isPass = true;
                        foreach (string c in condition)
                        {
                            if (c == "1")
                            {
                                if (string.IsNullOrEmpty(pipeline.UnitWorkId))
                                {
                                    isPass = false;
                                    break;

                                }
                            }
                            if (c == "2")
                            {
                                if (string.IsNullOrEmpty(pipeline.UnitId))
                                {
                                    isPass = false;
                                    break;

                                }
                            }
                            if (c == "3")
                            {
                                if (string.IsNullOrEmpty(newWeldJoint.DetectionTypeId))
                                {
                                    isPass = false;
                                    break;
                                }
                            }
                            if (c == "4")
                            {
                                if (string.IsNullOrEmpty(pipeline.DetectionRateId))
                                {
                                    isPass = false;
                                    break;
                                }
                            }
                            if (c == "5")
                            {
                                if (string.IsNullOrEmpty(pipeline.PipingClassId))
                                {
                                    isPass = false;
                                    break;
                                }
                            }
                            // 6是管线，7是焊工都不可能为空，这里就不判断了
                        }

                        if (isPass)
                        {
                            string strSql = @"SELECT PointBatchId FROM dbo.HJGL_Batch_PointBatch
                                                 WHERE (EndDate IS NULL OR EndDate ='')
                                                  AND ProjectId = @ProjectId 
                                                  AND UnitWorkId = @UnitWorkId AND UnitId =@UnitId 
                                                  AND DetectionTypeId =@DetectionTypeId
                                                  AND DetectionRateId =@DetectionRateId";
                            List<SqlParameter> listStr = new List<SqlParameter>();
                            listStr.Add(new SqlParameter("@ProjectId", projectId));
                            listStr.Add(new SqlParameter("@UnitWorkId", pipeline.UnitWorkId));
                            listStr.Add(new SqlParameter("@UnitId", pipeline.UnitId));
                            listStr.Add(new SqlParameter("@DetectionTypeId", newWeldJoint.DetectionTypeId));
                            listStr.Add(new SqlParameter("@DetectionRateId", pipeline.DetectionRateId));

                            // 5,6,7项为可选项
                            if (condition.Contains("5"))
                            {
                                strSql += " AND PipingClassId =@PipingClassId";
                                listStr.Add(new SqlParameter("@PipingClassId", pipeline.PipingClassId));
                            }
                            if (condition.Contains("6"))
                            {
                                strSql += " AND PipelineId =@PipelineId";
                                listStr.Add(new SqlParameter("@PipelineId", newWeldJoint.PipelineId));
                            }
                            if (condition.Contains("7"))
                            {
                                strSql += " AND WelderId =@WelderId";
                                listStr.Add(new SqlParameter("@WelderId", newWeldJoint.CoverWelderId));
                            }

                            SqlParameter[] parameter = listStr.ToArray();
                            DataTable batchInfo = SQLHelper.GetDataTableRunText(strSql, parameter);

                            string batchId = string.Empty;
                            if (batchInfo.Rows.Count == 0)
                            {
                                Model.HJGL_Batch_PointBatch batch = new Model.HJGL_Batch_PointBatch();
                                batch.PointBatchId = SQLHelper.GetNewID(typeof(Model.HJGL_Batch_PointBatch));
                                batchId = batch.PointBatchId;
                                string perfix = project.ProjectCode + "-" + unitWork.UnitWorkCode + "-GD-DK-" + ndt.DetectionTypeCode + "-" + ndtr.DetectionRateValue.ToString() + "%-";
                                batch.PointBatchCode = BLL.SQLHelper.RunProcNewIdByProjectId("SpGetNewCode4ByProjectId", "dbo.HJGL_Batch_PointBatch", "PointBatchCode", projectId, perfix);

                                batch.ProjectId = projectId;
                                batch.UnitWorkId = pipeline.UnitWorkId;
                                batch.BatchCondition = batchCondition;
                                batch.UnitId = pipeline.UnitId;
                                batch.DetectionTypeId = newWeldJoint.DetectionTypeId;
                                batch.DetectionRateId = pipeline.DetectionRateId;
                                if (condition.Contains("5"))
                                {
                                    batch.PipingClassId = pipeline.PipingClassId;
                                }
                                if (condition.Contains("6"))
                                {
                                    batch.PipelineId = newWeldJoint.PipelineId;
                                }
                                if (condition.Contains("7"))
                                {
                                    batch.WelderId = newWeldJoint.CoverWelderId;
                                }
                                batch.StartDate = DateTime.Now;
                                BLL.PointBatchService.AddPointBatch(batch);
                            }
                            else
                            {
                                batchId = batchInfo.Rows[0][0].ToString();
                            }

                            var b = BLL.PointBatchDetailService.GetBatchDetailByJotId(weldJointId);
                            if (b == null)
                            {
                                try
                                {
                                    Model.HJGL_Batch_PointBatchItem batchDetail = new Model.HJGL_Batch_PointBatchItem();
                                    string pointBatchItemId = SQLHelper.GetNewID(typeof(Model.HJGL_Batch_PointBatchItem));
                                    batchDetail.PointBatchItemId = pointBatchItemId;
                                    batchDetail.PointBatchId = batchId;
                                    batchDetail.WeldJointId = weldJointId;
                                    batchDetail.WeldingDate = weldingDate;
                                    batchDetail.CreatDate = DateTime.Now;
                                    BLL.Funs.DB.HJGL_Batch_PointBatchItem.InsertOnSubmit(batchDetail);
                                    BLL.Funs.DB.SubmitChanges();

                                    // 焊工首道口RT必点 
                                    var joints = from x in Funs.DB.HJGL_Batch_PointBatchItem
                                                 join y in Funs.DB.HJGL_Batch_PointBatch on x.PointBatchId equals y.PointBatchId
                                                 join z in Funs.DB.Base_DetectionType on y.DetectionTypeId equals z.DetectionTypeId
                                                 join j in Funs.DB.HJGL_WeldJoint on x.WeldJointId equals j.WeldJointId
                                                 where z.DetectionTypeCode == "RT"
                                                 && j.CoverWelderId == newWeldJoint.CoverWelderId
                                                 select x;
                                    if (joints.Count() <= 1)
                                    {
                                        BLL.PointBatchDetailService.UpdatePointBatchDetail(pointBatchItemId, "1", System.DateTime.Now);
                                        BLL.PointBatchDetailService.UpdateWelderFirst(pointBatchItemId, true);
                                    }
                                }
                                catch
                                {

                                }
                            }
                        }
                        else
                        {
                            errlog += "焊口【" + newWeldJoint.WeldJointCode + "】组批条件不能为空。";
                        }

                    }

                }
                else
                {
                    errlog = "焊口" + "【" + newWeldJoint.WeldJointCode + "】" + "未选择焊工";
                }
            }

            return errlog;
        }
        #endregion

        #region 日报明细删除（更新焊口信息），组批等
        /// <summary>
        /// 日报明细删除（更新焊口信息），组批等
        /// </summary>
        /// <param name="item"></param>
        /// <param name="weldingDailyId"></param>
        /// <returns></returns>
        private static string DeleteWeldingDailyItem(string weldJointId, string coverWelderId, string backingWelderId, string jointAttribute, DateTime? weldingDate, string batchCondition, bool isSave)
        {
            string errlog = string.Empty;
            var newWeldJoint = BLL.WeldJointService.GetWeldJointByWeldJointId(weldJointId);
            var pipeline = BLL.PipelineService.GetPipelineByPipelineId(newWeldJoint.PipelineId);
            var unit = BLL.UnitService.GetUnitByUnitId(pipeline.UnitId);
            var ndt = BLL.Base_DetectionTypeService.GetDetectionTypeByDetectionTypeId(newWeldJoint.DetectionTypeId);
            var ndtr = BLL.Base_DetectionRateService.GetDetectionRateByDetectionRateId(pipeline.DetectionRateId);

            if (newWeldJoint != null && !string.IsNullOrEmpty(newWeldJoint.WeldingDailyId))
            {
                var isTrust = from x in Funs.DB.HJGL_Batch_BatchTrustItem
                              where x.WeldJointId == weldJointId
                              select x; ;
                if (isTrust.Count() == 0)
                {
                    var updateWeldJoint = BLL.WeldJointService.GetWeldJointByWeldJointId(weldJointId);
                    if (updateWeldJoint != null)
                    {
                        updateWeldJoint.WeldingDailyId = null;
                        updateWeldJoint.WeldingDailyCode = null;
                        updateWeldJoint.CoverWelderId = null;
                        updateWeldJoint.BackingWelderId = null;
                        BLL.WeldJointService.UpdateWeldJoint(updateWeldJoint);

                        var pointBatchItems = from x in Funs.DB.HJGL_Batch_PointBatchItem where x.WeldJointId == weldJointId select x;
                        string pointBatchId = pointBatchItems.FirstOrDefault().PointBatchId;

                        // 删除焊口所在批明细信息
                        BLL.PointBatchDetailService.DeleteBatchDetail(weldJointId);

                        // 删除批信息
                        var batch = from x in Funs.DB.HJGL_Batch_PointBatchItem where x.PointBatchId == pointBatchId select x;
                        if (pointBatchId != null && batch.Count() == 0)
                        {
                            BLL.PointBatchService.DeleteBatch(pointBatchId);
                        }
                        //BLL.Batch_NDEItemService.DeleteAllNDEInfoToWeldJoint(item.WeldJointId);
                    }
                    //BLL.Sys_LogService.AddLog(BLL.Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_WeldReportMenuId, Const.BtnDelete, weldingDailyId);
                }
                else
                {
                    errlog += "焊口【" + newWeldJoint.WeldJointCode + "】已进委托单了，不能删除。";
                }
            }

            return errlog;
        }
        #endregion
    }
}
