using BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace FineUIPro.Web.PZHGL.InformationProject
{
    public partial class ConstructionReportEdit : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string ConstructionReportId
        {
            get
            {
                return (string)ViewState["ConstructionReportId"];
            }
            set
            {
                ViewState["ConstructionReportId"] = value;
            }
        }
        public int ContactImg
        {
            get
            {
                return Convert.ToInt32(ViewState["ContactImg"]);
            }
            set
            {
                ViewState["ContactImg"] = value;
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
                ConstructionReportId = Request.Params["ConstructionReportId"];
                HideOptions.Hidden = true;
                rblIsAgree.Hidden = true;
                BindData();
                drpFileType.DataValueField = "Value";
                drpFileType.DataTextField = "Text";
                drpFileType.DataSource = BLL.ConstructionReportService.GetFileTypeList();
                drpFileType.DataBind();
                Funs.FineUIPleaseSelect(drpFileType);
                if (!string.IsNullOrEmpty(ConstructionReportId))
                {
                    HFConstructionReportId.Text = ConstructionReportId;
                    Model.ZHGL_ConstructionReport constructionReport = ConstructionReportService.GetConstructionReportById(ConstructionReportId);
                    string unitType = string.Empty;
                    txtCode.Text = constructionReport.Code;
                    if (!string.IsNullOrEmpty(constructionReport.FileType))
                    {
                        this.drpFileType.SelectedValue = constructionReport.FileType;
                    }
                    if (constructionReport.CompileDate != null)
                    {
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", constructionReport.CompileDate);
                    }
                    this.txtContent.Text = HttpUtility.HtmlDecode(constructionReport.Content);
                    if (!string.IsNullOrEmpty(constructionReport.State))
                    {
                        State = constructionReport.State;
                    }
                    else
                    {
                        State = Const.ConstructionReport_Compile;
                        HideOptions.Hidden = true;
                        //Url.Visible = false;//附件查看权限-1
                        ContactImg = -1;
                        rblIsAgree.Hidden = true;
                    }
                    if (State != Const.ConstructionReport_Complete)
                    {
                        ConstructionReportService.InitHandleType(drpHandleType, false, State);
                    }
                    if (State == Const.ConstructionReport_Compile || State == Const.ConstructionReport_ReCompile)
                    {
                        HideOptions.Hidden = true;
                        ContactImg = 0;
                        rblIsAgree.Hidden = true;
                        drpHandleMan.Enabled = true;
                        drpHandleMan.Required = true;
                        UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, Const.UnitId_SEDIN);
                        //drpHandleMan.Items.AddRange(UserService.GetAllUserList(CurrUser.LoginProjectId));
                        drpHandleMan.SelectedIndex = 0;
                    }
                    else
                    {
                        //------------
                        UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, Const.UnitId_SEDIN);
                        //drpHandleMan.Items.AddRange(UserService.GetAllUserList(CurrUser.LoginProjectId));
                        HideOptions.Hidden = false;
                        //Url.Visible = true; 附件查看权限 - 1
                        ContactImg = -1;
                        rblIsAgree.Hidden = false;
                    }
                    if (drpHandleType.SelectedValue == Const.ConstructionReport_Complete)
                    {
                        rblIsAgree.Hidden = false;
                        drpHandleMan.Enabled = false;
                        drpHandleMan.Required = false;

                    }
                    else
                    {
                        drpHandleMan.Items.Clear();
                        //if (State != Const.ConstructionReport_Audit1)
                        //{
                            UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, Const.UnitId_SEDIN);
                        //}
                        //else
                        //{
                        //    UserService.InitUserProjectIdUnitIdRoleIdDropDownList(drpHandleMan, string.Empty, Const.UnitId_SEDIN, Const.SystemManager, false);
                        //}
                        drpHandleMan.Enabled = true;
                        drpHandleMan.Required = true;
                    }
                    if (rblIsAgree.Hidden == false)
                    {
                        Agree();
                    }
                    if (State == Const.ConstructionReport_Compile || State == Const.ConstructionReport_ReCompile)
                    {
                        HideOptions.Hidden = true;
                    }
                    //设置回复审批场景下的操作
                }
                else
                {
                    State = Const.ConstructionReport_Compile;
                    ConstructionReportService.InitHandleType(drpHandleType, false, State);
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, Const.UnitId_SEDIN);
                    drpHandleMan.SelectedIndex = 0;
                    plApprove2.Hidden = true;
                    string unitId = string.Empty;
                }
            }
            else
            {
                var eventArgs = GetRequestEventArgument(); // 此函数所在文件：PageBase.cs
                if (eventArgs.StartsWith("ButtonClick"))
                {
                    string rootPath = Server.MapPath("~/");
                    string path = string.Empty;
                    if (drpFileType.SelectedValue != BLL.Const._Null)
                    {
                        if (drpFileType.SelectedValue == "1")
                        {
                            path = Const.ConstructionWeeklyReportTemplateUrl;
                        }
                        else if (drpFileType.SelectedValue == "2")
                        {
                            path = Const.ConstructionMonthlyReportTemplateUrl;
                        }
                        else if (drpFileType.SelectedValue == "3")
                        {
                            path = Const.ConstructionCommencementReportTemplateUrl;
                        }
                        else if (drpFileType.SelectedValue == "4")
                        {
                            path = Const.ConstructionCompletionReportTemplateUrl;
                        }
                        else if (drpFileType.SelectedValue == "5")
                        {
                            path = Const.ConstructionSpecialReportTemplateUrl;
                        }
                        string uploadfilepath = rootPath + path;
                        string fileName = Path.GetFileName(uploadfilepath);
                        FileInfo fileInfo = new FileInfo(uploadfilepath);
                        FileInfo info = new FileInfo(uploadfilepath);
                        long fileSize = info.Length;
                        Response.Clear();
                        Response.ContentType = "application/octet-stream";
                        Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                        Response.AddHeader("Content-Length", fileSize.ToString());
                        Response.TransmitFile(uploadfilepath, 0, fileSize);
                        Response.Flush();
                    }
                    else
                    {
                        Alert.ShowInTop("请选择文件类型！", MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
        }

        private void BindData()
        {
            var table = ConstructionReportApproveService.getListData(ConstructionReportId);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        public void DoEabled()
        {
            txtCode.Enabled = false;
            txtCompileDate.Enabled = false;
            ContactImg = -1;
        }

        public void DoEdit()
        {
            txtCode.Enabled = true;
            txtCompileDate.Enabled = true;
            ContactImg = 0;
        }
        /// <summary>
        /// 附件内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgBtnFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HFConstructionReportId.Text))   //新增记录
            {
                HFConstructionReportId.Text = SQLHelper.GetNewID(typeof(Model.ZHGL_ConstructionReport));
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(
            String.Format("../../AttachFile/webuploader.aspx?type={0}&toKeyId={1}&path=FileUpload/ConstructionReport&menuId={2}",
            ContactImg, HFConstructionReportId.Text, Const.ConstructionReportMenuId)));
        }

        #region  保存
        /// <summary>
        /// 保存开工报告
        /// </summary>
        private void SavePauseNotice(string saveType)
        {
            Model.ZHGL_ConstructionReport constructionReport = new Model.ZHGL_ConstructionReport();
            constructionReport.Code = txtCode.Text.Trim();
            constructionReport.ProjectId = CurrUser.LoginProjectId;
            if (this.drpFileType.SelectedValue != BLL.Const._Null)
            {
                constructionReport.FileType = this.drpFileType.SelectedValue;
            }
            constructionReport.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
            constructionReport.Content = HttpUtility.HtmlEncode(this.txtContent.Text);
            if (saveType == "submit")
            {
                constructionReport.State = drpHandleType.SelectedValue.Trim();
            }
            else
            {
                Model.ZHGL_ConstructionReport constructionReport1 = ConstructionReportService.GetConstructionReportById(ConstructionReportId);
                if (constructionReport1 != null)
                {
                    if (string.IsNullOrEmpty(constructionReport1.State))
                    {
                        constructionReport.State = Const.ConstructionReport_Compile;
                    }
                    else
                    {
                        constructionReport.State = constructionReport1.State;
                    }
                }
                else
                {
                    constructionReport.State = Const.ConstructionReport_Compile;
                }
            }
            if (!string.IsNullOrEmpty(ConstructionReportId) && ConstructionReportService.GetConstructionReportById(Request.Params["ConstructionReportId"]) != null)
            {
                Model.ZHGL_ConstructionReport constructionReport1 = ConstructionReportService.GetConstructionReportById(ConstructionReportId);
                Model.ZHGL_ConstructionReportApprove approve1 = ConstructionReportApproveService.GetConstructionReportApproveByConstructionReportId(ConstructionReportId);
                if (approve1 != null && saveType == "submit")
                {
                    approve1.IsAgree = Convert.ToBoolean(rblIsAgree.SelectedValue);
                    approve1.ApproveDate = DateTime.Now;
                    approve1.ApproveIdea = txtOpinions.Text.Trim();
                    ConstructionReportApproveService.UpdateConstructionReportApprove(approve1);
                }
                if (saveType == "submit")
                {
                    Model.ZHGL_ConstructionReportApprove approve = new Model.ZHGL_ConstructionReportApprove();
                    approve.ConstructionReportId = constructionReport1.ConstructionReportId;
                    if (drpHandleMan.SelectedValue != "0")
                    {
                        approve.ApproveMan = drpHandleMan.SelectedValue;
                    }
                    approve.ApproveType = drpHandleType.SelectedValue;
                    if (this.drpHandleType.SelectedValue == BLL.Const.ConstructionReport_Complete)
                    {
                        approve.ApproveDate = DateTime.Now.AddMinutes(1);
                    }
                    ConstructionReportApproveService.AddConstructionReportApprove(approve);
                    //APICommonService.SendSubscribeMessage(approve.ApproveMan, "总承包商施工报告待办理", this.CurrUser.UserName, string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now));
                }
                constructionReport.ConstructionReportId = ConstructionReportId;
                ConstructionReportService.UpdateConstructionReport(constructionReport);
            }
            else
            {
                if (!string.IsNullOrEmpty(HFConstructionReportId.Text))
                {
                    constructionReport.ConstructionReportId = HFConstructionReportId.Text;
                }
                else
                {
                    constructionReport.ConstructionReportId = SQLHelper.GetNewID(typeof(Model.ZHGL_ConstructionReport));
                }
                constructionReport.CompileMan = CurrUser.UserId;
                ConstructionReportService.AddConstructionReport(constructionReport);
                if (saveType == "submit")
                {
                    Model.ZHGL_ConstructionReportApprove approve1 = new Model.ZHGL_ConstructionReportApprove();
                    approve1.ConstructionReportId = constructionReport.ConstructionReportId;
                    approve1.ApproveDate = DateTime.Now;
                    approve1.ApproveMan = CurrUser.UserId;
                    approve1.ApproveType = Const.ConstructionReport_Compile;
                    ConstructionReportApproveService.AddConstructionReportApprove(approve1);

                    Model.ZHGL_ConstructionReportApprove approve = new Model.ZHGL_ConstructionReportApprove();
                    approve.ConstructionReportId = constructionReport.ConstructionReportId;
                    if (drpHandleMan.SelectedValue != "0")
                    {
                        approve.ApproveMan = drpHandleMan.SelectedValue;
                    }
                    approve.ApproveType = drpHandleType.SelectedValue;

                    ConstructionReportApproveService.AddConstructionReportApprove(approve);
                    APICommonService.SendSubscribeMessage(approve.ApproveMan, "总承包商施工报告待办理", this.CurrUser.UserName, string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now));
                }
                else
                {
                    Model.ZHGL_ConstructionReportApprove approve1 = new Model.ZHGL_ConstructionReportApprove();
                    approve1.ConstructionReportId = constructionReport.ConstructionReportId;
                    approve1.ApproveMan = CurrUser.UserId;
                    approve1.ApproveType = Const.ConstructionReport_Compile;
                    ConstructionReportApproveService.AddConstructionReportApprove(approve1);
                }
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            LogService.AddSys_Log(CurrUser, constructionReport.Code, ConstructionReportId, Const.ConstructionReportMenuId, "总承包商施工报告");
        }
        #endregion

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, CurrUser.UserId, Const.ConstructionReportMenuId, Const.BtnSubmit))
            {
                if (this.drpFileType.SelectedValue == BLL.Const._Null)
                {
                    Alert.ShowInTop("请选择文件类型！", MessageBoxIcon.Warning);
                    return;
                }
                SavePauseNotice("submit");

            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, CurrUser.UserId, Const.ConstructionReportMenuId, Const.BtnSave))
            {
                SavePauseNotice("save");

                //Response.Redirect("/check/CheckList.aspx");

            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        protected void rblIsAgree_SelectedIndexChanged(object sender, EventArgs e)
        {
            Agree();
        }
        /// <summary>
        /// 是否同意的逻辑处理
        /// </summary>
        public void Agree()
        {
            drpHandleType.Items.Clear();
            string State = ConstructionReportService.GetConstructionReportById(ConstructionReportId).State;
            ConstructionReportService.InitHandleType(drpHandleType, false, State);
            if (rblIsAgree.SelectedValue.Equals("true"))
            {
                drpHandleType.SelectedIndex = 0;
                //if (drpHandleType.SelectedValue != Const.ConstructionReport_Audit2)
                //{
                    UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, Const.UnitId_SEDIN);
                //}
                //else
                //{
                //    UserService.InitUserProjectIdUnitIdRoleIdDropDownList(drpHandleMan, string.Empty, Const.UnitId_SEDIN, Const.SystemManager, false);
                //}
                drpHandleMan.SelectedIndex = 0;
                if (drpHandleType.SelectedValue == Const.ConstructionReport_Complete)
                {
                    drpHandleMan.Items.Clear();
                    drpHandleMan.Enabled = false;
                    drpHandleMan.Required = false;
                }
                else
                {
                    drpHandleMan.Enabled = true;
                    drpHandleMan.Required = true;
                }
            }
            else
            {
                drpHandleMan.Items.Clear();
                drpHandleType.SelectedIndex = 1;
                //if (drpHandleType.SelectedValue != Const.ConstructionReport_Audit2)
                //{
                    UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, Const.UnitId_SEDIN);
                //}
                //else
                //{
                //    UserService.InitUserProjectIdUnitIdRoleIdDropDownList(drpHandleMan, string.Empty, Const.UnitId_SEDIN, Const.SystemManager, false);
                //}
                drpHandleMan.SelectedIndex = 0;
                if (drpHandleType.SelectedValue == Const.ConstructionReport_ReCompile)
                {
                    drpHandleMan.Enabled = true;
                    var HandleMan = BLL.ConstructionReportApproveService.GetComplie(this.ConstructionReportId);                    if (HandleMan != null)                    {                        this.drpHandleMan.SelectedValue = HandleMan.ApproveMan;                    }
                    drpHandleMan.Required = true;
                }
                else
                {
                    drpHandleMan.Enabled = true;
                    drpHandleMan.Required = true;

                }
            }
        }
    }
}