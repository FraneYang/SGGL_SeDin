using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace FineUIPro.Web.HJGL.HotProcessHard
{
    public partial class HardReport : PageBase
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
            var WeldJointList = (from x in Funs.DB.HJGL_WeldJoint
                                 join y in Funs.DB.HJGL_Pipeline on x.PipelineId equals y.PipelineId
                                 where x.ProjectId == this.CurrUser.LoginProjectId
                                 select new { x.WeldJointId, y.UnitWorkId }).ToList();
            if (unitWork1.Count() > 0)
            {
                foreach (var q in unitWork1)
                {
                    var HardTrustItem = (from y in Funs.DB.HJGL_Hard_TrustItem
                                         join z in Funs.DB.HJGL_Hard_Trust on y.HardTrustID equals z.HardTrustID
                                         where z.ProjectId == this.CurrUser.LoginProjectId && z.UnitWorkId == q.UnitWorkId
                                         select y);
                    var WeldJoint = from x in WeldJointList join y in Funs.DB.HJGL_HotProess_TrustItem on x.WeldJointId equals y.WeldJointId
                                    where y.IsCompleted == true && x.UnitWorkId == q.UnitWorkId select x;
                    var resultCount = WeldJoint.Where(w => HardTrustItem.Count(h => h.WeldJointId == w.WeldJointId) == 0).ToList().Count;
                    var u = BLL.UnitService.GetUnitByUnitId(q.UnitId);
                    TreeNode tn1 = new TreeNode();
                    tn1.NodeID = q.UnitWorkId;
                    tn1.Text = q.UnitWorkName;
                    tn1.ToolTip = "施工单位：" + u.UnitName + "(未检测焊口总数:" + resultCount + ")"; 
                    tn1.CommandName = "单位工程";
                    rootNode1.Nodes.Add(tn1);
                    BindNodes(tn1);
                }
            }
            if (unitWork2.Count() > 0)
            {
                foreach (var q in unitWork2)
                {
                    var HardTrustItem = (from y in Funs.DB.HJGL_Hard_TrustItem
                                         join z in Funs.DB.HJGL_Hard_Trust on y.HardTrustID equals z.HardTrustID
                                         where z.ProjectId == this.CurrUser.LoginProjectId && z.UnitWorkId == q.UnitWorkId
                                         select y);
                    var WeldJoint = from x in WeldJointList
                                    join y in Funs.DB.HJGL_HotProess_TrustItem on x.WeldJointId equals y.WeldJointId
                                    where y.IsCompleted == true && x.UnitWorkId == q.UnitWorkId
                                    select x;
                    var resultCount = WeldJoint.Where(w => HardTrustItem.Count(h => h.WeldJointId == w.WeldJointId) == 0).ToList().Count;
                    var u = BLL.UnitService.GetUnitByUnitId(q.UnitId);
                    TreeNode tn2 = new TreeNode();
                    tn2.NodeID = q.UnitWorkId;
                    tn2.Text = q.UnitWorkName;
                    tn2.ToolTip = "施工单位：" + u.UnitName + "(未检测焊口总数:" + resultCount + ")"; 
                    tn2.CommandName = "单位工程";
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
                trustLists = (from x in Funs.DB.HJGL_Hard_Trust where x.HardTrustNo.Contains(this.txtSearchNo.Text.Trim()) orderby x.HardTrustNo select x).ToList();
            }
            else
            {
                trustLists = (from x in Funs.DB.HJGL_Hard_Trust orderby x.HardTrustNo select x).ToList();
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
                strSql = @"SELECT * ,(CASE WHEN IsPass=1 THEN '合格' WHEN IsPass=0 THEN '不合格' WHEN IsPass IS NULL THEN '待检测' END) AS checkResult
                           FROM dbo.View_HJGL_Hard_TrustItem
                           WHERE HardTrustID=@HardTrustID";
                listStr.Add(new SqlParameter("@HardTrustID", this.HardTrustID));
            }
            if (!string.IsNullOrEmpty(this.txtPipelineCode.Text.Trim()))
            {
                strSql += @" and PipelineCode like @PipelineCode ";
                listStr.Add(new SqlParameter("@PipelineCode", "%" + this.txtPipelineCode.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtWeldJointCode.Text.Trim()))
            {
                strSql += @" and WeldJointCode like @WeldJointCode ";
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
        }

        #region 加载页面输入提交信息
        /// <summary>
        /// 加载页面输入提交信息
        /// </summary>
        private void PageInfoLoad()
        {
            this.SimpleForm1.Reset(); ///重置所有字段
            var trust = Funs.DB.View_HJGL_Hard_Trust.FirstOrDefault(x => x.HardTrustID == this.HardTrustID);
            if (trust != null)
            {
                this.txtHardnessMethod.Text = trust.HardnessMethod;
                this.txtInspectionNum.Text = trust.InspectionNum;
                this.txtEquipmentModel.Text = trust.EquipmentModel;
            }
        }
        #endregion

        /// <summary>
        /// 情况
        /// </summary>
        private void SetTextTemp()
        {
            this.txtHardnessMethod.Text = string.Empty;
            this.txtInspectionNum.Text = string.Empty;
            this.txtEquipmentModel.Text = string.Empty;
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
        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_HotHardReportMenuId, Const.BtnSave))
            {
                var trust = Funs.DB.View_HJGL_Hard_Trust.FirstOrDefault(x => x.HardTrustID == tvControlItem.SelectedNodeID);
                if (trust != null)
                {
                    string window = String.Format("HardReportEdit.aspx?HardTrustID={0}", trust.HardTrustID, "编辑 - ");
                    PageContext.RegisterStartupScript(Window1.GetShowReference(window));
                }
                else
                {
                    ShowNotify("请选择要修改的硬度委托记录！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }
    }
}