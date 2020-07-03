using System;
using BLL;

namespace FineUIPro.Web.HSSE.Hazard
{
    public partial class EnvironmentalRiskItemEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string EnvironmentalRiskItemId
        {
            get
            {
                return (string)ViewState["EnvironmentalRiskItemId"];
            }
            set
            {
                ViewState["EnvironmentalRiskItemId"] = value;
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
                btnClose.OnClientClick = ActiveWindow.GetHideReference();                                
                BLL.ConstValue.InitConstValueDropDownList(this.drpSmallType, ConstValue.Group_EnvironmentalSmallType, true);
                BLL.ConstValue.InitConstValueRadioButtonList(this.rblIsImportant, ConstValue.Group_0001, "False");
                this.EnvironmentalRiskItemId = Request.Params["EnvironmentalRiskItemId"];
                if (!string.IsNullOrEmpty(this.EnvironmentalRiskItemId))
                {
                    var riskItem = BLL.Hazard_EnvironmentalRiskItemService.GetEnvironmentalRiskItemListByEnvironmentalRiskItemId(this.EnvironmentalRiskItemId);
                    if (riskItem != null)
                    {                        
                        this.txtActivePoint.Text = riskItem.ActivePoint;
                        this.txtEnvironmentalFactors.Text = riskItem.EnvironmentalFactors;
                        if (!string.IsNullOrEmpty(riskItem.SmallType))
                        {
                            this.drpSmallType.SelectedValue = riskItem.SmallType;
                        }
                        if (riskItem.IsImportant == true)
                        {
                            this.rblIsImportant.SelectedValue = "True";
                        }
                        else
                        {
                            this.rblIsImportant.SelectedValue = "False";
                        }
                        if (riskItem.AValue.HasValue)
                        {
                            this.txtAValue.Text = riskItem.AValue.ToString();
                        }
                        if (riskItem.BValue.HasValue)
                        {
                            this.txtBValue.Text = riskItem.BValue.ToString();
                        }
                        if (riskItem.CValue.HasValue)
                        {
                            this.txtCValue.Text = riskItem.CValue.ToString();
                        }
                        if (riskItem.DValue.HasValue)
                        {
                            this.txtDValue.Text = riskItem.DValue.ToString();
                        }
                        if (riskItem.EValue.HasValue)
                        {
                            this.txtEValue.Text = riskItem.EValue.ToString();
                        }
                        if (riskItem.FValue.HasValue)
                        {
                            this.txtFValue.Text = riskItem.FValue.ToString();
                        }
                        if (riskItem.GValue.HasValue)
                        {
                            this.txtGValue.Text = riskItem.GValue.ToString();
                        }
                        this.txtControlMeasures.Text = riskItem.ControlMeasures;
                        this.txtRemark.Text = riskItem.Remark;
                        this.txtEnvironmentEffect.Text = riskItem.EnvironmentEffect;
                    }
                }
                this.ShowRow();
            }
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
            var environmentalItem = BLL.Hazard_EnvironmentalRiskItemService.GetEnvironmentalRiskItemListByEnvironmentalRiskItemId(this.EnvironmentalRiskItemId);
            if (environmentalItem != null)
            {
                environmentalItem.ActivePoint = this.txtActivePoint.Text.Trim();
                environmentalItem.EnvironmentalFactors = this.txtEnvironmentalFactors.Text.Trim();
                if (this.drpSmallType.SelectedValue != BLL.Const._Null)
                {
                    environmentalItem.SmallType = this.drpSmallType.SelectedValue;
                }
                if (this.rblIsImportant.SelectedValue == "True")
                {
                    environmentalItem.IsImportant = true;
                }
                else
                {
                    environmentalItem.IsImportant = false;
                }
                int? aValue = Funs.GetNewInt(this.txtAValue.Text.Trim());
                if (aValue == 2 || aValue == 4)
                {
                    environmentalItem.AValue = aValue - 1;
                }
                else
                {
                    environmentalItem.AValue = aValue;
                }
                int? bValue = Funs.GetNewInt(this.txtBValue.Text.Trim());
                if (bValue == 2 || bValue == 4)
                {
                    environmentalItem.BValue = bValue - 1;
                }
                else
                {
                    environmentalItem.BValue = bValue;
                }
                int? cValue = Funs.GetNewInt(this.txtCValue.Text.Trim());
                if (cValue == 2 || cValue == 4)
                {
                    environmentalItem.CValue = cValue - 1;
                }
                else
                {
                    environmentalItem.CValue = cValue;
                }
                int? dValue = Funs.GetNewInt(this.txtDValue.Text.Trim());
                if (dValue == 2 || dValue == 4)
                {
                    environmentalItem.DValue = dValue - 1;
                }
                else
                {
                    environmentalItem.DValue = dValue;
                }
                int? eValue = Funs.GetNewInt(this.txtEValue.Text.Trim());
                if (eValue == 2 || eValue == 4)
                {
                    environmentalItem.EValue = eValue - 1;
                }
                else
                {
                    environmentalItem.EValue = eValue;
                }
                int? fValue = Funs.GetNewInt(this.txtFValue.Text.Trim());
                if (fValue == 2 || fValue == 4)
                {
                    environmentalItem.FValue = fValue - 1;
                }
                else
                {
                    environmentalItem.FValue = fValue;
                }
                int? gValue = Funs.GetNewInt(this.txtGValue.Text.Trim());
                if (gValue == 2 || gValue == 4)
                {
                    environmentalItem.GValue = gValue - 1;
                }
                else
                {
                    environmentalItem.GValue = gValue;
                }
                environmentalItem.ControlMeasures = this.txtControlMeasures.Text.Trim();
                environmentalItem.Remark = this.txtRemark.Text.Trim();
                environmentalItem.EnvironmentEffect = this.txtEnvironmentEffect.Text.Trim();
                environmentalItem.EnvironmentalRiskItemId = this.EnvironmentalRiskItemId;
                BLL.Hazard_EnvironmentalRiskItemService.UpdateEnvironmentalRiskItem(environmentalItem);
                BLL.LogService.AddSys_Log(this.CurrUser, environmentalItem.Code, environmentalItem.EnvironmentalRiskItemId, BLL.Const.ProjectEnvironmentalRiskListMenuId, BLL.Const.BtnModify);
            }

            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion
                                               
        #region 文本框改变事件
        /// <summary>
        /// 文本框改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            this.ShowRow();
        }

        /// <summary>
        /// 危险源类型改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpSmallType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtAValue.Text = string.Empty;
            this.txtBValue.Text = string.Empty;
            this.txtCValue.Text = string.Empty;
            this.txtDValue.Text = string.Empty;
            this.txtEValue.Text = string.Empty;
            this.txtFValue.Text = string.Empty;
            this.txtGValue.Text = string.Empty;
            this.txtZValue.Text = string.Empty;
            this.ShowRow();
        }

        /// <summary>
        ///  危险源类型事件
        /// </summary>
        private void ShowRow()
        {
            this.rblIsImportant.SelectedValue = "False";
            if (this.drpSmallType.SelectedValue == "1")
            {
                this.fRow1.Hidden = true;
                this.fRow2.Hidden = false;
                int f = Funs.GetNewIntOrZero(this.txtFValue.Text);
                int g = Funs.GetNewIntOrZero(this.txtGValue.Text);
                this.txtZValue.Text = (f + g).ToString();
                if (f >= 5 || g >= 5 || (f + g) > 7)
                {
                    this.rblIsImportant.SelectedValue = "True";
                }
            }
            else
            {
                this.fRow2.Hidden = true;
                this.fRow1.Hidden = false;
                int a = Funs.GetNewIntOrZero(this.txtAValue.Text);
                int b = Funs.GetNewIntOrZero(this.txtBValue.Text);
                int c = Funs.GetNewIntOrZero(this.txtCValue.Text);
                int d = Funs.GetNewIntOrZero(this.txtDValue.Text);
                int e = Funs.GetNewIntOrZero(this.txtEValue.Text);
                this.txtZValue.Text = (a + b + c + d + e).ToString();
                if (a >= 5 || b >= 5 || d >= 5 || (a + b + c + d + e) >= 15)
                {
                    this.rblIsImportant.SelectedValue = "True";
                }
            }
        }
        #endregion
    }
}