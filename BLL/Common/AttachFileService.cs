using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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

        public static string getFileUrl(string toKeyId)
        {
            try
            {
                using (var db = new Model.SGGLDB(Funs.ConnString))
                {
                    string res = "";
                    var list = db.AttachFile.Where(p => p.ToKeyId == toKeyId).ToList();
                    if (list != null && list.Count > 0)
                    {
                        foreach (var item in list)
                        {
                            if (!string.IsNullOrEmpty(item.AttachUrl))
                                res += item.AttachUrl.ToLower().TrimEnd(',') + ",";
                        }
                        res = res.Substring(0, res.Length - 1);
                    }
                    return res.Replace('\\', '/');
                }
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public static Boolean Getfile(string toKeyId, string menuId)
        {
            bool res = false;
            var q = Funs.DB.AttachFile.FirstOrDefault((x => x.ToKeyId == toKeyId && x.MenuId == menuId));
            if (q != null)
            {
                var file = q.AttachUrl;
                if (!string.IsNullOrEmpty(file))
                {
                    res = true;
                }
            }
            return res;
        }

        public static IList<Model.AttachFile> Getfilelist(string toKeyId, string menuId)
        {
            List<string> listToKeyId = new List<string>();
            listToKeyId.Add(toKeyId);
            listToKeyId.Add(toKeyId + "r");
            listToKeyId.Add(toKeyId + "re");
            var list = Funs.DB.AttachFile.Where(p => listToKeyId.Contains(p.ToKeyId) && p.MenuId == menuId).ToList();
            return list;

        }

        public static bool updateAttachFile(string url, string toKeyId, string menuId)
        {
            if (!string.IsNullOrEmpty(url))
            {
                string fileDir = "WebApi";
                switch (menuId)
                {
                    case Const.CheckEquipmentMenuId:
                        fileDir = "CheckEquipment";
                        break;
                    case Const.CheckListMenuId:
                        fileDir = "CheckControl";
                        break;
                    case Const.JointCheckMenuId:
                        fileDir = "JointCheck";
                        break;
                    case Const.TechnicalContactListMenuId:
                        fileDir = "TechnicalContactList";
                        break;
                    case Const.WorkContactMenuId:
                        fileDir = "WorkContact";
                        break;
                    case Const.DesignMenuId:
                        fileDir = "Design";
                        break;
                    case Const.CQMSConstructSolutionMenuId:
                        fileDir = "Solution";
                        break;
                }
                using (var db = new Model.SGGLDB(Funs.ConnString))
                {
                    var query = from f in db.AttachFile
                                where f.ToKeyId == toKeyId
                                select f;
                    Model.AttachFile temp = query.FirstOrDefault();
                    if (temp == null)
                    {
                        temp = new Model.AttachFile();
                        temp.AttachFileId = BLL.SQLHelper.GetNewID(typeof(Model.AttachFile));
                        temp.ToKeyId = toKeyId;
                        temp.AttachUrl = reNameUrl(url, fileDir);
                        temp.AttachSource = getAttachSource(url, fileDir);
                        temp.MenuId = menuId;
                        db.AttachFile.InsertOnSubmit(temp);
                        db.SubmitChanges();
                    }
                    else
                    {
                        temp.AttachUrl = reNameUrl(url, fileDir);
                        temp.AttachSource = getAttachSource(url, fileDir);
                        db.SubmitChanges();
                    }
                    return true;
                }
            }
            else
            {
                using (var db = new Model.SGGLDB(Funs.ConnString))
                {
                    var query = from f in db.AttachFile
                                where f.ToKeyId == toKeyId
                                select f;
                    Model.AttachFile temp = query.FirstOrDefault();
                    if (temp != null)
                    {
                        db.AttachFile.DeleteOnSubmit(temp);
                        db.SubmitChanges();
                    }
                    return true;
                }
            }
            return false;
        }

        public static string reNameUrl(string url, string fileDir)
        {
            string[] urls = url.Split(',');
            string res = "";
            foreach (var item in urls)
            {
                string size = "0";
                int strInt = item.LastIndexOf("~");
                if (strInt < 0)
                {
                    strInt = item.LastIndexOf("\\");
                }
                if (strInt < 0)
                {
                    strInt = item.LastIndexOf("/");
                }
                res += "FileUpLoad/" + fileDir + "/" + item.Substring(strInt + 1) + ",";
            }
            if (!string.IsNullOrEmpty(res))
                return res.Substring(0, res.Length - 1);
            return res;
        }

        public static string getAttachSource(string url, string fileDir)
        {
            string res = "";
            string attachSource = string.Empty;
            if (!string.IsNullOrEmpty(url))
            {
                string[] urls = url.Split(',');
                foreach (var item in urls)
                {
                    string size = "0";
                    int strInt = item.LastIndexOf("~");
                    if (strInt < 0)
                    {
                        strInt = item.LastIndexOf("\\");
                    }
                    if (strInt < 0)
                    {
                        strInt = item.LastIndexOf("/");
                    }
                    string filepath = ConfigurationManager.AppSettings["localRoot"] + "/FileUpLoad/" + fileDir + "/" + item.Substring(strInt + 1);
                    strInt = filepath.LastIndexOf("~");
                    if (strInt < 0)
                    {
                        strInt = filepath.LastIndexOf("\\");
                    }
                    if (strInt < 0)
                    {
                        strInt = filepath.LastIndexOf("/");
                    }
                    if (File.Exists(ConfigurationManager.AppSettings["localRoot"] + "/" + item))
                    {
                        if (!Directory.Exists(filepath.Substring(0, strInt)))
                        {
                            Directory.CreateDirectory(filepath.Substring(0, strInt));
                        }
                        if (!File.Exists(filepath))
                        {
                            File.Copy(ConfigurationManager.AppSettings["localRoot"] + "/" + item, filepath);
                        }
                        FileInfo fileInfo = new System.IO.FileInfo(filepath);
                        size = System.Math.Ceiling(fileInfo.Length / 1024.0) + "";
                    }

                    string name = filepath.Substring(strInt + 1);
                    string type = filepath.Substring(filepath.LastIndexOf(".") + 1);


                    string id = SQLHelper.GetNewID(typeof(Model.AttachFile));
                    attachSource += "{    \"name\": \"" + name + "\",    \"type\": \"" + type + "\",    \"savedName\": \"" + name
                        + "\",    \"size\": " + size + ",    \"id\": \"" + SQLHelper.GetNewID(typeof(Model.AttachFile)) + "\"  },";
                }
            }
            if (!string.IsNullOrEmpty(attachSource))
            {
                attachSource = attachSource.Substring(0, attachSource.Length - 1);
            }
            return "[" + attachSource + "]";
        }

        /// <summary>
        /// 根据id获取url地址
        /// </summary>
        /// <param name="toKeyId"></param>
        /// <returns></returns>
        public static string GetfileUrl(string toKeyId)
        {
            try
            {
                string file = string.Empty;
                var result = Funs.DB.AttachFile.FirstOrDefault((x => x.ToKeyId == toKeyId && x.MenuId == Const.UserMenuId));
                if (result != null && !string.IsNullOrEmpty(result.AttachUrl))
                {
                    file = result.AttachUrl.ToLower();
                    file = file.Replace('\\', '/').TrimEnd(',');
                }
                return file;
            }
            catch (Exception e)
            {
                return "";

            }
        }

        public static Model.AttachFile Getfiles(string toKeyId, string menuId)
        {
            var list = Funs.DB.AttachFile.FirstOrDefault((x => x.ToKeyId == toKeyId && x.MenuId == menuId));
            return list;
        }

        public static IList<Model.AttachFile> GetfileDetaillist(List<string> toKeyId, string menuId)
        {
            if (toKeyId.Count > 0)
            {
                var list = Funs.DB.AttachFile.Where(p => toKeyId.Contains(p.ToKeyId) && p.MenuId == menuId).ToList();
                return list;
            }
            else
            {
                return null;
            }
        }
    }
}
