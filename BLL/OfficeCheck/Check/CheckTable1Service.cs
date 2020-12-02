using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class CheckTable1Service
    {
        public static Model.ProjectSupervision_CheckTable1 GetCheckTable1ByCheckNoticeId(string checkNoticeId)
        {
            return Funs.DB.ProjectSupervision_CheckTable1.FirstOrDefault(e => e.CheckNoticeId == checkNoticeId);
        }

        public static void AddCheckTable1(Model.ProjectSupervision_CheckTable1 table1)
        {
            Model.ProjectSupervision_CheckTable1 newTable1 = new Model.ProjectSupervision_CheckTable1();
            newTable1.CheckItemId = table1.CheckItemId;
            newTable1.CheckNoticeId = table1.CheckNoticeId;
            newTable1.SubjectProjectId = table1.SubjectProjectId;
            newTable1.CheckMan = table1.CheckMan;
            newTable1.CheckLeader = table1.CheckLeader;
            newTable1.CheckDate = table1.CheckDate;
            newTable1.SubjectUnitMan = table1.SubjectUnitMan;
            newTable1.SubjectUnitDate = table1.SubjectUnitDate;
            newTable1.TotalBaseScore = table1.TotalBaseScore;
            newTable1.TotalDeletScore = table1.TotalDeletScore;
            newTable1.TotalGetScore = table1.TotalGetScore;
            newTable1.Total100Score = table1.Total100Score;
            newTable1.TotalLastScore = table1.TotalLastScore;
            newTable1.EvaluationResult = table1.EvaluationResult;
            Funs.DB.ProjectSupervision_CheckTable1.InsertOnSubmit(newTable1);
            Funs.DB.SubmitChanges();
        }

        public static void DeleteCheckTable1ByNoticeId(string noticeId)
        {
            Model.ProjectSupervision_CheckTable1 table1 = Funs.DB.ProjectSupervision_CheckTable1.FirstOrDefault(e => e.CheckNoticeId == noticeId);
            if (table1!=null)
            {
                Funs.DB.ProjectSupervision_CheckTable1.DeleteOnSubmit(table1);
                Funs.DB.SubmitChanges();
            }
        }
    }
}
