namespace BLL
{
    using Model;
    using System.Collections.Generic;
    using System.Linq;

    public static class Base_GrooveTypeService
    {
        /// <summary>
        ///获取坡口类型信息
        /// </summary>
        /// <returns></returns>
        public static Model.Base_GrooveType GetGrooveTypeByGrooveTypeId(string drooveTypeId)
        {
            return new Model.SGGLDB(Funs.ConnString).Base_GrooveType.FirstOrDefault(e => e.GrooveTypeId == drooveTypeId);
        }

        /// <summary>
        /// 增加坡口类型信息
        /// </summary>
        /// <param name="grooveType"></param>
        public static void AddGrooveType(Model.Base_GrooveType grooveType)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_GrooveType newGrooveType = new Base_GrooveType
            {
                GrooveTypeId = grooveType.GrooveTypeId,
                GrooveTypeCode = grooveType.GrooveTypeCode,
                GrooveTypeName = grooveType.GrooveTypeName,
                Remark = grooveType.Remark,
            };

            db.Base_GrooveType.InsertOnSubmit(newGrooveType);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改坡口类型信息 
        /// </summary>
        /// <param name="grooveType"></param>
        public static void UpdateGrooveType(Model.Base_GrooveType grooveType)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_GrooveType newGrooveType = db.Base_GrooveType.FirstOrDefault(e => e.GrooveTypeId == grooveType.GrooveTypeId);
            if (newGrooveType != null)
            {
                newGrooveType.GrooveTypeCode = grooveType.GrooveTypeCode;
                newGrooveType.GrooveTypeName = grooveType.GrooveTypeName;
                newGrooveType.Remark = grooveType.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据坡口类型Id删除一个坡口类型信息
        /// </summary>
        /// <param name="grooveTypeId"></param>
        public static void DeleteGrooveTypeByGrooveTypeId(string grooveTypeId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_GrooveType delGrooveType = db.Base_GrooveType.FirstOrDefault(e => e.GrooveTypeId == grooveTypeId);
            if (delGrooveType != null)
            {
                db.Base_GrooveType.DeleteOnSubmit(delGrooveType);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 按类型获取坡口类型项
        /// </summary>
        /// <param name="GrooveTypeType"></param>
        /// <returns></returns>
        public static List<Model.Base_GrooveType> GetGrooveTypeList()
        {
            var list = (from x in new Model.SGGLDB(Funs.ConnString).Base_GrooveType
                        orderby x.GrooveTypeCode
                        select x).ToList();

            return list;
        }

        #region 坡口类型下拉项
        /// <summary>
        /// 坡口类型下拉项
        /// </summary>
        /// <param name="dropName">下拉框名称</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        /// <param name="GrooveTypeType">耗材类型</param>
        public static void InitGrooveTypeDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease,string itemText)
        {
            dropName.DataValueField = "GrooveTypeId";
            dropName.DataTextField = "GrooveTypeCode";
            dropName.DataSource = GetGrooveTypeList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName,itemText);
            }
        }
        #endregion
    }
}
