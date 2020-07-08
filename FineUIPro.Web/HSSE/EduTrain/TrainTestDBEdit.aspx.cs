using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.HSSE.EduTrain
{
    public partial class TrainTestDBEdit : PageBase
    {
        #region 定义变量
        public string TrainTestId
        {
            get
            {
                return (string)ViewState["TrainTestId"];
            }
            set
            {
                ViewState["TrainTestId"] = value;
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
        #endregion

        #region 加载页面
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
                LoadData();

                this.drpIsEndLever.DataTextField = "TypeName";
                drpIsEndLever.DataValueField = "TypeId";
                drpIsEndLever.DataSource = BLL.TrainingService.GetIsEndLeverList();
                drpIsEndLever.DataBind();

                this.TrainTestId = Request.Params["TrainTestId"];
                this.SupTrainingId = Request.QueryString["SupTrainingId"];
                if (!string.IsNullOrEmpty(this.TrainTestId))
                {
                    Model.Training_TrainTestDB trainTestDB = BLL.TrainTestDBService.GetTrainTestDBById(this.TrainTestId);
                    if (trainTestDB != null)
                    {
                        this.txtTrainTestCode.Text = trainTestDB.TrainTestCode;
                        this.txtTrainTestName.Text = trainTestDB.TrainTestName;
                        if (trainTestDB.IsEndLever == true)
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
        #endregion

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.Training_TrainTestDB trainTestDB = new Model.Training_TrainTestDB
            {
                TrainTestCode = this.txtTrainTestCode.Text.Trim(),
                TrainTestName = this.txtTrainTestName.Text.Trim()
            };
            if (this.drpIsEndLever.SelectedValue == "true")
            {
                trainTestDB.IsEndLever = true;
            }
            else
            {
                trainTestDB.IsEndLever = false;
            }
            if (string.IsNullOrEmpty(this.TrainTestId))
            {
                trainTestDB.TrainTestId = SQLHelper.GetNewID(typeof(Model.Training_TrainTestDB));
                trainTestDB.SupTrainTestId = this.SupTrainingId;
                BLL.TrainTestDBService.AddTrainTestDB(trainTestDB);
                BLL.LogService.AddSys_Log(this.CurrUser, trainTestDB.TrainTestCode, trainTestDB.TrainTestId, BLL.Const.TrainTestDBMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                Model.Training_TrainTestDB t = BLL.TrainTestDBService.GetTrainTestDBById(this.TrainTestId);
                if (t != null)
                {
                    trainTestDB.SupTrainTestId = t.SupTrainTestId;
                }
                trainTestDB.TrainTestId = this.TrainTestId;
                BLL.TrainTestDBService.UpdateTrainTestDB(trainTestDB);
                BLL.LogService.AddSys_Log(this.CurrUser, trainTestDB.TrainTestCode, trainTestDB.TrainTestId, BLL.Const.TrainTestDBMenuId, BLL.Const.BtnModify);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.TrainTestDBMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        #region 验证名称是否存在
        /// <summary>
        /// 验证名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = new Model.SGGLDB(Funs.ConnString).Training_TrainTestDB.FirstOrDefault(x => x.SupTrainTestId == this.SupTrainingId && x.TrainTestName == this.txtTrainTestName.Text.Trim() && (x.TrainTestId != this.TrainTestId || (this.TrainTestId == null && x.TrainTestId != null)));
            if (q != null)
            {
                ShowNotify("输入的名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}