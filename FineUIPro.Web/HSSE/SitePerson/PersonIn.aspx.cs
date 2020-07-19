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

                AddDatasetToSQL(ds.Tables[0], 18);
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
                            if (col3.Length > 50)
                            {
                                result += "第" + (i + 2).ToString() + "行," + "身份证号码" + "," + "[" + col3 + "]错误！" + "|";
                            }

                            if (PersonService.GetPersonCountByIdentityCard(col3, this.ProjectId) != null)
                            {
                                result += "第" + (i + 2).ToString() + "行," + "身份证号码" + "," + "[" + col3 + "]已存在！" + "|";
                            }
                        }
                        else
                        {
                            result += "第" + (i + 2).ToString() + "行," + "身份证号码" + "," + "此项为必填项！" + "|";
                        }

                        string col5 = pds.Rows[i][5].ToString().Trim();
                        if (!string.IsNullOrEmpty(col5))
                        {
                            var unit = units.FirstOrDefault(e => e.UnitName == col5);
                            if (unit != null)
                            {
                                var projectUnit = Funs.DB.Project_ProjectUnit.FirstOrDefault(x => x.ProjectId == this.ProjectId && x.UnitId == unit.UnitId);
                                if (projectUnit == null)
                                {
                                    result += "第" + (i + 2).ToString() + "行," + "所属单位" + "," + "[" + col5 + "]不在本项目中！" + "|";
                                }
                            }
                            else
                            {
                                result += "第" + (i + 2).ToString() + "行," + "所属单位" + "," + "[" + col5 + "]不在单位表中！" + "|";
                            }
                        }
                        else
                        {
                            result += "第" + (i + 2).ToString() + "行," + "所属单位" + "," + "此项为必填项！" + "|";
                        }

                        string col6 = pds.Rows[i][6].ToString().Trim();
                        if (!string.IsNullOrEmpty(col6))
                        {
                            var teamGroup = teamGroups.FirstOrDefault(e => e.TeamGroupName == col6);
                            if (teamGroup == null)
                            {
                                result += "第" + (i + 2).ToString() + "行," + "所在班组" + "," + "[" + col6 + "]错误！" + "|";
                            }
                        }

                        string col7 = pds.Rows[i][7].ToString().Trim();
                        if (!string.IsNullOrEmpty(col7))
                        {
                            var workArea = workAreas.FirstOrDefault(e => e.UnitWorkName == col7);
                            if (workArea == null)
                            {
                                result += "第" + (i + 2).ToString() + "行," + "作业区域" + "," + "[" + col7 + "]错误！" + "|";
                            }
                        }

                        string col8 = pds.Rows[i][8].ToString().Trim();
                        if (!string.IsNullOrEmpty(col8))
                        {
                            var post = posts.FirstOrDefault(e => e.WorkPostName == col8);
                            if (post == null)
                            {
                                result += "第" + (i + 2).ToString() + "行," + "岗位" + "," + "[" + col8 + "]错误！" + "|";
                            }
                        }
                        else
                        {
                            result += "第" + (i + 2).ToString() + "行," + "岗位" + "," + "此项为必填项！" + "|";
                        }

                        string col9 = pds.Rows[i][9].ToString().Trim();
                        if (!string.IsNullOrEmpty(col9))
                        {
                            var certificate = certificates.FirstOrDefault(e => e.CertificateName == col9);
                            if (certificate == null)
                            {
                                result += "第" + (i + 2).ToString() + "行," + "特岗证书" + "," + "[" + col9 + "]错误！" + "|";
                            }
                        }

                        string col11 = pds.Rows[i][11].ToString().Trim();
                        if (!string.IsNullOrEmpty(col11))
                        {
                            try
                            {
                                DateTime date = Convert.ToDateTime(col11);
                            }
                            catch (Exception)
                            {
                                result += "第" + (i + 2).ToString() + "行," + "证书有效期" + "," + "[" + col11 + "]错误！" + "|";
                            }
                        }

                        string col12 = pds.Rows[i][12].ToString().Trim();
                        if (!string.IsNullOrEmpty(col12))
                        {
                            try
                            {
                                DateTime date = Convert.ToDateTime(col12);
                            }
                            catch (Exception)
                            {
                                result += "第" + (i + 2).ToString() + "行," + "入场时间" + "," + "[" + col12 + "]错误！" + "|";
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
                                result += "第" + (i + 2).ToString() + "行," + "出场时间" + "," + "[" + col13 + "]错误！" + "|";
                            }
                        }

                        string col16 = pds.Rows[i][16].ToString().Trim();
                        if (!string.IsNullOrEmpty(col16))
                        {
                            if (col16 != "是" && col16 != "否")
                            {
                                result += "第" + (i + 2).ToString() + "行," + "人员是否在场" + "," + "[" + col16 + "]错误！" + "|";
                            }
                        }
                        else
                        {
                            result += "第" + (i + 2).ToString() + "行," + "人员是否在场" + "," + "此项为必填项！" + "|";
                        }

                        string col17 = pds.Rows[i][17].ToString().Trim();
                        if (!string.IsNullOrEmpty(col17))
                        {
                            if (col17 != "是" && col17 != "否")
                            {
                                result += "第" + (i + 2).ToString() + "行," + "考勤卡是否启用" + "," + "[" + col17 + "]错误！" + "|";
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

                AddDatasetToSQL2(ds.Tables[0], 18);
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
                        if (!string.IsNullOrEmpty(col3))//身份证号码
                        {
                            person.IdentityCard = col3;
                        }
                        if (!string.IsNullOrEmpty(col4))//家庭地址
                        {
                            person.Address = col4;
                        }
                        if (!string.IsNullOrEmpty(col5))//所属单位
                        {
                            var unit = units.FirstOrDefault(x => x.UnitName == col5);
                            if (unit != null)
                            {
                                person.UnitId = unit.UnitId;
                                person.UnitName = unit.UnitName;
                            }
                        }
                        if (!string.IsNullOrEmpty(col6))//所在班组
                        {
                            var teamGroup = teamGroups.FirstOrDefault(e => e.TeamGroupName == col6);
                            if (teamGroup != null)
                            {
                                person.TeamGroupId = teamGroup.TeamGroupId;
                                person.TeamGroupName = teamGroup.TeamGroupName;
                            }
                        }
                        if (!string.IsNullOrEmpty(col7))//作业区域
                        {
                            var workArea = workAreas.FirstOrDefault(e => e.UnitWorkName == col7);
                            if (workArea != null)
                            {
                                person.WorkAreaId = workArea.UnitWorkId;
                                person.WorkAreaName = workArea.UnitWorkName;
                            }
                        }
                        if (!string.IsNullOrEmpty(col8))//岗位
                        {
                            var post = posts.FirstOrDefault(e => e.WorkPostName == col8);
                            if (post != null)
                            {
                                person.WorkPostId = post.WorkPostId;
                                person.WorkPostName = post.WorkPostName;
                            }
                        }
                        if (!string.IsNullOrEmpty(col9))//特岗证书
                        {
                            personQuality.CertificateName = col9;
                        }
                        if (!string.IsNullOrEmpty(col10))//证书编号
                        {
                            personQuality.CertificateNo = col10;
                        }
                        if (!string.IsNullOrEmpty(col11))//证书有效期
                        {
                            personQuality.LimitDate = Funs.GetNewDateTime(col11);
                        }
                        if (!string.IsNullOrEmpty(col12))//入场时间
                        {
                            person.InTime = Funs.GetNewDateTime(col12);
                        }
                        if (!string.IsNullOrEmpty(col13))//出场时间
                        {
                            person.OutTime = Funs.GetNewDateTime(col13);
                        }
                        if (!string.IsNullOrEmpty(col14))//出场原因
                        {
                            person.OutResult = col14;
                        }
                        if (!string.IsNullOrEmpty(col15))//电话
                        {
                            person.Telephone = col15;
                        }
                        if (!string.IsNullOrEmpty(col16))//人员是否在场
                        {
                            person.IsUsedName = col16;
                        }
                        if (!string.IsNullOrEmpty(col17))//考勤卡是否启用
                        {
                            person.IsCardUsedName = col17;
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
                    //!BLL.PersonService.IsExistPersonByUnit(persons[i].UnitId, persons[i].IdentityCard, Request.Params["ProjectId"]) &&
                    if (PersonService.GetPersonCountByIdentityCard(persons[i].IdentityCard, Request.Params["ProjectId"]) == null)
                    {
                        Model.SitePerson_Person newPerson = new Model.SitePerson_Person();
                        string newKeyID = SQLHelper.GetNewID(typeof(Model.SitePerson_Person));
                        newPerson.PersonId = newKeyID;
                        newPerson.ProjectId = Request.Params["ProjectId"];
                        newPerson.CardNo = persons[i].CardNo;
                        newPerson.PersonName = persons[i].PersonName;
                        newPerson.Sex = persons[i].SexName == "男" ? "1" : "2";
                        newPerson.IdentityCard = persons[i].IdentityCard;
                        newPerson.Address = persons[i].Address;
                        newPerson.UnitId = persons[i].UnitId;
                        newPerson.TeamGroupId = persons[i].TeamGroupId;
                        newPerson.WorkAreaId = persons[i].WorkAreaId;
                        newPerson.WorkPostId = persons[i].WorkPostId;
                        //newPerson.CertificateId = persons[i].CertificateId;
                        //newPerson.CertificateCode = persons[i].CertificateCode;
                        //newPerson.CertificateLimitTime = persons[i].CertificateLimitTime;
                        newPerson.InTime = persons[i].InTime;
                        newPerson.OutTime = persons[i].OutTime;
                        newPerson.OutResult = persons[i].OutResult;
                        newPerson.Telephone = persons[i].Telephone;
                        newPerson.IsUsed = persons[i].IsUsedName == "是" ? true : false;
                        newPerson.IsCardUsed = persons[i].IsCardUsedName == "是" ? true : false;
                        BLL.PersonService.AddPerson(newPerson);

                        var item = personQualitys.FirstOrDefault(x => x.Remark ==  newPerson.IdentityCard);
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