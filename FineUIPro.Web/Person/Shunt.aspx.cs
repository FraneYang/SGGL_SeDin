using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.Person
{
    public partial class Shunt : PageBase
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
                Funs.DropDownPageSize(this.ddlPageSize);
                BLL.ProjectService.InitProjectDropDownList(this.drpProject, true);
                ////权限按钮方法
                this.GetButtonPower();
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                }
                WorkPostService.InitWorkPostNameByTypeDropDownList2(this.drpWorkPost, "1", true);  //加载管理岗位
                WorkPostService.InitWorkPostNameByTypeDropDownList(this.drpWP, "1", false);  //加载管理岗位
                PostTitleService.InitPostTitleDropDownList(this.drpPostTitle, true);
                PracticeCertificateService.InitPracticeCertificateDropDownList(this.drpCertificate, true);
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
            string strSql = @"SELECT Users.UserId,Users.Account,Users.UserCode,Users.Password,Users.UserName,Users.RoleId,Users.UnitId,Users.IsPost,CASE WHEN  Users.IsPost=1 THEN '是' ELSE '否' END AS IsPostName,Users.IdentityCard,Users.Telephone,Users.IsOffice,"
                                     + @"Roles.RoleName,Unit.UnitName,Unit.UnitCode,Depart.DepartName,Users.Major,PostTitle.PostTitleName,pc.PracticeCertificateName,project.ProjectName,Post.WorkPostName"
                                     + @",ProjectRoleName= STUFF(( SELECT ',' + RoleName FROM dbo.Sys_Role where PATINDEX('%,' + RTRIM(RoleId) + ',%',',' +Users.ProjectRoleId + ',')>0 FOR XML PATH('')), 1, 1,'')"
                                     + @" From dbo.Sys_User AS Users"
                                     + @" LEFT JOIN Sys_Role AS Roles ON Roles.RoleId=Users.RoleId"
                                     + @" LEFT JOIN Base_Unit AS Unit ON Unit.UnitId=Users.UnitId"
                                     + @" LEFT JOIN Base_WorkPost AS Post ON Post.WorkPostId=Users.WorkPostId"
                                     + @" LEFT JOIN Base_Depart AS Depart ON Depart.DepartId=Users.DepartId"
                                     + @" LEFT JOIN Base_PostTitle AS PostTitle ON PostTitle.PostTitleId=Users.PostTitleId"
                                     + @" LEFT JOIN Base_PracticeCertificate AS pc ON pc.PracticeCertificateId=Users.CertificateId"
                                     + @" LEFT JOIN Base_Project AS project ON project.projectId=Users.ProjectId"
                                     + @" WHERE Users.UserId !='" + Const.sysglyId + "' AND Users.UserId !='" + Const.hfnbdId + "' AND  Users.UserId !='" + Const.sedinId + "' AND Unit.UnitId='" + Const.UnitId_SEDIN + "' AND Users.DepartId='" + Const.Depart_constructionId + "' AND Users.IsPost =1 ";
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

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            //tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
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

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.PersonShuntMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSure.Hidden = false;
                }
            }
        }
        #endregion

        #region  删除数据
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
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                string strShowNotify = string.Empty;
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    BLL.Person_ShuntDetailService.DeleteShuntDetailByShuntId(rowID);
                    BLL.Person_ShuntApproveService.DeleteShuntApprovesByShuntId(rowID);
                    BLL.Person_ShuntService.DeleteShunt(rowID);
                }
                BindGrid();
                if (!string.IsNullOrEmpty(strShowNotify))
                {
                    Alert.ShowInTop(strShowNotify, MessageBoxIcon.Warning);
                }
                else
                {
                    ShowNotify("删除数据成功!", MessageBoxIcon.Success);
                }
            }
        }
        #endregion

        /// <summary>
        /// 关闭弹出窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }

        #region 分页
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
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
            if (this.drpProject.SelectedValue != BLL.Const._Null)
            {
                bool b = true;
                foreach (JObject mergedRow in Grid1.GetMergedData())
                {
                    JObject values = mergedRow.Value<JObject>("values");
                    int i = mergedRow.Value<int>("index");
                    if (this.Grid1.SelectedRowIndexArray.Contains(i))
                    {
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
                }
                if (!b)
                {
                    Alert.ShowInTop("勾选人员的拟聘岗位不能为空！", MessageBoxIcon.Warning);
                    return;
                }
                Model.Person_Shunt shunt = new Model.Person_Shunt();
                shunt.Code = BLL.SQLHelper.RunProcNewId("SpGetNewCode3", "dbo.Person_Shunt", "Code", string.Empty);
                shunt.ProjectId = this.drpProject.SelectedValue;
                shunt.State = BLL.Const.Shunt_Complete;
                shunt.CompileDate = DateTime.Now;
                shunt.CompileMan = this.CurrUser.UserId;
                shunt.ShuntId = SQLHelper.GetNewID(typeof(Model.Person_Shunt));
                BLL.Person_ShuntService.AddShunt(shunt);
                var workPosts = BLL.WorkPostService.GetWorkPostList();
                foreach (JObject mergedRow in Grid1.GetMergedData())
                {
                    JObject values = mergedRow.Value<JObject>("values");
                    int i = mergedRow.Value<int>("index");
                    AspNet.CheckBox cb = (AspNet.CheckBox)(this.Grid1.Rows[i].FindControl("cbSelect"));
                    if (cb.Checked)   //选择项
                    {
                        Model.Person_ShuntDetail detail = new Model.Person_ShuntDetail();
                        detail.ShuntDetailId = SQLHelper.GetNewID(typeof(Model.Person_ShuntDetail));
                        detail.ShuntId = shunt.ShuntId;
                        detail.UserId = this.Grid1.Rows[i].RowID;
                        string workPost = values.Value<string>("WorkPost");
                        var w = workPosts.FirstOrDefault(x => x.WorkPostName == workPost);
                        if (w != null)
                        {
                            detail.WorkPostId = w.WorkPostId;
                        }
                        detail.SortIndex = i;
                        BLL.Person_ShuntDetailService.AddShuntDetail(detail);
                        var currProjectUser = BLL.ProjectUserService.GetCurrProjectUserByUserId(detail.UserId);
                        if (currProjectUser != null)
                        {
                            currProjectUser.IsPost = false;
                            BLL.ProjectUserService.UpdateProjectUser(currProjectUser);
                        }
                        var projectUser = BLL.ProjectUserService.GetProjectUserByUserIdProjectId(shunt.ProjectId, detail.UserId);
                        if (projectUser == null)
                        {
                            var user = BLL.UserService.GetUserByUserId(detail.UserId);
                            if (user != null)
                            {
                                user.ProjectId = this.drpProject.SelectedValue;
                                user.WorkPostId = detail.WorkPostId;
                                BLL.UserService.UpdateUser(user);
                                Model.Project_ProjectUser newProjectUser = new Model.Project_ProjectUser
                                {
                                    ProjectId = shunt.ProjectId,
                                    UserId = detail.UserId,
                                    UnitId = user.UnitId,
                                    RoleId = user.RoleId,
                                    IsPost = true
                                };
                                BLL.ProjectUserService.AddProjectUser(newProjectUser);
                                Model.Sys_RoleItem roleItem = new Model.Sys_RoleItem();
                                roleItem.ProjectId = shunt.ProjectId;
                                roleItem.UserId = detail.UserId;
                                roleItem.RoleId = user.RoleId;
                                roleItem.IntoDate = DateTime.Now;
                                BLL.RoleItemService.AddRoleItem(roleItem);
                                if (!string.IsNullOrEmpty(user.IdentityCard))
                                {
                                    ///当前用户是否已经添加到项目现场人员中
                                    var sitePerson = BLL.PersonService.GetPersonByIdentityCard(shunt.ProjectId, user.IdentityCard);
                                    if (sitePerson == null)
                                    {
                                        Model.SitePerson_Person newPerson = new Model.SitePerson_Person
                                        {
                                            PersonId = SQLHelper.GetNewID(typeof(Model.SitePerson_Person)),
                                            PersonName = user.UserName,
                                            Sex = user.Sex,
                                            IdentityCard = user.IdentityCard,
                                            ProjectId = shunt.ProjectId,
                                            UnitId = user.UnitId,
                                            IsUsed = true,
                                            WorkPostId = detail.WorkPostId,
                                        };
                                        BLL.PersonService.AddPerson(newPerson);
                                    }
                                }
                            }
                        }
                    }
                }
                ShowNotify("拟聘成功！", MessageBoxIcon.Success);
                BindGrid();
                BLL.LogService.AddSys_Log(this.CurrUser, shunt.Code, shunt.ShuntId, BLL.Const.PersonShuntMenuId, "编辑分流管理");
            }
            else
            {
                Alert.ShowInParent("请选择拟聘项目！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnView_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ShuntList.aspx", "编辑 - ")));
        }

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
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string Id = Grid1.SelectedRowID;
            var shunt = BLL.Person_ShuntService.GetShunt(Id);

            if (shunt != null)
            {
                if (shunt.State.Equals(Const.Shunt_Complete))
                {
                    Alert.ShowInTop("您不是当前办理人，无法编辑,请右键查看！", MessageBoxIcon.Warning);
                    return;
                }
                Model.Person_ShuntApprove approve = BLL.Person_ShuntApproveService.GetShuntApproveByShuntId(Id);
                if (approve != null)
                {
                    if (!string.IsNullOrEmpty(approve.ApproveMan))
                    {
                        if (this.CurrUser.UserId == approve.ApproveMan || CurrUser.UserId == Const.sysglyId)
                        {
                            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ShuntEdit.aspx?ShuntId={0}", Id, "编辑 - ")));
                            return;
                        }
                        else if (shunt.State == BLL.Const.Shunt_Complete)
                        {
                            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ShuntView.aspx?ShuntId={0}", Id, "查看 - ")));
                            return;
                        }
                        else
                        {
                            Alert.ShowInTop("您不是当前办理人，无法编辑,请右键查看！", MessageBoxIcon.Warning);
                            return;
                        }

                    }
                    //if (this.btnMenuModify.Hidden || checks.State == BLL.Const.State_2)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
                    //{
                    //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckListView.aspx?CheckControlCode={0}", codes, "查看 - ")));
                    //    return;
                    //}
                    //else
                    //{
                    //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckListEdit.aspx?CheckControlCode={0}", codes, "编辑 - ")));
                    //    return;
                    //}

                }
                else
                {
                    Alert.ShowInTop("您不是当前办理人，无法编辑,请右键查看！", MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }

        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string Id = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ShuntView.aspx?ShuntId={0}", Id, "查看 - ")));
        }

        #region 判断是否可删除
        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private string judgementDelete(string id)
        {
            string content = string.Empty;
            if (Funs.DB.Project_ProjectUser.FirstOrDefault(x => x.UserId == id) != null)
            {
                content += "已在【项目员工】中使用，不能删除！";
            }

            return content;
        }
        #endregion

        /// <summary>
        /// 关闭导入弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }

        protected void ckbAll_CheckedChanged(object sender, CheckedEventArgs e)
        {
            BindGrid();
        }

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

        //<summary>
        //获取办理人姓名
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        protected string ConvertMan(object ShuntId)
        {
            if (ShuntId != null)
            {
                Model.Person_ShuntApprove a = BLL.Person_ShuntApproveService.GetShuntApproveByShuntId(ShuntId.ToString());
                if (a != null)
                {
                    if (a.ApproveMan != null)
                    {
                        return BLL.UserService.GetUserByUserId(a.ApproveMan).UserName;
                    }
                }
                else
                {
                    return "";
                }
            }
            return "";
        }
    }
}