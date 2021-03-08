using System;
using System.Linq;
using BLL;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class GasProtectionModeEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string GasProtectionModeId
        {
            get
            {
                return (string)ViewState["GasProtectionModeId"];
            }
            set
            {
                ViewState["GasProtectionModeId"] = value;
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
                this.txtGasProtectionModeName.Focus();
                btnClose.OnClientClick = ActiveWindow.GetHideReference();

                this.GasProtectionModeId = Request.Params["GasProtectionModeId"];
                if (!string.IsNullOrEmpty(this.GasProtectionModeId))
                {
                    Model.Base_GasProtectionMode GasProtectionMode = BLL.Base_GasProtectionModeService.GetGasProtectionModeByGasProtectionModeId(this.GasProtectionModeId);
                    if (GasProtectionMode != null)
                    {
                        this.txtGasProtectionModeName.Text = GasProtectionMode.GasProtectionModeName;
                        this.txtRemark.Text = GasProtectionMode.Remark;
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
            var q = Funs.DB.Base_GasProtectionMode.FirstOrDefault(x => x.GasProtectionModeName == this.txtGasProtectionModeName.Text.Trim() && (x.GasProtectionModeId.ToString() != this.GasProtectionModeId || this.GasProtectionModeId == null));
            if (q != null)
            {
                Alert.ShowInTop("此气体保护方式名称已存在", MessageBoxIcon.Warning);
                return;
            }

            Model.Base_GasProtectionMode newGasProtectionMode = new Model.Base_GasProtectionMode
            {
                GasProtectionModeName = this.txtGasProtectionModeName.Text.Trim(),
                Remark=this.txtRemark.Text.Trim(),
            };

            if (!string.IsNullOrEmpty(this.GasProtectionModeId))
            {
                newGasProtectionMode.GasProtectionModeId = this.GasProtectionModeId;
                BLL.Base_GasProtectionModeService.UpdateGasProtectionMode(newGasProtectionMode);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_GasProtectionModeMenuId, Const.BtnDelete, this.GasProtectionModeId);
            }
            else
            {
                newGasProtectionMode.GasProtectionModeId = SQLHelper.GetNewID();
                BLL.Base_GasProtectionModeService.AddGasProtectionMode(newGasProtectionMode);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_GasProtectionModeMenuId, Const.BtnDelete, this.GasProtectionModeId);
            }

            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion
    }
}