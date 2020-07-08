using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.HSSE.EduTrain
{
    public partial class PlanView : PageBase
    {
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                string planId = Request.Params["PlanId"];
                var plan = BLL.TrainingPlanService.GetPlanById(planId);
                if (plan != null)
                {
                    this.txtPlanCode.Text = plan.PlanCode;
                    this.txtPlanName.Text = plan.PlanName;
                    this.txtDesignerName.Text = BLL.UserService.GetUserNameByUserId(plan.DesignerId);
                    this.txtDesignerDate.Text = string.Format("{0:yyyy-MM-dd}", plan.DesignerDate);
                    this.txtInstallationNames.Text = WorkPostService.getWorkPostNamesWorkPostIds(plan.WorkPostId);
                    this.txtUnitName.Text = UnitService.getUnitNamesUnitIds(plan.UnitIds);
                    var testPlanTraining = from x in new Model.SGGLDB(Funs.ConnString).Training_PlanItem
                                           join y in new Model.SGGLDB(Funs.ConnString).Training_Training on x.TrainingEduId equals y.TrainingId
                                           where x.PlanId == planId
                                           orderby y.TrainingCode
                                           select y.TrainingName;
                    foreach (var item in testPlanTraining)
                    {
                        this.txtTrainingEdu.Text += item + "；";
                    }
                }
            }
        }
        #endregion
    }
}