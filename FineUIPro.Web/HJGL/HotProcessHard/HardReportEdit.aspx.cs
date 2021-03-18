using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.HJGL.HotProcessHard
{
    public partial class HardReportEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string HardTrustID
        {
            get
            {
                return (string)ViewState["HardTrustID"];
            }
            set
            {
                ViewState["HardTrustID"] = value;
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
                this.HardTrustID = Request.Params["HardTrustID"];
                var trust = BLL.Hard_TrustService.GetHardTrustById(this.HardTrustID);
                ///委托人
                this.drpHardTrustMan.DataValueField = "UserId";
                this.drpHardTrustMan.DataTextField = "UserName";
                this.drpHardTrustMan.DataSource = from x in Funs.DB.Sys_User
                                                  join y in Funs.DB.Project_ProjectUser
                                                  on x.UserId equals y.UserId
                                                  where y.ProjectId == this.CurrUser.LoginProjectId
                                                  select x;
                this.drpHardTrustMan.DataBind();
                BLL.UnitService.InitUnitByProjectIdUnitTypeDropDownList(this.drpHardTrustUnit, this.CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_2, true);
                BLL.UnitService.InitUnitByProjectIdUnitTypeDropDownList(this.drpCheckUnit, this.CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_5, true);
                BLL.UnitWorkService.InitUnitWorkDownList(this.drpUnitWork, this.CurrUser.LoginProjectId, true);
                BLL.UserService.InitUserProjectIdUnitTypeDropDownList(drpSendee, this.CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_5, true);
                if (trust != null)
                {
                    this.txtHardTrustNo.Text = trust.HardTrustNo;
                    if (!string.IsNullOrEmpty(trust.HardTrustUnit))
                    {
                        this.drpHardTrustUnit.SelectedValue = trust.HardTrustUnit;
                    }
                    if (!string.IsNullOrEmpty(trust.UnitWorkId))
                    {
                        this.drpUnitWork.SelectedValue = trust.UnitWorkId;
                    }
                    if (!string.IsNullOrEmpty(trust.CheckUnit))
                    {
                        this.drpCheckUnit.SelectedValue = trust.CheckUnit;
                    }
                    if (!string.IsNullOrEmpty(trust.HardTrustMan) && trust.HardTrustMan != BLL.Const._Null)
                    {
                        this.drpHardTrustMan.SelectedValue = trust.HardTrustMan;
                    }
                    if (trust.HardTrustDate != null)
                    {
                        this.txtHardTrustDate.Text = string.Format("{0:yyyy-MM-dd}", trust.HardTrustDate);
                    }
                    this.txtHardnessMethod.Text = trust.HardnessMethod;
                    this.txtHardnessRate.Text = trust.HardnessRate;
                    this.txtStandards.Text = trust.Standards;
                    this.txtInspectionNum.Text = trust.InspectionNum;
                    this.txtCheckNum.Text = trust.CheckNum;
                    this.txtTestWeldNum.Text = trust.TestWeldNum;
                    this.rblDetectionTime.SelectedValue = trust.DetectionTime;
                    if (!string.IsNullOrEmpty(trust.Sendee))
                    {
                        drpSendee.SelectedValue = trust.Sendee;
                    }
                    this.txtCheckName.Text = trust.CheckName;
                    this.txtAcceptStandard.Text = trust.AcceptStandard;
                    this.txtEquipmentModel.Text = trust.EquipmentModel;
                    BindGrid();
                }
            }
        }

        #region 数据绑定
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            string strSql = string.Empty;
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql = @"SELECT * ,(CASE WHEN IsPass=1 THEN '合格' WHEN IsPass=0 THEN '不合格' WHEN IsPass IS NULL THEN '待检测' END) AS checkResult
                           FROM dbo.View_HJGL_Hard_TrustItem
                           WHERE HardTrustID=@HardTrustID";
            listStr.Add(new SqlParameter("@HardTrustID", this.HardTrustID));
            if (!string.IsNullOrEmpty(this.txtPipelineCode.Text.Trim()))
            {
                strSql += @" and PipelineCode like @PipelineCode ";
                listStr.Add(new SqlParameter("@PipelineCode", "%" + this.txtPipelineCode.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtWeldJointCode.Text.Trim()))
            {
                strSql += @" and WeldJointCode like @WeldJointCode ";
                listStr.Add(new SqlParameter("@WeldJointCode", "%" + this.txtWeldJointCode.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(Grid1, tb1);
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion
        #endregion

        #region 分页排序
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
        #endregion

        #region 硬度委托 提交事件
        /// <summary>
        /// 编辑硬度委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var trust = BLL.Hard_TrustService.GetHardTrustById(this.HardTrustID);
            if (trust != null)
            {
                trust.InspectionNum = this.txtInspectionNum.Text;
                trust.HardnessMethod = this.txtHardnessMethod.Text;
                trust.EquipmentModel = this.txtEquipmentModel.Text;
                BLL.Hard_TrustService.UpdateHardTrust(trust);
            }
            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        #endregion
    }
}