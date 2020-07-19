using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 施工方案模板类型
    /// </summary>
    public static class SolutionTempleteTypeService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取施工方案模板类型
        /// </summary>
        /// <param name="solutionTempleteTypeCode"></param>
        /// <returns></returns>
        public static Model.Base_SolutionTempleteType GetSolutionTempleteTypeById(string solutionTempleteTypeCode)
        {
            return Funs.DB.Base_SolutionTempleteType.FirstOrDefault(e => e.SolutionTempleteTypeCode == solutionTempleteTypeCode);
        }

        /// <summary>
        /// 添加施工方案模板类型
        /// </summary>
        /// <param name="solutionTempleteType"></param>
        public static void AddSolutionTempleteType(Model.Base_SolutionTempleteType solutionTempleteType)
        {
            Model.Base_SolutionTempleteType newSolutionTempleteType = new Model.Base_SolutionTempleteType
            {
                SolutionTempleteTypeCode = solutionTempleteType.SolutionTempleteTypeCode,
                SolutionTempleteTypeName = solutionTempleteType.SolutionTempleteTypeName,
                Remark = solutionTempleteType.Remark,
                SortIndex = solutionTempleteType.SortIndex
            };
            db.Base_SolutionTempleteType.InsertOnSubmit(newSolutionTempleteType);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改施工方案模板类型
        /// </summary>
        /// <param name="solutionTempleteType"></param>
        public static void UpdateSolutionTempleteType(Model.Base_SolutionTempleteType solutionTempleteType)
        {
            Model.Base_SolutionTempleteType newSolutionTempleteType = db.Base_SolutionTempleteType.FirstOrDefault(e => e.SolutionTempleteTypeCode == solutionTempleteType.SolutionTempleteTypeCode);
            if (newSolutionTempleteType != null)
            {
                newSolutionTempleteType.SolutionTempleteTypeName = solutionTempleteType.SolutionTempleteTypeName;
                newSolutionTempleteType.Remark = solutionTempleteType.Remark;
                newSolutionTempleteType.SortIndex = solutionTempleteType.SortIndex;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除施工方案模板类型
        /// </summary>
        /// <param name="solutionTempleteTypeCode"></param>
        public static void DeleteSolutionTempleteTypeById(string solutionTempleteTypeCode)
        {
            var solutionTemplate = db.Base_SolutionTempleteType.FirstOrDefault(e => e.SolutionTempleteTypeCode == solutionTempleteTypeCode);
            if (solutionTemplate != null)
            {
                db.Base_SolutionTempleteType.DeleteOnSubmit(solutionTemplate);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取施工方案模板类型列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_SolutionTempleteType> GetSolutionTempleteType()
        {
            return (from x in db.Base_SolutionTempleteType orderby x.SortIndex select x).ToList();
        }
    }
}
