using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class TestMediumView : System.Web.UI.Page
    {
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

               string testMediumId = Request.Params["MediumId"];
                if (!string.IsNullOrEmpty(testMediumId))
                {
                    Model.Base_TestMedium Medium = BLL.Base_TestMediumService.GetTestMediumById(testMediumId);
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
    }
}