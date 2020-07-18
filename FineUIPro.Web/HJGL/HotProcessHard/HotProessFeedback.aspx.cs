using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using BLL;
using System.Data;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.HJGL.HotProcessHard
{
    public partial class HotProessFeedback : PageBase
    {
        #region 定义项
        /// <summary>
        /// 热处理委托主键
        /// </summary>
        public string HotProessTrustId
        {
            get
            {
                return (string)ViewState["HotProessTrustId"];
            }
            set
            {
                ViewState["HotProessTrustId"] = value;
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
                
                this.HotProessTrustId = string.Empty;
                this.InitTreeMenu();//加载树
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
                    rootNode1.Nodes.Add(tn1);
                    BindNodes(tn1);
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
                    rootNode2.Nodes.Add(tn2);
                    BindNodes(tn2);
                }
            }
        }

        /// <summary>
        ///  绑定树节点
        /// </summary>
        /// <param name="node"></param>
        private void BindNodes(TreeNode node)
        {
                List<Model.HJGL_HotProess_Trust> trustLists = new List<Model.HJGL_HotProess_Trust>();

                if (!string.IsNullOrEmpty(this.txtSearchNo.Text.Trim()))
                {
                    trustLists = (from x in Funs.DB.HJGL_HotProess_Trust where x.HotProessTrustNo.Contains(this.txtSearchNo.Text.Trim()) orderby x.HotProessTrustNo select x).ToList();
                }
                else
                {
                    trustLists = (from x in Funs.DB.HJGL_HotProess_Trust orderby x.HotProessTrustNo select x).ToList();
                }
                var trustList = from x in trustLists
                                where x.ProjectId == this.CurrUser.LoginProjectId
                                      && x.UnitWorkId == node.NodeID
                                select x;
                foreach (var item in trustList)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = item.HotProessTrustNo;
                    newNode.NodeID = item.HotProessTrustId;
                    newNode.ToolTip = item.HotProessTrustNo;
                    newNode.CommandName = "委托单号";
                    newNode.EnableClickEvent = true;
                    node.Nodes.Add(newNode);
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
            this.BindGrid();
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            string strSql = string.Empty;
            List<SqlParameter> listStr = new List<SqlParameter>();
            this.SetTextTemp();
            this.PageInfoLoad(); ///页面输入提交信息

            if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.CommandName == "委托单号")
            {
                var hotProessTrust = BLL.HotProess_TrustService.GetHotProessTrustById(this.tvControlItem.SelectedNodeID);
                if (hotProessTrust != null)
                {
                    this.HotProessTrustId = hotProessTrust.HotProessTrustId;
                    strSql = @"SELECT * "
                    + @" FROM dbo.View_HJGL_HotProess_TrustItem AS Trust"
                    + @" WHERE Trust.ProjectId= @ProjectId AND Trust.HotProessTrustId=@HotProessTrustId ";

                    listStr.Add(new SqlParameter("@ProjectId", hotProessTrust != null ? hotProessTrust.ProjectId : this.CurrUser.LoginProjectId));
                    listStr.Add(new SqlParameter("@HotProessTrustId", this.HotProessTrustId));

                    if (!string.IsNullOrEmpty(this.txtIsoNo.Text.Trim()))
                    {
                        strSql += @" and Trust.PipelineCode like '%'+@PipelineCode+'%' ";
                        listStr.Add(new SqlParameter("@PipelineCode", this.txtIsoNo.Text.Trim()));
                    }

                    SqlParameter[] parameter = listStr.ToArray();
                    DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
                    // 2.获取当前分页数据
                    //var table = this.GetPagedDataTable(Grid1, tb1);
                    Grid1.RecordCount = tb.Rows.Count;
                    //tb = GetFilteredTable(Grid1.FilteredData, tb);
                    var table = this.GetPagedDataTable(Grid1, tb);
                    Grid1.DataSource = table;
                    Grid1.DataBind();

                    //是否合格、是否需硬度检测的绑定
                    for (int i = 0; i < this.Grid1.Rows.Count; i++)
                    {
                        string hotProessTrustItemId = this.Grid1.Rows[i].DataKeys[0].ToString();
                        if (hotProessTrustItemId != null)
                        {
                            var hotProessFeedback = BLL.HotProessTrustItemService.GetHotProessTrustItemById(hotProessTrustItemId);
                            if (hotProessFeedback.IsCompleted == true)
                            {
                                this.Grid1.Rows[i].Values[6] = BLL.Const._True;//是否完成
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        #region 加载页面输入提交信息
        /// <summary>
        /// 加载页面输入提交信息
        /// </summary>
        private void PageInfoLoad()
        {
            var trust = BLL.HotProess_TrustService.GetHotProessTrustById(this.HotProessTrustId);
            if (trust != null)
            {
                this.txtHotProessTrustNo.Text = trust.HotProessTrustNo;
                if (trust.ProessDate.HasValue)
                {
                    this.txtProessDate.Text = string.Format("{0:yyyy-MM-dd}", trust.ProessDate);
                }
             
                this.txtProessMethod.Text = trust.ProessMethod;
                this.txtProessEquipment.Text = trust.ProessEquipment;
                if (!string.IsNullOrEmpty(trust.Tabler))
                {
                    this.txtTabler.Text = BLL.UserService.GetUserNameByUserId(trust.Tabler);
                }
                this.txtRemark.Text = trust.Remark;
            }
        }
        #endregion

        #region 清空文本
        /// <summary>
        /// 清空文本
        /// </summary>
        private void SetTextTemp()
        {
            this.txtHotProessTrustNo.Text = string.Empty;
            this.txtProessDate.Text = string.Empty;
            this.txtProessMethod.Text = string.Empty;
            this.txtProessEquipment.Text = string.Empty;
            this.txtTabler.Text = string.Empty;
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

        #region 提交
        /// <summary>
        /// 提交反馈结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_HotProessFeedbackMenuId, Const.BtnSave))
            {
                
                JArray ModifiedData = Grid1.GetMergedData();
                foreach (JObject modifiedRow in ModifiedData)
                {
                    int index = modifiedRow.Value<int>("index");
                    JObject values = modifiedRow.Value<JObject>("values");
                    CheckBoxField cbIsCompleted = (CheckBoxField)Grid1.FindColumn("IsCompleted");
                    string hotProessTrustItemId = Grid1.DataKeys[index][0].ToString();
                    var hotProessFeedback = BLL.HotProessTrustItemService.GetHotProessTrustItemById(hotProessTrustItemId);
                    if (hotProessFeedback != null)
                    {
                        bool newIsPass = cbIsCompleted.GetCheckedState(index);
                        if (newIsPass)
                        {
                            hotProessFeedback.IsCompleted = true;
                            hotProessFeedback.IsHardness = true;
                        }
                        else
                        {
                            hotProessFeedback.IsCompleted = false;
                            hotProessFeedback.IsHardness = false;
                        }
                        BLL.HotProessTrustItemService.UpdateHotProessFeedback(hotProessFeedback);
                    }                    
                }
                ShowNotify("提交成功！", MessageBoxIcon.Success);
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }
        #endregion           
    }
}