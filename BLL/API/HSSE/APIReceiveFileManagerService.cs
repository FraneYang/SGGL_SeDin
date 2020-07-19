using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;

namespace BLL
{
    /// <summary>
    /// 来文服务类
    /// </summary>
    public static class APIReceiveFileManagerService
    {
        #region 根据ReceiveFileManagerId获取来文详细信息
        /// <summary>
        /// 根据ReceiveFileManagerId获取来文详细信息
        /// </summary>
        /// <param name="receiveFileManagerId">来文ID</param>
        /// <param name="fileType">来文类型(0-项目发文；1-公司来文)</param>
        /// <returns>来文详细</returns>
        public static Model.ReceiveFileManagerItem getReceiveFileManagerById(string receiveFileManagerId, string fileType)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getReceiveFileManagerItem = (from x in db.InformationProject_ReceiveFileManager
                                                 where x.ReceiveFileManagerId == receiveFileManagerId && x.FileType == fileType
                                                 select new Model.ReceiveFileManagerItem
                                                 {
                                                     ReceiveFileManagerId = x.ReceiveFileManagerId,
                                                     ProjectId = x.ProjectId,
                                                     ReceiveFileCode = x.ReceiveFileCode,
                                                     ReceiveFileName = x.ReceiveFileName,
                                                     Version = x.Version,
                                                     FileUnitId = x.FileUnitId,
                                                     FileUnitName = db.Base_Unit.First(u => u.UnitId == x.FileUnitId).UnitName,
                                                     FileCode = x.FileCode,
                                                     FilePageNum = x.FilePageNum,
                                                     GetFileDate = string.Format("{0:yyyy-MM-dd}", x.GetFileDate),
                                                     SendPersonId = x.SendPersonId,
                                                     SendPersonName = db.Sys_User.First(u => u.UserId == x.SendPersonId).UserName,
                                                     MainContent = x.MainContent,
                                                     UnitIds = x.UnitIds,
                                                     UnitNames = UnitService.getUnitNamesUnitIds(x.UnitIds),
                                                     FileAttachUrl = APIUpLoadFileService.getFileUrl(x.ReceiveFileManagerId, null),
                                                     ReplyFileAttachUrl = APIUpLoadFileService.getFileUrl(x.ReceiveFileManagerId + "#1", null),
                                                     Issue = (db.InformationProject_ReceiveFileManager.FirstOrDefault(y => y.FromId == x.ReceiveFileManagerId) == null) ? false : true,
                                                 }).FirstOrDefault();
                return getReceiveFileManagerItem;
            }
        }
        #endregion

        #region 根据projectId、fileType获取来文列表
        /// <summary>
        /// 根据projectId、fileType获取来文列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="fileType">来文类型(0-项目发文；1-公司来文)</param>
        /// <param name="unitId">单位ID</param>
        /// <param name="states">状态（0待提交；1-已提交；2：已回复）</param>
        /// <returns></returns>
        public static List<Model.ReceiveFileManagerItem> getReceiveFileManagerList(string projectId, string fileType, string unitId, string states)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getReceiveFileManagerItem = (from x in db.InformationProject_ReceiveFileManager
                                                 where x.ProjectId == projectId && x.FileType == fileType
                                                 select new Model.ReceiveFileManagerItem
                                                 {
                                                     ReceiveFileManagerId = x.ReceiveFileManagerId,
                                                     ProjectId = x.ProjectId,
                                                     FileType = x.FileType,
                                                     ReceiveFileCode = x.ReceiveFileCode,
                                                     ReceiveFileName = x.ReceiveFileName,
                                                     Version = x.Version,
                                                     FileUnitId = x.FileUnitId,
                                                     FileUnitName = db.Base_Unit.First(u => u.UnitId == x.FileUnitId).UnitName,
                                                     FileCode = x.FileCode,
                                                     FilePageNum = x.FilePageNum,
                                                     GetFileDate = string.Format("{0:yyyy-MM-dd}", x.GetFileDate),
                                                     SendPersonId = x.SendPersonId,
                                                     SendPersonName = db.Sys_User.First(u => u.UserId == x.SendPersonId).UserName,
                                                     MainContent = x.MainContent,
                                                     UnitIds = x.UnitIds,
                                                     UnitNames = UnitService.getUnitNamesUnitIds(x.UnitIds),
                                                     FileAttachUrl = APIUpLoadFileService.getFileUrl(x.ReceiveFileManagerId, null),
                                                     ReplyFileAttachUrl = APIUpLoadFileService.getFileUrl(x.ReceiveFileManagerId + "#1", null),
                                                     States = x.States,
                                                     Issue = (db.InformationProject_ReceiveFileManager.FirstOrDefault(y => y.FromId == x.ReceiveFileManagerId) == null) ? false : true,
                                                 }).AsEnumerable();
                if (getReceiveFileManagerItem.Count() > 0)
                {
                    if (!string.IsNullOrEmpty(unitId) && unitId != Const.UnitId_SEDIN)
                    {
                        getReceiveFileManagerItem = getReceiveFileManagerItem.Where(x => x.FileUnitId == unitId || (x.UnitIds != null && x.UnitIds.Contains(unitId)));
                    }
                    if (states == Const.State_0)
                    {
                        getReceiveFileManagerItem = getReceiveFileManagerItem.Where(x => x.States == Const.State_0 || x.States == null);
                    }
                    else if (states == Const.State_1)
                    {
                        getReceiveFileManagerItem = getReceiveFileManagerItem.Where(x => x.States == Const.State_2 && (x.ReplyFileAttachUrl == null || x.ReplyFileAttachUrl == ""));
                    }
                    else if (states == Const.State_2)
                    {
                        getReceiveFileManagerItem = getReceiveFileManagerItem.Where(x => x.States == Const.State_2 && x.ReplyFileAttachUrl != null && x.ReplyFileAttachUrl != "");
                    }
                }

                return getReceiveFileManagerItem.OrderByDescending(x => x.ReceiveFileCode).ToList();
            }
        }
        #endregion

        #region 保存ReceiveFileManager
        /// <summary>
        /// 保存ReceiveFileManager
        /// </summary>
        /// <param name="newItem">来文信息</param>
        /// <returns></returns>
        public static void SaveReceiveFileManager(Model.ReceiveFileManagerItem newItem)
        {
            Model.SGGLDB db = Funs.DB;
            string menuId = string.Empty;
            Model.InformationProject_ReceiveFileManager newReceiveFile = new Model.InformationProject_ReceiveFileManager
            {
                FileType=newItem.FileType,
                ReceiveFileManagerId = newItem.ReceiveFileManagerId,
                ProjectId = newItem.ProjectId,
                ReceiveFileCode=newItem.ReceiveFileCode,
                ReceiveFileName=newItem.ReceiveFileName,
                Version=newItem.Version,
                FileCode = newItem.FileCode,
                FilePageNum=newItem.FilePageNum,
                GetFileDate=Funs.GetNewDateTime(newItem.GetFileDate),
                MainContent = System.Web.HttpUtility.HtmlEncode(newItem.MainContent),
                UnitIds = newItem.UnitIds,
                States = Const.State_2,
            };
            if (!string.IsNullOrEmpty(newItem.FileUnitId))
            {
                newReceiveFile.FileUnitId = newItem.FileUnitId;
            }
            if (!string.IsNullOrEmpty(newItem.SendPersonId))
            {
                newReceiveFile.SendPersonId = newItem.SendPersonId;
            }
            if (newItem.States != "1")
            {
                newReceiveFile.States = Const.State_0;
            }

            var updateFile = db.InformationProject_ReceiveFileManager.FirstOrDefault(x => x.ReceiveFileManagerId == newItem.ReceiveFileManagerId);
            if (updateFile == null)
            {
                newItem.ReceiveFileManagerId = newReceiveFile.ReceiveFileManagerId = SQLHelper.GetNewID();
                newReceiveFile.ReceiveFileCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ReceiveFileManagerMenuId, newReceiveFile.ProjectId, null);
                ReceiveFileManagerService.AddReceiveFileManager(newReceiveFile);
            }
            else
            {
                ReceiveFileManagerService.UpdateReceiveFileManager(newReceiveFile);
            }
            if (newItem.States == "1")
            {
                CommonService.btnSaveData(newItem.ProjectId, Const.ReceiveFileManagerMenuId, newReceiveFile.ReceiveFileManagerId, newReceiveFile.SendPersonId, true, newReceiveFile.ReceiveFileName, "../ReceiveFileManager/ReceiveFileManagerView.aspx?ReceiveFileManagerId={0}");
            }

            ////保存附件
            if (!string.IsNullOrEmpty(newItem.FileAttachUrl))
            {
                UploadFileService.SaveAttachUrl(UploadFileService.GetSourceByAttachUrl(newItem.FileAttachUrl, 10, null), newItem.FileAttachUrl, Const.ReceiveFileManagerMenuId, newItem.ReceiveFileManagerId);
            }
            else
            {
                CommonService.DeleteAttachFileById(Const.ReceiveFileManagerMenuId, newItem.ReceiveFileManagerId);
            }
        }
        #endregion

        #region 保存来文-回复文件
        /// <summary>
        /// 保存来文-回复文件
        /// </summary>
        /// <param name="receiveFileManagerId">主键</param>
        /// <param name="attachUrl">回执单路径</param>
        public static void SaveReplyFileAttachUrl(string receiveFileManagerId, string replyFileAttachUrl)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getFile = db.InformationProject_ReceiveFileManager.FirstOrDefault(x => x.ReceiveFileManagerId == receiveFileManagerId);
                if (getFile != null)
                {
                    ////保存附件
                    if (!string.IsNullOrEmpty(replyFileAttachUrl))
                    {
                        UploadFileService.SaveAttachUrl(UploadFileService.GetSourceByAttachUrl(replyFileAttachUrl, 10, null), replyFileAttachUrl, Const.ReceiveFileManagerMenuId, getFile.ReceiveFileManagerId + "#1");
                    }
                    else
                    {
                        CommonService.DeleteAttachFileById(Const.ReceiveFileManagerMenuId, getFile.ReceiveFileManagerId + "#1");
                    }
                }
            }
        }
        #endregion
    }
}
