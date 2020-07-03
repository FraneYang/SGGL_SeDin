using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class APINDETrustService
    {
        #region 根据单位工程、点口批号、批开始时间获取还未进行委托的批列表
        /// <summary>
        /// 根据单位工程、点口批号、批开始时间获取还未进行委托的批列表
        /// </summary>
        /// <param name="unitWorkId"></param>
        /// <param name="pointBatchCode"></param>
        /// <param name="startDate"></param>
        /// <returns></returns>
        public static List<Model.NDETrustItem> getPointBatchCode(string unitWorkId, string pointBatchCode, DateTime? startDate)
        {
            var dataList = from x in Funs.DB.HJGL_Batch_PointBatchItem
                           join y in Funs.DB.HJGL_Batch_PointBatch on x.PointBatchId equals y.PointBatchId
                           where y.UnitWorkId==unitWorkId && x.IsBuildTrust == null && y.EndDate.HasValue
                           select new Model.NDETrustItem
                           {
                               PointBatchId = x.PointBatchId,
                               PointBatchCode = y.PointBatchCode,
                               StartDate = y.StartDate
                           };

            if (!string.IsNullOrEmpty(pointBatchCode))
            {
                dataList = dataList.Where(e => e.PointBatchCode.Contains(pointBatchCode));
            }
            if (startDate.HasValue)
            {
                dataList = dataList.Where(e => e.StartDate.Value.Date == startDate.Value.Date);
            }

            var getDataLists = (from x in dataList
                                orderby x.PointBatchCode
                                select new Model.NDETrustItem
                                {
                                    PointBatchId = x.PointBatchId,
                                    PointBatchCode = x.PointBatchCode,
                                }).Distinct().ToList();
            return getDataLists;
        }
        #endregion

        #region 根据点口批ID获取已点口还未审批的焊口
        /// <summary>
        /// 根据点口批ID获取已点口还未审批的焊口
        /// </summary>
        /// <param name="pointBatchCode"></param>
        /// <param name="startDate"></param>
        /// <returns></returns>
        public static List<Model.NDETrustItem> getPointWeldJoint(string pointBatchId)
        {
            var getDataLists = (from x in Funs.DB.HJGL_Batch_PointBatchItem
                                join y in Funs.DB.HJGL_WeldJoint on x.WeldJointId equals y.WeldJointId
                                where x.IsBuildTrust == null && x.PointBatchId == pointBatchId && x.PointState == "1"
                                select new Model.NDETrustItem
                                {
                                    WeldJointId = x.WeldJointId,
                                    WeldJointCode = y.WeldJointCode,
                                    PipelineCode = y.PipelineCode,
                                    JointArea = y.JointArea,
                                    AttachUrl = y.AttachUrl
                                }).ToList();

            return getDataLists;
        }
        #endregion

        #region 根据点口批ID和焊口ID获取待调整焊口
        /// <summary>
        /// 根据点口批ID和焊口ID获取待调整焊口
        /// </summary>
        /// <param name="pointBatchCode"></param>
        /// <param name="startDate"></param>
        /// <returns></returns>
        public static List<Model.NDETrustItem> getPointWeldJoint(string pointBatchId, string weldJointId)
        {
            var jot = BLL.WeldJointService.GetWeldJointByWeldJointId(weldJointId);

            var getDataLists = (from x in Funs.DB.HJGL_Batch_PointBatchItem
                                join y in Funs.DB.HJGL_WeldJoint on x.WeldJointId equals y.WeldJointId
                                where x.PointBatchId == pointBatchId && x.PointState == null
                                      && y.JointAttribute == jot.JointAttribute
                                select new Model.NDETrustItem
                                {
                                    WeldJointId = x.WeldJointId,
                                    WeldJointCode = y.WeldJointCode,
                                    PipelineCode = y.PipelineCode,
                                    JointArea = y.JointArea,
                                    AttachUrl = y.AttachUrl
                                }).ToList();

            return getDataLists;
        }
        #endregion

        #region 点口调整 
        /// <summary>
        /// 点口调整
        /// </summary>
        /// <param name="oldJointId">原来点的焊口</param>
        /// <param name="newJointId">调为新的焊口</param>
        public static void RePointSave(string oldJointId,string newJointId)
        {
            var oldPoint = BLL.PointBatchDetailService.GetBatchDetailByJotId(oldJointId);
            if (oldPoint != null)
            {
                oldPoint.PointDate = null;
                oldPoint.PointState = null;
                oldPoint.IsAudit = null;
                oldPoint.IsPipelineFirst = null;
                oldPoint.IsWelderFirst = null;
            }

            var newPoint = BLL.PointBatchDetailService.GetBatchDetailByJotId(newJointId);
            if (newPoint != null)
            {
                newPoint.PointState = "1";
                newPoint.PointDate = DateTime.Now;
            }

            Funs.DB.SubmitChanges();
        }
        #endregion

        #region 根据单位工程获取所有已点的焊口列表
        /// <summary>
        /// 根据单位工程获取所有已点的焊口列表
        /// </summary>
        /// <param name="unitWorkId"></param>
        /// <returns></returns>
        public static List<Model.NDETrustItem> GetPointWeldJointList(string unitWorkId)
        {
            var getDataLists = (from x in Funs.DB.HJGL_Batch_PointBatchItem
                                join y in Funs.DB.HJGL_WeldJoint on x.WeldJointId equals y.WeldJointId
                                join z in Funs.DB.HJGL_Batch_PointBatch on x.PointBatchId equals z.PointBatchId
                                where x.IsBuildTrust == null && z.UnitWorkId == unitWorkId 
                                       && x.PointState == "1"
                                select new Model.NDETrustItem
                                {
                                    WeldJointId = x.WeldJointId,
                                    PointBatchCode=z.PointBatchCode,
                                    WeldJointCode = y.WeldJointCode,
                                    PipelineCode = y.PipelineCode,
                                    AttachUrl = y.AttachUrl
                                }).ToList();

            return getDataLists;
        }
        #endregion

        #region 根据单位工程获取所有已点的焊口生成委托单
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitWorkId"></param>
        public static void GenerateTrust(string unitWorkId)
        {
           //var getViewGenerateTrustLists = (from x in Funs.DB.View_GenerateTrust where x.UnitWorkId == unitWorkId select x).ToList();
        }
        #endregion
    }
}
