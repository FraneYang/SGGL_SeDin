using FineUIPro;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// 机具设备
    /// </summary>
    public static class SpecialEquipmentService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取机具设备
        /// </summary>
        /// <param name="specialEquipmentId"></param>
        /// <returns></returns>
        public static Model.Base_SpecialEquipment GetSpecialEquipmentById(string specialEquipmentId)
        {
            return Funs.DB.Base_SpecialEquipment.FirstOrDefault(e => e.SpecialEquipmentId == specialEquipmentId);
        }

        /// <summary>
        /// 添加机具设备
        /// </summary>
        /// <param name="specialEquipment"></param>
        public static void AddSpecialEquipment(Model.Base_SpecialEquipment specialEquipment)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_SpecialEquipment newSpecialEquipment = new Model.Base_SpecialEquipment
            {
                SpecialEquipmentId = specialEquipment.SpecialEquipmentId,
                SpecialEquipmentCode = specialEquipment.SpecialEquipmentCode,
                SpecialEquipmentName = specialEquipment.SpecialEquipmentName,
                Remark = specialEquipment.Remark,
                IsSpecial = specialEquipment.IsSpecial,
                SpecialEquipmentType = specialEquipment.SpecialEquipmentType,
            };
            db.Base_SpecialEquipment.InsertOnSubmit(newSpecialEquipment);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改机具设备
        /// </summary>
        /// <param name="specialEquipment"></param>
        public static void UpdateSpecialEquipment(Model.Base_SpecialEquipment specialEquipment)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_SpecialEquipment newSpecialEquipment = db.Base_SpecialEquipment.FirstOrDefault(e => e.SpecialEquipmentId == specialEquipment.SpecialEquipmentId);
            if (newSpecialEquipment != null)
            {
                newSpecialEquipment.SpecialEquipmentCode = specialEquipment.SpecialEquipmentCode;
                newSpecialEquipment.SpecialEquipmentName = specialEquipment.SpecialEquipmentName;
                newSpecialEquipment.Remark = specialEquipment.Remark;
                newSpecialEquipment.IsSpecial = specialEquipment.IsSpecial;
                newSpecialEquipment.SpecialEquipmentType = specialEquipment.SpecialEquipmentType;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除机具设备
        /// </summary>
        /// <param name="specialEquipmentId"></param>
        public static void DeleteSpecialEquipmentById(string specialEquipmentId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_SpecialEquipment specialEquipment = db.Base_SpecialEquipment.FirstOrDefault(e => e.SpecialEquipmentId == specialEquipmentId);
            if (specialEquipment != null)
            {
                db.Base_SpecialEquipment.DeleteOnSubmit(specialEquipment);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取机具设备列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_SpecialEquipment> GetSpecialEquipmentList(bool isSpecial)
        {
            return (from x in Funs.DB.Base_SpecialEquipment where x.IsSpecial == isSpecial
                    orderby x.SpecialEquipmentCode select x).ToList();
        }

        /// <summary>
        /// 设备下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isSpecial">项目id</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitSpecialEquipmentDropDownList(DropDownList dropName, bool isSpecial, bool isShowPlease)
        {
            dropName.DataValueField = "SpecialEquipmentId";
            dropName.DataTextField = "SpecialEquipmentName";
            dropName.DataSource = GetSpecialEquipmentList(isSpecial);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        /// 获取机具设备列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_SpecialEquipment> GetSpecialEquipmentListByType(string type)
        {
            return (from x in Funs.DB.Base_SpecialEquipment
                    where x.SpecialEquipmentType == type
                    orderby x.SpecialEquipmentCode
                    select x).ToList();
        }

        /// <summary>
        ///  设备类型下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitSpecialEquipmentTypeDropDownList(DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "Value";
            dropName.DataTextField = "Text";
            dropName.DataSource = getEquipmentTypeItem();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        /// 查询下拉列表值
        /// </summary>
        /// <returns></returns>
        public static ListItem[] getEquipmentTypeItem()
        {
            ListItem[] list = new ListItem[4];
            list[0] = new ListItem("特种设备", "1");
            list[1] = new ListItem("大型机具设备", "2");
            list[2] = new ListItem("特殊机具设备","3");
            list[3] = new ListItem("其他", "4");
            return list;
        }

        public static string getTypeName(string type)
        {
            string name = "其他";
            var getEquipmentT = getEquipmentTypeItem().FirstOrDefault(x => x.Value == type);
            if (getEquipmentT != null)
            {
                name = getEquipmentT.Text;
            }
            return name;

        }
    }
}