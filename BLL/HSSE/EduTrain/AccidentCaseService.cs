using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class AccidentCaseService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取事故伤害及预防
        /// </summary>
        /// <param name="accidentCaseId"></param>
        /// <returns></returns>
        public static Model.EduTrain_AccidentCase GetAccidentCaseById(string accidentCaseId)
        {
            return new Model.SGGLDB(Funs.ConnString).EduTrain_AccidentCase.FirstOrDefault(e => e.AccidentCaseId == accidentCaseId);
        }

        /// <summary>
        /// 增加事故伤害及预防
        /// </summary>
        /// <param name="accidentCase"></param>
        public static void AddAccidentCase(Model.EduTrain_AccidentCase accidentCase)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.EduTrain_AccidentCase newAccidentCase = new Model.EduTrain_AccidentCase
            {
                AccidentCaseId = accidentCase.AccidentCaseId,
                AccidentCaseCode = accidentCase.AccidentCaseCode,
                AccidentCaseName = accidentCase.AccidentCaseName,
                SupAccidentCaseId = accidentCase.SupAccidentCaseId,
                IsEndLever = accidentCase.IsEndLever
            };
            db.EduTrain_AccidentCase.InsertOnSubmit(newAccidentCase);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改事故伤害及预防
        /// </summary>
        /// <param name="accidentCase"></param>
        public static void UpdateAccidentCase(Model.EduTrain_AccidentCase accidentCase)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.EduTrain_AccidentCase newAccidentCase = db.EduTrain_AccidentCase.FirstOrDefault(e => e.AccidentCaseId == accidentCase.AccidentCaseId);
            if (newAccidentCase != null)
            {
                newAccidentCase.AccidentCaseCode = accidentCase.AccidentCaseCode;
                newAccidentCase.AccidentCaseName = accidentCase.AccidentCaseName;
                newAccidentCase.SupAccidentCaseId = accidentCase.SupAccidentCaseId;
                newAccidentCase.IsEndLever = accidentCase.IsEndLever;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除事故伤害及预防
        /// </summary>
        /// <param name="accidentCaseId"></param>
        public static void DeleteAccidentCaseByAccidentCaseId(string accidentCaseId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.EduTrain_AccidentCase accidentCase = db.EduTrain_AccidentCase.FirstOrDefault(e => e.AccidentCaseId == accidentCaseId);
            if (accidentCase != null)
            {
                BLL.AccidentCaseItemService.DeleteAccidentCaseItemsByAccidentCaseId(accidentCase.AccidentCaseId);
                db.EduTrain_AccidentCase.DeleteOnSubmit(accidentCase);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 是否可删除资源节点
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-可以，false-不可以</returns>
        public static bool IsDeleteAccidentCase(string accidentCaseId)
        {
            bool isDelete = true;
            var accidentCase = BLL.AccidentCaseService.GetAccidentCaseById(accidentCaseId);
            if (accidentCase != null)
            {
                if (accidentCase.IsBuild == true)
                {
                    isDelete = false;
                }
                if (accidentCase.IsEndLever == true)
                {
                    var detailCout = new Model.SGGLDB(Funs.ConnString).EduTrain_AccidentCaseItem.FirstOrDefault(x => x.AccidentCaseId == accidentCaseId);
                    if (detailCout != null)
                    {
                        isDelete = false;
                    }
                }
                else
                {
                    var supItemSetCount = BLL.AccidentCaseService.GetAccidentCaseBySupItem(accidentCaseId);
                    if (supItemSetCount.Count() > 0)
                    {
                        isDelete = false;
                    }
                }
            }

            return isDelete;
        }

        /// <summary>
        /// 根据上级Id查询所有事故案例
        /// </summary>
        /// <param name="supItem">上级Id</param>
        /// <returns>事故案例的集合</returns>
        public static List<Model.EduTrain_AccidentCase> GetAccidentCaseBySupItem(string supItem)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).EduTrain_AccidentCase
                    where x.SupAccidentCaseId == supItem
                    orderby x.AccidentCaseCode
                    select x).ToList();
        }
    }
}
