﻿using System;
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
    public partial class PersonTrainingAdd : PageBase
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
                UserService.InitUserUnitIdDepartIdDropDownList(drpHandleMan, Const.UnitId_SEDIN, Const.Depart_constructionId, true);
                WorkPostService.InitWorkPostNameByTypeDropDownList2(this.drpWorkPost, "1", true);  //加载管理岗位
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
                else
                {
                    BindGrid();
                    this.txtTrainingPlanTitle.Text = "人员培训计划";
                    this.txtTrainingPlanCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.PersonTrainingMenuId, this.CurrUser.LoginProjectId, this.CurrUser.UnitId);
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
            if (this.drpWorkPost.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND CHARINDEX(@WorkPostId,Users.WorkPostId)>0 ";
                listStr.Add(new SqlParameter("@WorkPostId", this.drpWorkPost.SelectedValue));
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            gvPerson.RecordCount = tb.Rows.Count;
            //tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(gvPerson, tb);
            gvPerson.DataSource = table;
            gvPerson.DataBind();
            var persons = from x in Funs.DB.View_Person_TrainingPerson where x.TrainingPlanId == this.TrainingPlanId select x;
            if (persons.Count() > 0)
            {
                for (int i = 0; i < this.gvPerson.Rows.Count; i++)
                {

                }
            }
        }
        #endregion

        #region 保存、提交事件

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData(BLL.Const.BtnSave);
        }

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
            Model.Person_TrainingPlan TrainingPlan = new Model.Person_TrainingPlan
            {
                TrainingPlanCode = txtTrainingPlanCode.Text,
                TrainingPlanTitle = txtTrainingPlanTitle.Text,
                TrainingPlanContent = txtTrainingPlanContent.Text,
                CompilePersonId = this.CurrUser.UserId,
                CompileTime = DateTime.Now
            };
            if (!string.IsNullOrEmpty(this.txtStartTime.Text.Trim()))
            {
                TrainingPlan.StartTime = Convert.ToDateTime(this.txtStartTime.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtEndTime.Text.Trim()))
            {
                TrainingPlan.EndTime = Convert.ToDateTime(this.txtEndTime.Text.Trim());
            }
            if (type == BLL.Const.BtnSubmit)
            {
                if (this.drpHandleMan.SelectedValue == BLL.Const._Null)
                {
                    Alert.ShowInTop("请先选择办理人", MessageBoxIcon.Warning);
                    return;
                }
                TrainingPlan.State = "1";
                TrainingPlan.ApprovePersonId = drpHandleMan.SelectedValue;
            }
            else
            {
                TrainingPlan.State = "0";
            }
            var getPlan = BLL.Person_TrainingPlanService.GetPersonTrainingPlanById(TrainingPlanId);
            if (getPlan == null)
            {
                if (string.IsNullOrEmpty(this.TrainingPlanId))
                {
                    if (string.IsNullOrEmpty(this.hdTrainingPlanId.Text.Trim()))
                    {
                        TrainingPlan.TrainingPlanId = SQLHelper.GetNewID(typeof(Model.Person_TrainingPlan));
                    }
                    else
                    {
                        TrainingPlan.TrainingPlanId = this.hdTrainingPlanId.Text.Trim();
                    }
                }
                else
                {
                    TrainingPlan.TrainingPlanId = this.TrainingPlanId;
                }
                BLL.Person_TrainingPlanService.AddPersonTrainingPlan(TrainingPlan);
                this.TrainingPlanId = TrainingPlan.TrainingPlanId;
            }
            else
            {
                TrainingPlan.TrainingPlanId = this.TrainingPlanId;
                if (drpHandleMan.SelectedValue != BLL.Const._Null)
                {
                    TrainingPlan.ApprovePersonId = this.drpHandleMan.SelectedValue;
                }

                BLL.Person_TrainingPlanService.UpdatePersonTrainingPlan(TrainingPlan);

            }

            ///保存员工/教材明细
            var getViewList = this.CollectGridInfo();
            foreach (var item in getViewList)
            {
                var PersonItem = Funs.DB.Person_TrainingPerson.FirstOrDefault(x => x.TrainingPersonId == item.TrainingPersonId);
                if (PersonItem != null)
                {
                    PersonItem.TrainingUserId = item.TrainingUserId;
                    PersonItem.TrainingPersonDepartId = item.TrainingPersonDepartId;
                    PersonItem.TrainingPersonWorkPostId = item.TrainingPersonWorkPostId;
                    Funs.DB.SubmitChanges();
                }
                else
                {
                    Model.Person_TrainingPerson newItem = new Model.Person_TrainingPerson
                    {
                        TrainingPersonId = item.TrainingPersonId,
                        TrainingPlanId = this.TrainingPlanId,
                        TrainingUserId = item.TrainingUserId,
                        TrainingPersonDepartId = item.TrainingPersonDepartId,
                        TrainingPersonWorkPostId = item.TrainingPersonWorkPostId,

                    };
                    Funs.DB.Person_TrainingPerson.InsertOnSubmit(newItem);
                    Funs.DB.SubmitChanges();
                }
            }
            ShowNotify("提交成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        #region 收集页面信息
        /// <summary>
        ///  收集页面信息
        /// </summary>
        /// <returns></returns>
        private List<Model.View_Person_TrainingPerson> CollectGridInfo()
        {
            List<Model.View_Person_TrainingPerson> getViewList = new List<Model.View_Person_TrainingPerson>();
            for (int i = 0; i < gvPerson.Rows.Count; i++)
            {
                System.Web.UI.WebControls.CheckBox cb = (System.Web.UI.WebControls.CheckBox)(this.gvPerson.Rows[i].FindControl("cbSelect"));
                if (cb.Checked)   //选择项
                {
                    Model.View_Person_TrainingPerson newView = new Model.View_Person_TrainingPerson();
                    newView.TrainingPersonId = SQLHelper.GetNewID();
                    newView.TrainingUserId = gvPerson.Rows[i].DataKeys[0].ToString();
                    newView.TrainingPersonDepartId = null;
                    newView.TrainingPersonWorkPostId = null;
                    getViewList.Add(newView);
                }
            }

            return getViewList;
        }
        #region 页面清空
        /// <summary>
        /// 页面清空
        /// </summary>
        private void InitText()
        {
            drpWorkPost.SelectedIndex = 0;
        }
        #endregion
        #endregion

        #region Grid操作
        /// <summary>
        /// 根据所选员工查询岗位/部门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpWorkPost_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

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
            if (string.IsNullOrEmpty(this.hdTrainingPlanId.Text))
            {
                this.hdTrainingPlanId.Text = SQLHelper.GetNewID(typeof(Model.Person_TrainingPlan));
            }
            string edit = "-1";
            if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.PersonTrainingMenuId, BLL.Const.BtnAdd))
            {
                edit = "0";
                DateTime date = DateTime.Now;
                if (!string.IsNullOrEmpty(this.txtStartTime.Text.Trim()))
                {
                    date = Convert.ToDateTime(this.txtStartTime.Text.Trim());
                }
                string dateStr = date.Year.ToString() + date.Month.ToString();
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PersonTraining/" + dateStr + "&menuId={1}&type={2}", this.hdTrainingPlanId.Text, Const.PersonTrainingMenuId, edit)));
            }
        }

        #endregion

    }
}