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
    public partial class NDTBatchAudit : PageBase
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
                if (!string.IsNullOrEmpty(Request.Params["View"]))
                {
                    this.btnSave.Hidden = true;
                }
                this.PageInfoLoad(); // 加载页面 
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
        /// 审核检测单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_NDTBatchMenuId, Const.BtnSave))
            {
                Model.HJGL_Batch_NDE nde = BLL.Batch_NDEService.GetNDEByTrustBatchId(this.TrustBatchId);
                List<Model.HJGL_Batch_NDEItem> GetNDEItem = BLL.Batch_NDEItemService.GetNDEItemByNDEID(nde.NDEID);
                Model.SGGLDB db = Funs.DB;
                //未录入检测结果的焊口，取消委托状态，可重新委托检测
                List<Model.HJGL_Batch_BatchTrustItem> list = (from x in Funs.DB.HJGL_Batch_BatchTrustItem where x.TrustBatchId == nde.TrustBatchId select x).ToList();
                foreach (var item in list)
                {
                    var ndeItem = GetNDEItem.FirstOrDefault(x => x.TrustBatchItemId == item.TrustBatchItemId);
                    if (ndeItem == null)   //未录入检测结果的焊口
                    {
                        ShowNotify("请录入所有委托焊口的检测结果后，再审核！", MessageBoxIcon.Warning);
                        return;
                    }
                }
                foreach (var item in GetNDEItem)
                {
                    item.SubmitDate = DateTime.Now;
                    BLL.Batch_NDEItemService.UpdateNDEItem(item);
                }
                if (nde != null)
                {
                    nde.AuditDate = DateTime.Now;
                    BLL.Batch_NDEService.UpdateNDE(nde);
                    BLL.Batch_BatchTrustService.UpdatTrustBatchtState(nde.TrustBatchId, true);
                }
                ShowNotify("审核成功！", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());



                //string errlog = string.Empty;
                //foreach (var item in GetNDEItem)
                //{
                //    var oldItem = BLL.Batch_NDEItemService.GetNDEItemById(item.NDEItemID);
                //    if (oldItem == null)
                //    {
                //        Model.HJGL_Batch_NDEItem newItem = new Model.HJGL_Batch_NDEItem();
                //        newItem.NDEItemID = item.NDEItemID;
                //        newItem.NDEID = nedId;
                //        newItem.TrustBatchItemId = item.TrustBatchItemId;
                //        newItem.DetectionTypeId = item.DetectionTypeId;
                //        newItem.RequestDate = item.RequestDate;
                //        newItem.RepairLocation = item.RepairLocation;
                //        newItem.TotalFilm = item.TotalFilm;
                //        newItem.PassFilm = item.PassFilm;
                //        newItem.CheckResult = item.CheckResult;
                //        newItem.NDEReportNo = item.NDEReportNo;
                //        newItem.FilmDate = item.FilmDate;
                //        newItem.ReportDate = item.ReportDate;
                //        newItem.SubmitDate = item.SubmitDate;
                //        newItem.CheckDefects = item.CheckDefects;
                //        newItem.JudgeGrade = item.JudgeGrade;
                //        newItem.Remark = item.Remark;
                //        BLL.Batch_NDEItemService.AddNDEItem(newItem);
                //    }
                //    else
                //    {
                //        oldItem.RequestDate = item.RequestDate;
                //        oldItem.RepairLocation = item.RepairLocation;
                //        oldItem.TotalFilm = item.TotalFilm;
                //        oldItem.PassFilm = item.PassFilm;
                //        oldItem.CheckResult = item.CheckResult;
                //        oldItem.NDEReportNo = item.NDEReportNo;
                //        oldItem.FilmDate = item.FilmDate;
                //        oldItem.ReportDate = item.ReportDate;
                //        oldItem.SubmitDate = item.SubmitDate;
                //        oldItem.CheckDefects = item.CheckDefects;
                //        oldItem.JudgeGrade = item.JudgeGrade;
                //        oldItem.Remark = item.Remark;
                //        BLL.Batch_NDEItemService.UpdateNDEItem(oldItem);
                //    }
                //}
                //if (string.IsNullOrEmpty(errlog))
                //{
                //    ShowNotify("提交成功！", MessageBoxIcon.Success);
                //    PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
                //}
                //else
                //{
                //    // string okj = ActiveWindow.GetWriteBackValueReference(newWeldReportMain.NDEID) + ActiveWindow.GetHidePostBackReference();
                //    Alert.ShowInTop("提交完成，检测明细中" + errlog, "提交结果", MessageBoxIcon.Warning);
                //    // ShowAlert("焊接明细中" + errlog, MessageBoxIcon.Warning);
                //}
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
            return GetNDEItem;
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 获取缺陷
        /// </summary>
        /// <param name="bigType"></param>
        /// <returns></returns>
        protected string ConvertCheckDefects(object CheckDefects)
        {
            string str = string.Empty;
            if (CheckDefects != null)
            {
                str = BLL.Base_DefectService.GetDefectNameStrByDefectIdStr(CheckDefects.ToString());
            }
            return str;
        }
        #endregion
    }
}