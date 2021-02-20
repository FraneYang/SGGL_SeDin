namespace FineUIPro.Web.HSSE.Law
{
    using BLL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Text;
    using System.Web.UI;

    public partial class LawRegulationList : PageBase
    {
        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //权限设置
                this.GetButtonPower();
                Funs.DropDownPageSize(this.ddlPageSize);
                btnNew.OnClientClick = Window1.GetShowReference("LawRegulationListEdit.aspx") + "return false;";                
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                BLL.LawsRegulationsTypeService.InitLawsRegulationsTypeDropDownList(this.drpType, true);
                // 绑定表格
                this.BindGrid();
            }
            else
            {
                if (GetRequestEventArgument() == "reloadGrid")
                {
                    this.BindGrid();
                }
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT Law.LawRegulationId
                                                ,sysConstStates.ConstText AS ReleaseStatesName 
                                                ,Law.LawRegulationName
                                                ,Law.LawRegulationCode
                                                ,Law.LawsRegulationsTypeId
                                                ,LawType.Code AS  LawsRegulationsTypeCode
                                                ,LawType.Name AS LawsRegulationsTypeName
                                                ,Law.ReleaseUnit
                                                ,Law.ApprovalDate
                                                ,Law.EffectiveDate
                                                ,Law.AbolitionDate
                                                ,Law.ReplaceInfo
                                                ,(CASE WHEN LEN(Law.ReplaceInfo) > 45 THEN LEFT(Law.ReplaceInfo,45) + '...' ELSE Law.ReplaceInfo END) AS ShortReplaceInfo
                                                ,Law.Description
                                                ,(CASE WHEN LEN(Law.Description) > 45 THEN LEFT(Law.Description,45) + '...' ELSE Law.Description END) AS ShortDescription 
                                                ,Law.CompileMan
                                                ,Law.CompileDate
                                                ,Law.IsBuild
                                                ,(CASE WHEN IsPass=1 THEN '' ELSE '' END) AS IsPassName
                                                , Substring(Law.AttachUrl,charindex('~',Law.AttachUrl)+1,LEN(Law.AttachUrl)) as  AttachUrlName
                                                ,Law.UnitId,(CASE WHEN IsBuild = 1 THEN '集团' ELSE '' END ) AS IsBuildName
                                                ,IndexesNames = STUFF((SELECT ',' + ConstText FROM Sys_Const as c
				                                                    where c.GroupId='HSSE_Indexes' AND PATINDEX('%,' + RTRIM(C.ConstValue) + ',%',',' + Law.IndexesIds + ',')>0
					                                                ORDER BY PATINDEX('%,' + RTRIM(Law.IndexesIds) + ',%',',' + Law.IndexesIds + ',')
					                                                FOR XML PATH('')), 1, 1,'')
                                                FROM dbo.Law_LawRegulationList AS Law
                                                LEFT JOIN dbo.Base_LawsRegulationsType AS  LawType ON LawType.Id=Law.LawsRegulationsTypeId                                                
                                                LEFT JOIN Sys_Const AS sysConstStates ON sysConstStates.GroupId='HSSE_ReleaseStates' 
	                                                AND Law.ReleaseStates=sysConstStates.ConstValue
                                                WHERE 1=1";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.txtLawRegulationName.Text.Trim()))
            {
                strSql += " AND LawRegulationName LIKE @LawRegulationName";
                listStr.Add(new SqlParameter("@LawRegulationName", "%" + this.txtLawRegulationName.Text.Trim() + "%"));
            }
            if (this.drpType.SelectedValue != Const._Null)
            {
                strSql += " AND Law.LawsRegulationsTypeId   =@TypeId";
                listStr.Add(new SqlParameter("@TypeId", this.drpType.SelectedValue));
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(Grid1, tb1);
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region 删除数据
        /// <summary>
        /// 批量删除数据
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
                    var law = BLL.LawRegulationListService.GetLawRegulationListById(rowID);
                    if (law != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, law.LawRegulationCode, law.LawRegulationId, BLL.Const.LawRegulationListMenuId, BLL.Const.BtnModify);
                        BLL.LawRegulationListService.DeleteLawRegulationListById(rowID);
                    }
                }

                BindGrid();
                ShowNotify("删除数据成功!");
            }
        }
        #endregion

        #region 数据编辑事件
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
                Alert.ShowInTop("请选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string LawRegulationId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("LawRegulationListEdit.aspx?LawRegulationId={0}", LawRegulationId, "编辑 - ")));
        }
        #endregion

        #region Grid 事件
             /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// 分页显示条数下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }
        #endregion

        #region 关闭窗口
        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion
        
        #region 获取权限按钮
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.LawRegulationListMenuId);
            if (buttonList.Count > 0)
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

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("法律法规" + filename, System.Text.Encoding.UTF8) + ".xls");
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