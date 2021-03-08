using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class ProtectionGasEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string ProtectionGasId
        {
            get
            {
                return (string)ViewState["ProtectionGasId"];
            }
            set
            {
                ViewState["ProtectionGasId"] = value;
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

                this.ProtectionGasId = Request.Params["ProtectionGasId"];
                if (!string.IsNullOrEmpty(this.ProtectionGasId))
                {
                    Model.Base_ProtectionGas ProtectionGas = BLL.Base_ProtectionGasService.GetProtectionGasByProtectionGasId(this.ProtectionGasId);
                    if (ProtectionGas != null)
                    {
                        this.txtProtectionGasCode.Text = ProtectionGas.ProtectionGasCode;
                        this.txtProtectionGasName.Text = ProtectionGas.ProtectionGasName;
                        this.txtRemark.Text = ProtectionGas.Remark;
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
            if (!string.IsNullOrEmpty(this.txtProtectionGasCode.Text.Trim()))
            {
                var q = Funs.DB.Base_ProtectionGas.FirstOrDefault(x => x.ProtectionGasCode == this.txtProtectionGasCode.Text.Trim() && (x.ProtectionGasId != this.ProtectionGasId || (this.ProtectionGasId == null && x.ProtectionGasId != null)));
                if (q != null)
                {
                    Alert.ShowInTop("此保护气体代码已经存在！", MessageBoxIcon.Warning);
                    return;
                }
            }
            var q2 = Funs.DB.Base_ProtectionGas.FirstOrDefault(x => x.ProtectionGasName == this.txtProtectionGasName.Text.Trim() && (x.ProtectionGasId != this.ProtectionGasId || (this.ProtectionGasId == null && x.ProtectionGasId != null)));
            if (q2 != null)
            {
                Alert.ShowInTop("此保护气体名称已经存在！", MessageBoxIcon.Warning);
                return;
            }

            Model.Base_ProtectionGas newProtectionGas = new Model.Base_ProtectionGas
            {
                ProtectionGasCode = this.txtProtectionGasCode.Text.Trim(),
                ProtectionGasName = this.txtProtectionGasName.Text.Trim(),
                Remark = this.txtRemark.Text.Trim()
            };
            if (!string.IsNullOrEmpty(this.ProtectionGasId))
            {
                newProtectionGas.ProtectionGasId = this.ProtectionGasId;
                BLL.Base_ProtectionGasService.UpdateProtectionGas(newProtectionGas);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_ProtectionGasMenuId, Const.BtnModify, newProtectionGas.ProtectionGasId);
            }
            else
            {
                this.ProtectionGasId = SQLHelper.GetNewID(typeof(Model.Base_ProtectionGas));
                newProtectionGas.ProtectionGasId = this.ProtectionGasId;
                BLL.Base_ProtectionGasService.AddProtectionGas(newProtectionGas);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_ProtectionGasMenuId, Const.BtnAdd, newProtectionGas.ProtectionGasId);
            }

            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion
    }
}