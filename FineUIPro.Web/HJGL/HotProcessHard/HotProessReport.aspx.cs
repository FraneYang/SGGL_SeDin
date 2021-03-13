using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HJGL.HotProcessHard
{
    public partial class HotProessReport : PageBase
    {
        #region 定义项
        /// <summary>
        /// 热处理委托主键
        /// </summary>
        public string HotProessTrustId
        {
            get
            {
                return (string)ViewState["HotProessTrustId"];
            }
            set
            {
                ViewState["HotProessTrustId"] = value;
            }
        }
        #endregion

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ddlPageSize.SelectedValue = this.Grid1.PageSize.ToString();

                this.HotProessTrustId = string.Empty;
                this.InitTreeMenu();//加载树
            }
        }
        #endregion

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
            var TrustItemList = (from x in Funs.DB.HJGL_HotProess_TrustItem
                                 join y in Funs.DB.HJGL_HotProess_Trust on x.HotProessTrustId equals y.HotProessTrustId
                                 where y.ProjectId == this.CurrUser.LoginProjectId
                                 select new { x.WeldJointId, y.UnitWorkId }).ToList();
            var ReportList = (from x in Funs.DB.HJGL_HotProess_TrustItem
                              join y in Funs.DB.HJGL_HotProess_Trust on x.HotProessTrustId equals y.HotProessTrustId
                              where y.ProjectId == this.CurrUser.LoginProjectId && x.IsCompleted == true
                              select new { x.WeldJointId, y.UnitWorkId }).ToList();
            if (unitWork1.Count() > 0)
            {
                foreach (var q in unitWork1)
                {
                    var trustItems = (from x in TrustItemList where x.UnitWorkId == q.UnitWorkId select x).ToList();
                    var reportItems = (from x in ReportList where x.UnitWorkId == q.UnitWorkId select x).ToList();
                    int num = trustItems.Count() - reportItems.Count();
                    var u = BLL.UnitService.GetUnitByUnitId(q.UnitId);
                    TreeNode tn1 = new TreeNode();
                    tn1.NodeID = q.UnitWorkId;
                    if (num > 0)
                    {
                        tn1.Text = q.UnitWorkName + "(" + num + ")";
                        tn1.ToolTip = "未生成热处理报告焊口总数：" + num;
                    }
                    else
                    {
                        tn1.Text = q.UnitWorkName;
                    }
                    tn1.CommandName = "单位工程";
                    rootNode1.Nodes.Add(tn1);
                    BindNodes(tn1);
                }
            }
            if (unitWork2.Count() > 0)
            {
                foreach (var q in unitWork2)
                {
                    var trustItems = (from x in TrustItemList where x.UnitWorkId == q.UnitWorkId select x).ToList();
                    var reportItems = (from x in ReportList where x.UnitWorkId == q.UnitWorkId select x).ToList();
                    int num = trustItems.Count() - reportItems.Count();
                    var u = BLL.UnitService.GetUnitByUnitId(q.UnitId);
                    TreeNode tn2 = new TreeNode();
                    tn2.NodeID = q.UnitWorkId;
                    if (num > 0)
                    {
                        tn2.Text = q.UnitWorkName + "(" + num + ")";
                        tn2.ToolTip = "未生成热处理报告焊口总数：" + num;
                    }
                    else
                    {
                        tn2.Text = q.UnitWorkName;
                    }
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
            List<Model.HJGL_HotProess_Trust> trustLists = new List<Model.HJGL_HotProess_Trust>();

            if (!string.IsNullOrEmpty(this.txtSearchNo.Text.Trim()))
            {
                trustLists = (from x in Funs.DB.HJGL_HotProess_Trust where x.HotProessTrustNo.Contains(this.txtSearchNo.Text.Trim()) orderby x.HotProessTrustNo select x).ToList();
            }
            else
            {
                trustLists = (from x in Funs.DB.HJGL_HotProess_Trust orderby x.HotProessTrustNo select x).ToList();
            }
            var trustList = from x in trustLists
                            where x.ProjectId == this.CurrUser.LoginProjectId
                                  && x.UnitWorkId == node.NodeID
                            select x;
            foreach (var item in trustList)
            {
                TreeNode newNode = new TreeNode();
                newNode.Text = item.HotProessTrustNo;
                newNode.NodeID = item.HotProessTrustId;
                newNode.ToolTip = item.HotProessTrustNo;
                newNode.CommandName = "委托单号";
                newNode.EnableClickEvent = true;
                node.Nodes.Add(newNode);
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

        #region DropDownList下拉选择事件
        /// <summary>
        /// 项目下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpProjectId_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.InitTreeMenu();
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            string strSql = string.Empty;
            List<SqlParameter> listStr = new List<SqlParameter>();
            this.SetTextTemp();
            if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.CommandName == "委托单号")
            {
                var hotProessTrust = BLL.HotProess_TrustService.GetHotProessTrustById(this.tvControlItem.SelectedNodeID);
                if (hotProessTrust != null)
                {
                    this.HotProessTrustId = hotProessTrust.HotProessTrustId;
                    strSql = @"SELECT * "
                    + @" FROM dbo.View_HJGL_HotProess_TrustItem AS Trust"
                    + @" WHERE Trust.ProjectId= @ProjectId AND Trust.HotProessTrustId=@HotProessTrustId ";

                    listStr.Add(new SqlParameter("@ProjectId", hotProessTrust != null ? hotProessTrust.ProjectId : this.CurrUser.LoginProjectId));
                    listStr.Add(new SqlParameter("@HotProessTrustId", this.HotProessTrustId));

                    if (!string.IsNullOrEmpty(this.txtIsoNo.Text.Trim()))
                    {
                        strSql += @" and Trust.PipelineCode like '%'+@PipelineCode+'%' ";
                        listStr.Add(new SqlParameter("@PipelineCode", this.txtIsoNo.Text.Trim()));
                    }

                    SqlParameter[] parameter = listStr.ToArray();
                    DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
                    // 2.获取当前分页数据
                    //var table = this.GetPagedDataTable(Grid1, tb1);
                    Grid1.RecordCount = tb.Rows.Count;
                    //tb = GetFilteredTable(Grid1.FilteredData, tb);
                    var table = this.GetPagedDataTable(Grid1, tb);
                    Grid1.DataSource = table;
                    Grid1.DataBind();
                }
            }
            this.PageInfoLoad(); ///页面输入提交信息
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

        #region 加载页面输入提交信息
        /// <summary>
        /// 加载页面输入提交信息
        /// </summary>
        private void PageInfoLoad()
        {
            var trust = BLL.HotProess_TrustService.GetHotProessTrustById(this.HotProessTrustId);
            if (trust != null)
            {
                this.txtHotProessTrustNo.Text = trust.HotProessTrustNo;
                if (trust.ProessDate.HasValue)
                {
                    this.txtProessDate.Text = string.Format("{0:yyyy-MM-dd}", trust.ProessDate);
                }
                this.txtProessMethod.Text = trust.ProessMethod;
                this.txtProessEquipment.Text = trust.ProessEquipment;
                if (!string.IsNullOrEmpty(trust.Tabler))
                {
                    this.txtTabler.Text = BLL.UserService.GetUserNameByUserId(trust.Tabler);
                }
                this.txtReport.Text = trust.ReportNo;
            }
        }
        #endregion

        #region 清空文本
        /// <summary>
        /// 清空文本
        /// </summary>
        private void SetTextTemp()
        {
            this.txtHotProessTrustNo.Text = string.Empty;
            this.txtProessDate.Text = string.Empty;
            this.txtProessMethod.Text = string.Empty;
            this.txtProessEquipment.Text = string.Empty;
            this.txtTabler.Text = string.Empty;
            this.txtReport.Text = string.Empty;
        }
        #endregion
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
            this.InitTreeMenu();
            if (!string.IsNullOrEmpty(this.hdHotProessTrustId.Text))
            {
                this.HotProessTrustId = this.hdHotProessTrustId.Text;
            }
            this.tvControlItem.SelectedNodeID = this.HotProessTrustId;
            this.BindGrid();
            this.hdHotProessTrustId.Text = string.Empty;
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

        #region 右键编辑热处理报告
        /// <summary>
        /// 热处理报告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuHotProessReport_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_HotProessReportMenuId, Const.BtnSave))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HotProessReport.aspx?HotProessTrustItemId={0}", this.Grid1.SelectedRowID, "编辑热处理报告 - ")));
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_HotProessReportMenuId, Const.BtnSave))
            {
                var trustManage = BLL.HotProess_TrustService.GetHotProessTrustById(this.tvControlItem.SelectedNodeID);
                if (trustManage != null)
                {
                    string window = String.Format("HotProessReportEdit.aspx?HotProessTrustId={0}", trustManage.HotProessTrustId, "编辑 - ");
                    PageContext.RegisterStartupScript(Window1.GetShowReference(window));
                }
                else
                {
                    ShowNotify("请选择要修改的热处理委托记录！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        protected void btnPrinter_Click(object sender, EventArgs e)
        {
            var trust = BLL.HotProess_TrustService.GetHotProessTrustById(this.tvControlItem.SelectedNodeID);
            if (trust != null)
            {
                if (string.IsNullOrEmpty(trust.ReportNo))
                {
                    ShowNotify("请先完善热处理报告信息，保存报告编号", MessageBoxIcon.Warning);
                    return;
                }
                string varValue = string.Empty;
                var project = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId);
                if (project != null)
                {
                    varValue = project.ProjectName;
                    var unitWork = BLL.UnitWorkService.GetUnitWorkByUnitWorkId(trust.UnitWorkId);
                    if (unitWork != null)
                    {
                        varValue = varValue + "|" + unitWork.UnitWorkName;
                    }
                    varValue = varValue + "|" + trust.ReportNo;
                    varValue = varValue + "|" + trust.ProessMethod;
                    varValue = varValue + "|" + trust.ProessEquipment;
                }
                if (!string.IsNullOrEmpty(varValue))
                {
                    varValue = HttpUtility.UrlEncodeUnicode(varValue);
                }
                List<SqlParameter> listStr = new List<SqlParameter>();
                listStr.Add(new SqlParameter("@HotProessTrustId", this.tvControlItem.SelectedNodeID));
                listStr.Add(new SqlParameter("@Flag", "0"));
                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = BLL.SQLHelper.GetDataTableRunProc("SP_HJGL_HotProessReportItem", parameter);
                if (tb.Rows.Count <= 7)
                {
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("../../ReportPrint/ExReportPrint.aspx?ispop=1&reportId={0}&replaceParameter={1}&varValue={2}&projectId=0", BLL.Const.HJGL_HotProessReportId1, this.tvControlItem.SelectedNodeID, varValue, "打印 - ")));
                }
                else
                {
                    PageContext.RegisterStartupScript(Window3.GetShowReference(String.Format("../../ReportPrint/ExReportPrint.aspx?ispop=1&reportId={0}&replaceParameter={1}&varValue={2}&projectId=0", Const.HJGL_HotProessReportId2, this.tvControlItem.SelectedNodeID, varValue, "打印 - ")));
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("../../ReportPrint/ExReportPrint.aspx?ispop=1&reportId={0}&replaceParameter={1}&varValue={2}&projectId=0", Const.HJGL_HotProessReportId1, this.tvControlItem.SelectedNodeID, varValue, "打印 - ")));
                }
            }
            else
            {
                ShowNotify("请选择委托单！", MessageBoxIcon.Warning);
                return;
            }
        }
    }
}