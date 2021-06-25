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
    public static class AttachUrl5Service
    {
        public static Model.PHTGL_AttachUrl5 GetAttachUrl5ByAttachUrlId(string attachUrlId)
        {
            return Funs.DB.PHTGL_AttachUrl5.FirstOrDefault(e => e.AttachUrlId == attachUrlId);
        }

        /// <summary>
        /// 增加附件5
        /// </summary>
        /// <param name="url1"></param>
        public static void AddAttachUrl5(Model.PHTGL_AttachUrl5 Url5)
        {
            Model.PHTGL_AttachUrl5 newUrl = new Model.PHTGL_AttachUrl5();
            newUrl.AttachUrlItemId = Url5.AttachUrlItemId;
            newUrl.AttachUrlId = Url5.AttachUrlId;
            newUrl.AttachUrlContent = Url5.AttachUrlContent;
            Funs.DB.PHTGL_AttachUrl5.InsertOnSubmit(newUrl);
            Funs.DB.SubmitChanges();
        }

        /// <summary>
        /// 修改附件5
        /// </summary>
        /// <param name="url1"></param>
        public static void UpdateAttachUrl5(Model.PHTGL_AttachUrl5 Url5)
        {
            Model.PHTGL_AttachUrl5 newUrl = Funs.DB.PHTGL_AttachUrl5.FirstOrDefault(e => e.AttachUrlId == Url5.AttachUrlId);
            if (newUrl != null)
            {
                newUrl.AttachUrlContent = Url5.AttachUrlContent;
                Funs.DB.SubmitChanges();
            }
        }
    }
}
