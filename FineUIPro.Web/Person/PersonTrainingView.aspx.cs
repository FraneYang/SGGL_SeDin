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
                        this.hdTrainingPlanId.Text = this.TrainingPlanId;
                        BindGrid();
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
        #endregion


        protected void MenuView_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PersonTrainingTaskItem.aspx?TrainingPersonId={0}", gvPerson.SelectedRowID, "操作 - ")));
        }

        #region 附件上传
        /// <summary>
        /// 上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            string edit = "-1";
            DateTime date = DateTime.Now;
            if (!string.IsNullOrEmpty(this.txtStartTime.Text.Trim()))
            {
                date = Convert.ToDateTime(this.txtStartTime.Text.Trim());
            }
            string dateStr = date.Year.ToString() + date.Month.ToString();
            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PersonTraining/" + dateStr + "&menuId={1}&type={2}", this.hdTrainingPlanId.Text, Const.PersonTrainingMenuId, edit)));
        }

        #endregion
    }
}