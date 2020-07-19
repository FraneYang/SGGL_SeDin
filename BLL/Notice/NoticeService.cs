using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 通知
    /// </summary>
    public static class NoticeService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取通知
        /// </summary>
        /// <param name="noticeId"></param>
        /// <returns></returns>
        public static Model.InformationProject_Notice GetNoticeById(string noticeId)
        {
            return Funs.DB.InformationProject_Notice.FirstOrDefault(e => e.NoticeId == noticeId);
        }

        /// <summary>
        /// 增加图片信息
        /// </summary>
        /// <param name="personQuality">图片实体</param>
        public static void AddNotice(Model.InformationProject_Notice Notice)
        {
            Model.SGGLDB db = Funs.DB;
            Model.InformationProject_Notice newNotice = new Model.InformationProject_Notice
            {
                NoticeId = Notice.NoticeId,
                NoticeCode = Notice.NoticeCode,
                ProjectId = Notice.ProjectId,
                NoticeTitle = Notice.NoticeTitle,
                MainContent = Notice.MainContent,
                CompileMan = Notice.CompileMan,
                CompileDate = Notice.CompileDate,
                IsRelease = Notice.IsRelease,
                ReleaseDate = Notice.ReleaseDate,
                States = Notice.States,
                AccessProjectId = Notice.AccessProjectId,
                AccessProjectText = Notice.AccessProjectText
            };
            db.InformationProject_Notice.InsertOnSubmit(newNotice);
            db.SubmitChanges();

            if (!string.IsNullOrEmpty(Notice.ProjectId))
            {
                if (Notice.NoticeCode == BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectNoticeMenuId, Notice.ProjectId, null))
                {
                    ////增加一条编码记录
                    BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectNoticeMenuId, Notice.ProjectId, null, Notice.NoticeId, Notice.CompileDate);
                }
            }
            else
            {
                if (Notice.NoticeCode == BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ServerNoticeMenuId, Notice.ProjectId, null))
                {
                    BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ServerNoticeMenuId, Notice.ProjectId, null, Notice.NoticeId, Notice.CompileDate);
                }
            }
        }

        /// <summary>
        /// 修改通知
        /// </summary>
        /// <param name="notice"></param>
        public static void UpdateNotice(Model.InformationProject_Notice notice)
        {
            Model.SGGLDB db = Funs.DB;
            Model.InformationProject_Notice newNotice = db.InformationProject_Notice.FirstOrDefault(e => e.NoticeId == notice.NoticeId);
            if (newNotice != null)
            {
                newNotice.NoticeCode = notice.NoticeCode;
                newNotice.ProjectId = notice.ProjectId;
                newNotice.NoticeTitle = notice.NoticeTitle;
                newNotice.MainContent = notice.MainContent;
                newNotice.CompileMan = notice.CompileMan;
                newNotice.CompileDate = notice.CompileDate;
                newNotice.IsRelease = notice.IsRelease;
                newNotice.ReleaseDate = notice.ReleaseDate;
                newNotice.States = notice.States;
                newNotice.AccessProjectId = notice.AccessProjectId;
                newNotice.AccessProjectText = notice.AccessProjectText;
                db.SubmitChanges();

                //if (!string.IsNullOrEmpty(notice.NoticeCode))
                //{
                //    ///删除编码表记录
                //    BLL.CodeRecordsService.DeleteCodeRecordsByDataId(notice.NoticeId);
                //}
                if (BLL.CodeRecordsService.ReturnCodeByDataId(notice.NoticeId) != notice.NoticeCode)
                {
                    ////增加一条编码记录
                    BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectNoticeMenuId, notice.ProjectId, null, notice.NoticeId, notice.CompileDate);
                    if (!string.IsNullOrEmpty(notice.ProjectId))
                    {
                        BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectNoticeMenuId, notice.ProjectId, null, notice.NoticeId, notice.CompileDate);
                    }
                    else
                    {
                        BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ServerNoticeMenuId, notice.ProjectId, null, notice.NoticeId, notice.CompileDate);
                    }
                }
            }
        }

        /// <summary>
        /// 根据主键删除通知
        /// </summary>
        /// <param name="noticeId"></param>
        public static void DeleteNoticeById(string noticeId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.InformationProject_Notice Notice = db.InformationProject_Notice.FirstOrDefault(e => e.NoticeId == noticeId);
            if (Notice != null)
            {
                BLL.CommonService.DeleteAttachFileById(noticeId);
                ///删除审核流程相关数据
                BLL.CommonService.DeleteFlowOperateByID(Notice.NoticeId);
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(noticeId);
                //// 删除浏览记录
                UserService.DeleteUserRead(noticeId);
                db.InformationProject_Notice.DeleteOnSubmit(Notice);
                db.SubmitChanges();
            }
        }        
    }
}