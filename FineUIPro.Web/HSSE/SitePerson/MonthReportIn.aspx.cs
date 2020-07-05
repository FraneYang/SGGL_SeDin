using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;

namespace FineUIPro.Web.HSSE.SitePerson
{
    public partial class MonthReportIn : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 上传预设的虚拟路径
        /// </summary>
        private string initPath = Const.ExcelUrl;

        /// <summary>
        /// 人工时月报集合
        /// </summary>
        public static List<Model.View_MonthReportView> monthReportViews = new List<Model.View_MonthReportView>();

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
                if (monthReportViews != null)
                {
                    monthReportViews.Clear();
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
                if (monthReportViews != null)
                {
                    monthReportViews.Clear();
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

                AddDatasetToSQL(ds.Tables[0], 7);
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
                var units = from x in Funs.DB.Base_Unit select x;
                var posts = from x in Funs.DB.Base_WorkPost select x;
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

                    string col4 = pds.Rows[i][4].ToString();
                    if (!string.IsNullOrEmpty(col4))
                    {
                        Model.Base_WorkPost workPost = posts.FirstOrDefault(e => e.WorkPostName == col4);
                        if (workPost == null)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "岗位" + "," + "[" + col4 + "]错误！" + "|";
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

                AddDatasetToSQL2(ds.Tables[0], 7);
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
            monthReportViews.Clear();
            ic = pds.Columns.Count;
            if (ic < Cols)
            {
                ShowNotify("导入Excel格式错误！Excel只有" + ic.ToString().Trim() + "列", MessageBoxIcon.Warning);
            }

            ir = pds.Rows.Count;
            if (pds != null && ir > 0)
            {
                var units = from x in Funs.DB.Base_Unit select x;
                var posts = from x in Funs.DB.Base_WorkPost select x;
                for (int i = 0; i < ir; i++)
                {
                    Model.View_MonthReportView monthReportView = new Model.View_MonthReportView();
                    string col0 = pds.Rows[i][0].ToString().Trim();
                    string col1 = pds.Rows[i][1].ToString().Trim();
                    string col2 = pds.Rows[i][2].ToString().Trim();
                    string col3 = pds.Rows[i][3].ToString().Trim();
                    string col4 = pds.Rows[i][4].ToString().Trim();
                    string col5 = pds.Rows[i][5].ToString().Trim();
                    string col6 = pds.Rows[i][6].ToString().Trim();
                    if (!string.IsNullOrEmpty(col0))
                    {
                        monthReportView.CompileDate = Funs.GetNewDateTime(col0.Trim());
                    }
                    if (!string.IsNullOrEmpty(col1))
                    {
                        monthReportView.UnitId = units.Where(x => x.UnitName == col1.Trim()).FirstOrDefault().UnitId;
                        monthReportView.UnitName = col1.Trim();
                    }
                    monthReportView.StaffData = col2;
                    monthReportView.WorkTime = Funs.GetNewDecimalOrZero(col3);//平均工时
                    if (!string.IsNullOrEmpty(col4))
                    {
                        monthReportView.PostId = posts.Where(x => x.WorkPostName == col4.Trim()).FirstOrDefault().WorkPostId;
                        monthReportView.WorkPostName = col4.Trim();
                    }
                    monthReportView.CheckPersonNum = Funs.GetNewIntOrZero(col5.Trim());
                    monthReportView.RealPersonNum = Funs.GetNewIntOrZero(col6.Trim());//实际人数
                    monthReportView.PersonWorkTime = (monthReportView.WorkTime) * (monthReportView.RealPersonNum);//当日人工时
                    if (!BLL.SitePerson_MonthReportService.IsExistMonthReport(Convert.ToDateTime(monthReportView.CompileDate), this.CurrUser.LoginProjectId))
                    {
                        monthReportView.MonthReportId = SQLHelper.GetNewID(typeof(Model.SitePerson_MonthReport));
                    }
                    else
                    {
                        Alert.ShowInTop("当月月报已存在！", MessageBoxIcon.Warning);
                        return false;
                    }

                    monthReportViews.Add(monthReportView);
                }
                if (monthReportViews.Count > 0)
                {
                    this.Grid1.Hidden = false;
                    this.Grid1.DataSource = monthReportViews;
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
                int a = monthReportViews.Count();
                for (int i = 0; i < a; i++)
                {
                    if (!BLL.SitePerson_MonthReportService.IsExistMonthReport(Convert.ToDateTime(monthReportViews[i].CompileDate), this.CurrUser.LoginProjectId))
                    {
                        Model.SitePerson_MonthReport newMonthReport = new Model.SitePerson_MonthReport
                        {
                            MonthReportId = monthReportViews[i].MonthReportId,
                            ProjectId = this.CurrUser.LoginProjectId,
                            CompileMan = this.CurrUser.UserId,
                            CompileDate = monthReportViews[i].CompileDate,
                            States = BLL.Const.State_0
                        };
                        BLL.SitePerson_MonthReportService.AddMonthReport(newMonthReport);

                        foreach (var item in monthReportViews)
                        {
                            var detail = BLL.SitePerson_MonthReportDetailService.GetDayReportDetailByUnit(item.UnitId, this.CurrUser.LoginProjectId);
                            if (detail == null)
                            {
                                Model.SitePerson_MonthReportDetail newMonthReportDetail = new Model.SitePerson_MonthReportDetail
                                {
                                    MonthReportDetailId = SQLHelper.GetNewID(typeof(Model.SitePerson_MonthReportDetail)),
                                    MonthReportId = newMonthReport.MonthReportId,
                                    UnitId = item.UnitId,
                                    StaffData = item.StaffData,//人员情况
                                    WorkTime = item.WorkTime,//平均工时
                                    CheckPersonNum = item.CheckPersonNum,//考勤人数
                                    RealPersonNum = item.RealPersonNum//实际人数
                                };
                                newMonthReportDetail.PersonWorkTime = (newMonthReportDetail.WorkTime) * (newMonthReportDetail.RealPersonNum);//当日人工时数

                                //当年人工时数
                                var yearPersonWorkTime = (from x in monthReportViews
                                                          where x.CompileDate <= newMonthReport.CompileDate
                                                          && x.CompileDate.Value.Year == newMonthReport.CompileDate.Value.Year
                                                          && x.ProjectId == this.CurrUser.LoginProjectId
                                                          && x.MonthReportId == item.MonthReportId
                                                          select x.PersonWorkTime ?? 0).Sum().ToString();
                                newMonthReportDetail.YearPersonWorkTime = Funs.GetNewIntOrZero(yearPersonWorkTime);

                                //总人工时
                                var totalPersonWorkTime = (from x in monthReportViews
                                                           where x.CompileDate <= newMonthReport.CompileDate
                                                               && x.ProjectId == this.CurrUser.LoginProjectId
                                                               && x.MonthReportId == item.MonthReportId
                                                           select x.PersonWorkTime ?? 0).Sum().ToString();
                                newMonthReportDetail.TotalPersonWorkTime = Funs.GetNewIntOrZero(totalPersonWorkTime);
                                BLL.SitePerson_MonthReportDetailService.AddMonthReportDetail(newMonthReportDetail);

                                foreach (var unitDetail in monthReportViews)
                                {
                                    Model.SitePerson_MonthReportUnitDetail newMonthReportUnitDetail = new Model.SitePerson_MonthReportUnitDetail
                                    {
                                        MonthReportUnitDetailId = SQLHelper.GetNewID(typeof(Model.SitePerson_MonthReportUnitDetail)),
                                        MonthReportDetailId = newMonthReportDetail.MonthReportDetailId,
                                        PostId = unitDetail.PostId,
                                        CheckPersonNum = unitDetail.CheckPersonNum,//考勤人数
                                        RealPersonNum = unitDetail.RealPersonNum//实际人数
                                    };
                                    newMonthReportUnitDetail.PersonWorkTime = (newMonthReportUnitDetail.CheckPersonNum) * (newMonthReportUnitDetail.RealPersonNum);//当日人工时数
                                    BLL.SitePerson_MonthReportUnitDetailService.AddMonthReportUnitDetail(newMonthReportUnitDetail);
                                }
                            }
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
            //errorInfos.Clear();
            //if (Session["errorInfos"] != null)
            //{
            //    this.hdCheckResult.Text = Session["errorInfos"].ToString();
            //}
            //else
            //{
            //    this.hdCheckResult.Text = string.Empty;
            //    this.Grid1.Hidden = false;
            //    this.gvErrorInfo.Hidden = true;
            //}
            //if (!string.IsNullOrEmpty(this.hdCheckResult.Text.Trim()))
            //{
            //    string result = this.hdCheckResult.Text.Trim();
            //    List<string> errorInfoList = result.Split('|').ToList();
            //    foreach (var item in errorInfoList)
            //    {
            //        string[] errors = item.Split(',');
            //        Model.ErrorInfo errorInfo = new Model.ErrorInfo();
            //        errorInfo.Row = errors[0];
            //        errorInfo.Column = errors[1];
            //        errorInfo.Reason = errors[2];
            //        errorInfos.Add(errorInfo);
            //    }
            //    if (errorInfos.Count > 0)
            //    {
            //        this.Grid1.Hidden = true;
            //        this.gvErrorInfo.Hidden = false;
            //        //this.btnOut.Hidden = false;
            //        this.gvErrorInfo.DataSource = errorInfos;
            //        this.gvErrorInfo.DataBind();
            //    }
            //}
        }

        /// <summary>
        /// 关闭导入弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            if (Session["monthReportViews"] != null)
            {
                monthReportViews = Session["monthReportViews"] as List<Model.View_MonthReportView>;
            }
            if (monthReportViews.Count > 0)
            {
                this.Grid1.Hidden = false;
                this.Grid1.DataSource = monthReportViews;
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
                string uploadfilepath = rootPath + Const.MonthReportTemplateUrl;
                string filePath = Const.MonthReportTemplateUrl;
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