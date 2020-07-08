namespace FineUIPro.Web.BaseInfo
{
    using BLL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public partial class SafetyMeasures : PageBase
    {
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
                this.btnNew.OnClientClick = Window1.GetShowReference("SafetyMeasuresEdit.aspx") + "return false;";               
                Funs.DropDownPageSize(this.ddlPageSize);
                ConstValue.InitConstValueDropDownList(this.drpLicenseType, ConstValue.Group_LicenseType, true);
                // 绑定表格
                this.BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT SafetyMeasures.SafetyMeasuresId,SafetyMeasures.SortIndex,SafetyMeasures.SafetyMeasures,SafetyMeasures.LicenseType,Const.ConstText AS LicenseTypeName"
                          + @" FROM Base_SafetyMeasures AS SafetyMeasures"
                          + @" LEFT JOIN Sys_Const AS Const ON Const.GroupId ='"+BLL.ConstValue.Group_LicenseType+"' AND SafetyMeasures.LicenseType=Const.ConstValue "
                          + @" WHERE 1 = 1";
            List<SqlParameter> listStr = new List<SqlParameter>();         
            if (!string.IsNullOrEmpty(this.txtSafetyMeasures.Text.Trim()))
            {
                strSql += " AND SafetyMeasuress.SafetyMeasures LIKE @SafetyMeasures";
                listStr.Add(new SqlParameter("@SafetyMeasures", "%" + this.txtSafetyMeasures.Text.Trim() + "%"));
            }
            if (this.drpLicenseType.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND SafetyMeasures.LicenseType = @LicenseTypeId";
                listStr.Add(new SqlParameter("@LicenseTypeId", this.drpLicenseType.SelectedValue));
            }      
           
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();           
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

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.SafetyMeasuresMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
                if (buttonList.Contains(Const.BtnModify))
                {
                    this.btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
                }
            }
        }
        #endregion
        
        #region  删除数据
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
                    if (judgementDelete(rowID, false))
                    {
                        var getD = SafetyMeasuresService.GetSafetyMeasuresBySafetyMeasuresId(rowID);
                        if (getD != null)
                        {
                            LogService.AddSys_Log(this.CurrUser, getD.SortIndex.ToString(), getD.SafetyMeasuresId, Const.SafetyMeasuresMenuId, BLL.Const.BtnDelete);
                            BLL.SafetyMeasuresService.DeleteSafetyMeasuresById(rowID);
                        }
                    }
                }
                BindGrid();
                ShowNotify("删除数据成功!");
            }
        }
        #endregion

        #region 分页
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {           
            BindGrid();
        }

        /// <summary>
        /// 分页显示条数下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Grid1.PageSize = Funs.GetNewIntOrZero(this.ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            BindGrid();
        }
        #endregion

        /// <summary>
        /// Grid行双击事件
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
                Alert.ShowInParent("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string Id = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SafetyMeasuresEdit.aspx?SafetyMeasuresId={0}", Id, "编辑 - ")));
        }

        #region 判断是否可删除
        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private bool judgementDelete(string id, bool isShow)
        {
            string content = string.Empty;
            //if (new Model.SGGLDB(Funs.ConnString).Project_ProjectSafetyMeasures.FirstOrDefault(x => x.SafetyMeasuresId == id) != null)
            //{
            //    content = "该用户已在【项目用户】中使用，不能删除！";
            //}
            
            if (string.IsNullOrEmpty(content))
            {
                return true;
            }
            else
            {
                if (isShow)
                {
                    Alert.ShowInTop(content);
                }
                return false;
            }
        }
        #endregion

       
    }
}