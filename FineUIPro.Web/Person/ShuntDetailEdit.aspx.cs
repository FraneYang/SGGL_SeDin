using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;
using Newtonsoft.Json.Linq;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.Person
{
    public partial class ShuntDetailEdit : PageBase
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
                WorkPostService.InitWorkPostDropDownList(this.drpWorkPost, true);
                WorkPostService.InitWorkPostNameDropDownList(this.drpWP, false);
                PostTitleService.InitPostTitleDropDownList(this.drpPostTitle, true);
                PracticeCertificateService.InitPracticeCertificateDropDownList(this.drpCertificate, true);
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
            string strSql = @"SELECT Users.UserId,Users.Account,Users.UserCode,Users.Password,Users.UserName,Users.RoleId,Users.WorkPostId,Users.UnitId,Users.IsPost,CASE WHEN  Users.IsPost=1 THEN '是' ELSE '否' END AS IsPostName,Users.IdentityCard,Users.Telephone,Users.IsOffice,"
                                + @"Users.Major,PostTitle.PostTitleName"
                                + @" From dbo.Sys_User AS Users"
                                + @" LEFT JOIN Base_Unit AS Unit ON Unit.UnitId=Users.UnitId"
                                + @" LEFT JOIN Base_PostTitle AS PostTitle ON PostTitle.PostTitleId=Users.PostTitleId"
                                + @" WHERE Users.UserId !='" + Const.sysglyId + "' AND Users.IsPost =1 AND Users.UserId !='" + Const.hfnbdId + "' AND  Users.UserId !='" + Const.sedinId + "' AND Unit.UnitId='" + Const.UnitId_SEDIN + "' AND Users.DepartId='" + Const.Depart_constructionId + "' ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (this.drpWorkPost.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND CHARINDEX(@WorkPostId,Users.WorkPostId)>0 ";
                listStr.Add(new SqlParameter("@WorkPostId", this.drpWorkPost.SelectedValue));
            }
            if (this.drpPostTitle.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND Users.PostTitleId = @PostTitleId";
                listStr.Add(new SqlParameter("@PostTitleId", this.drpPostTitle.SelectedValue));
            }
            if (this.drpCertificate.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND CHARINDEX(@CertificateId,Users.CertificateId)>0 ";
                listStr.Add(new SqlParameter("@CertificateId", this.drpCertificate.SelectedValue));
            }
            if (!string.IsNullOrEmpty(Request.Params["ids"]))
            {
                strSql += " AND CHARINDEX(Users.UserId,@Ids)=0 ";
                listStr.Add(new SqlParameter("@Ids", Request.Params["ids"]));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
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
            bool b = true;
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                AspNet.CheckBox cb = (AspNet.CheckBox)(this.Grid1.Rows[i].FindControl("cbSelect"));
                if (cb.Checked)   //选择项
                {
                    string workPost = values.Value<string>("WorkPost");
                    if (string.IsNullOrEmpty(workPost))
                    {
                        b = false;
                    }
                }
            }
            if (!b)
            {
                Alert.ShowInTop("勾选人员的拟聘岗位不能为空！", MessageBoxIcon.Warning);
                return;
            }
            string ids = string.Empty;
            if (!string.IsNullOrEmpty(Request.Params["ids"]))
            {
                ids = Request.Params["ids"] + ",";
            }
            var workPosts = BLL.WorkPostService.GetWorkPostList();
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                AspNet.CheckBox cb = (AspNet.CheckBox)(this.Grid1.Rows[i].FindControl("cbSelect"));
                if (cb.Checked)   //选择项
                {
                    string id = Grid1.Rows[i].RowID;
                    string workPost = values.Value<string>("WorkPost");
                    var w = workPosts.FirstOrDefault(x => x.WorkPostName == workPost);
                    string wid = string.Empty;
                    if (w != null)
                    {
                        wid = w.WorkPostId;
                    }
                    ids += id + "|" + wid + ",";
                }
            }
            if (!string.IsNullOrEmpty(ids))
            {
                ids = ids.Substring(0, ids.Length - 1);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(ids) + ActiveWindow.GetHidePostBackReference());
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

        #region 转换字符串
        //<summary>
        //获取职业资格证书名称
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        protected string ConvertCertificateName(object UserId)
        {
            string CertificateName = string.Empty;
            if (UserId != null)
            {
                var user = BLL.UserService.GetUserByUserId(UserId.ToString());
                if (user != null && !string.IsNullOrEmpty(user.CertificateId))
                {
                    string[] Ids = user.CertificateId.Split(',');
                    foreach (string t in Ids)
                    {
                        var type = BLL.PracticeCertificateService.GetPracticeCertificateById(t);
                        if (type != null)
                        {
                            CertificateName += type.PracticeCertificateName + ",";
                        }
                    }
                }
            }
            if (CertificateName != string.Empty)
            {
                return CertificateName.Substring(0, CertificateName.Length - 1);
            }
            else
            {
                return "";
            }
        }

        //<summary>
        //获取当前项目名称
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        protected string ConvertCurrProject(object UserId)
        {
            string ProjectName = string.Empty;
            if (UserId != null)
            {
                var roleItem = BLL.RoleItemService.GeCurrRoleItemByUserId(UserId.ToString());
                if (roleItem != null && roleItem.OutDate != null)
                {
                    if (!string.IsNullOrEmpty(roleItem.ProjectId))
                    {
                        ProjectName = BLL.ProjectService.GetProjectNameByProjectId(roleItem.ProjectId);
                    }
                    else
                    {
                        ProjectName = roleItem.ProjectName;
                    }
                }
            }
            return ProjectName;
        }

        //<summary>
        //获取岗位名称
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        protected string ConvertUserWorkPost(object UserId)
        {
            string WorkPostName = string.Empty;
            if (UserId != null)
            {
                var user = BLL.UserService.GetUserByUserId(UserId.ToString());
                if (user != null && !string.IsNullOrEmpty(user.WorkPostId))
                {
                    string[] Ids = user.WorkPostId.Split(',');
                    foreach (string t in Ids)
                    {
                        var type = BLL.WorkPostService.GetWorkPostById(t);
                        if (type != null)
                        {
                            WorkPostName += type.WorkPostName + ",";
                        }
                    }
                }
            }
            return WorkPostName;
        }

        //<summary>
        //获取当前岗位名称
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        protected string ConvertCurrWorkPost(object UserId)
        {
            string WorkPostName = string.Empty;
            if (UserId != null)
            {
                var roleItem = BLL.RoleItemService.GeCurrRoleItemByUserId(UserId.ToString());
                if (roleItem != null && roleItem.OutDate != null)
                {
                    var projectUser = BLL.ProjectUserService.GetProjectUserByUserIdProjectId(roleItem.ProjectId, roleItem.UserId);
                    if (projectUser != null)
                    {
                        var user = BLL.UserService.GetUserByUserId(projectUser.UserId);
                        var sitePerson = BLL.PersonService.GetPersonByIdentityCard(projectUser.ProjectId, user.IdentityCard);
                        if (sitePerson != null && !string.IsNullOrEmpty(sitePerson.WorkPostId))
                        {
                            var workPost = BLL.WorkPostService.GetWorkPostById(sitePerson.WorkPostId);
                            if (workPost != null)
                            {
                                WorkPostName = workPost.WorkPostName;
                            }
                        }
                    }
                }
            }
            return WorkPostName;
        }

        //<summary>
        //获取历史岗位名称
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        protected string ConvertOldWorkPost(object UserId)
        {
            string WorkPostName = string.Empty;
            if (UserId != null)
            {
                var roleItems = BLL.RoleItemService.GeOldRoleItemsByUserId(UserId.ToString());
                if (roleItems.Count > 3)
                {
                    roleItems = roleItems.Take(3).ToList();
                }
                foreach (var roleItem in roleItems)
                {
                    if (roleItem != null && roleItem.OutDate != null)
                    {
                        var projectUser = BLL.ProjectUserService.GetProjectUserByUserIdProjectId(roleItem.ProjectId, roleItem.UserId);
                        if (projectUser != null)
                        {
                            var user = BLL.UserService.GetUserByUserId(projectUser.UserId);
                            var sitePerson = BLL.PersonService.GetPersonByIdentityCard(projectUser.ProjectId, user.IdentityCard);
                            if (sitePerson != null && !string.IsNullOrEmpty(sitePerson.WorkPostId))
                            {
                                var workPost = BLL.WorkPostService.GetWorkPostById(sitePerson.WorkPostId);
                                if (workPost != null)
                                {
                                    WorkPostName += workPost.WorkPostName + ",";
                                }
                            }
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(WorkPostName))
            {
                WorkPostName = WorkPostName.Substring(0, WorkPostName.Length - 1);
            }
            return WorkPostName;
        }

        //<summary>
        //获取拟聘岗位名称
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        protected string ConvertWorkPost(object WorkPostId)
        {
            string WorkPostName = string.Empty;
            if (WorkPostId != null)
            {
                var workPost = BLL.WorkPostService.GetWorkPostById(WorkPostId.ToString());
                if (workPost != null)
                {
                    WorkPostName = workPost.WorkPostName;

                }
            }
            return WorkPostName;
        }
        /// <summary>
        /// 把状态转换代号为文字形式
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertState(object state)
        {
            if (state != null)
            {
                if (state.ToString() == BLL.Const.Shunt_ReCompile)
                {
                    return "重新编制";
                }
                else if (state.ToString() == BLL.Const.Shunt_Compile)
                {
                    return "编制";
                }
                else if (state.ToString() == BLL.Const.Shunt_Audit)
                {
                    return "审核";
                }
                else if (state.ToString() == BLL.Const.Shunt_Complete)
                {
                    return "审批完成";
                }
                else
                {
                    return "";
                }
            }
            return "";
        }
        #endregion
    }
}