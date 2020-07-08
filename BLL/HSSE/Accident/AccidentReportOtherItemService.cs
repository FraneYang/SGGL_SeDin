using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 事故调查处理报告调查组成员
    /// </summary>
    public class AccidentReportOtherItemService
    {
        Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据事故调查处理报告主键获取所有相关调查组成员信息列表
        /// </summary>
        /// <param name="accidentReportOtherId"></param>
        /// <returns></returns>
        public static List<Model.Accident_AccidentReportOtherItem> GetAccidentReportOtherItemByAccidentReportOtherId(string accidentReportOtherId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReportOtherItem where x.AccidentReportOtherId == accidentReportOtherId select x).ToList();
        }

        /// <summary>
        /// 根据主键获取事故调查处理报告调查组成员
        /// </summary>
        /// <param name="accidentReportOtherItemId"></param>
        /// <returns></returns>
        public static Model.Accident_AccidentReportOtherItem GetAccidentReportOtherItemById(string accidentReportOtherItemId)
        {
            return new Model.SGGLDB(Funs.ConnString).Accident_AccidentReportOtherItem.FirstOrDefault(e => e.AccidentReportOtherItemId == accidentReportOtherItemId);
        }

        /// <summary>
        /// 添加调查组人员
        /// </summary>
        /// <param name="accidentReportOtherItem"></param>
        public static void AddAccidentReportOtherItem(Model.Accident_AccidentReportOtherItem accidentReportOtherItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Accident_AccidentReportOtherItem newAccidentReportOtherItem = new Model.Accident_AccidentReportOtherItem
            {
                AccidentReportOtherItemId = accidentReportOtherItem.AccidentReportOtherItemId,
                AccidentReportOtherId = accidentReportOtherItem.AccidentReportOtherId,
                UnitId = accidentReportOtherItem.UnitId,
                PersonId = accidentReportOtherItem.PersonId,
                PositionId = accidentReportOtherItem.PositionId
            };
            db.Accident_AccidentReportOtherItem.InsertOnSubmit(newAccidentReportOtherItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改调查组人员
        /// </summary>
        /// <param name="accidentReportOtherItem"></param>
        public static void UpdateAccidentReportOtherItem(Model.Accident_AccidentReportOtherItem accidentReportOtherItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Accident_AccidentReportOtherItem newAccidentReportOtherItem = db.Accident_AccidentReportOtherItem.FirstOrDefault(e => e.AccidentReportOtherItemId == accidentReportOtherItem.AccidentReportOtherItemId);
            if (newAccidentReportOtherItem != null)
            {
                newAccidentReportOtherItem.AccidentReportOtherId = accidentReportOtherItem.AccidentReportOtherId;
                newAccidentReportOtherItem.UnitId = accidentReportOtherItem.UnitId;
                newAccidentReportOtherItem.PersonId = accidentReportOtherItem.PersonId;
                newAccidentReportOtherItem.PositionId = accidentReportOtherItem.PositionId;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据事故调查报告主键删除所有相关调查组人员信息
        /// </summary>
        /// <param name="accidentReportOtherId"></param>
        public static void DeleteAccidentReportOtherItemByAccidentReportOtherId(string accidentReportOtherId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var q = (from x in db.Accident_AccidentReportOtherItem where x.AccidentReportOtherId == accidentReportOtherId select x).ToList();
            if (q != null)
            {
                db.Accident_AccidentReportOtherItem.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除调查组人员
        /// </summary>
        /// <param name="accidentReportOtherItemId"></param>
        public static void DeleteAccidentReportOtherItemById(string accidentReportOtherItemId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Accident_AccidentReportOtherItem item = db.Accident_AccidentReportOtherItem.FirstOrDefault(e => e.AccidentReportOtherItemId == accidentReportOtherItemId);
            if (item != null)
            {
                db.Accident_AccidentReportOtherItem.DeleteOnSubmit(item);
                db.SubmitChanges();
            }
        }
    }
}
