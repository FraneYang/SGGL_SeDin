
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace FineUIPro.Web.HJGL.PersonManage
{
    public partial class CheckerManage : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitTreeMenu();
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            if (!string.IsNullOrEmpty(this.tvControlItem.SelectedNodeID))
            {
                Model.SitePerson_Person Checker = BLL.CheckerService.GetCheckerById(this.tvControlItem.SelectedNodeID);
                if (Checker != null)
                {
                    this.btnEdit.Hidden = false;
                    this.btnNew.Hidden = false;
                    this.btnDelete.Hidden = false;
                    this.txtCheckerCode.Text = Checker.WelderCode;
                    this.txtCheckerName.Text = Checker.PersonName;

                    if (!string.IsNullOrEmpty(Checker.UnitId))
                    {
                        this.drpUnitId.Text =UnitService.GetUnitNameByUnitId(Checker.UnitId);
                    }
                    this.txtSex.Text = Checker.Sex=="1"?"男":"女";
                    if (Checker.Birthday.HasValue)
                    {
                        this.txtBirthday.Text = string.Format("{0:yyyy-MM-dd}", Checker.Birthday);
                    }
                    this.txtIdentityCard.Text = Checker.IdentityCard;
                    if (Checker.IsUsed == true)
                    {
                        cbIsOnDuty.Checked = true;
                    }
                    else
                    {
                        cbIsOnDuty.Checked = false;
                    }
                }
            }
        }
        private void BindGvItem() {
            if (!string.IsNullOrEmpty(this.tvControlItem.SelectedNodeID))
            {
                string strSql = @"SELECT WelderQualifyId, WelderId, 
                                     QualificationItem, LimitDate, CheckDate 
                           FROM Welder_WelderQualify
                           LEFT JOIN SitePerson_Person AS Welder ON Welder.PersonId=Welder_WelderQualify.WelderId
                           WHERE WelderId=@WelderId";

            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(new SqlParameter("@WelderId", this.tvControlItem.SelectedNodeID));
            if (!string.IsNullOrEmpty(this.txtQualificationItem.Text))
            {
                strSql += " and QualificationItem LIKE  @QualificationItem";
                parms.Add(new SqlParameter("@QualificationItem", "%" + this.txtQualificationItem.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = parms.ToArray();
            DataTable dt = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = dt.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, dt);

            Grid1.DataSource = table;
            Grid1.DataBind();
            }
        }
        #region 加载树
        /// <summary>
        /// 加载树
        /// </summary>
        private void InitTreeMenu()
        {
            this.tvControlItem.Nodes.Clear();
            var getUnits = UnitService.GetUnitByProjectIdUnitTypeList(this.CurrUser.LoginProjectId, Const.ProjectUnitType_2);
            foreach (var item in getUnits)
            {
                TreeNode rootNode = new TreeNode();
                rootNode.NodeID = item.UnitId;
                rootNode.Text = item.UnitName;
                this.tvControlItem.Nodes.Add(rootNode);
                var getCheckers = (from x in Funs.DB.SitePerson_Person where x.ProjectId == this.CurrUser.LoginProjectId && x.WorkPostId == Const.WorkPost_Checker && x.UnitId == item.UnitId select x).ToList();
                foreach (var sitem in getCheckers)
                {
                    TreeNode tn = new TreeNode();
                    tn.NodeID = sitem.PersonId;
                    tn.Text = sitem.PersonName;
                    tn.EnableClickEvent = true;
                    rootNode.Nodes.Add(tn);
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
            this.BindGrid();
            this.BindGvItem();
        }
        #endregion
        
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private bool GetButtonPower(string button)
        {
            return BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.CheckerManageMenuId, button);
        }
        #endregion

        #region 检测工资质Grid操作
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (GetButtonPower(Const.BtnModify))
            {
                if (!string.IsNullOrEmpty(this.tvControlItem.SelectedNodeID))
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckerItemEdit.aspx?PersonId={0}", this.tvControlItem.SelectedNodeID, "新增 - ")));
                }
            }
        }
        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            Grid1_RowDoubleClick(null, null);
        }
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckerItemEdit.aspx?WelderQualifyId={0}", Grid1.SelectedRowID, "编辑 - ")));
        }
        /// <summary>
        /// 查看按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnView_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckerItemView.aspx?WelderQualifyId={0}", Grid1.SelectedRowID, "查看 - ")));
        }
        /// <summary>
        /// 删除检测工资质
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.WelderManageMenuId, Const.BtnAdd))
            {
                if (Grid1.SelectedRowIndexArray.Length > 0)
                {
                    string strShowNotify = string.Empty;
                    foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                    {
                        string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                        var welder = BLL.WelderQualifyService.GetWelderQualifyById(rowID);
                        if (welder != null)
                        {
                                BLL.WelderQualifyService.DeleteWelderQualifyById(rowID);
                        }
                    }

                    BindGrid();
                    if (!string.IsNullOrEmpty(strShowNotify))
                    {
                        Alert.ShowInTop(strShowNotify, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        ShowNotify("删除成功！", MessageBoxIcon.Success);
                    }
                }
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtQualificationItem_TextChanged(object sender, EventArgs e)
        {
            this.BindGvItem();
        }
        protected void Grid2_Sort(object sender, GridSortEventArgs e)
        {
            BindGvItem();
        }
        /// <summary>
        /// 分页下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGvItem();
        }
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 检测工信息维护事件
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tvControlItem.SelectedNodeID))
            {
                Alert.ShowInTop("请至少选择一条记录", MessageBoxIcon.Warning);
                return;
            }
            if (GetButtonPower(Const.BtnModify))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckerManageEdit.aspx?CheckerId={0}", this.tvControlItem.SelectedNodeID, "编辑 - ")));
            }
            else if (GetButtonPower(Const.BtnSee))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckerManageView.aspx?CheckerId={0}", Grid1.SelectedRowID, "查看 - ")));
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (GetButtonPower(Const.BtnDelete))
            {
                if (string.IsNullOrEmpty(this.tvControlItem.SelectedNodeID))
                {
                    Alert.ShowInTop("请至少选择一条记录", MessageBoxIcon.Warning);
                    return;
                }
                var Checker = BLL.CheckerService.GetCheckerById(this.tvControlItem.SelectedNodeID);
                if (Checker != null)
                {
                    var ItemCheck = from x in Funs.DB.Welder_WelderQualify where x.WelderId == this.tvControlItem.SelectedNodeID select x;
                    if (ItemCheck != null)
                    {
                        Funs.DB.Welder_WelderQualify.DeleteAllOnSubmit(ItemCheck);
                        Funs.DB.SubmitChanges();
                    }
                    BLL.CheckerService.DeleteCheckerById(this.tvControlItem.SelectedNodeID);
                }
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }

        #endregion

        
    }
}