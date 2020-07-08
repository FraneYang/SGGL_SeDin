using BLL;
using System;
using System.Data;
using System.Linq;

namespace FineUIPro.Web.PersonManage.Test
{
    public partial class TestPlanView : PageBase
    {
        #region 定义项    
        /// <summary>
        /// 主键
        /// </summary>
        private string TestPlanId
        {
            get
            {
                return (string)ViewState["TestPlanId"];
            }
            set
            {
                ViewState["TestPlanId"] = value;
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
                this.TestPlanId = Request.Params["TestPlanId"];
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                GetButtonPower();
                var getTestPlan = ServerTestPlanService.GetTestPlanById(this.TestPlanId);
                if (getTestPlan != null)
                {
                    this.txtPlanCode.Text = getTestPlan.PlanCode;
                    this.txtPlanName.Text = getTestPlan.PlanName;
                    this.drpPlanMan.Text = UserService.GetUserNameByUserId(getTestPlan.PlanManId);
                    this.txtPlanDate.Text = string.Format("{0:yyyy-MM-dd}", getTestPlan.PlanDate);
                    this.txtTestStartTime.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", getTestPlan.TestStartTime);
                    this.txtTestEndTime.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", getTestPlan.TestEndTime);
                    this.txtActualTime.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", getTestPlan.ActualTime);
                    this.txtDuration.Text = getTestPlan.Duration.ToString();
                    this.txtSValue.Text = getTestPlan.SValue.ToString();
                    this.txtMValue.Text = getTestPlan.MValue.ToString();
                    this.txtJValue.Text = getTestPlan.JValue.ToString();
                    this.txtTestPalce.Text = getTestPlan.TestPalce;
                    Grid1.DataSource = (from x in new Model.SGGLDB(Funs.ConnString).View_Test_TestPlanTraining
                                        where x.TestPlanId == this.TestPlanId
                                        select x).ToList();
                    Grid1.DataBind();

                    if (getTestPlan.States == Const.State_1)
                    {
                        this.btnSubmit.Hidden = true;
                    }
                    else if (getTestPlan.States == Const.State_2)
                    {
                        this.btnSave.Hidden = true;
                    }
                    else
                    {
                        this.btnSubmit.Hidden = true;
                        this.btnSave.Hidden = true;
                    }
                }
            }
        }
        #endregion

        #region 获取权限按钮
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ServerTestPlanMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                    this.btnSubmit.Hidden = false;
                }
            }
        }
        #endregion

        /// <summary>
        ///  开始考试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var getTestPlan = ServerTestPlanService.GetTestPlanById(this.TestPlanId);
            if (getTestPlan != null && getTestPlan.States == Const.State_1 && getTestPlan.TestStartTime <= DateTime.Now)
            {
                getTestPlan.States = Const.State_2;
                new Model.SGGLDB(Funs.ConnString).SubmitChanges();
                ShowNotify("开始考试成功!", MessageBoxIcon.Success);
            }
            else
            {
                ShowNotify("不符合开始考试条件!", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 结束考试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string info = ServerTestPlanService.EndTestPlan(this.TestPlanId);
            if (!string.IsNullOrEmpty(info))
            {
                ShowNotify(info, MessageBoxIcon.Warning);
            }
            else
            {
                ShowNotify("结束考试成功!", MessageBoxIcon.Success);
            }
        }
    }
}