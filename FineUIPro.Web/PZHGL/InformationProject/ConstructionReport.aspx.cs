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
    public partial class ConstructionReport : PageBase
    {
        /// <summary>
        /// 工程联系单主键
        /// </summary>
        public string ConstructionReportId
        {
            get
            {
                return (string)ViewState["ConstructionReportId"];
            }
            set
            {
                ViewState["ConstructionReportId"] = value;
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
            var buttonList = CommonService.GetAllButtonList(CurrUser.LoginProjectId, CurrUser.UserId, Const.ConstructionReportMenuId);
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
                drpFileType.DataValueField = "Value";
                drpFileType.DataTextField = "Text";
                drpFileType.DataSource = BLL.ConstructionReportService.GetFileTypeList();
                drpFileType.DataBind();
                Funs.FineUIPleaseSelect(drpFileType);
                btnNew.OnClientClick = window_tt.GetShowReference("ConstructionReportEdit.aspx", "总承包商施工报告") + "return false;";
                GetButtonPower();
                BindGrid();
            }
        }
        protected DataTable ChecklistData()
        {
            string strSql = @"SELECT chec.ConstructionReportId,chec.ProjectId,chec.FileType,chec.Code,chec.CompileDate,chec.State,chec.CompileMan,u.userName"
                          + @" FROM ZHGL_ConstructionReport chec "
                          + @" left join sys_User u on u.userId = chec.CompileMan"
                          + @" where chec.ProjectId=@ProjectId";

            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", CurrUser.LoginProjectId));
            if (!string.IsNullOrEmpty(this.txtCode.Text.Trim()))
            {
                strSql += " AND chec.Code like @Code";
                listStr.Add(new SqlParameter("@Code", "%" + this.txtCode.Text.Trim() + "%"));
            }
            if (this.drpFileType.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND chec.FileType = @FileType";
                listStr.Add(new SqlParameter("@FileType", this.drpFileType.SelectedValue));
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
            ConstructionReportId = Grid1.SelectedRowID.Split(',')[0];
            Model.ZHGL_ConstructionReport ConstructionReport = BLL.ConstructionReportService.GetConstructionReportById(ConstructionReportId);
            Model.ZHGL_ConstructionReportApprove approve = BLL.ConstructionReportApproveService.GetConstructionReportApproveByConstructionReportId(ConstructionReportId);
            if (ConstructionReport.State == BLL.Const.ConstructionReport_Complete)
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
                        PageContext.RegisterStartupScript(window_tt.GetShowReference(String.Format("ConstructionReportEdit.aspx?ConstructionReportId={0}", ConstructionReportId, "编辑 - ")));

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
            ConstructionReportId = Grid1.SelectedRowID.Split(',')[0];
            var constructionReport = ConstructionReportService.GetConstructionReportById(ConstructionReportId);
            BLL.ConstructionReportApproveService.DeleteConstructionReportApproveByConstructionReportId(ConstructionReportId);
            BLL.ConstructionReportService.DeleteConstructionReportById(ConstructionReportId);
            BLL.LogService.AddSys_Log(this.CurrUser, constructionReport.Code, ConstructionReportId, Const.ConstructionReportMenuId, "删除总承包商施工报告");
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
            string ConstructionReportId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(window_tt.GetShowReference(String.Format("ConstructionReportView.aspx?ConstructionReportId={0}", ConstructionReportId, "查看 - ")));
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