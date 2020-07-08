using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 许可证类型
    /// </summary>
    public static class LicenseTypeService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取许可证类型
        /// </summary>
        /// <param name="licenseTypeId"></param>
        /// <returns></returns>
        public static Model.Base_LicenseType GetLicenseTypeById(string licenseTypeId)
        {
            return new Model.SGGLDB(Funs.ConnString).Base_LicenseType.FirstOrDefault(e => e.LicenseTypeId == licenseTypeId);
        }

        /// <summary>
        /// 添加许可证类型
        /// </summary>
        /// <param name="licenseType"></param>
        public static void AddLicenseType(Model.Base_LicenseType licenseType)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_LicenseType newLicenseType = new Model.Base_LicenseType
            {
                LicenseTypeId = licenseType.LicenseTypeId,
                LicenseTypeCode = licenseType.LicenseTypeCode,
                LicenseTypeName = licenseType.LicenseTypeName,
                Remark = licenseType.Remark,
                LicenseContents = licenseType.LicenseContents
            };
            db.Base_LicenseType.InsertOnSubmit(newLicenseType);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改许可证类型
        /// </summary>
        /// <param name="licenseType"></param>
        public static void UpdateLicenseType(Model.Base_LicenseType licenseType)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_LicenseType newLicenseType = db.Base_LicenseType.FirstOrDefault(e => e.LicenseTypeId == licenseType.LicenseTypeId);
            if (newLicenseType != null)
            {
                newLicenseType.LicenseTypeCode = licenseType.LicenseTypeCode;
                newLicenseType.LicenseTypeName = licenseType.LicenseTypeName;
                newLicenseType.Remark = licenseType.Remark;
                newLicenseType.LicenseContents = licenseType.LicenseContents;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除许可证类型
        /// </summary>
        /// <param name="licenseTypeId"></param>
        public static void DeleteLicenseTypeById(string licenseTypeId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_LicenseType licenseType = db.Base_LicenseType.FirstOrDefault(e => e.LicenseTypeId == licenseTypeId);
            if (licenseType != null)
            {
                db.Base_LicenseType.DeleteOnSubmit(licenseType);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取许可证类型下拉选择项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_LicenseType> GetLicenseTypeList()
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Base_LicenseType orderby x.LicenseTypeCode select x).ToList();
        }

        #region 许可证类型下拉框
        /// <summary>
        /// 许可证类型下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitLicenseTypeDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "LicenseTypeId";
            dropName.DataTextField = "LicenseTypeName";
            dropName.DataSource = GetLicenseTypeList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}
