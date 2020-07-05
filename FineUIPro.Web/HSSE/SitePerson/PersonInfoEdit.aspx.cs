using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FineUIPro.Web.HSSE.SitePerson
{
    public partial class PersonInfoEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string CheckingId
        {
            get
            {
                return (string)ViewState["CheckingId"];
            }
            set
            {
                ViewState["CheckingId"] = value;
            }
        }
        /// <summary>
        /// 项目主键
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
            }
        }
        #endregion

        #region 加载
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ProjectId = this.CurrUser.LoginProjectId;
                ///区域下拉框
                BLL.UnitWorkService.InitUnitWorkDownList(this.drpWorkArea, this.ProjectId, true);
                BindGrid(string.Empty);
                this.CheckingId = Request.Params["CheckingId"];
                if (!string.IsNullOrEmpty(Request.Params["type"]))
                {
                    this.btnSave.Hidden = true;
                }
                if (!string.IsNullOrEmpty(this.CheckingId))
                {
                    Model.SitePerson_Checking personInfo = BLL.SitePerson_CheckingService.GetPersonInfoByCheckingId(this.CheckingId);
                    if (personInfo != null)
                    {
                        this.ProjectId = personInfo.ProjectId;
                        if (!string.IsNullOrEmpty(personInfo.PersonId))
                        {
                            BindGrid(personInfo.PersonId);
                            this.drpPersonId.Value = personInfo.PersonId;
                        }
                        this.txtWorkArea.Text = personInfo.WorkAreaName;
                        this.txtAddress.Text = personInfo.Address;
                        if (personInfo.IntoOutTime != null)
                        {
                            this.txtTime.Text = string.Format("{0:yyyy-MM-dd}", personInfo.IntoOutTime);
                            this.txtTime2.Text = string.Format("{0:HH:mm:ss}", personInfo.IntoOutTime);
                        }
                        this.drpType.SelectedValue = personInfo.IntoOut;
                    }
                }
                else
                {
                    this.txtTime.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtTime2.Text = string.Format("{0:HH:mm:ss}", DateTime.Now);
                }
            }
        }
        #endregion

        #region DropDownList下拉选择事件
        /// <summary>
        ///  人员下拉框选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpPersonId_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.drpPersonId.Value))
            {
                var person = BLL.PersonService.GetPersonById(this.drpPersonId.Value);
                if (person != null)
                {
                    if (!string.IsNullOrEmpty(person.WorkAreaId))
                    {
                        this.drpWorkArea.SelectedValue = person.WorkAreaId;
                        this.txtWorkArea.Text = this.drpWorkArea.SelectedItem.Text;
                    }
                }
            }
        }
        #endregion

        #region 区域选择框事件
        /// <summary>
        /// 区域选择框事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpWorkArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpWorkArea.SelectedValue != BLL.Const._Null)
            {
                this.txtWorkArea.Text = this.drpWorkArea.SelectedText;
            }
            else
            {
                this.txtWorkArea.Text = string.Empty;
            }
        }
        #endregion

        #region 绑定数据
        /// <summary>
        /// 下拉框查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid(string.Empty);
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid(string personId)
        {
            string strSql = @"SELECT Person.PersonId,Person.CardNo,Person.PersonName,Person.IdentityCard,Person.UnitId,Person.WorkPostId,Unit.UnitName,WorkPost.WorkPostName "
                + @" FROM SitePerson_Person AS Person "
                + @" LEFT JOIN Base_Unit AS Unit ON Unit.UnitId = Person.UnitId "
                + @" LEFT JOIN Base_WorkPost AS WorkPost ON WorkPost.WorkPostId = Person.WorkPostId  WHERE ProjectId='" + this.ProjectId + "'";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.txtCardNo.Text.Trim()))
            {
                strSql += " AND Person.CardNo LIKE @CardNo";
                listStr.Add(new SqlParameter("@CardNo", "%" + this.txtCardNo.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtPersonName.Text.Trim()))
            {
                strSql += " AND Person.PersonName LIKE @PersonName";
                listStr.Add(new SqlParameter("@PersonName", "%" + this.txtPersonName.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtIdentityCard.Text.Trim()))
            {
                strSql += " AND Person.IdentityCard LIKE @IdentityCard";
                listStr.Add(new SqlParameter("@IdentityCard", "%" + this.txtIdentityCard.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(personId))
            {
                strSql += " AND Person.PersonId = @PersonId";
                listStr.Add(new SqlParameter("@PersonId", personId));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;       
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.drpPersonId.Value))
            {
                ShowNotify("请选择人员！", MessageBoxIcon.Warning);
                return;
            }
            Model.SitePerson_Checking personInfo = new Model.SitePerson_Checking
            {
                PersonId = this.drpPersonId.Value,
                ProjectId = this.ProjectId,
                WorkAreaName = this.txtWorkArea.Text.Trim(),
                Address = this.txtAddress.Text.Trim()
            };
            string date = string.IsNullOrEmpty(this.txtTime.Text) ? string.Format("{0:yyyy-MM-dd}", DateTime.Now) : this.txtTime.Text.Trim();
            string time = string.IsNullOrEmpty(this.txtTime2.Text) ? string.Format("{0:HH:mm:ss}", DateTime.Now) : this.txtTime2.Text.Trim();
            personInfo.IntoOutTime = Funs.GetNewDateTimeOrNow(date + " " + time);
            personInfo.IntoOut = this.drpType.SelectedValue.Trim();

            if (!string.IsNullOrEmpty(CheckingId))
            {
                personInfo.CheckingId = CheckingId;
                BLL.SitePerson_CheckingService.UpdatePersonInfo(personInfo);
                BLL.LogService.AddSys_Log(this.CurrUser, personInfo.CardNo, personInfo.CheckingId, BLL.Const.PersonalInfoMenuId, BLL.Const.BtnModify);
            }
            else
            {
                string newKeyID = SQLHelper.GetNewID(typeof(Model.SitePerson_Checking));
                personInfo.CheckingId = newKeyID;
                BLL.SitePerson_CheckingService.AddPersonInfo(personInfo);
                BLL.LogService.AddSys_Log(this.CurrUser, personInfo.CardNo, personInfo.CheckingId, BLL.Const.PersonalInfoMenuId, BLL.Const.BtnDelete);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        #region rbl选择事件
        /// <summary>
        /// 自动、手动选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblCheck_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblCheck.SelectedValue == "自动")
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PersonAutoDetail.aspx", "编辑 - ")));
            }
        }
        #endregion
    }
}