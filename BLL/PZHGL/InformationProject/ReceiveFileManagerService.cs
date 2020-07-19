using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 一般来文
    /// </summary>
    public static class ReceiveFileManagerService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取一般来文
        /// </summary>
        /// <param name="ReceiveFileManagerId"></param>
        /// <returns></returns>
        public static Model.InformationProject_ReceiveFileManager GetReceiveFileManagerById(string ReceiveFileManagerId)
        {
            return Funs.DB.InformationProject_ReceiveFileManager.FirstOrDefault(e => e.ReceiveFileManagerId == ReceiveFileManagerId);
        }

        /// <summary>
        /// 添加一般来文
        /// </summary>
        /// <param name="ReceiveFileManager"></param>
        public static void AddReceiveFileManager(Model.InformationProject_ReceiveFileManager ReceiveFileManager)
        {
            Model.SGGLDB db = Funs.DB;
            Model.InformationProject_ReceiveFileManager newReceiveFileManager = new Model.InformationProject_ReceiveFileManager
            {
                ReceiveFileManagerId = ReceiveFileManager.ReceiveFileManagerId,
                ProjectId = ReceiveFileManager.ProjectId,
                ReceiveFileCode = ReceiveFileManager.ReceiveFileCode,
                ReceiveFileName = ReceiveFileManager.ReceiveFileName,
                Version = ReceiveFileManager.Version,
                FileUnitId = ReceiveFileManager.FileUnitId,
                FileCode = ReceiveFileManager.FileCode,
                FilePageNum = ReceiveFileManager.FilePageNum,
                GetFileDate = ReceiveFileManager.GetFileDate,
                SendPersonId = ReceiveFileManager.SendPersonId,
                MainContent = ReceiveFileManager.MainContent,
                AttachUrl = ReceiveFileManager.AttachUrl,
                States = ReceiveFileManager.States,
                UnitIds = ReceiveFileManager.UnitIds,
                FileType=ReceiveFileManager.FileType,
                FromId=ReceiveFileManager.FromId,
                FromType=ReceiveFileManager.FromType,
            };
            db.InformationProject_ReceiveFileManager.InsertOnSubmit(newReceiveFileManager);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ReceiveFileManagerMenuId, ReceiveFileManager.ProjectId, null, ReceiveFileManager.ReceiveFileManagerId, ReceiveFileManager.GetFileDate);
        }

        /// <summary>
        /// 修改一般来文
        /// </summary>
        /// <param name="ReceiveFileManager"></param>
        public static void UpdateReceiveFileManager(Model.InformationProject_ReceiveFileManager ReceiveFileManager)
        {
            Model.SGGLDB db = Funs.DB;
            Model.InformationProject_ReceiveFileManager newReceiveFileManager = db.InformationProject_ReceiveFileManager.FirstOrDefault(e => e.ReceiveFileManagerId == ReceiveFileManager.ReceiveFileManagerId);
            if (newReceiveFileManager != null)
            {
               // newReceiveFileManager.ReceiveFileCode = ReceiveFileManager.ReceiveFileCode;
                newReceiveFileManager.ReceiveFileName = ReceiveFileManager.ReceiveFileName;
                newReceiveFileManager.Version = ReceiveFileManager.Version;
                newReceiveFileManager.FileUnitId = ReceiveFileManager.FileUnitId;
                newReceiveFileManager.FileCode = ReceiveFileManager.FileCode;
                newReceiveFileManager.FilePageNum = ReceiveFileManager.FilePageNum;
                newReceiveFileManager.GetFileDate = ReceiveFileManager.GetFileDate;
                newReceiveFileManager.SendPersonId = ReceiveFileManager.SendPersonId;
                newReceiveFileManager.MainContent = ReceiveFileManager.MainContent;
                newReceiveFileManager.AttachUrl = ReceiveFileManager.AttachUrl;
                newReceiveFileManager.States = ReceiveFileManager.States;
                newReceiveFileManager.UnitIds = ReceiveFileManager.UnitIds;
                newReceiveFileManager.FileType = ReceiveFileManager.FileType;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除一般来文
        /// </summary>
        /// <param name="ReceiveFileManagerId"></param>
        public static void DeleteReceiveFileManagerById(string ReceiveFileManagerId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.InformationProject_ReceiveFileManager ReceiveFileManager = db.InformationProject_ReceiveFileManager.FirstOrDefault(e => e.ReceiveFileManagerId == ReceiveFileManagerId);
            if (ReceiveFileManager != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(ReceiveFileManager.ReceiveFileManagerId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(ReceiveFileManager.ReceiveFileManagerId);
                ////删除流程表
                BLL.CommonService.DeleteFlowOperateByID(ReceiveFileManager.ReceiveFileManagerId);
                db.InformationProject_ReceiveFileManager.DeleteOnSubmit(ReceiveFileManager);
                db.SubmitChanges();
            }
        }

        /// <summary>
        ///  根据通知 公司通知生成公司来文 项目通知生成项目来文
        /// </summary>
        public static void CreateReceiveFile(Model.InformationProject_Notice notice)
        {
            var getProjects = Funs.GetStrListByStr(notice.AccessProjectId, ',');
            string unitId =Const.UnitId_SEDIN;
            var getAtt = Funs.DB.AttachFile.FirstOrDefault(x => x.ToKeyId == notice.NoticeId);
            foreach (var item in getProjects)
            {
                Model.InformationProject_ReceiveFileManager newFile = new Model.InformationProject_ReceiveFileManager
                {
                    ReceiveFileManagerId = SQLHelper.GetNewID(),
                    ProjectId = item,
                    ReceiveFileCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ReceiveFileManagerMenuId, item, unitId),
                    ReceiveFileName = notice.NoticeTitle,
                    Version = "V1.0",
                    FileUnitId = unitId,
                    FileCode = notice.NoticeCode,
                    FilePageNum = 1,
                    GetFileDate = DateTime.Now,
                    SendPersonId = notice.CompileMan,
                    MainContent = notice.MainContent,
                    FromId=notice.NoticeId,
                    FromType ="1",
                };
                if (!string.IsNullOrEmpty(notice.ProjectId))
                {
                    newFile.FileType = "0";
                    var getPUnits = Funs.DB.Project_ProjectUnit.Where(x => x.ProjectId == item);
                    foreach (var uItem in getPUnits)
                    {
                        if (string.IsNullOrEmpty(newFile.UnitIds))
                        {
                            newFile.UnitIds = uItem.UnitId;
                        }
                        else
                        {
                            newFile.UnitIds += "," + uItem.UnitId;
                        }
                    }
                }
                else
                {
                    newFile.FileType = "1";
                    newFile.UnitIds = unitId;
                }
               
                newFile.States = Const.State_2;
                ReceiveFileManagerService.AddReceiveFileManager(newFile);
                if (getAtt != null && !string.IsNullOrEmpty(getAtt.AttachUrl))
                {
                    APIUpLoadFileService.SaveAttachUrl(Const.ReceiveFileManagerMenuId, newFile.ReceiveFileManagerId, getAtt.AttachUrl, "0");
                }
                CommonService.btnSaveData(item, Const.ReceiveFileManagerMenuId, newFile.ReceiveFileManagerId, newFile.SendPersonId, true, newFile.ReceiveFileName, "../InformationProject/ReceiveFileManagerView.aspx?ReceiveFileManagerId={0}");
            }
        }

        /// <summary>
        ///  根据通知 公司通知生成公司来文 项目通知生成项目来文
        /// </summary>
        public static void IssueReceiveFile(string receiveFileManagerId)
        {
            var getFile = Funs.DB.InformationProject_ReceiveFileManager.FirstOrDefault(x => x.ReceiveFileManagerId == receiveFileManagerId);
            if (getFile != null && getFile.FileType == "1")
            {
                var getPUnits = Funs.DB.Project_ProjectUnit.Where(x => x.ProjectId == getFile.ProjectId);
                foreach (var uItem in getPUnits)
                {
                    if (string.IsNullOrEmpty(getFile.UnitIds))
                    {
                        getFile.UnitIds = uItem.UnitId;
                    }
                    else
                    {
                        getFile.UnitIds += "," + uItem.UnitId;
                    }
                }

                Model.InformationProject_ReceiveFileManager newReceiveFileManager = new Model.InformationProject_ReceiveFileManager
                {
                    ReceiveFileManagerId = SQLHelper.GetNewID(),
                ProjectId = getFile.ProjectId,
                    ReceiveFileName = getFile.ReceiveFileName,
                    Version = getFile.Version,
                    FileUnitId = getFile.FileUnitId,
                    FileCode = getFile.FileCode,
                    FilePageNum = getFile.FilePageNum,
                    GetFileDate = getFile.GetFileDate,
                    SendPersonId = getFile.SendPersonId,
                    MainContent = getFile.MainContent,
                    AttachUrl = getFile.AttachUrl,
                    States = getFile.States,
                    UnitIds = getFile.UnitIds,
                };

                newReceiveFileManager.ReceiveFileManagerId = SQLHelper.GetNewID();
                newReceiveFileManager.ReceiveFileCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ReceiveFileManagerMenuId, newReceiveFileManager.ProjectId, newReceiveFileManager.FileUnitId);
                newReceiveFileManager.FileType = "0";
                newReceiveFileManager.FromId = getFile.ReceiveFileManagerId;
                newReceiveFileManager.FromType = "2";

                db.InformationProject_ReceiveFileManager.InsertOnSubmit(newReceiveFileManager);
                db.SubmitChanges();
                ////增加一条编码记录
                BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ReceiveFileManagerMenuId, newReceiveFileManager.ProjectId, null, newReceiveFileManager.ReceiveFileManagerId, newReceiveFileManager.GetFileDate);

                var getAtt = Funs.DB.AttachFile.FirstOrDefault(x => x.ToKeyId == getFile.ReceiveFileManagerId);
                if (getAtt != null && !string.IsNullOrEmpty(getAtt.AttachUrl))
                {
                    APIUpLoadFileService.SaveAttachUrl(Const.ReceiveFileManagerMenuId, newReceiveFileManager.ReceiveFileManagerId, getAtt.AttachUrl, "0");
                }
                CommonService.btnSaveData(getFile.ProjectId, Const.ReceiveFileManagerMenuId, newReceiveFileManager.ReceiveFileManagerId, newReceiveFileManager.SendPersonId, true, newReceiveFileManager.ReceiveFileName, "../InformationProject/ReceiveFileManagerView.aspx?ReceiveFileManagerId={0}");
            }
        }
    }
}
