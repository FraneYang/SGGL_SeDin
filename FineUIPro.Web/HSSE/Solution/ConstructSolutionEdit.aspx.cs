using BLL;
using System;
using System.Web;

namespace FineUIPro.Web.HSSE.Solution
{
    public partial class ConstructSolutionEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string ConstructSolutionId
        {
            get
            {
                return (string)ViewState["ConstructSolutionId"];
            }
            set
            {
                ViewState["ConstructSolutionId"] = value;
            }
        }
        #endregion

        #region 项目主键
        /// <summary>
        /// 项目主键
        /// </summary>
        public string ProjectId
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ProjectId = this.CurrUser.LoginProjectId;
                BLL.UnitService.InitUnitDropDownList(this.drpUnitId, this.ProjectId, true);
                BLL.ConstValue.InitConstValueDropDownList(this.drpSolutinType, ConstValue.Group_CNProfessional, false);
                BLL.ConstValue.InitConstValueDropDownList(this.drpInvestigateType, ConstValue.Group_InvestigateType, false);
               
                this.ConstructSolutionId = Request.Params["ConstructSolutionId"];
                if (!string.IsNullOrEmpty(this.ConstructSolutionId))
                {
                    Model.Solution_ConstructSolution constructSolution = BLL.ConstructSolutionService.GetConstructSolutionById(this.ConstructSolutionId);
                    if (constructSolution != null)
                    {
                        this.ProjectId = constructSolution.ProjectId;
                        BLL.UnitService.InitUnitDropDownList(this.drpUnitId, this.ProjectId, true);
                        ///读取编号
                        if (!string.IsNullOrEmpty(constructSolution.ConstructSolutionCode))
                        {
                            this.txtConstructSolutionCode.Text = constructSolution.ConstructSolutionCode;
                        }
                        else
                        {
                            this.txtConstructSolutionCode.Text = CodeRecordsService.ReturnCodeByDataId(this.ConstructSolutionId);
                        }
                        if (!string.IsNullOrEmpty(constructSolution.InvestigateType))
                        {
                            this.drpInvestigateType.SelectedValue = constructSolution.InvestigateType;
                        }
                        if (!string.IsNullOrEmpty(constructSolution.UnitId))
                        {
                            this.drpUnitId.SelectedValue = constructSolution.UnitId;
                        }
                        if (!string.IsNullOrEmpty(constructSolution.SolutinType))
                        {
                            this.drpSolutinType.SelectedValue = constructSolution.SolutinType;
                        }
                        this.txtConstructSolutionName.Text = constructSolution.ConstructSolutionName;
                        this.txtCompileManName.Text = constructSolution.CompileManName;
                        if (constructSolution.CompileDate != null)
                        {
                            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", constructSolution.CompileDate);
                        }
                        this.txtRemark.Text = constructSolution.Remark;
                        this.txtFileContents.Text = HttpUtility.HtmlDecode(constructSolution.FileContents);
                    }
                }
                else
                {
                    this.txtCompileManName.Text = this.CurrUser.UserName;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    ////自动生成编码
                    this.txtConstructSolutionCode.Text = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectConstructSolutionMenuId, this.ProjectId, this.CurrUser.UnitId);
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectConstructSolutionMenuId;
                this.ctlAuditFlow.DataId = this.ConstructSolutionId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
            }
        }
        #endregion

        #region 保存、提交
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.drpInvestigateType.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择审查类型", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpUnitId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择单位名称", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.drpInvestigateType.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择审查类型", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpUnitId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择单位名称", MessageBoxIcon.Warning);
                return;
            }
            if (this.ctlAuditFlow.NextStep == BLL.Const.State_1 && this.ctlAuditFlow.NextPerson == BLL.Const._Null)
            {
                ShowNotify("请选择下一步办理人！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="p"></param>
        private void SaveData(string type)
        {
            Model.Solution_ConstructSolution constructSolution = new Model.Solution_ConstructSolution
            {
                ProjectId = this.ProjectId,
                ConstructSolutionCode = this.txtConstructSolutionCode.Text.Trim(),
                ConstructSolutionName = this.txtConstructSolutionName.Text.Trim()
            };
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                constructSolution.UnitId = this.drpUnitId.SelectedValue;
            }
            if (this.drpInvestigateType.SelectedValue != BLL.Const._Null)
            {
                constructSolution.InvestigateType = this.drpInvestigateType.SelectedValue;
            }
            if (this.drpSolutinType.SelectedValue != BLL.Const._Null)
            {
                constructSolution.SolutinType = this.drpSolutinType.SelectedValue;
            }
            constructSolution.FileContents = HttpUtility.HtmlEncode(this.txtFileContents.Text);
            constructSolution.Remark = this.txtRemark.Text.Trim();
            constructSolution.CompileManName = this.txtCompileManName.Text.Trim();
            constructSolution.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
            constructSolution.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                constructSolution.States = this.ctlAuditFlow.NextStep;
                if (this.ctlAuditFlow.NextStep == BLL.Const.State_2)
                {
                    constructSolution.VersionNo = BLL.SQLHelper.RunProcNewId2("SpGetVersionNumber", "Solution_ConstructSolution", "VersionNo", this.ProjectId);
                }
            }
            if (!string.IsNullOrEmpty(this.ConstructSolutionId))
            {
                constructSolution.ConstructSolutionId = this.ConstructSolutionId;
                BLL.ConstructSolutionService.UpdateConstructSolution(constructSolution);
                BLL.LogService.AddSys_Log(this.CurrUser, constructSolution.ConstructSolutionCode, constructSolution.ConstructSolutionId, BLL.Const.ProjectConstructSolutionMenuId, BLL.Const.BtnModify);
            }
            else
            {
                constructSolution.CompileMan = this.CurrUser.UserId;
                this.ConstructSolutionId = SQLHelper.GetNewID(typeof(Model.Solution_ConstructSolution));
                constructSolution.ConstructSolutionId = this.ConstructSolutionId;
                BLL.ConstructSolutionService.AddConstructSolution(constructSolution);
                BLL.LogService.AddSys_Log(this.CurrUser, constructSolution.ConstructSolutionCode, constructSolution.ConstructSolutionId, BLL.Const.ProjectConstructSolutionMenuId, BLL.Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectConstructSolutionMenuId, this.ConstructSolutionId, (type == BLL.Const.BtnSubmit ? true : false), constructSolution.ConstructSolutionName, "../Solution/ConstructSolutionView.aspx?ConstructSolutionId={0}");
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.ConstructSolutionId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ConstructSolutionAttachUrl&menuId={1}", ConstructSolutionId, BLL.Const.ProjectConstructSolutionMenuId)));
        }
        #endregion

        #region 获取模板
        /// <summary>
        /// 根据方案类别获取模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpSolutinType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var codeTemplateRule = BLL.SolutionTemplateService.GetSolutionTemplateBySolutionTemplateType(this.drpSolutinType.SelectedValue, this.ProjectId);
            if (codeTemplateRule != null)
            {
                this.txtFileContents.Text = HttpUtility.HtmlDecode(codeTemplateRule.FileContents);
            }
        }
        #endregion

        #region 查看对应标准规范
        /// <summary>
        /// 查看对应标准规范
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSeeLaw_Click(object sender, EventArgs e)
        {
            if (this.drpSolutinType.SelectedValue != BLL.Const._Null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ShowIndexToLaw.aspx?ToLawIndex={0}", this.drpSolutinType.SelectedValue, "查看 - ")));
            }
            else
            {
                Alert.ShowInTop("请先选择方案类别！", MessageBoxIcon.Warning);
                return;
            }
        }
        #endregion
    }
}