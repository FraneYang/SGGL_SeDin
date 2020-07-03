using BLL;
using Model;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.EduTrain
{
    public partial class AccidentCaseItemSave : PageBase
    {
        #region 定义变量
        public string AccidentCaseId
        {
            get
            {
                return (string)ViewState["AccidentCaseId"];
            }
            set
            {
                ViewState["AccidentCaseId"] = value;
            }
        }

        public string AccidentCaseItemId
        {
            get
            {
                return (string)ViewState["AccidentCaseItemId"];
            }
            set
            {
                ViewState["AccidentCaseItemId"] = value;
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
                this.AccidentCaseItemId = Request.QueryString["AccidentCaseItemId"];
                this.AccidentCaseId = Request.QueryString["AccidentCaseId"];
                if (!String.IsNullOrEmpty(this.AccidentCaseItemId))
                {
                    var q = BLL.AccidentCaseItemService.GetAccidentCaseItemById(this.AccidentCaseItemId);
                    if (q != null)
                    {
                        this.AccidentCaseId = q.AccidentCaseId;
                        this.txtActivities.Text = q.Activities;
                        this.txtAccidentName.Text = q.AccidentName;
                        this.txtAccidentProfiles.Text = q.AccidentProfiles;
                        this.txtAccidentReview.Text = q.AccidentReview;
                        if (!string.IsNullOrEmpty(q.AccidentCaseId))
                        {
                            var accidentCase = BLL.AccidentCaseService.GetAccidentCaseById(q.AccidentCaseId);
                            if (accidentCase != null)
                            {
                                this.txtAccidentCaseName.Text = accidentCase.AccidentCaseName;
                            }
                        }
                    }
                }

               
                if (!string.IsNullOrEmpty(this.AccidentCaseId))
                {
                    var accidentCase = BLL.AccidentCaseService.GetAccidentCaseById(this.AccidentCaseId);
                    if (accidentCase != null)
                    {
                        this.txtAccidentCaseName.Text = accidentCase.AccidentCaseName;
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
        /// 保存数据
        /// </summary>
        private void SaveData()
        {
            Model.EduTrain_AccidentCaseItem accidentCaseItem = new EduTrain_AccidentCaseItem
            {
                Activities = this.txtActivities.Text.Trim(),
                AccidentName = this.txtAccidentName.Text.Trim(),
                AccidentProfiles = this.txtAccidentProfiles.Text.Trim(),
                AccidentReview = this.txtAccidentReview.Text.Trim(),
            };
            if (string.IsNullOrEmpty(this.AccidentCaseItemId))
            {
                accidentCaseItem.CompileMan = this.CurrUser.UserName;
                accidentCaseItem.UnitId =this.CurrUser.UnitId;
                accidentCaseItem.CompileDate = DateTime.Now;
                accidentCaseItem.IsPass = true;
                accidentCaseItem.AccidentCaseItemId = SQLHelper.GetNewID(typeof(Model.EduTrain_AccidentCaseItem));
                AccidentCaseItemId = accidentCaseItem.AccidentCaseItemId;
                accidentCaseItem.AccidentCaseId = this.AccidentCaseId;
                BLL.AccidentCaseItemService.AddAccidentCaseItem(accidentCaseItem);
                BLL.LogService.AddSys_Log(this.CurrUser, accidentCaseItem.AccidentName, accidentCaseItem.AccidentCaseItemId, BLL.Const.AccidentCaseMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                Model.EduTrain_AccidentCaseItem t = BLL.AccidentCaseItemService.GetAccidentCaseItemById(this.AccidentCaseItemId);
                accidentCaseItem.AccidentCaseItemId = this.AccidentCaseItemId;
                if (t != null)
                {
                    accidentCaseItem.AccidentCaseId = t.AccidentCaseId;
                }
                BLL.AccidentCaseItemService.UpdateAccidentCaseItem(accidentCaseItem);
                BLL.LogService.AddSys_Log(this.CurrUser, accidentCaseItem.AccidentName, accidentCaseItem.AccidentCaseItemId, BLL.Const.AccidentCaseMenuId, BLL.Const.BtnModify);
            }
            // 2. 关闭本窗体，然后刷新父窗体
            // PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
            // 2. 关闭本窗体，然后回发父窗体
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            //PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(wedId) + ActiveWindow.GetHideReference());
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        #endregion
        
        #region 按钮权限
        /// <summary>
        /// 按钮权限
        /// </summary>
        private void GetButtonPower()
        {
            if(Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.AccidentCaseMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        #region 验证事故名称是否存在
        /// <summary>
        /// 验证事故名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.EduTrain_AccidentCaseItem.FirstOrDefault(x => x.IsPass == true && x.AccidentCaseId == this.AccidentCaseId && x.AccidentName == this.txtAccidentName.Text.Trim() && (x.AccidentCaseItemId != this.AccidentCaseItemId || (this.AccidentCaseItemId == null && x.AccidentCaseItemId != null)));
            if (q != null)
            {
                ShowNotify("输入的事故名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}