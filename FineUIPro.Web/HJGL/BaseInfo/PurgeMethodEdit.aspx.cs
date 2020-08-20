using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class PurgeMethodEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string PurgeMethodId
        {
            get
            {
                return (string)ViewState["PurgeMethodId"];
            }
            set
            {
                ViewState["PurgeMethodId"] = value;
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
                this.txtPurgeMethodCode.Focus();
                btnClose.OnClientClick = ActiveWindow.GetHideReference();

                this.PurgeMethodId = Request.Params["PurgeMethodId"];
                if (!string.IsNullOrEmpty(this.PurgeMethodId))
                {
                    Model.Base_PurgeMethod purgeMethod = BLL.Base_PurgeMethodService.GetPurgeMethod(this.PurgeMethodId);
                    if (purgeMethod != null)
                    {
                        this.txtPurgeMethodCode.Text = purgeMethod.PurgeMethodCode;
                        txtPurgeMethodName.Text = purgeMethod.PurgeMethodName;
                        this.txtRemark.Text = purgeMethod.Remark;
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
            var q = Funs.DB.Base_PurgeMethod.FirstOrDefault(x => x.PurgeMethodCode == this.txtPurgeMethodCode.Text.Trim() && (x.PurgeMethodId != this.PurgeMethodId || (this.PurgeMethodId == null && x.PurgeMethodId != null)));
            if (q != null)
            {
                Alert.ShowInTop("此管道吹洗方法代号已经存在！", MessageBoxIcon.Warning);
                return;
            }

            Model.Base_PurgeMethod newPurgeMethod = new Model.Base_PurgeMethod
            {
                PurgeMethodCode = this.txtPurgeMethodCode.Text.Trim(),
                PurgeMethodName = txtPurgeMethodName.Text.Trim(),
                Remark = this.txtRemark.Text.Trim()
            };

            if (!string.IsNullOrEmpty(this.PurgeMethodId))
            {
                newPurgeMethod.PurgeMethodId = this.PurgeMethodId;
                BLL.Base_PurgeMethodService.UpdatePurgeMethod(newPurgeMethod);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_GrooveTypeMenuId, Const.BtnModify, this.GrooveTypeId);
            }
            else
            {
                this.PurgeMethodId = SQLHelper.GetNewID(typeof(Model.Base_PurgeMethod));
                newPurgeMethod.PurgeMethodId = this.PurgeMethodId;
                BLL.Base_PurgeMethodService.AddPurgeMethod(newPurgeMethod);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_GrooveTypeMenuId, Const.BtnAdd, this.GrooveTypeId);
            }

            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion
    }
}