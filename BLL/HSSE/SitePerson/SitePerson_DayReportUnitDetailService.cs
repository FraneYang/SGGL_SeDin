using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public class SitePerson_DayReportUnitDetailService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);


        /// <summary>
        /// 增加工作日报明细信息
        /// </summary>
        /// <param name="dayReportUnitDetail">工作日报明细实体</param>
        public static void AddDayReportUnitDetail(Model.SitePerson_DayReportUnitDetail dayReportUnitDetail)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.SitePerson_DayReportUnitDetail newDayReportUnitDetail = new Model.SitePerson_DayReportUnitDetail();
            string newKeyID = SQLHelper.GetNewID(typeof(Model.SitePerson_DayReportUnitDetail));
            newDayReportUnitDetail.DayReportUnitDetailId = newKeyID;
            newDayReportUnitDetail.DayReportDetailId = dayReportUnitDetail.DayReportDetailId;
            newDayReportUnitDetail.PostId = dayReportUnitDetail.PostId;
            newDayReportUnitDetail.CheckPersonNum = dayReportUnitDetail.CheckPersonNum;
            newDayReportUnitDetail.RealPersonNum = dayReportUnitDetail.RealPersonNum;
            newDayReportUnitDetail.PersonWorkTime = dayReportUnitDetail.PersonWorkTime;
            newDayReportUnitDetail.Remark = dayReportUnitDetail.Remark;
            db.SitePerson_DayReportUnitDetail.InsertOnSubmit(newDayReportUnitDetail);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改工作日报明细信息
        /// </summary>
        /// <param name="dayReportDetail">工作日报明细实体</param>
        public static void UpdateDayReportUnitDetail(Model.SitePerson_DayReportUnitDetail dayReportUnitDetail)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.SitePerson_DayReportUnitDetail newDayReportUnitDetail = db.SitePerson_DayReportUnitDetail.FirstOrDefault(e => e.DayReportUnitDetailId == dayReportUnitDetail.DayReportUnitDetailId);
            if (newDayReportUnitDetail != null)
            {
                newDayReportUnitDetail.PostId = dayReportUnitDetail.PostId;
                newDayReportUnitDetail.CheckPersonNum = dayReportUnitDetail.CheckPersonNum;
                newDayReportUnitDetail.RealPersonNum = dayReportUnitDetail.RealPersonNum;
                newDayReportUnitDetail.PersonWorkTime = dayReportUnitDetail.PersonWorkTime;
                newDayReportUnitDetail.Remark = dayReportUnitDetail.Remark;
                db.SubmitChanges();
            }
        }
    }
}
