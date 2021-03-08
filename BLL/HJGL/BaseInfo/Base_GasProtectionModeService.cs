using Model;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public static class Base_GasProtectionModeService
    {
        /// <summary>
        ///获取气体保护方式信息
        /// </summary>
        /// <returns></returns>
        public static Model.Base_GasProtectionMode GetGasProtectionModeByGasProtectionModeId(string gasProtectionModeId)
        {
            return Funs.DB.Base_GasProtectionMode.FirstOrDefault(e => e.GasProtectionModeId == gasProtectionModeId);
        }

        /// <summary>
        /// 增加气体保护方式信息
        /// </summary>
        /// <param name="gasProtectionMode"></param>
        public static void AddGasProtectionMode(Model.Base_GasProtectionMode gasProtectionMode)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_GasProtectionMode newGasProtectionMode = new Base_GasProtectionMode
            {
                GasProtectionModeId = gasProtectionMode.GasProtectionModeId,
                GasProtectionModeName = gasProtectionMode.GasProtectionModeName,
                Remark = gasProtectionMode.Remark,
            };

            db.Base_GasProtectionMode.InsertOnSubmit(newGasProtectionMode);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改气体保护方式信息 
        /// </summary>
        /// <param name="gasProtectionMode"></param>
        public static void UpdateGasProtectionMode(Model.Base_GasProtectionMode gasProtectionMode)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_GasProtectionMode newGasProtectionMode = db.Base_GasProtectionMode.FirstOrDefault(e => e.GasProtectionModeId == gasProtectionMode.GasProtectionModeId);
            if (newGasProtectionMode != null)
            {
                newGasProtectionMode.GasProtectionModeName = gasProtectionMode.GasProtectionModeName;
                newGasProtectionMode.Remark = gasProtectionMode.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据气体保护方式Id删除一个气体保护方式信息
        /// </summary>
        /// <param name="gasProtectionModeId"></param>
        public static void DeleteGasProtectionModeByGasProtectionModeId(string gasProtectionModeId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_GasProtectionMode delGasProtectionMode = db.Base_GasProtectionMode.FirstOrDefault(e => e.GasProtectionModeId == gasProtectionModeId);
            if (delGasProtectionMode != null)
            {
                db.Base_GasProtectionMode.DeleteOnSubmit(delGasProtectionMode);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 按类型获取气体保护方式项
        /// </summary>
        /// <param name="gasProtectionModeType"></param>
        /// <returns></returns>
        public static List<Model.Base_GasProtectionMode> GetGasProtectionModeListByGasProtectionModeType()
        {
            var list = (from x in Funs.DB.Base_GasProtectionMode
                        orderby x.GasProtectionModeName
                        select x).ToList();
            return list;
        }

        #region 气体保护方式下拉项
        /// <summary>
        /// 气体保护方式下拉项
        /// </summary>
        /// <param name="dropName">下拉框名称</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        /// <param name="gasProtectionModeType">耗材类型</param>
        public static void InitGasProtectionModeDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease, string itemText)
        {
            dropName.DataValueField = "GasProtectionModeId";
            dropName.DataTextField = "GasProtectionModeName";
            dropName.DataSource = GetGasProtectionModeListByGasProtectionModeType();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName, itemText);
            }
        }
        #endregion
    }
}
