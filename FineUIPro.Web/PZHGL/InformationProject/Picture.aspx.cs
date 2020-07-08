using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.PZHGL.InformationProject
{
    public partial class Picture : PageBase
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
                this.btnNew.OnClientClick = Window1.GetShowReference("PictureEdit.aspx") + "return false;";
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                }
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();

                this.BindGrid2();
                if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
                {
                    this.drpProject.Value = this.CurrUser.LoginProjectId;
                    this.drpProject.Enabled = false;
                }
                if (!string.IsNullOrEmpty(Request.Params["projectId"]))
                {
                    this.drpProject.Value = Request.Params["projectId"];
                    this.drpProject.Enabled = false;
                }              
                // 绑定表格
                this.BindGrid();
            }
        }
        #endregion

        #region 项目下拉框绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid2()
        {
            string strSql = @"SELECT * FROM Base_Project WHERE ProjectType != '5'";

            if (!string.IsNullOrEmpty(strSql))
            {
                List<SqlParameter> listStr = new List<SqlParameter>();
                if (this.cbType.SelectedValue == "1")
                {
                    strSql += " AND (ProjectState IS NULL OR ProjectState = '" + BLL.Const.ProjectState_1 + "')";
                }
                else
                {
                    strSql += " AND (ProjectState = '" + BLL.Const.ProjectState_2 + "' OR ProjectState = '" + BLL.Const.ProjectState_3 + "')";
                }
                if (!string.IsNullOrEmpty(this.txtProjectName.Text.Trim()))
                {
                    strSql += " AND ProjectName LIKE @ProjectName";
                    listStr.Add(new SqlParameter("@ProjectName", "%" + this.txtProjectName.Text.Trim() + "%"));
                }
                if (!string.IsNullOrEmpty(this.txtProjectCode.Text.Trim()))
                {
                    strSql += " AND ProjectCode LIKE @ProjectCode";
                    listStr.Add(new SqlParameter("@ProjectCode", "%" + this.txtProjectCode.Text.Trim() + "%"));
                }
                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
                Grid2.RecordCount = tb.Rows.Count;                
                var table = this.GetPagedDataTable(Grid2, tb);
                Grid2.DataSource = table;
                Grid2.DataBind();
            }
            else
            {
                Grid2.DataSource = null;
                Grid2.DataBind();
            }
        }
        #endregion

        #region 绑定数据GV
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = "SELECT Picture.PictureId, Picture.ProjectId,Picture.Title,Picture.ContentDef, PictureType.Name AS PictureTypeName,Picture.UploadDate,Picture.States,Picture.AttachUrl,CodeRecords.Code,"
                        + @" (CASE WHEN LEN(Picture.ContentDef) > 40 THEN LEFT(Picture.ContentDef,40)+'...' ELSE Picture.ContentDef END) AS ShortContentDef, Users.UserName AS CompileManName,"
                        + @" (CASE WHEN Picture.States = " + BLL.Const.State_0 + " OR Picture.States IS NULL THEN '待['+OperateUser.UserName+']提交' WHEN Picture.States =  " + BLL.Const.State_2 + " THEN '审核/审批完成' ELSE '待['+OperateUser.UserName+']办理' END) AS  FlowOperateName "
                        + @" FROM InformationProject_Picture AS Picture "
                        + @" LEFT JOIN Sys_CodeRecords AS CodeRecords ON Picture.PictureId=CodeRecords.DataId  "
                        + @" LEFT JOIN Sys_FlowOperate AS FlowOperate ON Picture.PictureId=FlowOperate.DataId AND FlowOperate.IsClosed <> 1 "
                        + @" LEFT JOIN Sys_User AS OperateUser ON FlowOperate.OperaterId=OperateUser.UserId "
                        + @" LEFT JOIN Base_PictureType AS PictureType ON PictureType.PictureTypeId=Picture.PictureType "
                        + @" LEFT JOIN Sys_User AS Users ON Picture.CompileMan=Users.UserId WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND Picture.ProjectId = @ProjectId";
            if (!string.IsNullOrEmpty(Request.Params["projectId"]))  ///是否文件柜查看页面传项目值
            {
                listStr.Add(new SqlParameter("@ProjectId", Request.Params["projectId"]));
                strSql += " AND Picture.States = @States";  ///状态为已完成
                listStr.Add(new SqlParameter("@States", BLL.Const.State_2));
            }
            else
            {
                if (!String.IsNullOrEmpty(this.drpProject.Value))
                {
                    listStr.Add(new SqlParameter("@ProjectId", this.drpProject.Value));
                }
                else
                {
                    listStr.Add(new SqlParameter("@ProjectId", BLL.Const._Null));
                }
            }

            if (!string.IsNullOrEmpty(this.txtTitle.Text.Trim()))
            {
                strSql += " AND Picture.Title LIKE @Title";
                listStr.Add(new SqlParameter("@Title", "%" + this.txtTitle.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;            
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region 分页 排序
        /// <summary>
        /// 改变索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 分页下拉选择事件
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
            BindGrid();
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

        #region 编辑
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
            string id = Grid1.SelectedRowID;
            var picture = BLL.PictureService.GetPictureById(id);
            if (picture != null)
            { 
                if (this.btnMenuEdit.Hidden || picture.States == BLL.Const.State_2)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PictureView.aspx?PictureId={0}", id, "查看 - ")));                    
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PictureEdit.aspx?PictureId={0}", id, "编辑 - ")));                    
                }
            } 
        }
        #endregion 

        #region 删除
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                bool isShow = false;
                if (Grid1.SelectedRowIndexArray.Length == 1)
                {
                    isShow = true;
                }
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    if (this.judgementDelete(rowID, isShow))
                    {
                        var p = BLL.PictureService.GetPictureById(rowID);
                        if (p != null)
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, p.Title, p.PictureId, BLL.Const.ProjectPictureMenuId, BLL.Const.BtnDelete);
                            BLL.PictureService.deletePictureById(rowID);
                        }
                    }
                }
                BindGrid();
                ShowNotify("删除数据成功!（表格数据已重新绑定）", MessageBoxIcon.Success);
            }
        }

        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private bool judgementDelete(string id, bool isShow)
        {
            string content = string.Empty;

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
            string menuId = BLL.Const.ServerPictureMenuId;
            if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
            {
                menuId = BLL.Const.ProjectPictureMenuId;
            }

            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, menuId);
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

        /// <summary>
        /// 项目下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpProject_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("项目图片" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = this.Grid1.RecordCount;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindGrid2();            
            this.BindGrid();
        }
    }
}