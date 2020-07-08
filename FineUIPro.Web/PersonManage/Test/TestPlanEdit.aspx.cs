using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FineUIPro.Web.PersonManage.Test
{
    public partial class TestPlanEdit : PageBase
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

        public  List<Model.View_Test_TestPlanTraining> viewTestPlanTrainingList;

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
                Funs.FineUIPleaseSelect(this.drpUserType);
                UserService.InitUserDropDownList(this.drpPlanMan, this.CurrUser.LoginProjectId, true);
                TestTrainingService.InitTestTrainingDropDownList(this.drpTraining, true);
                var getTestPlan = ServerTestPlanService.GetTestPlanById(this.TestPlanId);
                if (getTestPlan != null)
                {
                    this.txtPlanCode.Text = getTestPlan.PlanCode;
                    this.txtPlanName.Text = getTestPlan.PlanName;
                    this.drpPlanMan.SelectedValue = getTestPlan.PlanManId;
                    this.txtPlanDate.Text = string.Format("{0:yyyy-MM-dd}", getTestPlan.PlanDate);
                    this.txtTestStartTime.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", getTestPlan.TestStartTime);
                    this.txtTestEndTime.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", getTestPlan.TestEndTime);
                    this.txtDuration.Text = getTestPlan.Duration.ToString();
                    this.txtSValue.Text = getTestPlan.SValue.ToString();
                    this.txtMValue.Text = getTestPlan.MValue.ToString();
                    this.txtJValue.Text = getTestPlan.JValue.ToString();
                    this.txtTestPalce.Text = getTestPlan.TestPalce;
                    //this.lbQuestionCount.Text = getTestPlan.QuestionCount.ToString();
                    //this.lbTotalScore.Text = getTestPlan.TotalScore.ToString();
                    viewTestPlanTrainingList = (from x in new Model.SGGLDB(Funs.ConnString).View_Test_TestPlanTraining
                                                where x.TestPlanId == this.TestPlanId
                                                select x).ToList();
                    Grid1.DataSource = viewTestPlanTrainingList;
                    Grid1.DataBind();
                }
                else
                {
                    this.txtPlanName.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("00") + "知识竞赛考试";
                    this.txtPlanDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.drpPlanMan.SelectedValue = this.CurrUser.UserId;
                    this.txtTestStartTime.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now.AddDays(1));
                    this.txtTestEndTime.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now.AddDays(2));
                    this.txtSValue.Text = "2";
                    this.txtMValue.Text = "3";
                    this.txtJValue.Text = "1";
                    this.txtDuration.Text = "60";
                    //this.lbQuestionCount.Text = "0";
                    //this.lbTotalScore.Text = "0";
                }
              
            }
        }
        #endregion

        #region 修改
        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 
        /// </summary>
        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            var getViewList = this.CollectGridInfo();
            var item = getViewList.FirstOrDefault(x=>x.TestPlanTrainingId == Grid1.SelectedRowID);
            if (item != null)
            {
                this.hdTestPlanTrainingId.Text = item.TestPlanTrainingId;
                this.drpTraining.SelectedValue = item.TrainingId;
                this.txtTestType1Count.Text = item.TestType1Count.ToString();
                this.txtTestType2Count.Text = item.TestType2Count.ToString();
                this.txtTestType3Count.Text = item.TestType3Count.ToString();
                this.drpUserType.SelectedValue = item.UserType;
            }
        }
        #endregion

        #region  删除数据
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
               var getViewList = this.CollectGridInfo();
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();                   
                    var item = getViewList.FirstOrDefault(x => x.TestPlanTrainingId == rowID);
                    if (item != null)
                    {
                        getViewList.Remove(item);
                    }
                }

                this.Grid1.DataSource = getViewList;
                this.Grid1.DataBind();
            }
        }
        #endregion

        #region 收集页面信息
        /// <summary>
        ///  收集页面信息
        /// </summary>
        /// <returns></returns>
        private List<Model.View_Test_TestPlanTraining> CollectGridInfo()
        {
            List<Model.View_Test_TestPlanTraining> getViewList = new List<Model.View_Test_TestPlanTraining>();
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                Model.View_Test_TestPlanTraining newView = new Model.View_Test_TestPlanTraining
                {
                    TestPlanTrainingId = Grid1.Rows[i].DataKeys[0].ToString(),
                    TestPlanId = this.TestPlanId,
                    UserTypeName = Grid1.Rows[i].Values[0].ToString(),
                    TestType1Count = Funs.GetNewIntOrZero(Grid1.Rows[i].Values[2].ToString()),
                    TestType2Count = Funs.GetNewIntOrZero(Grid1.Rows[i].Values[3].ToString()),
                    TestType3Count = Funs.GetNewIntOrZero(Grid1.Rows[i].Values[4].ToString()),                                      
                    UserType = Grid1.Rows[i].Values[5].ToString(),
                    TrainingId = Grid1.Rows[i].Values[6].ToString(),
                };

                var getTestTraining = TestTrainingService.GetTestTrainingById(newView.TrainingId);
                if (getTestTraining != null)
                {
                    newView.TrainingCode = getTestTraining.TrainingCode;
                    newView.TrainingName = getTestTraining.TrainingName;
                }
           
                getViewList.Add(newView);
            }

            return getViewList;
        }
        #endregion

        #region 确定按钮事件
        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSure_Click(object sender, EventArgs e)
        {
            if (this.drpTraining.SelectedValue != Const._Null)
            {
                var getViewList = this.CollectGridInfo();
                getViewList = getViewList.Where(x => x.TestPlanTrainingId != this.hdTestPlanTrainingId.Text).ToList();
                Model.View_Test_TestPlanTraining newView = new Model.View_Test_TestPlanTraining
                {
                    TestPlanTrainingId = SQLHelper.GetNewID(),
                    TestPlanId = this.TestPlanId,
                    TrainingId = this.drpTraining.SelectedValue,
                    TestType1Count = Funs.GetNewIntOrZero(this.txtTestType1Count.Text),
                    TestType2Count = Funs.GetNewIntOrZero(this.txtTestType2Count.Text),
                    TestType3Count = Funs.GetNewIntOrZero(this.txtTestType3Count.Text)
                };
                var getTestTraining = TestTrainingService.GetTestTrainingById(newView.TrainingId);
                if (getTestTraining != null)
                {
                    newView.TrainingCode = getTestTraining.TrainingCode;
                    newView.TrainingName = getTestTraining.TrainingName;
                }
              
                if (this.drpUserType.SelectedValue != Const._Null)
                {
                    newView.UserType = this.drpUserType.SelectedValue;
                    newView.UserTypeName = this.drpUserType.SelectedText;
                }
                getViewList.Add(newView);

                this.Grid1.DataSource = getViewList;
                this.Grid1.DataBind();
                this.InitText();              
            }
        }
        #endregion

        #region 页面清空
        /// <summary>
        /// 页面清空
        /// </summary>
        private void InitText()
        {
            this.hdTestPlanTrainingId.Text = string.Empty;
            this.drpUserType.SelectedIndex = 0;
            this.drpTraining.SelectedIndex = 0;
            this.txtTestType1Count.Text =string.Empty;
            this.txtTestType2Count.Text = string.Empty;
            this.txtTestType3Count.Text = string.Empty;
        }
        #endregion

        #region 保存方法
        /// <summary>
        /// 保存按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SavaData(Const.BtnSave);
        }

        /// <summary>
        ///  提交按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SavaData(Const.BtnSubmit);
        }

        /// <summary>
        /// 
        /// </summary>
        private void SavaData(string type)
        {
            Model.Test_TestPlan newTestPlan = new Model.Test_TestPlan
            {
                PlanCode = this.txtPlanCode.Text.Trim(),
                PlanName = this.txtPlanName.Text.Trim(),
                PlanDate = Funs.GetNewDateTime(this.txtPlanDate.Text),
                TestStartTime = Funs.GetNewDateTime(this.txtTestStartTime.Text),
                TestEndTime = Funs.GetNewDateTime(this.txtTestEndTime.Text),
                Duration=Funs.GetNewInt(this.txtDuration.Text),
                TestPalce=this.txtTestPalce.Text,
                SValue = Funs.GetNewInt(this.txtSValue.Text),
                MValue = Funs.GetNewInt(this.txtMValue.Text),
                JValue = Funs.GetNewInt(this.txtJValue.Text),
            };
            if (this.drpPlanMan.SelectedValue != BLL.Const._Null)
            {
                newTestPlan.PlanManId = this.drpPlanMan.SelectedValue;
            }          
            newTestPlan.States = Const.State_0;
            if (type == Const.BtnSubmit)
            {
                newTestPlan.States = Const.State_1;
            }
                       
            //if (getViewList.Count() > 0)
            //{
            //    int s = getViewList.Sum(x => x.TestType1Count ?? 0);
            //    int m = getViewList.Sum(x => x.TestType2Count ?? 0);
            //    int j = getViewList.Sum(x => x.TestType3Count ?? 0);
            //    newTestPlan.QuestionCount = s + m + j;
            //    newTestPlan.TotalScore = newTestPlan.SValue * s + newTestPlan.MValue * m + newTestPlan.JValue * j;
            //}
            if (!string.IsNullOrEmpty(this.TestPlanId))
            {
                newTestPlan.TestPlanId = this.TestPlanId;
                ServerTestPlanService.UpdateTestPlan(newTestPlan);
                LogService.AddSys_Log(this.CurrUser, newTestPlan.PlanCode, newTestPlan.TestPlanId, Const.ServerTestPlanMenuId, Const.BtnModify);
                ServerTestPlanTrainingService.DeleteTestPlanTrainingByTestPlanId(this.TestPlanId);
            }
            else
            {
                this.TestPlanId = SQLHelper.GetNewID();
                newTestPlan.TestPlanId = this.TestPlanId;
                ServerTestPlanService.AddTestPlan(newTestPlan);
                LogService.AddSys_Log(this.CurrUser, newTestPlan.PlanCode, newTestPlan.TestPlanId, Const.ServerTestPlanMenuId, Const.BtnAdd);
            }

            var getViewList = this.CollectGridInfo();
            var getTestPlanTrainings = from x in getViewList
                                      select new Model.Test_TestPlanTraining
                                      {
                                          TestPlanTrainingId = x.TestPlanTrainingId,
                                          TestPlanId = this.TestPlanId,
                                          TrainingId = x.TrainingId,
                                          TestType1Count = x.TestType1Count,
                                          TestType2Count = x.TestType2Count,
                                          TestType3Count = x.TestType3Count,
                                          UserType = x.UserType,
                                      };
           if(getTestPlanTrainings.Count() >0 )
            {
                new Model.SGGLDB(Funs.ConnString).Test_TestPlanTraining.InsertAllOnSubmit(getTestPlanTrainings);
                new Model.SGGLDB(Funs.ConnString).SubmitChanges();
            }

            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        #endregion
    }
}