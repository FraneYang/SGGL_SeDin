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

namespace FineUIPro.Web.Person
{
    public partial class ShuntEdit : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string ShuntId
        {
            get
            {
                return (string)ViewState["ShuntId"];
            }
            set
            {
                ViewState["ShuntId"] = value;
            }
        }
        /// <summary>
        /// 办理类型
        /// </summary>
        public string State
        {
            get
            {
                return (string)ViewState["State"];
            }
            set
            {
                ViewState["State"] = value;
            }
        }
        /// <summary>
        /// 明细集合
        /// </summary>
        private List<Model.Person_ShuntDetail> details = new List<Model.Person_ShuntDetail>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                details.Clear();
                BLL.ProjectService.InitAllProjectDropDownList(this.drpProject, true);
                ShuntId = Request.Params["ShuntId"];
                plApprove1.Hidden = true;
                plApprove2.Hidden = true;
                rblIsAgree.Hidden = true;
                rblIsAgree.SelectedValue = "true";
                if (!string.IsNullOrEmpty(ShuntId))
                {
                    plApprove1.Hidden = false;
                    plApprove2.Hidden = false;
                    var dt = Person_ShuntApproveService.getListData(ShuntId);
                    gvApprove.DataSource = dt;
                    gvApprove.DataBind();
                    Model.Person_Shunt shunt = Person_ShuntService.GetShunt(ShuntId);
                    this.txtCode.Text = shunt.Code;
                    if (!string.IsNullOrEmpty(shunt.ProjectId))
                    {
                        this.drpProject.SelectedValue = shunt.ProjectId;
                    }
                    if (shunt.CompileDate != null)
                    {
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", shunt.CompileDate);
                    }
                    if (!string.IsNullOrEmpty(shunt.State))
                    {
                        State = shunt.State;
                    }
                    else
                    {
                        State = BLL.Const.Shunt_Compile;
                        this.rblIsAgree.Hidden = false;
                    }
                    if (State != BLL.Const.Shunt_Complete)
                    {
                        Person_ShuntService.Init(drpHandleType, State, false);
                        UserService.InitSGBUser(drpHandleMan, false);
                    }
                    if (State == BLL.Const.Shunt_Compile)
                    {
                        this.rblIsAgree.Hidden = true;
                    }
                    else
                    {
                        this.rblIsAgree.Hidden = false;
                    }
                    HandleMan();
                    details = BLL.Person_ShuntDetailService.GetLists(ShuntId);
                    this.Grid1.DataSource = details;
                    this.Grid1.DataBind();
                }
                else
                {
                    this.txtCode.Text = BLL.SQLHelper.RunProcNewId("SpGetNewCode3", "dbo.Person_Shunt", "Code", string.Empty);
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    State = BLL.Const.Shunt_Compile;
                    Person_ShuntService.Init(drpHandleType, State, false);
                    UserService.InitSGBUser(drpHandleMan, false);
                }
                Model.Person_Shunt shunt1 = Person_ShuntService.GetShunt(ShuntId);
                if (shunt1 != null && !string.IsNullOrEmpty(shunt1.SaveHandleMan))
                {
                    this.drpHandleMan.SelectedValue = shunt1.SaveHandleMan;
                }
            }
        }

        /// <summary>
        /// 关闭弹出窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            details = GetDetails();
            if (!string.IsNullOrEmpty(this.hdIds.Text))
            {
                string[] ids = this.hdIds.Text.Split(',');
                foreach (var id in ids)
                {
                    string[] strs = id.Split('|');
                    var d = details.FirstOrDefault(x=>x.UserId== strs[0]);
                    if (d == null)
                    {
                        Model.Person_ShuntDetail detail = new Model.Person_ShuntDetail();
                        detail.ShuntDetailId = SQLHelper.GetNewID();
                        detail.UserId = strs[0];
                        detail.WorkPostId = strs[1];
                        details.Add(detail);
                    }
                }
            }
            this.Grid1.DataSource = details;
            this.Grid1.DataBind();
        }

        private List<Model.Person_ShuntDetail> GetDetails()
        {
            details.Clear();
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                Model.Person_ShuntDetail detail = new Model.Person_ShuntDetail();
                detail.ShuntDetailId = Grid1.Rows[i].DataKeys[0].ToString();
                detail.UserId = Grid1.Rows[i].DataKeys[1].ToString();
                detail.WorkPostId = Grid1.Rows[i].DataKeys[2].ToString();
                details.Add(detail);
            }
            return details;
        }

        #region Grid行点击事件
        /// <summary>
        /// Grid行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            details = GetDetails();
            string id = this.Grid1.SelectedRow.RowID;
            if (e.CommandName == "del")//删除
            {
                var d = details.FirstOrDefault(x => x.ShuntDetailId == id);
                if (d != null)
                {
                    details.Remove(d);
                }
                this.Grid1.DataSource = details;
                this.Grid1.DataBind();
            }
        }
        #endregion

        #region 增加历史记录
        /// <summary>
        /// 增加历史记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            string openUrl = String.Format("ShuntDetailEdit.aspx?ids={0}", this.hdIds.Text, "编辑 - ");
            PageContext.RegisterStartupScript(Window1.GetSaveStateReference(hdIds.ClientID)
                    + Window1.GetShowReference(openUrl));
            //PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ShuntDetailEdit.aspx", "编辑 - ")));
        }
        #endregion

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
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("RoleItemEdit.aspx?roleItemId={0}", Id, "编辑 - ")));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //string projectId, string userId, string menuId, string buttonName)
            if (BLL.CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.PersonShuntMenuId, BLL.Const.BtnSave))
            {
                SavePauseNotice("save");
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());

            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.PersonShuntMenuId, BLL.Const.BtnSubmit))
            {
                if (this.drpProject.SelectedValue == BLL.Const._Null)
                {
                    Alert.ShowInTop("请选择项目！", MessageBoxIcon.Warning);
                    return;
                }
                SavePauseNotice("submit");
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        #region 保存处理
        /// <summary>
        /// 保存方法
        /// </summary>
        private void SavePauseNotice(string saveType)
        {
            Model.Person_Shunt shunt = new Model.Person_Shunt();
            shunt.Code = this.txtCode.Text.Trim();
            if (this.drpProject.SelectedValue != BLL.Const._Null)
            {
                shunt.ProjectId = this.drpProject.SelectedValue;
            }
            if (!string.IsNullOrEmpty(this.txtCompileDate.Text.Trim()))
            {
                shunt.CompileDate = Convert.ToDateTime(this.txtCompileDate.Text.Trim());
            }
            if (saveType == "submit")
            {
                shunt.State = drpHandleType.SelectedValue.Trim();
            }
            else
            {
                Model.Person_Shunt shunt1 = BLL.Person_ShuntService.GetShunt(ShuntId);
                if (shunt1 != null)
                {
                    if (string.IsNullOrEmpty(shunt1.State))
                    {
                        shunt.State = BLL.Const.Shunt_Compile;
                    }
                    else
                    {
                        shunt.State = shunt1.State;
                    }
                }
                else
                {
                    shunt.State = BLL.Const.Shunt_Compile;
                }
            }
            if (!string.IsNullOrEmpty(ShuntId))
            {
                Model.Person_ShuntApprove approve1 = BLL.Person_ShuntApproveService.GetShuntApproveByShuntId(ShuntId);
                if (approve1 != null && saveType == "submit")
                {
                    approve1.ApproveDate = DateTime.Now;
                    approve1.ApproveIdea = txtOpinions.Text.Trim();
                    approve1.IsAgree = Convert.ToBoolean(this.rblIsAgree.SelectedValue);
                    BLL.Person_ShuntApproveService.UpdateShuntApprove(approve1);
                }
                if (saveType == "submit")
                {
                    shunt.SaveHandleMan = null;
                    Model.Person_ShuntApprove approve = new Model.Person_ShuntApprove();
                    approve.ShuntId = ShuntId;
                    if (this.drpHandleMan.SelectedValue != "0")
                    {
                        approve.ApproveMan = this.drpHandleMan.SelectedValue;
                    }
                    approve.ApproveType = this.drpHandleType.SelectedValue;
                    if (this.drpHandleType.SelectedValue == BLL.Const.CheckControl_Complete)
                    {
                        approve.ApproveDate = DateTime.Now.AddMinutes(1);
                    }
                    BLL.Person_ShuntApproveService.AddShuntApprove(approve);
                }
                if (saveType == "save")
                {
                    shunt.SaveHandleMan = this.drpHandleMan.SelectedValue;
                }
                shunt.ShuntId = ShuntId;
                BLL.Person_ShuntService.UpdateShunt(shunt);
            }
            else
            {
                shunt.CompileMan = this.CurrUser.UserId;
                if (saveType == "save")
                {
                    shunt.SaveHandleMan = this.drpHandleMan.SelectedValue;
                }
                shunt.ShuntId = SQLHelper.GetNewID(typeof(Model.Person_Shunt));
                BLL.Person_ShuntService.AddShunt(shunt);
                if (saveType == "submit")
                {
                    Model.Person_ShuntApprove approve1 = new Model.Person_ShuntApprove();
                    approve1.ShuntId = shunt.ShuntId;
                    approve1.ApproveDate = DateTime.Now;
                    approve1.ApproveMan = this.CurrUser.UserId;
                    approve1.ApproveType = BLL.Const.CheckControl_Compile;
                    BLL.Person_ShuntApproveService.AddShuntApprove(approve1);

                    Model.Person_ShuntApprove approve = new Model.Person_ShuntApprove();
                    approve.ShuntId = shunt.ShuntId;
                    if (this.drpHandleMan.SelectedValue != "0")
                    {
                        approve.ApproveMan = this.drpHandleMan.SelectedValue;
                    }
                    approve.ApproveType = this.drpHandleType.SelectedValue;

                    BLL.Person_ShuntApproveService.AddShuntApprove(approve);
                }
                else
                {
                    Model.Person_ShuntApprove approve1 = new Model.Person_ShuntApprove();
                    approve1.ShuntId = shunt.ShuntId;
                    approve1.ApproveMan = this.CurrUser.UserId;
                    approve1.ApproveType = BLL.Const.CheckControl_Compile;
                    BLL.Person_ShuntApproveService.AddShuntApprove(approve1);
                }
            }
            BLL.Person_ShuntDetailService.DeleteShuntDetailByShuntId(shunt.ShuntId);
            details = GetDetails();
            foreach (var d in details)
            {
                d.ShuntId = shunt.ShuntId;
                BLL.Person_ShuntDetailService.AddShuntDetail(d);
                if (this.drpHandleType.SelectedValue == BLL.Const.Shunt_Complete)   //审批完成，生成项目用户
                {
                    var projectUser = BLL.ProjectUserService.GetProjectUserByUserIdProjectId(shunt.ProjectId, d.UserId);
                    if (projectUser == null)
                    {
                        var user = BLL.UserService.GetUserByUserId(d.UserId);
                        if (user != null)
                        {
                            Model.Project_ProjectUser newProjectUser = new Model.Project_ProjectUser
                            {
                                ProjectId = shunt.ProjectId,
                                UserId = d.UserId,
                                UnitId = user.UnitId,
                                RoleId = user.RoleId,
                                IsPost = true
                            };
                            BLL.ProjectUserService.AddProjectUser(newProjectUser);
                            Model.Sys_RoleItem roleItem = new Model.Sys_RoleItem();
                            roleItem.ProjectId = shunt.ProjectId;
                            roleItem.UserId = d.UserId;
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
                                        IsUsed = true
                                    };
                                    BLL.PersonService.AddPerson(newPerson);
                                }
                            }
                        }
                    }
                }
            }

            BLL.LogService.AddSys_Log(this.CurrUser, shunt.Code, shunt.ShuntId, BLL.Const.PersonShuntMenuId, "编辑分流管理");
        }
        #endregion

        protected void rblIsAgree_SelectedIndexChanged(object sender, EventArgs e)
        {
            HandleType();
        }

        /// <summary>
        /// 待办事项的下拉框的处理
        /// </summary>
        public void HandleType()
        {

            drpHandleType.Items.Clear();
            //Funs.Bind(drpHandleType, CheckControlService.GetDHandleTypeByState(State));
            Person_ShuntService.Init(drpHandleType, State, false);
            string res = null;
            List<string> list = new List<string>();
            list.Add(Const.CheckControl_ReCompile);
            list.Add(Const.CheckControl_ReCompile2);
            var count = drpHandleType.Items.Count;
            List<ListItem> listitem = new List<ListItem>();
            if (rblIsAgree.SelectedValue.Equals("true"))
            {
                for (int i = 0; i < count; i++)
                {
                    res = drpHandleType.Items[i].Value;
                    if (list.Contains(res))
                    {
                        var item = (drpHandleType.Items[i]);
                        listitem.Add(item);
                    }
                }
                if (listitem.Count > 0)
                {
                    for (int i = 0; i < listitem.Count; i++)
                    {
                        drpHandleType.Items.Remove(listitem[i]);
                    }
                }
            }
            else
            {

                for (int i = 0; i < count; i++)
                {

                    res = drpHandleType.Items[i].Value;
                    if (!list.Contains(res))
                    {
                        var item = drpHandleType.Items[i];
                        listitem.Add(item);
                    }
                }
                if (listitem.Count > 0)
                {
                    for (int i = 0; i < listitem.Count; i++)
                    {
                        drpHandleType.Items.Remove(listitem[i]);
                    }
                }


            }
            if (count > 0)
            {
                drpHandleType.SelectedIndex = 0;
                HandleMan();
            }
        }

        /// <summary>
        /// 办理人员的自动筛选
        /// </summary>
        protected void HandleMan()
        {
            drpHandleMan.Items.Clear();
            if (!string.IsNullOrEmpty(drpHandleType.SelectedText))
            {
                if (drpHandleMan.Items.Count > 0)
                {
                    drpHandleMan.SelectedIndex = 0;
                }
                if (drpHandleType.SelectedText.Contains("重新编制"))
                {
                    UserService.InitSGBUser(drpHandleMan, false);
                    var HandleMan = BLL.Person_ShuntApproveService.GetComplie(this.ShuntId);
                    if (HandleMan != null)
                    {
                        this.drpHandleMan.SelectedValue = HandleMan.ApproveMan;
                    }
                }
                if (drpHandleType.SelectedValue == BLL.Const.Shunt_Complete)
                {
                    drpHandleMan.Items.Clear();
                    drpHandleMan.Enabled = false;
                    drpHandleMan.Required = false;
                }
                else
                {
                    drpHandleMan.Enabled = true;
                    drpHandleMan.Required = true;
                }
            }
        }

        #region 转换字符串
        //<summary>
        //获取姓名
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        protected string ConvertUserName(object UserId)
        {
            string UserName = string.Empty;
            if (UserId != null)
            {
                var user = BLL.UserService.GetUserByUserId(UserId.ToString());
                if (user != null)
                {
                    UserName = user.UserName;
                }
            }
            return UserName;
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
        //获取职称名称
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        protected string ConvertPostTitleName(object UserId)
        {
            string PostTitleName = string.Empty;
            if (UserId != null)
            {
                var user = BLL.UserService.GetUserByUserId(UserId.ToString());
                if (user != null && !string.IsNullOrEmpty(user.PostTitleId))
                {
                    var postTitle = BLL.PostTitleService.GetPostTitleById(user.PostTitleId);
                    if (postTitle != null)
                    {
                        PostTitleName = postTitle.PostTitleName;
                    }
                }
            }
            return PostTitleName;
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