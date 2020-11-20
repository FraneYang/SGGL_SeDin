using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.OfficeCheck.Check
{
    public partial class CheckNotice : PageBase
    {
        #region 定义项
        /// <summary>
        /// 检查通知主键
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
                ////权限按钮方法
                this.GetButtonPower();
                this.InitTreeMenu();
                this.CheckNoticeId = string.Empty;
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
        private void BindNodes(TreeNode node, List<Model.ProjectSupervision_CheckNotice> checkNoticeList)
        {
            if (node.ToolTip == "年份")
            {
                var pointListMonth = (from x in checkNoticeList
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
                    this.BindNodes(newNode, checkNoticeList);
                }
            }
            else if (node.ToolTip == "月份")
            {
                var dReports = from x in checkNoticeList
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
            this.GetButtonPower();
            //this.txtCheckType.Text = this.tvControlItem.SelectedNode.ParentNode.ParentNode.Text;
            this.PageInfoLoad(); ///页面输入保存信息
            //this.BindGrid1();
            this.BindGrid2();
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
                this.txtCheckStartTime.Text = string.Format("{0:yyyy-MM-dd}", checkInfo.CheckStartTime);
                this.txtCheckEndTime.Text = string.Format("{0:yyyy-MM-dd}", checkInfo.CheckEndTime);
                this.drpSubjectUnit.Text = BLL.UnitService.GetUnitNameByUnitId(checkInfo.SubjectUnitId);               
                this.txtSubjectUnitMan.Text = checkInfo.SubjectUnitMan;
                this.txtSubjectUnitAdd.Text = checkInfo.SubjectUnitAdd;
                this.txtSubjectUnitTel.Text = checkInfo.SubjectUnitTel;
                this.txtSubjectObject.Text = checkInfo.SubjectObject;
                this.txtCheckTeamLeader.Text = BLL.UserService.GetUserNameByUserId(checkInfo.CheckTeamLeader);
                this.txtCompileMan.Text = BLL.UserService.GetUserNameByUserId(checkInfo.CompileMan);
                this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", checkInfo.CompileDate);
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
                this.txtCheckTeamLeader.Text = string.Empty;
                this.txtCompileMan.Text = string.Empty;
                this.txtCompileDate.Text = string.Empty;
                this.CheckNoticeId = string.Empty;
            }
        }
        #endregion

        #region 页面维护
        /// <summary>
        /// 增加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            string window = String.Format("CheckNoticeEdit.aspx?CheckNoticeId={0}", string.Empty, "新增 - ");
            PageContext.RegisterStartupScript(Window1.GetSaveStateReference(this.hdCheckNoticeId.ClientID)
              + Window1.GetShowReference(window));
        }

        /// <summary>
        /// 编辑监督检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string window = String.Format("CheckNoticeEdit.aspx?CheckNoticeId={0}", this.CheckNoticeId, "编辑 - ");
            PageContext.RegisterStartupScript(Window1.GetSaveStateReference(this.hdCheckNoticeId.ClientID)
              + Window1.GetShowReference(window));
        }

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
            //BLL.CheckInfoService.DeleteCheckInfoByCheckInfoId(this.CheckInfoId);
            BLL.CheckNoticeService.DeleteCheckNoticeByCheckNoticeId(this.CheckNoticeId);
            ShowNotify("删除成功！", MessageBoxIcon.Success);
            this.PageInfoLoad();
            this.InitTreeMenu();
            //this.BindGrid1();
            this.BindGrid2();
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid2()
        {
            string strSql = @"SELECT CheckTeam.CheckTeamId,CheckTeam.CheckNoticeId,CheckTeam.UserId,CheckTeam.SortIndex,CheckTeam.PostName,CheckTeam.WorkTitle,CheckTeam.CheckPostName,CheckTeam.CheckDate"
                + @" ,CheckTeam.UserName,CheckTeam.SexName,Unit.UnitName,Unit.UnitName "
                + @" FROM dbo.ProjectSupervision_CheckTeam AS CheckTeam "
                + @" LEFT JOIN Base_Unit AS Unit ON CheckTeam.UnitId = Unit.UnitId "
                + @" WHERE 1=1  ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND CheckNoticeId = @CheckNoticeId";
            listStr.Add(new SqlParameter("@CheckNoticeId", this.CheckNoticeId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid2.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid2.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid2, tb);
            Grid2.DataSource = table;
            Grid2.DataBind();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid2_Sort(object sender, GridSortEventArgs e)
        {
            BindGrid2();
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
            //this.BindGrid1();
            this.BindGrid2();
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
            this.CheckNoticeId = this.hdCheckNoticeId.Text;
            this.InitTreeMenu();
            this.PageInfoLoad(); ///页面输入保存信息
            //this.BindGrid1();
            this.BindGrid2();
            this.hdCheckNoticeId.Text = string.Empty;
        }

        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void WindowTeam_Close(object sender, WindowCloseEventArgs e)
        {
            this.BindGrid2();
        }
        #endregion

        #region 检查工作组 右键事件
        /// <summary>
        /// 增加监督检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCheckTeamAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.CheckNoticeId))
            {
                PageContext.RegisterStartupScript(WindowTeam.GetShowReference(String.Format("CheckTeamEdit.aspx?CheckNoticeId={0}", this.CheckNoticeId, "增加 - ")));
            }
            else
            {
                ShowNotify("请先保存受检单位信息", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 右键编辑明细事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCheckTeamEdit_Click(object sender, EventArgs e)
        {
            this.EditData2();
        }

        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid2_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData2();
        }

        /// <summary>
        /// 编辑事件
        /// </summary>
        private void EditData2()
        {
            if (Grid2.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请选择一条记录！", MessageBoxIcon.Warning);
                return;
            }

            string checkTeamId = Grid2.SelectedRowID;
            PageContext.RegisterStartupScript(WindowTeam.GetShowReference(String.Format("CheckTeamEdit.aspx?CheckTeamId={0}", checkTeamId, "维护 - ")));
        }

        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCheckTeamDelete_Click(object sender, EventArgs e)
        {
            if (Grid2.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid2.SelectedRowIndexArray)
                {
                    string rowID = Grid2.DataKeys[rowIndex][0].ToString();
                    BLL.CheckTeamService.DeleteCheckTeamByCheckTeamId(rowID);
                }
                BindGrid2();
                ShowNotify("删除数据成功!（表格数据已重新绑定）", MessageBoxIcon.Success);
            }
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.CheckNoticeMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                    if (!string.IsNullOrEmpty(this.CheckNoticeId))
                    {
                        //this.btnCheckFileAdd.Hidden = false;
                        this.btnCheckTeamAdd.Hidden = false;
                    }
                }
                if (buttonList.Contains(BLL.Const.BtnModify) && !string.IsNullOrEmpty(this.CheckNoticeId))
                {
                    this.btnEdit.Hidden = false;
                    //this.btnCheckFileEdit.Hidden = false;
                    this.btnCheckTeamEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete) && !string.IsNullOrEmpty(this.CheckNoticeId))
                {
                    this.btnDelete.Hidden = false;
                    //this.btnCheckFileDelete.Hidden = false;
                    this.btnCheckTeamDelete.Hidden = false;
                }
            }
        }
        #endregion
    }
}