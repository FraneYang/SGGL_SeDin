using System;
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
        public static List<Model.BaseInfoItem> getHotProessTrustNo(string unitWrokId,string hotProessTrustNo)
        {
            var dataLists = from x in Funs.DB.HJGL_HotProess_Trust
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
        #endregion

        #region 根据单位工程获取硬度委托单号
        /// <summary>
        ///  根据单位工程获取硬度委托单号
        /// </summary>
        /// <param name="unitWrokId"></param>
        /// <param name="hardTrustNo"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getHardTrustNo(string unitWrokId,string hardTrustNo)
        {
            var dataLists = from x in Funs.DB.HJGL_Hard_Trust
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
        #endregion

        #region 根据热处理委托单获取委托单明细
        /// <summary>
        ///  根据热处理委托单获取委托单明细
        /// </summary>
        /// <param name="hotProessTrustId"></param>
        /// <returns></returns>
        public static List<Model.HotProcessHardItem> getHotProcessItem(string hotProessTrustId)
        {
            var getDataLists = (from x in Funs.DB.View_HJGL_HotProess_TrustItem
                                where x.HotProessTrustId == hotProessTrustId
                                orderby x.PipelineCode, x.WeldJointCode
                                select new Model.HotProcessHardItem
                                {
                                    PipelineCode = x.PipelineCode,
                                    WeldJointCode = x.WeldJointCode,
                                    Specification = x.Specification,
                                    Material = x.MaterialCode,
                                    TrustDate = x.ProessDate,
                                    IsCompleted = x.IsCompleted == true ? "是" : "否",
                                    IsPass=x.IsPass==true? "是" : "否"
                                }
                                ).ToList();
            return getDataLists;
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
            var getDataLists = (from x in Funs.DB.View_HJGL_Hard_TrustItem
                                where x.HardTrustID == hardTrustId
                                orderby x.PipelineCode, x.WeldJointCode
                                select new Model.HotProcessHardItem
                                {
                                    PipelineCode = x.PipelineCode,
                                    WeldJointCode = x.WeldJointCode,
                                    Specification = x.Specification,
                                    Material = x.MaterialCode,
                                    IsPass = x.IsPass == true ? "是" : "否"
                                }
                                ).ToList();
            return getDataLists;
        }
        #endregion 
    }
}
