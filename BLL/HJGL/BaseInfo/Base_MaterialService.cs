using Model;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public static class Base_MaterialService
    {
        /// <summary>
        ///获取材质定义信息
        /// </summary>
        /// <returns></returns>
        public static Model.Base_Material GetMaterialByMaterialId(string materialId)
        {
            return Funs.DB.Base_Material.FirstOrDefault(e => e.MaterialId == materialId);
        }

        /// <summary>
        /// 增加材质定义信息
        /// </summary>
        /// <param name="material"></param>
        public static void AddMaterial(Model.Base_Material material)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_Material newMaterial = new Base_Material
            {
                MaterialId = material.MaterialId,
                MaterialCode = material.MaterialCode,
                MaterialType = material.MaterialType,                
                SteelType = material.SteelType,              
                Remark = material.Remark,
                MaterialClass=material.MaterialClass,
                MaterialGroup=material.MaterialGroup,
                MetalType = material.MetalType,
        };
            db.Base_Material.InsertOnSubmit(newMaterial);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改材质定义信息 
        /// </summary>
        /// <param name="material"></param>
        public static void UpdateMaterial(Model.Base_Material material)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_Material newMaterial = db.Base_Material.FirstOrDefault(e => e.MaterialId == material.MaterialId);
            if (newMaterial != null)
            {
                newMaterial.MaterialCode = material.MaterialCode;
                newMaterial.MaterialType = material.MaterialType;               
                newMaterial.SteelType = material.SteelType;              
                newMaterial.Remark = material.Remark;
                newMaterial.MaterialClass=material.MaterialClass;
                newMaterial.MaterialGroup = material.MaterialGroup;
                newMaterial.MetalType = material.MetalType;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据材质定义Id删除一个材质定义信息
        /// </summary>
        /// <param name="materialId"></param>
        public static void DeleteMaterialByMaterialId(string materialId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_Material delMaterial = db.Base_Material.FirstOrDefault(e => e.MaterialId == materialId);
            if (delMaterial != null)
            {
                db.Base_Material.DeleteOnSubmit(delMaterial);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 按类型获取材质定义项
        /// </summary>
        /// <param name="MaterialType"></param>
        /// <returns></returns>
        public static List<Model.Base_Material> GetMaterialList()
        {
            var list = (from x in Funs.DB.Base_Material
                        orderby x.MaterialCode
                        select x).ToList();

            return list;
        }

        #region 材质定义下拉项
        /// <summary>
        /// 材质定义下拉项
        /// </summary>
        /// <param name="dropName">下拉框名称</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        /// <param name="MaterialType">耗材类型</param>
        public static void InitMaterialDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease,string itemText)
        {
            dropName.DataValueField = "MaterialId";
            dropName.DataTextField = "MaterialCode";
            dropName.DataSource = GetMaterialList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName,itemText);
            }
        }
        #endregion
    }
}
