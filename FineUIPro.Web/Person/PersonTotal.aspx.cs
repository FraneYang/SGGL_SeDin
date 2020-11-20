using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.Person
{
    public partial class PersonTotal : PageBase
    {
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Funs.DropDownPageSize(this.ddlPageSize);
                ////权限按钮方法
                this.GetButtonPower();
                if (this.CurrUser.UserId == BLL.Const.sysglyId || this.CurrUser.UserId == BLL.Const.hfnbdId)
                {
                    this.btnOut.Hidden = false;
                }
                this.btnNew.OnClientClick = Window1.GetShowReference("PersonTotalEdit.aspx") + "return false;";
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                }

                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                this.BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"select PersonTotalId, T.UserId, Content, StartTime, EndTime ,U.UserName,RoleName
                             from PersonTotal T
                             left join Sys_User  U on T.UserId=U.UserId ";

            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.txtUserName.Text.Trim()))
            {
                strSql += " Where U.UserName LIKE @UserName";
                listStr.Add(new SqlParameter("@UserName", "%" + this.txtUserName.Text.Trim() + "%"));
            }
            if (this.CurrUser.UserId != BLL.Const.sysglyId && this.CurrUser.UserId != BLL.Const.hfnbdId)
            {
                strSql += " Where T.UserId = @UserId";
                listStr.Add(new SqlParameter("@UserId", this.CurrUser.UserId));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion 

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.PersonTotalMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
                }
            }
        }
        #endregion

        #region  删除数据
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            this.DeleteData();
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        private void DeleteData()
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                string strShowNotify = string.Empty;
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var user = BLL.UserService.GetUserByUserId(rowID);
                    if (user != null)
                    {
                        string cont = string.Empty;
                        //string cont = judgementDelete(rowID);
                        if (string.IsNullOrEmpty(cont))
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, user.UserCode, user.UserId, BLL.Const.PersonTotalMenuId, BLL.Const.BtnDelete);
                            BLL.PersonTotalService.DeletePersonTotal(Grid1.SelectedRowID);
                        }
                        else
                        {
                            strShowNotify += "人员：" + user.UserName + cont;
                        }
                    }
                }

                BindGrid();
                if (!string.IsNullOrEmpty(strShowNotify))
                {
                    Alert.ShowInTop(strShowNotify, MessageBoxIcon.Warning);
                }
                else
                {
                    ShowNotify("删除数据成功!", MessageBoxIcon.Success);
                }
            }
        }
        #endregion

        #region 分页
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 分页显示条数下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 编辑

        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            this.EditData();
        }
        protected void MenuView_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PersonTotalView.aspx?PersonTotalId={0}", Grid1.SelectedRowID, "查看 - ")));
        }
        /// <summary>
        /// 编辑数据方法
        /// </summary>
        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string Id = Grid1.SelectedRowID;
            if (BLL.CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.PersonTotalMenuId, BLL.Const.BtnModify))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PersonTotalEdit.aspx?PersonTotalId={0}", Id, "编辑 - ")));
            }
            else
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PersonTotalView.aspx?PersonTotalId={0}", Id, "查看 - ")));
            }

        }
        #endregion

        #region 判断是否可删除
        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private string judgementDelete(string id)
        {
            string content = string.Empty;
            if (Funs.DB.Project_ProjectUser.FirstOrDefault(x => x.UserId == id) != null)
            {
                content += "已在【项目用户】中使用，不能删除！";
            }

            return content;
        }

        #endregion

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            string rootPath = Server.MapPath("~/");
            var personTotals = from x in Funs.DB.PersonTotal select x;
            List<string> list = new List<string>();
            if (personTotals.Count() > 0)
            {
                string filePath = rootPath + "FileUpload\\员工总结\\";
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                foreach (var detail in personTotals)
                {
                    string attachFileUrls = BLL.AttachFileService.getFileUrl(detail.PersonTotalId);
                    string[] urls = attachFileUrls.Split(',');
                    foreach (var url in urls)
                    {
                        string atturl = Funs.RootPath + url;
                        if (File.Exists(atturl))
                        {
                            string newUrlPath = url.Substring(url.LastIndexOf("/"));
                            string newUrl = filePath + "/" + newUrlPath.Substring(newUrlPath.IndexOf("_") + 1);
                            if (!File.Exists(newUrl))
                            {
                                File.Copy(atturl, newUrl);
                                list.Add(newUrl);
                            }
                        }
                    }
                }
                string startPath = rootPath + "FileUpload\\员工总结\\";
                string zipPath = rootPath + "FileUpload\\员工总结.zip";
                ZipFile.CreateFromDirectory(startPath, zipPath);
                string fileName = Path.GetFileName(zipPath);
                FileInfo info = new FileInfo(zipPath);
                long fileSize = info.Length;
                Response.ClearContent();
                Response.ContentType = "application/x-zip-compressed";
                Response.AddHeader("content-disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                Response.AddHeader("Content-Length", fileSize.ToString());
                Response.TransmitFile(zipPath, 0, fileSize);
                Response.Flush();
                Response.Close();
                File.Delete(zipPath);
                foreach (var item in list)
                {
                    File.Delete(item);
                }
            }
            else
            {
                ShowNotify("尚无员工总结可以导出！", MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}