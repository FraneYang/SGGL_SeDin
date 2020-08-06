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
    public partial class UnitWork : PageBase
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
                Funs.FineUIPleaseSelect(this.drpProjectType);
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();

                // 绑定表格
                BindGrid();
                btnNew.OnClientClick = Window1.GetShowReference("UnitWorkEdit.aspx") + "return false;";


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
            string strSql = @"select UnitWorkId,UnitWorkCode,UnitWorkName,Costs,Weights,SuperUnitWork,(case IsChild when '1' then 'true' else 'false' end) isChild,(case ProjectType when '1' then '建筑工程' when '2' then '安装工程' else '' end ) ProjectType ,Unit.UnitName AS UnitId,SupervisorUnit.UnitName AS SupervisorUnitId,NDEUnit.UnitName AS NDEUnit from  [dbo].[WBS_UnitWork] AS UnitWork 
              Left join Base_Unit AS Unit on UnitWork.UnitId=Unit.UnitId
              Left join Base_Unit AS SupervisorUnit on UnitWork.SupervisorUnitId=SupervisorUnit.UnitId
              Left join Base_Unit AS NDEUnit on UnitWork.NDEUnit=NDEUnit.UnitId where ProjectId=@ProjectId ";

            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            if (!string.IsNullOrEmpty(this.txtUnitWorkCode.Text.Trim()))
            {
                strSql += " AND UnitWorkCode like @UnitWorkCode";
                listStr.Add(new SqlParameter("@UnitWorkCode", "%" + this.txtUnitWorkCode.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtUnitWorkName.Text.Trim()))
            {
                strSql += " AND UnitWorkName like @UnitWorkName";
                listStr.Add(new SqlParameter("@UnitWorkName", "%" + this.txtUnitWorkName.Text.Trim() + "%"));
            }
            if (drpProjectType.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND ProjectType=@ProjectType";
                listStr.Add(new SqlParameter("@ProjectType", drpProjectType.SelectedValue));
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
                    var Unitwork = BLL.UnitWorkService.GetUnitWorkByUnitWorkId(rowID);
                    if (Unitwork != null)
                    {
                        BLL.ControlItemAndCycleService.DeleteAllControlItemAndCycleByUnitWorkId(rowID);
                        BLL.WorkPackageService.DeleteAllWorkPackageByUnitWorkId(rowID);
                        BLL.UnitWorkService.DeleteUnitWorkById(rowID);
                    }
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
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("UnitWorkView.aspx?UnitWorkId={0}", Grid1.SelectedRowID, "查看 - ")));
            }
            else
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("UnitWorkEdit.aspx?UnitWorkId={0}", Grid1.SelectedRowID, "编辑 - ")));
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.ProjectId, this.CurrUser.UserId, BLL.Const.UnitWorkMenuId);
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
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("UnitWorkView.aspx?UnitWorkId={0}", Grid1.SelectedRowID, "查看 - ")));
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
            drpProjectType.SelectedIndex = 0;
            txtUnitWorkCode.Text = "";
            txtUnitWorkName.Text = "";
            BindGrid();
        }

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
    }
}