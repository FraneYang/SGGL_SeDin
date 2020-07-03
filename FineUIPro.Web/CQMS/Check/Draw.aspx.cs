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
    public partial class Draw : PageBase
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
                BLL.DrawService.InitMainItemDropDownList(drpMainItem, this.CurrUser.LoginProjectId);
                BLL.DrawService.InitDesignCNNameDropDownList(drpDesignCN);
                btnNew.OnClientClick = Window1.GetShowReference("DrawEdit.aspx") + "return false;";
                GetButtonPower();
                BindGrid();
            }
        }
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
            string strSql = @"select C.DrawId,C.DrawCode,C.DrawName,M.MainItemName as MainItem,D.ProfessionalName as DesignCN,C.ProjectId,C.Edition,C.AcceptDate from [dbo].[Check_Draw] C 
              left join [dbo].[Base_DesignProfessional] D on D.DesignProfessionalId=C.DesignCN
              left join  [dbo].[ProjectData_MainItem] M on C.MainItem=M.MainItemId where 1=1";

            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND C.ProjectId = @ProjectId";
            listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));
            if (!string.IsNullOrEmpty(this.txtDrawCode.Text.Trim()))
            {
                strSql += " AND DrawCode like @DrawCode";
                listStr.Add(new SqlParameter("@DrawCode", "%" + this.txtDrawCode.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtDrowName.Text.Trim()))
            {
                strSql += " AND DrawName like @DrawName";
                listStr.Add(new SqlParameter("@DrawName", "%" + this.txtDrowName.Text.Trim() + "%"));
            }
            if (drpMainItem.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND M.MainItemToUnitWorkId=@MainItem";
                listStr.Add(new SqlParameter("@MainItem", drpMainItem.SelectedValue));
            }
            if (drpDesignCN.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND C.DesignCN=@DesignCN";
                listStr.Add(new SqlParameter("@DesignCN", drpDesignCN.SelectedValue));
            }
            if (!string.IsNullOrEmpty(this.txtEdition.Text.Trim()))
            {
                strSql += " AND Edition like @Edition";
                listStr.Add(new SqlParameter("@Edition", "%" + this.txtEdition.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            return tb;
        }
        /// <summary>
        /// 分页显示条数下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }
        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        //右键编辑
        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            EditData();
        }
        //双击编辑
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            EditData();
        }
        //分页
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
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
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("DrawEdit.aspx?DrawId={0}", Grid1.SelectedRowID, "编辑 - ")));

        }
        //右键查看
        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录", MessageBoxIcon.Warning);
                return;
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("DrawView.aspx?DrawId={0}", Grid1.SelectedRowID, "查看 - ")));
        }
        //右键删除
        protected void btnMenuDel_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var Unitwork = BLL.DrawService.GetDrawByDrawId(rowID);
                    if (Unitwork != null)
                    {
                        BLL.DrawService.DeleteDrawById(rowID);

                    }
                }

                BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRset_Click(object sender, EventArgs e)
        {
            drpMainItem.SelectedIndex = 0;
            drpDesignCN.SelectedIndex = 0;
            txtDrowName.Text = "";
            txtDrawCode.Text = "";
            txtEdition.Text = "";
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.ProjectId, this.CurrUser.UserId, BLL.Const.DrawMenuId);
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
    }
}