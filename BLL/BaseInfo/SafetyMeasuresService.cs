using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 安全措施
    /// </summary>
    public static class SafetyMeasuresService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取安全措施
        /// </summary>
        /// <param name="SafetyMeasuresId"></param>
        /// <returns></returns>
        public static Model.Base_SafetyMeasures GetSafetyMeasuresBySafetyMeasuresId(string SafetyMeasuresId)
        {
            return new Model.SGGLDB(Funs.ConnString).Base_SafetyMeasures.FirstOrDefault(e => e.SafetyMeasuresId == SafetyMeasuresId);
        }

        /// <summary>
        /// 获取安全措施名称是否存在
        /// </summary>
        /// <param name="SafetyMeasuresId">安全措施id</param>
        /// <param name="SafetyMeasures">名称</param>
        /// <returns>是否存在</returns>
        public static bool IsExistSafetyMeasures(string SafetyMeasuresId, string SafetyMeasures)
        {
            bool isExist = false;
            var role = new Model.SGGLDB(Funs.ConnString).Base_SafetyMeasures.FirstOrDefault(x => x.SafetyMeasures == SafetyMeasures && x.SafetyMeasuresId != SafetyMeasuresId);
            if (role != null)
            {
                isExist = true;
            }
            return isExist;
        }

        /// <summary>
        /// 添加安全措施
        /// </summary>
        /// <param name="SafetyMeasures"></param>
        public static void AddSafetyMeasures(Model.Base_SafetyMeasures SafetyMeasures)
        {
            Model.Base_SafetyMeasures newSafetyMeasures = new Model.Base_SafetyMeasures
            {
                SafetyMeasuresId = SafetyMeasures.SafetyMeasuresId,
                SortIndex = SafetyMeasures.SortIndex,
                SafetyMeasures = SafetyMeasures.SafetyMeasures,
                LicenseType = SafetyMeasures.LicenseType
            };
            db.Base_SafetyMeasures.InsertOnSubmit(newSafetyMeasures);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改安全措施
        /// </summary>
        /// <param name="SafetyMeasures"></param>
        public static void UpdateSafetyMeasures(Model.Base_SafetyMeasures SafetyMeasures)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_SafetyMeasures newSafetyMeasures = db.Base_SafetyMeasures.FirstOrDefault(e => e.SafetyMeasuresId == SafetyMeasures.SafetyMeasuresId);
            if (newSafetyMeasures != null)
            {
                newSafetyMeasures.SortIndex = SafetyMeasures.SortIndex;
                newSafetyMeasures.SafetyMeasures = SafetyMeasures.SafetyMeasures;
                newSafetyMeasures.LicenseType = SafetyMeasures.LicenseType;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除安全措施
        /// </summary>
        /// <param name="SafetyMeasuresId"></param>
        public static void DeleteSafetyMeasuresById(string SafetyMeasuresId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_SafetyMeasures SafetyMeasures = db.Base_SafetyMeasures.FirstOrDefault(e => e.SafetyMeasuresId == SafetyMeasuresId);
            if (SafetyMeasures != null)
            {
                db.Base_SafetyMeasures.DeleteOnSubmit(SafetyMeasures);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据部门Id获取安全措施下拉选择项
        /// </summary>
        /// <param name="departId"></param>
        /// <returns></returns>
        public static List<Model.Base_SafetyMeasures> GetSafetyMeasuresList(string licenseType)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Base_SafetyMeasures
                    where x.LicenseType == licenseType
                    orderby x.SortIndex
                    select x).ToList();
        }

        #region 安全措施表下拉框
        /// <summary>
        ///  安全措施表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitSafetyMeasuresDropDownList(FineUIPro.DropDownList dropName,string licenseType, bool isShowPlease)
        {
            dropName.DataValueField = "SafetyMeasuresId";
            dropName.DataTextField = "SafetyMeasures";
            dropName.DataSource = BLL.SafetyMeasuresService.GetSafetyMeasuresList(licenseType);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}
