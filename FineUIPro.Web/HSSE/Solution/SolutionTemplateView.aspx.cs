using System;
using System.Web;

namespace FineUIPro.Web.HSSE.Solution
{
    public partial class SolutionTemplateView : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        private string SolutionTemplateId
        {
            get
            {
                return (string)ViewState["SolutionTemplateId"];
            }
            set
            {
                ViewState["SolutionTemplateId"] = value;
            }
        }

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
                this.SolutionTemplateId = Request.Params["SolutionTemplateId"];
                if (!string.IsNullOrEmpty(this.SolutionTemplateId))
                {
                    Model.Solution_SolutionTemplate solutionTemplate = BLL.SolutionTemplateService.GetSolutionTemplateById(this.SolutionTemplateId);
                    if (solutionTemplate != null)
                    {
                        this.txtSolutionTemplateCode.Text = solutionTemplate.SolutionTemplateCode;
                        this.txtSolutionTemplateName.Text = solutionTemplate.SolutionTemplateName;
                        if (!string.IsNullOrEmpty(solutionTemplate.SolutionTemplateType))
                        {
                            this.txtSolutionTemplateType.Text = BLL.ConstValue.GetConstByConstValueAndGroupId(solutionTemplate.SolutionTemplateType, BLL.ConstValue.Group_CNProfessional).ConstText;
                        }
                        if (!string.IsNullOrEmpty(solutionTemplate.CompileMan))
                        {
                            var user = BLL.UserService.GetUserByUserId(solutionTemplate.CompileMan);
                            if (user != null)
                            {
                                this.txtCompileMan.Text = user.UserName;
                            }
                        }
                        if (solutionTemplate.CompileDate != null)
                        {
                            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", solutionTemplate.CompileDate);
                        }
                        this.txtFileContents.Text = HttpUtility.HtmlDecode(solutionTemplate.FileContents);
                    }
                }
            }
        }
    }
}