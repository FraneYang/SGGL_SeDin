using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Data.Linq;
using System.Web.Security;
using System.Web.UI.WebControls;
using Model;
using BLL;

namespace BLL
{
    public class HSSEStandardListTypeService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 获取标准规范类别信息
        /// </summary>
        /// <param name="typeId">标准规范类别Id</param>
        /// <returns></returns>
        public static Model.Base_HSSEStandardListType GetHSSEStandardListType(string typeId)
        {
            return new Model.SGGLDB(Funs.ConnString).Base_HSSEStandardListType.FirstOrDefault(x => x.TypeId == typeId);
        }

        /// <summary>
        /// 增加标准规范类别
        /// </summary>
        /// <param name="hSSEStandardListTypeName"></param>
        /// <param name="def"></param>
        public static void AddHSSEStandardListType(Model.Base_HSSEStandardListType hSSEStandardListType)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_HSSEStandardListType newHSSEStandardListType = new Model.Base_HSSEStandardListType
            {
                TypeId = hSSEStandardListType.TypeId,
                TypeCode = hSSEStandardListType.TypeCode,
                TypeName = hSSEStandardListType.TypeName,
                Remark = hSSEStandardListType.Remark
            };

            db.Base_HSSEStandardListType.InsertOnSubmit(newHSSEStandardListType);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改标准规范类别信息
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="hSSEStandardListTypeName"></param>
        /// <param name="def"></param>
        public static void UpdateHSSEStandardListType(string typeId, string typeCode, string typeName, string remark)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_HSSEStandardListType hSSEStandardListType = db.Base_HSSEStandardListType.FirstOrDefault(e => e.TypeId == typeId);
            if (hSSEStandardListType != null)
            {
                hSSEStandardListType.TypeCode = typeCode;
                hSSEStandardListType.TypeName = typeName;
                hSSEStandardListType.Remark = remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 删除标准规范类别
        /// </summary>
        /// <param name="typeId"></param>
        public static void DeleteHSSEStandardListType(string typeId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_HSSEStandardListType hSSEStandardListType = db.Base_HSSEStandardListType.FirstOrDefault(e => e.TypeId == typeId);
            if (hSSEStandardListType != null)
            {
                db.Base_HSSEStandardListType.DeleteOnSubmit(hSSEStandardListType);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 判断是否存在标准规范类别
        /// </summary>
        /// <param name="typeId">标准规范类别</param>
        /// <returns>true:存在；false:不存在</returns>
        public static bool IsExistHSSEStandardListType(string typeId)
        {
            Model.Base_HSSEStandardListType m = new Model.SGGLDB(Funs.ConnString).Base_HSSEStandardListType.FirstOrDefault(e => e.TypeId == typeId);
            if (m != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取分包单位名称项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_HSSEStandardListType> GetHSSEStandardListTypeList()
        {
            var list = (from x in new Model.SGGLDB(Funs.ConnString).Base_HSSEStandardListType orderby x.TypeCode select x).ToList();               
            return list;
        }
    }
}
