using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.HJGL.TestPackage
{
    public partial class TestPackageAudit : PageBase
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
        /// <summary>
        /// 未通过数
        /// </summary>
        public int Count
        {
            get
            {
                return (int)ViewState["Count"];
            }
            set
            {
                ViewState["Count"] = value;
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
                                                            where x.ProjectId == this.CurrUser.LoginProjectId select x).ToList();
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
            btnAudit.Hidden = false;
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
            string strSql = @"SELECT * FROM dbo.View_PTP_TestPackageAudit
                             WHERE ProjectId= @ProjectId AND PTP_ID=@PTP_ID";
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
            this.ShowGridItem();
        }

        /// <summary>
        /// 行颜色设置
        /// </summary>
        private void ShowGridItem()
        {
            Count = 0;
            int Count1 = 0, Count2 = 0, Count3 = 0, Count4 = 0;
            int rowsCount = this.Grid1.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                int IsoInfoCount = Funs.GetNewIntOrZero(this.Grid1.Rows[i].Values[3].ToString()); //总焊口
                int IsoInfoCountT = Funs.GetNewIntOrZero(this.Grid1.Rows[i].Values[4].ToString()); //完成总焊口
                int CountS = Funs.GetNewIntOrZero(this.Grid1.Rows[i].Values[5].ToString()); //合格数
                int CountU = Funs.GetNewIntOrZero(this.Grid1.Rows[i].Values[6].ToString());  //不合格数
                decimal Rate = 0;
                bool convertible = decimal.TryParse(this.Grid1.Rows[i].Values[9].ToString(), out Rate); //应检测比例
                decimal Ratio = Funs.GetNewDecimalOrZero(this.Grid1.Rows[i].Values[10].ToString()); //实际检测比例

                if (IsoInfoCount > IsoInfoCountT) //未焊完
                {
                    Count1 += 1;
                    this.Grid1.Rows[i].RowCssClass = "Cyan";
                }
                else if (Rate > Ratio) //已焊完，未达检测比例
                {
                    Count2 += 1;
                    this.Grid1.Rows[i].RowCssClass = "Yellow";
                }
                else if (CountU > 0) //已焊完，已达检测比例，但有不合格
                {
                    Count3 += 1;
                    this.Grid1.Rows[i].RowCssClass = "Green";
                }
                else
                {
                    Count4 += 1;
                    this.Grid1.Rows[i].RowCssClass = "Purple";
                }
            }

            Count = Count1 + Count2 + Count2;
            this.lab1.Text = Count1.ToString();
            this.lab2.Text = Count2.ToString();
            this.lab3.Text = Count3.ToString();
            this.lab4.Text = Count4.ToString();
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
                if (!string.IsNullOrEmpty(testPackageManage.Check1))
                {
                    drpInstallationSpecification.SelectedValue = testPackageManage.Check1;
                }
                if (!string.IsNullOrEmpty(testPackageManage.Check2))
                {
                    drpPressureTest.SelectedValue = testPackageManage.Check2;
                }                if (!string.IsNullOrEmpty(testPackageManage.Check3))
                {
                    drpWorkRecord.SelectedValue = testPackageManage.Check3;
                }                if (!string.IsNullOrEmpty(testPackageManage.Check4))
                {
                    drpNDTConform.SelectedValue = testPackageManage.Check4;
                }                if (!string.IsNullOrEmpty(testPackageManage.Check5))
                {
                    drpHotConform.SelectedValue = testPackageManage.Check5;
                }                if (!string.IsNullOrEmpty(testPackageManage.Check6))
                {
                    drpInstallationCorrectness.SelectedValue = testPackageManage.Check6;
                }                if (!string.IsNullOrEmpty(testPackageManage.Check7))
                {
                    drpMarkClearly.SelectedValue = testPackageManage.Check7;
                }                if (!string.IsNullOrEmpty(testPackageManage.Check8))
                {
                    drpIsolationOpening.SelectedValue = testPackageManage.Check8;
                }                if (!string.IsNullOrEmpty(testPackageManage.Check9))
                {
                    drpConstructionPlanAsk.SelectedValue = testPackageManage.Check9;
                }                if (!string.IsNullOrEmpty(testPackageManage.Check10))
                {
                    drpCover.SelectedValue = testPackageManage.Check10;
                }                if (!string.IsNullOrEmpty(testPackageManage.Check11))
                {
                    drpMeetRequirements.SelectedValue = testPackageManage.Check11;
                }                if (!string.IsNullOrEmpty(testPackageManage.Check12))
                {
                    drpStainlessTestWater.SelectedValue = testPackageManage.Check12;
                }
            }
        }
        #endregion


        #region 清空输入框
        /// <summary>
        /// 清空输入框
        /// </summary>
        private void SetTextTemp()
        {
            this.txtTestPackageNo.Text = string.Empty;
            this.txtRemark.Text = string.Empty;
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

        #region 试压前条件确认
        #region 审核检测单
        /// <summary>
        /// 审核检测单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAudit_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.TestPackageAuditMenuId, Const.BtnAuditing))
            {
                var updateTestPackage = BLL.TestPackageEditService.GetTestPackageByID(this.PTP_ID);
                if (updateTestPackage != null)
                {
                    if (Count == 0)
                    {
                        string isnoHot = BLL.TestPackageEditService.IsExistNoHotHardItem(this.PTP_ID);
                        if (string.IsNullOrEmpty(isnoHot))
                        {
                            string inspectionIsoRate = BLL.TestPackageEditService.InspectionIsoRate(this.PTP_ID);
                            if (string.IsNullOrEmpty(inspectionIsoRate))
                            {
                                updateTestPackage.Check1 = drpInstallationSpecification.SelectedValue;
                                updateTestPackage.Check2 = drpPressureTest.SelectedValue;
                                updateTestPackage.Check3 = drpWorkRecord.SelectedValue;
                                updateTestPackage.Check4 = drpNDTConform.SelectedValue;
                                updateTestPackage.Check5 = drpHotConform.SelectedValue;
                                updateTestPackage.Check6 = drpInstallationCorrectness.SelectedValue;
                                updateTestPackage.Check7 = drpMarkClearly.SelectedValue;
                                updateTestPackage.Check8 = drpIsolationOpening.SelectedValue;
                                updateTestPackage.Check9 = drpConstructionPlanAsk.SelectedValue;
                                updateTestPackage.Check10 = drpCover.SelectedValue;
                                updateTestPackage.Check11 = drpMeetRequirements.SelectedValue;
                                updateTestPackage.Check12 = drpStainlessTestWater.SelectedValue;
                                updateTestPackage.AduditDate = DateTime.Now;
                                updateTestPackage.Auditer = this.CurrUser.UserId;
                                BLL.TestPackageEditService.UpdateTestPackage(updateTestPackage);
                                this.InitTreeMenu();
                                this.BindGrid();
                                ShowNotify("保存成功！", MessageBoxIcon.Success);
                            }
                            else
                            {
                                Alert.ShowInTop(inspectionIsoRate, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        else
                        {
                            Alert.ShowInTop(isnoHot, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        Alert.ShowInTop("管线未全部通过不允许审核操作！", MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    Alert.ShowInTop("请选择要确认的单据！", MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
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

        #region  试压包打印
        /// <summary>
        ///  试压包打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.PTP_ID))
            {
                //string reportId = BLL.Const.HJGL_TrustReportId; // 试压包打印  待做模板                             
                //PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("../../Common/ReportPrint/ExReportPrint.aspx?ispop=1&reportId={0}&replaceParameter={1}&varValue={2}", reportId, this.PTP_ID, string.Empty, "打印 - ")));
            }
            else
            {
                ShowNotify("请选择无损委托记录！", MessageBoxIcon.Warning);
                return;
            }
        }
        #endregion
    }
}