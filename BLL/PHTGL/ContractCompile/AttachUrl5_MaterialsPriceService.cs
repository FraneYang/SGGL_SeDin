using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL 
{
   public static   class AttachUrl5_MaterialsPriceService

    {
        /// <summary>
        /// 根据Id获取附件5 材料暂估价表内容
        /// </summary>
        /// <param name="attachUrlItemId"></param>
        /// <returns></returns>
        public static Model.PHTGL_AttachUrl5_MaterialsPrice GetAttachurl5ByitemId(string achUrlItemId)
        {
            return Funs.DB.PHTGL_AttachUrl5_MaterialsPrice.FirstOrDefault(e => e.AttachUrlItemId == achUrlItemId);
        }
        public static Model.PHTGL_AttachUrl5_MaterialsPrice GetAttachurl5ById(string achUrlid)
        {
            return Funs.DB.PHTGL_AttachUrl5_MaterialsPrice.FirstOrDefault(e => e.AttachUrlId == achUrlid);
        }

        /// <summary>
        /// 增加附件5材料暂估价表
        /// </summary>
        /// <param name="url5"></param>
        public static void AddAttachurl5(Model.PHTGL_AttachUrl5_MaterialsPrice url5)
        {
            Model.PHTGL_AttachUrl5_MaterialsPrice newUrl = new Model.PHTGL_AttachUrl5_MaterialsPrice();
            newUrl.AttachUrlItemId = url5.AttachUrlItemId;
            newUrl.AttachUrlId = url5.AttachUrlId;
            newUrl.OrderNumber = url5.OrderNumber;
            newUrl.Name = url5.Name;
            newUrl.Spec = url5.Spec;
            newUrl.Material = url5.Material;
            newUrl.Company = url5.Company;
            newUrl.UnitPrice = url5.UnitPrice;
            newUrl.Remarks = url5.Remarks;
            Funs.DB.PHTGL_AttachUrl5_MaterialsPrice.InsertOnSubmit(newUrl);
            Funs.DB.SubmitChanges();
        }

        /// <summary>
        /// 修改材料暂估价表
        /// </summary>
        /// <param name="sub"></param>
        public static void UpdateAttachurl5(Model.PHTGL_AttachUrl5_MaterialsPrice url5)
        {
            Model.PHTGL_AttachUrl5_MaterialsPrice newUrl = Funs.DB.PHTGL_AttachUrl5_MaterialsPrice.FirstOrDefault(e => e.AttachUrlItemId == url5.AttachUrlItemId);
            if (newUrl != null)
            {
                newUrl.AttachUrlItemId = url5.AttachUrlItemId;
                newUrl.AttachUrlId = url5.AttachUrlId;
                newUrl.OrderNumber = url5.OrderNumber;
                newUrl.Name = url5.Name;
                newUrl.Spec = url5.Spec;
                newUrl.Material = url5.Material;
                newUrl.Company = url5.Company;
                newUrl.UnitPrice = url5.UnitPrice;
                newUrl.Remarks = url5.Remarks;
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
        /// <summary>
        /// 删除材料暂估价表内容
        /// </summary>
        /// <param name="AttachUrlItemId"></param>
        public static void DeleteUrl5ById(string AttachUrlItemId)
        {
            Model.PHTGL_AttachUrl5_MaterialsPrice url5 = Funs.DB.PHTGL_AttachUrl5_MaterialsPrice.FirstOrDefault(e => e.AttachUrlItemId == AttachUrlItemId);
            if (url5 != null)
            {
                Funs.DB.PHTGL_AttachUrl5_MaterialsPrice.DeleteOnSubmit(url5);
                Funs.DB.SubmitChanges();
            }
        }
    }
}
