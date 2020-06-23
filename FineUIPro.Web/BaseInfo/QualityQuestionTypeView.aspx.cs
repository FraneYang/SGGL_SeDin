using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.BaseInfo
{
    public partial class QualityQuestionTypeView : PageBase
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
    }
}