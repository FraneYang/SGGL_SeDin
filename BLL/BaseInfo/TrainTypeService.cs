using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 培训类型
    /// </summary>
    public static class TrainTypeService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取培训类型
        /// </summary>
        /// <param name="trainTypeId"></param>
        /// <returns></returns>
        public static Model.Base_TrainType GetTrainTypeById(string trainTypeId)
        {
            return new Model.SGGLDB(Funs.ConnString).Base_TrainType.FirstOrDefault(e => e.TrainTypeId == trainTypeId);
        }
        
        /// <summary>
        /// 添加培训类型
        /// </summary>
        /// <param name="trainType"></param>
        public static void AddTrainType(Model.Base_TrainType trainType)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_TrainType newTrainType = new Model.Base_TrainType
            {
                TrainTypeId = trainType.TrainTypeId,
                TrainTypeCode = trainType.TrainTypeCode,
                TrainTypeName = trainType.TrainTypeName,
                Remark = trainType.Remark,
                IsAboutSendCard = trainType.IsAboutSendCard,
                IsRepeat = trainType.IsRepeat
            };
            db.Base_TrainType.InsertOnSubmit(newTrainType);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改培训类型
        /// </summary>
        /// <param name="trainType"></param>
        public static void UpdateTrainType(Model.Base_TrainType trainType)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_TrainType newTrainType = db.Base_TrainType.FirstOrDefault(e => e.TrainTypeId == trainType.TrainTypeId);
            if (newTrainType != null)
            {
                newTrainType.TrainTypeCode = trainType.TrainTypeCode;
                newTrainType.TrainTypeName = trainType.TrainTypeName;
                newTrainType.Remark = trainType.Remark;
                newTrainType.IsAboutSendCard = trainType.IsAboutSendCard;
                newTrainType.IsRepeat = trainType.IsRepeat;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除培训类型
        /// </summary>
        /// <param name="trainTypeId"></param>
        public static void DeleteTrainTypeById(string trainTypeId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_TrainType trainType = db.Base_TrainType.FirstOrDefault(e => e.TrainTypeId == trainTypeId);
            if (trainType != null)
            {
                var getItems = from x in db.Base_TrainTypeItem where x.TrainTypeId == trainType.TrainTypeId select x;
                if (getItems.Count() > 0)
                {
                    foreach (var item in getItems)
                    {
                        DeleteTrainTypeItemById(item.TrainTypeItemId);
                    }
                }
                db.Base_TrainType.DeleteOnSubmit(trainType);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取培训类型列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_TrainType> GetTrainTypeList()
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Base_TrainType orderby x.TrainTypeCode select x).ToList();
        }
        
        /// <summary>
        /// 获取关联人员发卡的培训类型列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_TrainType> GetIsAboutSendCardTrainTypeList()
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Base_TrainType where x.IsAboutSendCard == true orderby x.TrainTypeCode select x).ToList();
        }

        #region 培训类型下拉框
        /// <summary>
        /// 培训类型下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitTrainTypeDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "TrainTypeId";
            dropName.DataTextField = "TrainTypeName";
            dropName.DataSource = GetTrainTypeList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion

        #region 培训类型题型
        /// <summary>
        /// 根据主键获取培训类型题型数量
        /// </summary>
        /// <param name="trainTypeItemId"></param>
        /// <returns></returns>
        public static Model.Base_TrainTypeItem GetTrainTypeItemById(string trainTypeItemId)
        {
            return new Model.SGGLDB(Funs.ConnString).Base_TrainTypeItem.FirstOrDefault(e => e.TrainTypeItemId == trainTypeItemId);
        }
        /// <summary>
        /// 获取培训类型题型数量列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_TrainTypeItem> GetTrainTypeItemList()
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Base_TrainTypeItem select x).ToList();
        }
        /// <summary>
        /// 添加培训类型题型数量
        /// </summary>
        /// <param name="getTrainTypeItem"></param>
        public static void AddTrainTypeItem(Model.Base_TrainTypeItem getTrainTypeItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_TrainTypeItem newTrainTypeItem = new Model.Base_TrainTypeItem
            {
                TrainTypeItemId = SQLHelper.GetNewID(),
                TrainTypeId = getTrainTypeItem.TrainTypeId,
                TrainingId = getTrainTypeItem.TrainingId,
                SCount = getTrainTypeItem.SCount,
                MCount = getTrainTypeItem.MCount,
                JCount = getTrainTypeItem.JCount
            };
            db.Base_TrainTypeItem.InsertOnSubmit(newTrainTypeItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据主键删除培训类型
        /// </summary>
        /// <param name="trainTypeId"></param>
        public static void DeleteTrainTypeItemById(string trainTypeItemId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_TrainTypeItem delTrainTypeItem = db.Base_TrainTypeItem.FirstOrDefault(e => e.TrainTypeItemId == trainTypeItemId);
            if (delTrainTypeItem != null)
            {
                db.Base_TrainTypeItem.DeleteOnSubmit(delTrainTypeItem);
                db.SubmitChanges();
            }
        }
        #endregion
    }
}
