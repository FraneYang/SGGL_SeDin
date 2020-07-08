namespace FineUIPro.Web.HSSE.EduTrain
{
    using BLL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;
    using System.Linq;

    public partial class TrainRecordIn : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 上传预设的虚拟路径
        /// </summary>
        private string initPath = Const.ExcelUrl;

        /// <summary>
        /// 导入集合
        /// </summary>
        public static List<Model.View_EduTrain_TrainRecordDetail> viewTrainRecordDetails = new List<Model.View_EduTrain_TrainRecordDetail>();

        /// <summary>
        /// 错误集合
        /// </summary>
        public static string errorInfos = string.Empty;

        /// <summary>
        /// 主键
        /// </summary>
        public string TrainingId
        {
            get
            {
                return (string)ViewState["TrainingId"];
            }
            set
            {
                ViewState["TrainingId"] = value;
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
                this.btnClose.OnClientClick = ActiveWindow.GetHidePostBackReference();
                this.hdFileName.Text = string.Empty;
                this.hdCheckResult.Text = string.Empty;
                this.TrainingId = Request.Params["TrainingId"];
                if (viewTrainRecordDetails != null)
                {
                    viewTrainRecordDetails.Clear();
                }
                errorInfos = string.Empty;
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
            if (string.IsNullOrEmpty(this.TrainingId))
            {
                Alert.ShowInTop("先保存培训，再导入！", MessageBoxIcon.Warning);
                return;
            }
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
                if (viewTrainRecordDetails != null)
                {
                    viewTrainRecordDetails.Clear();
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
                viewTrainRecordDetails.Clear();
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
            int  ir = pds.Rows.Count;
            if (pds != null && ir > 0)
            {
                var trainRecord = BLL.EduTrain_TrainRecordService.GetTrainingByTrainingId(this.TrainingId);
                if (trainRecord != null)
                {
                    var units = from x in new Model.SGGLDB(Funs.ConnString).Base_Unit select x;
                    var persons = from x in new Model.SGGLDB(Funs.ConnString).SitePerson_Person where x.ProjectId == trainRecord.ProjectId select x;
                    for (int i = 0; i < ir; i++)
                    {
                        string result = string.Empty;
                        string col0 = pds.Rows[i][0].ToString().Trim();
                        string col1 = pds.Rows[i][1].ToString().Trim();
                        string col2 = pds.Rows[i][2].ToString().Trim();
                        string col3 = pds.Rows[i][3].ToString().Trim();
                        if (!string.IsNullOrEmpty(col0) || !string.IsNullOrEmpty(col1) || !string.IsNullOrEmpty(col2))
                        {
                            if (string.IsNullOrEmpty(col0) || string.IsNullOrEmpty(col1) || string.IsNullOrEmpty(col2))
                            {
                                result += "第" + (i + 2).ToString() + "行," + "导入项" + "," + "此项为必填项！" + "|";
                            }
                            else
                            {
                                Model.View_EduTrain_TrainRecordDetail newViewTrainRecordDetail = new Model.View_EduTrain_TrainRecordDetail
                                {
                                    TrainDetailId = SQLHelper.GetNewID(typeof(Model.View_EduTrain_TrainRecordDetail)),
                                    TrainingId = trainRecord.TrainingId,
                                    UnitName = col0,
                                    PersonName = col1,
                                    CheckResult = false,
                                    CheckScore=Funs.GetNewDecimal(col3),
                                };

                                if (col2 == "合格")
                                {
                                    newViewTrainRecordDetail.CheckResult = true;
                                }                              
                                var unit = units.FirstOrDefault(x => x.UnitName == col0);
                                if (unit != null)
                                {
                                    newViewTrainRecordDetail.UnitId = unit.UnitId;
                                    var person = persons.FirstOrDefault(x => x.PersonName == col1 && x.UnitId == unit.UnitId);
                                    if (person != null)
                                    {
                                        newViewTrainRecordDetail.PersonId = person.PersonId;
                                    }
                                    else
                                    {
                                        result += "第" + (i + 2).ToString() + "行," + "在现场人员信息中不存在！" + "|";
                                    }

                                }
                                else
                                {
                                    result += "第" + (i + 2).ToString() + "行," + "在单位信息中不存在！" + "|";
                                }


                                ///判断是否已存在
                                var addItem = new Model.SGGLDB(Funs.ConnString).EduTrain_TrainRecordDetail.FirstOrDefault(x => x.TrainingId == newViewTrainRecordDetail.TrainingId && x.PersonId == newViewTrainRecordDetail.PersonId);
                                if (addItem == null)
                                {
                                    if (string.IsNullOrEmpty(result))
                                    {
                                        Model.EduTrain_TrainRecordDetail newTrainRecordDetail = new Model.EduTrain_TrainRecordDetail
                                        {
                                            TrainDetailId = newViewTrainRecordDetail.TrainDetailId,
                                            TrainingId = newViewTrainRecordDetail.TrainingId,
                                            PersonId = newViewTrainRecordDetail.PersonId,
                                            CheckResult = newViewTrainRecordDetail.CheckResult,
                                            CheckScore= newViewTrainRecordDetail.CheckScore,
                                        };

                                        BLL.EduTrain_TrainRecordDetailService.AddTrainDetail(newTrainRecordDetail);
                                        ///加入
                                        viewTrainRecordDetails.Add(newViewTrainRecordDetail);
                                    }
                                }
                                else
                                {
                                    result += "第" + (i + 2).ToString() + "行," + "导入数据重复" + "|";
                                }


                                if (!string.IsNullOrEmpty(result))
                                {
                                    results += result;
                                }
                            }
                        }
                    }
                    if (viewTrainRecordDetails.Count > 0)
                    {
                        viewTrainRecordDetails = viewTrainRecordDetails.Distinct().ToList();
                        this.Grid1.Hidden = false;
                        this.Grid1.DataSource = viewTrainRecordDetails;
                        this.Grid1.DataBind();
                    }

                    if (!string.IsNullOrEmpty(results))
                    {
                        viewTrainRecordDetails.Clear();
                        results = "数据导入完成，未成功数据：" + results.Substring(0, results.LastIndexOf("|"));
                        errorInfos = results;
                        Alert.ShowInParent(results, MessageBoxIcon.Warning);                        
                    }
                    else
                    {
                        errorInfos = string.Empty;
                        ShowNotify("导入成功！", MessageBoxIcon.Success);
                        PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                    }
                }
                else
                {
                    Alert.ShowInTop("培训数据为空！", MessageBoxIcon.Warning);
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
        
        #region 关闭弹出窗口
        /// <summary>
        /// 关闭导入弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            if (Session["trainRecordDetails"] != null)
            {
                viewTrainRecordDetails = Session["trainRecordDetails"] as List<Model.View_EduTrain_TrainRecordDetail>;
            }
            if (viewTrainRecordDetails.Count > 0)
            {
                this.Grid1.Hidden = false;
                this.Grid1.DataSource = viewTrainRecordDetails;
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
                string filePath = Const.TrainRecordTemplateUrl;
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
    }
}