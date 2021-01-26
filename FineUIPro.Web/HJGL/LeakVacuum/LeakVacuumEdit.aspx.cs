using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace FineUIPro.Web.HJGL.LeakVacuum
{
    public partial class LeakVacuumEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string LeakVacuumId
        {
            get
            {
                return (string)ViewState["LeakVacuumId"];
            }
            set
            {
                ViewState["LeakVacuumId"] = value;
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
                this.LeakVacuumId = string.Empty;
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
            rootNode2.EnableClickEvent = true;
            rootNode2.Expanded = true;
            this.tvControlItem.Nodes.Add(rootNode2);
            var pUnits = (from x in Funs.DB.Project_ProjectUnit where x.ProjectId == this.CurrUser.LoginProjectId select x).ToList();
            // 获取当前用户所在单位
            var currUnit = pUnits.FirstOrDefault(x => x.UnitId == this.CurrUser.UnitId);

            var unitWorkList = (from x in Funs.DB.WBS_UnitWork
                                where x.ProjectId == this.CurrUser.LoginProjectId
                                      && x.SuperUnitWork == null && x.UnitId != null && x.ProjectType != null
                                select x).ToList();
            List<Model.HJGL_LV_LeakVacuum> LeakVacuumLists = (from x in Funs.DB.HJGL_LV_LeakVacuum where x.ProjectId == this.CurrUser.LoginProjectId  select x).ToList();
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
                    var LeakVacuumUnitList = LeakVacuumLists.Where(x => x.UnitWorkId == q.UnitWorkId).ToList();
                    BindNodes(tn1, LeakVacuumUnitList);
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
                    var LeakVacuumUnitList = LeakVacuumLists.Where(x => x.UnitWorkId == q.UnitWorkId).ToList();
                    BindNodes(tn2, LeakVacuumUnitList);
                }
            }
        }
        #endregion

        #region 绑定树节点
        /// <summary>
        ///  绑定树节点
        /// </summary>
        /// <param name="node"></param>
        private void BindNodes(TreeNode node, List<Model.HJGL_LV_LeakVacuum> LeakVacuumUnitList)
        {
            if (node.CommandName == "单位工程")
            {
                var dReports = from x in LeakVacuumUnitList
                               where x.UnitWorkId == node.NodeID
                               orderby x.SysNo descending
                               select x;
                foreach (var item in dReports)
                {
                    TreeNode newNode = new TreeNode();
                    if (!string.IsNullOrEmpty(item.SysNo))
                    {
                        newNode.Text = item.SysNo;
                    }
                    else
                    {
                        newNode.Text = "未知";
                    }
                    //if (!string.IsNullOrEmpty(item.AduditDate) || string.IsNullOrEmpty(item.Auditer))
                    //{
                    //    newNode.Text = "<font color='#FF7575'>" + newNode.Text + "</font>";
                    //    node.Text = "<font color='#FF7575'>" + node.Text + "</font>";
                    //}
                    newNode.NodeID = item.LeakVacuumId;
                    newNode.CommandName = "LeakVacuum";
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
            this.LeakVacuumId = tvControlItem.SelectedNodeID;
            Model.WBS_UnitWork unitWork = BLL.UnitWorkService.getUnitWorkByUnitWorkId(this.tvControlItem.SelectedNodeID);
            Model.HJGL_LV_LeakVacuum leakVacuum = BLL.LeakVacuumEditService.GetLeakVacuumByID(this.tvControlItem.SelectedNodeID);
            if (unitWork != null)
            {
                this.btnMenuNew.Hidden = false;
                this.btnMenuModify.Hidden = true;
                this.btnMenuDel.Hidden = true;
                this.btnMenuPrint.Hidden = true;
            }
            else if (leakVacuum != null)
            {
                this.btnMenuNew.Hidden = true;
                this.btnMenuModify.Hidden = false;
                this.btnMenuDel.Hidden = false;
                this.btnMenuPrint.Hidden = false;
            }
            else
            {
                this.btnMenuNew.Hidden = true;
                this.btnMenuModify.Hidden = true;
                this.btnMenuDel.Hidden = true;
                this.btnMenuPrint.Hidden = true;
            }
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
            string strSql = @"SELECT  lv.LeakVacuumId,lv.LV_PipeId, IsoInfo.PipelineCode,IsoInfo.DesignPress,IsoInfo.DesignTemperature,
                               IsoInfo.TestPressure,lv.AmbientTemperature,lv.TestMediumTemperature,lv.LeakPressure,lv.LeakMedium,
                               lea.MediumName as LeaMediumName,vac.MediumName as VacuumMediumName,lv.VacuumPressure,lv.VacuumMedium  
                               FROM dbo.HJGL_LV_Pipeline AS lv
                               LEFT JOIN dbo.HJGL_Pipeline AS IsoInfo ON  IsoInfo.PipelineId = lv.PipelineId
							   LEFT JOIN dbo.Base_TestMedium  AS lea ON lea.TestMediumId = lv.LeakMedium
                               LEFT JOIN dbo.Base_TestMedium  AS vac ON vac.TestMediumId = lv.VacuumMedium
                               WHERE lv.LeakVacuumId=@LeakVacuumId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            listStr.Add(new SqlParameter("@LeakVacuumId", this.LeakVacuumId));
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
            var leakVacuunManage = BLL.LeakVacuumEditService.GetLeakVacuumByID(this.LeakVacuumId);
            if (leakVacuunManage != null)
            {
                this.txtSysNo.Text = leakVacuunManage.SysNo;
                this.txtSysName.Text = leakVacuunManage.SysName;
                this.txtTableDate.Text = string.Format("{0:yyyy-MM-dd}", leakVacuunManage.TableDate);
                this.txtRemark.Text = leakVacuunManage.Remark;
                this.txtAduditDate.Text = string.Format("{0:yyyy-MM-dd}", leakVacuunManage.AduditDate);
                this.txtFinishDef.Text = leakVacuunManage.FinishDef;
                txtCheck1.Text = leakVacuunManage.Check1;
                txtCheck2.Text = leakVacuunManage.Check2;
                txtCheck3.Text = leakVacuunManage.Check3;
                txtCheck4.Text = leakVacuunManage.Check4;
                txtCheck5.Text = leakVacuunManage.Check5;
            }
        }
        #endregion

        #region 清空页面输入信息
        /// <summary>
        /// 清空页面输入信息
        /// </summary>
        private void SetTextTemp()
        {
            this.txtSysNo.Text = string.Empty;
            this.txtSysName.Text = string.Empty;
            this.txtTableDate.Text = string.Empty;
            this.txtRemark.Text = string.Empty;
            this.txtFinishDef.Text = string.Empty;
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

        #region 维护事件
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuNew_Click(object sender, EventArgs e)
        {
            if (this.tvControlItem.SelectedNode.CommandName == "单位工程") {
                if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.LeakVacuumEditMenuId, Const.BtnAdd))
                {
                    if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.CommandName == "单位工程")
                    {
                        this.SetTextTemp();
                        string window = String.Format("LeakVacuumItemEdit.aspx?unitWorkId={0}", this.tvControlItem.SelectedNodeID, "新增 - ");
                        PageContext.RegisterStartupScript(Window1.GetSaveStateReference(this.hdLeakVacuumId.ClientID)
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
            if (this.tvControlItem.SelectedNode.CommandName == "LeakVacuum") {
                if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.LeakVacuumEditMenuId, Const.BtnModify))
                {
                    var leakVacuunManage = BLL.LeakVacuumEditService.GetLeakVacuumByID(this.LeakVacuumId);
                    if (leakVacuunManage != null)
                    {
                        if (!string.IsNullOrEmpty(leakVacuunManage.AduditDate))
                        {
                            Alert.ShowInTop("此泄露性/真空试验单已审核！", MessageBoxIcon.Warning);
                            return;
                        }

                        string window = String.Format("LeakVacuumItemEdit.aspx?LeakVacuumId={0}", this.LeakVacuumId, "编辑 - ");
                        PageContext.RegisterStartupScript(Window1.GetSaveStateReference(this.hdLeakVacuumId.ClientID)
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
                ShowNotify("非泄露性/真空试验类型无法编辑！", MessageBoxIcon.Warning);
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
            if (this.tvControlItem.SelectedNode.CommandName == "LeakVacuum")
            {
                if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.LeakVacuumEditMenuId, Const.BtnDelete))
                {
                    var LeakVacuumManage = BLL.LeakVacuumEditService.GetLeakVacuumByID(this.LeakVacuumId);
                    if (LeakVacuumManage != null)
                    {
                        if (!string.IsNullOrEmpty(LeakVacuumManage.AduditDate))
                        {
                            Alert.ShowInTop("此试压单已审核！", MessageBoxIcon.Warning);
                            return;
                        }

                        BLL.LeakVacuumEditService.DeletePipelineListByLeakVacuumId(this.LeakVacuumId);
                        BLL.LeakVacuumEditService.DeleteLeakVacuum(this.LeakVacuumId);
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
                ShowNotify("非泄露性/真空试验类型无法删除！", MessageBoxIcon.Warning);
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
            this.LeakVacuumId = this.hdLeakVacuumId.Text;
            this.BindGrid();
            this.InitTreeMenu();
            this.hdLeakVacuumId.Text = string.Empty;
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

        protected void btnMenuPrint_Click(object sender, EventArgs e)
        {
            string LeakVacuumId = this.tvControlItem.SelectedNodeID;
            var p = BLL.LeakVacuumEditService.GetLeakVacuumByID(LeakVacuumId);
            if (p != null)
            {
                string varValue = string.Empty;
                var project = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId);
                if (project != null)
                {
                    varValue = project.ProjectName;
                    var unitWork = BLL.UnitWorkService.GetUnitWorkByUnitWorkId(p.UnitWorkId);
                    if (unitWork != null)
                    {
                        varValue = varValue + "|" + unitWork.UnitWorkName;
                    }
                    varValue = varValue + "|" + p.SysNo;
                    varValue = varValue + "|" + p.SysName;
                    varValue = varValue + "|" + p.Check1;
                    varValue = varValue + "|" + p.Check2;
                    varValue = varValue + "|" + p.Check3;
                    varValue = varValue + "|" + p.Check4;
                    varValue = varValue + "|" + p.Check5;
                    if (!string.IsNullOrEmpty(p.FinishDef))
                    {
                        varValue = varValue + "|" + p.FinishDef;
                    }
                }
                if (!string.IsNullOrEmpty(varValue))
                {
                    varValue = HttpUtility.UrlEncodeUnicode(varValue);
                }
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("../../ReportPrint/ExReportPrint.aspx?ispop=1&reportId={0}&replaceParameter={1}&varValue={2}&projectId={3}", BLL.Const.HJGL_LeakVacuumReportId, LeakVacuumId, varValue, this.CurrUser.LoginProjectId)));
            }
            else
            {
                ShowNotify("请选择吹扫/清洗试验！", MessageBoxIcon.Warning);
                return;
            }
        }
    }
}