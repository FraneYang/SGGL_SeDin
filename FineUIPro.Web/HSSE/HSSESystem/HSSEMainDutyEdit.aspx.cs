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

            if (string.IsNullOrEmpty(this.txtDuties.Text.Trim()))
            {
                ShowNotify("请输入职责！");
                return;
            }
           
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        /// <summary>
        /// 
        /// </summary>
        private void SaveData()
        {
            Model.HSSESystem_HSSEMainDuty hsseMainDuty = new Model.HSSESystem_HSSEMainDuty
            {
                WorkPostId = this.hdWorkPostId.Text
            };
            if (!string.IsNullOrEmpty(this.txtDuties.Text.Trim()))
            {
                hsseMainDuty.Duties = this.txtDuties.Text.Trim();
            }
            hsseMainDuty.Remark = this.txtRemark.Text.Trim();
            hsseMainDuty.SortIndex = this.txtSortIndex.Text.Trim();
            if (string.IsNullOrEmpty(this.HSSEMainDutyId))
            {
                this.HSSEMainDutyId=hsseMainDuty.HSSEMainDutyId = SQLHelper.GetNewID();
                BLL.HSSEMainDutyService.AddHSSEMainDuty(hsseMainDuty);
                BLL.LogService.AddSys_Log(this.CurrUser, hsseMainDuty.SortIndex, hsseMainDuty.HSSEMainDutyId, BLL.Const.HSSEMainDutyMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                hsseMainDuty.HSSEMainDutyId = this.HSSEMainDutyId;
                BLL.HSSEMainDutyService.UpdateHSSEMainDuty(hsseMainDuty);
                BLL.LogService.AddSys_Log(this.CurrUser, hsseMainDuty.SortIndex, hsseMainDuty.HSSEMainDutyId, BLL.Const.HSSEMainDutyMenuId, BLL.Const.BtnModify);
            }
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

        #region 上传附件资源
        /// <summary>
        /// 上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploadResources_Click(object sender, EventArgs e)
        {
            if (this.btnSave.Hidden)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CompanyTraining&menuId={1}&type=-1", HSSEMainDutyId, BLL.Const.HSSEMainDutyMenuId)));
            }
            else
            {
                if (string.IsNullOrEmpty(this.HSSEMainDutyId))
                {
                    SaveData();
                }
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CompanyTraining&menuId={1}", HSSEMainDutyId, BLL.Const.HSSEMainDutyMenuId)));
            }
        }
        #endregion

    }
}