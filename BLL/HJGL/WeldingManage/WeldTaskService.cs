using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class WeldTaskService
    {
        /// <summary>
        ///获取焊接任务单信息
        /// </summary>
        /// <returns></returns>
        public static Model.HJGL_WeldTask GetWeldTaskById(string WeldTaskId)
        {
            return Funs.DB.HJGL_WeldTask.FirstOrDefault(e => e.WeldTaskId == WeldTaskId);
        }
        /// <summary>
        /// 根据单位工程Id获取焊接任务单明细信息
        /// </summary>
        /// <param name="ManagerTotalId"></param>
        /// <returns></returns>
        public static List<Model.SpWeldingDailyItem> GetWeldingTaskItem(string UnitWorkId)
        {
            List<Model.SpWeldingDailyItem> returnViewMatch = new List<Model.SpWeldingDailyItem>();
            var a = from x in Funs.DB.HJGL_WeldTask
                    join y in Funs.DB.HJGL_WeldJoint on x.WeldJointId equals y.WeldJointId
                    join z in Funs.DB.HJGL_Pipeline on y.PipelineId equals z.PipelineId
                    where x.UnitWorkId == UnitWorkId select x;
            var weldlineLists = from x in Funs.DB.HJGL_WeldTask
                                join y in Funs.DB.HJGL_WeldJoint on x.WeldJointId equals y.WeldJointId
                                join z in Funs.DB.HJGL_Pipeline on y.PipelineId equals z.PipelineId
                                join w in Funs.DB.Base_WeldType on y.WeldTypeId equals w.WeldTypeId
                                join m in Funs.DB.Base_WeldingMethod on y.WeldingMethodId equals m.WeldingMethodId
                                join l in Funs.DB.Base_WeldingLocation on y.WeldingLocationId equals l.WeldingLocationId
                                where x.UnitWorkId == UnitWorkId
                                select new
                                { x.WeldJointId,x.CoverWelderId,x.BackingWelderId,x.JointAttribute,x.WelderCodeStr,x.WeldingMode,
                                y.WeldJointCode,y.Dia,y.Thickness,y.Size,y.WeldingLocationId,z.PipelineCode,w.WeldTypeCode,m.WeldingMethodCode,l.WeldingLocationCode};
            if (weldlineLists.Count() > 0)
            {
                foreach (var item in weldlineLists)
                {
                    Model.SpWeldingDailyItem newWeldTask = new Model.SpWeldingDailyItem();
                    newWeldTask.WeldJointId = item.WeldJointId;
                    newWeldTask.WeldJointCode = item.WeldJointCode;
                    newWeldTask.PipelineCode = item.PipelineCode;
                    newWeldTask.CoverWelderId = item.CoverWelderId;
                    newWeldTask.BackingWelderId = item.BackingWelderId;
                    newWeldTask.WeldTypeCode = item.WeldTypeCode;
                    newWeldTask.WeldingLocationId = item.WeldingLocationId;
                    newWeldTask.WeldingLocationCode = item.WeldingLocationCode;
                    newWeldTask.JointAttribute = item.JointAttribute;
                    newWeldTask.Size = item.Size;
                    newWeldTask.Dia = item.Dia;
                    newWeldTask.Thickness = item.Thickness;
                    newWeldTask.WeldingMethodCode = item.WeldingMethodCode;
                    newWeldTask.WeldingMode = item.WeldingMode;
                    returnViewMatch.Add(newWeldTask);
                }
            }

            return returnViewMatch;
        }
        /// <summary>
        /// 增加焊接任务单信息
        /// </summary>
        /// <param name="WeldTask"></param>
        public static void AddWeldTask(Model.HJGL_WeldTask WeldTask)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_WeldTask newWeldTask=new Model.HJGL_WeldTask 
            {
                WeldTaskId = WeldTask.WeldTaskId,
                ProjectId = WeldTask.ProjectId,
                UnitId = WeldTask.UnitId,
                UnitWorkId = WeldTask.UnitWorkId,
                WeldJointId = WeldTask.WeldJointId,
                TaskDate= WeldTask.TaskDate,
                CoverWelderId= WeldTask.CoverWelderId,
                BackingWelderId= WeldTask.BackingWelderId,
                JointAttribute= WeldTask.JointAttribute,
                WelderCodeStr= WeldTask.WelderCodeStr,
                Tabler = WeldTask.Tabler,
                TableDate = WeldTask.TableDate,
                WeldingMode = WeldTask.WeldingMode,
            };

            db.HJGL_WeldTask.InsertOnSubmit(newWeldTask);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改焊接任务单信息 
        /// </summary>
        /// <param name="WeldTask"></param>
        public static void UpdateWeldTask(Model.HJGL_WeldTask WeldTask)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_WeldTask newWeldTask = db.HJGL_WeldTask.FirstOrDefault(e => e.WeldTaskId == WeldTask.WeldTaskId);
            if (newWeldTask != null)
            {
                newWeldTask.WeldTaskId = WeldTask.WeldTaskId;
                newWeldTask.ProjectId = WeldTask.ProjectId;
                newWeldTask.UnitId = WeldTask.UnitId;
                newWeldTask.UnitWorkId = WeldTask.UnitWorkId;
                newWeldTask.WeldJointId = WeldTask.WeldJointId;
                newWeldTask.TaskDate = WeldTask.TaskDate;
                newWeldTask.CoverWelderId = WeldTask.CoverWelderId;
                newWeldTask.BackingWelderId = WeldTask.BackingWelderId;
                newWeldTask.JointAttribute = WeldTask.JointAttribute;
                newWeldTask.WelderCodeStr = WeldTask.WelderCodeStr;
                newWeldTask.Tabler = WeldTask.Tabler;
                newWeldTask.TableDate = WeldTask.TableDate;
                newWeldTask.WeldingMode = WeldTask.WeldingMode;
                db.SubmitChanges();
            }
        }
        /// <summary>
        /// 查找后返回集合增加到列表集团中
        /// </summary>
        /// <param name="hdItemsString"></param>
        /// <returns></returns>
        public static List<Model.SpWeldingDailyItem> GetWeldTaskListAddItem(string hdString)
        {
            List<Model.SpWeldingDailyItem> returnViewMatch = new List<Model.SpWeldingDailyItem>(); //= getWeldReportItem;
            if (!string.IsNullOrEmpty(hdString))
            {
                List<string> totallist = Funs.GetStrListByStr(hdString, '@');
                foreach (var hdItemsString in totallist)
                {
                    List<string> list = Funs.GetStrListByStr(hdItemsString, '#');
                    if (list.Count() == 4)
                    {
                        string CoverWelderCode = string.Empty;  //盖面焊工号
                        string BackingWelderCode = string.Empty;   //打底焊工号
                        string weldingLocationCode = string.Empty;
                        string weldingLocationId = list[0];
                        var loc = BLL.Base_WeldingLocationServie.GetWeldingLocationById(weldingLocationId);
                        if (loc != null)
                        {
                            weldingLocationCode = loc.WeldingLocationCode;
                        }
                        string jointAttribute = list[1];
                        string WeldingMode = list[2];
                        string weldlineIdLists = list[3];
                        List<string> weldlineIds = Funs.GetStrListByStr(weldlineIdLists, '|');
                        foreach (var weldlineItem in weldlineIds)
                        {
                            var jot = Funs.DB.View_HJGL_WeldJoint.FirstOrDefault(e => e.WeldJointId == weldlineItem);
                            if (jot != null)
                            {
                                Model.SpWeldingDailyItem newWeldReportItem = new Model.SpWeldingDailyItem();
                                newWeldReportItem.WeldJointId = jot.WeldJointId;
                                newWeldReportItem.WeldJointCode = jot.WeldJointCode;
                                newWeldReportItem.PipelineCode = jot.PipelineCode;
                                newWeldReportItem.WeldTypeCode = jot.WeldTypeCode;
                                newWeldReportItem.JointArea = jot.JointArea;
                                newWeldReportItem.WeldingLocationId = weldingLocationId;
                                newWeldReportItem.WeldingLocationCode = weldingLocationCode;
                                newWeldReportItem.JointAttribute = jointAttribute;
                                newWeldReportItem.Size = jot.Size;
                                newWeldReportItem.Dia = jot.Dia;
                                newWeldReportItem.Thickness = jot.Thickness;
                                newWeldReportItem.WeldingMethodCode = jot.WeldingMethodCode;
                                newWeldReportItem.WeldingMode = WeldingMode;
                                // 如存在任务单，默认还是原任务单焊工
                                if (!string.IsNullOrEmpty(jot.WeldingDailyId))
                                {
                                    newWeldReportItem.CoverWelderId = jot.CoverWelderId;
                                    newWeldReportItem.BackingWelderId = jot.BackingWelderId;
                                    var welderCover = BLL.WelderService.GetWelderById(jot.CoverWelderId);
                                    if (welderCover != null)
                                    {
                                        newWeldReportItem.CoverWelderCode = welderCover.WelderCode;
                                    }
                                    var welderBacking = BLL.WelderService.GetWelderById(jot.BackingWelderId);
                                    if (welderBacking != null)
                                    {
                                        newWeldReportItem.BackingWelderCode = welderBacking.WelderCode;
                                    }
                                    newWeldReportItem.JointAttribute = jot.JointAttribute;

                                    newWeldReportItem.WeldingLocationId = jot.WeldingLocationId;
                                    var location = BLL.Base_WeldingLocationServie.GetWeldingLocationById(jot.WeldingLocationId);
                                    if (location != null)
                                    {
                                        newWeldReportItem.WeldingLocationCode = location.WeldingLocationCode;
                                    }

                                }
                                returnViewMatch.Add(newWeldReportItem);
                            }
                        }
                    }
                }
            }

            return returnViewMatch;
        }

        /// <summary>
        /// 根据Id删除一个焊接任务单明细信息
        /// </summary>
        /// <param name="WeldingDailyId"></param>
        public static void DeleteWeldingTask(string WeldTaskId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_WeldTask delWeldTask = db.HJGL_WeldTask.FirstOrDefault(e => e.WeldTaskId == WeldTaskId);
            if (delWeldTask != null)
            {
                db.HJGL_WeldTask.DeleteOnSubmit(delWeldTask);
                db.SubmitChanges();
            }
        }
    }
}
