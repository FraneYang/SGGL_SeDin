using System;
using System.Linq;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class ConsumablesView : PageBase
    {
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                string consumablesId = Request.Params["ConsumablesId"];
                if (!string.IsNullOrEmpty(consumablesId))
                {
                    Model.Base_Consumables getConsumables = BLL.Base_ConsumablesService.GetConsumablesByConsumablesId(consumablesId);
                    if (getConsumables != null)
                    {
                        this.txtConsumablesCode.Text = getConsumables.ConsumablesCode;
                        this.txtConsumablesName.Text = getConsumables.ConsumablesName;
                        this.txtSteelFormat.Text = getConsumables.SteelFormat;
                        txtStandard.Text = getConsumables.Standard;
                        this.txtRemark.Text = getConsumables.Remark;
                        var getConsumablesType = BLL.DropListService.HJGL_ConsumablesTypeList().FirstOrDefault(x => x.Value == getConsumables.ConsumablesType);
                        if (getConsumablesType != null)
                        {
                            this.txtConsumablesType.Text = getConsumablesType.Text;
                        }

                        var getSteelType = BLL.DropListService.HJGL_GetSteTypeList().FirstOrDefault(x => x.Value == getConsumables.SteelType);
                        if (getSteelType != null)
                        {
                            this.txtSteelType.Text = getSteelType.Text;
                        }
                    }
                }
            }
        }
        #endregion
    }
}