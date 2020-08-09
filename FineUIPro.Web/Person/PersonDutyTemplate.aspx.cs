using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace FineUIPro.Web.Person
{
    public partial class PersonDutyTemplate :PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                WorkPostService.InitWorkPostDropDownList(drpWorkPost, true);
            }
        }

        protected void drpWorkPost_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtTemplate.Text = HttpUtility.HtmlDecode(string.Empty);
            if (this.drpWorkPost.SelectedValue != BLL.Const._Null) {
                var WorkPost = BLL.WorkPostService.GetWorkPostById(drpWorkPost.SelectedValue);
                if (!string.IsNullOrEmpty(WorkPost.Template))
                {
                    this.txtTemplate.Text = HttpUtility.HtmlDecode(WorkPost.Template);
                }
            }
        }
        #region 保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.drpWorkPost.SelectedValue != BLL.Const._Null)
            {
                var WorkPost = BLL.WorkPostService.GetWorkPostById(drpWorkPost.SelectedValue);
                if (WorkPost != null)
                {
                    if (!string.IsNullOrEmpty(txtTemplate.Text))
                    {
                        WorkPost.Template = HttpUtility.HtmlEncode(this.txtTemplate.Text);
                        Funs.DB.SubmitChanges();
                    }
                }

            }
            else {
                Alert.ShowInParent("请先选择岗位！", MessageBoxIcon.Warning);
                return;
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        #endregion

    }
}