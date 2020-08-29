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
        /// 单位工程主键
        /// </summary>
        public string UnitWorkId
        {
            get
            {
                return (string)ViewState["UnitWorkId"];
            }
            set
            {
                ViewState["UnitWorkId"] = value;
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
                this.UnitWorkId = string.Empty;
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
                    tn1.EnableClickEvent = true;
                    rootNode1.Nodes.Add(tn1);
                    var testPackageUnitList = testPackageLists.Where(x => x.UnitWorkId == q.UnitWorkId).ToList();
                    //BindNodes(tn1, testPackageUnitList);
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
                    //BindNodes(tn2, testPackageUnitList);
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
            this.UnitWorkId = tvControlItem.SelectedNodeID;
            this.BindGrid();
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"select PTP_ID, TestPackageNo, TestPackageName, Tabler, TableDate, Remark, P.ProjectId, UnitWorkId,State,UserName from [dbo].[PTP_TestPackage] p LEFT Join Sys_User U on P.Tabler=U.UserId WHERE Check1 is not null And P.ProjectId =@ProjectId and UnitWorkId=@UnitWorkId";
            SqlParameter[] parameter = new SqlParameter[]
                    {
                        new SqlParameter("@UnitWorkId",this.UnitWorkId),
                        new SqlParameter("@ProjectId",this.CurrUser.LoginProjectId),
                    };
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.DataSource = tb;
            Grid1.DataBind();
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
                ShowNotify("您没有这个权限，请与管理员联系！");
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

        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.AItemEndCheckMenuId, Const.BtnModify))
            {
                if (Grid1.SelectedRowIndexArray.Length == 0)
                {
                    Alert.ShowInTop("请至少选择一条记录", MessageBoxIcon.Warning);
                    return;
                }
                string url = "ItemEndCheckView.aspx?PTP_ID={0}";
                var TestPackageApprove = TestPackageApproveService.GetTestPackageApproveById(Grid1.SelectedRowID);
                var TestPackage = BLL.TestPackageEditService.GetTestPackageByID(Grid1.SelectedRowID);

                if (string.IsNullOrEmpty(TestPackage.State))
                {
                    url = "ItemEndCheckEdit.aspx?PTP_ID={0}";
                }
                else
                {
                    if (TestPackageApprove != null)
                    {
                        if (!string.IsNullOrEmpty(TestPackageApprove.ApproveMan))
                        {
                            if (this.CurrUser.UserId == TestPackageApprove.ApproveMan || this.CurrUser.UserId == BLL.Const.sysglyId)
                            {
                                url = "ItemEndCheckEdit2.aspx?PTP_ID={0}";
                            }
                        }
                    }
                }
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format(url, Grid1.SelectedRowID, "操作 - ")));
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
        protected string ConvertMan(object PTP_ID)
        {
            if (PTP_ID != null)
            {
                var approve = BLL.TestPackageApproveService.GetTestPackageApproveById(PTP_ID.ToString());
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
                    return "施工分包商重新整改";
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

            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ItemEndCheckView.aspx?PTP_ID={0}", Grid1.SelectedRowID, "操作 - ")));
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