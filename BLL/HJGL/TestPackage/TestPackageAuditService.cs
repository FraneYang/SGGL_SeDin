using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public class TestPackageAuditService
    {
        /// <summary>
        /// 根据试压Id获取用于管线明细信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static List<Model.View_PTP_TestPackageAudit> GetTestPackageAuditByPTP_ID(string PTP_ID)
        {

            var view = from x in new Model.SGGLDB(Funs.ConnString).View_PTP_TestPackageAudit
                       where x.PTP_ID == PTP_ID
                       select x;
            return view.ToList();
        }

        /// <summary>
        /// 审核试压信息
        /// </summary>
        /// <param name="testPackage">试压实体</param>
        public static void AuditTestPackage(Model.PTP_TestPackage testPackage)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.PTP_TestPackage newTestPackage = db.PTP_TestPackage.FirstOrDefault(e => e.PTP_ID == testPackage.PTP_ID);
            if (newTestPackage != null)
            {
                newTestPackage.PTP_ID = testPackage.PTP_ID;
                newTestPackage.Auditer = testPackage.Auditer;
                newTestPackage.AduditDate = testPackage.AduditDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 试压完工审核信息
        /// </summary>
        /// <param name="testPackage">试压实体</param>
        public static void AuditFinishDef(Model.PTP_TestPackage testPackage)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.PTP_TestPackage newTestPackage = db.PTP_TestPackage.FirstOrDefault(e => e.PTP_ID == testPackage.PTP_ID);
            if (newTestPackage != null)
            {
                newTestPackage.PTP_ID = testPackage.PTP_ID;
                newTestPackage.Finisher = testPackage.Finisher;
                newTestPackage.FinishDate = testPackage.FinishDate;
                newTestPackage.FinishDef = testPackage.FinishDef;
                db.SubmitChanges();
            }
        }
    }
}
