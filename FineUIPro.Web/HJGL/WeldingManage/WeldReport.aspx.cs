using System;
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
                        var u = BLL.UnitService.GetUnitByUnitId(q.UnitId);
                        TreeNode tn1 = new TreeNode();
                        tn1.NodeID = q.UnitWorkId;
                        tn1.Text = q.UnitWorkName ;
                        tn1.ToolTip = "施工单位：" + u.UnitName;
                        rootNode1.Nodes.Add(tn1);
                        BindNodes(tn1);
                    }
                }
                if (unitWork2.Count() > 0)
                {
                    foreach (var q in unitWork2)
                    {
                        var u = BLL.UnitService.GetUnitByUnitId(q.UnitId);
                        TreeNode tn2 = new TreeNode();
                        tn2.NodeID = q.UnitWorkId;
                        tn2.Text = q.UnitWorkName;
                        tn2.ToolTip = "施工单位：" + u.UnitName;
                        rootNode2.Nodes.Add(tn2);
                        BindNodes(tn2);
                    }
                }
            }
            else
            {
                Alert.ShowInTop("请选择月份！", MessageBoxIcon.Warning);
            }
        }

        private void BindNodes(TreeNode node)
        {
            var p = from x in Funs.DB.HJGL_WeldingDaily
                    where x.UnitWorkId == node.NodeID
                         && x.WeldingDate < Convert.ToDateTime(this.txtMonth.Text.Trim() + "-01").AddMonths(1)
                         && x.WeldingDate >= Convert.ToDateTime(this.txtMonth.Text.Trim() + "-01")
                    orderby x.WeldingDailyCode descending
                    select x;
                     
            if (p.Count() > 0)
            {
                foreach (var item in p)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = item.WeldingDailyCode;
                    newNode.NodeID = item.WeldingDailyId;
                    newNode.EnableClickEvent = true;
                    node.Nodes.Add(newNode);
                }
            }

        }
        #endregion


        #region 点击TreeView
        /// <summary>
        /// 点击TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvControlItem_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(tvControlItem.SelectedNodeID))
            {
                this.BindGrid();
                var daily = BLL.WeldingDailyService.GetPipeline_WeldingDailyByWeldingDailyId(tvControlItem.SelectedNodeID);
                txtUnitName.Text = BLL.UnitService.GetUnitNameByUnitId(daily.UnitId);

                txtTabler.Text = BLL.UserService.GetUserNameByUserId(daily.Tabler);
                txtTableDate.Text = string.Format("{0:yyyy-MM-dd}", daily.TableDate);
                txtWeldingDate.Text= string.Format("{0:yyyy-MM-dd}", daily.WeldingDate);
            }
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

                string strSql = @"SELECT WeldingDailyId,WeldJointId,PipelineCode,WeldJointCode,
                                         BackingWelderCode,CoverWelderCode,Material1Code,Material2Code,
                                         Dia,Thickness,WeldTypeCode,WeldingMethodCode,WeldingWireCode,
	                                     WeldingRodCode
                                  FROM dbo.View_HJGL_WeldJoint
                                   WHERE WeldingDailyId=@WeldingDailyId";
                List<SqlParameter> listStr = new List<SqlParameter>();
                listStr.Add(new SqlParameter("@WeldingDailyId", tvControlItem.SelectedNodeID));
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
            else
            {
                Grid1.DataSource = null;
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
        //protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        //{
        //    if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HJGL_WeldReportMenuId, BLL.Const.BtnModify))
        //    {
        //        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("WeldReportEdit.aspx?WeldingDailyId={0}", Grid1.SelectedRowID, "编辑 - ")));
        //    }
        //    else
        //    {
        //        ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
        //    }
        //}

        /// <summary>
        /// 增加焊接日报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuAdd_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_WeldReportMenuId, Const.BtnAdd))
            {
                var unitWork = BLL.UnitWorkService.getUnitWorkByUnitWorkId(tvControlItem.SelectedNodeID);
                if (!string.IsNullOrEmpty(tvControlItem.SelectedNodeID) && unitWork != null)
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("WeldReportEdit.aspx?unitWorkId={0}", tvControlItem.SelectedNodeID, "新增 - ")));
                }
                else
                {
                    ShowNotify("请选择单位工程", MessageBoxIcon.Warning);
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
                var daily = BLL.WeldingDailyService.GetPipeline_WeldingDailyByWeldingDailyId(tvControlItem.SelectedNodeID);
                if (!string.IsNullOrEmpty(tvControlItem.SelectedNodeID) && daily != null)
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("WeldReportEdit.aspx?WeldingDailyId={0}", tvControlItem.SelectedNodeID, "维护 - ")));
                }
                else
                {
                    ShowNotify("请选择要编辑的日报！", MessageBoxIcon.Warning);
                }
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
                var daily = BLL.WeldingDailyService.GetPipeline_WeldingDailyByWeldingDailyId(tvControlItem.SelectedNodeID);
                if (daily == null)
                {
                    Alert.ShowInTop("请选择要删除的日报", MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    string weldingDailyId = tvControlItem.SelectedNodeID;
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
                        this.InitTreeMenu();
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