using BLL;
using System;

namespace FineUIPro.Web.HSSE.InApproveManager
{
    public partial class EquipmentOutItemEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string EquipmentOutItemId
        {
            get
            {
                return (string)ViewState["EquipmentOutItemId"];
            }
            set
            {
                ViewState["EquipmentOutItemId"] = value;
            }
        }

        /// <summary>
        /// 主表主键
        /// </summary>
        public string EquipmentOutId
        {
            get
            {
                return (string)ViewState["EquipmentOutId"];
            }
            set
            {
                ViewState["EquipmentOutId"] = value;
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                ///机具设备下拉框
                BLL.SpecialEquipmentService.InitSpecialEquipmentDropDownList(this.drpSpecialEquipmentId, true, true);
                this.EquipmentOutId = Request.Params["EquipmentOutId"];
                this.EquipmentOutItemId = Request.Params["EquipmentOutItemId"];
                if (!string.IsNullOrEmpty(this.EquipmentOutItemId))
                {
                    Model.InApproveManager_EquipmentOutItem equipmentOutItem = BLL.EquipmentOutItemService.GetEquipmentOutItemById(this.EquipmentOutItemId);
                    if (equipmentOutItem!=null)
                    {
                        this.EquipmentOutId = equipmentOutItem.EquipmentOutId;
                        if (!string.IsNullOrEmpty(equipmentOutItem.SpecialEquipmentId))
                        {
                            this.drpSpecialEquipmentId.SelectedValue = equipmentOutItem.SpecialEquipmentId;
                        }
                        this.txtSizeModel.Text = equipmentOutItem.SizeModel;
                        this.txtOwnerCheck.Text = equipmentOutItem.OwnerCheck;
                        this.txtCertificateNum.Text = equipmentOutItem.CertificateNum;
                        this.txtInsuranceNum.Text = equipmentOutItem.InsuranceNum;
                        this.drpOutReason.SelectedValue = equipmentOutItem.OutReason;
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
            Model.InApproveManager_EquipmentOutItem equipmentOutItem = new Model.InApproveManager_EquipmentOutItem
            {
                EquipmentOutId = this.EquipmentOutId
            };
            if (this.drpSpecialEquipmentId.SelectedValue != BLL.Const._Null)
            {
                equipmentOutItem.SpecialEquipmentId = this.drpSpecialEquipmentId.SelectedValue;
            }
            else
            {
                Alert.ShowInTop("请选择设备！", MessageBoxIcon.Warning);
                return;
            }
            equipmentOutItem.SizeModel = this.txtSizeModel.Text.Trim();
            equipmentOutItem.OwnerCheck = this.txtOwnerCheck.Text.Trim();
            equipmentOutItem.CertificateNum = this.txtCertificateNum.Text.Trim();
            equipmentOutItem.InsuranceNum = this.txtInsuranceNum.Text.Trim();
            equipmentOutItem.OutReason = this.drpOutReason.SelectedValue;
            if (!string.IsNullOrEmpty(this.EquipmentOutItemId))
            {
                equipmentOutItem.EquipmentOutItemId = this.EquipmentOutItemId;
                BLL.EquipmentOutItemService.UpdateEquipmentItemOut(equipmentOutItem);
            }
            else
            {
                this.EquipmentOutItemId = SQLHelper.GetNewID(typeof(Model.InApproveManager_EquipmentOutItem));
                equipmentOutItem.EquipmentOutItemId = this.EquipmentOutItemId;
                BLL.EquipmentOutItemService.AddEquipmentOutItem(equipmentOutItem);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion
    }
}