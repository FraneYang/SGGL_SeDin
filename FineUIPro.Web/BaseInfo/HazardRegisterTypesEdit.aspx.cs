using BLL;
using System;

namespace FineUIPro.Web.BaseInfo
{
    public partial class HazardRegisterTypesEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 问题巡检类型主键
        /// </summary>
        public string RegisterTypesId
        {
            get
            {
                return (string)ViewState["RegisterTypesId"];
            }
            set
            {
                ViewState["RegisterTypesId"] = value;
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

                this.RegisterTypesId = Request.QueryString["RegisterTypesId"];
                if (!string.IsNullOrEmpty(this.RegisterTypesId))
                {
                    var title = BLL.HSSE_Hazard_HazardRegisterTypesService.GetTitleByRegisterTypesId(this.RegisterTypesId);
                    if (title != null)
                    {
                        this.txtRegisterTypesName.Text = title.RegisterTypesName;
                        this.txtTypeCode.Text = title.TypeCode;
                        if (title.IsPunished == true)
                        {
                            chkIsPunished.Checked = true;
                        }
                        else
                        {
                            chkIsPunished.Checked = false;
                        }
                    }
                }
            }
        }
        #endregion

        #region 保存数据
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HazardRegisterTypesMenuId, Const.BtnSave))
            {
                Model.HSSE_Hazard_HazardRegisterTypes title = new Model.HSSE_Hazard_HazardRegisterTypes();
                title.RegisterTypesName = this.txtRegisterTypesName.Text.Trim();
                title.TypeCode = this.txtTypeCode.Text.Trim();
                title.HazardRegisterType = "1";   //安全巡检类型
                if (this.chkIsPunished.Checked)
                {
                    title.IsPunished = true;
                }
                if (string.IsNullOrEmpty(this.RegisterTypesId))
                {
                    this.RegisterTypesId = SQLHelper.GetNewID(typeof(Model.HSSE_Hazard_HazardRegisterTypes));
                    title.RegisterTypesId = this.RegisterTypesId;
                    BLL.HSSE_Hazard_HazardRegisterTypesService.AddHazardRegisterTypes(title);
                    BLL.LogService.AddSys_Log(this.CurrUser, title.TypeCode, title.RegisterTypesId,BLL.Const.HazardRegisterTypesMenuId,BLL.Const.BtnAdd);
                }
                else
                {
                    title.RegisterTypesId = this.RegisterTypesId;
                    BLL.HSSE_Hazard_HazardRegisterTypesService.UpdateHazardRegisterTypes(title);
                    BLL.LogService.AddSys_Log(this.CurrUser, title.TypeCode, title.RegisterTypesId, BLL.Const.HazardRegisterTypesMenuId, BLL.Const.BtnModify);
                }
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！");
            }
        }
        #endregion
    }
}