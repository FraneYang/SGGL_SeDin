using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;

namespace BLL
{
    public static class APIUnitService
    {
        /// <summary>
        /// 根据UnitId获取单位信息
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static Model.UnitItem getUnitByUnitId(string unitId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getUnit = db.Base_Unit.FirstOrDefault(x => x.UnitId == unitId);
                return ObjectMapperManager.DefaultInstance.GetMapper<Model.Base_Unit, Model.UnitItem>().Map(getUnit);
            }
        }

        /// <summary>
        /// 获取所有单位信息
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getUnitLists()
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var units = (from x in db.Base_Unit
                             orderby x.UnitName
                             select new Model.BaseInfoItem { BaseInfoId = x.UnitId, BaseInfoCode = x.UnitCode, BaseInfoName = x.UnitName }).ToList();
                return units;
            }
        }

        /// <summary>
        /// 根据projectId、unitType获取单位信息（总包1;施工分包2;监理3;业主4;其他5）
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static List<Model.UnitItem> getUnitByProjectIdUnitType(string projectId, string unitType)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var units = (from x in db.Base_Unit
                             join y in db.Project_ProjectUnit
                             on x.UnitId equals y.UnitId
                             where y.ProjectId == projectId && (y.UnitType == unitType || unitType == null)
                             orderby x.UnitName
                             select x).ToList();
                return ObjectMapperManager.DefaultInstance.GetMapper<List<Model.Base_Unit>, List<Model.UnitItem>>().Map(units.ToList());
            }
        }

        /// <summary>
        /// 根据projectId、unitType获取单位信息（总包1;施工分包2;监理3;业主4;其他5）
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static List<Model.UnitItem> getUnitByProjectIdUnitTypeQuery(string projectId, string strParam, string unitType)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var units = (from x in db.Base_Unit
                             join y in db.Project_ProjectUnit
                             on x.UnitId equals y.UnitId
                             where y.ProjectId == projectId && (strParam == null || x.UnitName.Contains(strParam))
                             && (y.UnitType == unitType || unitType == null)
                             orderby x.UnitName
                             select x).ToList();
                return ObjectMapperManager.DefaultInstance.GetMapper<List<Model.Base_Unit>, List<Model.UnitItem>>().Map(units.ToList());
            }
        }

        /// <summary>
        /// 根据subUnitQualityId获取分包商资质信息
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static List<Model.SubUnitQualityItem> getSubUnitQualityBySubUnitQualityId(string subUnitQualityId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getData = from x in db.QualityAudit_SubUnitQuality
                              join y in db.Base_Unit on x.UnitId equals y.UnitId
                              where x.SubUnitQualityId == subUnitQualityId
                              select new Model.SubUnitQualityItem
                              {
                                  UnitId = y.UnitId,
                                  UnitName = y.UnitName,
                                  SubUnitQualityId = x.SubUnitQualityId,
                                  SubUnitQualityName = x.SubUnitQualityName,
                                  BusinessLicense = x.BusinessLicense,
                                  BL_EnableDate = string.Format("{0:yyyy-MM-dd}", x.BL_EnableDate),
                                  BL_ScanUrl = x.BL_ScanUrl.Replace("\\", "/"),
                                  Certificate = x.Certificate,
                                  C_EnableDate = string.Format("{0:yyyy-MM-dd}", x.C_EnableDate),
                                  C_ScanUrl = x.C_ScanUrl.Replace("\\", "/"),
                                  QualityLicense = x.QualityLicense,
                                  QL_EnableDate = string.Format("{0:yyyy-MM-dd}", x.QL_EnableDate),
                                  QL_ScanUrl = x.QL_ScanUrl.Replace("\\", "/"),
                                  HSELicense = x.HSELicense,
                                  H_EnableDate = string.Format("{0:yyyy-MM-dd}", x.H_EnableDate),
                                  H_ScanUrl = x.H_ScanUrl.Replace("\\", "/"),
                                  HSELicense2 = x.HSELicense2,
                                  H_EnableDate2 = string.Format("{0:yyyy-MM-dd}", x.H_EnableDate2),
                                  H_ScanUrl2 = x.H_ScanUrl2.Replace("\\", "/"),
                                  SecurityLicense = x.SecurityLicense,
                                  SL_EnableDate = string.Format("{0:yyyy-MM-dd}", x.SL_EnableDate),
                                  SL_ScanUrl = x.SL_ScanUrl.Replace("\\", "/")
                              };
                return getData.ToList();
                return null;
            }
        }
    }
}
