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
    public partial class PersonChecking : PageBase
    {
        /// <summary>
        /// 绩效表主键
        /// </summary>
        public string QuarterCheckId
        {
            get
            {
                return (string)ViewState["QuarterCheckId"];
            }
            set
            {
                ViewState["QuarterCheckId"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.QuarterCheckId = Request.Params["QuarterCheckId"];
                Model.Person_QuarterCheck getCheck = BLL.Person_QuarterCheckService.GetPerson_QuarterCheckById(this.QuarterCheckId);
                if (getCheck != null)
                {
                    this.txtCheckedMan.Text = BLL.UserService.GetUserByUserId(getCheck.UserId).UserName;
                    this.txtRoleName.Text = BLL.RoleService.GetRoleByRoleId(getCheck.RoleId).RoleName;
                    this.txtProject.Text = BLL.ProjectService.GetProjectNameByProjectId(getCheck.ProjectId);
                    if (string.IsNullOrEmpty(this.txtProject.Text))
                    {
                        this.txtProject.Hidden = true;
                    }
                }

                BindGrid();

            }
        }
        private void BindGrid()
        {
            string strSql = @"select QuarterCheckItemId, QuarterCheckId, C.UserId, TargetClass2, CheckContent,'100' AS standardGrade,                   Grade,TargetClass1,SortId from [dbo].[Person_QuarterCheckItem] C
                              left join Sys_User U on C.UserId=U.UserId where QuarterCheckId=@QuarterCheckId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@QuarterCheckId", this.QuarterCheckId));
            if (this.CurrUser.UserId == BLL.Const.SGGLB)
            {
                strSql += " AND C.UserId=@UserId";
                listStr.Add(new SqlParameter("@UserId", this.CurrUser.UserId));
            }
            else
            {
                var RoleId = BLL.UserService.GetUserByUserId(this.CurrUser.UserId).RoleId;
                if (RoleId == BLL.Const.ProjectManager)
                {
                    strSql += " AND U.RoleId=@RoleId";
                    listStr.Add(new SqlParameter("@RoleId", BLL.Const.ProjectManager));
                }
                else if (RoleId == BLL.Const.QAManager)
                {
                    strSql += " AND U.RoleId=@RoleId";
                    listStr.Add(new SqlParameter("@RoleId", BLL.Const.QAManager));
                }
                else if (RoleId == BLL.Const.HSSEManager)
                {
                    strSql += " AND U.RoleId=@RoleId";
                    listStr.Add(new SqlParameter("@RoleId", BLL.Const.HSSEManager));
                }
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Grid1.GetMergedData().Count > 0)
            {
                foreach (JObject mergedRow in Grid1.GetMergedData())
                {
                    int i = mergedRow.Value<int>("index");
                    string id = Grid1.Rows[i].RowID;
                    JObject values = mergedRow.Value<JObject>("values");
                    Model.Person_QuarterCheckItem Item = BLL.Person_QuarterCheckItemService.GetCheckItemById(id);
                    if (Item != null)
                    {
                        Item.Grade = values.Value<decimal>("Grade").ToString() == "" ? 0 : values.Value<decimal>("Grade");
                    }
                    BLL.Person_QuarterCheckItemService.UpdateCheckItem(Item);
                }
                Model.Person_QuarterCheckApprove getApprove = BLL.Person_QuarterCheckApproveService.GetApproveByQuarterCheckIdUserId(this.QuarterCheckId, this.CurrUser.UserId);
                if (getApprove != null)
                {
                    getApprove.ApproveDate = DateTime.Now;
                }
                BLL.Person_QuarterCheckApproveService.UpdateCheckApprove(getApprove);
                List<Model.Person_QuarterCheckApprove> Approve = BLL.Person_QuarterCheckApproveService.GetApproveByQuarterCheckId(this.QuarterCheckId);
                if (Approve.Count == 0)
                {
                    Model.Person_QuarterCheck Construct = BLL.Person_QuarterCheckService.GetPerson_QuarterCheckById(this.QuarterCheckId);
                    if (Construct != null)
                    {
                        Construct.State = "1";
                        BLL.Person_QuarterCheckService.UpdatePerson_QuarterCheck(Construct);
                    }
                }

                ShowNotify("提交成功！", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }

    }
}