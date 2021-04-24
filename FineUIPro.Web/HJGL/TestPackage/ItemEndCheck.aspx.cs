using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.HJGL.TestPackage
{
    public partial class ItemEndCheck : PageBase
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

        private bool AppendToEnd = false;
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
                this.PTP_ID = string.Empty;
                this.InitTreeMenu();//加载树
                // 删除选中单元格的客户端脚本
                string deleteScript = GetDeleteScript();
                // 新增数据初始值
                JObject defaultObj = new JObject();
                defaultObj.Add("Remark", "");
                defaultObj.Add("CheckMan", "");
                defaultObj.Add("CheckDate", "");
                defaultObj.Add("DealMan", "");
                defaultObj.Add("DealDate", "");
                defaultObj.Add("Delete", String.Format("<a href=\"javascript:;\" onclick=\"{0}\"><img src=\"{1}\"/></a>", deleteScript, IconHelper.GetResolvedIconUrl(Icon.Delete)));
                // 在第一行新增一条数据

                // Grid1.GetAddNewRecordReference(defaultObj, AppendToEnd);
                // 删除选中行按钮
                //btnDelete.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！") + deleteScript;
                // 绑定表格
                BindGrid();
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
                    tn2.ToolTip = "试压包存在未闭合尾项记录红色提醒，存在流程未闭合记录黄色提醒";
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
                               where x.AduditDate.HasValue
                               orderby x.TestPackageNo descending
                               select x;
                var totalItems = from x in Funs.DB.PTP_ItemEndCheck select x;
                var totalList = from x in Funs.DB.PTP_ItemEndCheckList select x;
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
                    var items = from x in totalItems
                                join y in totalList on x.ItemEndCheckListId equals y.ItemEndCheckListId
                                where y.PTP_ID == item.PTP_ID && (x.Result==null || x.Result=="不合格")
                                select x;
                    if (items.Count() > 0)   //存在未闭合尾项记录
                    {
                        newNode.Text = "<font color='#FF7575'>" + newNode.Text + "</font>";
                    }
                    else
                    {
                        var notCompletelist = from x in totalList
                                              where x.PTP_ID == item.PTP_ID && x.State != BLL.Const.TestPackage_Complete
                                              select x;
                        if (notCompletelist.Count() > 0)   //存在流程未闭合记录
                        {
                            newNode.Text = "<font color='#FFD700'>" + newNode.Text + "</font>";
                        }
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
            Model.PTP_TestPackage testPackage = BLL.TestPackageEditService.GetTestPackageByID(this.tvControlItem.SelectedNodeID);
            if (testPackage != null)
            {
                this.btnMenuNew.Hidden = false;
                this.BindGrid();
            }
            else
            {
                this.btnMenuNew.Hidden = true;
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"select ItemEndCheckListId,i.PTP_ID, TestPackageNo, TestPackageName, CompileMan, CompileDate, Remark, P.ProjectId, UnitWorkId,i.State,UserName from [dbo].[PTP_ItemEndCheckList] i left join [dbo].[PTP_TestPackage] p on i.PTP_ID=p.PTP_ID LEFT Join Sys_User U on i.CompileMan=U.UserId WHERE P.ProjectId =@ProjectId and i.PTP_ID=@PTP_ID order by CompileDate desc";
            SqlParameter[] parameter = new SqlParameter[]
                    {
                        new SqlParameter("@PTP_ID",this.PTP_ID),
                        new SqlParameter("@ProjectId",this.CurrUser.LoginProjectId),
                    };
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.DataSource = tb;
            Grid1.DataBind();
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                string rowID = Grid1.Rows[i].DataKeys[0].ToString();
                var items = BLL.AItemEndCheckService.GetItemEndCheckByItemEndCheckListId(rowID);
                var aItems = items.Where(x => x.ItemType == "A");
                var bItems = items.Where(x => x.ItemType == "B");
                var aOKItems = aItems.Where(x => x.Result == "合格");
                var bOKItems = bItems.Where(x => x.Result == "合格");
                if (aItems.Count() > 0)   //存在A项
                {
                    if (aItems.Count() == aOKItems.Count())   //A项完成
                    {
                        if (bItems.Count() > 0)   //存在B项
                        {
                            if (bItems.Count() == bOKItems.Count())   //B项完成
                            {
                                Grid1.Rows[i].RowCssClass = "Green";
                            }
                            else
                            {
                                Grid1.Rows[i].RowCssClass = "Yellow";
                            }
                        }
                        else
                        {
                            Grid1.Rows[i].RowCssClass = "Green";
                        }
                    }
                    else
                    {
                        Grid1.Rows[i].RowCssClass = "Red";
                    }
                }
                else
                {
                    if (bItems.Count() > 0)   //存在B项
                    {
                        if (bItems.Count() == bOKItems.Count())   //B项完成
                        {
                            Grid1.Rows[i].RowCssClass = "Green";
                        }
                        else
                        {
                            Grid1.Rows[i].RowCssClass = "Yellow";
                        }
                    }
                    else
                    {
                        Grid1.Rows[i].RowCssClass = "Green";
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetDeleteScript()
        {
            if (!CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.AItemEndCheckMenuId, Const.BtnDelete))
            {
                //ShowNotify("您没有这个权限，请与管理员联系！");
                return null;
            }
            else
            {
                return Confirm.GetShowReference("删除选中行？", String.Empty, MessageBoxIcon.Question, Grid1.GetDeleteSelectedRowsReference(), String.Empty);
            }
        }

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

        #region 刷新页面
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



        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }


        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            btnMenuModify_Click(null, null);
        }

        /// <summary>
        /// 增加尾项记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuNew_Click(object sender, EventArgs e)
        {
            if (this.tvControlItem.SelectedNode.CommandName == "TestPackage")
            {
                if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.AItemEndCheckMenuId, Const.BtnAdd))
                {
                    string url = "ItemEndCheckEdit.aspx?PTP_ID={0}";
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format(url, tvControlItem.SelectedNodeID, "操作 - ")));
                }
                else
                {
                    ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("非试压包节点无法新增！", MessageBoxIcon.Warning);
            }
        }

        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.AItemEndCheckMenuId, Const.BtnModify))
            {
                if (Grid1.SelectedRowIndexArray.Length == 0)
                {
                    Alert.ShowInTop("请至少选择一条记录", MessageBoxIcon.Warning);
                    return;
                }
                string url = "ItemEndCheckView.aspx?PTP_ID={0}&ItemEndCheckListId={1}";
                var TestPackageApprove = TestPackageApproveService.GetTestPackageApproveById(Grid1.SelectedRowID);
                var ItemEndCheckList = BLL.ItemEndCheckListService.GetItemEndCheckListByID(Grid1.SelectedRowID);

                if (ItemEndCheckList.State == BLL.Const.TestPackage_Compile)
                {
                    url = "ItemEndCheckEdit.aspx?PTP_ID={0}&ItemEndCheckListId={1}";
                }
                else
                {
                    if (TestPackageApprove != null)
                    {
                        if (!string.IsNullOrEmpty(TestPackageApprove.ApproveMan))
                        {
                            if (this.CurrUser.UserId == TestPackageApprove.ApproveMan || this.CurrUser.UserId == BLL.Const.sysglyId || this.CurrUser.UserId == BLL.Const.hfnbdId)
                            {
                                url = "ItemEndCheckEdit2.aspx?PTP_ID={0}&ItemEndCheckListId={1}";
                            }
                            else
                            {
                                Alert.ShowInTop("您不是办理人，无法办理！", MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                }
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format(url, tvControlItem.SelectedNodeID, Grid1.SelectedRowID, "操作 - ")));
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！");
            }
        }
        //<summary>
        //获取办理人姓名
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        protected string ConvertMan(object ItemEndCheckListId)
        {
            if (ItemEndCheckListId != null)
            {
                var approve = BLL.TestPackageApproveService.GetTestPackageApproveById(ItemEndCheckListId.ToString());
                if (approve != null)
                {
                    if (approve.ApproveMan != null)
                    {
                        return BLL.UserService.GetUserByUserId(approve.ApproveMan).UserName;
                    }
                }
                else
                {
                    return "";
                }
            }
            return "";
        }

        /// <summary>
        /// 获取A项整改状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertAState(object ItemEndCheckListId)
        {
            string rowID = ItemEndCheckListId.ToString();
            var items = BLL.AItemEndCheckService.GetItemEndCheckByItemEndCheckListId(rowID);
            var aItems = items.Where(x => x.ItemType == "A");
            var aOKItems = aItems.Where(x => x.Result == "合格");
            if (aItems.Count() > 0)   //存在A项
            {
                if (aItems.Count() == aOKItems.Count())   //A项完成
                {
                    return "已完成";
                }
                else
                {
                    return "未完成";
                }
            }
            else
            {
                return "无";
            }
        }

        /// <summary>
        /// 获取B项整改状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertBState(object ItemEndCheckListId)
        {
            string rowID = ItemEndCheckListId.ToString();
            var items = BLL.AItemEndCheckService.GetItemEndCheckByItemEndCheckListId(rowID);
            var bItems = items.Where(x => x.ItemType == "B");
            var bOKItems = bItems.Where(x => x.Result == "合格");
            if (bItems.Count() > 0)   //存在B项
            {
                if (bItems.Count() == bOKItems.Count())   //B项完成
                {
                    return "已完成";
                }
                else
                {
                    return "未完成";
                }
            }
            else
            {
                return "无";
            }
        }

        /// <summary>
        /// 把状态转换代号为文字形式
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertState(object Type)
        {
            if (Type != null)
            {
                if (Type.ToString() == BLL.Const.TestPackage_Compile)
                {
                    return "总包专业工程师编制";
                }
                else if (Type.ToString() == Const.TestPackage_Audit1)
                {

                    return "施工分包商整改";
                }
                else if (Type.ToString() == Const.TestPackage_Audit2)
                {

                    return "总包确认";
                }
                else if (Type.ToString() == Const.TestPackage_Audit3)
                {
                    return "监理确认";
                }
                else if (Type.ToString() == Const.TestPackage_ReAudit2)
                {
                    return "施工分包商继续整改";
                }
                else if (Type.ToString() == Const.TestPackage_Complete)
                {
                    return "审批完成";
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        protected void btnMenuView_Click(object sender, EventArgs e)
        {

            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ItemEndCheckView.aspx?PTP_ID={0}&ItemEndCheckListId={1}", tvControlItem.SelectedNodeID, Grid1.SelectedRowID, "操作 - ")));
        }

        protected void btnMenuDel_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.AItemEndCheckMenuId, Const.BtnDelete))
            {
                string rowID = Grid1.SelectedRowID;
                if (!string.IsNullOrEmpty(rowID))
                {
                    BLL.TestPackageApproveService.DeleteAllTestPackageApproveByID(rowID);
                    BLL.AItemEndCheckService.DeleteAllItemEndCheckByID(rowID);
                    BLL.ItemEndCheckListService.DeleteItemEndCheckList(rowID);
                    //Model.PTP_TestPackage testPackage = BLL.TestPackageEditService.GetTestPackageByID(rowID);
                    //if (testPackage != null)
                    //{
                    //    testPackage.State = null;
                    //    BLL.TestPackageEditService.UpdateTestPackage(testPackage);
                    //}
                    BindGrid();
                }
                else
                {
                    ShowNotify("请选中删除行！");
                }
                Alert.ShowInTop("删除成功！", MessageBoxIcon.Success);
                BindGrid();
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！");
            }
        }
    }
}