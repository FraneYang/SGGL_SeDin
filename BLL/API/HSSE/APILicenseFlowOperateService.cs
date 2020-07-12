using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// 作业票-流程审核
    /// </summary>
    public static class APILicenseFlowOperateService
    {
        #region 根据主键ID获取作业票流程审核详细信息
        /// <summary>
        /// 根据主键ID获取作业票流程审核详细信息
        /// </summary>
        /// <param name="flowOperateId">审核主键ID</param>
        /// <returns></returns>
        public static Model.FlowOperateItem getLicenseFlowOperateById(string flowOperateId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataList = from x in db.License_FlowOperate
                                  where x.FlowOperateId == flowOperateId
                                  orderby x.SortIndex
                                  select new Model.FlowOperateItem
                                  {
                                      FlowOperateId = x.FlowOperateId,
                                      DataId = x.DataId,
                                      AuditFlowName = x.AuditFlowName,
                                      SortIndex = x.SortIndex ?? 1,
                                      RoleIds = x.RoleIds,
                                      RoleNames = RoleService.getRoleNamesRoleIds(x.RoleIds),
                                      OperaterId = x.OperaterId,
                                      OperaterName = db.Sys_User.First(u => u.UserId == x.OperaterId).UserName,
                                      OperaterTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.OperaterTime),
                                      IsAgree = x.IsAgree,
                                      Opinion = x.Opinion,
                                      IsFlowEnd = x.IsFlowEnd ?? false,
                                      IsClosed = x.IsClosed ?? false,
                                  };
                return getDataList.FirstOrDefault();
            }
        }
        #endregion

        #region 获取作业票流程审核列表信息
        /// <summary>
        /// 获取作业票流程审核列表信息
        /// </summary>
        /// <param name="dataId">单据ID</param>
        /// <returns></returns>
        public static List<Model.FlowOperateItem> getLicenseFlowOperateList(string dataId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataList = from x in db.License_FlowOperate
                                  where x.DataId == dataId
                                  orderby x.SortIndex
                                  select new Model.FlowOperateItem
                                  {
                                      FlowOperateId = x.FlowOperateId,
                                      DataId = x.DataId,
                                      AuditFlowName = x.AuditFlowName,
                                      SortIndex = x.SortIndex ?? 1,
                                      RoleIds = x.RoleIds,
                                      RoleNames = RoleService.getRoleNamesRoleIds(x.RoleIds),
                                      OperaterId = x.OperaterId,
                                      OperaterName = db.Sys_User.First(u => u.UserId == x.OperaterId).UserName,
                                      OperaterTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.OperaterTime),
                                      IsAgree = x.IsAgree,
                                      Opinion = x.Opinion,
                                      IsFlowEnd = x.IsFlowEnd ?? false,
                                      IsClosed = x.IsClosed ?? false,
                                  };
                return getDataList.ToList();
            }
        }
        #endregion

        #region 获取作业票流程审核记录明细列表信息
        /// <summary>
        /// 获取作业票流程审核记录明细列表信息
        /// </summary>
        /// <param name="flowOperateId">审核记录ID</param>
        /// <returns></returns>
        public static List<Model.FlowOperateItem> getLicenseFlowOperateItemList(string flowOperateId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataList = from x in db.License_FlowOperateItem
                                  join y in db.License_FlowOperate on x.FlowOperateId equals y.FlowOperateId
                                  where x.FlowOperateId == flowOperateId
                                  orderby y.SortIndex, x.OperaterTime
                                  select new Model.FlowOperateItem
                                  {
                                      FlowOperateId = x.FlowOperateId,
                                      AuditFlowName = y.AuditFlowName,
                                      SortIndex = y.SortIndex ?? 1,
                                      OperaterId = x.OperaterId,
                                      OperaterName = db.Sys_User.First(u => u.UserId == x.OperaterId).UserName,
                                      OperaterTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.OperaterTime),
                                      IsAgree = x.IsAgree,
                                      Opinion = x.Opinion,
                                  };
                return getDataList.ToList();
            }
        }
        #endregion

        #region 初始化作业票提交-审核流程[State=0]
        /// <summary>
        /// 初始化作业票提交-审核流程[State=0]
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="dataId"></param>
        public static void SaveInitFlowOperate(string menuId,string dataId)
        {
            var getSysMenuFlowOperate = MenuFlowOperateService.GetMenuFlowOperateListByMenuId(menuId);
            if (getSysMenuFlowOperate.Count() > 0)
            {
                foreach (var item in getSysMenuFlowOperate)
                {
                    Model.License_FlowOperate newFlowOperate = new Model.License_FlowOperate
                    {
                        FlowOperateId = SQLHelper.GetNewID(),
                        DataId = dataId,
                        AuditFlowName = item.AuditFlowName,
                        SortIndex = item.FlowStep,
                        RoleIds = item.RoleId,
                        IsFlowEnd = item.IsFlowEnd,
                    };

                    LicensePublicService.AddFlowOperate(newFlowOperate);
                }
            }
        }
        #endregion

        #region 保存作业票-流程审核[State=1]
        /// <summary>
        /// 保存作业票-流程审核
        /// </summary>
        /// <param name="flowOperate">流程审核集合</param>
        /// <returns></returns>
        public static void SaveFlowOperate(Model.FlowOperateItem flowOperate)
        {            
            var getFlowOperate = LicensePublicService.GetFlowOperateById(flowOperate.FlowOperateId);
            if (getFlowOperate != null)
            {
                getFlowOperate.OperaterId = flowOperate.OperaterId;
                getFlowOperate.OperaterTime = System.DateTime.Now;
                getFlowOperate.IsAgree = flowOperate.IsAgree;
                getFlowOperate.Opinion = flowOperate.Opinion;
                getFlowOperate.IsClosed = flowOperate.IsAgree;///同意时，当前流程关闭；不同意则返回申请人。
                LicensePublicService.UpdateFlowOperate(getFlowOperate);

                Model.License_FlowOperateItem newFlowOperateItem = new Model.License_FlowOperateItem
                {
                    FlowOperateItemId = SQLHelper.GetNewID(),
                    FlowOperateId = getFlowOperate.FlowOperateId,
                    OperaterId = getFlowOperate.OperaterId,
                    OperaterTime = getFlowOperate.OperaterTime,
                    IsAgree = getFlowOperate.IsAgree,
                    Opinion = getFlowOperate.Opinion,
                };

                LicensePublicService.AddFlowOperateItem(newFlowOperateItem);
            }
        }
        #endregion
    }
}
