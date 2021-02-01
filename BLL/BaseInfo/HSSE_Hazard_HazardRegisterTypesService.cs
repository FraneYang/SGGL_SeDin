using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public static class HSSE_Hazard_HazardRegisterTypesService
    {
        /// <summary>
        /// 获取某一巡检问题类型信息
        /// </summary>
        /// <param name="RegisterTypesId"></param>
        /// <returns></returns>
        public static Model.HSSE_Hazard_HazardRegisterTypes GetTitleByRegisterTypesId(string RegisterTypesId)
        {
            return Funs.DB.HSSE_Hazard_HazardRegisterTypes.FirstOrDefault(e => e.RegisterTypesId == RegisterTypesId);
        }

        /// <summary>
        /// 添加巡检问题类型信息
        /// </summary>
        /// <param name="RegisterTypesName"></param>
        /// <param name="TypeCode"></param>
        public static void AddHazardRegisterTypes(Model.HSSE_Hazard_HazardRegisterTypes types)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HSSE_Hazard_HazardRegisterTypes newTitle = new Model.HSSE_Hazard_HazardRegisterTypes
            {
                RegisterTypesId = types.RegisterTypesId,
                RegisterTypesName = types.RegisterTypesName,
                TypeCode = types.TypeCode,
                HazardRegisterType = types.HazardRegisterType,
                GroupType = types.GroupType,
                Remark = types.Remark
            };
            //   newTitle.IsPunished = types.IsPunished;

            db.HSSE_Hazard_HazardRegisterTypes.InsertOnSubmit(newTitle);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改巡检问题类型信息
        /// </summary>
        /// <param name="RegisterTypesId"></param>
        /// <param name="RegisterTypesName"></param>
        /// <param name="TypeCode"></param>
        public static void UpdateHazardRegisterTypes(Model.HSSE_Hazard_HazardRegisterTypes types)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HSSE_Hazard_HazardRegisterTypes newTypes = db.HSSE_Hazard_HazardRegisterTypes.FirstOrDefault(e => e.RegisterTypesId == types.RegisterTypesId);
            if (newTypes != null)
            {
                newTypes.RegisterTypesName = types.RegisterTypesName;
                newTypes.TypeCode = types.TypeCode;
                newTypes.HazardRegisterType = types.HazardRegisterType;
                newTypes.GroupType = types.GroupType;
                newTypes.Remark = types.Remark;
                //newTitle.IsPunished = types.IsPunished;

                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 删除职务信息
        /// </summary>
        /// <param name="RegisterTypesId"></param>
        public static void DeleteRegisterTypes(string RegisterTypesId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HSSE_Hazard_HazardRegisterTypes types = db.HSSE_Hazard_HazardRegisterTypes.FirstOrDefault(e => e.RegisterTypesId == RegisterTypesId);
            if (types != null)
            {
                db.HSSE_Hazard_HazardRegisterTypes.DeleteOnSubmit(types);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取巡检问题类型项
        /// </summary>
        /// <returns></returns>
        public static List<Model.HSSE_Hazard_HazardRegisterTypes> GetHazardRegisterTypesList(string hazardRegisterType)
        {
            return (from x in Funs.DB.HSSE_Hazard_HazardRegisterTypes
                    where x.HazardRegisterType == hazardRegisterType
                    orderby x.TypeCode select x).ToList();
        }

        /// <summary>
        /// 应急响应类型下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="projectId">项目id</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitAccidentTypeDropDownList(FineUIPro.DropDownList dropName,string type, bool isShowPlease)
        {
            dropName.DataValueField = "RegisterTypesId";
            dropName.DataTextField = "RegisterTypesName";
            dropName.DataSource = GetHazardRegisterTypesList(type);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
    }
}
