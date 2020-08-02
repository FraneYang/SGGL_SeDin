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
    public partial class DesignView : PageBase
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


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DesignId = Request.Params["DesignId"];
                if (!string.IsNullOrEmpty(DesignId))
                {
                    BindGrid();
                }
                txtProjectName.Text = ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId).ProjectName;
                BLL.MainItemService.InitMainItemDownList(drpMainItem, this.CurrUser.LoginProjectId, true);//主项
                this.drpDesignType.DataTextField = "Text";
                this.drpDesignType.DataValueField = "Value";
                drpDesignType.DataSource = BLL.DesignService.GetDesignTypeList();
                drpDesignType.DataBind();
                Funs.FineUIPleaseSelect(drpDesignType);//变更类型
                DesignProfessionalService.InitDesignProfessionalDownList(drpCNProfessional, true);//专业
                Funs.FineUIPleaseSelect(drpHandleMan);
                this.HideOptions.Hidden = true;
                this.rblIsAgree.Hidden = true;
                if (!string.IsNullOrEmpty(DesignId))
                {
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
                        List<string> units = design.CarryUnitIds.Split(',').ToList();
                        string unit = string.Empty;
                        foreach (var item in units)
                        {
                            unit += BLL.UnitService.GetUnitByUnitId(item).UnitName + ",";
                        }
                        if (!string.IsNullOrEmpty(unit))
                        {
                            this.txtCarryUnit.Text = unit.Substring(0, unit.LastIndexOf(","));
                        }
                    }
                    if (design.IsNoChange == true || design.IsNoChange == null)
                    {
                        this.rblIsNoChange.Text = "是";
                    }
                    else
                    {
                        this.rblIsNoChange.Text = "否";
                    }
                    if (design.IsNeedMaterial == true || design.IsNeedMaterial == null)
                    {
                        this.rblIsNeedMaterial.Text = "是";
                    }
                    else
                    {
                        this.rblIsNeedMaterial.Text = "否";
                    }
                    if (!string.IsNullOrEmpty(design.BuyMaterialUnitIds))
                    {
                        List<string> units = design.BuyMaterialUnitIds.Split(',').ToList();
                        string unit = string.Empty;
                        foreach (var item in units)
                        {
                            unit += BLL.UnitService.GetUnitByUnitId(item).UnitName + ",";
                        }
                        if (!string.IsNullOrEmpty(unit))
                        {
                            this.txtBuyMaterialUnit.Text = unit.Substring(0, unit.LastIndexOf(","));
                        }
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
                        this.drpHandleMan.DataTextField = "Text";
                        this.drpHandleMan.DataValueField = "Value";
                        this.drpHandleMan.DataSource = BLL.UserService.GetProjectUserListByProjectId(this.CurrUser.LoginProjectId);
                        this.drpHandleMan.DataBind();
                        this.drpHandleMan.SelectedIndex = 1;
                    }
                    else
                    {
                        this.drpHandleMan.DataTextField = "Text";
                        this.drpHandleMan.DataValueField = "Value";
                        this.drpHandleMan.DataSource = BLL.UserService.GetProjectUserListByProjectId(this.CurrUser.LoginProjectId);
                        this.drpHandleMan.DataBind();
                        this.drpHandleMan.SelectedIndex = 1;
                        this.HideOptions.Hidden = false;
                        this.rblIsAgree.Hidden = false;
                    }
                    if (State == Const.Design_Audit4)
                    {
                    }
                    if (State == BLL.Const.Design_Complete || !string.IsNullOrEmpty(Request.Params["see"]))
                    {
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
                    this.drpHandleMan.DataTextField = "Text";
                    this.drpHandleMan.DataValueField = "Value";
                    this.drpHandleMan.DataSource = BLL.UserService.GetProjectUserListByProjectId(this.CurrUser.LoginProjectId);
                    this.drpHandleMan.DataBind();
                    this.drpHandleMan.SelectedIndex = 1;
                    plApprove2.Hidden = true;
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
        protected void btnAttach_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?type=-1&toKeyId={0}&path=FileUpload/Design&menuId={1}", DesignId, BLL.Const.DesignMenuId)));
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
                    return "审批完成";
                }
                else
                {
                    return "";
                }
            }
            return "";
        }
    }
}