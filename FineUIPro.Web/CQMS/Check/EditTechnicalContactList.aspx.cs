using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aspose.Words;
using System.Text;
using System.Web.Security;

namespace FineUIPro.Web.CQMS.Check
{
    public partial class EditTechnicalContactList : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string TechnicalContactListId
        {
            get
            {
                return (string)ViewState["TechnicalContactListId"];
            }
            set
            {
                ViewState["TechnicalContactListId"] = value;
            }
        }

        public int HandleImg
        {
            get
            {
                return Convert.ToInt32(ViewState["HandleImg"]);
            }
            set
            {
                ViewState["HandleImg"] = value;
            }
        }
        public int ReplyFileImg
        {
            get
            {
                return Convert.ToInt32(ViewState["ReplyFileImg"]);
            }
            set
            {
                ViewState["ReplyFileImg"] = value;
            }
        }

        /// <summary>
        /// 办理类型
        /// </summary>
        public string State
        {
            get
            {
                return (string)ViewState["State"];
            }
            set
            {
                ViewState["State"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                UnitService.GetUnit(drpProposeUnit, CurrUser.LoginProjectId, false);
                var unitWork = UnitWorkService.GetUnitWorkLists(CurrUser.LoginProjectId);
                gvUnitWork.DataSource = unitWork;
                gvUnitWork.DataBind();
                var gvCNProfessional = CNProfessionalService.GetList();
                gvCNPro.DataSource = gvCNProfessional;
                gvCNPro.DataBind();
                //主送单位
                gvMainSendUnit.DataSource = UnitService.GetUnitByProjectIdList(CurrUser.LoginProjectId);
                gvMainSendUnit.DataBind();
                //抄送单位
                gvCCUnit.DataSource = UnitService.GetUnitByProjectIdList(CurrUser.LoginProjectId);
                gvCCUnit.DataBind();
                HideOptions.Hidden = true;
                HideReplyFile.Hidden = true;
                ReOpinion.Hidden = true;
                //Url.Visible = false;
                rblIsAgree.Visible = false;
                txtProjectName.Text = ProjectService.GetProjectByProjectId(CurrUser.LoginProjectId).ProjectName;

                TechnicalContactListId = Request.Params["TechnicalContactListId"];
                if (!string.IsNullOrEmpty(TechnicalContactListId))
                {
                    hdTechnicalContactListId.Text = TechnicalContactListId;

                    Model.Check_TechnicalContactList technicalContactList = TechnicalContactListService.GetTechnicalContactListByTechnicalContactListId(TechnicalContactListId);
                    string unitType = string.Empty;
                    txtCode.Text = technicalContactList.Code;
                    if (!string.IsNullOrEmpty(technicalContactList.ProposedUnitId))
                    {
                        drpProposeUnit.SelectedValue = technicalContactList.ProposedUnitId;
                        Model.Project_ProjectUnit unit = ProjectUnitService.GetProjectUnitByUnitIdProjectId(this.CurrUser.LoginProjectId, technicalContactList.ProposedUnitId);
                        if (unit != null)
                        {
                            unitType = unit.UnitType;
                        }
                    }

                    if (!string.IsNullOrEmpty(technicalContactList.UnitWorkId))
                    {
                        //txtUnitWork.Text = ConstructSolutionService.GetUnitWorkName(technicalContactList.UnitWorkId);
                        txtUnitWork.Values = technicalContactList.UnitWorkId.Split(',');
                    }
                    if (!string.IsNullOrEmpty(technicalContactList.CNProfessionalCode))
                    {
                        //txtCNProfessional.Text = ConstructSolutionService.GetProfessionalName(technicalContactList.CNProfessionalCode);
                        txtCNProfessional.Values = technicalContactList.CNProfessionalCode.Split(',');
                    }
                    if (!string.IsNullOrEmpty(technicalContactList.MainSendUnitId))
                    {
                        txtMainSendUnit.Values = technicalContactList.MainSendUnitId.Split(',');
                    }
                    if (!string.IsNullOrEmpty(technicalContactList.CCUnitIds))
                    {
                        txtCCUnit.Values = technicalContactList.CCUnitIds.Split(',');
                    }
                    string contactListType = technicalContactList.ContactListType;
                    string isReply = technicalContactList.IsReply;
                    if (!string.IsNullOrEmpty(technicalContactList.ContactListType))
                    {
                        rblContactListType.SelectedValue = technicalContactList.ContactListType;
                    }
                    if (!string.IsNullOrEmpty(technicalContactList.IsReply))
                    {
                        rblIsReply.SelectedValue = technicalContactList.IsReply;
                    }
                    txtCause.Text = technicalContactList.Cause;
                    txtContents.Text = technicalContactList.Contents;
                    txtReOpinion.Text = technicalContactList.ReOpinion;
                    Model.Check_TechnicalContactListApprove approve = TechnicalContactListApproveService.GetComplie(TechnicalContactListId);
                    if (approve != null)
                    {

                    }
                    if (!string.IsNullOrEmpty(technicalContactList.State))
                    {
                        State = technicalContactList.State;
                    }
                    else
                    {
                        State = Const.TechnicalContactList_Compile;
                        HideOptions.Hidden = true;
                        rblIsAgree.Visible = false;

                        ReplyFileImg = -1;
                    }
                    if (State != Const.TechnicalContactList_Complete)
                    {
                        TechnicalContactListService.InitHandleType(drpHandleType, false, State, unitType, technicalContactList.ContactListType, technicalContactList.IsReply);
                    }
                    if (State == Const.TechnicalContactList_Compile || State == Const.TechnicalContactList_ReCompile)
                    {
                        HideOptions.Hidden = true;
                        rblIsAgree.Visible = false;
                        drpHandleMan.Enabled = true;
                        drpHandleMan.Required = true;
                        if (drpHandleType.SelectedValue == Const.TechnicalContactList_ReCompile)
                        {
                            drpHandleMan.Enabled = true;
                            UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, drpProposeUnit.SelectedValue);
                            drpHandleMan.Required = true;
                        }
                        else
                        {
                            drpHandleMan.Enabled = true;
                            UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, string.Empty);
                            drpHandleMan.Required = true;

                        }


                        drpHandleMan.SelectedIndex = 1;
                        //HandleImg = -1;
                    }
                    else
                    {

                        UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, string.Empty);
                        HideOptions.Hidden = false;
                        rblIsAgree.Visible = true;
                        HideReplyFile.Hidden = true;
                        ReOpinion.Hidden = true;
                        HandleImg = 0;
                        ReplyFileImg = 0;
                    }
                    if (unitType == BLL.Const.ProjectUnitType_2)  //分包发起
                    {
                        if (contactListType == "1")  //图纸类
                        {
                            if (State == Const.TechnicalContactList_Audit3)
                            {
                                rblIsAgree.Visible = true;
                                drpHandleMan.Enabled = false;
                                drpHandleMan.Required = false;
                            }
                        }
                        else
                        {
                            if (State == Const.TechnicalContactList_Audit3)
                            {
                                rblIsAgree.Visible = true;
                                drpHandleMan.Enabled = false;
                                drpHandleMan.Required = false;
                            }
                        }
                        if (isReply == "2")    //不需回复
                        {
                            if (State == Const.TechnicalContactList_Audit1)
                            {
                                rblIsAgree.Visible = true;
                                drpHandleMan.Enabled = false;
                                drpHandleMan.Required = false;
                            }
                        }

                    }
                    else   //总包发起
                    {
                        if (isReply == "1")  //需要回复
                        {
                            if (State == Const.TechnicalContactList_Audit1)
                            {
                                rblIsAgree.Visible = true;
                                drpHandleMan.Enabled = false;
                                drpHandleMan.Required = false;
                            }
                        }
                        else  //不需回复
                        {
                            if (State == Const.TechnicalContactList_Audit3)
                            {
                                rblIsAgree.Visible = true;
                                drpHandleMan.Enabled = false;
                                drpHandleMan.Required = false;
                            }
                        }
                        HandleImg = 0;
                    }
                    if (State == Const.TechnicalContactList_Complete || !string.IsNullOrEmpty(Request.Params["see"]))
                    {
                        btnSave.Visible = false;
                        btnSubmit.Visible = false;
                        next.Visible = false;
                    }
                    if (unitType == BLL.Const.ProjectUnitType_2)  //施工分包商
                    {
                        if (State == Const.TechnicalContactList_Audit2 || State == Const.TechnicalContactList_Audit2R || State == Const.TechnicalContactList_Audit2H || State == Const.TechnicalContactList_Audit3 || State == Const.TechnicalContactList_Audit4 || State == Const.TechnicalContactList_Audit4R)
                        {
                            DoeNabled();
                            //HideReplyFile.Visible = true;
                            //var str = txtMainSendUnit.Values.ToList();
                            //drpHandleMan.Items.Clear();
                            //UserService.InitUsersDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, string.Join(",", str));
                            //txtCode.Enabled = false;
                            //drpProposeUnit.Enabled = false;
                            //txtUnitWork.Enabled = false;
                            //txtCNProfessional.Enabled = false;
                            //txtMainSendUnit.Enabled = false;
                            //txtCCUnit.Enabled = false;
                            //rblContactListType.Enabled = false;
                            //rblIsReply.Enabled = false;
                            //txtCause.Enabled = false;
                            //txtContents.Enabled = false;
                            //imgfile.Visible = false;
                        }
                        //图纸类，总包专工操作时，显示导出和上传功能
                        if (State == Const.TechnicalContactList_Audit2 && technicalContactList.ContactListType == "1")
                        {
                            plExport.Hidden = false;
                            plReFile.Hidden = false;
                            AttachFile();
                        }
                        //总包负责人审批，两种打回方式
                        if (State == Const.TechnicalContactList_Audit3)
                        {
                            drpHandleType.Enabled = true;
                        }
                        if (State == Const.TechnicalContactList_Audit2 || State == Const.TechnicalContactList_Audit2H || State == Const.TechnicalContactList_Audit4 || State == Const.TechnicalContactList_Audit2R)
                        {
                            txtOpinions.Required = false;
                        }
                    }
                    else   //总包
                    {
                        if (State == Const.TechnicalContactList_Audit1 || State == Const.TechnicalContactList_Audit6 || State == Const.TechnicalContactList_Audit6R)
                        {
                            //txtCode.Enabled = false;
                            //txtUnitWork.Enabled = false;
                            //txtCNProfessional.Enabled = false;
                            //txtMainSendUnit.Enabled = false;
                            //txtCCUnit.Enabled = false;
                            //rblContactListType.Enabled = false;
                            //rblIsReply.Enabled = false;
                            //txtCause.Enabled = false;
                            //txtContents.Enabled = false;
                            //imgfile.Visible = false;
                            DoeNabled();
                            //HideReplyFile.Visible = true;
                            //var str = txtMainSendUnit.Values.ToList();
                            //drpHandleMan.Items.Clear();
                            //UserService.InitUsersDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, string.Join(",", str));
                        }
                        if (State == Const.TechnicalContactList_Audit1)
                        {
                            drpHandleType.Enabled = true;

                        }
                        if (State == Const.TechnicalContactList_Audit6 || State == Const.TechnicalContactList_Audit6R)
                        {
                            txtOpinions.Required = false;
                        }
                    }
                    if (technicalContactList.State == Const.TechnicalContactList_Audit6 || technicalContactList.State == Const.TechnicalContactList_Audit6R)
                    {
                        rblIsAgree.Visible = false;
                        HideReplyFile.Hidden = false;
                        ReOpinion.Hidden = false;
                        HideOptions.Hidden = true;
                        txtReOpinion.Required = true;
                        txtReOpinion.ShowRedStar = true;
                    }
                    //drpProposeUnit_SelectedIndexChanged(null, null);
                    if (drpHandleType.Items.Count == 2)
                    {
                        drpHandleType.Readonly = true;
                    }

                    //设置用户的的可编辑区域
                    if (!State.Equals(Const.TechnicalContactList_Complete))
                    {
                        if (State.Equals(Const.TechnicalContactList_ReCompile) || State.Equals(Const.TechnicalContactList_Compile) ||
                          State.Equals(Const.TechnicalContactList_Audit1) || State.Equals(Const.TechnicalContactList_Audit6))
                        {
                            DoeNabled();
                        }
                        if (State.Equals(Const.TechnicalContactList_Compile) || State.Equals(Const.TechnicalContactList_ReCompile))
                        {
                            DoEdit();
                        }
                    }
                    drpProposeUnit_SelectedIndexChanged(null, null);
                    if (rblIsAgree.Visible == true)
                    {
                        Agree();
                    }
                    //设置回复审批场景下的操作
                    Reply(unitType);
                    BindGrid();
                }
                else
                {
                    State = Const.TechnicalContactList_Compile;
                    UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, string.Empty);
                    drpHandleMan.SelectedIndex = 1;
                    plApprove2.Hidden = true;
                    txtCode.Text = SQLHelper.RunProcNewId2("SpGetNewCode3ByProjectId", "dbo.Check_TechnicalContactList", "Code", CurrUser.LoginProjectId);
                    string unitId = string.Empty;
                    var mainUnit = UnitService.GetUnitByProjectIdUnitTypeList(this.CurrUser.LoginProjectId, Const.ProjectUnitType_1)[0];
                    if (mainUnit != null)
                    {
                        unitId = mainUnit.UnitId;
                    }
                    this.drpProposeUnit.SelectedValue = this.CurrUser.UnitId ?? unitId;
                    HandleImg = 0;
                    drpProposeUnit_SelectedIndexChanged(null, null);
                }


            }
            else
            {
                var eventArgs = GetRequestEventArgument(); // 此函数所在文件：PageBase.cs
                if (eventArgs.StartsWith("ButtonClick"))
                {
                    string rootPath = Server.MapPath("~/");
                    string uploadfilepath = rootPath + initTemplatePath;
                    string newUrl = uploadfilepath.Replace(".doc", txtCode.Text.Trim() + ".doc");
                    File.Copy(uploadfilepath, newUrl);
                    //更新书签内容
                    Model.Check_TechnicalContactList technicalContactList = TechnicalContactListService.GetTechnicalContactListByTechnicalContactListId(TechnicalContactListId);
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
                        bookmarkProposedUnit.Text = drpProposeUnit.SelectedItem.Text;
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
                        if (rblContactListType.SelectedValue == "1")
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
                        if (rblIsReply.SelectedValue == "1")
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
                        bookmarkCause.Text = txtCause.Text;
                    }
                    Bookmark bookmarkContents = doc.Range.Bookmarks["Contents"];
                    if (bookmarkContents != null)
                    {
                        bookmarkContents.Text = txtContents.Text;
                    }
                    Bookmark bookmarkAttachUrl = doc.Range.Bookmarks["AttachUrl"];
                    if (bookmarkAttachUrl != null)
                    {
                        if (!string.IsNullOrEmpty(technicalContactList.AttachUrl))
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
                        var user = UserService.GetUserByUserId(technicalContactList.CompileMan);
                        if (user != null)
                        {
                            bookmarkCompileMan.Text = user.UserName;
                        }
                    }
                    Bookmark bookmarkAuditMan1 = doc.Range.Bookmarks["AuditMan1"];
                    if (bookmarkAuditMan1 != null)
                    {
                        var approve = TechnicalContactListApproveService.GetApprove(TechnicalContactListId);
                        if (approve != null)
                        {
                            var user = UserService.GetUserByUserId(approve.ApproveMan);
                            if (user != null)
                            {
                                bookmarkAuditMan1.Text = user.UserName;
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
                        bookmarkApproveIdea.Text = txtOpinions.Text.Trim();
                    }
                    doc.Save(newUrl);
                    //生成PDF文件
                    string pdfUrl = newUrl.Replace(".doc", ".pdf");
                    Document doc1 = new Aspose.Words.Document(newUrl);
                    //验证参数
                    if (doc1 == null) { throw new Exception("Word文件无效"); }
                    doc1.Save(pdfUrl, Aspose.Words.SaveFormat.Pdf);//还可以改成其它格式
                                                                   //Microsoft.Office.Interop.Word.Document doc1 = new Microsoft.Office.Interop.Word.Document(newUrl);
                                                                   //object fontname = "Wingdings 2";
                                                                   //object uic = true;
                                                                   //doc1.Bookmarks["ApproveIdea"].Range.InsertSymbol(-4014, ref fontname, ref uic);

                    string filePath = initTemplatePath.Replace(".doc", txtCode.Text.Trim() + ".pdf");
                    string fileName = Path.GetFileName(filePath);
                    FileInfo info = new FileInfo(pdfUrl);
                    long fileSize = info.Length;
                    Response.Clear();
                    Response.ContentType = "application/x-zip-compressed";
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                    Response.AddHeader("Content-Length", fileSize.ToString());
                    Response.TransmitFile(pdfUrl, 0, fileSize);
                    Response.Flush();
                    File.Delete(newUrl);
                    File.Delete(pdfUrl);
                }
            }
        }

        /// <summary>
        /// 设置回复审批场景下的操作
        /// </summary>
        public void Reply(string type)
        {
            Model.Check_TechnicalContactList technicalContactList = TechnicalContactListService.GetTechnicalContactListByTechnicalContactListId(TechnicalContactListId);
            if (rblIsReply.SelectedValue.Equals("1"))
            {
                //回复操作
                //if (drpHandleType.SelectedValue.Equals(Const.TechnicalContactList_Audit2H) || drpHandleType.SelectedValue.Equals(Const.TechnicalContactList_Audit2R)
                //    || drpHandleType.SelectedValue.Equals(Const.TechnicalContactList_Audit4) || drpHandleType.SelectedValue.Equals(Const.TechnicalContactList_Audit4R)
                //    || drpHandleType.SelectedValue.Equals(Const.TechnicalContactList_Audit6) || drpHandleType.SelectedValue.Equals(Const.TechnicalContactList_Audit6R))
                //{
                //    //HideReplyFile.Visible = true;
                //    //txtMainSendUnit.Values.Join(",")
                //    var str = txtMainSendUnit.Values.ToList();
                //    drpHandleMan.Items.Clear();
                //    UserService.InitUsersDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, string.Join(",", str));
                //}
                //审批操作
                //|| State.Equals(Const.TechnicalContactList_Audit1) || State.Equals(Const.TechnicalContactList_Audit3)
                //var type = UnitService.GetUnitByUnitId(drpProposeUnit.SelectedValue).UnitType;
                //State.Equals(Const.TechnicalContactList_Audit1  TechnicalContactList_Audit3)
                if (type.Equals(Const.ProjectUnitType_1))
                {
                    if (State.Equals(Const.TechnicalContactList_Audit1) || State.Equals(Const.TechnicalContactList_Audit6)
                        || State.Equals(Const.TechnicalContactList_Audit6R))
                    {
                        HideReplyFile.Hidden = false;
                        ReOpinion.Hidden = false;
                        txtReOpinion.Required = true;
                        HideOptions.Hidden = true;
                        txtReOpinion.ShowRedStar = true;
                    }
                    else
                    {
                        HideReplyFile.Hidden = true;
                        ReOpinion.Hidden = true;
                        HideOptions.Hidden = false;

                        if (State == Const.TechnicalContactList_ReCompile)
                        {
                            if (!string.IsNullOrEmpty(technicalContactList.ReOpinion))
                            {
                                this.ReOpinion.Hidden = false;
                                this.txtReOpinion.Enabled = false;
                            }
                            this.txtOpinions.Hidden = true;
                        }
                    }

                    if (drpHandleType.SelectedValue.Equals(Const.TechnicalContactList_Audit1) || drpHandleType.SelectedValue.Equals(Const.TechnicalContactList_Audit6) || drpHandleType.SelectedValue.Equals(Const.TechnicalContactList_Audit6R))
                    {
                        //HideReplyFile.Visible = true;
                        //txtMainSendUnit.Values.Join(",")
                        var str = txtMainSendUnit.Values.ToList();
                        drpHandleMan.Items.Clear();
                        UserService.InitUsersDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, string.Join(",", str));
                    }

                }



                if (type.Equals(Const.ProjectUnitType_2))
                {
                    if (State.Equals(Const.TechnicalContactList_Audit2H) || State.Equals(Const.TechnicalContactList_Audit2R)
                            || State.Equals(Const.TechnicalContactList_Audit4) || State.Equals(Const.TechnicalContactList_Audit4R)
                            || State.Equals(Const.TechnicalContactList_Audit3))
                    {
                        HideReplyFile.Hidden = false;
                        ReOpinion.Hidden = false;
                        HideOptions.Hidden = true;
                        txtReOpinion.Required = true;
                        txtReOpinion.ShowRedStar = true;
                    }
                    else
                    {
                        HideReplyFile.Hidden = true;
                        ReOpinion.Hidden = true;
                        HideOptions.Hidden = false;

                        if (State == Const.TechnicalContactList_ReCompile)
                        {
                            if (!string.IsNullOrEmpty(technicalContactList.ReOpinion))
                            {
                                this.ReOpinion.Hidden = false;
                                this.txtReOpinion.Enabled = false;
                            }
                            this.txtOpinions.Hidden = true;
                        }
                    }

                    if (drpHandleType.SelectedValue.Equals(Const.TechnicalContactList_Audit2H) || drpHandleType.SelectedValue.Equals(Const.TechnicalContactList_Audit2R)
                    || drpHandleType.SelectedValue.Equals(Const.TechnicalContactList_Audit4) || drpHandleType.SelectedValue.Equals(Const.TechnicalContactList_Audit4R)
                    || drpHandleType.SelectedValue.Equals(Const.TechnicalContactList_Audit3))
                    {
                        var str = txtMainSendUnit.Values.ToList();
                        drpHandleMan.Items.Clear();
                        UserService.InitUsersDropDownList(drpHandleMan, CurrUser.LoginProjectId,

                            false, string.Join(",", str));
                    }

                }

                //if (State.Equals(Const.TechnicalContactList_Audit2H) || State.Equals(Const.TechnicalContactList_Audit2R)
                //   || State.Equals(Const.TechnicalContactList_Audit4) || State.Equals(Const.TechnicalContactList_Audit4R)
                //   || State.Equals(Const.TechnicalContactList_Audit6) || State.Equals(Const.TechnicalContactList_Audit6R) )
                //{
                //    HideReplyFile.Visible = true;
                //    //txtMainSendUnit.Values.Join(",")

                //}
                //else
                //{
                //    HideReplyFile.Visible = false;
                //}
            }
        }
        //TechnicalContactListApproveService
        public void BindGrid()
        {
            var data = TechnicalContactListApproveService.getListData(TechnicalContactListId);
            gvApprove.DataSource = data;
            gvApprove.DataBind();
        }

        protected void drpProposeUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            string unitType = string.Empty;
            Model.Project_ProjectUnit unit = ProjectUnitService.GetProjectUnitByUnitIdProjectId(this.CurrUser.LoginProjectId, drpProposeUnit.SelectedValue);
            if (unit != null)
            {
                unitType = unit.UnitType;
            }
            drpHandleType.Items.Clear();
            TechnicalContactListService.InitHandleType(drpHandleType, false, State, unitType, rblContactListType.SelectedValue, rblIsReply.SelectedValue);
            drpHandleType.SelectedIndex = 0;
            if (!string.IsNullOrWhiteSpace(TechnicalContactListId))
            {
                Model.Check_TechnicalContactList technicalContactList = TechnicalContactListService.GetTechnicalContactListByTechnicalContactListId(TechnicalContactListId);
                txtUnitWork.Values = technicalContactList.UnitWorkId.Split(',');
                txtCNProfessional.Values = technicalContactList.CNProfessionalCode.Split(',');
                txtMainSendUnit.Values = technicalContactList.MainSendUnitId.Split(',');
                if (!string.IsNullOrWhiteSpace(technicalContactList.CCUnitIds))
                {
                    txtCCUnit.Values = technicalContactList.CCUnitIds.Split(',');
                }

            }
            drpHandleType_SelectedIndexChanged(null, null);
        }



        protected void rblContactListType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string unitType = string.Empty;
            Model.Project_ProjectUnit unit = ProjectUnitService.GetProjectUnitByUnitIdProjectId(this.CurrUser.LoginProjectId, drpProposeUnit.SelectedValue);
            if (unit != null)
            {
                unitType = unit.UnitType;
            }
            drpHandleType.Items.Clear();
            TechnicalContactListService.InitHandleType(drpHandleType, false, State, unitType, rblContactListType.SelectedValue, rblIsReply.SelectedValue);
            drpHandleType.SelectedIndex = 0;
            if (!string.IsNullOrWhiteSpace(TechnicalContactListId))
            {
                Model.Check_TechnicalContactList technicalContactList = TechnicalContactListService.GetTechnicalContactListByTechnicalContactListId(TechnicalContactListId);
                txtUnitWork.Values = technicalContactList.UnitWorkId.Split(',');
                txtCNProfessional.Values = technicalContactList.CNProfessionalCode.Split(',');
                txtMainSendUnit.Values = technicalContactList.MainSendUnitId.Split(',');
                txtCCUnit.Values = technicalContactList.CCUnitIds.Split(',');
            }
        }

        protected void drpHandleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpHandleMan.Items.Clear();
            if (drpHandleType.SelectedText.Contains("分包") || drpHandleType.SelectedText.Contains("编制"))
            {
                UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, drpProposeUnit.SelectedValue);

            }
            else
            {
                UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, string.Empty);

            }
            if (drpHandleMan.Items.Count > 0)
            {
                drpHandleMan.SelectedIndex = 0;
            }

            if (drpHandleType.SelectedValue == Const.TechnicalContactList_Complete)
            {
                drpHandleMan.Items.Clear();
                drpHandleMan.Enabled = false;
                drpHandleMan.Required = false;
            }
            //Funs.FineUIPleaseSelect(drpHandleMan);
            //if (State.Equals(Const.TechnicalContactList_Compile))
            //{
            //    UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, drpProposeUnit.SelectedValue);
            //    if (drpHandleMan.Items.Count > 0)
            //    {
            //        drpHandleMan.SelectedIndex = 0;
            //    }
            //}
            //if (drpHandleType.SelectedValue == Const.TechnicalContactList_Complete)
            //{
            //    drpHandleMan.Enabled = false;
            //    drpHandleMan.Required = false;
            //}
            //else if (drpHandleType.SelectedValue == Const.TechnicalContactList_ReCompile)
            //{
            //    drpHandleMan.Enabled = true;
            //    UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, drpProposeUnit.SelectedValue);
            //    drpHandleMan.Required = true;
            //}
            //else if (drpHandleType.SelectedValue == Const.TechnicalContactList_Audit2R)
            //{
            //    drpHandleMan.Enabled = true;
            //    UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, string.Empty);
            //    drpHandleMan.Required = true;
            //}
            //else if (drpHandleType.SelectedValue == Const.TechnicalContactList_Audit4R)
            //{
            //    drpHandleMan.Enabled = true;
            //    UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, string.Empty);
            //    drpHandleMan.Required = true;
            //}
            //else if (drpHandleType.SelectedValue == Const.TechnicalContactList_Audit6R)
            //{
            //    drpHandleMan.Enabled = true;
            //    UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, drpProposeUnit.SelectedValue);
            //    drpHandleMan.Required = true;
            //}
        }
        /// <summary>
        /// 保存开工报告
        /// </summary>
        private void SavePauseNotice(string saveType)
        {
            Model.Check_TechnicalContactList technicalContactList = new Model.Check_TechnicalContactList();
            string unitType = string.Empty;
            technicalContactList.Code = txtCode.Text.Trim();
            technicalContactList.ProjectId = CurrUser.LoginProjectId;
            if (drpProposeUnit.SelectedValue != "0")
            {
                technicalContactList.ProposedUnitId = drpProposeUnit.SelectedValue;
                Model.Project_ProjectUnit unit = ProjectUnitService.GetProjectUnitByUnitIdProjectId(this.CurrUser.LoginProjectId, drpProposeUnit.SelectedValue);
                if (unit != null)
                {
                    unitType = unit.UnitType;
                }
            }
            technicalContactList.ReOpinion = txtReOpinion.Text.Trim();
            technicalContactList.UnitWorkId = String.Join(",", txtUnitWork.Values);
            technicalContactList.CNProfessionalCode = String.Join(",", txtCNProfessional.Values);
            technicalContactList.MainSendUnitId = String.Join(",", txtMainSendUnit.Values);
            if (!string.IsNullOrWhiteSpace(String.Join(",", txtCCUnit.Values)))
            {
                technicalContactList.CCUnitIds = String.Join(",", txtCCUnit.Values);
            }
            if (!string.IsNullOrEmpty(rblContactListType.SelectedValue))
            {
                technicalContactList.ContactListType = rblContactListType.SelectedValue;
            }
            else
            {
                technicalContactList.ContactListType = null;
            }
            if (!string.IsNullOrEmpty(rblIsReply.SelectedValue))
            {
                technicalContactList.IsReply = rblIsReply.SelectedValue;
            }
            else
            {
                technicalContactList.IsReply = null;
            }
            technicalContactList.Cause = txtCause.Text.Trim();
            technicalContactList.Contents = txtContents.Text.Trim();
            //technicalContactList.AttachUrl = hdFilePath.Value;
            //technicalContactList.ReAttachUrl = hdReFilePath.Value;
            if (saveType == "submit")
            {
                technicalContactList.State = drpHandleType.SelectedValue.Trim();
            }
            else
            {
                Model.Check_TechnicalContactList technicalContactList1 = TechnicalContactListService.GetTechnicalContactListByTechnicalContactListId(TechnicalContactListId);
                if (technicalContactList1 != null)
                {
                    if (string.IsNullOrEmpty(technicalContactList1.State))
                    {
                        technicalContactList.State = Const.TechnicalContactList_Compile;
                    }
                    else
                    {
                        technicalContactList.State = technicalContactList1.State;
                    }
                }
                else
                {
                    technicalContactList.State = Const.TechnicalContactList_Compile;
                }
            }

            if (!string.IsNullOrEmpty(TechnicalContactListId) && TechnicalContactListService.GetTechnicalContactListByTechnicalContactListId(TechnicalContactListId) != null)
            {
                Model.Check_TechnicalContactList technicalContactList1 = TechnicalContactListService.GetTechnicalContactListByTechnicalContactListId(TechnicalContactListId);
                Model.Check_TechnicalContactListApprove approve1 = TechnicalContactListApproveService.GetTechnicalContactListApproveByTechnicalContactListId(TechnicalContactListId);
                if (approve1 != null && saveType == "submit")
                {
                    //approve1.ApproveMan = CurrUser.UserId;
                    approve1.ApproveDate = DateTime.Now;
                    approve1.ApproveIdea = txtOpinions.Text.Trim();
                    TechnicalContactListApproveService.UpdateTechnicalContactListApprove(approve1);
                }
                if (saveType == "submit")
                {
                    //if (tr1.Visible == true && !string.IsNullOrEmpty(hdReFilePath.Value))    //总包专工操作且已上传反馈附件
                    //{
                    //    Model.Check_TechnicalContactListApprove approve = new Model.Check_TechnicalContactListApprove();
                    //    approve.TechnicalContactListId = technicalContactList1.TechnicalContactListId;
                    //    approve.ApproveType = Const.TechnicalContactList_Complete;
                    //    TechnicalContactListApproveService.AddTechnicalContactListApprove(approve);
                    //}
                    //else
                    //{
                    Model.Check_TechnicalContactListApprove approve = new Model.Check_TechnicalContactListApprove();
                    approve.TechnicalContactListId = technicalContactList1.TechnicalContactListId;
                    if (drpHandleMan.SelectedValue != "0")
                    {
                        approve.ApproveMan = drpHandleMan.SelectedValue;
                    }
                    approve.ApproveType = drpHandleType.SelectedValue;
                    TechnicalContactListApproveService.AddTechnicalContactListApprove(approve);
                    APICommonService.SendSubscribeMessage(approve.ApproveMan, "工程联络单待办理", this.CurrUser.UserName, string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now));
                    //}
                    if (unitType == BLL.Const.ProjectUnitType_1 && technicalContactList.IsReply == "2" && drpHandleType.SelectedValue == Const.TechnicalContactList_Complete)  //总包发起
                    {
                        List<Model.Sys_User> seeUsers = new List<Model.Sys_User>();
                        seeUsers.AddRange(UserService.GetSeeUserList3(CurrUser.LoginProjectId, technicalContactList.ProposedUnitId, technicalContactList.MainSendUnitId, technicalContactList.CCUnitIds, technicalContactList.CNProfessionalCode, technicalContactList.UnitWorkId.ToString()));
                        seeUsers = seeUsers.Distinct().ToList();
                        foreach (var seeUser in seeUsers)
                        {
                            Model.Check_TechnicalContactListApprove approveS = new Model.Check_TechnicalContactListApprove();
                            approveS.TechnicalContactListId = technicalContactList1.TechnicalContactListId;
                            approveS.ApproveMan = seeUser.UserId;
                            approveS.ApproveType = "S";
                            TechnicalContactListApproveService.AddTechnicalContactListApprove(approveS);
                        }
                    }
                    if (unitType == BLL.Const.ProjectUnitType_2 && technicalContactList.IsReply == "2" && drpHandleType.SelectedValue == Const.TechnicalContactList_Complete)  //分包发起
                    {
                        List<Model.Sys_User> seeUsers = new List<Model.Sys_User>();
                        seeUsers.AddRange(UserService.GetSeeUserList3(CurrUser.LoginProjectId, technicalContactList.ProposedUnitId, technicalContactList.MainSendUnitId, technicalContactList.CCUnitIds, technicalContactList.CNProfessionalCode, technicalContactList.UnitWorkId.ToString()));
                        seeUsers = seeUsers.Distinct().ToList();
                        foreach (var seeUser in seeUsers)
                        {
                            Model.Check_TechnicalContactListApprove approveS = new Model.Check_TechnicalContactListApprove();
                            approveS.TechnicalContactListId = technicalContactList1.TechnicalContactListId;
                            approveS.ApproveMan = seeUser.UserId;
                            approveS.ApproveType = "S";
                            TechnicalContactListApproveService.AddTechnicalContactListApprove(approveS);
                        }
                    }
                }
                technicalContactList.TechnicalContactListId = TechnicalContactListId;
                technicalContactList.ReOpinion = txtReOpinion.Text.Trim();
                TechnicalContactListService.UpdateTechnicalContactList(technicalContactList);
            }
            else
            {
                if (!string.IsNullOrEmpty(hdTechnicalContactListId.Text))
                {
                    technicalContactList.TechnicalContactListId = hdTechnicalContactListId.Text;
                }
                else
                {
                    technicalContactList.TechnicalContactListId = SQLHelper.GetNewID(typeof(Model.Check_TechnicalContactList));
                }
                //technicalContactList.TechnicalContactListId = newId;
                technicalContactList.CompileMan = CurrUser.UserId;
                technicalContactList.CompileDate = DateTime.Now;
                TechnicalContactListService.AddTechnicalContactList(technicalContactList);
                TechnicalContactListId = technicalContactList.TechnicalContactListId;
                Model.Check_TechnicalContactList technicalContactList1 = TechnicalContactListService.GetTechnicalContactListByTechnicalContactListId(TechnicalContactListId);

                if (saveType == "submit")
                {
                    Model.Check_TechnicalContactListApprove approve1 = new Model.Check_TechnicalContactListApprove();
                    approve1.TechnicalContactListId = technicalContactList.TechnicalContactListId;
                    approve1.ApproveDate = DateTime.Now;
                    approve1.ApproveMan = CurrUser.UserId;
                    approve1.ApproveType = Const.TechnicalContactList_Compile;
                    TechnicalContactListApproveService.AddTechnicalContactListApprove(approve1);

                    Model.Check_TechnicalContactListApprove approve = new Model.Check_TechnicalContactListApprove();
                    approve.TechnicalContactListId = technicalContactList.TechnicalContactListId;
                    if (drpHandleMan.SelectedValue != "0")
                    {
                        approve.ApproveMan = drpHandleMan.SelectedValue;
                    }
                    approve.ApproveType = drpHandleType.SelectedValue;

                    TechnicalContactListApproveService.AddTechnicalContactListApprove(approve);
                    APICommonService.SendSubscribeMessage(approve.ApproveMan, "工程联络单待办理", this.CurrUser.UserName, string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now));
                }
                else
                {
                    Model.Check_TechnicalContactListApprove approve1 = new Model.Check_TechnicalContactListApprove();
                    approve1.TechnicalContactListId = technicalContactList.TechnicalContactListId;
                    approve1.ApproveMan = CurrUser.UserId;
                    approve1.ApproveType = Const.TechnicalContactList_Compile;
                    TechnicalContactListApproveService.AddTechnicalContactListApprove(approve1);
                }
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            LogService.AddSys_Log(CurrUser, technicalContactList.Code, TechnicalContactListId, Const.TechnicalContactListMenuId, "编辑工程联络单");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, CurrUser.UserId, Const.TechnicalContactListMenuId, Const.BtnSave))
            {
                SavePauseNotice("save");

            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);

            }



        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, CurrUser.UserId, Const.TechnicalContactListMenuId, Const.BtnSave))
            {
                SavePauseNotice("submit");
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 是否同意的逻辑处理
        /// </summary>
        public void Agree()
        {
            string unitType = string.Empty;
            bool flag = false;
            Model.Project_ProjectUnit unit = ProjectUnitService.GetProjectUnitByUnitIdProjectId(this.CurrUser.LoginProjectId, drpProposeUnit.SelectedValue);
            if (unit != null)
            {
                unitType = unit.UnitType;
            }
            string contactListType = rblContactListType.SelectedValue;
            string isReply = rblIsReply.SelectedValue;
            drpHandleMan.Enabled = true;
            drpHandleMan.Required = true;
            drpHandleType.Items.Clear();
            string State = TechnicalContactListService.GetTechnicalContactListByTechnicalContactListId(TechnicalContactListId).State;
            TechnicalContactListService.InitHandleType(drpHandleType, false, State, unitType, rblContactListType.SelectedValue, rblIsReply.SelectedValue);
            if (rblIsAgree.SelectedValue.Equals("true"))
            {
                if (unitType == BLL.Const.ProjectUnitType_2)  //分包发起
                {
                    if (contactListType == "1")  //图纸类
                    {
                        if (State == Const.TechnicalContactList_Audit3)
                        {
                            drpHandleMan.Enabled = false;
                            drpHandleMan.Required = false;
                        }
                    }
                    else
                    {
                        if (State == Const.TechnicalContactList_Audit3)
                        {
                            drpHandleMan.Enabled = false;
                            drpHandleMan.Required = false;
                        }
                    }

                    if (State == Const.TechnicalContactList_Audit1)
                    {
                        DoEdit();
                    }
                }
                else   //总包发起
                {
                    if (isReply == "1")  //需要回复
                    {
                        if (State == Const.TechnicalContactList_Audit1)
                        {
                            drpHandleMan.Enabled = false;
                            drpHandleMan.Required = false;
                        }
                    }
                    else  //不需回复
                    {
                        if (State == Const.TechnicalContactList_Audit3)
                        {
                            drpHandleMan.Enabled = false;
                            drpHandleMan.Required = false;
                        }
                    }

                }

                if (drpHandleType.SelectedValue == Const.TechnicalContactList_Complete)
                {
                    drpHandleMan.Enabled = false;
                    drpHandleMan.Required = false;
                }

                if (drpHandleType.Items.Count == 2)
                {
                    drpHandleType.Readonly = true;
                }

                drpHandleType.SelectedIndex = 0;
                //List<>
                if (drpHandleType.Items.Count > 0)
                {
                    List<ListItem> lst = new List<ListItem>();
                    foreach (var item in drpHandleType.Items)
                    {
                        int index = drpHandleType.Items.IndexOf(item);
                        if (index != 0)
                        {
                            lst.Add(item);
                            //drpHandleType.Items.Remove(item);
                        }

                    }
                    if (lst.Count > 0)
                    {
                        foreach (var item in lst)
                        {
                            drpHandleType.Items.Remove(item);
                        }
                    }

                }

                if (drpHandleType.SelectedValue == Const.TechnicalContactList_Complete)
                {
                    drpHandleMan.Items.Clear();
                    drpHandleMan.Enabled = false;
                    drpHandleMan.Required = false;
                }
                else
                {
                    drpHandleMan.Items.Clear();
                    UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, string.Empty);
                    drpHandleMan.Enabled = true;
                    drpHandleMan.Required = true;
                }
                if (State == Const.TechnicalContactList_Audit2 && contactListType == "1")
                {
                    AttachFile();
                }
            }
            else
            {
                drpHandleMan.Items.Clear();
                //Funs.FineUIPleaseSelect(drpHandleMan);
                if (drpHandleType.Items.Count == 2)
                {
                    drpHandleType.Readonly = true;
                }
                drpHandleType.SelectedIndex = 1;
                if (drpHandleType.Items.Count > 0)
                {
                    drpHandleType.Items.RemoveAt(0);
                }
                if (drpHandleType.SelectedValue == Const.TechnicalContactList_ReCompile)
                {
                    drpHandleMan.Enabled = true;
                    UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, drpProposeUnit.SelectedValue);
                    var HandleMan = BLL.TechnicalContactListApproveService.GetComplie(this.TechnicalContactListId);                    if (HandleMan != null)                    {                        this.drpHandleMan.SelectedValue = HandleMan.ApproveMan;                        flag = true;                    }
                    drpHandleMan.Required = true;
                }
                else
                {
                    drpHandleMan.Enabled = true;
                    UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, string.Empty);
                    drpHandleMan.Required = true;

                }

            }
            Reply(unitType);

            if (drpHandleMan.Items.Count > 0)
            {
                if (!flag)
                {
                    drpHandleMan.SelectedIndex = 0;
                }

            }
        }


        protected void rblIsAgree_SelectedIndexChanged(object sender, EventArgs e)
        {
            Agree();
        }

        protected void imgfile_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(hdTechnicalContactListId.Text))   //新增记录
            {
                hdTechnicalContactListId.Text = SQLHelper.GetNewID(typeof(Model.Check_TechnicalContactList));
            }

            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?type={0}&toKeyId={1}&path=FileUpload/TechnicalContactList&menuId={2}", HandleImg, hdTechnicalContactListId.Text, Const.TechnicalContactListMenuId)));
        }
        /// <summary>
        /// 总包的操作
        /// </summary>
        public void DoeNabled()
        {
            txtCode.Enabled = false;
            drpProposeUnit.Enabled = false;
            txtUnitWork.Enabled = false;
            txtCNProfessional.Enabled = false;
            txtMainSendUnit.Enabled = false;
            txtCCUnit.Enabled = false;
            HandleImg = -1;
            rblContactListType.Enabled = false;
            rblIsReply.Enabled = false;
            txtCause.Enabled = false;
            txtContents.Enabled = false;
            txtProjectName.Enabled = false;
        }


        public void DoEdit()
        {
            txtCode.Enabled = true;
            drpProposeUnit.Enabled = true;
            txtUnitWork.Enabled = true;
            txtCNProfessional.Enabled = true;
            txtMainSendUnit.Enabled = true;
            txtCCUnit.Enabled = true;
            HandleImg = 0;
            rblContactListType.Enabled = true;
            rblIsReply.Enabled = true;
            txtCause.Enabled = true;
            txtContents.Enabled = true;
            txtProjectName.Enabled = true;
        }
        protected void ReplyFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hdTechnicalContactListId.Text))   //新增记录
            {
                hdTechnicalContactListId.Text = SQLHelper.GetNewID(typeof(Model.Check_TechnicalContactList));
            }

            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?type={0}&toKeyId={1}&path=FileUpload/TechnicalContactList&menuId={2}", ReplyFileImg, hdTechnicalContactListId.Text + "r", Const.TechnicalContactListMenuId)));
        }

        protected void imgBtnReFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hdTechnicalContactListId.Text))   //新增记录
            {
                hdTechnicalContactListId.Text = SQLHelper.GetNewID(typeof(Model.Check_TechnicalContactList));
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?type={0}&toKeyId={1}&path=FileUpload/TechnicalContactList&menuId={2}", 0, hdTechnicalContactListId.Text + "re", Const.TechnicalContactListMenuId)));

        }
        /// <summary>
        /// 人员模版文件原始的虚拟路径
        /// </summary>
        private string initTemplatePath = Const.TechnicalContactListTemplateUrl;

        /// <summary>
        /// 已上传设计反馈附件
        /// </summary>
        public void AttachFile()
        {
            string toKeyId = hdTechnicalContactListId.Text + "re";
            var res = AttachFileService.Getfile(hdTechnicalContactListId.Text + "re", Const.TechnicalContactListMenuId);
            //Alert.ShowInTop(res.ToString(), MessageBoxIcon.Warning);
            if (res)   //已上传设计反馈附件
            {
                drpHandleType.Items.Clear();
                drpHandleMan.Enabled = false;
                drpHandleMan.Required = false;
                drpHandleType.Items.Add("审批完成", Const.TechnicalContactList_Complete);
                drpHandleType.SelectedIndex = 0;
                drpHandleMan.Items.Clear();
            }
            else
            {
                drpProposeUnit_SelectedIndexChanged(null, null);
                drpHandleMan.Enabled = true;
                drpHandleMan.Required = true;
            }
        }

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            AttachFile();
        }
    }
}