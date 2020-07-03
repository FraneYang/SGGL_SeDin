using BLL;
using Model;
using System;
using System.Linq;

namespace FineUIPro.Web.HSSE.EduTrain
{
    public partial class TrainingItemSave : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string TrainingItemId
        {
            get
            {
                return (string)ViewState["TrainingItemId"];
            }
            set
            {
                ViewState["TrainingItemId"] = value;
            }
        }

        /// <summary>
        /// 主表主键
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
        #endregion

        #region 加载页面
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetButtonPower();
                LoadData();

                this.TrainingItemId = Request.QueryString["TrainingItemId"];
                this.TrainingId = Request.QueryString["TrainingId"];
                if (!String.IsNullOrEmpty(this.TrainingItemId))
                {
                    var q = BLL.TrainingItemService.GetTrainingItemByTrainingItemId(this.TrainingItemId);
                    if (q != null)
                    {
                        txtTrainingItemCode.Text = q.TrainingItemCode;
                        txtTrainingItemName.Text = q.TrainingItemName;
                        txtCompileMan.Text = q.CompileMan;
                        hdCompileMan.Text = q.CompileMan;
                        if (q.CompileDate != null)
                        {
                            txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", q.CompileDate);
                        }
                    }
                }
                else
                {
                    txtCompileMan.Text = this.CurrUser.UserName;
                    hdCompileMan.Text = this.CurrUser.UserName;
                    txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
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
        /// 保存数据
        /// </summary>
        private void SaveData(bool isClose)
        {
            Model.Training_TrainingItem trainingItem = new Training_TrainingItem
            {
                //TrainingItemId = Request.QueryString["TrainingItemId"];

                TrainingItemCode = txtTrainingItemCode.Text.Trim(),
                TrainingItemName = txtTrainingItemName.Text.Trim(),
                CompileMan = hdCompileMan.Text.Trim(),
                UnitId = this.CurrUser.UnitId
            };
            if (!string.IsNullOrEmpty(txtCompileDate.Text.Trim()))
            {
                trainingItem.CompileDate = Convert.ToDateTime(txtCompileDate.Text.Trim());
            }
            if (String.IsNullOrEmpty(TrainingItemId))
            {
                trainingItem.IsPass = true;
                trainingItem.TrainingItemId = SQLHelper.GetNewID(typeof(Model.Training_TrainingItem));
                trainingItem.TrainingId = this.TrainingId;
                this.TrainingItemId = trainingItem.TrainingItemId;
                BLL.TrainingItemService.AddTrainingItem(trainingItem);
                BLL.LogService.AddSys_Log(this.CurrUser, trainingItem.TrainingItemCode, trainingItem.TrainingItemId, BLL.Const.TrainDBMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                Model.Training_TrainingItem t = BLL.TrainingItemService.GetTrainingItemByTrainingItemId(TrainingItemId);
                trainingItem.TrainingItemId = TrainingItemId;
                if (t != null)
                {
                    trainingItem.TrainingId = t.TrainingId;
                }
                BLL.TrainingItemService.UpdateTrainingItem(trainingItem);
                BLL.LogService.AddSys_Log(this.CurrUser, trainingItem.TrainingItemCode, trainingItem.TrainingItemId, BLL.Const.TrainDBMenuId, BLL.Const.BtnModify);
            }
            if (isClose)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData(true);
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
            if (Request.Params["value"] == "0")
            {
                return;
            }
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

        #region 验证教材名称是否存在
        /// <summary>
        /// 验证教材名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Training_TrainingItem.FirstOrDefault(x => x.IsPass == true && x.TrainingId == this.TrainingId && x.TrainingItemName == this.txtTrainingItemName.Text.Trim() && (x.TrainingItemId != this.TrainingItemId || (this.TrainingItemId == null && x.TrainingItemId != null)));
            if (q != null)
            {
                ShowNotify("输入的教材名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        /// <summary>
        /// 上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploadResources_Click(object sender, EventArgs e)
        {
            if (this.btnSave.Hidden)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Training&type=-1", TrainingItemId)));
            }
            else
            {
                if (string.IsNullOrEmpty(this.TrainingItemId))
                {
                    SaveData(false);
                }
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Training&menuId={1}", TrainingItemId, BLL.Const.TrainDBMenuId)));
            }
        }
    }
}