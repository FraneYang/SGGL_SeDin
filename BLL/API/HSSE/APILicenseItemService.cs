using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// 作业票-安全措施
    /// </summary>
    public static class APILicenseItemService
    {
        #region 根据主键ID获取作业票安全措施详细
        /// <summary>
        ///  根据主键ID获取安全措施详细
        /// </summary>
        /// <param name="licenseItemId">主键</param>
        /// <returns></returns>
        public static Model.LicenseItem getLicenseItemById(string licenseItemId)
        {
            var getInfo = from x in new Model.SGGLDB(Funs.ConnString).License_LicenseItem
                          where x.LicenseItemId == licenseItemId
                          select new Model.LicenseItem
                          {
                              LicenseItemId = x.LicenseItemId,
                              DataId = x.DataId,
                              SortIndex = x.SortIndex ?? 1,
                              SafetyMeasures = x.SafetyMeasures,
                              IsUsed = x.IsUsed ?? true,
                              ConfirmManId = x.ConfirmManId,
                              ConfirmManName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(u => u.UserId == x.ConfirmManId).UserName,
                          };
            return getInfo.FirstOrDefault();
        }
        #endregion        

        #region 获取作业票安全措施列表信息
        /// <summary>
        /// 获取作业票安全措施列表信息
        /// </summary>
        /// <param name="dataId">单据ID</param>
        /// <returns></returns>
        public static List<Model.LicenseItem> getLicenseItemList(string dataId)
        {
            var getDataList = from x in new Model.SGGLDB(Funs.ConnString).License_LicenseItem
                              where x.DataId == dataId
                              orderby x.SortIndex
                              select new Model.LicenseItem
                              {
                                  LicenseItemId = x.LicenseItemId,
                                  DataId = x.DataId,
                                  SortIndex = x.SortIndex ?? 1,
                                  SafetyMeasures = x.SafetyMeasures,
                                  IsUsed = x.IsUsed ?? true,
                                  ConfirmManId = x.ConfirmManId,
                                  ConfirmManName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(u => u.UserId == x.ConfirmManId).UserName,
                              };
            return getDataList.ToList();
        }
        #endregion

        #region 保存作业票-安全措施
        /// <summary>
        /// 保存作业票-安全措施
        /// </summary>
        /// <param name="licenseItem">安全措施集合</param>
        /// <returns></returns>
        public static void SaveLicenseItemList(List< Model.LicenseItem> licenseItemList)
        {
            foreach (var item in licenseItemList)
            {
                Model.License_LicenseItem newLicenseItem = new Model.License_LicenseItem
                {                   
                    DataId = item.DataId,
                    SortIndex = item.SortIndex,
                    SafetyMeasures = item.SafetyMeasures,
                    IsUsed = item.IsUsed,
                    ConfirmManId = item.ConfirmManId,
                };
                LicensePublicService.AddLicenseItem(newLicenseItem);
            }
        }
        #endregion
    }
}
