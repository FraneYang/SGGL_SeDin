using System;
using System.Linq;
using BLL;

namespace FineUIPro.Web.SysManage
{
    public partial class UnitEdit : PageBase
    {
        #region 单位主键
        /// <summary>
        /// 单位主键
        /// </summary>
        public string UnitId
        {
            get
            {
                return (string)ViewState["UnitId"];
            }
            set
            {
                ViewState["UnitId"] = value;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                ////权限按钮方法
                this.GetButtonPower();                
                UnitTypeService.InitUnitTypeDropDownList(this.ddlUnitTypeId, true);
                this.UnitId = Request.Params["UnitId"];
                if (!string.IsNullOrEmpty(this.UnitId))
                {
                    Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(this.UnitId);
                    if (unit!=null)
                    {
                        this.txtUnitCode.Text = unit.UnitCode;
                        this.txtUnitName.Text = unit.UnitName;
                        if (!string.IsNullOrEmpty(unit.UnitTypeId))
                        {
                            this.ddlUnitTypeId.SelectedValue = unit.UnitTypeId;
                        }
                        this.txtCorporate.Text = unit.Corporate;
                        this.txtAddress.Text = unit.Address;
                        this.txtTelephone.Text = unit.Telephone;
                        this.txtFax.Text = unit.Fax;
                        this.txtEMail.Text = unit.EMail;
                        this.txtProjectRange.Text = unit.ProjectRange;
                        if (unit.IsBranch == true)
                        {
                            this.rblIsBranch.SelectedValue = "true";
                        }                        
                    }
                }
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (BLL.UnitService.IsExitUnitByUnitName(this.UnitId, this.txtUnitName.Text.Trim()))
            {
                Alert.ShowInTop("单位名称已存在！",MessageBoxIcon.Warning);
                return;
            }
            if (BLL.UnitService.IsExitUnitByUnitName(this.UnitId, this.txtUnitCode.Text.Trim()))
            {
                Alert.ShowInTop("单位代码已存在！", MessageBoxIcon.Warning);
                return;
            }

            Model.Base_Unit unit = new Model.Base_Unit
            {
                UnitCode = this.txtUnitCode.Text.Trim(),
                UnitName = this.txtUnitName.Text.Trim()

            };
            unit.IsBranch = Convert.ToBoolean(this.rblIsBranch.SelectedValue);
            ////单位类型下拉框
            if (this.ddlUnitTypeId.SelectedValue != BLL.Const._Null)
            {
                unit.UnitTypeId = this.ddlUnitTypeId.SelectedValue;
            }
            unit.Corporate = this.txtCorporate.Text.Trim();
            unit.Address = this.txtAddress.Text.Trim();
            unit.Telephone = this.txtTelephone.Text.Trim();
            unit.Fax = this.txtFax.Text.Trim();
            unit.EMail = this.txtEMail.Text.Trim();
            unit.ProjectRange = this.txtProjectRange.Text.Trim();                  
            if (string.IsNullOrEmpty(this.UnitId))
            {
                unit.UnitId = SQLHelper.GetNewID(typeof(Model.Base_Unit));
                unit.DataSources = this.CurrUser.LoginProjectId;
                BLL.UnitService.AddUnit(unit);
                BLL.LogService.AddSys_Log(this.CurrUser, unit.UnitCode, unit.UnitId, BLL.Const.UnitMenuId, Const.BtnAdd);
              
            }
            else
            {
                unit.UnitId = this.UnitId;
                BLL.UnitService.UpdateUnit(unit);
                BLL.LogService.AddSys_Log(this.CurrUser, unit.UnitCode, unit.UnitId, BLL.Const.UnitMenuId, Const.BtnModify);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        
        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.UnitMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion        

    }
}