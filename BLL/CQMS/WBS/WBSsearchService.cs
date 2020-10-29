using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public class WBSsearchService
    {
        /// <summary>
        /// 根据查询条件查询列表
        /// </summary>
        /// <param name="WorkPackageId"></param>
        public static List<Model.View_WBS_ControlItemAndCycle> getWBSlistForApi(string projectId, int index, int page, string unitWorkId = "", string ControlItemContent = "", string ControlPoint = "", string ControlItemDef = "", string HGForms = "")
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                IQueryable<Model.View_WBS_ControlItemAndCycle> q = db.View_WBS_ControlItemAndCycle;
                if (!string.IsNullOrEmpty(projectId) && "undefined" != projectId)
                {
                    q = q.Where(e => e.ProjectId == projectId);
                }
                if (!string.IsNullOrEmpty(unitWorkId) && "undefined" != unitWorkId)
                {
                    q = q.Where(e => e.UnitWorkId.Contains(unitWorkId));
                }
                if (!string.IsNullOrEmpty(ControlItemContent) && "undefined" != ControlItemContent)
                {
                    q = q.Where(e => e.ControlItemContent.Contains(ControlItemContent));
                }
                //if (!string.IsNullOrEmpty(ControlPoint) && "undefined" != ControlPoint)
                //{
                //    q = q.Where(e => e.ControlPoint.Contains(ControlPoint));
                //}
                if (!string.IsNullOrEmpty(ControlPoint) && "undefined" != ControlPoint)
                {
                    if (ControlPoint.ToString().Contains(","))
                    {
                        string[] strArray = ControlPoint.Split(',');
                        q = q.Where(e => strArray.Contains(e.ControlPoint));
                    }
                    else
                    {
                        q = q.Where(e => e.ControlPoint.IndexOf(ControlPoint)>=0);
                    }
                }
                if (!string.IsNullOrEmpty(ControlItemDef) && "undefined" != ControlItemDef)
                {
                    q = q.Where(e => e.ControlItemDef.Contains(ControlItemDef));
                }
                if (!string.IsNullOrEmpty(HGForms) && "undefined" != HGForms)
                {
                    q = q.Where(e => e.HGForms.Contains(HGForms) || e.SHForms.Contains(HGForms));
                }
                q = q.Where(e => e.IsApprove == true);

                var qres = from x in q
                           select new
                           {
                               x.ControlItemAndCycleId,
                               x.UnitWorkId,
                               UnitWorkName = BLL.UnitWorkService.GetUnitWorkName(x.UnitWorkId),
                               x.ControlItemContent,
                               x.ProjectId,
                               x.ControlPoint,
                               x.Weights,
                               x.ControlItemDef,
                               x.HGFormsJZ,
                               x.HGForms,
                               x.SHForms,
                               x.Standard,
                               x.CheckNum
                           };
                List<Model.View_WBS_ControlItemAndCycle> res = new List<Model.View_WBS_ControlItemAndCycle>();

                var list = qres.Skip(index * page).Take(page).ToList();
                foreach (var item in list)
                {
                    Model.View_WBS_ControlItemAndCycle tc = new Model.View_WBS_ControlItemAndCycle();
                    tc.ControlItemAndCycleId = item.ControlItemAndCycleId;
                    tc.ProjectId = item.ProjectId;
                    tc.UnitWorkId = item.UnitWorkId + "$" + item.UnitWorkName;
                    tc.ControlItemContent = item.ControlItemContent;
                    tc.ControlPoint = item.ControlPoint;
                    tc.Weights = item.Weights;
                    tc.ControlItemDef = item.ControlItemDef;
                    tc.HGFormsJZ = item.HGFormsJZ;
                    tc.HGForms = item.HGForms;
                    tc.SHForms = item.SHForms;
                    tc.Standard = item.Standard;
                    tc.CheckNum = item.CheckNum;
                    res.Add(tc);
                }
                return res;
            }
        }

        /// <summary>
        /// 根据编号获取明细
        /// </summary>
        /// <param name="controlItemCode"></param>
        public static Model.WBS_ControlItemInit GetControlItemInitByCode(string controlItemCode)
        {
            return Funs.DB.WBS_ControlItemInit.FirstOrDefault(e => e.ControlItemCode == controlItemCode);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="ControlItemAndCycle"></param>
        public static void AddControlItemInit(Model.WBS_ControlItemInit controlItem)
        {
            Model.SGGLDB db = Funs.DB;
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
            Model.SGGLDB db = Funs.DB;
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
            Model.SGGLDB db = Funs.DB;
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
            Model.SGGLDB db = Funs.DB;
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
            var q = from x in Funs.DB.WBS_ControlItemInit where x.WorkPackageCode == workPackageCode && x.ControlItemContent == controlItemContent && x.ControlItemCode != controlItemCode select x;
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
