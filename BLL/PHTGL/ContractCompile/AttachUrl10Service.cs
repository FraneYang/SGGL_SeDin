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
    public static class AttachUrl10Service
    {
        public static Model.PHTGL_AttachUrl10 GetAttachUrl10ByAttachUrlId(string attachUrlId)
        {
            return Funs.DB.PHTGL_AttachUrl10.FirstOrDefault(e => e.AttachUrlId == attachUrlId);
        }

        /// <summary>
        /// 增加附件10
        /// </summary>
        /// <param name="url1"></param>
        public static void AddAttachUrl10(Model.PHTGL_AttachUrl10 Url10)
        {
            Model.PHTGL_AttachUrl10 newUrl = new Model.PHTGL_AttachUrl10();
            newUrl.AttachUrlItemId = Url10.AttachUrlItemId;
            newUrl.AttachUrlId = Url10.AttachUrlId;
            newUrl.AttachUrlContent = Url10.AttachUrlContent;
            Funs.DB.PHTGL_AttachUrl10.InsertOnSubmit(newUrl);
            Funs.DB.SubmitChanges();
        }

        /// <summary>
        /// 修改附件10
        /// </summary>
        /// <param name="url1"></param>
        public static void UpdateAttachUrl10(Model.PHTGL_AttachUrl10 Url10)
        {
            Model.PHTGL_AttachUrl10 newUrl = Funs.DB.PHTGL_AttachUrl10.FirstOrDefault(e => e.AttachUrlId == Url10.AttachUrlId);
            if (newUrl != null)
            {
                newUrl.AttachUrlContent = Url10.AttachUrlContent;
                Funs.DB.SubmitChanges();
            }
        }
    }
}
