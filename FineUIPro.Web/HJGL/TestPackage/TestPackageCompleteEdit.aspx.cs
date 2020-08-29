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
                if (getTestpackage != null) {
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
            string strSql = @"SELECT * FROM dbo.View_PTP_TestPackageAudit
                             WHERE ProjectId= @ProjectId AND PTP_ID=@PTP_ID";
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
            this.ShowGridItem();
        }

        /// <summary>
        /// 行颜色设置
        /// </summary>
        private void ShowGridItem()
        {
            Count = 0;
            int Count1 = 0, Count2 = 0, Count3 = 0, Count4 = 0;
            int rowsCount = this.Grid1.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                int IsoInfoCount = Funs.GetNewIntOrZero(this.Grid1.Rows[i].Values[3].ToString()); //总焊口
                int IsoInfoCountT = Funs.GetNewIntOrZero(this.Grid1.Rows[i].Values[4].ToString()); //完成总焊口
                int CountS = Funs.GetNewIntOrZero(this.Grid1.Rows[i].Values[5].ToString()); ; //合格数
                int CountU = Funs.GetNewIntOrZero(this.Grid1.Rows[i].Values[6].ToString()); ; //不合格数
                decimal Rate = 0;
                bool convertible = decimal.TryParse(this.Grid1.Rows[i].Values[9].ToString(), out Rate); //应检测比例
                decimal Ratio = Funs.GetNewDecimalOrZero(this.Grid1.Rows[i].Values[10].ToString()); //实际检测比例

                if (IsoInfoCount > IsoInfoCountT) //未焊完
                {
                    Count1 += 1;
                    this.Grid1.Rows[i].RowCssClass = "Cyan";
                }
                else if (Rate > Ratio) //已焊完，未达检测比例
                {
                    Count2 += 1;
                    this.Grid1.Rows[i].RowCssClass = "Yellow";
                }
                else if (CountU > 0) //已焊完，已达检测比例，但有不合格
                {
                    Count3 += 1;
                    this.Grid1.Rows[i].RowCssClass = "Green";
                }
                else
                {
                    Count4 += 1;
                    this.Grid1.Rows[i].RowCssClass = "Purple";
                }
            }

            Count = Count1 + Count2 + Count2;
            this.lab1.Text = Count1.ToString();
            this.lab2.Text = Count2.ToString();
            this.lab3.Text = Count3.ToString();
            this.lab4.Text = Count4.ToString();
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
                this.txtRemark.Text = testPackageManage.Remark;
                this.txtadjustTestPressure.Text = testPackageManage.AdjustTestPressure;
                this.txtTestMediumTemperature.Text = testPackageManage.TestMediumTemperature.ToString();
                this.txtAmbientTemperature.Text = testPackageManage.AmbientTemperature.ToString();
                this.txtHoldingTime.Text = testPackageManage.HoldingTime.ToString();
                this.txtFinishDef.Text = testPackageManage.FinishDef;
                txtInstallationSpecification.Text = testPackageManage.Check1;
                txtPressureTest.Text = testPackageManage.Check2;
                txtWorkRecord.Text = testPackageManage.Check3;
                txtNDTConform.Text = testPackageManage.Check4;
                txtHotConform.Text = testPackageManage.Check5;
                txtInstallationCorrectness.Text = testPackageManage.Check6;
                txtMarkClearly.Text = testPackageManage.Check7;
                txtIsolationOpening.Text = testPackageManage.Check8;
                txtConstructionPlanAsk.Text = testPackageManage.Check9;
                txtCover.Text = testPackageManage.Check10;
                txtMeetRequirements.Text = testPackageManage.Check11;
                txtStainlessTestWater.Text = testPackageManage.Check12;
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
                        updateTestPackage.AmbientTemperature =Funs.GetNewDecimal(this.txtAmbientTemperature.Text.Trim());
                        BLL.TestPackageEditService.UpdateTestPackage(updateTestPackage);
                        this.BindGrid();
                        ShowNotify("保存成功！", MessageBoxIcon.Success);
                }
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 取消审核检测单
        /// <summary>
        /// 取消审核检测单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelAudit_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.TestPackageCompleteMenuId, Const.BtnCancelAuditing))
            {
                var updateTestPackage = BLL.TestPackageEditService.GetTestPackageByID(this.PTP_ID);
                if (updateTestPackage != null)
                {
                    updateTestPackage.Finisher = null;
                    updateTestPackage.FinishDate = null;
                    //updateTestPackage.FinishDef = this.txtFinishDef.Text.Trim();
                    BLL.TestPackageAuditService.AuditFinishDef(updateTestPackage);
                    this.BindGrid();
                    ShowNotify("取消审核完成！", MessageBoxIcon.Success);
                }
                else
                {
                    ShowNotify("请确认单据！", MessageBoxIcon.Warning);
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
