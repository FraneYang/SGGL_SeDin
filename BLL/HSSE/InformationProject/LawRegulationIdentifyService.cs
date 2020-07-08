using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Collections;

namespace BLL
{
    public class LawRegulationIdentifyService
    {
        /// <summary>
        /// 根据法律法规辨识编号获取一个法律法规辨识信息
        /// </summary>
        /// <param name="lawRegulationIdentifyCode">法律法规辨识编号</param>
        /// <returns>一个法律法规辨识实体</returns>
        public static Model.Law_LawRegulationIdentify GetLawRegulationIdentifyByLawRegulationIdentifyId(string lawRegulationIdentifyId)
        {
            return new Model.SGGLDB(Funs.ConnString).Law_LawRegulationIdentify.FirstOrDefault(x => x.LawRegulationIdentifyId == lawRegulationIdentifyId);
        }

        /// <summary>
        /// 增加法律法规辨识信息
        /// </summary>
        /// <param name="lawRegulationIdentify">法律法规辨识实体</param>
        public static void AddLawRegulationIdentify(Model.Law_LawRegulationIdentify lawRegulationIdentify)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Law_LawRegulationIdentify newLawRegulationIdentify = new Model.Law_LawRegulationIdentify
            {
                LawRegulationIdentifyId = lawRegulationIdentify.LawRegulationIdentifyId,
                LawRegulationIdentifyCode = lawRegulationIdentify.LawRegulationIdentifyCode,
                VersionNumber = lawRegulationIdentify.VersionNumber,
                ProjectId = lawRegulationIdentify.ProjectId,
                IdentifyPerson = lawRegulationIdentify.IdentifyPerson,
                IdentifyDate = lawRegulationIdentify.IdentifyDate,
                ManagerContent = lawRegulationIdentify.ManagerContent,
                IdentifyConclude = lawRegulationIdentify.IdentifyConclude,
                States = lawRegulationIdentify.States,
                GroupMember = lawRegulationIdentify.GroupMember,
                Remark = lawRegulationIdentify.Remark,
                UpdateDate = lawRegulationIdentify.UpdateDate
            };
            db.Law_LawRegulationIdentify.InsertOnSubmit(newLawRegulationIdentify);
            db.SubmitChanges();

            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.LawRegulationIdentifyMenuId, lawRegulationIdentify.ProjectId, null, lawRegulationIdentify.LawRegulationIdentifyId, lawRegulationIdentify.IdentifyDate);
        }

        /// <summary>
        /// 修改法律法规辨识信息
        /// </summary>
        /// <param name="lawRegulationIdentify">法律法规辨识实体</param>
        public static void UpdateLawRegulationIdentify(Model.Law_LawRegulationIdentify lawRegulationIdentify)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Law_LawRegulationIdentify newLawRegulationIdentify = db.Law_LawRegulationIdentify.FirstOrDefault(e => e.LawRegulationIdentifyId == lawRegulationIdentify.LawRegulationIdentifyId);
            if (newLawRegulationIdentify != null)
            {
                newLawRegulationIdentify.VersionNumber = lawRegulationIdentify.VersionNumber;
                newLawRegulationIdentify.ProjectId = lawRegulationIdentify.ProjectId;
                newLawRegulationIdentify.IdentifyPerson = lawRegulationIdentify.IdentifyPerson;
                newLawRegulationIdentify.IdentifyDate = lawRegulationIdentify.IdentifyDate;
                newLawRegulationIdentify.ManagerContent = lawRegulationIdentify.ManagerContent;
                newLawRegulationIdentify.IdentifyConclude = lawRegulationIdentify.IdentifyConclude;
                newLawRegulationIdentify.States = lawRegulationIdentify.States;
                newLawRegulationIdentify.GroupMember = lawRegulationIdentify.GroupMember;
                newLawRegulationIdentify.Remark = lawRegulationIdentify.Remark;
                newLawRegulationIdentify.UpdateDate = lawRegulationIdentify.UpdateDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据法律法规辨识编号删除一个法律法规辨识信息
        /// </summary>
        /// <param name="lawRegulationIdentifyCode">法律法规辨识编号</param>
        public static void DeleteLawRegulationIdentify(string lawRegulationIdentifyId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Law_LawRegulationIdentify lawRegulationIdentify = db.Law_LawRegulationIdentify.FirstOrDefault(e => e.LawRegulationIdentifyId == lawRegulationIdentifyId);
            if (lawRegulationIdentify != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(lawRegulationIdentifyId);
                //CommonService.DeleteAttachFileById(lawRegulationIdentifyId);//删除附件
                ProjectDataFlowSetService.DeleteFlowSetByDataId(lawRegulationIdentifyId);//删除流程
                ////删除审核流程表
                BLL.CommonService.DeleteFlowOperateByID(lawRegulationIdentifyId);
                db.Law_LawRegulationIdentify.DeleteOnSubmit(lawRegulationIdentify);
                db.SubmitChanges();
            }
        }
    }
}
