using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 四不放过
    /// </summary>
    public static class NoFourLetoffService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取四不放过
        /// </summary>
        /// <param name="noFourLetoffId"></param>
        /// <returns></returns>
        public static Model.Accident_NoFourLetoff GetNoFourLetoffById(string noFourLetoffId)
        {
            return Funs.DB.Accident_NoFourLetoff.FirstOrDefault(e => e.NoFourLetoffId == noFourLetoffId);
        }

        /// <summary>
        /// 根据事故Id获取一个四不放过信息
        /// </summary>
        /// <param name="accidentHandleId">事故Id</param>
        /// <returns>一个四不放过实体</returns>
        public static Model.Accident_NoFourLetoff GetNoFourLetoffByAccidentHandleId(string accidentHandleId)
        {
            return Funs.DB.Accident_NoFourLetoff.FirstOrDefault(x => x.AccidentHandleId == accidentHandleId);
        }

        /// <summary>
        /// 添加四不放过
        /// </summary>
        /// <param name="noFourLetoff"></param>
        public static void AddNoFourLetoff(Model.Accident_NoFourLetoff noFourLetoff)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Accident_NoFourLetoff newNoFourLetoff = new Model.Accident_NoFourLetoff
            {
                NoFourLetoffId = noFourLetoff.NoFourLetoffId,
                ProjectId = noFourLetoff.ProjectId,
                AccidentHandleId = noFourLetoff.AccidentHandleId,
                NoFourLetoffCode = noFourLetoff.NoFourLetoffCode,
                UnitId = noFourLetoff.UnitId,
                AccidentDate = noFourLetoff.AccidentDate,
                FileContents = noFourLetoff.FileContents,
                RegistUnitId = noFourLetoff.RegistUnitId,
                HeadMan = noFourLetoff.HeadMan,
                RegistDate = noFourLetoff.RegistDate
            };
            db.Accident_NoFourLetoff.InsertOnSubmit(newNoFourLetoff);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改四不放过
        /// </summary>
        /// <param name="noFourLetoff"></param>
        public static void UpdateNoFourLetoff(Model.Accident_NoFourLetoff noFourLetoff)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Accident_NoFourLetoff newNoFourLetoff = db.Accident_NoFourLetoff.FirstOrDefault(e => e.NoFourLetoffId == noFourLetoff.NoFourLetoffId);
            if (newNoFourLetoff != null)
            {
                newNoFourLetoff.ProjectId = noFourLetoff.ProjectId;
                newNoFourLetoff.AccidentHandleId = noFourLetoff.AccidentHandleId;
                newNoFourLetoff.NoFourLetoffCode = noFourLetoff.NoFourLetoffCode;
                newNoFourLetoff.UnitId = noFourLetoff.UnitId;
                newNoFourLetoff.AccidentDate = noFourLetoff.AccidentDate;
                newNoFourLetoff.FileContents = noFourLetoff.FileContents;
                newNoFourLetoff.RegistUnitId = noFourLetoff.RegistUnitId;
                newNoFourLetoff.HeadMan = noFourLetoff.HeadMan;
                newNoFourLetoff.RegistDate = noFourLetoff.RegistDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除四不放过
        /// </summary>
        /// <param name="noFourLetoffId"></param>
        public static void DeleteNoFourLetoffByNoFourLetoffId(string noFourLetoffId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Accident_NoFourLetoff noFourLetoff = db.Accident_NoFourLetoff.FirstOrDefault(e => e.NoFourLetoffId == noFourLetoffId);
            if (noFourLetoff != null)
            {
                CommonService.DeleteAttachFileById(noFourLetoffId);
                ////删除审核流程表
                BLL.CommonService.DeleteFlowOperateByID(noFourLetoff.NoFourLetoffId);
                db.Accident_NoFourLetoff.DeleteOnSubmit(noFourLetoff);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据HSSE事故(含未遂)处理删除所相关的四不放过
        /// </summary>
        /// <param name="accidentHandleId"></param>
        public static void DeleteNoFourLetoffByAccidentHandleId(string accidentHandleId)
        {
            Model.SGGLDB db = Funs.DB;
            var q = (from x in db.Accident_NoFourLetoff where x.AccidentHandleId == accidentHandleId select x).ToList();
            if (q!=null)
            {
                db.Accident_NoFourLetoff.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}