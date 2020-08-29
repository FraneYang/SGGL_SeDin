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
            var view = Funs.DB.PTP_TestPackage.FirstOrDefault(e => e.PTP_ID == PTP_ID);
            return view;
        }

        /// <summary>
        /// 根据试压Id获取用于管线明细信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static List<Model.HJGL_Pipeline> GetPipeLineListByPTP_ID(string PTP_ID)
        {
            Model.SGGLDB db = Funs.DB;
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
            Model.SGGLDB db = Funs.DB;
            Model.PTP_TestPackage newTestPackage = new Model.PTP_TestPackage();
            newTestPackage.PTP_ID = testPackage.PTP_ID;
            newTestPackage.UnitWorkId = testPackage.UnitWorkId;
            newTestPackage.UnitId = testPackage.UnitId;
            newTestPackage.TestPackageNo = testPackage.TestPackageNo;
            newTestPackage.TestPackageName = testPackage.TestPackageName;
            newTestPackage.Finisher = testPackage.Finisher;
            newTestPackage.FinishDate = testPackage.FinishDate;
            newTestPackage.Tabler = testPackage.Tabler;
            newTestPackage.TableDate = testPackage.TableDate;
            newTestPackage.Auditer = testPackage.Auditer;
            newTestPackage.AduditDate = testPackage.AduditDate;
            newTestPackage.Remark = testPackage.Remark;
            newTestPackage.ProjectId = testPackage.ProjectId;
            newTestPackage.AdjustTestPressure = testPackage.AdjustTestPressure;
            newTestPackage.Check1 = testPackage.Check1;
            newTestPackage.Check2 = testPackage.Check2;
            newTestPackage.Check3 = testPackage.Check3;
            newTestPackage.Check4 = testPackage.Check4;
            newTestPackage.Check5 = testPackage.Check5;
            newTestPackage.Check6 = testPackage.Check6;
            newTestPackage.Check7 = testPackage.Check7;
            newTestPackage.Check8 = testPackage.Check8;
            newTestPackage.Check9 = testPackage.Check9;
            newTestPackage.Check10 = testPackage.Check10;
            newTestPackage.Check11 = testPackage.Check11;
            newTestPackage.Check12 = testPackage.Check12;
            db.PTP_TestPackage.InsertOnSubmit(newTestPackage);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改试压信息
        /// </summary>
        /// <param name="weldReport">试压实体</param>
        public static void UpdateTestPackage(Model.PTP_TestPackage testPackage)
        {
            Model.SGGLDB db = Funs.DB;
            Model.PTP_TestPackage newTestPackage = db.PTP_TestPackage.First(e => e.PTP_ID == testPackage.PTP_ID);
            newTestPackage.UnitId = testPackage.UnitId;
            newTestPackage.UnitWorkId = testPackage.UnitWorkId;
            newTestPackage.TestPackageNo = testPackage.TestPackageNo;
            newTestPackage.TestPackageName = testPackage.TestPackageName;
            newTestPackage.Finisher = testPackage.Finisher;
            newTestPackage.FinishDate = testPackage.FinishDate;
            newTestPackage.Tabler = testPackage.Tabler;
            newTestPackage.TableDate = testPackage.TableDate;
            newTestPackage.Auditer = testPackage.Auditer;
            newTestPackage.AduditDate = testPackage.AduditDate;
            newTestPackage.Remark = testPackage.Remark;
            newTestPackage.AdjustTestPressure = testPackage.AdjustTestPressure;
            newTestPackage.ProjectId = testPackage.ProjectId;
            newTestPackage.Check1 = testPackage.Check1;
            newTestPackage.Check2 = testPackage.Check2;
            newTestPackage.Check3 = testPackage.Check3;
            newTestPackage.Check4 = testPackage.Check4;
            newTestPackage.Check5 = testPackage.Check5;
            newTestPackage.Check6 = testPackage.Check6;
            newTestPackage.Check7 = testPackage.Check7;
            newTestPackage.Check8 = testPackage.Check8;
            newTestPackage.Check9 = testPackage.Check9;
            newTestPackage.Check10 = testPackage.Check10;
            newTestPackage.Check11 = testPackage.Check11;
            newTestPackage.Check12 = testPackage.Check12;
            newTestPackage.TestMediumTemperature = testPackage.TestMediumTemperature;
            newTestPackage.AmbientTemperature = testPackage.AmbientTemperature;
            newTestPackage.HoldingTime = testPackage.HoldingTime;
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据主键删除试压信息
        /// </summary>
        /// <param name="testPackageID">试压主键</param>
        public static void DeleteTestPackage(string testPackageID)
        {
            Model.SGGLDB db = Funs.DB;
            Model.PTP_TestPackage testPackage = db.PTP_TestPackage.First(e => e.PTP_ID == testPackageID);
            var ItemCheck = from x in db.PTP_ItemEndCheck where x.PTP_ID == testPackageID select x;
            if (ItemCheck != null)
            {
                db.PTP_ItemEndCheck.DeleteAllOnSubmit(ItemCheck);
                db.SubmitChanges();
            }
            var Approve = from x in db.PTP_TestPackageApprove where x.PTP_ID == testPackageID select x;
            if (Approve != null)
            {
                db.PTP_TestPackageApprove.DeleteAllOnSubmit(Approve);
                db.SubmitChanges();
            }
            db.PTP_TestPackage.DeleteOnSubmit(testPackage);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据主键删除试压信息明细
        /// </summary>
        /// <param name="testPackageID">试压主键</param>
        public static void DeletePipelineListByPTP_ID(string testPackageID)
        {
            Model.SGGLDB db = Funs.DB;
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
            Model.SGGLDB db = Funs.DB;
            Model.PTP_PipelineList newPipelineList = new Model.PTP_PipelineList();
            newPipelineList.PT_PipeId = SQLHelper.GetNewID(typeof(Model.PTP_PipelineList));
            newPipelineList.PTP_ID = IsoList.PTP_ID;
            newPipelineList.PipelineId = IsoList.PipelineId;
            newPipelineList.PT_DataType = IsoList.PT_DataType;
            newPipelineList.DesignPress = IsoList.DesignPress;
            newPipelineList.DesignTemperature = IsoList.DesignTemperature;
            newPipelineList.AmbientTemperature = IsoList.AmbientTemperature;
            newPipelineList.TestMedium = IsoList.TestMedium;
            newPipelineList.TestMediumTemperature = IsoList.TestMediumTemperature;
            newPipelineList.TestPressure = IsoList.TestPressure;
            newPipelineList.HoldingTime = IsoList.HoldingTime;
            db.PTP_PipelineList.InsertOnSubmit(newPipelineList);
            db.SubmitChanges();
        }
        /// <summary>
        /// 修改试压信息明细
        /// </summary>
        /// <param name="IsoList">试压明细实体</param>
        public static void UpdatePipelineList(Model.PTP_PipelineList IsoList)
        {
            Model.SGGLDB db = Funs.DB;
            Model.PTP_PipelineList newPipelineList = db.PTP_PipelineList.FirstOrDefault(e => e.PT_PipeId == IsoList.PT_PipeId);
            newPipelineList.PTP_ID = IsoList.PTP_ID;
            newPipelineList.PipelineId = IsoList.PipelineId;
            newPipelineList.PT_DataType = IsoList.PT_DataType;
            newPipelineList.DesignPress = IsoList.DesignPress;
            newPipelineList.DesignTemperature = IsoList.DesignTemperature;
            newPipelineList.AmbientTemperature = IsoList.AmbientTemperature;
            newPipelineList.TestMedium = IsoList.TestMedium;
            newPipelineList.TestMediumTemperature = IsoList.TestMediumTemperature;
            newPipelineList.TestPressure = IsoList.TestPressure;
            newPipelineList.HoldingTime = IsoList.HoldingTime;
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据单位获取试压
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static int GetTestPackageByUnitId(string unitId)
        {
            var q = (from x in Funs.DB.PTP_TestPackage where x.UnitId == unitId select x).ToList();
            return q.Count();
        }
        /// <summary>
        /// 根据装置获取试压
        /// </summary>
        /// <param name="installationId"></param>
        /// <returns></returns>
        //public static int GetTestPackageByInstallationId(string installationId)
        //{
        //    var q = (from x in Funs.DB.PTP_TestPackage where x.InstallationId == installationId select x).ToList();
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
            var q = Funs.DB.PTP_TestPackage.FirstOrDefault(x => x.TestPackageNo == TestPackageNo && x.ProjectId == projectId && x.PTP_ID != PTP_ID);
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
            var pipelineList = from x in Funs.DB.PTP_PipelineList where x.PTP_ID == PTP_ID select x;
            if (pipelineList.Count() > 0)
            {
                foreach (var pipe in pipelineList)
                {
                    var jots = from x in Funs.DB.HJGL_WeldJoint where x.PipelineId == pipe.PipelineId && x.IsHotProess == true select x;
                    if (jots.Count() > 0)
                    {
                        string jotMessage = string.Empty;
                        foreach (var jotItem in jots)
                        {
                            var hotProssItem = Funs.DB.HJGL_HotProess_TrustItem.FirstOrDefault(x => x.WeldJointId == jotItem.WeldJointId);
                            if (hotProssItem == null)
                            {
                                jotMessage += "焊口：" + jotItem.WeldJointCode + "未作热处理；";
                            }
                            else
                            {
                                var hotHardItem = Funs.DB.HJGL_Hard_TrustItem.FirstOrDefault(x => x.WeldJointId == jotItem.WeldJointId);
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
            Model.SGGLDB db = Funs.DB;
            string isoRate = string.Empty;
            var pipelineList = from x in db.PTP_PipelineList where x.PTP_ID == PTP_ID select x;
            if (pipelineList.Count() > 0)
            {
                foreach (var isoInfo in pipelineList)
                {
                    var isoinfo = BLL.PipelineService.GetPipelineByPipelineId(isoInfo.PipelineId);
                    if (isoinfo != null)
                    {
                        int jotCouts = (from x in db.HJGL_WeldJoint where x.PipelineId == isoinfo.PipelineId select x).Count(); //焊口总数                     
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
        public static void Init(FineUIPro.DropDownList dropName, string state, bool isShowPlease)
        {
            dropName.DataValueField = "Value";
            dropName.DataTextField = "Text";
            dropName.DataSource = GetHandleTypeByState(state);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        /// <summary>
        /// 根据状态选择下一步办理类型
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static ListItem[] GetHandleTypeByState(string state)
        {
            if (state == BLL.Const.TestPackage_Compile )
            {
                ListItem[] lis = new ListItem[1];
                lis[0] = new ListItem("施工分包商整改", Const.TestPackage_Audit1);
                return lis;
            }
            else if (state == Const.TestPackage_Audit1||state==Const.TestPackage_ReAudit2)
            {
                ListItem[] lis = new ListItem[1];
                lis[0] = new ListItem("总包确认", Const.TestPackage_Audit2);
                return lis;
            }
            else if (state == Const.TestPackage_Audit2)//有是否同意
            {
                ListItem[] lis = new ListItem[2];
                lis[0] = new ListItem("监理确认", Const.TestPackage_Audit3);//是 加载
                lis[1] = new ListItem("审批完成", Const.TestPackage_Complete);//是加载
                return lis;
            }
            else if (state ==Const.TestPackage_Audit3)
            {
                ListItem[] lis = new ListItem[1];
                lis[0] = new ListItem("审批完成", "5");
                return lis;
            }
            else if (state == "F")//选否
            {
                ListItem[] lis = new ListItem[1];
                lis[0] = new ListItem("施工分包商重新整改", Const.TestPackage_ReAudit2);//否加载
                return lis;
            }
            else
                return null;
        }

    }
}
