using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class ConstructionStandardIdentifyService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据标准规范辨识主键获取一个标准规范辨识信息
        /// </summary>
        /// <param name="constructionStandardIdentifyId">标准规范辨识主键</param>
        /// <returns>一个标准规范辨识实体</returns>
        public static Model.InformationProject_ConstructionStandardIdentify GetConstructionStandardIdentifyById(string constructionStandardIdentifyId)
        {
            return new Model.SGGLDB(Funs.ConnString).InformationProject_ConstructionStandardIdentify.FirstOrDefault(x => x.ConstructionStandardIdentifyId == constructionStandardIdentifyId);
        }

        /// <summary>
        /// 查询还未生成版本号的标准规范量
        /// </summary>
        /// <returns>还未生成版本号的标准规范的数量</returns>
        public static int GetConstructionStandardIdentifyByVersionIsNull(string projectId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).InformationProject_ConstructionStandardIdentify where x.ProjectId == projectId && x.VersionNumber == null select x).Count();
        }

        /// <summary>
        /// 增加标准规范辨识信息
        /// </summary>
        /// <param name="lawRegulationIdentify">标准规范辨识实体</param>
        public static void AddConstructionStandardIdentify(Model.InformationProject_ConstructionStandardIdentify constructionStandardIdentify)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InformationProject_ConstructionStandardIdentify newConstructionStandardIdentify = new Model.InformationProject_ConstructionStandardIdentify
            {
                ConstructionStandardIdentifyId = constructionStandardIdentify.ConstructionStandardIdentifyId,
                ConstructionStandardIdentifyCode = constructionStandardIdentify.ConstructionStandardIdentifyCode,
                VersionNumber = constructionStandardIdentify.VersionNumber,
                ProjectId = constructionStandardIdentify.ProjectId,
                IdentifyPerson = constructionStandardIdentify.IdentifyPerson,
                IdentifyDate = constructionStandardIdentify.IdentifyDate,
                State = constructionStandardIdentify.State,
                Remark = constructionStandardIdentify.Remark,
                UpdateDate = constructionStandardIdentify.UpdateDate
            };
            db.InformationProject_ConstructionStandardIdentify.InsertOnSubmit(newConstructionStandardIdentify);
            db.SubmitChanges();
            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ConstructionStandardIdentifyMenuId, constructionStandardIdentify.ProjectId, null, constructionStandardIdentify.ConstructionStandardIdentifyId, constructionStandardIdentify.IdentifyDate);
        }

        /// <summary>
        /// 修改标准规范辨识信息
        /// </summary>
        /// <param name="lawRegulationIdentify">标准规范辨识实体</param>
        public static void UpdateConstructionStandardIdentify(Model.InformationProject_ConstructionStandardIdentify constructionStandardIdentify)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InformationProject_ConstructionStandardIdentify newConstructionStandardIdentify = db.InformationProject_ConstructionStandardIdentify.FirstOrDefault(e => e.ConstructionStandardIdentifyId == constructionStandardIdentify.ConstructionStandardIdentifyId);
            if (newConstructionStandardIdentify != null)
            {
                newConstructionStandardIdentify.ConstructionStandardIdentifyCode = constructionStandardIdentify.ConstructionStandardIdentifyCode;
                newConstructionStandardIdentify.VersionNumber = constructionStandardIdentify.VersionNumber;
                newConstructionStandardIdentify.ProjectId = constructionStandardIdentify.ProjectId;
                newConstructionStandardIdentify.IdentifyPerson = constructionStandardIdentify.IdentifyPerson;
                newConstructionStandardIdentify.IdentifyDate = constructionStandardIdentify.IdentifyDate;
                newConstructionStandardIdentify.State = constructionStandardIdentify.State;
                newConstructionStandardIdentify.Remark = constructionStandardIdentify.Remark;
                newConstructionStandardIdentify.UpdateDate = constructionStandardIdentify.UpdateDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据标准规范辨识编号删除一个标准规范辨识信息
        /// </summary>
        /// <param name="constructionStandardIdentifyId">标准规范辨识主键</param>
        public static void DeleteConstructionStandardIdentifyById(string constructionStandardIdentifyId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InformationProject_ConstructionStandardIdentify constructionStandardIdentify = db.InformationProject_ConstructionStandardIdentify.FirstOrDefault(e => e.ConstructionStandardIdentifyId == constructionStandardIdentifyId);
            if (constructionStandardIdentify != null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(constructionStandardIdentifyId);//删除编号
                //CommonService.DeleteAttachFileById(constructionStandardIdentifyId);//删除附件
                CommonService.DeleteFlowOperateByID(constructionStandardIdentifyId);//删除流程
                db.InformationProject_ConstructionStandardIdentify.DeleteOnSubmit(constructionStandardIdentify);
                db.SubmitChanges();
            }
        }
    }
}
