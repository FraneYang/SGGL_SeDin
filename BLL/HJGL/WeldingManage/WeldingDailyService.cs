using Model;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public partial class WeldingDailyService
    {
        /// <summary>
        ///获取焊接日报信息
        /// </summary>
        /// <returns></returns>
        public static Model.HJGL_WeldingDaily GetPipeline_WeldingDailyByWeldingDailyId(string WeldingDailyId)
        {
            return Funs.DB.HJGL_WeldingDaily.FirstOrDefault(e => e.WeldingDailyId == WeldingDailyId);
        }

        /// <summary>
        /// 根据焊接日报主键获取焊接日报信息
        /// </summary>
        /// <param name="weldReportNo">焊接日报编号</param>
        /// <returns>焊接日报信息</returns>
        public static bool IsExistWeldingDailyCode(string weldingDailyCode, string weldingDailyId, string projectId)
        {
            var q = Funs.DB.HJGL_WeldingDaily.FirstOrDefault(x => x.WeldingDailyCode == weldingDailyCode && x.ProjectId == projectId && x.WeldingDailyId != weldingDailyId);
            if (q != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据焊接日报Id获取焊接日报明细信息
        /// </summary>
        /// <param name="ManagerTotalId"></param>
        /// <returns></returns>
        public static List<Model.SpWeldingDailyItem> GetWeldingDailyItem(string weldingDailyId)
        {
            List<Model.SpWeldingDailyItem> returnViewMatch = new List<Model.SpWeldingDailyItem>();
            var weldlineLists = from x in Funs.DB.View_HJGL_WeldJoint
                                where x.WeldingDailyId == weldingDailyId
                                select x;
            if (weldlineLists.Count() > 0)
            {
                foreach (var item in weldlineLists)
                {
                    Model.SpWeldingDailyItem newWeldReportItem = new Model.SpWeldingDailyItem();
                    newWeldReportItem.WeldJointId = item.WeldJointId;
                    newWeldReportItem.WeldJointCode = item.WeldJointCode;
                    newWeldReportItem.PipelineCode = item.PipelineCode;
                    newWeldReportItem.CoverWelderCode = item.CoverWelderCode;
                    newWeldReportItem.CoverWelderId = item.CoverWelderId;
                    newWeldReportItem.BackingWelderCode = item.BackingWelderCode;
                    newWeldReportItem.BackingWelderId = item.BackingWelderId;
                    //newWeldReportItem.WeldTypeId = item.WeldTypeCode;
                    newWeldReportItem.JointArea = item.JointArea;
                    newWeldReportItem.WeldingLocationId = item.WeldingLocationId;
                    newWeldReportItem.WeldingLocationCode = item.WeldingLocationCode;
                    newWeldReportItem.JointAttribute = item.JointAttribute;
                    newWeldReportItem.Size = item.Size;
                    newWeldReportItem.Dia = item.Dia;
                    newWeldReportItem.Thickness = item.Thickness;
                    newWeldReportItem.WeldingMethodCode = item.WeldingMethodCode;
                    newWeldReportItem.WeldingWireCode = item.WeldingWireCode;
                    newWeldReportItem.WeldingRodCode = item.WeldingRodCode;
                    returnViewMatch.Add(newWeldReportItem);
                }
            }

            return returnViewMatch;
        }

        /// <summary>
        /// 查找后返回集合增加到列表集团中
        /// </summary>
        /// <param name="hdItemsString"></param>
        /// <returns></returns>
        public static List<Model.SpWeldingDailyItem> GetWeldReportAddItem(string hdString)
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

                        string welderLists = list[0];
                        List<string> welderIds = Funs.GetStrListByStr(welderLists, '|');
                        if (welderIds.Count() == 2)
                        {
                            var welderCell = BLL.WelderService.GetWelderById(welderIds[0]);
                            if (welderCell != null)
                            {
                                CoverWelderCode = welderCell.WelderCode;
                            }
                            var welderFloor = BLL.WelderService.GetWelderById(welderIds[1]);
                            if (welderFloor != null)
                            {
                                BackingWelderCode = welderFloor.WelderCode;
                            }
                        }

                        string weldingLocationCode = string.Empty;
                        string weldingLocationId = list[1];
                        var loc = BLL.Base_WeldingLocationServie.GetWeldingLocationById(weldingLocationId);
                        if (loc != null)
                        {
                            weldingLocationCode = loc.WeldingLocationCode;
                        }
                        string jointAttribute = list[2];

                        string weldlineIdLists = list[3];
                        List<string> weldlineIds = Funs.GetStrListByStr(weldlineIdLists, '|');
                        foreach (var weldlineItem in weldlineIds)
                        {
                            //if (returnViewMatch.FirstOrDefault(x => x.JOT_ID == jotItem) == null)
                            //{
                            var jot = Funs.DB.View_HJGL_WeldJoint.FirstOrDefault(e => e.WeldJointId == weldlineItem);
                            //var weldline = BLL.Pipeline_WeldJointService.GetViewWeldJointById(weldlineItem);
                            if (jot != null)
                            {
                                Model.SpWeldingDailyItem newWeldReportItem = new Model.SpWeldingDailyItem();
                                newWeldReportItem.WeldJointId = jot.WeldJointId;
                                newWeldReportItem.WeldJointCode = jot.WeldJointCode;
                                newWeldReportItem.PipelineCode = jot.PipelineCode;
                                newWeldReportItem.CoverWelderCode = CoverWelderCode;
                                newWeldReportItem.CoverWelderId = welderIds[0];
                                newWeldReportItem.BackingWelderCode = BackingWelderCode;
                                newWeldReportItem.BackingWelderId = welderIds[1];
                                newWeldReportItem.WeldTypeCode = jot.WeldTypeCode;
                                newWeldReportItem.JointArea = jot.JointArea;
                                newWeldReportItem.WeldingLocationId = weldingLocationId;
                                newWeldReportItem.WeldingLocationCode = weldingLocationCode;
                                newWeldReportItem.JointAttribute = jointAttribute;
                                newWeldReportItem.Size = jot.Size;
                                newWeldReportItem.Dia = jot.Dia;
                                newWeldReportItem.Thickness = jot.Thickness;
                                newWeldReportItem.WeldingMethodCode = jot.WeldingMethodCode;
                                // 如存在日报，默认还是原日报焊工
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
        /// 获取未焊接焊口信息
        /// </summary>
        /// <param name="ManagerTotalId"></param>
        /// <returns></returns>
        public static List<Model.SpWeldingDailyItem> GetWeldReportItemFind(string weldingDailyId, string pipelineId)
        {
            List<Model.SpWeldingDailyItem> returnViewMatch = new List<Model.SpWeldingDailyItem>();
            var weldlineLists = from x in Funs.DB.View_HJGL_NoWeldJointFind
                                where x.PipelineId == pipelineId &&
                                      (x.WeldingDailyId == null || x.WeldingDailyId == weldingDailyId)
                                select x;
            if (weldlineLists.Count() > 0)
            {
                foreach (var item in weldlineLists)
                {
                    Model.SpWeldingDailyItem newWeldReportItem = new Model.SpWeldingDailyItem();
                    newWeldReportItem.WeldJointId = item.WeldJointId;
                    newWeldReportItem.WeldJointCode = item.WeldJointCode;
                    newWeldReportItem.Dia = item.Dia;
                    newWeldReportItem.Thickness = item.Thickness;
                    newWeldReportItem.WeldingMethodCode = item.WeldingMethodCode;
                    newWeldReportItem.WeldTypeCode = item.WeldTypeCode;
                    returnViewMatch.Add(newWeldReportItem);
                }
            }
            return returnViewMatch;
        }

        /// <summary>
        /// 增加焊接日报信息
        /// </summary>
        /// <param name="WeldingDaily"></param>
        public static void AddWeldingDaily(Model.HJGL_WeldingDaily WeldingDaily)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_WeldingDaily newWeldingDaily = new HJGL_WeldingDaily
            {
                WeldingDailyId = WeldingDaily.WeldingDailyId,
                WeldingDailyCode = WeldingDaily.WeldingDailyCode,
                ProjectId = WeldingDaily.ProjectId,
                UnitId = WeldingDaily.UnitId,
                UnitWorkId = WeldingDaily.UnitWorkId,
                WeldingDate = WeldingDaily.WeldingDate,
                Tabler = WeldingDaily.Tabler,
                TableDate = WeldingDaily.TableDate,
                Remark = WeldingDaily.Remark,
            };

            db.HJGL_WeldingDaily.InsertOnSubmit(newWeldingDaily);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改焊接日报信息 
        /// </summary>
        /// <param name="WeldingDaily"></param>
        public static void UpdateWeldingDaily(Model.HJGL_WeldingDaily WeldingDaily)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_WeldingDaily newWeldingDaily = db.HJGL_WeldingDaily.FirstOrDefault(e => e.WeldingDailyId == WeldingDaily.WeldingDailyId);
            if (newWeldingDaily != null)
            {
                newWeldingDaily.WeldingDailyCode = WeldingDaily.WeldingDailyCode;
                newWeldingDaily.UnitId = WeldingDaily.UnitId;
                newWeldingDaily.UnitWorkId = WeldingDaily.UnitWorkId;
                newWeldingDaily.WeldingDate = WeldingDaily.WeldingDate;
                newWeldingDaily.Tabler = WeldingDaily.Tabler;
                newWeldingDaily.TableDate = WeldingDaily.TableDate;
                newWeldingDaily.Remark = WeldingDaily.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据焊接日报Id删除一个焊接日报信息
        /// </summary>
        /// <param name="WeldingDailyId"></param>
        public static void DeleteWeldingDaily(string WeldingDailyId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_WeldingDaily delWeldingDaily = db.HJGL_WeldingDaily.FirstOrDefault(e => e.WeldingDailyId == WeldingDailyId);
            if (delWeldingDaily != null)
            {
                db.HJGL_WeldingDaily.DeleteOnSubmit(delWeldingDaily);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 按类型获取焊接日报项
        /// </summary>
        /// <param name="WeldingDailyType"></param>
        /// <returns></returns>
        public static List<Model.HJGL_WeldingDaily> GetPipeline_WeldingDailyList(string unitId)
        {
            var list = (from x in Funs.DB.HJGL_WeldingDaily
                        where x.UnitId == unitId
                        orderby x.WeldingDailyCode
                        select x).ToList();

            return list;
        }

        #region 焊接日报下拉项
        /// <summary>
        /// 焊接日报下拉项
        /// </summary>
        /// <param name="dropName">下拉框名称</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        /// <param name="WeldingDailyType">耗材类型</param>
        public static void InitWeldingDailyDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease, string unitId)
        {
            dropName.DataValueField = "WeldingDailyId";
            dropName.DataTextField = "WeldingDailyCode";
            dropName.DataSource = GetPipeline_WeldingDailyList(unitId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}
