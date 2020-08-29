using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PurgingCleaningEditService
    {
        /// <summary>
        /// 根据试压Id获取用于试压信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static Model.HJGL_PC_PurgingCleaning GetPurgingCleaningByID(string PurgingCleaningId)
        {
            var view = Funs.DB.HJGL_PC_PurgingCleaning.FirstOrDefault(e => e.PurgingCleaningId == PurgingCleaningId);
            return view;
        }

        /// <summary>
        /// 根据试压Id获取用于管线明细信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static List<Model.HJGL_Pipeline> GetPipeLineListByPurgingCleaningId(string PurgingCleaningId)
        {
            Model.SGGLDB db = Funs.DB;
            var view = from x in db.HJGL_Pipeline
                       join y in db.HJGL_PC_Pipeline on x.PipelineId equals y.PipelineId
                       where y.PurgingCleaningId == PurgingCleaningId
                       select x;
            return view.ToList();
        }

        /// <summary>
        /// 增加试压信息
        /// </summary>
        /// <param name="LeakVacuum">试压实体</param>
        public static void AddPurgingCleaning(Model.HJGL_PC_PurgingCleaning PurgingCleaning)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_PC_PurgingCleaning newPurgingCleaning = new Model.HJGL_PC_PurgingCleaning();
            newPurgingCleaning.PurgingCleaningId = PurgingCleaning.PurgingCleaningId;
            newPurgingCleaning.UnitWorkId = PurgingCleaning.UnitWorkId;
            newPurgingCleaning.UnitId = PurgingCleaning.UnitId;
            newPurgingCleaning.SysNo = PurgingCleaning.SysNo;
            newPurgingCleaning.SysName = PurgingCleaning.SysName;
            newPurgingCleaning.Finisher = PurgingCleaning.Finisher;
            newPurgingCleaning.FinishDate = PurgingCleaning.FinishDate;
            newPurgingCleaning.Tabler = PurgingCleaning.Tabler;
            newPurgingCleaning.TableDate = PurgingCleaning.TableDate;
            newPurgingCleaning.Auditer = PurgingCleaning.Auditer;
            newPurgingCleaning.AduditDate = PurgingCleaning.AduditDate;
            newPurgingCleaning.Remark = PurgingCleaning.Remark;
            newPurgingCleaning.ProjectId = PurgingCleaning.ProjectId;
            newPurgingCleaning.Check1 = PurgingCleaning.Check1;
            newPurgingCleaning.Check2 = PurgingCleaning.Check2;
            newPurgingCleaning.Check3 = PurgingCleaning.Check3;
            newPurgingCleaning.Check4 = PurgingCleaning.Check4;
            db.HJGL_PC_PurgingCleaning.InsertOnSubmit(newPurgingCleaning);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改试压信息
        /// </summary>
        /// <param name="weldReport">试压实体</param>
        public static void UpdatePurgingCleaning(Model.HJGL_PC_PurgingCleaning PurgingCleaning)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_PC_PurgingCleaning newPurgingCleaning = db.HJGL_PC_PurgingCleaning.First(e => e.PurgingCleaningId ==PurgingCleaning.PurgingCleaningId);
            newPurgingCleaning.UnitId = PurgingCleaning.UnitId;
            newPurgingCleaning.UnitWorkId = PurgingCleaning.UnitWorkId;
            newPurgingCleaning.SysNo = PurgingCleaning.SysNo;
            newPurgingCleaning.SysName = PurgingCleaning.SysName;
            newPurgingCleaning.Finisher = PurgingCleaning.Finisher;
            newPurgingCleaning.FinishDate = PurgingCleaning.FinishDate;
            newPurgingCleaning.Tabler = PurgingCleaning.Tabler;
            newPurgingCleaning.TableDate = PurgingCleaning.TableDate;
            newPurgingCleaning.Auditer = PurgingCleaning.Auditer;
            newPurgingCleaning.AduditDate = PurgingCleaning.AduditDate;
            newPurgingCleaning.Remark = PurgingCleaning.Remark;
            newPurgingCleaning.ProjectId = PurgingCleaning.ProjectId;
            newPurgingCleaning.Check1 = PurgingCleaning.Check1;
            newPurgingCleaning.Check2 = PurgingCleaning.Check2;
            newPurgingCleaning.Check3 = PurgingCleaning.Check3;
            newPurgingCleaning.Check4 = PurgingCleaning.Check4;
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据主键删除试压信息
        /// </summary>
        /// <param name="PurgingCleaningId">试压主键</param>
        public static void DeletePurgingCleaning(string PurgingCleaningId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_PC_PurgingCleaning PurgingCleaning = db.HJGL_PC_PurgingCleaning.First(e => e.PurgingCleaningId == PurgingCleaningId);
            db.HJGL_PC_PurgingCleaning.DeleteOnSubmit(PurgingCleaning);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据主键删除试压信息明细
        /// </summary>
        /// <param name="PurgingCleaningId">试压主键</param>
        public static void DeletePipelineListByPurgingCleaningId(string PurgingCleaningId)
        {
            Model.SGGLDB db = Funs.DB;
            var LeakVacuum = from x in db.HJGL_PC_Pipeline where x.PurgingCleaningId == PurgingCleaningId select x;
            if (LeakVacuum != null)
            {
                foreach (var item in LeakVacuum)
                {
                    var ItemCheck = from x in db.HJGL_PC_ItemEndCheck where x.PC_PipeId == item.PC_PipeId select x;
                    if (ItemCheck != null)
                    {
                        db.HJGL_PC_ItemEndCheck.DeleteAllOnSubmit(ItemCheck);
                        db.SubmitChanges();
                    }
                }
                db.HJGL_PC_Pipeline.DeleteAllOnSubmit(LeakVacuum);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 增加试压信息明细
        /// </summary>
        /// <param name="IsoList">试压明细实体</param>
        public static void AddPipelineList(Model.HJGL_PC_Pipeline IsoList)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_PC_Pipeline newPipelineList = new Model.HJGL_PC_Pipeline();
            newPipelineList.PC_PipeId = SQLHelper.GetNewID(typeof(Model.HJGL_PC_Pipeline));
            newPipelineList.PurgingCleaningId = IsoList.PurgingCleaningId;
            newPipelineList.PipelineId = IsoList.PipelineId;
            newPipelineList.MaterialId = IsoList.MaterialId;
            newPipelineList.MediumId = IsoList.MediumId;
            newPipelineList.PurgingMedium = IsoList.PurgingMedium;
            newPipelineList.CleaningMedium = IsoList.CleaningMedium;
            db.HJGL_PC_Pipeline.InsertOnSubmit(newPipelineList);
            db.SubmitChanges();
        }
        /// <summary>
        /// 修改试压信息明细
        /// </summary>
        /// <param name="IsoList">试压明细实体</param>
        public static void UpdatePipelineList(Model.HJGL_PC_Pipeline IsoList)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_PC_Pipeline newPipelineList = db.HJGL_PC_Pipeline.FirstOrDefault(e => e.PC_PipeId == IsoList.PC_PipeId);
            newPipelineList.PurgingCleaningId = IsoList.PurgingCleaningId;
            newPipelineList.PipelineId = IsoList.PipelineId;
            newPipelineList.MaterialId = IsoList.MaterialId;
            newPipelineList.MediumId = IsoList.MediumId;
            newPipelineList.PurgingMedium = IsoList.PurgingMedium;
            newPipelineList.CleaningMedium = IsoList.CleaningMedium;
            db.SubmitChanges();
        }
        /// <summary>
        /// 根据单位获取试压
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static int GetPurgingCleaningByUnitId(string unitId)
        {
            var q = (from x in Funs.DB.HJGL_PC_PurgingCleaning where x.UnitId == unitId select x).ToList();
            return q.Count();
        }
        /// <summary>
        /// 试压包编号是否存在
        /// </summary>
        /// <param name="pointNo"></param>
        /// <param name="pointId"></param>
        /// <returns></returns>
        public static bool IsExistPurgingCleaningCode(string SysNo, string PurgingCleaningId, string projectId)
        {
            var q = Funs.DB.HJGL_PC_PurgingCleaning.FirstOrDefault(x => x.SysNo == SysNo && x.ProjectId == projectId && x.PurgingCleaningId != PurgingCleaningId);
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
        /// <param name="PurgingCleaningId"></param>
        /// <returns></returns>
        public static string IsExistNoHotHardItem(string PurgingCleaningId)
        {
            string isohot = string.Empty;
            var pipelineList = from x in Funs.DB.HJGL_PC_Pipeline where x.PurgingCleaningId == PurgingCleaningId select x;
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
        /// <param name="PurgingCleaningId"></param>
        /// <returns></returns>
        public static string InspectionIsoRate(string PurgingCleaningId)
        {
            Model.SGGLDB db = Funs.DB;
            string isoRate = string.Empty;
            var pipelineList = from x in db.HJGL_PC_Pipeline where x.PurgingCleaningId == PurgingCleaningId select x;
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
