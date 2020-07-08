using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.HJGL.HotProcessHard
{
    public partial class HardFeedback : PageBase
    {
        #region 定义项
        /// <summary>
        /// 硬度委托主键
        /// </summary>
        public string HardTrustID
        {
            get
            {
                return (string)ViewState["HardTrustID"];
            }
            set
            {
                ViewState["HardTrustID"] = value;
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
                this.HardTrustID = string.Empty;
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

            var pUnits = (from x in new Model.SGGLDB(Funs.ConnString).Project_ProjectUnit where x.ProjectId == this.CurrUser.LoginProjectId select x).ToList();
            // 获取当前用户所在单位
            var currUnit = pUnits.FirstOrDefault(x => x.UnitId == this.CurrUser.UnitId);

            var unitWorkList = (from x in new Model.SGGLDB(Funs.ConnString).WBS_UnitWork
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
                    int a = (from x in new Model.SGGLDB(Funs.ConnString).HJGL_Pipeline where x.ProjectId == this.CurrUser.LoginProjectId && x.UnitWorkId == q.UnitWorkId select x).Count();
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
                    int a = (from x in new Model.SGGLDB(Funs.ConnString).HJGL_Pipeline where x.ProjectId == this.CurrUser.LoginProjectId && x.UnitWorkId == q.UnitWorkId select x).Count();
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
            List<Model.HJGL_Hard_Trust> trustLists = new List<Model.HJGL_Hard_Trust>();

            if (!string.IsNullOrEmpty(this.txtSearchNo.Text.Trim()))
            {
                trustLists = (from x in new Model.SGGLDB(Funs.ConnString).HJGL_Hard_Trust where x.HardTrustNo.Contains(this.txtSearchNo.Text.Trim()) orderby x.HardTrustNo select x).ToList();
            }
            else
            {
                trustLists = (from x in new Model.SGGLDB(Funs.ConnString).HJGL_Hard_Trust orderby x.HardTrustNo select x).ToList();
            }

            var trustList = from x in trustLists
                            where x.ProjectId == this.CurrUser.LoginProjectId
                                  && x.UnitWorkId == node.NodeID
                            select x;
            foreach (var item in trustList)
            {
                TreeNode newNode = new TreeNode();
                newNode.Text = item.HardTrustNo;
                newNode.NodeID = item.HardTrustID;
                newNode.ToolTip = item.HardTrustNo;
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
            this.HardTrustID = tvControlItem.SelectedNodeID;
            this.BindGrid();
        }
        #endregion

        #region 数据绑定
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            this.SetTextTemp();
            this.PageInfoLoad(); ///页面输入提交信息
            string strSql = string.Empty;
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.CommandName == "委托单号")
            {
                strSql = @"SELECT * "
                     + @" FROM dbo.View_HJGL_Hard_TrustItem AS Trust"
                     + @" WHERE Trust.HardTrustID=@HardTrustID";
                listStr.Add(new SqlParameter("@HardTrustID", this.HardTrustID));
            }
            if (!string.IsNullOrEmpty(this.txtPipelineCode.Text.Trim()))
            {
                strSql += @" and Trust.PipelineCode like @PipelineCode ";
                listStr.Add(new SqlParameter("@PipelineCode", "%" + this.txtPipelineCode.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtWeldJointCode.Text.Trim()))
            {
                strSql += @" and Trust.WeldJointCode like @WeldJointCode ";
                listStr.Add(new SqlParameter("@WeldJointCode", "%" + this.txtWeldJointCode.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(Grid1, tb1);
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();

            //是否合格的绑定
            for (int i = 0; i < this.Grid1.Rows.Count; i++)
            {
                string hardTrustItemId = this.Grid1.Rows[i].DataKeys[0].ToString();
                if (hardTrustItemId != null)
                {
                    var hardFeedback = BLL.Hard_TrustItemService.GetHardTrustItemById(hardTrustItemId);
                    if (hardFeedback.IsPass == true)
                    {
                        this.Grid1.Rows[i].Values[7] = BLL.Const._True;//合格
                    }
                    else if (hardFeedback.IsPass == false)
                    {
                        this.Grid1.Rows[i].Values[8] = BLL.Const._True;//不合格
                    }
                }
            }
        }

        #region 加载页面输入提交信息
        /// <summary>
        /// 加载页面输入提交信息
        /// </summary>
        private void PageInfoLoad()
        {
            this.SimpleForm1.Reset(); ///重置所有字段
            //var trust = new Model.SGGLDB(Funs.ConnString).View_HJGL_Hard_Trust.FirstOrDefault(x => x.HardTrustID == this.HardTrustID);
            //if (trust != null)
            //{
            //    this.txtHardTrustNo.Text = trust.HardTrustNo;
            //    this.txtCheckUnit.Text = trust.CheckUnitName;
            //    this.txtHardTrustMan.Text = trust.HardTrustManName;
            //    if (trust.HardTrustDate != null)
            //    {
            //        this.txtHardTrustDate.Text = string.Format("{0:yyyy-MM-dd}", trust.HardTrustDate);
            //    }
            //    this.txtHardnessMethod.Text = trust.HardnessMethod;
            //    this.txtHardnessRate.Text = trust.HardnessRate;
            //    this.txtStandards.Text = trust.Standards;
            //    this.txtInspectionNum.Text = trust.InspectionNum;
            //    this.txtCheckNum.Text = trust.CheckNum;
            //    this.txtTestWeldNum.Text = trust.TestWeldNum;
            //    this.txtSendee.Text = trust.Sendee;
            //    this.txtDetectionTime.Text = trust.DetectionTimeStr;
            //}
        }
        #endregion

        /// <summary>
        /// 情况
        /// </summary>
        private void SetTextTemp()
        {
            this.txtHardTrustNo.Text = string.Empty;
            this.txtCheckUnit.Text = string.Empty;
            this.txtHardTrustMan.Text = string.Empty;
            this.txtHardTrustDate.Text = string.Empty;
            this.txtHardnessMethod.Text = string.Empty;
            this.txtHardnessRate.Text = string.Empty;
            this.txtStandards.Text = string.Empty;
            this.txtInspectionNum.Text = string.Empty;
            this.txtCheckNum.Text = string.Empty;
            this.txtTestWeldNum.Text = string.Empty;
            this.txtSendee.Text = string.Empty;
            this.txtDetectionTime.Text = string.Empty;
        }
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

        #region 提交
        /// <summary>
        /// 提交反馈结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_HardFeedbackMenuId, Const.BtnSave))
            {
                JArray ModifiedData = Grid1.GetMergedData();
                foreach (JObject modifiedRow in ModifiedData)
                {
                    int index = modifiedRow.Value<int>("index");
                    JObject values = modifiedRow.Value<JObject>("values");
                    CheckBoxField cbIsPass = (CheckBoxField)Grid1.FindColumn("IsPass");
                    CheckBoxField cbIsNotPass = (CheckBoxField)Grid1.FindColumn("IsNotPass");
                    string hardTrustItemId = Grid1.DataKeys[index][0].ToString();
                    var hardFeedback = BLL.Hard_TrustItemService.GetHardTrustItemById(hardTrustItemId);
                    if (hardFeedback != null)
                    {
                        bool newIsPass = cbIsPass.GetCheckedState(index);
                        bool newIsNotPass = cbIsNotPass.GetCheckedState(index);
                        var hotProessTrustItem = new Model.SGGLDB(Funs.ConnString).HJGL_HotProess_TrustItem.FirstOrDefault(x => x.WeldJointId == hardFeedback.WeldJointId && x.HotProessTrustItemId == hardFeedback.HotProessTrustItemId);
                        if (newIsPass)
                        {
                            hardFeedback.IsPass = true;
                            if (hotProessTrustItem != null)   //更新热处理委托硬度不合格记录id为空
                            {
                                hotProessTrustItem.HardTrustItemID = null;
                            }
                            hotProessTrustItem.IsPass = true;
                        }
                        else if (newIsNotPass)
                        {
                            hardFeedback.IsPass = false;
                            if (hotProessTrustItem != null)    //更新热处理委托硬度不合格记录id值
                            {
                                hotProessTrustItem.HardTrustItemID = hardFeedback.HardTrustItemID;
                            }
                            hotProessTrustItem.IsPass = false;
                        }
                        else
                        {
                            hardFeedback.IsPass = null;
                            hotProessTrustItem.HardTrustItemID = null;
                            hotProessTrustItem.IsPass = null;
                        }
                        BLL.HotProessTrustItemService.UpdateHotProessTrustItem(hotProessTrustItem);
                        BLL.Hard_TrustItemService.UpdateHardTrustItem(hardFeedback);
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

        #region 关闭弹出窗口及刷新页面
        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            this.HardTrustID = this.hdHardTrustID.Text;
            this.BindGrid();
            //this.InitTreeMenu();
            this.hdHardTrustID.Text = string.Empty;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Tree_TextChanged(object sender, EventArgs e)
        {
            this.InitTreeMenu();
            //this.BindGrid();
        }
        #endregion
    }
}