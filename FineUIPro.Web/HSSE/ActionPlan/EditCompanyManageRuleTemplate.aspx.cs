using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace FineUIPro.Web.HSSE.ActionPlan
{
    public partial class EditCompanyManageRuleTemplate : PageBase
    {
        #region 加载页面
        /// <summary>
        /// GV被选择项列表
        /// </summary>
        public List<string> ItemSelectedList
        {
            get
            {
                return (List<string>)ViewState["ItemSelectedList"];
            }
            set
            {
                ViewState["ItemSelectedList"] = value;
            }
        }
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
                this.ItemSelectedList = new List<string>();
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                }
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                this.BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = "SELECT SafetyInstitution.SafetyInstitutionId"
                          + @",SafetyInstitution.SafetyInstitutionName"
                          + @",SafetyInstitution.EffectiveDate"
                          + @",SafetyInstitution.Scope"
                          + @",SafetyInstitution.Remark"
                          + @",SafetyInstitution.FileContents"
                          + @" FROM HSSESystem_SafetyInstitution AS SafetyInstitution "
                          + @" WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.txtSafetyInstitutionName.Text.Trim()))
            {
                strSql += " AND SafetyInstitution.SafetyInstitutionName LIKE @SafetyInstitutionName";
                listStr.Add(new SqlParameter("@SafetyInstitutionName", "%" + this.txtSafetyInstitutionName.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtStartDate.Text.Trim()))
            {
                strSql += " AND SafetyInstitution.EffectiveDate >= @StartDate";
                listStr.Add(new SqlParameter("@StartDate", this.txtStartDate.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
            {
                strSql += " AND SafetyInstitution.EffectiveDate <= @EndDate";
                listStr.Add(new SqlParameter("@EndDate", this.txtEndDate.Text.Trim()));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        /// <summary>
        /// 改变索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// 分页下拉选择事件
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
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
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
            if (!string.IsNullOrEmpty(this.txtStartDate.Text.Trim()) && !string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
            {
                if (Funs.GetNewDateTime(this.txtStartDate.Text.Trim()) > Funs.GetNewDateTime(this.txtEndDate.Text.Trim()))
                {
                    Alert.ShowInTop("开始时间不能大于结束时间", MessageBoxIcon.Warning);
                    return;
                }
            }
            this.BindGrid();
        }
        #endregion

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string rowID = Grid1.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "IsSelected")
            {
                CheckBoxField checkField = (CheckBoxField)Grid1.FindColumn("ckbIsSelected");
                if (checkField.GetCheckedState(e.RowIndex))
                {
                    if (!ItemSelectedList.Contains(rowID))
                    {
                        ItemSelectedList.Add(rowID);
                    }
                }
                else
                {
                    if (ItemSelectedList.Contains(rowID))
                    {
                        ItemSelectedList.Remove(rowID);
                    }
                }
            }
        }

        #region  保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ItemSelectedList.Count() > 0)
            {
                foreach (var item in ItemSelectedList)
                {
                    Model.HSSESystem_SafetyInstitution rule = BLL.ServerSafetyInstitutionService.GetSafetyInstitutionById(item);
                    if (rule != null)
                    {
                        string newKeyID = SQLHelper.GetNewID(typeof(Model.ActionPlan_CompanyManagerRule));
                        Model.ActionPlan_CompanyManagerRule newManagerRule = new Model.ActionPlan_CompanyManagerRule
                        {
                            ManagerRuleId = newKeyID,
                            OldManageRuleId = rule.SafetyInstitutionId,
                            ProjectId = this.CurrUser.LoginProjectId,
                            ManageRuleName = rule.SafetyInstitutionName,
                            CompileDate = DateTime.Now,
                            Remark = rule.Remark,
                            CompileMan = this.CurrUser.UserId,
                            IsIssue = false,
                            Flag = true,
                            State = BLL.Const.State_0,
                            AttachUrl = rule.AttachUrl,
                            SeeFile = rule.FileContents
                        };
                        BLL.ActionPlan_CompanyManagerRuleService.AddManageRule(newManagerRule);
                        ////保存流程审核数据         
                        this.ctlAuditFlow.btnSaveData(this.CurrUser.LoginProjectId, BLL.Const.ActionPlan_CompanyManagerRuleMenuId, newManagerRule.ManagerRuleId, true, newManagerRule.ManageRuleName, "../ActionPlan/CompanyManageRuleView.aspx?ManagerRuleId={0}");

                        Model.AttachFile attachFile = Funs.DB.AttachFile.FirstOrDefault(x => x.ToKeyId == item);
                        if (attachFile != null)
                        {
                            Model.AttachFile newAttachFile = new Model.AttachFile
                            {
                                AttachFileId = SQLHelper.GetNewID(typeof(Model.AttachFile)),
                                ToKeyId = newKeyID
                            };
                            string[] urls = attachFile.AttachUrl.Split(',');
                            foreach (string url in urls)
                            {
                                string urlStr = BLL.Funs.RootPath + url;
                                if (File.Exists(urlStr))
                                {
                                    string newUrl = urlStr.Replace("ManageRule", "ActionPlanManagerRule");
                                    if (!Directory.Exists(newUrl.Substring(0, newUrl.LastIndexOf("\\"))))
                                    {
                                        Directory.CreateDirectory(newUrl.Substring(0, newUrl.LastIndexOf("\\")));
                                    }
                                    if (!File.Exists(newUrl))
                                    {
                                        File.Copy(urlStr, newUrl);
                                    }
                                }
                            }
                            newAttachFile.AttachSource = attachFile.AttachSource.Replace("ManageRule", "ActionPlanManagerRule");
                            newAttachFile.AttachUrl = attachFile.AttachUrl.Replace("ManageRule", "ActionPlanManagerRule");
                            newAttachFile.MenuId = BLL.Const.ActionPlan_CompanyManagerRuleMenuId;
                            Funs.DB.AttachFile.InsertOnSubmit(newAttachFile);
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                Alert.ShowInParent("请至少选择一条记录！");
                return;
            }
        }
        #endregion
    }
}