using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.HSSE.Technique
{
    public partial class RectifyEdit : PageBase
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
        /// 上一节点id
        /// </summary>
        public string SupRectifyId
        {
            get
            {
                return (string)ViewState["SupRectifyId"];
            }
            set
            {
                ViewState["SupRectifyId"] = value;
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
                ////权限按钮方法
                this.GetButtonPower();
                this.drpIsEndLever.DataTextField = "TypeName";
                drpIsEndLever.DataValueField = "TypeId";
                drpIsEndLever.DataSource = BLL.TrainingService.GetIsEndLeverList();
                drpIsEndLever.DataBind();

                this.RectifyId = Request.Params["RectifyId"];
                this.SupRectifyId = Request.Params["SupRectifyId"];
                if (!string.IsNullOrEmpty(this.RectifyId))
                {
                    Model.Technique_Rectify rectify = BLL.RectifyService.GetRectifyById(this.RectifyId);
                    if (rectify != null)
                    {
                        this.txtRectifyCode.Text = rectify.RectifyCode;
                        this.txtRectifyName.Text = rectify.RectifyName;
                        if (rectify.IsEndLever == true)
                        {
                            this.drpIsEndLever.SelectedValue = "true";
                        }
                        else
                        {
                            this.drpIsEndLever.SelectedValue = "false";
                        }
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
            Model.Technique_Rectify rectify = new Model.Technique_Rectify
            {
                RectifyCode = this.txtRectifyCode.Text.Trim(),
                RectifyName = this.txtRectifyName.Text.Trim()
            };
            if (this.drpIsEndLever.SelectedValue == "true")
            {
                rectify.IsEndLever = true;
            }
            else
            {
                rectify.IsEndLever = false;
            }
            if (string.IsNullOrEmpty(this.RectifyId))
            {
                rectify.SupRectifyId = this.SupRectifyId;
                rectify.RectifyId = SQLHelper.GetNewID(typeof(Model.Technique_Rectify));
                BLL.RectifyService.AddRectify(rectify);
                
                BLL.LogService.AddSys_Log(this.CurrUser, rectify.RectifyName, rectify.RectifyId, BLL.Const.RectifyMenuId, Const.BtnAdd);
            }
            else
            {
                Model.Technique_Rectify r = BLL.RectifyService.GetRectifyById(this.RectifyId);
                if (r != null)
                {
                    rectify.SupRectifyId = r.SupRectifyId;
                }
                rectify.RectifyId = this.RectifyId;
                BLL.RectifyService.UpdateRectify(rectify);
                
                BLL.LogService.AddSys_Log(this.CurrUser, rectify.RectifyName, rectify.RectifyId, BLL.Const.RectifyMenuId, Const.BtnModify);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
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