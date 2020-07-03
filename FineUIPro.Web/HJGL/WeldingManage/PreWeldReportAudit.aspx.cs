using System;
using System.Collections.Generic;
using System.Linq;
using BLL;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.HJGL.WeldingManage
{
    public partial class PreWeldReportAudit :PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitTreeMenu();
            }
        }

        #region 加载树装置-单位
        /// <summary>
        /// 加载树
        /// </summary>
        private void InitTreeMenu()
        {
            this.tvControlItem.Nodes.Clear();
            var weldReports = (from x in Funs.DB.HJGL_PreWeldingDaily
                               where x.ProjectId == this.CurrUser.LoginProjectId
                               select x).ToList();
            this.BindNodes(null,null, weldReports);
        }

        /// <summary>
        ///  绑定树节点
        /// </summary>
        /// <param name="node"></param>
        private void BindNodes(TreeNode node1, TreeNode node2, List<Model.HJGL_PreWeldingDaily> preWeldingDaily)
        {
            if (node1 == null && node2==null)
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

                this.BindNodes(rootNode1, rootNode2, preWeldingDaily);
            }
            else
            {
                var pWorkArea = (from x in Funs.DB.WBS_UnitWork where x.ProjectId == this.CurrUser.LoginProjectId select x).ToList();
                var pUnits = (from x in Funs.DB.Project_ProjectUnit where x.ProjectId == this.CurrUser.LoginProjectId select x).ToList();

                var pUnitDepth = Funs.DB.Project_ProjectUnit.FirstOrDefault(x => x.UnitId == this.CurrUser.UnitId && x.ProjectId == this.CurrUser.LoginProjectId);
                if (node1.CommandName == "建筑工程")
                {
                    List<Model.WBS_UnitWork> workAreas = null;
                    if (pUnitDepth == null || pUnitDepth.UnitType.Contains(Const.ProjectUnitType_1) || pUnitDepth.UnitType.Contains(Const.ProjectUnitType_5))
                    {
                        workAreas = (from x in pWorkArea
                                     join y in pUnits on x.UnitId equals y.UnitId
                                     where x.ProjectType == node1.NodeID && y.UnitType.Contains(Const.ProjectUnitType_2)
                                     select x).ToList();
                    }
                    else
                    {
                        workAreas = (from x in pWorkArea
                                     join y in pUnits on x.UnitId equals y.UnitId
                                     where x.ProjectType == node1.NodeID && y.UnitType.Contains(Const.ProjectUnitType_2)
                                     && x.UnitId == this.CurrUser.UnitId
                                     select x).ToList();
                    }

                    workAreas = workAreas.OrderByDescending(x => x.UnitWorkCode).ToList();
                    foreach (var q in workAreas)
                    {
                        var u = BLL.UnitService.GetUnitByUnitId(q.UnitId);
                        TreeNode newNode = new TreeNode();
                        newNode.Text = q.UnitWorkName;
                        newNode.NodeID = q.UnitWorkId;
                        newNode.ToolTip = "施工单位：" + u.UnitName;
                        newNode.EnableClickEvent = true;
                        node1.Nodes.Add(newNode);
                    }
                }
                if (node2.CommandName == "安装工程")
                {
                    List<Model.WBS_UnitWork> workAreas = null;
                    if (pUnitDepth == null || pUnitDepth.UnitType.Contains(Const.ProjectUnitType_1) || pUnitDepth.UnitType.Contains(Const.ProjectUnitType_5))
                    {
                        workAreas = (from x in pWorkArea
                                     join y in pUnits on x.UnitId equals y.UnitId
                                     where x.ProjectType == node2.NodeID && y.UnitType.Contains(Const.ProjectUnitType_2)
                                     select x).ToList();
                    }
                    else
                    {
                        workAreas = (from x in pWorkArea
                                     join y in pUnits on x.UnitId equals y.UnitId
                                     where x.ProjectType == node2.NodeID && y.UnitType.Contains(Const.ProjectUnitType_2)
                                     && x.UnitId == this.CurrUser.UnitId
                                     select x).ToList();
                    }

                    workAreas = workAreas.OrderByDescending(x => x.UnitWorkCode).ToList();
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
            string strSql = @"SELECT preWeld.PreWeldingDailyId, 
                                     preWeld.ProjectId, 
                                     preWeld.WeldJointId, 
                                     preWeld.WeldingDate, 
									 jot.WeldJointCode,
									 jot.PipelineCode,
                                     jot.JointArea,
                                     preWeld.JointAttribute, 
                                     jot.Size, 
                                     jot.Dia, 
                                     jot.Thickness,                            
                                     preWeld.AttachUrl, 
                                     cellWelder.WelderCode AS CellWelderCode,
                                     backingWelder.WelderCode AS BackingWelderCode,
                                     method.WeldingMethodCode AS WeldMethod,
                                     preWeld.AuditDate,
                                     users.UserName AS AuditManName 
                                  FROM dbo.HJGL_PreWeldingDaily AS preWeld
                                  LEFT JOIN dbo.HJGL_WeldJoint AS jot ON jot.WeldJointId = preWeld.WeldJointId
                                  LEFT JOIN dbo.SitePerson_Person AS cellWelder ON cellWelder.PersonId=preWeld.CoverWelderId
                                  LEFT JOIN dbo.SitePerson_Person AS backingWelder ON backingWelder.PersonId=preWeld.BackingWelderId
                                  LEFT JOIN dbo.Base_WeldingMethod method ON method.WeldingMethodId=jot.WeldingMethodId
                                  LEFT JOIN dbo.Sys_User AS users ON users.UserId = preWeld.AuditMan
                                  WHERE preWeld.ProjectId=@ProjectId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            if (!string.IsNullOrEmpty(tvControlItem.SelectedNodeID))
            {
                strSql += " AND preWeld.UnitWorkId =@UnitWorkId";
                listStr.Add(new SqlParameter("@UnitWorkId", tvControlItem.SelectedNode.NodeID));
            }
            if (IsAudit.SelectedValue == "0")
            {
                strSql += " AND preWeld.AuditDate IS NOT NULL ";
            }
            else
            {
                strSql += " AND preWeld.AuditDate IS NULL ";
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            // 2.获取当前分页数据
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
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
            Grid1.PageIndex = e.NewPageIndex;
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
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
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

        protected void btnAudit_Click(object sender, EventArgs e)
        {

        }
    }
}