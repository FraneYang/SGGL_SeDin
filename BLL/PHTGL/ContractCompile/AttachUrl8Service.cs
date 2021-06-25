using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
  public static  class AttachUrl8Service


    {
        /// <summary>
        /// 根据Id获取附件8 总承包商关键人员一览内容
        /// </summary>
        /// <param name="attachUrlItemId"></param>
        /// <returns></returns>
        public static Model.PHTGL_AttachUrl8 GetAttachurl8ById(string attachUrlId)
        {
            return Funs.DB.PHTGL_AttachUrl8.FirstOrDefault(e => e.AttachUrlId == attachUrlId);
        }

        /// <summary>
        /// 增加附件8总承包商关键人员一览
        /// </summary>
        /// <param name="url8"></param>
        public static void AddAttachurl8(Model.PHTGL_AttachUrl8 url8)
        {
            Model.PHTGL_AttachUrl8 newUrl = new Model.PHTGL_AttachUrl8();
            newUrl.AttachUrlItemId = url8.AttachUrlItemId;
            newUrl.AttachUrlId = url8.AttachUrlId;
            newUrl.ProjectManager = url8.ProjectManager;
            newUrl.ProjectManager_deputy = url8.ProjectManager_deputy;
            newUrl.SafetyDirector = url8.SafetyDirector;
            newUrl.ControlManager = url8.ControlManager;
            newUrl.DesignManager = url8.DesignManager;
            newUrl.PurchasingManager = url8.PurchasingManager;
            newUrl.ConstructionManager = url8.ConstructionManager;
            newUrl.ConstructionManager_deputy = url8.ConstructionManager_deputy;
            newUrl.QualityManager = url8.QualityManager;
            newUrl.HSEManager = url8.HSEManager;
            newUrl.DrivingManager = url8.DrivingManager;
            newUrl.FinancialManager = url8.FinancialManager;
            newUrl.OfficeManager = url8.OfficeManager;
            newUrl.AttachUrlContent = url8.AttachUrlContent;

            Funs.DB.PHTGL_AttachUrl8.InsertOnSubmit(newUrl);
            Funs.DB.SubmitChanges();
        }

        /// <summary>
        /// 修改附件8总承包商关键人员一览
        /// </summary>
        /// <param name="sub"></param>
        public static void UpdateAttachurl8(Model.PHTGL_AttachUrl8 url8)
        {
            Model.PHTGL_AttachUrl8 newUrl = Funs.DB.PHTGL_AttachUrl8.FirstOrDefault(e => e.AttachUrlItemId == url8.AttachUrlItemId);
            if (newUrl != null)
            {
                newUrl.AttachUrlItemId = url8.AttachUrlItemId;
                newUrl.AttachUrlId = url8.AttachUrlId;
                newUrl.ProjectManager = url8.ProjectManager;
                newUrl.ProjectManager_deputy = url8.ProjectManager_deputy;
                newUrl.SafetyDirector = url8.SafetyDirector;
                newUrl.ControlManager = url8.ControlManager;
                newUrl.DesignManager = url8.DesignManager;
                newUrl.PurchasingManager = url8.PurchasingManager;
                newUrl.ConstructionManager = url8.ConstructionManager;
                newUrl.ConstructionManager_deputy = url8.ConstructionManager_deputy;
                newUrl.QualityManager = url8.QualityManager;
                newUrl.HSEManager = url8.HSEManager;
                newUrl.DrivingManager = url8.DrivingManager;
                newUrl.FinancialManager = url8.FinancialManager;
                newUrl.OfficeManager = url8.OfficeManager;
                newUrl.AttachUrlContent = url8.AttachUrlContent;

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
        /// 删除附件8总承包商关键人员一览
        /// </summary>
        /// <param name="AttachUrlItemId"></param>
        public static void Deleteurl8ById(string AttachUrlItemId)
        {
            Model.PHTGL_AttachUrl8 url8 = Funs.DB.PHTGL_AttachUrl8.FirstOrDefault(e => e.AttachUrlItemId == AttachUrlItemId);
            if (url8 != null)
            {
                Funs.DB.PHTGL_AttachUrl8.DeleteOnSubmit(url8);
                Funs.DB.SubmitChanges();
            }
        }
    }
}
