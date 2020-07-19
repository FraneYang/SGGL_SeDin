using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class UnitTypeService
    {

        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Base_UnitType GetUnitTypeById(string unitTypeId)
        {
            return Funs.DB.Base_UnitType.FirstOrDefault(e => e.UnitTypeId == unitTypeId);
        }

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Base_UnitType GetUnitTypeByName(string unitTypeName)
        {
            return Funs.DB.Base_UnitType.FirstOrDefault(e => e.UnitTypeName == unitTypeName);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="?"></param>
        public static void AddUnitType(Model.Base_UnitType unitType)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_UnitType newUnitType = new Model.Base_UnitType
            {
                UnitTypeId = unitType.UnitTypeId,
                UnitTypeCode = unitType.UnitTypeCode,
                UnitTypeName = unitType.UnitTypeName,
                Remark = unitType.Remark
            };

            db.Base_UnitType.InsertOnSubmit(newUnitType);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void UpdateUnitType(Model.Base_UnitType unitType)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_UnitType newUnitType = db.Base_UnitType.FirstOrDefault(e => e.UnitTypeId == unitType.UnitTypeId);
            if (newUnitType != null)
            {
                newUnitType.UnitTypeCode = unitType.UnitTypeCode;
                newUnitType.UnitTypeName = unitType.UnitTypeName;
                newUnitType.Remark = unitType.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="unitTypeId"></param>
        public static void DeleteUnitTypeById(string unitTypeId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_UnitType unitType = db.Base_UnitType.FirstOrDefault(e => e.UnitTypeId == unitTypeId);
            {
                db.Base_UnitType.DeleteOnSubmit(unitType);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取类别下拉项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_UnitType> GetUnitTypeList()
        {
            var list = (from x in Funs.DB.Base_UnitType orderby x.UnitTypeCode select x).ToList();
            return list;
        }
        /// <summary>
        /// 获取单位类型下拉选项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_UnitType> GetUnitTypeDropDownList()
        {
            var list = (from x in Funs.DB.Base_UnitType orderby x.UnitTypeCode select x).ToList();          
            return list;
        }

        #region 单位类型表下拉框
        /// <summary>
        ///  单位类型表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitUnitTypeDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "UnitTypeId";
            dropName.DataTextField = "UnitTypeName";
            dropName.DataSource = BLL.UnitTypeService.GetUnitTypeDropDownList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}