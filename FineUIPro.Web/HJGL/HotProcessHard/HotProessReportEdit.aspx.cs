using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BLL;

namespace FineUIPro.Web.HJGL.HotProcessHard
{
    public partial class HotProessReportEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 热处理委托主键
        /// </summary>
        public string HotProessTrustId
        {
            get
            {
                return (string)ViewState["HotProessTrustId"];
            }
            set
            {
                ViewState["HotProessTrustId"] = value;
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
            if (!IsPostBack) {
                this.ddlPageSize.SelectedValue = this.Grid1.PageSize.ToString();
                BLL.UnitWorkService.InitUnitWorkDownList(this.drpUnitWork, this.CurrUser.LoginProjectId, true);//单位工程
                BLL.UnitService.InitUnitByProjectIdUnitTypeDropDownList(this.drpUnitId, this.CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_2, true);//单位
                this.HotProessTrustId = Request.Params["HotProessTrustId"];
                BindGrid();
            }
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            string strSql = string.Empty;
            List<SqlParameter> listStr = new List<SqlParameter>();
            this.PageInfoLoad(); ///页面输入提交信息
                var hotProessTrust = BLL.HotProess_TrustService.GetHotProessTrustById(HotProessTrustId);
                if (hotProessTrust != null)
                {
                    this.HotProessTrustId = hotProessTrust.HotProessTrustId;
                    strSql = @"SELECT * "
                    + @" FROM dbo.View_HJGL_HotProess_TrustItem AS Trust"
                    + @" WHERE Trust.ProjectId= @ProjectId AND Trust.HotProessTrustId=@HotProessTrustId ";

                    listStr.Add(new SqlParameter("@ProjectId", hotProessTrust != null ? hotProessTrust.ProjectId : this.CurrUser.LoginProjectId));
                    listStr.Add(new SqlParameter("@HotProessTrustId", this.HotProessTrustId));

                    if (!string.IsNullOrEmpty(this.txtIsoNo.Text.Trim()))
                    {
                        strSql += @" and Trust.PipelineCode like '%'+@PipelineCode+'%' ";
                        listStr.Add(new SqlParameter("@PipelineCode", this.txtIsoNo.Text.Trim()));
                    }

                    SqlParameter[] parameter = listStr.ToArray();
                    DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
                    // 2.获取当前分页数据
                    //var table = this.GetPagedDataTable(Grid1, tb1);
                    Grid1.RecordCount = tb.Rows.Count;
                    //tb = GetFilteredTable(Grid1.FilteredData, tb);
                    var table = this.GetPagedDataTable(Grid1, tb);
                    Grid1.DataSource = table;
                    Grid1.DataBind();
                }
        }

        #region 加载页面输入提交信息
        /// <summary>
        /// 加载页面输入提交信息
        /// </summary>
        private void PageInfoLoad()
        {
            var trust = BLL.HotProess_TrustService.GetHotProessTrustById(this.HotProessTrustId);
            if (trust != null)
            {
                this.txtHotProessTrustNo.Text = trust.HotProessTrustNo;
                //if (trust.ProessDate.HasValue)
                //{
                //    this.txtProessDate.Text = string.Format("{0:yyyy-MM-dd}", trust.ProessDate);
                //}
                if (!string.IsNullOrEmpty(trust.UnitWorkId))
                {
                    this.drpUnitWork.SelectedValue = trust.UnitWorkId;
                }
                if (!string.IsNullOrEmpty(trust.UnitId))
                {
                    this.drpUnitId.SelectedValue = trust.UnitId;
                }
                this.txtProessMethod.Text = trust.ProessMethod;
                this.txtProessEquipment.Text = trust.ProessEquipment;
                if (!string.IsNullOrEmpty(trust.Tabler))
                {
                    this.txtTabler.Text = BLL.UserService.GetUserNameByUserId(trust.Tabler);
                }
                this.txtRemark.Text = trust.Remark;
                this.txtReportNo.Text = trust.ReportNo;
            }
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var trust = BLL.HotProess_TrustService.GetHotProessTrustById(this.HotProessTrustId);
            if (trust != null) {
                trust.ProessMethod = this.txtProessMethod.Text.Trim();
                trust.ProessEquipment = this.txtProessEquipment.Text.Trim();
                trust.ReportNo = this.txtReportNo.Text.Trim();
                BLL.HotProess_TrustService.UpdateHotProessTrust(trust);
                ShowNotify("保存成功！", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript( ActiveWindow.GetHidePostBackReference());
            }
        }

        protected void txtIsoNo_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
    }
}