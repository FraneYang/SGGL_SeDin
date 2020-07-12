using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;

namespace BLL
{
    public static class APIHazardListService
    {
        #region 根据主键ID获取危险源辨识与评价详细
        /// <summary>
        ///  根据主键ID获取危险源辨识与评价详细
        /// </summary>
        /// <param name="hazardListId"></param>
        /// <returns></returns>
        public static Model.HazardListItem getHazardListInfoByHazardListId(string hazardListId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getInfo = from x in db.Hazard_HazardList
                              where x.HazardListId == hazardListId
                              select new Model.HazardListItem
                              {
                                  HazardListId = x.HazardListId,
                                  ProjectId = x.ProjectId,
                                  HazardListCode = x.HazardListCode,
                                  VersionNo = x.VersionNo,
                                  CompileManId = x.CompileMan,
                                  CompileManName = db.Sys_User.First(u => u.UserId == x.CompileMan).UserName,
                                  CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                                  WorkStageIds = x.WorkStage,
                                  WorkStageNames = WorkStageService.getWorkStageNamesWorkStageIds(x.WorkStage),
                                  Contents = x.Contents,
                                  WorkAreaName = x.WorkAreaName,
                                  IdentificationDate = string.Format("{0:yyyy-MM-dd}", x.IdentificationDate),
                                  ControllingPersonId = x.ControllingPerson,
                                  ControllingPersonName = db.Sys_User.First(u => u.UserId == x.ControllingPerson).UserName,
                                  States = x.States,
                              };
                return getInfo.FirstOrDefault();
            }
        }
        #endregion        

        #region 根据projectId获取危险源辨识评价列表
        /// <summary>
        /// 根据projectId获取危险源辨识评价列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static List<Model.HazardListItem> getHazardListList(string projectId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getHazardList = from x in db.Hazard_HazardList
                                    where x.ProjectId == projectId && x.States == Const.State_2
                                    orderby x.IdentificationDate descending
                                    select new Model.HazardListItem
                                    {
                                        HazardListId = x.HazardListId,
                                        ProjectId = x.ProjectId,
                                        HazardListCode = x.HazardListCode,
                                        VersionNo = x.VersionNo,
                                        CompileManId = x.CompileMan,
                                        CompileManName = db.Sys_User.First(u => u.UserId == x.CompileMan).UserName,
                                        CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                                        WorkStageIds = x.WorkStage,
                                        WorkStageNames = WorkStageService.getWorkStageNamesWorkStageIds(x.WorkStage),
                                        Contents = x.Contents,
                                        WorkAreaName = x.WorkAreaName,
                                        IdentificationDate = string.Format("{0:yyyy-MM-dd}", x.IdentificationDate),
                                        ControllingPersonId = x.ControllingPerson,
                                        ControllingPersonName = db.Sys_User.First(u => u.UserId == x.ControllingPerson).UserName,
                                        States = x.States,
                                    };
                return getHazardList.ToList();
            }
        }
        #endregion        

        #region 根据HazardListId获取危险源辨识评价明细信息
        /// <summary>
        /// 根据HazardListId获取危险源辨识评价明细信息
        /// </summary>
        /// <param name="hazardListId"></param>
        /// <returns></returns>
        public static List<Model.HazardListSelectedItem> getHazardListSelectedInfo(string hazardListId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getHazardList = from x in db.Hazard_HazardSelectedItem
                                    join y in db.Technique_HazardListType on x.HazardListTypeId equals y.HazardListTypeId
                                    where x.HazardListId == hazardListId
                                    orderby x.HazardListTypeId descending
                                    select new Model.HazardListSelectedItem
                                    {
                                        HazardId = x.HazardId,
                                        WorkStageName = WorkStageService.getWorkStageNamesWorkStageIds(x.WorkStage),
                                        SupHazardListTypeName = db.Technique_HazardListType.First(z => z.HazardListTypeId == y.SupHazardListTypeId).HazardListTypeName,
                                        HazardListTypeName = y.HazardListTypeName,
                                        HazardCode = y.HazardListTypeCode,
                                        HazardItems = x.HazardItems,
                                        DefectsType = x.DefectsType,
                                        MayLeadAccidents = x.MayLeadAccidents,
                                        HelperMethod = x.HelperMethod,
                                        HazardJudge_L = x.HazardJudge_L,
                                        HazardJudge_E = x.HazardJudge_E,
                                        HazardJudge_C = x.HazardJudge_C,
                                        HazardJudge_D = x.HazardJudge_D,
                                        HazardLevel = x.HazardLevel,
                                        ControlMeasures = x.ControlMeasures,
                                    };
                return getHazardList.ToList();
            }
        }
        #endregion        
    }
}
