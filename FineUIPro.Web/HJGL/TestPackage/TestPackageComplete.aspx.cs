using System;using System.Collections.Generic;using System.Data;using System.Data.SqlClient;using System.Linq;using BLL;using Newtonsoft.Json.Linq;namespace FineUIPro.Web.HJGL.TestPackage{    public partial class TestPackageComplete : PageBase    {        #region 定义项
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
        #region 加载页面        /// <summary>                             /// 加载页面                             /// </summary>                             /// <param name="sender"></param>                             /// <param name="e"></param>        protected void Page_Load(object sender, EventArgs e)        {            if (!IsPostBack)            {                               this.ddlPageSize.SelectedValue = this.Grid1.PageSize.ToString();                this.PTP_ID = string.Empty;                this.InitTreeMenu();//加载树            }        }        #endregion        #region 加载树装置-单位-工作区
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
                    rootNode2.Nodes.Add(tn2);
                    var testPackageUnitList = testPackageLists.Where(x => x.UnitWorkId == q.UnitWorkId).ToList();
                    BindNodes(tn2, testPackageUnitList);
                }
            }
        }
        #endregion        #region 绑定树节点
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
                this.txtadjustTestPressure.Text = testPackageManage.AdjustTestPressure;
                this.txtAmbientTemperature.Text = testPackageManage.AmbientTemperature.ToString();
                this.txtFinishDef.Text = testPackageManage.FinishDef;
                this.txtHoldingTime.Text = testPackageManage.HoldingTime.ToString();
                this.txtTestDate.Text = testPackageManage.TestDate?.ToString("yyyy-MM-dd");
                this.txtTestMediumTemperature.Text = testPackageManage.TestMediumTemperature.ToString();
            }
        }
        #endregion

        #region 清空页面输入信息
        /// <summary>
        /// 清空页面输入信息
        /// </summary>
        private void SetTextTemp()
        {
            this.txtadjustTestPressure.Text = string.Empty;
            this.txtAmbientTemperature.Text = string.Empty;
            this.txtFinishDef.Text = string.Empty;
            this.txtHoldingTime.Text = string.Empty;
            this.txtTestDate.Text = string.Empty;
            this.txtTestMediumTemperature.Text = string.Empty;
        }
        #endregion
        #endregion
        #region 分页排序
        #region 页索引改变事件        /// <summary>                                /// 页索引改变事件                                /// </summary>                                /// <param name="sender"></param>                                /// <param name="e"></param>        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)        {            BindGrid();        }        #endregion        #region 排序        /// <summary>        /// 排序        /// </summary>        /// <param name="sender"></param>        /// <param name="e"></param>        protected void Grid1_Sort(object sender, GridSortEventArgs e)        {            BindGrid();        }        #endregion        #region 分页选择下拉改变事件        /// <summary>        /// 分页选择下拉改变事件        /// </summary>        /// <param name="sender"></param>        /// <param name="e"></param>        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)        {            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);            BindGrid();        }        #endregion        #endregion                #region 关闭弹出窗口及刷新页面        /// <summary>        /// 关闭弹出窗口        /// </summary>        /// <param name="sender"></param>        /// <param name="e"></param>        protected void Window1_Close(object sender, WindowCloseEventArgs e)        {            this.PTP_ID = this.hdPTP_ID.Text;            this.BindGrid();            this.InitTreeMenu();            this.hdPTP_ID.Text = string.Empty;        }                /// <summary>        /// 查询        /// </summary>        /// <param name="sender"></param>        /// <param name="e"></param>        protected void Tree_TextChanged(object sender, EventArgs e)        {            this.InitTreeMenu();            this.BindGrid();        }

        #endregion

        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetSaveStateReference(this.hdPTP_ID.ClientID)+ Window1.GetShowReference(String.Format("TestPackageCompleteEdit.aspx?PTP_ID={0}", this.tvControlItem.SelectedNodeID, "操作 - ")));
        }
    }}