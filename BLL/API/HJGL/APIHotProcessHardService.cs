﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class APIHotProcessHardService
    {
        #region 根据单位工程获取热处理委托单号
        /// <summary>
        ///  根据单位工程获取热处理委托单号
        /// </summary>
        /// <param name="unitWrokId"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getHotProessTrustNo(string unitWrokId, string hotProessTrustNo)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var dataLists = from x in db.HJGL_HotProess_Trust
                                where x.UnitWorkId == unitWrokId
                                select x;

                if (!string.IsNullOrEmpty(hotProessTrustNo))
                {
                    dataLists = dataLists.Where(x => x.HotProessTrustNo.Contains(hotProessTrustNo));

                }

                var getDataLists = (from x in dataLists
                                    orderby x.HotProessTrustNo
                                    select new Model.BaseInfoItem
                                    {
                                        BaseInfoId = x.HotProessTrustId,
                                        BaseInfoCode = x.HotProessTrustNo
                                    }).ToList();
                return getDataLists;
            }
        }
        #endregion

        #region 根据单位工程获取硬度委托单号
        /// <summary>
        ///  根据单位工程获取硬度委托单号
        /// </summary>
        /// <param name="unitWrokId"></param>
        /// <param name="hardTrustNo"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getHardTrustNo(string unitWrokId, string hardTrustNo)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var dataLists = from x in db.HJGL_Hard_Trust
                                where x.UnitWorkId == unitWrokId
                                select x;

                if (!string.IsNullOrEmpty(hardTrustNo))
                {
                    dataLists = dataLists.Where(x => x.HardTrustNo.Contains(hardTrustNo));

                }

                var getDataLists = (from x in dataLists
                                    orderby x.HardTrustNo
                                    select new Model.BaseInfoItem
                                    {
                                        BaseInfoId = x.HardTrustID,
                                        BaseInfoCode = x.HardTrustNo
                                    }
                                    ).ToList();
                return getDataLists;
            }
        }
        #endregion

        #region 根据热处理委托单获取委托单明细
        /// <summary>
        ///  根据热处理委托单获取委托单明细
        /// </summary>
        /// <param name="hotProessTrustId"></param>
        /// <returns></returns>
        public static List<Model.HotProcessHardItem> getHotProcessItem(string hotProessTrustId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.View_HJGL_HotProess_TrustItem
                                    where x.HotProessTrustId == hotProessTrustId
                                    orderby x.PipelineCode, x.WeldJointCode
                                    select new Model.HotProcessHardItem
                                    {
                                        PipelineCode = x.PipelineCode,
                                        WeldJointCode = x.WeldJointCode,
                                        Specification = x.Specification,
                                        Material = x.MaterialCode,
                                        TrustDate = x.ProessDate,
                                        IsCompleted = x.IsCompleted,
                                        IsPass = x.IsPass == true ? "是" : "否"
                                    }
                                ).ToList();
                return getDataLists;
            }
        }
        #endregion

        #region 根据硬度委托单获取委托单明细
        /// <summary>
        ///  根据硬度委托单获取委托单明细
        /// </summary>
        /// <param name="hotProessTrustId"></param>
        /// <returns></returns>
        public static List<Model.HotProcessHardItem> getHardTrustItem(string hardTrustId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.View_HJGL_Hard_TrustItem
                                    where x.HardTrustID == hardTrustId
                                    orderby x.PipelineCode, x.WeldJointCode
                                    select new Model.HotProcessHardItem
                                    {
                                        PipelineCode = x.PipelineCode,
                                        WeldJointCode = x.WeldJointCode,
                                        WelderCode = x.WelderCode,
                                        Specification = x.Specification,
                                        Material = x.MaterialCode,
                                        TrustDate = x.HardTrustDate,
                                        IsPass = x.IsPass == true ? "是" : "否"
                                    }
                                ).ToList();
                return getDataLists;
            }
        }
        #endregion

        #region 硬度检测不合格预警
        /// <summary>
        /// 硬度检测不合格预警
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> GetHardNoPassWarning(string projectId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.HJGL_Hard_TrustItem
                                    join y in db.HJGL_Hard_Trust on x.HardTrustID equals y.HardTrustID
                                    join z in db.HJGL_WeldJoint on x.WeldJointId equals z.WeldJointId
                                    where y.ProjectId == projectId && x.IsPass == false
                                    orderby y.HardTrustNo, z.WeldJointCode
                                    select new Model.BaseInfoItem
                                    {
                                        BaseInfoId = x.WeldJointId,
                                        BaseInfoCode = "硬度委托单号：" + y.HardTrustNo,
                                        BaseInfoName = "不合格焊口：" + z.WeldJointCode
                                    }
                               ).ToList();
                return getDataLists;
            }
        }
        #endregion
    }
}
