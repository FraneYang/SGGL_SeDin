using BLL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FineUIPro.Web.HSSE.CostGoods
{
    public partial class CostSmallDetailEdit : PageBase
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
        /// <summary>
        /// 项目主键
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.hdProjectA1.Text = "0";
                this.hdProjectA2.Text = "0";
                this.hdProjectA3.Text = "0";
                this.hdProjectA4.Text = "0";
                this.hdProjectA5.Text = "0";
                this.hdProjectA6.Text = "0";
                this.hdProjectB1.Text = "0";
                this.hdProjectB2.Text = "0";
                this.hdProjectB3.Text = "0";
                this.InitDropDownList();
                this.CostSmallDetailId = Request.Params["CostSmallDetailId"];
                if (!string.IsNullOrEmpty(this.CostSmallDetailId))
                {
                    Model.CostGoods_CostSmallDetail costSmallDetail = BLL.CostSmallDetailService.GetCostSmallDetailById(this.CostSmallDetailId);
                    if (costSmallDetail != null)
                    {
                        this.ProjectId = costSmallDetail.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        if (costSmallDetail.Months != null)
                        {
                            //this.txtMonths.Text = string.Format("{0:yyyy-MM}", costSmallDetail.Months);
                            this.drpYear.SelectedValue = Convert.ToString(costSmallDetail.Months.Value.Year);
                            this.drpMonths.SelectedValue = Convert.ToString(costSmallDetail.Months.Value.Month);
                        }
                        this.txtCostSmallDetailCode.Text = CodeRecordsService.ReturnCodeByDataId(this.CostSmallDetailId);
                        if (!string.IsNullOrEmpty(costSmallDetail.UnitId))
                        {
                            this.drpUnitId.SelectedValue = costSmallDetail.UnitId;
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
                        Model.Base_Project project = BLL.ProjectService.GetProjectByProjectId(this.ProjectId);
                        if (project != null && costSmallDetail.Months.HasValue)
                        {
                            List<Model.CostGoods_CostSmallDetailItem> projectDetails = BLL.CostSmallDetailItemService.GetCostDetailsByUnitId(this.CurrUser.LoginProjectId, costSmallDetail.UnitId, project != null ? project.StartDate : null, Convert.ToDateTime(costSmallDetail.Months));
                            List<Model.CostGoods_CostSmallDetailItem> details = BLL.CostSmallDetailItemService.GetCostSmallDetailItemByCostSmallDetailId(this.CostSmallDetailId);
                            Model.CostGoods_CostSmallDetailItem a1 = details.FirstOrDefault(x => x.CostType == "A1");
                            this.hdProjectA1.Text = (from x in projectDetails where x.CostType == "A1" select x.CostMoney ?? 0).Sum().ToString();
                            this.hdProjectA2.Text = (from x in projectDetails where x.CostType == "A2" select x.CostMoney ?? 0).Sum().ToString();
                            this.hdProjectA3.Text = (from x in projectDetails where x.CostType == "A3" select x.CostMoney ?? 0).Sum().ToString();
                            this.hdProjectA4.Text = (from x in projectDetails where x.CostType == "A4" select x.CostMoney ?? 0).Sum().ToString();
                            this.hdProjectA5.Text = (from x in projectDetails where x.CostType == "A5" select x.CostMoney ?? 0).Sum().ToString();
                            this.hdProjectA6.Text = (from x in projectDetails where x.CostType == "A6" select x.CostMoney ?? 0).Sum().ToString();
                            this.hdProjectB1.Text = (from x in projectDetails where x.CostType == "B1" select x.CostMoney ?? 0).Sum().ToString();
                            this.hdProjectB2.Text = (from x in projectDetails where x.CostType == "B2" select x.CostMoney ?? 0).Sum().ToString();
                            this.hdProjectB3.Text = (from x in projectDetails where x.CostType == "B3" select x.CostMoney ?? 0).Sum().ToString();
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
                }
                else
                {
                    this.drpYear.SelectedValue = Convert.ToString(DateTime.Now.Year);
                    this.drpMonths.SelectedValue = Convert.ToString(DateTime.Now.Month);
                    //this.txtMonths.Text = DateTime.Now.ToString("yyyy-MM");
                    //this.txtCompileMan.Text = this.CurrUser.UserName;
                    this.txtReportDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    //this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtCostSmallDetailCode.Text = CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectCostSmallDetailMenuId, this.ProjectId, this.CurrUser.UnitId);
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectCostSmallDetailMenuId;
                this.ctlAuditFlow.DataId = this.CostSmallDetailId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            this.drpUnitId.DataValueField = "UnitId";
            this.drpUnitId.DataTextField = "UnitName";
            this.drpUnitId.DataSource = BLL.UnitService.GetMainAndSubUnitByProjectIdList(this.ProjectId);
            this.drpUnitId.DataBind();
            Funs.FineUIPleaseSelect(this.drpUnitId);            
            BLL.ConstValue.InitConstValueDropDownList(this.drpYear, BLL.ConstValue.Group_0008, true);
            BLL.ConstValue.InitConstValueDropDownList(this.drpMonths, BLL.ConstValue.Group_0009, true);
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.drpYear.SelectedValue == BLL.Const._Null || this.drpMonths.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择月份", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.drpYear.SelectedValue == BLL.Const._Null || this.drpMonths.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择月份", MessageBoxIcon.Warning);
                return;
            }
            if (this.ctlAuditFlow.NextStep == BLL.Const.State_1 && this.ctlAuditFlow.NextPerson == BLL.Const._Null)
            {
                ShowNotify("请选择下一步办理人！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.CostGoods_CostSmallDetail costSmallDetail = new Model.CostGoods_CostSmallDetail
            {
                ProjectId = this.ProjectId
            };
            //if (!string.IsNullOrEmpty(this.txtMonths.Text))
            //{
            //    costSmallDetail.Months = Funs.GetNewDateTime(this.txtMonths.Text + "-01");
            //}
            if (this.drpYear.SelectedValue != BLL.Const._Null && this.drpMonths.SelectedValue != BLL.Const._Null)
            {
                costSmallDetail.Months = Funs.GetNewDateTime(this.drpYear.SelectedValue + "-" + this.drpMonths.SelectedValue);
            }
            costSmallDetail.CostSmallDetailCode = this.txtCostSmallDetailCode.Text.Trim();
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                costSmallDetail.UnitId = this.drpUnitId.SelectedValue;
            }
            costSmallDetail.ReportDate = Funs.GetNewDateTime(this.txtReportDate.Text.Trim());
          
            //costSmallDetail.CheckMan = this.txtCheckMan.Text;
            //costSmallDetail.CheckDate = Funs.GetNewDateTime(this.txtCheckDate.Text.Trim());
            //costSmallDetail.ApproveMan = this.txtApproveMan.Text;
            //costSmallDetail.ApproveDate = Funs.GetNewDateTime(this.txtApproveDate.Text.Trim());
            costSmallDetail.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                costSmallDetail.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.CostSmallDetailId))
            {
                costSmallDetail.CostSmallDetailId = this.CostSmallDetailId;
                BLL.CostSmallDetailService.UpdateCostSmallDetail(costSmallDetail);
                BLL.LogService.AddSys_Log(this.CurrUser, costSmallDetail.CostSmallDetailCode, costSmallDetail.CostSmallDetailId,BLL.Const.ProjectCostSmallDetailMenuId,BLL.Const.BtnModify);
                BLL.CostSmallDetailItemService.DeleteCostSmallDetailItemByCostSmallDetailId(this.CostSmallDetailId);
            }
            else
            {
                this.CostSmallDetailId = SQLHelper.GetNewID(typeof(Model.CostGoods_CostSmallDetail));
                costSmallDetail.CostSmallDetailId = this.CostSmallDetailId;
                //costSmallDetail.CompileMan = this.CurrUser.UserName;
                costSmallDetail.CompileDate = DateTime.Now;
                BLL.CostSmallDetailService.AddCostSmallDetail(costSmallDetail);
                BLL.LogService.AddSys_Log(this.CurrUser, costSmallDetail.CostSmallDetailCode, costSmallDetail.CostSmallDetailId, BLL.Const.ProjectCostSmallDetailMenuId, BLL.Const.BtnAdd);
            }
            //保存费用明细
            BLL.CostSmallDetailItemService.AddCostDetail(costSmallDetail.CostSmallDetailId, "A1", Funs.GetNewDecimalOrZero(this.nbA1.Text), this.txtDefA1.Text.Trim());
            BLL.CostSmallDetailItemService.AddCostDetail(costSmallDetail.CostSmallDetailId, "A2", Funs.GetNewDecimalOrZero(this.nbA2.Text), this.txtDefA2.Text.Trim());
            BLL.CostSmallDetailItemService.AddCostDetail(costSmallDetail.CostSmallDetailId, "A3", Funs.GetNewDecimalOrZero(this.nbA3.Text), this.txtDefA3.Text.Trim());
            BLL.CostSmallDetailItemService.AddCostDetail(costSmallDetail.CostSmallDetailId, "A4", Funs.GetNewDecimalOrZero(this.nbA4.Text), this.txtDefA4.Text.Trim());
            BLL.CostSmallDetailItemService.AddCostDetail(costSmallDetail.CostSmallDetailId, "A5", Funs.GetNewDecimalOrZero(this.nbA5.Text), this.txtDefA5.Text.Trim());
            BLL.CostSmallDetailItemService.AddCostDetail(costSmallDetail.CostSmallDetailId, "A6", Funs.GetNewDecimalOrZero(this.nbA6.Text), this.txtDefA6.Text.Trim());
            BLL.CostSmallDetailItemService.AddCostDetail(costSmallDetail.CostSmallDetailId, "B1", Funs.GetNewDecimalOrZero(this.nbB1.Text), this.txtDefB1.Text.Trim());
            BLL.CostSmallDetailItemService.AddCostDetail(costSmallDetail.CostSmallDetailId, "B2", Funs.GetNewDecimalOrZero(this.nbB2.Text), this.txtDefB2.Text.Trim());
            BLL.CostSmallDetailItemService.AddCostDetail(costSmallDetail.CostSmallDetailId, "B3", Funs.GetNewDecimalOrZero(this.nbB3.Text), this.txtDefB3.Text.Trim());
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectCostSmallDetailMenuId, this.CostSmallDetailId, (type == BLL.Const.BtnSubmit ? true : false), costSmallDetail.CostSmallDetailCode, "../CostGoods/CostSmallDetailView.aspx?CostSmallDetailId={0}");
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
            if (string.IsNullOrEmpty(this.CostSmallDetailId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CostSmallDetailAttachUrl&menuId={1}", this.CostSmallDetailId, BLL.Const.ProjectCostSmallDetailMenuId)));
        }
        #endregion

        #region 输入框变化事件
        /// <summary>
        /// 输入框变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void nbA1_TextChanged(object sender, EventArgs e)
        {
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                this.nbProjectA1.Text = (Funs.GetNewDecimalOrZero(this.hdProjectA1.Text) + Funs.GetNewDecimalOrZero(this.nbA1.Text)).ToString();
                this.nbA.Text = (Funs.GetNewDecimalOrZero(this.nbA1.Text) + Funs.GetNewDecimalOrZero(this.nbA2.Text) + Funs.GetNewDecimalOrZero(this.nbA3.Text) + Funs.GetNewDecimalOrZero(this.nbA4.Text) + Funs.GetNewDecimalOrZero(this.nbA5.Text) + Funs.GetNewDecimalOrZero(this.nbA6.Text)).ToString();
                this.nbProjectA.Text = (Funs.GetNewDecimalOrZero(this.nbProjectA1.Text) + Funs.GetNewDecimalOrZero(this.nbProjectA2.Text) + Funs.GetNewDecimalOrZero(this.nbProjectA3.Text) + Funs.GetNewDecimalOrZero(this.nbProjectA4.Text) + Funs.GetNewDecimalOrZero(this.nbProjectA5.Text) + Funs.GetNewDecimalOrZero(this.nbProjectA6.Text)).ToString();
                this.nbAB.Text = (Funs.GetNewDecimalOrZero(this.nbA.Text) + Funs.GetNewDecimalOrZero(this.nbB.Text)).ToString();
                this.nbProjectAB.Text = (Funs.GetNewDecimalOrZero(this.nbProjectA.Text) + Funs.GetNewDecimalOrZero(this.nbProjectB.Text)).ToString();
            }
            else
            {
                this.nbA1.Text = string.Empty;
                Alert.ShowInTop("请先选择填报单位！", MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 输入框变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void nbA2_TextChanged(object sender, EventArgs e)
        {
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                this.nbProjectA2.Text = (Funs.GetNewDecimalOrZero(this.hdProjectA2.Text) + Funs.GetNewDecimalOrZero(this.nbA2.Text)).ToString();
                this.nbA.Text = (Funs.GetNewDecimalOrZero(this.nbA1.Text) + Funs.GetNewDecimalOrZero(this.nbA2.Text) + Funs.GetNewDecimalOrZero(this.nbA3.Text) + Funs.GetNewDecimalOrZero(this.nbA4.Text) + Funs.GetNewDecimalOrZero(this.nbA5.Text) + Funs.GetNewDecimalOrZero(this.nbA6.Text)).ToString();
                this.nbProjectA.Text = (Funs.GetNewDecimalOrZero(this.nbProjectA1.Text) + Funs.GetNewDecimalOrZero(this.nbProjectA2.Text) + Funs.GetNewDecimalOrZero(this.nbProjectA3.Text) + Funs.GetNewDecimalOrZero(this.nbProjectA4.Text) + Funs.GetNewDecimalOrZero(this.nbProjectA5.Text) + Funs.GetNewDecimalOrZero(this.nbProjectA6.Text)).ToString();
                this.nbAB.Text = (Funs.GetNewDecimalOrZero(this.nbA.Text) + Funs.GetNewDecimalOrZero(this.nbB.Text)).ToString();
                this.nbProjectAB.Text = (Funs.GetNewDecimalOrZero(this.nbProjectA.Text) + Funs.GetNewDecimalOrZero(this.nbProjectB.Text)).ToString();
            }
            else
            {
                this.nbA2.Text = string.Empty;
                Alert.ShowInTop("请先选择填报单位！", MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 输入框变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void nbA3_TextChanged(object sender, EventArgs e)
        {
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                this.nbProjectA3.Text = (Funs.GetNewDecimalOrZero(this.hdProjectA3.Text) + Funs.GetNewDecimalOrZero(this.nbA3.Text)).ToString();
                this.nbA.Text = (Funs.GetNewDecimalOrZero(this.nbA1.Text) + Funs.GetNewDecimalOrZero(this.nbA2.Text) + Funs.GetNewDecimalOrZero(this.nbA3.Text) + Funs.GetNewDecimalOrZero(this.nbA4.Text) + Funs.GetNewDecimalOrZero(this.nbA5.Text) + Funs.GetNewDecimalOrZero(this.nbA6.Text)).ToString();
                this.nbProjectA.Text = (Funs.GetNewDecimalOrZero(this.nbProjectA1.Text) + Funs.GetNewDecimalOrZero(this.nbProjectA2.Text) + Funs.GetNewDecimalOrZero(this.nbProjectA3.Text) + Funs.GetNewDecimalOrZero(this.nbProjectA4.Text) + Funs.GetNewDecimalOrZero(this.nbProjectA5.Text) + Funs.GetNewDecimalOrZero(this.nbProjectA6.Text)).ToString();
                this.nbAB.Text = (Funs.GetNewDecimalOrZero(this.nbA.Text) + Funs.GetNewDecimalOrZero(this.nbB.Text)).ToString();
                this.nbProjectAB.Text = (Funs.GetNewDecimalOrZero(this.nbProjectA.Text) + Funs.GetNewDecimalOrZero(this.nbProjectB.Text)).ToString();
            }
            else
            {
                this.nbA3.Text = string.Empty;
                Alert.ShowInTop("请先选择填报单位！", MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 输入框变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void nbA4_TextChanged(object sender, EventArgs e)
        {
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                this.nbProjectA4.Text = (Funs.GetNewDecimalOrZero(this.hdProjectA4.Text) + Funs.GetNewDecimalOrZero(this.nbA4.Text)).ToString();
                this.nbA.Text = (Funs.GetNewDecimalOrZero(this.nbA1.Text) + Funs.GetNewDecimalOrZero(this.nbA2.Text) + Funs.GetNewDecimalOrZero(this.nbA3.Text) + Funs.GetNewDecimalOrZero(this.nbA4.Text) + Funs.GetNewDecimalOrZero(this.nbA5.Text) + Funs.GetNewDecimalOrZero(this.nbA6.Text)).ToString();
                this.nbProjectA.Text = (Funs.GetNewDecimalOrZero(this.nbProjectA1.Text) + Funs.GetNewDecimalOrZero(this.nbProjectA2.Text) + Funs.GetNewDecimalOrZero(this.nbProjectA3.Text) + Funs.GetNewDecimalOrZero(this.nbProjectA4.Text) + Funs.GetNewDecimalOrZero(this.nbProjectA5.Text) + Funs.GetNewDecimalOrZero(this.nbProjectA6.Text)).ToString();
                this.nbAB.Text = (Funs.GetNewDecimalOrZero(this.nbA.Text) + Funs.GetNewDecimalOrZero(this.nbB.Text)).ToString();
                this.nbProjectAB.Text = (Funs.GetNewDecimalOrZero(this.nbProjectA.Text) + Funs.GetNewDecimalOrZero(this.nbProjectB.Text)).ToString();
            }
            else
            {
                this.nbA4.Text = string.Empty;
                Alert.ShowInTop("请先选择填报单位！", MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 输入框变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void nbA5_TextChanged(object sender, EventArgs e)
        {
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                this.nbProjectA5.Text = (Funs.GetNewDecimalOrZero(this.hdProjectA5.Text) + Funs.GetNewDecimalOrZero(this.nbA5.Text)).ToString();
                this.nbA.Text = (Funs.GetNewDecimalOrZero(this.nbA1.Text) + Funs.GetNewDecimalOrZero(this.nbA2.Text) + Funs.GetNewDecimalOrZero(this.nbA3.Text) + Funs.GetNewDecimalOrZero(this.nbA4.Text) + Funs.GetNewDecimalOrZero(this.nbA5.Text) + Funs.GetNewDecimalOrZero(this.nbA6.Text)).ToString();
                this.nbProjectA.Text = (Funs.GetNewDecimalOrZero(this.nbProjectA1.Text) + Funs.GetNewDecimalOrZero(this.nbProjectA2.Text) + Funs.GetNewDecimalOrZero(this.nbProjectA3.Text) + Funs.GetNewDecimalOrZero(this.nbProjectA4.Text) + Funs.GetNewDecimalOrZero(this.nbProjectA5.Text) + Funs.GetNewDecimalOrZero(this.nbProjectA6.Text)).ToString();
                this.nbAB.Text = (Funs.GetNewDecimalOrZero(this.nbA.Text) + Funs.GetNewDecimalOrZero(this.nbB.Text)).ToString();
                this.nbProjectAB.Text = (Funs.GetNewDecimalOrZero(this.nbProjectA.Text) + Funs.GetNewDecimalOrZero(this.nbProjectB.Text)).ToString();
            }
            else
            {
                this.nbA5.Text = string.Empty;
                Alert.ShowInTop("请先选择填报单位！", MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 输入框变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void nbA6_TextChanged(object sender, EventArgs e)
        {
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                this.nbProjectA6.Text = (Funs.GetNewDecimalOrZero(this.hdProjectA6.Text) + Funs.GetNewDecimalOrZero(this.nbA6.Text)).ToString();
                this.nbA.Text = (Funs.GetNewDecimalOrZero(this.nbA1.Text) + Funs.GetNewDecimalOrZero(this.nbA2.Text) + Funs.GetNewDecimalOrZero(this.nbA3.Text) + Funs.GetNewDecimalOrZero(this.nbA4.Text) + Funs.GetNewDecimalOrZero(this.nbA5.Text) + Funs.GetNewDecimalOrZero(this.nbA6.Text)).ToString();
                this.nbProjectA.Text = (Funs.GetNewDecimalOrZero(this.nbProjectA1.Text) + Funs.GetNewDecimalOrZero(this.nbProjectA2.Text) + Funs.GetNewDecimalOrZero(this.nbProjectA3.Text) + Funs.GetNewDecimalOrZero(this.nbProjectA4.Text) + Funs.GetNewDecimalOrZero(this.nbProjectA5.Text) + Funs.GetNewDecimalOrZero(this.nbProjectA6.Text)).ToString();
                this.nbAB.Text = (Funs.GetNewDecimalOrZero(this.nbA.Text) + Funs.GetNewDecimalOrZero(this.nbB.Text)).ToString();
                this.nbProjectAB.Text = (Funs.GetNewDecimalOrZero(this.nbProjectA.Text) + Funs.GetNewDecimalOrZero(this.nbProjectB.Text)).ToString();
            }
            else
            {
                this.nbA6.Text = string.Empty;
                Alert.ShowInTop("请先选择填报单位！", MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 输入框变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void nbB1_TextChanged(object sender, EventArgs e)
        {
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                this.nbProjectB1.Text = (Funs.GetNewDecimalOrZero(this.hdProjectB1.Text) + Funs.GetNewDecimalOrZero(this.nbB1.Text)).ToString();
                this.nbB.Text = (Funs.GetNewDecimalOrZero(this.nbB1.Text) + Funs.GetNewDecimalOrZero(this.nbB2.Text) + Funs.GetNewDecimalOrZero(this.nbB3.Text)).ToString();
                this.nbProjectB.Text = (Funs.GetNewDecimalOrZero(this.nbProjectB1.Text) + Funs.GetNewDecimalOrZero(this.nbProjectB2.Text) + Funs.GetNewDecimalOrZero(this.nbProjectB3.Text)).ToString();
                this.nbAB.Text = (Funs.GetNewDecimalOrZero(this.nbA.Text) + Funs.GetNewDecimalOrZero(this.nbB.Text)).ToString();
                this.nbProjectAB.Text = (Funs.GetNewDecimalOrZero(this.nbProjectA.Text) + Funs.GetNewDecimalOrZero(this.nbProjectB.Text)).ToString();
            }
            else
            {
                this.nbB1.Text = string.Empty;
                Alert.ShowInTop("请先选择填报单位！", MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 输入框变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void nbB2_TextChanged(object sender, EventArgs e)
        {
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                this.nbProjectB2.Text = (Funs.GetNewDecimalOrZero(this.hdProjectB2.Text) + Funs.GetNewDecimalOrZero(this.nbB2.Text)).ToString();
                this.nbB.Text = (Funs.GetNewDecimalOrZero(this.nbB1.Text) + Funs.GetNewDecimalOrZero(this.nbB2.Text) + Funs.GetNewDecimalOrZero(this.nbB3.Text)).ToString();
                this.nbProjectB.Text = (Funs.GetNewDecimalOrZero(this.nbProjectB1.Text) + Funs.GetNewDecimalOrZero(this.nbProjectB2.Text) + Funs.GetNewDecimalOrZero(this.nbProjectB3.Text)).ToString();
                this.nbAB.Text = (Funs.GetNewDecimalOrZero(this.nbA.Text) + Funs.GetNewDecimalOrZero(this.nbB.Text)).ToString();
                this.nbProjectAB.Text = (Funs.GetNewDecimalOrZero(this.nbProjectA.Text) + Funs.GetNewDecimalOrZero(this.nbProjectB.Text)).ToString();
            }
            else
            {
                this.nbB2.Text = string.Empty;
                Alert.ShowInTop("请先选择填报单位！", MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 输入框变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void nbB3_TextChanged(object sender, EventArgs e)
        {
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                this.nbProjectB3.Text = (Funs.GetNewDecimalOrZero(this.hdProjectB3.Text) + Funs.GetNewDecimalOrZero(this.nbB3.Text)).ToString();
                this.nbB.Text = (Funs.GetNewDecimalOrZero(this.nbB1.Text) + Funs.GetNewDecimalOrZero(this.nbB2.Text) + Funs.GetNewDecimalOrZero(this.nbB3.Text)).ToString();
                this.nbProjectB.Text = (Funs.GetNewDecimalOrZero(this.nbProjectB1.Text) + Funs.GetNewDecimalOrZero(this.nbProjectB2.Text) + Funs.GetNewDecimalOrZero(this.nbProjectB3.Text)).ToString();
                this.nbAB.Text = (Funs.GetNewDecimalOrZero(this.nbA.Text) + Funs.GetNewDecimalOrZero(this.nbB.Text)).ToString();
                this.nbProjectAB.Text = (Funs.GetNewDecimalOrZero(this.nbProjectA.Text) + Funs.GetNewDecimalOrZero(this.nbProjectB.Text)).ToString();
            }
            else
            {
                this.nbB3.Text = string.Empty;
                Alert.ShowInTop("请先选择填报单位！", MessageBoxIcon.Warning);
                return;
            }
        }
        #endregion

        #region 单位变化事件
        /// <summary>
        /// 单位变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpUnitId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                Model.Base_Project project = BLL.ProjectService.GetProjectByProjectId(this.ProjectId);
                DateTime? date = Funs.GetNewDateTime(this.drpYear.SelectedValue + "-" + this.drpMonths.SelectedValue + "-01");
                if (project != null && date.HasValue)
                {
                    List<Model.CostGoods_CostSmallDetailItem> projectDetails = BLL.CostSmallDetailItemService.GetCostDetailsByUnitId(this.CurrUser.LoginProjectId, this.drpUnitId.SelectedValue, project != null ? project.StartDate : null, date);
                    this.hdProjectA1.Text = (from x in projectDetails where x.CostType == "A1" select x.CostMoney ?? 0).Sum().ToString();
                    this.hdProjectA2.Text = (from x in projectDetails where x.CostType == "A2" select x.CostMoney ?? 0).Sum().ToString();
                    this.hdProjectA3.Text = (from x in projectDetails where x.CostType == "A3" select x.CostMoney ?? 0).Sum().ToString();
                    this.hdProjectA4.Text = (from x in projectDetails where x.CostType == "A4" select x.CostMoney ?? 0).Sum().ToString();
                    this.hdProjectA5.Text = (from x in projectDetails where x.CostType == "A5" select x.CostMoney ?? 0).Sum().ToString();
                    this.hdProjectA6.Text = (from x in projectDetails where x.CostType == "A6" select x.CostMoney ?? 0).Sum().ToString();
                    this.hdProjectB1.Text = (from x in projectDetails where x.CostType == "B1" select x.CostMoney ?? 0).Sum().ToString();
                    this.hdProjectB2.Text = (from x in projectDetails where x.CostType == "B2" select x.CostMoney ?? 0).Sum().ToString();
                    this.hdProjectB3.Text = (from x in projectDetails where x.CostType == "B3" select x.CostMoney ?? 0).Sum().ToString();
                }
            }
            else
            {
                this.hdProjectA1.Text = "0";
                this.hdProjectA2.Text = "0";
                this.hdProjectA3.Text = "0";
                this.hdProjectA4.Text = "0";
                this.hdProjectA5.Text = "0";
                this.hdProjectA6.Text = "0";
                this.hdProjectB1.Text = "0";
                this.hdProjectB2.Text = "0";
                this.hdProjectB3.Text = "0";
            }
        }
        #endregion
    }
}