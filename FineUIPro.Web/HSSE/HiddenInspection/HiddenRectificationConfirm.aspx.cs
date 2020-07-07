using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.HSSE.HiddenInspection
{
    public partial class HiddenRectificationConfirm : PageBase
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
                    Model.View_Hazard_HazardRegister registration = (from x in BLL.Funs.DB.View_Hazard_HazardRegister where x.HazardRegisterId == HazardRegisterId select x).FirstOrDefault();
                    if (registration != null)
                    {
                        this.txtUnitName.Text = registration.ResponsibilityUnitName;
                        this.txtWorkAreaName.Text = registration.WorkAreaName;
                        this.txtRegisterTypesName.Text = registration.RegisterTypesName;
                        if (registration.CheckCycle == "D")
                        {
                            this.txtType.Text = "日检";
                        }
                        else if (registration.CheckCycle == "W")
                        {
                            this.txtType.Text = "周检";
                        }
                        else if (registration.CheckCycle == "M")
                        {
                            this.txtType.Text = "月检";
                        }
                        this.txtResponsibleManName.Text = registration.ResponsibilityManName;
                        if (registration.RectificationPeriod != null)
                        {
                            this.txtRectificationPeriod.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", registration.RectificationPeriod);
                        }
                        this.txtRegisterDef.Text = registration.RegisterDef;
                        this.txtCutPayment.Text = registration.CutPayment.ToString();
                        this.txtCheckManName.Text = registration.CheckManName;
                        if (registration.CheckTime != null)
                        {
                            this.txtCheckTime.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", registration.CheckTime);
                        }
                        this.txtRectification.Text = registration.Rectification;
                        if (registration.States == "3")   //已闭环
                        {
                            this.ckConfirm.SelectedValue = "True";
                        }
                        else if (registration.States == "1")   //重新整改
                        {
                            this.ckConfirm.SelectedValue = "False";
                            this.txtHandleIdea.Hidden = false;
                            this.txtHandleIdea.Text = registration.HandleIdea;
                        }
                        else if (registration.States == "2")   //已整改
                        {
                            if (!string.IsNullOrEmpty(registration.HandleIdea))
                            {
                                this.txtOldHandleIdea.Hidden = false;
                                this.txtOldHandleIdea.Text = registration.HandleIdea;
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
            if (this.ckConfirm.SelectedValue == "True")   //通过
            {
                register.States = "3";   //已闭环
            }
            else   //未通过
            {
                register.States = "1";   //重新整改
                register.HandleIdea = this.txtHandleIdea.Text.Trim();
            }
            register.ConfirmMan = this.CurrUser.UserId;
            register.ConfirmDate = DateTime.Now;
            BLL.HSSE_Hazard_HazardRegisterService.UpdateHazardRegister(register);
            BLL.LogService.AddSys_Log(this.CurrUser, register.HazardCode, register.HazardRegisterId, BLL.Const.HiddenRectificationMenuId, BLL.Const.BtnModify);

            if (isClosed)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
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
            string edit = "1";
            Model.HSSE_Hazard_HazardRegister register = BLL.HSSE_Hazard_HazardRegisterService.GetHazardRegisterByHazardRegisterId(this.HazardRegisterId);
            DateTime date = Convert.ToDateTime(register.CheckTime);
            string dateStr = date.Year.ToString() + date.Month.ToString();
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Registration/" + dateStr + "&menuId={1}&edit={2}", this.HazardRegisterId, Const.HSSE_HiddenRectificationListMenuId, edit)));
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
            string edit = "0";
            Model.HSSE_Hazard_HazardRegister register = BLL.HSSE_Hazard_HazardRegisterService.GetHazardRegisterByHazardRegisterId(this.HazardRegisterId);
            DateTime date = Convert.ToDateTime(register.CheckTime);
            string dateStr = date.Year.ToString() + date.Month.ToString();
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Registration/" + dateStr + "&menuId={1}&edit={2}", this.HazardRegisterId + "-R", Const.HSSE_HiddenRectificationListMenuId, edit)));
        }
        #endregion

        #region 是否通过变化事件
        /// <summary>
        /// 是否通过变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.ckConfirm.SelectedValue == "True")
            {
                this.txtHandleIdea.Hidden = true;
            }
            else
            {
                this.txtHandleIdea.Hidden = false;
            }
        }
        #endregion
    }
}