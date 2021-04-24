using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// 附件1
    /// </summary>
    public static class AttachUrl1Service
    {
        /// <summary>
        /// 根据Id获取附件1内容
        /// </summary>
        /// <param name="attachUrlItemId"></param>
        /// <returns></returns>
        public static Model.PHTGL_AttachUrl1 GetAttachUrl1ById(string attachUrlId)
        {
            return Funs.DB.PHTGL_AttachUrl1.FirstOrDefault(e => e.AttachUrlId == attachUrlId);
        }

        /// <summary>
        /// 增加附件1
        /// </summary>
        /// <param name="url1"></param>
        public static void AddAttachUrl1(Model.PHTGL_AttachUrl1 url1)
        {
            Model.PHTGL_AttachUrl1 newUrl = new Model.PHTGL_AttachUrl1();
            newUrl.AttachUrlItemId = url1.AttachUrlItemId;
            newUrl.AttachUrlId = url1.AttachUrlId;
            newUrl.AttachUrlContent = url1.AttachUrlContent;
            Funs.DB.PHTGL_AttachUrl1.InsertOnSubmit(newUrl);
            Funs.DB.SubmitChanges();
        }

        /// <summary>
        /// 修改附件1
        /// </summary>
        /// <param name="url1"></param>
        public static void UpdateAttachUrl1(Model.PHTGL_AttachUrl1 url1)
        {
            Model.PHTGL_AttachUrl1 newUrl = Funs.DB.PHTGL_AttachUrl1.FirstOrDefault(e => e.AttachUrlId == url1.AttachUrlId);
            if (newUrl != null)
            {
                newUrl.AttachUrlContent = url1.AttachUrlContent;
                Funs.DB.SubmitChanges();
            }
        }
    }
}
