using BLL;
using Model;
using System;
using System.Linq;

namespace FineUIPro.Web.HSSE.EduTrain
{
    public partial class AccidentCaseSave : PageBase
    {
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

        public string SupAccidentCaseId
        {
            get
            {
                return (string)ViewState["SupAccidentCaseId"];
            }
            set
            {
                ViewState["SupAccidentCaseId"] = value;
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

                this.AccidentCaseId = Request.QueryString["AccidentCaseId"];
                this.SupAccidentCaseId = Request.QueryString["SupAccidentCaseId"];
                if (!String.IsNullOrEmpty(this.AccidentCaseId))
                {
                    var q = BLL.AccidentCaseService.GetAccidentCaseById(this.AccidentCaseId);
                    if (q != null)
                    {
                        txtAccidentCaseCode.Text = q.AccidentCaseCode;
                        txtAccidentCaseName.Text = q.AccidentCaseName;
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
            Model.EduTrain_AccidentCase accidentCase = new EduTrain_AccidentCase
            {
                //string accidentCaseId = Request.QueryString["AccidentCaseId"];

                AccidentCaseCode = txtAccidentCaseCode.Text.Trim(),
                AccidentCaseName = txtAccidentCaseName.Text.Trim(),

                IsEndLever = Convert.ToBoolean(drpIsEndLever.SelectedValue)
            };
            if (String.IsNullOrEmpty(this.AccidentCaseId))
            {
                accidentCase.AccidentCaseId = SQLHelper.GetNewID(typeof(Model.EduTrain_AccidentCase));
                this.AccidentCaseId = accidentCase.AccidentCaseId;
                accidentCase.SupAccidentCaseId = this.SupAccidentCaseId;
                BLL.AccidentCaseService.AddAccidentCase(accidentCase);
                BLL.LogService.AddSys_Log(this.CurrUser, accidentCase.AccidentCaseCode, accidentCase.AccidentCaseId, BLL.Const.AccidentCaseMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                Model.EduTrain_AccidentCase t = BLL.AccidentCaseService.GetAccidentCaseById(this.AccidentCaseId);
                accidentCase.AccidentCaseId = this.AccidentCaseId;
                if (t != null)
                {
                    accidentCase.SupAccidentCaseId = t.SupAccidentCaseId;
                }
                BLL.AccidentCaseService.UpdateAccidentCase(accidentCase);
                BLL.LogService.AddSys_Log(this.CurrUser, accidentCase.AccidentCaseCode, accidentCase.AccidentCaseId, BLL.Const.AccidentCaseMenuId, BLL.Const.BtnModify);
            }
            // 2. 关闭本窗体，然后刷新父窗体
            // PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
            // 2. 关闭本窗体，然后回发父窗体
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            //PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(wedId) + ActiveWindow.GetHideReference());

        }

        #region 按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
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

        #region 验证名称是否存在
        /// <summary>
        /// 验证名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = new Model.SGGLDB(Funs.ConnString).EduTrain_AccidentCase.FirstOrDefault(x => x.SupAccidentCaseId == this.SupAccidentCaseId && x.AccidentCaseName == this.txtAccidentCaseName.Text.Trim() && (x.AccidentCaseId != this.AccidentCaseId || (this.AccidentCaseId == null && x.AccidentCaseId != null)));
            if (q != null)
            {
                ShowNotify("输入的名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}