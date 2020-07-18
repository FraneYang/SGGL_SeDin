using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HJGL.PointTrust
{
    public partial class PointBatch : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 批次主键
        /// </summary>
        public string PointBatchId
        {
            get
            {
                return (string)ViewState["PointBatchId"];
            }
            set
            {
                ViewState["PointBatchId"] = value;
            }
        }
        #endregion

        #region 加载
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ddlPageSize.SelectedValue = this.Grid1.PageSize.ToString();
                this.txtStartTime.Text = string.Format("{0:yyyy-MM}", DateTime.Now);
                this.InitTreeMenu();//加载树
            }
        }

        #region 加载树
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
                    tn1.Text = q.UnitWorkName;
                    tn1.ToolTip = "施工单位：" + u.UnitName;
                    tn1.CommandName = "单位工程";
                    tn1.EnableExpandEvent = true;
                    rootNode1.Nodes.Add(tn1);
                    BindNodes(tn1);
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
                    tn2.Text = q.UnitWorkName;
                    tn2.ToolTip = "施工单位：" + u.UnitName;
                    tn2.EnableExpandEvent = true;
                    tn2.CommandName = "单位工程";
                    rootNode2.Nodes.Add(tn2);
                    BindNodes(tn2);
                }
            }
        }
        /// <summary>
        ///  绑定树节点
        /// </summary>
        /// <param name="node"></param>
        private void BindNodes(TreeNode node)
        {

            var p = from x in Funs.DB.HJGL_Batch_PointBatch
                    where x.UnitWorkId == node.NodeID
                    && x.StartDate < Convert.ToDateTime(this.txtStartTime.Text.Trim() + "-01").AddMonths(1)
                    && x.StartDate >= Convert.ToDateTime(this.txtStartTime.Text.Trim() + "-01")
                    select x;
            if (p.Count() > 0)
            {
                TreeNode newNode = new TreeNode();
                newNode.Text = "探伤类型";
                newNode.NodeID = "探伤类型";
                node.Nodes.Add(newNode);
            }

        }
        #endregion
        #endregion

        #region 绑定树节点

        protected void tvControlItem_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
        {
            e.Node.Nodes.Clear();
            if (e.Node.CommandName == "单位工程")
            {
                var detectionTypes = from x in Funs.DB.Base_DetectionType
                                     orderby x.DetectionTypeCode
                                     select new { x.DetectionTypeId, x.DetectionTypeCode, x.DetectionTypeName };
                foreach (var item in detectionTypes)
                {
                    var pointManages = from x in Funs.DB.View_HJGL_Batch_PointBatch
                                       where x.ProjectId == this.CurrUser.LoginProjectId
                                       && x.UnitWorkId == e.Node.NodeID
                                       && x.DetectionTypeId == item.DetectionTypeId
                                       select x;

                    TreeNode newNode = new TreeNode();
                    if (pointManages.Count() > 0)
                    {
                        newNode.Text = item.DetectionTypeCode;
                        newNode.NodeID = item.DetectionTypeId + "|" + e.Node.NodeID;
                        newNode.EnableExpandEvent = true;
                        newNode.ToolTip = item.DetectionTypeName;
                        newNode.CommandName = "探伤类型";
                        e.Node.Nodes.Add(newNode);
                    }

                    TreeNode tn1 = new TreeNode
                    {
                        Text = "检测比例",
                        NodeID = "检测比例",
                    };
                    newNode.Nodes.Add(tn1);
                }
            }

            if (e.Node.CommandName == "探伤类型")
            {
                var detectionRates = from x in Funs.DB.Base_DetectionRate
                                     orderby x.DetectionRateCode
                                     select new { x.DetectionRateId, x.DetectionRateCode, x.DetectionRateValue };
                foreach (var item in detectionRates)
                {
                    var pointManages = from x in Funs.DB.View_HJGL_Batch_PointBatch
                                       where x.ProjectId == this.CurrUser.LoginProjectId
                                       && x.UnitWorkId == e.Node.ParentNode.NodeID
                                       && x.DetectionTypeId == e.Node.NodeID.Split('|')[0]
                                       && x.DetectionRateId == item.DetectionRateId
                                       select x;

                    TreeNode newNode = new TreeNode();
                    if (pointManages.Count() > 0)
                    {
                        newNode.Text = item.DetectionRateValue.ToString() + "%";
                        newNode.NodeID = item.DetectionRateId + "|" + e.Node.NodeID;
                        newNode.EnableExpandEvent = true;
                        newNode.ToolTip = item.DetectionRateCode;
                        newNode.CommandName = "检测比例";

                        e.Node.Nodes.Add(newNode);
                    }

                    TreeNode tn1 = new TreeNode
                    {
                        Text = "检测批",
                        NodeID = "检测批",
                    };
                    newNode.Nodes.Add(tn1);
                }
            }

            if (e.Node.CommandName == "检测比例")
            {
                var pointManages = from x in Funs.DB.View_HJGL_Batch_PointBatch
                                   where x.ProjectId == this.CurrUser.LoginProjectId
                                   && x.DetectionRateId == e.NodeID.Split('|')[0]
                                   && x.DetectionTypeId == e.Node.ParentNode.NodeID.Split('|')[0]
                                   && x.UnitWorkId == e.Node.ParentNode.ParentNode.NodeID
                                   && x.StartDate < Convert.ToDateTime(this.txtStartTime.Text.Trim() + "-01").AddMonths(1)
                                   && x.StartDate >= Convert.ToDateTime(this.txtStartTime.Text.Trim() + "-01")
                                   select x;

                if (!string.IsNullOrEmpty(this.txtWelderCode.Text))
                {
                    pointManages = pointManages.Where(x => x.WelderCode.Contains(this.txtWelderCode.Text.Trim()));
                }



                foreach (var item in pointManages)
                {

                    TreeNode newNode = new TreeNode
                    {
                        NodeID = item.PointBatchId,
                        ToolTip = "批",
                        EnableClickEvent = true,
                    };

                    if (!item.EndDate.HasValue) ////批 没有关闭 粉色
                    {
                        newNode.Text = "<font color='#FA58D0'>" + item.PointBatchCode + "</font>";
                        newNode.ToolTip = "批尚未关闭";
                    }
                    // 当天批
                    else if (string.Format("{0:yyyy-MM-dd}", item.StartDate) == string.Format("{0:yyyy-MM-dd}", System.DateTime.Now)
                        || string.Format("{0:yyyy-MM-dd}", item.EndDate) == string.Format("{0:yyyy-MM-dd}", System.DateTime.Now))
                    {
                        newNode.Text = "<font color='#EE0000'>" + item.PointBatchCode + "</font>";
                        newNode.ToolTip = "当天批";
                    }
                    else
                    {
                        newNode.Text = item.PointBatchCode;
                    }
                    e.Node.Nodes.Add(newNode);

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
            if (this.tvControlItem.SelectedNodeID != "0")
            {
                this.PointBatchId = tvControlItem.SelectedNodeID;
                this.BindGrid();
            }
        }
        #endregion

        #region 数据绑定
        #region 加载页面输入提交信息
        /// <summary>
        /// 加载页面输入提交信息
        /// </summary>
        private void PageInfoLoad()
        {
            this.txtStartDate.Text = string.Empty;
            this.txtEndDate.Text = string.Empty;
            this.txtState.Text = "未关闭";

            Model.HJGL_Batch_PointBatch pointBatch = BLL.PointBatchService.GetPointBatchById(this.PointBatchId);
            if (pointBatch != null)
            {
                if (pointBatch.StartDate.HasValue)
                {
                    this.txtStartDate.Text = string.Format("{0:yyyy-MM-dd}", pointBatch.StartDate);
                }
                if (pointBatch.EndDate.HasValue)
                {
                    this.txtEndDate.Text = string.Format("{0:yyyy-MM-dd}", pointBatch.EndDate);
                    this.txtState.Text = "已关闭";
                }
            }
        }
        #endregion

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            this.PageInfoLoad();
            string strSql = @"SELECT PointBatchItemId,PointBatchId,WeldJointId,PointState,PointDate,RepairDate,CutDate,WeldJointCode,IsBuildTrust,
                                     JointAttribute ,JointArea,IsWelderFirst,Size,WeldingDate,PipelineCode,PipingClassName,PointIsAudit
                              FROM dbo.View_HJGL_Batch_PointBatchItem 
                              WHERE PointBatchId=@PointBatchId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(PointBatchId))
            {
                listStr.Add(new SqlParameter("@PointBatchId", this.PointBatchId));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            // tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();

            for (int i = 0; i < this.Grid1.Rows.Count; i++)
            {
                this.Grid1.Rows[i].CellCssClasses[3] = null;
                this.Grid1.Rows[i].CellCssClasses[6] = null;
                var pointItem = BLL.PointBatchDetailService.GetBatchDetailById(this.Grid1.Rows[i].DataKeys[0].ToString());
                if (pointItem != null)
                {
                    if (pointItem.PointState != null)
                    {
                        this.Grid1.Rows[i].CellCssClasses[3] = "colorredBlue";
                        this.Grid1.Rows[i].CellCssClasses[6] = "colorredBlue";
                    }
                }
            }
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

        #region 关闭弹出窗口及刷新页面
        /// <summary>
        /// 批关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            this.BindGrid();
        }

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Tree_TextChanged(object sender, EventArgs e)
        {
            this.InitTreeMenu();
        }
        #endregion
        #endregion

        #region 自动点口、打开重新点口、点口审核
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAutoPoint_Click(object sender, EventArgs e)
        {
            var batch = BLL.PointBatchService.GetPointBatchById(this.PointBatchId);
            if (!batch.EndDate.HasValue)
            {
                BLL.PointBatchDetailService.AutoPoint(this.PointBatchId);
                this.BindGrid();
                Alert.ShowInTop("已成功点口！", MessageBoxIcon.Success);
            }
            else
            {
                Alert.ShowInTop("批已关闭，不能自动点口！", MessageBoxIcon.Success);
            }


        }

        /// <summary>
        /// 手动点口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPointAudit_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_PointBatchMenuId, Const.BtnPointAudit))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PointAudit.aspx", "点口审核 - ")));
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 打开重新点口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnbtnOpenResetPoint_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_PointBatchMenuId, Const.BtnOpenResetPoint))
            {
                if (!string.IsNullOrEmpty(this.PointBatchId))
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("OpenResetPoint.aspx?PointBatchId={0}", this.PointBatchId, "重新点口 - ")));
                }
                else
                {
                    Alert.ShowInTop("请选择要重新点口的批！");
                }
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }


        /// <summary>
        /// 重新选择扩口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelectExpandPoint_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region 生成
        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            //if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_PointBatchMenuId, Const.BtnGenerate))
            //{

            //    var getViewGenerateTrustLists = (from x in Funs.DB.View_GenerateTrust where x.ProjectId == this.CurrUser.LoginProjectId select x).ToList();
            //    if (getViewGenerateTrustLists.Count() > 0)
            //    {
            //        var getUnit = BLL.Base_UnitService.GetUnit(this.CurrUser.UnitId);
            //        if (getUnit == null || getUnit.UnitTypeId == "1" || getUnit.UnitTypeId == "2" || getUnit.UnitTypeId == "3")
            //        {
            //            GenerateTrust(getViewGenerateTrustLists);
            //        }
            //        else
            //        {
            //            var getUnitViewGenerateTrustLists = getViewGenerateTrustLists.Where(x => x.UnitId == this.CurrUser.UnitId);
            //            if (getUnitViewGenerateTrustLists.Count() > 0)
            //            {
            //                // 当前单位未委托批
            //                GenerateTrust(getUnitViewGenerateTrustLists.ToList());
            //            }
            //            else
            //            {
            //                Alert.ShowInTop("所属单位点口已全部生成委托单！", MessageBoxIcon.Warning);
            //            }
            //        }
            //        BLL.Sys_LogService.AddLog(BLL.Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_PointBatchMenuId, Const.BtnGenerate, null);
            //        this.InitTreeMenu();//加载树
            //        this.BindGrid();
            //    }
            //    else
            //    {
            //        Alert.ShowInTop("已全部生成委托单！", MessageBoxIcon.Warning);
            //    }
            //}
            //else
            //{
            //    ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            //    return;
            //}
        }

        /// <summary>
        /// 生成委托单
        /// </summary>
        /// <param name="unitId"></param>
        //private void GenerateTrust(List<Model.View_GenerateTrust> GenerateTrustLists)
        //{

        //}
        #endregion


        #region 切除焊口

        #endregion

        #region 取消扩透
        /// <summary>
        ///  取消扩透
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region 打印
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.PointBatchId))
            {
                string parm = string.Empty;
                var pointBatch = Funs.DB.View_HJGL_Batch_PointBatch.FirstOrDefault(x => x.PointBatchId == this.PointBatchId);
                if (pointBatch != null)
                {
                    if (!string.IsNullOrEmpty(pointBatch.UnitCode))
                    {
                        parm = pointBatch.UnitCode;
                    }
                    else
                    {
                        parm = "NULL";
                    }

                    if (!string.IsNullOrEmpty(pointBatch.ProjectCode))
                    {
                        parm = parm + "|" + pointBatch.ProjectCode;
                    }
                    else
                    {
                        parm = parm + "|" + "NULL";
                    }

                    if (!string.IsNullOrEmpty(pointBatch.DetectionTypeCode))
                    {
                        parm = parm + "|" + "NDE Type：" + pointBatch.DetectionTypeCode;
                    }
                    else
                    {
                        parm = parm + "|" + "NDE Type：" + "NULL";
                    }

                    // PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../../Report/ReportPrint.aspx?report=1&pointBatchId={0}&parm={1}", this.PointBatchId, parm, "编辑 - ")));
                }
            }
        }
        #endregion

        private string GetNewTrust(string code)
        {
            int r_num = 0;
            string newTrustCode = string.Empty;
            if (code.Contains("R1") || code.Contains("R2") || code.Contains("R3") || code.Contains("R4"))
            {
                int indexR = code.LastIndexOf("R");
                if (indexR > 0)
                {
                    try
                    {
                        r_num = Convert.ToInt32(code.Substring(indexR + 1, 1)) + 1;
                        newTrustCode = code.Substring(0, code.Length - 1) + r_num.ToString();
                    }
                    catch { }
                }
            }
            else
            {
                newTrustCode = code + "R1";
            }

            return newTrustCode;
        }
    }
}