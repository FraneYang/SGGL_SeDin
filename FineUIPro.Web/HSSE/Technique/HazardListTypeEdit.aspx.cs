using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.HSSE.Technique
{
    public partial class HazardListTypeEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string HazardListTypeId
        {
            get
            {
                return (string)ViewState["HazardListTypeId"];
            }
            set
            {
                ViewState["HazardListTypeId"] = value;
            }
        }

        /// <summary>
        /// 上级菜单Id
        /// </summary>
        public string SupHazardListTypeId
        {
            get
            {
                return (string)ViewState["SupHazardListTypeId"];
            }
            set
            {
                ViewState["SupHazardListTypeId"] = value;
            }
        }
        #endregion

        #region 加载页面
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
                ////权限按钮方法
                this.GetButtonPower();               
                BLL.ConstValue.InitConstValueDropDownList(this.drpIsEndLever, ConstValue.Group_0001, false);               
                BLL.WorkStageService.InitWorkPostDropDownList(this.drpWorkStage, true);
                this.HazardListTypeId = Request.Params["HazardListTypeId"];
                this.SupHazardListTypeId = Request.Params["SupHazardListTypeId"];
                if (this.SupHazardListTypeId != "0" && !string.IsNullOrEmpty(SupHazardListTypeId))
                {
                    this.drpWorkStage.Hidden = true;
                }
                if (!string.IsNullOrEmpty(this.HazardListTypeId))
                {
                    Model.Technique_HazardListType hazardListType = BLL.HazardListTypeService.GetHazardListTypeById(this.HazardListTypeId);
                    if (hazardListType != null)
                    {
                        this.txtHazardListTypeCode.Text = hazardListType.HazardListTypeCode;
                        this.txtHazardListTypeName.Text = hazardListType.HazardListTypeName;
                        if (hazardListType.IsEndLevel != null)
                        {
                            this.drpIsEndLever.SelectedValue = Convert.ToString(hazardListType.IsEndLevel);
                        }
                        if (hazardListType.SupHazardListTypeId != "0")
                        {
                            this.drpWorkStage.Hidden = true;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(hazardListType.WorkStage))
                            {
                                this.drpWorkStage.SelectedValueArray = hazardListType.WorkStage.Split(',');
                            }
                        }
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
            Model.Technique_HazardListType hazardListType = new Model.Technique_HazardListType
            {
                HazardListTypeCode = this.txtHazardListTypeCode.Text.Trim(),
                HazardListTypeName = this.txtHazardListTypeName.Text.Trim(),
                IsEndLevel = Convert.ToBoolean(this.drpIsEndLever.SelectedValue)
            };
            //参与单位
            string workStage = string.Empty;
            foreach (var item in this.drpWorkStage.SelectedValueArray)
            {
                var wor = BLL.WorkStageService.GetWorkStageById(item);
                if (wor != null)
                {
                    workStage += item + ",";
                }
            }
            if (!string.IsNullOrEmpty(workStage))
            {
                workStage = workStage.Substring(0, workStage.LastIndexOf(","));
            }
            hazardListType.WorkStage = workStage;
            if (string.IsNullOrEmpty(this.HazardListTypeId))
            {
                hazardListType.IsCompany = Convert.ToBoolean(Request.Params["IsCompany"]);
                hazardListType.HazardListTypeId = SQLHelper.GetNewID(typeof(Model.Technique_HazardListType));
                hazardListType.SupHazardListTypeId = this.SupHazardListTypeId;
                BLL.HazardListTypeService.AddHazardListType(hazardListType);
                BLL.LogService.AddSys_Log(this.CurrUser, hazardListType.HazardListTypeCode, hazardListType.HazardListTypeId, BLL.Const.HazardListMenuId, Const.BtnAdd);
            }
            else
            {
                hazardListType.HazardListTypeId = this.HazardListTypeId;
                Model.Technique_HazardListType t = BLL.HazardListTypeService.GetHazardListTypeById(this.HazardListTypeId);
                if (t != null)
                {
                    hazardListType.SupHazardListTypeId = t.SupHazardListTypeId;
                }
                BLL.HazardListTypeService.UpdateHazardListType(hazardListType);
                BLL.LogService.AddSys_Log(this.CurrUser, hazardListType.HazardListTypeCode, hazardListType.HazardListTypeId, BLL.Const.HazardListMenuId, Const.BtnModify);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        #region 验证危险源名称是否存在
        /// <summary>
        /// 验证危险源名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = new Model.SGGLDB(Funs.ConnString).Technique_HazardListType.FirstOrDefault(x => x.SupHazardListTypeId == this.SupHazardListTypeId && x.HazardListTypeName == this.txtHazardListTypeName.Text.Trim() && (x.HazardListTypeId != this.HazardListTypeId || (this.HazardListTypeId == null && x.HazardListTypeId != null)));
            if (q != null)
            {
                ShowNotify("输入的名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HazardListMenuId);
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