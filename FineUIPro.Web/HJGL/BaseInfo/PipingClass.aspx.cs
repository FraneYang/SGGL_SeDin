﻿using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class PipingClass : PageBase
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
            string strSql = @"SELECT PipingClassId,PipingClassCode,PipingClassName,Remark,PNO"
                         + @" FROM dbo.Base_PipingClass WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND ProjectId = @ProjectId";
            listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            if (!string.IsNullOrEmpty(this.txtPipingClassCode.Text.Trim()))
            {
                strSql += " AND PipingClassCode LIKE @PipingClassCode";
                listStr.Add(new SqlParameter("@PipingClassCode", "%" + this.txtPipingClassCode.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtPipingClassName.Text.Trim()))
            {
                strSql += " AND PipingClassName LIKE @PipingClassName";
                listStr.Add(new SqlParameter("@PipingClassName", "%" + this.txtPipingClassName.Text.Trim() + "%"));
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
            if (GetButtonPower(Const.BtnAdd))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PipingClassEdit.aspx", "新增 - ")));
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
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PipingClassEdit.aspx?PipingClassId={0}", Grid1.SelectedRowID, "编辑 - ")));
            }
            else if (GetButtonPower(Const.BtnSee))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PipingClassView.aspx?PipingClassId={0}", Grid1.SelectedRowID, "查看 - ")));
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
                        var getPipingClass = BLL.Base_PipingClassService.GetPipingClassByPipingClassId(rowID);
                        if (getPipingClass != null)
                        {
                            string cont = judgementDelete(rowID);
                            if (string.IsNullOrEmpty(cont))
                            {
                                BLL.Base_PipingClassService.DeletePipingClassByPipingClassId(rowID);
                                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_PipingClassMenuId, Const.BtnDelete, rowID);
                            }
                            else
                            {
                                strShowNotify += "管道等级" + "：" + getPipingClass.PipingClassCode + cont;
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
            //if (Funs.DB.Pipeline_Pipeline.FirstOrDefault(x => x.PipingClassId == id) != null)
            //{
            //    content += "已在【管线信息】中使用，不能删除！";
            //}

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

        #region 查看按钮
        /// <summary>
        /// 查看按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnView_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PipingClassView.aspx?PipingClassId={0}", Grid1.SelectedRowID, "查看 - ")));
        }
        #endregion

        #region Grid行点击事件
        /// <summary>
        /// Grid行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "ConditionalSettings")
            {
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("PipingClassDetail.aspx?PipingClassId={0}", Grid1.SelectedRowID, "条件设置 - ")));
            }
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
            return BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HJGL_PipingClassMenuId, button);
        }
        #endregion

        #region 导入
        /// <summary>
        /// 导入按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {
             PageContext.RegisterStartupScript(Window3.GetShowReference(String.Format("PipingClassIn.aspx", "导入 - ")));
        }
        #endregion
    }
}