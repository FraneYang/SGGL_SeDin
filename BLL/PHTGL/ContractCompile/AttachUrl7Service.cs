using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// 附件7
    /// </summary>
    public static class AttachUrl7Service
    {
        /// <summary>
        /// 根据Id获取附件7内容
        /// </summary>
        /// <param name="attachUrlItemId"></param>
        /// <returns></returns>
        public static Model.PHTGL_AttachUrl7 GetAttachUrl7ById(string attachUrlId)
        {
            return Funs.DB.PHTGL_AttachUrl7.FirstOrDefault(e => e.AttachUrlId == attachUrlId);
        }

        /// <summary>
        /// 增加附件7
        /// </summary>
        /// <param name="Url7"></param>
        public static void AddAttachUrl7(Model.PHTGL_AttachUrl7 Url7)
        {
            Model.PHTGL_AttachUrl7 newUrl = new Model.PHTGL_AttachUrl7();
            newUrl.AttachUrlItemId = Url7.AttachUrlItemId;
            newUrl.AttachUrlId = Url7.AttachUrlId;
            newUrl.AttachUrlContent = Url7.AttachUrlContent;
            Funs.DB.PHTGL_AttachUrl7.InsertOnSubmit(newUrl);
            Funs.DB.SubmitChanges();
        }

        /// <summary>
        /// 修改附件7
        /// </summary>
        /// <param name="Url7"></param>
        public static void UpdateAttachUrl7(Model.PHTGL_AttachUrl7 Url7)
        {
            Model.PHTGL_AttachUrl7 newUrl = Funs.DB.PHTGL_AttachUrl7.FirstOrDefault(e => e.AttachUrlId == Url7.AttachUrlId);
            if (newUrl != null)
            {
                newUrl.AttachUrlContent = Url7.AttachUrlContent;
                Funs.DB.SubmitChanges();
            }
        }
    }
}
