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

namespace FineUIPro.Web.HJGL.WeldingManage
{
    public partial class PipelineListUpdateIn : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 上传预设的虚拟路径
        /// </summary>
        private string initPath = Const.ExcelUrl;

        /// <summary>
        /// 安装组件集合
        /// </summary>
        public static List<Model.View_HJGL_Pipeline> PipelineList = new List<Model.View_HJGL_Pipeline>();

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
                if (PipelineList != null)
                {
                    PipelineList.Clear();
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
                if (PipelineList != null)
                {
                    PipelineList.Clear();
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
                AddDatasetToSQL(ds.Tables[0], 18);
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
            ir = pds.Rows.Count;
            if (ic < Cols)
            {
                ShowNotify("导入Excel格式错误！Excel只有" + ic.ToString().Trim() + "列", MessageBoxIcon.Warning);
                return false;
            }
            if (pds != null && ir > 0)
            {
                var getMedium = from x in Funs.DB.Base_Medium where x.ProjectId == this.CurrUser.LoginProjectId select x;//介质
                var getPipeLineClass = from x in Funs.DB.Base_PipingClass where x.ProjectId == this.CurrUser.LoginProjectId select x;//管道等级
                var getDetectionRate = from x in Funs.DB.Base_DetectionRate select x;//探伤比例
                var getDetectionType = from x in Funs.DB.Base_DetectionType select x;//探伤类型
                var getPressurePipingClass = from x in Funs.DB.Base_PressurePipingClass select x;//压力管道级别
                var getTestMedium = from x in Funs.DB.Base_TestMedium where x.TestType == "1" select x;//压力试验介质
                var getLeakMedium = from x in Funs.DB.Base_TestMedium where x.TestType == "2" select x;//泄露性试验介质
                var getPurgeMethod = from x in Funs.DB.Base_PurgeMethod select x;
                var getMaterial = from x in Funs.DB.Base_Material select x;
                for (int i = 0; i < ir; i++)
                {
                    var isExistPipeline = Funs.DB.HJGL_Pipeline.FirstOrDefault(x => x.UnitWorkId == Request.Params["UnitWorkId"] && x.PipelineCode == pds.Rows[i][0].ToString());
                    Model.View_HJGL_Pipeline pipeline = new Model.View_HJGL_Pipeline();
                    if (isExistPipeline != null)
                    {
                        pipeline.PipelineId = isExistPipeline.PipelineId;
                    }
                    Model.WBS_UnitWork unitWork = BLL.UnitWorkService.GetUnitWorkByUnitWorkId(Request.Params["UnitWorkId"]);
                    if (unitWork != null)
                    {
                        pipeline.UnitWorkId = Request.Params["UnitWorkId"];
                        pipeline.UnitId = unitWork.UnitId;
                    }
                    string col0 = pds.Rows[i][0].ToString();
                    if (string.IsNullOrEmpty(col0))
                    {
                        result += "第" + (i + 2).ToString() + "行," + "管线号" + "," + "此项为必填项！" + "|";
                    }
                    else
                    {
                        if (BLL.PipelineService.IsExistPipelineCode(col0, Request.Params["UnitWorkId"], null))
                        {
                            pipeline.PipelineCode = col0;
                            //Alert.ShowInTop("该管线号已存在！", MessageBoxIcon.Warning);
                        }
                        else
                        {
                            //pipeline.PipelineCode = col0;
                            result += "第" + (i + 2).ToString() + "行," + "管线号" + "," + "不存在！" + "|";
                        }
                    }
                    pipeline.SingleNumber = pds.Rows[i][1].ToString();
                    string col2 = pds.Rows[i][2].ToString();
                    if (string.IsNullOrEmpty(col2))
                    {
                        result += "第" + (i + 2).ToString() + "行," + "介质" + "," + "此项为必填项！" + "|";
                    }
                    else
                    {
                        var Medium = getMedium.FirstOrDefault(x => x.MediumName == col2);
                        if (Medium == null)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "该介质不存在！" + "|";
                        }
                        else
                        {
                            pipeline.MediumId = Medium.MediumId;
                        }
                    }
                    string col3 = pds.Rows[i][3].ToString();
                    if (string.IsNullOrEmpty(col3))
                    {
                        result += "第" + (i + 2).ToString() + "行," + "管线等级" + "," + "此项为必填项！" + "|";
                    }
                    else
                    {
                        var PipeLineClass = getPipeLineClass.FirstOrDefault(x => x.PipingClassCode == col3);
                        if (PipeLineClass == null)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "该管线等级不存在！" + "|";
                        }
                        else
                        {
                            pipeline.PipingClassId = PipeLineClass.PipingClassId;
                        }
                    }
                    string col4 = pds.Rows[i][4].ToString();
                    if (!string.IsNullOrEmpty(col4))
                    {
                        var DetectionRate = getDetectionRate.FirstOrDefault(x => x.DetectionRateCode == col4);
                        if (DetectionRate == null)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "该探伤比例不存在！" + "|";
                        }
                        else
                        {
                            pipeline.DetectionRateId = DetectionRate.DetectionRateId;
                        }
                    }
                    else
                    {
                        result += "第" + (i + 2).ToString() + "行," + "探伤比例" + "," + "此项为必填项！" + "|";
                    }
                    string col5 = pds.Rows[i][5].ToString();
                    if (!string.IsNullOrEmpty(col5))
                    {
                        string[] types = col5.ToString().Split(',');
                        foreach (string t in types)
                        {
                            var type = getDetectionType.FirstOrDefault(x => x.DetectionTypeCode == t);
                            if (type == null)
                            {
                                result += "第" + (i + 2).ToString() + "行," + "探伤类型【" + t + "】不存在！" + "|";
                            }
                            else
                            {
                                pipeline.DetectionType += type.DetectionTypeId + "|";
                            }
                            if (pipeline.DetectionType != string.Empty)
                            {
                                pipeline.DetectionType = pipeline.DetectionType.Substring(0, pipeline.DetectionType.Length - 1);
                            }
                        }
                    }
                    else
                    {
                        result += "第" + (i + 2).ToString() + "行," + "探伤类型" + "," + "此项为必填项！" + "|";
                    }
                    string col6 = pds.Rows[i][6].ToString();
                    if (!string.IsNullOrEmpty(col6))
                    {
                        pipeline.DesignTemperature = col6;
                    }
                    string col7 = pds.Rows[i][7].ToString();
                    if (!string.IsNullOrEmpty(col7))
                    {
                        pipeline.DesignPress = col7;
                    }
                    string col8 = pds.Rows[i][8].ToString();
                    if (!string.IsNullOrEmpty(col8))
                    {
                        var TestMedium = getTestMedium.FirstOrDefault(x => x.MediumName == col8);
                        if (TestMedium == null)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "该压力试验介质不存在！" + "|";
                        }
                        else
                        {
                            pipeline.TestMedium = TestMedium.TestMediumId;
                        }
                    }
                    string col9 = pds.Rows[i][9].ToString();
                    if (!string.IsNullOrEmpty(col9))
                    {
                        pipeline.TestPressure = col9;
                    }
                    string col10 = pds.Rows[i][10].ToString();
                    if (!string.IsNullOrEmpty(col10))
                    {
                        var PressurePipingClass = getPressurePipingClass.FirstOrDefault(x => x.PressurePipingClassCode == col10);
                        if (PressurePipingClass == null)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "该压力管道级别不存在！" + "|";
                        }
                        else
                        {
                            pipeline.PressurePipingClassId = PressurePipingClass.PressurePipingClassId;
                        }
                    }
                    string col11 = pds.Rows[i][11].ToString();
                    if (!string.IsNullOrEmpty(col11))
                    {
                        try
                        {
                            var PipeLenth = Funs.GetNewDecimal(col11);
                            pipeline.PipeLenth = PipeLenth;
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "管线长度(m)格式输入有误" + "|";
                        }
                    }
                    string col12 = pds.Rows[i][12].ToString();
                    if (!string.IsNullOrEmpty(col12))
                    {
                        var LeakMedium = getLeakMedium.FirstOrDefault(x => x.MediumName == col12);
                        if (LeakMedium == null)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "该泄露试验介质不存在！" + "|";
                        }
                        else
                        {
                            pipeline.LeakMedium = LeakMedium.TestMediumId;
                        }
                    }
                    string col13 = pds.Rows[i][13].ToString();
                    if (!string.IsNullOrEmpty(col13))
                    {
                        pipeline.LeakPressure = col13;
                    }
                    string col14 = pds.Rows[i][14].ToString();
                    if (!string.IsNullOrEmpty(col14))
                    {
                        var PurgeMethod = getPurgeMethod.FirstOrDefault(x => x.PurgeMethodCode == col14);
                        if (PurgeMethod == null)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "该吹洗要求不存在！" + "|";
                        }
                        else
                        {
                            pipeline.PCMedium = PurgeMethod.PurgeMethodId;
                            var getTestMediun = Base_TestMediumService.GetTestMediumById(PurgeMethod.PurgeMethodId);
                            if (getTestMediun != null)
                            {
                                if (getTestMediun.TestType == "4")//判断当前吹洗要求是吹扫还是清洗
                                {
                                    pipeline.PCtype = "1";
                                }
                                else if (getTestMediun.TestType == "5")
                                {
                                    pipeline.PCtype = "2";
                                }
                            }
                        }
                    }
                    string col15 = pds.Rows[i][15].ToString();
                    if (!string.IsNullOrEmpty(col15))
                    {
                        pipeline.VacuumPressure = col15;
                    }
                    string col16 = pds.Rows[i][16].ToString();
                    if (!string.IsNullOrEmpty(col16))
                    {
                        var material = getMaterial.FirstOrDefault(x => x.MaterialCode == col16);
                        if (material == null)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "该材质不存在！" + "|";
                        }
                        else
                        {
                            pipeline.MaterialId = material.MaterialId;
                            pipeline.MaterialCode = col16;
                        }
                    }

                    pipeline.Remark = pds.Rows[i][17].ToString();
                    if (!string.IsNullOrEmpty(pipeline.PipelineCode))
                    {
                        PipelineList.Add(pipeline);
                    }
                }
                if (!string.IsNullOrEmpty(result))
                {
                    PipelineList.Clear();
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
                    if (PipelineList.Count > 0)
                    {
                        this.Grid1.Hidden = false;
                        this.Grid1.DataSource = PipelineList;
                        this.Grid1.DataBind();
                        Grid1.RecordCount = PipelineList.Count;
                    }
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
                int a = PipelineList.Count();
                for (int i = 0; i < a; i++)
                {
                    //var isExistPipelineCode = Funs.DB.HJGL_Pipeline.FirstOrDefault(x => x.UnitWorkId == Request.Params["UnitWorkId"] && x.PipelineCode == PipelineList[i].PipelineCode);
                    //if (isExistPipelineCode != null)
                    //{
                    //    //ShowNotify("存在相同批次的管线号，请修正后重新提交！", MessageBoxIcon.Warning);
                    //    //return;
                    //}
                    //else
                    //{
                        Model.HJGL_Pipeline pipeline = new Model.HJGL_Pipeline();
                        pipeline.PipelineId = PipelineList[i].PipelineId;
                        pipeline.ProjectId = this.CurrUser.LoginProjectId;
                        pipeline.UnitId = PipelineList[i].UnitId;
                        pipeline.UnitWorkId = PipelineList[i].UnitWorkId;
                        pipeline.PipelineCode = PipelineList[i].PipelineCode;
                        pipeline.DetectionRateId = PipelineList[i].DetectionRateId;
                        pipeline.MediumId = PipelineList[i].MediumId;
                        pipeline.PipingClassId = PipelineList[i].PipingClassId;
                        pipeline.TestMedium = PipelineList[i].TestMedium;
                        pipeline.SingleNumber = PipelineList[i].SingleNumber;
                        pipeline.DesignPress = PipelineList[i].DesignPress;
                        pipeline.DesignTemperature = PipelineList[i].DesignTemperature;
                        pipeline.TestPressure = PipelineList[i].TestPressure;
                        pipeline.DetectionType = PipelineList[i].DetectionType;
                        pipeline.PressurePipingClassId = PipelineList[i].PressurePipingClassId;
                        pipeline.PipeLenth = PipelineList[i].PipeLenth;
                        pipeline.LeakPressure = PipelineList[i].LeakPressure;
                        pipeline.LeakMedium = PipelineList[i].LeakMedium;
                        pipeline.VacuumPressure = PipelineList[i].VacuumPressure;
                        pipeline.PCMedium = PipelineList[i].PCMedium;
                        pipeline.PCtype = PipelineList[i].PCtype;
                        pipeline.MaterialId = PipelineList[i].MaterialId;
                        pipeline.Remark = PipelineList[i].Remark;
                        BLL.PipelineService.UpdatePipeline(pipeline);
                    //}
                }
                string rootPath = Server.MapPath("~/");
                string initFullPath = rootPath + initPath;
                string filePath = initFullPath + this.hdFileName.Text;
                if (filePath != string.Empty && System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);//删除上传的XLS文件
                }
                ShowNotify("更新成功！", MessageBoxIcon.Success);
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
                string uploadfilepath = rootPath + Const.PipelineTemplateUrl;
                string filePath = Const.PipelineTemplateUrl;
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
        protected string ConvertDetectionType(object detectionType)
        {
            string detectionName = string.Empty;
            if (detectionType != null)
            {
                string[] types = detectionType.ToString().Split('|');
                foreach (string t in types)
                {
                    var type = BLL.Base_DetectionTypeService.GetDetectionTypeByDetectionTypeId(t);
                    if (type != null)
                    {
                        detectionName += type.DetectionTypeCode + ",";
                    }
                }
            }
            if (detectionName != string.Empty)
            {
                return detectionName.Substring(0, detectionName.Length - 1);
            }
            else
            {
                return "";
            }
        }
        #region 分页选择下拉改变事件
        /// <summary>
        /// 分页选择下拉改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            this.Grid1.DataSource = PipelineList;
            this.Grid1.DataBind();
            Grid1.RecordCount = PipelineList.Count;
        }
        #endregion
    }
}