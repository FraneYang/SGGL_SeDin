using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ControlItemProjectService
    {
        /// <summary>
        /// 根据分部分项编号获取对应所有工作包内容
        /// </summary>
        /// <param name="WorkPackageId"></param>
        public static List<Model.WBS_ControlItemProject> GetItemsByWorkPackageCode(string workPackageCode, string projectId)
        {
            var q = (from x in new Model.SGGLDB(Funs.ConnString).WBS_ControlItemProject where x.WorkPackageCode == workPackageCode && x.ProjectId == projectId orderby x.ControlItemCode select x).ToList();
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
        public static Model.WBS_ControlItemProject GetControlItemProjectByCode(string controlItemCode, string projectId)
        {
            return new Model.SGGLDB(Funs.ConnString).WBS_ControlItemProject.FirstOrDefault(e => e.ControlItemCode == controlItemCode && e.ProjectId == projectId);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="ControlItemAndCycle"></param>
        public static void AddControlItemProject(Model.WBS_ControlItemProject controlItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.WBS_ControlItemProject newControlItem = new Model.WBS_ControlItemProject();

            newControlItem.ControlItemCode = controlItem.ControlItemCode;
            newControlItem.ProjectId = controlItem.ProjectId;
            newControlItem.WorkPackageCode = controlItem.WorkPackageCode;
            newControlItem.ControlItemContent = controlItem.ControlItemContent;
            newControlItem.ControlPoint = controlItem.ControlPoint;
            newControlItem.ControlItemDef = controlItem.ControlItemDef;
            newControlItem.Weights = controlItem.Weights;
            newControlItem.HGForms = controlItem.HGForms;
            newControlItem.SHForms = controlItem.SHForms;
            newControlItem.Standard = controlItem.Standard;
            newControlItem.ClauseNo = controlItem.ClauseNo;
            newControlItem.CheckNum = controlItem.CheckNum;

            db.WBS_ControlItemProject.InsertOnSubmit(newControlItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="controlItem"></param>
        public static void UpdateControlItemProject(Model.WBS_ControlItemProject controlItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.WBS_ControlItemProject newControlItem = db.WBS_ControlItemProject.First(e => e.ControlItemCode == controlItem.ControlItemCode);

            newControlItem.WorkPackageCode = controlItem.WorkPackageCode;
            newControlItem.ControlItemContent = controlItem.ControlItemContent;
            newControlItem.ControlPoint = controlItem.ControlPoint;
            newControlItem.ControlItemDef = controlItem.ControlItemDef;
            newControlItem.Weights = controlItem.Weights;
            newControlItem.HGForms = controlItem.HGForms;
            newControlItem.SHForms = controlItem.SHForms;
            newControlItem.Standard = controlItem.Standard;
            newControlItem.ClauseNo = controlItem.ClauseNo;
            newControlItem.CheckNum = controlItem.CheckNum;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据工作包编号删除所有明细信息
        /// </summary>
        /// <param name="workPackageCode"></param>
        public static void DeleteAllControlItemProject(string workPackageCode, string projectId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            List<Model.WBS_ControlItemProject> q = (from x in db.WBS_ControlItemProject where x.WorkPackageCode == workPackageCode && x.ProjectId == projectId orderby x.ControlItemCode select x).ToList();
            db.WBS_ControlItemProject.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据编号删除信息
        /// </summary>
        /// <param name="controlItemCode"></param>
        public static void DeleteControlItemProject(string controlItemCode, string projectId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.WBS_ControlItemProject controlItem = db.WBS_ControlItemProject.First(e => e.ControlItemCode == controlItemCode && e.ProjectId == projectId);
            db.WBS_ControlItemProject.DeleteOnSubmit(controlItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 是否存在工作包
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-存在，false-不存在</returns>
        public static bool IsExistControlItemProjectName(string workPackageCode, string controlItemContent, string controlItemCode, string projectId)
        {
            var q = from x in new Model.SGGLDB(Funs.ConnString).WBS_ControlItemProject where x.WorkPackageCode == workPackageCode && x.ControlItemContent == controlItemContent && x.ControlItemCode != controlItemCode && x.ProjectId == projectId select x;
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
