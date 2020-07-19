using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace FineUIPro.Web.HSSE.ActionPlan
{
    public partial class EditManagerRuleTemplate : PageBase
    {
        #region  定义项
        /// <summary>
        /// 选中项
        /// </summary>
        public string[] arr
        {
            get
            {
                return (string[])ViewState["arr"];
            }
            set
            {
                ViewState["arr"] = value;
            }
        }

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
                this.ItemSelectedList = new List<string>();
                Funs.DropDownPageSize(this.ddlPageSize);
                // 绑定表格
                BindGrid();
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ActionPlan_ManagerRuleMenuId;
                this.ctlAuditFlow.DataId = string.Empty;
                this.ctlAuditFlow.ProjectId = this.CurrUser.LoginProjectId;
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT ManageRuleId,ManageRuleCode,ManageRuleName,ManageRuleTypeId,VersionNo,CompileMan,CompileDate,AttachUrl,Remark
                                        , RemarkDef, IsPass, UnitId, IsBuild, ManageRuleTypeCode, ManageRuleTypeName, AttachUrlName, IsBuildName
                                        FROM dbo.View_Law_ManageRule
                                        WHERE IsPass = 1 ";       
            List<SqlParameter> listStr = new List<SqlParameter>();             
            if (!string.IsNullOrEmpty(this.txtManageRuleCode.Text.Trim()))
            {
                strSql += " AND ManageRuleCode LIKE @ManageRuleCode";
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
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
            if (ItemSelectedList.Count > 0)
            {
                for (int j = 0; j < Grid1.Rows.Count; j++)
                {
                    if (ItemSelectedList.Contains(Grid1.DataKeys[j][0].ToString()))
                    {
                        Grid1.Rows[j].Values[0] = true;
                    }
                }
            }
        }
        #endregion

        #region 分页、关闭窗口
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
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }
        #endregion

        #region Grid行点击事件
        /// <summary>
        /// Grid1行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        #endregion

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
                    Model.Law_ManageRule rule = BLL.ManageRuleService.GetManageRuleById(item);
                    if (rule != null)
                    {
                        string newKeyID = SQLHelper.GetNewID(typeof(Model.ActionPlan_ManagerRule));
                        Model.ActionPlan_ManagerRule newManagerRule = new Model.ActionPlan_ManagerRule
                        {
                            ManagerRuleId = newKeyID,
                            OldManageRuleId = rule.ManageRuleId,
                            ProjectId = this.CurrUser.LoginProjectId,
                            ManageRuleName = rule.ManageRuleName,
                            ManageRuleTypeId = rule.ManageRuleTypeId,
                            CompileDate = DateTime.Now,
                            Remark = rule.Remark,
                            CompileMan = this.CurrUser.UserId,
                            IsIssue = false,
                            Flag = true,
                            State = BLL.Const.State_0,
                            AttachUrl = rule.AttachUrl,
                            SeeFile = rule.SeeFile
                        };
                        BLL.ActionPlan_ManagerRuleService.AddManageRule(newManagerRule);
                        ////保存流程审核数据         
                        this.ctlAuditFlow.btnSaveData(this.CurrUser.LoginProjectId, BLL.Const.ActionPlan_ManagerRuleMenuId, newManagerRule.ManagerRuleId, true, newManagerRule.ManageRuleName, "../ActionPlan/ManagerRuleView.aspx?ManagerRuleId={0}");
                        
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
                                    string newUrl=urlStr.Replace("ManageRule", "ActionPlanManagerRule");
                                    if (!Directory.Exists(newUrl.Substring(0,newUrl.LastIndexOf("\\"))))
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
                            newAttachFile.MenuId = BLL.Const.ActionPlan_ManagerRuleMenuId;
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

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion
    }
}