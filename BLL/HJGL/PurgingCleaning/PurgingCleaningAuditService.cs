using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PurgingCleaningAuditService
    {
        /// <summary>
        /// 根据吹扫/清洗试验Id获取用于管线明细信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static List<Model.View_PC_PurgingCleaningAudit> GetPurgingCleaningAuditByPurgingCleaningId(string PurgingCleaningId)
        {

            var view = from x in Funs.DB.View_PC_PurgingCleaningAudit
                       where x.PurgingCleaningId == PurgingCleaningId
                       select x;
            return view.ToList();
        }

        /// <summary>
        /// 审核吹扫/清洗试验信息
        /// </summary>
        /// <param name="leakVacuum">吹扫/清洗试验实体</param>
        public static void AuditleakVacuum(Model.HJGL_PC_PurgingCleaning leakVacuum)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_PC_PurgingCleaning newleakVacuum = db.HJGL_PC_PurgingCleaning.FirstOrDefault(e => e.PurgingCleaningId == leakVacuum.PurgingCleaningId);
            if (newleakVacuum != null)
            {
                newleakVacuum.PurgingCleaningId = leakVacuum.PurgingCleaningId;
                newleakVacuum.Auditer = leakVacuum.Auditer;
                newleakVacuum.AduditDate = leakVacuum.AduditDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 吹扫/清洗试验完工审核信息
        /// </summary>
        /// <param name="leakVacuum">吹扫/清洗试验实体</param>
        public static void AuditFinishDef(Model.HJGL_PC_PurgingCleaning leakVacuum)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_PC_PurgingCleaning newleakVacuum = db.HJGL_PC_PurgingCleaning.FirstOrDefault(e => e.PurgingCleaningId == leakVacuum.PurgingCleaningId);
            if (newleakVacuum != null)
            {
                newleakVacuum.PurgingCleaningId = leakVacuum.PurgingCleaningId;
                newleakVacuum.Finisher = leakVacuum.Finisher;
                newleakVacuum.FinishDate = leakVacuum.FinishDate;
                newleakVacuum.FinishDef = leakVacuum.FinishDef;
                db.SubmitChanges();
            }
        }
    }
}
