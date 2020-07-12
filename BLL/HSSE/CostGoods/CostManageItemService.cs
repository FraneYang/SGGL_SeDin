using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace BLL
{
    /// <summary>
    /// 费用管理明细
    /// </summary>
    public static class CostManageItemService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键删除费用管理明细
        /// </summary>
        /// <param name="costManageItemId"></param>
        /// <returns></returns>
        public static Model.CostGoods_CostManageItem GetCostManageItemById(string costManageItemId)
        {
            return new Model.SGGLDB(Funs.ConnString).CostGoods_CostManageItem.FirstOrDefault(e => e.CostManageItemId == costManageItemId);
        }

        /// <summary>
        /// 根据费用管理主键获取所有相关明细信息
        /// </summary>
        /// <param name="costManageId"></param>
        /// <returns></returns>
        public static List<Model.CostGoods_CostManageItem> GetCostManageItemByCostManageId(string costManageId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).CostGoods_CostManageItem where x.CostManageId == costManageId select x).ToList();
        }

        /// <summary>
        /// 根据项目Id，单位Id和月份获取安全费用管理明细
        /// </summary>
        /// <param name="costManageId"></param>
        /// <returns></returns>
        public static List<Model.CostGoods_CostManageItem> GetCostManageItemByUnitIdAndDate(string projectId, string unidId, DateTime date)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).CostGoods_CostManageItem
                    join y in new Model.SGGLDB(Funs.ConnString).CostGoods_CostManage
                    on x.CostManageId equals y.CostManageId
                    where y.ProjectId == projectId && y.UnitId == unidId
                    && y.CostManageDate.Value.Year == date.Year && y.CostManageDate.Value.Month == date.Month
                    select x).ToList();
        }

        /// <summary>
        /// 添加费用管理明细
        /// </summary>
        /// <param name="costManageItem"></param>
        public static void AddCostManageItem(Model.CostGoods_CostManageItem costManageItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.CostGoods_CostManageItem newCostManageItem = new Model.CostGoods_CostManageItem
            {
                CostManageItemId = costManageItem.CostManageItemId,
                CostManageId = costManageItem.CostManageId,
                InvestCostProject = costManageItem.InvestCostProject,
                UseReason = costManageItem.UseReason,
                Counts = costManageItem.Counts,
                PriceMoney = costManageItem.PriceMoney,
                AuditCounts = costManageItem.AuditCounts,
                AuditPriceMoney = costManageItem.AuditPriceMoney,
                Remark = costManageItem.Remark
            };
            db.CostGoods_CostManageItem.InsertOnSubmit(newCostManageItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改费用管理明细
        /// </summary>
        /// <param name="costManageItem"></param>
        public static void UpdateCostManageItem(Model.CostGoods_CostManageItem costManageItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.CostGoods_CostManageItem newCostManageItem = db.CostGoods_CostManageItem.FirstOrDefault(e => e.CostManageItemId == costManageItem.CostManageItemId);
            if (newCostManageItem != null)
            {
                newCostManageItem.CostManageId = costManageItem.CostManageId;
                newCostManageItem.InvestCostProject = costManageItem.InvestCostProject;
                newCostManageItem.UseReason = costManageItem.UseReason;
                newCostManageItem.Counts = costManageItem.Counts;
                newCostManageItem.PriceMoney = costManageItem.PriceMoney;
                newCostManageItem.AuditCounts = costManageItem.AuditCounts;
                newCostManageItem.AuditPriceMoney = costManageItem.AuditPriceMoney;
                newCostManageItem.Remark = costManageItem.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据费用管理主键删除所有相关明细信息
        /// </summary>
        /// <param name="costManageId"></param>
        public static void DeleteCostManageItemByCostManageId(string costManageId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var q = (from x in db.CostGoods_CostManageItem where x.CostManageId == costManageId select x).ToList();
            if (q != null)
            {
                db.CostGoods_CostManageItem.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除费用管理信息
        /// </summary>
        /// <param name="costManageItemId"></param>
        public static void DeleteCostManageItemById(string costManageItemId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.CostGoods_CostManageItem costManageItem = db.CostGoods_CostManageItem.FirstOrDefault(e => e.CostManageItemId == costManageItemId);
            if (costManageItem != null)
            {
                db.CostGoods_CostManageItem.DeleteOnSubmit(costManageItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 投入费用项目下拉框内容
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetInvestCostProjectList()
        {
            ListItem[] lis = new ListItem[29];
            lis[0] = new ListItem("内业管理", "内业管理");
            lis[1] = new ListItem("检测器材", "检测器材");
            lis[2] = new ListItem("警示警戒", "警示警戒");
            lis[3] = new ListItem("安全奖励", "安全奖励");
            lis[4] = new ListItem("其他", "其他");
            lis[5] = new ListItem("安全技术", "安全技术");
            lis[6] = new ListItem("工业卫生", "工业卫生");
            lis[7] = new ListItem("安全用电", "安全用电");
            lis[8] = new ListItem("高处作业及基坑", "高处作业及基坑");
            lis[9] = new ListItem("临边洞口防护", "临边洞口防护");
            lis[10] = new ListItem("受限空间内作业", "受限空间内作业");
            lis[11] = new ListItem("动火作业", "动火作业");
            lis[12] = new ListItem("机械装备防护", "机械装备防护");
            lis[13] = new ListItem("吊装运输和起重", "吊装运输和起重");
            lis[14] = new ListItem("硼砂作业", "硼砂作业");
            lis[15] = new ListItem("拆除工程", "拆除工程");
            lis[16] = new ListItem("试压试车与有害介质作业", "试压试车与有害介质作业");
            lis[17] = new ListItem("特种作业防护", "特种作业防护");
            lis[18] = new ListItem("应急管理", "应急管理");
            lis[19] = new ListItem("非常措施", "非常措施");
            lis[20] = new ListItem("其他安全措施", "其他安全措施");
            lis[21] = new ListItem("装置区封闭管理", "装置区封闭管理");
            lis[22] = new ListItem("防爆施工器具", "防爆施工器具");
            lis[23] = new ListItem("标识标签与锁定", "标识标签与锁定");
            lis[24] = new ListItem("关键场所封闭", "关键场所封闭");
            lis[25] = new ListItem("催化剂加装还原", "催化剂加装还原");
            lis[26] = new ListItem("联动和化工试车", "联动和化工试车");
            lis[27] = new ListItem("教育培训", "教育培训");
            lis[28] = new ListItem("防护控制和排放", "防护控制和排放");
            return lis;
        }

        public static decimal? GetCostsByUnitId(string unitId, DateTime startTime, DateTime endTime)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var q = (from x in db.CostGoods_CostManageItem
                         join y in db.CostGoods_CostManage
                         on x.CostManageId equals y.CostManageId
                         where y.UnitId == unitId && y.States == BLL.Const.State_2 && y.CostManageDate >= startTime && y.CostManageDate < endTime
                         select x).ToList();
                if (q.Count > 0)
                {
                    return q.Sum(e => (e.AuditCounts * e.AuditPriceMoney));
                }
                return null;
            }
        }
    }
}