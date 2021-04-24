using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BLL;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace FineUIPro.Web.HJGL.PointTrust
{
    public partial class PointTrust : PageBase
    {
        #region 定义项
        /// <summary>
        /// 检验批主键
        /// </summary>
        public string PointBatchId
        {
            get
            {
                return (string)ViewState["PointBatchId"];
            }
            set
            {
                ViewState["PointBatchId"] = value;
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
                this.PointBatchId = Request.Params["PointBatchId"];
                BLL.UnitService.InitUnitByProjectIdUnitTypeDropDownList(this.drpNDEUnit, this.CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_5, true);
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
            var batch = BLL.PointBatchService.GetPointBatchById(this.PointBatchId);
            if (batch != null)
            {
                var unit = BLL.UnitService.GetUnitByUnitId(batch.UnitId);
                var unitWork = BLL.UnitWorkService.GetUnitWorkByUnitWorkId(batch.UnitWorkId);
                var ndt = BLL.Base_DetectionTypeService.GetDetectionTypeByDetectionTypeId(batch.DetectionTypeId);
                var rate = BLL.Base_DetectionRateService.GetDetectionRateByDetectionRateId(batch.DetectionRateId);
                this.txtTrustBatchCode.Text = batch.PointBatchCode.Replace("-DK-", "-WT-");
                if (unit != null)
                {
                    this.txtUnit.Text = unit.UnitName;
                }
                if (unitWork != null)
                {
                    this.txtUnitWork.Text = unitWork.UnitWorkName;
                }
                this.txtTrustDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                if (ndt != null)
                {
                    this.txtDetectionType.Text = ndt.DetectionTypeCode;
                }
                this.BindGrid();  // 初始化页面 
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            var batch = BLL.PointBatchService.GetPointBatchById(this.PointBatchId);
            // 生成委托条件对比
            var generateTrustItem = (from x in Funs.DB.View_GenerateTrustItem
                                     where x.PointBatchId == this.PointBatchId
                                     select x).ToList();
            DataTable tb = this.LINQToDataTable(generateTrustItem);
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
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 分页选择下拉改变事件
        /// <summary>
        /// 分页选择下拉改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }
        #endregion

        #region 页索引改变事件
        /// <summary>
        /// 页索引改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 委托单 提交事件
        /// <summary>
        /// 编辑委托单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_TrustBatchMenuId, Const.BtnGenerate))
            {
                if (this.drpNDEUnit.SelectedValue == BLL.Const._Null)
                {
                    ShowNotify("请选择检测单位！", MessageBoxIcon.Warning);
                    return;
                }
                Model.SGGLDB db = Funs.DB;
                Model.HJGL_Batch_PointBatch batch = BLL.PointBatchService.GetPointBatchById(this.PointBatchId);
                if (batch != null)
                {
                    Model.HJGL_Batch_BatchTrust newBatchTrust = new Model.HJGL_Batch_BatchTrust();
                    var project = BLL.ProjectService.GetProjectByProjectId(batch.ProjectId);
                    var unit = BLL.UnitService.GetUnitByUnitId(batch.UnitId);
                    var area = BLL.UnitWorkService.getUnitWorkByUnitWorkId(batch.UnitWorkId);
                    var ndt = BLL.Base_DetectionTypeService.GetDetectionTypeByDetectionTypeId(batch.DetectionTypeId);
                    var rate = BLL.Base_DetectionRateService.GetDetectionRateByDetectionRateId(batch.DetectionRateId);

                    string perfix = string.Empty;

                    newBatchTrust.TrustBatchCode = this.txtTrustBatchCode.Text.Trim();
                    string trustBatchId = SQLHelper.GetNewID(typeof(Model.HJGL_Batch_BatchTrust));
                    newBatchTrust.TrustBatchId = trustBatchId;

                    newBatchTrust.TrustDate = DateTime.Now;
                    newBatchTrust.ProjectId = batch.ProjectId;
                    newBatchTrust.PointBatchId = batch.PointBatchId;
                    newBatchTrust.UnitId = batch.UnitId;
                    newBatchTrust.UnitWorkId = batch.UnitWorkId;
                    newBatchTrust.DetectionTypeId = batch.DetectionTypeId;
                    newBatchTrust.DetectionRateId = batch.DetectionRateId;
                    if (this.drpNDEUnit.SelectedValue != BLL.Const._Null)
                    {
                        newBatchTrust.NDEUnit = this.drpNDEUnit.SelectedValue;
                    }
                    newBatchTrust.PointBatchId = this.PointBatchId;
                    BLL.Batch_BatchTrustService.AddBatchTrust(newBatchTrust);  // 新增委托单

                    // 生成委托条件对比
                    var generateTrustItem = from x in db.View_GenerateTrustItem
                                            where x.ProjectId == batch.ProjectId
                                            && x.UnitWorkId == batch.UnitWorkId && x.UnitId == batch.UnitId
                                            && x.DetectionTypeId == batch.DetectionTypeId
                                            && x.DetectionRateId == batch.DetectionRateId
                                            select x;

                    List<string> toPointBatchList = generateTrustItem.Select(x => x.PointBatchId).Distinct().ToList();

                    // 生成委托明细，并回写点口明细信息
                    foreach (var item in generateTrustItem)
                    {
                        if (BLL.Batch_BatchTrustItemService.GetIsGenerateTrust(item.PointBatchItemId)) ////生成委托单的条件判断
                        {
                            Model.HJGL_Batch_BatchTrustItem trustItem = new Model.HJGL_Batch_BatchTrustItem
                            {
                                TrustBatchItemId = SQLHelper.GetNewID(typeof(Model.HJGL_Batch_BatchTrustItem)),
                                TrustBatchId = trustBatchId,
                                PointBatchItemId = item.PointBatchItemId,
                                WeldJointId = item.WeldJointId,
                                CreateDate = DateTime.Now
                            };
                            Batch_BatchTrustItemService.AddBatchTrustItem(trustItem);
                        }

                        Model.HJGL_Batch_PointBatchItem pointBatchItem = db.HJGL_Batch_PointBatchItem.First(x => x.PointBatchItemId == item.PointBatchItemId);

                        pointBatchItem.IsBuildTrust = true;
                        db.SubmitChanges();
                    }


                    // 回写委托批对应点口信息
                    if (toPointBatchList.Count() > 0)
                    {
                        string toPointBatch = String.Join(",", toPointBatchList);

                        var updateTrut = BLL.Batch_BatchTrustService.GetBatchTrustById(trustBatchId);
                        if (updateTrut != null)
                        {
                            updateTrut.TopointBatch = toPointBatch;
                            BLL.Batch_BatchTrustService.UpdateBatchTrust(updateTrut);
                        }
                    }
                }
                ShowNotify("提交成功！", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }
        #endregion
    }
}