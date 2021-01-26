using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class MediumEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string MediumId
        {
            get
            {
                return (string)ViewState["MediumId"];
            }
            set
            {
                ViewState["MediumId"] = value;
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
                this.txtMediumCode.Focus();
                btnClose.OnClientClick = ActiveWindow.GetHideReference();

                this.MediumId = Request.Params["MediumId"];
                if (!string.IsNullOrEmpty(this.MediumId))
                {
                    Model.Base_Medium Medium = BLL.Base_MediumService.GetMediumByMediumId(this.MediumId);
                    if (Medium != null)
                    {
                        this.txtMediumCode.Text = Medium.MediumCode;
                        this.txtMediumName.Text = Medium.MediumName;
                        this.txtMediumAbbreviation.Text = Medium.MediumAbbreviation;
                        this.txtRemark.Text = Medium.Remark;
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
            var q = Funs.DB.Base_Medium.FirstOrDefault(x => x.MediumCode == this.txtMediumCode.Text.Trim() && x.MediumName == this.txtMediumName.Text.Trim() && (x.MediumId != this.MediumId || (this.MediumId == null && x.MediumId != null)) && x.ProjectId==this.CurrUser.LoginProjectId);
            if (q != null)
            {
                Alert.ShowInTop("此介质代号、名称已经存在！", MessageBoxIcon.Warning);
                return;
            }

            Model.Base_Medium newMedium = new Model.Base_Medium
            {
                MediumCode = this.txtMediumCode.Text.Trim(),
                MediumName = this.txtMediumName.Text.Trim(),
                MediumAbbreviation = this.txtMediumAbbreviation.Text.Trim(),
                Remark = this.txtRemark.Text.Trim(),
                ProjectId=this.CurrUser.LoginProjectId
            };

            if (!string.IsNullOrEmpty(this.MediumId))
            {
                newMedium.MediumId = this.MediumId;
                BLL.Base_MediumService.UpdateMedium(newMedium);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_MediumMenuId, Const.BtnDelete, this.MediumId);
            }
            else
            {
                this.MediumId = SQLHelper.GetNewID(typeof(Model.Base_Medium));
                newMedium.MediumId = this.MediumId;
                BLL.Base_MediumService.AddMedium(newMedium);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_MediumMenuId, Const.BtnDelete, this.MediumId);
            }

            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion
    }
}