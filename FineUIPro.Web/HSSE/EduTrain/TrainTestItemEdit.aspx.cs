using BLL;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.EduTrain
{
    public partial class TrainTestItemEdit :PageBase
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

        public string TrainTestItemId
        {
            get
            {
                return (string)ViewState["TrainTestItemId"];
            }
            set
            {
                ViewState["TrainTestItemId"] = value;
            }
        }

        #endregion

        #region 加载页面
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetButtonPower();
                this.TrainTestId = Request.Params["TrainTestId"];
                this.TrainTestItemId = Request.Params["TrainTestItemId"];
                if (!string.IsNullOrEmpty(this.TrainTestItemId))
                {
                    Model.Training_TrainTestDBItem trainTestDBItem = BLL.TrainTestDBItemService.GetTrainTestDBItemById(this.TrainTestItemId);
                    if (trainTestDBItem!=null)
                    {
                        this.TrainTestId = trainTestDBItem.TrainTestId;
                        this.txtTrainTestItemCode.Text = trainTestDBItem.TrainTestItemCode;
                        this.txtTrainTestItemName.Text = trainTestDBItem.TraiinTestItemName;
                        if (!string.IsNullOrEmpty(trainTestDBItem.AttachUrl))
                        {
                            //this.FullAttachUrl = trainTestDBItem.AttachUrl;
                            //this.lbAttachUrl.Text = trainTestDBItem.AttachUrl.Substring(trainTestDBItem.AttachUrl.IndexOf("~") + 1);
                        }
                    }
                }
            }
        }
        #endregion

        #region 保存数据
        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData( bool isClose)
        {
            Model.Training_TrainTestDBItem trainTestDBItem = new Model.Training_TrainTestDBItem
            {
                TrainTestItemCode = this.txtTrainTestItemCode.Text.Trim(),
                TraiinTestItemName = this.txtTrainTestItemName.Text.Trim(),
                //trainTestDBItem.AttachUrl = this.FullAttachUrl;
            };
            if (string.IsNullOrEmpty(this.TrainTestItemId))
            {
                trainTestDBItem.IsPass = true;
                trainTestDBItem.CompileMan = this.CurrUser.UserName;
                trainTestDBItem.UnitId =  this.CurrUser.UnitId;
                trainTestDBItem.CompileDate = DateTime.Now;
                trainTestDBItem.TrainTestId = this.TrainTestId;
                trainTestDBItem.TrainTestItemId = SQLHelper.GetNewID(typeof(Model.Training_TrainTestDBItem));
                TrainTestItemId = trainTestDBItem.TrainTestItemId;
                BLL.TrainTestDBItemService.AddTrainTestDBItem(trainTestDBItem);
                BLL.LogService.AddSys_Log(this.CurrUser, trainTestDBItem.TrainTestItemCode, trainTestDBItem.TrainTestItemId, BLL.Const.TrainTestDBMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                Model.Training_TrainTestDBItem t = BLL.TrainTestDBItemService.GetTrainTestDBItemById(this.TrainTestItemId);
                if (t != null)
                {
                    trainTestDBItem.TrainTestId = t.TrainTestId;
                }
                trainTestDBItem.TrainTestItemId = this.TrainTestItemId;
                BLL.TrainTestDBItemService.UpdateTrainTestDBItem(trainTestDBItem);
                BLL.LogService.AddSys_Log(this.CurrUser, trainTestDBItem.TrainTestItemCode, trainTestDBItem.TrainTestItemId, BLL.Const.TrainTestDBMenuId, BLL.Const.BtnModify);
            }
            if (isClose)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }

        /// <summary>
        /// 保存按钮
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.TrainTestDBMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                    //this.btnUpFile.Hidden = false;
                    //this.btnDelete.Hidden = false;
                }
            }
        }
        #endregion

        #region 验证试题名称是否存在
        /// <summary>
        /// 验证试题名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = new Model.SGGLDB(Funs.ConnString).Training_TrainTestDBItem.FirstOrDefault(x => x.IsPass == true && x.TrainTestId == this.TrainTestId && x.TraiinTestItemName == this.txtTrainTestItemName.Text.Trim() && (x.TrainTestItemId != this.TrainTestItemId || (this.TrainTestItemId == null && x.TrainTestItemId != null)));
            if (q != null)
            {
                ShowNotify("输入的试题名称已存在！", MessageBoxIcon.Warning);
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
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/TrainTestDB&type=-1", TrainTestItemId)));
            }
            else
            {
                if (string.IsNullOrEmpty(this.TrainTestItemId))
                {
                    SaveData( false);
                }
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/TrainTestDB&menuId={1}", TrainTestItemId, BLL.Const.TrainTestDBMenuId)));
            }
        }
    }
}