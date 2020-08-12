using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.CQMS.Check
{
    public partial class EditDesign : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string DesignId
        {
            get
            {
                return (string)ViewState["DesignId"];
            }
            set
            {
                ViewState["DesignId"] = value;
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
        /// <summary>
        /// 附件
        /// </summary>
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DesignId = Request.Params["DesignId"];
                if (!string.IsNullOrEmpty(DesignId))
                {
                    BindGrid();
                }
                HandleImg = 0;
                txtProjectName.Text = ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId).ProjectName;
                BLL.MainItemService.InitMainItemDownList(drpMainItem, this.CurrUser.LoginProjectId, false);//主项
                this.drpDesignType.DataTextField = "Text";
                this.drpDesignType.DataValueField = "Value";
                drpDesignType.DataSource = BLL.DesignService.GetDesignTypeList();
                drpDesignType.DataBind();
                //Funs.FineUIPleaseSelect(drpDesignType);//变更类型
                DesignProfessionalService.InitDesignProfessionalDownList(drpCNProfessional, false);//专业
                Funs.FineUIPleaseSelect(drpHandleMan);
                this.HideOptions.Hidden = true;
                this.rblIsAgree.Hidden = true;
                gvCarryUnit.DataSource = BLL.UnitService.GetUnitByProjectIdList(this.CurrUser.LoginProjectId);
                gvCarryUnit.DataBind();
                gvBuyMaterialUnit.DataSource = BLL.UnitService.GetUnitByProjectIdList(this.CurrUser.LoginProjectId);
                gvBuyMaterialUnit.DataBind();
                if (!string.IsNullOrEmpty(DesignId))
                {
                    this.hdCheckDesignCode.Text = DesignId;
                    Model.Check_Design design = BLL.DesignService.GetDesignByDesignId(DesignId);
                    string unitType = string.Empty;
                    this.txtDesignCode.Text = design.DesignCode;
                    if (!string.IsNullOrEmpty(design.DesignType))
                    {
                        this.drpDesignType.SelectedValue = design.DesignType;
                    }
                    if (design.MainItemId != null)
                    {
                        this.drpMainItem.SelectedValue = design.MainItemId;
                    }
                    if (!string.IsNullOrEmpty(design.CNProfessionalCode))
                    {
                        this.drpCNProfessional.SelectedValue = design.CNProfessionalCode;
                    }
                    if (design.DesignDate != null)
                    {
                        this.txtDesignDate.Text = string.Format("{0:yyyy-MM-dd}", design.DesignDate);
                    }
                    if (!string.IsNullOrEmpty(design.CarryUnitIds))
                    {
                        txtCarryUnit.Values = design.CarryUnitIds.Split(',');
                    }
                    if (design.IsNoChange == true || design.IsNoChange == null)
                    {
                        this.rblIsNoChange.SelectedValue = "true";
                    }
                    else
                    {
                        this.rblIsNoChange.SelectedValue = "false";
                    }
                    if (design.IsNeedMaterial == true || design.IsNeedMaterial == null)
                    {
                        this.rblIsNeedMaterial.SelectedValue = "true";
                    }
                    else
                    {
                        this.rblIsNeedMaterial.SelectedValue = "false";
                    }
                    if (!string.IsNullOrEmpty(design.BuyMaterialUnitIds))
                    {
                        txtBuyMaterialUnit.Values = design.BuyMaterialUnitIds.Split(',');
                    }
                    if (design.MaterialPlanReachDate != null)
                    {
                        this.txtMaterialPlanReachDate.Text = string.Format("{0:yyyy-MM-dd}", design.MaterialPlanReachDate);
                    }
                    if (design.PlanDay != null)
                    {
                        this.txtPlanDay.Text = design.PlanDay.ToString();
                    }
                    if (design.PlanCompleteDate != null)
                    {
                        this.txtPlanCompleteDate.Text = string.Format("{0:yyyy-MM-dd}", design.PlanCompleteDate);
                    }
                    if (design.MaterialRealReachDate != null)
                    {
                        this.txtMaterialRealReachDate.Text = string.Format("{0:yyyy-MM-dd}", design.MaterialRealReachDate);
                    }
                    if (design.RealCompleteDate != null)
                    {
                        this.txtRealCompleteDate.Text = string.Format("{0:yyyy-MM-dd}", design.RealCompleteDate);
                    }
                    this.txtDesignContents.Text = design.DesignContents;
                    if (!string.IsNullOrEmpty(design.State))
                    {
                        State = design.State;
                    }
                    else
                    {
                        State = BLL.Const.Design_Compile;
                        this.HideOptions.Hidden = true;
                        this.rblIsAgree.Hidden = true;
                    }
                    if (State != BLL.Const.Design_Complete.ToString())
                    {
                        this.drpHandleType.DataTextField = "Text";
                        this.drpHandleType.DataValueField = "Value";
                        drpHandleType.DataSource = BLL.DesignService.GetDHandleTypeByState(State);
                        drpHandleType.DataBind();

                    }
                    if (State == BLL.Const.Design_Compile || State == BLL.Const.Design_ReCompile)
                    {
                        this.HideOptions.Visible = false;
                        this.rblIsAgree.Visible = false;
                        this.drpHandleMan.Enabled = true;
                        this.drpHandleMan.DataTextField = "UserName";
                        this.drpHandleMan.DataValueField = "UserId";
                        this.drpHandleMan.DataSource = BLL.UserService.GetProjectUserListByProjectId(this.CurrUser.LoginProjectId);
                        this.drpHandleMan.DataBind();
                    }
                    else
                    {
                        this.drpHandleMan.DataTextField = "UserName";
                        this.drpHandleMan.DataValueField = "UserId";
                        this.drpHandleMan.DataSource = BLL.UserService.GetProjectUserListByProjectId(this.CurrUser.LoginProjectId);
                        this.drpHandleMan.DataBind();
                        this.HideOptions.Hidden = false;
                        this.rblIsAgree.Hidden = false;
                    }
                    if (State == Const.Design_Audit4)
                    {
                        this.drpHandleMan.Enabled = false;
                    }
                    if (State == BLL.Const.Design_Complete || !string.IsNullOrEmpty(Request.Params["see"]))
                    {
                        this.btnSave.Visible = false;
                        this.btnSubmit.Visible = false;
                        this.next.Hidden = true;
                    }
                    if (State == Const.Design_Audit1 || State == Const.Design_Audit3)
                    {
                        this.rblIsAgree.Visible = false;
                    }
                }
                else
                {
                    State = Const.Design_Compile;
                    this.drpHandleType.DataTextField = "Text";
                    this.drpHandleType.DataValueField = "Value";
                    drpHandleType.DataSource = BLL.DesignService.GetDHandleTypeByState(State);
                    drpHandleType.DataBind();
                    this.drpHandleMan.DataTextField = "UserName";
                    this.drpHandleMan.DataValueField = "UserId";
                    this.drpHandleMan.DataSource = BLL.UserService.GetProjectUserListByProjectId(this.CurrUser.LoginProjectId);
                    this.drpHandleMan.DataBind();
                    plApprove2.Hidden = true;
                }
                if (State == Const.Design_Compile || State == Const.Design_ReCompile)
                {
                    //变更分析
                    this.txtCarryUnit.Enabled = false;
                    this.rblIsNoChange.Enabled = false;
                    this.rblIsNeedMaterial.Enabled = false;
                    this.txtBuyMaterialUnit.Enabled = false;
                    this.txtMaterialPlanReachDate.Enabled = false;
                    this.txtPlanDay.Enabled = false;
                    this.txtPlanCompleteDate.Enabled = false;
                    //变更实施
                    this.txtMaterialRealReachDate.Enabled = false;
                    this.txtRealCompleteDate.Enabled = false;
                }
                else if (State == Const.Design_Audit1 || State == Const.Design_Audit2)
                {
                    //变更信息
                    this.txtProjectName.Enabled = false;
                    this.drpMainItem.Enabled = false;
                    this.drpCNProfessional.Enabled = false;
                    this.txtDesignCode.Enabled = false;
                    this.drpDesignType.Enabled = false;
                    this.txtDesignDate.Enabled = false;
                    this.txtDesignContents.Enabled = false;
                    HandleImg = -1;
                    if (this.rblIsNeedMaterial.SelectedValue == "true")
                    {
                        this.txtBuyMaterialUnit.Enabled = true;
                        this.txtMaterialPlanReachDate.ShowRedStar = true;
                        this.txtMaterialPlanReachDate.Required = true;
                    }
                    else
                    {
                        this.txtBuyMaterialUnit.Enabled = false;
                        this.txtMaterialPlanReachDate.Enabled = false;
                        this.txtMaterialPlanReachDate.ShowRedStar = false;
                        this.txtMaterialPlanReachDate.Required = false;
                    }
                    this.txtPlanDay.Enabled = true;
                    this.txtPlanCompleteDate.Enabled = true;
                    //变更实施
                    this.txtMaterialRealReachDate.Enabled = false;
                    this.txtRealCompleteDate.Enabled = false;
                }
                else if (State == Const.Design_Audit3 || State == Const.Design_Audit4)
                {
                    //变更信息
                    if (State == Const.Design_Audit3)
                    {
                        drpHandleMan.SelectedIndex = 1;
                    }
                    else
                    {
                        Funs.FineUIPleaseSelect(drpHandleMan);
                        drpHandleMan.SelectedIndex = 0;
                    }
                    this.txtProjectName.Enabled = false;
                    this.drpMainItem.Enabled = false;
                    this.drpCNProfessional.Enabled = false;
                    this.txtDesignCode.Enabled = false;
                    this.drpDesignType.Enabled = false;
                    this.txtDesignDate.Enabled = false;
                    this.txtDesignContents.Enabled = false;
                    HandleImg = -1;
                    //变更分析
                    this.txtCarryUnit.Enabled = false;
                    this.rblIsNoChange.Enabled = false;
                    this.rblIsNeedMaterial.Enabled = false;
                    this.txtBuyMaterialUnit.Enabled = false;
                    this.txtMaterialPlanReachDate.Enabled = false;
                    this.txtPlanDay.Enabled = false;
                    this.txtPlanCompleteDate.Enabled = false;
                    //变更实施
                    if (this.rblIsNeedMaterial.SelectedValue == "false")   //不需要增补材料
                    {
                        this.txtMaterialRealReachDate.ShowRedStar = false;
                        this.txtMaterialRealReachDate.Enabled = false;
                    }
                    else
                    {
                        this.txtMaterialRealReachDate.ShowRedStar = true;
                        this.txtMaterialRealReachDate.Enabled = true;
                    }
                    this.txtRealCompleteDate.Enabled = true;
                }
                Model.Check_Design design1 = BLL.DesignService.GetDesignByDesignId(DesignId);
                if (design1 != null && !string.IsNullOrEmpty(design1.SaveHandleMan))
                {
                    this.drpHandleMan.SelectedValue = design1.SaveHandleMan;
                }
            }
        }
        private void BindGrid()
        {
            string strSql = "select D.DesignApproveId,DesignId,U.UserName, ApproveDate,ApproveIdea,D.ApproveType  from Check_DesignApprove D left join Sys_user U on D.ApproveMan = U.UserId where DesignId=@DesignId and  D.ApproveDate is not null";

            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@DesignId", DesignId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            gvApprove.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(gvApprove.FilteredData, tb);
            var table = this.GetPagedDataTable(gvApprove, tb);

            gvApprove.DataSource = table;
            gvApprove.DataBind();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.DesignMenuId, BLL.Const.BtnSubmit))
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
            if (BLL.CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.DesignMenuId, BLL.Const.BtnSave))
            {
                SavePauseNotice("save");
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }
        /// <summary>
        /// 保存开工报告
        /// </summary>
        private void SavePauseNotice(string saveType)
        {
            Model.Check_Design design = new Model.Check_Design();
            design.DesignCode = this.txtDesignCode.Text.Trim();
            design.ProjectId = this.CurrUser.LoginProjectId;
            if (this.drpDesignType.SelectedValue != BLL.Const._Null)
            {
                design.DesignType = this.drpDesignType.SelectedValue;
            }
            else
            {
                Alert.ShowInTop("变更类型不能为空！", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpMainItem.SelectedValue != BLL.Const._Null)
            {
                design.MainItemId = this.drpMainItem.SelectedValue;
            }
            else
            {
                Alert.ShowInTop("主项不能为空！", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpCNProfessional.SelectedValue != BLL.Const._Null)
            {
                design.CNProfessionalCode = this.drpCNProfessional.SelectedValue;
            }
            else
            {
                Alert.ShowInTop("专业不能为空！", MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrEmpty(this.txtDesignDate.Text.Trim()))
            {
                design.DesignDate = Convert.ToDateTime(this.txtDesignDate.Text.Trim());
            }
            if (!string.IsNullOrWhiteSpace(String.Join(",", this.txtCarryUnit.Values)))
            {
                design.CarryUnitIds = string.Join(",", txtCarryUnit.Values);
            }
            if (State != BLL.Const.Design_Compile.ToString() && State != BLL.Const.Design_ReCompile.ToString())
            {
                if (!string.IsNullOrEmpty(this.rblIsNoChange.SelectedValue))
                {
                    design.IsNoChange = Convert.ToBoolean(this.rblIsNoChange.SelectedValue);
                }
                else
                {
                    design.IsNoChange = null;
                }
                if (!string.IsNullOrEmpty(this.rblIsNeedMaterial.SelectedValue))
                {
                    design.IsNeedMaterial = Convert.ToBoolean(this.rblIsNeedMaterial.SelectedValue);
                }
                else
                {
                    design.IsNeedMaterial = null;
                }
            }
            if (!string.IsNullOrWhiteSpace(String.Join(",", this.txtBuyMaterialUnit.Value)))
            {
                design.BuyMaterialUnitIds = string.Join(",", txtBuyMaterialUnit.Values);
            }
            if (!string.IsNullOrEmpty(this.txtMaterialPlanReachDate.Text.Trim()))
            {
                design.MaterialPlanReachDate = Convert.ToDateTime(this.txtMaterialPlanReachDate.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtPlanDay.Text.Trim()))
            {
                design.PlanDay = Convert.ToDecimal(this.txtPlanDay.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtPlanCompleteDate.Text.Trim()))
            {
                design.PlanCompleteDate = Convert.ToDateTime(this.txtPlanCompleteDate.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtMaterialRealReachDate.Text.Trim()))
            {
                design.MaterialRealReachDate = Convert.ToDateTime(this.txtMaterialRealReachDate.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtRealCompleteDate.Text.Trim()))
            {
                design.RealCompleteDate = Convert.ToDateTime(this.txtRealCompleteDate.Text.Trim());
            }
            design.DesignContents = this.txtDesignContents.Text.Trim();
            if (saveType == "submit")
            {
                design.State = drpHandleType.SelectedValue.Trim();
            }
            else
            {
                Model.Check_Design design1 = BLL.DesignService.GetDesignByDesignId(DesignId);
                if (design1 != null)
                {
                    if (string.IsNullOrEmpty(design1.State))
                    {
                        design.State = BLL.Const.Design_Compile;
                    }
                    else
                    {
                        design.State = design1.State;
                    }
                }
                else
                {
                    design.State = BLL.Const.Design_Compile;
                }
            }
            if (!string.IsNullOrEmpty(DesignId))
            {
                Model.Check_Design design1 = BLL.DesignService.GetDesignByDesignId(DesignId);
                Model.Check_DesignApprove approve1 = BLL.DesignApproveService.GetDesignApproveByDesignId(DesignId);
                if (approve1 != null && saveType == "submit")
                {
                    approve1.ApproveDate = DateTime.Now;
                    approve1.ApproveIdea = this.txtOpinions.Text.Trim();
                    BLL.DesignApproveService.UpdateDesignApprove(approve1);
                }
                if (saveType == "submit")
                {
                    design.SaveHandleMan = null;
                    Model.Check_DesignApprove approve = new Model.Check_DesignApprove();
                    approve.DesignId = design1.DesignId;
                    if (this.drpHandleMan.SelectedValue != BLL.Const._Null)
                    {
                        approve.ApproveMan = this.drpHandleMan.SelectedValue;
                    }
                    approve.ApproveType = this.drpHandleType.SelectedValue;
                    BLL.DesignApproveService.AddDesignApprove(approve);
                    APICommonService.SendSubscribeMessage(approve.ApproveMan, "设计变更待办理", this.CurrUser.UserName, string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now));
                }
                if (saveType == "save")
                {
                    design.SaveHandleMan = this.drpHandleMan.SelectedValue;
                }
                design.DesignId = DesignId;
                BLL.DesignService.UpdateDesign(design);
            }
            else
            {
                if (!string.IsNullOrEmpty(this.hdCheckDesignCode.Text))
                {
                    design.DesignId = this.hdCheckDesignCode.Text;
                }
                else
                {
                    design.DesignId = SQLHelper.GetNewID(typeof(Model.Check_Design));
                }
                if (saveType == "save")
                {
                    design.SaveHandleMan = this.drpHandleMan.SelectedValue;
                }
                design.CompileMan = this.CurrUser.UserId;
                design.CompileDate = DateTime.Now;
                BLL.DesignService.AddDesign(design);
                DesignId = design.DesignId;
                Model.Check_Design design1 = BLL.DesignService.GetDesignByDesignId(DesignId);

                if (saveType == "submit")
                {
                    Model.Check_DesignApprove approve1 = new Model.Check_DesignApprove();
                    approve1.DesignId = design.DesignId;
                    approve1.ApproveDate = DateTime.Now;
                    approve1.ApproveMan = this.CurrUser.UserId;
                    approve1.ApproveType = BLL.Const.Design_Compile;
                    BLL.DesignApproveService.AddDesignApprove(approve1);

                    Model.Check_DesignApprove approve = new Model.Check_DesignApprove();
                    approve.DesignId = design.DesignId;
                    if (this.drpHandleMan.SelectedValue != BLL.Const._Null)
                    {
                        approve.ApproveMan = this.drpHandleMan.SelectedValue;
                    }
                    approve.ApproveType = this.drpHandleType.SelectedValue;
                    if (this.drpHandleType.SelectedValue == BLL.Const.Design_Complete)
                    {
                        approve.ApproveDate = DateTime.Now.AddMinutes(1);
                    }
                    BLL.DesignApproveService.AddDesignApprove(approve);
                    APICommonService.SendSubscribeMessage(approve.ApproveMan, "设计变更待办理", this.CurrUser.UserName, string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now));
                }
                else
                {
                    Model.Check_DesignApprove approve1 = new Model.Check_DesignApprove();
                    approve1.DesignId = design.DesignId;
                    approve1.ApproveMan = this.CurrUser.UserId;
                    approve1.ApproveType = BLL.Const.Design_Compile;
                    BLL.DesignApproveService.AddDesignApprove(approve1);
                }
            }

            ShowNotify("提交成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        protected void btnAttach_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.hdCheckDesignCode.Text))   //新增记录
            {
                this.hdCheckDesignCode.Text = SQLHelper.GetNewID(typeof(Model.Check_Design));
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?type={0}&toKeyId={1}&path=FileUpload/Design&menuId={2}", HandleImg, this.hdCheckDesignCode.Text, BLL.Const.DesignMenuId)));
        }

        /// <summary>
        /// 把状态转换代号为文字形式
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertState(object state)
        {
            if (state != null)
            {
                if (state.ToString() == BLL.Const.Design_ReCompile)
                {
                    return "重新编制";
                }
                else if (state.ToString() == BLL.Const.Design_Compile)
                {
                    return "变更录入";
                }
                else if (state.ToString() == BLL.Const.Design_Audit1)
                {
                    return "变更分析";
                }
                else if (state.ToString() == BLL.Const.Design_Audit2)
                {
                    return "变更分析审核";
                }
                else if (state.ToString() == BLL.Const.Design_Audit3)
                {
                    return "变更实施";
                }
                else if (state.ToString() == BLL.Const.Design_Audit4)
                {
                    return "变更实施审核";
                }
                else if (state.ToString() == BLL.Const.Design_Complete)
                {
                    return "审批";
                }
                else
                {
                    return "";
                }
            }
            return "";
        }
        /// <summary>
        /// 办理步骤下拉框改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpHandleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpHandleMan.Items.Clear();
            Funs.FineUIPleaseSelect(this.drpHandleMan);
            if (this.drpHandleType.SelectedValue == BLL.Const.Design_Complete)
            {
                this.drpHandleMan.Enabled = false;
            }
            else if (this.drpHandleType.SelectedValue == BLL.Const.Design_ReCompile)
            {
                this.drpHandleMan.Enabled = true;
                string userId = (from x in Funs.DB.Check_DesignApprove where x.DesignId == DesignId && x.ApproveType == BLL.Const.Design_Compile select x.ApproveMan).First();
                ListItem lis = new ListItem(BLL.UserService.GetUserByUserId(userId).UserName, userId);
                this.drpHandleMan.Items.Add(lis);
                this.drpHandleMan.SelectedIndex = 0;
            }
            else
            {
                this.drpHandleMan.Enabled = true;
                this.drpHandleMan.DataTextField = "UserName";
                this.drpHandleMan.DataValueField = "UserId";
                this.drpHandleMan.DataSource = BLL.UserService.GetProjectUserListByProjectId(this.CurrUser.LoginProjectId);
                this.drpHandleMan.DataBind();
                this.drpHandleMan.SelectedIndex = 1;
            }
        }
        /// <summary>
        /// 增补材料单选框改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblIsNeedMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rblIsNeedMaterial.SelectedValue == "true")
            {
                this.txtBuyMaterialUnit.Enabled = true;
                this.txtMaterialPlanReachDate.Enabled = true;
            }
            else
            {
                this.txtBuyMaterialUnit.Values = new string[] { };
                this.txtBuyMaterialUnit.EmptyText = "--请选择--";
                this.txtBuyMaterialUnit.Enabled = false;
                this.txtMaterialPlanReachDate.Text = "";
                this.txtMaterialPlanReachDate.Enabled = false;
            }
            if (!string.IsNullOrWhiteSpace(DesignId))
            {
                Model.Check_Design Design = DesignService.GetDesignByDesignId(DesignId);
                if (Design.CarryUnitIds != null)
                {
                    txtCarryUnit.Values = Design.CarryUnitIds.Split(',');

                }
                if (Design.BuyMaterialUnitIds != null)
                {
                    if (this.rblIsNeedMaterial.SelectedValue == "true")
                    {
                        txtBuyMaterialUnit.Values = Design.BuyMaterialUnitIds.Split(',');
                        this.txtMaterialPlanReachDate.Text = string.Format("{0:yyyy-MM-dd}", Design.MaterialPlanReachDate);
                    }

                }

            }
        }

        /// <summary>
        /// 材料预计到齐时间文本框改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtMaterialPlanReachDate_TextChanged(object sender, EventArgs e)
        {
            this.txtPlanCompleteDate.Text = string.Empty;
            if (!string.IsNullOrEmpty(this.txtPlanDay.Text.Trim()))
            {
                try
                {
                    decimal d = Convert.ToDecimal(this.txtPlanDay.Text.Trim());
                    int i = 0;
                    if (this.txtPlanDay.Text.Contains("."))  //有小数
                    {
                        i = (Int32)d + 1;
                    }
                    else
                    {
                        i = (Int32)d;
                    }
                    if (this.rblIsNeedMaterial.SelectedValue == "true")  //需要增补材料
                    {
                        if (!string.IsNullOrEmpty(this.txtMaterialPlanReachDate.Text.Trim()))
                        {
                            DateTime date = Convert.ToDateTime(this.txtMaterialPlanReachDate.Text.Trim());
                            date = date.AddDays(i);
                            this.txtPlanCompleteDate.Text = string.Format("{0:yyyy-MM-dd}", date);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.txtDesignDate.Text.Trim()))
                        {
                            DateTime date = Convert.ToDateTime(this.txtDesignDate.Text.Trim());
                            date = date.AddDays(i);
                            this.txtPlanCompleteDate.Text = string.Format("{0:yyyy-MM-dd}", date);
                        }
                    }
                }
                catch (Exception)
                {
                    this.txtPlanDay.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('预计施工周期只能录入整数或小数！')", true);
                }
            }
            if (!string.IsNullOrWhiteSpace(DesignId))
            {
                Model.Check_Design Design = DesignService.GetDesignByDesignId(DesignId);
                if (Design.CarryUnitIds != null)
                {
                    txtCarryUnit.Values = Design.CarryUnitIds.Split(',');

                }
                if (Design.BuyMaterialUnitIds != null)
                {
                    if (this.rblIsNeedMaterial.SelectedValue == "true")
                    {
                        txtBuyMaterialUnit.Values = Design.BuyMaterialUnitIds.Split(',');
                    }

                }

            }
        }
        /// <summary>
        /// 同意审核单选框改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string contactListType = this.rblIsNoChange.SelectedValue;
            string isReply = this.rblIsNeedMaterial.SelectedValue;
            this.drpHandleMan.Enabled = true;
            string State = BLL.DesignService.GetDesignByDesignId(DesignId).State;
            if (this.RadioButtonList1.SelectedValue.Equals("true"))
            {
                if (State == Const.Design_Audit4)
                {
                    this.drpHandleMan.Enabled = false;
                }
                this.drpHandleType.SelectedIndex = 0;
                this.drpHandleMan.Items.Clear();
                this.drpHandleMan.DataTextField = "UserName";
                this.drpHandleMan.DataValueField = "UserId";
                this.drpHandleMan.DataSource = BLL.UserService.GetProjectUserListByProjectId(this.CurrUser.LoginProjectId);
                this.drpHandleMan.DataBind();
                Funs.FineUIPleaseSelect(drpHandleMan);
                if (State == Const.Design_Audit2)
                {
                    this.drpHandleMan.SelectedIndex = 2;
                }
                else
                {
                    this.drpHandleMan.SelectedIndex = 0;
                }

            }
            else
            {
                this.drpHandleMan.Items.Clear();
                Funs.FineUIPleaseSelect(drpHandleMan);
                this.drpHandleType.SelectedIndex = 1;
                ListItem item = new ListItem();
                Model.Sys_User user = BLL.UserService.GetUserByUserId(BLL.DesignApproveService.GetAuditMan(DesignId, this.drpHandleType.SelectedValue).ApproveMan);
                item.Value = user.UserId;
                item.Text = user.UserName;
                this.drpHandleMan.Items.Add(item);
                this.drpHandleMan.SelectedIndex = 1;
            }
        }

        protected void txtPlanDay_Blur(object sender, EventArgs e)
        {
            this.txtPlanCompleteDate.Text = string.Empty;
            if (!string.IsNullOrEmpty(this.txtPlanDay.Text.Trim()))
            {
                try
                {
                    decimal d = Convert.ToDecimal(this.txtPlanDay.Text.Trim());
                    int i = 0;
                    if (this.txtPlanDay.Text.Contains("."))  //有小数
                    {
                        i = (Int32)d + 1;
                    }
                    else
                    {
                        i = (Int32)d;
                    }
                    if (this.rblIsNeedMaterial.SelectedValue == "true")  //需要增补材料
                    {
                        if (!string.IsNullOrEmpty(this.txtMaterialPlanReachDate.Text.Trim()))
                        {
                            DateTime date = Convert.ToDateTime(this.txtMaterialPlanReachDate.Text.Trim());
                            date = date.AddDays(i);
                            this.txtPlanCompleteDate.Text = string.Format("{0:yyyy-MM-dd}", date);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.txtDesignDate.Text.Trim()))
                        {
                            DateTime date = Convert.ToDateTime(this.txtDesignDate.Text.Trim());
                            date = date.AddDays(i);
                            this.txtPlanCompleteDate.Text = string.Format("{0:yyyy-MM-dd}", date);
                        }
                    }
                }
                catch (Exception)
                {
                    this.txtPlanDay.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('预计施工周期只能录入整数或小数！')", true);
                }
            }
            if (!string.IsNullOrWhiteSpace(DesignId))
            {
                Model.Check_Design Design = DesignService.GetDesignByDesignId(DesignId);
                if (Design.CarryUnitIds != null)
                {
                    txtCarryUnit.Values = Design.CarryUnitIds.Split(',');

                }
                if (Design.BuyMaterialUnitIds != null)
                {
                    if (this.rblIsNeedMaterial.SelectedValue == "true")
                    {
                        txtBuyMaterialUnit.Values = Design.BuyMaterialUnitIds.Split(',');
                    }

                }

            }
        }
    }
}