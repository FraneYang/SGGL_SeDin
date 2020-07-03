using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;

namespace FineUIPro.Web.HSSE.Technique
{
    public partial class HazardListIn : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string IsCompany
        {
            get
            {
                return (string)ViewState["IsCompany"];
            }
            set
            {
                ViewState["IsCompany"] = value;
            }
        }
        /// <summary>
        /// 上传预设的虚拟路径
        /// </summary>
        private string initPath = Const.ExcelUrl;

        /// <summary>
        /// 危险源清单集合
        /// </summary>
        public static List<Model.Technique_HazardList> hazardLists = new List<Model.Technique_HazardList>();

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
                this.IsCompany = Request.Params["IsCompany"];
                //this.hdCheckResult.Text = string.Empty;
                if (hazardLists != null)
                {
                    hazardLists.Clear();
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
                if (hazardLists != null)
                {
                    hazardLists.Clear();
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
                var hazardListTypes = from x in Funs.DB.Technique_HazardListType select x;
                for (int i = 0; i < ir; i++)
                {
                    string col0 = pds.Rows[i][0].ToString();
                    if (string.IsNullOrEmpty(col0))
                    {
                        result += "第" + (i + 2).ToString() + "行," + "危险源类别（一级）" + "," + "此项为必填项！" + "|";
                    }
                   
                    string col3 = pds.Rows[i][3].ToString();
                    if (string.IsNullOrEmpty(col3))
                    {
                        result += "第" + (i + 2).ToString() + "行," + "代码" + "," + "此项为必填项！" + "|";
                    }
                    string col4 = pds.Rows[i][4].ToString();
                    if (string.IsNullOrEmpty(col4))
                    {
                        result += "第" + (i + 2).ToString() + "行," + "危险因素明细" + "," + "此项为必填项！" + "|";
                    }
                    string col7 = pds.Rows[i][7].ToString();
                    if (!string.IsNullOrEmpty(col7))
                    {
                        if (col7 != "Ⅰ" && col7 != "Ⅱ" && col7 != "Ⅲ" && col7 != "Ⅳ" && col7 != "Ⅴ")
                        {
                            result += "第" + (i + 2).ToString() + "行," + "辅助方法" + "," + "[" + col7 + "]错误！" + "|";
                        }
                    }
                    string col8 = pds.Rows[i][8].ToString();
                    if (!string.IsNullOrEmpty(col8))
                    {
                        try
                        {
                            decimal dec = Convert.ToDecimal(col8);
                        }
                        catch (Exception)
                        {

                            result += "第" + (i + 2).ToString() + "行," + "危险评价L" + "," + "[" + col8 + "]错误！" + "|";
                        }
                    }
                    string col9 = pds.Rows[i][9].ToString();
                    if (!string.IsNullOrEmpty(col9))
                    {
                        try
                        {
                            decimal dec = Convert.ToDecimal(col9);
                        }
                        catch (Exception)
                        {

                            result += "第" + (i + 2).ToString() + "行," + "危险评价E" + "," + "[" + col9 + "]错误！" + "|";
                        }
                    }
                    string col10 = pds.Rows[i][10].ToString();
                    if (!string.IsNullOrEmpty(col10))
                    {
                        try
                        {
                            decimal dec = Convert.ToDecimal(col10);
                        }
                        catch (Exception)
                        {

                            result += "第" + (i + 2).ToString() + "行," + "危险评价C" + "," + "[" + col10 + "]错误！" + "|";
                        }
                    }
                    string col11 = pds.Rows[i][11].ToString();
                    if (!string.IsNullOrEmpty(col11))
                    {
                        try
                        {
                            decimal dec = Convert.ToDecimal(col11);
                        }
                        catch (Exception)
                        {

                            result += "第" + (i + 2).ToString() + "行," + "危险评价D" + "," + "[" + col11 + "]错误！" + "|";
                        }
                    }
                    string col12 = pds.Rows[i][12].ToString();
                    if (!string.IsNullOrEmpty(col12))
                    {
                        if (col12 != "1级" && col12 != "2级" && col12 != "3级" && col12 != "4级" && col12 != "5级")
                        {
                            result += "第" + (i + 2).ToString() + "行," + "危险级别" + "," + "[" + col12 + "]错误！" + "|";
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
            string IsCompany = Request.Params["IsCompany"];
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
            hazardLists.Clear();
            ic = pds.Columns.Count;
            if (ic < Cols)
            {
                ShowNotify("导入Excel格式错误！Excel只有" + ic.ToString().Trim() + "列", MessageBoxIcon.Warning);
            }

            ir = pds.Rows.Count;
            if (pds != null && ir > 0)
            {
                var hazardListTypes = from x in Funs.DB.Technique_HazardListType select x;
                for (int i = 0; i < ir; i++)
                {
                    Model.Technique_HazardList hazardList = new Model.Technique_HazardList();
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
                  
                    if (!string.IsNullOrEmpty(col0))//危险源类别（一级）
                    {
                        string type1Id=  SQLHelper.GetNewID(typeof(Model.Technique_HazardListType));

                        var hazardListType1 = hazardListTypes.FirstOrDefault(e => e.HazardListTypeName == col0 && (Convert.ToBoolean(this.IsCompany) == e.IsCompany || (this.IsCompany=="False" && !e.IsCompany.HasValue)));
                        if (hazardListType1 != null)
                        {
                          type1Id =  hazardList.HazardListTypeId = hazardListType1.HazardListTypeId;
                        }
                        else
                        {
                            Model.Technique_HazardListType newHazardListType = new Model.Technique_HazardListType
                            {
                                HazardListTypeId = type1Id,
                                HazardListTypeName = col0,
                                SupHazardListTypeId = "0"
                            };
                            hazardList.HazardListTypeId = newHazardListType.HazardListTypeId;
                            if (!string.IsNullOrEmpty(col1))
                            {
                                newHazardListType.IsEndLevel = false;
                            }
                            else
                            {
                                newHazardListType.IsEndLevel = true;
                            }
                            newHazardListType.IsCompany = Convert.ToBoolean(Request.Params["IsCompany"]);
                            BLL.HazardListTypeService.AddHazardListType(newHazardListType);
                        }
                     
                        if (!string.IsNullOrEmpty(col1))//危险源类别（二级）
                        {
                            string type2Id = SQLHelper.GetNewID(typeof(Model.Technique_HazardListType));
                            var hazardListType2 = hazardListTypes.FirstOrDefault(e => e.HazardListTypeName == col1 && e.SupHazardListTypeId == type1Id && (Convert.ToBoolean(this.IsCompany) == e.IsCompany || (this.IsCompany=="False" && !e.IsCompany.HasValue)));
                            if (hazardListType2 != null)
                            {
                              type2Id=  hazardList.HazardListTypeId = hazardListType2.HazardListTypeId;
                            }
                            else
                            {
                                Model.Technique_HazardListType newHazardListType = new Model.Technique_HazardListType
                                {
                                    HazardListTypeId = type2Id,
                                    HazardListTypeName = col1,
                                    SupHazardListTypeId = type1Id
                                };
                                hazardList.HazardListTypeId = newHazardListType.HazardListTypeId;
                                if (!string.IsNullOrEmpty(col2))
                                {
                                    newHazardListType.IsEndLevel = false;
                                }
                                else
                                {
                                    newHazardListType.IsEndLevel = true;
                                }
                                newHazardListType.IsCompany = Convert.ToBoolean(Request.Params["IsCompany"]);
                                BLL.HazardListTypeService.AddHazardListType(newHazardListType);
                            }

                            if (!string.IsNullOrEmpty(col2))//危险源类别（三级）
                            {
                                var hazardListType3 = hazardListTypes.FirstOrDefault(e => e.HazardListTypeName == col2 && e.SupHazardListTypeId == type2Id && (Convert.ToBoolean(this.IsCompany) == e.IsCompany || (this.IsCompany=="False" && !e.IsCompany.HasValue)));
                                if (hazardListType3 != null)
                                {
                                    hazardList.HazardListTypeId = hazardListType3.HazardListTypeId;
                                }
                                else
                                {
                                    Model.Technique_HazardListType newHazardListType = new Model.Technique_HazardListType
                                    {
                                        HazardListTypeId = SQLHelper.GetNewID(typeof(Model.Technique_HazardListType)),
                                        HazardListTypeName = col2,
                                        SupHazardListTypeId = type2Id
                                    };
                                    hazardList.HazardListTypeId = newHazardListType.HazardListTypeId;
                                    if (!string.IsNullOrEmpty(col2))
                                    {
                                        newHazardListType.IsEndLevel = false;
                                    }
                                    else
                                    {
                                        newHazardListType.IsEndLevel = true;
                                    }
                                    newHazardListType.IsCompany = Convert.ToBoolean(Request.Params["IsCompany"]);
                                    BLL.HazardListTypeService.AddHazardListType(newHazardListType);
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(col3))//代码
                    {
                        hazardList.HazardCode = col3;
                    }
                    if (!string.IsNullOrEmpty(col4))//危险因素明细
                    {
                        hazardList.HazardItems = col4;
                    }
                    if (!string.IsNullOrEmpty(col5))//缺陷类型
                    {
                        hazardList.DefectsType = col5;
                    }
                    if (!string.IsNullOrEmpty(col6))//可能导致的事故
                    {
                        hazardList.MayLeadAccidents = col6;
                    }
                    if (!string.IsNullOrEmpty(col7))//辅助方法
                    {
                        hazardList.HelperMethod = col7;
                    }
                    if (!string.IsNullOrEmpty(col8))//危险评价（L）
                    {
                        hazardList.HazardJudge_L = Funs.GetNewDecimal(col8);
                    }
                    if (!string.IsNullOrEmpty(col9))//危险评价（E）
                    {
                        hazardList.HazardJudge_E = Funs.GetNewDecimal(col9);
                    }
                    if (!string.IsNullOrEmpty(col10))//危险评价（C）
                    {
                        hazardList.HazardJudge_C = Funs.GetNewDecimal(col10);
                    }
                    if (!string.IsNullOrEmpty(col11))//危险评价（D）
                    {
                        hazardList.HazardJudge_D = Funs.GetNewDecimal(col11);
                    }
                    if (!string.IsNullOrEmpty(col12))//危险级别
                    {
                        hazardList.HazardLevel = col12;
                    }
                    if (!string.IsNullOrEmpty(col13))//控制措施    
                    {
                        hazardList.ControlMeasures = col13;
                    }
                    hazardList.HazardId = SQLHelper.GetNewID(typeof(Model.Technique_HazardList));
                    hazardLists.Add(hazardList);
                }
                if (hazardLists.Count > 0)
                {
                    this.Grid1.Hidden = false;
                    this.Grid1.DataSource = hazardLists;
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
                int a = hazardLists.Count();
                for (int i = 0; i < a; i++)
                {
                    var isExistHazardCode = BLL.HazardListService.IsExistHazardCode(hazardLists[i].HazardListTypeId, hazardLists[i].HazardId, hazardLists[i].HazardCode);
                    if (!isExistHazardCode)
                    {
                        Model.Technique_HazardList hazardList = new Model.Technique_HazardList
                        {
                            HazardId = hazardLists[i].HazardId,
                            HazardListTypeId = hazardLists[i].HazardListTypeId,
                            HazardCode = hazardLists[i].HazardCode,
                            HazardItems = hazardLists[i].HazardItems,
                            DefectsType = hazardLists[i].DefectsType,
                            MayLeadAccidents = hazardLists[i].MayLeadAccidents,
                            HelperMethod = hazardLists[i].HelperMethod,
                            HazardJudge_L = hazardLists[i].HazardJudge_L,
                            HazardJudge_E = hazardLists[i].HazardJudge_E,
                            HazardJudge_C = hazardLists[i].HazardJudge_C,
                            HazardJudge_D = hazardLists[i].HazardJudge_D
                        };
                        if (!string.IsNullOrEmpty(hazardLists[i].HazardLevel))
                        {
                            if (hazardLists[i].HazardLevel == "1级")
                            {
                                hazardList.HazardLevel = "1";
                            }
                            else if (hazardLists[i].HazardLevel == "2级")
                            {
                                hazardList.HazardLevel = "2";
                            }
                            else if (hazardLists[i].HazardLevel == "3级")
                            {
                                hazardList.HazardLevel = "3";
                            }
                            else if (hazardLists[i].HazardLevel == "4级")
                            {
                                hazardList.HazardLevel = "4";
                            }
                            else if (hazardLists[i].HazardLevel == "5级")
                            {
                                hazardList.HazardLevel = "5";
                            }
                        }
                        hazardList.ControlMeasures = hazardLists[i].ControlMeasures;
                        hazardList.CompileMan = this.CurrUser.UserName;
                        hazardList.CompileDate = DateTime.Now;
                        hazardList.IsPass = true;
                        hazardList.UnitId = string.IsNullOrEmpty(this.CurrUser.UnitId) ? Const.UnitId_SEDIN : this.CurrUser.UnitId;
                        BLL.HazardListService.AddHazardList(hazardList);
                        BLL.LogService.AddSys_Log(this.CurrUser, hazardList.HazardCode, hazardList.HazardId, BLL.Const.HazardListMenuId, Const.BtnAdd);
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
            if (Session["hazardLists"] != null)
            {
                hazardLists = Session["hazardLists"] as List<Model.Technique_HazardList>;
            }
            if (hazardLists.Count > 0)
            {
                this.Grid1.Hidden = false;
                this.Grid1.DataSource = hazardLists;
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
                string uploadfilepath = rootPath + Const.HazardListTemplateUrl;
                string filePath = Const.HazardListTemplateUrl;
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
        /// 根据危险源类别ID获取危险源类别
        /// </summary>
        /// <param name="hazardListTypeId"></param>
        /// <returns></returns>
        protected string ConvertHazardListType(object hazardListTypeId)
        {
            if (hazardListTypeId != null)
            {
                var hazardListType = BLL.HazardListTypeService.GetHazardListTypeById(hazardListTypeId.ToString());
                if (hazardListType != null)
                {
                    return hazardListType.HazardListTypeName;
                }
            }
            return null;
        }
        #endregion
    }
}