using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;
using System.Text;
using System.Web;

namespace FineUIPro.Web.HJGL.WeldingManage
{
    public partial class WeldJointList : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ddlPageSize.SelectedValue = this.Grid1.PageSize.ToString();
                this.InitTreeMenu();//加载树
                //显示列
                //Model.Sys_UserShowColumns c = BLL.UserShowColumnsService.GetColumnsByUserId(this.CurrUser.UserId, "Joint");
                //if (c != null)
                //{
                //    this.GetShowColumn(c.Columns);
                //}
            }
        }

        protected void drpProjectId_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.InitTreeMenu();
        }

        #region 加载树装置-单位-工作区
        /// <summary>
        /// 加载树
        /// </summary>
        private void InitTreeMenu()
        {
            this.tvControlItem.Nodes.Clear();

            TreeNode rootNode1 = new TreeNode();
            rootNode1.NodeID = "1";
            rootNode1.Text = "建筑工程";
            rootNode1.CommandName = "建筑工程";
            this.tvControlItem.Nodes.Add(rootNode1);

            TreeNode rootNode2 = new TreeNode();
            rootNode2.NodeID = "2";
            rootNode2.Text = "安装工程";
            rootNode2.CommandName = "安装工程";
            rootNode2.Expanded = true;
            this.tvControlItem.Nodes.Add(rootNode2);

            var pUnits = (from x in Funs.DB.Project_ProjectUnit where x.ProjectId == this.CurrUser.LoginProjectId select x).ToList();
            // 获取当前用户所在单位
            var currUnit = pUnits.FirstOrDefault(x => x.UnitId == this.CurrUser.UnitId);

            var unitWorkList = (from x in Funs.DB.WBS_UnitWork
                                where x.ProjectId == this.CurrUser.LoginProjectId
                                      && x.SuperUnitWork == null && x.UnitId != null && x.ProjectType != null
                                select x).ToList();

            List<Model.WBS_UnitWork> unitWork1 = null;
            List<Model.WBS_UnitWork> unitWork2 = null;

            // 当前为施工单位，只能操作本单位的数据
            if (currUnit != null && currUnit.UnitType == Const.ProjectUnitType_2)
            {
                unitWork1 = (from x in unitWorkList
                             where x.UnitId == this.CurrUser.UnitId && x.ProjectType == "1"
                             select x).ToList();
                unitWork2 = (from x in unitWorkList
                             where x.UnitId == this.CurrUser.UnitId && x.ProjectType == "2"
                             select x).ToList();
            }
            else
            {
                unitWork1 = (from x in unitWorkList where x.ProjectType == "1" select x).ToList();
                unitWork2 = (from x in unitWorkList where x.ProjectType == "2" select x).ToList();
            }

            if (unitWork1.Count() > 0)
            {
                foreach (var q in unitWork1)
                {
                    int a = (from x in Funs.DB.HJGL_Pipeline where x.ProjectId == this.CurrUser.LoginProjectId && x.UnitWorkId == q.UnitWorkId select x).Count();
                    var u = BLL.UnitService.GetUnitByUnitId(q.UnitId);
                    TreeNode tn1 = new TreeNode();
                    tn1.NodeID = q.UnitWorkId;
                    tn1.Text = q.UnitWorkName + "【" + a.ToString() + "】" + "管线";
                    tn1.ToolTip = "施工单位：" + u.UnitName;
                    rootNode1.Nodes.Add(tn1);
                    if (a > 0) {
                        BindNodes(tn1);
                    }
                }
            }
            if (unitWork2.Count() > 0)
            {
                foreach (var q in unitWork2)
                {
                    int a = (from x in Funs.DB.HJGL_Pipeline where x.ProjectId == this.CurrUser.LoginProjectId && x.UnitWorkId == q.UnitWorkId select x).Count();
                    var u = BLL.UnitService.GetUnitByUnitId(q.UnitId);
                    TreeNode tn2 = new TreeNode();
                    tn2.NodeID = q.UnitWorkId;
                    tn2.Text = q.UnitWorkName + "【" + a.ToString() + "】" + "管线";
                    tn2.ToolTip = "施工单位：" + u.UnitName;
                    rootNode2.Nodes.Add(tn2);
                    if (a > 0)
                    {
                        BindNodes(tn2);
                    }
                }
            }
        }
        #endregion
        private void BindNodes(TreeNode node)
        {
            List<Model.HJGL_Pipeline> pipeline = new List<Model.HJGL_Pipeline>();
            var pipelines = from x in Funs.DB.HJGL_Pipeline select x;
            pipeline = (from x in pipelines
                        where x.ProjectId == this.CurrUser.LoginProjectId && x.UnitWorkId == node.NodeID
                       && x.PipelineCode.Contains(this.txtPipelineCode.Text.Trim())
                        orderby x.PipelineCode
                        select x).ToList();
            foreach (var item in pipeline)
            {
                var jotCount = (from x in Funs.DB.HJGL_WeldJoint where x.PipelineId == item.PipelineId  select x).Count();
                TreeNode newNode = new TreeNode();
                newNode.Text = item.PipelineCode;
                newNode.Text += "【" + jotCount.ToString() + " " + "焊口" + "】";
                newNode.ToolTip = "管线号【焊口数】";
                newNode.NodeID = item.PipelineId;
                newNode.EnableClickEvent = true;
                node.Nodes.Add(newNode);
            }
        }

        #region 点击TreeView
        /// <summary>
        /// 点击TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvControlItem_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT WeldJointId,WeldJointCode,PipelineId,PipelineCode,JointAttribute,
                                     ComponentsCode1,ComponentsCode2,IsWelding,IsHotProessStr,Material1Code,Material2Code,
                                     WeldTypeCode,Specification,HeartNo1,HeartNo2,Size,Dia,Thickness,GrooveTypeCode,
                                     WeldingMethodCode,WeldingWireCode,WeldingRodCode,WeldingDate,WeldingDailyCode,
                                     BackingWelderCode,CoverWelderCode,MediumCode ,PreTemperature,JointArea,WPQCode,Remark
                              FROM View_HJGL_WeldJoint WHERE 1= 1";
            List<SqlParameter> listStr = new List<SqlParameter> { };

            strSql += " AND PipelineId =@PipelineId";
            listStr.Add(new SqlParameter("@PipelineId", this.tvControlItem.SelectedNodeID));

            if (!string.IsNullOrEmpty(this.txtWeldJointCode.Text.Trim()))
            {
                strSql += " AND WeldJointCode LIKE @WeldJointCode";
                listStr.Add(new SqlParameter("@WeldJointCode", "%" + this.txtWeldJointCode.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            // 2.获取当前分页数据
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region 分页排序
        #region 页索引改变事件
        /// <summary>
        /// 页索引改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 排序
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 分页选择下拉改变事件
        /// <summary>
        /// 分页选择下拉改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }
        #endregion
        #endregion

        #region 焊口信息 维护事件
        /// <summary>
        /// Grid双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HJGL_WeldJointMenuId, BLL.Const.BtnModify))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("WeldJointEdit.aspx?WeldJointId={0}", Grid1.SelectedRowID, "编辑 - ")));
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 增加焊口信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_WeldJointMenuId, Const.BtnAdd))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("WeldJointEdit.aspx?PipelineId={0}", this.tvControlItem.SelectedNodeID, "新增 - ")));
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 批量增加焊口信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBatchAdd_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_WeldJointMenuId, Const.BtnAdd))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("WeldJointBatchEdit.aspx?PipelineId={0}", this.tvControlItem.SelectedNodeID, "新增 - ")));
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 焊口信息编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HJGL_WeldJointMenuId, BLL.Const.BtnModify))
            {
                if (Grid1.SelectedRowIndexArray.Length == 0)
                {
                    Alert.ShowInTop("请至少选择一条记录", MessageBoxIcon.Warning);
                    return;
                }
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("WeldJointEdit.aspx?WeldJointId={0}", Grid1.SelectedRowID, "维护 - ")));
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_WeldJointMenuId, Const.BtnDelete))
            {
                if (Grid1.SelectedRowIndexArray.Length == 0)
                {
                    Alert.ShowInTop("请至少选择一条记录", MessageBoxIcon.Warning);
                    return;
                }

                bool isShow = true;
                if (Grid1.SelectedRowIndexArray.Length > 1)
                {
                    isShow = false;
                }
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    if (judgementDelete(rowID, isShow))
                    {
                        BLL.WeldJointService.DeleteWeldJointById(rowID);
                        //BLL.Sys_LogService.AddLog(BLL.Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_WeldJointMenuId, Const.BtnDelete, rowID);
                    }
                }

                ShowNotify("删除成功！", MessageBoxIcon.Success);
                this.BindGrid();
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region  报表打印
        /// <summary>
        ///  报表打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string pipelineId = this.tvControlItem.SelectedNodeID;
            var q = BLL.PipelineService.GetPipelineByPipelineId(pipelineId);

            if (q != null)
            {
                string varValue = string.Empty;
                var project = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId);
                if (project != null)
                {
                    varValue = project.ProjectName;
                    var unitWork = BLL.UnitWorkService.GetUnitWorkByUnitWorkId(q.UnitWorkId);
                    if (unitWork != null)
                    {
                        varValue = varValue + "|" + unitWork.UnitWorkName;
                    }
                }
                List<SqlParameter> listStr = new List<SqlParameter>();
                listStr.Add(new SqlParameter("@IsoId", pipelineId));
                listStr.Add(new SqlParameter("@Flag", "0"));
                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = BLL.SQLHelper.GetDataTableRunProc("HJGL_spJointWorkRecordNew", parameter);
                string page = Funs.GetPagesCountByPageSize(11, 16, tb.Rows.Count).ToString();


                varValue = varValue + "|" + page;

                if (!string.IsNullOrEmpty(varValue))
                {
                    varValue = HttpUtility.UrlEncodeUnicode(varValue);
                }
                if (tb.Rows.Count <= 11)
                {
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("../../ReportPrint/ExReportPrint.aspx?ispop=1&reportId={0}&replaceParameter={1}&varValue={2}&projectId={3}", BLL.Const.HJGL_JointInfoReport1Id, pipelineId, varValue, this.CurrUser.LoginProjectId)));
                }
                else
                {
                    PageContext.RegisterStartupScript(Window3.GetShowReference(String.Format("../../ReportPrint/ExReportPrint.aspx?ispop=1&reportId={0}&replaceParameter={1}&varValue={2}&projectId={3}", BLL.Const.HJGL_JointInfoReport2Id, pipelineId, varValue, this.CurrUser.LoginProjectId)));
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("../../ReportPrint/ExReportPrint.aspx?ispop=1&reportId={0}&replaceParameter={1}&varValue={2}&projectId={3}", BLL.Const.HJGL_JointInfoReport1Id, pipelineId, varValue, this.CurrUser.LoginProjectId)));
                }
            }

            else
            {
                ShowNotify("请选择管线！", MessageBoxIcon.Warning);
                return;
            }
        }
        #endregion

        #region 关闭弹出窗口及刷新页面
        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            this.InitTreeMenu();//加载树
            this.BindGrid();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Tree_TextChanged(object sender, EventArgs e)
        {
            this.InitTreeMenu();
            this.BindGrid();
        }
        #endregion

        #region 判断是否可删除
        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private bool judgementDelete(string id, bool isShow)
        {
            string content = string.Empty;
            if (!string.IsNullOrEmpty(BLL.WeldJointService.GetWeldJointByWeldJointId(id).WeldingDailyId))
            {
                content = "该焊口已焊接，不能删除！";
            }
            if (BLL.Funs.DB.HJGL_HotProess_TrustItem.FirstOrDefault(x => x.WeldJointId == id) != null)
            {
                content = "热处理已经使用了该焊口，不能删除！";
            }

            if (string.IsNullOrEmpty(content))
            {
                return true;
            }
            else
            {
                if (isShow)
                {
                    Alert.ShowInTop(content, MessageBoxIcon.Error);
                }
                return false;
            }
        }
        #endregion

        #region 导出焊口信息
        /// <summary>
        /// 导出焊口信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            //var iso = BLL.Pipeline_PipelineService.GetPipelineByPipelineId(this.tvControlItem.SelectedNodeID);
            //var workArea = BLL.Project_WorkAreaService.GetProject_WorkAreaByWorkAreaId(this.tvControlItem.SelectedNodeID);
            //if (iso != null)
            //{
            //    PageContext.RegisterStartupScript(Window3.GetShowReference(String.Format("JointInfoOut.aspx?PipelineId={0}", this.tvControlItem.SelectedNodeID, "导出 - ")));
            //}
            //else if (workArea != null)
            //{
            //    PageContext.RegisterStartupScript(Window3.GetShowReference(String.Format("JointInfoOut.aspx?WorkAreaId={0}", this.tvControlItem.SelectedNodeID, "导出 - ")));
            //}
            //else
            //{
            //    Alert.ShowInTop("请选择"PipelineOrArea, MessageBoxIcon.Warning);
            //}
        }

        /// <summary>
        /// 导出焊口初始信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut2_Click(object sender, EventArgs e)
        {
            //var iso = BLL.Pipeline_PipelineService.GetPipelineByPipelineId(this.tvControlItem.SelectedNodeID);
            //if (iso != null)
            //{
            //    Response.ClearContent();
            //    string filename = Funs.GetNewFileName();
            //    Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(Resources.Lan.WeldingJointInfo + filename, System.Text.Encoding.UTF8) + ".xls");
            //    Response.ContentType = "application/excel";
            //    Response.ContentEncoding = System.Text.Encoding.UTF8;
            //    this.Grid1.PageSize = 100000;
            //    this.BindGrid();
            //    Response.Write(GetGridTableHtml(Grid1));
            //    Response.End();
            //}
            //else
            //{
            //    Alert.ShowInTop("请选择"PipelinetFirst, MessageBoxIcon.Warning);
            //}
        }

        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<meta http-equiv=\"content-type\" content=\"application/excel; charset=UTF-8\"/>");
            sb.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");
            sb.Append("<tr>");
            foreach (GridColumn column in grid.Columns)
            {
                if (column.HeaderText != "序号")
                {
                    sb.AppendFormat("<td>{0}</td>", column.HeaderText);
                }
            }
            sb.Append("</tr>");
            foreach (GridRow row in grid.Rows)
            {
                sb.Append("<tr>");
                foreach (GridColumn column in grid.Columns)
                {
                    string html = row.Values[column.ColumnIndex].ToString();
                    if (column.ColumnID != "tfNumber")
                    {
                        //html = (row.FindControl("lblNumber") as AspNet.Label).Text;
                        sb.AppendFormat("<td>{0}</td>", html);
                    }
                    //sb.AppendFormat("<td>{0}</td>", html);
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }
        #endregion

        #region 选择要显示列
        /// <summary>
        /// 选择显示列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelectColumn_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window4.GetShowReference(String.Format("JointShowColumn.aspx", "显示列 - ")));
        }
        #endregion

        #region 显示列
        protected void Window4_Close(object sender, WindowCloseEventArgs e)
        {
            //this.BindGrid();
            ////显示列
            //Model.Sys_UserShowColumns c = BLL.UserShowColumnsService.GetColumnsByUserId(this.CurrUser.UserId, "Joint");
            //if (c != null)
            //{
            //    this.GetShowColumn(c.Columns);
            //}
        }

        /// <summary>
        /// 显示的列
        /// </summary>
        /// <param name="column"></param>
        private void GetShowColumn(string column)
        {
            if (!string.IsNullOrEmpty(column))
            {
                this.Grid1.Columns[1].Hidden = true;
                this.Grid1.Columns[2].Hidden = true;
                this.Grid1.Columns[3].Hidden = true;
                this.Grid1.Columns[4].Hidden = true;
                this.Grid1.Columns[5].Hidden = true;
                this.Grid1.Columns[6].Hidden = true;
                this.Grid1.Columns[7].Hidden = true;
                this.Grid1.Columns[8].Hidden = true;
                this.Grid1.Columns[9].Hidden = true;
                this.Grid1.Columns[10].Hidden = true;
                this.Grid1.Columns[11].Hidden = true;
                this.Grid1.Columns[12].Hidden = true;
                this.Grid1.Columns[13].Hidden = true;
                this.Grid1.Columns[14].Hidden = true;
                this.Grid1.Columns[15].Hidden = true;
                this.Grid1.Columns[16].Hidden = true;
                this.Grid1.Columns[17].Hidden = true;
                this.Grid1.Columns[18].Hidden = true;
                this.Grid1.Columns[19].Hidden = true;
                this.Grid1.Columns[20].Hidden = true;
                this.Grid1.Columns[21].Hidden = true;
                this.Grid1.Columns[22].Hidden = true;
                this.Grid1.Columns[23].Hidden = true;
                this.Grid1.Columns[24].Hidden = true;
                this.Grid1.Columns[25].Hidden = true;
                this.Grid1.Columns[26].Hidden = true;
                this.Grid1.Columns[27].Hidden = true;
                this.Grid1.Columns[28].Hidden = true;
                this.Grid1.Columns[29].Hidden = true;
                List<string> columns = column.Split(',').ToList();
                foreach (var item in columns)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        this.Grid1.Columns[Convert.ToInt32(item)].Hidden = false;
                    }
                }
            }
        }
        #endregion
    }
}