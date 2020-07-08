using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 危险性较大的工程清单
    /// </summary>
    public static class LargerHazardService
    {
        /// <summary>
        /// 根据危险性较大的工程清单ID获取危险性较大的工程清单信息
        /// </summary>
        /// <param name="LargerHazardName"></param>
        /// <returns></returns>
        public static Model.Solution_LargerHazard GetLargerHazardByHazardId(string hazardId)
        {
            return new Model.SGGLDB(Funs.ConnString).Solution_LargerHazard.FirstOrDefault(e => e.HazardId == hazardId);
        }

        /// <summary>
        /// 添加安全危险性较大的工程清单
        /// </summary>
        /// <param name="largerHazard"></param>
        public static void AddLargerHazard(Model.Solution_LargerHazard largerHazard)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Solution_LargerHazard newLargerHazard = new Model.Solution_LargerHazard
            {
                HazardId = largerHazard.HazardId,
                HazardCode = largerHazard.HazardCode,
                HazardType = largerHazard.HazardType,
                ProjectId = largerHazard.ProjectId,
                Address = largerHazard.Address,
                ExpectedTime = largerHazard.ExpectedTime,
                IsArgument = largerHazard.IsArgument,
                RecordTime = largerHazard.RecordTime,
                RecardMan = largerHazard.RecardMan,
                Remark = largerHazard.Remark,
                States = largerHazard.States,
                Descriptions = largerHazard.Descriptions
            };

            db.Solution_LargerHazard.InsertOnSubmit(newLargerHazard);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectLargerHazardListMenuId, largerHazard.ProjectId, null, largerHazard.HazardId, largerHazard.RecordTime);
        }

        /// <summary>
        /// 修改安全危险性较大的工程清单
        /// </summary>
        /// <param name="largerHazard"></param>
        public static void UpdateLargerHazard(Model.Solution_LargerHazard largerHazard)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Solution_LargerHazard newLargerHazard = db.Solution_LargerHazard.FirstOrDefault(e => e.HazardId == largerHazard.HazardId);
            if (newLargerHazard != null)
            {
                newLargerHazard.HazardCode = largerHazard.HazardCode;
                newLargerHazard.HazardType = largerHazard.HazardType;
                newLargerHazard.ProjectId = largerHazard.ProjectId;
                newLargerHazard.Address = largerHazard.Address;
                newLargerHazard.ExpectedTime = largerHazard.ExpectedTime;
                newLargerHazard.IsArgument = largerHazard.IsArgument;
                newLargerHazard.Remark = largerHazard.Remark;
                newLargerHazard.States = largerHazard.States;
                newLargerHazard.Descriptions = largerHazard.Descriptions;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据危险性较大的工程清单ID删除对应危险性较大的工程清单记录信息
        /// </summary>
        /// <param name="superviseCheckReportId"></param>
        public static void DeleteLargerHazard(string hazardId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var largerHazard = (from x in db.Solution_LargerHazard where x.HazardId == hazardId select x).FirstOrDefault();
            if (largerHazard != null)
            {
                ////删除审核流程表
                BLL.CommonService.DeleteFlowOperateByID(largerHazard.HazardId);
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(largerHazard.HazardId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(largerHazard.HazardId);
                db.Solution_LargerHazard.DeleteOnSubmit(largerHazard);
                db.SubmitChanges();
            }
        }
    }
}
