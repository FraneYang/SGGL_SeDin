using Aspose.Words;
using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace FineUIPro.Web.HSSE.Manager
{
    public partial class HSEDiary : PageBase
    {
        #region 项目主键
        /// <summary>
        /// 项目主键
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
                Funs.DropDownPageSize(this.ddlPageSize);
                ////权限按钮方法
                this.GetButtonPower();
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.CurrUser.LoginProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }

                UserService.InitUserDropDownList(this.drpUser, this.ProjectId, true);
                this.drpUser.SelectedValue = this.CurrUser.UserId;
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
            string strSql = "SELECT H.HSEDiaryId,H.ProjectId,H.DiaryDate,H.UserId,U.UserName,Unit.UnitCode,Unit.UnitName,H.DailySummary,H.TomorrowPlan"
                        + @" FROM Project_HSEDiary AS H"
                        + @" LEFT JOIN Sys_User AS U ON H.UserId=U.UserId "
                        + @" LEFT JOIN Base_Unit AS Unit ON Unit.UnitId=U.UnitId"
                        + @" WHERE H.ProjectId= '" + this.ProjectId + "'";
            List<SqlParameter> listStr = new List<SqlParameter>();

            if (this.drpUser.SelectedValue != Const._Null)
            {
                strSql += " AND H.UserId = @UserId";
                listStr.Add(new SqlParameter("@UserId", this.drpUser.SelectedValue));
            }
            if (!string.IsNullOrEmpty(this.txtDiaryDate.Text))
            {
                strSql += " AND H.DiaryDate = @DiaryDate";
                listStr.Add(new SqlParameter("@DiaryDate", this.txtDiaryDate.Text));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        /// <summary>
        /// 改变索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 分页下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

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

        #region 编辑
        /// <summary>
        /// 双击事件
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
        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 编辑数据方法
        /// </summary>
        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string id = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HSEDiaryView.aspx?HSEDiaryId={0}", id, "查看 - ")));
        }
        #endregion

        #region 删除
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    APIHSEDiaryService.DeleteHSEDiary(rowID);
                }

                this.BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
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
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.ProjectHSEDiaryMenuId);
            if (buttonList.Count() > 0)
            {
                //if (buttonList.Contains(BLL.Const.BtnAdd))
                //{
                //    this.btnNew.Hidden = false;
                //}
                if (buttonList.Contains(Const.BtnModify))
                {
                    this.btnMenuView.Hidden = false;
                }
                if (buttonList.Contains(Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
                }
            }
        }
        #endregion

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("HSE日志" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = Encoding.UTF8;
            this.Grid1.PageSize = this.Grid1.RecordCount;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion        
        #region 导出详细
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrinter_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string Id = Grid1.SelectedRowID;
            var getHSEDiary = Funs.DB.Project_HSEDiary.FirstOrDefault(x => x.HSEDiaryId == Id);
            string rootPath = Server.MapPath("~/");
            string initTemplatePath = string.Empty;
            string uploadfilepath = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            initTemplatePath = "File\\Word\\HSSE\\安全日志.doc";
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".doc", string.Format("{0:yyyy-MM}", DateTime.Now) + ".doc");
            filePath = initTemplatePath.Replace(".doc", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
            if (File.Exists(newUrl))
            {
                File.Delete(newUrl);
            }
            File.Copy(uploadfilepath, newUrl);
            ///更新书签
            Document doc = new Aspose.Words.Document(newUrl);
            if (getHSEDiary != null)
            {
                if (getHSEDiary.DiaryDate.HasValue && !string.IsNullOrEmpty(getHSEDiary.UserId))
                {
                    
                    Bookmark bookmarkDateTime = doc.Range.Bookmarks["DateTime"];
                    if (bookmarkDateTime != null)
                    {
                        bookmarkDateTime.Text = string.Format("{0:yyyy-MM-dd}", getHSEDiary.DiaryDate);
                    }
                    Bookmark bookmarkHSSEEngineer = doc.Range.Bookmarks["HSSEEngineer"];
                    if (bookmarkHSSEEngineer != null)
                    {
                        var getUser = UserService.GetUserByUserId(getHSEDiary.UserId);
                        if (getUser != null)
                        {
                            bookmarkHSSEEngineer.Text = getUser.UserName;
                        }
                    }
                    var getFlowOperteList = APIHSEDiaryService.ReturnFlowOperteList(getHSEDiary.ProjectId, getHSEDiary.UserId, getHSEDiary.DiaryDate.Value);
                    Bookmark bookmarkValue1 = doc.Range.Bookmarks["Value1"];
                    if (bookmarkValue1 != null)
                    {
                        bookmarkValue1.Text = APIHSEDiaryService.getValues1(getFlowOperteList, getHSEDiary.ProjectId, getHSEDiary.UserId, getHSEDiary.DiaryDate.Value);
                    }
                    Bookmark bookmarkValue2 = doc.Range.Bookmarks["Value2"];
                    if (bookmarkValue2 != null)
                    {
                        bookmarkValue2.Text = APIHSEDiaryService.getValues2(getFlowOperteList, getHSEDiary.ProjectId, getHSEDiary.UserId, getHSEDiary.DiaryDate.Value);
                    }
                    Bookmark bookmarkValue3 = doc.Range.Bookmarks["Value3"];
                    if (bookmarkValue3 != null)
                    {
                        bookmarkValue3.Text = APIHSEDiaryService.getValues3(getFlowOperteList, getHSEDiary.ProjectId, getHSEDiary.UserId, getHSEDiary.DiaryDate.Value);
                    }
                    Bookmark bookmarkValue4 = doc.Range.Bookmarks["Value4"];
                    if (bookmarkValue4 != null)
                    {
                        bookmarkValue4.Text = APIHSEDiaryService.getValues4(getFlowOperteList, getHSEDiary.ProjectId, getHSEDiary.UserId, getHSEDiary.DiaryDate.Value);
                    }
                    Bookmark bookmarkValue5 = doc.Range.Bookmarks["Value5"];
                    if (bookmarkValue5 != null)
                    {
                        bookmarkValue5.Text = APIHSEDiaryService.getValues5(getFlowOperteList, getHSEDiary.ProjectId, getHSEDiary.UserId, getHSEDiary.DiaryDate.Value);
                    }
                    Bookmark bookmarkValue6 = doc.Range.Bookmarks["Value6"];
                    if (bookmarkValue6 != null)
                    {
                        bookmarkValue6.Text = APIHSEDiaryService.getValues6(getFlowOperteList, getHSEDiary.ProjectId, getHSEDiary.UserId, getHSEDiary.DiaryDate.Value);
                    }
                    Bookmark bookmarkValue7 = doc.Range.Bookmarks["Value7"];
                    if (bookmarkValue7 != null)
                    {
                        bookmarkValue7.Text = APIHSEDiaryService.getValues7(getFlowOperteList, getHSEDiary.ProjectId, getHSEDiary.UserId, getHSEDiary.DiaryDate.Value);
                    }
                    Bookmark bookmarkValue8 = doc.Range.Bookmarks["Value8"];
                    if (bookmarkValue8 != null)
                    {
                        bookmarkValue8.Text = APIHSEDiaryService.getValues8(getFlowOperteList, getHSEDiary.ProjectId, getHSEDiary.UserId, getHSEDiary.DiaryDate.Value);
                    }
                    Bookmark bookmarkValue9 = doc.Range.Bookmarks["Value9"];
                    if (bookmarkValue9 != null)
                    {
                        bookmarkValue9.Text = APIHSEDiaryService.getValues9(getFlowOperteList, getHSEDiary.ProjectId, getHSEDiary.UserId, getHSEDiary.DiaryDate.Value);
                    }
                    Bookmark bookmarkValue10 = doc.Range.Bookmarks["Value10"];
                    if (bookmarkValue9 != null)
                    {
                        bookmarkValue10.Text = APIHSEDiaryService.getValues10(getFlowOperteList, getHSEDiary.ProjectId, getHSEDiary.UserId, getHSEDiary.DiaryDate.Value);
                    }
                    Bookmark bookmarkDailySummary = doc.Range.Bookmarks["DailySummary"];
                    if (bookmarkDailySummary != null)
                    {
                        bookmarkDailySummary.Text = getHSEDiary.DailySummary;
                    }
                    Bookmark bookmarkTomorrowPlan = doc.Range.Bookmarks["TomorrowPlan"];
                    if (bookmarkTomorrowPlan != null)
                    {
                        bookmarkTomorrowPlan.Text = getHSEDiary.TomorrowPlan;
                    }
                }
            }
            doc.Save(newUrl);
            //生成PDF文件
            string pdfUrl = newUrl.Replace(".doc", ".pdf");
            Document doc1 = new Aspose.Words.Document(newUrl);
            //验证参数
            if (doc1 == null) { throw new Exception("Word文件无效"); }
            doc1.Save(pdfUrl, Aspose.Words.SaveFormat.Pdf);//还可以改成其它格式
            string fileName = Path.GetFileName(filePath);
            FileInfo info = new FileInfo(pdfUrl);
            long fileSize = info.Length;
            Response.Clear();
            Response.ContentType = "application/x-zip-compressed";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
            Response.AddHeader("Content-Length", fileSize.ToString());
            Response.TransmitFile(pdfUrl, 0, fileSize);
            Response.Flush();
            Response.Close();
            File.Delete(newUrl);
            File.Delete(pdfUrl);
        }
        #endregion

    }
}