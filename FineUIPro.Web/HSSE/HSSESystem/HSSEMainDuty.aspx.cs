using BLL;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.HSSE.HSSESystem
{
    public partial class HSSEMainDuty : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Funs.DropDownPageSize(this.ddlPageSize);
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                this.GetButtonPower();
               
                BindGrid2();
                BindGrid1();
            }
        }

        #region 加载Grid
        /// <summary>
        /// 加载Grid2
        /// </summary>
        private void BindGrid2()
        {
            Grid2.DataSource = from x in Funs.DB.Base_WorkPost select x;
            Grid2.DataBind();
            Grid2.SelectedRowIndex = 0;
        }

        /// <summary>
        /// 加载Grid1
        /// </summary>
        private void BindGrid1()
        {
            if (Grid2.SelectedRowIndex < 0)
            {
                return;
            }
            //string workPostId = Convert.ToString(Grid2.DataKeys[Grid2.SelectedRowIndex][0]);
            string workPostId = Grid2.SelectedRowID;
            if (!string.IsNullOrEmpty(workPostId))
            {
                string strSql = "select * from dbo.HSSESystem_HSSEMainDuty where WorkPostId = @WorkPostId";
                SqlParameter[] parameter = new SqlParameter[]       
                    {
                        new SqlParameter("@WorkPostId",workPostId),
                    };
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
                Grid1.RecordCount = tb.Rows.Count;
                var table = this.GetPagedDataTable(Grid1, tb);

                Grid1.DataSource = table;
                Grid1.DataBind();
            }
        }
        #endregion

        /// <summary>
        /// Grid2行选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid2_RowSelect(object sender, GridRowSelectEventArgs e)
        {
            BindGrid1();
        }
        
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid1();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid1();
        }

        /// <summary>
        /// Grid1排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid1();
        }

        #region 增加
        /// <summary>
        /// 增加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            //string workPostId = Convert.ToString(Grid2.DataKeys[Grid2.SelectedRowIndex][0]);
            string workPostId = Grid2.SelectedRowID;
            if (!string.IsNullOrEmpty(workPostId))
            {
              
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HSSEMainDutyEdit.aspx?WorkPostId={0}", workPostId, "编辑 - ")));
              
            }
            else
            {
                ShowNotify("请选择一个单位！",MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 编辑按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            this.EditData();
        }
        
        /// <summary>
        /// Grid1行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 编辑数据方法
        /// </summary>
        private void EditData()
        {
           
                if (Grid1.SelectedRowIndexArray.Length == 0)
                {
                    Alert.ShowInTop("请至少选择一条记录！",MessageBoxIcon.Warning);
                    return;
                }
                string Id = Grid1.SelectedRowID;
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HSSEMainDutyEdit.aspx?HSSEMainDutyId={0}", Id, "编辑 - ")));
           
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            this.DeleteData();
        }  
        
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            this.DeleteData();
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        private void DeleteData()
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var getV = BLL.HSSEMainDutyService.GetHSSEMainDutyById(rowID);
                    if (getV != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, getV.SortIndex, getV.HSSEMainDutyId, BLL.Const.HSSEMainDutyMenuId, BLL.Const.BtnDelete);
                        BLL.HSSEMainDutyService.DeleteHSSEMainDuty(rowID);
                    }
                }

                BindGrid1();
                ShowNotify("删除数据成功!");
            } }
        #endregion

        #region 关闭窗口
        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid1();
        }
        #endregion

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSEMainDutyMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
                }
                
            }
        }
        #endregion
    }
}