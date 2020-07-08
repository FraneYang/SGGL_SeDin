using BLL;
using System;

namespace FineUIPro.Web.HSSE.InApproveManager
{
    public partial class GeneralEquipmentOutItemEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string GeneralEquipmentOutItemId
        {
            get
            {
                return (string)ViewState["GeneralEquipmentOutItemId"];
            }
            set
            {
                ViewState["GeneralEquipmentOutItemId"] = value;
            }
        }

        /// <summary>
        /// 主表主键
        /// </summary>
        public string GeneralEquipmentOutId
        {
            get
            {
                return (string)ViewState["GeneralEquipmentOutId"];
            }
            set
            {
                ViewState["GeneralEquipmentOutId"] = value;
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
                BLL.SpecialEquipmentService.InitSpecialEquipmentDropDownList(this.drpSpecialEquipmentId, false, true);
                this.GeneralEquipmentOutId = Request.Params["GeneralEquipmentOutId"];
                this.GeneralEquipmentOutItemId = Request.Params["GeneralEquipmentOutItemId"];
                if (!string.IsNullOrEmpty(this.GeneralEquipmentOutItemId))
                {
                    Model.InApproveManager_GeneralEquipmentOutItem generalEquipmentOutItem = BLL.GeneralEquipmentOutItemService.GetGeneralEquipmentOutItemById(this.GeneralEquipmentOutItemId);
                    if (generalEquipmentOutItem!=null)
                    {
                        this.GeneralEquipmentOutId = generalEquipmentOutItem.GeneralEquipmentOutId;
                        if (!string.IsNullOrEmpty(generalEquipmentOutItem.SpecialEquipmentId))
                        {
                            this.drpSpecialEquipmentId.SelectedValue = generalEquipmentOutItem.SpecialEquipmentId;
                        }
                        this.txtSizeModel.Text = generalEquipmentOutItem.SizeModel;
                        this.txtOwnerCheck.Text = generalEquipmentOutItem.OwnerCheck;
                        this.txtCertificateNum.Text = generalEquipmentOutItem.CertificateNum;
                        this.txtInsuranceNum.Text = generalEquipmentOutItem.InsuranceNum;
                        this.drpOutReason.SelectedValue = generalEquipmentOutItem.OutReason;
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
            Model.InApproveManager_GeneralEquipmentOutItem generalEquipmentOutItem = new Model.InApproveManager_GeneralEquipmentOutItem
            {
                GeneralEquipmentOutId = this.GeneralEquipmentOutId
            };
            if (this.drpSpecialEquipmentId.SelectedValue!=BLL.Const._Null)
            {
                generalEquipmentOutItem.SpecialEquipmentId = this.drpSpecialEquipmentId.SelectedValue;
            }
            generalEquipmentOutItem.SizeModel = this.txtSizeModel.Text.Trim();
            generalEquipmentOutItem.OwnerCheck = this.txtOwnerCheck.Text.Trim();
            generalEquipmentOutItem.CertificateNum = this.txtCertificateNum.Text.Trim();
            generalEquipmentOutItem.InsuranceNum = this.txtInsuranceNum.Text.Trim();
            generalEquipmentOutItem.OutReason = this.drpOutReason.SelectedValue;
            if (!string.IsNullOrEmpty(this.GeneralEquipmentOutItemId))
            {
                generalEquipmentOutItem.GeneralEquipmentOutItemId = this.GeneralEquipmentOutItemId;
                BLL.GeneralEquipmentOutItemService.UpdateGeneralEquipmentOutItem(generalEquipmentOutItem);
            }
            else
            {
                this.GeneralEquipmentOutItemId = SQLHelper.GetNewID(typeof(Model.InApproveManager_GeneralEquipmentOutItem));
                generalEquipmentOutItem.GeneralEquipmentOutItemId = this.GeneralEquipmentOutItemId;
                BLL.GeneralEquipmentOutItemService.AddGeneralEquipmentOutItem(generalEquipmentOutItem);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion
    }
}