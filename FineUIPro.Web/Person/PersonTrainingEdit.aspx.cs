using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace FineUIPro.Web.Person
{
    public partial class PersonTrainingEdit : PageBase
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


        #region 提交事件


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SaveData(BLL.Const.BtnSubmit);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            var getPlan = BLL.Person_TrainingPlanService.GetPersonTrainingPlanById(TrainingPlanId);
            if (getPlan != null)
            {
                if (this.rdbIsAgree.SelectedValue.Contains("false"))
                {
                    getPlan.State = "0";
                }
                else
                {
                    getPlan.ApproveTime = DateTime.Now;
                    getPlan.State = "2";
                    ///保存员工培训任务书
                    SaveTask();
                }
            }
            ShowNotify("提交成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        /// <summary>
        /// 保存员工培训任务书明细
        /// </summary>
        private void SaveTask()
        {
            var getTrainingPerson = (from x in Funs.DB.Person_TrainingPerson where x.TrainingPlanId == this.TrainingPlanId select x).ToList();
            foreach (var Person in getTrainingPerson)
            {
                var getTrainingCompany = (from y in Funs.DB.Person_TrainingCompany where y.TrainingPlanId == this.TrainingPlanId select y).ToList();
                foreach (var Company in getTrainingCompany)
                {
                    var getCompanyItem = (from z in Funs.DB.Training_CompanyTrainingItem where z.CompanyTrainingId == Company.CompanyTrainingId select z).ToList();
                    foreach (var item in getCompanyItem)
                    {
                        Model.Person_TrainingTask newTask = new Model.Person_TrainingTask
                        {
                            TrainingTaskId = SQLHelper.GetNewID(typeof(Model.Person_TrainingTask)),
                            TrainingPlanId = this.TrainingPlanId,
                            TrainingPersonId = Person.TrainingPersonId,
                            TrainingUserId = Person.TrainingUserId,
                            CompanyTrainingId = item.CompanyTrainingId,
                            CompanyTrainingItemId = item.CompanyTrainingItemId
                        };
                        Funs.DB.Person_TrainingTask.InsertOnSubmit(newTask);
                        Funs.DB.SubmitChanges();
                    }
                }
            }
        }
        #endregion

        protected void rdbIsAgree_SelectedIndexChanged(object sender, EventArgs e)
        {
            var trainingPlan = BLL.Person_TrainingPlanService.GetPersonTrainingPlanById(TrainingPlanId);
            if (this.rdbIsAgree.SelectedValue.Contains("false"))
            {
                this.txtHandelMan.Hidden = false;
                this.txtHandelMan.Text = UserService.GetUserNameByUserId(trainingPlan.CompilePersonId);
                this.txtHandelMan.Label = "打回制定人";

            }
            else
            {

                this.txtHandelMan.Hidden = true;
            }
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