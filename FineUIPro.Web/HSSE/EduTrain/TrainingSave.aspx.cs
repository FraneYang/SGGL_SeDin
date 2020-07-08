using BLL;
using Model;
using System;
using System.Linq;

namespace FineUIPro.Web.HSSE.EduTrain
{
    public partial class TrainingSave : PageBase
    {
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetButtonPower();
                LoadData();


                this.drpIsEndLever.DataTextField = "TypeName";
                drpIsEndLever.DataValueField = "TypeId";
                drpIsEndLever.DataSource = BLL.TrainingService.GetIsEndLeverList();
                drpIsEndLever.DataBind();

                TrainingId = Request.QueryString["TrainingId"];
                this.SupTrainingId = Request.QueryString["SupTrainingId"];
                if (!String.IsNullOrEmpty(TrainingId))
                {
                    var q = BLL.TrainingService.GetTrainingByTrainingId(TrainingId);
                    if (q != null)
                    {
                        txtTrainingCode.Text = q.TrainingCode;
                        txtTrainingName.Text = q.TrainingName;
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

        private void LoadData()
        {

            btnClose.OnClientClick = ActiveWindow.GetHideReference();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.Training_Training training = new Training_Training
            {
                //string trainingId = Request.QueryString["TrainingId"];

                TrainingCode = txtTrainingCode.Text.Trim(),
                TrainingName = txtTrainingName.Text.Trim(),

                IsEndLever = Convert.ToBoolean(drpIsEndLever.SelectedValue)
            };
            if (String.IsNullOrEmpty(TrainingId))
            {
                TrainingId = SQLHelper.GetNewID(typeof(Model.Training_Training));
                training.TrainingId = TrainingId;
                training.SupTrainingId = this.SupTrainingId;
                BLL.TrainingService.AddTraining(training);
                BLL.LogService.AddSys_Log(this.CurrUser, training.TrainingCode, training.TrainingId, BLL.Const.TrainDBMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                Model.Training_Training t = BLL.TrainingService.GetTrainingByTrainingId(TrainingId);
                training.TrainingId = TrainingId;
                if (t != null)
                {
                    training.SupTrainingId = t.SupTrainingId;
                }
                training.IsBuild = t.IsBuild;
                BLL.TrainingService.UpdateTraining(training);
                BLL.LogService.AddSys_Log(this.CurrUser, training.TrainingCode, training.TrainingId, BLL.Const.TrainDBMenuId, BLL.Const.BtnModify);
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.TrainDBMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        #region 验证培训教材库名称是否存在
        /// <summary>
        /// 验证培训教材库名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var standard = new Model.SGGLDB(Funs.ConnString).Training_Training.FirstOrDefault(x => x.SupTrainingId == this.SupTrainingId && x.TrainingName == this.txtTrainingName.Text.Trim() && (x.TrainingId != this.TrainingId || (this.TrainingId == null && x.TrainingId != null)));
            if (standard != null)
            {
                ShowNotify("输入的名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}