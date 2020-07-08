using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public class ControlItemInitService
    {
        /// <summary>
        /// 根据对应专业下是否有工作包内容
        /// </summary>
        /// <param name="WorkPackageId"></param>
        public static List<Model.WBS_ControlItemInit> GetItemsByWorkPackageCode(string workPackageCode)
        {
            var q = (from x in new Model.SGGLDB(Funs.ConnString).WBS_ControlItemInit where x.WorkPackageCode == workPackageCode orderby x.ControlItemCode select x).ToList();
            if (q.Count > 0)
            {
                return q;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据编号获取明细
        /// </summary>
        /// <param name="controlItemCode"></param>
        public static Model.WBS_ControlItemInit GetControlItemInitByCode(string controlItemCode)
        {
            return new Model.SGGLDB(Funs.ConnString).WBS_ControlItemInit.FirstOrDefault(e => e.ControlItemCode == controlItemCode);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="ControlItemAndCycle"></param>
        public static void AddControlItemInit(Model.WBS_ControlItemInit controlItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.WBS_ControlItemInit newControlItem = new Model.WBS_ControlItemInit();

            newControlItem.ControlItemCode = controlItem.ControlItemCode;
            newControlItem.WorkPackageCode = controlItem.WorkPackageCode;
            newControlItem.ControlItemContent = controlItem.ControlItemContent;
            newControlItem.ControlPoint = controlItem.ControlPoint;
            newControlItem.ControlItemDef = controlItem.ControlItemDef;
            newControlItem.Weights = controlItem.Weights;
            newControlItem.HGForms = controlItem.HGForms;
            newControlItem.SHForms = controlItem.SHForms;
            newControlItem.Standard = controlItem.Standard;
            newControlItem.ClauseNo = controlItem.ClauseNo;

            db.WBS_ControlItemInit.InsertOnSubmit(newControlItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="controlItem"></param>
        public static void UpdateControlItemInit(Model.WBS_ControlItemInit controlItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.WBS_ControlItemInit newControlItem = db.WBS_ControlItemInit.First(e => e.ControlItemCode == controlItem.ControlItemCode);

            newControlItem.WorkPackageCode = controlItem.WorkPackageCode;
            newControlItem.ControlItemContent = controlItem.ControlItemContent;
            newControlItem.ControlPoint = controlItem.ControlPoint;
            newControlItem.ControlItemDef = controlItem.ControlItemDef;
            newControlItem.Weights = controlItem.Weights;
            newControlItem.HGForms = controlItem.HGForms;
            newControlItem.SHForms = controlItem.SHForms;
            newControlItem.Standard = controlItem.Standard;
            newControlItem.ClauseNo = controlItem.ClauseNo;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据编号删除信息
        /// </summary>
        /// <param name="controlItemCode"></param>
        public static void DeleteControlItemInit(string controlItemCode)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.WBS_ControlItemInit controlItem = db.WBS_ControlItemInit.First(e => e.ControlItemCode == controlItemCode);
            db.WBS_ControlItemInit.DeleteOnSubmit(controlItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据工作包编号删除所有明细信息
        /// </summary>
        /// <param name="workPackageCode"></param>
        public static void DeleteAllControlItemInit(string workPackageCode)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            List<Model.WBS_ControlItemInit> q = (from x in db.WBS_ControlItemInit where x.WorkPackageCode == workPackageCode orderby x.ControlItemCode select x).ToList();
            db.WBS_ControlItemInit.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }

        /// <summary>
        /// 是否存在工作包
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-存在，false-不存在</returns>
        public static bool IsExistControlItemInitName(string workPackageCode, string controlItemContent, string controlItemCode)
        {
            var q = from x in new Model.SGGLDB(Funs.ConnString).WBS_ControlItemInit where x.WorkPackageCode == workPackageCode && x.ControlItemContent == controlItemContent && x.ControlItemCode != controlItemCode select x;
            if (q.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
