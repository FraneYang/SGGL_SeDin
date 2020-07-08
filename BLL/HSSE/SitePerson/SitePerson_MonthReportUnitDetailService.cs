using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public class SitePerson_MonthReportUnitDetailService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 增加工作日报明细信息
        /// </summary>
        /// <param name="monthReportUnitDetail">工作日报明细实体</param>
        public static void AddMonthReportUnitDetail(Model.SitePerson_MonthReportUnitDetail monthReportUnitDetail)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.SitePerson_MonthReportUnitDetail newMonthReportUnitDetail = new Model.SitePerson_MonthReportUnitDetail();
            string newKeyID = SQLHelper.GetNewID(typeof(Model.SitePerson_MonthReportUnitDetail));
            newMonthReportUnitDetail.MonthReportUnitDetailId = newKeyID;
            newMonthReportUnitDetail.MonthReportDetailId = monthReportUnitDetail.MonthReportDetailId;
            newMonthReportUnitDetail.PostId = monthReportUnitDetail.PostId;
            newMonthReportUnitDetail.CheckPersonNum = monthReportUnitDetail.CheckPersonNum;
            newMonthReportUnitDetail.RealPersonNum = monthReportUnitDetail.RealPersonNum;
            newMonthReportUnitDetail.PersonWorkTime = monthReportUnitDetail.PersonWorkTime;
            newMonthReportUnitDetail.Remark = monthReportUnitDetail.Remark;
            db.SitePerson_MonthReportUnitDetail.InsertOnSubmit(newMonthReportUnitDetail);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改工作日报明细信息
        /// </summary>
        /// <param name="monthReportDetail">工作日报明细实体</param>
        public static void UpdateMonthReportUnitDetail(Model.SitePerson_MonthReportUnitDetail monthReportUnitDetail)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.SitePerson_MonthReportUnitDetail newMonthReportUnitDetail = db.SitePerson_MonthReportUnitDetail.FirstOrDefault(e => e.MonthReportUnitDetailId == monthReportUnitDetail.MonthReportUnitDetailId);
            if (newMonthReportUnitDetail != null)
            {
                newMonthReportUnitDetail.PostId = monthReportUnitDetail.PostId;
                newMonthReportUnitDetail.CheckPersonNum = monthReportUnitDetail.CheckPersonNum;
                newMonthReportUnitDetail.RealPersonNum = monthReportUnitDetail.RealPersonNum;
                newMonthReportUnitDetail.PersonWorkTime = monthReportUnitDetail.PersonWorkTime;
                newMonthReportUnitDetail.Remark = monthReportUnitDetail.Remark;
                db.SubmitChanges();
            }
        }
    }
}
