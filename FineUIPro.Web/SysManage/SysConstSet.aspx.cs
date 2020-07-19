using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BLL;

namespace FineUIPro.Web.SysManage
{
    public partial class SysConstSet : PageBase
    {
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                /// TAB1加载页面方法
                this.LoadTab1Data();
                /// TAB2加载页面方法
                this.LoadTab2Data();
                /// TAB2加载页面方法
                this.LoadTab3Data();
            }
        }

        #region TAB1加载页面方法
        /// <summary>
        /// 加载页面方法
        /// </summary>
        private void LoadTab1Data()
        {
            //var sysSet = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_Synchronization).FirstOrDefault();
            //if (sysSet != null)
            //{
            //    if (sysSet.ConstValue == "1")
            //    {
            //        this.ckSynchronization.Checked = true;
            //    }
            //    else
            //    {
            //        this.ckSynchronization.Checked = false;
            //    }
            //}
            var sysSet2 = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_IsMonthReportGetAVG).FirstOrDefault();
            if (sysSet2 != null)
            {
                if (sysSet2.ConstValue == "1")
                {
                    this.ckIsMonthReportGetAVG.Checked = true;
                }
                else
                {
                    this.ckIsMonthReportGetAVG.Checked = false;
                }
            }
            var sysSet3 = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_MonthReportFreezeDay).FirstOrDefault();
            if (sysSet3 != null)
            {
                this.txtMonthReportFreezeDay.Text = sysSet3.ConstValue;
            }

            var sysSet4 = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_MenuFlowOperate).FirstOrDefault();
            if (sysSet4 != null)
            {
                if (sysSet4.ConstValue == "1")
                {
                    this.ckMenuFlowOperate.Checked = true;
                }
                else
                {
                    this.ckMenuFlowOperate.Checked = false;
                }
            }
            var sysSet5= (from x in Funs.DB.Sys_Const  where x.ConstText == "员工绩效考核第一季度生成时间" select x).ToList().FirstOrDefault();
            if (sysSet5 != null) {
                string[] str = sysSet5.ConstValue.Split('|');
                if (str.Length > 0) {
                    this.txtMarch.Text = (str[0] == null ? "" : str[0]).ToString();
                    this.txtMarchday.Text= (str[1] == null ? "" : str[1]).ToString();
                }
            }
            var sysSet6 = (from x in Funs.DB.Sys_Const where x.ConstText == "员工绩效考核第二季度生成时间" select x).ToList().FirstOrDefault();
            if (sysSet6 != null)
            {
                string[] str = sysSet6.ConstValue.Split('|');
                if (str.Length > 0)
                {
                    this.txtJune.Text = (str[0] == null ? "" : str[0]).ToString();
                    this.txtJuneday.Text = (str[1] == null ? "" : str[1]).ToString();
                }
            }
            var sysSet7 = (from x in Funs.DB.Sys_Const where x.ConstText == "员工绩效考核第三季度生成时间" select x).ToList().FirstOrDefault();
            if (sysSet7 != null)
            {
                string[] str = sysSet7.ConstValue.Split('|');
                if (str.Length > 0)
                {
                    this.txtSeptember.Text = (str[0] == null ? "" : str[0]).ToString();
                    this.txtSeptemberday.Text = (str[1] == null ? "" : str[1]).ToString();
                }
            }
            var sysSet8 = (from x in Funs.DB.Sys_Const where x.ConstText == "员工绩效考核第四季度生成时间" select x).ToList().FirstOrDefault();
            if (sysSet8 != null)
            {
                string[] str = sysSet8.ConstValue.Split('|');
                if (str.Length > 0)
                {
                    this.txtDecember.Text = (str[0] == null ? "" : str[0]).ToString();
                    this.txtDecemberday.Text = (str[1] == null ? "" : str[1]).ToString();
                }
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var db = new Model.SGGLDB(Funs.ConnString);
            //var sysSet = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_Synchronization).FirstOrDefault();
            //if (sysSet != null)
            //{
            //    if (this.ckSynchronization.Checked == true)
            //    {
            //        sysSet.ConstValue = "1";
            //    }
            //    else
            //    {
            //        sysSet.ConstValue = "0";
            //    }
            //    Funs.DB.SubmitChanges();
            //}
            var sysSet2 = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_IsMonthReportGetAVG).FirstOrDefault();
            if (sysSet2 != null)
            {
                if (this.ckIsMonthReportGetAVG.Checked == true)
                {
                    sysSet2.ConstValue = "1";
                }
                else
                {
                    sysSet2.ConstValue = "0";
                }
                db.SubmitChanges();
            }
            var sysSet3 = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_MonthReportFreezeDay).FirstOrDefault();
            if (sysSet3 != null)
            {
                sysSet3.ConstValue = this.txtMonthReportFreezeDay.Text.Trim();
                Funs.DB.SubmitChanges();
            }

            var sysSet4 = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_MenuFlowOperate).FirstOrDefault();
            if (sysSet4 != null)
            {
                if (this.ckMenuFlowOperate.Checked == true)
                {
                    sysSet4.ConstValue = "1";
                }
                else
                {
                    sysSet4.ConstValue = "0";
                }
                db.SubmitChanges();
            }
            Model.Sys_Const sysSet5 = db.Sys_Const.FirstOrDefault(x =>x.ConstText == "员工绩效考核第一季度生成时间");
            if (sysSet5 != null)
            {
                if (!string.IsNullOrEmpty(this.txtMarch.Text.Trim())) {
                    sysSet5.ConstValue = this.txtMarch.Text.Trim();
                }
                if (!string.IsNullOrEmpty(this.txtMarchday.Text.Trim())) {
                    sysSet5.ConstValue += "|" + this.txtMarchday.Text.Trim();
                }
                Funs.DB.SubmitChanges();
            }
            var sysSet6 = db.Sys_Const.FirstOrDefault(x => x.ConstText == "员工绩效考核第二季度生成时间");
            if (sysSet6 != null)
            {
                if (!string.IsNullOrEmpty(this.txtJune.Text.Trim()))
                {
                    sysSet6.ConstValue = this.txtJune.Text.Trim();
                }
                if (!string.IsNullOrEmpty(this.txtJuneday.Text.Trim()))
                {
                    sysSet6.ConstValue += "|" + this.txtJuneday.Text.Trim();
                }
                db.SubmitChanges();
            }
            var sysSet7 = db.Sys_Const.FirstOrDefault(x => x.ConstText == "员工绩效考核第三季度生成时间");
            if (sysSet7 != null)
            {
                if (!string.IsNullOrEmpty(this.txtSeptember.Text.Trim()))
                {
                    sysSet7.ConstValue = this.txtSeptember.Text.Trim();
                }
                if (!string.IsNullOrEmpty(this.txtSeptemberday.Text.Trim()))
                {
                    sysSet7.ConstValue += "|" + this.txtSeptemberday.Text.Trim();
                }
                db.SubmitChanges();
            }
            var sysSet8 = db.Sys_Const.FirstOrDefault(x => x.ConstText == "员工绩效考核第四季度生成时间");
            if (sysSet8 != null)
            {
                if (!string.IsNullOrEmpty(this.txtDecember.Text.Trim()))
                {
                    sysSet8.ConstValue = this.txtDecember.Text.Trim();
                }
                if (!string.IsNullOrEmpty(this.txtDecemberday.Text.Trim()))
                {
                    sysSet8.ConstValue += "|" + this.txtDecemberday.Text.Trim();
                }
                db.SubmitChanges();
            }
            ShowNotify("保存成功！", MessageBoxIcon.Success);
            BLL.LogService.AddSys_Log(this.CurrUser, "修改系统环境设置！", string.Empty, BLL.Const.SysConstSetMenuId, BLL.Const.BtnModify);
        }
        
        #endregion

        #region TAB2加载页面方法
        /// <summary>
        /// TAB2加载页面方法
        /// </summary>
        private void LoadTab2Data()
        {
            this.treeMenu.Nodes.Clear();
            var sysMenu = BLL.SysMenuService.GetIsUsedMenuListBySupType(this.rblMenuType.SelectedValue);
            if (sysMenu.Count() > 0)
            {
                this.InitTreeMenu(sysMenu, null);
            }            
        }

        #region 加载菜单下拉框树
        /// <summary>
        /// 加载菜单下拉框树
        /// </summary>
        private void InitTreeMenu(List<Model.Sys_Menu> menusList, TreeNode node)
        {
            string supMenu = "0";
            if (node != null)
            { 
                supMenu = node.NodeID;
            }
            var menuItemList = menusList.Where(x => x.SuperMenu == supMenu).OrderBy(x => x.SortIndex);    //获取菜单列表
            if (menuItemList.Count() > 0)
            {
                foreach (var item in menuItemList)
                {
                    TreeNode newNode = new TreeNode
                    {
                        Text = item.MenuName,
                        NodeID = item.MenuId,
                    };

                    if (node == null)
                    {
                        this.treeMenu.Nodes.Add(newNode);
                    }
                    else
                    {
                        node.Nodes.Add(newNode);
                    }
                    if (!item.IsEnd.HasValue || item.IsEnd == false)
                    {
                        InitTreeMenu(menusList, newNode);
                    }
                }
            }
        }
        #endregion

        #region 下拉框回发事件
        /// <summary>
        /// 下拉框回发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpMenu_TextChanged(object sender, EventArgs e)
        {
            string menuId = this.drpMenu.Value;
            ///加载流程列表
            this.BindGrid();
            var sysMenu = BLL.SysMenuService.GetSysMenuByMenuId(menuId);
            if (sysMenu != null && sysMenu.IsEnd == true)
            {
            }
            else
            {
                this.drpMenu.Text = string.Empty;
                this.drpMenu.Value = string.Empty;
                if (sysMenu != null)
                { 
                    ShowNotify("请选择末级菜单操作！", MessageBoxIcon.Warning);
                }
            }
        }
        #endregion

        #region 流程列表绑定数据
        /// <summary>
        /// 流程列表绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT flow.FlowOperateId,flow.MenuId,flow.FlowStep,flow.GroupNum,flow.OrderNum,flow.AuditFlowName,flow.RoleId,flow.IsFlowEnd"                
                + @" FROM dbo.Sys_MenuFlowOperate AS flow "
                + @" WHERE flow.MenuId=@MenuId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            string menuId = string.Empty;
            if (!string.IsNullOrEmpty(this.drpMenu.Value))
            {
                menuId = this.drpMenu.Value;
            }
            listStr.Add(new SqlParameter("@MenuId", menuId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();          
        }
        
        /// <summary>
        /// 得到角色名称字符串
        /// </summary>
        /// <param name="bigType"></param>
        /// <returns></returns>
        protected string ConvertRole(object roleIds)
        {
            return BLL.RoleService.getRoleNamesRoleIds(roleIds);
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

        #region 增加编辑事件
        /// <summary>
        /// 增加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFlowOperateNew_Click(object sender, EventArgs e)
        {
            var sysMenu = SysMenuService.GetSysMenuByMenuId(this.drpMenu.Value);
            if (sysMenu != null && sysMenu.IsEnd == true)
            {
                var getMenuFlowOperate = Funs.DB.Sys_MenuFlowOperate.FirstOrDefault(x => x.MenuId == sysMenu.MenuId && x.IsFlowEnd == true);
                if (getMenuFlowOperate == null)
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("MenuFlowOperateEdit.aspx?MenuId={0}&FlowOperateId={1}", sysMenu.MenuId, string.Empty, "增加 - ")));
                }
                else
                {
                    Alert.ShowInParent("流程已存在结束步骤！", MessageBoxIcon.Warning);
                }
                
            }           
        }

        /// <summary>
        /// Grid双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {          
            var sysMenu = BLL.SysMenuService.GetSysMenuByMenuId(this.drpMenu.Value);
            if (sysMenu != null && sysMenu.IsEnd == true)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("MenuFlowOperateEdit.aspx?MenuId={0}&FlowOperateId={1}", sysMenu.MenuId, Grid1.SelectedRowID, "编辑 - ")));
            }
        }
        #endregion

        #region  删除数据
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFlowOperateDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                //if ( LicensePublicService.lisenWorkList.Contains(this.drpMenu.Value))
                //{
                //    foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                //    {
                //        string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                //        BLL.SysConstSetService.DeleteMenuFlowOperateLicense(rowID);
                //    }
                //}
                //else
                //{
                    foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                    {
                        string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                        BLL.SysConstSetService.DeleteMenuFlowOperateByFlowOperateId(rowID);
                    }
                //}

                BLL.MenuFlowOperateService.SetSortIndex(this.drpMenu.Value);
                BindGrid();
                BLL.LogService.AddSys_Log(this.CurrUser, "删除审批流程信息！", null, BLL.Const.SysConstSetMenuId, BLL.Const.BtnDelete);
                ShowNotify("删除数据成功!");
            }
        }
        #endregion

        #region 关闭弹出窗口
        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion
        #endregion        
        #endregion

        #region TAB3加载页面方法
        /// <summary>
        /// 加载页面方法
        /// </summary>
        private void LoadTab3Data()
        {
            var sysTestRule = Funs.DB.Sys_TestRule.FirstOrDefault();
            if (sysTestRule != null)
            {
                this.txtDuration.Text = sysTestRule.Duration.ToString();
                this.txtSValue.Text = sysTestRule.SValue.ToString();
                this.txtMValue.Text = sysTestRule.MValue.ToString();
                this.txtJValue.Text = sysTestRule.JValue.ToString();
                this.txtSCount.Text = sysTestRule.SCount.ToString();
                this.txtMCount.Text = sysTestRule.MCount.ToString();
                this.txtJCount.Text = sysTestRule.JCount.ToString();                
                txtTab3_TextChanged(null, null);
                this.txtPassingScore.Text = sysTestRule.PassingScore.ToString();
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTab3Save_Click(object sender, EventArgs e)
        {
            var getTestRule = from x in Funs.DB.Sys_TestRule select x;
            if (getTestRule.Count() > 0)
            {
                Funs.DB.Sys_TestRule.DeleteAllOnSubmit(getTestRule);
            }

            Model.Sys_TestRule newTestRule = new Model.Sys_TestRule
            {
                TestRuleId = SQLHelper.GetNewID(),
                Duration = Funs.GetNewIntOrZero(this.txtDuration.Text),
                SValue = Funs.GetNewIntOrZero(this.txtSValue.Text),
                MValue = Funs.GetNewIntOrZero(this.txtMValue.Text),
                JValue = Funs.GetNewIntOrZero(this.txtJValue.Text),
                SCount = Funs.GetNewIntOrZero(this.txtSCount.Text),
                MCount = Funs.GetNewIntOrZero(this.txtMCount.Text),
                JCount = Funs.GetNewIntOrZero(this.txtJCount.Text),
                PassingScore = Funs.GetNewIntOrZero(this.txtPassingScore.Text),
            };

            Funs.DB.Sys_TestRule.InsertOnSubmit(newTestRule);
            Funs.DB.SubmitChanges();

            ShowNotify("保存成功！", MessageBoxIcon.Success);
            LogService.AddSys_Log(this.CurrUser, "修改考试规则设置！", string.Empty, Const.SysConstSetMenuId, Const.BtnModify);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtTab3_TextChanged(object sender, EventArgs e)
        {
            int SValue = Funs.GetNewIntOrZero(this.txtSValue.Text);
            int MValue = Funs.GetNewIntOrZero(this.txtMValue.Text);
            int JValue = Funs.GetNewIntOrZero(this.txtJValue.Text);
            int SCount = Funs.GetNewIntOrZero(this.txtSCount.Text);
            int MCount = Funs.GetNewIntOrZero(this.txtMCount.Text);
            int JCount = Funs.GetNewIntOrZero(this.txtJCount.Text);
            this.lbTotalScore.Text = (SCount * SValue + MCount * MValue + JCount * JValue).ToString();
            this.lbTotalCount.Text = (SCount + MCount + JCount).ToString();
        }
        #endregion

        /// <summary>
        ///  选择菜单类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblMenuType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadTab2Data();
            this.drpMenu.Text = string.Empty;
            this.drpMenu.Value = string.Empty;
        }
    }
}