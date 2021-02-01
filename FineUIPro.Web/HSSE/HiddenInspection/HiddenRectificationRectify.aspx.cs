using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.HiddenInspection
{
    public partial class HiddenRectificationRectify : PageBase
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
                this.HazardRegisterId = Request.Params["HazardRegisterId"];
                //新增初始化
                if (!string.IsNullOrEmpty(this.HazardRegisterId))
                {
                    var registration = Funs.DB.View_Hazard_HazardRegister.FirstOrDefault(x=> x.HazardRegisterId == HazardRegisterId);
                    if (registration != null)
                    {
                        this.txtRegisterDef.Text = registration.RegisterDef;
                        this.txtCheckManName.Text = registration.CheckManName;
                        this.txtCheckTime.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", registration.CheckTime);
                        this.drpRegisterTypes.Text = registration.RegisterTypesName;
                        this.drpRegisterTypes2.Text = registration.RegisterTypes2Name;
                        this.drpRegisterTypes3.Text = registration.RegisterTypes3Name;
                        this.drpRegisterTypes4.Text = registration.RegisterTypes4Name;
                        this.drpHazardValue.Text = registration.HazardValue;
                        this.txtRequirements.Text = registration.Requirements;
                        this.drpUnit.Text = registration.ResponsibilityUnitName;
                        this.drpResponsibleMan.Text = registration.ResponsibilityManName;
                        this.drpWorkArea.Text = registration.WorkAreaName;
                        this.txtRectificationPeriod.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", registration.RectificationPeriod);
                        this.drpCCManIds.Text = registration.CCManNames;
                        this.txtRectification.Text = registration.Rectification;
                        this.txtHandleIdea.Text = registration.HandleIdea;
                        if (!string.IsNullOrEmpty(registration.HandleIdea))
                        {
                            this.txtHandleIdea.Hidden = false;                            
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
            if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSE_HiddenRectificationListMenuId, BLL.Const.BtnSave))
            {
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
            Model.HSSE_Hazard_HazardRegister register = BLL.HSSE_Hazard_HazardRegisterService.GetHazardRegisterByHazardRegisterId(this.HazardRegisterId);
            if (register != null)
            {
                register.States = "2";   //已整改待确认
                register.Rectification = this.txtRectification.Text.Trim();
                register.RectificationTime = DateTime.Now;
                BLL.HSSE_Hazard_HazardRegisterService.UpdateHazardRegister(register);
                BLL.LogService.AddSys_Log(this.CurrUser, register.HazardCode, register.HazardRegisterId, BLL.Const.HSSE_HiddenRectificationListMenuId, BLL.Const.BtnModify);
                if (isClosed)
                {
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                }
            }
        }
        #endregion

        #region 附件查看
        /// <summary>
        /// 附件查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            string edit = "0";
            Model.HSSE_Hazard_HazardRegister register = BLL.HSSE_Hazard_HazardRegisterService.GetHazardRegisterByHazardRegisterId(this.HazardRegisterId);
            DateTime date = Convert.ToDateTime(register.CheckTime);
            string dateStr = date.Year.ToString() + date.Month.ToString();
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Registration/" + dateStr + "&menuId={1}&edit={2}", this.HazardRegisterId, Const.HSSE_HiddenRectificationListMenuId, edit)));
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 附件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrlR_Click(object sender, EventArgs e)
        {
            string edit = "1";
            Model.HSSE_Hazard_HazardRegister register = BLL.HSSE_Hazard_HazardRegisterService.GetHazardRegisterByHazardRegisterId(this.HazardRegisterId);
            DateTime date = Convert.ToDateTime(register.CheckTime);
            string dateStr = date.Year.ToString() + date.Month.ToString();
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Registration/" + dateStr + "&menuId={1}&edit={2}", this.HazardRegisterId + "-R", Const.HSSE_HiddenRectificationListMenuId, edit)));
        }
        #endregion
    }
}