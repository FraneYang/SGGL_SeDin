using System;
using System.Collections.Generic;
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
        public static Model.HJGL_PreWeldingDailyItem getPreWeldingDailyInfo(string preWeldingDailyId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getInfo = from x in db.HJGL_PreWeldingDaily
                              where x.PreWeldingDailyId == preWeldingDailyId
                              select new Model.HJGL_PreWeldingDailyItem
                              {
                                  PreWeldingDailyId = x.PreWeldingDailyId,
                                  ProjectId = x.ProjectId,
                                  UnitWorkId = x.UnitWorkId,
                                  UnitWorkName = db.WBS_UnitWork.First(y => y.UnitWorkId == x.UnitWorkId).UnitWorkName,
                                  UnitId = x.UnitId,
                                  UnitName = db.Base_Unit.First(u => u.UnitId == x.UnitId).UnitName,
                              };
                return getInfo.FirstOrDefault();
            }
        }
        #endregion

        #region 获取焊接任务单信息
        public static List<Model.View_HJGL_WeldingTask> GetWeldingTasks(string weldingDailyId, string unitWorkId, string date,string projectId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                var list = from x in db.View_HJGL_WeldingTask where x.ProjectId == projectId select x;
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
                    x.IsWelding = item.IsWelding;
                    x.PipelineCode = item.PipelineCode;
                    x.WeldTypeCode = item.WeldTypeCode;
                    x.WeldingMethodCode = item.WeldingMethodCode;
                    x.WeldingLocationCode = item.WeldingLocationCode;
                    x.CanWelderCode = item.CanWelderCode;
                    x.CanWelderId = item.CanWelderId;
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
    }
}
