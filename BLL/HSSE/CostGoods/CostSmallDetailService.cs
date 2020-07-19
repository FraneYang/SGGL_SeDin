using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 安全费用投入登记
    /// </summary>
    public static class CostSmallDetailService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取安全费用投入登记
        /// </summary>
        /// <param name="costSmallDetailId"></param>
        /// <returns></returns>
        public static Model.CostGoods_CostSmallDetail GetCostSmallDetailById(string costSmallDetailId)
        {
            return Funs.DB.CostGoods_CostSmallDetail.FirstOrDefault(e => e.CostSmallDetailId == costSmallDetailId);
        }

        /// <summary>
        /// 添加安全费用投入登记
        /// </summary>
        /// <param name="costSmallDetail"></param>
        public static void AddCostSmallDetail(Model.CostGoods_CostSmallDetail costSmallDetail)
        {
            Model.SGGLDB db = Funs.DB;
            Model.CostGoods_CostSmallDetail newCostSmallDetail = new Model.CostGoods_CostSmallDetail
            {
                CostSmallDetailId = costSmallDetail.CostSmallDetailId,
                ProjectId = costSmallDetail.ProjectId,
                CostSmallDetailCode = costSmallDetail.CostSmallDetailCode,
                UnitId = costSmallDetail.UnitId,
                States = costSmallDetail.States,
                //newCostSmallDetail.CompileMan = costSmallDetail.CompileMan;
                CompileDate = costSmallDetail.CompileDate,
                Months = costSmallDetail.Months,
                ReportDate = costSmallDetail.ReportDate,
                //newCostSmallDetail.CheckMan = costSmallDetail.CheckMan;
                CheckDate = costSmallDetail.CheckDate,
                // newCostSmallDetail.ApproveMan = costSmallDetail.ApproveMan;
                ApproveDate = costSmallDetail.ApproveDate
            };
            db.CostGoods_CostSmallDetail.InsertOnSubmit(newCostSmallDetail);
            db.SubmitChanges();
            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectCostSmallDetailMenuId, costSmallDetail.ProjectId, null, costSmallDetail.CostSmallDetailId, costSmallDetail.CompileDate);
        }

        /// <summary>
        /// 修改安全费用投入登记
        /// </summary>
        /// <param name="costSmallDetail"></param>
        public static void UpdateCostSmallDetail(Model.CostGoods_CostSmallDetail costSmallDetail)
        {
            Model.SGGLDB db = Funs.DB;
            Model.CostGoods_CostSmallDetail newCostSmallDetail = db.CostGoods_CostSmallDetail.FirstOrDefault(e => e.CostSmallDetailId == costSmallDetail.CostSmallDetailId);
            if (newCostSmallDetail != null)
            {
                //newCostSmallDetail.ProjectId = costSmallDetail.ProjectId;
                newCostSmallDetail.CostSmallDetailCode = costSmallDetail.CostSmallDetailCode;
                newCostSmallDetail.UnitId = costSmallDetail.UnitId;
                newCostSmallDetail.States = costSmallDetail.States;
               // newCostSmallDetail.CompileMan = costSmallDetail.CompileMan;
                //newCostSmallDetail.CompileDate = costSmallDetail.CompileDate;
                newCostSmallDetail.Months = costSmallDetail.Months;
                newCostSmallDetail.ReportDate = costSmallDetail.ReportDate;
               // newCostSmallDetail.CheckMan = costSmallDetail.CheckMan;
                newCostSmallDetail.CheckDate = costSmallDetail.CheckDate;
               // newCostSmallDetail.ApproveMan = costSmallDetail.ApproveMan;
                newCostSmallDetail.ApproveDate = costSmallDetail.ApproveDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除安全费用投入登记
        /// </summary>
        /// <param name="costSmallDetailId"></param>
        public static void DeleteCostSmallDetailById(string costSmallDetailId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.CostGoods_CostSmallDetail costSmallDetail = db.CostGoods_CostSmallDetail.FirstOrDefault(e => e.CostSmallDetailId == costSmallDetailId);
            if (costSmallDetail != null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(costSmallDetailId);//删除编号
                CommonService.DeleteAttachFileById(costSmallDetailId);//删除附件
                 ////删除流程表
                BLL.CommonService.DeleteFlowOperateByID(costSmallDetail.CostSmallDetailId);
                db.CostGoods_CostSmallDetail.DeleteOnSubmit(costSmallDetail);
                db.SubmitChanges();
            }
        }
    }
}