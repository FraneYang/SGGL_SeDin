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
using Newtonsoft.Json.Linq;
using AspNet = System.Web.UI.WebControls;
namespace FineUIPro.Web.CQMS.WBS
{
    public partial class WorkPackageSet2In : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 上传预设的虚拟路径
        /// </summary>
        private string initPath = Const.ExcelUrl;

        /// <summary>
        /// 导入集合
        /// </summary>
        private List<Model.WBS_WorkPackage> ViewWorkPackages = new List<Model.WBS_WorkPackage>();

        /// <summary>
        /// 错误集合
        /// </summary>
        public static string errorInfos = string.Empty;
        /// <summary>
        /// 主键
        /// </summary>
        public string WorkPackageId
        {
            get
            {
                return (string)ViewState["WorkPackageId"];
            }
            set
            {
                ViewState["WorkPackageId"] = value;
            }
        }
        /// <summary>
        /// 单位工程
        /// </summary>
        public string UnitWorkId
        {
            get
            {
                return (string)ViewState["UnitWorkId"];
            }
            set
            {
                ViewState["UnitWorkId"] = value;
            }
        }
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
                if (ViewWorkPackages != null)
                {
                    ViewWorkPackages.Clear();
                }
                errorInfos = string.Empty;
                WorkPackageId = Request.Params["WorkPackageId"];
                var workPackage = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(WorkPackageId);
                if (workPackage != null)
                {
                    UnitWorkId = workPackage.UnitWorkId;
                    if (workPackage.Costs != null)
                    {
                        this.hdTotalValue.Text = workPackage.Costs.ToString();
                    }
                    else
                    {
                        this.hdTotalValue.Text = "0";
                    }
                }

            }
        }
        #endregion

        #region 数据导入
        /// <summary>
        /// 数据导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAudit_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.fuAttachUrl.HasFile == false)
                {
                    Alert.ShowInTop("请您选择Excel文件！", MessageBoxIcon.Warning);
                    return;
                }
                string IsXls = Path.GetExtension(this.fuAttachUrl.FileName).ToString().Trim().ToLower();
                if (IsXls != ".xls")
                {
                    Alert.ShowInTop("只可以选择Excel文件！", MessageBoxIcon.Warning);
                    return;
                }
                if (ViewWorkPackages != null)
                {
                    ViewWorkPackages.Clear();
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
                ViewWorkPackages.Clear();
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

                AddDatasetToSQL(ds.Tables[0]);
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
        private bool AddDatasetToSQL(DataTable pds)
        {

            string results = string.Empty;
            int ir = pds.Rows.Count;
            if (pds != null && ir > 0)
            {
                Model.WBS_WorkPackage workPackage = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(WorkPackageId);
                var workPackageProjects = BLL.WorkPackageProjectService.GetWorkPackageProjects2ByWorkPackageCode(workPackage.InitWorkPackageCode, this.CurrUser.LoginProjectId);
                if (workPackage != null)
                {
                    for (int i = 0; i < ir; i++)
                    {
                        string result = string.Empty;
                        string col0 = pds.Rows[i][0].ToString().Trim();
                        string col1 = pds.Rows[i][1].ToString().Trim();
                        string col2 = pds.Rows[i][2].ToString().Trim();
                        string col3 = pds.Rows[i][3].ToString().Trim();
                        if (!string.IsNullOrEmpty(col0))
                        {
                            if (string.IsNullOrEmpty(col0))
                            {
                                result += "第" + (i + 2).ToString() + "行," + "导入项" + "," + "分项为必填项！" + "|";
                            }
                            else
                            {
                                Model.WBS_WorkPackage newWorkPackage = new Model.WBS_WorkPackage
                                {
                                    WorkPackageId = SQLHelper.GetNewID(typeof(Model.WBS_WorkPackage)),


                                };
                                if (!string.IsNullOrEmpty(col1) && !string.IsNullOrEmpty(col2))
                                {
                                    newWorkPackage.SuperWorkPack = col1 + "-" + col2;
                                }
                                foreach (var item in workPackageProjects)
                                {
                                    if (col0 == item.PackageContent)
                                    {
                                        newWorkPackage.PackageContent = col0;
                                        newWorkPackage.WorkPackageCode = item.WorkPackageCode;
                                    }

                                }
                                if (string.IsNullOrEmpty(newWorkPackage.PackageContent))
                                {
                                    result += "第" + (i + 2).ToString() + "行," + "分项输入值有误！" + "|";
                                }
                                if (!string.IsNullOrEmpty(col3))
                                {
                                    try
                                    {
                                        newWorkPackage.Weights = Convert.ToDecimal(col3);
                                    }
                                    catch (Exception)
                                    {
                                        result += "第" + (i + 2).ToString() + "行," + "权重输入值有误！" + "|";
                                    }
                                }

                                ViewWorkPackages.Add(newWorkPackage);
                                if (!string.IsNullOrEmpty(result))
                                {
                                    results += result;
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(results))
                    {
                        results = "数据导入失败，未成功数据：" + results.Substring(0, results.LastIndexOf("|"));
                        errorInfos = results;
                        Alert.ShowInParent(results, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        ViewWorkPackages = ViewWorkPackages.Distinct().ToList();
                        this.Grid1.Hidden = false;
                        this.Grid1.DataSource = ViewWorkPackages;
                        this.Grid1.DataBind();
                        errorInfos = string.Empty;
                        ShowNotify("导入成功！", MessageBoxIcon.Success);
                    }
                }
            }
            else
            {
                Alert.ShowInTop("导入数据为空！", MessageBoxIcon.Warning);
            }

            BLL.UploadFileService.DeleteFile(Funs.RootPath, initPath + this.hdFileName.Text);
            return true;
        }
        #endregion
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
                string filePath = Const.WBSWorkPackageTemplateUrl;
                string uploadfilepath = rootPath + filePath;
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
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.fuAttachUrl.HasFile == false)
                {
                    Alert.ShowInTop("请您选择Excel文件！", MessageBoxIcon.Warning);
                    return;
                }
                string IsXls = Path.GetExtension(this.fuAttachUrl.FileName).ToString().Trim().ToLower();
                if (IsXls != ".xls")
                {
                    Alert.ShowInTop("只可以选择Excel文件！", MessageBoxIcon.Warning);
                    return;
                }
                if (ViewWorkPackages != null)
                {
                    ViewWorkPackages.Clear();
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

                ViewWorkPackages.Clear();
                string oleDBConnString = String.Empty;
                oleDBConnString = "Provider=Microsoft.Jet.OLEDB.4.0;";
                oleDBConnString += "Data Source=";
                oleDBConnString += rootPath + initPath + this.hdFileName.Text;
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

                DataTable pds = ds.Tables[0];
                string results = string.Empty;
                int ir = pds.Rows.Count;
                if (pds != null && ir > 0)
                {
                    Model.WBS_WorkPackage workPackage = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(WorkPackageId);
                    var workPackageProjects = BLL.WorkPackageProjectService.GetWorkPackageProjects2ByWorkPackageCode(workPackage.InitWorkPackageCode, this.CurrUser.LoginProjectId);
                    if (workPackage != null)
                    {
                        for (int i = 0; i < ir; i++)
                        {
                            string result = string.Empty;
                            string col0 = pds.Rows[i][0].ToString().Trim();
                            string col1 = pds.Rows[i][1].ToString().Trim();
                            string col2 = pds.Rows[i][2].ToString().Trim();
                            string col3 = pds.Rows[i][3].ToString().Trim();
                            if (!string.IsNullOrEmpty(col0))
                            {
                                if (string.IsNullOrEmpty(col0))
                                {
                                    result += "第" + (i + 2).ToString() + "行," + "导入项" + "," + "分项为必填项！" + "|";
                                }
                                else
                                {
                                    Model.WBS_WorkPackage newWorkPackage = new Model.WBS_WorkPackage
                                    {
                                        WorkPackageId = SQLHelper.GetNewID(typeof(Model.WBS_WorkPackage)),


                                    };
                                    if (!string.IsNullOrEmpty(col1) && !string.IsNullOrEmpty(col2))
                                    {
                                        newWorkPackage.SuperWorkPack = col1 + "-" + col2;
                                    }
                                    foreach (var item in workPackageProjects)
                                    {
                                        if (col0 == item.PackageContent)
                                        {
                                            newWorkPackage.PackageContent = col0;
                                            newWorkPackage.WorkPackageCode = item.WorkPackageCode;
                                        }

                                    }
                                    if (string.IsNullOrEmpty(newWorkPackage.PackageContent))
                                    {
                                        result += "第" + (i + 2).ToString() + "行," + "分项输入值有误！" + "|";
                                    }
                                    if (!string.IsNullOrEmpty(col3))
                                    {
                                        try
                                        {
                                            newWorkPackage.Weights = Convert.ToDecimal(col3);
                                        }
                                        catch (Exception)
                                        {
                                            result += "第" + (i + 2).ToString() + "行," + "权重输入值有误！" + "|";
                                        }
                                    }

                                    ViewWorkPackages.Add(newWorkPackage);
                                    if (!string.IsNullOrEmpty(result))
                                    {
                                        results += result;
                                    }
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(results))
                        {
                            results = "数据导入失败，未成功数据：" + results.Substring(0, results.LastIndexOf("|"));
                            errorInfos = results;
                            Alert.ShowInParent(results, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            ViewWorkPackages = ViewWorkPackages.Distinct().ToList();
                            this.Grid1.Hidden = false;
                            this.Grid1.DataSource = ViewWorkPackages;
                            this.Grid1.DataBind();
                            errorInfos = string.Empty;
                        }
                    }
                }
                else
                {
                    Alert.ShowInTop("导入数据为空！", MessageBoxIcon.Warning);
                }
                BLL.UploadFileService.DeleteFile(Funs.RootPath, initPath + this.hdFileName.Text);
            }
            catch (Exception ex)
            {
                ShowNotify("'" + ex.Message + "'", MessageBoxIcon.Warning);
                return;
            }
            string workPackageCode = string.Empty;
            int num = 1;
            string code = string.Empty;
            Model.WBS_WorkPackage parentWorkPackage = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(WorkPackageId);
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                string workPackageId = this.Grid1.Rows[i].DataKeys[0].ToString();
                Model.WBS_WorkPackage oldWorkPackage = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(workPackageId);
                string workPackageCode2 = this.Grid1.Rows[i].DataKeys[1].ToString();
                string txtName = values.Value<string>("SuperWorkPack");
                string txtWeights = values.Value<string>("Weights");
                Model.WBS_WorkPackageProject workPackageProject = BLL.WorkPackageProjectService.GetWorkPackageProjectByWorkPackageCode(workPackageCode2, this.CurrUser.LoginProjectId);
                if (oldWorkPackage == null)   //新增内容
                {
                    Model.WBS_WorkPackage newWorkPackage = new Model.WBS_WorkPackage();
                    if (workPackageCode != workPackageProject.WorkPackageCode)  //循环至新的分部
                    {
                        workPackageCode = workPackageProject.WorkPackageCode;
                        var oldWorkPackages = BLL.WorkPackageService.GetWorkPackagesByInitWorkPackageCodeAndUnitWorkId(workPackageCode, UnitWorkId);
                        if (oldWorkPackages.Count > 0)  //该工作包已存在内容
                        {
                            var old = oldWorkPackages.First();
                            string oldStr = old.WorkPackageCode.Substring(old.WorkPackageCode.Length - 2);
                            num = Convert.ToInt32(oldStr) + 1;
                            if (num < 10)
                            {
                                code = "0" + num.ToString();
                            }
                            else
                            {
                                code = num.ToString();
                            }
                        }
                        else
                        {
                            num = 1;
                            code = "01";
                        }
                    }
                    else
                    {
                        if (num < 10)
                        {
                            code = "0" + num.ToString();
                        }
                        else
                        {
                            code = num.ToString();
                        }
                    }
                    newWorkPackage.WorkPackageId = SQLHelper.GetNewID(typeof(Model.WBS_WorkPackage));
                    newWorkPackage.WorkPackageCode = parentWorkPackage.WorkPackageCode + workPackageCode.Substring(workPackageCode.IndexOf(parentWorkPackage.InitWorkPackageCode) + parentWorkPackage.InitWorkPackageCode.Length).Replace("00", "0000") + code;
                    newWorkPackage.ProjectId = this.CurrUser.LoginProjectId;
                    newWorkPackage.UnitWorkId = UnitWorkId;
                    newWorkPackage.PackageContent = workPackageProject.PackageContent + "-" + txtName;
                    newWorkPackage.SuperWorkPack = workPackageProject.SuperWorkPack;
                    newWorkPackage.SuperWorkPackageId = WorkPackageId;
                    newWorkPackage.PackageCode = code;
                    newWorkPackage.ProjectType = workPackageProject.ProjectType;
                    newWorkPackage.InitWorkPackageCode = workPackageProject.WorkPackageCode;
                    try
                    {
                        newWorkPackage.Weights = Convert.ToDecimal(txtWeights.Trim());
                        if (hdTotalValue.Text != "0" && newWorkPackage.Weights != null)
                        {
                            newWorkPackage.Costs = newWorkPackage.Weights * Convert.ToDecimal(hdTotalValue.Text) / 100;
                        }
                    }
                    catch (Exception)
                    {
                        newWorkPackage.Weights = null;
                        newWorkPackage.Costs = null;
                    }
                    newWorkPackage.IsApprove = true;
                    BLL.WorkPackageService.AddWorkPackage(newWorkPackage);
                    num++;
                }
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            ShowNotify("保存成功！", MessageBoxIcon.Success);
        }
    }
}