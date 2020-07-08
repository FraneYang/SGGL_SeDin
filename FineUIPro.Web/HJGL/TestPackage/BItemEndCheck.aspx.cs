using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.HJGL.TestPackage
{
    public partial class BItemEndCheck : PageBase
    {
        #region 定义项
        /// <summary>
        /// 管线主键
        /// </summary>
        public string PipelineId
        {
            get
            {
                return (string)ViewState["PipelineId"];
            }
            set
            {
                ViewState["PipelineId"] = value;
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
                this.PipelineId = string.Empty;
                this.txtSearchDate.Text = string.Format("{0:yyyy-MM}", System.DateTime.Now);
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
                btnNew.OnClientClick = Grid1.GetAddNewRecordReference(defaultObj, AppendToEnd);
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
            DateTime startTime = Convert.ToDateTime(this.txtSearchDate.Text.Trim() + "-01");
            DateTime endTime = startTime.AddMonths(1);
            var pUnits = (from x in new Model.SGGLDB(Funs.ConnString).Project_ProjectUnit where x.ProjectId == this.CurrUser.LoginProjectId select x).ToList();
            // 获取当前用户所在单位
            var currUnit = pUnits.FirstOrDefault(x => x.UnitId == this.CurrUser.UnitId);

            var unitWorkList = (from x in new Model.SGGLDB(Funs.ConnString).WBS_UnitWork
                                where x.ProjectId == this.CurrUser.LoginProjectId
                                      && x.SuperUnitWork == null && x.UnitId != null && x.ProjectType != null
                                select x).ToList();
            List<Model.PTP_TestPackage> testPackageLists = (from x in new Model.SGGLDB(Funs.ConnString).PTP_TestPackage
                                                            where x.ProjectId == this.CurrUser.LoginProjectId && x.TableDate >= startTime && x.TableDate < endTime
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
                    int a = (from x in new Model.SGGLDB(Funs.ConnString).HJGL_Pipeline where x.ProjectId == this.CurrUser.LoginProjectId && x.UnitWorkId == q.UnitWorkId select x).Count();
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
                    int a = (from x in new Model.SGGLDB(Funs.ConnString).HJGL_Pipeline where x.ProjectId == this.CurrUser.LoginProjectId && x.UnitWorkId == q.UnitWorkId select x).Count();
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
            DateTime startTime = Convert.ToDateTime(this.txtSearchDate.Text.Trim() + "-01");
            DateTime endTime = startTime.AddMonths(1);
            if (node.CommandName == "单位工程")
            {
                var pointListMonth = (from x in testPackageUnitList
                                      where x.UnitWorkId == node.NodeID
                                      select string.Format("{0:yyyy-MM}", x.TableDate)).Distinct();
                foreach (var item in pointListMonth)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = item;
                    newNode.NodeID = item + "|" + node.NodeID;
                    newNode.CommandName = "月份";
                    node.Nodes.Add(newNode);
                    this.BindNodes(newNode, testPackageUnitList);
                }
            }
            else if (node.CommandName == "月份")
            {
                var dReports = from x in testPackageUnitList
                               where x.UnitWorkId == node.ParentNode.NodeID
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
                    newNode.CommandName = "试压包";
                    newNode.NodeID = item.PTP_ID;
                    node.Nodes.Add(newNode);
                    this.BindNodes(newNode, testPackageUnitList);
                }
            }
            else if (node.CommandName == "试压包")
            {
                var isoIdList = from x in new Model.SGGLDB(Funs.ConnString).PTP_PipelineList
                                where x.PTP_ID == node.NodeID
                                select x.PipelineId;
                var isoInfos = from x in new Model.SGGLDB(Funs.ConnString).HJGL_Pipeline where isoIdList.Contains(x.PipelineId) select x;
                foreach (var item in isoInfos)
                {
                    TreeNode newNode = new TreeNode();
                    if (!string.IsNullOrEmpty(item.PipelineCode))
                    {
                        newNode.Text = item.PipelineCode;
                    }
                    else
                    {
                        newNode.Text = "未知";
                    }
                    newNode.CommandName = "管线";
                    newNode.NodeID = item.PipelineId;
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
            this.PipelineId = tvControlItem.SelectedNodeID;
            this.BindGrid();
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            this.Toolbar5.Hidden = true;
            if (!string.IsNullOrEmpty(this.PipelineId))
            {
                this.Toolbar5.Hidden = false;
            }
            string strSql = @"SELECT * FROM dbo.PTP_BItemEndCheck WHERE PipelineId=@PipelineId";
            SqlParameter[] parameter = new SqlParameter[]
                    {
                        new SqlParameter("@PipelineId",this.PipelineId),
                    };
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.DataSource = tb;
            Grid1.DataBind();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 设置LinkButtonField的点击客户端事件
            LinkButtonField deleteField = Grid1.FindColumn("Delete") as LinkButtonField;
            deleteField.OnClientClick = GetDeleteScript();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetDeleteScript()
        {
            if (!CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.BItemEndCheckMenuId, Const.BtnDelete))
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.BItemEndCheckMenuId, Const.BtnSave))
            {
                ShowNotify("您没有这个权限，请与管理员联系！");
                return;
            }
            if (string.IsNullOrEmpty(this.PipelineId))
            {
                ShowNotify("请先选择一条试压包下管线！");
                return;
            }
            if (Grid1.GetModifiedData().Count > 0)
            {
                JArray teamGroupData = Grid1.GetMergedData();
                foreach (JObject teamGroupRow in teamGroupData)
                {
                    string status = teamGroupRow.Value<string>("status");
                    JObject values = teamGroupRow.Value<JObject>("values");
                    Model.PTP_BItemEndCheck newBItemEndCheck = new Model.PTP_BItemEndCheck();
                    newBItemEndCheck.PipelineId = this.PipelineId;
                    newBItemEndCheck.Remark = values.Value<string>("Remark");
                    newBItemEndCheck.CheckMan = values.Value<string>("CheckMan");
                    newBItemEndCheck.CheckDate = Funs.GetNewDateTime(values.Value<string>("CheckDate"));
                    newBItemEndCheck.DealMan = values.Value<string>("DealMan");
                    newBItemEndCheck.DealDate = Funs.GetNewDateTime(values.Value<string>("DealDate"));
                    if (status == "newadded")
                    {
                        BLL.BItemEndCheckService.AddBItemEndCheck(newBItemEndCheck);
                    }
                    else
                    {
                        newBItemEndCheck.BItemCheckId = teamGroupRow.Value<string>("id");
                        BLL.BItemEndCheckService.UpdateBItemEndCheck(newBItemEndCheck);
                    }
                }
                this.BindGrid();
            }

            ShowNotify("数据保存成功！（表格数据已重新绑定）");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.BItemEndCheckMenuId, Const.BtnDelete))
            {
                string rowID = Grid1.SelectedRowID;
                if (!string.IsNullOrEmpty(rowID))
                {
                    BLL.BItemEndCheckService.DeleteBItemEndCheckByID(rowID);
                    BindGrid();
                }
                else
                {
                    ShowNotify("请选中删除行！");
                }

            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！");
            }
        }
    }
}