namespace BLL
{
    using Model;
    using System.Collections.Generic;
    using System.Linq;

    public static class Base_ConsumablesService
    {
        /// <summary>
        ///获取焊接耗材信息
        /// </summary>
        /// <returns></returns>
        public static Model.Base_Consumables GetConsumablesByConsumablesId(string consumablesId)
        {
            return Funs.DB.Base_Consumables.FirstOrDefault(e => e.ConsumablesId == consumablesId);
        }

        /// <summary>
        /// 增加焊接耗材信息
        /// </summary>
        /// <param name="consumables"></param>
        public static void AddConsumables(Model.Base_Consumables consumables)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_Consumables newConsumables = new Base_Consumables
            {
                ConsumablesId = consumables.ConsumablesId,
                ConsumablesCode = consumables.ConsumablesCode,
                ConsumablesName = consumables.ConsumablesName,
                ConsumablesType = consumables.ConsumablesType,
                SteelType = consumables.SteelType,
                SteelFormat = consumables.SteelFormat,
                Standard = consumables.Standard,
                Remark = consumables.Remark,
            };

            db.Base_Consumables.InsertOnSubmit(newConsumables);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改焊接耗材信息 
        /// </summary>
        /// <param name="consumables"></param>
        public static void UpdateConsumables(Model.Base_Consumables consumables)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_Consumables newConsumables = db.Base_Consumables.FirstOrDefault(e => e.ConsumablesId == consumables.ConsumablesId);
            if (newConsumables != null)
            {
                newConsumables.ConsumablesCode = consumables.ConsumablesCode;
                newConsumables.ConsumablesName = consumables.ConsumablesName;
                newConsumables.ConsumablesType = consumables.ConsumablesType;
                newConsumables.SteelType = consumables.SteelType;
                newConsumables.SteelFormat = consumables.SteelFormat;
                newConsumables.Standard = consumables.Standard;
                newConsumables.Remark = consumables.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据焊接耗材Id删除一个焊接耗材信息
        /// </summary>
        /// <param name="consumablesId"></param>
        public static void DeleteConsumablesByConsumablesId(string consumablesId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_Consumables delConsumables = db.Base_Consumables.FirstOrDefault(e => e.ConsumablesId == consumablesId);
            if (delConsumables != null)
            {
                db.Base_Consumables.DeleteOnSubmit(delConsumables);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 按类型获取焊接耗材项
        /// </summary>
        /// <param name="consumablesType"></param>
        /// <returns></returns>
        public static List<Model.Base_Consumables> GetConsumablesListByConsumablesType(string consumablesType)
        {
            var list = (from x in Funs.DB.Base_Consumables
                        orderby x.ConsumablesName
                        select x).ToList();

            if (!string.IsNullOrEmpty(consumablesType))
            {
                list = list.Where(x => x.ConsumablesType == consumablesType).ToList();
            }

            return list;
        }

        #region 焊接耗材下拉项
        /// <summary>
        /// 焊接耗材下拉项
        /// </summary>
        /// <param name="dropName">下拉框名称</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        /// <param name="consumablesType">耗材类型</param>
        public static void InitConsumablesDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease, string consumablesType,string itemText)
        {
            dropName.DataValueField = "ConsumablesId";
            dropName.DataTextField = "ConsumablesName";
            dropName.DataSource = GetConsumablesListByConsumablesType(consumablesType);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName,itemText);
            }
        }
        #endregion
    }
}
