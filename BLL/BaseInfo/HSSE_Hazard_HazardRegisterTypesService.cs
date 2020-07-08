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
            return new Model.SGGLDB(Funs.ConnString).HSSE_Hazard_HazardRegisterTypes.FirstOrDefault(e => e.RegisterTypesId == RegisterTypesId);
        }

        /// <summary>
        /// 添加巡检问题类型信息
        /// </summary>
        /// <param name="RegisterTypesName"></param>
        /// <param name="TypeCode"></param>
        public static void AddHazardRegisterTypes(Model.HSSE_Hazard_HazardRegisterTypes types)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.HSSE_Hazard_HazardRegisterTypes newTitle = new Model.HSSE_Hazard_HazardRegisterTypes();
            newTitle.RegisterTypesId = types.RegisterTypesId;
            newTitle.RegisterTypesName = types.RegisterTypesName;
            newTitle.TypeCode = types.TypeCode;
            newTitle.HazardRegisterType = types.HazardRegisterType;
            newTitle.GroupType = types.GroupType;
            newTitle.Remark = types.Remark;
            newTitle.IsPunished = types.IsPunished;

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
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.HSSE_Hazard_HazardRegisterTypes newTitle = db.HSSE_Hazard_HazardRegisterTypes.FirstOrDefault(e => e.RegisterTypesId == types.RegisterTypesId);
            if (newTitle != null)
            {
                newTitle.RegisterTypesName = types.RegisterTypesName;
                newTitle.TypeCode = types.TypeCode;
                newTitle.GroupType = types.GroupType;
                newTitle.Remark = types.Remark;
                newTitle.IsPunished = types.IsPunished;

                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 删除职务信息
        /// </summary>
        /// <param name="RegisterTypesId"></param>
        public static void DeleteTitle(string RegisterTypesId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
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
            return (from x in new Model.SGGLDB(Funs.ConnString).HSSE_Hazard_HazardRegisterTypes where x.HazardRegisterType == hazardRegisterType orderby x.TypeCode select x).ToList();
        }
    }
}
