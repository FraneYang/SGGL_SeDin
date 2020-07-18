using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using BLL;

namespace FineUIPro.Web.HJGL.HotProcessHard
{
    public partial class HotProessReport : PageBase
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
                string hotProessTrustItemId = Request.Params["HotProessTrustItemId"];
                var hotProessFeedback = BLL.HotProessTrustItemService.GetHotProessTrustItemById(hotProessTrustItemId);
                if (hotProessFeedback.IsCompleted == true)
                {
                    ckbIsCompleted.Checked = true;
                }
                else
                {
                    ckbIsCompleted.Checked = false;
                }
                // 绑定表格
                this.BindGrid();
            }
        }

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT Report.HotProessReportId, 
                                     report.HotProessTrustItemId, 
                                     report.WeldJointId, 
                                     report.PointCount, 
                                     report.RequiredT,
                                     report.ActualT, 
                                     report.RequestTime, 
                                     report.ActualTime, 
                                     report.RecordChartNo,
                                     weldJoint.WeldJointCode
                         FROM HJGL_HotProess_Report AS report
                         LEFT JOIN HJGL_WeldJoint AS weldJoint ON weldJoint.WeldJointId=report.WeldJointId
                        WHERE report.HotProessTrustItemId=@HotProessTrustItemId";
            List<SqlParameter> listStr = new List<SqlParameter>();

            listStr.Add(new SqlParameter("@HotProessTrustItemId", Request.Params["HotProessTrustItemId"]));

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
        #endregion

        #region 增加按钮事件
        /// <summary>
        /// 增加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_HotProessTrustMenuId, Const.BtnAdd))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HotProessReportEdit.aspx?HotProessTrustItemId={0}", Request.Params["HotProessTrustItemId"], "新增 - ")));
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }
        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string hotProessTrustItemId = Request.Params["HotProessTrustItemId"];
            var hotProessFeedback = BLL.HotProessTrustItemService.GetHotProessTrustItemById(hotProessTrustItemId);
            if (ckbIsCompleted.Checked)
            {
                hotProessFeedback.IsCompleted = true;
                hotProessFeedback.IsHardness = true;
            }
            else
            {
                hotProessFeedback.IsCompleted = false;
                hotProessFeedback.IsHardness = false;
            }
            BLL.HotProessTrustItemService.UpdateHotProessFeedback(hotProessFeedback);
            Alert.ShowInTop("保存成功", MessageBoxIcon.Success);
        }

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
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_HotProessTrustMenuId, Const.BtnModify))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HotProessReportEdit.aspx?HotProessReportId={0}", Grid1.SelectedRowID, "编辑 - ")));
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
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_HotProessTrustMenuId, Const.BtnDelete))
            {
                if (Grid1.SelectedRowIndexArray.Length > 0)
                {
                    string strShowNotify = string.Empty;
                    foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                    {
                        string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                        var hotProessReport = BLL.HotProessReportService.GetHotProessReportById(rowID);
                        if (hotProessReport != null)
                        {
                            string cont = judgementDelete(rowID);
                            if (string.IsNullOrEmpty(cont))
                            {
                                BLL.AttachFileService.DeleteAttachFile(Funs.RootPath, rowID, Const.HJGL_HotProessTrustMenuId);//删除附件
                                BLL.HotProessReportService.DeleteHotProessReportById(rowID);
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
            //if (Funs.DB.Project_ProjectUser.FirstOrDefault(x => x.UserId == id) != null)
            //{
            //    content += "已在【项目用户】中使用，不能删除！";
            //}            

            return content;
        }
        #endregion
        #endregion

        #region 行点击事件
        /// <summary>
        /// Grid行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string hotProessReportId = Grid1.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "attchUrl")
            {
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/HJGL/HotProcessHard&menuId={1}&edit=0", hotProessReportId, BLL.Const.HJGL_HotProessTrustMenuId)));
            }
        }
        #endregion
    }
}