using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
                this.txtSearchDate.Text = string.Format("{0:yyyy-MM}", System.DateTime.Now);             
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
            DateTime startTime = Convert.ToDateTime(this.txtSearchDate.Text.Trim() + "-01");
            DateTime endTime = startTime.AddMonths(1);
            this.tvControlItem.Nodes.Clear();
            var totalUnitWork = from x in Funs.DB.WBS_UnitWork select x;
            var totalUnit = from x in Funs.DB.Project_ProjectUnit select x;
            
            ////区域
            var pUnitWork = (from x in totalUnitWork where x.ProjectId == this.CurrUser.LoginProjectId select x).ToList();
            ////单位
            var pUnits = (from x in totalUnit where x.ProjectId == this.CurrUser.LoginProjectId select x).ToList();
            
            pUnits = (from x in pUnits
                      join y in pUnitWork on x.UnitId equals y.UnitId
                      select x).Distinct().ToList();
            List<Model.PTP_TestPackage> testPackageLists = (from x in Funs.DB.PTP_TestPackage
                                                            where x.ProjectId == this.CurrUser.LoginProjectId && x.TableDate >= startTime && x.TableDate < endTime
                                                                select x).ToList();
                    this.BindNodes(null,null, pUnitWork, pUnits, testPackageLists);
        }
        #endregion

        #region 绑定树节点
        /// <summary>
        ///  绑定树节点
        /// </summary>
        /// <param name="node"></param>
        private void BindNodes(TreeNode node1, TreeNode node2, List<Model.WBS_UnitWork> pUnitWork, List<Model.Project_ProjectUnit> pUnits,List<Model.PTP_TestPackage> testPackageUnitList)
        {
            var pUnitDepth = pUnits.FirstOrDefault(x => x.UnitId == this.CurrUser.UnitId);
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

                this.BindNodes(rootNode1, rootNode2, pUnitWork, pUnits, testPackageUnitList);
            }
            else {
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
                    foreach (var q in workAreas)
                    {
                        var u = BLL.UnitService.GetUnitByUnitId(q.UnitId);
                        TreeNode newNode = new TreeNode();
                        newNode.Text = q.UnitWorkName;
                        newNode.NodeID = q.UnitWorkId;
                        newNode.ToolTip = "施工单位：" + u.UnitName;
                        newNode.CommandName = "单位工程";
                        newNode.EnableExpandEvent = true;
                        node1.Nodes.Add(newNode);
                       var testPackageLists = testPackageUnitList.Where(x => x.UnitWorkId == q.UnitWorkId).ToList();
                        BindChildNodes(newNode, pUnitWork, testPackageLists);

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
                    foreach (var q in workAreas)
                    {
                        var u = BLL.UnitService.GetUnitByUnitId(q.UnitId);
                        TreeNode newNode = new TreeNode();
                        newNode.Text = q.UnitWorkName;
                        newNode.NodeID = q.UnitWorkId;
                        newNode.ToolTip = "施工单位：" + u.UnitName;
                        newNode.CommandName = "单位工程";
                        newNode.EnableExpandEvent = true;
                        node2.Nodes.Add(newNode);
                        var testPackageLists = testPackageUnitList.Where(x => x.UnitWorkId == q.UnitWorkId).ToList();
                        BindChildNodes(newNode, pUnitWork, testPackageLists);
                    }
                }
            }
        }

        //绑定子节点
        private void BindChildNodes(TreeNode ChildNodes, List<Model.WBS_UnitWork> pUnitWork, List<Model.PTP_TestPackage> testPackageUnitList)
        {
            if (ChildNodes.CommandName == "单位工程")
            {
                var pointListMonth = (from x in testPackageUnitList
                                      select string.Format("{0:yyyy-MM}", x.TableDate)).Distinct();
                foreach (var item in pointListMonth)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = item;
                    newNode.NodeID = item + "|" + ChildNodes.NodeID;
                    newNode.CommandName = "月份";
                    ChildNodes.Nodes.Add(newNode);
                    this.BindChildNodes(newNode, pUnitWork, testPackageUnitList);
                }
            }
            else if (ChildNodes.CommandName == "月份") {
                DateTime startTime = Convert.ToDateTime(this.txtSearchDate.Text.Trim() + "-01");
                DateTime endTime = startTime.AddMonths(1);
                var dReports = from x in testPackageUnitList
                               where x.UnitWorkId == ChildNodes.ParentNode.NodeID
                               && x.TableDate >= startTime && x.TableDate < endTime
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
                        ChildNodes.Text = "<font color='#FF7575'>" + ChildNodes.Text + "</font>";
                        ChildNodes.ParentNode.Text = "<font color='#FF7575'>" + ChildNodes.ParentNode.Text + "</font>";
                    }
                    newNode.NodeID = item.PTP_ID;
                    newNode.EnableClickEvent = true;
                    ChildNodes.Nodes.Add(newNode);
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
            string strSql = @"SELECT ptp.ProjectId, ptp.PTP_ID,ptpPipe.PT_PipeId, UnitWork.UnitWorkCode,IsoInfo.PipelineCode,
                                   class.PipingClassCode, IsoInfo.TestPressure,IsoInfo.TestMedium,ser.MediumName
                               FROM dbo.PTP_TestPackage AS ptp
                               LEFT JOIN dbo.PTP_PipelineList AS ptpPipe ON ptp.PTP_ID=ptpPipe.PTP_ID
                               LEFT JOIN dbo.HJGL_Pipeline AS IsoInfo ON  IsoInfo.PipelineId = ptpPipe.PipelineId
                               LEFT JOIN WBS_UnitWork AS UnitWork ON IsoInfo.UnitWorkId=UnitWork.UnitWorkId
                               LEFT JOIN dbo.Base_Medium AS ser ON  ser.MediumId = IsoInfo.MediumId
							   LEFT JOIN dbo.Base_PipingClass class ON class.PipingClassId = IsoInfo.PipingClassId
                               WHERE ptp.ProjectId= @ProjectId AND ptp.PTP_ID=@PTP_ID";
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
            this.btnEdit.Hidden = true;
            this.btnDelete.Hidden = true;
            this.btnPrint.Hidden = true;
            var testPackageManage = BLL.TestPackageEditService.GetTestPackageByID(this.PTP_ID);
            if (testPackageManage != null)
            {
                this.txtTestPackageNo.Text = testPackageManage.TestPackageNo;
                //if (!string.IsNullOrEmpty(testPackageManage.UnitId))
                //{
                //    var unit = BLL.Base_UnitService.GetUnit(testPackageManage.UnitId);
                //    if (unit != null)
                //    {
                //        this.drpUnit.Text = unit.UnitName;
                //    }
                //}
                if (!string.IsNullOrEmpty(testPackageManage.UnitWorkId))
                {
                    var install = BLL.UnitWorkService.getUnitWorkByUnitWorkId(testPackageManage.UnitWorkId);
                    if (install != null)
                    {
                        this.drpInstallation.Text = install.UnitWorkName;
                    }
                }

                this.txtTestPackageName.Text = testPackageManage.TestPackageName;
                //this.txtTestPackageCode.Text = testPackageManage.TestPackageCode;
                if (!string.IsNullOrEmpty(testPackageManage.TestType))
                {
                    var testType = BLL.Base_PressureService.GetPressureByPressureId(testPackageManage.TestType);
                    if (testType != null)
                    {
                        this.drpTestType.Text = testType.PressureName;
                    }
                }
                this.txtTestService.Text = testPackageManage.TestService;
                this.txtTestHeat.Text = testPackageManage.TestHeat;
                this.txtTestAmbientTemp.Text = testPackageManage.TestAmbientTemp;

                this.txtTestMediumTemp.Text = testPackageManage.TestMediumTemp;
                this.txtVacuumTestService.Text = testPackageManage.VacuumTestService;
                this.txtVacuumTestPressure.Text = testPackageManage.VacuumTestPressure;

                this.txtTightnessTestTime.Text = testPackageManage.TightnessTestTime;
                this.txtTightnessTestTemp.Text = testPackageManage.TightnessTestTemp;
                this.txtTightnessTest.Text = testPackageManage.TightnessTest;

                this.txtTestPressure.Text = testPackageManage.TestPressure;
                this.txtTestPressureTemp.Text = testPackageManage.TestPressureTemp;
                this.txtTestPressureTime.Text = testPackageManage.TestPressureTime;

                this.txtOperationMedium.Text = testPackageManage.OperationMedium;
                this.txtPurgingMedium.Text = testPackageManage.PurgingMedium;
                this.txtCleaningMedium.Text = testPackageManage.CleaningMedium;

                this.txtLeakageTestService.Text = testPackageManage.LeakageTestService;
                this.txtLeakageTestPressure.Text = testPackageManage.LeakageTestPressure;
                this.txtAllowSeepage.Text = testPackageManage.AllowSeepage;
                this.txtFactSeepage.Text = testPackageManage.FactSeepage;
                this.txtModifyDate.Text = string.Format("{0:yyyy-MM-dd}", testPackageManage.ModifyDate);
                if (!string.IsNullOrEmpty(testPackageManage.Modifier))
                {
                    var users = BLL.UserService.GetUserByUserId(testPackageManage.Modifier);
                    if (users != null)
                    {
                        this.drpModifier.Text = users.UserName;
                    }
                }
                this.txtTableDate.Text = string.Format("{0:yyyy-MM-dd}", testPackageManage.TableDate);
                if (!string.IsNullOrEmpty(testPackageManage.Tabler))
                {
                    var users = BLL.UserService.GetUserByUserId(testPackageManage.Tabler);
                    if (users != null)
                    {
                        this.drpTabler.Text = users.UserName;
                    }
                }
                this.txtRemark.Text = testPackageManage.Remark;

                this.txtAduditDate.Text = string.Format("{0:yyyy-MM-dd}", testPackageManage.AduditDate);
                if (!string.IsNullOrEmpty(testPackageManage.Auditer))
                {
                    var users = BLL.UserService.GetUserByUserId(testPackageManage.Auditer);
                    if (users != null)
                    {
                        this.drpAuditer.Text = users.UserName;
                    }
                }

                if (string.IsNullOrEmpty(testPackageManage.Auditer) || !testPackageManage.AduditDate.HasValue)
                {
                    this.btnEdit.Hidden = false;
                    this.btnDelete.Hidden = false;
                    this.btnPrint.Hidden = false;
                }
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
            //this.drpUnit.Text = string.Empty;
            this.drpInstallation.Text = string.Empty;
            this.txtTestPackageName.Text = string.Empty;
            //this.txtTestPackageCode.Text = string.Empty;
            this.drpTestType.Text = string.Empty;
            this.txtTestService.Text = string.Empty;
            this.txtTestHeat.Text = string.Empty;
            this.txtTestAmbientTemp.Text = string.Empty;
            this.txtTestMediumTemp.Text = string.Empty;
            this.txtVacuumTestService.Text = string.Empty;
            this.txtVacuumTestPressure.Text = string.Empty;
            this.txtTightnessTestTime.Text = string.Empty;
            this.txtTightnessTestTemp.Text = string.Empty;
            this.txtTightnessTest.Text = string.Empty;
            this.txtTestPressure.Text = string.Empty;
            this.txtTestPressureTemp.Text = string.Empty;
            this.txtTestPressureTime.Text = string.Empty;
            this.txtOperationMedium.Text = string.Empty;
            this.txtPurgingMedium.Text = string.Empty;
            this.txtCleaningMedium.Text = string.Empty;
            this.txtLeakageTestService.Text = string.Empty;
            this.txtLeakageTestPressure.Text = string.Empty;
            this.txtAllowSeepage.Text = string.Empty;
            this.txtFactSeepage.Text = string.Empty;
            this.drpModifier.Text = string.Empty;
            this.txtModifyDate.Text = string.Empty;
            this.drpTabler.Text = string.Empty;
            this.txtTableDate.Text = string.Empty;
            this.txtRemark.Text = string.Empty;
            this.drpAuditer.Text = string.Empty;
            this.txtAduditDate.Text = string.Empty;
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
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.TestPackageEditMenuId, Const.BtnAdd))
            {
                this.SetTextTemp();
                string window = String.Format("TestPackageItemEdit.aspx?PTP_ID={0}", string.Empty, "新增 - ");
                PageContext.RegisterStartupScript(Window1.GetSaveStateReference(this.hdPTP_ID.ClientID)
                  + Window1.GetShowReference(window));
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        #region 编辑试压包
        /// <summary>
        /// 编辑试压包
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.TestPackageEditMenuId, Const.BtnModify))
            {
                var testPackageManage = BLL.TestPackageEditService.GetTestPackageByID(this.PTP_ID);
                if (testPackageManage != null)
                {
                    if (testPackageManage.AduditDate.HasValue)
                    {
                        Alert.ShowInTop("此试压单已审核！", MessageBoxIcon.Warning);
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
        #endregion

        #region 删除试压包
        /// <summary>
        /// 删除试压包
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(this.PTP_ID))
            //{
            //    List<SqlParameter> listStr = new List<SqlParameter>();
            //    listStr.Add(new SqlParameter("@PTP_ID", this.PTP_ID));
            //    SqlParameter[] parameter = listStr.ToArray();
            //    DataTable tb = BLL.SQLHelper.GetDataTableRunProc("HJGL_spPressureTestItemReport", parameter);
            //    string varValue = Funs.GetPagesCountByPageSize(4, 24, tb.Rows.Count).ToString();
            //    if (tb.Rows.Count <= 4)
            //    {
            //        PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("../../Common/ReportPrint/ExReportPrint.aspx?ispop=1&reportId={0}&replaceParameter={1}&varValue={2}&projectId={3}", BLL.Const.HJGL_TestPackageReport1Id, this.PTP_ID, varValue, this.CurrUser.LoginProjectId, "打印 - ")));
            //    }
            //    else
            //    {
            //        PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("../../Common/ReportPrint/ExReportPrint.aspx?ispop=1&reportId={0}&replaceParameter={1}&varValue={2}&projectId={3}", BLL.Const.HJGL_TestPackageReport2Id, this.PTP_ID, string.Empty, this.CurrUser.LoginProjectId, "打印 - ")));
            //        PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("../../Common/ReportPrint/ExReportPrint.aspx?ispop=1&reportId={0}&replaceParameter={1}&varValue={2}&projectId={3}", BLL.Const.HJGL_TestPackageReport1Id, this.PTP_ID, varValue, this.CurrUser.LoginProjectId, "打印 - ")));
            //    }
            //}
            //else
            //{
            //    ShowNotify("请选择试压包记录！", MessageBoxIcon.Warning);
            //    return;
            //}
        }

       
    }
}