using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web.UI;

namespace FineUIPro.Web.HSSE.SitePerson
{
    public partial class PersonIn : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 上传预设的虚拟路径
        /// </summary>
        private string initPath = Const.ExcelUrl;

        /// <summary>
        /// 人员集合
        /// </summary>
        public static List<Model.View_SitePerson_Person> persons = new List<Model.View_SitePerson_Person>();

        /// <summary>
        /// 人员资质集合
        /// </summary>
        public static List<Model.QualityAudit_PersonQuality> personQualitys = new List<Model.QualityAudit_PersonQuality>();

        /// <summary>
        /// 错误集合
        /// </summary>
        public static string errorInfos = string.Empty;

        /// <summary>
        /// 项目ID
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
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
                if (persons != null)
                {
                    persons.Clear();
                }
                errorInfos = string.Empty;
                this.ProjectId = Request.Params["ProjectId"];
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
                if (persons != null)
                {
                    persons.Clear();
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

                AddDatasetToSQL(ds.Tables[0], 36);
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
                var units = from x in Funs.DB.Base_Unit
                            select x;
                var cnProfessionals = from x in Funs.DB.Base_CNProfessional select x;
                var basicDatas = from x in Funs.DB.RealName_BasicData select x;
                var countrys = from x in Funs.DB.RealName_Country select x;
                var citys = from x in Funs.DB.RealName_City select x;
                var teamGroups = from x in Funs.DB.ProjectData_TeamGroup
                                 where x.ProjectId == this.ProjectId
                                 select x;
                var workAreas = from x in Funs.DB.WBS_UnitWork
                                where x.ProjectId == this.ProjectId
                                select x;
                var posts = from x in Funs.DB.Base_WorkPost
                            select x;
                var certificates = from x in Funs.DB.Base_Certificate
                                   select x;
                var positions = from x in Funs.DB.Base_Position select x;
                var postTitles = from x in Funs.DB.Base_PostTitle select x;
                for (int i = 0; i < ir; i++)
                {
                    string col1 = pds.Rows[i][1].ToString().Trim();
                    if (!string.IsNullOrEmpty(col1))
                    {
                        if (string.IsNullOrEmpty(col1))
                        {
                            result += "第" + (i + 2).ToString() + "行," + "人员姓名" + "," + "此项为必填项！" + "|";
                        }

                        string col2 = pds.Rows[i][2].ToString().Trim();
                        if (!string.IsNullOrEmpty(col2))
                        {
                            if (col2 != "男" && col2 != "女")
                            {
                                result += "第" + (i + 2).ToString() + "行," + "性别" + "," + "[" + col2 + "]错误！" + "|";
                            }
                        }
                        else
                        {
                            result += "第" + (i + 2).ToString() + "行," + "性别" + "," + "此项为必填项！" + "|";
                        }

                        string col3 = pds.Rows[i][3].ToString().Trim();
                        if (!string.IsNullOrEmpty(col3))
                        {
                            var basicData = basicDatas.FirstOrDefault(e => e.DictName == col3);
                            if (basicData == null)
                            {
                                result += "第" + (i + 2).ToString() + "行," + "证件类型" + "," + "[" + col3 + "]错误！" + "|";
                            }
                        }
                        else
                        {
                            result += "第" + (i + 2).ToString() + "行," + "证件类型" + "," + "此项为必填项！" + "|";
                        }

                        string col4 = pds.Rows[i][4].ToString().Trim();
                        if (!string.IsNullOrEmpty(col4))
                        {
                            if (col4.Length > 50)
                            {
                                result += "第" + (i + 2).ToString() + "行," + "身份证号码" + "," + "[" + col4 + "]错误！" + "|";
                            }

                            if (PersonService.GetPersonCountByIdentityCard(col4, this.ProjectId) != null)
                            {
                                result += "第" + (i + 2).ToString() + "行," + "身份证号码" + "," + "[" + col4 + "]已存在！" + "|";
                            }
                        }
                        else
                        {
                            result += "第" + (i + 2).ToString() + "行," + "身份证号码" + "," + "此项为必填项！" + "|";
                        }

                        string col5 = pds.Rows[i][5].ToString().Trim();
                        if (!string.IsNullOrEmpty(col5))
                        {
                            try
                            {
                                DateTime date = Convert.ToDateTime(col5);
                            }
                            catch (Exception)
                            {
                                result += "第" + (i + 2).ToString() + "行," + "证件开始日期" + "," + "[" + col5 + "]错误！" + "|";
                            }
                        }
                        else
                        {
                            result += "第" + (i + 2).ToString() + "行," + "证件开始日期" + "," + "此项为必填项！" + "|";
                        }

                        string col6 = pds.Rows[i][6].ToString().Trim();
                        if (!string.IsNullOrEmpty(col6))
                        {
                            if (col6 != "是" && col6 != "否")
                            {
                                result += "第" + (i + 2).ToString() + "行," + "证件是否永久有效" + "," + "[" + col6 + "]错误！" + "|";
                            }
                        }
                        else
                        {
                            result += "第" + (i + 2).ToString() + "行," + "证件是否永久有效" + "," + "此项为必填项！" + "|";
                        }

                        string col7 = pds.Rows[i][7].ToString().Trim();
                        if (!string.IsNullOrEmpty(col7))
                        {
                            try
                            {
                                DateTime date = Convert.ToDateTime(col7);
                            }
                            catch (Exception)
                            {
                                result += "第" + (i + 2).ToString() + "行," + "证件有效日期" + "," + "[" + col7 + "]错误！" + "|";
                            }
                        }
                        else
                        {
                            if (col6 == "否")
                            {
                                result += "第" + (i + 2).ToString() + "行," + "证件有效日期" + "," + "此项为必填项！" + "|";
                            }
                        }

                        string col9 = pds.Rows[i][9].ToString().Trim();
                        if (!string.IsNullOrEmpty(col9))
                        {
                            var unit = units.FirstOrDefault(e => e.UnitName == col9);
                            if (unit != null)
                            {
                                var projectUnit = Funs.DB.Project_ProjectUnit.FirstOrDefault(x => x.ProjectId == this.ProjectId && x.UnitId == unit.UnitId);
                                if (projectUnit == null)
                                {
                                    result += "第" + (i + 2).ToString() + "行," + "所属单位" + "," + "[" + col9 + "]不在本项目中！" + "|";
                                }
                            }
                            else
                            {
                                result += "第" + (i + 2).ToString() + "行," + "所属单位" + "," + "[" + col9 + "]不在单位表中！" + "|";
                            }
                        }
                        else
                        {
                            result += "第" + (i + 2).ToString() + "行," + "所属单位" + "," + "此项为必填项！" + "|";
                        }

                        string col10 = pds.Rows[i][10].ToString().Trim();
                        if (!string.IsNullOrEmpty(col10))
                        {
                            var post = posts.FirstOrDefault(e => e.WorkPostName == col10);
                            if (post == null)
                            {
                                result += "第" + (i + 2).ToString() + "行," + "岗位" + "," + "[" + col10 + "]错误！" + "|";
                            }
                        }
                        else
                        {
                            result += "第" + (i + 2).ToString() + "行," + "岗位" + "," + "此项为必填项！" + "|";
                        }

                        string col11 = pds.Rows[i][11].ToString().Trim();
                        if (!string.IsNullOrEmpty(col11))
                        {
                            var teamGroup = teamGroups.FirstOrDefault(e => e.TeamGroupName == col11);
                            if (teamGroup == null)
                            {
                                result += "第" + (i + 2).ToString() + "行," + "所在班组" + "," + "[" + col11 + "]错误！" + "|";
                            }
                        }
                        else
                        {
                            result += "第" + (i + 2).ToString() + "行," + "所在班组" + "," + "此项为必填项！" + "|";
                        }

                        string col12 = pds.Rows[i][12].ToString().Trim();
                        if (!string.IsNullOrEmpty(col12))
                        {
                            string[] strs = col12.Split('，');
                            foreach (var item in strs)
                            {
                                var workArea = workAreas.FirstOrDefault(e => e.UnitWorkName == item);
                                if (workArea == null)
                                {
                                    result += "第" + (i + 2).ToString() + "行," + "单位工程" + "," + "[" + item + "]错误！" + "|";
                                }
                            }
                        }

                        string col13 = pds.Rows[i][13].ToString().Trim();
                        if (!string.IsNullOrEmpty(col13))
                        {
                            try
                            {
                                DateTime date = Convert.ToDateTime(col13);
                            }
                            catch (Exception)
                            {
                                result += "第" + (i + 2).ToString() + "行," + "入场时间" + "," + "[" + col13 + "]错误！" + "|";
                            }
                        }
                        else
                        {
                            result += "第" + (i + 2).ToString() + "行," + "入场时间" + "," + "此项为必填项！" + "|";
                        }

                        string col14 = pds.Rows[i][14].ToString().Trim();
                        if (!string.IsNullOrEmpty(col14))
                        {
                            var position = positions.FirstOrDefault(e => e.PositionName == col14);
                            if (position == null)
                            {
                                result += "第" + (i + 2).ToString() + "行," + "所属职务" + "," + "[" + col14 + "]错误！" + "|";
                            }
                        }

                        string col15 = pds.Rows[i][15].ToString().Trim();
                        if (!string.IsNullOrEmpty(col15))
                        {
                            var postTitle = postTitles.FirstOrDefault(e => e.PostTitleName == col15);
                            if (postTitle == null)
                            {
                                result += "第" + (i + 2).ToString() + "行," + "所属职称" + "," + "[" + col15 + "]错误！" + "|";
                            }
                        }

                        string col16 = pds.Rows[i][16].ToString().Trim();
                        if (!string.IsNullOrEmpty(col16))
                        {
                            var basicData = basicDatas.FirstOrDefault(e => e.DictName == col16);
                            if (basicData == null)
                            {
                                result += "第" + (i + 2).ToString() + "行," + "文化程度" + "," + "[" + col16 + "]错误！" + "|";
                            }
                        }

                        string col17 = pds.Rows[i][17].ToString().Trim();
                        if (!string.IsNullOrEmpty(col17))
                        {
                            var basicData = basicDatas.FirstOrDefault(e => e.DictName == col17);
                            if (basicData == null)
                            {
                                result += "第" + (i + 2).ToString() + "行," + "婚姻状况" + "," + "[" + col17 + "]错误！" + "|";
                            }
                        }

                        string col18 = pds.Rows[i][18].ToString().Trim();
                        if (!string.IsNullOrEmpty(col18))
                        {
                            var basicData = basicDatas.FirstOrDefault(e => e.DictName == col18);
                            if (basicData == null)
                            {
                                result += "第" + (i + 2).ToString() + "行," + "政治面貌" + "," + "[" + col18 + "]错误！" + "|";
                            }
                        }

                        string col19 = pds.Rows[i][19].ToString().Trim();
                        if (!string.IsNullOrEmpty(col19))
                        {
                            var basicData = basicDatas.FirstOrDefault(e => e.DictName == col19);
                            if (basicData == null)
                            {
                                result += "第" + (i + 2).ToString() + "行," + "民族" + "," + "[" + col19 + "]错误！" + "|";
                            }
                        }

                        string col20 = pds.Rows[i][20].ToString().Trim();
                        if (!string.IsNullOrEmpty(col20))
                        {
                            var country = countrys.FirstOrDefault(e => e.Cname == col20);
                            if (country == null)
                            {
                                result += "第" + (i + 2).ToString() + "行," + "国家" + "," + "[" + col20 + "]错误！" + "|";
                            }
                        }

                        string col21 = pds.Rows[i][21].ToString().Trim();
                        if (!string.IsNullOrEmpty(col21))
                        {
                            var city = citys.FirstOrDefault(e => e.Cname == col21);
                            if (city == null)
                            {
                                result += "第" + (i + 2).ToString() + "行," + "省或地区" + "," + "[" + col21 + "]错误！" + "|";
                            }
                        }

                        string col22 = pds.Rows[i][22].ToString().Trim();
                        if (!string.IsNullOrEmpty(col22))
                        {
                            var cnProfessional = cnProfessionals.FirstOrDefault(e => e.ProfessionalName == col22);
                            if (cnProfessional == null)
                            {
                                result += "第" + (i + 2).ToString() + "行," + "主专业" + "," + "[" + col22 + "]错误！" + "|";
                            }
                        }

                        string col23 = pds.Rows[i][23].ToString().Trim();
                        if (!string.IsNullOrEmpty(col23))
                        {
                            string[] strs = col23.Split('，');
                            foreach (var item in strs)
                            {
                                var cnProfessional = cnProfessionals.FirstOrDefault(e => e.ProfessionalName == item);
                                if (cnProfessional == null)
                                {
                                    result += "第" + (i + 2).ToString() + "行," + "副专业" + "," + "[" + item + "]错误！" + "|";
                                }
                            }
                        }

                        string col24 = pds.Rows[i][24].ToString().Trim();
                        if (!string.IsNullOrEmpty(col24))
                        {
                            var certificate = certificates.FirstOrDefault(e => e.CertificateName == col24);
                            if (certificate == null)
                            {
                                result += "第" + (i + 2).ToString() + "行," + "特岗证书" + "," + "[" + col24 + "]错误！" + "|";
                            }
                        }

                        string col26 = pds.Rows[i][26].ToString().Trim();
                        if (!string.IsNullOrEmpty(col26))
                        {
                            try
                            {
                                DateTime date = Convert.ToDateTime(col26);
                            }
                            catch (Exception)
                            {
                                result += "第" + (i + 2).ToString() + "行," + "证书有效期" + "," + "[" + col26 + "]错误！" + "|";
                            }
                        }

                        string col27 = pds.Rows[i][27].ToString().Trim();
                        if (!string.IsNullOrEmpty(col27))
                        {
                            try
                            {
                                DateTime date = Convert.ToDateTime(col27);
                            }
                            catch (Exception)
                            {
                                result += "第" + (i + 2).ToString() + "行," + "出生日期" + "," + "[" + col27 + "]错误！" + "|";
                            }
                        }

                        string col30 = pds.Rows[i][30].ToString().Trim();
                        if (!string.IsNullOrEmpty(col30))
                        {
                            try
                            {
                                DateTime date = Convert.ToDateTime(col30);
                            }
                            catch (Exception)
                            {
                                result += "第" + (i + 2).ToString() + "行," + "出场时间" + "," + "[" + col30 + "]错误！" + "|";
                            }
                        }

                        string col32 = pds.Rows[i][32].ToString().Trim();
                        if (!string.IsNullOrEmpty(col32))
                        {
                            if (col32 != "是" && col32 != "否")
                            {
                                result += "第" + (i + 2).ToString() + "行," + "外籍" + "," + "[" + col32 + "]错误！" + "|";
                            }
                        }

                        string col33 = pds.Rows[i][33].ToString().Trim();
                        if (!string.IsNullOrEmpty(col33))
                        {
                            if (col33 != "是" && col33 != "否")
                            {
                                result += "第" + (i + 2).ToString() + "行," + "外聘" + "," + "[" + col33 + "]错误！" + "|";
                            }
                        }

                        string col34 = pds.Rows[i][34].ToString().Trim();
                        if (!string.IsNullOrEmpty(col34))
                        {
                            if (col34 != "是" && col34 != "否")
                            {
                                result += "第" + (i + 2).ToString() + "行," + "人员是否在场" + "," + "[" + col34 + "]错误！" + "|";
                            }
                        }
                        else
                        {
                            result += "第" + (i + 2).ToString() + "行," + "人员是否在场" + "," + "此项为必填项！" + "|";
                        }

                        string col35 = pds.Rows[i][35].ToString().Trim();
                        if (!string.IsNullOrEmpty(col35))
                        {
                            if (col35 != "是" && col35 != "否")
                            {
                                result += "第" + (i + 2).ToString() + "行," + "考勤卡是否启用" + "," + "[" + col35 + "]错误！" + "|";
                            }
                        }
                        else
                        {
                            result += "第" + (i + 2).ToString() + "行," + "考勤卡是否启用" + "," + "此项为必填项！" + "|";
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

                AddDatasetToSQL2(ds.Tables[0], 36);
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
            persons.Clear();
            ic = pds.Columns.Count;
            if (ic < Cols)
            {
                Alert.ShowInTop("导入Excel格式错误！Excel只有" + ic.ToString().Trim() + "列", MessageBoxIcon.Warning);
            }

            ir = pds.Rows.Count;
            if (pds != null && ir > 0)
            {
                var units = from x in Funs.DB.Base_Unit
                            select x;
                var cnProfessionals = from x in Funs.DB.Base_CNProfessional select x;
                var basicDatas = from x in Funs.DB.RealName_BasicData select x;
                var countrys = from x in Funs.DB.RealName_Country select x;
                var citys = from x in Funs.DB.RealName_City select x;
                var teamGroups = from x in Funs.DB.ProjectData_TeamGroup
                                 where x.ProjectId == this.ProjectId
                                 select x;
                var workAreas = from x in Funs.DB.WBS_UnitWork
                                where x.ProjectId == this.ProjectId
                                select x;
                var posts = from x in Funs.DB.Base_WorkPost
                            select x;
                var certificates = from x in Funs.DB.Base_Certificate
                                   select x;
                var positions = from x in Funs.DB.Base_Position select x;
                var postTitles = from x in Funs.DB.Base_PostTitle select x;
                for (int i = 0; i < ir; i++)
                {
                    string col1 = pds.Rows[i][1].ToString().Trim();
                    if (!string.IsNullOrEmpty(col1))
                    {
                        Model.View_SitePerson_Person person = new Model.View_SitePerson_Person();
                        Model.QualityAudit_PersonQuality personQuality = new Model.QualityAudit_PersonQuality();
                        string col0 = pds.Rows[i][0].ToString().Trim();

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
                        string col14 = pds.Rows[i][14].ToString().Trim();
                        string col15 = pds.Rows[i][15].ToString().Trim();
                        string col16 = pds.Rows[i][16].ToString().Trim();
                        string col17 = pds.Rows[i][17].ToString().Trim();
                        string col18 = pds.Rows[i][18].ToString().Trim();
                        string col19 = pds.Rows[i][19].ToString().Trim();
                        string col20 = pds.Rows[i][20].ToString().Trim();
                        string col21 = pds.Rows[i][21].ToString().Trim();
                        string col22 = pds.Rows[i][22].ToString().Trim();
                        string col23 = pds.Rows[i][23].ToString().Trim();
                        string col24 = pds.Rows[i][24].ToString().Trim();
                        string col25 = pds.Rows[i][25].ToString().Trim();
                        string col26 = pds.Rows[i][26].ToString().Trim();
                        string col27 = pds.Rows[i][27].ToString().Trim();
                        string col28 = pds.Rows[i][28].ToString().Trim();
                        string col29 = pds.Rows[i][29].ToString().Trim();
                        string col30 = pds.Rows[i][30].ToString().Trim();
                        string col31 = pds.Rows[i][31].ToString().Trim();
                        string col32 = pds.Rows[i][32].ToString().Trim();
                        string col33 = pds.Rows[i][33].ToString().Trim();
                        string col34 = pds.Rows[i][34].ToString().Trim();
                        string col35 = pds.Rows[i][35].ToString().Trim();

                        if (!string.IsNullOrEmpty(col0))//卡号
                        {
                            person.CardNo = col0;
                        }

                        if (!string.IsNullOrEmpty(col1))//姓名
                        {
                            person.PersonName = col1;
                            person.ProjectId = this.ProjectId;

                        }
                        if (!string.IsNullOrEmpty(col2))//性别
                        {
                            person.SexName = col2;
                        }
                        if (!string.IsNullOrEmpty(col3))//证件类型
                        {
                            person.IdcardTypeName = col3;
                            person.IdcardType = basicDatas.FirstOrDefault(x => x.DictName == col3).DictCode;
                        }
                        if (!string.IsNullOrEmpty(col4))//身份证号码
                        {
                            person.IdentityCard = col4;
                        }
                        if (!string.IsNullOrEmpty(col5))//证件开始日期
                        {
                            person.IdcardStartDate = Funs.GetNewDateTime(col5);
                        }
                        if (!string.IsNullOrEmpty(col6))//证件是否永久有效
                        {
                            person.IdcardForeverStr = col6;
                            person.IdcardForever = col6 == "是" ? "Y" : "N";
                        }
                        if (!string.IsNullOrEmpty(col7))//证件有效日期
                        {
                            person.IdcardEndDate = Funs.GetNewDateTime(col7);
                        }
                        if (!string.IsNullOrEmpty(col8))//发证机关
                        {
                            person.IdcardAddress = col8;
                        }
                        if (!string.IsNullOrEmpty(col9))//所属单位
                        {
                            var unit = units.FirstOrDefault(x => x.UnitName == col9);
                            if (unit != null)
                            {
                                person.UnitId = unit.UnitId;
                                person.UnitName = unit.UnitName;
                            }
                        }
                        if (!string.IsNullOrEmpty(col10))//岗位
                        {
                            var post = posts.FirstOrDefault(e => e.WorkPostName == col10);
                            if (post != null)
                            {
                                person.WorkPostId = post.WorkPostId;
                                person.WorkPostName = post.WorkPostName;
                            }
                        }
                        if (!string.IsNullOrEmpty(col11))//所在班组
                        {
                            var teamGroup = teamGroups.FirstOrDefault(e => e.TeamGroupName == col11);
                            if (teamGroup != null)
                            {
                                person.TeamGroupId = teamGroup.TeamGroupId;
                                person.TeamGroupName = teamGroup.TeamGroupName;
                            }
                        }
                        if (!string.IsNullOrEmpty(col12))//单位工程
                        {
                            person.WorkAreaName = col12;
                            string ids = string.Empty;
                            string[] strs = col12.Split('，');
                            foreach (var item in strs)
                            {
                                var workArea = workAreas.FirstOrDefault(e => e.UnitWorkName == item);
                                if (workArea != null)
                                {
                                    ids += workArea.UnitWorkId + ",";
                                }
                            }
                            if (!string.IsNullOrEmpty(ids))
                            {
                                ids = ids.Substring(0, ids.Length - 1);
                            }
                            person.WorkAreaId = ids;
                        }
                        if (!string.IsNullOrEmpty(col13))//入场时间
                        {
                            person.InTime = Funs.GetNewDateTime(col13);
                        }
                        if (!string.IsNullOrEmpty(col14))//所属职务
                        {
                            var position = positions.FirstOrDefault(e => e.PositionName == col14);
                            if (position != null)
                            {
                                person.PositionId = position.PositionId;
                                person.PositionName = position.PositionName;
                            }
                        }
                        if (!string.IsNullOrEmpty(col15))//所属职称
                        {
                            var postTitle = postTitles.FirstOrDefault(e => e.PostTitleName == col15);
                            if (postTitle != null)
                            {
                                person.PostTitleId = postTitle.PostTitleId;
                                person.PostTitleName = postTitle.PostTitleName;
                            }
                        }
                        if (!string.IsNullOrEmpty(col16))//文化程度
                        {
                            var basicData = basicDatas.FirstOrDefault(e => e.DictName == col16);
                            if (basicData != null)
                            {
                                person.EduLevel = basicData.DictCode;
                                person.EduLevelName = basicData.DictName;
                            }
                        }
                        if (!string.IsNullOrEmpty(col17))//婚姻状况
                        {
                            var basicData = basicDatas.FirstOrDefault(e => e.DictName == col17);
                            if (basicData != null)
                            {
                                person.MaritalStatus = basicData.DictCode;
                                person.MaritalStatusName = basicData.DictName;
                            }
                        }
                        if (!string.IsNullOrEmpty(col18))//政治面貌
                        {
                            var basicData = basicDatas.FirstOrDefault(e => e.DictName == col18);
                            if (basicData != null)
                            {
                                person.PoliticsStatus = basicData.DictCode;
                                person.PoliticsStatusName = basicData.DictName;
                            }
                        }
                        if (!string.IsNullOrEmpty(col19))//民族
                        {
                            var basicData = basicDatas.FirstOrDefault(e => e.DictName == col19);
                            if (basicData != null)
                            {
                                person.Nation = basicData.DictCode;
                                person.NationName = basicData.DictName;
                            }
                        }
                        if (!string.IsNullOrEmpty(col20))//国家
                        {
                            var country = countrys.FirstOrDefault(e => e.Cname == col20);
                            if (country != null)
                            {
                                person.CountryCode = country.CountryId;
                                person.CountryName = country.Cname;
                            }
                        }
                        if (!string.IsNullOrEmpty(col21))//省或地区
                        {
                            var city = citys.FirstOrDefault(e => e.Cname == col21);
                            if (city != null)
                            {
                                person.ProvinceCode = city.ProvinceCode;
                                person.ProvinceName = city.Cname;
                            }
                        }
                        if (!string.IsNullOrEmpty(col22))//主专业
                        {
                            var cnProfessional = cnProfessionals.FirstOrDefault(e => e.ProfessionalName == col22);
                            if (cnProfessional != null)
                            {
                                person.MainCNProfessionalId = cnProfessional.CNProfessionalId;
                                person.MainCNProfessionalName = cnProfessional.ProfessionalName;
                            }
                        }
                        if (!string.IsNullOrEmpty(col23))//副专业
                        {
                            person.ViceCNProfessionalName = col23;
                            string ids = string.Empty;
                            string[] strs = col23.Split('，');
                            foreach (var item in strs)
                            {
                                var cnProfessional = cnProfessionals.FirstOrDefault(e => e.ProfessionalName == item);
                                if (cnProfessional != null)
                                {
                                    ids += cnProfessional.CNProfessionalId + ",";
                                }
                            }
                            if (!string.IsNullOrEmpty(ids))
                            {
                                ids = ids.Substring(0, ids.Length - 1);
                            }
                            person.ViceCNProfessionalId = ids;
                        }
                        if (!string.IsNullOrEmpty(col24))//特岗证书
                        {
                            personQuality.CertificateName = col24;
                        }
                        if (!string.IsNullOrEmpty(col25))//证书编号
                        {
                            personQuality.CertificateNo = col25;
                        }
                        if (!string.IsNullOrEmpty(col26))//证书有效期
                        {
                            personQuality.LimitDate = Funs.GetNewDateTime(col26);
                        }
                        if (!string.IsNullOrEmpty(col27))//出生日期
                        {
                            person.Birthday = Funs.GetNewDateTime(col27);
                        }
                        if (!string.IsNullOrEmpty(col28))//电话
                        {
                            person.Telephone = col28;
                        }
                        if (!string.IsNullOrEmpty(col29))//家庭地址
                        {
                            person.Address = col29;
                        }
                        if (!string.IsNullOrEmpty(col30))//出场时间
                        {
                            person.OutTime = Funs.GetNewDateTime(col30);
                        }
                        if (!string.IsNullOrEmpty(col31))//出场原因
                        {
                            person.OutResult = col31;
                        }
                        if (!string.IsNullOrEmpty(col32))//外籍
                        {
                            person.IsForeignStr = col32;
                            person.IsForeign = col32 == "是" ? true : false;
                        }
                        if (!string.IsNullOrEmpty(col33))//外聘
                        {
                            person.IsOutsideStr = col33;
                            person.IsOutside = col33 == "是" ? true : false;
                        }
                        if (!string.IsNullOrEmpty(col34))//人员是否在场
                        {
                            person.IsUsedName = col34;
                        }
                        if (!string.IsNullOrEmpty(col35))//考勤卡是否启用
                        {
                            person.IsCardUsedName = col35;
                        }
                        person.PersonId = SQLHelper.GetNewID(typeof(Model.SitePerson_Person));
                        persons.Add(person);

                        personQuality.Remark = person.IdentityCard;
                        personQualitys.Add(personQuality);
                    }
                }
                if (persons.Count > 0)
                {
                    this.Grid1.Hidden = false;
                    this.Grid1.DataSource = persons;
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
                var certificates = from x in Funs.DB.Base_Certificate select x;
                int a = persons.Count();
                for (int i = 0; i < a; i++)
                {
                    var getPerson = PersonService.GetPersonCountByIdentityCard(persons[i].IdentityCard, Request.Params["ProjectId"]);
                    //!BLL.PersonService.IsExistPersonByUnit(persons[i].UnitId, persons[i].IdentityCard, Request.Params["ProjectId"]) &&
                    if (getPerson == null)
                    {
                        Model.SitePerson_Person newPerson = new Model.SitePerson_Person();
                        string newKeyID = SQLHelper.GetNewID(typeof(Model.SitePerson_Person));
                        newPerson.PersonId = newKeyID;
                        newPerson.ProjectId = Request.Params["ProjectId"];
                        newPerson.CardNo = persons[i].CardNo;
                        newPerson.PersonName = persons[i].PersonName;
                        newPerson.Sex = persons[i].SexName == "男" ? "1" : "2";
                        newPerson.IdcardType = persons[i].IdcardType;
                        newPerson.IdentityCard = persons[i].IdentityCard;
                        newPerson.IdcardStartDate = persons[i].IdcardStartDate;
                        newPerson.IdcardForever = persons[i].IdcardForever;
                        newPerson.IdcardEndDate = persons[i].IdcardEndDate;
                        newPerson.IdcardAddress = persons[i].IdcardAddress;
                        newPerson.UnitId = persons[i].UnitId;
                        newPerson.WorkPostId = persons[i].WorkPostId;
                        newPerson.TeamGroupId = persons[i].TeamGroupId;
                        newPerson.WorkAreaId = persons[i].WorkAreaId;
                        newPerson.InTime = persons[i].InTime;
                        newPerson.PositionId = persons[i].PositionId;
                        newPerson.PostTitleId = persons[i].PostTitleId;
                        newPerson.EduLevel = persons[i].EduLevel;
                        newPerson.MaritalStatus = persons[i].MaritalStatus;
                        newPerson.PoliticsStatus = persons[i].PoliticsStatus;
                        newPerson.Nation = persons[i].Nation;
                        newPerson.CountryCode = persons[i].CountryCode;
                        newPerson.ProvinceCode = persons[i].ProvinceCode;
                        newPerson.MainCNProfessionalId = persons[i].MainCNProfessionalId;
                        newPerson.ViceCNProfessionalId = persons[i].ViceCNProfessionalId;
                        newPerson.Birthday = persons[i].Birthday;
                        newPerson.Telephone = persons[i].Telephone;
                        newPerson.Address = persons[i].Address;
                        newPerson.OutTime = persons[i].OutTime;
                        newPerson.OutResult = persons[i].OutResult;
                        newPerson.IsForeign = persons[i].IsForeign;
                        newPerson.IsOutside = persons[i].IsOutside;
                        newPerson.IsUsed = persons[i].IsUsedName == "是" ? true : false;
                        newPerson.IsCardUsed = persons[i].IsCardUsedName == "是" ? true : false;
                        BLL.PersonService.AddPerson(newPerson);

                        var item = personQualitys.FirstOrDefault(x => x.Remark == newPerson.IdentityCard);
                        if (item != null)
                        {
                            Model.QualityAudit_PersonQuality newPersonQuality = new Model.QualityAudit_PersonQuality
                            {
                                PersonQualityId = SQLHelper.GetNewID(typeof(Model.QualityAudit_PersonQuality)),
                                PersonId = newPerson.PersonId,
                                CompileMan = this.CurrUser.UserId,
                                CompileDate = DateTime.Now
                            };
                            var certificate = certificates.FirstOrDefault(x => x.CertificateName == item.CertificateName);
                            if (certificate != null)
                            {
                                newPersonQuality.CertificateId = certificate.CertificateId;
                            }
                            newPersonQuality.CertificateName = item.CertificateName;
                            newPersonQuality.CertificateNo = item.CertificateNo;
                            newPersonQuality.LimitDate = item.LimitDate;
                            BLL.PersonQualityService.AddPersonQuality(newPersonQuality);
                        }
                    }
                    else
                    {
                        getPerson.CardNo = persons[i].CardNo;
                        getPerson.PersonName = persons[i].PersonName;
                        getPerson.Sex = persons[i].SexName == "男" ? "1" : "2";
                        getPerson.IdentityCard = persons[i].IdentityCard;
                        getPerson.Address = persons[i].Address;
                        getPerson.UnitId = persons[i].UnitId;
                        getPerson.TeamGroupId = persons[i].TeamGroupId;
                        getPerson.WorkAreaId = persons[i].WorkAreaId;
                        getPerson.WorkPostId = persons[i].WorkPostId;
                        //newPerson.CertificateId = persons[i].CertificateId;
                        //newPerson.CertificateCode = persons[i].CertificateCode;
                        //newPerson.CertificateLimitTime = persons[i].CertificateLimitTime;
                        getPerson.InTime = persons[i].InTime;
                        getPerson.OutTime = persons[i].OutTime;
                        getPerson.OutResult = persons[i].OutResult;
                        getPerson.Telephone = persons[i].Telephone;
                        getPerson.IsUsed = persons[i].IsUsedName == "是" ? true : false;
                        getPerson.IsCardUsed = persons[i].IsCardUsedName == "是" ? true : false;
                        Funs.DB.SubmitChanges();
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
            //string strFileName = DateTime.Now.ToString("yyyyMMdd-hhmmss");
            //System.Web.HttpContext HC = System.Web.HttpContext.Current;
            //HC.Response.Clear();
            //HC.Response.Buffer = true;
            //HC.Response.ContentEncoding = System.Text.Encoding.UTF8;//设置输出流为简体中文

            ////---导出为Excel文件
            //HC.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(strFileName, System.Text.Encoding.UTF8) + ".xls");
            //HC.Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。

            //System.IO.StringWriter sw = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            //this.gvErrorInfo.RenderControl(htw);
            //HC.Response.Write(sw.ToString());
            //HC.Response.End();
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
            if (Session["persons"] != null)
            {
                persons = Session["persons"] as List<Model.View_SitePerson_Person>;
            }
            if (persons.Count > 0)
            {
                this.Grid1.Hidden = false;
                //this.gvErrorInfo.Hidden = true;
                //this.btnOut.Hidden = true;
                this.Grid1.DataSource = persons;
                this.Grid1.DataBind();
            }
        }

        ///// <summary>
        ///// 关闭保存弹出窗口
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void Window3_Close(object sender, WindowCloseEventArgs e)
        //{
        //    if (Session["persons"] != null)
        //    {
        //        persons = Session["persons"] as List<Model.View_DataIn_AccidentCauseReport>;
        //    }
        //    if (persons.Count > 0)
        //    {
        //        this.Grid1.Visible = true;
        //        this.Form2.Visible = false;
        //        this.Grid1.DataSource = persons;
        //        this.Grid1.DataBind();
        //    }
        //}
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
                string uploadfilepath = rootPath + Const.PersonTemplateUrl;
                string filePath = Const.PersonTemplateUrl;
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