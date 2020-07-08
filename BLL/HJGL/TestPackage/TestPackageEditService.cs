using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public class TestPackageEditService
    {
        /// <summary>
        /// 根据试压Id获取用于试压信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static Model.PTP_TestPackage GetTestPackageByID(string PTP_ID)
        {
            var view = new Model.SGGLDB(Funs.ConnString).PTP_TestPackage.FirstOrDefault(e => e.PTP_ID == PTP_ID);
            return view;
        }

        /// <summary>
        /// 根据试压Id获取用于管线明细信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static List<Model.HJGL_Pipeline> GetPipeLineListByPTP_ID(string PTP_ID)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var view = from x in db.HJGL_Pipeline
                       join y in db.PTP_PipelineList on x.PipelineId equals y.PipelineId
                       where y.PTP_ID == PTP_ID
                       select x;
            return view.ToList();
        }

        /// <summary>
        /// 增加试压信息
        /// </summary>
        /// <param name="testPackage">试压实体</param>
        public static void AddTestPackage(Model.PTP_TestPackage testPackage)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.PTP_TestPackage newTestPackage = new Model.PTP_TestPackage();
            newTestPackage.PTP_ID = testPackage.PTP_ID;
            newTestPackage.UnitWorkId = testPackage.UnitWorkId;
            newTestPackage.UnitId = testPackage.UnitId;
            newTestPackage.TestPackageNo = testPackage.TestPackageNo;
            newTestPackage.TestPackageName = testPackage.TestPackageName;
            newTestPackage.TestHeat = testPackage.TestHeat;
            newTestPackage.TestService = testPackage.TestService;
            newTestPackage.TestType = testPackage.TestType;
            newTestPackage.Finisher = testPackage.Finisher;
            newTestPackage.FinishDate = testPackage.FinishDate;
            newTestPackage.Tabler = testPackage.Tabler;
            newTestPackage.TableDate = testPackage.TableDate;
            newTestPackage.Modifier = testPackage.Modifier;
            newTestPackage.ModifyDate = testPackage.ModifyDate;
            newTestPackage.Auditer = testPackage.Auditer;
            newTestPackage.AduditDate = testPackage.AduditDate;
            newTestPackage.Remark = testPackage.Remark;
            newTestPackage.TestPackageCode = testPackage.TestPackageCode;
            newTestPackage.TestAmbientTemp = testPackage.TestAmbientTemp;
            newTestPackage.TestMediumTemp = testPackage.TestMediumTemp;
            newTestPackage.TestPressure = testPackage.TestPressure;
            newTestPackage.TestPressureTemp = testPackage.TestPressureTemp;
            newTestPackage.TestPressureTime = testPackage.TestPressureTime;
            newTestPackage.TightnessTest = testPackage.TightnessTest;
            newTestPackage.TightnessTestTemp = testPackage.TightnessTestTemp;
            newTestPackage.TightnessTestTime = testPackage.TightnessTestTime;
            newTestPackage.LeakageTestService = testPackage.LeakageTestService;
            newTestPackage.LeakageTestPressure = testPackage.LeakageTestPressure;
            newTestPackage.VacuumTestService = testPackage.VacuumTestService;
            newTestPackage.VacuumTestPressure = testPackage.VacuumTestPressure;
            newTestPackage.OperationMedium = testPackage.OperationMedium;
            newTestPackage.PurgingMedium = testPackage.PurgingMedium;
            newTestPackage.CleaningMedium = testPackage.CleaningMedium;
            newTestPackage.AllowSeepage = testPackage.AllowSeepage;
            newTestPackage.FactSeepage = testPackage.FactSeepage;
            newTestPackage.ProjectId = testPackage.ProjectId;
            //newTestPackage.InstallationId = testPackage.InstallationId;
            db.PTP_TestPackage.InsertOnSubmit(newTestPackage);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改试压信息
        /// </summary>
        /// <param name="weldReport">试压实体</param>
        public static void UpdateTestPackage(Model.PTP_TestPackage testPackage)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.PTP_TestPackage newTestPackage = db.PTP_TestPackage.First(e => e.PTP_ID == testPackage.PTP_ID);
            newTestPackage.UnitId = testPackage.UnitId;
            newTestPackage.UnitWorkId = testPackage.UnitWorkId;
            newTestPackage.TestPackageNo = testPackage.TestPackageNo;
            newTestPackage.TestPackageName = testPackage.TestPackageName;
            newTestPackage.TestHeat = testPackage.TestHeat;
            newTestPackage.TestService = testPackage.TestService;
            newTestPackage.TestType = testPackage.TestType;
            newTestPackage.Finisher = testPackage.Finisher;
            newTestPackage.FinishDate = testPackage.FinishDate;
            newTestPackage.Tabler = testPackage.Tabler;
            newTestPackage.TableDate = testPackage.TableDate;
            newTestPackage.Modifier = testPackage.Modifier;
            newTestPackage.ModifyDate = testPackage.ModifyDate;
            newTestPackage.Auditer = testPackage.Auditer;
            newTestPackage.AduditDate = testPackage.AduditDate;
            newTestPackage.Remark = testPackage.Remark;
            newTestPackage.TestPackageCode = testPackage.TestPackageCode;
            newTestPackage.TestAmbientTemp = testPackage.TestAmbientTemp;
            newTestPackage.TestMediumTemp = testPackage.TestMediumTemp;
            newTestPackage.TestPressure = testPackage.TestPressure;
            newTestPackage.TestPressureTemp = testPackage.TestPressureTemp;
            newTestPackage.TestPressureTime = testPackage.TestPressureTime;
            newTestPackage.TightnessTest = testPackage.TightnessTest;
            newTestPackage.TightnessTestTemp = testPackage.TightnessTestTemp;
            newTestPackage.TightnessTestTime = testPackage.TightnessTestTime;
            newTestPackage.LeakageTestService = testPackage.LeakageTestService;
            newTestPackage.LeakageTestPressure = testPackage.LeakageTestPressure;
            newTestPackage.VacuumTestService = testPackage.VacuumTestService;
            newTestPackage.VacuumTestPressure = testPackage.VacuumTestPressure;
            newTestPackage.OperationMedium = testPackage.OperationMedium;
            newTestPackage.PurgingMedium = testPackage.PurgingMedium;
            newTestPackage.CleaningMedium = testPackage.CleaningMedium;
            newTestPackage.AllowSeepage = testPackage.AllowSeepage;
            newTestPackage.FactSeepage = testPackage.FactSeepage;
            newTestPackage.ProjectId = testPackage.ProjectId;
            //newTestPackage.InstallationId = testPackage.InstallationId;
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据主键删除试压信息
        /// </summary>
        /// <param name="testPackageID">试压主键</param>
        public static void DeleteTestPackage(string testPackageID)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.PTP_TestPackage testPackage = db.PTP_TestPackage.First(e => e.PTP_ID == testPackageID);
            db.PTP_TestPackage.DeleteOnSubmit(testPackage);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据主键删除试压信息明细
        /// </summary>
        /// <param name="testPackageID">试压主键</param>
        public static void DeletePipelineListByPTP_ID(string testPackageID)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var testPackage = from x in db.PTP_PipelineList where x.PTP_ID == testPackageID select x;
            if (testPackage != null)
            {
                db.PTP_PipelineList.DeleteAllOnSubmit(testPackage);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 增加试压信息明细
        /// </summary>
        /// <param name="IsoList">试压明细实体</param>
        public static void AddPipelineList(Model.PTP_PipelineList IsoList)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.PTP_PipelineList newPipelineList = new Model.PTP_PipelineList();

            newPipelineList.PT_PipeId = SQLHelper.GetNewID(typeof(Model.PTP_PipelineList));
            newPipelineList.PTP_ID = IsoList.PTP_ID;
            newPipelineList.PipelineId = IsoList.PipelineId;
            newPipelineList.PT_DataType = IsoList.PT_DataType;
            db.PTP_PipelineList.InsertOnSubmit(newPipelineList);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据单位获取试压
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static int GetTestPackageByUnitId(string unitId)
        {
            var q = (from x in new Model.SGGLDB(Funs.ConnString).PTP_TestPackage where x.UnitId == unitId select x).ToList();
            return q.Count();
        }
        /// <summary>
        /// 根据装置获取试压
        /// </summary>
        /// <param name="installationId"></param>
        /// <returns></returns>
        //public static int GetTestPackageByInstallationId(string installationId)
        //{
        //    var q = (from x in new Model.SGGLDB(Funs.ConnString).PTP_TestPackage where x.InstallationId == installationId select x).ToList();
        //    return q.Count();
        //}

        /// <summary>
        /// 试压包编号是否存在
        /// </summary>
        /// <param name="pointNo"></param>
        /// <param name="pointId"></param>
        /// <returns></returns>
        public static bool IsExistTestPackageCode(string TestPackageNo, string PTP_ID, string projectId)
        {
            var q = new Model.SGGLDB(Funs.ConnString).PTP_TestPackage.FirstOrDefault(x => x.TestPackageNo == TestPackageNo && x.ProjectId == projectId && x.PTP_ID != PTP_ID);
            if (q != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 试压包是否全部（需要热处理或硬度处理的已经全部处理）
        /// </summary>
        /// <param name="PTP_ID"></param>
        /// <returns></returns>
        public static string IsExistNoHotHardItem(string PTP_ID)
        {
            string isohot = string.Empty;
            var pipelineList = from x in new Model.SGGLDB(Funs.ConnString).PTP_PipelineList where x.PTP_ID == PTP_ID select x;
            if (pipelineList.Count() > 0)
            {
                foreach (var pipe in pipelineList)
                {
                    var jots = from x in new Model.SGGLDB(Funs.ConnString).HJGL_WeldJoint where x.PipelineId == pipe.PipelineId && x.IsHotProess == true select x;
                    if (jots.Count() > 0)
                    {
                        string jotMessage = string.Empty;
                        foreach (var jotItem in jots)
                        {
                            var hotProssItem = new Model.SGGLDB(Funs.ConnString).HJGL_HotProess_TrustItem.FirstOrDefault(x => x.WeldJointId == jotItem.WeldJointId);
                            if (hotProssItem == null)
                            {
                                jotMessage += "焊口：" + jotItem.WeldJointCode + "未作热处理；";
                            }
                            else
                            {
                                var hotHardItem = new Model.SGGLDB(Funs.ConnString).HJGL_Hard_TrustItem.FirstOrDefault(x => x.WeldJointId == jotItem.WeldJointId);
                                if (hotHardItem == null)
                                {
                                    jotMessage += "焊口：" + jotItem.WeldJointCode + "未作硬度检测；";
                                }
                            }

                        }

                        if (!string.IsNullOrEmpty(jotMessage))
                        {
                            var isoinfo = BLL.PipelineService.GetPipelineByPipelineId(pipe.PipelineId);
                            if (isoinfo != null)
                            {
                                isohot += "管线：" + isoinfo.PipelineCode + "中" + jotMessage;
                            }
                        }
                    }
                }
            }
            return isohot;
        }

        /// <summary>
        /// 检验试压包检测率（管线中设置的每个检测方法的检测比例是否达标）
        /// </summary>
        /// <param name="PTP_ID"></param>
        /// <returns></returns>
        public static string InspectionIsoRate(string PTP_ID)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            string isoRate = string.Empty;
            var pipelineList = from x in new Model.SGGLDB(Funs.ConnString).PTP_PipelineList where x.PTP_ID == PTP_ID select x;
            if (pipelineList.Count() > 0)
            {
                foreach (var isoInfo in pipelineList)
                {
                    var isoinfo = BLL.PipelineService.GetPipelineByPipelineId(isoInfo.PipelineId);
                    if (isoinfo != null)
                    {
                        int jotCouts = (from x in new Model.SGGLDB(Funs.ConnString).HJGL_WeldJoint where x.PipelineId == isoinfo.PipelineId select x).Count(); //焊口总数                     
                        if (jotCouts > 0)
                        {
                            int? raleValue = null;
                            var rates = BLL.Base_DetectionRateService.GetDetectionRateByDetectionRateId(isoinfo.DetectionRateId); //探伤比例
                            if (rates != null)
                            {
                                raleValue = rates.DetectionRateValue;
                            }

                            if (raleValue.HasValue)
                            {
                                var checkJotCout = (from x in db.HJGL_Batch_NDEItem
                                                    join y in db.HJGL_Batch_BatchTrustItem on x.TrustBatchItemId equals y.TrustBatchItemId
                                                    join z in db.HJGL_Batch_BatchTrust on y.TrustBatchId equals z.TrustBatchId
                                                    join d in db.Base_DetectionType on z.DetectionTypeId equals d.DetectionTypeId
                                                    join e in db.HJGL_WeldJoint on y.WeldJointId equals e.WeldJointId
                                                    where e.PipelineId == isoInfo.PipelineId && d.SysType == "射线检测"
                                                    select y.WeldJointId).Distinct().Count();
                                decimal? realRaleValue = Convert.ToDecimal(checkJotCout / jotCouts) * 100;
                                if (realRaleValue < raleValue)
                                {
                                    isoRate += "管线：" + isoinfo.PipelineCode + "的RT实际检测比例小于应检测比例值。";
                                }
                            }
                        }
                    }
                }
            }
            return isoRate;
        }


    }
}
