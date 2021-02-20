using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace FineUIPro.Web.HSSE.Law
{
    public partial class HSSEStandardList : PageBase
    {
        #region 加载页面
        protected void Page_Load(object sender, EventArgs e)
        {          
            if (!IsPostBack)
            {
                Funs.DropDownPageSize(this.ddlPageSize);
                this.GetButtonPower();//设置权限
                btnNew.OnClientClick = Window1.GetShowReference("HSSEStandardListSave.aspx") + "return false;";                             
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                BLL.HSSEStandardListTypeService.InitStandardListTypeDropDownList(this.drpType, true);
                // 绑定表格
                BindGrid();
            }
            else
            {
                if (GetRequestEventArgument() == "reloadGrid")
                {
                    BindGrid();
                }
            }
        } 

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT hsl.StandardId,sysConstStates.ConstText AS ReleaseStatesName 
                            ,hsl.StandardName,hsl.StandardNo,hsl.TypeId,hslt.TypeCode,hslt.TypeName
                            ,hsl.ReleaseUnit,hsl.ApprovalDate,hsl.EffectiveDate,hsl.AbolitionDate,hsl.ReplaceInfo,Description
                            ,hsl.CompileMan,hsl.CompileDate,hsl.IsPass,(CASE WHEN IsBuild = 1 THEN '集团' ELSE '' END ) AS IsBuildName
                            ,hsl.UnitId,hsl.UpState,hsl.IsBuild
                            ,IndexesNames = STUFF((SELECT ',' + ConstText FROM Sys_Const as c
				                                where c.GroupId='HSSE_Indexes' AND PATINDEX('%,' + RTRIM(C.ConstValue) + ',%',',' + hsl.IndexesIds + ',')>0
					                            ORDER BY PATINDEX('%,' + RTRIM(hsl.IndexesIds) + ',%',',' + hsl.IndexesIds + ',')
					                            FOR XML PATH('')), 1, 1,'')
                            FROM Law_HSSEStandardsList as hsl
                            LEFT JOIN Base_HSSEStandardListType AS hslt ON hslt.TypeId = hsl.TypeId 
                            LEFT JOIN Sys_Const AS sysConstStates ON sysConstStates.GroupId='HSSE_ReleaseStates' 
	                            AND hsl.ReleaseStates=sysConstStates.ConstValue
                            WHERE 1=1";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.txtStandardNo.Text.Trim()))
            {
                strSql += " AND StandardNo LIKE @StandardNo";
                listStr.Add(new SqlParameter("@StandardNo", "%" + this.txtStandardNo.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtStandardName.Text.Trim()))
            {
                strSql += " AND StandardName LIKE @StandardName";
                listStr.Add(new SqlParameter("@StandardName", "%" + this.txtStandardName.Text.Trim() + "%"));
            }
            if (this.drpType.SelectedValue != Const._Null)
            {
                strSql += " AND hsl.TypeId  =@TypeId";
                listStr.Add(new SqlParameter("@TypeId", this.drpType.SelectedValue));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion
        
        #region 删除
        // 删除数据
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
                    var getV = BLL.HSSEStandardsListService.GetHSSEStandardsListByHSSEStandardsListId(rowID);
                    if (getV != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser,getV.StandardNo,getV.StandardId,BLL.Const.HSSEStandardListMenuId,BLL.Const.BtnDelete);
                        BLL.HSSEStandardsListService.DeleteHSSEStandardsList(rowID);
                    }
                }
                
                BindGrid();
                ShowNotify("删除数据成功!");
            }
        }
        #endregion

        #region 分页
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }
        #endregion

        #region 关闭弹出框
        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 编辑
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

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
                Alert.ShowInTop("请选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string standardId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HSSEStandardListSave.aspx?StandardId={0}", standardId, "编辑 - ")));
        }

        #endregion
        
        #region 文本框查询事件
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

        #region 按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSEStandardListMenuId);
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
                if (buttonList.Contains(BLL.Const.BtnOut))
                {
                    this.btnOut.Hidden = false;
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("标准规范" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = Encoding.UTF8;
            this.Grid1.PageSize = this.Grid1.RecordCount;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion
    }
}