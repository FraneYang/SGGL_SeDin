using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class AttachUrlService
    {
        /// <summary>
        /// 根据主键获取附件信息
        /// </summary>
        /// <param name="attachUrlId"></param>
        /// <returns></returns>
        public static Model.PHTGL_AttachUrl GetAttachUrlById(string attachUrlId)
        {
            return Funs.DB.PHTGL_AttachUrl.FirstOrDefault(e => e.AttachUrlId == attachUrlId);
        }

        /// <summary>
        /// 根据专用条款id获取附件信息
        /// </summary>
        /// <param name="attachUrlId"></param>
        /// <returns></returns>
        public static List< Model.PHTGL_AttachUrl> GetAttachUrlBySpecialTermsConditionsId(string specialTermsConditionsId)
        {
            var list = (from x in Funs.DB.PHTGL_AttachUrl
                        where x.SpecialTermsConditionsId == specialTermsConditionsId orderby  Convert.ToInt32( x.AttachUrlCode.Substring(2))
                        select x ).ToList();
            return list;
        }

        /// <summary>
        /// 获取对应专用条款模板的对应附件
        /// </summary>
        /// <param name="attachUrlId"></param>
        /// <returns></returns>
        public static Model.PHTGL_AttachUrl GetAttachUrlByAttachUrlCode(string specialTermsConditionsId,int AttachUrlCode)
        {
           specialTermsConditionsId = "专用条款模板";
            AttachUrlCode = AttachUrlCode - 1;
           var list = (from x in Funs.DB.PHTGL_AttachUrl
                        where x.SpecialTermsConditionsId == specialTermsConditionsId
                        orderby Convert.ToInt32(x.AttachUrlCode.Substring(2))
                        select x).ToList();
            if (AttachUrlCode>list.Count)
            {
                AttachUrlCode = list.Count-1;
            }
            return list[AttachUrlCode];
        }
        public static void AddPHTGL_AttachUrl(Model.PHTGL_AttachUrl newtable)
        {
            Model.PHTGL_AttachUrl table = new Model.PHTGL_AttachUrl();
            table.AttachUrlId = newtable.AttachUrlId;
            table.SpecialTermsConditionsId = newtable.SpecialTermsConditionsId;
            table.AttachUrlCode = newtable.AttachUrlCode;
            table.AttachUrlName = newtable.AttachUrlName;
            table.IsBuild = newtable.IsBuild;
            table.IsSelected = newtable.IsSelected;
            table.SortIndex = newtable.SortIndex;
            Funs.DB.PHTGL_AttachUrl.InsertOnSubmit(table);
            Funs.DB.SubmitChanges();
        }


        /// <summary>
        /// 修改附件信息
        /// </summary>
        /// <param name="attachUrl"></param>
        public static void UpdateAttachUrl(Model.PHTGL_AttachUrl attachUrl)
        {
            Model.PHTGL_AttachUrl newAttachUrl = Funs.DB.PHTGL_AttachUrl.FirstOrDefault(e => e.AttachUrlId == attachUrl.AttachUrlId);
            if (newAttachUrl != null)
            {
                newAttachUrl.SpecialTermsConditionsId = attachUrl.SpecialTermsConditionsId;
                newAttachUrl.IsSelected = attachUrl.IsSelected;
                Funs.DB.SubmitChanges();
            }
        }
    }
}