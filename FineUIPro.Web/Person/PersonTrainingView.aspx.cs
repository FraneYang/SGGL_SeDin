using BLL;
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
    public partial class PersonTrainingView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 员工责任主键
        /// </summary>
        public string TrainingPlanId
        {
            get
            {
                return (string)ViewState["TrainingPlanId"];
            }
            set
            {
                ViewState["TrainingPlanId"] = value;
            }

        }
        #endregion

        #region 加载

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.TrainingPlanId = Request.Params["TrainingPlanId"];
                if (!string.IsNullOrEmpty(this.TrainingPlanId))
                {
                    var TrainingPlan = BLL.Person_TrainingPlanService.GetPersonTrainingPlanById(this.TrainingPlanId);
                    if (TrainingPlan != null)
                    {
                        BindGrid();
                        BindGrid1();
                        if (!string.IsNullOrEmpty(TrainingPlan.TrainingPlanCode))
                        {
                            this.txtTrainingPlanCode.Text = TrainingPlan.TrainingPlanCode;
                        }
                        if (!string.IsNullOrEmpty(TrainingPlan.TrainingPlanTitle))
                        {
                            this.txtTrainingPlanTitle.Text = TrainingPlan.TrainingPlanTitle;
                        }
                        if (!string.IsNullOrEmpty(TrainingPlan.TrainingPlanContent))
                        {
                            this.txtTrainingPlanContent.Text = TrainingPlan.TrainingPlanContent;
                        }
                        if (TrainingPlan.StartTime.HasValue)
                        {
                            this.txtStartTime.Text = string.Format("{0:yyyy-MM-dd}", TrainingPlan.StartTime);
                        }
                        if (TrainingPlan.EndTime.HasValue)
                        {
                            this.txtEndTime.Text = string.Format("{0:yyyy-MM-dd}", TrainingPlan.EndTime);
                        }
                    }
                }
            }
        }

        private void BindGrid()
        {
            string sqlStr = "select * from View_Person_TrainingPerson where TrainingPlanId=@TrainingPlanId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@TrainingPlanId", this.TrainingPlanId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(sqlStr, parameter);
            gvPerson.DataSource = GetPagedDataTable(gvPerson, tb);
            gvPerson.DataBind();
        }
        private void BindGrid1()
        {
            string sqlStr = "select * from Person_TrainingCompany T left join Training_CompanyTraining C on T.CompanyTrainingId=C.CompanyTrainingId where TrainingPlanId=@TrainingPlanId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@TrainingPlanId", this.TrainingPlanId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb1 = SQLHelper.GetDataTableRunText(sqlStr, parameter);
            gvCompany.DataSource = GetPagedDataTable(gvCompany, tb1);
            gvCompany.DataBind();
        }
        #endregion
        

        protected void MenuView_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PersonTrainingTaskItem.aspx?TrainingPersonId={0}", gvPerson.SelectedRowID, "操作 - ")));
        }
    }
}