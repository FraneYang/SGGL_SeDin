using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public static class Hazard_HazardListService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 要求主键获取危险清单信息
        /// </summary>
        /// <param name="hazardListCode">主键</param>
        /// <returns></returns>
        public static Model.Hazard_HazardList GetHazardList(string hazardListId)
        {
            return new Model.SGGLDB(Funs.ConnString).Hazard_HazardList.FirstOrDefault(e => e.HazardListId == hazardListId);
        }

        /// <summary>
        /// 查询版本号为空的危险清单的数量
        /// </summary>
        /// <returns>危险清单的数量</returns>
        public static int GetHazardListCountByVersionNoIsNull(string projectId)
        {
            return (from x in db.Hazard_HazardList where x.VersionNo == null && x.ProjectId == projectId select x).Count();
        }

        /// <summary>
        /// 根据项目主键和开始、结束时间获得危险源辨识与评价清单的数量
        /// </summary>
        /// <param name="projectId">项目主键</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public static int GetHazardListCountByProjectIdAndDate(string projectId, DateTime startTime, DateTime endTime)
        {
            var q = (from x in new Model.SGGLDB(Funs.ConnString).Hazard_HazardList where x.ProjectId == projectId && x.CompileDate >= startTime && x.CompileDate <= endTime select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 增加危险源辨识与评价清单信息
        /// </summary>
        /// <param name="hazardList">危险源辨识与评价清单实体</param>
        public static void AddHazardList(Model.Hazard_HazardList hazardList)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Hazard_HazardList newHazardList = new Model.Hazard_HazardList
            {
                HazardListId = hazardList.HazardListId,
                HazardListCode = hazardList.HazardListCode,
                ProjectId = hazardList.ProjectId,
                VersionNo = hazardList.VersionNo,
                CompileMan = hazardList.CompileMan,
                CompileDate = hazardList.CompileDate,
                States = hazardList.States,
                WorkStage = hazardList.WorkStage,
                Contents = hazardList.Contents,
                WorkAreaName = hazardList.WorkAreaName,
                IdentificationDate = hazardList.IdentificationDate,
                ControllingPerson = hazardList.ControllingPerson
            };
            new Model.SGGLDB(Funs.ConnString).Hazard_HazardList.InsertOnSubmit(newHazardList);
            new Model.SGGLDB(Funs.ConnString).SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectHazardListMenuId, hazardList.ProjectId, null, hazardList.HazardListId, hazardList.CompileDate);
        }

        /// <summary>
        /// 修改危险源辨识与评价清单信息
        /// </summary>
        /// <param name="hazardList">危险源辨识与评价清单实体</param>
        public static void UpdateHazardList(Model.Hazard_HazardList hazardList)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Hazard_HazardList newHazardList = db.Hazard_HazardList.FirstOrDefault(e => e.HazardListId == hazardList.HazardListId);
            if (newHazardList != null)
            {
                newHazardList.HazardListCode = hazardList.HazardListCode;
                //newHazardList.ProjectId = hazardList.ProjectId;
                newHazardList.VersionNo = hazardList.VersionNo;
                newHazardList.CompileMan = hazardList.CompileMan;
                newHazardList.CompileDate = hazardList.CompileDate;
                newHazardList.States = hazardList.States;
                newHazardList.WorkStage = hazardList.WorkStage;
                newHazardList.Contents = hazardList.Contents;
                newHazardList.WorkAreaName = hazardList.WorkAreaName;
                newHazardList.IdentificationDate = hazardList.IdentificationDate;
                newHazardList.ControllingPerson = hazardList.ControllingPerson;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据危险源辨识与评价清单Id删除一个危险源辨识与评价清单信息
        /// </summary>
        /// <param name="hazardListCode">危险源辨识与评价清单Id</param>
        public static void DeleteHazardListByHazardListId(string hazardListId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Hazard_HazardList hazardList = db.Hazard_HazardList.FirstOrDefault(e => e.HazardListId == hazardListId);
            if (hazardList != null)
            {
                ////删除审核流程表
                BLL.CommonService.DeleteFlowOperateByID(hazardList.HazardListId);
                ///删除附件
                BLL.CommonService.DeleteAttachFileById(hazardList.HazardListId);
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(hazardList.HazardListId);
                db.Hazard_HazardList.DeleteOnSubmit(hazardList);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据用户主键获得危险源辨识与评价清单的数量
        /// </summary>
        /// <param name="userId">角色</param>
        /// <returns></returns>
        public static int GetHazardListCountByUserId(string userId)
        {
            var q = (from x in new Model.SGGLDB(Funs.ConnString).Hazard_HazardList where x.CompileMan == userId select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 根据项目主键获得危险源辨识与评价清单的数量
        /// </summary>
        /// <param name="projectId">项目主键</param>
        /// <returns></returns>
        public static int GetHazardListCountByProjectId(string projectId)
        {
            var q = (from x in new Model.SGGLDB(Funs.ConnString).Hazard_HazardList where x.ProjectId == projectId select x).ToList();
            return q.Count();
        }
    }
}
