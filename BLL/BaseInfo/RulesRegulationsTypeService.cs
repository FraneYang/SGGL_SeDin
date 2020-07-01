using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 安全生产规章制度类别表
    /// </summary>
    public static class RulesRegulationsTypeService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取规章制度类别
        /// </summary>
        /// <param name="rulesRegulationsTypeId"></param>
        /// <returns></returns>
        public static Model.Base_RulesRegulationsType GetRulesRegulationsTypeById(string rulesRegulationsTypeId)
        {
            return Funs.DB.Base_RulesRegulationsType.FirstOrDefault(e => e.RulesRegulationsTypeId == rulesRegulationsTypeId);
        }

        /// <summary>
        /// 添加规章制度类别
        /// </summary>
        /// <param name="rulesRegulationsType"></param>
        public static void AddRulesRegulationsType(Model.Base_RulesRegulationsType rulesRegulationsType)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_RulesRegulationsType newRulesRegulationsType = new Model.Base_RulesRegulationsType
            {
                RulesRegulationsTypeId = rulesRegulationsType.RulesRegulationsTypeId,
                RulesRegulationsTypeCode = rulesRegulationsType.RulesRegulationsTypeCode,
                RulesRegulationsTypeName = rulesRegulationsType.RulesRegulationsTypeName,
                Remark = rulesRegulationsType.Remark
            };
            db.Base_RulesRegulationsType.InsertOnSubmit(newRulesRegulationsType);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改规章制度类别
        /// </summary>
        /// <param name="rulesRegulationsType"></param>
        public static void UpdateRulesRegulationsType(Model.Base_RulesRegulationsType rulesRegulationsType)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_RulesRegulationsType newRulesRegulationsType = db.Base_RulesRegulationsType.FirstOrDefault(e => e.RulesRegulationsTypeId == rulesRegulationsType.RulesRegulationsTypeId);
            if (newRulesRegulationsType != null)
            {
                newRulesRegulationsType.RulesRegulationsTypeCode = rulesRegulationsType.RulesRegulationsTypeCode;
                newRulesRegulationsType.RulesRegulationsTypeName = rulesRegulationsType.RulesRegulationsTypeName;
                newRulesRegulationsType.Remark = rulesRegulationsType.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除规章制度类别
        /// </summary>
        /// <param name="rulesRegulationsTypeId"></param>
        public static void DeleteRulesRegulationsTypeById(string rulesRegulationsTypeId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_RulesRegulationsType rulesRegulationType = db.Base_RulesRegulationsType.FirstOrDefault(e => e.RulesRegulationsTypeId == rulesRegulationsTypeId);
            if (rulesRegulationType != null)
            {
                db.Base_RulesRegulationsType.DeleteOnSubmit(rulesRegulationType);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取规章制度类别下拉选项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_RulesRegulationsType> GetRulesRegulationsTypeList()
        {
            var list = (from x in Funs.DB.Base_RulesRegulationsType orderby x.RulesRegulationsTypeCode select x).ToList();           
            return list;
        }
    }
}
