using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.BaseInfo
{
    public partial class SafetyMeasuresEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 安全措施主键
        /// </summary>
        public string SafetyMeasuresId
        {
            get
            {
                return (string)ViewState["SafetyMeasuresId"];
            }
            set
            {
                ViewState["SafetyMeasuresId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 安全措施编辑页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                ///权限
                this.GetButtonPower();
                this.SafetyMeasuresId = Request.Params["SafetyMeasuresId"];
                BLL.ConstValue.InitConstValueDropDownList(this.drpLicenseType, ConstValue.Group_LicenseType, false);                            
                if (!string.IsNullOrEmpty(this.SafetyMeasuresId))
                {
                    var SafetyMeasures = BLL.SafetyMeasuresService.GetSafetyMeasuresBySafetyMeasuresId(this.SafetyMeasuresId);
                    if (SafetyMeasures != null)
                    {
                        if (!string.IsNullOrEmpty(SafetyMeasures.LicenseType))
                        {
                            this.drpLicenseType.SelectedValue = SafetyMeasures.LicenseType;
                        }
                        this.txtSortIndex.Text = SafetyMeasures.SortIndex.ToString();
                        this.txtSafetyMeasures.Text = SafetyMeasures.SafetyMeasures;                       
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
            if (this.drpLicenseType.SelectedValue == Const._Null)
            {
                Alert.ShowInParent("请作业许可证类型！", MessageBoxIcon.Warning);
                return;
            }
            if (BLL.SafetyMeasuresService.IsExistSafetyMeasures(this.SafetyMeasuresId, this.txtSafetyMeasures.Text.Trim()))
            {
                Alert.ShowInParent("安全措施名称已存在，请修改后再保存！", MessageBoxIcon.Warning);
                return;
            }

            Model.Base_SafetyMeasures newSafetyMeasures = new Model.Base_SafetyMeasures
            {
                SortIndex = Funs.GetNewInt(this.txtSortIndex.Text.Trim()),
                SafetyMeasures = this.txtSafetyMeasures.Text.Trim()
            };
            if (this.drpLicenseType.SelectedValue != Const._Null)
            {
                newSafetyMeasures.LicenseType = this.drpLicenseType.SelectedValue;
            }
            if (string.IsNullOrEmpty(this.SafetyMeasuresId))
            {
                newSafetyMeasures.SafetyMeasuresId = SQLHelper.GetNewID(typeof(Model.Base_SafetyMeasures));
                BLL.SafetyMeasuresService.AddSafetyMeasures(newSafetyMeasures);               
                BLL.LogService.AddSys_Log(this.CurrUser, newSafetyMeasures.SortIndex.ToString(), newSafetyMeasures.SafetyMeasuresId, BLL.Const.SafetyMeasuresMenuId, Const.BtnAdd);
            }
            else
            {
                newSafetyMeasures.SafetyMeasuresId = this.SafetyMeasuresId;
                BLL.SafetyMeasuresService.UpdateSafetyMeasures(newSafetyMeasures);
                BLL.LogService.AddSys_Log(this.CurrUser, newSafetyMeasures.SortIndex.ToString(), newSafetyMeasures.SafetyMeasuresId, BLL.Const.SafetyMeasuresMenuId, Const.BtnModify);
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.SafetyMeasuresMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        #region 验证安全措施编号、名称是否存在
        /// <summary>
        /// 验证安全措施编号、名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = new Model.SGGLDB(Funs.ConnString).Base_SafetyMeasures.FirstOrDefault(x => x.SafetyMeasures == this.txtSafetyMeasures.Text.Trim() && (x.SafetyMeasuresId != this.SafetyMeasuresId || (this.SafetyMeasuresId == null && x.SafetyMeasuresId != null)));
            if (q != null)
            {
                ShowNotify("输入的名称已存在！", MessageBoxIcon.Warning);
            }

            var q2 = new Model.SGGLDB(Funs.ConnString).Base_SafetyMeasures.FirstOrDefault(x => x.SortIndex.ToString() == this.txtSortIndex.Text.Trim() && (x.SafetyMeasuresId != this.SafetyMeasuresId || (this.SafetyMeasuresId == null && x.SafetyMeasuresId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的编号已存在！", MessageBoxIcon.Warning);
            }

        }
        #endregion

    }
}