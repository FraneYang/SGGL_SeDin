using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class PipingClassIn : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 上传预设的虚拟路径
        /// </summary>
        private string initPath = Const.ExcelUrl;

        /// <summary>
        /// 焊接工艺评定台账集合
        /// </summary>
        public static List<Model.Base_PipingClass> PipingClassList = new List<Model.Base_PipingClass>();

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
                if (PipingClassList != null)
                {
                    PipingClassList.Clear();
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
                if (PipingClassList != null)
                {
                    PipingClassList.Clear();
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
            //支持.xls和.xlsx，即包括office2010等版本的   HDR=Yes代表第一行是标题，不是数据；
            //string cmdText = "Provider=Microsoft.ACE.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0; HDR=Yes; IMEX=1'";

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

                AddDatasetToSQL(ds.Tables[0], 4);
            }
            catch (Exception exc)
            {
                Response.Write(exc);
                //return null;
                // return dt;
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
                ShowNotify("导入Excel格式错误！Excel只有" + ic.ToString().Trim() + "列", MessageBoxIcon.Warning);
                return false;
            }

            ir = pds.Rows.Count;
            if (pds != null && ir > 0)
            {
                var q = from x in Funs.DB.Base_PipingClass where x.ProjectId == this.CurrUser.LoginProjectId select x;
                var q2 = from x in Funs.DB.Base_PipingClass where x.ProjectId == this.CurrUser.LoginProjectId select x;
                for (int i = 0; i < ir; i++)
                {
                    string col0 = pds.Rows[i][0].ToString();
                    if (string.IsNullOrEmpty(col0))
                    {
                        result += "第" + (i + 2).ToString() + "行," + "管道等级代号" + "," + "此项为必填项！" + "|";
                    }
                    else
                    {
                        var code = q.FirstOrDefault(x => x.PipingClassCode == col0);
                        if (code != null)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "此等级代号已存在" + "|";
                        }
                    }
                    string col1 = pds.Rows[i][1].ToString();
                    if (string.IsNullOrEmpty(col1))
                    {
                        result += "第" + (i + 2).ToString() + "行," + "管道等级名称" + "," + "此项为必填项！" + "|";
                    }
                    else
                    {
                        var code = q.FirstOrDefault(x => x.PipingClassName == col1);
                        if (code != null)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "此等级名称已存在" + "|";
                        }
                    }
                    string col2 = pds.Rows[i][2].ToString();
                    if (string.IsNullOrEmpty(col1))
                    {
                        result += "第" + (i + 2).ToString() + "行," + "钢材类型" + "," + "此项为必填项！" + "|";
                    }
                    else
                    {
                        if (col2 != "碳钢" && col2 != "合金钢" && col2 != "不锈钢")
                        {
                            result += "第" + (i + 2).ToString() + "行," + "钢材类型错误" + "|";
                        }
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
            //支持.xls和.xlsx，即包括office2010等版本的   HDR=Yes代表第一行是标题，不是数据；
            //string cmdText = "Provider=Microsoft.ACE.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0; HDR=Yes; IMEX=1'";
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
                AddDatasetToSQL2(ds.Tables[0], 4);
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
            PipingClassList.Clear();
            ic = pds.Columns.Count;
            if (ic < Cols)
            {
                ShowNotify("导入Excel格式错误！Excel只有" + ic.ToString().Trim() + "列", MessageBoxIcon.Warning);
            }

            ir = pds.Rows.Count;
            if (pds != null && ir > 0)
            {
                for (int i = 0; i < ir; i++)
                {
                    string col0 = pds.Rows[i][0].ToString().Trim();
                    string col1 = pds.Rows[i][1].ToString().Trim();
                    string col2 = pds.Rows[i][2].ToString().Trim();
                    string col3 = pds.Rows[i][3].ToString().Trim();
                    Model.Base_PipingClass newPipingClass = new Model.Base_PipingClass
                    {
                        PipingClassId = SQLHelper.GetNewID(typeof(Model.Base_PipingClass)),
                        PipingClassCode = col0,
                        PipingClassName = col1,
                        SteelType=col2,
                        Remark = col3,
                        ProjectId = this.CurrUser.LoginProjectId
                    };

                    PipingClassList.Add(newPipingClass);
                } 
                if (PipingClassList.Count > 0)
                {
                    this.Grid1.Hidden = false;
                    this.Grid1.DataSource = PipingClassList;
                    this.Grid1.DataBind();
                    Grid1.RecordCount = PipingClassList.Count;
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
                int a = PipingClassList.Count();
                for (int i = 0; i < a; i++)
                {
                    var isExistPipingClassCode = Funs.DB.Base_PipingClass.FirstOrDefault(x => (x.PipingClassId != PipingClassList[i].PipingClassId || (PipingClassList[i].PipingClassId == null && x.PipingClassId != null)) && x.PipingClassCode == PipingClassList[i].PipingClassCode);
                    var isExistPipingClassName = Funs.DB.Base_PipingClass.FirstOrDefault(x => (x.PipingClassId != PipingClassList[i].PipingClassId || (PipingClassList[i].PipingClassId == null && x.PipingClassId != null)) && x.PipingClassName == PipingClassList[i].PipingClassName);
                    if (isExistPipingClassCode != null)
                    {
                        ShowNotify("存在相同批次的管道等级代号，请修正后重新提交！", MessageBoxIcon.Warning);
                        return;
                    }
                    else if (isExistPipingClassName != null)
                    {
                        ShowNotify("存在相同批次的管道等级名称，请修正后重新提交！", MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        Model.Base_PipingClass newPipingClass = new Model.Base_PipingClass
                        {
                            PipingClassId = PipingClassList[i].PipingClassId,
                            PipingClassCode = PipingClassList[i].PipingClassCode,
                            PipingClassName = PipingClassList[i].PipingClassName,
                            SteelType= PipingClassList[i].SteelType,
                            Remark = PipingClassList[i].Remark,
                            ProjectId = this.CurrUser.LoginProjectId
                        };
                        BLL.Base_PipingClassService.AddPipingClass(newPipingClass);
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
                string uploadfilepath = rootPath + Const.PipingClassTemplateUrl;
                string filePath = Const.PipingClassTemplateUrl;
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

    }
}