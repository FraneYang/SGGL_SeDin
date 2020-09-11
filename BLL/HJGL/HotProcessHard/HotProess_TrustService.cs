using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 热处理设备
    /// </summary>
    public static class HotProess_TrustService
    {
        /// <summary>
        /// 根据主键获取热处理
        /// </summary>
        /// <param name="hotProessTrustId"></param>
        /// <returns></returns>
        public static Model.HJGL_HotProess_Trust GetHotProessTrustById(string hotProessTrustId)
        {
            return Funs.DB.HJGL_HotProess_Trust.FirstOrDefault(e => e.HotProessTrustId == hotProessTrustId);
        }

        /// <summary>
        /// 添加热处理
        /// </summary>
        /// <param name="hotProessTrust"></param>
        public static void AddHotProessTrust(Model.HJGL_HotProess_Trust hotProessTrust)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_HotProess_Trust newHotProessTrust = new Model.HJGL_HotProess_Trust();
            newHotProessTrust.HotProessTrustId = hotProessTrust.HotProessTrustId;
            newHotProessTrust.HotProessTrustNo = hotProessTrust.HotProessTrustNo;
            newHotProessTrust.ProessDate = hotProessTrust.ProessDate;
            newHotProessTrust.ProjectId = hotProessTrust.ProjectId;
            newHotProessTrust.UnitWorkId = hotProessTrust.UnitWorkId;
            newHotProessTrust.UnitId = hotProessTrust.UnitId;
            newHotProessTrust.Tabler = hotProessTrust.Tabler;
            newHotProessTrust.Remark = hotProessTrust.Remark;
            newHotProessTrust.ProessMethod = hotProessTrust.ProessMethod;
            newHotProessTrust.ProessEquipment = hotProessTrust.ProessEquipment;
            db.HJGL_HotProess_Trust.InsertOnSubmit(newHotProessTrust);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改热处理
        /// </summary>
        /// <param name="hotProessTrust"></param>
        public static void UpdateHotProessTrust(Model.HJGL_HotProess_Trust hotProessTrust)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_HotProess_Trust newHotProessTrust = db.HJGL_HotProess_Trust.FirstOrDefault(e => e.HotProessTrustId == hotProessTrust.HotProessTrustId);
            if (newHotProessTrust != null)
            {
                newHotProessTrust.HotProessTrustNo = hotProessTrust.HotProessTrustNo;
                newHotProessTrust.ProessDate = hotProessTrust.ProessDate;
                newHotProessTrust.UnitWorkId = hotProessTrust.UnitWorkId;
                newHotProessTrust.ProjectId = hotProessTrust.ProjectId;
                newHotProessTrust.UnitId = hotProessTrust.UnitId;
                newHotProessTrust.Tabler = hotProessTrust.Tabler;
                newHotProessTrust.Remark = hotProessTrust.Remark;
                newHotProessTrust.ProessMethod = hotProessTrust.ProessMethod;
                newHotProessTrust.ProessEquipment = hotProessTrust.ProessEquipment;
                newHotProessTrust.ReportNo = hotProessTrust.ReportNo;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除热处理
        /// </summary>
        /// <param name="hotProessTrustId"></param>
        public static void DeleteHotProessTrustById(string hotProessTrustId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_HotProess_Trust hotProessTrust = db.HJGL_HotProess_Trust.FirstOrDefault(e => e.HotProessTrustId == hotProessTrustId);
            if (hotProessTrust != null)
            {
                db.HJGL_HotProess_Trust.DeleteOnSubmit(hotProessTrust);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 热处理委托单编号是否存在
        /// </summary>
        /// <param name="pointNo"></param>
        /// <param name="pointId"></param>
        /// <returns></returns>
        public static bool IsExistTrustCode(string hotProessTrustNo, string hotProessTrustId, string projectId)
        {
            var q = Funs.DB.HJGL_HotProess_Trust.FirstOrDefault(x => x.HotProessTrustNo == hotProessTrustNo && x.ProjectId == projectId && x.HotProessTrustId != hotProessTrustId);
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
        /// 查找后返回集合增加到列表集团中
        /// </summary>
        /// <param name="hdItemsString"></param>
        /// <returns></returns>
        public static List<Model.View_HJGL_HotProess_TrustItem> GetHotProessTrustAddItem(string hdItemsString)
        {
            var jointInfos = from x in Funs.DB.View_HJGL_WeldJoint select x;
            List<Model.View_HJGL_HotProess_TrustItem> returnViewMatch = new List<Model.View_HJGL_HotProess_TrustItem>();
            if (!string.IsNullOrEmpty(hdItemsString))
            {
                List<string> jotIds = Funs.GetStrListByStr(hdItemsString, '|');
                foreach (var jotItem in jotIds)
                {
                    string[] strs = jotItem.Split(',');
                    var jotInfo = jointInfos.FirstOrDefault(x => x.WeldJointId == strs[0]);
                    Model.View_HJGL_HotProess_TrustItem newItem = new Model.View_HJGL_HotProess_TrustItem();
                    newItem.HotProessTrustItemId = SQLHelper.GetNewID(typeof(Model.View_HJGL_HotProess_TrustItem));
                    newItem.WeldJointId = jotInfo.WeldJointId;
                    newItem.WeldJointCode = jotInfo.WeldJointCode;
                    newItem.PipelineCode = jotInfo.PipelineCode;
                    newItem.Specification = jotInfo.Specification;
                    newItem.MaterialCode = jotInfo.Material1Code;
                    returnViewMatch.Add(newItem);
                }
            }
            return returnViewMatch;
        }

        /// <summary>
        /// 根据项目状态获取热处理委托明细信息
        /// </summary>
        /// <param name="ManagerTotalId"></param>
        /// <returns></returns>
        public static List<Model.View_HJGL_HotProess_TrustItem> GetHotProessTrustItem(string projectId, string hotProessTrustId)
        {
            List<Model.View_HJGL_HotProess_TrustItem> returnViewMatch = (from x in Funs.DB.View_HJGL_HotProess_TrustItem
                                                                         where x.ProjectId == projectId && x.HotProessTrustId == hotProessTrustId
                                                                    select x).ToList();
            return returnViewMatch;
        }

        /// <summary>
        /// 查找需要热处理的焊口信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="hotProessTrustId"></param>
        /// <param name="iso_id"></param>
        /// <returns></returns>
        public static List<Model.View_HJGL_HotProessTrustItemSearch> GetHotProessTrustFind(string projectId, string hotProessTrustId, string pipelineId)
        {
            var weldJoints = (from x in Funs.DB.View_HJGL_WeldJoint select x).ToList();
            List<Model.View_HJGL_HotProessTrustItemSearch> returnViewMatch = new List<Model.View_HJGL_HotProessTrustItemSearch>();

            var hotProessTrustItems = from x in Funs.DB.HJGL_HotProess_TrustItem
                                      join z in Funs.DB.HJGL_WeldJoint
                                      on x.WeldJointId equals z.WeldJointId
                                      where z.ProjectId == projectId && z.PipelineId == pipelineId
                                      select x;

            if (weldJoints.Count() > 0)
            {
                foreach (var item in weldJoints)
                {
                    var jothotProessTrustItems = from x in hotProessTrustItems where x.WeldJointId == item.WeldJointId select x;
                    bool isShow = false;
                    if (item.IsHotProess == true)//需要热处理
                    {
                        if (jothotProessTrustItems.Count() == 0) //未进行过热处理
                        {
                            isShow = true;
                        }
                        if (isShow)
                        {
                            Model.View_HJGL_HotProessTrustItemSearch newItem = new Model.View_HJGL_HotProessTrustItemSearch();
                            newItem.WeldJointId = item.WeldJointId;
                            newItem.PipelineId = item.PipelineId;
                            newItem.PipelineCode = item.PipelineCode;
                            newItem.WeldJointCode = item.WeldJointCode;
                            newItem.Specification = item.Specification;
                            newItem.MaterialCode = item.Material1Code;
                            newItem.WeldingDailyId = item.WeldingDailyId;
                            var weldingDaily = BLL.WeldingDailyService.GetPipeline_WeldingDailyByWeldingDailyId(item.WeldingDailyId);
                            if (weldingDaily != null)
                            {
                                newItem.WeldingDate = weldingDaily.WeldingDate;
                            }
                            returnViewMatch.Add(newItem);
                        }
                    }
                }
            }
            return returnViewMatch;
        }
    }
}
