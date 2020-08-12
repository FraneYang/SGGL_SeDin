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
    public partial class PersonTrainingTaskItem : PageBase
    {
        #region 定义项
        /// <summary>
        /// 员工责任主键
        /// </summary>
        public string TrainingPersonId
        {
            get
            {
                return (string)ViewState["TrainingPersonId"];
            }
            set
            {
                ViewState["TrainingPersonId"] = value;
            }

        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.TrainingPersonId = Request.Params["TrainingPersonId"];
                if (!string.IsNullOrEmpty(this.TrainingPersonId))
                {
                    BindGrid();
                }
                    
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>

        public void BindGrid()
        {
           
            string strSql = @"select pt.TrainingTaskId, pt.TrainingPlanId, pt.TrainingPersonId, pt.TrainingUserId, pt.CompanyTrainingId, 
                              pt.CompanyTrainingItemId,U.UserName,ct.CompanyTrainingName,ci.CompanyTrainingItemName,ptp.TrainingPlanTitle 
                              from Person_TrainingTask pt
                              left join Person_TrainingPlan ptp on pt.TrainingPlanId=ptp.TrainingPlanId
                              left join Sys_User U on pt.TrainingUserId=U.UserId
                              left join Training_CompanyTraining ct on pt.CompanyTrainingId=ct.CompanyTrainingId
                              left join Training_CompanyTrainingItem ci on pt.CompanyTrainingItemId=ci.CompanyTrainingItemId where pt.TrainingPersonId=@TrainingPersonId";

            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@TrainingPersonId", this.TrainingPersonId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
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
       
    }
}