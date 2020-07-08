using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;

namespace FineUIPro.Web.HSSE.EduTrain
{
    public partial class TestTrainingItemIn : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 上传预设的虚拟路径
        /// </summary>
        private string initPath = Const.ExcelUrl;

        /// <summary>
        /// 导入集合
        /// </summary>
        public static List<Model.View_Training_TestTrainingItem> viewTrainingItems = new List<Model.View_Training_TestTrainingItem>();

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
                if (viewTrainingItems != null)
                {
                    viewTrainingItems.Clear();
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
                if (viewTrainingItems != null)
                {
                    viewTrainingItems.Clear();
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
                viewTrainingItems.Clear();
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
            string result = string.Empty;
            int ic, ir;
            ic = pds.Columns.Count;
            ir = pds.Rows.Count;
            if (pds != null && ir > 0)
            {
                ///试题类型
                var trainings = from x in new Model.SGGLDB(Funs.ConnString).Training_TestTraining select x;
                ///岗位
                var workPosts = from x in new Model.SGGLDB(Funs.ConnString).Base_WorkPost select x;
                for (int i = 0; i < ir; i++)
                {
                    Model.View_Training_TestTrainingItem newViewTrainingItem = new Model.View_Training_TestTrainingItem
                    {
                        TrainingCode = pds.Rows[i][0].ToString().Trim(),
                        TrainingItemCode = pds.Rows[i][2].ToString().Trim(),
                        Abstracts = pds.Rows[i][5].ToString().Trim(),
                        AItem = pds.Rows[i][6].ToString().Trim(),
                        BItem = pds.Rows[i][7].ToString().Trim(),
                    };

                    ////试题类型
                    string col1 = pds.Rows[i][1].ToString().Trim();
                    if (string.IsNullOrEmpty(col1))
                    {
                        result += "第" + (i + 2).ToString() + "行," + "导入试题类型" + "," + "此项为必填项！" + "|";
                    }
                    else
                    {
                        var standard = trainings.FirstOrDefault(x => x.TrainingName == col1);
                        if (standard != null)
                        {
                            newViewTrainingItem.TrainingId = standard.TrainingId;
                            newViewTrainingItem.TrainingCode = standard.TrainingCode;
                            newViewTrainingItem.TrainingName = standard.TrainingName;
                        }
                        else
                        {
                            Model.Training_TestTraining newTraining = new Model.Training_TestTraining();
                            newViewTrainingItem.TrainingId = newTraining.TrainingId = SQLHelper.GetNewID(typeof(Model.Training_TestTraining));
                            newViewTrainingItem.TrainingName = newTraining.TrainingName = col1;
                            newTraining.TrainingCode = newViewTrainingItem.TrainingCode;
                            BLL.TestTrainingService.AddTestTraining(newTraining);
                        }
                    }
                    ///试题题型
                    string col3 = pds.Rows[i][3].ToString().Trim();
                    if (string.IsNullOrEmpty(col3))
                    {
                        result += "第" + (i + 2).ToString() + "行," + "导入试题题型" + "," + "此项为必填项！" + "|";
                    }
                    else
                    {

                        if (col3 == "单选题")
                        {
                            newViewTrainingItem.TestType = "1";
                            //newViewTrainingItem.Score = SValue;
                            newViewTrainingItem.CItem = pds.Rows[i][8].ToString().Trim();
                            newViewTrainingItem.DItem = pds.Rows[i][9].ToString().Trim();
                            //if (string.IsNullOrEmpty(newViewTrainingItem.AItem) || string.IsNullOrEmpty(newViewTrainingItem.BItem) || string.IsNullOrEmpty(newViewTrainingItem.CItem) || string.IsNullOrEmpty(newViewTrainingItem.DItem))
                            //{
                            //    result += "第" + (i + 2).ToString() + "行," + "单选题ABCD都不能为空！" + "|";
                            //}

                        }
                        else if (col3 == "多选题")
                        {
                            newViewTrainingItem.TestType = "2";
                            //newViewTrainingItem.Score = MValue;
                            newViewTrainingItem.CItem = pds.Rows[i][8].ToString().Trim();
                            newViewTrainingItem.DItem = pds.Rows[i][9].ToString().Trim();
                            newViewTrainingItem.EItem = pds.Rows[i][10].ToString().Trim();
                            //if (string.IsNullOrEmpty(newViewTrainingItem.AItem) || string.IsNullOrEmpty(newViewTrainingItem.BItem) || string.IsNullOrEmpty(newViewTrainingItem.CItem) || string.IsNullOrEmpty(newViewTrainingItem.DItem))
                            //{
                            //    result += "第" + (i + 2).ToString() + "行," + "多选题ABCD都不能为空！" + "|";
                            //}
                        }
                        else
                        {
                            newViewTrainingItem.TestType = "3";
                            //newViewTrainingItem.Score = JValue;
                            //if (string.IsNullOrEmpty(newViewTrainingItem.AItem) || string.IsNullOrEmpty(newViewTrainingItem.BItem))
                            //{
                            //    result += "第" + (i + 2).ToString() + "行," + "判断题AB不能为空！" + "|";
                            //}
                        }
                    }
                    ////适合岗位
                    string col4 = pds.Rows[i][4].ToString().Trim();
                    if (!string.IsNullOrEmpty(col4))
                    {

                        List<string> WorkPostSels = Funs.GetStrListByStr(col4, ',');
                        if (WorkPostSels.Count() > 0)
                        {
                            string ids = string.Empty;
                            foreach (var item in WorkPostSels)
                            {
                                var wp = workPosts.FirstOrDefault(x => x.WorkPostName == item);
                                if (wp != null)
                                {
                                    ids += wp.WorkPostId + ",";
                                }
                                else
                                {
                                    result += "第" + (i + 2).ToString() + "行," + "导入适合岗位" + item + "," + "此项基础表不存在！" + "|";
                                }
                            }
                            if (!string.IsNullOrEmpty(ids))
                            {
                                newViewTrainingItem.WorkPostNames = col4;
                                newViewTrainingItem.WorkPostIds = ids.Substring(0, ids.LastIndexOf(","));
                            }
                        }
                    }

                    ////正确答案
                    string col11 = pds.Rows[i][11].ToString().Trim();
                    if (string.IsNullOrEmpty(col11))
                    {
                        result += "第" + (i + 2).ToString() + "行," + "导入正确答案项" + "," + "为必填项！" + "|";
                    }
                    else
                    {
                        int icount = 0;
                        List<string> selecItem = Funs.GetStrListByStr(col11, ',');
                        foreach (var item in selecItem)
                        {
                            if (item != "A" && item != "a" && item != "B" && item != "b" && item != "C" && item != "c" && item != "D" && item != "d" && item != "E" && item != "e")
                            {
                                icount++;
                            }
                        }
                        if (icount == 0)
                        {
                            newViewTrainingItem.AnswerItems = col11.Replace("a", "A").Replace("b", "B").Replace("c", "C").Replace("d", "D").Replace("e", "E");
                        }
                        else
                        {
                            result += "第" + (i + 2).ToString() + "行," + "正确答案项只能输入ABCDE且用','隔开！" + "|";
                        }
                    }

                    ////试题内容
                    if (string.IsNullOrEmpty(newViewTrainingItem.Abstracts))
                    {
                        result += "第" + (i + 2).ToString() + "行," + "导入试题内容" + "," + "为必填项！" + "|";
                    }
                    else
                    {
                        var addTrainingItem = new Model.SGGLDB(Funs.ConnString).Training_TestTrainingItem.FirstOrDefault(x => x.Abstracts == newViewTrainingItem.Abstracts
                                                            && x.TrainingId == newViewTrainingItem.TrainingId);
                        if (addTrainingItem == null)
                        {
                            Model.Training_TestTrainingItem newTrainingItem = new Model.Training_TestTrainingItem
                            {
                                TrainingItemId = newViewTrainingItem.TrainingItemId = SQLHelper.GetNewID(typeof(Model.Training_TestTraining)),
                                TrainingId = newViewTrainingItem.TrainingId,
                                TrainingItemCode = newViewTrainingItem.TrainingItemCode,
                                TrainingItemName = newViewTrainingItem.TrainingItemName,

                                AttachUrl = newViewTrainingItem.AttachUrl,
                                VersionNum = newViewTrainingItem.VersionNum,
                                TestType = newViewTrainingItem.TestType,
                                WorkPostIds = newViewTrainingItem.WorkPostIds,
                                WorkPostNames = newViewTrainingItem.WorkPostNames,
                                AItem = newViewTrainingItem.AItem,
                                BItem = newViewTrainingItem.BItem,
                                CItem = newViewTrainingItem.CItem,
                                DItem = newViewTrainingItem.DItem,
                                EItem = newViewTrainingItem.EItem,
                                // Score = newViewTrainingItem.Score,
                                AnswerItems = newViewTrainingItem.AnswerItems,
                            };
                            if (newViewTrainingItem.Abstracts.Contains("(") || newViewTrainingItem.Abstracts.Contains(")")
                                || newViewTrainingItem.Abstracts.Contains("（") || newViewTrainingItem.Abstracts.Contains("）"))
                            {
                                newTrainingItem.Abstracts = newViewTrainingItem.Abstracts;
                            }
                            else
                            {
                                newTrainingItem.Abstracts = newViewTrainingItem.Abstracts + "( )";
                            }

                            BLL.TestTrainingItemService.AddTestTrainingItem(newTrainingItem);
                            ///加入培训试题库
                            viewTrainingItems.Add(newViewTrainingItem);
                        }
                        else
                        {
                            result += "第" + (i + 2).ToString() + "行," + "导入试题重复" + "|";
                        }
                    }
                }
                if (viewTrainingItems.Count > 0)
                {
                    viewTrainingItems = viewTrainingItems.Distinct().ToList();
                    this.Grid1.Hidden = false;
                    this.Grid1.DataSource = viewTrainingItems;
                    this.Grid1.DataBind();
                }

                if (!string.IsNullOrEmpty(result))
                {
                    viewTrainingItems.Clear();
                    result = "数据导入完成，未成功数据：" + result.Substring(0, result.LastIndexOf("|"));
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
                    ShowNotify("导入成功！", MessageBoxIcon.Success);
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                }
            }
            else
            {
                Alert.ShowInTop("导入数据为空！", MessageBoxIcon.Warning);
            }
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
            if (Session["trainingItem"] != null)
            {
                viewTrainingItems = Session["trainingItem"] as List<Model.View_Training_TestTrainingItem>;
            }
            if (viewTrainingItems.Count > 0)
            {
                this.Grid1.Hidden = false;
                this.Grid1.DataSource = viewTrainingItems;
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
                string filePath = Const.TestTrainingTemplateUrl;
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