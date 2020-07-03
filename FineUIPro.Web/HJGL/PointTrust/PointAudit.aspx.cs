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
            string strSql = @"SELECT PointBatchItem.PointBatchItemId,PointBatch.PointBatchCode, WeldJoint.WeldJointCode,UnitWork.UnitWorkCode,unit.UnitCode,
                                    (CASE PointBatchItem.IsAudit WHEN 1 THEN '是' ELSE '否' END) AS PointIsAudit,
                                     WeldJoint.JointArea,WeldingDaily.WeldingDate,PipingClass.PipingClassName,
                                     trustItem.TrustBatchItemId
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

            DataRow[] rds=tb.Select("PointIsAudit='是'");
            if (null != rds)
            {
                string[] ids = rds.Select(t => t.Field<string>("PointBatchItemId")).ToArray();
                this.Grid1.SelectedRowIDArray = ids;
            }


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
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_PointBatchMenuId, Const.BtnGenerate))
            {
                /////取当前项目所有未委托批
                //var getViewPointBatchLists = (from x in Funs.DB.View_Batch_PointBatch
                //                              where x.EndDate.HasValue && (!x.IsTrust.HasValue || !x.IsTrust.Value) && x.ProjectId == this.CurrUser.LoginProjectId
                //                              select x).ToList();

                var getViewGenerateTrustLists = (from x in Funs.DB.View_GenerateTrust where x.ProjectId == this.CurrUser.LoginProjectId select x).ToList();
                if (getViewGenerateTrustLists.Count() > 0)
                {
                    var getUnit = BLL.UnitService.GetUnitByUnitId(this.CurrUser.UnitId);
                    if (getUnit == null || getUnit.UnitTypeId == "1" || getUnit.UnitTypeId == "2" || getUnit.UnitTypeId == "3")
                    {
                        GenerateTrust(getViewGenerateTrustLists);
                    }
                    else
                    {
                        var getUnitViewGenerateTrustLists = getViewGenerateTrustLists.Where(x => x.UnitId == this.CurrUser.UnitId);
                        if (getUnitViewGenerateTrustLists.Count() > 0)
                        {
                            // 当前单位未委托批
                            GenerateTrust(getUnitViewGenerateTrustLists.ToList());
                        }
                        else
                        {
                            Alert.ShowInTop("所属单位审核的点口已全部生成委托单！", MessageBoxIcon.Warning);
                        }
                    }
                    //BLL.Sys_LogService.AddLog(BLL.Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_PointBatchMenuId, Const.BtnGenerate, null);
                    this.BindGrid();
                }
                else
                {
                    Alert.ShowInTop("已全部生成委托单！", MessageBoxIcon.Warning);
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
        private void GenerateTrust(List<Model.View_GenerateTrust> GenerateTrustLists)
        {
            Model.SGGLDB db = Funs.DB;
            foreach (var trust in GenerateTrustLists)
            {
                Model.HJGL_Batch_BatchTrust newBatchTrust = new Model.HJGL_Batch_BatchTrust();
                var project = BLL.ProjectService.GetProjectByProjectId(trust.ProjectId);
                var unit = BLL.UnitService.GetUnitByUnitId(trust.UnitId);
                var area = BLL.UnitWorkService.getUnitWorkByUnitWorkId(trust.UnitWorkId);
                var ndt = BLL.Base_DetectionTypeService.GetDetectionTypeByDetectionTypeId(trust.DetectionTypeId);
                var rate = BLL.Base_DetectionRateService.GetDetectionRateByDetectionRateId(trust.DetectionRateId);

                string perfix = string.Empty;
                perfix = project.ProjectCode + "-" + unit.UnitCode + "-" + ndt.DetectionTypeCode + "-" + rate.DetectionRateValue.ToString() + "%-" + area.UnitWorkCode + "-";
                newBatchTrust.TrustBatchCode = BLL.SQLHelper.RunProcNewId("SpGetNewCode", "dbo.HJGL_Batch_BatchTrust", "TrustBatchCode", project.ProjectId, perfix);

                string trustBatchId = SQLHelper.GetNewID(typeof(Model.HJGL_Batch_BatchTrust));
                newBatchTrust.TrustBatchId = trustBatchId;

                newBatchTrust.TrustDate = DateTime.Now;
                newBatchTrust.ProjectId = trust.ProjectId;
                newBatchTrust.UnitId = trust.UnitId;
                newBatchTrust.UnitWorkId = trust.UnitWorkId;
                newBatchTrust.DetectionTypeId = trust.DetectionTypeId;
                newBatchTrust.NDEUuit = area.NDEUnit;

                BLL.Batch_BatchTrustService.AddBatchTrust(newBatchTrust);  // 新增委托单

                // 生成委托条件对比
                var generateTrustItem = from x in db.View_GenerateTrustItem
                                        where x.ProjectId == trust.ProjectId 
                                        && x.UnitWorkId == trust.UnitWorkId && x.UnitId == trust.UnitId
                                        && x.DetectionTypeId == trust.DetectionTypeId
                                        && x.DetectionRateId==trust.DetectionRateId
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

                    Model.HJGL_Batch_PointBatchItem pointBatchItem = db.HJGL_Batch_PointBatchItem.FirstOrDefault(e => e.PointBatchItemId == item.PointBatchItemId);
                   
                    pointBatchItem.IsBuildTrust = true;
                    db.SubmitChanges();
                }

               
                // 回写委托批对应点口信息
                if (toPointBatchList.Count()>0)
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

            Alert.ShowInTop("已成功生成委托单！", MessageBoxIcon.Success);
        }

        #endregion
    }
}