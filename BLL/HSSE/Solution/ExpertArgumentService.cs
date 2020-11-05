using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 专家论证清单
    /// </summary>
    public static class ExpertArgumentService
    {
        public static Model.SGGLDB db = Funs.DB;

        #region 专家论证清单
        /// <summary>
        /// 根据主键获取专家论证清单
        /// </summary>
        /// <param name="expertArgumentId"></param>
        /// <returns></returns>
        public static Model.Solution_ExpertArgument GetExpertArgumentById(string expertArgumentId)
        {
            return Funs.DB.Solution_ExpertArgument.FirstOrDefault(e => e.ExpertArgumentId == expertArgumentId);
        }

        /// <summary>
        /// 添加专家论证清单
        /// </summary>
        /// <param name="expertArgument"></param>
        public static void AddExpertArgument(Model.Solution_ExpertArgument expertArgument)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Solution_ExpertArgument newExpertArgument = new Model.Solution_ExpertArgument
            {
                ExpertArgumentId = expertArgument.ExpertArgumentId,
                ExpertArgumentCode = expertArgument.ExpertArgumentCode,
                HazardType = expertArgument.HazardType,
                ProjectId = expertArgument.ProjectId,
                Address = expertArgument.Address,
                ExpectedTime = expertArgument.ExpectedTime,
                IsArgument = expertArgument.IsArgument,
                RecardMan = expertArgument.RecardMan,
                RecordTime = expertArgument.RecordTime,
                Remark = expertArgument.Remark,
                States = expertArgument.States,
                Descriptions = expertArgument.Descriptions
            };
            db.Solution_ExpertArgument.InsertOnSubmit(newExpertArgument);
            db.SubmitChanges();

            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectExpertArgumentMenuId, expertArgument.ProjectId, null, expertArgument.ExpertArgumentId, expertArgument.ExpectedTime);
        }

        /// <summary>
        /// 修改专家论证清单
        /// </summary>
        /// <param name="expertArgument"></param>
        public static void UpdateExpertArgument(Model.Solution_ExpertArgument expertArgument)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Solution_ExpertArgument newExpertArgument = db.Solution_ExpertArgument.FirstOrDefault(e => e.ExpertArgumentId == expertArgument.ExpertArgumentId);
            if (newExpertArgument != null)
            {
                newExpertArgument.ExpertArgumentCode = expertArgument.ExpertArgumentCode;
                newExpertArgument.HazardType = expertArgument.HazardType;
                newExpertArgument.ProjectId = expertArgument.ProjectId;
                newExpertArgument.Address = expertArgument.Address;
                newExpertArgument.ExpectedTime = expertArgument.ExpectedTime;
                newExpertArgument.IsArgument = expertArgument.IsArgument;
                newExpertArgument.RecardMan = expertArgument.RecardMan;
                newExpertArgument.RecordTime = expertArgument.RecordTime;
                newExpertArgument.Remark = expertArgument.Remark;
                newExpertArgument.States = expertArgument.States;
                newExpertArgument.Descriptions = expertArgument.Descriptions;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除专家论证清单
        /// </summary>
        /// <param name="expertArgumentId"></param>
        public static void DeleteExpertArgumentById(string expertArgumentId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Solution_ExpertArgument expertArgument = db.Solution_ExpertArgument.FirstOrDefault(e => e.ExpertArgumentId == expertArgumentId);
            if (expertArgument != null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(expertArgumentId);
                db.Solution_ExpertArgument.DeleteOnSubmit(expertArgument);
                db.SubmitChanges();
            }
        }
        #endregion

        /// <summary>
        /// 根据主键获取危大工程清单
        /// </summary>
        /// <param name="LargerHazardListId"></param>
        /// <returns></returns>
        public static Model.Solution_LargerHazardList GetLargerHazardListById(string LargerHazardListId)
        {
            return Funs.DB.Solution_LargerHazardList.FirstOrDefault(e => e.LargerHazardListId == LargerHazardListId);
        }

        /// <summary>
        /// 添加危大工程清单
        /// </summary>
        /// <param name="LargerHazardList"></param>
        public static void AddLargerHazardList(Model.Solution_LargerHazardList LargerHazardList)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Solution_LargerHazardList newLargerHazardList = new Model.Solution_LargerHazardList
            {
                LargerHazardListId = LargerHazardList.LargerHazardListId,
                HazardCode = LargerHazardList.HazardCode,
                ProjectId = LargerHazardList.ProjectId,
                RecardManId = LargerHazardList.RecardManId,
                RecordTime = LargerHazardList.RecordTime,
                Remark = LargerHazardList.Remark,
                VersionNo = LargerHazardList.VersionNo,
                States = LargerHazardList.States,
            };
            db.Solution_LargerHazardList.InsertOnSubmit(newLargerHazardList);
            db.SubmitChanges();

            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectExpertArgumentMenuId, LargerHazardList.ProjectId, null, LargerHazardList.LargerHazardListId, LargerHazardList.RecordTime);
        }

        /// <summary>
        /// 修改危大工程清单
        /// </summary>
        /// <param name="LargerHazardList"></param>
        public static void UpdateLargerHazardList(Model.Solution_LargerHazardList LargerHazardList)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Solution_LargerHazardList newLargerHazardList = db.Solution_LargerHazardList.FirstOrDefault(e => e.LargerHazardListId == LargerHazardList.LargerHazardListId);
            if (newLargerHazardList != null)
            {
                newLargerHazardList.HazardCode = LargerHazardList.HazardCode;
                newLargerHazardList.RecardManId = LargerHazardList.RecardManId;
                newLargerHazardList.RecordTime = LargerHazardList.RecordTime;
                newLargerHazardList.Remark = LargerHazardList.Remark;
                newLargerHazardList.VersionNo = LargerHazardList.VersionNo;
                newLargerHazardList.States = LargerHazardList.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除危大工程清单
        /// </summary>
        /// <param name="LargerHazardListId"></param>
        public static void DeleteLargerHazardListById(string LargerHazardListId)
        {
            Model.SGGLDB db = Funs.DB;
            var getDelLargerHazardList = db.Solution_LargerHazardList.FirstOrDefault(e => e.LargerHazardListId == LargerHazardListId);
            if (getDelLargerHazardList != null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(LargerHazardListId);
                db.Solution_LargerHazardList.DeleteOnSubmit(getDelLargerHazardList);
                db.SubmitChanges();
            }
        }

        #region 危大工程清单明细
        /// <summary>
        /// 
        /// </summary>
        public static List<Model.View_Solution_LargerHazardListItem> getViewLargerHazardListItem = new List<Model.View_Solution_LargerHazardListItem>();

        /// <summary>
        /// 根据主键获取危大工程清单明细
        /// </summary>
        /// <param name="LargerHazardListItemId"></param>
        /// <returns></returns>
        public static Model.Solution_LargerHazardListItem GetLargerHazardListItemById(string LargerHazardListItemId)
        {
            return Funs.DB.Solution_LargerHazardListItem.FirstOrDefault(e => e.LargerHazardListItemId == LargerHazardListItemId);
        }

        /// <summary>
        /// 添加危大工程清单明细
        /// </summary>
        /// <param name="LargerHazardListItem"></param>
        public static void AddLargerHazardListItem(Model.Solution_LargerHazardListItem LargerHazardListItem)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Solution_LargerHazardListItem newLargerHazardListItem = new Model.Solution_LargerHazardListItem
            {
                LargerHazardListItemId = LargerHazardListItem.LargerHazardListItemId,
                SortIndex = LargerHazardListItem.SortIndex,
                LargerHazardListId = LargerHazardListItem.LargerHazardListId,
                UnitWorkId = LargerHazardListItem.UnitWorkId,
                WorkPackageId = LargerHazardListItem.WorkPackageId,
                WorkPackageSize = LargerHazardListItem.WorkPackageSize,
                ExpectedStartTime = LargerHazardListItem.ExpectedStartTime,
                ExpectedEndTime = LargerHazardListItem.ExpectedEndTime,
                IsArgument = LargerHazardListItem.IsArgument,
                UnitId = LargerHazardListItem.UnitId,
            };
            db.Solution_LargerHazardListItem.InsertOnSubmit(newLargerHazardListItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改危大工程清单明细
        /// </summary>
        /// <param name="LargerHazardListItem"></param>
        public static void UpdateLargerHazardListItem(Model.Solution_LargerHazardListItem LargerHazardListItem)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Solution_LargerHazardListItem newLargerHazardListItem = db.Solution_LargerHazardListItem.FirstOrDefault(e => e.LargerHazardListItemId == LargerHazardListItem.LargerHazardListItemId);
            if (newLargerHazardListItem != null)
            {
                newLargerHazardListItem.SortIndex = LargerHazardListItem.SortIndex;
                newLargerHazardListItem.UnitWorkId = LargerHazardListItem.UnitWorkId;
                newLargerHazardListItem.WorkPackageId = LargerHazardListItem.WorkPackageId;
                newLargerHazardListItem.WorkPackageSize = LargerHazardListItem.WorkPackageSize;
                newLargerHazardListItem.ExpectedStartTime = LargerHazardListItem.ExpectedStartTime;
                newLargerHazardListItem.ExpectedEndTime = LargerHazardListItem.ExpectedEndTime;
                newLargerHazardListItem.IsArgument = LargerHazardListItem.IsArgument;
                newLargerHazardListItem.UnitId = LargerHazardListItem.UnitId;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除危大工程清单明细
        /// </summary>
        /// <param name="LargerHazardListItemId"></param>
        public static void DeleteLargerHazardListItemById(string LargerHazardListItemId)
        {
            Model.SGGLDB db = Funs.DB;
            var getDelLargerHazardListItem = db.Solution_LargerHazardListItem.FirstOrDefault(e => e.LargerHazardListItemId == LargerHazardListItemId);
            if (getDelLargerHazardListItem != null)
            {
                db.Solution_LargerHazardListItem.DeleteOnSubmit(getDelLargerHazardListItem);
                db.SubmitChanges();
            }
        }


        /// <summary>
        /// 根据主键删除危大工程清单明细
        /// </summary>
        /// <param name="LargerHazardListItemId"></param>
        public static void DeleteLargerHazardListItemByLargerHazardListId(string LargerHazardListId)
        {
            Model.SGGLDB db = Funs.DB;
            var getDelLargerHazardListItems = from x in db.Solution_LargerHazardListItem
                                             where x.LargerHazardListId == LargerHazardListId
                                             select x;
            if (getDelLargerHazardListItems.Count() > 0)
            {
                db.Solution_LargerHazardListItem.DeleteAllOnSubmit(getDelLargerHazardListItems);
                db.SubmitChanges();
            }
        }
        
        #endregion
    }
}
