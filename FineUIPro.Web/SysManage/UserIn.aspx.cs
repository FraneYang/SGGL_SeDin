using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web.UI;
using BLL;

namespace FineUIPro.Web.SysManage
{
    public partial class UserIn : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 上传预设的虚拟路径
        /// </summary>
        private string initPath = Const.ExcelUrl;

        /// <summary>
        /// 人员集合
        /// </summary>
        public static List<Model.View_Sys_User> userViews = new List<Model.View_Sys_User>();

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
                if (userViews != null)
                {
                    userViews.Clear();
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
                if (userViews != null)
                {
                    userViews.Clear();
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
                userViews.Clear();
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
                var units = from x in new Model.SGGLDB(Funs.ConnString).Base_Unit select x;
                var roles = from x in new Model.SGGLDB(Funs.ConnString).Sys_Role select x;
                for (int i = 0; i < ir; i++)
                {
                    Model.View_Sys_User newSysUser = new Model.View_Sys_User
                    {
                        RCount=i+2,
                        UserCode = pds.Rows[i][0].ToString().Trim(),
                        UserName = pds.Rows[i][1].ToString().Trim(),
                        Account = pds.Rows[i][2].ToString().Trim(),
                        UnitName = pds.Rows[i][3].ToString().Trim(),
                        RoleName = pds.Rows[i][4].ToString().Trim(),
                        IdentityCard = pds.Rows[i][5].ToString().Trim(),
                        Telephone = pds.Rows[i][6].ToString().Trim(),
                        IsPostName = pds.Rows[i][7].ToString().Trim(),
                        IsPost = pds.Rows[i][7].ToString().Trim() == "是" ? true : false,
                        IsOffice = pds.Rows[i][8].ToString().Trim() == "是" ? true : false,
                    };

                    if (string.IsNullOrEmpty(newSysUser.UserName))
                    {
                        result += "第" + (i + 2).ToString() + "行," + "人员姓名" + "," + "此项为必填项！" + "|";
                    }
                    if (string.IsNullOrEmpty(newSysUser.Account))
                    {
                        result += "第" + (i + 2).ToString() + "行," + "登录账号" + "," + "此项为必填项！" + "|";
                    }
                 
                    string unitName = newSysUser.UnitName;
                    if (!string.IsNullOrEmpty(unitName))
                    {
                        var unit = units.FirstOrDefault(e => e.UnitName == unitName);
                        if (unit == null)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "所属单位" + "," + "[" + unitName + "]不在单位表中！" + "|";
                        }
                        else
                        {
                            newSysUser.UnitId = unit.UnitId;
                        }

                    }
                    else
                    {
                        result += "第" + (i + 2).ToString() + "行," + "所属单位" + "," + "此项为必填项！" + "|";
                    }

                    string roleName = newSysUser.RoleName;
                    var role = roles.FirstOrDefault(e => e.RoleName == roleName);
                    if (role == null)
                    {
                        result += "第" + (i + 2).ToString() + "行," + "角色" + "," + "[" + roleName + "]错误！" + "|";
                    }
                    else
                    {
                        newSysUser.RoleId = role.RoleId;
                        newSysUser.RoleName = role.RoleName;
                    }
                                
                    if (!newSysUser.IsPost.HasValue)
                    {
                        result += "第" + (i + 2).ToString() + "行," + "在岗" + "," + "此项为必填项！" + "|";
                    }
                   
                    ///加入用户试图
                    userViews.Add(newSysUser);
                }
                if (!string.IsNullOrEmpty(result))
                {
                    userViews.Clear();
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
                    if (userViews.Count > 0)
                    {
                        this.Grid1.Hidden = false;
                        this.Grid1.DataSource = userViews;
                        this.Grid1.DataBind();
                        Alert.ShowInTop("审核完成,请点击保存！", MessageBoxIcon.Success);
                    }
                    else
                    {
                        Alert.ShowInTop("导入数据为空！", MessageBoxIcon.Warning);
                    }
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
                int a = userViews.Count();
                int insertCount = 0;
                int updateCount = 0;
                for (int i = 0; i < a; i++)
                {
                    Model.Sys_User newUser = new Model.Sys_User
                    {
                        UserCode = userViews[i].UserCode,
                        UserName = userViews[i].UserName,
                        Account = userViews[i].Account,
                        UnitId = userViews[i].UnitId,
                        RoleId = userViews[i].RoleId,
                        IdentityCard = userViews[i].IdentityCard,
                        Telephone = userViews[i].Telephone,
                        IsPost = userViews[i].IsPost,
                        IsOffice=userViews[i].IsOffice,
                    };
                    var getUser = new Model.SGGLDB(Funs.ConnString).Sys_User.FirstOrDefault(x => x.Account == userViews[i].Account);
                    if (getUser == null)
                    {
                        newUser.Password = Funs.EncryptionPassword(Const.Password);
                        BLL.UserService.AddUser(newUser);
                        insertCount++;
                    }
                    else
                    {
                        newUser.UserId = getUser.UserId;
                        BLL.UserService.UpdateUser(newUser);
                        updateCount++;
                    }
                }
                string rootPath = Server.MapPath("~/");
                string initFullPath = rootPath + initPath;
                string filePath = initFullPath + this.hdFileName.Text;
                if (filePath != string.Empty && File.Exists(filePath))
                {
                    File.Delete(filePath);//删除上传的XLS文件
                }
                ShowNotify("导入完成！插入"+ insertCount.ToString()+"条，更新"+updateCount.ToString()+"条记录。", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                Alert.ShowInTop("请先将错误数据修正，再重新导入保存！", MessageBoxIcon.Warning);
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
            if (Session["sysUsers"] != null)
            {
                userViews = Session["sysUsers"] as List<Model.View_Sys_User>;
            }
            if (userViews.Count > 0)
            {
                this.Grid1.Hidden = false;
                this.Grid1.DataSource = userViews;
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
                string uploadfilepath = rootPath + Const.UserTemplateUrl;
                string filePath = Const.UserTemplateUrl;
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