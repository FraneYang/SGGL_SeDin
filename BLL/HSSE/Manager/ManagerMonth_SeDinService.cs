using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 管理月报
    /// </summary>
    public class ManagerMonth_SeDinService
    {
        /// <summary>
        /// 根据月报告主键获取月报告信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns>月报告信息</returns>
        public static Model.SeDin_MonthReport GetMonthReportByMonthReportId(string monthReportId)
        {
            return Funs.DB.SeDin_MonthReport.FirstOrDefault(x => x.MonthReportId == monthReportId);
        }

        /// <summary>
        /// 根据时间获取最近时间的月报
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static bool GetMonthReportByDate(DateTime date, string projectId)
        {
            var a = Funs.DB.SeDin_MonthReport.FirstOrDefault(x => x.ProjectId == projectId && x.ReporMonth.Value.Year == date.Year && x.ReporMonth.Value.Month == date.Month);
            return (a != null);
        }

        /// <summary>
        /// 根据月报告主键删除一个月报告信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteMonthReportByMonthReportId(string monthReportId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.SeDin_MonthReport monthReport = db.SeDin_MonthReport.FirstOrDefault(e => e.MonthReportId == monthReportId);
            if (monthReport != null)
            {
                var delMonthReport1 = from x in db.SeDin_MonthReport1 where x.MonthReportId == monthReportId select x;
                if (delMonthReport1.Count() > 0)
                {
                    db.SeDin_MonthReport1.DeleteAllOnSubmit(delMonthReport1);
                    db.SubmitChanges();
                }
                var delMonthReport2 = from x in db.SeDin_MonthReport2 where x.MonthReportId == monthReportId select x;
                if (delMonthReport2.Count() > 0)
                {
                    db.SeDin_MonthReport2.DeleteAllOnSubmit(delMonthReport2);
                    db.SubmitChanges();
                }
                var delMonthReport3 = from x in db.SeDin_MonthReport3 where x.MonthReportId == monthReportId select x;
                if (delMonthReport3.Count() > 0)
                {
                    db.SeDin_MonthReport3.DeleteAllOnSubmit(delMonthReport3);
                    db.SubmitChanges();
                }
                var delMonthReport4 = from x in db.SeDin_MonthReport4 where x.MonthReportId == monthReportId select x;
                if (delMonthReport4.Count() > 0)
                {
                    db.SeDin_MonthReport4.DeleteAllOnSubmit(delMonthReport4);
                    db.SubmitChanges();
                }
                var delMonthReport5 = from x in db.SeDin_MonthReport5 where x.MonthReportId == monthReportId select x;
                if (delMonthReport5.Count() > 0)
                {
                    db.SeDin_MonthReport5.DeleteAllOnSubmit(delMonthReport5);
                    db.SubmitChanges();
                }
                var delMonthReport6 = from x in db.SeDin_MonthReport6 where x.MonthReportId == monthReportId select x;
                if (delMonthReport6.Count() > 0)
                {
                    db.SeDin_MonthReport6.DeleteAllOnSubmit(delMonthReport6);
                    db.SubmitChanges();
                }
                var delMonthReport7 = from x in db.SeDin_MonthReport7 where x.MonthReportId == monthReportId select x;
                if (delMonthReport7.Count() > 0)
                {
                    db.SeDin_MonthReport7.DeleteAllOnSubmit(delMonthReport7);
                    db.SubmitChanges();
                }
                var delMonthReport8 = from x in db.SeDin_MonthReport8 where x.MonthReportId == monthReportId select x;
                if (delMonthReport8.Count() > 0)
                {
                    db.SeDin_MonthReport8.DeleteAllOnSubmit(delMonthReport8);
                    db.SubmitChanges();
                }
                var delMonthReport8Item = from x in db.SeDin_MonthReport8Item where x.MonthReportId == monthReportId select x;
                if (delMonthReport8Item.Count() > 0)
                {
                    db.SeDin_MonthReport8Item.DeleteAllOnSubmit(delMonthReport8Item);
                    db.SubmitChanges();
                }
                var delMonthReport9 = from x in db.SeDin_MonthReport9 where x.MonthReportId == monthReportId select x;
                if (delMonthReport9.Count() > 0)
                {
                    db.SeDin_MonthReport9.DeleteAllOnSubmit(delMonthReport9);
                    db.SubmitChanges();
                }
                var delMonthReport9Item_Rectification = from x in db.SeDin_MonthReport9Item_Rectification where x.MonthReportId == monthReportId select x;
                if (delMonthReport9Item_Rectification.Count() > 0)
                {
                    db.SeDin_MonthReport9Item_Rectification.DeleteAllOnSubmit(delMonthReport9Item_Rectification);
                    db.SubmitChanges();
                }
                var delMonthReport9Item_Special = from x in db.SeDin_MonthReport9Item_Special where x.MonthReportId == monthReportId select x;
                if (delMonthReport9Item_Special.Count() > 0)
                {
                    db.SeDin_MonthReport9Item_Special.DeleteAllOnSubmit(delMonthReport9Item_Special);
                    db.SubmitChanges();
                }
                var delMonthReport9Item_Stoppage = from x in db.SeDin_MonthReport9Item_Stoppage where x.MonthReportId == monthReportId select x;
                if (delMonthReport9Item_Stoppage.Count() > 0)
                {
                    db.SeDin_MonthReport9Item_Stoppage.DeleteAllOnSubmit(delMonthReport9Item_Stoppage);
                    db.SubmitChanges();
                }
                var delMonthReport10 = from x in db.SeDin_MonthReport10 where x.MonthReportId == monthReportId select x;
                if (delMonthReport10.Count() > 0)
                {
                    db.SeDin_MonthReport10.DeleteAllOnSubmit(delMonthReport10);
                    db.SubmitChanges();
                }
                var delMonthReport11 = from x in db.SeDin_MonthReport11 where x.MonthReportId == monthReportId select x;
                if (delMonthReport11.Count() > 0)
                {
                    db.SeDin_MonthReport11.DeleteAllOnSubmit(delMonthReport11);
                    db.SubmitChanges();
                }
                var delMonthReport12 = from x in db.SeDin_MonthReport12 where x.MonthReportId == monthReportId select x;
                if (delMonthReport12.Count() > 0)
                {
                    db.SeDin_MonthReport12.DeleteAllOnSubmit(delMonthReport12);
                    db.SubmitChanges();
                }

                db.SeDin_MonthReport.DeleteOnSubmit(monthReport);
                db.SubmitChanges();
            }
        }
    }
}
