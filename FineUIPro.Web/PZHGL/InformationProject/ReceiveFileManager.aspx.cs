using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace FineUIPro.Web.PZHGL.InformationProject
{
    public partial class ReceiveFileManager : PageBase
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
                this.btnNew.OnClientClick = Window1.GetShowReference("ReceiveFileManagerEdit.aspx") + "return false;";
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

        #region GV绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT ReceiveFileManager.ReceiveFileManagerId,(ReceiveFileManager.ReceiveFileManagerId+'#1') AS ReReceiveFileManagerId,ReceiveFileManager.ProjectId,CodeRecords.Code AS ReceiveFileCode,ReceiveFileManager.ReceiveFileName,ReceiveFileManager.Version,Unit.UnitName,ReceiveFileManager.FileCode,ReceiveFileManager.FilePageNum,ReceiveFileManager.GetFileDate,ReceiveFileManager.States "
                          + @" ,(CASE WHEN ReceiveFileManager.States = " + Const.State_0 + " OR ReceiveFileManager.States IS NULL THEN '待['+OperateUser.UserName+']提交' WHEN ReceiveFileManager.States =  " + BLL.Const.State_2 + " THEN '审核/审批完成' ELSE '待['+OperateUser.UserName+']办理' END) AS  FlowOperateName"
                          + @" ,(CASE WHEN ReceiveFileManager.States != 2 THEN '未审核完成' WHEN (SELECT COUNT(*) FROM AttachFile WHERE ToKeyId =(ReceiveFileManager.ReceiveFileManagerId+'#1')) > 0 THEN '已闭环' ELSE '未回执' END) AS  RetrunSateName"
                          + @" FROM InformationProject_ReceiveFileManager AS ReceiveFileManager  "
                          + @" LEFT JOIN Sys_CodeRecords AS CodeRecords ON ReceiveFileManager.ReceiveFileManagerId=CodeRecords.DataId "
                          + @" LEFT JOIN Sys_FlowOperate AS FlowOperate ON ReceiveFileManager.ReceiveFileManagerId=FlowOperate.DataId AND FlowOperate.IsClosed <> 1"
                          + @" LEFT JOIN Sys_User AS OperateUser ON FlowOperate.OperaterId=OperateUser.UserId"
                          + @" LEFT JOIN Base_Unit AS Unit ON ReceiveFileManager.FileUnitId=Unit.UnitId WHERE ReceiveFileManager.FileType=" + this.rbFileType.SelectedValue;
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND ReceiveFileManager.ProjectId = @ProjectId";
            if (!string.IsNullOrEmpty(Request.Params["projectId"]))  ///是否文件柜查看页面传项目值
            {
                listStr.Add(new SqlParameter("@ProjectId", Request.Params["projectId"]));
                strSql += " AND ReceiveFileManager.States = @States";  ///状态为已完成
                listStr.Add(new SqlParameter("@States", BLL.Const.State_2)); 
            }
            else
            {
                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }

            if (!string.IsNullOrEmpty(this.txtReceiveFileCode.Text.Trim()))
            {
                strSql += " AND ReceiveFileCode LIKE @ReceiveFileCode";
                listStr.Add(new SqlParameter("@ReceiveFileCode", "%" + this.txtReceiveFileCode.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtReceiveFileName.Text.Trim()))
            {
                strSql += " AND ReceiveFileManager.ReceiveFileName LIKE @ReceiveFileName";
                listStr.Add(new SqlParameter("@ReceiveFileName", "%" + this.txtReceiveFileName.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;            
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

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
            this.Grid1.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            this.BindGrid();
        }
        #endregion
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
            this.btnIssue.Hidden = true;
            if (this.rbFileType.SelectedValue == "1" && !this.btnNew.Hidden)
            {
                this.btnIssue.Hidden = false;
            }
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
            var InformationProject = BLL.ReceiveFileManagerService.GetReceiveFileManagerById(id);
            if (InformationProject != null)
            {
                if (this.btnMenuEdit.Hidden || InformationProject.States == BLL.Const.State_2)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ReceiveFileManagerView.aspx?ReceiveFileManagerId={0}", id, "查看 - ")));                    
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ReceiveFileManagerEdit.aspx?ReceiveFileManagerId={0}", id, "编辑 - ")));
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
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var ReceiveFileManager = BLL.ReceiveFileManagerService.GetReceiveFileManagerById(rowID);
                    if (ReceiveFileManager != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, ReceiveFileManager.ReceiveFileCode, ReceiveFileManager.ReceiveFileManagerId, BLL.Const.ReceiveFileManagerMenuId, BLL.Const.BtnDelete);
                        BLL.ReceiveFileManagerService.DeleteReceiveFileManagerById(rowID);
                    }
                }

                this.BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ReceiveFileManagerMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                    if (this.rbFileType.SelectedValue == "1")
                    {
                        this.btnIssue.Hidden = false;
                    }
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

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("一般来文管理" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = Encoding.UTF8;
            this.Grid1.PageSize = Grid1.RecordCount;
            this.BindGrid();
            Response.Write(PageBase.GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion

        #region 关闭弹出框
        protected void WindowAtt_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion

        protected void btnIssue_Click(object sender, EventArgs e)
        {
            var InformationProject = BLL.ReceiveFileManagerService.GetReceiveFileManagerById(Grid1.SelectedRowID);
            if (InformationProject != null && InformationProject.FileType == "1")
            {
                var getF = new Model.SGGLDB(Funs.ConnString).InformationProject_ReceiveFileManager.FirstOrDefault(x => x.FromId == InformationProject.ReceiveFileManagerId);
                if (getF == null)
                {
                    ReceiveFileManagerService.IssueReceiveFile(InformationProject.ReceiveFileManagerId);
                }
                else
                {
                    ShowNotify("已下发！", MessageBoxIcon.Warning);
                }
            }
            else {
                ShowNotify("非公司文件不能下发！", MessageBoxIcon.Warning);
            }
        }
    }
}