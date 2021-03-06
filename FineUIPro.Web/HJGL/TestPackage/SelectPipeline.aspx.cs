﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace FineUIPro.Web.HJGL.TestPackage
{
    public partial class SelectPipeline : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 试验介质
                Base_TestMediumService.InitMediumDropDownList(this.drpTestMedium, "1", true);
                // 管线等级
                BLL.Base_PipingClassService.InitPipingClassDropDownList(drpPipingClass, this.CurrUser.LoginProjectId, true, "请选择");
                //管道介质
                Base_MediumService.InitMediumDropDownList(this.drpMedium, this.CurrUser.LoginProjectId, true);
                BindGrid();
            }
        }
        private void BindGrid()
        {
            string PTP_ID = Request.Params["PTP_ID"];
            string UnitWorkId = Request.Params["UnitWorkId"];
            string strSql = @"SELECT IsoInfo.ProjectId,IsoInfo.UnitWorkId,UnitWork.UnitWorkCode,IsoInfo.PipelineId,IsoInfo.PipelineCode,
                              IsoInfo.UnitId,IsoInfo.DesignPress,IsoInfo.DesignTemperature,IsoInfo.TestPressure,IsoInfo.TestMedium,testMedium.MediumName AS 
                              TestMediumName,IsoInfo.SingleNumber,IsoInfo.PipingClassId,class.PipingClassCode
                              FROM dbo.HJGL_Pipeline AS IsoInfo
                              LEFT JOIN WBS_UnitWork AS UnitWork ON IsoInfo.UnitWorkId=UnitWork.UnitWorkId
							  LEFT JOIN dbo.Base_TestMedium  AS testMedium ON testMedium.TestMediumId = IsoInfo.TestMedium
							  LEFT JOIN dbo.Base_PipingClass class ON class.PipingClassId = IsoInfo.PipingClassId
                              WHERE IsoInfo.ProjectId= @ProjectId AND UnitWork.UnitWorkId= @UnitWorkId 
                              and (select count(*) from PTP_PipelineList p where p.PipelineId=IsoInfo.PipelineId)=0";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            listStr.Add(new SqlParameter("@UnitWorkId", UnitWorkId));


            if (this.drpPipingClass.SelectedValue != Const._Null)
            {
                strSql += " AND IsoInfo.PipingClassId = @PipingClassId";
                listStr.Add(new SqlParameter("@PipingClassId", this.drpPipingClass.SelectedValue));
            }
            if (this.drpMedium.SelectedValue != Const._Null)
            {
                strSql += " AND IsoInfo.MediumId = @MediumId";
                listStr.Add(new SqlParameter("@MediumId", this.drpMedium.SelectedValue));
            }
            if (this.drpTestMedium.SelectedValue != Const._Null)
            {
                strSql += " AND IsoInfo.TestMedium = @TestMedium";
                listStr.Add(new SqlParameter("@TestMedium", this.drpTestMedium.SelectedValue));
            }

            if (!string.IsNullOrEmpty(numTestPressure.Text) && !string.IsNullOrEmpty(numTo.Text))
            {
                strSql += " AND IsoInfo.TestPressure >=@MinTestPressure AND IsoInfo.TestPressure <=@MaxTestPressure";
                listStr.Add(new SqlParameter("@MinTestPressure", Convert.ToDecimal(numTestPressure.Text)));
                listStr.Add(new SqlParameter("@MaxTestPressure", Convert.ToDecimal(numTo.Text)));
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(Grid1, tb1);
            Grid1.RecordCount = tb.Rows.Count;
            // tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        protected void btnFind_Click(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        #region 分页排序
        #region 页索引改变事件
        /// <summary>
        /// 页索引改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            this.BindGrid();
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

        #region 分页选择下拉改变事件
        /// <summary>
        /// 分页选择下拉改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            this.BindGrid();
        }
        #endregion

        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一行", MessageBoxIcon.Warning);
                return;
            }
            string Pipeline = Request.Params["Pipelines"];
            string Pipelines = string.Empty;
            if (!string.IsNullOrEmpty(Pipeline))
            {
                Pipelines += Pipeline + ",";
            }
            int[] selections = Grid1.SelectedRowIndexArray;
            foreach (int rowIndex in selections)
            {
                if (!Pipeline.Contains(Grid1.DataKeys[rowIndex][0].ToString()))
                {
                    Pipelines += Grid1.DataKeys[rowIndex][0].ToString() + ",";
                }
            }
            if (Pipelines != string.Empty)
            {
                Pipelines = Pipelines.Substring(0, Pipelines.Length - 1);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(Pipelines) + ActiveWindow.GetHidePostBackReference());
        }
        protected void btnRset_Click(object sender, EventArgs e)
        {
            drpMedium.SelectedIndex = 0;
            drpPipingClass.SelectedIndex = 0;
            drpTestMedium.SelectedIndex = 0;
            numTestPressure.Text = "";
            numTo.Text = "";
            BindGrid();
        }
    }
}