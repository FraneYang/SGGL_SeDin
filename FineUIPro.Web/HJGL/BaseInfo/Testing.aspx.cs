using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class Testing : PageBase
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
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();

                //this.drpSysType.DataTextField = "Text";
                //this.drpSysType.DataValueField = "Value";
                //this.drpSysType.DataSource = BLL.DropListService.HJGL_GetTestintTypeList();
                //this.drpSysType.DataBind();
                //Funs.FineUIPleaseSelect(this.drpSysType);
                // 绑定表格
                this.BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT DetectionTypeId,DetectionTypeCode,DetectionTypeName,SysType,SecuritySpace,InjuryDegree,Remark"
                         + @" FROM dbo.Base_DetectionType WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(this.txtDetectionTypeCode.Text.Trim()))
            {
                strSql += " AND DetectionTypeCode LIKE @DetectionTypeCode";
                listStr.Add(new SqlParameter("@DetectionTypeCode", "%" + this.txtDetectionTypeCode.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtDetectionTypeName.Text.Trim()))
            {
                strSql += " AND DetectionTypeName = @DetectionTypeName";
                listStr.Add(new SqlParameter("@DetectionTypeName", this.txtDetectionTypeName.Text.Trim()));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            //tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

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

        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 增加按钮事件
        /// <summary>
        /// 增加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_DetectionTypeMenuId, Const.BtnAdd))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("TestingEdit.aspx", "新增 - ")));
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
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
                Alert.ShowInTop("请至少选择一条记录", MessageBoxIcon.Warning);
                return;
            }

            ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
            if (GetButtonPower(Const.BtnModify))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("TestingEdit.aspx?DetectionTypeId={0}", Grid1.SelectedRowID, "编辑 - ")));
            }
            else if (GetButtonPower(Const.BtnAdd))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("TestingView.aspx?DetectionTypeId={0}", Grid1.SelectedRowID, "查看 - ")));
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
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
            if (GetButtonPower(Const.BtnDelete))
            {
                if (Grid1.SelectedRowIndexArray.Length > 0)
                {
                    string strShowNotify = string.Empty;
                    foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                    {
                        string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                        var getDetectionType = BLL.Base_DetectionTypeService.GetDetectionTypeByDetectionTypeId(rowID);
                        if (getDetectionType != null)
                        {
                            string cont = judgementDelete(rowID);
                            if (string.IsNullOrEmpty(cont))
                            {
                                BLL.Base_DetectionTypeService.DeleteDetectionTypeByDetectionTypeId(rowID);
                                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_DetectionTypeMenuId, Const.BtnDelete, rowID);
                            }
                            else
                            {
                                strShowNotify += "检测方法" + "：" + getDetectionType.DetectionTypeCode + cont;
                            }
                        }
                    }

                    BindGrid();
                    if (!string.IsNullOrEmpty(strShowNotify))
                    {
                        Alert.ShowInTop(strShowNotify, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        ShowNotify("删除成功！", MessageBoxIcon.Success);
                    }
                }
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }

        #region 判断是否可删除
        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private string judgementDelete(string id)
        {
            string content = string.Empty;
            if (Funs.DB.Base_WeldType.FirstOrDefault(x => x.DetectionType.Contains(id)) != null)
            {
                content += "已在【管线等级】中使用，不能删除！";
            }
            if (Funs.DB.HJGL_Pipeline.FirstOrDefault(x => x.DetectionType.Contains(id)) != null)
            {
                content += "已在【管线信息】中使用，不能删除！";
            }

            return content;
        }
        #endregion
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region 查看
        /// <summary>
        /// 查看按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnView_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("TestingView.aspx?DetectionTypeId={0}", Grid1.SelectedRowID, "查看 - ")));
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 得到类型类型
        /// </summary>
        /// <param name="SysType"></param>
        /// <returns></returns>
        protected string ConvertSysType(object SysType)
        {
            string name = string.Empty;
            if (SysType != null)
            {
                var dropValue = BLL.DropListService.HJGL_GetTestintTypeList().FirstOrDefault(x => x.Value == SysType.ToString());
                if (dropValue != null)
                {
                    name = dropValue.Text;
                }
            }

            return name;
        }
        #endregion

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private bool GetButtonPower(string button)
        {
            return BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HJGL_DetectionTypeMenuId, button);
        }
        #endregion
    }
}