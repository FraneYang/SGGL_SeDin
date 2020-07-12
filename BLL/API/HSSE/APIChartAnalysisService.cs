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
                if (type == "1")
                {
                    var getUnitlistIds = getHazardRegister.Select(x => x.ResponsibleUnit).Distinct().ToList();
                    foreach (var unitItem in getUnitlistIds)
                    {
                        var getUnitRegister = getHazardRegister.Where(x => x.ResponsibleUnit == unitItem);
                        Model.ChartAnalysisItem newItem = new Model.ChartAnalysisItem
                        {
                            DataId = unitItem,
                            Type = type,
                            DataName = UnitService.GetUnitCodeByUnitId(unitItem),
                            DataSumCount = getUnitRegister.Count(),
                            DataCount1 = getUnitRegister.Where(x => x.States == "2" || x.States == "3").Count(),
                        };

                        getDataLists.Add(newItem);

                    }
                }
                else if (type == "2")
                {
                    var getRegisterTypesIds = getHazardRegister.Select(x => x.RegisterTypesId).Distinct().ToList();
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
