using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class AttachFileService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 添加附件存储信息
        /// </summary>
        /// <param name="workArea"></param>
        public static void AddAttachFile(Model.AttachFile attachFile)
        {
            string newKeyID = SQLHelper.GetNewID(typeof(Model.AttachFile));
            Model.AttachFile newAttachFile = new Model.AttachFile();
            newAttachFile.AttachFileId = newKeyID;
            newAttachFile.ToKeyId = attachFile.ToKeyId;
            newAttachFile.AttachSource = attachFile.AttachSource;
            newAttachFile.AttachUrl = attachFile.AttachUrl;
            newAttachFile.MenuId = attachFile.MenuId;

            db.AttachFile.InsertOnSubmit(newAttachFile);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改附件存储信息
        /// </summary>
        /// <param name="workArea"></param>
        public static void updateAttachFile(Model.AttachFile attachFile)
        {
            Model.AttachFile newAttachFile = db.AttachFile.FirstOrDefault(x => x.AttachFileId == attachFile.AttachFileId);
            newAttachFile.ToKeyId = attachFile.ToKeyId;
            newAttachFile.AttachSource = attachFile.AttachSource;
            newAttachFile.AttachUrl = attachFile.AttachUrl;
            newAttachFile.MenuId = attachFile.MenuId;
            db.SubmitChanges();

        }

        /// <summary>
        /// 根据对应Id删除附件信息及文件存放的物理位置
        /// </summary>
        /// <param name="workAreaId"></param>
        public static void DeleteAttachFile(string rootPath, string toKeyId, string menuId)
        {
            Model.AttachFile att = db.AttachFile.FirstOrDefault(e => e.ToKeyId == toKeyId && e.MenuId == menuId);
            if (att != null)
            {
                BLL.UploadFileService.DeleteFile(rootPath, att.AttachUrl);
                db.AttachFile.DeleteOnSubmit(att);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据对应主键和菜单获取文件信息
        /// </summary>
        /// <param name="toKey">对应主键</param>
        /// <param name="menuId">对应菜单</param>
        /// <returns>文件信息</returns>
        public static Model.AttachFile GetAttachFile(string toKey, string menuId)
        {
            return Funs.DB.AttachFile.FirstOrDefault(e => e.ToKeyId == toKey && e.MenuId == menuId);
        }
    }
}
