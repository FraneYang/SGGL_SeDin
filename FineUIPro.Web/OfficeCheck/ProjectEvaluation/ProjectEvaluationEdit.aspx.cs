using BLL;
using System;

namespace FineUIPro.Web.OfficeCheck.ProjectEvaluation
{
    public partial class ProjectEvaluationEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string PerfomanceRecordId
        {
            get
            {
                return (string)ViewState["PerfomanceRecordId"];
            }
            set
            {
                ViewState["PerfomanceRecordId"] = value;
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.InitDropDownList();
                ProjectId = Request.Params["ProjectId"];
                this.PerfomanceRecordId = Request.Params["PerfomanceRecordId"];
                if (!string.IsNullOrEmpty(this.PerfomanceRecordId))
                {
                    Model.ProjectSupervision_ProjectEvaluation perfomanceRecord = BLL.ProjectEvaluationService.GetPerfomanceRecordById(this.PerfomanceRecordId);
                    if (perfomanceRecord != null)
                    {
                        this.ProjectId = perfomanceRecord.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtPerfomanceRecordCode.Text = perfomanceRecord.PerfomanceRecordCode;
                        this.txtEvaluationDate.Text = string.Format("{0:yyyy-MM-dd}", perfomanceRecord.EvaluationDate);
                        this.txtEvaluationDef.Text = perfomanceRecord.EvaluationDef;
                        if (!string.IsNullOrEmpty(perfomanceRecord.RewardOrPunish))
                        {
                            this.drpRewardOrPunish.SelectedValue = perfomanceRecord.RewardOrPunish;
                        }
                        if (perfomanceRecord.RPMoney.HasValue)
                        {
                            this.txtRPMoney.Text = Convert.ToString(perfomanceRecord.RPMoney);
                        }
                        this.txtAssessmentGroup.Text = perfomanceRecord.AssessmentGroup;
                        this.txtBehavior_1.Text = perfomanceRecord.Behavior_1;
                        this.txtBehavior_2.Text = perfomanceRecord.Behavior_2;
                        this.txtBehavior_3.Text = perfomanceRecord.Behavior_3;
                        this.txtBehavior_4.Text = perfomanceRecord.Behavior_4;
                        this.txtBehavior_5.Text = perfomanceRecord.Behavior_5;
                        this.txtBehavior_6.Text = perfomanceRecord.Behavior_6;
                        this.txtBehavior_7.Text = perfomanceRecord.Behavior_7;
                        this.txtBehavior_8.Text = perfomanceRecord.Behavior_8;
                        this.txtBehavior_9.Text = perfomanceRecord.Behavior_9;
                        this.txtBehavior_10.Text = perfomanceRecord.Behavior_10;
                        this.txtBehavior_11.Text = perfomanceRecord.Behavior_11;
                        this.txtBehavior_12.Text = perfomanceRecord.Behavior_12;
                        this.txtBehavior_13.Text = perfomanceRecord.Behavior_13;
                        this.txtBehavior_14.Text = perfomanceRecord.Behavior_14;
                        this.txtBehavior_15.Text = perfomanceRecord.Behavior_15;
                        this.txtBehavior_16.Text = perfomanceRecord.Behavior_16;
                        this.txtBehavior_17.Text = perfomanceRecord.Behavior_17;
                        this.txtBehavior_18.Text = perfomanceRecord.Behavior_18;
                        this.txtBehavior_19.Text = perfomanceRecord.Behavior_19;
                        this.txtBehavior_20.Text = perfomanceRecord.Behavior_20;
                        this.txtScore_1.Text = Convert.ToString(perfomanceRecord.Score_1);
                        this.txtScore_2.Text = Convert.ToString(perfomanceRecord.Score_2);
                        this.txtScore_3.Text = Convert.ToString(perfomanceRecord.Score_3);
                        this.txtScore_4.Text = Convert.ToString(perfomanceRecord.Score_4);
                        this.txtScore_5.Text = Convert.ToString(perfomanceRecord.Score_5);
                        this.txtScore_6.Text = Convert.ToString(perfomanceRecord.Score_6);
                        this.txtScore_7.Text = Convert.ToString(perfomanceRecord.Score_7);
                        this.txtScore_8.Text = Convert.ToString(perfomanceRecord.Score_8);
                        this.txtScore_9.Text = Convert.ToString(perfomanceRecord.Score_9);
                        this.txtScore_10.Text = Convert.ToString(perfomanceRecord.Score_10);
                        this.txtScore_11.Text = Convert.ToString(perfomanceRecord.Score_11);
                        this.txtScore_12.Text = Convert.ToString(perfomanceRecord.Score_12);
                        this.txtScore_13.Text = Convert.ToString(perfomanceRecord.Score_13);
                        this.txtScore_14.Text = Convert.ToString(perfomanceRecord.Score_14);
                        this.txtScore_15.Text = Convert.ToString(perfomanceRecord.Score_15);
                        this.txtScore_16.Text = Convert.ToString(perfomanceRecord.Score_16);
                        this.txtScore_17.Text = Convert.ToString(perfomanceRecord.Score_17);
                        this.txtScore_18.Text = Convert.ToString(perfomanceRecord.Score_18);
                        this.txtScore_19.Text = Convert.ToString(perfomanceRecord.Score_19);
                        this.txtScore_20.Text = Convert.ToString(perfomanceRecord.Score_20);
                        this.txtTotalJudging.Text = perfomanceRecord.TotalJudging;
                        this.txtTotalScore.Text = Convert.ToString(perfomanceRecord.TotalScore);
                    }
                }
                else
                {
                    this.txtEvaluationDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }
                Model.Base_Project project = BLL.ProjectService.GetProjectByProjectId(this.ProjectId);
                if (project != null)
                {
                    this.txtProjectName.Text = project.ProjectName;
                }
            }
        }
        #endregion

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            BLL.ConstValue.InitConstValueDropDownList(this.drpRewardOrPunish, BLL.ConstValue.Group_RewardOrPunish, true);
        }

        #region 保存、提交
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.ProjectSupervision_ProjectEvaluation perfomanceRecord = new Model.ProjectSupervision_ProjectEvaluation
            {
                ProjectId = this.ProjectId,
                PerfomanceRecordCode = this.txtPerfomanceRecordCode.Text.Trim(),
                EvaluationDate = Funs.GetNewDateTime(this.txtEvaluationDate.Text.Trim()),
                EvaluationDef = this.txtEvaluationDef.Text.Trim()
            };
            if (this.drpRewardOrPunish.SelectedValue != BLL.Const._Null)
            {
                perfomanceRecord.RewardOrPunish = this.drpRewardOrPunish.SelectedValue;
            }
            perfomanceRecord.RPMoney = Funs.GetNewDecimalOrZero(this.txtRPMoney.Text.Trim());
            perfomanceRecord.AssessmentGroup = this.txtAssessmentGroup.Text.Trim();
            perfomanceRecord.Behavior_1 = this.txtBehavior_1.Text.Trim();
            perfomanceRecord.Behavior_2 = this.txtBehavior_2.Text.Trim();
            perfomanceRecord.Behavior_3 = this.txtBehavior_3.Text.Trim();
            perfomanceRecord.Behavior_4 = this.txtBehavior_4.Text.Trim();
            perfomanceRecord.Behavior_5 = this.txtBehavior_5.Text.Trim();
            perfomanceRecord.Behavior_6 = this.txtBehavior_6.Text.Trim();
            perfomanceRecord.Behavior_7 = this.txtBehavior_7.Text.Trim();
            perfomanceRecord.Behavior_8 = this.txtBehavior_8.Text.Trim();
            perfomanceRecord.Behavior_9 = this.txtBehavior_9.Text.Trim();
            perfomanceRecord.Behavior_10 = this.txtBehavior_10.Text.Trim();
            perfomanceRecord.Behavior_11 = this.txtBehavior_11.Text.Trim();
            perfomanceRecord.Behavior_12 = this.txtBehavior_12.Text.Trim();
            perfomanceRecord.Behavior_13 = this.txtBehavior_13.Text.Trim();
            perfomanceRecord.Behavior_14 = this.txtBehavior_14.Text.Trim();
            perfomanceRecord.Behavior_15 = this.txtBehavior_15.Text.Trim();
            perfomanceRecord.Behavior_16 = this.txtBehavior_16.Text.Trim();
            perfomanceRecord.Behavior_17 = this.txtBehavior_17.Text.Trim();
            perfomanceRecord.Behavior_18 = this.txtBehavior_18.Text.Trim();
            perfomanceRecord.Behavior_19 = this.txtBehavior_19.Text.Trim();
            perfomanceRecord.Behavior_20 = this.txtBehavior_20.Text.Trim();
            perfomanceRecord.Score_1 = Funs.GetNewDecimalOrZero(this.txtScore_1.Text.Trim());
            perfomanceRecord.Score_2 = Funs.GetNewDecimalOrZero(this.txtScore_2.Text.Trim());
            perfomanceRecord.Score_3 = Funs.GetNewDecimalOrZero(this.txtScore_3.Text.Trim());
            perfomanceRecord.Score_4 = Funs.GetNewDecimalOrZero(this.txtScore_4.Text.Trim());
            perfomanceRecord.Score_5 = Funs.GetNewDecimalOrZero(this.txtScore_5.Text.Trim());
            perfomanceRecord.Score_6 = Funs.GetNewDecimalOrZero(this.txtScore_6.Text.Trim());
            perfomanceRecord.Score_7 = Funs.GetNewDecimalOrZero(this.txtScore_7.Text.Trim());
            perfomanceRecord.Score_8 = Funs.GetNewDecimalOrZero(this.txtScore_8.Text.Trim());
            perfomanceRecord.Score_9 = Funs.GetNewDecimalOrZero(this.txtScore_9.Text.Trim());
            perfomanceRecord.Score_10 = Funs.GetNewDecimalOrZero(this.txtScore_10.Text.Trim());
            perfomanceRecord.Score_11 = Funs.GetNewDecimalOrZero(this.txtScore_11.Text.Trim());
            perfomanceRecord.Score_12 = Funs.GetNewDecimalOrZero(this.txtScore_12.Text.Trim());
            perfomanceRecord.Score_13 = Funs.GetNewDecimalOrZero(this.txtScore_13.Text.Trim());
            perfomanceRecord.Score_14 = Funs.GetNewDecimalOrZero(this.txtScore_14.Text.Trim());
            perfomanceRecord.Score_15 = Funs.GetNewDecimalOrZero(this.txtScore_15.Text.Trim());
            perfomanceRecord.Score_16 = Funs.GetNewDecimalOrZero(this.txtScore_16.Text.Trim());
            perfomanceRecord.Score_17 = Funs.GetNewDecimalOrZero(this.txtScore_17.Text.Trim());
            perfomanceRecord.Score_18 = Funs.GetNewDecimalOrZero(this.txtScore_18.Text.Trim());
            perfomanceRecord.Score_19 = Funs.GetNewDecimalOrZero(this.txtScore_19.Text.Trim());
            perfomanceRecord.Score_20 = Funs.GetNewDecimalOrZero(this.txtScore_20.Text.Trim());
            perfomanceRecord.TotalJudging = this.txtTotalJudging.Text.Trim();
            perfomanceRecord.TotalScore = Funs.GetNewDecimalOrZero(this.txtTotalScore.Text.Trim());
            perfomanceRecord.States = BLL.Const.State_2;
            perfomanceRecord.CompileMan = this.CurrUser.UserId;
            perfomanceRecord.CompileDate = DateTime.Now;
            
            if (!string.IsNullOrEmpty(this.PerfomanceRecordId))
            {
                perfomanceRecord.PerfomanceRecordId = this.PerfomanceRecordId;
                BLL.ProjectEvaluationService.UpdatePerfomanceRecord(perfomanceRecord);
                BLL.LogService.AddSys_Log(this.CurrUser, perfomanceRecord.PerfomanceRecordCode, perfomanceRecord.PerfomanceRecordId,BLL.Const.PerfomanceRecordMenuId,BLL.Const.BtnModify);
            }
            else
            {
                this.PerfomanceRecordId = SQLHelper.GetNewID(typeof(Model.ProjectSupervision_ProjectEvaluation));
                perfomanceRecord.PerfomanceRecordId = this.PerfomanceRecordId;
                BLL.ProjectEvaluationService.AddPerfomanceRecord(perfomanceRecord);
                BLL.LogService.AddSys_Log(this.CurrUser, perfomanceRecord.PerfomanceRecordCode, perfomanceRecord.PerfomanceRecordId, BLL.Const.PerfomanceRecordMenuId, BLL.Const.BtnAdd);
            }
        }
        #endregion

        #region 计算总分数
        /// <summary>
        /// 计算总分数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_OnTextChanged(object sender, EventArgs e)
        {
            decimal score_1 = 0, score_2 = 0, score_3 = 0, score_4 = 0, score_5 = 0, score_6 = 0, score_7 = 0, score_8 = 0, score_9 = 0, score_10 = 0, score_11 = 0, score_12 = 0, score_13 = 0, score_14 = 0, score_15 = 0, score_16 = 0, score_17 = 0, score_18 = 0, score_19 = 0, score_20 = 0;
            if (!string.IsNullOrEmpty(this.txtScore_1.Text.Trim()))
            {
                score_1 = Convert.ToDecimal(this.txtScore_1.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtScore_2.Text.Trim()))
            {
                score_2 = Convert.ToDecimal(this.txtScore_2.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtScore_3.Text.Trim()))
            {
                score_3 = Convert.ToDecimal(this.txtScore_3.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtScore_4.Text.Trim()))
            {
                score_4 = Convert.ToDecimal(this.txtScore_4.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtScore_5.Text.Trim()))
            {
                score_5 = Convert.ToDecimal(this.txtScore_5.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtScore_6.Text.Trim()))
            {
                score_6 = Convert.ToDecimal(this.txtScore_6.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtScore_7.Text.Trim()))
            {
                score_7 = Convert.ToDecimal(this.txtScore_7.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtScore_8.Text.Trim()))
            {
                score_8 = Convert.ToDecimal(this.txtScore_8.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtScore_9.Text.Trim()))
            {
                score_9 = Convert.ToDecimal(this.txtScore_9.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtScore_10.Text.Trim()))
            {
                score_10 = Convert.ToDecimal(this.txtScore_10.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtScore_11.Text.Trim()))
            {
                score_11 = Convert.ToDecimal(this.txtScore_11.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtScore_12.Text.Trim()))
            {
                score_12 = Convert.ToDecimal(this.txtScore_12.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtScore_13.Text.Trim()))
            {
                score_13 = Convert.ToDecimal(this.txtScore_13.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtScore_14.Text.Trim()))
            {
                score_14 = Convert.ToDecimal(this.txtScore_14.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtScore_15.Text.Trim()))
            {
                score_15 = Convert.ToDecimal(this.txtScore_15.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtScore_16.Text.Trim()))
            {
                score_16 = Convert.ToDecimal(this.txtScore_16.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtScore_17.Text.Trim()))
            {
                score_17 = Convert.ToDecimal(this.txtScore_17.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtScore_18.Text.Trim()))
            {
                score_18 = Convert.ToDecimal(this.txtScore_18.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtScore_19.Text.Trim()))
            {
                score_19 = Convert.ToDecimal(this.txtScore_19.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtScore_20.Text.Trim()))
            {
                score_20 = Convert.ToDecimal(this.txtScore_20.Text.Trim());
            }
            this.txtTotalScore.Text = Convert.ToString(score_1 + score_2 + score_3 + score_4 + score_5 + score_6 + score_7 + score_8 + score_9 + score_10 + score_11 + score_12 + score_13 + score_14 + score_15 + score_16 + score_17 + score_18 + score_19 + score_20);
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
            if (string.IsNullOrEmpty(this.PerfomanceRecordId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ProjectEvaluation&menuId={1}", this.PerfomanceRecordId, BLL.Const.ProjectEvaluationMenuId)));
        }
        #endregion
    }
}