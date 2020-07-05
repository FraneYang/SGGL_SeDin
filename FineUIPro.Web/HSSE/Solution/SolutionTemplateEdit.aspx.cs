using BLL;
using System;
using System.Web;

namespace FineUIPro.Web.HSSE.Solution
{
    public partial class SolutionTemplateEdit : PageBase
    {
        #region 定义项
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
        /// 主键
        /// </summary>
        private string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();

                this.InitDropDown();
                this.SolutionTemplateId = Request.Params["SolutionTemplateId"];
                if (!string.IsNullOrEmpty(this.SolutionTemplateId))
                {
                    Model.Solution_SolutionTemplate solutionTemplate = BLL.SolutionTemplateService.GetSolutionTemplateById(this.SolutionTemplateId);
                    if (solutionTemplate != null)
                    {
                        this.ProjectId = solutionTemplate.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDown();
                        }
                        this.txtSolutionTemplateCode.Text = solutionTemplate.SolutionTemplateCode;
                        this.txtSolutionTemplateName.Text = solutionTemplate.SolutionTemplateName;
                        if (!string.IsNullOrEmpty(solutionTemplate.SolutionTemplateType))
                        {
                            this.drpSolutionTemplateType.SelectedValue = solutionTemplate.SolutionTemplateType;
                        }
                        if (!string.IsNullOrEmpty(solutionTemplate.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = solutionTemplate.CompileMan;
                        }
                        if (solutionTemplate.CompileDate != null)
                        {
                            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", solutionTemplate.CompileDate);
                        }
                        this.txtFileContents.Text = HttpUtility.HtmlDecode(solutionTemplate.FileContents);
                    }
                }
                else
                {
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

                    ////自动生成编码
                    //this.txtSolutionTemplateCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.SolutionTemplateMenuId, this.CurrUser.LoginProjectId, this.CurrUser.UnitId);
                }
            }
        }
        #endregion

        private void InitDropDown()
        {
            BLL.ConstValue.InitConstValueDropDownList(this.drpSolutionTemplateType, BLL.ConstValue.Group_CNProfessional, true);
            BLL.UserService.InitUserDropDownList(this.drpCompileMan, this.ProjectId, true);
        }

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.Solution_SolutionTemplate solutionTemplate = new Model.Solution_SolutionTemplate
            {
                ProjectId = this.ProjectId,
                SolutionTemplateCode = this.txtSolutionTemplateCode.Text.Trim(),
                SolutionTemplateName = this.txtSolutionTemplateName.Text.Trim()
            };
            if (this.drpSolutionTemplateType.SelectedValue != BLL.Const._Null)
            {
                solutionTemplate.SolutionTemplateType = this.drpSolutionTemplateType.SelectedValue;
            }
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                solutionTemplate.CompileMan = this.drpCompileMan.SelectedValue;
            }
            solutionTemplate.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
            solutionTemplate.FileContents = HttpUtility.HtmlEncode(this.txtFileContents.Text);
            if (!string.IsNullOrEmpty(this.SolutionTemplateId))
            {
                solutionTemplate.SolutionTemplateId = this.SolutionTemplateId;
                BLL.SolutionTemplateService.UpdateSolutionTemplate(solutionTemplate);
                BLL.LogService.AddSys_Log(this.CurrUser, solutionTemplate.SolutionTemplateCode, solutionTemplate.SolutionTemplateId,BLL.Const.SolutionTemplateMenuId,BLL.Const.BtnModify);
            }
            else
            {
                this.SolutionTemplateId = SQLHelper.GetNewID(typeof(Model.Solution_SolutionTemplate));
                solutionTemplate.SolutionTemplateId = this.SolutionTemplateId;
                BLL.SolutionTemplateService.AddSolutionTemplate(solutionTemplate);
                BLL.LogService.AddSys_Log(this.CurrUser, solutionTemplate.SolutionTemplateCode, solutionTemplate.SolutionTemplateId, BLL.Const.SolutionTemplateMenuId, BLL.Const.BtnAdd);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        #endregion
    }
}