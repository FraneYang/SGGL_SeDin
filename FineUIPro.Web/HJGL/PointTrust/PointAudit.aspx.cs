using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;

namespace FineUIPro.Web.HJGL.PointTrust
{
    public partial class PointAudit : PageBase
    {
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
                this.BindGrid();          
            }
        }
        #endregion
        
        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT distinct PointBatch.PointBatchId,PointBatch.PointBatchCode, UnitWork.UnitWorkName,unit.UnitCode
                               FROM dbo.HJGL_Batch_PointBatchItem AS PointBatchItem
                               LEFT JOIN dbo.HJGL_Batch_PointBatch AS PointBatch ON PointBatch.PointBatchId=PointBatchItem.PointBatchId
                               LEFT JOIN dbo.HJGL_WeldJoint AS WeldJoint ON WeldJoint.WeldJointId=PointBatchItem.WeldJointId
                               LEFT JOIN dbo.HJGL_Pipeline AS Pipeline ON Pipeline.PipelineId=WeldJoint.PipelineId
                               LEFT JOIN WBS_UnitWork AS UnitWork ON UnitWork.UnitWorkId=Pipeline.UnitWorkId
                               LEFT JOIN dbo.HJGL_WeldingDaily AS WeldingDaily ON WeldingDaily.WeldingDailyId=WeldJoint.WeldingDailyId
                               LEFT JOIN Base_PipingClass AS PipingClass ON PipingClass.PipingClassId=Pipeline.PipingClassId
                               LEFT JOIN dbo.Base_Unit unit ON unit.UnitId = PointBatch.UnitId
                               LEFT JOIN dbo.HJGL_Batch_BatchTrustItem trustItem ON trustItem.PointBatchItemId = PointBatchItem.PointBatchItemId
                               WHERE PointBatchItem.PointState IS NOT NULL AND trustItem.TrustBatchItemId IS NULL
                                     AND PointBatch.ProjectId=@ProjectId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            // tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();

            //DataRow[] rds=tb.Select("PointIsAudit='是'");
            //if (null != rds)
            //{
            //    string[] ids = rds.Select(t => t.Field<string>("PointBatchItemId")).ToArray();
            //    this.Grid1.SelectedRowIDArray = ids;
            //}


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
            this.BindGrid();
        }
        #endregion

        #region 确定按钮
        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAccept_Click(object sender, EventArgs e)
        {
            string[] selectRowId = Grid1.SelectedRowIDArray;
            if (selectRowId.Count() > 0)
            {
                foreach (var item in selectRowId)
                {
                    BLL.PointBatchDetailService.PointAudit(item, true);
                    //BLL.Sys_LogService.AddLog(BLL.Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_PointBatchMenuId, Const.BtnPointAudit, item);
                }
                this.BindGrid();
                Alert.ShowInTop("勾选的焊口已审核！");
            }
            else
            {
                Alert.ShowInTop("请勾选要审核的焊口！", MessageBoxIcon.Warning);
            }
        }

        protected void btnCancelAccept_Click(object sender, EventArgs e)
        {
            string[] selectRowId = Grid1.SelectedRowIDArray;
            if (selectRowId.Count() > 0)
            {
                foreach (var item in selectRowId)
                {
                    BLL.PointBatchDetailService.PointAudit(item, false);
                    //BLL.Sys_LogService.AddLog(BLL.Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_PointBatchMenuId, Const.BtnPointAudit, item);
                }
                this.BindGrid();
                Alert.ShowInTop("勾选的焊口已取消审核！");
            }
            else
            {
                Alert.ShowInTop("请勾选要取消审核的焊口！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 生成委托单
        /// <summary>
        /// 生成委托单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_TrustBatchMenuId, Const.BtnGenerate))
            {
                if (this.Grid1.SelectedRowIDArray.Length > 0)
                {
                    GenerateTrust();
                    BindGrid();
                }
                else
                {
                    Alert.ShowInTop("请选择需要生成委托单的批！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }


        /// <summary>
        /// 生成委托单
        /// </summary>
        /// <param name="unitId"></param>
        private void GenerateTrust()
        {
            Model.SGGLDB db = Funs.DB;
            foreach (var batchId in this.Grid1.SelectedRowIDArray)
            {
                Model.HJGL_Batch_PointBatch batch = BLL.PointBatchService.GetPointBatchById(batchId);
                if (batch != null)
                {
                    Model.HJGL_Batch_BatchTrust newBatchTrust = new Model.HJGL_Batch_BatchTrust();
                    var project = BLL.ProjectService.GetProjectByProjectId(batch.ProjectId);
                    var unit = BLL.UnitService.GetUnitByUnitId(batch.UnitId);
                    var area = BLL.UnitWorkService.getUnitWorkByUnitWorkId(batch.UnitWorkId);
                    var ndt = BLL.Base_DetectionTypeService.GetDetectionTypeByDetectionTypeId(batch.DetectionTypeId);
                    var rate = BLL.Base_DetectionRateService.GetDetectionRateByDetectionRateId(batch.DetectionRateId);

                    string perfix = string.Empty;
                    perfix = unit.UnitCode + "-" + ndt.DetectionTypeCode + "-" + rate.DetectionRateValue.ToString() + "%-";
                    newBatchTrust.TrustBatchCode = BLL.SQLHelper.RunProcNewId("SpGetNewCode5ByProjectId", "dbo.HJGL_Batch_BatchTrust", "TrustBatchCode", project.ProjectId, perfix);

                    string trustBatchId = SQLHelper.GetNewID(typeof(Model.HJGL_Batch_BatchTrust));
                    newBatchTrust.TrustBatchId = trustBatchId;

                    newBatchTrust.TrustDate = DateTime.Now;
                    newBatchTrust.ProjectId = batch.ProjectId;
                    newBatchTrust.PointBatchId = batch.PointBatchId;
                    newBatchTrust.UnitId = batch.UnitId;
                    newBatchTrust.UnitWorkId = batch.UnitWorkId;
                    newBatchTrust.DetectionTypeId = batch.DetectionTypeId;
                    newBatchTrust.DetectionRateId = batch.DetectionRateId;
                    newBatchTrust.NDEUnit = area.NDEUnit;

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

                        Model.HJGL_Batch_PointBatchItem pointBatchItem = db.HJGL_Batch_PointBatchItem.First(e => e.PointBatchItemId == item.PointBatchItemId);

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
            }

            Alert.ShowInTop("已成功生成委托单！", MessageBoxIcon.Success);
        }

        #endregion
    }
}