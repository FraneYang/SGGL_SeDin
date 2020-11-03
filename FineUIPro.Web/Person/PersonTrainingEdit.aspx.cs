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
            string strSql = @"SELECT Users.UserId,Users.Account,Users.UserCode,Users.Password,Users.UserName,Users.RoleId,Users.UnitId,Users.IsPost,CASE WHEN  Users.IsPost=1 THEN '是' ELSE '否' END AS IsPostName,Users.IdentityCard,Users.Telephone,Users.IsOffice,"
                                       + @"Roles.RoleName,Unit.UnitName,Unit.UnitCode,Depart.DepartName,Users.Major,Users.WorkPostId,PostTitle.PostTitleName,pc.PracticeCertificateName,project.ProjectName,Post.WorkPostName"
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
            var personIds = from x in Funs.DB.View_Person_TrainingPerson where x.TrainingPlanId == this.TrainingPlanId select x.TrainingUserId;
            if (personIds.Count() > 0)
            {
                string userIds = string.Empty;
                foreach (var personId in personIds)
                {
                    userIds += personId + ",";
                }
                strSql += " AND CHARINDEX(Users.UserId,@UserId)>0 ";
                listStr.Add(new SqlParameter("@UserId", userIds));
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            gvPerson.RecordCount = tb.Rows.Count;
            //tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(gvPerson, tb);
            gvPerson.DataSource = table;
            gvPerson.DataBind();
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
        //获取岗位名称
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        protected string ConvertWorkPostName(object WorkPostId)
        {
            string WorkPostName = string.Empty;
            if (WorkPostId != null)
            {
                WorkPostName = BLL.WorkPostService.getWorkPostNamesWorkPostIds(WorkPostId.ToString());
            }
            return WorkPostName;
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