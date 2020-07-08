using BLL;
using System;

namespace FineUIPro.Web.HSSE.QualityAudit
{
    public partial class GeneralEquipmentQualityEdit :PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string GeneralEquipmentQualityId
        {
            get
            {
                return (string)ViewState["GeneralEquipmentQualityId"];
            }
            set
            {
                ViewState["GeneralEquipmentQualityId"] = value;
            }
        }
        /// <summary>
        /// 二维码路径id
        /// </summary>
        public string QRCodeAttachUrl
        {
            get
            {
                return (string)ViewState["QRCodeAttachUrl"];
            }
            set
            {
                ViewState["QRCodeAttachUrl"] = value;
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
                BLL.UnitService.InitUnitDropDownList(this.drpUnitId, this.CurrUser.LoginProjectId, true);
                ///机具设备下拉框
                BLL.SpecialEquipmentService.InitSpecialEquipmentDropDownList(this.drpSpecialEquipmentId, false, true);

                BLL.ConstValue.InitConstValueDropDownList(this.drpIsQualified, ConstValue.Group_0001, false);
                this.GeneralEquipmentQualityId = Request.Params["GeneralEquipmentQualityId"];
                if (!string.IsNullOrEmpty(this.GeneralEquipmentQualityId))
                {
                    Model.QualityAudit_GeneralEquipmentQuality generalEquipmentQuality = BLL.GeneralEquipmentQualityService.GetGeneralEquipmentQualityById(this.GeneralEquipmentQualityId);
                    if (generalEquipmentQuality != null)
                    {
                        this.txtGeneralEquipmentQualityCode.Text = CodeRecordsService.ReturnCodeByDataId(this.GeneralEquipmentQualityId);
                        if (!string.IsNullOrEmpty(generalEquipmentQuality.UnitId))
                        {
                            this.drpUnitId.SelectedValue = generalEquipmentQuality.UnitId;
                        }
                        if (!string.IsNullOrEmpty(generalEquipmentQuality.SpecialEquipmentId))
                        {
                            this.drpSpecialEquipmentId.SelectedValue = generalEquipmentQuality.SpecialEquipmentId;
                        }
                        if (generalEquipmentQuality.EquipmentCount != null)
                        {
                            this.txtEquipmentCount.Text = Convert.ToString(generalEquipmentQuality.EquipmentCount);
                        }
                        if (generalEquipmentQuality.InDate != null)
                        {
                            this.txtInDate.Text = string.Format("{0:yyyy-MM-dd}",generalEquipmentQuality.InDate);
                        }
                        this.drpIsQualified.SelectedValue = Convert.ToString(generalEquipmentQuality.IsQualified);
                        this.txtRemark.Text = generalEquipmentQuality.Remark;
                    }
                }
                else
                {
                    ////自动生成编码
                    this.txtGeneralEquipmentQualityCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.GeneralEquipmentQualityMenuId, this.CurrUser.LoginProjectId, this.CurrUser.UnitId);
                    this.txtInDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }

                if (Request.Params["value"] == "0")
                {
                    this.btnSave.Hidden = true;
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
            if (this.drpUnitId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择单位名称", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpSpecialEquipmentId.SelectedValue==BLL.Const._Null)
            {
                Alert.ShowInTop("请选择机具设备类型", MessageBoxIcon.Warning);
                return;                
            }
            SaveData(true);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="isClose"></param>
        private void SaveData(bool isClose)
        {
            Model.QualityAudit_GeneralEquipmentQuality generalEquipmentQuality = new Model.QualityAudit_GeneralEquipmentQuality
            {
                ProjectId = this.CurrUser.LoginProjectId,
                GeneralEquipmentQualityCode = this.txtGeneralEquipmentQualityCode.Text.Trim()
            };
            if (this.drpUnitId.SelectedValue!=BLL.Const._Null)
            {
                generalEquipmentQuality.UnitId = this.drpUnitId.SelectedValue;
            }
            if (this.drpSpecialEquipmentId.SelectedValue != BLL.Const._Null)
            {
                generalEquipmentQuality.SpecialEquipmentId = this.drpSpecialEquipmentId.SelectedValue;
            }
            generalEquipmentQuality.InDate = Funs.GetNewDateTimeOrNow(this.txtInDate.Text.Trim());
            generalEquipmentQuality.EquipmentCount = Funs.GetNewInt(this.txtEquipmentCount.Text.Trim());
            generalEquipmentQuality.IsQualified =Convert.ToBoolean( this.drpIsQualified.SelectedValue);
            generalEquipmentQuality.Remark = this.txtRemark.Text.Trim();
            generalEquipmentQuality.CompileMan = this.CurrUser.UserId;
            generalEquipmentQuality.CompileDate = DateTime.Now;
            if (!string.IsNullOrEmpty(this.GeneralEquipmentQualityId))
            {
                generalEquipmentQuality.GeneralEquipmentQualityId = this.GeneralEquipmentQualityId;
                BLL.GeneralEquipmentQualityService.UpdateGeneralEquipmentQuality(generalEquipmentQuality);
                BLL.LogService.AddSys_Log(this.CurrUser, generalEquipmentQuality.GeneralEquipmentQualityCode, generalEquipmentQuality.GeneralEquipmentQualityId, BLL.Const.GeneralEquipmentQualityMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.GeneralEquipmentQualityId = SQLHelper.GetNewID(typeof(Model.QualityAudit_GeneralEquipmentQuality));
                generalEquipmentQuality.GeneralEquipmentQualityId = this.GeneralEquipmentQualityId;
                BLL.GeneralEquipmentQualityService.AddGeneralEquipmentQuality(generalEquipmentQuality);
                BLL.LogService.AddSys_Log(this.CurrUser, generalEquipmentQuality.GeneralEquipmentQualityCode, generalEquipmentQuality.GeneralEquipmentQualityId, BLL.Const.GeneralEquipmentQualityMenuId, BLL.Const.BtnAdd);
            }
            if (isClose)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
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
            if (this.btnSave.Hidden)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/GeneralEquipmentQualityAttachUrl&type=-1", GeneralEquipmentQualityId, BLL.Const.GeneralEquipmentQualityMenuId)));
            }
            else
            {
                if (this.drpUnitId.SelectedValue == BLL.Const._Null)
                {
                    Alert.ShowInTop("请选择单位名称", MessageBoxIcon.Warning);
                    return;
                }
                if (this.drpSpecialEquipmentId.SelectedValue == BLL.Const._Null)
                {
                    Alert.ShowInTop("请选择机具设备类型", MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(this.GeneralEquipmentQualityId))
                {
                    SaveData(false);
                }
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/GeneralEquipmentQualityAttachUrl&menuId={1}", GeneralEquipmentQualityId, BLL.Const.GeneralEquipmentQualityMenuId)));
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQR_Click(object sender, EventArgs e)
        {
            if (this.drpUnitId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择单位名称", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpSpecialEquipmentId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择机具设备类型", MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(this.GeneralEquipmentQualityId))
            {
                this.SaveData(false);
            }

            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("~/Controls/SeeQRImage.aspx?GeneralEquipmentQualityId={0}&strCode={1}", this.GeneralEquipmentQualityId, "equipment$" + this.GeneralEquipmentQualityId), "二维码查看", 400, 400));            
        }
    }
}