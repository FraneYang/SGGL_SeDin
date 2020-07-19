using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// 事故类型
    /// </summary>
    public static class AccidentTypeService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取事故类型
        /// </summary>
        /// <param name="accidentTypeId"></param>
        /// <returns></returns>
        public static Model.Base_AccidentType GetAccidentTypeById(string accidentTypeId)
        {
            return Funs.DB.Base_AccidentType.FirstOrDefault(e => e.AccidentTypeId == accidentTypeId);
        }

        /// <summary>
        /// 添加事故类型
        /// </summary>
        /// <param name="accidentType"></param>
        public static void AddAccidentType(Model.Base_AccidentType accidentType)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_AccidentType newAccidentType = new Model.Base_AccidentType
            {
                AccidentTypeId = accidentType.AccidentTypeId,
                AccidentTypeCode = accidentType.AccidentTypeCode,
                AccidentTypeName = accidentType.AccidentTypeName,
                Remark = accidentType.Remark
            };
            db.Base_AccidentType.InsertOnSubmit(newAccidentType);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改事故类型
        /// </summary>
        /// <param name="accidentType"></param>
        public static void UpdateAccidentType(Model.Base_AccidentType accidentType)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_AccidentType newAccidentType = db.Base_AccidentType.FirstOrDefault(e => e.AccidentTypeId == accidentType.AccidentTypeId);
            if (newAccidentType != null)
            {
                newAccidentType.AccidentTypeCode = accidentType.AccidentTypeCode;
                newAccidentType.AccidentTypeName = accidentType.AccidentTypeName;
                newAccidentType.Remark = accidentType.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除事故类型
        /// </summary>
        /// <param name="accidentTypeId"></param>
        public static void DeleteAccidentTypeById(string accidentTypeId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_AccidentType accidentType = db.Base_AccidentType.FirstOrDefault(e => e.AccidentTypeId == accidentTypeId);
            if (accidentType != null)
            {
                db.Base_AccidentType.DeleteOnSubmit(accidentType);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取事故类型下拉列表项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_AccidentType> GetAccidentTypeList()
        {
            return (from x in Funs.DB.Base_AccidentType orderby x.AccidentTypeCode select x).ToList();
        }

        /// <summary>
        /// 应急响应类型下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="projectId">项目id</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitAccidentTypeDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "AccidentTypeId";
            dropName.DataTextField = "AccidentTypeName";
            dropName.DataSource = GetAccidentTypeList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
    }
}
