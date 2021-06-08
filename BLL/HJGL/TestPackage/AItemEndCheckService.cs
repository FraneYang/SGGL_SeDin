using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public class AItemEndCheckService
    {
        /// <summary>
        /// 根据主键Id获取用于A项信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static Model.PTP_ItemEndCheck GetAItemEndCheckByID(string id)
        {
            return Funs.DB.PTP_ItemEndCheck.FirstOrDefault(x=>x.ItemCheckId == id);
        }
        /// <summary>
        /// 根据试压包主键Id获取尾项检查信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static List<Model.PTP_ItemEndCheck> GetItemEndCheckByItemEndCheckListId(string ItemEndCheckListId)
        {
            return (from x in Funs.DB.PTP_ItemEndCheck
                   where x.ItemEndCheckListId == ItemEndCheckListId
                    select x).ToList();
        }

        /// <summary>
        /// 根据管线Id获取用于A项信息
        /// </summary>
        /// <param name="pipelineId"></param>
        /// <returns></returns>
        public static List<Model.PTP_ItemEndCheck> GetAItemEndCheckBypipelineId(string pipelineId)
        {
            var view = from x in Funs.DB.PTP_ItemEndCheck
                       where x.PipelineId == pipelineId
                       select x;
            return view.ToList();
        } 
        
          /// <summary>
        /// 增加业务_A项尾工检查表
        /// </summary>
        /// <param name="aItemEndCheck">试压实体</param>
        public static void AddAItemEndCheck(Model.PTP_ItemEndCheck aItemEndCheck)
        {
            Model.SGGLDB db = Funs.DB;
            Model.PTP_ItemEndCheck newAItemEndCheck = new Model.PTP_ItemEndCheck();
            newAItemEndCheck.ItemCheckId = SQLHelper.GetNewID(typeof(Model.PTP_ItemEndCheck));
            newAItemEndCheck.PipelineId = aItemEndCheck.PipelineId;
            newAItemEndCheck.ItemEndCheckListId = aItemEndCheck.ItemEndCheckListId;
            newAItemEndCheck.Content = aItemEndCheck.Content;
            newAItemEndCheck.ItemType = aItemEndCheck.ItemType;
            newAItemEndCheck.Result = aItemEndCheck.Result;
            newAItemEndCheck.Remark = aItemEndCheck.Remark;
            db.PTP_ItemEndCheck.InsertOnSubmit(newAItemEndCheck);
            db.SubmitChanges();
        }

        public static void AddAItemEndCheckForApi(Model.PTP_ItemEndCheck aItemEndCheck)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.PTP_ItemEndCheck newAItemEndCheck = new Model.PTP_ItemEndCheck();
                newAItemEndCheck.ItemCheckId = SQLHelper.GetNewID(typeof(Model.PTP_ItemEndCheck));
                newAItemEndCheck.PipelineId = aItemEndCheck.PipelineId;
                newAItemEndCheck.ItemEndCheckListId = aItemEndCheck.ItemEndCheckListId;
                newAItemEndCheck.Content = aItemEndCheck.Content;
                newAItemEndCheck.ItemType = aItemEndCheck.ItemType;
                newAItemEndCheck.Result = aItemEndCheck.Result;
                newAItemEndCheck.Remark = aItemEndCheck.Remark;
                db.PTP_ItemEndCheck.InsertOnSubmit(newAItemEndCheck);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 修改业务_A项尾工检查表
        /// </summary>
        /// <param name="weldReport">试压实体</param>
        public static void UpdateAItemEndCheck(Model.PTP_ItemEndCheck aItemEndCheck)
        {
            Model.SGGLDB db = Funs.DB;
            Model.PTP_ItemEndCheck newAItemEndCheck = db.PTP_ItemEndCheck.First(e => e.ItemCheckId == aItemEndCheck.ItemCheckId);
            newAItemEndCheck.PipelineId = aItemEndCheck.PipelineId;
            newAItemEndCheck.ItemEndCheckListId = aItemEndCheck.ItemEndCheckListId;
            newAItemEndCheck.ItemType = aItemEndCheck.ItemType;
            newAItemEndCheck.Result = aItemEndCheck.Result;
            newAItemEndCheck.Remark = aItemEndCheck.Remark;
            db.SubmitChanges();
        }

        public static void UpdateAItemEndCheckForApi(Model.PTP_ItemEndCheck aItemEndCheck)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.PTP_ItemEndCheck newAItemEndCheck = db.PTP_ItemEndCheck.First(e => e.ItemCheckId == aItemEndCheck.ItemCheckId);
                if (!string.IsNullOrEmpty(aItemEndCheck.PipelineId))
                    newAItemEndCheck.PipelineId = aItemEndCheck.PipelineId;
                if (!string.IsNullOrEmpty(aItemEndCheck.ItemEndCheckListId))
                    newAItemEndCheck.ItemEndCheckListId = aItemEndCheck.ItemEndCheckListId;
                if (!string.IsNullOrEmpty(aItemEndCheck.ItemType))
                    newAItemEndCheck.ItemType = aItemEndCheck.ItemType;
                if (!string.IsNullOrEmpty(aItemEndCheck.Result))
                    newAItemEndCheck.Result = aItemEndCheck.Result;
                if (!string.IsNullOrEmpty(aItemEndCheck.Remark))
                    newAItemEndCheck.Remark = aItemEndCheck.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据试压包主键删除业务_A项尾工检查表
        /// </summary>
        /// <param name="id">业务_A项尾工检查表主键</param>
        public static void DeleteAItemEndCheckByID(string ItemCheckId)
        {
            Model.SGGLDB db = Funs.DB;
            var ItemCheck = db.PTP_ItemEndCheck.FirstOrDefault(e => e.ItemCheckId == ItemCheckId);
            db.PTP_ItemEndCheck.DeleteOnSubmit(ItemCheck);
            db.SubmitChanges();
        }
        public static void DeleteAllItemEndCheckByID(string ItemEndCheckListId)
        {
            Model.SGGLDB db = Funs.DB;
            var ItemCheck = from x in db.PTP_ItemEndCheck where x.ItemEndCheckListId == ItemEndCheckListId select x;
            if (ItemCheck != null)
            {
                db.PTP_ItemEndCheck.DeleteAllOnSubmit(ItemCheck);
                db.SubmitChanges();
            }
        }
        /// <summary>
        /// 根据管线Id判断是否存在A项尾工
        /// </summary>
        /// <param name="isono"></param>
        /// <returns></returns>
        public static bool IsExistAItemEndCheck(string pipelineId)
        {
            var q = from x in Funs.DB.PTP_ItemEndCheck where x.PipelineId == pipelineId select x;
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
