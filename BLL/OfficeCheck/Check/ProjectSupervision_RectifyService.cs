using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// 隐患整改
    /// </summary>
    public static class ProjectSupervision_RectifyService
    {
        /// <summary>
        /// 根据主键获取隐患整改
        /// </summary>
        /// <param name="rectifyId"></param>
        /// <returns></returns>
        public static Model.ProjectSupervision_Rectify GetRectifyById(string rectifyId)
        {
            return Funs.DB.ProjectSupervision_Rectify.FirstOrDefault(e => e.RectifyId == rectifyId);
        }

        /// <summary>
        /// 根据检查通知Id获取隐患整改
        /// </summary>
        /// <param name="checkNoticeId"></param>
        /// <returns></returns>
        public static Model.ProjectSupervision_Rectify GetRectifyByCheckNoticeId(string checkNoticeId)
        {
            return Funs.DB.ProjectSupervision_Rectify.FirstOrDefault(e => e.CheckNoticeId == checkNoticeId);
        }

        /// <summary>
        /// 添加隐患整改
        /// </summary>
        /// <param name="rectify"></param>
        public static void AddRectify(Model.ProjectSupervision_Rectify rectify)
        {
            Model.ProjectSupervision_Rectify newRectify = new Model.ProjectSupervision_Rectify();
            newRectify.RectifyId = rectify.RectifyId;
            newRectify.RectifyCode = rectify.RectifyCode;
            newRectify.ProjectId = rectify.ProjectId;
            newRectify.CheckManIds = rectify.CheckManIds;
            newRectify.CheckManNames = rectify.CheckManNames;
            newRectify.CheckedDate = rectify.CheckedDate;
            newRectify.HiddenHazardType = rectify.HiddenHazardType;
            newRectify.SignPerson = rectify.SignPerson;
            newRectify.CheckNoticeId = rectify.CheckNoticeId;
            newRectify.States = rectify.States;
            Funs.DB.ProjectSupervision_Rectify.InsertOnSubmit(newRectify);
            Funs.DB.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="rectify"></param>
        public static void UpdateRectify(Model.ProjectSupervision_Rectify rectify)
        {
            Model.ProjectSupervision_Rectify newRectify = Funs.DB.ProjectSupervision_Rectify.FirstOrDefault(e => e.RectifyId == rectify.RectifyId);
            if (newRectify != null)
            {
                newRectify.RectifyCode = rectify.RectifyCode;
                newRectify.ProjectId = rectify.ProjectId;
                newRectify.CheckManIds = rectify.CheckManIds;
                newRectify.CheckManNames = rectify.CheckManNames;
                newRectify.CheckedDate = rectify.CheckedDate;
                newRectify.HiddenHazardType = rectify.HiddenHazardType;
                newRectify.SignPerson = rectify.SignPerson;
                newRectify.States = rectify.States;
                Funs.DB.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据检查通知Id获取隐患整改信息
        /// </summary>
        /// <param name="checkNoticeId"></param>
        public static void DeleteRectifyByCheckNoticeId(string checkNoticeId)
        {
            Model.ProjectSupervision_Rectify newRectify = Funs.DB.ProjectSupervision_Rectify.FirstOrDefault(e => e.CheckNoticeId == checkNoticeId);
            if (newRectify!=null)
            {
                Funs.DB.ProjectSupervision_Rectify.DeleteOnSubmit(newRectify);
                Funs.DB.SubmitChanges();
            }

        }
    }
}
