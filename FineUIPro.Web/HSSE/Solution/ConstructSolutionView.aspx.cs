using BLL;
using System;
using System.Web;

namespace FineUIPro.Web.HSSE.Solution
{
    public partial class ConstructSolutionView : PageBase
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
        /// <summary>
        /// 二维码路径id
        /// </summary>
        public string QRCodeAttachUrl
        {
            get
            {
                return (string)ViewState["QRCodeAttachUrl"];
            }
            set
            {
                ViewState["QRCodeAttachUrl"] = value;
            }
        }
        /// <summary>
        /// 方案类型
        /// </summary>
        private string SolutinType
        {
            get
            {
                return (string)ViewState["SolutinType"];
            }
            set
            {
                ViewState["SolutinType"] = value;
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
                this.ConstructSolutionId = Request.Params["ConstructSolutionId"];
                if (!string.IsNullOrEmpty(this.ConstructSolutionId))
                {
                    Model.Solution_ConstructSolution constructSolution = BLL.ConstructSolutionService.GetConstructSolutionById(this.ConstructSolutionId);
                    if (constructSolution != null)
                    {
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
                            var constvalue = BLL.ConstValue.GetConstByConstValueAndGroupId(constructSolution.InvestigateType, BLL.ConstValue.Group_InvestigateType);
                            if (constvalue != null)
                            {
                                this.txtInvestigateType.Text = constvalue.ConstText;
                            }
                        }
                        this.txtUnitName.Text = BLL.UnitService.GetUnitNameByUnitId(constructSolution.UnitId);
                        if (!string.IsNullOrEmpty(constructSolution.SolutinType))
                        {
                            this.SolutinType = constructSolution.SolutinType;
                            var constvalue = BLL.ConstValue.GetConstByConstValueAndGroupId(constructSolution.SolutinType, BLL.ConstValue.Group_CNProfessional);
                            if (constvalue != null)
                            {
                                this.txtSolutinType.Text = constvalue.ConstText;
                            }
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
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectConstructSolutionMenuId;
                this.ctlAuditFlow.DataId = this.ConstructSolutionId;
                this.ctlAuditFlow.ProjectId = this.CurrUser.LoginProjectId;
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
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ShowIndexToLaw.aspx?ToLawIndex={0}", this.SolutinType, "查看 - ")));
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
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ConstructSolutionAttachUrl&menuId={1}", ConstructSolutionId, BLL.Const.ProjectConstructSolutionMenuId)));
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQR_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("~/Controls/SeeQRImage.aspx?ConstructSolutionId={0}&strCode={1}", this.ConstructSolutionId, "constructSolution$" + this.ConstructSolutionId), "二维码查看", 400, 400));           
        }
    }
}