using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 突发环境事件
    /// </summary>
    public static class UnexpectedEnvironmentalService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取突发环境事件
        /// </summary>
        /// <param name="UnexpectedEnvironmentalId"></param>
        /// <returns></returns>
        public static Model.Environmental_UnexpectedEnvironmental GetUnexpectedEnvironmentalById(string fileId)
        {
            return new Model.SGGLDB(Funs.ConnString).Environmental_UnexpectedEnvironmental.FirstOrDefault(e => e.FileId == fileId);
        }

        /// <summary>
        /// 添加突发环境事件
        /// </summary>
        /// <param name="UnexpectedEnvironmental"></param>
        public static void AddUnexpectedEnvironmental(Model.Environmental_UnexpectedEnvironmental UnexpectedEnvironmental)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Environmental_UnexpectedEnvironmental newUnexpectedEnvironmental = new Model.Environmental_UnexpectedEnvironmental
            {
                FileId = UnexpectedEnvironmental.FileId,
                FileCode = UnexpectedEnvironmental.FileCode,
                ProjectId = UnexpectedEnvironmental.ProjectId,
                FileName = UnexpectedEnvironmental.FileName,
                FileContent = UnexpectedEnvironmental.FileContent,
                CompileMan = UnexpectedEnvironmental.CompileMan,
                CompileDate = UnexpectedEnvironmental.CompileDate,
                AttachUrl = UnexpectedEnvironmental.AttachUrl,
                States = UnexpectedEnvironmental.States
            };
            db.Environmental_UnexpectedEnvironmental.InsertOnSubmit(newUnexpectedEnvironmental);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.UnexpectedEnvironmentalMenuId, UnexpectedEnvironmental.ProjectId, null, UnexpectedEnvironmental.FileId, UnexpectedEnvironmental.CompileDate);
        }

        /// <summary>
        /// 修改突发环境事件
        /// </summary>
        /// <param name="UnexpectedEnvironmental"></param>
        public static void UpdateUnexpectedEnvironmental(Model.Environmental_UnexpectedEnvironmental UnexpectedEnvironmental)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Environmental_UnexpectedEnvironmental newUnexpectedEnvironmental = db.Environmental_UnexpectedEnvironmental.FirstOrDefault(e => e.FileId == UnexpectedEnvironmental.FileId);
            if (newUnexpectedEnvironmental != null)
            {
                newUnexpectedEnvironmental.FileCode = UnexpectedEnvironmental.FileCode;
                newUnexpectedEnvironmental.FileName = UnexpectedEnvironmental.FileName;
                newUnexpectedEnvironmental.FileContent = UnexpectedEnvironmental.FileContent;
                newUnexpectedEnvironmental.CompileMan = UnexpectedEnvironmental.CompileMan;
                newUnexpectedEnvironmental.CompileDate = UnexpectedEnvironmental.CompileDate;
                newUnexpectedEnvironmental.AttachUrl = UnexpectedEnvironmental.AttachUrl;
                newUnexpectedEnvironmental.States = UnexpectedEnvironmental.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除突发环境事件
        /// </summary>
        /// <param name="FileId"></param>
        public static void DeleteUnexpectedEnvironmentalById(string FileId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Environmental_UnexpectedEnvironmental UnexpectedEnvironmental = db.Environmental_UnexpectedEnvironmental.FirstOrDefault(e => e.FileId == FileId);
            if (UnexpectedEnvironmental != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(FileId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(UnexpectedEnvironmental.FileId);
                BLL.CommonService.DeleteFlowOperateByID(FileId);
                db.Environmental_UnexpectedEnvironmental.DeleteOnSubmit(UnexpectedEnvironmental);
                db.SubmitChanges();
            }
        }
    }
}
