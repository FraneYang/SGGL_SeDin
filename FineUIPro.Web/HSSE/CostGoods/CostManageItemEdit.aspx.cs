using BLL;
using System;

namespace FineUIPro.Web.HSSE.CostGoods
{
    public partial class CostManageItemEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string CostManageItemId
        {
            get
            {
                return (string)ViewState["CostManageItemId"];
            }
            set
            {
                ViewState["CostManageItemId"] = value;
            }
        }

        /// <summary>
        /// 费用管理主键
        /// </summary>
        private string CostManageId
        {
            get
            {
                return (string)ViewState["CostManageId"];
            }
            set
            {
                ViewState["CostManageId"] = value;
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
                this.CostManageId = Request.Params["CostManageId"];
                this.CostManageItemId = Request.Params["CostManageItemId"];
                this.drpInvestCostProject.DataTextField = "Text";
                this.drpInvestCostProject.DataValueField = "Value";
                this.drpInvestCostProject.DataSource = BLL.CostManageItemService.GetInvestCostProjectList();
                this.drpInvestCostProject.DataBind();
                if (!string.IsNullOrEmpty(this.CostManageItemId))
                {
                    Model.CostGoods_CostManageItem costManageItem = BLL.CostManageItemService.GetCostManageItemById(this.CostManageItemId);
                    if (costManageItem != null)
                    {
                        this.CostManageId = costManageItem.CostManageId;
                        this.drpInvestCostProject.SelectedValue = costManageItem.InvestCostProject;
                        this.txtUseReason.Text = costManageItem.UseReason;
                        if (costManageItem.Counts != null)
                        {
                            this.txtCount.Text = Convert.ToString(costManageItem.Counts);
                        }
                        if (costManageItem.PriceMoney != null)
                        {
                            this.txtPriceMoney.Text = Convert.ToString(costManageItem.PriceMoney);
                        }
                        this.txtTotalMoney.Text = Convert.ToString(costManageItem.Counts * costManageItem.PriceMoney);
                    }
                }
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
            Model.CostGoods_CostManageItem costManageItem = new Model.CostGoods_CostManageItem
            {
                CostManageId = this.CostManageId,
                InvestCostProject = this.drpInvestCostProject.SelectedValue,
                UseReason = this.txtUseReason.Text.Trim(),
                Counts = Funs.GetNewInt(this.txtCount.Text.Trim()),
                PriceMoney = Funs.GetNewDecimalOrZero(this.txtPriceMoney.Text.Trim()),
            };
            if (!string.IsNullOrEmpty(this.CostManageItemId))
            {
                costManageItem.CostManageItemId = this.CostManageItemId;
                BLL.CostManageItemService.UpdateCostManageItem(costManageItem);
            }
            else
            {
                this.CostManageItemId = SQLHelper.GetNewID(typeof(Model.CostGoods_CostManageItem));
                costManageItem.CostManageItemId = this.CostManageItemId;
                BLL.CostManageItemService.AddCostManageItem(costManageItem);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        #region 计算总价
        /// <summary>
        /// 计算总价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Text_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtCount.Text.Trim()) && !string.IsNullOrEmpty(this.txtPriceMoney.Text.Trim()))
            {
                int? count = Funs.GetNewInt(this.txtCount.Text.Trim());
                decimal? price = Funs.GetNewDecimalOrZero(this.txtPriceMoney.Text.Trim());
                this.txtTotalMoney.Text = Convert.ToString(count * price);
            }
        }
        #endregion
    }
}