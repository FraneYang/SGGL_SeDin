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
    public partial class CheckMonth : PageBase
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                GetButtonPower();
                BindGrid();
            }
        }
        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void BindGrid()
        {
            DataTable tb = ChecklistData();
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        protected DataTable ChecklistData()
        {
            string strSql = @"select C.CheckMonthId,C.ProjectId,C.Months,C.CompileDate,C.CompileMan, U.UserName from Check_CheckMonth C  left join Sys_User U on  U.UserId = C.CompileMan where 1=1";

            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND C.ProjectId = @ProjectId";
            listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            return tb;
        }
        #endregion
        #region 操作数据
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.CheckMonthMenuId, BLL.Const.BtnAdd))
            {
                if (!string.IsNullOrEmpty(this.txtMonths.Text.Trim()))
                {
                    string months = txtMonths.Text.Trim() + "-01";
                    if (BLL.CheckMonthService.GetCheckMonthByMonths(Convert.ToDateTime(months), this.CurrUser.LoginProjectId) == null)
                    {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EditCheckMonth.aspx?months={0}", months, "添加 - ")));
                    }
                    else
                    {
                        Alert.ShowInTop("该月份月报已存在！", MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    Alert.ShowInTop("请选择月份！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }
        //右键编辑
        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.CheckMonthMenuId, BLL.Const.BtnModify))
            {
                if (Grid1.SelectedRowIndexArray.Length == 0)
                {
                    Alert.ShowInTop("请至少选择一条记录", MessageBoxIcon.Warning);
                    return;
                }
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EditCheckMonth.aspx?CheckMonthId={0}", Grid1.SelectedRowID, "编辑 - ")));
            }
        }
        //双击编辑
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.CheckMonthMenuId, BLL.Const.BtnModify))
            {
                if (Grid1.SelectedRowIndexArray.Length == 0)
                {
                    Alert.ShowInTop("请至少选择一条记录", MessageBoxIcon.Warning);
                    return;
                }
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EditCheckMonth.aspx?CheckMonthId={0}", Grid1.SelectedRowID, "编辑 - ")));
            }
        }
        //查看
        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录", MessageBoxIcon.Warning);
                return;
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EditCheckMonth.aspx?see=see&CheckMonthId={0}", Grid1.SelectedRowID, "查看 - ")));
        }

        protected void btnMenuDel_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.CheckMonthMenuId, BLL.Const.BtnDelete))
            {
                if (Grid1.SelectedRowIndexArray.Length > 0)
                {
                    foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                    {
                        string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                        var CheckMonth = BLL.MonthSpotCheckDetailService.GetMonthSpotCheckDetailsByCheckMonthId(rowID);
                        if (CheckMonth != null)
                        {

                            BLL.MonthSpotCheckDetailService.DeleteMonthSpotCheckDetailsByCheckMonthId(rowID);

                        }
                        BLL.SpecialEquipmentDetailService.DeleteSpecialEquipmentDetailsByCheckMonthId(rowID);
                        BLL.CheckMonthService.DeleteCheckMonth(rowID);

                    }

                    BindGrid();
                    ShowNotify("删除数据成功!", MessageBoxIcon.Success);
                }
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }

        }
        /// <summary>
        /// 编辑数据方法
        /// </summary>
        private void EditData()
        {

            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录", MessageBoxIcon.Warning);
                return;
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EditCheckMonth.aspx?CheckMonthId={0}", Grid1.SelectedRowID, "编辑 - ")));

        }
        #endregion
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.ProjectId, this.CurrUser.UserId, BLL.Const.CheckMonthMenuId);
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

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
    }
}