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
                UserService.InitUserUnitIdDropDownList(drpTrainingPerson, Const.UnitId_SEDIN, true);
                CompanyTrainingService.InitCompanyTrainingIsEndDropDownList(drpCompanyTraining, true);
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
                else
                {
                    this.txtTrainingPlanTitle.Text = "人员培训计划";                    
                    this.txtTrainingPlanCode.Text= BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.PersonTrainingMenuId, this.CurrUser.LoginProjectId, this.CurrUser.UnitId);
                }
            }
        }

        private void BindGrid()
        {
            string sqlStr = "select * from View_Person_TrainingPerson where TrainingPlanId=@TrainingPlanId";
            List<SqlParameter> listStr = new List<SqlParameter>
            {
                new SqlParameter("@TrainingPlanId", this.TrainingPlanId)
            };
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(sqlStr, parameter);
            gvPerson.DataSource = GetPagedDataTable(gvPerson, tb);
            gvPerson.DataBind();
        }
        private void BindGrid1()
        {
            string sqlStr = "select * from Person_TrainingCompany T left join Training_CompanyTraining C on T.CompanyTrainingId=C.CompanyTrainingId where TrainingPlanId=@TrainingPlanId";
            List<SqlParameter> listStr = new List<SqlParameter>
            {
                new SqlParameter("@TrainingPlanId", this.TrainingPlanId)
            };
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb1 = SQLHelper.GetDataTableRunText(sqlStr, parameter);
            gvCompany.DataSource = GetPagedDataTable(gvCompany, tb1);
            gvCompany.DataBind();
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
                if (drpHandleMan.SelectedValue != BLL.Const._Null) {
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
            var getViewList1 = this.CollectgvCompanyInfo();
            foreach (var item in getViewList1)
            {
                var CompanyItem = Funs.DB.Person_TrainingCompany.FirstOrDefault(x => x.TrainingCompanyId == item.TrainingCompanyId);
                if (CompanyItem != null)
                {
                    CompanyItem.CompanyTrainingId = item.CompanyTrainingId;
                    Funs.DB.SubmitChanges();
                }
                else
                {
                    Model.Person_TrainingCompany newItem = new Model.Person_TrainingCompany
                    {
                        TrainingCompanyId = item.TrainingCompanyId,
                        TrainingPlanId = this.TrainingPlanId,
                        CompanyTrainingId = item.CompanyTrainingId,

                    };
                    Funs.DB.Person_TrainingCompany.InsertOnSubmit(newItem);
                    Funs.DB.SubmitChanges();
                }
            }
            ShowNotify("提交成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion
        
        #region 按钮确定事件
        //员工
        protected void btnSure_Click(object sender, EventArgs e)
        {
            if (drpTrainingPerson.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请先选择员工", MessageBoxIcon.Warning);
                return;
            }
            var getViewList = this.CollectGridInfo();
            var ifUpdate = getViewList.FirstOrDefault(x => x.TrainingPersonId == this.hdTrainingPersonId.Text);
            getViewList = (from x in getViewList where x.TrainingUserId != this.drpTrainingPerson.SelectedValue && x.TrainingPersonId!=this.hdTrainingPersonId.Text select x).ToList();
            var getUser = UserService.GetUserByUserId(this.drpTrainingPerson.SelectedValue);
            if (getUser != null)
            {
                Model.View_Person_TrainingPerson newView = new Model.View_Person_TrainingPerson
                {
                    TrainingPlanId = this.TrainingPlanId,
                    TrainingUserId = this.drpTrainingPerson.SelectedValue,
                    UserName = getUser.UserName
                };
                if (ifUpdate != null)
                {
                    newView.TrainingPersonId = ifUpdate.TrainingPersonId;
                }
                else
                {
                    newView.TrainingPersonId = SQLHelper.GetNewID(typeof(Model.Person_TrainingPerson));
                }
                var getDep = DepartService.GetDepartById(getUser.DepartId);
                if (getDep != null)
                {
                    newView.TrainingPersonDepartId = getDep.DepartId;
                    newView.DepartName = getDep.DepartName;
                }

                var getwork = WorkPostService.GetWorkPostById(getUser.WorkPostId);
                if (getwork != null)
                {
                    newView.TrainingPersonWorkPostId = getwork.WorkPostId;
                    newView.WorkPostName = getwork.WorkPostName;
                }

                getViewList.Add(newView);
                gvPerson.DataSource = getViewList;
                gvPerson.DataBind();
                InitText();
            }
        }
        //教材
        protected void btn_SureComPany_Click(object sender, EventArgs e)
        {
            if (drpCompanyTraining.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请先选择教材类型", MessageBoxIcon.Warning);
                return;
            }
            var getViewList = this.CollectgvCompanyInfo();
            var ifUpdate = getViewList.FirstOrDefault(x => x.TrainingCompanyId == this.hdTrainingCompanyId.Text);
            getViewList = (from x in getViewList where x.CompanyTrainingId != this.drpCompanyTraining.SelectedValue && x.TrainingCompanyId!=hdTrainingCompanyId.Text select x).ToList();
            Model.Person_TrainingCompany newView = new Model.Person_TrainingCompany
            {
                CompanyTrainingId = this.drpCompanyTraining.SelectedValue,
            };
            if (ifUpdate != null)
            {
                newView.TrainingCompanyId = ifUpdate.TrainingCompanyId;
            }
            else {
                newView.TrainingCompanyId = SQLHelper.GetNewID(typeof(Model.Person_TrainingCompany));
            }
            getViewList.Add(newView);
            var getItems = (from x in getViewList
                            join y in Funs.DB.Training_CompanyTraining on x.CompanyTrainingId equals y.CompanyTrainingId
                            select new
                            { x.TrainingCompanyId, x.CompanyTrainingId, y.CompanyTrainingName, }).ToList();
            this.gvCompany.DataSource = getItems;
            this.gvCompany.DataBind();
            this.drpCompanyTraining.SelectedIndex = 0;
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
                Model.View_Person_TrainingPerson newView = new Model.View_Person_TrainingPerson();
                newView.TrainingPersonId = gvPerson.Rows[i].DataKeys[0].ToString();
                newView.TrainingUserId = gvPerson.Rows[i].DataKeys[1].ToString();
                if (gvPerson.Rows[i].DataKeys[2] != null)
                {
                    newView.TrainingPersonDepartId = gvPerson.Rows[i].DataKeys[2].ToString();
                }
                if (gvPerson.Rows[i].DataKeys[3] != null)
                {
                    newView.TrainingPersonWorkPostId = gvPerson.Rows[i].DataKeys[3].ToString();
                }
                newView.UserName = gvPerson.Rows[i].Values[1].ToString();
                newView.DepartName = gvPerson.Rows[i].Values[2].ToString();
                newView.WorkPostName = gvPerson.Rows[i].Values[3].ToString();
                getViewList.Add(newView);
            }

            return getViewList;
        }
        #region 页面清空
        /// <summary>
        /// 页面清空
        /// </summary>
        private void InitText()
        {
            drpTrainingPerson.SelectedIndex = 0;
            this.txtDepart.Text = string.Empty;
            this.txtWorkPost.Text = string.Empty;
        }
        #endregion
        private List<Model.Person_TrainingCompany> CollectgvCompanyInfo()
        {
            List<Model.Person_TrainingCompany> getViewList = new List<Model.Person_TrainingCompany>();
            for (int i = 0; i < gvCompany.Rows.Count; i++)
            {
                Model.Person_TrainingCompany newView = new Model.Person_TrainingCompany();
                newView.TrainingCompanyId = gvCompany.Rows[i].DataKeys[0].ToString();
                newView.CompanyTrainingId = gvCompany.Rows[i].DataKeys[1].ToString();
                getViewList.Add(newView);
            }

            return getViewList;
        }
        #endregion

        #region Grid操作
        /// <summary>
        /// 根据所选员工查询岗位/部门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpTrainingPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpTrainingPerson.SelectedValue != BLL.Const._Null)
            {
                txtDepart.Text = string.Empty;
                txtWorkPost.Text = string.Empty;
                var getUser = BLL.UserService.GetUserByUserId(this.drpTrainingPerson.SelectedValue);
                if (getUser != null)
                {
                    if (!string.IsNullOrEmpty(getUser.DepartId))
                    {
                        this.txtDepart.Text = Funs.DB.Base_Depart.FirstOrDefault(x => x.DepartId == getUser.DepartId).DepartName;
                    }
                    if (!string.IsNullOrEmpty(getUser.WorkPostId))
                    {
                        this.txtWorkPost.Text = BLL.WorkPostService.getWorkPostNameById(getUser.WorkPostId);
                    }
                }
            }
        }
        //员工
        protected void gvPerson_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            if (gvPerson.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            var getViewList = this.CollectGridInfo();
            var item = getViewList.FirstOrDefault(x => x.TrainingPersonId == gvPerson.SelectedRowID);
            if (item != null)
            {
                this.hdTrainingPersonId.Text = item.TrainingPersonId;
                this.drpTrainingPerson.SelectedValue = item.TrainingUserId;
                this.txtDepart.Text = item.DepartName;
                this.txtWorkPost.Text = item.WorkPostName;
            }
        }
        //教材
        protected void gvCompany_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            if (gvCompany.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            var getViewList = this.CollectgvCompanyInfo();
            var item = getViewList.FirstOrDefault(x => x.TrainingCompanyId == gvCompany.SelectedRowID);
            if (item != null)
            {
                this.hdTrainingCompanyId.Text = item.TrainingCompanyId;
                this.drpCompanyTraining.SelectedValue = item.CompanyTrainingId;
            }
        }
        /// <summary>
        /// 删除员工
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (gvPerson.SelectedRowIndexArray.Length > 0)
            {
                var getViewList = this.CollectGridInfo();
                foreach (int rowIndex in gvPerson.SelectedRowIndexArray)
                {
                    string rowID = gvPerson.DataKeys[rowIndex][0].ToString();
                    var item = getViewList.FirstOrDefault(x => x.TrainingPersonId == rowID);
                    if (item != null)
                    {
                        getViewList.Remove(item);
                    }
                    var PersonItem = Funs.DB.Person_TrainingPerson.FirstOrDefault(x => x.TrainingPersonId == rowID);
                    if (PersonItem != null)
                    {
                        Funs.DB.Person_TrainingPerson.DeleteOnSubmit(PersonItem);
                        Funs.DB.SubmitChanges();
                    }
                }

                this.gvPerson.DataSource = getViewList;
                this.gvPerson.DataBind();
            }
        }
        /// <summary>
        /// 删除教材
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete1_Click(object sender, EventArgs e)
        {
            if (gvCompany.SelectedRowIndexArray.Length > 0)
            {
                var getViewList = this.CollectgvCompanyInfo();
                foreach (int rowIndex in gvCompany.SelectedRowIndexArray)
                {
                    string rowID = gvCompany.DataKeys[rowIndex][0].ToString();
                    var item = getViewList.FirstOrDefault(x => x.TrainingCompanyId == rowID);
                    if (item != null)
                    {
                        getViewList.Remove(item);
                    }
                    var CompanyItem = Funs.DB.Person_TrainingCompany.FirstOrDefault(x => x.TrainingCompanyId == rowID);
                    if (CompanyItem != null)
                    {
                        Funs.DB.Person_TrainingCompany.DeleteOnSubmit(CompanyItem);
                        Funs.DB.SubmitChanges();
                    }
                }

                this.gvCompany.DataSource = getViewList;
                this.gvCompany.DataBind();
            }
        }
        #endregion

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
                this.hdTrainingPlanId.Text= SQLHelper.GetNewID(typeof(Model.Person_TrainingPlan));
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