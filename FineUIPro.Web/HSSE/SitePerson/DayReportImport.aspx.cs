using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.SitePerson
{
    public partial class DayReportImport : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 上传预设的虚拟路径
        /// </summary>
        private string initPath = Const.ExcelUrl;

        /// <summary>
        /// 人工时日报集合
        /// </summary>
        public static List<Model.View_DayRportView> dayRportViews = new List<Model.View_DayRportView>();

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
                if (dayRportViews != null)
                {
                    dayRportViews.Clear();
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
                if (dayRportViews != null)
                {
                    dayRportViews.Clear();
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

                AddDatasetToSQL(ds.Tables[0], 6);
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
                var units = from x in new Model.SGGLDB(Funs.ConnString).Base_Unit select x;
                var posts = from x in new Model.SGGLDB(Funs.ConnString).Base_WorkPost select x;
                for (int i = 0; i < ir; i++)
                {
                    string col0 = pds.Rows[i][0].ToString().Trim();
                    if (!string.IsNullOrEmpty(col0))
                    {
                        try
                        {
                            DateTime date = Convert.ToDateTime(col0);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "日报日期" + "," + "[" + col0 + "]错误！" + "|";
                        }
                    }
                    else
                    {
                        result += "第" + (i + 2).ToString() + "行," + "日报日期" + "," + "此项为必填项！" + "|";
                    }

                    string col1 = pds.Rows[i][1].ToString();
                    if (!string.IsNullOrEmpty(col1))
                    {
                        Model.Base_Unit unit = units.FirstOrDefault(e => e.UnitName == col1);
                        if (unit == null)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "单位名称" + "," + "[" + col1 + "]错误！" + "|";
                        }
                    }
                    else
                    {
                        result += "第" + (i + 2).ToString() + "行," + "单位名称" + "," + "此项为必填项！" + "|";
                    }

                    string col3 = pds.Rows[i][3].ToString();
                    if (!string.IsNullOrEmpty(col3))
                    {
                        Model.Base_WorkPost workPost = posts.FirstOrDefault(e => e.WorkPostName == col3);
                        if (workPost == null)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "岗位" + "," + "[" + col3 + "]错误！" + "|";
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

                AddDatasetToSQL2(ds.Tables[0], 6);
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
            dayRportViews.Clear();
            ic = pds.Columns.Count;
            if (ic < Cols)
            {
                ShowNotify("导入Excel格式错误！Excel只有" + ic.ToString().Trim() + "列", MessageBoxIcon.Warning);
            }

            ir = pds.Rows.Count;
            if (pds != null && ir > 0)
            {
                var units = from x in new Model.SGGLDB(Funs.ConnString).Base_Unit select x;
                var posts = from x in new Model.SGGLDB(Funs.ConnString).Base_WorkPost select x;
                for (int i = 0; i < ir; i++)
                {
                    Model.View_DayRportView dayRportView = new Model.View_DayRportView();
                    string col0 = pds.Rows[i][0].ToString().Trim();
                    string col1 = pds.Rows[i][1].ToString().Trim();
                    string col2 = pds.Rows[i][2].ToString().Trim();
                    string col3 = pds.Rows[i][3].ToString().Trim();
                    string col4 = pds.Rows[i][4].ToString().Trim();
                    string col5 = pds.Rows[i][5].ToString().Trim();
                    if (!string.IsNullOrEmpty(col0))
                    {
                        dayRportView.CompileDate = Funs.GetNewDateTime(col0.Trim());
                    }
                    if (!string.IsNullOrEmpty(col1))
                    {
                        dayRportView.UnitId = units.Where(x => x.UnitName == col1.Trim()).FirstOrDefault().UnitId;
                        dayRportView.UnitName = col1.Trim();
                    }
                    dayRportView.WorkTime = Funs.GetNewDecimalOrZero(col2);//平均工时
                    if (!string.IsNullOrEmpty(col3))
                    {
                        dayRportView.PostId = posts.Where(x => x.WorkPostName == col3.Trim()).FirstOrDefault().WorkPostId;
                        dayRportView.WorkPostName = col3.Trim();
                    }
                    dayRportView.RealPersonNum = (int)(Funs.GetNewDecimalOrZero(col4.Trim()));//实际人数
                    //dayRportView.RealPersonNum = Funs.GetNewIntOrZero(col4.Trim());//实际人数
                    dayRportView.PersonWorkTime = (dayRportView.WorkTime) * (dayRportView.RealPersonNum);//当日人工时
                    if (!BLL.SitePerson_DayReportService.IsExistDayReport(Convert.ToDateTime(dayRportView.CompileDate), this.CurrUser.LoginProjectId))
                    {
                        dayRportView.DayReportId = SQLHelper.GetNewID(typeof(Model.SitePerson_DayReport));
                    }
                    else
                    {
                        Alert.ShowInTop("当日日报已存在！", MessageBoxIcon.Warning);
                        return false;
                    }

                    dayRportViews.Add(dayRportView);
                }
                if (dayRportViews.Count > 0)
                {
                    this.Grid1.Hidden = false;
                    this.Grid1.DataSource = dayRportViews;
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
                if (dayRportViews.Count() > 0)
                {
                    if (!BLL.SitePerson_DayReportService.IsExistDayReport(Convert.ToDateTime(dayRportViews[0].CompileDate), this.CurrUser.LoginProjectId))
                    {
                        Model.SitePerson_DayReport newDayReport = new Model.SitePerson_DayReport
                        {
                            DayReportId = SQLHelper.GetNewID(typeof(Model.SitePerson_DayReport)),
                            ProjectId = this.CurrUser.LoginProjectId,
                            CompileMan = this.CurrUser.UserId,
                            CompileDate = dayRportViews[0].CompileDate,
                            States = BLL.Const.State_2
                        };
                        BLL.SitePerson_DayReportService.AddDayReport(newDayReport);
                        List<string> unitIds = (from x in dayRportViews
                                                select x.UnitId).Distinct().ToList();
                        List<Model.SitePerson_DayReportDetail> newDayReportDetails = new List<Model.SitePerson_DayReportDetail>();
                        foreach (var unitId in unitIds)
                        {
                            var detail = BLL.SitePerson_DayReportDetailService.GetDayReportDetailByUnit(unitId, newDayReport.DayReportId);
                            if (detail == null)
                            {
                                Model.SitePerson_DayReportDetail newDayReportDetail = new Model.SitePerson_DayReportDetail
                                {
                                    DayReportDetailId = SQLHelper.GetNewID(typeof(Model.SitePerson_DayReportDetail)),
                                    DayReportId = newDayReport.DayReportId,
                                    UnitId = unitId,
                                    WorkTime = (from x in dayRportViews where x.UnitId == unitId select x.WorkTime ?? 0).FirstOrDefault(),//平均工时                               
                                    RealPersonNum = (from x in dayRportViews where x.UnitId == unitId select x.RealPersonNum ?? 0).Sum()//实际人数
                                };
                                newDayReportDetail.PersonWorkTime = (newDayReportDetail.WorkTime) * (newDayReportDetail.RealPersonNum);//当日人工时数
                                BLL.SitePerson_DayReportDetailService.AddDayReportDetail(newDayReportDetail);
                                newDayReportDetails.Add(newDayReportDetail);
                            }
                        }
                        foreach (var unitDetail in dayRportViews)
                        {
                            Model.SitePerson_DayReportUnitDetail newDayReportUnitDetail = new Model.SitePerson_DayReportUnitDetail
                            {
                                DayReportUnitDetailId = SQLHelper.GetNewID(typeof(Model.SitePerson_DayReportUnitDetail)),
                                DayReportDetailId = (from x in newDayReportDetails where x.UnitId == unitDetail.UnitId select x.DayReportDetailId).FirstOrDefault(),
                                PostId = unitDetail.PostId,
                                RealPersonNum = unitDetail.RealPersonNum//实际人数
                            };
                            newDayReportUnitDetail.PersonWorkTime = ((from x in newDayReportDetails where x.UnitId == unitDetail.UnitId select x.WorkTime ?? 0).FirstOrDefault()) * (newDayReportUnitDetail.RealPersonNum);//当日人工时数
                            BLL.SitePerson_DayReportUnitDetailService.AddDayReportUnitDetail(newDayReportUnitDetail);
                        }
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
            if (Session["dayRportViews"] != null)
            {
                dayRportViews = Session["dayRportViews"] as List<Model.View_DayRportView>;
            }
            if (dayRportViews.Count > 0)
            {
                this.Grid1.Hidden = false;
                this.Grid1.DataSource = dayRportViews;
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
                string uploadfilepath = rootPath + Const.DayReportTemplateUrl2;
                string filePath = Const.DayReportTemplateUrl;
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