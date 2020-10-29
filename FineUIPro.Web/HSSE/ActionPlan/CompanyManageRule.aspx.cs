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
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.ActionPlan
{
    public partial class CompanyManageRule : PageBase
    {
        /// <summary>
        /// 页面
        /// </summary>
        public int? PageSize
        {
            get
            {
                return (int?)ViewState["PageSize"];
            }
            set
            {
                ViewState["PageSize"] = value;
            }
        }
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
                Funs.DropDownPageSize(this.ddlPageSize);
                this.GetButtonPower();
                this.btnMenuDelete.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！");
                this.btnMenuDelete.ConfirmText = String.Format("你确定要删除选中的&nbsp;<b><script>{0}</script></b>&nbsp;行数据吗？", Grid1.GetSelectedCountReference());
                // 绑定表格
                BindGrid();
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT ManagerRule.ManagerRuleId,CodeRecords.Code AS ManageRuleCode,ManagerRule.ProjectId,
                             ManagerRule.ManageRuleName,ManagerRule.ManageRuleTypeId,
                             ManagerRule.VersionNo,
                             ManagerRule.CompileMan,
                             ManagerRule.CompileDate,
                             ManagerRule.AttachUrl,
                             ManagerRule.IsIssue,
                             (CASE WHEN LEN(ManagerRule.Remark) > 25 THEN LEFT(ManagerRule.Remark,25)+'...' ELSE ManagerRule.Remark END) AS ShortRemark,
                             (CASE WHEN ManagerRule.State = 0 OR ManagerRule.State IS NULL THEN '待['+OperateUser.UserName+']提交' WHEN ManagerRule.State = 2 THEN '审核/审批完成' ELSE '待['+OperateUser.UserName+']办理' END) AS  FlowOperateName,
                             ManagerRule.Remark,
                             ManagerRule.State,
                             ManagerRule.IssueDate,
                             ManageRuleType.ManageRuleTypeName AS ManageRuleTypeName 
                             FROM ActionPlan_CompanyManagerRule AS ManagerRule
                             LEFT JOIN Sys_FlowOperate AS FlowOperate ON ManagerRule.ManagerRuleId=FlowOperate.DataId AND FlowOperate.IsClosed <> 1
                             LEFT JOIN Sys_User AS OperateUser ON FlowOperate.OperaterId=OperateUser.UserId 
                             LEFT JOIN Sys_User AS Users ON Users.UserId = ManagerRule.CompileMan
                             LEFT JOIN Sys_CodeRecords AS CodeRecords ON ManagerRule.ManagerRuleId=CodeRecords.DataId 
                             LEFT JOIN dbo.Base_ManageRuleType AS ManageRuleType ON ManageRuleType.ManageRuleTypeId=ManagerRule.ManageRuleTypeId 
                             WHERE 1 =1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND ManagerRule.ProjectId = @ProjectId";
            if (!string.IsNullOrEmpty(Request.Params["projectId"]))  ///是否文件柜查看页面传项目值
            {
                listStr.Add(new SqlParameter("@ProjectId", Request.Params["projectId"]));
                strSql += " AND ManagerRule.State = @State";  ///状态为已完成
                listStr.Add(new SqlParameter("@State", BLL.Const.State_2));
            }
            else
            {
                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }
            if (!string.IsNullOrEmpty(this.txtManageRuleCode.Text.Trim()))
            {
                strSql += " AND CodeRecords.Code LIKE @ManageRuleCode";
                listStr.Add(new SqlParameter("@ManageRuleCode", "%" + this.txtManageRuleCode.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtManageRuleName.Text.Trim()))
            {
                strSql += " AND ManageRuleName LIKE @ManageRuleName";
                listStr.Add(new SqlParameter("@ManageRuleName", "%" + this.txtManageRuleName.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtManageRuleTypeName.Text.Trim()))
            {
                strSql += " AND ManageRuleTypeName LIKE @ManageRuleTypeName";
                listStr.Add(new SqlParameter("@ManageRuleTypeName", "%" + this.txtManageRuleTypeName.Text.Trim() + "%"));
            }

            if (this.cbIssue.SelectedValueArray.Length == 1)
            {
                ///是否发布
                string selectValue = String.Join(", ", this.cbIssue.SelectedValueArray);
                if (selectValue == "1")
                {
                    strSql += " AND ManagerRule.IsIssue = 1 ";
                }
                else
                {
                    strSql += " AND (ManagerRule.IsIssue = 0 OR ManagerRule.IsIssue IS NULL) ";
                }
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion    

        #region 排序、分页
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// 分页显示条数下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }
        #endregion

        #region 删除
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            this.DeleteData();
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        private void DeleteData()
        {
            this.PageSize = this.Grid1.PageIndex;
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                bool isShow = false;
                if (Grid1.SelectedRowIndexArray.Length == 1)
                {
                    isShow = true;
                }
                foreach (string id in Grid1.SelectedRowIDArray)
                {
                    string rowID = id;
                    if (this.judgementDelete(rowID, isShow))
                    {
                        var manageRule = BLL.ActionPlan_CompanyManagerRuleService.GetManagerRuleById(rowID);
                        if (manageRule != null)
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, manageRule.ManageRuleCode, manageRule.ManagerRuleId, BLL.Const.ActionPlan_CompanyManagerRuleMenuId, Const.BtnDelete);
                            BLL.ActionPlan_CompanyManagerRuleService.DeleteManageRuleById(rowID);
                        }

                        BindGrid();
                        ShowNotify("删除成功！", MessageBoxIcon.Success);
                    }
                }
            }
        }

        /// <summary>
        /// 判断是否可删除
        /// </summary>
        /// <param name="rowID"></param>
        /// <param name="isShow"></param>
        /// <returns></returns>
        private bool judgementDelete(string rowID, bool isShow)
        {
            string content = string.Empty;
            if (string.IsNullOrEmpty(content))
            {
                return true;
            }
            else
            {
                if (isShow)
                {
                    Alert.ShowInTop(content);
                }
                return false;
            }
        }
        #endregion

        #region 编辑
        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 编辑数据方法
        /// </summary>
        private void EditData()
        {
            this.PageSize = this.Grid1.PageIndex;
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string managerRuleId = Grid1.SelectedRowID;

            var managerRule = BLL.ActionPlan_CompanyManagerRuleService.GetManagerRuleById(managerRuleId);
            if (managerRule != null)
            {
                if (this.btnMenuEdit.Hidden || managerRule.State == BLL.Const.State_2)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
                {
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("CompanyManageRuleView.aspx?ManagerRuleId={0}", managerRuleId, "查看 - ")));
                }
                else
                {
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("CompanyManageRuleEdit.aspx?ManagerRuleId={0}", managerRuleId, "编辑 - ")));
                }
            }
        }
        #endregion

        #region 关闭弹出窗
        /// <summary>
        /// 关闭弹出窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }

        #endregion

        #region 编制
        /// <summary>
        /// 点击编制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCompile_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EditCompanyManageRuleTemplate.aspx", "编辑 - ")));
        }
        #endregion

        #region 审核
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuAudit_Click(object sender, EventArgs e)
        {
            //List<string> managerRuleIds = new List<string>();
            //string[] ids = Grid1.SelectedRowIDArray;
            //foreach (string id in ids)
            //{
            //    managerRuleIds.Add(id);
            //    managerRuleIds = (from x in managerRuleIds orderby x descending select x).ToList();   //固定明细排列顺序
            //}
            //if (managerRuleIds == null || managerRuleIds.Count == 0)
            //{
            //    Alert.ShowInTop("没有选择任何要操作的项！", MessageBoxIcon.Warning);
            //    return;
            //}
            //else
            //{
            //    string state = string.Empty;
            //    int j = 0;
            //    bool equles = false;
            //    foreach (string rule in managerRuleIds)
            //    {
            //        Model.HSSE_ActionPlan_ManagerRule manager = BLL.HSSE_ActionPlan_ManagerRuleService.GetManagerRuleByManagerRuleId(rule);
            //        if (j == 0)
            //        {
            //            state = manager.State;
            //            if (state == BLL.Const.ManagerRule_ApproveCompleted)
            //            {
            //                Alert.ShowInTop("请不要选择审批完成的项！", MessageBoxIcon.Warning);
            //                return;
            //            }
            //        }
            //        else
            //        {
            //            if (state != manager.State)
            //            {
            //                equles = true;
            //                break;
            //            }
            //        }
            //        j++;
            //    }

            //    if (equles == true)
            //    {
            //        Alert.ShowInTop("请选择审批状态相同的项！", MessageBoxIcon.Warning);
            //        return;
            //    }

            //    Model.HSSE_ActionPlan_ManagerRule managerRule = BLL.HSSE_ActionPlan_ManagerRuleService.GetManagerRuleByManagerRuleId(managerRuleIds[0]);

            //    Session["managerRuleIds"] = managerRuleIds;

            //    if (managerRule.State == BLL.Const.ManagerRule_Compile || managerRule.State == BLL.Const.ManagerRule_ReCompile)
            //    {
            //        PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("AddManagerRule.aspx", "编辑 - ")));
            //    }
            //    else
            //    {
            //        PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("ManagerRuleApprove.aspx", "编辑 - ")));
            //    }
            //}
        }
        #endregion

        #region 发布
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuIssuance_Click(object sender, EventArgs e)
        {
            string strShowNotify = string.Empty;
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string managerRuleId = Grid1.DataKeys[rowIndex][0].ToString();
                    Model.ActionPlan_CompanyManagerRule managerRule = BLL.ActionPlan_CompanyManagerRuleService.GetManagerRuleById(managerRuleId);
                    if (managerRule != null)
                    {
                        if (managerRule.State == BLL.Const.State_2)
                        {
                            List<Model.ActionPlan_CompanyManagerRule> issuanceManagerRules = BLL.ActionPlan_CompanyManagerRuleService.GetIsIssueManagerRulesByName(managerRule.ManageRuleName);
                            if (issuanceManagerRules.Count == 0)   //无对应版本的管理规定
                            {
                                managerRule.VersionNo = "V1.0";
                                managerRule.IsIssue = true;
                                managerRule.IssueDate = DateTime.Now;
                                BLL.ActionPlan_CompanyManagerRuleService.UpdateManageRule(managerRule);
                            }
                            else   //存在对应版本的管理规定
                            {
                                string maxVersionNo = issuanceManagerRules[issuanceManagerRules.Count - 1].VersionNo;
                                managerRule.VersionNo = "V" + (Convert.ToInt32(maxVersionNo.Substring(1, maxVersionNo.LastIndexOf(".") - 1)) + 1) + ".0";
                                managerRule.IsIssue = true;
                                managerRule.IssueDate = DateTime.Now;
                                BLL.ActionPlan_CompanyManagerRuleService.UpdateManageRule(managerRule);
                            }
                        }
                        else
                        {
                            strShowNotify += "管理规定：" + managerRule.ManageRuleName + "；";

                        }
                    }
                }
            }

            this.BindGrid();
            if (!string.IsNullOrEmpty(strShowNotify))
            {

                Alert.ShowInTop(strShowNotify + "尚未审批完成，无法发布！", MessageBoxIcon.Warning);
            }
            else
            {
                ShowNotify("发布成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region 格式化字符串
        ///// <summary>
        ///// 获取办理人
        ///// </summary>
        ///// <param name="manaerRuleId"></param>
        ///// <returns></returns>
        //protected string ConvertHandleManName(object manaerRuleId)
        //{
        //    if (manaerRuleId != null)
        //    {
        //        return "";
        //    }
        //    return null;
        //}

        ///// <summary>
        ///// 办理状态
        ///// </summary>
        ///// <param name="state"></param>
        ///// <returns></returns>
        //protected string ConvertState(object state)
        //{
        //    if (state != null)
        //    {
        //        return "";
        //    }
        //    return "";
        //}
        #endregion

        #region 获取权限按钮
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }

            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ActionPlan_CompanyManagerRuleMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnCompile.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuEdit.Hidden = false;
                }
                //if (buttonList.Contains(BLL.Const.BtnAuditing))
                //{
                //    this.btnMenuAudit.Hidden = false;
                //}
                if (buttonList.Contains(BLL.Const.BtnIssuance))
                {
                    this.btnMenuIssuance.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
                }
            }
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
            if (this.PageSize.HasValue)
            {
                this.ddlPageSize.SelectedValue = this.PageSize.ToString();
            }

            BLL.LogService.AddSys_Log(this.CurrUser, string.Empty, string.Empty, BLL.Const.ActionPlan_CompanyManagerRuleMenuId, Const.BtnQuery);
        }
        #endregion

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("安全管理规定" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

#pragma warning disable CS0108 // “ManagerRule.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “ManagerRule.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<meta http-equiv=\"content-type\" content=\"application/excel; charset=UTF-8\"/>");
            sb.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");
            sb.Append("<tr>");
            foreach (GridColumn column in grid.Columns)
            {
                sb.AppendFormat("<td>{0}</td>", column.HeaderText);
            }
            sb.Append("</tr>");
            foreach (GridRow row in grid.Rows)
            {
                sb.Append("<tr>");
                foreach (GridColumn column in grid.Columns)
                {
                    string html = row.Values[column.ColumnIndex].ToString();
                    if (column.ColumnID == "tfNumber")
                    {
                        html = (row.FindControl("labNumber") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfManageRuleCode")
                    {
                        html = (row.FindControl("lblManageRuleCode") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfOldManageRuleCode")
                    {
                        html = (row.FindControl("lblOldManageRuleCode") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfManageRuleName")
                    {
                        html = (row.FindControl("lblManageRuleName") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfManageRuleTypeName")
                    {
                        html = (row.FindControl("lblManageRuleTypeName") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfShortRemark")
                    {
                        html = (row.FindControl("lblRemark") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfState")
                    {
                        html = (row.FindControl("lblState") as AspNet.Label).Text;
                    }
                    sb.AppendFormat("<td>{0}</td>", html);
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }
        #endregion
    }
}