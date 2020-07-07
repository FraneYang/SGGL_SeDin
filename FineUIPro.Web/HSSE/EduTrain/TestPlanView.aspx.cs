using System;
using System.Collections.Generic;
using System.Linq;
using BLL;

namespace FineUIPro.Web.EduTrain
{
    public partial class TestPlanView : PageBase
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
                string testPlanId = Request.Params["TestPlanId"];
                Model.Training_TestPlan plan = BLL.TestPlanService.GetTestPlanById(testPlanId);
                if (plan != null)
                {
                    this.txtPlanCode.Text = plan.PlanCode;
                    this.txtPlanName.Text = plan.PlanName;
                    switch (plan.States)
                    {
                        case "0":
                            this.txtStates.Text = "未发布";
                            break;
                        case "1":
                            this.txtStates.Text = "待考试";
                            break;

                        case "2":
                            this.txtStates.Text = "考试中";
                            break;
                        case "3":
                            this.txtStates.Text = "考试结束";
                            break;
                        case "-1":
                            this.txtStates.Text = "已作废";
                            break;
                    }

                    this.txtTestStartTime.Text = string.Format("{0:yyyy-MM-dd hh:mm:ss}", plan.TestStartTime);
                    this.txtTestEndTime.Text = string.Format("{0:yyyy-MM-dd hh:mm:ss}", plan.TestEndTime);
                    this.txtDuration.Text = plan.Duration.ToString();
                    this.txtTestPalce.Text = plan.TestPalce;
                    this.txtTotalScore.Text = plan.TotalScore.ToString();
                    this.txtQuestionCount.Text = plan.QuestionCount.ToString();
                    this.txtWorkPostNames.Text = plan.WorkPostNames;


                    var testPlanTraining = from x in Funs.DB.Training_TestPlanTraining
                                           join y in Funs.DB.Training_Training on x.TrainingId equals y.TrainingId
                                           where x.TestPlanId == testPlanId
                                           orderby y.TrainingCode
                                           select new { y.TrainingName, x.TestType1Count, x.TestType2Count, x.TestType3Count };
                    foreach (var item in testPlanTraining)
                    {
                        this.txtTrainingName.Text += item.TrainingName + "；";
                        if (item.TestType1Count.HasValue)
                        {
                            this.txtTestType1.Text += item.TrainingName + ":" + item.TestType1Count.Value + "；";
                        }
                        if (item.TestType2Count.HasValue)
                        {
                            this.txtTestType2.Text += item.TrainingName + ":" + item.TestType2Count.Value + "；";
                        }
                        if (item.TestType3Count.HasValue)
                        {
                            this.txtTestType3.Text += item.TrainingName + ":" + item.TestType3Count.Value + "；";
                        }
                    }
                }
            }
        }
        #endregion      
    }
}