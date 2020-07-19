using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 危险源清单
    /// </summary>
    public static class HazardListService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取危险源清单
        /// </summary>
        /// <param name="hazardId"></param>
        /// <returns></returns>
        public static Model.Technique_HazardList GetHazardListById(string hazardId)
        {
            return Funs.DB.Technique_HazardList.FirstOrDefault(e => e.HazardId == hazardId);
        }

        /// <summary>
        /// 根据整理人获取危险源清单
        /// </summary>
        /// <param name="compileMan"></param>
        /// <returns></returns>
        public static List<Model.Technique_HazardList> GetHazardListByCompileMan(string compileMan)
        {
            return (from x in Funs.DB.Technique_HazardList where x.CompileMan == compileMan select x).ToList();
        }

        /// <summary>
        /// 根据类别获取危险源清单集合
        /// </summary>
        /// <param name="compileMan"></param>
        /// <returns></returns>
        public static List<Model.Technique_HazardList> GetHazardListByHazardListTypeId(string hazardListTypeId)
        {
            return (from x in Funs.DB.Technique_HazardList where x.HazardListTypeId == hazardListTypeId select x).ToList();
        }

        /// <summary>
        /// 添加危险源清单
        /// </summary>
        /// <param name="hazardList"></param>
        public static void AddHazardList(Model.Technique_HazardList hazardList)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Technique_HazardList newHazardList = new Model.Technique_HazardList
            {
                HazardId = hazardList.HazardId,
                HazardListTypeId = hazardList.HazardListTypeId,
                HazardCode = hazardList.HazardCode,
                HazardItems = hazardList.HazardItems,
                DefectsType = hazardList.DefectsType,
                MayLeadAccidents = hazardList.MayLeadAccidents,
                HelperMethod = hazardList.HelperMethod,
                HazardJudge_L = hazardList.HazardJudge_L,
                HazardJudge_E = hazardList.HazardJudge_E,
                HazardJudge_C = hazardList.HazardJudge_C,
                HazardJudge_D = hazardList.HazardJudge_D,
                HazardLevel = hazardList.HazardLevel,
                ControlMeasures = hazardList.ControlMeasures,
                CompileMan = hazardList.CompileMan,
                CompileDate = hazardList.CompileDate,
                IsPass = hazardList.IsPass,
                UnitId = hazardList.UnitId,
                UpState = hazardList.UpState
            };
            db.Technique_HazardList.InsertOnSubmit(newHazardList);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改危险源清单
        /// </summary>
        /// <param name="hazardList"></param>
        public static void UpdateHazardList(Model.Technique_HazardList hazardList)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Technique_HazardList newHazardList = db.Technique_HazardList.FirstOrDefault(e => e.HazardId == hazardList.HazardId);
            if (newHazardList != null)
            {
                newHazardList.HazardListTypeId = hazardList.HazardListTypeId;
                newHazardList.HazardCode = hazardList.HazardCode;
                newHazardList.HazardItems = hazardList.HazardItems;
                newHazardList.DefectsType = hazardList.DefectsType;
                newHazardList.MayLeadAccidents = hazardList.MayLeadAccidents;
                newHazardList.HelperMethod = hazardList.HelperMethod;
                newHazardList.HazardJudge_L = hazardList.HazardJudge_L;
                newHazardList.HazardJudge_E = hazardList.HazardJudge_E;
                newHazardList.HazardJudge_C = hazardList.HazardJudge_C;
                newHazardList.HazardJudge_D = hazardList.HazardJudge_D;
                newHazardList.HazardLevel = hazardList.HazardLevel;
                newHazardList.ControlMeasures = hazardList.ControlMeasures;
                newHazardList.UpState = hazardList.UpState;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 修改危险源清单 是否采用
        /// </summary>
        /// <param name="hazardList"></param>
        public static void UpdateHazardListIsPass(Model.Technique_HazardList hazardList)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Technique_HazardList newHazardList = db.Technique_HazardList.FirstOrDefault(e => e.HazardId == hazardList.HazardId);
            if (newHazardList != null)
            {
                newHazardList.AuditMan = hazardList.AuditMan;
                newHazardList.AuditDate = hazardList.AuditDate;
                newHazardList.IsPass = hazardList.IsPass;
                newHazardList.UpState = hazardList.UpState;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除危险源清单
        /// </summary>
        /// <param name="hazardId"></param>
        public static void DeleteHazardListById(string hazardId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Technique_HazardList hazardList = db.Technique_HazardList.FirstOrDefault(e => e.HazardId == hazardId);
            if (hazardList != null)
            {
                db.Technique_HazardList.DeleteOnSubmit(hazardList);
                db.SubmitChanges();
            }
        }

        /// <summary>
        ///判断相同危险源类型下的是否存在相同代码
        /// </summary>
        /// <param name="hazardListTypeId"></param>
        /// <param name="hazardId"></param>
        /// <param name="hazardCode"></param>
        /// <returns></returns>
        public static bool IsExistHazardCode(string hazardListTypeId, string hazardId, string hazardCode)
        {
            var q = Funs.DB.Technique_HazardList.FirstOrDefault(x => x.IsPass == true && x.HazardListTypeId == hazardListTypeId && x.HazardCode == hazardCode && (x.HazardId != hazardId || (hazardId == null && x.HazardId != null)));
            if (q != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
