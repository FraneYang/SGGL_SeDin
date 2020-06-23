using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.ReportPrint
{
    public partial class ReadExReportFile : System.Web.UI.Page
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.ShowReportContent();
        }

        /// <summary>
        /// 获取报表对应模板信息
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public DataSet GetData(string str)
        {
            DataSet dataset = new DataSet();
            try
            {
                dataset = BLL.SQLHelper.RunSqlString("SELECT * FROM dbo.ReportServer WHERE ReportId='" + str + "'", "ReportServer");
            }
            catch (Exception e)
            {
                Response.Write(e.Message);
                Response.Write(e.Source);
                Response.End();

            }
            finally
            {
            }
            return dataset;
        }

        /// <summary>
        /// 获取模板内容
        /// </summary>
        private void ShowReportContent()
        {
            if (Request.QueryString["reportId"].ToString().Trim() != "")
            {
                string reportId = Request.QueryString["reportId"].ToString().Trim();

                DataSet ds;
                ds = GetData(reportId);
                if (ds != null)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string tabContent = dt.Rows[0]["TabContent"].ToString().Trim();
                        Response.Write(tabContent);
                    }
                }
            }
        }
    }

}