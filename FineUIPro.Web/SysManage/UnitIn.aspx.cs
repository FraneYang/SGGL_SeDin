using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web.UI;

namespace FineUIPro.Web.SysManage
{
    public partial class UnitIn : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 上传预设的虚拟路径
        /// </summary>
        private string initPath = Const.ExcelUrl;

        /// <summary>
        /// 单位集合
        /// </summary>
        public static List<Model.Base_Unit> units = new List<Model.Base_Unit>();

        /// <summary>
        /// 错误集合
        /// </summary>
        public static string errorInfos = string.Empty;

        #endregion

        #region 加载页面
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
                this.hdCheckResult.Text = string.Empty;
                if (units != null)
                {
                    units.Clear();
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
                if (units != null)
                {
                    units.Clear();
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
                //PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PersonDataAudit.aspx?FileName={0}&ProjectId={1}", this.hdFileName.Text, Request.Params["ProjectId"], "审核 - ")));
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

                AddDatasetToSQL(ds.Tables[0], 10);
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
                Alert.ShowInTop("导入Excel格式错误！Excel只有" + ic.ToString().Trim() + "行", MessageBoxIcon.Warning);
            }
            ir = pds.Rows.Count;
            if (pds != null && ir > 0)
            {
                var unitTypes = from x in Funs.DB.Base_UnitType select x;
                for (int i = 0; i < ir; i++)
                {
                    string col0 = pds.Rows[i][0].ToString().Trim();
                    if (string.IsNullOrEmpty(col0))
                    {
                        result += "第" + (i + 2).ToString() + "行," + "单位代码" + "," + "此项为必填项！" + "|";
                    }
                    string col1 = pds.Rows[i][1].ToString().Trim();
                    if (string.IsNullOrEmpty(col1))
                    {
                        result += "第" + (i + 2).ToString() + "行," + "单位名称" + "," + "此项为必填项！" + "|";
                    }
                    string col2 = pds.Rows[i][2].ToString().Trim();
                    if (!string.IsNullOrEmpty(col2))
                    {
                        var unitType = unitTypes.FirstOrDefault(e => e.UnitTypeName == col2);
                        if (unitType == null)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "单位类型" + "," + "[" + col2 + "]不存在！" + "|";
                        }
                    }
                    string col9 = pds.Rows[i][9].ToString().Trim();
                    if (!string.IsNullOrEmpty(col9))
                    {
                        if (col9 != "是" && col9 != "否")
                        {
                            result += "第" + (i + 2).ToString() + "行," + "是否分公司" + "," + "[" + col9 + "]错误！" + "|";
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
                Alert.ShowInTop("请先将错误数据修正，再重新导入保存！", MessageBoxIcon.Warning);
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

                AddDatasetToSQL2(ds.Tables[0], 10);
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
            units.Clear();
            ic = pds.Columns.Count;
            if (ic < Cols)
            {
                Alert.ShowInTop("导入Excel格式错误！Excel只有" + ic.ToString().Trim() + "列", MessageBoxIcon.Warning);
            }

            ir = pds.Rows.Count;
            if (pds != null && ir > 0)
            {
                var unitTypes = from x in Funs.DB.Base_UnitType select x;
                for (int i = 0; i < ir; i++)
                {
                    Model.Base_Unit unit = new Model.Base_Unit();
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

                    if (!string.IsNullOrEmpty(col0))
                    {
                        unit.UnitCode = col0;
                    }
                    if (!string.IsNullOrEmpty(col1))
                    {
                        unit.UnitName = col1;
                    }
                    if (!string.IsNullOrEmpty(col2))
                    {
                        var unitType = unitTypes.FirstOrDefault(x => x.UnitTypeName == col2);
                        if (unitType != null)
                        {
                            unit.UnitTypeId = unitType.UnitTypeId;
                        }
                    }
                    if (!string.IsNullOrEmpty(col3))
                    {
                        unit.ProjectRange = col3;
                    }
                    if (!string.IsNullOrEmpty(col4))
                    {
                        unit.Corporate = col4;
                    }
                    if (!string.IsNullOrEmpty(col5))
                    {
                        unit.Address = col5;
                    }
                    if (!string.IsNullOrEmpty(col6))
                    {
                        unit.Telephone = col6;
                    }
                    if (!string.IsNullOrEmpty(col7))
                    {
                        unit.Fax = col7;
                    }
                    if (!string.IsNullOrEmpty(col8))
                    {
                        unit.EMail = col8;
                    }
                    if (!string.IsNullOrEmpty(col9))
                    {
                        if (col9 == "是")
                        {
                            unit.IsBranch = true;
                        }
                        else
                        {
                            unit.IsBranch = false;
                        }
                    }
                    unit.UnitId = SQLHelper.GetNewID(typeof(Model.Base_Unit));
                    units.Add(unit);
                }
                if (units.Count > 0)
                {
                    this.Grid1.Hidden = false;
                    this.Grid1.DataSource = units;
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
                int a = units.Count();
                for (int i = 0; i < a; i++)
                {
                    if (!BLL.UnitService.IsExitUnitByUnitName(units[i].UnitId, units[i].UnitName))
                    {
                        Model.Base_Unit newUnit = new Model.Base_Unit();
                        newUnit.UnitId = units[i].UnitId;
                        newUnit.UnitCode = units[i].UnitCode;
                        newUnit.UnitName = units[i].UnitName;
                        newUnit.UnitTypeId = units[i].UnitTypeId;
                        newUnit.ProjectRange = units[i].ProjectRange;
                        newUnit.Corporate = units[i].Corporate;
                        newUnit.Address = units[i].Address;
                        newUnit.Telephone = units[i].Telephone;
                        newUnit.Fax = units[i].Fax;
                        newUnit.EMail = units[i].EMail;
                        newUnit.IsBranch = units[i].IsBranch.ToString() == "是" ? true : false;
                        BLL.UnitService.AddUnit(newUnit);
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
                Alert.ShowInTop("请先将错误数据修正，再重新导入保存！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 导出错误提示
        /// <summary>
        /// 导出错误提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 重载VerifyRenderingInServerForm方法，否则运行的时候会出现如下错误提示：“类型“GridView”的控件“GridView1”必须放在具有 runat=server 的窗体标记内”
        /// </summary>
        /// <param name="control"></param>
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        #endregion

        #region 关闭弹出窗口
        /// <summary>
        /// 关闭审核弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {

        }

        /// <summary>
        /// 关闭导入弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            if (Session["units"] != null)
            {
                units = Session["units"] as List<Model.Base_Unit>;
            }
            if (units.Count > 0)
            {
                this.Grid1.Hidden = false;
                this.Grid1.DataSource = units;
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
                string uploadfilepath = rootPath + Const.UnitTemplateUrl;
                string filePath = Const.UnitTemplateUrl;
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
        /// 获取是否本单位
        /// </summary>
        /// <param name="isBranch"></param>
        /// <returns></returns>
        protected string ConvertIsBranch(object isBranch)
        {
            if (isBranch != null)
            {
                if (isBranch.ToString() == "True")
                {
                    return "是";
                }
                else
                {
                    return "否";
                }
            }
            return null;
        }
        #endregion
    }
}