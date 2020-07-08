using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 硬度明细
    /// </summary>
    public static class Hard_TrustItemService
    {
        /// <summary>
        /// 根据主键获取硬度明细
        /// </summary>
        /// <param name="hardTrustItemId"></param>
        /// <returns></returns>
        public static Model.HJGL_Hard_TrustItem GetHardTrustItemById(string hardTrustItemId)
        {
            return new Model.SGGLDB(Funs.ConnString).HJGL_Hard_TrustItem.FirstOrDefault(e => e.HardTrustItemID == hardTrustItemId);
        }

        /// <summary>
        /// 根据硬度Id获取相关明细信息
        /// </summary>
        /// <param name="hardTrustId"></param>
        /// <returns></returns>
        public static List<Model.HJGL_Hard_TrustItem> GetHardTrustItemByHardTrustId(string hardTrustId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).HJGL_Hard_TrustItem where x.HardTrustID == hardTrustId select x).ToList();
        }

        /// <summary>
        /// 添加硬度明细
        /// </summary>
        /// <param name="hardTrustItem"></param>
        public static void AddHardTrustItem(Model.HJGL_Hard_TrustItem hardTrustItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.HJGL_Hard_TrustItem newHardTrustItem = new Model.HJGL_Hard_TrustItem();
            newHardTrustItem.HardTrustItemID = SQLHelper.GetNewID(typeof(Model.HJGL_Hard_TrustItem));
            newHardTrustItem.HardTrustID = hardTrustItem.HardTrustID;
            newHardTrustItem.HotProessTrustItemId = hardTrustItem.HotProessTrustItemId;
            newHardTrustItem.WeldJointId = hardTrustItem.WeldJointId;
            newHardTrustItem.IsPass = hardTrustItem.IsPass;
            newHardTrustItem.IsTrust = hardTrustItem.IsTrust;
            db.HJGL_Hard_TrustItem.InsertOnSubmit(newHardTrustItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改硬度
        /// </summary>
        /// <param name="hardTrustItem"></param>
        public static void UpdateHardTrustItem(Model.HJGL_Hard_TrustItem hardTrustItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.HJGL_Hard_TrustItem newHardTrustItem = db.HJGL_Hard_TrustItem.FirstOrDefault(e => e.HardTrustItemID == hardTrustItem.HardTrustItemID);
            if (newHardTrustItem != null)
            {
                newHardTrustItem.IsPass = hardTrustItem.IsPass;
                newHardTrustItem.IsTrust = hardTrustItem.IsTrust;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据硬度主键删除相关明细信息
        /// </summary>
        /// <param name="hardTrustId"></param>
        public static void DeleteHardTrustItemById(string hardTrustId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var hardTrustItem = (from x in db.HJGL_Hard_TrustItem where x.HardTrustID == hardTrustId select x).ToList();
            if (hardTrustItem != null)
            {
                db.HJGL_Hard_TrustItem.DeleteAllOnSubmit(hardTrustItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据项目ID、硬度Id获取相关明细视图信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="hardTrustId"></param>
        /// <returns></returns>
        public static List<Model.View_HJGL_Hard_TrustItem> GetViewHardTrustItem(string hardTrustId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).View_HJGL_Hard_TrustItem where x.HardTrustID == hardTrustId select x).ToList();
        }
    }
}
