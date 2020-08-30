﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LV_ItemEndCheckService
    {
        /// <summary>
        /// 根据主键Id获取用于A项信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static Model.HJGL_LV_ItemEndCheck GetAItemEndCheckByID(string id)
        {
            return Funs.DB.HJGL_LV_ItemEndCheck.FirstOrDefault(x => x.LVItemEndCheckId == id);
        }

        /// <summary>
        /// 根据明细主键获取用于A项信息
        /// </summary>
        /// <param name="LV_PipeId"></param>
        /// <returns></returns>
        public static List<Model.HJGL_LV_ItemEndCheck> GetAItemEndCheckByLV_PipeId(string LV_PipeId)
        {
            var view = from x in Funs.DB.HJGL_LV_ItemEndCheck
                       where x.LV_PipeId == LV_PipeId
                       orderby x.CheckDate
                       select x;
            return view.ToList();
        }

        /// <summary>
        /// 增加业务_A项尾工检查表
        /// </summary>
        /// <param name="aItemEndCheck">试压实体</param>
        public static void AddAItemEndCheck(Model.HJGL_LV_ItemEndCheck aItemEndCheck)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_LV_ItemEndCheck newAItemEndCheck = new Model.HJGL_LV_ItemEndCheck();
            newAItemEndCheck.LVItemEndCheckId = SQLHelper.GetNewID(typeof(Model.HJGL_LV_ItemEndCheck));
            newAItemEndCheck.LV_PipeId = aItemEndCheck.LV_PipeId;
            newAItemEndCheck.CheckMan = aItemEndCheck.CheckMan;
            newAItemEndCheck.CheckDate = aItemEndCheck.CheckDate;
            newAItemEndCheck.DealMan = aItemEndCheck.DealMan;
            newAItemEndCheck.DealDate = aItemEndCheck.DealDate;
            newAItemEndCheck.Remark = aItemEndCheck.Remark;
            newAItemEndCheck.ItemType = aItemEndCheck.ItemType;
            newAItemEndCheck.Opinion = aItemEndCheck.Opinion;
            db.HJGL_LV_ItemEndCheck.InsertOnSubmit(newAItemEndCheck);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改业务_A项尾工检查表
        /// </summary>
        /// <param name="weldReport">试压实体</param>
        public static void UpdateAItemEndCheck(Model.HJGL_LV_ItemEndCheck aItemEndCheck)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_LV_ItemEndCheck newAItemEndCheck = db.HJGL_LV_ItemEndCheck.First(e => e.LVItemEndCheckId == aItemEndCheck.LVItemEndCheckId);
            newAItemEndCheck.LV_PipeId = aItemEndCheck.LV_PipeId;
            newAItemEndCheck.CheckMan = aItemEndCheck.CheckMan;
            newAItemEndCheck.CheckDate = aItemEndCheck.CheckDate;
            newAItemEndCheck.DealMan = aItemEndCheck.DealMan;
            newAItemEndCheck.DealDate = aItemEndCheck.DealDate;
            newAItemEndCheck.Remark = aItemEndCheck.Remark;
            newAItemEndCheck.ItemType = aItemEndCheck.ItemType;
            newAItemEndCheck.Opinion = aItemEndCheck.Opinion;
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据主键删除业务_A项尾工检查表
        /// </summary>
        /// <param name="id">业务_A项尾工检查表主键</param>
        public static void DeleteAItemEndCheckByID(string LVItemEndCheckId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_LV_ItemEndCheck newAItemEndCheck = db.HJGL_LV_ItemEndCheck.First(e => e.LVItemEndCheckId == LVItemEndCheckId);
            db.HJGL_LV_ItemEndCheck.DeleteOnSubmit(newAItemEndCheck);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据管线Id判断是否存在A项尾工
        /// </summary>
        /// <param name="isono"></param>
        /// <returns></returns>
        public static bool IsExistAItemEndCheck(string LV_PipeId)
        {
            var q = from x in Funs.DB.HJGL_LV_ItemEndCheck where x.LV_PipeId == LV_PipeId select x;
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