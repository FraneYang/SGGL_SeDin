using BLL;
using System;

namespace FineUIPro.Web.HSSE.InApproveManager
{
    public partial class EquipmentInItemEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string EquipmentInItemId
        {
            get
            {
                return (string)ViewState["EquipmentInItemId"];
            }
            set
            {
                ViewState["EquipmentInItemId"] = value;
            }
        }

        /// <summary>
        /// 主表主键
        /// </summary>
        public string EquipmentInId
        {
            get
            {
                return (string)ViewState["EquipmentInId"];
            }
            set
            {
                ViewState["EquipmentInId"] = value;
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
                ///机具设备下拉框
                BLL.SpecialEquipmentService.InitSpecialEquipmentDropDownList(this.drpSpecialEquipmentId, true, true);

                this.EquipmentInId = Request.Params["EquipmentInId"];
                this.EquipmentInItemId = Request.Params["EquipmentInItemId"];
                if (!string.IsNullOrEmpty(this.EquipmentInItemId))
                {
                    Model.InApproveManager_EquipmentInItem equipmentInItem = BLL.EquipmentInItemService.GetEquipmentInItemById(this.EquipmentInItemId);
                    if (equipmentInItem != null)
                    {
                        this.EquipmentInId = equipmentInItem.EquipmentInId;
                        if (!string.IsNullOrEmpty(equipmentInItem.SpecialEquipmentId))
                        {
                            this.drpSpecialEquipmentId.SelectedValue = equipmentInItem.SpecialEquipmentId;
                        }
                        this.txtSizeModel.Text = equipmentInItem.SizeModel;
                        this.txtOwnerCheck.Text = equipmentInItem.OwnerCheck;
                        this.txtCertificateNum.Text = equipmentInItem.CertificateNum;
                        this.txtSafetyInspectionNum.Text = equipmentInItem.SafetyInspectionNum;
                        this.txtDrivingLicenseNum.Text = equipmentInItem.DrivingLicenseNum;
                        this.txtRegistrationNum.Text = equipmentInItem.RegistrationNum;
                        this.txtOperationQualificationNum.Text = equipmentInItem.OperationQualificationNum;
                        this.txtInsuranceNum.Text = equipmentInItem.InsuranceNum;
                        this.txtCommercialInsuranceNum.Text = equipmentInItem.CommercialInsuranceNum;
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
            Model.InApproveManager_EquipmentInItem equipmentInItem = new Model.InApproveManager_EquipmentInItem
            {
                EquipmentInId = this.EquipmentInId
            };
            if (this.drpSpecialEquipmentId.SelectedValue != BLL.Const._Null)
            {
                equipmentInItem.SpecialEquipmentId = this.drpSpecialEquipmentId.SelectedValue;
            }
            else
            {
                Alert.ShowInTop("请选择设备", MessageBoxIcon.Warning);
                return;
            }
            equipmentInItem.SizeModel = this.txtSizeModel.Text.Trim();
            equipmentInItem.OwnerCheck = this.txtOwnerCheck.Text.Trim();
            equipmentInItem.CertificateNum = this.txtCertificateNum.Text.Trim();
            equipmentInItem.SafetyInspectionNum = this.txtSafetyInspectionNum.Text.Trim();
            equipmentInItem.DrivingLicenseNum = this.txtDrivingLicenseNum.Text.Trim();
            equipmentInItem.RegistrationNum = this.txtRegistrationNum.Text.Trim();
            equipmentInItem.OperationQualificationNum = this.txtOperationQualificationNum.Text.Trim();
            equipmentInItem.InsuranceNum = this.txtInsuranceNum.Text.Trim();
            equipmentInItem.CommercialInsuranceNum = this.txtCommercialInsuranceNum.Text.Trim();
            if (!string.IsNullOrEmpty(this.EquipmentInItemId))
            {
                equipmentInItem.EquipmentInItemId = this.EquipmentInItemId;
                BLL.EquipmentInItemService.UpdateEquipmentInItem(equipmentInItem);
            }
            else
            {
                this.EquipmentInItemId = SQLHelper.GetNewID(typeof(Model.InApproveManager_EquipmentInItem));
                equipmentInItem.EquipmentInItemId = this.EquipmentInItemId;
                BLL.EquipmentInItemService.AddEquipmentInItem(equipmentInItem);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion
    }
}