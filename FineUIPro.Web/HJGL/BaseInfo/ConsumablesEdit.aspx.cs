using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class ConsumablesEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string ConsumablesId
        {
            get
            {
                return (string)ViewState["ConsumablesId"];
            }
            set
            {
                ViewState["ConsumablesId"] = value;
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
                this.txtConsumablesCode.Focus();
                btnClose.OnClientClick = ActiveWindow.GetHideReference();

                this.drpConsumablesType.DataTextField = "Text";
                this.drpConsumablesType.DataValueField = "Value";
                this.drpConsumablesType.DataSource = BLL.DropListService.HJGL_ConsumablesTypeList();
                this.drpConsumablesType.DataBind();
                Funs.FineUIPleaseSelect(this.drpConsumablesType, "请选择");

                this.drpSteelType.DataTextField = "Text";
                this.drpSteelType.DataValueField = "Value";
                this.drpSteelType.DataSource = BLL.DropListService.HJGL_GetSteTypeList();
                this.drpSteelType.DataBind();
                Funs.FineUIPleaseSelect(this.drpSteelType, "请选择");

                this.ConsumablesId = Request.Params["ConsumablesId"];
                if (!string.IsNullOrEmpty(this.ConsumablesId))
                {
                    Model.Base_Consumables Consumables = BLL.Base_ConsumablesService.GetConsumablesByConsumablesId(this.ConsumablesId);
                    if (Consumables != null)
                    {
                        this.txtConsumablesCode.Text = Consumables.ConsumablesCode;
                        this.txtConsumablesName.Text = Consumables.ConsumablesName;
                        this.txtSteelFormat.Text = Consumables.SteelFormat;
                        this.drpConsumablesType.SelectedValue = Consumables.ConsumablesType;
                        this.drpSteelType.SelectedValue = Consumables.SteelType;
                        txtStandard.Text = Consumables.Standard;
                        this.txtRemark.Text = Consumables.Remark;
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
            var q = Funs.DB.Base_Consumables.FirstOrDefault(x => x.ConsumablesCode == this.txtConsumablesCode.Text.Trim() && (x.ConsumablesId != this.ConsumablesId || (this.ConsumablesId == null && x.ConsumablesId != null)));
            if (q != null)
            {
                Alert.ShowInTop("此焊材代号已存在！", MessageBoxIcon.Warning);
                return;
            }

            var q2 = Funs.DB.Base_Consumables.FirstOrDefault(x => x.ConsumablesName == this.txtConsumablesName.Text.Trim() && (x.ConsumablesId != this.ConsumablesId || (this.ConsumablesId == null && x.ConsumablesId != null)));
            if (q2 != null)
            {
                Alert.ShowInTop("此焊材名称已存在！", MessageBoxIcon.Warning);
                return;
            }

            Model.Base_Consumables newConsumables = new Model.Base_Consumables
            {
                ConsumablesCode = this.txtConsumablesCode.Text.Trim(),
                ConsumablesName = this.txtConsumablesName.Text.Trim(),
                SteelFormat = this.txtSteelFormat.Text.Trim(),
                Standard = txtStandard.Text.Trim(),
                Remark = this.txtRemark.Text.Trim()
            };
            if (this.drpConsumablesType.SelectedValue != BLL.Const._Null)
            {
                newConsumables.ConsumablesType = this.drpConsumablesType.SelectedValue;
            }
            if (this.drpSteelType.SelectedValue != BLL.Const._Null)
            {
                newConsumables.SteelType = this.drpSteelType.SelectedValue;
            }

            if (!string.IsNullOrEmpty(this.ConsumablesId))
            {
                newConsumables.ConsumablesId = this.ConsumablesId;
                BLL.Base_ConsumablesService.UpdateConsumables(newConsumables);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_ConsumablesMenuId, Const.BtnModify, this.ConsumablesId);
            }
            else
            {
                this.ConsumablesId = SQLHelper.GetNewID(typeof(Model.Base_Consumables));
                newConsumables.ConsumablesId = this.ConsumablesId;
                BLL.Base_ConsumablesService.AddConsumables(newConsumables);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_ConsumablesMenuId, Const.BtnAdd, this.ConsumablesId);

                ShowNotify("保存成功！", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            #endregion
        }
    }
}