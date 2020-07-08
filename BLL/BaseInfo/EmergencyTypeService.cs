using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class EmergencyTypeService
   {
       public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

       /// <summary>
       /// 根据主键获取信息
       /// </summary>
       /// <param name="groupId"></param>
       /// <returns></returns>
       public static Model.Base_EmergencyType GetEmergencyTypeById(string emergencyTypeId)
       {
           return new Model.SGGLDB(Funs.ConnString).Base_EmergencyType.FirstOrDefault(e => e.EmergencyTypeId == emergencyTypeId);
       }

       /// <summary>
       /// 添加
       /// </summary>
       /// <param name="?"></param>
       public static void AddEmergencyType(Model.Base_EmergencyType emergencyType)
       {
           Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_EmergencyType newEmergencyType = new Model.Base_EmergencyType
            {
                EmergencyTypeId = emergencyType.EmergencyTypeId,
                EmergencyTypeCode = emergencyType.EmergencyTypeCode,
                EmergencyTypeName = emergencyType.EmergencyTypeName,
                Remark = emergencyType.Remark
            };

            db.Base_EmergencyType.InsertOnSubmit(newEmergencyType);
           db.SubmitChanges();
       }

       /// <summary>
       /// 修改
       /// </summary>
       /// <param name="teamGroup"></param>
       public static void UpdateEmergencyType(Model.Base_EmergencyType emergencyType)
       {
           Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
           Model.Base_EmergencyType newEmergencyType = db.Base_EmergencyType.FirstOrDefault(e => e.EmergencyTypeId == emergencyType.EmergencyTypeId);
           if (newEmergencyType != null)
           {
               newEmergencyType.EmergencyTypeCode = emergencyType.EmergencyTypeCode;
               newEmergencyType.EmergencyTypeName = emergencyType.EmergencyTypeName;
               newEmergencyType.Remark = emergencyType.Remark;
               db.SubmitChanges();
           }
       }

       /// <summary>
       /// 根据主键删除信息
       /// </summary>
       /// <param name="emergencyTypeId"></param>
       public static void DeleteEmergencyTypeById(string emergencyTypeId)
       {
           Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
           Model.Base_EmergencyType emergencyType = db.Base_EmergencyType.FirstOrDefault(e => e.EmergencyTypeId == emergencyTypeId);
           {
               db.Base_EmergencyType.DeleteOnSubmit(emergencyType);
               db.SubmitChanges();
           }
       }

       /// <summary>
       /// 获取类别下拉项
       /// </summary>
       /// <returns></returns>
       public static List<Model.Base_EmergencyType> GetEmergencyTypeList()
       {
           var list = (from x in new Model.SGGLDB(Funs.ConnString).Base_EmergencyType
                       where x.EmergencyTypeName != null
                       orderby x.EmergencyTypeCode select x).ToList();
           return list;
       }
       /// <summary>
       /// 根据主键获取信息
       /// </summary>
       /// <param name="groupId"></param>
       /// <returns></returns>
       public static Model.Base_EmergencyType GetEmergencyTypeByName(string Name)
       {
           return new Model.SGGLDB(Funs.ConnString).Base_EmergencyType.FirstOrDefault(e => e.EmergencyTypeName == Name);
       }

       #region 应急响应类型
       /// <summary>
       /// 应急响应类型下拉框
       /// </summary>
       /// <param name="dropName">下拉框名字</param>
       /// <param name="projectId">项目id</param>
       /// <param name="isShowPlease">是否显示请选择</param>
       public static void InitEmergencyTypeDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
       {
           dropName.DataValueField = "EmergencyTypeId";
           dropName.DataTextField = "EmergencyTypeName";
           dropName.DataSource = BLL.EmergencyTypeService.GetEmergencyTypeList();
           dropName.DataBind();
           if (isShowPlease)
           {
               Funs.FineUIPleaseSelect(dropName);
           }
       }
    #endregion
   }
}