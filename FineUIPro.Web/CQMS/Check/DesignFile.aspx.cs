using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aspose.Words;
using BLL;

namespace FineUIPro.Web.CQMS.Check
{
    public partial class DesignFile : PageBase
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UnitService.InitUnitByProjectIdUnitTypeDropDownList1(drpUnit, this.CurrUser.LoginProjectId, true);//施工单位
                CNProfessionalService.InitCNProfessionalDownList(drpCNProfessional, true);//专业
                BLL.MainItemService.InitMainItemDownList(drpMainItem, this.CurrUser.LoginProjectId, true);//主项
                this.drpDesignType.DataTextField = "Text";
                this.drpDesignType.DataValueField = "Value";
                drpDesignType.DataSource = BLL.DesignService.GetDesignTypeList();
                drpDesignType.DataBind();
                Funs.FineUIPleaseSelect(drpDesignType);
                BindGrid();

            }
            else
            {
                var eventArgs = GetRequestEventArgument(); // 此函数所在文件：PageBase.cs
                if (eventArgs.StartsWith("ButtonClick"))
                {
                    if (drpCNProfessional.Hidden == false && drpMainItem.Hidden == false)
                    {
                        if (drpMainItem.SelectedValue != Const._Null && drpCNProfessional.SelectedValue != Const._Null)
                        {
                            Model.ProjectData_MainItem mainItem = MainItemService.GetMainItemByMainItemId(drpMainItem.SelectedValue);
                            if (mainItem == null)
                            {
                                Alert.ShowInTop("无数据可以导出！", MessageBoxIcon.Warning);
                            }
                            string fileNames = string.Empty;
                            if (mainItem != null)
                            {
                                fileNames = mainItem.MainItemName + "-" + drpCNProfessional.SelectedItem.Text;
                            }
                            string rootPath = Server.MapPath("~/");
                            string initTemplatePath = string.Empty;
                            string uploadfilepath = string.Empty;
                            string newUrl = string.Empty;
                            string filePath = string.Empty;
                            initTemplatePath = Const.DesignTemplateUrl;
                            uploadfilepath = rootPath + initTemplatePath;
                            newUrl = uploadfilepath.Replace(".doc", fileNames + ".doc");
                            filePath = initTemplatePath.Replace(".doc", fileNames + ".pdf");
                            File.Copy(uploadfilepath, newUrl);
                            //更新书签内容
                            Document doc = new Aspose.Words.Document(newUrl);
                            Bookmark bookmarkProjectName = doc.Range.Bookmarks["ProjectName"];
                            if (bookmarkProjectName != null)
                            {
                                var project = ProjectService.GetProjectByProjectId(CurrUser.LoginProjectId);
                                if (project != null)
                                {
                                    bookmarkProjectName.Text = project.ProjectName;
                                }
                            }
                            Bookmark bookmarkUnitWork = doc.Range.Bookmarks["UnitWork"];
                            if (bookmarkUnitWork != null)
                            {
                                if (mainItem != null)
                                {
                                    bookmarkUnitWork.Text = mainItem.MainItemName;
                                }
                            }
                            Bookmark bookmarkCNProfessional = doc.Range.Bookmarks["CNProfessional"];
                            if (bookmarkCNProfessional != null)
                            {
                                bookmarkCNProfessional.Text = drpCNProfessional.SelectedText;
                            }
                            Aspose.Words.DocumentBuilder builder = new Aspose.Words.DocumentBuilder(doc);
                            bool isbool = builder.MoveToBookmark("Table");
                            if (isbool)
                            {
                                builder.StartTable();
                                builder.RowFormat.Alignment = Aspose.Words.Tables.RowAlignment.Center;
                                builder.CellFormat.Borders.LineStyle = LineStyle.Single;
                                builder.CellFormat.Borders.Color = System.Drawing.Color.Black;
                                builder.RowFormat.LeftIndent = 5;
                                //builder.RowFormat.RightPadding = 50;
                                builder.Bold = false;
                                //builder.RowFormat.Height = 20;
                                //builder.CellFormat.Width = 80;
                            }
                            builder.RowFormat.Height = 20;
                            builder.Bold = false;
                            builder.Font.Size = 7;
                            var designs = from x in new Model.SGGLDB(Funs.ConnString).Check_Design
                                          where x.ProjectId == CurrUser.LoginProjectId && x.State == Const.Design_Complete
                                          && x.MainItemId == drpMainItem.SelectedValue && x.CNProfessionalCode == drpCNProfessional.SelectedValue
                                          select x;
                            int i = 1;
                            foreach (Model.Check_Design design in designs)
                            {
                                //序号
                                builder.InsertCell();
                                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                                builder.CellFormat.Width = 25;
                                builder.Write(i.ToString());
                                //变更类型
                                builder.InsertCell();
                                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                                builder.CellFormat.Width = 36;
                                builder.Write(design.DesignType);
                                //变更编号
                                builder.InsertCell();
                                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                                builder.CellFormat.Width = 39;
                                builder.Write(design.DesignCode);
                                //变更内容
                                builder.InsertCell();
                                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                                builder.CellFormat.Width = 39;
                                builder.Write(design.DesignContents);
                                //变更日期
                                builder.InsertCell();
                                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                                                                                                                        //builder.Font.Style= Aspose.Words.Style
                                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                                builder.CellFormat.Width = 40;
                                string date = string.Empty;
                                if (design.DesignDate != null)
                                {
                                    date = string.Format("{0:yyyy-MM-dd}", design.DesignDate);
                                }
                                builder.Write(date);
                                //实施单位
                                builder.InsertCell();
                                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                                builder.CellFormat.Width = 72;
                                builder.Write(UnitService.getUnitNamesUnitIds(design.CarryUnitIds));
                                //是否已按原图纸施工
                                builder.InsertCell();
                                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                                builder.CellFormat.Width = 72;
                                builder.Write(design.IsNoChange == true ? "是" : "否");
                                //是否需要增补材料
                                builder.InsertCell();
                                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                                builder.CellFormat.Width = 64;
                                builder.Write(design.IsNeedMaterial == true ? "是" : "否");
                                //增补材料采购方
                                builder.InsertCell();
                                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                                builder.CellFormat.Width = 60;
                                builder.Write(UnitService.getUnitNamesUnitIds(design.BuyMaterialUnitIds));
                                //材料预计到齐时间
                                builder.InsertCell();
                                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                                builder.CellFormat.Width = 65;
                                string date2 = string.Empty;
                                if (design.MaterialPlanReachDate != null)
                                {
                                    date2 = string.Format("{0:yyyy-MM-dd}", design.MaterialPlanReachDate);
                                }
                                builder.Write(date2);
                                //预计施工周期
                                builder.InsertCell();
                                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                                builder.CellFormat.Width = 52;
                                string day = string.Empty;
                                if (design.PlanDay != null)
                                {
                                    day = Convert.ToDecimal(design.PlanDay).ToString("0.#");
                                }
                                builder.Write(day);
                                //计划完成时间
                                builder.InsertCell();
                                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                                builder.CellFormat.Width = 53;
                                string date3 = string.Empty;
                                if (design.PlanCompleteDate != null)
                                {
                                    date3 = string.Format("{0:yyyy-MM-dd}", design.PlanCompleteDate);
                                }
                                builder.Write(date3);
                                //增补材料到齐时间
                                builder.InsertCell();
                                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                                builder.CellFormat.Width = 65;
                                string date4 = string.Empty;
                                if (design.MaterialRealReachDate != null)
                                {
                                    date4 = string.Format("{0:yyyy-MM-dd}", design.MaterialRealReachDate);
                                }
                                builder.Write(date4);
                                //施工完成时间
                                builder.InsertCell();
                                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                                builder.CellFormat.Width = 52;
                                string date5 = string.Empty;
                                if (design.RealCompleteDate != null)
                                {
                                    date5 = string.Format("{0:yyyy-MM-dd}", design.RealCompleteDate);
                                }
                                builder.Write(date5);
                                builder.EndRow();
                                i++;
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


                            //Response.ClearContent();
                            //Response.AddHeader("content-disposition", "attachment; filename=row_" + e.RowIndex + ".txt");
                            //Response.ContentType = "text/plain";
                            //Response.ContentEncoding = System.Text.Encoding.UTF8;
                            //Response.Write(result);
                            //Response.End();
                            Response.ClearContent();

                            //Response.Clear();
                            Response.ContentType = "application/x-zip-compressed";
                            //Response.ContentType = "text/plain";
                            //Response.ContentType = "application/octet-stream";
                            Response.AddHeader("content-disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                            Response.AddHeader("Content-Length", fileSize.ToString());
                            Response.TransmitFile(pdfUrl, 0, fileSize);
                            Response.Flush();
                            Response.Close();
                            File.Delete(newUrl);
                            File.Delete(pdfUrl);
                        }
                        else
                        {
                            Alert.ShowInTop("请选择主项及专业后，再导出内容！", MessageBoxIcon.Warning);
                        }
                    }
                }
            }
        }
        private void BindGrid()
        {
            string strSql = "select D.CarryUnitIds, D.BuyMaterialUnitIds,D.MainItemId,D.DesignId,D.ProjectId,M.MainItemName,C.ProfessionalName, D.State,D.DesignType,D.DesignCode,D.DesignContents,D.DesignDate,U.UnitName as CarryUnit,(case D.IsNoChange when 'true' then '是' when 'false' then '否' else '' end) IsNoChange,(case D.IsNeedMaterial when 'true' then '是' when 'false' then '否' else '' end) IsNeedMaterial,U1.UnitName as BuyMaterialUnit,D.MaterialPlanReachDate,D.PlanDay,D.PlanCompleteDate,D.PlanCompleteDate,D.RealCompleteDate,D.CompileMan,C.ProfessionalName, D.CompileDate from Check_Design D left join Base_Unit U1 on U1.UnitId = D.BuyMaterialUnitIds left join Base_Unit U on U.UnitId = D.CarryUnitIds left join ProjectData_MainItem M on M.MainItemId = D.MainItemId left join Base_DesignProfessional C on C.DesignProfessionalId = D.CNProfessionalCode";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " where D.ProjectId = @ProjectId";
            listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            if (drpDesignType.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND D.DesignType = @DesignType";
                listStr.Add(new SqlParameter("@DesignType", this.drpDesignType.SelectedValue));
            }
            if (drpUnit.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND U.UnitId = @UnitId";
                listStr.Add(new SqlParameter("@UnitId", this.drpUnit.SelectedValue));
            }
            if (drpMainItem.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND D.MainItemId = @MainItemId";
                listStr.Add(new SqlParameter("@MainItemId", this.drpMainItem.SelectedValue));
            }
            if (drpCNProfessional.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND D.CNProfessionalCode = @CNProfessionalCode";
                listStr.Add(new SqlParameter("@CNProfessionalCode", this.drpCNProfessional.SelectedValue));
            }
            if (!string.IsNullOrEmpty(txtStartTime.Text.Trim()))
            {
                strSql += " AND DesignDate >= @DesignDate";
                listStr.Add(new SqlParameter("@DesignDate", txtStartTime.Text.Trim() + " 00:00:00"));
            }
            if (!string.IsNullOrEmpty(txtEndTime.Text.Trim()))
            {
                strSql += " AND DesignDate <= @DesignDateE";
                listStr.Add(new SqlParameter("@DesignDateE", txtEndTime.Text.Trim() + " 23:59:59"));
            }
            strSql += " AND D.State=@State";
            listStr.Add(new SqlParameter("@State", Const.Design_Complete));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        //<summary>
        //获取办理人姓名
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        protected string ConvertMan(object designId)
        {
            if (designId != null)
            {
                Model.Check_DesignApprove a = BLL.DesignApproveService.GetDesignApproveByDesignId(designId.ToString());
                if (a != null)
                {
                    if (a.ApproveMan != null)
                    {
                        return BLL.UserService.GetUserByUserId(a.ApproveMan).UserName;
                    }
                }
                else
                {
                    return "";
                }
            }
            return "";
        }

        /// <summary>
        /// 把状态转换代号为文字形式
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertState(object state)
        {
            if (state != null)
            {
                if (state.ToString() == BLL.Const.Design_ReCompile)
                {
                    return "重新编制";
                }
                else if (state.ToString() == BLL.Const.Design_Compile)
                {
                    return "变更录入";
                }
                else if (state.ToString() == BLL.Const.Design_Audit1)
                {
                    return "变更分析";
                }
                else if (state.ToString() == BLL.Const.Design_Audit2)
                {
                    return "变更分析审核";
                }
                else if (state.ToString() == BLL.Const.Design_Audit3)
                {
                    return "变更实施";
                }
                else if (state.ToString() == BLL.Const.Design_Audit4)
                {
                    return "变更实施审核";
                }
                else if (state.ToString() == BLL.Const.Design_Complete)
                {
                    return "审批完成";
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void btnRset_Click(object sender, EventArgs e)
        {
            drpUnit.SelectedIndex = 0;
            drpMainItem.SelectedIndex = 0;
            drpCNProfessional.SelectedIndex = 0;
            drpDesignType.SelectedIndex = 0;
            txtStartTime.Text = "";
            txtEndTime.Text = "";
            BindGrid();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        /// <summary>
        /// 获取单位名称
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertCarryUnit(object CarryUnitIds)
        {
            string CarryUnitName = string.Empty;
            if (CarryUnitIds != null)
            {
                string[] Ids = CarryUnitIds.ToString().Split(',');
                foreach (string t in Ids)
                {
                    var type = BLL.UnitService.GetUnitByUnitId(t);
                    if (type != null)
                    {
                        CarryUnitName += type.UnitName + ",";
                    }
                }
            }
            if (CarryUnitName != string.Empty)
            {
                return CarryUnitName.Substring(0, CarryUnitName.Length - 1);
            }
            else
            {
                return "";
            }
        }

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }


        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }
        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("DesignView.aspx?see=see&DesignId=" + Grid1.SelectedRowID, "查看 - ")));
        }
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            object[] keys = Grid1.DataKeys[e.RowIndex];
            string fileId = string.Empty;
            if (keys == null)
            {
                return;
            }
            else
            {
                fileId = keys[0].ToString();
            }

            if (e.CommandName.Equals("download"))
            {
                string menuId = Const.DesignMenuId;
                PageContext.RegisterStartupScript(Windowtt.GetShowReference(
                 String.Format("../../AttachFile/webuploader.aspx?type=-1&source=1&toKeyId={0}&path=FileUpload/Design&menuId={1}", fileId, menuId)));
            }
        }
    }
}