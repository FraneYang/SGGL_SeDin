using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.HSSE.Technique
{
    public partial class RectifyItemEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 安全隐患主键
        /// </summary>
        public string RectifyId
        {
            get
            {
                return (string)ViewState["RectifyId"];
            }
            set
            {
                ViewState["RectifyId"] = value;
            }
        }

        /// <summary>
        /// 安全隐患明细主键
        /// </summary>
        public string RectifyItemId
        {
            get
            {
                return (string)ViewState["RectifyItemId"];
            }
            set
            {
                ViewState["RectifyItemId"] = value;
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
                ////权限按钮方法
                this.GetButtonPower();
                this.RectifyId = Request.Params["RectifyId"];
                this.RectifyItemId = Request.Params["RectifyItemId"];
                if (!string.IsNullOrEmpty(this.RectifyItemId))
                {
                    var rectifyItem = BLL.RectifyItemService.GetRectifyItemById(this.RectifyItemId);
                    if (rectifyItem != null)
                    {
                        if (!string.IsNullOrEmpty(rectifyItem.RectifyId))
                        {
                            var rectify = BLL.RectifyService.GetRectifyById(rectifyItem.RectifyId);
                            if (rectify != null)
                            {
                                this.lblRectifyName.Text = rectify.RectifyName;
                            }
                        }
                        this.txtHazardSourcePoint.Text = rectifyItem.HazardSourcePoint;
                        this.txtRiskAnalysis.Text = rectifyItem.RiskAnalysis;
                        this.txtRiskPrevention.Text = rectifyItem.RiskPrevention;
                        this.txtSimilarRisk.Text = rectifyItem.SimilarRisk;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.RectifyId))
                    {
                        var rectify = BLL.RectifyService.GetRectifyById(this.RectifyId);
                        if (rectify != null)
                        {
                            this.lblRectifyName.Text = rectify.RectifyName;
                        }
                    }
                }
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData()
        {
            Model.Technique_RectifyItem rectifyItem = new Model.Technique_RectifyItem
            {
                HazardSourcePoint = this.txtHazardSourcePoint.Text.Trim(),
                RiskAnalysis = this.txtRiskAnalysis.Text.Trim(),
                RiskPrevention = this.txtRiskPrevention.Text.Trim(),
                SimilarRisk = this.txtSimilarRisk.Text.Trim(),
            };
            if (string.IsNullOrEmpty(this.RectifyItemId))
            {
                rectifyItem.CompileMan = this.CurrUser.UserName;
                rectifyItem.UnitId = String.IsNullOrEmpty(this.CurrUser.UnitId) ? Const.UnitId_SEDIN : this.CurrUser.UnitId;
                rectifyItem.CompileDate = DateTime.Now;
                rectifyItem.IsPass = true;
                rectifyItem.RectifyItemId = SQLHelper.GetNewID(typeof(Model.Technique_RectifyItem));
                RectifyItemId = rectifyItem.RectifyItemId;
                rectifyItem.RectifyId = this.RectifyId;
                BLL.RectifyItemService.AddRectifyItem(rectifyItem);
                BLL.LogService.AddSys_Log(this.CurrUser, rectifyItem.HazardSourcePoint, rectifyItem.RectifyItemId, BLL.Const.RectifyMenuId, Const.BtnAdd);
            }
            else
            {
                rectifyItem.RectifyItemId = this.RectifyItemId;
                Model.Technique_RectifyItem r = BLL.RectifyItemService.GetRectifyItemById(this.RectifyId);
                if (r != null)
                {
                    rectifyItem.RectifyId = r.RectifyId;
                }
                BLL.RectifyItemService.UpdateRectifyItem(rectifyItem);
                BLL.LogService.AddSys_Log(this.CurrUser, rectifyItem.HazardSourcePoint, rectifyItem.RectifyItemId, BLL.Const.RectifyMenuId, Const.BtnModify);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        #endregion
               
        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }

            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.RectifyMenuId);
            if (buttonList.Count() > 0)
            {

                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion
    }
}