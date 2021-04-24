using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// 施工分包商组织机构人员配置表
    /// </summary>
    public class AttachUrl9_SubStaffingService
    {
        /// <summary>
        /// 根据主键获取施工分包商组织机构人员配置表
        /// </summary>
        /// <param name="attachUrlItemId"></param>
        /// <returns></returns>
        public static Model.PHTGL_AttachUrl9_SubStaffing GetSubStaffingByAttachUrlItemId(string attachUrlItemId)
        {
            return Funs.DB.PHTGL_AttachUrl9_SubStaffing.FirstOrDefault(e => e.AttachUrlItemId == attachUrlItemId);
        }

        /// <summary>
        /// 根据附件ID获取施工分包商组织机构人员配置表列表
        /// </summary>
        /// <param name="attachUrlId"></param>
        /// <returns></returns>
        public static List<Model.PHTGL_AttachUrl9_SubStaffing> GetSubStaffingByAttachUrlId(string attachUrlId)
        {
            return (from x in Funs.DB.PHTGL_AttachUrl9_SubStaffing where x.AttachUrlId == attachUrlId select x).ToList();
        }

        /// <summary>
        /// 添加施工分包商组织机构人员配置表
        /// </summary>
        /// <param name="subStaffing"></param>
        public static void AddSubStaffing(Model.PHTGL_AttachUrl9_SubStaffing subStaffing)
        {
            Model.PHTGL_AttachUrl9_SubStaffing newSubStaffing = new Model.PHTGL_AttachUrl9_SubStaffing();
            newSubStaffing.AttachUrlItemId = subStaffing.AttachUrlItemId;
            newSubStaffing.AttachUrlId = subStaffing.AttachUrlId;
            newSubStaffing.WorkPostName = subStaffing.WorkPostName;
            newSubStaffing.Number = subStaffing.Number;
            newSubStaffing.Arrivaltime = subStaffing.Arrivaltime;
            newSubStaffing.Remarks = subStaffing.Remarks;
            Funs.DB.PHTGL_AttachUrl9_SubStaffing.InsertOnSubmit(newSubStaffing);
            Funs.DB.SubmitChanges();
        }

        /// <summary>
        /// 根据附件Id删除施工分包商组织机构人员配置表列表
        /// </summary>
        /// <param name="attachUrlId"></param>
        public static void DeleteSubStaffingByAttachUrlId(string attachUrlId)
        {
            var q = (from x in Funs.DB.PHTGL_AttachUrl9_SubStaffing where x.AttachUrlId == attachUrlId select x).ToList();
            if (q != null)
            {
                Funs.DB.PHTGL_AttachUrl9_SubStaffing.DeleteAllOnSubmit(q);
                Funs.DB.SubmitChanges();
            }
        }
    }
}
