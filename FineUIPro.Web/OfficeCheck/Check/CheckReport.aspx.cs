using BLL;
using Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.OfficeCheck.Check
{
    public partial class CheckReport : PageBase
    {
        #region 定义项
        /// <summary>
        /// 检查主键
        /// </summary>
        public string CheckNoticeId
        {
            get
            {
                return (string)ViewState["CheckNoticeId"];
            }
            set
            {
                ViewState["CheckNoticeId"] = value;
            }
        }

        /// <summary>
        /// 定义集合
        /// </summary>
        private static List<Model.ProjectSupervision_CheckReportItem> checkReportItems = new List<Model.ProjectSupervision_CheckReportItem>();
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
                string type = Request.Params["type"];
                if (type!=null)
                {
                    this.btnSave.Hidden = true;
                    this.btnAdd.Hidden = true;
                    this.btnRefresh.Hidden = true;
                    this.Grid2.Columns[4].Hidden = true;
                }
                this.CheckNoticeId = Request.Params["CheckNoticeId"];
                if (!string.IsNullOrEmpty(this.CheckNoticeId))
                {
                    var checkReport = BLL.CheckReportService.GetCheckReportByCheckNoticeId(this.CheckNoticeId);
                    if (checkReport != null)
                    {
                        this.hdCheckReportId.Text = checkReport.CheckReportId;
                        this.txtValues1.Text = checkReport.CheckPurpose;
                        this.txtValues2.Text = checkReport.Basis;
                        this.txtValues3.Text = checkReport.BasicInfo;
                        this.txtValues4.Text = checkReport.ConformItem;
                        this.txtValues6.Text = checkReport.Opinion;
                        this.txtValues7.Text = checkReport.CheckResult;
                        this.BindGrid2();
                    }
                    this.BindGrid();
                }
                checkReportItems.Clear();
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"select checkTeam.CheckTeamId, 
                                     checkTeam.CheckNoticeId, 
                                     checkTeam.UserId, 
                                     checkTeam.SortIndex, 
                                     checkTeam.PostName, 
                                     checkTeam.WorkTitle, 
                                     checkTeam.CheckPostName, 
                                     checkTeam.CheckDate, 
                                     checkTeam.UserName, 
                                     checkTeam.UnitId, 
                                     checkTeam.SexName,
                                     unit.UnitName"
                        + @" from ProjectSupervision_CheckTeam as checkTeam "
                        + @" left join Base_Unit as unit on unit.UnitId = checkTeam.UnitId where 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND checkTeam.CheckNoticeId = @CheckNoticeId";
            listStr.Add(new SqlParameter("@CheckNoticeId", this.CheckNoticeId));
            strSql += " ORDER BY checkTeam.SortIndex";
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region 排序
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 不符合项数据绑定
        /// <summary>
        /// 不符合项数据绑定
        /// </summary>
        private void BindGrid2()
        {
            string strSql = @"select CheckReportItemId, CheckReportId, CheckReportCode, UnConformItem from ProjectSupervision_CheckReportItem ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += "where CheckReportId = @CheckReportId";
            listStr.Add(new SqlParameter("@CheckReportId", this.hdCheckReportId.Text));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            var table = this.GetPagedDataTable(Grid2, tb);
            Grid2.DataSource = table;
            Grid2.DataBind();
        }
        #endregion

        #region 刷新
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            //BLL.CheckInfo_Table8Service.SetTeam(this.CheckInfoId);
            BindGrid();
        }
        #endregion

        #region 保存按钮事件
        /// <summary>
        ///  保存按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.ProjectSupervision_CheckReport newCheckReport = new Model.ProjectSupervision_CheckReport();
            newCheckReport.CheckPurpose = this.txtValues1.Text.Trim();
            newCheckReport.Basis = this.txtValues2.Text.Trim();
            newCheckReport.BasicInfo = this.txtValues3.Text.Trim();
            newCheckReport.ConformItem = this.txtValues4.Text.Trim();
            newCheckReport.Opinion = this.txtValues6.Text.Trim();
            newCheckReport.CheckResult = this.txtValues7.Text.Trim();
            if (!string.IsNullOrEmpty(hdCheckReportId.Text.Trim()))
            {
                newCheckReport.CheckReportId = this.hdCheckReportId.Text.Trim();
                BLL.CheckReportService.UpdateCheckReport(newCheckReport);
            }
            else
            {
                newCheckReport.CheckNoticeId = this.CheckNoticeId;
                newCheckReport.CheckReportId = SQLHelper.GetNewID(typeof(Model.ProjectSupervision_CheckReport));
                BLL.CheckReportService.AddCheckReport(newCheckReport);
                this.hdCheckReportId.Text = newCheckReport.CheckReportId;
            }
            saveItem();
            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        /// <summary>
        /// 保存明细
        /// </summary>
        public void saveItem()
        {
            var data = Grid2.GetMergedData();
            if (data != null)
            {
                foreach (JObject mergedRow in Grid2.GetMergedData())
                {
                    int i = mergedRow.Value<int>("index");
                    JObject values = mergedRow.Value<JObject>("values");
                    string checkReportItemId = values.Value<string>("CheckReportItemId");
                    string checkReportCode = values.Value<string>("CheckReportCode");
                    string unConformItem = values.Value<string>("UnConformItem");
                    Model.ProjectSupervision_CheckReportItem checkReportItem = Funs.DB.ProjectSupervision_CheckReportItem.FirstOrDefault(e => e.CheckReportItemId == checkReportItemId);
                    if (checkReportItem != null)
                    {
                        checkReportItem.CheckReportItemId =checkReportItemId;
                        checkReportItem.CheckReportId = this.hdCheckReportId.Text.Trim();
                        checkReportItem.CheckReportCode = checkReportCode;
                        checkReportItem.UnConformItem = unConformItem;
                        Funs.DB.SubmitChanges();
                    }
                    else
                    {

                        var item = new ProjectSupervision_CheckReportItem();
                        item.CheckReportItemId = checkReportItemId;
                        item.CheckReportId = this.hdCheckReportId.Text.Trim();
                        item.CheckReportCode = checkReportCode;
                        item.UnConformItem = unConformItem;
                        Funs.DB.ProjectSupervision_CheckReportItem.InsertOnSubmit(item);
                        Funs.DB.SubmitChanges();
                    }
                }
            }
        }
        #endregion

        #region 不符合项行点击事件
        protected void Grid2_RowCommand(object sender, GridCommandEventArgs e)
        {
            string itemId = Grid2.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "AttachUrl")
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Rectify&menuId={1}&type=0&strParam=1", itemId, BLL.Const.CheckInfoMenuId)));
            }
            if (e.CommandName == "delete")
            {
                checkReportItems.Remove(checkReportItems.FirstOrDefault(p => p.CheckReportItemId == itemId));
                Grid2.DataSource = checkReportItems;
                Grid2.DataBind();
            }
        }
        #endregion

        #region 不符合项行绑定事件
        protected void Grid2_RowDataBound(object sender, GridRowEventArgs e)
        {

        }
        #endregion

        #region 增加
        /// <summary>
        /// 增加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            addItem();
            Model.ProjectSupervision_CheckReportItem checkReportItem = new Model.ProjectSupervision_CheckReportItem();
            checkReportItem.CheckReportItemId = SQLHelper.GetNewID(typeof(Model.ProjectSupervision_CheckReportItem));
            checkReportItems.Add(checkReportItem);
            //将gd数据保存在list中
            Grid2.DataSource = checkReportItems;
            Grid2.DataBind();
        }

        private void addItem()
        {
            checkReportItems.Clear();
            var data = Grid2.GetMergedData();
            if (data != null)
            {
                foreach (JObject mergedRow in Grid2.GetMergedData())
                {
                    int i = mergedRow.Value<int>("index");
                    JObject values = mergedRow.Value<JObject>("values");
                    string checkReportItemId = values.Value<string>("CheckReportItemId");
                    string checkReportCode = values.Value<string>("CheckReportCode");
                    string unConformItem = values.Value<string>("UnConformItem");
                    var item = new ProjectSupervision_CheckReportItem();
                    item.CheckReportItemId = checkReportItemId;
                    item.CheckReportId = this.hdCheckReportId.Text.Trim();
                    item.CheckReportCode = checkReportCode;
                    item.UnConformItem = unConformItem;
                    checkReportItems.Add(item);
                }
            }
        }
        #endregion
    }
}