using Aspose.Words;
using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace FineUIPro.Web.HSSE.License
{
    public partial class RadialWork : PageBase
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

                UnitService.InitUnitDropDownList(this.drpUnit, this.ProjectId, true);
                if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
                {
                    this.drpUnit.SelectedValue = this.CurrUser.UnitId;
                    this.drpUnit.Enabled = false;
                }

                this.drpStates.DataValueField = "Value";
                this.drpStates.DataTextField = "Text";
                this.drpStates.DataSource = LicensePublicService.drpStatesItem();
                this.drpStates.DataBind();
                this.drpStates.SelectedValue = Const._Null;
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
            string strSql = "SELECT license.RadialWorkId,license.ProjectId,license.LicenseCode,license.ApplyUnitId,ApplyUnit.UnitName AS ApplyUnitName,license.ApplyManId,license.ApplyDate,license.RadialType,license.WorkPalce,license.ValidityStartTime,license.ValidityEndTime,license.WorkMeasures,license.States"
                        + @" ,(CASE WHEN license.States=0 THEN '待提交' WHEN license.States=1 THEN '审核中'  WHEN license.States=2 THEN '作业中' WHEN license.States=3 THEN '已关闭' WHEN license.States=-1 THEN '已取消' ELSE '未知' END) AS StatesName "
                        + @" FROM dbo.License_RadialWork AS license "
                        + @" LEFT JOIN Base_Unit AS ApplyUnit ON license.ApplyUnitId =ApplyUnit.UnitId"
                        + @" WHERE license.ProjectId= '" + this.ProjectId +"'";
            List<SqlParameter> listStr = new List<SqlParameter>();
           
            if (ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
            {
                strSql += " AND license.ApplyUnitId = @UnitId";  ///状态为已完成
                listStr.Add(new SqlParameter("@UnitId", this.CurrUser.UnitId));
            }       
            if (this.drpUnit.SelectedValue != Const._Null)
            {
                strSql += " AND license.ApplyUnitId = @UnitId2";
                listStr.Add(new SqlParameter("@UnitId2", this.drpUnit.SelectedValue));
            }
            if (!string.IsNullOrEmpty(this.drpStates.SelectedValue) && this.drpStates.SelectedValue != Const._Null)
            {
                strSql += " AND license.States = @States";
                listStr.Add(new SqlParameter("@States", this.drpStates.SelectedValue));
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
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// 分页下拉选择事件
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
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
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
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("RadialWorkView.aspx?RadialWorkId={0}", id, "查看 - ")));
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
                    var RadialWork = LicensePublicService.GetRadialWorkById(rowID);
                    if (RadialWork != null)
                    {
                        LogService.AddSys_Log(this.CurrUser, RadialWork.LicenseCode, RadialWork.RadialWorkId, Const.ProjectRadialWorkMenuId, Const.BtnDelete);
                        LicensePublicService.DeleteRadialWorkById(rowID);
                    }
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ProjectRadialWorkMenuId);
            if (buttonList.Count() > 0)
            {
                //if (buttonList.Contains(BLL.Const.BtnAdd))
                //{
                //    this.btnNew.Hidden = false;
                //}
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuView.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("射线作业票" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = Encoding.UTF8;
            this.Grid1.PageSize = this.Grid1.RecordCount;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion

        #region 打印
        protected void btnPrinter_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string Id = Grid1.SelectedRowID;

            string rootPath = Server.MapPath("~/");
            string initTemplatePath = string.Empty;
            string uploadfilepath = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            initTemplatePath = "File\\Word\\HSSE\\射线作业票.doc";
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".doc", string.Format("{0:yyyy-MM}", DateTime.Now) + ".doc");
            filePath = initTemplatePath.Replace(".doc", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
            File.Copy(uploadfilepath, newUrl);
            ///更新书签
            var getRadialWork = LicensePublicService.GetRadialWorkById(Id);
            Document doc = new Aspose.Words.Document(newUrl);
            Bookmark bookmarkLicenseCode = doc.Range.Bookmarks["LicenseCode"];//编号
            if (bookmarkLicenseCode != null)
            {
                if (getRadialWork != null)
                {
                    bookmarkLicenseCode.Text = getRadialWork.LicenseCode;
                }
            }
            Bookmark bookmarkApplyManName = doc.Range.Bookmarks["ApplyManName"];//申请人
            if (bookmarkApplyManName != null)
            {
                if (getRadialWork != null)
                {
                    var getUser = UserService.GetUserByUserId(getRadialWork.ApplyManId);
                    if (getUser != null)
                    {
                            bookmarkApplyManName.Text = getUser.UserName;
                    }


                }
            }
            Bookmark bookmarkUnitName = doc.Range.Bookmarks["UnitName"];//申请单位
            if (bookmarkUnitName != null)
            {
                if (getRadialWork != null)
                {
                    if (!string.IsNullOrEmpty(getRadialWork.ApplyUnitId))
                    {
                        bookmarkUnitName.Text = BLL.UnitService.GetUnitNameByUnitId(getRadialWork.ApplyUnitId);
                    }

                }
            }
            Bookmark bookmarkRadialType = doc.Range.Bookmarks["RadialType"];//作业地点
            if (bookmarkRadialType != null)
            {
                if (getRadialWork != null)
                {
                    bookmarkRadialType.Text = getRadialWork.RadialType;

                }
            }
            Bookmark bookmarkWorkLeaderTel = doc.Range.Bookmarks["WorkLeaderTel"];//作业负责人
            if (bookmarkWorkLeaderTel != null)
            {
                if (getRadialWork != null)
                {
                    var getUser = UserService.GetUserByUserId(getRadialWork.WorkLeaderId);
                    if (getUser != null)
                    {
                            bookmarkWorkLeaderTel.Text = getUser.UserName+"/"+getRadialWork.WorkLeaderTel;
                    }

                }
            }
            Bookmark bookmarkValidityDate = doc.Range.Bookmarks["ValidityDate"];//作业时间
            if (bookmarkValidityDate != null)
            {
                if (getRadialWork != null)
                {
                    if (getRadialWork.ValidityStartTime.HasValue)
                    {

                        bookmarkValidityDate.Text = getRadialWork.ValidityStartTime.Value.Year + "年" + getRadialWork.ValidityStartTime.Value.Month + "月" + getRadialWork.ValidityStartTime.Value.Day + "日" + getRadialWork.ValidityStartTime.Value.Hour + "时至";
                        if (getRadialWork.ValidityEndTime.HasValue)
                        {
                            bookmarkValidityDate.Text += getRadialWork.ValidityEndTime.Value.Year + "年" + getRadialWork.ValidityEndTime.Value.Month + "月" + getRadialWork.ValidityEndTime.Value.Day + "日" + getRadialWork.ValidityEndTime.Value.Hour + "时";
                        }
                    }
                }
            }
            Bookmark bookmarkWorkPalce = doc.Range.Bookmarks["WorkPalce"];//作业地点
            if (bookmarkWorkPalce != null)
            {
                if (getRadialWork != null)
                {
                    bookmarkWorkPalce.Text = getRadialWork.WorkPalce;

                }
            }
            Bookmark bookmarkWorkMeasures = doc.Range.Bookmarks["WorkMeasures"];//作业内容
            if (bookmarkWorkMeasures != null)
            {
                if (getRadialWork != null)
                {
                    bookmarkWorkMeasures.Text = getRadialWork.WorkMeasures;

                }
            }
            Bookmark bookmarkFireWatchManName = doc.Range.Bookmarks["FireWatchManName"];//监火人员
            if (bookmarkFireWatchManName != null)
            {
                if (getRadialWork != null)
                {
                    var getUser = UserService.GetUserByUserId(getRadialWork.FireWatchManId);
                    if (getUser != null)
                    {
                            bookmarkFireWatchManName.Text = getUser.UserName;
                    }
                }
            }
            Bookmark bookmarkWatchManContact = doc.Range.Bookmarks["WatchManContact"];//联系方式
            if (bookmarkWatchManContact != null)
            {
                if (getRadialWork != null)
                {
                    bookmarkWatchManContact.Text = getRadialWork.WatchManContact;

                }
            }


            var GetLicenseItemList = LicensePublicService.GetLicenseItemListByDataId(Id);
            if (GetLicenseItemList.Count > 0)
            {
                var item1 = GetLicenseItemList.FirstOrDefault(x => x.SortIndex == 1);
                if (item1 != null)
                {
                    if (item1.IsUsed == true)
                    {
                        Bookmark bookmarkUser1 = doc.Range.Bookmarks["User1"];//确认执行
                        if (bookmarkUser1 != null)
                        {
                            bookmarkUser1.Text = "√";
                        }
                    }
                    else
                    {
                        Bookmark bookmarkIsFit1 = doc.Range.Bookmarks["IsFit1"];
                        if (bookmarkIsFit1 != null)
                        {
                            if (item1 != null)
                            {
                                bookmarkIsFit1.Text = "×";
                            }
                        }
                    }
                }



                var item2 = GetLicenseItemList.FirstOrDefault(x => x.SortIndex == 2);
                if (item2 != null)
                {
                    if (item2.IsUsed == true)
                    {
                        Bookmark bookmarkUser2 = doc.Range.Bookmarks["User2"];//确认执行
                        if (bookmarkUser2 != null)
                        {
                            bookmarkUser2.Text = "√";
                        }
                    }
                    else
                    {
                        Bookmark bookmarkIsFit2 = doc.Range.Bookmarks["IsFit2"];
                        if (bookmarkIsFit2 != null)
                        {
                            if (item2 != null)
                            {
                                bookmarkIsFit2.Text = "×";
                            }
                        }
                    }
                }
                var item3 = GetLicenseItemList.FirstOrDefault(x => x.SortIndex == 3);
                if (item3 != null)
                {
                    if (item3.IsUsed == true)
                    {
                        Bookmark bookmarkUser3 = doc.Range.Bookmarks["User3"];//确认执行
                        if (bookmarkUser3 != null)
                        {
                            bookmarkUser3.Text = "√";
                        }
                    }
                    else
                    {
                        Bookmark bookmarkIsFit3 = doc.Range.Bookmarks["IsFit3"];
                        if (bookmarkIsFit3 != null)
                        {
                            if (item3 != null)
                            {
                                bookmarkIsFit3.Text = "×";
                            }
                        }
                    }
                }

                var item4 = GetLicenseItemList.FirstOrDefault(x => x.SortIndex == 4);
                if (item4 != null)
                {
                    if (item4.IsUsed == true)
                    {
                        Bookmark bookmarkUser4 = doc.Range.Bookmarks["User4"];//确认执行
                        if (bookmarkUser4 != null)
                        {
                            bookmarkUser4.Text = "√";
                        }
                    }
                    else
                    {
                        Bookmark bookmarkIsFit4 = doc.Range.Bookmarks["IsFit4"];
                        if (bookmarkIsFit4 != null)
                        {
                            if (item4 != null)
                            {
                                bookmarkIsFit4.Text = "×";
                            }
                        }
                    }
                }
                var item5 = GetLicenseItemList.FirstOrDefault(x => x.SortIndex == 5);
                if (item5 != null)
                {
                    if (item5.IsUsed == true)
                    {
                        Bookmark bookmarkUser5 = doc.Range.Bookmarks["User5"];//确认执行
                        if (bookmarkUser5 != null)
                        {
                            bookmarkUser5.Text = "√";
                        }
                    }
                    else
                    {
                        Bookmark bookmarkIsFit5 = doc.Range.Bookmarks["IsFit5"];
                        if (bookmarkIsFit5 != null)
                        {
                            if (item5 != null)
                            {
                                bookmarkIsFit5.Text = "×";
                            }
                        }
                    }
                }

                var item6 = GetLicenseItemList.FirstOrDefault(x => x.SortIndex == 6);
                if (item6 != null)
                {
                    if (item6.IsUsed == true)
                    {
                        Bookmark bookmarkUser6 = doc.Range.Bookmarks["User6"];//确认执行
                        if (bookmarkUser6 != null)
                        {
                            bookmarkUser6.Text = "√";
                        }
                    }
                    else
                    {
                        Bookmark bookmarkIsFit6 = doc.Range.Bookmarks["IsFit6"];
                        if (bookmarkIsFit6 != null)
                        {
                            if (item6 != null)
                            {
                                bookmarkIsFit6.Text = "×";
                            }
                        }
                    }
                }
                var item7 = GetLicenseItemList.FirstOrDefault(x => x.SortIndex == 7);
                if (item7 != null)
                {
                    if (item7.IsUsed == true)
                    {
                        Bookmark bookmarkUser7 = doc.Range.Bookmarks["User7"];//确认执行
                        if (bookmarkUser7 != null)
                        {
                            bookmarkUser7.Text = "√";
                        }
                    }
                    else
                    {
                        Bookmark bookmarkIsFit7 = doc.Range.Bookmarks["IsFit7"];
                        if (bookmarkIsFit7 != null)
                        {
                            if (item7 != null)
                            {
                                bookmarkIsFit7.Text = "×";
                            }
                        }
                    }
                }

                var item8 = GetLicenseItemList.FirstOrDefault(x => x.SortIndex == 8);
                if (item8 != null)
                {
                    if (item8.IsUsed == true)
                    {
                        Bookmark bookmarkUser8 = doc.Range.Bookmarks["User8"];//确认执行
                        if (bookmarkUser8 != null)
                        {
                            bookmarkUser8.Text = "√";
                        }
                    }
                    else
                    {
                        Bookmark bookmarkIsFit8 = doc.Range.Bookmarks["IsFit8"];
                        if (bookmarkIsFit8 != null)
                        {
                            if (item8 != null)
                            {
                                bookmarkIsFit8.Text = "×";
                            }
                        }
                    }
                }

                var item9 = GetLicenseItemList.FirstOrDefault(x => x.SortIndex == 9);
                if (item9 != null)
                {
                    if (item9.IsUsed == true)
                    {
                        Bookmark bookmarkUser9 = doc.Range.Bookmarks["User9"];//确认执行
                        if (bookmarkUser9 != null)
                        {
                            bookmarkUser9.Text = "√";
                        }
                    }
                    else
                    {
                        Bookmark bookmarkIsFit9 = doc.Range.Bookmarks["IsFit9"];
                        if (bookmarkIsFit9 != null)
                        {
                            if (item9 != null)
                            {
                                bookmarkIsFit9.Text = "×";
                            }
                        }
                    }
                }

                var item10 = GetLicenseItemList.FirstOrDefault(x => x.SortIndex == 10);
                if (item10 != null)
                {
                    if (item10.IsUsed == true)
                    {
                        Bookmark bookmarkUser10 = doc.Range.Bookmarks["User10"];//确认执行
                        if (bookmarkUser10 != null)
                        {
                            bookmarkUser10.Text = "√";
                        }
                    }
                    else
                    {
                        Bookmark bookmarkIsFit10 = doc.Range.Bookmarks["IsFit10"];
                        if (bookmarkIsFit10 != null)
                        {
                            if (item10 != null)
                            {
                                bookmarkIsFit10.Text = "×";
                            }
                        }
                    }
                }
                var item11 = GetLicenseItemList.FirstOrDefault(x => x.SortIndex == 11);
                if (item11 != null)
                {
                    if (item11.IsUsed == true)
                    {
                        Bookmark bookmarkUser11 = doc.Range.Bookmarks["User11"];//确认执行
                        if (bookmarkUser11 != null)
                        {
                            bookmarkUser11.Text = "√";
                        }
                    }
                    else
                    {
                        Bookmark bookmarkIsFit11 = doc.Range.Bookmarks["IsFit11"];
                        if (bookmarkIsFit11 != null)
                        {
                            if (item11 != null)
                            {
                                bookmarkIsFit11.Text = "×";
                            }
                        }
                    }
                }

                var item12 = GetLicenseItemList.FirstOrDefault(x => x.SortIndex == 12);
                if (item12 != null)
                {
                    if (item12.IsUsed == true)
                    {
                        Bookmark bookmarkUser12 = doc.Range.Bookmarks["User12"];//确认执行
                        if (bookmarkUser12 != null)
                        {
                            bookmarkUser12.Text = "√";
                        }
                    }
                    else
                    {
                        Bookmark bookmarkIsFit12 = doc.Range.Bookmarks["IsFit12"];
                        if (bookmarkIsFit12 != null)
                        {
                            if (item12 != null)
                            {
                                bookmarkIsFit12.Text = "×";
                            }
                        }
                    }
                }


            }
            //审核记录
            var getFlows = LicensePublicService.GetFlowOperateListByDataId(Id);
            if (getFlows.Count() > 0)
            {
                var getF1 = getFlows.FirstOrDefault(x => x.SortIndex == 1);
                if (getF1 != null)
                {
                    var getUser = UserService.GetUserByUserId(getF1.OperaterId);
                    Bookmark bookmarkOpinion1 = doc.Range.Bookmarks["Opinion1"];
                    if (bookmarkOpinion1 != null)
                    {


                        if (getUser != null)
                        {
                            if (getF1.IsAgree == true)
                            {
                                bookmarkOpinion1.Text = getF1.Opinion;
                            }
                            else
                            {
                                bookmarkOpinion1.Text = "不同意:   " + getF1.Opinion;
                            }
                        }


                    }
                    Bookmark bookmarkOperaterMan1 = doc.Range.Bookmarks["OperaterMan1"];
                    if (bookmarkOperaterMan1 != null)
                    {
                        if (getF1.OperaterTime.HasValue)
                        {
                            if (getUser != null)
                            {
                                DocumentBuilder builders = new DocumentBuilder(doc);
                                if (!string.IsNullOrEmpty(getUser.SignatureUrl) && File.Exists(rootPath + getUser.SignatureUrl))
                                {
                                    var file = rootPath + getUser.SignatureUrl;
                                    builders.MoveToBookmark("OperaterMan1");
                                    builders.InsertImage(file, 80, 20);
                                }
                                else
                                {
                                    bookmarkOperaterMan1.Text = getUser.UserName;
                                }
                            }
                        }
                    }
                    Bookmark bookmarkOperaterTime1 = doc.Range.Bookmarks["OperaterTime1"];
                    if (bookmarkOperaterTime1 != null)
                    {
                        if (getF1.OperaterTime.HasValue)
                        {
                            bookmarkOperaterTime1.Text = string.Format("{0:yyyy-MM-dd HH:mm}", getF1.OperaterTime);
                        }
                    }
                }
                var getF2 = getFlows.FirstOrDefault(x => x.SortIndex == 2);
                if (getF2 != null)
                {
                    var getUser = UserService.GetUserByUserId(getF2.OperaterId);
                    Bookmark bookmarkOpinion2 = doc.Range.Bookmarks["Opinion2"];
                    if (bookmarkOpinion2 != null)
                    {


                        if (getUser != null)
                        {
                            if (getF2.IsAgree == true)
                            {
                                bookmarkOpinion2.Text = getF2.Opinion;
                            }
                            else
                            {
                                bookmarkOpinion2.Text = "不同意:   " + getF2.Opinion;
                            }
                        }


                    }
                    Bookmark bookmarkOperaterMan2 = doc.Range.Bookmarks["OperaterMan2"];
                    if (bookmarkOperaterMan2 != null)
                    {
                        if (getF2.OperaterTime.HasValue)
                        {
                            if (getUser != null)
                            {
                                DocumentBuilder builders = new DocumentBuilder(doc);
                                if (!string.IsNullOrEmpty(getUser.SignatureUrl) && File.Exists(rootPath + getUser.SignatureUrl))
                                {
                                    var file = rootPath + getUser.SignatureUrl;
                                    builders.MoveToBookmark("OperaterMan2");
                                    builders.InsertImage(file, 80, 20);
                                }
                                else
                                {
                                    bookmarkOperaterMan2.Text = getUser.UserName;
                                }
                            }
                        }
                    }
                    Bookmark bookmarkOperaterTime2 = doc.Range.Bookmarks["OperaterTime2"];
                    if (bookmarkOperaterTime2 != null)
                    {
                        if (getF2.OperaterTime.HasValue)
                        {
                            bookmarkOperaterTime2.Text = string.Format("{0:yyyy-MM-dd HH:mm}", getF2.OperaterTime);
                        }
                    }
                }
                var getF3 = getFlows.FirstOrDefault(x => x.SortIndex == 3);
                if (getF3 != null)
                {
                    var getUser = UserService.GetUserByUserId(getF3.OperaterId);
                    Bookmark bookmarkOpinion3 = doc.Range.Bookmarks["Opinion3"];
                    if (bookmarkOpinion3 != null)
                    {


                        if (getUser != null)
                        {
                            if (getF3.IsAgree == true)
                            {
                                bookmarkOpinion3.Text = getF3.Opinion;
                            }
                            else
                            {
                                bookmarkOpinion3.Text = "不同意:   " + getF3.Opinion;
                            }
                        }


                    }
                    Bookmark bookmarkOperaterMan3 = doc.Range.Bookmarks["OperaterMan3"];
                    if (bookmarkOperaterMan3 != null)
                    {
                        if (getF3.OperaterTime.HasValue)
                        {
                            if (getUser != null)
                            {
                                DocumentBuilder builders = new DocumentBuilder(doc);
                                if (!string.IsNullOrEmpty(getUser.SignatureUrl) && File.Exists(rootPath + getUser.SignatureUrl))
                                {
                                    var file = rootPath + getUser.SignatureUrl;
                                    builders.MoveToBookmark("OperaterMan3");
                                    builders.InsertImage(file, 80, 20);
                                }
                                else
                                {
                                    bookmarkOperaterMan3.Text = getUser.UserName;
                                }
                            }
                        }
                    }
                    Bookmark bookmarkOperaterTime3 = doc.Range.Bookmarks["OperaterTime3"];
                    if (bookmarkOperaterTime3 != null)
                    {
                        if (getF3.OperaterTime.HasValue)
                        {
                            bookmarkOperaterTime3.Text = string.Format("{0:yyyy-MM-dd HH:mm}", getF3.OperaterTime);
                        }
                    }
                }
                var getF4 = getFlows.FirstOrDefault(x => x.SortIndex == 4);
                if (getF4 != null)
                {
                    var getUser = UserService.GetUserByUserId(getF4.OperaterId);
                    Bookmark bookmarkOpinion4 = doc.Range.Bookmarks["Opinion4"];
                    if (bookmarkOpinion4 != null)
                    {


                        if (getUser != null)
                        {
                            if (getF4.IsAgree == true)
                            {
                                bookmarkOpinion4.Text = getF4.Opinion;
                            }
                            else
                            {
                                bookmarkOpinion4.Text = "不同意:   " + getF4.Opinion;
                            }
                        }


                    }
                    Bookmark bookmarkOperaterMan4 = doc.Range.Bookmarks["OperaterMan4"];
                    if (bookmarkOperaterMan4 != null)
                    {
                        if (getF4.OperaterTime.HasValue)
                        {
                            if (getUser != null)
                            {
                                DocumentBuilder builders = new DocumentBuilder(doc);
                                if (!string.IsNullOrEmpty(getUser.SignatureUrl) && File.Exists(rootPath + getUser.SignatureUrl))
                                {
                                    var file = rootPath + getUser.SignatureUrl;
                                    builders.MoveToBookmark("OperaterMan4");
                                    builders.InsertImage(file, 80, 20);
                                }
                                else
                                {
                                    bookmarkOperaterMan4.Text = getUser.UserName;
                                }
                            }
                        }
                    }
                    Bookmark bookmarkOperaterTime4 = doc.Range.Bookmarks["OperaterTime4"];
                    if (bookmarkOperaterTime4 != null)
                    {
                        if (getF4.OperaterTime.HasValue)
                        {
                            bookmarkOperaterTime4.Text = string.Format("{0:yyyy-MM-dd HH:mm}", getF4.OperaterTime);
                        }
                    }
                }
                var getF5 = getFlows.FirstOrDefault(x => x.SortIndex == 5);
                if (getF5 != null)
                {
                    var getUser = UserService.GetUserByUserId(getF5.OperaterId);
                    Bookmark bookmarkOpinion5 = doc.Range.Bookmarks["Opinion5"];
                    if (bookmarkOpinion5 != null)
                    {


                        if (getUser != null)
                        {
                            if (getF5.IsAgree == true)
                            {
                                bookmarkOpinion5.Text = getF5.Opinion;
                            }
                            else
                            {
                                bookmarkOpinion5.Text = "不同意:   " + getF5.Opinion;
                            }
                        }


                    }
                    Bookmark bookmarkOperaterMan5 = doc.Range.Bookmarks["OperaterMan5"];
                    if (bookmarkOperaterMan5 != null)
                    {
                        if (getF5.OperaterTime.HasValue)
                        {
                            if (getUser != null)
                            {
                                DocumentBuilder builders = new DocumentBuilder(doc);
                                if (!string.IsNullOrEmpty(getUser.SignatureUrl) && File.Exists(rootPath + getUser.SignatureUrl))
                                {
                                    var file = rootPath + getUser.SignatureUrl;
                                    builders.MoveToBookmark("OperaterMan5");
                                    builders.InsertImage(file, 80, 20);
                                }
                                else
                                {
                                    bookmarkOperaterMan5.Text = getUser.UserName;
                                }
                            }
                        }
                    }
                    Bookmark bookmarkOperaterTime5 = doc.Range.Bookmarks["OperaterTime5"];
                    if (bookmarkOperaterTime5 != null)
                    {
                        if (getF5.OperaterTime.HasValue)
                        {
                            bookmarkOperaterTime5.Text = string.Format("{0:yyyy-MM-dd HH:mm}", getF5.OperaterTime);
                        }
                    }
                }
                var getF6 = getFlows.FirstOrDefault(x => x.SortIndex == 6);
                if (getF6 != null)
                {
                    var getUser = UserService.GetUserByUserId(getF6.OperaterId);
                    Bookmark bookmarkOpinion6 = doc.Range.Bookmarks["Opinion6"];
                    if (bookmarkOpinion6 != null)
                    {


                        if (getUser != null)
                        {
                            if (getF6.IsAgree == true)
                            {
                                bookmarkOpinion6.Text = getF6.Opinion;
                            }
                            else
                            {
                                bookmarkOpinion6.Text = "不同意:   " + getF6.Opinion;
                            }
                        }


                    }
                    Bookmark bookmarkOperaterMan6 = doc.Range.Bookmarks["OperaterMan6"];
                    if (bookmarkOperaterMan6 != null)
                    {
                        if (getF6.OperaterTime.HasValue)
                        {
                            if (getUser != null)
                            {
                                DocumentBuilder builders = new DocumentBuilder(doc);
                                if (!string.IsNullOrEmpty(getUser.SignatureUrl) && File.Exists(rootPath + getUser.SignatureUrl))
                                {
                                    var file = rootPath + getUser.SignatureUrl;
                                    builders.MoveToBookmark("OperaterMan6");
                                    builders.InsertImage(file, 80, 20);
                                }
                                else
                                {
                                    bookmarkOperaterMan6.Text = getUser.UserName;
                                }
                            }
                        }
                    }
                    Bookmark bookmarkOperaterTime6 = doc.Range.Bookmarks["OperaterTime6"];
                    if (bookmarkOperaterTime6 != null)
                    {
                        if (getF6.OperaterTime.HasValue)
                        {
                            bookmarkOperaterTime6.Text = string.Format("{0:yyyy-MM-dd HH:mm}", getF6.OperaterTime);
                        }
                    }
                }
            }

            Bookmark bookmarkCance = doc.Range.Bookmarks["Cancel"];//取消
            if (bookmarkCance != null)
            {
                if (getRadialWork != null)
                {
                    if (!string.IsNullOrEmpty(getRadialWork.CancelManId))
                    {
                        var getUser = UserService.GetUserByUserId(getRadialWork.CancelManId);
                        if (getUser != null)
                        {
                            if (!string.IsNullOrEmpty(getUser.SignatureUrl) && File.Exists(rootPath + getUser.SignatureUrl))
                            {
                                var file = rootPath + getUser.SignatureUrl;
                                DocumentBuilder builders = new DocumentBuilder(doc);
                                builders.MoveToBookmark("Cancel");
                                builders.InsertImage(file, 80, 20);
                                builders.Write("取消原因：" + getRadialWork.CancelReasons + "  取消时间：" + string.Format("{0:yyyy-MM-dd HH:mm}", getRadialWork.CancelTime));
                            }
                            else
                            {
                                bookmarkCance.Text = getUser.UserName + "  取消原因：" + getRadialWork.CancelReasons + "  取消时间：" + string.Format("{0:yyyy-MM-dd HH:mm}", getRadialWork.CancelTime);
                            }
                        }
                    }
                }
            }

            Bookmark bookmarkClose = doc.Range.Bookmarks["Close"];//关闭
            if (bookmarkClose != null)
            {
                if (getRadialWork != null)
                {
                    if (!string.IsNullOrEmpty(getRadialWork.CloseManId))
                    {
                        var getUser = UserService.GetUserByUserId(getRadialWork.CloseManId);
                        if (getUser != null)
                        {
                            if (!string.IsNullOrEmpty(getRadialWork.CloseReasons))
                            {
                                bookmarkClose.Text = getRadialWork.CloseReasons + " 关闭时间："
                                 + string.Format("{0:yyyy-MM-dd HH:mm}", getRadialWork.CloseTime) + "。";
                            }
                            else if (!string.IsNullOrEmpty(getUser.SignatureUrl) && File.Exists(rootPath + getUser.SignatureUrl))
                            {
                                var file = rootPath + getUser.SignatureUrl;
                                DocumentBuilder builders = new DocumentBuilder(doc);
                                builders.MoveToBookmark("Close");
                                builders.InsertImage(file, 80, 20);
                                builders.Write("关闭时间：" + string.Format("{0:yyyy-MM-dd HH:mm}", getRadialWork.CloseTime));
                            }
                            else
                            {
                                bookmarkClose.Text = getUser.UserName + "  关闭时间：" + string.Format("{0:yyyy-MM-dd HH:mm}", getRadialWork.CloseTime);
                            }
                        }
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