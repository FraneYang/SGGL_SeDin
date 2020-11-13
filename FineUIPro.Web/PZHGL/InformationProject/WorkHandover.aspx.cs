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
    public partial class WorkHandover : PageBase
    {
        /// <summary>
        /// 工程联系单主键
        /// </summary>
        public string WorkHandoverId
        {
            get
            {
                return (string)ViewState["WorkHandoverId"];
            }
            set
            {
                ViewState["WorkHandoverId"] = value;
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
            var buttonList = CommonService.GetAllButtonList(CurrUser.LoginProjectId, CurrUser.UserId, Const.WorkHandoverMenuId);
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
                btnNew.OnClientClick = window_tt.GetShowReference("WorkHandoverEdit.aspx", "工作交接") + "return false;";
                GetButtonPower();
                BindGrid();
            }
        }
        protected DataTable ChecklistData()
        {
            string strSql = @"SELECT chec.WorkHandoverId,chec.ProjectId,chec.TransferDate,chec.State,chec.TransferMan,u1.userName as TransferManName,u2.userName as ReceiveManName"
                          + @" FROM ZHGL_WorkHandover chec "
                          + @" left join sys_User u1 on u1.userId = chec.TransferMan"
                          + @" left join sys_User u2 on u2.userId = chec.ReceiveMan"
                          + @" where chec.ProjectId=@ProjectId";

            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", CurrUser.LoginProjectId));
            strSql += " order by chec.TransferDate desc ";
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
            WorkHandoverId = Grid1.SelectedRowID.Split(',')[0];
            Model.ZHGL_WorkHandover WorkHandover = BLL.WorkHandoverService.GetWorkHandoverById(WorkHandoverId);
            Model.ZHGL_WorkHandoverApprove approve = BLL.WorkHandoverApproveService.GetWorkHandoverApproveByWorkHandoverId(WorkHandoverId);
            if (WorkHandover.State == BLL.Const.WorkHandover_Complete)
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
                        PageContext.RegisterStartupScript(window_tt.GetShowReference(String.Format("WorkHandoverEdit.aspx?WorkHandoverId={0}", WorkHandoverId, "编辑 - ")));

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
            WorkHandoverId = Grid1.SelectedRowID.Split(',')[0];
            var workHandover = WorkHandoverService.GetWorkHandoverById(WorkHandoverId);
            BLL.WorkHandoverApproveService.DeleteWorkHandoverApproveByWorkHandoverId(WorkHandoverId);
            BLL.WorkHandoverDetailService.DeleteMonthSpotCheckDetailsByWorkHandoverId(WorkHandoverId);
            BLL.WorkHandoverService.DeleteWorkHandoverById(WorkHandoverId);
            var user = BLL.UserService.GetUserByUserId(workHandover.TransferMan);
            BLL.LogService.AddSys_Log(this.CurrUser, user.UserName, WorkHandoverId, Const.WorkHandoverMenuId, "删除工作交接");
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
            string WorkHandoverId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(window_tt.GetShowReference(String.Format("WorkHandoverView.aspx?WorkHandoverId={0}", WorkHandoverId, "查看 - ")));
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