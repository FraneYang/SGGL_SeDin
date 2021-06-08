using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// 附件1
    /// </summary>
    public static class AttachUrl4Service
    {
        /// <summary>
        /// 根据Id获取附件1内容
        /// </summary>
        /// <param name="attachUrlItemId"></param>
        /// <returns></returns>
        public static Model.PHTGL_AttachUrl4 GetAttachurl4ByItemId(string achUrlItemId)
        {
            return Funs.DB.PHTGL_AttachUrl4.FirstOrDefault(e => e.AttachUrlItemId == achUrlItemId);
        }

        public static Model.PHTGL_AttachUrl4 GetAttachurl4ById(string AttachUrlId)
        {
            return Funs.DB.PHTGL_AttachUrl4.FirstOrDefault(e => e.AttachUrlId == AttachUrlId);
        }
        public static List<Model.PHTGL_AttachUrl4> GetListAttachurl4ById(string AttachUrlId)
        {
            var q = (from x in Funs.DB.PHTGL_AttachUrl4 orderby x.OrderNumber where x.AttachUrlId == AttachUrlId select x).ToList();
            return q;
        }
        /// <summary>
        /// 增加附件1
        /// </summary>
        /// <param name="url4"></param>
        public static void AddAttachurl4(Model.PHTGL_AttachUrl4 url4)
        {
            Model.PHTGL_AttachUrl4 newUrl = new Model.PHTGL_AttachUrl4();
            newUrl.AttachUrlItemId = url4.AttachUrlItemId;
            newUrl.AttachUrlId = url4.AttachUrlId;
            newUrl.OrderNumber = url4.OrderNumber;
            newUrl.Describe = url4.Describe;
            newUrl.Duty_Gen = url4.Duty_Gen;
            newUrl.Duty_Sub = url4.Duty_Sub;
            newUrl.Remarks = url4.Remarks;
            Funs.DB.PHTGL_AttachUrl4.InsertOnSubmit(newUrl);
            Funs.DB.SubmitChanges();
        }

        /// <summary>
        /// 修改合同协议书
        /// </summary>
        /// <param name="sub"></param>
        public static void UpdateAttachurl4(Model.PHTGL_AttachUrl4 url4)
        {
            Model.PHTGL_AttachUrl4 newUrl = Funs.DB.PHTGL_AttachUrl4.FirstOrDefault(e => e.AttachUrlItemId == url4.AttachUrlItemId);
            if (newUrl != null)
            {
                newUrl.AttachUrlItemId = url4.AttachUrlItemId;
                newUrl.AttachUrlId = url4.AttachUrlId;
                newUrl.OrderNumber = url4.OrderNumber;
                newUrl.Describe = url4.Describe;
                newUrl.Duty_Gen = url4.Duty_Gen;
                newUrl.Duty_Sub = url4.Duty_Sub;
                newUrl.Remarks = url4.Remarks;
                try
                {
                    Funs.DB.SubmitChanges();
                }
                catch (System.Data.Linq.ChangeConflictException ex)
                {
                    Funs.DB.ChangeConflicts.ResolveAll(RefreshMode.KeepCurrentValues);  //保持当前的值
                    Funs.DB.ChangeConflicts.ResolveAll(RefreshMode.OverwriteCurrentValues);//保持原来的更新,放弃了当前的值.
                    Funs.DB.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges);//保存原来的值 有冲突的话保存当前版本


                    Funs.DB.SubmitChanges();
                }
            }
        }

        public static void deletePHTGL_AttachUrl14byAttachUrlId(string AttachUrlId)
        {
            var q = (from x in Funs.DB.PHTGL_AttachUrl4 where x.AttachUrlId == AttachUrlId select x).ToList();
            if (q != null)
            {
                Funs.DB.PHTGL_AttachUrl4.DeleteAllOnSubmit(q);
                Funs.DB.SubmitChanges();
            }
        }

    }
}
