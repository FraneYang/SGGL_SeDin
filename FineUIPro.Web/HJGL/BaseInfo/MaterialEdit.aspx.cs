using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class MaterialEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string MaterialId
        {
            get
            {
                return (string)ViewState["MaterialId"];
            }
            set
            {
                ViewState["MaterialId"] = value;
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
                this.txtMaterialCode.Focus();
                btnClose.OnClientClick = ActiveWindow.GetHideReference();

                this.drpSteelType.DataTextField = "Text";
                this.drpSteelType.DataValueField = "Value";
                this.drpSteelType.DataSource = BLL.DropListService.HJGL_GetSteTypeList();
                this.drpSteelType.DataBind();
                Funs.FineUIPleaseSelect(this.drpSteelType);

                this.MaterialId = Request.Params["MaterialId"];
                if (!string.IsNullOrEmpty(this.MaterialId))
                {
                    Model.Base_Material Material = BLL.Base_MaterialService.GetMaterialByMaterialId(this.MaterialId);
                    if (Material != null)
                    {
                        this.txtMaterialCode.Text = Material.MaterialCode;
                        this.txtMaterialType.Text = Material.MaterialType;                      
                        this.drpSteelType.SelectedValue = Material.SteelType;
                        this.txtMaterialClass.Text = Material.MaterialClass;
                        this.txtMaterialGroup.Text = Material.MaterialGroup;
                        this.txtRemark.Text = Material.Remark;
                        this.txtMetalType.Text = Material.MetalType;
                    }
                }
            }
        }
        #endregion

        #region 类别变化事件
        /// <summary>
        /// 类别变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpMaterialClass_TextChanged(object sender, EventArgs e)
        {
            
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
            var q = Funs.DB.Base_Material.FirstOrDefault(x => x.MaterialCode == this.txtMaterialCode.Text.Trim() && (x.MaterialId != this.MaterialId || (this.MaterialId == null && x.MaterialId != null)));
            if (q != null)
            {
                Alert.ShowInTop("此材质代号已经存在！", MessageBoxIcon.Warning);
                return;
            }

            if (this.drpSteelType.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择材质类型！", MessageBoxIcon.Warning);
                return;
            }

            Model.Base_Material newMaterial = new Model.Base_Material
            {
                MaterialCode = this.txtMaterialCode.Text.Trim(),
                MaterialType = this.txtMaterialType.Text.Trim(),
                MaterialClass = this.txtMaterialClass.Text.Trim(),
                MaterialGroup = this.txtMaterialGroup.Text.Trim(),
                Remark = this.txtRemark.Text.Trim(),
                MetalType=this.txtMetalType.Text.Trim()
            };

            if (this.drpSteelType.SelectedValue != BLL.Const._Null)
            {
                newMaterial.SteelType = this.drpSteelType.SelectedValue;
            }

            if (!string.IsNullOrEmpty(this.MaterialId))
            {
                newMaterial.MaterialId = this.MaterialId;
                BLL.Base_MaterialService.UpdateMaterial(newMaterial);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_MaterialMenuId, Const.BtnModify, this.MaterialId);
            }
            else
            {
                this.MaterialId = SQLHelper.GetNewID(typeof(Model.Base_Material));
                newMaterial.MaterialId = this.MaterialId;
                BLL.Base_MaterialService.AddMaterial(newMaterial);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_MaterialMenuId, Const.BtnAdd, this.MaterialId);
            }

            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion
    }
}