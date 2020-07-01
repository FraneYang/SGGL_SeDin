using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Collections;

namespace BLL
{
    public class LawRegulationSelectedItemService
    {
        /// <summary>
        /// 根据法律法规辨识Id获取对应所有法律法规项信息
        /// </summary>
        /// <param name="lawRegulationIdentifyCode">法律法规辨识Id</param>
        /// <returns>所有法律法规项信息</returns>
        public static List<Model.Law_LawRegulationSelectedItem> GetLawRegulationSelectedItemsByLawRegulationIdentifyId(string lawRegulationIdentifyId)
        {
            return (from x in Funs.DB.Law_LawRegulationSelectedItem where x.LawRegulationIdentifyId == lawRegulationIdentifyId select x).ToList();
        }

        /// <summary>
        /// 增加法律法规项信息
        /// </summary>
        /// <param name="lawRegulationSelectedItem">法律法规项实体</param>
        public static void AddLawRegulationSelectedItem(Model.Law_LawRegulationSelectedItem lawRegulationSelectedItem)
        {
            Model.SGGLDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Law_LawRegulationSelectedItem));
            Model.Law_LawRegulationSelectedItem newLawRegulationSelectedItem = new Model.Law_LawRegulationSelectedItem
            {
                LawRegulationSelectedItemId = newKeyID,
                LawRegulationIdentifyId = lawRegulationSelectedItem.LawRegulationIdentifyId,
                LawRegulationId = lawRegulationSelectedItem.LawRegulationId,
                LawRegulationGrade = lawRegulationSelectedItem.LawRegulationGrade,
                FitPerfomance = lawRegulationSelectedItem.FitPerfomance
            };

            db.Law_LawRegulationSelectedItem.InsertOnSubmit(newLawRegulationSelectedItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据法律法规辨识主键删除对应的所有法律法规项信息
        /// </summary>
        /// <param name="lawRegulationIdentifyCode">法律法规辨识主键</param>
        public static void DeleteLawRegulationSelectedItemByLawRegulationIdentifyId(string lawRegulationIdentifyId)
        {
            Model.SGGLDB db = Funs.DB;
            var q = (from x in db.Law_LawRegulationSelectedItem where x.LawRegulationIdentifyId == lawRegulationIdentifyId select x).ToList();
            if (q.Count() > 0)
            {
                db.Law_LawRegulationSelectedItem.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
