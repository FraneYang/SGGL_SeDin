using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.PZHGL.InformationProject
{
    public partial class ConstructionPlan : PageBase
    {
        /// <summary>
        /// 工程联系单主键
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
            var buttonList = CommonService.GetAllButtonList(CurrUser.LoginProjectId, CurrUser.UserId, Const.ConstructionPlanMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(Const.BtnAdd))
                {
                    btnNew.Hidden = false;
                }
                if (buttonList.Contains(Const.BtnModify))
                {
                    btnMenuModify.Hidden = false;
                }
                if (buttonList.Contains(Const.BtnDelete))
                {
                    btnMenuDel.Hidden = false;
                }
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                btnNew.OnClientClick = window_tt.GetShowReference("ConstructionPlanEdit.aspx", "总承包商施工计划") + "return false;";
                GetButtonPower();
                BindGrid();
            }
        }
        protected DataTable ChecklistData()
        {
            string strSql = @"SELECT chec.ConstructionPlanId,chec.ProjectId,chec.Code,chec.CompileDate,chec.State,chec.CompileMan,u.userName"
                          + @" FROM ZHGL_ConstructionPlan chec "
                          + @" left join sys_User u on u.userId = chec.CompileMan"
                          + @" where chec.ProjectId=@ProjectId";

            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", CurrUser.LoginProjectId));
            if (!string.IsNullOrEmpty(this.txtCode.Text.Trim()))
            {
                strSql += " AND chec.Code like @Code";
                listStr.Add(new SqlParameter("@Code", "%" + this.txtCode.Text.Trim() + "%"));
            }
            strSql += " order by chec.Code desc ";
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            return tb;
        }
        private void BindGrid()
        {
            var list = ChecklistData();
            Grid1.RecordCount = list.Rows.Count;
            list = GetFilteredTable(Grid1.FilteredData, list);
            var table = GetPagedDataTable(Grid1, list);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            ConstructionPlanId = Grid1.SelectedRowID.Split(',')[0];
            Model.ZHGL_ConstructionPlan ConstructionPlan = BLL.ConstructionPlanService.GetConstructionPlanById(ConstructionPlanId);
            Model.ZHGL_ConstructionPlanApprove approve = BLL.ConstructionPlanApproveService.GetConstructionPlanApproveByConstructionPlanId(ConstructionPlanId);
            if (ConstructionPlan.State == BLL.Const.ConstructionPlan_Complete)
            {
                Alert.ShowInTop("该记录已审批完成！可以点击右键查看", MessageBoxIcon.Warning);
                return;
            }

            else if (approve != null)
            {
                if (!string.IsNullOrEmpty(approve.ApproveMan))
                {
                    if (this.CurrUser.UserId == approve.ApproveMan || this.CurrUser.UserId == BLL.Const.sysglyId || this.CurrUser.UserId == BLL.Const.hfnbdId)
                    {
                        PageContext.RegisterStartupScript(window_tt.GetShowReference(String.Format("ConstructionPlanEdit.aspx?ConstructionPlanId={0}", ConstructionPlanId, "编辑 - ")));

                    }
                    else
                    {
                        Alert.ShowInTop("您不是当前办理人，无法操作！可以点击右键查看", MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            else
            {
                Alert.ShowInTop("您不是当前办理人，无法操作！可以点击右键查看", MessageBoxIcon.Warning);
                return;
            }
            //else
            //{
            //    Alert.ShowInTop("您不是当前办理人，无法操作！", MessageBoxIcon.Warning);
            //    return;
            //}

        }

        protected void btnMenuDel_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            ConstructionPlanId = Grid1.SelectedRowID.Split(',')[0];
            var constructionPlan = ConstructionPlanService.GetConstructionPlanById(ConstructionPlanId);
            BLL.ConstructionPlanApproveService.DeleteConstructionPlanApproveByConstructionPlanId(ConstructionPlanId);
            BLL.ConstructionPlanService.DeleteConstructionPlanById(ConstructionPlanId);
            BLL.LogService.AddSys_Log(this.CurrUser, constructionPlan.Code, ConstructionPlanId, Const.ConstructionPlanMenuId, "删除总承包商施工计划");
            BindGrid();
            Alert.ShowInTop("删除数据成功！", MessageBoxIcon.Success);
        }

        protected void window_tt_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }

        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            btnMenuModify_Click(sender, e);
        }

        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string ConstructionPlanId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(window_tt.GetShowReference(String.Format("ConstructionPlanView.aspx?ConstructionPlanId={0}", ConstructionPlanId, "查看 - ")));
        }

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }
    }
}