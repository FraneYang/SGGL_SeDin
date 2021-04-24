using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BLL;

namespace FineUIPro.Web.HJGL.RepairAndExpand
{
    public partial class RepairTrust : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtTrustDateMonth.Text = string.Format("{0:yyyy-MM}", DateTime.Now);
                this.ddlPageSize.SelectedValue = this.Grid1.PageSize.ToString();
                //BLL.Base_UnitService.InitProjectUnitDropDownList(this.drpNDEUnit, false, this.CurrUser.LoginProjectId, BLL.Const.UnitType_4, "请选择");
                this.InitTreeMenu();//加载树
            }
        }

        #region 加载树项目-月份
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
                    tn1.EnableExpandEvent = true;
                    tn1.CommandName = "单位工程";
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
        #endregion

        #region 绑定树节点
        /// <summary>
        ///  绑定树节点
        /// </summary>
        /// <param name="node"></param>
        private void BindNodes(TreeNode node)
        {
            var p = from x in Funs.DB.HJGL_Batch_BatchTrust
                    where x.UnitWorkId == node.NodeID && x.TrustType == "R"
                         && x.TrustDate < Convert.ToDateTime(this.txtTrustDateMonth.Text.Trim() + "-01").AddMonths(1)
                         && x.TrustDate >= Convert.ToDateTime(this.txtTrustDateMonth.Text.Trim() + "-01")
                    select x;
            if (p.Count() > 0)
            {
                TreeNode newNode = new TreeNode();
                newNode.Text = "探伤类型";
                newNode.NodeID = "探伤类型";
                node.Nodes.Add(newNode);
            }

        }
        protected void tvControlItem_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
        {
            e.Node.Nodes.Clear();
            e.Node.Expanded = true;
            if (e.Node.CommandName == "单位工程")
            {
                var detectionTypes = from x in Funs.DB.Base_DetectionType
                                     orderby x.DetectionTypeCode
                                     select new { x.DetectionTypeId, x.DetectionTypeCode, x.DetectionTypeName };
                foreach (var item in detectionTypes)
                {
                    var pointManages = from x in Funs.DB.View_Batch_BatchTrust
                                       where x.ProjectId == this.CurrUser.LoginProjectId
                                       && x.UnitWorkId == e.Node.NodeID && x.TrustType == "R"
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
                        Text = "委托单号",
                        NodeID = "委托单号",
                    };
                    newNode.Nodes.Add(tn1);
                }
            }

            if (e.Node.CommandName == "探伤类型")
            {
                ///单号
                var trusts = from x in Funs.DB.HJGL_Batch_BatchTrust
                             where x.TrustDate < Convert.ToDateTime(this.txtTrustDateMonth.Text.Trim() + "-01").AddMonths(1)
                             && x.TrustDate >= Convert.ToDateTime(this.txtTrustDateMonth.Text.Trim() + "-01") && x.TrustType == "R"
                             && x.ProjectId == this.CurrUser.LoginProjectId && x.TrustBatchCode.Contains(this.txtSearchCode.Text.Trim())
                             && x.UnitWorkId == e.Node.ParentNode.NodeID
                             && x.DetectionTypeId == e.NodeID.Split('|')[0]
                             orderby x.TrustBatchCode descending
                             select x;
                foreach (var trust in trusts)
                {
                    TreeNode newNode = new TreeNode();
                    string code= "FXWT-" + trust.TrustBatchCode.Substring(trust.TrustBatchCode.Length - 4);
                    if (string.Format("{0:yyyy-MM-dd}", trust.TrustDate) == string.Format("{0:yyyy-MM-dd}", System.DateTime.Now))
                    {
                        newNode.Text = "<font color='#EE0000'>" + code + "</font>";
                        newNode.ToolTip = "当天委托单";
                    }
                    else
                    {
                        newNode.Text = code;
                        newNode.ToolTip = "非当天委托单";
                    }
                    newNode.NodeID = trust.TrustBatchId;
                    newNode.EnableClickEvent = true;
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
            PageInfo();
            this.BindGrid();
        }
        #endregion

        private void PageInfo()
        {
            Model.View_Batch_BatchTrust trust = BLL.Batch_BatchTrustService.GetBatchTrustViewById(this.tvControlItem.SelectedNodeID);
            if (trust != null)
            {
                this.txtTrustBatchCode.Text = trust.TrustBatchCode;
                if (trust.TrustDate != null)
                {
                    this.txtTrustDate.Text = string.Format("{0:yyyy-MM-dd}", trust.TrustDate);
                }
                this.txtDetectionTypeCode.Text = trust.DetectionTypeCode;
                if (trust.IsCheck == true)
                {
                    lbIsCheck.Text = "已检测";
                }
                else
                {
                    lbIsCheck.Text = "未检测";
                }

                if (trust.IsAudit == true)
                {
                    lbIsAudit.Text = "已审核";
                }
                else
                {
                    lbIsAudit.Text = "未审核";
                }
                if (!string.IsNullOrEmpty(trust.NDEUnit))
                {
                    var unit = BLL.UnitService.GetUnitByUnitId(trust.NDEUnit);
                    if (unit != null)
                    {
                        lbNDEUnit.Text = unit.UnitName;
                    }
                }
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

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            if (this.tvControlItem.SelectedNode != null)
            {
                string strSql = @"SELECT * FROM dbo.View_Batch_BatchTrustItem d WHERE TrustBatchId=@TrustBatchId ";
                List<SqlParameter> listStr = new List<SqlParameter>
                {

                };
                listStr.Add(new SqlParameter("@TrustBatchId", this.tvControlItem.SelectedNodeID));
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

        protected void btnAudit_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_RepairTrustMenuId, Const.BtnAuditing))
            {
                if (!string.IsNullOrEmpty(this.tvControlItem.SelectedNodeID))
                {
                    string trustBatchId = this.tvControlItem.SelectedNodeID;
                    BLL.Batch_BatchTrustService.UpdateBatchTrustAudit(trustBatchId, true);
                    PageInfo();
                    ShowNotify("该委托单已审核！", MessageBoxIcon.Success);
                }
                else
                {
                    ShowNotify("请选择要审核委托单！", MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_RepairTrustMenuId, Const.BtnDelete))
            {
                Model.SGGLDB db = Funs.DB;
                if (!string.IsNullOrEmpty(this.tvControlItem.SelectedNodeID))
                {
                    string trustBatchId = this.tvControlItem.SelectedNodeID;
                    string trustBatchCode = this.tvControlItem.SelectedNode.Text;
                    var trust = BLL.Batch_BatchTrustService.GetBatchTrustById(trustBatchId);

                    if (trust.IsAudit == true)
                    {
                        ShowNotify("该委托单已审核，不能删除！", MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        var batchItem = from y in db.HJGL_Batch_BatchTrustItem where y.TrustBatchId == trustBatchId select y;
                        foreach (var item in batchItem)
                        {
                            Model.HJGL_Batch_PointBatchItem pointBatchItem = null;
                            if (item.PointBatchItemId != null)
                            {
                                pointBatchItem = db.HJGL_Batch_PointBatchItem.FirstOrDefault(x => x.PointBatchItemId == item.PointBatchItemId);
                            }
                            else
                            {
                                pointBatchItem = db.HJGL_Batch_PointBatchItem.FirstOrDefault(x => x.RepairRecordId == item.RepairRecordId);
                            }
                            if (pointBatchItem != null)
                            {
                                pointBatchItem.IsBuildTrust = null;
                            }
                        }

                        db.HJGL_Batch_BatchTrustItem.DeleteAllOnSubmit(batchItem);
                        db.HJGL_Batch_BatchTrust.DeleteOnSubmit(trust);
                        db.SubmitChanges();
                        this.InitTreeMenu();
                        this.BindGrid();
                        ShowNotify("已删除委托单：" + trustBatchCode, MessageBoxIcon.Success);
                    }
                }
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_RepairTrustMenuId, Const.BtnPrint))
            {
                if (!string.IsNullOrEmpty(this.tvControlItem.SelectedNodeID))
                {
                    string varValue = string.Empty;
                    string trustBatchId = this.tvControlItem.SelectedNodeID;

                    Model.View_Batch_BatchTrust trust = BLL.Batch_BatchTrustService.GetBatchTrustViewById(trustBatchId);
                    if (trust != null)
                    {
                        varValue = trust.TrustBatchCode + "|" + trust.TrustDate.Value.Date + "|";
                        var project = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId);
                        if (project != null)
                        {
                            varValue = varValue + project.ProjectName + "|" + project.ProjectCode + "|";
                        }
                        //varValue = varValue + trust.InstallationName + "|" + trust.InstallationCode;
                        if (!string.IsNullOrEmpty(varValue))
                        {
                            varValue = HttpUtility.UrlEncodeUnicode(varValue);
                        }

                        //PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../../Common/ReportPrint/ExReportPrint.aspx?ispop=1&reportId={0}&replaceParameter={1}&varValue={2}&projectId=0", BLL.Const.CheckTrustReport, trustBatchId, varValue)));
                    }
                }
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }

        #region 判断是否可删除
        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private bool judgementDelete(string id, bool isShow)
        {
            string content = string.Empty;
            //if (BLL.HJGL_HotProessManageEditService.GetHotProessByJotId(id) > 0)
            //{
            //    content = "热处理已经使用了该焊口，不能删除！";
            //}
            //if (BLL.Funs.DB.HJGL_CH_TrustItem.FirstOrDefault(x => x.TrustBatchItemId == id) != null)
            //{
            //    content = "无损委托已经使用了该焊口，不能删除！";
            //}
            //if (BLL.Funs.DB.HJGL_CH_CheckItem.FirstOrDefault(x => x.TrustBatchItemId == id) != null)
            //{
            //    content = "检测单已经使用了该焊口，不能删除！";
            //}
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
    }
}