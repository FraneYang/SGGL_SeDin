using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.OfficeCheck.Check
{
    public partial class CheckContentEdit1 : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string CheckNoticeId
        {
            get
            {
                return (string)ViewState["CheckNoticeId"];
            }
            set
            {
                ViewState["CheckNoticeId"] = value;
            }
        }
        #endregion

        #region 加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetButtonPower();
                BLL.ProjectService.InitAllProjectDropDownList(this.drpSubjectProject, true);
                string type = Request.Params["type"];//查看
                if (type == "1")
                {
                    this.btnSave.Hidden = true;
                }
                this.CheckNoticeId = Request.Params["CheckNoticeId"];                
                if (!string.IsNullOrEmpty(this.CheckNoticeId))
                {
                    var table1 = BLL.CheckTable1Service.GetCheckTable1ByCheckNoticeId(this.CheckNoticeId);
                    if (table1 != null)
                    {
                        if (!string.IsNullOrEmpty(table1.SubjectProjectId))
                        {
                            this.drpSubjectProject.SelectedValue = table1.SubjectProjectId;
                        }
                        if (!string.IsNullOrEmpty(table1.CheckMan))
                        {
                            this.txtCheckMan.Text = table1.CheckMan;
                        }
                        else
                        {
                            this.txtCheckMan.Text = this.CurrUser.UserName;
                        }
                        this.txtCheckLeader.Text = table1.CheckLeader;
                        this.txtSubjectUnitMan.Text = table1.SubjectUnitMan;
                        this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", table1.CheckDate);
                        this.txtSubjectUnitDate.Text = string.Format("{0:yyyy-MM-dd}", table1.SubjectUnitDate);
                        if (table1.Total100Score.HasValue)
                        {
                            this.lbTotal100Score.Text = "本表百分制得分 = （实查项实得分之和/实查项应得满分之和*100）   " + table1.Total100Score + "   分";
                        }
                        else
                        {
                            this.lbTotal100Score.Text = "本表百分制得分 = （实查项实得分之和/实查项应得满分之和*100）       分";
                        }

                        if (table1.TotalLastScore.HasValue)
                        {
                            this.lbTotalLastScore.Text = "综合评定得分 = 本表得分 - 负面清单罚分 =   " + table1.TotalLastScore + "   分";
                        }
                        else
                        {
                            this.lbTotalLastScore.Text = "综合评定得分 = 本表得分 - 负面清单罚分 =       分";
                        }

                        this.lbEvaluationResult.Text = table1.EvaluationResult;
                    }
                    this.BindGrid();
                }
            }
        }
        #endregion

        #region 数据绑定
        private void BindGrid()
        {
            string strSql = @"SELECT [ID], 
                                     [SortIndex], 
                                     [CheckItem], 
                                     [CheckStandard], 
                                     [CheckMethod], 
                                     [CheckResult], 
                                     [BaseScore], 
                                     [DeletScore], 
                                     [GetScore], 
                                     [Type], 
                                     [CheckNoticeId]"
                         + @" FROM ProjectSupervision_Check1"
                         + @" WHERE Type='1' AND CheckNoticeId=@CheckNoticeId ORDER BY SortIndex";
            SqlParameter[] parameter = new SqlParameter[]
                    {
                        new SqlParameter("@CheckNoticeId",this.CheckNoticeId),
                    };
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            this.Grid1.DataSource = tb;
            //求和
            this.OutputSummaryData(tb);

            if (tb.Rows.Count == 0)
            {
                string strSql2 = @"SELECT [ID], 
                                          [SortIndex], 
                                          [CheckItem], 
                                          [CheckStandard], 
                                          [CheckMethod], 
                                          [BaseScore], 
                                          [Type], 
                                          [Indexs],
                                          NULL as DeletScore,
                                          [BaseScore] AS GetScore"
                            + @" FROM [dbo].[ProjectSupervision_CheckTemplate]"
                            + @" WHERE Type='1' ORDER BY [Indexs]";
                List<SqlParameter> listStr2 = new List<SqlParameter>();

                SqlParameter[] parameter2 = listStr2.ToArray();
                DataTable tb2 = SQLHelper.GetDataTableRunText(strSql2, parameter2);
                this.Grid1.DataSource = tb2;
                //求和
                this.OutputSummaryData(tb2);
            }
            this.Grid1.DataBind();
        }
        /// <summary>
        /// 合计值
        /// </summary>
        /// <param name="source"></param>
        private void OutputSummaryData(DataTable source)
        {
            decimal baseScoreTotal = 0;
            decimal deletScoreTotal = 0;
            decimal getScoreTotal = 0;
            List<string> lists = new List<string>();
            foreach (DataRow row in source.Rows)
            {
                var checks = BLL.ProjectSupervision_Check1Service.GetCheck1ByCheckItem(row["CheckItem"].ToString(), this.CheckNoticeId);
                if (checks != null)
                {
                    if (!lists.Contains(checks.CheckItem))
                    {
                        baseScoreTotal += Funs.GetNewDecimalOrZero(row["BaseScore"].ToString());
                        deletScoreTotal += Funs.GetNewDecimalOrZero(row["DeletScore"].ToString());
                        getScoreTotal += Funs.GetNewDecimalOrZero(row["GetScore"].ToString());

                        lists.Add(row["CheckItem"].ToString());
                    }
                }
                else
                {
                    var temp = Funs.DB.ProjectSupervision_CheckTemplate.FirstOrDefault(x => x.CheckItem == row["CheckItem"].ToString() && x.Type == "1");
                    if (temp != null)
                    {
                        if (!lists.Contains(temp.CheckItem))
                        {
                            baseScoreTotal += Funs.GetNewDecimalOrZero(row["BaseScore"].ToString());
                            deletScoreTotal += Funs.GetNewDecimalOrZero(row["DeletScore"].ToString());
                            getScoreTotal += Funs.GetNewDecimalOrZero(row["GetScore"].ToString());

                            lists.Add(row["CheckItem"].ToString());
                        }
                    }
                }
            }

            JObject summary = new JObject();
            summary.Add("CheckItem", "合计分");
            summary.Add("BaseScore", baseScoreTotal);
            summary.Add("DeletScore", deletScoreTotal);
            summary.Add("GetScore", getScoreTotal);
            this.Grid1.SummaryData = summary;
        }
        #endregion

        #region 排序
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 保存按钮事件
        /// <summary>
        ///  保存按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.drpSubjectProject.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择项目名称！", MessageBoxIcon.Warning);
                return;
            }
            if (this.Grid1.GetModifiedData().Count > 0)
            {
                BLL.ProjectSupervision_Check1Service.DeleteCheckByNoticeId(this.CheckNoticeId);

                JArray teamGroupData = this.Grid1.GetMergedData();
                foreach (JObject teamGroupRow in teamGroupData)
                {
                    JObject values = teamGroupRow.Value<JObject>("values");
                    var checks = BLL.ProjectSupervision_Check1Service.GetCheck1ByCheckItem(values.Value<string>("CheckItem"), this.CheckNoticeId);
                    if (checks == null)
                    {
                        Model.ProjectSupervision_Check1 newCheck = new Model.ProjectSupervision_Check1();
                        newCheck.ID = SQLHelper.GetNewID(typeof(Model.ProjectSupervision_Check1));
                        newCheck.SortIndex = Funs.GetNewInt(values.Value<string>("SortIndex"));
                        newCheck.CheckItem = values.Value<string>("CheckItem");
                        newCheck.CheckStandard = values.Value<string>("CheckStandard");
                        newCheck.CheckMethod = values.Value<string>("CheckMethod");
                        newCheck.CheckResult = values.Value<string>("CheckResult");
                        newCheck.BaseScore = Funs.GetNewDecimalOrZero(values.Value<string>("BaseScore"));
                        newCheck.DeletScore = Funs.GetNewDecimalOrZero(values.Value<string>("DeletScore"));
                        newCheck.GetScore = Funs.GetNewDecimalOrZero(values.Value<string>("GetScore"));
                        newCheck.Type = "1";
                        newCheck.CheckNoticeId = this.CheckNoticeId;
                        BLL.ProjectSupervision_Check1Service.AddCheck1(newCheck);
                    }
                    else
                    {
                        Model.ProjectSupervision_Check1 newCheck = new Model.ProjectSupervision_Check1();
                        newCheck.ID = SQLHelper.GetNewID(typeof(Model.ProjectSupervision_Check1));
                        newCheck.SortIndex = checks.SortIndex;
                        newCheck.CheckItem = checks.CheckItem;
                        newCheck.CheckStandard = values.Value<string>("CheckStandard");
                        newCheck.CheckMethod = checks.CheckMethod;
                        newCheck.CheckResult = checks.CheckResult;
                        newCheck.BaseScore = checks.BaseScore;
                        newCheck.DeletScore = checks.DeletScore;
                        newCheck.GetScore = checks.GetScore;
                        newCheck.Type = "1";
                        newCheck.CheckNoticeId = this.CheckNoticeId;
                        BLL.ProjectSupervision_Check1Service.AddCheck1(newCheck);
                    }
                }
            }

            decimal totalBaseScore = 0; ///总基准分
            decimal totalDeletScore = 0; ///总扣减分
            decimal totalGetScore = 0; ///总得分
            decimal total100Score = 0; ///换算100分制
            decimal totalLastScore = 0; ///综合得分
            var tale1Item = from x in Funs.DB.ProjectSupervision_Check1 where x.CheckNoticeId == this.CheckNoticeId && x.Type == "1" orderby x.SortIndex select x;
            if (tale1Item.Count() > 0)
            {
                List<string> lists = new List<string>();
                foreach (var item in tale1Item)
                {
                    var result = lists.Exists(t => t == item.CheckItem);//如果是相同的检查项目，则只增加其中一条的分数
                    if (!result)
                    {
                        totalBaseScore += item.BaseScore.Value;
                        totalDeletScore += item.DeletScore.Value;
                        lists.Add(item.CheckItem);
                    }
                }

                if (totalBaseScore > 0)
                {
                    totalGetScore = totalBaseScore - totalDeletScore;
                    decimal sS = (totalGetScore / totalBaseScore) * 100;
                    total100Score = Math.Round(sS, 2);
                    totalLastScore = (total100Score - Funs.GetNewDecimalOrZero(this.hdTotalDeletScore6_7.Text));
                }
            }

            BLL.CheckTable1Service.DeleteCheckTable1ByNoticeId(this.CheckNoticeId);
            Model.ProjectSupervision_CheckTable1 table1 = new Model.ProjectSupervision_CheckTable1();
            if (this.drpSubjectProject.SelectedValue != BLL.Const._Null && !string.IsNullOrEmpty(this.drpSubjectProject.SelectedValue))
            {
                table1.SubjectProjectId = this.drpSubjectProject.SelectedValue;
            }

            table1.CheckMan = this.txtCheckMan.Text.Trim();
            table1.CheckLeader = this.txtCheckLeader.Text.Trim();
            table1.CheckDate = Funs.GetNewDateTime(this.txtCheckDate.Text);
            table1.SubjectUnitMan = this.txtSubjectUnitMan.Text.Trim();
            table1.SubjectUnitDate = Funs.GetNewDateTime(this.txtSubjectUnitDate.Text);
            table1.TotalBaseScore = totalBaseScore;
            table1.TotalDeletScore = totalDeletScore;
            table1.TotalGetScore = totalGetScore;
            table1.Total100Score = total100Score;
            table1.TotalLastScore = totalLastScore;
            table1.EvaluationResult = Funs.ReturnEvaluationResultByScore(table1.TotalLastScore);
            table1.CheckNoticeId = this.CheckNoticeId;
            table1.CheckItemId = SQLHelper.GetNewID(typeof(Model.ProjectSupervision_CheckTable1));
            BLL.CheckTable1Service.AddCheckTable1(table1);

            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion 

        #region 按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.CheckNoticeMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion
    }
}