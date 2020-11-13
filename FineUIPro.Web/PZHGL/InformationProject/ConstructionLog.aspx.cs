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
    public partial class ConstructionLog : PageBase
    {
        /// <summary>
        /// 工程联系单主键
        /// </summary>
        public string ConstructionLogId
        {
            get
            {
                return (string)ViewState["ConstructionLogId"];
            }
            set
            {
                ViewState["ConstructionLogId"] = value;
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
            var buttonList = CommonService.GetAllButtonList(CurrUser.LoginProjectId, CurrUser.UserId, Const.ConstructionLogMenuId);
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
                btnNew.OnClientClick = window_tt.GetShowReference("ConstructionLogEdit.aspx", "项目级施工日志") + "return false;";
                GetButtonPower();
                BindGrid();
            }
        }
        protected DataTable ChecklistData()
        {
            string strSql = @"SELECT chec.ConstructionLogId,chec.ProjectId,chec.CompileMan,chec.CompileDate,u.userName"
                          + @" FROM ZHGL_ConstructionLog chec "
                          + @" left join sys_User u on u.userId = chec.CompileMan"
                          + @" where chec.ProjectId=@ProjectId";

            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", CurrUser.LoginProjectId));
            if (!string.IsNullOrEmpty(this.txtUserName.Text.Trim()))
            {
                strSql += " AND u.userName like @UserName";
                listStr.Add(new SqlParameter("@UserName", "%" + this.txtUserName.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtCompileDate.Text.Trim()))
            {
                strSql += " AND chec.CompileDate = @CompileDate";
                listStr.Add(new SqlParameter("@CompileDate", this.txtCompileDate.Text.Trim()));
            }
            strSql += " order by chec.CompileDate desc ";
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
            ConstructionLogId = Grid1.SelectedRowID.Split(',')[0];
            Model.ZHGL_ConstructionLog ConstructionLog = BLL.ConstructionLogService.GetConstructionLogById(ConstructionLogId);
            if (this.CurrUser.UserId == ConstructionLog.CompileMan || this.CurrUser.UserId == BLL.Const.sysglyId || this.CurrUser.UserId == BLL.Const.hfnbdId)
            {
                PageContext.RegisterStartupScript(window_tt.GetShowReference(String.Format("ConstructionLogEdit.aspx?ConstructionLogId={0}", ConstructionLogId, "编辑 - ")));

            }
            else
            {
                Alert.ShowInTop("您不是编制人，无法操作！可以点击右键查看", MessageBoxIcon.Warning);
                return;
            }
        }

        protected void btnMenuDel_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            ConstructionLogId = Grid1.SelectedRowID.Split(',')[0];
            var constructionLog = ConstructionLogService.GetConstructionLogById(ConstructionLogId);
            BLL.ConstructionLogService.DeleteConstructionLogById(ConstructionLogId);
            BLL.LogService.AddSys_Log(this.CurrUser, string.Format("{0:yyyy-MM-dd}", constructionLog.CompileDate), ConstructionLogId, Const.ConstructionLogMenuId, "删除项目级施工日志");
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
            string ConstructionLogId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(window_tt.GetShowReference(String.Format("ConstructionLogView.aspx?ConstructionLogId={0}", ConstructionLogId, "查看 - ")));
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