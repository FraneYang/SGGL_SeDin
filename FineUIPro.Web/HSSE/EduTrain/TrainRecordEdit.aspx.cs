using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FineUIPro.Web.HSSE.EduTrain
{
    public partial class TrainRecordEdit : PageBase
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
        /// 主键
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
                this.btnClose.OnClientClick = ActiveWindow.GetHidePostBackReference();
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.InitDropDownList();
                trainRecordDetails.Clear();

                this.TrainingId = Request.Params["TrainingId"];
                var trainRecord = EduTrain_TrainRecordService.GetTrainingByTrainingId(this.TrainingId);
                if (trainRecord != null)
                {
                    this.ProjectId = trainRecord.ProjectId;
                    if (this.ProjectId != this.CurrUser.LoginProjectId)
                    {
                        this.InitDropDownList();
                    }
                    this.txtTrainingCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.TrainingId);
                    if (!string.IsNullOrEmpty(trainRecord.TrainTypeId))
                    {
                        this.drpTrainType.SelectedValue = trainRecord.TrainTypeId;
                    }
                    if (!string.IsNullOrEmpty(trainRecord.TrainLevelId))
                    {
                        this.drpTrainLevel.SelectedValue = trainRecord.TrainLevelId;
                    }
                    this.txtTrainTitle.Text = trainRecord.TrainTitle;
                    if (!string.IsNullOrEmpty(trainRecord.UnitIds))
                    {
                        this.drpUnits.SelectedValueArray = trainRecord.UnitIds.Split(',');
                    }
                    if (!string.IsNullOrEmpty(trainRecord.WorkPostIds))
                    {
                        this.drpWorkPostIds.SelectedValueArray = trainRecord.WorkPostIds.Split(',');
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
                else
                {
                    ////自动生成编码
                    this.txtTrainingCode.Text = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectTrainRecordMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtTrainStartDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtTeachMan.Text = this.CurrUser.UserName;
                    this.txtTeachAddress.Text = "办公室";
                    this.txtTeachHour.Text = "1";
                    //this.txtTrainEndDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }

                this.Grid1DataBind();
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = Const.ProjectTrainRecordMenuId;
                this.ctlAuditFlow.DataId = this.TrainingId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }
        #endregion

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
                if(isRed)
                {
                    var getRecordDetail = trainRecordDetails.FirstOrDefault(x => x.TrainDetailId == trainDetailId);
                    if (getRecordDetail != null && !string.IsNullOrEmpty(this.TestPlanId)) ////未参加过培训的人员
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

        /// <summary>
        /// 
        /// </summary>
        private void InitDropDownList()
        {
            //培训类型
            TrainTypeService.InitTrainTypeDropDownList(this.drpTrainType, true);
            //培训级别
            TrainLevelService.InitTrainLevelDropDownList(this.drpTrainLevel, true);
            //培训单位
            UnitService.InitUnitDropDownList(this.drpUnits, this.ProjectId, false);
            WorkPostService.InitWorkPostDropDownList(this.drpWorkPostIds, true);
            this.drpUnits.SelectedValue = this.CurrUser.UnitId;
        }

        #region 选择按钮
        /// <summary>
        /// 选择按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {           
            if (string.IsNullOrEmpty(this.TrainingId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ShowPerson.aspx?TrainingId={0}&TrainTypeId={1}", this.TrainingId, this.drpTrainType.SelectedValue, "编辑 - ")));
        }
        #endregion

        #region 导入
        /// <summary>
        /// 导入按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (this.drpTrainType.SelectedValue == BLL.Const._Null)
            {
                ShowNotify("请选择培训类型！", MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(this.TrainingId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }

            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("TrainRecordIn.aspx?TrainingId={0}", this.TrainingId, "导入 - "), "导入", 900, 560));
        }
        #endregion

        #region 提交按钮
        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.ctlAuditFlow.NextStep == BLL.Const.State_1 && this.ctlAuditFlow.NextPerson == BLL.Const._Null)
            {
                ShowNotify("请选择下一步办理人！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        #endregion

        #region 保存按钮
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        #region 保存方法
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            int i = 0;
            if (this.drpTrainType.SelectedValue == BLL.Const._Null)
            {
                ShowNotify("请选择培训类型！", MessageBoxIcon.Warning);
                i++;
                return;
            }

            Model.EduTrain_TrainRecord trainRecord = new Model.EduTrain_TrainRecord
            {
                TrainingCode = this.txtTrainingCode.Text.Trim(),
                ProjectId = this.ProjectId,
                TrainTitle = this.txtTrainTitle.Text.Trim(),
                TrainContent = this.txtTrainContent.Text.Trim(),
                TrainStartDate = Funs.GetNewDateTime(this.txtTrainStartDate.Text.Trim()),
                TrainPersonNum = this.Grid1.Rows.Count,
                TeachHour = Funs.GetNewDecimalOrZero(this.txtTeachHour.Text.Trim()),
                TeachMan = this.txtTeachMan.Text.Trim(),
                TeachAddress = this.txtTeachAddress.Text.Trim(),
            };
            if (this.drpTrainType.SelectedValue != BLL.Const._Null)
            {
                trainRecord.TrainTypeId = this.drpTrainType.SelectedValue;
            }
            if (this.drpTrainLevel.SelectedValue != BLL.Const._Null)
            {
                trainRecord.TrainLevelId = this.drpTrainLevel.SelectedValue;
            }           
            //培训单位
            string unitIds = string.Empty;
            foreach (var item in this.drpUnits.SelectedValueArray)
            {
                unitIds += item + ",";
            }
            if (!string.IsNullOrEmpty(unitIds))
            {
                unitIds = unitIds.Substring(0, unitIds.LastIndexOf(","));
            }
            trainRecord.UnitIds = unitIds;

            //培训岗位
            string workPostIds = string.Empty;
            foreach (var item in this.drpWorkPostIds.SelectedValueArray)
            {
                workPostIds += item + ",";
            }
            if (!string.IsNullOrEmpty(workPostIds))
            {
                workPostIds = workPostIds.Substring(0, workPostIds.LastIndexOf(","));
            }
            trainRecord.WorkPostIds = workPostIds;

            ////单据状态
            trainRecord.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                trainRecord.States = this.ctlAuditFlow.NextStep;
                //if (!this.drpTrainStates.Hidden && trainRecord.TrainStates != "3" && trainRecord.States == Const.State_2)
                //{
                //    ShowNotify("当前培训还未结束，不能审核完成！", MessageBoxIcon.Warning);
                //    i++;
                //    return;
                //}
            }
            if (!string.IsNullOrEmpty(this.TrainingId))
            {
                var getTrain = BLL.EduTrain_TrainRecordService.GetTrainingByTrainingId(this.TrainingId);
                if (getTrain != null)
                {
                    trainRecord.FromRecordId = getTrain.FromRecordId;
                }

                trainRecord.TrainingId = this.TrainingId;
                BLL.EduTrain_TrainRecordService.UpdateTraining(trainRecord);
                BLL.LogService.AddSys_Log(this.CurrUser, trainRecord.TrainingCode, trainRecord.TrainingId, BLL.Const.ProjectTrainRecordMenuId, BLL.Const.BtnModify);
            }
            else
            {
                trainRecord.TrainingId = SQLHelper.GetNewID(typeof(Model.EduTrain_TrainRecord));
                this.TrainingId = trainRecord.TrainingId;
                trainRecord.CompileMan = this.CurrUser.UserId;
                BLL.EduTrain_TrainRecordService.AddTraining(trainRecord);
                BLL.LogService.AddSys_Log(this.CurrUser, trainRecord.TrainingCode, trainRecord.TrainingId, BLL.Const.ProjectTrainRecordMenuId, BLL.Const.BtnAdd);
            }

            JArray mergedData = Grid1.GetMergedData();
            foreach (JObject mergedRow in mergedData)
            {
                string status = mergedRow.Value<string>("status");
                JObject values = mergedRow.Value<JObject>("values");
                Model.EduTrain_TrainRecordDetail detail = BLL.EduTrain_TrainRecordDetailService.GetTrainDetailByTrainDetailId(values.Value<string>("TrainDetailId").ToString());
                if (detail != null)
                {
                    if (values.Value<string>("CheckResult").ToString() == "1")
                    {
                        detail.CheckResult = true;
                    }
                    else
                    {
                        detail.CheckResult = false;
                    }
                    detail.CheckScore = Funs.GetNewDecimalOrZero(values.Value<string>("CheckScore").ToString()); ;
                    BLL.EduTrain_TrainRecordDetailService.UpdateTrainDetail(detail);
                }
            }
            
            if (i == 0)
            {
                ////保存流程审核数据         
                this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectTrainRecordMenuId, this.TrainingId, (type == BLL.Const.BtnSubmit ? true : false), this.txtTrainTitle.Text.Trim(), "../EduTrain/TrainRecordView.aspx?TrainingId={0}");
            }
        }
        #endregion

        #region 关闭弹出窗
        /// <summary>
        /// 关闭弹出窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            this.Grid1DataBind();
            this.txtTrainPersonNum.Text = trainRecordDetails.Count.ToString();
        }
        #endregion

        #region 删除
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    BLL.EduTrain_TrainRecordDetailService.DeleteTrainDetailByTrainDetail(rowID);
                }

                this.Grid1DataBind();
                this.txtTrainPersonNum.Text = trainRecordDetails.Count.ToString();              
                this.ShowNotify("删除数据成功!（表格数据已重新绑定）");
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
            if (this.drpTrainType.SelectedValue == BLL.Const._Null)
            {
                ShowNotify("请选择培训类型！", MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(this.TrainingId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/TrainRecord&menuId={1}", this.TrainingId, BLL.Const.ProjectTrainRecordMenuId)));
        }
        #endregion

        /// <summary>
        /// 培训试题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTrainTest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.TrainingId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("TrainTest.aspx?TrainingId={0}", this.TrainingId, "编辑 - ")));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("TrainTestView.aspx?TrainDetailId={0}", rowID, "查看试卷 - ")));
                }                               
            }
        }

        ///// <summary>
        /////  选择培训教材类型
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnTrainingType_Click(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(this.TrainingId))
        //    {
        //        this.SaveData(BLL.Const.BtnSave);
        //    }
        //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ShowTrainingType.aspx?TrainingId={0}&TrainTypeId={1}", this.TrainingId, this.drpTrainType.SelectedValue, "编辑 - ")));
        //}

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