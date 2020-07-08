using BLL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FineUIPro.Web.HSSE.CostGoods
{
    public partial class CostSmallDetailView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string CostSmallDetailId
        {
            get
            {
                return (string)ViewState["CostSmallDetailId"];
            }
            set
            {
                ViewState["CostSmallDetailId"] = value;
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
                btnClose.OnClientClick = ActiveWindow.GetHideReference();            

                this.CostSmallDetailId = Request.Params["CostSmallDetailId"];
                if (!string.IsNullOrEmpty(this.CostSmallDetailId))
                {
                    Model.CostGoods_CostSmallDetail costSmallDetail = BLL.CostSmallDetailService.GetCostSmallDetailById(this.CostSmallDetailId);
                    if (costSmallDetail != null)
                    {
                        this.txtCostSmallDetailCode.Text = CodeRecordsService.ReturnCodeByDataId(this.CostSmallDetailId);
                        if (costSmallDetail.Months != null)
                        {
                            this.txtMonths.Text = string.Format("{0:yyyy-MM}", costSmallDetail.Months);
                        }
                        if (!string.IsNullOrEmpty(costSmallDetail.UnitId))
                        {
                            Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(costSmallDetail.UnitId);
                            if (unit != null)
                            {
                                this.drpUnitId.Text = unit.UnitName;
                            }
                        }
                        if (costSmallDetail.ReportDate != null)
                        {
                            this.txtReportDate.Text = string.Format("{0:yyyy-MM-dd}", costSmallDetail.ReportDate);
                        }
                        //this.txtCompileMan.Text = costSmallDetail.CompileMan;
                        //if (costSmallDetail.CompileDate != null)
                        //{
                        //    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", costSmallDetail.CompileDate);
                        //}
                        //this.txtCheckMan.Text = costSmallDetail.CheckMan;
                        //if (costSmallDetail.CheckDate != null)
                        //{
                        //    this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", costSmallDetail.CheckDate);
                        //}
                        //this.txtApproveMan.Text = costSmallDetail.ApproveMan;
                        //if (costSmallDetail.ApproveDate != null)
                        //{
                        //    this.txtApproveDate.Text = string.Format("{0:yyyy-MM-dd}", costSmallDetail.ApproveDate);
                        //}
                        decimal totalA = 0, totalB = 0, totalProjectA = 0, totalProjectB = 0;
                        Model.Base_Project project = BLL.ProjectService.GetProjectByProjectId(costSmallDetail.ProjectId);
                        if (project != null && costSmallDetail.Months.HasValue)
                        {
                            List<Model.CostGoods_CostSmallDetailItem> projectDetails = BLL.CostSmallDetailItemService.GetCostDetailsByUnitId(this.CurrUser.LoginProjectId, costSmallDetail.UnitId, project.StartDate, costSmallDetail.Months);
                            List<Model.CostGoods_CostSmallDetailItem> details = BLL.CostSmallDetailItemService.GetCostSmallDetailItemByCostSmallDetailId(this.CostSmallDetailId);
                            Model.CostGoods_CostSmallDetailItem a1 = details.FirstOrDefault(x => x.CostType == "A1");
                            if (a1 != null)
                            {
                                this.nbA1.Text = (a1.CostMoney ?? 0).ToString();
                                totalA += Funs.GetNewDecimalOrZero(this.nbA1.Text);
                                this.nbProjectA1.Text = ((from x in projectDetails where x.CostType == "A1" select x.CostMoney ?? 0).Sum() + a1.CostMoney ?? 0).ToString();
                                totalProjectA += Funs.GetNewDecimalOrZero(this.nbProjectA1.Text);
                                this.txtDefA1.Text = a1.CostDef;
                            }
                            Model.CostGoods_CostSmallDetailItem a2 = details.FirstOrDefault(x => x.CostType == "A2");
                            if (a2 != null)
                            {
                                this.nbA2.Text = (a2.CostMoney ?? 0).ToString();
                                totalA += Funs.GetNewDecimalOrZero(this.nbA2.Text);
                                this.nbProjectA2.Text = ((from x in projectDetails where x.CostType == "A2" select x.CostMoney ?? 0).Sum() + a2.CostMoney ?? 0).ToString();
                                totalProjectA += Funs.GetNewDecimalOrZero(this.nbProjectA2.Text);
                                this.txtDefA2.Text = a2.CostDef;
                            }
                            Model.CostGoods_CostSmallDetailItem a3 = details.FirstOrDefault(x => x.CostType == "A3");
                            if (a3 != null)
                            {
                                this.nbA3.Text = (a3.CostMoney ?? 0).ToString();
                                totalA += Funs.GetNewDecimalOrZero(this.nbA3.Text);
                                this.nbProjectA3.Text = ((from x in projectDetails where x.CostType == "A3" select x.CostMoney ?? 0).Sum() + a3.CostMoney ?? 0).ToString();
                                totalProjectA += Funs.GetNewDecimalOrZero(this.nbProjectA3.Text);
                                this.txtDefA3.Text = a3.CostDef;
                            }
                            Model.CostGoods_CostSmallDetailItem a4 = details.FirstOrDefault(x => x.CostType == "A4");
                            if (a4 != null)
                            {
                                this.nbA4.Text = (a4.CostMoney ?? 0).ToString();
                                totalA += Funs.GetNewDecimalOrZero(this.nbA4.Text);
                                this.nbProjectA4.Text = ((from x in projectDetails where x.CostType == "A4" select x.CostMoney ?? 0).Sum() + a4.CostMoney ?? 0).ToString();
                                totalProjectA += Funs.GetNewDecimalOrZero(this.nbProjectA4.Text);
                                this.txtDefA4.Text = a4.CostDef;
                            }
                            Model.CostGoods_CostSmallDetailItem a5 = details.FirstOrDefault(x => x.CostType == "A5");
                            if (a5 != null)
                            {
                                this.nbA5.Text = (a5.CostMoney ?? 0).ToString();
                                totalA += Funs.GetNewDecimalOrZero(this.nbA5.Text);
                                this.nbProjectA5.Text = ((from x in projectDetails where x.CostType == "A5" select x.CostMoney ?? 0).Sum() + a5.CostMoney ?? 0).ToString();
                                totalProjectA += Funs.GetNewDecimalOrZero(this.nbProjectA5.Text);
                                this.txtDefA5.Text = a5.CostDef;
                            }
                            Model.CostGoods_CostSmallDetailItem a6 = details.FirstOrDefault(x => x.CostType == "A6");
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
                            Model.CostGoods_CostSmallDetailItem b1 = details.FirstOrDefault(x => x.CostType == "B1");
                            if (b1 != null)
                            {
                                this.nbB1.Text = (b1.CostMoney ?? 0).ToString();
                                totalB += Funs.GetNewDecimalOrZero(this.nbB1.Text);
                                this.nbProjectB1.Text = ((from x in projectDetails where x.CostType == "B1" select x.CostMoney ?? 0).Sum() + b1.CostMoney ?? 0).ToString();
                                totalProjectB += Funs.GetNewDecimalOrZero(this.nbProjectB1.Text);
                                this.txtDefB1.Text = b1.CostDef;
                            }
                            Model.CostGoods_CostSmallDetailItem b2 = details.FirstOrDefault(x => x.CostType == "B2");
                            if (b2 != null)
                            {
                                this.nbB2.Text = (b2.CostMoney ?? 0).ToString();
                                totalB += Funs.GetNewDecimalOrZero(this.nbB2.Text);
                                this.nbProjectB2.Text = ((from x in projectDetails where x.CostType == "B2" select x.CostMoney ?? 0).Sum() + b2.CostMoney ?? 0).ToString();
                                totalProjectB += Funs.GetNewDecimalOrZero(this.nbProjectB2.Text);
                                this.txtDefB2.Text = b2.CostDef;
                            }
                            Model.CostGoods_CostSmallDetailItem b3 = details.FirstOrDefault(x => x.CostType == "B3");
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
                    this.ctlAuditFlow.MenuId = BLL.Const.ProjectCostSmallDetailMenuId;
                    this.ctlAuditFlow.DataId = this.CostSmallDetailId;
                }
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
            if (!string.IsNullOrEmpty(this.CostSmallDetailId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CostSmallDetailAttachUrl&menuId={1}", this.CostSmallDetailId, BLL.Const.ProjectCostSmallDetailMenuId)));
            }
        }
        #endregion
    }
}