﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 焊接工艺评定台账
    /// </summary>
    public static class WPQListServiceService
    {
        /// <summary>
        /// 根据主键获取焊接工艺评定台账
        /// </summary>
        /// <param name="WPQId"></param>
        /// <returns></returns>
        public static Model.WPQ_WPQList GetWPQById(string wpqId)
        {
            return Funs.DB.WPQ_WPQList.FirstOrDefault(e => e.WPQId == wpqId);
        }

        /// <summary>
        /// 添加焊接工艺评定台账
        /// </summary>
        /// <param name="WPQ"></param>
        public static void AddWPQ(Model.WPQ_WPQList WPQ)
        {
            Model.SGGLDB db = Funs.DB;
            Model.WPQ_WPQList newWPQ = new Model.WPQ_WPQList();
            newWPQ.WPQId = WPQ.WPQId;
            newWPQ.WPQCode = WPQ.WPQCode;
            newWPQ.UnitId = WPQ.UnitId;
            newWPQ.CompileDate = WPQ.CompileDate;
            newWPQ.MaterialId1 = WPQ.MaterialId1;
            newWPQ.MaterialId2 = WPQ.MaterialId2;
            newWPQ.Material1Class = WPQ.Material1Class;
            newWPQ.Material2Class = WPQ.Material2Class;
            newWPQ.Material1Group = WPQ.Material1Group;
            newWPQ.Material2Group = WPQ.Material2Group;

            newWPQ.Specifications = WPQ.Specifications;
            newWPQ.WeldingRod = WPQ.WeldingRod;
            newWPQ.WeldingWire = WPQ.WeldingWire;
            newWPQ.GrooveType = WPQ.GrooveType;
            newWPQ.WeldingPosition = WPQ.WeldingPosition;
            newWPQ.WeldingMethodId = WPQ.WeldingMethodId;
            newWPQ.MinImpactDia = WPQ.MinImpactDia;
            newWPQ.MaxImpactDia = WPQ.MaxImpactDia;

            newWPQ.MinCImpactDia = WPQ.MinCImpactDia;
            newWPQ.MaxCImpactDia = WPQ.MaxCImpactDia;
            newWPQ.MinImpactThickness = WPQ.MinImpactThickness;
            newWPQ.MaxImpactThickness = WPQ.MaxImpactThickness;
            newWPQ.NoMinImpactThickness = WPQ.NoMinImpactThickness;
            newWPQ.NoMaxImpactThickness = WPQ.NoMaxImpactThickness;
            newWPQ.IsHotProess = WPQ.IsHotProess;
            newWPQ.WPQStandard = WPQ.WPQStandard;
            newWPQ.PreTemperature = WPQ.PreTemperature;
            newWPQ.Remark = WPQ.Remark;
            newWPQ.JointType = WPQ.JointType;
            newWPQ.Motorization = WPQ.Motorization;
            newWPQ.ProtectiveGas = WPQ.ProtectiveGas;
            newWPQ.Stretching = WPQ.Stretching;
            newWPQ.Bend = WPQ.Bend;
            newWPQ.ToAttack = WPQ.ToAttack;
            newWPQ.Others = WPQ.Others;
            newWPQ.State = WPQ.State;
            newWPQ.ApproveManId = WPQ.ApproveManId;
            newWPQ.RequiredT = WPQ.RequiredT;
            newWPQ.RequestTime = WPQ.RequestTime;
            newWPQ.WeldingWireSpecification = WPQ.WeldingWireSpecification;
            newWPQ.WeldingRodSpecification = WPQ.WeldingRodSpecification;
            newWPQ.GasProtectionModeId = WPQ.GasProtectionModeId;
            newWPQ.ProtectionGasId = WPQ.ProtectionGasId;
            db.WPQ_WPQList.InsertOnSubmit(newWPQ);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改焊接工艺评定台账
        /// </summary>
        /// <param name="WPQ"></param>
        public static void UpdateWPQ(Model.WPQ_WPQList WPQ)
        {
            Model.SGGLDB db = Funs.DB;
            Model.WPQ_WPQList newWPQ = db.WPQ_WPQList.FirstOrDefault(e => e.WPQId == WPQ.WPQId);
            if (newWPQ != null)
            {
                newWPQ.WPQCode = WPQ.WPQCode;
                newWPQ.UnitId = WPQ.UnitId;
                newWPQ.CompileDate = WPQ.CompileDate;
                newWPQ.MaterialId1 = WPQ.MaterialId1;
                newWPQ.MaterialId2 = WPQ.MaterialId2;
                newWPQ.Material1Class = WPQ.Material1Class;
                newWPQ.Material2Class = WPQ.Material2Class;
                newWPQ.Material1Group = WPQ.Material1Group;
                newWPQ.Material2Group = WPQ.Material2Group;
                newWPQ.Specifications = WPQ.Specifications;
                newWPQ.WeldingRod = WPQ.WeldingRod;
                newWPQ.WeldingWire = WPQ.WeldingWire;
                newWPQ.GrooveType = WPQ.GrooveType;
                newWPQ.WeldingPosition = WPQ.WeldingPosition;
                newWPQ.WeldingMethodId = WPQ.WeldingMethodId;
                newWPQ.MinImpactDia = WPQ.MinImpactDia;
                newWPQ.MaxImpactDia = WPQ.MaxImpactDia;
                newWPQ.MinImpactThickness = WPQ.MinImpactThickness;
                newWPQ.MaxImpactThickness = WPQ.MaxImpactThickness;
                newWPQ.NoMinImpactThickness = WPQ.NoMinImpactThickness;
                newWPQ.NoMaxImpactThickness = WPQ.NoMaxImpactThickness;
                newWPQ.IsHotProess = WPQ.IsHotProess;
                newWPQ.WPQStandard = WPQ.WPQStandard;
                newWPQ.PreTemperature = WPQ.PreTemperature;
                newWPQ.Remark = WPQ.Remark;
                newWPQ.JointType = WPQ.JointType;
                newWPQ.Motorization = WPQ.Motorization;
                newWPQ.ProtectiveGas = WPQ.ProtectiveGas;
                newWPQ.Stretching = WPQ.Stretching;
                newWPQ.Bend = WPQ.Bend;
                newWPQ.ToAttack = WPQ.ToAttack;
                newWPQ.Others = WPQ.Others;
                newWPQ.State = WPQ.State;
                newWPQ.ApproveManId = WPQ.ApproveManId;
                newWPQ.ApproveTime = WPQ.ApproveTime;
                newWPQ.RequiredT = WPQ.RequiredT;
                newWPQ.RequestTime = WPQ.RequestTime;
                newWPQ.WeldingWireSpecification = WPQ.WeldingWireSpecification;
                newWPQ.WeldingRodSpecification = WPQ.WeldingRodSpecification;
                newWPQ.GasProtectionModeId = WPQ.GasProtectionModeId;
                newWPQ.ProtectionGasId = WPQ.ProtectionGasId;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除焊接工艺评定台账
        /// </summary>
        /// <param name="WPQId"></param>
        public static void DeleteWPQById(string WPQId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.WPQ_WPQList WPQ = db.WPQ_WPQList.FirstOrDefault(e => e.WPQId == WPQId);
            if (WPQ != null)
            {
                var flow = db.WPQ_WPQListFlowOperate.Where(x => x.WPQId == WPQId);
                if (flow.Count() > 0)
                {
                    db.WPQ_WPQListFlowOperate.DeleteAllOnSubmit(flow);
                    db.SubmitChanges();
                }
                AttachFileService.DeleteAttachFile(Funs.RootPath, WPQId, Const.WPQListMenuId);//删除附件
                db.WPQ_WPQList.DeleteOnSubmit(WPQ);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 判断是否存在相同评定编号
        /// </summary>
        /// <param name="WPQId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool IsWPQCode(string WPQId, string code)
        {
            var q = Funs.DB.WPQ_WPQList.FirstOrDefault(x => (x.WPQId != WPQId || (WPQId == null && x.WPQId != null)) && x.WPQCode == code);
            if (q != null)
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
