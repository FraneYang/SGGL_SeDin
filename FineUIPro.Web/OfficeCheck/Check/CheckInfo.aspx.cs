using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.OfficeCheck.Check
{
    public partial class CheckInfo : PageBase
    {
        #region 定义项
        /// <summary>
        /// 检查主键
        /// </summary>
        public string CheckNoticeId
        {
            get
            {
                return (string)ViewState["CheckNoticeId"];
            }
            set
            {
                ViewState["CheckNoticeId"] = value;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ////权限按钮方法
                this.GetButtonPower();
                this.InitTreeMenu();
                this.CheckNoticeId = string.Empty;
            }
        }

        #region 加载树
        /// <summary>
        /// 加载树
        /// </summary>
        private void InitTreeMenu()
        {
            this.tvControlItem.Nodes.Clear();
            this.tvControlItem.ShowBorder = false;
            this.tvControlItem.ShowHeader = false;
            this.tvControlItem.EnableIcons = true;
            this.tvControlItem.AutoScroll = true;
            this.tvControlItem.EnableSingleClickExpand = true;
            TreeNode rootNode = new TreeNode
            {
                Text = "年月",
                NodeID = "0",
                ToolTip = "年份",
                Expanded = true
            };
            this.tvControlItem.Nodes.Add(rootNode);
            var checkInfoLists = BLL.CheckNoticeService.GetCheckInfoList(this.CurrUser.UnitId, this.CurrUser.UserId, this.CurrUser.RoleId);
            if (!string.IsNullOrEmpty(this.txtCheckStartTimeS.Text))
            {
                checkInfoLists = checkInfoLists.Where(x => x.CheckStartTime >= Funs.GetNewDateTime(this.txtCheckStartTimeS.Text)).ToList();
            }
            if (!string.IsNullOrEmpty(this.txtCheckEndTimeS.Text))
            {
                checkInfoLists = checkInfoLists.Where(x => x.CheckEndTime <= Funs.GetNewDateTime(this.txtCheckEndTimeS.Text)).ToList();
            }
            this.BindNodes(rootNode, checkInfoLists);
        }
        #endregion

        #region 绑定树节点
        /// <summary>
        ///  绑定树节点
        /// </summary>
        /// <param name="node"></param>
        private void BindNodes(TreeNode node, List<Model.ProjectSupervision_CheckNotice> checkInfoList)
        {
            if (node.ToolTip == "年份")
            {
                var pointListMonth = (from x in checkInfoList
                                      orderby x.CheckStartTime descending
                                      select string.Format("{0:yyyy-MM}", x.CheckStartTime)).Distinct();
                foreach (var item in pointListMonth)
                {
                    TreeNode newNode = new TreeNode
                    {
                        Text = item,
                        NodeID = item + "|" + node.NodeID,
                        ToolTip = "月份"
                    };
                    node.Nodes.Add(newNode);
                    this.BindNodes(newNode, checkInfoList);
                }
            }
            else if (node.ToolTip == "月份")
            {
                var dReports = from x in checkInfoList
                               where string.Format("{0:yyyy-MM}", x.CheckStartTime) == node.Text
                               orderby x.CheckStartTime descending
                               select x;
                foreach (var item in dReports)
                {
                    TreeNode newNode = new TreeNode();
                    var units = BLL.UnitService.GetUnitByUnitId(item.SubjectUnitId);
                    if (units != null)
                    {
                        newNode.Text = (item.CheckStartTime.Day).ToString().PadLeft(2, '0') + "日：" + units.UnitName;
                    }
                    else
                    {
                        newNode.Text = (item.CheckStartTime.Day).ToString().PadLeft(2, '0') + "日：未知单位";
                    }
                    newNode.NodeID = item.CheckNoticeId;
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
            this.CheckNoticeId = this.tvControlItem.SelectedNodeID;
            PageInfoLoad();
        }
        #endregion

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

        #region 编辑监督检查
        /// <summary>
        /// 编辑监督检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string window = String.Format("CheckNoticeEdit.aspx?CheckNoticeId={0}", this.CheckNoticeId, "编辑 - ");
            PageContext.RegisterStartupScript(Window1.GetShowReference(window));
        }
        #endregion        

        #region 删除监督检查
        /// <summary>
        /// 删除监督检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //BLL.CheckInfo_Table1Service.DeleteCheckInfo_Table1ByCheckInfo_Table1Id(this.CheckInfoId);
            //BLL.CheckInfo_Table2Service.DeleteCheckInfo_Table2ByCheckInfo_Table2Id(this.CheckInfoId);
            //BLL.CheckInfo_Table3Service.DeleteCheckInfo_Table3ByCheckInfo_Table3Id(this.CheckInfoId);
            //BLL.CheckInfo_Table4Service.DeleteCheckInfo_Table4ByCheckInfo_Table4Id(this.CheckInfoId);
            //BLL.CheckInfo_Table5Service.DeleteCheckInfo_Table5ByCheckInfo_Table5Id(this.CheckInfoId);
            //BLL.CheckInfo_Table6Service.DeleteCheckInfo_Table6ByCheckInfo_Table6Id(this.CheckInfoId);
            //BLL.CheckInfo_Table7Service.DeleteCheckInfo_Table7ByCheckInfo_Table7Id(this.CheckInfoId);
            //BLL.CheckInfo_Table8Service.DeleteCheckInfo_Table8ByCheckInfo_Table8Id(this.CheckInfoId);
            BLL.CheckNoticeService.DeleteCheckNoticeByCheckNoticeId(this.CheckNoticeId);
            //BLL.LogService.AddLog(this.CurrUser.UserId, "删除监督检查");
            ShowNotify("删除成功！", MessageBoxIcon.Success);
            this.InitTreeMenu();
            //this.BindGrid();
        }
        #endregion

        #region 加载页面输入保存信息
        /// <summary>
        /// 加载页面输入保存信息
        /// </summary>
        private void PageInfoLoad()
        {
            var checkInfo = BLL.CheckNoticeService.GetCheckNoticeById(this.CheckNoticeId);
            if (checkInfo != null)
            {
                this.GetButtonPower();
                this.txtCheckStartTime.Text = string.Format("{0:yyyy-MM-dd}", checkInfo.CheckStartTime);
                this.txtCheckEndTime.Text = string.Format("{0:yyyy-MM-dd}", checkInfo.CheckEndTime);
                var unit = BLL.UnitService.GetUnitByUnitId(checkInfo.SubjectUnitId);
                if (unit != null)
                {
                    this.drpSubjectUnit.Text = unit.UnitName;
                }
                this.txtSubjectUnitMan.Text = checkInfo.SubjectUnitMan;
                this.txtSubjectUnitAdd.Text = checkInfo.SubjectUnitAdd;
                this.txtSubjectUnitTel.Text = checkInfo.SubjectUnitTel;
                this.txtSubjectObject.Text = checkInfo.SubjectObject;
                this.txtCompileMan.Text = BLL.UserService.GetUserNameByUserId(checkInfo.CompileMan);
                this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", checkInfo.CompileDate);

                //现场安全检查
                var checkTable = BLL.CheckTable1Service.GetCheckTable1ByCheckNoticeId(this.CheckNoticeId);
                if (checkTable != null)
                {
                    this.lblSubjectUnitId.Text = "企业名称：" + BLL.UnitService.GetUnitNameByUnitId(checkTable.SubjectUnitId);
                    this.lblCheckDate.Text = string.Format("{0:yyyy-MM-dd}", checkTable.CheckDate);
                    this.lblResult.Text = "评定得分：" + checkTable.TotalLastScore + "；评定结果：" + checkTable.EvaluationResult;
                }
                //检查报告
                var checkReport = BLL.CheckReportService.GetCheckReportByCheckNoticeId(this.CheckNoticeId);
                if (checkReport != null)
                {
                    this.lblCheckObject.Text = "受检单位：" + BLL.UnitService.GetUnitNameByUnitId(checkInfo.SubjectUnitId);
                    this.lblCheckStartTime.Text = string.Format("{0:yyyy-MM-dd}", checkInfo.CheckStartTime);
                    this.lblCheckReportResult.Text = checkReport.CheckResult;
                }
                //隐患整改
                var rectify = BLL.ProjectSupervision_RectifyService.GetRectifyByCheckNoticeId(this.CheckNoticeId);
                if (rectify != null)
                {
                    this.lblUnitId.Text = "受检单位：" + BLL.UnitService.GetUnitNameByUnitId(rectify.UnitId);
                    this.lblCheckedDate.Text = string.Format("{0:yyyy-MM-dd}", rectify.CheckedDate);
                    var item = BLL.ProjectSupervision_RectifyItemService.GetRectifyItemByRectifyId(rectify.RectifyId);
                    if (item != null)
                    {
                        this.lblCheckResult.Text = "隐患：" + item.Count() + "条";
                    }
                }
            }
            else
            {
                this.drpSubjectUnit.Text = string.Empty;
                this.txtSubjectObject.Text = string.Empty;
                this.txtSubjectUnitMan.Text = string.Empty;
                this.txtSubjectUnitTel.Text = string.Empty;
                this.txtSubjectUnitAdd.Text = string.Empty;
                this.txtCheckStartTime.Text = string.Empty;
                this.txtCheckEndTime.Text = string.Empty;
                this.txtCompileMan.Text = string.Empty;
                this.txtCompileDate.Text = string.Empty;
                this.CheckNoticeId = string.Empty;
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
            PageInfoLoad();
        }
        #endregion

        #region 按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.CheckInfoMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnModify) && !string.IsNullOrEmpty(this.CheckNoticeId))
                {
                    this.btnEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete) && !string.IsNullOrEmpty(this.CheckNoticeId))
                {
                    this.btnDelete.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnSave) && !string.IsNullOrEmpty(this.CheckNoticeId))
                {
                    this.btnCheck1.Hidden = false;
                    this.btnView1.Hidden = false;
                    this.btnCheck2.Hidden = false;
                    this.btnView2.Hidden = false;
                    this.btnCheck3.Hidden = false;
                    this.btnView3.Hidden = false;
                }
            }
        }
        #endregion

        #region 现场安全检查维护
        /// <summary>
        /// 编辑现场安全检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCheck1_Click(object sender, EventArgs e)
        {
            string window = String.Format("CheckContentEdit1.aspx?CheckNoticeId={0}", this.CheckNoticeId, "编辑 - ");
            PageContext.RegisterStartupScript(Window2.GetShowReference(window));
        }

        /// <summary>
        /// 查看现场安全检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnView1_Click(object sender, EventArgs e)
        {
            string window = String.Format("CheckContentEdit1.aspx?CheckNoticeId={0}&type=1", this.CheckNoticeId, "编辑 - ");
            PageContext.RegisterStartupScript(Window2.GetShowReference(window));
        }
        #endregion

        #region 隐患整改维护
        /// <summary>
        /// 编辑隐患整改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCheck2_Click(object sender, EventArgs e)
        {
            string window = String.Format("RectifyEdit.aspx?CheckNoticeId={0}", this.CheckNoticeId, "编辑 - ");
            PageContext.RegisterStartupScript(Window2.GetShowReference(window));
        }

        /// <summary>
        /// 查看隐患整改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnView2_Click(object sender, EventArgs e)
        {
            string window = String.Format("RectifyEdit.aspx?CheckNoticeId={0}&type=1", this.CheckNoticeId, "编辑 - ");
            PageContext.RegisterStartupScript(Window2.GetShowReference(window));
        }
        #endregion

        #region 检查报告维护
        /// <summary>
        /// 编辑检查报告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCheck3_Click(object sender, EventArgs e)
        {
            string window = String.Format("CheckReport.aspx?CheckNoticeId={0}", this.CheckNoticeId, "编辑 - ");
            PageContext.RegisterStartupScript(Window2.GetShowReference(window));
        }

        /// <summary>
        /// 查看检查报告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnView3_Click(object sender, EventArgs e)
        {
            string window = String.Format("CheckReport.aspx?CheckNoticeId={0}&type=1", this.CheckNoticeId, "编辑 - ");
            PageContext.RegisterStartupScript(Window2.GetShowReference(window));
        }
        #endregion
    }
}