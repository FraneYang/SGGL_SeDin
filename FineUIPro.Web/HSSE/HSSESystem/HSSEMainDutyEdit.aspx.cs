using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.HSSE.HSSESystem
{
    public partial class HSSEMainDutyEdit : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string HSSEMainDutyId
        {
            get
            {
                return (string)ViewState["HSSEMainDutyId"];
            }
            set
            {
                ViewState["HSSEMainDutyId"] = value;
            }
        }

        /// <summary>
        /// 岗位id
        /// </summary>
        public string WorkPostId
        {
            get
            {
                return (string)ViewState["WorkPostId"];
            }
            set
            {
                ViewState["WorkPostId"] = value;
            }
        }

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
                LoadData();
                this.WorkPostId = Request.Params["WorkPostId"];
                if (!string.IsNullOrEmpty(this.WorkPostId))
                {
                    this.hdWorkPostId.Text = this.WorkPostId;
                    var workPost = BLL.WorkPostService.GetWorkPostById(this.hdWorkPostId.Text);
                    if (workPost != null)
                    {
                        this.txtWorkPostName.Text = workPost.WorkPostName;
                    }
                }
                this.HSSEMainDutyId = Request.Params["HSSEMainDutyId"];
                if (!string.IsNullOrEmpty(this.HSSEMainDutyId))
                {
                    Model.HSSESystem_HSSEMainDuty hsseMainDuty = BLL.HSSEMainDutyService.GetHSSEMainDutyById(this.HSSEMainDutyId);
                    if (hsseMainDuty != null)
                    {
                        if (!string.IsNullOrEmpty(hsseMainDuty.WorkPostId))
                        {
                            var workPost = BLL.WorkPostService.GetWorkPostById(hsseMainDuty.WorkPostId);
                            if (workPost != null)
                            {
                                this.txtWorkPostName.Text = workPost.WorkPostName;
                            }
                        }
                        this.txtDuties.Text = hsseMainDuty.Duties;
                        this.txtRemark.Text = hsseMainDuty.Remark;
                        this.txtSortIndex.Text = hsseMainDuty.SortIndex;
                    }
                }
            }
        }

        /// <summary>
        /// 加载页面
        /// </summary>
        private void LoadData()
        {
            btnClose.OnClientClick = ActiveWindow.GetHideReference();
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.HSSESystem_HSSEMainDuty hsseMainDuty = new Model.HSSESystem_HSSEMainDuty
            {
                WorkPostId = this.hdWorkPostId.Text
            };
            if (!string.IsNullOrEmpty(this.txtDuties.Text.Trim()))
            {
                hsseMainDuty.Duties = this.txtDuties.Text.Trim();
            }
            else
            {
                ShowNotify("请输入职责！");
                return;
            }
            hsseMainDuty.Remark = this.txtRemark.Text.Trim();
            hsseMainDuty.SortIndex = this.txtSortIndex.Text.Trim();
            if (string.IsNullOrEmpty(this.HSSEMainDutyId))
            {
                hsseMainDuty.HSSEMainDutyId = SQLHelper.GetNewID(typeof(Model.HSSESystem_HSSEMainDuty));
                BLL.HSSEMainDutyService.AddHSSEMainDuty(hsseMainDuty);
                BLL.LogService.AddSys_Log(this.CurrUser, hsseMainDuty.SortIndex, hsseMainDuty.HSSEMainDutyId, BLL.Const.HSSEMainDutyMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                hsseMainDuty.HSSEMainDutyId = this.HSSEMainDutyId;
                BLL.HSSEMainDutyService.UpdateHSSEMainDuty(hsseMainDuty);
                BLL.LogService.AddSys_Log(this.CurrUser, hsseMainDuty.SortIndex, hsseMainDuty.HSSEMainDutyId, BLL.Const.HSSEMainDutyMenuId, BLL.Const.BtnModify);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSEMainDutyMenuId);
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