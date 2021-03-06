﻿using System;
using System.Collections.Generic;
using System.Linq;
using BLL;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;
namespace FineUIPro.Web.HJGL.WPQ
{
    public partial class WPQIn : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 上传预设的虚拟路径
        /// </summary>
        private string initPath = Const.ExcelUrl;

        /// <summary>
        /// 焊接工艺评定台账集合
        /// </summary>
        public static List<Model.WPQ_WPQList> weldingProcedures = new List<Model.WPQ_WPQList>();

        /// <summary>
        /// 错误集合
        /// </summary>
        public static string errorInfos = string.Empty;
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
                this.hdFileName.Text = string.Empty;
                if (weldingProcedures != null)
                {
                    weldingProcedures.Clear();
                }
                errorInfos = string.Empty;
            }
        }
        #endregion

        #region 审核
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAudit_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.fuAttachUrl.HasFile == false)
                {
                    ShowNotify("请您选择Excel文件！", MessageBoxIcon.Warning);
                    return;
                }
                string IsXls = Path.GetExtension(this.fuAttachUrl.FileName).ToString().Trim().ToLower();
                if (IsXls != ".xls")
                {
                    ShowNotify("只可以选择Excel文件！", MessageBoxIcon.Warning);
                    return;
                }
                if (weldingProcedures != null)
                {
                    weldingProcedures.Clear();
                }
                if (!string.IsNullOrEmpty(errorInfos))
                {
                    errorInfos = string.Empty;
                }
                string rootPath = Server.MapPath("~/");
                string initFullPath = rootPath + initPath;
                if (!Directory.Exists(initFullPath))
                {
                    Directory.CreateDirectory(initFullPath);
                }

                this.hdFileName.Text = BLL.Funs.GetNewFileName() + IsXls;
                string filePath = initFullPath + this.hdFileName.Text;
                this.fuAttachUrl.PostedFile.SaveAs(filePath);
                ImportXlsToData(rootPath + initPath + this.hdFileName.Text);
            }
            catch (Exception ex)
            {
                ShowNotify("'" + ex.Message + "'", MessageBoxIcon.Warning);
            }
        }

        #region 读Excel提取数据
        /// <summary>
        /// 从Excel提取数据--》Dataset
        /// </summary>
        /// <param name="filename">Excel文件路径名</param>
        private void ImportXlsToData(string fileName)
        {
            ////支持.xls和.xlsx，即包括office2010等版本的   HDR=Yes代表第一行是标题，不是数据；
            //string cmdText = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'";
            
            ////建立连接
            //OleDbConnection conn = new OleDbConnection(string.Format(cmdText, fileName));
            try
            {
                //打开连接
                //if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                //{
                //    conn.Open();
                //}
                //OleDbDataAdapter oleAdMaster = null;
                //DataTable m_tableName = new DataTable();
                //DataSet ds = new DataSet();
                //m_tableName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                //if (m_tableName != null && m_tableName.Rows.Count > 0)
                //{

                //    m_tableName.TableName = m_tableName.Rows[0]["TABLE_NAME"].ToString().Trim();

                //}
                //string sqlMaster;
                //sqlMaster = " SELECT *  FROM [" + m_tableName.TableName + "]";
                //oleAdMaster = new OleDbDataAdapter(sqlMaster, conn);
                //oleAdMaster.Fill(ds, "m_tableName");
                //oleAdMaster.Dispose();
                //conn.Close();
                //conn.Dispose();
                string oleDBConnString = String.Empty;
                oleDBConnString = "Provider=Microsoft.Jet.OLEDB.4.0;";
                oleDBConnString += "Data Source=";
                oleDBConnString += fileName;
                oleDBConnString += ";Extended Properties=Excel 8.0;";
                OleDbConnection oleDBConn = null;
                OleDbDataAdapter oleAdMaster = null;
                DataTable m_tableName = new DataTable();
                DataSet ds = new DataSet();

                oleDBConn = new OleDbConnection(oleDBConnString);
                oleDBConn.Open();
                m_tableName = oleDBConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (m_tableName != null && m_tableName.Rows.Count > 0)
                {
                    m_tableName.TableName = m_tableName.Rows[0]["TABLE_NAME"].ToString().Trim();
                }
                string sqlMaster;
                sqlMaster = " SELECT *  FROM [" + m_tableName.TableName + "]";
                oleAdMaster = new OleDbDataAdapter(sqlMaster, oleDBConn);
                oleAdMaster.Fill(ds, "m_tableName");
                oleAdMaster.Dispose();
                oleDBConn.Close();
                oleDBConn.Dispose();
                AddDatasetToSQL(ds.Tables[0], 26);
            }
            catch (Exception exc)
            {
                Response.Write(exc);
                //return null;
                // return dt;
            }
            //finally
            //{
            //    conn.Close();
            //    conn.Dispose();
            //}
            //try
            //{
            //    string oleDBConnString = String.Empty;
            //    oleDBConnString = "Provider=Microsoft.Jet.OLEDB.4.0;";
            //    oleDBConnString += "Data Source=";
            //    oleDBConnString += fileName;
            //    oleDBConnString += ";Extended Properties=Excel 8.0;";
            //    OleDbConnection oleDBConn = null;
            //    OleDbDataAdapter oleAdMaster = null;
            //    DataTable m_tableName = new DataTable();
            //    DataSet ds = new DataSet();

            //    oleDBConn = new OleDbConnection(oleDBConnString);
            //    oleDBConn.Open();
            //    m_tableName = oleDBConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            //    if (m_tableName != null && m_tableName.Rows.Count > 0)
            //    {

            //        m_tableName.TableName = m_tableName.Rows[0]["TABLE_NAME"].ToString().Trim();

            //    }
            //    string sqlMaster;
            //    sqlMaster = " SELECT *  FROM [" + m_tableName.TableName + "]";
            //    oleAdMaster = new OleDbDataAdapter(sqlMaster, oleDBConn);
            //    oleAdMaster.Fill(ds, "m_tableName");
            //    oleAdMaster.Dispose();
            //    oleDBConn.Close();
            //    oleDBConn.Dispose();

            //    AddDatasetToSQL(ds.Tables[0], 26);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
        #endregion
       
        #region 将Dataset的数据导入数据库
        /// <summary>
        /// 将Dataset的数据导入数据库
        /// </summary>
        /// <param name="pds">数据集</param>
        /// <param name="Cols">数据集行数</param>
        /// <returns></returns>
        private bool AddDatasetToSQL(DataTable pds, int Cols)
        {
            string result = string.Empty;
            int ic, ir;
            ic = pds.Columns.Count;
            if (ic < Cols)
            {
                ShowNotify("导入Excel格式错误！Excel只有" + ic.ToString().Trim() + "行", MessageBoxIcon.Warning);
            }

            ir = pds.Rows.Count;
            if (pds != null && ir > 0)
            {
                var units = BLL.UnitService.GetUnitByProjectIdUnitTypeList(this.CurrUser.LoginProjectId, Const.ProjectUnitType_2);
                var steels = from x in Funs.DB.Base_Material orderby x.MaterialCode select x;//材质
                var consumables = from x in Funs.DB.Base_Consumables orderby x.ConsumablesCode select x;
                var GrooveType = from x in Funs.DB.Base_GrooveType orderby x.GrooveTypeCode select x;
                var weldMethods = from x in Funs.DB.Base_WeldingMethod orderby x.WeldingMethodCode select x;//焊接方法
                var users = (from x in Funs.DB.Sys_User
                            join y in Funs.DB.Project_ProjectUser on x.UserId equals y.UserId
                            join z in Funs.DB.Project_ProjectUnit on x.UnitId equals z.UnitId
                            where x.IsPost == true && y.ProjectId == this.CurrUser.LoginProjectId && z.ProjectId == this.CurrUser.LoginProjectId && z.UnitType == Const.ProjectUnitType_1
                            orderby x.UserId
                            select x).ToList();//审批人
                for (int i = 0; i < ir; i++)
                {
                    string col0 = pds.Rows[i][0].ToString();
                    if (string.IsNullOrEmpty(col0))
                    {
                        result += "第" + (i + 2).ToString() + "行," + "评定编号" + "," + "此项为必填项！" + "|";
                    }
                    string col1 = pds.Rows[i][1].ToString();
                    if (!string.IsNullOrEmpty(col1))
                    {
                        Model.Base_Unit unit = units.FirstOrDefault(x => x.UnitName == col1);
                        if (unit == null)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "编制单位" + "," + "[" + col1 + "]错误！" + "|";
                        }
                    }
                    else
                    {
                        result += "第" + (i + 2).ToString() + "行," + "编制单位" + "," + "此项为必填项！" + "|";
                    }
                    string col2 = pds.Rows[i][2].ToString();
                    if (!string.IsNullOrEmpty(col2))
                    {
                        Model.Base_Material steel = steels.FirstOrDefault(e => e.MaterialCode == col2);
                        if (steel == null)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "材质1" + "," + "[" + col2 + "]错误！" + "|";
                        }
                    }
                    else
                    {
                        result += "第" + (i + 2).ToString() + "行," + "材质1" + "," + "此项为必填项！" + "|";
                    }
                    string col3 = pds.Rows[i][3].ToString();
                    if (!string.IsNullOrEmpty(col3))
                    {
                        Model.Base_Material steel = steels.FirstOrDefault(e => e.MaterialCode == col3);
                        if (steel == null)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "材质2" + "," + "[" + col3 + "]错误！" + "|";
                        }
                    }
                    else
                    {
                        result += "第" + (i + 2).ToString() + "行," + "材质2" + "," + "此项为必填项！" + "|";
                    }
                    string col5 = pds.Rows[i][5].ToString();
                    if (!string.IsNullOrEmpty(col5))
                    {
                        Model.Base_Consumables consumable = consumables.FirstOrDefault(e => e.ConsumablesName == col5 && e.ConsumablesType=="1");
                        if (consumable == null)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "焊丝" + "," + "[" + col5 + "]错误！" + "|";
                        }
                    }
                    string col6 = pds.Rows[i][6].ToString();
                    if (!string.IsNullOrEmpty(col6))
                    {
                        Model.Base_Consumables consumable = consumables.FirstOrDefault(e => e.ConsumablesName == col6 && e.ConsumablesType == "2");
                        if (consumable == null)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "焊条" + "," + "[" + col6 + "]错误！" + "|";
                        }
                    }
                    if(string.IsNullOrWhiteSpace(col5) && string.IsNullOrWhiteSpace(col6))
                    {
                        result += "第" + (i + 2).ToString() + "行," + "焊丝、焊条不能同时为空！|";
                    }
                    string col7 = pds.Rows[i][7].ToString();
                    if (!string.IsNullOrEmpty(col7))
                    {
                        Model.Base_GrooveType grooveTypes = GrooveType.FirstOrDefault(e => e.GrooveTypeCode == col7);
                        if (GrooveType == null)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "坡口类型" + "," + "[" + col7 + "]错误！" + "|";
                        }
                    }
                    else
                    {
                        result += "第" + (i + 2).ToString() + "行," + "坡口类型" + "," + "此项为必填项！" + "|";
                    }
                    string col8 = pds.Rows[i][8].ToString();
                    if(col8!= "对接焊缝" && col8 != "角焊缝" && col8 != "支管连接焊缝")
                    {
                        result += "第" + (i + 2).ToString() + "行," + "接头形式" + "," + "[" + col8 + "]错误！" + "|";
                    }
                        string col9 = pds.Rows[i][9].ToString();
                    if (!string.IsNullOrEmpty(col9))
                    {
                        Model.Base_WeldingMethod weldMethod = weldMethods.FirstOrDefault(e => e.WeldingMethodCode == col9);
                        if (weldMethod == null)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "焊接方法" + "," + "[" + col9 + "]错误！" + "|";
                        }
                    }
                    string col10 = pds.Rows[i][10].ToString();
                    if (!string.IsNullOrEmpty(col10))
                    {
                        try
                        {
                            decimal? minImpactDia = Funs.GetNewDecimal(col10);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "管径覆盖最小值(对接焊缝)" + "," + "[" + col10 + "]错误！" + "|";
                        }
                    }
                    string col11 = pds.Rows[i][11].ToString();
                    if (!string.IsNullOrEmpty(col11))
                    {
                        try
                        {
                            decimal? maxImpactDia = Funs.GetNewDecimal(col11);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "管径覆盖最大值(对接焊缝)" + "," + "[" + col11 + "]错误！" + "|";
                        }
                    }
                    string col12 = pds.Rows[i][12].ToString();
                    if (!string.IsNullOrEmpty(col12))
                    {
                        try
                        {
                            decimal? minImpactThickness = Funs.GetNewDecimal(col12);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "壁厚覆盖最小值(对接焊缝)" + "," + "[" + col12 + "]错误！" + "|";
                        }
                    }
                    string col13 = pds.Rows[i][13].ToString();
                    if (!string.IsNullOrEmpty(col13))
                    {
                        try
                        {
                            decimal? maxImpactThickness = Funs.GetNewDecimal(col13);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "壁厚覆盖最大值(对接焊缝)" + "," + "[" + col13 + "]错误！" + "|";
                        }
                    }
                    string col14 = pds.Rows[i][14].ToString();
                    if (!string.IsNullOrEmpty(col14))
                    {
                        try
                        {
                            decimal? MinCImpactDia = Funs.GetNewDecimal(col14);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "管径覆盖最小值(角焊缝)" + "," + "[" + col14 + "]错误！" + "|";
                        }
                    }
                    string col15 = pds.Rows[i][15].ToString();
                    if (!string.IsNullOrEmpty(col15))
                    {
                        try
                        {
                            decimal? MaxCImpactDia = Funs.GetNewDecimal(col15);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "管径覆盖最大值(角焊缝)" + "," + "[" + col15 + "]错误！" + "|";
                        }
                    } 
                    string col16 = pds.Rows[i][16].ToString();
                    if (!string.IsNullOrEmpty(col16))
                    {
                        try
                        {
                            decimal? noMinImpactThickness = Funs.GetNewDecimal(col16);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "壁厚覆盖最小值(角焊缝)" + "," + "[" + col16 + "]错误！" + "|";
                        }
                    }
                    string col17 = pds.Rows[i][17].ToString();
                    if (!string.IsNullOrEmpty(col17))
                    {
                        try
                        {
                            decimal? noMaxImpactThickness = Funs.GetNewDecimal(col17);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "壁厚覆盖最大值(角焊缝)" + "," + "[" + col17 + "]错误！" + "|";
                        }
                    }
                    string col19 = pds.Rows[i][19].ToString();
                    if (!string.IsNullOrEmpty(col19))
                    {
                        if (col19 != "是" && col19 != "否")
                        {
                            result += "第" + (i + 2).ToString() + "行," + "是否热处理" + "," + "[" + col19 + "]错误！" + "|";
                        }
                    }
                    string col25 = pds.Rows[i][25].ToString();
                    if (!string.IsNullOrEmpty(col25))
                    {
                        var user = users.FirstOrDefault(c => c.UserName == col25);
                        if (user == null)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "审批人" + "," + "[" + col25 + "]错误！" + "|";
                        }
                    }
                    else
                    {
                        result += "第" + (i + 2).ToString() + "行," + "审批人" + "," + "此项为必填项！" + "|";
                    }
                }
                if (!string.IsNullOrEmpty(result))
                {
                    result = result.Substring(0, result.LastIndexOf("|"));
                    errorInfos = result;
                    Alert alert = new Alert();
                    alert.Message = result;
                    alert.Target = Target.Self;
                    alert.Show();
                }
                else
                {
                    errorInfos = string.Empty;
                    ShowNotify("审核完成,请点击导入！", MessageBoxIcon.Success);
                }
            }
            else
            {
                ShowNotify("导入数据为空！", MessageBoxIcon.Warning);
            }
            return true;
        }
        #endregion
        #endregion

        #region 导入
        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(errorInfos))
            {
                if (!string.IsNullOrEmpty(this.hdFileName.Text))
                {
                    string rootPath = Server.MapPath("~/");
                    ImportXlsToData2(rootPath + initPath + this.hdFileName.Text);
                }
                else
                {
                    ShowNotify("请先审核要导入的文件！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("请先将错误数据修正，再重新导入提交！", MessageBoxIcon.Warning);
            }
        }

        #region Excel提取数据
        /// <summary>
        /// 从Excel提取数据--》Dataset
        /// </summary>
        /// <param name="filename">Excel文件路径名</param>
        private void ImportXlsToData2(string fileName)
        {
            ////支持.xls和.xlsx，即包括office2010等版本的   HDR=Yes代表第一行是标题，不是数据；
            //string cmdText = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'";
            ////建立连接
            //OleDbConnection conn = new OleDbConnection(string.Format(cmdText, fileName));
            try
            {
                //打开连接
                //if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                //{
                //    conn.Open();
                //}
                //OleDbDataAdapter oleAdMaster = null;
                //DataTable m_tableName = new DataTable();
                //DataSet ds = new DataSet();
                //m_tableName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                //if (m_tableName != null && m_tableName.Rows.Count > 0)
                //{

                //    m_tableName.TableName = m_tableName.Rows[0]["TABLE_NAME"].ToString().Trim();

                //}
                //string sqlMaster;
                //sqlMaster = " SELECT *  FROM [" + m_tableName.TableName + "]";
                //oleAdMaster = new OleDbDataAdapter(sqlMaster, conn);
                //oleAdMaster.Fill(ds, "m_tableName");
                //oleAdMaster.Dispose();
                //conn.Close();
                //conn.Dispose();
                string oleDBConnString = String.Empty;
                oleDBConnString = "Provider=Microsoft.Jet.OLEDB.4.0;";
                oleDBConnString += "Data Source=";
                oleDBConnString += fileName;
                oleDBConnString += ";Extended Properties=Excel 8.0;";
                OleDbConnection oleDBConn = null;
                OleDbDataAdapter oleAdMaster = null;
                DataTable m_tableName = new DataTable();
                DataSet ds = new DataSet();

                oleDBConn = new OleDbConnection(oleDBConnString);
                oleDBConn.Open();
                m_tableName = oleDBConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (m_tableName != null && m_tableName.Rows.Count > 0)
                {
                    m_tableName.TableName = m_tableName.Rows[0]["TABLE_NAME"].ToString().Trim();
                }
                string sqlMaster;
                sqlMaster = " SELECT *  FROM [" + m_tableName.TableName + "]";
                oleAdMaster = new OleDbDataAdapter(sqlMaster, oleDBConn);
                oleAdMaster.Fill(ds, "m_tableName");
                oleAdMaster.Dispose();
                oleDBConn.Close();
                oleDBConn.Dispose();

                AddDatasetToSQL2(ds.Tables[0],26);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 将Dataset的数据导入数据库
        /// <summary>
        /// 将Dataset的数据导入数据库
        /// </summary>
        /// <param name="pds">数据集</param>
        /// <param name="Cols">数据集列数</param>
        /// <returns></returns>
        private bool AddDatasetToSQL2(DataTable pds, int Cols)
        {
            int ic, ir;
            weldingProcedures.Clear();
            ic = pds.Columns.Count;
            if (ic < Cols)
            {
                ShowNotify("导入Excel格式错误！Excel只有" + ic.ToString().Trim() + "列", MessageBoxIcon.Warning);
            }

            ir = pds.Rows.Count;
            if (pds != null && ir > 0)
            {
                var steels = from x in Funs.DB.Base_Material orderby x.MaterialCode select x;//材质
                var weldMethods = from x in Funs.DB.Base_WeldingMethod orderby x.WeldingMethodCode select x;//焊接方法  
                var consumables = from x in Funs.DB.Base_Consumables orderby x.ConsumablesCode select x;
                var GrooveType = from x in Funs.DB.Base_GrooveType orderby x.GrooveTypeCode select x;
                var units = BLL.UnitService.GetUnitByProjectIdUnitTypeList(this.CurrUser.LoginProjectId, Const.ProjectUnitType_2);
                var users = (from x in Funs.DB.Sys_User
                             join y in Funs.DB.Project_ProjectUser on x.UserId equals y.UserId
                             join z in Funs.DB.Project_ProjectUnit on x.UnitId equals z.UnitId
                             where x.IsPost == true && y.ProjectId == this.CurrUser.LoginProjectId && z.ProjectId == this.CurrUser.LoginProjectId && z.UnitType == Const.ProjectUnitType_1
                             orderby x.UserId
                             select x).ToList();//审批人
                for (int i = 0; i < ir; i++)
                {
                    Model.WPQ_WPQList wpq = new Model.WPQ_WPQList();
                    string col0 = pds.Rows[i][0].ToString().Trim();
                    string col1 = pds.Rows[i][1].ToString().Trim();
                    string col2 = pds.Rows[i][2].ToString().Trim();
                    string col3 = pds.Rows[i][3].ToString().Trim();
                    string col4 = pds.Rows[i][4].ToString().Trim();
                    string col5 = pds.Rows[i][5].ToString().Trim();
                    string col6 = pds.Rows[i][6].ToString().Trim();
                    string col7 = pds.Rows[i][7].ToString().Trim();
                    string col8 = pds.Rows[i][8].ToString().Trim();
                    string col9 = pds.Rows[i][9].ToString().Trim();
                    string col10 = pds.Rows[i][10].ToString().Trim();
                    string col11 = pds.Rows[i][11].ToString().Trim();
                    string col12 = pds.Rows[i][12].ToString().Trim();
                    string col13 = pds.Rows[i][13].ToString().Trim();
                    string col14 = pds.Rows[i][14].ToString().Trim();
                    string col15 = pds.Rows[i][15].ToString().Trim();
                    string col16 = pds.Rows[i][16].ToString().Trim();
                    string col17 = pds.Rows[i][17].ToString().Trim();
                    string col18 = pds.Rows[i][18].ToString().Trim();
                    string col19 = pds.Rows[i][19].ToString().Trim();
                    string col20 = pds.Rows[i][20].ToString().Trim();
                    string col21 = pds.Rows[i][21].ToString().Trim();
                    string col22 = pds.Rows[i][22].ToString().Trim();
                    string col23 = pds.Rows[i][23].ToString().Trim();
                    string col24 = pds.Rows[i][24].ToString().Trim();
                    string col25 = pds.Rows[i][25].ToString().Trim();
                    //string col26 = pds.Rows[i][26].ToString().Trim();
                    //string col27 = pds.Rows[i][27].ToString().Trim();
                    //string col28 = pds.Rows[i][28].ToString().Trim();
                    //string col29 = pds.Rows[i][29].ToString().Trim();
                    wpq.WPQCode = col0;  //评定编号
                    if (!string.IsNullOrEmpty(col1))//编制单位
                    {
                        var unit = units.FirstOrDefault(e => e.UnitName == col1);
                        if (unit != null)
                        {
                            wpq.UnitId = unit.UnitId;
                        }
                    }
                    if (!string.IsNullOrEmpty(col2))//材质1
                    {
                        var steel = steels.FirstOrDefault(e => e.MaterialCode == col2);
                        if (steel != null)
                        {
                            wpq.MaterialId1 = steel.MaterialId;
                        }
                    }
                    if (!string.IsNullOrEmpty(col3))//材质2
                    {
                        var steel = steels.FirstOrDefault(e => e.MaterialCode == col3);
                        if (steel != null)
                        {
                            wpq.MaterialId2 = steel.MaterialId;
                        }
                    }
                    wpq.Specifications = col4;//规格
                    if (!string.IsNullOrEmpty(col5))//焊丝
                    {
                        var consumable = consumables.FirstOrDefault(e => e.ConsumablesName == col5);
                        if (consumable != null)
                        {
                            wpq.WeldingWire = consumable.ConsumablesId;
                        }
                    }
                    if (!string.IsNullOrEmpty(col6))//焊条
                    {
                        var consumable = consumables.FirstOrDefault(e => e.ConsumablesName == col6);
                        if (consumable != null)
                        {
                            wpq.WeldingRod = consumable.ConsumablesId;
                        }
                    }
                    if (!string.IsNullOrEmpty(col7))//坡口类型
                    {
                        var GrooveTypes = GrooveType.FirstOrDefault(e => e.GrooveTypeCode == col7);
                        if (GrooveTypes != null)
                        {
                            wpq.GrooveType = GrooveTypes.GrooveTypeId;
                        }
                    }
                    wpq.JointType = col8;//接头形式
                    if (!string.IsNullOrEmpty(col9))//焊接方法
                    {
                        var weldMethod = weldMethods.FirstOrDefault(e => e.WeldingMethodCode == col9);
                        if (weldMethod != null)
                        {
                            wpq.WeldingMethodId = weldMethod.WeldingMethodId;
                        }
                    }
                    wpq.MinImpactDia = Funs.GetNewDecimal(col10);//外径最小值
                    wpq.MaxImpactDia = Funs.GetNewDecimal(col11);//外径最大值
                    wpq.MinImpactThickness = Funs.GetNewDecimal(col12);//冲击时覆盖厚度最小值
                    wpq.MaxImpactThickness = Funs.GetNewDecimal(col13);//冲击时覆盖厚度最大值
                    wpq.MinCImpactDia = Funs.GetNewDecimal(col14);//冲击时覆盖厚度最小值
                    wpq.MaxCImpactDia = Funs.GetNewDecimal(col15);//冲击时覆盖厚度最大值
                    wpq.NoMinImpactThickness = Funs.GetNewDecimal(col16);//不冲击时覆盖厚度最小值
                    wpq.NoMaxImpactThickness = Funs.GetNewDecimal(col17);//不冲击时覆盖厚度最大值
                    wpq.WeldingPosition = col18;//焊接位置
                    if (col19 == "是")//是否热处理
                    {
                        wpq.IsHotProess = true;
                    }
                    else
                    {
                        wpq.IsHotProess = false;
                    }
                    wpq.WPQStandard = col20;
                    wpq.ProtectiveGas = col21;//保护气体
                    wpq.CompileDate =Funs.GetNewDateTime(col22);//编制时间；
                    wpq.PreTemperature = col23;//预热温度；
                    wpq.Remark = col24; //备注
                    wpq.ApproveManId = users.FirstOrDefault(x => x.UserName == col25).UserId;//审批人
                    wpq.WPQId = SQLHelper.GetNewID(typeof(Model.WPQ_WPQList));
                    weldingProcedures.Add(wpq);
                }
                if (weldingProcedures.Count > 0)
                {
                    this.Grid1.Hidden = false;
                    this.Grid1.DataSource = weldingProcedures;
                    this.Grid1.DataBind();
                }
            }
            else
            {
                ShowNotify("导入数据为空！", MessageBoxIcon.Warning);
            }
            return true;
        }
        #endregion
        #endregion
        #region 提交
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(errorInfos))
            {
                int a = weldingProcedures.Count();
                for (int i = 0; i < a; i++)
                {
                    var isExistWPQCode = BLL.WPQListServiceService.IsWPQCode(weldingProcedures[i].WPQId, weldingProcedures[i].WPQCode);
                    if (!isExistWPQCode)
                    {
                        Model.WPQ_WPQList wpq = new Model.WPQ_WPQList();
                        wpq.WPQId = weldingProcedures[i].WPQId;
                        wpq.WPQCode = weldingProcedures[i].WPQCode;
                        wpq.UnitId = weldingProcedures[i].UnitId;
                        wpq.CompileDate = DateTime.Now;
                        wpq.MaterialId1 = weldingProcedures[i].MaterialId1;
                        var material1 = BLL.Base_MaterialService.GetMaterialByMaterialId(wpq.MaterialId1);
                        if (material1 != null)
                        {
                            wpq.Material1Class = material1.MaterialClass;
                            wpq.Material1Group = material1.MaterialGroup;
                        }
                        wpq.MaterialId2 = weldingProcedures[i].MaterialId2;
                        var material2 = BLL.Base_MaterialService.GetMaterialByMaterialId(wpq.MaterialId2);
                        if (material2 != null)
                        {
                            wpq.Material2Class = material2.MaterialClass;
                            wpq.Material2Group = material2.MaterialGroup;
                        }
                        //wpq.Material1Class = weldingProcedures[i].Material1Class;
                        //wpq.Material1Group = weldingProcedures[i].Material1Group;
                        //wpq.Material2Class = weldingProcedures[i].Material2Class;
                        //wpq.Material2Group = weldingProcedures[i].Material2Group;
                        wpq.Specifications = weldingProcedures[i].Specifications;
                        wpq.WeldingRod = weldingProcedures[i].WeldingRod;
                        wpq.WeldingWire = weldingProcedures[i].WeldingWire;
                        wpq.GrooveType = weldingProcedures[i].GrooveType;
                        wpq.WeldingPosition = weldingProcedures[i].WeldingPosition;
                        wpq.WeldingMethodId = weldingProcedures[i].WeldingMethodId;
                        wpq.MinImpactDia = weldingProcedures[i].MinImpactDia;
                        wpq.MaxImpactDia = weldingProcedures[i].MaxImpactDia;
                        wpq.MinImpactThickness = weldingProcedures[i].MinImpactThickness;
                        wpq.MaxImpactThickness = weldingProcedures[i].MaxImpactThickness;
                        wpq.NoMinImpactThickness = weldingProcedures[i].NoMinImpactThickness;
                        wpq.NoMaxImpactThickness = weldingProcedures[i].NoMaxImpactThickness;
                        wpq.MinCImpactDia = weldingProcedures[i].MinCImpactDia;
                        wpq.MaxCImpactDia = weldingProcedures[i].MaxCImpactDia;
                        wpq.IsHotProess = weldingProcedures[i].IsHotProess;
                        wpq.WPQStandard = weldingProcedures[i].WPQStandard;
                        wpq.PreTemperature = weldingProcedures[i].PreTemperature;
                        wpq.Remark = weldingProcedures[i].Remark;
                        wpq.JointType = weldingProcedures[i].JointType;
                        wpq.ProtectiveGas = weldingProcedures[i].ProtectiveGas;
                        wpq.State = "1";
                        wpq.ApproveManId= weldingProcedures[i].ApproveManId;
                        BLL.WPQListServiceService.AddWPQ(wpq);
                        SaveFlowOperate(wpq.WPQId, "施工单位编制");
               
                    }
                    else {
                        ShowNotify("存在相同批次的评定编号，请修正后重新提交！", MessageBoxIcon.Warning);
                        return;
                    }
                }
                string rootPath = Server.MapPath("~/");
                string initFullPath = rootPath + initPath;
                string filePath = initFullPath + this.hdFileName.Text;
                if (filePath != string.Empty && System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);//删除上传的XLS文件
                }
                ShowNotify("导入成功！", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                ShowNotify("请先将错误数据修正，再重新导入提交！", MessageBoxIcon.Warning);
            }
        }
        #region 保存流程审核数据
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="menuId">菜单id</param>
        /// <param name="dataId">主键id</param>
        /// <param name="isClosed">是否关闭这步流程</param>
        /// <param name="content">单据内容</param>
        /// <param name="url">路径</param>
        public void SaveFlowOperate(string WpqId, string OperateName)
        {
            Model.WPQ_WPQListFlowOperate newFlowOperate = new Model.WPQ_WPQListFlowOperate();
            newFlowOperate.FlowOperateId = SQLHelper.GetNewID(typeof(Model.WPQ_WPQListFlowOperate));
            newFlowOperate.WPQId = WpqId;
            newFlowOperate.OperateName = OperateName;
            newFlowOperate.OperateManId = CurrUser.UserId;
            newFlowOperate.OperateTime = DateTime.Now;
            Funs.DB.WPQ_WPQListFlowOperate.InsertOnSubmit(newFlowOperate);
            Funs.DB.SubmitChanges();

        }
        #endregion
        #endregion

        #region 关闭弹出窗口
        /// <summary>
        /// 关闭导入弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            if (Session["weldingProcedures"] != null)
            {
                weldingProcedures = Session["weldingProcedures"] as List<Model.WPQ_WPQList>;
            }
            if (weldingProcedures.Count > 0)
            {
                this.Grid1.Hidden = false;
                this.Grid1.DataSource = weldingProcedures;
                this.Grid1.DataBind();
            }
        }
        #endregion

        #region 下载模板
        /// <summary>
        /// 下载模板按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDownLoad_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Confirm.GetShowReference("确定下载导入模板吗？", String.Empty, MessageBoxIcon.Question, PageManager1.GetCustomEventReference(false, "Confirm_OK"), PageManager1.GetCustomEventReference("Confirm_Cancel")));
        }

        /// <summary>
        /// 下载导入模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PageManager1_CustomEvent(object sender, CustomEventArgs e)
        {
            if (e.EventArgument == "Confirm_OK")
            {
                string rootPath = Server.MapPath("~/");
                string uploadfilepath = rootPath + Const.WPQTemplateUrl;
                string filePath = Const.WPQTemplateUrl;
                string fileName = Path.GetFileName(filePath);
                FileInfo info = new FileInfo(uploadfilepath);
                long fileSize = info.Length;
                Response.ClearContent();
                Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                Response.ContentType = "excel/plain";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.AddHeader("Content-Length", fileSize.ToString().Trim());
                Response.TransmitFile(uploadfilepath, 0, fileSize);
                Response.End();
            }
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 获取材质名称
        /// </summary>
        /// <param name="steId"></param>
        /// <returns></returns>
        protected string ConvertMaterial(object matId)
        {
            if (matId != null)
            {
                var ste = BLL.Base_MaterialService.GetMaterialByMaterialId(matId.ToString());
                if (ste != null)
                {
                    return ste.MaterialCode;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取焊丝焊条
        /// </summary>
        /// <param name="wme_id"></param>
        /// <returns></returns>
        protected string ConvertConsumables(object id)
        {
            if (id != null)
            {
                var consumables = BLL.Base_ConsumablesService.GetConsumablesByConsumablesId(id.ToString());
                if (consumables != null)
                {
                    return consumables.ConsumablesName;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取坡口类型
        /// </summary>
        /// <param name="wme_id"></param>
        /// <returns></returns>
        protected string ConvertGrooveType(object id)
        {
            if (id != null)
            {
                var grooveType = BLL.Base_GrooveTypeService.GetGrooveTypeByGrooveTypeId(id.ToString());
                if (grooveType != null)
                {
                    return grooveType.GrooveTypeCode;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取焊接方法
        /// </summary>
        /// <param name="wme_id"></param>
        /// <returns></returns>
        protected string ConvertWeldMethod(object wme_id)
        {
            if (wme_id != null)
            {
                var weldMethod = BLL.Base_WeldingMethodService.GetWeldingMethodByWeldingMethodId(wme_id.ToString());
                if (weldMethod != null)
                {
                    return weldMethod.WeldingMethodCode;
                }
            }
            return null;
        }

        #endregion
    }
}