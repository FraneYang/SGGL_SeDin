using BLL;
using Model;
using System;
using System.Linq;

namespace FineUIPro.Web.HSSE.EduTrain
{
    public partial class TestTrainingSave : PageBase
    {
        #region 自定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string TrainingId
        {
            get
            {
                return (string)ViewState["TrainingId"];
            }
            set
            {
                ViewState["TrainingId"] = value;
            }
        }

        /// <summary>
        /// 上级主键
        /// </summary>
        public string SupTrainingId
        {
            get
            {
                return (string)ViewState["SupTrainingId"];
            }
            set
            {
                ViewState["SupTrainingId"] = value;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetButtonPower();
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                Funs.FineUIPleaseSelect(this.drpMenuType);

                this.TrainingId = Request.QueryString["TrainingId"];
                this.SupTrainingId = Request.QueryString["SupTrainingId"];
                if (!String.IsNullOrEmpty(TrainingId))
                {
                    var q = BLL.TestTrainingService.GetTestTrainingById(TrainingId);
                    if (q != null)
                    {
                        this.SupTrainingId = q.SupTrainingId;
                        txtTrainingCode.Text = q.TrainingCode;
                        txtTrainingName.Text = q.TrainingName;
                        if (q.IsEndLever == true)
                        {
                            ckIsEndLever.Checked = true;
                        }
                        else
                        {
                            ckIsEndLever.Checked = false;
                        }
                        if (q.IsOffice == true)
                        {
                            ckIsOffice.Checked = true;
                        }
                        else
                        {
                            ckIsOffice.Checked = false;
                        }
                        if (!string.IsNullOrEmpty(q.MenuType))
                        {
                            this.drpMenuType.SelectedValue = q.MenuType;
                        }
                    }
                }
                var supq = BLL.TestTrainingService.GetTestTrainingById(this.SupTrainingId);
                if (supq != null)
                {
                    this.txtSupTraining.Text = supq.TrainingName;
                }
                else
                {
                    this.SupTrainingId = "0";
                    this.txtSupTraining.Text = "考试试题库";
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.Training_TestTraining training = new Training_TestTraining
            {
                TrainingCode = txtTrainingCode.Text.Trim(),
                TrainingName = txtTrainingName.Text.Trim(),
                SupTrainingId = this.SupTrainingId,
                IsEndLever = this.ckIsEndLever.Checked,
                IsOffice = this.ckIsOffice.Checked,
            };
            if (this.drpMenuType.SelectedValue != Const._Null)
            {
                training.MenuType = this.drpMenuType.SelectedValue;
            };

            if (String.IsNullOrEmpty(TrainingId))
            {
                TrainingId = SQLHelper.GetNewID(typeof(Model.Training_TestTraining));
                training.TrainingId = TrainingId;
                BLL.TestTrainingService.AddTestTraining(training);
            }
            else
            {
                training.TrainingId = TrainingId;
                BLL.TestTrainingService.UpdateTestTraining(training);
            }
            // 2. 关闭本窗体，然后刷新父窗体
            // PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
            // 2. 关闭本窗体，然后回发父窗体
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            //PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(trainingId) + ActiveWindow.GetHideReference());

        }

        #region 按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.TestTrainingMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        #region 验证考试试题库名称是否存在
        /// <summary>
        /// 验证考试试题库名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var standard = Funs.DB.Training_TestTraining.FirstOrDefault(x => x.TrainingName == this.txtTrainingName.Text.Trim() && (x.TrainingId != this.TrainingId || (this.TrainingId == null && x.TrainingId != null)));
            if (standard != null)
            {
                ShowNotify("输入的名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}