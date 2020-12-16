using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BLL;

namespace FineUIPro.Web.ZHGL.RealName
{
    public partial class BasicData : PageBase
    {
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetButtonPower();
                SynchroSetService.InitCountryDropDownList(this.drpCountry, false);
                this.drpCountry.SelectedValue ="101";

                SynchroSetService.InitProjectDropDownList(this.drpProject, false);
                // 绑定表格
                this.BindGrid();
                this.BindGrid2();
                this.BindGrid3();
                this.BindGrid4();
                this.BindGrid5();
            }
        }

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.RealNameBasicDataMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    //this.btnSave.Hidden = false;
                    this.btnDatabaseGo.Hidden = false;
                    this.btnDatabaseGo2.Hidden = false;
                    this.btnDatabaseGo3.Hidden = false;
                    this.btnDatabaseGo31.Hidden = false;
                    this.btnDatabaseGo4.Hidden = false;
                    this.btnDatabaseGo5.Hidden = false;
                    this.btnDatabaseGo51.Hidden = false;
                }
            }
        }
        #endregion

        #region TAB 1 基础字典数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT BasicDataId,dictTypeCode,dictCode,dictName FROM dbo.RealName_BasicData"
                                + @" WHERE 1=1";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.drpBaseType.SelectedValue))
            {
                strSql += " AND dictTypeCode = @dictTypeCode";
                listStr.Add(new SqlParameter("@dictTypeCode", this.drpBaseType.SelectedValue));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        protected void drpBaseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        protected void btnDatabaseGo_Click(object sender, EventArgs e)
        {
            ShowNotify(BLL.SynchroSetService.getBasicData(this.drpBaseType.SelectedValue), MessageBoxIcon.Information);
            this.BindGrid();
        }
        #endregion

        #region TAB 2 国家数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDatabaseGo2_Click(object sender, EventArgs e)
        {
            ShowNotify(BLL.SynchroSetService.getCountry(), MessageBoxIcon.Information);
            this.BindGrid2();
        }
             
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid2()
        {
            string strSql = @"SELECT ID,CountryId,countryCode,cname,name FROM dbo.RealName_Country"
                                + @" WHERE 1=1";
            List<SqlParameter> listStr = new List<SqlParameter>();
            //if (!string.IsNullOrEmpty(this.drpBaseType.SelectedValue))
            //{
            //    strSql += " AND dictTypeCode = @dictTypeCode";
            //    listStr.Add(new SqlParameter("@dictTypeCode", this.drpBaseType.SelectedValue));
            //}
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid2.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid2, tb);
            Grid2.DataSource = table;
            Grid2.DataBind();
        }
        #endregion

        #region TAB 3 省份数据
        /// <summary>
        /// 国家下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindGrid3();
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid3()
        {
            string strSql = @"SELECT ID,provinceCode,cityCode,cname,cnShortName,name,countryId FROM dbo.RealName_City"
                                + @" WHERE 1=1";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.drpCountry.SelectedValue))
            {
                strSql += " AND countryId = @countryId";
                listStr.Add(new SqlParameter("@countryId", this.drpCountry.SelectedValue));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid3.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid3, tb);
            Grid3.DataSource = table;
            Grid3.DataBind();
        }
        protected void btnDatabaseGo3_Click(object sender, EventArgs e)
        {
            ShowNotify(BLL.SynchroSetService.getCity(this.drpCountry.SelectedValue), MessageBoxIcon.Information);
            this.BindGrid3();
        }

        protected void btnDatabaseGo31_Click(object sender, EventArgs e)
        {
            ShowNotify(BLL.SynchroSetService.getCity(null), MessageBoxIcon.Information);
            this.BindGrid3();
        }
        #endregion

        #region TAB 4 项目数据
        /// <summary>
        /// 获取项目数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDatabaseGo4_Click(object sender, EventArgs e)
        {
            ShowNotify(BLL.SynchroSetService.getProject(), MessageBoxIcon.Information);
            this.BindGrid4();
        }

        /// <summary>
        /// 绑定项目数据
        /// </summary>
        private void BindGrid4()
        {
            string strSql = @"SELECT ID,proCode,proName,proShortName FROM dbo.RealName_Project"
                                + @" WHERE 1=1";
            List<SqlParameter> listStr = new List<SqlParameter>();
            //if (!string.IsNullOrEmpty(this.drpBaseType.SelectedValue))
            //{
            //    strSql += " AND dictTypeCode = @dictTypeCode";
            //    listStr.Add(new SqlParameter("@dictTypeCode", this.drpBaseType.SelectedValue));
            //}
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid4.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid4, tb);
            Grid4.DataSource = table;
            Grid4.DataBind();
        }
        #endregion

        #region TAB 5 施工队数据
        /// <summary>
        /// 国家下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindGrid5();
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid5()
        {
            string strSql = @"SELECT ID,teamId,proCode,teamName,teamLeaderName,teamLeaderMobile FROM dbo.RealName_CollTeam"
                                + @" WHERE 1=1";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.drpProject.SelectedValue))
            {
                strSql += " AND proCode = @proCode";
                listStr.Add(new SqlParameter("@proCode", this.drpProject.SelectedValue));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid5.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid5, tb);
            Grid5.DataSource = table;
            Grid5.DataBind();
        }
        protected void btnDatabaseGo5_Click(object sender, EventArgs e)
        {
            ShowNotify(BLL.SynchroSetService.getCollTeam(this.drpProject.SelectedValue), MessageBoxIcon.Information);
            this.BindGrid5();
        }

        protected void btnDatabaseGo51_Click(object sender, EventArgs e)
        {
            ShowNotify(BLL.SynchroSetService.getCollTeam(null), MessageBoxIcon.Information);
            this.BindGrid5();
        }
        #endregion
    }
}