namespace BLL
{
    using Model;
    using System.Collections.Generic;
    using System.Linq;

    public static class Base_DetectionTypeService
    {
        /// <summary>
        ///获取探伤类型信息
        /// </summary>
        /// <returns></returns>
        public static Model.Base_DetectionType GetDetectionTypeByDetectionTypeId(string detectionTypeId)
        {
            return Funs.DB.Base_DetectionType.FirstOrDefault(e => e.DetectionTypeId == detectionTypeId);
        }

        /// <summary>
        ///根据探伤类型获取探伤信息
        /// </summary>
        /// <returns></returns>
        public static Model.Base_DetectionType GetDetectionTypeIdByDetectionTypeCode(string detectionTypeCode)
        {
            return Funs.DB.Base_DetectionType.FirstOrDefault(e => e.DetectionTypeCode == detectionTypeCode);
        }

        /// <summary>
        /// 增加探伤类型信息
        /// </summary>
        /// <param name="detectionType"></param>
        public static void AddDetectionType(Model.Base_DetectionType detectionType)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_DetectionType newDetectionType = new Base_DetectionType
            {
                DetectionTypeId = detectionType.DetectionTypeId,
                DetectionTypeCode = detectionType.DetectionTypeCode,
                DetectionTypeName = detectionType.DetectionTypeName,
                SysType = detectionType.SysType,
                SecuritySpace = detectionType.SecuritySpace,
                InjuryDegree = detectionType.InjuryDegree,
                Remark = detectionType.Remark,
            };

            db.Base_DetectionType.InsertOnSubmit(newDetectionType);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改探伤类型信息 
        /// </summary>
        /// <param name="detectionType"></param>
        public static void UpdateDetectionType(Model.Base_DetectionType detectionType)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_DetectionType newDetectionType = db.Base_DetectionType.FirstOrDefault(e => e.DetectionTypeId == detectionType.DetectionTypeId);
            if (newDetectionType != null)
            {
                newDetectionType.DetectionTypeCode = detectionType.DetectionTypeCode;
                newDetectionType.DetectionTypeName = detectionType.DetectionTypeName;
                newDetectionType.SysType = detectionType.SysType;
                newDetectionType.SecuritySpace = detectionType.SecuritySpace;
                newDetectionType.InjuryDegree = detectionType.InjuryDegree;
                newDetectionType.Remark = detectionType.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据探伤类型Id删除一个探伤类型信息
        /// </summary>
        /// <param name="detectionTypeId"></param>
        public static void DeleteDetectionTypeByDetectionTypeId(string detectionTypeId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_DetectionType delDetectionType = db.Base_DetectionType.FirstOrDefault(e => e.DetectionTypeId == detectionTypeId);
            if (delDetectionType != null)
            {
                db.Base_DetectionType.DeleteOnSubmit(delDetectionType);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 按类型获取探伤类型项
        /// </summary>
        /// <param name="DetectionTypeType"></param>
        /// <returns></returns>
        public static List<Model.Base_DetectionType> GetDetectionTypeListByDetectionTypeType(string sysType)
        {
            var list = (from x in Funs.DB.Base_DetectionType
                        orderby x.DetectionTypeCode
                        select x).ToList();

            if (!string.IsNullOrEmpty(sysType))
            {
                list = list.Where(x => x.SysType == sysType).ToList();
            }

            return list;
        }

        #region 探伤类型下拉项
        /// <summary>
        /// 探伤类型下拉项
        /// </summary>
        /// <param name="dropName">下拉框名称</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        /// <param name="DetectionTypeType">耗材类型</param>
        public static void InitDetectionTypeDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease, string sysType)
        {
            dropName.DataValueField = "DetectionTypeId";
            dropName.DataTextField = "DetectionTypeCode";
            dropName.DataSource = GetDetectionTypeListByDetectionTypeType(sysType);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}
