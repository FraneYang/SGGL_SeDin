using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.ProjectData
{
    public partial class ProjectList : PageBase
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
                ////权限按钮方法
                this.GetButtonPower();
                Funs.DropDownPageSize(this.ddlPageSize);
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
            string strSql = "SELECT Project.ProjectId,Project.ProjectCode,Project.ProjectName,unit.UnitName,Project.StartDate,Project.EndDate,Project.ProjectAddress,ShortName, "
                          + @" (CASE WHEN ProjectState='" + BLL.Const.ProjectState_2 + "' THEN '暂停中' WHEN ProjectState='" + BLL.Const.ProjectState_3 + "' THEN '已完工'  ELSE '施工中' END) AS ProjectStateName,Project.ProjectState"
                          + @" ,ProjectMoney,DATEDIFF(DAY,Project.StartDate,GETDATE()) AS DayCount,ProjectType.ProjectTypeName AS ProjectTypeName"
                          + @" FROM Base_Project AS Project LEFT JOIN Base_Unit as unit on unit.UnitId=Project.UnitId " 
                          + @" LEFT JOIN Base_ProjectType AS ProjectType ON Project.ProjectType =ProjectType.ProjectTypeId"
                          + @" WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();            
            //if (!string.IsNullOrEmpty(Request.Params["projectId"]))  ///是否文件柜查看页面传项目值
            //{
            //    strSql += " AND ProjectId = @ProjectId";
            //    listStr.Add(new SqlParameter("@ProjectId", Request.Params["projectId"]));               
            //}
            //else if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
            //{
            //    strSql += " AND ProjectId = @ProjectId";
            //    listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            //}

            if (!string.IsNullOrEmpty(this.txtProjectName.Text.Trim()))
            {
                strSql += " AND ProjectName LIKE @ProjectName";
                listStr.Add(new SqlParameter("@ProjectName", "%" + this.txtProjectName.Text.Trim() + "%"));
            }

            if (this.ckState.SelectedValue != "0")
            {
                if (this.ckState.SelectedValue == "1")
                {
                    strSql += " AND (ProjectState = '1' OR ProjectState IS NULL)";
                }
                else
                {
                    strSql += " AND (ProjectState = @states )";
                    listStr.Add(new SqlParameter("@states", this.ckState.SelectedValue));
                }
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;         
            Grid1.DataSource = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataBind();
        }
        #endregion

        #region Grid
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
        #endregion

        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ProjectSetView.aspx?ProjectId={0}", Grid1.SelectedRowID, "查看 - ")));
        }

        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnView_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ProjectSetView.aspx?ProjectId={0}", Grid1.SelectedRowID, "查看 - ")));
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

            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.SeverProjectListMenuId);
            if (buttonList.Count() > 0)
            {
            }
        }
        #endregion

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

        #region 项目经理
        /// <summary>
        /// 施工经理
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        protected string ConvertConstructionManager(object projectId)
        {
            return BLL.ProjectService.GetConstructionManagerName(projectId.ToString());
        }

        /// <summary>
        /// 施工分包商
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        protected string ConvertSubcontractor(object projectId)
        {
            string unitName = string.Empty;
            if (projectId != null)
            {
                var getUnitName = from x in Funs.DB.Project_ProjectUnit
                                  join y in Funs.DB.Base_Unit on x.UnitId equals y.UnitId
                                  where x.ProjectId == projectId.ToString() && x.UnitType == Const.ProjectUnitType_2
                                  select y.UnitName;
                if (getUnitName.Count() > 0)
                {
                    unitName = Funs.GetStringByArray(getUnitName.ToArray());
                }
            }
            return unitName;
        }

        protected string ConvertOwn(object projectId)
        {
            string name = string.Empty;
            if (projectId != null)
            {
                var getUnitt = Funs.DB.Project_ProjectUnit.FirstOrDefault(x => x.ProjectId == projectId.ToString() && x.UnitType == Const.ProjectUnitType_4);
                if (getUnitt != null)
                {
                    name = UnitService.GetUnitNameByUnitId(getUnitt.UnitId);
                }

            }
            return name;
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("项目信息" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = this.Grid1.RecordCount;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion

        /// <summary>
        /// 进入项目现场
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEnter_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Grid1.SelectedRowID))
            {
                string url = "~/indexProject.aspx?projectId=" + Grid1.SelectedRowID;
                UserService.UpdateLastUserInfo(this.CurrUser.UserId, null, false, Grid1.SelectedRowID);
                PageContext.Redirect(url, "_top");
            }
            else
            {
                ShowNotify("请选择项目进入！", MessageBoxIcon.Warning);
            }
        }
    }
}