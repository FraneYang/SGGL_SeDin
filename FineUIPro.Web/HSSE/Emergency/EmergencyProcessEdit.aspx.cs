using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.HSSE.Emergency
{
    public partial class EmergencyProcessEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string EmergencyProcessId
        {
            get
            {
                return (string)ViewState["EmergencyProcessId"];
            }
            set
            {
                ViewState["EmergencyProcessId"] = value;
            }
        }
        /// <summary>
        /// 项目主键
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.EmergencyProcessId = Request.Params["EmergencyProcessId"];
                if (!string.IsNullOrEmpty(this.EmergencyProcessId))
                {
                    var EmergencyProcess = new Model.SGGLDB(Funs.ConnString).Emergency_EmergencyProcess.FirstOrDefault(x => x.EmergencyProcessId == this.EmergencyProcessId);
                    if (EmergencyProcess != null)
                    {
                        this.txtProcessSteps.Text = EmergencyProcess.ProcessSteps;
                        this.txtProcessName.Text = EmergencyProcess.ProcessName;
                        this.txtStepOperator.Text = EmergencyProcess.StepOperator;
                        this.txtRemark.Text = EmergencyProcess.Remark;
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
            this.SaveData();
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
         void SaveData()
        {
            var EmergencyProcess = new Model.SGGLDB(Funs.ConnString).Emergency_EmergencyProcess.FirstOrDefault(x => x.EmergencyProcessId == this.EmergencyProcessId);
            if (EmergencyProcess != null)
            {

                EmergencyProcess.ProcessName = this.txtProcessName.Text.Trim();
                EmergencyProcess.StepOperator = this.txtStepOperator.Text.Trim();
                EmergencyProcess.Remark = this.txtRemark.Text.Trim();
                new Model.SGGLDB(Funs.ConnString).SubmitChanges();
            }
        }
        #endregion

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
         void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ProjectEmergencyProcessMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

    }
}