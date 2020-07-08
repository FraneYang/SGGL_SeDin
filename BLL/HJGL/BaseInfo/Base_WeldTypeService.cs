namespace BLL
{
    using Model;
    using System.Collections.Generic;
    using System.Linq;

    public static class Base_WeldTypeService
    {
        /// <summary>
        ///获取焊缝类型信息
        /// </summary>
        /// <returns></returns>
        public static Model.Base_WeldType GetWeldTypeByWeldTypeId(string weldTypeId)
        {
            return new Model.SGGLDB(Funs.ConnString).Base_WeldType.FirstOrDefault(e => e.WeldTypeId == weldTypeId);
        }
        /// <summary>
        /// 增加焊缝类型信息
        /// </summary>
        /// <param name="weldType"></param>
        public static void AddWeldType(Model.Base_WeldType weldType)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_WeldType newWeldType = new Base_WeldType
            {
                WeldTypeId = weldType.WeldTypeId,
                WeldTypeCode = weldType.WeldTypeCode,
                WeldTypeName = weldType.WeldTypeName,
                DetectionType = weldType.DetectionType,
                Thickness = weldType.Thickness,
            Remark = weldType.Remark,
            };
            db.Base_WeldType.InsertOnSubmit(newWeldType);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改焊缝类型信息 
        /// </summary>
        /// <param name="weldType"></param>
        public static void UpdateWeldType(Model.Base_WeldType weldType)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_WeldType newWeldType = db.Base_WeldType.FirstOrDefault(e => e.WeldTypeId == weldType.WeldTypeId);
            if (newWeldType != null)
            {
                newWeldType.WeldTypeCode = weldType.WeldTypeCode;
                newWeldType.WeldTypeName = weldType.WeldTypeName;
                newWeldType.DetectionType = weldType.DetectionType;
                newWeldType.Thickness = weldType.Thickness;
                newWeldType.Remark = weldType.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据焊缝类型Id删除一个焊缝类型信息
        /// </summary>
        /// <param name="weldTypeId"></param>
        public static void DeleteWeldTypeByWeldTypeId(string weldTypeId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_WeldType delWeldType = db.Base_WeldType.FirstOrDefault(e => e.WeldTypeId == weldTypeId);
            if (delWeldType != null)
            {
                db.Base_WeldType.DeleteOnSubmit(delWeldType);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 按类型获取焊缝类型项
        /// </summary>
        /// <param name="WeldTypeType"></param>
        /// <returns></returns>
        public static List<Model.Base_WeldType> GetWeldTypeList()
        {
            var list = (from x in new Model.SGGLDB(Funs.ConnString).Base_WeldType
                        orderby x.WeldTypeCode
                        select x).ToList();

            return list;
        }

        #region 焊缝类型下拉项
        /// <summary>
        /// 焊缝类型下拉项
        /// </summary>
        /// <param name="dropName">下拉框名称</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        /// <param name="WeldTypeType">耗材类型</param>
        public static void InitWeldTypeDropDownList(FineUIPro.DropDownList dropName,string itemText, bool isShowPlease)
        {
            dropName.DataValueField = "WeldTypeId";
            dropName.DataTextField = "WeldTypeCode";
            dropName.DataSource = GetWeldTypeList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName, itemText);
            }
        }
        #endregion
    }
}
