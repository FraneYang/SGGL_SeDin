using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;

namespace BLL
{
    /// <summary>
    /// 焊接日报
    /// </summary>
    public static class APIPreWeldingDailyService
    {
        #region 根据获取详细信息
        /// <summary>
        ///  根据获取详细信息
        /// </summary>
        /// <param name="preWeldingDailyId"></param>
        /// <returns></returns>
        public static Model.HJGL_PreWeldingDailyItem getPreWeldingDailyInfo(string preWeldingDailyId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getInfo = from x in db.HJGL_PreWeldingDaily
                              where x.PreWeldingDailyId == preWeldingDailyId
                              select new Model.HJGL_PreWeldingDailyItem
                              {
                                  PreWeldingDailyId = x.PreWeldingDailyId,
                                  ProjectId = x.ProjectId,
                                  UnitWorkId = x.UnitWorkId,
                                  UnitWorkName = db.WBS_UnitWork.First(y => y.UnitWorkId == x.UnitWorkId).UnitWorkName,
                                  UnitId = x.UnitId,
                                  UnitName = db.Base_Unit.First(u => u.UnitId == x.UnitId).UnitName,
                              };
                return getInfo.FirstOrDefault();
            }
        }
        #endregion        

        #region 获取焊接日报列表信息
        /// <summary>
        /// 获取施工方案列表信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.HJGL_PreWeldingDailyItem> getPreWeldingDailyList(string projectId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getList = from x in db.HJGL_PreWeldingDaily
                              where x.ProjectId == projectId
                              orderby x.WeldingDate descending
                              select new Model.HJGL_PreWeldingDailyItem
                              {
                                  PreWeldingDailyId = x.PreWeldingDailyId,
                                  ProjectId = x.ProjectId,
                                  UnitWorkId = x.UnitWorkId,
                                  UnitWorkName = db.WBS_UnitWork.First(y => y.UnitWorkId == x.UnitWorkId).UnitWorkName,
                                  UnitId = x.UnitId,
                                  UnitName = db.Base_Unit.First(u => u.UnitId == x.UnitId).UnitName,
                              };
                return getList.ToList();
            }
        }
        #endregion        

        #region 保存Solution_ConstructSolution
        /// <summary>
        /// 保存Solution_ConstructSolution
        /// </summary>
        /// <param name="newItem">施工方案</param>
        /// <returns></returns>
        public static void SavePreWeldingDaily(Model.HJGL_PreWeldingDailyItem newItem)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_PreWeldingDaily newP= new Model.HJGL_PreWeldingDaily
            {
                PreWeldingDailyId = newItem.PreWeldingDailyId,
                ProjectId = newItem.ProjectId,
                UnitWorkId = newItem.UnitWorkId,
                UnitId = newItem.UnitId,
            };

            var updateItem = db.HJGL_PreWeldingDaily.FirstOrDefault(x => x.PreWeldingDailyId == newItem.PreWeldingDailyId);
            if (updateItem == null)
            {

                newP.PreWeldingDailyId = SQLHelper.GetNewID();
                db.HJGL_PreWeldingDaily.InsertOnSubmit(newP);
                db.SubmitChanges();
            }
            else
            {
                ///   更新
                ///   //
            }

            //if (newItem.BaseInfoItem != null && newItem.BaseInfoItem.Count() > 0)
            //{
            //    foreach (var item in newItem.BaseInfoItem)
            //    {
            //       // var a =item.BaseInfoId,

            //    }
            //}
        }
        #endregion
    }
}
