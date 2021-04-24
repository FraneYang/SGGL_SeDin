using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// 附件6
    /// </summary>
    public static class AttachUrl6Service
    {
        /// <summary>
        /// 根据Id获取附件6内容
        /// </summary>
        /// <param name="attachUrlItemId"></param>
        /// <returns></returns>
        public static Model.PHTGL_AttachUrl6 GetAttachUrl6ById(string attachUrlId)
        {
            return Funs.DB.PHTGL_AttachUrl6.FirstOrDefault(e => e.AttachUrlId == attachUrlId);
        }

        /// <summary>
        /// 增加附件6
        /// </summary>
        /// <param name="url6"></param>
        public static void AddAttachUrl6(Model.PHTGL_AttachUrl6 url6)
        {
            Model.PHTGL_AttachUrl6 newUrl = new Model.PHTGL_AttachUrl6();
            newUrl.AttachUrlItemId = url6.AttachUrlItemId;
            newUrl.AttachUrlId = url6.AttachUrlId;
            newUrl.AttachUrlContent = url6.AttachUrlContent;
            Funs.DB.PHTGL_AttachUrl6.InsertOnSubmit(newUrl);
            Funs.DB.SubmitChanges();
        }

        /// <summary>
        /// 修改附件6
        /// </summary>
        /// <param name="url6"></param>
        public static void UpdateAttachUrl6(Model.PHTGL_AttachUrl6 url6)
        {
            Model.PHTGL_AttachUrl6 newUrl = Funs.DB.PHTGL_AttachUrl6.FirstOrDefault(e => e.AttachUrlId == url6.AttachUrlId);
            if (newUrl != null)
            {
                newUrl.AttachUrlContent = url6.AttachUrlContent;
                Funs.DB.SubmitChanges();
            }
        }
    }
}
