using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 职工伤亡事故原因分析报明细表
    /// </summary>
  public static  class ProjectAccidentCauseReportItemService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 职工伤亡事故原因分析报表明细表
        /// </summary>
        /// <param name="AccidentCauseReportItemId">职工伤亡事故原因分析报表明细表Id</param>
        /// <returns>职工伤亡事故原因分析报表明细表</returns>
        public static Model.InformationProject_AccidentCauseReportItem GetAccidentCauseReportItemById(string AccidentCauseReportItemId)
        {
            return new Model.SGGLDB(Funs.ConnString).InformationProject_AccidentCauseReportItem.FirstOrDefault(e => e.AccidentCauseReportItemId == AccidentCauseReportItemId);
        }

        /// <summary>
        /// 增加职工伤亡事故原因分析报表明细表
        /// </summary>
        /// <param name="AccidentCauseReportItem">职工伤亡事故原因分析报表明细表实体</param>
        public static void AddAccidentCauseReportItem(Model.InformationProject_AccidentCauseReportItem AccidentCauseReportItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InformationProject_AccidentCauseReportItem newAccidentCauseReportItem = new Model.InformationProject_AccidentCauseReportItem
            {
                AccidentCauseReportItemId = AccidentCauseReportItem.AccidentCauseReportItemId,
                AccidentCauseReportId = AccidentCauseReportItem.AccidentCauseReportId,
                AccidentType = AccidentCauseReportItem.AccidentType,
                TotalDeath = AccidentCauseReportItem.TotalDeath,
                TotalInjuries = AccidentCauseReportItem.TotalInjuries,
                TotalMinorInjuries = AccidentCauseReportItem.TotalMinorInjuries,
                Death1 = AccidentCauseReportItem.Death1,
                Injuries1 = AccidentCauseReportItem.Injuries1,
                MinorInjuries1 = AccidentCauseReportItem.MinorInjuries1,
                Death2 = AccidentCauseReportItem.Death2,
                Injuries2 = AccidentCauseReportItem.Injuries2,
                MinorInjuries2 = AccidentCauseReportItem.MinorInjuries2,
                Death3 = AccidentCauseReportItem.Death3,
                Injuries3 = AccidentCauseReportItem.Injuries3,
                MinorInjuries3 = AccidentCauseReportItem.MinorInjuries3,
                Death4 = AccidentCauseReportItem.Death4,
                Injuries4 = AccidentCauseReportItem.Injuries4,
                MinorInjuries4 = AccidentCauseReportItem.MinorInjuries4,
                Death5 = AccidentCauseReportItem.Death5,
                Injuries5 = AccidentCauseReportItem.Injuries5,
                MinorInjuries5 = AccidentCauseReportItem.MinorInjuries5,
                Death6 = AccidentCauseReportItem.Death6,
                Injuries6 = AccidentCauseReportItem.Injuries6,
                MinorInjuries6 = AccidentCauseReportItem.MinorInjuries6,
                Death7 = AccidentCauseReportItem.Death7,
                Injuries7 = AccidentCauseReportItem.Injuries7,
                MinorInjuries7 = AccidentCauseReportItem.MinorInjuries7,
                Death8 = AccidentCauseReportItem.Death8,
                Injuries8 = AccidentCauseReportItem.Injuries8,
                MinorInjuries8 = AccidentCauseReportItem.MinorInjuries8,
                Death9 = AccidentCauseReportItem.Death9,
                Injuries9 = AccidentCauseReportItem.Injuries9,
                MinorInjuries9 = AccidentCauseReportItem.MinorInjuries9,
                Death10 = AccidentCauseReportItem.Death10,
                Injuries10 = AccidentCauseReportItem.Injuries10,
                MinorInjuries10 = AccidentCauseReportItem.MinorInjuries10,
                Death11 = AccidentCauseReportItem.Death11,
                Injuries11 = AccidentCauseReportItem.Injuries11,
                MinorInjuries11 = AccidentCauseReportItem.MinorInjuries11
            };

            db.InformationProject_AccidentCauseReportItem.InsertOnSubmit(newAccidentCauseReportItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改职工伤亡事故原因分析报表明细表
        /// </summary>
        /// <param name="AccidentCauseReportItem">职工伤亡事故原因分析报表明细表实体</param>
        public static void UpdateAccidentCauseReportItem(Model.InformationProject_AccidentCauseReportItem AccidentCauseReportItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InformationProject_AccidentCauseReportItem newAccidentCauseReportItem = db.InformationProject_AccidentCauseReportItem.FirstOrDefault(e => e.AccidentCauseReportItemId == AccidentCauseReportItem.AccidentCauseReportItemId);
            newAccidentCauseReportItem.AccidentType = AccidentCauseReportItem.AccidentType;
            newAccidentCauseReportItem.TotalDeath = AccidentCauseReportItem.TotalDeath;
            newAccidentCauseReportItem.TotalInjuries = AccidentCauseReportItem.TotalInjuries;
            newAccidentCauseReportItem.TotalMinorInjuries = AccidentCauseReportItem.TotalMinorInjuries;
            newAccidentCauseReportItem.Death1 = AccidentCauseReportItem.Death1;
            newAccidentCauseReportItem.Injuries1 = AccidentCauseReportItem.Injuries1;
            newAccidentCauseReportItem.MinorInjuries1 = AccidentCauseReportItem.MinorInjuries1;
            newAccidentCauseReportItem.Death2 = AccidentCauseReportItem.Death2;
            newAccidentCauseReportItem.Injuries2 = AccidentCauseReportItem.Injuries2;
            newAccidentCauseReportItem.MinorInjuries2 = AccidentCauseReportItem.MinorInjuries2;
            newAccidentCauseReportItem.Death3 = AccidentCauseReportItem.Death3;
            newAccidentCauseReportItem.Injuries3 = AccidentCauseReportItem.Injuries3;
            newAccidentCauseReportItem.MinorInjuries3 = AccidentCauseReportItem.MinorInjuries3;
            newAccidentCauseReportItem.Death4 = AccidentCauseReportItem.Death4;
            newAccidentCauseReportItem.Injuries4 = AccidentCauseReportItem.Injuries4;
            newAccidentCauseReportItem.MinorInjuries4 = AccidentCauseReportItem.MinorInjuries4;
            newAccidentCauseReportItem.Death5 = AccidentCauseReportItem.Death5;
            newAccidentCauseReportItem.Injuries5 = AccidentCauseReportItem.Injuries5;
            newAccidentCauseReportItem.MinorInjuries5 = AccidentCauseReportItem.MinorInjuries5;
            newAccidentCauseReportItem.Death6 = AccidentCauseReportItem.Death6;
            newAccidentCauseReportItem.Injuries6 = AccidentCauseReportItem.Injuries6;
            newAccidentCauseReportItem.MinorInjuries6 = AccidentCauseReportItem.MinorInjuries6;
            newAccidentCauseReportItem.Death7 = AccidentCauseReportItem.Death7;
            newAccidentCauseReportItem.Injuries7 = AccidentCauseReportItem.Injuries7;
            newAccidentCauseReportItem.MinorInjuries7 = AccidentCauseReportItem.MinorInjuries7;
            newAccidentCauseReportItem.Death8 = AccidentCauseReportItem.Death8;
            newAccidentCauseReportItem.Injuries8 = AccidentCauseReportItem.Injuries8;
            newAccidentCauseReportItem.MinorInjuries8 = AccidentCauseReportItem.MinorInjuries8;
            newAccidentCauseReportItem.Death9 = AccidentCauseReportItem.Death9;
            newAccidentCauseReportItem.Injuries9 = AccidentCauseReportItem.Injuries9;
            newAccidentCauseReportItem.MinorInjuries9 = AccidentCauseReportItem.MinorInjuries9;
            newAccidentCauseReportItem.Death10 = AccidentCauseReportItem.Death10;
            newAccidentCauseReportItem.Injuries10 = AccidentCauseReportItem.Injuries10;
            newAccidentCauseReportItem.MinorInjuries10 = AccidentCauseReportItem.MinorInjuries10;
            newAccidentCauseReportItem.Death11 = AccidentCauseReportItem.Death11;
            newAccidentCauseReportItem.Injuries11 = AccidentCauseReportItem.Injuries11;
            newAccidentCauseReportItem.MinorInjuries11 = AccidentCauseReportItem.MinorInjuries11;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据Id删除所有数据
        /// </summary>
        /// <param name="AccidentCauseReportItemId"></param>
        public static void DeleteAccidentCauseReportItemByAccidentCauseReportId(string AccidentCauseReportId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var q = from x in db.InformationProject_AccidentCauseReportItem where x.AccidentCauseReportId == AccidentCauseReportId select x;
            if (q != null)
            {
                db.InformationProject_AccidentCauseReportItem.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主表Id获取明细记录集合(不包含总计行)
        /// </summary>
        /// <param name="AccidentCauseReportItemId">职工伤亡事故原因分析报表明细表Id</param>
        /// <returns>明细记录集合</returns>
        public static List<Model.InformationProject_AccidentCauseReportItem> GetItemsNoSum(string AccidentCauseReportId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                return (from x in db.InformationProject_AccidentCauseReportItem
                        join y in db.Sys_Const on x.AccidentType equals y.ConstText
                        where x.AccidentCauseReportId == AccidentCauseReportId && x.AccidentType != "总计" && y.GroupId == ConstValue.Group_0012
                        orderby y.SortIndex
                        select x).ToList();
            }
        }
    }
}
