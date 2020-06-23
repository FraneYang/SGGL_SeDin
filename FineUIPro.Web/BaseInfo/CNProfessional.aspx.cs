using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.BaseInfo
{
    public partial class CNProfessional : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetButtonPower();
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();

                // 绑定表格
                BindGrid();
                btnNew.OnClientClick = Window1.GetShowReference("CNProfessionalEdit.aspx") + "return false;";
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>

        public void BindGrid()
        {
            DataTable tb = BindData();
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }


        protected DataTable BindData()
        {
            string strSql = @"select CNProfessionalId,CNProfessionalCode,ProfessionalName,SortIndex from  [dbo].[Base_CNProfessional] where 1=1 ";

            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.txtCNProfessionalCode.Text.Trim()))
            {
                strSql += " AND CNProfessionalCode like @CNProfessionalCode";
                listStr.Add(new SqlParameter("@CNProfessionalCode", "%" + this.txtCNProfessionalCode.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtProfessionalName.Text.Trim()))
            {
                strSql += " AND ProfessionalName like @ProfessionalName";
                listStr.Add(new SqlParameter("@ProfessionalName", "%" + this.txtProfessionalName.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            return tb;
        }
        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            EditData();
        }

        protected void btnMenuDel_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    BLL.CNProfessionalService.DeleteCNProfessionalById(rowID);
                }

                BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
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
            if (this.btnMenuModify.Hidden)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CNProfessionalView.aspx?CNProfessionalId={0}", Grid1.SelectedRowID, "查看 - ")));
            }
            else
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CNProfessionalEdit.aspx?CNProfessionalId={0}", Grid1.SelectedRowID, "编辑 - ")));
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.CNProfessionalMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
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


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            EditData();
        }


        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CNProfessionalView.aspx?CNProfessionalId={0}", Grid1.SelectedRowID, "查看 - ")));
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }
        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRset_Click(object sender, EventArgs e)
        {
            txtCNProfessionalCode.Text = "";
            txtProfessionalName.Text = "";
            BindGrid();
        }

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
    }
}