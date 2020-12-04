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
                // 绑定表格
                this.BindGrid();
            }
        }

        #region TAB 1
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

        protected void btnDatabaseRefresh_Click(object sender, EventArgs e)
        {

        }
        #endregion

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
                }
            }
        }
        #endregion
                
    }
}