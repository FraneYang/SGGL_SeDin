using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 项目图片
    /// </summary>
    public static class PictureService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取项目图片
        /// </summary>
        /// <param name="pictureId"></param>
        /// <returns></returns>
        public static Model.InformationProject_Picture GetPictureById(string pictureId)
        {
            return Funs.DB.InformationProject_Picture.FirstOrDefault(e => e.PictureId == pictureId);
        }

        /// <summary>
        /// 增加图片信息
        /// </summary>
        /// <param name="personQuality">图片实体</param>
        public static void AddPicture(Model.InformationProject_Picture picture)
        {
            Model.SGGLDB db = Funs.DB;
            Model.InformationProject_Picture newPicture = new Model.InformationProject_Picture
            {
                PictureId = picture.PictureId,
                ProjectId = picture.ProjectId,
                Title = picture.Title,
                ContentDef = picture.ContentDef,
                UploadDate = picture.UploadDate,
                PictureType = picture.PictureType,
                States = picture.States,
                AttachUrl = picture.AttachUrl,
                CompileMan = picture.CompileMan
            };
            db.InformationProject_Picture.InsertOnSubmit(newPicture);
            db.SubmitChanges();

            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectPictureMenuId, newPicture.ProjectId, null, newPicture.PictureId, newPicture.UploadDate);
        }

        /// <summary>
        /// 修改项目图片
        /// </summary>
        /// <param name="picture"></param>
        public static void UpdatePicture(Model.InformationProject_Picture picture)
        {
            Model.SGGLDB db = Funs.DB;
            Model.InformationProject_Picture newPicture = db.InformationProject_Picture.FirstOrDefault(e => e.PictureId == picture.PictureId);
            if (newPicture != null)
            {
                //newPicture.ProjectId = picture.ProjectId;
                newPicture.Title = picture.Title;
                newPicture.ContentDef = picture.ContentDef;
                newPicture.UploadDate = picture.UploadDate;
                newPicture.PictureType = picture.PictureType;
                newPicture.States = picture.States;
                newPicture.AttachUrl = picture.AttachUrl;
                newPicture.CompileMan = picture.CompileMan;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除项目图片
        /// </summary>
        /// <param name="pictureId"></param>
        public static void deletePictureById(string pictureId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.InformationProject_Picture picture = db.InformationProject_Picture.FirstOrDefault(e => e.PictureId == pictureId);
            if (picture != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(picture.PictureId);
                BLL.CommonService.DeleteAttachFileById(picture.PictureId);  ///删除附件
                BLL.UploadFileService.DeleteFile(Funs.RootPath, picture.AttachUrl);  ///删除附件
                //////删除审核流程
                BLL.CommonService.DeleteFlowOperateByID(picture.PictureId);
                db.InformationProject_Picture.DeleteOnSubmit(picture);
                db.SubmitChanges();
            }
        }
    }
}