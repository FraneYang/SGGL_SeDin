using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 应急演练开展情况季报明细
    /// </summary>
    public static class ProjectDrillConductedQuarterlyReportItemService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取应急演练开展情况季报表明细信息
        /// </summary>
        /// <param name="drillConductedQuarterlyReportItemId"></param>
        /// <returns></returns>
        public static Model.InformationProject_DrillConductedQuarterlyReportItem GetDrillConductedQuarterlyReportItemById(string drillConductedQuarterlyReportItemId)
        {
            return new Model.SGGLDB(Funs.ConnString).InformationProject_DrillConductedQuarterlyReportItem.FirstOrDefault(e => e.DrillConductedQuarterlyReportItemId == drillConductedQuarterlyReportItemId);
        }

        /// <summary>
        /// 根据应急演练开展情况季报Id获取所有相关明细信息
        /// </summary>
        /// <param name="drillConductedQuarterlyReportId"></param>
        /// <returns></returns>
        public static List<Model.InformationProject_DrillConductedQuarterlyReportItem> GetDrillConductedQuarterlyReportItemList(string drillConductedQuarterlyReportId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).InformationProject_DrillConductedQuarterlyReportItem where x.DrillConductedQuarterlyReportId == drillConductedQuarterlyReportId orderby x.SortIndex select x).ToList();
        }

        /// <summary>
        /// 添加应急演练开展情况季报明细信息
        /// </summary>
        /// <param name="drillConductedQuarterlyReportItem"></param>
        public static void AddDrillConductedQuarterlyReportItem(Model.InformationProject_DrillConductedQuarterlyReportItem drillConductedQuarterlyReportItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InformationProject_DrillConductedQuarterlyReportItem newDrillConductedQuarterlyReportItem = new Model.InformationProject_DrillConductedQuarterlyReportItem
            {
                DrillConductedQuarterlyReportItemId = drillConductedQuarterlyReportItem.DrillConductedQuarterlyReportItemId,
                DrillConductedQuarterlyReportId = drillConductedQuarterlyReportItem.DrillConductedQuarterlyReportId,
                IndustryType = drillConductedQuarterlyReportItem.IndustryType,
                TotalConductCount = drillConductedQuarterlyReportItem.TotalConductCount,
                TotalPeopleCount = drillConductedQuarterlyReportItem.TotalPeopleCount,
                TotalInvestment = drillConductedQuarterlyReportItem.TotalInvestment,
                HQConductCount = drillConductedQuarterlyReportItem.HQConductCount,
                HQPeopleCount = drillConductedQuarterlyReportItem.HQPeopleCount,
                HQInvestment = drillConductedQuarterlyReportItem.HQInvestment,
                BasicConductCount = drillConductedQuarterlyReportItem.BasicConductCount,
                BasicPeopleCount = drillConductedQuarterlyReportItem.BasicPeopleCount,
                BasicInvestment = drillConductedQuarterlyReportItem.BasicInvestment,
                ComprehensivePractice = drillConductedQuarterlyReportItem.ComprehensivePractice,
                CPScene = drillConductedQuarterlyReportItem.CPScene,
                CPDesktop = drillConductedQuarterlyReportItem.CPDesktop,
                SpecialDrill = drillConductedQuarterlyReportItem.SpecialDrill,
                SDScene = drillConductedQuarterlyReportItem.SDScene,
                SDDesktop = drillConductedQuarterlyReportItem.SDDesktop,
                SortIndex = drillConductedQuarterlyReportItem.SortIndex
            };
            db.InformationProject_DrillConductedQuarterlyReportItem.InsertOnSubmit(newDrillConductedQuarterlyReportItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改应急演练开展情况季报明细信息
        /// </summary>
        /// <param name="drillConductedQuarterlyReportItem"></param>
        public static void UpdateDrillConductedQuarterlyReportItem(Model.InformationProject_DrillConductedQuarterlyReportItem drillConductedQuarterlyReportItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InformationProject_DrillConductedQuarterlyReportItem newDrillConductedQuarterlyReportItem = db.InformationProject_DrillConductedQuarterlyReportItem.FirstOrDefault(e => e.DrillConductedQuarterlyReportItemId == drillConductedQuarterlyReportItem.DrillConductedQuarterlyReportItemId);
            if (newDrillConductedQuarterlyReportItem != null)
            {
                newDrillConductedQuarterlyReportItem.IndustryType = drillConductedQuarterlyReportItem.IndustryType;
                newDrillConductedQuarterlyReportItem.TotalConductCount = drillConductedQuarterlyReportItem.TotalConductCount;
                newDrillConductedQuarterlyReportItem.TotalPeopleCount = drillConductedQuarterlyReportItem.TotalPeopleCount;
                newDrillConductedQuarterlyReportItem.TotalInvestment = drillConductedQuarterlyReportItem.TotalInvestment;
                newDrillConductedQuarterlyReportItem.HQConductCount = drillConductedQuarterlyReportItem.HQConductCount;
                newDrillConductedQuarterlyReportItem.HQPeopleCount = drillConductedQuarterlyReportItem.HQPeopleCount;
                newDrillConductedQuarterlyReportItem.HQInvestment = drillConductedQuarterlyReportItem.HQInvestment;
                newDrillConductedQuarterlyReportItem.BasicConductCount = drillConductedQuarterlyReportItem.BasicConductCount;
                newDrillConductedQuarterlyReportItem.BasicPeopleCount = drillConductedQuarterlyReportItem.BasicPeopleCount;
                newDrillConductedQuarterlyReportItem.BasicInvestment = drillConductedQuarterlyReportItem.BasicInvestment;
                newDrillConductedQuarterlyReportItem.ComprehensivePractice = drillConductedQuarterlyReportItem.ComprehensivePractice;
                newDrillConductedQuarterlyReportItem.CPScene = drillConductedQuarterlyReportItem.CPScene;
                newDrillConductedQuarterlyReportItem.CPDesktop = drillConductedQuarterlyReportItem.CPDesktop;
                newDrillConductedQuarterlyReportItem.SpecialDrill = drillConductedQuarterlyReportItem.SpecialDrill;
                newDrillConductedQuarterlyReportItem.SDScene = drillConductedQuarterlyReportItem.SDScene;
                newDrillConductedQuarterlyReportItem.SDDesktop = drillConductedQuarterlyReportItem.SDDesktop;
                newDrillConductedQuarterlyReportItem.SortIndex = drillConductedQuarterlyReportItem.SortIndex;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除应急演练开展情况季报明细信息
        /// </summary>
        /// <param name="drillConductedQuarterlyReportItemId"></param>
        public static void DeleteDrillConductedQuarterlyReportItemById(string drillConductedQuarterlyReportItemId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InformationProject_DrillConductedQuarterlyReportItem drillConductedQuarterlyReportItem = db.InformationProject_DrillConductedQuarterlyReportItem.FirstOrDefault(e => e.DrillConductedQuarterlyReportItemId == drillConductedQuarterlyReportItemId);
            if (drillConductedQuarterlyReportItem != null)
            {
                db.InformationProject_DrillConductedQuarterlyReportItem.DeleteOnSubmit(drillConductedQuarterlyReportItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据应急演练开展情况季报主表id删除所有相关明细信息
        /// </summary>
        /// <param name="drillConductedQuarterlyReportId"></param>
        public static void DeleteDrillConductedQuarterlyReportItemList(string drillConductedQuarterlyReportId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var q = (from x in db.InformationProject_DrillConductedQuarterlyReportItem where x.DrillConductedQuarterlyReportId == drillConductedQuarterlyReportId select x).ToList();
            if (q != null)
            {
                db.InformationProject_DrillConductedQuarterlyReportItem.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}