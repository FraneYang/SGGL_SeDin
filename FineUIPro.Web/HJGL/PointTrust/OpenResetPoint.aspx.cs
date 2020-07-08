using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;

namespace FineUIPro.Web.HJGL.PointTrust
{
    public partial class OpenResetPoint : PageBase
    {
        #region 定义项       
        /// <summary>
        /// 批主键
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
            string strSql = @"SELECT PointBatchItem.PointBatchItemId,PointBatch.PointBatchCode, WeldJoint.WeldJointCode,
                                     PointBatchItem.PointState,  UnitWork.UnitWorkCode,unit.UnitCode,
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
                               WHERE trustItem.TrustBatchItemId IS NULL AND  PointBatchItem.IsAudit IS NULL 
                                     AND PointBatch.ProjectId=@ProjectId AND PointBatchItem.PointBatchId=@PointBatchId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            listStr.Add(new SqlParameter("@PointBatchId", this.PointBatchId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            // tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();

            DataRow[] rds = tb.Select("PointState='1'");
            if (null != rds)
            {
                string[] ids = rds.Select(t => t.Field<string>("PointBatchItemId")).ToArray();
                this.Grid1.SelectedRowIDArray = ids;
            }


            //string ids = string.Empty;
            //for (int i = 0; i < this.Grid1.Rows.Count; i++)
            //{
            //    var pointItem = BLL.PointBatchDetailService.GetBatchDetailById(this.Grid1.Rows[i].DataKeys[0].ToString());
            //    if (pointItem != null && pointItem.PointState == "1")
            //    {
            //        ids += pointItem.PointBatchItemId + ",";
            //    }
            //}

            //if (!string.IsNullOrEmpty(ids))
            //{
            //    ids = ids.Substring(0, ids.Length - 1);
            //    this.Grid1.SelectedRowIDArray = ids.Split(',');
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
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            string[] selectRowId = Grid1.SelectedRowIDArray;
            foreach (GridRow row in Grid1.Rows)
            {
                if (selectRowId.Contains(row.RowID))
                {
                    BLL.PointBatchDetailService.UpdatePointBatchDetail(row.RowID, "1", DateTime.Now);
                }
                else
                {
                    BLL.PointBatchDetailService.UpdatePointBatchDetail(row.RowID, null, null);
                }
            }
            //if (selectRowId.Count() > 0)
            //{
            //    foreach (var item in selectRowId)
            //    {
            //        BLL.PointBatchDetailService.UpdatePointBatchDetail(item, "1", DateTime.Now);
            //    }
            //}
           
            //BLL.Sys_LogService.AddLog(BLL.Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_PointBatchMenuId, Const.BtnOpenResetPoint, this.PointBatchId);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        protected void btnCancelAccept_Click(object sender, EventArgs e)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            string[] selectRowId = Grid1.SelectedRowIDArray;
            if (selectRowId.Count() > 0)
            {
                foreach (var item in selectRowId)
                {
                    BLL.PointBatchDetailService.UpdatePointBatchDetail(item, null, null);
                }
            }

            //BLL.Sys_LogService.AddLog(BLL.Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_PointBatchMenuId, Const.BtnOpenResetPoint, this.PointBatchId);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
    }
}