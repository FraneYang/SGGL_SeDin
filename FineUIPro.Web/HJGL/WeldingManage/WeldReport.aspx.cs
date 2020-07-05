﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;

namespace FineUIPro.Web.HJGL.WeldingManage
{
    public partial class WeldReport : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtMonth.Text = string.Format("{0:yyyy-MM}", DateTime.Now);
                this.ddlPageSize.SelectedValue = this.Grid1.PageSize.ToString();
                this.InitTreeMenu();//加载树
            }
        }

        #region 加载树装置-单位
        /// <summary>
        /// 加载树
        /// </summary>
        private void InitTreeMenu()
        {
            if (!string.IsNullOrEmpty(this.txtMonth.Text.Trim()))
            {
                DateTime startTime = Convert.ToDateTime(this.txtMonth.Text.Trim() + "-01");
                DateTime endTime = startTime.AddMonths(1);
                this.tvControlItem.Nodes.Clear();
                var weldReports = (from x in Funs.DB.HJGL_WeldingDaily
                                   where x.ProjectId == this.CurrUser.LoginProjectId
                                   select x).ToList();
                this.BindNodes(null, null, weldReports, startTime, endTime);
            }
            else
            {
                Alert.ShowInTop("请选择月份！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 绑定树节点

        #region 绑定树节点
        /// <summary>
        ///  绑定树节点
        /// </summary>
        /// <param name="node"></param>
        private void BindNodes(TreeNode node1, TreeNode node2, List<Model.HJGL_WeldingDaily> projectWeldReports, DateTime startTime, DateTime endTime)
        {
            var WeldReports = (from x in projectWeldReports
                               where x.WeldingDate >= startTime && x.WeldingDate < endTime
                               select x).ToList();
            if (node1 == null && node2 == null)
            {
                
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

                    this.BindNodes(rootNode1, rootNode2, projectWeldReports, startTime, endTime);
            }
            else
            {
                var pUnitWork = (from x in Funs.DB.WBS_UnitWork where x.ProjectId == this.CurrUser.LoginProjectId select x).ToList();
                var pUnits = (from x in Funs.DB.Project_ProjectUnit where x.ProjectId == this.CurrUser.LoginProjectId select x).ToList();
                var pUnitDepth = Funs.DB.Project_ProjectUnit.FirstOrDefault(x => x.UnitId == this.CurrUser.UnitId && x.ProjectId == this.CurrUser.LoginProjectId);
                
                if (node1.CommandName == "建筑工程")
                {
                    List<Model.WBS_UnitWork> workAreas = null;
                    if (pUnitDepth == null || pUnitDepth.UnitType.Contains(Const.ProjectUnitType_1) || pUnitDepth.UnitType.Contains(Const.ProjectUnitType_5))
                    {
                        workAreas = (from x in pUnitWork
                                     join y in pUnits on x.UnitId equals y.UnitId
                                     where x.ProjectType == node1.NodeID && y.UnitType.Contains(Const.ProjectUnitType_2)
                                     select x).ToList();
                    }
                    else
                    {
                        workAreas = (from x in pUnitWork
                                     join y in pUnits on x.UnitId equals y.UnitId
                                     where x.ProjectType == node1.NodeID && y.UnitType.Contains(Const.ProjectUnitType_2)
                                     && x.UnitId == this.CurrUser.UnitId
                                     select x).ToList();
                    }

                    workAreas = workAreas.OrderByDescending(x => x.UnitWorkCode).ToList();
                    var pipelines = from x in Funs.DB.HJGL_Pipeline select x;
                    foreach (var q in workAreas)
                    {
                        var u = BLL.UnitService.GetUnitByUnitId(q.UnitId);
                        TreeNode newNode = new TreeNode();
                        newNode.Text = q.UnitWorkName;
                        newNode.NodeID = q.UnitWorkId;
                        newNode.EnableClickEvent = true;
                        newNode.ToolTip = "施工单位：" + u.UnitName;
                        node1.Nodes.Add(newNode);
                    }
                }
                if (node2.CommandName == "安装工程")
                {
                    List<Model.WBS_UnitWork> workAreas = null;
                    if (pUnitDepth == null || pUnitDepth.UnitType.Contains(Const.ProjectUnitType_1) || pUnitDepth.UnitType.Contains(Const.ProjectUnitType_5))
                    {
                        workAreas = (from x in pUnitWork
                                     join y in pUnits on x.UnitId equals y.UnitId
                                     where x.ProjectType == node2.NodeID && y.UnitType.Contains(Const.ProjectUnitType_2)
                                     select x).ToList();
                    }
                    else
                    {
                        workAreas = (from x in pUnitWork
                                     join y in pUnits on x.UnitId equals y.UnitId
                                     where x.ProjectType == node2.NodeID && y.UnitType.Contains(Const.ProjectUnitType_2)
                                     && x.UnitId == this.CurrUser.UnitId
                                     select x).ToList();
                    }
                    workAreas = workAreas.OrderByDescending(x => x.UnitWorkCode).ToList();
                    var pipelines = from x in Funs.DB.HJGL_Pipeline select x;
                    foreach (var q in workAreas)
                    {
                        var u = BLL.UnitService.GetUnitByUnitId(q.UnitId);
                        TreeNode newNode = new TreeNode();
                        newNode.Text = q.UnitWorkName;
                        newNode.NodeID = q.UnitWorkId;
                        newNode.EnableClickEvent = true;
                        newNode.ToolTip = "施工单位：" + u.UnitName;
                        node2.Nodes.Add(newNode);
                    }
                }
            }
        }
        #endregion
        #endregion

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

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            if (this.tvControlItem.SelectedNode != null)
            {
                DateTime startDate = Convert.ToDateTime(this.txtMonth.Text.Trim() + "-1");
                DateTime endDate = startDate.AddMonths(1);
                string startDateStr = string.Format("{0:yyyy-MM-dd}", startDate);
                string endDateStr = string.Format("{0:yyyy-MM-dd}", endDate);
                string strSql = @"SELECT distinct d.WeldingDailyId,d.WeldingDailyCode,d.ProjectId,d.UnitId,unit.UnitName,
                                         d.WeldingDate,d.Tabler,u.UserName,d.TableDate,d.Remark
                                   FROM dbo.HJGL_WeldingDaily d 
                                   left join dbo.HJGL_WeldJoint w on w.WeldingDailyId=d.WeldingDailyId
								   LEFT JOIN dbo.Base_Unit unit ON unit.UnitId = d.UnitId
								   LEFT JOIN dbo.Sys_User u ON u.UserId = d.Tabler
                                   WHERE 1=1";
                List<SqlParameter> listStr = new List<SqlParameter>
                {

                };
                strSql += " AND d.ProjectId =@ProjectId";
                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
                strSql += " AND d.UnitWorkId =@UnitWorkId";
                listStr.Add(new SqlParameter("@UnitWorkId", tvControlItem.SelectedNode.NodeID));
                strSql += " AND d.WeldingDate >=@startDateStr";
                listStr.Add(new SqlParameter("@startDateStr", startDateStr));
                strSql += " AND d.WeldingDate <@endDateStr";
                listStr.Add(new SqlParameter("@endDateStr", endDateStr));
                if (!string.IsNullOrEmpty(this.txtWeldingDate.Text.Trim()))
                {
                    strSql += " AND d.WeldingDate = @WeldingDate";
                    listStr.Add(new SqlParameter("@WeldingDate", this.txtWeldingDate.Text.Trim()));
                }
                if (!string.IsNullOrEmpty(this.txtWeldingDailyCode.Text.Trim()))
                {
                    strSql += " AND d.WeldingDailyCode LIKE @WeldingDailyCode";
                    listStr.Add(new SqlParameter("@WeldingDailyCode", "%" + this.txtWeldingDailyCode.Text.Trim() + "%"));
                }
                if (!string.IsNullOrEmpty(this.txtPipelineCode.Text.Trim()))
                {
                    strSql += " AND w.PipelineCode LIKE @PipelineCode";
                    listStr.Add(new SqlParameter("@PipelineCode", "%" + this.txtPipelineCode.Text.Trim() + "%"));
                }
                if (!string.IsNullOrEmpty(this.txtWeldJointCode.Text.Trim()))
                {
                    strSql += " AND w.WeldJointCode LIKE @WeldJointCode";
                    listStr.Add(new SqlParameter("@WeldJointCode", "%" + this.txtWeldJointCode.Text.Trim() + "%"));
                }
                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

                // 2.获取当前分页数据
                //var table = this.GetPagedDataTable(Grid1, tb1);
                Grid1.RecordCount = tb.Rows.Count;
                tb = GetFilteredTable(Grid1.FilteredData, tb);
                var table = this.GetPagedDataTable(Grid1, tb);
                Grid1.DataSource = table;
                Grid1.DataBind();
            }
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

        #region 焊接日报 维护事件
        /// <summary>
        /// Grid双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HJGL_WeldReportMenuId, BLL.Const.BtnModify))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("WeldReportEdit.aspx?WeldingDailyId={0}", Grid1.SelectedRowID, "编辑 - ")));
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 增加焊接日报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_WeldReportMenuId, Const.BtnAdd))
            {
                if (!string.IsNullOrEmpty(tvControlItem.SelectedNodeID))
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("WeldReportEdit.aspx?workAreaId={0}", tvControlItem.SelectedNodeID, "新增 - ")));
                }
                else
                {
                    ShowNotify("请选择区域", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 焊接日报编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HJGL_WeldReportMenuId, BLL.Const.BtnModify))
            {
                if (Grid1.SelectedRowIndexArray.Length == 0)
                {
                    Alert.ShowInTop("请至少选择一条记录", MessageBoxIcon.Warning);
                    return;
                }
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("WeldReportEdit.aspx?WeldingDailyId={0}", Grid1.SelectedRowID, "维护 - ")));
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
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_WeldReportMenuId, Const.BtnDelete))
            {
                if (Grid1.SelectedRowIndexArray.Length == 0)
                {
                    Alert.ShowInTop("请至少选择一条记录", MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    string weldingDailyId = Grid1.SelectedRowID;
                    var isTrust = from x in Funs.DB.HJGL_Batch_BatchTrustItem
                                  join y in Funs.DB.HJGL_WeldJoint on x.WeldJointId equals y.WeldJointId
                                  where y.WeldingDailyId == weldingDailyId
                                  select x; ;
                    if (isTrust.Count() == 0)
                    {
                        var weldJoints = BLL.WeldJointService.GetWeldlinesByWeldingDailyId(weldingDailyId);
                        if (weldJoints.Count() > 0)
                        {
                            foreach (var item in weldJoints)
                            {
                                var updateWeldJoint = BLL.WeldJointService.GetWeldJointByWeldJointId(item.WeldJointId);
                                if (updateWeldJoint != null)
                                {
                                    updateWeldJoint.WeldingDailyId = null;
                                    updateWeldJoint.WeldingDailyCode = null;
                                    updateWeldJoint.CoverWelderId = null;
                                    updateWeldJoint.BackingWelderId = null;
                                    BLL.WeldJointService.UpdateWeldJoint(updateWeldJoint);

                                    var pointBatchItems = from x in Funs.DB.HJGL_Batch_PointBatchItem where x.WeldJointId == item.WeldJointId select x;
                                    string pointBatchId = pointBatchItems.FirstOrDefault().PointBatchId;

                                    // 删除焊口所在批明细信息
                                    BLL.PointBatchDetailService.DeleteBatchDetail(item.WeldJointId);

                                    // 删除批信息
                                    var batch = from x in Funs.DB.HJGL_Batch_PointBatchItem where x.PointBatchId == pointBatchId select x;
                                    if (pointBatchId != null && batch.Count() == 0)
                                    {
                                        BLL.PointBatchService.DeleteBatch(pointBatchId);
                                    }
                                    //BLL.Batch_NDEItemService.DeleteAllNDEInfoToWeldJoint(item.WeldJointId);
                                }
                            }
                        }
                        BLL.WeldingDailyService.DeleteWeldingDaily(weldingDailyId);
                        //BLL.Sys_LogService.AddLog(BLL.Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_WeldReportMenuId, Const.BtnDelete, weldingDailyId);
                        ShowNotify("删除成功！", MessageBoxIcon.Success);
                        this.BindGrid();

                    }
                    else
                    {
                        Alert.ShowInTop("该日报下已有焊口进委托单了，不能删除！", MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
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

    }
}