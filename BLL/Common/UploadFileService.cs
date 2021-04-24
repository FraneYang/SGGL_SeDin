using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 上传附件相关
    /// </summary>
    public class UploadFileService
    {
        #region 附件上传
        /// <summary>
        /// 附件上传
        /// </summary>
        /// <param name="fileUpload">上传控件</param>
        /// <param name="fileUrl">上传路径</param>
        /// <param name="constUrl">定义路径</param>
        /// <returns></returns>
        public static string UploadAttachment(string rootPath, FineUIPro.FileUpload fileUpload, string fileUrl, string constUrl)
        {
            if (!string.IsNullOrEmpty(fileUrl))  ////是否存在附件 存在则删除
            {
                string urlFullPath = rootPath + fileUrl;
                if (File.Exists(urlFullPath))
                {
                    File.Delete(urlFullPath);
                }
            }

            string initFullPath = rootPath + constUrl;
            if (!Directory.Exists(initFullPath))
            {
                Directory.CreateDirectory(initFullPath);
            }

            string filePath = fileUpload.PostedFile.FileName;
            string fileName = Funs.GetNewFileName() + "~" + Path.GetFileName(filePath);
            int count = fileUpload.PostedFile.ContentLength;
            string savePath = constUrl + fileName;
            string fullPath = initFullPath + fileName;
            if (!File.Exists(fullPath))
            {
                byte[] buffer = new byte[count];
                Stream stream = fileUpload.PostedFile.InputStream;

                stream.Read(buffer, 0, count);
                MemoryStream memoryStream = new MemoryStream(buffer);
                FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                memoryStream.WriteTo(fs);
                memoryStream.Flush();
                memoryStream.Close();
                fs.Flush();
                fs.Close();
                memoryStream = null;
                fs = null;
                //if (!string.IsNullOrEmpty(fileUrl))
                //{
                //    fileUrl += "," + savePath;
                //}
                //else
                //{
                //    fileUrl += savePath;
                //}
                fileUrl = savePath;
            }
            else
            {
                fileUrl = string.Empty;
            }

            return fileUrl;
        }
        #endregion

        #region 附件资源删除
        /// <summary>
        /// 附件资源删除
        /// </summary>
        /// <param name="rootPath"></param>
        /// <param name="fileUrl"></param>
        public static void DeleteFile(string rootPath, string fileUrl)
        {
            if (!string.IsNullOrEmpty(fileUrl))
            {
                string[] strs = fileUrl.Trim().Split(',');
                foreach (var item in strs)
                {
                    string urlFullPath = rootPath + item;
                    if (File.Exists(urlFullPath))
                    {
                        File.Delete(urlFullPath);
                    }
                }
            }
        }
        #endregion

        #region 上传文件路径
        /// <summary>
        /// 用户-附件路径
        /// </summary>
        public static string UserFilePath = "FileUpload\\User\\";
        /// <summary>
        /// 安全组织体系
        /// </summary>
        public const string HSSEOrganizeFilePath = "FileUpload\\HSSEOrganize\\";
        /// <summary>
        /// 安全管理机构-附件路径
        /// </summary>
        public const string HSSEManageFilePath = "FileUpload\\HSSEManage\\";
        /// <summary>
        /// 标准规范-附件路径
        /// </summary>
        public static string HSSEStandardsListFilePath = "FileUpload\\HSSEStandardsList\\";
        /// <summary>
        ///法律法规-附件路径
        /// </summary>
        public static string LawRegulationFilePath = "FileUpload\\LawRegulation\\";
        /// <summary>
        /// 管理规定-附件路径
        /// </summary>
        public static string ManageRuleFilePath = "FileUpload\\ManageRule\\";
        /// <summary>
        /// 生产管理规章制度-附件路径
        /// </summary>
        public static string RulesRegulationsFilePath = "FileUpload\\RulesRegulations\\";
        /// <summary>
        /// 二维码上传路径
        /// </summary>
        public const string QRCodeImageFilePath = "FileUpload\\QRCodeFile\\";
        /// <summary>
        /// 培训教材库-附件路径
        /// </summary>
        public static string TrainingFilePath = "FileUpload\\Training\\";
        /// <summary>
        /// 事故案例库-附件路径
        /// </summary>
        public static string AccidentCaseFilePath = "FileUpload\\AccidentCase\\";
        /// <summary>
        /// HAZOP-附件路径
        /// </summary>
        public static string HAZOPFilePath = "FileUpload\\HAZOP\\";
        /// <summary>
        /// 安全试题库-附件路径
        /// </summary>
        public static string TrainTestDBFilePath = "FileUpload\\TrainTestDB\\";
        /// <summary>
        /// 安全专家-附件路径
        /// </summary>
        public static string ExpertFilePath = "FileUpload\\Expert\\";
        /// <summary>
        /// 应急预案-附件路径
        /// </summary>
        public const string EmergencyFilePath = "FileUpload\\Emergency\\";
        /// <summary>
        /// 安全评价
        /// </summary>
        public const string AppraiseFilePath = "FileUpload\\Appraise\\";
        /// <summary>
        /// 专项方案-附件路径
        /// </summary>
        public const string SpecialSchemeFilePath = "FileUpload\\SpecialScheme\\";
        /// <summary>
        /// 安全监督检查报告-附件路径
        /// </summary>
        public static string SuperviseCheckReportFilePath = "FileUpload\\SuperviseCheckReport\\";
        /// <summary>
        /// 安全生产快报-附件路径
        /// </summary>
        public static string SafeProductionExpressFilePath = "FileUpload\\SafeProductionExpress\\";
        /// <summary>
        /// 安全交流-附件路径
        /// </summary>
        public static string ExchangeFilePath = "FileUpload\\Exchange\\";
        /// <summary>
        /// 安全生产数据季报-附件路径
        /// </summary>
        public static string SafetyQuarterlyReportFilePath = "FileUpload\\SafetyQuarterlyReport\\";
        /// <summary>
        /// 在线督查
        /// </summary>
        public const string OnLineSupervisionFilePath = "FileUpload\\OnLineSupervision\\";
        /// <summary>
        /// 项目评价
        /// </summary>
        public const string ProjectEvaluationFilePath = "FileUpload\\ProjectEvaluation\\";
        /// <summary>
        /// 事故快报
        /// </summary>
        public const string ProjectAccidentFilePath = "FileUpload\\ProjectAccident\\";
        /// <summary>
        /// 评价报告
        /// </summary>
        public const string SubUnitCheckRectifyFilePath = "FileUpload\\SubUnitCheckRectify\\";
        /// <summary>
        /// 企业安全文件上报
        /// </summary>
        public const string SubUnitReportFilePath = "FileUpload\\SubUnitReport\\";
        /// <summary>
        /// 隐患整改通知单附件路径
        /// </summary>
        public const string RectifyNoticeFilePath = "FileUpload\\RectifyNotice\\";
        /// <summary>
        /// 工程暂停令附件路径
        /// </summary>
        public const string PauseNoticeFilePath = "FileUpload\\PauseNotice\\";
        /// <summary>
        /// 危险观察登记-附件路径
        /// </summary>
        public static string RegistrationFilePath = "FileUpload\\Registration\\";
        /// <summary>
        /// 分包方绩效评价-附件路径
        /// </summary>
        public static string PerfomanceRecordFilePath = "FileUpload\\PerfomanceRecord\\";
        /// <summary>
        /// 个人绩效评价-附件路径
        /// </summary>
        public static string PersonPerfomanceFilePath = "FileUpload\\PersonPerfomance\\";
        /// <summary>
        /// 奖励通知单-附件路径
        /// </summary>
        public const string IncentiveNoticeFilePath = "FileUpload\\IncentiveNotice\\";
        /// <summary>
        /// 处罚通知单-附件路径
        /// </summary>
        public const string PunishNoticeFilePath = "FileUpload\\PunishNotice\\";
        /// <summary>
        /// 获奖证书或奖杯--附件路径
        /// </summary>
        public const string HSECertificateFilePath = "FileUpload\\HSECertificate\\";
        /// <summary>
        /// 安全专职人员名单附件
        /// </summary>
        public const string FullTimeManFilePath = "FileUpload\\FullTimeMan\\";
        /// <summary>
        /// 项目经理人员名单附件
        /// </summary>
        public const string PMManFilePath = "FileUpload\\PMMan\\";

        #region 分包商资质
        /// <summary>
        /// 营业执照扫描件-附件路径
        /// </summary>
        public const string BL_ScanUrlFilePath = "FileUpload\\BL_ScanUrl\\";
        /// <summary>
        /// 机构代码扫描件-附件路径
        /// </summary>
        public const string O_ScanUrlFilePath = "FileUpload\\O_ScanUrl\\";
        /// <summary>
        /// 资质证书扫描件-附件路径
        /// </summary>
        public const string C_ScanUrlFilePath = "FileUpload\\C_ScanUrl\\";
        /// <summary>
        /// 质量体系认证证书扫描件-附件路径
        /// </summary>
        public const string QL_ScanUrlFilePath = "FileUpload\\QL_ScanUrl\\";
        /// <summary>
        /// HSE体系认证证书扫描件-附件路径
        /// </summary>
        public const string H_ScanUrlFilePath = "FileUpload\\H_ScanUrl\\";
        /// <summary>
        /// HSE体系认证证书扫描件-附件路径
        /// </summary>
        public const string H_ScanUrl2FilePath = "FileUpload\\H_ScanUrl2\\";
        /// <summary>
        /// 安全生产许可证扫描件-附件路径
        /// </summary>
        public const string SL_ScanUrlFilePath = "FileUpload\\SL_ScanUrl\\";
        #endregion

        #region 采购供货厂家管理
        /// <summary>
        // 培训记录-附件路径
        /// </summary>
        public const string TrainRecordsUrlFilePath = "FileUpload\\TrainRecordsUrl\\";
        /// <summary>
        /// 方案及资质审查-附件路径
        /// </summary>
        public const string PlanUrlFilePath = "FileUpload\\PlanUrl\\";
        /// <summary>
        /// 临时到场人员培训-附件路径
        /// </summary>
        public const string TemporaryPersonUrlFilePath = "FileUpload\\TemporaryPersonUrl\\";
        /// <summary>
        /// 厂家入场安全人员培训-附件路径
        /// </summary>
        public const string InPersonTrainUrlFilePath = "FileUpload\\InPersonTrainUrl\\";
        /// <summary>
        /// HSE协议-附件路径
        /// </summary>
        public const string HSEAgreementUrlFilePath = "FileUpload\\HSEAgreementUrl\\";
        #endregion
        #endregion

        /// <summary>
        /// 保存附件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="attachUrl"></param>
        /// <param name="menuId"></param>
        /// <param name="toKeyId"></param>
        public static void SaveAttachUrl(string source, string attachUrl,string menuId,string toKeyId)
        {
            string rootUrl = ConfigurationManager.AppSettings["localRoot"];
            if (string.IsNullOrEmpty(rootUrl))
            {
                rootUrl = Funs.RootPath;
            }
            List<Model.AttachFile> sour = (from x in Funs.DB.AttachFile
                                           where x.MenuId == menuId &&  x.ToKeyId == toKeyId
                                           select x).ToList();
            if (sour.Count() == 0)
            {
                Model.AttachFile att = new Model.AttachFile
                {
                    AttachFileId = SQLHelper.GetNewID(),
                    ToKeyId = toKeyId,
                    AttachSource = source.ToString(),
                    AttachUrl = attachUrl,
                    MenuId = menuId,
                    //ImageByte= imageByte,
                    //AttachPath= attachPath,
                };

                if (menuId == Const.PersonListMenuId)
                {
                    att.ImageByte = AttachFileService.SetImageToByteArray(rootUrl + attachUrl.Split(',')[0]);
                }
                Funs.DB.AttachFile.InsertOnSubmit(att);
                Funs.DB.SubmitChanges();
            }
            else
            {
                Model.AttachFile att = Funs.DB.AttachFile.FirstOrDefault(x => x.MenuId == menuId && x.AttachFileId == sour.First().AttachFileId);
                if (att != null)
                {
                    att.ToKeyId = toKeyId;
                    att.AttachSource = source.ToString();
                    att.AttachUrl = attachUrl;
                    att.MenuId = menuId;
                    if (menuId == Const.PersonListMenuId)
                    {
                        att.ImageByte = AttachFileService.SetImageToByteArray(rootUrl + attachUrl.Split(',')[0]);
                    }
                    Funs.DB.SubmitChanges();
                }
            }
        }

        /// <summary>
        /// 通过附件路径得到Source
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetSourceByAttachUrl(string attachUrl, int size, string oldSrouce)
        {
            string attachSource = string.Empty;
            if (!string.IsNullOrEmpty(attachUrl))
            {
                attachUrl= attachUrl.Replace('/', '\\');
                var allUrl = Funs.GetStrListByStr(attachUrl, ',');
                foreach (var item in allUrl)
                {
                    int strInt = item.LastIndexOf("~");
                    if (strInt < 0)
                    {
                        strInt = item.LastIndexOf("\\");
                    }
                    string folder = item.Substring(0,strInt + 1).Replace('\\', '/');
                    string name = item.Substring(strInt + 1);
                    string type = item.Substring(item.LastIndexOf(".") + 1);
                    string savedName = item.Substring(item.LastIndexOf("\\") + 1);

                    string id = SQLHelper.GetNewID(typeof(Model.AttachFile));
                    attachSource += "{    \"name\": \"" + name + "\",  \"folder\": \"" + folder + "\",    \"type\": \"" + type + "\",    \"savedName\": \"" + savedName
                        + "\",    \"size\": " + size + ",    \"id\": \"" + SQLHelper.GetNewID(typeof(Model.AttachFile)) + "\"  }@";
                }
                attachSource = attachSource.Substring(0, attachSource.LastIndexOf("@")).Replace("@", ",");

                if (!string.IsNullOrEmpty(oldSrouce))
                {
                    attachSource = oldSrouce.Replace("]", ",") + attachSource + "]";
                }
                else
                {
                    attachSource = "[" + attachSource + "]";
                }
            }
            return attachSource;
        }

        ////将虚拟路径转化为文件的路径然后最后转化为文件流

        //public static ActionResult SaveImage(string path)
        //{
        //    var url = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + path;

        //    FileStream fs = new FileStream(url, FileMode.Open, FileAccess.Read); //将图片以文件流的形式进行保存
        //    BinaryReader br = new BinaryReader(fs);
        //    byte[] imgBytesIn = br.ReadBytes((int)fs.Length); //将流读入到字节数组中
        //    Encoding myEncoding = Encoding.GetEncoding("utf-8");
        //    string stImageByte = Convert.ToBase64String(imgBytesIn);

        //    return Json(stImageByte);
        //}
    }
}