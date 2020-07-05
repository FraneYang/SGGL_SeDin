using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FineUIPro.Web.HSSE.SitePerson
{
    public partial class PersonOut : PageBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                //项目施工单位
                BLL.UnitService.InitUnitDropDownList(this.drpUnitId, this.CurrUser.LoginProjectId, false);
                if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.CurrUser.LoginProjectId, this.CurrUser.UnitId))
                {
                    this.drpUnitId.SelectedValue = this.CurrUser.UnitId;
                    this.drpUnitId.Enabled = false;
                }
                this.txtChangeTime.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
                this.BindGrid();
            }
        }

        #region 人员下拉框绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
            {
                string strSql = string.Empty;
                ///在场人员出场
                strSql = @"SELECT PersonId,CardNo,PersonName,IdentityCard"
                          + @" FROM SitePerson_Person WHERE (OutTime IS  NULL OR OutTime>= GETDATE())";
                List<SqlParameter> listStr = new List<SqlParameter>();
                strSql += " AND ProjectId = @ProjectId";
                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
                if (!string.IsNullOrEmpty(this.CurrUser.UnitId))
                {
                    strSql += " AND UnitId = @UnitId";
                    listStr.Add(new SqlParameter("@UnitId", this.drpUnitId.SelectedValue));
                }
                if (!string.IsNullOrEmpty(this.txtPersonName.Text.Trim()))
                {
                    strSql += " AND PersonName LIKE @PersonName";
                    listStr.Add(new SqlParameter("@PersonName", "%" + this.txtPersonName.Text.Trim() + "%"));
                }
                if (!string.IsNullOrEmpty(this.txtIdentityCard.Text.Trim()))
                {
                    strSql += " AND IdentityCard LIKE @IdentityCard";
                    listStr.Add(new SqlParameter("@IdentityCard", "%" + this.txtIdentityCard.Text.Trim() + "%"));
                }
                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

                Grid1.RecordCount = tb.Rows.Count;
                var table = this.GetPagedDataTable(Grid1, tb);
                Grid1.DataSource = table;
                Grid1.DataBind();
            }
        }
        #endregion

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            foreach (var item in this.drpPerson.Values)
            {
                var person = BLL.PersonService.GetPersonById(item);
                if (person != null)
                {
                    person.OutTime = Funs.GetNewDateTime(this.txtChangeTime.Text);
                    person.IsUsed = false;
                    BLL.PersonService.UpdatePerson(person);
                }
            }

            BLL.LogService.AddSys_Log(this.CurrUser, "人员批量出场", null, BLL.Const.PersonListMenuId, BLL.Const.BtnModify);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        #region 查询
        /// <summary>
        /// 下拉框查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion
    }
}