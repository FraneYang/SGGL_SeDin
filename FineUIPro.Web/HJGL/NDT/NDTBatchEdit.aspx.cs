using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BLL;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace FineUIPro.Web.HJGL.NDT
{
    public partial class NDTBatchEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 委托单主键
        /// </summary>
        public string TrustBatchId
        {
            get
            {
                return (string)ViewState["TrustBatchId"];
            }
            set
            {
                ViewState["TrustBatchId"] = value;
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
                this.TrustBatchId = Request.Params["TrustBatchId"];
                ///评定级别
                ListItem[] list = new ListItem[5];
                list[0] = new ListItem("Ⅰ", "Ⅰ");
                list[1] = new ListItem("Ⅱ", "Ⅱ");
                list[2] = new ListItem("Ⅲ", "Ⅲ");
                list[3] = new ListItem("Ⅳ", "Ⅳ");
                list[4] = new ListItem("Ⅴ", "Ⅴ");
                this.drpJudgeGrade.DataTextField = "Text";
                this.drpJudgeGrade.DataValueField = "Value";
                this.drpJudgeGrade.DataSource = list;
                this.drpJudgeGrade.DataBind();

                BLL.Base_DefectService.InitDefectDropDownList(this.drpCheckDefects, false, string.Empty);

                this.PageInfoLoad(); // 加载页面 
            }
            else
            {
                if (GetRequestEventArgument() == "UPDATEDate")
                {
                    // 页面要求批量更新日期
                    UpdateDate();
                }
            }
        }
        #endregion

        #region 批量更新日期
        /// <summary>
        /// 批量更新日期
        /// </summary>
        private void UpdateDate()
        {
            if (this.Grid1.Rows.Count > 0)
            {
                string date = string.Empty, date2 = string.Empty;
                DateTime? d1 = null, d2 = null;
                if (!string.IsNullOrEmpty(this.changeFilmDate.Text))
                {
                    date = this.changeFilmDate.Text.Substring(4, 11);
                    d1 = Convert.ToDateTime(date);
                }
                if (!string.IsNullOrEmpty(this.changeReportDate.Text))
                {
                    date2 = this.changeReportDate.Text.Substring(4, 11);
                    d2 = Convert.ToDateTime(date2);
                }
                bool isNew = true;    //是否新记录，如果是已保存记录则为false
                for (int i = 0; i < this.Grid1.Rows.Count; i++)
                {
                    string rowId = this.Grid1.Rows[i].DataKeys[0].ToString();
                    if (this.changeId.Text == rowId)   //操作的行
                    {
                        Model.HJGL_Batch_NDEItem item = BLL.Batch_NDEItemService.GetNDEItemById(rowId);
                        if (item != null)
                        {
                            isNew = false;
                            break;
                        }
                    }
                }
                for (int i = 0; i < this.Grid1.Rows.Count; i++)
                {
                    string rowId = this.Grid1.Rows[i].DataKeys[0].ToString();
                    CheckBoxField ckbIsSelected = (CheckBoxField)Grid1.FindColumn("ckbIsSelected");
                    bool b = ckbIsSelected.GetCheckedState(i);
                    if (b)   //选中行
                    {
                        if (isNew)    //操作的是未保存新记录，批量更新新记录
                        {
                            Model.HJGL_Batch_NDEItem item = BLL.Batch_NDEItemService.GetNDEItemById(rowId);
                            if (item == null)
                            {
                                if (this.ckAllFilmDate.Checked)
                                {
                                    if (d1 != null)
                                    {
                                        this.Grid1.Rows[i].Values[6] = string.Format("{0:yyyy-MM-dd}", d1);
                                    }
                                }
                                if (this.ckAllReportDate.Checked)
                                {
                                    if (d2 != null)
                                    {
                                        this.Grid1.Rows[i].Values[7] = string.Format("{0:yyyy-MM-dd}", d2);
                                    }
                                }
                            }
                        }
                        else  //操作的是已保存记录，则只更新操作的这条记录
                        {
                            if (this.Grid1.Rows[i].Values[19].ToString() == rowId)   //操作的行
                            {
                                if (d1 != null)
                                {
                                    this.Grid1.Rows[i].Values[6] = string.Format("{0:yyyy-MM-dd}", d1);
                                }
                                if (d2 != null)
                                {
                                    this.Grid1.Rows[i].Values[7] = string.Format("{0:yyyy-MM-dd}", d2);
                                }
                            }
                        }
                    }
                }
                this.Grid1.CommitChanges();
                this.changeFilmDate.Text = string.Empty;
                this.changeReportDate.Text = string.Empty;
                this.changeId.Text = string.Empty;
            }
        }
        #endregion

        #region 加载页面输入提交信息
        /// <summary>
        /// 加载页面输入提交信息
        /// </summary>
        private void PageInfoLoad()
        {
            var check = BLL.Batch_NDEService.GetNDEViewByTrustBatchId(this.TrustBatchId);
            if (check != null)
            {
                this.txtNDECode.Text = check.NDECode;
                this.txtUnit.Text = check.UnitName;
                this.txtUnitWork.Text = check.UnitWorkName;
                this.txtNDEUnit.Text = check.NDEUnitName;
                this.txtDetectionType.Text = check.DetectionTypeCode;
                this.hdDetectionType.Text = check.DetectionTypeId;
                this.txtTrustCode.Text = check.TrustBatchCode;
                List<Model.View_Batch_NDEItem> GetNDEItem = BLL.Batch_NDEItemService.GetViewNDEItem(check.NDEID);
                List<string> trustBatchItemIds = (from x in GetNDEItem
                                                  select x.TrustBatchItemId).ToList();
                var batchTrustItems = BLL.Batch_BatchTrustItemService.GetViewBatchTrustItem(this.TrustBatchId);
                foreach (var batchTrustItem in batchTrustItems)
                {
                    if (!trustBatchItemIds.Contains(batchTrustItem.TrustBatchItemId))
                    {
                        Model.View_Batch_NDEItem nDEItem = new Model.View_Batch_NDEItem();
                        nDEItem.NDEItemID = BLL.SQLHelper.GetNewID(typeof(Model.HJGL_Batch_NDEItem));
                        nDEItem.PipelineCode = batchTrustItem.PipelineCode;
                        nDEItem.WeldJointCode = batchTrustItem.WeldJointCode;
                        nDEItem.UnitWorkCode = batchTrustItem.UnitWorkCode;
                        nDEItem.WelderCode = batchTrustItem.WelderCode;
                        nDEItem.NDEReportNo = batchTrustItem.TrustBatchCode;
                        nDEItem.TrustBatchItemId = batchTrustItem.TrustBatchItemId;
                        GetNDEItem.Add(nDEItem);
                    }
                }
                this.BindGrid(GetNDEItem);  // 初始化页面 
            }
            else
            {
                var trust = BLL.Batch_BatchTrustService.GetBatchTrustViewById(this.TrustBatchId);
                if (trust != null)
                {
                    if (trust.TrustType == "R")  //返修委托
                    {
                        this.txtNDECode.Text = trust.TrustBatchCode.Replace("-FXWT-", "-FXJC-");
                    }
                    else
                    {
                        this.txtNDECode.Text = trust.TrustBatchCode.Replace("-WT-", "-JC-");
                    }
                    this.txtUnit.Text = trust.UnitName;
                    this.txtUnitWork.Text = trust.UnitWorkName;
                    Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(trust.NDEUnit);
                    if (unit != null)
                    {
                        this.txtNDEUnit.Text = unit.UnitName;
                    }
                    this.txtDetectionType.Text = trust.DetectionTypeCode;
                    this.hdDetectionType.Text = trust.DetectionTypeId;
                    this.txtTrustCode.Text = trust.TrustBatchCode;
                    List<Model.View_Batch_NDEItem> GetNDEItem = new List<Model.View_Batch_NDEItem>();
                    var batchTrustItems = BLL.Batch_BatchTrustItemService.GetViewBatchTrustItem(this.TrustBatchId);
                    foreach (var batchTrustItem in batchTrustItems)
                    {
                        Model.View_Batch_NDEItem nDEItem = new Model.View_Batch_NDEItem();
                        nDEItem.NDEItemID = BLL.SQLHelper.GetNewID(typeof(Model.HJGL_Batch_NDEItem));
                        nDEItem.PipelineCode = batchTrustItem.PipelineCode;
                        nDEItem.WeldJointCode = batchTrustItem.WeldJointCode;
                        nDEItem.UnitWorkCode = batchTrustItem.UnitWorkCode;
                        nDEItem.WelderCode = batchTrustItem.WelderCode;
                        if (batchTrustItem.TrustType == "2")
                        {
                            nDEItem.NDEReportNo = batchTrustItem.TrustBatchCode + "K1";
                        }
                        else if (batchTrustItem.TrustType == "3")
                        {
                            nDEItem.NDEReportNo = batchTrustItem.TrustBatchCode + "A";
                        }
                        else
                        {
                            nDEItem.NDEReportNo = batchTrustItem.TrustBatchCode.Replace("-WT-", "-JC-");
                        }

                        nDEItem.TrustBatchItemId = batchTrustItem.TrustBatchItemId;
                        GetNDEItem.Add(nDEItem);
                    }
                    this.BindGrid(GetNDEItem);  // 初始化页面 
                }
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid(List<Model.View_Batch_NDEItem> nDEItems)
        {
            DataTable tb = this.LINQToDataTable(nDEItems);
            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(GridNewDynamic, tb1);
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();

            CheckBoxField ckbIsSelected = (CheckBoxField)Grid1.FindColumn("ckbIsSelected");

            for (int i = 0; i < this.Grid1.Rows.Count; i++)
            {
                string id = Grid1.DataKeys[i][0].ToString();
                Model.HJGL_Batch_NDEItem item = BLL.Batch_NDEItemService.GetNDEItemById(id);
                if (item != null)
                {
                    ckbIsSelected.SetCheckedState(i, true);
                    this.Grid1.Rows[i].Values[12] = BLL.Base_DefectService.GetDefectNameStrByDefectIdStr(item.CheckDefects);
                    if (item.SubmitDate != null)    //已审核
                    {
                        this.Grid1.Rows[i].CellCssClasses[6] = "f-grid-cell-uneditable";
                        this.Grid1.Rows[i].CellCssClasses[7] = "f-grid-cell-uneditable";
                        this.Grid1.Rows[i].CellCssClasses[8] = "f-grid-cell-uneditable";
                        this.Grid1.Rows[i].CellCssClasses[9] = "f-grid-cell-uneditable";
                        this.Grid1.Rows[i].CellCssClasses[10] = "f-grid-cell-uneditable";
                        this.Grid1.Rows[i].CellCssClasses[11] = "f-grid-cell-uneditable";
                        this.Grid1.Rows[i].CellCssClasses[12] = "f-grid-cell-uneditable";
                        this.Grid1.Rows[i].CellCssClasses[13] = "f-grid-cell-uneditable";
                        this.Grid1.Rows[i].CellCssClasses[14] = "f-grid-cell-uneditable";
                        this.Grid1.Rows[i].CellCssClasses[15] = "f-grid-cell-uneditable";
                    }
                }
            }
        }
        #endregion

        #region 排序
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void Grid1_Sort(object sender, GridSortEventArgs e)
        //{
        //    List<Model.View_Batch_NDEItem> GetNDEItemItem = this.CollectGridNDEItem();
        //    this.BindGrid(GetNDEItemItem);
        //}
        #endregion

        #region 检测单 提交事件
        /// <summary>
        /// 编辑检测单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_NDTBatchMenuId, Const.BtnSave))
            {
                List<Model.View_Batch_NDEItem> GetNDEItem = this.CollectGridNDEItem();
                if (this.Grid1.Rows.Count == 0)
                {
                    ShowNotify("没有要检测的焊口！", MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    bool noData = false;
                    foreach (var item in GetNDEItem)
                    {
                        if (item.TotalFilm == null)
                        {
                            noData = true;
                            break;
                        }
                    }
                    if (noData)
                    {
                        ShowNotify("请勾选并录入焊口的检测信息！", MessageBoxIcon.Warning);
                        return;
                    }
                }

                Model.HJGL_Batch_NDE newNDE = new Model.HJGL_Batch_NDE();
                Model.HJGL_Batch_BatchTrust trust = BLL.Batch_BatchTrustService.GetBatchTrustById(this.TrustBatchId);
                newNDE.TrustBatchId = trust.TrustBatchId;
                newNDE.NDECode = this.txtNDECode.Text.Trim();
                newNDE.ProjectId = this.CurrUser.LoginProjectId;
                newNDE.UnitId = trust.UnitId;
                newNDE.UnitWorkId = trust.UnitWorkId;
                newNDE.NDEUnit = trust.NDEUnit;
                newNDE.NDEMan = this.CurrUser.UserId;
                string nedId = string.Empty;
                Model.View_Batch_NDE nde = BLL.Batch_NDEService.GetNDEViewByTrustBatchId(this.TrustBatchId);
                if (nde!=null)
                {
                    nedId= nde.NDEID;
                    newNDE.NDEID = nde.NDEID;
                    BLL.Batch_NDEService.UpdateNDE(newNDE);
                    //BLL.Sys_LogService.AddLog(BLL.Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_NDTBatchMenuId, Const.BtnSave, this.NDEID);
                }
                else
                {
                    nedId= SQLHelper.GetNewID(typeof(Model.HJGL_Batch_NDE));
                    newNDE.NDEID = nedId;
                    BLL.Batch_NDEService.AddNDE(newNDE);
                    //BLL.Sys_LogService.AddLog(BLL.Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_NDTBatchMenuId, Const.BtnSave, this.NDEID);
                }


                string errlog = string.Empty;
                foreach (var item in GetNDEItem)
                {
                    var oldItem = BLL.Batch_NDEItemService.GetNDEItemById(item.NDEItemID);
                    if (oldItem == null)
                    {
                        Model.HJGL_Batch_NDEItem newItem = new Model.HJGL_Batch_NDEItem();
                        newItem.NDEItemID = item.NDEItemID;
                        newItem.NDEID = nedId;
                        newItem.TrustBatchItemId = item.TrustBatchItemId;
                        newItem.DetectionTypeId = item.DetectionTypeId;
                        newItem.RequestDate = item.RequestDate;
                        newItem.RepairLocation = item.RepairLocation;
                        newItem.TotalFilm = item.TotalFilm;
                        newItem.PassFilm = item.PassFilm;
                        newItem.CheckResult = item.CheckResult;
                        newItem.NDEReportNo = item.NDEReportNo;
                        newItem.FilmDate = item.FilmDate;
                        newItem.ReportDate = item.ReportDate;
                        newItem.SubmitDate = item.SubmitDate;
                        newItem.CheckDefects = item.CheckDefects;
                        newItem.JudgeGrade = item.JudgeGrade;
                        newItem.Remark = item.Remark;
                        BLL.Batch_NDEItemService.AddNDEItem(newItem);
                    }
                    else
                    {
                        oldItem.RequestDate = item.RequestDate;
                        oldItem.RepairLocation = item.RepairLocation;
                        oldItem.TotalFilm = item.TotalFilm;
                        oldItem.PassFilm = item.PassFilm;
                        oldItem.CheckResult = item.CheckResult;
                        oldItem.NDEReportNo = item.NDEReportNo;
                        oldItem.FilmDate = item.FilmDate;
                        oldItem.ReportDate = item.ReportDate;
                        oldItem.SubmitDate = item.SubmitDate;
                        oldItem.CheckDefects = item.CheckDefects;
                        oldItem.JudgeGrade = item.JudgeGrade;
                        oldItem.Remark = item.Remark;
                        BLL.Batch_NDEItemService.UpdateNDEItem(oldItem);
                    }
                }
                if (string.IsNullOrEmpty(errlog))
                {
                    ShowNotify("提交成功！", MessageBoxIcon.Success);
                    PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
                }
                else
                {
                    // string okj = ActiveWindow.GetWriteBackValueReference(newWeldReportMain.NDEID) + ActiveWindow.GetHidePostBackReference();
                    Alert.ShowInTop("提交完成，检测明细中" + errlog, "提交结果", MessageBoxIcon.Warning);
                    // ShowAlert("焊接明细中" + errlog, MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }
        #endregion

        #region 收集Grid页面信息
        /// <summary>
        /// 收集Grid页面信息
        /// </summary>
        /// <returns></returns>
        private List<Model.View_Batch_NDEItem> CollectGridNDEItem()
        {
            List<Model.View_Batch_NDEItem> GetNDEItem = new List<Model.View_Batch_NDEItem>();
            JArray mergedData = Grid1.GetMergedData();
            foreach (JObject mergedRow in mergedData)
            {
                string status = mergedRow.Value<string>("status");
                JObject values = mergedRow.Value<JObject>("values");
                int rowIndex = mergedRow.Value<int>("index");
                CheckBoxField ckbIsSelected = (CheckBoxField)Grid1.FindColumn("ckbIsSelected");
                bool b = ckbIsSelected.GetCheckedState(rowIndex);
                if (b)
                {
                    Model.View_Batch_NDEItem item = new Model.View_Batch_NDEItem();
                    item.NDEItemID = values.Value<string>("NDEItemID");
                    item.TrustBatchItemId = values.Value<string>("TrustBatchItemId");
                    item.DetectionTypeId = this.hdDetectionType.Text;
                    item.RepairLocation = values.Value<string>("RepairLocation");
                    item.TotalFilm = Funs.GetNewInt(values.Value<string>("TotalFilm"));
                    item.PassFilm = Funs.GetNewInt(values.Value<string>("PassFilm"));
                    string checkResult = values.Value<string>("CheckResultStr");
                    if (checkResult == "合格")
                    {
                        item.CheckResult = "1";
                    }
                    else if (checkResult == "不合格")
                    {
                        item.CheckResult = "2";
                    }
                    else
                    {
                        item.CheckResult = null;
                    }
                    item.NDEReportNo = values.Value<string>("NDEReportNo");
                    item.FilmDate = Funs.GetNewDateTime(values.Value<string>("FilmDate"));
                    item.ReportDate = Funs.GetNewDateTime(values.Value<string>("ReportDate"));
                    item.SubmitDate = Funs.GetNewDateTime(values.Value<string>("SubmitDate"));
                    string checkDefects = string.Empty;
                    string strs = values.Value<string>("CheckDefects");
                    checkDefects = BLL.Base_DefectService.GetDefectIdStrByDefectNameStr(strs);
                    item.CheckDefects = checkDefects;
                    item.JudgeGrade = values.Value<string>("JudgeGrade");
                    item.Remark = values.Value<string>("Remark");


                    GetNDEItem.Add(item);
                }

            }
            return GetNDEItem;
        }
        #endregion
    }
}