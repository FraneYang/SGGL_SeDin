using Aspose.Words;
using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace FineUIPro.Web.HSSE.Check
{
    public partial class CheckSpecial : PageBase
    {
        /// <summary>
        /// 项目id
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
                Funs.DropDownPageSize(this.ddlPageSize);
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.ProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                Technique_CheckItemSetService.InitCheckItemSetDropDownList(this.drpSupCheckItemSet, "2", "0", true);
                ////权限按钮方法
                this.GetButtonPower();
                btnNew.OnClientClick = Window1.GetShowReference("CheckSpecialEdit.aspx") + "return false;";
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT CheckSpecial.CheckSpecialId,CodeRecords.Code AS CheckSpecialCode,"
                          + @" CheckItemSet.CheckItemName,CheckSpecial.CheckTime,(CASE WHEN CheckSpecial.CheckType ='1' THEN '联合检查' ELSE '专项检查' END) AS CheckTypeName"
                          + @" ,(CASE WHEN CheckSpecial.States='2' THEN '已完成' WHEN CheckSpecial.States='1' THEN '待整改' ELSE '待提交' END) AS StatesName"
                          + @" FROM Check_CheckSpecial AS CheckSpecial "
                          + @" LEFT JOIN Sys_CodeRecords AS CodeRecords ON CheckSpecial.CheckSpecialId=CodeRecords.DataId "
                          + @" LEFT JOIN Technique_CheckItemSet AS CheckItemSet ON CheckItemSet.CheckItemSetId = CheckSpecial.CheckItemSetId where 1=1";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND CheckSpecial.ProjectId = @ProjectId";
            listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));

            if (this.rbStates.SelectedValue!="-1")
            {
                strSql += " AND CheckSpecial.States = @States"; 
                listStr.Add(new SqlParameter("@States", this.rbStates.SelectedValue));
            }
            if (this.rbType.SelectedValue != "-1")
            {
                if (this.rbType.SelectedValue == "1")
                {
                    strSql += " AND CheckSpecial.CheckType = @CheckType";
                    listStr.Add(new SqlParameter("@CheckType", this.rbType.SelectedValue));
                }
                else
                {
                    strSql += " AND (CheckSpecial.CheckType = @CheckType OR CheckSpecial.CheckType IS NULL) ";
                    listStr.Add(new SqlParameter("@CheckType", this.rbType.SelectedValue));
                }
            }
            if (this.drpSupCheckItemSet.SelectedValue!=BLL.Const._Null)
            {
                strSql += " AND CheckSpecial.CheckItemSetId = @CheckItemSetId";
                listStr.Add(new SqlParameter("@CheckItemSetId", this.drpSupCheckItemSet.SelectedValue ));
            }
            if (!string.IsNullOrEmpty(this.txtStartTime.Text.Trim()))
            {
                strSql += " AND CheckSpecial.CheckTime >= @StartTime";
                listStr.Add(new SqlParameter("@StartTime", this.txtStartTime.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtEndTime.Text.Trim()))
            {
                strSql += " AND CheckSpecial.CheckTime <= @EndTime";
                listStr.Add(new SqlParameter("@EndTime", this.txtEndTime.Text.Trim()));
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
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

        #region 表排序、分页、关闭窗口
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
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
        /// 关闭弹出窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region Grid双击事件
        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            btnMenuModify_Click(null, null);
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 编辑按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string CheckSpecialId = Grid1.SelectedRowID.Split(',')[0];
            var checkSpecial = BLL.Check_CheckSpecialService.GetCheckSpecialByCheckSpecialId(CheckSpecialId);
            if (checkSpecial != null)
            {
                if (this.btnMenuModify.Hidden || checkSpecial.States == BLL.Const.State_1 || checkSpecial.States == BLL.Const.State_2)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckSpecialView.aspx?CheckSpecialId={0}", CheckSpecialId, "查看 - ")));
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckSpecialEdit.aspx?CheckSpecialId={0}", CheckSpecialId, "编辑 - ")));
                }
            }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDel_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var checkSpecial = BLL.Check_CheckSpecialService.GetCheckSpecialByCheckSpecialId(rowID);
                    if (checkSpecial != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, checkSpecial.CheckSpecialCode, checkSpecial.CheckSpecialId, BLL.Const.ProjectCheckSpecialMenuId, BLL.Const.BtnDelete);
                        BLL.Check_CheckSpecialDetailService.DeleteCheckSpecialDetails(rowID);

                        BLL.Check_CheckSpecialService.DeleteCheckSpecial(rowID);
                    }
                }
                BindGrid();
                ShowNotify("删除数据成功!");
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.ProjectId, this.CurrUser.UserId, BLL.Const.ProjectCheckSpecialMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuModify.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDel.Hidden = false;
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("专项检查" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = this.Grid1.RecordCount;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rbStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        protected void drpSupCheckItemSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "click")
            {
                string[] checkSpecialDetail = (Grid1.DataKeys[e.RowIndex][0].ToString()).Split(',');
                if (checkSpecialDetail.Count() > 1)
                {
                    var detail = Check_CheckSpecialDetailService.GetCheckSpecialDetailByCheckSpecialDetailId(checkSpecialDetail[1]);
                    if (detail != null)
                    {
                        if (detail.DataType == "1")
                        {
                            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("RectifyNoticesView.aspx?RectifyNoticesId={0}", detail.DataId, "查看 - ")));
                        }
                        else if (detail.DataType == "2")
                        {
                            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PunishNoticeView.aspx?PunishNoticeId={0}", detail.DataId, "查看 - ")));
                        }
                        else if (detail.DataType == "3")
                        {
                            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PauseNoticeView.aspx?PauseNoticeId={0}", detail.DataId, "查看 - ")));
                        }
                    }
                }
            }
        }

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
            var checkSpecial = BLL.Check_CheckSpecialService.GetCheckSpecialByCheckSpecialId(Id);
            string rootPath = Server.MapPath("~/");
            string initTemplatePath = string.Empty;
            string uploadfilepath = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            initTemplatePath = "File\\Word\\HSSE\\专项检查.doc";
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".doc", string.Format("{0:yyyy-MM}", DateTime.Now) + ".doc");
            filePath = initTemplatePath.Replace(".doc", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
            if (File.Exists(newUrl)) {
                File.Delete(newUrl);
            }
            File.Copy(uploadfilepath, newUrl);
            ///更新书签
            Document doc = new Aspose.Words.Document(newUrl);

            Bookmark bookmarkProjectName = doc.Range.Bookmarks["ProjectName"];
            if (bookmarkProjectName != null)
            {
                if (checkSpecial != null)
                {
                    var project = ProjectService.GetProjectByProjectId(checkSpecial.ProjectId);
                    if (project != null)
                    {
                        bookmarkProjectName.Text = project.ProjectName;
                    }
                }

            }
            Bookmark bookmarkCheckSpecialCode = doc.Range.Bookmarks["CheckSpecialCode"];
            if (bookmarkCheckSpecialCode != null)
            {
                if (checkSpecial != null)
                {
                    if (!string.IsNullOrEmpty(checkSpecial.CheckSpecialCode))
                    {
                        bookmarkCheckSpecialCode.Text = checkSpecial.CheckSpecialCode;
                    }
                }

            }
            Bookmark bookmarkSupCheckItemSet = doc.Range.Bookmarks["SupCheckItemSet"];
            if (bookmarkSupCheckItemSet != null)
            {
                if (checkSpecial != null)
                {
                    if (!string.IsNullOrEmpty(checkSpecial.CheckItemSetId)) {
                        bookmarkSupCheckItemSet.Text = Technique_CheckItemSetService.GetCheckItemSetNameById(checkSpecial.CheckItemSetId);
                    }
                    
                }

            }
            Bookmark bookmarkCheckDate = doc.Range.Bookmarks["CheckDate"];
            if (bookmarkCheckDate != null)
            {
                if (checkSpecial != null)
                {
                    if (checkSpecial.CheckTime.HasValue) {
                        bookmarkCheckDate.Text = string.Format("{0:yyyy-MM-dd}", checkSpecial.CheckTime);
                    }
                    
                }

            }
            Bookmark bookmarkPartInPersons = doc.Range.Bookmarks["PartInPersons"];
            if (bookmarkPartInPersons != null)
            {
                if (checkSpecial != null)
                {
                    if (!string.IsNullOrEmpty(checkSpecial.PartInPersons))
                    {
                        bookmarkPartInPersons.Text = checkSpecial.PartInPersons;
                    }

                }

            }
            //专项检查列表
            Aspose.Words.DocumentBuilder builder = new Aspose.Words.DocumentBuilder(doc);
            builder.MoveToBookmark("tab");
            builder.StartTable();
            builder.RowFormat.Alignment = Aspose.Words.Tables.RowAlignment.Center;
            builder.CellFormat.Borders.LineStyle = LineStyle.Single;
            builder.CellFormat.Borders.Color = System.Drawing.Color.Black;
            builder.RowFormat.LeftIndent = 100;
            builder.Bold = false;
            builder.RowFormat.Height = 20;
            builder.Bold = false;
            var checkSpecialDetails = (from x in Funs.DB.View_CheckSpecialDetail
                                       where x.CheckSpecialId == Id
                                       orderby x.SortIndex
                                       select x).ToList();
            int num = 1;
            foreach (Model.View_CheckSpecialDetail detail in checkSpecialDetails)
            {
                //序号
                builder.InsertCell();
                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                builder.CellFormat.Width = 20;
                builder.Write(num.ToString());
                //单位工程
                builder.InsertCell();
                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                builder.CellFormat.Width = 55;
                builder.Write(string.IsNullOrEmpty(detail.CheckAreaName) ? "" : detail.CheckAreaName);
                //单位
                builder.InsertCell();
                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                builder.CellFormat.Width = 60;
                builder.Write(string.IsNullOrEmpty(detail.UnitName) ? "" : detail.UnitName);
                //问题描述
                builder.InsertCell();
                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                builder.CellFormat.Width = 140;
                builder.Write(string.IsNullOrEmpty(detail.Unqualified) ? "" : detail.Unqualified);
                //问题类型
                builder.InsertCell();
                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                builder.CellFormat.Width = 60;
                builder.Write(string.IsNullOrEmpty(detail.CheckItemName)?"": detail.CheckItemName);

                //处理结果
                builder.InsertCell();
                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                builder.CellFormat.Width = 50;
                builder.Write(string.IsNullOrEmpty(detail.CompleteStatusName) ? "" : detail.CompleteStatusName);
                //隐患类别
                builder.InsertCell();
                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                builder.CellFormat.Width = 50;
                builder.Write(string.IsNullOrEmpty(detail.HiddenHazardTypeName) ? "" : detail.HiddenHazardTypeName);
                builder.EndRow();
                num++;
            }
            builder.EndTable();
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