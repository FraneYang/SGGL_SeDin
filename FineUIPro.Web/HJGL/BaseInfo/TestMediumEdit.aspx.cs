using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class TestMediumEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string TestMediumId
        {
            get
            {
                return (string)ViewState["TestMediumId"];
            }
            set
            {
                ViewState["TestMediumId"] = value;
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

                this.TestMediumId = Request.Params["MediumId"];
                if (!string.IsNullOrEmpty(this.TestMediumId))
                {
                    Model.Base_TestMedium Medium = BLL.Base_TestMediumService.GetTestMediumById(this.TestMediumId);
                    if (Medium != null)
                    {
                        this.txtMediumCode.Text = Medium.MediumCode;
                        this.txtMediumName.Text = Medium.MediumName;
                        drpTestType.SelectedValue = Medium.TestType;
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
            Model.Base_TestMedium newMedium = new Model.Base_TestMedium
            {
                MediumCode = this.txtMediumCode.Text.Trim(),
                MediumName = this.txtMediumName.Text.Trim(),
                TestType = drpTestType.SelectedValue,
                Remark = this.txtRemark.Text.Trim(),
            };

            if (!string.IsNullOrEmpty(this.TestMediumId))
            {
                newMedium.TestMediumId = this.TestMediumId;
                BLL.Base_TestMediumService.UpdateTestMedium(newMedium);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_MediumMenuId, Const.BtnDelete, this.MediumId);
            }
            else
            {
                this.TestMediumId = SQLHelper.GetNewID(typeof(Model.Base_TestMedium));
                newMedium.TestMediumId = this.TestMediumId;
                BLL.Base_TestMediumService.AddTestMedium(newMedium);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_MediumMenuId, Const.BtnDelete, this.MediumId);
            }

            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion
    }
}