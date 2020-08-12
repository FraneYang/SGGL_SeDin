using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BLL;

namespace FineUIPro.Web.ProjectData
{
    public partial class ProjectUserSelect : PageBase
    {
        /// <summary>
        /// 定义项
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
            }
        }

        #region 加载
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
                this.ProjectId = Request.QueryString["ProjectId"];                
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                } 
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();

                BLL.UnitService.InitUnitDropDownList(this.drpUnit, this.ProjectId, false);
                if (!string.IsNullOrEmpty(this.CurrUser.UnitId))
                {
                    this.drpUnit.SelectedValue = this.CurrUser.UnitId;
                }
                // 绑定表格
                this.BindGrid();
            }
        }
        #endregion

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            if (!string.IsNullOrEmpty(this.ProjectId))
            {
                string strSql = @"SELECT Users.UserId,Users.UserCode,Users.UserName,Unit.UnitName"
                                + @" FROM Sys_User AS Users "
                                + @" LEFT JOIN Project_ProjectUnit AS ProjectUnit ON Users.UnitId =ProjectUnit.UnitId "
                                + @" LEFT JOIN Base_Unit AS Unit ON Users.UnitId =Unit.UnitId "
                                + @" WHERE ProjectUnit.ProjectId =@ProjectId AND ProjectUnit.UnitId=@UnitId AND Users.UserId <> @UserId "
                                + @" AND Users.UserId NOT IN (SELECT UserId FROM Project_ProjectUser WHERE ProjectId =ProjectUnit.ProjectId) ";
                List<SqlParameter> listStr = new List<SqlParameter>
                {
                    new SqlParameter("@ProjectId", this.ProjectId),
                    new SqlParameter("@UnitId", this.drpUnit.SelectedValue),
                    new SqlParameter("@UserId", BLL.Const.sysglyId)
                };
                if (!string.IsNullOrEmpty(this.txtUserName.Text.Trim()))
                {
                    strSql += " AND Users.UserName LIKE @UserName";
                    listStr.Add(new SqlParameter("@UserName", "%" + this.txtUserName.Text.Trim() + "%"));
                }
                
                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
                Grid1.RecordCount = tb.Rows.Count;
                var table = this.GetPagedDataTable(Grid1, tb);
                Grid1.DataSource = table;
                Grid1.DataBind();
            }
        }
                
        #region 排序 分页
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 
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

        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSure_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            foreach (int rowIndex in Grid1.SelectedRowIndexArray)
            {
                this.SaveData(Grid1.DataKeys[rowIndex][0].ToString());
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }

            this.SaveData(Grid1.SelectedRowID);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
                                
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

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="userId"></param>
        private void SaveData(string userId)
        {
            var projectUser = BLL.ProjectUserService.GetProjectUserByUserIdProjectId(this.ProjectId, userId);
            if (projectUser == null)
            {
                var user = BLL.UserService.GetUserByUserId(userId);
                if (user != null)
                {
                    Model.Project_ProjectUser newProjectUser = new Model.Project_ProjectUser
                    {
                        ProjectId = this.ProjectId,
                        UserId = userId,
                        UnitId = user.UnitId,
                        RoleId = user.RoleId,
                        IsPost = true
                    };
                    BLL.ProjectUserService.AddProjectUser(newProjectUser);
                    Model.Sys_RoleItem roleItem = new Model.Sys_RoleItem();
                    roleItem.ProjectId = this.ProjectId;
                    roleItem.UserId = userId;
                    roleItem.RoleId = user.RoleId;
                    roleItem.IntoDate = DateTime.Now;
                    BLL.RoleItemService.AddRoleItem(roleItem);
                    if (!string.IsNullOrEmpty(user.IdentityCard))
                    {
                        ///当前用户是否已经添加到项目现场人员中
                        var sitePerson = BLL.PersonService.GetPersonByIdentityCard(this.ProjectId, user.IdentityCard);
                        if (sitePerson == null)
                        {
                            Model.SitePerson_Person newPerson = new Model.SitePerson_Person
                            {
                                PersonId = SQLHelper.GetNewID(typeof(Model.SitePerson_Person)),
                                PersonName = user.UserName,
                                Sex = user.Sex,
                                IdentityCard = user.IdentityCard,
                                ProjectId = this.ProjectId,
                                UnitId = user.UnitId,
                                IsUsed = true
                            };
                            BLL.PersonService.AddPerson(newPerson);
                        }
                    }
                }
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../SysManage/UserListEdit.aspx?UnitId={0}&type=-1", this.drpUnit.SelectedValue, "新增 - ")));
        }
    }
}