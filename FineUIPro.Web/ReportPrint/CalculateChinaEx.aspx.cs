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
    public partial class CalculateChinaEx : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strFunc = Request.QueryString["func"].ToString().Trim();
                string reportId = Request.QueryString["reportId"].ToString().Trim();
                if (strFunc == "GetSqlResult")
                {
                    string strSql = Server.UrlDecode(Request.QueryString["sql"].ToString().Trim());
                    string strtype = Request.QueryString["dtype"].ToString().Trim();
                    string strSql2 = Server.UrlDecode(Request.QueryString["sql2"].ToString().Trim());

                    if (strSql != "" && strSql != null && strtype != "" && strtype != null)
                    {
                        // string Connstr = "Provider=SQLNCLI10.1;Integrated Security='';Persist Security Info=False;User ID=sa;Initial Catalog=GOLDB;Data Source=(local);Initial File Name='';Server SPN=''";
                        // ADODB.Connection Conn = new ADODB.Connection();
                        // coReportEngine.ReportSvr rsave = new coReportEngine.ReportSvr();
                        // Conn.Open(Connstr, "", "", -1);

                        string strTemp = "";
                        string strValue = "";
                        int intstrCountQuery = Request.QueryString.Count;
                        if (intstrCountQuery > 5)
                        {
                            for (int i = 0; i < intstrCountQuery; i++)
                            {
                                strTemp = Server.UrlDecode(Request.QueryString.GetKey(i).ToString());
                                if (strTemp != "" && strTemp != "undefined" && strTemp != "func" && strTemp != "reportId" && strTemp != "dtype" && strTemp != "sql" && strTemp != "sql2")
                                {
                                    strValue = Server.UrlDecode(Request.QueryString[i].ToString());
                                    strSql = strSql.Replace("${" + strTemp + "}", strValue);
                                    strSql2 = strSql2.Replace("${" + strTemp + "}", strValue);
                                }
                            }
                        }
                        if (strtype == "2")
                        {
                            // Response.BinaryWrite((byte[])rsave.GetMainSubData(Conn, strSql, strSql2));
                            int iFieldCount = 0;
                            string xmlstring;
                            xmlstring = "=" + "\n";
                            DataSet dataset = new DataSet();
                            dataset = BLL.SQLHelper.RunSqlString(strSql, "Report_ReportServer");
                            DataTableReader obRead = dataset.CreateDataReader();
                            iFieldCount = obRead.FieldCount;
                            for (int i = 0; i < iFieldCount; i++)
                            {
                                if (i == iFieldCount - 1)
                                {
                                    xmlstring = xmlstring + obRead.GetName(i).ToString() + "\n";
                                }
                                else
                                {
                                    xmlstring = xmlstring + obRead.GetName(i).ToString() + "\t";
                                }
                            }
                            if (obRead.Read())
                            {
                                for (int i = 0; i < iFieldCount; i++)
                                {
                                    if (i == iFieldCount - 1)
                                    {
                                        xmlstring = xmlstring + obRead[obRead.GetName(i).ToString()].ToString().Replace("\r\n", "&at;").Replace("\t", " ").Replace("0:00:00", "") + "\n";
                                    }
                                    else
                                    {
                                        xmlstring = xmlstring + obRead[obRead.GetName(i).ToString()].ToString().Replace("\r\n", "&at;").Replace("\t", " ").Replace("0:00:00", "") + "\t";
                                    }
                                }
                            }


                            xmlstring = xmlstring + "=" + "\n";
                            DataSet dataset1 = new DataSet();
                            dataset1 = BLL.SQLHelper.RunSqlString(strSql2, "Report_ReportServer");
                            DataTableReader obRead1 = dataset1.CreateDataReader();
                            iFieldCount = obRead1.FieldCount;
                            for (int i = 0; i < iFieldCount; i++)
                            {
                                if (i == iFieldCount - 1)
                                {
                                    xmlstring = xmlstring + obRead1.GetName(i).ToString() + "\n";
                                }
                                else
                                {
                                    xmlstring = xmlstring + obRead1.GetName(i).ToString() + "\t";
                                }
                            }
                            while (obRead1.Read())
                            {
                                for (int i = 0; i < iFieldCount; i++)
                                {
                                    if (i == iFieldCount - 1)
                                    {
                                        xmlstring = xmlstring + obRead1[obRead1.GetName(i).ToString()].ToString().Replace("0:00:00", "") + "\n";
                                    }
                                    else
                                    {
                                        xmlstring = xmlstring + obRead1[obRead1.GetName(i).ToString()].ToString().Replace("0:00:00", "") + "\t";
                                    }
                                }
                            }

                            Response.Write(xmlstring);
                        }
                        else
                        {
                            //      Response.BinaryWrite((byte[])rsave.GetData(Conn, strSql));

                            int iFieldCount = 0;
                            string xmlstring;
                            xmlstring = "";
                            DataSet dataset = new DataSet();
                            //dataset = BLL.SQLHelper.RunSqlString(strSql, "TempTable");
                            //DataTableReader obRead = dataset.CreateDataReader();
                            using (SqlConnection conn = new SqlConnection(BLL.Funs.ConnString))
                            {                               
                                conn.Open();
                                SqlCommand cmd = new SqlCommand(strSql, conn);
                                SqlDataReader obRead = cmd.ExecuteReader();
                                iFieldCount = obRead.FieldCount;

                                for (int i = 0; i < iFieldCount; i++)
                                {
                                    if (i == iFieldCount - 1)
                                    {
                                        xmlstring = xmlstring + obRead.GetName(i).ToString() + "\n";
                                    }
                                    else
                                    {
                                        xmlstring = xmlstring + obRead.GetName(i).ToString() + "\t";
                                    }
                                }
                                while (obRead.Read())
                                {
                                    for (int i = 0; i < iFieldCount; i++)
                                    {
                                        if (i == iFieldCount - 1)
                                        {
                                            xmlstring = xmlstring + obRead[obRead.GetName(i).ToString()].ToString().Replace("0:00:00", "").Replace(".000", "") + "\n";
                                        }
                                        else
                                        {
                                            xmlstring = xmlstring + obRead[obRead.GetName(i).ToString()].ToString().Replace("0:00:00", "").Replace(".000", "") + "\t";
                                        }
                                    }
                                }
                                conn.Close();
                                Response.Write(xmlstring);
                            }
                        }
                    }
                }
            }
        }
    }
}