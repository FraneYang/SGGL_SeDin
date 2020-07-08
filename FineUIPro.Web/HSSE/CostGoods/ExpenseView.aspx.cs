using BLL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FineUIPro.Web.HSSE.CostGoods
{
    public partial class ExpenseView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string ExpenseId
        {
            get
            {
                return (string)ViewState["ExpenseId"];
            }
            set
            {
                ViewState["ExpenseId"] = value;
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();                

                this.ExpenseId = Request.Params["ExpenseId"];
                if (!string.IsNullOrEmpty(this.ExpenseId))
                {
                    Model.CostGoods_Expense expense = BLL.ExpenseService.GetExpenseById(this.ExpenseId);
                    if (expense != null)
                    {
                        this.txtExpenseCode.Text = CodeRecordsService.ReturnCodeByDataId(this.ExpenseId);
                        if (expense.Months != null)
                        {
                            this.txtMonths.Text = string.Format("{0:yyyy-MM}", expense.Months);
                        }
                        if (!string.IsNullOrEmpty(expense.UnitId))
                        {
                            Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(expense.UnitId);
                            if (unit != null)
                            {
                                this.drpUnitId.Text = unit.UnitName;
                            }
                        }
                        if (expense.ReportDate != null)
                        {
                            this.txtReportDate.Text = string.Format("{0:yyyy-MM-dd}", expense.ReportDate);
                        }
                        //this.txtCompileMan.Text = expense.CompileMan;
                        //if (expense.CompileDate != null)
                        //{
                        //    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", expense.CompileDate);
                        //}
                        //this.txtCheckMan.Text = expense.CheckMan;
                        //if (expense.CheckDate != null)
                        //{
                        //    this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", expense.CheckDate);
                        //}
                        //this.txtApproveMan.Text = expense.ApproveMan;
                        //if (expense.ApproveDate != null)
                        //{
                        //    this.txtApproveDate.Text = string.Format("{0:yyyy-MM-dd}", expense.ApproveDate);
                        //}
                        decimal totalA = 0, totalB = 0, totalProjectA = 0, totalProjectB = 0;
                        Model.Base_Project project = BLL.ProjectService.GetProjectByProjectId(expense.ProjectId);
                        List<Model.CostGoods_ExpenseDetail> projectDetails = BLL.ExpenseDetailService.GetCostDetailsByUnitId(expense.UnitId, project != null ? Convert.ToDateTime(project.StartDate) : Convert.ToDateTime("2000-01-01"), Convert.ToDateTime(expense.Months));
                        List<Model.CostGoods_ExpenseDetail> details = BLL.ExpenseDetailService.GetExpenseDetailsByExpenseId(this.ExpenseId);
                        Model.CostGoods_ExpenseDetail a1 = details.FirstOrDefault(x => x.CostType == "A1");
                        if (a1 != null)
                        {
                            this.nbA1.Text = (a1.CostMoney ?? 0).ToString();
                            totalA += Funs.GetNewDecimalOrZero(this.nbA1.Text);
                            this.nbProjectA1.Text = ((from x in projectDetails where x.CostType == "A1" select x.CostMoney ?? 0).Sum() + a1.CostMoney ?? 0).ToString();
                            totalProjectA += Funs.GetNewDecimalOrZero(this.nbProjectA1.Text);
                            this.txtDefA1.Text = a1.CostDef;
                        }
                        Model.CostGoods_ExpenseDetail a2 = details.FirstOrDefault(x => x.CostType == "A2");
                        if (a2 != null)
                        {
                            this.nbA2.Text = (a2.CostMoney ?? 0).ToString();
                            totalA += Funs.GetNewDecimalOrZero(this.nbA2.Text);
                            this.nbProjectA2.Text = ((from x in projectDetails where x.CostType == "A2" select x.CostMoney ?? 0).Sum() + a2.CostMoney ?? 0).ToString();
                            totalProjectA += Funs.GetNewDecimalOrZero(this.nbProjectA2.Text);
                            this.txtDefA2.Text = a2.CostDef;
                        }
                        Model.CostGoods_ExpenseDetail a3 = details.FirstOrDefault(x => x.CostType == "A3");
                        if (a3 != null)
                        {
                            this.nbA3.Text = (a3.CostMoney ?? 0).ToString();
                            totalA += Funs.GetNewDecimalOrZero(this.nbA3.Text);
                            this.nbProjectA3.Text = ((from x in projectDetails where x.CostType == "A3" select x.CostMoney ?? 0).Sum() + a3.CostMoney ?? 0).ToString();
                            totalProjectA += Funs.GetNewDecimalOrZero(this.nbProjectA3.Text);
                            this.txtDefA3.Text = a3.CostDef;
                        }
                        Model.CostGoods_ExpenseDetail a4 = details.FirstOrDefault(x => x.CostType == "A4");
                        if (a4 != null)
                        {
                            this.nbA4.Text = (a4.CostMoney ?? 0).ToString();
                            totalA += Funs.GetNewDecimalOrZero(this.nbA4.Text);
                            this.nbProjectA4.Text = ((from x in projectDetails where x.CostType == "A4" select x.CostMoney ?? 0).Sum() + a4.CostMoney ?? 0).ToString();
                            totalProjectA += Funs.GetNewDecimalOrZero(this.nbProjectA4.Text);
                            this.txtDefA4.Text = a4.CostDef;
                        }
                        Model.CostGoods_ExpenseDetail a5 = details.FirstOrDefault(x => x.CostType == "A5");
                        if (a5 != null)
                        {
                            this.nbA5.Text = (a5.CostMoney ?? 0).ToString();
                            totalA += Funs.GetNewDecimalOrZero(this.nbA5.Text);
                            this.nbProjectA5.Text = ((from x in projectDetails where x.CostType == "A5" select x.CostMoney ?? 0).Sum() + a5.CostMoney ?? 0).ToString();
                            totalProjectA += Funs.GetNewDecimalOrZero(this.nbProjectA5.Text);
                            this.txtDefA5.Text = a5.CostDef;
                        }
                        Model.CostGoods_ExpenseDetail a6 = details.FirstOrDefault(x => x.CostType == "A6");
                        if (a6 != null)
                        {
                            this.nbA6.Text = (a6.CostMoney ?? 0).ToString();
                            totalA += Funs.GetNewDecimalOrZero(this.nbA6.Text);
                            this.nbProjectA6.Text = ((from x in projectDetails where x.CostType == "A6" select x.CostMoney ?? 0).Sum() + a6.CostMoney ?? 0).ToString();
                            totalProjectA += Funs.GetNewDecimalOrZero(this.nbProjectA6.Text);
                            this.txtDefA6.Text = a6.CostDef;
                        }
                        this.nbA.Text = totalA.ToString();
                        this.nbProjectA.Text = totalProjectA.ToString();
                        Model.CostGoods_ExpenseDetail b1 = details.FirstOrDefault(x => x.CostType == "B1");
                        if (b1 != null)
                        {
                            this.nbB1.Text = (b1.CostMoney ?? 0).ToString();
                            totalB += Funs.GetNewDecimalOrZero(this.nbB1.Text);
                            this.nbProjectB1.Text = ((from x in projectDetails where x.CostType == "B1" select x.CostMoney ?? 0).Sum() + b1.CostMoney ?? 0).ToString();
                            totalProjectB += Funs.GetNewDecimalOrZero(this.nbProjectB1.Text);
                            this.txtDefB1.Text = b1.CostDef;
                        }
                        Model.CostGoods_ExpenseDetail b2 = details.FirstOrDefault(x => x.CostType == "B2");
                        if (b2 != null)
                        {
                            this.nbB2.Text = (b2.CostMoney ?? 0).ToString();
                            totalB += Funs.GetNewDecimalOrZero(this.nbB2.Text);
                            this.nbProjectB2.Text = ((from x in projectDetails where x.CostType == "B2" select x.CostMoney ?? 0).Sum() + b2.CostMoney ?? 0).ToString();
                            totalProjectB += Funs.GetNewDecimalOrZero(this.nbProjectB2.Text);
                            this.txtDefB2.Text = b2.CostDef;
                        }
                        Model.CostGoods_ExpenseDetail b3 = details.FirstOrDefault(x => x.CostType == "B3");
                        if (b3 != null)
                        {
                            this.nbB3.Text = (b3.CostMoney ?? 0).ToString();
                            totalB += Funs.GetNewDecimalOrZero(this.nbB3.Text);
                            this.nbProjectB3.Text = ((from x in projectDetails where x.CostType == "B3" select x.CostMoney ?? 0).Sum() + b3.CostMoney ?? 0).ToString();
                            totalProjectB += Funs.GetNewDecimalOrZero(this.nbProjectB3.Text);
                            this.txtDefB3.Text = b3.CostDef;
                        }
                        this.nbB.Text = totalB.ToString();
                        this.nbProjectB.Text = totalProjectB.ToString();
                        this.nbAB.Text = (totalA + totalB).ToString();
                        this.nbProjectAB.Text = (totalProjectA + totalProjectB).ToString();
                    }
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectExpenseMenuId;
                this.ctlAuditFlow.DataId = this.ExpenseId;
            }
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.ExpenseId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ExpenseAttachUrl&menuId={1}", ExpenseId, BLL.Const.ProjectExpenseMenuId)));
            }
        }
        #endregion
    }
}