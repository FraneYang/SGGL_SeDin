using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 项目HSE数据汇总
    /// </summary>
    public static class HSEDataCollectService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取项目HSE数据汇总
        /// </summary>
        /// <param name="HSEDataCollectId"></param>
        /// <returns></returns>
        public static Model.DigData_HSEDataCollect GetHSEDataCollectById(string HSEDataCollectId)
        {
            return Funs.DB.DigData_HSEDataCollect.FirstOrDefault(e => e.HSEDataCollectId == HSEDataCollectId);
        }
        /// <summary>
        /// 根据年度获取项目HSE数据汇总
        /// </summary>
        /// <param name="HSEDataCollectId"></param>
        /// <returns></returns>
        public static Model.DigData_HSEDataCollect GetHSEDataCollectByYear(int Year)
        {
            return Funs.DB.DigData_HSEDataCollect.FirstOrDefault(e => e.Year == Year);
        }

        #region 创建项目HSE数据汇总
        /// <summary>
        /// 创建项目HSE数据汇总
        /// </summary>
        /// <param name="HSEDataCollectId"></param>
        /// <returns></returns>
        public static string CreateHSEDataCollectByYear(int year)
        {
            Model.SGGLDB db = Funs.DB;
            string returnHSEDataCollectId = SQLHelper.GetNewID();
            Model.DigData_HSEDataCollect newHSEDataCollect = new Model.DigData_HSEDataCollect
            {
                HSEDataCollectId = returnHSEDataCollectId,
                Year = year,
            };
            db.DigData_HSEDataCollect.InsertOnSubmit(newHSEDataCollect);
            db.SubmitChanges();
            ////新增明细
            var getSysConst = ConstValue.drpConstItemList(ConstValue.Group_HSEData);
            foreach (var item in getSysConst)
            {
                Model.DigData_HSEDataCollectItem newItem = new Model.DigData_HSEDataCollectItem()
                {
                    HSEDataCollectItemId = SQLHelper.GetNewID(),
                    HSEDataCollectId = returnHSEDataCollectId,
                    Year = year,
                    SortIndex = item.SortIndex,
                    HSEContent = item.ConstText,
                    MeasureUnit = item.Remark,
                };
                db.DigData_HSEDataCollectItem.InsertOnSubmit(newItem);
                db.SubmitChanges();
            }
            ////新增月报提交情况
            var getProject = from x in Funs.DB.Base_Project
                             where x.ProjectState == Const.ProjectState_1 || x.ProjectState == null
                             select x;
            foreach (var item in getProject)
            {
                Model.DigData_HSEDataCollectSubmission newSubmission = new Model.DigData_HSEDataCollectSubmission()
                {
                    HSEDataCollectSubmissionId = SQLHelper.GetNewID(),
                    HSEDataCollectId = returnHSEDataCollectId,
                    Year = year,
                    ProjectId = item.ProjectId,
                };

                db.DigData_HSEDataCollectSubmission.InsertOnSubmit(newSubmission);
                db.SubmitChanges();
            }
            return returnHSEDataCollectId;
        }
        #endregion

        /// <summary>
        ///   根据项目信息
        /// </summary>
        public static void ProjectHSEDataCollectSubmission(Model.Base_Project project)
        {
            if (project.ProjectState == Const.ProjectState_3)
            {
                #region 项目关闭
                DateTime nowD = DateTime.Now.AddMonths(1);
                var getSubmission = Funs.DB.DigData_HSEDataCollectSubmission.FirstOrDefault(x => x.ProjectId == project.ProjectId && x.Year == nowD.Year);
                if (getSubmission != null)
                {
                    if (nowD.Month == 1)
                    {
                        getSubmission.Month1 = "已关闭";
                    }
                    else if (nowD.Month == 2)
                    {
                        getSubmission.Month2 = "已关闭";
                    }
                    else if (nowD.Month == 3)
                    {
                        getSubmission.Month3 = "已关闭";
                    }
                    else if (nowD.Month == 4)
                    {
                        getSubmission.Month4 = "已关闭";
                    }
                    else if (nowD.Month == 5)
                    {
                        getSubmission.Month5 = "已关闭";
                    }
                    else if (nowD.Month == 6)
                    {
                        getSubmission.Month6 = "已关闭";
                    }
                    else if (nowD.Month == 7)
                    {
                        getSubmission.Month7 = "已关闭";
                    }
                    else if (nowD.Month == 8)
                    {
                        getSubmission.Month8 = "已关闭";
                    }
                    else if (nowD.Month == 9)
                    {
                        getSubmission.Month9 = "已关闭";
                    }
                    else if (nowD.Month == 10)
                    {
                        getSubmission.Month10 = "已关闭";
                    }
                    else if (nowD.Month == 11)
                    {
                        getSubmission.Month11 = "已关闭";
                    }
                    else if (nowD.Month == 12)
                    {
                        getSubmission.Month12 = "已关闭";
                    }
                    Funs.DB.SubmitChanges();
                }
                #endregion
            }
            else
            {
                #region 项目新增
                DateTime nowD = DateTime.Now;
                string getHSEDataCollectId = string.Empty;
                var getHSEDataCollect = GetHSEDataCollectByYear(nowD.Year);
                if (getHSEDataCollect != null)
                {
                    getHSEDataCollectId = getHSEDataCollect.HSEDataCollectId;
                }
                else
                {
                    getHSEDataCollectId = CreateHSEDataCollectByYear(nowD.Year);
                }
                var getSubmission = Funs.DB.DigData_HSEDataCollectSubmission.FirstOrDefault(x => x.HSEDataCollectId == getHSEDataCollectId && x.ProjectId == project.ProjectId);
                if (getSubmission == null)
                {
                    Model.DigData_HSEDataCollectSubmission newSubmission = new Model.DigData_HSEDataCollectSubmission()
                    {
                        HSEDataCollectSubmissionId = SQLHelper.GetNewID(),
                        HSEDataCollectId = getHSEDataCollectId,
                        Year = nowD.Year,
                        ProjectId = project.ProjectId,
                    };

                    db.DigData_HSEDataCollectSubmission.InsertOnSubmit(newSubmission);
                    db.SubmitChanges();
                }
                #endregion
            }
        }

        #region 根据月报信息汇总数据
        /// <summary>
        ///  根据月报信息汇总数据
        /// </summary>
        public static void SaveHSEDataCollectItem(Model.SeDin_MonthReport monthReport)
        {
            Model.SGGLDB db = Funs.DB;
            string getHSEDataCollectId = string.Empty;
            var getHSEDataCollect = GetHSEDataCollectByYear(monthReport.ReporMonth.Value.Year);
            if (getHSEDataCollect != null)
            {
                getHSEDataCollectId = getHSEDataCollect.HSEDataCollectId;
            }
            else
            {
                getHSEDataCollectId = CreateHSEDataCollectByYear(monthReport.ReporMonth.Value.Year);
            }

            var getHSEDataCollectItemYear = from x in db.DigData_HSEDataCollectItem
                                            where x.HSEDataCollectId == getHSEDataCollectId
                                            select x;
            var getHSEDataCollectISubmissionYear = from x in db.DigData_HSEDataCollectSubmission
                                                   where x.HSEDataCollectId == getHSEDataCollectId
                                                   select x;
            ////一月份
            if (monthReport.ReporMonth.Value.Month == 1)
            {
                #region 提交情况
                bool isSave = true;
                var getSubmissionMonth = getHSEDataCollectISubmissionYear.FirstOrDefault(x => x.ProjectId == monthReport.ProjectId);
                if (getSubmissionMonth == null)
                {
                    Model.DigData_HSEDataCollectSubmission newSubmission = new Model.DigData_HSEDataCollectSubmission()
                    {
                        HSEDataCollectSubmissionId = SQLHelper.GetNewID(),
                        HSEDataCollectId = getHSEDataCollectId,
                        Year = monthReport.ReporMonth.Value.Year,
                        ProjectId = monthReport.ProjectId,
                        Month1 = string.Format("{0:yyyy-MM-dd}", (monthReport.CommitTime ?? DateTime.Now)),
                    };
                    db.DigData_HSEDataCollectSubmission.InsertOnSubmit(newSubmission);
                    db.SubmitChanges();
                }
                else
                {
                    if (!string.IsNullOrEmpty(getSubmissionMonth.Month1))
                    {
                        isSave = false;
                    }
                    else
                    {
                        getSubmissionMonth.Month1 = string.Format("{0:yyyy-MM-dd}", (monthReport.CommitTime ?? DateTime.Now));
                        db.SubmitChanges();
                    }
                }
                #endregion
                #region 一月份 数据汇总
                if (isSave)
                {
                    ////员工
                    var getSeDin_MonthReport4Other = db.SeDin_MonthReport4Other.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport4Other != null)
                    {
                        var getItem1 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 1);
                        if (getItem1 != null)
                        {
                            getItem1.Month1 = (Funs.GetNewIntOrZero(getItem1.Month1) + getSeDin_MonthReport4Other.TotalNum ?? 0).ToString();
                        }
                        var getItem2 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 2);
                        if (getItem2 != null)
                        {
                            getItem2.Month1 = (Funs.GetNewIntOrZero(getItem2.Month1) + getSeDin_MonthReport4Other.OutsideNum ?? 0).ToString();                            
                        }
                        var getItem3 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 3);
                        if (getItem3 != null)
                        {
                            getItem3.Month1 =(Funs.GetNewIntOrZero( getItem3.Month1) + getSeDin_MonthReport4Other.ForeignNum ?? 0).ToString();
                        }
                        var getItem4 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 4);
                        if (getItem4 != null)
                        {
                            getItem4.Month1 = (Funs.GetNewIntOrZero(getItem4.Month1) + getSeDin_MonthReport4Other.ManagerNum ?? 0).ToString();
                        }
                    }
                    ////分包人数
                    var getSeDin_MonthReport4 = from x in db.SeDin_MonthReport4
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport4.Count() > 0)
                    {
                        var getItem5 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 5);
                        if (getItem5 != null)
                        {
                            getItem5.Month1 = (Funs.GetNewIntOrZero(getItem5.Month1) + getSeDin_MonthReport4.Sum(x => x.TotalNum ?? 0)).ToString();
                        }
                        var getItem6 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 6);
                        if (getItem6 != null)
                        {
                            getItem6.Month1 = (Funs.GetNewIntOrZero(getItem6.Month1) +getSeDin_MonthReport4.Sum(x => x.SafeManangerNum ?? 0)).ToString();
                        }
                    }
                    ///安全人工时
                    var getSeDin_MonthReport2 = db.SeDin_MonthReport2.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport2 != null)
                    {
                        var getItem7 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 7);
                        if (getItem7 != null)
                        {
                            getItem7.Month1 = (Funs.GetNewDecimalOrZero(getItem7.Month1) + getSeDin_MonthReport2.MonthWorkTime ?? 0).ToString();
                        }
                    }
                    ////事故
                    var getSeDin_MonthReport3 = from x in db.SeDin_MonthReport3
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport3.Count() > 0)
                    {
                        var getItem8 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 8);
                        if (getItem8 != null)
                        {
                            var r1 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 1);
                            if (r1 != null)
                            {
                                getItem8.Month1 = (Funs.GetNewIntOrZero(getItem8.Month1) + r1.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem9 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 9);
                        if (getItem9 != null)
                        {
                            var r2 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 2);
                            if (r2 != null)
                            {
                                getItem9.Month1 = (Funs.GetNewIntOrZero(getItem9.Month1) + r2.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem10 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 10);
                        if (getItem10 != null)
                        {
                            var r3 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 3);
                            if (r3 != null)
                            {
                                getItem10.Month1 =(Funs.GetNewIntOrZero(getItem10.Month1)+ r3.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem11 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 11);
                        if (getItem11 != null)
                        {
                            var r4 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 4);
                            if (r4 != null)
                            {
                                getItem11.Month1 = (Funs.GetNewIntOrZero(getItem11.Month1) + r4.MonthPersons ?? 0).ToString();
                            }
                        }

                        var getItem12 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 12);
                        if (getItem12 != null)
                        {
                            var r5 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 5);
                            if (r5 != null)
                            {
                                getItem12.Month1 = (Funs.GetNewIntOrZero(getItem12.Month1)+ r5.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem13 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 13);
                        if (getItem13 != null)
                        {
                            var r6 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 6);
                            if (r6 != null)
                            {
                                getItem13.Month1 = (Funs.GetNewIntOrZero(getItem13.Month1) + r6.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem14 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 14);
                        if (getItem14 != null)
                        {
                            var r7 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 7);
                            if (r7 != null)
                            {
                                getItem14.Month1 = (Funs.GetNewIntOrZero(getItem14.Month1) + r7.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem15 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 15);
                        if (getItem15 != null)
                        {
                            var r8 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 8);
                            if (r8 != null)
                            {
                                getItem15.Month1 = (Funs.GetNewIntOrZero(getItem15.Month1) + r8.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem16 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 16);
                        if (getItem16 != null)
                        {
                            var r9 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 9);
                            if (r9 != null)
                            {
                                getItem16.Month1 = (Funs.GetNewIntOrZero(getItem16.Month1) + r9.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem17 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 17);
                        if (getItem17 != null)
                        {
                            var r10 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 10);
                            if (r10 != null)
                            {
                                getItem17.Month1 = (Funs.GetNewIntOrZero(getItem17.Month1) + r10.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem18 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 18);
                        if (getItem18 != null)
                        {
                            var r11 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 11);
                            if (r11 != null)
                            {
                                getItem18.Month1 = (Funs.GetNewIntOrZero(getItem18.Month1) + r11.MonthTimes ?? 0).ToString();
                            }
                        }
                    }
                    ////大型、特种设备
                    var getSeDin_MonthReport5 = from x in db.SeDin_MonthReport5 where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    var getItem19 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 19);
                    if (getSeDin_MonthReport5.Count() > 0 && getItem19 != null)
                    {
                        int countSumR5 = getSeDin_MonthReport5.Sum(x => x.T01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.T04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T05 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T06 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.S01 ?? 0);
                        getItem19.Month1 = (Funs.GetNewIntOrZero(getItem19.Month1) + countSumR5).ToString();
                    }
                    ////安全生产费用
                    var getSeDin_MonthReport6 = db.SeDin_MonthReport6.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport6 != null)
                    {
                        var getItem20 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 20);
                        if (getItem20 != null)
                        {
                            getItem20.Month1 = (Funs.GetNewDecimalOrZero(getItem20.Month1) + getSeDin_MonthReport6.SumMonth ?? 0).ToString();
                        }
                    }
                    ////项目现场员工入场安全培训人数
                    var getSeDin_MonthReport7 = db.SeDin_MonthReport7.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport7 != null)
                    {
                        var getItem21 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 21);
                        if (getItem21 != null)
                        {
                            getItem21.Month1 = (Funs.GetNewIntOrZero(getItem21.Month1) + getSeDin_MonthReport7.EmployeeMontPerson ?? 0).ToString();
                        }
                    }
                    ////项目会次
                    var getSeDin_MonthReport8 = db.SeDin_MonthReport8.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport8 != null)
                    {
                        var getItem22 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 22);
                        if (getItem22 != null)
                        {
                            getItem22.Month1 = (Funs.GetNewIntOrZero(getItem22.Month1) +  getSeDin_MonthReport8.MonthMontNum ?? 0).ToString();
                        }
                        var getItem23 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 23);
                        if (getItem23 != null)
                        {
                            getItem23.Month1 = (Funs.GetNewIntOrZero(getItem23.Month1) +  getSeDin_MonthReport8.WeekMontNum ?? 0).ToString();
                        }
                        var getItem24 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 24);
                        if (getItem24 != null)
                        {
                            getItem24.Month1 = (Funs.GetNewIntOrZero(getItem24.Month1) +  getSeDin_MonthReport8.SpecialMontPerson ?? 0).ToString();
                        }
                    }
                    /// 安全检查
                    var getSeDin_MonthReport9 = db.SeDin_MonthReport9.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport9 != null)
                    {
                        var getItem25 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 25);
                        if (getItem25 != null)
                        {
                            getItem25.Month1 = (Funs.GetNewIntOrZero(getItem25.Month1) +  getSeDin_MonthReport9.DailyMonth ?? 0).ToString();
                        }
                        var getItem26 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 26);
                        if (getItem26 != null)
                        {
                            getItem26.Month1 = (Funs.GetNewIntOrZero(getItem26.Month1) +  getSeDin_MonthReport9.WeekMonth ?? 0).ToString();
                        }
                        var getItem27 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 27);
                        if (getItem27 != null)
                        {
                            getItem27.Month1 = (Funs.GetNewIntOrZero(getItem27.Month1) +  getSeDin_MonthReport9.SpecialMonth ?? 0).ToString();
                        }
                        var getItem28 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 28);
                        if (getItem28 != null)
                        {
                            getItem28.Month1 = (Funs.GetNewIntOrZero(getItem28.Month1) +  getSeDin_MonthReport9.MonthlyMonth ?? 0).ToString();
                        }
                    }
                    /////隐患整改单
                    var getSeDin_MonthReport9Rectification = from x in db.SeDin_MonthReport9Item_Rectification where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Rectification.Count() > 0)
                    {
                        var getItem29 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 29);
                        if (getItem29 != null)
                        {
                            getItem29.Month1 = (Funs.GetNewIntOrZero(getItem29.Month1) +  getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                        var getItem30 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 30);
                        if (getItem30 != null)
                        {
                            getItem30.Month1 = (Funs.GetNewIntOrZero(getItem30.Month1) +  getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMoth ?? 0)).ToString();
                        }
                        var getItem31 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 31);
                        if (getItem31 != null)
                        {
                            getItem31.Month1 = (Funs.GetNewIntOrZero(getItem31.Month1) +  getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthLarge ?? 0)).ToString();
                        }
                        var getItem32 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 32);
                        if (getItem32 != null)
                        {
                            getItem32.Month1 = (Funs.GetNewIntOrZero(getItem32.Month1) +  getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothLarge ?? 0)).ToString();
                        }
                        var getItem33 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 33);
                        if (getItem33 != null)
                        {
                            getItem33.Month1 = (Funs.GetNewIntOrZero(getItem33.Month1) +  getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthSerious ?? 0)).ToString();
                        }
                        var getItem34 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 34);
                        if (getItem34 != null)
                        {
                            getItem34.Month1 = (Funs.GetNewIntOrZero(getItem34.Month1) +  getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothSerious ?? 0)).ToString();
                        }
                    }
                    /////停工令
                    var getSeDin_MonthReport9Stoppage = from x in db.SeDin_MonthReport9Item_Stoppage where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Stoppage.Count() > 0)
                    {
                        var getItem35 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 35);
                        if (getItem35 != null)
                        {
                            getItem35.Month1 = (Funs.GetNewIntOrZero(getItem35.Month1) +  getSeDin_MonthReport9Stoppage.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                    }
                    ////奖惩
                    var getSeDin_MonthReport10 = db.SeDin_MonthReport10.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport10 != null)
                    {
                        var getItem36 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 36);
                        if (getItem36 != null)
                        {
                            getItem36.Month1 = (Funs.GetNewDecimalOrZero(getItem36.Month1) +  getSeDin_MonthReport10.SafeMonthMoney ?? 0 + getSeDin_MonthReport10.HseMonthMoney ?? 0 + getSeDin_MonthReport10.ProduceMonthMoney ?? 0).ToString();
                        }
                        var getItem37 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 37);
                        if (getItem37 != null)
                        {
                            getItem37.Month1 = (Funs.GetNewDecimalOrZero(getItem37.Month1) +  getSeDin_MonthReport10.AccidentMonthMoney ?? 0 + getSeDin_MonthReport10.ViolationMonthMoney ?? 0 + getSeDin_MonthReport10.ManageMonthMoney ?? 0).ToString();
                        }
                    }
                    ////危大工程
                    var getSeDin_MonthReport11 = db.SeDin_MonthReport11.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport11 != null)
                    {
                        var getItem38 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 38);
                        if (getItem38 != null)
                        {
                            getItem38.Month1 = (Funs.GetNewIntOrZero(getItem38.Month1) +  getSeDin_MonthReport11.RiskWorkNum ?? 0).ToString();
                        }
                        var getItem39 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 39);
                        if (getItem39 != null)
                        {
                            getItem39.Month1 = (Funs.GetNewIntOrZero(getItem39.Month1) +  getSeDin_MonthReport11.LargeWorkNum ?? 0).ToString();
                        }
                    }
                    ////应急
                    var getSeDin_MonthReport12 = db.SeDin_MonthReport12.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport12 != null)
                    {
                        var getItem40 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 40);
                        if (getItem40 != null)
                        {
                            getItem40.Month1 = (Funs.GetNewIntOrZero(getItem40.Month1) +  getSeDin_MonthReport12.MultipleSiteNum ?? 0 + getSeDin_MonthReport12.MultipleDesktopNum ?? 0
                                + getSeDin_MonthReport12.SingleSiteNum ?? 0 + getSeDin_MonthReport12.SingleDesktopNum ?? 0).ToString();
                        }
                    }
                    db.SubmitChanges();
                }
                #endregion
            }
            else  if (monthReport.ReporMonth.Value.Month == 2)
            {
                #region 提交情况
                bool isSave = true;
                var getSubmissionMonth = getHSEDataCollectISubmissionYear.FirstOrDefault(x => x.ProjectId == monthReport.ProjectId);
                if (getSubmissionMonth == null)
                {
                    Model.DigData_HSEDataCollectSubmission newSubmission = new Model.DigData_HSEDataCollectSubmission()
                    {
                        HSEDataCollectSubmissionId = SQLHelper.GetNewID(),
                        HSEDataCollectId = getHSEDataCollectId,
                        Year = monthReport.ReporMonth.Value.Year,
                        ProjectId = monthReport.ProjectId,
                        Month2 = string.Format("{0:yyyy-MM-dd}", (monthReport.CommitTime ?? DateTime.Now)),
                    };
                    db.DigData_HSEDataCollectSubmission.InsertOnSubmit(newSubmission);
                    db.SubmitChanges();
                }
                else
                {
                    if (!string.IsNullOrEmpty(getSubmissionMonth.Month2))
                    {
                        isSave = false;
                    }
                    else
                    {
                        getSubmissionMonth.Month2 = string.Format("{0:yyyy-MM-dd}", (monthReport.CommitTime ?? DateTime.Now));
                        db.SubmitChanges();
                    }
                }
                #endregion
                #region 二月份 数据汇总
                if (isSave)
                {
                    ////员工
                    var getSeDin_MonthReport4Other = db.SeDin_MonthReport4Other.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport4Other != null)
                    {
                        var getItem1 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 1);
                        if (getItem1 != null)
                        {
                            getItem1.Month2 = (Funs.GetNewIntOrZero(getItem1.Month2) + getSeDin_MonthReport4Other.TotalNum ?? 0).ToString();
                        }
                        var getItem2 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 2);
                        if (getItem2 != null)
                        {
                            getItem2.Month2 = (Funs.GetNewIntOrZero(getItem2.Month2) + getSeDin_MonthReport4Other.OutsideNum ?? 0).ToString();
                        }
                        var getItem3 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 3);
                        if (getItem3 != null)
                        {
                            getItem3.Month2 = (Funs.GetNewIntOrZero(getItem3.Month2) + getSeDin_MonthReport4Other.ForeignNum ?? 0).ToString();
                        }
                        var getItem4 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 4);
                        if (getItem4 != null)
                        {
                            getItem4.Month2 = (Funs.GetNewIntOrZero(getItem4.Month2) + getSeDin_MonthReport4Other.ManagerNum ?? 0).ToString();
                        }
                    }
                    ////分包人数
                    var getSeDin_MonthReport4 = from x in db.SeDin_MonthReport4
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport4.Count() > 0)
                    {
                        var getItem5 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 5);
                        if (getItem5 != null)
                        {
                            getItem5.Month2 = (Funs.GetNewIntOrZero(getItem5.Month2) + getSeDin_MonthReport4.Sum(x => x.TotalNum ?? 0)).ToString();
                        }
                        var getItem6 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 6);
                        if (getItem6 != null)
                        {
                            getItem6.Month2 = (Funs.GetNewIntOrZero(getItem6.Month2) + getSeDin_MonthReport4.Sum(x => x.SafeManangerNum ?? 0)).ToString();
                        }
                    }
                    ///安全人工时
                    var getSeDin_MonthReport2 = db.SeDin_MonthReport2.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport2 != null)
                    {
                        var getItem7 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 7);
                        if (getItem7 != null)
                        {
                            getItem7.Month2 = (Funs.GetNewDecimalOrZero(getItem7.Month2) + getSeDin_MonthReport2.MonthWorkTime ?? 0).ToString();
                        }
                    }
                    ////事故
                    var getSeDin_MonthReport3 = from x in db.SeDin_MonthReport3
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport3.Count() > 0)
                    {
                        var getItem8 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 8);
                        if (getItem8 != null)
                        {
                            var r1 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 1);
                            if (r1 != null)
                            {
                                getItem8.Month2 = (Funs.GetNewIntOrZero(getItem8.Month2) + r1.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem9 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 9);
                        if (getItem9 != null)
                        {
                            var r2 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 2);
                            if (r2 != null)
                            {
                                getItem9.Month2 = (Funs.GetNewIntOrZero(getItem9.Month2) + r2.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem10 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 10);
                        if (getItem10 != null)
                        {
                            var r3 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 3);
                            if (r3 != null)
                            {
                                getItem10.Month2 = (Funs.GetNewIntOrZero(getItem10.Month2) + r3.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem11 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 11);
                        if (getItem11 != null)
                        {
                            var r4 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 4);
                            if (r4 != null)
                            {
                                getItem11.Month2 = (Funs.GetNewIntOrZero(getItem11.Month2) + r4.MonthPersons ?? 0).ToString();
                            }
                        }

                        var getItem12 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 12);
                        if (getItem12 != null)
                        {
                            var r5 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 5);
                            if (r5 != null)
                            {
                                getItem12.Month2 = (Funs.GetNewIntOrZero(getItem12.Month2) + r5.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem13 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 13);
                        if (getItem13 != null)
                        {
                            var r6 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 6);
                            if (r6 != null)
                            {
                                getItem13.Month2 = (Funs.GetNewIntOrZero(getItem13.Month2) + r6.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem14 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 14);
                        if (getItem14 != null)
                        {
                            var r7 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 7);
                            if (r7 != null)
                            {
                                getItem14.Month2 = (Funs.GetNewIntOrZero(getItem14.Month2) + r7.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem15 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 15);
                        if (getItem15 != null)
                        {
                            var r8 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 8);
                            if (r8 != null)
                            {
                                getItem15.Month2 = (Funs.GetNewIntOrZero(getItem15.Month2) + r8.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem16 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 16);
                        if (getItem16 != null)
                        {
                            var r9 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 9);
                            if (r9 != null)
                            {
                                getItem16.Month2 = (Funs.GetNewIntOrZero(getItem16.Month2) + r9.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem17 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 17);
                        if (getItem17 != null)
                        {
                            var r10 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 10);
                            if (r10 != null)
                            {
                                getItem17.Month2 = (Funs.GetNewIntOrZero(getItem17.Month2) + r10.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem18 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 18);
                        if (getItem18 != null)
                        {
                            var r11 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 11);
                            if (r11 != null)
                            {
                                getItem18.Month2 = (Funs.GetNewIntOrZero(getItem18.Month2) + r11.MonthTimes ?? 0).ToString();
                            }
                        }
                    }
                    ////大型、特种设备
                    var getSeDin_MonthReport5 = from x in db.SeDin_MonthReport5
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    var getItem19 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 19);
                    if (getSeDin_MonthReport5.Count() > 0 && getItem19 != null)
                    {
                        int countSumR5 = getSeDin_MonthReport5.Sum(x => x.T01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.T04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T05 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T06 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.S01 ?? 0);
                        getItem19.Month2 = (Funs.GetNewIntOrZero(getItem19.Month2) + countSumR5).ToString();
                    }
                    ////安全生产费用
                    var getSeDin_MonthReport6 = db.SeDin_MonthReport6.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport6 != null)
                    {
                        var getItem20 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 20);
                        if (getItem20 != null)
                        {
                            getItem20.Month2 = (Funs.GetNewDecimalOrZero(getItem20.Month2) + getSeDin_MonthReport6.SumMonth ?? 0).ToString();
                        }
                    }
                    ////项目现场员工入场安全培训人数
                    var getSeDin_MonthReport7 = db.SeDin_MonthReport7.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport7 != null)
                    {
                        var getItem21 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 21);
                        if (getItem21 != null)
                        {
                            getItem21.Month2 = (Funs.GetNewIntOrZero(getItem21.Month2) + getSeDin_MonthReport7.EmployeeMontPerson ?? 0).ToString();
                        }
                    }
                    ////项目会次
                    var getSeDin_MonthReport8 = db.SeDin_MonthReport8.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport8 != null)
                    {
                        var getItem22 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 22);
                        if (getItem22 != null)
                        {
                            getItem22.Month2 = (Funs.GetNewIntOrZero(getItem22.Month2) + getSeDin_MonthReport8.MonthMontNum ?? 0).ToString();
                        }
                        var getItem23 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 23);
                        if (getItem23 != null)
                        {
                            getItem23.Month2 = (Funs.GetNewIntOrZero(getItem23.Month2) + getSeDin_MonthReport8.WeekMontNum ?? 0).ToString();
                        }
                        var getItem24 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 24);
                        if (getItem24 != null)
                        {
                            getItem24.Month2 = (Funs.GetNewIntOrZero(getItem24.Month2) + getSeDin_MonthReport8.SpecialMontPerson ?? 0).ToString();
                        }
                    }
                    /// 安全检查
                    var getSeDin_MonthReport9 = db.SeDin_MonthReport9.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport9 != null)
                    {
                        var getItem25 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 25);
                        if (getItem25 != null)
                        {
                            getItem25.Month2 = (Funs.GetNewIntOrZero(getItem25.Month2) + getSeDin_MonthReport9.DailyMonth ?? 0).ToString();
                        }
                        var getItem26 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 26);
                        if (getItem26 != null)
                        {
                            getItem26.Month2 = (Funs.GetNewIntOrZero(getItem26.Month2) + getSeDin_MonthReport9.WeekMonth ?? 0).ToString();
                        }
                        var getItem27 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 27);
                        if (getItem27 != null)
                        {
                            getItem27.Month2 = (Funs.GetNewIntOrZero(getItem27.Month2) + getSeDin_MonthReport9.SpecialMonth ?? 0).ToString();
                        }
                        var getItem28 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 28);
                        if (getItem28 != null)
                        {
                            getItem28.Month2 = (Funs.GetNewIntOrZero(getItem28.Month2) + getSeDin_MonthReport9.MonthlyMonth ?? 0).ToString();
                        }
                    }
                    /////隐患整改单
                    var getSeDin_MonthReport9Rectification = from x in db.SeDin_MonthReport9Item_Rectification where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Rectification.Count() > 0)
                    {
                        var getItem29 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 29);
                        if (getItem29 != null)
                        {
                            getItem29.Month2 = (Funs.GetNewIntOrZero(getItem29.Month2) + getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                        var getItem30 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 30);
                        if (getItem30 != null)
                        {
                            getItem30.Month2 = (Funs.GetNewIntOrZero(getItem30.Month2) + getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMoth ?? 0)).ToString();
                        }
                        var getItem31 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 31);
                        if (getItem31 != null)
                        {
                            getItem31.Month2 = (Funs.GetNewIntOrZero(getItem31.Month2) + getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthLarge ?? 0)).ToString();
                        }
                        var getItem32 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 32);
                        if (getItem32 != null)
                        {
                            getItem32.Month2 = (Funs.GetNewIntOrZero(getItem32.Month2) + getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothLarge ?? 0)).ToString();
                        }
                        var getItem33 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 33);
                        if (getItem33 != null)
                        {
                            getItem33.Month2 = (Funs.GetNewIntOrZero(getItem33.Month2) + getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthSerious ?? 0)).ToString();
                        }
                        var getItem34 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 34);
                        if (getItem34 != null)
                        {
                            getItem34.Month2 = (Funs.GetNewIntOrZero(getItem34.Month2) + getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothSerious ?? 0)).ToString();
                        }
                    }
                    /////停工令
                    var getSeDin_MonthReport9Stoppage = from x in db.SeDin_MonthReport9Item_Stoppage where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Stoppage.Count() > 0)
                    {
                        var getItem35 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 35);
                        if (getItem35 != null)
                        {
                            getItem35.Month2 = (Funs.GetNewIntOrZero(getItem35.Month2) + getSeDin_MonthReport9Stoppage.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                    }
                    ////奖惩
                    var getSeDin_MonthReport10 = db.SeDin_MonthReport10.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport10 != null)
                    {
                        var getItem36 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 36);
                        if (getItem36 != null)
                        {
                            getItem36.Month2 = (Funs.GetNewDecimalOrZero(getItem36.Month2) + getSeDin_MonthReport10.SafeMonthMoney ?? 0 + getSeDin_MonthReport10.HseMonthMoney ?? 0 + getSeDin_MonthReport10.ProduceMonthMoney ?? 0).ToString();
                        }
                        var getItem37 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 37);
                        if (getItem37 != null)
                        {
                            getItem37.Month2 = (Funs.GetNewDecimalOrZero(getItem37.Month2) + getSeDin_MonthReport10.AccidentMonthMoney ?? 0 + getSeDin_MonthReport10.ViolationMonthMoney ?? 0 + getSeDin_MonthReport10.ManageMonthMoney ?? 0).ToString();
                        }
                    }
                    ////危大工程
                    var getSeDin_MonthReport11 = db.SeDin_MonthReport11.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport11 != null)
                    {
                        var getItem38 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 38);
                        if (getItem38 != null)
                        {
                            getItem38.Month2 = (Funs.GetNewIntOrZero(getItem38.Month2) + getSeDin_MonthReport11.RiskWorkNum ?? 0).ToString();
                        }
                        var getItem39 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 39);
                        if (getItem39 != null)
                        {
                            getItem39.Month2 = (Funs.GetNewIntOrZero(getItem39.Month2) + getSeDin_MonthReport11.LargeWorkNum ?? 0).ToString();
                        }
                    }
                    ////应急
                    var getSeDin_MonthReport12 = db.SeDin_MonthReport12.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport12 != null)
                    {
                        var getItem40 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 40);
                        if (getItem40 != null)
                        {
                            getItem40.Month2 = (Funs.GetNewIntOrZero(getItem40.Month2) + getSeDin_MonthReport12.MultipleSiteNum ?? 0 + getSeDin_MonthReport12.MultipleDesktopNum ?? 0
                                + getSeDin_MonthReport12.SingleSiteNum ?? 0 + getSeDin_MonthReport12.SingleDesktopNum ?? 0).ToString();
                        }
                    }
                    db.SubmitChanges();
                }
                #endregion
            }
            else if (monthReport.ReporMonth.Value.Month == 3)
            {
                #region 提交情况
                bool isSave = true;
                var getSubmissionMonth = getHSEDataCollectISubmissionYear.FirstOrDefault(x => x.ProjectId == monthReport.ProjectId);
                if (getSubmissionMonth == null)
                {
                    Model.DigData_HSEDataCollectSubmission newSubmission = new Model.DigData_HSEDataCollectSubmission()
                    {
                        HSEDataCollectSubmissionId = SQLHelper.GetNewID(),
                        HSEDataCollectId = getHSEDataCollectId,
                        Year = monthReport.ReporMonth.Value.Year,
                        ProjectId = monthReport.ProjectId,
                        Month3 = string.Format("{0:yyyy-MM-dd}", (monthReport.CommitTime ?? DateTime.Now)),
                    };
                    db.DigData_HSEDataCollectSubmission.InsertOnSubmit(newSubmission);
                    db.SubmitChanges();
                }
                else
                {
                    if (!string.IsNullOrEmpty(getSubmissionMonth.Month3))
                    {
                        isSave = false;
                    }
                    else
                    {
                        getSubmissionMonth.Month3 = string.Format("{0:yyyy-MM-dd}", (monthReport.CommitTime ?? DateTime.Now));
                        db.SubmitChanges();
                    }
                }
                #endregion
                #region 三月份 数据汇总
                if (isSave)
                {
                    ////员工
                    var getSeDin_MonthReport4Other = db.SeDin_MonthReport4Other.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport4Other != null)
                    {
                        var getItem1 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 1);
                        if (getItem1 != null)
                        {
                            getItem1.Month3 = (Funs.GetNewIntOrZero(getItem1.Month3) + getSeDin_MonthReport4Other.TotalNum ?? 0).ToString();
                        }
                        var getItem2 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 2);
                        if (getItem2 != null)
                        {
                            getItem2.Month3 = (Funs.GetNewIntOrZero(getItem2.Month3) + getSeDin_MonthReport4Other.OutsideNum ?? 0).ToString();
                        }
                        var getItem3 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 3);
                        if (getItem3 != null)
                        {
                            getItem3.Month3 = (Funs.GetNewIntOrZero(getItem3.Month3) + getSeDin_MonthReport4Other.ForeignNum ?? 0).ToString();
                        }
                        var getItem4 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 4);
                        if (getItem4 != null)
                        {
                            getItem4.Month3 = (Funs.GetNewIntOrZero(getItem4.Month3) + getSeDin_MonthReport4Other.ManagerNum ?? 0).ToString();
                        }
                    }
                    ////分包人数
                    var getSeDin_MonthReport4 = from x in db.SeDin_MonthReport4
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport4.Count() > 0)
                    {
                        var getItem5 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 5);
                        if (getItem5 != null)
                        {
                            getItem5.Month3 = (Funs.GetNewIntOrZero(getItem5.Month3) + getSeDin_MonthReport4.Sum(x => x.TotalNum ?? 0)).ToString();
                        }
                        var getItem6 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 6);
                        if (getItem6 != null)
                        {
                            getItem6.Month3 = (Funs.GetNewIntOrZero(getItem6.Month3) + getSeDin_MonthReport4.Sum(x => x.SafeManangerNum ?? 0)).ToString();
                        }
                    }
                    ///安全人工时
                    var getSeDin_MonthReport2 = db.SeDin_MonthReport2.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport2 != null)
                    {
                        var getItem7 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 7);
                        if (getItem7 != null)
                        {
                            getItem7.Month3 = (Funs.GetNewDecimalOrZero(getItem7.Month3) + getSeDin_MonthReport2.MonthWorkTime ?? 0).ToString();
                        }
                    }
                    ////事故
                    var getSeDin_MonthReport3 = from x in db.SeDin_MonthReport3
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport3.Count() > 0)
                    {
                        var getItem8 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 8);
                        if (getItem8 != null)
                        {
                            var r1 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 1);
                            if (r1 != null)
                            {
                                getItem8.Month3 = (Funs.GetNewIntOrZero(getItem8.Month3) + r1.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem9 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 9);
                        if (getItem9 != null)
                        {
                            var r2 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 2);
                            if (r2 != null)
                            {
                                getItem9.Month3 = (Funs.GetNewIntOrZero(getItem9.Month3) + r2.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem10 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 10);
                        if (getItem10 != null)
                        {
                            var r3 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 3);
                            if (r3 != null)
                            {
                                getItem10.Month3 = (Funs.GetNewIntOrZero(getItem10.Month3) + r3.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem11 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 11);
                        if (getItem11 != null)
                        {
                            var r4 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 4);
                            if (r4 != null)
                            {
                                getItem11.Month3 = (Funs.GetNewIntOrZero(getItem11.Month3) + r4.MonthPersons ?? 0).ToString();
                            }
                        }

                        var getItem12 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 12);
                        if (getItem12 != null)
                        {
                            var r5 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 5);
                            if (r5 != null)
                            {
                                getItem12.Month3 = (Funs.GetNewIntOrZero(getItem12.Month3) + r5.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem13 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 13);
                        if (getItem13 != null)
                        {
                            var r6 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 6);
                            if (r6 != null)
                            {
                                getItem13.Month3 = (Funs.GetNewIntOrZero(getItem13.Month3) + r6.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem14 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 14);
                        if (getItem14 != null)
                        {
                            var r7 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 7);
                            if (r7 != null)
                            {
                                getItem14.Month3 = (Funs.GetNewIntOrZero(getItem14.Month3) + r7.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem15 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 15);
                        if (getItem15 != null)
                        {
                            var r8 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 8);
                            if (r8 != null)
                            {
                                getItem15.Month3 = (Funs.GetNewIntOrZero(getItem15.Month3) + r8.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem16 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 16);
                        if (getItem16 != null)
                        {
                            var r9 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 9);
                            if (r9 != null)
                            {
                                getItem16.Month3 = (Funs.GetNewIntOrZero(getItem16.Month3) + r9.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem17 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 17);
                        if (getItem17 != null)
                        {
                            var r10 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 10);
                            if (r10 != null)
                            {
                                getItem17.Month3 = (Funs.GetNewIntOrZero(getItem17.Month3) + r10.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem18 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 18);
                        if (getItem18 != null)
                        {
                            var r11 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 11);
                            if (r11 != null)
                            {
                                getItem18.Month3 = (Funs.GetNewIntOrZero(getItem18.Month3) + r11.MonthTimes ?? 0).ToString();
                            }
                        }
                    }
                    ////大型、特种设备
                    var getSeDin_MonthReport5 = from x in db.SeDin_MonthReport5
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    var getItem19 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 19);
                    if (getSeDin_MonthReport5.Count() > 0 && getItem19 != null)
                    {
                        int countSumR5 = getSeDin_MonthReport5.Sum(x => x.T01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.T04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T05 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T06 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.S01 ?? 0);
                        getItem19.Month3 = (Funs.GetNewIntOrZero(getItem19.Month3) + countSumR5).ToString();
                    }
                    ////安全生产费用
                    var getSeDin_MonthReport6 = db.SeDin_MonthReport6.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport6 != null)
                    {
                        var getItem20 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 20);
                        if (getItem20 != null)
                        {
                            getItem20.Month3 = (Funs.GetNewDecimalOrZero(getItem20.Month3) + getSeDin_MonthReport6.SumMonth ?? 0).ToString();
                        }
                    }
                    ////项目现场员工入场安全培训人数
                    var getSeDin_MonthReport7 = db.SeDin_MonthReport7.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport7 != null)
                    {
                        var getItem21 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 21);
                        if (getItem21 != null)
                        {
                            getItem21.Month3 = (Funs.GetNewIntOrZero(getItem21.Month3) + getSeDin_MonthReport7.EmployeeMontPerson ?? 0).ToString();
                        }
                    }
                    ////项目会次
                    var getSeDin_MonthReport8 = db.SeDin_MonthReport8.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport8 != null)
                    {
                        var getItem22 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 22);
                        if (getItem22 != null)
                        {
                            getItem22.Month3 = (Funs.GetNewIntOrZero(getItem22.Month3) + getSeDin_MonthReport8.MonthMontNum ?? 0).ToString();
                        }
                        var getItem23 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 23);
                        if (getItem23 != null)
                        {
                            getItem23.Month3 = (Funs.GetNewIntOrZero(getItem23.Month3) + getSeDin_MonthReport8.WeekMontNum ?? 0).ToString();
                        }
                        var getItem24 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 24);
                        if (getItem24 != null)
                        {
                            getItem24.Month3 = (Funs.GetNewIntOrZero(getItem24.Month3) + getSeDin_MonthReport8.SpecialMontPerson ?? 0).ToString();
                        }
                    }
                    /// 安全检查
                    var getSeDin_MonthReport9 = db.SeDin_MonthReport9.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport9 != null)
                    {
                        var getItem25 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 25);
                        if (getItem25 != null)
                        {
                            getItem25.Month3 = (Funs.GetNewIntOrZero(getItem25.Month3) + getSeDin_MonthReport9.DailyMonth ?? 0).ToString();
                        }
                        var getItem26 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 26);
                        if (getItem26 != null)
                        {
                            getItem26.Month3 = (Funs.GetNewIntOrZero(getItem26.Month3) + getSeDin_MonthReport9.WeekMonth ?? 0).ToString();
                        }
                        var getItem27 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 27);
                        if (getItem27 != null)
                        {
                            getItem27.Month3 = (Funs.GetNewIntOrZero(getItem27.Month3) + getSeDin_MonthReport9.SpecialMonth ?? 0).ToString();
                        }
                        var getItem28 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 28);
                        if (getItem28 != null)
                        {
                            getItem28.Month3 = (Funs.GetNewIntOrZero(getItem28.Month3) + getSeDin_MonthReport9.MonthlyMonth ?? 0).ToString();
                        }
                    }
                    /////隐患整改单
                    var getSeDin_MonthReport9Rectification = from x in db.SeDin_MonthReport9Item_Rectification where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Rectification.Count() > 0)
                    {
                        var getItem29 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 29);
                        if (getItem29 != null)
                        {
                            getItem29.Month3 = (Funs.GetNewIntOrZero(getItem29.Month3) + getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                        var getItem30 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 30);
                        if (getItem30 != null)
                        {
                            getItem30.Month3 = (Funs.GetNewIntOrZero(getItem30.Month3) + getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMoth ?? 0)).ToString();
                        }
                        var getItem31 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 31);
                        if (getItem31 != null)
                        {
                            getItem31.Month3 = (Funs.GetNewIntOrZero(getItem31.Month3) + getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthLarge ?? 0)).ToString();
                        }
                        var getItem32 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 32);
                        if (getItem32 != null)
                        {
                            getItem32.Month3 = (Funs.GetNewIntOrZero(getItem32.Month3) + getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothLarge ?? 0)).ToString();
                        }
                        var getItem33 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 33);
                        if (getItem33 != null)
                        {
                            getItem33.Month3 = (Funs.GetNewIntOrZero(getItem33.Month3) + getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthSerious ?? 0)).ToString();
                        }
                        var getItem34 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 34);
                        if (getItem34 != null)
                        {
                            getItem34.Month3 = (Funs.GetNewIntOrZero(getItem34.Month3) + getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothSerious ?? 0)).ToString();
                        }
                    }
                    /////停工令
                    var getSeDin_MonthReport9Stoppage = from x in db.SeDin_MonthReport9Item_Stoppage where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Stoppage.Count() > 0)
                    {
                        var getItem35 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 35);
                        if (getItem35 != null)
                        {
                            getItem35.Month3 = (Funs.GetNewIntOrZero(getItem35.Month3) + getSeDin_MonthReport9Stoppage.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                    }
                    ////奖惩
                    var getSeDin_MonthReport10 = db.SeDin_MonthReport10.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport10 != null)
                    {
                        var getItem36 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 36);
                        if (getItem36 != null)
                        {
                            getItem36.Month3 = (Funs.GetNewDecimalOrZero(getItem36.Month3) + getSeDin_MonthReport10.SafeMonthMoney ?? 0 + getSeDin_MonthReport10.HseMonthMoney ?? 0 + getSeDin_MonthReport10.ProduceMonthMoney ?? 0).ToString();
                        }
                        var getItem37 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 37);
                        if (getItem37 != null)
                        {
                            getItem37.Month3 = (Funs.GetNewDecimalOrZero(getItem37.Month3) + getSeDin_MonthReport10.AccidentMonthMoney ?? 0 + getSeDin_MonthReport10.ViolationMonthMoney ?? 0 + getSeDin_MonthReport10.ManageMonthMoney ?? 0).ToString();
                        }
                    }
                    ////危大工程
                    var getSeDin_MonthReport11 = db.SeDin_MonthReport11.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport11 != null)
                    {
                        var getItem38 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 38);
                        if (getItem38 != null)
                        {
                            getItem38.Month3 = (Funs.GetNewIntOrZero(getItem38.Month3) + getSeDin_MonthReport11.RiskWorkNum ?? 0).ToString();
                        }
                        var getItem39 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 39);
                        if (getItem39 != null)
                        {
                            getItem39.Month3 = (Funs.GetNewIntOrZero(getItem39.Month3) + getSeDin_MonthReport11.LargeWorkNum ?? 0).ToString();
                        }
                    }
                    ////应急
                    var getSeDin_MonthReport12 = db.SeDin_MonthReport12.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport12 != null)
                    {
                        var getItem40 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 40);
                        if (getItem40 != null)
                        {
                            getItem40.Month3 = (Funs.GetNewIntOrZero(getItem40.Month3) + getSeDin_MonthReport12.MultipleSiteNum ?? 0 + getSeDin_MonthReport12.MultipleDesktopNum ?? 0
                                + getSeDin_MonthReport12.SingleSiteNum ?? 0 + getSeDin_MonthReport12.SingleDesktopNum ?? 0).ToString();
                        }
                    }
                    db.SubmitChanges();
                }
                #endregion
            }
            else if (monthReport.ReporMonth.Value.Month == 4)
            {
                #region 提交情况
                bool isSave = true;
                var getSubmissionMonth = getHSEDataCollectISubmissionYear.FirstOrDefault(x => x.ProjectId == monthReport.ProjectId);
                if (getSubmissionMonth == null)
                {
                    Model.DigData_HSEDataCollectSubmission newSubmission = new Model.DigData_HSEDataCollectSubmission()
                    {
                        HSEDataCollectSubmissionId = SQLHelper.GetNewID(),
                        HSEDataCollectId = getHSEDataCollectId,
                        Year = monthReport.ReporMonth.Value.Year,
                        ProjectId = monthReport.ProjectId,
                        Month4 = string.Format("{0:yyyy-MM-dd}", (monthReport.CommitTime ?? DateTime.Now)),
                    };
                    db.DigData_HSEDataCollectSubmission.InsertOnSubmit(newSubmission);
                    db.SubmitChanges();
                }
                else
                {
                    if (!string.IsNullOrEmpty(getSubmissionMonth.Month4))
                    {
                        isSave = false;
                    }
                    else
                    {
                        getSubmissionMonth.Month4 = string.Format("{0:yyyy-MM-dd}", (monthReport.CommitTime ?? DateTime.Now));
                        db.SubmitChanges();
                    }
                }
                #endregion
                #region 四月份 数据汇总
                if (isSave)
                {
                    ////员工
                    var getSeDin_MonthReport4Other = db.SeDin_MonthReport4Other.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport4Other != null)
                    {
                        var getItem1 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 1);
                        if (getItem1 != null)
                        {
                            getItem1.Month4 = (Funs.GetNewIntOrZero(getItem1.Month4) + getSeDin_MonthReport4Other.TotalNum ?? 0).ToString();
                        }
                        var getItem2 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 2);
                        if (getItem2 != null)
                        {
                            getItem2.Month4 = (Funs.GetNewIntOrZero(getItem2.Month4) + getSeDin_MonthReport4Other.OutsideNum ?? 0).ToString();
                        }
                        var getItem3 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 3);
                        if (getItem3 != null)
                        {
                            getItem3.Month4 = (Funs.GetNewIntOrZero(getItem3.Month4) + getSeDin_MonthReport4Other.ForeignNum ?? 0).ToString();
                        }
                        var getItem4 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 4);
                        if (getItem4 != null)
                        {
                            getItem4.Month4 = (Funs.GetNewIntOrZero(getItem4.Month4) + getSeDin_MonthReport4Other.ManagerNum ?? 0).ToString();
                        }
                    }
                    ////分包人数
                    var getSeDin_MonthReport4 = from x in db.SeDin_MonthReport4
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport4.Count() > 0)
                    {
                        var getItem5 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 5);
                        if (getItem5 != null)
                        {
                            getItem5.Month4 = (Funs.GetNewIntOrZero(getItem5.Month4) + getSeDin_MonthReport4.Sum(x => x.TotalNum ?? 0)).ToString();
                        }
                        var getItem6 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 6);
                        if (getItem6 != null)
                        {
                            getItem6.Month4 = (Funs.GetNewIntOrZero(getItem6.Month4) + getSeDin_MonthReport4.Sum(x => x.SafeManangerNum ?? 0)).ToString();
                        }
                    }
                    ///安全人工时
                    var getSeDin_MonthReport2 = db.SeDin_MonthReport2.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport2 != null)
                    {
                        var getItem7 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 7);
                        if (getItem7 != null)
                        {
                            getItem7.Month4 = (Funs.GetNewDecimalOrZero(getItem7.Month4) + getSeDin_MonthReport2.MonthWorkTime ?? 0).ToString();
                        }
                    }
                    ////事故
                    var getSeDin_MonthReport3 = from x in db.SeDin_MonthReport3
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport3.Count() > 0)
                    {
                        var getItem8 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 8);
                        if (getItem8 != null)
                        {
                            var r1 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 1);
                            if (r1 != null)
                            {
                                getItem8.Month4 = (Funs.GetNewIntOrZero(getItem8.Month4) + r1.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem9 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 9);
                        if (getItem9 != null)
                        {
                            var r2 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 2);
                            if (r2 != null)
                            {
                                getItem9.Month4 = (Funs.GetNewIntOrZero(getItem9.Month4) + r2.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem10 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 10);
                        if (getItem10 != null)
                        {
                            var r3 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 3);
                            if (r3 != null)
                            {
                                getItem10.Month4 = (Funs.GetNewIntOrZero(getItem10.Month4) + r3.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem11 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 11);
                        if (getItem11 != null)
                        {
                            var r4 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 4);
                            if (r4 != null)
                            {
                                getItem11.Month4 = (Funs.GetNewIntOrZero(getItem11.Month4) + r4.MonthPersons ?? 0).ToString();
                            }
                        }

                        var getItem12 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 12);
                        if (getItem12 != null)
                        {
                            var r5 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 5);
                            if (r5 != null)
                            {
                                getItem12.Month4 = (Funs.GetNewIntOrZero(getItem12.Month4) + r5.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem13 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 13);
                        if (getItem13 != null)
                        {
                            var r6 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 6);
                            if (r6 != null)
                            {
                                getItem13.Month4 = (Funs.GetNewIntOrZero(getItem13.Month4) + r6.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem14 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 14);
                        if (getItem14 != null)
                        {
                            var r7 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 7);
                            if (r7 != null)
                            {
                                getItem14.Month4 = (Funs.GetNewIntOrZero(getItem14.Month4) + r7.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem15 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 15);
                        if (getItem15 != null)
                        {
                            var r8 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 8);
                            if (r8 != null)
                            {
                                getItem15.Month4 = (Funs.GetNewIntOrZero(getItem15.Month4) + r8.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem16 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 16);
                        if (getItem16 != null)
                        {
                            var r9 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 9);
                            if (r9 != null)
                            {
                                getItem16.Month4 = (Funs.GetNewIntOrZero(getItem16.Month4) + r9.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem17 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 17);
                        if (getItem17 != null)
                        {
                            var r10 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 10);
                            if (r10 != null)
                            {
                                getItem17.Month4 = (Funs.GetNewIntOrZero(getItem17.Month4) + r10.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem18 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 18);
                        if (getItem18 != null)
                        {
                            var r11 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 11);
                            if (r11 != null)
                            {
                                getItem18.Month4 = (Funs.GetNewIntOrZero(getItem18.Month4) + r11.MonthTimes ?? 0).ToString();
                            }
                        }
                    }
                    ////大型、特种设备
                    var getSeDin_MonthReport5 = from x in db.SeDin_MonthReport5
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    var getItem19 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 19);
                    if (getSeDin_MonthReport5.Count() > 0 && getItem19 != null)
                    {
                        int countSumR5 = getSeDin_MonthReport5.Sum(x => x.T01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.T04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T05 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T06 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.S01 ?? 0);
                        getItem19.Month4 = (Funs.GetNewIntOrZero(getItem19.Month4) + countSumR5).ToString();
                    }
                    ////安全生产费用
                    var getSeDin_MonthReport6 = db.SeDin_MonthReport6.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport6 != null)
                    {
                        var getItem20 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 20);
                        if (getItem20 != null)
                        {
                            getItem20.Month4 = (Funs.GetNewDecimalOrZero(getItem20.Month4) + getSeDin_MonthReport6.SumMonth ?? 0).ToString();
                        }
                    }
                    ////项目现场员工入场安全培训人数
                    var getSeDin_MonthReport7 = db.SeDin_MonthReport7.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport7 != null)
                    {
                        var getItem21 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 21);
                        if (getItem21 != null)
                        {
                            getItem21.Month4 = (Funs.GetNewIntOrZero(getItem21.Month4) + getSeDin_MonthReport7.EmployeeMontPerson ?? 0).ToString();
                        }
                    }
                    ////项目会次
                    var getSeDin_MonthReport8 = db.SeDin_MonthReport8.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport8 != null)
                    {
                        var getItem22 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 22);
                        if (getItem22 != null)
                        {
                            getItem22.Month4 = (Funs.GetNewIntOrZero(getItem22.Month4) + getSeDin_MonthReport8.MonthMontNum ?? 0).ToString();
                        }
                        var getItem23 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 23);
                        if (getItem23 != null)
                        {
                            getItem23.Month4 = (Funs.GetNewIntOrZero(getItem23.Month4) + getSeDin_MonthReport8.WeekMontNum ?? 0).ToString();
                        }
                        var getItem24 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 24);
                        if (getItem24 != null)
                        {
                            getItem24.Month4 = (Funs.GetNewIntOrZero(getItem24.Month4) + getSeDin_MonthReport8.SpecialMontPerson ?? 0).ToString();
                        }
                    }
                    /// 安全检查
                    var getSeDin_MonthReport9 = db.SeDin_MonthReport9.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport9 != null)
                    {
                        var getItem25 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 25);
                        if (getItem25 != null)
                        {
                            getItem25.Month4 = (Funs.GetNewIntOrZero(getItem25.Month4) + getSeDin_MonthReport9.DailyMonth ?? 0).ToString();
                        }
                        var getItem26 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 26);
                        if (getItem26 != null)
                        {
                            getItem26.Month4 = (Funs.GetNewIntOrZero(getItem26.Month4) + getSeDin_MonthReport9.WeekMonth ?? 0).ToString();
                        }
                        var getItem27 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 27);
                        if (getItem27 != null)
                        {
                            getItem27.Month4 = (Funs.GetNewIntOrZero(getItem27.Month4) + getSeDin_MonthReport9.SpecialMonth ?? 0).ToString();
                        }
                        var getItem28 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 28);
                        if (getItem28 != null)
                        {
                            getItem28.Month4 = (Funs.GetNewIntOrZero(getItem28.Month4) + getSeDin_MonthReport9.MonthlyMonth ?? 0).ToString();
                        }
                    }
                    /////隐患整改单
                    var getSeDin_MonthReport9Rectification = from x in db.SeDin_MonthReport9Item_Rectification where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Rectification.Count() > 0)
                    {
                        var getItem29 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 29);
                        if (getItem29 != null)
                        {
                            getItem29.Month4 = (Funs.GetNewIntOrZero(getItem29.Month4) + getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                        var getItem30 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 30);
                        if (getItem30 != null)
                        {
                            getItem30.Month4 = (Funs.GetNewIntOrZero(getItem30.Month4) + getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMoth ?? 0)).ToString();
                        }
                        var getItem31 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 31);
                        if (getItem31 != null)
                        {
                            getItem31.Month4 = (Funs.GetNewIntOrZero(getItem31.Month4) + getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthLarge ?? 0)).ToString();
                        }
                        var getItem32 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 32);
                        if (getItem32 != null)
                        {
                            getItem32.Month4 = (Funs.GetNewIntOrZero(getItem32.Month4) + getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothLarge ?? 0)).ToString();
                        }
                        var getItem33 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 33);
                        if (getItem33 != null)
                        {
                            getItem33.Month4 = (Funs.GetNewIntOrZero(getItem33.Month4) + getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthSerious ?? 0)).ToString();
                        }
                        var getItem34 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 34);
                        if (getItem34 != null)
                        {
                            getItem34.Month4 = (Funs.GetNewIntOrZero(getItem34.Month4) + getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothSerious ?? 0)).ToString();
                        }
                    }
                    /////停工令
                    var getSeDin_MonthReport9Stoppage = from x in db.SeDin_MonthReport9Item_Stoppage where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Stoppage.Count() > 0)
                    {
                        var getItem35 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 35);
                        if (getItem35 != null)
                        {
                            getItem35.Month4 = (Funs.GetNewIntOrZero(getItem35.Month4) + getSeDin_MonthReport9Stoppage.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                    }
                    ////奖惩
                    var getSeDin_MonthReport10 = db.SeDin_MonthReport10.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport10 != null)
                    {
                        var getItem36 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 36);
                        if (getItem36 != null)
                        {
                            getItem36.Month4 = (Funs.GetNewDecimalOrZero(getItem36.Month4) + getSeDin_MonthReport10.SafeMonthMoney ?? 0 + getSeDin_MonthReport10.HseMonthMoney ?? 0 + getSeDin_MonthReport10.ProduceMonthMoney ?? 0).ToString();
                        }
                        var getItem37 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 37);
                        if (getItem37 != null)
                        {
                            getItem37.Month4 = (Funs.GetNewDecimalOrZero(getItem37.Month4) + getSeDin_MonthReport10.AccidentMonthMoney ?? 0 + getSeDin_MonthReport10.ViolationMonthMoney ?? 0 + getSeDin_MonthReport10.ManageMonthMoney ?? 0).ToString();
                        }
                    }
                    ////危大工程
                    var getSeDin_MonthReport11 = db.SeDin_MonthReport11.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport11 != null)
                    {
                        var getItem38 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 38);
                        if (getItem38 != null)
                        {
                            getItem38.Month4 = (Funs.GetNewIntOrZero(getItem38.Month4) + getSeDin_MonthReport11.RiskWorkNum ?? 0).ToString();
                        }
                        var getItem39 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 39);
                        if (getItem39 != null)
                        {
                            getItem39.Month4 = (Funs.GetNewIntOrZero(getItem39.Month4) + getSeDin_MonthReport11.LargeWorkNum ?? 0).ToString();
                        }
                    }
                    ////应急
                    var getSeDin_MonthReport12 = db.SeDin_MonthReport12.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport12 != null)
                    {
                        var getItem40 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 40);
                        if (getItem40 != null)
                        {
                            getItem40.Month4 = (Funs.GetNewIntOrZero(getItem40.Month4) + getSeDin_MonthReport12.MultipleSiteNum ?? 0 + getSeDin_MonthReport12.MultipleDesktopNum ?? 0
                                + getSeDin_MonthReport12.SingleSiteNum ?? 0 + getSeDin_MonthReport12.SingleDesktopNum ?? 0).ToString();
                        }
                    }
                    db.SubmitChanges();
                }
                #endregion
            }
            else if (monthReport.ReporMonth.Value.Month == 5)
            {
                #region 提交情况
                bool isSave = true;
                var getSubmissionMonth = getHSEDataCollectISubmissionYear.FirstOrDefault(x => x.ProjectId == monthReport.ProjectId);
                if (getSubmissionMonth == null)
                {
                    Model.DigData_HSEDataCollectSubmission newSubmission = new Model.DigData_HSEDataCollectSubmission()
                    {
                        HSEDataCollectSubmissionId = SQLHelper.GetNewID(),
                        HSEDataCollectId = getHSEDataCollectId,
                        Year = monthReport.ReporMonth.Value.Year,
                        ProjectId = monthReport.ProjectId,
                        Month5 = string.Format("{0:yyyy-MM-dd}", (monthReport.CommitTime ?? DateTime.Now)),
                    };
                    db.DigData_HSEDataCollectSubmission.InsertOnSubmit(newSubmission);
                    db.SubmitChanges();
                }
                else
                {
                    if (!string.IsNullOrEmpty(getSubmissionMonth.Month5))
                    {
                        isSave = false;
                    }
                    else
                    {
                        getSubmissionMonth.Month5 = string.Format("{0:yyyy-MM-dd}", (monthReport.CommitTime ?? DateTime.Now));
                        db.SubmitChanges();
                    }
                }
                #endregion
                #region 五月份 数据汇总
                if (isSave)
                {
                    ////员工
                    var getSeDin_MonthReport4Other = db.SeDin_MonthReport4Other.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport4Other != null)
                    {
                        var getItem1 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 1);
                        if (getItem1 != null)
                        {
                            getItem1.Month5 = (Funs.GetNewIntOrZero(getItem1.Month5) + getSeDin_MonthReport4Other.TotalNum ?? 0).ToString();
                        }
                        var getItem2 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 2);
                        if (getItem2 != null)
                        {
                            getItem2.Month5 = (Funs.GetNewIntOrZero(getItem2.Month5) + getSeDin_MonthReport4Other.OutsideNum ?? 0).ToString();
                        }
                        var getItem3 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 3);
                        if (getItem3 != null)
                        {
                            getItem3.Month5 = (Funs.GetNewIntOrZero(getItem3.Month5) + getSeDin_MonthReport4Other.ForeignNum ?? 0).ToString();
                        }
                        var getItem4 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 4);
                        if (getItem4 != null)
                        {
                            getItem4.Month5 = (Funs.GetNewIntOrZero(getItem4.Month5) + getSeDin_MonthReport4Other.ManagerNum ?? 0).ToString();
                        }
                    }
                    ////分包人数
                    var getSeDin_MonthReport4 = from x in db.SeDin_MonthReport4
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport4.Count() > 0)
                    {
                        var getItem5 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 5);
                        if (getItem5 != null)
                        {
                            getItem5.Month5 = (Funs.GetNewIntOrZero(getItem5.Month5) + getSeDin_MonthReport4.Sum(x => x.TotalNum ?? 0)).ToString();
                        }
                        var getItem6 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 6);
                        if (getItem6 != null)
                        {
                            getItem6.Month5 = (Funs.GetNewIntOrZero(getItem6.Month5) + getSeDin_MonthReport4.Sum(x => x.SafeManangerNum ?? 0)).ToString();
                        }
                    }
                    ///安全人工时
                    var getSeDin_MonthReport2 = db.SeDin_MonthReport2.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport2 != null)
                    {
                        var getItem7 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 7);
                        if (getItem7 != null)
                        {
                            getItem7.Month5 = (Funs.GetNewDecimalOrZero(getItem7.Month5) + getSeDin_MonthReport2.MonthWorkTime ?? 0).ToString();
                        }
                    }
                    ////事故
                    var getSeDin_MonthReport3 = from x in db.SeDin_MonthReport3
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport3.Count() > 0)
                    {
                        var getItem8 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 8);
                        if (getItem8 != null)
                        {
                            var r1 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 1);
                            if (r1 != null)
                            {
                                getItem8.Month5 = (Funs.GetNewIntOrZero(getItem8.Month5) + r1.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem9 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 9);
                        if (getItem9 != null)
                        {
                            var r2 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 2);
                            if (r2 != null)
                            {
                                getItem9.Month5 = (Funs.GetNewIntOrZero(getItem9.Month5) + r2.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem10 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 10);
                        if (getItem10 != null)
                        {
                            var r3 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 3);
                            if (r3 != null)
                            {
                                getItem10.Month5 = (Funs.GetNewIntOrZero(getItem10.Month5) + r3.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem11 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 11);
                        if (getItem11 != null)
                        {
                            var r4 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 4);
                            if (r4 != null)
                            {
                                getItem11.Month5 = (Funs.GetNewIntOrZero(getItem11.Month5) + r4.MonthPersons ?? 0).ToString();
                            }
                        }

                        var getItem12 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 12);
                        if (getItem12 != null)
                        {
                            var r5 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 5);
                            if (r5 != null)
                            {
                                getItem12.Month5 = (Funs.GetNewIntOrZero(getItem12.Month5) + r5.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem13 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 13);
                        if (getItem13 != null)
                        {
                            var r6 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 6);
                            if (r6 != null)
                            {
                                getItem13.Month5 = (Funs.GetNewIntOrZero(getItem13.Month5) + r6.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem14 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 14);
                        if (getItem14 != null)
                        {
                            var r7 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 7);
                            if (r7 != null)
                            {
                                getItem14.Month5 = (Funs.GetNewIntOrZero(getItem14.Month5) + r7.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem15 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 15);
                        if (getItem15 != null)
                        {
                            var r8 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 8);
                            if (r8 != null)
                            {
                                getItem15.Month5 = (Funs.GetNewIntOrZero(getItem15.Month5) + r8.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem16 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 16);
                        if (getItem16 != null)
                        {
                            var r9 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 9);
                            if (r9 != null)
                            {
                                getItem16.Month5 = (Funs.GetNewIntOrZero(getItem16.Month5) + r9.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem17 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 17);
                        if (getItem17 != null)
                        {
                            var r10 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 10);
                            if (r10 != null)
                            {
                                getItem17.Month5 = (Funs.GetNewIntOrZero(getItem17.Month5) + r10.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem18 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 18);
                        if (getItem18 != null)
                        {
                            var r11 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 11);
                            if (r11 != null)
                            {
                                getItem18.Month5 = (Funs.GetNewIntOrZero(getItem18.Month5) + r11.MonthTimes ?? 0).ToString();
                            }
                        }
                    }
                    ////大型、特种设备
                    var getSeDin_MonthReport5 = from x in db.SeDin_MonthReport5
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    var getItem19 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 19);
                    if (getSeDin_MonthReport5.Count() > 0 && getItem19 != null)
                    {
                        int countSumR5 = getSeDin_MonthReport5.Sum(x => x.T01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.T04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T05 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T06 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.S01 ?? 0);
                        getItem19.Month5 = (Funs.GetNewIntOrZero(getItem19.Month5) + countSumR5).ToString();
                    }
                    ////安全生产费用
                    var getSeDin_MonthReport6 = db.SeDin_MonthReport6.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport6 != null)
                    {
                        var getItem20 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 20);
                        if (getItem20 != null)
                        {
                            getItem20.Month5 = (Funs.GetNewDecimalOrZero(getItem20.Month5) + getSeDin_MonthReport6.SumMonth ?? 0).ToString();
                        }
                    }
                    ////项目现场员工入场安全培训人数
                    var getSeDin_MonthReport7 = db.SeDin_MonthReport7.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport7 != null)
                    {
                        var getItem21 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 21);
                        if (getItem21 != null)
                        {
                            getItem21.Month5 = (Funs.GetNewIntOrZero(getItem21.Month5) + getSeDin_MonthReport7.EmployeeMontPerson ?? 0).ToString();
                        }
                    }
                    ////项目会次
                    var getSeDin_MonthReport8 = db.SeDin_MonthReport8.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport8 != null)
                    {
                        var getItem22 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 22);
                        if (getItem22 != null)
                        {
                            getItem22.Month5 = (Funs.GetNewIntOrZero(getItem22.Month5) + getSeDin_MonthReport8.MonthMontNum ?? 0).ToString();
                        }
                        var getItem23 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 23);
                        if (getItem23 != null)
                        {
                            getItem23.Month5 = (Funs.GetNewIntOrZero(getItem23.Month5) + getSeDin_MonthReport8.WeekMontNum ?? 0).ToString();
                        }
                        var getItem24 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 24);
                        if (getItem24 != null)
                        {
                            getItem24.Month5 = (Funs.GetNewIntOrZero(getItem24.Month5) + getSeDin_MonthReport8.SpecialMontPerson ?? 0).ToString();
                        }
                    }
                    /// 安全检查
                    var getSeDin_MonthReport9 = db.SeDin_MonthReport9.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport9 != null)
                    {
                        var getItem25 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 25);
                        if (getItem25 != null)
                        {
                            getItem25.Month5 = (Funs.GetNewIntOrZero(getItem25.Month5) + getSeDin_MonthReport9.DailyMonth ?? 0).ToString();
                        }
                        var getItem26 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 26);
                        if (getItem26 != null)
                        {
                            getItem26.Month5 = (Funs.GetNewIntOrZero(getItem26.Month5) + getSeDin_MonthReport9.WeekMonth ?? 0).ToString();
                        }
                        var getItem27 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 27);
                        if (getItem27 != null)
                        {
                            getItem27.Month5 = (Funs.GetNewIntOrZero(getItem27.Month5) + getSeDin_MonthReport9.SpecialMonth ?? 0).ToString();
                        }
                        var getItem28 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 28);
                        if (getItem28 != null)
                        {
                            getItem28.Month5 = (Funs.GetNewIntOrZero(getItem28.Month5) + getSeDin_MonthReport9.MonthlyMonth ?? 0).ToString();
                        }
                    }
                    /////隐患整改单
                    var getSeDin_MonthReport9Rectification = from x in db.SeDin_MonthReport9Item_Rectification where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Rectification.Count() > 0)
                    {
                        var getItem29 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 29);
                        if (getItem29 != null)
                        {
                            getItem29.Month5 = (Funs.GetNewIntOrZero(getItem29.Month5) + getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                        var getItem30 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 30);
                        if (getItem30 != null)
                        {
                            getItem30.Month5 = (Funs.GetNewIntOrZero(getItem30.Month5) + getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMoth ?? 0)).ToString();
                        }
                        var getItem31 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 31);
                        if (getItem31 != null)
                        {
                            getItem31.Month5 = (Funs.GetNewIntOrZero(getItem31.Month5) + getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthLarge ?? 0)).ToString();
                        }
                        var getItem32 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 32);
                        if (getItem32 != null)
                        {
                            getItem32.Month5 = (Funs.GetNewIntOrZero(getItem32.Month5) + getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothLarge ?? 0)).ToString();
                        }
                        var getItem33 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 33);
                        if (getItem33 != null)
                        {
                            getItem33.Month5 = (Funs.GetNewIntOrZero(getItem33.Month5) + getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthSerious ?? 0)).ToString();
                        }
                        var getItem34 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 34);
                        if (getItem34 != null)
                        {
                            getItem34.Month5 = (Funs.GetNewIntOrZero(getItem34.Month5) + getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothSerious ?? 0)).ToString();
                        }
                    }
                    /////停工令
                    var getSeDin_MonthReport9Stoppage = from x in db.SeDin_MonthReport9Item_Stoppage where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Stoppage.Count() > 0)
                    {
                        var getItem35 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 35);
                        if (getItem35 != null)
                        {
                            getItem35.Month5 = (Funs.GetNewIntOrZero(getItem35.Month5) + getSeDin_MonthReport9Stoppage.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                    }
                    ////奖惩
                    var getSeDin_MonthReport10 = db.SeDin_MonthReport10.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport10 != null)
                    {
                        var getItem36 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 36);
                        if (getItem36 != null)
                        {
                            getItem36.Month5 = (Funs.GetNewDecimalOrZero(getItem36.Month5) + getSeDin_MonthReport10.SafeMonthMoney ?? 0 + getSeDin_MonthReport10.HseMonthMoney ?? 0 + getSeDin_MonthReport10.ProduceMonthMoney ?? 0).ToString();
                        }
                        var getItem37 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 37);
                        if (getItem37 != null)
                        {
                            getItem37.Month5 = (Funs.GetNewDecimalOrZero(getItem37.Month5) + getSeDin_MonthReport10.AccidentMonthMoney ?? 0 + getSeDin_MonthReport10.ViolationMonthMoney ?? 0 + getSeDin_MonthReport10.ManageMonthMoney ?? 0).ToString();
                        }
                    }
                    ////危大工程
                    var getSeDin_MonthReport11 = db.SeDin_MonthReport11.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport11 != null)
                    {
                        var getItem38 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 38);
                        if (getItem38 != null)
                        {
                            getItem38.Month5 = (Funs.GetNewIntOrZero(getItem38.Month5) + getSeDin_MonthReport11.RiskWorkNum ?? 0).ToString();
                        }
                        var getItem39 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 39);
                        if (getItem39 != null)
                        {
                            getItem39.Month5 = (Funs.GetNewIntOrZero(getItem39.Month5) + getSeDin_MonthReport11.LargeWorkNum ?? 0).ToString();
                        }
                    }
                    ////应急
                    var getSeDin_MonthReport12 = db.SeDin_MonthReport12.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport12 != null)
                    {
                        var getItem40 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 40);
                        if (getItem40 != null)
                        {
                            getItem40.Month5 = (Funs.GetNewIntOrZero(getItem40.Month5) + getSeDin_MonthReport12.MultipleSiteNum ?? 0 + getSeDin_MonthReport12.MultipleDesktopNum ?? 0
                                + getSeDin_MonthReport12.SingleSiteNum ?? 0 + getSeDin_MonthReport12.SingleDesktopNum ?? 0).ToString();
                        }
                    }
                    db.SubmitChanges();
                }
                #endregion
            }
            else if (monthReport.ReporMonth.Value.Month == 6)
            {
                #region 提交情况
                bool isSave = true;
                var getSubmissionMonth = getHSEDataCollectISubmissionYear.FirstOrDefault(x => x.ProjectId == monthReport.ProjectId);
                if (getSubmissionMonth == null)
                {
                    Model.DigData_HSEDataCollectSubmission newSubmission = new Model.DigData_HSEDataCollectSubmission()
                    {
                        HSEDataCollectSubmissionId = SQLHelper.GetNewID(),
                        HSEDataCollectId = getHSEDataCollectId,
                        Year = monthReport.ReporMonth.Value.Year,
                        ProjectId = monthReport.ProjectId,
                        Month6 = string.Format("{0:yyyy-MM-dd}", (monthReport.CommitTime ?? DateTime.Now)),
                    };
                    db.DigData_HSEDataCollectSubmission.InsertOnSubmit(newSubmission);
                    db.SubmitChanges();
                }
                else
                {
                    if (!string.IsNullOrEmpty(getSubmissionMonth.Month6))
                    {
                        isSave = false;
                    }
                    else
                    {
                        getSubmissionMonth.Month6 = string.Format("{0:yyyy-MM-dd}", (monthReport.CommitTime ?? DateTime.Now));
                        db.SubmitChanges();
                    }
                }
                #endregion
                #region 数据汇总
                if (isSave)
                {
                    ////员工
                    var getSeDin_MonthReport4Other = db.SeDin_MonthReport4Other.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport4Other != null)
                    {
                        var getItem1 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 1);
                        if (getItem1 != null)
                        {
                            getItem1.Month6 = (Funs.GetNewIntOrZero(getItem1.Month6) + getSeDin_MonthReport4Other.TotalNum ?? 0).ToString();
                        }
                        var getItem2 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 2);
                        if (getItem2 != null)
                        {
                            getItem2.Month6 = (Funs.GetNewIntOrZero(getItem2.Month6) + getSeDin_MonthReport4Other.OutsideNum ?? 0).ToString();
                        }
                        var getItem3 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 3);
                        if (getItem3 != null)
                        {
                            getItem3.Month6 = (Funs.GetNewIntOrZero(getItem3.Month6) + getSeDin_MonthReport4Other.ForeignNum ?? 0).ToString();
                        }
                        var getItem4 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 4);
                        if (getItem4 != null)
                        {
                            getItem4.Month6 = (Funs.GetNewIntOrZero(getItem4.Month6) + getSeDin_MonthReport4Other.ManagerNum ?? 0).ToString();
                        }
                    }
                    ////分包人数
                    var getSeDin_MonthReport4 = from x in db.SeDin_MonthReport4
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport4.Count() > 0)
                    {
                        var getItem5 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 5);
                        if (getItem5 != null)
                        {
                            getItem5.Month6 = (Funs.GetNewIntOrZero(getItem5.Month6) + getSeDin_MonthReport4.Sum(x => x.TotalNum ?? 0)).ToString();
                        }
                        var getItem6 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 6);
                        if (getItem6 != null)
                        {
                            getItem6.Month6 = (Funs.GetNewIntOrZero(getItem6.Month6) + getSeDin_MonthReport4.Sum(x => x.SafeManangerNum ?? 0)).ToString();
                        }
                    }
                    ///安全人工时
                    var getSeDin_MonthReport2 = db.SeDin_MonthReport2.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport2 != null)
                    {
                        var getItem7 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 7);
                        if (getItem7 != null)
                        {
                            getItem7.Month6 = (Funs.GetNewDecimalOrZero(getItem7.Month7) + getSeDin_MonthReport2.MonthWorkTime ?? 0).ToString();
                        }
                    }
                    ////事故
                    var getSeDin_MonthReport3 = from x in db.SeDin_MonthReport3
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport3.Count() > 0)
                    {
                        var getItem8 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 8);
                        if (getItem8 != null)
                        {
                            var r1 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 1);
                            if (r1 != null)
                            {
                                getItem8.Month6 = (Funs.GetNewIntOrZero(getItem8.Month6) + r1.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem9 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 9);
                        if (getItem9 != null)
                        {
                            var r2 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 2);
                            if (r2 != null)
                            {
                                getItem9.Month6 = (Funs.GetNewIntOrZero(getItem9.Month6) + r2.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem10 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 10);
                        if (getItem10 != null)
                        {
                            var r3 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 3);
                            if (r3 != null)
                            {
                                getItem10.Month6 = (Funs.GetNewIntOrZero(getItem10.Month6) + r3.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem11 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 11);
                        if (getItem11 != null)
                        {
                            var r4 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 4);
                            if (r4 != null)
                            {
                                getItem11.Month6 = (Funs.GetNewIntOrZero(getItem11.Month6) + r4.MonthPersons ?? 0).ToString();
                            }
                        }

                        var getItem12 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 12);
                        if (getItem12 != null)
                        {
                            var r5 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 5);
                            if (r5 != null)
                            {
                                getItem12.Month6 = (Funs.GetNewIntOrZero(getItem12.Month6) + r5.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem13 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 13);
                        if (getItem13 != null)
                        {
                            var r6 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 6);
                            if (r6 != null)
                            {
                                getItem13.Month6 = (Funs.GetNewIntOrZero(getItem13.Month6) + r6.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem14 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 14);
                        if (getItem14 != null)
                        {
                            var r7 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 7);
                            if (r7 != null)
                            {
                                getItem14.Month6 = (Funs.GetNewIntOrZero(getItem14.Month6) + r7.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem15 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 15);
                        if (getItem15 != null)
                        {
                            var r8 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 8);
                            if (r8 != null)
                            {
                                getItem15.Month6 = (Funs.GetNewIntOrZero(getItem15.Month6) + r8.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem16 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 16);
                        if (getItem16 != null)
                        {
                            var r9 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 9);
                            if (r9 != null)
                            {
                                getItem16.Month6 = (Funs.GetNewIntOrZero(getItem16.Month6) + r9.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem17 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 17);
                        if (getItem17 != null)
                        {
                            var r10 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 10);
                            if (r10 != null)
                            {
                                getItem17.Month6 = (Funs.GetNewIntOrZero(getItem17.Month6) + r10.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem18 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 18);
                        if (getItem18 != null)
                        {
                            var r11 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 11);
                            if (r11 != null)
                            {
                                getItem18.Month6 = (Funs.GetNewIntOrZero(getItem18.Month6) + r11.MonthTimes ?? 0).ToString();
                            }
                        }
                    }
                    ////大型、特种设备
                    var getSeDin_MonthReport5 = from x in db.SeDin_MonthReport5
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    var getItem19 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 19);
                    if (getSeDin_MonthReport5.Count() > 0 && getItem19 != null)
                    {
                        int countSumR5 = getSeDin_MonthReport5.Sum(x => x.T01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.T04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T05 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T06 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.S01 ?? 0);
                        getItem19.Month6 = (Funs.GetNewIntOrZero(getItem19.Month6) + countSumR5).ToString();
                    }
                    ////安全生产费用
                    var getSeDin_MonthReport6 = db.SeDin_MonthReport6.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport6 != null)
                    {
                        var getItem20 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 20);
                        if (getItem20 != null)
                        {
                            getItem20.Month6 = (Funs.GetNewDecimalOrZero(getItem20.Month6) + getSeDin_MonthReport6.SumMonth ?? 0).ToString();
                        }
                    }
                    ////项目现场员工入场安全培训人数
                    var getSeDin_MonthReport7 = db.SeDin_MonthReport7.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport7 != null)
                    {
                        var getItem21 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 21);
                        if (getItem21 != null)
                        {
                            getItem21.Month6 = (Funs.GetNewIntOrZero(getItem21.Month6) + getSeDin_MonthReport7.EmployeeMontPerson ?? 0).ToString();
                        }
                    }
                    ////项目会次
                    var getSeDin_MonthReport8 = db.SeDin_MonthReport8.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport8 != null)
                    {
                        var getItem22 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 22);
                        if (getItem22 != null)
                        {
                            getItem22.Month6 = (Funs.GetNewIntOrZero(getItem22.Month6) + getSeDin_MonthReport8.MonthMontNum ?? 0).ToString();
                        }
                        var getItem23 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 23);
                        if (getItem23 != null)
                        {
                            getItem23.Month6 = (Funs.GetNewIntOrZero(getItem23.Month6) + getSeDin_MonthReport8.WeekMontNum ?? 0).ToString();
                        }
                        var getItem24 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 24);
                        if (getItem24 != null)
                        {
                            getItem24.Month6 = (Funs.GetNewIntOrZero(getItem24.Month6) + getSeDin_MonthReport8.SpecialMontPerson ?? 0).ToString();
                        }
                    }
                    /// 安全检查
                    var getSeDin_MonthReport9 = db.SeDin_MonthReport9.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport9 != null)
                    {
                        var getItem25 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 25);
                        if (getItem25 != null)
                        {
                            getItem25.Month6 = (Funs.GetNewIntOrZero(getItem25.Month6) + getSeDin_MonthReport9.DailyMonth ?? 0).ToString();
                        }
                        var getItem26 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 26);
                        if (getItem26 != null)
                        {
                            getItem26.Month6 = (Funs.GetNewIntOrZero(getItem26.Month6) + getSeDin_MonthReport9.WeekMonth ?? 0).ToString();
                        }
                        var getItem27 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 27);
                        if (getItem27 != null)
                        {
                            getItem27.Month6 = (Funs.GetNewIntOrZero(getItem27.Month6) + getSeDin_MonthReport9.SpecialMonth ?? 0).ToString();
                        }
                        var getItem28 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 28);
                        if (getItem28 != null)
                        {
                            getItem28.Month6 = (Funs.GetNewIntOrZero(getItem28.Month6) + getSeDin_MonthReport9.MonthlyMonth ?? 0).ToString();
                        }
                    }
                    /////隐患整改单
                    var getSeDin_MonthReport9Rectification = from x in db.SeDin_MonthReport9Item_Rectification where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Rectification.Count() > 0)
                    {
                        var getItem29 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 29);
                        if (getItem29 != null)
                        {
                            getItem29.Month6 = (Funs.GetNewIntOrZero(getItem29.Month6) + getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                        var getItem30 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 30);
                        if (getItem30 != null)
                        {
                            getItem30.Month6 = (Funs.GetNewIntOrZero(getItem30.Month6) + getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMoth ?? 0)).ToString();
                        }
                        var getItem31 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 31);
                        if (getItem31 != null)
                        {
                            getItem31.Month6 = (Funs.GetNewIntOrZero(getItem31.Month6) + getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthLarge ?? 0)).ToString();
                        }
                        var getItem32 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 32);
                        if (getItem32 != null)
                        {
                            getItem32.Month6 = (Funs.GetNewIntOrZero(getItem32.Month6) + getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothLarge ?? 0)).ToString();
                        }
                        var getItem33 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 33);
                        if (getItem33 != null)
                        {
                            getItem33.Month6 = (Funs.GetNewIntOrZero(getItem33.Month6) + getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthSerious ?? 0)).ToString();
                        }
                        var getItem34 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 34);
                        if (getItem34 != null)
                        {
                            getItem34.Month6 = (Funs.GetNewIntOrZero(getItem34.Month6) + getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothSerious ?? 0)).ToString();
                        }
                    }
                    /////停工令
                    var getSeDin_MonthReport9Stoppage = from x in db.SeDin_MonthReport9Item_Stoppage where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Stoppage.Count() > 0)
                    {
                        var getItem35 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 35);
                        if (getItem35 != null)
                        {
                            getItem35.Month6 = (Funs.GetNewIntOrZero(getItem35.Month6) + getSeDin_MonthReport9Stoppage.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                    }
                    ////奖惩
                    var getSeDin_MonthReport10 = db.SeDin_MonthReport10.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport10 != null)
                    {
                        var getItem36 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 36);
                        if (getItem36 != null)
                        {
                            getItem36.Month6 = (Funs.GetNewDecimalOrZero(getItem36.Month6) + getSeDin_MonthReport10.SafeMonthMoney ?? 0 + getSeDin_MonthReport10.HseMonthMoney ?? 0 + getSeDin_MonthReport10.ProduceMonthMoney ?? 0).ToString();
                        }
                        var getItem37 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 37);
                        if (getItem37 != null)
                        {
                            getItem37.Month6 = (Funs.GetNewDecimalOrZero(getItem37.Month6) + getSeDin_MonthReport10.AccidentMonthMoney ?? 0 + getSeDin_MonthReport10.ViolationMonthMoney ?? 0 + getSeDin_MonthReport10.ManageMonthMoney ?? 0).ToString();
                        }
                    }
                    ////危大工程
                    var getSeDin_MonthReport11 = db.SeDin_MonthReport11.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport11 != null)
                    {
                        var getItem38 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 38);
                        if (getItem38 != null)
                        {
                            getItem38.Month6 = (Funs.GetNewIntOrZero(getItem38.Month6) + getSeDin_MonthReport11.RiskWorkNum ?? 0).ToString();
                        }
                        var getItem39 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 39);
                        if (getItem39 != null)
                        {
                            getItem39.Month6 = (Funs.GetNewIntOrZero(getItem39.Month6) + getSeDin_MonthReport11.LargeWorkNum ?? 0).ToString();
                        }
                    }
                    ////应急
                    var getSeDin_MonthReport12 = db.SeDin_MonthReport12.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport12 != null)
                    {
                        var getItem40 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 40);
                        if (getItem40 != null)
                        {
                            getItem40.Month6 = (Funs.GetNewIntOrZero(getItem40.Month6) + getSeDin_MonthReport12.MultipleSiteNum ?? 0 + getSeDin_MonthReport12.MultipleDesktopNum ?? 0
                                + getSeDin_MonthReport12.SingleSiteNum ?? 0 + getSeDin_MonthReport12.SingleDesktopNum ?? 0).ToString();
                        }
                    }
                    db.SubmitChanges();
                }
                #endregion
            }
            else if (monthReport.ReporMonth.Value.Month == 7)
            {
                #region 提交情况
                bool isSave = true;
                var getSubmissionMonth = getHSEDataCollectISubmissionYear.FirstOrDefault(x => x.ProjectId == monthReport.ProjectId);
                if (getSubmissionMonth == null)
                {
                    Model.DigData_HSEDataCollectSubmission newSubmission = new Model.DigData_HSEDataCollectSubmission()
                    {
                        HSEDataCollectSubmissionId = SQLHelper.GetNewID(),
                        HSEDataCollectId = getHSEDataCollectId,
                        Year = monthReport.ReporMonth.Value.Year,
                        ProjectId = monthReport.ProjectId,
                        Month7 = string.Format("{0:yyyy-MM-dd}", (monthReport.CommitTime ?? DateTime.Now)),
                    };
                    db.DigData_HSEDataCollectSubmission.InsertOnSubmit(newSubmission);
                    db.SubmitChanges();
                }
                else
                {
                    if (!string.IsNullOrEmpty(getSubmissionMonth.Month7))
                    {
                        isSave = false;
                    }
                    else
                    {
                        getSubmissionMonth.Month7 = string.Format("{0:yyyy-MM-dd}", (monthReport.CommitTime ?? DateTime.Now));
                        db.SubmitChanges();
                    }
                }
                #endregion
                #region 数据汇总
                if (isSave)
                {
                    ////员工
                    var getSeDin_MonthReport4Other = db.SeDin_MonthReport4Other.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport4Other != null)
                    {
                        var getItem1 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 1);
                        if (getItem1 != null)
                        {
                            getItem1.Month7 = (Funs.GetNewIntOrZero(getItem1.Month7) + getSeDin_MonthReport4Other.TotalNum ?? 0).ToString();
                        }
                        var getItem2 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 2);
                        if (getItem2 != null)
                        {
                            getItem2.Month7 = (Funs.GetNewIntOrZero(getItem2.Month7) + getSeDin_MonthReport4Other.OutsideNum ?? 0).ToString();
                        }
                        var getItem3 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 3);
                        if (getItem3 != null)
                        {
                            getItem3.Month7 = (Funs.GetNewIntOrZero(getItem3.Month7) + getSeDin_MonthReport4Other.ForeignNum ?? 0).ToString();
                        }
                        var getItem4 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 4);
                        if (getItem4 != null)
                        {
                            getItem4.Month7 = (Funs.GetNewIntOrZero(getItem4.Month7) + getSeDin_MonthReport4Other.ManagerNum ?? 0).ToString();
                        }
                    }
                    ////分包人数
                    var getSeDin_MonthReport4 = from x in db.SeDin_MonthReport4
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport4.Count() > 0)
                    {
                        var getItem5 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 5);
                        if (getItem5 != null)
                        {
                            getItem5.Month7 = (Funs.GetNewIntOrZero(getItem5.Month7) + getSeDin_MonthReport4.Sum(x => x.TotalNum ?? 0)).ToString();
                        }
                        var getItem6 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 6);
                        if (getItem6 != null)
                        {
                            getItem6.Month7 = (Funs.GetNewIntOrZero(getItem6.Month7) + getSeDin_MonthReport4.Sum(x => x.SafeManangerNum ?? 0)).ToString();
                        }
                    }
                    ///安全人工时
                    var getSeDin_MonthReport2 = db.SeDin_MonthReport2.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport2 != null)
                    {
                        var getItem7 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 7);
                        if (getItem7 != null)
                        {
                            getItem7.Month7 = (Funs.GetNewDecimalOrZero(getItem7.Month7) + getSeDin_MonthReport2.MonthWorkTime ?? 0).ToString();
                        }
                    }
                    ////事故
                    var getSeDin_MonthReport3 = from x in db.SeDin_MonthReport3
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport3.Count() > 0)
                    {
                        var getItem8 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 8);
                        if (getItem8 != null)
                        {
                            var r1 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 1);
                            if (r1 != null)
                            {
                                getItem8.Month7 = (Funs.GetNewIntOrZero(getItem8.Month7) + r1.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem9 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 9);
                        if (getItem9 != null)
                        {
                            var r2 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 2);
                            if (r2 != null)
                            {
                                getItem9.Month7 = (Funs.GetNewIntOrZero(getItem9.Month7) + r2.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem10 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 10);
                        if (getItem10 != null)
                        {
                            var r3 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 3);
                            if (r3 != null)
                            {
                                getItem10.Month7 = (Funs.GetNewIntOrZero(getItem10.Month7) + r3.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem11 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 11);
                        if (getItem11 != null)
                        {
                            var r4 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 4);
                            if (r4 != null)
                            {
                                getItem11.Month7 = (Funs.GetNewIntOrZero(getItem11.Month7) + r4.MonthPersons ?? 0).ToString();
                            }
                        }

                        var getItem12 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 12);
                        if (getItem12 != null)
                        {
                            var r5 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 5);
                            if (r5 != null)
                            {
                                getItem12.Month7 = (Funs.GetNewIntOrZero(getItem12.Month7) + r5.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem13 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 13);
                        if (getItem13 != null)
                        {
                            var r6 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 6);
                            if (r6 != null)
                            {
                                getItem13.Month7 = (Funs.GetNewIntOrZero(getItem13.Month7) + r6.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem14 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 14);
                        if (getItem14 != null)
                        {
                            var r7 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 7);
                            if (r7 != null)
                            {
                                getItem14.Month7 = (Funs.GetNewIntOrZero(getItem14.Month7) + r7.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem15 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 15);
                        if (getItem15 != null)
                        {
                            var r8 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 8);
                            if (r8 != null)
                            {
                                getItem15.Month7 = (Funs.GetNewIntOrZero(getItem15.Month7) + r8.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem16 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 16);
                        if (getItem16 != null)
                        {
                            var r9 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 9);
                            if (r9 != null)
                            {
                                getItem16.Month7 = (Funs.GetNewIntOrZero(getItem16.Month7) + r9.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem17 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 17);
                        if (getItem17 != null)
                        {
                            var r10 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 10);
                            if (r10 != null)
                            {
                                getItem17.Month7 = (Funs.GetNewIntOrZero(getItem17.Month7) + r10.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem18 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 18);
                        if (getItem18 != null)
                        {
                            var r11 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 11);
                            if (r11 != null)
                            {
                                getItem18.Month7 = (Funs.GetNewIntOrZero(getItem18.Month7) + r11.MonthTimes ?? 0).ToString();
                            }
                        }
                    }
                    ////大型、特种设备
                    var getSeDin_MonthReport5 = from x in db.SeDin_MonthReport5
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    var getItem19 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 19);
                    if (getSeDin_MonthReport5.Count() > 0 && getItem19 != null)
                    {
                        int countSumR5 = getSeDin_MonthReport5.Sum(x => x.T01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.T04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T05 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T06 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.S01 ?? 0);
                        getItem19.Month7 = (Funs.GetNewIntOrZero(getItem19.Month7) + countSumR5).ToString();
                    }
                    ////安全生产费用
                    var getSeDin_MonthReport6 = db.SeDin_MonthReport6.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport6 != null)
                    {
                        var getItem20 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 20);
                        if (getItem20 != null)
                        {
                            getItem20.Month7 = (Funs.GetNewDecimalOrZero(getItem20.Month7) + getSeDin_MonthReport6.SumMonth ?? 0).ToString();
                        }
                    }
                    ////项目现场员工入场安全培训人数
                    var getSeDin_MonthReport7 = db.SeDin_MonthReport7.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport7 != null)
                    {
                        var getItem21 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 21);
                        if (getItem21 != null)
                        {
                            getItem21.Month7 = (Funs.GetNewIntOrZero(getItem21.Month7) + getSeDin_MonthReport7.EmployeeMontPerson ?? 0).ToString();
                        }
                    }
                    ////项目会次
                    var getSeDin_MonthReport8 = db.SeDin_MonthReport8.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport8 != null)
                    {
                        var getItem22 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 22);
                        if (getItem22 != null)
                        {
                            getItem22.Month7 = (Funs.GetNewIntOrZero(getItem22.Month7) + getSeDin_MonthReport8.MonthMontNum ?? 0).ToString();
                        }
                        var getItem23 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 23);
                        if (getItem23 != null)
                        {
                            getItem23.Month7 = (Funs.GetNewIntOrZero(getItem23.Month7) + getSeDin_MonthReport8.WeekMontNum ?? 0).ToString();
                        }
                        var getItem24 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 24);
                        if (getItem24 != null)
                        {
                            getItem24.Month7 = (Funs.GetNewIntOrZero(getItem24.Month7) + getSeDin_MonthReport8.SpecialMontPerson ?? 0).ToString();
                        }
                    }
                    /// 安全检查
                    var getSeDin_MonthReport9 = db.SeDin_MonthReport9.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport9 != null)
                    {
                        var getItem25 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 25);
                        if (getItem25 != null)
                        {
                            getItem25.Month7 = (Funs.GetNewIntOrZero(getItem25.Month7) + getSeDin_MonthReport9.DailyMonth ?? 0).ToString();
                        }
                        var getItem26 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 26);
                        if (getItem26 != null)
                        {
                            getItem26.Month7 = (Funs.GetNewIntOrZero(getItem26.Month7) + getSeDin_MonthReport9.WeekMonth ?? 0).ToString();
                        }
                        var getItem27 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 27);
                        if (getItem27 != null)
                        {
                            getItem27.Month7 = (Funs.GetNewIntOrZero(getItem27.Month7) + getSeDin_MonthReport9.SpecialMonth ?? 0).ToString();
                        }
                        var getItem28 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 28);
                        if (getItem28 != null)
                        {
                            getItem28.Month7 = (Funs.GetNewIntOrZero(getItem28.Month7) + getSeDin_MonthReport9.MonthlyMonth ?? 0).ToString();
                        }
                    }
                    /////隐患整改单
                    var getSeDin_MonthReport9Rectification = from x in db.SeDin_MonthReport9Item_Rectification where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Rectification.Count() > 0)
                    {
                        var getItem29 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 29);
                        if (getItem29 != null)
                        {
                            getItem29.Month7 = (Funs.GetNewIntOrZero(getItem29.Month7) + getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                        var getItem30 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 30);
                        if (getItem30 != null)
                        {
                            getItem30.Month7 = (Funs.GetNewIntOrZero(getItem30.Month7) + getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMoth ?? 0)).ToString();
                        }
                        var getItem31 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 31);
                        if (getItem31 != null)
                        {
                            getItem31.Month7 = (Funs.GetNewIntOrZero(getItem31.Month7) + getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthLarge ?? 0)).ToString();
                        }
                        var getItem32 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 32);
                        if (getItem32 != null)
                        {
                            getItem32.Month7 = (Funs.GetNewIntOrZero(getItem32.Month7) + getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothLarge ?? 0)).ToString();
                        }
                        var getItem33 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 33);
                        if (getItem33 != null)
                        {
                            getItem33.Month7 = (Funs.GetNewIntOrZero(getItem33.Month7) + getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthSerious ?? 0)).ToString();
                        }
                        var getItem34 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 34);
                        if (getItem34 != null)
                        {
                            getItem34.Month7 = (Funs.GetNewIntOrZero(getItem34.Month7) + getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothSerious ?? 0)).ToString();
                        }
                    }
                    /////停工令
                    var getSeDin_MonthReport9Stoppage = from x in db.SeDin_MonthReport9Item_Stoppage where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Stoppage.Count() > 0)
                    {
                        var getItem35 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 35);
                        if (getItem35 != null)
                        {
                            getItem35.Month7 = (Funs.GetNewIntOrZero(getItem35.Month7) + getSeDin_MonthReport9Stoppage.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                    }
                    ////奖惩
                    var getSeDin_MonthReport10 = db.SeDin_MonthReport10.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport10 != null)
                    {
                        var getItem36 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 36);
                        if (getItem36 != null)
                        {
                            getItem36.Month7 = (Funs.GetNewDecimalOrZero(getItem36.Month7) + getSeDin_MonthReport10.SafeMonthMoney ?? 0 + getSeDin_MonthReport10.HseMonthMoney ?? 0 + getSeDin_MonthReport10.ProduceMonthMoney ?? 0).ToString();
                        }
                        var getItem37 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 37);
                        if (getItem37 != null)
                        {
                            getItem37.Month7 = (Funs.GetNewDecimalOrZero(getItem37.Month7) + getSeDin_MonthReport10.AccidentMonthMoney ?? 0 + getSeDin_MonthReport10.ViolationMonthMoney ?? 0 + getSeDin_MonthReport10.ManageMonthMoney ?? 0).ToString();
                        }
                    }
                    ////危大工程
                    var getSeDin_MonthReport11 = db.SeDin_MonthReport11.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport11 != null)
                    {
                        var getItem38 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 38);
                        if (getItem38 != null)
                        {
                            getItem38.Month7 = (Funs.GetNewIntOrZero(getItem38.Month7) + getSeDin_MonthReport11.RiskWorkNum ?? 0).ToString();
                        }
                        var getItem39 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 39);
                        if (getItem39 != null)
                        {
                            getItem39.Month7 = (Funs.GetNewIntOrZero(getItem39.Month7) + getSeDin_MonthReport11.LargeWorkNum ?? 0).ToString();
                        }
                    }
                    ////应急
                    var getSeDin_MonthReport12 = db.SeDin_MonthReport12.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport12 != null)
                    {
                        var getItem40 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 40);
                        if (getItem40 != null)
                        {
                            getItem40.Month7 = (Funs.GetNewIntOrZero(getItem40.Month7) + getSeDin_MonthReport12.MultipleSiteNum ?? 0 + getSeDin_MonthReport12.MultipleDesktopNum ?? 0
                                + getSeDin_MonthReport12.SingleSiteNum ?? 0 + getSeDin_MonthReport12.SingleDesktopNum ?? 0).ToString();
                        }
                    }
                    db.SubmitChanges();
                }
                #endregion
            }
            else if (monthReport.ReporMonth.Value.Month == 8)
            {
                #region 提交情况
                bool isSave = true;
                var getSubmissionMonth = getHSEDataCollectISubmissionYear.FirstOrDefault(x => x.ProjectId == monthReport.ProjectId);
                if (getSubmissionMonth == null)
                {
                    Model.DigData_HSEDataCollectSubmission newSubmission = new Model.DigData_HSEDataCollectSubmission()
                    {
                        HSEDataCollectSubmissionId = SQLHelper.GetNewID(),
                        HSEDataCollectId = getHSEDataCollectId,
                        Year = monthReport.ReporMonth.Value.Year,
                        ProjectId = monthReport.ProjectId,
                        Month8 = string.Format("{0:yyyy-MM-dd}", (monthReport.CommitTime ?? DateTime.Now)),
                    };
                    db.DigData_HSEDataCollectSubmission.InsertOnSubmit(newSubmission);
                    db.SubmitChanges();
                }
                else
                {
                    if (!string.IsNullOrEmpty(getSubmissionMonth.Month8))
                    {
                        isSave = false;
                    }
                    else
                    {
                        getSubmissionMonth.Month8 = string.Format("{0:yyyy-MM-dd}", (monthReport.CommitTime ?? DateTime.Now));
                        db.SubmitChanges();
                    }
                }
                #endregion
                #region 数据汇总
                if (isSave)
                {
                    ////员工
                    var getSeDin_MonthReport4Other = db.SeDin_MonthReport4Other.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport4Other != null)
                    {
                        var getItem1 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 1);
                        if (getItem1 != null)
                        {
                            getItem1.Month8 = (Funs.GetNewIntOrZero(getItem1.Month8) + getSeDin_MonthReport4Other.TotalNum ?? 0).ToString();
                        }
                        var getItem2 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 2);
                        if (getItem2 != null)
                        {
                            getItem2.Month8 = (Funs.GetNewIntOrZero(getItem2.Month8) + getSeDin_MonthReport4Other.OutsideNum ?? 0).ToString();
                        }
                        var getItem3 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 3);
                        if (getItem3 != null)
                        {
                            getItem3.Month8 = (Funs.GetNewIntOrZero(getItem3.Month8) + getSeDin_MonthReport4Other.ForeignNum ?? 0).ToString();
                        }
                        var getItem4 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 4);
                        if (getItem4 != null)
                        {
                            getItem4.Month8 = (Funs.GetNewIntOrZero(getItem4.Month8) + getSeDin_MonthReport4Other.ManagerNum ?? 0).ToString();
                        }
                    }
                    ////分包人数
                    var getSeDin_MonthReport4 = from x in db.SeDin_MonthReport4
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport4.Count() > 0)
                    {
                        var getItem5 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 5);
                        if (getItem5 != null)
                        {
                            getItem5.Month8 = (Funs.GetNewIntOrZero(getItem5.Month8) + getSeDin_MonthReport4.Sum(x => x.TotalNum ?? 0)).ToString();
                        }
                        var getItem6 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 6);
                        if (getItem6 != null)
                        {
                            getItem6.Month8 = (Funs.GetNewIntOrZero(getItem6.Month8) + getSeDin_MonthReport4.Sum(x => x.SafeManangerNum ?? 0)).ToString();
                        }
                    }
                    ///安全人工时
                    var getSeDin_MonthReport2 = db.SeDin_MonthReport2.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport2 != null)
                    {
                        var getItem7 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 7);
                        if (getItem7 != null)
                        {
                            getItem7.Month8 = (Funs.GetNewDecimalOrZero(getItem7.Month8) + getSeDin_MonthReport2.MonthWorkTime ?? 0).ToString();
                        }
                    }
                    ////事故
                    var getSeDin_MonthReport3 = from x in db.SeDin_MonthReport3
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport3.Count() > 0)
                    {
                        var getItem8 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 8);
                        if (getItem8 != null)
                        {
                            var r1 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 1);
                            if (r1 != null)
                            {
                                getItem8.Month8 = (Funs.GetNewIntOrZero(getItem8.Month8) + r1.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem9 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 9);
                        if (getItem9 != null)
                        {
                            var r2 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 2);
                            if (r2 != null)
                            {
                                getItem9.Month8 = (Funs.GetNewIntOrZero(getItem9.Month8) + r2.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem10 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 10);
                        if (getItem10 != null)
                        {
                            var r3 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 3);
                            if (r3 != null)
                            {
                                getItem10.Month8 = (Funs.GetNewIntOrZero(getItem10.Month8) + r3.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem11 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 11);
                        if (getItem11 != null)
                        {
                            var r4 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 4);
                            if (r4 != null)
                            {
                                getItem11.Month8 = (Funs.GetNewIntOrZero(getItem11.Month8) + r4.MonthPersons ?? 0).ToString();
                            }
                        }

                        var getItem12 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 12);
                        if (getItem12 != null)
                        {
                            var r5 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 5);
                            if (r5 != null)
                            {
                                getItem12.Month8 = (Funs.GetNewIntOrZero(getItem12.Month8) + r5.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem13 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 13);
                        if (getItem13 != null)
                        {
                            var r6 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 6);
                            if (r6 != null)
                            {
                                getItem13.Month8 = (Funs.GetNewIntOrZero(getItem13.Month8) + r6.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem14 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 14);
                        if (getItem14 != null)
                        {
                            var r7 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 7);
                            if (r7 != null)
                            {
                                getItem14.Month8 = (Funs.GetNewIntOrZero(getItem14.Month8) + r7.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem15 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 15);
                        if (getItem15 != null)
                        {
                            var r8 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 8);
                            if (r8 != null)
                            {
                                getItem15.Month8 = (Funs.GetNewIntOrZero(getItem15.Month8) + r8.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem16 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 16);
                        if (getItem16 != null)
                        {
                            var r9 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 9);
                            if (r9 != null)
                            {
                                getItem16.Month8 = (Funs.GetNewIntOrZero(getItem16.Month8) + r9.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem17 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 17);
                        if (getItem17 != null)
                        {
                            var r10 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 10);
                            if (r10 != null)
                            {
                                getItem17.Month8 = (Funs.GetNewIntOrZero(getItem17.Month8) + r10.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem18 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 18);
                        if (getItem18 != null)
                        {
                            var r11 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 11);
                            if (r11 != null)
                            {
                                getItem18.Month8 = (Funs.GetNewIntOrZero(getItem18.Month8) + r11.MonthTimes ?? 0).ToString();
                            }
                        }
                    }
                    ////大型、特种设备
                    var getSeDin_MonthReport5 = from x in db.SeDin_MonthReport5
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    var getItem19 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 19);
                    if (getSeDin_MonthReport5.Count() > 0 && getItem19 != null)
                    {
                        int countSumR5 = getSeDin_MonthReport5.Sum(x => x.T01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.T04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T05 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T06 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.S01 ?? 0);
                        getItem19.Month8 = (Funs.GetNewIntOrZero(getItem19.Month8) + countSumR5).ToString();
                    }
                    ////安全生产费用
                    var getSeDin_MonthReport6 = db.SeDin_MonthReport6.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport6 != null)
                    {
                        var getItem20 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 20);
                        if (getItem20 != null)
                        {
                            getItem20.Month8 = (Funs.GetNewDecimalOrZero(getItem20.Month8) + getSeDin_MonthReport6.SumMonth ?? 0).ToString();
                        }
                    }
                    ////项目现场员工入场安全培训人数
                    var getSeDin_MonthReport7 = db.SeDin_MonthReport7.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport7 != null)
                    {
                        var getItem21 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 21);
                        if (getItem21 != null)
                        {
                            getItem21.Month8 = (Funs.GetNewIntOrZero(getItem21.Month8) + getSeDin_MonthReport7.EmployeeMontPerson ?? 0).ToString();
                        }
                    }
                    ////项目会次
                    var getSeDin_MonthReport8 = db.SeDin_MonthReport8.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport8 != null)
                    {
                        var getItem22 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 22);
                        if (getItem22 != null)
                        {
                            getItem22.Month8 = (Funs.GetNewIntOrZero(getItem22.Month8) + getSeDin_MonthReport8.MonthMontNum ?? 0).ToString();
                        }
                        var getItem23 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 23);
                        if (getItem23 != null)
                        {
                            getItem23.Month8 = (Funs.GetNewIntOrZero(getItem23.Month8) + getSeDin_MonthReport8.WeekMontNum ?? 0).ToString();
                        }
                        var getItem24 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 24);
                        if (getItem24 != null)
                        {
                            getItem24.Month8 = (Funs.GetNewIntOrZero(getItem24.Month8) + getSeDin_MonthReport8.SpecialMontPerson ?? 0).ToString();
                        }
                    }
                    /// 安全检查
                    var getSeDin_MonthReport9 = db.SeDin_MonthReport9.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport9 != null)
                    {
                        var getItem25 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 25);
                        if (getItem25 != null)
                        {
                            getItem25.Month8 = (Funs.GetNewIntOrZero(getItem25.Month8) + getSeDin_MonthReport9.DailyMonth ?? 0).ToString();
                        }
                        var getItem26 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 26);
                        if (getItem26 != null)
                        {
                            getItem26.Month8 = (Funs.GetNewIntOrZero(getItem26.Month8) + getSeDin_MonthReport9.WeekMonth ?? 0).ToString();
                        }
                        var getItem27 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 27);
                        if (getItem27 != null)
                        {
                            getItem27.Month8 = (Funs.GetNewIntOrZero(getItem27.Month8) + getSeDin_MonthReport9.SpecialMonth ?? 0).ToString();
                        }
                        var getItem28 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 28);
                        if (getItem28 != null)
                        {
                            getItem28.Month8 = (Funs.GetNewIntOrZero(getItem28.Month8) + getSeDin_MonthReport9.MonthlyMonth ?? 0).ToString();
                        }
                    }
                    /////隐患整改单
                    var getSeDin_MonthReport9Rectification = from x in db.SeDin_MonthReport9Item_Rectification where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Rectification.Count() > 0)
                    {
                        var getItem29 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 29);
                        if (getItem29 != null)
                        {
                            getItem29.Month8 = (Funs.GetNewIntOrZero(getItem29.Month8) + getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                        var getItem30 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 30);
                        if (getItem30 != null)
                        {
                            getItem30.Month8 = (Funs.GetNewIntOrZero(getItem30.Month8) + getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMoth ?? 0)).ToString();
                        }
                        var getItem31 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 31);
                        if (getItem31 != null)
                        {
                            getItem31.Month8 = (Funs.GetNewIntOrZero(getItem31.Month8) + getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthLarge ?? 0)).ToString();
                        }
                        var getItem32 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 32);
                        if (getItem32 != null)
                        {
                            getItem32.Month8 = (Funs.GetNewIntOrZero(getItem32.Month8) + getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothLarge ?? 0)).ToString();
                        }
                        var getItem33 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 33);
                        if (getItem33 != null)
                        {
                            getItem33.Month8 = (Funs.GetNewIntOrZero(getItem33.Month8) + getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthSerious ?? 0)).ToString();
                        }
                        var getItem34 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 34);
                        if (getItem34 != null)
                        {
                            getItem34.Month8 = (Funs.GetNewIntOrZero(getItem34.Month8) + getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothSerious ?? 0)).ToString();
                        }
                    }
                    /////停工令
                    var getSeDin_MonthReport9Stoppage = from x in db.SeDin_MonthReport9Item_Stoppage where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Stoppage.Count() > 0)
                    {
                        var getItem35 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 35);
                        if (getItem35 != null)
                        {
                            getItem35.Month8 = (Funs.GetNewIntOrZero(getItem35.Month8) + getSeDin_MonthReport9Stoppage.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                    }
                    ////奖惩
                    var getSeDin_MonthReport10 = db.SeDin_MonthReport10.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport10 != null)
                    {
                        var getItem36 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 36);
                        if (getItem36 != null)
                        {
                            getItem36.Month8 = (Funs.GetNewDecimalOrZero(getItem36.Month8) + getSeDin_MonthReport10.SafeMonthMoney ?? 0 + getSeDin_MonthReport10.HseMonthMoney ?? 0 + getSeDin_MonthReport10.ProduceMonthMoney ?? 0).ToString();
                        }
                        var getItem37 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 37);
                        if (getItem37 != null)
                        {
                            getItem37.Month8 = (Funs.GetNewDecimalOrZero(getItem37.Month8) + getSeDin_MonthReport10.AccidentMonthMoney ?? 0 + getSeDin_MonthReport10.ViolationMonthMoney ?? 0 + getSeDin_MonthReport10.ManageMonthMoney ?? 0).ToString();
                        }
                    }
                    ////危大工程
                    var getSeDin_MonthReport11 = db.SeDin_MonthReport11.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport11 != null)
                    {
                        var getItem38 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 38);
                        if (getItem38 != null)
                        {
                            getItem38.Month8 = (Funs.GetNewIntOrZero(getItem38.Month8) + getSeDin_MonthReport11.RiskWorkNum ?? 0).ToString();
                        }
                        var getItem39 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 39);
                        if (getItem39 != null)
                        {
                            getItem39.Month8 = (Funs.GetNewIntOrZero(getItem39.Month8) + getSeDin_MonthReport11.LargeWorkNum ?? 0).ToString();
                        }
                    }
                    ////应急
                    var getSeDin_MonthReport12 = db.SeDin_MonthReport12.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport12 != null)
                    {
                        var getItem40 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 40);
                        if (getItem40 != null)
                        {
                            getItem40.Month8 = (Funs.GetNewIntOrZero(getItem40.Month8) + getSeDin_MonthReport12.MultipleSiteNum ?? 0 + getSeDin_MonthReport12.MultipleDesktopNum ?? 0
                                + getSeDin_MonthReport12.SingleSiteNum ?? 0 + getSeDin_MonthReport12.SingleDesktopNum ?? 0).ToString();
                        }
                    }
                    db.SubmitChanges();
                }
                #endregion
            }
            else if (monthReport.ReporMonth.Value.Month == 9)
            {
                #region 提交情况
                bool isSave = true;
                var getSubmissionMonth = getHSEDataCollectISubmissionYear.FirstOrDefault(x => x.ProjectId == monthReport.ProjectId);
                if (getSubmissionMonth == null)
                {
                    Model.DigData_HSEDataCollectSubmission newSubmission = new Model.DigData_HSEDataCollectSubmission()
                    {
                        HSEDataCollectSubmissionId = SQLHelper.GetNewID(),
                        HSEDataCollectId = getHSEDataCollectId,
                        Year = monthReport.ReporMonth.Value.Year,
                        ProjectId = monthReport.ProjectId,
                        Month9 = string.Format("{0:yyyy-MM-dd}", (monthReport.CommitTime ?? DateTime.Now)),
                    };
                    db.DigData_HSEDataCollectSubmission.InsertOnSubmit(newSubmission);
                    db.SubmitChanges();
                }
                else
                {
                    if (!string.IsNullOrEmpty(getSubmissionMonth.Month9))
                    {
                        isSave = false;
                    }
                    else
                    {
                        getSubmissionMonth.Month9 = string.Format("{0:yyyy-MM-dd}", (monthReport.CommitTime ?? DateTime.Now));
                        db.SubmitChanges();
                    }
                }
                #endregion
                #region 数据汇总
                if (isSave)
                {
                    ////员工
                    var getSeDin_MonthReport4Other = db.SeDin_MonthReport4Other.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport4Other != null)
                    {
                        var getItem1 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 1);
                        if (getItem1 != null)
                        {
                            getItem1.Month9 = (Funs.GetNewIntOrZero(getItem1.Month9) + getSeDin_MonthReport4Other.TotalNum ?? 0).ToString();
                        }
                        var getItem2 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 2);
                        if (getItem2 != null)
                        {
                            getItem2.Month9 = (Funs.GetNewIntOrZero(getItem2.Month9) + getSeDin_MonthReport4Other.OutsideNum ?? 0).ToString();
                        }
                        var getItem3 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 3);
                        if (getItem3 != null)
                        {
                            getItem3.Month9 = (Funs.GetNewIntOrZero(getItem3.Month9) + getSeDin_MonthReport4Other.ForeignNum ?? 0).ToString();
                        }
                        var getItem4 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 4);
                        if (getItem4 != null)
                        {
                            getItem4.Month9 = (Funs.GetNewIntOrZero(getItem4.Month9) + getSeDin_MonthReport4Other.ManagerNum ?? 0).ToString();
                        }
                    }
                    ////分包人数
                    var getSeDin_MonthReport4 = from x in db.SeDin_MonthReport4
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport4.Count() > 0)
                    {
                        var getItem5 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 5);
                        if (getItem5 != null)
                        {
                            getItem5.Month9 = (Funs.GetNewIntOrZero(getItem5.Month9) + getSeDin_MonthReport4.Sum(x => x.TotalNum ?? 0)).ToString();
                        }
                        var getItem6 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 6);
                        if (getItem6 != null)
                        {
                            getItem6.Month9 = (Funs.GetNewIntOrZero(getItem6.Month9) + getSeDin_MonthReport4.Sum(x => x.SafeManangerNum ?? 0)).ToString();
                        }
                    }
                    ///安全人工时
                    var getSeDin_MonthReport2 = db.SeDin_MonthReport2.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport2 != null)
                    {
                        var getItem7 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 7);
                        if (getItem7 != null)
                        {
                            getItem7.Month9 = (Funs.GetNewDecimalOrZero(getItem7.Month9) + getSeDin_MonthReport2.MonthWorkTime ?? 0).ToString();
                        }
                    }
                    ////事故
                    var getSeDin_MonthReport3 = from x in db.SeDin_MonthReport3
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport3.Count() > 0)
                    {
                        var getItem8 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 8);
                        if (getItem8 != null)
                        {
                            var r1 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 1);
                            if (r1 != null)
                            {
                                getItem8.Month9 = (Funs.GetNewIntOrZero(getItem8.Month9) + r1.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem9 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 9);
                        if (getItem9 != null)
                        {
                            var r2 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 2);
                            if (r2 != null)
                            {
                                getItem9.Month9 = (Funs.GetNewIntOrZero(getItem9.Month9) + r2.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem10 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 10);
                        if (getItem10 != null)
                        {
                            var r3 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 3);
                            if (r3 != null)
                            {
                                getItem10.Month9 = (Funs.GetNewIntOrZero(getItem10.Month9) + r3.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem11 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 11);
                        if (getItem11 != null)
                        {
                            var r4 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 4);
                            if (r4 != null)
                            {
                                getItem11.Month9 = (Funs.GetNewIntOrZero(getItem11.Month9) + r4.MonthPersons ?? 0).ToString();
                            }
                        }

                        var getItem12 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 12);
                        if (getItem12 != null)
                        {
                            var r5 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 5);
                            if (r5 != null)
                            {
                                getItem12.Month9 = (Funs.GetNewIntOrZero(getItem12.Month9) + r5.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem13 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 13);
                        if (getItem13 != null)
                        {
                            var r6 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 6);
                            if (r6 != null)
                            {
                                getItem13.Month9 = (Funs.GetNewIntOrZero(getItem13.Month9) + r6.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem14 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 14);
                        if (getItem14 != null)
                        {
                            var r7 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 7);
                            if (r7 != null)
                            {
                                getItem14.Month9 = (Funs.GetNewIntOrZero(getItem14.Month9) + r7.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem15 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 15);
                        if (getItem15 != null)
                        {
                            var r8 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 8);
                            if (r8 != null)
                            {
                                getItem15.Month9 = (Funs.GetNewIntOrZero(getItem15.Month9) + r8.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem16 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 16);
                        if (getItem16 != null)
                        {
                            var r9 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 9);
                            if (r9 != null)
                            {
                                getItem16.Month9 = (Funs.GetNewIntOrZero(getItem16.Month9) + r9.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem17 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 17);
                        if (getItem17 != null)
                        {
                            var r10 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 10);
                            if (r10 != null)
                            {
                                getItem17.Month9 = (Funs.GetNewIntOrZero(getItem17.Month9) + r10.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem18 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 18);
                        if (getItem18 != null)
                        {
                            var r11 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 11);
                            if (r11 != null)
                            {
                                getItem18.Month9 = (Funs.GetNewIntOrZero(getItem18.Month9) + r11.MonthTimes ?? 0).ToString();
                            }
                        }
                    }
                    ////大型、特种设备
                    var getSeDin_MonthReport5 = from x in db.SeDin_MonthReport5
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    var getItem19 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 19);
                    if (getSeDin_MonthReport5.Count() > 0 && getItem19 != null)
                    {
                        int countSumR5 = getSeDin_MonthReport5.Sum(x => x.T01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.T04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T05 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T06 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.S01 ?? 0);
                        getItem19.Month9 = (Funs.GetNewIntOrZero(getItem19.Month9) + countSumR5).ToString();
                    }
                    ////安全生产费用
                    var getSeDin_MonthReport6 = db.SeDin_MonthReport6.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport6 != null)
                    {
                        var getItem20 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 20);
                        if (getItem20 != null)
                        {
                            getItem20.Month9 = (Funs.GetNewDecimalOrZero(getItem20.Month9) + getSeDin_MonthReport6.SumMonth ?? 0).ToString();
                        }
                    }
                    ////项目现场员工入场安全培训人数
                    var getSeDin_MonthReport7 = db.SeDin_MonthReport7.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport7 != null)
                    {
                        var getItem21 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 21);
                        if (getItem21 != null)
                        {
                            getItem21.Month9 = (Funs.GetNewIntOrZero(getItem21.Month9) + getSeDin_MonthReport7.EmployeeMontPerson ?? 0).ToString();
                        }
                    }
                    ////项目会次
                    var getSeDin_MonthReport8 = db.SeDin_MonthReport8.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport8 != null)
                    {
                        var getItem22 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 22);
                        if (getItem22 != null)
                        {
                            getItem22.Month9 = (Funs.GetNewIntOrZero(getItem22.Month9) + getSeDin_MonthReport8.MonthMontNum ?? 0).ToString();
                        }
                        var getItem23 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 23);
                        if (getItem23 != null)
                        {
                            getItem23.Month9 = (Funs.GetNewIntOrZero(getItem23.Month9) + getSeDin_MonthReport8.WeekMontNum ?? 0).ToString();
                        }
                        var getItem24 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 24);
                        if (getItem24 != null)
                        {
                            getItem24.Month9 = (Funs.GetNewIntOrZero(getItem24.Month9) + getSeDin_MonthReport8.SpecialMontPerson ?? 0).ToString();
                        }
                    }
                    /// 安全检查
                    var getSeDin_MonthReport9 = db.SeDin_MonthReport9.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport9 != null)
                    {
                        var getItem25 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 25);
                        if (getItem25 != null)
                        {
                            getItem25.Month9 = (Funs.GetNewIntOrZero(getItem25.Month9) + getSeDin_MonthReport9.DailyMonth ?? 0).ToString();
                        }
                        var getItem26 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 26);
                        if (getItem26 != null)
                        {
                            getItem26.Month9 = (Funs.GetNewIntOrZero(getItem26.Month9) + getSeDin_MonthReport9.WeekMonth ?? 0).ToString();
                        }
                        var getItem27 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 27);
                        if (getItem27 != null)
                        {
                            getItem27.Month9 = (Funs.GetNewIntOrZero(getItem27.Month9) + getSeDin_MonthReport9.SpecialMonth ?? 0).ToString();
                        }
                        var getItem28 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 28);
                        if (getItem28 != null)
                        {
                            getItem28.Month9 = (Funs.GetNewIntOrZero(getItem28.Month9) + getSeDin_MonthReport9.MonthlyMonth ?? 0).ToString();
                        }
                    }
                    /////隐患整改单
                    var getSeDin_MonthReport9Rectification = from x in db.SeDin_MonthReport9Item_Rectification where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Rectification.Count() > 0)
                    {
                        var getItem29 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 29);
                        if (getItem29 != null)
                        {
                            getItem29.Month9 = (Funs.GetNewIntOrZero(getItem29.Month9) + getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                        var getItem30 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 30);
                        if (getItem30 != null)
                        {
                            getItem30.Month9 = (Funs.GetNewIntOrZero(getItem30.Month9) + getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMoth ?? 0)).ToString();
                        }
                        var getItem31 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 31);
                        if (getItem31 != null)
                        {
                            getItem31.Month9 = (Funs.GetNewIntOrZero(getItem31.Month9) + getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthLarge ?? 0)).ToString();
                        }
                        var getItem32 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 32);
                        if (getItem32 != null)
                        {
                            getItem32.Month9 = (Funs.GetNewIntOrZero(getItem32.Month9) + getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothLarge ?? 0)).ToString();
                        }
                        var getItem33 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 33);
                        if (getItem33 != null)
                        {
                            getItem33.Month9 = (Funs.GetNewIntOrZero(getItem33.Month9) + getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthSerious ?? 0)).ToString();
                        }
                        var getItem34 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 34);
                        if (getItem34 != null)
                        {
                            getItem34.Month9 = (Funs.GetNewIntOrZero(getItem34.Month9) + getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothSerious ?? 0)).ToString();
                        }
                    }
                    /////停工令
                    var getSeDin_MonthReport9Stoppage = from x in db.SeDin_MonthReport9Item_Stoppage where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Stoppage.Count() > 0)
                    {
                        var getItem35 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 35);
                        if (getItem35 != null)
                        {
                            getItem35.Month9 = (Funs.GetNewIntOrZero(getItem35.Month9) + getSeDin_MonthReport9Stoppage.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                    }
                    ////奖惩
                    var getSeDin_MonthReport10 = db.SeDin_MonthReport10.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport10 != null)
                    {
                        var getItem36 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 36);
                        if (getItem36 != null)
                        {
                            getItem36.Month9 = (Funs.GetNewDecimalOrZero(getItem36.Month9) + getSeDin_MonthReport10.SafeMonthMoney ?? 0 + getSeDin_MonthReport10.HseMonthMoney ?? 0 + getSeDin_MonthReport10.ProduceMonthMoney ?? 0).ToString();
                        }
                        var getItem37 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 37);
                        if (getItem37 != null)
                        {
                            getItem37.Month9 = (Funs.GetNewDecimalOrZero(getItem37.Month9) + getSeDin_MonthReport10.AccidentMonthMoney ?? 0 + getSeDin_MonthReport10.ViolationMonthMoney ?? 0 + getSeDin_MonthReport10.ManageMonthMoney ?? 0).ToString();
                        }
                    }
                    ////危大工程
                    var getSeDin_MonthReport11 = db.SeDin_MonthReport11.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport11 != null)
                    {
                        var getItem38 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 38);
                        if (getItem38 != null)
                        {
                            getItem38.Month9 = (Funs.GetNewIntOrZero(getItem38.Month9) + getSeDin_MonthReport11.RiskWorkNum ?? 0).ToString();
                        }
                        var getItem39 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 39);
                        if (getItem39 != null)
                        {
                            getItem39.Month9 = (Funs.GetNewIntOrZero(getItem39.Month9) + getSeDin_MonthReport11.LargeWorkNum ?? 0).ToString();
                        }
                    }
                    ////应急
                    var getSeDin_MonthReport12 = db.SeDin_MonthReport12.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport12 != null)
                    {
                        var getItem40 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 40);
                        if (getItem40 != null)
                        {
                            getItem40.Month9 = (Funs.GetNewIntOrZero(getItem40.Month9) + getSeDin_MonthReport12.MultipleSiteNum ?? 0 + getSeDin_MonthReport12.MultipleDesktopNum ?? 0
                                + getSeDin_MonthReport12.SingleSiteNum ?? 0 + getSeDin_MonthReport12.SingleDesktopNum ?? 0).ToString();
                        }
                    }
                    db.SubmitChanges();
                }
                #endregion
            }
            else if (monthReport.ReporMonth.Value.Month == 10)
            {
                #region 提交情况
                bool isSave = true;
                var getSubmissionMonth = getHSEDataCollectISubmissionYear.FirstOrDefault(x => x.ProjectId == monthReport.ProjectId);
                if (getSubmissionMonth == null)
                {
                    Model.DigData_HSEDataCollectSubmission newSubmission = new Model.DigData_HSEDataCollectSubmission()
                    {
                        HSEDataCollectSubmissionId = SQLHelper.GetNewID(),
                        HSEDataCollectId = getHSEDataCollectId,
                        Year = monthReport.ReporMonth.Value.Year,
                        ProjectId = monthReport.ProjectId,
                        Month10 = string.Format("{0:yyyy-MM-dd}", (monthReport.CommitTime ?? DateTime.Now)),
                    };
                    db.DigData_HSEDataCollectSubmission.InsertOnSubmit(newSubmission);
                    db.SubmitChanges();
                }
                else
                {
                    if (!string.IsNullOrEmpty(getSubmissionMonth.Month10))
                    {
                        isSave = false;
                    }
                    else
                    {
                        getSubmissionMonth.Month10 = string.Format("{0:yyyy-MM-dd}", (monthReport.CommitTime ?? DateTime.Now));
                        db.SubmitChanges();
                    }
                }
                #endregion
                #region 数据汇总
                if (isSave)
                {
                    ////员工
                    var getSeDin_MonthReport4Other = db.SeDin_MonthReport4Other.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport4Other != null)
                    {
                        var getItem1 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 1);
                        if (getItem1 != null)
                        {
                            getItem1.Month10 = (Funs.GetNewIntOrZero(getItem1.Month10) + getSeDin_MonthReport4Other.TotalNum ?? 0).ToString();
                        }
                        var getItem2 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 2);
                        if (getItem2 != null)
                        {
                            getItem2.Month10 = (Funs.GetNewIntOrZero(getItem2.Month10) + getSeDin_MonthReport4Other.OutsideNum ?? 0).ToString();
                        }
                        var getItem3 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 3);
                        if (getItem3 != null)
                        {
                            getItem3.Month10 = (Funs.GetNewIntOrZero(getItem3.Month10) + getSeDin_MonthReport4Other.ForeignNum ?? 0).ToString();
                        }
                        var getItem4 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 4);
                        if (getItem4 != null)
                        {
                            getItem4.Month10 = (Funs.GetNewIntOrZero(getItem4.Month10) + getSeDin_MonthReport4Other.ManagerNum ?? 0).ToString();
                        }
                    }
                    ////分包人数
                    var getSeDin_MonthReport4 = from x in db.SeDin_MonthReport4
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport4.Count() > 0)
                    {
                        var getItem5 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 5);
                        if (getItem5 != null)
                        {
                            getItem5.Month10 = (Funs.GetNewIntOrZero(getItem5.Month10) + getSeDin_MonthReport4.Sum(x => x.TotalNum ?? 0)).ToString();
                        }
                        var getItem6 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 6);
                        if (getItem6 != null)
                        {
                            getItem6.Month10 = (Funs.GetNewIntOrZero(getItem6.Month10) + getSeDin_MonthReport4.Sum(x => x.SafeManangerNum ?? 0)).ToString();
                        }
                    }
                    ///安全人工时
                    var getSeDin_MonthReport2 = db.SeDin_MonthReport2.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport2 != null)
                    {
                        var getItem7 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 7);
                        if (getItem7 != null)
                        {
                            getItem7.Month10 = (Funs.GetNewDecimalOrZero(getItem7.Month10) + getSeDin_MonthReport2.MonthWorkTime ?? 0).ToString();
                        }
                    }
                    ////事故
                    var getSeDin_MonthReport3 = from x in db.SeDin_MonthReport3
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport3.Count() > 0)
                    {
                        var getItem8 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 8);
                        if (getItem8 != null)
                        {
                            var r1 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 1);
                            if (r1 != null)
                            {
                                getItem8.Month10 = (Funs.GetNewIntOrZero(getItem8.Month10) + r1.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem9 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 9);
                        if (getItem9 != null)
                        {
                            var r2 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 2);
                            if (r2 != null)
                            {
                                getItem9.Month10 = (Funs.GetNewIntOrZero(getItem9.Month10) + r2.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem10 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 10);
                        if (getItem10 != null)
                        {
                            var r3 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 3);
                            if (r3 != null)
                            {
                                getItem10.Month10 = (Funs.GetNewIntOrZero(getItem10.Month10) + r3.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem11 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 11);
                        if (getItem11 != null)
                        {
                            var r4 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 4);
                            if (r4 != null)
                            {
                                getItem11.Month10 = (Funs.GetNewIntOrZero(getItem11.Month10) + r4.MonthPersons ?? 0).ToString();
                            }
                        }

                        var getItem12 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 12);
                        if (getItem12 != null)
                        {
                            var r5 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 5);
                            if (r5 != null)
                            {
                                getItem12.Month10 = (Funs.GetNewIntOrZero(getItem12.Month10) + r5.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem13 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 13);
                        if (getItem13 != null)
                        {
                            var r6 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 6);
                            if (r6 != null)
                            {
                                getItem13.Month10 = (Funs.GetNewIntOrZero(getItem13.Month10) + r6.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem14 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 14);
                        if (getItem14 != null)
                        {
                            var r7 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 7);
                            if (r7 != null)
                            {
                                getItem14.Month10 = (Funs.GetNewIntOrZero(getItem14.Month10) + r7.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem15 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 15);
                        if (getItem15 != null)
                        {
                            var r8 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 8);
                            if (r8 != null)
                            {
                                getItem15.Month10 = (Funs.GetNewIntOrZero(getItem15.Month10) + r8.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem16 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 16);
                        if (getItem16 != null)
                        {
                            var r9 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 9);
                            if (r9 != null)
                            {
                                getItem16.Month10 = (Funs.GetNewIntOrZero(getItem16.Month10) + r9.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem17 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 17);
                        if (getItem17 != null)
                        {
                            var r10 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 10);
                            if (r10 != null)
                            {
                                getItem17.Month10 = (Funs.GetNewIntOrZero(getItem17.Month10) + r10.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem18 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 18);
                        if (getItem18 != null)
                        {
                            var r11 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 11);
                            if (r11 != null)
                            {
                                getItem18.Month10 = (Funs.GetNewIntOrZero(getItem18.Month10) + r11.MonthTimes ?? 0).ToString();
                            }
                        }
                    }
                    ////大型、特种设备
                    var getSeDin_MonthReport5 = from x in db.SeDin_MonthReport5
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    var getItem19 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 19);
                    if (getSeDin_MonthReport5.Count() > 0 && getItem19 != null)
                    {
                        int countSumR5 = getSeDin_MonthReport5.Sum(x => x.T01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.T04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T05 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T06 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.S01 ?? 0);
                        getItem19.Month10 = (Funs.GetNewIntOrZero(getItem19.Month10) + countSumR5).ToString();
                    }
                    ////安全生产费用
                    var getSeDin_MonthReport6 = db.SeDin_MonthReport6.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport6 != null)
                    {
                        var getItem20 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 20);
                        if (getItem20 != null)
                        {
                            getItem20.Month10 = (Funs.GetNewDecimalOrZero(getItem20.Month10) + getSeDin_MonthReport6.SumMonth ?? 0).ToString();
                        }
                    }
                    ////项目现场员工入场安全培训人数
                    var getSeDin_MonthReport7 = db.SeDin_MonthReport7.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport7 != null)
                    {
                        var getItem21 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 21);
                        if (getItem21 != null)
                        {
                            getItem21.Month10 = (Funs.GetNewIntOrZero(getItem21.Month10) + getSeDin_MonthReport7.EmployeeMontPerson ?? 0).ToString();
                        }
                    }
                    ////项目会次
                    var getSeDin_MonthReport8 = db.SeDin_MonthReport8.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport8 != null)
                    {
                        var getItem22 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 22);
                        if (getItem22 != null)
                        {
                            getItem22.Month10 = (Funs.GetNewIntOrZero(getItem22.Month10) + getSeDin_MonthReport8.MonthMontNum ?? 0).ToString();
                        }
                        var getItem23 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 23);
                        if (getItem23 != null)
                        {
                            getItem23.Month10 = (Funs.GetNewIntOrZero(getItem23.Month10) + getSeDin_MonthReport8.WeekMontNum ?? 0).ToString();
                        }
                        var getItem24 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 24);
                        if (getItem24 != null)
                        {
                            getItem24.Month10 = (Funs.GetNewIntOrZero(getItem24.Month10) + getSeDin_MonthReport8.SpecialMontPerson ?? 0).ToString();
                        }
                    }
                    /// 安全检查
                    var getSeDin_MonthReport9 = db.SeDin_MonthReport9.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport9 != null)
                    {
                        var getItem25 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 25);
                        if (getItem25 != null)
                        {
                            getItem25.Month10 = (Funs.GetNewIntOrZero(getItem25.Month10) + getSeDin_MonthReport9.DailyMonth ?? 0).ToString();
                        }
                        var getItem26 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 26);
                        if (getItem26 != null)
                        {
                            getItem26.Month10 = (Funs.GetNewIntOrZero(getItem26.Month10) + getSeDin_MonthReport9.WeekMonth ?? 0).ToString();
                        }
                        var getItem27 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 27);
                        if (getItem27 != null)
                        {
                            getItem27.Month10 = (Funs.GetNewIntOrZero(getItem27.Month10) + getSeDin_MonthReport9.SpecialMonth ?? 0).ToString();
                        }
                        var getItem28 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 28);
                        if (getItem28 != null)
                        {
                            getItem28.Month10 = (Funs.GetNewIntOrZero(getItem28.Month10) + getSeDin_MonthReport9.MonthlyMonth ?? 0).ToString();
                        }
                    }
                    /////隐患整改单
                    var getSeDin_MonthReport9Rectification = from x in db.SeDin_MonthReport9Item_Rectification where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Rectification.Count() > 0)
                    {
                        var getItem29 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 29);
                        if (getItem29 != null)
                        {
                            getItem29.Month10 = (Funs.GetNewIntOrZero(getItem29.Month10) + getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                        var getItem30 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 30);
                        if (getItem30 != null)
                        {
                            getItem30.Month10 = (Funs.GetNewIntOrZero(getItem30.Month10) + getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMoth ?? 0)).ToString();
                        }
                        var getItem31 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 31);
                        if (getItem31 != null)
                        {
                            getItem31.Month10 = (Funs.GetNewIntOrZero(getItem31.Month10) + getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthLarge ?? 0)).ToString();
                        }
                        var getItem32 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 32);
                        if (getItem32 != null)
                        {
                            getItem32.Month10 = (Funs.GetNewIntOrZero(getItem32.Month10) + getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothLarge ?? 0)).ToString();
                        }
                        var getItem33 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 33);
                        if (getItem33 != null)
                        {
                            getItem33.Month10 = (Funs.GetNewIntOrZero(getItem33.Month10) + getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthSerious ?? 0)).ToString();
                        }
                        var getItem34 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 34);
                        if (getItem34 != null)
                        {
                            getItem34.Month10 = (Funs.GetNewIntOrZero(getItem34.Month10) + getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothSerious ?? 0)).ToString();
                        }
                    }
                    /////停工令
                    var getSeDin_MonthReport9Stoppage = from x in db.SeDin_MonthReport9Item_Stoppage where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Stoppage.Count() > 0)
                    {
                        var getItem35 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 35);
                        if (getItem35 != null)
                        {
                            getItem35.Month10 = (Funs.GetNewIntOrZero(getItem35.Month10) + getSeDin_MonthReport9Stoppage.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                    }
                    ////奖惩
                    var getSeDin_MonthReport10 = db.SeDin_MonthReport10.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport10 != null)
                    {
                        var getItem36 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 36);
                        if (getItem36 != null)
                        {
                            getItem36.Month10 = (Funs.GetNewDecimalOrZero(getItem36.Month10) + getSeDin_MonthReport10.SafeMonthMoney ?? 0 + getSeDin_MonthReport10.HseMonthMoney ?? 0 + getSeDin_MonthReport10.ProduceMonthMoney ?? 0).ToString();
                        }
                        var getItem37 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 37);
                        if (getItem37 != null)
                        {
                            getItem37.Month10 = (Funs.GetNewDecimalOrZero(getItem37.Month10) + getSeDin_MonthReport10.AccidentMonthMoney ?? 0 + getSeDin_MonthReport10.ViolationMonthMoney ?? 0 + getSeDin_MonthReport10.ManageMonthMoney ?? 0).ToString();
                        }
                    }
                    ////危大工程
                    var getSeDin_MonthReport11 = db.SeDin_MonthReport11.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport11 != null)
                    {
                        var getItem38 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 38);
                        if (getItem38 != null)
                        {
                            getItem38.Month10 = (Funs.GetNewIntOrZero(getItem38.Month10) + getSeDin_MonthReport11.RiskWorkNum ?? 0).ToString();
                        }
                        var getItem39 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 39);
                        if (getItem39 != null)
                        {
                            getItem39.Month10 = (Funs.GetNewIntOrZero(getItem39.Month10) + getSeDin_MonthReport11.LargeWorkNum ?? 0).ToString();
                        }
                    }
                    ////应急
                    var getSeDin_MonthReport12 = db.SeDin_MonthReport12.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport12 != null)
                    {
                        var getItem40 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 40);
                        if (getItem40 != null)
                        {
                            getItem40.Month10 = (Funs.GetNewIntOrZero(getItem40.Month10) + getSeDin_MonthReport12.MultipleSiteNum ?? 0 + getSeDin_MonthReport12.MultipleDesktopNum ?? 0
                                + getSeDin_MonthReport12.SingleSiteNum ?? 0 + getSeDin_MonthReport12.SingleDesktopNum ?? 0).ToString();
                        }
                    }
                    db.SubmitChanges();
                }
                #endregion
            }
            else if (monthReport.ReporMonth.Value.Month == 11)
            {
                #region 提交情况
                bool isSave = true;
                var getSubmissionMonth = getHSEDataCollectISubmissionYear.FirstOrDefault(x => x.ProjectId == monthReport.ProjectId);
                if (getSubmissionMonth == null)
                {
                    Model.DigData_HSEDataCollectSubmission newSubmission = new Model.DigData_HSEDataCollectSubmission()
                    {
                        HSEDataCollectSubmissionId = SQLHelper.GetNewID(),
                        HSEDataCollectId = getHSEDataCollectId,
                        Year = monthReport.ReporMonth.Value.Year,
                        ProjectId = monthReport.ProjectId,
                        Month11 = string.Format("{0:yyyy-MM-dd}", (monthReport.CommitTime ?? DateTime.Now)),
                    };
                    db.DigData_HSEDataCollectSubmission.InsertOnSubmit(newSubmission);
                    db.SubmitChanges();
                }
                else
                {
                    if (!string.IsNullOrEmpty(getSubmissionMonth.Month11))
                    {
                        isSave = false;
                    }
                    else
                    {
                        getSubmissionMonth.Month11 = string.Format("{0:yyyy-MM-dd}", (monthReport.CommitTime ?? DateTime.Now));
                        db.SubmitChanges();
                    }
                }
                #endregion
                #region 数据汇总
                if (isSave)
                {
                    ////员工
                    var getSeDin_MonthReport4Other = db.SeDin_MonthReport4Other.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport4Other != null)
                    {
                        var getItem1 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 1);
                        if (getItem1 != null)
                        {
                            getItem1.Month11 = (Funs.GetNewIntOrZero(getItem1.Month11) + getSeDin_MonthReport4Other.TotalNum ?? 0).ToString();
                        }
                        var getItem2 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 2);
                        if (getItem2 != null)
                        {
                            getItem2.Month11 = (Funs.GetNewIntOrZero(getItem2.Month11) + getSeDin_MonthReport4Other.OutsideNum ?? 0).ToString();
                        }
                        var getItem3 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 3);
                        if (getItem3 != null)
                        {
                            getItem3.Month11 = (Funs.GetNewIntOrZero(getItem3.Month11) + getSeDin_MonthReport4Other.ForeignNum ?? 0).ToString();
                        }
                        var getItem4 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 4);
                        if (getItem4 != null)
                        {
                            getItem4.Month11 = (Funs.GetNewIntOrZero(getItem4.Month11) + getSeDin_MonthReport4Other.ManagerNum ?? 0).ToString();
                        }
                    }
                    ////分包人数
                    var getSeDin_MonthReport4 = from x in db.SeDin_MonthReport4
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport4.Count() > 0)
                    {
                        var getItem5 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 5);
                        if (getItem5 != null)
                        {
                            getItem5.Month11 = (Funs.GetNewIntOrZero(getItem5.Month11) + getSeDin_MonthReport4.Sum(x => x.TotalNum ?? 0)).ToString();
                        }
                        var getItem6 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 6);
                        if (getItem6 != null)
                        {
                            getItem6.Month11 = (Funs.GetNewIntOrZero(getItem6.Month11) + getSeDin_MonthReport4.Sum(x => x.SafeManangerNum ?? 0)).ToString();
                        }
                    }
                    ///安全人工时
                    var getSeDin_MonthReport2 = db.SeDin_MonthReport2.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport2 != null)
                    {
                        var getItem7 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 7);
                        if (getItem7 != null)
                        {
                            getItem7.Month11 = (Funs.GetNewDecimalOrZero(getItem7.Month11) + getSeDin_MonthReport2.MonthWorkTime ?? 0).ToString();
                        }
                    }
                    ////事故
                    var getSeDin_MonthReport3 = from x in db.SeDin_MonthReport3
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport3.Count() > 0)
                    {
                        var getItem8 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 8);
                        if (getItem8 != null)
                        {
                            var r1 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 1);
                            if (r1 != null)
                            {
                                getItem8.Month11 = (Funs.GetNewIntOrZero(getItem8.Month11) + r1.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem9 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 9);
                        if (getItem9 != null)
                        {
                            var r2 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 2);
                            if (r2 != null)
                            {
                                getItem9.Month11 = (Funs.GetNewIntOrZero(getItem9.Month11) + r2.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem10 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 10);
                        if (getItem10 != null)
                        {
                            var r3 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 3);
                            if (r3 != null)
                            {
                                getItem10.Month11 = (Funs.GetNewIntOrZero(getItem10.Month11) + r3.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem11 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 11);
                        if (getItem11 != null)
                        {
                            var r4 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 4);
                            if (r4 != null)
                            {
                                getItem11.Month11 = (Funs.GetNewIntOrZero(getItem11.Month11) + r4.MonthPersons ?? 0).ToString();
                            }
                        }

                        var getItem12 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 12);
                        if (getItem12 != null)
                        {
                            var r5 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 5);
                            if (r5 != null)
                            {
                                getItem12.Month11 = (Funs.GetNewIntOrZero(getItem12.Month11) + r5.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem13 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 13);
                        if (getItem13 != null)
                        {
                            var r6 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 6);
                            if (r6 != null)
                            {
                                getItem13.Month11 = (Funs.GetNewIntOrZero(getItem13.Month11) + r6.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem14 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 14);
                        if (getItem14 != null)
                        {
                            var r7 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 7);
                            if (r7 != null)
                            {
                                getItem14.Month11 = (Funs.GetNewIntOrZero(getItem14.Month11) + r7.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem15 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 15);
                        if (getItem15 != null)
                        {
                            var r8 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 8);
                            if (r8 != null)
                            {
                                getItem15.Month11 = (Funs.GetNewIntOrZero(getItem15.Month11) + r8.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem16 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 16);
                        if (getItem16 != null)
                        {
                            var r9 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 9);
                            if (r9 != null)
                            {
                                getItem16.Month11 = (Funs.GetNewIntOrZero(getItem16.Month11) + r9.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem17 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 17);
                        if (getItem17 != null)
                        {
                            var r10 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 10);
                            if (r10 != null)
                            {
                                getItem17.Month11 = (Funs.GetNewIntOrZero(getItem17.Month11) + r10.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem18 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 18);
                        if (getItem18 != null)
                        {
                            var r11 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 11);
                            if (r11 != null)
                            {
                                getItem18.Month11 = (Funs.GetNewIntOrZero(getItem18.Month11) + r11.MonthTimes ?? 0).ToString();
                            }
                        }
                    }
                    ////大型、特种设备
                    var getSeDin_MonthReport5 = from x in db.SeDin_MonthReport5
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    var getItem19 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 19);
                    if (getSeDin_MonthReport5.Count() > 0 && getItem19 != null)
                    {
                        int countSumR5 = getSeDin_MonthReport5.Sum(x => x.T01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.T04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T05 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T06 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.S01 ?? 0);
                        getItem19.Month11 = (Funs.GetNewIntOrZero(getItem19.Month11) + countSumR5).ToString();
                    }
                    ////安全生产费用
                    var getSeDin_MonthReport6 = db.SeDin_MonthReport6.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport6 != null)
                    {
                        var getItem20 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 20);
                        if (getItem20 != null)
                        {
                            getItem20.Month11 = (Funs.GetNewDecimalOrZero(getItem20.Month11) + getSeDin_MonthReport6.SumMonth ?? 0).ToString();
                        }
                    }
                    ////项目现场员工入场安全培训人数
                    var getSeDin_MonthReport7 = db.SeDin_MonthReport7.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport7 != null)
                    {
                        var getItem21 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 21);
                        if (getItem21 != null)
                        {
                            getItem21.Month11 = (Funs.GetNewIntOrZero(getItem21.Month11) + getSeDin_MonthReport7.EmployeeMontPerson ?? 0).ToString();
                        }
                    }
                    ////项目会次
                    var getSeDin_MonthReport8 = db.SeDin_MonthReport8.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport8 != null)
                    {
                        var getItem22 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 22);
                        if (getItem22 != null)
                        {
                            getItem22.Month11 = (Funs.GetNewIntOrZero(getItem22.Month11) + getSeDin_MonthReport8.MonthMontNum ?? 0).ToString();
                        }
                        var getItem23 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 23);
                        if (getItem23 != null)
                        {
                            getItem23.Month11 = (Funs.GetNewIntOrZero(getItem23.Month11) + getSeDin_MonthReport8.WeekMontNum ?? 0).ToString();
                        }
                        var getItem24 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 24);
                        if (getItem24 != null)
                        {
                            getItem24.Month11 = (Funs.GetNewIntOrZero(getItem24.Month11) + getSeDin_MonthReport8.SpecialMontPerson ?? 0).ToString();
                        }
                    }
                    /// 安全检查
                    var getSeDin_MonthReport9 = db.SeDin_MonthReport9.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport9 != null)
                    {
                        var getItem25 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 25);
                        if (getItem25 != null)
                        {
                            getItem25.Month11 = (Funs.GetNewIntOrZero(getItem25.Month11) + getSeDin_MonthReport9.DailyMonth ?? 0).ToString();
                        }
                        var getItem26 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 26);
                        if (getItem26 != null)
                        {
                            getItem26.Month11 = (Funs.GetNewIntOrZero(getItem26.Month11) + getSeDin_MonthReport9.WeekMonth ?? 0).ToString();
                        }
                        var getItem27 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 27);
                        if (getItem27 != null)
                        {
                            getItem27.Month11 = (Funs.GetNewIntOrZero(getItem27.Month11) + getSeDin_MonthReport9.SpecialMonth ?? 0).ToString();
                        }
                        var getItem28 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 28);
                        if (getItem28 != null)
                        {
                            getItem28.Month11 = (Funs.GetNewIntOrZero(getItem28.Month11) + getSeDin_MonthReport9.MonthlyMonth ?? 0).ToString();
                        }
                    }
                    /////隐患整改单
                    var getSeDin_MonthReport9Rectification = from x in db.SeDin_MonthReport9Item_Rectification where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Rectification.Count() > 0)
                    {
                        var getItem29 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 29);
                        if (getItem29 != null)
                        {
                            getItem29.Month11 = (Funs.GetNewIntOrZero(getItem29.Month11) + getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                        var getItem30 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 30);
                        if (getItem30 != null)
                        {
                            getItem30.Month11 = (Funs.GetNewIntOrZero(getItem30.Month11) + getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMoth ?? 0)).ToString();
                        }
                        var getItem31 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 31);
                        if (getItem31 != null)
                        {
                            getItem31.Month11 = (Funs.GetNewIntOrZero(getItem31.Month11) + getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthLarge ?? 0)).ToString();
                        }
                        var getItem32 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 32);
                        if (getItem32 != null)
                        {
                            getItem32.Month11 = (Funs.GetNewIntOrZero(getItem32.Month11) + getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothLarge ?? 0)).ToString();
                        }
                        var getItem33 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 33);
                        if (getItem33 != null)
                        {
                            getItem33.Month11 = (Funs.GetNewIntOrZero(getItem33.Month11) + getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthSerious ?? 0)).ToString();
                        }
                        var getItem34 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 34);
                        if (getItem34 != null)
                        {
                            getItem34.Month11 = (Funs.GetNewIntOrZero(getItem34.Month11) + getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothSerious ?? 0)).ToString();
                        }
                    }
                    /////停工令
                    var getSeDin_MonthReport9Stoppage = from x in db.SeDin_MonthReport9Item_Stoppage where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Stoppage.Count() > 0)
                    {
                        var getItem35 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 35);
                        if (getItem35 != null)
                        {
                            getItem35.Month11 = (Funs.GetNewIntOrZero(getItem35.Month11) + getSeDin_MonthReport9Stoppage.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                    }
                    ////奖惩
                    var getSeDin_MonthReport10 = db.SeDin_MonthReport10.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport10 != null)
                    {
                        var getItem36 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 36);
                        if (getItem36 != null)
                        {
                            getItem36.Month11 = (Funs.GetNewDecimalOrZero(getItem36.Month11) + getSeDin_MonthReport10.SafeMonthMoney ?? 0 + getSeDin_MonthReport10.HseMonthMoney ?? 0 + getSeDin_MonthReport10.ProduceMonthMoney ?? 0).ToString();
                        }
                        var getItem37 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 37);
                        if (getItem37 != null)
                        {
                            getItem37.Month11 = (Funs.GetNewDecimalOrZero(getItem37.Month11) + getSeDin_MonthReport10.AccidentMonthMoney ?? 0 + getSeDin_MonthReport10.ViolationMonthMoney ?? 0 + getSeDin_MonthReport10.ManageMonthMoney ?? 0).ToString();
                        }
                    }
                    ////危大工程
                    var getSeDin_MonthReport11 = db.SeDin_MonthReport11.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport11 != null)
                    {
                        var getItem38 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 38);
                        if (getItem38 != null)
                        {
                            getItem38.Month11 = (Funs.GetNewIntOrZero(getItem38.Month11) + getSeDin_MonthReport11.RiskWorkNum ?? 0).ToString();
                        }
                        var getItem39 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 39);
                        if (getItem39 != null)
                        {
                            getItem39.Month11 = (Funs.GetNewIntOrZero(getItem39.Month11) + getSeDin_MonthReport11.LargeWorkNum ?? 0).ToString();
                        }
                    }
                    ////应急
                    var getSeDin_MonthReport12 = db.SeDin_MonthReport12.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport12 != null)
                    {
                        var getItem40 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 40);
                        if (getItem40 != null)
                        {
                            getItem40.Month11 = (Funs.GetNewIntOrZero(getItem40.Month11) + getSeDin_MonthReport12.MultipleSiteNum ?? 0 + getSeDin_MonthReport12.MultipleDesktopNum ?? 0
                                + getSeDin_MonthReport12.SingleSiteNum ?? 0 + getSeDin_MonthReport12.SingleDesktopNum ?? 0).ToString();
                        }
                    }
                    db.SubmitChanges();
                }
                #endregion
            }
            else if (monthReport.ReporMonth.Value.Month == 12)
            {
                #region 提交情况
                bool isSave = true;
                var getSubmissionMonth = getHSEDataCollectISubmissionYear.FirstOrDefault(x => x.ProjectId == monthReport.ProjectId);
                if (getSubmissionMonth == null)
                {
                    Model.DigData_HSEDataCollectSubmission newSubmission = new Model.DigData_HSEDataCollectSubmission()
                    {
                        HSEDataCollectSubmissionId = SQLHelper.GetNewID(),
                        HSEDataCollectId = getHSEDataCollectId,
                        Year = monthReport.ReporMonth.Value.Year,
                        ProjectId = monthReport.ProjectId,
                        Month12 = string.Format("{0:yyyy-MM-dd}", (monthReport.CommitTime ?? DateTime.Now)),
                    };
                    db.DigData_HSEDataCollectSubmission.InsertOnSubmit(newSubmission);
                    db.SubmitChanges();
                }
                else
                {
                    if (!string.IsNullOrEmpty(getSubmissionMonth.Month12))
                    {
                        isSave = false;
                    }
                    else
                    {
                        getSubmissionMonth.Month12 = string.Format("{0:yyyy-MM-dd}", (monthReport.CommitTime ?? DateTime.Now));
                        db.SubmitChanges();
                    }
                }
                #endregion
                #region 数据汇总
                if (isSave)
                {
                    ////员工
                    var getSeDin_MonthReport4Other = db.SeDin_MonthReport4Other.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport4Other != null)
                    {
                        var getItem1 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 1);
                        if (getItem1 != null)
                        {
                            getItem1.Month12 = (Funs.GetNewIntOrZero(getItem1.Month12) + getSeDin_MonthReport4Other.TotalNum ?? 0).ToString();
                        }
                        var getItem2 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 2);
                        if (getItem2 != null)
                        {
                            getItem2.Month12 = (Funs.GetNewIntOrZero(getItem2.Month12) + getSeDin_MonthReport4Other.OutsideNum ?? 0).ToString();
                        }
                        var getItem3 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 3);
                        if (getItem3 != null)
                        {
                            getItem3.Month12 = (Funs.GetNewIntOrZero(getItem3.Month12) + getSeDin_MonthReport4Other.ForeignNum ?? 0).ToString();
                        }
                        var getItem4 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 4);
                        if (getItem4 != null)
                        {
                            getItem4.Month12 = (Funs.GetNewIntOrZero(getItem4.Month12) + getSeDin_MonthReport4Other.ManagerNum ?? 0).ToString();
                        }
                    }
                    ////分包人数
                    var getSeDin_MonthReport4 = from x in db.SeDin_MonthReport4
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport4.Count() > 0)
                    {
                        var getItem5 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 5);
                        if (getItem5 != null)
                        {
                            getItem5.Month12 = (Funs.GetNewIntOrZero(getItem5.Month12) + getSeDin_MonthReport4.Sum(x => x.TotalNum ?? 0)).ToString();
                        }
                        var getItem6 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 6);
                        if (getItem6 != null)
                        {
                            getItem6.Month12 = (Funs.GetNewIntOrZero(getItem6.Month12) + getSeDin_MonthReport4.Sum(x => x.SafeManangerNum ?? 0)).ToString();
                        }
                    }
                    ///安全人工时
                    var getSeDin_MonthReport2 = db.SeDin_MonthReport2.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport2 != null)
                    {
                        var getItem7 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 7);
                        if (getItem7 != null)
                        {
                            getItem7.Month12 = (Funs.GetNewDecimalOrZero(getItem7.Month12) + getSeDin_MonthReport2.MonthWorkTime ?? 0).ToString();
                        }
                    }
                    ////事故
                    var getSeDin_MonthReport3 = from x in db.SeDin_MonthReport3
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport3.Count() > 0)
                    {
                        var getItem8 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 8);
                        if (getItem8 != null)
                        {
                            var r1 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 1);
                            if (r1 != null)
                            {
                                getItem8.Month12 = (Funs.GetNewIntOrZero(getItem8.Month12) + r1.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem9 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 9);
                        if (getItem9 != null)
                        {
                            var r2 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 2);
                            if (r2 != null)
                            {
                                getItem9.Month12 = (Funs.GetNewIntOrZero(getItem9.Month12) + r2.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem10 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 10);
                        if (getItem10 != null)
                        {
                            var r3 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 3);
                            if (r3 != null)
                            {
                                getItem10.Month12 = (Funs.GetNewIntOrZero(getItem10.Month12) + r3.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem11 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 11);
                        if (getItem11 != null)
                        {
                            var r4 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 4);
                            if (r4 != null)
                            {
                                getItem11.Month12 = (Funs.GetNewIntOrZero(getItem11.Month12) + r4.MonthPersons ?? 0).ToString();
                            }
                        }

                        var getItem12 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 12);
                        if (getItem12 != null)
                        {
                            var r5 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 5);
                            if (r5 != null)
                            {
                                getItem12.Month12 = (Funs.GetNewIntOrZero(getItem12.Month12) + r5.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem13 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 13);
                        if (getItem13 != null)
                        {
                            var r6 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 6);
                            if (r6 != null)
                            {
                                getItem13.Month12 = (Funs.GetNewIntOrZero(getItem13.Month12) + r6.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem14 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 14);
                        if (getItem14 != null)
                        {
                            var r7 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 7);
                            if (r7 != null)
                            {
                                getItem14.Month12 = (Funs.GetNewIntOrZero(getItem14.Month12) + r7.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem15 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 15);
                        if (getItem15 != null)
                        {
                            var r8 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 8);
                            if (r8 != null)
                            {
                                getItem15.Month12 = (Funs.GetNewIntOrZero(getItem15.Month12) + r8.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem16 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 16);
                        if (getItem16 != null)
                        {
                            var r9 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 9);
                            if (r9 != null)
                            {
                                getItem16.Month12 = (Funs.GetNewIntOrZero(getItem16.Month12) + r9.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem17 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 17);
                        if (getItem17 != null)
                        {
                            var r10 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 10);
                            if (r10 != null)
                            {
                                getItem17.Month12 = (Funs.GetNewIntOrZero(getItem17.Month12) + r10.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem18 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 18);
                        if (getItem18 != null)
                        {
                            var r11 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 11);
                            if (r11 != null)
                            {
                                getItem18.Month12 = (Funs.GetNewIntOrZero(getItem18.Month12) + r11.MonthTimes ?? 0).ToString();
                            }
                        }
                    }
                    ////大型、特种设备
                    var getSeDin_MonthReport5 = from x in db.SeDin_MonthReport5
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    var getItem19 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 19);
                    if (getSeDin_MonthReport5.Count() > 0 && getItem19 != null)
                    {
                        int countSumR5 = getSeDin_MonthReport5.Sum(x => x.T01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.T04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T05 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T06 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.S01 ?? 0);
                        getItem19.Month12 = (Funs.GetNewIntOrZero(getItem19.Month12) + countSumR5).ToString();
                    }
                    ////安全生产费用
                    var getSeDin_MonthReport6 = db.SeDin_MonthReport6.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport6 != null)
                    {
                        var getItem20 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 20);
                        if (getItem20 != null)
                        {
                            getItem20.Month12 = (Funs.GetNewDecimalOrZero(getItem20.Month12) + getSeDin_MonthReport6.SumMonth ?? 0).ToString();
                        }
                    }
                    ////项目现场员工入场安全培训人数
                    var getSeDin_MonthReport7 = db.SeDin_MonthReport7.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport7 != null)
                    {
                        var getItem21 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 21);
                        if (getItem21 != null)
                        {
                            getItem21.Month12 = (Funs.GetNewIntOrZero(getItem21.Month12) + getSeDin_MonthReport7.EmployeeMontPerson ?? 0).ToString();
                        }
                    }
                    ////项目会次
                    var getSeDin_MonthReport8 = db.SeDin_MonthReport8.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport8 != null)
                    {
                        var getItem22 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 22);
                        if (getItem22 != null)
                        {
                            getItem22.Month12 = (Funs.GetNewIntOrZero(getItem22.Month12) + getSeDin_MonthReport8.MonthMontNum ?? 0).ToString();
                        }
                        var getItem23 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 23);
                        if (getItem23 != null)
                        {
                            getItem23.Month12 = (Funs.GetNewIntOrZero(getItem23.Month12) + getSeDin_MonthReport8.WeekMontNum ?? 0).ToString();
                        }
                        var getItem24 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 24);
                        if (getItem24 != null)
                        {
                            getItem24.Month12 = (Funs.GetNewIntOrZero(getItem24.Month12) + getSeDin_MonthReport8.SpecialMontPerson ?? 0).ToString();
                        }
                    }
                    /// 安全检查
                    var getSeDin_MonthReport9 = db.SeDin_MonthReport9.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport9 != null)
                    {
                        var getItem25 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 25);
                        if (getItem25 != null)
                        {
                            getItem25.Month12 = (Funs.GetNewIntOrZero(getItem25.Month12) + getSeDin_MonthReport9.DailyMonth ?? 0).ToString();
                        }
                        var getItem26 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 26);
                        if (getItem26 != null)
                        {
                            getItem26.Month12 = (Funs.GetNewIntOrZero(getItem26.Month12) + getSeDin_MonthReport9.WeekMonth ?? 0).ToString();
                        }
                        var getItem27 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 27);
                        if (getItem27 != null)
                        {
                            getItem27.Month12 = (Funs.GetNewIntOrZero(getItem27.Month12) + getSeDin_MonthReport9.SpecialMonth ?? 0).ToString();
                        }
                        var getItem28 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 28);
                        if (getItem28 != null)
                        {
                            getItem28.Month12 = (Funs.GetNewIntOrZero(getItem28.Month12) + getSeDin_MonthReport9.MonthlyMonth ?? 0).ToString();
                        }
                    }
                    /////隐患整改单
                    var getSeDin_MonthReport9Rectification = from x in db.SeDin_MonthReport9Item_Rectification where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Rectification.Count() > 0)
                    {
                        var getItem29 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 29);
                        if (getItem29 != null)
                        {
                            getItem29.Month12 = (Funs.GetNewIntOrZero(getItem29.Month12) + getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                        var getItem30 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 30);
                        if (getItem30 != null)
                        {
                            getItem30.Month12 = (Funs.GetNewIntOrZero(getItem30.Month12) + getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMoth ?? 0)).ToString();
                        }
                        var getItem31 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 31);
                        if (getItem31 != null)
                        {
                            getItem31.Month12 = (Funs.GetNewIntOrZero(getItem31.Month12) + getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthLarge ?? 0)).ToString();
                        }
                        var getItem32 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 32);
                        if (getItem32 != null)
                        {
                            getItem32.Month12 = (Funs.GetNewIntOrZero(getItem32.Month12) + getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothLarge ?? 0)).ToString();
                        }
                        var getItem33 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 33);
                        if (getItem33 != null)
                        {
                            getItem33.Month12 = (Funs.GetNewIntOrZero(getItem33.Month12) + getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthSerious ?? 0)).ToString();
                        }
                        var getItem34 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 34);
                        if (getItem34 != null)
                        {
                            getItem34.Month12 = (Funs.GetNewIntOrZero(getItem34.Month12) + getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothSerious ?? 0)).ToString();
                        }
                    }
                    /////停工令
                    var getSeDin_MonthReport9Stoppage = from x in db.SeDin_MonthReport9Item_Stoppage where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Stoppage.Count() > 0)
                    {
                        var getItem35 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 35);
                        if (getItem35 != null)
                        {
                            getItem35.Month12 = (Funs.GetNewIntOrZero(getItem35.Month12) + getSeDin_MonthReport9Stoppage.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                    }
                    ////奖惩
                    var getSeDin_MonthReport10 = db.SeDin_MonthReport10.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport10 != null)
                    {
                        var getItem36 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 36);
                        if (getItem36 != null)
                        {
                            getItem36.Month12 = (Funs.GetNewDecimalOrZero(getItem36.Month12) + getSeDin_MonthReport10.SafeMonthMoney ?? 0 + getSeDin_MonthReport10.HseMonthMoney ?? 0 + getSeDin_MonthReport10.ProduceMonthMoney ?? 0).ToString();
                        }
                        var getItem37 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 37);
                        if (getItem37 != null)
                        {
                            getItem37.Month12 = (Funs.GetNewDecimalOrZero(getItem37.Month12) + getSeDin_MonthReport10.AccidentMonthMoney ?? 0 + getSeDin_MonthReport10.ViolationMonthMoney ?? 0 + getSeDin_MonthReport10.ManageMonthMoney ?? 0).ToString();
                        }
                    }
                    ////危大工程
                    var getSeDin_MonthReport11 = db.SeDin_MonthReport11.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport11 != null)
                    {
                        var getItem38 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 38);
                        if (getItem38 != null)
                        {
                            getItem38.Month12 = (Funs.GetNewIntOrZero(getItem38.Month12) + getSeDin_MonthReport11.RiskWorkNum ?? 0).ToString();
                        }
                        var getItem39 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 39);
                        if (getItem39 != null)
                        {
                            getItem39.Month12 = (Funs.GetNewIntOrZero(getItem39.Month12) + getSeDin_MonthReport11.LargeWorkNum ?? 0).ToString();
                        }
                    }
                    ////应急
                    var getSeDin_MonthReport12 = db.SeDin_MonthReport12.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport12 != null)
                    {
                        var getItem40 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 40);
                        if (getItem40 != null)
                        {
                            getItem40.Month12 = (Funs.GetNewIntOrZero(getItem40.Month12) + getSeDin_MonthReport12.MultipleSiteNum ?? 0 + getSeDin_MonthReport12.MultipleDesktopNum ?? 0
                                + getSeDin_MonthReport12.SingleSiteNum ?? 0 + getSeDin_MonthReport12.SingleDesktopNum ?? 0).ToString();
                        }
                    }
                    db.SubmitChanges();
                }
                #endregion
            }
        }
        #endregion

        #region 根据月报信息删除汇总数据
        /// <summary>
        ///  根据月报信息汇总数据
        /// </summary>
        public static void DeleteHSEDataCollectItem(Model.SeDin_MonthReport monthReport)
        {
            Model.SGGLDB db = Funs.DB;
            string getHSEDataCollectId = string.Empty;
            var getHSEDataCollect = GetHSEDataCollectByYear(monthReport.ReporMonth.Value.Year);
            if (getHSEDataCollect != null)
            {
                getHSEDataCollectId = getHSEDataCollect.HSEDataCollectId;
            }
            else
            {
                getHSEDataCollectId = CreateHSEDataCollectByYear(monthReport.ReporMonth.Value.Year);
            }

            var getHSEDataCollectItemYear = from x in db.DigData_HSEDataCollectItem
                                            where x.HSEDataCollectId == getHSEDataCollectId
                                            select x;
            var getHSEDataCollectISubmissionYear = from x in db.DigData_HSEDataCollectSubmission
                                                   where x.HSEDataCollectId == getHSEDataCollectId
                                                   select x;
            ////一月份
            if (monthReport.ReporMonth.Value.Month == 1)
            {
                #region 数据调整
                bool isSave = false;
                var getSubmissionMonth = getHSEDataCollectISubmissionYear.FirstOrDefault(x => x.ProjectId == monthReport.ProjectId);
                if (getSubmissionMonth != null && Funs.GetNewDateTime(getSubmissionMonth.Month1).HasValue)
                {
                    isSave = true;
                }
                if (isSave)
                {
                    ////员工
                    var getSeDin_MonthReport4Other = db.SeDin_MonthReport4Other.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport4Other != null)
                    {
                        var getItem1 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 1);
                        if (getItem1 != null)
                        {
                            getItem1.Month1 = (Funs.GetNewIntOrZero(getItem1.Month1) - getSeDin_MonthReport4Other.TotalNum ?? 0).ToString();
                        }
                        var getItem2 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 2);
                        if (getItem2 != null)
                        {
                            getItem2.Month1 = (Funs.GetNewIntOrZero(getItem2.Month1) - getSeDin_MonthReport4Other.OutsideNum ?? 0).ToString();
                        }
                        var getItem3 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 3);
                        if (getItem3 != null)
                        {
                            getItem3.Month1 = (Funs.GetNewIntOrZero(getItem3.Month1) - getSeDin_MonthReport4Other.ForeignNum ?? 0).ToString();
                        }
                        var getItem4 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 4);
                        if (getItem4 != null)
                        {
                            getItem4.Month1 = (Funs.GetNewIntOrZero(getItem4.Month1) - getSeDin_MonthReport4Other.ManagerNum ?? 0).ToString();
                        }
                    }
                    ////分包人数
                    var getSeDin_MonthReport4 = from x in db.SeDin_MonthReport4
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport4.Count() > 0)
                    {
                        var getItem5 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 5);
                        if (getItem5 != null)
                        {
                            getItem5.Month1 = (Funs.GetNewIntOrZero(getItem5.Month1) - getSeDin_MonthReport4.Sum(x => x.TotalNum ?? 0)).ToString();
                        }
                        var getItem6 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 6);
                        if (getItem6 != null)
                        {
                            getItem6.Month1 = (Funs.GetNewIntOrZero(getItem6.Month1) - getSeDin_MonthReport4.Sum(x => x.SafeManangerNum ?? 0)).ToString();
                        }
                    }
                    ///安全人工时
                    var getSeDin_MonthReport2 = db.SeDin_MonthReport2.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport2 != null)
                    {
                        var getItem7 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 7);
                        if (getItem7 != null)
                        {
                            getItem7.Month1 = (Funs.GetNewDecimalOrZero(getItem7.Month1) - getSeDin_MonthReport2.MonthWorkTime ?? 0).ToString();
                        }
                    }
                    ////事故
                    var getSeDin_MonthReport3 = from x in db.SeDin_MonthReport3
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport3.Count() > 0)
                    {
                        var getItem8 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 8);
                        if (getItem8 != null)
                        {
                            var r1 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 1);
                            if (r1 != null)
                            {
                                getItem8.Month1 = (Funs.GetNewIntOrZero(getItem8.Month1) - r1.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem9 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 9);
                        if (getItem9 != null)
                        {
                            var r2 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 2);
                            if (r2 != null)
                            {
                                getItem9.Month1 = (Funs.GetNewIntOrZero(getItem9.Month1) - r2.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem10 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 10);
                        if (getItem10 != null)
                        {
                            var r3 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 3);
                            if (r3 != null)
                            {
                                getItem10.Month1 = (Funs.GetNewIntOrZero(getItem10.Month1) - r3.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem11 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 11);
                        if (getItem11 != null)
                        {
                            var r4 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 4);
                            if (r4 != null)
                            {
                                getItem11.Month1 = (Funs.GetNewIntOrZero(getItem11.Month1) - r4.MonthPersons ?? 0).ToString();
                            }
                        }

                        var getItem12 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 12);
                        if (getItem12 != null)
                        {
                            var r5 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 5);
                            if (r5 != null)
                            {
                                getItem12.Month1 = (Funs.GetNewIntOrZero(getItem12.Month1) - r5.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem13 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 13);
                        if (getItem13 != null)
                        {
                            var r6 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 6);
                            if (r6 != null)
                            {
                                getItem13.Month1 = (Funs.GetNewIntOrZero(getItem13.Month1) - r6.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem14 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 14);
                        if (getItem14 != null)
                        {
                            var r7 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 7);
                            if (r7 != null)
                            {
                                getItem14.Month1 = (Funs.GetNewIntOrZero(getItem14.Month1) - r7.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem15 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 15);
                        if (getItem15 != null)
                        {
                            var r8 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 8);
                            if (r8 != null)
                            {
                                getItem15.Month1 = (Funs.GetNewIntOrZero(getItem15.Month1) - r8.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem16 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 16);
                        if (getItem16 != null)
                        {
                            var r9 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 9);
                            if (r9 != null)
                            {
                                getItem16.Month1 = (Funs.GetNewIntOrZero(getItem16.Month1) - r9.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem17 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 17);
                        if (getItem17 != null)
                        {
                            var r10 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 10);
                            if (r10 != null)
                            {
                                getItem17.Month1 = (Funs.GetNewIntOrZero(getItem17.Month1) - r10.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem18 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 18);
                        if (getItem18 != null)
                        {
                            var r11 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 11);
                            if (r11 != null)
                            {
                                getItem18.Month1 = (Funs.GetNewIntOrZero(getItem18.Month1) - r11.MonthTimes ?? 0).ToString();
                            }
                        }
                    }
                    ////大型、特种设备
                    var getSeDin_MonthReport5 = from x in db.SeDin_MonthReport5
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    var getItem19 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 19);
                    if (getSeDin_MonthReport5.Count() > 0 && getItem19 != null)
                    {
                        int countSumR5 = getSeDin_MonthReport5.Sum(x => x.T01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.T04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T05 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T06 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.S01 ?? 0);
                        getItem19.Month1 = (Funs.GetNewIntOrZero(getItem19.Month1) - countSumR5).ToString();
                    }
                    ////安全生产费用
                    var getSeDin_MonthReport6 = db.SeDin_MonthReport6.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport6 != null)
                    {
                        var getItem20 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 20);
                        if (getItem20 != null)
                        {
                            getItem20.Month1 = (Funs.GetNewDecimalOrZero(getItem20.Month1) - getSeDin_MonthReport6.SumMonth ?? 0).ToString();
                        }
                    }
                    ////项目现场员工入场安全培训人数
                    var getSeDin_MonthReport7 = db.SeDin_MonthReport7.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport7 != null)
                    {
                        var getItem21 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 21);
                        if (getItem21 != null)
                        {
                            getItem21.Month1 = (Funs.GetNewIntOrZero(getItem21.Month1) - getSeDin_MonthReport7.EmployeeMontPerson ?? 0).ToString();
                        }
                    }
                    ////项目会次
                    var getSeDin_MonthReport8 = db.SeDin_MonthReport8.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport8 != null)
                    {
                        var getItem22 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 22);
                        if (getItem22 != null)
                        {
                            getItem22.Month1 = (Funs.GetNewIntOrZero(getItem22.Month1) - getSeDin_MonthReport8.MonthMontNum ?? 0).ToString();
                        }
                        var getItem23 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 23);
                        if (getItem23 != null)
                        {
                            getItem23.Month1 = (Funs.GetNewIntOrZero(getItem23.Month1) - getSeDin_MonthReport8.WeekMontNum ?? 0).ToString();
                        }
                        var getItem24 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 24);
                        if (getItem24 != null)
                        {
                            getItem24.Month1 = (Funs.GetNewIntOrZero(getItem24.Month1) - getSeDin_MonthReport8.SpecialMontPerson ?? 0).ToString();
                        }
                    }
                    /// 安全检查
                    var getSeDin_MonthReport9 = db.SeDin_MonthReport9.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport9 != null)
                    {
                        var getItem25 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 25);
                        if (getItem25 != null)
                        {
                            getItem25.Month1 = (Funs.GetNewIntOrZero(getItem25.Month1) - getSeDin_MonthReport9.DailyMonth ?? 0).ToString();
                        }
                        var getItem26 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 26);
                        if (getItem26 != null)
                        {
                            getItem26.Month1 = (Funs.GetNewIntOrZero(getItem26.Month1) - getSeDin_MonthReport9.WeekMonth ?? 0).ToString();
                        }
                        var getItem27 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 27);
                        if (getItem27 != null)
                        {
                            getItem27.Month1 = (Funs.GetNewIntOrZero(getItem27.Month1) - getSeDin_MonthReport9.SpecialMonth ?? 0).ToString();
                        }
                        var getItem28 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 28);
                        if (getItem28 != null)
                        {
                            getItem28.Month1 = (Funs.GetNewIntOrZero(getItem28.Month1) - getSeDin_MonthReport9.MonthlyMonth ?? 0).ToString();
                        }
                    }
                    /////隐患整改单
                    var getSeDin_MonthReport9Rectification = from x in db.SeDin_MonthReport9Item_Rectification where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Rectification.Count() > 0)
                    {
                        var getItem29 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 29);
                        if (getItem29 != null)
                        {
                            getItem29.Month1 = (Funs.GetNewIntOrZero(getItem29.Month1) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                        var getItem30 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 30);
                        if (getItem30 != null)
                        {
                            getItem30.Month1 = (Funs.GetNewIntOrZero(getItem30.Month1) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMoth ?? 0)).ToString();
                        }
                        var getItem31 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 31);
                        if (getItem31 != null)
                        {
                            getItem31.Month1 = (Funs.GetNewIntOrZero(getItem31.Month1) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthLarge ?? 0)).ToString();
                        }
                        var getItem32 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 32);
                        if (getItem32 != null)
                        {
                            getItem32.Month1 = (Funs.GetNewIntOrZero(getItem32.Month1) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothLarge ?? 0)).ToString();
                        }
                        var getItem33 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 33);
                        if (getItem33 != null)
                        {
                            getItem33.Month1 = (Funs.GetNewIntOrZero(getItem33.Month1) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthSerious ?? 0)).ToString();
                        }
                        var getItem34 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 34);
                        if (getItem34 != null)
                        {
                            getItem34.Month1 = (Funs.GetNewIntOrZero(getItem34.Month1) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothSerious ?? 0)).ToString();
                        }
                    }
                    /////停工令
                    var getSeDin_MonthReport9Stoppage = from x in db.SeDin_MonthReport9Item_Stoppage where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Stoppage.Count() > 0)
                    {
                        var getItem35 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 35);
                        if (getItem35 != null)
                        {
                            getItem35.Month1 = (Funs.GetNewIntOrZero(getItem35.Month1) - getSeDin_MonthReport9Stoppage.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                    }
                    ////奖惩
                    var getSeDin_MonthReport10 = db.SeDin_MonthReport10.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport10 != null)
                    {
                        var getItem36 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 36);
                        if (getItem36 != null)
                        {
                            getItem36.Month1 = (Funs.GetNewDecimalOrZero(getItem36.Month1) - getSeDin_MonthReport10.SafeMonthMoney ?? 0 - getSeDin_MonthReport10.HseMonthMoney ?? 0 - getSeDin_MonthReport10.ProduceMonthMoney ?? 0).ToString();
                        }
                        var getItem37 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 37);
                        if (getItem37 != null)
                        {
                            getItem37.Month1 = (Funs.GetNewDecimalOrZero(getItem37.Month1) - getSeDin_MonthReport10.AccidentMonthMoney ?? 0 - getSeDin_MonthReport10.ViolationMonthMoney ?? 0 - getSeDin_MonthReport10.ManageMonthMoney ?? 0).ToString();
                        }
                    }
                    ////危大工程
                    var getSeDin_MonthReport11 = db.SeDin_MonthReport11.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport11 != null)
                    {
                        var getItem38 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 38);
                        if (getItem38 != null)
                        {
                            getItem38.Month1 = (Funs.GetNewIntOrZero(getItem38.Month1) - getSeDin_MonthReport11.RiskWorkNum ?? 0).ToString();
                        }
                        var getItem39 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 39);
                        if (getItem39 != null)
                        {
                            getItem39.Month1 = (Funs.GetNewIntOrZero(getItem39.Month1) - getSeDin_MonthReport11.LargeWorkNum ?? 0).ToString();
                        }
                    }
                    ////应急
                    var getSeDin_MonthReport12 = db.SeDin_MonthReport12.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport12 != null)
                    {
                        var getItem40 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 40);
                        if (getItem40 != null)
                        {
                            getItem40.Month1 = (Funs.GetNewIntOrZero(getItem40.Month1) - getSeDin_MonthReport12.MultipleSiteNum ?? 0 - getSeDin_MonthReport12.MultipleDesktopNum ?? 0
                               - getSeDin_MonthReport12.SingleSiteNum ?? 0 - getSeDin_MonthReport12.SingleDesktopNum ?? 0).ToString();
                        }
                    }
                    db.SubmitChanges();
                }
                #endregion
            }
            else if (monthReport.ReporMonth.Value.Month == 2)
            {
                #region 数据调整
                bool isSave = false;
                var getSubmissionMonth = getHSEDataCollectISubmissionYear.FirstOrDefault(x => x.ProjectId == monthReport.ProjectId);
                if (getSubmissionMonth != null && Funs.GetNewDateTime(getSubmissionMonth.Month2).HasValue)
                {
                    isSave = true;
                }
                if (isSave)
                {
                    ////员工
                    var getSeDin_MonthReport4Other = db.SeDin_MonthReport4Other.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport4Other != null)
                    {
                        var getItem1 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 1);
                        if (getItem1 != null)
                        {
                            getItem1.Month2 = (Funs.GetNewIntOrZero(getItem1.Month2) - getSeDin_MonthReport4Other.TotalNum ?? 0).ToString();
                        }
                        var getItem2 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 2);
                        if (getItem2 != null)
                        {
                            getItem2.Month2 = (Funs.GetNewIntOrZero(getItem2.Month2) - getSeDin_MonthReport4Other.OutsideNum ?? 0).ToString();
                        }
                        var getItem3 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 3);
                        if (getItem3 != null)
                        {
                            getItem3.Month2 = (Funs.GetNewIntOrZero(getItem3.Month2) - getSeDin_MonthReport4Other.ForeignNum ?? 0).ToString();
                        }
                        var getItem4 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 4);
                        if (getItem4 != null)
                        {
                            getItem4.Month2 = (Funs.GetNewIntOrZero(getItem4.Month2) - getSeDin_MonthReport4Other.ManagerNum ?? 0).ToString();
                        }
                    }
                    ////分包人数
                    var getSeDin_MonthReport4 = from x in db.SeDin_MonthReport4
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport4.Count() > 0)
                    {
                        var getItem5 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 5);
                        if (getItem5 != null)
                        {
                            getItem5.Month2 = (Funs.GetNewIntOrZero(getItem5.Month2) - getSeDin_MonthReport4.Sum(x => x.TotalNum ?? 0)).ToString();
                        }
                        var getItem6 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 6);
                        if (getItem6 != null)
                        {
                            getItem6.Month2 = (Funs.GetNewIntOrZero(getItem6.Month2) - getSeDin_MonthReport4.Sum(x => x.SafeManangerNum ?? 0)).ToString();
                        }
                    }
                    ///安全人工时
                    var getSeDin_MonthReport2 = db.SeDin_MonthReport2.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport2 != null)
                    {
                        var getItem7 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 7);
                        if (getItem7 != null)
                        {
                            getItem7.Month2 = (Funs.GetNewDecimalOrZero(getItem7.Month2) - getSeDin_MonthReport2.MonthWorkTime ?? 0).ToString();
                        }
                    }
                    ////事故
                    var getSeDin_MonthReport3 = from x in db.SeDin_MonthReport3
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport3.Count() > 0)
                    {
                        var getItem8 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 8);
                        if (getItem8 != null)
                        {
                            var r1 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 1);
                            if (r1 != null)
                            {
                                getItem8.Month2 = (Funs.GetNewIntOrZero(getItem8.Month2) - r1.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem9 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 9);
                        if (getItem9 != null)
                        {
                            var r2 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 2);
                            if (r2 != null)
                            {
                                getItem9.Month2 = (Funs.GetNewIntOrZero(getItem9.Month2) - r2.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem10 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 10);
                        if (getItem10 != null)
                        {
                            var r3 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 3);
                            if (r3 != null)
                            {
                                getItem10.Month2 = (Funs.GetNewIntOrZero(getItem10.Month2) - r3.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem11 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 11);
                        if (getItem11 != null)
                        {
                            var r4 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 4);
                            if (r4 != null)
                            {
                                getItem11.Month2 = (Funs.GetNewIntOrZero(getItem11.Month2) - r4.MonthPersons ?? 0).ToString();
                            }
                        }

                        var getItem12 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 12);
                        if (getItem12 != null)
                        {
                            var r5 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 5);
                            if (r5 != null)
                            {
                                getItem12.Month2 = (Funs.GetNewIntOrZero(getItem12.Month2) - r5.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem13 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 13);
                        if (getItem13 != null)
                        {
                            var r6 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 6);
                            if (r6 != null)
                            {
                                getItem13.Month2 = (Funs.GetNewIntOrZero(getItem13.Month2) - r6.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem14 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 14);
                        if (getItem14 != null)
                        {
                            var r7 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 7);
                            if (r7 != null)
                            {
                                getItem14.Month2 = (Funs.GetNewIntOrZero(getItem14.Month2) - r7.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem15 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 15);
                        if (getItem15 != null)
                        {
                            var r8 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 8);
                            if (r8 != null)
                            {
                                getItem15.Month2 = (Funs.GetNewIntOrZero(getItem15.Month2) - r8.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem16 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 16);
                        if (getItem16 != null)
                        {
                            var r9 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 9);
                            if (r9 != null)
                            {
                                getItem16.Month2 = (Funs.GetNewIntOrZero(getItem16.Month2) - r9.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem17 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 17);
                        if (getItem17 != null)
                        {
                            var r10 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 10);
                            if (r10 != null)
                            {
                                getItem17.Month2 = (Funs.GetNewIntOrZero(getItem17.Month2) - r10.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem18 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 18);
                        if (getItem18 != null)
                        {
                            var r11 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 11);
                            if (r11 != null)
                            {
                                getItem18.Month2 = (Funs.GetNewIntOrZero(getItem18.Month2) - r11.MonthTimes ?? 0).ToString();
                            }
                        }
                    }
                    ////大型、特种设备
                    var getSeDin_MonthReport5 = from x in db.SeDin_MonthReport5
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    var getItem19 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 19);
                    if (getSeDin_MonthReport5.Count() > 0 && getItem19 != null)
                    {
                        int countSumR5 = getSeDin_MonthReport5.Sum(x => x.T01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.T04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T05 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T06 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.S01 ?? 0);
                        getItem19.Month2 = (Funs.GetNewIntOrZero(getItem19.Month2) - countSumR5).ToString();
                    }
                    ////安全生产费用
                    var getSeDin_MonthReport6 = db.SeDin_MonthReport6.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport6 != null)
                    {
                        var getItem20 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 20);
                        if (getItem20 != null)
                        {
                            getItem20.Month2 = (Funs.GetNewDecimalOrZero(getItem20.Month2) - getSeDin_MonthReport6.SumMonth ?? 0).ToString();
                        }
                    }
                    ////项目现场员工入场安全培训人数
                    var getSeDin_MonthReport7 = db.SeDin_MonthReport7.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport7 != null)
                    {
                        var getItem21 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 21);
                        if (getItem21 != null)
                        {
                            getItem21.Month2 = (Funs.GetNewIntOrZero(getItem21.Month2) - getSeDin_MonthReport7.EmployeeMontPerson ?? 0).ToString();
                        }
                    }
                    ////项目会次
                    var getSeDin_MonthReport8 = db.SeDin_MonthReport8.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport8 != null)
                    {
                        var getItem22 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 22);
                        if (getItem22 != null)
                        {
                            getItem22.Month2 = (Funs.GetNewIntOrZero(getItem22.Month2) - getSeDin_MonthReport8.MonthMontNum ?? 0).ToString();
                        }
                        var getItem23 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 23);
                        if (getItem23 != null)
                        {
                            getItem23.Month2 = (Funs.GetNewIntOrZero(getItem23.Month2) - getSeDin_MonthReport8.WeekMontNum ?? 0).ToString();
                        }
                        var getItem24 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 24);
                        if (getItem24 != null)
                        {
                            getItem24.Month2 = (Funs.GetNewIntOrZero(getItem24.Month2) - getSeDin_MonthReport8.SpecialMontPerson ?? 0).ToString();
                        }
                    }
                    /// 安全检查
                    var getSeDin_MonthReport9 = db.SeDin_MonthReport9.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport9 != null)
                    {
                        var getItem25 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 25);
                        if (getItem25 != null)
                        {
                            getItem25.Month2 = (Funs.GetNewIntOrZero(getItem25.Month2) - getSeDin_MonthReport9.DailyMonth ?? 0).ToString();
                        }
                        var getItem26 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 26);
                        if (getItem26 != null)
                        {
                            getItem26.Month2 = (Funs.GetNewIntOrZero(getItem26.Month2) - getSeDin_MonthReport9.WeekMonth ?? 0).ToString();
                        }
                        var getItem27 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 27);
                        if (getItem27 != null)
                        {
                            getItem27.Month2 = (Funs.GetNewIntOrZero(getItem27.Month2) - getSeDin_MonthReport9.SpecialMonth ?? 0).ToString();
                        }
                        var getItem28 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 28);
                        if (getItem28 != null)
                        {
                            getItem28.Month2 = (Funs.GetNewIntOrZero(getItem28.Month2) - getSeDin_MonthReport9.MonthlyMonth ?? 0).ToString();
                        }
                    }
                    /////隐患整改单
                    var getSeDin_MonthReport9Rectification = from x in db.SeDin_MonthReport9Item_Rectification where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Rectification.Count() > 0)
                    {
                        var getItem29 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 29);
                        if (getItem29 != null)
                        {
                            getItem29.Month2 = (Funs.GetNewIntOrZero(getItem29.Month2) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                        var getItem30 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 30);
                        if (getItem30 != null)
                        {
                            getItem30.Month2 = (Funs.GetNewIntOrZero(getItem30.Month2) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMoth ?? 0)).ToString();
                        }
                        var getItem31 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 31);
                        if (getItem31 != null)
                        {
                            getItem31.Month2 = (Funs.GetNewIntOrZero(getItem31.Month2) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthLarge ?? 0)).ToString();
                        }
                        var getItem32 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 32);
                        if (getItem32 != null)
                        {
                            getItem32.Month2 = (Funs.GetNewIntOrZero(getItem32.Month2) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothLarge ?? 0)).ToString();
                        }
                        var getItem33 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 33);
                        if (getItem33 != null)
                        {
                            getItem33.Month2 = (Funs.GetNewIntOrZero(getItem33.Month2) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthSerious ?? 0)).ToString();
                        }
                        var getItem34 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 34);
                        if (getItem34 != null)
                        {
                            getItem34.Month2 = (Funs.GetNewIntOrZero(getItem34.Month2) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothSerious ?? 0)).ToString();
                        }
                    }
                    /////停工令
                    var getSeDin_MonthReport9Stoppage = from x in db.SeDin_MonthReport9Item_Stoppage where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Stoppage.Count() > 0)
                    {
                        var getItem35 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 35);
                        if (getItem35 != null)
                        {
                            getItem35.Month2 = (Funs.GetNewIntOrZero(getItem35.Month2) - getSeDin_MonthReport9Stoppage.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                    }
                    ////奖惩
                    var getSeDin_MonthReport10 = db.SeDin_MonthReport10.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport10 != null)
                    {
                        var getItem36 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 36);
                        if (getItem36 != null)
                        {
                            getItem36.Month2 = (Funs.GetNewDecimalOrZero(getItem36.Month2) - getSeDin_MonthReport10.SafeMonthMoney ?? 0 - getSeDin_MonthReport10.HseMonthMoney ?? 0 - getSeDin_MonthReport10.ProduceMonthMoney ?? 0).ToString();
                        }
                        var getItem37 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 37);
                        if (getItem37 != null)
                        {
                            getItem37.Month2 = (Funs.GetNewDecimalOrZero(getItem37.Month2) - getSeDin_MonthReport10.AccidentMonthMoney ?? 0 - getSeDin_MonthReport10.ViolationMonthMoney ?? 0 - getSeDin_MonthReport10.ManageMonthMoney ?? 0).ToString();
                        }
                    }
                    ////危大工程
                    var getSeDin_MonthReport11 = db.SeDin_MonthReport11.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport11 != null)
                    {
                        var getItem38 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 38);
                        if (getItem38 != null)
                        {
                            getItem38.Month2 = (Funs.GetNewIntOrZero(getItem38.Month2) - getSeDin_MonthReport11.RiskWorkNum ?? 0).ToString();
                        }
                        var getItem39 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 39);
                        if (getItem39 != null)
                        {
                            getItem39.Month2 = (Funs.GetNewIntOrZero(getItem39.Month2) - getSeDin_MonthReport11.LargeWorkNum ?? 0).ToString();
                        }
                    }
                    ////应急
                    var getSeDin_MonthReport12 = db.SeDin_MonthReport12.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport12 != null)
                    {
                        var getItem40 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 40);
                        if (getItem40 != null)
                        {
                            getItem40.Month2 = (Funs.GetNewIntOrZero(getItem40.Month2) - getSeDin_MonthReport12.MultipleSiteNum ?? 0 - getSeDin_MonthReport12.MultipleDesktopNum ?? 0
                               - getSeDin_MonthReport12.SingleSiteNum ?? 0 - getSeDin_MonthReport12.SingleDesktopNum ?? 0).ToString();
                        }
                    }
                    db.SubmitChanges();
                }
                #endregion                
            }
            else if (monthReport.ReporMonth.Value.Month == 3)
            {
                #region 数据调整
                bool isSave = false;
                var getSubmissionMonth = getHSEDataCollectISubmissionYear.FirstOrDefault(x => x.ProjectId == monthReport.ProjectId);
                if (getSubmissionMonth != null && Funs.GetNewDateTime(getSubmissionMonth.Month3).HasValue)
                {
                    isSave = true;
                }
                if (isSave)
                {
                    ////员工
                    var getSeDin_MonthReport4Other = db.SeDin_MonthReport4Other.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport4Other != null)
                    {
                        var getItem1 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 1);
                        if (getItem1 != null)
                        {
                            getItem1.Month3 = (Funs.GetNewIntOrZero(getItem1.Month3) - getSeDin_MonthReport4Other.TotalNum ?? 0).ToString();
                        }
                        var getItem2 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 2);
                        if (getItem2 != null)
                        {
                            getItem2.Month3 = (Funs.GetNewIntOrZero(getItem2.Month3) - getSeDin_MonthReport4Other.OutsideNum ?? 0).ToString();
                        }
                        var getItem3 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 3);
                        if (getItem3 != null)
                        {
                            getItem3.Month3 = (Funs.GetNewIntOrZero(getItem3.Month3) - getSeDin_MonthReport4Other.ForeignNum ?? 0).ToString();
                        }
                        var getItem4 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 4);
                        if (getItem4 != null)
                        {
                            getItem4.Month3 = (Funs.GetNewIntOrZero(getItem4.Month3) - getSeDin_MonthReport4Other.ManagerNum ?? 0).ToString();
                        }
                    }
                    ////分包人数
                    var getSeDin_MonthReport4 = from x in db.SeDin_MonthReport4
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport4.Count() > 0)
                    {
                        var getItem5 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 5);
                        if (getItem5 != null)
                        {
                            getItem5.Month3 = (Funs.GetNewIntOrZero(getItem5.Month3) - getSeDin_MonthReport4.Sum(x => x.TotalNum ?? 0)).ToString();
                        }
                        var getItem6 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 6);
                        if (getItem6 != null)
                        {
                            getItem6.Month3 = (Funs.GetNewIntOrZero(getItem6.Month3) - getSeDin_MonthReport4.Sum(x => x.SafeManangerNum ?? 0)).ToString();
                        }
                    }
                    ///安全人工时
                    var getSeDin_MonthReport2 = db.SeDin_MonthReport2.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport2 != null)
                    {
                        var getItem7 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 7);
                        if (getItem7 != null)
                        {
                            getItem7.Month3 = (Funs.GetNewDecimalOrZero(getItem7.Month3) - getSeDin_MonthReport2.MonthWorkTime ?? 0).ToString();
                        }
                    }
                    ////事故
                    var getSeDin_MonthReport3 = from x in db.SeDin_MonthReport3
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport3.Count() > 0)
                    {
                        var getItem8 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 8);
                        if (getItem8 != null)
                        {
                            var r1 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 1);
                            if (r1 != null)
                            {
                                getItem8.Month3 = (Funs.GetNewIntOrZero(getItem8.Month3) - r1.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem9 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 9);
                        if (getItem9 != null)
                        {
                            var r2 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 2);
                            if (r2 != null)
                            {
                                getItem9.Month3 = (Funs.GetNewIntOrZero(getItem9.Month3) - r2.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem10 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 10);
                        if (getItem10 != null)
                        {
                            var r3 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 3);
                            if (r3 != null)
                            {
                                getItem10.Month3 = (Funs.GetNewIntOrZero(getItem10.Month3) - r3.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem11 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 11);
                        if (getItem11 != null)
                        {
                            var r4 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 4);
                            if (r4 != null)
                            {
                                getItem11.Month3 = (Funs.GetNewIntOrZero(getItem11.Month3) - r4.MonthPersons ?? 0).ToString();
                            }
                        }

                        var getItem12 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 12);
                        if (getItem12 != null)
                        {
                            var r5 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 5);
                            if (r5 != null)
                            {
                                getItem12.Month3 = (Funs.GetNewIntOrZero(getItem12.Month3) - r5.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem13 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 13);
                        if (getItem13 != null)
                        {
                            var r6 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 6);
                            if (r6 != null)
                            {
                                getItem13.Month3 = (Funs.GetNewIntOrZero(getItem13.Month3) - r6.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem14 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 14);
                        if (getItem14 != null)
                        {
                            var r7 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 7);
                            if (r7 != null)
                            {
                                getItem14.Month3 = (Funs.GetNewIntOrZero(getItem14.Month3) - r7.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem15 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 15);
                        if (getItem15 != null)
                        {
                            var r8 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 8);
                            if (r8 != null)
                            {
                                getItem15.Month3 = (Funs.GetNewIntOrZero(getItem15.Month3) - r8.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem16 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 16);
                        if (getItem16 != null)
                        {
                            var r9 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 9);
                            if (r9 != null)
                            {
                                getItem16.Month3 = (Funs.GetNewIntOrZero(getItem16.Month3) - r9.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem17 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 17);
                        if (getItem17 != null)
                        {
                            var r10 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 10);
                            if (r10 != null)
                            {
                                getItem17.Month3 = (Funs.GetNewIntOrZero(getItem17.Month3) - r10.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem18 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 18);
                        if (getItem18 != null)
                        {
                            var r11 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 11);
                            if (r11 != null)
                            {
                                getItem18.Month3 = (Funs.GetNewIntOrZero(getItem18.Month3) - r11.MonthTimes ?? 0).ToString();
                            }
                        }
                    }
                    ////大型、特种设备
                    var getSeDin_MonthReport5 = from x in db.SeDin_MonthReport5
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    var getItem19 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 19);
                    if (getSeDin_MonthReport5.Count() > 0 && getItem19 != null)
                    {
                        int countSumR5 = getSeDin_MonthReport5.Sum(x => x.T01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.T04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T05 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T06 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.S01 ?? 0);
                        getItem19.Month3 = (Funs.GetNewIntOrZero(getItem19.Month3) - countSumR5).ToString();
                    }
                    ////安全生产费用
                    var getSeDin_MonthReport6 = db.SeDin_MonthReport6.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport6 != null)
                    {
                        var getItem20 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 20);
                        if (getItem20 != null)
                        {
                            getItem20.Month3 = (Funs.GetNewDecimalOrZero(getItem20.Month3) - getSeDin_MonthReport6.SumMonth ?? 0).ToString();
                        }
                    }
                    ////项目现场员工入场安全培训人数
                    var getSeDin_MonthReport7 = db.SeDin_MonthReport7.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport7 != null)
                    {
                        var getItem21 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 21);
                        if (getItem21 != null)
                        {
                            getItem21.Month3 = (Funs.GetNewIntOrZero(getItem21.Month3) - getSeDin_MonthReport7.EmployeeMontPerson ?? 0).ToString();
                        }
                    }
                    ////项目会次
                    var getSeDin_MonthReport8 = db.SeDin_MonthReport8.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport8 != null)
                    {
                        var getItem22 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 22);
                        if (getItem22 != null)
                        {
                            getItem22.Month3 = (Funs.GetNewIntOrZero(getItem22.Month3) - getSeDin_MonthReport8.MonthMontNum ?? 0).ToString();
                        }
                        var getItem23 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 23);
                        if (getItem23 != null)
                        {
                            getItem23.Month3 = (Funs.GetNewIntOrZero(getItem23.Month3) - getSeDin_MonthReport8.WeekMontNum ?? 0).ToString();
                        }
                        var getItem24 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 24);
                        if (getItem24 != null)
                        {
                            getItem24.Month3 = (Funs.GetNewIntOrZero(getItem24.Month3) - getSeDin_MonthReport8.SpecialMontPerson ?? 0).ToString();
                        }
                    }
                    /// 安全检查
                    var getSeDin_MonthReport9 = db.SeDin_MonthReport9.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport9 != null)
                    {
                        var getItem25 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 25);
                        if (getItem25 != null)
                        {
                            getItem25.Month3 = (Funs.GetNewIntOrZero(getItem25.Month3) - getSeDin_MonthReport9.DailyMonth ?? 0).ToString();
                        }
                        var getItem26 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 26);
                        if (getItem26 != null)
                        {
                            getItem26.Month3 = (Funs.GetNewIntOrZero(getItem26.Month3) - getSeDin_MonthReport9.WeekMonth ?? 0).ToString();
                        }
                        var getItem27 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 27);
                        if (getItem27 != null)
                        {
                            getItem27.Month3 = (Funs.GetNewIntOrZero(getItem27.Month3) - getSeDin_MonthReport9.SpecialMonth ?? 0).ToString();
                        }
                        var getItem28 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 28);
                        if (getItem28 != null)
                        {
                            getItem28.Month3 = (Funs.GetNewIntOrZero(getItem28.Month3) - getSeDin_MonthReport9.MonthlyMonth ?? 0).ToString();
                        }
                    }
                    /////隐患整改单
                    var getSeDin_MonthReport9Rectification = from x in db.SeDin_MonthReport9Item_Rectification where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Rectification.Count() > 0)
                    {
                        var getItem29 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 29);
                        if (getItem29 != null)
                        {
                            getItem29.Month3 = (Funs.GetNewIntOrZero(getItem29.Month3) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                        var getItem30 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 30);
                        if (getItem30 != null)
                        {
                            getItem30.Month3 = (Funs.GetNewIntOrZero(getItem30.Month3) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMoth ?? 0)).ToString();
                        }
                        var getItem31 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 31);
                        if (getItem31 != null)
                        {
                            getItem31.Month3 = (Funs.GetNewIntOrZero(getItem31.Month3) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthLarge ?? 0)).ToString();
                        }
                        var getItem32 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 32);
                        if (getItem32 != null)
                        {
                            getItem32.Month3 = (Funs.GetNewIntOrZero(getItem32.Month3) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothLarge ?? 0)).ToString();
                        }
                        var getItem33 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 33);
                        if (getItem33 != null)
                        {
                            getItem33.Month3 = (Funs.GetNewIntOrZero(getItem33.Month3) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthSerious ?? 0)).ToString();
                        }
                        var getItem34 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 34);
                        if (getItem34 != null)
                        {
                            getItem34.Month3 = (Funs.GetNewIntOrZero(getItem34.Month3) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothSerious ?? 0)).ToString();
                        }
                    }
                    /////停工令
                    var getSeDin_MonthReport9Stoppage = from x in db.SeDin_MonthReport9Item_Stoppage where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Stoppage.Count() > 0)
                    {
                        var getItem35 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 35);
                        if (getItem35 != null)
                        {
                            getItem35.Month3 = (Funs.GetNewIntOrZero(getItem35.Month3) - getSeDin_MonthReport9Stoppage.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                    }
                    ////奖惩
                    var getSeDin_MonthReport10 = db.SeDin_MonthReport10.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport10 != null)
                    {
                        var getItem36 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 36);
                        if (getItem36 != null)
                        {
                            getItem36.Month3 = (Funs.GetNewDecimalOrZero(getItem36.Month3) - getSeDin_MonthReport10.SafeMonthMoney ?? 0 - getSeDin_MonthReport10.HseMonthMoney ?? 0 - getSeDin_MonthReport10.ProduceMonthMoney ?? 0).ToString();
                        }
                        var getItem37 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 37);
                        if (getItem37 != null)
                        {
                            getItem37.Month3 = (Funs.GetNewDecimalOrZero(getItem37.Month3) - getSeDin_MonthReport10.AccidentMonthMoney ?? 0 - getSeDin_MonthReport10.ViolationMonthMoney ?? 0 - getSeDin_MonthReport10.ManageMonthMoney ?? 0).ToString();
                        }
                    }
                    ////危大工程
                    var getSeDin_MonthReport11 = db.SeDin_MonthReport11.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport11 != null)
                    {
                        var getItem38 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 38);
                        if (getItem38 != null)
                        {
                            getItem38.Month3 = (Funs.GetNewIntOrZero(getItem38.Month3) - getSeDin_MonthReport11.RiskWorkNum ?? 0).ToString();
                        }
                        var getItem39 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 39);
                        if (getItem39 != null)
                        {
                            getItem39.Month3 = (Funs.GetNewIntOrZero(getItem39.Month3) - getSeDin_MonthReport11.LargeWorkNum ?? 0).ToString();
                        }
                    }
                    ////应急
                    var getSeDin_MonthReport12 = db.SeDin_MonthReport12.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport12 != null)
                    {
                        var getItem40 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 40);
                        if (getItem40 != null)
                        {
                            getItem40.Month3 = (Funs.GetNewIntOrZero(getItem40.Month3) - getSeDin_MonthReport12.MultipleSiteNum ?? 0 - getSeDin_MonthReport12.MultipleDesktopNum ?? 0
                               - getSeDin_MonthReport12.SingleSiteNum ?? 0 - getSeDin_MonthReport12.SingleDesktopNum ?? 0).ToString();
                        }
                    }
                    db.SubmitChanges();
                }
                #endregion
            }
            else if (monthReport.ReporMonth.Value.Month == 4)
            {
                #region 数据调整
                bool isSave = false;
                var getSubmissionMonth = getHSEDataCollectISubmissionYear.FirstOrDefault(x => x.ProjectId == monthReport.ProjectId);
                if (getSubmissionMonth != null && Funs.GetNewDateTime(getSubmissionMonth.Month4).HasValue)
                {
                    isSave = true;
                }
                if (isSave)
                {
                    ////员工
                    var getSeDin_MonthReport4Other = db.SeDin_MonthReport4Other.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport4Other != null)
                    {
                        var getItem1 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 1);
                        if (getItem1 != null)
                        {
                            getItem1.Month4 = (Funs.GetNewIntOrZero(getItem1.Month4) - getSeDin_MonthReport4Other.TotalNum ?? 0).ToString();
                        }
                        var getItem2 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 2);
                        if (getItem2 != null)
                        {
                            getItem2.Month4 = (Funs.GetNewIntOrZero(getItem2.Month4) - getSeDin_MonthReport4Other.OutsideNum ?? 0).ToString();
                        }
                        var getItem3 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 3);
                        if (getItem3 != null)
                        {
                            getItem3.Month4 = (Funs.GetNewIntOrZero(getItem3.Month4) - getSeDin_MonthReport4Other.ForeignNum ?? 0).ToString();
                        }
                        var getItem4 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 4);
                        if (getItem4 != null)
                        {
                            getItem4.Month4 = (Funs.GetNewIntOrZero(getItem4.Month4) - getSeDin_MonthReport4Other.ManagerNum ?? 0).ToString();
                        }
                    }
                    ////分包人数
                    var getSeDin_MonthReport4 = from x in db.SeDin_MonthReport4
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport4.Count() > 0)
                    {
                        var getItem5 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 5);
                        if (getItem5 != null)
                        {
                            getItem5.Month4 = (Funs.GetNewIntOrZero(getItem5.Month4) - getSeDin_MonthReport4.Sum(x => x.TotalNum ?? 0)).ToString();
                        }
                        var getItem6 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 6);
                        if (getItem6 != null)
                        {
                            getItem6.Month4 = (Funs.GetNewIntOrZero(getItem6.Month4) - getSeDin_MonthReport4.Sum(x => x.SafeManangerNum ?? 0)).ToString();
                        }
                    }
                    ///安全人工时
                    var getSeDin_MonthReport2 = db.SeDin_MonthReport2.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport2 != null)
                    {
                        var getItem7 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 7);
                        if (getItem7 != null)
                        {
                            getItem7.Month4 = (Funs.GetNewDecimalOrZero(getItem7.Month4) - getSeDin_MonthReport2.MonthWorkTime ?? 0).ToString();
                        }
                    }
                    ////事故
                    var getSeDin_MonthReport3 = from x in db.SeDin_MonthReport3
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport3.Count() > 0)
                    {
                        var getItem8 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 8);
                        if (getItem8 != null)
                        {
                            var r1 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 1);
                            if (r1 != null)
                            {
                                getItem8.Month4 = (Funs.GetNewIntOrZero(getItem8.Month4) - r1.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem9 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 9);
                        if (getItem9 != null)
                        {
                            var r2 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 2);
                            if (r2 != null)
                            {
                                getItem9.Month4 = (Funs.GetNewIntOrZero(getItem9.Month4) - r2.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem10 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 10);
                        if (getItem10 != null)
                        {
                            var r3 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 3);
                            if (r3 != null)
                            {
                                getItem10.Month4 = (Funs.GetNewIntOrZero(getItem10.Month4) - r3.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem11 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 11);
                        if (getItem11 != null)
                        {
                            var r4 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 4);
                            if (r4 != null)
                            {
                                getItem11.Month4 = (Funs.GetNewIntOrZero(getItem11.Month4) - r4.MonthPersons ?? 0).ToString();
                            }
                        }

                        var getItem12 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 12);
                        if (getItem12 != null)
                        {
                            var r5 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 5);
                            if (r5 != null)
                            {
                                getItem12.Month4 = (Funs.GetNewIntOrZero(getItem12.Month4) - r5.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem13 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 13);
                        if (getItem13 != null)
                        {
                            var r6 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 6);
                            if (r6 != null)
                            {
                                getItem13.Month4 = (Funs.GetNewIntOrZero(getItem13.Month4) - r6.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem14 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 14);
                        if (getItem14 != null)
                        {
                            var r7 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 7);
                            if (r7 != null)
                            {
                                getItem14.Month4 = (Funs.GetNewIntOrZero(getItem14.Month4) - r7.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem15 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 15);
                        if (getItem15 != null)
                        {
                            var r8 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 8);
                            if (r8 != null)
                            {
                                getItem15.Month4 = (Funs.GetNewIntOrZero(getItem15.Month4) - r8.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem16 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 16);
                        if (getItem16 != null)
                        {
                            var r9 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 9);
                            if (r9 != null)
                            {
                                getItem16.Month4 = (Funs.GetNewIntOrZero(getItem16.Month4) - r9.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem17 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 17);
                        if (getItem17 != null)
                        {
                            var r10 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 10);
                            if (r10 != null)
                            {
                                getItem17.Month4 = (Funs.GetNewIntOrZero(getItem17.Month4) - r10.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem18 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 18);
                        if (getItem18 != null)
                        {
                            var r11 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 11);
                            if (r11 != null)
                            {
                                getItem18.Month4 = (Funs.GetNewIntOrZero(getItem18.Month4) - r11.MonthTimes ?? 0).ToString();
                            }
                        }
                    }
                    ////大型、特种设备
                    var getSeDin_MonthReport5 = from x in db.SeDin_MonthReport5
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    var getItem19 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 19);
                    if (getSeDin_MonthReport5.Count() > 0 && getItem19 != null)
                    {
                        int countSumR5 = getSeDin_MonthReport5.Sum(x => x.T01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.T04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T05 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T06 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.S01 ?? 0);
                        getItem19.Month4 = (Funs.GetNewIntOrZero(getItem19.Month4) - countSumR5).ToString();
                    }
                    ////安全生产费用
                    var getSeDin_MonthReport6 = db.SeDin_MonthReport6.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport6 != null)
                    {
                        var getItem20 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 20);
                        if (getItem20 != null)
                        {
                            getItem20.Month4 = (Funs.GetNewDecimalOrZero(getItem20.Month4) - getSeDin_MonthReport6.SumMonth ?? 0).ToString();
                        }
                    }
                    ////项目现场员工入场安全培训人数
                    var getSeDin_MonthReport7 = db.SeDin_MonthReport7.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport7 != null)
                    {
                        var getItem21 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 21);
                        if (getItem21 != null)
                        {
                            getItem21.Month4 = (Funs.GetNewIntOrZero(getItem21.Month4) - getSeDin_MonthReport7.EmployeeMontPerson ?? 0).ToString();
                        }
                    }
                    ////项目会次
                    var getSeDin_MonthReport8 = db.SeDin_MonthReport8.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport8 != null)
                    {
                        var getItem22 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 22);
                        if (getItem22 != null)
                        {
                            getItem22.Month4 = (Funs.GetNewIntOrZero(getItem22.Month4) - getSeDin_MonthReport8.MonthMontNum ?? 0).ToString();
                        }
                        var getItem23 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 23);
                        if (getItem23 != null)
                        {
                            getItem23.Month4 = (Funs.GetNewIntOrZero(getItem23.Month4) - getSeDin_MonthReport8.WeekMontNum ?? 0).ToString();
                        }
                        var getItem24 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 24);
                        if (getItem24 != null)
                        {
                            getItem24.Month4 = (Funs.GetNewIntOrZero(getItem24.Month4) - getSeDin_MonthReport8.SpecialMontPerson ?? 0).ToString();
                        }
                    }
                    /// 安全检查
                    var getSeDin_MonthReport9 = db.SeDin_MonthReport9.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport9 != null)
                    {
                        var getItem25 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 25);
                        if (getItem25 != null)
                        {
                            getItem25.Month4 = (Funs.GetNewIntOrZero(getItem25.Month4) - getSeDin_MonthReport9.DailyMonth ?? 0).ToString();
                        }
                        var getItem26 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 26);
                        if (getItem26 != null)
                        {
                            getItem26.Month4 = (Funs.GetNewIntOrZero(getItem26.Month4) - getSeDin_MonthReport9.WeekMonth ?? 0).ToString();
                        }
                        var getItem27 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 27);
                        if (getItem27 != null)
                        {
                            getItem27.Month4 = (Funs.GetNewIntOrZero(getItem27.Month4) - getSeDin_MonthReport9.SpecialMonth ?? 0).ToString();
                        }
                        var getItem28 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 28);
                        if (getItem28 != null)
                        {
                            getItem28.Month4 = (Funs.GetNewIntOrZero(getItem28.Month4) - getSeDin_MonthReport9.MonthlyMonth ?? 0).ToString();
                        }
                    }
                    /////隐患整改单
                    var getSeDin_MonthReport9Rectification = from x in db.SeDin_MonthReport9Item_Rectification where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Rectification.Count() > 0)
                    {
                        var getItem29 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 29);
                        if (getItem29 != null)
                        {
                            getItem29.Month4 = (Funs.GetNewIntOrZero(getItem29.Month4) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                        var getItem30 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 30);
                        if (getItem30 != null)
                        {
                            getItem30.Month4 = (Funs.GetNewIntOrZero(getItem30.Month4) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMoth ?? 0)).ToString();
                        }
                        var getItem31 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 31);
                        if (getItem31 != null)
                        {
                            getItem31.Month4 = (Funs.GetNewIntOrZero(getItem31.Month4) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthLarge ?? 0)).ToString();
                        }
                        var getItem32 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 32);
                        if (getItem32 != null)
                        {
                            getItem32.Month4 = (Funs.GetNewIntOrZero(getItem32.Month4) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothLarge ?? 0)).ToString();
                        }
                        var getItem33 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 33);
                        if (getItem33 != null)
                        {
                            getItem33.Month4 = (Funs.GetNewIntOrZero(getItem33.Month4) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthSerious ?? 0)).ToString();
                        }
                        var getItem34 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 34);
                        if (getItem34 != null)
                        {
                            getItem34.Month4 = (Funs.GetNewIntOrZero(getItem34.Month4) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothSerious ?? 0)).ToString();
                        }
                    }
                    /////停工令
                    var getSeDin_MonthReport9Stoppage = from x in db.SeDin_MonthReport9Item_Stoppage where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Stoppage.Count() > 0)
                    {
                        var getItem35 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 35);
                        if (getItem35 != null)
                        {
                            getItem35.Month4 = (Funs.GetNewIntOrZero(getItem35.Month4) - getSeDin_MonthReport9Stoppage.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                    }
                    ////奖惩
                    var getSeDin_MonthReport10 = db.SeDin_MonthReport10.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport10 != null)
                    {
                        var getItem36 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 36);
                        if (getItem36 != null)
                        {
                            getItem36.Month4 = (Funs.GetNewDecimalOrZero(getItem36.Month4) - getSeDin_MonthReport10.SafeMonthMoney ?? 0 - getSeDin_MonthReport10.HseMonthMoney ?? 0 - getSeDin_MonthReport10.ProduceMonthMoney ?? 0).ToString();
                        }
                        var getItem37 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 37);
                        if (getItem37 != null)
                        {
                            getItem37.Month4 = (Funs.GetNewDecimalOrZero(getItem37.Month4) - getSeDin_MonthReport10.AccidentMonthMoney ?? 0 - getSeDin_MonthReport10.ViolationMonthMoney ?? 0 - getSeDin_MonthReport10.ManageMonthMoney ?? 0).ToString();
                        }
                    }
                    ////危大工程
                    var getSeDin_MonthReport11 = db.SeDin_MonthReport11.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport11 != null)
                    {
                        var getItem38 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 38);
                        if (getItem38 != null)
                        {
                            getItem38.Month4 = (Funs.GetNewIntOrZero(getItem38.Month4) - getSeDin_MonthReport11.RiskWorkNum ?? 0).ToString();
                        }
                        var getItem39 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 39);
                        if (getItem39 != null)
                        {
                            getItem39.Month4 = (Funs.GetNewIntOrZero(getItem39.Month4) - getSeDin_MonthReport11.LargeWorkNum ?? 0).ToString();
                        }
                    }
                    ////应急
                    var getSeDin_MonthReport12 = db.SeDin_MonthReport12.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport12 != null)
                    {
                        var getItem40 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 40);
                        if (getItem40 != null)
                        {
                            getItem40.Month4 = (Funs.GetNewIntOrZero(getItem40.Month4) - getSeDin_MonthReport12.MultipleSiteNum ?? 0 - getSeDin_MonthReport12.MultipleDesktopNum ?? 0
                               - getSeDin_MonthReport12.SingleSiteNum ?? 0 - getSeDin_MonthReport12.SingleDesktopNum ?? 0).ToString();
                        }
                    }
                    db.SubmitChanges();
                }
                #endregion
            }
            else if (monthReport.ReporMonth.Value.Month == 5)
            {
                #region 数据调整
                bool isSave = false;
                var getSubmissionMonth = getHSEDataCollectISubmissionYear.FirstOrDefault(x => x.ProjectId == monthReport.ProjectId);
                if (getSubmissionMonth != null && Funs.GetNewDateTime(getSubmissionMonth.Month5).HasValue)
                {
                    isSave = true;
                }
                if (isSave)
                {
                    ////员工
                    var getSeDin_MonthReport4Other = db.SeDin_MonthReport4Other.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport4Other != null)
                    {
                        var getItem1 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 1);
                        if (getItem1 != null)
                        {
                            getItem1.Month5 = (Funs.GetNewIntOrZero(getItem1.Month5) - getSeDin_MonthReport4Other.TotalNum ?? 0).ToString();
                        }
                        var getItem2 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 2);
                        if (getItem2 != null)
                        {
                            getItem2.Month5 = (Funs.GetNewIntOrZero(getItem2.Month5) - getSeDin_MonthReport4Other.OutsideNum ?? 0).ToString();
                        }
                        var getItem3 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 3);
                        if (getItem3 != null)
                        {
                            getItem3.Month5 = (Funs.GetNewIntOrZero(getItem3.Month5) - getSeDin_MonthReport4Other.ForeignNum ?? 0).ToString();
                        }
                        var getItem4 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 4);
                        if (getItem4 != null)
                        {
                            getItem4.Month5 = (Funs.GetNewIntOrZero(getItem4.Month5) - getSeDin_MonthReport4Other.ManagerNum ?? 0).ToString();
                        }
                    }
                    ////分包人数
                    var getSeDin_MonthReport4 = from x in db.SeDin_MonthReport4
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport4.Count() > 0)
                    {
                        var getItem5 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 5);
                        if (getItem5 != null)
                        {
                            getItem5.Month5 = (Funs.GetNewIntOrZero(getItem5.Month5) - getSeDin_MonthReport4.Sum(x => x.TotalNum ?? 0)).ToString();
                        }
                        var getItem6 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 6);
                        if (getItem6 != null)
                        {
                            getItem6.Month5 = (Funs.GetNewIntOrZero(getItem6.Month5) - getSeDin_MonthReport4.Sum(x => x.SafeManangerNum ?? 0)).ToString();
                        }
                    }
                    ///安全人工时
                    var getSeDin_MonthReport2 = db.SeDin_MonthReport2.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport2 != null)
                    {
                        var getItem7 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 7);
                        if (getItem7 != null)
                        {
                            getItem7.Month5 = (Funs.GetNewDecimalOrZero(getItem7.Month5) - getSeDin_MonthReport2.MonthWorkTime ?? 0).ToString();
                        }
                    }
                    ////事故
                    var getSeDin_MonthReport3 = from x in db.SeDin_MonthReport3
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport3.Count() > 0)
                    {
                        var getItem8 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 8);
                        if (getItem8 != null)
                        {
                            var r1 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 1);
                            if (r1 != null)
                            {
                                getItem8.Month5 = (Funs.GetNewIntOrZero(getItem8.Month5) - r1.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem9 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 9);
                        if (getItem9 != null)
                        {
                            var r2 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 2);
                            if (r2 != null)
                            {
                                getItem9.Month5 = (Funs.GetNewIntOrZero(getItem9.Month5) - r2.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem10 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 10);
                        if (getItem10 != null)
                        {
                            var r3 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 3);
                            if (r3 != null)
                            {
                                getItem10.Month5 = (Funs.GetNewIntOrZero(getItem10.Month5) - r3.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem11 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 11);
                        if (getItem11 != null)
                        {
                            var r4 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 4);
                            if (r4 != null)
                            {
                                getItem11.Month5 = (Funs.GetNewIntOrZero(getItem11.Month5) - r4.MonthPersons ?? 0).ToString();
                            }
                        }

                        var getItem12 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 12);
                        if (getItem12 != null)
                        {
                            var r5 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 5);
                            if (r5 != null)
                            {
                                getItem12.Month5 = (Funs.GetNewIntOrZero(getItem12.Month5) - r5.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem13 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 13);
                        if (getItem13 != null)
                        {
                            var r6 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 6);
                            if (r6 != null)
                            {
                                getItem13.Month5 = (Funs.GetNewIntOrZero(getItem13.Month5) - r6.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem14 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 14);
                        if (getItem14 != null)
                        {
                            var r7 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 7);
                            if (r7 != null)
                            {
                                getItem14.Month5 = (Funs.GetNewIntOrZero(getItem14.Month5) - r7.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem15 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 15);
                        if (getItem15 != null)
                        {
                            var r8 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 8);
                            if (r8 != null)
                            {
                                getItem15.Month5 = (Funs.GetNewIntOrZero(getItem15.Month5) - r8.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem16 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 16);
                        if (getItem16 != null)
                        {
                            var r9 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 9);
                            if (r9 != null)
                            {
                                getItem16.Month5 = (Funs.GetNewIntOrZero(getItem16.Month5) - r9.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem17 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 17);
                        if (getItem17 != null)
                        {
                            var r10 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 10);
                            if (r10 != null)
                            {
                                getItem17.Month5 = (Funs.GetNewIntOrZero(getItem17.Month5) - r10.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem18 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 18);
                        if (getItem18 != null)
                        {
                            var r11 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 11);
                            if (r11 != null)
                            {
                                getItem18.Month5 = (Funs.GetNewIntOrZero(getItem18.Month5) - r11.MonthTimes ?? 0).ToString();
                            }
                        }
                    }
                    ////大型、特种设备
                    var getSeDin_MonthReport5 = from x in db.SeDin_MonthReport5
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    var getItem19 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 19);
                    if (getSeDin_MonthReport5.Count() > 0 && getItem19 != null)
                    {
                        int countSumR5 = getSeDin_MonthReport5.Sum(x => x.T01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.T04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T05 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T06 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.S01 ?? 0);
                        getItem19.Month5 = (Funs.GetNewIntOrZero(getItem19.Month5) - countSumR5).ToString();
                    }
                    ////安全生产费用
                    var getSeDin_MonthReport6 = db.SeDin_MonthReport6.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport6 != null)
                    {
                        var getItem20 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 20);
                        if (getItem20 != null)
                        {
                            getItem20.Month5 = (Funs.GetNewDecimalOrZero(getItem20.Month5) - getSeDin_MonthReport6.SumMonth ?? 0).ToString();
                        }
                    }
                    ////项目现场员工入场安全培训人数
                    var getSeDin_MonthReport7 = db.SeDin_MonthReport7.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport7 != null)
                    {
                        var getItem21 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 21);
                        if (getItem21 != null)
                        {
                            getItem21.Month5 = (Funs.GetNewIntOrZero(getItem21.Month5) - getSeDin_MonthReport7.EmployeeMontPerson ?? 0).ToString();
                        }
                    }
                    ////项目会次
                    var getSeDin_MonthReport8 = db.SeDin_MonthReport8.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport8 != null)
                    {
                        var getItem22 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 22);
                        if (getItem22 != null)
                        {
                            getItem22.Month5 = (Funs.GetNewIntOrZero(getItem22.Month5) - getSeDin_MonthReport8.MonthMontNum ?? 0).ToString();
                        }
                        var getItem23 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 23);
                        if (getItem23 != null)
                        {
                            getItem23.Month5 = (Funs.GetNewIntOrZero(getItem23.Month5) - getSeDin_MonthReport8.WeekMontNum ?? 0).ToString();
                        }
                        var getItem24 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 24);
                        if (getItem24 != null)
                        {
                            getItem24.Month5 = (Funs.GetNewIntOrZero(getItem24.Month5) - getSeDin_MonthReport8.SpecialMontPerson ?? 0).ToString();
                        }
                    }
                    /// 安全检查
                    var getSeDin_MonthReport9 = db.SeDin_MonthReport9.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport9 != null)
                    {
                        var getItem25 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 25);
                        if (getItem25 != null)
                        {
                            getItem25.Month5 = (Funs.GetNewIntOrZero(getItem25.Month5) - getSeDin_MonthReport9.DailyMonth ?? 0).ToString();
                        }
                        var getItem26 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 26);
                        if (getItem26 != null)
                        {
                            getItem26.Month5 = (Funs.GetNewIntOrZero(getItem26.Month5) - getSeDin_MonthReport9.WeekMonth ?? 0).ToString();
                        }
                        var getItem27 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 27);
                        if (getItem27 != null)
                        {
                            getItem27.Month5 = (Funs.GetNewIntOrZero(getItem27.Month5) - getSeDin_MonthReport9.SpecialMonth ?? 0).ToString();
                        }
                        var getItem28 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 28);
                        if (getItem28 != null)
                        {
                            getItem28.Month5 = (Funs.GetNewIntOrZero(getItem28.Month5) - getSeDin_MonthReport9.MonthlyMonth ?? 0).ToString();
                        }
                    }
                    /////隐患整改单
                    var getSeDin_MonthReport9Rectification = from x in db.SeDin_MonthReport9Item_Rectification where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Rectification.Count() > 0)
                    {
                        var getItem29 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 29);
                        if (getItem29 != null)
                        {
                            getItem29.Month5 = (Funs.GetNewIntOrZero(getItem29.Month5) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                        var getItem30 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 30);
                        if (getItem30 != null)
                        {
                            getItem30.Month5 = (Funs.GetNewIntOrZero(getItem30.Month5) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMoth ?? 0)).ToString();
                        }
                        var getItem31 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 31);
                        if (getItem31 != null)
                        {
                            getItem31.Month5 = (Funs.GetNewIntOrZero(getItem31.Month5) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthLarge ?? 0)).ToString();
                        }
                        var getItem32 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 32);
                        if (getItem32 != null)
                        {
                            getItem32.Month5 = (Funs.GetNewIntOrZero(getItem32.Month5) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothLarge ?? 0)).ToString();
                        }
                        var getItem33 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 33);
                        if (getItem33 != null)
                        {
                            getItem33.Month5 = (Funs.GetNewIntOrZero(getItem33.Month5) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthSerious ?? 0)).ToString();
                        }
                        var getItem34 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 34);
                        if (getItem34 != null)
                        {
                            getItem34.Month5 = (Funs.GetNewIntOrZero(getItem34.Month5) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothSerious ?? 0)).ToString();
                        }
                    }
                    /////停工令
                    var getSeDin_MonthReport9Stoppage = from x in db.SeDin_MonthReport9Item_Stoppage where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Stoppage.Count() > 0)
                    {
                        var getItem35 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 35);
                        if (getItem35 != null)
                        {
                            getItem35.Month5 = (Funs.GetNewIntOrZero(getItem35.Month5) - getSeDin_MonthReport9Stoppage.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                    }
                    ////奖惩
                    var getSeDin_MonthReport10 = db.SeDin_MonthReport10.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport10 != null)
                    {
                        var getItem36 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 36);
                        if (getItem36 != null)
                        {
                            getItem36.Month5 = (Funs.GetNewDecimalOrZero(getItem36.Month5) - getSeDin_MonthReport10.SafeMonthMoney ?? 0 - getSeDin_MonthReport10.HseMonthMoney ?? 0 - getSeDin_MonthReport10.ProduceMonthMoney ?? 0).ToString();
                        }
                        var getItem37 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 37);
                        if (getItem37 != null)
                        {
                            getItem37.Month5 = (Funs.GetNewDecimalOrZero(getItem37.Month5) - getSeDin_MonthReport10.AccidentMonthMoney ?? 0 - getSeDin_MonthReport10.ViolationMonthMoney ?? 0 - getSeDin_MonthReport10.ManageMonthMoney ?? 0).ToString();
                        }
                    }
                    ////危大工程
                    var getSeDin_MonthReport11 = db.SeDin_MonthReport11.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport11 != null)
                    {
                        var getItem38 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 38);
                        if (getItem38 != null)
                        {
                            getItem38.Month5 = (Funs.GetNewIntOrZero(getItem38.Month5) - getSeDin_MonthReport11.RiskWorkNum ?? 0).ToString();
                        }
                        var getItem39 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 39);
                        if (getItem39 != null)
                        {
                            getItem39.Month5 = (Funs.GetNewIntOrZero(getItem39.Month5) - getSeDin_MonthReport11.LargeWorkNum ?? 0).ToString();
                        }
                    }
                    ////应急
                    var getSeDin_MonthReport12 = db.SeDin_MonthReport12.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport12 != null)
                    {
                        var getItem40 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 40);
                        if (getItem40 != null)
                        {
                            getItem40.Month5 = (Funs.GetNewIntOrZero(getItem40.Month5) - getSeDin_MonthReport12.MultipleSiteNum ?? 0 - getSeDin_MonthReport12.MultipleDesktopNum ?? 0
                               - getSeDin_MonthReport12.SingleSiteNum ?? 0 - getSeDin_MonthReport12.SingleDesktopNum ?? 0).ToString();
                        }
                    }
                    db.SubmitChanges();
                }
                #endregion
            }
            else if (monthReport.ReporMonth.Value.Month == 6)
            {
                #region 数据调整
                bool isSave = false;
                var getSubmissionMonth = getHSEDataCollectISubmissionYear.FirstOrDefault(x => x.ProjectId == monthReport.ProjectId);
                if (getSubmissionMonth != null && Funs.GetNewDateTime(getSubmissionMonth.Month6).HasValue)
                {
                    isSave = true;
                }
                if (isSave)
                {
                    ////员工
                    var getSeDin_MonthReport4Other = db.SeDin_MonthReport4Other.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport4Other != null)
                    {
                        var getItem1 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 1);
                        if (getItem1 != null)
                        {
                            getItem1.Month6 = (Funs.GetNewIntOrZero(getItem1.Month6) - getSeDin_MonthReport4Other.TotalNum ?? 0).ToString();
                        }
                        var getItem2 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 2);
                        if (getItem2 != null)
                        {
                            getItem2.Month6 = (Funs.GetNewIntOrZero(getItem2.Month6) - getSeDin_MonthReport4Other.OutsideNum ?? 0).ToString();
                        }
                        var getItem3 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 3);
                        if (getItem3 != null)
                        {
                            getItem3.Month6 = (Funs.GetNewIntOrZero(getItem3.Month6) - getSeDin_MonthReport4Other.ForeignNum ?? 0).ToString();
                        }
                        var getItem4 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 4);
                        if (getItem4 != null)
                        {
                            getItem4.Month6 = (Funs.GetNewIntOrZero(getItem4.Month6) - getSeDin_MonthReport4Other.ManagerNum ?? 0).ToString();
                        }
                    }
                    ////分包人数
                    var getSeDin_MonthReport4 = from x in db.SeDin_MonthReport4
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport4.Count() > 0)
                    {
                        var getItem5 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 5);
                        if (getItem5 != null)
                        {
                            getItem5.Month6 = (Funs.GetNewIntOrZero(getItem5.Month6) - getSeDin_MonthReport4.Sum(x => x.TotalNum ?? 0)).ToString();
                        }
                        var getItem6 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 6);
                        if (getItem6 != null)
                        {
                            getItem6.Month6 = (Funs.GetNewIntOrZero(getItem6.Month6) - getSeDin_MonthReport4.Sum(x => x.SafeManangerNum ?? 0)).ToString();
                        }
                    }
                    ///安全人工时
                    var getSeDin_MonthReport2 = db.SeDin_MonthReport2.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport2 != null)
                    {
                        var getItem7 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 7);
                        if (getItem7 != null)
                        {
                            getItem7.Month6 = (Funs.GetNewDecimalOrZero(getItem7.Month6) - getSeDin_MonthReport2.MonthWorkTime ?? 0).ToString();
                        }
                    }
                    ////事故
                    var getSeDin_MonthReport3 = from x in db.SeDin_MonthReport3
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport3.Count() > 0)
                    {
                        var getItem8 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 8);
                        if (getItem8 != null)
                        {
                            var r1 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 1);
                            if (r1 != null)
                            {
                                getItem8.Month6 = (Funs.GetNewIntOrZero(getItem8.Month6) - r1.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem9 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 9);
                        if (getItem9 != null)
                        {
                            var r2 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 2);
                            if (r2 != null)
                            {
                                getItem9.Month6 = (Funs.GetNewIntOrZero(getItem9.Month6) - r2.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem10 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 10);
                        if (getItem10 != null)
                        {
                            var r3 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 3);
                            if (r3 != null)
                            {
                                getItem10.Month6 = (Funs.GetNewIntOrZero(getItem10.Month6) - r3.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem11 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 11);
                        if (getItem11 != null)
                        {
                            var r4 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 4);
                            if (r4 != null)
                            {
                                getItem11.Month6 = (Funs.GetNewIntOrZero(getItem11.Month6) - r4.MonthPersons ?? 0).ToString();
                            }
                        }

                        var getItem12 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 12);
                        if (getItem12 != null)
                        {
                            var r5 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 5);
                            if (r5 != null)
                            {
                                getItem12.Month6 = (Funs.GetNewIntOrZero(getItem12.Month6) - r5.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem13 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 13);
                        if (getItem13 != null)
                        {
                            var r6 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 6);
                            if (r6 != null)
                            {
                                getItem13.Month6 = (Funs.GetNewIntOrZero(getItem13.Month6) - r6.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem14 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 14);
                        if (getItem14 != null)
                        {
                            var r7 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 7);
                            if (r7 != null)
                            {
                                getItem14.Month6 = (Funs.GetNewIntOrZero(getItem14.Month6) - r7.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem15 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 15);
                        if (getItem15 != null)
                        {
                            var r8 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 8);
                            if (r8 != null)
                            {
                                getItem15.Month6 = (Funs.GetNewIntOrZero(getItem15.Month6) - r8.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem16 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 16);
                        if (getItem16 != null)
                        {
                            var r9 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 9);
                            if (r9 != null)
                            {
                                getItem16.Month6 = (Funs.GetNewIntOrZero(getItem16.Month6) - r9.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem17 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 17);
                        if (getItem17 != null)
                        {
                            var r10 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 10);
                            if (r10 != null)
                            {
                                getItem17.Month6 = (Funs.GetNewIntOrZero(getItem17.Month6) - r10.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem18 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 18);
                        if (getItem18 != null)
                        {
                            var r11 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 11);
                            if (r11 != null)
                            {
                                getItem18.Month6 = (Funs.GetNewIntOrZero(getItem18.Month6) - r11.MonthTimes ?? 0).ToString();
                            }
                        }
                    }
                    ////大型、特种设备
                    var getSeDin_MonthReport5 = from x in db.SeDin_MonthReport5
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    var getItem19 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 19);
                    if (getSeDin_MonthReport5.Count() > 0 && getItem19 != null)
                    {
                        int countSumR5 = getSeDin_MonthReport5.Sum(x => x.T01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.T04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T05 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T06 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.S01 ?? 0);
                        getItem19.Month6 = (Funs.GetNewIntOrZero(getItem19.Month6) - countSumR5).ToString();
                    }
                    ////安全生产费用
                    var getSeDin_MonthReport6 = db.SeDin_MonthReport6.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport6 != null)
                    {
                        var getItem20 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 20);
                        if (getItem20 != null)
                        {
                            getItem20.Month6 = (Funs.GetNewDecimalOrZero(getItem20.Month6) - getSeDin_MonthReport6.SumMonth ?? 0).ToString();
                        }
                    }
                    ////项目现场员工入场安全培训人数
                    var getSeDin_MonthReport7 = db.SeDin_MonthReport7.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport7 != null)
                    {
                        var getItem21 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 21);
                        if (getItem21 != null)
                        {
                            getItem21.Month6 = (Funs.GetNewIntOrZero(getItem21.Month6) - getSeDin_MonthReport7.EmployeeMontPerson ?? 0).ToString();
                        }
                    }
                    ////项目会次
                    var getSeDin_MonthReport8 = db.SeDin_MonthReport8.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport8 != null)
                    {
                        var getItem22 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 22);
                        if (getItem22 != null)
                        {
                            getItem22.Month6 = (Funs.GetNewIntOrZero(getItem22.Month6) - getSeDin_MonthReport8.MonthMontNum ?? 0).ToString();
                        }
                        var getItem23 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 23);
                        if (getItem23 != null)
                        {
                            getItem23.Month6 = (Funs.GetNewIntOrZero(getItem23.Month6) - getSeDin_MonthReport8.WeekMontNum ?? 0).ToString();
                        }
                        var getItem24 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 24);
                        if (getItem24 != null)
                        {
                            getItem24.Month6 = (Funs.GetNewIntOrZero(getItem24.Month6) - getSeDin_MonthReport8.SpecialMontPerson ?? 0).ToString();
                        }
                    }
                    /// 安全检查
                    var getSeDin_MonthReport9 = db.SeDin_MonthReport9.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport9 != null)
                    {
                        var getItem25 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 25);
                        if (getItem25 != null)
                        {
                            getItem25.Month6 = (Funs.GetNewIntOrZero(getItem25.Month6) - getSeDin_MonthReport9.DailyMonth ?? 0).ToString();
                        }
                        var getItem26 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 26);
                        if (getItem26 != null)
                        {
                            getItem26.Month6 = (Funs.GetNewIntOrZero(getItem26.Month6) - getSeDin_MonthReport9.WeekMonth ?? 0).ToString();
                        }
                        var getItem27 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 27);
                        if (getItem27 != null)
                        {
                            getItem27.Month6 = (Funs.GetNewIntOrZero(getItem27.Month6) - getSeDin_MonthReport9.SpecialMonth ?? 0).ToString();
                        }
                        var getItem28 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 28);
                        if (getItem28 != null)
                        {
                            getItem28.Month6 = (Funs.GetNewIntOrZero(getItem28.Month6) - getSeDin_MonthReport9.MonthlyMonth ?? 0).ToString();
                        }
                    }
                    /////隐患整改单
                    var getSeDin_MonthReport9Rectification = from x in db.SeDin_MonthReport9Item_Rectification where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Rectification.Count() > 0)
                    {
                        var getItem29 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 29);
                        if (getItem29 != null)
                        {
                            getItem29.Month6 = (Funs.GetNewIntOrZero(getItem29.Month6) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                        var getItem30 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 30);
                        if (getItem30 != null)
                        {
                            getItem30.Month6 = (Funs.GetNewIntOrZero(getItem30.Month6) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMoth ?? 0)).ToString();
                        }
                        var getItem31 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 31);
                        if (getItem31 != null)
                        {
                            getItem31.Month6 = (Funs.GetNewIntOrZero(getItem31.Month6) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthLarge ?? 0)).ToString();
                        }
                        var getItem32 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 32);
                        if (getItem32 != null)
                        {
                            getItem32.Month6 = (Funs.GetNewIntOrZero(getItem32.Month6) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothLarge ?? 0)).ToString();
                        }
                        var getItem33 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 33);
                        if (getItem33 != null)
                        {
                            getItem33.Month6 = (Funs.GetNewIntOrZero(getItem33.Month6) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthSerious ?? 0)).ToString();
                        }
                        var getItem34 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 34);
                        if (getItem34 != null)
                        {
                            getItem34.Month6 = (Funs.GetNewIntOrZero(getItem34.Month6) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothSerious ?? 0)).ToString();
                        }
                    }
                    /////停工令
                    var getSeDin_MonthReport9Stoppage = from x in db.SeDin_MonthReport9Item_Stoppage where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Stoppage.Count() > 0)
                    {
                        var getItem35 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 35);
                        if (getItem35 != null)
                        {
                            getItem35.Month6 = (Funs.GetNewIntOrZero(getItem35.Month6) - getSeDin_MonthReport9Stoppage.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                    }
                    ////奖惩
                    var getSeDin_MonthReport10 = db.SeDin_MonthReport10.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport10 != null)
                    {
                        var getItem36 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 36);
                        if (getItem36 != null)
                        {
                            getItem36.Month6 = (Funs.GetNewDecimalOrZero(getItem36.Month6) - getSeDin_MonthReport10.SafeMonthMoney ?? 0 - getSeDin_MonthReport10.HseMonthMoney ?? 0 - getSeDin_MonthReport10.ProduceMonthMoney ?? 0).ToString();
                        }
                        var getItem37 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 37);
                        if (getItem37 != null)
                        {
                            getItem37.Month6 = (Funs.GetNewDecimalOrZero(getItem37.Month6) - getSeDin_MonthReport10.AccidentMonthMoney ?? 0 - getSeDin_MonthReport10.ViolationMonthMoney ?? 0 - getSeDin_MonthReport10.ManageMonthMoney ?? 0).ToString();
                        }
                    }
                    ////危大工程
                    var getSeDin_MonthReport11 = db.SeDin_MonthReport11.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport11 != null)
                    {
                        var getItem38 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 38);
                        if (getItem38 != null)
                        {
                            getItem38.Month6 = (Funs.GetNewIntOrZero(getItem38.Month6) - getSeDin_MonthReport11.RiskWorkNum ?? 0).ToString();
                        }
                        var getItem39 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 39);
                        if (getItem39 != null)
                        {
                            getItem39.Month6 = (Funs.GetNewIntOrZero(getItem39.Month6) - getSeDin_MonthReport11.LargeWorkNum ?? 0).ToString();
                        }
                    }
                    ////应急
                    var getSeDin_MonthReport12 = db.SeDin_MonthReport12.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport12 != null)
                    {
                        var getItem40 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 40);
                        if (getItem40 != null)
                        {
                            getItem40.Month6 = (Funs.GetNewIntOrZero(getItem40.Month6) - getSeDin_MonthReport12.MultipleSiteNum ?? 0 - getSeDin_MonthReport12.MultipleDesktopNum ?? 0
                               - getSeDin_MonthReport12.SingleSiteNum ?? 0 - getSeDin_MonthReport12.SingleDesktopNum ?? 0).ToString();
                        }
                    }
                    db.SubmitChanges();
                }
                #endregion
            }
            else if (monthReport.ReporMonth.Value.Month == 7)
            {
                #region 数据调整
                bool isSave = false;
                var getSubmissionMonth = getHSEDataCollectISubmissionYear.FirstOrDefault(x => x.ProjectId == monthReport.ProjectId);
                if (getSubmissionMonth != null && Funs.GetNewDateTime(getSubmissionMonth.Month7).HasValue)
                {
                    isSave = true;
                }
                if (isSave)
                {
                    ////员工
                    var getSeDin_MonthReport4Other = db.SeDin_MonthReport4Other.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport4Other != null)
                    {
                        var getItem1 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 1);
                        if (getItem1 != null)
                        {
                            getItem1.Month7 = (Funs.GetNewIntOrZero(getItem1.Month7) - getSeDin_MonthReport4Other.TotalNum ?? 0).ToString();
                        }
                        var getItem2 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 2);
                        if (getItem2 != null)
                        {
                            getItem2.Month7 = (Funs.GetNewIntOrZero(getItem2.Month7) - getSeDin_MonthReport4Other.OutsideNum ?? 0).ToString();
                        }
                        var getItem3 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 3);
                        if (getItem3 != null)
                        {
                            getItem3.Month7 = (Funs.GetNewIntOrZero(getItem3.Month7) - getSeDin_MonthReport4Other.ForeignNum ?? 0).ToString();
                        }
                        var getItem4 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 4);
                        if (getItem4 != null)
                        {
                            getItem4.Month7 = (Funs.GetNewIntOrZero(getItem4.Month7) - getSeDin_MonthReport4Other.ManagerNum ?? 0).ToString();
                        }
                    }
                    ////分包人数
                    var getSeDin_MonthReport4 = from x in db.SeDin_MonthReport4
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport4.Count() > 0)
                    {
                        var getItem5 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 5);
                        if (getItem5 != null)
                        {
                            getItem5.Month7 = (Funs.GetNewIntOrZero(getItem5.Month7) - getSeDin_MonthReport4.Sum(x => x.TotalNum ?? 0)).ToString();
                        }
                        var getItem6 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 6);
                        if (getItem6 != null)
                        {
                            getItem6.Month7 = (Funs.GetNewIntOrZero(getItem6.Month7) - getSeDin_MonthReport4.Sum(x => x.SafeManangerNum ?? 0)).ToString();
                        }
                    }
                    ///安全人工时
                    var getSeDin_MonthReport2 = db.SeDin_MonthReport2.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport2 != null)
                    {
                        var getItem7 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 7);
                        if (getItem7 != null)
                        {
                            getItem7.Month7 = (Funs.GetNewDecimalOrZero(getItem7.Month7) - getSeDin_MonthReport2.MonthWorkTime ?? 0).ToString();
                        }
                    }
                    ////事故
                    var getSeDin_MonthReport3 = from x in db.SeDin_MonthReport3
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport3.Count() > 0)
                    {
                        var getItem8 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 8);
                        if (getItem8 != null)
                        {
                            var r1 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 1);
                            if (r1 != null)
                            {
                                getItem8.Month7 = (Funs.GetNewIntOrZero(getItem8.Month7) - r1.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem9 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 9);
                        if (getItem9 != null)
                        {
                            var r2 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 2);
                            if (r2 != null)
                            {
                                getItem9.Month7 = (Funs.GetNewIntOrZero(getItem9.Month7) - r2.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem10 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 10);
                        if (getItem10 != null)
                        {
                            var r3 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 3);
                            if (r3 != null)
                            {
                                getItem10.Month7 = (Funs.GetNewIntOrZero(getItem10.Month7) - r3.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem11 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 11);
                        if (getItem11 != null)
                        {
                            var r4 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 4);
                            if (r4 != null)
                            {
                                getItem11.Month7 = (Funs.GetNewIntOrZero(getItem11.Month7) - r4.MonthPersons ?? 0).ToString();
                            }
                        }

                        var getItem12 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 12);
                        if (getItem12 != null)
                        {
                            var r5 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 5);
                            if (r5 != null)
                            {
                                getItem12.Month7 = (Funs.GetNewIntOrZero(getItem12.Month7) - r5.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem13 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 13);
                        if (getItem13 != null)
                        {
                            var r6 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 6);
                            if (r6 != null)
                            {
                                getItem13.Month7 = (Funs.GetNewIntOrZero(getItem13.Month7) - r6.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem14 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 14);
                        if (getItem14 != null)
                        {
                            var r7 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 7);
                            if (r7 != null)
                            {
                                getItem14.Month7 = (Funs.GetNewIntOrZero(getItem14.Month7) - r7.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem15 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 15);
                        if (getItem15 != null)
                        {
                            var r8 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 8);
                            if (r8 != null)
                            {
                                getItem15.Month7 = (Funs.GetNewIntOrZero(getItem15.Month7) - r8.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem16 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 16);
                        if (getItem16 != null)
                        {
                            var r9 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 9);
                            if (r9 != null)
                            {
                                getItem16.Month7 = (Funs.GetNewIntOrZero(getItem16.Month7) - r9.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem17 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 17);
                        if (getItem17 != null)
                        {
                            var r10 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 10);
                            if (r10 != null)
                            {
                                getItem17.Month7 = (Funs.GetNewIntOrZero(getItem17.Month7) - r10.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem18 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 18);
                        if (getItem18 != null)
                        {
                            var r11 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 11);
                            if (r11 != null)
                            {
                                getItem18.Month7 = (Funs.GetNewIntOrZero(getItem18.Month7) - r11.MonthTimes ?? 0).ToString();
                            }
                        }
                    }
                    ////大型、特种设备
                    var getSeDin_MonthReport5 = from x in db.SeDin_MonthReport5
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    var getItem19 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 19);
                    if (getSeDin_MonthReport5.Count() > 0 && getItem19 != null)
                    {
                        int countSumR5 = getSeDin_MonthReport5.Sum(x => x.T01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.T04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T05 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T06 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.S01 ?? 0);
                        getItem19.Month7 = (Funs.GetNewIntOrZero(getItem19.Month7) - countSumR5).ToString();
                    }
                    ////安全生产费用
                    var getSeDin_MonthReport6 = db.SeDin_MonthReport6.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport6 != null)
                    {
                        var getItem20 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 20);
                        if (getItem20 != null)
                        {
                            getItem20.Month7 = (Funs.GetNewDecimalOrZero(getItem20.Month7) - getSeDin_MonthReport6.SumMonth ?? 0).ToString();
                        }
                    }
                    ////项目现场员工入场安全培训人数
                    var getSeDin_MonthReport7 = db.SeDin_MonthReport7.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport7 != null)
                    {
                        var getItem21 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 21);
                        if (getItem21 != null)
                        {
                            getItem21.Month7 = (Funs.GetNewIntOrZero(getItem21.Month7) - getSeDin_MonthReport7.EmployeeMontPerson ?? 0).ToString();
                        }
                    }
                    ////项目会次
                    var getSeDin_MonthReport8 = db.SeDin_MonthReport8.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport8 != null)
                    {
                        var getItem22 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 22);
                        if (getItem22 != null)
                        {
                            getItem22.Month7 = (Funs.GetNewIntOrZero(getItem22.Month7) - getSeDin_MonthReport8.MonthMontNum ?? 0).ToString();
                        }
                        var getItem23 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 23);
                        if (getItem23 != null)
                        {
                            getItem23.Month7 = (Funs.GetNewIntOrZero(getItem23.Month7) - getSeDin_MonthReport8.WeekMontNum ?? 0).ToString();
                        }
                        var getItem24 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 24);
                        if (getItem24 != null)
                        {
                            getItem24.Month7 = (Funs.GetNewIntOrZero(getItem24.Month7) - getSeDin_MonthReport8.SpecialMontPerson ?? 0).ToString();
                        }
                    }
                    /// 安全检查
                    var getSeDin_MonthReport9 = db.SeDin_MonthReport9.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport9 != null)
                    {
                        var getItem25 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 25);
                        if (getItem25 != null)
                        {
                            getItem25.Month7 = (Funs.GetNewIntOrZero(getItem25.Month7) - getSeDin_MonthReport9.DailyMonth ?? 0).ToString();
                        }
                        var getItem26 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 26);
                        if (getItem26 != null)
                        {
                            getItem26.Month7 = (Funs.GetNewIntOrZero(getItem26.Month7) - getSeDin_MonthReport9.WeekMonth ?? 0).ToString();
                        }
                        var getItem27 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 27);
                        if (getItem27 != null)
                        {
                            getItem27.Month7 = (Funs.GetNewIntOrZero(getItem27.Month7) - getSeDin_MonthReport9.SpecialMonth ?? 0).ToString();
                        }
                        var getItem28 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 28);
                        if (getItem28 != null)
                        {
                            getItem28.Month7 = (Funs.GetNewIntOrZero(getItem28.Month7) - getSeDin_MonthReport9.MonthlyMonth ?? 0).ToString();
                        }
                    }
                    /////隐患整改单
                    var getSeDin_MonthReport9Rectification = from x in db.SeDin_MonthReport9Item_Rectification where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Rectification.Count() > 0)
                    {
                        var getItem29 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 29);
                        if (getItem29 != null)
                        {
                            getItem29.Month7 = (Funs.GetNewIntOrZero(getItem29.Month7) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                        var getItem30 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 30);
                        if (getItem30 != null)
                        {
                            getItem30.Month7 = (Funs.GetNewIntOrZero(getItem30.Month7) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMoth ?? 0)).ToString();
                        }
                        var getItem31 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 31);
                        if (getItem31 != null)
                        {
                            getItem31.Month7 = (Funs.GetNewIntOrZero(getItem31.Month7) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthLarge ?? 0)).ToString();
                        }
                        var getItem32 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 32);
                        if (getItem32 != null)
                        {
                            getItem32.Month7 = (Funs.GetNewIntOrZero(getItem32.Month7) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothLarge ?? 0)).ToString();
                        }
                        var getItem33 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 33);
                        if (getItem33 != null)
                        {
                            getItem33.Month7 = (Funs.GetNewIntOrZero(getItem33.Month7) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthSerious ?? 0)).ToString();
                        }
                        var getItem34 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 34);
                        if (getItem34 != null)
                        {
                            getItem34.Month7 = (Funs.GetNewIntOrZero(getItem34.Month7) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothSerious ?? 0)).ToString();
                        }
                    }
                    /////停工令
                    var getSeDin_MonthReport9Stoppage = from x in db.SeDin_MonthReport9Item_Stoppage where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Stoppage.Count() > 0)
                    {
                        var getItem35 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 35);
                        if (getItem35 != null)
                        {
                            getItem35.Month7 = (Funs.GetNewIntOrZero(getItem35.Month7) - getSeDin_MonthReport9Stoppage.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                    }
                    ////奖惩
                    var getSeDin_MonthReport10 = db.SeDin_MonthReport10.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport10 != null)
                    {
                        var getItem36 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 36);
                        if (getItem36 != null)
                        {
                            getItem36.Month7 = (Funs.GetNewDecimalOrZero(getItem36.Month7) - getSeDin_MonthReport10.SafeMonthMoney ?? 0 - getSeDin_MonthReport10.HseMonthMoney ?? 0 - getSeDin_MonthReport10.ProduceMonthMoney ?? 0).ToString();
                        }
                        var getItem37 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 37);
                        if (getItem37 != null)
                        {
                            getItem37.Month7 = (Funs.GetNewDecimalOrZero(getItem37.Month7) - getSeDin_MonthReport10.AccidentMonthMoney ?? 0 - getSeDin_MonthReport10.ViolationMonthMoney ?? 0 - getSeDin_MonthReport10.ManageMonthMoney ?? 0).ToString();
                        }
                    }
                    ////危大工程
                    var getSeDin_MonthReport11 = db.SeDin_MonthReport11.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport11 != null)
                    {
                        var getItem38 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 38);
                        if (getItem38 != null)
                        {
                            getItem38.Month7 = (Funs.GetNewIntOrZero(getItem38.Month7) - getSeDin_MonthReport11.RiskWorkNum ?? 0).ToString();
                        }
                        var getItem39 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 39);
                        if (getItem39 != null)
                        {
                            getItem39.Month7 = (Funs.GetNewIntOrZero(getItem39.Month7) - getSeDin_MonthReport11.LargeWorkNum ?? 0).ToString();
                        }
                    }
                    ////应急
                    var getSeDin_MonthReport12 = db.SeDin_MonthReport12.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport12 != null)
                    {
                        var getItem40 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 40);
                        if (getItem40 != null)
                        {
                            getItem40.Month7 = (Funs.GetNewIntOrZero(getItem40.Month7) - getSeDin_MonthReport12.MultipleSiteNum ?? 0 - getSeDin_MonthReport12.MultipleDesktopNum ?? 0
                               - getSeDin_MonthReport12.SingleSiteNum ?? 0 - getSeDin_MonthReport12.SingleDesktopNum ?? 0).ToString();
                        }
                    }
                    db.SubmitChanges();
                }
                #endregion                
            }
            else if (monthReport.ReporMonth.Value.Month == 8)
            {
                #region 数据调整
                bool isSave = false;
                var getSubmissionMonth = getHSEDataCollectISubmissionYear.FirstOrDefault(x => x.ProjectId == monthReport.ProjectId);
                if (getSubmissionMonth != null && Funs.GetNewDateTime(getSubmissionMonth.Month8).HasValue)
                {
                    isSave = true;
                }
                if (isSave)
                {
                    ////员工
                    var getSeDin_MonthReport4Other = db.SeDin_MonthReport4Other.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport4Other != null)
                    {
                        var getItem1 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 1);
                        if (getItem1 != null)
                        {
                            getItem1.Month8 = (Funs.GetNewIntOrZero(getItem1.Month8) - getSeDin_MonthReport4Other.TotalNum ?? 0).ToString();
                        }
                        var getItem2 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 2);
                        if (getItem2 != null)
                        {
                            getItem2.Month8 = (Funs.GetNewIntOrZero(getItem2.Month8) - getSeDin_MonthReport4Other.OutsideNum ?? 0).ToString();
                        }
                        var getItem3 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 3);
                        if (getItem3 != null)
                        {
                            getItem3.Month8 = (Funs.GetNewIntOrZero(getItem3.Month8) - getSeDin_MonthReport4Other.ForeignNum ?? 0).ToString();
                        }
                        var getItem4 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 4);
                        if (getItem4 != null)
                        {
                            getItem4.Month8 = (Funs.GetNewIntOrZero(getItem4.Month8) - getSeDin_MonthReport4Other.ManagerNum ?? 0).ToString();
                        }
                    }
                    ////分包人数
                    var getSeDin_MonthReport4 = from x in db.SeDin_MonthReport4
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport4.Count() > 0)
                    {
                        var getItem5 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 5);
                        if (getItem5 != null)
                        {
                            getItem5.Month8 = (Funs.GetNewIntOrZero(getItem5.Month8) - getSeDin_MonthReport4.Sum(x => x.TotalNum ?? 0)).ToString();
                        }
                        var getItem6 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 6);
                        if (getItem6 != null)
                        {
                            getItem6.Month8 = (Funs.GetNewIntOrZero(getItem6.Month8) - getSeDin_MonthReport4.Sum(x => x.SafeManangerNum ?? 0)).ToString();
                        }
                    }
                    ///安全人工时
                    var getSeDin_MonthReport2 = db.SeDin_MonthReport2.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport2 != null)
                    {
                        var getItem7 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 7);
                        if (getItem7 != null)
                        {
                            getItem7.Month8 = (Funs.GetNewDecimalOrZero(getItem7.Month8) - getSeDin_MonthReport2.MonthWorkTime ?? 0).ToString();
                        }
                    }
                    ////事故
                    var getSeDin_MonthReport3 = from x in db.SeDin_MonthReport3
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport3.Count() > 0)
                    {
                        var getItem8 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 8);
                        if (getItem8 != null)
                        {
                            var r1 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 1);
                            if (r1 != null)
                            {
                                getItem8.Month8 = (Funs.GetNewIntOrZero(getItem8.Month8) - r1.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem9 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 9);
                        if (getItem9 != null)
                        {
                            var r2 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 2);
                            if (r2 != null)
                            {
                                getItem9.Month8 = (Funs.GetNewIntOrZero(getItem9.Month8) - r2.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem10 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 10);
                        if (getItem10 != null)
                        {
                            var r3 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 3);
                            if (r3 != null)
                            {
                                getItem10.Month8 = (Funs.GetNewIntOrZero(getItem10.Month8) - r3.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem11 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 11);
                        if (getItem11 != null)
                        {
                            var r4 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 4);
                            if (r4 != null)
                            {
                                getItem11.Month8 = (Funs.GetNewIntOrZero(getItem11.Month8) - r4.MonthPersons ?? 0).ToString();
                            }
                        }

                        var getItem12 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 12);
                        if (getItem12 != null)
                        {
                            var r5 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 5);
                            if (r5 != null)
                            {
                                getItem12.Month8 = (Funs.GetNewIntOrZero(getItem12.Month8) - r5.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem13 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 13);
                        if (getItem13 != null)
                        {
                            var r6 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 6);
                            if (r6 != null)
                            {
                                getItem13.Month8 = (Funs.GetNewIntOrZero(getItem13.Month8) - r6.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem14 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 14);
                        if (getItem14 != null)
                        {
                            var r7 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 7);
                            if (r7 != null)
                            {
                                getItem14.Month8 = (Funs.GetNewIntOrZero(getItem14.Month8) - r7.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem15 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 15);
                        if (getItem15 != null)
                        {
                            var r8 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 8);
                            if (r8 != null)
                            {
                                getItem15.Month8 = (Funs.GetNewIntOrZero(getItem15.Month8) - r8.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem16 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 16);
                        if (getItem16 != null)
                        {
                            var r9 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 9);
                            if (r9 != null)
                            {
                                getItem16.Month8 = (Funs.GetNewIntOrZero(getItem16.Month8) - r9.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem17 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 17);
                        if (getItem17 != null)
                        {
                            var r10 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 10);
                            if (r10 != null)
                            {
                                getItem17.Month8 = (Funs.GetNewIntOrZero(getItem17.Month8) - r10.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem18 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 18);
                        if (getItem18 != null)
                        {
                            var r11 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 11);
                            if (r11 != null)
                            {
                                getItem18.Month8 = (Funs.GetNewIntOrZero(getItem18.Month8) - r11.MonthTimes ?? 0).ToString();
                            }
                        }
                    }
                    ////大型、特种设备
                    var getSeDin_MonthReport5 = from x in db.SeDin_MonthReport5
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    var getItem19 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 19);
                    if (getSeDin_MonthReport5.Count() > 0 && getItem19 != null)
                    {
                        int countSumR5 = getSeDin_MonthReport5.Sum(x => x.T01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.T04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T05 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T06 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.S01 ?? 0);
                        getItem19.Month8 = (Funs.GetNewIntOrZero(getItem19.Month8) - countSumR5).ToString();
                    }
                    ////安全生产费用
                    var getSeDin_MonthReport6 = db.SeDin_MonthReport6.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport6 != null)
                    {
                        var getItem20 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 20);
                        if (getItem20 != null)
                        {
                            getItem20.Month8 = (Funs.GetNewDecimalOrZero(getItem20.Month8) - getSeDin_MonthReport6.SumMonth ?? 0).ToString();
                        }
                    }
                    ////项目现场员工入场安全培训人数
                    var getSeDin_MonthReport7 = db.SeDin_MonthReport7.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport7 != null)
                    {
                        var getItem21 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 21);
                        if (getItem21 != null)
                        {
                            getItem21.Month8 = (Funs.GetNewIntOrZero(getItem21.Month8) - getSeDin_MonthReport7.EmployeeMontPerson ?? 0).ToString();
                        }
                    }
                    ////项目会次
                    var getSeDin_MonthReport8 = db.SeDin_MonthReport8.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport8 != null)
                    {
                        var getItem22 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 22);
                        if (getItem22 != null)
                        {
                            getItem22.Month8 = (Funs.GetNewIntOrZero(getItem22.Month8) - getSeDin_MonthReport8.MonthMontNum ?? 0).ToString();
                        }
                        var getItem23 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 23);
                        if (getItem23 != null)
                        {
                            getItem23.Month8 = (Funs.GetNewIntOrZero(getItem23.Month8) - getSeDin_MonthReport8.WeekMontNum ?? 0).ToString();
                        }
                        var getItem24 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 24);
                        if (getItem24 != null)
                        {
                            getItem24.Month8 = (Funs.GetNewIntOrZero(getItem24.Month8) - getSeDin_MonthReport8.SpecialMontPerson ?? 0).ToString();
                        }
                    }
                    /// 安全检查
                    var getSeDin_MonthReport9 = db.SeDin_MonthReport9.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport9 != null)
                    {
                        var getItem25 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 25);
                        if (getItem25 != null)
                        {
                            getItem25.Month8 = (Funs.GetNewIntOrZero(getItem25.Month8) - getSeDin_MonthReport9.DailyMonth ?? 0).ToString();
                        }
                        var getItem26 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 26);
                        if (getItem26 != null)
                        {
                            getItem26.Month8 = (Funs.GetNewIntOrZero(getItem26.Month8) - getSeDin_MonthReport9.WeekMonth ?? 0).ToString();
                        }
                        var getItem27 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 27);
                        if (getItem27 != null)
                        {
                            getItem27.Month8 = (Funs.GetNewIntOrZero(getItem27.Month8) - getSeDin_MonthReport9.SpecialMonth ?? 0).ToString();
                        }
                        var getItem28 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 28);
                        if (getItem28 != null)
                        {
                            getItem28.Month8 = (Funs.GetNewIntOrZero(getItem28.Month8) - getSeDin_MonthReport9.MonthlyMonth ?? 0).ToString();
                        }
                    }
                    /////隐患整改单
                    var getSeDin_MonthReport9Rectification = from x in db.SeDin_MonthReport9Item_Rectification where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Rectification.Count() > 0)
                    {
                        var getItem29 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 29);
                        if (getItem29 != null)
                        {
                            getItem29.Month8 = (Funs.GetNewIntOrZero(getItem29.Month8) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                        var getItem30 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 30);
                        if (getItem30 != null)
                        {
                            getItem30.Month8 = (Funs.GetNewIntOrZero(getItem30.Month8) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMoth ?? 0)).ToString();
                        }
                        var getItem31 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 31);
                        if (getItem31 != null)
                        {
                            getItem31.Month8 = (Funs.GetNewIntOrZero(getItem31.Month8) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthLarge ?? 0)).ToString();
                        }
                        var getItem32 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 32);
                        if (getItem32 != null)
                        {
                            getItem32.Month8 = (Funs.GetNewIntOrZero(getItem32.Month8) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothLarge ?? 0)).ToString();
                        }
                        var getItem33 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 33);
                        if (getItem33 != null)
                        {
                            getItem33.Month8 = (Funs.GetNewIntOrZero(getItem33.Month8) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthSerious ?? 0)).ToString();
                        }
                        var getItem34 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 34);
                        if (getItem34 != null)
                        {
                            getItem34.Month8 = (Funs.GetNewIntOrZero(getItem34.Month8) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothSerious ?? 0)).ToString();
                        }
                    }
                    /////停工令
                    var getSeDin_MonthReport9Stoppage = from x in db.SeDin_MonthReport9Item_Stoppage where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Stoppage.Count() > 0)
                    {
                        var getItem35 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 35);
                        if (getItem35 != null)
                        {
                            getItem35.Month8 = (Funs.GetNewIntOrZero(getItem35.Month8) - getSeDin_MonthReport9Stoppage.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                    }
                    ////奖惩
                    var getSeDin_MonthReport10 = db.SeDin_MonthReport10.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport10 != null)
                    {
                        var getItem36 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 36);
                        if (getItem36 != null)
                        {
                            getItem36.Month8 = (Funs.GetNewDecimalOrZero(getItem36.Month8) - getSeDin_MonthReport10.SafeMonthMoney ?? 0 - getSeDin_MonthReport10.HseMonthMoney ?? 0 - getSeDin_MonthReport10.ProduceMonthMoney ?? 0).ToString();
                        }
                        var getItem37 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 37);
                        if (getItem37 != null)
                        {
                            getItem37.Month8 = (Funs.GetNewDecimalOrZero(getItem37.Month8) - getSeDin_MonthReport10.AccidentMonthMoney ?? 0 - getSeDin_MonthReport10.ViolationMonthMoney ?? 0 - getSeDin_MonthReport10.ManageMonthMoney ?? 0).ToString();
                        }
                    }
                    ////危大工程
                    var getSeDin_MonthReport11 = db.SeDin_MonthReport11.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport11 != null)
                    {
                        var getItem38 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 38);
                        if (getItem38 != null)
                        {
                            getItem38.Month8 = (Funs.GetNewIntOrZero(getItem38.Month8) - getSeDin_MonthReport11.RiskWorkNum ?? 0).ToString();
                        }
                        var getItem39 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 39);
                        if (getItem39 != null)
                        {
                            getItem39.Month8 = (Funs.GetNewIntOrZero(getItem39.Month8) - getSeDin_MonthReport11.LargeWorkNum ?? 0).ToString();
                        }
                    }
                    ////应急
                    var getSeDin_MonthReport12 = db.SeDin_MonthReport12.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport12 != null)
                    {
                        var getItem40 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 40);
                        if (getItem40 != null)
                        {
                            getItem40.Month8 = (Funs.GetNewIntOrZero(getItem40.Month8) - getSeDin_MonthReport12.MultipleSiteNum ?? 0 - getSeDin_MonthReport12.MultipleDesktopNum ?? 0
                               - getSeDin_MonthReport12.SingleSiteNum ?? 0 - getSeDin_MonthReport12.SingleDesktopNum ?? 0).ToString();
                        }
                    }
                    db.SubmitChanges();
                }
                #endregion
            }
            else if (monthReport.ReporMonth.Value.Month == 9)
            {
                #region 数据调整
                bool isSave = false;
                var getSubmissionMonth = getHSEDataCollectISubmissionYear.FirstOrDefault(x => x.ProjectId == monthReport.ProjectId);
                if (getSubmissionMonth != null && Funs.GetNewDateTime(getSubmissionMonth.Month9).HasValue)
                {
                    isSave = true;
                }
                if (isSave)
                {
                    ////员工
                    var getSeDin_MonthReport4Other = db.SeDin_MonthReport4Other.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport4Other != null)
                    {
                        var getItem1 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 1);
                        if (getItem1 != null)
                        {
                            getItem1.Month9 = (Funs.GetNewIntOrZero(getItem1.Month9) - getSeDin_MonthReport4Other.TotalNum ?? 0).ToString();
                        }
                        var getItem2 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 2);
                        if (getItem2 != null)
                        {
                            getItem2.Month9 = (Funs.GetNewIntOrZero(getItem2.Month9) - getSeDin_MonthReport4Other.OutsideNum ?? 0).ToString();
                        }
                        var getItem3 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 3);
                        if (getItem3 != null)
                        {
                            getItem3.Month9 = (Funs.GetNewIntOrZero(getItem3.Month9) - getSeDin_MonthReport4Other.ForeignNum ?? 0).ToString();
                        }
                        var getItem4 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 4);
                        if (getItem4 != null)
                        {
                            getItem4.Month9 = (Funs.GetNewIntOrZero(getItem4.Month9) - getSeDin_MonthReport4Other.ManagerNum ?? 0).ToString();
                        }
                    }
                    ////分包人数
                    var getSeDin_MonthReport4 = from x in db.SeDin_MonthReport4
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport4.Count() > 0)
                    {
                        var getItem5 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 5);
                        if (getItem5 != null)
                        {
                            getItem5.Month9 = (Funs.GetNewIntOrZero(getItem5.Month9) - getSeDin_MonthReport4.Sum(x => x.TotalNum ?? 0)).ToString();
                        }
                        var getItem6 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 6);
                        if (getItem6 != null)
                        {
                            getItem6.Month9 = (Funs.GetNewIntOrZero(getItem6.Month9) - getSeDin_MonthReport4.Sum(x => x.SafeManangerNum ?? 0)).ToString();
                        }
                    }
                    ///安全人工时
                    var getSeDin_MonthReport2 = db.SeDin_MonthReport2.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport2 != null)
                    {
                        var getItem7 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 7);
                        if (getItem7 != null)
                        {
                            getItem7.Month9 = (Funs.GetNewDecimalOrZero(getItem7.Month9) - getSeDin_MonthReport2.MonthWorkTime ?? 0).ToString();
                        }
                    }
                    ////事故
                    var getSeDin_MonthReport3 = from x in db.SeDin_MonthReport3
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport3.Count() > 0)
                    {
                        var getItem8 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 8);
                        if (getItem8 != null)
                        {
                            var r1 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 1);
                            if (r1 != null)
                            {
                                getItem8.Month9 = (Funs.GetNewIntOrZero(getItem8.Month9) - r1.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem9 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 9);
                        if (getItem9 != null)
                        {
                            var r2 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 2);
                            if (r2 != null)
                            {
                                getItem9.Month9 = (Funs.GetNewIntOrZero(getItem9.Month9) - r2.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem10 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 10);
                        if (getItem10 != null)
                        {
                            var r3 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 3);
                            if (r3 != null)
                            {
                                getItem10.Month9 = (Funs.GetNewIntOrZero(getItem10.Month9) - r3.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem11 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 11);
                        if (getItem11 != null)
                        {
                            var r4 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 4);
                            if (r4 != null)
                            {
                                getItem11.Month9 = (Funs.GetNewIntOrZero(getItem11.Month9) - r4.MonthPersons ?? 0).ToString();
                            }
                        }

                        var getItem12 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 12);
                        if (getItem12 != null)
                        {
                            var r5 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 5);
                            if (r5 != null)
                            {
                                getItem12.Month9 = (Funs.GetNewIntOrZero(getItem12.Month9) - r5.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem13 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 13);
                        if (getItem13 != null)
                        {
                            var r6 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 6);
                            if (r6 != null)
                            {
                                getItem13.Month9 = (Funs.GetNewIntOrZero(getItem13.Month9) - r6.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem14 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 14);
                        if (getItem14 != null)
                        {
                            var r7 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 7);
                            if (r7 != null)
                            {
                                getItem14.Month9 = (Funs.GetNewIntOrZero(getItem14.Month9) - r7.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem15 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 15);
                        if (getItem15 != null)
                        {
                            var r8 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 8);
                            if (r8 != null)
                            {
                                getItem15.Month9 = (Funs.GetNewIntOrZero(getItem15.Month9) - r8.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem16 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 16);
                        if (getItem16 != null)
                        {
                            var r9 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 9);
                            if (r9 != null)
                            {
                                getItem16.Month9 = (Funs.GetNewIntOrZero(getItem16.Month9) - r9.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem17 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 17);
                        if (getItem17 != null)
                        {
                            var r10 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 10);
                            if (r10 != null)
                            {
                                getItem17.Month9 = (Funs.GetNewIntOrZero(getItem17.Month9) - r10.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem18 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 18);
                        if (getItem18 != null)
                        {
                            var r11 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 11);
                            if (r11 != null)
                            {
                                getItem18.Month9 = (Funs.GetNewIntOrZero(getItem18.Month9) - r11.MonthTimes ?? 0).ToString();
                            }
                        }
                    }
                    ////大型、特种设备
                    var getSeDin_MonthReport5 = from x in db.SeDin_MonthReport5
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    var getItem19 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 19);
                    if (getSeDin_MonthReport5.Count() > 0 && getItem19 != null)
                    {
                        int countSumR5 = getSeDin_MonthReport5.Sum(x => x.T01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.T04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T05 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T06 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.S01 ?? 0);
                        getItem19.Month9 = (Funs.GetNewIntOrZero(getItem19.Month9) - countSumR5).ToString();
                    }
                    ////安全生产费用
                    var getSeDin_MonthReport6 = db.SeDin_MonthReport6.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport6 != null)
                    {
                        var getItem20 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 20);
                        if (getItem20 != null)
                        {
                            getItem20.Month9 = (Funs.GetNewDecimalOrZero(getItem20.Month9) - getSeDin_MonthReport6.SumMonth ?? 0).ToString();
                        }
                    }
                    ////项目现场员工入场安全培训人数
                    var getSeDin_MonthReport7 = db.SeDin_MonthReport7.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport7 != null)
                    {
                        var getItem21 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 21);
                        if (getItem21 != null)
                        {
                            getItem21.Month9 = (Funs.GetNewIntOrZero(getItem21.Month9) - getSeDin_MonthReport7.EmployeeMontPerson ?? 0).ToString();
                        }
                    }
                    ////项目会次
                    var getSeDin_MonthReport8 = db.SeDin_MonthReport8.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport8 != null)
                    {
                        var getItem22 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 22);
                        if (getItem22 != null)
                        {
                            getItem22.Month9 = (Funs.GetNewIntOrZero(getItem22.Month9) - getSeDin_MonthReport8.MonthMontNum ?? 0).ToString();
                        }
                        var getItem23 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 23);
                        if (getItem23 != null)
                        {
                            getItem23.Month9 = (Funs.GetNewIntOrZero(getItem23.Month9) - getSeDin_MonthReport8.WeekMontNum ?? 0).ToString();
                        }
                        var getItem24 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 24);
                        if (getItem24 != null)
                        {
                            getItem24.Month9 = (Funs.GetNewIntOrZero(getItem24.Month9) - getSeDin_MonthReport8.SpecialMontPerson ?? 0).ToString();
                        }
                    }
                    /// 安全检查
                    var getSeDin_MonthReport9 = db.SeDin_MonthReport9.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport9 != null)
                    {
                        var getItem25 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 25);
                        if (getItem25 != null)
                        {
                            getItem25.Month9 = (Funs.GetNewIntOrZero(getItem25.Month9) - getSeDin_MonthReport9.DailyMonth ?? 0).ToString();
                        }
                        var getItem26 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 26);
                        if (getItem26 != null)
                        {
                            getItem26.Month9 = (Funs.GetNewIntOrZero(getItem26.Month9) - getSeDin_MonthReport9.WeekMonth ?? 0).ToString();
                        }
                        var getItem27 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 27);
                        if (getItem27 != null)
                        {
                            getItem27.Month9 = (Funs.GetNewIntOrZero(getItem27.Month9) - getSeDin_MonthReport9.SpecialMonth ?? 0).ToString();
                        }
                        var getItem28 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 28);
                        if (getItem28 != null)
                        {
                            getItem28.Month9 = (Funs.GetNewIntOrZero(getItem28.Month9) - getSeDin_MonthReport9.MonthlyMonth ?? 0).ToString();
                        }
                    }
                    /////隐患整改单
                    var getSeDin_MonthReport9Rectification = from x in db.SeDin_MonthReport9Item_Rectification where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Rectification.Count() > 0)
                    {
                        var getItem29 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 29);
                        if (getItem29 != null)
                        {
                            getItem29.Month9 = (Funs.GetNewIntOrZero(getItem29.Month9) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                        var getItem30 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 30);
                        if (getItem30 != null)
                        {
                            getItem30.Month9 = (Funs.GetNewIntOrZero(getItem30.Month9) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMoth ?? 0)).ToString();
                        }
                        var getItem31 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 31);
                        if (getItem31 != null)
                        {
                            getItem31.Month9 = (Funs.GetNewIntOrZero(getItem31.Month9) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthLarge ?? 0)).ToString();
                        }
                        var getItem32 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 32);
                        if (getItem32 != null)
                        {
                            getItem32.Month9 = (Funs.GetNewIntOrZero(getItem32.Month9) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothLarge ?? 0)).ToString();
                        }
                        var getItem33 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 33);
                        if (getItem33 != null)
                        {
                            getItem33.Month9 = (Funs.GetNewIntOrZero(getItem33.Month9) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthSerious ?? 0)).ToString();
                        }
                        var getItem34 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 34);
                        if (getItem34 != null)
                        {
                            getItem34.Month9 = (Funs.GetNewIntOrZero(getItem34.Month9) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothSerious ?? 0)).ToString();
                        }
                    }
                    /////停工令
                    var getSeDin_MonthReport9Stoppage = from x in db.SeDin_MonthReport9Item_Stoppage where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Stoppage.Count() > 0)
                    {
                        var getItem35 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 35);
                        if (getItem35 != null)
                        {
                            getItem35.Month9 = (Funs.GetNewIntOrZero(getItem35.Month9) - getSeDin_MonthReport9Stoppage.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                    }
                    ////奖惩
                    var getSeDin_MonthReport10 = db.SeDin_MonthReport10.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport10 != null)
                    {
                        var getItem36 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 36);
                        if (getItem36 != null)
                        {
                            getItem36.Month9 = (Funs.GetNewDecimalOrZero(getItem36.Month9) - getSeDin_MonthReport10.SafeMonthMoney ?? 0 - getSeDin_MonthReport10.HseMonthMoney ?? 0 - getSeDin_MonthReport10.ProduceMonthMoney ?? 0).ToString();
                        }
                        var getItem37 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 37);
                        if (getItem37 != null)
                        {
                            getItem37.Month9 = (Funs.GetNewDecimalOrZero(getItem37.Month9) - getSeDin_MonthReport10.AccidentMonthMoney ?? 0 - getSeDin_MonthReport10.ViolationMonthMoney ?? 0 - getSeDin_MonthReport10.ManageMonthMoney ?? 0).ToString();
                        }
                    }
                    ////危大工程
                    var getSeDin_MonthReport11 = db.SeDin_MonthReport11.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport11 != null)
                    {
                        var getItem38 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 38);
                        if (getItem38 != null)
                        {
                            getItem38.Month9 = (Funs.GetNewIntOrZero(getItem38.Month9) - getSeDin_MonthReport11.RiskWorkNum ?? 0).ToString();
                        }
                        var getItem39 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 39);
                        if (getItem39 != null)
                        {
                            getItem39.Month9 = (Funs.GetNewIntOrZero(getItem39.Month9) - getSeDin_MonthReport11.LargeWorkNum ?? 0).ToString();
                        }
                    }
                    ////应急
                    var getSeDin_MonthReport12 = db.SeDin_MonthReport12.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport12 != null)
                    {
                        var getItem40 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 40);
                        if (getItem40 != null)
                        {
                            getItem40.Month9 = (Funs.GetNewIntOrZero(getItem40.Month9) - getSeDin_MonthReport12.MultipleSiteNum ?? 0 - getSeDin_MonthReport12.MultipleDesktopNum ?? 0
                               - getSeDin_MonthReport12.SingleSiteNum ?? 0 - getSeDin_MonthReport12.SingleDesktopNum ?? 0).ToString();
                        }
                    }
                    db.SubmitChanges();
                }
                #endregion
            }
            else if (monthReport.ReporMonth.Value.Month == 10)
            {
                #region 数据调整
                bool isSave = false;
                var getSubmissionMonth = getHSEDataCollectISubmissionYear.FirstOrDefault(x => x.ProjectId == monthReport.ProjectId);
                if (getSubmissionMonth != null && Funs.GetNewDateTime(getSubmissionMonth.Month10).HasValue)
                {
                    isSave = true;
                }
                if (isSave)
                {
                    ////员工
                    var getSeDin_MonthReport4Other = db.SeDin_MonthReport4Other.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport4Other != null)
                    {
                        var getItem1 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 1);
                        if (getItem1 != null)
                        {
                            getItem1.Month10 = (Funs.GetNewIntOrZero(getItem1.Month10) - getSeDin_MonthReport4Other.TotalNum ?? 0).ToString();
                        }
                        var getItem2 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 2);
                        if (getItem2 != null)
                        {
                            getItem2.Month10 = (Funs.GetNewIntOrZero(getItem2.Month10) - getSeDin_MonthReport4Other.OutsideNum ?? 0).ToString();
                        }
                        var getItem3 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 3);
                        if (getItem3 != null)
                        {
                            getItem3.Month10 = (Funs.GetNewIntOrZero(getItem3.Month10) - getSeDin_MonthReport4Other.ForeignNum ?? 0).ToString();
                        }
                        var getItem4 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 4);
                        if (getItem4 != null)
                        {
                            getItem4.Month10 = (Funs.GetNewIntOrZero(getItem4.Month10) - getSeDin_MonthReport4Other.ManagerNum ?? 0).ToString();
                        }
                    }
                    ////分包人数
                    var getSeDin_MonthReport4 = from x in db.SeDin_MonthReport4
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport4.Count() > 0)
                    {
                        var getItem5 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 5);
                        if (getItem5 != null)
                        {
                            getItem5.Month10 = (Funs.GetNewIntOrZero(getItem5.Month10) - getSeDin_MonthReport4.Sum(x => x.TotalNum ?? 0)).ToString();
                        }
                        var getItem6 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 6);
                        if (getItem6 != null)
                        {
                            getItem6.Month10 = (Funs.GetNewIntOrZero(getItem6.Month10) - getSeDin_MonthReport4.Sum(x => x.SafeManangerNum ?? 0)).ToString();
                        }
                    }
                    ///安全人工时
                    var getSeDin_MonthReport2 = db.SeDin_MonthReport2.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport2 != null)
                    {
                        var getItem7 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 7);
                        if (getItem7 != null)
                        {
                            getItem7.Month10 = (Funs.GetNewDecimalOrZero(getItem7.Month10) - getSeDin_MonthReport2.MonthWorkTime ?? 0).ToString();
                        }
                    }
                    ////事故
                    var getSeDin_MonthReport3 = from x in db.SeDin_MonthReport3
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport3.Count() > 0)
                    {
                        var getItem8 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 8);
                        if (getItem8 != null)
                        {
                            var r1 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 1);
                            if (r1 != null)
                            {
                                getItem8.Month10 = (Funs.GetNewIntOrZero(getItem8.Month10) - r1.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem9 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 9);
                        if (getItem9 != null)
                        {
                            var r2 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 2);
                            if (r2 != null)
                            {
                                getItem9.Month10 = (Funs.GetNewIntOrZero(getItem9.Month10) - r2.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem10 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 10);
                        if (getItem10 != null)
                        {
                            var r3 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 3);
                            if (r3 != null)
                            {
                                getItem10.Month10 = (Funs.GetNewIntOrZero(getItem10.Month10) - r3.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem11 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 11);
                        if (getItem11 != null)
                        {
                            var r4 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 4);
                            if (r4 != null)
                            {
                                getItem11.Month10 = (Funs.GetNewIntOrZero(getItem11.Month10) - r4.MonthPersons ?? 0).ToString();
                            }
                        }

                        var getItem12 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 12);
                        if (getItem12 != null)
                        {
                            var r5 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 5);
                            if (r5 != null)
                            {
                                getItem12.Month10 = (Funs.GetNewIntOrZero(getItem12.Month10) - r5.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem13 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 13);
                        if (getItem13 != null)
                        {
                            var r6 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 6);
                            if (r6 != null)
                            {
                                getItem13.Month10 = (Funs.GetNewIntOrZero(getItem13.Month10) - r6.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem14 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 14);
                        if (getItem14 != null)
                        {
                            var r7 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 7);
                            if (r7 != null)
                            {
                                getItem14.Month10 = (Funs.GetNewIntOrZero(getItem14.Month10) - r7.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem15 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 15);
                        if (getItem15 != null)
                        {
                            var r8 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 8);
                            if (r8 != null)
                            {
                                getItem15.Month10 = (Funs.GetNewIntOrZero(getItem15.Month10) - r8.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem16 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 16);
                        if (getItem16 != null)
                        {
                            var r9 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 9);
                            if (r9 != null)
                            {
                                getItem16.Month10 = (Funs.GetNewIntOrZero(getItem16.Month10) - r9.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem17 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 17);
                        if (getItem17 != null)
                        {
                            var r10 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 10);
                            if (r10 != null)
                            {
                                getItem17.Month10 = (Funs.GetNewIntOrZero(getItem17.Month10) - r10.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem18 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 18);
                        if (getItem18 != null)
                        {
                            var r11 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 11);
                            if (r11 != null)
                            {
                                getItem18.Month10 = (Funs.GetNewIntOrZero(getItem18.Month10) - r11.MonthTimes ?? 0).ToString();
                            }
                        }
                    }
                    ////大型、特种设备
                    var getSeDin_MonthReport5 = from x in db.SeDin_MonthReport5
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    var getItem19 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 19);
                    if (getSeDin_MonthReport5.Count() > 0 && getItem19 != null)
                    {
                        int countSumR5 = getSeDin_MonthReport5.Sum(x => x.T01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.T04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T05 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T06 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.S01 ?? 0);
                        getItem19.Month10 = (Funs.GetNewIntOrZero(getItem19.Month10) - countSumR5).ToString();
                    }
                    ////安全生产费用
                    var getSeDin_MonthReport6 = db.SeDin_MonthReport6.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport6 != null)
                    {
                        var getItem20 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 20);
                        if (getItem20 != null)
                        {
                            getItem20.Month10 = (Funs.GetNewDecimalOrZero(getItem20.Month10) - getSeDin_MonthReport6.SumMonth ?? 0).ToString();
                        }
                    }
                    ////项目现场员工入场安全培训人数
                    var getSeDin_MonthReport7 = db.SeDin_MonthReport7.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport7 != null)
                    {
                        var getItem21 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 21);
                        if (getItem21 != null)
                        {
                            getItem21.Month10 = (Funs.GetNewIntOrZero(getItem21.Month10) - getSeDin_MonthReport7.EmployeeMontPerson ?? 0).ToString();
                        }
                    }
                    ////项目会次
                    var getSeDin_MonthReport8 = db.SeDin_MonthReport8.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport8 != null)
                    {
                        var getItem22 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 22);
                        if (getItem22 != null)
                        {
                            getItem22.Month10 = (Funs.GetNewIntOrZero(getItem22.Month10) - getSeDin_MonthReport8.MonthMontNum ?? 0).ToString();
                        }
                        var getItem23 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 23);
                        if (getItem23 != null)
                        {
                            getItem23.Month10 = (Funs.GetNewIntOrZero(getItem23.Month10) - getSeDin_MonthReport8.WeekMontNum ?? 0).ToString();
                        }
                        var getItem24 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 24);
                        if (getItem24 != null)
                        {
                            getItem24.Month10 = (Funs.GetNewIntOrZero(getItem24.Month10) - getSeDin_MonthReport8.SpecialMontPerson ?? 0).ToString();
                        }
                    }
                    /// 安全检查
                    var getSeDin_MonthReport9 = db.SeDin_MonthReport9.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport9 != null)
                    {
                        var getItem25 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 25);
                        if (getItem25 != null)
                        {
                            getItem25.Month10 = (Funs.GetNewIntOrZero(getItem25.Month10) - getSeDin_MonthReport9.DailyMonth ?? 0).ToString();
                        }
                        var getItem26 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 26);
                        if (getItem26 != null)
                        {
                            getItem26.Month10 = (Funs.GetNewIntOrZero(getItem26.Month10) - getSeDin_MonthReport9.WeekMonth ?? 0).ToString();
                        }
                        var getItem27 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 27);
                        if (getItem27 != null)
                        {
                            getItem27.Month10 = (Funs.GetNewIntOrZero(getItem27.Month10) - getSeDin_MonthReport9.SpecialMonth ?? 0).ToString();
                        }
                        var getItem28 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 28);
                        if (getItem28 != null)
                        {
                            getItem28.Month10 = (Funs.GetNewIntOrZero(getItem28.Month10) - getSeDin_MonthReport9.MonthlyMonth ?? 0).ToString();
                        }
                    }
                    /////隐患整改单
                    var getSeDin_MonthReport9Rectification = from x in db.SeDin_MonthReport9Item_Rectification where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Rectification.Count() > 0)
                    {
                        var getItem29 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 29);
                        if (getItem29 != null)
                        {
                            getItem29.Month10 = (Funs.GetNewIntOrZero(getItem29.Month10) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                        var getItem30 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 30);
                        if (getItem30 != null)
                        {
                            getItem30.Month10 = (Funs.GetNewIntOrZero(getItem30.Month10) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMoth ?? 0)).ToString();
                        }
                        var getItem31 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 31);
                        if (getItem31 != null)
                        {
                            getItem31.Month10 = (Funs.GetNewIntOrZero(getItem31.Month10) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthLarge ?? 0)).ToString();
                        }
                        var getItem32 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 32);
                        if (getItem32 != null)
                        {
                            getItem32.Month10 = (Funs.GetNewIntOrZero(getItem32.Month10) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothLarge ?? 0)).ToString();
                        }
                        var getItem33 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 33);
                        if (getItem33 != null)
                        {
                            getItem33.Month10 = (Funs.GetNewIntOrZero(getItem33.Month10) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthSerious ?? 0)).ToString();
                        }
                        var getItem34 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 34);
                        if (getItem34 != null)
                        {
                            getItem34.Month10 = (Funs.GetNewIntOrZero(getItem34.Month10) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothSerious ?? 0)).ToString();
                        }
                    }
                    /////停工令
                    var getSeDin_MonthReport9Stoppage = from x in db.SeDin_MonthReport9Item_Stoppage where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Stoppage.Count() > 0)
                    {
                        var getItem35 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 35);
                        if (getItem35 != null)
                        {
                            getItem35.Month10 = (Funs.GetNewIntOrZero(getItem35.Month10) - getSeDin_MonthReport9Stoppage.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                    }
                    ////奖惩
                    var getSeDin_MonthReport10 = db.SeDin_MonthReport10.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport10 != null)
                    {
                        var getItem36 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 36);
                        if (getItem36 != null)
                        {
                            getItem36.Month10 = (Funs.GetNewDecimalOrZero(getItem36.Month10) - getSeDin_MonthReport10.SafeMonthMoney ?? 0 - getSeDin_MonthReport10.HseMonthMoney ?? 0 - getSeDin_MonthReport10.ProduceMonthMoney ?? 0).ToString();
                        }
                        var getItem37 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 37);
                        if (getItem37 != null)
                        {
                            getItem37.Month10 = (Funs.GetNewDecimalOrZero(getItem37.Month10) - getSeDin_MonthReport10.AccidentMonthMoney ?? 0 - getSeDin_MonthReport10.ViolationMonthMoney ?? 0 - getSeDin_MonthReport10.ManageMonthMoney ?? 0).ToString();
                        }
                    }
                    ////危大工程
                    var getSeDin_MonthReport11 = db.SeDin_MonthReport11.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport11 != null)
                    {
                        var getItem38 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 38);
                        if (getItem38 != null)
                        {
                            getItem38.Month10 = (Funs.GetNewIntOrZero(getItem38.Month10) - getSeDin_MonthReport11.RiskWorkNum ?? 0).ToString();
                        }
                        var getItem39 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 39);
                        if (getItem39 != null)
                        {
                            getItem39.Month10 = (Funs.GetNewIntOrZero(getItem39.Month10) - getSeDin_MonthReport11.LargeWorkNum ?? 0).ToString();
                        }
                    }
                    ////应急
                    var getSeDin_MonthReport12 = db.SeDin_MonthReport12.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport12 != null)
                    {
                        var getItem40 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 40);
                        if (getItem40 != null)
                        {
                            getItem40.Month10 = (Funs.GetNewIntOrZero(getItem40.Month10) - getSeDin_MonthReport12.MultipleSiteNum ?? 0 - getSeDin_MonthReport12.MultipleDesktopNum ?? 0
                               - getSeDin_MonthReport12.SingleSiteNum ?? 0 - getSeDin_MonthReport12.SingleDesktopNum ?? 0).ToString();
                        }
                    }
                    db.SubmitChanges();
                }
                #endregion
            }
            else if (monthReport.ReporMonth.Value.Month == 11)
            {
                #region 数据调整
                bool isSave = false;
                var getSubmissionMonth = getHSEDataCollectISubmissionYear.FirstOrDefault(x => x.ProjectId == monthReport.ProjectId);
                if (getSubmissionMonth != null && Funs.GetNewDateTime(getSubmissionMonth.Month11).HasValue)
                {
                    isSave = true;
                }
                if (isSave)
                {
                    ////员工
                    var getSeDin_MonthReport4Other = db.SeDin_MonthReport4Other.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport4Other != null)
                    {
                        var getItem1 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 1);
                        if (getItem1 != null)
                        {
                            getItem1.Month11 = (Funs.GetNewIntOrZero(getItem1.Month11) - getSeDin_MonthReport4Other.TotalNum ?? 0).ToString();
                        }
                        var getItem2 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 2);
                        if (getItem2 != null)
                        {
                            getItem2.Month11 = (Funs.GetNewIntOrZero(getItem2.Month11) - getSeDin_MonthReport4Other.OutsideNum ?? 0).ToString();
                        }
                        var getItem3 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 3);
                        if (getItem3 != null)
                        {
                            getItem3.Month11 = (Funs.GetNewIntOrZero(getItem3.Month11) - getSeDin_MonthReport4Other.ForeignNum ?? 0).ToString();
                        }
                        var getItem4 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 4);
                        if (getItem4 != null)
                        {
                            getItem4.Month11 = (Funs.GetNewIntOrZero(getItem4.Month11) - getSeDin_MonthReport4Other.ManagerNum ?? 0).ToString();
                        }
                    }
                    ////分包人数
                    var getSeDin_MonthReport4 = from x in db.SeDin_MonthReport4
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport4.Count() > 0)
                    {
                        var getItem5 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 5);
                        if (getItem5 != null)
                        {
                            getItem5.Month11 = (Funs.GetNewIntOrZero(getItem5.Month11) - getSeDin_MonthReport4.Sum(x => x.TotalNum ?? 0)).ToString();
                        }
                        var getItem6 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 6);
                        if (getItem6 != null)
                        {
                            getItem6.Month11 = (Funs.GetNewIntOrZero(getItem6.Month11) - getSeDin_MonthReport4.Sum(x => x.SafeManangerNum ?? 0)).ToString();
                        }
                    }
                    ///安全人工时
                    var getSeDin_MonthReport2 = db.SeDin_MonthReport2.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport2 != null)
                    {
                        var getItem7 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 7);
                        if (getItem7 != null)
                        {
                            getItem7.Month11 = (Funs.GetNewDecimalOrZero(getItem7.Month11) - getSeDin_MonthReport2.MonthWorkTime ?? 0).ToString();
                        }
                    }
                    ////事故
                    var getSeDin_MonthReport3 = from x in db.SeDin_MonthReport3
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport3.Count() > 0)
                    {
                        var getItem8 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 8);
                        if (getItem8 != null)
                        {
                            var r1 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 1);
                            if (r1 != null)
                            {
                                getItem8.Month11 = (Funs.GetNewIntOrZero(getItem8.Month11) - r1.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem9 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 9);
                        if (getItem9 != null)
                        {
                            var r2 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 2);
                            if (r2 != null)
                            {
                                getItem9.Month11 = (Funs.GetNewIntOrZero(getItem9.Month11) - r2.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem10 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 10);
                        if (getItem10 != null)
                        {
                            var r3 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 3);
                            if (r3 != null)
                            {
                                getItem10.Month11 = (Funs.GetNewIntOrZero(getItem10.Month11) - r3.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem11 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 11);
                        if (getItem11 != null)
                        {
                            var r4 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 4);
                            if (r4 != null)
                            {
                                getItem11.Month11 = (Funs.GetNewIntOrZero(getItem11.Month11) - r4.MonthPersons ?? 0).ToString();
                            }
                        }

                        var getItem12 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 12);
                        if (getItem12 != null)
                        {
                            var r5 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 5);
                            if (r5 != null)
                            {
                                getItem12.Month11 = (Funs.GetNewIntOrZero(getItem12.Month11) - r5.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem13 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 13);
                        if (getItem13 != null)
                        {
                            var r6 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 6);
                            if (r6 != null)
                            {
                                getItem13.Month11 = (Funs.GetNewIntOrZero(getItem13.Month11) - r6.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem14 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 14);
                        if (getItem14 != null)
                        {
                            var r7 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 7);
                            if (r7 != null)
                            {
                                getItem14.Month11 = (Funs.GetNewIntOrZero(getItem14.Month11) - r7.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem15 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 15);
                        if (getItem15 != null)
                        {
                            var r8 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 8);
                            if (r8 != null)
                            {
                                getItem15.Month11 = (Funs.GetNewIntOrZero(getItem15.Month11) - r8.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem16 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 16);
                        if (getItem16 != null)
                        {
                            var r9 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 9);
                            if (r9 != null)
                            {
                                getItem16.Month11 = (Funs.GetNewIntOrZero(getItem16.Month11) - r9.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem17 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 17);
                        if (getItem17 != null)
                        {
                            var r10 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 10);
                            if (r10 != null)
                            {
                                getItem17.Month11 = (Funs.GetNewIntOrZero(getItem17.Month11) - r10.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem18 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 18);
                        if (getItem18 != null)
                        {
                            var r11 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 11);
                            if (r11 != null)
                            {
                                getItem18.Month11 = (Funs.GetNewIntOrZero(getItem18.Month11) - r11.MonthTimes ?? 0).ToString();
                            }
                        }
                    }
                    ////大型、特种设备
                    var getSeDin_MonthReport5 = from x in db.SeDin_MonthReport5
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    var getItem19 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 19);
                    if (getSeDin_MonthReport5.Count() > 0 && getItem19 != null)
                    {
                        int countSumR5 = getSeDin_MonthReport5.Sum(x => x.T01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.T04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T05 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T06 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.S01 ?? 0);
                        getItem19.Month11 = (Funs.GetNewIntOrZero(getItem19.Month11) - countSumR5).ToString();
                    }
                    ////安全生产费用
                    var getSeDin_MonthReport6 = db.SeDin_MonthReport6.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport6 != null)
                    {
                        var getItem20 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 20);
                        if (getItem20 != null)
                        {
                            getItem20.Month11 = (Funs.GetNewDecimalOrZero(getItem20.Month11) - getSeDin_MonthReport6.SumMonth ?? 0).ToString();
                        }
                    }
                    ////项目现场员工入场安全培训人数
                    var getSeDin_MonthReport7 = db.SeDin_MonthReport7.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport7 != null)
                    {
                        var getItem21 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 21);
                        if (getItem21 != null)
                        {
                            getItem21.Month11 = (Funs.GetNewIntOrZero(getItem21.Month11) - getSeDin_MonthReport7.EmployeeMontPerson ?? 0).ToString();
                        }
                    }
                    ////项目会次
                    var getSeDin_MonthReport8 = db.SeDin_MonthReport8.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport8 != null)
                    {
                        var getItem22 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 22);
                        if (getItem22 != null)
                        {
                            getItem22.Month11 = (Funs.GetNewIntOrZero(getItem22.Month11) - getSeDin_MonthReport8.MonthMontNum ?? 0).ToString();
                        }
                        var getItem23 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 23);
                        if (getItem23 != null)
                        {
                            getItem23.Month11 = (Funs.GetNewIntOrZero(getItem23.Month11) - getSeDin_MonthReport8.WeekMontNum ?? 0).ToString();
                        }
                        var getItem24 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 24);
                        if (getItem24 != null)
                        {
                            getItem24.Month11 = (Funs.GetNewIntOrZero(getItem24.Month11) - getSeDin_MonthReport8.SpecialMontPerson ?? 0).ToString();
                        }
                    }
                    /// 安全检查
                    var getSeDin_MonthReport9 = db.SeDin_MonthReport9.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport9 != null)
                    {
                        var getItem25 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 25);
                        if (getItem25 != null)
                        {
                            getItem25.Month11 = (Funs.GetNewIntOrZero(getItem25.Month11) - getSeDin_MonthReport9.DailyMonth ?? 0).ToString();
                        }
                        var getItem26 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 26);
                        if (getItem26 != null)
                        {
                            getItem26.Month11 = (Funs.GetNewIntOrZero(getItem26.Month11) - getSeDin_MonthReport9.WeekMonth ?? 0).ToString();
                        }
                        var getItem27 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 27);
                        if (getItem27 != null)
                        {
                            getItem27.Month11 = (Funs.GetNewIntOrZero(getItem27.Month11) - getSeDin_MonthReport9.SpecialMonth ?? 0).ToString();
                        }
                        var getItem28 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 28);
                        if (getItem28 != null)
                        {
                            getItem28.Month11 = (Funs.GetNewIntOrZero(getItem28.Month11) - getSeDin_MonthReport9.MonthlyMonth ?? 0).ToString();
                        }
                    }
                    /////隐患整改单
                    var getSeDin_MonthReport9Rectification = from x in db.SeDin_MonthReport9Item_Rectification where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Rectification.Count() > 0)
                    {
                        var getItem29 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 29);
                        if (getItem29 != null)
                        {
                            getItem29.Month11 = (Funs.GetNewIntOrZero(getItem29.Month11) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                        var getItem30 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 30);
                        if (getItem30 != null)
                        {
                            getItem30.Month11 = (Funs.GetNewIntOrZero(getItem30.Month11) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMoth ?? 0)).ToString();
                        }
                        var getItem31 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 31);
                        if (getItem31 != null)
                        {
                            getItem31.Month11 = (Funs.GetNewIntOrZero(getItem31.Month11) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthLarge ?? 0)).ToString();
                        }
                        var getItem32 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 32);
                        if (getItem32 != null)
                        {
                            getItem32.Month11 = (Funs.GetNewIntOrZero(getItem32.Month11) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothLarge ?? 0)).ToString();
                        }
                        var getItem33 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 33);
                        if (getItem33 != null)
                        {
                            getItem33.Month11 = (Funs.GetNewIntOrZero(getItem33.Month11) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthSerious ?? 0)).ToString();
                        }
                        var getItem34 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 34);
                        if (getItem34 != null)
                        {
                            getItem34.Month11 = (Funs.GetNewIntOrZero(getItem34.Month11) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothSerious ?? 0)).ToString();
                        }
                    }
                    /////停工令
                    var getSeDin_MonthReport9Stoppage = from x in db.SeDin_MonthReport9Item_Stoppage where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Stoppage.Count() > 0)
                    {
                        var getItem35 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 35);
                        if (getItem35 != null)
                        {
                            getItem35.Month11 = (Funs.GetNewIntOrZero(getItem35.Month11) - getSeDin_MonthReport9Stoppage.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                    }
                    ////奖惩
                    var getSeDin_MonthReport10 = db.SeDin_MonthReport10.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport10 != null)
                    {
                        var getItem36 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 36);
                        if (getItem36 != null)
                        {
                            getItem36.Month11 = (Funs.GetNewDecimalOrZero(getItem36.Month11) - getSeDin_MonthReport10.SafeMonthMoney ?? 0 - getSeDin_MonthReport10.HseMonthMoney ?? 0 - getSeDin_MonthReport10.ProduceMonthMoney ?? 0).ToString();
                        }
                        var getItem37 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 37);
                        if (getItem37 != null)
                        {
                            getItem37.Month11 = (Funs.GetNewDecimalOrZero(getItem37.Month11) - getSeDin_MonthReport10.AccidentMonthMoney ?? 0 - getSeDin_MonthReport10.ViolationMonthMoney ?? 0 - getSeDin_MonthReport10.ManageMonthMoney ?? 0).ToString();
                        }
                    }
                    ////危大工程
                    var getSeDin_MonthReport11 = db.SeDin_MonthReport11.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport11 != null)
                    {
                        var getItem38 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 38);
                        if (getItem38 != null)
                        {
                            getItem38.Month11 = (Funs.GetNewIntOrZero(getItem38.Month11) - getSeDin_MonthReport11.RiskWorkNum ?? 0).ToString();
                        }
                        var getItem39 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 39);
                        if (getItem39 != null)
                        {
                            getItem39.Month11 = (Funs.GetNewIntOrZero(getItem39.Month11) - getSeDin_MonthReport11.LargeWorkNum ?? 0).ToString();
                        }
                    }
                    ////应急
                    var getSeDin_MonthReport12 = db.SeDin_MonthReport12.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport12 != null)
                    {
                        var getItem40 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 40);
                        if (getItem40 != null)
                        {
                            getItem40.Month11 = (Funs.GetNewIntOrZero(getItem40.Month11) - getSeDin_MonthReport12.MultipleSiteNum ?? 0 - getSeDin_MonthReport12.MultipleDesktopNum ?? 0
                               - getSeDin_MonthReport12.SingleSiteNum ?? 0 - getSeDin_MonthReport12.SingleDesktopNum ?? 0).ToString();
                        }
                    }
                    db.SubmitChanges();
                }
                #endregion
            }
            else if (monthReport.ReporMonth.Value.Month == 12)
            {
                #region 数据调整
                bool isSave = false;
                var getSubmissionMonth = getHSEDataCollectISubmissionYear.FirstOrDefault(x => x.ProjectId == monthReport.ProjectId);
                if (getSubmissionMonth != null && Funs.GetNewDateTime(getSubmissionMonth.Month12).HasValue)
                {
                    isSave = true;
                }
                if (isSave)
                {
                    ////员工
                    var getSeDin_MonthReport4Other = db.SeDin_MonthReport4Other.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport4Other != null)
                    {
                        var getItem1 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 1);
                        if (getItem1 != null)
                        {
                            getItem1.Month12 = (Funs.GetNewIntOrZero(getItem1.Month12) - getSeDin_MonthReport4Other.TotalNum ?? 0).ToString();
                        }
                        var getItem2 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 2);
                        if (getItem2 != null)
                        {
                            getItem2.Month12 = (Funs.GetNewIntOrZero(getItem2.Month12) - getSeDin_MonthReport4Other.OutsideNum ?? 0).ToString();
                        }
                        var getItem3 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 3);
                        if (getItem3 != null)
                        {
                            getItem3.Month12 = (Funs.GetNewIntOrZero(getItem3.Month12) - getSeDin_MonthReport4Other.ForeignNum ?? 0).ToString();
                        }
                        var getItem4 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 4);
                        if (getItem4 != null)
                        {
                            getItem4.Month12 = (Funs.GetNewIntOrZero(getItem4.Month12) - getSeDin_MonthReport4Other.ManagerNum ?? 0).ToString();
                        }
                    }
                    ////分包人数
                    var getSeDin_MonthReport4 = from x in db.SeDin_MonthReport4
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport4.Count() > 0)
                    {
                        var getItem5 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 5);
                        if (getItem5 != null)
                        {
                            getItem5.Month12 = (Funs.GetNewIntOrZero(getItem5.Month12) - getSeDin_MonthReport4.Sum(x => x.TotalNum ?? 0)).ToString();
                        }
                        var getItem6 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 6);
                        if (getItem6 != null)
                        {
                            getItem6.Month12 = (Funs.GetNewIntOrZero(getItem6.Month12) - getSeDin_MonthReport4.Sum(x => x.SafeManangerNum ?? 0)).ToString();
                        }
                    }
                    ///安全人工时
                    var getSeDin_MonthReport2 = db.SeDin_MonthReport2.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport2 != null)
                    {
                        var getItem7 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 7);
                        if (getItem7 != null)
                        {
                            getItem7.Month12 = (Funs.GetNewDecimalOrZero(getItem7.Month12) - getSeDin_MonthReport2.MonthWorkTime ?? 0).ToString();
                        }
                    }
                    ////事故
                    var getSeDin_MonthReport3 = from x in db.SeDin_MonthReport3
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    if (getSeDin_MonthReport3.Count() > 0)
                    {
                        var getItem8 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 8);
                        if (getItem8 != null)
                        {
                            var r1 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 1);
                            if (r1 != null)
                            {
                                getItem8.Month12 = (Funs.GetNewIntOrZero(getItem8.Month12) - r1.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem9 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 9);
                        if (getItem9 != null)
                        {
                            var r2 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 2);
                            if (r2 != null)
                            {
                                getItem9.Month12 = (Funs.GetNewIntOrZero(getItem9.Month12) - r2.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem10 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 10);
                        if (getItem10 != null)
                        {
                            var r3 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 3);
                            if (r3 != null)
                            {
                                getItem10.Month12 = (Funs.GetNewIntOrZero(getItem10.Month12) - r3.MonthPersons ?? 0).ToString();
                            }
                        }
                        var getItem11 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 11);
                        if (getItem11 != null)
                        {
                            var r4 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 4);
                            if (r4 != null)
                            {
                                getItem11.Month12 = (Funs.GetNewIntOrZero(getItem11.Month12) - r4.MonthPersons ?? 0).ToString();
                            }
                        }

                        var getItem12 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 12);
                        if (getItem12 != null)
                        {
                            var r5 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 5);
                            if (r5 != null)
                            {
                                getItem12.Month12 = (Funs.GetNewIntOrZero(getItem12.Month12) - r5.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem13 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 13);
                        if (getItem13 != null)
                        {
                            var r6 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 6);
                            if (r6 != null)
                            {
                                getItem13.Month12 = (Funs.GetNewIntOrZero(getItem13.Month12) - r6.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem14 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 14);
                        if (getItem14 != null)
                        {
                            var r7 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 7);
                            if (r7 != null)
                            {
                                getItem14.Month12 = (Funs.GetNewIntOrZero(getItem14.Month12) - r7.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem15 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 15);
                        if (getItem15 != null)
                        {
                            var r8 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 8);
                            if (r8 != null)
                            {
                                getItem15.Month12 = (Funs.GetNewIntOrZero(getItem15.Month12) - r8.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem16 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 16);
                        if (getItem16 != null)
                        {
                            var r9 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 9);
                            if (r9 != null)
                            {
                                getItem16.Month12 = (Funs.GetNewIntOrZero(getItem16.Month12) - r9.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem17 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 17);
                        if (getItem17 != null)
                        {
                            var r10 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 10);
                            if (r10 != null)
                            {
                                getItem17.Month12 = (Funs.GetNewIntOrZero(getItem17.Month12) - r10.MonthTimes ?? 0).ToString();
                            }
                        }
                        var getItem18 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 18);
                        if (getItem18 != null)
                        {
                            var r11 = getSeDin_MonthReport3.FirstOrDefault(x => x.SortIndex == 11);
                            if (r11 != null)
                            {
                                getItem18.Month12 = (Funs.GetNewIntOrZero(getItem18.Month12) - r11.MonthTimes ?? 0).ToString();
                            }
                        }
                    }
                    ////大型、特种设备
                    var getSeDin_MonthReport5 = from x in db.SeDin_MonthReport5
                                                where x.MonthReportId == monthReport.MonthReportId
                                                select x;
                    var getItem19 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 19);
                    if (getSeDin_MonthReport5.Count() > 0 && getItem19 != null)
                    {
                        int countSumR5 = getSeDin_MonthReport5.Sum(x => x.T01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.T04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T05 ?? 0) + getSeDin_MonthReport5.Sum(x => x.T06 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D01 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D02 ?? 0) + getSeDin_MonthReport5.Sum(x => x.D03 ?? 0)
                                                       + getSeDin_MonthReport5.Sum(x => x.D04 ?? 0) + getSeDin_MonthReport5.Sum(x => x.S01 ?? 0);
                        getItem19.Month12 = (Funs.GetNewIntOrZero(getItem19.Month12) - countSumR5).ToString();
                    }
                    ////安全生产费用
                    var getSeDin_MonthReport6 = db.SeDin_MonthReport6.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport6 != null)
                    {
                        var getItem20 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 20);
                        if (getItem20 != null)
                        {
                            getItem20.Month12 = (Funs.GetNewDecimalOrZero(getItem20.Month12) - getSeDin_MonthReport6.SumMonth ?? 0).ToString();
                        }
                    }
                    ////项目现场员工入场安全培训人数
                    var getSeDin_MonthReport7 = db.SeDin_MonthReport7.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport7 != null)
                    {
                        var getItem21 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 21);
                        if (getItem21 != null)
                        {
                            getItem21.Month12 = (Funs.GetNewIntOrZero(getItem21.Month12) - getSeDin_MonthReport7.EmployeeMontPerson ?? 0).ToString();
                        }
                    }
                    ////项目会次
                    var getSeDin_MonthReport8 = db.SeDin_MonthReport8.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport8 != null)
                    {
                        var getItem22 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 22);
                        if (getItem22 != null)
                        {
                            getItem22.Month12 = (Funs.GetNewIntOrZero(getItem22.Month12) - getSeDin_MonthReport8.MonthMontNum ?? 0).ToString();
                        }
                        var getItem23 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 23);
                        if (getItem23 != null)
                        {
                            getItem23.Month12 = (Funs.GetNewIntOrZero(getItem23.Month12) - getSeDin_MonthReport8.WeekMontNum ?? 0).ToString();
                        }
                        var getItem24 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 24);
                        if (getItem24 != null)
                        {
                            getItem24.Month12 = (Funs.GetNewIntOrZero(getItem24.Month12) - getSeDin_MonthReport8.SpecialMontPerson ?? 0).ToString();
                        }
                    }
                    /// 安全检查
                    var getSeDin_MonthReport9 = db.SeDin_MonthReport9.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport9 != null)
                    {
                        var getItem25 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 25);
                        if (getItem25 != null)
                        {
                            getItem25.Month12 = (Funs.GetNewIntOrZero(getItem25.Month12) - getSeDin_MonthReport9.DailyMonth ?? 0).ToString();
                        }
                        var getItem26 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 26);
                        if (getItem26 != null)
                        {
                            getItem26.Month12 = (Funs.GetNewIntOrZero(getItem26.Month12) - getSeDin_MonthReport9.WeekMonth ?? 0).ToString();
                        }
                        var getItem27 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 27);
                        if (getItem27 != null)
                        {
                            getItem27.Month12 = (Funs.GetNewIntOrZero(getItem27.Month12) - getSeDin_MonthReport9.SpecialMonth ?? 0).ToString();
                        }
                        var getItem28 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 28);
                        if (getItem28 != null)
                        {
                            getItem28.Month12 = (Funs.GetNewIntOrZero(getItem28.Month12) - getSeDin_MonthReport9.MonthlyMonth ?? 0).ToString();
                        }
                    }
                    /////隐患整改单
                    var getSeDin_MonthReport9Rectification = from x in db.SeDin_MonthReport9Item_Rectification where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Rectification.Count() > 0)
                    {
                        var getItem29 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 29);
                        if (getItem29 != null)
                        {
                            getItem29.Month12 = (Funs.GetNewIntOrZero(getItem29.Month12) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                        var getItem30 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 30);
                        if (getItem30 != null)
                        {
                            getItem30.Month12 = (Funs.GetNewIntOrZero(getItem30.Month12) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMoth ?? 0)).ToString();
                        }
                        var getItem31 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 31);
                        if (getItem31 != null)
                        {
                            getItem31.Month12 = (Funs.GetNewIntOrZero(getItem31.Month12) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthLarge ?? 0)).ToString();
                        }
                        var getItem32 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 32);
                        if (getItem32 != null)
                        {
                            getItem32.Month12 = (Funs.GetNewIntOrZero(getItem32.Month12) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothLarge ?? 0)).ToString();
                        }
                        var getItem33 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 33);
                        if (getItem33 != null)
                        {
                            getItem33.Month12 = (Funs.GetNewIntOrZero(getItem33.Month12) - getSeDin_MonthReport9Rectification.Sum(x => x.IssuedMonthSerious ?? 0)).ToString();
                        }
                        var getItem34 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 34);
                        if (getItem34 != null)
                        {
                            getItem34.Month12 = (Funs.GetNewIntOrZero(getItem34.Month12) - getSeDin_MonthReport9Rectification.Sum(x => x.RectificationMothSerious ?? 0)).ToString();
                        }
                    }
                    /////停工令
                    var getSeDin_MonthReport9Stoppage = from x in db.SeDin_MonthReport9Item_Stoppage where x.MonthReportId == monthReport.MonthReportId select x;
                    if (getSeDin_MonthReport9Stoppage.Count() > 0)
                    {
                        var getItem35 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 35);
                        if (getItem35 != null)
                        {
                            getItem35.Month12 = (Funs.GetNewIntOrZero(getItem35.Month12) - getSeDin_MonthReport9Stoppage.Sum(x => x.IssuedMonth ?? 0)).ToString();
                        }
                    }
                    ////奖惩
                    var getSeDin_MonthReport10 = db.SeDin_MonthReport10.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport10 != null)
                    {
                        var getItem36 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 36);
                        if (getItem36 != null)
                        {
                            getItem36.Month12 = (Funs.GetNewDecimalOrZero(getItem36.Month12) - getSeDin_MonthReport10.SafeMonthMoney ?? 0 - getSeDin_MonthReport10.HseMonthMoney ?? 0 - getSeDin_MonthReport10.ProduceMonthMoney ?? 0).ToString();
                        }
                        var getItem37 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 37);
                        if (getItem37 != null)
                        {
                            getItem37.Month12 = (Funs.GetNewDecimalOrZero(getItem37.Month12) - getSeDin_MonthReport10.AccidentMonthMoney ?? 0 - getSeDin_MonthReport10.ViolationMonthMoney ?? 0 - getSeDin_MonthReport10.ManageMonthMoney ?? 0).ToString();
                        }
                    }
                    ////危大工程
                    var getSeDin_MonthReport11 = db.SeDin_MonthReport11.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport11 != null)
                    {
                        var getItem38 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 38);
                        if (getItem38 != null)
                        {
                            getItem38.Month12 = (Funs.GetNewIntOrZero(getItem38.Month12) - getSeDin_MonthReport11.RiskWorkNum ?? 0).ToString();
                        }
                        var getItem39 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 39);
                        if (getItem39 != null)
                        {
                            getItem39.Month12 = (Funs.GetNewIntOrZero(getItem39.Month12) - getSeDin_MonthReport11.LargeWorkNum ?? 0).ToString();
                        }
                    }
                    ////应急
                    var getSeDin_MonthReport12 = db.SeDin_MonthReport12.FirstOrDefault(x => x.MonthReportId == monthReport.MonthReportId);
                    if (getSeDin_MonthReport12 != null)
                    {
                        var getItem40 = getHSEDataCollectItemYear.FirstOrDefault(x => x.SortIndex == 40);
                        if (getItem40 != null)
                        {
                            getItem40.Month12 = (Funs.GetNewIntOrZero(getItem40.Month12) - getSeDin_MonthReport12.MultipleSiteNum ?? 0 - getSeDin_MonthReport12.MultipleDesktopNum ?? 0
                               - getSeDin_MonthReport12.SingleSiteNum ?? 0 - getSeDin_MonthReport12.SingleDesktopNum ?? 0).ToString();
                        }
                    }
                    db.SubmitChanges();
                }
                #endregion
            }
        }
        #endregion
    }
}