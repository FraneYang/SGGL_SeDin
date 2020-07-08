using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.HSSE.EduTrain
{
    public partial class TestRecordPrint : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string TestRecordId
        {
            get
            {
                return (string)ViewState["TestRecordId"];
            }
            set
            {
                ViewState["TestRecordId"] = value;
            }
        }
        /// <summary>
        ///  类型
        /// </summary>
        public string Type
        {
            get
            {
                return (string)ViewState["Type"];
            }
            set
            {
                ViewState["Type"] = value;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.lbTitleName.Text = UnitService.GetUnitNameByUnitId(Const.UnitId_SEDIN) + ProjectService.GetProjectNameByProjectId(this.CurrUser.LoginProjectId);
                this.lbTestType.Text = "培训试题";
                this.TestRecordId = Request.Params["TestRecordId"];
                this.Type = Request.Params["Type"];
                string sql = string.Empty;
                if (this.Type == "1")
                {
                    var testRecord = ServerTestRecordService.GetTestRecordById(this.TestRecordId);
                    if (testRecord != null)
                    {
                        this.lbTestType.Text = "知识竞赛" + this.lbTestType.Text;
                        string personInfo = "单位：" + UnitService.GetUnitNameByUnitId(testRecord.UnitId) + "        ";
                        if (testRecord.ManType == "3")
                        {
                            personInfo += "项目：" + ProjectService.GetProjectNameByProjectId(testRecord.ProjectId) + "        ";
                        }
                        else if (testRecord.UnitId == Const.UnitId_SEDIN)
                        {
                            personInfo += "部门：" + DepartService.getDepartNameById(testRecord.DepartId) + "        ";
                        }
                        else
                        {
                            personInfo += "部门：" + DepartService.getDepartNameById(testRecord.DepartId) + "        ";
                            personInfo += "项目：" + ProjectService.GetProjectNameByProjectId(testRecord.ProjectId) + "        ";
                        }
                        personInfo += "姓名：" + testRecord.TestManName + "        " 
                                            + "身份证号码：" + testRecord.IdentityCard + "        ";
                        if (testRecord.TestScores.HasValue)
                        {
                            personInfo += "成绩：" + testRecord.TestScores.ToString();
                        }

                        this.lbTestPerson.Text = personInfo;
                        var attachFile = new Model.SGGLDB(Funs.ConnString).AttachFile.FirstOrDefault(x => x.ToKeyId == this.TestRecordId);
                        if (attachFile != null && !string.IsNullOrEmpty(attachFile.AttachUrl))
                        {
                            List<string> listUrl = Funs.GetStrListByStr(attachFile.AttachUrl, ',');
                            int count = listUrl.Count();
                            if (count > 0)
                            {
                                this.timg1.Src = "../" + listUrl[0];
                                this.timg2.Src = "../" + listUrl[0];
                                if (count >= 2)
                                {
                                    int cout2 = count / 2;
                                    this.timg2.Src = "../" + listUrl[cout2];
                                }
                                this.timg3.Src = "../" + listUrl[count - 1];
                            }
                        }
                        var getTestPlan = new Model.SGGLDB(Funs.ConnString).Test_TestPlan.FirstOrDefault(x => x.TestPlanId == testRecord.TestPlanId);
                        if (getTestPlan != null)
                        {
                            int getA = 0, getB = 0, getC = 0;
                            var getTraining = from x in new Model.SGGLDB(Funs.ConnString).Test_TestPlanTraining
                                              where x.TestPlanId == testRecord.TestPlanId && x.UserType == testRecord.ManType
                                              select x;
                            if (getTraining.Count() > 0)
                            {
                                getA = getTraining.Sum(x => x.TestType1Count) ?? 0;
                                getB = getTraining.Sum(x => x.TestType2Count) ?? 0;
                                getC = getTraining.Sum(x => x.TestType3Count) ?? 0;
                            }

                            Namea += "（每题" + getTestPlan.SValue.ToString() + "分，共" + getTestPlan.SValue * getA + "分）";
                            Nameb += "（每题" + getTestPlan.MValue.ToString() + "分，共" + getTestPlan.MValue * getB + "分）";
                            Namec += "（每题" + getTestPlan.JValue.ToString() + "分，共" + getTestPlan.JValue * getC + "分）";
                        }
                    }
                    sql = @"SELECT TestRecordItemId,TestRecordId,replace(replace(replace(replace(Abstracts,' ',''),'(','（'),')','）'),'（）',('（'+ISNULL(Replace(Replace(Replace(Replace(Replace(SelectedItem,'1','A'),'2', 'B'),'3', 'C'),'4', 'D'),'5', 'E'),'')+'）')) AS Abstracts,AttachUrl
                            ,('A. '+AItem) AS AItem,('B. '+BItem) AS BItem,(CASE WHEN CItem IS NULL OR CItem='' THEN '' ELSE  ('C. '+CItem)  END) AS CItem
                            ,(CASE WHEN DItem IS NULL OR DItem='' THEN NULL ELSE 'D. '+DItem END) AS DItem,(CASE WHEN EItem IS NULL  OR EItem='' THEN NULL ELSE 'E. '+EItem END) AS EItem  
                            ,('('+ISNULL(Replace(Replace(Replace(Replace(Replace(SelectedItem,'1','A'),'2', 'B'),'3', 'C'),'4', 'D'),'5', 'E'),'')+')') AS SelectedItem
                            ,TestType,TrainingItemCode
                             FROM Test_TestRecordItem WHERE TestRecordId= '" + this.TestRecordId + "'";
                }
                else
                {
                    var testRecord = TestRecordService.GetTestRecordById(this.TestRecordId);
                    if (testRecord != null)
                    {
                        var getTrainTypeName = (from x in new Model.SGGLDB(Funs.ConnString).Training_TestRecord
                                                join y in new Model.SGGLDB(Funs.ConnString).Training_TestPlan on x.TestPlanId equals y.TestPlanId
                                                join z in new Model.SGGLDB(Funs.ConnString).Training_Plan on y.PlanId equals z.PlanId
                                                join t in new Model.SGGLDB(Funs.ConnString).Base_TrainType on z.TrainTypeId equals t.TrainTypeId
                                                where x.TestRecordId == this.TestRecordId
                                                select t.TrainTypeName).FirstOrDefault();
                        if (getTrainTypeName != null)
                        {
                            this.lbTestType.Text = getTrainTypeName + this.lbTestType.Text;
                        }
                        string personInfo = string.Empty;
                        var testMan = PersonService.GetPersonByUserId(testRecord.TestManId, testRecord.ProjectId);
                        if (testMan != null)
                        {
                            personInfo = "单位：" + UnitService.GetUnitNameByUnitId(testMan.UnitId) + "        " +
                                "姓名：" + testMan.PersonName + "        " +
                                "身份证号码：" + testMan.IdentityCard + "        " +
                                "岗位：" + WorkPostService.getWorkPostNamesWorkPostIds(testMan.WorkPostId) + "        ";
                        }
                        //this.Namea += "时间：" + string.Format("{0:yyyy-MM-dd HH:mm}", testRecord.TestStartTime) + " 至 "+ string.Format("{0:yyyy-MM-dd HH:mm}", testRecord.TestEndTime)+"   ";
                        if (testRecord.TestScores.HasValue)
                        {
                            personInfo += "成绩：" + testRecord.TestScores.ToString();
                        }

                        this.lbTestPerson.Text = personInfo;
                        var attachFile = new Model.SGGLDB(Funs.ConnString).AttachFile.FirstOrDefault(x => x.ToKeyId == this.TestRecordId);
                        if (attachFile != null && !string.IsNullOrEmpty(attachFile.AttachUrl))
                        {
                            List<string> listUrl = Funs.GetStrListByStr(attachFile.AttachUrl, ',');
                            int count = listUrl.Count();
                            if (count > 0)
                            {
                                this.timg1.Src = "../" + listUrl[0];
                                this.timg2.Src = "../" + listUrl[0];
                                if (count >= 2)
                                {
                                    int cout2 = count / 2;
                                    this.timg2.Src = "../" + listUrl[cout2];
                                }
                                this.timg3.Src = "../" + listUrl[count - 1];
                            }
                        }

                        var getTestPlan = new Model.SGGLDB(Funs.ConnString).Training_TestPlan.FirstOrDefault(x => x.TestPlanId == testRecord.TestPlanId);
                        if (getTestPlan != null)
                        {
                            int getA = 0, getB = 0, getC = 0;
                            var getTraining = from x in new Model.SGGLDB(Funs.ConnString).Training_TestPlanTraining
                                              where x.TestPlanId == getTestPlan.TestPlanId
                                              select x;
                            if (getTraining.Count() > 0)
                            {
                                getA = getTraining.Sum(x => x.TestType1Count) ?? 0;
                                getB = getTraining.Sum(x => x.TestType2Count) ?? 0;
                                getC = getTraining.Sum(x => x.TestType3Count) ?? 0;
                            }

                            Namea += "（每题" + getTestPlan.SValue.ToString() + "分，共" + getTestPlan.SValue * getA + "分）";
                            Nameb += "（每题" + getTestPlan.MValue.ToString() + "分，共" + getTestPlan.MValue * getB + "分）";
                            Namec += "（每题" + getTestPlan.JValue.ToString() + "分，共" + getTestPlan.JValue * getC + "分）";
                        }
                    }
                    sql = @"SELECT TestRecordItemId,TestRecordId,TrainingItemName,replace(replace(replace(replace(Abstracts,' ',''),'(','（'),')','）'),'（）',('（'+ISNULL(Replace(Replace(Replace(Replace(Replace(SelectedItem,'1','A'),'2', 'B'),'3', 'C'),'4', 'D'),'5', 'E'),'')+'）')) AS Abstracts,AttachUrl
                            ,('A. '+AItem) AS AItem,('B. '+BItem) AS BItem,(CASE WHEN CItem IS NULL OR CItem='' THEN '' ELSE  ('C. '+CItem)  END) AS CItem
                            ,(CASE WHEN DItem IS NULL OR DItem='' THEN NULL ELSE 'D. '+DItem END) AS DItem,(CASE WHEN EItem IS NULL  OR EItem='' THEN NULL ELSE 'E. '+EItem END) AS EItem  
                            ,('('+ISNULL(Replace(Replace(Replace(Replace(Replace(SelectedItem,'1','A'),'2', 'B'),'3', 'C'),'4', 'D'),'5', 'E'),'')+')') AS SelectedItem
                            ,TestType,TrainingItemCode
                             FROM Training_TestRecordItem WHERE TestRecordId= '" + this.TestRecordId + "'";
                }
                this.BindGrid(sql);
                this.BindGrid2(sql);
                this.BindGrid3(sql);
            }
        }

        protected string Namea = "一、单项选择题  ";
        protected string Nameb = "二、多项选择题  ";
        protected string Namec = "三、判断题  ";

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid(string sql)
        {
            gvTest.DataSource = SQLHelper.GetDataTableRunText(sql + " AND TestType= '1' ORDER BY TrainingItemCode", null);
            gvTest.DataBind();           
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid2(string sql)
        {
            gvTest2.DataSource = SQLHelper.GetDataTableRunText(sql + " AND TestType= '2' ORDER BY TrainingItemCode", null); ;
            gvTest2.DataBind();
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid3(string sql)
        {
            gvTest3.DataSource = SQLHelper.GetDataTableRunText(sql + " AND TestType= '3' ORDER BY TrainingItemCode", null); ; ;
            gvTest3.DataBind();
        }
        /// <summary>
        /// 在控件被绑定后激发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTest_DataBound(object sender, EventArgs e)
        {          
        }
        /// <summary>
        /// 在控件被绑定后激发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTest2_DataBound(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// 在控件被绑定后激发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTest3_DataBound(object sender, EventArgs e)
        {
        }
    }
}