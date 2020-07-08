namespace BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class SignManageService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 获取标牌信息
        /// </summary>
        /// <param name="SignManageId">标牌Id</param>
        /// <returns></returns>
        public static Model.Resources_SignManage GetSignManageBySignManageId(string SignManageId)
        {
            return new Model.SGGLDB(Funs.ConnString).Resources_SignManage.FirstOrDefault(x => x.SignManageId == SignManageId);
        }

        /// <summary>
        /// 增加标牌
        /// </summary>
        /// <param name="SignManage"></param>
        public static void AddSignManage(Model.Resources_SignManage SignManage)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Resources_SignManage newSignManage = new Model.Resources_SignManage
            {
                SignManageId = SignManage.SignManageId,
                SignCode = SignManage.SignCode,
                SignName = SignManage.SignName,
                SignLen = SignManage.SignLen,
                SignWide = SignManage.SignWide,
                SignHigh = SignManage.SignHigh,
                SignThick = SignManage.SignThick,
                Material = SignManage.Material,
                SignArea = SignManage.SignArea,
                SignType = SignManage.SignType,
                SignImage = SignManage.SignImage,
                SignUrl = SignManage.SignUrl
            };
            db.Resources_SignManage.InsertOnSubmit(newSignManage);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改标牌信息
        /// </summary>
        /// <param name="SignManageId"></param>
        /// <param name="SignName"></param>
        /// <param name="def"></param>
        public static void UpdateSignManage(Model.Resources_SignManage SignManage)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Resources_SignManage updateSignManage = db.Resources_SignManage.FirstOrDefault(e => e.SignManageId == SignManage.SignManageId);
            if (updateSignManage != null)
            {
                updateSignManage.SignCode = SignManage.SignCode;
                updateSignManage.SignName = SignManage.SignName;
                updateSignManage.SignLen = SignManage.SignLen;
                updateSignManage.SignWide = SignManage.SignWide;
                updateSignManage.SignHigh = SignManage.SignHigh;
                updateSignManage.SignThick = SignManage.SignThick;
                updateSignManage.Material = SignManage.Material;
                updateSignManage.SignArea = SignManage.SignArea;
                updateSignManage.SignType = SignManage.SignType;
                updateSignManage.SignImage = SignManage.SignImage;
                updateSignManage.SignUrl = SignManage.SignUrl;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 删除标牌
        /// </summary>
        /// <param name="SignManageId"></param>
        public static void DeleteSignManage(string signManageId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Resources_SignManage deleteSignManage = db.Resources_SignManage.FirstOrDefault(e => e.SignManageId == signManageId);
            if (deleteSignManage != null)
            {
                if (!string.IsNullOrEmpty(deleteSignManage.SignUrl))
                {
                    BLL.UploadFileService.DeleteFile(Funs.RootPath, deleteSignManage.SignUrl);
                }
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(deleteSignManage.SignManageId);
                db.Resources_SignManage.DeleteOnSubmit(deleteSignManage);
                db.SubmitChanges();
            }
        }
    }
}
