using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.HSSE.HiddenInspection
{
    public partial class HiddenRectificationAdd : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string HazardRegisterId
        {
            get
            {
                return (string)ViewState["HazardRegisterId"];
            }
            set
            {
                ViewState["HazardRegisterId"] = value;
            }
        }

        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImageUrl
        {
            get
            {
                return (string)ViewState["ImageUrl"];
            }
            set
            {
                ViewState["ImageUrl"] = value;
            }
        }

        /// <summary>
        /// 整改后附件路径
        /// </summary>
        public string RectificationImageUrl
        {
            get
            {
                return (string)ViewState["RectificationImageUrl"];
            }
            set
            {
                ViewState["RectificationImageUrl"] = value;
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
                UnitService.InitUnitByProjectIdUnitTypeDropDownList(this.drpUnit, this.CurrUser.LoginProjectId, Const.ProjectUnitType_2, true);
                UnitWorkService.InitUnitWorkDownList(this.drpWorkArea, this.CurrUser.LoginProjectId, true);
                UserService.InitUserProjectIdUnitTypeDropDownList(this.drpResponsibleMan, this.CurrUser.LoginProjectId, Const.ProjectUnitType_2, true);
                UserService.InitUserDropDownList(this.drpCCManIds, this.CurrUser.LoginProjectId, true);
                HSSE_Hazard_HazardRegisterTypesService.InitAccidentTypeDropDownList(this.drpRegisterTypes, "1", true);
                HSSE_Hazard_HazardRegisterTypesService.InitAccidentTypeDropDownList(this.drpRegisterTypes2, "2", true);
                HSSE_Hazard_HazardRegisterTypesService.InitAccidentTypeDropDownList(this.drpRegisterTypes3, "3", true);
                HSSE_Hazard_HazardRegisterTypesService.InitAccidentTypeDropDownList(this.drpRegisterTypes4, "4", true);
                Funs.FineUIPleaseSelect(this.drpHazardValue);
                this.HazardRegisterId = Request.Params["HazardRegisterId"];
                //新增初始化
                this.txtCheckManName.Text = this.CurrUser.UserName;
                this.hdCheckManId.Text = this.CurrUser.UserId;
                this.txtCheckTime.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
                this.txtRectificationPeriod.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now.AddDays(1));
                if (!string.IsNullOrEmpty(this.HazardRegisterId))
                {
                    var registration = Funs.DB.View_Hazard_HazardRegister.FirstOrDefault(x => x.HazardRegisterId == HazardRegisterId);
                    if (registration != null)
                    {
                        if (!string.IsNullOrEmpty(registration.ResponsibleUnit))
                        {
                            this.drpUnit.SelectedValue = registration.ResponsibleUnit;
                        }
                        this.drpUnit_OnSelectedIndexChanged(null, null);
                        if (!string.IsNullOrEmpty(registration.Place))
                        {
                            this.drpWorkArea.SelectedValue = registration.Place;
                        }
                        if (!string.IsNullOrEmpty(registration.RegisterTypesId))
                        {
                            this.drpRegisterTypes.SelectedValue = registration.RegisterTypesId;
                        }
                        if (!string.IsNullOrEmpty(registration.RegisterTypes2Id))
                        {
                            this.drpRegisterTypes2.SelectedValue = registration.RegisterTypes2Id;
                        }
                        if (!string.IsNullOrEmpty(registration.RegisterTypes3Id))
                        {
                            this.drpRegisterTypes3.SelectedValue = registration.RegisterTypes3Id;
                        }
                        if (!string.IsNullOrEmpty(registration.RegisterTypes4Id))
                        {
                            this.drpRegisterTypes4.SelectedValue = registration.RegisterTypes4Id;
                        }
                        if (!string.IsNullOrEmpty(registration.HazardValue))
                        {
                            this.drpHazardValue.SelectedValue = registration.HazardValue;
                        }
                        if (!string.IsNullOrEmpty(registration.ResponsibleMan))
                        {
                            this.drpResponsibleMan.SelectedValue = registration.ResponsibleMan;
                        }
                        if (registration.RectificationPeriod != null)
                        {
                            this.txtRectificationPeriod.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", registration.RectificationPeriod);
                        }
                        
                        this.txtRegisterDef.Text = registration.RegisterDef;
                        this.txtRequirements.Text = registration.Requirements;
                        this.txtCheckManName.Text = registration.CheckManName;
                        this.hdCheckManId.Text = registration.CheckManId;
                        if (registration.CheckTime != null)
                        {
                            this.txtCheckTime.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", registration.CheckTime);
                        }
                        if (!string.IsNullOrEmpty(registration.CCManIds))
                        {
                            this.drpCCManIds.SelectedValueArray = registration.CCManIds.Split(',');
                        }
                        if (!string.IsNullOrEmpty(registration.HandleIdea))
                        {
                            this.txtHandleIdea.Hidden = false;
                            this.txtHandleIdea.Text = registration.HandleIdea;
                        }
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 单位选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpUnit_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpWorkArea.Items.Clear();
            this.drpResponsibleMan.Items.Clear();
            if (this.drpUnit.SelectedValue != BLL.Const._Null)
            {
                UnitWorkService.InitUnitWorkDownList(this.drpWorkArea, this.CurrUser.LoginProjectId, true);
                UserService.InitUserProjectIdUnitIdDropDownList(this.drpResponsibleMan, this.CurrUser.LoginProjectId, this.drpUnit.SelectedValue, true);
                this.drpWorkArea.SelectedValue = BLL.Const._Null;
                this.drpResponsibleMan.SelectedValue = BLL.Const._Null;
            }
        }

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSE_HiddenRectificationListMenuId, BLL.Const.BtnAdd))
            {
                if (this.drpUnit.SelectedValue == BLL.Const._Null)
                {
                    ShowNotify("请选择责任单位！", MessageBoxIcon.Warning);
                    return;
                }
                if (this.drpWorkArea.SelectedValue == BLL.Const._Null)
                {
                    ShowNotify("请选择单位工程！", MessageBoxIcon.Warning);
                    return;
                }
                if (this.drpWorkArea.SelectedValue == BLL.Const._Null)
                {
                    ShowNotify("请选择责任人！", MessageBoxIcon.Warning);
                    return;
                }
                if (this.drpRegisterTypes.SelectedValue == BLL.Const._Null)
                {
                    ShowNotify("请选择问题类型！", MessageBoxIcon.Warning);
                    return;
                }
                SaveData(true);
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="p"></param>
        private void SaveData(bool isClosed)
        {
            Model.HSSE_Hazard_HazardRegister register = new Model.HSSE_Hazard_HazardRegister();
            register.ProjectId = this.CurrUser.LoginProjectId;
            register.ProblemTypes = "1";    //安全隐患问题
            register.RegisterTypesId = this.drpRegisterTypes.SelectedValue;          
            register.IsEffective = "1";
            if (this.drpRegisterTypes2.SelectedValue != BLL.Const._Null)
            {
                register.RegisterTypes2Id = this.drpRegisterTypes2.SelectedValue;
            }
            if (this.drpRegisterTypes3.SelectedValue != BLL.Const._Null)
            {
                register.RegisterTypes3Id = this.drpRegisterTypes3.SelectedValue;
            }
            if (this.drpRegisterTypes4.SelectedValue != BLL.Const._Null)
            {
                register.RegisterTypes4Id = this.drpRegisterTypes4.SelectedValue;
            }
            if (this.drpHazardValue.SelectedValue != BLL.Const._Null)
            {
                register.HazardValue = this.drpHazardValue.SelectedValue;
            }
            if (this.drpUnit.SelectedValue != BLL.Const._Null)
            {
                register.ResponsibleUnit = this.drpUnit.SelectedValue;
            }
            if (this.drpWorkArea.SelectedValue != BLL.Const._Null)
            {
                register.Place = this.drpWorkArea.SelectedValue;
            }
            register.RegisterDef = this.txtRegisterDef.Text.Trim();
            register.Requirements = this.txtRequirements.Text.Trim();
            if (this.drpResponsibleMan.SelectedValue != BLL.Const._Null)
            {
                register.ResponsibleMan = this.drpResponsibleMan.SelectedValue;
            }
            if (this.drpCCManIds.SelectedValue != BLL.Const._Null)
            {
                register.CCManIds = Funs.GetStringByArray(this.drpCCManIds.SelectedValueArray);
            }
            register.RectificationPeriod = Funs.GetNewDateTime(this.txtRectificationPeriod.Text.Trim());
            register.CheckManId = this.hdCheckManId.Text;
            register.States = "1";    //待整改
            if (!string.IsNullOrEmpty(HazardRegisterId))
            {
                register.HazardRegisterId = HazardRegisterId;
                BLL.HSSE_Hazard_HazardRegisterService.UpdateHazardRegister(register);
                BLL.LogService.AddSys_Log(this.CurrUser, register.HazardCode, register.HazardRegisterId, BLL.Const.HiddenRectificationMenuId, BLL.Const.BtnModify);
            }
            else
            {
                register.HazardRegisterId = SQLHelper.GetNewID(typeof(Model.HSSE_Hazard_HazardRegister));
                HazardRegisterId = register.HazardRegisterId;
                register.CheckTime = DateTime.Now;
                BLL.HSSE_Hazard_HazardRegisterService.AddHazardRegister(register);
                BLL.LogService.AddSys_Log(this.CurrUser, register.HazardCode, register.HazardRegisterId, BLL.Const.HiddenRectificationMenuId, BLL.Const.BtnAdd);
            }
            if (isClosed)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.HazardRegisterId))
            {
                SaveData(false);
            }
            string edit = "0";
            if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSE_HiddenRectificationListMenuId, BLL.Const.BtnAdd))
            {
                edit = "1";
                Model.HSSE_Hazard_HazardRegister register = BLL.HSSE_Hazard_HazardRegisterService.GetHazardRegisterByHazardRegisterId(this.HazardRegisterId);
                DateTime date = Convert.ToDateTime(register.CheckTime);
                string dateStr = date.Year.ToString() + date.Month.ToString();
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Registration/" + dateStr + "&menuId={1}&edit={2}", this.HazardRegisterId, Const.HSSE_HiddenRectificationListMenuId, edit)));
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpCCManIds_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpCCManIds.SelectedValueArray = Funs.RemoveDropDownListNull(this.drpCCManIds.SelectedValueArray);
        }
    }
}