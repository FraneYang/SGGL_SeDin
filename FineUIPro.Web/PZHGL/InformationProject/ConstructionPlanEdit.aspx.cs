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
    public partial class ConstructionPlanEdit : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string ConstructionPlanId
        {
            get
            {
                return (string)ViewState["ConstructionPlanId"];
            }
            set
            {
                ViewState["ConstructionPlanId"] = value;
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
                ConstructionPlanId = Request.Params["ConstructionPlanId"];
                HideOptions.Hidden = true;
                rblIsAgree.Hidden = true;
                BindData();
                if (!string.IsNullOrEmpty(ConstructionPlanId))
                {
                    HFConstructionPlanId.Text = ConstructionPlanId;
                    Model.ZHGL_ConstructionPlan constructionPlan = ConstructionPlanService.GetConstructionPlanById(ConstructionPlanId);
                    string unitType = string.Empty;
                    txtCode.Text = constructionPlan.Code;
                    if (constructionPlan.CompileDate != null)
                    {
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", constructionPlan.CompileDate);
                    }
                    this.txtContent.Text = HttpUtility.HtmlDecode(constructionPlan.Content);
                    if (!string.IsNullOrEmpty(constructionPlan.State))
                    {
                        State = constructionPlan.State;
                    }
                    else
                    {
                        State = Const.ConstructionPlan_Compile;
                        HideOptions.Hidden = true;
                        //Url.Visible = false;//附件查看权限-1
                        ContactImg = -1;
                        rblIsAgree.Hidden = true;
                    }
                    if (State != Const.ConstructionPlan_Complete)
                    {
                        ConstructionPlanService.InitHandleType(drpHandleType, false, State);
                    }
                    if (State == Const.ConstructionPlan_Compile || State == Const.ConstructionPlan_ReCompile)
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
                    if (drpHandleType.SelectedValue == Const.ConstructionPlan_Complete)
                    {
                        rblIsAgree.Hidden = false;
                        drpHandleMan.Enabled = false;
                        drpHandleMan.Required = false;

                    }
                    else
                    {
                        drpHandleMan.Items.Clear();
                        if (State != Const.ConstructionPlan_Audit1)
                        {
                            UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, Const.UnitId_SEDIN);
                        }
                        else
                        {
                            UserService.InitUserProjectIdUnitIdRoleIdDropDownList(drpHandleMan, string.Empty, Const.UnitId_SEDIN, Const.SystemManager, false);
                        }
                        drpHandleMan.Enabled = true;
                        drpHandleMan.Required = true;
                    }
                    if (rblIsAgree.Hidden == false)
                    {
                        Agree();
                    }
                    if (State == Const.ConstructionPlan_Compile || State == Const.ConstructionPlan_ReCompile)
                    {
                        HideOptions.Hidden = true;
                    }
                    //设置回复审批场景下的操作
                }
                else
                {
                    State = Const.ConstructionPlan_Compile;
                    ConstructionPlanService.InitHandleType(drpHandleType, false, State);
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
                    string path = Const.ConstructionPlanTemplateUrl;
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
            }
        }

        private void BindData()
        {
            var table = ConstructionPlanApproveService.getListData(ConstructionPlanId);
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
            if (string.IsNullOrEmpty(HFConstructionPlanId.Text))   //新增记录
            {
                HFConstructionPlanId.Text = SQLHelper.GetNewID(typeof(Model.ZHGL_ConstructionPlan));
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(
            String.Format("../../AttachFile/webuploader.aspx?type={0}&toKeyId={1}&path=FileUpload/ConstructionPlan&menuId={2}",
            ContactImg, HFConstructionPlanId.Text, Const.ConstructionPlanMenuId)));
        }

        #region  保存
        /// <summary>
        /// 保存开工报告
        /// </summary>
        private void SavePauseNotice(string saveType)
        {
            Model.ZHGL_ConstructionPlan constructionPlan = new Model.ZHGL_ConstructionPlan();
            constructionPlan.Code = txtCode.Text.Trim();
            constructionPlan.ProjectId = CurrUser.LoginProjectId;
            constructionPlan.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
            constructionPlan.Content = HttpUtility.HtmlEncode(this.txtContent.Text);
            if (saveType == "submit")
            {
                constructionPlan.State = drpHandleType.SelectedValue.Trim();
            }
            else
            {
                Model.ZHGL_ConstructionPlan constructionPlan1 = ConstructionPlanService.GetConstructionPlanById(ConstructionPlanId);
                if (constructionPlan1 != null)
                {
                    if (string.IsNullOrEmpty(constructionPlan1.State))
                    {
                        constructionPlan.State = Const.ConstructionPlan_Compile;
                    }
                    else
                    {
                        constructionPlan.State = constructionPlan1.State;
                    }
                }
                else
                {
                    constructionPlan.State = Const.ConstructionPlan_Compile;
                }
            }
            if (!string.IsNullOrEmpty(ConstructionPlanId) && ConstructionPlanService.GetConstructionPlanById(Request.Params["ConstructionPlanId"]) != null)
            {
                Model.ZHGL_ConstructionPlan constructionPlan1 = ConstructionPlanService.GetConstructionPlanById(ConstructionPlanId);
                Model.ZHGL_ConstructionPlanApprove approve1 = ConstructionPlanApproveService.GetConstructionPlanApproveByConstructionPlanId(ConstructionPlanId);
                if (approve1 != null && saveType == "submit")
                {
                    approve1.IsAgree = Convert.ToBoolean(rblIsAgree.SelectedValue);
                    approve1.ApproveDate = DateTime.Now;
                    approve1.ApproveIdea = txtOpinions.Text.Trim();
                    ConstructionPlanApproveService.UpdateConstructionPlanApprove(approve1);
                }
                if (saveType == "submit")
                {
                    Model.ZHGL_ConstructionPlanApprove approve = new Model.ZHGL_ConstructionPlanApprove();
                    approve.ConstructionPlanId = constructionPlan1.ConstructionPlanId;
                    if (drpHandleMan.SelectedValue != "0")
                    {
                        approve.ApproveMan = drpHandleMan.SelectedValue;
                    }
                    approve.ApproveType = drpHandleType.SelectedValue;
                    if (this.drpHandleType.SelectedValue == BLL.Const.ConstructionPlan_Complete)
                    {
                        approve.ApproveDate = DateTime.Now.AddMinutes(1);
                    }
                    ConstructionPlanApproveService.AddConstructionPlanApprove(approve);
                    //APICommonService.SendSubscribeMessage(approve.ApproveMan, "总承包商施工计划待办理", this.CurrUser.UserName, string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now));
                }
                constructionPlan.ConstructionPlanId = ConstructionPlanId;
                ConstructionPlanService.UpdateConstructionPlan(constructionPlan);
            }
            else
            {
                if (!string.IsNullOrEmpty(HFConstructionPlanId.Text))
                {
                    constructionPlan.ConstructionPlanId = HFConstructionPlanId.Text;
                }
                else
                {
                    constructionPlan.ConstructionPlanId = SQLHelper.GetNewID(typeof(Model.ZHGL_ConstructionPlan));
                }
                constructionPlan.CompileMan = CurrUser.UserId;
                ConstructionPlanService.AddConstructionPlan(constructionPlan);
                if (saveType == "submit")
                {
                    Model.ZHGL_ConstructionPlanApprove approve1 = new Model.ZHGL_ConstructionPlanApprove();
                    approve1.ConstructionPlanId = constructionPlan.ConstructionPlanId;
                    approve1.ApproveDate = DateTime.Now;
                    approve1.ApproveMan = CurrUser.UserId;
                    approve1.ApproveType = Const.ConstructionPlan_Compile;
                    ConstructionPlanApproveService.AddConstructionPlanApprove(approve1);

                    Model.ZHGL_ConstructionPlanApprove approve = new Model.ZHGL_ConstructionPlanApprove();
                    approve.ConstructionPlanId = constructionPlan.ConstructionPlanId;
                    if (drpHandleMan.SelectedValue != "0")
                    {
                        approve.ApproveMan = drpHandleMan.SelectedValue;
                    }
                    approve.ApproveType = drpHandleType.SelectedValue;

                    ConstructionPlanApproveService.AddConstructionPlanApprove(approve);
                    APICommonService.SendSubscribeMessage(approve.ApproveMan, "总承包商施工计划待办理", this.CurrUser.UserName, string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now));
                }
                else
                {
                    Model.ZHGL_ConstructionPlanApprove approve1 = new Model.ZHGL_ConstructionPlanApprove();
                    approve1.ConstructionPlanId = constructionPlan.ConstructionPlanId;
                    approve1.ApproveMan = CurrUser.UserId;
                    approve1.ApproveType = Const.ConstructionPlan_Compile;
                    ConstructionPlanApproveService.AddConstructionPlanApprove(approve1);
                }
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            LogService.AddSys_Log(CurrUser, constructionPlan.Code, ConstructionPlanId, Const.ConstructionPlanMenuId, "总承包商施工计划");
        }
        #endregion

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, CurrUser.UserId, Const.ConstructionPlanMenuId, Const.BtnSubmit))
            {
                SavePauseNotice("submit");

            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, CurrUser.UserId, Const.ConstructionPlanMenuId, Const.BtnSave))
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
            string State = ConstructionPlanService.GetConstructionPlanById(ConstructionPlanId).State;
            ConstructionPlanService.InitHandleType(drpHandleType, false, State);
            if (rblIsAgree.SelectedValue.Equals("true"))
            {
                drpHandleType.SelectedIndex = 0;
                if (drpHandleType.SelectedValue != Const.ConstructionPlan_Audit2)
                {
                    UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, Const.UnitId_SEDIN);
                }
                else
                {
                    UserService.InitUserProjectIdUnitIdRoleIdDropDownList(drpHandleMan, string.Empty, Const.UnitId_SEDIN, Const.SystemManager, false);
                }
                drpHandleMan.SelectedIndex = 0;
                if (drpHandleType.SelectedValue == Const.ConstructionPlan_Complete)
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
                if (drpHandleType.SelectedValue != Const.ConstructionPlan_Audit2)
                {
                    UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, Const.UnitId_SEDIN);
                }
                else
                {
                    UserService.InitUserProjectIdUnitIdRoleIdDropDownList(drpHandleMan, string.Empty, Const.UnitId_SEDIN, Const.SystemManager, false);
                }
                drpHandleMan.SelectedIndex = 0;
                if (drpHandleType.SelectedValue == Const.ConstructionPlan_ReCompile)
                {
                    drpHandleMan.Enabled = true;
                    var HandleMan = BLL.ConstructionPlanApproveService.GetComplie(this.ConstructionPlanId);                    if (HandleMan != null)                    {                        this.drpHandleMan.SelectedValue = HandleMan.ApproveMan;                    }
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