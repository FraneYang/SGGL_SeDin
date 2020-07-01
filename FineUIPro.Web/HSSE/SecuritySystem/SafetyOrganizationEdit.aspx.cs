using BLL;
using System;

namespace FineUIPro.Web.HSSE.SecuritySystem
{
    public partial class SafetyOrganizationItemEdit : PageBase
    {
        #region 定义项
        public string SafetyOrganizationId
        {
            get
            {
                return (string)ViewState["SafetyOrganizationId"];
            }
            set
            {
                ViewState["SafetyOrganizationId"] = value;
            }
        }

        public string UnitId
        {
            get
            {
                return (string)ViewState["UnitId"];
            }
            set
            {
                ViewState["UnitId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.SafetyOrganizationId = Request.Params["SafetyOrganizationId"];
                this.UnitId = Request.Params["UnitId"];
                if (!string.IsNullOrEmpty(this.SafetyOrganizationId))
                {
                    var item = BLL.SafetyOrganizationService.GetSafetyOrganizationById(this.SafetyOrganizationId);
                    if (item != null)
                    {
                        this.txtPost.Text = item.Post;
                        this.txtNames.Text = item.Names;
                        this.txtTelephone.Text = item.Telephone;
                        this.txtMobilePhone.Text = item.MobilePhone;
                        this.txtEMail.Text = item.EMail;
                        this.txtDuty.Text = item.Duty;
                        this.txtSortIndex.Text = item.SortIndex;
                    }
                }

                if (Request.Params["value"] == "0")
                {
                    this.btnSave.Hidden=true;
                }
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.SecuritySystem_SafetyOrganization newItem = new Model.SecuritySystem_SafetyOrganization
            {
                Post = this.txtPost.Text.Trim(),
                Names = this.txtNames.Text.Trim(),
                Telephone = this.txtTelephone.Text.Trim(),
                MobilePhone = this.txtMobilePhone.Text.Trim(),
                EMail = this.txtEMail.Text.Trim(),
                Duty = this.txtDuty.Text.Trim(),
                SortIndex = this.txtSortIndex.Text.Trim()
            };

            if (string.IsNullOrEmpty(this.SafetyOrganizationId))
            {
                newItem.ProjectId = this.CurrUser.LoginProjectId;
                newItem.UnitId = this.UnitId;
                newItem.SafetyOrganizationId = this.SafetyOrganizationId;
                this.SafetyOrganizationId = SQLHelper.GetNewID(typeof(Model.SecuritySystem_SafetyOrganization));
                newItem.SafetyOrganizationId = this.SafetyOrganizationId;
                BLL.SafetyOrganizationService.AddSafetyOrganization(newItem);
                BLL.LogService.AddSys_Log(this.CurrUser, newItem.Names, newItem.SafetyOrganizationId, BLL.Const.ProjectSafetyOrganizationMenuId, BLL.Const.BtnAdd);
            }
            else
            {                
                newItem.SafetyOrganizationId = this.SafetyOrganizationId;
                BLL.SafetyOrganizationService.UpdateSafetyOrganization(newItem);
                BLL.LogService.AddSys_Log(this.CurrUser, newItem.Names, newItem.SafetyOrganizationId,BLL.Const.ProjectSafetyOrganizationMenuId,BLL.Const.BtnModify);
            }

            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
    }
}