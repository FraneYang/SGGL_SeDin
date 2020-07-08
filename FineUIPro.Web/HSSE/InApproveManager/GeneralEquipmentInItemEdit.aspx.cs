using BLL;
using System;

namespace FineUIPro.Web.HSSE.InApproveManager
{
    public partial class GeneralEquipmentInItemEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string GeneralEquipmentInItemId
        {
            get
            {
                return (string)ViewState["GeneralEquipmentInItemId"];
            }
            set
            {
                ViewState["GeneralEquipmentInItemId"] = value;
            }
        }

        /// <summary>
        /// 主表主键
        /// </summary>
        public string GeneralEquipmentInId
        {
            get
            {
                return (string)ViewState["GeneralEquipmentInId"];
            }
            set
            {
                ViewState["GeneralEquipmentInId"] = value;
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
                this.GeneralEquipmentInId = Request.Params["GeneralEquipmentInId"];
                this.GeneralEquipmentInItemId = Request.Params["GeneralEquipmentInItemId"];
                if (!string.IsNullOrEmpty(this.GeneralEquipmentInItemId))
                {
                    Model.InApproveManager_GeneralEquipmentInItem generalEquipmentInItem = BLL.GeneralEquipmentInItemService.GetGeneralEquipmentInItemById(this.GeneralEquipmentInItemId);
                    if (generalEquipmentInItem!=null)
                    {
                        this.GeneralEquipmentInId = generalEquipmentInItem.GeneralEquipmentInId;
                        if (!string.IsNullOrEmpty(generalEquipmentInItem.SpecialEquipmentId))
                        {
                            this.drpSpecialEquipmentId.SelectedValue = generalEquipmentInItem.SpecialEquipmentId;
                        }
                        this.txtSizeModel.Text = generalEquipmentInItem.SizeModel;
                        this.txtOwnerCheck.Text = generalEquipmentInItem.OwnerCheck;
                        this.txtCertificateNum.Text = generalEquipmentInItem.CertificateNum;
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
            Model.InApproveManager_GeneralEquipmentInItem generalEquipmentInItem = new Model.InApproveManager_GeneralEquipmentInItem
            {
                GeneralEquipmentInId = this.GeneralEquipmentInId
            };
            if (this.drpSpecialEquipmentId.SelectedValue!=BLL.Const._Null)
            {
                generalEquipmentInItem.SpecialEquipmentId = this.drpSpecialEquipmentId.SelectedValue;
            }
            generalEquipmentInItem.SizeModel = this.txtSizeModel.Text.Trim();
            generalEquipmentInItem.OwnerCheck = this.txtOwnerCheck.Text.Trim();
            generalEquipmentInItem.CertificateNum = this.txtCertificateNum.Text.Trim();
            if (!string.IsNullOrEmpty(this.GeneralEquipmentInItemId))
            {
                generalEquipmentInItem.GeneralEquipmentInItemId = this.GeneralEquipmentInItemId;
                BLL.GeneralEquipmentInItemService.UpdateGeneralEquipmentInItem(generalEquipmentInItem);
            }
            else
            {
                this.GeneralEquipmentInItemId = SQLHelper.GetNewID(typeof(Model.InApproveManager_GeneralEquipmentInItem));
                generalEquipmentInItem.GeneralEquipmentInItemId = this.GeneralEquipmentInItemId;
                BLL.GeneralEquipmentInItemService.AddGeneralEquipmentInItem(generalEquipmentInItem);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion
    }
}