using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LeakVacuumEditService
    {
        /// <summary>
        /// 根据试压Id获取用于试压信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static Model.HJGL_LV_LeakVacuum GetLeakVacuumByID(string LeakVacuumId)
        {
            var view = Funs.DB.HJGL_LV_LeakVacuum.FirstOrDefault(e => e.LeakVacuumId == LeakVacuumId);
            return view;
        }

        /// <summary>
        /// 根据试压Id获取用于管线明细信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static List<Model.HJGL_Pipeline> GetPipeLineListByLeakVacuumId(string LeakVacuumId)
        {
            Model.SGGLDB db = Funs.DB;
            var view = from x in db.HJGL_Pipeline
                       join y in db.HJGL_LV_Pipeline on x.PipelineId equals y.PipelineId
                       where y.LeakVacuumId == LeakVacuumId
                       select x;
            return view.ToList();
        }

        /// <summary>
        /// 增加试压信息
        /// </summary>
        /// <param name="LeakVacuum">试压实体</param>
        public static void AddLeakVacuum(Model.HJGL_LV_LeakVacuum LeakVacuum)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_LV_LeakVacuum newLeakVacuum = new Model.HJGL_LV_LeakVacuum();
            newLeakVacuum.LeakVacuumId = LeakVacuum.LeakVacuumId;
            newLeakVacuum.UnitWorkId = LeakVacuum.UnitWorkId;
            newLeakVacuum.UnitId = LeakVacuum.UnitId;
            newLeakVacuum.SysNo = LeakVacuum.SysNo;
            newLeakVacuum.SysName = LeakVacuum.SysName;
            newLeakVacuum.Finisher = LeakVacuum.Finisher;
            newLeakVacuum.FinishDate = LeakVacuum.FinishDate;
            newLeakVacuum.Tabler = LeakVacuum.Tabler;
            newLeakVacuum.TableDate = LeakVacuum.TableDate;
            newLeakVacuum.Auditer = LeakVacuum.Auditer;
            newLeakVacuum.AduditDate = LeakVacuum.AduditDate;
            newLeakVacuum.Remark = LeakVacuum.Remark;
            newLeakVacuum.ProjectId = LeakVacuum.ProjectId;
            newLeakVacuum.Check1 = LeakVacuum.Check1;
            newLeakVacuum.Check2 = LeakVacuum.Check2;
            newLeakVacuum.Check3 = LeakVacuum.Check3;
            newLeakVacuum.Check4 = LeakVacuum.Check4;
            newLeakVacuum.Check5 = LeakVacuum.Check5;
            db.HJGL_LV_LeakVacuum.InsertOnSubmit(newLeakVacuum);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改试压信息
        /// </summary>
        /// <param name="weldReport">试压实体</param>
        public static void UpdateLeakVacuum(Model.HJGL_LV_LeakVacuum LeakVacuum)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_LV_LeakVacuum newLeakVacuum = db.HJGL_LV_LeakVacuum.First(e => e.LeakVacuumId == LeakVacuum.LeakVacuumId);
            newLeakVacuum.UnitId = LeakVacuum.UnitId;
            newLeakVacuum.UnitWorkId = LeakVacuum.UnitWorkId;
            newLeakVacuum.SysNo = LeakVacuum.SysNo;
            newLeakVacuum.SysName = LeakVacuum.SysName;
            newLeakVacuum.Finisher = LeakVacuum.Finisher;
            newLeakVacuum.FinishDate = LeakVacuum.FinishDate;
            newLeakVacuum.Tabler = LeakVacuum.Tabler;
            newLeakVacuum.TableDate = LeakVacuum.TableDate;
            newLeakVacuum.Auditer = LeakVacuum.Auditer;
            newLeakVacuum.AduditDate = LeakVacuum.AduditDate;
            newLeakVacuum.Remark = LeakVacuum.Remark;
            newLeakVacuum.ProjectId = LeakVacuum.ProjectId;
            newLeakVacuum.Check1 = LeakVacuum.Check1;
            newLeakVacuum.Check2 = LeakVacuum.Check2;
            newLeakVacuum.Check3 = LeakVacuum.Check3;
            newLeakVacuum.Check4 = LeakVacuum.Check4;
            newLeakVacuum.Check5 = LeakVacuum.Check5;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据主键删除试压信息
        /// </summary>
        /// <param name="LeakVacuumID">试压主键</param>
        public static void DeleteLeakVacuum(string LeakVacuumID)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_LV_LeakVacuum LeakVacuum = db.HJGL_LV_LeakVacuum.First(e => e.LeakVacuumId == LeakVacuumID);
            db.HJGL_LV_LeakVacuum.DeleteOnSubmit(LeakVacuum);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据主键删除试压信息明细
        /// </summary>
        /// <param name="LeakVacuumID">试压主键</param>
        public static void DeletePipelineListByLeakVacuumId(string LeakVacuumID)
        {
            Model.SGGLDB db = Funs.DB;
            var LeakVacuum = from x in db.HJGL_LV_Pipeline where x.LeakVacuumId == LeakVacuumID select x;
            if (LeakVacuum != null)
            {
                foreach (var item in LeakVacuum)
                {
                    var ItemCheck = from x in db.HJGL_LV_ItemEndCheck where x.LV_PipeId == item.LV_PipeId select x;
                    if (ItemCheck != null) {
                        db.HJGL_LV_ItemEndCheck.DeleteAllOnSubmit(ItemCheck);
                        db.SubmitChanges();
                    }
                }
                db.HJGL_LV_Pipeline.DeleteAllOnSubmit(LeakVacuum);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 增加试压信息明细
        /// </summary>
        /// <param name="IsoList">试压明细实体</param>
        public static void AddPipelineList(Model.HJGL_LV_Pipeline IsoList)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_LV_Pipeline newPipelineList = new Model.HJGL_LV_Pipeline();
            newPipelineList.LV_PipeId = SQLHelper.GetNewID(typeof(Model.HJGL_LV_Pipeline));
            newPipelineList.LeakVacuumId = IsoList.LeakVacuumId;
            newPipelineList.PipelineId = IsoList.PipelineId;
            newPipelineList.DesignPress = IsoList.DesignPress;
            newPipelineList.DesignTemperature = IsoList.DesignTemperature;
            newPipelineList.AmbientTemperature = IsoList.AmbientTemperature;
            newPipelineList.LeakPressure = IsoList.LeakPressure;
            newPipelineList.LeakMedium = IsoList.LeakMedium;
            newPipelineList.VacuumPressure = IsoList.VacuumPressure;
            newPipelineList.VacuumMedium = IsoList.VacuumMedium;
            newPipelineList.TestMediumTemperature = IsoList.TestMediumTemperature;
            db.HJGL_LV_Pipeline.InsertOnSubmit(newPipelineList);
            db.SubmitChanges();
        }
        /// <summary>
        /// 修改试压信息明细
        /// </summary>
        /// <param name="IsoList">试压明细实体</param>
        public static void UpdatePipelineList(Model.HJGL_LV_Pipeline IsoList)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_LV_Pipeline newPipelineList = db.HJGL_LV_Pipeline.FirstOrDefault(e => e.LV_PipeId == IsoList.LV_PipeId);
            newPipelineList.LeakVacuumId = IsoList.LeakVacuumId;
            newPipelineList.PipelineId = IsoList.PipelineId;
            newPipelineList.DesignPress = IsoList.DesignPress;
            newPipelineList.DesignTemperature = IsoList.DesignTemperature;
            newPipelineList.AmbientTemperature = IsoList.AmbientTemperature;
            newPipelineList.LeakPressure = IsoList.LeakPressure;
            newPipelineList.LeakMedium = IsoList.LeakMedium;
            newPipelineList.VacuumPressure = IsoList.VacuumPressure;
            newPipelineList.VacuumMedium = IsoList.VacuumMedium;
            newPipelineList.TestMediumTemperature = IsoList.TestMediumTemperature;
            db.SubmitChanges();
        }
        /// <summary>
        /// 根据单位获取试压
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static int GetLeakVacuumByUnitId(string unitId)
        {
            var q = (from x in Funs.DB.HJGL_LV_LeakVacuum where x.UnitId == unitId select x).ToList();
            return q.Count();
        }
        /// <summary>
        /// 根据装置获取试压
        /// </summary>
        /// <param name="installationId"></param>
        /// <returns></returns>
        //public static int GetLeakVacuumByInstallationId(string installationId)
        //{
        //    var q = (from x in Funs.DB.HJGL_LV_LeakVacuum where x.InstallationId == installationId select x).ToList();
        //    return q.Count();
        //}

        /// <summary>
        /// 试压包编号是否存在
        /// </summary>
        /// <param name="pointNo"></param>
        /// <param name="pointId"></param>
        /// <returns></returns>
        public static bool IsExistLeakVacuumCode(string SysNo, string LeakVacuumId, string projectId)
        {
            var q = Funs.DB.HJGL_LV_LeakVacuum.FirstOrDefault(x => x.SysNo == SysNo && x.ProjectId == projectId && x.LeakVacuumId != LeakVacuumId);
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
        /// <param name="LeakVacuumId"></param>
        /// <returns></returns>
        public static string IsExistNoHotHardItem(string LeakVacuumId)
        {
            string isohot = string.Empty;
            var pipelineList = from x in Funs.DB.HJGL_LV_Pipeline where x.LeakVacuumId == LeakVacuumId select x;
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
        /// <param name="LeakVacuumId"></param>
        /// <returns></returns>
        public static string InspectionIsoRate(string LeakVacuumId)
        {
            Model.SGGLDB db = Funs.DB;
            string isoRate = string.Empty;
            var pipelineList = from x in db.HJGL_LV_Pipeline where x.LeakVacuumId == LeakVacuumId select x;
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


    }
}
