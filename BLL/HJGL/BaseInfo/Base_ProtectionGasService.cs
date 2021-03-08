using Model;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public static class Base_ProtectionGasService
    {
        /// <summary>
        ///获取保护气体信息
        /// </summary>
        /// <returns></returns>
        public static Model.Base_ProtectionGas GetProtectionGasByProtectionGasId(string protectionGasId)
        {
            return Funs.DB.Base_ProtectionGas.FirstOrDefault(e => e.ProtectionGasId == protectionGasId);
        }

        /// <summary>
        /// 增加保护气体信息
        /// </summary>
        /// <param name="protectionGas"></param>
        public static void AddProtectionGas(Model.Base_ProtectionGas protectionGas)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_ProtectionGas newProtectionGas = new Base_ProtectionGas
            {
                ProtectionGasId = protectionGas.ProtectionGasId,
                ProtectionGasCode = protectionGas.ProtectionGasCode,
                ProtectionGasName = protectionGas.ProtectionGasName,
                Remark = protectionGas.Remark,
            };

            db.Base_ProtectionGas.InsertOnSubmit(newProtectionGas);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改保护气体信息 
        /// </summary>
        /// <param name="protectionGas"></param>
        public static void UpdateProtectionGas(Model.Base_ProtectionGas protectionGas)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_ProtectionGas newProtectionGas = db.Base_ProtectionGas.FirstOrDefault(e => e.ProtectionGasId == protectionGas.ProtectionGasId);
            if (newProtectionGas != null)
            {
                newProtectionGas.ProtectionGasCode = protectionGas.ProtectionGasCode;
                newProtectionGas.ProtectionGasName = protectionGas.ProtectionGasName;
                newProtectionGas.Remark = protectionGas.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据保护气体Id删除一个保护气体信息
        /// </summary>
        /// <param name="protectionGasId"></param>
        public static void DeleteProtectionGasByProtectionGasId(string protectionGasId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_ProtectionGas delProtectionGas = db.Base_ProtectionGas.FirstOrDefault(e => e.ProtectionGasId == protectionGasId);
            if (delProtectionGas != null)
            {
                db.Base_ProtectionGas.DeleteOnSubmit(delProtectionGas);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 按类型获取保护气体项
        /// </summary>
        /// <param name="protectionGasType"></param>
        /// <returns></returns>
        public static List<Model.Base_ProtectionGas> GetProtectionGasListByProtectionGasType()
        {
            var list = (from x in Funs.DB.Base_ProtectionGas
                        orderby x.ProtectionGasName
                        select x).ToList();
            return list;
        }

        #region 保护气体下拉项
        /// <summary>
        /// 保护气体下拉项
        /// </summary>
        /// <param name="dropName">下拉框名称</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        /// <param name="protectionGasType">耗材类型</param>
        public static void InitProtectionGasDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease, string itemText)
        {
            dropName.DataValueField = "ProtectionGasId";
            dropName.DataTextField = "ProtectionGasName";
            dropName.DataSource = GetProtectionGasListByProtectionGasType();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName, itemText);
            }
        }
        #endregion
    }
}
