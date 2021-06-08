using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// 附件3    工程价格清单
    /// </summary>
    public static class AttachUrl3Service
    {
        public static Model.PHTGL_AttachUrl3 GetAttachUrl3ByAttachUrlId(string attachUrlId)
        {
            return Funs.DB.PHTGL_AttachUrl3.FirstOrDefault(e => e.AttachUrlId == attachUrlId);
        }

        /// <summary>
        /// 增加附件3
        /// </summary>
        /// <param name="url1"></param>
        public static void AddAttachUrl3(Model.PHTGL_AttachUrl3 url3)
        {
            Model.PHTGL_AttachUrl3 newUrl = new Model.PHTGL_AttachUrl3();
            newUrl.AttachUrlItemId = url3.AttachUrlItemId;
            newUrl.AttachUrlId = url3.AttachUrlId;
            newUrl.AttachUrlContent = url3.AttachUrlContent;
            Funs.DB.PHTGL_AttachUrl3.InsertOnSubmit(newUrl);
            Funs.DB.SubmitChanges();
        }

        /// <summary>
        /// 修改附件3
        /// </summary>
        /// <param name="url1"></param>
        public static void UpdateAttachUrl3(Model.PHTGL_AttachUrl3 url3)
        {
            Model.PHTGL_AttachUrl3 newUrl = Funs.DB.PHTGL_AttachUrl3.FirstOrDefault(e => e.AttachUrlId == url3.AttachUrlId);
            if (newUrl != null)
            {
                newUrl.AttachUrlContent = url3.AttachUrlContent;
                Funs.DB.SubmitChanges();
            }
        }
    }
}
