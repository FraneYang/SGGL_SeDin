using System;
using System.Linq;
using BLL;

namespace FineUIPro.Web.HSSE.EduTrain
{
    public partial class CompanyTrainingSave : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string CompanyTrainingId
        {
            get
            {
                return (string)ViewState["CompanyTrainingId"];
            }
            set
            {
                ViewState["CompanyTrainingId"] = value;
            }
        }

        /// <summary>
        /// 上级ID
        /// </summary>
        public string SupCompanyTrainingId
        {
            get
            {
                return (string)ViewState["SupCompanyTrainingId"];
            }
            set
            {
                ViewState["SupCompanyTrainingId"] = value;
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
                this.GetButtonPower();
                btnClose.OnClientClick = ActiveWindow.GetHideReference();

                this.drpIsEndLever.DataTextField = "TypeName";
                drpIsEndLever.DataValueField = "TypeId";
                drpIsEndLever.DataSource = BLL.TrainingService.GetIsEndLeverList();
                drpIsEndLever.DataBind();

                this.CompanyTrainingId = Request.QueryString["CompanyTrainingId"];
                this.SupCompanyTrainingId = Request.QueryString["SupCompanyTrainingId"];
                if (!string.IsNullOrEmpty(CompanyTrainingId))
                {
                    var q = BLL.CompanyTrainingService.GetCompanyTrainingById(CompanyTrainingId);
                    if (q != null)
                    {
                        txtCompanyTrainingCode.Text = q.CompanyTrainingCode;
                        txtCompanyTrainingName.Text = q.CompanyTrainingName;
                        if (q.IsEndLever == true)
                        {
                            this.drpIsEndLever.SelectedValue = "true";
                        }
                        else
                        {
                            this.drpIsEndLever.SelectedValue = "false";
                        }
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
            Model.Training_CompanyTraining companyTraining = new Model.Training_CompanyTraining();
            companyTraining.CompanyTrainingCode = this.txtCompanyTrainingCode.Text.Trim();
            companyTraining.CompanyTrainingName = this.txtCompanyTrainingName.Text.Trim();
            companyTraining.IsEndLever = Convert.ToBoolean(drpIsEndLever.SelectedValue);
            if (!string.IsNullOrEmpty(this.CompanyTrainingId))
            {
                Model.Training_CompanyTraining t = BLL.CompanyTrainingService.GetCompanyTrainingById(this.CompanyTrainingId);    
                if (t != null)
                {
                    companyTraining.SupCompanyTrainingId = t.SupCompanyTrainingId;
                }
                companyTraining.CompanyTrainingId = this.CompanyTrainingId;
                BLL.CompanyTrainingService.UpdateCompanyTraining(companyTraining);
            }
            else
            {
                companyTraining.SupCompanyTrainingId = this.SupCompanyTrainingId;
                this.CompanyTrainingId = SQLHelper.GetNewID(typeof(Model.Training_CompanyTraining));
                companyTraining.CompanyTrainingId = this.CompanyTrainingId;
                BLL.CompanyTrainingService.AddCompanyTraining(companyTraining);
            }
            // 2. 关闭本窗体，然后刷新父窗体
            // PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
            // 2. 关闭本窗体，然后回发父窗体
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            //PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(trainingId) + ActiveWindow.GetHideReference());
        }
        #endregion

        #region 验证公司教材库名称是否存在
        /// <summary>
        /// 验证公司教材库名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var companyTraining = Funs.DB.Training_CompanyTraining.FirstOrDefault(x => x.SupCompanyTrainingId == this.SupCompanyTrainingId && x.CompanyTrainingName == this.txtCompanyTrainingName.Text.Trim() && (x.CompanyTrainingId != this.CompanyTrainingId || (this.CompanyTrainingId == null && x.CompanyTrainingId != null)));
            if (companyTraining != null)
            {
                ShowNotify("输入的名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.CompanyTrainingMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion
    }
}