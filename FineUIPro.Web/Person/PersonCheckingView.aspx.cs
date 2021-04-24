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
    public partial class PersonCheckingView : PageBase
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
                    if (string.IsNullOrEmpty(this.txtProject.Text)) {
                        this.txtProject.Hidden = true;
                    }
                }
                BindGrid();

                this.OutputSummaryData(); ///取合计值

            }
        }
        private void BindGrid()
        {
            string strSql = @"select QuarterCheckItemId, QuarterCheckId, C.UserId, TargetClass2, CheckContent, Grade,TargetClass1,SortId from [dbo].[Person_QuarterCheckItem] C
                              left join Sys_User U on C.UserId=U.UserId where QuarterCheckId=@QuarterCheckId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@QuarterCheckId", this.QuarterCheckId));
            Model.Person_QuarterCheck getCheck = BLL.Person_QuarterCheckService.GetPerson_QuarterCheckById(this.QuarterCheckId);
            if (getCheck.UserId == this.CurrUser.UserId || this.CurrUser.UserId == BLL.Const.sysglyId || this.CurrUser.UserId == BLL.Const.hfnbdId)
            {

            }
            else
            {
                strSql += " AND C.UserId=@UserId";
                listStr.Add(new SqlParameter("@UserId", this.CurrUser.UserId));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
            
        }
        /// <summary>
        /// 计算合计
        /// </summary>
        private void OutputSummaryData()
        {
            decimal totalValue = 0;
            JArray mergedData = Grid1.GetMergedData();
            if (mergedData.Count() > 0)
            {
                foreach (JObject mergedRow in mergedData)
                {
                    int i = mergedRow.Value<int>("index");
                    JObject values = mergedRow.Value<JObject>("values");
                    System.Web.UI.WebControls.Label txtGrade = (System.Web.UI.WebControls.Label)Grid1.Rows[i].FindControl("txtGrade");
                    if (!string.IsNullOrEmpty(txtGrade.Text))
                    {
                        totalValue += Convert.ToDecimal(txtGrade.Text);
                    }
                }

                JObject summary = new JObject();
                summary.Add("CheckContent", "合计");
                summary.Add("Grade", totalValue.ToString());
                Grid1.SummaryData = summary;
            }
        }
        protected string ConvertGrade(object ConstructItemId)
        {
            decimal result = 12.30M;
            if (!string.IsNullOrEmpty(ConstructItemId.ToString()))
            {
                Model.Person_QuarterCheckItem Item = BLL.Person_QuarterCheckItemService.GetCheckItemById(ConstructItemId.ToString());
                if (Item != null) {
                   // decimal ConstructItem = BLL.Person_QuarterCheckItemService.GetCheckItemListById(Item.QuarterCheckId,Item.UserId);
                    var a = Convert.ToDouble(Convert.ToDouble(Item.Grade) * (Convert.ToDouble(Item.StandardGrade) / 100));
                    result = decimal.Round(decimal.Parse(a.ToString()), 1);
                }
                
                
                return result.ToString();
            }
            return "";
        }
    }
}