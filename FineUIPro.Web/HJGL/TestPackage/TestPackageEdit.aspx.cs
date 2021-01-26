using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BLL;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.HJGL.TestPackage
{
    public partial class TestPackageEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 试压包主键
        /// </summary>
        public string PTP_ID
        {
            get
            {
                return (string)ViewState["PTP_ID"];
            }
            set
            {
                ViewState["PTP_ID"] = value;
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
                this.PTP_ID = string.Empty;
                this.InitTreeMenu();//加载树
            }
        }
        #endregion

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
            rootNode1.EnableClickEvent = true;
            this.tvControlItem.Nodes.Add(rootNode1);

            TreeNode rootNode2 = new TreeNode();
            rootNode2.NodeID = "2";
            rootNode2.Text = "安装工程";
            rootNode2.CommandName = "安装工程";
            rootNode2.Expanded = true;
            rootNode2.EnableClickEvent = true;
            this.tvControlItem.Nodes.Add(rootNode2);
            var pUnits = (from x in Funs.DB.Project_ProjectUnit where x.ProjectId == this.CurrUser.LoginProjectId select x).ToList();
            // 获取当前用户所在单位
            var currUnit = pUnits.FirstOrDefault(x => x.UnitId == this.CurrUser.UnitId);

            var unitWorkList = (from x in Funs.DB.WBS_UnitWork
                                where x.ProjectId == this.CurrUser.LoginProjectId
                                      && x.SuperUnitWork == null && x.UnitId != null && x.ProjectType != null
                                select x).ToList();
            List<Model.PTP_TestPackage> testPackageLists = (from x in Funs.DB.PTP_TestPackage
                                                            where x.ProjectId == this.CurrUser.LoginProjectId
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
                    tn1.EnableClickEvent = true;
                    rootNode1.Nodes.Add(tn1);
                    var testPackageUnitList = testPackageLists.Where(x => x.UnitWorkId == q.UnitWorkId).ToList();
                    BindNodes(tn1, testPackageUnitList);
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
                    tn2.CommandName = "单位工程";
                    tn2.EnableClickEvent = true;
                    rootNode2.Nodes.Add(tn2);
                    var testPackageUnitList = testPackageLists.Where(x => x.UnitWorkId == q.UnitWorkId).ToList();
                    BindNodes(tn2, testPackageUnitList);
                }
            }
        }
        #endregion

        #region 绑定树节点
        /// <summary>
        ///  绑定树节点
        /// </summary>
        /// <param name="node"></param>
        private void BindNodes(TreeNode node, List<Model.PTP_TestPackage> testPackageUnitList)
        {
            if (node.CommandName == "单位工程")
            {
                var dReports = from x in testPackageUnitList
                               where x.UnitWorkId == node.NodeID
                               orderby x.TestPackageNo descending
                               select x;
                foreach (var item in dReports)
                {
                    TreeNode newNode = new TreeNode();
                    if (!string.IsNullOrEmpty(item.TestPackageNo))
                    {
                        newNode.Text = item.TestPackageNo;
                    }
                    else
                    {
                        newNode.Text = "未知";
                    }
                    if (!item.AduditDate.HasValue || string.IsNullOrEmpty(item.Auditer))
                    {
                        newNode.Text = "<font color='#FF7575'>" + newNode.Text + "</font>";
                        node.Text = "<font color='#FF7575'>" + node.Text + "</font>";
                    }
                    newNode.NodeID = item.PTP_ID;
                    newNode.EnableClickEvent = true;
                    newNode.CommandName = "TestPackage";
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
            Model.WBS_UnitWork unitWork = BLL.UnitWorkService.getUnitWorkByUnitWorkId(this.tvControlItem.SelectedNodeID);
            Model.PTP_TestPackage testPackage = BLL.TestPackageEditService.GetTestPackageByID(this.tvControlItem.SelectedNodeID);
            if (unitWork != null)
            {
                this.btnMenuNew.Hidden = false;
                this.btnMenuModify.Hidden = true;
                this.btnMenuSee.Hidden = true;
                this.btnMenuDel.Hidden = true;
                //this.btnPrinter.Hidden = true;
                this.btnPrinterAll.Hidden = false;
            }
            else if (testPackage != null)
            {
                this.btnMenuNew.Hidden = true;
                this.btnMenuModify.Hidden = false;
                this.btnMenuSee.Hidden = false;
                this.btnMenuDel.Hidden = false;
                //this.btnPrinter.Hidden = false;
                this.btnPrinterAll.Hidden = true;
            }
            else
            {
                this.btnMenuNew.Hidden = true;
                this.btnMenuModify.Hidden = true;
                this.btnMenuSee.Hidden = true;
                this.btnMenuDel.Hidden = true;
                //this.btnPrinter.Hidden = true;
                this.btnPrinterAll.Hidden = true;
            }
            this.PTP_ID = tvControlItem.SelectedNodeID;
            this.BindGrid();
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            this.SetTextTemp();
            this.PageInfoLoad(); ///页面输入保存信息
            string strSql = @" SELECT ptpPipe.PT_PipeId, ptpPipe.PTP_ID, ptpPipe.PipelineId, ptpPipe.DesignPress, 
                               ptpPipe.DesignTemperature, ptpPipe.AmbientTemperature, ptpPipe.TestMedium, 
                               ptpPipe.TestMediumTemperature, ptpPipe.TestPressure, ptpPipe.HoldingTime,IsoInfo.PipelineCode,testMedium.MediumName
                               FROM dbo.PTP_PipelineList AS ptpPipe 
                               LEFT JOIN dbo.HJGL_Pipeline AS IsoInfo ON  ptpPipe.PipelineId = IsoInfo.PipelineId
							   LEFT JOIN dbo.Base_TestMedium  AS testMedium ON testMedium.TestMediumId = IsoInfo.TestMedium
                               WHERE  ptpPipe.PTP_ID=@PTP_ID";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            listStr.Add(new SqlParameter("@PTP_ID", this.PTP_ID));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(Grid1, tb1);
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        #region 加载页面输入保存信息
        /// <summary>
        /// 加载页面输入保存信息
        /// </summary>
        private void PageInfoLoad()
        {
            var testPackageManage = BLL.TestPackageEditService.GetTestPackageByID(this.PTP_ID);
            if (testPackageManage != null)
            {
                this.txtTestPackageNo.Text = testPackageManage.TestPackageNo;
                this.txtTestPackageName.Text = testPackageManage.TestPackageName;
                this.txtRemark.Text = testPackageManage.Remark;
                this.txtadjustTestPressure.Text = testPackageManage.AdjustTestPressure;
            }
        }
        #endregion

        #region 清空页面输入信息
        /// <summary>
        /// 清空页面输入信息
        /// </summary>
        private void SetTextTemp()
        {
            this.txtTestPackageNo.Text = string.Empty;
            this.txtTestPackageName.Text = string.Empty;
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

        #region 试压包 维护事件
        /// <summary>
        /// 增加试压包
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuNew_Click(object sender, EventArgs e)
        {
            if (this.tvControlItem.SelectedNode.CommandName == "单位工程")
            {
                if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.TestPackageEditMenuId, Const.BtnAdd))
                {
                    if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.CommandName == "单位工程")
                    {
                        this.SetTextTemp();
                        string window = String.Format("TestPackageItemEdit.aspx?unitWorkId={0}", this.tvControlItem.SelectedNodeID, "新增 - ");
                        PageContext.RegisterStartupScript(Window1.GetSaveStateReference(this.hdPTP_ID.ClientID)
                          + Window1.GetShowReference(window));
                    }
                }
                else
                {
                    ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("非单位工程类型无法新增！", MessageBoxIcon.Warning);
            }
        }
        #region 编辑试压包
        /// <summary>
        /// 编辑试压包
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            if (this.tvControlItem.SelectedNode.CommandName == "TestPackage")
            {
                if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.TestPackageEditMenuId, Const.BtnModify))
                {
                    var testPackageManage = BLL.TestPackageEditService.GetTestPackageByID(this.PTP_ID);
                    if (testPackageManage != null)
                    {
                        if (testPackageManage.AduditDate.HasValue)
                        {
                            Alert.ShowInTop("此试压包已条件确认！", MessageBoxIcon.Warning);
                            return;
                        }

                        string window = String.Format("TestPackageItemEdit.aspx?PTP_ID={0}", this.PTP_ID, "编辑 - ");
                        PageContext.RegisterStartupScript(Window1.GetSaveStateReference(this.hdPTP_ID.ClientID)
                          + Window1.GetShowReference(window));
                    }
                    else
                    {
                        ShowNotify("请选择要修改的试压包记录！", MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("非试压包类型无法编辑！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 查看试压包
        /// <summary>
        /// 查看试压包
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuSee_Click(object sender, EventArgs e)
        {
            if (this.tvControlItem.SelectedNode.CommandName == "TestPackage")
            {
                var testPackageManage = BLL.TestPackageEditService.GetTestPackageByID(this.PTP_ID);
                if (testPackageManage != null)
                {
                    string window = String.Format("TestPackageItemEdit.aspx?PTP_ID={0}&type=see", this.PTP_ID, "编辑 - ");
                    PageContext.RegisterStartupScript(Window1.GetSaveStateReference(this.hdPTP_ID.ClientID)
                      + Window1.GetShowReference(window));
                }
                else
                {
                    ShowNotify("请选择要修改的试压包记录！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("非试压包类型无法查看！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 删除试压包
        /// <summary>
        /// 删除试压包
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDel_Click(object sender, EventArgs e)
        {
            if (this.tvControlItem.SelectedNode.CommandName == "TestPackage")
            {
                if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.TestPackageEditMenuId, Const.BtnDelete))
                {
                    var testPackageManage = BLL.TestPackageEditService.GetTestPackageByID(this.PTP_ID);
                    if (testPackageManage != null)
                    {
                        if (testPackageManage.AduditDate.HasValue)
                        {
                            Alert.ShowInTop("此试压单已审核！", MessageBoxIcon.Warning);
                            return;
                        }
                        BLL.TestPackageEditService.DeletePipelineListByPTP_ID(this.PTP_ID);
                        BLL.TestPackageEditService.DeleteTestPackage(this.PTP_ID);
                        //BLL.Sys_LogService.AddLog(BLL.Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.TestPackageEditMenuId, Const.BtnDelete, this.PTP_ID);
                        Alert.ShowInTop("删除成功！", MessageBoxIcon.Success);
                        this.InitTreeMenu();
                        this.BindGrid();
                    }
                    else
                    {
                        ShowNotify("请选择要删除的试压包记录！", MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                ShowNotify("非试压包类型无法删除！", MessageBoxIcon.Warning);
            }
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
            this.PTP_ID = this.hdPTP_ID.Text;
            this.BindGrid();
            this.InitTreeMenu();
            this.hdPTP_ID.Text = string.Empty;
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

        protected void btnPrinterAll_Click(object sender, EventArgs e)
        {
            string unitWorkId = this.tvControlItem.SelectedNodeID;
            var p = BLL.UnitWorkService.GetUnitWorkByUnitWorkId(unitWorkId);
            if (p != null)
            {
                string varValue = string.Empty;
                var project = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId);
                if (project != null)
                {
                    varValue = project.ProjectName;
                    varValue = varValue + "|" + p.UnitWorkName;
                }
                if (!string.IsNullOrEmpty(varValue))
                {
                    varValue = HttpUtility.UrlEncodeUnicode(varValue);
                }
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("../../ReportPrint/ExReportPrint.aspx?ispop=1&reportId={0}&replaceParameter={1}&varValue={2}&projectId={3}", BLL.Const.HJGL_TestPackageListReportId, unitWorkId, varValue, this.CurrUser.LoginProjectId)));
            }
            else
            {
                ShowNotify("请选择单位工程！", MessageBoxIcon.Warning);
                return;
            }
        }
    }
}