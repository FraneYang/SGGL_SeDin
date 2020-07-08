using System;
using System.Collections.Generic;

namespace FineUIPro.Web.HSSE.InApproveManager
{
    public partial class EquipmentQualityInView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键 
        /// </summary>
        private string EquipmentQualityInId
        {
            get
            {
                return (string)ViewState["EquipmentQualityInId"];
            }
            set
            {
                ViewState["EquipmentQualityInId"] = value;
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
                this.EquipmentQualityInId = Request.Params["EquipmentQualityInId"];
                if (!string.IsNullOrEmpty(this.EquipmentQualityInId))
                {
                    Model.InApproveManager_EquipmentQualityIn EquipmentQualityIn = BLL.EquipmentQualityInService.GetEquipmentQualityInById(this.EquipmentQualityInId);
                    if (EquipmentQualityIn != null)
                    {
                        if (!string.IsNullOrEmpty(EquipmentQualityIn.UnitId))
                        {
                            var unit = BLL.UnitService.GetUnitByUnitId(EquipmentQualityIn.UnitId);
                            if (unit!=null)
                            {
                                this.txtUnitName.Text = unit.UnitName;
                            }
                        }
                        this.txtDriverName.Text = EquipmentQualityIn.DriverName;
                        this.txtCarNum.Text = EquipmentQualityIn.CarNum;
                        this.txtCarType.Text = EquipmentQualityIn.CarType;
                        this.txtDutyMan.Text = EquipmentQualityIn.DutyMan;
                    }
                    List<Model.InApproveManager_EquipmentQualityInItem> carChecks = BLL.EquipmentQualityInItemService.GetEquipmentQualityInItemByEquipmentQualityInId(this.EquipmentQualityInId);
                    if (carChecks != null && carChecks.Count > 0)
                    {
                        this.ckbCheckItem1.Checked = Convert.ToBoolean(carChecks[0].CheckItem1);
                        this.ckbCheckItem2.Checked = Convert.ToBoolean(carChecks[0].CheckItem2);
                        this.ckbCheckItem3.Checked = Convert.ToBoolean(carChecks[0].CheckItem3);
                        this.ckbCheckItem4.Checked = Convert.ToBoolean(carChecks[0].CheckItem4);
                        this.ckbCheckItem5.Checked = Convert.ToBoolean(carChecks[0].CheckItem5);
                        this.ckbCheckItem6.Checked = Convert.ToBoolean(carChecks[0].CheckItem6);
                        this.ckbCheckItem7.Checked = Convert.ToBoolean(carChecks[0].CheckItem7);
                        this.ckbCheckItem8.Checked = Convert.ToBoolean(carChecks[0].CheckItem8);
                        this.ckbCheckItem9.Checked = Convert.ToBoolean(carChecks[0].CheckItem9);
                        this.ckbCheckItem10.Checked = Convert.ToBoolean(carChecks[0].CheckItem10);
                        this.ckbCheckItem11.Checked = Convert.ToBoolean(carChecks[0].CheckItem11);
                        this.ckbCheckItem12.Checked = Convert.ToBoolean(carChecks[0].CheckItem12);
                        this.ckbCheckItem13.Checked = Convert.ToBoolean(carChecks[0].CheckItem13);
                        this.ckbCheckItem14.Checked = Convert.ToBoolean(carChecks[0].CheckItem14);
                        this.ckbCheckItem15.Checked = Convert.ToBoolean(carChecks[0].CheckItem15);
                        this.ckbCheckItem16.Checked = Convert.ToBoolean(carChecks[0].CheckItem16);
                        this.ckbCheckItem17.Checked = Convert.ToBoolean(carChecks[0].CheckItem17);
                        this.ckbCheckItem18.Checked = Convert.ToBoolean(carChecks[0].CheckItem18);
                        this.ckbCheckItem19.Checked = Convert.ToBoolean(carChecks[0].CheckItem19);
                        this.ckbCheckItem20.Checked = Convert.ToBoolean(carChecks[0].CheckItem20);
                    }
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.EquipmentQualityInMenuId;
                this.ctlAuditFlow.DataId = this.EquipmentQualityInId;
            }
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.EquipmentQualityInId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/EquipmentQualityInAttachUrl&menuId={1}&type=-1", this.EquipmentQualityInId, BLL.Const.EquipmentQualityInMenuId)));
            }
        }
        #endregion
    }
}