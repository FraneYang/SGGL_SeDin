using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Http;
using System.Web.UI.WebControls;
using Aspose.Words;

namespace Mvc.Controllers
{
    public class TechnicalContactController : ApiController
    {
        [HttpGet]
        public ResponseData<List<Check_TechnicalContactList>> Index(string projectId, int index, int page, string name = "")
        {
            ResponseData<List<Check_TechnicalContactList>> res = new ResponseData<List<Check_TechnicalContactList>>();

            res.successful = true;
            res.resultValue = BLL.TechnicalContactListService.getListDataForApi(name, projectId, index, page);
            return res;
        }


        [HttpGet]
        public ResponseData<List<Check_TechnicalContactList>> Search(string projectId, int index, int page, string proposedUnitId = "", string unitWorkId = "", string mainSendUnit = "", string cCUnitIds = "", string professional = "", string state = "", string contactListType = "", string isReply = "", string dateA = "", string dateZ = "")
        {
            ResponseData<List<Check_TechnicalContactList>> res = new ResponseData<List<Check_TechnicalContactList>>();

            res.successful = true;
            res.resultValue = BLL.TechnicalContactListService.getListDataForApi(state, contactListType, isReply, dateA, dateZ, proposedUnitId, unitWorkId, mainSendUnit, cCUnitIds, professional, projectId, index, page);
            return res;
        }
        /// <summary>
        /// 根据id获取详情
        /// </summary>
        /// <param name="CheckControlCode"></param>
        /// <returns></returns>
        public ResponseData<Check_TechnicalContactList> GetContactById(string id)
        {
            ResponseData<Check_TechnicalContactList> res = new ResponseData<Check_TechnicalContactList>();
            Check_TechnicalContactList technicalContactList = BLL.TechnicalContactListService.GetTechnicalContactListByTechnicalContactListIdForApi(id);

            res.successful = true;
            res.resultValue = BeanUtil.CopyOjbect<Check_TechnicalContactList>(technicalContactList, true);
            return res;
        }
        /// <summary>
        /// 根据code获取 审核记录
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ResponseData<List<Check_TechnicalContactListApprove>> GetApproveById(string id)
        {
            ResponseData<List<Check_TechnicalContactListApprove>> res = new ResponseData<List<Check_TechnicalContactListApprove>>();

            res.successful = true;
            res.resultValue = BLL.TechnicalContactListApproveService.GetListDataByIdForApi(id);
            return res;
        }


        public ResponseData<Check_TechnicalContactListApprove> GetCurrApproveById(string id)
        {
            ResponseData<Check_TechnicalContactListApprove> res = new ResponseData<Check_TechnicalContactListApprove>();

            res.successful = true;
            res.resultValue = BeanUtil.CopyOjbect<Check_TechnicalContactListApprove>(BLL.TechnicalContactListApproveService.getCurrApproveForApi(id), true);
            return res;
        }


        [HttpPost]
        public ResponseData<string> AddTechnicalContact([FromBody]Model.Check_TechnicalContactList CheckControl)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {
                if (string.IsNullOrEmpty(CheckControl.TechnicalContactListId))
                {
                    CheckControl.TechnicalContactListId = Guid.NewGuid().ToString();
                    CheckControl.CompileDate = DateTime.Now;
                    BLL.TechnicalContactListService.AddTechnicalContactListForApi(CheckControl);
                    //BLL.AttachFileService.updateAttachFile(CheckControl.ReturnAttachUrl, CheckControl.TechnicalContactListId + "r",BLL. Const.TechnicalContactListMenuId);
                    //BLL.AttachFileService.updateAttachFile(CheckControl.AttachUrl, CheckControl.TechnicalContactListId, BLL.Const.TechnicalContactListMenuId);
                    SaveAttachFile(CheckControl.TechnicalContactListId + "re", BLL.Const.TechnicalContactListMenuId, CheckControl.ReturnAttachUrl);
                    SaveAttachFile(CheckControl.TechnicalContactListId + "r", BLL.Const.TechnicalContactListMenuId, CheckControl.ReAttachUrl);
                    SaveAttachFile(CheckControl.TechnicalContactListId, BLL.Const.TechnicalContactListMenuId, CheckControl.AttachUrl);
                    res.resultValue = CheckControl.TechnicalContactListId;
                }
                else
                {
                    BLL.TechnicalContactListService.UpdateTechnicalContactListForApi(CheckControl);
                    //BLL.AttachFileService.updateAttachFile(CheckControl.ReturnAttachUrl, CheckControl.TechnicalContactListId + "r", BLL.Const.TechnicalContactListMenuId);
                    //BLL.AttachFileService.updateAttachFile(CheckControl.AttachUrl, CheckControl.TechnicalContactListId, BLL.Const.TechnicalContactListMenuId);
                    SaveAttachFile(CheckControl.TechnicalContactListId + "re", BLL.Const.TechnicalContactListMenuId, CheckControl.ReturnAttachUrl);
                    SaveAttachFile(CheckControl.TechnicalContactListId + "r", BLL.Const.TechnicalContactListMenuId, CheckControl.ReAttachUrl);
                    SaveAttachFile(CheckControl.TechnicalContactListId, BLL.Const.TechnicalContactListMenuId, CheckControl.AttachUrl);
                    res.resultValue = CheckControl.TechnicalContactListId;
                }
                res.successful = true;
            }
            catch (Exception e)
            {
                res.resultHint = e.StackTrace;
                res.successful = false;
            }

            return res;

        }

        /// <summary>
        /// 
        /// </summary>
        public static void SaveAttachFile(string dataId, string menuId, string url)
        {
            Model.ToDoItem toDoItem = new Model.ToDoItem
            {
                MenuId = menuId,
                DataId = dataId,
                UrlStr = url,
            };
            APIUpLoadFileService.SaveAttachUrl(toDoItem);
        }

        [HttpPost]
        public ResponseData<string> AddApprove([FromBody]Model.Check_TechnicalContactListApprove approve)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {
                Model.Check_TechnicalContactList CheckControl = new Model.Check_TechnicalContactList();
                CheckControl.TechnicalContactListId = approve.TechnicalContactListId;
                CheckControl.State = approve.ApproveType;
                BLL.TechnicalContactListService.UpdateTechnicalContactListForApi(CheckControl);
                Model.Check_TechnicalContactList technicalContactList = TechnicalContactListService.GetTechnicalContactListByTechnicalContactListId(approve.TechnicalContactListId);
                string unitType = string.Empty;
                Model.Project_ProjectUnit unit = ProjectUnitService.GetProjectUnitByUnitIdProjectId(technicalContactList.ProjectId, technicalContactList.ProposedUnitId);
                if (unit != null)
                {
                    unitType = unit.UnitType;
                }
                if (unitType == BLL.Const.ProjectUnitType_1 && technicalContactList.IsReply == "2" && approve.ApproveType == Const.TechnicalContactList_Complete)  //总包发起
                {
                    List<Model.Sys_User> seeUsers = new List<Model.Sys_User>();
                    seeUsers.AddRange(UserService.GetSeeUserList3(technicalContactList.ProjectId, technicalContactList.ProposedUnitId, technicalContactList.MainSendUnitId, technicalContactList.CCUnitIds, technicalContactList.CNProfessionalCode, technicalContactList.UnitWorkId.ToString()));
                    seeUsers = seeUsers.Distinct().ToList();
                    foreach (var seeUser in seeUsers)
                    {
                        Model.Check_TechnicalContactListApprove approveS = new Model.Check_TechnicalContactListApprove();
                        approveS.TechnicalContactListId = approve.TechnicalContactListId;
                        approveS.ApproveMan = seeUser.UserId;
                        approveS.ApproveType = "S";
                        TechnicalContactListApproveService.AddTechnicalContactListApprove(approveS);
                    }
                }
                if (unitType == BLL.Const.ProjectUnitType_2 && technicalContactList.IsReply == "2" && approve.ApproveType == Const.TechnicalContactList_Complete)  //分包发起
                {
                    List<Model.Sys_User> seeUsers = new List<Model.Sys_User>();
                    seeUsers.AddRange(UserService.GetSeeUserList3(technicalContactList.ProjectId, technicalContactList.ProposedUnitId, technicalContactList.MainSendUnitId, technicalContactList.CCUnitIds, technicalContactList.CNProfessionalCode, technicalContactList.UnitWorkId.ToString()));
                    seeUsers = seeUsers.Distinct().ToList();
                    foreach (var seeUser in seeUsers)
                    {
                        Model.Check_TechnicalContactListApprove approveS = new Model.Check_TechnicalContactListApprove();
                        approveS.TechnicalContactListId = approve.TechnicalContactListId;
                        approveS.ApproveMan = seeUser.UserId;
                        approveS.ApproveType = "S";
                        TechnicalContactListApproveService.AddTechnicalContactListApprove(approveS);
                    }
                }
                res.resultValue = BLL.TechnicalContactListApproveService.AddTechnicalContactListApprove(approve);
                res.successful = true;
            }
            catch (Exception e)
            {
                res.resultHint = e.StackTrace;
                res.successful = false;
            }

            return res;

        }
        [HttpPost]
        public ResponseData<string> UpdateApprove([FromBody]Model.Check_TechnicalContactListApprove approve)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {
                Model.Check_TechnicalContactListApprove approve1 = BLL.TechnicalContactListApproveService.GetTechnicalContactListApproveByApproveIdForApi(approve.TechnicalContactListApproveId);

                Check_TechnicalContactList technicalContactList = BLL.TechnicalContactListService.GetTechnicalContactListByTechnicalContactListId(approve1.TechnicalContactListId);

                if (technicalContactList.IsReply == "1")
                {
                    approve.ApproveDate = DateTime.Now;
                    //  approve1.ApproveIdea = approve.ApproveIdea;
                    // approve1.IsAgree = approve.IsAgree;
                    //approve1.AttachUrl = approve.AttachUrl;

                    switch (approve1.ApproveType)
                    {
                        case "5":
                        case "7":
                        case "F":
                        case "Z":
                        case "J":
                        case "H":
                            {
                                Model.Check_TechnicalContactList CheckControl = new Model.Check_TechnicalContactList();
                                CheckControl.TechnicalContactListId = approve1.TechnicalContactListId;
                                CheckControl.ReOpinion = approve.ApproveIdea;
                                BLL.TechnicalContactListService.UpdateTechnicalContactListForApi(CheckControl);
                                approve.ApproveIdea = null;

                            }
                            break;
                        case "2":
                            Project_ProjectUnit unit = BLL.ProjectUnitService.GetProjectUnitByUnitIdProjectId(technicalContactList.ProjectId, technicalContactList.ProposedUnitId);
                            //Base_Unit unit = BLL.UnitService.GetUnitByUnitId(technicalContactList.ProposedUnitId);
                            if (unit.UnitType == BLL.Const.ProjectUnitType_1)
                            {
                                Model.Check_TechnicalContactList CheckControl = new Model.Check_TechnicalContactList();
                                CheckControl.TechnicalContactListId = approve1.TechnicalContactListId;
                                CheckControl.ReOpinion = approve.ApproveIdea;
                                BLL.TechnicalContactListService.UpdateTechnicalContactListForApi(CheckControl);
                                approve.ApproveIdea = null;

                            }

                            break;
                        case "4":
                            Project_ProjectUnit unit1 = BLL.ProjectUnitService.GetProjectUnitByUnitIdProjectId(technicalContactList.ProjectId, technicalContactList.ProposedUnitId);

                            if (unit1.UnitType != BLL.Const.ProjectUnitType_1)
                            {
                                Model.Check_TechnicalContactList CheckControl = new Model.Check_TechnicalContactList();
                                CheckControl.TechnicalContactListId = approve1.TechnicalContactListId;
                                CheckControl.ReOpinion = approve.ApproveIdea;
                                BLL.TechnicalContactListService.UpdateTechnicalContactListForApi(CheckControl);
                                approve.ApproveIdea = null;

                            }

                            break;
                    }

                }
                BLL.TechnicalContactListApproveService.UpdateTechnicalContactListApproveForApi(approve);
                res.successful = true;
            }
            catch (Exception e)
            {
                res.resultHint = e.StackTrace;
                res.successful = false;
            }

            return res;

        }
        [HttpGet]
        public ResponseData<string> see(string dataId, string userId)
        {
            ResponseData<string> res = new ResponseData<string>();
            res.successful = true;
            BLL.TechnicalContactListApproveService.See(dataId, userId);
            return res;
        }
        /// <summary>
        /// 导出工程联络单
        /// </summary>
        /// <param name="CheckControlCode"></param>
        /// <returns></returns>
        [HttpGet]
        public ResponseData<string> UplodeTechnicalContact(string id)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {
                Model.Check_TechnicalContactList technicalContactList = TechnicalContactListService.GetTechnicalContactListByTechnicalContactListId(id);
                string rootPath = ConfigurationManager.AppSettings["localRoot"];//物理路径 
                string uploadfilepath = rootPath + Const.TechnicalContactListTemplateUrl;
                string path = "FileUpload\\TechnicalContactList\\" + DateTime.Now.ToString("yyyy-MM") + "\\工程联络单.doc";
                string newuploadfilepath = rootPath + path;
                string newUrl = newuploadfilepath.Replace(".doc", technicalContactList.Code + ".doc");
                string AttachPath = rootPath + "FileUpload\\TechnicalContactList\\" + DateTime.Now.ToString("yyyy-MM"); ///文件夹
                if (!Directory.Exists(AttachPath))
                {
                    Directory.CreateDirectory(AttachPath);
                }
                if (File.Exists(newUrl))
                {
                    File.Delete(newUrl);
                }
                File.Copy(uploadfilepath, newUrl);
                //更新书签内容
                var unit = UnitService.GetUnitByUnitId(technicalContactList.ProposedUnitId);
                string unitType = string.Empty;
                var projectUnit = ProjectUnitService.GetProjectUnitByUnitIdProjectId(technicalContactList.ProjectId, technicalContactList.ProposedUnitId);
                if (unit != null)
                {
                    unitType = projectUnit.UnitType;
                }
                Document doc = new Aspose.Words.Document(newUrl);
                Bookmark bookmarkProjectName = doc.Range.Bookmarks["ProjectName"];
                if (bookmarkProjectName != null)
                {
                    var project = ProjectService.GetProjectByProjectId(technicalContactList.ProjectId);
                    if (project != null)
                    {
                        bookmarkProjectName.Text = project.ProjectName;
                    }
                }
                Bookmark bookmarkCode = doc.Range.Bookmarks["Code"];
                if (bookmarkCode != null)
                {
                    bookmarkCode.Text = technicalContactList.Code;
                }
                Bookmark bookmarkProposedUnit = doc.Range.Bookmarks["ProposedUnit"];
                if (bookmarkProposedUnit != null)
                {

                    if (unit != null)
                    {
                        bookmarkProposedUnit.Text = unit.UnitName;
                    }

                }
                Bookmark bookmarkUnitWork = doc.Range.Bookmarks["UnitWork"];
                if (bookmarkUnitWork != null)
                {
                    bookmarkUnitWork.Text = UnitWorkService.GetUnitWorkName(technicalContactList.UnitWorkId);
                }
                Bookmark bookmarkCNProfessional = doc.Range.Bookmarks["CNProfessional"];
                if (bookmarkCNProfessional != null)
                {
                    bookmarkCNProfessional.Text = CNProfessionalService.GetCNProfessionalNameByCode(technicalContactList.CNProfessionalCode);
                }
                Bookmark bookmarkMainSendUnit = doc.Range.Bookmarks["MainSendUnit"];
                if (bookmarkMainSendUnit != null)
                {
                    bookmarkMainSendUnit.Text = UnitService.GetUnitNameByUnitId(technicalContactList.MainSendUnitId);
                }
                Bookmark bookmarkCCUnit = doc.Range.Bookmarks["CCUnit"];
                if (bookmarkCCUnit != null)
                {
                    bookmarkCCUnit.Text = UnitService.GetUnitNameByUnitId(technicalContactList.CCUnitIds);
                }
                Bookmark bookmarkContactListType = doc.Range.Bookmarks["ContactListType"];
                if (bookmarkContactListType != null)
                {
                    if (technicalContactList.ContactListType == "1")
                    {
                        bookmarkContactListType.Text = "■图纸类   □非图纸类";
                    }
                    else
                    {
                        bookmarkContactListType.Text = "□图纸类   ■非图纸类";
                    }
                }
                Bookmark bookmarkIsReply = doc.Range.Bookmarks["IsReply"];
                if (bookmarkIsReply != null)
                {
                    if (technicalContactList.IsReply == "1")
                    {
                        bookmarkIsReply.Text = "■需要回复   □不需回复";
                    }
                    else
                    {
                        bookmarkIsReply.Text = "□需要回复   ■不需回复";
                    }
                }
                //☑
                Bookmark bookmarkCause = doc.Range.Bookmarks["Cause"];
                if (bookmarkCause != null)
                {
                    if (!string.IsNullOrEmpty(technicalContactList.Cause))
                    {
                        bookmarkCause.Text = technicalContactList.Cause;
                    }

                }
                Bookmark bookmarkContents = doc.Range.Bookmarks["Contents"];
                if (bookmarkContents != null)
                {
                    if (!string.IsNullOrEmpty(technicalContactList.Contents))
                    {
                        bookmarkContents.Text = technicalContactList.Contents;
                    }

                }
                Bookmark bookmarkAttachUrl = doc.Range.Bookmarks["AttachUrl"];
                if (bookmarkAttachUrl != null)
                {
                    if (AttachFileService.Getfile(technicalContactList.TechnicalContactListId, Const.TechnicalContactListMenuId))
                    {
                        bookmarkAttachUrl.Text = "见附页";
                    }
                    else
                    {
                        bookmarkAttachUrl.Text = "无";
                    }
                }
                Bookmark bookmarkCompileMan = doc.Range.Bookmarks["CompileMan"];
                if (bookmarkCompileMan != null)
                {
                    Model.Sys_User user = UserService.GetUserByUserId(technicalContactList.CompileMan);
                    var file = string.Empty;
                    if (user != null)
                    {
                        file = user.SignatureUrl;
                    }
                    if (!string.IsNullOrWhiteSpace(file))
                    {
                        string url = rootPath + file;
                        DocumentBuilder builders = new DocumentBuilder(doc);
                        builders.MoveToBookmark("CompileMan");
                        if (!string.IsNullOrEmpty(url))
                        {
                            System.Drawing.Size JpgSize;
                            float Wpx;
                            float Hpx;
                            UploadAttachmentService.getJpgSize(url, out JpgSize, out Wpx, out Hpx);
                            double i = 1;
                            i = JpgSize.Width / 50.0;
                            if (File.Exists(url))
                            {
                                builders.InsertImage(url, JpgSize.Width / i, JpgSize.Height / i);
                            }
                            else
                            {
                                bookmarkCompileMan.Text = user.UserName;
                            }

                        }
                    }
                    else
                    {
                        bookmarkCompileMan.Text = UserService.GetUserNameByUserId(technicalContactList.CompileMan);
                    }
                }
                Bookmark bookmarkAuditMan1 = doc.Range.Bookmarks["AuditMan1"];
                if (bookmarkAuditMan1 != null)
                {
                    Model.Check_TechnicalContactListApprove approve = null;
                    if (unitType == BLL.Const.ProjectUnitType_2)   //施工分包发起
                    {
                        approve = TechnicalContactListApproveService.GetApprove(technicalContactList.TechnicalContactListId);
                    }
                    else
                    {
                        approve = TechnicalContactListApproveService.GetApprove2(technicalContactList.TechnicalContactListId);
                    }
                    if (approve != null)
                    {
                        Model.Sys_User user = UserService.GetUserByUserId(approve.ApproveMan);
                        var file = user.SignatureUrl;
                        if (!string.IsNullOrWhiteSpace(file))
                        {
                            string url = rootPath + file;
                            DocumentBuilder builders = new DocumentBuilder(doc);
                            builders.MoveToBookmark("AuditMan1");
                            if (!string.IsNullOrEmpty(url))
                            {
                                System.Drawing.Size JpgSize;
                                float Wpx;
                                float Hpx;
                                UploadAttachmentService.getJpgSize(url, out JpgSize, out Wpx, out Hpx);
                                double i = 1;
                                i = JpgSize.Width / 50.0;
                                if (File.Exists(url))
                                {
                                    builders.InsertImage(url, JpgSize.Width / i, JpgSize.Height / i);
                                }
                                else
                                {
                                    bookmarkAuditMan1.Text = user.UserName;
                                }

                            }
                        }
                        else
                        {
                            bookmarkAuditMan1.Text = UserService.GetUserNameByUserId(approve.ApproveMan);
                        }
                    }
                }
                Bookmark bookmarkCompileDate = doc.Range.Bookmarks["CompileDate"];
                if (bookmarkCompileDate != null)
                {
                    if (technicalContactList.CompileDate != null)
                    {
                        bookmarkCompileDate.Text = string.Format("{0:yyyy-MM-dd}", technicalContactList.CompileDate);
                    }
                }
                Bookmark bookmarkApproveIdea = doc.Range.Bookmarks["ApproveIdea"];
                if (bookmarkApproveIdea != null)
                {
                    if (!string.IsNullOrEmpty(technicalContactList.ReOpinion))
                    {
                        bookmarkApproveIdea.Text = technicalContactList.ReOpinion;
                    }

                }
                doc.Save(newUrl);
                //生成PDF文件
                string pdfUrl = newUrl.Replace(".doc", ".pdf");
                if (File.Exists(pdfUrl))
                {
                    File.Delete(pdfUrl);
                }
                Document doc1 = new Aspose.Words.Document(newUrl);
                //验证参数
                if (doc1 == null) { throw new Exception("Word文件无效"); }
                doc1.Save(pdfUrl, Aspose.Words.SaveFormat.Pdf);//还可以改成其它格式
                                                               //Microsoft.Office.Interop.Word.Document doc1 = new Microsoft.Office.Interop.Word.Document(newUrl);
                                                               //object fontname = "Wingdings 2";
                                                               //object uic = true;
                                                               //doc1.Bookmarks["ApproveIdea"].Range.InsertSymbol(-4014, ref fontname, ref uic);
                string initTemplatePath = "FileUpload/TechnicalContactList/" + DateTime.Now.ToString("yyyy-MM") + "/工程联络单.doc";
                string filePath = initTemplatePath.Replace(".doc", technicalContactList.Code + ".pdf");
                //string fileName = Path.GetFileName(filePath);
                //FileInfo info = new FileInfo(pdfUrl);
                //long fileSize = info.Length;
                //System.Web.HttpContext.Current.Response.Clear();
                //System.Web.HttpContext.Current.Response.ContentType = "application/x-zip-compressed";
                //System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                //System.Web.HttpContext.Current.Response.AddHeader("Content-Length", fileSize.ToString());
                //System.Web.HttpContext.Current.Response.TransmitFile(pdfUrl, 0, fileSize);
                //System.Web.HttpContext.Current.Response.Flush();
                res.successful = true;
                res.resultValue = filePath;
            }
            catch (Exception e)
            {
                res.successful = false;
                res.resultValue = e.StackTrace + e.Message;
                throw;
            }

            return res;
        }
    }
}
