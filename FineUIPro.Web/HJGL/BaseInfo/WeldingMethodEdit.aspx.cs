using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class WeldingMethodEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string WeldingMethodId
        {
            get
            {
                return (string)ViewState["WeldingMethodId"];
            }
            set
            {
                ViewState["WeldingMethodId"] = value;
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
                this.txtWeldingMethodCode.Focus();
                btnClose.OnClientClick = ActiveWindow.GetHideReference();

                this.WeldingMethodId = Request.Params["WeldingMethodId"];
                if (!string.IsNullOrEmpty(this.WeldingMethodId))
                {
                    Model.Base_WeldingMethod WeldingMethod = BLL.Base_WeldingMethodService.GetWeldingMethodByWeldingMethodId(this.WeldingMethodId);
                    if (WeldingMethod != null)
                    {
                        this.txtWeldingMethodCode.Text = WeldingMethod.WeldingMethodCode;
                        this.txtWeldingMethodName.Text = WeldingMethod.WeldingMethodName;
                        this.txtRemark.Text = WeldingMethod.Remark;
                        if (!string.IsNullOrEmpty(WeldingMethod.ConsumablesType))
                        {
                            this.drpConsumablesType.SelectedValueArray = WeldingMethod.ConsumablesType.Split(',');
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
            var q = Funs.DB.Base_WeldingMethod.FirstOrDefault(x => x.WeldingMethodCode == this.txtWeldingMethodCode.Text.Trim() && (x.WeldingMethodId != this.WeldingMethodId || (this.WeldingMethodId == null && x.WeldingMethodId != null)));
            if (q != null)
            {
                Alert.ShowInTop("此焊接方法代码已经存在！", MessageBoxIcon.Warning);
                return;
            }

            var q2 = Funs.DB.Base_WeldingMethod.FirstOrDefault(x => x.WeldingMethodName == this.txtWeldingMethodName.Text.Trim() && (x.WeldingMethodId != this.WeldingMethodId || (this.WeldingMethodId == null && x.WeldingMethodId != null)));
            if (q2 != null)
            {
                Alert.ShowInTop("此焊接方法名称已经存在！", MessageBoxIcon.Warning);
                return;
            }

            Model.Base_WeldingMethod newWeldingMethod = new Model.Base_WeldingMethod
            {
                WeldingMethodCode = this.txtWeldingMethodCode.Text.Trim(),
                WeldingMethodName = this.txtWeldingMethodName.Text.Trim(),
                Remark = this.txtRemark.Text.Trim()
            };
            if (!string.IsNullOrEmpty(this.drpConsumablesType.SelectedValue))
            {
                string values = string.Empty;
                foreach (var item in this.drpConsumablesType.SelectedValueArray)
                {
                    values += item + ",";
                }
                if (!string.IsNullOrEmpty(values))
                {
                    newWeldingMethod.ConsumablesType = values.Substring(0, values.LastIndexOf(','));
                }
            }
            if (!string.IsNullOrEmpty(this.WeldingMethodId))
            {
                newWeldingMethod.WeldingMethodId = this.WeldingMethodId;
                BLL.Base_WeldingMethodService.UpdateWeldingMethod(newWeldingMethod);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_WeldingMethodMenuId, Const.BtnModify, newWeldingMethod.WeldingMethodId);
            }
            else
            {
                this.WeldingMethodId = SQLHelper.GetNewID(typeof(Model.Base_WeldingMethod));
                newWeldingMethod.WeldingMethodId = this.WeldingMethodId;
                BLL.Base_WeldingMethodService.AddWeldingMethod(newWeldingMethod);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_WeldingMethodMenuId, Const.BtnAdd, newWeldingMethod.WeldingMethodId);
            }

            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion
    }
}