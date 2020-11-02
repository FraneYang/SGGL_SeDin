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
                WorkPostService.InitMainWorkPostDropDownList(drpWorkPost, true);
            }
        }

        protected void drpWorkPost_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtTemplate.Text = HttpUtility.HtmlDecode(string.Empty);
            if (this.drpWorkPost.SelectedValue != BLL.Const._Null) {
                var dutyTemplate = BLL.Person_DutyTemplateService.GetPersondutyTemplateByWorkPostId(this.drpWorkPost.SelectedValue);
                if (dutyTemplate != null)
                {
                    if (!string.IsNullOrEmpty(dutyTemplate.Template))
                    {
                        this.txtTemplate.Text = HttpUtility.HtmlDecode(dutyTemplate.Template);
                    }
                }
            }
        }
        #region 保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.drpWorkPost.SelectedValue != BLL.Const._Null)
            {
                var dutyTemplate = BLL.Person_DutyTemplateService.GetPersondutyTemplateByWorkPostId(this.drpWorkPost.SelectedValue);
                if (dutyTemplate != null)
                {
                    if (!string.IsNullOrEmpty(txtTemplate.Text))
                    {
                        dutyTemplate.Template = HttpUtility.HtmlEncode(this.txtTemplate.Text);
                        Funs.DB.SubmitChanges();
                    }
                }
                else
                {
                    Model.Person_DutyTemplate newDutyTemplate = new Model.Person_DutyTemplate();
                    newDutyTemplate.DutyTemplateId = SQLHelper.GetNewID();
                    newDutyTemplate.WorkPostId = this.drpWorkPost.SelectedValue;
                    if (!string.IsNullOrEmpty(txtTemplate.Text))
                    {
                        newDutyTemplate.Template = HttpUtility.HtmlEncode(this.txtTemplate.Text);
                    }
                    BLL.Person_DutyTemplateService.AddPersondutyTemplate(newDutyTemplate);
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