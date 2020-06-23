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
    public partial class SaveTabFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SaveFile();
            }
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="str"></param>
        public void ExecSaveFile(string str)
        {
            try
            {
                BLL.SQLHelper.RunSqlString(str, "ReportServer");
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
        }

        /// <summary>
        /// 保存模板文件
        /// </summary>
        public void SaveFile()
        {
            string reportId = Request.Form["reportId"].ToString();
            string tabContent = Request.Form["tabContent"].ToString();
            string reportName = Request.Form["reportName"].ToString();
            string reportCount = BLL.SQLHelper.getStr("SELECT COUNT(ReportId) FROM dbo.ReportServer WHERE ReportId='" + reportId + "'");

            string str = string.Empty;

            if (reportCount == "0")
            {
                str = "INSERT INTO dbo.ReportServer(ReportId,TabContent,ReportName) VALUES('" + reportId + "','" + tabContent + "','" + reportName + "')";
            }
            else
            {
                str = "UPDATE dbo.ReportServer SET TabContent = '" + tabContent + "',ReportName='" + reportName + "' WHERE ReportId = '" + reportId + "'";
            }
            //DataSet dataset = new DataSet();
            this.ExecSaveFile(str);
            Response.Write("保存成功");

        }
    }
}