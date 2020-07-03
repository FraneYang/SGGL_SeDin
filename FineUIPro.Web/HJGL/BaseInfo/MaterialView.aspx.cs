using System;
using System.Linq;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class MaterialView : PageBase
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
                string consumablesId = Request.Params["MaterialId"];
                if (!string.IsNullOrEmpty(consumablesId))
                {
                    Model.Base_Material getMaterial = BLL.Base_MaterialService.GetMaterialByMaterialId(consumablesId);
                    if (getMaterial != null)
                    {
                        this.txtMaterialCode.Text = getMaterial.MaterialCode;
                        this.txtMaterialType.Text = getMaterial.MaterialType;                       
                        var getSteelType = BLL.DropListService.HJGL_GetSteTypeList().FirstOrDefault(x => x.Value == getMaterial.SteelType);
                        if (getSteelType != null)
                        {
                            this.txtSteelType.Text = getSteelType.Text;
                        }
                        this.txtMaterialClass.Text = getMaterial.MaterialClass;
                        this.txtMaterialGroup.Text = getMaterial.MaterialGroup;
                        this.txtRemark.Text = getMaterial.Remark;                        
                    }
                }
            }
        }
        #endregion
    }
}