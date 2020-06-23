using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.ProjectData
{
    public partial class MainItem : PageBase
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
                if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
                {
                    this.ProjectId = this.CurrUser.LoginProjectId;
                }
                else
                {
                    this.ProjectId = this.CurrUser.LoginProjectId;
                }
                btnNew.OnClientClick = Window1.GetShowReference("MainItemEdit.aspx") + "return false;";
                GetButtonPower();
                BindGrid();
            }
        }
        private void BindGrid()
        {
            string strSql = "select * from ProjectData_MainItem where 1=1";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " and ProjectId = @ProjectId";
            listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));
            if (!string.IsNullOrEmpty(this.txtMainItemCode.Text.Trim()))
            {
                strSql += " and MainItemCode like @MainItemCode";
                listStr.Add(new SqlParameter("@MainItemCode", "%" + this.txtMainItemCode.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {

        }


        protected void btnRset_Click(object sender, EventArgs e)
        {
            this.txtMainItemCode.Text = "";
            BindGrid();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #region 操作数据
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            EditData();
        }
        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            EditData();
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
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("MainItemView.aspx?MainItemId={0}", Grid1.SelectedRowID, "查看 - ")));
            }
            else
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("MainItemEdit.aspx?MainItemId={0}", Grid1.SelectedRowID, "编辑 - ")));
            }
        }

        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("MainItemView.aspx?MainItemId=" + Grid1.SelectedRowID, "查看 - ")));
        }

        protected void btnMenuDel_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    BLL.MainItemService.DeleteMainItemByMainItemId(rowID);
                }

                BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.ProjectId, this.CurrUser.UserId, BLL.Const.MainItemMenuId);
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
        /// <summary>
        /// 获取主项对应单位工程
        /// </summary>
        /// <param name="UnitWorks"></param>
        /// <returns></returns>
        protected string ConvertUnitName(object UnitWorks)
        {
            string UnitName = string.Empty;
            if (UnitWorks != null)
            {
                string[] Ids = UnitWorks.ToString().Split(',');
                foreach (string t in Ids)
                {
                    var type = BLL.UnitWorkService.getUnitWorkByUnitWorkId(t);
                    if (type != null)
                    {
                        UnitName += type.UnitWorkName + ",";
                    }
                }
            }
            if (UnitName != string.Empty)
            {
                return UnitName.Substring(0, UnitName.Length - 1);
            }
            else
            {
                return "";
            }
        }
    }
}