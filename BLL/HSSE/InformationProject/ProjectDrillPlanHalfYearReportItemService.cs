using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 应急演练工作计划半年报明细
    /// </summary>
    public static class ProjectDrillPlanHalfYearReportItemService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取应急演练工作计划半年报明细信息
        /// </summary>
        /// <param name="drillPlanHalfYearReportItemId"></param>
        /// <returns></returns>
        public static Model.InformationProject_DrillPlanHalfYearReportItem GetDrillPlanHalfYearReportItemById(string drillPlanHalfYearReportItemId)
        {
            return new Model.SGGLDB(Funs.ConnString).InformationProject_DrillPlanHalfYearReportItem.FirstOrDefault(e => e.DrillPlanHalfYearReportItemId == drillPlanHalfYearReportItemId);
        }

        /// <summary>
        /// 根据应急演练工作计划半年报Id获取所有相关明细信息
        /// </summary>
        /// <param name="drillPlanHalfYearReportId"></param>
        /// <returns></returns>
        public static List<Model.InformationProject_DrillPlanHalfYearReportItem> GetDrillPlanHalfYearReportItemList(string drillPlanHalfYearReportId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).InformationProject_DrillPlanHalfYearReportItem where x.DrillPlanHalfYearReportId == drillPlanHalfYearReportId orderby x.SortIndex select x).ToList();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="drillPlanHalfYearReportItem"></param>
        public static void AddDrillPlanHalfYearReportItem(Model.InformationProject_DrillPlanHalfYearReportItem drillPlanHalfYearReportItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InformationProject_DrillPlanHalfYearReportItem newDrillPlanHalfYearReportItem = new Model.InformationProject_DrillPlanHalfYearReportItem
            {
                DrillPlanHalfYearReportItemId = drillPlanHalfYearReportItem.DrillPlanHalfYearReportItemId,
                DrillPlanHalfYearReportId = drillPlanHalfYearReportItem.DrillPlanHalfYearReportId,
                DrillPlanName = drillPlanHalfYearReportItem.DrillPlanName,
                OrganizationUnit = drillPlanHalfYearReportItem.OrganizationUnit,
                DrillPlanDate = drillPlanHalfYearReportItem.DrillPlanDate,
                AccidentScene = drillPlanHalfYearReportItem.AccidentScene,
                ExerciseWay = drillPlanHalfYearReportItem.ExerciseWay,
                SortIndex = drillPlanHalfYearReportItem.SortIndex
            };
            db.InformationProject_DrillPlanHalfYearReportItem.InsertOnSubmit(newDrillPlanHalfYearReportItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="drillPlanHalfYearReportItem"></param>
        public static void UpdateDrillPlanHalfYearReportItem(Model.InformationProject_DrillPlanHalfYearReportItem drillPlanHalfYearReportItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InformationProject_DrillPlanHalfYearReportItem newDrillPlanHalfYearReportItem = db.InformationProject_DrillPlanHalfYearReportItem.FirstOrDefault(e => e.DrillPlanHalfYearReportItemId == drillPlanHalfYearReportItem.DrillPlanHalfYearReportItemId);
            if (newDrillPlanHalfYearReportItem != null)
            {
                newDrillPlanHalfYearReportItem.DrillPlanName = drillPlanHalfYearReportItem.DrillPlanName;
                newDrillPlanHalfYearReportItem.OrganizationUnit = drillPlanHalfYearReportItem.OrganizationUnit;
                newDrillPlanHalfYearReportItem.DrillPlanDate = drillPlanHalfYearReportItem.DrillPlanDate;
                newDrillPlanHalfYearReportItem.AccidentScene = drillPlanHalfYearReportItem.AccidentScene;
                newDrillPlanHalfYearReportItem.ExerciseWay = drillPlanHalfYearReportItem.ExerciseWay;
                newDrillPlanHalfYearReportItem.SortIndex = drillPlanHalfYearReportItem.SortIndex;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="drillPlanHalfYearReportItemId"></param>
        public static void DeleteDrillPlanHalfYearReportItemById(string drillPlanHalfYearReportItemId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InformationProject_DrillPlanHalfYearReportItem drillPlanHalfYearReportItem = db.InformationProject_DrillPlanHalfYearReportItem.FirstOrDefault(e => e.DrillPlanHalfYearReportItemId == drillPlanHalfYearReportItemId);
            if (drillPlanHalfYearReportItem != null)
            {
                db.InformationProject_DrillPlanHalfYearReportItem.DeleteOnSubmit(drillPlanHalfYearReportItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主表主键删除所有相关明细信息
        /// </summary>
        /// <param name="drillPlanHalfYearReportId"></param>
        public static void DeleteDrillPlanHalfYearReportItemList(string drillPlanHalfYearReportId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var q = (from x in db.InformationProject_DrillPlanHalfYearReportItem where x.DrillPlanHalfYearReportId == drillPlanHalfYearReportId select x).ToList();
            if (q != null)
            {
                db.InformationProject_DrillPlanHalfYearReportItem.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}