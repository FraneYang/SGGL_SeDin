using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 热处理明细
    /// </summary>
    public static class HotProessTrustItemService
    {
        /// <summary>
        /// 根据主键获取热处理明细
        /// </summary>
        /// <param name="hotProessTrustItemId"></param>
        /// <returns></returns>
        public static Model.HJGL_HotProess_TrustItem GetHotProessTrustItemById(string hotProessTrustItemId)
        {
            return new Model.SGGLDB(Funs.ConnString).HJGL_HotProess_TrustItem.FirstOrDefault(e => e.HotProessTrustItemId == hotProessTrustItemId);
        }

        /// <summary>
        /// 根据热处理Id获取相关明细信息
        /// </summary>
        /// <param name="hotProessTrustId"></param>
        /// <returns></returns>
        public static List<Model.HJGL_HotProess_TrustItem> GetHotProessTrustItemByHotProessTrustId(string hotProessTrustId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).HJGL_HotProess_TrustItem where x.HotProessTrustId == hotProessTrustId select x).ToList();
        }

        /// <summary>
        /// 添加热处理明细
        /// </summary>
        /// <param name="hotProessTrustItem"></param>
        public static void AddHotProessTrustItem(Model.HJGL_HotProess_TrustItem hotProessTrustItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.HJGL_HotProess_TrustItem newHotProessTrustItem = new Model.HJGL_HotProess_TrustItem();
            newHotProessTrustItem.HotProessTrustItemId = SQLHelper.GetNewID(typeof(Model.HJGL_HotProess_TrustItem));
            newHotProessTrustItem.HotProessTrustId = hotProessTrustItem.HotProessTrustId;
            newHotProessTrustItem.WeldJointId = hotProessTrustItem.WeldJointId;
            newHotProessTrustItem.IsPass = hotProessTrustItem.IsPass;
            newHotProessTrustItem.IsHardness = hotProessTrustItem.IsHardness;
            newHotProessTrustItem.IsTrust = hotProessTrustItem.IsTrust;
            newHotProessTrustItem.HardTrustItemID = hotProessTrustItem.HardTrustItemID;
            db.HJGL_HotProess_TrustItem.InsertOnSubmit(newHotProessTrustItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改热处理
        /// </summary>
        /// <param name="hotProessTrustItem"></param>
        public static void UpdateHotProessTrustItem(Model.HJGL_HotProess_TrustItem hotProessTrustItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.HJGL_HotProess_TrustItem newHotProessTrustItem = db.HJGL_HotProess_TrustItem.FirstOrDefault(e => e.HotProessTrustItemId == hotProessTrustItem.HotProessTrustItemId);
            if (newHotProessTrustItem != null)
            {
                newHotProessTrustItem.HotProessTrustId = hotProessTrustItem.HotProessTrustId;
                newHotProessTrustItem.WeldJointId = hotProessTrustItem.WeldJointId;
                newHotProessTrustItem.IsPass = hotProessTrustItem.IsPass;
                newHotProessTrustItem.IsHardness = hotProessTrustItem.IsHardness;
                newHotProessTrustItem.IsTrust = hotProessTrustItem.IsTrust;
                newHotProessTrustItem.HardTrustItemID = hotProessTrustItem.HardTrustItemID;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 修改热处理反馈及硬度委托信息
        /// </summary>
        /// <param name="hotProessTrustItem"></param>
        public static void UpdateHotProessFeedback(Model.HJGL_HotProess_TrustItem hotProessTrustItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.HJGL_HotProess_TrustItem newHotProessTrustItem = db.HJGL_HotProess_TrustItem.FirstOrDefault(e => e.HotProessTrustItemId == hotProessTrustItem.HotProessTrustItemId);
            if (newHotProessTrustItem != null)
            {
                newHotProessTrustItem.IsCompleted = hotProessTrustItem.IsCompleted;
                newHotProessTrustItem.IsHardness = hotProessTrustItem.IsHardness;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据热处理主键删除相关明细信息
        /// </summary>
        /// <param name="hotProessTrustId"></param>
        public static void DeleteHotProessTrustItemById(string hotProessTrustId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var hotProessTrustItem = (from x in db.HJGL_HotProess_TrustItem where x.HotProessTrustId == hotProessTrustId select x).ToList();
            if (hotProessTrustItem != null)
            {
                db.HJGL_HotProess_TrustItem.DeleteAllOnSubmit(hotProessTrustItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据项目ID、热处理Id获取相关明细视图信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="hotProessTrustId"></param>
        /// <returns></returns>
        public static List<Model.View_HJGL_HotProess_TrustItem> GetViewHotProessTrustItem(string projectId, string hotProessTrustId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).View_HJGL_HotProess_TrustItem where x.ProjectId == projectId && x.HotProessTrustId == hotProessTrustId select x).ToList();
        }


    }
}