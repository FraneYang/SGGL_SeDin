using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    ///  
    /// </summary>
    public static class AttachUrl20Service
    {
        public static Model.PHTGL_AttachUrl20 GetAttachUrl20ByAttachUrlId(string attachUrlId)
        {
            return Funs.DB.PHTGL_AttachUrl20.FirstOrDefault(e => e.AttachUrlId == attachUrlId);
        }

        /// <summary>
        /// 增加附件20
        /// </summary>
        /// <param name="url1"></param>
        public static void AddAttachUrl20(Model.PHTGL_AttachUrl20 Url20)
        {
            Model.PHTGL_AttachUrl20 newUrl = new Model.PHTGL_AttachUrl20();
            newUrl.AttachUrlItemId = Url20.AttachUrlItemId;
            newUrl.AttachUrlId = Url20.AttachUrlId;
            newUrl.AttachUrlContent = Url20.AttachUrlContent;
            Funs.DB.PHTGL_AttachUrl20.InsertOnSubmit(newUrl);
            Funs.DB.SubmitChanges();
        }

        /// <summary>
        /// 修改附件20
        /// </summary>
        /// <param name="url1"></param>
        public static void UpdateAttachUrl20(Model.PHTGL_AttachUrl20 Url20)
        {
            Model.PHTGL_AttachUrl20 newUrl = Funs.DB.PHTGL_AttachUrl20.FirstOrDefault(e => e.AttachUrlId == Url20.AttachUrlId);
            if (newUrl != null)
            {
                newUrl.AttachUrlContent = Url20.AttachUrlContent;
                Funs.DB.SubmitChanges();
            }
        }
    }
}
