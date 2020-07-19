using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace FineUIPro.Web.PZHGL.ProjectData
{
    public partial class ProjectPageData : PageBase
    {
        #region 加载
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                Funs.DropDownPageSize(this.ddlPageSize);
                ////权限按钮方法
                this.GetButtonPower();               
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                } 
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                this.BindGrid();
            }
        }
        #endregion

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = "SELECT PageDataId,ProjectId,CreatDate,CreatManId,SafeHours,SitePersonNum,SpecialEquipmentNum,EntryTrainingNum,HiddenDangerNum,RectificationNum,RiskI,RiskII,RiskIII,RiskIV,RiskV"
                          + @" FROM dbo.Wx_PageData"
                          + @" WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND ProjectId = @ProjectId";
            if (!string.IsNullOrEmpty(Request.Params["ProjectId"]))  ///是否文件柜查看页面传项目值
            {
                listStr.Add(new SqlParameter("@ProjectId", Request.Params["ProjectId"]));
            }
            else if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
            {
                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }
            
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;            
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        //#region 操作 Events
        ///// <summary>
        ///// 右键删除事件
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnMenuDelete_Click(object sender, EventArgs e)
        //{
        //    if (Grid1.SelectedRowIndexArray.Length > 0)
        //    {
        //        bool isShow = false;
        //        if (Grid1.SelectedRowIndexArray.Length == 1)
        //        {
        //            isShow = true;
        //        }
        //        foreach (int rowIndex in Grid1.SelectedRowIndexArray)
        //        {
        //            string rowID = Grid1.DataKeys[rowIndex][0].ToString();
        //            if (this.judgementDelete(rowID, isShow))
        //            {                     
        //            }
        //        }

        //        BindGrid();
        //        ShowNotify("操作完成!", MessageBoxIcon.Success);
        //    }
        //}
        //#endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            BindGrid();
        }
        
        /// <summary>
        /// 双击事件
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
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }

            var project = ProjectPageDataService.GetPageDataByPageDataId(Grid1.SelectedRowID);
            if (project != null && !this.btnMenuEdit.Hidden)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ProjectPageDataSave.aspx?PageDataId={0}", Grid1.SelectedRowID, "编辑 - ")));
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
            string menuId = Const.ProjectPageDataMenuId;
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, menuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(Const.BtnSave))
                {
                    this.btnNew.Hidden = false;
                    this.btnMenuEdit.Hidden = false;
                }
            }
        }
        #endregion

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("移动端首页信息" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = Encoding.UTF8;
            this.Grid1.PageSize = this.Grid1.Rows.Count();
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion

        protected void btnNew_Click(object sender, EventArgs e)
        {
            var getData = Funs.DB.Wx_PageData.FirstOrDefault(x => x.ProjectId == this.CurrUser.LoginProjectId && x.CreatDate.Value.Year == DateTime.Now.Year
                                    && x.CreatDate.Value.Month == DateTime.Now.Month && x.CreatDate.Value.Day == DateTime.Now.Day);
            if (getData == null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ProjectPageDataSave.aspx", "新增 - ")));
            }
            else
            {
                Alert.Show("当日数据已存在！", MessageBoxIcon.Warning);
            }
        }
    }
}