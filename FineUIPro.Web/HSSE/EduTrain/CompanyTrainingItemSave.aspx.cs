using System;
using System.Linq;
using Model;
using BLL;

namespace FineUIPro.Web.HSSE.EduTrain
{
    public partial class CompanyTrainingItemSave : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string CompanyTrainingItemId
        {
            get
            {
                return (string)ViewState["CompanyTrainingItemId"];
            }
            set
            {
                ViewState["CompanyTrainingItemId"] = value;
            }
        }

        /// <summary>
        /// 主表主键
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
        #endregion

        #region 加载页面
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetButtonPower();
                btnClose.OnClientClick = ActiveWindow.GetHideReference();

                this.CompanyTrainingItemId = Request.QueryString["CompanyTrainingItemId"];
                this.CompanyTrainingId = Request.QueryString["CompanyTrainingId"];
                if (!string.IsNullOrEmpty(this.CompanyTrainingItemId))
                {
                    var q = CompanyTrainingItemService.GetCompanyTrainingItemById(this.CompanyTrainingItemId);
                    if (q != null)
                    {
                        this.CompanyTrainingId = q.CompanyTrainingId;
                        txtCompanyTrainingItemCode.Text = q.CompanyTrainingItemCode;
                        txtCompanyTrainingItemName.Text = q.CompanyTrainingItemName;
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
                    var cTraining = BLL.CompanyTrainingService.GetCompanyTrainingById(this.CompanyTrainingId);
                    if (cTraining != null)
                    {
                        txtCompileMan.Text = this.CurrUser.UserName;
                        hdCompileMan.Text = this.CurrUser.UserName;
                        this.txtCompanyTrainingItemName.Text = cTraining.CompanyTrainingName;
                        this.txtCompanyTrainingItemCode.Text = SQLHelper.RunProcNewId("SpGetNewCode3", "Training_CompanyTrainingItem", "CompanyTrainingItemCode", cTraining.CompanyTrainingCode + "-");
                        txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    }
                }
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData(bool isClose)
        {
            Model.Training_CompanyTrainingItem newCompanyTrainItem = new Training_CompanyTrainingItem
            {
                CompanyTrainingItemCode = this.txtCompanyTrainingItemCode.Text.Trim(),
                CompanyTrainingItemName = this.txtCompanyTrainingItemName.Text.Trim(),
                CompileMan = hdCompileMan.Text.Trim()
            };
            if (!string.IsNullOrEmpty(txtCompileDate.Text.Trim()))
            {
                newCompanyTrainItem.CompileDate = Convert.ToDateTime(txtCompileDate.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.CompanyTrainingItemId))
            {
                newCompanyTrainItem.CompanyTrainingItemId = this.CompanyTrainingItemId;
                BLL.CompanyTrainingItemService.UpdateCompanyTrainingItem(newCompanyTrainItem);
                BLL.LogService.AddSys_Log(this.CurrUser, newCompanyTrainItem.CompanyTrainingItemCode, newCompanyTrainItem.CompanyTrainingItemId,BLL.Const.CompanyTrainingMenuId,BLL.Const.BtnModify);
            }
            else
            {
                newCompanyTrainItem.CompanyTrainingId = this.CompanyTrainingId;
                this.CompanyTrainingItemId = SQLHelper.GetNewID(typeof(Model.Training_CompanyTrainingItem));
                newCompanyTrainItem.CompanyTrainingItemId = this.CompanyTrainingItemId;
                BLL.CompanyTrainingItemService.AddCompanyTrainingItem(newCompanyTrainItem);
                BLL.LogService.AddSys_Log(this.CurrUser, newCompanyTrainItem.CompanyTrainingItemCode, newCompanyTrainItem.CompanyTrainingItemId, BLL.Const.CompanyTrainingMenuId, BLL.Const.BtnAdd);
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

        #region 验证教材名称是否存在
        /// <summary>
        /// 验证教材名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = new Model.SGGLDB(Funs.ConnString).Training_CompanyTrainingItem.FirstOrDefault(x => x.CompanyTrainingId == this.CompanyTrainingId && x.CompanyTrainingItemName == this.txtCompanyTrainingItemName.Text.Trim() && (x.CompanyTrainingItemId != this.CompanyTrainingItemId || (this.CompanyTrainingItemId == null && x.CompanyTrainingItemId != null)));
            if (q != null)
            {
                ShowNotify("输入的教材名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 上传附件资源
        /// <summary>
        /// 上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploadResources_Click(object sender, EventArgs e)
        {
            if (this.btnSave.Hidden)
            {                
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CompanyTraining&menuId={1}&type=-1", CompanyTrainingItemId, BLL.Const.CompanyTrainingMenuId)));
            }
            else
            {
                if (string.IsNullOrEmpty(this.CompanyTrainingItemId))
                {
                    SaveData(false);
                }
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CompanyTraining&menuId={1}", CompanyTrainingItemId, BLL.Const.CompanyTrainingMenuId)));
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
            if (Request.Params["value"] == "0")
            {
                return;
            }
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