using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.BaseInfo
{
    public partial class QualityQuestionTypeEdit : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                string QualityQuestionTypeId = Request.Params["QualityQuestionTypeId"];
                if (!string.IsNullOrEmpty(QualityQuestionTypeId))
                {

                    Model.Base_QualityQuestionType QualityQuestionType = BLL.QualityQuestionTypeService.GetQualityQuestionType(QualityQuestionTypeId);
                    if (QualityQuestionType != null)
                    {
                        this.txtQualityQuestionType.Text = QualityQuestionType.QualityQuestionType;
                        if (QualityQuestionType.SortIndex != null)
                        {
                            this.txtSortIndex.Text = QualityQuestionType.SortIndex.ToString();
                        }
                    }
                }
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData(true);
        }

        private void SaveData(bool b)
        {
            string QualityQuestionTypeId = Request.Params["QualityQuestionTypeId"];
            Model.Base_QualityQuestionType QualityQuestionType = new Model.Base_QualityQuestionType();
            QualityQuestionType.QualityQuestionType = this.txtQualityQuestionType.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtSortIndex.Text.Trim()))
            {
                QualityQuestionType.SortIndex = Convert.ToInt32(this.txtSortIndex.Text.Trim());
            }
            if (!string.IsNullOrEmpty(QualityQuestionTypeId))
            {
                QualityQuestionType.QualityQuestionTypeId = QualityQuestionTypeId;
                BLL.QualityQuestionTypeService.UpdateQualityQuestionType(QualityQuestionType);
            }
            else
            {
                QualityQuestionType.QualityQuestionTypeId = SQLHelper.GetNewID(typeof(Model.Base_QualityQuestionType));
                BLL.QualityQuestionTypeService.AddQualityQuestionType(QualityQuestionType);

            }
            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
    }
}