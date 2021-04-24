using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public   static class AttachUrl5_DevicePriceService
    {
        /// <summary>
        /// 根据Id获取附件5 工程设备暂估价表内容
        /// </summary>
        /// <param name="attachUrlItemId"></param>
        /// <returns></returns>
        public static Model.PHTGL_AttachUrl5_DevicePrice GetAttachurl5ByItemId(string achUrlItemId)
        {
            return Funs.DB.PHTGL_AttachUrl5_DevicePrice.FirstOrDefault(e => e.AttachUrlItemId == achUrlItemId);
        }

        public static Model.PHTGL_AttachUrl5_DevicePrice GetAttachurl5ById(string achUrlid )
        {
            return Funs.DB.PHTGL_AttachUrl5_DevicePrice.FirstOrDefault(e=>e.AttachUrlId==achUrlid);
        }

        /// <summary>
        /// 增加附件5工程设备暂估价表
        /// </summary>
        /// <param name="url5"></param>
        public static void AddAttachurl5(Model.PHTGL_AttachUrl5_DevicePrice url5)
        {
            Model.PHTGL_AttachUrl5_DevicePrice newUrl = new Model.PHTGL_AttachUrl5_DevicePrice();
            newUrl.AttachUrlItemId = url5.AttachUrlItemId;
            newUrl.AttachUrlId = url5.AttachUrlId;
            newUrl.OrderNumber = url5.OrderNumber;
            newUrl.Name = url5.Name;
            newUrl.Company = url5.Company;
            newUrl.Amount = url5.Amount;
            newUrl.UnitPrice = url5.UnitPrice;
            newUrl.Totalprice = url5.Totalprice;
            newUrl.Remarks = url5.Remarks;
            Funs.DB.PHTGL_AttachUrl5_DevicePrice.InsertOnSubmit(newUrl);
            Funs.DB.SubmitChanges();
        }

        /// <summary>
        /// 修改工程设备暂估价表
        /// </summary>
        /// <param name="sub"></param>
        public static void UpdateAttachurl5(Model.PHTGL_AttachUrl5_DevicePrice url5)
        {
            Model.PHTGL_AttachUrl5_DevicePrice newUrl = Funs.DB.PHTGL_AttachUrl5_DevicePrice.FirstOrDefault(e => e.AttachUrlItemId == url5.AttachUrlItemId);
            if (newUrl != null)
            {
                newUrl.AttachUrlItemId = url5.AttachUrlItemId;
                newUrl.AttachUrlId = url5.AttachUrlId;
                newUrl.OrderNumber = url5.OrderNumber;
                newUrl.Name = url5.Name;
                newUrl.Company = url5.Company;
                newUrl.Amount = url5.Amount;
                newUrl.UnitPrice = url5.UnitPrice;
                newUrl.Totalprice = url5.Totalprice;
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

        public static void DeleteUrl5ById(string AttachUrlItemId)
        {
            Model.PHTGL_AttachUrl5_DevicePrice url5 = Funs.DB.PHTGL_AttachUrl5_DevicePrice.FirstOrDefault(e => e.AttachUrlItemId == AttachUrlItemId);
            if (url5 != null)
            {
                Funs.DB.PHTGL_AttachUrl5_DevicePrice.DeleteOnSubmit(url5);
                Funs.DB.SubmitChanges();
            }
        }
    }
}
