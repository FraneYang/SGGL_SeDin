using System.Linq;

namespace BLL
{
    /// <summary>
    /// 完工报告
    /// </summary>
    public static class CompletionReportService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取完工报告
        /// </summary>
        /// <param name="completionReportId"></param>
        /// <returns></returns>
        public static Model.Manager_CompletionReport GetCompletionReportById(string completionReportId)
        {
            return new Model.SGGLDB(Funs.ConnString).Manager_CompletionReport.FirstOrDefault(e => e.CompletionReportId == completionReportId);
        }

        /// <summary>
        /// 添加完工报告
        /// </summary>
        /// <param name="completionReport"></param>
        public static void AddCompletionReport(Model.Manager_CompletionReport completionReport)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Manager_CompletionReport newCompletionReport = new Model.Manager_CompletionReport
            {
                CompletionReportId = completionReport.CompletionReportId,
                ProjectId = completionReport.ProjectId,
                CompletionReportCode = completionReport.CompletionReportCode,
                CompletionReportName = completionReport.CompletionReportName,
                FileContent = completionReport.FileContent,
                CompileMan = completionReport.CompileMan,
                CompileDate = completionReport.CompileDate,
                States = completionReport.States
            };
            db.Manager_CompletionReport.InsertOnSubmit(newCompletionReport);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectCompletionReportMenuId, completionReport.ProjectId, null, completionReport.CompletionReportId, completionReport.CompileDate);
        }

        /// <summary>
        /// 修改完工报告
        /// </summary>
        /// <param name="completionReport"></param>
        public static void UpdateCompletionReport(Model.Manager_CompletionReport completionReport)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Manager_CompletionReport newCompletionReport = db.Manager_CompletionReport.FirstOrDefault(e => e.CompletionReportId == completionReport.CompletionReportId);
            if (newCompletionReport != null)
            {
                //newCompletionReport.ProjectId = completionReport.ProjectId;
                newCompletionReport.CompletionReportCode = completionReport.CompletionReportCode;
                newCompletionReport.CompletionReportName = completionReport.CompletionReportName;
                newCompletionReport.FileContent = completionReport.FileContent;
                newCompletionReport.CompileMan = completionReport.CompileMan;
                newCompletionReport.CompileDate = completionReport.CompileDate;
                newCompletionReport.States = completionReport.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除完工报告
        /// </summary>
        /// <param name="completionReportId"></param>
        public static void DeleteCompletionReportById(string completionReportId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Manager_CompletionReport completionReport = db.Manager_CompletionReport.FirstOrDefault(e => e.CompletionReportId == completionReportId);
            if (completionReport != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(completionReportId);
                BLL.CommonService.DeleteAttachFileById(completionReportId);//删除附件
                BLL.CommonService.DeleteFlowOperateByID(completionReportId);//删除审核流程
                db.Manager_CompletionReport.DeleteOnSubmit(completionReport);
                db.SubmitChanges();
            }
        }
    }
}
