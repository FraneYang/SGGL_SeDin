using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public static class APIChartAnalysisService
    {
        #region 根据类型获取图型数据
        /// <summary>
        /// 根据类型获取图型数据
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<Model.ChartAnalysisItem> getChartAnalysisByType(string projectId, string type, string startDate, string endDate)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                List<Model.ChartAnalysisItem> getDataLists = new List<Model.ChartAnalysisItem>();
                var getHazardRegister = from x in db.HSSE_Hazard_HazardRegister
                                        where x.ProjectId == projectId
                                        select x;
                if (!string.IsNullOrEmpty(startDate))
                {
                    var sDate = Funs.GetNewDateTime(startDate);
                    getHazardRegister = getHazardRegister.Where(x => x.RegisterDate >= sDate);
                }
                if (!string.IsNullOrEmpty(endDate))
                {
                    var eDate = Funs.GetNewDateTime(endDate);
                    getHazardRegister = getHazardRegister.Where(x => x.RegisterDate <= eDate);
                }
                if (type == "6")
                {
                    var getUnitlistIds = getHazardRegister.Select(x => x.ResponsibleUnit).Distinct().ToList();
                    foreach (var unitItem in getUnitlistIds)
                    {
                        var getUnitRegister = getHazardRegister.Where(x => x.ResponsibleUnit == unitItem);
                        Model.ChartAnalysisItem newItem = new Model.ChartAnalysisItem
                        {
                            DataId = unitItem,
                            Type = type,
                            DataName = UnitService.GetShortUnitNameByUnitId(unitItem),
                            DataSumCount = getUnitRegister.Count(),
                            DataCount1 = getUnitRegister.Where(x => x.States == "2" || x.States == "3").Count(),
                        };

                        getDataLists.Add(newItem);

                    }
                }
                else if (type == "5")
                {
                    var getWorkAreaIds = getHazardRegister.Select(x => x.Place).Distinct().ToList();
                    foreach (var typeItem in getWorkAreaIds)
                    {
                        var getUnitRegister = getHazardRegister.Where(x => x.Place == typeItem);
                        Model.ChartAnalysisItem newItem = new Model.ChartAnalysisItem
                        {
                            DataId = typeItem,
                            Type = type,
                            DataName = db.WBS_UnitWork.First(y => y.UnitWorkId == typeItem).UnitWorkName,
                            DataSumCount = getUnitRegister.Count(),
                            DataCount1 = getUnitRegister.Where(x => x.States == "2" || x.States == "3").Count(),
                        };

                        getDataLists.Add(newItem);

                    }
                }
                else
                {
                    List<string> getRegisterTypesIds = new List<string>();
                    if (type == "1")
                    {
                        getRegisterTypesIds = getHazardRegister.Select(x => x.RegisterTypesId).Distinct().ToList();
                    }
                    else if (type == "2")
                    {
                        getRegisterTypesIds = getHazardRegister.Select(x => x.RegisterTypes2Id).Distinct().ToList();
                    }
                    else if (type == "3")
                    {
                        getRegisterTypesIds = getHazardRegister.Select(x => x.RegisterTypes3Id).Distinct().ToList();
                    }
                    else if (type == "4")
                    {
                        getRegisterTypesIds = getHazardRegister.Select(x => x.RegisterTypes4Id).Distinct().ToList();
                    }

                    foreach (var typeItem in getRegisterTypesIds)
                    {
                        var getUnitRegister = getHazardRegister.Where(x => x.RegisterTypesId == typeItem);
                        Model.ChartAnalysisItem newItem = new Model.ChartAnalysisItem
                        {
                            DataId = typeItem,
                            Type = type,
                            DataName = db.HSSE_Hazard_HazardRegisterTypes.First(y => y.RegisterTypesId == typeItem).RegisterTypesName,
                            DataSumCount = getUnitRegister.Count(),
                            DataCount1 = getUnitRegister.Where(x => x.States == "2" || x.States == "3").Count(),
                        };

                        getDataLists.Add(newItem);

                    }
                }
                return getDataLists;
            }
        }
        #endregion
    }
}
