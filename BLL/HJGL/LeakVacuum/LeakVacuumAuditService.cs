using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LeakVacuumAuditService
    {
        /// <summary>
        /// 根据泄露性/真空试验Id获取用于管线明细信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static List<Model.View_LV_LeakVacuumAudit> GetLeakVacuumAuditByLeakVacuumId(string LeakVacuumId)
        {

            var view = from x in Funs.DB.View_LV_LeakVacuumAudit
                       where x.LeakVacuumId == LeakVacuumId
                       select x;
            return view.ToList();
        }

        /// <summary>
        /// 审核泄露性/真空试验信息
        /// </summary>
        /// <param name="leakVacuum">泄露性/真空试验实体</param>
        public static void AuditleakVacuum(Model.HJGL_LV_LeakVacuum leakVacuum)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_LV_LeakVacuum newleakVacuum = db.HJGL_LV_LeakVacuum.FirstOrDefault(e => e.LeakVacuumId == leakVacuum.LeakVacuumId);
            if (newleakVacuum != null)
            {
                newleakVacuum.LeakVacuumId = leakVacuum.LeakVacuumId;
                newleakVacuum.Auditer = leakVacuum.Auditer;
                newleakVacuum.AduditDate = leakVacuum.AduditDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 泄露性/真空试验完工审核信息
        /// </summary>
        /// <param name="leakVacuum">泄露性/真空试验实体</param>
        public static void AuditFinishDef(Model.HJGL_LV_LeakVacuum leakVacuum)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_LV_LeakVacuum newleakVacuum = db.HJGL_LV_LeakVacuum.FirstOrDefault(e => e.LeakVacuumId == leakVacuum.LeakVacuumId);
            if (newleakVacuum != null)
            {
                newleakVacuum.LeakVacuumId = leakVacuum.LeakVacuumId;
                newleakVacuum.Finisher = leakVacuum.Finisher;
                newleakVacuum.FinishDate = leakVacuum.FinishDate;
                newleakVacuum.FinishDef = leakVacuum.FinishDef;
                db.SubmitChanges();
            }
        }
    }
}
