using System;
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
    public partial class TestPackageCompleteEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 试压包主键
        /// </summary>
        public string PTP_ID
        {
            get
            {
                return (string)ViewState["PTP_ID"];
            }
            set
            {
                ViewState["PTP_ID"] = value;
            }
        }
        /// <summary>
        /// 未通过数
        /// </summary>
        public int Count
        {
            get
            {
                return (int)ViewState["Count"];
            }
            set
            {
                ViewState["Count"] = value;
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
                this.ddlPageSize.SelectedValue = this.Grid1.PageSize.ToString();
                this.PTP_ID = Request.Params["PTP_ID"];
                var getTestpackage = TestPackageEditService.GetTestPackageByID(PTP_ID);
                if (getTestpackage != null)
                {
                    BindGrid();
                }
            }
        }
        #endregion
        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            this.PageInfoLoad(); ///页面输入保存信息
            string strSql = @" SELECT ptpPipe.PT_PipeId, ptpPipe.PTP_ID, ptpPipe.PipelineId, ptpPipe.DesignPress, 
                               ptpPipe.DesignTemperature, ptpPipe.AmbientTemperature, ptpPipe.TestMedium, 
                               ptpPipe.TestMediumTemperature, ptpPipe.TestPressure, ptpPipe.HoldingTime,IsoInfo.PipelineCode,testMedium.MediumName
                               FROM dbo.PTP_PipelineList AS ptpPipe 
                               LEFT JOIN dbo.HJGL_Pipeline AS IsoInfo ON  ptpPipe.PipelineId = IsoInfo.PipelineId
							   LEFT JOIN dbo.Base_TestMedium  AS testMedium ON testMedium.TestMediumId = IsoInfo.TestMedium
                               WHERE  ptpPipe.PTP_ID=@PTP_ID";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            listStr.Add(new SqlParameter("@PTP_ID", this.PTP_ID));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(Grid1, tb1);
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        #region 加载页面输入保存信息
        /// <summary>
        /// 加载页面输入保存信息
        /// </summary>
        private void PageInfoLoad()
        {
            var testPackageManage = BLL.TestPackageEditService.GetTestPackageByID(this.PTP_ID);
            if (testPackageManage != null)
            {
                this.txtTestPackageNo.Text = testPackageManage.TestPackageNo;
                this.txtTestPackageName.Text = testPackageManage.TestPackageName;
                this.txtadjustTestPressure.Text = testPackageManage.AdjustTestPressure;
                this.txtAmbientTemperature.Text = testPackageManage.AmbientTemperature.ToString();
                this.txtFinishDef.Text = testPackageManage.FinishDef;
                this.txtHoldingTime.Text = testPackageManage.HoldingTime.ToString();
                this.txtTestDate.Text = testPackageManage.TestDate?.ToString("yyyy-MM-dd");
                this.txtTestMediumTemperature.Text = testPackageManage.TestMediumTemperature.ToString();
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

        #region 试压包 维护事件
        #region 试压包完成
        /// <summary>
        /// 审核检测单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAudit_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.TestPackageCompleteMenuId, Const.BtnTestComplete))
            {
                var updateTestPackage = BLL.TestPackageEditService.GetTestPackageByID(this.PTP_ID);
                if (updateTestPackage != null)
                {
                    updateTestPackage.FinishDate = DateTime.Now;
                    updateTestPackage.Finisher = this.CurrUser.UserId;
                    updateTestPackage.FinishDef = this.txtFinishDef.Text.Trim();
                    updateTestPackage.TestMediumTemperature = Funs.GetNewDecimal(txtTestMediumTemperature.Text.Trim());
                    updateTestPackage.HoldingTime = Funs.GetNewDecimal(txtHoldingTime.Text.Trim());
                    updateTestPackage.AmbientTemperature = Funs.GetNewDecimal(this.txtAmbientTemperature.Text.Trim());
                    updateTestPackage.TestDate =Funs.GetNewDateTime(txtTestDate.Text.Trim());
                    BLL.TestPackageEditService.UpdateTestPackage(updateTestPackage);
                    this.BindGrid();
                    ShowNotify("保存成功！", MessageBoxIcon.Success);
                    PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(this.PTP_ID)
                  + ActiveWindow.GetHidePostBackReference());
                }
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }
        #endregion
        #endregion

        #region 关闭弹出窗口及刷新页面
        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            this.BindGrid();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Tree_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion        

        #region  试压包打印
        /// <summary>
        ///  试压包打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.PTP_ID))
            {
                //string reportId = BLL.Const.HJGL_TrustReportId; // 试压包打印  待做模板                             
                //PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("../../Common/ReportPrint/ExReportPrint.aspx?ispop=1&reportId={0}&replaceParameter={1}&varValue={2}", reportId, this.PTP_ID, string.Empty, "打印 - ")));
            }
            else
            {
                ShowNotify("请选择无损委托记录！", MessageBoxIcon.Warning);
                return;
            }
        }
        #endregion
    }
}
