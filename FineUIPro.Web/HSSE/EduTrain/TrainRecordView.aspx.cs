﻿using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.EduTrain
{
    public partial class TrainRecordView : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
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
        /// <summary>
        /// 考试计划ID
        /// </summary>
        public string TestPlanId
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
        /// <summary>
        /// 定义集合
        /// </summary>
        private static List<Model.View_EduTrain_TrainRecordDetail> trainRecordDetails = new List<Model.View_EduTrain_TrainRecordDetail>();
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
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                trainRecordDetails.Clear();
                this.TrainingId = Request.Params["TrainingId"];
                var trainRecord = BLL.EduTrain_TrainRecordService.GetTrainingByTrainingId(this.TrainingId);
                if (trainRecord != null)
                {
                    this.txtTrainingCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.TrainingId);
                    Model.Base_TrainType trainType = BLL.TrainTypeService.GetTrainTypeById(trainRecord.TrainTypeId);
                    if (trainType != null)
                    {
                        this.txtTrainType.Text = trainType.TrainTypeName;
                    }
                    Model.Base_TrainLevel trainLevel = BLL.TrainLevelService.GetTrainLevelById(trainRecord.TrainLevelId);
                    if (trainLevel != null)
                    {
                        this.txtTrainLevel.Text = trainLevel.TrainLevelName;
                    }
                    this.txtTrainTitle.Text = trainRecord.TrainTitle;
                    if (!string.IsNullOrEmpty(trainRecord.UnitIds))
                    {
                        string unitNames = string.Empty;
                        string[] unitIds = trainRecord.UnitIds.Split(',');
                        foreach (var item in unitIds)
                        {
                            Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(item);
                            if (unit != null)
                            {
                                unitNames += unit.UnitName + ",";
                            }
                        }
                        if (!string.IsNullOrEmpty(unitNames))
                        {
                            unitNames = unitNames.Substring(0, unitNames.LastIndexOf(","));
                        }
                        this.txtUnits.Text = unitNames;
                    }

                    if (!string.IsNullOrEmpty(trainRecord.WorkPostIds))
                    {
                        string WorkPostNames = string.Empty;
                        string[] WorkPostIds = trainRecord.WorkPostIds.Split(',');
                        foreach (var item in WorkPostIds)
                        {
                            Model.Base_WorkPost WorkPost = BLL.WorkPostService.GetWorkPostById(item);
                            if (WorkPost != null)
                            {
                                WorkPostNames += WorkPost.WorkPostName + ",";
                            }
                        }
                        if (!string.IsNullOrEmpty(WorkPostNames))
                        {
                            WorkPostNames = WorkPostNames.Substring(0, WorkPostNames.LastIndexOf(","));
                        }
                        this.txtWorkPostIds.Text = WorkPostNames;
                    }

                    this.txtTeachMan.Text = trainRecord.TeachMan;
                    this.txtTeachAddress.Text = trainRecord.TeachAddress;
                    if (trainRecord.TeachHour != null)
                    {
                        this.txtTeachHour.Text = trainRecord.TeachHour.ToString();
                    }
                    if (trainRecord.TrainStartDate != null)
                    {
                        this.txtTrainStartDate.Text = string.Format("{0:yyyy-MM-dd}", trainRecord.TrainStartDate);
                    }
                    if (trainRecord.TrainPersonNum != null)
                    {
                        this.txtTrainPersonNum.Text = Convert.ToString(trainRecord.TrainPersonNum);
                    }
                    this.txtTrainContent.Text = trainRecord.TrainContent;
                    var getTestPlan = TestPlanService.GetTestPlanByPlanId(trainRecord.PlanId);
                    if (getTestPlan != null)
                    {
                        this.TestPlanId = getTestPlan.TestPlanId;
                    }
                }

                this.Grid1DataBind();
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectTrainRecordMenuId;
                this.ctlAuditFlow.DataId = this.TrainingId;
            }
        }
        #endregion

        #region 绑定 Grid1
        /// <summary>
        /// 绑定 Grid1
        /// </summary>
        private void Grid1DataBind()
        {
            trainRecordDetails = (from x in Funs.DB.View_EduTrain_TrainRecordDetail
                                  where x.TrainingId == this.TrainingId
                                  orderby x.UnitName, x.PersonName
                                  select x).ToList();
            Grid1.DataSource = trainRecordDetails;
            Grid1.DataBind();
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                bool isRed = true;
                string trainDetailId = Grid1.Rows[i].DataKeys[0].ToString();
                var getAtt = Funs.DB.AttachFile.FirstOrDefault(x => x.ToKeyId == trainDetailId);
                if (getAtt != null)
                {
                    isRed = false;
                }
                if (isRed)
                {
                    var getRecordDetail = trainRecordDetails.FirstOrDefault(x => x.TrainDetailId == trainDetailId);
                    if (getRecordDetail != null && !string.IsNullOrEmpty(this.TestPlanId)) 
                    {
                        var getTestRecord = Funs.DB.Training_TestRecord.FirstOrDefault(x => x.TestManId == getRecordDetail.PersonId && x.TestPlanId == this.TestPlanId);
                        if (getTestRecord != null)
                        {
                            isRed = false;
                        }
                    }
                }
                if (isRed)
                {
                    Grid1.Rows[i].RowCssClass = "Red";
                }
            }
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TrainingId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/TrainRecord&menuId={1}&type=-1", this.TrainingId, BLL.Const.ProjectTrainRecordMenuId)));
            }
        }
        #endregion

        /// <summary>
        /// 培训试题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTrainTest_Click(object sender, EventArgs e)
        {           
            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("TrainTest.aspx?TrainingId={0}", this.TrainingId, "查看 - ")));
        }

        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            var trainDetailId = Grid1.SelectedRowID;
            var getAtt = Funs.DB.AttachFile.FirstOrDefault(x => x.ToKeyId == trainDetailId);
            if (getAtt != null)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/TrainRecord&menuId={1}&type=-1", trainDetailId, BLL.Const.ProjectTrainRecordMenuId)));
            }
            else
            {
                var getRecordDetail = trainRecordDetails.FirstOrDefault(x => x.TrainDetailId == trainDetailId);
                if (getRecordDetail != null && !string.IsNullOrEmpty(this.TestPlanId))
                {
                    var getTestRecord = Funs.DB.Training_TestRecord.Where(x => x.TestManId == getRecordDetail.PersonId
                    && x.TestPlanId == this.TestPlanId);
                    if (getTestRecord.Count() > 0)
                    {    ////及格分数
                        int getPassScores = SysConstSetService.getPassScore();
                        var getOK = getTestRecord.FirstOrDefault(x => x.TestScores >= getPassScores);
                        if (getOK != null)
                        {
                            PrinterDocService.PrinterDocMethod(Const.ProjectTestRecordMenuId, getOK.TestRecordId, "试卷");
                        }
                        else
                        {
                            PrinterDocService.PrinterDocMethod(Const.ProjectTestRecordMenuId, getTestRecord.FirstOrDefault().TestRecordId, "试卷");
                        }
                    }
                    else
                    {
                    }
                }
            }
        }

        #region 格式化字符串
        /// <summary>
        /// 格式化受伤情况
        /// </summary>
        /// <param name="injury"></param>
        /// <returns></returns>
        protected string GetCheckScore(object TrainDetailId)
        {
            string values = string.Empty;
            var getTrainRecordDetail = Funs.DB.EduTrain_TrainRecordDetail.FirstOrDefault(x => x.TrainDetailId == TrainDetailId.ToString());
            if (getTrainRecordDetail != null)
            {
                var getTrainRecord = Funs.DB.EduTrain_TrainRecord.FirstOrDefault(x => x.TrainingId == getTrainRecordDetail.TrainingId);
                if (getTrainRecord != null)
                {
                    var getTestPlan = Funs.DB.Training_TestPlan.FirstOrDefault(x => x.PlanId == getTrainRecord.PlanId);
                    if (getTestPlan != null)
                    {
                        decimal? scors = 0;
                        var getTestRecord = Funs.DB.Training_TestRecord.Where(x => x.TestPlanId == getTestPlan.TestPlanId && x.TestManId == getTrainRecordDetail.PersonId);
                        foreach (var item in getTestRecord)
                        {
                            if (scors == 0)
                            {
                                scors = item.TestScores;
                            }
                            else
                            {
                                if (item.TestScores < scors)
                                {
                                    scors = item.TestScores ?? 0;
                                }
                            }
                        }

                        values = scors.ToString();
                    }
                }
            }
            return values;
        }
        #endregion
    }
}