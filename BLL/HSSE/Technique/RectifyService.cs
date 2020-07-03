using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 安全隐患主表
    /// </summary>
    public static class RectifyService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取安全隐患
        /// </summary>
        /// <param name="rectifyId"></param>
        /// <returns></returns>
        public static Model.Technique_Rectify GetRectifyById(string rectifyId)
        {
            return Funs.DB.Technique_Rectify.FirstOrDefault(e => e.RectifyId == rectifyId);
        }

        /// <summary>
        /// 根据上一节点id获取安全隐患
        /// </summary>
        /// <param name="supRectifyId"></param>
        /// <returns></returns>
        public static List<Model.Technique_Rectify> GetRectifyBySupRectifyId(string supRectifyId)
        {
            return (from x in Funs.DB.Technique_Rectify where x.SupRectifyId == supRectifyId select x).ToList();
        }

        /// <summary>
        /// 添加安全隐患
        /// </summary>
        /// <param name="rectify"></param>
        public static void AddRectify(Model.Technique_Rectify rectify)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Technique_Rectify newRectify = new Model.Technique_Rectify
            {
                RectifyId = rectify.RectifyId,
                RectifyCode = rectify.RectifyCode,
                RectifyName = rectify.RectifyName,
                SupRectifyId = rectify.SupRectifyId,
                IsEndLever = rectify.IsEndLever
            };
            newRectify.UpState = newRectify.UpState;
            db.Technique_Rectify.InsertOnSubmit(newRectify);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改安全隐患
        /// </summary>
        /// <param name="rectify"></param>
        public static void UpdateRectify(Model.Technique_Rectify rectify)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Technique_Rectify newRectify = db.Technique_Rectify.FirstOrDefault(e => e.RectifyId == rectify.RectifyId);
            if (newRectify != null)
            {
                newRectify.RectifyCode = rectify.RectifyCode;
                newRectify.RectifyName = rectify.RectifyName;
                newRectify.SupRectifyId = rectify.SupRectifyId;
                newRectify.IsEndLever = rectify.IsEndLever;
                newRectify.UpState = newRectify.UpState;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除安全隐患
        /// </summary>
        /// <param name="rectifyId"></param>
        public static void DeleteRectify(string rectifyId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Technique_Rectify rectify = db.Technique_Rectify.FirstOrDefault(e => e.RectifyId == rectifyId);
            if (rectify != null)
            {
                db.Technique_Rectify.DeleteOnSubmit(rectify);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 是否可删除资源节点
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-可以，false-不可以</returns>
        public static bool IsDeleteRectify(string rectifyId)
        {
            bool isDelete = true;
            var rectify = BLL.RectifyService.GetRectifyById(rectifyId);
            if (rectify != null)
            {
                if (rectify.IsBuild == true)
                {
                    isDelete = false;
                }
                if (rectify.IsEndLever == true)
                {
                    var detailCout = Funs.DB.Technique_RectifyItem.FirstOrDefault(x => x.RectifyId == rectifyId);
                    if (detailCout != null)
                    {
                        isDelete = false;
                    }
                }
                else
                {
                    var supItemSetCount = BLL.RectifyService.GetRectifyBySupRectifyId(rectifyId);
                    if (supItemSetCount.Count() > 0)
                    {
                        isDelete = false;
                    }
                }
            }
            return isDelete;
        }
    }
}
