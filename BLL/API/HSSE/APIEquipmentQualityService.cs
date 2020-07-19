using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;

namespace BLL
{
    public static class APIEquipmentQualityService
    {
        #region 根据equipmentQualityId获取机具设备信息
        /// <summary>
        /// 根据equipmentQualityId获取机具设备信息
        /// </summary>
        /// <param name="equipmentQualityId"></param>
        /// <returns></returns>
        public static Model.EquipmentQualityItem getEquipmentQualityByEquipmentQualityIdFactoryCode(string equipmentQualityId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getEquipmentQuality = (from x in db.View_QualityAudit_EquipmentQuality
                                           where x.EquipmentQualityId == equipmentQualityId || x.FactoryCode == equipmentQualityId
                                           select new Model.EquipmentQualityItem
                                           {
                                               EquipmentQualityId = x.EquipmentQualityId,
                                               ProjectId = x.ProjectId,
                                               EquipmentQualityCode = x.EquipmentQualityCode,
                                               UnitId = x.UnitId,
                                               UnitName = x.UnitName,
                                               SpecialEquipmentName = x.SpecialEquipmentName,
                                               EquipmentQualityName = x.EquipmentQualityName,
                                               FactoryCode = x.FactoryCode,
                                               CertificateCode = x.CertificateCode,
                                               CheckDate = string.Format("{0:yyyy-MM-dd}", x.CheckDate),
                                               LimitDate = string.Format("{0:yyyy-MM-dd}", x.LimitDate),
                                               InDate = string.Format("{0:yyyy-MM-dd}", x.InDate),
                                               OutDate = string.Format("{0:yyyy-MM-dd}", x.OutDate),
                                               ApprovalPerson = x.ApprovalPerson,
                                               CarNum = x.CarNum,
                                               SizeModel = x.SizeModel,
                                               Remark = x.Remark,
                                               CompileManId = x.CompileMan,
                                               CompileManName = x.CompileManName,
                                               CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                                               AttachUrl = x.AttachUrl.Replace('\\', '/')
                                           });
                return getEquipmentQuality.FirstOrDefault();
            }
        }
        #endregion        

        #region 获取机具设备列表信息
        /// <summary>
        /// 获取机具设备列表信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static List<Model.EquipmentQualityItem> getEquipmentQualityList(string projectId, string unitId, string strParam)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getEquipmentQuality = from x in db.View_QualityAudit_EquipmentQuality
                                          where x.ProjectId == projectId && (x.UnitId == unitId || unitId == null)
                                          && (strParam == null || x.FactoryCode.Contains(strParam) || x.SpecialEquipmentName.Contains(strParam) || x.EquipmentQualityName.Contains(strParam))
                                          orderby x.EquipmentQualityCode descending
                                          select new Model.EquipmentQualityItem
                                          {
                                              EquipmentQualityId = x.EquipmentQualityId,
                                              ProjectId = x.ProjectId,
                                              EquipmentQualityCode = x.EquipmentQualityCode,
                                              UnitId = x.UnitId,
                                              UnitName = x.UnitName,
                                              SpecialEquipmentName = x.SpecialEquipmentName,
                                              EquipmentQualityName = x.EquipmentQualityName,
                                              FactoryCode = x.FactoryCode,
                                              CertificateCode = x.CertificateCode,
                                              CheckDate = string.Format("{0:yyyy-MM-dd}", x.CheckDate),
                                              LimitDate = string.Format("{0:yyyy-MM-dd}", x.LimitDate),
                                              InDate = string.Format("{0:yyyy-MM-dd}", x.InDate),
                                              OutDate = string.Format("{0:yyyy-MM-dd}", x.OutDate),
                                              ApprovalPerson = x.ApprovalPerson,
                                              CarNum = x.CarNum,
                                              Remark = x.Remark,
                                              SizeModel = x.SizeModel,
                                              CompileManId = x.CompileMan,
                                              CompileManName = x.CompileManName,
                                              CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                                              AttachUrl = x.AttachUrl.Replace('\\', '/')
                                          };
                return getEquipmentQuality.ToList();
            }
        }
        #endregion        

        #region 保存QualityAudit_EquipmentQuality
        /// <summary>
        /// 保存QualityAudit_EquipmentQuality
        /// </summary>
        /// <param name="newItem">机具设备资质</param>
        /// <returns></returns>
        public static void SaveEquipmentQuality(Model.EquipmentQualityItem newItem)
        {
            Model.SGGLDB db = Funs.DB;
            Model.QualityAudit_EquipmentQuality newEquipmentQuality = new Model.QualityAudit_EquipmentQuality
            {
                EquipmentQualityId = newItem.EquipmentQualityId,
                ProjectId = newItem.ProjectId,
                EquipmentQualityCode = newItem.EquipmentQualityCode,
                UnitId = newItem.UnitId,
                SpecialEquipmentId = newItem.SpecialEquipmentId,
                EquipmentQualityName = newItem.EquipmentQualityName,
                SizeModel = newItem.SizeModel,
                FactoryCode = newItem.FactoryCode,
                CertificateCode = newItem.CertificateCode,
                CheckDate =Funs.GetNewDateTime( newItem.CheckDate),
                LimitDate = Funs.GetNewDateTimeOrNow(newItem.LimitDate),
                InDate = Funs.GetNewDateTime(newItem.InDate),
                OutDate = Funs.GetNewDateTime(newItem.OutDate),
                ApprovalPerson = newItem.ApprovalPerson,
                CarNum = newItem.CarNum,
                Remark = newItem.Remark,
                CompileMan = newItem.CompileManId,
            };
            
            var updateEquipmentQuality = db.QualityAudit_EquipmentQuality.FirstOrDefault(x => x.EquipmentQualityId == newItem.EquipmentQualityId);
            if (updateEquipmentQuality == null)
            {
                newEquipmentQuality.CompileDate = DateTime.Now;
                newEquipmentQuality.EquipmentQualityId = SQLHelper.GetNewID();
                newEquipmentQuality.EquipmentQualityCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.EquipmentQualityMenuId, newEquipmentQuality.ProjectId, newEquipmentQuality.UnitId);
                EquipmentQualityService.AddEquipmentQuality(newEquipmentQuality);
            }
            else
            {
                EquipmentQualityService.UpdateEquipmentQuality(newEquipmentQuality);
            }
            if (!string.IsNullOrEmpty(newItem.AttachUrl))
            {
                ////保存附件
                UploadFileService.SaveAttachUrl(UploadFileService.GetSourceByAttachUrl(newItem.AttachUrl, 10, null), newItem.AttachUrl, Const.EquipmentQualityMenuId, newEquipmentQuality.EquipmentQualityId);
            }
            else
            {
                CommonService.DeleteAttachFileById(newEquipmentQuality.EquipmentQualityId);
            }
        }
        #endregion
    }
}
