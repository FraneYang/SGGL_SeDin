using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;

namespace FineUIPro.Web.HSSE.Technique
{
    public partial class EnvironmentalIn : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 上传预设的虚拟路径
        /// </summary>
        private string initPath = Const.ExcelUrl;

        /// <summary>
        /// 环境危险源集合
        /// </summary>
        public static List<Model.Technique_Environmental> environmentals = new List<Model.Technique_Environmental>();

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
                if (environmentals != null)
                {
                    environmentals.Clear();
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
                if (environmentals != null)
                {
                    environmentals.Clear();
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
            try
            {
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

                AddDatasetToSQL(ds.Tables[0], 13);
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
                var environmentalSmallTypes = from x in Funs.DB.Sys_Const where x.GroupId == ConstValue.Group_EnvironmentalSmallType select x;//危险源类型   
                var environmentalTypes = from x in Funs.DB.Sys_Const where x.GroupId == ConstValue.Group_EnvironmentalType select x;//环境类型             
                for (int i = 0; i < ir; i++)
                {
                    string col0 = pds.Rows[i][0].ToString();
                    if (string.IsNullOrEmpty(col0))
                    {
                        result += "第" + (i + 2).ToString() + "行," + "危险源代码" + "," + "此项为必填项！" + "|";
                    }
                    string col1 = pds.Rows[i][1].ToString();
                    if (!string.IsNullOrEmpty(col1))
                    {
                        Model.Sys_Const environmentalSmallType = environmentalSmallTypes.FirstOrDefault(e => e.ConstText == col1);
                        if (environmentalSmallType == null)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "危险源类型" + "," + "[" + col1 + "]错误！" + "|";
                        }
                    }
                    else
                    {
                        result += "第" + (i + 2).ToString() + "行," + "危险源类型" + "," + "此项为必填项！" + "|";
                    }
                    string col2 = pds.Rows[i][2].ToString();
                    if (!string.IsNullOrEmpty(col2))
                    {
                        Model.Sys_Const environmentalType = environmentalTypes.FirstOrDefault(e => e.ConstText == col2);
                        if (environmentalType == null)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "环境类型" + "," + "[" + col2 + "]错误！" + "|";
                        }
                    }
                    string col3 = pds.Rows[i][3].ToString();
                    if (string.IsNullOrEmpty(col3))
                    {
                        result += "第" + (i + 2).ToString() + "行," + "分项工程/活动点" + "," + "此项为必填项！" + "|";
                    }
                    string col4 = pds.Rows[i][4].ToString();
                    if (string.IsNullOrEmpty(col4))
                    {
                        result += "第" + (i + 2).ToString() + "行," + "环境因素" + "," + "此项为必填项！" + "|";
                    }
                    string col5 = pds.Rows[i][5].ToString();
                    if (!string.IsNullOrEmpty(col5))
                    {
                        if (col5 != "1" && col5 != "2" && col5 != "3" && col5 != "4" && col5 != "5")
                        {
                            result += "第" + (i + 2).ToString() + "行," + "A值" + "," + "[" + col5 + "]错误！" + "|";
                        }
                    }
                    string col6 = pds.Rows[i][6].ToString();
                    if (!string.IsNullOrEmpty(col6))
                    {
                        if (col6 != "1" && col6 != "3" && col6 != "5")
                        {
                            result += "第" + (i + 2).ToString() + "行," + "B值" + "," + "[" + col5 + "]错误！" + "|";
                        }
                    }
                    string col7 = pds.Rows[i][7].ToString();
                    if (!string.IsNullOrEmpty(col7))
                    {
                        if (col7 != "1" && col7 != "2" && col7 != "3" && col7 != "4" && col7 != "5")
                        {
                            result += "第" + (i + 2).ToString() + "行," + "C值" + "," + "[" + col7 + "]错误！" + "|";
                        }
                    }
                    string col8 = pds.Rows[i][8].ToString();
                    if (!string.IsNullOrEmpty(col8))
                    {
                        if (col8 != "1" && col8 != "3" && col8 != "5")
                        {
                            result += "第" + (i + 2).ToString() + "行," + "D值" + "," + "[" + col8 + "]错误！" + "|";
                        }
                    }
                    string col9 = pds.Rows[i][9].ToString();
                    if (!string.IsNullOrEmpty(col9))
                    {
                        if (col9 != "1" && col9 != "2" && col9 != "3" && col9 != "4" && col9 != "5")
                        {
                            result += "第" + (i + 2).ToString() + "行," + "E值" + "," + "[" + col9 + "]错误！" + "|";
                        }
                    }
                    string col10 = pds.Rows[i][10].ToString();
                    if (!string.IsNullOrEmpty(col10))
                    {
                        if (col10 != "1" && col10 != "3" && col10 != "5")
                        {
                            result += "第" + (i + 2).ToString() + "行," + "F值" + "," + "[" + col10 + "]错误！" + "|";
                        }
                    }
                    string col11 = pds.Rows[i][11].ToString();
                    if (!string.IsNullOrEmpty(col11))
                    {
                        if (col11 != "1" && col11 != "3" && col11 != "5")
                        {
                            result += "第" + (i + 2).ToString() + "行," + "G值" + "," + "[" + col11 + "]错误！" + "|";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(result))
                {
                    result = result.Substring(0, result.LastIndexOf("|"));
                    errorInfos = result;
                    Alert alert = new Alert
                    {
                        Message = result,
                        Target = Target.Self
                    };
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
                ShowNotify("请先将错误数据修正，再重新导入保存！", MessageBoxIcon.Warning);
            }
        }

        #region Excel提取数据
        /// <summary>
        /// 从Excel提取数据--》Dataset
        /// </summary>
        /// <param name="filename">Excel文件路径名</param>
        private void ImportXlsToData2(string fileName)
        {
            try
            {
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

                AddDatasetToSQL2(ds.Tables[0], 13);
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
            environmentals.Clear();
            ic = pds.Columns.Count;
            if (ic < Cols)
            {
                ShowNotify("导入Excel格式错误！Excel只有" + ic.ToString().Trim() + "列", MessageBoxIcon.Warning);
            }

            ir = pds.Rows.Count;
            if (pds != null && ir > 0)
            {
                var environmentalSmallTypes = from x in Funs.DB.Sys_Const where x.GroupId == ConstValue.Group_EnvironmentalSmallType select x;//危险源类型   
                var environmentalTypes = from x in Funs.DB.Sys_Const where x.GroupId == ConstValue.Group_EnvironmentalType select x;//环境类型           
                for (int i = 0; i < ir; i++)
                {
                    Model.Technique_Environmental environmental = new Model.Technique_Environmental();
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

                    if (!string.IsNullOrEmpty(col0))//危险源代码
                    {
                        environmental.Code = col0;
                    }
                    if (!string.IsNullOrEmpty(col1))//危险源类型
                    {
                        var environmentalSmallType = environmentalSmallTypes.FirstOrDefault(e => e.ConstText == col1);
                        if (environmentalSmallType != null)
                        {
                            environmental.SmallType = environmentalSmallType.ConstValue;
                        }
                    }
                    if (!string.IsNullOrEmpty(col2))//环境类型
                    {
                        var environmentalType = environmentalTypes.FirstOrDefault(e => e.ConstText == col2);
                        if (environmentalType != null)
                        {
                            environmental.EType = environmentalType.ConstValue;
                        }
                    }
                    if (!string.IsNullOrEmpty(col3))//分项工程/活动点
                    {
                        environmental.ActivePoint = col3;
                    }
                    if (!string.IsNullOrEmpty(col4))//环境因素
                    {
                        environmental.EnvironmentalFactors = col4;
                    }
                    if (!string.IsNullOrEmpty(col5))//A值
                    {
                        environmental.AValue = Funs.GetNewIntOrZero(col5);
                    }
                    if (!string.IsNullOrEmpty(col6))//B值
                    {
                        environmental.BValue = Funs.GetNewIntOrZero(col6);
                    }
                    if (!string.IsNullOrEmpty(col7))//C值
                    {
                        environmental.CValue = Funs.GetNewIntOrZero(col7);
                    }
                    if (!string.IsNullOrEmpty(col8))//D值
                    {
                        environmental.DValue = Funs.GetNewIntOrZero(col8);
                    }
                    if (!string.IsNullOrEmpty(col9))//E值
                    {
                        environmental.EValue = Funs.GetNewIntOrZero(col9);
                    }
                    if (!string.IsNullOrEmpty(col10))//F值
                    {
                        environmental.FValue = Funs.GetNewIntOrZero(col10);
                    }
                    if (!string.IsNullOrEmpty(col11))//G值
                    {
                        environmental.GValue = Funs.GetNewIntOrZero(col11);
                    }
                    if (!string.IsNullOrEmpty(col12))//安全措施
                    {
                        environmental.ControlMeasures = col12;
                    }
                    environmental.ZValue = Funs.GetNewIntOrZero(col5) + Funs.GetNewIntOrZero(col6) + Funs.GetNewIntOrZero(col7) + Funs.GetNewIntOrZero(col8) + Funs.GetNewIntOrZero(col9);
                    //是否重要
                    if ((environmental.AValue == 5) || (environmental.BValue == 5) || (environmental.DValue == 5) || (environmental.ZValue) >= 15 || (environmental.FValue) == 5 || (environmental.GValue == 5) || (environmental.FValue + environmental.GValue > 7))
                    {
                        environmental.IsImportant = true;
                    }
                    else
                    {
                        environmental.IsImportant = false;
                    }
                    environmental.IsCompany = Convert.ToBoolean(Request.Params["IsCompany"]);//是否本公司
                    environmental.EnvironmentalId = SQLHelper.GetNewID(typeof(Model.Technique_Environmental));
                    environmentals.Add(environmental);
                }
                if (environmentals.Count > 0)
                {
                    this.Grid1.Hidden = false;
                    this.Grid1.DataSource = environmentals;
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

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(errorInfos))
            {
                int a = environmentals.Count();
                for (int i = 0; i < a; i++)
                {
                    var isExistHazardCode = BLL.EnvironmentalService.IsEnvironmentalCode(environmentals[i].EnvironmentalId, environmentals[i].Code, Convert.ToBoolean(Request.Params["IsCompany"]));
                    if (!isExistHazardCode)
                    {
                        Model.Technique_Environmental environmental = new Model.Technique_Environmental
                        {
                            EnvironmentalId = environmentals[i].EnvironmentalId,
                            EType = environmentals[i].EType,
                            ActivePoint = environmentals[i].ActivePoint,
                            EnvironmentalFactors = environmentals[i].EnvironmentalFactors,
                            AValue = environmentals[i].AValue,
                            BValue = environmentals[i].BValue,
                            CValue = environmentals[i].CValue,
                            DValue = environmentals[i].DValue,
                            EValue = environmentals[i].EValue,
                            ZValue = environmentals[i].ZValue,
                            SmallType = environmentals[i].SmallType,
                            IsImportant = environmentals[i].IsImportant,
                            Code = environmentals[i].Code,
                            FValue = environmentals[i].FValue,
                            GValue = environmentals[i].GValue,
                            ControlMeasures = environmentals[i].ControlMeasures,
                            IsCompany = environmentals[i].IsCompany
                        };
                        BLL.EnvironmentalService.AddEnvironmental(environmental);
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
                ShowNotify("请先将错误数据修正，再重新导入保存！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 关闭弹出窗口
        /// <summary>
        /// 关闭导入弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            if (Session["environmentals"] != null)
            {
                environmentals = Session["environmentals"] as List<Model.Technique_Environmental>;
            }
            if (environmentals.Count > 0)
            {
                this.Grid1.Hidden = false;
                this.Grid1.DataSource = environmentals;
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
                string uploadfilepath = rootPath + Const.EnvironmentalTemplateUrl;
                string filePath = Const.EnvironmentalTemplateUrl;
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
        /// 获取危险源类型名称
        /// </summary>
        /// <param name="smallType"></param>
        /// <returns></returns>
        protected string ConvertSmallType(object smallType)
        {
            if (smallType != null)
            {
                Model.Sys_Const environmentalSmallType = BLL.ConstValue.GetConstByConstValueAndGroupId(smallType.ToString(), BLL.ConstValue.Group_EnvironmentalSmallType);
                if (environmentalSmallType != null)
                {
                    return environmentalSmallType.ConstText;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取环境类型名称
        /// </summary>
        /// <param name="eType"></param>
        /// <returns></returns>
        protected string ConvertEType(object eType)
        {
            if (eType!=null)
            {
                Model.Sys_Const environmentalType = BLL.ConstValue.GetConstByConstValueAndGroupId(eType.ToString(), BLL.ConstValue.Group_EnvironmentalType);
                if (environmentalType!=null)
                {
                    return environmentalType.ConstText;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取ZValue2的值
        /// </summary>
        /// <param name="environmentalId"></param>
        /// <returns></returns>
        protected string ConvertZValue2(object environmentalId)
        {
            if (environmentalId != null)
            {
                var environmental = environmentals.FirstOrDefault(e => e.EnvironmentalId == environmentalId.ToString());
                if (environmental != null)
                {
                    return Convert.ToString(environmental.GValue + environmental.FValue);
                }
            }
            return "0";
        }
        #endregion
    }
}