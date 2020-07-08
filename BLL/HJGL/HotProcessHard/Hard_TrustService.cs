using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 硬度委托
    /// </summary>
    public static class Hard_TrustService
    {
        /// <summary>
        /// 根据主键获取硬度委托
        /// </summary>
        /// <param name="hardTrustID"></param>
        /// <returns></returns>
        public static Model.HJGL_Hard_Trust GetHardTrustById(string hardTrustID)
        {
            return new Model.SGGLDB(Funs.ConnString).HJGL_Hard_Trust.FirstOrDefault(e => e.HardTrustID == hardTrustID);
        }

        /// <summary>
        /// 添加硬度委托
        /// </summary>
        /// <param name="hardTrust"></param>
        public static void AddHardTrust(Model.HJGL_Hard_Trust hardTrust)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.HJGL_Hard_Trust newHardTrust = new Model.HJGL_Hard_Trust();
            newHardTrust.HardTrustID = hardTrust.HardTrustID;
            newHardTrust.HardTrustNo = hardTrust.HardTrustNo;
            newHardTrust.HardTrustUnit = hardTrust.HardTrustUnit;
            newHardTrust.HardTrustDate = hardTrust.HardTrustDate;
            newHardTrust.AuditMan = hardTrust.AuditMan;
            newHardTrust.AuditDate = hardTrust.AuditDate;
            newHardTrust.HardnessRate = hardTrust.HardnessRate;
            newHardTrust.HardnessMethod = hardTrust.HardnessMethod;
            newHardTrust.CheckUnit = hardTrust.CheckUnit;
            newHardTrust.ProjectId = hardTrust.ProjectId;
            newHardTrust.UnitWorkId = hardTrust.UnitWorkId;
            newHardTrust.DetectionTime = hardTrust.DetectionTime;
            newHardTrust.Sendee = hardTrust.Sendee;
            newHardTrust.Standards = hardTrust.Standards;
            newHardTrust.InspectionNum = hardTrust.InspectionNum;
            newHardTrust.CheckNum = hardTrust.CheckNum;
            newHardTrust.TestWeldNum = hardTrust.TestWeldNum;
            newHardTrust.HardTrustMan = hardTrust.HardTrustMan;
            db.HJGL_Hard_Trust.InsertOnSubmit(newHardTrust);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改硬度委托
        /// </summary>
        /// <param name="hardTrust"></param>
        public static void UpdateHardTrust(Model.HJGL_Hard_Trust hardTrust)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.HJGL_Hard_Trust newHardTrust = db.HJGL_Hard_Trust.FirstOrDefault(e => e.HardTrustID == hardTrust.HardTrustID);
            if (newHardTrust != null)
            {
                newHardTrust.HardTrustNo = hardTrust.HardTrustNo;
                newHardTrust.HardTrustUnit = hardTrust.HardTrustUnit;
                newHardTrust.HardTrustDate = hardTrust.HardTrustDate;
                newHardTrust.AuditMan = hardTrust.AuditMan;
                newHardTrust.AuditDate = hardTrust.AuditDate;
                newHardTrust.HardnessRate = hardTrust.HardnessRate;
                newHardTrust.HardnessMethod = hardTrust.HardnessMethod;
                newHardTrust.CheckUnit = hardTrust.CheckUnit;
                newHardTrust.ProjectId = hardTrust.ProjectId;
                newHardTrust.UnitWorkId = hardTrust.UnitWorkId;
                newHardTrust.DetectionTime = hardTrust.DetectionTime;
                newHardTrust.Sendee = hardTrust.Sendee;
                newHardTrust.Standards = hardTrust.Standards;
                newHardTrust.InspectionNum = hardTrust.InspectionNum;
                newHardTrust.CheckNum = hardTrust.CheckNum;
                newHardTrust.TestWeldNum = hardTrust.TestWeldNum;
                newHardTrust.HardTrustMan = hardTrust.HardTrustMan;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除硬度委托
        /// </summary>
        /// <param name="hardTrustID"></param>
        public static void DeleteHardTrustById(string hardTrustID)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.HJGL_Hard_Trust hardTrust = db.HJGL_Hard_Trust.FirstOrDefault(e => e.HardTrustID == hardTrustID);
            if (hardTrust != null)
            {
                db.HJGL_Hard_Trust.DeleteOnSubmit(hardTrust);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 硬度委托委托单编号是否存在
        /// </summary>
        /// <param name="pointNo"></param>
        /// <param name="pointId"></param>
        /// <returns></returns>
        public static bool IsExistTrustCode(string hardTrustNo, string hardTrustID, string projectId)
        {
            var q = new Model.SGGLDB(Funs.ConnString).HJGL_Hard_Trust.FirstOrDefault(x => x.HardTrustNo == hardTrustNo && x.ProjectId == projectId && x.HardTrustID != hardTrustID);
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
        public static List<Model.View_HJGL_Hard_TrustItem> GetHardTrustAddItem(string hdItemsString)
        {
            var jointInfos = from x in new Model.SGGLDB(Funs.ConnString).View_HJGL_WeldJoint select x;
            List<Model.View_HJGL_Hard_TrustItem> returnViewMatch = new List<Model.View_HJGL_Hard_TrustItem>();
            if (!string.IsNullOrEmpty(hdItemsString))
            {
                List<string> jotIds = Funs.GetStrListByStr(hdItemsString, '|');
                foreach (var jotItem in jotIds)
                {
                    string[] strs = jotItem.Split(',');
                    var jotInfo = jointInfos.FirstOrDefault(x => x.WeldJointId == strs[0]);
                    Model.View_HJGL_Hard_TrustItem newItem = new Model.View_HJGL_Hard_TrustItem();
                    newItem.HardTrustItemID = SQLHelper.GetNewID(typeof(Model.View_HJGL_Hard_TrustItem));
                    newItem.WeldJointId = jotInfo.WeldJointId;
                    newItem.HotProessTrustItemId = strs[1];
                    newItem.PipelineCode = jotInfo.PipelineCode;
                    newItem.WeldJointCode = jotInfo.WeldJointCode;
                    newItem.WelderCode = jotInfo.WelderCode;
                    newItem.Specification = jotInfo.Specification;
                    newItem.MaterialCode = jotInfo.MaterialCode;
                    newItem.SingleNumber = jotInfo.SingleNumber;
                    newItem.Remark = jotInfo.Remark;
                    returnViewMatch.Add(newItem);
                }
            }
            return returnViewMatch;
        }

        /// <summary>
        /// 根据项目状态获取硬度委托委托明细信息
        /// </summary>
        /// <param name="ManagerTotalId"></param>
        /// <returns></returns>
        public static List<Model.View_HJGL_Hard_TrustItem> GetHardTrustItem(string hardTrustID)
        {
            List<Model.View_HJGL_Hard_TrustItem> returnViewMatch = (from x in new Model.SGGLDB(Funs.ConnString).View_HJGL_Hard_TrustItem
                                                                    where x.HardTrustID == hardTrustID
                                                               select x).ToList();
            return returnViewMatch;
        }

        /// <summary>
        /// 查找需要硬度委托的焊口信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="hardTrustID"></param>
        /// <param name="iso_id"></param>
        /// <returns></returns>
        public static List<Model.View_HJGL_Hard_TrustItem> GetHardTrustFind(string projectId, string hardTrustID, string pipelineId)
        {
            ///根据已经热处理且需要硬度检测且未进行硬度检测的焊口获取焊口视图集合
            var weldJoints = (from x in new Model.SGGLDB(Funs.ConnString).HJGL_HotProess_TrustItem
                              join y in new Model.SGGLDB(Funs.ConnString).View_HJGL_WeldJoint
                              on x.WeldJointId equals y.WeldJointId
                              where y.PipelineId == pipelineId && x.IsHardness == true && x.IsTrust == null
                              select new {
                                  WeldJointId = y.WeldJointId,
                                  PipelineCode = y.PipelineCode,
                                  WeldJointCode = y.WeldJointCode,
                                  WelderCode = y.WelderCode,
                                  Specification = y.Specification,
                                  MaterialCode = y.MaterialCode,
                                  SingleNumber = y.SingleNumber,
                                  Remark=y.Remark,
                                  HotProessTrustItemId = x.HotProessTrustItemId,
                              }).Distinct().ToList();
            List<Model.View_HJGL_Hard_TrustItem> returnViewMatch = new List<Model.View_HJGL_Hard_TrustItem>();
            foreach (var item in weldJoints)
            {
                Model.View_HJGL_Hard_TrustItem newItem = new Model.View_HJGL_Hard_TrustItem();
                newItem.WeldJointId = item.WeldJointId;
                newItem.PipelineCode = item.PipelineCode;
                newItem.WeldJointCode = item.WeldJointCode;
                newItem.WelderCode = item.WelderCode;
                newItem.Specification = item.Specification;
                newItem.MaterialCode = item.MaterialCode;
                newItem.SingleNumber = item.SingleNumber;
                newItem.Remark = item.Remark;
                newItem.HotProessTrustItemId = item.HotProessTrustItemId;
                returnViewMatch.Add(newItem);
            }
            return returnViewMatch;
        }
    }
}
