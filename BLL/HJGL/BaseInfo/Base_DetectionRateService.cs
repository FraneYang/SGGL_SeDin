namespace BLL
{
    using Model;
    using System.Collections.Generic;
    using System.Linq;

    public static class Base_DetectionRateService
    {
        /// <summary>
        ///获取探伤比例信息
        /// </summary>
        /// <returns></returns>
        public static Model.Base_DetectionRate GetDetectionRateByDetectionRateId(string detectionRateId)
        {
            return new Model.SGGLDB(Funs.ConnString).Base_DetectionRate.FirstOrDefault(e => e.DetectionRateId == detectionRateId);
        }

        /// <summary>
        /// 增加探伤比例信息
        /// </summary>
        /// <param name="detectionRate"></param>
        public static void AddDetectionRate(Model.Base_DetectionRate detectionRate)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_DetectionRate newDetectionRate = new Base_DetectionRate
            {
                DetectionRateId = detectionRate.DetectionRateId,
                DetectionRateCode = detectionRate.DetectionRateCode,
                DetectionRateValue = detectionRate.DetectionRateValue,
                Remark = detectionRate.Remark,
            };

            db.Base_DetectionRate.InsertOnSubmit(newDetectionRate);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改探伤比例信息 
        /// </summary>
        /// <param name="DetectionRate"></param>
        public static void UpdateDetectionRate(Model.Base_DetectionRate detectionRate)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_DetectionRate newDetectionRate = db.Base_DetectionRate.FirstOrDefault(e => e.DetectionRateId == detectionRate.DetectionRateId);
            if (newDetectionRate != null)
            {
                newDetectionRate.DetectionRateCode = detectionRate.DetectionRateCode;
                newDetectionRate.DetectionRateValue = detectionRate.DetectionRateValue;
                newDetectionRate.Remark = detectionRate.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据探伤比例Id删除一个探伤比例信息
        /// </summary>
        /// <param name="detectionRateId"></param>
        public static void DeleteDetectionRateByDetectionRateId(string detectionRateId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_DetectionRate delDetectionRate = db.Base_DetectionRate.FirstOrDefault(e => e.DetectionRateId == detectionRateId);
            if (delDetectionRate != null)
            {
                db.Base_DetectionRate.DeleteOnSubmit(delDetectionRate);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 按类型获取探伤比例项
        /// </summary>
        /// <param name="DetectionRateType"></param>
        /// <returns></returns>
        public static List<Model.Base_DetectionRate> GetDetectionRateList()
        {
            var list = (from x in new Model.SGGLDB(Funs.ConnString).Base_DetectionRate
                        orderby x.DetectionRateCode
                        select x).ToList();

            return list;
        }

        #region 探伤比例下拉项
        /// <summary>
        /// 探伤比例下拉项
        /// </summary>
        /// <param name="dropName">下拉框名称</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        /// <param name="DetectionRateType">耗材类型</param>
        public static void InitDetectionRateDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "DetectionRateId";
            dropName.DataTextField = "DetectionRateCode";
            dropName.DataSource = GetDetectionRateList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}
