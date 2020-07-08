using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.HSSE.InApproveManager
{
    public partial class EquipmentQualityInEdit : PageBase
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
        /// <summary>
        /// 项目主键
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.InitDropDownList();
                this.EquipmentQualityInId = Request.Params["EquipmentQualityInId"];
                if (!string.IsNullOrEmpty(this.EquipmentQualityInId))
                {
                    Model.InApproveManager_EquipmentQualityIn EquipmentQualityIn = BLL.EquipmentQualityInService.GetEquipmentQualityInById(this.EquipmentQualityInId);
                    if (EquipmentQualityIn != null)
                    {
                        this.ProjectId = EquipmentQualityIn.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        if (!string.IsNullOrEmpty(EquipmentQualityIn.UnitId))
                        {
                            this.drpUnitId.SelectedValue = EquipmentQualityIn.UnitId;
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
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }
        #endregion

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            this.drpUnitId.DataValueField = "UnitId";
            this.drpUnitId.DataTextField = "UnitName";
            this.drpUnitId.DataSource = BLL.UnitService.GetUnitByProjectIdList(this.ProjectId);
            this.drpUnitId.DataBind();
            Funs.FineUIPleaseSelect(this.drpUnitId);
        }

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.EquipmentQualityInId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/EquipmentQualityInAttachUrl&menuId={1}", this.EquipmentQualityInId, BLL.Const.EquipmentQualityInMenuId)));
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.InApproveManager_EquipmentQualityIn EquipmentQualityIn = new Model.InApproveManager_EquipmentQualityIn
            {
                ProjectId = this.ProjectId
            };
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                EquipmentQualityIn.UnitId = this.drpUnitId.SelectedValue;
            }
            EquipmentQualityIn.DriverName = this.txtDriverName.Text.Trim();
            EquipmentQualityIn.CarNum = this.txtCarNum.Text.Trim();
            EquipmentQualityIn.CarType = this.txtCarType.Text.Trim();
            EquipmentQualityIn.States = BLL.Const.State_0;
            EquipmentQualityIn.DutyMan = this.txtDutyMan.Text.Trim();
            if (type == BLL.Const.BtnSubmit)
            {
                EquipmentQualityIn.States = ctlAuditFlow.NextStep;
            }
            EquipmentQualityIn.CompileMan = this.CurrUser.UserId;
            EquipmentQualityIn.CompileDate = DateTime.Now;
            if (!string.IsNullOrEmpty(EquipmentQualityInId))
            {
                EquipmentQualityIn.EquipmentQualityInId = this.EquipmentQualityInId;
                BLL.EquipmentQualityInService.UpdateEquipmentQualityIn(EquipmentQualityIn);
                BLL.LogService.AddSys_Log(this.CurrUser, EquipmentQualityIn.DriverName, EquipmentQualityIn.EquipmentQualityInId,BLL.Const.EquipmentQualityInMenuId,BLL.Const.BtnModify);
            }
            else
            {
                this.EquipmentQualityInId = SQLHelper.GetNewID(typeof(Model.InApproveManager_EquipmentQualityIn));
                EquipmentQualityIn.EquipmentQualityInId = this.EquipmentQualityInId;
                BLL.EquipmentQualityInService.AddEquipmentQualityIn(EquipmentQualityIn);
                BLL.LogService.AddSys_Log(this.CurrUser, EquipmentQualityIn.DriverName, EquipmentQualityIn.EquipmentQualityInId, BLL.Const.EquipmentQualityInMenuId, BLL.Const.BtnAdd);
            }
            BLL.EquipmentQualityInItemService.DeleteEquipmentQualityInItemByEquipmentQualityInId(this.EquipmentQualityInId);
            Model.InApproveManager_EquipmentQualityInItem EquipmentQualityInItem = new Model.InApproveManager_EquipmentQualityInItem
            {
                EquipmentQualityInItemId = SQLHelper.GetNewID(typeof(Model.InApproveManager_EquipmentQualityInItem)),
                EquipmentQualityInId = this.EquipmentQualityInId,
                CheckItem1 = this.ckbCheckItem1.Checked,
                CheckItem2 = this.ckbCheckItem2.Checked,
                CheckItem3 = this.ckbCheckItem3.Checked,
                CheckItem4 = this.ckbCheckItem4.Checked,
                CheckItem5 = this.ckbCheckItem5.Checked,
                CheckItem6 = this.ckbCheckItem6.Checked,
                CheckItem7 = this.ckbCheckItem7.Checked,
                CheckItem8 = this.ckbCheckItem8.Checked,
                CheckItem9 = this.ckbCheckItem9.Checked,
                CheckItem10 = this.ckbCheckItem10.Checked,
                CheckItem11 = this.ckbCheckItem11.Checked,
                CheckItem12 = this.ckbCheckItem12.Checked,
                CheckItem13 = this.ckbCheckItem13.Checked,
                CheckItem14 = this.ckbCheckItem14.Checked,
                CheckItem15 = this.ckbCheckItem15.Checked,
                CheckItem16 = this.ckbCheckItem16.Checked,
                CheckItem17 = this.ckbCheckItem17.Checked,
                CheckItem18 = this.ckbCheckItem18.Checked,
                CheckItem19 = this.ckbCheckItem19.Checked,
                CheckItem20 = this.ckbCheckItem20.Checked
            };
            BLL.EquipmentQualityInItemService.AddEquipmentQualityInItem(EquipmentQualityInItem);

            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.EquipmentQualityInMenuId, this.EquipmentQualityInId, (type == BLL.Const.BtnSubmit ? true : false), (EquipmentQualityIn.DriverName + EquipmentQualityIn.CarNum), "../InApproveManager/EquipmentInView.aspx?EquipmentInId={0}");
        }

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
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.drpUnitId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择单位名称", MessageBoxIcon.Warning);
                return;
            }
            if (this.ctlAuditFlow.NextStep == BLL.Const.State_1 && this.ctlAuditFlow.NextPerson == BLL.Const._Null)
            {
                ShowNotify("请选择下一步办理人！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        #endregion
    }
}