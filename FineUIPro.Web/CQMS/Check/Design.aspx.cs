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
    public partial class Design : PageBase
    {
        /// <summary>
        /// 项目id
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UnitService.InitUnitByProjectIdUnitTypeDropDownList1(drpUnit, this.CurrUser.LoginProjectId, true);//施工单位
                DesignProfessionalService.InitDesignProfessionalDownList(drpCNProfessional, true);//专业
                BLL.MainItemService.InitMainItemDownList(drpMainItem, this.CurrUser.LoginProjectId, true);//主项
                this.drpDesignType.DataTextField = "Text";
                this.drpDesignType.DataValueField = "Value";
                drpDesignType.DataSource = BLL.DesignService.GetDesignTypeList();
                drpDesignType.DataBind();
                Funs.FineUIPleaseSelect(drpDesignType);
                Funs.FineUIPleaseSelect(drpState);
                btnNew.OnClientClick = Window1.GetShowReference("EditDesign.aspx") + "return false;";
                BindGrid();
                ProjectId = CurrUser.LoginProjectId;
                GetButtonPower();
            }
        }
        private void BindGrid()
        {
            string strSql = "select D.CarryUnitIds, D.MaterialRealReachDate,D.BuyMaterialUnitIds,D.MainItemId,D.DesignId,D.ProjectId,M.MainItemName,C.ProfessionalName, D.State,D.DesignType,D.DesignCode,D.DesignContents,D.DesignDate,U.UnitName as CarryUnit,(case D.IsNoChange when 'true' then '是' when 'false' then '否' else '' end) IsNoChange,(case D.IsNeedMaterial when 'true' then '是' when 'false' then '否' else '' end) IsNeedMaterial,U1.UnitName as BuyMaterialUnit,D.MaterialPlanReachDate,D.PlanDay,D.PlanCompleteDate,D.PlanCompleteDate,D.RealCompleteDate,D.CompileMan,C.ProfessionalName, D.CompileDate from Check_Design D left join Base_Unit U1 on U1.UnitId = D.BuyMaterialUnitIds left join Base_Unit U on U.UnitId = D.CarryUnitIds left join ProjectData_MainItem M on M.MainItemId = D.MainItemId left join Base_DesignProfessional C on C.DesignProfessionalId = D.CNProfessionalCode";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " where D.ProjectId = @ProjectId";
            listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            if (drpDesignType.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND D.DesignType = @DesignType";
                listStr.Add(new SqlParameter("@DesignType", this.drpDesignType.SelectedValue));
            }
            if (drpUnit.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND  CHARINDEX(@UnitId,D.CarryUnitIds)>0";
                listStr.Add(new SqlParameter("@UnitId", this.drpUnit.SelectedValue));
            }
            if (drpMainItem.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND D.MainItemId = @MainItemId";
                listStr.Add(new SqlParameter("@MainItemId", this.drpMainItem.SelectedValue));
            }
            if (drpCNProfessional.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND D.CNProfessionalCode = @CNProfessionalCode";
                listStr.Add(new SqlParameter("@CNProfessionalCode", this.drpCNProfessional.SelectedValue));
            }
            if (!string.IsNullOrEmpty(txtStartTime.Text.Trim()))
            {
                strSql += " AND DesignDate >= @DesignDate";
                listStr.Add(new SqlParameter("@DesignDate", txtStartTime.Text.Trim() + " 00:00:00"));
            }
            if (!string.IsNullOrEmpty(txtEndTime.Text.Trim()))
            {
                strSql += " AND DesignDate <= @DesignDateE";
                listStr.Add(new SqlParameter("@DesignDateE", txtEndTime.Text.Trim() + " 23:59:59"));
            }
            if (drpState.SelectedValue != Const._Null)
            {
                if (drpState.SelectedValue == "1")   //已闭合
                {
                    strSql += " AND State=@State";
                    listStr.Add(new SqlParameter("@State", "6"));
                }
                else   //未闭合
                {
                    strSql += " AND State!=@State";
                    listStr.Add(new SqlParameter("@State", "6"));
                }
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        //<summary>
        //获取办理人姓名
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        protected string ConvertMan(object designId)
        {
            if (designId != null)
            {
                Model.Check_DesignApprove a = BLL.DesignApproveService.GetDesignApproveByDesignId(designId.ToString());
                if (a != null)
                {
                    if (a.ApproveMan != null)
                    {
                        return BLL.UserService.GetUserByUserId(a.ApproveMan).UserName;
                    }
                }
                else
                {
                    return "";
                }
            }
            return "";
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
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 获取单位名称
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertCarryUnit(object CarryUnitIds)
        {
            string CarryUnitName = string.Empty;
            if (CarryUnitIds != null)
            {
                string[] Ids = CarryUnitIds.ToString().Split(',');
                foreach (string t in Ids)
                {
                    var type = BLL.UnitService.GetUnitByUnitId(t);
                    if (type != null)
                    {
                        CarryUnitName += type.UnitName + ",";
                    }
                }
            }
            if (CarryUnitName != string.Empty)
            {
                return CarryUnitName.Substring(0, CarryUnitName.Length - 1);
            }
            else
            {
                return "";
            }
        }
        #region 数据操作

        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.DesignMenuId, BLL.Const.BtnModify))
            {
                if (Grid1.SelectedRowIndexArray.Length == 0)
                {
                    Alert.ShowInTop("请至少选择一条记录", MessageBoxIcon.Warning);
                    return;
                }
                Model.Check_Design design = BLL.DesignService.GetDesignByDesignId(Grid1.SelectedRowID);
                Model.Check_DesignApprove approve = BLL.DesignApproveService.GetDesignApproveByDesignId(Grid1.SelectedRowID);
                if (design.State == BLL.Const.Design_Complete)
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("DesignView.aspx?see=see&DesignId=" + Grid1.SelectedRowID, "查看 - ")));
                }
                if (approve != null)
                {
                    if (!string.IsNullOrEmpty(approve.ApproveMan))
                    {
                        if (this.CurrUser.UserId == approve.ApproveMan || this.CurrUser.UserId == BLL.Const.sysglyId)
                        {
                            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EditDesign.aspx?DesignId=" + Grid1.SelectedRowID, "编辑 - ")));
                        }
                        else
                        {
                            Alert.ShowInTop("您不是当前办理人，无法操作！", MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    Alert.ShowInTop("您不是当前办理人，无法操作！", MessageBoxIcon.Warning);
                }


            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("DesignView.aspx?see=see&DesignId=" + Grid1.SelectedRowID, "查看 - ")));
        }

        protected void btnMenuDel_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.DesignMenuId, BLL.Const.BtnDelete))
            {
                BLL.DesignApproveService.DeleteDesignApprovesByDesignId(Grid1.SelectedRowID);
                BLL.DesignService.DeleteDesign(Grid1.SelectedRowID);
                //BLL.LogService.AddLog(this.CurrUser.UserId, "删除设计变更", this.CurrUser.LoginProjectId);
                BindGrid();
                Alert.ShowInTop("删除数据成功！", MessageBoxIcon.Success);
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }



        protected void btnRset_Click(object sender, EventArgs e)
        {
            drpUnit.SelectedIndex = 0;
            drpMainItem.SelectedIndex = 0;
            drpCNProfessional.SelectedIndex = 0;
            drpDesignType.SelectedIndex = 0;
            drpState.SelectedIndex = 0;
            txtStartTime.Text = "";
            txtEndTime.Text = "";
            BindGrid();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.ProjectId, this.CurrUser.UserId, BLL.Const.DesignMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuModify.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDel.Hidden = false;
                }
            }
        }

        #endregion

        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.DesignMenuId, BLL.Const.BtnModify))
            {
                if (Grid1.SelectedRowIndexArray.Length == 0)
                {
                    Alert.ShowInTop("请至少选择一条记录", MessageBoxIcon.Warning);
                    return;
                }
                Model.Check_Design design = BLL.DesignService.GetDesignByDesignId(Grid1.SelectedRowID);
                Model.Check_DesignApprove approve = BLL.DesignApproveService.GetDesignApproveByDesignId(Grid1.SelectedRowID);
                if (design.State == BLL.Const.Design_Complete)
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("DesignView.aspx?see=see&DesignId=" + Grid1.SelectedRowID, "查看 - ")));
                }
                if (approve != null)
                {
                    if (!string.IsNullOrEmpty(approve.ApproveMan))
                    {
                        if (this.CurrUser.UserId == approve.ApproveMan || this.CurrUser.UserId == BLL.Const.sysglyId)
                        {
                            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EditDesign.aspx?DesignId=" + Grid1.SelectedRowID, "编辑 - ")));
                        }
                        else
                        {
                            Alert.ShowInTop("您不是当前办理人，无法操作！", MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    Alert.ShowInTop("您不是当前办理人，无法操作！", MessageBoxIcon.Warning);
                }


            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }
    }
}